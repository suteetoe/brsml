using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections;

namespace SMLReport._design
{
    /// <summary>
    /// Base class for all draw objects
    /// </summary>
    [Serializable]
    public abstract class _drawObject : IDisposable
    {
        private string[] _defaultFieldValueResult;
        private string _printPageResult = "All";
        private bool _absolutePositionResult = true;

        #region BaseObject Fields

        // Object properties
        private bool _selectedResult;
        private Color _lineColorResult;
        private int _penWidthResult;
        private float _drawScaleResult = 1f;

        private Color _backColorResult;

        // Last used property values (may be kept in the Registry)
        private static Color _lastUsedColorResult = Color.Black;
        private static Color _lastUsedBackColorResult = Color.Transparent;
        private static int _lastUsedPenWidthResult = 1;

        // Entry names for serialization
        private const string entryColor = "Color";
        private const string entryBackColor = "BackColor";
        private const string entryPenWidth = "PenWidth";
        private const string entryLock = "Lock";

        protected Rectangle _rectangleResult;
        private bool _lockResult = false;

        /// <summary>เก็บ owner (formdesign.cs)</summary>
        public object _Owner;
        #endregion

        #region BaseObject Property

        [Category("_SML")]
        [Description("ล๊อค ไม่ให้เคลื่อนย้ายไปไหนได้")]
        [DisplayName("Lock : ")]
        [DefaultValue(false)]
        public bool _lock
        {
            get
            {
                return _lockResult;
            }
            set
            {
                _lockResult = value;
            }
        }

        [Category("_SML")]
        [Description("รูปแบบการจัดวางตำแหน่งโดยนับจากตำแหน่งของขอบฟอร์ม")]
        [Browsable(false)]
        [DefaultValue(true)]
        public bool _absolutePosition
        {
            get
            {
                return _absolutePositionResult;
            }
            set
            {
                _absolutePositionResult = value;
            }
        }

        [Category("PrintOption")]
        [Description("การพิมพ์ All : ทุกหน้า, \nOdd : หน้าคู่, Even : หน้าคี่, \nFirst : หน้าแรก, Last : หน้าสุดท้าย, \nOddTotal : หน้าคู่โดยคิดจากจำนวนหน้าทั้งหมด, EvenTotal : หน้าคี่โดยคิดจากจำนวนหน้าทั้งหมด \nFirstTotal : หน้าแรกโดยคิดจากจำนวนหน้าทั้งหมด, LastTotal : หน้าสุดท้ายโดยคิดจากจำนวนหน้าทั้งหมด \nหรือระบุเลขหน้า")]
        [DisplayName("PrintPage : กำหนดหน้าการพิมพ์")]
        [DefaultValue(typeof(string), "All")]
        public string PringPage
        {
            get
            {
                return _printPageResult;
            }
            set
            {
                _printPageResult = value;
            }
        }

        [Browsable(false)]
        public string[] _defaultField
        {
            get
            {
                return _defaultFieldValueResult;
            }

            set
            {
                _defaultFieldValueResult = value;
            }
        }

        [Browsable(false)]
        public Rectangle Size
        {
            get
            {
                Rectangle __newRectangle = _rectangleResult;
                __newRectangle.X = (int)(_rectangleResult.X * _drawScale);
                __newRectangle.Y = (int)(_rectangleResult.Y * _drawScale);
                __newRectangle.Width = (int)(_rectangleResult.Width * _drawScale);
                __newRectangle.Height = (int)(_rectangleResult.Height * _drawScale);
                return __newRectangle;
            }
        }

        /// <summary>Get or Set กรอบ</summary>
        [Category("_SML")]
        [Description("การจัดวาง")]
        [DisplayName("Location, Size : กรอบ")]
        public virtual Rectangle _actualSize
        {
            get
            {
                Rectangle __newRectangle = _rectangleResult;
                __newRectangle.X = _rectangleResult.X;
                __newRectangle.Y = _rectangleResult.Y;
                __newRectangle.Width = _rectangleResult.Width;
                __newRectangle.Height = _rectangleResult.Height;
                return __newRectangle;
            }

            set
            {
                _rectangleResult = value;
            }
        }

        /// <summary>
        /// Selection flag
        /// </summary>
        [Browsable(false)]
        public virtual bool _selected
        {
            get
            {
                return _selectedResult;
            }
            set
            {
                _selectedResult = value;
            }
        }

        [Browsable(false)]
        public virtual float _drawScale
        {
            get
            {
                return _drawScaleResult;
            }
            set
            {
                _drawScaleResult = value;
            }
        }

        /// <summary>
        /// กำหนดสีเส้น (Color)
        /// </summary>
        /// 
        [Category("_SML")]
        [Description("กำหนดสีเส้น")]
        [DisplayName("Line Color : กำหนดสีเส้น")]
        public Color _lineColor
        {
            get
            {
                return _lineColorResult;
            }
            set
            {
                _lineColorResult = value;
            }
        }

        /// <summary>
        /// กำหนดสีพื้นหลัง (Back Color)
        /// </summary>
        [Category("_SML")]
        [Description("กำหนดสีพื้นหลัง")]
        [DisplayName("Back Color : กำหนดสีพื้นหลัง")]
        public Color _backColor
        {
            get
            {
                return _backColorResult;
            }
            set
            {
                _backColorResult = value;
            }
        }

        /// <summary>
        /// กำหนดขนาดเส้น (Pen width)
        /// </summary>
        [Category("_SML")]
        [Description("กำหนดขนาดเส้น")]
        [DisplayName("Pen Width : กำหนดขนาดเส้น")]
        [DefaultValue(1)]
        public int _penWidth
        {
            get
            {
                return _penWidthResult;
            }
            set
            {
                _penWidthResult = value;
            }
        }

        /// <summary>
        /// Number of handles
        /// </summary>
        [Browsable(false)]
        public virtual int _handleCount
        {
            get
            {
                return 0;
            }

        }

        /// <summary>ความกว้าง</summary>
        [DisplayName("Width : ความกว้าง")]
        public virtual int _width
        {
            get
            {
                return _actualSize.Width;
            }
            set
            {
                this._rectangleResult.Width = value;
            }
        }

        /// <summary>ความสูง</summary>
        [DisplayName("Height : ความสูง")]
        public virtual int _height
        {
            get
            {
                return _actualSize.Height;
            }
            set
            {
                this._rectangleResult.Height = value;
            }
        }

        [DisplayName("X :")]
        public virtual int X
        {
            get { return _actualSize.X; }
            set { this._rectangleResult.X = value; }
        }

        [DisplayName("Y :")]
        public virtual int Y
        {
            get { return _actualSize.Y; }
            set { this._rectangleResult.Y = value; }
        }

        /// <summary>
        /// Last used color
        /// </summary>
        public static Color LastUsedColor
        {
            get
            {
                return _lastUsedColorResult;
            }
            set
            {
                _lastUsedColorResult = value;
            }
        }

        /// <summary>
        /// Last used pen width
        /// </summary>
        public static int _lastUsedPenWidth
        {
            get
            {
                return _lastUsedPenWidthResult;
            }
            set
            {
                _lastUsedPenWidthResult = value;
            }
        }

        #endregion

        #region BaseObject Methods

        /// <summary>
        /// Draw object
        /// </summary>
        /// <param name="g"></param>
        public virtual void _draw(Graphics g)
        {
        }

        /// <summary>
        /// Draw Object ( Fix Start Point )
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__point"></param>
        public virtual void _drawFromPoint(Graphics g, PointF __point, float __drawScale)
        {
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Point _getHandle(int handleNumber)
        {
            return new Point(0, 0);
        }

        public virtual Point _getHandleNoScale(int handleNumber)
        {
            return new Point(0, 0);
        }

        /// <summary>
        /// Get handle rectangle by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Rectangle _getHandleRectangle(int handleNumber)
        {
            Point point = _getHandle(handleNumber);

            return new Rectangle(point.X - 3, point.Y - 3, 7, 7);
        }

        public virtual Rectangle _getHandleRectangleNoScale(int handleNumber)
        {
            Point point = _getHandleNoScale(handleNumber);

            return new Rectangle(point.X, point.Y, 0, 0);
        }

        /// <summary>
        /// Draw tracker for selected object
        /// </summary>
        /// <param name="g"></param>
        public virtual void _drawTracker(Graphics g)
        {
            if (!_selected)
                return;

            Pen __pen = new Pen(Color.Black, 0);
            SolidBrush __brush = new SolidBrush(Color.Lime);

            for (int i = 1; i <= _handleCount; i++)
            {
                g.FillRectangle(__brush, _getHandleRectangle(i));
                g.DrawRectangle(__pen, _getHandleRectangle(i));
            }
            __pen.Dispose();
            __brush.Dispose();
        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual int _hitTest(Point point)
        {
            return -1;
        }


        /// <summary>
        /// Test whether point is inside of the object
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected virtual bool _pointInObject(Point point)
        {
            return false;
        }

        /// <summary>
        /// Get curesor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Cursor _getHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public virtual bool _intersectsWith(Rectangle rectangle)
        {
            return false;
        }

        /// <summary>
        /// Move object (Mouse Move
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public virtual void _move(int deltaX, int deltaY)
        {
        }

        /// <summary>
        /// Move object (Mouse Move
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public virtual void _moveToPoint(Point newPoint)
        {
        }

        /// <summary>
        /// Move handle to the point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public virtual void _moveHandleTo(Point point, int handleNumber)
        {
        }

        /// <summary>
        /// Dump (for debugging)
        /// </summary>
        public virtual void _dump()
        {
            Trace.WriteLine("");
            Trace.WriteLine(this.GetType().Name);
            Trace.WriteLine("Selected = " + _selectedResult.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Normalize object.
        /// Call this function in the end of object resizing.
        /// </summary>
        public virtual void _normalize()
        {
        }

        /// <summary>
        /// Save object to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public virtual void _saveToStream(SerializationInfo info, int orderNumber)
        {
            // add line color
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}",
                    entryColor, orderNumber),
                _lineColor.ToArgb());

            // add penwidth
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryPenWidth, orderNumber),
                _penWidth);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryLock, orderNumber),
                _lock);
        }

        /// <summary>
        /// Load object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public virtual void _loadFromStream(SerializationInfo info, int orderNumber)
        {
            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}",
                    entryColor, orderNumber));

            _lineColor = __getColorFromInt32(n);

            _penWidth = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryPenWidth, orderNumber));

            _lock = (bool)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryLock, orderNumber),
                typeof(bool));
        }

        /// <summary>
        /// Initialization
        /// </summary>
        protected void _initialize()
        {
            _lineColorResult = _lastUsedColorResult;
            _backColorResult = _lastUsedBackColorResult;
            _penWidthResult = _lastUsedPenWidth;
        }

        public object _clone()
        {
            return this.MemberwiseClone();
        }

        public static Color __getColorFromInt32(int n)
        {
            if (n == 16777215)
            {
                return Color.Transparent;
            }
            return Color.FromArgb(n);
        }

        /// <summary>
        /// Caluate New Point To Draw String alignment
        /// </summary>
        /// <param name="__x">Rectangle X</param>
        /// <param name="__y">Rectangle Y</param>
        /// <param name="__width">Rectangle Width</param>
        /// <param name="__height">Rectangle Height</param>
        /// <param name="__strWidth">String Width (After MeasureString)</param>
        /// <param name="__strHeight">String Height (After MeasureStrig)</param>
        /// <param name="__strAlignment">String Alignment Enum</param>
        /// <returns>Point on Rectangle (todrawonly)</returns>
        protected PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, _textAlign __strAlignment)
        {
            return _getPointTextAlingDraw(__width, __height, __strWidth, __strHeight, __strAlignment, new Padding(0));
        }

        protected PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, _textAlign __strAlignment, Padding __padding)
        {
            PointF __newDrawPoint = new PointF(0, 0);

            switch (__strAlignment)
            {
                case _textAlign.Left:
                    __newDrawPoint.X = 0 + (__padding.Left * _drawScale);
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case _textAlign.Center:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case _textAlign.Right:
                    __newDrawPoint.X = (__width - __strWidth) - (__padding.Right * _drawScale);
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;
            }

            return __newDrawPoint;
        }

        protected PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, ContentAlignment __strAlignment)
        {
            return _getPointTextAlingDraw(__width, __height, __strWidth, __strHeight, __strAlignment, new Padding(0));
        }

        protected PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, ContentAlignment __strAlignment, Padding __padding)
        {
            PointF __newDrawPoint = new PointF(0, 0);

            switch (__strAlignment)
            {
                case ContentAlignment.TopLeft:
                    __newDrawPoint.X = 0 + (__padding.Left * _drawScale);
                    break;

                case ContentAlignment.TopCenter:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    break;

                case ContentAlignment.TopRight:
                    __newDrawPoint.X = (__width - __strWidth) - (__padding.Right * _drawScale);
                    break;

                case ContentAlignment.MiddleLeft:
                    __newDrawPoint.X = 0 + (__padding.Left * _drawScale);
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case ContentAlignment.MiddleCenter:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case ContentAlignment.MiddleRight:
                    __newDrawPoint.X = (__width - __strWidth) - (__padding.Right * _drawScale);
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case ContentAlignment.BottomLeft:
                    __newDrawPoint.X = 0 + (__padding.Left * _drawScale);
                    __newDrawPoint.Y = (__height - __strHeight);
                    break;

                case ContentAlignment.BottomCenter:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    __newDrawPoint.Y = (__height - __strHeight);
                    break;

                case ContentAlignment.BottomRight:
                    __newDrawPoint.X = (__width - __strWidth) - (__padding.Right * _drawScale);
                    __newDrawPoint.Y = (__height - __strHeight);
                    break;
            }

            return __newDrawPoint;
        }

        protected SizeF _getTextSize(string __strMeasure, Font __Font)
        {
            SizeF _textSize = new SizeF();
            Control _tmpControl = new Control();
            Graphics g = _tmpControl.CreateGraphics();

            _textSize = g.MeasureString(__strMeasure, __Font, 0, StringFormat.GenericTypographic);

            return _textSize;
        }

        protected SizeF _getTextSize(ArrayList __strMeasureList, Font __Font)
        {
            SizeF _textSize = new SizeF();
            Control _tmpControl = new Control();
            Graphics g = _tmpControl.CreateGraphics();

            foreach (string __strMeasure in __strMeasureList)
            {
                SizeF __strSize = g.MeasureString(__strMeasure, __Font, 0, StringFormat.GenericTypographic);
                _textSize.Height += __strSize.Height;

                if (_textSize.Width < __strSize.Width)
                {
                    _textSize.Width = __strSize.Width;
                }
            }

            return _textSize;
        }

        public StringFormat _getStringFormat(ContentAlignment __alignMent)
        {
            StringFormat __sf = StringFormat.GenericTypographic;

            switch (__alignMent)
            {
                case ContentAlignment.TopLeft:
                    __sf.Alignment = StringAlignment.Near;
                    __sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopCenter:
                    __sf.Alignment = StringAlignment.Center;
                    __sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopRight:
                    __sf.Alignment = StringAlignment.Far;
                    __sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                    __sf.Alignment = StringAlignment.Near;
                    __sf.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleCenter:
                    __sf.Alignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleRight:
                    __sf.Alignment = StringAlignment.Far;
                    __sf.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomLeft:
                    __sf.Alignment = StringAlignment.Near;
                    __sf.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomCenter:
                    __sf.Alignment = StringAlignment.Center;
                    __sf.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomRight:
                    __sf.Alignment = StringAlignment.Far;
                    __sf.LineAlignment = StringAlignment.Far;
                    break;
            }

            return __sf;
        }

        public RectangleF _getRectangleFPadding(RectangleF __rect, Padding __padding)
        {
            __rect.X += __padding.Left;
            __rect.Y += __padding.Top;

            __rect.Width -= (__padding.Left + __padding.Right);
            __rect.Height -= (__padding.Top + __padding.Bottom);

            return __rect;
        }

        #endregion


        public virtual string[] _getFieldStandardValue(_drawObject sender)
        {
            if (_onGetStandardValue != null)
            {
                _onGetStandardValue(sender);
            }

            if (this._defaultFieldValueResult != null)
                return this._defaultFieldValueResult;

            return null;
        }

        public virtual void Dispose()
        {

        }

        public event OnGetStandardValue _onGetStandardValue;
    }

    public delegate void OnGetStandardValue(_drawObject sender);
}
