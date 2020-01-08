using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class ProductFactory : IFactory
{
    /// <summary>
    /// Creates a building object and returns that object. If specified type does not exists returns null.
    /// </summary>
    /// <param name="producibleType"></param>
    /// <returns></returns>
    public IBuildable Create(ProducibleType producibleType)
    {
        IBuildable building = null;
        switch (producibleType)
        {
            case ProducibleType.Barrack:
                building = new Barracks(4, 4, "Barrack", "Barrack",
                    new SoldierUnit(1, 1, "Soldier Unit", "Soldier"));
                break;
            case ProducibleType.PowerPlant:
                building = new PowerPlants(2, 3, "Power Plant", "PowerPlant");
                break;

            default:
                Debug.LogError("This type of product is not specified.");
                break;

        }
        return building;
    }
}
