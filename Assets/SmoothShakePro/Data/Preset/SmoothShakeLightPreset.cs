using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [CreateAssetMenu(fileName = "SmoothShakeLightPreset", menuName = "Smooth Shake/Smooth Shake Light Preset", order = 5)]
    public class SmoothShakeLightPreset : ShakeBasePreset
    {
        [Header("Shake Settings")]
        [Tooltip("Settings for Intensity Shake")]
        public List<MultiFloatShaker> intensityShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for Range Shake")]
        public List<MultiFloatShaker> rangeShake = new List<MultiFloatShaker>();

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Intensity, Range }
#endif
    }
}
