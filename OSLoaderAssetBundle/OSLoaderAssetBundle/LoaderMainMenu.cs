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

        private void Awake()
        {
            // use in the future
            // OSLoader.OSAPI.
            string loaderVersion = "0.0.0";
            title.text = $"OSLoader Menu (F10 to toggle) v{loaderVersion}";

            closeButton.onClick.AddListener(OnClose);
            saveButton.onClick.AddListener(OnSave);
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
    }
}
