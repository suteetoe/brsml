using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace _g._changeCode
{
    public partial class _itemCodeUserControl : UserControl
    {
        public MyLib._searchDataFull _searchItem = new MyLib._searchDataFull();
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _itemCodeUserControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._grid._table_name = _g.d.ic_inventory._table;
            this._grid._addColumn(_g.d.ic_inventory._code_old, 1, 20, 20, true, false, true, true);
            this._grid._addColumn(_g.d.ic_inventory._code, 1, 20, 20, true, false);
            this._grid._addColumn(_g.d.ic_inventory._name_1, 1, 20, 40, false, false);
            this._grid._addColumn(_g.d.ic_inventory._remark, 1, 20, 20, false, false);
            this._grid._clickSearchButton += new MyLib.SearchEventHandler(_grid__clickSearchButton);
            this._grid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);
            //
            this._searchItem._showMode = 0;
            this._searchItem.WindowState = FormWindowState.Maximized;
            this._searchItem._name = _g.g._search_screen_ic_inventory;
            this._searchItem._dataList._tableName = _g.d.ic_inventory._table;
            this._searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
            this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._searchItem._dataList._loadViewFormat(this._searchItem._name, MyLib._myGlobal._userSearchScreenGroup, false);
            //
            this._processButton.Click += new EventHandler(_processButton_Click);
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemCodeOldColumn = this._grid._findColumnByName(_g.d.ic_inventory._code_old);
            int __getItemCodeColumn = this._grid._findColumnByName(_g.d.ic_inventory._code);
            if (column == __getItemCodeOldColumn)
            {
                string __itemCode = this._grid._cellGet(row, _g.d.ic_inventory._code_old).ToString();
                string __query = "select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + MyLib._myGlobal._convertStrToQuery(MyLib._myUtil._convertTextToXml(__itemCode)) + "\'";
                string __itemName = "";
                DataTable __getData = this._myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    __itemName = __getData.Rows[0][_g.d.ic_inventory._name_1].ToString();
                }
                this._grid._cellUpdate(row, _g.d.ic_inventory._name_1, __itemName, false);
            }
            if (column == __getItemCodeColumn)
            {
                string __itemCode = this._grid._cellGet(row, _g.d.ic_inventory._code).ToString();
                string __query = "select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + MyLib._myGlobal._convertStrToQuery(MyLib._myUtil._convertTextToXml(__itemCode)) + "\'";
                DataTable __getData = this._myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("รหัสสินค้าใหม่ซ้ำ"));
                    this._grid._cellUpdate(row, _g.d.ic_inventory._code, "", false);
                }
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            string __name = __getParent2._name;
            string __itemCode = (string)this._searchItem._dataList._gridData._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
            this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_inventory._code_old, __itemCode, true);
            this._searchItem.Close();
            SendKeys.Send("{TAB}");
        }

        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            string __itemCode = (string)this._searchItem._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
            this._grid._cellUpdate(this._grid._selectRow, _g.d.ic_inventory._code_old, __itemCode, true);
            this._searchItem.Close();
            SendKeys.Send("{TAB}");
        }

        void _grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItem.ShowDialog();
        }

        string _command(string tableName, string fieldName, int row)
        {
            string __itemCodeOld = this._grid._cellGet(row, _g.d.ic_inventory._code_old).ToString().ToUpper();
            string __itemCodeNew = this._grid._cellGet(row, _g.d.ic_inventory._code).ToString().Trim().ToUpper();
            StringBuilder __result = new StringBuilder();
            if (__itemCodeOld.Equals(__itemCodeNew) == false)
            {
                //string __deleteFormat = "delete from {0} where {1}=\'" + __itemCodeNew + "\'";
                string __updateFormat = "update {0} set {1}=\'" + MyLib._myGlobal._convertStrToQuery(__itemCodeNew) + "\' where {1}=\'" + MyLib._myGlobal._convertStrToQuery(__itemCodeOld) + "\'";
                //__result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__deleteFormat), tableName, fieldName));
                __result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__updateFormat), tableName, fieldName));
            }
            return __result.ToString();
        }

        void _processButton_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                {
                    string __itemName = this._grid._cellGet(__row, _g.d.ic_inventory._name_1).ToString().Trim().ToUpper();
                    string __itemCode = this._grid._cellGet(__row, _g.d.ic_inventory._code).ToString().Trim().ToUpper();
                    string __remark = "Skip";
                    if (__itemName.Trim().Length > 0 && __itemCode.Length > 0)
                    {
                        __query.Append(this._command(_g.d.ic_inventory._table, _g.d.ic_inventory._code, __row));
                        __query.Append(this._command(_g.d.ic_extra_detail._table, _g.d.ic_extra_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_color_use._table, _g.d.ic_color_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_append._table, _g.d.ic_inventory_append._ic_append_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_append_detail._table, _g.d.ic_inventory_append_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_barcode._table, _g.d.ic_inventory_barcode._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_detail._table, _g.d.ic_inventory_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.images._table, _g.d.images._image_id, __row));
                        __query.Append(this._command(_g.d.ic_inventory_price._table, _g.d.ic_inventory_price._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_purchase_price._table, _g.d.ic_inventory_purchase_price._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_replace._table, _g.d.ic_inventory_replace._ic_replace_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_replace_detail._table, _g.d.ic_inventory_replace_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_set_detail._table, _g.d.ic_inventory_set_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_billing._table, _g.d.ic_name_billing._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_merket._table, _g.d.ic_name_merket._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_pos._table, _g.d.ic_name_pos._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_short._table, _g.d.ic_name_short._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_opposite_unit._table, _g.d.ic_opposite_unit._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_pattern_use._table, _g.d.ic_pattern_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_promotion_detail._table, _g.d.ic_promotion_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_permium_condition._table, _g.d.ic_purchase_permium_condition._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_permium_list._table, _g.d.ic_purchase_permium_list._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_price_detail._table, _g.d.ic_purchase_price_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_relation_detail._table, _g.d.ic_relation_detail._ic_code_1, __row));
                        __query.Append(this._command(_g.d.ic_serial._table, _g.d.ic_serial._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_size_use._table, _g.d.ic_size_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_stk_build._table, _g.d.ic_stk_build._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_stk_build_detail._table, _g.d.ic_stk_build_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._item_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_detail_lot._table, _g.d.ic_trans_detail_lot._item_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_wh_shelf._table, _g.d.ic_wh_shelf._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_level._table, _g.d.ic_inventory_level._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code, __row));

                        __remark = "Process";
                    }
                    this._grid._cellUpdate(__row, _g.d.ic_inventory._remark, __remark, false);
                }
                __query.Append("</node>");
                string __result = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Fail : " + __result);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _clipBoardButton_Click(object sender, EventArgs e)
        {
            try
            {
                string __str = Clipboard.GetText();
                string[] __rowStr = __str.Replace("\r", "").Split('\n');
                for (int __row = 0; __row < __rowStr.Length; __row++)
                {
                    string[] __field = __rowStr[__row].ToString().Split('\t');
                    if (__field.Length == 2)
                    {
                        string __itemOldCode = __field[0].ToString();
                        string __itemNewCode = __field[1].ToString();
                        int __addr = this._grid._addRow();
                        this._grid._cellUpdate(__addr, _g.d.ic_inventory._code_old, __itemOldCode, true);
                        this._grid._cellUpdate(__addr, _g.d.ic_inventory._code, __itemNewCode, true);
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._grid._clear();
        }
    }
}
