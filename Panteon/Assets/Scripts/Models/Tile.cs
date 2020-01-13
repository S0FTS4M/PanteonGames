
using System;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Tile
{
    public static event Action<Tile> OnTileCreated;
    public static event Func<Tile, bool> OnHoverTile;
    public static event Action<Tile> OnTileClicked;
    public int X { get; }
    public int Y { get; }

    public World world { get; }

    public UnitBase PlacedUnit
    {
        get;
        private set;
    }

    public float movementCost
    {
        get
        {
            if (PlacedUnit != null)
                return 0; //yürünemez

            return 1;
        }
    }

    public bool IsOccupied => PlacedUnit != null || IsSpawnPoint;

    public bool IsSpawnPoint { get; set; }
    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.X = x;
        this.Y = y;
        OnTileCreated?.Invoke(this);
    }

    public void SetUnit(UnitBase unitBase)
    {
        PlacedUnit = unitBase;
    }

    public void TileClick()
    {
        OnTileClicked?.Invoke(this);
    }

    public bool? TileHover()
    {
        return OnHoverTile?.Invoke(this);
    }
    /// <summary>
    /// Gets the neighbours.
    /// </summary>
    /// <returns>The neighbours.</returns>
    /// <param name="diagOkay">Is diagonal movement okay?.</param>
    public Tile[] GetNeighbours(bool diagOkay = false)
    {
        Tile[] ns;

        if (diagOkay == false)
        {
            ns = new Tile[4];   // Tile order: N E S W
        }
        else
        {
            ns = new Tile[8];   // Tile order : N E S W NE SE SW NW
        }

        Tile n;

        n = world.GetTileAt(X, Y + 1);
        ns[0] = n;  // Could be null, but that's okay.
        n = world.GetTileAt(X + 1, Y);
        ns[1] = n;  // Could be null, but that's okay.
        n = world.GetTileAt(X, Y - 1);
        ns[2] = n;  // Could be null, but that's okay.
        n = world.GetTileAt(X - 1, Y);
        ns[3] = n;  // Could be null, but that's okay.

        if (diagOkay == true)
        {
            n = world.GetTileAt(X + 1, Y + 1);
            ns[4] = n;  // Could be null, but that's okay.
            n = world.GetTileAt(X + 1, Y - 1);
            ns[5] = n;  // Could be null, but that's okay.
            n = world.GetTileAt(X - 1, Y - 1);
            ns[6] = n;  // Could be null, but that's okay.
            n = world.GetTileAt(X - 1, Y + 1);
            ns[7] = n;  // Could be null, but that's okay.
        }

        return ns;
    }
    public bool IsNeighbour(Tile tile, bool diagOkay = false)
    {
        // Check to see if we have a difference of exactly ONE between the two
        // tile coordinates.  Is so, then we are vertical or horizontal neighbours.
        return
            Mathf.Abs(this.X - tile.X) + Mathf.Abs(this.Y - tile.Y) == 1 ||  // Check hori/vert adjacency
            (diagOkay && (Mathf.Abs(this.X - tile.X) == 1 && Mathf.Abs(this.Y - tile.Y) == 1)) // Check diag adjacency
            ;
    }

}
