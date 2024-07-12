using System;
using System.Collections.Generic;
using System.Text;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_String : ModSettingUI_Base
    {
        public string localValue;
        public TMP_InputField input;

        public override void OnInitialized()
        {
            base.OnInitialized();
            input.onEndEdit.AddListener(OnEditEnd);
            input.onDeselect.AddListener(OnEditEnd);
            input.onValueChanged.AddListener(OnValueChanged);

            StringSettingAttribute _attribute = (StringSettingAttribute)attribute;

            input.characterLimit = _attribute.maxLength == uint.MaxValue ? 0 : (int)_attribute.maxLength;
            input.characterValidation = TMP_InputField.CharacterValidation.Regex;
        }

        private void OnEditEnd(string newValue)
        {
            StringSettingAttribute _attribute = (StringSettingAttribute)attribute;

            // If is empty and we don't allow empties
            if (newValue == string.Empty && (_attribute.constraints & StringConstraints.NoEmpty) != 0)
            {
                input.text = localValue;
            }

            // If we want to trim (if !NoTrim)
            if ((_attribute.constraints & StringConstraints.NoTrim) == 0)
            {
                localValue = newValue.Trim();
            }
            else
            {
                localValue = newValue;
            }
            OnSettingChanged();
        }

        private void OnValueChanged(string newValue)
        {
            string oldValue = localValue;
            StringSettingAttribute _attribute = (StringSettingAttribute)attribute;

            // NOTE: TMP_INPUT FIELD ALREADY MAY SUPPORT THIS, CHECK
            //if (_attribute.constraints & StringConstraints)
            OnSettingChanged();
        }

        private void OnEnable()
        {

        }

        public override void OnSave()
        {
            linkedField.SetValue(modEntryUI.mod.actualMod.settings, localValue);
        }
    }
}
