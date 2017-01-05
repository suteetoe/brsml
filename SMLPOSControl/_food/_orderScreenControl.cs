using System;
using System.Collections.Generic;
using System.Text;

namespace SMLPOSControl._food
{
    public class _orderScreenControl : MyLib._myScreen
    {
        public _orderScreenControl()
        {
            this._table_name = _g.d.table_order_doc._table;
            this._maxColumn = 3;
            this._addDateBox(0, 0, 1, 0, _g.d.table_order_doc._doc_date, 1, true, false);
            this._addTextBox(0, 1, _g.d.table_order_doc._doc_time, 10);
            this._addNumberBox(0, 2, 1, 0, _g.d.table_order_doc._qty, 1, 2, true);
            this._addTextBox(1, 0, _g.d.table_order_doc._doc_no, 10);
            this._addTextBox(1, 1, _g.d.table_order_doc._table_number, 10);
            this._addNumberBox(1, 2, 1, 0, _g.d.table_order_doc._sum_amount, 1, 2, true);
            this._addTextBox(2, 0, _g.d.table_order_doc._cust_code, 10);
            this._addTextBox(2, 1, 1, _g.d.table_order_doc._cust_name, 2, 10);
            this._enabedControl(_g.d.table_order_doc._doc_date, false);
            this._enabedControl(_g.d.table_order_doc._doc_time, false);
            this._enabedControl(_g.d.table_order_doc._doc_no, false);
            this._enabedControl(_g.d.table_order_doc._table_number, false);
            this._enabedControl(_g.d.table_order_doc._cust_code, false);
            this._enabedControl(_g.d.table_order_doc._cust_name, false);
            this._enabedControl(_g.d.table_order_doc._qty, false);
            this._enabedControl(_g.d.table_order_doc._sum_amount, false);
        }
    }
}
