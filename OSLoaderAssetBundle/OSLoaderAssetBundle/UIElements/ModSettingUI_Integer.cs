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
            }
        }

        private void OnValueEntered(string newValue)
        {
            int oldLocalValue = localValue;
            if (!int.TryParse(newValue, out localValue))
            {
                localValue = oldLocalValue;
            }
            else
            {
                UpdateValue(localValue);
                valueDisplay.text = localValue.ToString();
            }
        }

        private void UpdateValue(int newValue)
        {
            int step = ((IntegerSettingAttribute)attribute).step;
            localValue = newValue / step * step;
            OnSettingChanged();
        }

        private void OnEnable()
        {
            localValue = (int)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
            if (isSliderType) slider.value = localValue;
            else valueDisplay.text = localValue.ToString();
        }

        protected override void OnSave()
        {
            linkedField.SetValue(modEntryUI.mod.actualMod.settings, localValue);
        }
    }
}
