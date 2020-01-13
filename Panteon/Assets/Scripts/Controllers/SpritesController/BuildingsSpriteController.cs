using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BuildingsSpriteController : MonoBehaviour
{

    //for every static unit we create needs to be a child of an object 
    [SerializeField]
    private Transform buildingsParent;
    World world
    {
        get { return WorldController.Instance.World; }
    }
    // Start is called before the first frame update
    void Start()
    {
        StaticUnitBase.OnUnitPlace += OnBuildingCreated;
    }

    public void OnBuildingCreated(UnitBase unit)
    {
        GameObject producibleGo = new GameObject(unit.Name);
        producibleGo.transform.SetParent(buildingsParent);
        producibleGo.transform.position = new Vector3(unit.PivotTile.X, unit.PivotTile.Y, 0);

        SpriteRenderer sr = ((GameObject)producibleGo).AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Buildings";
        sr.sprite = SpritesController.Instance.GetSpriteByName(unit.ImageName);



    }



}
