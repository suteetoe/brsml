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
    public partial class _reprintBillForm : Form
    {
        private string _userCode;
        private string _password;
        private SMLPOSControl._posScreenConfig _posScreenConfig;
        public event PrintHandler _print;

        public _reprintBillForm(SMLPOSControl._posScreenConfig posScreenConfig, string userCode, string password)
        {
            InitializeComponent();
            this._userCode = userCode;
            this._password = password;
            this._posScreenConfig = posScreenConfig;
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            // ตรวจสอบ user,password
            if (this._userCode.Equals(this._textBoxUserCode.Text) && this._password.Equals(this._textBoxPassword.Text))
            {
                string __docNoRef = this._textBoxBillNo.Text.Trim().ToUpper();
                string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._tax_doc_no, _g.d.ic_trans._vat_rate, _g.d.ic_trans._cashier_code, _g.d.ic_trans._cust_code, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._vat_type, _g.d.ic_trans._last_status, _g.d.ic_trans._pos_id, _g.d.ic_trans._is_pos, _g.d.ic_trans._member_code);
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select " + __field + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no_guid) + "=\'" + __docNoRef + "\' or " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + "=\'" + __docNoRef + "\'"; ;
                DataTable __docDataTable = __myFrameWork._queryShort(__query).Tables[0];
                if (__docDataTable.Rows.Count > 0)
                {
                    string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                    DialogResult __ask = MessageBox.Show("ต้องการพิมพ์เอกสาร" + " [" + __docNo + "] " + "จริงหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                    if (__ask == System.Windows.Forms.DialogResult.Yes)
                    {
                        this._print(__docNo);
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

        void _searchBill()
        {
            
            MyLib._searchDataFull __search = new MyLib._searchDataFull();

            DateTime __beginDate = DateTime.Now;
            string __beginTime = "00:00";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __dt = __myFrameWork._queryShort("select * from " + _g.d.pos_period._table + " where " + _g.d.pos_period._machine_code + "=\'" + this._posScreenConfig._posid + "\' order by " + _g.d.pos_period._period_status + " ," + _g.d.pos_period._begin_date + " desc," + _g.d.pos_period._begin_time + " desc").Tables[0];
            if (__dt.Rows.Count > 0)
            {
                int __status = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.pos_period._period_status].ToString());
                if (__status == 0)
                {
                    __beginDate = MyLib._myGlobal._convertDateFromQuery(__dt.Rows[0][_g.d.pos_period._begin_date].ToString());
                    __beginTime = __dt.Rows[0][_g.d.pos_period._begin_time].ToString();
                }
            }
            string __dateCompareTrans = "to_timestamp(date(" + _g.d.ic_trans._doc_date + ")||\' \'||" + _g.d.ic_trans._doc_time + ",'YYYY/MM/DD HH24:MI')::timestamp";
            string __dateTime = __beginDate.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + " " + __beginTime + ":00";
            //
            string __extraWhere = _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + this._posScreenConfig._posid + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' and ( " + _g.d.ic_trans._doc_ref + " is null or " + _g.d.ic_trans._doc_ref + " = \'\' ) ";


            __search.Text = MyLib._myGlobal._resource("ค้นหาใบกำกับภาษี");
            __search._dataList._loadViewFormat(_g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ), MyLib._myGlobal._userSearchScreenGroup, false);
            __search.WindowState = FormWindowState.Maximized;
            __search._dataList._extraWhere = __extraWhere;
            __search._dataList._gridData._mouseClick += (s1, e1) =>
            {
                this._textBoxBillNo.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                __search.Close();
                __search.Dispose();
            };
            __search._searchEnterKeyPress += (s1, e1) =>
            {
                this._textBoxBillNo.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                __search.Close();
                __search.Dispose();
            };
            __search.ShowDialog();

            /*
            Form __form = new Form();
            //
            DateTime __beginDate = DateTime.Now;
            string __beginTime = "00:00";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __dt = __myFrameWork._queryShort("select * from " + _g.d.pos_period._table + " where " + _g.d.pos_period._machine_code + "=\'" + this._posScreenConfig._posid + "\' order by " + _g.d.pos_period._period_status + " ," + _g.d.pos_period._begin_date + " desc," + _g.d.pos_period._begin_time + " desc").Tables[0];
            if (__dt.Rows.Count > 0)
            {
                int __status = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.pos_period._period_status].ToString());
                if (__status == 0)
                {
                    __beginDate = MyLib._myGlobal._convertDateFromQuery(__dt.Rows[0][_g.d.pos_period._begin_date].ToString());
                    __beginTime = __dt.Rows[0][_g.d.pos_period._begin_time].ToString();
                }
            }
            string __dateCompareTrans = "to_timestamp(date(" + _g.d.ic_trans._doc_date + ")||\' \'||" + _g.d.ic_trans._doc_time + ",'YYYY/MM/DD HH24:MI')::timestamp";
            string __dateTime = __beginDate.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + " " + __beginTime + ":00";
            //
            __form.Text = MyLib._myGlobal._resource("ค้นหาใบกำกับภาษี");  //this._posConfig._posid + " " + __dateTime;
            SMLERPSO._soInvoice __control = new SMLERPSO._soInvoice(__form.Text, _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + this._posScreenConfig._posid + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' and ( " + _g.d.ic_trans._doc_ref + " is null or " + _g.d.ic_trans._doc_ref + " = \'\' ) ", true);
            __control.Dock = DockStyle.Fill;
            __form.Controls.Add(__control);
            __form.WindowState = FormWindowState.Maximized;

            __control._ictrans._myManageTrans._dataList._gridData._mouseClick += (s1, e1) =>
            {
                this._textBoxBillNo.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                __form.Close();
                __form.Dispose();
            };
            __search._searchEnterKeyPress += (s1, e1) =>
            {
                this._textBoxBillNo.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                __form.Close();
                __form.Dispose();
            };

            __form.ShowDialog();*/

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    this._searchBill();
                    return true;
                
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public delegate void PrintHandler(string docNo);

        private void _buttonBillSearch_Click(object sender, EventArgs e)
        {
            this._searchBill();
        }
    }
}
