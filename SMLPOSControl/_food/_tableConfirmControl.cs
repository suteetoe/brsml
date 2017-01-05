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
    public partial class _tableConfirmControl : UserControl
    {
        public delegate void _tableConfirmProcessComplete();
        public event _tableConfirmProcessComplete _processComplete;

        _orderListGridControl _orderList = new _orderListGridControl();
        _itemGridControl _itemByOrder = new _itemGridControl(0, true, true);
        _itemGridControl _itemByAll = new _itemGridControl(1, true, true);
        _orderScreenControl _orderScreen = new _orderScreenControl();

        private string _saleCode = "";
        private string _saleName = "";
        private string _tableName = "";
        private string _device_id = "";

        /// <summary>
        /// 0=ตามใบสั่งอาหาร,1=แสดงทั้งหมด
        /// </summary>
        private int _viewType = 0;

        private string _tableNumber = "";
        _tableSearchLevelMenuControl _table = null;

        public _tableConfirmControl(string saleCode, string saleName, string tableNumber, string tableName, string device_id, string device_doc_format, _tableSearchLevelMenuControl __table, int barcodePriceLevel)
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
            this._orderListPanel.Controls.Add(this._orderList);
            this._orderList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            /*
            this._orderScreen.AutoSize = true;
            this._orderScreen.Dock = DockStyle.Top;
            this._itemByOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._itemByOrder._message = "Double Click=Edit";
            this._itemByOrder.Dock = DockStyle.Fill;
            //this._orderDetailPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this._orderDetailPanel.Controls.Add(this._itemByOrder);
            this._orderDetailPanel.Controls.Add(this._orderScreen);
            */

            this._buildView();
            this._loadData(0, false);
            this._orderList._afterSelectRow += _orderList__afterSelectRow;

            //
        }

        void _orderList__afterSelectRow(object sender, int row)
        {
            this._loadData(row, false);
        }

        void _buildView()
        {
            this._itemListPanel.Controls.Clear();
            switch (this._viewType)
            {
                case 0: this._itemByOrder.Dock = DockStyle.Fill;
                    this._orderScreen.AutoSize = true;
                    this._orderScreen.Dock = DockStyle.Top;
                    this._itemListPanel.Controls.Add(this._itemByOrder);
                    this._itemListPanel.Controls.Add(this._orderScreen);
                    break;
                case 1:
                    this._itemByAll.Dock = DockStyle.Fill;
                    this._itemListPanel.Controls.Add(this._itemByAll);
                    break;
            }
        }

        void _loadOrder(int viewType, string tableNumber, string transGuid, string docNo)
        {
            string __where = (viewType == 0) ? " and " + _g.d.table_order._doc_no + "=\'" + docNo + "\'" : "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order._confirm_status, _g.d.table_order._guid_line, _g.d.table_order._doc_no, _g.d.table_order._item_code, "case when  length(" + _g.d.table_order._as_item_name + ") > 0  then " + _g.d.table_order._as_item_name + " else  (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.table_order._item_code + ") end as " + _g.d.table_order._item_name, _g.d.table_order._qty, _g.d.table_order._qty_send, _g.d.table_order._qty_cancel, _g.d.table_order._qty_balance, _g.d.table_order._unit_code, _g.d.table_order._price, _g.d.table_order._sum_amount, _g.d.table_order._remark) + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,1) " + __where + " order by " + _g.d.table_order._doc_no + "," + _g.d.table_order._line_number)); // โต๋ เพิ่ม where trans_guid เพื่อดึงสถานะ โต๊ะ ล่าสุด , โต๋ เอา and " + _g.d.table_order._trans_guid + " =\'" + transGuid + "\' ออก
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.table_order_doc._table + " where " + _g.d.table_order_doc._doc_no + "=\'" + docNo + "\' and " + _g.d.table_order_doc._order_status + " in (0,1)"));
            __myquery.Append("</node>");
            string __debug_query = __myquery.ToString();
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            switch (viewType)
            {
                case 0:
                    this._itemByOrder._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
                    this._orderScreen._loadData(((DataSet)__data[1]).Tables[0]);
                    break;
                case 1: this._itemByAll._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
                    break;
            }
        }

        void _loadData(int row, Boolean all)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order_doc._doc_no, _g.d.table_order_doc._doc_date, _g.d.table_order_doc._doc_time, _g.d.table_order_doc._sale_code, _g.d.table_order_doc._qty, _g.d.table_order_doc._sum_amount) + " from " + _g.d.table_order_doc._table + " where " + _g.d.table_order_doc._doc_no + " in (select " + _g.d.table_order._doc_no + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + this._table._tableNumber + "\' and last_status in (0,1) )")); // โต๋ เพิ่ม where trans_guid เพื่อดึงสถานะ โต๊ะ ล่าสุด, โต๋เอา and " + _g.d.table_order._trans_guid + "=\'" + this._table._transGuidNumber + "\' ออก
            __myquery.Append("</node>");
            string __debug_query = __myquery.ToString();
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._orderList._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
            if (all)
            {
                this._loadOrder(1, this._tableNumber, this._table._transGuidNumber, "");
            }
            this._loadOrder(0, this._tableNumber, this._table._transGuidNumber, (this._orderList._rowData.Count == 0) ? "" : this._orderList._cellGet(row, _g.d.table_order_doc._doc_no).ToString());
        }

        private void _viewByDocButton_Click(object sender, EventArgs e)
        {
            this._viewType = 0;
            this._buildView();

        }

        private void _viewAllButton_Click(object sender, EventArgs e)
        {
            this._viewType = 1;
            this._buildView();
            this._loadData(1, true);
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            switch (this._viewType)
            {
                case 0:
                    for (int __row = 0; __row < this._itemByOrder._rowData.Count; __row++)
                    {
                        this._itemByOrder._cellUpdate(__row, _g.d.table_order._confirm_status, 1, true);
                    }
                    this._itemByOrder.Invalidate();
                    break;
                case 1:
                    for (int __row = 0; __row < this._itemByAll._rowData.Count; __row++)
                    {
                        this._itemByAll._cellUpdate(__row, _g.d.table_order._confirm_status, 1, true);
                    }
                    this._itemByAll.Invalidate();
                    break;
            }

        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            switch (this._viewType)
            {
                case 0:
                    for (int __row = 0; __row < this._itemByOrder._rowData.Count; __row++)
                    {
                        this._itemByOrder._cellUpdate(__row, _g.d.table_order._confirm_status, 0, true);
                    }
                    this._itemByOrder.Invalidate();
                    break;
                case 1:
                    for (int __row = 0; __row < this._itemByAll._rowData.Count; __row++)
                    {
                        this._itemByAll._cellUpdate(__row, _g.d.table_order._confirm_status, 0, true);
                    }
                    this._itemByAll.Invalidate();
                    break;
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            save_data();
        }

        void save_data()
        {
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            switch (this._viewType)
            {
                case 0:
                    for (int __row = 0; __row < this._itemByOrder._rowData.Count; __row++)
                    {
                        string __confirmStatus = this._itemByOrder._cellGet(__row, _g.d.table_order._confirm_status).ToString();
                        string __itemCode = this._itemByOrder._cellGet(__row, _g.d.table_order._item_code).ToString();
                        string __guid_line = this._itemByOrder._cellGet(__row, _g.d.table_order._guid_line).ToString();

                        string __queryUpdateTableOrder = "update " + _g.d.table_order._table + " set " + _g.d.table_order._confirm_status + "=" + __confirmStatus + " where " + _g.d.table_order._table_number + "=\'" + this._tableNumber + "\' and " + _g.d.table_order._item_code + "= \'" + __itemCode + "\' and " + _g.d.table_order._guid_line + "= \'" + __guid_line + "\'";

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryUpdateTableOrder));

                    }
                    break;
                case 1:
                    for (int __row = 0; __row < this._itemByAll._rowData.Count; __row++)
                    {
                        string __confirmStatus = this._itemByAll._cellGet(__row, _g.d.table_order._confirm_status).ToString();
                        string __itemCode = this._itemByAll._cellGet(__row, _g.d.table_order._item_code).ToString();
                        string __guid_line = this._itemByAll._cellGet(__row, _g.d.table_order._guid_line).ToString();

                        string __queryUpdateTableOrder = "update " + _g.d.table_order._table + " set " + _g.d.table_order._confirm_status + "=" + __confirmStatus + " where " + _g.d.table_order._table_number + "=\'" + this._tableNumber + "\' and " + _g.d.table_order._item_code + "= \'" + __itemCode + "\' and " + _g.d.table_order._guid_line + "= \'" + __guid_line + "\'";

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryUpdateTableOrder));
                    }
                    break;
            }

            __query.Append("</node>");
            // save edit order
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __debug = __query.ToString();
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

        }


    }

}
