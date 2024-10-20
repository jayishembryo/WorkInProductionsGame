using System.Collections.Generic;
using UnityEngine;
#if CINEMACHINE 
#if !UNITY_6
using Cinemachine;
#else 
using Unity.Cinemachine;
#endif

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Cinemachine")]
    [RequireComponent(typeof(CinemachineCameraOffset))]
    [RequireComponent(typeof(CinemachineRecomposer))]
    public class SmoothShakeCinemachine : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakeCinemachinePreset preset;

#if UNITY_2020
        [Header("Shake Settings")]
        [Tooltip("Settings for Position Shake")]
        public List<MultiVectorShaker> positionShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for Rotation Shake")]
        public List<MultiVectorShaker> rotationShake = new List<MultiVectorShaker>();
        [Tooltip("Settings for FOV Shake")]
        public List<MultiFloatShaker> FOVShake = new List<MultiFloatShaker>();
#else
        [Header("Shake Settings")]
        [Tooltip("Settings for Position Shake")]
        public List<MultiVectorShaker> positionShake = new();
        [Tooltip("Settings for Rotation Shake")]
        public List<MultiVectorShaker> rotationShake = new();
        [Tooltip("Settings for FOV Shake")]
        public List<MultiFloatShaker> FOVShake = new();
#endif

        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Position, Rotation, FOV }

        [HideInInspector] internal Vector3 startPosition;
        [HideInInspector] internal Vector3 startRotation;
        [HideInInspector] internal float startFOV;

#if !UNITY_6
        [HideInInspector] internal CinemachineVirtualCamera cvcCam;
        [HideInInspector] internal CinemachineFreeLook cflCam;
#else
        [HideInInspector] internal CinemachineCamera cCam;
#endif
        [HideInInspector] internal CinemachineCameraOffset cinemachinePosOffset;
        [HideInInspector] internal CinemachineRecomposer cinemachineRotOffset;

        internal new void Awake()
        {
            base.Awake();
            GetReferences();

            if (preset) ApplyPresetSettings(preset);
        }

        private void GetReferences()
        {
#if !UNITY_6
            if (GetComponent<CinemachineVirtualCamera>() != null && cvcCam == null)
                cvcCam = GetComponent<CinemachineVirtualCamera>();
            if (GetComponent<CinemachineFreeLook>() != null && cflCam == null)
                cflCam = GetComponent<CinemachineFreeLook>();
#else
            if (GetComponent<CinemachineCamera>() != null && cCam == null)
                cCam = GetComponent<CinemachineCamera>();
#endif

            if (cinemachinePosOffset == null) cinemachinePosOffset = GetComponent<CinemachineCameraOffset>();
            if (cinemachineRotOffset == null) cinemachineRotOffset = GetComponent<CinemachineRecomposer>();
        }

        internal sealed override void Apply(Vector3[] value)
        {
            if (cinemachinePosOffset != null && cinemachineRotOffset != null)
            {
                //Debug.Log(sum[0]);
#if !UNITY_6
                cinemachinePosOffset.m_Offset = startPosition + (value[0]);
                cinemachineRotOffset.m_Tilt = startRotation.x + (value[1].x);
                cinemachineRotOffset.m_Pan = startRotation.y + (value[1].y);
                cinemachineRotOffset.m_Dutch = startRotation.z + (value[1].z);
#else
                cinemachinePosOffset.Offset = startPosition + (value[0]);
                cinemachineRotOffset.Tilt = startRotation.x + (value[1].x);
                cinemachineRotOffset.Pan = startRotation.y + (value[1].y);
                cinemachineRotOffset.Dutch = startRotation.z + (value[1].z);
#endif
            }

#if !UNITY_6
            if (cvcCam)
                cvcCam.m_Lens.FieldOfView = startFOV + (value[2].x);
            if (cflCam)
                cflCam.m_Lens.FieldOfView = startFOV + (value[2].x);
#else
            if (cCam)
                cCam.Lens.FieldOfView = startFOV + (value[2].x);
#endif
        }


        internal sealed override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if (preset is SmoothShakeCinemachinePreset sscpreset)
            {
                positionShake.Clear();
                positionShake.AddRange(sscpreset.positionShake);
                rotationShake.Clear();
                rotationShake.AddRange(sscpreset.rotationShake);
                FOVShake.Clear();
                FOVShake.AddRange(sscpreset.FOVShake);
                timeSettings = sscpreset.timeSettings;
            }
            else
            {
                Debug.LogError("Preset is not a SmoothShakeCinemachinePreset");
            }
        }

        internal sealed override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { positionShake, rotationShake, FOVShake };
        }

        internal sealed override void ResetDefaultValues()
        {
            GetReferences();

#if !UNITY_6
            cinemachinePosOffset.m_Offset = startPosition;
            cinemachineRotOffset.m_Tilt = startRotation.x;
            cinemachineRotOffset.m_Pan = startRotation.y;
            cinemachineRotOffset.m_Dutch = startRotation.z;
            if (cvcCam)
                cvcCam.m_Lens.FieldOfView = startFOV;
            if (cflCam)
                cflCam.m_Lens.FieldOfView = startFOV;
#else
            cinemachinePosOffset.Offset = startPosition;
            cinemachineRotOffset.Tilt = startRotation.x;
            cinemachineRotOffset.Pan = startRotation.y;
            cinemachineRotOffset.Dutch = startRotation.z;
            if (cCam)
                cCam.Lens.FieldOfView = startFOV;
#endif
        }

        internal sealed override void SaveDefaultValues()
        {
            GetReferences();

#if !UNITY_6
            startPosition = cinemachinePosOffset.m_Offset;
            startRotation.x = cinemachineRotOffset.m_Tilt;
            startRotation.y = cinemachineRotOffset.m_Pan;
            startRotation.z = cinemachineRotOffset.m_Dutch;
            if (cvcCam)
                startFOV = cvcCam.m_Lens.FieldOfView;
            if (cflCam)
                startFOV = cflCam.m_Lens.FieldOfView;
#else
            startPosition = cinemachinePosOffset.Offset;
            startRotation.x = cinemachineRotOffset.Tilt;
            startRotation.y = cinemachineRotOffset.Pan;
            startRotation.z = cinemachineRotOffset.Dutch;
            if (cCam)
                startFOV = cCam.Lens.FieldOfView;
#endif
        }

        protected override Shaker[] GetShakers() { return null; }
    }
}
#endif