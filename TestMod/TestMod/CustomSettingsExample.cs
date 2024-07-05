using System;
using System.Collections.Generic;
using System.Text;
using OSLoader;

namespace TestMod
{
    class CustomSettingsExample : ModSettings
    {
        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int myIntVariable;

        [SettingTitle("A title that will appear above the float variable")]
        [FloatSetting("A float setting", -1f, 1f, 0.1f)]
        public float myFloatVariable;

        [StringSetting("A string setting")]
        public string myStringVariable = "";

        [BoolSetting("A bool setting")]
        public bool myBoolVariable;

        [IntegerSetting("An integer setting", -100, 100, 10)]
        public int var1;

        [IntegerSetting("An integer setting")]
        public int var2;

        [FloatSetting("A float setting")]
        public float myFloatVariable2 = 100f;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var3;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var4;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var5;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var6;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var7;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var8;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var9;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var10;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var11;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var12;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var13;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var14;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var151;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var122;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var133;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var144;

        [IntegerSetting("An integer setting", -1, 1, 1)]
        public int var155;
    }

}
