using System.Collections.Generic;
using UnityEngine.Playables;

namespace SmoothShakePro
{
    public abstract class ShakeBehaviourBase : PlayableBehaviour
    {
        public IEnumerable<Shaker>[] shakers;

        public double CustomClipStart { get; set; }
        public double CustomClipEnd { get; set; }
        public double CustomEaseInDuration { get; set; }
        public double CustomEaseOutDuration { get; set; }

        public abstract IEnumerable<Shaker>[] GetMultiShakers();
    }
}
