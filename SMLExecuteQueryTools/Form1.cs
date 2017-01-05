using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLExecuteQueryTools
{
    public partial class Form1 : Form
    {
        string __targetGUID = "";

        public Form1()
        {
            InitializeComponent();
            this._queryGroupBox.Enabled = false;
        }

        private void _desConnectButton_Click(object sender, EventArgs e)
        {
            string __host = "";
            string __provider = "";
            string __group = "";
            string __userCode = "";
            string __password = "";

            __host = this._desWebServiceTextbox.Text;
            __provider = this._desProviderTextbox.Text;
            __group = this._desGroupTextbox.Text;
            __userCode = this._desUserCodeTextbox.Text;
            __password = this._desPasswordTextbox.Text;

            if (__targetGUID.Length > 0)
            {
                // logout
                _disConnect(__host, __provider, __group, __userCode, __password, this._desDatabaseNameTextbox.Text, __targetGUID);

                this._desWebServiceTextbox.Enabled =
                        this._desProviderTextbox.Enabled =
                        this._desGroupTextbox.Enabled =
                        this._desUserCodeTextbox.Enabled =
                        this._desPasswordTextbox.Enabled =
                        this._desDatabaseNameTextbox.Enabled = true;

                this._queryGroupBox.Enabled = false;

                this._desConnectButton.Text = "Connect";
                this._desConnectedStatus.Text = "";
                this.__targetGUID = "";
            }
            else
            {
                string __guid = _connect(__host, __provider, __group, __userCode, __password, this._desDatabaseNameTextbox.Text);
                if (__guid.Length > 0)
                {
                    this._desConnectedStatus.Text = "Connected";
                    this.__targetGUID = __guid;

                    // ปรับสถานะ
                    this._desWebServiceTextbox.Enabled =
                        this._desProviderTextbox.Enabled =
                        this._desGroupTextbox.Enabled =
                        this._desUserCodeTextbox.Enabled =
                        this._desPasswordTextbox.Enabled =
                        this._desDatabaseNameTextbox.Enabled = false;

                    this._queryGroupBox.Enabled = true;
                    this._desConnectButton.Text = "Disconnect";
                }
                else
                {
                    MessageBox.Show("Login Failed");
                    this._desConnectedStatus.Text = __errorConnectStr;
                }
            }
        }

        void _disConnect(string server, string provider, string group, string userCode, string password, string databaseName, string guid)
        {
            // __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid);
            try
            {
                MyLib._myGlobal._webServiceServerList.Clear();

                MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                data._webServiceName = server;
                data._webServiceConnected = false;
                MyLib._myGlobal._webServiceServerList.Add(data);

                MyLib._myGlobal._providerCode = provider;
                MyLib._myGlobal._databaseName = databaseName;
                MyLib._myGlobal._nonPermission = true;
                MyLib._myGlobal._userCode = userCode;
                MyLib._myGlobal._password = password; //  "superadmin";

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();


                __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, guid);

            }
            catch (Exception ex)
            {
                __errorConnectStr = ex.Message.ToString();
            }
        }

        string __errorConnectStr = "";
        string _connect(string server, string provider, string group, string userCode, string password, string databaseName)
        {
            string __result = "";

            try
            {
                MyLib._myGlobal._webServiceServerList.Clear();

                MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                data._webServiceName = server;
                data._webServiceConnected = false;
                MyLib._myGlobal._webServiceServerList.Add(data);

                MyLib._myGlobal._providerCode = provider;
                MyLib._myGlobal._databaseName = databaseName;
                MyLib._myGlobal._nonPermission = true;
                MyLib._myGlobal._userCode = userCode;
                MyLib._myGlobal._password = password; //  "superadmin";

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                __result = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._userCode, MyLib._myGlobal._password, MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);

            }
            catch (Exception ex)
            {
                __errorConnectStr = ex.Message.ToString();
            }

            return __result;
        }

        private void _executeButton_Click(object sender, EventArgs e)
        {
            if (SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") == -1)
            {
                MessageBox.Show("ไม่อนุญาติให้ทำรายการ");
                return;
            }

            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __result = myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, _textBoxQuery.Text);

            if (__result.Length > 0)
            {
                this._executeResult.Text = __result;
            }
            else
            {
                this._executeResult.Text = "Success";
            }
        }

        private void _queryButton_Click(object sender, EventArgs e)
        {
            string query = _textBoxQuery.Text.ToUpper();
            if (query.Length > 7)
            {
                string __computerName = SystemInformation.ComputerName.ToLower();
                if ((query.IndexOf("INSERT") != -1 || query.IndexOf("UPDATE") != -1 || query.IndexOf("DELETE") != -1 || query.IndexOf("DROP") != -1 || query.IndexOf("TRUNCATE") != -1) && (__computerName.IndexOf("toe-pc") == -1))
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning20"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                        DataSet result = myFrameWork._query(MyLib._myGlobal._databaseName, _textBoxQuery.Text);

                        if (result.Tables[0].Rows.Count == 0)
                        {
                            this._executeResult.Text = "No Result Data";
                            this._resultDatagridView.DataSource = null;
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            this._resultDatagridView.DataSource = result;
                            this._resultDatagridView.DataMember = "Row";
                            this.Cursor = Cursors.Default;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Debugger.Break();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(MyLib._myGlobal._resource("warning20") + "\n" + ex.Message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
