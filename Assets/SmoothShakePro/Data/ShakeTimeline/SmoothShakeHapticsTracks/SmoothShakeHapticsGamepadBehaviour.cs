using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    public class SmoothShakeHapticsGamepadBehaviour : ShakeBehaviourBase
    {
        public List<MultiFloatShaker> lowFrequencyMotorShake;
        public List<MultiFloatShaker> highFrequencyMotorShake;

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { lowFrequencyMotorShake, highFrequencyMotorShake };
        }
    }
}
