using UnityEngine;

namespace SmoothShakePro
{
    public abstract class ShakeBasePreset : ScriptableObject
    {
        [Header("Time Settings")]
        [Tooltip("Settings for the shake timing")]
        public TimeSettings timeSettings;

#if UNITY_EDITOR
        [HideInInspector] internal AxisToPreview axisToPreview;
#endif
    }
}
