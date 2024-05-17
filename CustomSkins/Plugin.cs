// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using CustomSkins.Patches;
using HarmonyLib;
using Wish;

namespace CustomSkins;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public const string PLUGIN_GUID = "CustomSkins";
    public const string PLUGIN_NAME = "CustomSkins";
    public const string PLUGIN_VERSION = "0.1.0";

    public static readonly string RootPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Plugin)).Location) ?? string.Empty;

    public static ManualLogSource Log;

    private void Awake()
    {
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        var harmony = new Harmony(PLUGIN_GUID);
        harmony.PatchAll(typeof(CharacterClothingStylesPatch));
        harmony.PatchAll(typeof(ClothingLayerDataPatch));
        Log = Logger;

    }
}