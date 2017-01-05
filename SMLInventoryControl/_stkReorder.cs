using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _stkReorder : UserControl
    {
        string __formatNumberQty = _g.g._getFormatNumberStr(1);
        string __formatNumberPrice = _g.g._getFormatNumberStr(2);
        string __formatNumberAmount = _g.g._getFormatNumberStr(3);
        int _mode = 0;
        public event SaveEventHandler _save;
        /// <summary>
        /// 0=Display Only
        /// </summary>
        /// <param name="mode"></param>
        public _stkReorder(int mode)
        {
            this._mode = mode;
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._resultGrid._table_name = _g.d.ic_resource._table;
            if (mode == 1)
            {
                this._resultGrid._addColumn("Select", 11, 10, 5, true, false, false, false, __formatNumberQty);
                this._closeButton.Text = "Save";
            }
            this._resultGrid._addColumn(_g.d.ic_resource._ic_code, 1, 10, 10, false, false, false);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_name, 1, 10, 20, false, false, false);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_unit_code, 1, 5, 8, false, false, false);
            //
            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            this._resultGrid._addColumn(_g.d.ic_resource._purchase_point, 3, 10, 8, false, false, false, false, __formatNumberQty);
            this._resultGrid._addColumn(_g.d.ic_resource._purchase_balance_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            if (mode == 1)
            {
                this._resultGrid._addColumn(_g.d.ic_resource._purchase_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            }
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
            this._resultGrid._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_resultGrid__afterSelectRow);

            // ประวัติการสั่งซื้อ
            this._purchaseInfoGrid._columnTopActive = true;
            this._purchaseInfoGrid._table_name = _g.d.ic_resource._table;
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._purchase_last_date, 4, 5, 10, true, false, true, false, "dd/MM/yyyy");
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._purchase_last_docno, 1, 5, 10);
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._date_expire, 4, 5, 10, true, false, true, false, "dd/MM/yyyy");
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._ap_detail, 1, 5, 15);
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._purchase_last_unit, 1, 3, 5);
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._purchase_last_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._purchase_last_price, 3, 10, 8, false, false, false, false, __formatNumberPrice);
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._purchase_last_discount, 1, 3, 5);
            this._purchaseInfoGrid._addColumn(_g.d.ic_resource._purchase_last_amount, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            //
            this._purchaseInfoGrid._setColumnBackground(_g.d.ic_resource._purchase_last_qty, Color.AliceBlue);
            this._purchaseInfoGrid._setColumnBackground(_g.d.ic_resource._purchase_last_price, Color.AliceBlue);
            this._purchaseInfoGrid._setColumnBackground(_g.d.ic_resource._purchase_last_amount, Color.AliceBlue);
            //
            this._purchaseInfoGrid._addColumnTop(_g.d.ic_resource._purchase_last, this._purchaseInfoGrid._findColumnByName(_g.d.ic_resource._purchase_last_date), this._purchaseInfoGrid._findColumnByName(_g.d.ic_resource._purchase_last_amount));
            //
            this._purchaseInfoGrid._total_show = true;
            this._purchaseInfoGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_purchaseInfoGrid__beforeDisplayRow);
            this._purchaseInfoGrid._calcPersentWidthToScatter();

            // ประวัติการซื้อ
            this._buyInfoGrid._columnTopActive = true;
            this._buyInfoGrid._table_name = _g.d.ic_resource._table;
            this._buyInfoGrid._addColumn(_g.d.ic_resource._buy_last_date, 4, 5, 10, true, false, true, false, "dd/MM/yyyy");
            this._buyInfoGrid._addColumn(_g.d.ic_resource._buy_last_docno, 1, 5, 10);
            this._buyInfoGrid._addColumn(_g.d.ic_resource._ap_detail, 1, 5, 15);
            this._buyInfoGrid._addColumn(_g.d.ic_resource._buy_last_unit, 1, 3, 5);
            this._buyInfoGrid._addColumn(_g.d.ic_resource._buy_last_qty, 3, 10, 8, false, false, false, false, __formatNumberQty);
            this._buyInfoGrid._addColumn(_g.d.ic_resource._buy_last_price, 3, 10, 8, false, false, false, false, __formatNumberPrice);
            this._buyInfoGrid._addColumn(_g.d.ic_resource._buy_last_discount, 1, 3, 5);
            this._buyInfoGrid._addColumn(_g.d.ic_resource._buy_last_amount, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            //
            this._buyInfoGrid._setColumnBackground(_g.d.ic_resource._buy_last_qty, Color.AliceBlue);
            this._buyInfoGrid._setColumnBackground(_g.d.ic_resource._buy_last_price, Color.AliceBlue);
            this._buyInfoGrid._setColumnBackground(_g.d.ic_resource._buy_last_amount, Color.AliceBlue);
            //
            this._buyInfoGrid._addColumnTop(_g.d.ic_resource._buy_last, this._buyInfoGrid._findColumnByName(_g.d.ic_resource._buy_last_date), this._buyInfoGrid._findColumnByName(_g.d.ic_resource._buy_last_amount));
            //
            this._buyInfoGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_buyInfoGrid__beforeDisplayRow);
            this._buyInfoGrid._total_show = true;
            this._buyInfoGrid._calcPersentWidthToScatter();
            //
        }

        MyLib.BeforeDisplayRowReturn _buyInfoGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            // กรณีส่วนลด ให้ชิดขวา
            int __discountColumnNumber = sender._findColumnByName(_g.d.ic_resource._buy_last_discount);
            if (columnNumber == __discountColumnNumber)
            {
                senderRow.align = ContentAlignment.MiddleRight;
            }
            return (senderRow);
        }

        MyLib.BeforeDisplayRowReturn _purchaseInfoGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            // กรณีส่วนลด ให้ชิดขวา
            int __discountColumnNumber = sender._findColumnByName(_g.d.ic_resource._purchase_last_discount);
            if (columnNumber == __discountColumnNumber)
            {
                senderRow.align = ContentAlignment.MiddleRight;
            }
            string __getExpire = sender._cellGet(row, _g.d.ic_resource._date_expire).ToString();
            if (__getExpire.Length > 0)
            {
                string __format = "yyyy-MM-dd";
                if (DateTime.Now.ToString(__format).CompareTo(MyLib._myGlobal._convertDate(__getExpire).ToString(__format)) > 0)
                {
                    senderRow.newColor = Color.Red;
                }

            }
            return (senderRow);
        }

        void _resultGrid__afterSelectRow(object sender, int row)
        {
            string __itemCode = this._resultGrid._cellGet(row, _g.d.ic_resource._ic_code).ToString().Trim();
            // ประวัติการสั่งซื้อ
            if (row < this._resultGrid._rowData.Count)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select " + _g.d.ic_trans_detail._doc_date + " as " + _g.d.ic_resource._purchase_last_date + "," +
                    "(select " + _g.d.ic_trans._expire_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ) + ") as " + _g.d.ic_resource._date_expire + "," +
                    _g.d.ic_trans_detail._doc_no + " as " + _g.d.ic_resource._purchase_last_docno + "," +
                    _g.d.ic_trans_detail._cust_code + "||'~'||(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ") as " + _g.d.ic_resource._ap_detail + "," +
                    _g.d.ic_trans_detail._unit_code + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_resource._purchase_last_unit + "," +
                    _g.d.ic_trans_detail._qty + " as " + _g.d.ic_resource._purchase_last_qty + "," +
                    _g.d.ic_trans_detail._price + " as " + _g.d.ic_resource._purchase_last_price + "," +
                    _g.d.ic_trans_detail._discount + " as " + _g.d.ic_resource._purchase_last_discount + "," +
                    _g.d.ic_trans_detail._sum_amount + " as " + _g.d.ic_resource._purchase_last_amount +
                    " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode + "\' " +
                    " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ) +
                    " order by " + _g.d.ic_trans_detail._doc_date + " desc";
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                this._purchaseInfoGrid._loadFromDataTable(__result.Tables[0]);
            }
            else
            {
                this._purchaseInfoGrid._clear();
            }
            this._purchaseInfoGrid.Invalidate();
            // ประวัติการซื้อ
            if (row < this._resultGrid._rowData.Count)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select " + _g.d.ic_trans_detail._doc_date + " as " + _g.d.ic_resource._buy_last_date + "," +
                    _g.d.ic_trans_detail._doc_no + " as " + _g.d.ic_resource._buy_last_docno + "," +
                    _g.d.ic_trans_detail._cust_code + "||'~'||(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code + ") as " + _g.d.ic_resource._ap_detail + "," +
                    _g.d.ic_trans_detail._unit_code + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_resource._buy_last_unit + "," +
                    _g.d.ic_trans_detail._qty + " as " + _g.d.ic_resource._buy_last_qty + "," +
                    _g.d.ic_trans_detail._price + " as " + _g.d.ic_resource._buy_last_price + "," +
                    _g.d.ic_trans_detail._discount + " as " + _g.d.ic_resource._buy_last_discount + "," +
                    _g.d.ic_trans_detail._sum_amount + " as " + _g.d.ic_resource._buy_last_amount +
                    " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._item_code + "=\'" + __itemCode + "\' " +
                    " and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) +
                    " order by " + _g.d.ic_trans_detail._doc_date + " desc";
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                this._buyInfoGrid._loadFromDataTable(__result.Tables[0]);
            }
            else
            {
                this._buyInfoGrid._clear();
            }
            this._buyInfoGrid.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            if (this._mode == 0)
            {
                this.Dispose();
            }
            else
            {
                if (this._save != null)
                {
                    this._save(this);
                }
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __purchasePointQuery = "(select " + _g.d.ic_inventory_detail._purchase_point + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ")";
            string __query = "select " + _g.d.ic_inventory._code + " as " + _g.d.ic_resource._ic_code + "," +
                _g.d.ic_inventory._name_1 + " as " + _g.d.ic_resource._ic_name + "," +
                _g.d.ic_inventory._unit_standard + "||'~'||" + _g.d.ic_inventory._unit_standard_name + " as " + _g.d.ic_resource._ic_unit_code + "," +
                _g.d.ic_inventory._balance_qty + " as " + _g.d.ic_resource._balance_qty + "," +
                __purchasePointQuery + " as " + _g.d.ic_resource._purchase_point + "," +
                _g.d.ic_inventory._accrued_in_qty + " as " + _g.d.ic_resource._purchase_balance_qty +
                " from " + _g.d.ic_inventory._table +
                " where " + _g.d.ic_inventory._item_type + "<>5 and " + __purchasePointQuery + ">0 and " + _g.d.ic_inventory._balance_qty + "<" + __purchasePointQuery + " order by " + _g.d.ic_inventory._code;
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            this._resultGrid._loadFromDataTable(__result.Tables[0]);
            if (this._mode == 1)
            {
                for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
                {
                    decimal __purchaseQty = 0.0M;
                    decimal __balanceQty = MyLib._myGlobal._decimalPhase(this._resultGrid._cellGet(__row, _g.d.ic_resource._balance_qty).ToString());
                    decimal __PurchasePoint = MyLib._myGlobal._decimalPhase(this._resultGrid._cellGet(__row, _g.d.ic_resource._purchase_point).ToString());
                    decimal __purchaseBalanceQty = MyLib._myGlobal._decimalPhase(this._resultGrid._cellGet(__row, _g.d.ic_resource._purchase_balance_qty).ToString());
                    __purchaseQty = __PurchasePoint - (__balanceQty + __purchaseBalanceQty);
                    this._resultGrid._cellUpdate(__row, _g.d.ic_resource._purchase_qty, __purchaseQty, false);
                }
            }
        }
    }
    public class _stkReorderConditionScreen : MyLib._myScreen
    {
        MyLib._searchDataFull _searchItem = new MyLib._searchDataFull();

        public _stkReorderConditionScreen()
        {
            this._table_name = _g.d.ic_resource._table;
            this._maxColumn = 2;
            this._addTextBox(0, 0, 0, 0, _g.d.ic_resource._ic_code_begin, 1, 25, 1, true, false, false);
            this._addTextBox(0, 1, 0, 0, _g.d.ic_resource._ic_code_end, 1, 25, 1, true, false, false);
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_stkMovementConditionScreen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_stkMovementConditionScreen__textBoxChanged);
            //
            this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchItem._dataList._refreshData();
            this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
        }

        void _stkMovementConditionScreen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._ic_code_begin))
            {
                string __itemCode = this._getDataStr(_g.d.ic_resource._ic_code_begin);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'").Tables[0];
                string __itemName = "";
                if (__getItem.Rows.Count > 0)
                {
                    __itemName = __getItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
                }
                this._setDataStr(_g.d.ic_resource._ic_code_begin, __itemCode, __itemName, true);
            }
            if (name.Equals(_g.d.ic_resource._ic_code_end))
            {
                string __itemCode = this._getDataStr(_g.d.ic_resource._ic_code_end);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getItem = __myFrameWork._queryShort("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'").Tables[0];
                string __itemName = "";
                if (__getItem.Rows.Count > 0)
                {
                    __itemName = __getItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
                }
                this._setDataStr(_g.d.ic_resource._ic_code_end, __itemCode, __itemName, true);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItem.Close();
            string __itemCode = ((MyLib._myGrid)sender)._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            //
            if (this._searchItem._name.Equals(_g.d.ic_resource._ic_code_begin))
                this._setDataStr(_g.d.ic_resource._ic_code_begin, __itemCode);
            else
                this._setDataStr(_g.d.ic_resource._ic_code_end, __itemCode);
            //
            SendKeys.Send("{TAB}");
        }

        void _stkMovementConditionScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)sender;
            this._searchItem._name = __textBox._name;
            this._searchItem.Text = __textBox._labelName;
            this._searchItem.StartPosition = FormStartPosition.CenterScreen;
            this._searchItem.ShowDialog();
        }
    }

    public delegate void SaveEventHandler(object sender);
}
