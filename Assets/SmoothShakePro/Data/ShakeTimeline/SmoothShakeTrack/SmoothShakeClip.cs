using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
    public class SmoothShakeClip : ShakeClipBase, ITimelineClipAsset
    {
        public ExposedReference<SmoothShakePreset> preset;
        [HideInInspector] public SmoothShakePreset _preset;

#if UNITY_2020
        [Header("Position Shake Settings")]
        public List<MultiVectorShaker> positionShake = new List<MultiVectorShaker>();
        [Header("Rotation Shake Settings")]
        public List<MultiVectorShaker> rotationShake = new List<MultiVectorShaker>();
        [Header("Scale Shake Settings")]
        public List<MultiVectorShaker> scaleShake = new List<MultiVectorShaker>();
        [Header("FOV Shake Settings")]
        public List<MultiFloatShaker> FOVShake = new List<MultiFloatShaker>();
#else
        [Header("Position Shake Settings")]
        public List<MultiVectorShaker> positionShake = new();
        [Header("Rotation Shake Settings")]
        public List<MultiVectorShaker> rotationShake = new();
        [Header("Scale Shake Settings")]
        public List<MultiVectorShaker> scaleShake = new();
        [Header("FOV Shake Settings")]
        public List<MultiFloatShaker> FOVShake = new();
#endif

        public override void ApplyPresetSettings()
        {
            if (graph.IsValid())
            {
                if (preset.Resolve(graph.GetResolver()) != null)
                {
                    _preset = preset.Resolve(graph.GetResolver());
                    positionShake.Clear();
                    positionShake.AddRange(_preset.positionShake);
                    rotationShake.Clear();
                    rotationShake.AddRange(_preset.rotationShake);
                    scaleShake.Clear();
                    scaleShake.AddRange(_preset.scaleShake);
                    FOVShake.Clear();
                    FOVShake.AddRange(_preset.FOVShake);
                }
                else
                {
                    _preset = null;
                }
            }
        }

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { positionShake, rotationShake, scaleShake, FOVShake };
        }

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            this.graph = graph; 

            var playable = ScriptPlayable<SmoothShakeBehaviour>.Create(graph);

            var smoothShakeBehaviour = playable.GetBehaviour();

            smoothShakeBehaviour.CustomClipStart = CustomClipStart;
            smoothShakeBehaviour.CustomClipEnd = CustomClipEnd;
            smoothShakeBehaviour.CustomEaseInDuration = CustomEaseInDuration;
            smoothShakeBehaviour.CustomEaseOutDuration = CustomEaseOutDuration;

            ApplyPresetSettings();

            smoothShakeBehaviour.positionShake = positionShake;
            smoothShakeBehaviour.rotationShake = rotationShake;
            smoothShakeBehaviour.scaleShake = scaleShake;
            smoothShakeBehaviour.FOVShake = FOVShake;

            shakers ??= GetMultiShakers();
            smoothShakeBehaviour.shakers = shakers;

            return playable;
        }


    }

}
