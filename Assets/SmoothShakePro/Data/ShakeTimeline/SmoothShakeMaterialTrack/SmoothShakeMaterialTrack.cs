using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
    [TrackColor(0.94f, 0.15f, 0.3f)]
    [TrackClipType(typeof(SmoothShakeMaterialClip))]
    [TrackBindingType(typeof(SmoothShakeMaterial))]
    public class SmoothShakeMaterialTrack : ShakeTrackBase
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var director = go.GetComponent<PlayableDirector>();
            trackBinding = director.GetGenericBinding(this) as SmoothShakeMaterial;

            foreach (var clip in GetClips())
            {
                var smoothShakeClip = clip.asset as SmoothShakeMaterialClip;
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

            return ScriptPlayable<SmoothShakeMaterialMixer>.Create(graph, inputCount);
        }
    }
}
