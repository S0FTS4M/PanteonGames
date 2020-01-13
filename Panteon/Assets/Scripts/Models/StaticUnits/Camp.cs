using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class Camp : StaticUnitBase, IProducer
{
    private readonly ISpawnPointSelector _spawnPointSelector;

    public Camp() : base(UnitType.Camp)
    {
        Name = "Camp";
        ImageName = "Camp";
    }
    public Camp(ISpawnPointSelector spawnPointSelector) : base(UnitType.Camp)
    {
        MoveableUnitPrototypes = new List<MoveableUnitBase>();
        _spawnPointSelector = spawnPointSelector;
        Name = "Camp";
        ImageName = "Camp";
    }

    public void SelectSpawnPoint(Border xBorders, Border yBorders)
    {
        SpawnPointTile = _spawnPointSelector.SelectSpawnPoint(xBorders, yBorders);
        if (SpawnPointTile != null) return;
        Debug.LogError("spawn point is null");

    }

    public Tile SpawnPointTile { get; private set; }
    public List<MoveableUnitBase> MoveableUnitPrototypes { get; set; }

    public UnitBase Produce(int index)
    {
        return (UnitBase)MoveableUnitPrototypes[index].Clone();
    }

}
