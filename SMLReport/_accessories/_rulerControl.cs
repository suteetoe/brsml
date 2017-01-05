using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._design
{
    public partial class _rulerControl : UserControl
    {
        private bool _verticalResult = false;
        private _measurementUnitType _unitResult = _measurementUnitType.Centimeters;
        private float _beginValueResult = 0.0f;
        private float _ruleScaleResult = 1f;

        public _rulerControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);

            this.Paint += new PaintEventHandler(_rulerControl_Paint);
            this.BackColor = Color.Transparent;
        }

        void _rulerControl_Paint(object sender, PaintEventArgs e)
        {
            Color __colorBegin = Color.FromArgb(0xFB, 0xFB, 0xFB);
            Color __colorEnd = Color.FromArgb(0xC7, 0xC7, 0xC7);
            LinearGradientBrush __brush;
            if (_verticalResult)
            {
                __brush = new LinearGradientBrush(new Point(0, 0), new Point(this.Width, 0), __colorBegin, __colorEnd);
            }
            else
            {
                __brush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), __colorBegin, __colorEnd);
            }
            Graphics __g = e.Graphics;
            __g.SmoothingMode = SmoothingMode.HighQuality;
            Pen __pen = new Pen(Color.Gray, 0);
            Pen __penBlack = new Pen(Color.Black, 0);
            Pen __penRed = new Pen(Color.Red, 0);

            __g.FillRectangle(__brush, 0, 0, this.Width, this.Height);
            float __ratio = _pageSetup._convertUnitToPixel(_unitResult);
            int __rulerLength = (int)(((this._verticalResult) ? this.Height : this.Width) / __ratio);
            if (__rulerLength < 100)
            {
                __rulerLength = 1000;
            }
            float __lineScale = 0.01f;
            float __lineBegin = _beginValue * _ruleScaleResult;
            float __lineEnd = (_beginValue + __rulerLength) * _ruleScaleResult;
            int __lastStringPoint = -200;
            int __newPoint = 0;
            for (float __loop = __lineBegin; __loop < __lineEnd && __newPoint < ((this._verticalResult) ? this.Height : this.Width); __loop = (float)Math.Round(__loop + __lineScale, 2))
            {
                __newPoint = (int)(Math.Abs((__loop - __lineBegin) / __ratio)) + 1;
                int __lineLength = 12;
                float __getRound = (float)Math.Abs(Math.Round(__loop / _ruleScaleResult, 2));
                float __get1 = (float)Math.Abs(Math.Round(__getRound % 0.2f, 2));
                float __get2 = (float)Math.Abs(Math.Round(__getRound % 0.1f, 2));
                if (__getRound % 1f == 0f)
                {
                    if (__newPoint > __lastStringPoint + 20)
                    {
                        int __strStart = (int)Math.Round(__loop / _ruleScaleResult, 2);
                        SizeF __stringSize = __g.MeasureString(__strStart.ToString(), MyLib._myGlobal._myFont);
                        if (_verticalResult)
                        {
                            __g.TranslateTransform(0, __newPoint + (int)__stringSize.Width);
                            __g.RotateTransform(-90);
                            __g.DrawString(__strStart.ToString(), MyLib._myGlobal._myFont, new SolidBrush(Color.Black), 0, 0);
                            __g.ResetTransform();
                        }
                        else
                        {
                            __g.DrawString(__strStart.ToString(), MyLib._myGlobal._myFont, new SolidBrush(Color.Black), __newPoint + 1, 0);
                        }
                        __lastStringPoint = __newPoint;
                    }
                    __lineLength = 0;
                }
                else
                    if (__get1 == 0f || __get1 == 0.2f)
                    {
                        __lineLength = 10;
                    }
                    else
                        if (__getRound % 0.5f == 0)
                        {
                            __lineLength = 8;
                        }
                if (__get2 == 0f || __get2 == 0.1f)
                {
                    if (_verticalResult)
                    {
                        __g.DrawLine(__pen, new Point(__lineLength, __newPoint), new Point(this.Width, __newPoint));
                    }
                    else
                    {
                        __g.DrawLine(__pen, new Point(__newPoint, __lineLength), new Point(__newPoint, this.Height));
                    }
                }
            }
            if (_verticalResult)
            {
                __g.DrawLine(__penBlack, new Point(this.Width - 1, 0), new Point(this.Width - 1, this.Height));
            }
            else
            {
                __g.DrawLine(__penBlack, new Point(0, this.Height - 1), new Point(this.Width, this.Height - 1));
            }

            __penRed.Dispose();
            __penBlack.Dispose();
            __pen.Dispose();
            __brush.Dispose();
        }

        [Category("_SML")]
        [Description("อัตราส่วน (ZOOM)")]
        [DisplayName("RuleScale : อัตราส่วน")]
        [DefaultValue(1)]
        public float _ruleScale
        {
            get
            {
                return _ruleScaleResult;
            }
            set
            {
                _ruleScaleResult = value;
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("แสดงในแนวตั้ง")]
        [DisplayName("Vertical : แสดงในแนวตั้ง")]
        [DefaultValue(false)]
        public bool _vertical
        {
            get
            {
                return _verticalResult;
            }
            set
            {
                _verticalResult = value;
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("ค่าแรก")]
        [DisplayName("BeginValue : ค่าแรก")]
        [DefaultValue(0)]
        public float _beginValue
        {
            get
            {
                return _beginValueResult;
            }
            set
            {
                _beginValueResult = value;
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("หน่วย")]
        [DisplayName("Unit : หน่วย")]
        [DefaultValue(_measurementUnitType.Centimeters)]
        public _measurementUnitType _unit
        {
            get
            {
                return _unitResult;
            }
            set
            {
                _unitResult = value;
                this.Invalidate();
            }
        }

        private void _rulerControl_Load(object sender, EventArgs e)
        {

        }
    }
}
