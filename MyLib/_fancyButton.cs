using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Text;

namespace MyLib
{
    [DefaultEvent("Click")]
    public class _fancyButton : System.Windows.Forms.UserControl
    {
        public string _name = "";
        public string _resource_name = "";
        public _languageEnum _lastLanguage = _languageEnum.Null;
        private string _resultFixed = "";

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _resultFixed;
            }
            set
            {
                _resultFixed = value;
                if (_myGlobal._language != this._lastLanguage)
                {
                    _lastLanguage = _myGlobal._language;
                    if (this._resultFixed.Length > 0)
                    {
                        this.ButtonText = _myGlobal._resource(this._resultFixed);
                        this.Invalidate();
                    }
                }
                this.Invalidate();
            }
        }


        private string _idResult;
        public string _id
        {
            get { return _idResult; }
            set { _idResult = value; }
        }

        #region -  Designer  -

        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initialize the component with it's
        /// default settings.
        /// </summary>
        public _fancyButton()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;

            this.Invalidated += _fancyButton_Invalidated;
            mFadeIn.Interval = 30;
            mFadeOut.Interval = 30;
        }

        void _fancyButton_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._resultFixed.Length > 0)
                {
                    this.ButtonText = _myGlobal._resource(this._resultFixed);
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Release resources used by the control.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region -  Component Designer generated code  -

        private void InitializeComponent()
        {
            // 
            // VistaButton
            // 
            this.Name = "VistaButton";
            this.Size = new System.Drawing.Size(100, 32);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.VistaButton_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VistaButton_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VistaButton_KeyDown);
            this.MouseEnter += new System.EventHandler(this.VistaButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.VistaButton_MouseLeave);
            this.MouseUp += new MouseEventHandler(VistaButton_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VistaButton_MouseDown);
            this.GotFocus += new EventHandler(VistaButton_MouseEnter);
            this.LostFocus += new EventHandler(VistaButton_MouseLeave);
            this.mFadeIn.Tick += new EventHandler(mFadeIn_Tick);
            this.mFadeOut.Tick += new EventHandler(mFadeOut_Tick);
            this.Resize += new EventHandler(VistaButton_Resize);
            this.DoubleClick += new EventHandler(_posButton_DoubleClick);

        }

        void _posButton_DoubleClick(object sender, EventArgs e)
        {
            InvokeOnClick(this, e);
        }

        #endregion

        #endregion

        #region -  Enums  -

        /// <summary>
        /// A private enumeration that determines 
        /// the mouse state in relation to the 
        /// current instance of the control.
        /// </summary>
        enum State { None, Hover, Pressed };

        /// <summary>
        /// A public enumeration that determines whether
        /// the button background is painted when the 
        /// mouse is not inside the ClientArea.
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// Draw the button as normal
            /// </summary>
            Default,
            /// <summary>
            /// Only draw the background on mouse over.
            /// </summary>
            Flat
        };

        #endregion

        #region -  Properties  -

        #region -  Private Variables  -

        private bool calledbykey = false;
        private State mButtonState = State.None;
        private Timer mFadeIn = new Timer();
        private Timer mFadeOut = new Timer();
        private int mGlowAlpha = 0;
        private TextImageRelation mTextImageRelation = TextImageRelation.Overlay;



        #endregion

        #region -  Text  -

        public TextImageRelation TextImageRelation
        {
            get { return mTextImageRelation; }
            set { mTextImageRelation = value; }
        }

        private Color mBlurColor = Color.Yellow;
        /// <summary>
        /// The color with which the text is drawn.
        /// </summary>
        [Category("Text"),
         Browsable(true),
         DefaultValue(typeof(Color), "Yellow"),
         Description("The color with which the blur text is drawn.")]
        public Color _BlurColor
        {
            get { return mBlurColor; }
            set { mBlurColor = value; this.Invalidate(); }
        }

        private Boolean _drawNewMethodResult = false;
        public Boolean _drawNewMethod
        {
            get { return _drawNewMethodResult; }
            set { _drawNewMethodResult = value; }
        }

        private int blurAmount = 8;
        [Category("Text"),
         Browsable(true),
         DefaultValue(typeof(int), "8"),
         Description("blue font weight.")]
        public int _blueAmountText
        {
            get { return blurAmount; }
            set { blurAmount = value; this.Invalidate(); }
        }

        public string mText;
        /// <summary>
        /// The text that is displayed on the button.
        /// </summary>
        [Category("Text"),
         Description("The text that is displayed on the button.")]
        public string ButtonText
        {
            get
            {
                return mText;
            }
            set
            {
                mText = value;
                this.Invalidate();
            }
        }

        private Color mForeColor = Color.Black;
        /// <summary>
        /// The color with which the text is drawn.
        /// </summary>
        [Category("Text"),
         Browsable(true),
         DefaultValue(typeof(Color), "Black"),
         Description("The color with which the text is drawn.")]
        public override Color ForeColor
        {
            get { return mForeColor; }
            set { mForeColor = value; this.Invalidate(); }
        }

        private ContentAlignment mTextAlign = ContentAlignment.MiddleCenter;
        /// <summary>
        /// The alignment of the button text
        /// that is displayed on the control.
        /// </summary>
        [Category("Text"),
         DefaultValue(typeof(ContentAlignment), "MiddleCenter"),
         Description("The alignment of the button text " +
                     "that is displayed on the control.")]
        public ContentAlignment myTextAlign
        {
            get { return mTextAlign; }
            set { mTextAlign = value; this.Invalidate(); }
        }

        #endregion

        #region -  Image  -

        private Image mImage;
        /// <summary>
        /// The image displayed on the button that 
        /// is used to help the user identify
        /// it's function if the text is ambiguous.
        /// </summary>
        [Category("Image"),
         DefaultValue(null),
         Description("The image displayed on the button that " +
                     "is used to help the user identify" +
                     "it's function if the text is ambiguous.")]
        public Image myImage
        {
            get { return mImage; }
            set { mImage = value; this.Invalidate(); }
        }

        private ContentAlignment mImageAlign = ContentAlignment.MiddleLeft;
        /// <summary>
        /// The alignment of the image 
        /// in relation to the button.
        /// </summary>
        [Category("Image"),
         DefaultValue(typeof(ContentAlignment), "MiddleLeft"),
         Description("The alignment of the image " +
                     "in relation to the button.")]
        public ContentAlignment myImageAlign
        {
            get { return mImageAlign; }
            set { mImageAlign = value; this.Invalidate(); }
        }

        private Size mImageSize = new Size(16, 16);
        /// <summary>
        /// The size of the image to be displayed on the
        /// button. This property defaults to 16x16.
        /// </summary>
        [Category("Image"),
         DefaultValue(typeof(Size), "16, 16"),
         Description("The size of the image to be displayed on the" +
                     "button. This property defaults to 16x16.")]
        public Size ImageSize
        {
            get { return mImageSize; }
            set { mImageSize = value; this.Invalidate(); }
        }

        #endregion

        #region -  Appearance  -

        private Style mButtonStyle = Style.Default;
        /// <summary>
        /// Sets whether the button background is drawn 
        /// while the mouse is outside of the client area.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Style), "Default"),
         Description("Sets whether the button background is drawn " +
                     "while the mouse is outside of the client area.")]
        public Style ButtonStyle
        {
            get { return mButtonStyle; }
            set { mButtonStyle = value; this.Invalidate(); }
        }

        private int mCornerRadius = 4;
        /// <summary>
        /// The radius for the button corners. The 
        /// greater this value is, the more 'smooth' 
        /// the corners are. This property should
        ///  not be greater than half of the 
        ///  controls height.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(4),
         Description("The radius for the button corners. The " +
                     "greater this value is, the more 'smooth' " +
                     "the corners are. This property should " +
                     "not be greater than half of the " +
                     "controls height.")]
        public int CornerRadius
        {
            get { return mCornerRadius; }
            set { mCornerRadius = value; this.Invalidate(); }
        }

        private Color mHighlightColor = Color.White;
        /// <summary>
        /// The colour of the highlight on the top of the button.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "White"),
         Description("The colour of the highlight on the top of the button.")]
        public Color HighlightColor
        {
            get { return mHighlightColor; }
            set { mHighlightColor = value; this.Invalidate(); }
        }

        private Color mButtonColor = Color.DodgerBlue;
        /// <summary>
        /// The bottom color of the button that 
        /// will be drawn over the base color.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "DodgerBlue"),
         Description("The bottom color of the button that " +
                     "will be drawn over the base color.")]
        public Color ButtonColor
        {
            get { return mButtonColor; }
            set { mButtonColor = value; this.Invalidate(); }
        }

        private Color mGlowColor = Color.White;
        /// <summary>
        /// The colour that the button glows when
        /// the mouse is inside the client area.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "White"),
         Description("The colour that the button glows when " +
                     "the mouse is inside the client area.")]
        public Color GlowColor
        {
            get { return mGlowColor; }
            set { mGlowColor = value; this.Invalidate(); }
        }

        private Image mBackImage;
        /// <summary>
        /// The background image for the button, 
        /// this image is drawn over the base 
        /// color of the button.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(null),
         Description("The background image for the button, " +
                     "this image is drawn over the base " +
                     "color of the button.")]
        public Image BackImage
        {
            get { return mBackImage; }
            set { mBackImage = value; this.Invalidate(); }
        }

        private Color mBaseColor = Color.White;
        /// <summary>
        /// The backing color that the rest of 
        /// the button is drawn. For a glassier 
        /// effect set this property to Transparent.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "White"),
         Description("The backing color that the rest of" +
                     "the button is drawn. For a glassier " +
                     "effect set this property to Transparent.")]
        public Color BaseColor
        {
            get { return mBaseColor; }
            set { mBaseColor = value; this.Invalidate(); }
        }

        #endregion

        #endregion

        #region -  Functions  -

        private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
        {
            float x = r.X, y = r.Y, w = r.Width, h = r.Height;
            GraphicsPath rr = new GraphicsPath();
            rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            rr.AddLine(x + r1, y, x + w - r2, y);
            rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
            rr.AddLine(x + w, y + r2, x + w, y + h - r3);
            rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
            rr.AddLine(x + w - r3, y + h, x + r4, y + h);
            rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
            rr.AddLine(x, y + h - r4, x, y + r1);
            return rr;
        }

        private StringFormat StringFormatAlignment(ContentAlignment textalign)
        {
            StringFormat sf = new StringFormat();
            bool __getLineAlignment = true;
            bool __getAlignment = true;

            if (this.mTextImageRelation == System.Windows.Forms.TextImageRelation.ImageAboveText)
            {
                sf.LineAlignment = StringAlignment.Near;
                __getLineAlignment = false;
            }

            if (__getLineAlignment)
            {
                switch (textalign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.MiddleRight:
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.BottomRight:
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                }
            }

            if (__getAlignment)
            {
                switch (textalign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        sf.Alignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.TopRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.BottomRight:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }
            }
            return sf;
        }

        private StringAlignment _getAlignment(ContentAlignment align)
        {
            switch (align)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return StringAlignment.Center;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return StringAlignment.Far;
            }

            return StringAlignment.Near;
        }

        private StringAlignment _getLineAlignment(ContentAlignment alien)
        {
            switch (alien)
            {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    return StringAlignment.Center;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    return StringAlignment.Far;
            }
            return StringAlignment.Near;

        }

        private Rectangle _drawImageRectangle;
        private Rectangle _drawTextRectangle;
        SizeF _textSize = new SizeF();
        private int _innerXPadding = 2;
        private int _innerYPadding = 4;

        private void _processTextImageRectangle()
        {
            _drawImageRectangle = new Rectangle(0, 0, this.ImageSize.Width, this.ImageSize.Height);
            _drawTextRectangle = new Rectangle(_innerXPadding, _innerYPadding, (this.Width - (_innerXPadding * 2)), (this.Height - (_innerYPadding * 2)));


            switch (this.TextImageRelation)
            {
                case System.Windows.Forms.TextImageRelation.ImageAboveText:
                    {
                        // image : buttom , text : top = image : middle , text = bottom
                        #region ImageAboveText

                        switch (this.myImageAlign)
                        {
                            case ContentAlignment.TopLeft:
                                _drawImageRectangle = new Rectangle(_innerXPadding, _innerYPadding, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.TopCenter:
                                _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, _innerYPadding, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.TopRight:
                                _drawImageRectangle = new Rectangle(this.Width - (_innerYPadding + this.ImageSize.Width), _innerYPadding, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.MiddleLeft:
                                _drawImageRectangle = new Rectangle(_innerXPadding, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.MiddleCenter:
                                _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.MiddleRight:
                                _drawImageRectangle = new Rectangle(this.Width - (_innerYPadding + this.ImageSize.Width), this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.BottomLeft:
                                {
                                    switch (this.myTextAlign)
                                    {
                                        case ContentAlignment.TopLeft:
                                        case ContentAlignment.TopCenter:
                                        case ContentAlignment.TopRight:
                                        case ContentAlignment.MiddleLeft:
                                        case ContentAlignment.MiddleCenter:
                                        case ContentAlignment.MiddleRight:
                                            _drawImageRectangle = new Rectangle(_innerXPadding, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                            break;
                                        default:
                                            _drawImageRectangle = new Rectangle(_innerXPadding, this.Height - (_innerYPadding + this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                                            break;
                                    }
                                }
                                break;
                            case ContentAlignment.BottomCenter:
                                {
                                    switch (this.myTextAlign)
                                    {
                                        case ContentAlignment.TopLeft:
                                        case ContentAlignment.TopCenter:
                                        case ContentAlignment.TopRight:
                                        case ContentAlignment.MiddleLeft:
                                        case ContentAlignment.MiddleCenter:
                                        case ContentAlignment.MiddleRight:
                                            _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                            break;
                                        default:
                                            _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height - (_innerYPadding + this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                                            break;
                                    }
                                }
                                break;
                            case ContentAlignment.BottomRight:
                                {
                                    switch (this.myTextAlign)
                                    {
                                        case ContentAlignment.TopLeft:
                                        case ContentAlignment.TopCenter:
                                        case ContentAlignment.TopRight:
                                        case ContentAlignment.MiddleLeft:
                                        case ContentAlignment.MiddleCenter:
                                        case ContentAlignment.MiddleRight:
                                            _drawImageRectangle = new Rectangle(this.Width - (_innerYPadding + this.ImageSize.Width), this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                            break;
                                        default:
                                            _drawImageRectangle = new Rectangle(this.Width - (_innerYPadding + this.ImageSize.Width), this.Height - (_innerYPadding + this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                                            break;
                                    }
                                }
                                break;
                        }

                        _textSize = this.CreateGraphics().MeasureString(this.ButtonText, this.Font, this.Width);

                        // text rect
                        if (this.myImageAlign == ContentAlignment.TopCenter || this.myImageAlign == ContentAlignment.TopLeft || this.myImageAlign == ContentAlignment.TopRight)
                        {
                            _drawTextRectangle = new Rectangle(0, this.ImageSize.Height + _innerYPadding, this.Width - (_innerYPadding * 2), this.Height - this.ImageSize.Height - (_innerXPadding * 2));
                        }
                        else if (this.myImageAlign == ContentAlignment.MiddleCenter || this.myImageAlign == ContentAlignment.MiddleLeft || this.myImageAlign == ContentAlignment.MiddleRight)
                        {
                            switch (this.myTextAlign)
                            {
                                case ContentAlignment.TopLeft:
                                case ContentAlignment.TopCenter:
                                case ContentAlignment.TopRight:
                                case ContentAlignment.MiddleLeft:
                                case ContentAlignment.MiddleCenter:
                                case ContentAlignment.MiddleRight:
                                    // หากึงตำแหน่งรูป แล้วเอาความสูงของ text ไปลบ
                                    _drawImageRectangle.Y -= (int)_textSize.Height + (int)(_textSize.Height / 2);
                                    _drawTextRectangle = new Rectangle(0, _drawImageRectangle.Y + this.ImageSize.Height, this.Width - (_innerYPadding * 2), this.Height - (_drawImageRectangle.Y + this.ImageSize.Height + _innerYPadding));
                                    break;
                                case ContentAlignment.BottomLeft:
                                case ContentAlignment.BottomCenter:
                                case ContentAlignment.BottomRight:

                                    // เอาความสูงรวมกัน หาร 2 (กึงกลางทั้งคู่)
                                    _drawImageRectangle.Y -= (int)_textSize.Height;
                                    _drawTextRectangle = new Rectangle(0, _drawImageRectangle.Y + this.ImageSize.Height + _innerXPadding, this.Width - (_innerYPadding * 2), this.Height - (_drawImageRectangle.Y + this.ImageSize.Height + _innerYPadding));
                                    break;
                            }
                        }
                        else if (this.myImageAlign == ContentAlignment.BottomLeft || this.myImageAlign == ContentAlignment.BottomCenter || this.myImageAlign == ContentAlignment.BottomRight)
                        {
                            switch (this.myTextAlign)
                            {
                                case ContentAlignment.TopLeft:
                                case ContentAlignment.TopCenter:
                                case ContentAlignment.TopRight:
                                case ContentAlignment.MiddleLeft:
                                case ContentAlignment.MiddleCenter:
                                case ContentAlignment.MiddleRight:
                                    // หากึงตำแหน่งรูป แล้วเอาความสูงของ text ไปลบ
                                    //_drawImageRectangle.Y -= (int)_textSize.Height + (int)(_textSize.Height / 2);
                                    //_drawTextRectangle = new Rectangle(0, _drawImageRectangle.Y + this.ImageSize.Height, this.Width - (_innerYPaddign * 2), this.Height - (_drawImageRectangle.Y + this.ImageSize.Height + _innerYPaddign));
                                    _drawImageRectangle.Y -= (int)_textSize.Height;
                                    _drawTextRectangle = new Rectangle(0, _drawImageRectangle.Y + this.ImageSize.Height + _innerXPadding, this.Width - (_innerYPadding * 2), this.Height - (_drawImageRectangle.Y + this.ImageSize.Height + _innerYPadding));
                                    break;
                                case ContentAlignment.BottomLeft:
                                case ContentAlignment.BottomCenter:
                                case ContentAlignment.BottomRight:

                                    // เอาความสูงรวมกัน หาร 2 (กึงกลางทั้งคู่)
                                    _drawImageRectangle.Y -= (int)_textSize.Height;
                                    _drawTextRectangle = new Rectangle(0, (_drawImageRectangle.Y + this.ImageSize.Height), this.Width - (_innerYPadding * 2), this.Height - (_drawImageRectangle.Y + this.ImageSize.Height + _innerYPadding));
                                    break;
                            }
                        }

                        #endregion
                    }
                    break;

                case System.Windows.Forms.TextImageRelation.TextAboveImage:
                    {
                        #region TextAboveImage

                        _textSize = this.CreateGraphics().MeasureString(this.ButtonText, this.Font, this.Width);


                        // set รูปภาพแนวนอน
                        switch (this.myImageAlign)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.BottomLeft:

                                _drawImageRectangle.X = _innerXPadding;

                                break;
                            case ContentAlignment.TopCenter:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.BottomCenter:

                                _drawImageRectangle.X = this.Width / 2 - this.ImageSize.Width / 2;

                                break;
                            case ContentAlignment.TopRight:
                            case ContentAlignment.MiddleRight:
                            case ContentAlignment.BottomRight:
                                _drawImageRectangle.X = this.Width - (_innerXPadding + this.ImageSize.Width);

                                break;
                        }


                        // รูปภาพแนวตั้ง
                        switch (this.myImageAlign)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopCenter:
                            case ContentAlignment.TopRight:
                                {
                                    #region รูปแถวบน

                                    if (this.myTextAlign == ContentAlignment.BottomCenter || this.myTextAlign == ContentAlignment.BottomLeft || this.myTextAlign == ContentAlignment.BottomRight)
                                    {
                                        _drawImageRectangle.Y = (this.Height / 2 - this.ImageSize.Height / 2) + (int)_textSize.Height / 2;
                                        //= new Rectangle(_innerXPadding, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);

                                    }
                                    else if (this.myTextAlign == ContentAlignment.TopCenter || this.myTextAlign == ContentAlignment.TopLeft || this.myTextAlign == ContentAlignment.TopRight)
                                    {
                                        _drawImageRectangle.Y = (int)_textSize.Height + _innerYPadding;
                                    }
                                    else
                                    {
                                        //_drawImageRectangle.Y = 
                                        //_drawImageRectangle = new Rectangle(_innerXPadding, (int)_textSize.Height + _innerXPadding, this.ImageSize.Width, this.ImageSize.Height);
                                    }

                                    #endregion
                                }
                                break;
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.MiddleRight:
                                {
                                    #region รูปแถวกลาง

                                    _drawImageRectangle.X = _innerXPadding;
                                    _drawImageRectangle.Y = _innerYPadding;

                                    //    _drawImageRectangle = new Rectangle(_innerXPadding, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                    //    break;
                                    //case ContentAlignment.MiddleCenter:
                                    //    _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                    //    break;
                                    //case ContentAlignment.MiddleRight:
                                    //    _drawImageRectangle = new Rectangle(this.Width - (_innerXPadding + this.ImageSize.Width), this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);

                                    #endregion
                                }
                                break;
                            case ContentAlignment.BottomLeft:
                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.BottomRight:
                                {
                                    #region รูปแถวล่าง
                                    _drawImageRectangle.Y = this.Height - (_innerYPadding + this.ImageSize.Height);
                                    #endregion
                                }
                                break;
                        }


                        // set ข้อความแนวนอน
                        _drawTextRectangle.X = _innerXPadding;
                        _drawTextRectangle.Width = this.Width - (_innerXPadding * 2);

                        // ข้อความแนวตั้ง
                        if (_getLineAlignment(this.myImageAlign) == StringAlignment.Near && _getLineAlignment(this.myTextAlign) == StringAlignment.Far)
                        {
                            _drawTextRectangle.Y = (this.Height / 2 - this.ImageSize.Height / 2) - _innerYPadding;
                            _drawTextRectangle.Height = (int)_textSize.Height;

                        }

                        if (_getLineAlignment(this.myImageAlign) == StringAlignment.Far && _getLineAlignment(this.myTextAlign) == StringAlignment.Far)
                        {
                            _drawTextRectangle.Y = this.Height - this.ImageSize.Height - _innerYPadding - (int)_textSize.Height;
                            _drawTextRectangle.Height = (int)_textSize.Height;
                        }

                        //switch (this.myImageAlign)
                        //{
                        //    case ContentAlignment.TopLeft:
                        //    case ContentAlignment.TopCenter:
                        //    case ContentAlignment.TopRight:
                        //        {
                        //            switch (this.myTextAlign)
                        //            {
                        //                case ContentAlignment.MiddleLeft:
                        //                case ContentAlignment.MiddleCenter:
                        //                case ContentAlignment.MiddleRight:
                        //                    {

                        //                    }
                        //                    break;
                        //                case ContentAlignment.BottomLeft:
                        //                case ContentAlignment.BottomCenter:
                        //                case ContentAlignment.BottomRight:
                        //                    {
                        //                        //_drawTextRectangle = new Rectangle(_innerXPadding, _innerYPadding, this.Width - (_innerYPadding * 2), this.Height / 2 - this.ImageSize.Height / 2 - (int)(this._textSize.Height / 2));
                        //                    }
                        //                    break;
                        //            }
                        //        }
                        //        break;
                        //    case ContentAlignment.MiddleLeft:
                        //    case ContentAlignment.MiddleCenter:
                        //    case ContentAlignment.MiddleRight:
                        //        {

                        //        }
                        //        break;
                        //    case ContentAlignment.BottomLeft:
                        //    case ContentAlignment.BottomCenter:
                        //    case ContentAlignment.BottomRight:
                        //        {
                        //            //_drawTextRectangle = new Rectangle(_innerXPadding, _innerYPadding, this.Width - (_innerYPadding * 2), this.Height - ((_innerXPadding * 2) + this.ImageSize.Height));
                        //        }
                        //        break;

                        //}

                        #endregion
                    }
                    break;
                default:
                    {
                        #region Overlay Method
                        // iamge rect
                        switch (this.myImageAlign)
                        {
                            case ContentAlignment.TopLeft:
                                _drawImageRectangle = new Rectangle(_innerXPadding, _innerYPadding, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.TopCenter:
                                _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, _innerYPadding, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.TopRight:
                                _drawImageRectangle = new Rectangle(this.Width - (_innerYPadding + this.ImageSize.Width), _innerYPadding, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.MiddleLeft:
                                _drawImageRectangle = new Rectangle(_innerXPadding, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.MiddleCenter:
                                _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.MiddleRight:
                                _drawImageRectangle = new Rectangle(this.Width - (_innerYPadding + this.ImageSize.Width), this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.BottomLeft:
                                _drawImageRectangle = new Rectangle(_innerXPadding, this.Height - (_innerYPadding + this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.BottomCenter:
                                _drawImageRectangle = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height - (_innerYPadding + this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                                break;
                            case ContentAlignment.BottomRight:
                                _drawImageRectangle = new Rectangle(this.Width - (_innerYPadding + this.ImageSize.Width), this.Height - (_innerYPadding + this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                                break;
                        }

                        _drawTextRectangle = new Rectangle(0, 0, this.Width - (_innerYPadding * 2), this.Height - (_innerXPadding * 2));

                        #endregion

                    }
                    break;
            }

        }

        #endregion



        #region -  Drawing  -

        /// <summary>
        /// Draws the outer border for the control
        /// using the ButtonColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawOuterStroke(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            Rectangle r = this.ClientRectangle;
            r.Width -= 1; r.Height -= 1;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (Pen p = new Pen(this.ButtonColor))
                {
                    g.DrawPath(p, rr);
                }
            }
        }

        /// <summary>
        /// Draws the inner border for the control
        /// using the HighlightColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawInnerStroke(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            Rectangle r = this.ClientRectangle;
            r.X++; r.Y++;
            r.Width -= 3; r.Height -= 3;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (Pen p = new Pen(this.HighlightColor))
                {
                    g.DrawPath(p, rr);
                }
            }
        }

        /// <summary>
        /// Draws the background for the control
        /// using the background image and the 
        /// BaseColor.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawBackground(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            int alpha = (mButtonState == State.Pressed) ? 204 : 127;
            Rectangle r = this.ClientRectangle;
            r.Width--; r.Height--;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (SolidBrush sb = new SolidBrush(this.BaseColor))
                {
                    g.FillPath(sb, rr);
                }
                SetClip(g);
                if (this.BackImage != null) { g.DrawImage(this.BackImage, this.ClientRectangle); }
                g.ResetClip();
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, this.ButtonColor)))
                {
                    g.FillPath(sb, rr);
                }
            }
        }

        /// <summary>
        /// Draws the Highlight over the top of the
        /// control using the HightlightColor.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawHighlight(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            int alpha = (mButtonState == State.Pressed) ? 60 : 150;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height / 2);
            using (GraphicsPath r = RoundRect(rect, CornerRadius, CornerRadius, 0, 0))
            {
                using (LinearGradientBrush lg = new LinearGradientBrush(r.GetBounds(),
                                            Color.FromArgb(alpha, this.HighlightColor),
                                            Color.FromArgb(alpha / 3, this.HighlightColor),
                                            LinearGradientMode.Vertical))
                {
                    g.FillPath(lg, r);
                }
            }
        }

        /// <summary>
        /// Draws the glow for the button when the
        /// mouse is inside the client area using
        /// the GlowColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawGlow(Graphics g)
        {
            if (this.mButtonState == State.Pressed) { return; }
            SetClip(g);
            using (GraphicsPath glow = new GraphicsPath())
            {
                glow.AddEllipse(-5, this.Height / 2 - 10, this.Width + 11, this.Height + 11);
                using (PathGradientBrush gl = new PathGradientBrush(glow))
                {
                    gl.CenterColor = Color.FromArgb(mGlowAlpha, this.GlowColor);
                    gl.SurroundColors = new Color[] { Color.FromArgb(0, this.GlowColor) };
                    g.FillPath(gl, glow);
                }
            }
            g.ResetClip();
        }

        /// <summary>
        /// Draws the text for the button.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawText(Graphics g)
        {
            StringFormat sf = StringFormatAlignment(this.myTextAlign);
            int __imageWidth = (this.myImage == null || this.ImageSize.Width == 0) ? 0 : this.ImageSize.Width + 8;
            Rectangle r = _drawTextRectangle; //new Rectangle(__imageWidth, 2, this.Width - __imageWidth, this.Height - 2);

            //if (_drawNewMethodResult)
            //{
            //    //int __width = (
            //    //    ((this.myImageAlign == ContentAlignment.MiddleCenter ) && (this.myTextAlign == ContentAlignment.MiddleCenter))
            //    //    ) ? this.ImageSize.Width + 8 : 0;

            //    int __width = (
            //        ((this.myImageAlign == ContentAlignment.TopCenter || this.myImageAlign == ContentAlignment.TopLeft || this.myImageAlign == ContentAlignment.TopRight) && (this.myTextAlign == ContentAlignment.TopCenter || this.myTextAlign == ContentAlignment.TopLeft || this.myTextAlign == ContentAlignment.TopRight)) ||
            //        ((this.myImageAlign == ContentAlignment.MiddleCenter || this.myImageAlign == ContentAlignment.MiddleLeft || this.myImageAlign == ContentAlignment.MiddleRight) && (this.myTextAlign == ContentAlignment.MiddleCenter || this.myTextAlign == ContentAlignment.MiddleLeft || this.myTextAlign == ContentAlignment.MiddleRight)) ||
            //        ((this.myImageAlign == ContentAlignment.BottomCenter || this.myImageAlign == ContentAlignment.BottomLeft || this.myImageAlign == ContentAlignment.BottomRight) && (this.myTextAlign == ContentAlignment.BottomCenter || this.myTextAlign == ContentAlignment.BottomLeft || this.myTextAlign == ContentAlignment.BottomRight))) ? this.ImageSize.Width + 8 : 0;

            //    int __height = (
            //        ((this.myImageAlign == ContentAlignment.TopCenter || this.myImageAlign == ContentAlignment.TopLeft || this.myImageAlign == ContentAlignment.TopRight) && (this.myTextAlign == ContentAlignment.TopCenter || this.myTextAlign == ContentAlignment.TopLeft || this.myTextAlign == ContentAlignment.TopRight)) ||
            //        ((this.myImageAlign == ContentAlignment.MiddleCenter || this.myImageAlign == ContentAlignment.MiddleLeft || this.myImageAlign == ContentAlignment.MiddleRight) && (this.myTextAlign == ContentAlignment.MiddleCenter || this.myTextAlign == ContentAlignment.MiddleLeft || this.myTextAlign == ContentAlignment.MiddleRight)) ||
            //        ((this.myImageAlign == ContentAlignment.BottomCenter || this.myImageAlign == ContentAlignment.BottomLeft || this.myImageAlign == ContentAlignment.BottomRight) && (this.myTextAlign == ContentAlignment.BottomCenter || this.myTextAlign == ContentAlignment.BottomLeft || this.myTextAlign == ContentAlignment.BottomRight))) ? this.ImageSize.Width + 8 : 0;

            //    r = new Rectangle(__width, __height, this.Width - __width, this.Height - __height);

            //}

            using (Bitmap bmp = new Bitmap((int)this.Width, (int)this.Height))
            using (Graphics gBmp = Graphics.FromImage(bmp))
            using (SolidBrush brBack = new SolidBrush(Color.FromArgb(16, mBlurColor.R, mBlurColor.G, mBlurColor.B)))
            using (SolidBrush brFore = new SolidBrush(this.ForeColor))
            {
                gBmp.SmoothingMode = SmoothingMode.HighQuality;
                gBmp.InterpolationMode = InterpolationMode.HighQualityBilinear;
                gBmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;


                gBmp.DrawString(this.ButtonText, this.Font, brBack, r, sf);

                // fancy text here
                for (int x = 0; x <= blurAmount; x++)
                    for (int y = 0; y <= blurAmount; y++)
                        g.DrawImageUnscaled(bmp, x, y);

            }

            r.X = r.X + (blurAmount / 2);
            r.Y = r.Y + (blurAmount / 2);


            g.DrawString(this.ButtonText, this.Font, new SolidBrush(this.ForeColor), r, sf);
        }

        /// <summary>
        /// Draws the image for the button
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawImage(Graphics g)
        {
            if (this.myImage == null) { return; }
            //Rectangle r = new Rectangle(8, 8, this.ImageSize.Width, this.ImageSize.Height);
            //switch (this.myImageAlign)
            //{
            //    case ContentAlignment.TopCenter:
            //        r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, 8, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //    case ContentAlignment.TopRight:
            //        r = new Rectangle(this.Width - 8 - this.ImageSize.Width, 8, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //    case ContentAlignment.MiddleLeft:
            //        r = new Rectangle(8, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //    case ContentAlignment.MiddleCenter:
            //        r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //    case ContentAlignment.MiddleRight:
            //        r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //    case ContentAlignment.BottomLeft:
            //        r = new Rectangle(8, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //    case ContentAlignment.BottomCenter:
            //        r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //    case ContentAlignment.BottomRight:
            //        r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
            //        break;
            //}
            g.DrawImage(this.myImage, _drawImageRectangle);
        }

        private void SetClip(Graphics g)
        {
            Rectangle r = this.ClientRectangle;
            r.X++; r.Y++; r.Width -= 3; r.Height -= 3;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                g.SetClip(rr);
            }
        }

        #endregion

        #region -  Private Subs  -

        private void VistaButton_Paint(object sender, PaintEventArgs e)
        {
            _processTextImageRectangle();
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            DrawBackground(e.Graphics);
            DrawHighlight(e.Graphics);
            DrawImage(e.Graphics);
            DrawGlow(e.Graphics);
            DrawOuterStroke(e.Graphics);
            DrawInnerStroke(e.Graphics);
            DrawText(e.Graphics);
        }

        private void VistaButton_Resize(object sender, EventArgs e)
        {
            Rectangle r = this.ClientRectangle;
            r.X -= 1; r.Y -= 1;
            r.Width += 2; r.Height += 2;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                this.Region = new Region(rr);
            }
        }

        #region -  Mouse and Keyboard Events  -

        private void VistaButton_MouseEnter(object sender, EventArgs e)
        {
            mButtonState = State.Hover;
            mFadeOut.Stop();
            mFadeIn.Start();
        }
        private void VistaButton_MouseLeave(object sender, EventArgs e)
        {
            mButtonState = State.None;
            if (this.mButtonStyle == Style.Flat) { mGlowAlpha = 0; }
            mFadeIn.Stop();
            mFadeOut.Start();
        }

        private void VistaButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mButtonState = State.Pressed;
                if (this.mButtonStyle != Style.Flat) { mGlowAlpha = 255; }
                mFadeIn.Stop();
                mFadeOut.Stop();
                this.Invalidate();
            }
        }

        private void mFadeIn_Tick(object sender, EventArgs e)
        {
            if (this.ButtonStyle == Style.Flat) { mGlowAlpha = 0; }
            if (mGlowAlpha + 30 >= 255)
            {
                mGlowAlpha = 255;
                mFadeIn.Stop();
            }
            else
            {
                mGlowAlpha += 30;
            }
            this.Invalidate();
        }

        private void mFadeOut_Tick(object sender, EventArgs e)
        {
            if (this.ButtonStyle == Style.Flat) { mGlowAlpha = 0; }
            if (mGlowAlpha - 30 <= 0)
            {
                mGlowAlpha = 0;
                mFadeOut.Stop();
            }
            else
            {
                mGlowAlpha -= 30;
            }
            this.Invalidate();
        }

        private void VistaButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs m = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
                VistaButton_MouseDown(sender, m);
            }
        }

        private void VistaButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs m = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
                calledbykey = true;
                VistaButton_MouseUp(sender, m);
            }
        }

        private void VistaButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mButtonState = State.Hover;
                mFadeIn.Stop();
                mFadeOut.Stop();
                this.Invalidate();
                if (calledbykey == true) { this.OnClick(EventArgs.Empty); calledbykey = false; }
            }
        }


        #endregion
        #endregion


    }

    public class _fancyLabel : System.Windows.Forms.Label
    {
        private System.ComponentModel.Container components = null;

        private int _blurAmountResult = 6;
        public int _blurAmount
        {
            get
            {
                return _blurAmountResult;
            }
            set
            {
                _blurAmountResult = value; this.Invalidate();
            }
        }

        private Color mBlurColor = Color.Yellow;
        /// <summary>
        /// The color with which the text is drawn.
        /// </summary>
        [Category("Text"),
         Browsable(true),
         DefaultValue(typeof(Color), "Yellow"),
         Description("The color with which the blur text is drawn.")]
        public Color _BlurColor
        {
            get { return mBlurColor; }
            set { mBlurColor = value; this.Invalidate(); }
        }


        public _fancyLabel()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public _fancyLabel(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ForeColor = Color.LightSkyBlue;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;

        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = this.ClientRectangle;
            StringFormat sf = StringFormat.GenericDefault;

            sf.Alignment = MyLib._myGlobal._getStringFormatTextAlignment(this.TextAlign);
            sf.LineAlignment = MyLib._myGlobal._getStringFormatTextLineAlignment(this.TextAlign);


            using (Bitmap bmp = new Bitmap((int)r.Width, (int)r.Height))
            using (Graphics gBmp = Graphics.FromImage(bmp))
            using (SolidBrush brBack = new SolidBrush(Color.FromArgb(16, mBlurColor.R, mBlurColor.G, mBlurColor.B)))
            using (SolidBrush brFore = new SolidBrush(this.ForeColor))
            {
                gBmp.SmoothingMode = SmoothingMode.HighQuality;
                gBmp.InterpolationMode = InterpolationMode.HighQualityBilinear;
                gBmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;


                gBmp.DrawString(this.Text, this.Font, brBack, r, sf);

                // fancy text here
                //int x = 0;
                //int y = 0;
                for (int x = 0; x <= _blurAmountResult; x++)
                    for (int y = 0; y <= _blurAmountResult; y++)
                        g.DrawImageUnscaled(bmp, x, y);
            }

            //base.OnPaint(e);
            r.X = r.X + (_blurAmountResult / 2);
            r.Y = r.Y + (_blurAmountResult / 2);


            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), r, sf);
        }
    }
}
