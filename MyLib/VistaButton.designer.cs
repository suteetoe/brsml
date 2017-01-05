using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using System.ComponentModel;

namespace MyLib
{
    partial class VistaButton : Button
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>

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
            this.Invalidated += new InvalidateEventHandler(VistaButton_Invalidated);
        }

        void VistaButton_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (this.AutoSize)
            {
                if (this.Text != this.ButtonText)
                {
                    this.Text = this.ButtonText;
                }
                Graphics __g = this.CreateGraphics();
                SizeF __calcWidth = __g.MeasureString(this.Text, this.Font);
                int __width = ((int)__calcWidth.Width) + ((this.myImage == null) ? 0 : this.ImageSize.Width) + 16;
                int __height = ((int)__calcWidth.Height) + 8;
                if (__height < this.ImageSize.Height + 8)
                {
                    __height = this.ImageSize.Height + 8;
                }
                if (__height < 22)
                {
                    __height = 22;
                }
                if (this.Width != __width || this.Height != __height)
                {
                    this.Size = new Size(__width, __height);
                }
            }
        }

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


        private Boolean _drawNewMethodResult = false;

        public Boolean _drawNewMethod
        {
            get { return _drawNewMethodResult; }
            set { _drawNewMethodResult = value; }
        }

        #endregion

        #region -  Text  -

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
            return sf;
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
            Rectangle r = new Rectangle(__imageWidth, 2, this.Width - __imageWidth, this.Height - 2);

            if (_drawNewMethodResult)
            {
                //int __width = (
                //    ((this.myImageAlign == ContentAlignment.MiddleCenter ) && (this.myTextAlign == ContentAlignment.MiddleCenter))
                //    ) ? this.ImageSize.Width + 8 : 0;

                int __width = (
                    ((this.myImageAlign == ContentAlignment.TopCenter || this.myImageAlign == ContentAlignment.TopLeft || this.myImageAlign == ContentAlignment.TopRight) && (this.myTextAlign == ContentAlignment.TopCenter || this.myTextAlign == ContentAlignment.TopLeft || this.myTextAlign == ContentAlignment.TopRight)) ||
                    ((this.myImageAlign == ContentAlignment.MiddleCenter || this.myImageAlign == ContentAlignment.MiddleLeft || this.myImageAlign == ContentAlignment.MiddleRight) && (this.myTextAlign == ContentAlignment.MiddleCenter || this.myTextAlign == ContentAlignment.MiddleLeft || this.myTextAlign == ContentAlignment.MiddleRight)) ||
                    ((this.myImageAlign == ContentAlignment.BottomCenter || this.myImageAlign == ContentAlignment.BottomLeft || this.myImageAlign == ContentAlignment.BottomRight) && (this.myTextAlign == ContentAlignment.BottomCenter || this.myTextAlign == ContentAlignment.BottomLeft || this.myTextAlign == ContentAlignment.BottomRight))) ? this.ImageSize.Width + 8 : 0;

                int __height = (
                    ((this.myImageAlign == ContentAlignment.TopCenter || this.myImageAlign == ContentAlignment.TopLeft || this.myImageAlign == ContentAlignment.TopRight) && (this.myTextAlign == ContentAlignment.TopCenter || this.myTextAlign == ContentAlignment.TopLeft || this.myTextAlign == ContentAlignment.TopRight)) ||
                    ((this.myImageAlign == ContentAlignment.MiddleCenter || this.myImageAlign == ContentAlignment.MiddleLeft || this.myImageAlign == ContentAlignment.MiddleRight) && (this.myTextAlign == ContentAlignment.MiddleCenter || this.myTextAlign == ContentAlignment.MiddleLeft || this.myTextAlign == ContentAlignment.MiddleRight)) ||
                    ((this.myImageAlign == ContentAlignment.BottomCenter || this.myImageAlign == ContentAlignment.BottomLeft || this.myImageAlign == ContentAlignment.BottomRight) && (this.myTextAlign == ContentAlignment.BottomCenter || this.myTextAlign == ContentAlignment.BottomLeft || this.myTextAlign == ContentAlignment.BottomRight))) ? this.ImageSize.Width + 8 : 0;

                r = new Rectangle(__width, __height, this.Width - __width, this.Height - __height);

            }

            g.DrawString(this.ButtonText, this.Font, new SolidBrush(this.ForeColor), r, sf);
        }

        /// <summary>
        /// Draws the image for the button
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawImage(Graphics g)
        {
            try
            {
                if (this.myImage == null) { return; }
                Rectangle r = new Rectangle(8, 8, this.ImageSize.Width, this.ImageSize.Height);
                switch (this.myImageAlign)
                {
                    case ContentAlignment.TopCenter:
                        r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, 8, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    case ContentAlignment.TopRight:
                        r = new Rectangle(this.Width - 8 - this.ImageSize.Width, 8, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    case ContentAlignment.MiddleLeft:
                        r = new Rectangle(8, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    case ContentAlignment.MiddleCenter:
                        r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    case ContentAlignment.MiddleRight:
                        r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    case ContentAlignment.BottomLeft:
                        r = new Rectangle(8, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    case ContentAlignment.BottomCenter:
                        r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    case ContentAlignment.BottomRight:
                        r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                }
                g.DrawImage(this.myImage, r);
            }
            catch
            {
            }
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
}
