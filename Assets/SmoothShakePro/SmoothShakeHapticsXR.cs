using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
#if UNITY_XR
using UnityEngine.XR.Interaction.Toolkit;
#endif

namespace SmoothShakePro
{
#if UNITY_XR
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Haptics XR")]
    public class SmoothShakeHapticsXR : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakeHapticsXRPreset preset;

#if UNITY_XR
        [Header("Controller Settings")]
        public XRBaseController leftController;
        public XRBaseController rightController;
#endif

        [Header("Shake Settings")]
        [Tooltip("Settings for left controller")]
        public List<MultiFloatShaker> leftControllerShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for right controller")]
        public List<MultiFloatShaker> rightControllerShake = new List<MultiFloatShaker>();

        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { leftController, rightController };

        internal new void Awake()
        {
            base.Awake();

#if !UNITY_XR
            Debug.LogWarning("Smooth Shake Haptics for XR requires the Unity XR Interaction Toolkit to work. If you are developing for XR, Please install it from the Unity Package Manager.");
#else
            if (leftController == null || rightController == null)
            {
                Debug.LogWarning("Smooth Shake Haptics for XR requires two XR controllers to be set.");
            }
#endif

            if (preset) ApplyPresetSettings(preset);
        }

        internal override void Apply(Vector3[] value)
        {
#if UNITY_XR
            if (leftController == null || rightController == null) return;

            leftController.SendHapticImpulse(value[0].x, Time.deltaTime);
            rightController.SendHapticImpulse(value[1].x, Time.deltaTime);
#endif
        }

        internal override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if (preset is SmoothShakeHapticsXRPreset smoothShakeHapticsPreset)
            {
                leftControllerShake.Clear();
                leftControllerShake.AddRange(smoothShakeHapticsPreset.leftControllerShake);
                rightControllerShake.Clear();
                rightControllerShake.AddRange(smoothShakeHapticsPreset.rightControllerShake);
            }
        }

        internal override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { leftControllerShake, rightControllerShake };
        }

        internal override void ResetDefaultValues() { }

        internal override void SaveDefaultValues() { }

        protected override Shaker[] GetShakers() => null;
    }
#endif
        }