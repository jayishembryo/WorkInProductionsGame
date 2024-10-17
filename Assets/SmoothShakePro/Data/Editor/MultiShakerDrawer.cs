#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SmoothShakePro.Shaker;

namespace SmoothShakePro
{
    [CustomPropertyDrawer(typeof(MultiVectorShaker))]
    internal class MultiShakerDrawer : PropertyDrawer
    {
        private ShakerDrawer shakerDrawer;

        private readonly float padding = 2f;

        //Init foldoutStates
#if UNITY_2020
        private static Dictionary<string, bool> foldoutStates = new Dictionary<string, bool>();
#else
        private static Dictionary<string, bool> foldoutStates = new();
#endif

        //Multi variables
        private readonly string[] generalVariables = new string[] { "blendingMode", "lifetime" };

        public string GetNoiseName(NoiseType noiseType) => noiseType switch
        {
            NoiseType.SineWave => "Sine Wave",
            NoiseType.WhiteNoise => "White Noise",
            NoiseType.BrownianNoise => "Brownian Noise",
            NoiseType.PerlinNoise => "Perlin Noise",
            NoiseType.GaussianNoise => "Gaussian Noise",
            NoiseType.SquareWave => "Square Wave",
            NoiseType.Sawtooth => "Sawtooth",
            NoiseType.TriangleWave => "Triangle Wave",
            NoiseType.Constant => "Constant",
            NoiseType.Custom => "Custom",
            _ => throw new System.Exception("Unknown noise type")
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty noiseTypeProperty = property.FindPropertyRelative("noiseType");
            NoiseType noiseType = (NoiseType)noiseTypeProperty.enumValueIndex;

            // Draw foldout
            InitFoldout(property, out string key);
            DrawFoldout(ref position, noiseType, key);
            if (!foldoutStates[key]) return;

            // Draw General Variables
            EditorUtility.DrawSerializedProperties(property, ref position, padding, generalVariables);

            shakerDrawer ??= new ShakerDrawer();
            shakerDrawer.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            InitFoldout(property, out string key);
            if (!foldoutStates[key]) return EditorGUIUtility.singleLineHeight + padding;

            //Add height for foldout
            float foldoutHeight = EditorGUIUtility.singleLineHeight + padding;

            shakerDrawer ??= new ShakerDrawer();
            return foldoutHeight + shakerDrawer.GetPropertyHeight(property, label) + ((EditorGUIUtility.singleLineHeight + padding) * generalVariables.Length);
        }

        private void InitFoldout(SerializedProperty property, out string key)
        {
            key = GetFoldoutKey(property);

            // Initialize foldout state if not present
            if (!foldoutStates.ContainsKey(key))
            {
                foldoutStates[key] = false;
            }
        }

        private void DrawFoldout( ref Rect position, NoiseType noiseType,string key)
        {
            foldoutStates[key] = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight + padding), foldoutStates[key], GetNoiseName(noiseType), true);
            position.y += EditorGUIUtility.singleLineHeight + padding;
        }

        private string GetFoldoutKey(SerializedProperty property)
        {
            return property.serializedObject.targetObject.GetInstanceID() + "-" + property.propertyPath;
        }
    }

}
#endif