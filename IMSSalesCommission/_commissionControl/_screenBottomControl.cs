using System;
using System.Collections.Generic;
using System.Text;

namespace IMSSalesCommission
{
    public class _screenBottomControl : MyLib._myScreen
    {

        public _screenBottomControl()
        {
            this._maxColumn = 4;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");

            this._table_name = _g.d.ic_trans._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
            this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
            this._addDateBox(1, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);
            this._addTextBox(2, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
            this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
            this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
            this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
            this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

            this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);

            this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
            this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
            this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
            this._addNumberBox(4, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
            this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
            this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
            //
            this._enabedControl(_g.d.ic_trans._total_discount, false);
            this._enabedControl(_g.d.ic_trans._advance_amount, false);
            this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);

        }
    }

}
