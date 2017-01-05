using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace SMLReport._design
{
    /// <summary>
    /// Line graphic object
    /// </summary>
    [Serializable]
    public class _drawLine : _drawObject
    {
        #region Line Fields

        private const string entryStart = "Start";
        private const string entryEnd = "End";

        private Point _startPointResult;
        private Point _endPointResult;
        private DashStyle _lineStyleResult;

        /// <summary>
        ///  Graphic objects for hit test
        /// </summary>
        //private GraphicsPath _areaPathResult = null;

        //private Pen _areaPenResult = null;
        //private Region _areaRegionResult = null;

        #endregion

        #region Line Property

        [Category("_SML")]
        [Description("การจัดวาง")]
        [DisplayName("Location, Size : กรอบ")]
        public override Rectangle _actualSize
        {
            get
            {
                _rectangleResult = _getRectangleFrame();
                return _rectangleResult;
            }

            set
            {
                _rectangleResult = value;
                setLineFromRectangle();
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

        public override int _width
        {
            get
            {
                return ((int)Math.Abs(_startPointResult.X - _startPointResult.X));
            }
        }

        public override int _height
        {
            get
            {
                return ((int)Math.Abs(_startPointResult.Y - _startPointResult.Y));
            }
        }

        [DisplayName("StartPoint")]
        public Point _ActurlStartPoint
        {
            get
            {
                return _startPointResult;
            }
            set
            {
                _startPointResult = value;
            }
        }

        [DisplayName("EndPoint")]
        public Point _ActurlEndPoint
        {
            get
            {
                return _endPointResult;
            }
            set
            {
                _endPointResult = value;
            }
        }

        [Browsable(false)]
        public Point StartPoint
        {
            get
            {
                return (new Point((int)(_startPointResult.X * _drawScale), (int)(_startPointResult.Y * _drawScale)));
            }
            set
            {
                _startPointResult = value;
                _startPointResult.X = (int)(_startPointResult.X / _drawScale);
                _startPointResult.Y = (int)(_startPointResult.Y / _drawScale);
            }
        }

        [Browsable(false)]
        public Point EndPoint
        {
            get
            {
                return (new Point((int)(_endPointResult.X * _drawScale), (int)(_endPointResult.Y * _drawScale)));
            }
            set
            {
                _endPointResult = value;
                _endPointResult.X = (int)(_endPointResult.X / _drawScale);
                _endPointResult.Y = (int)(_endPointResult.Y / _drawScale);
            }
        }

        public override int _handleCount
        {
            get
            {
                return 2;
            }
        }

        //protected GraphicsPath _areaPath
        //{
        //    get
        //    {
        //        return _areaPathResult;
        //    }
        //    set
        //    {
        //        _areaPathResult = value;
        //    }
        //}

        //protected Pen _areaPen
        //{
        //    get
        //    {
        //        return _areaPenResult;
        //    }
        //    set
        //    {
        //        _areaPenResult = value;
        //    }
        //}

        //protected Region _areaRegion
        //{
        //    get
        //    {
        //        return _areaRegionResult;
        //    }
        //    set
        //    {
        //        _areaRegionResult = value;
        //    }
        //}

        #endregion

        #region Line Constructor

        public _drawLine()
        {
            _startPointResult.X = 0;
            _startPointResult.Y = 0;
            _endPointResult.X = 1;
            _endPointResult.Y = 1;

            _rectangleResult = _getRectangleFrame();
            _initialize();
        }

        public _drawLine(float scale)
        {
            _drawScale = scale;
            _startPointResult.X = 0;
            _startPointResult.Y = 0;
            _endPointResult.X = 1;
            _endPointResult.Y = 1;

            _rectangleResult = _getRectangleFrame();
            _initialize();
        }

        public _drawLine(int x1, int y1, int x2, int y2, float scale)
        {
            _drawScale = scale;
            x1 = (int)(x1 / _drawScale);
            x2 = (int)(x2 / _drawScale);
            y1 = (int)(y1 / _drawScale);
            y2 = (int)(y2 / _drawScale);
            _startPointResult.X = x1;
            _startPointResult.Y = y1;
            _endPointResult.X = x2;
            _endPointResult.Y = y2;

            // set rectangle
            _rectangleResult = _getRectangleFrame();
            _initialize();
        }

        #endregion

        #region Line Methods

        public override void _draw(Graphics g)
        {
            _draw(g, new PointF(0, 0));
        }

        public void _draw(Graphics g, PointF __point)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen(_lineColor, _penWidth * _drawScale);
            pen.DashStyle = this._LineStyle;

            g.DrawLine(pen, StartPoint.X + __point.X, StartPoint.Y + __point.Y, EndPoint.X + __point.X, EndPoint.Y + __point.Y);

            pen.Dispose();

            _rectangleResult = _getRectangleFrame();
        }

        public override void _drawFromPoint(Graphics g, PointF __point, float __drawScale)
        {
            this._drawScale = __drawScale;
            _draw(g, __point);
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void _moveToPoint(Point newPoint)
        {
            //_startPointResult.X = (int)(newPoint.X / _drawScale);
            //_startPointResult.Y = (int)(newPoint.Y / _drawScale);
            //_endPointResult = new Point(_startPointResult.X + 10, _startPointResult.Y + 10);
            _rectangleResult.X = (int)(newPoint.X / _drawScale);
            _rectangleResult.Y = (int)(newPoint.Y / _drawScale);
            setLineFromRectangle();
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point _getHandle(int handleNumber)
        {
            if (handleNumber == 1)
                return StartPoint;
            else
                return EndPoint;
        }

        public override Point _getHandleNoScale(int handleNumber)
        {
            if (handleNumber == 1)
                return _startPointResult;
            else
                return _endPointResult;
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
            //_areaPath = null;
            return -1;
        }

        protected override bool _pointInObject(Point point)
        {
            Region _areaRegion = _createObjects();

            return _areaRegion.IsVisible(point);
        }

        public override bool _intersectsWith(Rectangle rectangle)
        {
            Region _areaRegion = _createObjects();

            return _areaRegion.IsVisible(rectangle);
        }

        /// <summary>
        /// Create graphic objects used from hit test.
        /// </summary>
        protected virtual Region _createObjects()
        {
            //// remove because cannot serialize GraphicPath
            //if (_areaPath != null)
            //    return;

            // Create path which contains wide line
            // for easy mouse selection
            GraphicsPath _areaPath = new GraphicsPath();
            Pen _areaPen = new Pen(Color.Black, 7);
            _areaPath.AddLine(StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y);
            _areaPath.Widen(_areaPen);

            // Create region from the path
            Region _areaRegion = new Region(_areaPath);


            return _areaRegion;
            
        }

        public override Cursor _getHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                case 2:
                    return Cursors.SizeAll;
                default:
                    return Cursors.Default;
            }
        }

        public override void _moveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber == 1)
                StartPoint = point;
            else
                EndPoint = point;

            Invalidate();
        }

        public override void _move(int deltaX, int deltaY)
        {
            deltaX = (int)(deltaX / _drawScale);
            deltaY = (int)(deltaY / _drawScale);

            _startPointResult.X += deltaX;
            _startPointResult.Y += deltaY;

            _endPointResult.X += deltaX;
            _endPointResult.Y += deltaY;

            Invalidate();
        }

        /// <summary>
        /// Invalidate object.
        /// When object is invalidated, path used for hit test
        /// is released and should be created again.
        /// </summary>
        protected void Invalidate()
        {
            // remove areaPath because can't serialize GraphicPath
            //if (_areaPath != null)
            //{
            //    _areaPath.Dispose();
            //    _areaPath = null;
            //}

            //if (_areaPen != null)
            //{
            //    _areaPen.Dispose();
            //    _areaPen = null;
            //}

            //if (_areaRegion != null)
            //{
            //    _areaRegion.Dispose();
            //    _areaRegion = null;
            //}
        }

        private Rectangle _getRectangleFrame()
        {
            Point __rectangleStartPoint = new Point();
            Size __rectangleSize = new Size();


            if ((_startPointResult.X < _endPointResult.X) && (_startPointResult.Y < _endPointResult.Y))
            {
                __rectangleStartPoint = _startPointResult;
                __rectangleSize = new Size((_endPointResult.X - _startPointResult.X), (_endPointResult.Y - _startPointResult.Y));
            }
            else if ((_startPointResult.X < _endPointResult.X) && (_startPointResult.Y > _endPointResult.Y))
            {
                __rectangleStartPoint = new Point(_startPointResult.X, _endPointResult.Y);
                __rectangleSize = new Size((_endPointResult.X - _startPointResult.X), (_startPointResult.Y - _endPointResult.Y));
            }
            else if ((_startPointResult.X > _endPointResult.X) && (_startPointResult.Y < _endPointResult.Y))
            {
                __rectangleStartPoint = new Point(_endPointResult.X, _startPointResult.Y);
                __rectangleSize = new Size((_startPointResult.X - _endPointResult.X), (_endPointResult.Y - _startPointResult.Y));
            }
            else if ((_startPointResult.X > _endPointResult.X) && (_startPointResult.Y > _endPointResult.Y))
            {
                __rectangleStartPoint = _endPointResult;
                __rectangleSize = new Size((_startPointResult.X - _endPointResult.X), (_startPointResult.Y - _endPointResult.Y));
            }
            else if ((_startPointResult.X == _endPointResult.X) && (_startPointResult.Y < _endPointResult.Y))
            {
                __rectangleStartPoint = new Point(_startPointResult.X, _startPointResult.Y);
                __rectangleSize = new Size(0, (_endPointResult.Y - _startPointResult.Y));
            }
            else if ((_startPointResult.X == _endPointResult.X) && (_startPointResult.Y > _endPointResult.Y))
            {
                __rectangleStartPoint = new Point(_startPointResult.X, _endPointResult.Y);
                __rectangleSize = new Size(0, (_startPointResult.Y - _endPointResult.Y));
            }
            else if ((_startPointResult.X < _endPointResult.X) && (_startPointResult.Y == _endPointResult.Y))
            {
                __rectangleStartPoint = new Point(_startPointResult.X, _startPointResult.Y);
                __rectangleSize = new Size((_endPointResult.X - _startPointResult.X), 0);
            }
            else if ((_startPointResult.X > _endPointResult.X) && (_startPointResult.Y == _endPointResult.Y))
            {
                __rectangleStartPoint = new Point(_endPointResult.X, _startPointResult.Y);
                __rectangleSize = new Size((_startPointResult.X - _endPointResult.X), 0);
            }

            Rectangle __newRectangle = _rectangleResult;
            __newRectangle.X = __rectangleStartPoint.X;
            __newRectangle.Y = __rectangleStartPoint.Y;
            __newRectangle.Width = __rectangleSize.Width;
            __newRectangle.Height = __rectangleSize.Height;

            return __newRectangle;
        }

        private void setLineFromRectangle()
        {
            Point __newStartPoint = new Point();
            Point __newEndPoint = new Point();
            if ((_startPointResult.X < _endPointResult.X) && (_startPointResult.Y < _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X;
                __newStartPoint.Y = _rectangleResult.Y;
                __newEndPoint.X = _rectangleResult.X + _rectangleResult.Width;
                __newEndPoint.Y = _rectangleResult.Y + _rectangleResult.Height;
            }
            else if ((_startPointResult.X < _endPointResult.X) && (_startPointResult.Y > _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X;
                __newStartPoint.Y = _rectangleResult.Y + _rectangleResult.Height;
                __newEndPoint.X = _rectangleResult.X + _rectangleResult.Width;
                __newEndPoint.Y = _rectangleResult.Y;
            }
            else if ((_startPointResult.X > _endPointResult.X) && (_startPointResult.Y < _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X + _rectangleResult.Width;
                __newStartPoint.Y = _rectangleResult.Y;
                __newEndPoint.X = _rectangleResult.X;
                __newEndPoint.Y = _rectangleResult.Y + _rectangleResult.Height;
            }
            else if ((_startPointResult.X > _endPointResult.X) && (_startPointResult.Y > _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X + _rectangleResult.Width;
                __newStartPoint.Y = _rectangleResult.Y + _rectangleResult.Height;
                __newEndPoint.X = _rectangleResult.X;
                __newEndPoint.Y = _rectangleResult.Y;
            }
            else if ((_startPointResult.X == _endPointResult.X) && (_startPointResult.Y < _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X;
                __newStartPoint.Y = _rectangleResult.Y;
                __newEndPoint.X = _rectangleResult.X;
                __newEndPoint.Y = _rectangleResult.Y + _rectangleResult.Height;
            }
            else if ((_startPointResult.X == _endPointResult.X) && (_startPointResult.Y > _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X;
                __newStartPoint.Y = _rectangleResult.Y + _rectangleResult.Height;
                __newEndPoint.X = _rectangleResult.X;
                __newEndPoint.Y = _rectangleResult.Y;
            }
            else if ((_startPointResult.X < _endPointResult.X) && (_startPointResult.Y == _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X;
                __newStartPoint.Y = _rectangleResult.Y;
                __newEndPoint.X = _rectangleResult.X + _rectangleResult.Width;
                __newEndPoint.Y = _rectangleResult.Y;
            }
            else if ((_startPointResult.X > _endPointResult.X) && (_startPointResult.Y == _endPointResult.Y))
            {
                __newStartPoint.X = _rectangleResult.X + _rectangleResult.Width;
                __newStartPoint.Y = _rectangleResult.Y;
                __newEndPoint.X = _rectangleResult.X;
                __newEndPoint.Y = _rectangleResult.Y;
            }

            _startPointResult = __newStartPoint;
            _endPointResult = __newEndPoint;

        }

        #endregion

        #region Save Load From Stream

        public override void _saveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryStart, orderNumber),
                _startPointResult);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryEnd, orderNumber),
                _endPointResult);

            base._saveToStream(info, orderNumber);
        }

        public override void _loadFromStream(SerializationInfo info, int orderNumber)
        {
            _startPointResult = (Point)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryStart, orderNumber),
                typeof(Point));

            _endPointResult = (Point)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryEnd, orderNumber),
                typeof(Point));

            base._loadFromStream(info, orderNumber);
        }

        #endregion

    }
}
