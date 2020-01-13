using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public static BuildController Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

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
    private void PlaceProducible_OnTileClicked(Tile tile)
    {
        World world = WorldController.Instance.World;
        StaticUnitBase staticUnitBase = (StaticUnitBase)WorldController.Instance.SelectedUnitForPlacing;

        Border xBorders = world.CalculateXBorders(tile, staticUnitBase.XDimension);
        Border yBorders = world.CalculateYBorders(tile, staticUnitBase.YDimension);

        staticUnitBase.PivotTile = WorldController.Instance.World.GetTileAt(xBorders.start, yBorders.start);

        staticUnitBase.PlaceUnit(world);

        //after we set the unitBase we check if this unitBase can produce other units if it so then select a spawn point for it
        if (staticUnitBase is IProducer producer)
        {
            producer.SelectSpawnPoint(xBorders, yBorders);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
