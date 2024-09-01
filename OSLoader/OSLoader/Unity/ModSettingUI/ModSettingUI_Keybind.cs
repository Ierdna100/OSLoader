using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OSLoader
{
    internal class ModSettingUI_Keybind : ModSettingUI_Interactable<KeyCode, KeybindSettingAttribute>
    {
        public Toggle toggle;
        public TMP_Text label;

        private bool awaitingInput;

        public override void OnInitialize(ModSettingDrawer settingDrawer)
        {
            base.OnInitialize(settingDrawer);
            toggle.onValueChanged.AddListener(OnValueChanged);
            OnceEnabled();
        }

        private void OnValueChanged(bool newValue)
        {
            awaitingInput = true;
            label.text = "...";
        }

        private void Update()
        {
            if (awaitingInput && Event.current.isKey)
            {
                localValue = Event.current.keyCode;
                toggle.isOn = true;
                label.text = localValue.ToString();
            }
        }

        protected override void OnceEnabled()
        {
            localValue = (KeyCode)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
            label.text = localValue.ToString();
        }
    }
}
