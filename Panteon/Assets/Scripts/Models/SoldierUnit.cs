using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class SoldierUnit : IProducible, IMoveable
{
    public string Name { get; }
    public int XDimension => throw new System.NotImplementedException();

    public int YDimension => throw new System.NotImplementedException();

    public string ImageName => throw new System.NotImplementedException();

    public IEnumerable Move(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public void Place()
    {
        throw new System.NotImplementedException();
    }
}
