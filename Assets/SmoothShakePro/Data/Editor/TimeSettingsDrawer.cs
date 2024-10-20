#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CustomPropertyDrawer(typeof(TimeSettings))]
    internal class TimeSettingsDrawer : PropertyDrawer
    {
        public float padding = 2f;
        private bool showAdvancedSettings = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Calculate the background box rect
            Rect backgroundRect = new Rect(position.x - (padding * 2), position.y, position.width + (padding * 2), GetPropertyHeight(property, label) - padding);

            // Draw the background box
            EditorGUI.DrawRect(backgroundRect, new Color(0.18f, 0.18f, 0.18f, 0.5f));

            // Add some padding to the content
            position.x += padding;
            position.y += padding;
            position.width -= 2 * padding;


            //Draw enableOnStart and constantShake
            EditorUtility.DrawSerializedProperties(property, ref position, padding, "enableOnStart", "constantShake", "loop");

            // Draw fadeInDuration and fadeInCurve 
            EditorUtility.DrawPropertiesHorizontally(property, ref position, "Fade In", padding, 25f, new float[] { 1.8f, 1 }, "fadeInDuration", "fadeInCurve");

            // Draw holdDuration
            EditorUtility.DrawSerializedProperties(property, ref position, padding, "holdDuration");

            // Draw fadeOutDuration and fadeOutCurve
            EditorUtility.DrawPropertiesHorizontally(property, ref position, "Fade Out", padding, 25f, new float[] { 1.8f, 1 }, "fadeOutDuration", "fadeOutCurve");

            ////Draw timescale enum and customTimescale horizontally
            //EditorUtility.DrawSerializedProperties(property, ref position, padding, "timescaleMode", "customTimescale");

            // Advanced Settings Foldout
            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            showAdvancedSettings = EditorGUI.Foldout(foldoutRect, showAdvancedSettings, "Advanced", true);

            position.y += EditorGUIUtility.singleLineHeight;

            if (showAdvancedSettings)
            {
                EditorGUI.indentLevel++;
                // Draw timescale enum and customTimescale
                EditorUtility.DrawSerializedProperties(property, ref position, padding, "timescaleMode", "customTimescale");
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = (EditorGUIUtility.singleLineHeight + padding) * 6f; // Base height for main properties
            height += EditorGUIUtility.singleLineHeight + padding; // Height for the foldout

            if (showAdvancedSettings)
            {
                height += (EditorGUIUtility.singleLineHeight + padding) * 2; // Height for timescale properties
            }

            return height + 2 * padding; // Add extra padding for the background box
        }
    }

}
#endif