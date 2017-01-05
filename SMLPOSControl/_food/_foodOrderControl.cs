using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SMLPOSControl._food
{
    public partial class _foodOrderControl : UserControl
    {
        public delegate void ScreenProcessComplete();
        public event ScreenProcessComplete _processComplete;
        public event _afterCloseTable _afterCloseTableForPay;

        public _itemGridControl _itemGrid = new _itemGridControl(0, false);
        private _itemGridControl _itemOrderList = new _itemGridControl(2, false);

        public bool _isForClseTable = false;

        private SMLInventoryControl._itemSearchLevelControl _itemSearchLevel = new SMLInventoryControl._itemSearchLevelControl();
        private _orderScreenControl _orderScreen = new _orderScreenControl();
        private string _saleCode = "";
        private string _saleName = "";
        private string _tableNumber = "";
        private string _tableName = "";
        private string _device_id = "";
        private _orderSuggestRemarkForm _suggestForm = null;
        private _foodDetailForm _detailEditForm = null;
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private string _order_doc_format = "";
        private string _trans_guid = "";
        private string _formatNumberQty = _g.g._getFormatNumberStr(1);
        private string _formatNumberPrice = _g.g._getFormatNumberStr(2);
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private SMLInventoryControl._icTransItemGridChangeNameForm _changeItemNameForm;
        private Boolean _print_from_center = false;
        int _mode;

        private Boolean _can_edit_price_user = false;

        public _foodOrderControl(int mode, string saleCode, string saleName, string tableNumber, string tableName, string device_id, string device_doc_format, string tableTransGuidNumber, int barcodePriceLevel, Boolean printFromCenter)
        {
            InitializeComponent();
            this._saleCode = saleCode;
            this._saleName = saleName;
            this._tableNumber = tableNumber;
            this._tableName = tableName;
            this._device_id = device_id;
            this._order_doc_format = device_doc_format;
            this._deviceLabel.Text = this._device_id;
            this._tableLabel.Text = this._tableNumber + "/" + this._tableName;
            this._saleLabel.Text = this._saleCode + "/" + this._saleName;
            this._itemSearchLevel.Dock = DockStyle.Fill;
            this._trans_guid = tableTransGuidNumber;
            this._print_from_center = printFromCenter;
            this._mode = mode;
            this._itemSearchLevel._barcode_price_level = barcodePriceLevel;
            if (mode == 0 || this._mode == 2)
            {
                this._itemLevelPanel.Controls.Add(this._itemSearchLevel);
                this._itemSearchLevel._menuItemClick += new SMLInventoryControl.MenuItemClick(_itemSearchLevel__menuItemClick);
            }
            else
            {
                this._itemOrderList.Dock = DockStyle.Fill;
                this._itemLevelPanel.Controls.Add(this._itemOrderList);
                this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical;
                this.splitContainer1.SplitterDistance = 600;

                Label __label = new Label();
                __label.Text = "รายการอาหารในโต๊ะ";
                __label.Dock = DockStyle.Top;
                this._itemLevelPanel.Controls.Add(__label);

                //this._saveButton.Visible = false;
                this._productBasketButton.Visible = false;
                this._searchArButton.Visible = false;
                this._saveAndCloseTableButton.Visible = true;
                this.toolStrip1.Visible = true;

            }
            //
            this._itemGrid.Dock = DockStyle.Fill;
            this._gridPanel.Controls.Add(this._itemGrid);
            this._itemGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_itemGrid__afterCalcTotal);
            this._itemGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_itemGrid__alterCellUpdate);
            //
            this._orderScreen.Dock = DockStyle.Fill;
            this._screenPanel.Controls.Add(this._orderScreen);
            //
            this._reset();
            this._itemSearchLevel._deleteLastItemButton.Click += new EventHandler(_deleteLastItemButton_Click);
            this._itemSearchLevel._renewItemButton.Click += new EventHandler(_renewItemButton_Click);
            this._itemGrid._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_itemGrid__mouseDoubleClick);

            // rename item
            this._itemGrid._columnExtraWord(_g.d.table_order._item_name, "(F10)");
            this._itemGrid._keyDown += new MyLib.KeyDownEventHandler(_itemGrid__keyDown);
            this._selectTableButton.Click += _selectTableButton_Click;
            if (_mode == 2)
            {
                this._selectTableButton.Visible = true;
                this.toolStrip1.Visible = true;
            }

            if (_g.g._companyProfile._warning_price_3)
            {
                // check user can edit prict
                DataTable __userEditPriceTable = _myFrameWork._queryShort("select " + _g.d.erp_user._price_level_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._saleCode + "\'").Tables[0];
                if (__userEditPriceTable.Rows.Count > 0)
                {
                    _can_edit_price_user = (__userEditPriceTable.Rows[0][0].ToString().Equals("1")) ? true : false;
                }
            }
        }
        _tableSearchLevelControl _tableSelect = null;

        void _selectTableButton_Click(object sender, EventArgs e)
        {
            // เลือกโต๊ะ
            if (_tableSelect == null)
            {
                _tableSelect = new _tableSearchLevelControl();
            }

            Form __selectTableForm = new Form();
            _tableSelect.Dock = DockStyle.Fill;
            _tableSelect._menuTableClick += (s1, e1) =>
            {
                _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;

                // เช็คสถานะโต๊ะ ด้วย

                this._orderScreen._setDataStr(_g.d.table_order_doc._table_number, __table._tableNumber);
                this._tableNumber = __table._tableNumber;
                __selectTableForm.Close();

            };
            __selectTableForm.Controls.Add(_tableSelect);
            __selectTableForm.WindowState = FormWindowState.Maximized;
            __selectTableForm.ShowDialog();

        }

        public void _loadControl(string saleCode, string saleName, string tableNumber, string tableName, string device_id, string device_doc_format, string tableTransGuidNumber, int barcodePriceLevel)
        {
            this._itemGrid._clear();
            this._orderScreen._clear();
            this._reset();

            this._saleCode = saleCode;
            this._saleName = saleName;
            this._tableNumber = tableNumber;
            this._tableName = tableName;
            this._device_id = device_id;
            this._order_doc_format = device_doc_format;
            this._deviceLabel.Text = this._device_id;
            this._tableLabel.Text = this._tableNumber + "/" + this._tableName;
            this._saleLabel.Text = this._saleCode + "/" + this._saleName;
            this._itemSearchLevel.Dock = DockStyle.Fill;
            this._trans_guid = tableTransGuidNumber;

            this._itemSearchLevel._barcode_price_level = barcodePriceLevel;


            if (this._mode == 1 && this._tableNumber.Length > 0)
            {
                // load order intable
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                // หา order ในโต๊ะ
                string __tableOrderQuery = "select * " +
                    ", (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ") as " + _g.d.table_order._item_name +
                    ", (select coalesce(" + _g.d.ic_inventory._drink_type + ", 0) from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ") as " + _g.d.ic_inventory._drink_type +
                    " from " + _g.d.table_order._table +
                    " where " + _g.d.table_order._table_number + " =\'" + this._tableNumber + "\'" + " and " + _g.d.table_order._last_status + " in (0,1) ";// + 
                //" order by (select coalesce(" + _g.d.ic_inventory._drink_type + ", 0) from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ")";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__tableOrderQuery));
                if (_g.g._companyProfile._warning_price_3)
                {
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._price_level_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._saleCode + "\'"));
                }
                __query.Append("</node>");

                ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                DataTable __dOrderList = ((DataSet)__getData[0]).Tables[0];
                this._itemOrderList._loadFromDataTable(__dOrderList);

                if (_g.g._companyProfile._warning_price_3)
                {
                    if (((DataSet)__getData[1]).Tables[0].Rows.Count > 0)
                    {
                        _can_edit_price_user = (((DataSet)__getData[1]).Tables[0].Rows[0][0].ToString().Equals("1")) ? true : false;
                    }
                }
            }
        }

        void _reset()
        {
            if (this._mode == 2)
            {
                this._tableNumber = "";
            }
            this._orderScreen._setDataDate(_g.d.table_order_doc._doc_date, DateTime.Now);
            this._orderScreen._setDataStr(_g.d.table_order_doc._doc_time, DateTime.Now.ToString("HH:mm"));
            this._orderScreen._setDataStr(_g.d.table_order_doc._doc_no, this._docAutoRun());
            this._orderScreen._setDataStr(_g.d.table_order_doc._table_number, this._tableNumber);
            this._itemOrderList._clear();

        }

        bool _itemGrid__keyDown(object sender, int row, int column, Keys keyCode)
        {
            if (keyCode.Equals(Keys.F10))
            {
                // แก้ไขชื่อ
                if (row < this._itemGrid._rowData.Count)
                {
                    this._changeItemNameForm = new SMLInventoryControl._icTransItemGridChangeNameForm();
                    this._changeItemNameForm._nameTextBox.Text = this._itemGrid._cellGet(row, _g.d.table_order._item_name).ToString();
                    this._changeItemNameForm.Text = "Code : " + this._itemGrid._cellGet(row, _g.d.table_order._item_code).ToString();
                    this._changeItemNameForm._submit += (modeResult) =>
                    {
                        if (modeResult == 1)
                        {
                            string __itemName = this._changeItemNameForm._nameTextBox.Text;
                            this._itemGrid._cellUpdate(row, _g.d.table_order._item_name, __itemName, false);
                            this._itemGrid._cellUpdate(row, _g.d.table_order._as_item_name, __itemName, false);
                        }
                    };
                    this._changeItemNameForm.ShowDialog();

                }
            }
            return false;
        }

        void _calc()
        {
            int __columnQty = this._itemGrid._findColumnByName(_g.d.table_order._qty);
            int __columnAmount = this._itemGrid._findColumnByName(_g.d.table_order._sum_amount);
            MyLib._myGrid._columnType __getColumnQty = (MyLib._myGrid._columnType)this._itemGrid._columnList[__columnQty];
            MyLib._myGrid._columnType __getColumnAmount = (MyLib._myGrid._columnType)this._itemGrid._columnList[__columnAmount];
            this._orderScreen._setDataNumber(_g.d.table_order_doc._qty, __getColumnQty._total);
            this._orderScreen._setDataNumber(_g.d.table_order_doc._sum_amount, __getColumnAmount._total);
            this._orderScreen.Invalidate();
        }

        void _itemGrid__alterCellUpdate(object sender, int row, int column)
        {
            this._calc();
        }

        void _itemGrid__afterCalcTotal(object sender)
        {
            this._calc();
        }

        void _itemGrid__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (this._itemGrid._rowData.Count > e._row && e._row != -1)
            {
                int editrow = e._row;
                string __productName = (string)this._itemGrid._cellGet(editrow, _g.d.table_order._item_name);
                decimal __qty = (decimal)this._itemGrid._cellGet(editrow, _g.d.table_order._qty);
                decimal __price = (decimal)this._itemGrid._cellGet(editrow, _g.d.table_order._price);
                string __suggest_remark = (string)this._itemGrid._cellGet(editrow, _g.d.table_order._remark);

                _detailEditForm = new _foodDetailForm(__productName, __qty, __price, __suggest_remark);

                if (_g.g._companyProfile._warning_price_3)
                {
                    // check can edit price
                    //if (_can_edit_price_user == false)
                    _detailEditForm._priceTextBox.Enabled = _can_edit_price_user;

                }
                _detailEditForm._deleteButton.Click += (_deleteButtonSender, _deleteButtonArgs) =>
                {
                    if (MessageBox.Show("ยืนยันการลบรายการ", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // delete row
                        this._itemGrid._rowData.RemoveAt(editrow);
                        this._itemGrid.Invalidate();
                        _detailEditForm.Close();
                        _detailEditForm.Dispose();
                    }
                };
                _detailEditForm.ShowDialog(MyLib._myGlobal._mainForm);
                if (_detailEditForm.DialogResult == DialogResult.OK)
                {

                    // toe check price edit by user

                    // update change detail
                    this._itemGrid._cellUpdate(editrow, _g.d.table_order._qty, MyLib._myGlobal._decimalPhase(_detailEditForm._qtyTextBox.Text), false);
                    this._itemGrid._cellUpdate(editrow, _g.d.table_order._price, MyLib._myGlobal._decimalPhase(_detailEditForm._priceTextBox.Text), false);
                    this._itemGrid._cellUpdate(editrow, _g.d.table_order._sum_amount, MyLib._myGlobal._decimalPhase(_detailEditForm._priceTextBox.Text) * MyLib._myGlobal._decimalPhase(_detailEditForm._qtyTextBox.Text), false);
                    this._itemGrid._cellUpdate(editrow, _g.d.table_order._remark, _detailEditForm._remarkTextBox.Text, false);
                    this._calc();
                }
                // gen dialog and save detail to grid
            }
        }

        void _renewItemButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการเริ่มใหม่หรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this._itemGrid._clear();
                this._itemGrid._calcTotal(true);
                this._itemGrid.Invalidate();
            }
        }

        void _deleteLastItemButton_Click(object sender, EventArgs e)
        {
            if (this._itemGrid._rowData.Count > 0)
            {
                this._itemGrid._rowData.RemoveAt(this._itemGrid._rowData.Count - 1);
                this._itemGrid._calcTotal(true);
                this._itemGrid.Invalidate();
            }
        }

        string _docAutoRun()
        {
            return _g.g._getAutoRun(_g.g._autoRunType.ใบสั่งอาหาร, this._device_id, MyLib._myGlobal._convertDateToString(DateTime.Now, false), this._order_doc_format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
        }

        void _itemSearchLevel__menuItemClick(object sender, EventArgs e)
        {
            SMLInventoryControl._itemSearchLevelMenuControl __item = (SMLInventoryControl._itemSearchLevelMenuControl)sender;
            string __suggestRemark = "";
            // ตรวจสอบ ตัวเลือก การสั่งอาหาร เผ็ดมาก ไม่เผ็ด ถ้ามีแสดง dialog มาให้เลือก save ทำต่อไป
            if (__item._suggest_remark.Length > 0)
            {
                // gen suggest remark
                this._suggestForm = new _orderSuggestRemarkForm(__item._suggest_remark);
                this._suggestForm.ShowDialog();
                __suggestRemark = this._suggestForm._suggest_select_value;
            }

            int __addr = 0;
            string __lastItemCode = "";
            string __lastUnitCode = "";
            string __lastSuggestRemark = "";
            if (this._itemGrid._rowData.Count > 0)
            {
                __lastItemCode = this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._item_code).ToString();
                __lastUnitCode = this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._unit_code).ToString();
                __lastSuggestRemark = this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._remark).ToString();
            }
            if (__lastItemCode.Equals(__item._itemCode) && __lastUnitCode.Equals(__item._unitCode) && __lastSuggestRemark.Equals(__suggestRemark))
            {
                __addr = this._itemGrid._rowData.Count - 1;
                decimal __qty = MyLib._myGlobal._decimalPhase(this._itemGrid._cellGet(__addr, _g.d.table_order._qty).ToString()) + this._itemSearchLevel._qty;
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._qty, __qty, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._price, __item._price, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._sum_amount, __item._price * __qty, false);
            }
            else
            {
                __addr = this._itemGrid._addRow();

                // gen guid 
                string __ConformGuid = Guid.NewGuid().ToString();
                __ConformGuid = __ConformGuid.Substring(0, 18);

                this._itemGrid._cellUpdate(__addr, _g.d.table_order._confirm_guid, __ConformGuid, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._barcode, __item._barcode, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._item_code, __item._itemCode, true);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._item_name, __item._itemName, true);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._unit_code, __item._unitCode, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._qty, this._itemSearchLevel._qty, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._price, __item._price, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._sum_amount, __item._price * this._itemSearchLevel._qty, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._remark, __suggestRemark, false);
                this._itemGrid._cellUpdate(__addr, _g.d.ic_inventory._barcode_checker_print, ((__item._print_checker == true) ? 1 : 0), false);
            }
            this._itemGrid._calcTotal(true);
            // ค้นหาห้องครัว
            StringBuilder _myQuery = new StringBuilder();
            _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.kitchen_master_item._kitchen_code + " from " + _g.d.kitchen_master_item._table + " where " + _g.d.kitchen_master_item._food_code + " = \'" + __item._itemCode + "\' and " + _g.d.kitchen_master_item._kitchen_code + " in (select " + _g.d.kitchen_master_order_id._kitchen_code + " from " + _g.d.kitchen_master_order_id._table + " where " + _g.d.kitchen_master_order_id._order_device + "=\'" + this._device_id + "\')"));
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._print_order_per_unit + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = \'" + __item._itemCode + "\' "));
            _myQuery.Append("</node>");
            ArrayList _result = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());
            DataTable __dt1 = ((DataSet)_result[0]).Tables[0];
            DataTable __dt2 = ((DataSet)_result[1]).Tables[0];
            if (__dt1.Rows.Count > 0)
            {
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._kitchen_code, __dt1.Rows[0][0].ToString(), false);
                this._itemGrid._cellUpdate(__addr, _g.d.ic_inventory._print_order_per_unit, (int)MyLib._myGlobal._decimalPhase(__dt2.Rows[0][0].ToString()), false);
            }
            //
            this._itemGrid._gotoCell(__addr, 0);
            this._itemGrid.Invalidate();
        }

        private MyLib._searchDataFull _searchItem;
        private void _searchArButton_Click(object sender, EventArgs e)
        {
            _searchItem = new MyLib._searchDataFull();
            this._searchItem.Text = MyLib._myGlobal._resource("ค้นหาสมาชิก");
            this._searchItem._dataList._loadViewFormat("screen_ar_customer", MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchItem.WindowState = FormWindowState.Maximized;
            this._searchItem._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __arCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                string __arName = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1).ToString();
                this._searchItem.Close();
                this._searchItem.Dispose();
                this._orderScreen._setDataStr(_g.d.table_order_doc._cust_code, __arCode);
                this._orderScreen._setDataStr(_g.d.table_order_doc._cust_name, __arName);
            };
            this._searchItem._searchEnterKeyPress += (s1, e1) =>
            {
                string __arCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                string __arName = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1).ToString();
                this._searchItem.Close();
                this._searchItem.Dispose();
                this._orderScreen._setDataStr(_g.d.table_order_doc._cust_code, __arCode);
                this._orderScreen._setDataStr(_g.d.table_order_doc._cust_name, __arName);
            };
            this._searchItem.ShowDialog(MyLib._myGlobal._mainForm);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _saveData()
        {
            _saveData(true);
        }

        bool _saveData(bool warning)
        {
            this._itemGrid._calcTotal(true);

            if (this._itemGrid._rowData.Count > 0)
            {
                string __docNo = _docAutoRun();
                if (this._tableNumber.Trim().Length == 0)
                {
                    MessageBox.Show("กรุณาเลือกโต๊ะก่อน");
                    return false;
                }
                else
                {
                    if (__docNo.Length > 0)
                    {
                        string __docDate = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        string __docTime = DateTime.Now.ToString("HH:mm");
                        StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                        decimal __sum_qty = ((MyLib._myGrid._columnType)this._itemGrid._columnList[this._itemGrid._findColumnByName(_g.d.table_order_doc._qty)])._total;
                        decimal __sum_amount = ((MyLib._myGrid._columnType)this._itemGrid._columnList[this._itemGrid._findColumnByName(_g.d.table_order_doc._sum_amount)])._total;
                        MyLib.RandomStringGenerator __genNumber = new MyLib.RandomStringGenerator();
                        // หัวเอกสาร

                        string __order_number_barcode = MyLib._myUtil._genBarCodeEan13(__genNumber.NextString(12, false, false, true, false, false));

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_doc._table + "(" + _g.d.table_order_doc._doc_no + ", " + _g.d.table_order_doc._doc_date + "," + _g.d.table_order_doc._doc_time + "," + _g.d.table_order_doc._sale_code + "," + _g.d.table_order_doc._table_number + "," + _g.d.table_order_doc._cust_code + "," + _g.d.table_order_doc._qty + "," + _g.d.table_order_doc._sum_amount + "," + _g.d.table_order_doc._trans_guid + "," + _g.d.table_order_doc._order_number_barcode + ") values (\'" + __docNo + "\', \'" + __docDate + "\',\'" + __docTime + "\',\'" + this._saleCode + "\',\'" + this._tableNumber + "\', " + this._orderScreen._getDataStrQuery(_g.d.table_order_doc._cust_code) + "," + __sum_qty.ToString() + "," + __sum_amount.ToString() + ", '" + this._trans_guid + "', '" + __order_number_barcode + "')"));
                        // หางเอกสาร
                        for (int __row = 0; __row < this._itemGrid._rowData.Count; __row++)
                        {
                            string __fieldList = MyLib._myGlobal._fieldAndComma(
                                _g.d.table_order._doc_no,
                                _g.d.table_order._doc_date,
                                _g.d.table_order._doc_time,
                                _g.d.table_order._line_number,
                                _g.d.table_order._table_number,
                                _g.d.table_order._item_code,
                                _g.d.table_order._barcode,
                                _g.d.table_order._price,
                                _g.d.table_order._qty,
                                _g.d.table_order._qty_send,
                                _g.d.table_order._qty_cancel,
                                _g.d.table_order._qty_balance,
                                _g.d.table_order._unit_code,
                                _g.d.table_order._sum_amount,
                                //_g.d.table_order._qty_send,
                                _g.d.table_order._order_type,
                                _g.d.table_order._remark,
                                _g.d.table_order._kitchen_code,
                                _g.d.table_order._guid_line,
                                _g.d.table_order._last_status,
                                _g.d.table_order._trans_guid,
                                _g.d.table_order._as_item_name,
                                _g.d.table_order._confirm_guid,
                                _g.d.table_order._order_number_barcode
                                );

                            string __guid_line = Guid.NewGuid().ToString().ToUpper();
                            int __is_item_checker = (int)this._itemGrid._cellGet(__row, _g.d.ic_inventory._barcode_checker_print);
                            int __print_per_unit = (int)this._itemGrid._cellGet(__row, _g.d.ic_inventory._print_order_per_unit);

                            // order_type 1=ลงโต๊ะ,2=กลับบ้าน, 3=develiry
                            string __valueList = MyLib._myGlobal._fieldAndComma(
                                "\'" + __docNo + "\'",
                                "\'" + __docDate + "\'",
                                "\'" + __docTime + "\'",
                                (__row + 1).ToString(),
                                "\'" + this._tableNumber + "\'",
                                "\'" + this._itemGrid._cellGet(__row, _g.d.table_order._item_code).ToString() + "\'",
                                "\'" + this._itemGrid._cellGet(__row, _g.d.table_order._barcode).ToString() + "\'",
                                this._itemGrid._cellGet(__row, _g.d.table_order._price).ToString(),
                                this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString(),
                                "0",
                                "0",
                                this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString(),
                                "\'" + this._itemGrid._cellGet(__row, _g.d.table_order._unit_code).ToString() + "\'",
                                this._itemGrid._cellGet(__row, _g.d.table_order._sum_amount).ToString(),
                                //"0", toe mark qty_send ซ้ำ
                                ((this._mode == 2) ? "2" : "1"), // order_type
                                "\'" + this._itemGrid._cellGet(__row, _g.d.table_order._remark).ToString() + "\'",
                                "\'" + this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString() + "\'",
                                "\'" + __guid_line + "\'",
                                "0",
                                "\'" + this._trans_guid + "\'",
                                "\'" + this._itemGrid._cellGet(__row, _g.d.table_order._as_item_name).ToString() + "\'",
                                "\'" + this._itemGrid._cellGet(__row, _g.d.table_order._confirm_guid).ToString() + "\'",
                                __order_number_barcode
                                );
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order._table + " (" + __fieldList + ") values(" + __valueList + ")"));

                            // gen checker 
                            if (_g.g._companyProfile._use_order_checker == true && __is_item_checker == 1)
                            {
                                int __qty = (int)MyLib._myGlobal._decimalPhase(this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString());
                                string __fieldMaster = MyLib._myGlobal._fieldAndComma(
                                    _g.d.order_checker._doc_date,
                                    _g.d.order_checker._doc_no,
                                    _g.d.order_checker._guid_line
                                    );

                                string __valueMaster = MyLib._myGlobal._fieldAndComma(
                                    "\'" + __docDate + "\'",
                                    "\'" + __docNo + "\'",
                                    "\'" + __guid_line + "\'"
                                    );

                                if (__print_per_unit == 1)
                                {
                                    // พิมพ์ใบ order แบบแยก
                                    for (int __i = 0; __i < __qty; __i++)
                                    {
                                        string __confirm_guid = MyLib._myUtil._genBarCodeEan13(__genNumber.NextString(12, false, false, true, false, false)); // Guid.NewGuid().ToString();
                                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.order_checker._table + " (" + __fieldMaster + "," + _g.d.order_checker._guid_confirm + "," + _g.d.order_checker._order_qty + ") values (" + __valueMaster + ",\'" + __confirm_guid + "\', \'1\')"));
                                    }
                                }
                                else
                                {
                                    string __confirm_guid = MyLib._myUtil._genBarCodeEan13(__genNumber.NextString(12, false, false, true, false, false)); // Guid.NewGuid().ToString();
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.order_checker._table + " (" + __fieldMaster + "," + _g.d.order_checker._guid_confirm + "," + _g.d.order_checker._order_qty + ") values (" + __valueMaster + ",\'" + __confirm_guid + "\', \'" + __qty + "\')"));
                                }
                            }
                        }
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + "," + _g.d.table_order_log._sale_code + ") values (now(),\'Order\',\'Table Number : " + this._tableNumber + ", trans_guid : " + this._trans_guid + ",Doc No : " + __docNo + "\',\'" + this._saleCode + "\')"));
                        __query.Append("</node>");

                        string __resultStr = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());

                        if (__resultStr.Length == 0)
                        {

                            // ส่งไปครัว
                            if (this._print_from_center == true)
                            {
                                __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_command._table + "(" + _g.d.kitchen_command._doc_no + "," + _g.d.kitchen_command._doc_date + "," + _g.d.kitchen_command._doc_time + ") values (\'" + __docNo + "\',\'" + __docDate + "\',\'" + __docTime + "\')"));
                                __query.Append("</node>");

                                __resultStr = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                                if (__resultStr.Length == 0)
                                {
                                    if (warning)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("บันทึกรายการสั่งอาหาร เรียบร้อยแล้ว"), MyLib._myGlobal._resource("บันทึก"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(__resultStr);
                                    return false;

                                }
                            }
                            else
                            {
                                if (warning)
                                {
                                    _kitchenPrintClass __printOrder = new _kitchenPrintClass();
                                    __printOrder._printOrder(__docNo, __docDate, __docTime, 0);
                                    MessageBox.Show(MyLib._myGlobal._resource("บันทึกรายการสั่งอาหาร เรียบร้อยแล้ว"), MyLib._myGlobal._resource("บันทึก"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(__resultStr);
                        }
                        if (_processComplete != null)
                        {
                            _processComplete();
                        }

                        if (warning)
                        {
                            this._tableNumber = "";
                            this._tableName = "";
                            this._tableLabel.Text = this._tableNumber + "/" + this._tableName;
                            this._orderScreen._setDataStr(_g.d.table_order_doc._table_number, this._tableNumber);
                            if (this._mode == 1 || this._mode == 2)
                            {
                                this._reset();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบเลขที่เอกสารการสั่งอาหาร", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            else
            {
                if (warning)
                {
                    MessageBox.Show("ไม่มีรายการอาหาร", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return true;
        }

        private void _buttonSearchItem_Click(object sender, EventArgs e)
        {
            this._searchItem = new MyLib._searchDataFull();
            this._searchItem.Text = MyLib._myGlobal._resource("ค้นหาสินค้าตาม Barcode");
            this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ic_barcode, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchItem.WindowState = FormWindowState.Maximized;
            this._searchItem._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __barCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow,
                    _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
                this._searchItem.Close();
                this._searchItem.Dispose();
                //this._command("append:textbox:input=" + __barCode + "@end:textbox:input");
                _addItemGrid(__barCode);
            };
            this._searchItem._searchEnterKeyPress += (s1, e1) =>
            {
                string __barCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow,
                    _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
                this._searchItem.Close();
                this._searchItem.Dispose();
                _addItemGrid(__barCode);
                //this._command("append:textbox:input=" + __barCode + "@end:textbox:input");
            };
            this._searchItem.ShowDialog(this);
        }

        void _addItemGrid(string itemBarcode)
        {
            _addItemGrid(itemBarcode, 1M);
        }

        void _addItemGrid(string itemBarcode, decimal qty)
        {

            StringBuilder _myQuery = new StringBuilder();
            _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_barcode._barcode + "," + _g.d.ic_inventory_barcode._ic_code + "," + _g.d.ic_inventory_barcode._description + "," + _g.d.ic_inventory_barcode._unit_code + "," + _g.d.ic_inventory_barcode._price + "," + _g.d.ic_inventory_barcode._price_2 + "," + _g.d.ic_inventory_barcode._price_3 + "," + _g.d.ic_inventory_barcode._price_4 + 
                ", (select " + _g.d.ic_inventory_level._suggest_remark + " from " + _g.d.ic_inventory_level._table + " where " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._barcode + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode + " and " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._ic_code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " and " + _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._unit_code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " ) as " + _g.d.ic_inventory_level._suggest_remark +
                ", coalesce((select " + _g.d.ic_inventory_detail._is_hold_sale + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + "), 0) as " + _g.d.ic_inventory_detail._is_hold_sale + 
                " from " + _g.d.ic_inventory_barcode._table + 
                " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + " = \'" + itemBarcode.ToUpper() + "\'"));
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.order_device_id._device_name + "," + _g.d.order_device_id._price_number + " from " + _g.d.order_device_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.order_device_id._device_id) + " = \'" + this._device_id.ToUpper() + "\'"));

            _myQuery.Append("</node>");

            ArrayList _result = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());

            DataTable __dt1 = ((DataSet)_result[0]).Tables[0];
            DataTable __dt2 = ((DataSet)_result[1]).Tables[0];

            if (__dt1.Rows.Count > 0)
            {
                // toe check หยุดขาย
                if (__dt1.Rows[0][_g.d.ic_inventory_detail._is_hold_sale].ToString().Equals("1"))
                {
                    MessageBox.Show("สินค้า " + __dt1.Rows[0][_g.d.ic_inventory_barcode._description].ToString() + " หยุดขาย", "เตือน");
                    return;
                }

                // get price
                int __priceLevel = (__dt2.Rows.Count > 0) ? (int)MyLib._myGlobal._decimalPhase(__dt2.Rows[0][_g.d.order_device_id._price_number].ToString()) : 0;
                decimal _price = 0M;

                switch (__priceLevel)
                {
                    case 2:
                        _price = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_inventory_barcode._price_2].ToString());
                        break;
                    case 3:
                        _price = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_inventory_barcode._price_3].ToString());
                        break;
                    case 4:
                        _price = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_inventory_barcode._price_4].ToString());
                        break;
                    default:
                        _price = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_inventory_barcode._price].ToString());
                        break;

                }

                string __item_success_remark = __dt1.Rows[0][_g.d.ic_inventory_level._suggest_remark].ToString();

                string __suggestRemark = "";
                string __itemCode = __dt1.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString(); ;
                string __unitCode = __dt1.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString(); ;
                // suggest remark
                if (__item_success_remark.Length > 0)
                {
                    this._suggestForm = new _orderSuggestRemarkForm(__item_success_remark);
                    this._suggestForm.ShowDialog();
                    __suggestRemark = this._suggestForm._suggest_select_value;
                }

                string __lastItemCode = "";
                string __lastUnitCode = "";
                string __lastSuggestRemark = "";
                if (this._itemGrid._rowData.Count > 0)
                {
                    __lastItemCode = this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._item_code).ToString();
                    __lastUnitCode = this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._unit_code).ToString();
                    __lastSuggestRemark = this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._remark).ToString();
                }
                int __addr = this._itemGrid._addRow();
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._item_code, __dt1.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString(), true);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._item_name, __dt1.Rows[0][_g.d.ic_inventory_barcode._description].ToString(), true);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._unit_code, __dt1.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString(), false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._qty, qty, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._price, _price, false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._sum_amount, (_price * qty), false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._barcode, __dt1.Rows[0][_g.d.ic_inventory_barcode._barcode].ToString(), false);
                this._itemGrid._cellUpdate(__addr, _g.d.table_order._remark, __suggestRemark, false);

                // ค้นหาห้องครัว
                StringBuilder _myQueryKitchen = new StringBuilder();
                _myQueryKitchen.Append(MyLib._myGlobal._xmlHeader + "<node>");
                _myQueryKitchen.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.kitchen_master_item._kitchen_code + " from " + _g.d.kitchen_master_item._table + " where " + _g.d.kitchen_master_item._food_code + " = \'" + __dt1.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString() + "\' and " + _g.d.kitchen_master_item._kitchen_code + " in (select " + _g.d.kitchen_master_order_id._kitchen_code + " from " + _g.d.kitchen_master_order_id._table + " where " + _g.d.kitchen_master_order_id._order_device + "=\'" + this._device_id + "\')"));
                _myQueryKitchen.Append("</node>");
                ArrayList _resultKitchen = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQueryKitchen.ToString());
                DataTable __dt1Kitchen = ((DataSet)_resultKitchen[0]).Tables[0];
                if (__dt1Kitchen.Rows.Count > 0)
                {
                    this._itemGrid._cellUpdate(__addr, _g.d.table_order._kitchen_code, __dt1Kitchen.Rows[0][0].ToString(), false);
                }
                //
                this._itemGrid._calcTotal(true);
                this._itemGrid._gotoCell(__addr, 0);
                this._itemGrid.Invalidate();

            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F12:
                    if (this._isForClseTable)
                    {
                        _checkInputKey("995");
                    }
                    else
                        _saveData();
                    return false;
                case Keys.F2:
                    if (this._isForClseTable == false)
                        _searchArButton_Click(this, null);
                    return false;
                case Keys.F3:
                    _buttonSearchItem_Click(this, null);
                    return false;
                case Keys.F5:
                    if (this._isForClseTable == false)
                        _productBasketButton_Click(this, null);
                    return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _productBasketButton_Click(object sender, EventArgs e)
        {
            Form __levelForm = new Form();
            __levelForm.WindowState = FormWindowState.Maximized;
            SMLInventoryControl._itemSearchLevelControl __control = new SMLInventoryControl._itemSearchLevelControl();
            __control._productBasket = true;
            __control._menuItemClick += (menuSender, menuEvent) =>
            {

                //SMLInventoryControl._itemSearchLevelMenuControl __obj = (SMLInventoryControl._itemSearchLevelMenuControl)menuSender;

                //__levelForm.Close();
                //string __itemCode = __obj._itemCode;

                //// checkrowdata
                //int __newRow = (this._selectRow > this._rowData.Count || this._selectRow == -1) ? this._addRow() : this._selectRow;

                //this._cellUpdate(__newRow, _g.d.ic_trans_detail._item_code, __itemCode, true);
                //SendKeys.Send("{TAB}");
                for (int __row = 0; __row < __control._selectGrid._rowData.Count; __row++)
                {
                    string __itemCode = __control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                    string __BarCode = __control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._barcode).ToString();
                    string __unitCode = __control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                    decimal __price = MyLib._myGlobal._decimalPhase(__control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._price).ToString());
                    decimal __qty = MyLib._myGlobal._decimalPhase(__control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());

                    if (!__BarCode.Equals(""))
                    {
                        //this._command("enter:textbox:input=" + __qty + "*" + __BarCode + "@end:textbox:input", serialNumber);
                        //if (__price != -1)
                        //{
                        //    this._command("&command&,*1*" + __price + "@end:textbox:input", serialNumber);
                        //}
                        _addItemGrid(__BarCode, __qty);

                        // แก้ไขจำนวน
                    }
                }
                __levelForm.Close();
            };
            __control.Dock = DockStyle.Fill;
            __levelForm.Controls.Add(__control);
            __levelForm.ShowDialog();
        }

        public void _checkInputKey(string input)
        {

            string __text = input;
            /**/
            if (__text.Equals("999") || (__text.Equals("995") && this._mode == 2))
            {
                // บันทึก

                // check จาก *02 หรือเปล่า
                if (this._isForClseTable)
                {
                    _checkInputKey("995");
                }
                else
                    _saveData();
            }
            else
                if (__text.Equals("998"))
                {
                    // เริ่มใหม่
                    this._itemGrid._clear();
                    this._itemGrid.Invalidate();
                }
                else
                    if (__text.Equals("995"))
                    {
                        // บันทึก order และทำการปิดโต๊ะ
                        if (this._tableNumber != "")
                        {
                            if (MessageBox.Show("ยืนยันการบันทึกรายการ/ปิดโต๊ะ โต๊ะ : " + this._tableNumber + "/" + this._tableName, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (this._saveData(false))
                                {
                                    // close table
                                    // เช็คสถานะโต๊ะ
                                    StringBuilder __myQueryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                    __myQueryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(" + _g.d.table_order._item_code + ") as countItem, sum(" + _g.d.table_order._qty_balance + ") as sum_balance from " + _g.d.table_order._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0 "));
                                    __myQueryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.table_master._status + " from " + _g.d.table_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber.ToUpper() + "\'  "));
                                    __myQueryList.Append("</node>");

                                    ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myQueryList.ToString());

                                    DataTable _orderTableRow = ((DataSet)__getData[0]).Tables[0];
                                    DataTable __dTableStatus = ((DataSet)__getData[1]).Tables[0];
                                    int __tableStatus = (int)MyLib._myGlobal._decimalPhase(__dTableStatus.Rows[0][_g.d.table_master._status].ToString());
                                    if (__tableStatus == 1)
                                    {
                                        if (_orderTableRow.Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(_orderTableRow.Rows[0]["countItem"].ToString()) > 0) && (MyLib._myGlobal._decimalPhase(_orderTableRow.Rows[0]["sum_balance"].ToString()) > 0))
                                        {
                                            // ปิดโต๊ะ อัพเดทสถานะ รอคิดเงิน
                                            StringBuilder __myQuery = new StringBuilder();
                                            IFormatProvider __cultureEng = new CultureInfo("en-US");
                                            string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                            string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber.ToUpper() + "\'"));
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "=\'" + __date + "\'," + _g.d.table_trans._close_doc_time + "=\'" + __time + "\'," + _g.d.table_trans._close_sale_code + "=\'" + this._saleCode + "\' where " + _g.d.table_trans._trans_guid + " = \'" + this._trans_guid + "\'"));
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._last_status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0"));// โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order_doc._table + " set " + _g.d.table_order_doc._last_status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_order_doc._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order_doc._last_status + " =0")); // โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                            __myQuery.Append("</node>");

                                            string __result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                            if (__result.Length == 0)
                                            {
                                                MessageBox.Show("ปิดโต๊ะหมายเลข" + " : " + this._tableNumber + "/" + this._tableName + " สำเร็จ");

                                                // show close table
                                                if (_afterCloseTableForPay != null)
                                                {
                                                    _tableSearchLevelMenuControl __tableControl = new _tableSearchLevelMenuControl(this._tableNumber, this._tableName, this._trans_guid, 2, 1);
                                                    _afterCloseTableForPay(this, __tableControl, this._saleCode, true);
                                                }
                                                this._reset();
                                            }
                                            else
                                            {
                                                MessageBox.Show(__result.ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (MessageBox.Show("ไม่มีรายการอาหารในโต๊ะ " + this._tableName + "/" + this._tableName + " คุณต้องการที่จะปิดโต๊ะ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                            {
                                                // โต๊ะว่าง อัพเดทสถานะ ว่าง
                                                StringBuilder __myQuery = new StringBuilder();
                                                IFormatProvider __cultureEng = new CultureInfo("en-US");
                                                string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                                string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                                // อัพเดท ผู้ปิดโต๊ะ 
                                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "=\'" + __date + "\'," + _g.d.table_trans._close_doc_time + "=\'" + __time + "\'," + _g.d.table_trans._close_sale_code + "=\'" + this._saleCode + "\' where " + _g.d.table_trans._trans_guid + " = \'" + this._trans_guid + "\'"));
                                                // เปลี่ยนสถานะโต๊ะ เป็น ว่าง, trans_guid = ''
                                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=0, " + _g.d.table_order._trans_guid + "=\'\' where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber.ToUpper() + "\'"));
                                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._last_status + "=3 where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0 "));
                                                __myQuery.Append("</node>");

                                                string __result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                                if (__result.Length == 0)
                                                {
                                                    MessageBox.Show("ปิดโต๊ะหมายเลข" + " : " + this._tableNumber + "/" + this._tableName + " สำเร็จ");
                                                }
                                                else
                                                {
                                                    MessageBox.Show(__result.ToString());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("โต๊ะ " + this._tableNumber + " ไม่สามารถปิดโต๊ะได้");
                                    }

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("กรุณาเลือกโต๊ะก่อนทำรายการบันทึกรายการ/ปิดโต๊ะ");
                        }
                    }
                    else
                        if (__text.Equals("991"))
                        {
                            // ลบบรรทัดล่าสุด
                            if (this._itemGrid._rowData.Count > 0)
                            {
                                this._itemGrid._rowData.RemoveAt(this._itemGrid._rowData.Count - 1);
                                this._itemGrid.Invalidate();
                            }
                        }
                        else
                        {
                            // หาโต๊ะ, หาหมายเหตุ
                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_master._number, _g.d.table_master._name_1, _g.d.table_master._status, _g.d.table_master._trans_guid) + " from " + _g.d.table_master._table + " where " + _g.d.table_master._table_barcode + "=\'" + __text + "\'"));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_remark._remark_desc + " from " + _g.d.ic_inventory_remark._table + " where " + _g.d.ic_inventory_remark._remark_code + "=\'" + __text + "\'"));

                            // หา order ในโต๊ะ
                            string __tableOrderQuery = "select * " +
                                ", (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ") as " + _g.d.table_order._item_name +
                                ", (select coalesce(" + _g.d.ic_inventory._drink_type + ", 0) from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ") as " + _g.d.ic_inventory._drink_type +
                                " from " + _g.d.table_order._table +
                                " where " + _g.d.table_order._table_number + " = (" + "select " + _g.d.table_master._number + " from " + _g.d.table_master._table + " where " + _g.d.table_master._table_barcode + "=\'" + __text + "\'" + ") and " + _g.d.table_order._last_status + " in (0,1) ";// + 
                            //" order by (select coalesce(" + _g.d.ic_inventory._drink_type + ", 0) from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ")";
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__tableOrderQuery));


                            __myquery.Append("</node>");
                            ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            DataTable __dTable = ((DataSet)__getData[0]).Tables[0];
                            DataTable __dRemark = ((DataSet)__getData[1]).Tables[0];
                            DataTable __dOrderList = ((DataSet)__getData[2]).Tables[0];

                            if (__dTable.Rows.Count > 0)
                            {
                                int __tableStatus = (int)MyLib._myGlobal._decimalPhase(__dTable.Rows[0][_g.d.table_master._status].ToString());
                                string __getTableNumber = __dTable.Rows[0][_g.d.table_master._number].ToString();
                                Boolean __swithTable = false;
                                if (__tableStatus == 0 && (this._mode == 1 || this._mode == 0))
                                {
                                    this._tableNumber = __dTable.Rows[0][_g.d.table_master._number].ToString();
                                    this._tableName = __dTable.Rows[0][_g.d.table_master._name_1].ToString();
                                    this._tableLabel.Text = this._tableNumber + "/" + this._tableName;
                                    // โต๊ะ ปิดอยู่ ให้เปิดเลย
                                    __swithTable = true;
                                    //
                                    StringBuilder __myQuery = new StringBuilder();
                                    IFormatProvider __cultureEng = new CultureInfo("en-US");
                                    string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                    string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    this._trans_guid = Guid.NewGuid().ToString();
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=1," + _g.d.table_master._trans_guid + "=\'" + this._trans_guid + "\'," + _g.d.table_master._open_date + "=\'" + __date + "\'," + _g.d.table_master._open_time + "=\'" + __time + "\' where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber + "\'"));
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_trans._table + " (" + _g.d.table_trans._trans_guid + "," + _g.d.table_trans._table_number + "," + _g.d.table_trans._open_doc_date + "," + _g.d.table_trans._open_doc_time + "," + _g.d.table_trans._open_sale_code + ") values (\'" + this._trans_guid + "\',\'" + this._tableNumber + "\',\'" + __date + "\',\'" + __time + "\',\'\')"));
                                    __myQuery.Append("</node>");
                                    string __result = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                    if (__result.Length != 0)
                                    {
                                        MessageBox.Show(__result.ToString());
                                    }
                                }
                                else
                                    if (__tableStatus == 1 || this._mode == 2)
                                    {
                                        // เปิดแล้ว ให้ switch โต๊ะ
                                        __swithTable = true;
                                        this._tableNumber = __dTable.Rows[0][_g.d.table_master._number].ToString();
                                        this._tableName = __dTable.Rows[0][_g.d.table_master._name_1].ToString();
                                        this._tableLabel.Text = this._tableNumber + "/" + this._tableName;

                                        this._trans_guid = __dTable.Rows[0][_g.d.table_master._trans_guid].ToString();
                                    }
                                    else
                                    {
                                        MessageBox.Show("โต๊ะ " + __getTableNumber + " ไม่สามารถสั่งอาหารได้");
                                    }
                                if (__swithTable)
                                {
                                    this._orderScreen._setDataDate(_g.d.table_order_doc._doc_date, DateTime.Now);
                                    this._orderScreen._setDataStr(_g.d.table_order_doc._doc_time, DateTime.Now.ToString("HH:mm"));
                                    this._orderScreen._setDataStr(_g.d.table_order_doc._doc_no, this._docAutoRun());
                                    this._orderScreen._setDataStr(_g.d.table_order_doc._table_number, this._tableNumber);
                                    this._itemOrderList._loadFromDataTable(__dOrderList);
                                }
                            }
                            else
                                if (__dRemark.Rows.Count > 0)
                                {
                                    if (this._itemGrid._rowData.Count > 0)
                                    {
                                        StringBuilder __get = new StringBuilder(this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._remark).ToString().Trim());
                                        if (__get.Length > 0)
                                        {
                                            __get.Append(",");
                                        }
                                        __get.Append(__dRemark.Rows[0][_g.d.ic_inventory_remark._remark_desc].ToString());
                                        this._itemGrid._cellUpdate(this._itemGrid._rowData.Count - 1, _g.d.table_order._remark, __get.ToString(), false);
                                        this._itemGrid.Invalidate();
                                    }
                                }
                                else
                                {
                                    // หาสินค้า
                                    string[] __splitBarCode = __text.Split('*');
                                    decimal __qty = 1;
                                    string __barCode = "";
                                    if (__splitBarCode.Length == 2)
                                    {
                                        // มีจำนวนและรหัสสินค้า เช่น 5*554456
                                        __qty = MyLib._myGlobal._decimalPhase(__splitBarCode[0].ToString());
                                        __barCode = __splitBarCode[1].ToString().ToUpper();
                                    }
                                    else
                                    {
                                        __barCode = __splitBarCode[0].ToString().ToUpper();
                                    }
                                    _addItemGrid(__barCode, __qty);
                                }
                        }
        }

        private void _inputCodeTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    {
                        string __text = _inputCodeTextbox.Text.Trim().ToUpper();
                        if (__text.Length > 0)
                        {
                            _checkInputKey(__text);
                            // โต๋ ย้ายไป checkinput
                            /* 
                            if (__text.Equals("999"))
                            {
                                // บันทึก
                                _saveData();
                            }
                            else
                                if (__text.Equals("998"))
                                {
                                    // เริ่มใหม่
                                    this._itemGrid._clear();
                                    this._itemGrid.Invalidate();
                                }
                                else
                                    if (__text.Equals("995"))
                                    {
                                        // บันทึก order และทำการปิดโต๊ะ
                                        if (this._tableNumber != "")
                                        {
                                            if (MessageBox.Show("ยืนยันการบันทึกรายการ/ปิดโต๊ะ โต๊ะ : " + this._tableNumber + "/" + this._tableName, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                if (this._saveData(false))
                                                {
                                                    // close table
                                                    // เช็คสถานะโต๊ะ
                                                    StringBuilder __myQueryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                    __myQueryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(" + _g.d.table_order._item_code + ") as countItem, sum(" + _g.d.table_order._qty_balance + ") as sum_balance from " + _g.d.table_order._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0 "));
                                                    __myQueryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.table_master._status + " from " + _g.d.table_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber.ToUpper() + "\'  "));
                                                    __myQueryList.Append("</node>");

                                                    ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myQueryList.ToString());

                                                    DataTable _orderTableRow = ((DataSet)__getData[0]).Tables[0];
                                                    DataTable __dTableStatus = ((DataSet)__getData[1]).Tables[0];
                                                    int __tableStatus = (int)MyLib._myGlobal._decimalPhase(__dTableStatus.Rows[0][_g.d.table_master._status].ToString());
                                                    if (__tableStatus == 1)
                                                    {
                                                        if (_orderTableRow.Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(_orderTableRow.Rows[0]["countItem"].ToString()) > 0) && (MyLib._myGlobal._decimalPhase(_orderTableRow.Rows[0]["sum_balance"].ToString()) > 0))
                                                        {
                                                            // ปิดโต๊ะ อัพเดทสถานะ รอคิดเงิน
                                                            StringBuilder __myQuery = new StringBuilder();
                                                            IFormatProvider __cultureEng = new CultureInfo("en-US");
                                                            string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                                            string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber.ToUpper() + "\'"));
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "=\'" + __date + "\'," + _g.d.table_trans._close_doc_time + "=\'" + __time + "\'," + _g.d.table_trans._close_sale_code + "=\'" + this._saleCode + "\' where " + _g.d.table_trans._trans_guid + " = \'" + this._trans_guid + "\'"));
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._last_status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0"));// โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order_doc._table + " set " + _g.d.table_order_doc._last_status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_order_doc._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order_doc._last_status + " =0")); // โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                                            __myQuery.Append("</node>");

                                                            string __result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                                            if (__result.Length == 0)
                                                            {
                                                                MessageBox.Show("ปิดโต๊ะหมายเลข" + " : " + this._tableNumber + "/" + this._tableName + " สำเร็จ");

                                                                // show close table
                                                                if (_afterCloseTableForPay != null)
                                                                {
                                                                    _tableSearchLevelMenuControl __tableControl = new _tableSearchLevelMenuControl(this._tableNumber, this._tableName, this._trans_guid, 2, 1);
                                                                    _afterCloseTableForPay(this, __tableControl, this._saleCode, true);
                                                                }
                                                                this._reset();
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show(__result.ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (MessageBox.Show("ไม่มีรายการอาหารในโต๊ะ " + this._tableName + "/" + this._tableName + " คุณต้องการที่จะปิดโต๊ะ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                                            {
                                                                // โต๊ะว่าง อัพเดทสถานะ ว่าง
                                                                StringBuilder __myQuery = new StringBuilder();
                                                                IFormatProvider __cultureEng = new CultureInfo("en-US");
                                                                string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                                                string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                                                // อัพเดท ผู้ปิดโต๊ะ 
                                                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "=\'" + __date + "\'," + _g.d.table_trans._close_doc_time + "=\'" + __time + "\'," + _g.d.table_trans._close_sale_code + "=\'" + this._saleCode + "\' where " + _g.d.table_trans._trans_guid + " = \'" + this._trans_guid + "\'"));
                                                                // เปลี่ยนสถานะโต๊ะ เป็น ว่าง, trans_guid = ''
                                                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=0, " + _g.d.table_order._trans_guid + "=\'\' where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber.ToUpper() + "\'"));
                                                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._last_status + "=3 where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + this._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0 "));
                                                                __myQuery.Append("</node>");

                                                                string __result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                                                if (__result.Length == 0)
                                                                {
                                                                    MessageBox.Show("ปิดโต๊ะหมายเลข" + " : " + this._tableNumber + "/" + this._tableName + " สำเร็จ");
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show(__result.ToString());
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("โต๊ะ " + this._tableNumber + " ไม่สามารถปิดโต๊ะได้");
                                                    }

                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("กรุณาเลือกโต๊ะก่อนทำรายการบันทึกรายการ/ปิดโต๊ะ");
                                        }
                                    }
                                    else
                                        if (__text.Equals("991"))
                                        {
                                            // ลบบรรทัดล่าสุด
                                            if (this._itemGrid._rowData.Count > 0)
                                            {
                                                this._itemGrid._rowData.RemoveAt(this._itemGrid._rowData.Count - 1);
                                                this._itemGrid.Invalidate();
                                            }
                                        }
                                        else
                                        {
                                            // หาโต๊ะ, หาหมายเหตุ
                                            StringBuilder __myquery = new StringBuilder();
                                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_master._number, _g.d.table_master._name_1, _g.d.table_master._status, _g.d.table_master._trans_guid) + " from " + _g.d.table_master._table + " where " + _g.d.table_master._table_barcode + "=\'" + __text + "\'"));
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_remark._remark_desc + " from " + _g.d.ic_inventory_remark._table + " where " + _g.d.ic_inventory_remark._remark_code + "=\'" + __text + "\'"));

                                            // หา order ในโต๊ะ
                                            string __tableOrderQuery = "select * " +
                                                ", (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table +"." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ") as " + _g.d.table_order._item_name +
                                                ", (select coalesce(" + _g.d.ic_inventory._drink_type + ", 0) from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ") as " + _g.d.ic_inventory._drink_type +
                                                " from " + _g.d.table_order._table +
                                                " where " + _g.d.table_order._table_number + " = (" + "select " + _g.d.table_master._number + " from " + _g.d.table_master._table + " where " + _g.d.table_master._table_barcode + "=\'" + __text + "\'" + ") and " + _g.d.table_order._last_status + " in (0,1) ";// + 
                                            //" order by (select coalesce(" + _g.d.ic_inventory._drink_type + ", 0) from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.table_order._table + "." + _g.d.table_order._item_code + ")";
                                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__tableOrderQuery));


                                            __myquery.Append("</node>");
                                            ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                                            DataTable __dTable = ((DataSet)__getData[0]).Tables[0];
                                            DataTable __dRemark = ((DataSet)__getData[1]).Tables[0];
                                            DataTable __dOrderList = ((DataSet)__getData[2]).Tables[0];

                                            if (__dTable.Rows.Count > 0)
                                            {
                                                int __tableStatus = (int)MyLib._myGlobal._decimalPhase(__dTable.Rows[0][_g.d.table_master._status].ToString());
                                                string __getTableNumber = __dTable.Rows[0][_g.d.table_master._number].ToString();
                                                Boolean __swithTable = false;
                                                if (__tableStatus == 0)
                                                {
                                                    this._tableNumber = __dTable.Rows[0][_g.d.table_master._number].ToString();
                                                    this._tableName = __dTable.Rows[0][_g.d.table_master._name_1].ToString();
                                                    this._tableLabel.Text = this._tableNumber + "/" + this._tableName;
                                                    // โต๊ะ ปิดอยู่ ให้เปิดเลย
                                                    __swithTable = true;
                                                    //
                                                    StringBuilder __myQuery = new StringBuilder();
                                                    IFormatProvider __cultureEng = new CultureInfo("en-US");
                                                    string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                                    string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                                    this._trans_guid = Guid.NewGuid().ToString();
                                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=1," + _g.d.table_master._trans_guid + "=\'" + this._trans_guid + "\'," + _g.d.table_master._open_date + "=\'" + __date + "\'," + _g.d.table_master._open_time + "=\'" + __time + "\' where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumber + "\'"));
                                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_trans._table + " (" + _g.d.table_trans._trans_guid + "," + _g.d.table_trans._table_number + "," + _g.d.table_trans._open_doc_date + "," + _g.d.table_trans._open_doc_time + "," + _g.d.table_trans._open_sale_code + ") values (\'" + this._trans_guid + "\',\'" + this._tableNumber + "\',\'" + __date + "\',\'" + __time + "\',\'\')"));
                                                    __myQuery.Append("</node>");
                                                    string __result = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                                    if (__result.Length != 0)
                                                    {
                                                        MessageBox.Show(__result.ToString());
                                                    }
                                                }
                                                else
                                                    if (__tableStatus == 1)
                                                    {
                                                        // เปิดแล้ว ให้ switch โต๊ะ
                                                        __swithTable = true;
                                                        this._tableNumber = __dTable.Rows[0][_g.d.table_master._number].ToString();
                                                        this._tableName = __dTable.Rows[0][_g.d.table_master._name_1].ToString();
                                                        this._tableLabel.Text = this._tableNumber + "/" + this._tableName;

                                                        this._trans_guid = __dTable.Rows[0][_g.d.table_master._trans_guid].ToString();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("โต๊ะ " + __getTableNumber  + " ไม่สามารถสั่งอาหารได้");
                                                    }
                                                if (__swithTable)
                                                {
                                                    this._orderScreen._setDataDate(_g.d.table_order_doc._doc_date, DateTime.Now);
                                                    this._orderScreen._setDataStr(_g.d.table_order_doc._doc_time, DateTime.Now.ToString("HH:mm"));
                                                    this._orderScreen._setDataStr(_g.d.table_order_doc._doc_no, this._docAutoRun());
                                                    this._orderScreen._setDataStr(_g.d.table_order_doc._table_number, this._tableNumber);
                                                    this._itemOrderList._loadFromDataTable(__dOrderList);
                                                }
                                            }
                                            else
                                                if (__dRemark.Rows.Count > 0)
                                                {
                                                    if (this._itemGrid._rowData.Count > 0)
                                                    {
                                                        StringBuilder __get = new StringBuilder(this._itemGrid._cellGet(this._itemGrid._rowData.Count - 1, _g.d.table_order._remark).ToString().Trim());
                                                        if (__get.Length > 0)
                                                        {
                                                            __get.Append(",");
                                                        }
                                                        __get.Append(__dRemark.Rows[0][_g.d.ic_inventory_remark._remark_desc].ToString());
                                                        this._itemGrid._cellUpdate(this._itemGrid._rowData.Count - 1, _g.d.table_order._remark, __get.ToString(), false);
                                                        this._itemGrid.Invalidate();
                                                    }
                                                }
                                                else
                                                {
                                                    // หาสินค้า
                                                    string[] __splitBarCode = __text.Split('*');
                                                    decimal __qty = 1;
                                                    string __barCode = "";
                                                    if (__splitBarCode.Length == 2)
                                                    {
                                                        // มีจำนวนและรหัสสินค้า เช่น 5*554456
                                                        __qty = MyLib._myGlobal._decimalPhase(__splitBarCode[0].ToString());
                                                        __barCode = __splitBarCode[1].ToString().ToUpper();
                                                    }
                                                    else
                                                    {
                                                        __barCode = __splitBarCode[0].ToString().ToUpper();
                                                    }
                                                    _addItemGrid(__barCode, __qty);
                                                }
                                        }
                             *  * */
                        }

                        _inputCodeTextbox.Text = "";
                    }
                    break;
            }
        }

        private void _saveAndCloseTableButton_Click(object sender, EventArgs e)
        {
            this._checkInputKey("995");
        }

    }
}
