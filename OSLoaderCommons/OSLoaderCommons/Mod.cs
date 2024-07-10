using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace OSLoader
{
    public class Mod : MonoBehaviour
    {
        public ModInfo info;
        public ModSettings settings;
        public Logger logger;

        public virtual void OnModLoaded()
        {
            logger = new Logger(info.name);
        }

        public virtual void OnSettingsChanged() { }

        public void SaveSettings()
        {
            if (!HasValidSettings()) return;
            File.WriteAllText(info.settingsFilepath, JsonConvert.SerializeObject(settings, Formatting.Indented));
            OnSettingsChanged();
        }

        public bool HasSettings()
        {
            return settings != null;
        }

        public bool HasValidSettings()
        {
            return settings?.Settings != null;
        }
    }
}

