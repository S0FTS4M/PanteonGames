using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldInputController : MonoBehaviour, IWorldInput
{
    public int MouseX { get; private set; }

    public int MouseY { get; private set; }

    public bool IsClicked { get; private set; }

    //onmousehover -> üzerinde olduğumuz tile hakkında bilgi sahibi olursak 
    //bir şey yerleştirmek istediğimiz zaman bu tile çevresindeki tüm tile nesneleri kontrol edilebilir

    //onmouseclick -> iki işlemden sorumlu 
    //1) Tıklanan tile bir nesne içeriyor ise buna ait info gösterilecek ve production menu güncellenecek
    //2) Eğer yerleştirilmek için bir nesne seçilmiş ise yerleştirme işlemi yapılacak 
    private void Start()
    {

    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseX = Mathf.FloorToInt(mousePos.x);
        MouseY = Mathf.FloorToInt(mousePos.y);


        //we only need to catch it one time
        IsClicked = false;
        if (Input.GetMouseButtonDown(0))
        {
            IsClicked = true;
        }
    }
}
