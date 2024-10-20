using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
    public class SmoothShakeHapticsGamepadMixer : ShakeMixerBase
    {
        protected override int GetMultiShakersLength() => 3;

        private SmoothShakeHapticsGamepadBehaviour[] behaviours;

        public override void OnGraphStart(Playable playable)
        {
            behaviours ??= GetBehaviours<SmoothShakeHapticsGamepadBehaviour>(playable);
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SmoothShakeHapticsGamepad trackBinding = playerData as SmoothShakeHapticsGamepad;

            if (!trackBinding)
            {
                Debug.LogWarning("No track binding on Smooth Shake Cinemachine track");
                return;
            }

            if (behaviours != null)
                ProcessShake(trackBinding, playable, behaviours);
            else
            {
                behaviours = GetBehaviours<SmoothShakeHapticsGamepadBehaviour>(playable);
                ProcessShake(trackBinding, playable, behaviours);
            }
        }
    }
}
