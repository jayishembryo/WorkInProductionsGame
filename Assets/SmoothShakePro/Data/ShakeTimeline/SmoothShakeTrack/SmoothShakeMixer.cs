using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
    public class SmoothShakeMixer : ShakeMixerBase
    {
        protected override int GetMultiShakersLength() => 4;

        private SmoothShakeBehaviour[] behaviours;

        public override void OnGraphStart(Playable playable)
        {
            behaviours ??= GetBehaviours<SmoothShakeBehaviour>(playable);
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SmoothShake trackBinding = playerData as SmoothShake;

            if (!trackBinding)
            {
                Debug.LogWarning("No track binding on Smooth Shake track");
                return;
            }

            if (behaviours != null)
                ProcessShake(trackBinding, playable, behaviours);
            else
            {
                behaviours = GetBehaviours<SmoothShakeBehaviour>(playable);
                ProcessShake(trackBinding, playable, behaviours);
            }
        }
    }

}
