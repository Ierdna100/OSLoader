using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    // Declaration is OnChanged<T>(T newValue)
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class CallbackAttribute : Attribute
    {
        internal Type type;
        internal string method;

        public CallbackAttribute(Type type, string method = null)
        {
            this.type = type;
            this.method = method;
        }
    }
}
