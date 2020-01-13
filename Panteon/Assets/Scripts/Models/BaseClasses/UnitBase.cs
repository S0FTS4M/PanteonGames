public abstract class UnitBase
{

    protected UnitBase(UnitType unitType)
    {
        Type = unitType;
    }
    public string Name { get; set; }
    public int XDimension { get; set; }
    public int YDimension { get; set; }
    public string ImageName { get; set; }
    //Bottom Left corner tile as a pivot tile
    public Tile pivotTile;
    public UnitType Type { get; set; }


}
