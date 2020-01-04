using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : IBuildable, ICanProduce
{
    IProduceable soldier;

    public Barracks(int xDim, int yDim, string name, IProduceable soldier)
    {
        this.soldier = soldier;
        XDimension = xDim;
        YDimension = yDim;
        Name = name;
    }

    public string Name
    {
        get; private set;
    }

    public int XDimension
    {
        get; private set;
    }

    public int YDimension
    {
        get; private set;
    }
    public string ImageName
    {
        get; private set;
    }

    public void Place(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public IMoveable Produce()
    {
        throw new System.NotImplementedException();
    }



}
