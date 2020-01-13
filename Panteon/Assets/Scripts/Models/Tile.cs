
using System;
using UnityEngine;

public class Tile
{
    public static event Action<Tile> OnTileCreated;
    public static event Func<Tile, bool> OnHoverTile;
    public static event Action<Tile> OnTileClicked;
    public int X { get; }
    public int Y { get; }

    public World World { get; }

    public UnitBase PlacedUnit
    {
        get;
        private set;
    }

    public float MovementCost => PlacedUnit != null ? 0 : 1;

    public bool IsOccupied => PlacedUnit != null || IsSpawnPoint;

    public bool IsSpawnPoint { get; set; }
    public Tile(World world, int x, int y)
    {
        this.World = world;
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
        var ns = diagOkay == false ? new Tile[4] : new Tile[8];

        var n = World.GetTileAt(X, Y + 1);
        ns[0] = n;  // Could be null, but that's okay.
        n = World.GetTileAt(X + 1, Y);
        ns[1] = n;  // Could be null, but that's okay.
        n = World.GetTileAt(X, Y - 1);
        ns[2] = n;  // Could be null, but that's okay.
        n = World.GetTileAt(X - 1, Y);
        ns[3] = n;  // Could be null, but that's okay.

        if (diagOkay != true) return ns;

        n = World.GetTileAt(X + 1, Y + 1);
        ns[4] = n;  // Could be null, but that's okay.
        n = World.GetTileAt(X + 1, Y - 1);
        ns[5] = n;  // Could be null, but that's okay.
        n = World.GetTileAt(X - 1, Y - 1);
        ns[6] = n;  // Could be null, but that's okay.
        n = World.GetTileAt(X - 1, Y + 1);
        ns[7] = n;  // Could be null, but that's okay.

        return ns;
    }

}
