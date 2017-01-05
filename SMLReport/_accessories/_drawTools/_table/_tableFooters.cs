using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SMLReport._design
{
    [Serializable]
    [TypeConverter(typeof(_tableFooterConverter))]
    public class _tableFooters
    {
        private float _columnsWidth;
        private string _text;
        private Color _backColor = Color.Transparent;
        private ContentAlignment _textAlignment = ContentAlignment.MiddleLeft;
        private string _fieldFormat = "";
        private _FieldType _fieldType;
        private bool _showIsNumberZeroResult = true;
        private Padding _paddingResult = new Padding(4, 0, 4, 0);
        private string _replaceTextResult = "";
        private _fieldOperation _operationResult = _fieldOperation.None;

        [Browsable(false)]
        public string[] __defaultValueTmp;

        [Category("Data")]
        [Description("ปรับเปลี่ยนการแสดงผลข้อมูล ใช้ @ แทน ค่าที่ได้จาก Field ตัวอย่าง (@) ผลลัพท์ (2,000.00)")]
        [DisplayName("ReplaceText")]
        public string _replaceText
        {
            get
            {
                return _replaceTextResult;
            }
            set
            {
                _replaceTextResult = value;
            }
        }

        [Category("_SML")]
        [Description("การจัดวาง")]
        [DisplayName("Padding : ระยะห่าง")]
        public Padding _padding
        {
            get
            {
                return _paddingResult;
            }

            set
            {
                _paddingResult = value;
            }
        }

        [XmlAttribute]
        [Category("Appearance")]
        [Description("กำหนดสีพื้นหลังของหัวตาราง")]
        [DisplayName("Header Background : ")]
        [Browsable(false)]
        public Color Background
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }

        [Category("Appearance")]
        [Description("กำหนดความกว้างของคอลัมน์")]
        [DisplayName("ColumnsWidth")]
        [DefaultValue(0)]
        [XmlAttribute]
        public float ColumnsWidth
        {
            get
            {
                return _columnsWidth;
            }
            set
            {
                _columnsWidth = value;
            }
        }

        [Category("Data")]
        [Description("กำหนดข้อความ หรือ ตัวแปลในการแสดงผล")]
        [DisplayName("Fields : ")]
        [XmlAttribute]
        [TypeConverter(typeof(_tableColumnsFieldConverter))]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        [Category("Appearance")]
        [Description("กำหนดการจัดวางข้อมุล")]
        [DisplayName("TextAlignment")]
        [XmlAttribute]
        public ContentAlignment TextAlignment
        {
            get
            {
                return _textAlignment;
            }
            set
            {
                _textAlignment = value;
            }
        }

        public override string ToString()
        {
            if (this._text != "")
                return this._text;
            return "Empty Text";
        }

        [Category("Data")]
        [Description("ฟิลด์ข้อมูล")]
        [DisplayName("Format : รูปแบบ")]
        [TypeConverter(typeof(string))]
        public string FieldFormat
        {
            get
            {
                return _fieldFormat;
            }
            set
            {
                _fieldFormat = value;
            }
        }

        [Category("Data")]
        [Description("ฟิลด์ข้อมูล")]
        [DisplayName("FieldType : ประเภท")]
        [TypeConverter(typeof(_FieldType))]
        public _FieldType FieldType
        {
            get
            {
                return _fieldType;
            }
            set
            {
                _fieldType = value;
            }
        }

        [Category("_SML")]
        [Description("ไม่แสดงกรณีมีค่าเป็นศูนย์")]
        [DisplayName("Show Zero Value :")]
        public bool _showIsNumberZero
        {
            get
            {
                return _showIsNumberZeroResult;
            }

            set
            {
                _showIsNumberZeroResult = value;
            }
        }

        [Category("Data")]
        [DisplayName("Operation")]
        [DefaultValue(_fieldOperation.None)]
        public _fieldOperation _operation
        {
            get
            {
                return _operationResult;
            }
            set
            {
                _operationResult = value;
            }
        }

    }
}
