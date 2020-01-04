using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable : IProduceable
{
    string Name { get; }

    void Place(int x, int y);
}