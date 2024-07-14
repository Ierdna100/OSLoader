using System;
using OSLoader;

namespace TestMod
{
    public class TestMod : Mod
    {
        public override void OnModLoaded()
        {
            base.OnModLoaded();
            settings = new CustomSettingsExample();
            logger.Log("This mod was loaded! Version 3!");
        }
    }
}
