using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._coupon
{
    public partial class _couponGenerateControl : UserControl
    {
        public _couponGenerateControl()
        {
            InitializeComponent();

            int __row = 0;
            this._screenTop._table_name = _g.d.coupon_list._table;
            this._screenTop._maxColumn = 2;

            this._screenTop._addNumberBox(__row++, 0, 1, 0, _g.d.coupon_list._amount, 1, 2, true);
            this._screenTop._addDateBox(__row, 0, 1, 0, _g.d.coupon_list._date, 1, true, false);
            this._screenTop._addDateBox(__row++, 1, 1, 0, _g.d.coupon_list._date_expire, 1, true, false);

            // grid

            this._couponGenGrid._table_name = _g.d.coupon_list._table;
            this._couponGenGrid._addColumn(_g.d.coupon_list._number, 1, 40, 40, false, false);
            //this._couponGenGrid._addColumn(_g.d.coupon_list._remark, 1, 60, 60);

            this._couponGenGrid._calcPersentWidthToScatter();
            this._couponGenGrid._isEdit = false;
        }

        private void _genButton_Click(object sender, EventArgs e)
        {
            // get format
            int __count = MyLib._myGlobal._intPhase(this._amountTextbox.Text);

            if (__count > 0 && this._formatTextbox.Text != "")
            {
                string __couponStartNumber = this._formatTextbox.Text;

                for (int __i = 0; __i < __count; __i++)
                {
                    int __rowIndex = this._couponGenGrid._addRow();
                    this._couponGenGrid._cellUpdate(__rowIndex, _g.d.coupon_list._number, __couponStartNumber, false);

                    string __nextCouponNumber = MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__couponStartNumber);
                    __couponStartNumber = __nextCouponNumber;

                }
            }
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._couponGenGrid._clear();
        }
    }

    //public class _couponGenerateScreen : MyLib._myScreen
    //{
    //    public _couponGenerateScreen()
    //    {
    //        this._table_name = _g.d.coupon_list._table;
    //        this._maxColumn = 2;

    //        this._addTextBox(0, 0, _g.d.coupon_list._amount, 10);
    //        this._addDateBox(1, 0, 1, 1, _g.d.coupon_list._date, 1, true);
    //        this._addDateBox(1, 1, 1, 1, _g.d.coupon_list._date_expire, 1, true);
    //    }
    //}
}
