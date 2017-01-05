using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace SMLReport._design
{
    /// <summary>
    /// Rectangle graphic object
    /// </summary>
    [Serializable]
    public class _drawRectangle : SMLReport._design._drawObject
    {
        #region Rectangle Fields

        private const string entryRectangle = "Rect";
        private const string _entryBackColor = "Backcolor";
        private const string _entryLineStyle = "LineStyle";
        private DashStyle _lineStyleResult;

        #endregion

        #region Rectangle Property

        public override int _width
        {
            get
            {
                return (_rectangleResult.Width);
            }
        }

        public override int _height
        {
            get
            {
                return (_rectangleResult.Height);
            }
        }

        //[Category("_SML")]
        //[Description("การจัดวาง")]
        //[DisplayName("Location, Size : กรอบ")]
        //public Rectangle Size
        //{
        //    get
        //    {
        //        Rectangle __newRectangle = _rectangle;
        //        __newRectangle.X = (int)(_rectangle.X * _drawScale);
        //        __newRectangle.Y = (int)(_rectangle.Y * _drawScale);
        //        __newRectangle.Width = (int)(_rectangle.Width * _drawScale);
        //        __newRectangle.Height = (int)(_rectangle.Height * _drawScale);
        //        return __newRectangle;
        //    }
        //    set
        //    {
        //        _rectangle = value;
        //    }
        //}

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

        #endregion

        #region Rectangle Constructor

        public _drawRectangle()
        {
            SetRectangle(0, 0, 1, 1, _drawScale);
            _initialize();
        }

        public _drawRectangle(float scale)
        {
            SetRectangle(0, 0, 1, 1, scale);
            _initialize();
        }

        public _drawRectangle(int x, int y, int width, int height, float scale)
        {
            SetRectangle(x, y, width, height, scale);

            //_drawScale = scale;

            //_rectangleResult.X = (int)(x / _drawScale);
            //_rectangleResult.Y = (int)(y / _drawScale);
            //_rectangleResult.Width = width;
            //_rectangleResult.Height = height;
            _initialize();
            this._lineStyleResult = DashStyle.Solid;
        }

        #endregion

        #region Rectangle Methods

        public override void _draw(Graphics g)
        {
            _draw(g, new PointF(0, 0));
        }

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public void _draw(Graphics g, PointF __startDrawPoint)
        {
            Pen pen = new Pen(this._lineColor, this._penWidth * (float)_drawScale);
            pen.DashStyle = this._lineStyleResult;
            SolidBrush brush = new SolidBrush(this._backColor);

            RectangleF __tmpRect = _drawRectangle._getNormalizedRectangle(Size);
            __tmpRect.X += __startDrawPoint.X;
            __tmpRect.Y += __startDrawPoint.Y;

            g.FillRectangle(brush, Rectangle.Round(__tmpRect));
            g.DrawRectangle(pen, Rectangle.Round(__tmpRect));

            pen.Dispose();
            brush.Dispose();
        }

        public override void _drawFromPoint(Graphics g, PointF __point, float __drawScale)
        {
            this._drawScale = __drawScale;
            _draw(g, __point);
        }

        protected virtual void SetRectangle(int x, int y, int width, int height, float scale)
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

        /// <summary>
        /// Normalization Rectangle ตรวจสอบความถูกต้อง ตำแหน่ง, ขนาด ของ Rectangle 
        /// </summary>
        /// <param name="x1">Point X</param>
        /// <param name="y1">Point Y</param>
        /// <param name="x2">Point X + Width</param>
        /// <param name="y2">Point Y + Height</param>
        /// <returns></returns>
        public static Rectangle _getNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if (y2 < y1)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Normalization Rectangle ตรวจสอบความถูกต้อง ตำแหน่ง, ขนาด ของ Rectangle 
        /// </summary>
        /// <param name="p1">Point 1 ( Top, Left)</param>
        /// <param name="p2">Point 2 ( Bottom, Right)</param>
        /// <returns></returns>
        public static Rectangle _getNormalizedRectangle(Point p1, Point p2)
        {
            return _getNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// Normalization Rectangle ตรวจสอบความถูกต้อง ตำแหน่ง, ขนาด ของ Rectangle 
        /// </summary>
        /// <param name="r">Rectangle Object</param>
        /// <returns></returns>
        public static Rectangle _getNormalizedRectangle(Rectangle r)
        {
            return _getNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }

        #endregion

        #region Save Load from stream

        /// <summary>
        /// Save objevt to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public override void _saveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            // add size
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryRectangle, orderNumber),
                Size);

            // add backcolor
            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryRectangle + _entryBackColor, orderNumber),
                _backColor.ToArgb());

            // add line 
            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryRectangle + _entryLineStyle, orderNumber),
                _LineStyle);

            base._saveToStream(info, orderNumber);
        }

        /// <summary>
        /// LOad object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public override void _loadFromStream(SerializationInfo info, int orderNumber)
        {
            _actualSize  = (Rectangle)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryRectangle, orderNumber),
                typeof(Rectangle));

            int tmpbackcolor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryRectangle + _entryBackColor, orderNumber));
            _backColor = __getColorFromInt32(tmpbackcolor);

            _LineStyle = (DashStyle)info.GetValue(
                string.Format("{0}{1}",
                entryRectangle + _entryLineStyle, orderNumber),
                typeof(DashStyle));

            base._loadFromStream(info, orderNumber);
        }

        #endregion

    }
}
