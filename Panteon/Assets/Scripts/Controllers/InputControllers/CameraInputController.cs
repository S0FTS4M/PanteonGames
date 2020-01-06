using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class CameraInputController : MonoBehaviour, ICameraMoveInput
{

    public Vector3 MousePosDifference { get; private set; }

    public bool Dragging { get; private set; }

    private Vector3 currFramePosition;
    private Vector3 lastFramePosition;
    bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;


        UpdateCameraMovement();

        // Save the mouse position from this frame
        // We don't use currFramePosition because we may have moved the camera.
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    private void UpdateCameraMovement()
    {

        if (Input.GetMouseButton(1))
        {   //  Left Mouse Button

            Vector3 diff = lastFramePosition - currFramePosition;
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
