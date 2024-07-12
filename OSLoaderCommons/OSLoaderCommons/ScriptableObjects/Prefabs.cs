using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [CreateAssetMenu(fileName = "Prefabs")]
    internal class Prefabs : ScriptableObject
    {
        public GameObject loaderMenu;
        public GameObject modEntry;
        public GameObject settingHeader;
        public GameObject stringSetting;
        public GameObject intSetting;
        public GameObject floatSetting;
        public GameObject boolSetting;
        public GameObject intSliderSetting;
        public GameObject floatSliderSetting;
    }
}
