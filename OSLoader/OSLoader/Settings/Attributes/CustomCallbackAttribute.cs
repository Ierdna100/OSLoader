using System;
using System.Collections.Generic;
using UnityEngine;

namespace OSLoader
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class CustomCallbackAttribute : Attribute
    {
        public abstract void OnChanged();
    }
}
