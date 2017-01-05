using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGLControl
{
    public partial class _glDetailExtraSelectCode : Form
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public event LoadDataEventHandler _loadDataSet;
        public event ConfirmEventHandler _confirm;

        public _glDetailExtraSelectCode()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._dataGrid._table_name = _g.d.gl_resource._table;
            this._dataGrid._addColumn(_g.d.gl_resource._check, 11, 1, 10);
            this._dataGrid._addColumn(_g.d.gl_resource._code, 1, 1, 25);
            this._dataGrid._addColumn(_g.d.gl_resource._name_1, 1, 1, 35);
            this._dataGrid._addColumn(_g.d.gl_resource._name_2, 1, 1, 30);
            //
            this.Load += new EventHandler(_glDetailExtraSelectCode_Load);
        }

        void _glDetailExtraSelectCode_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        void _loadData()
        {
            if (_loadDataSet != null)
            {
                DataSet __getData = _loadDataSet();
                this._dataGrid._loadFromDataTable(__getData.Tables[0]);
            }
        }

        private void _setCheckAll(bool setValue)
        {
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                this._dataGrid._cellUpdate(__row, _g.d.gl_resource._check, (setValue == true) ? 1 : 0, false);
            }
            this._dataGrid.Invalidate();
        }

        private void _buttonRefresh_Click(object sender, EventArgs e)
        {
            _loadData();
        }

        private void _buttonSelectAll_Click(object sender, EventArgs e)
        {
            _setCheckAll(true);
        }

        private void _buttonRemoveAll_Click(object sender, EventArgs e)
        {
            _setCheckAll(false);
        }

        private void _buttonConfirm_Click(object sender, EventArgs e)
        {
            if (_confirm != null)
            {
                this._confirm(this._dataGrid);
            }
            this.Close();
        }
    }

    public delegate DataSet LoadDataEventHandler();
    public delegate void ConfirmEventHandler(MyLib._myGrid dataGrid);
}