using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    public abstract class MultiShakeBase : ShakeBase
    {
#if UNITY_EDITOR
        [HideInInspector] internal AxisToPreview axisToPreview;
#endif

        private new IEnumerable<Shaker>[] shakers;

        public new void Awake()
        {
            if(GetMultiShakers() != null)
            {
                shakers ??= GetMultiShakers();
                sum = new Vector3[shakers.Length];
                fadeValues = new float[shakers.Length];
            }
        }

        [ContextMenu("Start Shake")]
        public override void StartShake()
        {
            if(shakers != null)
            {
                isShaking = true;
                if (activeShakeRoutines.Count == 0)
                {
                    clearAfterFinished = StartCoroutine(ClearAfterFinished());
                    SaveDefaultValues();

                    for (int i = 0; i < shakers.Length; i++)
                    {
                        foreach (var shaker in shakers[i])
                        {
                            Utility.SetShakerLifetime(shaker, timeSettings);

                            switch (shaker)
                            {
                                case MultiVectorShaker multiVectorShaker:
                                    activeShakeRoutines.Add(StartCoroutine(ShakeRoutine(multiVectorShaker, multiVectorShaker.shakerTimeSettings, i)));
                                    break;
                                case MultiFloatShaker multiFloatShaker:
                                    activeShakeRoutines.Add(StartCoroutine(ShakeRoutine(multiFloatShaker, multiFloatShaker.shakerTimeSettings, i)));
                                    break;
                                default:
                                    Debug.LogError("Shaker is not a MultiShaker");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    ForceStop();
                    StartShake();
                }
            }
        }

        protected override void Execute(Shaker shaker, TimeSettings timeSettings, int i, Vector3 phase)
        {
            Utility.ExecuteMultiShaker(Time.time, shaker, fadeValues[i], sum, i, amplitudeMultiplier, phase);
        }

        protected override void ApplySum()
        {
            base.ApplySum();
            for (int i = 0; i < sum.Length; i++) sum[i] = Vector3.zero;
        }

        internal abstract IEnumerable<Shaker>[] GetMultiShakers();

        public override void SetShakerProperty(int shakerIndex, float value, ShakerProperty property, bool overwrite = true)
        {
            SetShakerProperty(shakerIndex, new Vector3(value, value, value), property, overwrite);
        }

        public override void SetShakerProperty(int shakerIndex, Vector3 value, ShakerProperty property, bool overwrite = true)
        {
            if (!Application.isPlaying)
            {
                foreach (IEnumerable<Shaker> shakers in GetMultiShakers())
                {
                    foreach (var shaker in shakers)
                    {
                        switch (property)
                        {
                            case ShakerProperty.Offset:
                                if (shaker is FloatShaker fs) fs.offset = overwrite ? value.x : fs.offset + value.x;
                                if (shaker is VectorShaker vs) vs.offset = overwrite ? value : vs.offset + value;
                                break;
                            case ShakerProperty.Phase:
                                if (shaker is FloatShaker fs2) fs2.phase = overwrite ? value.x : fs2.phase + value.x;
                                if (shaker is VectorShaker vs2) vs2.phase = overwrite ? value : vs2.phase + value;
                                break;
                            case ShakerProperty.Amplitude:
                                if (shaker is FloatShaker fs3) fs3.amplitude = overwrite ? value.x : fs3.amplitude + value.x;
                                if (shaker is VectorShaker vs3) vs3.amplitude = overwrite ? value : vs3.amplitude + value;
                                break;
                            case ShakerProperty.Frequency:
                                if (shaker is FloatShaker fs4) fs4.frequency = overwrite ? value.x : fs4.frequency + value.x;
                                if (shaker is VectorShaker vs4) vs4.frequency = overwrite ? value : vs4.frequency + value;
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (var shaker in shakers[shakerIndex])
                {
                    switch (property)
                    {
                        case ShakerProperty.Offset:
                            if (shaker is FloatShaker fs) fs.offset = overwrite ? value.x : fs.offset + value.x;
                            if (shaker is VectorShaker vs) vs.offset = overwrite ? value : vs.offset + value;
                            break;
                        case ShakerProperty.Phase:
                            if (shaker is FloatShaker fs2) fs2.phase = overwrite ? value.x : fs2.phase + value.x;
                            if (shaker is VectorShaker vs2) vs2.phase = overwrite ? value : vs2.phase + value;
                            break;
                        case ShakerProperty.Amplitude:
                            if (shaker is FloatShaker fs3) fs3.amplitude = overwrite ? value.x : fs3.amplitude + value.x;
                            if (shaker is VectorShaker vs3) vs3.amplitude = overwrite ? value : vs3.amplitude + value;
                            break;
                        case ShakerProperty.Frequency:
                            if (shaker is FloatShaker fs4) fs4.frequency = overwrite ? value.x : fs4.frequency + value.x;
                            if (shaker is VectorShaker vs4) vs4.frequency = overwrite ? value : vs4.frequency + value;
                            break;
                    }

                }
            }
        }
    }
}
