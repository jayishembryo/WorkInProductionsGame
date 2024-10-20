using UnityEngine;

namespace SmoothShakePro
{
    [System.Serializable]
    public class FloatShaker : Shaker
    {
        //Serializable properties
        [Tooltip("The amplitude (strength) of this shaker")]
        public float amplitude;
        [Tooltip("The frequency (speed) of this shaker")]
        public float frequency;
        [Tooltip("The value to offset this shaker")]
        public float offset;
        [Tooltip("The phase of this shaker")]
        public float phase;
        [Tooltip("Randomize the phase of this shaker")]
        public bool randomizePhase;

        //Brownian Noise variables
        [Tooltip("The maximum value the values can grow to")]
        public float maximum;
        [Tooltip("The size for the noise accumulation steps")]
        public float stepSize;

        //Custom noise variables
        [Tooltip("The custom curve to use for custom noise")]
        public AnimationCurve curve;

        //Convert to Vector3
        public override Vector3 Evaluate(float t, float amplitudeMultiplier, Vector3 newPhase)
        {
            Vector3 modified;
            modified.x = EvaluateBase(t, amplitude, frequency, offset, newPhase.x, maximum, stepSize, curve, randomizePhase, amplitudeMultiplier);
            modified.y = 0;
            modified.z = 0;
            return modified;
        }
    }

}
