using UnityEngine;

namespace SmoothShakePro
{
    [System.Serializable]
    public class MultiVectorShaker : VectorShaker, ISerializationCallbackReceiver
    {
        [Tooltip("The blending mode to use for this shaker")]
        public BlendingMode blendingMode;
        [Tooltip("The relative lifetime of this shaker (1 equals the duration set by the time settings)")]
        [Range(.00001f, 1)] public float lifetime = 1f;

        [HideInInspector] internal TimeSettings shakerTimeSettings;

        public void OnAfterDeserialize()
        {
            if(lifetime == 0f) lifetime = 1f;
        }

        public void OnBeforeSerialize() { }
    }
}
