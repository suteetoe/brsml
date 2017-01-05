using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _selectSaleButtonControl : MyLib.VistaButton
    {
        public string _saleCode = "";
        public string _saleName = "";
        public string _passWord = "";

        public _selectSaleButtonControl(string saleCode, string saleName)
        {
            this.DoubleBuffered = true;
            this._saleCode = saleCode;
            this._saleName = saleName;
            this.mText = this._saleNameFull;
            this.Invalidated += (s1, e1) =>
            {
                this.mText = (this.myImage == null) ? this._saleNameFull : "";
            };
            this.Invalidate();
        }

        private string _saleNameFull
        {
            get
            {
                return this._saleCode + "/" + this._saleName;
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (this.myImage != null)
            {
                ArrayList __cutStr = MyLib._myUtil._cutString(pevent.Graphics, _saleNameFull, this.Font, this.Width);
                float __y = this.Height - 5;
                for (int __loop = __cutStr.Count - 1; __loop >= 0; __loop--)
                {
                    SizeF __stringSize = pevent.Graphics.MeasureString(__cutStr[__loop].ToString(), this.Font);
                    __y -= __stringSize.Height;
                    pevent.Graphics.DrawString(__cutStr[__loop].ToString(), this.Font, new SolidBrush(Color.Black), (this.Width - __stringSize.Width) / 2, __y);
                }
            }
        }
    }
}
