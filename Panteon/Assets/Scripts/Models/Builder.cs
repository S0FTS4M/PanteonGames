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

    public IBuildable Build(BuildingType buildingType)
    {
        return buildingFactory.Create(buildingType);
    }
}
