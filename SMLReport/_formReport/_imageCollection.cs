using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace SMLReport._formReport
{
    [Serializable]
    public class _imageCollection : CollectionBase, ICustomTypeDescriptor, ICloneable
    {
        public _imageObject this[int index]
        {
            get
            {
                if (this.List.Count > index)
                {
                    return (_imageObject)this.List[index];
                }
                return null;
            }
        }

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
                    _imageCollectionPropertyDescriptor __pd = new _imageCollectionPropertyDescriptor(this, __i);
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

        #region ICloneable Imprement

        public object Clone()
        {
            return this;
        }

        #endregion

        #region ICollection Imprement

        public void Add(_imageObject __pic)
        {
            this.List.Add(__pic);
        }

        #endregion
    }

}
