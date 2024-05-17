using CustomSkins.Manages;
using HarmonyLib;
using Wish;

namespace CustomSkins.Patches;

[HarmonyPatch(nameof(CharacterClothingStyles))]
public class CharacterClothingStylesPatch
{
    [HarmonyPatch(typeof(CharacterClothingStyles), "Awake")]
    [HarmonyPrefix]
    public static bool Awake_Prefix(CharacterClothingStyles __instance)
    {
        ItemsManager.RegisterItems();
        
        DumpManager.DumpCharacterClothingStyles();
        DumpManager.DumpAvailableItemDb();
        return true;
    }
}
