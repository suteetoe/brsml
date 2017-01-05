using System;
using System.Collections.Generic;
using System.Text;

namespace SMLInventoryControl
{
    class _icTransLabelPrintGrid : MyLib._myGrid
    {
        public _icTransLabelPrintGrid()
        {
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._table_name = _g.d.ic_trans_detail._table;
            this._addRowEnabled = false;
            this._addColumn("check", 11, 10, 10);
            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 15, 15);
            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 15, 15);
            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 15, 15);
            //this._addColumn(_g.d.ic_trans_detail._qty, 3, 15, 15);
            this._addColumn(_g.d.ic_trans_detail._qty, 3, 15, 15, true, false, true, false, __formatNumberQty);
            this._addColumn("print_qty", 3, 15, 15);
            this._addColumn(_g.d.ic_trans_detail._approval_unit, 1, 15, 15, true, false, true, true);
            this._addColumn(_g.d.ic_trans_detail._start_qty, 3, 15, 15, true, false, true, false, __formatNumberQty);
            this._addColumn(_g.d.ic_trans_detail._end_qty, 3, 15, 15, true, false, true, false, __formatNumberQty);

            this._calcPersentWidthToScatter();
        }
    }
}
