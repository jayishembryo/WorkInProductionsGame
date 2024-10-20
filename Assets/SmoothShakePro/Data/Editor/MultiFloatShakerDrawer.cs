#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    [CustomPropertyDrawer(typeof(MultiFloatShaker))]
    internal class MultiFloatShakerDrawer : PropertyDrawer
    {
        private MultiShakerDrawer multiShakerDrawer;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            multiShakerDrawer ??= new MultiShakerDrawer();
            multiShakerDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            multiShakerDrawer ??= new MultiShakerDrawer();
            return multiShakerDrawer.GetPropertyHeight(property, label);
        }
    }

}
#endif