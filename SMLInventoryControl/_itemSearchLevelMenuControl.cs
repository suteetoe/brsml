using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _itemSearchLevelMenuControl : MyLib.VistaButton
    {
        public int _level = 0;
        public string _itemCode = "";
        public string _itemName = "";
        public string _unitCode = "";
        public string _barcode = "";
        public decimal _price = 0M;
        public string _suggest_remark = "";
        public bool _print_checker = false;
        public string _unitNameEng = "";
        public string _nameEng = "";
        //

        public _itemSearchLevelMenuControl(string itemCode, string itemName, string barcode, string unitCode, decimal price, int level, string suggest_remark)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.DoubleBuffered = true;
            this._barcode = barcode;
            this._price = price;
            this._unitCode = unitCode;
            this._itemCode = itemCode;
            this._itemName = itemName;
            this._suggest_remark = suggest_remark;

            this.Size = new Size(100, 25);
            this._level = level;
            this.mText = this._itemNameFull;
            this.Invalidated += (s1, e1) =>
            {
                this.mText = (this.myImage == null) ? this._itemNameFull : "";
            };
            this.Invalidate();
        }


        private string _itemNameFull
        {
            get
            {
                StringBuilder __result = new StringBuilder(((MyLib._myGlobal._language != MyLib._languageEnum.Thai && this._nameEng.Length > 0) ? this._nameEng : this._itemName));
                if (this._unitCode.Trim().Length > 0)
                {
                    __result.Append("/" + ((MyLib._myGlobal._language != MyLib._languageEnum.Thai && this._unitNameEng.Length > 0) ? this._unitNameEng : this._unitCode));
                }
                if (this._price != 0)
                {
                    __result.Append("/" + this._price.ToString("#,###,###.##"));
                }
                return __result.ToString();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {

            base.OnPaint(pevent);
            if (this.myImage != null)
            {
                ArrayList __cutStr = MyLib._myUtil._cutString(pevent.Graphics, this._itemNameFull, this.Font, this.Width);
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
