using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization;

namespace SMLReport._design
{
    /// <summary>
    /// draw Rounded Rectangle ( not define save and load to serialize redraw ด้วยนะ )
    /// </summary>
    public class _drawRoundedRectangle : SMLReport._design._drawObject
    {
        #region RoundedRectangle Fields

        private const string _entryRoundedRectangle = "RoundedRect";
        private const string _entryBackColor = "BackColor";
        private const string _entryLineStyle = "LineStyle";
        private const string _entryCornerRadius = "Corner";

        private DashStyle _lineStyleResult;
        private RoundedRectangleRadius _roundedRadiusProperty = new RoundedRectangleRadius(10);

        #endregion

        #region RoundedRectangle Property

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

        //[Category("_SML")]
        //[Description("การจัดวาง")]
        //[DisplayName("Location, Size : กรอบ")]
        //public Rectangle Size
        //{
        //    get
        //    {
        //        Rectangle __newRectangle = _rectangleResult;
        //        __newRectangle.X = (int)(_rectangleResult.X * _drawScale);
        //        __newRectangle.Y = (int)(_rectangleResult.Y * _drawScale);
        //        __newRectangle.Width = (int)(_rectangleResult.Width * _drawScale);
        //        __newRectangle.Height = (int)(_rectangleResult.Height * _drawScale);
        //        return __newRectangle;
        //    }
        //    set
        //    {
        //        _rectangleResult = value;
        //    }
        //}

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
        
        /// <summary>ระยะมุมโค้ง</summary>
        [Category("_SML")]
        [Description("การจัดวาง")]
        [DisplayName("CornorRadius : ขนาดของมุม")]
        [TypeConverter(typeof(_roundedRectangleRadiusConverter))]
        public RoundedRectangleRadius RoundedRadius
        {
            get
            {
                return _roundedRadiusProperty;
            }

            set
            {
                _roundedRadiusProperty = value;
            }
        }

        #endregion

        #region RouncedRectangle Constructor

        public _drawRoundedRectangle()
        {
            SetRectangle(0, 0, 1, 1, _drawScale);
            _initialize();
        }

        public _drawRoundedRectangle(float scale)
        {
            SetRectangle(0, 0, 1, 1, scale);
            _initialize();
        }

        public _drawRoundedRectangle(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            _rectangleResult.X = (int)(x / _drawScale);
            _rectangleResult.Y = (int)(y / _drawScale);
            _rectangleResult.Width = width;
            _rectangleResult.Height = height;
            _initialize();
        }

        #endregion

        #region RoundedRectangle Methods

        /// <summary>
        /// Set Rectangle Size
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Rectangle Width</param>
        /// <param name="height">Rectangle Height</param>
        /// <param name="scale">Draw Sacle</param>
        protected void SetRectangle(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            _rectangleResult.X = (int)(x / _drawScale);
            _rectangleResult.Y = (int)(y / _drawScale);
            _rectangleResult.Width = (int)(width / _drawScale);
            _rectangleResult.Height = (int)(height / _drawScale);
        }

        /// <summary>
        /// Move handle to new point (resizing) or MouseUp on draw object
        /// </summary>
        /// <param name="point">Mouse Point</param>
        /// <param name="handleNumber">Condition Mouse Point</param>
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
        /// Move object ยังไม่เข้าใจ ว่าทำงานตอนไหน อาจจะเป็น move โดย manual หรือเลือกแล้ว ทำการกดปุ่ม top left down right
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void _moveToPoint(Point newPoint)
        {
            _rectangleResult.X = (int)(newPoint.X / _drawScale);
            _rectangleResult.Y = (int)(newPoint.Y / _drawScale);
        }

        /// <summary>
        /// Draw Rounded Rectangle
        /// </summary>
        /// <param name="g"></param>
        public override void _draw(Graphics g)
        {
            Pen __pen = new Pen(this._lineColor, this._penWidth * (float)this._drawScale);
            __pen.DashStyle = this._lineStyleResult;
            SolidBrush __brush = new SolidBrush(this._backColor);

            Rectangle __RectangleNormalized = _drawRectangle._getNormalizedRectangle(Size);

            GraphicsPath gfxPath = new GraphicsPath();

            gfxPath = this._getRectangleGraphic(__RectangleNormalized);

            g.FillPath(__brush, gfxPath);
            g.DrawPath(__pen, gfxPath);

            //g.FillRectangle(__brush, __RectangleNormalized);
            //g.DrawRectangle(__pen, __RectangleNormalized);

            __pen.Dispose();
            __brush.Dispose();

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

        public override void _dump()
        {
            base._dump();
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

        /// <summary>
        /// check this object positon in rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle to Check inner</param>
        /// <returns>return 'true' is this object inner rectangle</returns>
        public override bool _intersectsWith(Rectangle rectangle)
        {
            return Size.IntersectsWith(rectangle);
        }

        protected override bool _pointInObject(Point point)
        {
            return Size.Contains(point);
        }

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

        public GraphicsPath _getRectangleGraphic(Rectangle __RectangleNormalized)
        {
            GraphicsPath gfxPath = new GraphicsPath();

            Point Point1 = new Point();
            Point Point2 = new Point();
            // TopLeft Corner
            if (RoundedRadius.TopLeft > 0)
            {
                gfxPath.AddArc(__RectangleNormalized.X, __RectangleNormalized.Y, (RoundedRadius.TopLeft * this._drawScale), (RoundedRadius.TopLeft * this._drawScale), 180, 90); // topLeft
            }
            else
            {
                Point1.X = __RectangleNormalized.X;
                Point1.Y = __RectangleNormalized.Y + (__RectangleNormalized.Height - RoundedRadius.Bottomleft);
                Point2.X = __RectangleNormalized.X;
                Point2.Y = __RectangleNormalized.Y;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __RectangleNormalized.X;
                Point1.Y = __RectangleNormalized.Y;
                Point2.X = __RectangleNormalized.X + (__RectangleNormalized.Width - RoundedRadius.TopRight);
                Point2.Y = __RectangleNormalized.Y;
                gfxPath.AddLine(Point1, Point2);
            }

            // TopRight Corner
            if (RoundedRadius.TopRight > 0)
            {
                gfxPath.AddArc(__RectangleNormalized.X + __RectangleNormalized.Width - (RoundedRadius.TopRight * this._drawScale), __RectangleNormalized.Y, (RoundedRadius.TopRight * this._drawScale), (RoundedRadius.TopRight * this._drawScale), 270, 90); // topRight
            }
            else
            {
                Point1.X = __RectangleNormalized.X + RoundedRadius.TopLeft;
                Point1.Y = __RectangleNormalized.Y;
                Point2.X = __RectangleNormalized.X + __RectangleNormalized.Width;
                Point2.Y = __RectangleNormalized.Y;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __RectangleNormalized.X + __RectangleNormalized.Width;
                Point1.Y = __RectangleNormalized.Y;
                Point2.X = __RectangleNormalized.X + __RectangleNormalized.Width;
                Point2.Y = __RectangleNormalized.Y + (__RectangleNormalized.Height - RoundedRadius.BottomRight);
                gfxPath.AddLine(Point1, Point2);
            }

            // Bottom Right 
            if (RoundedRadius.BottomRight > 0)
            {
                gfxPath.AddArc(__RectangleNormalized.X + __RectangleNormalized.Width - (RoundedRadius.BottomRight * this._drawScale), __RectangleNormalized.Y + __RectangleNormalized.Height - (RoundedRadius.BottomRight * this._drawScale), (RoundedRadius.BottomRight * this._drawScale), (RoundedRadius.BottomRight * this._drawScale), 0, 90); // bottomRight
            }
            else
            {
                Point1.X = __RectangleNormalized.X + __RectangleNormalized.Width;
                Point1.Y = __RectangleNormalized.Y + RoundedRadius.TopRight;
                Point2.X = __RectangleNormalized.X + __RectangleNormalized.Width;
                Point2.Y = __RectangleNormalized.Y + __RectangleNormalized.Height;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __RectangleNormalized.X + __RectangleNormalized.Width;
                Point1.Y = __RectangleNormalized.Y + __RectangleNormalized.Height;
                Point2.X = __RectangleNormalized.X + RoundedRadius.Bottomleft;
                Point2.Y = __RectangleNormalized.Y + __RectangleNormalized.Height;
                gfxPath.AddLine(Point1, Point2);
            }

            // Bottom Left 

            if (RoundedRadius.Bottomleft > 0)
            {
                gfxPath.AddArc(__RectangleNormalized.X, __RectangleNormalized.Y + __RectangleNormalized.Height - (RoundedRadius.Bottomleft * this._drawScale), (RoundedRadius.Bottomleft * this._drawScale), (RoundedRadius.Bottomleft * this._drawScale), 90, 90); // bottomLeft
            }
            else
            {
                Point1.X = __RectangleNormalized.X + (__RectangleNormalized.Width - RoundedRadius.BottomRight);
                Point1.Y = __RectangleNormalized.Y + __RectangleNormalized.Height;
                Point2.X = __RectangleNormalized.X;
                Point2.Y = __RectangleNormalized.Y + __RectangleNormalized.Height;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __RectangleNormalized.X;
                Point1.Y = __RectangleNormalized.Y + __RectangleNormalized.Height;
                Point2.X = __RectangleNormalized.X;
                Point2.Y = __RectangleNormalized.Y + RoundedRadius.TopLeft;
                gfxPath.AddLine(Point1, Point2);
            }

            gfxPath.CloseAllFigures();

            return gfxPath;
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
                _entryRoundedRectangle, orderNumber),
                Size);

            // add backcolor
            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryRoundedRectangle + _entryBackColor, orderNumber),
                _backColor.ToArgb());

            // add line 
            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryRoundedRectangle + _entryLineStyle, orderNumber),
                _LineStyle);
            
            // add rounded rect
            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryRoundedRectangle + _entryCornerRadius, orderNumber),
                _roundedRadiusProperty);
                

            base._saveToStream(info, orderNumber);
        }

        /// <summary>
        /// LOad object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public override void _loadFromStream(SerializationInfo info, int orderNumber)
        {
            _actualSize = (Rectangle)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryRoundedRectangle, orderNumber),
                typeof(Rectangle));

            int tmpbackcolor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryRoundedRectangle + _entryBackColor, orderNumber));
            _backColor = __getColorFromInt32(tmpbackcolor);

            _LineStyle = (DashStyle)info.GetValue(
                string.Format("{0}{1}",
                _entryRoundedRectangle + _entryLineStyle, orderNumber),
                typeof(DashStyle));

            _roundedRadiusProperty = (RoundedRectangleRadius)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryRoundedRectangle + _entryCornerRadius, orderNumber),
                typeof(RoundedRectangleRadius));

            base._loadFromStream(info, orderNumber);
        }

        #endregion
    }

    [Serializable]
    public struct RoundedRectangleRadius
    {
        public static RoundedRectangleRadius Empty;

        private int _allProperty;
        private int _topLeft;
        private int _topRight;
        private int _bottomLeft;
        private int _bottomRight;

        public int All
        {
            get
            {
                if ((this._topLeft == this._topRight) && (this._topLeft == this._bottomLeft) && (this._topLeft == this._bottomRight) && (this._bottomRight == this._bottomLeft) && (this._bottomRight == this._topRight) && (this._topRight == this._bottomLeft))
                {
                    return this._allProperty;
                }

                return -1;
            }

            set
            {
                this._allProperty = value;
                this._topLeft = value;
                this._topRight = value;
                this._bottomLeft = value;
                this._bottomRight = value;
            }
        }
        public int TopLeft
        {
            get
            {
                return this._topLeft;
            }
            set
            {
                this._topLeft = value;
            }
        }
        public int TopRight { get { return this._topRight; } set { this._topRight = value; } }
        public int Bottomleft { get { return this._bottomLeft; } set { this._bottomLeft = value; } }
        public int BottomRight { get { return this._bottomRight ;}  set { this._bottomRight = value; } }

        public RoundedRectangleRadius(int __All)
        {
            this._allProperty = __All;
            this._topLeft = __All;
            this._topRight = __All;
            this._bottomRight = __All;
            this._bottomLeft = __All;
        }

        public RoundedRectangleRadius(int __topLeft, int __topRight, int __bottomLeft, int __bottomRight)
        {
            this._allProperty = 0;
            this._topLeft = __topLeft;
            this._topRight = __topRight;
            this._bottomLeft = __bottomLeft;
            this._bottomRight = __bottomRight;

        }


        public override string ToString()
        {
            if ((this._topLeft == this._topRight) && (this._topLeft == this._bottomLeft) && (this._topLeft == this._bottomRight) && (this._bottomRight == this._bottomLeft) && (this._bottomRight == this._topRight) && (this._topRight == this._bottomLeft))
            {
                return this._allProperty.ToString();
            }

            return string.Format("{0},{1},{2},{3}", new object[] { this._topLeft, this._topRight, this._bottomLeft, this._bottomRight }); ;
        }

    }

    internal class _roundedRectangleRadiusConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is RoundedRectangleRadius)
            {
                return value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            // check what object is change
            _drawRoundedRectangle __roundedrect = (_drawRoundedRectangle)context.Instance;
            if (__roundedrect.RoundedRadius.All != (int)propertyValues["All"])
            {
                return new RoundedRectangleRadius((int)propertyValues["All"]);
            }

            return new RoundedRectangleRadius((int)propertyValues["TopLeft"], (int)propertyValues["TopRight"], (int)propertyValues["Bottomleft"], (int)propertyValues["BottomRight"]);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) ? true : base.CanConvertFrom(context, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }
    }

}
