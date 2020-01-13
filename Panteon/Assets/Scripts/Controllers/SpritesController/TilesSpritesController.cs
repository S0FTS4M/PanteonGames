using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;


public class TilesSpritesController : MonoBehaviour
{


    int say = 0;
    Tile oldHoveredTile;
    List<Tile> cachedTiles = new List<Tile>();

    [SerializeField]
    Transform gridParent;
    // Start is called before the first frame update
    void OnEnable()
    {
        Tile.OnTileCreated += OnTileCreated;
        Tile.OnHoverTile += WorldController_OnHoverTile;

    }



    void ResetTiles()
    {
        for (int i = 0; i < cachedTiles.Count; i++)
        {
            if (cachedTiles[i].IsOccupied == false)
            {
                GameObject tileGO = WorldController.Instance.TileToGoMap[cachedTiles[i]];


                tileGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            }
        }
        cachedTiles.Clear();
    }

    private bool WorldController_OnHoverTile(Tile tile)
    {
        UnitBase unitBase = WorldController.Instance.SelectedUnitForPlacing;
        ResetTiles();

        if (tile == null)
            return false;
        World world = WorldController.Instance.World;
        Border xBorders = world.CalculateXBorders(tile, unitBase.XDimension);

        Border yBorders = world.CalculateYBorders(tile, unitBase.YDimension);



        // Debug.Log(xBorders + "____" + yBorders);
        //eğer yerleştirmeye çalıştığımız alan dolu ise kırmızı indicator ile bu alanın yasaklı olduğunu bileceğiz
        bool _isOccupied = false;
        List<Tile> activeTiles = world.GetTilesInSelectedArea(xBorders, yBorders);
        for (int i = 0; i < activeTiles.Count; i++)
        {

            if (activeTiles[i].IsOccupied)
            {
                _isOccupied = true;
                continue;
            }
            Tile currentTile = activeTiles[i];
            GameObject tileGO = WorldController.Instance.TileToGoMap[currentTile];
            tileGO.GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
            cachedTiles.Add(currentTile);
        }
        //yasaklı bir alan olduğunu tespit ettik false döndür
        if (_isOccupied)
            return false;


        for (int i = 0; i < activeTiles.Count; i++)
        {
            Tile currentTile = activeTiles[i];
            GameObject tileGO = WorldController.Instance.TileToGoMap[currentTile];
            tileGO.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            cachedTiles.Add(currentTile);
        }


        return true;
    }

    private void OnTileCreated(Tile tile)
    {
        var tileGo = new GameObject($"Tile_{tile.X}_{tile.Y}");
        tileGo.transform.SetParent(gridParent, true);
        tileGo.transform.position = new Vector3(tile.X, tile.Y, 0);
        WorldController.Instance.TileToGoMap.Add(tile, tileGo);
        SpriteRenderer tile_sr = tileGo.AddComponent<SpriteRenderer>();
        tile_sr.sprite = SpritesController.Instance.GetSpriteByName("Tile");
        tile_sr.sortingLayerName = "Tile";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
