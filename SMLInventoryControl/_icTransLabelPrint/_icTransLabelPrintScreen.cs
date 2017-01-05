using System;
using System.Collections.Generic;
using System.Text;

namespace SMLInventoryControl
{
    class _icTransLabelPrintScreen : MyLib._myScreen
    {
        public _icTransLabelPrintScreen()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.ic_trans._table;
            this._addTextBox(0, 0, _g.d.ic_trans._doc_no, 10);
            this._addDateBox(1, 0, 1, 0, _g.d.ic_trans._doc_no, 1, true);
        }
    }
}
