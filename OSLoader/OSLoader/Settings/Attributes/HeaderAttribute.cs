using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class HeaderAttribute : Attribute
    {
        internal string name;

        public HeaderAttribute(string name)
        {
            this.name = name;
        }
    }
}
