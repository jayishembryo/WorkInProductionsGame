#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    internal class GlobalSettings : EditorWindow
    {
        public static string basePath = "Assets/SmoothShakePro";

        public static GlobalSettingsObject globalSettings;

        public static bool wrongBasePath;

        public static void GetGlobalSettings()
        {
            //If path is not valid, log a warning
            if (!AssetDatabase.IsValidFolder($"{basePath}/Data/Editor"))
            {
                globalSettings = null;
                //EditorUtility.CreateFoldersForInvalidPaths($"{basePath}/Data/Editor");
                wrongBasePath = true;
                return;
            }
            else { wrongBasePath = false; }

            if (globalSettings == null)
            {
                globalSettings = AssetDatabase.LoadAssetAtPath<GlobalSettingsObject>($"{basePath}/Data/Editor/GlobalSettingsObject.asset");
            }

            //If it is still null, create a new one at path
            if (globalSettings == null)
            {
                globalSettings = ScriptableObject.CreateInstance<GlobalSettingsObject>();
                AssetDatabase.CreateAsset(globalSettings, $"{basePath}/Data/Editor/GlobalSettingsObject.asset");
                AssetDatabase.SaveAssets();
                globalSettings.basePath = basePath;
            }

            //Warning if global settings object is not found
            if (globalSettings == null) Debug.LogWarning("Global settings object not found. Please create a GlobalSettingsObject in the SmoothShakePro/Data/Editor folder.");
        }

        [MenuItem("Window/SmoothShakePro/Global Settings ...")]
        public static void ShowExample()
        {
            GlobalSettings gSettings = GetWindow<GlobalSettings>();
            gSettings.titleContent = new GUIContent("Global Smooth Shake Settings");
            gSettings.minSize = new Vector2(500, 500);
        }

        [MenuItem("Window/SmoothShakePro/Showcase and Tutorial")]
        public static void TutorialLink()
        {
            Application.OpenURL("https://youtu.be/SFpfRgB9yh0?si=8H1EVeIFZ1tNjTdt&t=170");
        }

        [MenuItem("Window/SmoothShakePro/Leave a review")]
        public static void ReviewLink()
        {
            Application.OpenURL("https://assetstore.unity.com/packages/tools/animation/smooth-shake-pro-271080#reviews");
        }

        private void OnEnable()
        {
            if (globalSettings == null) GetGlobalSettings();
        }

        [InitializeOnLoadMethod]
        private static void LoadStaticVariable()
        {
            basePath = EditorPrefs.GetString("SmoothShakePro_BasePath", "Assets/SmoothShakePro");
        }

        private void OnGUI()
        {
            if (globalSettings == null) GetGlobalSettings();

            // Display and edit a string property
            EditorUtility.DrawTitle("Global Smooth Shake Settings");

            EditorUtility.DrawUILine(EditorUtility.SmoothShakeColor, 1, 10);

            GUILayout.Space(10);

            if (wrongBasePath)
            {
                //Display global settings base path
                basePath = EditorGUILayout.TextField("Base path", basePath);
                EditorGUILayout.HelpBox("The base path is not valid. Please check the path.", MessageType.Warning);
            }
            else
            {
                if (globalSettings != null) globalSettings.basePath = basePath;
                EditorPrefs.SetString("SmoothShakePro_BasePath", basePath);
            }


            if (globalSettings != null)
            {
                globalSettings.defaultSavePath = EditorGUILayout.TextField("Default Preset Save Path", globalSettings.defaultSavePath);

                globalSettings.changeFileLocationPerSave = EditorGUILayout.Toggle("Change save path per save", globalSettings.changeFileLocationPerSave);
                EditorUtility.changeFileLocationPerSave = globalSettings.changeFileLocationPerSave;

                globalSettings.addDefinitionOnInitiating = EditorGUILayout.Toggle("Add Definition in Player Settings", globalSettings.addDefinitionOnInitiating);
                EditorUtility.addDefinitionOnInitiating = globalSettings.addDefinitionOnInitiating;

                globalSettings.defaultTimescaleMode = (TimescaleMode)EditorGUILayout.EnumPopup("Default Timescale Mode", globalSettings.defaultTimescaleMode);
                EditorUtility.defaultTimescaleMode = globalSettings.defaultTimescaleMode;

                globalSettings.defaultCustomTimescale = EditorGUILayout.FloatField("Default Custom Timescale", globalSettings.defaultCustomTimescale);
                EditorUtility.defaultCustomTimescale = globalSettings.defaultCustomTimescale;

#if !UNITY_2020
                globalSettings.SmoothShakeColor = EditorGUILayout.ColorField("Smooth Shake Color", globalSettings.SmoothShakeColor);
                EditorUtility.SmoothShakeColor = globalSettings.SmoothShakeColor;
#endif

                // Save the changes to the globalSettings object
                if (GUI.changed)
                {
                    UnityEditor.EditorUtility.SetDirty(globalSettings);
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }

}
#endif
