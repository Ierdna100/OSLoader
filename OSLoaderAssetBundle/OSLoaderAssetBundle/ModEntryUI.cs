using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using OSLoader;
using System;
using System.Reflection;

namespace OSLoader
{
    internal class ModEntryUI : MonoBehaviour
    {
        internal ModReference mod;

        public TMP_Text modName;
        public TMP_Text version;
        public TMP_Text dependencies;

        public Button gitLink;
        public Button externalLink;
        public Button settingsButton;

        public List<ModSettingUI_Base> UISettings;

        public const int initialSpacingAtFirstSetting = 5;
        public const int spacingBetweenSettings = 2;

        public void OnInitialized()
        {
            if (mod == null)
            {
                Debug.LogError("Could not load mod UI: 'mod' is null!");
                return;
            }

            if (mod.info.repositoryUrl != null)
            {
                gitLink.gameObject.SetActive(true);
                gitLink.onClick.AddListener(OnGitPress);
            }

            if (mod.info.modUrl != null)
            {
                externalLink.gameObject.SetActive(true);
                externalLink.onClick.AddListener(OnExternalLinkPress);
            }

            if (mod.loaded)
            {
                GenerateSettings();
            }
            else
            {
                mod.generateUISettings = GenerateSettings;
            }

            modName.text = mod.actualMod.info.name;
            version.text = mod.actualMod.info.Version.ToString();
        }

        public void OnAnyValueChanged()
        {

        }

        private void GenerateSettings()
        {
            if (!mod.actualMod.HasValidSettings()) return;

            UISettings = new List<ModSettingUI_Base>();

            foreach (Tuple<FieldInfo, string> fieldData in mod.actualMod.settings.Settings)
            {
                if (fieldData.Item2 != null)
                {
                    GameObject settingUIGO = Instantiate(Loader.Instance.prefabs.settingHeader, transform);
                    ModSettingUI_Header headerUI = settingUIGO.GetComponent<ModSettingUI_Header>();
                    headerUI.title.text = fieldData.Item1.GetCustomAttribute<SettingTitleAttribute>().name;
                    UISettings.Add(headerUI);
                }

                ModSettingAttribute attribute = fieldData.Item1.GetCustomAttribute<ModSettingAttribute>();
                GameObject UIGO = null;
                
                switch (attribute)
                {
                    case IntegerSettingAttribute intAttribute:
                        if (intAttribute.isSliderType)
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.intSliderSetting, transform);
                        }
                        else
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.intSetting, transform);
                        }
                        break;
                    case StringSettingAttribute stringAttribute:
                        UIGO = Instantiate(Loader.Instance.prefabs.stringSetting, transform);
                        break;
                    case FloatSettingAttribute floatAttribute:
                        if (floatAttribute.isSliderType)
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.floatSliderSetting, transform);
                        }
                        else
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.floatSetting, transform);
                        }
                        break;
                    case BoolSettingAttribute boolAttribute:
                        UIGO = Instantiate(Loader.Instance.prefabs.boolSetting, transform);
                        break;
                }

                ModSettingUI_Base modSetting = UIGO.GetComponent<ModSettingUI_Base>();
                modSetting.attribute = attribute;
                modSetting.OnInitialized();
                UISettings.Add(modSetting);
            }

            SetPositionsOfSettings();

            settingsButton.gameObject.SetActive(true);
            settingsButton.onClick.AddListener(OnSettingsToggle);
        }

        private void SetPositionsOfSettings()
        {
            int y = initialSpacingAtFirstSetting;

            foreach (ModSettingUI_Base setting in UISettings)
            {
                ((RectTransform)setting.transform).localPosition = new Vector3(0, y, 0);
                y += (int)((RectTransform)setting.transform).sizeDelta.y;
                y += spacingBetweenSettings;
            }

            ((RectTransform)transform).sizeDelta = new Vector2(((RectTransform)transform).sizeDelta.x, y);
        }

        public void OnGitPress()
        {
            Application.OpenURL(mod.actualMod.info.repositoryUrl);
        }

        public void OnExternalLinkPress()
        {
            Application.OpenURL(mod.actualMod.info.modUrl);
        }

        public void OnSettingsToggle()
        {

        }
    }
}
