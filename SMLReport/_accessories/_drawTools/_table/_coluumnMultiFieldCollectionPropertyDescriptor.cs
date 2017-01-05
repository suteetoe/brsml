using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SMLReport._design
{
    class _coluumnMultiFieldCollectionPropertyDescriptor : PropertyDescriptor
    {
        private _columnMultiFieldCollection _collection = null;
        private int _index = -1;

        public _coluumnMultiFieldCollectionPropertyDescriptor(_columnMultiFieldCollection __collection, int __index)
            : base("#" + __index.ToString(), null)
        {
            _collection = __collection;
            _index = __index;
        }

        #region Imprement PropertyDescriptor

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return _collection.GetType(); }
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

        #endregion
    }
}
