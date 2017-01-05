using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _g
{
    public class _searchChartOfAccountDialog : MyLib._searchDataFull
    {
        public _searchChartOfAccountDialog()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._name = _g.g._search_screen_gl_chart_of_account;
                this._dataList._loadViewFormat(this._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._dataList._gridData._addColumn(_g.d.gl_chart_of_account._account_level, 2, 0, 0, false, true, true);
                this._dataList._gridData._addColumn(_g.d.gl_chart_of_account._status, 2, 0, 0, false, true, true);
                this._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                this.WindowState = FormWindowState.Maximized;
                this.SizeChanged += new EventHandler(_searchChartOfAccountDialog_SizeChanged);
            }
        }

        void _searchChartOfAccountDialog_SizeChanged(object sender, EventArgs e)
        {
            this._dataList._loadViewData(0);
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            return (_g.g._chartOfAccountBeforeDisplay(sender, columnNumber, columnName, senderRow, columnType, rowData));
        }
    }
}
