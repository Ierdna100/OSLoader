using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    public static void OnBoolSettingChanged(bool newValue)
    {
        // This gets called if boolSetting was modified and the user saved their settings via the UI.
    }

    [Callback(typeof(CustomSettings), nameof(CustomSettings.OnBoolSettingChanged))]
    public bool boolSetting = true;
}
