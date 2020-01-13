using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class ProductFactory : IFactory
{
    /// <summary>
    /// Creates a building object and returns that object. If specified type does not exists returns null.
    /// </summary>
    /// <param name="unitType"></param>
    /// <returns></returns>
    public UnitBase Create(UnitType unitType)
    {
        UnitBase unitBase = null;
        switch (unitType)
        {
            case UnitType.Barrack:

                unitBase = new Barracks(new UnitSpawnTileSelector())
                {
                    XDimension = 4,
                    YDimension = 4,
                    MoveableUnitPrototypes =
                    {
                        CreateMoveableUnit("soldier")
                    }
                };

                break;
            case UnitType.PowerPlant:
                unitBase = new PowerPlants()
                {
                    XDimension = 2,
                    YDimension = 3
                };
                break;
            case UnitType.Camp:

                unitBase = new Camp(new UnitSpawnTileSelector())
                {
                    XDimension = 3,
                    YDimension = 2,
                    MoveableUnitPrototypes = {
                        CreateMoveableUnit("camper"),
                        CreateMoveableUnit("soldier") }
                };
                break;
            default:
                Debug.LogError("This type of product is not specified.");
                break;

        }
        return unitBase;
    }

    MoveableUnitBase CreateMoveableUnit(string name)
    {
        MoveableUnitBase moveableUnit = null;
        switch (name.ToLower())
        {
            case "camper":
                moveableUnit = new CamperUnit
                {
                    XDimension = 1,
                    YDimension = 1
                };
                break;
            case "soldier":
                moveableUnit = new SoldierUnit
                {
                    XDimension = 1,
                    YDimension = 1
                };
                break;

        }

        return moveableUnit;
    }

}
