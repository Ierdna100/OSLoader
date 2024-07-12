using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using OSLoaderCommons;

namespace OSLoader
{
    public class ModInfo
    {
        internal ModType modType;

        [JsonIgnore]
        internal string infoFilepath;
        [JsonIgnore]
        internal string settingsFilepath;

        public string name;
        public string description;
        public bool loadOnStart;

        [JsonProperty]
        private string version;

        [JsonIgnore]
        public Version Version
        {
            get { return new Version(version); }
            set { version = value.ToString(); }
        }

        public string repositoryUrl;
        public string modUrl;

        public bool IsValid()
        {
            Loader.Instance.logger.Detail($"Validation check for ModInfo at path {infoFilepath}: name: {name}, version: {version}");
            return name != null
                && version != null;
        }

        public void SaveInfo()
        {
            File.WriteAllText(infoFilepath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
