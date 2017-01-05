using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPControl
{
    public class _documentPictureScreenTopControl : MyLib._myScreen
    {
        public _documentPictureScreenTopControl()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.ic_trans._table;

            this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
            this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 10, 0, true, false, false);
            this._addDateBox(1, 0, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true);
            this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 10, 0, true, false);
            this._addTextBox(2, 0, 2, 0, _g.d.ic_trans._remark, 2, 255, 0, true, false, true);

        }
    }
}
