using System;
using System.Collections.Generic;
using System.Text;

namespace _g
{
    public class _customerType : MyLib._manageMasterCodeFull
    {
      
        public _customerType(String screenName)
        {
            this._labelTitle.Text = screenName;
            this._dataTableName = _g.d.customer_type._table;
            this._addColumn(_g.d.customer_type._code, 10, 20);
            this._inputScreen._setUpper(_g.d.customer_type._code);
            this._addColumn(_g.d.customer_type._name_1, 100, 40);
            this._addColumn(_g.d.customer_type._name_2, 100, 40);
            this._finish();
        }


    }
}
