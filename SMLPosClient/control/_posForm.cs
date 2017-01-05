using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SMLPosClient.control
{
    public partial class _posForm : Form
    {
        public _posForm()
        {
            InitializeComponent();
        }

        /// <summary>ตรวจว่ามี object ตัวไหน set parent ได้บ้าง</summary>
        public void _processParentControl()
        {
            // check parent object
            // 1.หาว่ามี container control หรือเปล่า ( panel )
            // 2.ถ้ามี หา object ที่ อยู่ใน อยู่ใน control นั้น แล้ว set parent มาที่ panel นี้
            List<Control> __panelControl = new List<Control>();

            int __controlCount = this.Controls.Count;
            for (int __i = 0; __i < __controlCount; __i++)
            {
                Control __obj = this.Controls[__i];
                if (__obj.GetType() == typeof(control._posPanel))
                {
                    __panelControl.Add(__obj);
                }
            }

            // for panel 
            for (int __i = 0; __i < __panelControl.Count; __i++)
            {
                List<Control> __innerControl = new List<Control>();

                control._posPanel __panel = (control._posPanel)__panelControl[__i];
                for (int _iObj = 0; _iObj < this.Controls.Count; _iObj++)
                {
                    // ตรวจสอบ ว่า ลอยอยู่เหนือ panel นี้หรือเปล่า
                    Control __test = this.Controls[_iObj];
                    if (isContainer(__panel.Location, __panel.Size, __test.Location, __test.Size))
                    {
                        //this.Controls[_iObj].Parent = __panel;
                        __innerControl.Add(__test);
                    }
                }

                // set parent
                foreach (Control __con in __innerControl)
                {
                    __con.Location = new Point(__con.Location.X - __panel.Location.X, __con.Location.Y - __panel.Location.Y);
                    __con.Parent = __panel;
                }

            }

        }

        protected virtual bool isContainer(Point __myObjectLocation, Size __myObjectSize, Point __targetObjLocation, Size __targetObjectSize)
        {
            if (__myObjectLocation.X < __targetObjLocation.X && __myObjectLocation.Y < __targetObjLocation.Y && ((__myObjectLocation.X + __myObjectSize.Width) > (__targetObjLocation.X + __targetObjectSize.Width)) && ((__myObjectLocation.Y + __myObjectSize.Height) > (__targetObjLocation.Y + __targetObjectSize.Height)))
            {
                return true;
            }
            return false;
        }

    }

    public class _posLabelShadow : MyLib._myShadowLabel
    {
        private string _idResult;
        public string _id
        {
            get { return _idResult; }
            set { _idResult = value; }
        }

        public _posLabelShadow()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            // new paint
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            StringFormat __sf = StringFormat.GenericDefault;
            __sf.Alignment = MyLib._myGlobal._getStringFormatTextAlignment(this.TextAlign);
            __sf.LineAlignment = MyLib._myGlobal._getStringFormatTextLineAlignment(this.TextAlign);

            if (this.DrawGradient == true)
            {
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), this.StartColor, this.EndColor, this.Angle, true);
                e.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
            }

            if (this.DrawShadow == true)
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ShadowColor), new Rectangle((int)this.XOffset, (int)this.YOffset, this.Width, this.Height), __sf);

            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Rectangle(0, 0, this.Width, this.Height), __sf);                  

        }
    }

    public class _posPictureBox : PictureBox
    {
        private string _idResult;
        public string _id
        {
            get { return _idResult; }
            set { _idResult = value; }
        }

    }

    public class _posHTML : WebBrowser
    {
        private string _idResult;
        public string _id
        {
            get { return _idResult; }
            set { _idResult = value; }
        }

        private string _documentTextResult = "";
        public string _documentText
        {
            get { return _documentTextResult; }
            set { _documentTextResult = value; }                
        }

        private string _htmlSchemaResult = "";
        public string _htmlSchema
        {
            get { return _htmlSchemaResult; }
            set { _htmlSchemaResult = value; }
        }
    }
}
