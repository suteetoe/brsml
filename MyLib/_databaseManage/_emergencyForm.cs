using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _emergencyForm : MyLib._myForm
    {
        public _emergencyForm()
        {
            InitializeComponent();
            this._passTextbox.KeyUp += new KeyEventHandler(_passTextbox_KeyUp);
            this._portTextbox.KeyUp += new KeyEventHandler(_passTextbox_KeyUp);
            this._loginButton.Click += new EventHandler(_loginButton_Click);
        }

        void _loginButton_Click(object sender, EventArgs e)
        {
            _getEmergencyURL();
        }

        void _passTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //_loginButton.PerformClick
                _getEmergencyURL();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _getEmergencyURL()
        {
            //MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            //string __loadXml = __myFrameWork._loadXmlFile(MyLib._myGlobal._registerXmlFileName);
            //string __xml = Encoding.Unicode.GetString(Convert.FromBase64String(__loadXml));
            //string[] __getData = __xml.Split('|');
            //string __productCode = __getData[0].ToString();

            MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
            DataSet __result = __ws._query(MyLib._myGlobal._masterRegisterDatabaseName, "select user_code,user_password, last_ip from smlaccount where user_code = \'" + this._userTextbox.Text + "\' and user_password = \'" + this._passTextbox.Text + "\' ");
            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                string __user_code = __result.Tables[0].Rows[0]["user_code"].ToString();
                string __user_password = __result.Tables[0].Rows[0]["user_password"].ToString();
                string __server_ip = __result.Tables[0].Rows[0]["last_ip"].ToString();

                if (__user_code.Equals(this._userTextbox.Text) && __user_password.Equals(this._passTextbox.Text))
                {
                    MyLib._myGlobal._emergencyMode = true;
                    MyLib._myGlobal._emergencyURL = __server_ip;
                    MyLib._myGlobal._emergencyPort = this._portTextbox.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ไม่พบ user และ password ดังกล่าว", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ไม่พบ user และ password ดังกล่าว", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        
    }
}
