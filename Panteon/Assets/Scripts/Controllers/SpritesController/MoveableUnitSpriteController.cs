using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.U2D;

public class MoveableUnitSpriteController : MonoBehaviour
{
    public static MoveableUnitSpriteController Instance;

    Dictionary<MoveableUnitBase, GameObject> moveableToGOMap;
    Dictionary<string, Sprite> moveableUnitSprites;

    World world
    {
        get { return WorldController.Instance.World; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;
        MoveableUnitBase.OnMoveableUnitCreated += OnMoveableUnitCreated;
        moveableToGOMap = new Dictionary<MoveableUnitBase, GameObject>();
        moveableUnitSprites = new Dictionary<string, Sprite>();
        LoadSprites();
    }
    void LoadSprites()
    {
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Sprites/Units/Moveables");
        Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(sprites);
        for (int i = 0; i < sprites.Length; i++)
        {

            moveableUnitSprites.Add(
                sprites[i].name.Replace("(Clone)", ""),
                sprites[i]);

        }
    }

    public Sprite GetMoveableSpriteByName(string spriteName)
    {
        if (moveableUnitSprites.ContainsKey(spriteName))
            return moveableUnitSprites[spriteName];


        return null;
    }
    public void OnMoveableUnitCreated(MoveableUnitBase moveableUnit)
    {
        Tile selectedTile = WorldController.Instance.SelectedTileForInfo;
        moveableUnit.SetTile(((IProducer)selectedTile.PlacedUnit).SpawnPointTile);
        moveableUnit.currTile.SetUnit(moveableUnit);
        GameObject moveable_go = new GameObject(moveableUnit.Name);

        moveableToGOMap.Add(moveableUnit, moveable_go);
        moveable_go.transform.position = new Vector3(moveableUnit.X, moveableUnit.Y, 0);
        moveable_go.transform.SetParent(this.transform, true);

        SpriteRenderer sr = moveable_go.AddComponent<SpriteRenderer>();
        sr.sprite = moveableUnitSprites[moveableUnit.ImageName];
        sr.sortingLayerName = "Moveables";


        // Register our callback so that our GameObject gets updated whenever
        // the object's into changes.
        moveableUnit.OnUnitMove += OnMoveUnit;
    }

    void OnMoveUnit(MoveableUnitBase moveableUnit)
    {
        if (moveableToGOMap.ContainsKey(moveableUnit) == false)
        {
            Debug.LogError("OnCharacterChanged -- trying to change visuals for character not in our map.");
            return;
        }

        GameObject char_go = moveableToGOMap[moveableUnit];


        char_go.transform.position = new Vector3(moveableUnit.X, moveableUnit.Y, 0);
    }

}
