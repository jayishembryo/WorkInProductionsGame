using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.U2D;
#if RENDER_UNIVERSAL
using UnityEngine.Rendering.Universal;
#if !UNITY_6
using UnityEngine.Experimental.Rendering.Universal;
#endif

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Light")]
    public class SmoothShakeLight : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakeLightPreset preset;

        enum LightType { Light, Light2D, None }

        LightType lightType;

        [Header("Light to Shake")]
        [Tooltip("Light to Shake")]
        public Light lightToShake;

        [Header("2D Light To Shake")]
        [Tooltip("2D Light to Shake")]
        public Light2D light2DToShake;

        [Header("Shake Settings")]
        [Tooltip("Settings for Intensity Shake")]
        public List<MultiFloatShaker> intensityShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for Range Shake")]
        public List<MultiFloatShaker> rangeShake = new List<MultiFloatShaker>();

        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Intensity, Range }

        private float startIntensity;
        private float startRange;

        private LightType GetLightType()
        {
            if (lightToShake != null)
            {
                lightType = LightType.Light;
                return lightType;
            }
            else if (light2DToShake != null)
            {
                lightType = LightType.Light2D;
                return lightType;
            }
            else
            {
                lightType = LightType.None;
                return lightType;
            }
        }

        internal new void Awake()
        {
            base.Awake();

            GetLightType();

            if (preset) ApplyPresetSettings(preset);
        }

        protected override Shaker[] GetShakers() => null;

        internal sealed override void Apply(Vector3[] value)
        {
            if (GetLightType() == LightType.None) return;

            switch (lightType)
            {
                case LightType.Light:
                    lightToShake.intensity = startIntensity + value[0].x;
                    lightToShake.range = startRange + value[1].x;
                    break;
                case LightType.Light2D:
                    light2DToShake.intensity = startIntensity + value[0].x;
                    light2DToShake.pointLightOuterRadius = startRange + value[1].x;
                    break;
            }


        }

        internal sealed override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if (preset is SmoothShakeLightPreset)
            {
                SmoothShakeLightPreset SmoothShakeLightPreset = (SmoothShakeLightPreset)preset;
                intensityShake.Clear();
                intensityShake.AddRange(SmoothShakeLightPreset.intensityShake);
                rangeShake.Clear();
                rangeShake.AddRange(SmoothShakeLightPreset.rangeShake);
                timeSettings = SmoothShakeLightPreset.timeSettings;
            }
        }

        internal sealed override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { intensityShake, rangeShake };
        }

        internal sealed override void ResetDefaultValues()
        {
            if (GetLightType() == LightType.None) return;

            switch (lightType)
            {
                case LightType.Light:
                    lightToShake.intensity = startIntensity;
                    lightToShake.range = startRange;
                    break;
                case LightType.Light2D:
                    light2DToShake.intensity = startIntensity;
                    light2DToShake.pointLightOuterRadius = startRange;
                    break;
            }
        }

        internal sealed override void SaveDefaultValues()
        {
            if (GetLightType() == LightType.None) return;

            switch (lightType)
            {
                case LightType.Light:
                    startIntensity = lightToShake.intensity;
                    startRange = lightToShake.range;
                    break;
                case LightType.Light2D:
                    startIntensity = light2DToShake.intensity;
                    startRange = light2DToShake.pointLightOuterRadius;
                    break;
            }
        }
    }
}
#else
namespace SmoothShakePro
    {
        [AddComponentMenu("Smooth Shake Pro/Smooth Shake Light")]
        public class SmoothShakeLight : MultiShakeBase
        {
            [Tooltip("Preset to use for this Smooth Shake")]
            public SmoothShakeLightPreset preset;

            [Header("Light to Shake")]
            [Tooltip("Light to Shake")]
            public Light lightToShake;

            [Header("Shake Settings")]
            [Tooltip("Settings for Intensity Shake")]
            public List<MultiFloatShaker> intensityShake = new List<MultiFloatShaker>();
            [Tooltip("Settings for Range Shake")]
            public List<MultiFloatShaker> rangeShake = new List<MultiFloatShaker>();

            [HideInInspector] internal ShakeToPreview shakeToPreview;
            internal enum ShakeToPreview { Intensity, Range }

            private float startIntensity;
            private float startRange;

            private bool GetLight()
            {
                if (lightToShake != null) return true;

                if (GetComponent<Light>() != null)
                {
                    lightToShake = GetComponent<Light>();
                    return true;
                }
                return false;
            }

            internal new void Awake()
            {
                base.Awake();

                GetLight();

                if (preset) ApplyPresetSettings(preset);
            }

            protected override Shaker[] GetShakers() => null;

            internal override void Apply(Vector3[] value)
            {
                if (!GetLight()) return;

                lightToShake.intensity = startIntensity + value[0].x;
                lightToShake.range = startRange + value[1].x;
            }

            internal override void ApplyPresetSettings(ShakeBasePreset preset)
            {
                if (preset is SmoothShakeLightPreset)
                {
                    SmoothShakeLightPreset SmoothShakeLightPreset = (SmoothShakeLightPreset)preset;
                    intensityShake.Clear();
                    intensityShake.AddRange(SmoothShakeLightPreset.intensityShake);
                    rangeShake.Clear();
                    rangeShake.AddRange(SmoothShakeLightPreset.rangeShake);
                    timeSettings = SmoothShakeLightPreset.timeSettings;
                }
            }

            internal override IEnumerable<Shaker>[] GetMultiShakers()
            {
                return new IEnumerable<Shaker>[] { intensityShake, rangeShake };
            }

            internal override void ResetDefaultValues()
            {
                if (!GetLight()) return;

                lightToShake.intensity = startIntensity;
                lightToShake.range = startRange;
            }

            internal override void SaveDefaultValues()
            {
                if (!GetLight()) return;

                startIntensity = lightToShake.intensity;
                startRange = lightToShake.range;
            }
        }
    }

#endif