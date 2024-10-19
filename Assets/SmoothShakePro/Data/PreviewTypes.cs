#if UNITY_EDITOR
using System.Collections.Generic;
using System;
using UnityEditor;

namespace SmoothShakePro
{
    //
    internal enum AxisToPreview { X, Y, Z }
    internal static class PreviewTypes
    {
        public static string[] axisOptions = { "X", "Y", "Z" };

        public static void DisplayPreviewShakerDropdown(ShakeBase shakeBase = null, ShakeBasePreset shakeBasePreset = null)
        {
            if (shakeBase)
            {
                switch (shakeBase)
                {
                    case SmoothShake ss:
                        ss.shakeToPreview = (SmoothShake.ShakeToPreview)EditorGUILayout.EnumPopup(ss.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#if CINEMACHINE
                    case SmoothShakeCinemachine ssc:
                        ssc.shakeToPreview = (SmoothShakeCinemachine.ShakeToPreview)EditorGUILayout.EnumPopup(ssc.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#endif
                    case SmoothShakeRigidbody ssr:
                        ssr.shakeToPreview = (SmoothShakeRigidbody.ShakeToPreview)EditorGUILayout.EnumPopup(ssr.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeMaterial ssm:
                        ssm.shakeToPreview = (SmoothShakeMaterial.ShakeToPreview)EditorGUILayout.EnumPopup(ssm.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeLight ssl:
                        ssl.shakeToPreview = (SmoothShakeLight.ShakeToPreview)EditorGUILayout.EnumPopup(ssl.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeAudio ssa:
                        ssa.shakeToPreview = (SmoothShakeAudio.ShakeToPreview)EditorGUILayout.EnumPopup(ssa.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeHapticsGamepad ssh:
                        ssh.shakeToPreview = (SmoothShakeHapticsGamepad.ShakeToPreview)EditorGUILayout.EnumPopup(ssh.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#if UNITY_XR
                    case SmoothShakeHapticsXR sshxr:
                        sshxr.shakeToPreview = (SmoothShakeHapticsXR.ShakeToPreview)EditorGUILayout.EnumPopup(sshxr.shakeToPreview, EditorStyles.toolbarPopup); 
                        break;
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                    case SmoothShakePostProcessing sspp:
                        sspp.shakeToPreview = (SmoothShakePostProcessing.ShakeToPreview)EditorGUILayout.EnumPopup(sspp.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#endif
                    default:
                        throw new Exception("ShakeBase not found");
                }
            }
            else if (shakeBasePreset)
            {
                switch (shakeBasePreset)
                {
                    case SmoothShakePreset ss:
                        ss.shakeToPreview = (SmoothShakePreset.ShakeToPreview)EditorGUILayout.EnumPopup(ss.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#if CINEMACHINE
                    case SmoothShakeCinemachinePreset ssc:
                        ssc.shakeToPreview = (SmoothShakeCinemachinePreset.ShakeToPreview)EditorGUILayout.EnumPopup(ssc.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#endif
                    case SmoothShakeRigidbodyPreset ssr:
                        ssr.shakeToPreview = (SmoothShakeRigidbodyPreset.ShakeToPreview)EditorGUILayout.EnumPopup(ssr.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeMaterialPreset ssm:
                        ssm.shakeToPreview = (SmoothShakeMaterialPreset.ShakeToPreview)EditorGUILayout.EnumPopup(ssm.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeLightPreset ssl:
                        ssl.shakeToPreview = (SmoothShakeLightPreset.ShakeToPreview)EditorGUILayout.EnumPopup(ssl.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeAudioPreset ssa:
                        ssa.shakeToPreview = (SmoothShakeAudioPreset.ShakeToPreview)EditorGUILayout.EnumPopup(ssa.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
                    case SmoothShakeHapticsGamepadPreset ssh: 
                        ssh.shakeToPreview = (SmoothShakeHapticsGamepadPreset.ShakeToPreview)EditorGUILayout.EnumPopup(ssh.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#if UNITY_XR
                    case SmoothShakeHapticsXRPreset sshxr:
                        sshxr.shakeToPreview = (SmoothShakeHapticsXRPreset.ShakeToPreview)EditorGUILayout.EnumPopup(sshxr.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                    case SmoothShakePostProcessingPreset sspp:
                        sspp.shakeToPreview = (SmoothShakePostProcessingPreset.ShakeToPreview)EditorGUILayout.EnumPopup(sspp.shakeToPreview, EditorStyles.toolbarPopup);
                        break;
#endif
                    default:
                        throw new Exception("ShakeBasePreset not found");
                }
            }
        }

        public static IEnumerable<Shaker> ShakerListForPreview(ShakeBase shakeBase = null, ShakeBasePreset shakeBasePreset = null)
        {
            if (shakeBase)
            {
                return shakeBase switch
                {
                    SmoothShake ss => ss.shakeToPreview switch
                    {
                        SmoothShake.ShakeToPreview.Position => ss.positionShake,
                        SmoothShake.ShakeToPreview.Rotation => ss.rotationShake,
                        SmoothShake.ShakeToPreview.Scale => ss.scaleShake,
                        SmoothShake.ShakeToPreview.FOV => ss.FOVShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if CINEMACHINE
                    SmoothShakeCinemachine ssc => ssc.shakeToPreview switch
                    {
                        SmoothShakeCinemachine.ShakeToPreview.Position => ssc.positionShake,
                        SmoothShakeCinemachine.ShakeToPreview.Rotation => ssc.rotationShake,
                        SmoothShakeCinemachine.ShakeToPreview.FOV => ssc.FOVShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    SmoothShakeRigidbody ssr => ssr.shakeToPreview switch
                    {
                        SmoothShakeRigidbody.ShakeToPreview.Force => ssr.forceShake,
                        SmoothShakeRigidbody.ShakeToPreview.Torque => ssr.torqueShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeMaterial ssm => ssm.shakeToPreview switch
                    {
                        SmoothShakeMaterial.ShakeToPreview.Float => ssm.propertyToShake.floatShake,
                        SmoothShakeMaterial.ShakeToPreview.Vector => ssm.propertyToShake.vectorShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeLight ssl => ssl.shakeToPreview switch
                    {
                        SmoothShakeLight.ShakeToPreview.Intensity => ssl.intensityShake,
                        SmoothShakeLight.ShakeToPreview.Range => ssl.rangeShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeAudio ssa => ssa.shakeToPreview switch
                    {
                        SmoothShakeAudio.ShakeToPreview.Volume => ssa.volumeShake,
                        SmoothShakeAudio.ShakeToPreview.Pan => ssa.panShake,
                        SmoothShakeAudio.ShakeToPreview.Pitch => ssa.pitchShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeHapticsGamepad ssh => ssh.shakeToPreview switch
                    {
                        SmoothShakeHapticsGamepad.ShakeToPreview.LowFrequencyMotor => ssh.lowFrequencyMotorShake,
                        SmoothShakeHapticsGamepad.ShakeToPreview.HighFrequencyMotor => ssh.highFrequencyMotorShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if UNITY_XR
                    SmoothShakeHapticsXR sshxr => sshxr.shakeToPreview switch
                    {
                        SmoothShakeHapticsXR.ShakeToPreview.leftController => sshxr.leftControllerShake,
                        SmoothShakeHapticsXR.ShakeToPreview.rightController => sshxr.rightControllerShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                    SmoothShakePostProcessing sspp => sspp.shakeToPreview switch
                    {
                        SmoothShakePostProcessing.ShakeToPreview.Weight => sspp.weightShake,
                        SmoothShakePostProcessing.ShakeToPreview.BloomThreshold => sspp.bloomThresholdShake,
                        SmoothShakePostProcessing.ShakeToPreview.BloomIntensity => sspp.bloomIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.BloomScatter => sspp.bloomScatterShake,
                        SmoothShakePostProcessing.ShakeToPreview.ChannelMixerRed => sspp.channelMixerRedShake,
                        SmoothShakePostProcessing.ShakeToPreview.ChannelMixerBlue => sspp.channelMixerBlueShake,
                        SmoothShakePostProcessing.ShakeToPreview.ChannelMixerGreen => sspp.channelMixerGreenShake,
                        SmoothShakePostProcessing.ShakeToPreview.ChromaticAberration => sspp.chromaticAberrationShake,
                        SmoothShakePostProcessing.ShakeToPreview.PostExposure => sspp.postExposureShake,
                        SmoothShakePostProcessing.ShakeToPreview.Contrast => sspp.contrastShake,
                        SmoothShakePostProcessing.ShakeToPreview.HueShift => sspp.hueShiftShake,
                        SmoothShakePostProcessing.ShakeToPreview.Saturation => sspp.saturationShake,
                        SmoothShakePostProcessing.ShakeToPreview.FilmGrain => sspp.filmGrainShake,
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionIntensity => sspp.lensDistortionIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionXYMultiplier => sspp.lensDistortionXYMultiplierShake,
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionCenter => sspp.lensDistortionCenterShake,
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionScale => sspp.lensDistortionScaleShake,
                        SmoothShakePostProcessing.ShakeToPreview.MotionBlur => sspp.motionBlurShake,
                        SmoothShakePostProcessing.ShakeToPreview.PaniniProjectionDistance => sspp.paniniProjectionDistanceShake,
                        SmoothShakePostProcessing.ShakeToPreview.PaniniProjectionCrop => sspp.paniniProjectionCropShake,
                        SmoothShakePostProcessing.ShakeToPreview.SplitToningBalance => sspp.splitToningBalanceShake,
                        SmoothShakePostProcessing.ShakeToPreview.VignetteCenter => sspp.vignetteCenterShake,
                        SmoothShakePostProcessing.ShakeToPreview.VignetteIntensity => sspp.vignetteIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.VignetteSmoothness => sspp.vignetteSmoothnessShake,
#if RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessing.ShakeToPreview.VignetteRoundness => sspp.vignetteRoundnessShake,
#endif
                        SmoothShakePostProcessing.ShakeToPreview.WhiteBalanceTemperature => sspp.whiteBalanceTemperatureShake,
                        SmoothShakePostProcessing.ShakeToPreview.WhiteBalanceTint => sspp.whiteBalanceTintShake,
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldGaussianStartEnd => sspp.depthOfFieldGaussianStartEndShake,
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldGaussianMaxRadius => sspp.depthOfFieldGaussianMaxRadiusShake,
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldBokehFocusDistance => sspp.depthOfFieldBokehFocusDistanceShake,
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldBokehFocalLength => sspp.depthOfFieldBokehFocalLengthShake,
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldBokehAperture => sspp.depthOfFieldBokehApertureShake,
                        SmoothShakePostProcessing.ShakeToPreview.ColorLookup => sspp.colorLookupShake,
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldPhysicalFocusDistance => sspp.depthOfFieldPhysicalFocusDistanceShake,
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldManualNearStartEnd => sspp.depthOfFieldManualNearStartEndShake,
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldManualFarStartEnd => sspp.depthOfFieldManualFarStartEndShake,
                        SmoothShakePostProcessing.ShakeToPreview.FogAttenuation => sspp.fogAttenuationShake,
                        SmoothShakePostProcessing.ShakeToPreview.FogBaseHeight => sspp.fogBaseHeightShake,
                        SmoothShakePostProcessing.ShakeToPreview.FogMaxHeight => sspp.fogMaxHeightShake,
#endif
#if SMOOTHPOSTPROCESSING
                        SmoothShakePostProcessing.ShakeToPreview.Blur => sspp.blurShake,
                        SmoothShakePostProcessing.ShakeToPreview.Displace => sspp.displaceShake,
                        SmoothShakePostProcessing.ShakeToPreview.EdgeDetection => sspp.edgeDetectionShake,
                        SmoothShakePostProcessing.ShakeToPreview.Glitch => sspp.glitchIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.Invert => sspp.invertIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.Kaleidoscope => sspp.kaleidoscopeShake,
                        SmoothShakePostProcessing.ShakeToPreview.MonitorScanline => sspp.monitorScanlineShake,
                        SmoothShakePostProcessing.ShakeToPreview.MonitorNoise => sspp.monitorNoiseShake,
                        SmoothShakePostProcessing.ShakeToPreview.NightVisionBrightness => sspp.nightVisionBrightnessShake,
                        SmoothShakePostProcessing.ShakeToPreview.NightVisionFlickerIntensity => sspp.nightVisionFlickerIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.Pixelation => sspp.pixelSizeShake,
                        SmoothShakePostProcessing.ShakeToPreview.RGBSplitR => sspp.rgbSplitShakeR,
                        SmoothShakePostProcessing.ShakeToPreview.RGBSplitG => sspp.rgbSplitShakeG,
                        SmoothShakePostProcessing.ShakeToPreview.RGBSplitB => sspp.rgbSplitShakeB,
                        SmoothShakePostProcessing.ShakeToPreview.RotBlur => sspp.rotBlurShake,
                        SmoothShakePostProcessing.ShakeToPreview.RotBlurAngle => sspp.rotBlurAngleShake,
                        SmoothShakePostProcessing.ShakeToPreview.RotBlurCenter => sspp.rotBlurCenterShake,
                        SmoothShakePostProcessing.ShakeToPreview.SolarizeIntensity => sspp.solarizeIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.SolarizeThreshold => sspp.solarizeThresholdShake,
                        SmoothShakePostProcessing.ShakeToPreview.SolarizeSmoothness => sspp.solarizeSmoothnessShake,
                        SmoothShakePostProcessing.ShakeToPreview.ThermalVisionBrightness => sspp.thermalVisionBrightnessShake,
                        SmoothShakePostProcessing.ShakeToPreview.ThermalVisionContrast => sspp.thermalVisionContrastShake,
                        SmoothShakePostProcessing.ShakeToPreview.TintVignette => sspp.tintVignetteIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.TintNoise => sspp.tintNoiseIntensityShake,
                        SmoothShakePostProcessing.ShakeToPreview.TwirlStrength => sspp.twirlStrengthShake,
                        SmoothShakePostProcessing.ShakeToPreview.TwirlCenter => sspp.twirlCenterShake,
                        SmoothShakePostProcessing.ShakeToPreview.TwirlRadius => sspp.twirlRadiusShake,
                        SmoothShakePostProcessing.ShakeToPreview.TwirlVignette => sspp.twirlVignetteShake,
                        SmoothShakePostProcessing.ShakeToPreview.Twitch => sspp.twitchOffsetShake,
                        SmoothShakePostProcessing.ShakeToPreview.WaveWarpAmplitude => sspp.waveWarpAmplitudeShake,
                        SmoothShakePostProcessing.ShakeToPreview.WaveWarpVignette => sspp.waveWarpVignetteShake,
                        SmoothShakePostProcessing.ShakeToPreview.ZoomBlur => sspp.zoomBlurShake,
                        SmoothShakePostProcessing.ShakeToPreview.ZoomBlurCenter => sspp.zoomBlurCenterShake,
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessing.ShakeToPreview.GaussianBlur => sspp.gaussianBlurShake,
                        SmoothShakePostProcessing.ShakeToPreview.NoiseBloomStrength => sspp.noiseBloomStrengthShake,
                        SmoothShakePostProcessing.ShakeToPreview.NoiseBloomThreshold => sspp.noiseBloomThresholdShake,
                        SmoothShakePostProcessing.ShakeToPreview.NoiseBloomRange => sspp.noiseBloomRangeShake,
#endif
#endif
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    _ => throw new Exception("MultiShakeBase not found"),
                };
            }
            else if (shakeBasePreset)
            {
                return shakeBasePreset switch
                {
                    SmoothShakePreset ssp => ssp.shakeToPreview switch
                    {
                        SmoothShakePreset.ShakeToPreview.Position => ssp.positionShake,
                        SmoothShakePreset.ShakeToPreview.Rotation => ssp.rotationShake,
                        SmoothShakePreset.ShakeToPreview.Scale => ssp.scaleShake,
                        SmoothShakePreset.ShakeToPreview.FOV => ssp.FOVShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if CINEMACHINE
                    SmoothShakeCinemachinePreset sscp => sscp.shakeToPreview switch
                    {
                        SmoothShakeCinemachinePreset.ShakeToPreview.Position => sscp.positionShake,
                        SmoothShakeCinemachinePreset.ShakeToPreview.Rotation => sscp.rotationShake,
                        SmoothShakeCinemachinePreset.ShakeToPreview.FOV => sscp.FOVShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    SmoothShakeRigidbodyPreset ssrp => ssrp.shakeToPreview switch
                    {
                        SmoothShakeRigidbodyPreset.ShakeToPreview.Force => ssrp.forceShake,
                        SmoothShakeRigidbodyPreset.ShakeToPreview.Torque => ssrp.torqueShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeMaterialPreset ssmp => ssmp.shakeToPreview switch
                    {
                        SmoothShakeMaterialPreset.ShakeToPreview.Float => ssmp.propertyToShake.floatShake,
                        SmoothShakeMaterialPreset.ShakeToPreview.Vector => ssmp.propertyToShake.vectorShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeLightPreset sslp => sslp.shakeToPreview switch
                    {
                        SmoothShakeLightPreset.ShakeToPreview.Intensity => sslp.intensityShake,
                        SmoothShakeLightPreset.ShakeToPreview.Range => sslp.rangeShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeAudioPreset ssap => ssap.shakeToPreview switch
                    {
                        SmoothShakeAudioPreset.ShakeToPreview.Volume => ssap.volumeShake,
                        SmoothShakeAudioPreset.ShakeToPreview.Pan => ssap.panShake,
                        SmoothShakeAudioPreset.ShakeToPreview.Pitch => ssap.pitchShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeHapticsGamepadPreset sshp => sshp.shakeToPreview switch
                    {
                        SmoothShakeHapticsGamepadPreset.ShakeToPreview.LowFrequencyMotor => sshp.lowFrequencyMotorShake,
                        SmoothShakeHapticsGamepadPreset.ShakeToPreview.HighFrequencyMotor => sshp.highFrequencyMotorShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if UNITY_XR
                    SmoothShakeHapticsXRPreset sshxrp => sshxrp.shakeToPreview switch
                    {
                        SmoothShakeHapticsXRPreset.ShakeToPreview.leftController => sshxrp.leftControllerShake,
                        SmoothShakeHapticsXRPreset.ShakeToPreview.rightController => sshxrp.rightControllerShake,
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                    SmoothShakePostProcessingPreset ssppp => ssppp.shakeToPreview switch
                    {
                        SmoothShakePostProcessingPreset.ShakeToPreview.Weight => ssppp.weightShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.BloomThreshold => ssppp.bloomThresholdShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.BloomIntensity => ssppp.bloomIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.BloomScatter => ssppp.bloomScatterShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChannelMixerRed => ssppp.channelMixerRedShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChannelMixerBlue => ssppp.channelMixerBlueShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChannelMixerGreen => ssppp.channelMixerGreenShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChromaticAberration => ssppp.chromaticAberrationShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.PostExposure => ssppp.postExposureShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Contrast => ssppp.contrastShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.HueShift => ssppp.hueShiftShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Saturation => ssppp.saturationShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.FilmGrain => ssppp.filmGrainShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionIntensity => ssppp.lensDistortionIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionXYMultiplier => ssppp.lensDistortionXYMultiplierShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionCenter => ssppp.lensDistortionCenterShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionScale => ssppp.lensDistortionScaleShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.MotionBlur => ssppp.motionBlurShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.PaniniProjectionDistance => ssppp.paniniProjectionDistanceShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.PaniniProjectionCrop => ssppp.paniniProjectionCropShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.SplitToningBalance => ssppp.splitToningBalanceShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteCenter => ssppp.vignetteCenterShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteIntensity => ssppp.vignetteIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteSmoothness => ssppp.vignetteSmoothnessShake,
#if RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteRoundness => ssppp.vignetteRoundnessShake,
#endif
                        SmoothShakePostProcessingPreset.ShakeToPreview.WhiteBalanceTemperature => ssppp.whiteBalanceTemperatureShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.WhiteBalanceTint => ssppp.whiteBalanceTintShake,
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldGaussianStartEnd => ssppp.depthOfFieldGaussianStartEndShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldGaussianMaxRadius => ssppp.depthOfFieldGaussianMaxRadiusShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldBokehFocusDistance => ssppp.depthOfFieldBokehFocusDistanceShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldBokehFocalLength => ssppp.depthOfFieldBokehFocalLengthShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldBokehAperture => ssppp.depthOfFieldBokehApertureShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ColorLookup => ssppp.colorLookupShake,
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldPhysicalFocusDistance => ssppp.depthOfFieldPhysicalFocusDistanceShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldManualNearStartEnd => ssppp.depthOfFieldManualNearStartEndShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldManualFarStartEnd => ssppp.depthOfFieldManualFarStartEndShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.FogAttenuation => ssppp.fogAttenuationShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.FogBaseHeight => ssppp.fogBaseHeightShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.FogMaxHeight => ssppp.fogMaxHeightShake,
#endif
#if SMOOTHPOSTPROCESSING
                        SmoothShakePostProcessingPreset.ShakeToPreview.Blur => ssppp.blurShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Displace => ssppp.displaceShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.EdgeDetection => ssppp.edgeDetectionShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Glitch => ssppp.glitchIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Invert => ssppp.invertIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Kaleidoscope => ssppp.kaleidoscopeShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.MonitorScanline => ssppp.monitorScanlineShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.MonitorNoise => ssppp.monitorNoiseShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.NightVisionBrightness => ssppp.nightVisionBrightnessShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.NightVisionFlickerIntensity => ssppp.nightVisionFlickerIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Pixelation => ssppp.pixelSizeShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.RGBSplitR => ssppp.rgbSplitShakeR,
                        SmoothShakePostProcessingPreset.ShakeToPreview.RGBSplitG => ssppp.rgbSplitShakeG,
                        SmoothShakePostProcessingPreset.ShakeToPreview.RGBSplitB => ssppp.rgbSplitShakeB,
                        SmoothShakePostProcessingPreset.ShakeToPreview.RotBlur => ssppp.rotBlurShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.RotBlurAngle => ssppp.rotBlurAngleShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.RotBlurCenter => ssppp.rotBlurCenterShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.SolarizeIntensity => ssppp.solarizeIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.SolarizeThreshold => ssppp.solarizeThresholdShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.SolarizeSmoothness => ssppp.solarizeSmoothnessShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ThermalVisionBrightness => ssppp.thermalVisionBrightnessShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ThermalVisionContrast => ssppp.thermalVisionContrastShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.TintVignette => ssppp.tintVignetteIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.TintNoise => ssppp.tintNoiseIntensityShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlStrength => ssppp.twirlStrengthShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlCenter => ssppp.twirlCenterShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlRadius => ssppp.twirlRadiusShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlVignette => ssppp.twirlVignetteShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.Twitch => ssppp.twitchOffsetShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.WaveWarpAmplitude => ssppp.waveWarpAmplitudeShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.WaveWarpVignette => ssppp.waveWarpVignetteShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ZoomBlur => ssppp.zoomBlurShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.ZoomBlurCenter => ssppp.zoomBlurCenterShake,
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessingPreset.ShakeToPreview.GaussianBlur => ssppp.gaussianBlurShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.NoiseBloomStrength => ssppp.noiseBloomStrengthShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.NoiseBloomThreshold => ssppp.noiseBloomThresholdShake,
                        SmoothShakePostProcessingPreset.ShakeToPreview.NoiseBloomRange => ssppp.noiseBloomRangeShake,
#endif
#endif
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    _ => throw new NotImplementedException("ShakeBasePreset not found"),
                };
            }
            else
            {
                throw new Exception("Must be given a shakebase or shakebasepreset");
            }
        }

        public static string GetShakeName(ShakeBase shakeBase = null, ShakeBasePreset shakeBasePreset = null)
        {
            if (shakeBase)
            {
                return shakeBase switch
                {
                    SmoothShake ss => ss.shakeToPreview switch
                    {
                        SmoothShake.ShakeToPreview.Position => "Position Shake",
                        SmoothShake.ShakeToPreview.Rotation => "Rotation Shake",
                        SmoothShake.ShakeToPreview.Scale => "Scale Shake",
                        SmoothShake.ShakeToPreview.FOV => "FOV Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if CINEMACHINE
                    SmoothShakeCinemachine ssc => ssc.shakeToPreview switch
                    {
                        SmoothShakeCinemachine.ShakeToPreview.Position => "Position Shake",
                        SmoothShakeCinemachine.ShakeToPreview.Rotation => "Rotation Shake",
                        SmoothShakeCinemachine.ShakeToPreview.FOV => "FOV Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    SmoothShakeRigidbody ssr => ssr.shakeToPreview switch
                    {
                        SmoothShakeRigidbody.ShakeToPreview.Force => "Force Shake",
                        SmoothShakeRigidbody.ShakeToPreview.Torque => "Torque Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeMaterial ssm => ssm.shakeToPreview switch
                    {
                        SmoothShakeMaterial.ShakeToPreview.Float => "Float Shake",
                        SmoothShakeMaterial.ShakeToPreview.Vector => "Vector Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeLight ssl => ssl.shakeToPreview switch
                    {
                        SmoothShakeLight.ShakeToPreview.Intensity => "Intensity Shake",
                        SmoothShakeLight.ShakeToPreview.Range => "Range Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeAudio ssa => ssa.shakeToPreview switch
                    {
                        SmoothShakeAudio.ShakeToPreview.Volume => "Volume Shake",
                        SmoothShakeAudio.ShakeToPreview.Pan => "Pan Shake",
                        SmoothShakeAudio.ShakeToPreview.Pitch => "Pitch Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeHapticsGamepad ssh => ssh.shakeToPreview switch
                    {
                        SmoothShakeHapticsGamepad.ShakeToPreview.LowFrequencyMotor => "Low Frequency Motor",
                        SmoothShakeHapticsGamepad.ShakeToPreview.HighFrequencyMotor => "High Frequency Motor",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if UNITY_XR
                    SmoothShakeHapticsXR sshxrp => sshxrp.shakeToPreview switch
                    {
                        SmoothShakeHapticsXR.ShakeToPreview.leftController => "Left Controller",
                        SmoothShakeHapticsXR.ShakeToPreview.rightController => "Right Controller",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                    SmoothShakePostProcessing sspp => sspp.shakeToPreview switch
                    {
                        SmoothShakePostProcessing.ShakeToPreview.Weight => "Weight",
                        SmoothShakePostProcessing.ShakeToPreview.BloomThreshold => "Bloom Threshold",
                        SmoothShakePostProcessing.ShakeToPreview.BloomIntensity => "Bloom Intensity",
                        SmoothShakePostProcessing.ShakeToPreview.BloomScatter => "Bloom Scatter",
                        SmoothShakePostProcessing.ShakeToPreview.ChannelMixerRed => "Channel Mixer Red",
                        SmoothShakePostProcessing.ShakeToPreview.ChannelMixerBlue => "Channel Mixer Blue",
                        SmoothShakePostProcessing.ShakeToPreview.ChannelMixerGreen => "Channel Mixer Green",
                        SmoothShakePostProcessing.ShakeToPreview.ChromaticAberration => "Chromatic Aberration",
                        SmoothShakePostProcessing.ShakeToPreview.PostExposure => "Post Exposure",
                        SmoothShakePostProcessing.ShakeToPreview.Contrast => "Contrast",
                        SmoothShakePostProcessing.ShakeToPreview.HueShift => "Hue Shift",
                        SmoothShakePostProcessing.ShakeToPreview.Saturation => "Saturation",
                        SmoothShakePostProcessing.ShakeToPreview.FilmGrain => "Film Grain",
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionIntensity => "Lens Distortion Intensity",
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionXYMultiplier => "Lens Distortion XY Multiplier",
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionCenter => "Lens Distortion Center",
                        SmoothShakePostProcessing.ShakeToPreview.LensDistortionScale => "Lens Distortion Scale",
                        SmoothShakePostProcessing.ShakeToPreview.MotionBlur => "Motion Blur",
                        SmoothShakePostProcessing.ShakeToPreview.PaniniProjectionDistance => "Panini Projection Distance",
                        SmoothShakePostProcessing.ShakeToPreview.PaniniProjectionCrop => "Panini Projection Crop",
                        SmoothShakePostProcessing.ShakeToPreview.SplitToningBalance => "Split Toning Balance",
                        SmoothShakePostProcessing.ShakeToPreview.VignetteCenter => "Vignette Center",
                        SmoothShakePostProcessing.ShakeToPreview.VignetteIntensity => "Vignette Intensity",
                        SmoothShakePostProcessing.ShakeToPreview.VignetteSmoothness => "Vignette Smoothness",
#if RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessing.ShakeToPreview.VignetteRoundness => "Vignette Roundness",
#endif
                        SmoothShakePostProcessing.ShakeToPreview.WhiteBalanceTemperature => "White Balance Temperature",
                        SmoothShakePostProcessing.ShakeToPreview.WhiteBalanceTint => "White Balance Tint",
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldGaussianStartEnd => "Depth Of Field Gaussian Start End",
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldGaussianMaxRadius => "Depth Of Field Gaussian Max Radius",
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldBokehFocusDistance => "Depth Of Field Bokeh Focus Distance",
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldBokehFocalLength => "Depth Of Field Bokeh Focal Length",
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldBokehAperture => "Depth Of Field Bokeh Aperture",
                        SmoothShakePostProcessing.ShakeToPreview.ColorLookup => "Color Lookup",
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldPhysicalFocusDistance => "Depth Of Field Physical Focus Distance",
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldManualNearStartEnd => "Depth Of Field Manual Near Start End",
                        SmoothShakePostProcessing.ShakeToPreview.DepthOfFieldManualFarStartEnd => "Depth Of Field Manual Far Start End",
                        SmoothShakePostProcessing.ShakeToPreview.FogAttenuation => "Fog Attenuation",
                        SmoothShakePostProcessing.ShakeToPreview.FogBaseHeight => "Fog Base Height",
                        SmoothShakePostProcessing.ShakeToPreview.FogMaxHeight => "Fog Max Height",
#endif
#if SMOOTHPOSTPROCESSING
                        SmoothShakePostProcessing.ShakeToPreview.Blur => "Blur",
                        SmoothShakePostProcessing.ShakeToPreview.Displace => "Displace",
                        SmoothShakePostProcessing.ShakeToPreview.EdgeDetection => "Edge Detection",
                        SmoothShakePostProcessing.ShakeToPreview.Glitch => "Glitch",
                        SmoothShakePostProcessing.ShakeToPreview.Invert => "Invert",
                        SmoothShakePostProcessing.ShakeToPreview.Kaleidoscope => "Kaleidoscope",
                        SmoothShakePostProcessing.ShakeToPreview.MonitorNoise => "Monitor Noise",
                        SmoothShakePostProcessing.ShakeToPreview.MonitorScanline => "Monitor Scanline",
                        SmoothShakePostProcessing.ShakeToPreview.NightVisionBrightness => "Night Vision Brightness",
                        SmoothShakePostProcessing.ShakeToPreview.NightVisionFlickerIntensity => "Night Vision Flicker Intensity",
                        SmoothShakePostProcessing.ShakeToPreview.Pixelation => "Pixelation",
                        SmoothShakePostProcessing.ShakeToPreview.RGBSplitR => "RGB Split R",
                        SmoothShakePostProcessing.ShakeToPreview.RGBSplitG => "RGB Split G",
                        SmoothShakePostProcessing.ShakeToPreview.RGBSplitB => "RGB Split B",
                        SmoothShakePostProcessing.ShakeToPreview.RotBlur => "Rot Blur",
                        SmoothShakePostProcessing.ShakeToPreview.RotBlurAngle => "Rot Blur Angle",
                        SmoothShakePostProcessing.ShakeToPreview.RotBlurCenter => "Rot Blur Center",
                        SmoothShakePostProcessing.ShakeToPreview.SolarizeIntensity => "Solarize Intensity",
                        SmoothShakePostProcessing.ShakeToPreview.SolarizeThreshold => "Solarize Threshold",
                        SmoothShakePostProcessing.ShakeToPreview.SolarizeSmoothness => "Solarize Smoothness",
                        SmoothShakePostProcessing.ShakeToPreview.ThermalVisionBrightness => "Thermal Vision Brightness",
                        SmoothShakePostProcessing.ShakeToPreview.ThermalVisionContrast => "Thermal Vision Contrast",
                        SmoothShakePostProcessing.ShakeToPreview.TintVignette => "Tint Vignette",
                        SmoothShakePostProcessing.ShakeToPreview.TintNoise => "Tint Noise",
                        SmoothShakePostProcessing.ShakeToPreview.TwirlStrength => "Twirl Strength",
                        SmoothShakePostProcessing.ShakeToPreview.TwirlCenter => "Twirl Center",
                        SmoothShakePostProcessing.ShakeToPreview.TwirlRadius => "Twirl Radius",
                        SmoothShakePostProcessing.ShakeToPreview.TwirlVignette => "Twirl Vignette",
                        SmoothShakePostProcessing.ShakeToPreview.Twitch => "Twitch",
                        SmoothShakePostProcessing.ShakeToPreview.WaveWarpAmplitude => "Wave Warp Amplitude",
                        SmoothShakePostProcessing.ShakeToPreview.WaveWarpVignette => "Wave Warp Vignette",
                        SmoothShakePostProcessing.ShakeToPreview.ZoomBlur => "Zoom Blur",
                        SmoothShakePostProcessing.ShakeToPreview.ZoomBlurCenter => "Zoom Blur Center",
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessing.ShakeToPreview.GaussianBlur => "Gaussian Blur",
                        SmoothShakePostProcessing.ShakeToPreview.NoiseBloomStrength => "Noise Bloom Strength",
                        SmoothShakePostProcessing.ShakeToPreview.NoiseBloomThreshold => "Noise Bloom Threshold",
                        SmoothShakePostProcessing.ShakeToPreview.NoiseBloomRange => "Noise Bloom Range",
#endif
#endif
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    _ => throw new Exception("MultiShakeBase not found"),
                };
            }
            else if (shakeBasePreset)
            {
                return shakeBasePreset switch
                {
                    SmoothShakePreset ss => ss.shakeToPreview switch
                    {
                        SmoothShakePreset.ShakeToPreview.Position => "Position Shake",
                        SmoothShakePreset.ShakeToPreview.Rotation => "Rotation Shake",
                        SmoothShakePreset.ShakeToPreview.Scale => "Scale Shake",
                        SmoothShakePreset.ShakeToPreview.FOV => "FOV Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if CINEMACHINE
                    SmoothShakeCinemachinePreset ssc => ssc.shakeToPreview switch
                    {
                        SmoothShakeCinemachinePreset.ShakeToPreview.Position => "Position Shake",
                        SmoothShakeCinemachinePreset.ShakeToPreview.Rotation => "Rotation Shake",
                        SmoothShakeCinemachinePreset.ShakeToPreview.FOV => "FOV Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    SmoothShakeRigidbodyPreset ssr => ssr.shakeToPreview switch
                    {
                        SmoothShakeRigidbodyPreset.ShakeToPreview.Force => "Force Shake",
                        SmoothShakeRigidbodyPreset.ShakeToPreview.Torque => "Torque Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeMaterialPreset ssm => ssm.shakeToPreview switch
                    {
                        SmoothShakeMaterialPreset.ShakeToPreview.Float => "Float Shake",
                        SmoothShakeMaterialPreset.ShakeToPreview.Vector => "Vector Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeLightPreset ssl => ssl.shakeToPreview switch
                    {
                        SmoothShakeLightPreset.ShakeToPreview.Intensity => "Intensity Shake",
                        SmoothShakeLightPreset.ShakeToPreview.Range => "Range Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeAudioPreset ssa => ssa.shakeToPreview switch
                    {
                        SmoothShakeAudioPreset.ShakeToPreview.Volume => "Volume Shake",
                        SmoothShakeAudioPreset.ShakeToPreview.Pan => "Pan Shake",
                        SmoothShakeAudioPreset.ShakeToPreview.Pitch => "Pitch Shake",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
                    SmoothShakeHapticsGamepadPreset ssh => ssh.shakeToPreview switch
                    {
                        SmoothShakeHapticsGamepadPreset.ShakeToPreview.LowFrequencyMotor => "Low Frequency Motor",
                        SmoothShakeHapticsGamepadPreset.ShakeToPreview.HighFrequencyMotor => "High Frequency Motor",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#if UNITY_XR
                    SmoothShakeHapticsXRPreset sshxrp => sshxrp.shakeToPreview switch
                    {
                        SmoothShakeHapticsXRPreset.ShakeToPreview.leftController => "Left Controller",
                        SmoothShakeHapticsXRPreset.ShakeToPreview.rightController => "Right Controller",
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                    SmoothShakePostProcessingPreset sspp => sspp.shakeToPreview switch
                    {
                        SmoothShakePostProcessingPreset.ShakeToPreview.Weight => "Weight",
                        SmoothShakePostProcessingPreset.ShakeToPreview.BloomThreshold => "Bloom Threshold",
                        SmoothShakePostProcessingPreset.ShakeToPreview.BloomIntensity => "Bloom Intensity",
                        SmoothShakePostProcessingPreset.ShakeToPreview.BloomScatter => "Bloom Scatter",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChannelMixerRed => "Channel Mixer Red",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChannelMixerBlue => "Channel Mixer Blue",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChannelMixerGreen => "Channel Mixer Green",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ChromaticAberration => "Chromatic Aberration",
                        SmoothShakePostProcessingPreset.ShakeToPreview.PostExposure => "Post Exposure",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Contrast => "Contrast",
                        SmoothShakePostProcessingPreset.ShakeToPreview.HueShift => "Hue Shift",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Saturation => "Saturation",
                        SmoothShakePostProcessingPreset.ShakeToPreview.FilmGrain => "Film Grain",
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionIntensity => "Lens Distortion Intensity",
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionXYMultiplier => "Lens Distortion XY Multiplier",
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionCenter => "Lens Distortion Center",
                        SmoothShakePostProcessingPreset.ShakeToPreview.LensDistortionScale => "Lens Distortion Scale",
                        SmoothShakePostProcessingPreset.ShakeToPreview.MotionBlur => "Motion Blur",
                        SmoothShakePostProcessingPreset.ShakeToPreview.PaniniProjectionDistance => "Panini Projection Distance",
                        SmoothShakePostProcessingPreset.ShakeToPreview.PaniniProjectionCrop => "Panini Projection Crop",
                        SmoothShakePostProcessingPreset.ShakeToPreview.SplitToningBalance => "Split Toning Balance",
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteCenter => "Vignette Center",
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteIntensity => "Vignette Intensity",
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteSmoothness => "Vignette Smoothness",
#if RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessingPreset.ShakeToPreview.VignetteRoundness => "Vignette Roundness",
#endif
                        SmoothShakePostProcessingPreset.ShakeToPreview.WhiteBalanceTemperature => "White Balance Temperature",
                        SmoothShakePostProcessingPreset.ShakeToPreview.WhiteBalanceTint => "White Balance Tint",
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldGaussianStartEnd => "Depth Of Field Gaussian Start End",
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldGaussianMaxRadius => "Depth Of Field Gaussian Max Radius",
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldBokehFocusDistance => "Depth Of Field Bokeh Focus Distance",
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldBokehFocalLength => "Depth Of Field Bokeh Focal Length",
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldBokehAperture => "Depth Of Field Bokeh Aperture",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ColorLookup => "Color Lookup",
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldPhysicalFocusDistance => "Depth Of Field Physical Focus Distance",
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldManualNearStartEnd => "Depth Of Field Manual Near Start End",
                        SmoothShakePostProcessingPreset.ShakeToPreview.DepthOfFieldManualFarStartEnd => "Depth Of Field Manual Far Start End",
                        SmoothShakePostProcessingPreset.ShakeToPreview.FogAttenuation => "Fog Attenuation",
                        SmoothShakePostProcessingPreset.ShakeToPreview.FogBaseHeight => "Fog Base Height",
                        SmoothShakePostProcessingPreset.ShakeToPreview.FogMaxHeight => "Fog Max Height",
#endif
#if SMOOTHPOSTPROCESSING
                        SmoothShakePostProcessingPreset.ShakeToPreview.Blur => "Blur",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Displace => "Displace",
                        SmoothShakePostProcessingPreset.ShakeToPreview.EdgeDetection => "Edge Detection",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Glitch => "Glitch",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Invert => "Invert",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Kaleidoscope => "Kaleidoscope",
                        SmoothShakePostProcessingPreset.ShakeToPreview.MonitorScanline => "Monitor Scanline",
                        SmoothShakePostProcessingPreset.ShakeToPreview.MonitorNoise => "Monitor Noise",
                        SmoothShakePostProcessingPreset.ShakeToPreview.NightVisionBrightness => "Night Vision Brightness",
                        SmoothShakePostProcessingPreset.ShakeToPreview.NightVisionFlickerIntensity => "Night Vision Flicker Intensity",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Pixelation => "Pixelation",
                        SmoothShakePostProcessingPreset.ShakeToPreview.RGBSplitR => "RGB Split R",
                        SmoothShakePostProcessingPreset.ShakeToPreview.RGBSplitG => "RGB Split G",
                        SmoothShakePostProcessingPreset.ShakeToPreview.RGBSplitB => "RGB Split B",
                        SmoothShakePostProcessingPreset.ShakeToPreview.RotBlur => "Rot Blur",
                        SmoothShakePostProcessingPreset.ShakeToPreview.RotBlurAngle => "Rot Blur Angle",
                        SmoothShakePostProcessingPreset.ShakeToPreview.RotBlurCenter => "Rot Blur Center",
                        SmoothShakePostProcessingPreset.ShakeToPreview.SolarizeIntensity => "Solarize Intensity",
                        SmoothShakePostProcessingPreset.ShakeToPreview.SolarizeThreshold => "Solarize Threshold",
                        SmoothShakePostProcessingPreset.ShakeToPreview.SolarizeSmoothness => "Solarize Smoothness",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ThermalVisionBrightness => "Thermal Vision Brightness",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ThermalVisionContrast => "Thermal Vision Contrast",
                        SmoothShakePostProcessingPreset.ShakeToPreview.TintVignette => "Tint Vignette",
                        SmoothShakePostProcessingPreset.ShakeToPreview.TintNoise => "Tint Noise",
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlStrength => "Twirl Strength",
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlCenter => "Twirl Center",
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlRadius => "Twirl Radius",
                        SmoothShakePostProcessingPreset.ShakeToPreview.TwirlVignette => "Twirl Vignette",
                        SmoothShakePostProcessingPreset.ShakeToPreview.Twitch => "Twitch",
                        SmoothShakePostProcessingPreset.ShakeToPreview.WaveWarpAmplitude => "Wave Warp Amplitude",
                        SmoothShakePostProcessingPreset.ShakeToPreview.WaveWarpVignette => "Wave Warp Vignette",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ZoomBlur => "Zoom Blur",
                        SmoothShakePostProcessingPreset.ShakeToPreview.ZoomBlurCenter => "Zoom Blur Center",
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        SmoothShakePostProcessingPreset.ShakeToPreview.GaussianBlur => "Gaussian Blur",
                        SmoothShakePostProcessingPreset.ShakeToPreview.NoiseBloomStrength => "Noise Bloom Strength",
                        SmoothShakePostProcessingPreset.ShakeToPreview.NoiseBloomThreshold => "Noise Bloom Threshold",
                        SmoothShakePostProcessingPreset.ShakeToPreview.NoiseBloomRange => "Noise Bloom Range",
#endif
#endif
                        _ => throw new Exception("ShakeToPreview not found"),
                    },
#endif
                    _ => throw new Exception("MultiShakeBase not found"),
                };
            }
            else
            {
                throw new Exception("Must be given a shakebase or shakebasepreset");
            }
        }
    }
}
#endif