using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SMLPosClient
{
    public partial class _printVatFull : Form
    {
        private string _userCode;
        private string _password;
        private SMLPOSControl._posScreenConfig _posScreenConfig;
        public event printBillFullSuccessEventHandler _billSuccess;
        string _docNo = "";
        string _docNoFormat = "";
        string _startRunningNumber = "";

        /// <summary>0=ออกใหม่,1=reprint</summary>
        int _mode = 0;

        public _printVatFull(SMLPOSControl._posScreenConfig posScreenConfig, string docNoFormat, string userCode, string password, int mode, string startRunningNumber)
        {
            InitializeComponent();
            this._mode = mode;
            this._docNoFormat = docNoFormat;
            this._userCode = userCode;
            this._password = password;
            this._posScreenConfig = posScreenConfig;
            this._startRunningNumber = startRunningNumber;

            // toe 
            if (this._mode == 1)
            {
                this._docNoLabel.Text = "เลขที่ใบกำกับภาษีเต็มรูปแบบ :";
                this._buttonCustSearch.Enabled = false;
                this._buttonCustAdd.Text = "แก้ไขข้อมูลลูกค้า (F3)";
            }

            this._textBoxBillNo.KeyUp += new KeyEventHandler(_textBoxBillNo_KeyUp);
            this._textBoxCustCode.TextChanged += new EventHandler(_textBoxCustCode_TextChanged);
        }

        void _newCust()
        {
            if (this._mode == 0)
            {
                _newCustForm __cust = new _newCustForm(0, "");
                __cust._insertSuccess += (code) =>
                {
                    __cust.Close();
                    this._textBoxCustCode.Text = code;
                };
                __cust.ShowDialog();
            }
            else
            {
                if (this._textBoxCustCode.Text.Length > 0)
                {
                    _newCustForm __cust = new _newCustForm(1, this._textBoxCustCode.Text);
                    __cust._insertSuccess += (code) =>
                    {
                        __cust.Close();
                        this._textBoxCustCode.Text = code;
                        _textBoxCustCode_TextChanged(__cust, null);
                    };
                    __cust.ShowDialog();
                }
                else
                {
                    MessageBox.Show("กรุณาป้อน เลขที่ใบกำกับภาษีเต็มรูปแบบ ก่อน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        void _searchCust()
        {
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            __search.Text = MyLib._myGlobal._resource("ค้นหาลูกค้า");
            __search._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);
            __search.WindowState = FormWindowState.Maximized;
            __search._dataList._gridData._mouseClick += (s1, e1) =>
            {
                this._textBoxCustCode.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                __search.Close();
                __search.Dispose();
            };
            __search._searchEnterKeyPress += (s1, e1) =>
            {
                this._textBoxCustCode.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                __search.Close();
                __search.Dispose();
            };
            __search.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    this._searchCust();
                    return true;
                case Keys.F3:
                    this._newCust();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _textBoxCustCode_TextChanged(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __dataTable = __myFrameWork._queryShort("select " + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + this._textBoxCustCode.Text.Trim().ToUpper() + "\'").Tables[0];
            if (__dataTable.Rows.Count > 0)
            {
                this._textBoxCustName.Text = __dataTable.Rows[0][_g.d.ar_customer._name_1].ToString();
                this._textBoxCustAddress.Text = __dataTable.Rows[0][_g.d.ar_customer._address].ToString();
            }
            else
            {
                this._textBoxCustName.Text = "";
                this._textBoxCustAddress.Text = "";
            }
        }

        void _textBoxBillNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                _check();
            }
        }

        void _check()
        {
            _check(true);
        }

        void _check(bool updateCust)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __docNoRef = this._textBoxBillNo.Text.Trim().ToUpper();
            if (__docNoRef.Length > 0)
            {
                if (this._mode == 0)
                {
                    DataTable __docDataTable = __myFrameWork._queryShort("select * from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no_guid) + "=\'" + __docNoRef + "\' or " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNoRef + "\'").Tables[0];
                    if (__docDataTable.Rows.Count > 0)
                    {
                        string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                        int __lastStatus = (int)MyLib._myGlobal._decimalPhase(__docDataTable.Rows[0][_g.d.ic_trans._last_status].ToString());
                        if (__lastStatus == 1)
                        {
                            MessageBox.Show("เอกสาร" + " [" + __docNo + "] " + "ยกเลิกไปแล้ว");
                            this._textBoxBillNo.Text = "";
                            this._docNo = "";
                            this._textBoxCustCode.Text = "";
                        }
                        else
                        {
                            DataTable __refDocDataTable = __myFrameWork._queryShort("select * from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_ref) + "=\'" + __docNo + "\'").Tables[0];
                            if (__refDocDataTable.Rows.Count > 0)
                            {
                                MessageBox.Show("เอกสาร" + " [" + __docNo + "] " + "อ้างอิงไปแล้ว");
                                this._textBoxBillNo.Text = "";
                                this._docNo = "";
                                this._textBoxCustCode.Text = "";
                            }
                            else
                            {
                                DataTable __dataTable = __myFrameWork._queryShort("select " + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._cust_code + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNo + "\' and is_pos = 1 and " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag( _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) ).Tables[0];
                                if (__dataTable.Rows.Count > 0)
                                {
                                    this._docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                                    if (updateCust == true)
                                    {
                                        this._textBoxCustCode.Text = __docDataTable.Rows[0][_g.d.ic_trans._cust_code].ToString();
                                    }
                                }
                                else
                                {
                                    this._textBoxBillNo.Text = "";
                                    this._docNo = "";
                                    this._textBoxCustCode.Text = "";
                                    MessageBox.Show("ไม่พบเอกสาร" + " [" + __docNo + "]");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบเอกสาร" + " [" + __docNoRef + "]");
                    }
                }
                else
                {
                    DataTable __docDataTable = __myFrameWork._queryShort("select * from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNoRef + "\' and " + _g.d.ic_trans._doc_ref + " <> \'\' and " + _g.d.ic_trans._doc_ref + " is not null ").Tables[0];
                    if (__docDataTable.Rows.Count > 0)
                    {
                        string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                        int __lastStatus = (int)MyLib._myGlobal._decimalPhase(__docDataTable.Rows[0][_g.d.ic_trans._last_status].ToString());
                        if (__lastStatus == 1)
                        {
                            MessageBox.Show("เอกสาร" + " [" + __docNo + "] " + "ยกเลิกไปแล้ว");
                            this._textBoxBillNo.Text = "";
                            this._docNo = "";
                            this._textBoxCustCode.Text = "";
                        }
                        else
                        {
                            DataTable __refDocDataTable = __myFrameWork._queryShort("select * from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_ref) + "=\'" + __docNo + "\'").Tables[0];
                            if (__refDocDataTable.Rows.Count > 0)
                            {
                                MessageBox.Show("เอกสาร" + " [" + __docNo + "] " + "อ้างอิงไปแล้ว");
                                this._textBoxBillNo.Text = "";
                                this._docNo = "";
                                this._textBoxCustCode.Text = "";
                            }
                            else
                            {
                                DataTable __dataTable = __myFrameWork._queryShort("select " + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._cust_code + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNo + "\'").Tables[0];
                                if (__dataTable.Rows.Count > 0)
                                {
                                    this._docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                                    if (updateCust == true)
                                    {
                                        this._textBoxCustCode.Text = __docDataTable.Rows[0][_g.d.ic_trans._cust_code].ToString();
                                    }
                                }
                                else
                                {
                                    this._textBoxBillNo.Text = "";
                                    this._docNo = "";
                                    this._textBoxCustCode.Text = "";
                                    MessageBox.Show("ไม่พบเอกสาร" + " [" + __docNo + "]");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบเอกสาร" + " [" + __docNoRef + "]");
                    }

                }
                
            }
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            try
            {
                this._check(false);
                if (this._textBoxCustName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("กรุณาบันทึกรายละเอียดลูกค้า");
                }
                else
                {
                    // ตรวจสอบ user,password
                    if (this._userCode.Equals(this._textBoxUserCode.Text) && this._password.Equals(this._textBoxPassword.Text))
                    {
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                        if (this._mode == 0)
                        {
                            string __docNoRef = this._textBoxBillNo.Text.Trim().ToUpper();
                            string __fieldDocRef = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._tax_doc_no, _g.d.ic_trans._vat_rate, _g.d.ic_trans._cashier_code, _g.d.ic_trans._cust_code, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._vat_type, _g.d.ic_trans._last_status, _g.d.ic_trans._pos_id, _g.d.ic_trans._is_pos, _g.d.ic_trans._member_code);
                            string __query = "select " + __fieldDocRef + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no_guid) + "=\'" + __docNoRef + "\' or " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNoRef + "\'"; ;
                            DataTable __docDataTable = __myFrameWork._queryShort(__query).Tables[0];
                            if (__docDataTable.Rows.Count > 0)
                            {
                                string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                                int __lastStatus = (int)MyLib._myGlobal._decimalPhase(__docDataTable.Rows[0][_g.d.ic_trans._last_status].ToString());
                                if (__lastStatus == 1)
                                {
                                    MessageBox.Show("เอกสาร" + " [" + __docNo + "] " + "ยกเลิกไปแล้ว");
                                }
                                else
                                {
                                    DialogResult __ask = MessageBox.Show("ต้องการออกใบกำกับภาษีแบบเต็ม" + " [" + __docNo + "] " + "จริงหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                                    if (__ask == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        StringBuilder __queryList = new StringBuilder();
                                        __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNo.ToUpper().ToString() + "\'"));
                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_detail._doc_no) + "=\'" + __docNo.ToUpper().ToString() + "\' order by " + _g.d.ic_trans_detail._line_number));
                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.cb_trans._doc_no) + "=\'" + __docNo.ToUpper().ToString() + "\'"));
                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_trans_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.cb_trans_detail._doc_no) + "=\'" + __docNo.ToUpper().ToString() + "\' and " + _g.d.cb_trans_detail._doc_type + "=3"));
                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_serial_number._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_serial_number._doc_no) + "=\'" + __docNo.ToUpper().ToString() + "\' order by " + _g.d.ic_trans_serial_number._doc_line_number + "," + _g.d.ic_trans_serial_number._line_number));
                                        __queryList.Append("</node>");

                                        string __debugQuery = __queryList.ToString();
                                        ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());
                                        DataTable __trans = ((DataSet)_getData[0]).Tables[0];
                                        DataTable __transDetail = ((DataSet)_getData[1]).Tables[0];
                                        DataTable __payDetail = ((DataSet)_getData[2]).Tables[0];
                                        DataTable __creditCardDetail = ((DataSet)_getData[3]).Tables[0];
                                        DataTable __serialTrans = ((DataSet)_getData[4]).Tables[0];

                                        // หัวเอกสาร
                                        string __dateFormat = "yyyy-MM-dd";
                                        string __docDateFull = DateTime.Now.ToString(__dateFormat, new CultureInfo("en-US"));
                                        string __docTimeFull = DateTime.Now.ToString("HH:mm");
                                        string __docNoFull = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, this._posScreenConfig._posid, DateTime.Now.ToString(__dateFormat, new CultureInfo("th-TH")), this._docNoFormat, _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน, _g.g._transControlTypeEnum.ว่าง, "", this._startRunningNumber);
                                        string __custCode = this._textBoxCustCode.Text.Trim().ToUpper();
                                        StringBuilder __myQuery = new StringBuilder();
                                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                        // หัวเอกสาร
                                        string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._tax_doc_no, _g.d.ic_trans._doc_ref, _g.d.ic_trans._vat_rate, _g.d.ic_trans._sale_code, _g.d.ic_trans._cashier_code, _g.d.ic_trans._cust_code, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._vat_type, _g.d.ic_trans._last_status, _g.d.ic_trans._discount_word, _g.d.ic_trans._total_discount, _g.d.ic_trans._total_before_vat, _g.d.ic_trans._total_vat_value, _g.d.ic_trans._total_after_vat, _g.d.ic_trans._total_except_vat, _g.d.ic_trans._total_amount, _g.d.ic_trans._total_value, _g.d.ic_trans._pos_id, _g.d.ic_trans._is_pos, _g.d.ic_trans._pos_bill_type, _g.d.ic_trans._pos_bill_change, _g.d.ic_trans._member_code, _g.d.ic_trans._remark, _g.d.ic_trans._branch_code, _g.d.ic_trans._sale_shift_id);
                                        string __value = MyLib._myGlobal._fieldAndComma("\'" + __docDateFull + "\'", "\'" + __docTimeFull + "\'", "\'" + __docNoFull + "\'", "\'" + __docDateFull + "\'", "\'" + __docNoFull + "\'", "\'" + __docNo + "\'", __trans.Rows[0][_g.d.ic_trans._vat_rate].ToString(), "\'" + __trans.Rows[0][_g.d.ic_trans._sale_code].ToString() + "\'", "\'" + this._userCode + "\'", "\'" + __custCode + "\'", _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน).ToString(), "2", "1", "1", "0", "\'" + __trans.Rows[0][_g.d.ic_trans._discount_word].ToString() + "\'", __trans.Rows[0][_g.d.ic_trans._total_discount].ToString(), __trans.Rows[0][_g.d.ic_trans._total_before_vat].ToString(), __trans.Rows[0][_g.d.ic_trans._total_vat_value].ToString(), __trans.Rows[0][_g.d.ic_trans._total_after_vat].ToString(), __trans.Rows[0][_g.d.ic_trans._total_except_vat].ToString(), __trans.Rows[0][_g.d.ic_trans._total_amount].ToString(), __trans.Rows[0][_g.d.ic_trans._total_amount].ToString(), "\'" + this._posScreenConfig._posid + "\'", "1", "0", "0", "\'" + __trans.Rows[0][_g.d.ic_trans._member_code].ToString() + "\'", "\'" + this._textBoxRemark.Text + "\'", "\'" + MyLib._myGlobal._branchCode + "\'", "\'" + _g.g._companyProfile._sale_shift_id + "\'");
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + " (" + __field + ") values (" + __value + ")"));
                                        // รายละเอียดสินค้า
                                        for (int __row = 0; __row < __transDetail.Rows.Count; __row++)
                                        {
                                            string __detailField = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time, _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._cust_code, _g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type, _g.d.ic_trans_detail._calc_flag, _g.d.ic_trans_detail._barcode, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._discount, _g.d.ic_trans_detail._discount_amount, _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._last_status, _g.d.ic_trans_detail._status, _g.d.ic_trans_detail._line_number, _g.d.ic_trans_detail._vat_type, _g.d.ic_trans_detail._tax_type, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, _g.d.ic_trans_detail._item_type, _g.d.ic_trans_detail._stand_value, _g.d.ic_trans_detail._divide_value, _g.d.ic_trans_detail._discount_number, _g.d.ic_trans_detail._price_changed, _g.d.ic_trans_detail._discount_changed, _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._price_default, _g.d.ic_trans_detail._doc_date_calc, _g.d.ic_trans_detail._is_pos, _g.d.ic_trans_detail._branch_code, _g.d.ic_trans_detail._sale_shift_id);
                                            string __detailValue = MyLib._myGlobal._fieldAndComma("\'" + __docDateFull + "\'", "\'" + __docTimeFull + "\'", "\'" + __docNoFull + "\'", "\'" + __custCode + "\'", _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน).ToString(), "2", "-1", "\'" + __transDetail.Rows[__row][_g.d.ic_trans_detail._barcode].ToString() + "\'", "\'" + __transDetail.Rows[__row][_g.d.ic_trans_detail._item_code].ToString() + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__transDetail.Rows[__row][_g.d.ic_trans_detail._item_name].ToString()) + "\'", "\'" + __transDetail.Rows[__row][_g.d.ic_trans_detail._unit_code].ToString() + "\'", __transDetail.Rows[__row][_g.d.ic_trans_detail._qty].ToString(), __transDetail.Rows[__row][_g.d.ic_trans_detail._price].ToString(), "\'" + __transDetail.Rows[__row][_g.d.ic_trans_detail._discount].ToString() + "\'", __transDetail.Rows[__row][_g.d.ic_trans_detail._discount_amount].ToString(), __transDetail.Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString(), "0", "0", __row.ToString(), "1", __transDetail.Rows[__row][_g.d.ic_trans_detail._tax_type].ToString(), "\'" + __transDetail.Rows[__row][_g.d.ic_trans_detail._wh_code].ToString() + "\'", "\'" + __transDetail.Rows[__row][_g.d.ic_trans_detail._shelf_code].ToString() + "\'", __transDetail.Rows[__row][_g.d.ic_trans_detail._item_type].ToString(), __transDetail.Rows[__row][_g.d.ic_trans_detail._stand_value].ToString(), __transDetail.Rows[__row][_g.d.ic_trans_detail._divide_value].ToString(), __transDetail.Rows[__row][_g.d.ic_trans_detail._discount_number].ToString(), __transDetail.Rows[__row][_g.d.ic_trans_detail._price_changed].ToString(), __transDetail.Rows[__row][_g.d.ic_trans_detail._discount_changed].ToString(), "\'" + __transDetail.Rows[__row][_g.d.ic_trans_detail._remark].ToString() + "\'", __transDetail.Rows[__row][_g.d.ic_trans_detail._price_default].ToString(), "\'" + __docDateFull + "\'", "1", "\'" + MyLib._myGlobal._branchCode + "\'", "\'" + _g.g._companyProfile._sale_shift_id + "\'");
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail._table + " (" + __detailField + ") values (" + __detailValue + ")"));
                                        }
                                        // รายละเอียด serial
                                        for (int __row = 0; __row < __serialTrans.Rows.Count; __row++)
                                        {
                                            string __detailField = MyLib._myGlobal._fieldAndComma(
                                                _g.d.ic_trans_serial_number._doc_no,
                                                _g.d.ic_trans_serial_number._doc_date,
                                                _g.d.ic_trans_serial_number._doc_time,
                                                _g.d.ic_trans_serial_number._line_number,
                                                _g.d.ic_trans_serial_number._doc_line_number,
                                                _g.d.ic_trans_serial_number._trans_flag,
                                                _g.d.ic_trans_serial_number._ic_code,
                                                _g.d.ic_trans_serial_number._unit_code,
                                                _g.d.ic_trans_serial_number._wh_code,
                                                _g.d.ic_trans_serial_number._shelf_code,
                                                _g.d.ic_trans_serial_number._wh_code_2,
                                                _g.d.ic_trans_serial_number._shelf_code_2,
                                                _g.d.ic_trans_serial_number._calc_flag,
                                                _g.d.ic_trans_serial_number._description,
                                                _g.d.ic_trans_serial_number._serial_number,
                                                _g.d.ic_trans_serial_number._price,
                                                _g.d.ic_trans_serial_number._inquiry_type,
                                                _g.d.ic_trans_serial_number._void_date);
                                            string __detailValue = MyLib._myGlobal._fieldAndComma(
                                                "\'" + __docNoFull + "\'",
                                                "\'" + __docDateFull + "\'",
                                                "\'" + __docTimeFull + "\'",
                                                __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._line_number].ToString(),
                                                __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._doc_line_number].ToString(),
                                                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน).ToString(),
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._ic_code].ToString() + "\'",
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._unit_code].ToString() + "\'",
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._wh_code].ToString() + "\'",
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._shelf_code].ToString() + "\'",
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._wh_code_2].ToString() + "\'",
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._shelf_code_2].ToString() + "\'",
                                                __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._calc_flag].ToString(),
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._description].ToString() + "\'",
                                                "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._serial_number].ToString() + "\'",
                                                __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._price].ToString(),
                                                __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._inquiry_type].ToString(),
                                                ((__serialTrans.Rows[__row][_g.d.ic_trans_serial_number._void_date].ToString().Length > 0) ? "\'" + __serialTrans.Rows[__row][_g.d.ic_trans_serial_number._void_date].ToString() + "\'" : "null"));
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_serial_number._table + " (" + __detailField + ") values (" + __detailValue + ")"));
                                        }

                                        // การจ่าย
                                        string __fieldList = _g.d.cb_trans_detail._doc_no + "," + _g.d.cb_trans_detail._doc_date + "," + _g.d.cb_trans_detail._doc_time + "," + _g.d.cb_trans_detail._trans_type + "," + _g.d.cb_trans_detail._trans_flag + "," + _g.d.cb_trans_detail._sale_shift_id;
                                        string __dataList = "\'" + __docNoFull + "\',\'" + __docDateFull + "\',\'" + __docTimeFull + "\'," + "2" + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน).ToString() + ",\'" + _g.g._companyProfile._sale_shift_id + "\'";
                                        // บัตรเครดิต
                                        if (__creditCardDetail.Rows.Count > 0)
                                        {
                                            for (int __row = 0; __row < __creditCardDetail.Rows.Count; __row++)
                                            {
                                                string __fieldCreditCard = MyLib._myGlobal._fieldAndComma(__fieldList, _g.d.cb_trans_detail._doc_type, _g.d.cb_trans_detail._trans_number_type, _g.d.cb_trans_detail._ap_ar_type, _g.d.cb_trans_detail._ap_ar_code, _g.d.cb_trans_detail._trans_number, _g.d.cb_trans_detail._no_approved, _g.d.cb_trans_detail._credit_card_type, _g.d.cb_trans_detail._amount, _g.d.cb_trans_detail._charge, _g.d.cb_trans_detail._sum_amount, _g.d.cb_trans_detail._remark);
                                                string __dataCreditCard = MyLib._myGlobal._fieldAndComma(__dataList, "3", "1", "1", "\'" + __custCode + "\'", "\'" + __creditCardDetail.Rows[__row][_g.d.cb_trans_detail._trans_number].ToString() + "\'", "\'" + __creditCardDetail.Rows[__row][_g.d.cb_trans_detail._no_approved].ToString() + "\'", "\'" + __creditCardDetail.Rows[__row][_g.d.cb_trans_detail._credit_card_type].ToString() + "\'", __creditCardDetail.Rows[__row][_g.d.cb_trans_detail._amount].ToString(), __creditCardDetail.Rows[__row][_g.d.cb_trans_detail._charge].ToString(), __creditCardDetail.Rows[__row][_g.d.cb_trans_detail._sum_amount].ToString(), "\'" + __creditCardDetail.Rows[__row][_g.d.cb_trans_detail._remark].ToString() + "\'");
                                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_trans_detail._table + "(" + __fieldList + ") values (" + __dataList + ")"));
                                            }
                                        }
                                        // รวม

                                        if (__payDetail.Rows.Count > 0)
                                        {
                                            string __fieldPay = MyLib._myGlobal._fieldAndComma(_g.d.cb_trans._doc_no, _g.d.cb_trans._doc_date, _g.d.cb_trans._doc_time, _g.d.cb_trans._ap_ar_code, _g.d.cb_trans._pay_type, _g.d.cb_trans._trans_type, _g.d.cb_trans._trans_flag, _g.d.cb_trans._cash_amount, _g.d.cb_trans._card_amount, _g.d.cb_trans._total_income_amount, _g.d.cb_trans._pay_cash_amount, _g.d.cb_trans._money_change, _g.d.cb_trans._sale_shift_id);
                                            string __dataPay = MyLib._myGlobal._fieldAndComma("\'" + __docNoFull + "\'", "\'" + __docDateFull + "\'", "\'" + __docTimeFull + "\'", "\'" + __custCode + "\'", "1", "2", _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน).ToString(), __payDetail.Rows[0][_g.d.cb_trans._cash_amount].ToString(), __payDetail.Rows[0][_g.d.cb_trans._card_amount].ToString(), __payDetail.Rows[0][_g.d.cb_trans._total_income_amount].ToString(), __payDetail.Rows[0][_g.d.cb_trans._pay_cash_amount].ToString(), __payDetail.Rows[0][_g.d.cb_trans._money_change].ToString(), "\'" + _g.g._companyProfile._sale_shift_id + "\'");
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_trans._table + " (" + __fieldPay + ") values (" + __dataPay + ")"));
                                        }
                                        //
                                        __myQuery.Append("</node>");
                                        string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                        if (__resultStr.Length == 0)
                                        {
                                            this.Close();
                                            this._billSuccess(__docNoFull, __docNo);
                                        }
                                        else
                                        {
                                            MessageBox.Show(__resultStr.ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("ไม่พบเอกสารเลขที่" + " : " + __docNoRef);
                            }
                        }
                        else
                        {
                            // reprint mode
                            DataTable __docDataTable = __myFrameWork._queryShort("select * from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + this._textBoxBillNo.Text + "\' and " + _g.d.ic_trans._doc_ref + " <> \'\' and " + _g.d.ic_trans._doc_ref + " is not null ").Tables[0];
                            if (__docDataTable.Rows.Count > 0)
                            {
                                string __docNoFull = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                                string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_ref].ToString();
                                DialogResult __ask = MessageBox.Show("ต้องการพิมพ์เอกสาร" + " [" + __docNoFull + "] " + "จริงหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                                if (__ask == System.Windows.Forms.DialogResult.Yes)
                                {
                                    this._billSuccess(__docNoFull, __docNo);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
        private void _buttonCustSearch_Click(object sender, EventArgs e)
        {
            this._searchCust();
        }

        private void _buttonCustAdd_Click(object sender, EventArgs e)
        {
            this._newCust();
        }
        public delegate void printBillFullSuccessEventHandler(string docNo, string docRefNo);
    }
}
