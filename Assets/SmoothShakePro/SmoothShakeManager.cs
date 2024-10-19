using System.Collections.Generic;
using UnityEngine;

namespace SmoothShakePro
{
    [AddComponentMenu("Smooth Shake Pro/Smooth Shake Manager")]
    public class SmoothShakeManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Tooltip("List of saved shakes")]
#if UNITY_2020
        public List<SavedShake> savedShakes = new List<SavedShake>();
        private readonly Dictionary<string, ShakeBase> shakeDictionary = new Dictionary<string, ShakeBase>();
#else
        public List<SavedShake> savedShakes = new();
        private readonly Dictionary<string, ShakeBase> shakeDictionary = new();
#endif

        public void OnAfterDeserialize()
        {
            shakeDictionary.Clear();
            HashSet<string> usedNames = new HashSet<string>();

            foreach (var savedShake in savedShakes)
            {
                string uniqueName = GetUniqueName(savedShake.name, usedNames);
                if(uniqueName == "") uniqueName = "New Shake";
                savedShake.name = uniqueName; // Update the name in the list to the unique name
                shakeDictionary.Add(uniqueName, savedShake.shake);
                usedNames.Add(uniqueName);
            }
        }

        public void OnBeforeSerialize() { }

        /// <summary>
        /// Start a shake by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="preset"></param>
        public void StartShake(string name, ShakeBasePreset preset = null)
        {
            if (shakeDictionary.ContainsKey(name))
            {
                if (preset)
                    shakeDictionary[name].StartShake(preset);
                else
                    shakeDictionary[name].StartShake();
            }
            else
            {
                Debug.LogWarning("Shake with name " + name + " not found");
            }
        }

        /// <summary>
        /// Stop a shake by name
        /// </summary>
        /// <param name="name"></param>
        public void StopShake(string name)
        {
            if (shakeDictionary.ContainsKey(name))
            {
                shakeDictionary[name].StopShake();
            }
            else
            {
                Debug.LogWarning("Shake with name " + name + " not found");
            }
        }

        /// <summary>
        /// Force stop a shake by name
        /// </summary>
        /// <param name="name"></param>
        public void ForceStop(string name)
        {
            if (shakeDictionary.ContainsKey(name))
            {
                shakeDictionary[name].ForceStop();
            }
            else
            {
                Debug.LogWarning("Shake with name " + name + " not found");
            }
        }

        private string GetUniqueName(string baseName, HashSet<string> usedNames)
        {
            string uniqueName = baseName;
            string append = "(Copy)";

            while (usedNames.Contains(uniqueName))
            {
                uniqueName = $"{baseName} {append}";
            }

            return uniqueName;
        }
    }

    [System.Serializable]
    public class SavedShake
    {
        [Tooltip("Name of this shake")]
        public string name;
        [Tooltip("The shake to save")]
        public ShakeBase shake;
    }

}
