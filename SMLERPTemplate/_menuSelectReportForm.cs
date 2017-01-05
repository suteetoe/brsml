using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPTemplate
{
    public partial class _menuSelectReportForm : Form
    {
        public _menuSelectReportForm()
        {
            InitializeComponent();

            this._grid._buttonDelete.Enabled = false;
            this._grid._buttonSelectAll.Enabled = false;
            this._grid._buttonNewFromTemp.Enabled = false;
            this._grid._buttonNew.Enabled = false;

            this._grid._gridData._isEdit = false;
            this._grid._lockRecord = false;
            this._grid._loadViewFormat("screen_fastreport_loadreport", MyLib._myGlobal._userSearchScreenGroup, false);
        }
    }
}
