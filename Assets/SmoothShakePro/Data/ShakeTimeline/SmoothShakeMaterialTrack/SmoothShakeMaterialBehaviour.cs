using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    public class SmoothShakeMaterialBehaviour : ShakeBehaviourBase
    {
        [Header("Shake Settings")]
        public List<MultiFloatShaker> floatShake = new List<MultiFloatShaker>();
        public List<MultiVectorShaker> vectorShake = new List<MultiVectorShaker>();

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { floatShake, vectorShake };
        }
    }
}
