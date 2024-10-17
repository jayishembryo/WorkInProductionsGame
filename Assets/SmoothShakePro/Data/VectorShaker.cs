using UnityEngine;

namespace SmoothShakePro
{
    [System.Serializable]
    public class VectorShaker : Shaker
    {
        //Serializable properties
        [Tooltip("The amplitude (strength) of this shaker")]
        public Vector3 amplitude;
        [Tooltip("The frequency (speed) of this shaker")]
        public Vector3 frequency;
        [Tooltip("The value to offset this shaker")]
        public Vector3 offset;
        [Tooltip("The phase of this shaker")]
        public Vector3 phase;
        [Tooltip("Randomize the phase of this shaker")]
        public bool randomizePhase;

        //Brownian Noise variables
        [Tooltip("The maximum value the values can grow to")]
        public Vector3 maximum;
        [Tooltip("The size for the noise accumulation steps")]
        public Vector3 stepSize;


        //Custom noise variables
        [Tooltip("The custom curve to use for custom noise")]
        public AnimationCurve curve;

        //Convert to Vector3
        public override Vector3 Evaluate(float t, float amplitudeMultiplier, Vector3 newPhase)
        {
            Vector3 modified;
            modified.x = EvaluateBase(t, amplitude.x, frequency.x, offset.x, newPhase.x, maximum.x, stepSize.x, curve, randomizePhase, amplitudeMultiplier);
            modified.y = EvaluateBase(t, amplitude.y, frequency.y, offset.y, newPhase.y, maximum.y, stepSize.y, curve, randomizePhase, amplitudeMultiplier);
            modified.z = EvaluateBase(t, amplitude.z, frequency.z, offset.z, newPhase.z, maximum.z, stepSize.z, curve, randomizePhase, amplitudeMultiplier);
            return modified;
        }
    }

}
