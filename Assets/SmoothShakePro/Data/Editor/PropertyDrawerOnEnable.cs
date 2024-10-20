#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    //Credit for this script goes to IyroHaYuu on unity forums
    internal abstract class PropertyDrawerWithEvent : PropertyDrawer
    {
        bool init = true;

        private void SelectionChanged() => Disable();

        /// Write code for when the property is first displayed or redisplayed.
        public virtual void OnEnable(Rect position, SerializedProperty property, GUIContent label) { }

        /// Write code for when the property may be hidden.
        public virtual void OnDisable() { }

        public abstract void OnGUIWithEvent(Rect position, SerializedProperty property, GUIContent label);

        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (init) Enable(position, property, label);

            OnGUIWithEvent(position, property, label);
        }

        public void Enable(Rect position, SerializedProperty property, GUIContent label)
        {
            init = false;
            Selection.selectionChanged += SelectionChanged;
            OnEnable(position, property, label);
        }

        public void Disable()
        {
            OnDisable();
            Selection.selectionChanged -= SelectionChanged;
            init = true;
        }
    }

}
#endif