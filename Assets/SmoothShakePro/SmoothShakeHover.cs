using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Hover")]
    public class SmoothShakeHover : MonoBehaviour
    {
        [Tooltip("Shake settings when the mouse enters the object.")]
        public ShakeBase onMouseEnter;
        [Tooltip("Shake settings when the mouse exits the object.")]
        public ShakeBase onMouseExit;
        [Tooltip("Shake settings when the mouse clicks the object.")]
        public ShakeBase onMouseClick;

        private void OnMouseEnter()
        {
            if (onMouseEnter != null) onMouseEnter.StartShake();
        }

        private void OnMouseExit()
        {
            if (onMouseEnter != null) onMouseEnter.StopShake();
            if (onMouseExit != null) onMouseExit.StartShake();
        }

        private void OnMouseDown()
        {
            if (onMouseClick != null) onMouseClick.StartShake();
        }
    }
}
