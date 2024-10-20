using UnityEngine;
using UnityEngine.EventSystems;

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Pointer")]
    public class SmoothShakePointer : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
    {
        [Tooltip("Shake settings when the mouse enters the object.")]
        public ShakeBase onPointerEnter;
        [Tooltip("Shake settings when the mouse exits the object.")]
        public ShakeBase onPointerExit;
        [Tooltip("Shake settings when the mouse clicks the object.")]
        public ShakeBase onPointerClick;

        public void OnPointerExit(PointerEventData eventData)
        {
            if(onPointerEnter) onPointerEnter.StopShake();
            if(onPointerExit) onPointerExit.StartShake();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(onPointerEnter) onPointerEnter.StartShake();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(onPointerClick) onPointerClick.StartShake();
        }
    }
}
