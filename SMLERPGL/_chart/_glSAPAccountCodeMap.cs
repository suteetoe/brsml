using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPGL._chart
{
    public class _glSAPAccountCodeMap : MyLib._manageMasterCodeFull
    {
        private _g._searchChartOfAccountDialog _chartOfAccountScreen = null;
        public _glSAPAccountCodeMap(String screenName)
        {
            this._labelTitle.Text = screenName;
            this._dataTableName = _g.d.gl_sap_account_code._table;
            this._inputScreen._addTextBox(0, 0, 1, 1, _g.d.gl_sap_account_code._code, 1, 10, 1, true, false);
            this._rowScreen++;
            this._addColumn(_g.d.gl_sap_account_code._sap_code, 10, 20);
         //   this._addColumn(_g.d.gl_sap_account_code._code, 10, 20);
            this._inputScreen._textBoxSearch += _inputScreen__textBoxSearch;

            this._finish();
            this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
        }
        private void _inputScreen__textBoxSearch(object sender)
        {
            _chartOfAccountScreen = new _g._searchChartOfAccountDialog();
            this._chartOfAccountScreen.Text = ((MyLib._myTextBox)sender)._labelName;
            this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchChartOfAccount__searchEnterKeyPress);
            this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._chartOfAccountScreen.ShowDialog();
        }
        void _search(MyLib._myGrid grid, int row)
        {
            this._inputScreen._setDataStr(_g.d.gl_sap_account_code._code, (string)grid._cellGet(row, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code));
            this._chartOfAccountScreen.Close();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _search((MyLib._myGrid)sender, e._row);
        }

        void _searchChartOfAccount__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _search((MyLib._myGrid)sender, row);
        }
    }
}
