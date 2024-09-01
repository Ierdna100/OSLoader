using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    internal abstract class ModSettingUI_Base : MonoBehaviour
    {
        public ModEntryUI modEntryUI;

        public abstract void OnInitialize(ModSettingDrawer relatedDrawer);

        public abstract void OnSave();
    }
}
