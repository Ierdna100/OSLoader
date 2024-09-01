using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    public class ModSettingDrawer
    {
        public GameObject objectToDraw;
        public FieldInfo relatedField;
        public Attribute relatedAttribute;

        public ModSettingDrawer(GameObject objectToDraw, FieldInfo relatedField, Attribute relatedAttribute)
        {
            this.objectToDraw = objectToDraw;
            this.relatedField = relatedField;
            this.relatedAttribute = relatedAttribute;
        }
    }
}
