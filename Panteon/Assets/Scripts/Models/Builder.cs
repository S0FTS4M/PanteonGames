using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Builder
{
    private readonly IFactory buildingFactory;

    public Builder(IFactory factory)
    {
        buildingFactory = factory;
    }

    public IBuildable Build(ProducibleType producibleType)
    {
        return buildingFactory.Create(producibleType);
    }
}
