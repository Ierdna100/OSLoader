# ModSettings Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: ModSettings.cs  

## Declaration
public **ModSettings**()

## Description
The ModSettings class is an abstract class to be inherited by user-defined settings for each mod. It has special functionality which allows the attribute-based settings system to function. 

The settings will not be loaded if there is a mismatch between the expected type from an attribute attached to a setting and the actual type of the field, if there is no default value or if other setting attribute-specific issues occur.

The default value of the setting is the value set programmatically, like `= true` in the example below. Due to how `C#` works, this value can be ommitted for primitives (such as `bool`, `int` and `float`), and the default value will be instead what the `default` (`false`, `0` and `0f` respectively) keyword would have generated for that specific field.

## Usage
[!code-csharp[](../Code Examples/ModSettings.cs)]

## Properties
Property | Description
-- | -
`List<ModSettingDrawer> SettingDrawers { get; internal set; }` | A list of SettingDrawers, which store information about each individual setting, its attribute, the object that will be drawn on the UI and callbacks. This is useful if you wish to draw your own UI.

