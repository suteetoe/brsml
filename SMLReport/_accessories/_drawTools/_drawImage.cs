using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SMLReport._design
{
    /// <summary>
    /// Rectangle graphic object
    /// </summary>
    public class _drawImage : SMLReport._design._drawObject
    {
        #region Image Fields
        private const string _entryImage = "Image";
        private const string _entryBackColor = "Backcolor";
        private const string _entryLineStyle = "LineStyle";
        private const string _entryBorderStyle = "BorderStyle";
        private const string _entrySizeMode = "SizeMode";
        private const string _entryImageSource = "Source";
        

        private Image _image;
        private ImageBorderStyleType _borderStyle = ImageBorderStyleType.None;
        private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.StretchImage;
        private int _opacity = 100;
        private RotateFlipType _rotateFlip = RotateFlipType.RotateNoneFlipNone;
        private float _angle = 0;

        public enum ImageBorderStyleType
        {
            None,
            Line,
            DoubleLine
        };
        private DashStyle _lineStyleResult;

        #endregion

        #region Image Property

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

        [Category("_SML")]
        [Description("รูปภาพ")]
        [DisplayName("Image : เลือกรูปภาพ")]
        [DefaultValue(true)]
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        [Category("_SML")]
        [Description("หมุนภาพ (องศา)")]
        [DisplayName("Angle : หมุนภาพ (องศา)")]
        [DefaultValue(0)]
        [Browsable(false)]
        public float Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
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

        [Category("_SML")]
        [Description("ระดับความใสของภาพ")]
        [DisplayName("Opacity (%) : ระดับความใสของภาพ ")]
        [DefaultValue(100)]
        [Browsable(false)]
        public int Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                _opacity = value;
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
                if (_image != null)
                {
                    _image = FlipImage(_image, value);
                }
            }
        }

        [Category("_SML")]
        [Description("กรอบรูป")]
        [DisplayName("Border Style : กรอบรูป")]
        [DefaultValue(ImageBorderStyleType.None)]
        [Browsable(false)]
        public ImageBorderStyleType BorderStyle
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                _borderStyle = value;
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

        #region Image Constructor

        public _drawImage()
        {
            SetRectangle(0, 0, 1, 1, _drawScale);
            _initialize();
        }

        public _drawImage(float scale)
        {
            _drawScale = scale;
            SetRectangle(0, 0, 1, 1, _drawScale);
            _initialize();
        }

        public _drawImage(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            _rectangleResult.X = (int)(x / _drawScale);
            _rectangleResult.Y = (int)(y / _drawScale);
            _rectangleResult.Width = width;
            _rectangleResult.Height = height;
            _initialize();
        }

        #endregion

        #region Image Methods

        protected void SetRectangle(int x, int y, int width, int height, float scale)
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
        }

        public override void _dump()
        {
            base._dump();

            Trace.WriteLine("rectangle.X = " + Size.X.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Y = " + Size.Y.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Width = " + Size.Width.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Height = " + Size.Height.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Normalize rectangle
        /// </summary>
        public override void _normalize()
        {
            //Rectangle = _drawRectangle.GetNormalizedRectangle(Rectangle);
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

        public static Rectangle GetNormalizedImageRectangle(int x1, int y1, int x2, int y2)
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

        public static Rectangle GetNormalizedImageRectangle(Point p1, Point p2)
        {
            return GetNormalizedImageRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static Rectangle GetNormalizedImageRectangle(Rectangle r)
        {
            if (r.Width == 0)
                r.Width = 1;
            if (r.Height == 0)
                r.Height = 1;
            return GetNormalizedImageRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public override void _draw(Graphics g)
        {
            Pen pen = new Pen(this._lineColor, this._penWidth * (float)_drawScale);
            SolidBrush brush = new SolidBrush(this._backColor);


            if (_image != null)
            {
                if (_sizeMode == PictureBoxSizeMode.AutoSize)
                {
                    _rectangleResult.Width = _image.Width;
                    _rectangleResult.Height = _image.Height;
                }

                int __x = Size.X;
                int __y = Size.Y;
                int __width = Size.Width;
                int __height = Size.Height;

                // get Image from sizeType


                Image __newImage = _SizeModeImg( _drawRectangle._getNormalizedRectangle(Size), _image, _sizeMode);
                //__newImage = FlipImage(_image, _rotateFlip);
               

                //if ((this._opacity < 100) && (this.Opacity > 0))
                //{
                //    ColorMatrix cmxPic = new ColorMatrix();
                //    cmxPic.Matrix33 = this._opacity;

                //    ImageAttributes iaPic = new ImageAttributes();
                //    iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //    g.DrawImage(_image, new Rectangle(0, 0, __width, __height), 0, 0, __height, __height, GraphicsUnit.Pixel, iaPic);

                //    //g.DrawImage(_image, __x, __y, __width, __height);
                //}
                //else
                //{
                //    g.DrawImage(_image, __x, __y, __width, __height);
                //}

                if (_borderStyle != ImageBorderStyleType.None)
                {
                    if (_penWidth < 1)
                    {
                        _penWidth = 1;
                    }
                    if (_borderStyle == ImageBorderStyleType.Line)
                    {
                        int __newLineWidth = _penWidth / 2;
                        if (__newLineWidth == 0)
                        {
                            __newLineWidth = 1;
                        }
                        __x += __newLineWidth;
                        __y += __newLineWidth;
                        __width -= __newLineWidth;
                        __height -= __newLineWidth;
                    }
                    Pen __newPen = new Pen(this._lineColor, this._penWidth * (float)_drawScale);

                    //g.DrawImage(__newImage, __x, __y, __width, __height);
                    g.DrawRectangle(__newPen, _drawRectangle._getNormalizedRectangle(Size));
                }

                Rectangle __tmpNewRect = new Rectangle(__x, __y, __width, __height);

                g.DrawImage(__newImage, _drawImage.GetNormalizedImageRectangle(__tmpNewRect));
                //g.DrawRectangle(pen, _drawRectangle._getNormalizedRectangle(__tmpNewRect));
            }
            else
            {
                g.FillRectangle(brush, _drawRectangle._getNormalizedRectangle(Size));
            }

            pen.DashStyle = this._LineStyle;
            g.DrawRectangle(pen, _drawRectangle._getNormalizedRectangle(Size));

            pen.Dispose();
            brush.Dispose();
        }

        public Image SetImgOpacity(Image imgPic, float imgOpac)
        {
            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            ColorMatrix cmxPic = new ColorMatrix();
            cmxPic.Matrix33 = imgOpac;

            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();

            return bmpPic;
        }

        public static Bitmap RotateImg(Bitmap bmp, float angle, Color bkColor)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            PixelFormat pf = default(PixelFormat);
            if (bkColor == Color.Transparent)
            {
                pf = PixelFormat.Format32bppArgb;
            }
            else
            {
                pf = bmp.PixelFormat;
            }

            Bitmap tempImg = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(tempImg);  //this._panel1.CreateGraphics();
            g.Clear(bkColor);
            //g.DrawImageUnscaled(bmp, 1, 1);
            g.DrawImage(bmp, 0, 0, w, h);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix mtrx = new Matrix();
            //Using System.Drawing.Drawing2D.Matrix class 
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);
            Bitmap newImg = new Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf);
            g = Graphics.FromImage(newImg);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tempImg, 0, 0);
            g.Dispose();
            tempImg.Dispose();
            return newImg;
        }

        public static Image FlipImage(Image __imgPic, RotateFlipType __FilpType)
        {
            //Bitmap __newImage = new Bitmap(__imgPic);
            //__newImage.RotateFlip(__FilpType);
            //return __newImage;

            __imgPic.RotateFlip(__FilpType);
            return __imgPic;
        }

        public static Image _SizeModeImg(Rectangle __Frame, Image __imgPic, PictureBoxSizeMode __sizeMode)
        {
            __Frame = _drawImage.GetNormalizedImageRectangle(__Frame);

            __Frame.X = 0;
            __Frame.Y = 0;
            Bitmap __newImage = new Bitmap(__Frame.Width, __Frame.Height);
            Graphics g = Graphics.FromImage(__newImage);

            Rectangle __newRect = _drawImage.GetNormalizedImageRectangle( new Rectangle(__Frame.X, __Frame.Y, __imgPic.Width, __imgPic.Height));

            switch (__sizeMode)
            {
                case PictureBoxSizeMode.Normal:

                    __newRect.X = 0;
                    __newRect.Y = 0;
                    __newRect.Width = __Frame.Width;
                    __newRect.Height = __Frame.Height;
                    g.DrawImage(__imgPic, _drawImage.GetNormalizedImageRectangle(__Frame), _drawImage.GetNormalizedImageRectangle(__newRect), GraphicsUnit.Pixel);
                    break;

                case PictureBoxSizeMode.CenterImage:

                    if ((__Frame.Width < __imgPic.Width) || (__Frame.Height < __imgPic.Height))
                    {
                        __newRect.X = (int)((__imgPic.Width - __Frame.Width) / 2);
                        __newRect.Y = (int)((__imgPic.Height - __Frame.Height) / 2);
                        __newRect.Width = __Frame.Width;
                        __newRect.Height = __Frame.Height;
                        g.DrawImage(__imgPic, _drawImage.GetNormalizedImageRectangle(__Frame), _drawImage.GetNormalizedImageRectangle(__newRect), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        __newRect.X += (int)((__Frame.Width - __imgPic.Width) / 2);
                        __newRect.Y += (int)((__Frame.Height - __imgPic.Height) / 2);

                        g.DrawImage(__imgPic, _drawImage.GetNormalizedImageRectangle(__newRect));
                    }


                    break;

                case PictureBoxSizeMode.StretchImage:
                    __newRect.Width = __Frame.Width;
                    __newRect.Height = __Frame.Height;
                    g.DrawImage(__imgPic, _drawImage.GetNormalizedImageRectangle(__newRect));
                    break;
                case PictureBoxSizeMode.Zoom:
                    // resize by ratio
                    float __ratioWidth = 1f;
                    float __ratioHeight = 1f;
                    if ((__Frame.Width < __imgPic.Width) || (__Frame.Height < __imgPic.Height))
                    {
                        __ratioWidth = (float)__imgPic.Width / (float)__Frame.Width;
                        __ratioHeight = (float)__imgPic.Height / (float)__Frame.Height;

                        if (__ratioWidth > __ratioHeight)
                        {
                            __newRect.Width = __Frame.Width;
                            __newRect.Height = (int)(__imgPic.Height / __ratioWidth);
                        }
                        else
                        {
                            __newRect.Width = (int)(__imgPic.Width / __ratioHeight);
                            __newRect.Height = __Frame.Height;
                        }
                    }
                    else
                    {
                        __ratioWidth = __Frame.Width / __imgPic.Width;
                        __ratioHeight = __Frame.Height / __imgPic.Height;
                        if (__ratioWidth < __ratioHeight)
                        {
                            __newRect.Width = (int)(__imgPic.Width * __ratioWidth);
                            __newRect.Height = (int)(__imgPic.Height * __ratioWidth);
                        }
                        else
                        {
                            __newRect.Width = (int)(__imgPic.Width * __ratioHeight);
                            __newRect.Height = (int)(__imgPic.Height * __ratioHeight);

                        }
                    }

                    // set x,y to center Rect Frame
                    __newRect.X += (__Frame.Width - __newRect.Width) / 2;
                    __newRect.Y += (__Frame.Height - __newRect.Height) / 2;

                    g.DrawImage(__imgPic, _drawImage.GetNormalizedImageRectangle(__newRect));
                    break;
                case PictureBoxSizeMode.AutoSize:
                    g.DrawImage(__imgPic, _drawImage.GetNormalizedImageRectangle(__newRect));
                    break;
            }

            return __newImage;
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
                _entryImage, orderNumber),
                Size);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entryBackColor, orderNumber),
                _backColor.ToArgb());

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entryBorderStyle, orderNumber),
                _borderStyle);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entrySizeMode, orderNumber),
                _sizeMode);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entryImageSource, orderNumber),
                _image);

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
                _entryImage, orderNumber),
                typeof(Rectangle));

            int tmpBackColor = info.GetInt32(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entryBackColor, orderNumber));
            _backColor =__getColorFromInt32(tmpBackColor);

            _borderStyle = (ImageBorderStyleType)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entryBorderStyle, orderNumber),
                typeof(ImageBorderStyleType));

            _sizeMode = (PictureBoxSizeMode)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entrySizeMode, orderNumber),
                typeof(PictureBoxSizeMode));
            
            _image = (Image)info.GetValue(
                string.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                _entryImage + _entryImageSource, orderNumber),
                typeof(Image));

            base._loadFromStream(info, orderNumber);
        }

        #endregion
    }
}
