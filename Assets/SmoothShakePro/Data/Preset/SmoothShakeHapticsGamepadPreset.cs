using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [CreateAssetMenu(fileName = "SmoothShakeHapticsPreset", menuName = "Smooth Shake/Smooth Shake Haptics Gamepad Preset")]
    public class SmoothShakeHapticsGamepadPreset : ShakeBasePreset
    {
        [Header("Shake Settings")]
        [Tooltip("Settings for low frequency motor")]
        public List<MultiFloatShaker> lowFrequencyMotorShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for high frequency motor")]
        public List<MultiFloatShaker> highFrequencyMotorShake = new List<MultiFloatShaker>();

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { LowFrequencyMotor, HighFrequencyMotor };
#endif
    }
}
