#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CustomPropertyDrawer(typeof(FloatShaker))]
    internal class FloatShakerDrawer : PropertyDrawer
    {
        private ShakerDrawer shakerDrawer;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            shakerDrawer ??= new ShakerDrawer();
            shakerDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            shakerDrawer ??= new ShakerDrawer();
            return shakerDrawer.GetPropertyHeight(property, label);
        }
    }
}
#endif
