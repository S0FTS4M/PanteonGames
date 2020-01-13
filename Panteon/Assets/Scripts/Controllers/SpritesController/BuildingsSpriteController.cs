using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BuildingsSpriteController : MonoBehaviour
{
    public static BuildingsSpriteController Instance;

    Dictionary<string, Sprite> buildingsSprites;

    //for every static unit we create needs to be a child of an object 
    [SerializeField]
    private Transform buildingsParent;
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
        buildingsSprites = new Dictionary<string, Sprite>();
        LoadSprites();
        StaticUnitBase.OnUnitPlace += OnBuildingCreated;
    }
    void LoadSprites()
    {

        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Sprites/Units/Buildings");
        Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(sprites);
        for (int i = 0; i < sprites.Length; i++)
        {
            //Every sprite has a (Clone) extension because of the sprite atlas
            buildingsSprites.Add(
                sprites[i].name.Replace("(Clone)", ""),
                sprites[i]);

        }
    }

    public Sprite GetBuildingSpriteByName(string spriteName)
    {
        if (buildingsSprites.ContainsKey(spriteName))
            return buildingsSprites[spriteName];

        Debug.LogError("GetBuildingSpriteByName::Sprite could not find with name " + spriteName);
        return null;
    }

    public void OnBuildingCreated(UnitBase unit)
    {
        GameObject producibleGo = new GameObject(unit.Name);
        producibleGo.transform.SetParent(buildingsParent);
        producibleGo.transform.position = new Vector3(unit.PivotTile.X, unit.PivotTile.Y, 0);

        SpriteRenderer sr = ((GameObject)producibleGo).AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Buildings";
        sr.sprite = buildingsSprites[unit.ImageName];



    }



}
