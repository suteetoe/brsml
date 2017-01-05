using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;

namespace DTSClientDownload
{
    public partial class _serverConfig : Form
    {
        public _serverConfig()
        {
            InitializeComponent();
            this.Load += new EventHandler(_serverConfig_Load);
        }

        void _serverConfig_Load(object sender, EventArgs e)
        {
            _loadServerConfig();
        }

        void _loadServerConfig()
        {
            try
            {
                string __xFileName = _global._smlConfigFile + "\\" + _global._dts_server_configFileName;

                XmlDocument xDoc = new XmlDocument();
                try
                {
                    xDoc.Load(__xFileName);
                }
                catch
                {
                    return;
                }

                xDoc.DocumentElement.Normalize();
                XmlElement __xRoot = xDoc.DocumentElement;


                for (int __loop = 1; __loop < 6; __loop++)
                {
                    string __getName = "";
                    switch (__loop)
                    {
                        case 1: __getName = "server"; break;
                        case 2: __getName = "user"; break;
                        case 3: __getName = "password"; break;
                        case 4: __getName = "database"; break;
                        case 5: __getName = "dts_server"; break;
                    }
                    XmlNodeList __xReader = __xRoot.GetElementsByTagName(__getName);

                    for (int __table = 0; __table < __xReader.Count; __table++)
                    {
                        XmlNode __xFirstNode = __xReader.Item(__table);
                        if (__xFirstNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement __xTable = (XmlElement)__xFirstNode;
                            switch (__loop)
                            {
                                case 1: this._serverNameTextBox.Text = __xTable.InnerText; break;
                                case 2: this._userCodeTextBox.Text = __xTable.InnerText; break;
                                case 3: break;
                                case 4: this._databaseNameTextBox.Text = __xTable.InnerText; break;
                                case 5: this._serverIpAddress.Text = __xTable.InnerText; break;
                            }
                        }
                    } // for
                } // for
            }
            catch
            {
            }

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _saveData()
        {
            if (_testConnect())
            {
                Boolean __foundAgent = false;
                string __agentName = "";

                string __query = "select * from " + _g.DataServer.dts_agent._table + " where " + MyLib._myGlobal._addUpper(_g.DataServer.dts_agent._user_code) + "=\'" + this._agentcodeTextbox.Text.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(_g.DataServer.dts_agent._user_password) + "=\'" + this._agentPasswordTextbox.Text.ToUpper() + "\'";

                //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(this._serverIpAddress.Text, "SMLConfigDTS.xml", MyLib._myGlobal._databaseType.MicrosoftSQL2005);

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(this._serverIpAddress.Text, "SMLConfigDTS.xml", MyLib._myGlobal._databaseType.MicrosoftSQL2005);
                DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);

                if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
                {
                    for (int __i = 0; __i < __dataResult.Tables[0].Rows.Count; __i++)
                    {
                        if (__dataResult.Tables[0].Rows[__i][_g.DataServer.dts_agent._user_code].ToString().ToUpper().Equals(this._agentcodeTextbox.Text.ToUpper()) && __dataResult.Tables[0].Rows[__i][_g.DataServer.dts_agent._user_password].ToString().ToUpper().Equals(this._agentPasswordTextbox.Text.ToUpper()))
                        {
                            __foundAgent = true;
                            __agentName = __dataResult.Tables[0].Rows[__i][_g.DataServer.dts_agent._agent_name].ToString();
                        }
                    }
                }

                // check agentcode and password
                if (__foundAgent == false)
                {
                    MessageBox.Show("Agent Code และ Agent Password ไม่ถูกต้อง", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // check first
                _clientFrameWork __fw = new _clientFrameWork(this._serverNameTextBox.Text, this._userCodeTextBox.Text, this._userPasswordTextBox.Text, this._databaseNameTextBox.Text);

                if (__fw._isTableExists("dts_user") == false)
                {
                    // สร้างตาราง dts user
                    StringBuilder __script = new StringBuilder();
                    __script.Append("create table dts_user \r\n");
                    __script.Append(" ( \r\n");
                    __script.Append("roworder int IDENTITY(1,1) NOT NULL, \r\n");
                    __script.Append("agent_code	varchar(25) NOT NULL, \r\n");
                    __script.Append("agent_name varchar(255) NOT NULL, \r\n");
                    __script.Append("user_code varchar(255) NOT NULL, \r\n");
                    __script.Append("user_password varchar(255), \r\n");
                    __script.Append("constraint pk_synd_send_data primary key nonclustered (roworder) \r\n");
                    __script.Append(" ); \r\n");
                    __fw._excute(__script.ToString());
                }

                // check user ถ้าไม่มี insert ได้เลย

                string __userQuery = "select count(*) as xcount from dts_user";
                DataSet __ds = __fw._query(__userQuery);
                if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                {
                    if (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][0].ToString()).Equals(0))
                    {
                        // pack query insert
                        string __insFirstLogin = "insert into dts_user (agent_code, agent_name, user_code, user_password) values ('" + this._agentcodeTextbox.Text + "', '" + __agentName + "', '" + this._agentcodeTextbox.Text + "', 'superadmin')";
                        __fw._excute(__insFirstLogin);
                    }
                    else
                    {
                        string __updatePassword = "";
                        if (MessageBox.Show("Reset Password ", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                        {
                            __updatePassword = ", user_password = 'superadmin' ";
                        }
                        //  user_password) values (, , , 'superadmin')";
                        string __insFirstLogin = "update dts_user set agent_code = '" + this._agentcodeTextbox.Text + "', agent_name = '" + __agentName + "', user_code = '" + this._agentcodeTextbox.Text + "'" + __updatePassword;
                        __fw._excute(__insFirstLogin);
                    }
                }


                try
                {
                    StringBuilder __xmlStr = new StringBuilder(String.Concat(MyLib._myGlobal._xmlHeader, "<node>"));
                    __xmlStr.Append(String.Concat("<server>", this._serverNameTextBox.Text, "</server>"));
                    __xmlStr.Append(String.Concat("<user>", this._userCodeTextBox.Text, "</user>"));
                    __xmlStr.Append(String.Concat("<password>", this._userPasswordTextBox.Text, "</password>"));
                    __xmlStr.Append(String.Concat("<database>", this._databaseNameTextBox.Text, "</database>"));
                    __xmlStr.Append(string.Concat("<dts_server>", this._serverIpAddress.Text, "</dts_server>"));
                    __xmlStr.Append(String.Concat("</node>"));

                    string __xFileName = _global._smlConfigFile + "\\" + _global._dts_server_configFileName;
                    StreamWriter __sr = File.CreateText(__xFileName);
                    __sr.WriteLine(__xmlStr.ToString());
                    __sr.Close();
                }
                catch
                {
                }

                _global._getServerConnect();
                this._verifyScript();

                MyLib._myGlobal._userCode = this._agentcodeTextbox.Text;
            }
            else
            {
                MessageBox.Show("ไม่สามารถ เชื่อมต่อกับเครื่องแม่ข่ายได้", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _closeScreen()
        {
            MessageBox.Show("บันทึกสำเร็จ ระบบจะปิดจอนี้ให้โดยอัตโนมัติ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

        }

        Thread _verifyThread;
        int __maxTable = 0;
        XmlDocument _xmlScript;

        void _verifyScript()
        {
            // loadxml
            StreamReader __source = MyLib._getStream._getDataStream("dtsclientdatabase.xml");
            string __getXml = __source.ReadToEnd();
            __source.Close();

            _xmlScript = new XmlDocument();
            _xmlScript.LoadXml(__getXml);
            __maxTable = _xmlScript.SelectNodes("/node/table").Count;

            _verifyThread = new Thread(new ThreadStart(_startVerify));
            _verifyThread.IsBackground = true;
            _verifyThread.Start();
            timer1.Start();
            this.progressBar1.Maximum = __maxTable;
        }

        int _currentTable = 0;
        string _verifyTableName = "";
        void _startVerify()
        {
            //rename bcpurchaseorder agent_code to agentcode
            StringBuilder __script  = new StringBuilder();
            _clientFrameWork __frameWork = new _clientFrameWork();

            __script = new StringBuilder();
            __script.Append("sp_rename '" + _g.DataClient.dts_bcpurchaseorder._table + ".agent_code' , '" + _g.DataClient.dts_bcpurchaseorder._agentcode + "', 'COLUMN';");
            string __err = __frameWork._excute(__script.ToString());

            //rename bcsaleorder agent_code to agentcode
            __script = new StringBuilder();
            __script.Append("sp_rename '" + _g.DataClient.dts_bcsaleorder._table + ".agent_code' , '" + _g.DataClient.dts_bcsaleorder._agentcode + "', 'COLUMN';");
            __err = __frameWork._excute(__script.ToString());
            
            XmlNodeList __nodeList = _xmlScript.SelectNodes("/node/table");

            _verifyDatabase __verifyDB = new _verifyDatabase(_global._champServer, _global._champUserName, _global._champPassword, _global._champDatabaseName, 4);

            for (_currentTable = 0; _currentTable < __nodeList.Count; _currentTable++)
            {
                // อ่าน field มา
                _verifyTableName = __nodeList[_currentTable].Attributes["name"].Value.ToString();
                XmlNodeList __fieldList = __nodeList[_currentTable].ChildNodes;
                string __result = __verifyDB._verifyTable(_global._champDatabaseName, _verifyTableName, __fieldList);
                if (__result.Length > 0)
                {
                    MessageBox.Show(__result.ToString());
                }
            }

            _global._verifyClient();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this._resultLabel.Text = "Verify : " + _verifyTableName;
            this.progressBar1.Value = _currentTable;
            if (_currentTable == __maxTable)
            {
                timer1.Stop();
                _closeScreen();
            }
        }


        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            if (_testConnect())
            {
                MessageBox.Show("เชื่อมต่อสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ไม่สามารถ เชื่อมต่อกับเครื่องแม่ข่ายได้", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Boolean _testConnect()
        {
            return _global._sqlTestConnection(this._serverNameTextBox.Text, this._databaseNameTextBox.Text, this._userCodeTextBox.Text, this._userPasswordTextBox.Text);
        }

        Boolean _checkAgentCodeAndPassword()
        {
            Boolean __result = false;

            string __query = "select * from " + _g.DataServer.dts_agent._table + " where " + MyLib._myGlobal._addUpper(_g.DataServer.dts_agent._user_code) + "=\'" + this._agentcodeTextbox.Text.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(_g.DataServer.dts_agent._user_password) + "=\'" + this._agentPasswordTextbox.Text.ToUpper() + "\'";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);

            if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
            {
                for (int __i = 0; __i < __dataResult.Tables[0].Rows.Count; __i++)
                {
                    if (__dataResult.Tables[0].Rows[__i][_g.DataServer.dts_agent._user_code].ToString().ToUpper().Equals(this._agentcodeTextbox.Text.ToUpper()) && __dataResult.Tables[0].Rows[__i][_g.DataServer.dts_agent._user_password].ToString().ToUpper().Equals(this._agentPasswordTextbox.Text.ToUpper()))
                    {
                        return true;
                    }
                }
            }

            return __result;

        }

    }
}
