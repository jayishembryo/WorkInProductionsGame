#if UNITY_EDITOR
using UnityEditor;

namespace SmoothShakePro
{
    [CustomEditor(typeof(SmoothShakeHover))]
    internal class SmoothShakeHoverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorUtility.DrawTitle("Smooth Shake Hover");

            EditorUtility.DrawUILine(EditorUtility.SmoothShakeColor, 1, 10);

            DrawPropertiesExcluding(serializedObject, "m_Script");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif