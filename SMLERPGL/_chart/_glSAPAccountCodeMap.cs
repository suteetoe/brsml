using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPGL._chart
{
    public class _glSAPAccountCodeMap : MyLib._manageMasterCodeFull
    {
        public _glSAPAccountCodeMap()
        {
            this._addColumn(_g.d.ic_shelf._code, 10, 20);
            this._inputScreen._textBoxSearch += _inputScreen__textBoxSearch;
            this._finish();

        }

        private void _inputScreen__textBoxSearch(object sender)
        {
         
        }
    }
}
