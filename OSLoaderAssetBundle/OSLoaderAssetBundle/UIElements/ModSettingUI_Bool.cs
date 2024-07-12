using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    internal class ModSettingUI_Bool : ModSettingUI_Base
    {
        bool localValue;

        private void OnEnable()
        {
            localValue = (bool)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
        }

        private void OnValueChanged(bool newValue)
        {
            localValue = newValue;
            OnSettingChanged();
        }

        protected override void OnSave()
        {
            linkedField.SetValue(modEntryUI.mod.actualMod.settings, localValue);
        }
    }
}
