using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [CreateAssetMenu(fileName = "Prefabs")]
    internal class Prefabs : ScriptableObject
    {
        public GameObject canvas;
        public GameObject loaderMenu;
        public GameObject modEntry;

        public GameObject settingHeader;

        public GameObject stringSetting;
        public GameObject intSetting;
        public GameObject intSliderSetting;
        public GameObject floatSetting;
        public GameObject floatSliderSetting;
        public GameObject boolSetting;
    }
}
