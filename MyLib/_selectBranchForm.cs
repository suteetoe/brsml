using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _selectBranchForm : Form
    {
        public string _selectBranch = "";
        public string _selectBranchName = "";
        public _selectBranchForm()
        {
            InitializeComponent();

            this._gridBranch._table_name = "erp_branch_list";
            this._gridBranch.IsEdit = false;
            this._gridBranch._addColumn("code", 1, 30, 70);
            this._gridBranch._addColumn("name_1", 1, 30, 70);
            this._gridBranch.WidthByPersent = true;
            this._gridBranch._calcPersentWidthToScatter();

            MyLib._myFrameWork __frameWork = new _myFrameWork();
            DataSet __result = __frameWork._queryShort("select code, name_1 from erp_branch_list order by code ");
            this._gridBranch._loadFromDataTable(__result.Tables[0]);

            this._gridBranch._mouseDoubleClick += _gridBranch__mouseDoubleClick;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_selectBranch.Length == 0)
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

        private void _gridBranch__mouseDoubleClick(object sender, GridCellEventArgs e)
        {
            if (e._row != -1 && e._column != -1)
            {
                this._selectBranch = this._gridBranch._cellGet(e._row, 0).ToString();
                this._selectBranchName = this._gridBranch._cellGet(e._row, 1).ToString();
                this.DialogResult = DialogResult.Yes;
            }
        }


    }
}
