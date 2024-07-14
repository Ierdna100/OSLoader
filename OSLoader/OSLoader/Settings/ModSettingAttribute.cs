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

        internal abstract Type GetExpectedType();
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

    public abstract class OnChangedCallbackAttribute : Attribute
    {
        public abstract void OnChanged();
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringSettingAttribute : ModSettingAttribute
    {
        internal uint maxLength = uint.MaxValue;
        internal StringConstraints constraints;

        public StringSettingAttribute(string name) : base(name) { }

        public StringSettingAttribute(string name, StringConstraints constraints) : base(name)
        {
            this.constraints = constraints;
        }

        public StringSettingAttribute(string name, uint maxLength) : base(name)
        {
            this.maxLength = maxLength;
        }

        public StringSettingAttribute(string name, StringConstraints constraints, uint maxLength) : base(name)
        {
            this.constraints = constraints;
            this.maxLength = maxLength;
        }

        internal override Type GetExpectedType()
        {
            return typeof(string);
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class IntegerSettingAttribute : ModSettingAttribute
    {
        internal int maxValue;
        internal int minValue;
        internal int step;
        internal bool isSliderType;

        public IntegerSettingAttribute(string name, int minValue, int maxValue, int step = 1) : base(name)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
            isSliderType = true;
        }

        public IntegerSettingAttribute(string name, int minValue, int maxValue, bool slider = true) : base(name)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            isSliderType = slider;
        }

        public IntegerSettingAttribute(string name) : base(name)
        {
            isSliderType = false;
        }

        internal override Type GetExpectedType()
        {
            return typeof(int);
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FloatSettingAttribute : ModSettingAttribute
    {
        internal float maxValue;
        internal float minValue;
        internal string customFormatter;
        internal float step;
        internal bool isSliderType;

        public FloatSettingAttribute(string name, float minValue, float maxValue, float step = 1f) : base(name)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.step = step;
            isSliderType = true;
        }

        public FloatSettingAttribute(string name, float minValue, float maxValue, bool slider = true) : base(name)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            isSliderType = slider;
        }

        public FloatSettingAttribute(string name) : base(name)
        {
            isSliderType = false;
        }

        internal override Type GetExpectedType()
        {
            return typeof(float);
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BoolSettingAttribute : ModSettingAttribute
    {
        public BoolSettingAttribute(string name) : base(name) { }

        internal override Type GetExpectedType()
        {
            return typeof(bool);
        }
    }
}
