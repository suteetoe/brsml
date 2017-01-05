using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace SMLERPIC._icPromotion
{
    public class _gridPriceAndDiscountClass : MyLib._myGrid
    {
        private SMLERPControl._searchItemForm _searchItem;
        private SMLERPControl._ic._icTransItemGridSelectUnitForm _icTransItemGridSelectUnit;

        public _gridPriceAndDiscountClass()
        {
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);

            this._table_name = _g.d.ic_promotion_formula_price_discount._table;
            this._addColumn(_g.d.ic_promotion_formula_price_discount._item_code, 1, 15, 15, true, false, true, true, "", "", "", _g.d.ic_promotion_formula_price_discount._item_code);
            this._addColumn(_g.d.ic_promotion_formula_price_discount._item_name, 1, 30, 30, false, false, false);
            this._addColumn(_g.d.ic_promotion_formula_price_discount._item_unit, 1, 10, 10, false, false);
            this._addColumn(_g.d.ic_promotion_formula_price_discount._qty_start, 3, 10, 10, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_price_discount._qty_start);
            this._addColumn(_g.d.ic_promotion_formula_price_discount._qty_end, 3, 10, 10, true, false, true, false, __formatNumberQty, "", "", _g.d.ic_promotion_formula_price_discount._qty_end);
            this._addColumn(_g.d.ic_promotion_formula_price_discount._price, 3, 10, 10, true, false, true, false, __formatNumberPrice, "", "", _g.d.ic_promotion_formula_price_discount._price);
            this._addColumn(_g.d.ic_promotion_formula_price_discount._discount, 1, 20, 20, true, false);
            this._addColumn(_g.d.ic_promotion_formula_price_discount._unit_type, 2, 1, 0, false, true);
            this._calcPersentWidthToScatter();
            this._message = "F5=Select Unit";
            this._clickSearchButton += new MyLib.SearchEventHandler(_gridPriceAndDiscountClass__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_gridPriceAndDiscountClass__alterCellUpdate);
        }

        /// <summary>
        /// เลือกหน่วยนับ
        /// </summary>
        protected void _selectUnitCode()
        {
            if (this._icTransItemGridSelectUnit == null)
            {
                this._icTransItemGridSelectUnit = new SMLERPControl._ic._icTransItemGridSelectUnitForm();
                this._icTransItemGridSelectUnit._selectUnitCode += new SMLERPControl._ic._icTransItemGridSelectUnitForm._selectUnitCodeEventHandler(_icTransItemGridSelectUnit__selectUnitCode);
            };

            if (this._selectRow != -1)
            {
                int __unitType = (int)this._cellGet(this._selectRow, _g.d.ic_promotion_formula_price_discount._unit_type);
                string __itemCode = this._cellGet(this._selectRow, _g.d.ic_promotion_formula_price_discount._item_code).ToString();
                string __itemDesc = __itemCode + "," + this._cellGet(this._selectRow, _g.d.ic_promotion_formula_price_discount._item_name).ToString();
                if (__unitType == 0)
                {
                    MessageBox.Show(__itemDesc + " : " + MyLib._myGlobal._resource("สินค้านี้มีหน่วยนับเดียว"));
                }
                else
                {
                    string __unitCode = this._cellGet(this._selectRow, _g.d.ic_promotion_formula_price_discount._unit_type).ToString();
                    this._icTransItemGridSelectUnit._itemCode = __itemCode;
                    this._icTransItemGridSelectUnit._lastCode = __unitCode;
                    this._icTransItemGridSelectUnit.Text = __itemDesc;
                    this._icTransItemGridSelectUnit.ShowDialog();
                }
            }
        }

        void _icTransItemGridSelectUnit__selectUnitCode(int mode, string unitCode)
        {
            if (mode == 1)
            {
                this._cellUpdate(this._selectRow, _g.d.ic_promotion_formula_price_discount._item_unit, unitCode, true);
            }
            this._inputCell(this._selectRow, this._selectColumn);
        }

        void _gridPriceAndDiscountClass__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._findColumnByName(_g.d.ic_promotion_formula_price_discount._item_code))
            {
                string __itemCode = this._cellGet(row, _g.d.ic_promotion_formula_price_discount._item_code).ToString();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._unit_standard + "," + _g.d.ic_inventory._unit_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'"));
                __query.Append("</node>");
                String __queryStr = __query.ToString();
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                if (__dataResult.Count > 0)
                {
                    DataTable __item = ((DataSet)__dataResult[0]).Tables[0];
                    this._cellUpdate(row, _g.d.ic_promotion_formula_price_discount._item_name, __item.Rows[0][_g.d.ic_inventory._name_1].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_promotion_formula_price_discount._item_unit, __item.Rows[0][_g.d.ic_inventory._unit_standard].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_promotion_formula_price_discount._unit_type, Int32.Parse(__item.Rows[0][_g.d.ic_inventory._unit_type].ToString()), false);
                }
            }
        }

        void _gridPriceAndDiscountClass__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (this._searchItem == null)
            {
                this._searchItem = new SMLERPControl._searchItemForm(false);
                this._searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                this._searchItem._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
                this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            }
            this._searchItem.ShowDialog();
        }

        void _searchItemSelect(MyLib._myGrid sender, int row)
        {
            this._searchItem.Close();
            string __itemCode = sender._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            this._cellUpdate(this._selectRow, this._selectColumn, __itemCode, true);
            SendKeys.Send("{TAB}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItemSelect((MyLib._myGrid)sender, e._row);
        }

        string _dataList__columnFieldNameReplace(string source)
        {
            return source.Replace("sml_value_branch_code", MyLib._myGlobal._branchCode);
        }

        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchItemSelect(sender, row);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.F5:
                        {
                            _selectUnitCode();
                            return true;
                        }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
