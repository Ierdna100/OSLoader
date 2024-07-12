using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

namespace OSLoader
{
    public class ModSetting
    {
        public FieldInfo field;
        public string header;
        public List<Action> callbacks;

        public ModSetting(FieldInfo field, string header, List<Action> callbacks) 
        {
            this.field = field;
            this.header = header;
            this.callbacks = callbacks;
        }
    }
}
