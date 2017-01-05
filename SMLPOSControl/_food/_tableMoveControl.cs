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
    public partial class _tableMoveControl : UserControl
    {
        public delegate void SaveSuccessEventHandler(_tableMoveControl sender);
        public event SaveSuccessEventHandler _saveSuccess;

        private _tableSearchLevelControl _tableSource = new _tableSearchLevelControl();
        private _tableSearchLevelControl _tableDestination = new _tableSearchLevelControl();
        private _itemGridControl _itemGridSource = new _itemGridControl(0, true);
        private _itemGridControl _itemGridDestination = new _itemGridControl(0, true);
        private string _tableSourceCode = "";
        private string _tableSourceTransGuidCode = "";

        private string _tableDestinationCode = "";
        private string _tableDestinationTransGuidCode = "";

        public string _saleCode = "";

        DataTable _sourceTempDataTable;
        DataTable _desinationTempDataTable;
        public _tableMoveControl()
        {
            InitializeComponent();
            this._tableSource.Dock = DockStyle.Fill;
            this._tableDestination.Dock = DockStyle.Fill;
            this._itemGridSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._itemGridSource.Dock = DockStyle.Fill;
            this._itemGridDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._itemGridDestination.Dock = DockStyle.Fill;
            this._itemGridSource._message = this._itemGridDestination._message = "Double Click = ย้ายรายการ";
            this._selectTableSourcePanel.Controls.Add(this._tableSource);
            this._selectTableDestinationPanel.Controls.Add(this._tableDestination);
            this._sourcePanel.Controls.Add(this._itemGridSource);
            this._destinationPanel.Controls.Add(this._itemGridDestination);
            //
            this._tableSource._menuTableClick += (s1, e1) =>
            {
                _tableSearchLevelMenuControl __s1 = (_tableSearchLevelMenuControl)s1;
                switch (__s1._status)
                {
                    case 0:
                        MessageBox.Show("โต๊ะยังไม่ได้เปิด");
                        break;
                    case 1:
                        this._tableSourceCode = __s1._tableNumber;
                        this._tableSourceTransGuidCode = __s1._transGuidNumber;
                        this._tableSourceLabel.Text = __s1._tableNumber + "/" + __s1._tableName;
                        this._sourceTempDataTable = this._loadOrder(this._tableSourceCode, this._tableSourceTransGuidCode, this._itemGridSource);
                        if (this._tableDestinationCode != "")
                        {
                            this._desinationTempDataTable = this._loadOrder(this._tableDestinationCode, this._tableDestinationTransGuidCode, this._itemGridDestination);
                        }
                        // Clear Destination
                        this._tableDestinationCode = "";
                        this._tableDestinationTransGuidCode = "";
                        this._tableDestinationLabel.Text = "";
                        this._itemGridDestination._clear();
                        break;
                    case 2:
                        MessageBox.Show("ไม่สามารถเลือกโต๊ะนี้ได้ กำลังอยู่ในสถานะ รอคิดเงิน");
                        break;
                    default:
                        MessageBox.Show("ผิดพลาด");
                        break;
                }

            };
            this._tableDestination._menuTableClick += (s1, e1) =>
            {
                _tableSearchLevelMenuControl __s1 = (_tableSearchLevelMenuControl)s1;
                if (__s1._tableNumber.Equals(this._tableSourceCode))
                {
                    MessageBox.Show("ห้ามเลือกโต๊ะเดียวกัน");
                }
                else
                {
                    switch (__s1._status)
                    {
                        case 0:
                            MessageBox.Show("โต๊ะยังไม่ได้เปิด");
                            break;
                        case 1:

                            this._tableDestinationCode = __s1._tableNumber;
                            this._tableDestinationTransGuidCode = __s1._transGuidNumber;
                            this._tableDestinationLabel.Text = __s1._tableNumber + "/" + __s1._tableName;
                            this._desinationTempDataTable = this._loadOrder(this._tableDestinationCode, this._tableDestinationTransGuidCode, this._itemGridDestination);
                            if (this._tableSourceCode != "")
                            {
                                this._sourceTempDataTable = this._loadOrder(this._tableSourceCode, this._tableSourceTransGuidCode, this._itemGridSource);
                            }
                            break;
                        case 2:
                            MessageBox.Show("ไม่สามารถเลือกโต๊ะนี้ได้ กำลังอยู่ในสถานะ รอคิดเงิน");
                            break;
                        default:
                            MessageBox.Show("ผิดพลาด");
                            break;
                    }

                }
            };
            this._itemGridSource._mouseDoubleClick += (s1, e1) =>
            {
                if (this._tableSourceCode.Length > 0 && this._tableDestinationCode.Length > 0)
                {
                    //try
                    //{
                    //    object __data = this._itemGridSource._rowData[e1._row];
                    //    this._itemGridDestination._rowData[this._itemGridDestination._addRow()] = __data;
                    //    this._itemGridDestination.Invalidate();
                    //    this._itemGridSource._rowData.RemoveAt(e1._row);
                    //    this._itemGridSource.Invalidate();
                    //}
                    //catch
                    //{
                    //}
                    _moveOrderOut(e1._row);
                }
            };
            this._itemGridDestination._mouseDoubleClick += (s1, e1) =>
            {
                if (this._tableSourceCode.Length > 0 && this._tableDestinationCode.Length > 0)
                {
                    //try
                    //{
                    //    object __data = this._itemGridDestination._rowData[e1._row];
                    //    this._itemGridSource._rowData[this._itemGridSource._addRow()] = __data;
                    //    this._itemGridSource.Invalidate();
                    //    this._itemGridDestination._rowData.RemoveAt(e1._row);
                    //    this._itemGridDestination.Invalidate();
                    //}
                    //catch
                    //{
                    //}
                    _moveOrderIn(e1._row);
                }
            };
        }

        DataTable _loadOrder(string tableNumber, string trans_guid, _itemGridControl grid)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(
                _g.d.table_order._doc_no,
                _g.d.table_order._doc_date + " as old_doc_date ",
                _g.d.table_order._doc_time + " as old_doc_time ",
                _g.d.table_order._item_code,
                "case when  length(" + _g.d.table_order._as_item_name + ") > 0  then " + _g.d.table_order._as_item_name + " else  (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.table_order._item_code + ") end as " + _g.d.table_order._item_name,
                _g.d.table_order._barcode,
                _g.d.table_order._price,
                _g.d.table_order._qty,
                _g.d.table_order._unit_code,
                _g.d.table_order._as_item_name,
                _g.d.table_order._qty_send, _g.d.table_order._qty_cancel, _g.d.table_order._qty_balance, _g.d.table_order._sum_amount, _g.d.table_order._remark, _g.d.table_order._kitchen_code, _g.d.table_order._guid_line, _g.d.table_order._table_number + " as " + _g.d.ic_inventory._code_old) +
                " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,1) order by " + _g.d.table_order._doc_no + "," + _g.d.table_order._line_number)); // and " + _g.d.table_order._trans_guid + " = \'" + trans_guid + "\'
            __myquery.Append("</node>");
            string __debug_query = __myquery.ToString();
            ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

            DataTable __result = ((DataSet)__data[0]).Tables[0];
            grid._loadFromDataTable(__result);

            return __result;
        }

        private void _moveOutAllButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการย้ายไปทั้งหมดหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this._tableSourceCode.Length > 0 && this._tableDestinationCode.Length > 0)
                {
                    try
                    {
                        while (this._itemGridSource._rowData.Count > 0)
                        {
                            this._itemGridDestination._rowData[this._itemGridDestination._addRow()] = this._itemGridSource._rowData[0];
                            this._itemGridSource._rowData.RemoveAt(0);
                        }
                        this._itemGridDestination.Invalidate();
                        this._itemGridSource.Invalidate();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    MessageBox.Show("ยังไม่ได้เลือกโต๊ะ ต้นทางและปลายทาง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _moveOrderOut(int row)
        {
            try
            {
                decimal __qty = MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(row, _g.d.table_order._qty).ToString());
                Boolean __moveAll = true;
                decimal __moveQty = 0M;
                if (__qty > 1)
                {
                    if (MessageBox.Show("ต้องการย้ายจำนวนทั้งหมดหรือไม่", "ย้ายจำนวน " + __qty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        __moveAll = false;
                        _tableOpenControl __openControl = new _tableOpenControl();
                        __openControl.Text = "ย้ายบางส่วน";
                        __openControl._myLabel1.Text = "ย้ายจำนวน :";
                        __openControl._myLabel1.ResourceName = "ย้ายจำนวน :";
                        __openControl.ShowDialog();
                        if (__openControl.DialogResult == DialogResult.OK)
                        {
                            __moveQty = MyLib._myGlobal._intPhase(__openControl._customerCountTextbox.Text);

                            if (__moveQty > __qty || __moveQty <= 0)
                            {
                                MessageBox.Show("ป้อนจำนวนผิด");
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                }

                if (__moveAll == true)
                {
                    object __data = this._itemGridSource._rowData[row];

                    int __addrRowAdd = this._itemGridDestination._addRow();
                    this._itemGridDestination._rowData[__addrRowAdd] = __data;
                    this._itemGridDestination.Invalidate();
                    this._itemGridSource._rowData.RemoveAt(row);
                    this._itemGridSource.Invalidate();
                }
                else
                {
                    // ย้ายจำนวนบางส่วน
                    object __data = this._itemGridSource._rowData[row];

                    int __addrRowAdd = this._itemGridDestination._addRow();
                    this._itemGridDestination._rowData[__addrRowAdd] = ((ArrayList)__data).Clone();
                    this._itemGridDestination.Invalidate();
                    //this._itemGridSource._rowData.RemoveAt(row);
                    decimal __balance_qty = (__qty - __moveQty);
                    // update จำนวนเดิม
                    this._itemGridSource._cellUpdate(row, _g.d.table_order._qty, (__qty - __moveQty), true);
                    this._itemGridSource.Invalidate();
                    this._itemGridSource.calcSumAmountBalanceQtyLine(row);

                    // ของใหม่ update จำนวนใหม่, guid_line = ""
                    this._itemGridDestination._cellUpdate(__addrRowAdd, _g.d.table_order._qty, __moveQty, true);
                    this._itemGridDestination._cellUpdate(__addrRowAdd, _g.d.table_order._guid_line, "", true);
                    this._itemGridDestination.calcSumAmountBalanceQtyLine(__addrRowAdd);
                }

                // check move  กรณีย้ายกลับมา จะต้องคืนค่า
                /*string __old_doc_no = this._itemGridDestination._cellGet(__addrRowAdd, _g.d.table_order._old_doc_no).ToString();
                string __doc_no = this._itemGridDestination._cellGet(__addrRowAdd, _g.d.table_order._doc_no).ToString();

                if (__old_doc_no.Length > 0 && __doc_no.Length == 0)
                {
                    // กรณีย้ายกลับ
                    this._itemGridDestination._cellUpdate(__addrRowAdd, _g.d.table_order._doc_no, __old_doc_no, true);
                    this._itemGridDestination._cellUpdate(__addrRowAdd, _g.d.table_order._old_doc_no, "", true);
                }
                else
                {
                    this._itemGridDestination._cellUpdate(__addrRowAdd, _g.d.table_order._old_doc_no, __doc_no, true);
                    this._itemGridDestination._cellUpdate(__addrRowAdd, _g.d.table_order._doc_no, "", true);
                }*/

            }
            catch
            {
            }
        }

        void _moveOrderIn(int row)
        {
            try
            {
                decimal __qty = MyLib._myGlobal._decimalPhase(this._itemGridDestination._cellGet(row, _g.d.table_order._qty).ToString());
                Boolean __moveAll = true;
                decimal __moveQty = 0M;
                if (__qty > 1)
                {
                    if (MessageBox.Show("ต้องการย้ายจำนวนทั้งหมดหรือไม่", "ย้ายจำนวน " + __qty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        __moveAll = false;
                        _tableOpenControl __openControl = new _tableOpenControl();
                        __openControl.Text = "ย้ายบางส่วน";
                        __openControl._myLabel1.Text = "ย้ายจำนวน :";
                        __openControl._myLabel1.ResourceName = "ย้ายจำนวน :";
                        __openControl.ShowDialog();
                        if (__openControl.DialogResult == DialogResult.OK)
                        {
                            __moveQty = MyLib._myGlobal._intPhase(__openControl._customerCountTextbox.Text);
                            if (__moveQty > __qty || __moveQty <= 0)
                            {
                                MessageBox.Show("ป้อนจำนวนผิด");
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                }

                if (__moveAll == true)
                {
                    object __data = this._itemGridDestination._rowData[row];

                    int __addrRowAdd = this._itemGridSource._addRow(); //this._itemGridDestination._addRow();

                    this._itemGridSource._rowData[__addrRowAdd] = __data;
                    this._itemGridSource.Invalidate();
                    this._itemGridDestination._rowData.RemoveAt(row);
                    this._itemGridDestination.Invalidate();

                }
                else
                {
                    // ย้ายจำนวนบางส่วน
                    object __data = this._itemGridDestination._rowData[row];
                    int __addrRowAdd = this._itemGridSource._addRow(); //this._itemGridDestination._addRow();
                    this._itemGridSource._rowData[__addrRowAdd] = ((ArrayList)__data).Clone();
                    this._itemGridSource.Invalidate();
                    //this._itemGridSource._rowData.RemoveAt(row);
                    decimal __balance_qty = (__qty - __moveQty);

                    // update จำนวนเดิม
                    this._itemGridDestination._cellUpdate(row, _g.d.table_order._qty, (__qty - __moveQty), true);
                    this._itemGridDestination.Invalidate();
                    this._itemGridDestination.calcSumAmountBalanceQtyLine(row);

                    // ของใหม่ update จำนวนใหม่, guid_line = ""
                    this._itemGridSource._cellUpdate(__addrRowAdd, _g.d.table_order._qty, __moveQty, true);
                    this._itemGridSource._cellUpdate(__addrRowAdd, _g.d.table_order._guid_line, "", true);
                    this._itemGridSource.calcSumAmountBalanceQtyLine(__addrRowAdd);
                }

                // check move  กรณีย้ายกลับมา จะต้องคืนค่า
                /*string __old_doc_no = this._itemGridSource._cellGet(__addrRowAdd, _g.d.table_order._old_doc_no).ToString();
                string __doc_no = this._itemGridSource._cellGet(__addrRowAdd, _g.d.table_order._doc_no).ToString();

                if (__old_doc_no.Length > 0 && __doc_no.Length == 0)
                {
                    this._itemGridSource._cellUpdate(__addrRowAdd, _g.d.table_order._doc_no, __old_doc_no, true);
                    this._itemGridSource._cellUpdate(__addrRowAdd, _g.d.table_order._old_doc_no, "", true);
                }
                else
                {
                    this._itemGridSource._cellUpdate(__addrRowAdd, _g.d.table_order._old_doc_no, __doc_no, true);
                    this._itemGridSource._cellUpdate(__addrRowAdd, _g.d.table_order._doc_no, "", true);
                }*/
            }
            catch
            {
            }
        }

        private void _moveOutButton_Click(object sender, EventArgs e)
        {
            if (this._tableSourceCode.Length > 0 && this._tableDestinationCode.Length > 0)
            {

                //try
                //{
                //    object __data = this._itemGridSource._rowData[this._itemGridSource._selectRow];
                //    this._itemGridDestination._rowData[this._itemGridDestination._addRow()] = __data;
                //    this._itemGridDestination.Invalidate();
                //    this._itemGridSource._rowData.RemoveAt(this._itemGridSource._selectRow);
                //    this._itemGridSource.Invalidate();
                //}
                //catch
                //{
                //}
                _moveOrderOut(this._itemGridSource._selectRow);
            }
            else
            {
                MessageBox.Show("ยังไม่ได้เลือกโต๊ะ ต้นทางและปลายทาง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _moveInAllButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการย้ายกลับทั้งหมดหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this._tableSourceCode.Length > 0 && this._tableDestinationCode.Length > 0)
                {
                    try
                    {
                        while (this._itemGridDestination._rowData.Count > 0)
                        {
                            this._itemGridSource._rowData[this._itemGridSource._addRow()] = this._itemGridDestination._rowData[0];
                            this._itemGridDestination._rowData.RemoveAt(0);
                        }
                        this._itemGridDestination.Invalidate();
                        this._itemGridSource.Invalidate();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    MessageBox.Show("ยังไม่ได้เลือกโต๊ะ ต้นทางและปลายทาง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _moveInButton_Click(object sender, EventArgs e)
        {
            if (this._tableSourceCode.Length > 0 && this._tableDestinationCode.Length > 0)
            {

                //try
                //{
                //    object __data = this._itemGridDestination._rowData[this._itemGridDestination._selectRow];
                //    this._itemGridSource._rowData[this._itemGridSource._addRow()] = __data;
                //    this._itemGridDestination.Invalidate();
                //    this._itemGridDestination._rowData.RemoveAt(this._itemGridDestination._selectRow);
                //    this._itemGridSource.Invalidate();
                //}
                //catch
                //{
                //}
                _moveOrderIn(this._itemGridDestination._selectRow);
            }
            else
            {
                MessageBox.Show("ยังไม่ได้เลือกโต๊ะ ต้นทางและปลายทาง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ต้องการบันทึกจริงหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                //string __transGuid = Guid.NewGuid().ToString();

                //// 1. ถ้าฝั่งไหนไม่มีรายการ ปิด โต๊ะ และ update สถานะ ฝั้งนั้นไปเลย
                //if (this._itemGridSource._rowData.Count == 0)
                //{
                //    // ปิดโต๊ะฝั่งซ้าย
                //}
                //else
                //{
                //    // ถ้ามีรายการมา ให้เช็คดุว่าเป็นโต๊ะ ที่ย้ายมาหรือเปล่า (ยังไม่ได้เปิดโต๊ะ) โดย check จาก trans_guid
                //}

                // toe get temp มา check ทำ log


                if (this._itemGridSource._rowData.Count == 0 || this._itemGridDestination._rowData.Count == 0)
                {
                    // ย้ายบางรายการ หรือ รวมโต๊ะ update all ได้เลย
                    List<string> __docNoList = new List<string>();
                    if (this._itemGridSource._rowData.Count > 0)
                    {
                        for (int __row = 0; __row < this._itemGridSource._rowData.Count; __row++)
                        {
                            string __order_doc_no = this._itemGridSource._cellGet(__row, _g.d.table_order._doc_no).ToString();
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._table_number + "=\'" + this._tableSourceCode + "\', " + _g.d.table_order._trans_guid + "=\'" + this._tableSourceTransGuidCode + "\' where " + _g.d.table_order._guid_line + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._guid_line).ToString() + "\'"));

                            if (__docNoList.IndexOf(__order_doc_no) == -1)
                            {
                                __docNoList.Add(__order_doc_no);
                            }
                        }

                        if (__docNoList.Count > 0)
                        {
                            StringBuilder __packDocNoList = new StringBuilder();
                            foreach (string __getDocNo in __docNoList)
                            {
                                if (__packDocNoList.Length > 0)
                                    __packDocNoList.Append(",");

                                __packDocNoList.Append("\'" + __getDocNo + "\'");

                            }
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order_doc._table + " set " + _g.d.table_order_doc._table_number + "=\'" + this._tableSourceCode + "\', " + _g.d.table_order_doc._trans_guid + "=\'" + this._tableSourceTransGuidCode + "\' where doc_no in (" + __packDocNoList.ToString() + ")"));

                        }

                    }
                    else
                    {
                        for (int __row = 0; __row < this._itemGridDestination._rowData.Count; __row++)
                        {
                            string __order_doc_no = this._itemGridDestination._cellGet(__row, _g.d.table_order._doc_no).ToString();
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._table_number + "=\'" + this._tableDestinationCode + "\', " + _g.d.table_order._trans_guid + "=\'" + this._tableDestinationTransGuidCode + "\' where " + _g.d.table_order._guid_line + "=\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._guid_line).ToString() + "\'"));

                            if (__docNoList.IndexOf(__order_doc_no) == -1)
                            {
                                __docNoList.Add(__order_doc_no);
                            }
                        }

                        if (__docNoList.Count > 0)
                        {
                            StringBuilder __packDocNoList = new StringBuilder();
                            foreach (string __getDocNo in __docNoList)
                            {
                                if (__packDocNoList.Length > 0)
                                    __packDocNoList.Append(",");

                                __packDocNoList.Append("\'" + __getDocNo + "\'");
                            }
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order_doc._table + " set " + _g.d.table_order_doc._table_number + "=\'" + this._tableDestinationCode + "\', " + _g.d.table_order_doc._trans_guid + "=\'" + this._tableDestinationTransGuidCode + "\' where doc_no in (" + __packDocNoList.ToString() + ")"));

                        }
                    }

                }
                else
                {
                    // concept 
                    // กรณีแยกโต๊ะ 
                    // - หากย้ายบางส่วนของบิลจะ new doc ฝั่งปลายทางเป็นหลัก

                    // แยกโต๊ะ และ อื่น ๆ

                    // grid ต้นทาง
                    // check order ที่ move มา
                    //bool __is_new_orderDoc = false;
                    //decimal __new_qty = 0M;
                    //decimal __new_amount = 0M;

                    string __new_doc_no = "";
                    string __new_doc_date = "";
                    string __new_doc_time = "";
                    /*
                    for (int __row = 0; __row < this._itemGridSource._rowData.Count; __row++)
                    {
                        string __get_old_tableName = this._itemGridSource._cellGet(__row, _g.d.ic_inventory._code_old).ToString();
                        decimal __getAmount = (decimal)this._itemGridSource._cellGet(__row, _g.d.table_order._sum_amount);
                        decimal __getQty = (decimal)this._itemGridSource._cellGet(__row, _g.d.table_order._qty);

                        if (__get_old_tableName.Equals(this._tableSourceCode) == false)
                        {
                            __is_new_orderDoc = true;

                            // sum จำนวน และ ราคา
                            __new_qty += __getQty;
                            __new_amount += __getAmount;

                        }

                    }

                    if (__is_new_orderDoc == true)
                    {
                        // gen new doc_no

                    }*/

                    for (int __row = 0; __row < this._itemGridSource._rowData.Count; __row++)
                    {
                        string __get_old_tableName = this._itemGridSource._cellGet(__row, _g.d.ic_inventory._code_old).ToString();
                        string __guid_line = this._itemGridSource._cellGet(__row, _g.d.table_order._guid_line).ToString();

                        /*if (__get_old_tableName.Equals(this._tableSourceCode) == false)
                        {
                            // กรณีเป็นรายการที่แทรกเข้ามาใหม่

                            // มี 2 choice 
                            // 1 ยกเลิกของเก่า แล้วใส่เข้าไป order ใหม่
                            // 2 update เลย

                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " +
                                _g.d.table_order._table_number + "=\'" + this._tableSourceCode + "\'" + ", " +
                                _g.d.table_order._trans_guid + "=\'" + this._tableSourceTransGuidCode + "\' " + ", " +

                                // จำนวน จำนวนคงเหลือ  ยอดรวม

                                _g.d.table_order._qty + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._qty).ToString() + "\' " + ", " +
                                _g.d.table_order._qty_balance + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._qty_balance).ToString() + "\' " + ", " +
                                _g.d.table_order._sum_amount + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._sum_amount).ToString() + "\' " +

                                " where " + _g.d.table_order._guid_line + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._guid_line).ToString() + "\'"));

                            // ใส่ log move  order จาก ไหน ไปไหน จำนวนเท่าไหร่

                        }
                        else
                        {
                        }*/

                        if (__guid_line != "")
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " +
                                _g.d.table_order._table_number + "=\'" + this._tableSourceCode + "\', " +
                                _g.d.table_order._trans_guid + "=\'" + this._tableSourceTransGuidCode + "\'," +
                                // จำนวน จำนวนคงเหลือ  ยอดรวม

                                _g.d.table_order._qty + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._qty).ToString() + "\' " + ", " +
                                _g.d.table_order._qty_balance + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._qty_balance).ToString() + "\' " + ", " +
                                _g.d.table_order._sum_amount + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._sum_amount).ToString() + "\' " +

                                " where " + _g.d.table_order._guid_line + "=\'" + __guid_line + "\'"));
                        }
                        else
                        {
                            //insert new line จากการย้ายบางจำนวน
                            //__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert " + _g.d.table_order._table + " set " + _g.d.table_order._table_number + "=\'" + this._tableSourceCode + "\', " + _g.d.table_order._trans_guid + "=\'" + this._tableSourceTransGuidCode + "\' where " + _g.d.table_order._guid_line + "=\'" + __guid_line + "\'"));

                            // ใส่ log move  order จาก ไหน ไปไหน จำนวนเท่าไหร่
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

                            string __guid_line_2 = Guid.NewGuid().ToString();
                            int __is_item_checker = (int)this._itemGridSource._cellGet(__row, _g.d.ic_inventory._barcode_checker_print);
                            int __print_per_unit = (int)this._itemGridSource._cellGet(__row, _g.d.ic_inventory._print_order_per_unit);

                            string __as_item_name = (this._itemGridDestination._cellGet(__row, _g.d.table_order._as_item_name)) == null ? "" : this._itemGridSource._cellGet(__row, _g.d.table_order._as_item_name).ToString();

                            // order_type 1=ลงโต๊ะ,2=กลับบ้าน, 3=develiry
                            string __valueList = MyLib._myGlobal._fieldAndComma(
                                "\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._doc_no).ToString() + "\'",
                                "\'" + MyLib._myGlobal._convertDateToQuery((DateTime)this._itemGridSource._cellGet(__row, "old_doc_date")) + "\'",
                                "\'" + this._itemGridSource._cellGet(__row, "old_doc_time").ToString() + "\'",
                                (__row + 1).ToString(),
                                "\'" + this._tableSourceCode + "\'",
                                "\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._item_code).ToString() + "\'",
                                "\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._barcode).ToString() + "\'",
                                this._itemGridSource._cellGet(__row, _g.d.table_order._price).ToString(),
                                this._itemGridSource._cellGet(__row, _g.d.table_order._qty).ToString(),
                                "0",
                                "0",
                                this._itemGridSource._cellGet(__row, _g.d.table_order._qty).ToString(),
                                "\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._unit_code).ToString() + "\'",
                                this._itemGridSource._cellGet(__row, _g.d.table_order._sum_amount).ToString(),
                                //"0", toe mark qty_send ซ้ำ
                                "1", // order_type
                                "\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._remark).ToString() + "\'",
                                "\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._kitchen_code).ToString() + "\'",
                                "\'" + __guid_line_2 + "\'",
                                "0",
                                "\'" + this._tableSourceTransGuidCode + "\'",
                                "\'" + __as_item_name + "\'", //  this._itemGridSource._cellGet(__row, _g.d.table_order._as_item_name).ToString()
                                "\'" + "\'", // this._itemGridSource._cellGet(__row, _g.d.table_order._confirm_guid).ToString()
                                "\'\'"
                                );
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order._table + " (" + __fieldList + ") values(" + __valueList + ")"));
                        }

                    }


                    // move รายการไป
                    // check ฝั่งปลายทาง
                    for (int __row = 0; __row < this._itemGridDestination._rowData.Count; __row++)
                    {
                        string __guid_line = this._itemGridDestination._cellGet(__row, _g.d.table_order._guid_line).ToString();

                        if (__guid_line != "")
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " +
                                _g.d.table_order._table_number + "=\'" + this._tableDestinationCode + "\', " +
                                _g.d.table_order._trans_guid + "=\'" + this._tableDestinationTransGuidCode + "\', " +
                                _g.d.table_order._qty + "=\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._qty).ToString() + "\' " + ", " +
                                _g.d.table_order._qty_balance + "=\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._qty_balance).ToString() + "\' " + ", " +
                                _g.d.table_order._sum_amount + "=\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._sum_amount).ToString() + "\' " +

                                " where " + _g.d.table_order._guid_line + "=\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._guid_line).ToString() + "\'"));
                        }
                        else
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

                            string __guid_line_2 = Guid.NewGuid().ToString();
                            int __is_item_checker = (int)this._itemGridDestination._cellGet(__row, _g.d.ic_inventory._barcode_checker_print);
                            int __print_per_unit = (int)this._itemGridDestination._cellGet(__row, _g.d.ic_inventory._print_order_per_unit);

                            string __as_item_name = (this._itemGridDestination._cellGet(__row, _g.d.table_order._as_item_name)) == null ? "" : this._itemGridDestination._cellGet(__row, _g.d.table_order._as_item_name).ToString();

                            // order_type 1=ลงโต๊ะ,2=กลับบ้าน, 3=develiry
                            string __valueList = MyLib._myGlobal._fieldAndComma(
                                "\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._doc_no).ToString() + "\'",
                                "\'" + MyLib._myGlobal._convertDateToQuery((DateTime)this._itemGridDestination._cellGet(__row, "old_doc_date")) + "\'",
                                "\'" + this._itemGridDestination._cellGet(__row, "old_doc_time").ToString() + "\'",
                                (__row + 1).ToString(),
                                "\'" + this._tableDestinationCode + "\'",
                                "\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._item_code).ToString() + "\'",
                                "\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._barcode).ToString() + "\'",
                                this._itemGridDestination._cellGet(__row, _g.d.table_order._price).ToString(),
                                this._itemGridDestination._cellGet(__row, _g.d.table_order._qty).ToString(),
                                "0",
                                "0",
                                this._itemGridDestination._cellGet(__row, _g.d.table_order._qty).ToString(),
                                "\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._unit_code).ToString() + "\'",
                                this._itemGridDestination._cellGet(__row, _g.d.table_order._sum_amount).ToString(),
                                //"0", toe mark qty_send ซ้ำ
                                "1", // order_type
                                "\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._remark).ToString() + "\'",
                                "\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._kitchen_code).ToString() + "\'",
                                "\'" + __guid_line_2 + "\'",
                                "0",
                                "\'" + this._tableDestinationTransGuidCode + "\'",
                                "\'" + __as_item_name + "\'", //  this._itemGridDestination._cellGet(__row, _g.d.table_order._as_item_name).ToString()
                                "\'\'", //"\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._confirm_guid).ToString() + "\'",
                                "\'\'"
                                );
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order._table + " (" + __fieldList + ") values(" + __valueList + ")"));
                        }
                    }

                }


                // get temp compare insert table_order_doc
                // source grid
                for (int orderRow = 0; orderRow < this._sourceTempDataTable.Rows.Count; orderRow++)
                {
                    string __guidLine = this._sourceTempDataTable.Rows[orderRow][_g.d.table_order._guid_line].ToString();
                    string __itemCode = this._sourceTempDataTable.Rows[orderRow][_g.d.table_order._item_code].ToString();
                    decimal __qty = MyLib._myGlobal._decimalPhase(this._sourceTempDataTable.Rows[orderRow][_g.d.table_order._qty_balance].ToString());
                    decimal __price =  MyLib._myGlobal._decimalPhase(this._sourceTempDataTable.Rows[orderRow][_g.d.table_order._price].ToString());
                    bool __found = false;

                    for (int row = 0; row < this._itemGridSource._rowData.Count; row++)
                    {
                        string getGuidLine = this._itemGridSource._cellGet(row, _g.d.table_order._guid_line).ToString();
                        string getItemCode = this._itemGridSource._cellGet(row, _g.d.table_order._item_code).ToString();

                        decimal getQty = MyLib._myGlobal._decimalPhase(this._itemGridSource._cellGet(row, _g.d.table_order._qty_balance).ToString());

                        if (getGuidLine == __guidLine && __itemCode == getItemCode)
                        {
                            if (__qty == getQty)
                            {
                                __found = true;
                            }
                            else
                            {
                                __found = false;
                                __qty -= getQty;
                            }
                        }
                    }

                    if (__found == false)
                    {
                        // insert order cancel log
                        string fieldInsert = _g.d.table_order_cancel._table_number + "," + _g.d.table_order_cancel._guid_line + ", " + _g.d.table_order_cancel._item_code + ", " + _g.d.table_order_cancel._unit_code + "," + _g.d.table_order_cancel._qty + "," + _g.d.table_order_cancel._price + "," + _g.d.table_order_cancel._sum_amount + "," + _g.d.table_order_cancel._doc_date + "," + _g.d.table_order_cancel._doc_time + "," + _g.d.table_order_cancel._sale_code + "," + _g.d.table_order_cancel._cancel_flag + "," + _g.d.table_order_cancel._table_number_2;
                        string valueInsert = "\'" + this._tableSourceCode + "\', \'" + __guidLine + "\', \'" + __itemCode + "\', \'" + this._sourceTempDataTable.Rows[orderRow][_g.d.table_order._unit_code].ToString() + "\', " + __qty + "," + __price + "," + (__qty * __price) + ",\'" + this._sourceTempDataTable.Rows[orderRow]["old_doc_date"].ToString() + "\',\'" + this._sourceTempDataTable.Rows[orderRow]["old_doc_time"].ToString() + "\',\'" + this._saleCode + "\', 1,\'" + this._tableDestinationCode+ "\'";
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_cancel._table + " (" + fieldInsert + ") values (" + valueInsert + ") "));

                    }

                }

                // source grid
                for (int orderRow = 0; orderRow < this._desinationTempDataTable.Rows.Count; orderRow++)
                {
                    string __guidLine = this._desinationTempDataTable.Rows[orderRow][_g.d.table_order._guid_line].ToString();
                    string __itemCode = this._desinationTempDataTable.Rows[orderRow][_g.d.table_order._item_code].ToString();
                    decimal __qty = MyLib._myGlobal._decimalPhase(this._desinationTempDataTable.Rows[orderRow][_g.d.table_order._qty_balance].ToString());
                    decimal __price = MyLib._myGlobal._decimalPhase(this._desinationTempDataTable.Rows[orderRow][_g.d.table_order._price].ToString());
                    bool __found = false;

                    for (int row = 0; row < this._itemGridDestination._rowData.Count; row++)
                    {
                        string getGuidLine = this._itemGridDestination._cellGet(row, _g.d.table_order._guid_line).ToString();
                        string getItemCode = this._itemGridDestination._cellGet(row, _g.d.table_order._item_code).ToString();

                        decimal getQty = MyLib._myGlobal._decimalPhase(this._itemGridDestination._cellGet(row, _g.d.table_order._qty_balance).ToString());

                        if (getGuidLine == __guidLine && __itemCode == getItemCode)
                        {
                            if (__qty == getQty)
                            {
                                __found = true;
                                break;
                            }
                            else
                            {
                                __found = false;
                                __qty -= getQty;
                                break;
                            }
                        }
                    }

                    if (__found == false)
                    {
                        // insert order cancel log
                        string fieldInsert = _g.d.table_order_cancel._table_number + "," + _g.d.table_order_cancel._guid_line + ", " + _g.d.table_order_cancel._item_code + ", " + _g.d.table_order_cancel._unit_code + "," + _g.d.table_order_cancel._qty + "," + _g.d.table_order_cancel._price + "," + _g.d.table_order_cancel._sum_amount + "," + _g.d.table_order_cancel._doc_date + "," + _g.d.table_order_cancel._doc_time + "," + _g.d.table_order_cancel._sale_code + "," + _g.d.table_order_cancel._cancel_flag + "," + _g.d.table_order_cancel._table_number_2;
                        string valueInsert = "\'" + this._tableDestinationCode + "\', \'" + __guidLine + "\', \'" + __itemCode + "\', \'" + this._desinationTempDataTable.Rows[orderRow][_g.d.table_order._unit_code].ToString() + "\', " + __qty + "," + __price + "," + (__qty * __price) + ",\'" + this._desinationTempDataTable.Rows[orderRow]["old_doc_date"].ToString() + "\',\'" + this._desinationTempDataTable.Rows[orderRow]["old_doc_time"].ToString() + "\',\'" + this._saleCode + "\', 1,\'" + this._tableSourceCode + "\'";
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_cancel._table + " (" + fieldInsert + ") values (" + valueInsert + ") "));

                    }

                }

                // move log
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + ") values (now(),\'Move Table\',\'Table Number : " + this._tableSourceCode + ", Table Number : " + this._tableDestinationCode + "\')"));
                __myQuery.Append("</node>");

                string __debugQuery = __myQuery.ToString();

                // old
                // move รายการไป                  
                // check ฝั่งต้นทางก่อน

                /*
                for (int __row = 0; __row < this._itemGridSource._rowData.Count; __row++)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._table_number + "=\'" + this._tableSourceCode + "\' where " + _g.d.table_order._guid_line + "=\'" + this._itemGridSource._cellGet(__row, _g.d.table_order._guid_line).ToString() + "\'"));
                }


                // move รายการไป
                // check ฝั่งปลายทาง
                for (int __row = 0; __row < this._itemGridDestination._rowData.Count; __row++)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._table_number + "=\'" + this._tableDestinationCode + "\' where " + _g.d.table_order._guid_line + "=\'" + this._itemGridDestination._cellGet(__row, _g.d.table_order._guid_line).ToString() + "\'"));
                }




                __myQuery.Append("</node>");
                 * */
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลสำเร็จ"), MyLib._myGlobal._resource("สำเร็จ"));

                    if (this._saveSuccess != null)
                    {
                        this._saveSuccess(this);
                    }
                }
                else
                {
                    MessageBox.Show(__result.ToString());
                }
            }
        }
    }
}
