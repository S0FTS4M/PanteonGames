using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class ProductFactory : IFactory
{
    /// <summary>
    /// Creates a building object and returns that object. If specified type does not exists returns null.
    /// </summary>
    /// <param name="buildingType"></param>
    /// <returns></returns>
    private static ProductFactory Instance;

    public static IFactory getInstance()
    {
        if (Instance == null)
            Instance = new ProductFactory();

        return Instance;
    }
    public IBuildable Create(BuildingType buildingType)
    {
        IBuildable building = null;
        switch (buildingType)
        {
            case BuildingType.Barrack:
                building = new Barracks(4, 4, "Barrack", new SoldierUnit());
                break;
            case BuildingType.PowePlant:
                building = new PowerPlants();
                break;

            default:
                Debug.LogError("This type of product is not specified.");
                break;

        }
        return building;
    }
}
