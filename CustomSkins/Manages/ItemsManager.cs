﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using Wish;
using Object = UnityEngine.Object;

namespace CustomSkins.Manages;

public class SearchResult
{
    public Texture2D Texture { get; set; }
    public string Name { get; set; }
    public string MenuName { get; set; }
    public List<ClothingLayer> Layers { get; set; }
}

public static class ItemsManager
{
    public static readonly List<ClothingLayerData> Customs = new();
    public static readonly List<ClothingChangeable> ItemsToRegister = new();

    public static void RegisterItems()
    {
        ItemsToRegister.AddRange(PluginConfig.Register());

        foreach (ClothingChangeable data in ItemsToRegister)
        {
            ClothingLayerData item = CreateNewClothingData(data);
            RegisterNewItem(item);
        }
    }

    public static async Task<SearchResult> SearchForItem(string menuName)
    {
        ClothingLayerData result = CharacterClothingStyles
            .AllClothing
            .FirstOrDefault(c => c.menuName.Contains(menuName));
        if (result == null) return null;
        var tsc = new TaskCompletionSource<Texture2D>();

        result.LoadClothingSprites(layer =>
        {
            try
            {
                Texture2D tex = layer._clothingLayerInfo[0].sprites[0].texture;
                tsc.SetResult(tex);
            }
            catch (Exception e)
            {
                Plugin.Log.LogError(e.Message);
                tsc.SetResult(null);
            }
        });
        Texture2D texture2D = await tsc.Task;
        return new SearchResult
        {
            Name = result.name,
            MenuName = result.menuName,
            Texture = texture2D,
            Layers = result.clothingLayers,
        };
    }

    public static async void SearchItemDrawer(ConfigEntryBase entry)
    {
        string value = entry.BoxedValue as string;
        GUILayout.BeginVertical();
        entry.SetSerializedValue(GUILayout.TextField(value));
        if (value != null && value.Length >= 3)
        {
            SearchResult found = await SearchForItem(value);
            if (found != null)
            {
                GUILayout.BeginVertical();
                GUILayout.Box(found.Texture, GUILayout.MaxWidth(270));
                GUILayout.Label("MenuName: " + found.MenuName);
                GUILayout.Label("Name: " + found.Name);
                GUILayout.Label("Layers: " + string.Join(",", found.Layers));
                if (GUILayout.Button("Save"))
                {
                    var path = Path.Combine(Application.persistentDataPath, "textures", $"{found.Texture.name}.png");
                    var directoryName = Path.GetDirectoryName(path);
                    if (directoryName != null)
                    {
                        Directory.CreateDirectory(directoryName);
                        var png = found.Texture.EncodeToPNG();
                        File.WriteAllBytes(path, png);
                        Application.OpenURL($"file://{directoryName}");
                    }
                }

                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.Label($"Not found: {value}");
            }
        }

        GUILayout.EndVertical();
    }

    private static ClothingLayerData CreateNewClothingData(ClothingChangeable data)
    {
        ClothingLayerData inh = CharacterClothingStyles.AllClothing.First(c => c.name == data.SpriteInheritance);
        ClothingLayerData item = ScriptableObject.CreateInstance<ClothingLayerData>();
        item.name = data.Name;
        item.menuName = data.MenuName;
        item.availableAtCharacterSelect = true; // TODO: FOR NOW ONLY SUPPORT THIS TYPE OF SKINS
        item.order = CharacterClothingStyles.AllClothing.Length;
        item.availableRaces = data.Races;
        item.requiresCompatibleBody = false;
        item.compatibleBodies = new List<ClothingLayerData>();
        item.hasColors = false;
        item.clothingColors = new List<ClothingColorData>();
        item.material = null;
        item.extra = null;
        item.armorData = null;
        item.canUseHumanHair = false;
        item.mainMenuOverideSprite = null;
        item.isHelmet = data.Layers.Contains(ClothingLayer.Hat);
        item.isHair = data.Layers.Contains(ClothingLayer.Hair);
        item.isEyes = data.Layers.Contains(ClothingLayer.Eyes);
        item.isFace = data.Layers.Contains(ClothingLayer.Face);
        item.isBody = false;
        item.clothingLayers = data.Layers;
        item.HatHair = HatType.Default;
        item.male = false;
        item.faceOnTopOfHair = false;
        item.faceBehindHairWhenFacingNorth = false;
        item.subRace = SubRace.Default;
        SetClothingLayerSprites(item, inh);

        return item;
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
                    origSpriteTexture.mipmapCount,
                    false
                );
                customTexture2D.filterMode = origSpriteTexture.filterMode;
                customTexture2D.wrapMode = origSpriteTexture.wrapMode;
                customTexture2D.wrapMode = origSpriteTexture.wrapMode;
                customTexture2D.wrapModeU = origSpriteTexture.wrapModeU;
                customTexture2D.wrapModeW = origSpriteTexture.wrapModeW;
                customTexture2D.wrapModeV = origSpriteTexture.wrapModeV;
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
        Customs.Add(item);
    }

    private static void SetClothingLayerSprites(ClothingLayerData to, ClothingLayerData from)
    {
        var copy = (AssetReferenceClothingLayerSprites)from
            .GetType()
            .GetField("clothingLayerSprites", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(from);
        Plugin.Log.LogInfo($"copy: is null: {copy == null}");
        Plugin.Log.LogInfo($"copy: {copy}");
        if (copy != null)
        {
            Plugin.Log.LogInfo($"copy: RuntimeKey: {copy.RuntimeKey}");
            var reference = new AssetReferenceClothingLayerSprites(copy.RuntimeKey.ToString());
            Plugin.Log.LogInfo($"reference: {reference}");
            FieldInfo toField = to
                .GetType()
                .GetField("clothingLayerSprites", BindingFlags.Instance | BindingFlags.NonPublic)!;

            Plugin.Log.LogInfo($"toField: is null: {toField == null}");
            if (toField != null)
            {
                toField.SetValue(to, reference);

                var toNew = toField.GetValue(to);
                Plugin.Log.LogInfo($"toNew: is null: {toNew == null}");
                Plugin.Log.LogInfo($"toNew: {toNew}");
            }
        }
    }
}