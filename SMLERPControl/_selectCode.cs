using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _selectCode : Form
    {
        private string _tableName;
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _selectCode(string tableName)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._tableName = tableName;
            this._dataGrid._table_name = _g.d.gl_resource._table;
            this._dataGrid._addColumn(_g.d.gl_resource._check, 11, 1, 10);
            this._dataGrid._addColumn(_g.d.gl_resource._code, 1, 1, 25);
            this._dataGrid._addColumn(_g.d.gl_resource._name_1, 1, 1, 35);
            this._dataGrid._addColumn(_g.d.gl_resource._name_2, 1, 1, 30);
            //
            _loadData();
        }

        void _loadData()
        {
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, string.Concat("select ", _g.d.gl_resource._code, ",", _g.d.gl_resource._name_1, ",", _g.d.gl_resource._name_2, " from ", this._tableName, " order by ", _g.d.gl_resource._code));
            this._dataGrid._loadFromDataTable(__getData.Tables[0]);
        }

        private void _buttonRefresh_Click(object sender, EventArgs e)
        {
            _loadData();
        }

        private void _setCheckAll(bool setValue)
        {
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                this._dataGrid._cellUpdate(__row, _g.d.gl_resource._check, (setValue == true) ? 1 : 0, false);
            }
            this._dataGrid.Invalidate();
        }

        private void _buttonSelectAll_Click(object sender, EventArgs e)
        {
            _setCheckAll(true);
        }

        private void _buttonRemoveAll_Click(object sender, EventArgs e)
        {
            _setCheckAll(false);
        }

        private void _selectCode_Load(object sender, EventArgs e)
        {

        }
    }
}