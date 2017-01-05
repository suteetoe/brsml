using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SMLReport._design
{
    public class _tableColumnsCollectionPropertyDescriptor : PropertyDescriptor
    {
        private _tableColumnsCollection _collection = null;
        private int _index = -1;

        public _tableColumnsCollectionPropertyDescriptor(_tableColumnsCollection __ColumnCollection, int __idx)
            : base("#" + __idx.ToString(), null)
        {
            _collection = __ColumnCollection;
            _index = __idx;
        }

        public override string Name
        {
            get
            {
                return "#" + _index.ToString();
            }
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(null);
            }
        }

        public override string DisplayName
        {
            get
            {
                //_tableColumns __columns = this._collection[_index];
                //if (__columns == null)
                //{
                //    return "";
                //}
                //return __columns.HeaderText;
                return "[" + _index + "]";
            }
        }

        public override string Description
        {
            get
            {
                _tableColumns __columns = this._collection[_index];
                if (__columns == null)
                {
                    return "";
                }
                StringBuilder __desc = new StringBuilder();
                __desc.Append("Custom Description");
                return __desc.ToString();
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return _collection.GetType();
            }
        }

        public override object GetValue(object component)
        {
            return _collection[_index];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get
            {
                if (_collection[_index] == null)
                {
                    return base.GetType();
                }
                return _collection[_index].GetType();
            }
        }

        public override void ResetValue(object component)
        {
            //throw new NotImplementedException();
        }

        public override void SetValue(object component, object value)
        {
            //throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
