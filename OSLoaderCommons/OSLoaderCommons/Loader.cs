using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace OSLoader
{
    internal class Loader
    {
        public static Loader Instance { get; private set; }

        public bool ModloaderInitialized { get; private set; } = false;

        public const string loaderFilepath = @"./OSLoader";
        public const string configFilepath = @"config";
        public const string loaderConfigFileFilepath = @"loader_config.json";
        public const string modsFilepath = @"mods";
        public const string modsSettingsFilename = @"settings.json";
        public const string modsInfoFilename = @"info.json";
        public const string assetBundleFilepath = @"loader";

        public Prefabs prefabs;

        public List<ModReference> mods = new List<ModReference>();

        public Logger logger = new Logger("OS Loader", true, true);

        public AssetBundle assetBundle;
        public LoaderConfig config;

        //public LoaderUI loaderUI;

        //public OSScene currentScene;
        
        public Loader()
        {
            Instance = this;

            Logger.Initialize();
            logger.Log("OS Loader initializing...");

            // When first called, this determines that the game has loaded its main assemblies
            SceneManager.sceneLoaded += OnSceneLoaded;
            logger.Log("OS Loader listening for game start...");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (!ModloaderInitialized) OnGameStart();
        }

        private void OnGameStart()
        {
            ModloaderInitialized = true;
            LoaderConfig.Load(loaderFilepath, configFilepath, loaderConfigFileFilepath, out config);

            // Asset Bundle
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(loaderFilepath, assetBundleFilepath));
            logger.Log("Asset bundle loaded!");
            if (logger.logDetails)
            {
                logger.Detail("Contents of asset bundle:");
                foreach (string assetName in assetBundle.GetAllAssetNames())
                {
                    logger.Detail("- " + assetName);
                }
            }

            // Figure out what mods exist
            string modsDir = Path.Combine(loaderFilepath, modsFilepath);
            if (Directory.Exists(modsDir))
            {
                string[] modsFilepaths = Directory.GetDirectories(Path.Combine(loaderFilepath, modsFilepath));
                foreach (string modFilepath in modsFilepaths)
                {
                    var mod = new ModReference(modFilepath);
                    if (mod.valid) mods.Add(mod);
                }
            }
            else
            {
                Directory.CreateDirectory(modsDir);
                logger.Log("No mods directory found, created one, skipping mod loading, given no mods could have been found");
            }

            // Create mod UI
            //GameObject loaderUIGO = new GameObject("Loader UI", typeof(LoaderUI));
            //GameObject.DontDestroyOnLoad(loaderUIGO);
            // loaderUI = loaderUIGO.GetComponent<LoaderUI>();

            // Mod loading
            logger.Log("Loading mods...");
            foreach (ModReference mod in mods)
            {
                if (mod.info.loadOnStart) mod.Load(true);
            }
            logger.Log("Finished loading mods!");

            logger.Log("OS Loader initialized!");
            Logger.DeleteDoorstopLog();
        }
    }
}

