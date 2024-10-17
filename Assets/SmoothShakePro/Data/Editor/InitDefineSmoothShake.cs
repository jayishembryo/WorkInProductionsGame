#if UNITY_EDITOR
using UnityEditor;

namespace SmoothShakePro
{
    internal static class InitDefineSmoothShake
    {
        [InitializeOnLoadMethod]
        public static void Init()
        {
#if UNITY_2023
            UnityEditor.Build.NamedBuildTarget target = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (EditorUtility.addDefinitionOnInitiating) SetDefineSymbol(target);
#else
            if (EditorUtility.addDefinitionOnInitiating) SetDefineSymbol(EditorUserBuildSettings.selectedBuildTargetGroup);
#endif
        }
#if UNITY_2023
        public static void SetDefineSymbol(UnityEditor.Build.NamedBuildTarget target)
#else
        public static void SetDefineSymbol(BuildTargetGroup targetGroup)
#endif
        {
#if UNITY_2023
            string currData = PlayerSettings.GetScriptingDefineSymbols(target);
#else
            string currData = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
#endif
            if (!currData.Contains("SMOOTHSHAKEPRO"))
            {
                if (string.IsNullOrEmpty(currData))
                {
#if UNITY_2023
                    PlayerSettings.SetScriptingDefineSymbols(target, "SMOOTHSHAKEPRO");
#else
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, "SMOOTHSHAKEPRO");
#endif
                }
                else
                {
                    if (!currData[currData.Length - 1].Equals(';'))
                    {
                        currData += ';';
                    }
                    currData += "SMOOTHSHAKEPRO";
#if UNITY_2023
                    PlayerSettings.SetScriptingDefineSymbols(target, currData);
#else
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, currData);
#endif
                }
            }
        }
    }
}
#endif