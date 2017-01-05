using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections;

namespace SMLReport._design
{
    [Serializable, XmlInclude(typeof(_tableFooters))]
    public class _tableFootersCollection : CollectionBase, ICustomTypeDescriptor, ICloneable
    {
        public _tableFooters this[int index]
        {
            get
            {
                if (this.List.Count > index)
                {
                    return (_tableFooters)this.List[index];
                }
                return null;
            }
        }

        public void Add(_tableFooters __col)
        {
            this.List.Add(__col);
        }


        #region Imprement ICloneable

        public object Clone()
        {
            return this;
        }

        #endregion

        #region Imprement ICustomTypeDescriptor

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection __pds = new PropertyDescriptorCollection(null);
            for (int __i = 0; __i < this.List.Count; __i++)
            {
                if (this != null)
                {
                    _tableFootersCollectionPropertyDescriptor __pd = new _tableFootersCollectionPropertyDescriptor(this, __i);
                    __pds.Add(__pd);
                }
            }
            return __pds;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }
}
