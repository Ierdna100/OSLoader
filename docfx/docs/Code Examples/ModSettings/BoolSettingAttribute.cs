using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    [BoolSetting("A boolean setting!")]
    public bool boolSetting0 = true;

    [BoolSetting("Another boolean setting!")]
    public bool boolSetting1 = false;

    // This is false, as the `default` keyword for booleans returns false.
    [BoolSetting("Yet another boolean setting!")]
    public bool boolSetting2;
}
