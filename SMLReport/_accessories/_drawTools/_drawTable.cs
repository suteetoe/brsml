using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using System.Collections;
namespace SMLReport._design
{
    [Serializable]
    public class _drawTable : _drawRectangle
    {
        #region Table Private Field

        private const string _entryTable = "Table";
        private const string _entryColumns = "Columns";
        private const string _entryShowHeaderTable = "ShowHeader";
        private const string _entryShowFooter = "ShowFooter";
        private const string _entryRowHeight = "RowHeight";
        private const string _entryRowPerPage = "RowPerPage";
        private const string _entryColumnLineColor = "ColumnLineColor";
        private const string _entryColumnLineStyle = "ColumnLineStyle";
        private const string _entryHeaderBackColor = "HeaderBackColor";
        private const string _entryHeaderForeColor = "HeaderForeColor";
        private const string _entryHeaderLineColor = "HeaderLineColor";
        private const string _entryHeaderLineStyle = "HeaderLineStyle";
        private const string _entryRowLineColor = "RowLineColor";
        private const string _entryRowLineStyle = "RowLineStyle";
        private const string _entryFont = "Font";
        private const string _entryAutoLineSpace = "AutoLineSpace";
        private const string _entryColumnMultiField = "ColumnsMultiField";
        private const string _entryFooters = "Footers";
        private const string _entryFooterHeight = "FooterHeight";
        private const string _entryDataQuery = "DataQuery";

        private Color _foreColorResult = Color.Black;

        private bool _autoLineSpaceResult = true;
        //private float _lineSpaceResult;

        private _tableColumnsCollection _columnProperty = new _tableColumnsCollection();

        private int _rowPerPage = 0;
        private bool _showHeaderTable = true;

        private float _rowHeight = 20;
        private Color _headerBackground = Color.WhiteSmoke;
        private DashStyle _columnsDashStyle;
        private Color _columnsLineColor = Color.Black;
        private Font _fontResult = MyLib._myGlobal._myFontFormDesigner;
        private Font _fontHeaderResult = MyLib._myGlobal._myFontFormDesigner;

        private DashStyle _rowsDashStyle;
        private Color _rowsLineColor = Color.Transparent;

        private DashStyle _headerRowLineStyle;
        private Color _headerRowLineColor = Color.Black;
        private Color _headerForeColor = Color.Black;

        private int _columnsCountProperty = 0;
        private bool _columnsMultiFieldResult = false;

        private bool _averageRowHeightResult = true;

        private _tableFootersCollection _footerProperty = new _tableFootersCollection();
        private bool _showFooterTable = true;
        private float _footerHeightResult = 0f;
        private string[] _nameImageListResult;
        private bool _pageOverflowNewLineResult = true;

        #endregion

        #region Table Public Field        

        public float _tableWidthSacleResult = 0f;

        public SMLReport._formReport._queryRule __queryRuleProperty;
        public _drawPanel _activedrawPanel = null;
        public float _currentRowHeightResult = 0f;
        public SMLReport._formReport._imageList __imageList;

        // serial number field
        /// <summary>Query ของ Serial Number</summary>
        public SMLReport._formReport._queryRule __serialQueryResult;
        /// <summary>ฟิลด์ เชื่อมข้อมูลของ Table Query กับ Serial Number</summary>
        private string _dataPrimaryLinkResult = "";
        /// <summary>ฟิลด์ เชื่อมข้อมูลสกุลเงิน</summary>
        private string _dataCurrencyResult = "";
        /// <summary>ฟิลด์ เชื่อมข้อมูลของ Serial Number Query กับ Table Query</summary>
        private string _dataSecondLinkResult = "";
        /// <summary>ฟิลด์สำหรับเก็บ Serial Number</summary>
        private string _serialNumberFieldResult = "";

        #endregion

        #region Table Property

        [Browsable(false)]
        public string[] _nameImageList
        {
            get
            {
                return _nameImageListResult;
            }

            set
            {
                _nameImageListResult = value;
            }
        }


        private Boolean _groupRowDetailResult = false;

        /// <summary>
        /// รวมรายการข้อมูล
        /// </summary>
        [Category("Lot Number")]
        [Description("รวมรายการข้อมูล")]
        [DisplayName("Group Item Lot Number : ")]
        [DefaultValue(false)]        
        public Boolean _groupRowDetail
        {
            get
            {
                return this._groupRowDetailResult;
            }
            set
            {
                this._groupRowDetailResult = value;
            }
        }

        public string _groupDetailFileNameResult = "";

        [Browsable(false)]
        public string _groupDetailFileName
        {
            get { return _groupDetailFileNameResult; }
            set { _groupDetailFileNameResult = value; }
        }


        [Category("Footers")]
        [Description("กำหนดความสูงของ ท้ายตาราง กำหนด เป็น 0 เพื่อต้องการให้มีการคำณวนอัตโนมัติ")]
        [DisplayName("FooterWidth : ")]
        [DefaultValue(0)]
        public float _footerHeight
        {
            get
            {
                return _footerHeightResult;
            }
            set
            {
                _footerHeightResult = value;
            }
        }

        [Category("_SML")]
        [Description("AverageRowHeight")]
        [DisplayName("AverageRowHeight : ")]
        [DefaultValue(false)]
        public bool _averageRowHeight
        {
            get
            {
                return _averageRowHeightResult;
            }
            set
            {
                _averageRowHeightResult = value;
            }
        }

        [Category("_SML")]
        [Description("อนุญาติให้ใช้ ข้อมูลหลาย ๆ ฟิลด์ในคอลัมน์")]
        [DisplayName("ColumnMultiFields : หลายฟิลด์ในคอลัมน์")]
        [DefaultValue(false)]
        public bool _columnsMultiField
        {
            get
            {
                return _columnsMultiFieldResult;
            }
            set
            {
                _columnsMultiFieldResult = value;
            }
        }

        [Category("_SML")]
        [Description("กำหนดสีตัวอักษร")]
        [DisplayName("ForeColor : สีตัวอักษร")]
        public Color _foreColor
        {
            get
            {
                return _foreColorResult;
            }
            set
            {
                _foreColorResult = value;
            }
        }

        [Category("_SML")]
        [Description("การคำณวนระยะห่างระหว่างบรรทัด")]
        [DisplayName("AutoLineSpace")]
        [DefaultValue(true)]
        public bool _autoLineSpace
        {
            get
            {
                return _autoLineSpaceResult;
            }

            set
            {
                _autoLineSpaceResult = value;
            }
        }

        [Category("Data")]
        [Description("เลือกชุด query")]
        [DisplayName("DataQuery")]
        public SMLReport._formReport._queryRule DataQuery
        {
            get
            {
                return __queryRuleProperty;
            }

            set
            {
                __queryRuleProperty = value;
            }
        }

        /// <summary>
        /// ชุดตัวแปรของ Serial Number Query
        /// </summary>
        [Category("Serial Number")]
        [Description("เลือกชุด query ของ Serial Number")]
        [DisplayName("Serial Number DataQuery")]
        public SMLReport._formReport._queryRule SerialQuery
        {
            get { return __serialQueryResult; }
            set
            {
                __serialQueryResult = value;

                // define ฟิวเชื่อม
            }
        }

        // call converter ด้วย
        [Category("Data")]
        [Description("ฟิลด์เครื่องหมายสกุลเงิน")]
        [DisplayName("Field Currency Code")]
        [TypeConverter(typeof(_drawObjectFieldConv))]
        public string _dataCurrencyCode
        {
            get { return _dataCurrencyResult; }
            set { _dataCurrencyResult = value; }
        }

        // call converter ด้วย
        [Category("Serial Number")]
        [Description("ฟิลด์เชื่อมข้อมูล ตาราง")]
        [DisplayName("Field Link Table")]
        [TypeConverter(typeof(_drawObjectFieldConv))]
        public string _dataPrimaryLink
        {
            get { return _dataPrimaryLinkResult; }
            set { _dataPrimaryLinkResult = value; }
        }

        // call converter ด้วย
        [Category("Serial Number")]
        [Description("ฟิลด์เชื่อมข้อมูล Serial Number")]
        [DisplayName("Field Link Serial Number")]
        [TypeConverter(typeof(_serialNumberFieldConv))]
        public string _dataSecondLink
        {
            get { return _dataSecondLinkResult; }
            set { _dataSecondLinkResult = value; }
        }

        [Category("Serial Number")]
        [Description("ฟิลด์เก็บ Serial Number")]
        [DisplayName("Serial Number Field")]
        [TypeConverter(typeof(_serialNumberFieldConv))]
        public string _serialNumberField
        {
            get { return _serialNumberFieldResult; }
            set { _serialNumberFieldResult = value; }
        }

        [Category("Columns")]
        [Description("คอลัมน์ข้อมูล")]
        [DisplayName("Columns : คอลัมน์")]
        [Browsable(true)]
        [TypeConverter(typeof(_tableColumnsCollectionConverter))]
        [Editor(typeof(_customColumnCollectionEditor), typeof(UITypeEditor))]
        public _tableColumnsCollection Columns
        {
            get
            {
                return _columnProperty;
            }

            set
            {
                _columnProperty = value;
            }
        }

        [Category("Footers")]
        [Description("ท้ายตาราง")]
        [DisplayName("Footers : ท้ายตาราง")]
        [Browsable(true)]
        [TypeConverter(typeof(_tableFootersCollectionConverter))]
        [Editor(typeof(_customFooterCollectionEditor), typeof(UITypeEditor))]
        public _tableFootersCollection Footers
        {
            get
            {
                return _footerProperty;
            }

            set
            {
                _footerProperty = value;
            }
        }

        [Category("Columns")]
        [Description("กำหนดรูปแบบเส้นแบ่งคอลัมน์")]
        [DisplayName("Table ColumnsLine Style ")]
        public DashStyle ColumnsSeparatorLine
        {
            get
            {
                return _columnsDashStyle;
            }

            set
            {
                _columnsDashStyle = value;
            }
        }

        [Category("Columns")]
        [Description("กำหนดสีเส้นแบ่งคอลัมน์")]
        [DisplayName("Line Color")]
        public Color ColumnsSeparatorLineColor
        {
            get
            {
                return _columnsLineColor;
            }

            set
            {
                _columnsLineColor = value;
            }
        }

        [Category("_SML")]
        [Description("การจัดวาง")]
        [DisplayName("Rows Per Page : จำนวนแถวต่อหน้า")]
        [DefaultValue(0)]
        public int RowPerPage
        {
            get
            {
                return _rowPerPage;
            }

            set
            {
                _rowPerPage = value;
            }
        }

        [Category("_SML")]
        [Description("การจัดวาง")]
        [DisplayName("Row Height : ความสูงของแถว")]
        [DefaultValue(0)]
        public float RowHeight
        {
            get
            {
                return _rowHeight;
            }
            set
            {
                _rowHeight = value;
            }
        }

        [Category("_SML")]
        [Description("ตัวเลือกการแสดงหัวคอลัมน์ของตาราง")]
        [DisplayName("Show Header Columns : แสดงหัวคอลัมน์")]
        [DefaultValue(true)]
        public bool ShowHeaderColumns
        {
            get
            {
                return _showHeaderTable;
            }

            set
            {
                _showHeaderTable = value;
            }
        }

        [Category("_SML")]
        [Description("ตัวเลือกการแสดงท้ายตาราง")]
        [DisplayName("Show Footer: แสดงท้ายตาราง")]
        [DefaultValue(true)]
        public bool ShowFooter
        {
            get
            {
                return _showFooterTable;
            }

            set
            {
                _showFooterTable = value;
            }
        }

        [Category("Header")]
        [Description("กำหนด สีพื้นหลังของหัวตาราง")]
        [DisplayName("Header Backgroud : สีพื้นหลังหัวตาราง")]
        public Color HeaderBackground
        {
            get
            {
                return _headerBackground;
            }
            set
            {
                _headerBackground = value;
            }
        }

        [Category("Header")]
        [Description("รูปแบบเส้นแถวหัวตาราง")]
        [DisplayName("Header Line Style : รูปแบบเส้นแถวหัวตาราง")]
        [DefaultValue(DashStyle.Solid)]
        public DashStyle HeaderRowLineStyle
        {
            get
            {
                return _headerRowLineStyle;
            }

            set
            {
                _headerRowLineStyle = value;
            }
        }

        [Category("Header")]
        [Description("รูปแบบเส้นแถวหัวตาราง")]
        [DisplayName("Header Line Color : สีเส้นแถวหัวตาราง")]
        public Color HeaderRowLineColor
        {
            get
            {
                return _headerRowLineColor;
            }

            set
            {
                _headerRowLineColor = value;
            }
        }

        [Category("Header")]
        [Description("สีตัวอักษรของหัวตาราง")]
        [DisplayName("Header ForeColor")]
        public Color HeaderForeColor
        {
            get
            {
                return _headerForeColor;
            }
            set
            {
                _headerForeColor = value;
            }
        }

        [Category("Rows")]
        [Description("กำหนด รูปแบบของเส้นแบ่งแถว")]
        [DisplayName("Row LineStyle :")]
        [DefaultValue(DashStyle.Solid)]
        public DashStyle RowLineStyle
        {
            get
            {
                return _rowsDashStyle;
            }

            set
            {
                _rowsDashStyle = value;
            }
        }

        [Category("Rows")]
        [Description("กำหนด รูปแบบของเส้นแบ่งแถว")]
        [DisplayName("Row LineStyle :")]
        public Color RowLineColor
        {
            get
            {
                return _rowsLineColor;
            }
            set
            {
                _rowsLineColor = value;
            }
        }

        [Category("_SML")]
        [Description("รูปแบบตัวอักษร")]
        [DisplayName("Font : รูปแบบตัวอักษร")]
        public Font _font
        {
            get
            {
                return _fontResult;
            }
            set
            {
                _fontResult = value;
            }
        }

        [Category("Header")]
        [Description("กำหนด รูปแบบตัวอักษรของหัวตาราง")]
        [DisplayName("Header Font : รูปแบบตัวอักษรหัวตาราง")]
        public Font _headerFont
        {
            get
            {
                return _fontHeaderResult;
            }
            set
            {
                _fontHeaderResult = value;
            }
        }

        [Browsable(false)]
        public int ColumnsCountProperty
        {
            get { return _columnsCountProperty; }
            set { _columnsCountProperty = value; }
        }

        /// <summary>
        /// กรณีล้นหน้า จะไปขึ้นหน้าใหม่ทั้งบรรทัด
        /// </summary>
        [Category("Appearance")]
        [Description("กรณีล้นหน้า จะไปขึ้นหน้าใหม่ทั้งบรรทัด")]
        [DisplayName("Page Overflow NewLine : ขึ้นหน้าใหม่ทั้งบรรทัด")]
        public bool _pageOverflowNewLine
        {
            get { return _pageOverflowNewLineResult; }
            set { _pageOverflowNewLineResult = value; }
        }

        #endregion

        #region Table Constructor

        public _drawTable()
        {
            SetRectangle(0, 0, 1, 1, _drawScale);

            _initialize();
        }

        public _drawTable(float scale)
        {
            SetRectangle(0, 0, 1, 1, scale);
            _initialize();
        }

        public _drawTable(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            x = (int)(x / _drawScale);
            y = (int)(y / _drawScale);
            width = (int)(width / _drawScale);
            height = (int)(height / _drawScale);
            _actualSize = new Rectangle(x, y, width, height);

            _initialize();
        }

        public _drawTable(int x, int y, int width, int height, float scale, _drawPanel drawArea)
        {
            _activedrawPanel = drawArea;
            _drawScale = scale;
            x = (int)(x / _drawScale);
            y = (int)(y / _drawScale);
            width = (int)(width / _drawScale);
            height = (int)(height / _drawScale);
            _actualSize = new Rectangle(x, y, width, height);
            _initialize();
        }

        #endregion

        #region Table Private Methods

        public void _processTableWidthScale()
        {
            _tableWidthSacleResult = (((float)Size.Width) / 100);
        }

        private void _drawMultiFieldObject(Graphics g, _tableColumns __column, PointF __startDrawPoint)
        {
            for (int __i = 0; __i < __column._multiFieldCollection.Count; __i++)
            {
                ((_drawObject)__column._multiFieldCollection[__i])._drawFromPoint(g, __startDrawPoint, this._drawScale);
            }
        }

        private string _getReplaceFooterText(string _replaceText, string __data)
        {
            if (_replaceText == "")
                return __data;

            if (_replaceText.IndexOf("@") != -1)
            {
                _replaceText = _replaceText.Replace("@", __data);
            }

            return _replaceText;
        }

        private string _getReplaceText(string _replaceText, string __data)
        {
            if (_replaceText.IndexOf("@") != -1)
            {
                __data = _replaceText.Replace("@", __data);
            }

            return __data;
        }

        private string _getResource(string __strFieldAndResource)
        {
            if (__strFieldAndResource == null)
                return "";

            string[] __split = __strFieldAndResource.Split(',');
            if (__strFieldAndResource.Trim().Length == 0 || __strFieldAndResource.Length == 0)
            {
                return __strFieldAndResource;
            }

            if (__split.Length == 1)
                return __split[0].Trim().ToString();

            string __result = __split[1].Trim();
            if (__result.Length == 0)
            {
                return __split[0].Trim();
            }

            return __result;

        }

        private float _getHeaderHeight()
        {
            float __headerHeight = 0;

            if (_showHeaderTable == false)
                return __headerHeight;

            float __tableWidthSacle = (((float)Size.Width) / 100);
            Font __newFont = new Font(_headerFont.FontFamily, (float)_headerFont.Size, _headerFont.Style, _headerFont.Unit, _headerFont.GdiCharSet, _headerFont.GdiVerticalFont);

            for (int __i = 0; __i < this.Columns.Count; __i++)
            {
                string __tmpHeaderText = "Test";
                if ((this.Columns[__i].HeaderText != null) && (this.Columns[__i].HeaderText != ""))
                {
                    __tmpHeaderText = this.Columns[__i].HeaderText;
                }

                float __colW = getColumnsWidth(__i);


                ArrayList __strList = _drawLabel._cutString(_drawLabel._replaceLineBreak(__tmpHeaderText, true), __newFont, __colW * __tableWidthSacle, 0, 0, new Padding(0));
                SizeF __tmpSize = _getTextSize(__strList, __newFont);

                if (__tmpSize.Height > __headerHeight)
                {
                    __headerHeight = __tmpSize.Height;
                }
            }

            return __headerHeight;
        }

        private float _getFooterHeight()
        {
            float __footerHeight = 0;

            if (this._showFooterTable == false || this.Footers.Count == 0)
                return __footerHeight;

            if (_footerHeight > 0)
                return _footerHeight;

            float __tableWidthSacle = (((float)Size.Width) / 100);
            Font __newFont = new Font(_font.FontFamily, (float)_font.Size, _font.Style, _font.Unit, _font.GdiCharSet, _font.GdiVerticalFont);

            string __sampleText = "SampleText";
            Control __tmpControl = new Control();
            Graphics g = __tmpControl.CreateGraphics();
            SizeF __tmpSize = g.MeasureString(__sampleText, this._font);

            __footerHeight = __tmpSize.Height;

            __tmpControl.Dispose();

            /*
            for (int __i = 0; __i < this.Footers.Count; __i++)
            {
                string __tmpHeaderText = "Test";
                if ((this.Footers[__i].HeaderText != null) && (this.Columns[__i].HeaderText != ""))
                {
                    __tmpHeaderText = this.Footers[__i].HeaderText;
                }

                float __colW = _getFootersWidth(__i);


                ArrayList __strList = _drawLabel._cutString(_drawLabel._replaceLineBreak(__tmpHeaderText, true), __newFont, __colW * __tableWidthSacle, 0, 0, new Padding(0));
                SizeF __tmpSize = _getTextSize(__strList, __newFont);

                if (__tmpSize.Height > __footerHeight)
                {
                    __footerHeight = __tmpSize.Height;
                }
            }*/

            return __footerHeight;
        }

        #endregion

        #region Table Public Method

        public override void _draw(Graphics g)
        {
            int __columnsCount = this.Columns.Count;

            Pen pen = new Pen(this._lineColor, this._penWidth * (float)_drawScale);
            SolidBrush brush = new SolidBrush(this._backColor);

            g.FillRectangle(brush, _drawRectangle._getNormalizedRectangle(Size));
            g.DrawRectangle(pen, _drawRectangle._getNormalizedRectangle(Size));

            pen.Dispose();
            brush.Dispose();
            // draw columns

            // draw Header 
            this._drawHeaderTable(g);

            //draw data detail Row 
            this._drawRowDetailTable(g);
        }

        public void _drawHeaderTable(Graphics g)
        {
            float __headerHeight = _getHeaderHeight() * _drawScale;
            float __tableWidthSacle = (((float)Size.Width) / 100);

            Font __newFont = new Font(_headerFont.FontFamily, (float)(_headerFont.Size * _drawScale), _headerFont.Style, _headerFont.Unit, _headerFont.GdiCharSet, _headerFont.GdiVerticalFont);
            SolidBrush __HeaderTextBrush = new SolidBrush(this.HeaderForeColor);
            Pen __headerRowLinePen = new Pen(this.HeaderRowLineColor, this._penWidth * (float)_drawScale);

            if (this.ShowHeaderColumns == true)
            {
                __headerRowLinePen.DashStyle = this.HeaderRowLineStyle;
                g.DrawLine(__headerRowLinePen, Size.X, Size.Y + __headerHeight, Size.X + Size.Width, Size.Y + __headerHeight);

                // define columnsSeparatorLineStyle
                //Pen __ColHeaderLinePen = new Pen(this.ColumnsSeparatorLineColor, this._penWidth * (float)_drawScale);
                //__ColHeaderLinePen.DashStyle = ColumnsSeparatorLine;

                SolidBrush __HeaderBGBrush = new SolidBrush(this.HeaderBackground);
                Pen __headerBgPen = new Pen(Color.Transparent, this._penWidth * _drawScale);
                g.FillRectangle(__HeaderBGBrush, Size.X, Size.Y, Size.Width, __headerHeight);
                g.DrawRectangle(__headerRowLinePen, Size.X, Size.Y, Size.Width, __headerHeight);

                __HeaderBGBrush.Dispose();
                __headerBgPen.Dispose();

                // draw header columns line
                PointF __currentPos = new PointF();

                for (int __i = 0; __i < this.Columns.Count; __i++)
                {
                    float __colw = getColumnsWidth(__i);
                    __currentPos.Y = 0;
                    // draw Header Background

                    // draw Header Text
                    if (this.Columns[__i].HeaderText != "")
                    {
                        ArrayList __dataList = _drawLabel._cutString(_drawLabel._replaceLineBreak(this.Columns[__i].HeaderText, true), __newFont, __colw * __tableWidthSacle);
                        SizeF __columnSize = _getTextSize(__dataList, __newFont);
                        PointF __drawPoint = _getPointTextAlingDraw(__colw * __tableWidthSacle, __headerHeight, __columnSize.Width, __columnSize.Height, this.Columns[__i].HeaderAlignment);
                        __currentPos.Y += __drawPoint.Y;


                        for (int __line = 0; __line < __dataList.Count; __line++)
                        {
                            SizeF __stringSize = _getTextSize((string)__dataList[__line], __newFont);
                            PointF __stringDrawPoint = _getPointTextAlingDraw(__colw * __tableWidthSacle, __headerHeight, __stringSize.Width, __stringSize.Height, this.Columns[__i].HeaderAlignment);

                            g.DrawString((string)__dataList[__line], __newFont, __HeaderTextBrush, Size.X + __currentPos.X + __stringDrawPoint.X, Size.Y + __currentPos.Y, StringFormat.GenericTypographic);

                            __currentPos.Y += __stringSize.Height;
                        }
                        /*
                        string __dataStr = _drawLabel._replaceLineBreak(this.Columns[__i].HeaderText, true);
                        g.DrawString(__dataStr, __newFont, __HeaderTextBrush, Rectangle.Round(new RectangleF(Size.X + __currentPos.X, Size.Y, __colw * __tableWidthSacle, __headerHeight)), _getStringFormat(this.Columns[__i].HeaderAlignment));
                        */

                    }

                    if (__i != this.Columns.Count - 1)
                    {
                        PointF __startLineCol = new PointF(Size.X + __currentPos.X + (__colw * __tableWidthSacle), Size.Y);
                        PointF __endLineCol = new PointF(Size.X + __currentPos.X + (__colw * __tableWidthSacle), Size.Y + __headerHeight);

                        g.DrawLine(__headerRowLinePen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                        __currentPos.X += __colw * __tableWidthSacle;
                    }
                }
            }

            __HeaderTextBrush.Dispose();
            __headerRowLinePen.Dispose();

        }

        public void _drawRowDetailTable(Graphics g)
        {
            float __tableWidthSacle = (((float)Size.Width) / 100);
            float __headerHeight = _getHeaderHeight() * _drawScale;
            float __footerHeight = _getFooterHeight() * _drawScale;
            bool _havePrintSerialNumberNewLine = false;
            bool _havePrintLotNumber = false;

            PointF __currentPoint = new PointF();
            PointF __rowDetailRectPoint = new PointF(Size.X, Size.Y);

            float __rowsDetailHeight = (this.ShowHeaderColumns) ? Size.Height - __headerHeight - __footerHeight : Size.Height - __footerHeight;

            if (this.ShowHeaderColumns)
            {
                __rowDetailRectPoint.Y = Size.Y + __headerHeight;
            }

            float __rowsHeight = _getRowsHeight(__rowsDetailHeight);

            if (_averageRowHeightResult == false)
                __rowsHeight = _currentRowHeightResult;

            Pen __ColLinePen = new Pen(this.ColumnsSeparatorLineColor, this._penWidth * (float)_drawScale);
            __ColLinePen.DashStyle = ColumnsSeparatorLine;
            Font __newFont = new Font(_font.FontFamily, (float)(_font.Size * _drawScale), _font.Style, _font.Unit, _font.GdiCharSet, _font.GdiVerticalFont);

            ArrayList __tmpColumnsPoint = new ArrayList();
            ArrayList __tmpColumnsText = new ArrayList();
            ArrayList __tmpColumnsSize = new ArrayList();
            // draw columns Line
            for (int __i = 0; __i < this.Columns.Count; __i++)
            {
                float __colw = getColumnsWidth(__i);

                // assign arrayList

                string __strShowResource = _getReplaceText(this.Columns[__i]._replaceText, _getResource(this.Columns[__i].Text));

                if (this.Columns[__i]._printSerialNumber && this.Columns[__i]._showSerialNewLine == false)
                {
                    __strShowResource = __strShowResource + " #Serial_Number#";
                }

                SizeF __stringSize = this._getTextSize(__strShowResource, __newFont);
                PointF __tmpPoint = _getPointTextAlingDraw(__colw * __tableWidthSacle, __rowsHeight, __stringSize.Width, __stringSize.Height, this.Columns[__i].TextAlignment, Columns[__i]._padding);
                float __colWidth = __colw * __tableWidthSacle;

                __tmpColumnsSize.Add(new SizeF(__colWidth, __rowsHeight));
                __tmpColumnsText.Add(__strShowResource);
                __tmpColumnsPoint.Add(__tmpPoint);

                // draw Column Line 
                if (__i != this.Columns.Count - 1)
                {
                    PointF __startLineCol = new PointF(__rowDetailRectPoint.X + __currentPoint.X + (__colw * __tableWidthSacle), __rowDetailRectPoint.Y);
                    PointF __endLineCol = new PointF(__rowDetailRectPoint.X + __currentPoint.X + (__colw * __tableWidthSacle), __rowDetailRectPoint.Y + __rowsDetailHeight);
                    g.DrawLine(__ColLinePen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                    __currentPoint.X += __colw * __tableWidthSacle;
                }

                if (this.Columns[__i]._printSerialNumber && this.Columns[__i]._showSerialNewLine)
                {
                    _havePrintSerialNumberNewLine = true;
                }

                if (this.Columns[__i].showLotNumber == true)
                {
                    _havePrintLotNumber = true;
                }
            }

            __ColLinePen.Dispose();

            // draw rows detail

            if (this.Columns.Count > 0)
            {
                Pen RowLinePen = new Pen(this.RowLineColor, this._penWidth * (float)_drawScale);
                RowLinePen.DashStyle = RowLineStyle;
                SolidBrush __rowsTextBrush = new SolidBrush(_foreColor);

                int __numRows = _getNumAllRows(__rowsDetailHeight);
                int __rowDivide = 1;
                //SolidBrush __rowsDetailbrush = new SolidBrush(this._backColor);
                //__currentY = 0;

                if (_havePrintSerialNumberNewLine)
                {
                    __rowDivide++;
                }

                if (_havePrintLotNumber)
                {
                    __rowDivide++;
                }

                if (__rowDivide > 1)
                {
                    __numRows = __numRows / 2;

                }

                __currentPoint.X = 0; __currentPoint.Y = 0;

                // drew ColumnsContent
                PointF __drawStrPoint = new PointF();

                for (int __i = 0; __i < __numRows; __i++)
                {
                    __currentPoint.X = 0;

                    float __currentRowHeight = __rowsHeight;

                    for (int __j = 0; __j < this.Columns.Count; __j++)
                    {
                        if (this._columnsMultiField)
                        {
                            _tableColumns __column = (_tableColumns)this.Columns[__j];
                            PointF __startDrawPoint = new PointF();
                            __startDrawPoint.X = __rowDetailRectPoint.X + __currentPoint.X;
                            __startDrawPoint.Y = __rowDetailRectPoint.Y + __currentPoint.Y;

                            // หา point ของ start column

                            _drawMultiFieldObject(g, __column, __startDrawPoint);
                        }
                        else
                        {

                            if ((this.Columns[__j].Text != null) && (this.Columns[__j].Text != ""))
                            {
                                __drawStrPoint.X = __rowDetailRectPoint.X + __currentPoint.X + (((PointF)__tmpColumnsPoint[__j]).X);
                                __drawStrPoint.Y = __rowDetailRectPoint.Y + __currentPoint.Y + (((PointF)__tmpColumnsPoint[__j]).Y);

                                g.DrawString((string)__tmpColumnsText[__j], __newFont, __rowsTextBrush, Point.Round(__drawStrPoint), StringFormat.GenericTypographic);

                                //__drawStrPoint.X = __rowDetailRectPoint.X + __currentPoint.X ;
                                //__drawStrPoint.Y = __rowDetailRectPoint.Y + __currentPoint.Y ;
                                //g.DrawString((string)__tmpColumnsText[__j], __newFont, __rowsTextBrush, _getRectangleFPadding(new RectangleF(__drawStrPoint, (SizeF)__tmpColumnsSize[__j]), this.Columns[__j]._padding), _getStringFormat(this.Columns[__j].TextAlignment));
                            }

                            if (this.Columns[__j]._printSerialNumber && this.Columns[__j]._showSerialNewLine)
                            {
                                // print serial
                                __drawStrPoint.Y += __rowsHeight;
                                g.DrawString("#Serial_Number#", __newFont, __rowsTextBrush, Point.Round(__drawStrPoint), StringFormat.GenericTypographic);
                                __currentRowHeight += __rowsHeight;

                            }

                            if (this.Columns[__j].showLotNumber)
                            {
                                __drawStrPoint.Y += __rowsHeight;
                                g.DrawString("#LotNumber#", __newFont, __rowsTextBrush, Point.Round(__drawStrPoint), StringFormat.GenericTypographic);
                                __currentRowHeight += __rowsHeight;

                            }

                        }

                        __currentPoint.X += ((SizeF)__tmpColumnsSize[__j]).Width;
                    }

                    // drew Line
                    if (__i != __numRows - 1)
                    {
                        PointF __lineStartPoint = new PointF(Size.X, __rowDetailRectPoint.Y + __currentPoint.Y + __currentRowHeight);
                        PointF __lineEndPoit = new PointF(Size.X + Size.Width, __rowDetailRectPoint.Y + __currentPoint.Y + __currentRowHeight);

                        g.DrawLine(RowLinePen, Point.Round(__lineStartPoint), Point.Round(__lineEndPoit));
                    }

                    // assign new Y Point
                    __currentPoint.Y += __currentRowHeight;

                }

                RowLinePen.Dispose();
                __rowsTextBrush.Dispose();
            }

            // draw Footer

            if (_showFooterTable == true && this.Footers.Count > 0)
            {
                // draw footer line
                PointF __startFooterPoint = new PointF(Size.X, Size.Y);

                if (this.ShowHeaderColumns)
                {
                    __startFooterPoint.Y = Size.Y + __headerHeight;
                }

                __startFooterPoint.Y += __rowsDetailHeight;

                PointF __lineStartFooterPoint = __startFooterPoint;
                PointF __lineEndFooterPoint = new PointF(Size.X + Size.Width, __startFooterPoint.Y);

                Pen __footerPen = new Pen(this._lineColor, this._penWidth * (float)_drawScale);
                SolidBrush __footerBrush = new SolidBrush(this._foreColor);
                //_footerPen.DashStyle = RowLineStyle;
                g.DrawLine(__footerPen, Point.Round(__lineStartFooterPoint), Point.Round(__lineEndFooterPoint));


                PointF __currentFooterPoint = __startFooterPoint;
                for (int __i = 0; __i < this.Footers.Count; __i++)
                {
                    float __footerWidth = _getFootersWidth(__i) * __tableWidthSacle;

                    // draw footer text
                    string __footerText = _getReplaceFooterText(this.Footers[__i]._replaceText, _getResource(this.Footers[__i].Text));

                    SizeF __stringSize = this._getTextSize(__footerText, __newFont);
                    PointF __tmpPoint = _getPointTextAlingDraw(__footerWidth, __footerHeight, __stringSize.Width, __stringSize.Height, this.Footers[__i].TextAlignment, this.Footers[__i]._padding);

                    g.DrawString(__footerText, __newFont, __footerBrush, Point.Round(new PointF(__currentFooterPoint.X + __tmpPoint.X, __currentFooterPoint.Y + __tmpPoint.Y)), StringFormat.GenericTypographic);

                    // draw  Footer Column Line
                    if (__i != this.Footers.Count - 1)
                    {
                        __currentFooterPoint.X += __footerWidth;

                        PointF __footerColumnsStartPoint = __currentFooterPoint;
                        PointF __footerColumnsEndPoint = new PointF(__currentFooterPoint.X, __currentFooterPoint.Y + __footerHeight);
                        g.DrawLine(__footerPen, Point.Round(__footerColumnsStartPoint), Point.Round(__footerColumnsEndPoint));
                    }
                }
            }

        }

        public int _getNumAllRows()
        {
            if (ShowHeaderColumns == false)
                return _getNumAllRows(Size.Height);

            float __detailHeight = this.Size.Height - (_getHeaderHeight() * _drawScale);
            return _getNumAllRows(__detailHeight);
        }

        /// <summary>
        /// คำณวณการแสดงผลต่อ 1 หน้า ว่าได้กี่บรรทัด
        /// </summary>
        /// <param name="__detailRowsHeight"></param>
        /// <returns></returns>
        public int _getNumAllRows(float __detailRowsHeight)
        {
            int __numRows = 0;
            float __defaultRowHeight = RowHeight;

            string __sampleText = "SampleText";
            Control __tmpControl = new Control();
            Graphics g = __tmpControl.CreateGraphics();
            SizeF __tmpSize = g.MeasureString(__sampleText, this._font);
            __defaultRowHeight = __tmpSize.Height;
            _currentRowHeightResult = __tmpSize.Height;
            __tmpControl.Dispose();

            // return RowPerPage if define it
            if (RowPerPage != 0)
            {
                return this.RowPerPage;
            }

            if ((_autoLineSpace) || (__defaultRowHeight == 0))
            {
                RowHeight = __defaultRowHeight;
            }

            __numRows = (int)Math.Floor(__detailRowsHeight / (RowHeight * _drawScale));
            return __numRows;
        }

        public float _getFootersWidth(int __index)
        {
            if (this.Footers.Count == 1)
                return 100;

            float __allFooterWidth = _getAllFootersWidth();
            float __widthScale = 100 / __allFooterWidth;

            if (__index < this.Footers.Count)
            {
                int __countAutoWidthColumns = 0;
                int __maxPercentWidth = (int)Math.Ceiling(__allFooterWidth / 100) * 100;

                List<float> __FootersWidthTmpPercent = new List<float>();
                for (int __i = 0; __i < this.Footers.Count; __i++)
                {
                    if (this.Footers[__i].ColumnsWidth == 0)
                    {
                        __countAutoWidthColumns++;
                        __FootersWidthTmpPercent.Add(this.Footers[__i].ColumnsWidth);
                    }
                    else
                    {
                        __FootersWidthTmpPercent.Add(this.Footers[__i].ColumnsWidth);
                    }
                }

                if (__countAutoWidthColumns > 0)
                {
                    float __shareAutoColumns = (100 - ((__allFooterWidth / __maxPercentWidth) * 100)) / __countAutoWidthColumns;
                    if (__maxPercentWidth == 0)
                        __shareAutoColumns = 100 / __countAutoWidthColumns;

                    for (int __i = 0; __i < __FootersWidthTmpPercent.Count; __i++)
                    {
                        if (__FootersWidthTmpPercent[__i] == 0)
                        {
                            __FootersWidthTmpPercent[__i] = __shareAutoColumns;
                        }
                    }
                }
                else
                {
                    return Footers[__index].ColumnsWidth * __widthScale;
                }

                return __FootersWidthTmpPercent[__index];
            }

            return 0;
        }

        public float _getAllFootersWidth()
        {
            float __allColumnsCount = 0;
            if (this.Footers.Count > 1)
            {
                for (int __i = 0; __i < this.Footers.Count; __i++)
                {
                    __allColumnsCount += this.Footers[__i].ColumnsWidth;
                }
            }
            return __allColumnsCount;
        }

        public float getColumnsWidth(int __index)
        {
            if (this.Columns.Count == 1)
                return 100;
            // check all columns sum is 100 %
            float __AllColWidth = _getAllColumnsWidth();

            float __widthScale = 100 / __AllColWidth;

            if (__index < this.Columns.Count)
            {

                int __countAutoWidthColumns = 0;
                int __maxPercentWidth = (int)Math.Ceiling(__AllColWidth / 100) * 100;

                List<float> __columnsWidthTmpPercent = new List<float>();
                for (int __i = 0; __i < this.Columns.Count; __i++)
                {
                    if (this.Columns[__i].ColumnsWidth == 0)
                    {
                        __countAutoWidthColumns++;
                        __columnsWidthTmpPercent.Add(this.Columns[__i].ColumnsWidth);
                    }
                    else
                    {
                        __columnsWidthTmpPercent.Add(this.Columns[__i].ColumnsWidth);
                    }
                }

                if (__countAutoWidthColumns > 0)
                {
                    float __shareAutoColumns = (100 - ((__AllColWidth / __maxPercentWidth) * 100)) / __countAutoWidthColumns;
                    if (__maxPercentWidth == 0)
                        __shareAutoColumns = 100 / __countAutoWidthColumns;

                    for (int __i = 0; __i < __columnsWidthTmpPercent.Count; __i++)
                    {
                        if (__columnsWidthTmpPercent[__i] == 0)
                        {
                            __columnsWidthTmpPercent[__i] = __shareAutoColumns;
                        }
                    }
                }
                else
                {
                    return Columns[__index].ColumnsWidth * __widthScale;
                }

                return __columnsWidthTmpPercent[__index];
            }

            return 0;
        }

        public float _getAllColumnsWidth()
        {
            float __allColumnsCount = 0;
            if (this.Columns.Count > 1)
            {
                for (int __i = 0; __i < this.Columns.Count; __i++)
                {
                    __allColumnsCount += this.Columns[__i].ColumnsWidth;
                }
            }
            return __allColumnsCount;
        }

        public float _getRowsHeight()
        {
            float __rowHeight = 0;

            if (Size == null)
                return 0;

            __rowHeight = ((float)Size.Height) / this._getNumAllRows();

            return __rowHeight;
        }

        public float _getRowsHeight(float __detailRowsHeight)
        {
            float __rowHeight = 0;

            if (Size == null)
                return 0;

            __rowHeight = ((float)__detailRowsHeight) / this._getNumAllRows(__detailRowsHeight);

            return __rowHeight;
        }

        public string[] _getNameInImageList(_drawObject sender)
        {
            if (GetNameImageList != null)
            {
                GetNameImageList(sender);
            }
            if (this._nameImageListResult != null)
                return this._nameImageListResult;

            return null;
        }

        #region Save Load From Stream

        public override void _saveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable, orderNumber),
                _actualSize);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryDataQuery, orderNumber),
                __queryRuleProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumns, orderNumber),
                _columnProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryFooters, orderNumber),
                _footerProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryShowHeaderTable, orderNumber),
                _showHeaderTable);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryShowFooter, orderNumber),
                _showFooterTable);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryFooterHeight, orderNumber),
                _footerHeight);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryAutoLineSpace, orderNumber),
                _autoLineSpace);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumnMultiField, orderNumber),
                _columnsMultiField);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowHeight, orderNumber),
                _rowHeight);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowPerPage, orderNumber),
                _rowPerPage);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumnLineColor, orderNumber),
                _columnsLineColor.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumnLineStyle, orderNumber),
                _columnsDashStyle);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderBackColor, orderNumber),
                _headerBackground.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderForeColor, orderNumber),
                _headerForeColor.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderLineColor, orderNumber),
                _headerRowLineColor.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderLineStyle, orderNumber),
                _headerRowLineStyle);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowLineColor, orderNumber),
                _rowsLineColor.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowLineStyle, orderNumber),
                _rowsDashStyle);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryFont, orderNumber),
                _fontResult);
            base._saveToStream(info, orderNumber);
        }

        public override void _loadFromStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            _actualSize = (Rectangle)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable, orderNumber),
                typeof(Rectangle));

            __queryRuleProperty = (_formReport._queryRule)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryDataQuery, orderNumber),
                typeof(_formReport._queryRule));

            _columnProperty = (_tableColumnsCollection)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumns, orderNumber),
                typeof(_tableColumnsCollection));

            _footerProperty = (_tableFootersCollection)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryFooters, orderNumber),
                typeof(_tableFootersCollection));

            _showHeaderTable = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryShowHeaderTable, orderNumber),
                typeof(bool));

            _showFooterTable = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryShowFooter, orderNumber),
                typeof(bool));

            _footerHeight = (float)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryFooterHeight, orderNumber),
                typeof(float));

            _autoLineSpace = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryAutoLineSpace, orderNumber),
                typeof(bool));

            _columnsMultiField = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumnMultiField, orderNumber),
                typeof(bool));

            _rowHeight = (float)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowHeight, orderNumber),
                typeof(float));

            _rowPerPage = (int)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowPerPage, orderNumber),
                typeof(int));

            int tmpcolLineColor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumnLineColor, orderNumber));
            _columnsLineColor = __getColorFromInt32(tmpcolLineColor);

            _columnsDashStyle = (DashStyle)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryColumnLineStyle, orderNumber),
                typeof(DashStyle));

            int tmpHeaderBackcolor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderBackColor, orderNumber));
            _headerBackground = __getColorFromInt32(tmpHeaderBackcolor);

            int tmpHeaderForeColor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderForeColor, orderNumber));
            _headerForeColor = __getColorFromInt32(tmpHeaderForeColor);

            int tmpHeaderRowLineColor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderLineColor, orderNumber));
            _headerRowLineColor = __getColorFromInt32(tmpHeaderRowLineColor);

            _headerRowLineStyle = (DashStyle)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryHeaderLineStyle, orderNumber),
                typeof(DashStyle));

            int tmpRowLineColor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowLineColor, orderNumber));
            _rowsLineColor = __getColorFromInt32(tmpRowLineColor);

            _rowsDashStyle = (DashStyle)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryRowLineStyle, orderNumber),
                typeof(DashStyle));

            _fontResult = (Font)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryTable + _entryFont, orderNumber),
                typeof(Font));

            base._loadFromStream(info, orderNumber);
        }

        #endregion

        #endregion

        #region Table Event

        public event _getImageListName GetNameImageList;

        #endregion

    }
}
