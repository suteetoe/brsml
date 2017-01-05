using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._reportDesign
{
    public partial class _loadForm : Form
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public _loadForm()
        {
            InitializeComponent();
            this._dataGrid._isEdit = false;
            this._dataGrid._table_name = _g.d.gl_design_report._table;
            this._dataGrid._addColumn(_g.d.gl_design_report._code, 1, 100, 20);
            this._dataGrid._addColumn(_g.d.gl_design_report._name1, 1, 100, 50);
            this._dataGrid._addColumn(_g.d.gl_design_report._name2, 1, 100, 30);
            this._dataGrid._calcPersentWidthToScatter();
            //
            string __query = "select " + _g.d.gl_design_report._code + "," + _g.d.gl_design_report._name1 + "," + _g.d.gl_design_report._name2 + " from " + _g.d.gl_design_report._table + " order by " + _g.d.gl_design_report._code;
            DataTable __data = this._myFrameWork._queryShort(__query).Tables[0];
            this._dataGrid._loadFromDataTable(__data);
        }
    }
}
