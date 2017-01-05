using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

namespace SMLERPControl._flow
{
    public class _flowObject : PictureBox
    {
        private Image _picture;
        private Boolean _mouseHover = false;
        private Size __originalSizeResult;
        private Point __originalLocationResult;

        public _flowObject()
        {
            this.LocationChanged += new EventHandler(_flowObject_LocationChanged);
            this.SizeChanged += new EventHandler(_flowObject_SizeChanged);
        }

        void _flowObject_SizeChanged(object sender, EventArgs e)
        {
            this.SizeChanged -= new EventHandler(_flowObject_SizeChanged);
            this.__originalSizeResult = this.Size;
        }

        void _flowObject_LocationChanged(object sender, EventArgs e)
        {
            this.LocationChanged -= new EventHandler(_flowObject_LocationChanged);
            this.__originalLocationResult = this.Location;
        }

        [Category("_SML")]
        [Description("ขนาดเดิม")]
        public Size OriginalSize
        {
            get
            {
                return this.__originalSizeResult;
            }
        }

        [Category("_SML")]
        [Description("ตำแหน่งเดิม")]
        public Point OriginalLocation
        {
            get
            {
                return this.__originalLocationResult;
            }
        }

        protected override void OnMouseHover(EventArgs e)
        {
            this._mouseHover = true;
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.LightYellow;
            base.OnMouseHover(e);
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this._mouseHover = false;
            this.Cursor = Cursors.Default;
            this.BackColor = Color.Transparent;
            base.OnMouseLeave(e);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics __g = pe.Graphics;
            __g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            float __fontSize = 8F * (float)((float)this.Width / (float)this.OriginalSize.Width);
            if (__fontSize > 12F)
            {
                __fontSize = 12F;
            }
            if (__fontSize > 0)
            {
                System.Drawing.Font __font = new System.Drawing.Font("Arial", __fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                SizeF __stringSize = __g.MeasureString(this.Text, __font);
                int __x = (this.Width - this._picture.Width) / 2;
                float __y = this.Height - (int)((__stringSize.Height * 2) + 5);
                __g.DrawImage(this._picture, 5, 5, (this.Width - (int)(this.Width * 0.1)) - 5, __y);
                string[] __str = this.Text.Split(' ');
                for (int __row = 0; __row < __str.Length; __row++)
                {
                    __stringSize = __g.MeasureString(__str[__row], __font);
                    __g.DrawString(__str[__row], __font, new SolidBrush((this._mouseHover) ? Color.Blue : Color.Black), ((this.Width - __stringSize.Width) / 2)+2, __y + 5);
                    __y = __y + (__stringSize.Height - 2);
                }
                if (this._mouseHover)
                {
                    Point[] __point = {
                        new Point(0,0),
                        new Point(0,this.Height-1),
                        new Point(this.Width-1,this.Height-1),
                        new Point(this.Width-1,0),
                        new Point(0,0)
                    };
                    __g.DrawLines(new Pen(Color.RoyalBlue, 1), __point);
                }
            }
            base.OnPaint(pe);
        }

        [Category("_SML")]
        [Description("รูปภาพ")]
        public Image Image
        {
            get
            {
                return this._picture;
            }
            set
            {
                this._picture = value;
                this.Invalidate();
            }
        }
    }
}
