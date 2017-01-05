using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SMLPosClient
{
    public partial class _moneyReceive : Form
    {
        public delegate void _saveReceiveSuccess();
        public event _saveReceiveSuccess _saveSuccess;
        private string _userCode;
        private string _cashierPassword;
        private SMLPOSControl._posScreenConfig _posScreenConfig;
        private string _posFillmoneyDocFormat;
        string _startRunningNumber = "";
        DateTime _openPeriodTime;

        public _moneyReceive(SMLPOSControl._posScreenConfig posScreenConfig, string posFillmoneyDocFormat, string userCode, string password, string startRunningNumber, DateTime openPeriodTime)
        {
            InitializeComponent();
            //
            this._userCode = userCode;
            this._screen.Invalidate();
            this._cashierPassword = password;
            this._posScreenConfig = posScreenConfig;
            this._posFillmoneyDocFormat = posFillmoneyDocFormat;
            this._startRunningNumber = startRunningNumber;
            this._openPeriodTime = DateTime.Now;

            if(openPeriodTime > this._openPeriodTime)
            {
                this._openPeriodTime = openPeriodTime;
            }

            //
            this._screen._setDataStr(_g.d.POSCashierSettle._MACHINECODE, posScreenConfig._posid);
            this._screen._setDataStr(_g.d.POSCashierSettle._CashierCode, this._userCode);

            string __docTime = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");
            if (_openPeriodTime > DateTime.Now)
            {
                __docTime = _openPeriodTime.ToString("HH:mm");
            }

            this._screen._setDataDate(_g.d.POSCashierSettle._DocDate, _openPeriodTime); // MyLib._myGlobal._workingDate
            this._screen._setDataStr(_g.d.POSCashierSettle._doc_time, __docTime);
            string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.POS_รับเงินทอน, this._posScreenConfig._posid, DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH")), posFillmoneyDocFormat, _g.g._transControlTypeEnum.POS_รับเงินทอน, _g.g._transControlTypeEnum.ว่าง, this._startRunningNumber);
            float __height = this._screen.Height;
            this._screen._setDataStr(_g.d.POSCashierSettle._DocNo, __newDoc);
            //
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            string __password = this._screen._getDataStr(_g.d.POSCashierSettle._cashier_password).ToString();
            if (this._cashierPassword.Equals(__password))
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // รหัสผ่านผู้จัดการ
                string __managerQuery = "select "+ _g.d.erp_user._password +" from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._screen._getDataStr(_g.d.POSCashierSettle._manager_code).ToString() + "\' and " + _g.d.erp_user._password + "=\'" + this._screen._getDataStr(_g.d.POSCashierSettle._manager_password).ToString() + "\'";
                DataTable __manager = __myFrameWork._queryShort(__managerQuery).Tables[0];
                if (__manager.Rows.Count > 0)
                {
                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.POS_รับเงินทอน, this._posScreenConfig._posid, DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH")), this._posFillmoneyDocFormat, _g.g._transControlTypeEnum.POS_รับเงินทอน, _g.g._transControlTypeEnum.ว่าง, "", this._startRunningNumber);
                    this._screen._setDataStr(_g.d.POSCashierSettle._DocNo, __newDoc);
                    //
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    string __fieldList = MyLib._myGlobal._fieldAndComma(_g.d.POSCashierSettle._MACHINECODE, _g.d.POSCashierSettle._DocNo, _g.d.POSCashierSettle._DocDate, _g.d.POSCashierSettle._doc_time, _g.d.POSCashierSettle._CashierCode, _g.d.POSCashierSettle._CashAmount, _g.d.POSCashierSettle._manager_code, _g.d.POSCashierSettle._remark, _g.d.POSCashierSettle._trans_type);
                    string __dataList = MyLib._myGlobal._fieldAndComma(this._screen._getDataStrQuery(_g.d.POSCashierSettle._MACHINECODE), this._screen._getDataStrQuery(_g.d.POSCashierSettle._DocNo), this._screen._getDataStrQuery(_g.d.POSCashierSettle._DocDate), this._screen._getDataStrQuery(_g.d.POSCashierSettle._doc_time), this._screen._getDataStrQuery(_g.d.POSCashierSettle._CashierCode), this._screen._getDataNumber(_g.d.POSCashierSettle._CashAmount).ToString(), this._screen._getDataStrQuery(_g.d.POSCashierSettle._manager_code), this._screen._getDataStrQuery(_g.d.POSCashierSettle._remark) + ",1");
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.POSCashierSettle._table + " (" + __fieldList + ") values (" + __dataList + ")"));
                    __myQuery.Append("</node>");
                    string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__resultStr.Length == 0)
                    {

                        MessageBox.Show("บันทึกสำเร็จ");
                        if (_saveSuccess != null)
                        {
                            _saveSuccess();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(__resultStr);
                    }
                }
                else
                {
                    MessageBox.Show("รหัสหรือรหัสผ่านผู้จัดการไม่ถูกต้อง");
                }
            }
            else
            {
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
            }
        }
    }

    public class __moneyReceiveScreen : MyLib._myScreen
    {
        public __moneyReceiveScreen()
        {
            this._table_name = _g.d.POSCashierSettle._table;
            this._maxColumn = 1;
            int __row = 0;
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._MACHINECODE, 0);
            this._addDateBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._DocDate, 1, true);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._doc_time, 0);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._DocNo, 0);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._CashierCode, 0);
            this._addTextBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._cashier_password, 1, 0, 0, true, true);
            this._addNumberBox(__row++, 0, 0, 0, _g.d.POSCashierSettle._CashAmount, 1, 2, true);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._manager_code, 0);
            this._addTextBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._manager_password, 1, 0, 0, true, true);
            this._addTextBox(__row++, 0, 2, 0, _g.d.POSCashierSettle._remark, 2, 1, 0, true, false, true);
            //
            this._enabedControl(_g.d.POSCashierSettle._MACHINECODE, false);
            this._enabedControl(_g.d.POSCashierSettle._CashierCode, false);
            this._enabedControl(_g.d.POSCashierSettle._DocDate, false);
            this._enabedControl(_g.d.POSCashierSettle._doc_time, false);
            this._enabedControl(_g.d.POSCashierSettle._DocNo, false);
            //
            this.Invalidate();

        }
    }


}
