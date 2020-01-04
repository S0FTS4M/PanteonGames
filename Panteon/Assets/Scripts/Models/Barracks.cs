using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : IBuildable, ICanProduce
{
    IPlaceable soldier;

    public string Name => throw new System.NotImplementedException();

    public int XDimension => throw new System.NotImplementedException();

    public int YDimension => throw new System.NotImplementedException();

    public string ImageName => throw new System.NotImplementedException();

    public void Place()
    {
        throw new System.NotImplementedException();
    }

    public IMoveable Produce()
    {
        throw new System.NotImplementedException();
    }


}
