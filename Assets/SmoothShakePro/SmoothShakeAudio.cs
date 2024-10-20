using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Audio")]
    public class SmoothShakeAudio : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakeAudioPreset preset;

        [Header("Audio to Shake")]
        [Tooltip("Audio to Shake")]
        public AudioSource audioToShake;

        [Header("Shake Settings")]
        [Tooltip("Settings for Volume Shake")]
        public List<MultiFloatShaker> volumeShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for Pan Shake")]
        public List<MultiFloatShaker> panShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for Pitch Shake")]
        public List<MultiFloatShaker> pitchShake = new List<MultiFloatShaker>();

        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { Volume, Pan, Pitch }

        private float startVolume;
        private float startPan;
        private float startPitch;

        private bool GetAudio()
        {
            if(audioToShake != null) return true;

            if (GetComponent<AudioSource>() != null)
            {
                audioToShake = GetComponent<AudioSource>();
                return true;
            }
            return false;
        }

        internal new void Awake()
        {
            base.Awake();

            GetAudio();

            if (preset) ApplyPresetSettings(preset);
        }

        protected override Shaker[] GetShakers() => null;

        internal sealed override void Apply(Vector3[] value)
        {
            if (!GetAudio()) return;

            audioToShake.volume = startVolume + value[0].x;
            audioToShake.panStereo = startPan + value[1].x;
            audioToShake.pitch = startPitch + value[2].x;
        }

        internal sealed override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if (preset is SmoothShakeAudioPreset)
            {
                SmoothShakeAudioPreset smoothShakeAudioPreset = (SmoothShakeAudioPreset)preset;
                volumeShake.Clear();
                volumeShake.AddRange(smoothShakeAudioPreset.volumeShake);
                panShake.Clear();
                panShake.AddRange(smoothShakeAudioPreset.panShake);
                pitchShake.Clear();
                pitchShake.AddRange(smoothShakeAudioPreset.pitchShake);
                timeSettings = smoothShakeAudioPreset.timeSettings;
            }
        }

        internal sealed override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { volumeShake, panShake, pitchShake };
        }

        internal sealed override void SaveDefaultValues()
        {
            startVolume = audioToShake.volume;
            startPan = audioToShake.panStereo;
            startPitch = audioToShake.pitch;
        }

        internal sealed override void ResetDefaultValues()
        {
            audioToShake.volume = startVolume;
            audioToShake.panStereo = startPan;
            audioToShake.pitch = startPitch;
        }
    }
}
