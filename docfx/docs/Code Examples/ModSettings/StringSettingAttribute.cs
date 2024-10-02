using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    [StringSetting("A string settign!", stringConstraints: StringConstraints.NoTrim | StringConstraints.NoAlphas, maxLength: 400)]
    public string stringSetting = "DefaultValue";
}
