using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    [IntegerSettingInputField("A integer setting!")]
    public int integerSetting0 = 12;

    // The following two are equivalent
    [IntegerSettingInputField("Another integer setting!", step: 2)]
    public int integerSetting1 = 14;

    [IntegerSettingInputField("Another another integer setting!", 2)]
    public int integerSetting2 = 14;

    // The following two are equivalent
    [IntegerSettingInputField("A fourth integer setting!", minValue: -100, maxValue: 100, step: 20)]
    public int integerSetting3 = 0;

    [IntegerSettingInputField("A fifth integer setting!", -100, 100, 20)]
    public int integerSetting4 = 0;

    // A clamped non-stepped value
    [IntegerSettingInputField("A sixth integer setting!", minValue: -20, maxValue: 0)]
    public int integerSetting5 = 0;

    // This is 0, as the `default` keyword for integers returns 0.
    [IntegerSettingInputField("Yet another integer setting!", step: 0.2f)]
    public int integerSetting6;
}

