using System.Collections.Generic;
using System;
using UnityEngine;

namespace SmoothShakePro
{
    public static class ShakeSimulation
    {
        public static Vector3 EvaluateMultiShaker(IEnumerable<Shaker> multiShaker, TimeSettings timeSettings, float t)
        {
            Vector3 sum = Vector3.zero;
            foreach (Shaker shaker in multiShaker)
            {
                var evaluatedValue = shaker.Evaluate(t, 1, Utility.GetPhase(shaker)) * EvaluateFadeValue(shaker, timeSettings, t);
                Func<Vector3, Vector3, Vector3> blendingModeFunc;

                if (shaker is MultiVectorShaker multiVectorShaker)
                    blendingModeFunc = BlendingModes.GetBlendingMode(multiVectorShaker.blendingMode);
                else if (shaker is MultiFloatShaker multiFloatShaker)
                    blendingModeFunc = BlendingModes.GetBlendingMode(multiFloatShaker.blendingMode);
                else
                    throw new Exception("Shaker is not a MultiShaker");

                sum = blendingModeFunc(sum, evaluatedValue);
            }
            return sum;
        }

        private enum ShakePhase
        {
            FadeIn,
            Hold,
            FadeOut,
            None
        }

        // Determine the Shake Phase
        private static ShakePhase DetermineShakePhase(float t, TimeSettings timeSettings)
        {
            if (t < timeSettings.fadeInDuration)
                return ShakePhase.FadeIn;
            if (t < timeSettings.fadeInDuration + timeSettings.holdDuration)
                return ShakePhase.Hold;
            if (t < timeSettings.fadeInDuration + timeSettings.holdDuration + timeSettings.fadeOutDuration)
                return ShakePhase.FadeOut;
            return ShakePhase.None;
        }

        // Compute Fade Value
        private static float ComputeFadeValue(AnimationCurve curve, float duration, bool isFadingOut, float t)
        {
            if (curve.length <= 1)
                return isFadingOut ? 0 : 1;

            Keyframe[] keys = curve.keys;
#if UNITY_2020
            float remappedTime = Utility.Remap(t, 0, duration, keys[0].time, keys[keys.Length - 1].time);
#else
            float remappedTime = Utility.Remap(t, 0, duration, keys[0].time, keys[^1].time);
#endif
            //float fadeValue = Utility.Remap(curve.Evaluate(remappedTime), keys[0].value, keys[^1].value, isFadingOut ? 1 : 0, isFadingOut ? 0 : 1);
            float fadeValue = curve.Evaluate(remappedTime);
            return fadeValue;
        }

        public static float EvaluateFadeValue(Shaker shaker, TimeSettings timeSettings, float t)
        {
            if (shaker is MultiVectorShaker || shaker is MultiFloatShaker) timeSettings = RelativeTimeSettings(shaker, timeSettings);

            bool isFadingOut;
            ShakePhase phase = DetermineShakePhase(t, timeSettings);

            float fadeValue = 0;

            switch (phase)
            {
                case ShakePhase.FadeIn:
                    isFadingOut = false;
                    fadeValue = ComputeFadeValue(timeSettings.fadeInCurve, timeSettings.fadeInDuration, isFadingOut, t);
                    break;
                case ShakePhase.Hold:
                    fadeValue = 1;
                    break;
                case ShakePhase.FadeOut:
                    isFadingOut = true;
                    fadeValue = ComputeFadeValue(timeSettings.fadeOutCurve, timeSettings.fadeOutDuration, isFadingOut, t - timeSettings.fadeInDuration - timeSettings.holdDuration);
                    break;
                case ShakePhase.None:
                    fadeValue = 0;
                    break;
            }

            return fadeValue;
        }

        private static TimeSettings RelativeTimeSettings(Shaker shaker, TimeSettings timeSettings)
        {
            TimeSettings relativeTimeSettings = new TimeSettings();
            if (shaker is MultiVectorShaker multiVectorShaker)
            {
                relativeTimeSettings.fadeInDuration = timeSettings.fadeInDuration * multiVectorShaker.lifetime;
                relativeTimeSettings.fadeInCurve = timeSettings.fadeInCurve;
                relativeTimeSettings.holdDuration = timeSettings.holdDuration * multiVectorShaker.lifetime;
                relativeTimeSettings.fadeOutCurve = timeSettings.fadeOutCurve;
                relativeTimeSettings.fadeOutDuration = timeSettings.fadeOutDuration * multiVectorShaker.lifetime;
            }
            else if (shaker is MultiFloatShaker multiFloatShaker)
            {
                relativeTimeSettings.fadeInDuration = timeSettings.fadeInDuration * multiFloatShaker.lifetime;
                relativeTimeSettings.fadeInCurve = timeSettings.fadeInCurve;
                relativeTimeSettings.holdDuration = timeSettings.holdDuration * multiFloatShaker.lifetime;
                relativeTimeSettings.fadeOutCurve = timeSettings.fadeOutCurve;
                relativeTimeSettings.fadeOutDuration = timeSettings.fadeOutDuration * multiFloatShaker.lifetime;
            }
            else
            {
                throw new Exception("Shaker is not a MultiShaker");
            }

            return relativeTimeSettings;
        }
    }

}
