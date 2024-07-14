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

        public bool initialized = false;

        protected void OnSettingChanged()
        {
            LoaderMainMenu.Instance.OnAnyValueChanged();
            onChangedCallbacks.ForEach(callback => callback.Invoke());
        }

        public abstract void OnSave();

        public virtual void OnInitialized()
        {
            title.text = attribute.name;
            initialized = true;
        }

        private void OnEnable()
        {
            if (!initialized) return;
            OnceEnabled();
        }

        protected abstract void OnceEnabled();
    }
}
