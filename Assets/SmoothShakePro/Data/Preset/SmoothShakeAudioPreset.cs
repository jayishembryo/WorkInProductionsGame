using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [CreateAssetMenu(fileName = "SmoothShakeAudioPreset", menuName = "Smooth Shake/Smooth Shake Audio Preset", order = 6)]
    public class SmoothShakeAudioPreset : ShakeBasePreset
    {
        [Header("Shake Settings")]
        [Tooltip("Settings for Volume Shake")]
        public List<MultiFloatShaker> volumeShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for Pan Shake")]
        public List<MultiFloatShaker> panShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for Pitch Shake")]
        public List<MultiFloatShaker> pitchShake = new List<MultiFloatShaker>();

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Volume, Pan, Pitch }
#endif
    }
}
