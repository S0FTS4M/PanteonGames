using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LetterboxCamera
{
    /* ForceCameraRatio.cs
     *
     * Forces the assigned Cameras to a given Aspect Ratio by Letterboxing them horizontally or vertically
     *
     * Copyright Hexdragonal Games 2015
     * Written by Tom Elliott */

    // A class for tracking individual Cameras and their Viewports
    [System.Serializable]
    public class CameraRatio
    {
        public enum CameraAnchor
        {
            Center,
            Top,
            Bottom,
            Left,
            Right,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        [Tooltip("The Camera assigned to have an automatically calculated Viewport Ratio")]
        public Camera camera;
        [Tooltip("When a Camera Viewport is shrunk to fit a ratio, it will anchor the new Viewport Rectangle at the given point (relative to the original, unshrunk Viewport)")]
        public CameraAnchor anchor = CameraAnchor.Center;

        [HideInInspector]
        public Vector2 vectorAnchor;
        private Rect _originViewPort;

        public CameraRatio(Camera camera, Vector2 anchor)
        {
            this.camera = camera;
            vectorAnchor = anchor;
            _originViewPort = this.camera.rect;
        }

        /// <summary>
        /// Sets the Camera's current Viewport as the viewport measurements to fill on resizing
        /// </summary>
        public void ResetOriginViewport()
        {
            _originViewPort = camera.rect;
            SetAnchorBasedOnEnum(anchor);
        }

        /// <summary>
        /// Sets the Anchor for this Camera when it is resized based on a given enum description
        /// </summary>
        /// <param name="_anchor"></param>
        public void SetAnchorBasedOnEnum(CameraAnchor _anchor)
        {
            switch (_anchor)
            {
                case CameraAnchor.Center:
                    vectorAnchor = new Vector2(0.5f, 0.5f);
                    break;
                case CameraAnchor.Top:
                    vectorAnchor = new Vector2(0.5f, 1f);
                    break;
                case CameraAnchor.Bottom:
                    vectorAnchor = new Vector2(0.5f, 0f);
                    break;
                case CameraAnchor.Left:
                    vectorAnchor = new Vector2(0f, 0.5f);
                    break;
                case CameraAnchor.Right:
                    vectorAnchor = new Vector2(1f, 0.5f);
                    break;
                case CameraAnchor.TopLeft:
                    vectorAnchor = new Vector2(0f, 1f);
                    break;
                case CameraAnchor.TopRight:
                    vectorAnchor = new Vector2(1f, 1f);
                    break;
                case CameraAnchor.BottomLeft:
                    vectorAnchor = new Vector2(0f, 0f);
                    break;
                case CameraAnchor.BottomRight:
                    vectorAnchor = new Vector2(1f, 0f);
                    break;
            }
        }

        /// <summary>
        /// Forces a camera to render at a given ratio
        /// Creates a letter box effect if the new viewport does not match the current Window ratio
        /// </summary>
        /// <param name="_targetAspect"></param>
        /// <param name="_currentAspect"></param>
        public void CalculateAndSetCameraRatio(float _width, float _height, bool _horizontalLetterbox)
        {

            Rect localViewPort = new Rect();

            // Force the viewport to a width and height accurate to the target ratio
            if (_horizontalLetterbox)
            { // current aspect is wider than target aspect so shorten down height of the viewport
                localViewPort.height = _height;
                localViewPort.width = 1;

            }
            else
            { // current aspect is taller than target aspect so thin down width of the viewport
                localViewPort.height = 1f;
                localViewPort.width = _width;
            }

            // Resize and position the viewport to fit in it's original position on screen (adhering to a given anchor point)
            Rect screenViewPortHorizontal = new Rect();
            Rect screenViewPortVertical = new Rect();

            // Calculate both a horizontally and vertically resized viewport
            screenViewPortHorizontal.width = _originViewPort.width;
            screenViewPortHorizontal.height = _originViewPort.width * (localViewPort.height / localViewPort.width);
            screenViewPortHorizontal.x = _originViewPort.x;
            screenViewPortHorizontal.y = Mathf.Lerp(_originViewPort.y, _originViewPort.y + (_originViewPort.height - screenViewPortHorizontal.height), vectorAnchor.y);

            screenViewPortVertical.width = _originViewPort.height * (localViewPort.width / localViewPort.height);
            screenViewPortVertical.height = _originViewPort.height;
            screenViewPortVertical.x = Mathf.Lerp(_originViewPort.x, _originViewPort.x + (_originViewPort.width - screenViewPortVertical.width), vectorAnchor.x);
            screenViewPortVertical.y = _originViewPort.y;

            // Use the best fitting of the two
            if (screenViewPortHorizontal.height >= screenViewPortVertical.height && screenViewPortHorizontal.width >= screenViewPortVertical.width)
            {
                if (screenViewPortHorizontal.height <= _originViewPort.height && screenViewPortHorizontal.width <= _originViewPort.width)
                {
                    camera.rect = screenViewPortHorizontal;
                }
                else
                {
                    camera.rect = screenViewPortVertical;
                }
            }
            else
            {
                if (screenViewPortVertical.height <= _originViewPort.height && screenViewPortVertical.width <= _originViewPort.width)
                {
                    camera.rect = screenViewPortVertical;
                }
                else
                {
                    camera.rect = screenViewPortHorizontal;
                }
            }
        }
    }

    // A class for tracking all cameras in a scene
    [System.Serializable]
    public class ForceCameraRatio : MonoBehaviour
    {
        public Vector2 ratio = new Vector2(16, 9);
        public bool forceRatioOnAwake = true;
        public bool listenForWindowChanges = true;
        public bool createCameraForLetterBoxRendering = true;
        public bool findCamerasAutomatically = true;
        public Color letterBoxCameraColor = new Color(0, 0, 0, 1);

        public List<CameraRatio> cameras;

        public Camera letterBoxCamera;

        private void Start()
        {
            // If no cameras have been assigned in editor, search for cameras in the scene
            if (findCamerasAutomatically)
            {
                FindAllCamerasInScene();
            }
            else if (cameras == null || cameras.Count == 0)
            {
                cameras = new List<CameraRatio>();
            }

            ValidateCameraArray();

            // Set the origin viewport space for each Camera
            foreach (var cams in cameras)
            {
                cams.ResetOriginViewport();
            }

            // If requested, a Camera will be generated that renders a letter box Color
            if (createCameraForLetterBoxRendering)
            {
                letterBoxCamera = new GameObject().AddComponent<Camera>();
                letterBoxCamera.backgroundColor = letterBoxCameraColor;
                letterBoxCamera.cullingMask = 0;
                letterBoxCamera.depth = -100;
                letterBoxCamera.farClipPlane = 1;
                letterBoxCamera.useOcclusionCulling = false;
                letterBoxCamera.allowHDR = false;
                letterBoxCamera.clearFlags = CameraClearFlags.Color;
                letterBoxCamera.name = "Letter Box Camera";

                foreach (var cams in cameras.Where(cams => cams.camera.depth == -100))
                {
                    Debug.LogError(cams.camera.name + " has a depth of -100 and may conflict with the Letter Box Camera in Forced Camera Ratio!");
                }
            }

            if (forceRatioOnAwake)
            {
                CalculateAndSetAllCameraRatios();
            }
        }

        private void Update()
        {
            if (listenForWindowChanges)
            {
                // Recalculate the viewport size if the window size has changed
                CalculateAndSetAllCameraRatios();
                if (letterBoxCamera != null)
                {
                    letterBoxCamera.backgroundColor = letterBoxCameraColor;
                }
            }
        }

        /// <summary>
        /// Returns the container class for a Camera and it's ratio by the _camera it contains
        /// Returns null if the given _camera is not being tracked
        /// </summary>
        /// <param name="_camera"></param>
        /// <returns></returns>
        private CameraRatio GetCameraRatioByCamera(Camera _camera)
        {
            return cameras?.FirstOrDefault(cams => cams != null && cams.camera == _camera);
        }

        /// <summary>
        /// Removes any null elements from the CameraRatio Array
        /// </summary>
        private void ValidateCameraArray()
        {
            for (var i = cameras.Count - 1; i >= 0; i--)
            {
                if (cameras[i].camera == null)
                {
                    cameras.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Populates the tracked Camera Array with every Camera currently in the scene
        /// </summary>
        public void FindAllCamerasInScene()
        {
            var allCameras = FindObjectsOfType<Camera>();
            cameras = new List<CameraRatio>();

            foreach (var cams in allCameras)
            {
                if ((createCameraForLetterBoxRendering || cams != letterBoxCamera))
                { // Ignore the Custom LetterBox Camera
                    cameras.Add(new CameraRatio(cams, new Vector2(0.5f, 0.5f)));
                }
            }
        }

        /// <summary>
        /// Loops through all cameras in scene (or that have been set in editor)
        /// Forces each camera to render at a given ratio
        /// Creates a letter box effect if the new viewport does not match the current Window ratio
        /// </summary>
        public void CalculateAndSetAllCameraRatios()
        {
            var targetAspect = ratio.x / ratio.y;
            var currentAspect = ((float)Screen.width) / ((float)Screen.height);

            var horizontalLetterbox = false;
            var fullWidth = targetAspect / currentAspect;
            var fullHeight = currentAspect / targetAspect;

            if (currentAspect > targetAspect)
            {
                horizontalLetterbox = false;
            }

            foreach (var cam in cameras)
            {
                cam.SetAnchorBasedOnEnum(cam.anchor);
                cam.CalculateAndSetCameraRatio(fullWidth, fullHeight, horizontalLetterbox);
            }
        }

        /// <summary>
        /// Set the anchor for a given Camera
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_anchor"></param>
        public void SetCameraAnchor(Camera _camera, Vector2 _anchor)
        {
            var cameraRatio = GetCameraRatioByCamera(_camera);
            if (cameraRatio != null)
            {
                cameraRatio.vectorAnchor = _anchor;
            }
        }

        public CameraRatio[] GetCameras()
        {
            if (cameras == null)
            {
                cameras = new List<CameraRatio>();
            }
            return cameras.ToArray();
        }
    }
}