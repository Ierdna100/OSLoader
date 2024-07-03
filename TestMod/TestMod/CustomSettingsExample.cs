using System;
using System.Collections.Generic;
using System.Text;
using OSLoader;

namespace TestMod
{
    class CustomSettingsExample : ModSettings
    {
        [IntegerSetting("An integer setting", 0, -1, 1, 1)]
        public int MyIntVariable;

        [SettingTitle("A title that will appear above the float variable")]
        [FloatSetting("A float setting", 0f, -1f, 1f, 0.1f)]
        public float myFloatVariable;

        // Forgot to have a constructor without a max length, oops
        [StringSetting("A string setting", "", 200)]
        public string myStringVariable;

        [BoolSetting("A bool setting", false)]
        public bool myBoolVariable;
    }
}
