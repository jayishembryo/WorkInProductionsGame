#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    internal class ShakerPreview : Editor
    {
        public MultiShakeBase shakeBaseForPreview;
        public ShakeBasePreset presetForPreview;

#if UNITY_2020
        public Color color = new Color(0.94f, 0.15f, 0.3f);
#else
        public Color color = new(0.94f, 0.15f, 0.3f);
#endif

        public override bool HasPreviewGUI() { if (shakeBaseForPreview || presetForPreview) return true; else return false; }

        public override bool RequiresConstantRepaint() { if (shakeBaseForPreview || presetForPreview) return true; else return false; }

        public override void OnPreviewSettings()
        {
            if (shakeBaseForPreview || presetForPreview)
            {
                // Dropdown menu in the top bar of the preview
                PreviewTypes.DisplayPreviewShakerDropdown(shakeBaseForPreview, presetForPreview);
                if (shakeBaseForPreview)
                    shakeBaseForPreview.axisToPreview = (AxisToPreview)EditorGUILayout.EnumPopup(shakeBaseForPreview.axisToPreview, EditorStyles.toolbarPopup);
                else if (presetForPreview)
                    presetForPreview.axisToPreview = (AxisToPreview)EditorGUILayout.EnumPopup(presetForPreview.axisToPreview, EditorStyles.toolbarPopup);
            }
        }

        private void DisplayInfo(Rect rect)
        {
            if (shakeBaseForPreview || presetForPreview)
            {
                // Display cube info at the bottom, centered
                string cubeInfo = "";
                if (shakeBaseForPreview)
                    cubeInfo = "Currently previewing Axis " + shakeBaseForPreview.axisToPreview.ToString() + " of " + PreviewTypes.GetShakeName(shakeBaseForPreview);
                else if (presetForPreview)
                    cubeInfo = "Currently previewing Axis " + presetForPreview.axisToPreview.ToString() + " of " + PreviewTypes.GetShakeName(null, presetForPreview);

                GUIStyle centeredStyle = new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter
                };
                EditorGUI.LabelField(new Rect(rect.x, rect.yMax - 40, rect.width, 40), cubeInfo, centeredStyle); // Adjust these values for position and size
            }
        }

        private void DisplayPreview(Rect rect)
        {
            if (shakeBaseForPreview || presetForPreview)
            {
                // Create a material with a shader that supports vertex colors
                Material lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
                {
                    hideFlags = HideFlags.HideAndDontSave
                };
                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                lineMaterial.SetInt("_ZWrite", 0);
                lineMaterial.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);

                GUI.BeginClip(rect);
                {
                    // Set the material pass
                    lineMaterial.SetPass(0);

                    GL.Begin(GL.LINE_STRIP);
                    {
                        int numVertices = 500; // Number of vertices to draw the line. Increase for smoother lines.

                        GL.Color(color);

                        for (int i = 0; i < numVertices; i++)
                        {
                            float t = i / (float)(numVertices - 1); // Normalized position along the line
                            float time;

                            if (shakeBaseForPreview) time = Mathf.Lerp(0, shakeBaseForPreview.timeSettings.GetShakeDuration(), t); // Map to shake duration
                            else if (presetForPreview) time = Mathf.Lerp(0, presetForPreview.timeSettings.GetShakeDuration(), t); // Map to shake duration
                            else throw new Exception("No shakeBase or preset found");

                            float width = Mathf.Lerp(0, rect.width, t); // Map to rect width

                            float y = 0;
                            if (shakeBaseForPreview)
                            {
                                y = shakeBaseForPreview.axisToPreview switch
                                {
                                    AxisToPreview.X => 0.5f - 0.5f * ShakeSimulation.EvaluateMultiShaker(PreviewTypes.ShakerListForPreview(shakeBaseForPreview), shakeBaseForPreview.timeSettings, time).x,
                                    AxisToPreview.Y => 0.5f - 0.5f * ShakeSimulation.EvaluateMultiShaker(PreviewTypes.ShakerListForPreview(shakeBaseForPreview), shakeBaseForPreview.timeSettings, time).y,
                                    AxisToPreview.Z => 0.5f - 0.5f * ShakeSimulation.EvaluateMultiShaker(PreviewTypes.ShakerListForPreview(shakeBaseForPreview), shakeBaseForPreview.timeSettings, time).z,
                                    _ => throw new Exception("AxisToPreview not found"),
                                };
                            }
                            else if (presetForPreview)
                            {
                                y = presetForPreview.axisToPreview switch
                                {
                                    AxisToPreview.X => 0.5f - 0.5f * ShakeSimulation.EvaluateMultiShaker(PreviewTypes.ShakerListForPreview(null, presetForPreview), presetForPreview.timeSettings, time).x,
                                    AxisToPreview.Y => 0.5f - 0.5f * ShakeSimulation.EvaluateMultiShaker(PreviewTypes.ShakerListForPreview(null, presetForPreview), presetForPreview.timeSettings, time).y,
                                    AxisToPreview.Z => 0.5f - 0.5f * ShakeSimulation.EvaluateMultiShaker(PreviewTypes.ShakerListForPreview(null, presetForPreview), presetForPreview.timeSettings, time).z,
                                    _ => throw new Exception("AxisToPreview not found"),
                                };
                            }

                            float height = Mathf.Lerp(0, rect.height, y); // Map to rect height

                            GL.Vertex3(width, height, 0);
                        }
                    }
                    GL.End();
                }
                GUI.EndClip();

                // Clean up the material
                DestroyImmediate(lineMaterial);
            }
        }

        public override void OnPreviewGUI(Rect rect, GUIStyle background)
        {
            if (shakeBaseForPreview || presetForPreview)
            {
                DisplayPreview(rect);

                DisplayInfo(rect);
            }
        }
    }

}
#endif