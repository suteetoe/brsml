using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _tableInfoControl : UserControl
    {
        _tableSearchLevelControl _tableSearch = new _tableSearchLevelControl();
        _orderListGridControl _orderList = new _orderListGridControl();
        _itemGridControl _itemByOrder = new _itemGridControl(0, true);
        _itemGridControl _itemByAll = new _itemGridControl(1, true);
        _orderScreenControl _orderScreen = new _orderScreenControl();
        /// <summary>
        /// 0=ตามใบสั่งอาหาร,1=แสดงทั้งหมด
        /// </summary>
        private int _viewType = 1;
        private string _tableNumber = "";
        public _tableSearchLevelMenuControl _table = null;

        public Boolean _print_from_center = false;


        public _tableInfoControl()
        {
            InitializeComponent();
            this._tableSearch.Dock = DockStyle.Fill;
            this._tableLevelPanel.Controls.Add(this._tableSearch);
            this._orderList._total_show = true;
            this._orderList.Dock = DockStyle.Fill;
            this._orderListPanel.Controls.Add(this._orderList);
            this._itemByAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._itemByOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._orderList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._buildView();
            //
            this._tableSearch._menuTableClick += new _tableSearchLevelControl.MenuTableClick(_tableSearch__menuTableClick);
            this._orderList._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_orderList__afterSelectRow);

            this._reprintOrderButton.Enabled = false;
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

        void _orderList__afterSelectRow(object sender, int row)
        {
            this._loadData(row, false);


        }

        void _tableSearch__menuTableClick(object sender, EventArgs e)
        {
            _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)sender;
            this._table = __table;
            this._tableNumber = __table._tableNumber;
            this._loadData(0, true);
        }

        void _loadOrder(int viewType, string tableNumber, string transGuid, string docNo)
        {
            this._reprintOrderButton.Enabled = false;

            string __where = (viewType == 0) ? " and " + _g.d.table_order._doc_no + "=\'" + docNo + "\'" : "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order._confirm_status, _g.d.table_order._doc_no, _g.d.table_order._item_code, "case when  length(" + _g.d.table_order._as_item_name + ") > 0  then " + _g.d.table_order._as_item_name + " else  (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.table_order._item_code + ") end as " + _g.d.table_order._item_name, _g.d.table_order._qty, _g.d.table_order._qty_send, _g.d.table_order._qty_cancel, _g.d.table_order._qty_balance, _g.d.table_order._unit_code, _g.d.table_order._price, _g.d.table_order._sum_amount, _g.d.table_order._remark) + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,1) " + __where + " order by " + _g.d.table_order._doc_no + "," + _g.d.table_order._line_number)); // โต๋ เพิ่ม where trans_guid เพื่อดึงสถานะ โต๊ะ ล่าสุด , โต๋ เอา and " + _g.d.table_order._trans_guid + " =\'" + transGuid + "\' ออก
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.table_order_doc._table + " where " + _g.d.table_order_doc._doc_no + "=\'" + docNo + "\' and " + _g.d.table_order_doc._order_status + " in (0,1)"));
            __myquery.Append("</node>");
            string __debug_query = __myquery.ToString();
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            switch (viewType)
            {
                case 0:
                    this._itemByOrder._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
                    this._orderScreen._loadData(((DataSet)__data[1]).Tables[0]);
                    this._reprintOrderButton.Enabled = true;
                    break;
                case 1:
                    this._itemByAll._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
                    break;
            }


        }

        void _buildView()
        {
            this._itemListPanel.Controls.Clear();
            switch (this._viewType)
            {
                case 0:
                    this._itemByOrder.Dock = DockStyle.Fill;
                    this._orderScreen.AutoSize = true;
                    this._orderScreen.Dock = DockStyle.Top;
                    this._itemListPanel.Controls.Add(this._itemByOrder);
                    this._itemListPanel.Controls.Add(this._orderScreen);
                    this._reprintOrderButton.Visible = true;
                    break;
                case 1:
                    this._itemByAll.Dock = DockStyle.Fill;
                    this._itemListPanel.Controls.Add(this._itemByAll);
                    this._reprintOrderButton.Visible = false;
                    break;
            }
        }

        private void _byOrderButton_Click(object sender, EventArgs e)
        {
            this._viewType = 0;
            this._buildView();
        }

        private void _allItemButton_Click(object sender, EventArgs e)
        {
            this._viewType = 1;
            this._buildView();
        }

        private void _reprintOrderButton_Click(object sender, EventArgs e)
        {

            string __docNo = this._orderScreen._getDataStr(_g.d.table_order_doc._doc_no);
            string __docDate = MyLib._myGlobal._convertDateToQuery(this._orderScreen._getDataDate(_g.d.table_order_doc._doc_date));
            string __docTime = this._orderScreen._getDataStr(_g.d.table_order_doc._doc_time);

            // ส่งไปครัว
            if (this._print_from_center == true)
            {
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_command._table + " where " + _g.d.kitchen_command._doc_no + " = \'" + __docNo + "\' and " + _g.d.kitchen_command._doc_date + " = \'" + __docDate + "\' and " + _g.d.kitchen_command._doc_time + "='" + __docTime + "\' "));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_command._table + "(" + _g.d.kitchen_command._doc_no + "," + _g.d.kitchen_command._doc_date + "," + _g.d.kitchen_command._doc_time + ", " + _g.d.kitchen_command._doc_type + ") values (\'" + __docNo + "\',\'" + __docDate + "\',\'" + __docTime + "\', 3)"));
                __query.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__resultStr.Length == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ส่งพิมพ์รายการสั่งอาหาร เรียบร้อยแล้ว"), MyLib._myGlobal._resource("บันทึก"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(__resultStr);
                }
            }
            else
            {
                //if (warning)
                {
                    _kitchenPrintClass __printOrder = new _kitchenPrintClass();
                    __printOrder._printOrder(__docNo, __docDate, __docTime, "พิมพ์ใหม่", 0);
                    MessageBox.Show(MyLib._myGlobal._resource("ส่งพิมพ์รายการสั่งอาหาร เรียบร้อยแล้ว"), MyLib._myGlobal._resource("บันทึก"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}
