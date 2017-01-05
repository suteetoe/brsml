using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReportTool
{
    public partial class _cashierSelectForm : Form
    {
        public _cashierSelectForm()
        {
            InitializeComponent();
            //
            this._grid._table_name = _g.d.erp_user._table;
            this._grid._addColumn("Check", 11, 20, 20);
            this._grid._addColumn(_g.d.erp_user._code, 1, 30, 30);
            this._grid._addColumn(_g.d.erp_user._name_1, 1, 50, 50);
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.erp_user._code + "," + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._pos_cashier + "=1 order by " + _g.d.erp_user._code).Tables[0];
            this._grid._loadFromDataTable(__dt);
        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
