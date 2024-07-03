using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace OSLoader
{
    public class ModInfo
    {
        [JsonIgnore]
        internal string infoFilepath;
        [JsonIgnore]
        internal string settingsFilepath;

        public string name;
        public string description;
        public bool loadOnStart;
        public string version;

        public bool IsValid()
        {
            return name != null && version != null;
        }

        public void SaveInfo()
        {
            File.WriteAllText(infoFilepath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
