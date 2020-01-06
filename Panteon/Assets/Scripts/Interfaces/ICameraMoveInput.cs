using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICameraMoveInput
    {
        /// <summary>
        /// mouse position difference between  frames
        /// </summary>
        Vector3 MousePosDifference { get; }
        /// <summary>
        /// Handles click and drag states
        /// </summary>
        bool Dragging { get; }

    }
}
