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
        private const int titleSpacing = 80;
        private const int scrollBarDistance = 20;

        private int scrollbarValue = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                UIEnabled = !UIEnabled;
            }
        }

        private void OnGUI()
        {
            if (!UIEnabled) return;

            int modsCount = Loader.Instance.mods.Count;
            int boxHeight = Screen.height - edgeWidth * 2;
            int boxWidth = Screen.width - edgeWidth * 2;

            int maximumModsToDisplay = (boxHeight - titleSpacing) / modEntryHeight;

            GUI.Box(new Rect(edgeWidth, edgeWidth, boxWidth, boxHeight), "OS Loader Menu (F10 to toggle)");
            if (modsCount * modEntryHeight + titleSpacing > boxHeight + 1)
            {
                scrollbarValue = (int)GUI.VerticalScrollbar(
                    new Rect(edgeWidth + boxWidth - scrollBarDistance, edgeWidth + titleSpacing, 15, boxHeight - titleSpacing - 20),
                    scrollbarValue, maximumModsToDisplay, 0, modsCount);
            }
            else
            {
                scrollbarValue = 0;
            }

            int titleX = edgeWidth + 10;
            int titleY = edgeWidth + titleSpacing - 40;

            float nameX = titleX + boxWidth * 0.05f;
            float versionX = titleX + boxWidth * 0.5f;
            float dependenciesX = titleX + boxWidth * 0.5f + 85;
            float enableX = titleX + boxWidth * 0.9f;
            float statusX = titleX + boxWidth * 0.95f;
            GUI.Box(new Rect(titleX, titleY - 5, boxWidth - 40, 30), "");
            GUI.Label(new Rect(nameX, titleY, 100, 40), "Name");
            GUI.Label(new Rect(versionX, titleY, 100, 40), "Version");
            GUI.Label(new Rect(dependenciesX, titleY, 100, 40), "Dependencies");
            GUI.Label(new Rect(enableX, titleY, 100, 40), "Enable");
            GUI.Label(new Rect(statusX, titleY, 100, 40), "Status");

            for (int i = 0; i < Math.Min(modsCount, maximumModsToDisplay); i++)
            {
                ModReference mod = Loader.Instance.mods[i + scrollbarValue];

                int y = edgeWidth + titleSpacing + i * modEntryHeight + 5;
                GUI.Box(new Rect(titleX, y, boxWidth - 40, modEntryHeight - 5), "");
                GUI.Label(new Rect(nameX, y, 100, 40), mod.config.name);
                GUI.Label(new Rect(versionX, y, 100, 40), mod.config.version);
                GUI.Label(new Rect(dependenciesX, y, 100, 40), "-");
                if (GUI.Toggle(new Rect(enableX, y, 100, 40), mod.loaded, "") && !mod.loaded) mod.Load();
                if (!mod.valid) GUI.Label(new Rect(statusX, y, 100, 40), "Invalid!");
            }
        }
    }
}
