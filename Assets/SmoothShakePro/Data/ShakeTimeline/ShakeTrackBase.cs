using UnityEngine;
using UnityEngine.Timeline;

namespace SmoothShakePro
{
    public abstract class ShakeTrackBase : TrackAsset
    {
        [HideInInspector] public ShakeBase trackBinding;
    }

}
