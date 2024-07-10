using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;
using System.Reflection;
using System.Linq;

namespace OSLoader
{
    internal class LoaderUI : MonoBehaviour
    {
        private class ModUI
        {
            public ModReference mod;
            public bool showSettings = false;

            public ModUI(ModReference mod) => this.mod = mod;
        }

        public bool UIEnabled { get; private set; }  = false;

        private const int edgeWidth = 350;
        private const int modEntryHeight = 40;
        private const int titleSpacing = 65;
        private const int scrollBarDistance = 20;
        private const int settingEntryHeight = 25;

        private Rect mainMenuRect;
        private int scrollbarValue = 0;

        private GameObject mainMenu;

        private GUIContent redCircle;
        private GUIContent yellowCircle;
        private GUIContent greenCircle;
        private GUIContent grayCircle;
        private Texture link;
        private Texture git;
        private Texture cog;

        private ModUI[] modUIs;
        
        private void Awake()
        {
            redCircle = new GUIContent(Loader.Instance.assetBundle.LoadAsset<Texture>("Assets/Loader UI/Red Circle.png"), "Mod is invalid!");
            yellowCircle = new GUIContent(Loader.Instance.assetBundle.LoadAsset<Texture>("Assets/Loader UI/Yellow Circle.png"), "Missing dependencies");
            greenCircle = new GUIContent(Loader.Instance.assetBundle.LoadAsset<Texture>("Assets/Loader UI/Green Circle.png"), "Mod is loaded");
            grayCircle = new GUIContent(Loader.Instance.assetBundle.LoadAsset<Texture>("Assets/Loader UI/Gray Circle.png"), "Mod is not loaded");
            link = Loader.Instance.assetBundle.LoadAsset<Texture>("Assets/Loader UI/Link.png");
            git = Loader.Instance.assetBundle.LoadAsset<Texture>("Assets/Loader UI/Github.png");
            cog = Loader.Instance.assetBundle.LoadAsset<Texture>("Assets/Loader UI/Cog.png");

            int boxHeight = Screen.height - edgeWidth * 2;
            int boxWidth = Screen.width - edgeWidth * 2;

            mainMenuRect = new Rect(edgeWidth, edgeWidth, boxWidth, boxHeight);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                //if (mainMenu == null) mainMenu = FindObjectOfType<MainMenu>()?.gameObject;
                UIEnabled = !UIEnabled;
                if (mainMenu != null) mainMenu.SetActive(!UIEnabled);

                if (!UIEnabled)
                {
                    Loader.Instance.mods.ForEach(m => m.actualMod.SaveSettings());
                }
            }
        }

        private void OnGUI()
        {
            if (!UIEnabled)
            {
                if (modUIs != null) modUIs = null;
                return;
            }

            if (modUIs == null)
            {
                modUIs = new ModUI[Loader.Instance.mods.Count];
                for (int i = 0; i < Loader.Instance.mods.Count; i++) modUIs[i] = new ModUI(Loader.Instance.mods[i]);
            }
            //mainMenuRect = GUI.Window(0, mainMenuRect, MainMenuWindowCallback, "OS Loader Menu (F10 to toggle)");
        }

        /*private void MainMenuWindowCallback(int id)
        {
            int height = (int)mainMenuRect.height;
            int width = (int)mainMenuRect.width;

            float titleY = 25;
            float nameX = width * 0.05f;
            float btn1X = width * 0.5f - 75;
            float btn2X = width * 0.5f - 125;
            float btn3X = width * 0.5f - 175;
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

            int y = titleSpacing - scrollbarValue;
            for (int i = 0; i < modUIs.Length; i++)
            {
                ModUI modRef = modUIs[i];

                int centeredY = y + 10;
                int modHeight = modEntryHeight;
                if (modRef.showSettings)
                {
                    modHeight += modRef.mod.actualMod?.HasValidSettings() ? 0 : modRef.mod.actualMod.settings.totalEntriesCount * settingEntryHeight + 45;
                }

                GUI.Box(new Rect(10, y, width - 50, modHeight), "");
                GUI.Label(new Rect(nameX, centeredY, 100, 40), modRef.mod.info.name);
                if (modRef.mod.actualMod?.settings?.settings != null)
                {
                    if (GUI.Button(new Rect(btn1X, y, 40, 40), cog))
                    {
                        modRef.showSettings = !modRef.showSettings;
                    }
                }
                if (modRef.mod.info.repositoryUrl != null 
                    && modRef.mod.info.repositoryUrl.Trim() != ""
                    && (modRef.mod.info.repositoryUrl.Contains("https://github.com")
                    || modRef.mod.info.repositoryUrl.Contains("https://gitlab.com")))
                {
                    if (GUI.Button(new Rect(btn2X, y, 40, 40), git))
                        Application.OpenURL(modRef.mod.info.repositoryUrl.Trim());
                }
                if (modRef.mod.info.modUrl != null && modRef.mod.info.repositoryUrl.Trim() != "")
                {
                    if (GUI.Button(new Rect(btn3X, y, 40, 40), link))
                    {
                        // Application.OpenURL("https://OSModding.com/mods/...);
                    }
                }
                GUI.Label(new Rect(versionX, centeredY, 100, 40), modRef.mod.info.Version.ToString());
                GUI.Label(new Rect(dependenciesX, centeredY, 200, 40), "-");
                bool los = GUI.Toggle(new Rect(loadOnStartX + 20, centeredY, 40, 40), modRef.mod.info.loadOnStart, "");
                if (los != modRef.mod.info.loadOnStart)
                {
                    modRef.mod.info.loadOnStart = los;
                    modRef.mod.info.SaveInfo();
                }
                if(GUI.Toggle(new Rect(enableX, centeredY, 100, 40), modRef.mod.loaded, "") != modRef.mod.loaded) 
                    modRef.mod.Load();
                GUIContent status;
                if (!modRef.mod.valid) 
                    status = redCircle;
                else if (!modRef.mod.loaded) 
                    status = grayCircle;
                else 
                    status = greenCircle;
                GUI.Label(new Rect(statusX, centeredY, 20, 20), status);

                if (modRef.showSettings)
                {
                    GUI.Box(new Rect(20, y + modEntryHeight + 20, width - 100, modRef.mod.actualMod.settings.totalEntriesCount * settingEntryHeight + 10), "");
                    DrawSettings(width, y + modEntryHeight + 20, modRef.mod.actualMod.settings);
                    y += 40;
                }

                y += modHeight;
            }

            if (y > height - titleSpacing)
            {
                scrollbarValue = (int)GUI.VerticalScrollbar(
                    new Rect(width - 30, titleSpacing, 20, height - titleSpacing), 
                    scrollbarValue, (height - titleSpacing) / y, 0, y);
            }
            else
            {
                scrollbarValue = 0;
            }
            
            GUI.DragWindow(new Rect(0, 0, width, titleSpacing));
        }

        private void DrawSettings(int width, int initY, ModSettings settings)
        {
            int y = initY;
            foreach (Tuple<FieldInfo, string> setting in settings.settings)
            {
                if (setting.Item2 != null)
                {
                    int x = Math.Min((width - WidthOf(setting.Item2)) / 2, 450);
                    GUI.Label(new Rect(x, y, width, settingEntryHeight), setting.Item2);
                    y += settingEntryHeight;
                }

                ModSettingAttribute attribute = setting.Item1.GetCustomAttribute<ModSettingAttribute>();
                int offset = DrawTitleOfSetting(attribute.name, y);
                if (attribute is IntegerSettingAttribute intAttribute)
                    DrawIntegerSetting(offset, y, settings, setting, intAttribute);
                else if (attribute is FloatSettingAttribute floatAttribute) 
                    DrawFloatSetting(offset, y, settings, setting, floatAttribute);
                else if (attribute is BoolSettingAttribute)
                    DrawBoolSetting(offset, y, settings, setting);
                else if (attribute is StringSettingAttribute stringAttribute)
                    DrawStringSetting(offset, y, settings, setting, stringAttribute);

                y += settingEntryHeight;
            }
        }

        private int DrawTitleOfSetting(string name, int y)
        {
            int x = 65;
            int width = WidthOf(name);
            GUI.Label(new Rect(x, y + 5, width, settingEntryHeight), name);
            return width + x + 30;
        }

        private void DrawIntegerSetting(int offset, int y, ModSettings settings, Tuple<FieldInfo, string> setting, IntegerSettingAttribute intAttribute)
        {
            if (intAttribute.isSliderType)
            {
                int value = (int)GUI.HorizontalSlider(new Rect(offset, y + 10, 150, 20), (int)setting.Item1.GetValue(settings), intAttribute.minValue, intAttribute.maxValue);
                setting.Item1.SetValue(settings, value / intAttribute.step * intAttribute.step);
                GUI.Label(new Rect(offset + 160, y + 10, 50, 20), ((int)setting.Item1.GetValue(settings)).ToString());
            }
            else
            {
                string rawValue = GUI.TextField(new Rect(offset, y + 10, 30, 20), ((int)setting.Item1.GetValue(settings)).ToString());
                try
                {
                    int value = int.Parse(rawValue);
                    setting.Item1.SetValue(settings, value / intAttribute.step * intAttribute.step);
                }
                catch { }
            }
        }

        private void DrawFloatSetting(int offset, int y, ModSettings settings, Tuple<FieldInfo, string> setting, FloatSettingAttribute floatAttribute)
        {
            if (floatAttribute.isSliderType)
            {
                float value = GUI.HorizontalSlider(new Rect(offset, y + 10, 150, 20), (float)setting.Item1.GetValue(settings), floatAttribute.minValue, floatAttribute.maxValue);
                setting.Item1.SetValue(settings, Mathf.Round(value / floatAttribute.step) * floatAttribute.step);
                if (floatAttribute.customFormatter != null)
                {
                    try
                    {
                        GUI.Label(new Rect(offset + 160, y + 10, 50, 20), string.Format(floatAttribute.customFormatter, (float)setting.Item1.GetValue(settings)));
                    }
                    catch (FormatException exception)
                    {
                        Loader.Instance.logger.Error($"Custom formatter is not valid for field '{setting.Item1.Name}': " + exception);
                        floatAttribute.customFormatter = null;
                    }
                }
                else
                {
                    GUI.Label(new Rect(offset + 160, y + 10, 50, 20), ((float)setting.Item1.GetValue(settings)).ToString());
                }
            }
            else
            {
                string rawValue = GUI.TextField(new Rect(offset, y + 10, 30, 20), ((float)setting.Item1.GetValue(settings)).ToString());
                try
                {
                    float value = float.Parse(rawValue);
                    setting.Item1.SetValue(settings, Mathf.Round(value / floatAttribute.step) * floatAttribute.step);
                }
                catch { }
            }
        }

        private void DrawBoolSetting(int offset, int y, ModSettings settings, Tuple<FieldInfo, string> setting)
        {
            bool value = GUI.Toggle(new Rect(offset, y + 10, 20, 20), (bool)setting.Item1.GetValue(settings), "");
            setting.Item1.SetValue(settings, value);
        }

        private void DrawStringSetting(int offset, int y, ModSettings settings, Tuple<FieldInfo, string> setting, StringSettingAttribute stringAttribute)
        {
            string rawValue = GUI.TextField(new Rect(offset, y + 10, 300, 20), (string)setting.Item1.GetValue(settings));
            if ((stringAttribute.constraints & StringConstraints.NoAlphas) != 0)
                rawValue = new string(rawValue.Where(c => !((c > 'A' && c < 'Z') || (c > 'a' && c < 'z'))).ToArray());
            // if ((stringAttribute.constraints & StringConstraints.NoEmpty) != 0)
            //     rawValue
            if ((stringAttribute.constraints & StringConstraints.NoNumerics) != 0)
                rawValue = new string(rawValue.Where(c => !(c > '0' && c < '9')).ToArray());
            if ((stringAttribute.constraints & StringConstraints.NoSpaces) != 0)
                rawValue = new string(rawValue.Where(c => c != ' ').ToArray());
            if ((stringAttribute.constraints & StringConstraints.NoSpecials) != 0)
                rawValue = new string(rawValue.Where(c => (c > 'A' && c < 'Z') || (c > 'a' && c < 'z') || (c > '0' && c < '9') || (int)c == (int)' ').ToArray());
            if ((stringAttribute.constraints & StringConstraints.NoTrim) == 0)
                rawValue = rawValue.TrimStart();

            setting.Item1.SetValue(settings, rawValue);
        }

        private int WidthOf(string str)
        {
            return (int)GUI.skin.label.CalcSize(new GUIContent(str)).x;
        }*/
    }
}
