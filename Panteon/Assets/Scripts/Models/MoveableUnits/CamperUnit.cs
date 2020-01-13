using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class CamperUnit : MoveableUnitBase
{
    public CamperUnit() : base(UnitType.MoveableUnit)
    {

        Name = "Camper Unit";
        ImageName = "Camper";
    }
}

