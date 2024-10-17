using System.Collections.Generic;

namespace SmoothShakePro
{
#if CINEMACHINE
    public class SmoothShakeCinemachineBehaviour : ShakeBehaviourBase
    {
        public List<MultiVectorShaker> positionShake;
        public List<MultiVectorShaker> rotationShake;
        public List<MultiFloatShaker> FOVShake;

        public override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { positionShake, rotationShake, FOVShake };
        }
    }
#endif
}
