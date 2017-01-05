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
    public class _arrowObject : PictureBox
    {
        private Point __pointStartResult;
        private Point __pointStopResult;
        private _arrowObjectStyle __style;
        private int __arrowWidth = 2;
        private Color __arrowColor = Color.Gray;
        private Size __originalSizeResult;
        private Point __originalLocationResult;

        public _arrowObject()
        {
            this.BackColor = Color.Transparent;
            this.SizeChanged += new EventHandler(_arrowObject_SizeChangedFirst);
            this.SizeChanged += new EventHandler(_arrowObject_SizeChanged);
            this.LocationChanged += new EventHandler(_arrowObject_LocationChanged);
        }

        void _arrowObject_LocationChanged(object sender, EventArgs e)
        {
            this.LocationChanged -= new EventHandler(_arrowObject_LocationChanged);
            this.__originalLocationResult = this.Location;
        }

        void _arrowObject_SizeChangedFirst(object sender, EventArgs e)
        {
            this.SizeChanged -= new EventHandler(_arrowObject_SizeChangedFirst);
            this.__originalSizeResult = this.Size;
        }

        void _arrowObject_SizeChanged(object sender, EventArgs e)
        {
            __recalc();
        }

        public void __recalc()
        {
            int __calc;
            switch (this.ArrowStyle)
            {
                case _arrowObjectStyle.LeftToRightCenter:
                    __calc = (this.Height / 2) - (this.ArrowWidth / 2);
                    this.__pointStartResult = new Point(0, __calc);
                    this.__pointStopResult = new Point(this.Width - (this.ArrowWidth * 2), __calc);
                    break;
                case _arrowObjectStyle.RightToLeftCenter:
                    __calc = (this.Height / 2) - (this.ArrowWidth / 2);
                    this.__pointStartResult = new Point(this.Width, __calc);
                    this.__pointStopResult = new Point((this.ArrowWidth * 2), __calc);
                    break;
                case _arrowObjectStyle.TopToDownCenter:
                    __calc = (this.Width / 2) - (this.ArrowWidth / 2);
                    this.__pointStartResult = new Point(__calc, 0);
                    this.__pointStopResult = new Point(__calc, this.Height - (this.ArrowWidth * 2));
                    break;
                case _arrowObjectStyle.DownToTopCenter:
                    __calc = (this.Width / 2) - (this.ArrowWidth / 2);
                    this.__pointStopResult = new Point(__calc, 0);
                    this.__pointStartResult = new Point(__calc, this.Height - (this.ArrowWidth * 2));
                    break;
            }
            this.Invalidate();
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

        [Category("_SML")]
        [Description("สีเส้น")]
        public Color ArrowColor
        {
            get
            {
                return this.__arrowColor;
            }
            set
            {
                this.__arrowColor = value;
                __recalc();
            }
        }

        [Category("_SML")]
        [Description("ความกว้างเส้น")]
        public int ArrowWidth
        {
            get
            {
                return this.__arrowWidth;
            }
            set
            {
                this.__arrowWidth = value;
                __recalc();
            }
        }

        [Category("_SML")]
        [Description("รูปแบบ")]
        public _arrowObjectStyle ArrowStyle
        {
            get
            {
                return this.__style;
            }
            set
            {
                this.__style = value;
                __recalc();
            }
        }

        [Category("_SML")]
        [Description("จุดเริ่มต้น")]
        public Point PointStart
        {
            get
            {
                return this.__pointStartResult;
            }
            set
            {
                this.__pointStartResult = value;
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("จุดสิ้นสุด")]
        public Point PointStop
        {
            get
            {
                return this.__pointStopResult;
            }
            set
            {
                this.__pointStopResult = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics __g = pe.Graphics;
            __g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen __pen = new Pen(this.ArrowColor, this.ArrowWidth);
            switch (this.ArrowStyle)
            {
                case _arrowObjectStyle.LeftToRightCenter:
                    __g.DrawLine(__pen, this.PointStart, this.PointStop);
                    __g.DrawLine(__pen, this.PointStop.X - 5, this.PointStop.Y - 5, this.PointStop.X, this.PointStop.Y);
                    __g.DrawLine(__pen, this.PointStop.X - 5, this.PointStop.Y + 5, this.PointStop.X, this.PointStop.Y);
                    break;
                case _arrowObjectStyle.RightToLeftCenter:
                    __g.DrawLine(__pen, this.PointStart, this.PointStop);
                    __g.DrawLine(__pen, this.PointStop.X + 5, this.PointStop.Y - 5, this.PointStop.X, this.PointStop.Y);
                    __g.DrawLine(__pen, this.PointStop.X + 5, this.PointStop.Y + 5, this.PointStop.X, this.PointStop.Y);
                    break;
                case _arrowObjectStyle.TopToDownCenter:
                    __g.DrawLine(__pen, this.PointStart, this.PointStop);
                    __g.DrawLine(__pen, this.PointStop.X - 5, this.PointStop.Y - 5, this.PointStop.X, this.PointStop.Y);
                    __g.DrawLine(__pen, this.PointStop.X + 5, this.PointStop.Y - 5, this.PointStop.X, this.PointStop.Y);
                    break;
                case _arrowObjectStyle.DownToTopCenter:
                    __g.DrawLine(__pen, this.PointStart, this.PointStop);
                    __g.DrawLine(__pen, this.PointStop.X - 5, this.PointStop.Y + 5, this.PointStop.X, this.PointStop.Y);
                    __g.DrawLine(__pen, this.PointStop.X + 5, this.PointStop.Y + 5, this.PointStop.X, this.PointStop.Y);
                    break;
            }
        }
    }

    public enum _arrowObjectStyle
    {
        LeftToRightCenter,
        RightToLeftCenter,
        TopToDownCenter,
        DownToTopCenter
    }
}
