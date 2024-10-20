using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [CreateAssetMenu(fileName = "SmoothShakeRigidbodyPreset", menuName = "Smooth Shake/Smooth Shake Rigidbody Preset", order = 3)]
    public class SmoothShakeRigidbodyPreset : ShakeBasePreset
    {
#if UNITY_2020
        [Header("Shake Settings")]
        [Tooltip("Settings for Force Shake")]
        public List<MultiVectorShaker> forceShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for Torque Shake")]
        public List<MultiVectorShaker> torqueShake = new List<MultiVectorShaker>();
#else
        [Header("Shake Settings")]
        [Tooltip("Settings for Force Shake")]
        public List<MultiVectorShaker> forceShake = new();
        [Tooltip("Settings for Torque Shake")]
        public List<MultiVectorShaker> torqueShake = new();
#endif

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Force, Torque }
#endif
    }

}
