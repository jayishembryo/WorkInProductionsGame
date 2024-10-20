using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Rigidbody")]
    public class SmoothShakeRigidbody : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakeRigidbodyPreset preset;

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

        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Force, Torque }

        private Rigidbody rb;
        private Rigidbody2D rb2;

        internal new void Awake()
        {
            base.Awake();

            if(GetComponent<Rigidbody>() != null) rb = GetComponent<Rigidbody>();
            if(GetComponent<Rigidbody2D>() != null) rb2 = GetComponent<Rigidbody2D>();

            if (preset) ApplyPresetSettings(preset);
        }

        internal sealed override void Apply(Vector3[] value)
        {
            if (rb)
            {
                rb.AddForce(value[0], ForceMode.Force);
                rb.AddTorque(value[1], ForceMode.Force);
            }
            else if (rb2)
            {
                rb2.AddForce(value[0], ForceMode2D.Force);
                rb2.AddTorque(value[1].z, ForceMode2D.Force);
            }
        }

        internal sealed override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if(preset is SmoothShakeRigidbodyPreset)
            {
                SmoothShakeRigidbodyPreset smoothShakeRigidbodyPreset = (SmoothShakeRigidbodyPreset)preset;
                forceShake.Clear();
                forceShake.AddRange(smoothShakeRigidbodyPreset.forceShake);
                torqueShake.Clear();
                torqueShake.AddRange(smoothShakeRigidbodyPreset.torqueShake);
                timeSettings = smoothShakeRigidbodyPreset.timeSettings;
            }
        }

        internal sealed override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { forceShake, torqueShake };
        }

        internal sealed override void ResetDefaultValues() { }

        internal sealed override void SaveDefaultValues() { }

        protected override Shaker[] GetShakers() => null;
    }

}
