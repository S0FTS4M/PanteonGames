using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProductFactory
{
    /// <summary>
    /// Creates a placeable object and returns that object. If specified type does not exists returns null.
    /// </summary>
    /// <param name="productType"></param>
    /// <returns></returns>
    public static IBuildable CreateProduct(BuildingType productType)
    {
        IBuildable building = null;
        switch (productType)
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
