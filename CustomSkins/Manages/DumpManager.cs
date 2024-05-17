using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wish;

namespace CustomSkins.Manages;

public static class DumpManager
{
    public static void DumpCharacterClothingStyles()
    {
        const string path = @"D:\PetProjects\SunHavenCustomSkins\items.log";
        StringBuilder builder = new StringBuilder();
        foreach (ClothingLayerData data in CharacterClothingStyles.AllClothing.Where(c => c.availableAtCharacterSelect))
        {
            builder.AppendLine("---------------------------------------------------");
            builder.Append(data.ToStingDebug());
            builder.AppendLine("---------------------------------------------------");
        }

        File.WriteAllText(path, builder.ToString());
        Plugin.Log.LogInfo($"Done logging items. Log in {path}");
    }

    public static void DumpAvailableItemDb()
    {
        const string path = @"D:\PetProjects\SunHavenCustomSkins\items.db";
        StringBuilder builder = new StringBuilder();
        IEnumerable<ClothingLayerData> list = CharacterClothingStyles
            .AllClothing
            .Where(c => 
                c.availableAtCharacterSelect &&
                !string.IsNullOrWhiteSpace(c.menuName.Trim()) &&
                !string.IsNullOrWhiteSpace(c.name.Trim())
            );
        foreach (ClothingLayerData data in list)
        {
            builder.Append(data.menuName);
            builder.Append(" | ");
            builder.Append(data.name);
            builder.Append("\n");
        }

        File.WriteAllText(path, builder.ToString());
        Plugin.Log.LogInfo($"Done logging items. Log in {path}");
    }
}