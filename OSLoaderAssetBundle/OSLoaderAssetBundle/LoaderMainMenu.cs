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

        public Button closeButton;
        public Button saveButton;

        public GameObject modsContentWindow;

        public List<ModEntryUI> modUIs = new List<ModEntryUI>();

        public const int initialSpacingAtFirstModEntry = 15;
        public const int spacingBetweenModEntries = 10;

        private void Awake()
        {
            // use in the future
            // OSLoader.OSAPI.
            string loaderVersion = "0.0.0";
            title.text = $"OSLoader Menu (F10 to toggle) v{loaderVersion}";
            
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
        }

        public void OnSave()
        {

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
