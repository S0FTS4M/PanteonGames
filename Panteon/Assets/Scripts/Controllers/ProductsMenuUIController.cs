using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
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


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        CreateDefaultProductsPanel();

    }
    public void RegisterGetInfoEvent()
    {
        WorldController.OnTileClicked += GetInfo_OnTileClicked;

    }
    public void UnRegisterGetInfoEvent()
    {
        WorldController.OnTileClicked -= GetInfo_OnTileClicked;

    }

    private void GetInfo_OnTileClicked(Tile tile)
    {
        CreateDefaultProductsPanel();

        if (tile?.PlacedUnit != null)
        {
            if (tile.PlacedUnit is ICanProduce canProduce)
            {
                AddButtonToProductionMenu(canProduce.producible.Name, () =>
                    { Debug.Log(canProduce.producible.Name + " created"); });
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ClearProductsMenu()
    {
        for (int i = 0; i < _contentTransform.childCount; i++)
        {
            SimplePool.Despawn(_contentTransform.GetChild(i).gameObject);
        }
    }
    public void CreateDefaultProductsPanel()
    {
        ClearProductsMenu();

        //TODO : There has to be a better way of handling this. This is not right

        AddButtonToProductionMenu("Barrack", () =>
            { WorldController.Instance.SetProducible(ProducibleType.Barrack); });

        AddButtonToProductionMenu("Power Plant", () =>
            { WorldController.Instance.SetProducible(ProducibleType.PowerPlant); });

        // LayoutRebuilder.ForceRebuildLayoutImmediate(_contentTransform as RectTransform);

    }

    void AddButtonToProductionMenu(string producibleName, UnityAction function)
    {
        GameObject spawned = SimplePool.Spawn(_buttonPrefab, Vector3.zero, Quaternion.identity);
        spawned.transform.SetParent(_contentTransform);
        spawned.transform.localScale = new Vector3(1, 1, 1);
        spawned.GetComponentInChildren<TextMeshProUGUI>().text = producibleName;

        Button btn = spawned.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();

        //assign names colors and click events to the UI elements
        btn.onClick.AddListener(function);
    }
}
