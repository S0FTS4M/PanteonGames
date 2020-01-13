using Assets.Scripts.Interfaces;
using UnityEngine;

public class CameraInputController : MonoBehaviour, ICameraMoveInput
{

    public Vector3 MousePosDifference { get; private set; }

    public bool Dragging { get; private set; }

    private Vector3 _currentFramePosition;
    private Vector3 _lastFramePosition;

    // Update is called once per frame
    void Update()
    {
        _currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _currentFramePosition.z = 0;

        UpdateCameraMovement();

        // Save the mouse position from this frame
        // We don't use currFramePosition because we may have moved the camera.
        _lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lastFramePosition.z = 0;
    }

    private void UpdateCameraMovement()
    {

        if (Input.GetMouseButton(1))
        {   //  Left Mouse Button
            var diff = _lastFramePosition - _currentFramePosition;
            Camera.main.transform.Translate(diff);
            MousePosDifference = diff;


            Dragging = true;
        }
        if (Input.GetMouseButtonUp(1))
        {   //  Left Mouse Button up
            Dragging = false;
        }

    }
}
