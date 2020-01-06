using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{

    Tile[,] tiles;

    /// <summary>
    /// width of the world
    /// </summary>
    int width;

    /// <summary>
    /// height of the world
    /// </summary>
    int height;

    public World(int width = 30, int height = 30)
    {
        this.width = width;
        this.height = height;

        //Create Tiles ( this is the only place where all tiles are created)
        tiles = new Tile[this.width, this.height];

        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

    }

    public int Width { get => width; private set => width = value; }
    public int Height { get => height; private set => height = value; }

    /// <summary>
    /// returns the tile with specific location.
    /// </summary>
    /// <param name="x"> X position of tile</param>
    /// <param name="y"> Y position of tile</param>
    /// <returns>Tile object in that location. If x or y out of range returns null</returns>
    public Tile GetTileAt(int x, int y)
    {
        if (x >= Width || x < 0 || y >= Height || y < 0)
        {
            //   Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
            return null;
        }
        // Debug.Log($"{x},{y}");
        return tiles[x, y];
    }





}
