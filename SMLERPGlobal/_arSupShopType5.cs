﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _g
{
    public class _arSubShopType5 : MyLib._manageMasterCodeFull
    {
        public _arSubShopType5(String screenName)
        {
            this._labelTitle.Text = screenName;
            this._dataTableName = _g.d.sub_ar_shoptype5._table;
            this._addColumn(_g.d.sub_ar_shoptype5._code, 10, 20);
            this._inputScreen._setUpper(_g.d.sub_ar_shoptype5._code);
            this._addColumn(_g.d.sub_ar_shoptype5._name_1, 100, 40);
            this._addColumn(_g.d.sub_ar_shoptype5._name_2, 100, 40);
            this._addColumn(_g.d.sub_ar_shoptype5._remark, 100, 40);
            this._finish();
        }
    }
}
