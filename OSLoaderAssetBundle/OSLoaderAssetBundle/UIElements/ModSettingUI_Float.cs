using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_Float : ModSettingUI_Base
    {
        private bool isSliderType;

        public float localValue;

        public TMP_Text valueDisplay;
        public Slider slider;

        public override void OnInitialized()
        {
            base.OnInitialized();
            FloatSettingAttribute _attribute = (FloatSettingAttribute)attribute;
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
            float oldLocalValue = localValue;
            if(!float.TryParse(newValue, out localValue))
            {
                localValue = oldLocalValue;
            }
            else
            {
                UpdateValue(localValue);
                UpdateText();
            }
        }

        private void UpdateValue(float newValue)
        {
            float step = ((FloatSettingAttribute)attribute).step;
            localValue = Mathf.Floor(newValue / step * step);
            OnSettingChanged();
        }

        private void UpdateText()
        {
            FloatSettingAttribute _attribute = (FloatSettingAttribute)attribute;
            if (_attribute.customFormatter == null || _attribute.customFormatter == string.Empty)
            {
                valueDisplay.text = localValue.ToString();
                return;
            }

            try
            {
                valueDisplay.text = string.Format(_attribute.customFormatter, localValue);
            }
            catch (FormatException fe) 
            {
                Loader.Instance.logger.Error($"Could not parse custom formatter for string option at field '{linkedField.Name}': {fe}");
                    _attribute.customFormatter = null;
            }
        }

        private void OnEnable()
        {
            localValue = (float)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
            if (isSliderType) slider.value = localValue;
            else UpdateText();
        }

        public override void OnSave()
        {
            linkedField.SetValue(modEntryUI.mod.actualMod.settings, localValue);
        }
    }
}
