#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CustomEditor(typeof(SmoothShakeManager))]
    internal class SmoothShakeManagerEditor : Editor
    {
        bool[] foldoutStates;
        int prevSavedShakesCount = 0;

        private void OnEnable()
        {
            InitFoldout();
        }

        private void InitFoldout()
        {
            SmoothShakeManager manager = (SmoothShakeManager)target;
            if(manager != null)
            {
                if (foldoutStates == null || foldoutStates.Length != manager.savedShakes.Count)
                {
                    foldoutStates = new bool[manager.savedShakes.Count];
                }
            }
            else
            {
                Debug.LogError("SmoothShakeManager is null");
            }

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            InitFoldout();

            EditorUtility.DrawTitle("Smooth Shake Manager");

            EditorUtility.DrawUILine(EditorUtility.SmoothShakeColor, 1, 10);

            DrawPropertiesExcluding(serializedObject, "m_Script");

#if UNITY_2020
            GUIStyle titleFont = new GUIStyle(GUI.skin.label);
#else
            GUIStyle titleFont = new(GUI.skin.label);
#endif
            titleFont.fontStyle = FontStyle.Bold;
            titleFont.fontSize = 14;

            SmoothShakeManager manager = (SmoothShakeManager)target;

            if (manager.savedShakes != null)
            {
                if (manager.savedShakes.Count > 0)
                {
                    if (manager.savedShakes.Count != prevSavedShakesCount)
                    {
                        Array.Resize(ref foldoutStates, manager.savedShakes.Count);
                        prevSavedShakesCount = manager.savedShakes.Count;
                    }

                    EditorGUILayout.LabelField("Saved Shakes", titleFont);
                    EditorGUILayout.GetControlRect(false, 10);

                    for (int i = 0; i < manager.savedShakes.Count; i++)
                    {
                        SavedShake savedShake = manager.savedShakes[i];

                        if (savedShake == null)
                            continue;

                        if (savedShake.name != "")
                        {
                            foldoutStates[i] = EditorGUILayout.Foldout(foldoutStates[i], savedShake.name);

                            if (foldoutStates[i])
                            {
                                EditorGUILayout.BeginVertical("Box");
                                {
                                    //Title
                                    EditorGUILayout.LabelField("Settings for " + savedShake.name, titleFont);

                                    EditorGUI.BeginChangeCheck();
                                    Editor shakeEditor = CreateEditor(savedShake.shake);
                                    if (shakeEditor != null) shakeEditor.OnInspectorGUI();
                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        serializedObject.ApplyModifiedProperties();
                                    }
                                }
                                EditorGUILayout.EndVertical();

                            }
                            GUI.enabled = true;
                        }
                        else
                        {
                            Debug.LogWarning("Shake name is empty");
                        }
                        
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}
#endif