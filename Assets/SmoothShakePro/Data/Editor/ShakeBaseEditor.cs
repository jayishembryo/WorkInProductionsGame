#if UNITY_EDITOR
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ShakeBase), true)]
    internal class ShakeBaseEditor : ShakerPreview
    {
        private ShakeBase shakeBase;
        private void OnEnable()
        {
            if (target is MultiShakeBase msb) shakeBaseForPreview = msb;

            shakeBase = shakeBase != null ? shakeBase : target as ShakeBase;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ShakeBase shakeBase = (ShakeBase)target;

            if(!shakeBase.initialized)
            {
                shakeBase.Initialize();
                shakeBase.initialized = true;
            }

            switch (shakeBase)
            {
                case SmoothShake ss:
                    EditorUtility.DrawTitle("Smooth Shake");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ss.preset, ss);
                    break;
#if CINEMACHINE
                case SmoothShakeCinemachine ssc:
                    EditorUtility.DrawTitle("Smooth Shake Cinemachine");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ssc.preset, ssc);
                    break;
#endif
                case SmoothShakeRigidbody ssr:
                    EditorUtility.DrawTitle("Smooth Shake Rigidbody");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ssr.preset, ssr);
                    break;
                case SmoothShakeMaterial ssm:
                    EditorUtility.DrawTitle("Smooth Shake Material");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ssm.preset, ssm);
                    break;
                case SmoothShakeLight ssl:
                    EditorUtility.DrawTitle("Smooth Shake Light");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ssl.preset, ssl);
                    break;
                case SmoothShakeAudio ssa:
                    EditorUtility.DrawTitle("Smooth Shake Audio");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ssa.preset, ssa);
                    break;
                case SmoothShakeStarter sss:
                    EditorUtility.DrawTitle("Smooth Shake Starter");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPropertiesExcluding(serializedObject, "m_Script", "timeSettings");

                    //Button for add increments
                    if (GUILayout.Button("Add Increments"))
                    {
                        sss.AddIncrements();
                    }

                    break;
                case SmoothShakeHapticsGamepad ssh:
                    EditorUtility.DrawTitle("Smooth Shake Haptics");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ssh.preset, ssh);
                    break;
#if UNITY_XR
                case SmoothShakeHapticsXR sshxr:
                    EditorUtility.DrawTitle("Smooth Shake Haptics XR");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(sshxr.preset, sshxr);
                    break;
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                case SmoothShakePostProcessing ssp:
                    EditorUtility.DrawTitle("Smooth Shake Post Processing");
                    EditorUtility.DrawUILine(color, 1, 10);
                    DrawPresetInspector(ssp.preset, ssp);
                    break;
#endif
            }

            EditorUtility.DrawUILine(Color.gray, 1, 10);

            //Draw test buttons
            DrawTestButtons(shakeBase);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPresetInspector(ShakeBasePreset preset, ShakeBase shakeBase)
        {
            //Draw preset property

            //Disable preset field GUI if application is playing

            if (Application.isPlaying)
            {
                GUI.enabled = false;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("preset"), true);

            GUI.enabled = true;

            if (preset)
            {
                if (Application.isPlaying)
                {
                    shakeBase.runtimePresetEditing = EditorGUILayout.Toggle("Runtime Preset Editing", shakeBase.runtimePresetEditing);
                    if (shakeBase.runtimePresetEditing) shakeBase.ApplyPresetSettings(preset);
                }
                else shakeBase.ApplyPresetSettings(preset);

                //Helpbox
                EditorGUILayout.HelpBox("This shake being overriden by " + preset.name, MessageType.Info);

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

                //Draw global multiplier property
                EditorGUILayout.PropertyField(serializedObject.FindProperty("amplitudeMultiplier"), true);

                if(shakeBase is SmoothShakeHapticsGamepad)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("gamepadSelectionMethod"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("customIndex"), true);
                }

#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                if (shakeBase is SmoothShakePostProcessing)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("volume"), true);
                }
#endif
            }
            else
            {
                DrawSavePresetButton();

                switch (shakeBase)
                {
                    case SmoothShake ss:
                        CustomSmoothShakeDrawer(ss);
                        break;
                    case SmoothShakeMaterial ssm:
                        CustomMaterialDrawer(ssm);
                        break;
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                    case SmoothShakePostProcessing sspp:
                        CustomPostProcessingDrawer(sspp);
                        break;
#endif
                    default:
                        DrawPropertiesExcluding(serializedObject, "m_Script", "preset");
                        break;
                }
            }
        }

        void DrawSavePresetButton()
        {
            if (GUILayout.Button("Save As New Preset..."))
            {
                InputPopUp.ShowWindow(this);
            }
        }

        public void OnInputSubmitted(string name, string savePath)
        {
            switch (shakeBase)
            {
                case SmoothShake ss:
                    EditorUtility.CreateMyAsset<SmoothShakePreset>(name, savePath, shakeBase);
                    break;
#if CINEMACHINE
                case SmoothShakeCinemachine ssc:
                    EditorUtility.CreateMyAsset<SmoothShakeCinemachinePreset>(name, savePath, shakeBase);
                    break;
#endif
                case SmoothShakeRigidbody ssr:
                    EditorUtility.CreateMyAsset<SmoothShakeRigidbodyPreset>(name, savePath, shakeBase);
                    break;
                case SmoothShakeMaterial ssm:
                    EditorUtility.CreateMyAsset<SmoothShakeMaterialPreset>(name, savePath, shakeBase);
                    break;
                case SmoothShakeLight ssl:
                    EditorUtility.CreateMyAsset<SmoothShakeLightPreset>(name, savePath, shakeBase);
                    break;
                case SmoothShakeAudio ssa:
                    EditorUtility.CreateMyAsset<SmoothShakeAudioPreset>(name, savePath, shakeBase);
                    break;
                case SmoothShakeHapticsGamepad ssh:
                    EditorUtility.CreateMyAsset<SmoothShakeHapticsGamepadPreset>(name, savePath, shakeBase);
                    break;
#if UNITY_XR
                case SmoothShakeHapticsXR sshxr:
                    EditorUtility.CreateMyAsset<SmoothShakeHapticsXRPreset>(name, savePath, shakeBase);
                    break;
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                case SmoothShakePostProcessing ssp:
                    EditorUtility.CreateMyAsset<SmoothShakePostProcessingPreset>(name, savePath, shakeBase);
                    break;
#endif
                default:
                    break;
            }
        }

        void DrawTestButtons(ShakeBase shakeBase)
        {
            //Test start and stop buttons in horizontal
            EditorGUILayout.BeginHorizontal();
            {
                //Disable buttons in edit mode
                GUI.enabled = Application.isPlaying;
                if (GUILayout.Button("Test Shake"))
                    shakeBase.StartShake();
                if (GUILayout.Button("Stop Test Shake"))
                    shakeBase.StopShake();
                if (GUILayout.Button("Force Stop Test Shake"))
                    shakeBase.ForceStop();
            }
            EditorGUILayout.EndHorizontal();

            if (!GUI.enabled)
                EditorGUILayout.HelpBox("You can only test shakes play mode", MessageType.Info);
        }

        private void CustomSmoothShakeDrawer(SmoothShake ss)
        {
            string hide;
            if (ss.gameObject.GetComponent<Camera>() != null) hide = "scaleShake";
            else hide = "FOVShake";
            DrawPropertiesExcluding(serializedObject, "m_Script", "preset", hide);
        }

        private void CustomMaterialDrawer(SmoothShakeMaterial ssm)
        {
            if(!ssm.useMaterialFromRenderer)
            {
                DrawPropertiesExcluding(serializedObject, "m_Script", "preset", "objectRenderer");
            }
            else
            {
                if (ssm.CheckRenderer()) if (ssm.GetComponent<Renderer>().sharedMaterials.Length > 1) DrawPropertiesExcluding(serializedObject, "m_Script", "preset");

                else DrawPropertiesExcluding(serializedObject, "m_Script", "preset", "material");
            }
        }

#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
        private void CustomPostProcessingDrawer(SmoothShakePostProcessing sspp)
        {
            var baseHide = new HashSet<string> { "m_Script", "preset" };

            var propertiesToShow = new HashSet<string>();

            foreach (Overrides flag in Enum.GetValues(typeof(Overrides)))
            {
                if (sspp.overrides.HasFlag(flag) && EditorUtility.postProcesPropertyMap.ContainsKey(flag))
                {
                    propertiesToShow.UnionWith(EditorUtility.postProcesPropertyMap[flag]);
                }
            }

#if SMOOTHPOSTPROCESSING
            foreach (SmoothPostProcessingOverrides flag in Enum.GetValues(typeof(SmoothPostProcessingOverrides)))
            {
                if (sspp.smoothPostProcessingOverrides.HasFlag(flag) && EditorUtility.postProcesPropertyMapSmoothShakePro.ContainsKey(flag))
                {
                    propertiesToShow.UnionWith(EditorUtility.postProcesPropertyMapSmoothShakePro[flag]);
                }
            }
#endif

            var propertiesToHide = baseHide
                .Union(EditorUtility.postProcesPropertyMap.Values.SelectMany(x => x))
#if SMOOTHPOSTPROCESSING
                .Union(EditorUtility.postProcesPropertyMapSmoothShakePro.Values.SelectMany(x => x))
#endif
                .Except(propertiesToShow)
                .ToArray();

            DrawPropertiesExcluding(serializedObject, propertiesToHide);
        }

#endif
    }
}
#endif