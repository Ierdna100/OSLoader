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
        public TMP_InputField input;

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
                slider.onValueChanged.AddListener(UpdateValue);
            }
            else
            {
                input.onDeselect.AddListener(OnValueEntered);
                input.onEndEdit.AddListener(OnValueEntered);
                input.onSubmit.AddListener(OnValueEntered);
            }
            OnceEnabled();
        }

        private void OnValueEntered(string newValue)
        {
            float oldLocalValue = localValue;
            if(!float.TryParse(newValue, out localValue))
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
            float step = ((FloatSettingAttribute)attribute).step;
            if (step == 0)
            {
                localValue = newValue;
            }
            else
            {
                localValue = Mathf.Floor(newValue / step * step);
            }

            if (isSliderType)
            {
                UpdateText();
            }
            OnSettingChanged();
        }

        private void UpdateText()
        {
            FloatSettingAttribute _attribute = (FloatSettingAttribute)attribute;
            if (string.IsNullOrEmpty(_attribute.customFormatter))
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

        protected override void OnceEnabled()
        {
            localValue = (float)linkedField.GetValue(modEntryUI.mod.actualMod.settings);
            if (isSliderType)
            {
                slider.value = localValue;
                UpdateText();
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
