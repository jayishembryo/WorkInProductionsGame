#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    public static class EditorUtility
    {
#if UNITY_2020
        public static Color SmoothShakeColor = new Color(0.94f, 0.15f, 0.3f);
#else
        public static Color SmoothShakeColor = new(0.94f, 0.15f, 0.3f);
#endif
        public static bool changeFileLocationPerSave = true;
        public static bool addDefinitionOnInitiating = true;
        public static TimescaleMode defaultTimescaleMode = TimescaleMode.Scaled;
        public static float defaultCustomTimescale = 1f;

#if UNITY_2022
        public static Dictionary<Overrides, string[]> postProcesPropertyMap = new Dictionary<Overrides, string[]>
        {
                { Overrides.Weight, new[] { "weightShake" } },
                { Overrides.Bloom, new[] { "bloomThresholdShake", "bloomIntensityShake", "bloomScatterShake" } },
                { Overrides.ChannelMixer, new[] { "channelMixerRedShake", "channelMixerGreenShake", "channelMixerBlueShake" } },
                { Overrides.ChromaticAberration, new[] { "chromaticAberrationShake" } },
                { Overrides.ColorAdjustments, new[] { "postExposureShake", "contrastShake", "hueShiftShake" ,"saturationShake" } },
                { Overrides.FilmGrain, new[] { "filmGrainShake" } },
                { Overrides.LensDistortion, new[] { "lensDistortionIntensityShake", "lensDistortionXYMultiplierShake", "lensDistortionCenterShake", "lensDistortionScaleShake" } },
                { Overrides.MotionBlur, new[] { "motionBlurShake" } },
                { Overrides.PaniniProjection, new[] { "paniniProjectionDistanceShake", "paniniProjectionCropShake" } },
                { Overrides.SplitToning, new[] { "splitToningBalanceShake" } },
#if RENDER_UNIVERSAL && !RENDER_HDRP
                { Overrides.Vignette, new[] { "vignetteCenterShake", "vignetteIntensityShake", "vignetteSmoothnessShake" } },
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                { Overrides.Vignette, new[] { "vignetteCenterShake", "vignetteIntensityShake", "vignetteSmoothnessShake", "vignetteRoundnessShake" } },
#endif
                { Overrides.WhiteBalance, new[] { "whiteBalanceTemperatureShake", "whiteBalanceTintShake" } },
#if RENDER_UNIVERSAL && !RENDER_HDRP
                { Overrides.DepthOfFieldGaussian, new[] { "depthOfFieldGaussianStartEndShake", "depthOfFieldGaussianMaxRadiusShake" } },
                { Overrides.DepthOfFieldBokeh, new[] { "depthOfFieldBokehFocusDistanceShake", "depthOfFieldBokehFocalLengthShake", "depthOfFieldBokehApertureShake" } },
                { Overrides.ColorLookup, new[] { "colorLookupShake" } },
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                { Overrides.DepthOfFieldPhysical, new[] { "depthOfFieldPhysicalFocusDistanceShake" } },
                { Overrides.DepthOfFieldManual , new[] { "depthOfFieldManualNearStartEndShake", "depthOfFieldManualFarStartEndShake" } },
                { Overrides.Fog, new[] { "fogAttenuationShake", "fogBaseHeightShake", "fogMaxHeightShake" } },
#endif  
        };

#if SMOOTHPOSTPROCESSING
        public static Dictionary<SmoothPostProcessingOverrides, string[]> postProcesPropertyMapSmoothShakePro = new Dictionary<SmoothPostProcessingOverrides, string[]>
        {
                { SmoothPostProcessingOverrides.Blur, new[] { "blurShake" } },
                { SmoothPostProcessingOverrides.Displace, new[] { "displaceShake" } },
                { SmoothPostProcessingOverrides.EdgeDetection, new[] { "edgeDetectionShake" } },
                { SmoothPostProcessingOverrides.Glitch, new[] { "glitchIntensityShake" } },
                { SmoothPostProcessingOverrides.Invert, new[] { "invertIntensityShake" } },
                { SmoothPostProcessingOverrides.Kaleidoscope, new[] { "kaleidoscopeShake" } },
                { SmoothPostProcessingOverrides.Monitor, new[] { "monitorScanlineShake" , "monitorNoiseShake" } },
                { SmoothPostProcessingOverrides.NightVision, new[] { "nightVisionBrightnessShake" , "nightVisionFlickerIntensityShake" } },
                { SmoothPostProcessingOverrides.Pixelation, new[] { "pixelSizeShake" } },
                { SmoothPostProcessingOverrides.RGBSplit, new[] { "rgbSplitShakeR" , "rgbSplitShakeG" , "rgbSplitShakeB" } },
                { SmoothPostProcessingOverrides.RotBlur, new[] { "rotBlurShake", "rotBlurAngleShake" , "rotBlurCenterShake" } },
                { SmoothPostProcessingOverrides.Solarize, new[] { "solarizeIntensityShake" , "solarizeThresholdShake" , "solarizeSmoothnessShake" } },
                { SmoothPostProcessingOverrides.ThermalVision, new[] { "thermalVisionBrightnessShake" , "thermalVisionContrastShake" } },
                { SmoothPostProcessingOverrides.Tint, new[] { "tintVignetteIntensityShake" , "tintNoiseIntensityShake" } },
                { SmoothPostProcessingOverrides.Twirl, new[] { "twirlStrengthShake" , "twirlCenterShake" , "twirlRadiusShake" , "twirlVignetteShake" } },
                { SmoothPostProcessingOverrides.Twitch, new[] { "twitchOffsetShake" } },
                { SmoothPostProcessingOverrides.WaveWarp, new[] { "waveWarpAmplitudeShake" , "waveWarpVignetteShake" } },
                { SmoothPostProcessingOverrides.ZoomBlur, new[] { "zoomBlurShake" , "zoomBlurCenterShake" } }, 
#if RENDER_UNIVERSAL && !RENDER_HDRP
                { SmoothPostProcessingOverrides.GaussianBlur, new[] { "gaussianBlurShake" } },
                { SmoothPostProcessingOverrides.NoiseBloom, new[] { "noiseBloomStrengthShake", "noiseBloomThresholdShake", "noiseBloomRangeShake", "noiseBloomRangeShake" } },
#endif
        };
#endif
#endif


        public static void CreateFoldersForInvalidPaths(string savePath)
        {
            if (!AssetDatabase.IsValidFolder(savePath))
            {
                //Dialogue popup to create folder
                if (UnityEditor.EditorUtility.DisplayDialog("Folder does not exist", $"The folder at path {savePath} does not exist. Do you want to create it?", "Yes", "No"))
                {
                    //Remove Assets/ from the path
                    savePath = savePath.Remove(0, 7);

                    //Create folder with the save path
                    string[] splitPath = savePath.Split('/');
                    string currentPath = "Assets";
                    for (int i = 0; i < splitPath.Length; i++)
                    {
                        if (AssetDatabase.IsValidFolder(currentPath + "/" + splitPath[i]))
                        {
                            currentPath += "/" + splitPath[i];
                        }
                        else
                        {
                            Debug.Log("Creating folder: " + currentPath + "/" + splitPath[i]);
                            AssetDatabase.CreateFolder(currentPath, splitPath[i]);
                            currentPath += "/" + splitPath[i];
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Folder does not exist, asset not created");
                }
            }
        }

        public static void CreateMyAsset<T>(string name, string savePath, ShakeBase shakeBase) where T : ShakeBasePreset
        {
            //If folder does not exist, create it
            CreateFoldersForInvalidPaths(savePath);

            T asset = ScriptableObject.CreateInstance<T>();

            switch (asset)
            {
                case SmoothShakePreset smoothShakePreset:
                    if (shakeBase is SmoothShake smoothShake)
                    {
                        smoothShakePreset.timeSettings = smoothShake.timeSettings;
                        smoothShakePreset.positionShake.AddRange(smoothShake.positionShake);
                        smoothShakePreset.rotationShake.AddRange(smoothShake.rotationShake);
                        smoothShakePreset.scaleShake.AddRange(smoothShake.scaleShake);
                        smoothShakePreset.FOVShake.AddRange(smoothShake.FOVShake);

                        smoothShake.preset = smoothShakePreset;
                    }
                    break;
#if CINEMACHINE
                case SmoothShakeCinemachinePreset smoothShakeCinemachinePreset:
                    if (shakeBase is SmoothShakeCinemachine smoothShakeCinemachine)
                    {
                        smoothShakeCinemachinePreset.timeSettings = smoothShakeCinemachine.timeSettings;

                        smoothShakeCinemachinePreset.positionShake.AddRange(smoothShakeCinemachine.positionShake);
                        smoothShakeCinemachinePreset.rotationShake.AddRange(smoothShakeCinemachine.rotationShake);
                        smoothShakeCinemachinePreset.FOVShake.AddRange(smoothShakeCinemachine.FOVShake);

                        smoothShakeCinemachine.preset = smoothShakeCinemachinePreset;
                    }
                    break;
#endif
                case SmoothShakeRigidbodyPreset smoothShakeRigidbodyPreset:
                    if (shakeBase is SmoothShakeRigidbody smoothShakeRigidbody)
                    {
                        smoothShakeRigidbodyPreset.timeSettings = smoothShakeRigidbody.timeSettings;

                        smoothShakeRigidbodyPreset.forceShake.AddRange(smoothShakeRigidbody.forceShake);
                        smoothShakeRigidbodyPreset.torqueShake.AddRange(smoothShakeRigidbody.torqueShake);

                        smoothShakeRigidbody.preset = smoothShakeRigidbodyPreset;
                    }
                    break;
                case SmoothShakeMaterialPreset smoothShakeMaterialPreset:
                    if (shakeBase is SmoothShakeMaterial smoothShakeMaterial)
                    {
                        smoothShakeMaterialPreset.timeSettings = smoothShakeMaterial.timeSettings;
                        smoothShakeMaterialPreset.propertyToShake = smoothShakeMaterial.propertyToShake;
                        smoothShakeMaterialPreset.propertyToShake.propertyName = smoothShakeMaterial.propertyToShake.propertyName;
                        smoothShakeMaterialPreset.propertyToShake.propertyType = smoothShakeMaterial.propertyToShake.propertyType;
                        smoothShakeMaterialPreset.propertyToShake.floatShake.AddRange(smoothShakeMaterial.propertyToShake.floatShake);
                        smoothShakeMaterialPreset.propertyToShake.vectorShake.AddRange(smoothShakeMaterial.propertyToShake.vectorShake);

                        smoothShakeMaterialPreset.useMaterialFromRenderer = smoothShakeMaterial.useMaterialFromRenderer;
                        if (!smoothShakeMaterial.useMaterialFromRenderer || smoothShakeMaterial.MultipleMaterials()) smoothShakeMaterialPreset.material = smoothShakeMaterial.material;

                        smoothShakeMaterial.preset = smoothShakeMaterialPreset;
                    }
                    break;
                case SmoothShakeLightPreset smoothShakeLightPreset:
                    if (shakeBase is SmoothShakeLight smoothShakeLight)
                    {
                        smoothShakeLightPreset.timeSettings = smoothShakeLight.timeSettings;

                        smoothShakeLightPreset.intensityShake.AddRange(smoothShakeLight.intensityShake);
                        smoothShakeLightPreset.rangeShake.AddRange(smoothShakeLight.rangeShake);

                        smoothShakeLight.preset = smoothShakeLightPreset;
                    }
                    break;
                case SmoothShakeAudioPreset smoothShakeAudioPreset:
                    if (shakeBase is SmoothShakeAudio smoothShakeAudio)
                    {
                        smoothShakeAudioPreset.timeSettings = smoothShakeAudio.timeSettings;

                        smoothShakeAudioPreset.volumeShake.AddRange(smoothShakeAudio.volumeShake);
                        smoothShakeAudioPreset.pitchShake.AddRange(smoothShakeAudio.pitchShake);

                        smoothShakeAudio.preset = smoothShakeAudioPreset;
                    }
                    break;
                case SmoothShakeHapticsGamepadPreset smoothShakeHapticsPreset:
                    if (shakeBase is SmoothShakeHapticsGamepad smoothShakeHaptics)
                    {
                        smoothShakeHapticsPreset.timeSettings = smoothShakeHaptics.timeSettings;

                        smoothShakeHapticsPreset.lowFrequencyMotorShake.AddRange(smoothShakeHaptics.lowFrequencyMotorShake);
                        smoothShakeHapticsPreset.highFrequencyMotorShake.AddRange(smoothShakeHaptics.highFrequencyMotorShake);

                        smoothShakeHaptics.preset = smoothShakeHapticsPreset;
                    }
                    break;
#if UNITY_XR
                case SmoothShakeHapticsXRPreset smoothShakeHapticsXRPreset:
                    if (shakeBase is SmoothShakeHapticsXR smoothShakeHapticsXR)
                    {
                        smoothShakeHapticsXRPreset.timeSettings = smoothShakeHapticsXR.timeSettings;

                        smoothShakeHapticsXRPreset.leftControllerShake.AddRange(smoothShakeHapticsXR.leftControllerShake);
                        smoothShakeHapticsXRPreset.rightControllerShake.AddRange(smoothShakeHapticsXR.rightControllerShake);

                        smoothShakeHapticsXR.preset = smoothShakeHapticsXRPreset;
                    }
                    break;
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                case SmoothShakePostProcessingPreset smoothShakePostProcessingPreset:
                    if (shakeBase is SmoothShakePostProcessing smoothShakePostProcessing)
                    {
                        smoothShakePostProcessingPreset.timeSettings = smoothShakePostProcessing.timeSettings;
                        smoothShakePostProcessingPreset.overrides = smoothShakePostProcessing.overrides;

                        smoothShakePostProcessingPreset.bloomThresholdShake.AddRange(smoothShakePostProcessing.bloomThresholdShake);
                        smoothShakePostProcessingPreset.bloomIntensityShake.AddRange(smoothShakePostProcessing.bloomIntensityShake);
                        smoothShakePostProcessingPreset.bloomScatterShake.AddRange(smoothShakePostProcessing.bloomScatterShake);
                        smoothShakePostProcessingPreset.channelMixerRedShake.AddRange(smoothShakePostProcessing.channelMixerRedShake);
                        smoothShakePostProcessingPreset.channelMixerGreenShake.AddRange(smoothShakePostProcessing.channelMixerGreenShake);
                        smoothShakePostProcessingPreset.channelMixerBlueShake.AddRange(smoothShakePostProcessing.channelMixerBlueShake);
                        smoothShakePostProcessingPreset.chromaticAberrationShake.AddRange(smoothShakePostProcessing.chromaticAberrationShake);
                        smoothShakePostProcessingPreset.postExposureShake.AddRange(smoothShakePostProcessing.postExposureShake);
                        smoothShakePostProcessingPreset.contrastShake.AddRange(smoothShakePostProcessing.contrastShake);
                        smoothShakePostProcessingPreset.hueShiftShake.AddRange(smoothShakePostProcessing.hueShiftShake);
                        smoothShakePostProcessingPreset.saturationShake.AddRange(smoothShakePostProcessing.saturationShake);
                        smoothShakePostProcessingPreset.filmGrainShake.AddRange(smoothShakePostProcessing.filmGrainShake);
                        smoothShakePostProcessingPreset.lensDistortionIntensityShake.AddRange(smoothShakePostProcessing.lensDistortionIntensityShake);
                        smoothShakePostProcessingPreset.lensDistortionXYMultiplierShake.AddRange(smoothShakePostProcessing.lensDistortionXYMultiplierShake);
                        smoothShakePostProcessingPreset.lensDistortionCenterShake.AddRange(smoothShakePostProcessing.lensDistortionCenterShake);
                        smoothShakePostProcessingPreset.lensDistortionScaleShake.AddRange(smoothShakePostProcessing.lensDistortionScaleShake);
                        smoothShakePostProcessingPreset.motionBlurShake.AddRange(smoothShakePostProcessing.motionBlurShake);
                        smoothShakePostProcessingPreset.paniniProjectionDistanceShake.AddRange(smoothShakePostProcessing.paniniProjectionDistanceShake);
                        smoothShakePostProcessingPreset.paniniProjectionCropShake.AddRange(smoothShakePostProcessing.paniniProjectionCropShake);
                        smoothShakePostProcessingPreset.splitToningBalanceShake.AddRange(smoothShakePostProcessing.splitToningBalanceShake);
                        smoothShakePostProcessingPreset.vignetteCenterShake.AddRange(smoothShakePostProcessing.vignetteCenterShake);
                        smoothShakePostProcessingPreset.vignetteIntensityShake.AddRange(smoothShakePostProcessing.vignetteIntensityShake);
                        smoothShakePostProcessingPreset.vignetteSmoothnessShake.AddRange(smoothShakePostProcessing.vignetteSmoothnessShake);
#if RENDER_HDRP && !RENDER_URP
                        smoothShakePostProcessingPreset.vignetteRoundnessShake.AddRange(smoothShakePostProcessing.vignetteRoundnessShake);
#endif
                        smoothShakePostProcessingPreset.whiteBalanceTemperatureShake.AddRange(smoothShakePostProcessing.whiteBalanceTemperatureShake);
                        smoothShakePostProcessingPreset.whiteBalanceTintShake.AddRange(smoothShakePostProcessing.whiteBalanceTintShake);
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        smoothShakePostProcessingPreset.depthOfFieldGaussianStartEndShake.AddRange(smoothShakePostProcessing.depthOfFieldGaussianStartEndShake);
                        smoothShakePostProcessingPreset.depthOfFieldGaussianMaxRadiusShake.AddRange(smoothShakePostProcessing.depthOfFieldGaussianMaxRadiusShake);
                        smoothShakePostProcessingPreset.depthOfFieldBokehFocusDistanceShake.AddRange(smoothShakePostProcessing.depthOfFieldBokehFocusDistanceShake);
                        smoothShakePostProcessingPreset.depthOfFieldBokehFocalLengthShake.AddRange(smoothShakePostProcessing.depthOfFieldBokehFocalLengthShake);
                        smoothShakePostProcessingPreset.depthOfFieldBokehApertureShake.AddRange(smoothShakePostProcessing.depthOfFieldBokehApertureShake);
                        smoothShakePostProcessingPreset.colorLookupShake.AddRange(smoothShakePostProcessing.colorLookupShake);
#elif RENDER_HDRP && !RENDER_UNIVERSAL
                        smoothShakePostProcessingPreset.depthOfFieldPhysicalFocusDistanceShake.AddRange(smoothShakePostProcessing.depthOfFieldPhysicalFocusDistanceShake);
                        smoothShakePostProcessingPreset.depthOfFieldManualNearStartEndShake.AddRange(smoothShakePostProcessing.depthOfFieldManualNearStartEndShake);
                        smoothShakePostProcessingPreset.depthOfFieldManualFarStartEndShake.AddRange(smoothShakePostProcessing.depthOfFieldManualFarStartEndShake);
                        smoothShakePostProcessingPreset.fogAttenuationShake.AddRange(smoothShakePostProcessing.fogAttenuationShake);
                        smoothShakePostProcessingPreset.fogBaseHeightShake.AddRange(smoothShakePostProcessing.fogBaseHeightShake);
                        smoothShakePostProcessingPreset.fogMaxHeightShake.AddRange(smoothShakePostProcessing.fogMaxHeightShake);
#endif
#if SMOOTHPOSTPROCESSING
                        smoothShakePostProcessingPreset.blurShake.AddRange(smoothShakePostProcessing.blurShake);
                        smoothShakePostProcessingPreset.displaceShake.AddRange(smoothShakePostProcessing.displaceShake);
                        smoothShakePostProcessingPreset.edgeDetectionShake.AddRange(smoothShakePostProcessing.edgeDetectionShake);
                        smoothShakePostProcessingPreset.glitchIntensityShake.AddRange(smoothShakePostProcessing.glitchIntensityShake);
                        smoothShakePostProcessingPreset.invertIntensityShake.AddRange(smoothShakePostProcessing.invertIntensityShake);
                        smoothShakePostProcessingPreset.kaleidoscopeShake.AddRange(smoothShakePostProcessing.kaleidoscopeShake);
                        smoothShakePostProcessingPreset.monitorScanlineShake.AddRange(smoothShakePostProcessing.monitorScanlineShake);
                        smoothShakePostProcessingPreset.monitorNoiseShake.AddRange(smoothShakePostProcessing.monitorNoiseShake);
                        smoothShakePostProcessingPreset.nightVisionBrightnessShake.AddRange(smoothShakePostProcessing.nightVisionBrightnessShake);
                        smoothShakePostProcessingPreset.nightVisionFlickerIntensityShake.AddRange(smoothShakePostProcessing.nightVisionFlickerIntensityShake);
                        smoothShakePostProcessingPreset.pixelSizeShake.AddRange(smoothShakePostProcessing.pixelSizeShake);
                        smoothShakePostProcessingPreset.rgbSplitShakeR.AddRange(smoothShakePostProcessing.rgbSplitShakeR);
                        smoothShakePostProcessingPreset.rgbSplitShakeG.AddRange(smoothShakePostProcessing.rgbSplitShakeG);
                        smoothShakePostProcessingPreset.rgbSplitShakeB.AddRange(smoothShakePostProcessing.rgbSplitShakeB);
                        smoothShakePostProcessingPreset.rotBlurShake.AddRange(smoothShakePostProcessing.rotBlurShake);
                        smoothShakePostProcessingPreset.rotBlurAngleShake.AddRange(smoothShakePostProcessing.rotBlurAngleShake);
                        smoothShakePostProcessingPreset.rotBlurCenterShake.AddRange(smoothShakePostProcessing.rotBlurCenterShake);
                        smoothShakePostProcessingPreset.solarizeIntensityShake.AddRange(smoothShakePostProcessing.solarizeIntensityShake);
                        smoothShakePostProcessingPreset.solarizeThresholdShake.AddRange(smoothShakePostProcessing.solarizeThresholdShake);
                        smoothShakePostProcessingPreset.solarizeSmoothnessShake.AddRange(smoothShakePostProcessing.solarizeSmoothnessShake);
                        smoothShakePostProcessingPreset.thermalVisionBrightnessShake.AddRange(smoothShakePostProcessing.thermalVisionBrightnessShake);
                        smoothShakePostProcessingPreset.thermalVisionContrastShake.AddRange(smoothShakePostProcessing.thermalVisionContrastShake);
                        smoothShakePostProcessingPreset.tintVignetteIntensityShake.AddRange(smoothShakePostProcessing.tintVignetteIntensityShake);
                        smoothShakePostProcessingPreset.tintNoiseIntensityShake.AddRange(smoothShakePostProcessing.tintNoiseIntensityShake);
                        smoothShakePostProcessingPreset.twirlStrengthShake.AddRange(smoothShakePostProcessing.twirlStrengthShake);
                        smoothShakePostProcessingPreset.twirlCenterShake.AddRange(smoothShakePostProcessing.twirlCenterShake);
                        smoothShakePostProcessingPreset.twirlRadiusShake.AddRange(smoothShakePostProcessing.twirlRadiusShake);
                        smoothShakePostProcessingPreset.twirlVignetteShake.AddRange(smoothShakePostProcessing.twirlVignetteShake);
                        smoothShakePostProcessingPreset.twitchOffsetShake.AddRange(smoothShakePostProcessing.twitchOffsetShake);
                        smoothShakePostProcessingPreset.waveWarpAmplitudeShake.AddRange(smoothShakePostProcessing.waveWarpAmplitudeShake);
                        smoothShakePostProcessingPreset.waveWarpVignetteShake.AddRange(smoothShakePostProcessing.waveWarpVignetteShake);
                        smoothShakePostProcessingPreset.zoomBlurShake.AddRange(smoothShakePostProcessing.zoomBlurShake);
                        smoothShakePostProcessingPreset.zoomBlurCenterShake.AddRange(smoothShakePostProcessing.zoomBlurCenterShake);
#if RENDER_UNIVERSAL && !RENDER_HDRP
                        smoothShakePostProcessingPreset.gaussianBlurShake.AddRange(smoothShakePostProcessing.gaussianBlurShake);
                        smoothShakePostProcessingPreset.noiseBloomStrengthShake.AddRange(smoothShakePostProcessing.noiseBloomStrengthShake);
                        smoothShakePostProcessingPreset.noiseBloomThresholdShake.AddRange(smoothShakePostProcessing.noiseBloomThresholdShake);
                        smoothShakePostProcessingPreset.noiseBloomRangeShake.AddRange(smoothShakePostProcessing.noiseBloomRangeShake);
#endif
#endif
                        smoothShakePostProcessing.preset = smoothShakePostProcessingPreset;
                    }
                    break;
#endif
            }

            AssetDatabase.CreateAsset(asset, savePath + "/" + name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //Selection.activeObject = asset;
        }

        public static void DrawTitle(string title)
        {
            //-------------------------------------------
            // Create a custom GUIStyle
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.fontSize = 18;  // Adjust the font size as needed
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            //-------------------------------------------

            EditorGUILayout.LabelField(title, titleStyle);
            EditorGUILayout.GetControlRect(false, 10);
        }

        public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        public static void DisplaySerializedProperties(SerializedObject obj, params string[] properties)
        {
            foreach (string property in properties)
            {
                SerializedProperty myDataProperty = obj.FindProperty(property);
                if (myDataProperty == null)
                {
                    Debug.LogError("Property '" + property + "' not found!");
                    continue;
                }
                EditorGUILayout.PropertyField(myDataProperty, true);
                obj.ApplyModifiedProperties();
            }
        }

        public static void DrawSerializedProperties(SerializedProperty property,ref Rect position, float padding, params string[] properties)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                SerializedProperty newProperty = property.FindPropertyRelative(properties[i]);

                if(newProperty == null)
                    Debug.LogError("Property '" + properties[i] + "' not found!");

                float propertyHeight = EditorGUI.GetPropertyHeight(newProperty);
                Rect fieldRect = new Rect(position.x, position.y, position.width, propertyHeight);
                EditorGUI.PropertyField(fieldRect, newProperty);
                position.y += propertyHeight + padding;
            }
        }

        public static string AddSpacesToSentence(string text, bool preserveAcronyms = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            string newText = Regex.Replace(text, "([a-z])([A-Z])", "$1 $2");
            if (preserveAcronyms)
            {
                newText = Regex.Replace(newText, "([A-Z])([A-Z][a-z])", "$1 $2");
            }
            return newText;
        }

        public static void DrawPropertiesHorizontally(SerializedProperty property,ref Rect position, string label, float padding, float minWidth, float[] weight, params string[] properties)
        {
            if (position.width < 5) return;

            Rect[] rects = new Rect[properties.Length];

            float[] minWidths = new float[properties.Length];
            for (int i = 0; i < minWidths.Length; i++) minWidths[i] = minWidth;

            Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            SplitRectsHorizontally(rect, padding, minWidths, weight, ref rects);

            for (int i = 0; i < properties.Length; i++)
            {
                if(i == 0) EditorGUI.PropertyField(rects[0], property.FindPropertyRelative(properties[0]), new GUIContent(label));
                else EditorGUI.PropertyField(rects[i], property.FindPropertyRelative(properties[i]), GUIContent.none);
            }
            position.y += EditorGUIUtility.singleLineHeight + padding;
        }

        public static void DrawDropdown(SerializedProperty property,ref Rect position,float padding,string label, string propertypath)
        {
            float dropdownHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative(propertypath));
            Rect dropdownRect = new Rect(position.x, position.y, position.width, dropdownHeight);
            EditorGUI.PropertyField(dropdownRect, property.FindPropertyRelative(propertypath), new GUIContent(label));
            position.y += dropdownHeight + padding;
        }

        public static bool ThinView(float viewWidth = 330f)
        {
            if (EditorGUIUtility.currentViewWidth > viewWidth)
                return false;
            else
                return true;
        }


        //------------------
        //Credit for this section goes to Wessel van der Es 
        public static void SplitRectsHorizontally(this Rect rect, float space, float[] minWidths, float[] weights, ref Rect[] rects)
        {
            int count = weights.Length;

            float budget = rect.width - (count - 1) * space - minWidths.Sum();
            float totalWeight = weights.Sum();

            float offset = 0.0f;
            float error = 0.0f;
            for (int i = 0; i < count; i++)
            {
                rects[i] = rect;
                rects[i].x += offset;
                rects[i].width = minWidths[i] + weights[i] / totalWeight * budget + error;

                float rounded = Mathf.Round(rects[i].width);
                error = rects[i].width - rounded;
                rects[i].width = rounded;

                offset += rects[i].width + space;
            }
        }

        public static float Sum(this IEnumerable<float> numbers)
        {
            float sum = 0.0f;
            foreach (float number in numbers)
                sum += number;
            return sum;
        }
        //------------------
    }

}
#endif