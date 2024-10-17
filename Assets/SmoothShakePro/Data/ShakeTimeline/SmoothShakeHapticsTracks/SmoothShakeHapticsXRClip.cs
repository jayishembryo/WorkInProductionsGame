using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
#if UNITY_XR
    public class SmoothShakeHapticsXRClip : ShakeClipBase, ITimelineClipAsset
    {

        public ExposedReference<SmoothShakeHapticsXRPreset> preset;
        [HideInInspector] public SmoothShakeHapticsXRPreset _preset;

        [Header("Shake Settings")]
        [Tooltip("Settings for left controller")]
        public List<MultiFloatShaker> leftControllerShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for riight controller")]
        public List<MultiFloatShaker> rightControllerShake = new List<MultiFloatShaker>();

        public override void ApplyPresetSettings()
        {
            if (graph.IsValid())
            {
                if (preset.Resolve(graph.GetResolver()) != null)
                {
                    _preset = preset.Resolve(graph.GetResolver());
                    leftControllerShake.Clear();
                    leftControllerShake.AddRange(_preset.leftControllerShake);
                    rightControllerShake.Clear();
                    rightControllerShake.AddRange(_preset.rightControllerShake);
                }
                else
                {
                    _preset = null;
                }
            }
        }

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { leftControllerShake, rightControllerShake };
        }

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            this.graph = graph;

            var playable = ScriptPlayable<SmoothShakeHapticsXRBehaviour>.Create(graph);

            var smoothShakeBehaviour = playable.GetBehaviour();

            smoothShakeBehaviour.CustomClipStart = CustomClipStart;
            smoothShakeBehaviour.CustomClipEnd = CustomClipEnd;
            smoothShakeBehaviour.CustomEaseInDuration = CustomEaseInDuration;
            smoothShakeBehaviour.CustomEaseOutDuration = CustomEaseOutDuration;

            ApplyPresetSettings();

            smoothShakeBehaviour.leftControllerShake = leftControllerShake;
            smoothShakeBehaviour.rightControllerShake = rightControllerShake;

            shakers ??= GetMultiShakers();
            smoothShakeBehaviour.shakers = shakers;

            return playable;
        }
    }
#endif
}


