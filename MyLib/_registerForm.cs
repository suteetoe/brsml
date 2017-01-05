using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _register : Form
    {
        public _register()
        {
            InitializeComponent();
            this._productCodeTextBox.Enabled = false;
            this._registerButton.Enabled = false;
            this._passwordTextBox.textBox.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                DataTable __dt = __ws._query(MyLib._myGlobal._masterRegisterDatabaseName, "select * from smlaccount where product_code=\'" + this._productCodeTextBox.Text.ToUpper() + "\'").Tables[0];
                if (__dt.Rows.Count == 0)
                {
                    MessageBox.Show("Product code not found.");
                }
                else
                {
                    string __id = __dt.Rows[0]["id"].ToString();
                    if (__id.Trim().Length != 0)
                    {
                        MessageBox.Show(this._productCodeTextBox.Text + " : มีการลงทะเบียนไปแล้ว");
                    }
                    else
                    {
                        MyLib._myGlobal._webServiceServer = this._webserviceTextBox.TextBox.Text;
                        _myFrameWork __myFrameWork = new _myFrameWork();
                        string __macAddress = __myFrameWork._queryInsertOrUpdate("", "MACADDRESS").Replace("\"", "");
                        if (__macAddress.Trim().Length > 0)
                        {
                            // toe auto gen โหมดฉุกเฉิน
                            RandomStringGenerator __ramdonStr = new RandomStringGenerator();

                            string __user = __ramdonStr.NextString(8, true, true, true, false);
                            string __pass = __ramdonStr.NextString(8, true, true, true, false);

                            string __result = __ws._queryInsertOrUpdate(MyLib._myGlobal._masterRegisterDatabaseName, "update smlaccount set id=\'" + __macAddress + "\',user_code = \'" + __user + "\', user_password = \'" + __pass + "\'  where product_code=\'" + this._productCodeTextBox.Text.ToUpper() + "\'");
                            if (__result.Length == 0)
                            {
                                MessageBox.Show("การลงทะเบียนสมบูรณ์");
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("การลงทะเบียนไม่สมบูรณ์ กรุณาติดต่อเจ้าหน้าที่");
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _buttonLoginAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myGlobal._webServiceServer = this._webserviceTextBox.TextBox.Text;
                _myFrameWork __myFrameWork = new _myFrameWork();
                if (__myFrameWork._systemLogin(this._passwordTextBox.TextBox.Text, this._webserviceTextBox.TextBox.Text) == "1")
                {
                    MessageBox.Show("Login Success.");
                    this._productCodeTextBox.Enabled = true;
                    this._registerButton.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Login Fail.");
                    this._productCodeTextBox.Enabled = false;
                    this._registerButton.Enabled = false;
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show("Error : " + __ex.Message.ToString());
            }
        }
    }
}
