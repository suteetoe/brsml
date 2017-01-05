using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _g
{
    public partial class _searchChartOfAccount : Form
    {
        public _searchChartOfAccount()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            // Create Grid Column
            _accountGrid._table_name = _g.d.gl_chart_of_account._table;
            _accountGrid._width_by_persent = true;
            _accountGrid._isEdit = false;
            _accountGrid.AllowDrop = true;
            _accountGrid._addColumn("Check", 11, 5, 5);
            _accountGrid._addColumn(_g.d.gl_chart_of_account._code, 1, 100, 20, false, false, false, false);
            _accountGrid._addColumn(_g.d.gl_chart_of_account._name_1, 1, 100, 40, false, false, false, false);
            _accountGrid._addColumn(_g.d.gl_chart_of_account._name_2, 1, 100, 35, false, false, false, false);
            _accountGrid._addColumn(_g.d.gl_chart_of_account._account_level, 2, 100, 35, false, true, false, false);
            _accountGrid._addColumn(_g.d.gl_chart_of_account._status, 2, 100, 35, false, true, false, false);
            _accountGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_accountGrid__beforeDisplayRow);
            _accountGrid.Refresh();
            // load Data

            if (MyLib._myGlobal._isDesignMode == false)
            {
                MyLib._myFrameWork __frameWork = new MyLib._myFrameWork();
                string __query = "select  " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._name_2 + "," + _g.d.gl_chart_of_account._account_level + "," + _g.d.gl_chart_of_account._status + " from " + _g.d.gl_chart_of_account._table + " order by " + _g.d.gl_chart_of_account._code;
                DataSet __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query);
                _accountGrid._loadFromDataTable(__getData.Tables[0]);
                _accountGrid.Invalidate();
            }
        }

        MyLib.BeforeDisplayRowReturn _accountGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            return (_g.g._chartOfAccountBeforeDisplay(sender, columnNumber, columnName, senderRow, columnType, rowData));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            //base.OnClosing(e);
        }
    }
}