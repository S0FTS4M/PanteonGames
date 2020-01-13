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
    [SerializeField] private GameObject _producibleGO;
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
        Tile.OnTileClicked += GetInfo_OnTileClicked;

    }
    public void UnRegisterGetInfoEvent()
    {
        Tile.OnTileClicked -= GetInfo_OnTileClicked;

    }
    private void GetInfo_OnTileClicked(Tile tile)
    {
        Transform produciblesTransform = ProducibleUnitPanel.transform.GetChild(1).transform;

        for (int i = 0; i < produciblesTransform.childCount; i++)
        {
            SimplePool.Despawn(produciblesTransform.GetChild(i).gameObject);
        }

        if (tile?.PlacedUnit != null)
        {
            InfoPanel.SetActive(true);
            unitNameTxt.text = tile.PlacedUnit.Name;

            unitImage.sprite = (tile.PlacedUnit is MoveableUnitBase) == false ?
                BuildingsSpriteController.Instance.GetBuildingSpriteByName(tile.PlacedUnit.ImageName) :
                MoveableUnitSpriteController.Instance.GetMoveableSpriteByName(tile.PlacedUnit.ImageName);



            if (tile.PlacedUnit is IProducer canProduce)
            {
                ProducibleUnitPanel.SetActive(true);


                //for every unitBase in the list create a image object
                foreach (var producible in canProduce.MoveableUnitPrototypes)
                {

                    ProducibleUnitPanel.GetComponentInChildren<TextMeshProUGUI>().text = producible.Name;
                    GameObject spawn = SimplePool.Spawn(_producibleGO, Vector3.zero, Quaternion.identity);
                    spawn.transform.SetParent(produciblesTransform);

                    spawn.transform.localScale = new Vector3(1, 1, 1);

                    spawn.GetComponent<Image>().sprite = MoveableUnitSpriteController.Instance.GetMoveableSpriteByName(producible.ImageName);

                }

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
