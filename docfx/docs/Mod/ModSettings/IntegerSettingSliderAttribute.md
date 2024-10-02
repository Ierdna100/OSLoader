# IntegerSettingSliderAttribute Class

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: IntegerSettingSliderAttribute.cs  

## Declaration
public **IntegerSettingSlider**(string name, int minValue, int maxValue, int step = 1, bool smooth = true)

## Parameters
Parameter | Description
-- | -
`name` | Name of the setting that will appear in the UI.
`minValue` | Minimum value of the slider (left-most value)
`maxValue` | Maximum value of the slider (right-most value)
`step` | The step at which the slider should update
`smooth` | If false, the slider will also only move step-wise graphically, if true, it will appear smooth graphically, but still follow the `step` internally.

## Description
An integer setting the user can set by moving a slider. Feedback to the user is given via a text box with the value.

## Usage
[!code-csharp[](../../Code Examples/ModSettings/IntegerSettingSliderAttribute.cs)]
