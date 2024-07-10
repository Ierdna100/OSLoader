using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;
using TMPro;

namespace OSLoader
{
    internal abstract class ModSettingUI_Base : MonoBehaviour
    {
        public FieldInfo linkedField;
        public ModSettingAttribute attribute;

        public ModEntryUI modEntryUI;
        public ModReference modReference;

        public TMP_Text title;

        public void OnSettingChanged()
        {
            modEntryUI.OnAnyValueChanged();
        }

        public abstract void OnSave();
    }
}
