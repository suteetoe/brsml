using System;
using System.Collections.Generic;
using System.Text;

namespace _g
{
    public class _arShopType1 : MyLib._manageMasterCodeFull
    {
        public _arShopType1(String screenName)
        {
            this._labelTitle.Text = screenName;
            this._dataTableName = _g.d.ar_shoptype1._table;
            this._addColumn(_g.d.ar_shoptype1._code, 10, 20);
            this._inputScreen._setUpper(_g.d.ar_shoptype1._code);
            this._addColumn(_g.d.ar_shoptype1._name_1, 100, 40);
            this._addColumn(_g.d.ar_shoptype1._name_2, 100, 40);
            this._addColumn(_g.d.ar_shoptype1._remark, 100, 40);
            this._finish();
        }
    }
}
