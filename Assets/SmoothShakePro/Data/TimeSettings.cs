using UnityEngine;

namespace SmoothShakePro
{
    public enum TimescaleMode
    {
        Scaled,
        Unscaled,
        Custom,
    }

    [System.Serializable]
    public struct TimeSettings
    {
        [HideInInspector] public float fadeValue;

        [Tooltip("Play this shake on start")]
        public bool enableOnStart;
        [Tooltip("Use an infinite holdduration (until stopped)")]
        public bool constantShake;
        [Tooltip("Should the shake loop")]
        public bool loop;

        [Tooltip("How long the shake fade in should last")]
        public float fadeInDuration;
        [Tooltip("The curve to use for the shake fade in")]
        public AnimationCurve fadeInCurve;

        [Tooltip("How long the shake should hold at full strength")]
        public float holdDuration;

        [Tooltip("How long the shake fade out should last")]
        public float fadeOutDuration;
        [Tooltip("The curve to use for the shake fade out")]
        public AnimationCurve fadeOutCurve;

        [Tooltip("The timescale mode to use for the shake")]
        public TimescaleMode timescaleMode;
        [Tooltip("The custom timescale used if timescale mode is set to custom")]
        public float customTimescale;

        /// <summary>
        /// Get the total duration of the shake
        /// </summary>
        /// <returns></returns>
        public readonly float GetShakeDuration() => fadeInDuration + holdDuration + fadeOutDuration;
    }

}
