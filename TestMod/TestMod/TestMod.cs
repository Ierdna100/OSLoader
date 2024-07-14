using System;
using OSLoader;
using UnityEngine;

namespace TestMod
{
    public class TestMod : Mod
    {
        public static TestMod instance;

        public override void OnModLoaded()
        {
            instance = this;
            base.OnModLoaded();
            settings = new CustomSettingsExample();
            logger.Log("This mod was loaded! Version 3!");
        }
    }

    // Attribute at the end of the name is not strictly required, but it is the convention
    // It gets removed anyway by the IDE.
    public class CallbackSettingExampleAttribute : OnChangedCallbackAttribute
    {
        public override void OnChanged()
        {
            Debug.Log("Callback executed successfully!");
        }
    }

    // Example of how to read the variable once this has been called
    public class CallbackSettingExample2Attribute : OnChangedCallbackAttribute
    {
        public override void OnChanged()
        {
            bool aNewValueThatWasJustSet = ((CustomSettingsExample)TestMod.instance.settings).callbackExample;
        }
    }
}
