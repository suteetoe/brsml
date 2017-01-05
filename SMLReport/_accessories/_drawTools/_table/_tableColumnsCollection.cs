using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SMLReport._design
{
    [Serializable, XmlInclude(typeof(_tableColumns))]
    public class _tableColumnsCollection : CollectionBase, ICustomTypeDescriptor, ICloneable
    {
        public _tableColumns this[int index]
        {
            get
            {
                if (this.List.Count > index)
                {
                    return (_tableColumns)this.List[index];
                }
                return null;
            }
        }

        public ArrayList ToArrayList()
        {
            ArrayList __colList = new ArrayList();
            for (int __i = 0; __i < this.List.Count; __i++)
            {
                __colList.Add(this.List[__i]);
            }

            return __colList;
        }

        #region ICollection Imprement

        public void Add(_tableColumns __col)
        {
            this.List.Add(__col);
        }

        #endregion

        #region ICloneable Imprement

        public object Clone()
        {
            return this;
        }

        #endregion

        #region ICustomTypeDescription Imprement

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
                    _tableColumnsCollectionPropertyDescriptor __pd = new _tableColumnsCollectionPropertyDescriptor(this, __i);
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
