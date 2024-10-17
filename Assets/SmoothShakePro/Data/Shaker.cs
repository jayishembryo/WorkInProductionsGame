using System;
using UnityEngine;

namespace SmoothShakePro
{
    [Serializable]
    public abstract class Shaker
    {
        //Serializable Properties
        [Tooltip("The type of shake to use")]
        public NoiseType noiseType;

        public abstract Vector3 Evaluate(float t, float amplitudeMultiplier, Vector3 phase);

        public float GetAmplitude(float amplitude, float amplitudeMultiplier) => amplitude * amplitudeMultiplier;

        //Evaluate based on noise type
        protected float EvaluateBase(
            float t, 
            float amplitude, 
            float frequency, 
            float offset, 
            float phase,
            float maximum, 
            float stepSize,
            AnimationCurve curve,
            bool randomizePhase,
            float amplitudeMultiplier
            ) => noiseType switch
        {
            NoiseType.SineWave => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateSinewave(phase + frequency * t),
            NoiseType.WhiteNoise => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateWhiteNoise(),
            NoiseType.BrownianNoise => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateBrownianNoise(maximum, stepSize),
            NoiseType.PerlinNoise => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluatePerlinNoise(phase + frequency * t),
            NoiseType.GaussianNoise => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateGaussianNoise(),
            NoiseType.SquareWave => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateSquarewave(phase + frequency * t),
            NoiseType.Sawtooth => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateSawtooth(phase + frequency * t),
            NoiseType.TriangleWave => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateTrianglewave(phase + frequency * t),
            NoiseType.Constant => offset + GetAmplitude(amplitude, amplitudeMultiplier),
            NoiseType.Custom => offset + GetAmplitude(amplitude, amplitudeMultiplier) * EvaluateCustom(t,phase, frequency, curve),
            _ => throw new Exception("Unknown noise type")
        };

        //Enum to store noise type
        public enum NoiseType
        {
            SineWave,
            WhiteNoise,
            BrownianNoise,
            PerlinNoise,
            GaussianNoise,
            SquareWave,
            Sawtooth,
            TriangleWave,
            Constant,
            Custom
        }

        //SineWave
        private float EvaluateSinewave(float t) => Mathf.Sin(2 * Mathf.PI * t);

        //Whitenoise
        private float EvaluateWhiteNoise() => UnityEngine.Random.Range(-1f, 1f);

        //Brownian noise
        private float input = 0f;
        private float EvaluateBrownianNoise(float maximum, float stepSize)
        {
            if (input >= maximum)
                input += UnityEngine.Random.Range(-stepSize, 0);
            else if (input <= -maximum)
                input += UnityEngine.Random.Range(0, stepSize);
            else { input += UnityEngine.Random.Range(-stepSize, stepSize); }

            return input;
        }

        //Perlin noise
        private float EvaluatePerlinNoise(float t) => Utility.Remap(Mathf.PerlinNoise(-t,t),0,1,-1,1);

        //Gaussian noise
        private float EvaluateGaussianNoise()
        {
            float u1 = UnityEngine.Random.value;
            float u2 = UnityEngine.Random.value;

            float z0 = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Cos(2f * Mathf.PI * u2);

            return z0;
        }

        //SquareWave
        private float EvaluateSquarewave(float t) => (t % 2 > 1) ? 1 : -1;

        //Sawtooth
        private float EvaluateSawtooth(float t) => (t % 2) - 1;

        //TriangleWave
        private float EvaluateTrianglewave(float t) => Utility.Remap(Mathf.PingPong(t, 1),0,1,-1,1);

        //Custom
        private float EvaluateCustom(float t,float phase, float frequency, AnimationCurve curve)
        {
            // Adjust t to account for frequency
            t *= frequency;
            // Ensure t loops between 0 and 1 using modulo
            t %= 1.0f;
            return curve.Evaluate(phase + t);
        }
    }
}