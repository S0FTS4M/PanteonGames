using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    int XDimension { get; }
    int YDimension { get; }
    string ImageName { get; }

    void Place();
}