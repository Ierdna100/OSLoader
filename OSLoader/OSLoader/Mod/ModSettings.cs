using System;
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
        internal List<Tuple<FieldInfo, string>> settings = new List<Tuple<FieldInfo, string>>();

        public ModSettings()
        {
            foreach (FieldInfo fieldInfo in GetType().GetFields())
            {
                ModSettingAttribute attribute = fieldInfo.GetCustomAttribute<ModSettingAttribute>();
                if (attribute == null)
                {
                    Loader.Instance.logger.Error("A setting in the mod settings lacks an attribute! Cannot generate settings.");
                    settings = null;
                    return;
                }

                if (attribute is StringSettingAttribute && fieldInfo.FieldType != typeof(string)
                    || attribute is BoolSettingAttribute && fieldInfo.FieldType != typeof(bool)
                    || attribute is FloatSettingAttribute && fieldInfo.FieldType != typeof(float)
                    || attribute is IntegerSettingAttribute && fieldInfo.FieldType != typeof(int))
                {
                    Loader.Instance.logger.Error("Type mismatch in settings attributes! Cannot generate settings menu.");
                    settings = null;
                    return;
                }

                SettingTitleAttribute title = fieldInfo.GetCustomAttribute<SettingTitleAttribute>();
                settings.Add(new Tuple<FieldInfo, string>(fieldInfo, title?.name));
            }

            GenerateDefaultSettings();
        }

        private void GenerateDefaultSettings()
        {
            if (settings == null) return;

            foreach (Tuple<FieldInfo, string> tuple in settings)
            {
                ModSettingAttribute attribute = tuple.Item1.GetCustomAttribute<ModSettingAttribute>();
                if (attribute is StringSettingAttribute stringAttribute)
                    tuple.Item1.SetValue(this, stringAttribute.defaultValue);
                if (attribute is BoolSettingAttribute boolAttribute)
                    tuple.Item1.SetValue(this, boolAttribute.defaultValue);
                if (attribute is IntegerSettingAttribute intAttribute)
                    tuple.Item1.SetValue(this, intAttribute.defaultValue);
                if (attribute is FloatSettingAttribute floatAttribute)
                    tuple.Item1.SetValue(this, floatAttribute.defaultValue);
            }
        }
    }
}
