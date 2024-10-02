# EnumSettingAttribute Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: EnumSettingAttribute.cs  

## Declaration
public **EnumSetting**(string name)

## Parameters
Parameter | Description
-- | -
`name` | Name of the setting that will appear in the UI.

## Description
An enum setting that the user can choose from. This will appear as a dropdown where the user can select each different value of the enumeration.

> [!NOTE]
> The callback for this attribute expects an integer, and not the type of your enumeration. It is up to you to cast the value of the integer to the enumeration.

## Usage
[!code-csharp[](../../Code Examples/ModSettings/EnumSettingAttribute.cs)]
