using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace OSLoader
{
    public abstract class ModSettings
    {
        [JsonIgnore]
        public List<ModSettingDrawer> SettingDrawers { get; internal set; } = new List<ModSettingDrawer>();

        public ModSettings()
        {
            foreach (FieldInfo fieldInfo in GetType().GetFields())
            {
                if (!ParseAttributesForField(fieldInfo))
                {
                    return;
                };
            }
        }

        private bool ParseAttributesForField(FieldInfo fieldInfo)
        {
            Attribute settingAttribute = null;
            GameObject objectToDraw = null;
            FieldInfo settingField = null;
            foreach (Attribute attribute in fieldInfo.GetCustomAttributes())
            {
                if (attribute is HeaderAttribute header)
                {
                    SettingDrawers.Add(new ModSettingDrawer(Loader.Instance.prefabs.header, fieldInfo, header));
                    continue;
                }

                if (!(attribute is ModSettingAttribute modSetting))
                {
                    Loader.Instance.logger.Error($"Invalid attribute of type '{attribute.GetType()}' on mod settings at field '{fieldInfo.Name}'! Cannot generate settings.");
                    SettingDrawers = null;
                    return false;
                }

                if (settingField != null)
                {
                    Loader.Instance.logger.Error($"Too many attributes on mod settings at field '{fieldInfo.Name}'! You can only specify one ModSettingAttribute per field. Cannot generate settings.");
                    SettingDrawers = null;
                    return false;
                }

                Type attributeType = modSetting.GetType();
                if (attributeType == typeof(ListSettingAttribute))
                {
                    // TODO: Special considerations
                }
                else if (attributeType == typeof(EnumSettingAttribute))
                {
                    // TODO: Special considerations
                }
                else if (modSetting.GetExpectedType() != fieldInfo.FieldType)
                {
                    Loader.Instance.logger.Error($"Type mismatch in settings attributes at field '{fieldInfo.Name}' ! (type {modSetting.GetExpectedType()} does not match expected type {fieldInfo.FieldType}) Cannot generate settings.");
                    SettingDrawers = null;
                    return false;
                }

                if (fieldInfo.GetValue(this) == null)
                {
                    Loader.Instance.logger.Error($"Default value in settings at field '{fieldInfo.Name}' is null! Cannot generate settings.");
                    SettingDrawers = null;
                    return false;
                }

                settingField = fieldInfo;
                objectToDraw = modSetting.GetObjectToDraw();
                settingAttribute = attribute;
            }

            if (settingField == null)
            {
                Loader.Instance.logger.Error($"No setting attribute found at field '{fieldInfo.Name}'! Cannot generate settings.");
                SettingDrawers = null;
                return false;
            }

            SettingDrawers.Add(new ModSettingDrawer(objectToDraw, settingField, settingAttribute));
            return true;
        }
    }
}
