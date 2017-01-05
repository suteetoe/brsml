using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace SMLReport._formReport
{
    [Serializable]
    public class _imageObject
    {
        private Image _imageResult;
        private string _keyResult;

        [Category("Image Object")]
        [Description("ชื่อสำหรับอ้างถึงภาพนี้")]
        [DisplayName("KeyName : ชื่อเรียก")]
        public string _keyName
        {
            get
            {
                return _keyResult;
            }
            set
            {
                _keyResult = value;
            }
        }


        [Category("Image Object")]
        [Description("รูปภาพ")]
        [DisplayName("Image : รูปภาพ")]
        public Image _image
        {
            get
            {
                return _imageResult;
            }
            set
            {
                _imageResult = value;
            }
        }
    }
}
