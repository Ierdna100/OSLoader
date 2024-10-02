# FloatSettingInputFieldAttribute Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: FloatSettingInputFieldAttribute.cs  

## Declaration
public **FloatSettingInputField**(string name, float step = 0.0f)  
public **FloatSettingInputField**(string name, float minValue, float maxValue, float step = 0.0f)

## Parameters
Parameter | Description
-- | -
`name` | Name of the setting that will appear in the UI.
`step` | The step at which the user is allowed to increment the setting by. The value will always be floored once the user enters it inside the input field. If the step is 0, the user can input any value they wish.
`minValue` | The minimum value allowed to be entered in the input field.
`maxValue` | The maximum value allowed to be entered in the input field.

## Description
A float setting the user can set via an input field (a text box). 

## Usage
[!code-csharp[](../../Code Examples/ModSettings/FloatSettingInputFieldAttribute.cs)]
