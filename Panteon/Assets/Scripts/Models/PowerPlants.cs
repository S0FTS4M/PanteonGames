using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class PowerPlants : IBuildable
{

    public PowerPlants(int xDim, int yDim, string name, string imageName)
    {
        XDimension = xDim;
        YDimension = yDim;
        Name = name;
        ImageName = imageName;
    }
    public ProducibleType Type => ProducibleType.PowerPlant;

    public void Place(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public void Place()
    {
        throw new System.NotImplementedException();
    }

    public string Name { get; }
    public int XDimension { get; }
    public int YDimension { get; }
    public string ImageName { get; }
}
