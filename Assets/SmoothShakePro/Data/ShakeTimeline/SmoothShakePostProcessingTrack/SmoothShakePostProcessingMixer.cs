using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
#if UNITY_TIMELINE && UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
    public class SmoothShakePostProcessingMixer : ShakeMixerBase
    {
        protected override int GetMultiShakersLength()
        {
            return 50;
        }

        private SmoothShakePostProcessingBehaviour[] behaviours;

        public override void OnGraphStart(Playable playable)
        {
            behaviours ??= GetBehaviours<SmoothShakePostProcessingBehaviour>(playable);
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SmoothShakePostProcessing trackBinding = playerData as SmoothShakePostProcessing;

            if (!trackBinding)
            {
                Debug.LogWarning("No track binding on Smooth Shake Post Processing track");
                return;
            }

            if (behaviours != null)
            {
                ProcessShake(trackBinding, playable, behaviours);
            }
            else
            {
                behaviours = GetBehaviours<SmoothShakePostProcessingBehaviour>(playable);
                ProcessShake(trackBinding, playable, behaviours);
            }
        }
    }
#endif
}