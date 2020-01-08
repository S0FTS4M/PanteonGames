using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;


public class SpritesController : MonoBehaviour
{
    public static SpritesController Instance;

    [SerializeField]
    Sprite tileSprite;

    int say = 0;
    Tile oldHoveredTile;
    List<Tile> cachedTiles = new List<Tile>();
    public Dictionary<string, Sprite> UnitNameToSpriteMap { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;

        }

        Instance = this;
        UnitNameToSpriteMap = new Dictionary<string, Sprite>();
        WorldController.OnTileGoCreated += OnTileGOCreated;
        WorldController.OnHoverTile += WorldController_OnHoverTile;
        LoadSprites();
        BuildController.OnProducibleGOCreated += OnProducibleGOCreated;

    }

    private void OnProducibleGOCreated(IProducible producible, object producibleGo)
    {
        SpriteRenderer sp = ((GameObject)producibleGo).AddComponent<SpriteRenderer>();
        sp.sortingOrder = 10;
        sp.sprite = SpritesController.Instance.UnitNameToSpriteMap[producible.ImageName];

    }

    void LoadSprites()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Buildings");
        for (int i = 0; i < sprites.Length; i++)
        {
            UnitNameToSpriteMap.Add(
                sprites[i].name,
                sprites[i]);
        }
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
        IProducible producible = WorldController.Instance.selectedProducible;
        ResetTiles();

        if (tile == null)
            return false;

        Border xBorders = WorldController.Instance.CalculateXBorders(tile.X, tile.Y, producible.XDimension);

        Border yBorders = WorldController.Instance.CalculateYBorders(tile.X, tile.Y, producible.YDimension);



        // Debug.Log(xBorders + "____" + yBorders);
        //eğer yerleştirmeye çalıştığımız alan dolu ise kırmızı indicator ile bu alanın yasaklı olduğunu bileceğiz
        bool _isOccupied = false;
        List<Tile> activeTiles = WorldController.Instance.GetTilesInSelectedArea(xBorders, yBorders);
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

    private void OnTileGOCreated(Tile tile)
    {
        GameObject tileGO = WorldController.Instance.TileToGoMap[tile];
        SpriteRenderer tile_sr = tileGO.AddComponent<SpriteRenderer>();
        tile_sr.sprite = tileSprite;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
