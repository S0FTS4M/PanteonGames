using UnityEngine;

public class BuildingsSpriteController : MonoBehaviour
{

    //for every static unit we create needs to be a child of an object 
    [SerializeField]
    private Transform _buildingsParent;

    // Start is called before the first frame update
    void Start()
    {
        StaticUnitBase.OnUnitPlace += OnBuildingCreated;
    }

    public void OnBuildingCreated(UnitBase unit)
    {
        var producibleGo = new GameObject(unit.Name);
        producibleGo.transform.SetParent(_buildingsParent);
        producibleGo.transform.position = new Vector3(unit.pivotTile.X, unit.pivotTile.Y, 0);

        var sr = ((GameObject)producibleGo).AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Buildings";
        sr.sprite = SpritesController.instance.GetSpriteByName(unit.ImageName);
    }



}
