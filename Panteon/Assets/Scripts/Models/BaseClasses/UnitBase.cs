using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase
{

    protected UnitBase(UnitType unitType)
    {
        Type = unitType;
    }
    public string Name { get; set; }
    public int XDimension { get; set; }
    public int YDimension { get; set; }
    public string ImageName { get; set; }
    //Bottom Left corner tile as a pivot tile
    public Tile PivotTile;
    public UnitType Type { get; set; }


}
