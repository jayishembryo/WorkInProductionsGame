
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    public class GlobalSettingsObject : ScriptableObject
    {
#if UNITY_EDITOR
        public string basePath = "Assets/SmoothShakePro";

        [Tooltip("The default save path for presets. This path is used when the changeFileLocationPerSave is disabled.")]
        public string defaultSavePath = "Assets/SmoothShakePro/Presets";

        [Tooltip("If enabled, the save path will be changed per save. If disabled, the default save path will be used for every save.")]
        public bool changeFileLocationPerSave = true;
        [Tooltip("If enabled, a definition called SMOOTHSHAKEPRO is automatically added in Player Settings")]
        public bool addDefinitionOnInitiating = true;

        [Tooltip("The default timescale mode to use for all shakes")]
        public TimescaleMode defaultTimescaleMode = TimescaleMode.Scaled;
        [Tooltip("The default custom timescale to use for all shakes (only used when timescale mode is set to custom")]
        public float defaultCustomTimescale = 1f;

#if UNITY_2020
        [Tooltip("The color of the Smooth Shake Pro GUI elements")]
        public static Color SmoothShakeColor = new Color(0.94f, 0.15f, 0.3f);
#else
        [Tooltip("The color of the Smooth Shake Pro GUI elements")]
        public Color SmoothShakeColor = new(0.94f, 0.15f, 0.3f);
#endif
#endif
    }
}
