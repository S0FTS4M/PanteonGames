using Assets.Scripts.Interfaces;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public static BuildController instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

    }

    public void RegisterPlaceUnitEvent()
    {
        Tile.OnTileClicked += PlaceProducible_OnTileClicked;

    }
    public void UnRegisterPlaceUnitEvent()
    {
        Tile.OnTileClicked -= PlaceProducible_OnTileClicked;

    }
    //When ever player activates place unit action we need to start listening for click events on valid tiles
    private static void PlaceProducible_OnTileClicked(Tile tile)
    {
        var world = WorldController.instance.World;
        var staticUnitBase = (StaticUnitBase)WorldController.instance.SelectedUnitForPlacing;

        var xBorders = world.CalculateXBorders(tile, staticUnitBase.XDimension);
        var yBorders = world.CalculateYBorders(tile, staticUnitBase.YDimension);

        staticUnitBase.pivotTile = WorldController.instance.World.GetTileAt(xBorders.start, yBorders.start);

        staticUnitBase.PlaceUnit(world);

        //after we set the unitBase we check if this unitBase can produce other units if it so then select a spawn point for it
        if (staticUnitBase is IProducer producer)
        {
            producer.SelectSpawnPoint(xBorders, yBorders);
        }

    }
}
