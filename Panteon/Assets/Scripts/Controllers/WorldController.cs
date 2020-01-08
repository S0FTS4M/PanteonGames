using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    /// <summary>
    /// when a tile gameobject gets created we want to set a sprite to it. we notify the class
    /// that responsible for sprites
    /// </summary>
    public static event Action<Tile> OnTileGoCreated;
    /// <summary>
    /// this will be working only when ever user wants to put a building to the map.
    /// this will show if it is possible to put buildings to a range of tiles
    /// </summary>
    public static event Func<Tile, bool> OnHoverTile;
    public static event Action<Tile> OnTileClicked;

    public static WorldController Instance;
    [SerializeField]
    public ProducibleType producibleType;

    public IProducible selectedProducible { get; private set; }
    bool? _canPlaceBuilding;
    IWorldInput _worldInput;

    [SerializeField]
    Transform worldParent;

    public Dictionary<Tile, GameObject> TileToGoMap { get; private set; }
    public World World { get; private set; }

    Tile _currentTile;


    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        World = new World();
        TileToGoMap = new Dictionary<Tile, GameObject>();
        //create a gameobject for every tile data and set the position.
        for (var x = 0; x < World.Width; x++)
        {
            for (var y = 0; y < World.Height; y++)
            {
                var tile = World.GetTileAt(x, y);
                var tileGo = new GameObject($"tile_{x}_{y}");
                tileGo.transform.SetParent(worldParent, true);
                tileGo.transform.position = new Vector3(x, y, 0);

                TileToGoMap.Add(tile, tileGo);

                OnTileGoCreated?.Invoke(tile);
            }

        }
        _worldInput = GetComponent<IWorldInput>();
        SetProducible(ProducibleType.None);
    }



    public void SetProducible(ProducibleType producibleType)
    {
        ///TODO : Bu selectedbuildable konusunda tekrar düşünmem lazım product menuden sadece building bilgisi gönderilmeyecek
        /// aynı zamanda pruducible bilgisi de gelecek

        if (producibleType == ProducibleType.None)
        {
            BuildController.Instance.UnRegisterPlaceProducibleEvent();
            InformationUIController.Instance.RegisterGetInfoEvent();
            ProductsMenuUIController.Instance.RegisterGetInfoEvent();
            selectedProducible = null;
            return;
        }
        BuildController.Instance.RegisterPlaceProducibleEvent();
        InformationUIController.Instance.UnRegisterGetInfoEvent();
        ProductsMenuUIController.Instance.UnRegisterGetInfoEvent();
        selectedProducible = Factory.GetFactoryOfType<ProductFactory>().Create(producibleType);

    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"{worldInput.MouseX}, {worldInput.MouseY}");
        _currentTile = World.GetTileAt(_worldInput.MouseX, _worldInput.MouseY);

        if (_worldInput.IsClicked == true)
        {
            //this means we are trying to place a building.
            if (selectedProducible != null && _canPlaceBuilding != null && _canPlaceBuilding == true)
            {
                OnTileClicked?.Invoke(_currentTile);
                SetProducible(ProducibleType.None);
            }
            else if (selectedProducible == null)
            {
                //otherwise (selectedProducible == null) we are getting info
                OnTileClicked?.Invoke(_currentTile);
            }


        }
        if (selectedProducible != null)
            _canPlaceBuilding = OnHoverTile?.Invoke(_currentTile);
    }
    #region TileFuncs
    /// <summary>
    /// Yerleştirilebilir nesneler için alan seçiminde üzerinde bulunduğumuz tile base alınarak
    /// x sınırlarını belirler
    /// TODO : bu çok anlaşılır değil gibi daha iyi açıklama ve metod ismi bulmak lazım
    /// </summary>
    /// <param name="xStart"> Üzerinde bulunduğumuz tile.X bilgisi </param>
    /// <param name="yStart"> Üzerinde bulunduğumuz tile.Y bilgisi </param>
    /// <param name="xLength"> Seçtiğimiz yerleştirilebilir nesnenin X uzunluğu</param>
    /// <returns></returns>
    public Border CalculateXBorders(int xStart, int yStart, int xLength)
    {
        //X ekseninde başlangıç ve bitiş noktasını hesaplayacağımız için bu değişkenleri tanımlıyoruz ve
        //başlangıç ve bitiş olarak üzerinde bulunduğumuz tile bilgilerini veriyoruz
        int startX = xStart, endX = xStart;
        //Her iterasyonda bulunduğumuz tiledan radius kadar uzaklığa bakıyoruz. 
        int radius = 1;


        Tile tile = null;

        for (int x = 1; x < xLength;)
        {
            //pozitif yön kontrolü
            tile = WorldController.Instance.World.GetTileAt(xStart + radius, yStart);
            if (tile != null)
            {
                endX = xStart + radius;
                x++;
                //x arttıktan sonra ulaşmakistediğimiz uzunluğu kontrol ediyoruz.
                //kontrollü artırım yapıyoruz
                if (x >= xLength)
                    continue;

            }

            tile = WorldController.Instance.World.GetTileAt(xStart - radius, yStart);
            if (tile != null)
            {
                startX = xStart - radius;
                x++;
            }

            radius++;

        }
        return new Border() { start = startX, end = endX };
    }
    /// <summary>
    /// Yerleştirilebilir nesneler için alan seçiminde üzerinde bulunduğumuz tile base alınarak
    /// y sınırlarını belirler
    /// TODO : bu çok anlaşılır değil gibi daha iyi açıklama ve metod ismi bulmak lazım
    /// </summary>
    /// <param name="xStart"> Üzerinde bulunduğumuz tile.X bilgisi </param>
    /// <param name="yStart"> Üzerinde bulunduğumuz tile.Y bilgisi </param>
    /// <param name="yLength"> Seçtiğimiz yerleştirilebilir nesnenin Y uzunluğu</param>
    /// <returns></returns>
    public Border CalculateYBorders(int xStart, int yStart, int yLength)
    {
        //Y ekseninde başlangıç ve bitiş noktasını hesaplayacağımız için bu değişkenleri tanımlıyoruz ve
        //başlangıç ve bitiş olarak üzerinde bulunduğumuz tile bilgilerini veriyoruz
        int startY = yStart, endY = yStart;
        //Her iterasyonda bulunduğumuz tiledan radius kadar uzaklığa bakıyoruz. 
        int radius = 1;

        Tile tile = null;

        for (int y = 1; y < yLength;)
        {

            //pozitif yön kontrolü
            tile = WorldController.Instance.World.GetTileAt(xStart, yStart + radius);
            if (tile != null)
            {
                endY = yStart + radius;
                //eğer uygun alan var ise Y uzunluğumuz arttı
                y++;

                //y arttıktan sonra ulaşmakistediğimiz uzunluğu kontrol ediyoruz.
                //kontrollü artırım yapıyoruz
                if (y >= yLength)
                    continue;

            }

            //negatif yön kontrolü
            tile = WorldController.Instance.World.GetTileAt(xStart, yStart - radius);
            if (tile != null)
            {
                startY = yStart - radius;
                y++;


            }

            radius++;

        }
        return new Border() { start = startY, end = endY };
    }

    public List<Tile> GetTilesInSelectedArea(Border xBorder, Border yBorder)
    {
        List<Tile> tilesAvaliable = new List<Tile>();
        for (int x = xBorder.start; x <= xBorder.end; x++)
        {
            for (int y = yBorder.start; y <= yBorder.end; y++)
            {
                Tile currentTile = WorldController.Instance.World.GetTileAt(x, y);
                tilesAvaliable.Add(currentTile);
            }
        }

        return tilesAvaliable;
    }
    #endregion
}
