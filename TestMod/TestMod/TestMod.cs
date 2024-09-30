using System;
using OSLoader;
using UnityEngine;

namespace TestMod
{
    public class TestMod : Mod
    {
        public static TestMod instance;

        public override void InitializeMod()
        {
            instance = this;
            settings = new CustomSettingsExample();
            Debug.Log("This amazing mod was loaded!");
        }
    }
}
