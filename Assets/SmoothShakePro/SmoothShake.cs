using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake")]
    public class SmoothShake : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakePreset preset;

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

        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Position, Rotation, Scale, FOV }

        [HideInInspector] internal Vector3 startPosition;
        [HideInInspector] internal Vector3 startRotation;
        [HideInInspector] internal Vector3 startScale;
        [HideInInspector] internal float startFOV;

        private Camera cam;

        private Camera GetCam()
        {
            if (cam) return cam;
            return GetComponent<Camera>();
        }

        private new void Awake()
        {
            cam = GetCam();
            base.Awake();

            if(preset) ApplyPresetSettings(preset);
        }

        internal sealed override void Apply(Vector3[] value)
        {
            transform.localPosition = startPosition + value[0];
            transform.localEulerAngles = startRotation + value[1];
            if (!cam) transform.localScale = startScale + value[2];
            if(cam) cam.fieldOfView = startFOV + value[2].x;
        }

        protected sealed override Shaker[] GetShakers() { return null; }

        internal sealed override IEnumerable<Shaker>[] GetMultiShakers()
        {
            if (cam) return new IEnumerable<Shaker>[] { positionShake, rotationShake, FOVShake };
            else return new IEnumerable<Shaker>[] { positionShake, rotationShake, scaleShake };
        }

        internal sealed override void ResetDefaultValues()
        {
            transform.localPosition = startPosition;
            transform.localEulerAngles = startRotation;
            if(!cam) transform.localScale = startScale;
            if(cam) cam.fieldOfView = startFOV;
        }

        internal sealed override void SaveDefaultValues()
        {
            cam = GetCam();

            startPosition = transform.localPosition;
            startRotation = transform.localEulerAngles;
            if (!cam) startScale = transform.localScale;
            if(cam) startFOV = cam.fieldOfView;
        }

        internal sealed override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if(preset is SmoothShakePreset smoothShakePreset)
            {
                positionShake.Clear();
                positionShake.AddRange(smoothShakePreset.positionShake);
                rotationShake.Clear();
                rotationShake.AddRange(smoothShakePreset.rotationShake);
                scaleShake.Clear();
                scaleShake.AddRange(smoothShakePreset.scaleShake);
                FOVShake.Clear();
                FOVShake.AddRange(smoothShakePreset.FOVShake);
                timeSettings = smoothShakePreset.timeSettings;
            }
            else
            {
                Debug.LogError("Preset is not a SmoothShakePreset");
            }
        }
    }
}
