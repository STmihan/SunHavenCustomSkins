using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Wish;

namespace CustomSkins;

[JsonObject]
public class ClothingChangeable
{
    public string SpriteInheritance { get; set; }
    public string Name { get; set; }
    public string MenuName { get; set; }
    public List<ClothingLayer> Layers { get; set; }

    [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
    public List<Race> Races { get; set; } = new()
    {
        Race.Human,
        Race.Elf,
        Race.Amari,
        Race.Naga,
        Race.Elemental,
        Race.Angel,
        Race.Demon,
    };

    public override string ToString()
    {
        return $"{Name} ({MenuName}) - {string.Join(",", Layers)} - {string.Join(",", Races)} - From: {SpriteInheritance}";
    }
}

public static class PluginConfig
{
    public static readonly string ItemsPath = Path.Combine(Plugin.RootPath, "Resources", "Items");

    public static List<ClothingChangeable> Register()
    {
        var list = new List<ClothingChangeable>();

        Plugin.Log.LogInfo($"Looking for items in {ItemsPath}");
        foreach (var file in Directory.GetFiles(ItemsPath, "*.json"))
        {
            Plugin.Log.LogInfo($"Processing {file}");
            list.Add(FromString(File.ReadAllText(file)));
        }

        return list;
    }

    private static ClothingChangeable FromString(string config)
    {
        var item = JsonConvert.DeserializeObject<ClothingChangeable>(config);
        Plugin.Log.LogInfo(item);
        return item;
    }
}