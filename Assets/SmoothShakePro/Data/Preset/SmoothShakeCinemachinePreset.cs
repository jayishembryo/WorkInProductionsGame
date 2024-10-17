using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
#if CINEMACHINE
    [CreateAssetMenu(fileName = "SmoothShakeCinemachinePreset", menuName = "Smooth Shake/Smooth Shake Cinemachine Preset", order = 2)]
    public class SmoothShakeCinemachinePreset : ShakeBasePreset
    {
#if UNITY_2020
        [Header("Shake Settings")]
        [Tooltip("Settings for Position Shake")]
        public List<MultiVectorShaker> positionShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for Rotation Shake")]
        public List<MultiVectorShaker> rotationShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for FOV Shake")]
        public List<MultiFloatShaker> FOVShake = new List<MultiFloatShaker>();
#else
        [Header("Shake Settings")]
        [Tooltip("Settings for Position Shake")]
        public List<MultiVectorShaker> positionShake = new();
        [Tooltip("Settings for Rotation Shake")]
        public List<MultiVectorShaker> rotationShake = new();
        [Tooltip("Settings for FOV Shake")]
        public List<MultiFloatShaker> FOVShake = new();
#endif

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Position, Rotation, FOV }
#endif
    }
#endif
}
