using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Wish;

namespace CustomSkins.Manages;

public static class SpritesManager
{
    public static readonly string TexturesPath = Path.Combine(Plugin.RootPath, "Resources", "Textures");

    public static readonly Dictionary<string, ClothingLayerSprites> CustomSprites = new();

    public static void SetupSprites()
    {
        var texturePaths = Directory.GetFiles(TexturesPath, "*.png");
        foreach (var texturePath in texturePaths)
        {
            Plugin.Log.LogInfo($"Processing {texturePath}");
            string name = Path.GetFileNameWithoutExtension(texturePath);
            if (!CustomSprites.TryGetValue(name, out ClothingLayerSprites spritesLayer)) continue;
            Plugin.Log.LogInfo($"Changing {name}");
            SetupCustomSpriteLayer(spritesLayer, texturePath);
        }
    }

    private static void SetupCustomSpriteLayer(ClothingLayerSprites layer, string texturePath)
    {
        for (var i = 0; i < layer._clothingLayerInfo.Count; i++)
        {
            ClothingLayerInfo info = layer._clothingLayerInfo[i];
            for (var j = 0; j < info.sprites.Length; j++)
            {
                info.sprites[j] = CreateNewSprite(info.sprites[j], texturePath);
            }

            layer._clothingLayerInfo[i] = info;
        }
    }

    private static Sprite CreateNewSprite(Sprite oldSprite, string newTexturePath)
    {
        var newTexture = new Texture2D(
            oldSprite.texture.width,
            oldSprite.texture.height
        );
        if (newTexture.LoadImage(File.ReadAllBytes(newTexturePath)))
        {
            newTexture.filterMode = FilterMode.Point;
            newTexture.wrapMode = TextureWrapMode.Clamp;
            newTexture.wrapModeU = TextureWrapMode.Clamp;
            newTexture.wrapModeV = TextureWrapMode.Clamp;
            newTexture.wrapModeW = TextureWrapMode.Clamp;
            newTexture.Apply();
            var sprite = Sprite.Create(
                newTexture,
                oldSprite.rect,
                new Vector2(
                    oldSprite.pivot.x / oldSprite.rect.width,
                    oldSprite.pivot.y / oldSprite.rect.height),
                oldSprite.pixelsPerUnit,
                0,
                SpriteMeshType.FullRect,
                oldSprite.border,
                true
            );
            return sprite;
        }

        Plugin.Log.LogError($"Failed to load {newTexturePath}");
        return null;
    }
}