using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class SoldierUnit : MoveableUnitBase
{
    public SoldierUnit() : base(UnitType.MoveableUnit)
    {

        Name = "Soldier Unit";
        ImageName = "Soldier";
    }
}

