using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
    public static class Utility
    {
        public static Vector3 RandomVector3()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        }

        public static float Remap(float valueInput, float oldRangeMin, float oldRangeMax, float newRangeMin, float newRangeMax)
        {
            return newRangeMin + (valueInput - oldRangeMin) * (newRangeMax - newRangeMin) / (oldRangeMax - oldRangeMin);
        }

        public static bool IsValid(Vector3 value)
        {
            return !float.IsNaN(value.x) && !float.IsNaN(value.y) && !float.IsNaN(value.z);
        }

        public static void ExecuteMultiShaker(float t, Shaker shaker, float fadeValue, Vector3[] sum, int i, float amplitudeMultiplier, Vector3 phase)
        {
            Vector3 evaluatedValue = shaker.Evaluate(t, amplitudeMultiplier, phase) * fadeValue;
            Func<Vector3, Vector3, Vector3> blendingModeFunc;

            if (shaker is MultiVectorShaker multiVectorShaker)
                blendingModeFunc = BlendingModes.GetBlendingMode(multiVectorShaker.blendingMode);
            else if (shaker is MultiFloatShaker multiFloatShaker)
                blendingModeFunc = BlendingModes.GetBlendingMode(multiFloatShaker.blendingMode);
            else
                throw new Exception("Shaker is not a MultiShaker");

            sum[i] = blendingModeFunc(sum[i], evaluatedValue);
        }

        public static void SetShakerLifetime(Shaker shaker, TimeSettings timeSettings)
        {
            if (shaker is MultiVectorShaker multiVectorShaker)
            {
                multiVectorShaker.shakerTimeSettings.fadeInDuration = timeSettings.fadeInDuration * multiVectorShaker.lifetime;
                multiVectorShaker.shakerTimeSettings.holdDuration = timeSettings.holdDuration * multiVectorShaker.lifetime;
                multiVectorShaker.shakerTimeSettings.fadeOutDuration = timeSettings.fadeOutDuration * multiVectorShaker.lifetime;
            }
            else if (shaker is MultiFloatShaker multiFloatShaker)
            {
                multiFloatShaker.shakerTimeSettings.fadeInDuration = timeSettings.fadeInDuration * multiFloatShaker.lifetime;
                multiFloatShaker.shakerTimeSettings.holdDuration = timeSettings.holdDuration * multiFloatShaker.lifetime;
                multiFloatShaker.shakerTimeSettings.fadeOutDuration = timeSettings.fadeOutDuration * multiFloatShaker.lifetime;
            }
            else
            {
                Debug.LogError("Shaker is not a MultiShaker");
            }
        }

        public static Vector3 GetPhase(Shaker shaker)
        {
            if (shaker is MultiVectorShaker multiVectorShaker)
                return multiVectorShaker.phase;
            else if (shaker is MultiFloatShaker multiFloatShaker)
                return new Vector3(multiFloatShaker.phase, 0, 0);
            return Vector3.zero;
        }

        public static UnityEngine.Object GetBinding(TrackAsset track)
        {
#if UNITY_2023
            PlayableDirector[] directors = UnityEngine.Object.FindObjectsByType<PlayableDirector>(FindObjectsSortMode.None);
#else
            PlayableDirector[] directors = UnityEngine.Object.FindObjectsOfType<PlayableDirector>();
#endif
            foreach (var director in directors)
            {
                if (director.playableAsset == track.timelineAsset)
                {
                    return director.GetGenericBinding(track);
                }
            }
            return null;
        }
    }
}
