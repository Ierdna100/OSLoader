using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    internal class LoaderUI : MonoBehaviour
    {
        public bool UIEnabled { get; private set; }  = false;

        private const int edgeWidth = 350;
        private const int modEntryHeight = 40;
        private const int titleSpacing = 65;
        private const int scrollBarDistance = 20;

        private Rect mainMenuRect;
        private int scrollbarValue = 0;

        private GameObject mainMenu;
        
        private void Awake()
        {
            int boxHeight = Screen.height - edgeWidth * 2;
            int boxWidth = Screen.width - edgeWidth * 2;

            mainMenuRect = new Rect(edgeWidth, edgeWidth, boxWidth, boxHeight);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                if (mainMenu == null) mainMenu = FindObjectOfType<MainMenu>()?.gameObject;
                UIEnabled = !UIEnabled;
                if (mainMenu != null) mainMenu.SetActive(!UIEnabled);
            }
        }

        private void OnGUI()
        {
            if (!UIEnabled) return;

            mainMenuRect = GUI.Window(0, mainMenuRect, MainMenuWindowCallback, "OS Loader Menu (F10 to toggle)");
        }

        private void MainMenuWindowCallback(int id)
        {
            int height = (int)mainMenuRect.height;
            int width = (int)mainMenuRect.width;

            int modsCount = Loader.Instance.mods.Count;
            int maximumModsToDisplay = (height - titleSpacing) / modEntryHeight;

            if (modsCount > maximumModsToDisplay + 1)
            {
                scrollbarValue = (int)GUI.VerticalScrollbar(
                    new Rect(edgeWidth + (int)mainMenuRect.width - scrollBarDistance, edgeWidth + titleSpacing, 15, height - titleSpacing - 20),
                    scrollbarValue, maximumModsToDisplay, 0, modsCount);
            }
            else
            {
                scrollbarValue = 0;
            }

            float titleY = 25;
            float nameX = width * 0.05f;
            float versionX = width * 0.5f;
            float dependenciesX = width * 0.5f + 85;
            float loadOnStartX = width * 0.8f;
            float enableX = width * 0.9f;
            float statusX = width * 0.95f;
            GUI.Box(new Rect(5, titleY, width - 40, 30), "");
            GUI.Label(new Rect(nameX, titleY, 100, 40), "Name");
            GUI.Label(new Rect(versionX, titleY, 100, 40), "Version");
            GUI.Label(new Rect(dependenciesX, titleY, 100, 40), "Dependencies");
            GUI.Label(new Rect(loadOnStartX, titleY, 100, 40), "Load On Start");
            GUI.Label(new Rect(enableX, titleY, 100, 40), "Enable");
            GUI.Label(new Rect(statusX, titleY, 100, 40), "Status");

            for (int i = 0; i < Math.Min(modsCount, maximumModsToDisplay); i++)
            {
                ModReference mod = Loader.Instance.mods[i + scrollbarValue];

                int y = titleSpacing + i * modEntryHeight;
                int y2 = y + 7;
                GUI.Box(new Rect(5, y, width - 40, modEntryHeight - 5), "");
                GUI.Label(new Rect(nameX, y2, 100, 200), mod.info.name);
                GUI.Label(new Rect(versionX, y2, 100, 200), mod.info.version);
                GUI.Label(new Rect(dependenciesX, y2, 100, 600), "-");
                bool loadOnStart = GUI.Toggle(new Rect(loadOnStartX, y2, 100, 20), mod.info.loadOnStart, "");
                if (mod.info.loadOnStart != loadOnStart)
                {
                    mod.info.loadOnStart = loadOnStart;
                    mod.info.SaveInfo();
                }
                if (GUI.Toggle(new Rect(enableX, y2, 100, 20), mod.loaded, "") && !mod.loaded) mod.Load();
                if (!mod.valid) GUI.Label(new Rect(statusX, y2, 100, 40), "Invalid!");
            }

            GUI.DragWindow(new Rect(0, 0, width, titleSpacing));
        }
    }
}
