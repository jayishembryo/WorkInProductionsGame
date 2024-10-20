using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
#if UNITY_XR
    public class SmoothShakeHapticsXRBehaviour : ShakeBehaviourBase
    {
        public List<MultiFloatShaker> leftControllerShake;
        public List<MultiFloatShaker> rightControllerShake;

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { leftControllerShake, rightControllerShake };
        }
    }
#endif
}
