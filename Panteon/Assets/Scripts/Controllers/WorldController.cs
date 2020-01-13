using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance;
    public List<MoveableUnitBase> moveables;
    public UnitBase SelectedUnitForPlacing { get; private set; }
    public Tile SelectedTileForInfo { get; private set; }

    bool? _canPlaceUnit;
    IWorldInput _worldInput;

    public Dictionary<Tile, GameObject> TileToGoMap { get; private set; }
    public World World { get; private set; }

    Tile _currentHoveredTile;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        TileToGoMap = new Dictionary<Tile, GameObject>();
        moveables = new List<MoveableUnitBase>();
        //create a gameobject for every tile data and set the position.

        _worldInput = GetComponent<IWorldInput>();
    }

    void Start()
    {
        World = new World();
        SetPlaceableUnit(UnitType.None);
    }
    public void CreateMoveable(int index)
    {
        //this method will be called only when a tile selected and it has a producer unit on it
        MoveableUnitBase unit = (MoveableUnitBase)(((IProducer)SelectedTileForInfo.PlacedUnit).Produce(index));
        moveables.Add(unit);
    }
    /// <summary>
    /// we set the SelectedUnitForPlacing based on unit type and creates a unit from factory
    /// </summary>
    /// <param name="unitType">Type of the unit</param>
    public void SetPlaceableUnit(UnitType unitType)
    {
        //we do things if unitType is different. If player spams same type, we do nothing
        if (SelectedUnitForPlacing?.Type != unitType)
        {
            //Unregister all events so we dont need to consider every time what to unregister
            //and what to register
            BuildController.Instance.UnRegisterPlaceUnitEvent();
            InformationUIController.Instance.UnRegisterGetInfoEvent();
            ProductsMenuUIController.Instance.UnRegisterGetInfoEvent();

            if (unitType != UnitType.None)
            {
                //if UnitType is not None we need t UnRegister get info on click events 
                //and register PlaceUnitEvent
                BuildController.Instance.RegisterPlaceUnitEvent();
                SelectedUnitForPlacing = Factory.GetFactoryOfType<ProductFactory>().Create(unitType);
                //we changed the world we added some new unwalkable places so update tile graphs
                World.ReCalculatePaths();
            }
            else
            {
                //if UnitType is None then we register get info events
                InformationUIController.Instance.RegisterGetInfoEvent();
                ProductsMenuUIController.Instance.RegisterGetInfoEvent();
                SelectedUnitForPlacing = null;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        _currentHoveredTile = World.GetTileAt(_worldInput.MouseX, _worldInput.MouseY);

        if (_worldInput.IsLeftClicked == true)
        {
            //this means we are trying to place a building.
            if (SelectedUnitForPlacing != null && _canPlaceUnit != null && _canPlaceUnit == true)
            {
                _currentHoveredTile?.TileClick();
                SetPlaceableUnit(UnitType.None);
            }
            else if (SelectedUnitForPlacing == null)
            {
                //otherwise (SelectedUnitForPlacing == null) we are getting info
                _currentHoveredTile?.TileClick();
                SelectedTileForInfo = _currentHoveredTile;
            }
        }
        //if player right clicks and the selected unit is a moveable unit then set a destination for that unit
        if (_worldInput.IsRightClicked == true)
        {
            if (SelectedTileForInfo?.PlacedUnit is MoveableUnitBase moveable)
            {

                moveable.SetDestination(_currentHoveredTile);
                SelectedTileForInfo = _currentHoveredTile;
            }
        }

        if (SelectedUnitForPlacing != null)
            _canPlaceUnit = _currentHoveredTile?.TileHover();

        foreach (var moveable in moveables)
        {
            moveable.Update(Time.deltaTime);
        }
    }

}
