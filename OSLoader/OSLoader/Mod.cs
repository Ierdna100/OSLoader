using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using System.Reflection;

namespace OSLoader
{
    public class Mod : MonoBehaviour
    {
        private const string initializerMethodName = "OnInitialize";

        public ModInfo info;
        internal ModSettings settings;

        internal void InitializeMod()
        {
            try
            {
                MethodInfo initMethod = GetType().GetMethod(initializerMethodName);
                if (initMethod == null)
                {
                    return;
                }

                if (initMethod.GetParameters().Length != 0) 
                {
                    Loader.Instance.logger.Error($"Mod '{info.name}' could not be loaded because its initializer method has parameters!");
                    return;
                }

                initMethod.Invoke(this, null);
            }
            catch (AmbiguousMatchException)
            {
                Loader.Instance.logger.Error($"Mod '{info.name}' could not be loaded because its main class contains more than one '{initializerMethodName}()' methods!");
            }
            catch (Exception e) 
            {
                Loader.Instance.logger.Error(e.ToString());
            }
        }

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

