using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductsMenuUIController : MonoBehaviour
{
    public static ProductsMenuUIController Instance { get; private set; }
    [SerializeField]
    Transform _contentTransform;

    [SerializeField] private GameObject _buttonPrefab;
    //this maps the name of the producible objects to image name and type of the object 
    private Dictionary<string, UnitBase> _nameToObjectForButtons;

    private int _defaultButtonCount;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        _nameToObjectForButtons = new Dictionary<string, UnitBase>();
        //find every object inherited from StaticUnitBase and get the desired info from this types
        var createableTypes = Assembly.GetAssembly(typeof(StaticUnitBase)).GetTypes()
            .Where(
                myType =>
                    myType.IsClass &&
                    !myType.IsAbstract &&
                    myType.IsSubclassOf(
                        typeof(StaticUnitBase)));
        foreach (var type in createableTypes)
        {
            var unit = (StaticUnitBase)Activator.CreateInstance(type);
            _nameToObjectForButtons.Add(unit.Name,
                unit);

        }

        CreateDefaultProductsPanel();

    }
    public void RegisterGetInfoEvent()
    {
        Tile.OnTileClicked += GetInfo_OnTileClicked;

    }
    public void UnRegisterGetInfoEvent()
    {
        Tile.OnTileClicked -= GetInfo_OnTileClicked;

    }

    private void GetInfo_OnTileClicked(Tile tile)
    {
        ClearProductsMenu();
        //if tile has no unit on it then return
        if (tile?.PlacedUnit == null) return;
        //if tile has unit and this unit is not a producer than return
        if (!(tile.PlacedUnit is IProducer producer)) return;

        //if unit is a producer then create buttons for this unit
        var i = 0;
        foreach (var producible in producer.MoveableUnitPrototypes)
        {
            var k = i;

            AddButtonToProductionMenu(producible, () =>
                { WorldController.instance.CreateMoveable(k); });
            i++;
        }

    }

    public void ClearProductsMenu()
    {
        for (var i = _defaultButtonCount; i < _contentTransform.childCount; i++)
        {
            if (_contentTransform.GetChild(i).gameObject.activeSelf)
                SimplePool.Despawn(_contentTransform.GetChild(i).gameObject);

        }
    }
    public void CreateDefaultProductsPanel()
    {
        foreach (var nameToObjectForButton in _nameToObjectForButtons)
        {
            AddButtonToProductionMenu(nameToObjectForButton.Value, () =>
                { WorldController.instance.SetPlaceableUnit(nameToObjectForButton.Value.Type); });
            _defaultButtonCount++;
        }
    }

    void AddButtonToProductionMenu(UnitBase unit, UnityAction function)
    {
        var spawned = SimplePool.Spawn(_buttonPrefab, Vector3.zero, Quaternion.identity);

        spawned.transform.SetParent(_contentTransform);
        spawned.transform.localScale = new Vector3(1, 1, 1);
        spawned.GetComponentInChildren<TextMeshProUGUI>().text = unit.Name;

        var btn = spawned.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();

        //assign names colors and click events to the UI elements
        spawned.GetComponent<Image>().sprite = SpritesController.instance.GetSpriteByName(unit.ImageName);
        btn.onClick.AddListener(function);
    }
}
