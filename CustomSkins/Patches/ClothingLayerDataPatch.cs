using CustomSkins.Manages;
using HarmonyLib;
using UnityEngine.Events;
using Wish;

namespace CustomSkins.Patches;

[HarmonyPatch(nameof(ClothingLayerData))]
public class ClothingLayerDataPatch
{
    [HarmonyPatch(typeof(ClothingLayerData), "LoadClothingSprites")]
    [HarmonyPrefix]
    public static bool LoadClothingSprites_Prefix(ClothingLayerData __instance, UnityAction<ClothingLayerSprites> onLoad)
    {
        if (SpritesManager.CustomSprites.TryGetValue(__instance.name, out ClothingLayerSprites spriteLayer))
        {
            Plugin.Log.LogInfo($"Loaded custom {__instance.name} sprite");
            onLoad?.Invoke(spriteLayer);
            return false;
        }
        return true;
    } 
}