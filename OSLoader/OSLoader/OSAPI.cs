using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine;

namespace OSLoader
{
    public static class OSAPI
    {
        public static Version GameVersion
        {
            get { return new Version(Application.version); }
            private set { }
        }

        /*public static OSScene CurrentScene
        {
            get { return Loader.Instance.currentScene; }
            private set { }
        }*/
    }
}

