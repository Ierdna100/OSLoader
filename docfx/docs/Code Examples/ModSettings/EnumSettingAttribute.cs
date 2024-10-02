using System;
using OSLoader;
using UnityEngine;

public class CustomSettings : ModSettings
{
    public enum AnEnum
    {
        OptionA,
        OptionB,
        OptionC
    }

    [EnumSetting("An enum setting!")]
    public AnEnum enumSetting = AnEnum.OptionB;
}
