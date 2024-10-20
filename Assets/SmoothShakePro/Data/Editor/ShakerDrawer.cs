#if UNITY_EDITOR
using UnityEditor;
using static SmoothShakePro.Shaker;
using UnityEngine;

namespace SmoothShakePro
{
    internal class ShakerDrawer : PropertyDrawer
    {
        private readonly float padding = 2f;
        
        //-----------------------------------------------------------------------------------------
        //Noise type implementation

        //Noise type variable setup
        private readonly string[] waveVariables = new string[] {"amplitude", "frequency", "offset","phase","randomizePhase" };
        private readonly string[] baseVariables = new string[] { "amplitude", "offset"};
        private readonly string[] brownianNoiseVariables = new string[] { "amplitude", "offset", "stepSize", "maximum" };
        private readonly string[] customNoiseVariables = new string[] { "amplitude","frequency", "offset", "phase", "randomizePhase", "curve" };

        public void DrawNoiseTypeSettings(Rect position,SerializedProperty property, NoiseType noiseType)
        {
            switch (noiseType)
            {
                case NoiseType.SineWave:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, waveVariables);
                    break;
                case NoiseType.WhiteNoise:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, baseVariables);
                    break;
                case NoiseType.BrownianNoise:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, brownianNoiseVariables);
                    break;
                case NoiseType.PerlinNoise:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, waveVariables);
                    break;
                case NoiseType.GaussianNoise:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, baseVariables);
                    break;
                case NoiseType.SquareWave:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, waveVariables);
                    break;
                case NoiseType.Sawtooth:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, waveVariables);
                    break;
                case NoiseType.TriangleWave:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, waveVariables);
                    break;
                case NoiseType.Constant:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, baseVariables);
                    break;
                case NoiseType.Custom:
                    EditorUtility.DrawSerializedProperties(property, ref position, padding, customNoiseVariables);
                    break;
            }
        }

        public float GetPropertyAmount(NoiseType noiseType) => noiseType switch
        {
            NoiseType.SineWave => waveVariables.Length,
            NoiseType.WhiteNoise => baseVariables.Length,
            NoiseType.BrownianNoise => brownianNoiseVariables.Length,
            NoiseType.PerlinNoise => waveVariables.Length,
            NoiseType.GaussianNoise => baseVariables.Length,
            NoiseType.SquareWave => waveVariables.Length,
            NoiseType.Sawtooth => waveVariables.Length,
            NoiseType.TriangleWave => waveVariables.Length,
            NoiseType.Constant => baseVariables.Length,
            NoiseType.Custom => customNoiseVariables.Length,
            _ => throw new System.Exception("Unknown noise type")
        };

        //-----------------------------------------------------------------------------------------

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Get noise type info
            SerializedProperty noiseTypeProperty = property.FindPropertyRelative("noiseType");
            NoiseType noiseType = (NoiseType)noiseTypeProperty.enumValueIndex;

            //Draw the noiseType property
            EditorUtility.DrawDropdown(property, ref position, padding, "Noise Type", "noiseType");

            // Draw fields based on the selected noiseType
            DrawNoiseTypeSettings(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property, noiseType);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //Add height for noiseType property
            NoiseType noiseType = (NoiseType)property.FindPropertyRelative("noiseType").enumValueIndex;
            float propertiesHeight = GetPropertyAmount(noiseType) * (EditorGUIUtility.singleLineHeight + padding) + padding;
            if (EditorUtility.ThinView()) propertiesHeight += GetPropertyAmount(noiseType) * (EditorGUIUtility.singleLineHeight + padding);

            return propertiesHeight + (EditorGUIUtility.singleLineHeight + padding) + padding;
        }
    }
}
#endif