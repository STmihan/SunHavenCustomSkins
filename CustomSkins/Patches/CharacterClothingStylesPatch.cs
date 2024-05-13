using System;
using System.Collections.Generic;
using System.Linq;
using CustomSkins.Manages;
using HarmonyLib;
using UnityEngine;
using Wish;
using Object = UnityEngine.Object;

namespace CustomSkins.Patches;

[HarmonyPatch(nameof(CharacterClothingStyles))]
public class CharacterClothingStylesPatch
{
    [HarmonyPatch(typeof(CharacterClothingStyles), "Awake")]
    [HarmonyPrefix]
    public static bool Awake_Prefix(CharacterClothingStyles __instance)
    {
        foreach (ClothingChangeable data in Plugin.ItemsToRegister)
        {
            ClothingLayerData item = CreateNewClothingData(data);
            RegisterNewItem(item);
        }

        return true;
    }

    private static ClothingLayerData CreateNewClothingData(ClothingChangeable data)
    {
        try
        {
            List<ClothingLayerData> all = CharacterClothingStyles.AllClothing.ToList();
            ClothingLayerData source = all.Last(c =>
                c.clothingLayers.FirstOrDefault() == data.Layer &&
                c.availableAtCharacterSelect == data.AvailableInMenu &&
                c.availableRaces.Intersect(data.Races).Count() == data.Races.Count
            );
            Plugin.Log.LogInfo($"Cloning {source.name}");

            ClothingLayerData clone = Object.Instantiate(source);
            clone.name = data.Name;
            clone.menuName = data.MenuName;
            clone.order = all.Count;

            return clone;
        }
        catch (Exception e)
        {
            // TODO: allow people to create full unique items with unique parameters
            Plugin.Log.LogError($"Something wrong with your item {data.Name}.");
            Plugin.Log.LogError(e);
            return null;
        }
    }

    private static void RegisterNewItem(ClothingLayerData item)
    {
        item.LoadClothingSprites(sp =>
        {
            ClothingLayerSprites spNew = Object.Instantiate(sp);
            for (var i = 0; i < spNew._clothingLayerInfo.Count; i++)
            for (var j = 0; j < spNew._clothingLayerInfo[i].sprites.Length; j++)
            {
                Sprite origSprite = spNew._clothingLayerInfo[i].sprites[j];
                Texture2D origSpriteTexture = origSprite.texture;
                var customTexture2D = new Texture2D(
                    origSpriteTexture.width,
                    origSpriteTexture.height,
                    origSpriteTexture.format,
                    false
                );
                customTexture2D.LoadRawTextureData(origSpriteTexture.GetRawTextureData());
                customTexture2D.name = item.name;
                customTexture2D.Apply();
                var customSprite = Sprite.Create(
                    customTexture2D,
                    origSprite.rect,
                    new Vector2(
                        origSprite.pivot.x / origSprite.rect.width,
                        origSprite.pivot.y / origSprite.rect.height),
                    origSprite.pixelsPerUnit,
                    0,
                    SpriteMeshType.FullRect,
                    origSprite.border,
                    true
                );
                spNew._clothingLayerInfo[i].sprites[j] = customSprite;
            }

            SpritesManager.CustomSprites[item.name] = spNew;
            SpritesManager.SetupSprites();
        });

        CharacterClothingStyles.AllClothing = CharacterClothingStyles.AllClothing.AddToArray(item);
        Plugin.Customs.Add(item);
    }
}