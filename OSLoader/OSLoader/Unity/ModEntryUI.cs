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
        public Toggle enableMod;

        public GameObject settingsContainer;
        public Image statusIcon;

        public List<ModSettingUI_Base> UISettings;

        public readonly Vector2 settingsClosedSizeDelta = new Vector2(1300, 75);
        public Vector2 settingsOpenSizeDelta;

        public const int initialSpacingAtFirstSetting = 30;
        public const int spacingBetweenSettings = 0;

        public void Initialize()
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
                statusIcon.sprite = Loader.Instance.modStates.loaded;
            }
            else
            {
                mod.generateUISettings = GenerateSettings;
                statusIcon.sprite = Loader.Instance.modStates.unloaded;
            }

            if (!mod.valid)
            {
                statusIcon.sprite = Loader.Instance.modStates.error;
            }

            modName.text = mod.info.name;
            version.text = mod.info.Version.ToString();
            enableMod.isOn = mod.loaded;
            enableMod.onValueChanged.AddListener(OnModEnable);
            settingsContainer.SetActive(false);
        }

        private void OnModEnable(bool newValue)
        {
            mod.Load();
            enableMod.isOn = mod.loaded;
            if (mod.loaded)
            {
                statusIcon.sprite = Loader.Instance.modStates.loaded;
            }
        }

        private void GenerateSettings()
        {
            if (!mod.actualMod.HasValidSettings()) return;

            UISettings = new List<ModSettingUI_Base>();

            foreach (ModSetting setting in mod.actualMod.settings.Settings)
            {
                if (false && setting.header != null)
                {
                    GameObject settingUIGO = Instantiate(Loader.Instance.prefabs.settingHeader, settingsContainer.transform);
                    ModSettingUI_Header headerUI = settingUIGO.GetComponent<ModSettingUI_Header>();
                    headerUI.title.text = setting.field.GetCustomAttribute<SettingTitleAttribute>().name;
                    UISettings.Add(headerUI);
                }

                ModSettingAttribute attribute = setting.field.GetCustomAttribute<ModSettingAttribute>();
                GameObject UIGO = null;
                
                switch (attribute)
                {
                    case IntegerSettingAttribute intAttribute:
                        if (intAttribute.isSliderType)
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.intSliderSetting, settingsContainer.transform);
                        }
                        else
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.intSetting, settingsContainer.transform);
                        }
                        break;
                    case StringSettingAttribute stringAttribute:
                        UIGO = Instantiate(Loader.Instance.prefabs.stringSetting, settingsContainer.transform);
                        break;
                    case FloatSettingAttribute floatAttribute:
                        if (floatAttribute.isSliderType)
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.floatSliderSetting, settingsContainer.transform);
                        }
                        else
                        {
                            UIGO = Instantiate(Loader.Instance.prefabs.floatSetting, settingsContainer.transform);
                        }
                        break;
                    case BoolSettingAttribute boolAttribute:
                        UIGO = Instantiate(Loader.Instance.prefabs.boolSetting, settingsContainer.transform);
                        break;
                }

                ModSettingUI_Base modSetting = UIGO.GetComponent<ModSettingUI_Base>();
                modSetting.attribute = attribute;
                modSetting.linkedField = setting.field;
                modSetting.onChangedCallbacks = setting.callbacks;
                modSetting.modEntryUI = this;
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
                // Reminder that the y axis points down in UI
                ((RectTransform)setting.transform).localPosition = new Vector3(0, -y, 0);
                y += (int)((RectTransform)setting.transform).sizeDelta.y;
                y += spacingBetweenSettings;
            }

            // Account for last setting's height
            y += (int)settingsClosedSizeDelta.y - initialSpacingAtFirstSetting;
            settingsOpenSizeDelta = new Vector2(((RectTransform)transform).sizeDelta.x, y);
        }

        public void OnGitPress()
        {
            Application.OpenURL(mod.info.repositoryUrl);
        }

        public void OnExternalLinkPress()
        {
            Application.OpenURL(mod.info.modUrl);
        }

        public void OnSettingsToggle()
        {
            settingsContainer.SetActive(!settingsContainer.activeSelf);
            if (settingsContainer.activeSelf)
            {
                ((RectTransform)transform).sizeDelta = settingsOpenSizeDelta;
            }
            else
            {
                ((RectTransform)transform).sizeDelta = settingsClosedSizeDelta;
            }
            LoaderMainMenu.Instance.UpdateModPositions();
        }
    }
}
