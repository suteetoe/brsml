using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SMLReport._formReport
{
    [Serializable] 
    public class _imageList
    {
        private _imageCollection _collectionResult = new _imageCollection();

        public _imageCollection _collection
        {
            get
            {
                return _collectionResult;
            }
            set
            {
                _collectionResult = value;
            }
        }

        public _imageList()
        {
            _collection = new _imageCollection();
        }

        public Image _getImageFromName(string __imageName)
        {
            for (int __i = 0; __i < _collection.Count; __i++)
            {
                if (_collection[__i]._keyName.Equals(__imageName.ToString()))
                {
                    return _collection[__i]._image;
                }
            }

            return null;
        }
    }
}
