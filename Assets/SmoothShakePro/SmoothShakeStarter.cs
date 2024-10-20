using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [System.Serializable]
    public class ShakeIncrement
    {
        [Tooltip("The property to change on the shaker")]
        public ShakerProperty property;
        [Tooltip("Which shake should be changed? See documentation for table on which index refers to which shake")]
        public int shakerIndex;
        [Tooltip("The value to increment the property by (if shaker is a float, use the x value)")]
        public Vector3 value;
    }

    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Starter")]
    public class SmoothShakeStarter : ShakeBase
    {
        [Header("Increment Settings")]
        public List<ShakeIncrement> shakeIncrements = new List<ShakeIncrement>();
        public bool incrementOnStart = true;


#if UNITY_2020 
        [Header("Shakes")]
        public List<ShakeBase> shakes = new List<ShakeBase>();
#else
        [Header("Shakes")]
        public List<ShakeBase> shakes = new();
#endif

        internal new void Start()
        {
            if (incrementOnStart)
            {
                AddIncrements();
            }
            base.Start();
        }

        public void AddIncrements()
        {
            if (shakes.Count == 0)
            {
                Debug.LogWarning("No shakes have been added to the list. Please add shakes to the list before adding increments.");
                return;
            }

            if (shakeIncrements.Count == 0)
            {
                Debug.LogWarning("No increments have been added to the list. Please add increments to the list before adding increments.");
                return;
            }

            foreach (var increment in shakeIncrements)
            {
                Vector3 value = increment.value;
                foreach (var shake in shakes)
                {
                    switch (shake)
                    {
                        case MultiShakeBase msb:
                            msb.SetShakerProperty(increment.shakerIndex, value, increment.property);
                            value += increment.value;
                            break;
                        default:
                            Debug.LogWarning("Shake type of " + shake.GetType() + " is not (yet) supported by SmoothShakeStarter increments");
                            break;
                    }
                }
            }
        }

        public override void StartShake()
        {
            foreach (var shake in shakes)
            {
                shake.StartShake();
            }
        }

        public override void StopShake()
        {
            foreach (var shake in shakes)
            {
                shake.StopShake();
            }
        }

        public override void ForceStop()
        {
            foreach (var shake in shakes)
            {
                shake.ForceStop();
            }
        }

        internal sealed override void Apply(Vector3[] value) { }
        internal override void ApplyPresetSettings(ShakeBasePreset preset) { }
        internal sealed override void ResetDefaultValues() { }
        internal sealed override void SaveDefaultValues() { }
        protected override Shaker[] GetShakers() => null;
    }

}
