using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OSLoader
{
    class ModReference
    {
        public string assemblyFilepath;
        public bool valid = false;

        public ModConfig config;
        public bool loaded = false;

        public Mod actualMod;

        public ModReference(string filepath)
        {
            var possibleAssembly = Directory.GetFiles(filepath, "*.dll");
            if (possibleAssembly.Length != 1)
            {
                Loader.Instance.logger.Log($"Unable to create mod reference at path {filepath}: No .dll found!");
                return;
            }

            assemblyFilepath = possibleAssembly[0];

            var configFilepath = Path.Combine(filepath, "config.json");
            if (!File.Exists(configFilepath)) {
                Loader.Instance.logger.Log($"Unable to create mod reference at path {filepath}: No config file found!");
                return;
            }

            string rawModConfig = File.ReadAllText(configFilepath);
            config = JsonConvert.DeserializeObject<ModConfig>(rawModConfig);
            if (!config.Check())
            {
                Loader.Instance.logger.Log($"Unable to create mod reference at path {filepath}: Config file check failed!");
                return;
            }

            valid = true;
        }

        public void Load()
        {
            if (!valid)
            {
                Loader.Instance.logger.Log($"Failed to load mod {config.name}: Valid flag is not set");
                return;
            }

            if (loaded)
            {
                Loader.Instance.logger.Log($"Not loading mod {config.name}: Mod is already loaded!");
                return;
            }

            Loader.Instance.logger.Log("Assembly filepath: ");
            Loader.Instance.logger.Log(assemblyFilepath);
            Assembly assembly = Assembly.LoadFrom(assemblyFilepath);
            string types = $"Types in assembly for mod {config.name}";
            foreach (Type type in assembly.GetTypes()) types += "\t" + type + "\n";
            Loader.Instance.logger.Detail(types);
            var entrypoint = from type in assembly.GetTypes()
                             where type.IsSubclassOf(typeof(Mod))
                             select type;

            if (entrypoint == null || entrypoint.Count() != 1)
            {
                Loader.Instance.logger.Log($"Failed to load mod {config.name}: Mod structure is invalid");
                return;
            }

            GameObject modGO = new GameObject(config.name, entrypoint.First());
            Object.DontDestroyOnLoad(modGO);
            actualMod = modGO.GetComponent<Mod>();
            actualMod.config = config;
            actualMod.OnModLoaded();

            Loader.Instance.logger.Log($"Loaded mod {config.name} ({Loader.Instance.mods.Where(e => e.loaded).Count()}/{Loader.Instance.mods.Count})");
            loaded = true;
        }
    }
}
