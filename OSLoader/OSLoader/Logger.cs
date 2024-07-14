using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

namespace OSLoader {
    public class Logger
    {
        private readonly string name;
        private readonly bool logToLoaderLog;

        private const string loaderFileFilepath = @"./loader.log";

        public bool logDetails = false;

        public Logger(string name, bool logToLoaderLog = false, bool logDetails = false)
        {
            this.name = name;
            this.logToLoaderLog = logToLoaderLog;
            this.logDetails = logDetails;
        }

        public static void Initialize()
        {
            // Wipe old file
            File.WriteAllText(loaderFileFilepath, "[OS Loader] Logger initialized\n");
        }

        public static void DeleteDoorstopLog()
        {
            string[] files = Directory.GetFiles("./", "*.log");
            foreach (string file in files)
            {
                if (file.ToLower().Contains("doorstop_"))
                {
                    Loader.Instance.logger.Log("Deleting doorstop log file with path " + file);
                    try
                    {
                        File.Delete(file);
                    }
                    catch // (Exception e)
                    {
                        // This actually comes in handy, we don't delete the last one (Shared File Exception)
                        // Loader.Instance.logger.Log("Could not delete Doorstop log file: " + e);
                    }
                }
            }
        }

        public void Log(bool boolean)
        {
            Log(boolean.ToString());
        }

        public void Log(int integer)
        {
            Log(integer.ToString());
        }

        public void Log(string message)
        {
            string log = $"[{name}] {message}";
            if (logToLoaderLog)
            {
                File.AppendAllText(loaderFileFilepath, log + "\n");
            }

            if (Loader.Instance.ModloaderInitialized)
            {
                Debug.Log(log);
            }
        }

        public void Detail(string message)
        {
            if (!logDetails) return;

            string log = $"[{name}] {message}";
            if (logToLoaderLog)
            {
                File.AppendAllText(loaderFileFilepath, log + "\n");
            }

            if (Loader.Instance.ModloaderInitialized)
            {
                Debug.Log(log);
            }
        }

        public void Error(string message)
        {
            if (!logDetails) return;

            string log = $"[{name}] {message}";
            if (logToLoaderLog)
            {
                File.AppendAllText(loaderFileFilepath, log + "\n");
            }

            if (Loader.Instance.ModloaderInitialized)
            {
                Debug.Log(log);
            }
        }
    }
}
