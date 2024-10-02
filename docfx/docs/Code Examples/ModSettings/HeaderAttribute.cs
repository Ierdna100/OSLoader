using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    [SettingsHeader("This header will appear above the boolean setting!")]
    [BoolSetting("A boolean setting!")]
    public bool boolSetting = true;
}
