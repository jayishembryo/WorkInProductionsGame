using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [Flags]
    public enum Overrides 
    {
        Weight = 1 << 0,
        Bloom = 1 << 1,
        ChannelMixer = 1 << 2,
        ChromaticAberration = 1 << 3,
        ColorAdjustments = 1 << 4,
        FilmGrain = 1 << 5,
        LensDistortion = 1 << 6,
        MotionBlur = 1 << 7,
        PaniniProjection = 1 << 8,
        SplitToning = 1 << 9,
        Vignette = 1 << 10,
        WhiteBalance = 1 << 11,
#if RENDER_UNIVERSAL && !RENDER_HDRP
        DepthOfFieldGaussian = 1 << 12,
        DepthOfFieldBokeh = 1 << 13,
        ColorLookup = 1 << 14,
#endif
#if RENDER_HDRP && !RENDER_UNIVERSAL
        DepthOfFieldPhysical = 1 << 12,
        DepthOfFieldManual = 1 << 13,
        Fog = 1 << 14,
#endif
    }

#if SMOOTHPOSTPROCESSING
    [Flags]
    public enum SmoothPostProcessingOverrides
    {
        Blur = 1 << 0,
        Displace = 1 << 1,
        EdgeDetection = 1 << 2,
        Glitch = 1 << 3,
        Invert = 1 << 4,
        Kaleidoscope = 1 << 5,
        Monitor = 1 << 6,
        NightVision = 1 << 7,
        Pixelation = 1 << 8,
        RGBSplit = 1 << 9,
        RotBlur = 1 << 10,
        Solarize = 1 << 11,
        ThermalVision = 1 << 12,
        Tint = 1 << 13,
        Twirl = 1 << 14,
        Twitch = 1 << 15,
        WaveWarp = 1 << 16,
        ZoomBlur = 1 << 17,
#if RENDER_UNIVERSAL && !RENDER_HDRP
        GaussianBlur = 1 << 18,
        NoiseBloom = 1 << 19,
#endif
    }
#endif
}
