using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if RENDER_UNIVERSAL
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#endif
#if RENDER_HDRP
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
#endif
#if SMOOTHPOSTPROCESSING
using SmoothPostProcessing;
#endif

namespace SmoothShakePro
{
#if UNITY_TIMELINE && UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
    [AddComponentMenu("Smooth Shake Pro/Post Processing/Smooth Shake Post Processing")]
    public class SmoothShakePostProcessing : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakePostProcessingPreset preset;

#if RENDER_UNIVERSAL || RENDER_HDRP
        [Header("Volume settings")]
        [Tooltip("Here you can manually set a volume to apply the shake effect to. Alternatively, Smooth Shake can automatically detect the volume if this object has one.")]
        public Volume volume;
#else
        [InitializeOnLoadMethod]
        public static void Init()
        {
            Debug.LogWarning("The extension Smooth Shake Post Processing is not initialized because neither URP nor HDRP is installed. Please first install either render pipeline and if you haven't already, drag the extension to the SmoothShakePro folder in your project.");
        }
#endif
        [Tooltip("Select which settings you want to use")]
        public Overrides overrides;
#if SMOOTHPOSTPROCESSING
        [Tooltip("Select which settings from SmoothShakePro effects you want to use")]
        public SmoothPostProcessingOverrides smoothPostProcessingOverrides;
#endif

        [Header("Weight Shake Settings")]
        public List<MultiFloatShaker> weightShake = new List<MultiFloatShaker>();

        [Header("Bloom Shake Settings")]
        [Tooltip("Shake settings for bloom threshold")]
        public List<MultiFloatShaker> bloomThresholdShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for bloom intensity")]
        public List<MultiFloatShaker> bloomIntensityShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for bloom scatter")]
        public List<MultiFloatShaker> bloomScatterShake = new List<MultiFloatShaker>();

        [Header("Channel Mixer Shake Settings")]
        [Tooltip("Shake settings for channel mixer red (RGB)")]
        public List<MultiVectorShaker> channelMixerRedShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for channel mixer green (RGB)")]
        public List<MultiVectorShaker> channelMixerGreenShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for channel mixer blue (RGB)")]
        public List<MultiVectorShaker> channelMixerBlueShake = new List<MultiVectorShaker>();

        [Header("Chromatic Aberration Shake Settings")]
        [Tooltip("Shake settings for chromatic aberration")]
        public List<MultiFloatShaker> chromaticAberrationShake = new List<MultiFloatShaker>();

        [Header("Color Adjustments Shake Settings")]
        [Tooltip("Shake settings for post exposure")]
        public List<MultiFloatShaker> postExposureShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for contrast")]
        public List<MultiFloatShaker> contrastShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for hue shift")]
        public List<MultiFloatShaker> hueShiftShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for saturation")]
        public List<MultiFloatShaker> saturationShake = new List<MultiFloatShaker>();

        [Header("Film Grain Shake Settings")]
        [Tooltip("Shake settings for film grain")]
        public List<MultiFloatShaker> filmGrainShake = new List<MultiFloatShaker>();

        [Header("Lens Distortion Shake Settings")]
        [Tooltip("Shake settings for lens distortion")]
        public List<MultiFloatShaker> lensDistortionIntensityShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for lens distortion XY multiplier (only use X & Y)")]
        public List<MultiVectorShaker> lensDistortionXYMultiplierShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for lens distortion center (only use X & Y)")]
        public List<MultiVectorShaker> lensDistortionCenterShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for lens distortion scale")]
        public List<MultiFloatShaker> lensDistortionScaleShake = new List<MultiFloatShaker>();

        [Header("Motion Blur Shake Settings")]
        [Tooltip("Shake settings for motion blur")]
        public List<MultiFloatShaker> motionBlurShake = new List<MultiFloatShaker>();

        [Header("Panini Projection Shake Settings")]
        [Tooltip("Shake settings for panini projection distance")]
        public List<MultiFloatShaker> paniniProjectionDistanceShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for panini projection crop")]
        public List<MultiFloatShaker> paniniProjectionCropShake = new List<MultiFloatShaker>();

        [Header("Split Toning Shake Settings")]
        [Tooltip("Shake settings for split toning balance")]
        public List<MultiFloatShaker> splitToningBalanceShake = new List<MultiFloatShaker>();

        [Header("Vignette Shake Settings")]
        [Tooltip("Shake settings for vignette (only use X & Y)")]
        public List<MultiVectorShaker> vignetteCenterShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for vignette intensity")]
        public List<MultiFloatShaker> vignetteIntensityShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for vignette smoothness")]
        public List<MultiFloatShaker> vignetteSmoothnessShake = new List<MultiFloatShaker>();
#if RENDER_HDRP && !RENDER_UNIVERSAL
        public List<MultiFloatShaker> vignetteRoundnessShake = new List<MultiFloatShaker>();
#endif

        [Header("White Balance Shake Settings")]
        [Tooltip("Shake settings for white balance")]
        public List<MultiFloatShaker> whiteBalanceTemperatureShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for white balance tint")]
        public List<MultiFloatShaker> whiteBalanceTintShake = new List<MultiFloatShaker>();

#if RENDER_UNIVERSAL && !RENDER_HDRP
        [Header("Depth Of Field Gaussian Shake Settings")]
        [Tooltip("Shake settings for depth of field gaussian. X = start, Y = end")]
        public List<MultiVectorShaker> depthOfFieldGaussianStartEndShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for depth of field gaussian max radius.")]
        public List<MultiFloatShaker> depthOfFieldGaussianMaxRadiusShake = new List<MultiFloatShaker>();

        [Header("Depth Of Field Bokeh Shake Settings")]
        [Tooltip("Shake settings for depth of field bokeh focus distance.")]
        public List<MultiFloatShaker> depthOfFieldBokehFocusDistanceShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for depth of field bokeh focal length.")]
        public List<MultiFloatShaker> depthOfFieldBokehFocalLengthShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for depth of field bokeh aperture.")]
        public List<MultiFloatShaker> depthOfFieldBokehApertureShake = new List<MultiFloatShaker>();

        [Header("Color Lookup Shake Settings")]
        [Tooltip("Shake settings for color lookup.")]
        public List<MultiFloatShaker> colorLookupShake = new List<MultiFloatShaker>();

#elif RENDER_HDRP && !RENDER_UNIVERSAL
        [Header("Depth of Field Physical Shake Settings")]
        [Tooltip("Shake settings for depth of field physical camera focus distance.")]
        public List<MultiFloatShaker> depthOfFieldPhysicalFocusDistanceShake = new List<MultiFloatShaker>();

        [Header("Depth of Field Manual Shake Settings")]
        [Tooltip("Shake settings for depth of field manual (near). X = start, Y = end")]
        public List<MultiVectorShaker> depthOfFieldManualNearStartEndShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for depth of field manual (far). X = start, Y = end")]
        public List<MultiVectorShaker> depthOfFieldManualFarStartEndShake = new List<MultiVectorShaker>();

        [Header("Fog Shake Settings")]
        [Tooltip("Shake settings for fog attenuation distance.")]
        public List<MultiFloatShaker> fogAttenuationShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for fog base height.")]
        public List<MultiFloatShaker> fogBaseHeightShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for fog max height.")]
        public List<MultiFloatShaker> fogMaxHeightShake = new List<MultiFloatShaker>();
#endif

#if SMOOTHPOSTPROCESSING
        [Header("Blur Shake Settings")]
        [Tooltip("Shake settings for blur range")]
        public List<MultiFloatShaker> blurShake = new List<MultiFloatShaker>();

        [Header("Displace Shake Settings")]
        [Tooltip("Shake settings for displacement intensity")]
        public List<MultiFloatShaker> displaceShake = new List<MultiFloatShaker>();

        [Header("Edge Detection Shake Settings")]
        [Tooltip("Shake settings for edge detection intensity")]
        public List<MultiFloatShaker> edgeDetectionShake = new List<MultiFloatShaker>();

        [Header("Glitch Shake Settings")]
        [Tooltip("Shake settings for glitch intensity")]
        public List<MultiFloatShaker> glitchIntensityShake = new List<MultiFloatShaker>();

        [Header("Invert Shake Settings")]
        [Tooltip("Shake settings for invert intensity")]
        public List<MultiFloatShaker> invertIntensityShake = new List<MultiFloatShaker>();

        [Header("Kaleidoscope Shake Settings")]
        [Tooltip("Shake settings for kaleidoscope rotation")]
        public List<MultiFloatShaker> kaleidoscopeShake = new List<MultiFloatShaker>();

        [Header("Monitor Shake Settings")]
        [Tooltip("Shake settings for monitor scanline intensity")]
        public List<MultiFloatShaker> monitorScanlineShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for monitor noise intensity")]
        public List<MultiFloatShaker> monitorNoiseShake = new List<MultiFloatShaker>();

        [Header("Night Vision Shake Settings")]
        [Tooltip("Shake settings for night vision brightness")]
        public List<MultiFloatShaker> nightVisionBrightnessShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for night vision flicker intensity")]
        public List<MultiFloatShaker> nightVisionFlickerIntensityShake = new List<MultiFloatShaker>();

        [Header("Pixelate Shake Settings")]
        [Tooltip("Shake settings for pixel size (only use X and Y)")]
        public List<MultiVectorShaker> pixelSizeShake = new List<MultiVectorShaker>();

        [Header("RGB Split Shake Settings")]
        [Tooltip("Shake settings for RGB split offset R (only use X & Y)")]
        public List<MultiVectorShaker> rgbSplitShakeR = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for RGB split offset G (only use X & Y)")]
        public List<MultiVectorShaker> rgbSplitShakeG = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for RGB split offset B (only use X & Y)")]
        public List<MultiVectorShaker> rgbSplitShakeB = new List<MultiVectorShaker>();

        [Header("Rot Blur Shake Settings")]
        [Tooltip("Shake settings for rot blur strength")]
        public List<MultiFloatShaker> rotBlurShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for rot blur angle")]
        public List<MultiFloatShaker> rotBlurAngleShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for rot blur center (only use X & Y)")]
        public List<MultiVectorShaker> rotBlurCenterShake = new List<MultiVectorShaker>();

        [Header("Solarize Shake Settings")]
        [Tooltip("Shake settings for solarize intensity")]
        public List<MultiVectorShaker> solarizeIntensityShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for solarize threshold")]
        public List<MultiFloatShaker> solarizeThresholdShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for solarize smoothness")]
        public List<MultiFloatShaker> solarizeSmoothnessShake = new List<MultiFloatShaker>();

        [Header("Thermal Vision Shake Settings")]
        [Tooltip("Shake settings for thermal vision brightness")]
        public List<MultiFloatShaker> thermalVisionBrightnessShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for thermal vision contrast")]
        public List<MultiFloatShaker> thermalVisionContrastShake = new List<MultiFloatShaker>();

        [Header("Tint Shake Settings")]
        [Tooltip("Shake settings for tint vignette intensity")]
        public List<MultiFloatShaker> tintVignetteIntensityShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for tint noise intensity")]
        public List<MultiFloatShaker> tintNoiseIntensityShake = new List<MultiFloatShaker>();

        [Header("Twirl Shake Settings")]
        [Tooltip("Shake settings for twirl strength")]
        public List<MultiFloatShaker> twirlStrengthShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for twirl center (only use X & Y)")]
        public List<MultiVectorShaker> twirlCenterShake = new List<MultiVectorShaker>();
        [Tooltip("Shake settings for twirl radius")]
        public List<MultiFloatShaker> twirlRadiusShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for twirl vignette")]
        public List<MultiFloatShaker> twirlVignetteShake = new List<MultiFloatShaker>();

        [Header("Twitch Shake Settings")]
        [Tooltip("Shake settings for twitch offset")]
        public List<MultiVectorShaker> twitchOffsetShake = new List<MultiVectorShaker>();

        [Header("Wave Warp Shake Settings")]
        [Tooltip("Shake settings for wave warp amplitude")]
        public List<MultiFloatShaker> waveWarpAmplitudeShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for wave warp vignette")]
        public List<MultiFloatShaker> waveWarpVignetteShake = new List<MultiFloatShaker>();

        [Header("Zoom Blur Shake Settings")]
        [Tooltip("Shake settings for zoom blur strength")]
        public List<MultiFloatShaker> zoomBlurShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for zoom blur center (only use X & Y)")]
        public List<MultiVectorShaker> zoomBlurCenterShake = new List<MultiVectorShaker>();

#if RENDER_UNIVERSAL && !RENDER_HDRP
        [Header("Gaussian Blur Shake Settings")]
        [Tooltip("Shake settings for gaussian blur strength")]
        public List<MultiFloatShaker> gaussianBlurShake = new List<MultiFloatShaker>();

        [Header("Noise Bloom Shake Settings")]
        [Tooltip("Shake settings for noise bloom strength")]
        public List<MultiFloatShaker> noiseBloomStrengthShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for noise bloom threshold")]
        public List<MultiFloatShaker> noiseBloomThresholdShake = new List<MultiFloatShaker>();
        [Tooltip("Shake settings for noise bloom range")]
        public List<MultiFloatShaker> noiseBloomRangeShake = new List<MultiFloatShaker>();
#endif
#endif

        [HideInInspector] public ShakeToPreview shakeToPreview;
        public enum ShakeToPreview
        {
            Weight,
            BloomThreshold, BloomIntensity, BloomScatter,
            ChannelMixerRed, ChannelMixerBlue, ChannelMixerGreen,
            ChromaticAberration,
            PostExposure, Contrast, HueShift, Saturation,
            FilmGrain,
            LensDistortionIntensity, LensDistortionXYMultiplier, LensDistortionCenter, LensDistortionScale,
            MotionBlur,
            PaniniProjectionDistance, PaniniProjectionCrop,
            SplitToningBalance,
            VignetteCenter, VignetteIntensity, VignetteSmoothness,
#if RENDER_HDRP && !RENDER_UNIVERSAL
            VignetteRoundness,
#endif
            WhiteBalanceTemperature, WhiteBalanceTint,
#if RENDER_UNIVERSAL && !RENDER_HDRP
            DepthOfFieldGaussianStartEnd, DepthOfFieldGaussianMaxRadius,
            DepthOfFieldBokehFocusDistance, DepthOfFieldBokehFocalLength, DepthOfFieldBokehAperture,
            ColorLookup
#elif RENDER_HDRP && !RENDER_UNIVERSAL
            DepthOfFieldPhysicalFocusDistance,
            DepthOfFieldManualNearStartEnd, DepthOfFieldManualFarStartEnd,
            FogAttenuation, FogBaseHeight, FogMaxHeight
#endif

#if SMOOTHPOSTPROCESSING
            , Blur, 
            Displace, 
            EdgeDetection, 
            Glitch, 
            Invert, 
            Kaleidoscope, 
            MonitorScanline, MonitorNoise,
            NightVisionBrightness, NightVisionFlickerIntensity,
            Pixelation,
            RGBSplitR, RGBSplitG, RGBSplitB,
            RotBlur, RotBlurAngle, RotBlurCenter,
            SolarizeIntensity, SolarizeThreshold, SolarizeSmoothness,
            ThermalVisionBrightness, ThermalVisionContrast,
            TintVignette, TintNoise,
            TwirlStrength, TwirlCenter, TwirlRadius, TwirlVignette,
            Twitch, 
            WaveWarpAmplitude, WaveWarpVignette, 
            ZoomBlur, ZoomBlurCenter
#if RENDER_UNIVERSAL && !RENDER_HDRP
            , GaussianBlur, 
            NoiseBloomStrength, NoiseBloomThreshold, NoiseBloomRange
#endif

#endif
        }

        private float startWeight;

        private float startBloomThreshold, startBloomIntensity, startBloomScatter;
        private Vector3 startChannelMixerRed, startChannelMixerGreen, startChannelMixerBlue;
        private float startChromaticAberration;
        private float startPostExposure, startContrast, startHueShift, startSaturation;
        private float startFilmGrain;
        private float startLensDistortionIntensity, startLensDistortionScale;
        private Vector3 startLensDistortionXYMultiplier, startLensDistortionCenter;
        private float startMotionBlur;
        private float startPaniniProjectionDistance, startPaniniProjectionCrop;
        private float startSplitToningBalance;
        private Vector3 startVignetteCenter;
        private float startVignetteIntensity, startVignetteSmoothness;
#if RENDER_HDRP && !RENDER_UNIVERSAL
        private float startVignetteRoundness;
#endif
        private float startWhiteBalanceTemperature, startWhiteBalanceTint;
#if RENDER_UNIVERSAL && !RENDER_HDRP
        private Vector3 startDepthOfFieldGaussianStartEnd;
        private float startDepthOfFieldGaussianMaxRadius;
        private float startDepthOfFieldBokehFocusDistance, startDepthOfFieldBokehFocalLength, startDepthOfFieldBokehAperture;
        private float startColorLookup;
#endif
#if RENDER_HDRP && !RENDER_UNIVERSAL
        private float startDepthOfFieldPhysicalFocusDistance;
        private Vector3 startDepthOfFieldManualNearStartEnd, startDepthOfFieldManualFarStartEnd;
        private float startFogAttenuation, startFogBaseHeight, startFogMaxHeight;
#endif

#if SMOOTHPOSTPROCESSING
        private float startBlur;
        private float startDisplace;
        private float startEdgeDetection;
        private float startGlitch;
        private float startInvert;
        private float startKaleidoscope;
        private float startMonitorScanline, startMonitorNoise;
        private float startNightVisionBrightness, startNightVisionFlickerIntensity;
        private Vector2 startPixelSize;
        private Vector2 startRGBSplitR, startRGBSplitG, startRGBSplitB;
        private float startRotBlur, startRotBlurAngle;
        private Vector2 startRotBlurCenter;
        private Vector3 startSolarizeIntensity;
        private float startSolarizeThreshold, startSolarizeSmoothness;
        private float startThermalVisionBrightness, startThermalVisionContrast;
        private float startTintVignetteIntensity, startTintNoiseIntensity;
        private float startTwirlStrength, startTwirlRadius, startTwirlVignette;
        private Vector2 startTwirlCenter;
        private Vector2 startTwitchOffset;
        private float startWaveWarpAmplitude, startWaveWarpVignette;
        private float startZoomBlur;
        private Vector2 startZoomBlurCenter;
#if RENDER_UNIVERSAL && !RENDER_HDRP
        private float startGaussianBlur;
        private float startNoiseBloomStrength, startNoiseBloomThreshold, startNoiseBloomRange;
#endif
#endif

#if RENDER_UNIVERSAL || RENDER_HDRP
        Bloom bloom;
        ChannelMixer channelMixer;
        ChromaticAberration chromaticAberration;
        ColorAdjustments colorAdjustments;
        FilmGrain filmGrain;
        LensDistortion lensDistortion;
        MotionBlur motionBlur;
        PaniniProjection paniniProjection;
        SplitToning splitToning;
        Vignette vignette;
        WhiteBalance whiteBalance;
        DepthOfField depthOfField;
#if RENDER_UNIVERSAL && !RENDER_HDRP
        ColorLookup colorLookup;
#endif
#if RENDER_HDRP && !RENDER_UNIVERSAL
        Fog fog;
#endif

#if SMOOTHPOSTPROCESSING
#if !RENDER_UNIVERSAL && RENDER_HDRP
        BlurFeatureHDRP blur;
        DisplaceFeatureHDRP displace;
        EdgeDetectionFeatureHDRP edgeDetection;
        GlitchFeatureHDRP glitch;
        InvertFeatureHDRP invert;
        KaleidoscopeFeatureHDRP kaleidoscope;
        MonitorFeatureHDRP monitor;
        NightVisionFeatureHDRP nightVision;
        PixelationFeatureHDRP pixelation;
        RotBlurFeatureHDRP rotBlur;
        SolarizeFeatureHDRP solarize;
        ThermalVisionFeatureHDRP thermalVision;
        TintFeatureHDRP tint;
        TwirlFeatureHDRP twirl;
        TwitchFeatureHDRP twitch;
        WaveWarpFeatureHDRP waveWarp;
        ZoomBlurFeatureHDRP zoomBlur;
#endif

#if RENDER_UNIVERSAL && !RENDER_HDRP
        BlurSettings blur;
        DisplaceSettings displace;
        EdgeDetectionSettings edgeDetection;
        GlitchSettings glitch;
        InvertSettings invert;
        KaleidoscopeSettings kaleidoscope;
        MonitorSettings monitor;
        NightVisionSettings nightVision;
        PixelationSettings pixelation;
        RGBSplitSettings rgbSplit;
        RotBlurSettings rotBlur;
        SolarizeSettings solarize;
        ThermalVisionSettings thermalVision;
        TintSettings tint;
        TwirlSettings twirl;
        TwitchSettings twitch;
        WaveWarpSettings waveWarp;
        ZoomBlurSettings zoomBlur;
        GaussianBlurSettings gaussianBlur;
        NoiseBloomSettings noiseBloom;
#endif
#endif

        VolumeProfile clonedStack;


        private Volume GetVolume()
        {
            if (volume != null) return volume;
            if (!TryGetComponent<Volume>(out volume))
            {
                if (volume != null) return volume;
                Debug.LogWarning("Skipping Smooth Shake Post Processing because object doesn't have a Volume component or Volume is not set in the inspector.");
                return volume;
            }
            return volume;
        }

        private void GetOverrideComponents(Volume volume)
        {
            if (volume.profile == null)
            {
                Debug.LogWarning("Skipping Smooth Shake Post Processing because Volume doesn't have a profile.");
                return;
            }

            volume.profile.TryGet(out bloom);
            volume.profile.TryGet(out channelMixer);
            volume.profile.TryGet(out chromaticAberration);
            volume.profile.TryGet(out colorAdjustments);
            volume.profile.TryGet(out filmGrain);
            volume.profile.TryGet(out lensDistortion);
            volume.profile.TryGet(out motionBlur);
            volume.profile.TryGet(out paniniProjection);
            volume.profile.TryGet(out splitToning);
            volume.profile.TryGet(out vignette);
            volume.profile.TryGet(out whiteBalance);
            volume.profile.TryGet(out depthOfField);
#if RENDER_UNIVERSAL && !RENDER_HDRP
            volume.profile.TryGet(out colorLookup);
#elif RENDER_HDRP && !RENDER_UNIVERSAL
            volume.profile.TryGet(out fog);
#endif

#if SMOOTHPOSTPROCESSING
            volume.profile.TryGet(out blur);
            volume.profile.TryGet(out displace);
            volume.profile.TryGet(out edgeDetection);
            volume.profile.TryGet(out glitch);
            volume.profile.TryGet(out invert);
            volume.profile.TryGet(out kaleidoscope);
            volume.profile.TryGet(out monitor);
            volume.profile.TryGet(out nightVision);
            volume.profile.TryGet(out pixelation);
            volume.profile.TryGet(out rgbSplit);
            volume.profile.TryGet(out rotBlur);
            volume.profile.TryGet(out solarize);
            volume.profile.TryGet(out thermalVision);
            volume.profile.TryGet(out tint);
            volume.profile.TryGet(out twirl);
            volume.profile.TryGet(out twitch);
            volume.profile.TryGet(out waveWarp);
            volume.profile.TryGet(out zoomBlur);
#if RENDER_UNIVERSAL && !RENDER_HDRP
            volume.profile.TryGet(out gaussianBlur);
            volume.profile.TryGet(out noiseBloom);
#endif
#endif
        }
#endif

        private new void Start()
        {
#if RENDER_UNIVERSAL || RENDER_HDRP
            volume = GetVolume();
            GetOverrideComponents(volume);
#endif
            base.Start();
        }

        private bool CheckFlag(Overrides overrides)
        {
            if (this.overrides.HasFlag(overrides)) return true;
            else return false;
        }

#if SMOOTHPOSTPROCESSING
        private bool CheckFlag(SmoothPostProcessingOverrides overrides)
        {
            if (this.smoothPostProcessingOverrides.HasFlag(overrides)) return true;
            else return false;
        }
#endif

#if RENDER_UNIVERSAL || RENDER_HDRP
#if SMOOTHPOSTPROCESSING
        private bool CheckOverrides(Overrides overrides, SmoothPostProcessingOverrides SSPoverrides)
#else
        private bool CheckOverrides(Overrides overrides)
#endif
        {
            switch (overrides)
            {
                case Overrides.Weight:
                    break;
                case Overrides.Bloom:
                    if (!bloom) return false;
                    break;
                case Overrides.ChannelMixer:
                    if (!channelMixer) return false;
                    break;
                case Overrides.ChromaticAberration:
                    if (!chromaticAberration) return false;
                    break;
                case Overrides.ColorAdjustments:
                    if (!colorAdjustments) return false;
                    break;
                case Overrides.FilmGrain:
                    if (!filmGrain) return false;
                    break;
                case Overrides.LensDistortion:
                    if (!lensDistortion) return false;
                    break;
                case Overrides.MotionBlur:
                    if (!motionBlur) return false;
                    break;
                case Overrides.PaniniProjection:
                    if (!paniniProjection) return false;
                    break;
                case Overrides.SplitToning:
                    if (!splitToning) return false;
                    break;
                case Overrides.Vignette:
                    if (!vignette) return false;
                    break;
                case Overrides.WhiteBalance:
                    if (!whiteBalance) return false;
                    break;
#if RENDER_UNIVERSAL && !RENDER_HDRP
                case Overrides.DepthOfFieldGaussian:
                    if (!depthOfField) return false;
                    break;
                case Overrides.DepthOfFieldBokeh:
                    if (!depthOfField) return false;
                    break;
                case Overrides.ColorLookup:
                    if (!colorLookup) return false;
                    break;
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                case Overrides.DepthOfFieldPhysical:
                    if (!depthOfField) return false;
                    break;
                case Overrides.DepthOfFieldManual:
                    if (!depthOfField) return false;
                    break;
                case Overrides.Fog:
                    if (!fog) return false;
                    break;
#endif
            }

#if SMOOTHPOSTPROCESSING
            switch (SSPoverrides)
            {
                case SmoothPostProcessingOverrides.Blur:
                    if (!blur) return false;
                    break;
                case SmoothPostProcessingOverrides.Displace:
                    if (!displace) return false;
                    break;
                case SmoothPostProcessingOverrides.EdgeDetection:
                    if (!edgeDetection) return false;
                    break;
                case SmoothPostProcessingOverrides.Glitch:
                    if (!glitch) return false;
                    break;
                case SmoothPostProcessingOverrides.Invert:
                    if (!invert) return false;
                    break;
                case SmoothPostProcessingOverrides.Kaleidoscope:
                    if (!kaleidoscope) return false;
                    break;
                case SmoothPostProcessingOverrides.Monitor:
                    if (!monitor) return false;
                    break;
                case SmoothPostProcessingOverrides.NightVision:
                    if (!nightVision) return false;
                    break;
                case SmoothPostProcessingOverrides.Pixelation:
                    if (!pixelation) return false;
                    break;
                case SmoothPostProcessingOverrides.RGBSplit:
                    if (!rgbSplit) return false;
                    break;
                case SmoothPostProcessingOverrides.RotBlur:
                    if (!rotBlur) return false;
                    break;
                case SmoothPostProcessingOverrides.Solarize:
                    if (!solarize) return false;
                    break;
                case SmoothPostProcessingOverrides.ThermalVision:
                    if (!thermalVision) return false;
                    break;
                case SmoothPostProcessingOverrides.Tint:
                    if (!tint) return false;
                    break;
                case SmoothPostProcessingOverrides.Twirl:
                    if (!twirl) return false;
                    break;
                case SmoothPostProcessingOverrides.Twitch:
                    if (!twitch) return false;
                    break;
                case SmoothPostProcessingOverrides.WaveWarp:
                    if (!waveWarp) return false;
                    break;
                case SmoothPostProcessingOverrides.ZoomBlur:
                    if (!zoomBlur) return false;
                    break;
#if RENDER_UNIVERSAL && !RENDER_HDRP
                case SmoothPostProcessingOverrides.GaussianBlur:
                    if (!gaussianBlur) return false;
                    break;
                case SmoothPostProcessingOverrides.NoiseBloom:
                    if (!noiseBloom) return false;
                    break;
#endif
            }
#endif

            return true;
        }
#endif

        internal override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { weightShake,
                bloomThresholdShake, bloomIntensityShake, bloomScatterShake,
                channelMixerRedShake, channelMixerGreenShake, channelMixerBlueShake,
                chromaticAberrationShake,
                postExposureShake, contrastShake, hueShiftShake, saturationShake,
                filmGrainShake,
                lensDistortionIntensityShake, lensDistortionXYMultiplierShake, lensDistortionCenterShake, lensDistortionScaleShake,
                motionBlurShake,
                paniniProjectionDistanceShake, paniniProjectionCropShake,
                splitToningBalanceShake,
                vignetteCenterShake, vignetteIntensityShake, vignetteSmoothnessShake,
                whiteBalanceTemperatureShake, whiteBalanceTintShake,
#if RENDER_UNIVERSAL && !RENDER_HDRP
                depthOfFieldGaussianStartEndShake, depthOfFieldGaussianMaxRadiusShake,
                depthOfFieldBokehFocusDistanceShake, depthOfFieldBokehFocalLengthShake, depthOfFieldBokehApertureShake,
                colorLookupShake,
#if SMOOTHPOSTPROCESSING
                gaussianBlurShake,
#endif
#endif
#if RENDER_HDRP && !RENDER_UNIVERSAL
                vignetteRoundnessShake, 
                depthOfFieldPhysicalFocusDistanceShake, depthOfFieldManualNearStartEndShake, depthOfFieldManualFarStartEndShake,
                fogAttenuationShake, fogBaseHeightShake, fogMaxHeightShake,
#endif
#if SMOOTHPOSTPROCESSING
                blurShake,
                displaceShake,
                edgeDetectionShake,
                glitchIntensityShake,
                invertIntensityShake,
                kaleidoscopeShake,
                monitorScanlineShake, monitorNoiseShake,
                nightVisionBrightnessShake, nightVisionFlickerIntensityShake,
                pixelSizeShake,
                rgbSplitShakeR, rgbSplitShakeG, rgbSplitShakeB,
                rotBlurShake, rotBlurAngleShake, rotBlurCenterShake,
                solarizeIntensityShake, solarizeThresholdShake, solarizeSmoothnessShake,
                thermalVisionBrightnessShake, thermalVisionContrastShake,

                tintVignetteIntensityShake, tintNoiseIntensityShake,
                twirlStrengthShake, twirlCenterShake, twirlRadiusShake, twirlVignetteShake,
                twitchOffsetShake,
                waveWarpAmplitudeShake, waveWarpVignetteShake,
                zoomBlurShake, zoomBlurCenterShake,
#if RENDER_UNIVERSAL && !RENDER_HDRP
                noiseBloomStrengthShake, noiseBloomThresholdShake, noiseBloomRangeShake
#endif
#endif
            };
        }

        internal override void Apply(Vector3[] value)
        {
#if RENDER_UNIVERSAL || RENDER_HDRP

            volume = GetVolume();
            if (!volume) return;

#if SMOOTHPOSTPROCESSING
            if (!CheckOverrides(overrides, smoothPostProcessingOverrides)) GetOverrideComponents(volume);
#else
            if (!CheckOverrides(overrides)) GetOverrideComponents(volume);
#endif

            if (CheckFlag(Overrides.Weight)) volume.weight = startWeight + value[0].x;

            if (bloom && CheckFlag(Overrides.Bloom))
            {
                bloom.threshold.SetValue(new FloatParameter(startBloomThreshold + value[1].x));
                bloom.intensity.SetValue(new FloatParameter(startBloomIntensity + value[2].x));
                bloom.scatter.SetValue(new FloatParameter(startBloomScatter + value[3].x));
            }

            if (channelMixer && CheckFlag(Overrides.ChannelMixer))
            {
                channelMixer.redOutRedIn.SetValue(new FloatParameter(startChannelMixerRed.x + value[4].x));
                channelMixer.redOutGreenIn.SetValue(new FloatParameter(startChannelMixerRed.y + value[4].y));
                channelMixer.redOutBlueIn.SetValue(new FloatParameter(startChannelMixerRed.z + value[4].z));

                channelMixer.greenOutRedIn.SetValue(new FloatParameter(startChannelMixerGreen.x + value[5].x));
                channelMixer.greenOutGreenIn.SetValue(new FloatParameter(startChannelMixerGreen.y + value[5].y));
                channelMixer.greenOutBlueIn.SetValue(new FloatParameter(startChannelMixerGreen.z + value[5].z));

                channelMixer.blueOutRedIn.SetValue(new FloatParameter(startChannelMixerBlue.x + value[6].x));
                channelMixer.blueOutGreenIn.SetValue(new FloatParameter(startChannelMixerBlue.y + value[6].y));
                channelMixer.blueOutBlueIn.SetValue(new FloatParameter(startChannelMixerBlue.z + value[6].z));
            }

            if (chromaticAberration && CheckFlag(Overrides.ChromaticAberration)) chromaticAberration.intensity.SetValue(new FloatParameter(startChromaticAberration + value[7].x));

            if (colorAdjustments && CheckFlag(Overrides.ColorAdjustments))
            {
                colorAdjustments.postExposure.SetValue(new FloatParameter(startPostExposure + value[8].x));
                colorAdjustments.contrast.SetValue(new FloatParameter(startContrast + value[9].x));
                colorAdjustments.hueShift.SetValue(new FloatParameter(startHueShift + value[10].x));
                colorAdjustments.saturation.SetValue(new FloatParameter(startSaturation + value[11].x));
            }

            if (filmGrain && CheckFlag(Overrides.FilmGrain)) filmGrain.intensity.SetValue(new FloatParameter(startFilmGrain + value[12].x));

            if (lensDistortion && CheckFlag(Overrides.LensDistortion))
            {
                lensDistortion.intensity.SetValue(new FloatParameter(startLensDistortionIntensity + value[13].x));
                lensDistortion.xMultiplier.SetValue(new FloatParameter(startLensDistortionXYMultiplier.x + value[14].x));
                lensDistortion.yMultiplier.SetValue(new FloatParameter(startLensDistortionXYMultiplier.y + value[14].y));
                lensDistortion.center.SetValue(new Vector2Parameter(new Vector2(startLensDistortionCenter.x + value[15].x, startLensDistortionCenter.y + value[15].y)));
                lensDistortion.scale.SetValue(new FloatParameter(startLensDistortionScale + value[16].x));
            }

            if (motionBlur && CheckFlag(Overrides.MotionBlur)) motionBlur.intensity.SetValue(new FloatParameter(startMotionBlur + value[17].x));

            if (paniniProjection && CheckFlag(Overrides.PaniniProjection))
            {
                paniniProjection.distance.SetValue(new FloatParameter(startPaniniProjectionDistance + value[18].x));
                paniniProjection.cropToFit.SetValue(new FloatParameter(startPaniniProjectionCrop + value[19].x));
            }

            if (splitToning && CheckFlag(Overrides.SplitToning)) splitToning.balance.SetValue(new FloatParameter(startSplitToningBalance + value[20].x));

            if (vignette && CheckFlag(Overrides.Vignette))
            {
                vignette.center.SetValue(new Vector2Parameter(new Vector2(startVignetteCenter.x + value[21].x, startVignetteCenter.y + value[21].y)));
                vignette.intensity.SetValue(new FloatParameter(startVignetteIntensity + value[22].x));
                vignette.smoothness.SetValue(new FloatParameter(startVignetteSmoothness + value[23].x));
#if RENDER_HDRP && !RENDER_UNIVERSAL
                vignette.roundness.SetValue(new FloatParameter(startVignetteRoundness + value[26].x));
#endif
            }

            if (whiteBalance && CheckFlag(Overrides.WhiteBalance))
            {
                whiteBalance.temperature.SetValue(new FloatParameter(startWhiteBalanceTemperature + value[24].x));
                whiteBalance.tint.SetValue(new FloatParameter(startWhiteBalanceTint + value[25].x));
            }
#if RENDER_UNIVERSAL && !RENDER_HDRP
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldGaussian))
            {
                depthOfField.gaussianStart.SetValue(new FloatParameter(startDepthOfFieldGaussianStartEnd.x + value[26].x));
                depthOfField.gaussianEnd.SetValue(new FloatParameter(startDepthOfFieldGaussianStartEnd.y + value[26].y));
                depthOfField.gaussianMaxRadius.SetValue(new FloatParameter(startDepthOfFieldGaussianMaxRadius + value[27].x));
            }

            if (depthOfField && CheckFlag(Overrides.DepthOfFieldBokeh))
            {
                depthOfField.focusDistance.SetValue(new FloatParameter(startDepthOfFieldBokehFocusDistance + value[28].x));
                depthOfField.focalLength.SetValue(new FloatParameter(startDepthOfFieldBokehFocalLength + value[29].x));
                depthOfField.aperture.SetValue(new FloatParameter(startDepthOfFieldBokehAperture + value[30].x));
            }

            if (colorLookup && CheckFlag(Overrides.ColorLookup)) colorLookup.contribution.SetValue(new FloatParameter(startColorLookup + value[31].x));
#elif RENDER_HDRP && !RENDER_UNIVERSAL
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldPhysical))
            {
                depthOfField.focusDistance.SetValue(new FloatParameter(startDepthOfFieldPhysicalFocusDistance + value[27].x));
            }
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldManual))
            {
                depthOfField.nearFocusStart.SetValue(new FloatParameter(startDepthOfFieldManualNearStartEnd.x + value[28].x));
                depthOfField.nearFocusEnd.SetValue(new FloatParameter(startDepthOfFieldManualNearStartEnd.y + value[28].y));
                depthOfField.farFocusStart.SetValue(new FloatParameter(startDepthOfFieldManualFarStartEnd.x + value[29].x));
                depthOfField.farFocusEnd.SetValue(new FloatParameter(startDepthOfFieldManualFarStartEnd.y + value[29].y));
            }

            if (fog && CheckFlag(Overrides.Fog))
            {
                fog.meanFreePath.SetValue(new FloatParameter(startFogAttenuation + value[30].x));
                fog.baseHeight.SetValue(new FloatParameter(startFogBaseHeight + value[31].x));
                fog.maximumHeight.SetValue(new FloatParameter(startFogMaxHeight + value[32].x));
            }
#endif

#if SMOOTHPOSTPROCESSING
            if (blur && CheckFlag(SmoothPostProcessingOverrides.Blur)) blur.range.SetValue(new FloatParameter(startBlur + value[33].x));

            if (displace && CheckFlag(SmoothPostProcessingOverrides.Displace)) displace.intensity.SetValue(new FloatParameter(startDisplace + value[34].x));

            if (edgeDetection && CheckFlag(SmoothPostProcessingOverrides.EdgeDetection)) edgeDetection.blend.SetValue(new FloatParameter(startEdgeDetection + value[35].x));

            if (glitch && CheckFlag(SmoothPostProcessingOverrides.Glitch)) glitch.glitchIntensity.SetValue(new FloatParameter(startGlitch + value[36].x));

            if (invert && CheckFlag(SmoothPostProcessingOverrides.Invert)) invert.invert.SetValue(new FloatParameter(startInvert + value[37].x));

            if (kaleidoscope && CheckFlag(SmoothPostProcessingOverrides.Kaleidoscope)) kaleidoscope.rotation.SetValue(new FloatParameter(startKaleidoscope + value[38].x));

            if (monitor && CheckFlag(SmoothPostProcessingOverrides.Monitor))
            {
                monitor.scanlineIntensity.SetValue(new FloatParameter(startMonitorScanline + value[39].x));
                monitor.noiseIntensity.SetValue(new FloatParameter(startMonitorNoise + value[40].x));
            }

            if (nightVision && CheckFlag(SmoothPostProcessingOverrides.NightVision))
            {
                nightVision.brightness.SetValue(new FloatParameter(startNightVisionBrightness + value[41].x));
                nightVision.flickerIntensity.SetValue(new FloatParameter(startNightVisionFlickerIntensity + value[42].x));
            }

            if (pixelation && CheckFlag(SmoothPostProcessingOverrides.Pixelation)) pixelation.pixelSize.SetValue(new Vector2Parameter(new Vector2(startPixelSize.x + value[43].x, startPixelSize.y + value[43].y)));

            if (rgbSplit && CheckFlag(SmoothPostProcessingOverrides.RGBSplit))
            {
                rgbSplit.offsetR.SetValue(new Vector2Parameter(new Vector2(startRGBSplitR.x + value[44].x, startRGBSplitR.y + value[44].y)));
                rgbSplit.offsetG.SetValue(new Vector2Parameter(new Vector2(startRGBSplitG.x + value[45].x, startRGBSplitG.y + value[45].y)));
                rgbSplit.offsetB.SetValue(new Vector2Parameter(new Vector2(startRGBSplitB.x + value[46].x, startRGBSplitB.y + value[46].y)));
            }

            if (rotBlur && CheckFlag(SmoothPostProcessingOverrides.RotBlur))
            {
                rotBlur.strength.SetValue(new FloatParameter(startRotBlur + value[47].x));
                rotBlur.angle.SetValue(new FloatParameter(startRotBlurAngle + value[48].x));
                rotBlur.rotationCenter.SetValue(new Vector2Parameter(new Vector2(startRotBlurCenter.x + value[49].x, startRotBlurCenter.y + value[49].y)));
            }

            if (solarize && CheckFlag(SmoothPostProcessingOverrides.Solarize))
            {
                solarize.intensity.SetValue(new Vector3Parameter(new Vector3(startSolarizeIntensity.x + value[50].x, startSolarizeIntensity.y + value[50].y, startSolarizeIntensity.z + value[50].z)));
                solarize.threshold.SetValue(new FloatParameter(startSolarizeThreshold + value[51].x));
                solarize.smoothness.SetValue(new FloatParameter(startSolarizeSmoothness + value[52].x));
            }

            if (thermalVision && CheckFlag(SmoothPostProcessingOverrides.ThermalVision))
            {
                thermalVision.brightness.SetValue(new FloatParameter(startThermalVisionBrightness + value[53].x));
                thermalVision.contrast.SetValue(new FloatParameter(startThermalVisionContrast + value[54].x));
            }

            if (tint && CheckFlag(SmoothPostProcessingOverrides.Tint))
            {
                tint.vignette.SetValue(new FloatParameter(startTintVignetteIntensity + value[55].x));
                tint.noiseIntensity.SetValue(new FloatParameter(startTintNoiseIntensity + value[56].x));
            }

            if (twirl && CheckFlag(SmoothPostProcessingOverrides.Twirl))
            {
                twirl.strength.SetValue(new FloatParameter(startTwirlStrength + value[57].x));
                twirl.twirlCenter.SetValue(new Vector2Parameter(new Vector2(startTwirlCenter.x + value[58].x, startTwirlCenter.y + value[58].y)));
                twirl.radius.SetValue(new FloatParameter(startTwirlRadius + value[59].x));
                twirl.vignette.SetValue(new FloatParameter(startTwirlVignette + value[60].x));
            }

            if (twitch && CheckFlag(SmoothPostProcessingOverrides.Twitch)) twitch.offset.SetValue(new Vector2Parameter(new Vector2(startTwitchOffset.x + value[61].x, startTwitchOffset.y + value[61].y)));

            if (waveWarp && CheckFlag(SmoothPostProcessingOverrides.WaveWarp))
            {
                waveWarp.amplitude.SetValue(new FloatParameter(startWaveWarpAmplitude + value[62].x));
                waveWarp.vignette.SetValue(new FloatParameter(startWaveWarpVignette + value[63].x));
            }

            if (zoomBlur && CheckFlag(SmoothPostProcessingOverrides.ZoomBlur))
            {
                zoomBlur.strength.SetValue(new FloatParameter(startZoomBlur + value[64].x));
                zoomBlur.zoomCenter.SetValue(new Vector2Parameter(new Vector2(startZoomBlurCenter.x + value[65].x, startZoomBlurCenter.y + value[65].y)));
            }

#if RENDER_UNIVERSAL && !RENDER_HDRP
            if (gaussianBlur && CheckFlag(SmoothPostProcessingOverrides.GaussianBlur)) gaussianBlur.strength.SetValue(new FloatParameter(startGaussianBlur + value[32].x));

            if (noiseBloom && CheckFlag(SmoothPostProcessingOverrides.NoiseBloom))
            {
                noiseBloom.noiseIntensity.SetValue(new FloatParameter(startNoiseBloomStrength + value[66].x));
                noiseBloom.threshold.SetValue(new FloatParameter(startNoiseBloomThreshold + value[67].x));
                noiseBloom.range.SetValue(new FloatParameter(startNoiseBloomRange + value[68].x));
            }
#endif
#endif

#endif
        }

        internal override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if (preset is SmoothShakePostProcessingPreset smoothShakePostProcessingPreset)
            {
                timeSettings = smoothShakePostProcessingPreset.timeSettings;

                weightShake.Clear();
                weightShake.AddRange(smoothShakePostProcessingPreset.weightShake);
                bloomThresholdShake.Clear();
                bloomThresholdShake.AddRange(smoothShakePostProcessingPreset.bloomThresholdShake);
                bloomIntensityShake.Clear();
                bloomIntensityShake.AddRange(smoothShakePostProcessingPreset.bloomIntensityShake);
                bloomScatterShake.Clear();
                bloomScatterShake.AddRange(smoothShakePostProcessingPreset.bloomScatterShake);

                channelMixerRedShake.Clear();
                channelMixerRedShake.AddRange(smoothShakePostProcessingPreset.channelMixerRedShake);
                channelMixerGreenShake.Clear();
                channelMixerGreenShake.AddRange(smoothShakePostProcessingPreset.channelMixerGreenShake);
                channelMixerBlueShake.Clear();
                channelMixerBlueShake.AddRange(smoothShakePostProcessingPreset.channelMixerBlueShake);

                chromaticAberrationShake.Clear();
                chromaticAberrationShake.AddRange(smoothShakePostProcessingPreset.chromaticAberrationShake);

                postExposureShake.Clear();
                postExposureShake.AddRange(smoothShakePostProcessingPreset.postExposureShake);
                contrastShake.Clear();
                contrastShake.AddRange(smoothShakePostProcessingPreset.contrastShake);
                hueShiftShake.Clear();
                hueShiftShake.AddRange(smoothShakePostProcessingPreset.hueShiftShake);
                saturationShake.Clear();
                saturationShake.AddRange(smoothShakePostProcessingPreset.saturationShake);

                filmGrainShake.Clear();
                filmGrainShake.AddRange(smoothShakePostProcessingPreset.filmGrainShake);

                lensDistortionIntensityShake.Clear();
                lensDistortionIntensityShake.AddRange(smoothShakePostProcessingPreset.lensDistortionIntensityShake);
                lensDistortionXYMultiplierShake.Clear();
                lensDistortionXYMultiplierShake.AddRange(smoothShakePostProcessingPreset.lensDistortionXYMultiplierShake);
                lensDistortionCenterShake.Clear();
                lensDistortionCenterShake.AddRange(smoothShakePostProcessingPreset.lensDistortionCenterShake);
                lensDistortionScaleShake.Clear();
                lensDistortionScaleShake.AddRange(smoothShakePostProcessingPreset.lensDistortionScaleShake);

                motionBlurShake.Clear();
                motionBlurShake.AddRange(smoothShakePostProcessingPreset.motionBlurShake);

                paniniProjectionDistanceShake.Clear();
                paniniProjectionDistanceShake.AddRange(smoothShakePostProcessingPreset.paniniProjectionDistanceShake);
                paniniProjectionCropShake.Clear();
                paniniProjectionCropShake.AddRange(smoothShakePostProcessingPreset.paniniProjectionCropShake);

                splitToningBalanceShake.Clear();
                splitToningBalanceShake.AddRange(smoothShakePostProcessingPreset.splitToningBalanceShake);

                vignetteCenterShake.Clear();
                vignetteCenterShake.AddRange(smoothShakePostProcessingPreset.vignetteCenterShake);
                vignetteIntensityShake.Clear();
                vignetteIntensityShake.AddRange(smoothShakePostProcessingPreset.vignetteIntensityShake);
                vignetteSmoothnessShake.Clear();
                vignetteSmoothnessShake.AddRange(smoothShakePostProcessingPreset.vignetteSmoothnessShake);
#if RENDER_HDRP && !RENDER_UNIVERSAL
                vignetteRoundnessShake.Clear();
                vignetteRoundnessShake.AddRange(smoothShakePostProcessingPreset.vignetteRoundnessShake);
#endif

                whiteBalanceTemperatureShake.Clear();
                whiteBalanceTemperatureShake.AddRange(smoothShakePostProcessingPreset.whiteBalanceTemperatureShake);
                whiteBalanceTintShake.Clear();
                whiteBalanceTintShake.AddRange(smoothShakePostProcessingPreset.whiteBalanceTintShake);

#if RENDER_UNIVERSAL && !RENDER_HDRP
                depthOfFieldGaussianStartEndShake.Clear();
                depthOfFieldGaussianStartEndShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldGaussianStartEndShake);
                depthOfFieldGaussianMaxRadiusShake.Clear();
                depthOfFieldGaussianMaxRadiusShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldGaussianMaxRadiusShake);

                depthOfFieldBokehFocusDistanceShake.Clear();
                depthOfFieldBokehFocusDistanceShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldBokehFocusDistanceShake);
                depthOfFieldBokehFocalLengthShake.Clear();
                depthOfFieldBokehFocalLengthShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldBokehFocalLengthShake);
                depthOfFieldBokehApertureShake.Clear();
                depthOfFieldBokehApertureShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldBokehApertureShake);

                colorLookupShake.Clear();
                colorLookupShake.AddRange(smoothShakePostProcessingPreset.colorLookupShake);
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                depthOfFieldPhysicalFocusDistanceShake.Clear();
                depthOfFieldPhysicalFocusDistanceShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldPhysicalFocusDistanceShake);
                depthOfFieldManualNearStartEndShake.Clear();
                depthOfFieldManualNearStartEndShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldManualNearStartEndShake);
                depthOfFieldManualFarStartEndShake.Clear();
                depthOfFieldManualFarStartEndShake.AddRange(smoothShakePostProcessingPreset.depthOfFieldManualFarStartEndShake);

                fogAttenuationShake.Clear();
                fogAttenuationShake.AddRange(smoothShakePostProcessingPreset.fogAttenuationShake);
                fogBaseHeightShake.Clear();
                fogBaseHeightShake.AddRange(smoothShakePostProcessingPreset.fogBaseHeightShake);
                fogMaxHeightShake.Clear();
                fogMaxHeightShake.AddRange(smoothShakePostProcessingPreset.fogMaxHeightShake);
#endif

#if SMOOTHPOSTPROCESSING
                blurShake.Clear();
                blurShake.AddRange(smoothShakePostProcessingPreset.blurShake);
                displaceShake.Clear();
                displaceShake.AddRange(smoothShakePostProcessingPreset.displaceShake);
                edgeDetectionShake.Clear();
                edgeDetectionShake.AddRange(smoothShakePostProcessingPreset.edgeDetectionShake);
                glitchIntensityShake.Clear();
                glitchIntensityShake.AddRange(smoothShakePostProcessingPreset.glitchIntensityShake);
                invertIntensityShake.Clear();
                invertIntensityShake.AddRange(smoothShakePostProcessingPreset.invertIntensityShake);
                kaleidoscopeShake.Clear();
                kaleidoscopeShake.AddRange(smoothShakePostProcessingPreset.kaleidoscopeShake);
                monitorScanlineShake.Clear();
                monitorScanlineShake.AddRange(smoothShakePostProcessingPreset.monitorScanlineShake);
                monitorNoiseShake.Clear();
                monitorNoiseShake.AddRange(smoothShakePostProcessingPreset.monitorNoiseShake);
                nightVisionBrightnessShake.Clear();
                nightVisionBrightnessShake.AddRange(smoothShakePostProcessingPreset.nightVisionBrightnessShake);
                nightVisionFlickerIntensityShake.Clear();
                nightVisionFlickerIntensityShake.AddRange(smoothShakePostProcessingPreset.nightVisionFlickerIntensityShake);
                pixelSizeShake.Clear();
                pixelSizeShake.AddRange(smoothShakePostProcessingPreset.pixelSizeShake);
                rgbSplitShakeR.Clear();
                rgbSplitShakeR.AddRange(smoothShakePostProcessingPreset.rgbSplitShakeR);
                rgbSplitShakeG.Clear();
                rgbSplitShakeG.AddRange(smoothShakePostProcessingPreset.rgbSplitShakeG);
                rgbSplitShakeB.Clear();
                rgbSplitShakeB.AddRange(smoothShakePostProcessingPreset.rgbSplitShakeB);
                rotBlurShake.Clear();
                solarizeIntensityShake.Clear();
                solarizeIntensityShake.AddRange(smoothShakePostProcessingPreset.solarizeIntensityShake);
                solarizeThresholdShake.Clear();
                solarizeThresholdShake.AddRange(smoothShakePostProcessingPreset.solarizeThresholdShake);
                solarizeSmoothnessShake.Clear();
                solarizeSmoothnessShake.AddRange(smoothShakePostProcessingPreset.solarizeSmoothnessShake);
                thermalVisionBrightnessShake.Clear();
                thermalVisionBrightnessShake.AddRange(smoothShakePostProcessingPreset.thermalVisionBrightnessShake);
                thermalVisionContrastShake.Clear();
                thermalVisionContrastShake.AddRange(smoothShakePostProcessingPreset.thermalVisionContrastShake);
                tintVignetteIntensityShake.Clear();
                tintVignetteIntensityShake.AddRange(smoothShakePostProcessingPreset.tintVignetteIntensityShake);
                tintNoiseIntensityShake.Clear();
                tintNoiseIntensityShake.AddRange(smoothShakePostProcessingPreset.tintNoiseIntensityShake);
                twirlStrengthShake.Clear();
                twirlStrengthShake.AddRange(smoothShakePostProcessingPreset.twirlStrengthShake);
                twirlCenterShake.Clear();
                twirlCenterShake.AddRange(smoothShakePostProcessingPreset.twirlCenterShake);
                twirlRadiusShake.Clear();
                twirlRadiusShake.AddRange(smoothShakePostProcessingPreset.twirlRadiusShake);
                twirlVignetteShake.Clear();
                twirlVignetteShake.AddRange(smoothShakePostProcessingPreset.twirlVignetteShake);
                twitchOffsetShake.Clear();
                twitchOffsetShake.AddRange(smoothShakePostProcessingPreset.twitchOffsetShake);
                waveWarpAmplitudeShake.Clear();
                waveWarpAmplitudeShake.AddRange(smoothShakePostProcessingPreset.waveWarpAmplitudeShake);
                waveWarpVignetteShake.Clear();
                waveWarpVignetteShake.AddRange(smoothShakePostProcessingPreset.waveWarpVignetteShake);
                zoomBlurShake.Clear();
                zoomBlurShake.AddRange(smoothShakePostProcessingPreset.zoomBlurShake);
#if RENDER_UNIVERSAL && !RENDER_HDRP
                gaussianBlurShake.Clear();
                gaussianBlurShake.AddRange(smoothShakePostProcessingPreset.gaussianBlurShake);
                noiseBloomStrengthShake.Clear();
                noiseBloomStrengthShake.AddRange(smoothShakePostProcessingPreset.noiseBloomStrengthShake);
#endif
#endif
            }
        }

        internal override void ResetDefaultValues()
        {
#if RENDER_UNIVERSAL || RENDER_HDRP
            if (!volume) return;

            if (CheckFlag(Overrides.Weight)) volume.weight = startWeight;

            if (bloom && CheckFlag(Overrides.Bloom))
            {
                bloom.threshold.SetValue(new FloatParameter(startBloomThreshold));
                bloom.intensity.SetValue(new FloatParameter(startBloomIntensity));
                bloom.scatter.SetValue(new FloatParameter(startBloomScatter));
            }

            if (channelMixer && CheckFlag(Overrides.ChannelMixer))
            {
                channelMixer.redOutRedIn.SetValue(new FloatParameter(startChannelMixerRed.x));
                channelMixer.redOutGreenIn.SetValue(new FloatParameter(startChannelMixerRed.y));
                channelMixer.redOutBlueIn.SetValue(new FloatParameter(startChannelMixerRed.z));

                channelMixer.greenOutRedIn.SetValue(new FloatParameter(startChannelMixerGreen.x));
                channelMixer.greenOutGreenIn.SetValue(new FloatParameter(startChannelMixerGreen.y));
                channelMixer.greenOutBlueIn.SetValue(new FloatParameter(startChannelMixerGreen.z));

                channelMixer.blueOutRedIn.SetValue(new FloatParameter(startChannelMixerBlue.x));
                channelMixer.blueOutGreenIn.SetValue(new FloatParameter(startChannelMixerBlue.y));
                channelMixer.blueOutBlueIn.SetValue(new FloatParameter(startChannelMixerBlue.z));
            }

            if (chromaticAberration && CheckFlag(Overrides.ChromaticAberration)) chromaticAberration.intensity.SetValue(new FloatParameter(startChromaticAberration));

            if (colorAdjustments && CheckFlag(Overrides.ColorAdjustments))
            {
                colorAdjustments.postExposure.SetValue(new FloatParameter(startPostExposure));
                colorAdjustments.contrast.SetValue(new FloatParameter(startContrast));
                colorAdjustments.hueShift.SetValue(new FloatParameter(startHueShift));
                colorAdjustments.saturation.SetValue(new FloatParameter(startSaturation));
            }

            if (filmGrain && CheckFlag(Overrides.FilmGrain)) filmGrain.intensity.SetValue(new FloatParameter(startFilmGrain));

            if (lensDistortion && CheckFlag(Overrides.LensDistortion))
            {
                lensDistortion.intensity.SetValue(new FloatParameter(startLensDistortionIntensity));
                lensDistortion.xMultiplier.SetValue(new FloatParameter(startLensDistortionXYMultiplier.x));
                lensDistortion.yMultiplier.SetValue(new FloatParameter(startLensDistortionXYMultiplier.y));
                lensDistortion.center.SetValue(new Vector2Parameter(new Vector2(startLensDistortionCenter.x, startLensDistortionCenter.y)));
                lensDistortion.scale.SetValue(new FloatParameter(startLensDistortionScale));
            }

            if (motionBlur && CheckFlag(Overrides.MotionBlur)) motionBlur.intensity.SetValue(new FloatParameter(startMotionBlur));

            if (paniniProjection && CheckFlag(Overrides.PaniniProjection))
            {
                paniniProjection.distance.SetValue(new FloatParameter(startPaniniProjectionDistance));
                paniniProjection.cropToFit.SetValue(new FloatParameter(startPaniniProjectionCrop));
            }

            if (splitToning && CheckFlag(Overrides.SplitToning)) splitToning.balance.SetValue(new FloatParameter(startSplitToningBalance));

            if (vignette && CheckFlag(Overrides.Vignette))
            {
                vignette.center.SetValue(new Vector2Parameter(startVignetteCenter));
                vignette.intensity.SetValue(new FloatParameter(startVignetteIntensity));
                vignette.smoothness.SetValue(new FloatParameter(startVignetteSmoothness));
#if RENDER_HDRP && !RENDER_UNIVERSAL
                vignette.roundness.SetValue(new FloatParameter(startVignetteRoundness));
#endif
            }

            if (whiteBalance && CheckFlag(Overrides.WhiteBalance))
            {
                whiteBalance.temperature.SetValue(new FloatParameter(startWhiteBalanceTemperature));
                whiteBalance.tint.SetValue(new FloatParameter(startWhiteBalanceTint));
            }

#if RENDER_UNIVERSAL && !RENDER_HDRP
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldGaussian))
            {
                depthOfField.gaussianStart.SetValue(new FloatParameter(startDepthOfFieldGaussianStartEnd.x));
                depthOfField.gaussianEnd.SetValue(new FloatParameter(startDepthOfFieldGaussianStartEnd.y));
                depthOfField.gaussianMaxRadius.SetValue(new FloatParameter(startDepthOfFieldGaussianMaxRadius));
            }

            if (depthOfField && CheckFlag(Overrides.DepthOfFieldBokeh))
            {
                depthOfField.focusDistance.SetValue(new FloatParameter(startDepthOfFieldBokehFocusDistance));
                depthOfField.focalLength.SetValue(new FloatParameter(startDepthOfFieldBokehFocalLength));
                depthOfField.aperture.SetValue(new FloatParameter(startDepthOfFieldBokehAperture));
            }

            if (colorLookup && CheckFlag(Overrides.ColorLookup)) colorLookup.contribution.SetValue(new FloatParameter(startColorLookup));
#elif RENDER_HDRP && !RENDER_UNIVERSAL
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldPhysical))
            {
                depthOfField.focusDistance.SetValue(new FloatParameter(startDepthOfFieldPhysicalFocusDistance));
            }
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldManual))
            {
                depthOfField.nearFocusStart.SetValue(new FloatParameter(startDepthOfFieldManualNearStartEnd.x));
                depthOfField.nearFocusEnd.SetValue(new FloatParameter(startDepthOfFieldManualNearStartEnd.y));
                depthOfField.farFocusStart.SetValue(new FloatParameter(startDepthOfFieldManualFarStartEnd.x));
                depthOfField.farFocusEnd.SetValue(new FloatParameter(startDepthOfFieldManualFarStartEnd.y));
            }

            if(fog && CheckFlag(Overrides.Fog))
            {
                fog.meanFreePath.SetValue(new FloatParameter(startFogAttenuation));
                fog.baseHeight.SetValue(new FloatParameter(startFogBaseHeight));
                fog.maximumHeight.SetValue(new FloatParameter(startFogMaxHeight));
            }
#endif

#if SMOOTHPOSTPROCESSING
            if (blur && CheckFlag(SmoothPostProcessingOverrides.Blur)) blur.range.SetValue(new FloatParameter(startBlur));
            if (displace && CheckFlag(SmoothPostProcessingOverrides.Displace)) displace.intensity.SetValue(new FloatParameter(startDisplace));
            if (edgeDetection && CheckFlag(SmoothPostProcessingOverrides.EdgeDetection)) edgeDetection.blend.SetValue(new FloatParameter(startEdgeDetection));
            if (glitch && CheckFlag(SmoothPostProcessingOverrides.Glitch)) glitch.glitchIntensity.SetValue(new FloatParameter(startGlitch));
            if (invert && CheckFlag(SmoothPostProcessingOverrides.Invert)) invert.invert.SetValue(new FloatParameter(startInvert));
            if (kaleidoscope && CheckFlag(SmoothPostProcessingOverrides.Kaleidoscope)) kaleidoscope.rotation.SetValue(new FloatParameter(startKaleidoscope));
            if (monitor && CheckFlag(SmoothPostProcessingOverrides.Monitor))
            {
                monitor.scanlineIntensity.SetValue(new FloatParameter(startMonitorScanline));
                monitor.noiseIntensity.SetValue(new FloatParameter(startMonitorNoise));
            }
            if (nightVision && CheckFlag(SmoothPostProcessingOverrides.NightVision))
            {
                nightVision.brightness.SetValue(new FloatParameter(startNightVisionBrightness));
                nightVision.flickerIntensity.SetValue(new FloatParameter(startNightVisionFlickerIntensity));
            }
            if (pixelation && CheckFlag(SmoothPostProcessingOverrides.Pixelation)) pixelation.pixelSize.SetValue(new Vector2Parameter(startPixelSize));
            if (rgbSplit && CheckFlag(SmoothPostProcessingOverrides.RGBSplit))
            {
                rgbSplit.offsetR.SetValue(new Vector2Parameter(startRGBSplitR));
                rgbSplit.offsetG.SetValue(new Vector2Parameter(startRGBSplitG));
                rgbSplit.offsetB.SetValue(new Vector2Parameter(startRGBSplitB));
            }
            if (rotBlur && CheckFlag(SmoothPostProcessingOverrides.RotBlur))
            {
                rotBlur.strength.SetValue(new FloatParameter(startRotBlur));
                rotBlur.angle.SetValue(new FloatParameter(startRotBlurAngle));
                rotBlur.rotationCenter.SetValue(new Vector2Parameter(startRotBlurCenter));
            }
            if (solarize && CheckFlag(SmoothPostProcessingOverrides.Solarize))
            {
                solarize.intensity.SetValue(new Vector3Parameter(startSolarizeIntensity));
                solarize.threshold.SetValue(new FloatParameter(startSolarizeThreshold));
                solarize.smoothness.SetValue(new FloatParameter(startSolarizeSmoothness));
            }
            if (thermalVision && CheckFlag(SmoothPostProcessingOverrides.ThermalVision))
            {
                thermalVision.brightness.SetValue(new FloatParameter(startThermalVisionBrightness));
                thermalVision.contrast.SetValue(new FloatParameter(startThermalVisionContrast));
            }
            if (tint && CheckFlag(SmoothPostProcessingOverrides.Tint))
            {
                tint.vignette.SetValue(new FloatParameter(startTintVignetteIntensity));
                tint.noiseIntensity.SetValue(new FloatParameter(startTintNoiseIntensity));
            }
            if (twirl && CheckFlag(SmoothPostProcessingOverrides.Twirl))
            {
                twirl.strength.SetValue(new FloatParameter(startTwirlStrength));
                twirl.twirlCenter.SetValue(new Vector2Parameter(startTwirlCenter));
                twirl.radius.SetValue(new FloatParameter(startTwirlRadius));
                twirl.vignette.SetValue(new FloatParameter(startTwirlVignette));
            }
            if (twitch && CheckFlag(SmoothPostProcessingOverrides.Twitch)) twitch.offset.SetValue(new Vector2Parameter(startTwitchOffset));
            if (waveWarp && CheckFlag(SmoothPostProcessingOverrides.WaveWarp))
            {
                waveWarp.amplitude.SetValue(new FloatParameter(startWaveWarpAmplitude));
                waveWarp.vignette.SetValue(new FloatParameter(startWaveWarpVignette));
            }
            if (zoomBlur && CheckFlag(SmoothPostProcessingOverrides.ZoomBlur))
            {
                zoomBlur.strength.SetValue(new FloatParameter(startZoomBlur));
                zoomBlur.zoomCenter.SetValue(new Vector2Parameter(startZoomBlurCenter));
            }
#if RENDER_UNIVERSAL && !RENDER_HDRP
            if (gaussianBlur && CheckFlag(SmoothPostProcessingOverrides.GaussianBlur)) gaussianBlur.strength.SetValue(new FloatParameter(startGaussianBlur));
            if (noiseBloom && CheckFlag(SmoothPostProcessingOverrides.NoiseBloom))
            {
                noiseBloom.noiseIntensity.SetValue(new FloatParameter(startNoiseBloomStrength));
                noiseBloom.threshold.SetValue(new FloatParameter(startNoiseBloomThreshold));
                noiseBloom.range.SetValue(new FloatParameter(startNoiseBloomRange));
            }
#endif
#endif

#endif

        }

        internal override void SaveDefaultValues()
        {
#if RENDER_UNIVERSAL || RENDER_HDRP
            if (!volume) return;

            if (Application.isPlaying)
            {
#if SMOOTHPOSTPROCESSING
                if (!CheckOverrides(overrides, smoothPostProcessingOverrides)) GetOverrideComponents(volume);
#else
                if (!CheckOverrides(overrides)) GetOverrideComponents(volume);
#endif
            }
            else GetOverrideComponents(volume);

            if (CheckFlag(Overrides.Weight)) startWeight = volume.weight;

            if (bloom && CheckFlag(Overrides.Bloom))
            {
                startBloomThreshold = bloom.threshold.value;
                startBloomIntensity = bloom.intensity.value;
                startBloomScatter = bloom.scatter.value;
            }

            if (channelMixer && CheckFlag(Overrides.ChannelMixer))
            {
                startChannelMixerRed = new Vector3(channelMixer.redOutRedIn.value, channelMixer.redOutGreenIn.value, channelMixer.redOutBlueIn.value);
                startChannelMixerGreen = new Vector3(channelMixer.greenOutRedIn.value, channelMixer.greenOutGreenIn.value, channelMixer.greenOutBlueIn.value);
                startChannelMixerBlue = new Vector3(channelMixer.blueOutRedIn.value, channelMixer.blueOutGreenIn.value, channelMixer.blueOutBlueIn.value);
            }

            if (chromaticAberration && CheckFlag(Overrides.ChromaticAberration)) startChromaticAberration = chromaticAberration.intensity.value;

            if (colorAdjustments && CheckFlag(Overrides.ColorAdjustments))
            {
                startPostExposure = colorAdjustments.postExposure.value;
                startContrast = colorAdjustments.contrast.value;
                startHueShift = colorAdjustments.hueShift.value;
                startSaturation = colorAdjustments.saturation.value;
            }

            if (filmGrain && CheckFlag(Overrides.FilmGrain)) startFilmGrain = filmGrain.intensity.value;

            if (lensDistortion && CheckFlag(Overrides.LensDistortion))
            {
                startLensDistortionIntensity = lensDistortion.intensity.value;
                startLensDistortionXYMultiplier = new Vector3(lensDistortion.xMultiplier.value, lensDistortion.yMultiplier.value, 0);
                startLensDistortionCenter = new Vector3(lensDistortion.center.value.x, lensDistortion.center.value.y, 0);
                startLensDistortionScale = lensDistortion.scale.value;
            }

            if (motionBlur && CheckFlag(Overrides.MotionBlur)) startMotionBlur = motionBlur.intensity.value;

            if (paniniProjection && CheckFlag(Overrides.PaniniProjection))
            {
                startPaniniProjectionDistance = paniniProjection.distance.value;
                startPaniniProjectionCrop = paniniProjection.cropToFit.value;
            }

            if (splitToning && CheckFlag(Overrides.SplitToning)) startSplitToningBalance = splitToning.balance.value;

            if (vignette && CheckFlag(Overrides.Vignette))
            {
                startVignetteCenter = vignette.center.value;
                startVignetteIntensity = vignette.intensity.value;
                startVignetteSmoothness = vignette.smoothness.value;
#if RENDER_HDRP && !RENDER_UNIVERSAL
                startVignetteRoundness = vignette.roundness.value;
#endif
            }

            if (whiteBalance && CheckFlag(Overrides.WhiteBalance))
            {
                startWhiteBalanceTemperature = whiteBalance.temperature.value;
                startWhiteBalanceTint = whiteBalance.tint.value;
            }

#if RENDER_UNIVERSAL && !RENDER_HDRP
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldGaussian))
            {
                startDepthOfFieldGaussianStartEnd = new Vector2(depthOfField.gaussianStart.value, depthOfField.gaussianEnd.value);
                startDepthOfFieldGaussianMaxRadius = depthOfField.gaussianMaxRadius.value;
            }

            if (depthOfField && CheckFlag(Overrides.DepthOfFieldBokeh))
            {
                startDepthOfFieldBokehFocusDistance = depthOfField.focusDistance.value;
                startDepthOfFieldBokehFocalLength = depthOfField.focalLength.value;
                startDepthOfFieldBokehAperture = depthOfField.aperture.value;
            }

            if (colorLookup && CheckFlag(Overrides.ColorLookup)) startColorLookup = colorLookup.contribution.value;
#elif RENDER_HDRP && !RENDER_UNIVERSAL
            if (depthOfField && CheckFlag(Overrides.DepthOfFieldPhysical))
            {
                startDepthOfFieldPhysicalFocusDistance = depthOfField.focusDistance.value;
            }

            if(depthOfField && CheckFlag(Overrides.DepthOfFieldManual))
            {
                startDepthOfFieldManualNearStartEnd = new Vector2(depthOfField.nearFocusStart.value, depthOfField.nearFocusEnd.value);
                startDepthOfFieldManualFarStartEnd = new Vector2(depthOfField.farFocusStart.value, depthOfField.farFocusEnd.value);
            }

            if(fog && CheckFlag(Overrides.Fog))
            {
                startFogAttenuation = fog.meanFreePath.value;
                startFogBaseHeight = fog.baseHeight.value;
                startFogMaxHeight = fog.maximumHeight.value;
            }
#endif

#if SMOOTHPOSTPROCESSING
            if (CheckFlag(SmoothPostProcessingOverrides.Blur) && blur) startBlur = blur.range.value;
            if (CheckFlag(SmoothPostProcessingOverrides.Displace) && displace) startDisplace = displace.intensity.value;
            if (CheckFlag(SmoothPostProcessingOverrides.EdgeDetection) && edgeDetection) startEdgeDetection = edgeDetection.blend.value;
            if (CheckFlag(SmoothPostProcessingOverrides.Glitch) && glitch) startGlitch = glitch.glitchIntensity.value;
            if (CheckFlag(SmoothPostProcessingOverrides.Invert) && invert) startInvert = invert.invert.value;
            if (CheckFlag(SmoothPostProcessingOverrides.Kaleidoscope) && kaleidoscope) startKaleidoscope = kaleidoscope.rotation.value;
            if (CheckFlag(SmoothPostProcessingOverrides.Monitor) && monitor)
            {
                startMonitorScanline = monitor.scanlineIntensity.value;
                startMonitorNoise = monitor.noiseIntensity.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.NightVision) && nightVision)
            {
                startNightVisionBrightness = nightVision.brightness.value;
                startNightVisionFlickerIntensity = nightVision.flickerIntensity.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.Pixelation) && pixelation) startPixelSize = pixelation.pixelSize.value;
            if (CheckFlag(SmoothPostProcessingOverrides.RGBSplit) && rgbSplit)
            {
                startRGBSplitR = rgbSplit.offsetR.value;
                startRGBSplitG = rgbSplit.offsetG.value;
                startRGBSplitB = rgbSplit.offsetB.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.RotBlur) && rotBlur)
            {
                startRotBlur = rotBlur.strength.value;
                startRotBlurAngle = rotBlur.angle.value;
                startRotBlurCenter = rotBlur.rotationCenter.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.Solarize) && solarize)
            {
                startSolarizeIntensity = solarize.intensity.value;
                startSolarizeThreshold = solarize.threshold.value;
                startSolarizeSmoothness = solarize.smoothness.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.ThermalVision) && thermalVision)
            {
                startThermalVisionBrightness = thermalVision.brightness.value;
                startThermalVisionContrast = thermalVision.contrast.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.Tint) && tint)
            {
                startTintVignetteIntensity = tint.vignette.value;
                startTintNoiseIntensity = tint.noiseIntensity.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.Twirl) && twirl)
            {
                startTwirlStrength = twirl.strength.value;
                startTwirlCenter = new Vector2(twirl.twirlCenter.value.x, twirl.twirlCenter.value.y);
                startTwirlRadius = twirl.radius.value;
                startTwirlVignette = twirl.vignette.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.Twitch) && twitch) startTwitchOffset = new Vector2(twitch.offset.value.x, twitch.offset.value.y);
            if (CheckFlag(SmoothPostProcessingOverrides.WaveWarp) && waveWarp)
            {
                startWaveWarpAmplitude = waveWarp.amplitude.value;
                startWaveWarpVignette = waveWarp.vignette.value;
            }
            if (CheckFlag(SmoothPostProcessingOverrides.ZoomBlur) && zoomBlur)
            {
                startZoomBlur = zoomBlur.strength.value;
                startZoomBlurCenter = new Vector2(zoomBlur.zoomCenter.value.x, zoomBlur.zoomCenter.value.y);
            }
#if RENDER_UNIVERSAL && !RENDER_HDRP
            if (CheckFlag(SmoothPostProcessingOverrides.GaussianBlur) && gaussianBlur) startGaussianBlur = gaussianBlur.strength.value;
            if (CheckFlag(SmoothPostProcessingOverrides.NoiseBloom) && noiseBloom)
            {
                startNoiseBloomStrength = noiseBloom.noiseIntensity.value;
                startNoiseBloomThreshold = noiseBloom.threshold.value;
                startNoiseBloomRange = noiseBloom.range.value;
            }
#endif
#endif

#endif
        }

        protected override Shaker[] GetShakers() => null;
    }
#else
#endif
}