using System;
using OSLoader;
using UnityEngine;

namespace YourNamespace
{
    public class YourMod : Mod
    {
        public static YourMod instance;

        public override void InitializeMod()
        {
            instance = this;
            settings = new CustomSettingsExample();
        }

        public void OnModLoaded()
        {
            Debug.Log("This great mod was loaded!");
            
            settings.yourSetting = false;
            SaveSettings()

            Debug.Log($"The author of this mod is {info.author}")
        }
    }
}
