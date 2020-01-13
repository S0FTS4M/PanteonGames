using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class UnitSpawnTileSelector : ISpawnPointSelector
{

    public Tile SelectSpawnPoint(Border xBorders, Border yBorders)
    {
        for (int i = xBorders.start - 1; i <= xBorders.end + 1; i++)
        {
            for (int j = yBorders.start - 1; j <= yBorders.end + 1; j++)
            {
                Tile tile = WorldController.Instance.World.GetTileAt(i, j);
                if (tile?.IsOccupied == false)
                {
                    tile.IsSpawnPoint = true;
                    return tile;
                }
            }
        }

        return null;
    }
}
