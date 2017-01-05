using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Calendar
{
    public partial class _dayControl : UserControl
    {
        public int _row = 0;
        public int _column = 0;
        public DateTime _date = new DateTime();
        public Color _colorBegin = Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        public Color _colorEnd = Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        public Boolean _showBackground = true;

        public _dayControl(DateTime date, int row, int column)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this._date = date;
            this._row = row;
            this._column = column;
            this._dayTitle.Text = date.Day.ToString();
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this._showBackground)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                LinearGradientBrush __GBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), _colorBegin, _colorEnd);
                Rectangle __rect = new Rectangle(0, 0, this.Width, this.Height);
                e.Graphics.FillRectangle(__GBrush, __rect);
                __GBrush.Dispose();
            }
        }
    }
}
