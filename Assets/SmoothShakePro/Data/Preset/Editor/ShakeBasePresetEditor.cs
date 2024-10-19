#if UNITY_EDITOR
using System.Collections.Generic;
using System;
using UnityEditor;
using System.Linq;

namespace SmoothShakePro
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ShakeBasePreset), true)]
    internal class ShakeBasePresetEditor : ShakerPreview
    {
        private void OnEnable()
        {
            presetForPreview = (ShakeBasePreset)target;
        }

        public override void OnInspectorGUI()
        {
            ShakeBasePreset preset = (ShakeBasePreset)target;

            switch (preset)
            {
                case SmoothShakePreset ssp:
                    EditorUtility.DrawTitle("Smooth Shake");
                    break;
#if CINEMACHINE
                case SmoothShakeCinemachinePreset sscp:
                    EditorUtility.DrawTitle("Smooth Shake Cinemachine");
                    break;
#endif
                case SmoothShakeRigidbodyPreset ssrp:
                    EditorUtility.DrawTitle("Smooth Shake Rigidbody");
                    break;
                case SmoothShakeMaterialPreset ssmp:
                    EditorUtility.DrawTitle("Smooth Shake Material");
                    break;
                case SmoothShakeLightPreset sslp:
                    EditorUtility.DrawTitle("Smooth Shake Light");
                    break;
                case SmoothShakeAudioPreset ssap:
                    EditorUtility.DrawTitle("Smooth Shake Audio");
                    break;
                case SmoothShakeHapticsGamepadPreset sshp:
                    EditorUtility.DrawTitle("Smooth Shake Haptics");
                    break;
#if UNITY_XR
                case SmoothShakeHapticsXRPreset sshxr:
                    EditorUtility.DrawTitle("Smooth Shake Haptics XR");
                    break;
#endif
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
                case SmoothShakePostProcessingPreset:
                    EditorUtility.DrawTitle("Smooth Shake Post Processing");
                    break;
#endif
            }

            EditorUtility.DrawUILine(color, 1, 10);

            serializedObject.Update();
#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
            if (preset is SmoothShakePostProcessingPreset sspp)
            {
 
                CustomPostProcessingDrawer(sspp);

            }
            else
            {
                DrawPropertiesExcluding(serializedObject, "m_Script");
            }
#else
            DrawPropertiesExcluding(serializedObject, "m_Script");
#endif
            serializedObject.ApplyModifiedProperties();
        }


#if UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
        private void CustomPostProcessingDrawer(SmoothShakePostProcessingPreset sspp)
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

            var propertiesToHide = baseHide.Union(
                EditorUtility.postProcesPropertyMap.Values.SelectMany(x => x)
            ).Except(propertiesToShow).ToArray();

            DrawPropertiesExcluding(serializedObject, propertiesToHide);
        }
#endif
    }

}
#endif
