using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPAudit
{
    public class _conditionScreenClass : MyLib._myScreen
    {
        public _conditionScreenClass()
        {
            this._table_name = _g.d.logs._table;
            this._maxColumn = 2;
            this._addDateBox(0, 0, 1, 0, _g.d.logs._date_begin, 1, true, false);
            this._addDateBox(0, 1, 1, 0, _g.d.logs._date_end, 1, true, false);
            this._addTextBox(1, 0, 1, 0, _g.d.logs._search, 2, 100, 0, true, false, false);
            //
            this._setDataDate(_g.d.logs._date_begin, MyLib._myGlobal._workingDate);
            this._setDataDate(_g.d.logs._date_end, MyLib._myGlobal._workingDate);
        }
    }
}
