using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
    public class SmoothShakeLightClip : ShakeClipBase, ITimelineClipAsset
    {
        public ExposedReference<SmoothShakeLightPreset> preset;
        [HideInInspector] public SmoothShakeLightPreset _preset;

        [Tooltip("Settings for Intensity Shake")]
        public List<MultiFloatShaker> intensityShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for Range Shake")]
        public List<MultiFloatShaker> rangeShake = new List<MultiFloatShaker>();
        public override void ApplyPresetSettings()
        {
            if (graph.IsValid())
            {
                if (preset.Resolve(graph.GetResolver()) != null)
                {
                    _preset = preset.Resolve(graph.GetResolver());
                    intensityShake.Clear();
                    intensityShake.AddRange(_preset.intensityShake);
                    rangeShake.Clear();
                    rangeShake.AddRange(_preset.rangeShake);
                }
                else
                {
                    _preset = null;
                }
            }
        }

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { intensityShake, rangeShake };
        }

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            this.graph = graph;

            var playable = ScriptPlayable<SmoothShakeLightBehaviour>.Create(graph);

            var smoothShakeBehaviour = playable.GetBehaviour();

            smoothShakeBehaviour.CustomClipStart = CustomClipStart;
            smoothShakeBehaviour.CustomClipEnd = CustomClipEnd;
            smoothShakeBehaviour.CustomEaseInDuration = CustomEaseInDuration;
            smoothShakeBehaviour.CustomEaseOutDuration = CustomEaseOutDuration;

            ApplyPresetSettings();

            smoothShakeBehaviour.intensityShake = intensityShake;
            smoothShakeBehaviour.rangeShake = rangeShake;

            shakers ??= GetMultiShakers();
            smoothShakeBehaviour.shakers = shakers;

            return playable;
        }
    }
}
