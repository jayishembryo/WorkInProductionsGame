using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
#if UNITY_TIMELINE && UNITY_2022
    public class SmoothShakePostProcessingBehaviour : ShakeBehaviourBase
    {
        [Tooltip("Select which settings you want to use")]
        public Overrides overrides;
#if SMOOTHPOSTPROCESSING
        [Tooltip("Select which settings from SmoothShakePro effects you want to use")]
        public SmoothPostProcessingOverrides smoothShakeProOverrides;
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

        public override IEnumerable<Shaker>[] GetMultiShakers()
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
    }
#endif
}