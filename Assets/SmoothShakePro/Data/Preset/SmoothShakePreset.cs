using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [CreateAssetMenu(fileName = "SmoothShakePreset", menuName = "Smooth Shake/Smooth Shake Preset", order = 1)]
    public class SmoothShakePreset : ShakeBasePreset
    {
#if UNITY_2020
        [Header("Shake Settings")]
        [Tooltip("Settings for Position Shake")]
        public List<MultiVectorShaker> positionShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for Rotation Shake")]
        public List<MultiVectorShaker> rotationShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for Scale Shake")]
        public List<MultiVectorShaker> scaleShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for FOV Shake")]
        public List<MultiFloatShaker> FOVShake = new List<MultiFloatShaker>();
#else
        [Header("Shake Settings")]
        [Tooltip("Settings for Position Shake")]
        public List<MultiVectorShaker> positionShake = new();
        [Tooltip("Settings for Rotation Shake")]
        public List<MultiVectorShaker> rotationShake = new();
        [Tooltip("Settings for Scale Shake")]
        public List<MultiVectorShaker> scaleShake = new();
        [Tooltip("Settings for FOV Shake")]
        public List<MultiFloatShaker> FOVShake = new();
#endif

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Position, Rotation, Scale, FOV }
#endif
    }

}
