using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    public class SmoothShakeLightBehaviour : ShakeBehaviourBase
    {
        public List<MultiFloatShaker> intensityShake;
        public List<MultiFloatShaker> rangeShake;

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { intensityShake, rangeShake };
        }
    }
}
