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
    public partial class _periodOpenForm : Form
    {
        string _machineCode = "";
        string _cashierPassword = "";
        public string _periodGuid = "";
        DateTime _openPeriodDatetime;

        public _periodOpenForm(string machineCode, string cashierCode, string cashierPassword)
        {
            InitializeComponent();
            //
            this._machineCode = machineCode;
            this._cashierPassword = cashierPassword;
            //
            this._screen._table_name = _g.d.pos_period._table;
            this._screen._maxColumn = 1;
            this._screen._addTextBox(0, 0, _g.d.pos_period._machine_code, 1);
            this._screen._addDateBox(1, 0, 1, 0, _g.d.pos_period._begin_date, 1, true);
            this._screen._addTextBox(2, 0, _g.d.pos_period._begin_time, 1);
            this._screen._addTextBox(3, 0, _g.d.pos_period._begin_user_code, 1);
            this._screen._addTextBox(4, 0, 1, 1, _g.d.pos_period._password, 1, 1, 0, true, true);
            //
            this._screen._enabedControl(_g.d.pos_period._machine_code, false);
            this._screen._enabedControl(_g.d.pos_period._begin_date, false);
            this._screen._enabedControl(_g.d.pos_period._begin_time, false);
            this._screen._enabedControl(_g.d.pos_period._begin_user_code, false);
            //
            this._screen._setDataStr(_g.d.pos_period._machine_code, machineCode);

            _openPeriodDatetime = DateTime.Now;
            this._screen._setDataDate(_g.d.pos_period._begin_date, _openPeriodDatetime);
            this._screen._setDataStr(_g.d.pos_period._begin_time, _openPeriodDatetime.ToString("HH:mm"));
            this._screen._setDataStr(_g.d.pos_period._begin_user_code, cashierCode);
            this.Load += new EventHandler(_periodOpenForm_Load);
        }

        void _periodOpenForm_Load(object sender, EventArgs e)
        {
            // ตรวจสอบว่าเปิดกะค้างไว้หรือเปล่า
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            // check กะค้าง
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.pos_period._table + " where " + _g.d.pos_period._machine_code + "=\'" + this._machineCode + "\' order by " + _g.d.pos_period._period_status + "," + _g.d.pos_period._begin_date + " desc," + _g.d.pos_period._begin_time + " desc"));

            // ดึงกะ ล่าสุดมาเช็คเวลาปิดกะ ว่าตรงกับเวลาเปิดกะหรือเปล่า
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(*) as xcount from " + _g.d.pos_period._table + " where " + _g.d.pos_period._machine_code + "=\'" + this._machineCode + "\' and " + _g.d.pos_period._end_date + " = " + this._screen._getDataStrQuery(_g.d.pos_period._begin_date) + " and " + _g.d.pos_period._end_time + " = \'" + this._screen._getDataStr(_g.d.pos_period._begin_time) + "\' "));

            __query.Append("</node>");

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Count > 0)
            {
                DataTable __dt = ((DataSet)__result[0]).Tables[0];

                //DataTable __dt = __myFrameWork._queryShort().Tables[0];
                if (__dt.Rows.Count > 0)
                {
                    int __status = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.pos_period._period_status].ToString());
                    if (__status == 0)
                    {
                        MessageBox.Show("มีการเปิดกะค้างไว้แล้ว กรุณาปิดกะก่อน");
                        this.Close();
                    }
                }

                //

                DataTable __check = ((DataSet)__result[1]).Tables[0];
                if (__check.Rows.Count > 0)
                {
                    int __count = MyLib._myGlobal._intPhase(__check.Rows[0][0].ToString());

                    if (__count > 0)
                    {

                        DateTime __currentDate = this._screen._getDataDate(_g.d.pos_period._begin_date);
                        string[] __docTime = this._screen._getDataStr(_g.d.pos_period._begin_time).Split(':');

                        DateTime __setDate = new DateTime(__currentDate.Year, __currentDate.Month, __currentDate.Day, MyLib._myGlobal._intPhase(__docTime[0]), MyLib._myGlobal._intPhase(__docTime[1]), 0);
                        __setDate = __setDate.AddMinutes(1);

                        this._screen._setDataDate(_g.d.pos_period._begin_date, __setDate);
                        this._screen._setDataStr(_g.d.pos_period._begin_time, __setDate.ToString("HH:mm"));

                        this._openPeriodDatetime = __setDate;
                    }
                }


                this._periodGuid = Guid.NewGuid().ToString();
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            string __password = this._screen._getDataStr(_g.d.pos_period._password).ToString();

            if (this._cashierPassword.Equals(__password) == true || MyLib._myGlobal._isDemo == true)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.pos_period._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.pos_period._begin_date, _g.d.pos_period._begin_time, _g.d.pos_period._begin_user_code, _g.d.pos_period._machine_code, _g.d.pos_period._period_status, _g.d.pos_period._period_guid) + ") values (" + MyLib._myGlobal._fieldAndComma(this._screen._getDataStrQuery(_g.d.pos_period._begin_date), this._screen._getDataStrQuery(_g.d.pos_period._begin_time), this._screen._getDataStrQuery(_g.d.pos_period._begin_user_code), this._screen._getDataStrQuery(_g.d.pos_period._machine_code) + ",0, \'" + this._periodGuid + "\')")));
                __myQuery.Append("</node>");
                string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__resultStr.Length == 0)
                {
                    MessageBox.Show("เปิดกะสำเร็จ");
                    if (this._openSuccess != null)
                    {
                        this._openSuccess(this._periodGuid, this._openPeriodDatetime);
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
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
            }
        }

        public event _openPeriodSuccessArgs _openSuccess;
    }

    public delegate void _openPeriodSuccessArgs(string periodGuid, DateTime openPeriodTime);
}
