using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;
using TMPro;

namespace OSLoader
{
    internal abstract class ModSettingUI_Interactable<T, T2> : ModSettingUI_Base where T2 : ModSettingAttribute
    {
        protected bool wasModified = false;
        protected T localValue;

        protected List<Action> customCallbacks;
        protected List<Action<T>> callbacks;

        protected FieldInfo linkedField;
        protected T2 attribute;

        protected ModEntryUI modEntryUI;

        protected TMP_Text title;

        public bool initialized = false;

        protected void OnSettingChanged()
        {
            wasModified = true;
            LoaderMainMenu.Instance.OnAnyValueChanged();
        }

        public override void OnSave()
        {
            if (wasModified)
            {
                linkedField.SetValue(modEntryUI.mod.actualMod.settings, localValue);
                callbacks.ForEach(c => c.Invoke(localValue));
                customCallbacks.ForEach(c => c.Invoke());
            }
        }

        public override void OnInitialize(ModSettingDrawer relatedDrawer)
        {
            linkedField = relatedDrawer.relatedField;
            attribute = (T2)relatedDrawer.relatedAttribute;

            title.text = attribute.name;
            RegisterCallbacks();
            initialized = true;
        }

        private void OnEnable()
        {
            if (!initialized) return;
            OnceEnabled();
        }

        public void RegisterCallbacks()
        {
            foreach (Attribute possibleAttribute in linkedField.GetCustomAttributes(true))
            {
                if (possibleAttribute is CallbackAttribute callbackAttribute)
                {
                    MethodInfo method = callbackAttribute.type.GetMethod(callbackAttribute.method);
                    if (!method.IsStatic)
                    {
                        Loader.Instance.logger.Error($"Could not register callback for field '{linkedField.Name}' at {callbackAttribute.type.Name}.{callbackAttribute.method} as it is not static.");
                        continue;
                    }
                    if (method.GetParameters().Length != 1)
                    {
                        Loader.Instance.logger.Error($"Could not register callback for field '{linkedField.Name}' at {callbackAttribute.type.Name}.{callbackAttribute.method} as it contains more than 1 parameter.");
                        continue;
                    }
                    if (method.GetParameters()[0].ParameterType != attribute.GetExpectedType())
                    {
                        Loader.Instance.logger.Error($"Could not register callback for field '{linkedField.Name}' at {callbackAttribute.type.Name}.{callbackAttribute.method} as the parameter it takes mismatches expected type {attribute.GetExpectedType().Name}");
                        continue;
                    }
                    // TODO: Do methods still bind if the return type doesn't match? Check later

                    callbacks.Add((Action<T>)Delegate.CreateDelegate(callbackAttribute.type, method, true));
                }
            }
        }

        protected abstract void OnceEnabled();
    }
}
