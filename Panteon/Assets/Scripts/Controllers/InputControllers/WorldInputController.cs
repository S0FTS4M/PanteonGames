using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldInputController : MonoBehaviour, IWorldInput
{
    public int MouseX { get; private set; }

    public int MouseY { get; private set; }

    public bool IsLeftClicked { get; private set; }
    public bool IsRightClicked { get; private set; }

    private bool _isDragging;
    //mouse position when the player right clicks
    private Vector2 _firstMousePos;
    //mouse position while dragging
    private Vector2 _secondMousePos;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            MouseX = -1;
            MouseY = -1;
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseX = Mathf.FloorToInt(mousePos.x);
        MouseY = Mathf.FloorToInt(mousePos.y);

        //we only need to catch it one time
        IsLeftClicked = IsRightClicked = false;

        if (Input.GetMouseButtonDown(0))
        {
            IsLeftClicked = true;
        }
        //checking for right click if we are dragging then do not respond as right click
        else if (Input.GetMouseButtonUp(1) && _isDragging == false)
        {
            IsRightClicked = true;
        }
        CheckForDragging();

    }
    /// <summary>
    /// checks if we drag the mouse
    /// </summary>
    void CheckForDragging()
    {
        //if player release the mouse then set dragging flag to false
        if (Input.GetMouseButtonUp(1))
        {
            _isDragging = false;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //get the position of the mouse when player presses it
            _firstMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {

            //while mouse button click is continues then calculate the distance and if ever goes above zero
            //this means we dragged
            _secondMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dragLength = Vector2.Distance(_firstMousePos, _secondMousePos);
            if (dragLength > 0.1f)
                _isDragging = true;
        }
    }

}
