using System;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : StaticUnitBase, IProducer
{
    public Tile SpawnPointTile { get; private set; }
    public List<MoveableUnitBase> MoveableUnitPrototypes { get; }
    private readonly ISpawnPointSelector _spawnPointSelector;

    public Barracks() : base(UnitType.Barrack)
    {
        Name = "Barrack";
        ImageName = "Barrack";
    }
    public Barracks(ISpawnPointSelector spawnPointSelector) : base(UnitType.Barrack)
    {
        MoveableUnitPrototypes = new List<MoveableUnitBase>();
        _spawnPointSelector = spawnPointSelector;
        Name = "Barrack";
        ImageName = "Barrack";
    }

    public void SelectSpawnPoint(Border xBorders, Border yBorders)
    {
        SpawnPointTile = _spawnPointSelector.SelectSpawnPoint(xBorders, yBorders);
        if (SpawnPointTile == null) { Debug.LogError("spawnpoint null geldi"); return; }

    }

    public UnitBase Produce(int index)
    {
        return (UnitBase)MoveableUnitPrototypes[index].Clone();
    }

}
