// #define DUMP
#if DEBUG && DUMP
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Wish;
#endif

namespace CustomSkins.Manages;

public static class DumpManager
{
    public static void DumpCharacterClothingStyles()
    {
#if DEBUG && DUMP
        string path = Path.Combine(Assembly.GetCallingAssembly().Location, "items.log");
        StringBuilder builder = new StringBuilder();
        foreach (ClothingLayerData data in CharacterClothingStyles.AllClothing.Where(c => c.availableAtCharacterSelect))
        {
            builder.AppendLine("---------------------------------------------------");
            builder.Append(data.ToStingDebug());
            builder.AppendLine("---------------------------------------------------");
        }

        File.WriteAllText(path, builder.ToString());
        Plugin.Log.LogInfo($"Done logging items. Log in {path}");
#endif
    }

    public static void DumpAvailableItemDb()
    {
#if DEBUG && DUMP
        string path = Path.Combine(Assembly.GetCallingAssembly().Location, "items.log");
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
#endif
    }
}