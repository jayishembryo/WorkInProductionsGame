#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CustomEditor(typeof(SmoothShakeRandomizer))]
    internal class SmoothShakeRandomizerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorUtility.DrawTitle("Smooth Shake Randomizer");

            EditorUtility.DrawUILine(EditorUtility.SmoothShakeColor, 1, 10);

            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script");

            SmoothShakeRandomizer randomizer = (SmoothShakeRandomizer)target;

            if (GUILayout.Button("Randomize"))
            {
                randomizer.Randomize();
                Debug.Log("Succesfully randomized " + randomizer.shakes.Count + " shakes");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
