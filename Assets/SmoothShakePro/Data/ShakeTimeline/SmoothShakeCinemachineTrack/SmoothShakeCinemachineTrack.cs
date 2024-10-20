using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
#if CINEMACHINE
    [TrackColor(0.94f, 0.15f, 0.3f)]
    [TrackClipType(typeof(SmoothShakeCinemachineClip))]
    [TrackBindingType(typeof(SmoothShakeCinemachine))]
    public class SmoothShakeCinemachineTrack : ShakeTrackBase
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var director = go.GetComponent<PlayableDirector>();
            trackBinding = director.GetGenericBinding(this) as SmoothShakeCinemachine;

            foreach (var clip in GetClips())
            {
                var smoothShakeClip = clip.asset as SmoothShakeCinemachineClip;
                if (smoothShakeClip != null)
                {
                    smoothShakeClip.CustomClipStart = clip.start;
                    smoothShakeClip.CustomClipEnd = clip.end;
                    smoothShakeClip.CustomEaseInDuration = clip.easeInDuration;
                    smoothShakeClip.CustomEaseOutDuration = clip.easeOutDuration;
                }
                else
                {
                    Debug.LogError("SmoothShakeClip is null");
                }
            }

            return ScriptPlayable<SmoothShakeCinemachineMixer>.Create(graph, inputCount);
        }
    }
#endif
}
