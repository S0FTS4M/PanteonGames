using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    int x;
    int y;

    World world;

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }
}
