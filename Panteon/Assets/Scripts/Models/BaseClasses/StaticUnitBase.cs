using System;

public abstract class StaticUnitBase : UnitBase, IPlaceable
{
    public static event Action<StaticUnitBase> OnUnitPlace;
    protected StaticUnitBase(UnitType unitType) : base(unitType)
    {
    }

    public void PlaceUnit(World world)
    {
        for (int x = PivotTile.X; x < PivotTile.X + XDimension; x++)
        {
            for (int y = PivotTile.Y; y < PivotTile.Y + YDimension; y++)
            {
                Tile currentTile = world.GetTileAt(x, y);
                currentTile.SetUnit(this);
            }
        }
        OnUnitPlace?.Invoke(this);
    }
}
