using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    // The following two are equivalent
    [FloatSettingSlider("A float setting!", minValue: -1.0f, maxValue: 1.0f, step: 0.1f, smooth: true)]
    public float floatSetting0 = 1.0f;

    [FloatSettingSlider("Another float setting!", -1.0f, 1.0f, 0.1f, true)]
    public float floatSetting1 = 1.0f;

    // A stepped slider
    [FloatSettingSlider("Another float setting!", -1.0f, 1.0f, 0.1f, false)]
    public float floatSetting2 = 0f;

    // This is 0f, as the `default` keyword for floats returns 0f.
    [FloatSettingSlider("Yet another float setting!")]
    public float floatSetting3;
}
