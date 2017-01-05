using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing.Design;

namespace SMLReport._design
{
    public enum _serialNumberDisplayEnum
    {
        SingleLine,
        Columns
    }

    [Serializable]
    [TypeConverter(typeof(_tableColumnsConverter))]
    public class _tableColumns
    {
        #region _tableColumn Field

        private string _headerProperty = "";
        //private bool _autoColumnWidthProperty = true;
        private float _columnsWidth;
        private string _text;
        private Color _headerBackGround = Color.Transparent;

        private ContentAlignment _headerTextAlign = ContentAlignment.MiddleCenter;
        private ContentAlignment _textAlignment = ContentAlignment.MiddleLeft;
        private _barcodeType _typeBarcodeResult;

        private string _fieldFormat = "";
        private _FieldType _fieldType;
        private bool _showIsNumberZeroResult = true;
        private Padding _paddingResult = new Padding(4, 0, 4, 0);
        private _columnMultiFieldCollection _multiFieldCollectionResult = new _columnMultiFieldCollection();
        private string _replaceTextResult = "";
        private bool _printSerialNumberResult = false;
        private _serialNumberDisplayEnum _serialDisplayResult;
        private int _serialNumberColumnResult = 1;
        private bool _showSerialNewLineResult = true;

        [Browsable(false)]
        public string[] __defaultValueTmp;

        [Browsable(false)]
        public string[] _nameImageListResultTmp;

        [Browsable(false)]
        public SizeF _headerTextSize = new SizeF();

        #endregion

        #region _tableColumn Property

        [Category("Barcode Option")]
        [Description("ชนิดของ Barcode")]
        [DisplayName("Barcode Type")]
        public _barcodeType _typeBarcode
        {
            get
            {
                return _typeBarcodeResult;
            }
            set
            {
                _typeBarcodeResult = value;
            }
        }

        private Boolean _autoLineBreakResult = true;
        [Description("Data")]
        [DisplayName("แบ่งบรรทัดอัตโนมัติ")]
        [DefaultValue(true)]
        public Boolean _autoLineBreak
        {
            get
            {
                return _autoLineBreakResult;
            }
            set
            {
                _autoLineBreakResult = value;
            }
        }

        private _columnDisplayTypeEnum _columnGroupTypeResult = _columnDisplayTypeEnum.DetailColumn;
        [Category("Data")]
        [Description("ประเภทการรวมคอลัมน์")]
        [DisplayName("Group Type : ")]
        [DefaultValue(_columnDisplayTypeEnum.DetailColumn)]
        public _columnDisplayTypeEnum _columnGroupType
        {
            get
            {
                return this._columnGroupTypeResult;
            }

            set
            {
                this._columnGroupTypeResult = value;
            }
        }

        [Category("Serial Number")]
        [Description("ตัวเลือกการพิมพ์ Serial Number")]
        [DisplayName("S/N Print : ")]
        public bool _printSerialNumber
        {
            get { return _printSerialNumberResult; }
            set { _printSerialNumberResult = value; }
        }

        [Category("Serial Number")]
        [Description("กำหนดรูปแบบการพิมพ์ Serial Number")]
        [DisplayName("S/N Display Style : ")]
        public _serialNumberDisplayEnum _serialNumberDisplay
        {
            get { return _serialDisplayResult; }
            set { _serialDisplayResult = value; }
        }

        [Category("Serial Number")]
        [Description("จำนวน Column ในการแสดงผล")]
        [DisplayName("S/N Display Column : ")]
        public int _serialNumberColumn
        {
            get { return _serialNumberColumnResult; }
            set { _serialNumberColumnResult = value; }
        }

        [Category("Serial Number")]
        [Description("ตำแหน่งในการแสดงผล SerialNumber")]
        [DisplayName("S/N Show New Line : ")]
        public bool _showSerialNewLine
        {
            get { return _showSerialNewLineResult; }
            set { _showSerialNewLineResult = value; }
        }

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

        [Category("Appearance")]
        [Description("กำหนดฟิลด์ภายใน")]
        [DisplayName("MultiField : ")]
        [Editor(typeof(_columnMultiFieldEditor), typeof(UITypeEditor))]
        public _columnMultiFieldCollection _multiFieldCollection
        {
            get
            {
                return _multiFieldCollectionResult;
            }
            set
            {
                _multiFieldCollectionResult = value;
            }

        }

        [XmlAttribute]
        [Category("Appearance")]
        [Description("ข้อความส่วนหัวของคอลัมน์")]
        [DisplayName("HeaderText")]
        public string HeaderText
        {
            get
            {
                return _headerProperty;

            }

            set
            {
                _headerProperty = value;
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
        [Description("กำหนด การจัดวาง หัวตาราง")]
        [DisplayName("Header Alignment : ")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment HeaderAlignment
        {
            get
            {
                return _headerTextAlign;
            }

            set
            {
                _headerTextAlign = value;
            }
        }

        [XmlAttribute]
        [Category("Appearance")]
        [Description("กำหนดสีพื้นหลังของหัวตาราง")]
        [DisplayName("Header Background : ")]
        [Browsable(false)]
        public Color HeaderBackground
        {
            get
            {
                return _headerBackGround;
            }
            set
            {
                _headerBackGround = value;
            }
        }

        //[Category("Appearance")]
        //[Description("กำหนดความกว้างของคอลัมน์โดยอัตโนมัติ")]
        //[DisplayName("AutoColumnWidth")]
        //public bool AutoColumnWidth
        //{
        //    get
        //    {
        //        return _autoColumnWidthProperty;
        //    }

        //    set
        //    {
        //        _autoColumnWidthProperty = value;
        //    }
        //}

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
            if (this.HeaderText != "")
                return this.HeaderText;
            return "Empty Header Text";
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

        public void _setHeaderTableSize()
        {

        }

        private bool _showLotNumber;
        [Category("Lot Number")]
        [Description("เป็นคอลัมน์ที่แสดงหมายเลข Lot สินค้า")]
        [DisplayName("Show Lot Number :")]
        public bool showLotNumber
        {
            get
            {
                return _showLotNumber;
            }

            set
            {
                _showLotNumber = value;
            }
        }

        string _lotFieldName;
        [Category("Lot Number")]
        [Description("กำหนด Field Lot")]
        [DisplayName("Lot Fields : ")]
        [XmlAttribute]
        [TypeConverter(typeof(_tableColumnsFieldConverter))]
        public string lotFieldName
        {
            get
            {
                return _lotFieldName;
            }
            set
            {
                _lotFieldName = value;
            }
        }

        private _fieldOperation _lotGroupOperation;

        [Category("Lot Number")]
        [Description("การจัดกลุ่ม Lot")]
        [DisplayName("Lot Group Operation :")]
        public _fieldOperation lotGroupOperation
        {
            get
            {
                return _lotGroupOperation;
            }

            set
            {
                _lotGroupOperation = value;
            }
        }


        #endregion

    }
}
