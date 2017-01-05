using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public class _payCreditCardGridControl : MyLib._myGrid
    {
        public delegate void _refreshDataEvent(_payCreditCardGridControl sender);
        public event _refreshDataEvent _refreshData;
        //
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        string _search_data_full_name = "";
        //string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
        //combo box
        //object[] _cerdit_card_type = new object[] {_g.d.cb_credit_card._credit_type_visa, _g.d.cb_credit_card._credit_type_master };
        //ArrayList _credit_card_type_arraylist = new ArrayList();

        public _payCreditCardGridControl()
        {
            this._table_name = _g.d.cb_trans_detail._table;
            this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 14, true, false, true, false, "", "", "", _g.d.cb_trans_detail._credit_card_no);
            this._addColumn(_g.d.cb_trans_detail._no_approved, 1, 1, 15, true, false, true);
            this._addColumn(_g.d.cb_trans_detail._credit_card_type, 1, 1, 14, true, false, true, true);
            this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 14, true, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._charge, 3, 1, 14, true, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 1, 14, false, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 15, true, false, true);
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            //this._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_pay_credit__cellComboBoxGet);
            //this._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_pay_credit__cellComboBoxItem);

            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;

            this._clickSearchButton += new MyLib.SearchEventHandler(_pay_credit__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_pay_credit__alterCellUpdate);
            this._beforeNumberBoxCheckValue += _payCreditCardGridControl__beforeNumberBoxCheckValue;
            this._totalCheck += new MyLib.TotalCheckEventHandler(_payCreditCardGridControl__totalCheck);
            this._calcPersentWidthToScatter();
            this.Invalidate();
        }

        void _payCreditCardGridControl__beforeNumberBoxCheckValue(object sender, string valueStr, int row, int columNumber)
        {
            if (columNumber == this._findColumnByName(_g.d.cb_trans_detail._charge))
            {
                decimal __amount = (decimal)this._cellGet(row, _g.d.cb_trans_detail._amount);
                if (__amount > 0 && valueStr.IndexOf("%") != -1)
                {
                    //MyLib._myNumberBox __numberBox = (MyLib._myNumberBox)sender;

                    decimal __chargeValue = MyLib._myGlobal._calcDiscountOnly(__amount, valueStr, __amount, _g.g._companyProfile._gl_no_decimal)._discountAmount;
                    this._inputNumberBox.textBox.Text = string.Format(__formatNumber, __chargeValue);
                    
                }
            }
        }

        bool _payCreditCardGridControl__totalCheck(object sender, int row, int column)
        {
            bool __result = true;
            int __columnNumber = this._findColumnByName(_g.d.cb_trans_detail._trans_number);
            if (this._cellGet(row, __columnNumber).ToString().Trim().Length == 0)
            {
                __result = false;
            }
            return __result;
        }

        void _pay_credit__alterCellUpdate(object sender, int row, int column)
        {
            try
            {
                decimal __sum_amount = MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._amount).ToString()) + MyLib._myGlobal._decimalPhase(this._cellGet(row, _g.d.cb_trans_detail._charge).ToString());
                this._cellUpdate(row, _g.d.cb_credit_card._sum_amount, __sum_amount, false);
                if (this._refreshData != null)
                {
                    this._refreshData(this);
                }
            }
            catch
            {
            }

        }

        void _pay_credit__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _search_data_full_name = _g.g._search_screen_bank_branch;
            if (e._columnName.Equals(_g.d.cb_trans_detail._bank_code)) _search_data_full_name = _g.g._search_screen_bank;
            if (e._columnName.Equals(_g.d.cb_trans_detail._credit_card_type)) _search_data_full_name = _g.g._search_screen_erp_credit_type;

            Boolean __found = false;
            int __addr = 0;
            for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
            {
                if (this._search_data_full_buffer_name[__loop].ToString().Equals(_search_data_full_name.ToLower()))
                {
                    __addr = __loop;
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                __addr = this._search_data_full_buffer_name.Add(_search_data_full_name.ToLower());
                this._search_data_full_buffer.Add((MyLib._searchDataFull)new MyLib._searchDataFull());
            }
            this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
            this._search_data_full_buffer_addr = __addr;

            if (!this._search_data_full_pointer._name.Equals(_search_data_full_name.ToLower()))
            {
                this._search_data_full_pointer.Text = e._columnName;
                this._search_data_full_pointer._name = _search_data_full_name;
                this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                this._search_data_full_pointer._dataList._loadViewFormat(_search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, this._search_data_full_pointer, false);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __result = "";
            this._search_data_full_pointer.Close();
            if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_bank))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_bank._table + "." + _g.d.erp_bank._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_code, __result, true);

            }
            else if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_bank_branch))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_bank_branch._table + "." + _g.d.erp_bank_branch._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_branch, __result, true);
            }
            else if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_erp_credit_type))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_credit_type._table + "." + _g.d.erp_credit_type._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._credit_card_type, __result, true);
            }
            SendKeys.Send("{ENTER}");
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
            SendKeys.Send("{ENTER}");
        }

        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        private void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_screen_bank))
            {
                string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    this._search_data_full_pointer.Close();
                    this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_code, result, true);
                }
            }
            else
                if (name.Equals(_g.g._search_screen_bank_branch))
                {
                    string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                    if (result.Length > 0)
                    {
                        this._search_data_full_pointer.Close();
                        this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._bank_code, result, true);
                    }
                }
                else
                    if (name.Equals(_g.g._search_screen_erp_credit_type))
                    {
                        string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                        if (result.Length > 0)
                        {
                            this._search_data_full_pointer.Close();
                            this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._credit_card_type, result, true);
                        }
                    }
        }
    }
}
