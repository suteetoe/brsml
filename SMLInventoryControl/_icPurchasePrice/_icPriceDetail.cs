using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._icPurchasePrice
{
    public partial class _icPriceDetail : UserControl
    {
        public _g.g._priceGridType _priceTypeTemp;
        public string _itemCode = "";
        int __width1 = 12;
        int __headSkip = 0;
        SMLERPControl._ic._icTransItemGridSelectUnitForm _icTransItemGridSelectUnit = new SMLERPControl._ic._icTransItemGridSelectUnitForm();
        MyLib._searchDataFull _searchData = null;
        object[] _sale_type = new object[] { "ไม่เลือก", "ขายสด", "ขายเชื่อ" };
        object[] _transport_type = new object[] { "ไม่เลือก", "รับเอง", "ส่งให้" };
        private _g.g._priceListType _priceListTypeTemp;

        public _icPriceDetail()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                // change resource
                _sale_type[0] = MyLib._myResource._findResource("ไม่เลือก")._str;
                _sale_type[1] = MyLib._myResource._findResource("ขายสด")._str;
                _sale_type[2] = MyLib._myResource._findResource("ขายเชื่อ")._str;

                _transport_type[0] = MyLib._myResource._findResource("ไม่เลือก")._str;
                _transport_type[1] = MyLib._myResource._findResource("รับเอง")._str;
                _transport_type[2] = MyLib._myResource._findResource("ส่งให้")._str;

            }
        }

        public _g.g._priceListType _priceListType
        {
            set
            {
                this._priceListTypeTemp = value;
            }
            get
            {
                return this._priceListTypeTemp;
            }
        }

        private void _createGrid()
        {
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);

            this._grid._clear();
            this._grid._columnListTop.Clear();
            this._grid._columnList.Clear();
            this._grid._columnTopActive = true;
            this._grid._table_name = _g.d.ic_inventory_purchase_price._table;
            switch (this._priceType)
            {
                case _g.g._priceGridType.ราคาตามเจ้าหนี้:
                    this.__width1 = 9;
                    this.__headSkip = 2;
                    this._grid._addColumn(_g.d.ic_inventory_purchase_price._supplier_code, 1, 0, 15, true, false, true, true);
                    this._grid._addColumn(_g.d.ic_inventory_purchase_price._supplier_name, 1, 0, 20, false, false, false, false);
                    this._grid._columnExtraWord(_g.d.ic_inventory_purchase_price._supplier_code, "(F2)");
                    break;
            }
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._sale_price1, 3, 0, 10, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._sale_price2, 3, 0, 10, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._unit_code, 1, 0, 10, false, false);
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._unit_name, 1, 0, 10, false, false, false);
            //
            this._grid._addColumnTop(_g.d.ic_inventory_purchase_price._qty_condition, 4 + this.__headSkip, 5 + this.__headSkip);
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._from_qty, 3, 0, this.__width1, true, false, true, false, __formatNumberPrice);
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._to_qty, 3, 0, this.__width1, true, false, true, false, __formatNumberPrice);
            this._grid._setColumnBackground(_g.d.ic_inventory_purchase_price._from_qty, Color.LavenderBlush);
            this._grid._setColumnBackground(_g.d.ic_inventory_purchase_price._to_qty, Color.LavenderBlush);
            //
            this._grid._addColumnTop(_g.d.ic_inventory_purchase_price._date_condition, 6 + this.__headSkip, 7 + this.__headSkip);
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._from_date, 4, 0, this.__width1, true, false);
            this._grid._addColumn(_g.d.ic_inventory_purchase_price._to_date, 4, 0, this.__width1, true, false);
            this._grid._setColumnBackground(_g.d.ic_inventory_purchase_price._from_date, Color.AliceBlue);
            this._grid._setColumnBackground(_g.d.ic_inventory_purchase_price._to_date, Color.AliceBlue);
            //
            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
            {
                this._grid._addColumnTop(_g.d.ic_inventory_purchase_price._other_condition, 8 + this.__headSkip, 10 + this.__headSkip);
                this._grid._addColumn(_g.d.ic_inventory_purchase_price._sale_type, 10, 0, this.__width1, true, false);
                this._grid._addColumn(_g.d.ic_inventory_purchase_price._transport_type, 10, 0, this.__width1, true, false);
                this._grid._addColumn(_g.d.ic_inventory_purchase_price._status, 11, 0, 5, true, false);
            }
            //
            this._addEvent();
            //
            this._grid._columnExtraWord(_g.d.ic_inventory_purchase_price._unit_code, "(F4)");
            this._grid._calcPersentWidthToScatter();
            //
            _icTransItemGridSelectUnit._selectUnitCode += new SMLERPControl._ic._icTransItemGridSelectUnitForm._selectUnitCodeEventHandler(_icTransItemGridSelectUnit__selectUnitCode);
        }

        public void _addEvent()
        {
            this._grid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_grid__cellComboBoxItem);
            this._grid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_grid__cellComboBoxGet);
            this._grid._afterAddRow += new MyLib.AfterAddRowEventHandler(_grid__afterAddRow);
            this._grid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            this._grid._clickSearchButton += new MyLib.SearchEventHandler(_grid__clickSearchButton);
            this._grid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_grid__queryForInsertCheck);
        }

        public void _removeEvent()
        {
            this._grid._cellComboBoxItem -= new MyLib.CellComboBoxItemEventHandler(_grid__cellComboBoxItem);
            this._grid._cellComboBoxGet -= new MyLib.CellComboBoxItemGetDisplay(_grid__cellComboBoxGet);
            this._grid._afterAddRow -= new MyLib.AfterAddRowEventHandler(_grid__afterAddRow);
            this._grid._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            this._grid._clickSearchButton -= new MyLib.SearchEventHandler(_grid__clickSearchButton);
            this._grid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_grid__queryForInsertCheck);
        }

        bool _grid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return (((decimal)this._grid._cellGet(row, _g.d.ic_inventory_purchase_price._sale_price1)) == 0 && ((decimal)this._grid._cellGet(row, _g.d.ic_inventory_purchase_price._sale_price2)) == 0) ? false : true;
        }

        void _search(string columnName, int columnNumber)
        {
            string __columnName = "";
            switch (this._priceType)
            {
                case _g.g._priceGridType.ราคาตามเจ้าหนี้:
                    __columnName = _g.d.ic_inventory_purchase_price._supplier_code;
                    break;
            }
            int __searchColumn = this._grid._findColumnByName(__columnName);
            if (__searchColumn != -1 && columnNumber == __searchColumn)
            {
                if (this._searchData != null)
                {
                    this._searchData.Dispose();
                }
                this._searchData = new MyLib._searchDataFull();
                this._searchData._name = columnName;
                switch (this._priceType)
                {
                    case _g.g._priceGridType.ราคาตามเจ้าหนี้:
                        this._searchData._dataList._loadViewFormat(_g.g._search_screen_ap, MyLib._myGlobal._userSearchScreenGroup, false);
                        break;
                }
                this._searchData._dataList._loadViewData(0);
                this._searchData._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchData._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(__searchCust__searchEnterKeyPress);
                switch (this._priceType)
                {
                    case _g.g._priceGridType.ราคาตามเจ้าหนี้:
                        this._searchData.WindowState = FormWindowState.Maximized;
                        break;
                }
                this._searchData.Focus();
                this._searchData._firstFocus();
                this._searchData.ShowDialog();
            }
        }

        void _grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _search(e._columnName, e._column);
        }

        void __searchCust__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            switch (this._priceType)
            {
                case _g.g._priceGridType.ราคาตามเจ้าหนี้:
                    string __custCode = (string)this._searchData._dataList._gridData._cellGet(row, 0);
                    this._searchData.Close();
                    this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_inventory_purchase_price._supplier_code, __custCode, true);
                    break;
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            switch (this._priceType)
            {
                case _g.g._priceGridType.ราคาตามเจ้าหนี้:
                    string __custCode = (string)this._searchData._dataList._gridData._cellGet(e._row, 0);
                    this._searchData.Close();
                    this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_inventory_purchase_price._supplier_code, __custCode, true);
                    break;
            }
            SendKeys.Send("{ENTER}");
        }

        void _searchSupplier(int row)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder();
            switch (this._priceType)
            {
                case _g.g._priceGridType.ราคาตามเจ้าหนี้:
                    string __custCode = (string)this._grid._cellGet(row, 0);
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + __custCode + "\'"));
                    __query.Append("</node>");
                    ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                    DataTable __t1 = ((DataSet)__dataResult[0]).Tables[0];
                    if (__t1.Rows.Count > 0)
                    {
                        this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_inventory_purchase_price._supplier_name, __t1.Rows[0][_g.d.ap_supplier._name_1].ToString(), true);
                    }
                    else
                    {
                        this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_inventory_purchase_price._supplier_name, "", true);
                    }
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    return true;
                case Keys.F4:
                    _selectUnitCode();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        /// <summary>
        /// เลือกหน่วยนับ
        /// </summary>
        protected void _selectUnitCode()
        {
            int __unitType = 0;
            string __itemName = "";
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder();
            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._unit_type + "," + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._itemCode + "\'"));
            __query.Append("</node>");
            String __queryStr = __query.ToString();
            ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
            //
            DataTable __t1 = ((DataSet)__dataResult[0]).Tables[0];
            if (__t1.Rows.Count > 0) __unitType = MyLib._myGlobal._intPhase(__t1.Rows[0][_g.d.ic_inventory._unit_type].ToString());
            if (__t1.Rows.Count > 0) __itemName = __t1.Rows[0][_g.d.ic_inventory._name_1].ToString();
            //
            string __itemDesc = this._itemCode + "," + __itemName;
            if (__unitType == 0)
            {
                MessageBox.Show(__itemDesc + " : สินค้านี้มีหน่วยนับเดียว");
            }
            else
            {
                string __unitCode = this._grid._cellGet(this._grid._selectRow, _g.d.ic_trans_detail._unit_code).ToString();
                this._icTransItemGridSelectUnit._itemCode = this._itemCode;
                this._icTransItemGridSelectUnit._lastCode = __unitCode;
                this._icTransItemGridSelectUnit.Text = __itemDesc;
                this._icTransItemGridSelectUnit.ShowDialog();
            }
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._grid._findColumnByName(_g.d.ic_inventory_purchase_price._supplier_code))
            {
                _searchSupplier(this._grid._selectRow);
            }
            else
                if (column == this._grid._findColumnByName(_g.d.ic_inventory_purchase_price._unit_code))
                {
                    _searchUnitName(this._grid._selectRow);
                }
        }

        void _icTransItemGridSelectUnit__selectUnitCode(int mode, string unitCode)
        {
            if (mode == 1)
            {
                this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_trans_detail._unit_code, unitCode, false);
                _searchUnitName(this._grid._selectRow);
            }
        }

        public void _searchUnitName(int row)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __unitCode = this._grid._cellGet(row, _g.d.ic_trans_detail._unit_code).ToString().ToUpper();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + "=\'" + __unitCode + "\'"));
                __query.Append("</node>");
                String __queryStr = __query.ToString();
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                //
                DataTable __t1 = ((DataSet)__dataResult[0]).Tables[0];
                if (__t1.Rows.Count > 0)
                {
                    this._grid._cellUpdate(row, _g.d.ic_trans_detail._unit_name, __t1.Rows[0][_g.d.ic_unit._name_1].ToString(), false);
                }
                else
                {
                    this._grid._cellUpdate(row, _g.d.ic_trans_detail._unit_name, "", false);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _grid__afterAddRow(object sender, int row)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + _g.d.ic_unit_use._code + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._ic_code + "=\'" + this._itemCode + "\' and " + _g.d.ic_unit_use._divide_value + "=1 and " + _g.d.ic_unit_use._stand_value + "=1";
            DataTable __getUnitCode = __myFrameWork._queryShort(__query).Tables[0];
            if (__getUnitCode.Rows.Count == 0)
            {
                __query = "select " + _g.d.ic_unit_use._code + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._ic_code + "=\'" + this._itemCode + "\'";
                __getUnitCode = __myFrameWork._queryShort(__query).Tables[0];
            }
            if (__getUnitCode.Rows.Count > 0)
            {
                this._grid._cellUpdate(row, _g.d.ic_inventory_purchase_price._unit_code, __getUnitCode.Rows[0][0].ToString(), true);
            }
            //
            this._grid._cellUpdate(row, _g.d.ic_inventory_purchase_price._status, 1, false);
        }

        string _grid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (columnName.Equals(_g.d.ic_inventory_price._sale_type))
            {
                return (_sale_type[(select == -1) ? 0 : select].ToString());
            }
            else
            {
                return (_transport_type[(select == -1) ? 0 : select].ToString());
            }
        }

        object[] _grid__cellComboBoxItem(object sender, int row, int column)
        {
            if (column == this._grid._findColumnByName(_g.d.ic_inventory_price._sale_type))
            {
                return _sale_type;
            }
            else
            {
                return _transport_type;
            }
        }

        /// <summary>
        /// ประเภทราคา
        /// </summary>
        public _g.g._priceGridType _priceType
        {
            get
            {
                return this._priceTypeTemp;
            }
            set
            {
                this._priceTypeTemp = value;
                this._createGrid();
            }
        }

        private void _dateAutoButton_Click(object sender, EventArgs e)
        {
            _tools __tools = new _tools();
            __tools._priceDetailAutoDate(this._grid, this._priceListType);
        }
    }
}
