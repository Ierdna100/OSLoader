using System;
using OSLoader;
using UnityEngine;

namespace YourNamespace
{
    public class YourMod : Mod
    {
        public override void InitializeMod()
        {
            settings = new CustomSettingsExample();
        }
    }

    public class CustomSettingsExample : ModSettings
    {
        [BoolSetting("A boolean setting!")]
        public bool boolSetting = true;
    }
}
