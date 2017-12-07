using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPGL._chart
{
    public class _glSAPAccountCodeMap : MyLib._manageMasterCodeFull
    {
        public _glSAPAccountCodeMap(String screenName)
        {
            this._labelTitle.Text = screenName;
            this._dataTableName = _g.d.gl_sap_account_code._table;
            this._addColumn(_g.d.gl_sap_account_code._sap_code, 10, 20);
            this._addColumn(_g.d.gl_sap_account_code._code, 10, 20);
            this._inputScreen._textBoxSearch += _inputScreen__textBoxSearch;
            this._finish();
        }
        private void _inputScreen__textBoxSearch(object sender)
        {
         
        }
    }
}
