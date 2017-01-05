using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace MyLib
{
    public class _myPanelVista : System.Windows.Forms.UserControl
    {
        #region -  Designer  -

        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initialize the component with it's
        /// default settings.
        /// </summary>
        public _myPanelVista()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
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
            this.SuspendLayout();
            // 
            // _panelVista
            // 
            this.Name = "_panelVista";
            this.Size = new System.Drawing.Size(155, 39);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.VistaPanel_Paint);
            this.Resize += new System.EventHandler(this._panelVista_Resize);
            this.ResumeLayout(false);

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
        /// the panel background is painted when the 
        /// mouse is not inside the ClientArea.
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// Draw the panel as normal
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

        private State mPanelState = State.None;      
        private int mGlowAlpha = 0;

        #endregion

        #region -  Text  -

        private string mText;
        /// <summary>
        /// The text that is displayed on the panel.
        /// </summary>
        [Category("Text"),
         Description("The text that is displayed on the panel.")]
        public string TopText
        {
            get { return mText; }
            set { mText = value; this.Invalidate(); }
        }
       
        private System.Drawing.Font _defaultTopFont = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178, false);
        [Category("Text"),
         DefaultValue(typeof(ContentAlignment), "MiddleCenter"),
         Description("The alignment of the panel text " +
                     "that is displayed on the control.")]
        public Font TopFont
        {
            get { return _defaultTopFont; }
            set
            {
                _defaultTopFont = value;
                this.Invalidate();
            }
        }

        private Color mTopForeColor = Color.Blue;
        /// <summary>
        /// The color with which the text is drawn.
        /// </summary>
        [Category("Text"),
         Browsable(true),
         DefaultValue(typeof(Color), "Blue"),
         Description("The color with which the text is drawn.")]
        public Color TopForeColor
        {
            get { return mTopForeColor; }
            set { mTopForeColor = value; this.Invalidate(); }
        }

        private ContentAlignment mTopTextAlign = ContentAlignment.TopLeft;
        /// <summary>
        /// The alignment of the panel text
        /// that is displayed on the control.
        /// </summary>
        [Category("Text"),
         DefaultValue(typeof(ContentAlignment), "MiddleCenter"),
         Description("The alignment of the panel text " +
                     "that is displayed on the control.")]
        public ContentAlignment TopTextAlign
        {
            get { return mTopTextAlign; }
            set { mTopTextAlign = value; this.Invalidate(); }
        }

        private string rText;
        [Category("Text"), Description("The text that is displayed on the panel.")]
        public string RightText
        {
            get { return rText; }
            set { rText = value; this.Invalidate(); }
        }

        private System.Drawing.Font _defaultRightFont = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178, false);
        [Category("Text"),
         DefaultValue(typeof(Font), "Tahoma"),
         Description("The alignment of the panel text " +
                     "that is displayed on the control.")]
        public Font RightFont
        {
            get { return _defaultRightFont; }
            set
            {
                _defaultRightFont = value;
                this.Invalidate();
            }
        }

        private Color mRightForeColor = Color.Blue;
        /// <summary>
        /// The color with which the text is drawn.
        /// </summary>
        [Category("Text"),
         Browsable(true),
         DefaultValue(typeof(Color), "Blue"),
         Description("The color with which the text is drawn.")]
        public Color RightForeColor
        {
            get { return mRightForeColor; }
            set { mRightForeColor = value; this.Invalidate(); }
        }
       
        

        private ContentAlignment mRightTextAlign = ContentAlignment.MiddleRight;
        /// <summary>
        /// The alignment of the panel text
        /// that is displayed on the control.
        /// </summary>
        [Category("Text"),
         DefaultValue(typeof(ContentAlignment), "MiddleCenter"),
         Description("The alignment of the panel text " +
                     "that is displayed on the control.")]
        public ContentAlignment RightTextAlign
        {
            get { return mRightTextAlign; }
            set { mRightTextAlign = value; this.Invalidate(); }
        }
        
        #endregion

        #region -  Image  -

        private Image mImage;
        /// <summary>
        /// The image displayed on the panel that 
        /// is used to help the user identify
        /// it's function if the text is ambiguous.
        /// </summary>
        [Category("Image"),
         DefaultValue(null),
         Description("The image displayed on the panel that " +
                     "is used to help the user identify" +
                     "it's function if the text is ambiguous.")]
        public Image Image
        {
            get { return mImage; }
            set { mImage = value; this.Invalidate(); }
        }

        private ContentAlignment mImageAlign = ContentAlignment.MiddleLeft;
        /// <summary>
        /// The alignment of the image 
        /// in relation to the panel.
        /// </summary>
        [Category("Image"),
         DefaultValue(typeof(ContentAlignment), "MiddleLeft"),
         Description("The alignment of the image " +
                     "in relation to the panel.")]
        public ContentAlignment ImageAlign
        {
            get { return mImageAlign; }
            set { mImageAlign = value; this.Invalidate(); }
        }

        private Size mImageSize = new Size(24, 24);
        /// <summary>
        /// The size of the image to be displayed on the
        /// panel. This property defaults to 24x24.
        /// </summary>
        [Category("Image"),
         DefaultValue(typeof(Size), "24, 24"),
         Description("The size of the image to be displayed on the" +
                     "panel. This property defaults to 24x24.")]
        public Size ImageSize
        {
            get { return mImageSize; }
            set { mImageSize = value; this.Invalidate(); }
        }

        #endregion

        #region -  Appearance  -

        private Style mPanelStyle = Style.Default;
        /// <summary>
        /// Sets whether the panel background is drawn 
        /// while the mouse is outside of the client area.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Style), "Default"),
         Description("Sets whether the panel background is drawn " +
                     "while the mouse is outside of the client area.")]
        public Style PanelStyle
        {
            get { return mPanelStyle; }
            set { mPanelStyle = value; this.Invalidate(); }
        }

        private int mCornerRadius = 8;
        /// <summary>
        /// The radius for the panel corners. The 
        /// greater this value is, the more 'smooth' 
        /// the corners are. This property should
        ///  not be greater than half of the 
        ///  controls height.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(8),
         Description("The radius for the panel corners. The " +
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
        /// The colour of the highlight on the top of the panel.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "White"),
         Description("The colour of the highlight on the top of the panel.")]
        public Color HighlightColor
        {
            get { return mHighlightColor; }
            set { mHighlightColor = value; this.Invalidate(); }
        }

        private Color mPanelColor = Color.Lavender;
        /// <summary>
        /// The bottom color of the panel that 
        /// will be drawn over the base color.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "Transparent"),
         Description("The bottom color of the panel that " +
                     "will be drawn over the base color.")]
        public Color PanelColor
        {
            get { return mPanelColor; }
            set { mPanelColor = value; this.Invalidate(); }
        }

        private Color mGlowColor = Color.FromArgb(141, 189, 255);
        /// <summary>
        /// The colour that the panel glows when
        /// the mouse is inside the client area.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "141,189,255"),
         Description("The colour that the panel glows when " +
                     "the mouse is inside the client area.")]
        public Color GlowColor
        {
            get { return mGlowColor; }
            set { mGlowColor = value; this.Invalidate(); }
        }

        private Image mBackImage;
        /// <summary>
        /// The background image for the panel, 
        /// this image is drawn over the base 
        /// color of the panel.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(null),
         Description("The background image for the panel, " +
                     "this image is drawn over the base " +
                     "color of the panel.")]
        public Image BackImage
        {
            get { return mBackImage; }
            set { mBackImage = value; this.Invalidate(); }
        }

        private Color mBaseColor = Color.White;
        /// <summary>
        /// The backing color that the rest of 
        /// the panel is drawn. For a glassier 
        /// effect set this property to Transparent.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "Transparent"),
         Description("The backing color that the rest of" +
                     "the panel is drawn. For a glassier " +
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
        /// using the panelColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawOuterStroke(Graphics g)
        {
            if (this.PanelStyle == Style.Flat && this.mPanelState == State.None) { return; }
            Rectangle r = this.ClientRectangle;
            r.Width -= 1; r.Height -= 1;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (Pen p = new Pen(this.PanelColor))
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
            if (this.PanelStyle == Style.Flat && this.mPanelState == State.None) { return; }
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
            if (this.PanelStyle == Style.Flat && this.mPanelState == State.None) { return; }
            int alpha = (mPanelState == State.Pressed) ? 204 : 127;
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
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, this.PanelColor)))
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
            if (this.PanelStyle == Style.Flat && this.mPanelState == State.None) { return; }
            int alpha = (mPanelState == State.Pressed) ? 60 : 150;
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
        /// Draws the glow for the panel when the
        /// mouse is inside the client area using
        /// the GlowColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawGlow(Graphics g)
        {
            if (this.mPanelState == State.Pressed) { return; }
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
        /// Draws the text for the panel.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawText(Graphics g)
        {
            StringFormat sf = StringFormatAlignment(this.TopTextAlign);
            StringFormat _sf = StringFormatAlignment(this.RightTextAlign);
           // Rectangle r = new Rectangle(5, 5, this.Width - 17, this.Height - 2);
            Rectangle r = new Rectangle(0, 0, this.Width-17, this.Height - 17);
            Rectangle rr = new Rectangle(8, 8, this.Width - 17, this.Height - 17);
            g.DrawString(this.TopText, this.TopFont, new SolidBrush(this.TopForeColor), r, sf); //ContentAlignment.MiddleRight
            g.DrawString(this.RightText,this.RightFont,new SolidBrush(this.RightForeColor), rr, _sf);

        }

        /// <summary>
        /// Draws the image for the panel
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawImage(Graphics g)
        {
            if (this.Image == null) { return; }
            Rectangle r = new Rectangle(8, 8, this.ImageSize.Width, this.ImageSize.Height);
            switch (this.ImageAlign)
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
            g.DrawImage(this.Image, r);
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

        private void VistaPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            DrawBackground(e.Graphics);
            DrawHighlight(e.Graphics);
            DrawImage(e.Graphics);
            DrawText(e.Graphics);
            DrawGlow(e.Graphics);
            DrawOuterStroke(e.Graphics);
            DrawInnerStroke(e.Graphics);
        }

        private void _panelVista_Resize(object sender, EventArgs e)
        {
            Rectangle r = this.ClientRectangle;
            r.X -= 1; r.Y -= 1;
            r.Width += 2; r.Height += 2;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                this.Region = new Region(rr);
            }
        }

        #endregion
    }
}
