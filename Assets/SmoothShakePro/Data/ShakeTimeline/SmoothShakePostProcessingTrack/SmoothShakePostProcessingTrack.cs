using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
#if UNITY_TIMELINE && UNITY_2022 && SMOOTHSHAKEPRO_EXPERIMENTAL
    [TrackColor(0.94f, 0.15f, 0.3f)]
    [TrackClipType(typeof(SmoothShakePostProcessingClip))]
    [TrackBindingType(typeof(SmoothShakePostProcessing))]
    public class SmoothShakePostProcessingTrack : ShakeTrackBase
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var director = go.GetComponent<PlayableDirector>();
            trackBinding = director.GetGenericBinding(this) as SmoothShakePostProcessing;

            foreach (var clip in GetClips())
            {
                var smoothShakeClip = clip.asset as SmoothShakePostProcessingClip;
                if (smoothShakeClip != null)
                {
                    smoothShakeClip.CustomClipStart = clip.start;
                    smoothShakeClip.CustomClipEnd = clip.end;
                    smoothShakeClip.CustomEaseInDuration = clip.easeInDuration;
                    smoothShakeClip.CustomEaseOutDuration = clip.easeOutDuration;

                    if(smoothShakeClip is SmoothShakePostProcessingClip ssppc && trackBinding is SmoothShakePostProcessing sspp)
                    {
                        ssppc.overrides = sspp.overrides;
#if SMOOTHPOSTPROCESSING
                        ssppc.smoothShakeProOverrides = sspp.smoothPostProcessingOverrides;
#endif
                    }
                }
                else
                {
                    Debug.LogError("SmoothShakeClip is null");
                }
            }

            return ScriptPlayable<SmoothShakePostProcessingMixer>.Create(graph, inputCount);
        }
    }
#endif
}
