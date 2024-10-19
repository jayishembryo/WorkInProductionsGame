using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
    public class SmoothShakeHapticsGamepadClip : ShakeClipBase, ITimelineClipAsset
    {

        public ExposedReference<SmoothShakeHapticsGamepadPreset> preset;
        [HideInInspector] public SmoothShakeHapticsGamepadPreset _preset;

        [Header("Shake Settings")]
        [Tooltip("Settings for low frequency motor")]
        public List<MultiFloatShaker> lowFrequencyMotorShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for high frequency motor")]
        public List<MultiFloatShaker> highFrequencyMotorShake = new List<MultiFloatShaker>();

        public override void ApplyPresetSettings()
        {
            if (graph.IsValid())
            {
                if (preset.Resolve(graph.GetResolver()) != null)
                {
                    _preset = preset.Resolve(graph.GetResolver());
                    lowFrequencyMotorShake.Clear();
                    lowFrequencyMotorShake.AddRange(_preset.lowFrequencyMotorShake);
                    highFrequencyMotorShake.Clear();
                    highFrequencyMotorShake.AddRange(_preset.highFrequencyMotorShake);
                }
                else
                {
                    _preset = null;
                }
            }
        }

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { lowFrequencyMotorShake, highFrequencyMotorShake };
        }

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            this.graph = graph;

            var playable = ScriptPlayable<SmoothShakeHapticsGamepadBehaviour>.Create(graph);

            var smoothShakeBehaviour = playable.GetBehaviour();

            smoothShakeBehaviour.CustomClipStart = CustomClipStart;
            smoothShakeBehaviour.CustomClipEnd = CustomClipEnd;
            smoothShakeBehaviour.CustomEaseInDuration = CustomEaseInDuration;
            smoothShakeBehaviour.CustomEaseOutDuration = CustomEaseOutDuration;

            ApplyPresetSettings();

            smoothShakeBehaviour.lowFrequencyMotorShake = lowFrequencyMotorShake;
            smoothShakeBehaviour.highFrequencyMotorShake = highFrequencyMotorShake;

            shakers ??= GetMultiShakers();
            smoothShakeBehaviour.shakers = shakers;

            return playable;
        }
    }
}


