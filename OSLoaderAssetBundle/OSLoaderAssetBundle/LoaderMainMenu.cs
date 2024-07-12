using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OSLoader;
using UnityEngine.UI;
using TMPro;

namespace OSLoader
{
    internal class LoaderMainMenu : MonoBehaviour
    {
        public GameObject loaderMenu;
        public GameObject noModsFoundText;
        public TMP_Text title;

        public TMP_Text confirmationText;
        public int confirmationTextLifetime = 200;
        public int confirmationTextLife = 0;

        public Button closeButton;
        public Button saveButton;

        public GameObject modsContentWindow;

        public List<ModEntryUI> modUIs = new List<ModEntryUI>();

        public const int initialSpacingAtFirstModEntry = 15;
        public const int spacingBetweenModEntries = 10;

        private void Awake()
        {
            title.text = $"OSLoader Menu (F10 to toggle) v{Loader.Instance.config.Version}";
            
            closeButton.onClick.AddListener(OnClose);
            saveButton.onClick.AddListener(OnSave);

            foreach (ModReference mod in Loader.Instance.mods)
            {
                var modUI = Instantiate(Loader.Instance.prefabs.modEntry, modsContentWindow.transform).GetComponent<ModEntryUI>();
                modUIs.Add(modUI);
                modUI.mod = mod;
                modUI.OnInitialized();
            }

            UpdateModPositions();
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                if (loaderMenu.activeSelf)
                    OnClose();
                else
                    OnOpen();
            }

            if (confirmationTextLife > 0)
            {
                confirmationTextLife--;
                if (!confirmationText.gameObject.activeSelf)
                {
                    confirmationText.gameObject.SetActive(true);
                }
            }
            else if (confirmationText.gameObject.activeSelf)
            {
                confirmationText.gameObject.SetActive(false);
            }
        }

        public void OnSave()
        {
            modUIs.ForEach(modUI => modUI.UISettings.ForEach(setting => setting.OnSave()));
            Loader.Instance.mods.ForEach(mod => mod.actualMod.SaveSettings());
            SetConfirmationText("Successfully saved settings!", Color.green);
        }

        public void SetConfirmationText(string text, Color color)
        {
            confirmationText.text = text;
            confirmationText.color = color;
            confirmationTextLife = confirmationTextLifetime;
        }

        public void OnClose()
        {
            loaderMenu.SetActive(false);
        }

        public void OnOpen()
        {
            loaderMenu.SetActive(true);
        }

        public void UpdateModPositions()
        {
            int y = initialSpacingAtFirstModEntry;

            foreach (ModEntryUI mod in modUIs)
            {
                ((RectTransform)mod.transform).localPosition = new Vector3(0, y, 0);
                y += (int)((RectTransform)mod.transform).sizeDelta.y;
                y += spacingBetweenModEntries;
            }

            ((RectTransform)modsContentWindow.transform).sizeDelta =
                new Vector2(((RectTransform)modsContentWindow.transform).sizeDelta.x, y);
        }
    }
}
