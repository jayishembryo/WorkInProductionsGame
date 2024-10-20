#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    internal class InputPopUp : EditorWindow
    {
        public static string shakeName = "New Shake Preset";
        public static string savePath = "Assets/SmoothShakePro/Presets";
        private ShakeBaseEditor parentEditor;

        public static void ShowWindow(ShakeBaseEditor parentEditor)
        {
            InputPopUp window = GetWindow<InputPopUp>("Save Preset");
            window.parentEditor = parentEditor;

            window.minSize = new Vector2(100, 300);
            window.maxSize = new Vector2(250, 300);
        }

        private void OnGUI()
        {
            if (parentEditor == null) return;

            GUILayout.Label("File name: ");
            shakeName = EditorGUILayout.TextField(shakeName);

            if (EditorUtility.changeFileLocationPerSave)
            {
                GUILayout.Label("Save location: ");
                savePath = EditorGUILayout.TextField(savePath);
            }
            else
                savePath = EditorPrefs.GetString("DefaultSavePath", "Assets/SmoothShakePro/Presets"); 


            if (GUILayout.Button("Save"))
            {
                parentEditor.OnInputSubmitted(shakeName, savePath);
                Close();
            }

            //Helpbox warning
            EditorGUILayout.HelpBox("Using the same name as an exisiting preset will overwrite it", MessageType.Info);
        }
    }

}
#endif