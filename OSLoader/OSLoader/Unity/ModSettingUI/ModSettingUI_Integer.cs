using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_Integer : ModSettingUI_Base
    {
        private bool isSliderType;

        public int localValue;

        public TMP_Text valueDisplay;
        public Slider slider;
        public TMP_InputField input;

        public override void OnInitialized()
        {
            base.OnInitialized();
            IntegerSettingAttribute _attribute = (IntegerSettingAttribute)attribute;
            isSliderType = _attribute.isSliderType;
            if (isSliderType)
            {
                slider.value = localValue;
                slider.maxValue = _attribute.maxValue;
                slider.minValue = _attribute.minValue;
                slider.onValueChanged.AddListener(UpdateValue);
            }
            else
            {
                input.onDeselect.AddListener(InputUpdateValue);
                input.onEndEdit.AddListener(InputUpdateValue);
                input.onSubmit.AddListener(InputUpdateValue);
            }
            OnceEnabled();
        }

        private void InputUpdateValue(string newValue)
        {
            int oldLocalValue = localValue;
            if (!int.TryParse(newValue, out localValue))
            {
                localValue = oldLocalValue;
                input.text = localValue.ToString();
            }
            else
            {
                UpdateValue(localValue);
            }
        }

        private void UpdateValue(float newValue)
        {
            int step = ((IntegerSettingAttribute)attribute).step;
            if (step == 0)
            {
                localValue = (int)newValue;
            }
            else
            {
                localValue = (int)newValue / step * step;
            }

            if (isSliderType)
            {
                valueDisplay.text = localValue.ToString();
            }
            OnSettingChanged();
        }

        protected override void OnceEnabled()
        {
            localValue = (int)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
            if (isSliderType)
            {
                slider.value = localValue;
                valueDisplay.text = localValue.ToString();
            }
            else
            {
                input.text = localValue.ToString();
            }
        }

        public override void OnSave()
        {
            linkedField.SetValue(modEntryUI.mod.actualMod.settings, localValue);
        }
    }
}
