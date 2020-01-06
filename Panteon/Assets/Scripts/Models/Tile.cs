
using Assets.Scripts.Interfaces;

public class Tile
{
    public int X { get; }
    public int Y { get; }

    World world;

    public IProducible PlacedUnit
    {
        get;
        set;
    }
    public bool IsOccupied => PlacedUnit != null;

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.X = x;
        this.Y = y;

    }
}
