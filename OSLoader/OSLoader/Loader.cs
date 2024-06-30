using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace OSLoader
{
    class Loader
    {
        public static Loader Instance { get; private set; }

        public bool ModloaderInitialized { get; private set; } = false;

        private const string loaderFilepath = @"./OSLoader";
        private const string configFilepath = @"config";
        private const string loaderConfigFileFilepath = @"loader_config.json";
        private const string modsFilepath = @"mods";

        internal List<ModReference> mods = new List<ModReference>();

        internal Logger logger = new Logger("OS Loader", true, true);

        internal LoaderConfig config;

        internal LoaderUI loaderUI;

        internal Loader()
        {
            Instance = this;

            Logger.Initialize();
            logger.Log("OS Loader initializing...");

            // When first called, this determines that the game has loaded its main assemblies
            SceneManager.sceneLoaded += OnSceneLoaded;
            logger.Log("OS Loader listening for game start...");
        }

        internal void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (!ModloaderInitialized) OnGameStart();
        }

        private void OnGameStart()
        {
            ModloaderInitialized = true;
            LoaderConfig.Load(loaderFilepath, configFilepath, loaderConfigFileFilepath, out config);

            // Figure out what mods exist
            string modsDir = Path.Combine(loaderFilepath, modsFilepath);
            if (Directory.Exists(modsDir))
            {
                string[] modsFilepaths = Directory.GetDirectories(Path.Combine(loaderFilepath, modsFilepath));
                foreach (string modFilepath in modsFilepaths)
                {
                    var mod = new ModReference(modFilepath);
                    mods.Add(mod);
                }
            }
            else
            {
                Directory.CreateDirectory(modsDir);
                logger.Log("No mods directory found, created one, skipping mod loading, given no mods could have been found");
            }

            // Create mod UI
            GameObject loaderUIGO = new GameObject("Loader UI", typeof(LoaderUI));
            GameObject.DontDestroyOnLoad(loaderUIGO);
            loaderUI = loaderUIGO.GetComponent<LoaderUI>();

            // Mod loading
            logger.Log("Loading mods...");
            foreach (ModReference mod in mods)
            {
                if (mod.config.loadOnStart) mod.Load();
            }
            logger.Log("Finished loading mods!");

            logger.Log("OS Loader initialized!");
            Logger.DeleteDoorstopLog();
        }
    }
}

