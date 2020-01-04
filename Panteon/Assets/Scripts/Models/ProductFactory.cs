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
    public static IPlaceable CreateProduct(ProductType productType)
    {
        IPlaceable product = null;
        switch (productType)
        {
            case ProductType.Barrack:
                product = new Barracks();
                break;
            case ProductType.PowePlant:
                product = new PowerPlants();
                break;
            case ProductType.SoldierUnit:
                product = new SoldierUnit();
                break;
            default:
                Debug.LogError("This type of product is not specified.");
                break;

        }
        return product;
    }
}
