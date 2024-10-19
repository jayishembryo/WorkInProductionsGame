using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
    public class SmoothShakeLightMixer : ShakeMixerBase
    {
        protected override int GetMultiShakersLength() => 3;

        private SmoothShakeLightBehaviour[] behaviours;

        public override void OnGraphStart(Playable playable)
        {
            behaviours ??= GetBehaviours<SmoothShakeLightBehaviour>(playable);
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SmoothShakeLight trackBinding = playerData as SmoothShakeLight;

            if (!trackBinding)
            {
                Debug.LogWarning("No track binding on Smooth Shake Light track");
                return;
            }

            if (behaviours != null)
                ProcessShake(trackBinding, playable, behaviours);
            else
            {
                behaviours = GetBehaviours<SmoothShakeLightBehaviour>(playable);
                ProcessShake(trackBinding, playable, behaviours);
            }
        }
    }
}
