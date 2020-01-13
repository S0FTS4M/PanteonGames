using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.U2D;

public class MoveableUnitSpriteController : MonoBehaviour
{
    Dictionary<MoveableUnitBase, GameObject> _moveableToGoMap;

    World world
    {
        get { return WorldController.Instance.World; }
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveableUnitBase.OnMoveableUnitCreated += OnMoveableUnitCreated;
        _moveableToGoMap = new Dictionary<MoveableUnitBase, GameObject>();

    }

    public void OnMoveableUnitCreated(MoveableUnitBase moveableUnit)
    {
        Tile selectedTile = WorldController.Instance.SelectedTileForInfo;
        moveableUnit.SetTile(((IProducer)selectedTile.PlacedUnit).SpawnPointTile);
        moveableUnit.currTile.SetUnit(moveableUnit);
        GameObject moveable_go = new GameObject(moveableUnit.Name);

        _moveableToGoMap.Add(moveableUnit, moveable_go);
        moveable_go.transform.position = new Vector3(moveableUnit.X, moveableUnit.Y, 0);
        moveable_go.transform.SetParent(this.transform, true);

        SpriteRenderer sr = moveable_go.AddComponent<SpriteRenderer>();
        sr.sprite = SpritesController.Instance.GetSpriteByName(moveableUnit.ImageName);
        sr.sortingLayerName = "Moveables";


        // Register our callback so that our GameObject gets updated whenever
        // the object's into changes.
        moveableUnit.OnUnitMove += OnMoveUnit;
    }

    void OnMoveUnit(MoveableUnitBase moveableUnit)
    {
        if (_moveableToGoMap.ContainsKey(moveableUnit) == false)
        {
            Debug.LogError("OnCharacterChanged -- trying to change visuals for character not in our map.");
            return;
        }

        GameObject char_go = _moveableToGoMap[moveableUnit];


        char_go.transform.position = new Vector3(moveableUnit.X, moveableUnit.Y, 0);
    }

}
