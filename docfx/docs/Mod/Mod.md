# Mod Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: Mod.cs  
Inherits [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html)

## Description
The mod class is the class in a mod's assembly that acts as the entrypoint, as a sort of "Main". This class is to be inherited and cannot be constructed, as it is abstract. It has special functionality attached to it, as detailed below. The assembly containing your class inheriting `Mod` must be the one you reference in your `info.json`'s `dllFilepath` property. The very contents of the `info.json` can also be accessed via the `info` property, as detailed below.

The [`settings`]() member also has special functionality, as detailed in its page.

This class also inherits MonoBehaviour, and exists in `DontDestroyOnLoad` at all times, as long as the mod is loaded. If the mod is not loaded on start via its `info.json` property `loadOnStart`, your code will only execute when the user clicks on the `load` button in OSLoader's mod manager menu.

You are expected to implement the functionality of `InitializeMod()`, which will be called when your mod will be loaded. It is not recommended to run code in `Start` and `Awake`, as these technically get called first. Instead, you should run non-initialization code in a method named `OnModLoaded`, which will be called via reflection by the modloader.

> [!WARNING]
> There is currently a bug where the settings are loaded after `OnModLoaded` is called, this will be fixed in a future patch.

## Usage
[!code-csharp[](../Code Examples/Mod.cs)]

## Methods
Method | Description
-- | -
`void InitializeMod` | Method called upon mod initialization. Create your settings in this method.
`void SaveSettings` | Method called whenever you wish to save settings, if modified programmatically. Note that calling this in `InitializeMod` will set the settings to their default values.
`bool HasSettings` | Returns true if the mod has settings
`bool HasValidSettings` | Returns true if the mod has no invalid settings, such as a setting with a mismatched attribute attached to it

## Methods Referenced Via Reflection
Method | Description
-- | -
`void OnModLoaded` | Method called when mod initialization and settings initialization is done
`void OnSceneLoaded` | Currently unused
`void OnSceneInitializing` | Currently unused

## Properties
Property | Description
-- | -
`ModInfo info` | Mod's info as present in the `info.json` file
`ModSettings settings` | Mod settings as present in the `settings.json` file