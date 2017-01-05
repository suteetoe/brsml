using System;
using System.Collections.Generic;
using System.Text;

namespace SMLPOSControl._food
{
    public class _orderListGridControl : MyLib._myGrid
    {
        private string _formatNumberQty = _g.g._getFormatNumberStr(1);
        private string _formatNumberPrice = _g.g._getFormatNumberStr(2);
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);

        public _orderListGridControl()
        {
            this._isEdit = false;
            this._table_name = _g.d.table_order_doc._table;
            this._addColumn(_g.d.table_order_doc._doc_no, 1, 20, 20);
            this._addColumn(_g.d.table_order_doc._doc_date, 4, 20, 20);
            this._addColumn(_g.d.table_order_doc._doc_time, 1, 10, 10);
            this._addColumn(_g.d.table_order_doc._sale_code, 1, 10, 10);
            this._addColumn(_g.d.table_order_doc._qty, 3, 10, 10, false, false, true, false, this._formatNumberQty);
            this._addColumn(_g.d.table_order_doc._sum_amount, 3, 10, 10, false, false, true, false, this._formatNumberAmount);
            this._calcPersentWidthToScatter();
        }
    }
}
