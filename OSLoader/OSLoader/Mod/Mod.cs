using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    public class Mod : MonoBehaviour
    {
        public ModConfig config;
        protected Logger logger;

        public virtual void OnModLoaded()
        {
            logger = new Logger(config.name);
        }
    }
}

