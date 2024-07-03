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

        public void SaveSettings()
        {
            if (settings?.settings == null) return;
            File.WriteAllText(info.settingsFilepath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }
}

