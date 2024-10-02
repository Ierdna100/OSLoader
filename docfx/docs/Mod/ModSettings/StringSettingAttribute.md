# StringSettingAttribute Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: StringSettingAttribute.cs  

## Declaration
public **StringSetting**(string name, StringConstraints stringConstraints = StringConstraints.None, uint maxLength = int.MaxValue)

## Parameters
Parameter | Description
-- | -
`name` | Name of the setting that will appear in the UI.
`stringConstraints` | Constraints as set by the `StringConstraints` enum. The user will have to follow the rules set by the enum value when entering a value.
`maxLength` | The maximum allowed length of the string when inputted by the user. Note that setting this value higher than `int.MaxValue` (such as `uint.MaxValue`) may cause an exception, the `uint` is there to ensure the value cannot be negative, as opposed to giving the entire range of `uint`'s possible values.

## Description
A string the user can input via a text field. By default, this text is trimmed of its beginning and end spaces, and allows all characters. You can further restrict or free the user inputs with `StringConstraints`.

Note that the enum is a [`Flags`](https://learn.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-8.0) attribute set to it, you can OR it together with various constraints, and a few predetermined useful constraints (such as `StringConstraints.OnlyAlphas`) already exist on the enum for the sake of simplicity.

## Usage
[!code-csharp[](../../Code Examples/ModSettings/StringSettingAttribute.cs)]
