﻿### Установка

- Установить [BepInEx 5](https://github.com/BepInEx/BepInEx) на Sun Haven (просто распакуйте все файлы из архива в папку с игрой и
  запустите игру 1 раз)
- Скачать последний релиз из вкладки [Releases](https://github.com/STmihan/SunHavenCustomSkins/releases)
- Распакуйте папку `CustomSkins` из архива в `BepInEx\plugins`
- Если всё установилось правильно, то при создании персонажа у вас должны появится новые скины во вкладках Face (Artist Face) и
  Chest (Shirt)
- Чтобы удалить эти скины (если они вам не нужны) просто удалите всё из папки `BepInEx\plugins\CustomSkins\Resources\Items`
  и `BepInEx\plugins\CustomSkins\Resources\Textures`

### Добавление новых скинов

- Переместить json файл с конфигурацией скина в `BepInEx\plugins\CustomSkins\Resources\Items`
- Переместить png текстуру скина в `BepInEx\plugins\CustomSkins\Resources\Textures`
- **ВАЖНО**: убедитесь, что названия этих файлов и параметр name в json файле одинаковые

### Создание новых скинов

За основу вы можете взять 2 скина, которые уже поставляются с модом

- Чтобы удобно создавать новые скины, вам нужно будет
  поставить [Plugin configuration manager for BepInEx](https://github.com/BepInEx/BepInEx.ConfigurationManager) (Просто положите
  ConfigurationManager.dll из архива в папку `BepInEx\plugins`)
- Откройте игру и нажмите F1
- В открывшимся окне выберите плагин CustomTextures
- В окне поиска вам надо будет вписать название скина, который вы хотите взять за основу
- На кнопку Save вы можете сохранить его в png формате
- Там же можно увидеть название скина
- Там же можно увидеть Layers, которые использует скин
- Дальше создайте свой json файл для конфигурации скина по [структуре](#структура-json-файла-конфигурации) и поместить его
  в `BepInEx\plugins\CustomSkins\Resources\Items`.
- Дальше измените созранённую текстуру так, как вам хочется и положите её в `BepInEx\plugins\CustomSkins\Resources\Textures`
- **ВАЖНО**: Название файла текстуры, название json файла конфигурации и параметр `Name` в файле конфигурации **ДОЛЖНЫ БЫТЬ
  ОДИНАКОВЫМИ**

#### Структура json файла конфигурации

| Параметр          | Доступные значения                                                                                                                                                                                                                          | Описание                                                                                                                                                                                                                  |
|-------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| SpriteInheritance | Любая строка                                                                                                                                                                                                                                | Сюда вписать название скина, скин которого вы брали. Это нужно для того, чтобы не прописывать вручную все параметры текстуры                                                                                              |
| Name              | Строка без пробелов                                                                                                                                                                                                                         | Это название, по которому ваш скин будет доступен в скриптах                                                                                                                                                              |
| MenuName          | Любая строка                                                                                                                                                                                                                                | Название скина, которое будет отображаться в меню                                                                                                                                                                         |
| Layers            | FrontArms<br/>BackArms<br/>FrontGloves<br/>BackGloves<br/>Back<br/>Tail<br/>FrontSleeves<br/>Hat<br/>Hair<br/>Eyes<br/>Mouth<br/>Ears<br/>Head<br/>Chest<br/>Body<br/>Pants<br/>Legs<br/>Overlay<br/>Face<br/>HairAccessory<br/>BackSleeves | Слоты, на персонаже которые будет занимать ваш скин. **Важно**: Если вы, допустим, сделали скин на грудь с рукавами, но не указали FrontSleeves в Layers, то рукава видны не будут. Такая же логика и с остальными слоями |
| Races             | Human<br/>Elf<br/>Amari<br/>Naga<br/>Elemental<br/>Angel<br/>Demon                                                                                                                                                                          | С какими расами будет работать ваш скин                                                                                                                                                                                   |

#### Пример json файла конфигурации

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