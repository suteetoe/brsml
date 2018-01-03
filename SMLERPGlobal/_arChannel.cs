using System;
using System.Collections.Generic;
using System.Text;

namespace _g
{
    public class _arChannel : MyLib._manageMasterCodeFull
    {
        public _arChannel(String screenName)
        {
            this._labelTitle.Text = screenName;
            this._dataTableName = _g.d.ar_channel._table;
            this._addColumn(_g.d.ar_channel._code, 10, 20);
            this._inputScreen._setUpper(_g.d.ar_channel._code);
            this._addColumn(_g.d.ar_channel._name_1, 100, 40);
            this._addColumn(_g.d.ar_channel._name_2, 100, 40);
            this._finish();
        }
  
    }
}
