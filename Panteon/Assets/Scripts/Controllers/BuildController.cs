using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public static BuildController Instance;

    [SerializeField]
    private Transform buildingsParent;
    public BuildingType selectedBuildingType { get; set; } = BuildingType.None;
    private Builder builder;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        builder = new Builder(new ProductFactory());
        WorldController.OnTileClicked += WorldController_OnTileClicked;
    }

    private void WorldController_OnTileClicked(Tile tile, IBuildable building)
    {
        Border xBorders = WorldController.Instance.CalculateXBorders(tile.X, tile.Y, building.XDimension);
        Border yBorders = WorldController.Instance.CalculateYBorders(tile.X, tile.Y, building.YDimension);
        for (int x = xBorders.start; x <= xBorders.end; x++)
        {
            for (int y = yBorders.start; y <= yBorders.end; y++)
            {
                Tile currentTile = WorldController.Instance.World.GetTileAt(x, y);
                currentTile.PlacedUnit = building;
            }
        }
        Debug.Log("clicked");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
