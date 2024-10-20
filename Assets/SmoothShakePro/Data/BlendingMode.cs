using System;
using UnityEngine;

namespace SmoothShakePro
{
    public enum BlendingMode
    {
        Add,
        Multiply,
        Subtract,
        Average,
        Max,
        Min,
        MagnitudeBlend
    }

    public static class BlendingModes
    {
        public static Func<Vector3, Vector3, Vector3> GetBlendingMode(BlendingMode mode) => mode switch
        {
            BlendingMode.Add => (a, b) => a + b,
            BlendingMode.Multiply => (a, b) => Vector3.Scale(a, b),
            BlendingMode.Subtract => (a, b) => a - b,
            BlendingMode.Average => (a, b) => (a + b) / 2,
            BlendingMode.Max => (a, b) => new Vector3(
                Mathf.Max(a.x, b.x),
                Mathf.Max(a.y, b.y),
                Mathf.Max(a.z, b.z)),
            BlendingMode.Min => (a, b) => new Vector3(
                Mathf.Min(a.x, b.x),
                Mathf.Min(a.y, b.y),
                Mathf.Min(a.z, b.z)),
            BlendingMode.MagnitudeBlend => (a, b) => (a.magnitude > b.magnitude) ? a : b,
            _ => null
        };
    }
}
