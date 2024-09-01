using System;
using System.Collections.Generic;
using System.Text;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_String : ModSettingUI_Interactable<string, StringSettingAttribute>
    {
        public TMP_InputField inputField;

        public override void OnInitialize(ModSettingDrawer settingsDrawer)
        {
            base.OnInitialize(settingsDrawer);
            inputField.onEndEdit.AddListener(OnEditEnd);
            inputField.onDeselect.AddListener(OnEditEnd);
            inputField.onSubmit.AddListener(OnEditEnd);

            inputField.characterLimit = attribute.maxLength;
            inputField.onValidateInput = OnValidateInput;
            OnceEnabled();
        }

        private char OnValidateInput(string text, int charIndex, char addedChar)
        {
            if (attribute.constraints == StringConstraints.None)
            {
                return addedChar;
            }

            if (IsFlagSet(attribute.constraints, StringConstraints.NoSpaces) && addedChar == ' ')
            {
                return '\0';
            }

            if (IsFlagSet(attribute.constraints, StringConstraints.NoAlphas) && char.IsLetter(addedChar))
            {
                return '\0';
            }

            if (IsFlagSet(attribute.constraints, StringConstraints.NoNumerics) && char.IsDigit(addedChar))
            {
                return '\0';
            }

            if (IsFlagSet(attribute.constraints, StringConstraints.NoSpecials) && !char.IsLetter(addedChar) && !char.IsDigit(addedChar))
            {
                return '\0';
            }

            return addedChar;
        }

        private void OnEditEnd(string newValue)
        {
            if (!IsFlagSet(attribute.constraints, StringConstraints.NoTrim))
            {
                newValue = newValue.Trim();
            }

            if (newValue == string.Empty && IsFlagSet(attribute.constraints, StringConstraints.NoEmpty))
            {
                inputField.text = localValue;
                return;
            }

            localValue = newValue;

            OnSettingChanged();
        }

        protected override void OnceEnabled()
        {
            localValue = (string)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
        }

        private bool IsFlagSet(StringConstraints constraints, StringConstraints flag)
        {
            return (constraints & flag) != 0;
        }
    }
}
