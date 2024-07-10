﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

namespace OSLoader
{
    public abstract class ModSettings
    {
        // String indicates a title that should go above it
        public List<Tuple<FieldInfo, string>> Settings { get; internal set; } = new List<Tuple<FieldInfo, string>>();

        public ModSettings()
        {
            foreach (FieldInfo fieldInfo in GetType().GetFields())
            {
                FieldInfo settingField = null;
                string settingTitle = null;
                foreach (Attribute attribute in fieldInfo.GetCustomAttributes())
                {
                    if (attribute is SettingTitleAttribute title)
                    {
                        settingTitle = title.name;
                        continue;
                    }

                    if (!(attribute is ModSettingAttribute modSetting))
                    {
                        Loader.Instance.logger.Error($"Invalid attribute on mod settings at field '{fieldInfo.Name}'! Cannot generate settings.");
                        Settings = null;
                        return;
                    }

                    if (settingField != null)
                    {
                        Loader.Instance.logger.Error($"Too many attributes on mod settings at field '{fieldInfo.Name}'! Cannot generate settings.");
                        Settings = null;
                        return;
                    }

                    if (modSetting.GetExpectedType() != fieldInfo.FieldType)
                    {
                        Loader.Instance.logger.Error($"Type mismatch in settings attributes at field '{fieldInfo.Name}' ! (type {modSetting.GetExpectedType()} does not match expected type {fieldInfo.FieldType}) Cannot generate settings.");
                        Settings = null;
                        return;
                    }

                    if (fieldInfo.GetValue(this) == null)
                    {
                        Loader.Instance.logger.Error($"Default value in settings at field '{fieldInfo.Name}' is null! Cannot generate settings.");
                        Settings = null;
                        return;
                    }

                    settingField = fieldInfo;
                }

                if (settingField == null)
                {
                    Loader.Instance.logger.Error($"No setting attribute found at field '{fieldInfo.Name}'! Cannot generate settings.");
                    Settings = null;
                    return;
                }

                Settings.Add(new Tuple<FieldInfo, string>(settingField, settingTitle));
            }
        }
    }
}