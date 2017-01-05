using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections;


namespace SMLReport._design
{
    /// <summary>
    /// Rectangle graphic object
    /// </summary>
    [Serializable]
    public class _drawLabel : _drawObject
    {
        public delegate void BeforeDrawEventHandler(Graphics g, PointF __startPoint);
        public event BeforeDrawEventHandler _beforeDraw;

        #region Label Field

        private const string _entryLabel = "Label";
        private const string _entryText = "Text";
        private const string _entryBackColor = "BackColor";
        private const string _entryLineStyle = "LineStyle";
        private const string _entryForeColor = "ForeColor";
        private const string _entryAutoSize = "AutoSize";
        private const string _entryTextAlign = "Align";
        private const string _entryFont = "Font";
        private const string _entryAllowLineBreak = "AllowLineBreak";
        private const string _entryCharSpace = "CharSpace";
        private const string _entryCharWidth = "CharWidth";

        private Font _fontResult = MyLib._myGlobal._myFontFormDesigner;
        private ContentAlignment _textAlignResult = ContentAlignment.MiddleLeft;
        public string _textProperty = "Label";
        private Color _foreColorResult;
        private DashStyle _LineStyleResult;
        private bool _autoSizeProperty = true;
        private Padding _paddingResult = new Padding(4, 0, 4, 0);
        private float charSpaceResult = 0f;
        private float _charWidthResult = -1;
        private bool _allowLineBreakResult = true;

        protected SizeF _textSizeResult = new SizeF();
        protected PointF _textDrawPoint = new PointF();
        private overFlowType _overFlowResult = overFlowType.NewLine;

        #endregion

        #region Label Property

        public overFlowType _overFlow
        {
            get
            {
                return _overFlowResult;
            }
            set
            {
                _overFlowResult = value;
            }
        }

        [Description("ความกว้างตัวอักษรวัดจาก Font ปัจจุบันที่เลือกไว้ โดยวัดจาก อักษร X")]
        [DisplayName("Font Char Width")]
        public float _FontCharWidth
        {
            get
            {
                Font __newFont = new Font(_font.FontFamily, (float)(_font.Size * _drawScale), _font.Style, _font.Unit, _font.GdiCharSet, _font.GdiVerticalFont);
                return _getTextSize("X", __newFont).Width;
            }
        }

        [Category("_SML")]
        [Description("กำหนดขนาดความกว้างของตัวอักษร")]
        [DisplayName("CharWidth : ตัวอักษรกว้าง")]
        public float _charWidth
        {
            get
            {
                return _charWidthResult;
            }
            set
            {
                _charWidthResult = value;
            }
        }

        [Category("_SML")]
        [Description("")]
        [DisplayName("AllowLineBreak")]
        public bool _allowLineBreak
        {
            get
            {
                return _allowLineBreakResult;
            }

            set
            {
                _allowLineBreakResult = value;
            }
        }

        [Category("_SML")]
        [Description("ระยะห่างระหว่างตัวอักษร")]
        [DisplayName("CharSpace : ระยะห่างตัวอักษร")]
        public float _charSpace
        {
            get
            {
                return charSpaceResult;
            }
            set
            {
                charSpaceResult = value;
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

        /// <summary>กำหนดข้อความ</summary>
        [Category("Data")]
        [Description("ข้อความ")]
        [DisplayName("Text : ข้อความ")]
        public string _text
        {
            set
            {
                this._textProperty = value;
                _setTextSize();
            }
            get
            {
                return this._textProperty;
            }
        }

        [Category("_SML")]
        [Description("การจัดวาง")]
        [DisplayName("Text Align : การจัดวางตำแหน่ง")]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment _textAlign
        {
            get
            {
                return _textAlignResult;
            }
            set
            {
                _textAlignResult = value;
            }
        }

        public override int _width
        {
            get
            {
                return (Size.Width);
            }
        }

        public override int _height
        {
            get
            {
                return (Size.Height);
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
                _setTextSize();
            }
        }

        /// <summary>สีตัวอักษร</summary>
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
        [Description("กำหนดลักษณะเส้น")]
        [DisplayName("LineStyle : กำหนดลักษณะเส้น")]
        public DashStyle _lineStyle
        {
            get
            {
                return _LineStyleResult;
            }

            set
            {
                _LineStyleResult = value;
            }
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

        [Category("_SML")]
        [Description("ปรับขนาดของข้อความอัตโนมัติ")]
        [DisplayName("AutoSize : ปรับขนาดอัตโนมัติ")]
        public bool _autoSize
        {
            get
            {
                return _autoSizeProperty;
            }
            set
            {
                _autoSizeProperty = value;
            }
        }

        #endregion

        #region Label Constructor

        public _drawLabel()
        {
            this._autoSize = false;
            _foreColorResult = Color.Black;
            _LineStyleResult = DashStyle.Solid;
            _lineColor = Color.Transparent;
            _backColor = Color.Transparent;
            _setRectangle(0, 0, 1, 1, _drawScale);
            _setTextSize();
        }

        public _drawLabel(int x, int y, int width, int height, float scale)
        {
            this._autoSize = false;
            _drawScale = scale;
            //_txext1Result = _xtext2Result = _entryLabel;

            //_rectangleResult.X = (int)(x / _drawScale);
            //_rectangleResult.Y = (int)(y / _drawScale);
            //_rectangleResult.Width = width;
            //_rectangleResult.Height = height;

            _setRectangle(x, y, width, height, _drawScale);

            _initialize();
            _foreColorResult = Color.Black;
            _LineStyleResult = DashStyle.Solid;
            _lineColor = Color.Transparent;
            _backColor = Color.Transparent;
            _setTextSize();
        }



        #endregion

        #region Label Methods

        public virtual void _setTextSize()
        {
            _textSizeResult = this._getTextSize(this._textProperty, _fontResult);
        }

        private void _setDrawPoint()
        {
            RectangleF __drawStrRect = _drawRectangle._getNormalizedRectangle(Size);
            _textDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, _textSizeResult.Width, _textSizeResult.Height, this._textAlign, _padding);
        }

        public override void _draw(Graphics g)
        {
            _draw(g, new PointF(0, 0));
        }

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public void _draw(Graphics g, PointF __startPoint)
        {
            if (this._beforeDraw != null)
            {
                this._beforeDraw(g, __startPoint);
            }
            Pen pen = new Pen(this._lineColor, this._penWidth * _drawScale);
            pen.DashStyle = this._lineStyle;
            SolidBrush brush = new SolidBrush(this._foreColor);
            SolidBrush __BgBrush = new SolidBrush(this._backColor);

            Font __newFont = new Font(_font.FontFamily, (float)(_font.Size * _drawScale), _font.Style, _font.Unit, _font.GdiCharSet, _font.GdiVerticalFont);

            if (this._textProperty != "" && _autoSizeProperty)
            {
                SizeF __stringSize = _textSizeResult;
                //SizeF __tmp = g.MeasureString(__dataStr, __newFont, new PointF(0, 0), _getStringFormat(_textAlign));
                this._rectangleResult.Width = (int)Math.Ceiling((__stringSize.Width / _drawScale)) + _padding.Left + _padding.Right;
                this._rectangleResult.Height = (int)Math.Ceiling((__stringSize.Height / _drawScale)) + _padding.Top + _padding.Bottom;

            }

            RectangleF __drawStrRect = _drawRectangle._getNormalizedRectangle(Size);
            __drawStrRect.X += __startPoint.X;
            __drawStrRect.Y += __startPoint.Y;

            g.FillRectangle(__BgBrush, Rectangle.Round(__drawStrRect));
            g.DrawRectangle(pen, Rectangle.Round(__drawStrRect));

            if (this._textProperty != "")
            {
                string __dataStr = this._textProperty;


                PointF __strDrawPoint = __drawStrRect.Location;

                PointF __getDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, _textSizeResult.Width, _textSizeResult.Height, this._textAlign, _padding);
                g.DrawString(__dataStr, __newFont, brush, (__strDrawPoint.X + __getDrawPoint.X), (__strDrawPoint.Y + __getDrawPoint.Y), StringFormat.GenericTypographic);

                //g.DrawString(__dataStr, __newFont, brush, _getRectangleFPadding(__drawStrRect, _padding), _getStringFormat(_textAlign));

            }

            __newFont.Dispose();
            __BgBrush.Dispose();
            pen.Dispose();
            brush.Dispose();
        }

        public static string _replaceLineBreak(string __data, bool __allowLineBreak)
        {
            __data = __data.Replace("\\n", "\n");
            if (__allowLineBreak == false)
            {
                __data = __data.Replace("\n", " ");
            }

            return __data;
        }

        #region Static Label Method

        public static ArrayList _cutString(string text, Font font, float width)
        {
            return SMLReport._design._drawLabel._cutString(text, font, width, 0);
        }

        public static ArrayList _cutString(string text, Font font, float width, float __charSpace)
        {
            return SMLReport._design._drawLabel._cutString(text, font, width, __charSpace, -1);
        }

        public static ArrayList _cutString(string text, Font font, float width, float __charSpace, float __charWidth)
        {
            return SMLReport._design._drawLabel._cutString(text, font, width, __charSpace, __charWidth, new Padding(0, 0, 0, 0));
        }

        public static ArrayList _cutString(Graphics g, string text, Font font, float width, float __charSpace, float __charWidth, Padding __Padding)
        {
            ArrayList __result = new ArrayList();
            ArrayList __strList = _getStrLine(text);

            width -= (__Padding.Left + __Padding.Right);

            foreach (string __str in __strList)
            {
                try
                {
                    SizeF __stringSize = g.MeasureString(__str, font, 0, StringFormat.GenericTypographic);
                    char[] __charCount = __str.ToCharArray();

                    if (__charWidth != -1 && __charWidth != 0)
                    {
                        __stringSize.Width = (__charCount.Length * __charWidth);
                    }

                    __stringSize.Width += (__charSpace * (__charCount.Length - 1));

                    if (__stringSize.Width > width)
                    {
                        int __tailFirst = 0;
                        int __tail = 0;
                        int __lastCutPoint = -1;
                        int __lastThaiPoint = -1;
                        char __lastChar = ' ';

                        while (__tail < __str.Length)
                        {
                            char __getChar = __str[__tail];
                            if (__getChar <= ' ' || (__getChar >= ';' && __getChar <= '@') ||
                                    (__getChar >= 'ก' && __getChar <= 'ฮ' && __tail - __lastThaiPoint > 2 && __lastChar != 'า' && !(__lastChar >= 'เ' && __lastChar <= 'โ')) ||
                                    (__getChar >= 'เ' && __getChar <= 'โ') || (__getChar >= '0' && __getChar <= 'z' && __lastChar >= 'ก' && __lastChar <= 'ฮ'))
                            {
                                __lastCutPoint = __tail;
                                __lastThaiPoint = __lastCutPoint;
                            }
                            __lastChar = __str[__tail];
                            string __lastString = __str.Substring(__tailFirst, __tail - __tailFirst);
                            char[] __lastCharArray = __lastString.ToCharArray();

                            __stringSize = g.MeasureString(__lastString, font, 0, StringFormat.GenericTypographic);

                            if (__charWidth != -1 && __charWidth != 0)
                            {
                                __stringSize.Width = (__lastCharArray.Length * __charWidth);
                            }

                            if (__lastString.Length > 0)
                            {
                                __stringSize.Width += (__charSpace * (__lastCharArray.Length - 1));
                            }

                            if (__stringSize.Width > width)
                            {
                                if (__lastCutPoint == -1)
                                {
                                    __lastCutPoint = __tail;
                                    __lastThaiPoint = __lastCutPoint;
                                }
                                __result.Add(__str.Substring(__tailFirst, (__lastCutPoint - __tailFirst)));
                                __tailFirst = __lastCutPoint;
                                __lastCutPoint = -1;
                                __lastThaiPoint = -1;
                            }
                            __tail++;
                        }// while
                        if (__tailFirst != __lastCutPoint)
                        {
                            __result.Add(__str.Substring(__tailFirst, (__str.Length - __tailFirst)));
                        }
                    }
                    else
                    {
                        __result.Add(__str);
                    }

                }
                catch
                {
                    __result.Add(__str);
                }
            }

            return __result;
        }

        /// <summary>
        /// ตัดข้อความ ตามความกว้าง และ font ที่กำหนดให้
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <param name="__charSpace"></param>
        /// <param name="__charWidth"></param>
        /// <param name="__Padding"></param>
        /// <returns>ชุดข้อความ ArrayList</returns>
        public static ArrayList _cutString(string text, Font font, float width, float __charSpace, float __charWidth, Padding __Padding)
        {
            Control __form = new Control();
            Graphics g = __form.CreateGraphics();
            ArrayList __result = new ArrayList();

            __result = _cutString(g, text, font, width, __charSpace, __charWidth, __Padding);
            
            return __result;
        }

        public static ArrayList _getStrLine(string __data)
        {
            ArrayList __strList = new ArrayList();

            string[] __tmpData = __data.Split('\n');

            foreach (string __str in __tmpData)
            {
                __strList.Add(__str);
            }

            return __strList;
        }

        #endregion

        public override void _drawFromPoint(Graphics g, PointF __point, float __drawScale)
        {
            this._drawScale = __drawScale;
            _draw(g, __point);
        }


        protected virtual void _setRectangle(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            x = (int)(x / _drawScale);
            y = (int)(y / _drawScale);
            width = (int)(width / _drawScale);
            height = (int)(height / _drawScale);
            _rectangleResult.X = x;
            _rectangleResult.Y = y;
            _rectangleResult.Width = width;
            _rectangleResult.Height = height;
            //_setDrawPoint();
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point _getHandle(int handleNumber)
        {
            /*int x, y, xCenter, yCenter;

            xCenter = Rectangle.X + Rectangle.Width / 2;
            yCenter = Rectangle.Y + Rectangle.Height / 2;
            x = Rectangle.X;
            y = Rectangle.Y;

            if (handleNumber == 4)
            {
                x = Rectangle.Right;
                y = yCenter;
            }
            else
            {
                x = Rectangle.X;
                y = yCenter;
            }*/
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

            _setRectangle(left, top, right - left, bottom - top, _drawScale);
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
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void _moveToPoint(Point newPoint)
        {
            _rectangleResult.X = (int)(newPoint.X / _drawScale);
            _rectangleResult.Y = (int)(newPoint.Y / _drawScale);
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
            //_rectangle = _drawRectangle.GetNormalizedRectangle(_rectangle);
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

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryText, orderNumber),
                _textProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryBackColor, orderNumber),
                _backColor.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryLineStyle, orderNumber),
                _lineStyle);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryForeColor, orderNumber),
                _foreColorResult.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryAutoSize, orderNumber),
                _autoSizeProperty);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryTextAlign, orderNumber),
                _textAlignResult);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryFont, orderNumber),
                _fontResult);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryAllowLineBreak, orderNumber),
                _allowLineBreak);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryCharSpace, orderNumber),
                _charSpace);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryCharWidth, orderNumber),
                _charWidth);
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

            _textProperty = (string)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryText, orderNumber),
                typeof(string));

            int tmpbackcolor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryBackColor, orderNumber));
            _backColor = __getColorFromInt32(tmpbackcolor);

            _lineStyle = (DashStyle)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryLineStyle, orderNumber),
                typeof(DashStyle));

            int tmpForeColor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryForeColor, orderNumber));
            _foreColorResult = __getColorFromInt32(tmpForeColor);

            _autoSizeProperty = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryAutoSize, orderNumber),
                typeof(bool));

            _textAlignResult = (ContentAlignment)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryTextAlign, orderNumber),
                typeof(ContentAlignment));

            _fontResult = (Font)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryFont, orderNumber),
                typeof(Font));

            _allowLineBreak = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryAllowLineBreak, orderNumber),
                typeof(bool));

            _charSpace = (float)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryCharSpace, orderNumber),
                typeof(float));

            _charWidth = (float)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryLabel + _entryCharWidth, orderNumber),
                typeof(float));

            base._loadFromStream(info, orderNumber);
            _setTextSize();
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            // this._fontResult.Dispose();
        }
    }

    public enum _textAlign
    {
        Left,
        Center,
        Right
    }

    #region comment

    // draw mulitline
    /*
                for (int __line = 0; __line < __dataStrList.Count; __line++)
                {

                    SizeF __getDrawSize = _getTextSize((string)__dataStrList[__line], __newFont);


                    if (_charSpace != 0)
                    {
                        char[] __char = ((string)__dataStrList[__line]).ToCharArray();
                        __getDrawSize.Width += (_charSpace * (__char.Length - 1));

                        PointF __getDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, __getDrawSize.Width, __getDrawSize.Height, this._textAlign, _padding);

                        PointF __currentDrawCharPoint = new PointF();
                        for (int __i = 0; __i < __char.Length; __i++)
                        {
                            g.DrawString(__char[__i].ToString(), __newFont, brush, (__getDrawPoint.X + __strDrawPoint.X + __currentDrawCharPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                            SizeF __currentCharSize = _getTextSize(__char[__i].ToString(), __newFont);
                            __currentDrawCharPoint.X += (__currentCharSize.Width + _charSpace);
                        }
                    }
                    else
                    {
                        PointF __getDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, __getDrawSize.Width, __getDrawSize.Height, this._textAlign, _padding);

                        g.DrawString((string)__dataStrList[__line], __newFont, brush, (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);
                    }

                    __strDrawPoint.Y += __getDrawSize.Height;
                } 
                */

    //ArrayList __dataStrList = SMLReport._design._drawLabel._cutString(__dataStr, __newFont, this.Size.Width, _charSpace, _charWidth, _padding);

    #endregion
}
