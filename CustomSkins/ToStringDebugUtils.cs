using System;
using System.Collections;
using System.Text;
using Wish;

namespace CustomSkins;

public static class ToStringDebugUtils
{
    public static string ToStingDebug(this ClothingLayerData data)
    {
        if (data == null) return "NULL\n";
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"menuName: {data.menuName}");
        stringBuilder.AppendLine($"name: {data.name}");
        stringBuilder.AppendLine($"availableAtCharacterSelect: {data.availableAtCharacterSelect}");
        stringBuilder.AppendLine($"availableRaces: {data.availableRaces.ToStringEnumeration(4)}");
        stringBuilder.AppendLine($"requiresCompatibleBody: {data.requiresCompatibleBody}");
        stringBuilder.AppendLine($"compatibleBodies: {data.compatibleBodies.ToStringEnumeration(4)}");
        stringBuilder.AppendLine($"hasColors: {data.hasColors}");
        stringBuilder.AppendLine(
            $"clothingColors: {data.clothingColors.ToStringEnumeration(4, o => {
                if (o is ClothingColorData d) {
                    return $"{d.clothingColor}";
                }

                return o.ToString();
            })}");
        stringBuilder.AppendLine($"material: {(data.material ? data.material.name : "null")}");
        stringBuilder.AppendLine($"extra: {(data.extra != null ? data.extra.name : "<null>")}");
        stringBuilder.AppendLine($"armorData: {data.armorData != null}"); // TODO
        stringBuilder.AppendLine($"canUseHumanHair: {data.canUseHumanHair}");
        stringBuilder.AppendLine("clothingLayerSprites: <skip>");
        if (data.mainMenuOverideSprite != null)
            stringBuilder.AppendLine(data.mainMenuOverideSprite.texture != null
                ? $"mainMenuOverideSprite: {data.mainMenuOverideSprite.texture.name}"
                : "mainMenuOverideSprite: texture null");
        else
            stringBuilder.AppendLine($"mainMenuOverideSprite: sprite null");
        stringBuilder.AppendLine($"isHelmet: {data.isHelmet}");
        stringBuilder.AppendLine($"isHair: {data.isHair}");
        stringBuilder.AppendLine($"isEyes: {data.isEyes}");
        stringBuilder.AppendLine($"isFace: {data.isFace}");
        stringBuilder.AppendLine($"isBody: {data.isBody}");
        stringBuilder.AppendLine($"clothingLayers: {data.clothingLayers.ToStringEnumeration(4)}");
        stringBuilder.AppendLine($"HatHair: {data.HatHair.ToString() ?? "NULL"}");
        stringBuilder.AppendLine($"male: {data.male}");
        stringBuilder.AppendLine($"faceOnTopOfHair: {data.faceOnTopOfHair}");
        stringBuilder.AppendLine($"faceBehindHairWhenFacingNorth: {data.faceBehindHairWhenFacingNorth}");
        stringBuilder.AppendLine($"subRace: {data.subRace.ToString() ?? "NULL"}");

        return stringBuilder.ToString();
    }

    public static string ToStringEnumeration(this IList enumerable, int intent = 0, Func<object, string> customToString = null)
    {
        if (enumerable == null) return "NULL";
        if (enumerable.Count == 0) return "EMPTY";
        var builder = new StringBuilder();
        builder.AppendLine();
        foreach (var o in enumerable)
        {
            for (int i = 0; i < intent; i++)
            {
                builder.Append(" ");
            }

            if (customToString == null)
            {
                builder.Append(o != null ? o.ToString() : "NULL");
            }
            else
            {
                builder.Append(o != null ? customToString(o) : "NULL");
            }

            builder.Append("\n");
        }

        return builder.ToString();
    }
}