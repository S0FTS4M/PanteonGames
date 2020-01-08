using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public static BuildController Instance;
    public static event Action<IProducible, object> OnProducibleGOCreated = delegate { };

    [SerializeField]
    private Transform buildingsParent;
    public ProducibleType SelectedProducibleType { get; set; } = ProducibleType.None;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;


    }

    public void RegisterPlaceProducibleEvent()
    {
        WorldController.OnTileClicked += PlaceProducible_OnTileClicked;

    }
    public void UnRegisterPlaceProducibleEvent()
    {
        WorldController.OnTileClicked -= PlaceProducible_OnTileClicked;

    }

    private void PlaceProducible_OnTileClicked(Tile tile)
    {
        IProducible producible = WorldController.Instance.selectedProducible;
        Border xBorders = WorldController.Instance.CalculateXBorders(tile.X, tile.Y, producible.XDimension);
        Border yBorders = WorldController.Instance.CalculateYBorders(tile.X, tile.Y, producible.YDimension);
        for (int x = xBorders.start; x <= xBorders.end; x++)
        {
            for (int y = yBorders.start; y <= yBorders.end; y++)
            {
                Tile currentTile = WorldController.Instance.World.GetTileAt(x, y);
                currentTile.PlacedUnit = producible;
            }
        }
        GameObject producibleGo = new GameObject(producible.Name);
        producibleGo.transform.SetParent(buildingsParent);
        producibleGo.transform.position = new Vector3(xBorders.start, yBorders.start, 0);

        OnProducibleGOCreated?.Invoke(producible, producibleGo);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
