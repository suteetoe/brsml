using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report
{
    public partial class _selectBranchControl : UserControl
    {
        public _selectBranchControl()
        {
            InitializeComponent();

            this._gridBranch._table_name = _g.d.gl_journal_book._table;
            this._gridBranch._addColumn("Check", 11, 1, 20, false);
            this._gridBranch._addColumn(_g.d.gl_journal_book._code, 1, 1, 30, true);
            this._gridBranch._addColumn(_g.d.gl_journal_book._name_1, 1, 1, 50, true);
            //
            if (MyLib._myGlobal._isDesignMode == false)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.erp_branch_list._table);
                this._gridBranch._loadFromDataTable(__getData.Tables[0]);
                for (int __loop = 0; __loop < this._gridBranch._rowData.Count; __loop++)
                {
                    this._gridBranch._cellUpdate(__loop, "check", 1, false);
                }
            }
        }
    }
}
