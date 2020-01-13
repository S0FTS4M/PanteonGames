using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class MoveableUnitSpriteController : MonoBehaviour
{
    Dictionary<MoveableUnitBase, GameObject> _moveableToGoMap;

    // Start is called before the first frame update
    void Start()
    {
        MoveableUnitBase.OnMoveableUnitCreated += OnMoveableUnitCreated;
        _moveableToGoMap = new Dictionary<MoveableUnitBase, GameObject>();

    }

    public void OnMoveableUnitCreated(MoveableUnitBase moveableUnit)
    {
        var selectedTile = WorldController.instance.SelectedTileForInfo;
        moveableUnit.SetTile(((IProducer)selectedTile.PlacedUnit).SpawnPointTile);
        moveableUnit.CurrentTile.SetUnit(moveableUnit);
        var moveableGo = new GameObject(moveableUnit.Name);

        _moveableToGoMap.Add(moveableUnit, moveableGo);
        moveableGo.transform.position = new Vector3(moveableUnit.X, moveableUnit.Y, 0);
        moveableGo.transform.SetParent(this.transform, true);

        var sr = moveableGo.AddComponent<SpriteRenderer>();
        sr.sprite = SpritesController.instance.GetSpriteByName(moveableUnit.ImageName);
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

        var charGo = _moveableToGoMap[moveableUnit];


        charGo.transform.position = new Vector3(moveableUnit.X, moveableUnit.Y, 0);
    }

}
