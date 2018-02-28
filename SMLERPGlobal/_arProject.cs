using System;
using System.Collections.Generic;
using System.Text;

namespace _g
{
    public class _arProject : MyLib._manageMasterCodeFull
    {
        public _arProject(String screenName)
        {
            this._labelTitle.Text = screenName;
            this._dataTableName = _g.d.ar_project._table;
            this._addColumn(_g.d.ar_project._code, 10, 20);
            this._inputScreen._setUpper(_g.d.ar_project._code);
            this._addColumn(_g.d.ar_project._name_1, 100, 40);
            this._addColumn(_g.d.ar_project._name_2, 100, 40);
            this._addColumn(_g.d.ar_project._remark, 100, 40);
            this._finish();
        }
    }
}
