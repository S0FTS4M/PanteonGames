using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ProductsMenuUIController : MonoBehaviour
{
    public static ProductsMenuUIController Instance { get; private set; }
    [SerializeField]
    Transform _contentTransform;

    [SerializeField] private GameObject _buttonPrefab;
    //this maps the name of the producible objects to image name and type of the object 
    private Dictionary<string, UnitBase> _nameToObjectForButtons;

    private int _defaultButtonCount = 0;
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
        foreach (Type type in
            Assembly.GetAssembly(typeof(StaticUnitBase)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(StaticUnitBase))))
        {
            StaticUnitBase unit = (StaticUnitBase)Activator.CreateInstance(type);
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
        if (tile?.PlacedUnit != null)
        {
            if (tile.PlacedUnit is IProducer producer)
            {

                int i = 0;
                foreach (var producible in producer.MoveableUnitPrototypes)
                {
                    int k = i;
                    AddButtonToProductionMenu(producible, () =>
                      { WorldController.Instance.CreateMoveable(k); });
                    i++;
                }
            }
        }

    }

    public void ClearProductsMenu()
    {
        for (int i = _defaultButtonCount; i < _contentTransform.childCount; i++)
        {
            SimplePool.Despawn(_contentTransform.GetChild(i).gameObject);
        }
    }
    public void CreateDefaultProductsPanel()
    {
        foreach (var nameToObjectForButton in _nameToObjectForButtons)
        {
            AddButtonToProductionMenu(nameToObjectForButton.Value, () =>
                { WorldController.Instance.SetPlaceableUnit(nameToObjectForButton.Value.Type); });
            _defaultButtonCount++;
        }
        // LayoutRebuilder.ForceRebuildLayoutImmediate(_contentTransform as RectTransform);

    }

    void AddButtonToProductionMenu(UnitBase unit, UnityAction function)
    {
        GameObject spawned = SimplePool.Spawn(_buttonPrefab, Vector3.zero, Quaternion.identity);
        spawned.transform.SetParent(_contentTransform);
        spawned.transform.localScale = new Vector3(1, 1, 1);
        spawned.GetComponentInChildren<TextMeshProUGUI>().text = unit.Name;

        spawned.GetComponent<Image>().sprite = SpritesController.Instance.GetSpriteByName(unit.ImageName);

        Button btn = spawned.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();

        //assign names colors and click events to the UI elements
        btn.onClick.AddListener(function);
    }
}
