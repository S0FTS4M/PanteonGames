using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Barracks : IBuildable, ICanProduce
{

    public Barracks(int xDim, int yDim, string name, string imageName, IProducible soldier)
    {
        this.producible = soldier;
        XDimension = xDim;
        YDimension = yDim;
        Name = name;
        ImageName = imageName;
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

    public ProducibleType Type => ProducibleType.Barrack;

    public void Place(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public IProducible producible { get; }

    public IMoveable Produce()
    {
        throw new System.NotImplementedException();
    }



}
