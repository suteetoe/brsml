using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posPrintSlipPeriod : Form
    {
        private string _userCode;
        private string _password;
        private SMLPOSControl._posScreenConfig _posScreenConfig;
        public event _periodBillPrint _perionBillPrint;

        public _posPrintSlipPeriod(SMLPOSControl._posScreenConfig posScreenConfig, string userCode, string password)
        {
            InitializeComponent();
            this._userCode = userCode;
            this._password = password;
            this._posScreenConfig = posScreenConfig;
            this._textBoxUserCode.Text = this._userCode;
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
                // ตรวจสอบ ผู้จัดการ
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // รหัสผ่านผู้จัดการ
                string __managerQuery = "select " + _g.d.erp_user._password + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._managerUserTextbox.Text + "\' and " + _g.d.erp_user._password + "=\'" + this._managerPasswordTextbox.Text + "\'";
                DataTable __manager = __myFrameWork._queryShort(__managerQuery).Tables[0];
                if (__manager.Rows.Count > 0)
                {
                    string __fromdoc = this._frombillTextbox.Text;
                    string __todoc = this._toBillTextbox.Text;

                    //string __docNoRef = this._textBoxBillNo.Text.Trim().ToUpper();
                    string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._tax_doc_no, _g.d.ic_trans._vat_rate, _g.d.ic_trans._cashier_code, _g.d.ic_trans._cust_code, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._vat_type, _g.d.ic_trans._last_status, _g.d.ic_trans._pos_id, _g.d.ic_trans._is_pos, _g.d.ic_trans._member_code);
                    string __query = "select " + __field + " from " + _g.d.ic_trans._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans._doc_no) + " between \'" + __fromdoc + "\' and \'" + __todoc + "\'"; ;
                    DataTable __docDataTable = __myFrameWork._queryShort(__query).Tables[0];
                    if (__docDataTable.Rows.Count > 0)
                    {
                        string __docNo = __docDataTable.Rows[0][_g.d.ic_trans._doc_no].ToString();
                        DialogResult __ask = MessageBox.Show("ต้องการพิมพ์เอกสาร \nตั้งแต่เลขที่ " + " [" + __fromdoc + "] " + " ถึงเลขที่ " + " [" + __todoc + "] " + " \nจริงหรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        if (__ask == System.Windows.Forms.DialogResult.Yes)
                        {
                            this._perionBillPrint(__fromdoc, __todoc);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบเอกสาร ตั้งแต่เลขที่ " + __fromdoc + " ถึงเอกสารเลขที่ " + __todoc);
                    }
                }
                else
                {
                    MessageBox.Show("รหัสหรือรหัสผ่านผู้จัดการไม่ถูกต้อง");
                }
            }
            else
            {
                MessageBox.Show("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
            }

        }
    }

    public delegate void _periodBillPrint(string fromdoc, string todoc);
}
