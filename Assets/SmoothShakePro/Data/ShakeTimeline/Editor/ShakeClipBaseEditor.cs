#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CustomEditor(typeof(ShakeClipBase), true)]
    internal class ShakeClipBaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ShakeClipBase clip = (ShakeClipBase)target;

            serializedObject.Update();

            switch (clip)
            {
                case SmoothShakeClip ssc:
                    DrawPropertiesExcludingCustom(clip, ssc._preset, "positionShake", "rotationShake", "scaleShake", "FOVShake");
                    break;
#if CINEMACHINE
                case SmoothShakeCinemachineClip sscc:
                    DrawPropertiesExcludingCustom(clip, sscc._preset, "positionShake", "rotationShake", "FOVShake");
                    break;
#endif
                case SmoothShakeLightClip sslc:
                    DrawPropertiesExcludingCustom(clip, sslc._preset, "intensityShake", "rangeShake");
                    break;
                case SmoothShakeHapticsGamepadClip sshc:
                    DrawPropertiesExcludingCustom(clip, sshc._preset, "lowFrequencyMotorShake", "highFrequencyMotorShake");
                    break;
                case SmoothShakeMaterialClip ssmc:
                    DrawPropertiesExcludingCustom(clip, ssmc._preset, "floatShake", "vectorShake");
                    break;
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                case SmoothShakePostProcessingClip ssppc:
                    DrawPropertiesExcludingCustom(clip, ssppc._preset, "weightShake",
                        "bloomThresholdShake", "bloomIntensityShake", "bloomScatterShake",
                        "channelMixerRedShake", "channelMixerGreenShake", "channelMixerBlueShake",
                        "chromaticAberrationShake",
                        "postExposureShake", "contrastShake", "hueShiftShake", "saturationShake",
                        "filmGrainShake",
                        "lensDistortionIntensityShake", "lensDistortionXYMultiplierShake", "lensDistortionCenterShake", "lensDistortionScaleShake",
                        "motionBlurShake",
                        "paniniProjectionDistance", "paniniProjectionCrop",
                        "splitToningBalance",
                        "vignetteCenter", "vignetteIntensity", "vignetteSmoothness",
#if !RENDER_UNIVERSAL && RENDER_HDRP
                        "vignetteRoundness",
#endif
                        "whiteBalanceTemperature", "whiteBalanceTint"
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        , "depthOfFieldGaussianStartEnd", "depthOfFieldGaussianMaxRadius",
                        "depthOfFieldBokehFocusDistance", "depthOfFieldBokehFocalLength", "depthOfFieldBokehAperture",
                        "colorLookup"
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                        , "depthOfFieldPhysicalFocusDistance", "depthOfFieldManualNearStartEnd", "depthOfFieldManualFarStartEnd",
                        "fogAttenuation", "fogBaseHeight", "fogMaxHeight" 
#endif
#if SMOOTHPOSTPROCESSING
                        , "blurShake",
                        "displaceShake",
                        "edgeDetectionShake",
                        "glitchIntensityShake",
                        "invertIntensityShake",
                        "kaleidoscopeShake",
                        "monitorScanlineShake", "monitorNoiseShake",
                        "nightVisionBrightnessShake", "nightVisionFlickerIntensityShake",
                        "pixelSizeShake",
                        "rgbSplitShakeR", "rgbSplitShakeG", "rgbSplitShakeB",
                        "rotBlurShake", "rotBlurAngleShake", "rotBlurCenterShake",
                        "solarizeIntensityShake", "solarizeThresholdShake", "solarizeSmoothnessShake",
                        "thermalVisionBrightnessShake", "thermalVisionContrastShake",
                        "tintVignetteIntensityShake", "tintNoiseIntensityShake",
                        "twirlStrengthShake", "twirlCenterShake", "twirlRadiusShake", "twirlVignetteShake",
                        "twitchOffsetShake",
                        "waveWarpAmplitudeShake", "waveWarpVignetteShake",
                        "zoomBlurShake", "zoomBlurCenterShake"
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        , "gaussianBlurShake",
                        "noiseBloomStrengthShake", "noiseBloomThresholdShake" , "noiseBloomRangeShake"
#endif
                        );
#else
                        );
#endif
                    break;
#endif
            }

            serializedObject.ApplyModifiedProperties();
        }
        private void DrawPropertiesExcludingCustom(ShakeClipBase clip, ShakeBasePreset preset, params string[] excluding)
        {
            if (preset)
                DrawPresetSettings(clip, preset, excluding);
            else
            {
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                if (clip is SmoothShakePostProcessingClip ssppc)
                {
#if SMOOTHPOSTPROCESSING
                    CustomPostProcessingDrawer(ssppc.overrides, ssppc.smoothShakeProOverrides);
#else
                    CustomPostProcessingDrawer(ssppc.overrides);
#endif
                }
                else DrawPropertiesExcluding(serializedObject, "m_Script");
#else
                DrawPropertiesExcluding(serializedObject, "m_Script");
#endif

                EditorUtility.DrawUILine(EditorUtility.SmoothShakeColor, 1, 10);

                EditorGUILayout.HelpBox("Note: Individual lifetime per shaker does not affect shakes in the timeline. This is controlled by the clip's duration. You can blend seperate clips instead.", MessageType.Info);
            }

        }

        private void DrawPresetSettings(ShakeClipBase clip, ShakeBasePreset preset, params string[] excluding)
        {
            clip.ApplyPresetSettings();

            DrawPropertiesExcluding(serializedObject, excluding);

            EditorUtility.DrawUILine(EditorUtility.SmoothShakeColor, 1, 10);

            //Helpbox
            EditorGUILayout.HelpBox("This shake being overriden by " + preset.name + ".", MessageType.Info);

            EditorGUILayout.HelpBox("Note: When a preset is removed, regular settings only return when the preset slot is exposed (little dropdown button on the right.)", MessageType.Info);

            EditorGUILayout.BeginHorizontal();
            {
                //Edit button
                if (GUILayout.Button("Edit Preset"))
                {
                    PopupWindow.Show(new Rect(), new PresetPopUp(preset));
                    GUIUtility.ExitGUI();
                }
                if (GUILayout.Button("Go To Preset"))
                {
                    Selection.activeObject = preset;
                }

            }
            EditorGUILayout.EndHorizontal();
        }

#if UNITY_2022
#if SMOOTHPOSTPROCESSING
        private void CustomPostProcessingDrawer(Overrides overrides, SmoothPostProcessingOverrides sppOverrides)
#else
        private void CustomPostProcessingDrawer(Overrides overrides)
#endif
        {
            var baseHide = new HashSet<string> { "m_Script" , "preset" };

            var propertiesToShow = new HashSet<string>();

            foreach (Overrides flag in Enum.GetValues(typeof(Overrides)))
            {
                if (overrides.HasFlag(flag) && EditorUtility.postProcesPropertyMap.ContainsKey(flag))
                {
                    propertiesToShow.UnionWith(EditorUtility.postProcesPropertyMap[flag]);
                }
            }

#if SMOOTHPOSTPROCESSING
            foreach (SmoothPostProcessingOverrides flag in Enum.GetValues(typeof(SmoothPostProcessingOverrides)))
            {
                if (sppOverrides.HasFlag(flag) && EditorUtility.postProcesPropertyMapSmoothShakePro.ContainsKey(flag))
                {
                    propertiesToShow.UnionWith(EditorUtility.postProcesPropertyMapSmoothShakePro[flag]);
                }
            }
#endif

            var propertiesToHide = baseHide.Union(
                EditorUtility.postProcesPropertyMap.Values.SelectMany(x => x)
#if SMOOTHPOSTPROCESSING
                .Union(EditorUtility.postProcesPropertyMapSmoothShakePro.Values.SelectMany(x => x))
#endif
            ).Except(propertiesToShow).ToArray();

            DrawPropertiesExcluding(serializedObject, propertiesToHide);

            EditorGUILayout.HelpBox("You have to choose which overrides to target on the referenced SmoothShakePostProcessing component", MessageType.Info);
        }
#endif
    }
}
#endif
