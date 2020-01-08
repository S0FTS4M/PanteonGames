using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationUIController : MonoBehaviour
{
    public static InformationUIController Instance { get; private set; }

    [SerializeField] private GameObject InfoPanel;
    [SerializeField] private TextMeshProUGUI unitNameTxt;
    [SerializeField] private Image unitImage;

    [SerializeField] private GameObject ProducibleUnitPanel;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
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
        if (tile?.PlacedUnit != null)
        {
            InfoPanel.SetActive(true);
            unitNameTxt.text = tile.PlacedUnit.Name;
            unitImage.sprite = SpritesController.Instance.UnitNameToSpriteMap[tile.PlacedUnit.ImageName];
            if (tile.PlacedUnit is ICanProduce canProduce)
            {
                ProducibleUnitPanel.SetActive(true);
                ProducibleUnitPanel.GetComponentInChildren<TextMeshProUGUI>().text = canProduce.producible.Name;
                ProducibleUnitPanel.transform.GetChild(1).GetComponent<Image>().sprite =
                    SpritesController.Instance.UnitNameToSpriteMap[canProduce.producible.ImageName];


            }
            else
            {
                ProducibleUnitPanel.SetActive(false);
            }

        }
        else
        {
            InfoPanel.SetActive(false);
            ProducibleUnitPanel.SetActive(false);
        }
    }

}
