using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPOSControl._food
{
    public partial class _tableOrderEditControl : UserControl
    {
        public delegate void ScreenProcessComplete();
        public event ScreenProcessComplete _processComplete;

        _orderListGridControl _orderList = new _orderListGridControl();
        _itemGridControl _itemByOrder = new _itemGridControl(3, true);
        _orderScreenControl _orderScreen = new _orderScreenControl();

        private string _saleCode = "";
        private string _saleName = "";
        private string _tableName = "";
        private string _device_id = "";

        /// <summary>
        /// 0=ตามใบสั่งอาหาร,1=แสดงทั้งหมด
        /// </summary>
        private string _tableNumber = "";
        _tableSearchLevelMenuControl _table = null;
        Boolean _can_edit_price_user = false;

        public _tableOrderEditControl(string saleCode, string saleName, string tableNumber, string tableName, string device_id, string device_doc_format, _tableSearchLevelMenuControl __table, int barcodePriceLevel)
        {
            InitializeComponent();

            this._tableNumber = tableNumber;
            _table = __table;
            this._saleCode = saleCode;
            this._saleName = saleName;
            this._tableName = tableName;
            this._device_id = device_id;

            this._deviceLabel.Text = this._device_id;
            this._tableLabel.Text = this._tableNumber + "/" + this._tableName;
            this._saleLabel.Text = this._saleCode + "/" + this._saleName;

            //this._tableSearch.Dock = DockStyle.Fill;
            //this._tableLevelPanel.Controls.Add(this._tableSearch);
            this._orderList._total_show = true;
            this._orderList.Dock = DockStyle.Fill;
            this._orderDocListPanel.Controls.Add(this._orderList);
            this._itemByOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._orderList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this._itemByOrder._message = "Double Click=Edit";

            this._itemByOrder.Dock = DockStyle.Fill;
            //this._orderDetailPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._orderScreen.AutoSize = true;
            this._orderScreen.Dock = DockStyle.Top;
            this._orderDetailPanel.Controls.Add(this._itemByOrder);
            this._orderDetailPanel.Controls.Add(this._orderScreen);

            //this._buildView();
            _loadData(0, true);
            //
            //this._tableSearch._menuTableClick += new _tableSearchLevelControl.MenuTableClick(_tableSearch__menuTableClick);
            this._orderList._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_orderList__afterSelectRow);
            this._itemByOrder._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_itemByOrder__mouseDoubleClick);
        }

        _foodDetailForm _detailEditForm = null;
        void _itemByOrder__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (this._itemByOrder._rowData.Count > e._row && e._row != -1)
            {
                if (MessageBox.Show("ยืนยันการแก้ไขรายละเอียดการสั่งอาหาร", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int editrow = e._row;
                    string __productName = (string)this._itemByOrder._cellGet(editrow, _g.d.table_order._item_name);
                    decimal __qty = (decimal)this._itemByOrder._cellGet(editrow, _g.d.table_order._qty);
                    decimal __price = (decimal)this._itemByOrder._cellGet(editrow, _g.d.table_order._price);
                    string __suggest_remark = (string)this._itemByOrder._cellGet(editrow, _g.d.table_order._remark);
                    decimal __qtyCancel = (decimal)this._itemByOrder._cellGet(editrow, _g.d.table_order._qty_cancel);
                    decimal __qtyBalance = (decimal)this._itemByOrder._cellGet(editrow, _g.d.table_order._qty_balance);

                    _detailEditForm = new _foodDetailForm(__productName, __qty, __price, __suggest_remark);

                    if (_g.g._companyProfile._warning_price_3)
                    {
                        // check can edit price
                        _detailEditForm._priceTextBox.Enabled = _can_edit_price_user;

                    }
                    _detailEditForm._deleteButton.Visible = false;
                    //_detailEditForm._deleteButton.Click += (_deleteButtonSender, _deleteButtonArgs) =>
                    //{
                    //    if (MessageBox.Show("ยืนยันการลบรายการ", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //    {
                    //        // delete row
                    //        this._itemByOrder._rowData.RemoveAt(editrow);
                    //        this._itemByOrder.Invalidate();
                    //        _detailEditForm.Close();
                    //        _detailEditForm.Dispose();
                    //    }
                    //};
                    _detailEditForm.ShowDialog(MyLib._myGlobal._mainForm);
                    if (_detailEditForm.DialogResult == DialogResult.OK)
                    {
                        // update change detail
                        decimal __qtyEdit = (decimal)MyLib._myGlobal._decimalPhase(_detailEditForm._qtyTextBox.Text);

                        __qtyBalance = __qtyEdit - __qtyCancel;

                        this._itemByOrder._cellUpdate(editrow, _g.d.table_order._qty, MyLib._myGlobal._decimalPhase(_detailEditForm._qtyTextBox.Text), false);
                        this._itemByOrder._cellUpdate(editrow, _g.d.table_order._qty_balance, __qtyBalance, false);
                        this._itemByOrder._cellUpdate(editrow, _g.d.table_order._price, MyLib._myGlobal._decimalPhase(_detailEditForm._priceTextBox.Text), false);
                        this._itemByOrder._cellUpdate(editrow, _g.d.table_order._sum_amount, MyLib._myGlobal._decimalPhase(_detailEditForm._priceTextBox.Text) * MyLib._myGlobal._decimalPhase(_detailEditForm._qtyTextBox.Text), false);
                        this._itemByOrder._cellUpdate(editrow, _g.d.table_order._remark, _detailEditForm._remarkTextBox.Text, false);
                        //this._calc();

                        //decimal __total_balance = 
                    }
                    // gen dialog and save detail to grid
                }
            }
        }

        void _orderList__afterSelectRow(object sender, int row)
        {
            this._loadData(row, false);
        }

        //void _buildView()
        //{
        //    this._orderDetailPanel.Controls.Clear();
        //    switch (this._viewType)
        //    {
        //        case 0: this._itemByOrder.Dock = DockStyle.Fill;
        //            this._orderScreen.AutoSize = true;
        //            this._orderScreen.Dock = DockStyle.Top;
        //            this._orderDetailPanel.Controls.Add(this._itemByOrder);
        //            this._orderDetailPanel.Controls.Add(this._orderScreen);
        //            break;
        //        case 1:
        //            this._itemByAll.Dock = DockStyle.Fill;
        //            this._orderDetailPanel.Controls.Add(this._itemByAll);
        //            break;
        //    }
        //}

        void _loadData(int row, Boolean all)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order_doc._doc_no, _g.d.table_order_doc._doc_date, _g.d.table_order_doc._doc_time, _g.d.table_order_doc._sale_code, _g.d.table_order_doc._qty, _g.d.table_order_doc._sum_amount) + " from " + _g.d.table_order_doc._table + " where " + _g.d.table_order_doc._doc_no + " in (select " + _g.d.table_order._doc_no + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + this._table._tableNumber + "\' and last_status in (0,1) )")); // โต๋ เพิ่ม where trans_guid เพื่อดึงสถานะ โต๊ะ ล่าสุด, โต๋เอา and " + _g.d.table_order._trans_guid + "=\'" + this._table._transGuidNumber + "\' ออก
            if (_g.g._companyProfile._warning_price_3)
            {
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._price_level_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._saleCode + "\'"));
            }
            __myquery.Append("</node>");
            string __debug_query = __myquery.ToString();

            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._orderList._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
            if (all)
            {
                this._loadOrder(1, this._tableNumber, this._table._transGuidNumber, "");
            }
            this._loadOrder(0, this._tableNumber, this._table._transGuidNumber, (this._orderList._rowData.Count == 0) ? "" : this._orderList._cellGet(row, _g.d.table_order_doc._doc_no).ToString());
            if (_g.g._companyProfile._warning_price_3)
            {
                if (((DataSet)__data[1]).Tables[0].Rows.Count > 0)
                {
                    _can_edit_price_user = (((DataSet)__data[1]).Tables[0].Rows[0][0].ToString().Equals("1")) ? true : false;
                }
            }
        }

        void _loadOrder(int viewType, string tableNumber, string transGuid, string docNo)
        {
            string __where = (viewType == 0) ? " and " + _g.d.table_order._doc_no + "=\'" + docNo + "\'" : "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order._guid_line
                , _g.d.table_order._doc_no
                , _g.d.table_order._item_code
                , "case when  length(" + _g.d.table_order._as_item_name + ") > 0  then " + _g.d.table_order._as_item_name + " else  (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.table_order._item_code + ") end as " + _g.d.table_order._item_name
                , _g.d.table_order._qty
                , _g.d.table_order._qty_send
                , _g.d.table_order._qty_cancel
                , _g.d.table_order._qty_balance
                , _g.d.table_order._unit_code
                , _g.d.table_order._price
                , _g.d.table_order._sum_amount
                , _g.d.table_order._remark
                , _g.d.table_order._kitchen_code
                , _g.d.table_order._qty + " as " + _g.d.table_order._old_qty
                , _g.d.table_order._price + " as " + _g.d.table_order._old_price) + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,1) " + __where + " order by " + _g.d.table_order._doc_no + "," + _g.d.table_order._line_number)); // โต๋ เพิ่ม where trans_guid เพื่อดึงสถานะ โต๊ะ ล่าสุด , โต๋ เอา and " + _g.d.table_order._trans_guid + " =\'" + transGuid + "\' ออก
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.table_order_doc._table + " where " + _g.d.table_order_doc._doc_no + "=\'" + docNo + "\' and " + _g.d.table_order_doc._order_status + " in (0,1)"));
            __myquery.Append("</node>");
            string __debug_query = __myquery.ToString();
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

            this._itemByOrder._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
            this._orderScreen._loadData(((DataSet)__data[1]).Tables[0]);

        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __row = 0; __row < this._itemByOrder._rowData.Count; __row++)
            {
                decimal __qty = (decimal)this._itemByOrder._cellGet(__row, _g.d.table_order._qty);
                decimal __qty_balance = (decimal)this._itemByOrder._cellGet(__row, _g.d.table_order._qty_balance);
                decimal __qty_cancel = (decimal)this._itemByOrder._cellGet(__row, _g.d.table_order._qty_cancel);
                decimal __sum_amount = (decimal)this._itemByOrder._cellGet(__row, _g.d.table_order._sum_amount);
                decimal __price = (decimal)this._itemByOrder._cellGet(__row, _g.d.table_order._price);
                string __itemCode = this._itemByOrder._cellGet(__row, _g.d.table_order._item_code).ToString();
                string __guid_line = this._itemByOrder._cellGet(__row, _g.d.table_order._guid_line).ToString();

                string __queryUpdateTableOrder = "update " + _g.d.table_order._table + " set " + _g.d.table_order._qty + "=" + __qty + "," + _g.d.table_order._price + "=" + __price + "," + _g.d.table_order._qty_balance + "=" + __qty_balance + "," + _g.d.table_order._qty_cancel + "=" + __qty_cancel + "," + _g.d.table_order._sum_amount + "= " + __sum_amount + " where " + _g.d.table_order._table_number + "=\'" + this._tableNumber + "\' and " + _g.d.table_order._item_code + "= \'" + __itemCode + "\' and " + _g.d.table_order._guid_line + "= \'" + __guid_line + "\'";

                decimal __oldQty = (decimal)this._itemByOrder._cellGet(__row, _g.d.table_order._old_qty);
                decimal __oldPrice = (decimal)this._itemByOrder._cellGet(__row, _g.d.table_order._old_price);

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryUpdateTableOrder));

                // เก็บ log จำนวน ราคา ก่อนแก้ และ หลังแก้ เข้าไป
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + "," + _g.d.table_order_log._sale_code + ") values (now(),\'Edit Order\',\'Table Number:" + this._tableNumber + ", OQty:" + __oldQty.ToString() + ",OPrice:" + __oldPrice + ",NQty:" + __qty + ",NPrice:" + __price + "\',\'" + this._saleCode + "\')"));

            }
            __query.Append("</node>");

            // save edit order
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, null);
                // save order cancel 
                if (_processComplete != null)
                {
                    _processComplete();
                }
            }
            else
            {
                MessageBox.Show(__result, "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // save edit log


        }

    }
}
