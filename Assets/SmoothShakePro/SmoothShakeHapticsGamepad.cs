using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Haptics Gamepad")]
    public class SmoothShakeHapticsGamepad : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakeHapticsGamepadPreset preset;

        public enum GamepadSelectionMethod { FirstConnected, LastConnected, Current, CustomIndex, All }

        [Header("Gamepad Settings")]
        public GamepadSelectionMethod gamepadSelectionMethod = GamepadSelectionMethod.Current;
        public int customIndex = 0;

        [Header("Shake Settings")]
        [Tooltip("Settings for low frequency motor")]
        public List<MultiFloatShaker> lowFrequencyMotorShake = new List<MultiFloatShaker>();
        [Tooltip("Settings for high frequency motor")]
        public List<MultiFloatShaker> highFrequencyMotorShake = new List<MultiFloatShaker>();

        [HideInInspector] internal ShakeToPreview shakeToPreview;
        internal enum ShakeToPreview { LowFrequencyMotor, HighFrequencyMotor };

#if ENABLE_INPUT_SYSTEM
        private Gamepad pad;
#endif

#if ENABLE_INPUT_SYSTEM
        private Gamepad GetGamepad()
        {
            switch (gamepadSelectionMethod) {
                case GamepadSelectionMethod.FirstConnected:
                    return Gamepad.all[0];
                case GamepadSelectionMethod.LastConnected:
                    return Gamepad.all[Gamepad.all.Count - 1];
                case GamepadSelectionMethod.Current:
                    return Gamepad.current;
                case GamepadSelectionMethod.CustomIndex:
                    if (customIndex < 0 || customIndex >= Gamepad.all.Count)
                    {
                        Debug.LogWarning("Trying to start a Smooth Shake Haptics with a custom index set to an invalid index: " + customIndex + ". There are " + Gamepad.all.Count + " gamepads connected");
                        return null;
                    }
                    return Gamepad.all[customIndex];
                default:
                    return null;
            }
        }
#endif

        internal new void Awake()
        {
            base.Awake();

#if ENABLE_INPUT_SYSTEM
            pad = GetGamepad();
#else
            Debug.LogWarning("Smooth Shake Haptics for Gamepads requires the Unity Input System to work. Please install it from the Unity Package Manager.");
#endif

            if (preset) ApplyPresetSettings(preset);
        }

        internal sealed override void Apply(Vector3[] value)
        {
#if ENABLE_INPUT_SYSTEM

            if(gamepadSelectionMethod == GamepadSelectionMethod.All)
            {
                foreach (var gamepad in Gamepad.all)
                {
                    gamepad.SetMotorSpeeds(value[0].x, value[1].x);
                    gamepad.SetMotorSpeeds(0, 0);
                }
            }
            else
            {
                pad?.SetMotorSpeeds(value[0].x, value[1].x);
                pad?.SetMotorSpeeds(0, 0);
            }
#endif
        }

        internal override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if (preset is SmoothShakeHapticsGamepadPreset smoothShakeHapticsPreset)
            {
                lowFrequencyMotorShake.Clear();
                lowFrequencyMotorShake.AddRange(smoothShakeHapticsPreset.lowFrequencyMotorShake);
                highFrequencyMotorShake.Clear();
                highFrequencyMotorShake.AddRange(smoothShakeHapticsPreset.highFrequencyMotorShake);
            }
        }

        internal sealed override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { lowFrequencyMotorShake, highFrequencyMotorShake };
        }

        internal sealed override void ResetDefaultValues()
        {
#if ENABLE_INPUT_SYSTEM
            if (gamepadSelectionMethod == GamepadSelectionMethod.All)
            {
                foreach (var gamepad in Gamepad.all)
                {
                    gamepad.SetMotorSpeeds(0, 0);
                    StartCoroutine(StopMotorAfterDelay(gamepad, 0.1f));
                }
            }
            else
            {
                pad?.SetMotorSpeeds(0, 0);
                StartCoroutine(StopMotorAfterDelay(pad, 0.1f));
            }
#endif
        }

        internal sealed override void SaveDefaultValues()
        {
#if ENABLE_INPUT_SYSTEM
            pad = GetGamepad();
#else
            Debug.LogWarning("Smooth Shake Haptics for Gamepads requires the Unity Input System to work. Please install it from the Unity Package Manager.");
#endif
        }

#if ENABLE_INPUT_SYSTEM
        private IEnumerator StopMotorAfterDelay(Gamepad gamepad, float delay)
        {
            yield return new WaitForSeconds(delay);
            gamepad?.SetMotorSpeeds(0, 0);
        }
#endif

        protected override Shaker[] GetShakers() => null;
    }
}