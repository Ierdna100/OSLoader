# IntegerSettingInputFieldAttribute Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: IntegerSettingInputFieldAttribute.cs  

## Declaration
public **IntegerSettingInputField**(string name, int step = 0)
public **IntegerSettingInputField**(string name, int minValue, int maxValue, int step = 0)

## Parameters
Parameter | Description
-- | -
`name` | Name of the setting that will appear in the UI.
`step` | The step at which the user is allowed to increment the setting by. The value will always be parsed as an int from a string once the user enters it inside the input field. If the step is 0, the user can input any value they wish.
`minValue` | The minimum value allowed to be entered in the input field.
`maxValue` | The maximum value allowed to be entered in the input field.

## Description
An integer setting the user can set via an input field (a text box). 

## Usage
[!code-csharp[](../../Code Examples/ModSettings/IntegerSettingInputFieldAttribute.cs)]
