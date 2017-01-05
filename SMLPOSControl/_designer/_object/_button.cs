using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SMLPOSControl._designer._object
{
    public class _button : _posObject
    {
        public object _parentControl;
        public object _ContainerControl;

        public _button()
        {
            this._backColor = Color.Black;
        }

        #region -  Private Variables  -

        private bool calledbykey = false;
        private State mButtonState = State.None;
        private Timer mFadeIn = new Timer();
        private Timer mFadeOut = new Timer();
        private int mGlowAlpha = 0;

        #endregion

        #region -  Property -

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

        #region -  Image  -

        private Image mImage;
        /// <summary>รูปภาพที่ใช้แสดงบนปุ่ม</summary>
        [Category("Image"),
         DefaultValue(null),
         Description("The image displayed on the button that " +
                     "is used to help the user identify" +
                     "it's function if the text is ambiguous.")]
        public Image Image
        {
            get { return mImage; }
            set { mImage = value; this.Invalidate(); }
        }

        private ContentAlignment mImageAlign = ContentAlignment.MiddleLeft;
        /// <summary>ตำแหน่งที่วางรูปภาพบนปุ่ม</summary>
        [Category("Image"),
         DefaultValue(typeof(ContentAlignment), "MiddleLeft"),
         Description("The alignment of the image " +
                     "in relation to the button.")]
        public ContentAlignment ImageAlign
        {
            get { return mImageAlign; }
            set { mImageAlign = value; this.Invalidate(); }
        }

        private Size mImageSize = new Size(24, 24);
        /// <summary>ขนาด Icon</summary>
        [Category("Image"),
         DefaultValue(typeof(Size), "24, 24"),
         Description("The size of the image to be displayed on the" +
                     "button. This property defaults to 24x24.")]
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

        private int mCornerRadius = 8;
        /// <summary>ระยะขอบโค้ง</summary>
        [Category("Appearance"),
         DefaultValue(8),
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

        /// <summary>
        /// กำหนดสีพื้นหลัง (Back Color)
        /// </summary>
        [Category("Appearance")]
        [Description("กำหนดสีพื้นหลัง")]
        [DisplayName("Back Color : กำหนดสีพื้นหลัง")]
        public new Color _backColor
        {
            get
            {
                return base._backColor;
            }
            set
            {
                base._backColor = value;
            }
        }

        /// <summary>
        /// กำหนดสีเส้น (Color)
        /// </summary>
        /// 
        [Category("Appearance")]
        [Description("กำหนดสีเส้น เพื่อความสวยงามแนะนำ ให้กำหนดเหมือนกับสีพิ้นหลัง")]
        [DisplayName("Line Color : กำหนดสีเส้น ")]
        public new Color _lineColor
        {
            get
            {
                return base._lineColor;
            }
            set
            {
                base._lineColor = value;
            }
        }

        /// <summary>กำหนดขนาดเส้น (Pen width)</summary>
        [Category("Appearance")]
        [Description("กำหนดขนาดเส้น")]
        [DisplayName("Pen Width : กำหนดขนาดเส้น")]
        [DefaultValue(1)]
        public new int _penWidth
        {
            get
            {
                return base._penWidth;
            }
            set
            {
                base._penWidth = value;
            }
        }


        private Color mGlowColor = Color.FromArgb(141, 189, 255);
        /// <summary>
        /// The colour that the button glows when
        /// the mouse is inside the client area.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "141,189,255"),
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

        private Color mBaseColor = Color.Black;
        /// <summary>
        /// The backing color that the rest of 
        /// the button is drawn. For a glassier 
        /// effect set this property to Transparent.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "Black"),
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

        public override void _draw(System.Drawing.Graphics g)
        {
            //base._draw(g);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            DrawBackground(g);
            DrawHighlight(g);
            DrawImage(g);
            DrawText(g);
            DrawGlow(g);
            DrawOuterStroke(g);
            DrawInnerStroke(g);
        }

        #region -  Functions  -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r">Rectangle</param>
        /// <param name="r1">Top Left Conner</param>
        /// <param name="r2">Top Right Conner</param>
        /// <param name="r3">Bottom Right Conner</param>
        /// <param name="r4">Bottom Left Conner</param>
        /// <returns></returns>
        public static GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
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
            Rectangle r = this._actualSize;
            //Rectangle r = this.ClientRectangle;
            r.Width -= 1; r.Height -= 1;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (Pen p = new Pen(this._backColor, _penWidth))
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
            Rectangle r = this._actualSize;
            //Rectangle r = this.ClientRectangle;
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
            Rectangle r = this._actualSize;
            //Rectangle r = this.ClientRectangle;
            r.Width--; r.Height--;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (SolidBrush sb = new SolidBrush(this.BaseColor))
                {
                    g.FillPath(sb, rr);
                }
                SetClip(g);
                if (this.BackImage != null) { g.DrawImage(this.BackImage, this._actualSize); }
                //if (this.BackImage != null) { g.DrawImage(this.BackImage, this.ClientRectangle); }
                g.ResetClip();
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, this._backColor)))
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
            //Rectangle rect = new Rectangle(0, 0, this._width, this._height / 2);
            Rectangle rect = new Rectangle(this._actualSize.X, this._actualSize.Y, this._width, this._height / 2);
            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height / 2);
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
                //glow.AddEllipse(-5, this.Height / 2 - 10, this.Width + 11, this.Height + 11);
                glow.AddEllipse(-5, this._height / 2 - 10, this._width + 11, this._height + 11);
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
            StringFormat sf = StringFormatAlignment(this._textAlign);
            //Rectangle r = new Rectangle(8, 8, this.Width - 17, this.Height - 17);
            Rectangle r = new Rectangle(this._actualSize.X + 8, this._actualSize.Y + 8, this._width - 17, this._height - 17);
            //g.DrawString(this.ButtonText, this.Font, new SolidBrush(this.ForeColor), r, sf);
            g.DrawString(this._text, this._font, new SolidBrush(this._foreColor), r, sf);
        }

        /// <summary>
        /// Draws the image for the button
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawImage(Graphics g)
        {
            if (this.Image == null) { return; }
            //Rectangle r = new Rectangle(8, 8, this.ImageSize.Width, this.ImageSize.Height);
            Rectangle r = new Rectangle(this._actualSize.X + 8, this._actualSize.Y + 8, this.ImageSize.Width, this.ImageSize.Height);
            switch (this.ImageAlign)
            {
                case ContentAlignment.TopCenter:
                    //r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, 8, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + (this._width / 2 - this.ImageSize.Width / 2), this._actualSize.Y + 8, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.TopRight:
                    //r = new Rectangle(this.Width - 8 - this.ImageSize.Width, 8, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + (this._width - 8 - this.ImageSize.Width), this._actualSize.Y + 8, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.MiddleLeft:
                    //r = new Rectangle(8, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + 8, this._actualSize.Y +  ( this._height / 2 - this.ImageSize.Height / 2), this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.MiddleCenter:
                    //r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + (this._width / 2 - this.ImageSize.Width / 2), (this._height / 2 - this.ImageSize.Height / 2) + this._actualSize.Y, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.MiddleRight:
                    //r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + (this._width - 8 - this.ImageSize.Width), (this._height / 2 - this.ImageSize.Height / 2) + this._actualSize.Y, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.BottomLeft:
                    //r = new Rectangle(8, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + 8,this._actualSize.Y + ( this._height - 8 - this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.BottomCenter:
                    //r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + (this._width / 2 - this.ImageSize.Width / 2),this._actualSize.Y + ( this._height - 8 - this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.BottomRight:
                    //r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    r = new Rectangle(this._actualSize.X + (this._width - 8 - this.ImageSize.Width),this._actualSize.Y + ( this._height - 8 - this.ImageSize.Height), this.ImageSize.Width, this.ImageSize.Height);
                    break;
            }
            g.DrawImage(this.Image, r);
        }

        private void SetClip(Graphics g)
        {
            //Rectangle r = this.ClientRectangle;
            Rectangle r = this._actualSize;
            r.X++; r.Y++; r.Width -= 3; r.Height -= 3;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                g.SetClip(rr);
            }
        }

        #endregion


        public void Invalidate()
        {
            if (this._parentControl != null)
            {
                ((_drawPanel)_parentControl).Invalidate();
            }
        }
    }
}
