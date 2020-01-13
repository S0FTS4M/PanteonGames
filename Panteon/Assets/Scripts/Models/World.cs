using System.Collections.Generic;

public class World
{
    private readonly Tile[,] _tiles;

    /// <summary>
    /// width of the world
    /// </summary>
    private int _width;

    /// <summary>
    /// height of the world
    /// </summary>
    private int _height;

    public Path_TileGraph tileGraph;
    public World(int width = 30, int height = 30)
    {
        this._width = width;
        this._height = height;

        //Create Tiles ( this is the only place where all tiles are created)
        _tiles = new Tile[this._width, this._height];

        for (int x = 0; x < this._width; x++)
        {
            for (int y = 0; y < this._height; y++)
            {
                _tiles[x, y] = new Tile(this, x, y);
            }
        }
    }
    /// <summary>
    /// Whenever player adds a static unit to the world we need to reset paths
    /// </summary>
    public void ReCalculatePaths()
    {
        tileGraph = null;
    }
    public int Width => _width;
    public int Height => _height;

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
        return _tiles[x, y];
    }
    public List<Tile> GetTilesInSelectedArea(Border xBorder, Border yBorder)
    {
        var tilesAvailable = new List<Tile>();
        for (var x = xBorder.start; x <= xBorder.end; x++)
        {
            for (var y = yBorder.start; y <= yBorder.end; y++)
            {
                var currentTile = GetTileAt(x, y);
                tilesAvailable.Add(currentTile);
            }
        }

        return tilesAvailable;
    }
    /// <summary>
    ///In the X axis checks for valid tiles. Tile Count will be xDimension of the Unit
    /// </summary>
    /// <param name="startTile">Starting tile for calculations.This is the
    /// tile that player is currently hovering on.</param>
    /// <param name="xDimension">Count of tiles that we need for Unit placing.</param>
    /// <returns>Returns a Border type which includes a start and an end value for X axis.</returns>
    public Border CalculateXBorders(Tile startTile, int xDimension)
    {
        //we need a startX and an endX for border values. Set the initial values as startingTile
        int startX = startTile.X, endX = startTile.X;
        //in every iteration we are checking the tiles on X axis (if in that distance there is a valid Tile)
        var distance = 1;

        for (var dim = 1; dim < xDimension;)
        {
            //Control of the east side
            var tile = GetTileAt(startTile.X + distance, startTile.Y);
            if (tile != null)
            {
                endX = startTile.X + distance;
                //if distance is valid we add one to dim.Because we already pick a valid tile.
                dim++;
                //check if we reached the dimension of the unit
                if (dim >= xDimension)
                    continue;

            }
            //Control of the west side
            tile = GetTileAt(startTile.X - distance, startTile.Y);
            if (tile != null)
            {
                startX = startTile.X - distance;
                dim++;
            }

            distance++;
        }
        return new Border() { start = startX, end = endX };
    }
    /// <summary>
    ///In the Y axis checks for valid tiles. Tile Count will be yDimension of the Unit.
    /// If a tile is not exist, then it is not valid
    /// </summary>
    /// <param name="startTile">Starting tile for calculations.This is the
    /// tile that player is currently hovering on.</param>
    /// <param name="yDimension">Count of tiles that we need for Unit placing.</param>
    /// <returns>Returns a Border type which includes a start and an end value for Y axis.</returns>
    public Border CalculateYBorders(Tile startTile, int yDimension)
    {
        //we need a startY and an endY for border values. Set the initial values as startingTile
        int startY = startTile.Y, endY = startTile.Y;
        //in every iteration we are checking the tiles on Y axis (if in that distance there is a valid Tile)
        var distance = 1;

        for (var dim = 1; dim < yDimension;)
        {
            //Control of the North side
            var tile = GetTileAt(startTile.X, startTile.Y + distance);
            if (tile != null)
            {
                endY = startTile.Y + distance;
                //if distance is valid we add one to dim.Because we already pick a valid tile.
                dim++;
                //check if we reached the dimension of the unit
                if (dim >= yDimension)
                    continue;
            }
            //Control of the South side
            tile = GetTileAt(startTile.X, startTile.Y - distance);
            if (tile != null)
            {
                startY = startTile.Y - distance;
                dim++;
            }

            distance++;

        }
        return new Border() { start = startY, end = endY };
    }

}
