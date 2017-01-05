using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace SMLPosClient
{
    public partial class _cancelBillForm : Form
    {
        private string _userCode;
        private string _password;
        private SMLPOSControl._posScreenConfig _posScreenConfig;
        public event CancelBillSuccessEventHandler _cancelBillSuccess;
        string _docNoFormat = "";
        string _startRunningNumber = "";
        private Boolean _otherDate = false;
        private Boolean _checkShift = false;
        private string _docFormatCode = "";

        public _cancelBillForm(SMLPOSControl._posScreenConfig posScreenConfig, string docNoFormat, string userCode, string password, Boolean otherDate, string startRunningNumber, Boolean checkShift, string docFormatCode)
        {
            InitializeComponent();
            this._docNoFormat = docNoFormat;
            this._userCode = userCode;
            this._password = password;
            this._posScreenConfig = posScreenConfig;
            this._otherDate = otherDate;
            this._startRunningNumber = startRunningNumber;
            this._checkShift = checkShift;
            this._docFormatCode = docFormatCode;
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบ user,password
                if (this._userCode.Equals(this._textBoxUserCode.Text) && this._password.Equals(this._textBoxPassword.Text))
                {
                    string __docNoRef = this._textBoxBillNo.Text.Trim().ToUpper();
                    string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._tax_doc_no, _g.d.ic_trans._vat_rate, _g.d.ic_trans._cashier_code, _g.d.ic_trans._cust_code, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._vat_type, _g.d.ic_trans._last_status, _g.d.ic_trans._pos_id, _g.d.ic_trans._is_pos, _g.d.ic_trans._member_code, _g.d.ic_trans._branch_code);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "select " + __field + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no_guid) + "=\'" + __docNoRef + "\' or " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNoRef + "\'"; ;
                    //string __query_detail = "select " + __field + " from " + _g.d.ic_trans_detail._table + " where  " + _g.d.ic_trans_detail._doc_no + "=(select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + "  where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no_guid) + "=\'" + __docNoRef + "\') or " + MyLib._myGlobal._addUpper(_g.d.ic_trans_detail._doc_no) + "=\'" + __docNoRef + "\'"; ;

                    StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                    __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                    //__queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query_detail));
                    if (this._checkShift ==true)
                    {
                        string __queryShift = "select * from " + _g.d.pos_period._table + " where " + _g.d.pos_period._machine_code + "=\'" + this._posScreenConfig._posid + "\' and " + _g.d.pos_period._period_status + " = 0 order by " + _g.d.pos_period._period_status + "," + _g.d.pos_period._begin_date + " desc," + _g.d.pos_period._begin_time + " desc";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryShift));

                    }
                    __queryList.Append("</node>");

                    ArrayList __resultQuery = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());



                    //DataTable __docDataTable = __myFrameWork._queryShort(__query).Tables[0];

                    DataTable __docDataTable = ((DataSet)__resultQuery[0]).Tables[0];


                    if (__docDataTable.Rows.Count > 0)
                    {
                        string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                        DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__docDataTable.Rows[0][_g.d.ic_trans._doc_date].ToString());
                        string __docTime = __docDataTable.Rows[0][_g.d.ic_trans._doc_time].ToString();

                        int __lastStatus = (int)MyLib._myGlobal._decimalPhase(__docDataTable.Rows[0][_g.d.ic_trans._last_status].ToString());
                        if (__lastStatus == 1)
                        {
                            MessageBox.Show("เอกสาร" + " [" + __docNo + "] " + "ยกเลิกไปแล้ว");
                        }
                        else
                        {

                            // check shift
                            if (this._checkShift == true)
                            {
                                // compare doc_date and open shift
                                int __docHour = (__docTime.IndexOf(":") != -1) ? MyLib._myGlobal._intPhase(__docTime.Split(':')[0].ToString()) : 0;
                                int __docminute = (__docTime.IndexOf(":") != -1) ? MyLib._myGlobal._intPhase(__docTime.Split(':')[1].ToString()) : 0;

                                DataTable __shiftTable = ((DataSet)__resultQuery[1]).Tables[0];

                                DateTime __openShiftDate = MyLib._myGlobal._convertDateFromQuery(__shiftTable.Rows[0][_g.d.pos_period._begin_date].ToString());
                                string __openShiftTime = __shiftTable.Rows[0][_g.d.pos_period._begin_time].ToString();
                                int __beginHour = (__openShiftTime.IndexOf(":") != -1) ? MyLib._myGlobal._intPhase(__openShiftTime.Split(':')[0].ToString()) : 0;
                                int __beginminute = (__openShiftTime.IndexOf(":") != -1) ? MyLib._myGlobal._intPhase(__openShiftTime.Split(':')[1].ToString()) : 0;


                                DateTime __docDateTimeCheck = new DateTime(__docDate.Year, __docDate.Month, __docDate.Day, __docHour, __docminute, 0);
                                DateTime __openShiftTimeCheck = new DateTime(__openShiftDate.Year, __openShiftDate.Month, __openShiftDate.Day, __beginHour, __beginminute, 0);

                                if (__openShiftTimeCheck > __docDateTimeCheck)
                                {
                                    MessageBox.Show("ห้ามยกเลิกใบเสร็จข้ามกะ");
                                    return;
                                }
                            }

                            bool __tmp = __docDate.Equals(DateTime.Today);
                            if (this._otherDate == false && !__docDate.Equals(DateTime.Today))
                            {
                                MessageBox.Show("ห้ามยกเลิกใบเสร็จข้ามวัน");
                                return;
                            }

                            DialogResult __ask = MessageBox.Show("ต้องการยกเลิกเอกสาร" + " [" + __docNo + "] " + "จริงหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                            if (__ask == System.Windows.Forms.DialogResult.Yes)
                            {
                                StringBuilder __myQuery = new StringBuilder();
                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                // หัวเอกสาร
                                string __dateFormat = "yyyy-MM-dd";
                                string __docDateCancel = DateTime.Now.ToString(__dateFormat, new CultureInfo("en-US"));
                                string __docTimeCancel = DateTime.Now.ToString("HH:mm");
                                string __docNoCancel = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, this._posScreenConfig._posid, DateTime.Now.ToString(__dateFormat, new CultureInfo("th-TH")), this._docNoFormat, _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก, _g.g._transControlTypeEnum.ว่าง, "", this._startRunningNumber);
                                string __custCode = __docDataTable.Rows[0][_g.d.ic_trans._cust_code].ToString();
                                string __memberCode = __docDataTable.Rows[0][_g.d.ic_trans._member_code].ToString();
                                string __value = MyLib._myGlobal._fieldAndComma("\'" + __docDateCancel + "\'", "\'" + __docTimeCancel + "\'", "\'" + __docNoCancel + "\'", "\'" + __docDateCancel + "\'", "\'" + __docNoCancel + "\'", _g.g._companyProfile._vat_rate.ToString(), "\'" + this._userCode + "\'", "\'" + __custCode + "\'", _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก).ToString(), "2", "1", "1", "0", "\'" + this._posScreenConfig._posid + "\'", "1", "\'" + __memberCode + "\'", "\'" + MyLib._myGlobal._branchCode + "\'");
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + " (" + __field + "," + _g.d.ic_trans._doc_ref + "," + _g.d.ic_trans._sale_shift_id + ",remark, " + _g.d.ic_trans._doc_format_code + ") values (" + __value + "," + "\'" + __docNo + "\'" + ",\'" + _g.g._companyProfile._sale_shift_id + "\', \'" + this._textBoxRemark.Text + "\',\'" + this._docFormatCode + "\')"));
                                // update
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans._table + " set " + _g.d.ic_trans._last_status + "=1 where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\'"));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._last_status + "=1 where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'"));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.cb_trans_detail._table + " set " + _g.d.cb_trans_detail._last_status + "=1 where " + _g.d.cb_trans_detail._doc_no + "=\'" + __docNo + "\'"));
                                __myQuery.Append("</node>");
                                string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                if (__resultStr.Length == 0)
                                {
                                    // process coupon 
                                    StringBuilder __couponList = new StringBuilder();
                                    string _queryCouponList = "select " + _g.d.cb_trans_detail._trans_number + " from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + __docNo + "\' and " + _g.d.cb_trans_detail._doc_type + "=9";
                                    DataTable __table = __myFrameWork._queryShort(_queryCouponList).Tables[0];

                                    for (int __i = 0; __i < __table.Rows.Count; __i++)
                                    {
                                        if (__i != 0)
                                            __couponList.Append(",");
                                        __couponList.Append(__table.Rows[__i][_g.d.cb_trans_detail._trans_number].ToString());
                                    }

                                    SMLProcess._posProcess __processPos = new SMLProcess._posProcess();
                                    __processPos._processCoupon(__couponList.ToString());

                                    // toe process item 
                                    string __query_detail = "select " + _g.d.ic_trans_detail._item_code + ", " + _g.d.ic_trans_detail._is_serial_number + " from " + _g.d.ic_trans_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_detail._doc_no) + "=\'" + __docNo + "\'"; ;
                                    DataTable __tableItemCancel = __myFrameWork._queryShort(__query_detail).Tables[0];

                                    Boolean __is_Serial_prcess = false;

                                    StringBuilder __itemList = new StringBuilder();
                                    for (int __loop = 0; __loop < __tableItemCancel.Rows.Count; __loop++)
                                    {
                                        if (__itemList.Length > 0)
                                            __itemList.Append(",");

                                        __itemList.Append("'" + __tableItemCancel.Rows[__loop][_g.d.ic_trans_detail._item_code].ToString() + "'");

                                        if (__tableItemCancel.Rows[__loop][_g.d.ic_trans_detail._is_serial_number].ToString().Equals("1"))
                                        {
                                            __is_Serial_prcess = true;
                                        }
                                    }


                                    if (__is_Serial_prcess == true)
                                    {
                                        SMLProcess._docFlowThread __flow = new SMLProcess._docFlowThread(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก, "", "");
                                        __myQuery = new StringBuilder();
                                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                                        __myQuery.Append(__flow._serialNumberQueryList());
                                        __myQuery.Append("</node>");

                                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                    }

                                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                                    __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, __itemList.ToString(), "*");


                                    MessageBox.Show("ยกเลิกเอกสาร" + " [" + __docNo + "] " + "สำเร็จ");
                                    this.Close();
                                    this._cancelBillSuccess(__docNo, __docNoCancel);
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
                    MessageBox.Show("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
        public delegate void CancelBillSuccessEventHandler(string docNo, string cancelDocNo);
    }
}
