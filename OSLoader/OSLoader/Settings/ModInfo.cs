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
        [JsonProperty] public ModType modType;
        [JsonProperty] public string name;
        [JsonProperty] public string description;
        [JsonProperty] public bool loadOnStart;
        [JsonProperty] public string repositoryUrl;
        [JsonProperty] public string modUrl;
        [JsonProperty] private string version;
        [JsonProperty] private string modId;
        [JsonProperty] private string author;
        [JsonProperty] private string dllFilepath;
        [JsonProperty] private string loaderVersion;

        [JsonIgnore]
        internal string infoFilepath;
        [JsonIgnore]
        internal string settingsFilepath;

        [JsonIgnore]
        public Version Version
        {
            get { return new Version(version); }
            set { version = value.ToString(); }
        }

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
