using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SmoothShakePro
{
    [System.Serializable]
    public class PropertyToShake
    {
        [Tooltip("Property that will be affected")]
        public string propertyName;
        [Tooltip("Type of property that will be affected")]
        public PropertyType propertyType;
#if UNITY_2020
        public List<MultiFloatShaker> floatShake = new List<MultiFloatShaker>();
        public List<MultiVectorShaker> vectorShake = new List<MultiVectorShaker>();
#else
        public List<MultiFloatShaker> floatShake = new();
        public List<MultiVectorShaker> vectorShake = new();
#endif

        public enum PropertyType
        {
            Float,
            Vector,
        }
    }

    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Material")]
    public class SmoothShakeMaterial : MultiShakeBase
    {
        [Tooltip("Preset to use for this Smooth Shake")]
        public SmoothShakeMaterialPreset preset;

        [Header("Renderer / Material Settings")]
        [Tooltip("If true, the material will be taken from the renderer of this object, if false, the material assigned to this component will be used")]
        public bool useMaterialFromRenderer = true;
        [Tooltip("Renderer to take the material from, if useMaterialFromRenderer is true")]
        public Renderer objectRenderer;
        [Tooltip("Material to shake, if useMaterialFromRenderer is false or renderer has multiple materials")]
        public Material material;

        [Header("Shake Settings")]
        public PropertyToShake propertyToShake;

        private int matIndex = 0;
        private Vector3 startVector;
        private float startFloat;

        [HideInInspector] public ShakeToPreview shakeToPreview;
        public enum ShakeToPreview { Float, Vector }

        internal bool MultipleMaterials()
        {
#if UNITY_EDITOR
            if(objectRenderer.sharedMaterials.Length > 1)
#else
            if(objectRenderer.materials.Length > 1)
#endif
            {
                if (GetMaterial() == null)
                {
                    Debug.LogWarning("Multiple materials found on renderer, SmoothShakeMaterial doesn't know which material to shake, please assign a material to the SmoothShakeMaterial component on " + gameObject.name);
                    return false;
                }
                else
                {
                    for (int i = 0; i < objectRenderer.materials.Length; i++)
                    {
                        string name = objectRenderer.materials[i].name;
#if UNITY_2020
                        string baseName = name;
                        string suffixToRemove = " (Instance)";

                        if (baseName.EndsWith(suffixToRemove))
                        {
                            baseName = baseName.Substring(0, baseName.Length - suffixToRemove.Length);
                        }
#else
                        string baseName = name[..^" (Instance)".Length];
#endif
                        if (baseName == material.name)
                        {
                            matIndex = i;
                            return true;
                        }
                    }

                    Debug.LogWarning("Renderer has multiple materials, but material assigned to SmoothShakeMaterial component on " + gameObject.name + " wasn't found between them, please assign a correct material to the SmoothShakeMaterial component on " + gameObject.name);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        internal bool CheckRenderer()
        {
            if (!UseMaterialFromRenderer()) return false;

            if (objectRenderer == null)
            {
                if (gameObject.GetComponent<Renderer>() != null)
                {
                    objectRenderer = gameObject.GetComponent<Renderer>();
                    Debug.Log("Retrieved material from Renderer");
                    return true;
                }
                else if (gameObject.GetComponent<ParticleSystem>() != null)
                {
                    objectRenderer = gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>();
                    Debug.Log("Retrieved material from Particle System Renderer");
                    return true;
                }
                else if (gameObject.GetComponent<Image>() != null)
                {
                    useMaterialFromRenderer = false;
                    material = gameObject.GetComponent<Image>().material;
                    Debug.Log("Retrieved material from Image");
                    return false;
                }
                else if(gameObject.GetComponent<RawImage>() != null)
                {
                    useMaterialFromRenderer = false;
                    material = gameObject.GetComponent<RawImage>().material;
                    Debug.Log("Retrieved material from Raw Image");
                    return false;
                }
                else
                {
                    Debug.LogWarning("No renderer assigned or found on object, please assign a renderer to the SmoothShakeMaterial component on " + gameObject.name);
                    useMaterialFromRenderer = false;
                    return false;
                }
            }
            else return true;
        }

        private bool UseMaterialFromRenderer()
        {
            if (useMaterialFromRenderer) return true;
            else
            {
                if (material == null) Debug.LogWarning("No material assigned to SmoothShakeMaterial component on " + gameObject.name);
                return false;
            }
        }

        private Material GetMaterial()
        {
            if (material) return material;
            else
            {
                if (CheckRenderer())
                {
                    if (MultipleMaterials())
                    {
#if UNITY_EDITOR
                        material = objectRenderer.sharedMaterials[matIndex];
#else
                        material = objectRenderer.materials[matIndex];
#endif
                        return material;
                    }
                    else
                    {
#if UNITY_EDITOR
                        material = objectRenderer.sharedMaterials[0];
#else
                        material = objectRenderer.materials[0];
#endif
                        return material;
                    }
                }
                Debug.LogWarning("No material assigned to SmoothShakeMaterial component on " + gameObject.name);
                return null;
            }
        }

        internal new void Awake()
        {
            base.Awake();

            material = GetMaterial();

            if (preset) ApplyPresetSettings(preset);
        }

        internal sealed override void Apply(Vector3[] value)
        {
            if (GetMaterial())
            {
                ShakeProperty(value, propertyToShake);
            }
        }

        private void ShakeProperty(Vector3[] value, PropertyToShake property)
        {
            switch (property.propertyType)
            {
                case PropertyToShake.PropertyType.Float:
                    GetMaterial().SetFloat(property.propertyName, startFloat + (value[0].x));
                    break;
                case PropertyToShake.PropertyType.Vector:
                    GetMaterial().SetVector(property.propertyName, startVector + (value[1]));
                    break;
            }
        }

        internal sealed override void ApplyPresetSettings(ShakeBasePreset preset)
        {
            if (preset is SmoothShakeMaterialPreset)
            {
                SmoothShakeMaterialPreset smoothShakeMaterialPreset = preset as SmoothShakeMaterialPreset;
                timeSettings = smoothShakeMaterialPreset.timeSettings;
                useMaterialFromRenderer = smoothShakeMaterialPreset.useMaterialFromRenderer;
                if(!useMaterialFromRenderer || MultipleMaterials()) material = smoothShakeMaterialPreset.material;
                propertyToShake = smoothShakeMaterialPreset.propertyToShake;
            }
            else
            {
                Debug.LogWarning("Invalid preset, not of type SmoothShakeMaterial");
            }
        }

        internal sealed override IEnumerable<Shaker>[] GetMultiShakers()
        {
            return new IEnumerable<Shaker>[] { propertyToShake.floatShake, propertyToShake.vectorShake };
        }

        internal sealed override void ResetDefaultValues()
        {
            if (material)
            {
                switch (propertyToShake.propertyType)
                {
                    case PropertyToShake.PropertyType.Float:
                        GetMaterial().SetFloat(propertyToShake.propertyName, startFloat);
                        break;
                    case PropertyToShake.PropertyType.Vector:
                        GetMaterial().SetVector(propertyToShake.propertyName, startVector);
                        break;
                }
            }
        }

        internal sealed override void SaveDefaultValues()
        {
            if (GetMaterial())
            {
                switch (propertyToShake.propertyType)
                {
                    case PropertyToShake.PropertyType.Float:
                        startFloat = GetMaterial().GetFloat(propertyToShake.propertyName);
                        break;
                    case PropertyToShake.PropertyType.Vector:
                        startVector = GetMaterial().GetVector(propertyToShake.propertyName);
                        break;
                }
            }
        }

        protected override Shaker[] GetShakers() => null;
    }

}
