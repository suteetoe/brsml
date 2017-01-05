using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MyLib
{
    public partial class VistaButton : Button
    {
        public TreeNodeCollection _next = null;
        public int _level = -1;
        public Boolean _haveNodes = false;

        public VistaButton()
        {
            this.SuspendLayout();
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            mFadeIn.Interval = 30;
            mFadeOut.Interval = 30;
            this.ResumeLayout();
        }
    }
}