using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
#if UNITY_XR
    public class SmoothShakeHapticsXRMixer : ShakeMixerBase
    {
        protected override int GetMultiShakersLength() => 3;

        private SmoothShakeHapticsXRBehaviour[] behaviours;

        public override void OnGraphStart(Playable playable)
        {
            behaviours ??= GetBehaviours<SmoothShakeHapticsXRBehaviour>(playable);
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SmoothShakeHapticsXR trackBinding = playerData as SmoothShakeHapticsXR;

            if (!trackBinding)
            {
                Debug.LogWarning("No track binding on Smooth Shake Cinemachine track");
                return;
            }

            if (behaviours != null)
                ProcessShake(trackBinding, playable, behaviours);
            else
            {
                behaviours = GetBehaviours<SmoothShakeHapticsXRBehaviour>(playable);
                ProcessShake(trackBinding, playable, behaviours);
            }
        }
    }
#endif
}
