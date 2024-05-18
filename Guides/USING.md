### Installation

- Install [BepInEx 5](https://github.com/BepInEx/BepInEx) on Sun Haven (just unpack all files from the archive into the game
  folder and run the game once).
- Download the latest release from the [Releases](https://github.com/STmihan/SunHavenCustomSkins/releases) tab.
- Unpack the `CustomSkins` folder from the archive into `BepInEx\plugins`.
- If everything is installed correctly, you should see new skins in the Face (Artist Face) and Chest (Shirt) tabs when creating a
  character.
- To delete these skins (if you don't need them), just delete everything from the `BepInEx\plugins\CustomSkins\Resources\Items`
  and `BepInEx\plugins\CustomSkins\Resources\Textures` folders.

### Adding New Skins

- Move the JSON configuration file of the skin to `BepInEx\plugins\CustomSkins\Resources\Items`.
- Move the PNG texture of the skin to `BepInEx\plugins\CustomSkins\Resources\Textures`.
- **IMPORTANT**: Make sure that the names of these files and the `Name` parameter in the JSON file are the same.

### Creating New Skins

You can use the two skins that come with the mod as a basis.

- To conveniently create new skins, you will need to
  install [Plugin configuration manager for BepInEx](https://github.com/BepInEx/BepInEx.ConfigurationManager) (just place the
  ConfigurationManager.dll from the archive into the `BepInEx\plugins` folder).
- Open the game and press F1.
- In the opened window, select the CustomTextures plugin.
- In the search window, you need to enter the name of the skin you want to use as a base.
- You can save it as a PNG file by clicking the Save button.
- Also you can see the name of the skin there.
- And the Layers used by the skin.
- Then create your JSON configuration file for the skin according to the [structure](#json-configuration-file-structure) and place
  it in `BepInEx\plugins\CustomSkins\Resources\Items`.
- Then modify the saved texture as you wish and place it in `BepInEx\plugins\CustomSkins\Resources\Textures`.
- **IMPORTANT**: The name of the texture file, the name of the JSON file, and the `Name` parameter in the JSON configuration file
  must all match.

### JSON Configuration File Structure

| Parameter         | Available Value                                                                                                                                                                                                                             | Description                                                                                                                                                                                                                                     |
|-------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| SpriteInheritance | Script name of the base texture                                                                                                                                                                                                             | Specify the name of the sprite you want to inherit from (the one you took as a basis)                                                                                                                                                           |
| Name              | String without spaces                                                                                                                                                                                                                       | This is the name by which your skin will be available in scripts                                                                                                                                                                                |
| MenuName          | Any string                                                                                                                                                                                                                                  | The name of the skin that will be displayed in the menu                                                                                                                                                                                         |
| Layers            | FrontArms<br/>BackArms<br/>FrontGloves<br/>BackGloves<br/>Back<br/>Tail<br/>FrontSleeves<br/>Hat<br/>Hair<br/>Eyes<br/>Mouth<br/>Ears<br/>Head<br/>Chest<br/>Body<br/>Pants<br/>Legs<br/>Overlay<br/>Face<br/>HairAccessory<br/>BackSleeves | The slots on the character that your skin will occupy. **Important**: If, for example, you made a chest skin with sleeves, but did not specify FrontSleeves in Layers, the sleeves will not be visible. The same logic applies to other layers. |
| Races             | Human<br/>Elf<br/>Amari<br/>Naga<br/>Elemental<br/>Angel<br/>Demon                                                                                                                                                                          | The races that your skin will work with                                                                                                                                                                                                         |

#### Example JSON Configuration File

```json
{
  "SpriteInheritance": "chest_bird_shirt_green",
  "Name": "shirt_green",
  "MenuName": "Shirt",
  "Layers": [
    "Chest",
    "FrontSleeves"
  ],
  "Races": [
    "Human"
  ]
}
```
