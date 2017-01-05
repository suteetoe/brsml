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
    public partial class _tableSearchLevelMenuControl : MyLib.VistaButton
    {
        public int _level = 0;
        public string _tableNumber = "";
        public string _tableName = "";
        public string _transGuidNumber = "";
        /// <summary>
        /// 0=ว่าง,1=โต๊ะเปิดอยู่,2=ปิดโต๊ะรอคิดเงิน,5=โต๊ะจอง
        /// </summary>
        public int _status = 0;
        public string _barcode = "";
        //
        public _tableSearchLevelMenuControl(string tableCode, string itemName, string transGuidNumber, int status, int level)
        {
            this.DoubleBuffered = true;
            this.Size = new Size(80, 60);

            switch (status)
            {
                case 1: this.BaseColor= Color.Red; break;
                case 2: this.BaseColor = Color.Yellow; break;
            }
            this._tableNumber = tableCode;
            this._tableName = itemName;
            this._transGuidNumber = transGuidNumber;
            this._level = level;
            this._status = status;
            this.mText = this._statusWord + "/" + this._tableNumber + "/" + this._tableName;
            this.Invalidated += (s1, e1) =>
            {
                this.mText = (this.myImage == null) ? this._statusWord+"/"+this._tableNumber + "/" + this._tableName : "";
            };
            this.Invalidate();
        }

        private string _statusWord
        {
            get
            {
                switch (this._status)
                {
                    case 1: return "เปิด";
                    case 2: return "รอคิดเงิน";
                    case 5: return "จอง";
                }
                return "ว่าง";
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (this.myImage != null)
            {
                ArrayList __cutStr = MyLib._myUtil._cutString(pevent.Graphics, this._tableName, this.Font, this.Width);
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
