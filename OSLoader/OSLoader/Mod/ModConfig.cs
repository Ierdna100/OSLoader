using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    public class ModConfig
    {
        public string name;
        public string description;
        public bool loadOnStart = false;
        public string version;

        public bool Check()
        {
            return name != null && version != null;
        }

        public void SaveConfigFile()
        {

        }
    }
}
