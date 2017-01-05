using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SMLReport._formReport
{
    [Serializable]
    public class SMLFormDesignXml
    {
        [XmlArrayItem("PageList", typeof(_xmlPageClass))]
        public ArrayList Page = new ArrayList();

        public string _guid;
        public string _formCode;
        public string _formName;
        public string _lastUpdate;
        public FormQuerys _query;
        public xmlImageList _imageList;
    }

    [Serializable]
    public class xmlImageList
    {
        [XmlArrayItem("_ImageObject", typeof(xmlImageObject))]
        public ArrayList ImagesObject = new ArrayList();
    }

    public class xmlImageObject
    {
        public string _imageSource;
        public string _imageName;
    }

    [Serializable]
    public class FormQuerys
    {
        [XmlArrayItem("Query", typeof(query))]
        public ArrayList QueryLists = new ArrayList();
    }

    [Serializable]
    public class query
    {
        public string _queryString;
        public string _talbeResource;

        [XmlArrayItem("FidldList", typeof(queryField))]
        public ArrayList _fieldList = new ArrayList();

        [XmlArrayItem("ConditionList", typeof(fieldCondition))]
        public ArrayList _coditionList = new ArrayList();
    }

    public class queryField
    {
        public string FieldName;
        public string Resource;
    }

    public class fieldCondition
    {
        public string FieldName;
        public string value;
    }

    [Serializable]
    public class BackgroundPageXML
    {
        [XmlArrayItem("PageBackground", typeof(_xmlBackground))]
        public ArrayList PageBackground = new ArrayList();
    }

    [Serializable]
    public class _xmlBackground
    {
        public string _backgroundPage;
        public float _backgroundTopMargin;
        public float _backgroundLeftMargin;
        public Boolean _backgroundShow;
    }

    [Serializable]
    public class _xmlPageClass
    {
        public _xmlPageSetupClass PageSetup = new _xmlPageSetupClass();
        [XmlArrayItem("DrawObjectList", typeof(_xmlDrawObjectClass))]
        public ArrayList DrawObject = new ArrayList();
        public string _backgroundPage;
        public float _backgroundTopMargin;
        public float _backgroundLeftMargin;
        public Boolean _backgroundShow;
    }

    [Serializable]
    public class _formClipBoardObject
    {
        [XmlArrayItem("DrawObjectList", typeof(_xmlDrawObjectClass))]
        public ArrayList DrawObject = new ArrayList();
    }

    public class _xmlPageSetupClass
    {
        public float PaperWidth;
        public float PaperHeight;
        public SMLReport._design._orientationType Orientation;
        public SMLReport._design._pageSizeType PaperSize;
        public SMLReport._design._measurementUnitType MeasurementUnit;
        public float MarginTop;
        public float MarginBottom;
        public float MarginLeft;
        public float MarginRight;
        public bool _autoPrinterPageSize;
    }

    [Serializable]
    public class _xmlDrawObjectClass
    {
        public Padding Padding;
        public float CharSpace;
        public float CharWidth;
        public Boolean AllowLineBreak = true;
        public Boolean AutoLineSpace = true;
        public Boolean ShowIsZeroValue = true;
        public Boolean MultiLine = true;
        public Boolean Lock = false;

        public float LineSpace = 0;
        public string FieldFormat;
        public SMLReport._design._FieldType FieldType;
        public SMLReport._design._drawToolType ToolType;
        public SMLReport._design._textAlign TextAlign;
        /// <summary>ตำแหน่งของการวางข้อความ</summary>
        public ContentAlignment ContentAlign = ContentAlignment.MiddleLeft;
        public SMLReport._design._fieldOperation Operation;
        /// <summary>สีตัวอักษร</summary>
        public string Color;
        /// <summary>สีพื้นหลัง</summary>
        public string BackgroundColor;
        /// <summary>ความหนาของเส้น</summary>
        public int PenWidth;
        /// <summary>ความยาว</summary>
        public int Width;
        /// <summary>ความสูง</summary>
        public int Height;
        /// <summary>drawLine = ตำแหน่งเริ่มต้น ด้าน X</summary>
        public int StartPointX;
        /// <summary>drawLine = ตำแหน่งเริ่มต้น ด้าน Y</summary>
        public int StartPointY;
        /// <summary>ตำแหน่งสิ้นสุด ด้าน X</summary>
        public int StopPointX;
        /// <summary>ตำแหน่งสิ้นสุด ด้าน Y</summary>
        public int StopPointY;
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;
        /// <summary>ตำแหน่ง X ในการเริ่มต้น</summary>
        public int X;
        /// <summary>ตำแหน่ง Y ในการเริ่มต้น</summary>
        public int Y;
        /// <summary>ข้อความ</summary>
        public string Text;
        public string _specialText;
        public string _asField;

        /// <summary>ชื่อ Font 1</summary>
        public string FontName;
        /// <summary>ขนาดตัวอักษร 1</summary>
        public float FontSize;
        /// <summary>ลักษณะตัวอักษร 1</summary>
        public FontStyle FontStyle;

        public SMLReport._design._drawImage.ImageBorderStyleType BorderStyle;
        public System.Windows.Forms.PictureBoxSizeMode PictureBoxSizeMode;
        public System.Drawing.RotateFlipType RotateFlip;
        public string Opacity;
        /// <summary>องศา</summary>
        public float Angle;
        /// <summary>สีเส้น</summary>
        public string LineColor;
        /// <summary>รูปภาพ 1</summary>
        public string Image;
        /// <summary>ลักษณะเส้น</summary>
        public System.Drawing.Drawing2D.DashStyle LineStyle;

        // spacial Property for RoundedRectangle
        /// <summary>ระยะเส้นของโค้ง</summary>
        public SMLReport._design.RoundedRectangleRadius RoundedRectangleRadius;

        // special Property for Table
        public string HeaderFontName;
        public float HeaderFontSize;
        public FontStyle HeaderFontStyle;

        [XmlArrayItem("Column", typeof(_XmlTableColumns))]
        public ArrayList TableColumns = new ArrayList();

        [XmlArrayItem("Column", typeof(_XmlTableFooter))]
        public ArrayList TableFooters = new ArrayList();

        /// <summary>Table = ShowHeader, POS Label = แสดง Shadow</summary>
        public Boolean showHeaderTable;
        public Boolean showFooterTable;
        public float FooterHeight;
        public string ColumnsSeparatorLineColor;
        public System.Drawing.Drawing2D.DashStyle ColumnsSeparatorLine;
        public string HeaderBackground;
        public string HeaderForeColor;
        public string HeaderLineColor;
        public System.Drawing.Drawing2D.DashStyle HeaderLineStyle;
        public string RowsLineColor;
        public System.Drawing.Drawing2D.DashStyle RowsLineStyle;
        public int RowPerPage;
        public Boolean _multiField;

        public _queryRule SerialQuery;
        public string _serialField;
        public string _dataPriLink;
        public string _dataSecLink;
        public bool _pageOverflowNewLine = true;
        public Boolean _groupRowDetail = false;
        public string _GroupDetailFieldName = "";
        // end table Property

        public _queryRule queryRule;
        public string PrintOption;
        public string Value;
        public string Currency_Field;

        public string ReplaceText;

        //barcode
        public SMLReport._design._barcodeType BarcodeType;
        public SMLReport._design._barcodeLabelPosition BarcodeLabelPosition;

        public SMLReport._design.overFlowType overFlow = _design.overFlowType.NewLine;

    }

    [Serializable]
    public class XmlTableObject
    {
        [XmlArrayItem("Column", typeof(_XmlTableColumns))]
        public ArrayList Columns = new ArrayList();

    }

    [Serializable]
    public class _XmlTableColumns
    {
        public string FieldFormat;
        public SMLReport._design._FieldType FieldType;
        public Boolean ShowIsZeroValue = true;
        /// <summary>ข้อความหัวตาราง</summary>
        public string HeaderText;
        public SMLReport._design._textAlign HeaderAlignment = _design._textAlign.Left;
        public ContentAlignment ContentHeaderAlign = ContentAlignment.MiddleLeft;
        public SMLReport._design._barcodeType BarcodeType;

        public string HeaderBackground;
        public float ColumnsWidth;
        public string Text;
        public SMLReport._design._textAlign TextAlignment = _design._textAlign.Left;
        public ContentAlignment ContentAlign = ContentAlignment.MiddleLeft;

        public Padding Padding;

        // for MultiFieldsCollection
        [XmlArrayItem("DrawObjectList", typeof(_xmlDrawObjectClass))]
        public ArrayList DrawObject = new ArrayList();

        public string ReplaceText;

        // serial number 
        public Boolean _printSerial;
        public int _serialNumberColumn;
        public SMLReport._design._serialNumberDisplayEnum _serialDiaplay;
        public Boolean _showSerialNewLine = true;
        public SMLReport._design._columnDisplayTypeEnum _groupType = _design._columnDisplayTypeEnum.DetailColumn;

        public Boolean _autoLineBreak = true;

        public Boolean _showLotNumber = false;
        public string _lotNumberField = "";
        public SMLReport._design._fieldOperation _lotOperation = _design._fieldOperation.None;
    }

    public class _XmlTableFooter
    {
        public string FieldFormat;
        public SMLReport._design._FieldType FieldType;
        public Boolean ShowIsZeroValue = true;
        public float ColumnsWidth;
        public string Text;
        public ContentAlignment ContentAlign;
        public string BackgroundColor;
        public string ForeColor;
        public Padding Padding;
        public string ReplaceText;
        public SMLReport._design._fieldOperation Operation;
    }


}
