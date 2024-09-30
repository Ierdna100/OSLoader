using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using System.Reflection;

namespace OSLoader
{
    public abstract class Mod : MonoBehaviour
    {
        public ModInfo info;
        public ModSettings settings;

        public abstract void InitializeMod();

        public void SaveSettings()
        {
            if (!HasValidSettings()) return;
            File.WriteAllText(info.settingsFilepath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        public bool HasSettings()
        {
            return settings != null;
        }

        public bool HasValidSettings()
        {
            return settings?.SettingDrawers != null;
        }
    }
}

