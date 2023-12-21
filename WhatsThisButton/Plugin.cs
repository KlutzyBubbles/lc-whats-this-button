using BepInEx;
using UnityEngine;
using HarmonyLib;
using BepInEx.Logging;
using BepInEx.Configuration;

namespace WhatsThisButton
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Texture2D texture;

        public static ManualLogSource StaticLogger;

        private void Awake()
        {
            StaticLogger = Logger;
            ConfigEntry<bool> configDevMode = Config.Bind("General", "Dev Mode", false, "Whether or not to print additional console output");
            Patches.devMode = configDevMode.Value;

            Patches.keybinds = new Keybinds();
            new Harmony(PluginInfo.PLUGIN_GUID).PatchAll(typeof(Patches));
            StaticLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}