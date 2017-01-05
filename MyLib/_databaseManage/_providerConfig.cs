using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _providerConfig : Form
    {
        string _providerCode = "Provider Code";
        string _providerName = "Provider Name";
        int _mode = 0;
        public string _selectCode = "";
        public int _exitMode = 0;
        Boolean _askClose = true;

        /// <summary>
        /// เลือก Provider
        /// </summary>
        /// <param name="mode">0=Setup,1=Select,2=hide with customprovider</param>
        public _providerConfig(int mode, String title)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._mode = mode;
                this.Text = title;
                if (this._mode == 1)
                {
                    this._saveButton.Text = "Select";
                    this._providerGrid._isEdit = false;
                }

                if (this._mode == 2)
                {
                    this._saveButton.Text = "Login";
                    this._providerGrid.Visible = false;
                    this.flowLayoutPanel1.Visible = false;
                    this.Size = new Size(623, 95);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this._askClose)
            {
                DialogResult __result = MessageBox.Show("Close windows", "Close", MessageBoxButtons.YesNo);
                if (__result == DialogResult.Yes)
                {
                    this._exitMode = 1;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            base.OnClosing(e);
        }


        private void _providerConfig_Load(object sender, EventArgs e)
        {
            this._providerGrid._addColumn(_providerCode, 1, 100, 40);
            this._providerGrid._addColumn(_providerName, 1, 100, 60);
            this._passwordTextBox.textBox.PasswordChar = '*';
            //
            this._providerGrid.Enabled = false;
            this._saveButton.Enabled = false;
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

                    if (this._mode == 2)
                    {
                        

                        StringBuilder __result = new StringBuilder();
                        Boolean _havePosstarterResult = false;
                        MyLib._myGlobal._webServiceServer = this._webserviceTextBox.TextBox.Text;

                        ArrayList __getProviderOld = __myFrameWork._providerLoad(this._passwordTextBox.TextBox.Text);
                        for (int __loop = 0; __loop < __getProviderOld.Count; __loop++)
                        {
                            MyLib._providerType __data = (MyLib._providerType)__getProviderOld[__loop];

                            if (__data._code.Equals(this._selectCode))
                            {
                                _havePosstarterResult = true;
                            }
                            if (__result.Length > 0)
                            {
                                __result.Append("<br>");
                            }
                            __result.Append(__data._code + "," + __data._name);
                        }


                        if (_havePosstarterResult == false)
                        {
                            if (__result.Length > 0)
                            {
                                __result.Append("<br>");
                            }
                            __result.Append(this._selectCode + "," + this._selectCode);
                        }

                        if (__myFrameWork._providerSave(this._passwordTextBox.TextBox.Text, __result.ToString()) == "1")
                        {
                            // save provider success
                            //this._selectCode = MyLib._myGlobal._mainProviderPOSStarter;
                            this._askClose = false;
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Save Fail.");
                        }
                    }

                    this._providerGrid.Enabled = true;
                    this._saveButton.Enabled = true;
                    // Load Data
                    this._providerGrid._clear();
                    __myFrameWork._initGlobal();
                    ArrayList __getData = __myFrameWork._providerLoad(this._passwordTextBox.TextBox.Text);
                    for (int __loop = 0; __loop < __getData.Count; __loop++)
                    {
                        MyLib._providerType __data = (MyLib._providerType)__getData[__loop];
                        int __rowAddr = this._providerGrid._addRow();
                        this._providerGrid._cellUpdate(__rowAddr, 0, __data._code, false);
                        this._providerGrid._cellUpdate(__rowAddr, 1, __data._name, false);
                    }


                }
                else
                {
                    MessageBox.Show("Login Fail.");
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show("Error : " + __ex.Message.ToString());
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._mode == 0)
            {
                StringBuilder __result = new StringBuilder();
                for (int __row = 0; __row < this._providerGrid._rowData.Count; __row++)
                {
                    string __code = this._providerGrid._cellGet(__row, 0).ToString().Trim().ToUpper();
                    if (__code.Length > 0)
                    {
                        if (__result.Length > 0)
                        {
                            __result.Append("<br>");
                        }
                        __result.Append(__code + ",");
                        __result.Append(this._providerGrid._cellGet(__row, 1).ToString());
                    }
                }
                MyLib._myGlobal._webServiceServer = this._webserviceTextBox.TextBox.Text;
                _myFrameWork __myFrameWork = new _myFrameWork();
                if (__myFrameWork._providerSave(this._passwordTextBox.TextBox.Text, __result.ToString()) == "1")
                {
                    MessageBox.Show("Save Success.");
                    this._askClose = false;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Save Fail.");
                }
            }
            else
            {
                if (this._providerGrid._selectRow != -1)
                {
                    this._selectCode = this._providerGrid._cellGet(this._providerGrid._selectRow, 0).ToString();
                    MessageBox.Show("Selecte : " + this._selectCode);
                    this._askClose = false;
                    this.Dispose();
                }
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
