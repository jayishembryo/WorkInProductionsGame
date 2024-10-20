using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
#if UNITY_XR
    [CreateAssetMenu(fileName = "SmoothShakeHapticsXRPreset", menuName = "Smooth Shake/Smooth Shake Haptics XR Preset")]
    public class SmoothShakeHapticsXRPreset : ShakeBasePreset
    {
        [Tooltip("Settings for left controller")]
        public List<MultiFloatShaker> leftControllerShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for right controller")]
        public List<MultiFloatShaker> rightControllerShake = new List<MultiFloatShaker>();

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { leftController, rightController };
#endif
    }
#endif
}
