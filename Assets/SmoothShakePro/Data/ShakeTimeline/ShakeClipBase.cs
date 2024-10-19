using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SmoothShakePro
{
    public abstract class ShakeClipBase : PlayableAsset
    {
        public IEnumerable<Shaker>[] shakers;

        [HideInInspector] public PlayableGraph graph;

        public double CustomClipStart { get; set; }
        public double CustomClipEnd { get; set; }
        public double CustomEaseInDuration { get; set; }
        public double CustomEaseOutDuration { get; set; }

        public abstract IEnumerable<Shaker>[] GetMultiShakers();

        public abstract void ApplyPresetSettings();
    }

}
