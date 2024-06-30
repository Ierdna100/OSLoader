using System;
using OSLoader;

namespace TestMod
{
    public class TestMod : Mod
    {
        public override void OnModLoaded()
        {
            base.OnModLoaded();
            logger.Log("This mod was loaded!");
        }
    }
}
