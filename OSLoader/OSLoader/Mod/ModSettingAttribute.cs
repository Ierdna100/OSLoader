using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class ModSettingAttribute : Attribute
    {
        internal string name;

        public ModSettingAttribute(string name)
        {
            this.name = name;
        }

        internal abstract bool IsOfValidType(Type type);
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SettingTitleAttribute : Attribute
    {
        internal string name;

        public SettingTitleAttribute(string name)
        {
            this.name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringSettingAttribute : ModSettingAttribute
    {
        internal string defaultValue;
        internal uint maxLength = uint.MaxValue;
        internal StringConstraints constraints;

        public StringSettingAttribute(string name, string defaultValue) : base(name)
        {
            this.defaultValue = defaultValue;
        }

        public StringSettingAttribute(string name, string defaultValue, StringConstraints constraints) : base(name)
        {
            this.defaultValue = defaultValue;
            this.constraints = constraints;
        }

        public StringSettingAttribute(string name, string defaultValue, uint maxLength) : base(name)
        {
            this.defaultValue = defaultValue;
            this.maxLength = maxLength;
        }

        public StringSettingAttribute(string name, string defaultValue, StringConstraints constraints, uint maxLength) : base(name)
        {
            this.defaultValue = defaultValue;
            this.constraints = constraints;
            this.maxLength = maxLength;
        }

        internal override bool IsOfValidType(Type type)
        {
            return type == typeof(string);
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class IntegerSettingAttribute : ModSettingAttribute
    {
        internal int defaultValue;
        internal int maxValue;
        internal int minValue;
        internal int step;

        public IntegerSettingAttribute(string name, int defaultValue, int minValue, int maxValue, int step) : base(name)
        {
            this.defaultValue = defaultValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
        }

        internal override bool IsOfValidType(Type type)
        {
            return type == typeof(int);
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FloatSettingAttribute : ModSettingAttribute
    {
        internal float defaultValue;
        internal float maxValue;
        internal float minValue;
        internal float step;

        public FloatSettingAttribute(string name, float defaultValue, float minValue, float maxValue, float step) : base(name)
        {
            this.defaultValue = defaultValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
        }

        internal override bool IsOfValidType(Type type)
        {
            return type == typeof(float);
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BoolSettingAttribute : ModSettingAttribute
    {
        internal bool defaultValue;

        public BoolSettingAttribute(string name, bool defaultValue) : base(name)
        {
            this.defaultValue = defaultValue;
        }

        internal override bool IsOfValidType(Type type)
        {
            return type == typeof(bool);
        }
    }
}
