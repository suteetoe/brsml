using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SMLReport._design;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace SMLPOSControl._designer._object
{
    public class _posSearchLevel : _posItemPanel
    {
        public _posSearchLevel()
        {
        }
    }

    /// <summary>Panel สินค้า</summary>
    public class _posItemPanel : _posObject
    {
        private int _buttonWidthResult = 60;
        private int _buttonHeightResult = 60;

        private bool _showNextPreviousPageResult;
        private bool _showHomeButtonResult;

        private Padding _paddingResult = new Padding(0);
        private Padding _buttonMarginResult = new Padding(3);
        //private Padding _buttonCornerRadiusResult = new Padding(3);
        private int _buttonCornerRadiusResult = 3;
        /// <summary>Glow Alpha ของปุ่ม</summary>
        private int mGlowAlpha = 0;

        public int _buttonWidth
        {
            get { return _buttonWidthResult; }
            set { _buttonWidthResult = value; }
        }

        public int _buttonHeight
        {
            get { return _buttonHeightResult; }
            set { _buttonHeightResult = value; }
        }

        public bool _showNextPreviousPage
        {
            get { return _showNextPreviousPageResult; }
            set { _showNextPreviousPageResult = value; }
        }

        public bool _showHomeButton
        {
            get { return _showHomeButtonResult; }
            set { _showHomeButtonResult = value; }
        }

        public Padding _buttonMargin
        {
            get { return _buttonMarginResult; }
            set
            {
                _buttonMarginResult = value;
            }
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
            set { mBaseColor = value; }
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
            set { mHighlightColor = value; }
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
            set { mGlowColor = value;}
        }

        private Color _buttonBackColorResult = Color.Black;
        public Color _buttonBackColor
        {
            get { return _buttonBackColorResult; }
            set { _buttonBackColorResult = value; }
        }


        //public Padding _buttonCornerRadius
        //{
        //    get { return _buttonCornerRadiusResult; }
        //    set { _buttonCornerRadiusResult = value; }
        //}
        public int _buttonCornerRadius
        {
            get { return _buttonCornerRadiusResult; }
            set { _buttonCornerRadiusResult = value; }
        }

        public _posItemPanel()
        {

        }

        public override void _draw(System.Drawing.Graphics g)
        {
            //base._draw(g);
            RectangleF __drawStrRect = _drawRectangle._getNormalizedRectangle(this._actualSize);
            SolidBrush __BgBrush = new SolidBrush(this._backColor);

            g.FillRectangle(__BgBrush, Rectangle.Round(__drawStrRect));

            _drawButtonInner(g);

        }

        // draw เหมือน flowlayout Control
        /// <summary>จำนวน column ปุ่ม</summary>
        private int _buttonCol;
        /// <summary>จำนวนแถวปุ่ม</summary>
        private int _buttonRow;

        public int _getInnerPanelWidth
        {
            get
            {
                return this._width - (this._padding.Left + this._padding.Right);
            }
        }

        public int _getInnerPanelHeight
        {
            get
            {
                return this._height - (this._padding.Top + this._padding.Bottom);
            }
        }

        /// <summary>
        /// วาดปุ่มข้างใน
        /// </summary>
        /// <param name="g">Graphic</param>

        private void _drawButtonInner(Graphics g)
        {
            // หาจำนวนปุ่ม แนวตั้ง และ แนวนอน
            _buttonCol = (int)(this._getInnerPanelWidth / (this._buttonWidthResult + _buttonMarginResult.Left + _buttonMarginResult.Right));
            _buttonRow = (int)(this._getInnerPanelHeight / (this._buttonHeightResult + _buttonMarginResult.Top + _buttonMarginResult.Bottom));

            //draw ปุ่ม
            for (int __row = 0; __row < _buttonRow; __row++)
            {
                for (int __col = 0; __col < _buttonCol; __col++)
                {
                    // draw location อะไร
                    Point __point = new Point(__col * (this._buttonWidthResult + _buttonMarginResult.Left + _buttonMarginResult.Right), __row * (this._buttonHeightResult + _buttonMarginResult.Top + _buttonMarginResult.Bottom));
                    __point.X += (this._actualSize.X + this._padding.Left);
                    __point.Y += (this._actualSize.Y + this._padding.Top);
                    _drawButton(g, __point);

                }
            }

        }

        /// <summary>
        /// วาดปุ่มข้างใน item panel ขาดคุณสมบัติของสีปุ่ม
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__drawPoint"></param>
        void _drawButton(Graphics g, Point __drawPoint)
        {
            //Pen __pen = new Pen(new SolidBrush(Color.Black));
            _drawButtonBackGround(g, __drawPoint);
            _drawButtonHighlight(g, __drawPoint);
            _drawButtionText(g, __drawPoint);
            _drawButtonGlow(g, __drawPoint); // ไล่โทนสี เช็คใหม่ มันผิด
            _drawButtonOuterStroke(g, __drawPoint);
            _drawButtonInnerStroke(g, __drawPoint);

            //g.DrawRectangle(__pen, __buttonRect);
            //g.DrawRectangle(__pen, 
        }

        private void _drawButtonBackGround(Graphics g, Point __drawPoint)
        {
            Rectangle __buttonRect = new Rectangle(__drawPoint, new Size(this._buttonWidthResult, this._buttonHeightResult));

            //if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            //int alpha = (mButtonState == State.Pressed) ? 204 : 127;
            //Rectangle r = this._actualSize;
            //Rectangle r = this.ClientRectangle;
            __buttonRect.Width--; __buttonRect.Height--;
            using (GraphicsPath rr = _designer._object._button.RoundRect(__buttonRect, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult))
            {
                using (SolidBrush sb = new SolidBrush(this.BaseColor))
                {
                    g.FillPath(sb, rr);
                }
                SetClip(g, __drawPoint);
                //if (this.BackImage != null) { g.DrawImage(this.BackImage, this._actualSize); }
                //if (this.BackImage != null) { g.DrawImage(this.BackImage, this.ClientRectangle); }
                g.ResetClip();
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(127, this._buttonBackColor)))
                {
                    g.FillPath(sb, rr);
                }
            }
        }

        private void _drawButtonHighlight(Graphics g, Point __drawPoint)
        {
            Rectangle __buttonRect = new Rectangle(__drawPoint, new Size(this._buttonWidthResult, this._buttonHeightResult / 2));

            //if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            int alpha = 150;// (mButtonState == State.Pressed) ? 60 : 150;
            //Rectangle rect = new Rectangle(0, 0, this._width, this._height / 2);
            //Rectangle rect = new Rectangle(this._actualSize.X, this._actualSize.Y, this._width, this._height / 2);
            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height / 2);
            using (GraphicsPath r = _designer._object._button.RoundRect(__buttonRect, _buttonCornerRadiusResult, _buttonCornerRadiusResult, 0, 0))
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

        private void _drawButtonGlow(Graphics g, Point __drawPoint)
        {
            //if (this.mButtonState == State.Pressed) { return; }
            Rectangle __buttonRect = new Rectangle(__drawPoint, new Size(this._buttonWidthResult, this._buttonHeightResult));
            SetClip(g, __drawPoint);
            using (GraphicsPath glow = _designer._object._button.RoundRect(__buttonRect, _buttonCornerRadiusResult, _buttonCornerRadiusResult, 0, 0)) //new GraphicsPath())
            {
                //glow.AddEllipse(-5, this.Height / 2 - 10, this.Width + 11, this.Height + 11);
                glow.AddEllipse(__drawPoint.X - 5, __drawPoint.Y + (this._buttonHeightResult / 2 - 10), this._buttonWidthResult + 11, this._buttonHeightResult + 11);
                using (PathGradientBrush gl = new PathGradientBrush(glow))
                {
                    gl.CenterColor = Color.FromArgb(mGlowAlpha, this.GlowColor);
                    gl.SurroundColors = new Color[] { Color.FromArgb(0, this.GlowColor) };
                    g.FillPath(gl, glow);
                }
            }
            g.ResetClip();
        }

        private void _drawButtonOuterStroke(Graphics g, Point __drawPoint)
        {
            //if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            //Rectangle r = this._actualSize;
            Rectangle __buttonRect = new Rectangle(__drawPoint, new Size(this._buttonWidthResult, this._buttonHeightResult));
            //Rectangle r = this.ClientRectangle;
            __buttonRect.Width -= 1; __buttonRect.Height -= 1;
            using (GraphicsPath rr = _designer._object._button.RoundRect(__buttonRect, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult))
            {
                using (Pen p = new Pen(this._buttonBackColor, _penWidth))
                {
                    g.DrawPath(p, rr);
                }
            }
        }

        private void _drawButtonInnerStroke(Graphics g, Point __drawPoint)
        {
            //if (this.ButtonStyle == Style.Flat && this.mButtonState == State.None) { return; }
            Rectangle __buttonRect = new Rectangle(__drawPoint, new Size(this._buttonWidthResult, this._buttonHeightResult));
            //Rectangle r = this.ClientRectangle;
            __buttonRect.X++; __buttonRect.Y++;
            __buttonRect.Width -= 3; __buttonRect.Height -= 3;
            using (GraphicsPath rr = _designer._object._button.RoundRect(__buttonRect, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult))
            {
                using (Pen p = new Pen(this.HighlightColor))
                {
                    g.DrawPath(p, rr);
                }
            }
        }

        private void _drawButtionText(Graphics g, Point __drawPoint)
        {
            StringFormat __sf = StringFormat.GenericDefault;
            __sf.Alignment = MyLib._myGlobal._getStringFormatTextAlignment(this._textAlign);
            __sf.LineAlignment = MyLib._myGlobal._getStringFormatTextLineAlignment(this._textAlign);

            //Rectangle r = new Rectangle(8, 8, this.Width - 17, this.Height - 17);
            Rectangle r = new Rectangle(__drawPoint.X + 8, __drawPoint.Y + 8, this._buttonWidth - 17, this._buttonHeight - 17);
            //g.DrawString(this.ButtonText, this.Font, new SolidBrush(this.ForeColor), r, sf);
            g.DrawString("Items", this._font, new SolidBrush(this._foreColor), r, __sf);
        }

        private void SetClip(Graphics g, Point __drawPoint)
        {
            //Rectangle r = this.ClientRectangle;
            Rectangle r = new Rectangle(__drawPoint, new Size(this._buttonWidth, this._buttonHeight));
            r.X++; r.Y++; r.Width -= 3; r.Height -= 3;
            using (GraphicsPath rr = _designer._object._button.RoundRect(r, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult, _buttonCornerRadiusResult))
            {
                g.SetClip(rr);
            }
        }

    }
}
