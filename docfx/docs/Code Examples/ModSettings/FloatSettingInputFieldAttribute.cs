using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    [FloatSettingInputField("A float setting!")]
    public float floatSetting0 = 12f;

    // The following two are equivalent
    [FloatSettingInputField("Another float setting!", step: 0.5f)]
    public float floatSetting1 = 14.5f;

    [FloatSettingInputField("Another another float setting!", 0.5f)]
    public float floatSetting2 = 14.0f;

    // The following two are equivalent
    [FloatSettingInputField("A fourth float setting!", minValue: -0.5f, maxValue: 0.5f, step: 0.1f)]
    public float floatSetting3 = 0f;

    [FloatSettingInputField("A fifth float setting!", -0.5f, 0.5f, 0.1f)]
    public float floatSetting4 = 0f;

    // A clamped non-stepped value
    [FloatSettingInputField("A sixth float setting!", minValue: -0.2f, maxValue: 12.0f)]
    public float floatSetting5 = 0f;

    // This is 0f, as the `default` keyword for floats returns 0f.
    [FloatSettingInputField("Yet another float setting!", step: 0.2f)]
    public float floatSetting6;
}
