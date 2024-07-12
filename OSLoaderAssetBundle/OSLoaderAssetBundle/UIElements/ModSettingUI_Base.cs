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
        public List<Action> onChangedCallbacks;

        public ModEntryUI modEntryUI;

        public TMP_Text title;

        protected void OnSettingChanged()
        {
            modEntryUI.OnAnyValueChanged();
            onChangedCallbacks.ForEach(callback => callback.Invoke());
        }

        protected abstract void OnSave();

        public virtual void OnInitialized()
        {
            title.text = attribute.name;
        }
    }
}
