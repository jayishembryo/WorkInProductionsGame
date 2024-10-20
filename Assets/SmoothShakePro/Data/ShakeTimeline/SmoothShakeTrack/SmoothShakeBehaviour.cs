using System.Collections.Generic;

namespace SmoothShakePro
{
    public class SmoothShakeBehaviour : ShakeBehaviourBase
    {
        public List<MultiVectorShaker> positionShake;
        public List<MultiVectorShaker> rotationShake;
        public List<MultiVectorShaker> scaleShake;
        public List<MultiFloatShaker> FOVShake;

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { positionShake, rotationShake, scaleShake, FOVShake };
        }
    }

}
