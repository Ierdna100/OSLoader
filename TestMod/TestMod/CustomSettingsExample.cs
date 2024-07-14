using System;
using System.Collections.Generic;
using System.Text;
using OSLoader;

namespace TestMod
{
    class CustomSettingsExample : ModSettings
    {
        // All settings must come with a name and a value assigned to them, that is their default.
        // Due to how C# works, a lack of value on primitives will just generate whatever the `default` keyword
        // generates, or in the case of a string, will error out when trying to load the mod.

        // The boolean setting only has a single consutrctor
        [BoolSetting("Boolean Setting Name")]
        public bool aBooleanSetting = false;

        // The simple ctor for int settings will show up as an input text box
        [IntegerSetting("Keyboard Input Int Setting")]
        public int anIntegerSetting = 0;

        // If provided with a min and max, as well as a step, a slider will instead be generated
        [IntegerSetting("Slider Input Int Setting", minValue: 12, maxValue: 20, step: 2)]
        public int anIntegerSettingThatGeneratesASlider = 14;

        // A step of 1 is assumed if none is provided.
        // Disregard slider here, it must always be true. I will implement non-slider min and max later.
        [IntegerSetting("Slider Input Int Setting", minValue: 12, maxValue: 20, slider: true)]
        public int anIntegerSettingThatGeneratesASlider2 = 14;

        // Same for floats, input text box
        [FloatSetting("Keyboard Input Float Setting")]
        public float aFloatSetting = 0f;

        // Same as for integers. Steps can be any arbitrary float.
        [FloatSetting("Slider Input Float Setting", minValue: 12, maxValue: 20, step: 0.2f)]
        public float aFloatSettingThatGeneratesASlider = 14f;

        // Same as for integers. Slider must always be true for the moment.
        [FloatSetting("Slider Input Float Setting", minValue: 12, maxValue: 20, slider: true)]
        public float aFloatSettingThatGeneratesASlider2 = 14f;

        // String settings are just always an input box.
        [StringSetting("String setting Name")]
        public string aStringSetting = "Default value";

        // ------------------------------------------------------------------
        //                       Extra functionality
        // ------------------------------------------------------------------

        // Headers to separate setting sections are also available. No UI is present for them
        // yet, but they will be registered nonetheless internally.
        // Works similar to the [Header] attribute for the Unity inspector.
        [SettingTitle("A header above this setting")]
        [BoolSetting("")]
        public bool titleExample = true;

        // You can use callbacks by creating your own attribute that inherits the one this does
        // See its implementation in TestMod.cs
        // Multiple callbacks are supported
        [CallbackSettingExample]
        [CallbackSettingExample2]
        [BoolSetting("")]
        public bool callbackExample = true;

        // ------------------------------------------------------------------
        //                       Currently unsupported
        // ------------------------------------------------------------------

        // This feature is not supported yet! Do not use it.
        // String constraints will be supported in the future. These will determine what the user is allowed to input.
        [StringSetting("", StringConstraints.NoSpaces | StringConstraints.NoAlphas)]
        public string exampleOfWhatsToCome = "";
    }
}
