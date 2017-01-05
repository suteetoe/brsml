using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;

namespace SMLReport._design
{
    [Serializable]
    public class _drawImageField : _drawObject
    {
        #region ImageField Private Field

        private const string _entryLabel = "ImageField";
        private const string _entryBackColor = "BackColor";
        private const string _entryForeColor = "ForeColor";
        private const string _entryTextAlign = "BarcodeLabelAlign";
        private const string _entryFont = "Font";
        private const string _entryFieldType = "FieldType";
        private const string _entryDataQuery = "Query";
        private const string _entryShowText = "ShowText";

        private string _fieldProperty = "[EmptyField]";
        private _FieldType _fieldType;
        private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.StretchImage;
        private DashStyle _lineStyleResult = DashStyle.Solid;
        private string _imageObjectNameResult;
        private string _fieldCompareValueResult;
        private SMLReport._formReport._queryRule __queryRuleProperty;
        private string[] _nameImageListResult;
        private bool _alwaysShowResult = false;
        private _barcodeType _typeBarcodeResult;
        private _barcodeLabelPosition _barcodeLabelPositionResult = _barcodeLabelPosition.BottomCenter;
        private bool _showBarcodeLabelResult;
        private RotateFlipType _rotateFlip = RotateFlipType.RotateNoneFlipNone;
        private _textAlign _barcodeAlignMentResult = _textAlign.Center;
        private Color _foreColorResult;
        private Font _fontResult = MyLib._myGlobal._myFontFormDesigner;
        private string _showTextResult;

        #endregion

        #region ImageField Public Field

        public SMLReport._formReport._imageList __imageList;

        #endregion

        #region ImageField Property

        [Category("_SML")]
        [Description("กำหนดสีตัวอักษร")]
        [DisplayName("Fore Color : กำหนดสีตัวอักษร")]
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

        [Category("Barcode Option")]
        [Description("การจัดวาง Barcode ")]
        [DisplayName("การจัดวาง Barcode ")]
        [DefaultValue(_textAlign.Center)]
        public _textAlign _barcodeAlignment
        {
            get
            {
                return _barcodeAlignMentResult;
            }
            set
            {
                _barcodeAlignMentResult = value;
            }
        }

        [Category("_SML")]
        [Description("การหมุนและกลับด้าน")]
        [DisplayName("Rotate Flip : การหมุนและกลับด้าน")]
        [DefaultValue(RotateFlipType.RotateNoneFlipNone)]
        public RotateFlipType RotateFlip
        {
            get
            {
                return _rotateFlip;
            }
            set
            {
                _rotateFlip = value;
            }
        }

        [Category("Barcode Option")]
        [Description("แสดงข้อความใน Barcode")]
        [DisplayName("Show Label : แสดงข้อความ")]
        [DefaultValue(false)]
        public bool _showBarcodeLabel
        {
            get
            {
                return _showBarcodeLabelResult;
            }
            set
            {
                _showBarcodeLabelResult = value;
            }
        }

        [Category("Barcode Option")]
        [Description("การวางตำแหน่งของข้อความ")]
        [DisplayName("Barcode Label Position")]
        [DefaultValue(_barcodeLabelPosition.BottomCenter)]
        public _barcodeLabelPosition _barcodeLabelPosition
        {
            get
            {
                return _barcodeLabelPositionResult;
            }
            set
            {
                _barcodeLabelPositionResult = value;
            }
        }

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

        [Category("_SML")]
        [Description("Always Show")]
        [DisplayName("Always Show : ")]
        [DefaultValue(false)]
        public bool _alwaysShow
        {
            get
            {
                return _alwaysShowResult;
            }
            set
            {
                _alwaysShowResult = value;
            }
        }

        [Category("Data")]
        [Description("กำหนดข้อความที่จะให้แสดงผล")]
        [DisplayName("Show Text : ")]
        public string _showText
        {
            get
            {
                return _showTextResult;
            }
            set
            {
                _showTextResult = value;
            }
        }

        [Category("Data")]
        [Description("ฟิลด์ข้อมูล")]
        [DisplayName("Field : ข้อมูล")]
        [TypeConverter(typeof(_fieldConverter))]
        public string Field
        {
            get
            {
                return _fieldProperty;
            }
            set
            {
                _fieldProperty = value;
            }
        }

        [Category("Data")]
        [Description("ฟิลด์ข้อมูล")]
        [DisplayName("Type : ประเภท")]
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
        [Description("การวางภาพในกรอบ")]
        [DisplayName("Size Mode : การวางภาพในกรอบ")]
        [DefaultValue(PictureBoxSizeMode.StretchImage)]
        public PictureBoxSizeMode SizeMode
        {
            get
            {
                return _sizeMode;
            }
            set
            {
                _sizeMode = value;
            }
        }

        [Category("Data")]
        [Description("รูปภาพ")]
        [DisplayName("ImageName : ชื่อภาพ")]
        [TypeConverter(typeof(SMLReport._imageListFieldConverter))]
        public string _imageObjectName
        {
            get
            {
                return _imageObjectNameResult;
            }

            set
            {
                _imageObjectNameResult = value;
            }
        }

        [Category("Data")]
        [Description("เงื่อนไขการแสดง")]
        [DisplayName("Compare Value : ข้อมูล")]
        public string _fieldCompareValue
        {
            get
            {
                return _fieldCompareValueResult;
            }

            set
            {
                _fieldCompareValueResult = value;
            }
        }

        [Category("_SML")]
        [Description("กำหนดลักษณะเส้น")]
        [DisplayName("LineStyle : กำหนดลักษณะเส้น")]
        public DashStyle _LineStyle
        {
            get
            {
                return _lineStyleResult;
            }

            set
            {
                _lineStyleResult = value;
            }
        }

        [Category("Data")]
        [Description("เลือกชุด query")]
        [DisplayName("DataQuery")]
        public SMLReport._formReport._queryRule query
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

        #endregion

        #region ImageField Constructor

        public _drawImageField()
        {
            SetRectangle(0, 0, 1, 1, _drawScale);
            _initialize();
            _lineColor = Color.Transparent;
            _foreColor = Color.Black;
        }

        public _drawImageField(float scale)
        {
            SetRectangle(0, 0, 1, 1, scale);
            _initialize();
            _lineColor = Color.Transparent;
            _foreColor = Color.Black;
        }

        public _drawImageField(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            _rectangleResult.X = (int)(x / _drawScale);
            _rectangleResult.Y = (int)(y / _drawScale);
            _rectangleResult.Width = width;
            _rectangleResult.Height = height;
            _initialize();
            _lineColor = Color.Transparent;
            _foreColor = Color.Black;
        }

        #endregion

        #region ImageField Methods

        public override void _draw(Graphics g)
        {
            _draw(g, new PointF(0, 0));
        }

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public void _draw(Graphics g, PointF __point)
        {
            Pen pen = new Pen((this._lineColor == Color.Transparent) ? Color.LightGray : this._lineColor, this._penWidth * _drawScale);
            pen.DashStyle = (this._lineColor == Color.Transparent) ? System.Drawing.Drawing2D.DashStyle.DashDot : _LineStyle;
            SolidBrush __bgbrush = new SolidBrush(this._backColor);
            SolidBrush __brush = new SolidBrush(this._foreColor);

            RectangleF __drawStrRect = _drawRectangle._getNormalizedRectangle(Size);
            __drawStrRect.X += __point.X;
            __drawStrRect.Y += __point.Y;

            g.FillRectangle(__bgbrush, Rectangle.Round(__drawStrRect));
            g.DrawRectangle(pen, Rectangle.Round(__drawStrRect));

            if (_showText != "")
            {
                SizeF __tmpSize = _getTextSize(_showText, _font);
                PointF __tmpPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, __tmpSize.Width, __tmpSize.Height, ContentAlignment.MiddleCenter);
                g.DrawString(_showText, _font, __brush, __drawStrRect.X + __tmpPoint.X, __drawStrRect.Y + __tmpPoint.Y, StringFormat.GenericTypographic);
            }
            else
            {
                if (this._fieldType == _FieldType.Barcode)
                {
                    Image __image = global::SMLReport.Resource16x16.ps_barcode4;
                    //_getBarcodeImage(__sampleText, Rectangle.Round(__drawStrRect).Size, _barcodeAlignment, _typeBarcode, _showBarcodeLabel, _barcodeLabelPosition, _font, RotateFlip, _foreColor, _backColor);
                    g.DrawImage(__image, Rectangle.Round(__drawStrRect));
                }
                else if (_imageObjectName != null && __imageList != null)
                {
                    Image __image = global::SMLReport.Resource16x16.icon_picture; //__imageList._getImageFromName(_imageObjectName);

                    if (__image != null)
                    {
                        //__image = _drawImage._SizeModeImg(Rectangle.Round(__drawStrRect), __image, SizeMode);
                        g.DrawImage(__image, Rectangle.Round(__drawStrRect));
                    }
                }
            }

            pen.Dispose();
            __bgbrush.Dispose();
        }

        public Image _getBarcodeImage(string __CodeStr, Size __size, _textAlign __barcodeAlign, _barcodeType __type, bool __showlabel, _barcodeLabelPosition __labelPosition, Font __myFont, RotateFlipType __flipType, Color __foreColoe, Color __backColor)
        {
            Image __barcodeImage = new Bitmap(__size.Width, __size.Height);

            // alignment
            BarcodeLib.AlignmentPositions __alignMent = BarcodeLib.AlignmentPositions.CENTER;
            if (__barcodeAlign == _textAlign.Left)
                __alignMent = BarcodeLib.AlignmentPositions.LEFT;
            if (__barcodeAlign == _textAlign.Right)
                __alignMent = BarcodeLib.AlignmentPositions.RIGHT;

            // types
            BarcodeLib.TYPE __barcodeType = BarcodeLib.TYPE.UNSPECIFIED;
            switch (__type)
            {
                case _barcodeType.BarCode_EAN13 :
                    __barcodeType = BarcodeLib.TYPE.EAN13;
                    break;
                case _barcodeType.BarCode_EAN8 : 
                    __barcodeType = BarcodeLib.TYPE.EAN8;
                    break;
                case _barcodeType.BarCode_2Of5 :
                     __barcodeType = BarcodeLib.TYPE.Standard2of5;
                   break;
                case _barcodeType.BarCode_Code39 :
                    __barcodeType = BarcodeLib.TYPE.CODE39;
                    break;
                case _barcodeType.BarCode_Code128 :
                    __barcodeType = BarcodeLib.TYPE.CODE128;
                    break;
            }

            BarcodeLib.Barcode __barcode = new BarcodeLib.Barcode();
            __barcode.IncludeLabel = __showlabel;
            __barcode.LabelFont = __myFont;
            __barcode.Alignment = __alignMent;

            __barcode.RotateFlipType = __flipType;

            //label alignment and position
            switch (__labelPosition)
            {
                case _design._barcodeLabelPosition.BottomLeft: __barcode.LabelPosition = BarcodeLib.LabelPositions.BOTTOMLEFT; break;
                case _design._barcodeLabelPosition.BottomRight: __barcode.LabelPosition = BarcodeLib.LabelPositions.BOTTOMRIGHT; break;
                case _design._barcodeLabelPosition.TopCenter: __barcode.LabelPosition = BarcodeLib.LabelPositions.TOPCENTER; break;
                case _design._barcodeLabelPosition.TopLeft: __barcode.LabelPosition = BarcodeLib.LabelPositions.TOPLEFT; break;
                case _design._barcodeLabelPosition.TopRight: __barcode.LabelPosition = BarcodeLib.LabelPositions.TOPRIGHT; break;
                default: __barcode.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER; break;
            }//switch

            try
            {
                __barcodeImage = __barcode.Encode(__barcodeType, __CodeStr, __foreColoe, __backColor, __size.Width, __size.Height);
            }
            catch
            {
            }
            return __barcodeImage;
        }

        public override void _drawFromPoint(Graphics g, PointF __point, float __drawScale)
        {
            this._drawScale = __drawScale;
            _draw(g, __point);
        }

        protected void SetRectangle(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            _rectangleResult.X = (int)(x / _drawScale);
            _rectangleResult.Y = (int)(y / _drawScale);
            _rectangleResult.Width = (int)(width / _drawScale);
            _rectangleResult.Height = (int)(height / _drawScale);
        }

        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int _handleCount
        {
            get
            {
                return 8;
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point _getHandle(int handleNumber)
        {
            int x, y, xCenter, yCenter;

            xCenter = Size.X + Size.Width / 2;
            yCenter = Size.Y + Size.Height / 2;
            x = Size.X;
            y = Size.Y;

            switch (handleNumber)
            {
                case 1:
                    x = Size.X;
                    y = Size.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = Size.Y;
                    break;
                case 3:
                    x = Size.Right;
                    y = Size.Y;
                    break;
                case 4:
                    x = Size.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = Size.Right;
                    y = Size.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = Size.Bottom;
                    break;
                case 7:
                    x = Size.X;
                    y = Size.Bottom;
                    break;
                case 8:
                    x = Size.X;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);

        }

        public override Point _getHandleNoScale(int handleNumber)
        {
            int x, y, xCenter, yCenter;

            xCenter = Size.X + Size.Width / 2;
            yCenter = Size.Y + Size.Height / 2;
            x = Size.X;
            y = Size.Y;

            switch (handleNumber)
            {
                case 1:
                    x = (int)(Size.X / _drawScale);
                    y = (int)(Size.Y / _drawScale);
                    break;
                case 2:
                    x = xCenter;
                    y = Size.Y;
                    break;
                case 3:
                    x = Size.Right;
                    y = Size.Y;
                    break;
                case 4:
                    x = Size.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = (int)(Size.Right / _drawScale);
                    y = (int)(Size.Bottom / _drawScale);
                    break;
                case 6:
                    x = xCenter;
                    y = Size.Bottom;
                    break;
                case 7:
                    x = Size.X;
                    y = Size.Bottom;
                    break;
                case 8:
                    x = Size.X;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);

        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int _hitTest(Point point)
        {
            if (_selected)
            {
                for (int i = 1; i <= _handleCount; i++)
                {
                    if (_getHandleRectangle(i).Contains(point))
                        return i;
                }
            }

            if (_pointInObject(point))
                return 0;

            return -1;
        }

        protected override bool _pointInObject(Point point)
        {
            return Size.Contains(point);
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Cursor _getHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Default;
            }
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void _moveHandleTo(Point point, int handleNumber)
        {
            int left = Size.Left;
            int top = Size.Top;
            int right = Size.Right;
            int bottom = Size.Bottom;

            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }
            SetRectangle(left, top, right - left, bottom - top, _drawScale);
        }

        public override bool _intersectsWith(Rectangle rectangle)
        {
            return Size.IntersectsWith(rectangle);
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void _move(int deltaX, int deltaY)
        {
            _rectangleResult.X += (int)(deltaX / _drawScale);
            _rectangleResult.Y += (int)(deltaY / _drawScale);
            //_rectangle.X += (int)(deltaX / _drawScale);
            //_rectangle.Y += (int)(deltaY / _drawScale);
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void _moveToPoint(Point newPoint)
        {
            //_rectangleResult.X += (int)(newPoint.X / _drawScale);
            //_rectangleResult.Y += (int)(newPoint.Y / _drawScale);
            _rectangleResult.X = (int)(newPoint.X / _drawScale);
            _rectangleResult.Y = (int)(newPoint.Y / _drawScale);
            //_rectangle.X = (int)(newPoint.X / _drawScale);
            //_rectangle.Y = (int)(newPoint.Y / _drawScale);
        }

        public override void _dump()
        {
            base._dump();
        }

        /// <summary>
        /// Normalize rectangle
        /// </summary>
        public override void _normalize()
        {
            //_rectangle = _drawRectangle.GetNormalizedRectangle(Rectangle);
            if (this._rectangleResult.Height < 0)
            {
                this._rectangleResult.Y -= Math.Abs(this._rectangleResult.Height);
                this._rectangleResult.Height = Math.Abs(this._rectangleResult.Height);
            }

            if (this._rectangleResult.Width < 0)
            {
                this._rectangleResult.X -= Math.Abs(this._rectangleResult.Width);
                this._rectangleResult.Width = Math.Abs(this._rectangleResult.Width);
            }
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

        public event _getImageListName GetNameImageList;

        #endregion

        #region Save Load From Stream
        /// <summary>
        /// Save objevt to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public override void _saveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel, orderNumber),
                _rectangleResult);

            //info.AddValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryText, orderNumber),
            //    _textProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryBackColor, orderNumber),
                _backColor.ToArgb());

            //info.AddValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryLineStyle, orderNumber),
            //    _lineStyle);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryForeColor, orderNumber),
                _foreColorResult.ToArgb());

            //info.AddValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryAutoSize, orderNumber),
            //    _autoSizeProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryTextAlign, orderNumber),
                _barcodeAlignment);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryFont, orderNumber),
                _fontResult);

            //info.AddValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryAllowLineBreak, orderNumber),
            //    _allowLineBreak);

            //info.AddValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryCharSpace, orderNumber),
            //    _charSpace);

            //info.AddValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryCharWidth, orderNumber),
            //    _charWidth);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryFieldType, orderNumber),
                FieldType);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryDataQuery, orderNumber),
                query);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryShowText, orderNumber),
                _showText);

            base._saveToStream(info, orderNumber);
        }

        /// <summary>
        /// LOad object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public override void _loadFromStream(SerializationInfo info, int orderNumber)
        {
            _rectangleResult = (Rectangle)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel, orderNumber),
                typeof(Rectangle));

            //_textProperty = (string)info.GetValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryText, orderNumber),
            //    typeof(string));

            int tmpbackcolor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryBackColor, orderNumber));
            _backColor = __getColorFromInt32(tmpbackcolor);

            //_lineStyle = (DashStyle)info.GetValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryLineStyle, orderNumber),
            //    typeof(DashStyle));

            int tmpForeColor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryForeColor, orderNumber));
            _foreColorResult = __getColorFromInt32(tmpForeColor);

            //_autoSizeProperty = (bool)info.GetValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryAutoSize, orderNumber),
            //    typeof(bool));

            _barcodeAlignment = (_textAlign)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryTextAlign, orderNumber),
                typeof(_textAlign));

            _fontResult = (Font)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryFont, orderNumber),
                typeof(Font));

            //_allowLineBreak = (bool)info.GetValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryAllowLineBreak, orderNumber),
            //    typeof(bool));

            //_charSpace = (float)info.GetValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryCharSpace, orderNumber),
            //    typeof(float));

            //_charWidth = (float)info.GetValue(
            //    string.Format(CultureInfo.InvariantCulture,
            //    "{0}{1}",
            //    _entryLabel + _entryCharWidth, orderNumber),
            //    typeof(float));

            FieldType = (_FieldType)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryFieldType, orderNumber),
                typeof(_FieldType));

            query = (_formReport._queryRule)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryDataQuery, orderNumber),
                typeof(_formReport._queryRule));

            _showText = (string)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryShowText, orderNumber),
                typeof(string));

            base._loadFromStream(info, orderNumber);
        }

        #endregion

    }

    public delegate void _getImageListName(_drawObject sender);
}
