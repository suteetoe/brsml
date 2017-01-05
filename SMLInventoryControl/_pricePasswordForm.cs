using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _pricePasswordForm : Form
    {
        public Boolean _passwordPass = false;
        public string _userCode = "";
        private int _mode;

        string __getTitle()
        {
            string __result = "";
            switch (this._mode)
            {
                case 1:
                    __result = "แก้ไขราคา";
                    break;
                case 2:
                    __result = "ราคาตามกลุ่มลูกค้า";

                    break;
                case 3:
                case 5:
                    __result = "อนุมัติวงเงินเครดิต";
                    break;
                case 4 :
                    __result = "อนุมัติส่วนลด";
                        break;
                        
            }
            return __result;
        }

        string __getFieldLevel()
        {
            string __result = "";
            switch (this._mode)
            {
                case 1:
                    __result = _g.d.erp_user._price_level_1;
                    break;
                case 2:
                    __result = _g.d.erp_user._price_level_2;
                    break;
                case 3:
                    __result = _g.d.erp_user._approve_ar_credit;
                    break;
                case 4 :
                    __result = _g.d.erp_user._discount_level_1;
                    break;
            }
            return __result;
        }

        public _pricePasswordForm(int mode)
        {
            InitializeComponent();
            //
            this._mode = mode;
            this.Text = MyLib._myGlobal._resource(__getTitle());
            this._userTextBox.KeyPress += new KeyPressEventHandler(_userTextBox_KeyPress);
            this._passwordTextBox.KeyPress += new KeyPressEventHandler(_passwordTextBox_KeyPress);
            this.Shown += new EventHandler(_pricePasswordForm_Shown);
        }

        void _passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this._process();
            }
        }

        void _userTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this._passwordTextBox.Focus();
            }
        }

        void _pricePasswordForm_Shown(object sender, EventArgs e)
        {
            this._userTextBox.Focus();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (keyData == Keys.F12)
            {
                this._process();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _process()
        {
            if (this._mode != 5)
            {
                // ตรวจรหัสผ่าน และมีสิทธิแก้ราคาตามกลุ่มลูกค้า หรือแก้ไขราคา
                Boolean __passwordFailMessage = true;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __levelField = __getFieldLevel();
                DataTable __getPassword = __myFrameWork._queryShort("select " + _g.d.erp_user._password + "," + __levelField + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._userTextBox.Text + "\'").Tables[0];
                if (__getPassword.Rows.Count > 0)
                {
                    string __password = __getPassword.Rows[0][0].ToString();
                    int __levelValue = MyLib._myGlobal._intPhase(__getPassword.Rows[0][1].ToString());
                    if (this._passwordTextBox.Text.Equals(__password))
                    {
                        if (__levelValue != 1)
                        {
                            // ไม่มีสิทธิอนุมัติ
                            MessageBox.Show(this._userTextBox.Text + " : " + MyLib._myGlobal._resource("ไม่มีสิทธิอนุมัติ"));
                            __passwordFailMessage = false;
                        }
                        else
                        {
                            this._userCode = this._userTextBox.Text;
                            this._passwordPass = true;
                            this.Close();
                            return;
                        }
                    }
                }
                if (__passwordFailMessage)
                {
                    MessageBox.Show("Password fail.");
                }
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._process();
        }
    }
}
