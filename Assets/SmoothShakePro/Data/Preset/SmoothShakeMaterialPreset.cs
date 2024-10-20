using UnityEngine;

namespace SmoothShakePro
{
    [CreateAssetMenu(fileName = "SmoothShakeMaterialPreset", menuName = "Smooth Shake/Smooth Shake Material Preset", order = 4)]
    public class SmoothShakeMaterialPreset : ShakeBasePreset
    {
        [Header("Renderer / Material Settings")]
        [Tooltip("If true, the material will be taken from the renderer of this object, if false, the material assigned to this component will be used")]
        public bool useMaterialFromRenderer = true;
        [Tooltip("Material to shake, if useMaterialFromRenderer is false or renderer has multiple materials")]
        public Material material;

        [Header("Shake Settings")]
        public PropertyToShake propertyToShake;

#if UNITY_EDITOR
        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Float, Vector }
#endif
    }

}
