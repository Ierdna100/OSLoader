# KeybindSettingAttribute Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: KeybindSettingAttribute.cs  

## Declaration
public **KeybindSetting**(string name, KeybindContrainsts keybindConstraints = KeybindConstraints.Default, KeyCode[] dissalowedKeys = null)

## Parameters
Parameter | Description
-- | -
`name` | Name of the setting that will appear in the UI.
`keybindConstraints` | Constraints as set by the `KeybindConstraints` enum. The user will have to follow the rules set by the enum value when entering a value.
`dissalowedKeys` | Specifically dissalowed keys, set with an array of [`KeyCode`](https://docs.unity3d.com/ScriptReference/KeyCode.html) entries

## Description
A keybind setting the user can set to by clicking on a button and pressing any key on their keyboard. `Escape` is not allowed, as it has special functionality, and you can restrict further keys with the constructor parameters. The values in the `settings.json` will appear as the integer value of the enum. 

Note that the enum has the [`Flags`](https://learn.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-8.0) attribute set to it, you can OR it together with various constraints, and a few predetermined useful multi-flag constraints (such as `KeybindConstraints.Default`) already exist on the enum for the sake of simplicity.'

By default, `Escape` and default Steam binds (`Shift+Tab`) are not allowed.

## Usage
[!code-csharp[](../../Code Examples/ModSettings/KeybindSettingAttribute.cs)]
