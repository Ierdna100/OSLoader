using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    internal class ModSettingUI_Bool : ModSettingUI_Base
    {
        private bool localValue;

        public override void OnInitialized()
        {
            base.OnInitialized();
            OnceEnabled();
        }

        protected override void OnceEnabled()
        {
            localValue = (bool)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
        }

        private void OnValueChanged(bool newValue)
        {
            localValue = newValue;
            OnSettingChanged();
        }

        public override void OnSave()
        {
            linkedField.SetValue(modEntryUI.mod.actualMod.settings, localValue);
        }
    }
}
