using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.Drawing.Printing;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

namespace SMLPOSControl._food
{
    public partial class _orderCancelControl : UserControl
    {
        public delegate void ScreenProcessComplete();
        public event ScreenProcessComplete _processComplete;

        private string _saleCode = "";
        private string _saleName = "";
        private string _tableNumber = "";
        private string _tableName = "";
        private string _device_id = "";
        private string _order_doc_format = "";
        private string _trans_guid = "";
        private string _formatNumberQty = _g.g._getFormatNumberStr(1);
        private string _formatNumberPrice = _g.g._getFormatNumberStr(2);
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private int _mode = 0;
        private _itemGridControl _itemGridSource = new _itemGridControl(0, true);
        private _itemCancelGridControl _itemCancelGrid = new _itemCancelGridControl();
        Boolean _printFromCenter = false;

        public _orderCancelControl(int mode, string saleCode, string saleName, string tableNumber, string tableName, string device_id, string device_doc_format, _tableSearchLevelMenuControl __table, int barcodePriceLevel, Boolean printFromCenter)
        {
            InitializeComponent();

            this._mode = mode;
            this._saleCode = saleCode;
            this._saleName = saleName;
            this._tableNumber = tableNumber;
            this._tableName = tableName;
            this._device_id = device_id;
            this._order_doc_format = device_doc_format;
            this._deviceLabel.Text = this._device_id;
            this._tableLabel.Text = this._tableNumber + "/" + this._tableName;
            this._saleLabel.Text = this._saleCode + "/" + this._saleName;
            this._trans_guid = __table._transGuidNumber;
            //
            this._itemGridSource._message = "Double Click=Cancel";
            this._itemGridSource.Dock = DockStyle.Fill;
            this._orderPanel.Controls.Clear();
            this._orderPanel.Controls.Add(this._itemGridSource);
            this._orderPanel.Controls.Add(this.toolStrip1);
            this._itemCancelGrid.Dock = DockStyle.Fill;
            this._itemCancelGrid._message = "Double Click=Remove";
            this._cancelPanel.Controls.Add(this._itemCancelGrid);
            this._printFromCenter = printFromCenter;

            //
            this._itemGridSource._mouseDoubleClick += (s1, e1) =>
            {
                if (e1._row < this._itemGridSource._rowData.Count)
                {
                    string __itemCode = this._itemGridSource._cellGet(e1._row, _g.d.table_order._item_code).ToString();
                    string __itemName = this._itemGridSource._cellGet(e1._row, _g.d.table_order._item_name).ToString();
                    string __barcode = this._itemGridSource._cellGet(e1._row, _g.d.table_order._barcode).ToString();
                    string __unit_code = this._itemGridSource._cellGet(e1._row, _g.d.table_order._unit_code).ToString();
                    int __qty_order = (int)MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(e1._row, _g.d.table_order._qty).ToString());
                    int __qty_cancel = (int)MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(e1._row, _g.d.table_order._qty_cancel).ToString());
                    int __qty_balance = (int)MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(e1._row, _g.d.table_order._qty_balance).ToString());
                    string __guidLine = this._itemGridSource._cellGet(e1._row, _g.d.table_order._guid_line).ToString();
                    decimal __item_price = (decimal)this._itemGridSource._cellGet(e1._row, _g.d.table_order._price);
                    string __kitchenCode = this._itemGridSource._cellGet(e1._row, _g.d.table_order._kitchen_code).ToString();

                    int __checker_print = (int)this._itemGridSource._cellGet(e1._row, _g.d.ic_inventory._barcode_checker_print);
                    int __print_per_unit = (int)this._itemGridSource._cellGet(e1._row, _g.d.ic_inventory._print_order_per_unit);

                    if (__qty_balance <= 0)
                    {
                        MessageBox.Show("รายการอาหารนี้ถูกยกเลิกไปหมดแล้ว", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _orderCancelForm __form = new _orderCancelForm(__table, __itemCode, __itemName, __unit_code, __qty_order, __qty_cancel, __qty_balance);

                    // หลังจาก save จำนวนต้องการยกเลิก แล้ว นำไป update รายวันโต๊ะ (คำณวนยอดคงเหลือ สั่งด้วยแล้ว)
                    __form._okButton.Click += (okSender, okEventArgs) =>
                    {
                        decimal __new_cancel_qty = __form._screen._getDataNumber(_g.d.table_order_cancel._qty);
                        decimal __canceled_qty = __form._screen._getDataNumber(_g.d.table_order_cancel._qty_cancel);
                        decimal __new_qty_balance = __form._screen._getDataNumber(_g.d.table_order_cancel._qty_balance);

                        decimal __total_cancel = __canceled_qty + __new_cancel_qty;
                        if (__qty_order < __total_cancel)
                        {
                            // เช็คคืนเกิน หรือ คืน
                            MessageBox.Show("ป้อนจำนวนยกเลิกผิด", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // เช็คก่อนว่าแก้ไขไปหรือเปล่า ถ้าแก้ไข เปลี่ยนสถานะสำหรับ update ด้วย
                        if (!__qty_balance.Equals(__new_qty_balance))
                        {
                            this._itemGridSource._cellUpdateChangeStatus(e1._row, true);

                            // update source grid
                            this._itemGridSource._cellUpdate(e1._row, _g.d.table_order._qty_cancel, __total_cancel, true);
                            //this._itemGridSource._cellUpdate(e1._row, _g.d.table_order._qty_balance, __new_qty_balance, false);
                            //this._itemGridSource._cellUpdate(e1._row, _g.d.table_order._sum_amount, (__item_price * __new_qty_balance), false);

                            // update cancel grid

                            // search grid (where guid_line = source)
                            bool __found = false;
                            int __foundRow = -1;
                            for (int __i = 0; __i < this._itemCancelGrid._rowData.Count; __i++)
                            {
                                if (this._itemCancelGrid._cellGet(__i, _g.d.table_order_cancel._guid_line).ToString().Equals(__guidLine))
                                {
                                    __found = true;
                                    __foundRow = __i;
                                }
                            }

                            if (__found == false)
                            {
                                __foundRow = this._itemCancelGrid._addRow();
                            }

                            if (__foundRow != -1)
                            {
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._item_code, __itemCode, false);
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._item_name, __itemName, false);
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._unit_code, __unit_code, false);
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._qty, __new_cancel_qty, false);
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._price, __item_price, false);
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._guid_line, __guidLine, false);
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order._kitchen_code, __kitchenCode, false);

                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.ic_inventory._barcode_checker_print, __checker_print, false);
                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.ic_inventory._print_order_per_unit, __print_per_unit, false);

                                decimal __total_cancel_amount = __new_cancel_qty * __item_price;

                                this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._sum_amount, __total_cancel_amount, false);
                            }
                        }

                        __form.Close();
                    };
                    __form.ShowDialog();
                }

            };

            this._itemGridSource._alterCellUpdate += new AfterCellUpdateEventHandler(_itemGridSource__alterCellUpdate);

            this._itemCancelGrid._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_itemCancelGrid__mouseDoubleClick);

            //
            this._loadData(tableNumber);
        }

        void _itemGridSource__alterCellUpdate(object sender, int row, int column)
        {
            // ยกเลิกรายการ
            if (column == this._itemGridSource._findColumnByName(_g.d.table_order._qty_cancel))
            {
                decimal __qty_cancel = (decimal)this._itemGridSource._cellGet(row, _g.d.table_order._qty_cancel);
                decimal __price = (decimal)this._itemGridSource._cellGet(row, _g.d.table_order._price);
                decimal __order_qty = (decimal)this._itemGridSource._cellGet(row, _g.d.table_order._qty);


                this._itemGridSource._cellUpdate(row, _g.d.table_order._qty_balance, __order_qty - __qty_cancel, false);
                this._itemGridSource._cellUpdate(row, _g.d.table_order._sum_amount, (__price * (__order_qty - __qty_cancel)), false);

            }
        }

        void _itemCancelGrid__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e1)
        {
            // remove cancel item
            if (e1._row < this._itemCancelGrid._rowData.Count)
            {
                string __itemCode = this._itemCancelGrid._cellGet(e1._row, _g.d.table_order_cancel._item_code).ToString();
                //string __itemName = this._itemCancelGrid._cellGet(e1._row, _g.d.table_order_cancel._item_name).ToString();
                string __unit_code = this._itemCancelGrid._cellGet(e1._row, _g.d.table_order_cancel._unit_code).ToString();
                decimal __qty = (decimal)MyLib._myGlobal._decimalPhase(this._itemCancelGrid._cellGet(e1._row, _g.d.table_order_cancel._qty).ToString());
                string __guidLine = this._itemCancelGrid._cellGet(e1._row, _g.d.table_order_cancel._guid_line).ToString();


                // find row in item source table and update return cancel value
                int __foundAddr = -1;

                for (int __row = 0; __row < this._itemGridSource._rowData.Count; __row++)
                {
                    if (this._itemGridSource._cellGet(__row, _g.d.table_order._guid_line).ToString().Equals(__guidLine))
                    {
                        __foundAddr = __row;
                        break;
                    }
                }

                if (__foundAddr != -1)
                {
                    decimal __item_price = (decimal)this._itemGridSource._cellGet(__foundAddr, _g.d.table_order._price);
                    decimal __blance_qty = (decimal)this._itemGridSource._cellGet(__foundAddr, _g.d.table_order._qty_balance);
                    decimal __cancel_qty = (decimal)this._itemGridSource._cellGet(__foundAddr, _g.d.table_order._qty_cancel);

                    this._itemGridSource._cellUpdate(__foundAddr, _g.d.table_order._qty_cancel, __cancel_qty - __qty, true);
                }

                this._itemCancelGrid._rowData.RemoveAt(e1._row);
                //this._itemCancelGrid.Invalidate();
            }
        }

        private void _loadData(string tableNumber)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order._doc_no, _g.d.table_order._item_code,
                "case when  length(" + _g.d.table_order._as_item_name + ") > 0  then " + _g.d.table_order._as_item_name + " else  (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.table_order._item_code + ") end as " + _g.d.table_order._item_name,
                _g.d.table_order._as_item_name, _g.d.table_order._qty, _g.d.table_order._qty_send, _g.d.table_order._qty_cancel, _g.d.table_order._qty_balance, _g.d.table_order._unit_code, _g.d.table_order._price, _g.d.table_order._sum_amount, _g.d.table_order._remark, _g.d.table_order._barcode, _g.d.table_order._guid_line, _g.d.table_order._kitchen_code) +
                ", coalesce((select " + _g.d.ic_inventory._barcode_checker_print + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + "), 0) as " + _g.d.ic_inventory._barcode_checker_print +
                ", coalesce((select " + _g.d.ic_inventory._print_order_per_unit + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + "), 0) as " + _g.d.ic_inventory._print_order_per_unit +
                " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,1) order by " + _g.d.table_order._doc_no + "," + _g.d.table_order._line_number)); // โต๋ เพิ่ม where trans_guid เพื่อดึงสถานะ โต๊ะ ล่าสุด , โต๋ เอา and " + _g.d.table_order._trans_guid + " =\'" + transGuid + "\' ออก
            __myquery.Append("</node>");
            string __debug_query = __myquery.ToString();
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._itemGridSource._loadFromDataTable(((DataSet)__data[0]).Tables[0]);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_daa();
        }

        void _save_daa()
        {
            //MessageBox.Show("Save Success !!");
            string __docDate = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            string __docTime = DateTime.Now.ToString("HH:mm");
            string __cancelGuid = Guid.NewGuid().ToString();

            // update order table 
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            //__query.Append(_itemGridSource._createQueryForUpdate(_g.d.table_order._table, ""));
            for (int __row = 0; __row < this._itemGridSource._rowData.Count; __row++)
            {
                decimal __qty_cancel = (decimal)this._itemGridSource._cellGet(__row, _g.d.table_order._qty_cancel);
                decimal __qty_balance = (decimal)this._itemGridSource._cellGet(__row, _g.d.table_order._qty_balance);
                decimal __sum_amount = (decimal)this._itemGridSource._cellGet(__row, _g.d.table_order._sum_amount);

                string __itemCode = this._itemGridSource._cellGet(__row, _g.d.table_order._item_code).ToString();
                string __guid_line = this._itemGridSource._cellGet(__row, _g.d.table_order._guid_line).ToString();

                string __queryUpdateTableOrder = "update " + _g.d.table_order._table + " set " + _g.d.table_order._qty_cancel + "=" + __qty_cancel + ", " + _g.d.table_order._qty_balance + "=" + __qty_balance + "," + _g.d.table_order._sum_amount + "= " + __sum_amount + " where " + _g.d.table_order._table_number + "=\'" + this._tableNumber + "\' and " + _g.d.table_order._item_code + "= \'" + __itemCode + "\' and " + _g.d.table_order._guid_line + "= \'" + __guid_line + "\'";

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryUpdateTableOrder));
            }


            this._itemCancelGrid._updateRowIsChangeAll(true);

            string __masterOrderCancelField = _g.d.table_order_cancel._table_number + "," + _g.d.table_order_cancel._cancel_guid + "," + _g.d.table_order_cancel._doc_date + "," + _g.d.table_order_cancel._doc_time + "," + _g.d.table_order_cancel._sale_code + ",";
            string __masterOrderCancelValue = "\'" + this._tableNumber + "\',\'" + __cancelGuid + "\',\'" + __docDate + "\', \'" + __docTime + "\',\'" + this._saleCode + "\',";

            __query.Append(this._itemCancelGrid._createQueryForInsert(_g.d.table_order_cancel._table, __masterOrderCancelField, __masterOrderCancelValue));
            // insert all row in cancel table

            // toe log
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + "," + _g.d.table_order_log._sale_code + ") values (now(),\'Cancel Order\',\'Table Number : " + this._tableNumber + "\',\'" + this._saleCode + "\')"));
            __query.Append("</node>");

            string __debugQuery = __query.ToString();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Length == 0)
            {
                // save order cancel 
                if (this._printFromCenter == true)
                {
                    __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_command._table + "(" + _g.d.kitchen_command._doc_no + "," + _g.d.kitchen_command._doc_date + "," + _g.d.kitchen_command._doc_time + "," + _g.d.kitchen_command._doc_type + ") values (\'" + __cancelGuid + "\',\'" + __docDate + "\',\'" + __docTime + "\', 1)"));
                    __query.Append("</node>");

                    __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                    if (__result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                    }
                    else
                    {
                        MessageBox.Show(__result);
                    }

                }
                else
                {
                    SMLPOSControl._kitchenPrintClass __printer = new _kitchenPrintClass();
                    __printer._printCancelOrder(__cancelGuid, __docDate, __docTime);

                    MyLib._myGlobal._displayWarning(1, null);

                }

                if (_g.g._companyProfile._use_order_checker == true)
                {
                    // ตัดรายการ cheecker ออกไป
                    // scan guid line

                    StringBuilder __deleteCheckOrder = new StringBuilder();

                    for (int __row = 0; __row < this._itemCancelGrid._rowData.Count; __row++)
                    {
                        string __getGuid_Line = this._itemCancelGrid._cellGet(__row, _g.d.table_order_cancel._guid_line).ToString();
                        decimal __cancel_qty = (decimal)this._itemCancelGrid._cellGet(__row, _g.d.table_order_cancel._qty);
                        bool __checker_print = this._itemCancelGrid._cellGet(__row, _g.d.ic_inventory._barcode_checker_print).ToString().Equals("1") ? true : false;
                        bool __print_per_unit = this._itemCancelGrid._cellGet(__row, _g.d.ic_inventory._print_order_per_unit).ToString().Equals("1") ? true : false;

                        if (__checker_print == true)
                        {
                            DataTable __checkerData = __myFrameWork._queryShort("select roworder, " + _g.d.order_checker._order_qty + " from " + _g.d.order_checker._table + " where " + _g.d.order_checker._guid_line + "=\'" + __getGuid_Line + "\' order by " + _g.d.order_checker._confirm_status).Tables[0];

                            if (__print_per_unit == true)
                            {
                                for (int __line = 0; __line < __cancel_qty; __line++)
                                {
                                    __deleteCheckOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.order_checker._table + " where roworder=\'" + __checkerData.Rows[__line][0].ToString() + "\'"));
                                }
                            }
                            else
                            {
                                if (__checkerData.Rows.Count > 0)
                                {
                                    if (__cancel_qty == MyLib._myGlobal._decimalPhase(__checkerData.Rows[0][_g.d.order_checker._order_qty].ToString()))
                                    {
                                        // delete
                                        __deleteCheckOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.order_checker._table + " where roworder=\'" + __checkerData.Rows[0][0].ToString() + "\'"));
                                    }
                                    else
                                    {
                                        // update
                                        __deleteCheckOrder.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.order_checker._table + " set " + _g.d.order_checker._order_qty + " = " + _g.d.order_checker._order_qty + "-" + __cancel_qty + " where roworder=\'" + __checkerData.Rows[0][0].ToString() + "\'"));

                                    }
                                }
                            }
                        }
                    }

                    if (__deleteCheckOrder.Length > 0)
                    {
                        StringBuilder __deleteChecker = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                        __deleteChecker.Append(__deleteCheckOrder.ToString());
                        __deleteChecker.Append("</node>");

                        __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __deleteChecker.ToString());
                        if (__result.Length > 0)
                        {
                            MessageBox.Show(__result);
                        }
                    }
                }

                // ส่งไปพิมพ์ที่ครัว
                /*

                StringBuilder _myQuery = new StringBuilder();
                _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_master._table + " where " + _g.d.kitchen_master._print_to_kitchen + "=1 order by " + _g.d.kitchen_master._code));
                _myQuery.Append("</node>");
                ArrayList __kitchenResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());
                DataTable __kitchen = ((DataSet)__kitchenResult[0]).Tables[0];

                //
                Font __headerFont = new Font("Tahoma", 12f, FontStyle.Bold);
                Font __subHeaderFont = new Font("Tahoma", 10f, FontStyle.Regular);
                Font __detailFont = new Font("Tahoma", 12f, FontStyle.Bold);
                for (int __kitchenLoop = 0; __kitchenLoop < __kitchen.Rows.Count; __kitchenLoop++)
                {
                    List<MyLib._printRaw> __printRaw = new List<MyLib._printRaw>();
                    string __kitchenCode = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._code].ToString();
                    string __kitchenName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._name_1].ToString();
                    int __printerMode = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_mode].ToString());
                    string __printerName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_name].ToString();
                    int __printSum = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_summery].ToString());
                    Boolean __printHead = false;
                    int __countLine = 1;
                    
                    for (int __row = 0; __row < this._itemCancelGrid._rowData.Count; __row++)
                    {
                        string __itemCode = this._itemCancelGrid._cellGet(__row, _g.d.table_order_cancel._item_code).ToString().Trim();
                        string __kitchenCode2 = this._itemCancelGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();
                        if (__itemCode.Length > 0 && __kitchenCode.Length > 0)
                        {
                            if (__kitchenCode.Equals(__kitchenCode2))
                            {
                                if (__printHead == false)
                                {
                                    __printHead = true;
                                    __printRaw.Add(new MyLib._printRaw(0, 20000));
                                    MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                    __source._height = 0;
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "ใบยกเลิก" + " : " + __kitchenName + " (" + __kitchenCode + ")", __headerFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "โต๊ะ" + " : " + this._tableNumber, __headerFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, this._saleCode + " : " + this._saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __docDate + " : " + __docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                                }
                                {
                                    // รายการอาหาร/เครื่องดื่ม
                                    MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                    string __itemName = this._itemCancelGrid._cellGet(__row, _g.d.table_order_cancel._item_name).ToString();
                                    string __itemUnit = this._itemCancelGrid._cellGet(__row, _g.d.table_order_cancel._unit_code).ToString();
                                    decimal __itemQty = MyLib._myGlobal._decimalPhase(this._itemCancelGrid._cellGet(__row, _g.d.table_order_cancel._qty).ToString());
                                    //string __itemRemark = this._itemCancelGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                                    StringBuilder __desc = new StringBuilder(__countLine.ToString() + "." + __itemName + " " + "@" + " " + __itemQty.ToString("N0") + " " + __itemUnit);
                                    //if (__itemRemark.Length > 0)
                                    //{
                                    //    __desc.Append(" ");
                                    //    __desc.Append(__itemRemark);
                                    //}
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                    __countLine++;
                                }
                                //
                                if (__printSum == 0)
                                {
                                    __printHead = false;
                                    __countLine = 1;
                                }
                            }
                        }
                    }

                    // Print
                    for (int __loop = 0; __loop < __printRaw.Count; __loop++)
                    {
                        switch (__printerMode)
                        {
                            case 0:
                                {
                                    try
                                    {
                                        PrintDocument __pd = new PrintDocument();
                                        __pd.PrinterSettings.PrinterName = __printerName;
                                        __pd.PrintPage += (s1, e1) =>
                                        {
                                            e1.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                            e1.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                            e1.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                            e1.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                            e1.Graphics.PageUnit = GraphicsUnit.Pixel;
                                            e1.Graphics.DrawImageUnscaled(__printRaw[__loop]._bitmap, 0, 0);
                                        };
                                        __pd.Print();
                                    }
                                    catch (Exception __ex)
                                    {
                                        MessageBox.Show(__ex.Message);
                                    }
                                }
                                break;
                            case 1:
                                {
                                    try
                                    {
                                        __printRaw[__loop]._print(__printerName);
                                        __printRaw[__loop]._print(__printerName, new byte[] { 29, 86, 66, 10 });
                                    }
                                    catch (Exception __ex)
                                    {
                                        MessageBox.Show(__ex.Message);
                                    }
                                }
                                break;
                            case 2:
                                {
                                    IPEndPoint __ip = new IPEndPoint(IPAddress.Parse(__printerName), 9100);
                                    Socket __socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                    __socket.Connect(__ip);
                                    __socket.Send(new byte[] { 27, 64 }); // Initialize printer
                                    __socket.Send(__printRaw[__loop]._getDataBytes());
                                    __socket.Send(new byte[] { 10 });
                                    __socket.Send(new byte[] { 29, 86, 66, 10 }); // Cut
                                    __socket.Shutdown(SocketShutdown.Both);
                                    __socket.Close();
                                }
                                break;
                        }
                    }
                }
                */

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

        float _printSlipText(Graphics g, float paperWidth, float textWidth, float x, float y, string text, Font textFont, _printSlipTextStyle style, Boolean warpString)
        {
            float __x = x;
            float __sumHeight = 0;
            string[] __text = text.Trim().Split('\n');
            for (int __loop = 0; __loop < __text.Length; __loop++)
            {
                ArrayList __getText = new ArrayList();
                if (warpString)
                {
                    __getText = MyLib._myUtil._cutString(g, __text[__loop], textFont, textWidth);
                }
                else
                {
                    __getText.Add(__text[__loop]);
                }
                for (int __loop2 = 0; __loop2 < __getText.Count; __loop2++)
                {
                    SizeF __size = g.MeasureString(__getText[__loop2].ToString(), textFont);
                    switch (style)
                    {
                        case _printSlipTextStyle.Center:
                            __x = (paperWidth - __size.Width) / 2;
                            break;
                        case _printSlipTextStyle.Right:
                            __x = paperWidth - __size.Width;
                            break;
                    }
                    g.DrawString(__getText[__loop2].ToString(), textFont, Brushes.Black, __x, y + __sumHeight);
                    __sumHeight += (__size.Height * 0.9f);
                }
            }
            return __sumHeight;
        }

        private void _cancelAllOrder_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการยกเลิกรายการทั้งหมดหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                for (int row = 0; row < this._itemGridSource._rowData.Count; row++)
                {
                    string __itemCode = this._itemGridSource._cellGet(row, _g.d.table_order._item_code).ToString();
                    string __itemName = this._itemGridSource._cellGet(row, _g.d.table_order._item_name).ToString();
                    string __barcode = this._itemGridSource._cellGet(row, _g.d.table_order._barcode).ToString();
                    string __unit_code = this._itemGridSource._cellGet(row, _g.d.table_order._unit_code).ToString();
                    int __qty_order = (int)MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(row, _g.d.table_order._qty).ToString());
                    int __qty_cancel = (int)MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(row, _g.d.table_order._qty_cancel).ToString());
                    decimal __qty_balance = MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(row, _g.d.table_order._qty_balance).ToString());
                    string __guidLine = this._itemGridSource._cellGet(row, _g.d.table_order._guid_line).ToString();
                    decimal __item_price = (decimal)this._itemGridSource._cellGet(row, _g.d.table_order._price);
                    string __kitchenCode = this._itemGridSource._cellGet(row, _g.d.table_order._kitchen_code).ToString();

                    int __checker_print = (int)this._itemGridSource._cellGet(row, _g.d.ic_inventory._barcode_checker_print);
                    int __print_per_unit = (int)this._itemGridSource._cellGet(row, _g.d.ic_inventory._print_order_per_unit);


                    this._itemGridSource._cellUpdateChangeStatus(row, true);

                    // update source grid
                    this._itemGridSource._cellUpdate(row, _g.d.table_order._qty_cancel, __qty_balance, true);
                    //this._itemGridSource._cellUpdate(e1._row, _g.d.table_order._qty_balance, __new_qty_balance, false);
                    //this._itemGridSource._cellUpdate(e1._row, _g.d.table_order._sum_amount, (__item_price * __new_qty_balance), false);

                    // update cancel grid

                    // search grid (where guid_line = source)
                    bool __found = false;
                    int __foundRow = -1;
                    for (int __i = 0; __i < this._itemCancelGrid._rowData.Count; __i++)
                    {
                        if (this._itemCancelGrid._cellGet(__i, _g.d.table_order_cancel._guid_line).ToString().Equals(__guidLine))
                        {
                            __found = true;
                            __foundRow = __i;
                        }
                    }

                    if (__found == false)
                    {
                        __foundRow = this._itemCancelGrid._addRow();
                    }

                    if (__foundRow != -1)
                    {
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._item_code, __itemCode, false);
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._item_name, __itemName, false);
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._unit_code, __unit_code, false);
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._qty, __qty_balance, false);
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._price, __item_price, false);
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._guid_line, __guidLine, false);
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order._kitchen_code, __kitchenCode, false);

                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.ic_inventory._barcode_checker_print, __checker_print, false);
                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.ic_inventory._print_order_per_unit, __print_per_unit, false);

                        decimal __total_cancel_amount = __qty_balance * __item_price;

                        this._itemCancelGrid._cellUpdate(__foundRow, _g.d.table_order_cancel._sum_amount, __total_cancel_amount, false);
                    }

                }

                this._itemGridSource.Invalidate();
            }
        }

    }
}
