using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TilesSpritesController : MonoBehaviour
{
    private Tile _oldHoveredTile;
    private readonly List<Tile> _cachedTiles = new List<Tile>();

    [SerializeField] private Transform _gridParent;
    // Start is called before the first frame update
    void OnEnable()
    {
        Tile.OnTileCreated += OnTileCreated;
        Tile.OnHoverTile += WorldController_OnHoverTile;
    }


    private void ResetTiles()
    {
        foreach (var tileGo in from tile in _cachedTiles where tile.IsOccupied == false select WorldController.instance.TileToGoMap[tile])
        {
            tileGo.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }

        _cachedTiles.Clear();
    }

    private bool WorldController_OnHoverTile(Tile tile)
    {
        var unitBase = WorldController.instance.SelectedUnitForPlacing;
        ResetTiles();

        if (tile == null)
            return false;
        var world = WorldController.instance.World;
        var xBorders = world.CalculateXBorders(tile, unitBase.XDimension);

        var yBorders = world.CalculateYBorders(tile, unitBase.YDimension);

        //if the area we are trying to place has a non-valid tile we will indicate with red color that it is forbidden 
        var isOccupied = false;
        var activeTiles = world.GetTilesInSelectedArea(xBorders, yBorders);
        foreach (var t in activeTiles)
        {
            if (t.IsOccupied)
            {
                isOccupied = true;
                continue;
            }
            var currentTile = t;
            var tileGo = WorldController.instance.TileToGoMap[currentTile];
            tileGo.GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
            _cachedTiles.Add(currentTile);
        }
        //there is  a non-valid tile in the area return false
        if (isOccupied)
            return false;

        foreach (var currentTile in activeTiles)
        {
            var tileGo = WorldController.instance.TileToGoMap[currentTile];
            tileGo.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            _cachedTiles.Add(currentTile);
        }
        return true;
    }

    private void OnTileCreated(Tile tile)
    {
        var tileGo = new GameObject($"Tile_{tile.X}_{tile.Y}");
        tileGo.transform.SetParent(_gridParent, true);
        tileGo.transform.position = new Vector3(tile.X, tile.Y, 0);
        WorldController.instance.TileToGoMap.Add(tile, tileGo);
        var tileSr = tileGo.AddComponent<SpriteRenderer>();
        tileSr.sprite = SpritesController.instance.GetSpriteByName("Tile");
        tileSr.sortingLayerName = "Tile";

    }
}
