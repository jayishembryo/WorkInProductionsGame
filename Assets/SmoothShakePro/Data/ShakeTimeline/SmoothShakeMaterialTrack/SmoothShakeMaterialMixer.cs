using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
    public class SmoothShakeMaterialMixer : ShakeMixerBase
    {
        protected override int GetMultiShakersLength() => 3;

        private SmoothShakeMaterialBehaviour[] behaviours;

        public override void OnGraphStart(Playable playable)
        {
            behaviours ??= GetBehaviours<SmoothShakeMaterialBehaviour>(playable);
            base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SmoothShakeMaterial trackBinding = playerData as SmoothShakeMaterial;

            if (!trackBinding)
            {
                Debug.LogWarning("No track binding on Smooth Shake Material track");
                return;
            }

            if (behaviours != null)
                ProcessShake(trackBinding, playable, behaviours);
            else
            {
                behaviours = GetBehaviours<SmoothShakeMaterialBehaviour>(playable);
                ProcessShake(trackBinding, playable, behaviours);
            }
        }
    }
}
