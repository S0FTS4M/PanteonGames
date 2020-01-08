using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public static class Factory
{
    private static IFactory factory;

    public static IFactory GetFactoryOfType<T>() where T : new()
    {
        return factory ?? (IFactory)new T();
    }
}
