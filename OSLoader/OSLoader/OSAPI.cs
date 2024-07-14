using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine;

namespace OSLoader
{
    public static class OSAPI
    {
        public static Version GameVersion
        {
            get { return new Version(Application.version); }
            private set { }
        }

        public static OSScene CurrentScene { get; internal set; }

        public static bool IsModLoaded(string modName)
        {
            return false;
        }

        public static bool IsModValid(string modName)
        {
            return false;
        }

        public static string GetConfigFilepath(Mod mod)
        {
            return mod.info.settingsFilepath;
        }
    }
}
