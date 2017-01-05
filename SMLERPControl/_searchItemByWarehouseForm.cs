using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _searchItemByWarehouseForm : Form
    {
        public delegate void SelectedHandler(string wareHouse, string location,int unitType);
        public event SelectedHandler _selected;
        private int _unitType = 0;
        private DataTable _itemPacking = null;

        public _searchItemByWarehouseForm(string itemCode, string itemName)
        {
            InitializeComponent();
            //
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            //
            this._grid._isEdit = false;
            this._grid._table_name = _g.d.ic_resource._table;
            this._grid._addColumn(_g.d.ic_resource._warehouse, 1, 5, 20);
            this._grid._addColumn(_g.d.ic_resource._location, 1, 5, 20);
            this._grid._addColumn(_g.d.ic_resource._balance_qty, 3, 40, 20, false, false, false, false, __formatNumberQty);
            this._grid._addColumn(_g.d.ic_resource._qty, 1, 5, 40);
            //
            this._grid._total_show = true;
            this._grid._calcPersentWidthToScatter();
            //
            _icInfoProcess __process = new _icInfoProcess();
            this._grid._loadFromDataTable(__process._stkStockInfoAndBalanceByLocation( _g.g._productCostType.ปรกติ,  null, itemCode, itemCode, DateTime.Now, DateTime.Now, false, _icInfoProcess._stockBalanceType.ยอดคงเหลือตามที่เก็บ, "", "", true, false, ""));
            this._grid._mouseClick += new MyLib.MouseClickHandler(_grid__mouseClick);
            this._grid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_grid__beforeDisplayRow);
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemCode + "\'"));
            string __queryUnit = "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + "{0})";
            string __queryPacking = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_unit_use._code, String.Format(__queryUnit, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code) + " as " + _g.d.ic_unit_use._name_1, "coalesce(" + _g.d.ic_unit_use._row_order + ",1) as " + _g.d.ic_unit_use._row_order, _g.d.ic_unit_use._stand_value, _g.d.ic_unit_use._divide_value) + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._status + "=1 and " + _g.d.ic_unit_use._ic_code + "=\'" + itemCode + "\' order by " + _g.d.ic_unit_use._ratio;
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryPacking));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._unitType = (int)MyLib._myGlobal._decimalPhase((((DataSet)__getData[0]).Tables[0]).Rows[0][_g.d.ic_inventory._unit_type].ToString());
            this._itemPacking = ((DataSet)__getData[1]).Tables[0];
            //
            this.Load += new EventHandler(_searchItemByWarehouseForm_Load);
            //
            this.Text = itemName + " (" + itemCode + ")";
            if (this._unitType != 0)
            {
                this.Text = this.Text + " : " + "สินค้าเป็นแบบหลายหน่วยนับ"; 
            }
        }

        MyLib.BeforeDisplayRowReturn _grid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            if (columnName.Equals(_g.d.ic_resource._table + "." + _g.d.ic_resource._qty))
            {
                decimal __qty = MyLib._myGlobal._decimalPhase(sender._cellGet(row, _g.d.ic_resource._balance_qty).ToString());
                ((ArrayList)senderRow.newData)[columnNumber] = this._packingWord(__qty);
            }
            return senderRow;
        }

        void _grid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __wareHouse = this._grid._cellGet(this._grid._selectRow, _g.d.ic_resource._warehouse).ToString();
            string __location = this._grid._cellGet(this._grid._selectRow, _g.d.ic_resource._location).ToString();
            this._selected(__wareHouse, __location, this._unitType);
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    string __wareHouse = this._grid._cellGet(this._grid._selectRow, _g.d.ic_resource._warehouse).ToString();
                    string __location = this._grid._cellGet(this._grid._selectRow, _g.d.ic_resource._location).ToString();
                    this.Close();
                    this._selected(__wareHouse, __location,this._unitType);
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _searchItemByWarehouseForm_Load(object sender, EventArgs e)
        {
            this._grid.Select();
            this._grid._gotoCell(0, 0);
        }

        string _packingWord(decimal qty)
        {
            try
            {
                List<_g.g._convertPackingWordClass> __packing = new List<_g.g._convertPackingWordClass>();
                DataRow[] __selectPacking = this._itemPacking.Select(_g.d.ic_unit_use._row_order + " > 0", _g.d.ic_unit_use._row_order + " desc");
                for (int __loop = 0; __loop < __selectPacking.Length; __loop++)
                {
                    _g.g._convertPackingWordClass __newData = new _g.g._convertPackingWordClass();
                    __newData._unitCode = __selectPacking[__loop][_g.d.ic_unit_use._code].ToString();
                    __newData._unitName = __selectPacking[__loop][_g.d.ic_unit_use._name_1].ToString();
                    __newData._standValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._stand_value].ToString());
                    __newData._divideValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._divide_value].ToString());
                    __packing.Add(__newData);
                }
                return _g.g._convertPackingWord(__packing, qty, false);
            }
            catch
            {
                return qty.ToString();
            }
        }
    }
}
