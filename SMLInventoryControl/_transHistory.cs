using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _transHistory : UserControl
    {
        string __formatNumberQty = _g.g._getFormatNumberStr(1);
        string __formatNumberPrice = _g.g._getFormatNumberStr(2);
        string __formatNumberAmount = _g.g._getFormatNumberStr(3);
        _transHistoryType historyTypeTemp;
        DataTable _dataTable;

        public _transHistory(_transHistoryType historyType, Boolean search)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this.historyTypeTemp = historyType;
            this._resultGrid._table_name = _g.d.ic_trans_detail._table;
            this._resultGrid._addColumn(_g.d.ic_trans_detail._doc_date, 4, 10, 10);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._doc_time, 1, 10, 5);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._doc_no, 1, 10, 10);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._cust_code, 1, 15, 15);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 15, 15);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._unit_name, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._wh_code, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 5, 5);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._price, 3, 10, 10, false, false, false, false, __formatNumberPrice);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._discount, 1, 8, 5);
            this._resultGrid._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            this._resultGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_resultGrid__beforeDisplayRow);
            //
            this._resultGrid._calcPersentWidthToScatter();
            this._condition._build(historyType, search);
            this._searchTextbox.textBox.KeyUp += textBox_KeyUp;
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            // filter
            if (this._dataTable != null && this._dataTable.Rows.Count > 0)
            {
                // filter and reload data

                // pack select
                string __fileter = _g.d.ic_trans_detail._item_name + " like '%" + this._searchTextbox.textBox.Text + "%' ";

                DataRow[] __select = this._dataTable.Select(__fileter);


                this._resultGrid._loadFromDataTable(this._dataTable, __select);
            }
        }

        MyLib.BeforeDisplayRowReturn _resultGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            // กรณีส่วนลด ให้ชิดขวา
            int __discountColumnNumber = sender._findColumnByName(_g.d.ic_trans_detail._discount);
            if (columnNumber == __discountColumnNumber)
            {
                senderRow.align = ContentAlignment.MiddleRight;
            }
            return (senderRow);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void _process()
        {
            string __custCondition = "";
            string __custCodeBegin = this._condition._getDataStr(_g.d.ic_resource._cust_code_begin).ToLower();
            string __custCodeEnd = this._condition._getDataStr(_g.d.ic_resource._cust_code_end).ToLower();
            //
            if (__custCodeBegin.Length != 0 && __custCodeEnd.Length != 0)
            {
                __custCondition = " and (lower(" + _g.d.ic_trans_detail._cust_code + ") between \'" + __custCodeBegin + "\' and \'" + __custCodeEnd + "\') ";
            }
            else
                if (__custCodeBegin.Length != 0 && __custCodeEnd.Length == 0)
                {
                    __custCondition = " and (lower(" + _g.d.ic_trans_detail._cust_code + ") >= \'" + __custCodeEnd + "\') ";
                }
                else
                    if (__custCodeBegin.Length == 0 && __custCodeEnd.Length != 0)
                    {
                        __custCondition = " and (lower(" + _g.d.ic_trans_detail._cust_code + ") <= \'" + __custCodeEnd + "\') ";
                    }
            //
            string __itemCondition = "";
            string __itemCodeBegin = this._condition._getDataStr(_g.d.ic_resource._ic_code_begin).ToLower();
            string __itemCodeEnd = this._condition._getDataStr(_g.d.ic_resource._ic_code_end).ToLower();
            //
            if (__itemCodeBegin.Length != 0 && __itemCodeEnd.Length != 0)
            {
                __itemCondition = " and (lower(" + _g.d.ic_trans_detail._item_code + ") between \'" + __itemCodeBegin + "\' and \'" + __itemCodeEnd + "\') ";
            }
            else
                if (__itemCodeBegin.Length != 0 && __itemCodeEnd.Length == 0)
                {
                    __itemCondition = " and (lower(" + _g.d.ic_trans_detail._item_code + ") >= \'" + __itemCodeBegin + "\') ";
                }
                else
                    if (__itemCodeBegin.Length == 0 && __itemCodeEnd.Length != 0)
                    {
                        __itemCondition = " and (lower(" + _g.d.ic_trans_detail._item_code + ") <= \'" + __itemCodeEnd + "\') ";
                    }
            //
            int __transFlag = (this.historyTypeTemp == _transHistoryType.ประวัติการขาย) ? _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) : _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
            StringBuilder __query = new StringBuilder("select " + _g.d.ic_trans_detail._doc_date + "," +
                        _g.d.ic_trans_detail._doc_time + "," +
                        _g.d.ic_trans_detail._doc_no + ",");
            if (this.historyTypeTemp == _transHistoryType.ประวัติการซื้อ)
            {
                __query.Append("(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ")||\'(\'||" + _g.d.ic_trans_detail._cust_code + "" + "||\')\' as " + _g.d.ic_trans_detail._cust_code + ",");
            }
            else
            {
                __query.Append("(select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ")||\'(\'||" + _g.d.ic_trans_detail._cust_code + "" + "||\')\' as " + _g.d.ic_trans_detail._cust_code + ",");
            }
            __query.Append("(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ")||\'(\'||" + _g.d.ic_trans_detail._item_code + "||\')\' as " + _g.d.ic_trans_detail._item_name + "," +
            "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ")||\'(\'||" + _g.d.ic_trans_detail._unit_code + "||\')\' as " + _g.d.ic_trans_detail._unit_name + "," +
            _g.d.ic_trans_detail._wh_code + "," +
            _g.d.ic_trans_detail._shelf_code + "," +
            _g.d.ic_trans_detail._qty + "," +
            _g.d.ic_trans_detail._price + "," +
            _g.d.ic_trans_detail._discount + "," +
            _g.d.ic_trans_detail._sum_amount +
            " from " + _g.d.ic_trans_detail._table +
            " where " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag.ToString() + __itemCondition + __custCondition +
            " order by " + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time + " desc");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            _dataTable = __myFrameWork._queryShort(__query.ToString()).Tables[0];
            this._resultGrid._loadFromDataTable(_dataTable);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._process();
        }
    }

    public class _saleHistoryConditionScreen : MyLib._myScreen
    {
        _transHistoryType historyTypeTemp;
        MyLib._searchDataFull _searchCust;
        MyLib._searchDataFull _searchItem;

        public _saleHistoryConditionScreen()
        {
        }

        public void _build(_transHistoryType historyType, Boolean search)
        {
            this.historyTypeTemp = historyType;
            //
            this._table_name = _g.d.ic_resource._table;
            this._maxColumn = 2;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_resource._cust_code_begin, 1, 25, 1, true, false, false, true, true, (historyType == _transHistoryType.ประวัติการซื้อ) ? _g.d.ic_resource._ap_code_begin : _g.d.ic_resource._ar_code_begin);
            this._addTextBox(0, 1, 0, 0, _g.d.ic_resource._cust_code_end, 1, 25, 1, true, false, false, true, true, (historyType == _transHistoryType.ประวัติการซื้อ) ? _g.d.ic_resource._ap_code_end : _g.d.ic_resource._ar_code_end);
            this._addTextBox(1, 0, 0, 0, _g.d.ic_resource._ic_code_begin, 1, 25, 1, true, false, false);
            this._addTextBox(1, 1, 0, 0, _g.d.ic_resource._ic_code_end, 1, 25, 1, true, false, false);
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_saleHistoryConditionScreen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_saleHistoryConditionScreen__textBoxChanged);
            //
            if (search)
            {
                this._searchCust = new MyLib._searchDataFull();
                this._searchItem = new MyLib._searchDataFull();
                //
                string __searchCustName = (historyType == _transHistoryType.ประวัติการซื้อ) ? _g.g._screen_ap_supplier_search : _g.g._search_screen_ar;
                this._searchCust._dataList._loadViewFormat(__searchCustName, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchCust._dataList._refreshData();
                this._searchCust._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);

                this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchItem._dataList._refreshData();
                this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__item_mouseClick);
            }
        }

        void _gridData__item_mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItem.Close();
            string __itemCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            if (this._searchItem._name.Equals(_g.d.ic_resource._ic_code_begin))
            {
                this._setDataStr(_g.d.ic_resource._ic_code_begin, __itemCode);
            }
            else
                if (this._searchItem._name.Equals(_g.d.ic_resource._ic_code_end))
                {
                    this._setDataStr(_g.d.ic_resource._ic_code_end, __itemCode);
                }
            SendKeys.Send("{TAB}");
        }

        string _getItemName(string itemCode)
        {
            string __itemName = "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where lower(" + _g.d.ic_inventory._code + ")=\'" + itemCode.ToLower() + "\'").Tables[0];
            if (__getItem.Rows.Count > 0)
            {
                __itemName = __getItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
            }
            return __itemName;
        }

        string _getCustName(string custCode)
        {
            string __custName = "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getCust = __myFrameWork._queryShort("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where lower(" + _g.d.ar_customer._code + ")=\'" + custCode.ToLower() + "\'").Tables[0];
            if (__getCust.Rows.Count > 0)
            {
                __custName = __getCust.Rows[0][_g.d.ar_customer._name_1].ToString();
            }
            return __custName;
        }

        void _saleHistoryConditionScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._cust_code_begin))
            {
                string __custCode = this._getDataStr(_g.d.ic_resource._cust_code_begin).ToUpper();
                string __getName = _getCustName(__custCode);
                this._setDataStr(_g.d.ic_resource._cust_code_begin, __custCode, __getName, true);
            }
            else
                if (name.Equals(_g.d.ic_resource._cust_code_end))
                {
                    string __custCode = this._getDataStr(_g.d.ic_resource._cust_code_end).ToUpper();
                    string __getName = _getCustName(__custCode);
                    this._setDataStr(_g.d.ic_resource._cust_code_end, __custCode, __getName, true);
                }
                else
                    if (name.Equals(_g.d.ic_resource._ic_code_begin))
                    {
                        string __itemCode = this._getDataStr(_g.d.ic_resource._ic_code_begin).ToUpper();
                        string __getName = _getItemName(__itemCode);
                        this._setDataStr(_g.d.ic_resource._ic_code_begin, __itemCode, __getName, true);
                    }
                    else
                        if (name.Equals(_g.d.ic_resource._ic_code_end))
                        {
                            string __itemCode = this._getDataStr(_g.d.ic_resource._ic_code_end).ToUpper();
                            string __getName = _getItemName(__itemCode);
                            this._setDataStr(_g.d.ic_resource._ic_code_end, __itemCode, __getName, true);
                        }
        }

        void _saleHistoryConditionScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)sender;
            if (__textBox._name.Equals(_g.d.ic_resource._cust_code_begin))
            {
                this._searchItem._name = _g.d.ic_resource._cust_code_begin;
                this._searchItem.Text = __textBox._label.Text;
                this._searchCust.StartPosition = FormStartPosition.CenterScreen;
                this._searchCust.ShowDialog();
            }
            else
                if (__textBox._name.Equals(_g.d.ic_resource._cust_code_end))
                {
                    this._searchItem._name = _g.d.ic_resource._cust_code_end;
                    this._searchCust.Text = __textBox._label.Text;
                    this._searchCust.StartPosition = FormStartPosition.CenterScreen;
                    this._searchCust.ShowDialog();
                }
                else
                    if (__textBox._name.Equals(_g.d.ic_resource._ic_code_begin))
                    {
                        this._searchItem._name = _g.d.ic_resource._ic_code_begin;
                        this._searchItem.Text = __textBox._label.Text;
                        this._searchItem.StartPosition = FormStartPosition.CenterScreen;
                        this._searchItem.ShowDialog();
                    }
                    else
                        if (__textBox._name.Equals(_g.d.ic_resource._ic_code_end))
                        {
                            this._searchItem._name = _g.d.ic_resource._ic_code_end;
                            this._searchItem.Text = __textBox._label.Text;
                            this._searchItem.StartPosition = FormStartPosition.CenterScreen;
                            this._searchItem.ShowDialog();
                        }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchCust.Close();
            string __code = "";
            if (this.historyTypeTemp == _transHistoryType.ประวัติการซื้อ)
            {
                __code = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code).ToString();
            }
            else
            {
                __code = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
            }
            if (this._searchItem._name.Equals(_g.d.ic_resource._cust_code_begin))
            {
                this._setDataStr(_g.d.ic_resource._cust_code_begin, __code);
            }
            else
                if (this._searchItem._name.Equals(_g.d.ic_resource._cust_code_end))
                {
                    this._setDataStr(_g.d.ic_resource._cust_code_end, __code);
                }
            SendKeys.Send("{TAB}");
        }
    }

    public enum _transHistoryType
    {
        ประวัติการขาย,
        ประวัติการซื้อ
    }
}
