using System;
using System.Collections.Generic;
using System.Text;

namespace IMSSalesCommission
{
    public class _screenTopControl : MyLib._myScreen
    {
        public _screenTopControl()
        {
            int __row = 0;
            this._table_name = _g.d.ic_trans._table;
            this._maxColumn = 4;
            this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
            this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
            this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
            this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);

            this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 1, 4, true, false, false, true, true, _g.d.ic_trans._ar_code);
            this._addTextBox(__row++, 2, 1, 0, _g.d.ic_trans._contactor, 2, 1, 1, true, false, true);

            this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
            this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 3, true, false, true);
            this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
            this._addDateBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);

            this._addComboBox(__row, 0, _g.d.ic_trans._inquiry_type, true, _g.g._saleType, true, _g.d.ic_trans._sale_type);
            this._addComboBox(__row, 1, _g.d.ic_trans._vat_type, true, _g.g._vatType, true);
            this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
            this._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._sale_group, 1, 1, 1, true, false, true);

        }
    }

}
