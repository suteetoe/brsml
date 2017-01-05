using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._ic
{
    public partial class _icPriceFormulaDetail : UserControl
    {
        public _g.g._priceGridType _priceTypeTemp;
        public string _itemCode = "";
        SMLERPControl._ic._icTransItemGridSelectUnitForm _icTransItemGridSelectUnit = new SMLERPControl._ic._icTransItemGridSelectUnitForm();
        object[] _sale_type = new object[] { "ไม่เลือก", "ขายสด", "ขายเชื่อ" };
        object[] _tax_type = new object[] { "ไม่เลือก", "แยกนอก", "รวมใน", "ภาษีอัตราศูนย์" };
        private _g.g._priceListType _priceListTypeTemp;

        public _icPriceFormulaDetail()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);

                // change resource
                _sale_type[0] = MyLib._myResource._findResource("ไม่เลือก")._str;
                _sale_type[1] = MyLib._myResource._findResource("ขายสด")._str;
                _sale_type[2] = MyLib._myResource._findResource("ขายเชื่อ")._str;


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

        public void _createGrid()
        {
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);

            this._grid._clear();
            this._grid._columnListTop.Clear();
            this._grid._columnList.Clear();
            this._grid._table_name = _g.d.ic_inventory_price_formula._table;
            this._grid._addColumn(_g.d.ic_inventory_price_formula._unit_code, 1, 0, 10, false, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_0, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_1, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_2, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_3, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_4, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_5, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_6, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_7, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_8, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._price_9, 1, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._sale_type, 10, 0, 10, true, false);
            this._grid._addColumn(_g.d.ic_inventory_price_formula._tax_type, 10, 0, 10, true, false);
            //
            this._grid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_grid__cellComboBoxItem);
            this._grid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_grid__cellComboBoxGet);
            this._grid._afterAddRow += new MyLib.AfterAddRowEventHandler(_grid__afterAddRow);
            this._grid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            this._grid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_grid__queryForInsertCheck);
            //
            this._grid._columnExtraWord(_g.d.ic_inventory_price_formula._unit_code, "(F4)");
            this._grid._addColumn("roworder", 1, 0, 0, false, true, false);
            this._grid._calcPersentWidthToScatter();
            //
            this._gridResult._clear();
            this._gridResult._columnListTop.Clear();
            this._gridResult._columnList.Clear();
            this._gridResult._table_name = _g.d.ic_inventory_price_formula._table;
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._unit_code, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_0, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_1, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_2, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_3, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_4, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_5, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_6, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_7, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_8, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._price_9, 1, 0, 10, false, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._sale_type, 10, 0, 10, true, false);
            this._gridResult._addColumn(_g.d.ic_inventory_price_formula._tax_type, 10, 0, 10, true, false);
            this._gridResult._calcPersentWidthToScatter();
            //
            this._gridResult._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_grid__cellComboBoxItem);
            this._gridResult._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_grid__cellComboBoxGet);
            //
            _icTransItemGridSelectUnit._selectUnitCode += new SMLERPControl._ic._icTransItemGridSelectUnitForm._selectUnitCodeEventHandler(_icTransItemGridSelectUnit__selectUnitCode);
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_price_formula_template._table + " order by " + _g.d.ic_price_formula_template._code));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataTable __table = ((DataSet)__getData[0]).Tables[0];
            for (int __loop = 0; __loop < __table.Rows.Count; __loop++)
            {
                string __str = __table.Rows[__loop][_g.d.ic_price_formula_template._code].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_1].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_2].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_3].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_4].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_5].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_6].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_7].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_8].ToString() + "|" + __table.Rows[__loop][_g.d.ic_price_formula_template._price_9].ToString();
                this._templateComboBox.Items.Add(__str);
            }
            this._templateComboBox.SelectedIndexChanged += new EventHandler(_templateComboBox_SelectedIndexChanged);
        }

        void _templateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._grid._rowData.Count == 0)
            {
                this._grid._addRow();
                this._grid._selectRow = 0;
            }
            try
            {
                if (this._grid._selectRow != -1 && this._grid._selectRow < this._grid._rowData.Count)
                {
                    string[] __str = this._templateComboBox.SelectedItem.ToString().Split('|');
                    int __columnNumber = this._grid._findColumnByName(_g.d.ic_inventory_price_formula._price_1);
                    for (int __loop = 1; __loop < __str.Length; __loop++)
                    {
                        this._grid._cellUpdate(this._grid._selectRow, __columnNumber, __str[__loop].ToString(), false);
                        __columnNumber++;
                    }
                }
                this._grid.Invalidate();
                this._calc();
            }
            catch
            {
            }
        }

        public void _calc()
        {
            this._gridResult._clear();
            for (int __row = 0; __row < this._grid._rowData.Count; __row++)
            {
                int __addr = this._gridResult._addRow();
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._unit_code, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._unit_code).ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_0).ToString(), false);
                decimal __price0 = MyLib._myGlobal._decimalPhase(this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_0).ToString());
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_1, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_1).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_2, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_2).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_3, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_3).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_4, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_4).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_5, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_5).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_6, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_6).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_7, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_7).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_8, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_8).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._price_9, _g.g._calcFormulaPrice(1, __price0, this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._price_9).ToString())._realPrice.ToString(), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._sale_type, (int)MyLib._myGlobal._decimalPhase(this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._sale_type).ToString()), false);
                this._gridResult._cellUpdate(__addr, _g.d.ic_inventory_price_formula._tax_type, (int)MyLib._myGlobal._decimalPhase(this._grid._cellGet(__row, _g.d.ic_inventory_price_formula._tax_type).ToString()), false);
            }
            this._gridResult.Invalidate();
        }

        bool _grid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                return (this._grid._cellGet(row, _g.d.ic_inventory_price_formula._price_1).ToString().Trim().Length != 0 || this._grid._cellGet(row, _g.d.ic_inventory_price_formula._price_1).ToString().Trim().Length != 0) ? true : false;
            }

            return (this._grid._cellGet(row, _g.d.ic_inventory_price_formula._price_0).ToString().Trim().Length == 0) ? false : true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
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
            if (this._grid._selectRow < this._grid._rowData.Count)
            {
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
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._grid._findColumnByName(_g.d.ic_inventory_price_formula._unit_code))
            {
                _searchUnitName(this._grid._selectRow);
            }
            this._calc();
        }

        void _icTransItemGridSelectUnit__selectUnitCode(int mode, string unitCode)
        {
            if (mode == 1)
            {
                if (this._grid._selectRow < this._grid._rowData.Count)
                {
                    this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_trans_detail._unit_code, unitCode, false);
                    _searchUnitName(this._grid._selectRow);
                }
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
                this._grid._cellUpdate(row, _g.d.ic_inventory_price_formula._unit_code, __getUnitCode.Rows[0][0].ToString(), true);
            }
            //
        }

        string _grid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (columnName.Equals("tax_type"))
                return (_tax_type[(select == -1) ? 0 : select].ToString());

            return (_sale_type[(select == -1) ? 0 : select].ToString());
        }

        object[] _grid__cellComboBoxItem(object sender, int row, int column)
        {
            if (column == this._grid._findColumnByName(_g.d.ic_inventory_price_formula._tax_type))
                return _tax_type;
            return _sale_type;
        }

        private void _copyAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._grid._rowData.Count; __row++)
            {
                if (__row != this._grid._selectRow)
                {
                    int __columnNumber = this._grid._findColumnByName(_g.d.ic_inventory_price_formula._price_1);
                    for (int __loop = 0; __loop < 9; __loop++)
                    {
                        this._grid._cellUpdate(__row, __columnNumber, this._grid._cellGet(this._grid._selectRow, __columnNumber).ToString(), false);
                        __columnNumber++;
                    }
                }
            }
            this._grid.Invalidate();
            this._calc();
        }
    }
}
