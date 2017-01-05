using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SMLSyncCheckingTools
{
    public partial class Form1 : Form
    {
        string __sourceGUID = "";
        string __targetGUID = "";

        public delegate void AddResultGridInvoke(List<Object> data);
        public event AddResultGridInvoke _addResultGridInvoke;

        public delegate void ErrorProcessInvoke(string message, int type);
        public event ErrorProcessInvoke _errorProcessInvoke;


        public Form1()
        {
            InitializeComponent();

            this._addResultGridInvoke += Form1__addResultGridInvoke;
            this._errorProcessInvoke += Form1__errorProcessInvoke;
        }

        private void Form1__errorProcessInvoke(string message, int type)
        {
            if (type == 0)
                MessageBox.Show(message, "Error");

            else if (type == 1)
            {
                //MessageBox.Show(message, "Info");
                this._labelStatusInfo.Text = message;
            }
        }

        private void Form1__addResultGridInvoke(List<object> data)
        {
            this._resultGrid.Rows.Add(data.ToArray());
        }

        private void _sourceConnectButton_Click(object sender, EventArgs e)
        {
            // test Connect
            if (__sourceGUID.Length > 0)
            {
                // logout
                _disConnect(this._sourceWebServiceTextbox.Text, this._sourceProviderTextbox.Text, this._sourceGroupTextbox.Text, this._sourceUserCodeTextbox.Text, this._sourcePasswordTextbox.Text, this._sourceDatabaseNameTextbox.Text, __sourceGUID);
                this._sourceConnectdStatus.Text = "";
                this.__sourceGUID = "";
                this._sourceConnectButton.Text = "connect";

                // change status
                this._sourceWebServiceTextbox.Enabled =
                    this._sourceProviderTextbox.Enabled =
                    this._sourceGroupTextbox.Enabled =
                    this._sourceUserCodeTextbox.Enabled =
                    this._sourcePasswordTextbox.Enabled =
                    this._sourceDatabaseNameTextbox.Enabled = true;
            }
            else
            {
                string __guid = _connect(this._sourceWebServiceTextbox.Text, this._sourceProviderTextbox.Text, this._sourceGroupTextbox.Text, this._sourceUserCodeTextbox.Text, this._sourcePasswordTextbox.Text, this._sourceDatabaseNameTextbox.Text);
                if (__guid.Length > 0)
                {
                    this._sourceConnectdStatus.Text = "Connected";
                    this.__sourceGUID = __guid;

                    // change status
                    this._sourceWebServiceTextbox.Enabled =
                        this._sourceProviderTextbox.Enabled =
                        this._sourceGroupTextbox.Enabled =
                        this._sourceUserCodeTextbox.Enabled =
                        this._sourcePasswordTextbox.Enabled =
                        this._sourceDatabaseNameTextbox.Enabled = false;
                    this._sourceConnectButton.Text = "Disconnect";
                }
                else
                {
                    MessageBox.Show("Login Failed");
                    this._sourceConnectdStatus.Text = __errorConnectStr;
                }
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

        private void _desConnectButton_Click(object sender, EventArgs e)
        {

            string __host = "";
            string __provider = "";
            string __group = "";
            string __userCode = "";
            string __password = "";

            //if (_connectSameSourceCheckbox.Checked == true)
            //{
            //    __host = this._sourceWebServiceTextbox.Text;
            //    __provider = this._sourceProviderTextbox.Text;
            //    __group = this._sourceGroupTextbox.Text;
            //    __userCode = this._sourceUserCodeTextbox.Text;
            //    __password = this._sourcePasswordTextbox.Text;
            //}
            //else
            {
                __host = this._desWebServiceTextbox.Text;
                __provider = this._desProviderTextbox.Text;
                __group = this._desGroupTextbox.Text;
                __userCode = this._desUserCodeTextbox.Text;
                __password = this._desPasswordTextbox.Text;
            }

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

                    this._desConnectButton.Text = "Disconnect";
                }
                else
                {
                    MessageBox.Show("Login Failed");
                    this._desConnectedStatus.Text = __errorConnectStr;
                }
            }

        }

        void _buildResultGrid()
        {
            this._resultGrid.Columns.Clear();
            this._resultGrid.DataSource = null;


            for (int __row = 0; __row < this._keyGrid.Rows.Count; __row++)
            {
                if (this._keyGrid.Rows[__row].Cells[0].Value != null)
                {
                    string __getKeyName = this._keyGrid.Rows[__row].Cells[0].Value.ToString();
                    this._resultGrid.Columns.Add(__getKeyName, __getKeyName);
                }
            }

            this._resultGrid.Columns.Add("guidserver", "guidserver");
            this._resultGrid.Columns.Add("guidclient", "guidclient");

        }

        Thread __threadStart;
        private void _startButton_Click(object sender, EventArgs e)
        {
            __sucessProcess = false;
            _buildResultGrid();
            this._processLabel.Text = "";
            this._startButton.Enabled = false;
            this._groupCondition.Enabled = false;
            timer1.Start();

            __threadStart = new Thread(new ThreadStart(_process));
            __threadStart.IsBackground = true;
            __threadStart.Start();


            //_process();
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                __sucessProcess = true;
                __threadStart.Abort();
            }
            catch
            {

            }
        }




        int __maxRowCheck = 0;
        int __rowChecked = 0;
        Boolean __sucessProcess = true;

        void _process()
        {
            try
            {
                // get data key from server 
                List<string> __keys = new List<string>();
                List<string> __keyTypes = new List<string>();

                StringBuilder __keyList = new StringBuilder();

                for (int __row = 0; __row < this._keyGrid.Rows.Count; __row++)
                {
                    if (this._keyGrid.Rows[__row].Cells[0].Value != null)
                    {
                        string __getKeyName = this._keyGrid.Rows[__row].Cells[0].Value.ToString();
                        string __getKeyType = (this._keyGrid.Rows[__row].Cells[1].Value == null) ? "String" : this._keyGrid.Rows[__row].Cells[1].Value.ToString();

                        if (__keyList.Length > 0)
                        {
                            __keyList.Append(",");
                        }
                        __keyList.Append(__getKeyName);


                        __keys.Add(__getKeyName);
                        __keyTypes.Add(__getKeyType);
                    }
                }
                // pack key

                string __guidField = this._indentityFieldName.Text;

                string __query = "select " + __guidField + "," + __keyList.ToString() + " from " + this._tableName.Text;

                
                MyLib._myFrameWork __serverFrameWork = new MyLib._myFrameWork(this._sourceWebServiceTextbox.Text, "SMLConfig" + this._sourceProviderTextbox.Text.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                __serverFrameWork._databaseSelectType = __serverFrameWork._setDataBaseCode();

                DataSet __serverResult = __serverFrameWork._queryStream(this._sourceDatabaseNameTextbox.Text, __query);
                

                // compare local server
                MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork(this._desWebServiceTextbox.Text, "SMLConfig" + this._desProviderTextbox.Text.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                __clientFrameWork._databaseSelectType = __clientFrameWork._setDataBaseCode();

                DataSet __result = __clientFrameWork._queryStream(this._desDatabaseNameTextbox.Text, __query);


                if (__result.Tables.Count > 0) // __serverResult.Tables.Count > 0 && 
                {

                    DataTable __serverTable = __serverResult.Tables[0];
                    DataTable __clientTable = __result.Tables[0];


                    this.Invoke(_errorProcessInvoke, "Server : " + __serverTable.Rows.Count + ", client :" + __clientTable.Rows.Count, 1);


                    __maxRowCheck = __clientTable.Rows.Count;

                    for (int __row = 0; __row < __clientTable.Rows.Count; __row++)
                    {
                        StringBuilder __filter = new StringBuilder();

                        for (int __fieldCount = 0; __fieldCount < __keys.Count; __fieldCount++)
                        {
                            if (__filter.Length > 0)
                            {
                                __filter.Append(" and ");
                            }

                            __filter.Append(__keys[__fieldCount] + "=\'" + __clientTable.Rows[__row][__keys[__fieldCount]].ToString() + "\'");
                        }

                        DataRow[] __getRow = __serverTable.Select(__filter.ToString());
                        if (__getRow.Length > 0)
                        {
                            // compare guid 
                            string __serverGuid = __getRow[0]["guid"].ToString().ToUpper();
                            string __clientGuid = __clientTable.Rows[__row]["guid"].ToString().ToUpper();

                            if (__serverGuid != __clientGuid)
                            {
                                // add to result grid

                                /*
                                int __rowAddr = this._resultGrid.Rows.Add();
                                for (int __fieldCount = 0; __fieldCount < __keys.Count; __fieldCount++)
                                {
                                    this._resultGrid.Rows[__rowAddr].Cells[__keys[__fieldCount]].Value = __clientTable.Rows[__row][__keys[__fieldCount]].ToString();
                                }

                                this._resultGrid.Rows[__rowAddr].Cells["guidserver"].Value = __serverGuid;
                                this._resultGrid.Rows[__rowAddr].Cells["guidclient"].Value = __clientGuid;
                                */

                                /*
                                Boolean __checkAgainPass = false;

                                // recheck again
                                DataSet __check2 = __serverFrameWork._query(this._sourceDatabaseNameTextbox.Text, __query + " where " + __filter);

                                if (__check2.Tables.Count > 0)
                                {
                                    __serverGuid = __check2.Tables[0].Rows[0]["guid"].ToString();
                                    if (__serverGuid != __clientGuid)
                                    {
                                        __checkAgainPass = true;
                                    }
                                }

                                if (__checkAgainPass == true)*/
                                {

                                    List<Object> data = new List<object>();
                                    for (int __fieldCount = 0; __fieldCount < __keys.Count; __fieldCount++)
                                    {
                                        data.Add(__clientTable.Rows[__row][__keys[__fieldCount]].ToString());
                                    }
                                    data.Add(__serverGuid);
                                    data.Add(__clientGuid);

                                    this.Invoke(_addResultGridInvoke, data);
                                }
                            }
                        }

                        __rowChecked = __row + 1;
                    }
                }
                else
                {
                    this.Invoke(_errorProcessInvoke, "No Process Query : " + __query, 1);
                }

            }
            catch (Exception ex)
            {
                this.Invoke(_errorProcessInvoke, ex.ToString(), 0);
            }

            // success
            __sucessProcess = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this._processLabel.Text = string.Format("{0}/{1}", __rowChecked, __maxRowCheck);

            if (__sucessProcess == true)
            {
                timer1.Stop();
                this._startButton.Enabled = true;
                this._groupCondition.Enabled = true;
            }
        }

        private void _genUpdateGuid_Click(object sender, EventArgs e)
        {
            _updateQueryForm queryForm = new _updateQueryForm();


            List<string> __keys = new List<string>();
            for (int __row = 0; __row < this._keyGrid.Rows.Count; __row++)
            {
                if (this._keyGrid.Rows[__row].Cells[0].Value != null)
                {
                    string __getKeyName = this._keyGrid.Rows[__row].Cells[0].Value.ToString();

                    __keys.Add(__getKeyName);
                }
            }

            for (int __row = 0; __row < this._resultGrid.Rows.Count;__row++)
            {
                string __serverGuid = this._resultGrid.Rows[__row].Cells["guidserver"].Value.ToString() ;

                StringBuilder __whereString = new StringBuilder();

                for (int __fieldCount = 0; __fieldCount < __keys.Count; __fieldCount++)
                {
                    if (__whereString.Length > 0)
                    {
                        __whereString.Append(" and ");
                    }

                    __whereString.Append(__keys[__fieldCount] + "=\'" + this._resultGrid.Rows[__row].Cells[__keys[__fieldCount]].Value.ToString() + "\'");
                }

                string __qeryPack = "update " + this._tableName.Text + " set guid =\'" + __serverGuid.ToLower() + "\' where " + __whereString.ToString() + ";\r\n";

                queryForm._queryTextbox.AppendText(__qeryPack);

            }

            queryForm.StartPosition = FormStartPosition.CenterScreen;
            queryForm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // compare insert
            String __queryGet = "select code, whcode, shelf_code, guid, ic_code from ic_wh_shelf ";


            string __getItemCodeQuery = "select code from ic_inventory order by code ";
            //DataTable __query 

        }
    }
}
