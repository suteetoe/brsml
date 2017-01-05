using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAudit
{
    public partial class _loginLog : UserControl
    {
        public _loginLog()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._dataGrid._table_name = _g.d.logs._table;
            this._dataGrid._addColumn(_g.d.logs._user_code, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.logs._computer_name, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.logs._login_time, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.logs._logout_time, 1, 1, 10);
            this._dataGrid._calcPersentWidthToScatter();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._dataGrid._clear();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + _g.d.logs._user_code + "," + _g.d.logs._computer_name + "," +
                _g.d.logs._date_time + " as " + _g.d.logs._login_time + "," +
                _g.d.logs._date_time_out + " as " + _g.d.logs._logout_time +
                " from " + _g.d.logs._table +
                " where (" + _g.d.logs._function_type + "=0 and " + _g.d.logs._date_time + " between " + this._conditionScreen._getDataStrQuery(_g.d.logs._date_begin, " 00:00") + " and " + this._conditionScreen._getDataStrQuery(_g.d.logs._date_end, " 23:59") + ")";
            string __search = this._conditionScreen._getDataStr(_g.d.logs._search).Trim();
            if (__search.Length > 0)
            {
                __query = __query + " and (" + _g.d.logs._user_code + " like \'%" + __search + "%\' or " + _g.d.logs._computer_name + " like \'%" + __search + "%\')";
            }
            DataTable __getData = __myFrameWork._queryShort(__query.ToLower()).Tables[0];
            this._dataGrid._loadFromDataTable(__getData);
        }
    }
}
