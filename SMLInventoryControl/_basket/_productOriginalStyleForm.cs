using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._basket
{
    public partial class _productOriginalStyleForm : Form
    {
        public delegate void _moveItemListEventHandler(object sender);
        public event _moveItemListEventHandler _moveItemList;

        public _productOriginalStyleForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._unitControl.Visible = false;
            this._unitControl._isEdit = false;
            this._unitControl._mouseClick += new MyLib.MouseClickHandler(_unitControl__mouseClick);
            this._unitControl.KeyDown += _unitControl_KeyDown;
            //
            this._dataList._fullMode = false;
            this._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
            this._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._dataList._gridData.KeyDown += _gridData_KeyDown;
            this._dataList._searchText.textBox.KeyDown += textBox_KeyDown;
            //
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            this._basketGrid._table_name = _g.d.ic_trans_detail._table;
            this._basketGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 20, 20, false, false);
            this._basketGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 20, 50);
            this._basketGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 20, 10, false, false);
            this._basketGrid._addColumn(_g.d.ic_trans_detail._unit_name, 1, 20, 10, false, false);
            this._basketGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty);
            //
            this._basketGrid._calcPersentWidthToScatter();
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Enter) == Keys.Enter)
            {
                if (this._dataList._gridData._selectRow != -1)
                {
                    _addByItemRow();
                }
            }
        }

        void _unitControl_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Enter) == Keys.Enter)
            {
                string __unitCode = this._unitControl._cellGet(this._unitControl._selectRow, _g.d.ic_unit_use._code).ToString();
                string __unitName = this._unitControl._cellGet(this._unitControl._selectRow, _g.d.ic_unit_use._name_1).ToString();
                this._addItem(this._unitControl._itemCode, this._unitControl._itemName, __unitCode, __unitName);
            }
        }

        void _gridData_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Enter) == Keys.Enter)
            {
                _addByItemRow();
            }
        }

        void _unitControl__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __unitCode = this._unitControl._cellGet(e._row, _g.d.ic_unit_use._code).ToString();
            string __unitName = this._unitControl._cellGet(e._row, _g.d.ic_unit_use._name_1).ToString();
            this._addItem(this._unitControl._itemCode, this._unitControl._itemName, __unitCode, __unitName);
        }

        void _addItem(string itemCode, string itemName, string unitCode, string unitName)
        {
            int __addr = this._basketGrid._addRow();
            this._basketGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, itemCode, false);
            this._basketGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, itemName, false);
            this._basketGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, unitCode, false);
            this._basketGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_name, unitName, false);
            this._basketGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, this._dataList._qty, false);
            this._basketGrid.Invalidate();
            this._dataList._searchText.Focus();
            this._dataList._searchText.textBox.Text = "";
            this._dataList._searchText.textBox.Focus();
        }

        void _addByItemRow()
        {
            try
            {
                string __itemCode = this._dataList._gridData._cellGet(this._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                string __itemName = this._dataList._gridData._cellGet(this._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ic_inventory._unit_type + "," + _g.d.ic_inventory._unit_standard + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + ") as " + _g.d.ic_inventory._unit_standard_name + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'").Tables[0];
                int __unitType = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._unit_type].ToString());
                string __unitCode = __dt.Rows[0][_g.d.ic_inventory._unit_standard].ToString();
                string __unitName = __dt.Rows[0][_g.d.ic_inventory._unit_standard_name].ToString();
                if (__unitType == 1)
                {
                    this._unitControl.Visible = true;
                    this._unitControl._itemCode = __itemCode;
                    this._unitControl._itemName = __itemName;
                    DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, this._unitControl._createQueryForLoad(__itemCode));
                    this._unitControl._loadFromDataTable(__result.Tables[0]);
                    this._unitControl._selectColumn = 0;
                    this._unitControl._selectRow = 0;
                    this._unitControl._message = __itemCode + " : " + __itemName;
                    this._unitControl.Invalidate();
                    this._unitControl.Focus();
                    // Find Row
                    this._unitControl._findAndGotoRow(_g.d.ic_unit_use._code, __unitCode);
                }
                else
                {
                    this._unitControl.Visible = false;
                    this._addItem(__itemCode, __itemName, __unitCode, __unitName);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _addByItemRow();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult __clear = MessageBox.Show("Clear All", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (__clear == System.Windows.Forms.DialogResult.Yes)
            {
                this._basketGrid._clear();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult __clear = MessageBox.Show("Move and Clear", "Move", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (__clear == System.Windows.Forms.DialogResult.Yes)
            {
                this._moveItemList(this);
                this._basketGrid._clear();
            }
        }
    }
}
