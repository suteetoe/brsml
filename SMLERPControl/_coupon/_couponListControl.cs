using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._coupon
{
    public partial class _couponListControl : UserControl
    {
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);

        public _couponListControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._screen._table_name = _g.d.coupon_list._table;
            this._screen._maxColumn = 2;
            int __row = 0;
            this._screen._addTextBox(__row++, 0, 1, 1, _g.d.coupon_list._number, 1, 10, 0, true, false, false);
            this._screen._addNumberBox(__row, 0, 0, 0, _g.d.coupon_list._amount, 1, 2, true);
            this._screen._addNumberBox(__row++, 1, 0, 0, _g.d.coupon_list._balance_amount, 1, 2, true);
            this._screen._addDateBox(__row, 0, 1, 0, _g.d.coupon_list._date, 1, true);
            this._screen._addDateBox(__row++, 1, 1, 0, _g.d.coupon_list._date_expire, 1, true);
            this._screen._addComboBox(__row, 0, _g.d.coupon_list._coupon_type, true, new string[] { _g.d.coupon_list._coupon_discount_amount, _g.d.coupon_list._coupon_discount_percent }, false);
            this._screen._addCheckBox(__row++, 1, _g.d.coupon_list._single_use, false, true);
            this._screen._addCheckBox(__row++, 0, _g.d.coupon_list._last_status, false, true);
            this._screen._addTextBox(__row++, 0, 4, _g.d.coupon_list._remark, 2, 10);

            this._screen._getControl(_g.d.coupon_list._balance_amount).Enabled = false;

            this._movementGrid._table_name = _g.d.cb_trans_detail._table;
            this._movementGrid._addColumn(_g.d.cb_trans_detail._doc_date, 4, 20, 20);
            this._movementGrid._addColumn(_g.d.cb_trans_detail._doc_time, 1, 10, 10);
            this._movementGrid._addColumn(_g.d.cb_trans_detail._doc_no, 1, 20, 20);
            this._movementGrid._addColumn(_g.d.cb_trans_detail._description, 1, 20, 20);
            this._movementGrid._addColumn(_g.d.cb_trans_detail._amount, 3, 10, 10, true, false, false, false, this._formatNumberAmount);
            this._movementGrid._addColumn(_g.d.cb_trans_detail._balance_amount, 3, 10, 10, true, false, false, false, this._formatNumberAmount);
            this._movementGrid._addColumn(_g.d.cb_trans_detail._last_status, 2, 10, 10, false, true, true);
            this._movementGrid._calcPersentWidthToScatter();
            this._movementGrid._isEdit = false;
        }
    }
}
