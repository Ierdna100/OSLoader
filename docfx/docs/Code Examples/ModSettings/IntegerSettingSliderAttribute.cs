using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    // The following two are equivalent
    [IntegerSettingSlider("A integer setting!", minValue: -1, maxValue: 1, step: 0, smooth: true)]
    public bool integerSetting0 = 1;

    [IntegerSettingSlider("Another integer setting!", -1, 1, 0, true)]
    public bool integerSetting1 = -15;

    // A stepped slider
    [IntegerSettingSlider("Another integer setting!", -1, 1, 2, false)]
    public bool integerSetting2 = -15;

    // This is 0, as the `default` keyword for integers returns 0.
    [IntegerSettingSlider("Yet another integer setting!")]
    public bool integerSetting3;
}
