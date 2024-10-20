using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
    public class SmoothShakeMaterialClip : ShakeClipBase, ITimelineClipAsset
    {
        public ExposedReference<SmoothShakeMaterialPreset> preset;
        [HideInInspector] public SmoothShakeMaterialPreset _preset;

        [Header("Shake Settings")]
        public List<MultiFloatShaker> floatShake = new List<MultiFloatShaker>();
        public List<MultiVectorShaker> vectorShake = new List<MultiVectorShaker>();

        public override void ApplyPresetSettings()
        {
            if (graph.IsValid())
            {
                if (preset.Resolve(graph.GetResolver()) != null)
                {
                    _preset = preset.Resolve(graph.GetResolver());
                    floatShake.Clear();
                    floatShake.AddRange(_preset.propertyToShake.floatShake);
                    vectorShake.Clear();
                    vectorShake.AddRange(_preset.propertyToShake.vectorShake);
                }
                else
                {
                    _preset = null;
                }
            }
        }

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { floatShake, vectorShake };
        }

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            this.graph = graph;

            var playable = ScriptPlayable<SmoothShakeMaterialBehaviour>.Create(graph);

            var smoothShakeBehaviour = playable.GetBehaviour();

            smoothShakeBehaviour.CustomClipStart = CustomClipStart;
            smoothShakeBehaviour.CustomClipEnd = CustomClipEnd;
            smoothShakeBehaviour.CustomEaseInDuration = CustomEaseInDuration;
            smoothShakeBehaviour.CustomEaseOutDuration = CustomEaseOutDuration;

            ApplyPresetSettings();

            smoothShakeBehaviour.floatShake = floatShake;
            smoothShakeBehaviour.vectorShake = vectorShake;

            shakers ??= GetMultiShakers();
            smoothShakeBehaviour.shakers = shakers;

            return playable;
        }
    }
}
