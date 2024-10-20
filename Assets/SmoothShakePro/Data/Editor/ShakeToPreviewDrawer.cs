#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CustomPropertyDrawer(typeof(SmoothShake.ShakeToPreview))]
    internal class ShakeToPreviewDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SmoothShake smoothShake = property.serializedObject.targetObject as SmoothShake;

            if (smoothShake != null)
            {
                EditorGUI.BeginProperty(position, label, property);

                // Check if the object has a camera component
                bool hasCamera = smoothShake.GetComponent<Camera>() != null;

                // Create a list of displayed options and corresponding option values
                string[] displayedOptions;
                int[] optionValues;
                if (hasCamera)
                {
                    displayedOptions = new[] {"Position", "Rotation", "FOV" };
                    optionValues = new[] { 0, 1, 3 };
                }
                else
                {
                    displayedOptions = new[] { "Position", "Rotation", "Scale" };
                    optionValues = new[] { 0, 1, 2 };
                }

                // Draw the label
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                // Draw the popup
                int selectedValue = property.intValue;
                int selectedIndex = System.Array.IndexOf(optionValues, selectedValue);
                selectedIndex = EditorGUI.Popup(position, selectedIndex, displayedOptions);
                if (selectedIndex >= 0) property.intValue = optionValues[selectedIndex];

                EditorGUI.EndProperty();
            }
        }
    }
}
#endif