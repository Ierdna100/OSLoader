using System;
using System.Collections.Generic;
using System.Text;
using OSLoader;
using UnityEngine;

namespace TestMod
{
    public static class CallbackExamples
    {
        public static void OnBoolSetting2(bool newValue)
        {

        }

        public static void OnBoolSetting3Changed(bool newValue)
        {

        }
    }

    public class CustomCallbackHandlerExample : CustomCallbackAttribute
    {
        public override void OnChanged()
        {
            throw new NotImplementedException();
        }
    }

    public class CustomSettingsExample : ModSettings
    {
        // All settings must come with a name and a value assigned to them, that is their default.
        // Due to how C# works, a lack of value on primitives will just generate whatever the `default` keyword
        // generates, or in the case of a string, lists, etc.; will error out when trying to load the mod.

        [BoolSetting("Bool 0")]
        public bool boolSetting0;

        [BoolSetting("Bool 1")]
        public bool boolSetting1;

        [Callback(typeof(CallbackExamples), nameof(CallbackExamples.OnBoolSetting2))]
        [BoolSetting("Bool 2")]
        public bool boolSetting2;

        [Callback(typeof(CallbackExamples))]
        [BoolSetting("Bool 3")]
        public bool boolSetting3;

        /* NOT IMPLEMENTED YET
        [CustomCallbackHandlerExample]
        [BoolSetting("Bool 4")]
        public bool boolSetting4;
        */

        [EnumSetting("Enum 0")]
        public StringConstraints constraintsSetting = StringConstraints.NoTrim;

        [FloatSettingSlider("Float 0", -1.0f, 1.0f, step: 0.1f, smooth: true)]
        public float floatSetting0;

        [FloatSettingInputField("Float 1", step: 0.2f)]
        public float floatSetting1;

        [FloatSettingInputField("Float 2", -1.0f, 1.0f, step: 0.2f)]
        public float floatSetting2;

        [OSLoader.Header("This will be a header")]

        [FloatSettingSlider("Int 0", -10, 10, step: 1, smooth: true)]
        public float intSetting0;

        [FloatSettingInputField("Int 1", step: 1)]
        public float intSetting1;

        [FloatSettingInputField("Int 2", -10, 10, step: 1)]
        public float intSetting2;

        // FORGOT TO IMPLEMENT KEYBIND CONSTRAINTS
        [KeybindSetting("Keybind 0", keybindConstraints: KeybindConstraints.NoEscape, dissalowedKeys: new KeyCode[] {KeyCode.PageUp, KeyCode.PageDown})]
        public KeyCode keybindSetting0 = KeyCode.G;

        // LIST ITEMS WILL BE IMPLEMENTED LATER

        [StringSetting("String 0", stringConstraints: StringConstraints.NoTrim, maxLength: 400)]
        public string stringSetting0 = "Default";
    }
}
