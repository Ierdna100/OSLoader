using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        public void OnDoneSaving()
        {
            if (mod.actualMod.HasValidSettings())
            {
                mod.actualMod.SaveSettings();
            }
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

            foreach (ModSettingDrawer settingDrawer in mod.actualMod.settings.SettingDrawers)
            {
                var attribute = settingDrawer.relatedAttribute as ModSettingAttribute;
                GameObject UIGO = Instantiate(settingDrawer.objectToDraw, settingsContainer.transform);

                ModSettingUI_Base modSettingUI = UIGO.GetComponent<ModSettingUI_Base>();
                modSettingUI.modEntryUI = this;
                modSettingUI.OnInitialize(settingDrawer);
                UISettings.Add(modSettingUI);
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
