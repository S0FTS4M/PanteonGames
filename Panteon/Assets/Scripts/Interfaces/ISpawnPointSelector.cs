using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public interface ISpawnPointSelector
{
    Tile SelectSpawnPoint(Border xBorders, Border yBorders);
}