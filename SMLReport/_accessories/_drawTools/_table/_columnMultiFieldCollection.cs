using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;

namespace SMLReport._design
{
    [Serializable, XmlInclude(typeof(_drawObject))]
    public class _columnMultiFieldCollection : CollectionBase, ICustomTypeDescriptor, ICloneable
    {
        public _drawObject this[int __index]
        {
            get
            {
                if (this.List.Count > __index)
                {
                    return (_drawObject)this.List[__index];
                }
                return null;
            }
        }

        #region ICollection Imprement

        public void Add(_drawObject __col)
        {
            this.List.Add(__col);
        }

        #endregion

        #region Imprement ICloneable

        public object Clone()
        {
            return this;
        }

        #endregion

        #region Imprement ICustomTypeDescription

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
                    _coluumnMultiFieldCollectionPropertyDescriptor __pd = new _coluumnMultiFieldCollectionPropertyDescriptor(this, __i);
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
