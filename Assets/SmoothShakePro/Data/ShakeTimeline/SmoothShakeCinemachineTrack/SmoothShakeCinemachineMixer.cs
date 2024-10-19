using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
#if CINEMACHINE
    public class SmoothShakeCinemachineMixer : ShakeMixerBase
    {
        protected override int GetMultiShakersLength() => 3;

        private SmoothShakeCinemachineBehaviour[] behaviours;

        public override void OnGraphStart(Playable playable)
        {
            behaviours ??= GetBehaviours<SmoothShakeCinemachineBehaviour>(playable);
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SmoothShakeCinemachine trackBinding = playerData as SmoothShakeCinemachine;

            if (!trackBinding)
            {
                Debug.LogWarning("No track binding on Smooth Shake Cinemachine track");
                return;
            }

            if (behaviours != null)
                ProcessShake(trackBinding, playable, behaviours);
            else
            {
                behaviours = GetBehaviours<SmoothShakeCinemachineBehaviour>(playable);
                ProcessShake(trackBinding, playable, behaviours);
            }
        }
    }
#endif
}
