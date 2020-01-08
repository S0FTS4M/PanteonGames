using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class SoldierUnit : IProducible, IMoveable
{
    public SoldierUnit(int xDim, int yDim, string name, string imageName)
    {
        XDimension = xDim;
        YDimension = yDim;
        Name = name;
        ImageName = imageName;

    }
    public string Name { get; }
    public int XDimension { get; }
    public int YDimension { get; }
    public string ImageName { get; }
    public ProducibleType Type => ProducibleType.Soldier;


    public IEnumerable Move(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public void Place()
    {
        throw new System.NotImplementedException();
    }
}
