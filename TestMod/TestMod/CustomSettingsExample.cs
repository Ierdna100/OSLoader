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

        [StringSetting("A string setting", "")]
        public string myStringVariable;

        [BoolSetting("A bool setting", false)]
        public bool myBoolVariable;
    }
}
