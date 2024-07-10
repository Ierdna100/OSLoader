using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using OSLoader;

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

        private void Start()
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

            if (mod.actualMod != null && mod.actualMod.HasValidSettings())
            {
                settingsButton.gameObject.SetActive(true);
                settingsButton.onClick.AddListener(OnSettingsToggle);
            }

            

            modName.text = mod.actualMod.info.name;
            version.text = mod.actualMod.info.Version.ToString();
        }

        public void OnAnyValueChanged()
        {

        }

        private void Update()
        {
            if (mod.loaded)
            {

            }
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
