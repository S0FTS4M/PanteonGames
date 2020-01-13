using System;

public abstract class StaticUnitBase : UnitBase, IPlaceable
{
    public static event Action<StaticUnitBase> OnUnitPlace;
    protected StaticUnitBase(UnitType unitType) : base(unitType)
    {
    }

    public void PlaceUnit(World world)
    {
        for (int x = pivotTile.X; x < pivotTile.X + XDimension; x++)
        {
            for (int y = pivotTile.Y; y < pivotTile.Y + YDimension; y++)
            {
                Tile currentTile = world.GetTileAt(x, y);
                currentTile.SetUnit(this);
            }
        }
        OnUnitPlace?.Invoke(this);
    }
}
