using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SmoothShakePro
{
    public enum ShakerProperty
    {
        Amplitude,
        Offset,
        Phase,
        Frequency,
    }

    public abstract class ShakeBase : MonoBehaviour
    {
        [HideInInspector] internal bool runtimePresetEditing = false;

        [Header("Time Settings")]
        [Tooltip("Settings for the shake timing")]
        public TimeSettings timeSettings;

        [Tooltip("A multiplier override for the amplitude of all shakers in this smooth shake. Works independently from presets. (Since it is a multiplier, you are still required to set the amplitude of shakers above 0 for this to have effect)")]
        public float amplitudeMultiplier = 1;
        protected float previousMultiplier = 1;

        private bool willStop = false;

        private bool _isShaking = false;

        internal bool initialized = false;

        internal void Initialize()
        {
#if UNITY_EDITOR
            timeSettings.timescaleMode = EditorUtility.defaultTimescaleMode;
            timeSettings.customTimescale = EditorUtility.defaultCustomTimescale;
#endif
        }

        public bool isShaking
        {
            get { return _isShaking; }
            protected set { _isShaking = value; }
        }

        private float ScaledDeltatime()
        {
            return timeSettings.timescaleMode switch
            {
                TimescaleMode.Scaled => Time.deltaTime,
                TimescaleMode.Custom => Time.unscaledDeltaTime * timeSettings.customTimescale,
                TimescaleMode.Unscaled => Time.unscaledDeltaTime,
                _ => Time.deltaTime,
            };
        }

        protected Shaker[] shakers;
        protected float[] fadeValues;
#if UNITY_2020
        protected readonly List<Coroutine> activeShakeRoutines = new List<Coroutine>();
#else
        protected readonly List<Coroutine> activeShakeRoutines = new();
#endif
        protected Coroutine clearAfterFinished;
        protected Vector3[] sum;

        public void Awake()
        {
            if (GetShakers() != null)
            {
                shakers ??= GetShakers();
                sum = new Vector3[shakers.Length];
                fadeValues = new float[shakers.Length];
            }
        }

        internal void Start()
        {
            if (timeSettings.enableOnStart) StartShake();
        }

        /// <summary>
        /// Start the shake
        /// </summary>
        public virtual void StartShake()
        {
            if (shakers != null)
            {
                isShaking = true;
                willStop = false;
                if (activeShakeRoutines.Count == 0)
                {
                    clearAfterFinished = StartCoroutine(ClearAfterFinished());
                    SaveDefaultValues();
                    for (int i = 0; i < shakers.Length; i++)
                    {
                        activeShakeRoutines.Add(StartCoroutine(ShakeRoutine(shakers[i], timeSettings, i)));
                    }
                }
                else
                {
                    ForceStop();
                    StartShake();
                }
            }
        }

        /// <summary>
        /// Start the shake with a preset
        /// </summary>
        /// <param name="preset"></param>
        public void StartShake(ShakeBasePreset preset)
        {
            ApplyPresetSettings(preset);
            StartShake();
        }


        /// <summary>
        /// Stop the shake. If a fade out duration is set, the shake will fade out
        /// </summary>
        public virtual void StopShake() => willStop = true;

        /// <summary>
        /// Force stop the shake. The shake will stop immediately
        /// </summary>
        public virtual void ForceStop()
        {
            isShaking = false;
            for (int i = 0; i < activeShakeRoutines.Count; i++)
            {
                if (activeShakeRoutines[i] != null)
                {
                    StopCoroutine(activeShakeRoutines[i]);
                    activeShakeRoutines[i] = null;
                }
            }
            if (clearAfterFinished != null)
            {
                StopCoroutine(clearAfterFinished);
                clearAfterFinished = null;
            }

            for (int i = 0; i < sum.Length; i++) sum[i] = Vector3.zero;
            for (int i = 0; i < fadeValues.Length; i++) fadeValues[i] = 0;

            activeShakeRoutines.Clear();
            ResetDefaultValues();
            willStop = false;
        }

        protected IEnumerator ClearAfterFinished()
        {
            if (timeSettings.constantShake)
            {
                while (true)
                {
                    if (willStop) break;
                    yield return null;
                }
                yield return new WaitForSeconds(timeSettings.fadeOutDuration);
                if(timeSettings.loop) StartShake();
                else ForceStop();
            }
            else
            {
                yield return new WaitForSeconds(timeSettings.GetShakeDuration());
                if (timeSettings.loop && !willStop) StartShake();
                else ForceStop();
            }
        }
        internal Vector3 RandomPhase(Shaker shaker)
        {
            switch (shaker)
            {
                case MultiVectorShaker multiVectorShaker:
                    if (multiVectorShaker.randomizePhase) return multiVectorShaker.phase + Utility.RandomVector3();
                    else return multiVectorShaker.phase;
                case MultiFloatShaker multiFloatShaker:
                    if (multiFloatShaker.randomizePhase) return new Vector3(multiFloatShaker.phase + UnityEngine.Random.Range(-1f, 1f), 0, 0);
                    else return new Vector3(multiFloatShaker.phase, 0, 0);
            }

            return Vector3.zero;
        }

        protected IEnumerator ShakeRoutine(Shaker shaker, TimeSettings timeSettings, int i)
        {
            Vector3 phase = RandomPhase(shaker);

            bool isFadingOut = false;
            if (timeSettings.fadeInDuration > 0) { yield return FadeRoutine(this.timeSettings.fadeInCurve, shaker, timeSettings, isFadingOut, i, phase); }

            if (timeSettings.holdDuration > 0 && !this.timeSettings.constantShake) { yield return HoldRoutine(timeSettings.holdDuration, shaker, timeSettings, i, phase); }
            if (this.timeSettings.constantShake) { yield return HoldRoutine(Mathf.Infinity, shaker, timeSettings, i, phase); }

            isFadingOut = true;
            if (timeSettings.fadeOutDuration > 0) { yield return FadeRoutine(this.timeSettings.fadeOutCurve, shaker, timeSettings, isFadingOut, i, phase); }
        }

        private IEnumerator FadeRoutine(AnimationCurve curve, Shaker shaker, TimeSettings timeSettings, bool isFadingOut, int i, Vector3 phase)
        {
            //Don't play the fade routine if the curve has no keys
            if (curve.length <= 1) yield break;

            if (isFadingOut && timeSettings.holdDuration == 0 && timeSettings.fadeInDuration == 0) fadeValues[i] = 1;

            Keyframe[] keys = curve.keys;
            float tEnd = isFadingOut ? timeSettings.fadeOutDuration : timeSettings.fadeInDuration;
            float t = isFadingOut ? 1 - fadeValues[i] : 0;

            while (t < tEnd)
            {
                if (!isFadingOut && willStop) yield break;
#if UNITY_2020
                float remappedTime = Utility.Remap(t, 0, tEnd, keys[0].time, keys[keys.Length - 1].time);
#else
                float remappedTime = Utility.Remap(t, 0, tEnd, keys[0].time, keys[^1].time);
#endif
                //timeSettings.fadeValue = Utility.Remap(curve.Evaluate(remappedTime), keys[0].value, keys[^1].value, isFadingOut ? 1 : 0, isFadingOut ? 0 : 1);
                fadeValues[i] = curve.Evaluate(remappedTime);
                Execute(shaker, timeSettings, i, phase);
                yield return null;
                t += ScaledDeltatime();
            }
        }

        private IEnumerator HoldRoutine(float duration, Shaker shaker, TimeSettings timeSettings, int i, Vector3 phase)
        {
            if (fadeValues[i] == 0) fadeValues[i] = 1;

            float t = 0;

            // Check if the duration is infinite
            if (float.IsInfinity(duration))
            {
                while (true) // Infinite loop
                {
                    if (willStop) yield break;

                    Execute(shaker, timeSettings, i, phase);
                    yield return null;
                }
            }
            else
            {
                while (t < duration)
                {
                    if (willStop) yield break;

                    Execute(shaker, timeSettings, i, phase);
                    yield return null;
                    t += ScaledDeltatime();
                }
            }
        }

        protected virtual void Execute(Shaker shaker, TimeSettings timeSettings, int i, Vector3 phase)
        {
            sum[i] = shaker.Evaluate(Time.time, amplitudeMultiplier, phase) * fadeValues[i];
        }

        protected virtual void ApplySum()
        {
            for (int i = 0; i < sum.Length; i++)
            {
                if (!Utility.IsValid(sum[i]))
                {
                    Debug.LogWarning($"invalid sum {i} is {sum[i]}");
                    sum[i] = Vector3.zero;
                }
            }

            Apply(sum);
        }

        private void Update() { if (activeShakeRoutines.Count > 0) ApplySum(); }

        internal abstract void Apply(Vector3[] value);
        protected abstract Shaker[] GetShakers();
        internal abstract void SaveDefaultValues();
        internal abstract void ResetDefaultValues();
        internal abstract void ApplyPresetSettings(ShakeBasePreset preset);

        /// <summary>
        /// Change the value of a shaker property
        /// </summary>
        /// <param name="shakerIndex"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <param name="overwrite"></param>
        public virtual void SetShakerProperty(int shakerIndex, float value, ShakerProperty property, bool overwrite = true)
        {
            SetShakerProperty(shakerIndex, new Vector3(value, value, value), property, overwrite);
        }

        /// <summary>
        /// Change the value of a shaker property
        /// </summary>
        /// <param name="shakerIndex"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <param name="overwrite"></param>
        public virtual void SetShakerProperty(int shakerIndex, Vector3 value, ShakerProperty property, bool overwrite = true)
        {
            switch (property)
            {
                case ShakerProperty.Offset:
                    if (shakers[shakerIndex] is VectorShaker vs) vs.offset = overwrite ? value : vs.offset + value;
                    else if (shakers[shakerIndex] is FloatShaker fs) fs.offset = overwrite ? value.x : fs.offset + value.x;
                    break;
                case ShakerProperty.Phase:
                    if (shakers[shakerIndex] is VectorShaker vs2) vs2.phase = overwrite ? value : vs2.phase + value;
                    else if (shakers[shakerIndex] is FloatShaker fs2) fs2.phase = overwrite ? value.x : fs2.phase + value.x;
                    break;
                case ShakerProperty.Amplitude:
                    if (shakers[shakerIndex] is VectorShaker vs3) vs3.amplitude = overwrite ? value : vs3.amplitude + value;
                    else if (shakers[shakerIndex] is FloatShaker fs3) fs3.amplitude = overwrite ? value.x : fs3.amplitude + value.x;
                    break;
                case ShakerProperty.Frequency:
                    if (shakers[shakerIndex] is VectorShaker vs4) vs4.frequency = overwrite ? value : vs4.frequency + value;
                    else if (shakers[shakerIndex] is FloatShaker fs4) fs4.frequency = overwrite ? value.x : fs4.frequency + value.x;
                    break;
            }
        }

        private void OnValidate()
        {
            if (timeSettings.customTimescale <= 0) timeSettings.customTimescale = 1;
        }
    }

}
