#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    internal class PresetPopUp : PopupWindowContent
    {
        private Object target;
        private Editor editor;
        private Vector2 scrollPosition;
        public PresetPopUp(Object target) => this.target = target;
        public override void OnOpen() => editor = Editor.CreateEditor(target);
        public override void OnClose() => Object.DestroyImmediate(editor);

#if UNITY_2020
        public override Vector2 GetWindowSize() => new Vector2(320, 500);
#else
        public override Vector2 GetWindowSize() => new(320, 500);
#endif

        public override void OnGUI(Rect rect)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            editor.OnInspectorGUI();
            GUILayout.EndScrollView();
        }
    }
}
#endif
