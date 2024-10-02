# CallbackAttribute Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: BoolSettingAttribute.cs  

> [!IMPORTANT]
> This is subject to change, the attribute does not currently work.

## Declaration
public **Callback**(Type type, string method)

## Parameters
Parameter | Description
-- | -
`type` | Type of the class which the attribute should call.
`method` | Name of the method to call in the class.

## Description
A callback that gets called whenever the user modifies a setting and saves the settings. Useful for modifying variables only once the user has set them. 

## Usage
[!code-csharp[](../../Code Examples/ModSettings/CallbackAttribute.cs)]
