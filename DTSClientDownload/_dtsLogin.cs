using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DTSClientDownload
{
    public class _dtsLogin : Form // MyLib._databaseManage._userLogin
    {
        string _user_code = "user_code";
        private Button button1;
        private ToolStrip toolStrip1;
        private ToolStripButton _perferenceButton;
        private Label label1;
        private Label label2;
        private TextBox _userCodeTextbox;
        private TextBox _passwordTextbox;
        private Button button2;
        string _user_password = "user_password";
        public _dtsLogin()
        {
            InitializeComponent();
        }

        //protected override void _loginProcess()
        //{
            
        //}

        //protected override void _build()
        //{
        //    this._fancyLabel1.Text = "DTS Download";
        //    this._fancyLabel1._blurAmount = 0;
        //    this._fancyLabel1.Visible = false;
        //    this._screenTop._addTextBox(0, 0, 1, 0, "user_code", 1, 25, 0, true, false);
        //    this._screenTop._addTextBox(1, 0, 1, 0, "user_password", 1, 25, 0, true, true);

        //    this._emergencyButton.Visible = false;
        //    this._selectLanguage.Visible = false;
        //    this._proxyButton.Visible = false;
        //    this._keysToolStripButton.Visible = false;
        //    this._helpButton.Visible = false;
        //    this.toolStripSeparator1.Visible = false;

        //    this._myPanel1.BeginColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
        //    this._myPanel1.EndColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
        //    this._myPanel1._showLine = false;
        //    this._loginButton.Visible = false;

        //}

        //protected override void _onConfigWebService()
        //{
        //    _serverConfig __configServer = new _serverConfig();
        //    __configServer.ShowDialog();
        //}

        void _server_config()
        {
            //base._onConfigWebService();
            MyLib._databaseManage._providerConfig __provider = new MyLib._databaseManage._providerConfig(2, "Select Provider");
            __provider._selectCode = MyLib._myGlobal._providerCode;
            __provider.ShowDialog();
            if (__provider._exitMode == 1)
            {
                return;
            }

            MyLib._databaseManage._serverSetup __serverStartUp = new MyLib._databaseManage._serverSetup(__provider);
            MyLib._myUtil._startDialog(this, "", __serverStartUp);
            if (__serverStartUp._connectSuccess == true)
            {
                // เช็ค database name
                checkDatabaseServer();
                _verifyServer();
            }
        }

        /// <summary>
        /// สร้างตารางฝั่ง server
        /// </summary>
        void _verifyServer()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __script = new StringBuilder();

            //rename bcpurchaseorder agent_code to agentcode
            __script = new StringBuilder();
            __script.Append("sp_rename '" + _g.DataServer.bcpurchaseorder._table + ".agent_code' , '" + _g.DataServer.bcpurchaseorder._agentcode + "', 'COLUMN';");
            string __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            //rename bcsaleorder agent_code to agentcode
            __script = new StringBuilder();
            __script.Append("sp_rename '" + _g.DataServer.bcsaleorder._table + ".agent_code' , '" + _g.DataServer.bcsaleorder._agentcode + "', 'COLUMN';");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
            
            

            MyLib._databaseManage._verifyDatabase __verify = new MyLib._databaseManage._verifyDatabase(MyLib._myGlobal._databaseName);
            __verify.ShowDialog();

            // สร้างตารางฝั่ง server
            /*
                create table sync_send_data (
	                roworder int IDENTITY(1,1) NOT NULL,
	                agent_code	varchar(25) NOT NULL,
	                table_name varchar(100) NOT NULL,
	                trans_mode int default 0,
	                guid uniqueidentifier default NEWSEQUENTIALID(),
	                constraint pk_synd_send_data primary key nonclustered (roworder)
	
                )

                create nonclustered index idx_synd_send_data_agentcode on sync_send_data (agent_code asc);
                create nonclustered index idx_synd_send_data_syncguid on sync_send_data (guid asc);

             * 
             *  
             */


            __script = new StringBuilder();
            __script.Append("create table sync_send_data \r\n");
            __script.Append(" ( \r\n");
            __script.Append("roworder int IDENTITY(1,1) NOT NULL, \r\n");
            __script.Append("agent_code	varchar(25) NOT NULL, \r\n");
            __script.Append("table_name varchar(100) NOT NULL, \r\n");
            __script.Append("command_mode int default 0, \r\n");
            __script.Append("guid uniqueidentifier , \r\n");
            __script.Append("constraint pk_synd_send_data primary key nonclustered (roworder) \r\n");
            __script.Append(" ); \r\n");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            __script = new StringBuilder();
            __script.Append("create table sync_log \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder int IDENTITY(1,1) NOT NULL, \r\n");
            __script.Append("date_time datetime, \r\n");
            __script.Append("message text NULL, \r\n");
            __script.Append("branch_code varchar(25) NULL, \r\n");
            __script.Append("constraint pk_sync_log primary key nonclustered (roworder) \r\n");
            __script.Append(" ); \r\n");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            __script = new StringBuilder();
            __script.Append("create nonclustered index idx_synd_send_data_agentcode on sync_send_data (agent_code asc);");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            __script = new StringBuilder();
            __script.Append("create nonclustered index idx_synd_send_data_syncguid on sync_send_data (guid asc);");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            __script = new StringBuilder();
            __script.Append("create nonclustered index idx_sync_log_agentcode on sync_log (branch_code asc);");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            _creatScriptForSync(_g.DataServer.bcitem._table);
            _creatScriptForSync(_g.DataServer.bcstkpacking._table);
            _creatScriptForSync(_g.DataServer.allunitcode._table);
            _creatScriptForSync(_g.DataServer.bcpurchaseorder._table, true, 1);
            _creatScriptForSync(_g.DataServer.bcpurchaseordersub._table, true, 1);
            _creatScriptForSync(_g.DataServer.bcsaleorder._table, true, 1);
            _creatScriptForSync(_g.DataServer.bcsaleordersub._table, true, 1);

            _creatScriptForSync(_g.DataServer.dts_download._table, true, 2);
            _creatScriptForSync(_g.DataServer.dts_download_detail._table, true, 2);


            // ลบ field

            // delete discountword
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseorder._table + " drop column discountword");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete discountamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseorder._table + " drop column discountamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete afterdiscount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseorder._table + " drop column afterdiscount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete beforetaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseorder._table + " drop column beforetaxamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete excepttaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseorder._table + " drop column excepttaxamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete zerotaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseorder._table + " drop column zerotaxamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcpurchaseordersub docdate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseordersub._table + " drop column docdate");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcpurchaseordersub apcode
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseordersub._table + " drop column apcode");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcpurchaseordersub agent_code
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseordersub._table + " drop column agent_code");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcpurchaseordersub itemname
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseordersub._table + " drop column itemname");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcpurchaseordersub discountword
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseordersub._table + " drop column discountword");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcpurchaseordersub discountamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseordersub._table + " drop column discountamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
            
            // delete bcpurchaseordersub taxrate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcpurchaseordersub._table + " drop column taxrate");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleorder afterdiscount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleorder._table + " drop column afterdiscount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleorder beforetaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleorder._table + " drop column beforetaxamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleorder excepttaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleorder._table + " drop column excepttaxamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleorder zerotaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleorder._table + " drop column zerotaxamount");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleordersub docdate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleordersub._table + " drop column docdate");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleordersub arcode
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleordersub._table + " drop column arcode");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleordersub agent_code
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleordersub._table + " drop column agent_code");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

            // delete bcsaleordersub itemname
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleordersub._table + " drop column itemname");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());


            // delete bcsaleordersub taxrate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataServer.bcsaleordersub._table + " drop column taxrate");
            __resultexe = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
        }

        void _creatScriptForSync(string tableName)
        {
            _creatScriptForSync(tableName, false, 1);
        }

        void _debugResult(string __result)
        {
            if (__result.Length > 0)
            {
                Console.WriteLine(__result);
            }
        }



        void _creatScriptForSync(string tableName, Boolean _agentFilter, int mode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = "";

            #region Mode ส่งออก
            if (mode == 1)
            // ส่งออก
            {
                // add column tablename
                StringBuilder __script = new StringBuilder();

                if (__myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.MicrosoftSQL2000)
                {
                    __script.Append("ALTER TABLE " + tableName + " ADD guid uniqueidentifier default NEWID();");
                }
                else
                {
                    __script.Append("ALTER TABLE " + tableName + " ADD guid uniqueidentifier default NEWSEQUENTIALID();");
                }
                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

                // add index tablename
                __script = new StringBuilder();
                __script.Append("create nonclustered index idx_synd_send_guid on " + tableName + " (guid asc);");
                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

                // drop 
                string __triggerName = "send_sync_after_insert_update_" + tableName;

                __script = new StringBuilder();
                __script.Append("DROP TRIGGER " + __triggerName + "; ");
                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

                // create function on trigger
                // insert update trigger
                __script = new StringBuilder();
                if (_agentFilter == false)
                {

                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                    __script.Append("ON " + tableName + " \r\n");
                    __script.Append("AFTER INSERT,UPDATE \r\n");
                    __script.Append("AS \r\n");
                    //__script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                    //__script.Append("SELECT @guid =  i.guid FROM inserted i; \r\n");
                    __script.Append("begin \r\n");
                    __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                    __script.Append(" select a." + _g.DataServer.dts_agent._agent_code + ", '" + tableName + "', 1, i.guid from  inserted i, " + _g.DataServer.dts_agent._table + " a \r\n");
                    __script.Append("end \r\n");

                }
                else
                {
                    if (tableName.Equals(_g.DataServer.bcpurchaseordersub._table) || tableName.Equals(_g.DataServer.bcsaleordersub._table))
                    {
                        string __subqueryTableName = tableName.Equals(_g.DataServer.bcpurchaseordersub._table) ? _g.DataServer.bcpurchaseorder._table : _g.DataServer.bcsaleorder._table;

                        __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                        __script.Append("ON " + tableName + " \r\n");
                        __script.Append("AFTER INSERT,UPDATE \r\n");
                        __script.Append("AS \r\n");
                        //__script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                        //__script.Append("DECLARE @docno as varchar(25);");
                        //__script.Append("SELECT @guid =  i.guid FROM inserted i; \r\n");
                        //__script.Append("SELECT @docno = i.docno from inserted i;"); // (select agentcode from  " + __subqueryTableName + " where " + __subqueryTableName + ".docno = ( ))
                        __script.Append("begin \r\n");
                        __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                        __script.Append("(select ( select top 1 " + __subqueryTableName + ".agentcode from  " + __subqueryTableName + " where  " + __subqueryTableName + ".docno = i.docno) , '" + tableName + "', 1, i.guid from inserted i ) \r\n");
                        __script.Append("end \r\n");
                    }
                    else
                    {
                        __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                        __script.Append("ON " + tableName + " \r\n");
                        __script.Append("AFTER INSERT,UPDATE \r\n");
                        __script.Append("AS \r\n");
                        //__script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                        //__script.Append("DECLARE @agentcode as varchar(25);");
                        //__script.Append("SELECT @guid =  i.guid FROM inserted i; \r\n");
                        //__script.Append("SELECT @agentcode = i.agentcode from inserted i;");
                        __script.Append("begin \r\n");
                        __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                        __script.Append("(select i.agentcode, '" + tableName + "', 1, i.guid from inserted i ) \r\n");
                        __script.Append("end \r\n");
                    }
                }

                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

                //  delete trigger

                __triggerName = "send_sync_after_delete_" + tableName;

                __script = new StringBuilder();
                __script.Append("DROP TRIGGER " + __triggerName + "; ");
                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

                __script = new StringBuilder();
                if (_agentFilter == false)
                {

                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                    __script.Append("ON " + tableName + " \r\n");
                    __script.Append("AFTER DELETE \r\n");
                    __script.Append("AS \r\n");
                    //__script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                    //__script.Append("SELECT @guid =  i.guid FROM deleted i; \r\n");
                    __script.Append("begin \r\n");
                    __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                    __script.Append(" select a." + _g.DataServer.dts_agent._agent_code + ", '" + tableName + "', 2, i.guid from deleted i, " + _g.DataServer.dts_agent._table + " a \r\n");
                    __script.Append("end \r\n");

                }
                else
                {
                    if (tableName.Equals(_g.DataServer.bcpurchaseordersub._table) || tableName.Equals(_g.DataServer.bcsaleordersub._table))
                    {
                        string __subqueryTableName = tableName.Equals(_g.DataServer.bcpurchaseordersub._table) ? _g.DataServer.bcpurchaseorder._table : _g.DataServer.bcsaleorder._table;

                        __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                        __script.Append("ON " + tableName + " \r\n");
                        __script.Append("AFTER DELETE \r\n");
                        __script.Append("AS \r\n");
                        //__script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                        //__script.Append("DECLARE @docno as varchar(25);");
                        //__script.Append("SELECT @guid =  i.guid FROM deleted i; \r\n");
                        //__script.Append("SELECT @docno = i.docno from inserted i;"); // (select agentcode from  " + __subqueryTableName + " where " + __subqueryTableName + ".docno = ())
                        __script.Append("begin \r\n");
                        __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                        __script.Append("(select ( select top 1 " + __subqueryTableName + ".agentcode from  " + __subqueryTableName + " where  " + __subqueryTableName + ".docno = i.docno) , '" + tableName + "', 2, i.guid from deleted i ) \r\n");
                        __script.Append("end \r\n");
                    }
                    else
                    {
                        __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                        __script.Append("ON " + tableName + " \r\n");
                        __script.Append("AFTER DELETE \r\n");
                        __script.Append("AS \r\n");
                        //__script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                        //__script.Append("DECLARE @agentcode as varchar(25);");
                        //__script.Append("SELECT @guid =  i.guid FROM deleted i; \r\n");
                        //__script.Append("SELECT @agentcode = i.agentcode from deleted i;");
                        __script.Append("begin \r\n");
                        __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                        __script.Append("( select i.agentcode, '" + tableName + "', 2, i.guid from deleted i ) \r\n");
                        __script.Append("end \r\n");
                    }
                }
                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

            }
            #endregion
            #region Mode รับเข้า
            else if (mode == 2)
            // รับเข้า
            {
                StringBuilder __script = new StringBuilder();
                __script.Append("ALTER TABLE " + tableName + " ADD guid UNIQUEIDENTIFIER;");
                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

            }
            #endregion
            #region Mode ส่งออกและรับเข้า
            else if (mode == 3)
            // ส่งออก และรับเข้า
            {
                StringBuilder __script = new StringBuilder();
                __script.Append("ALTER TABLE " + tableName + " ADD guid UNIQUEIDENTIFIER default NEWSEQUENTIALID();");
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                // after delete
                string __triggerName = "send_sync_after_delete_" + tableName;
                __script = new StringBuilder();

                if (_agentFilter == false)
                {
                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                    __script.Append("ON " + tableName + " \r\n");
                    __script.Append("AFTER DELETE \r\n");
                    __script.Append("AS \r\n");
                    __script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                    __script.Append("SELECT @guid =  i.guid FROM deleted i; \r\n");
                    __script.Append("begin \r\n");
                    __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                    __script.Append(" select " + _g.DataServer.dts_agent._agent_code + ", '" + tableName + "', 2, @guid from " + _g.DataServer.dts_agent._table + " \r\n");
                    __script.Append("end \r\n");

                }
                else
                {
                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                    __script.Append("ON " + tableName + " \r\n");
                    __script.Append("AFTER DELETE \r\n");
                    __script.Append("AS \r\n");
                    __script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                    __script.Append("DECLARE @agentcode as varchar(25);");
                    __script.Append("SELECT @guid = i.guid FROM deleted i; \r\n");
                    __script.Append("SELECT @agentcode = i.agentcode from deleted i;");
                    __script.Append("begin \r\n");
                    __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                    __script.Append("values(@agentcode, '" + tableName + "', 2, @guid ) \r\n");
                    __script.Append("end \r\n");
                }

                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

                // after insert,update
                __triggerName = "send_sync_after_insert_update_" + tableName;
                if (_agentFilter == false)
                {

                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                    __script.Append("ON " + tableName + " \r\n");
                    __script.Append("AFTER INSERT,UPDATE \r\n");
                    __script.Append("AS \r\n");
                    __script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                    __script.Append("SELECT @guid =  i.guid FROM inserted i; \r\n");
                    __script.Append("begin \r\n");
                    __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                    __script.Append(" select agentcode, '" + tableName + "', 1, @guid from " + _g.DataServer.dts_agent._table + " \r\n");
                    __script.Append("end \r\n");

                }
                else
                {
                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                    __script.Append("ON " + tableName + " \r\n");
                    __script.Append("AFTER INSERT,UPDATE \r\n");
                    __script.Append("AS \r\n");
                    __script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
                    __script.Append("DECLARE @agentcode as varchar(25);");
                    __script.Append("SELECT @guid =  i.guid FROM inserted i; \r\n");
                    __script.Append("SELECT @agentcode = i.agentcode from inserted i;");
                    __script.Append("begin \r\n");
                    __script.Append("insert into sync_send_data(agent_code, table_name, command_mode, guid) \r\n");
                    __script.Append("values(@agentcode, '" + tableName + "', 1, @guid ) \r\n");
                    __script.Append("end \r\n");
                }

                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                _debugResult(__result);

            }
            #endregion

        }

        void checkDatabaseServer()
        {
            bool __found = false;

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            try
            {
                __found = __myFrameWork._findDatabase(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName);


            }
            catch
            {
            }

            
                //string __query = " select count from " + MyLib._d.sml_database_list._table + " where data_code = \'" + MyLib._myGlobal._databaseName + "\'";  //"select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + "  from " + MyLib._d.sml_database_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_group) + "=\'" + MyLib._myGlobal._dataGroup.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=0 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\') or " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=1 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._group_code) + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')) order by " + MyLib._d.sml_database_list._data_name;
                ////query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + " from " + MyLib._d.sml_database_list._table;
                //DataSet __dbResult = __myFrameWork._query(MyLib._myGlobal._databaseConfig, __query);

                //if (__dbResult.Tables.Count == 0)
                //{
                //    // insert now
                //    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseConfig, "insert into sml_database_list(data_group, data_code,data_name,data_database_name) values (\'SML\', \'" + MyLib._myGlobal._databaseName + "\', \'" + MyLib._myGlobal._databaseName + "\', \'" + MyLib._myGlobal._databaseName + "\' ) ");
                //}
                //{
                //    DataRow[] __getRows = __dbResult.Tables[0].Select();

                //    for (int __row = 0; __row < __getRows.Length; __row++)
                //    {
                //        //_gridDatabaseList._addRow();
                //        //int rowDataGrid = _gridDatabaseList._rowData.Count - 1;
                //        //_gridDatabaseList._cellUpdate(rowDataGrid, 0, __getRows[__row].ItemArray[0].ToString(), false);
                //        //_gridDatabaseList._cellUpdate(rowDataGrid, 1, __getRows[__row].ItemArray[1].ToString(), false);
                //        if (__getRows[__row].ItemArray[0].ToString().Equals(MyLib._myGlobal._databaseName))
                //        {
                //            __found = true;
                //            break;
                //        }
                //    } // for
                //}
            
            if (__found == false)
            {
                if (MessageBox.Show("ไม่พบฐานข้อมูลที่ใช้งาน คุณต้องการสร้างฐานข้อมูลใหม่หรือไม่", DTSClientDownload._global._champMessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    // สร้าง ใหม่
                    MyLib._databaseManage._createDatabase _createDatabase = new MyLib._databaseManage._createDatabase(1);
                    _createDatabase.ShowDialog();

                }
            }
            else
            {
                string __query = " select count from " + MyLib._d.sml_database_list._table + " where data_code = \'" + MyLib._myGlobal._databaseName + "\'";  //"select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + "  from " + MyLib._d.sml_database_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_group) + "=\'" + MyLib._myGlobal._dataGroup.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=0 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\') or " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=1 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._group_code) + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')) order by " + MyLib._d.sml_database_list._data_name;
                //query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + " from " + MyLib._d.sml_database_list._table;
                DataSet __dbResult = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);

                if (__dbResult.Tables.Count == 0)
                {
                    // insert now
                    string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, "insert into sml_database_list(data_group, data_code,data_name,data_database_name) values (\'SML\', \'" + MyLib._myGlobal._databaseName + "\', \'" + MyLib._myGlobal._databaseName + "\', \'" + MyLib._myGlobal._databaseName + "\' ) ");
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.F2:
                    _server_config();
                    return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _global._getServerConnect();
            if (_global._sqlTestConnection() == false)
            {
                MessageBox.Show("ไม่สามารถเชื่อมต่อกับแม่ข่าย CHAMP ได้", DTSClientDownload._global._champMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }


            string __userCode = this._userCodeTextbox.Text;// this._screenTop._getDataStr("user_code");
            string __userPassword = this._passwordTextbox.Text;// this._screenTop._getDataStr("user_password");

            string __query = "select * from dts_user where " + MyLib._myGlobal._addUpper(_user_code) + "=\'" + __userCode.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(_user_password) + "=\'" + __userPassword.ToUpper() + "\'";
            _clientFrameWork __FrameWork = new _clientFrameWork();
            DataSet __result = __FrameWork._query(__query);
            bool __loginSuccess = false;

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                {
                    if (__result.Tables[0].Rows[__i][_user_code].ToString().ToUpper().Equals(__userCode.ToUpper()) && __result.Tables[0].Rows[__i][_user_password].ToString().ToUpper().Equals(__userPassword.ToUpper()))
                    {
                        __loginSuccess = true;
                        MyLib._myGlobal._userCode = __result.Tables[0].Rows[__i]["agent_code"].ToString();
                        MyLib._myGlobal._userName = __result.Tables[0].Rows[__i]["agent_name"].ToString();

                    }
                }
            }

            if (__loginSuccess)
            {
                // check server config

                string __message_1 = string.Format("ยินดีต้อนรับ [{0}] เข้าสู่ระบบ\nท่านสามารถทำรายการต่อไปได้", MyLib._myGlobal._userName);
                string __message_2 = MyLib._myGlobal._resource("warning94");
                MessageBox.Show(__message_1, DTSClientDownload._global._champMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //_saveProfile(MyLib._myGlobal._providerCode, "SML", __userCode, false);
                MyLib._myGlobal._userLoginSuccess = true;
                Close();

            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("warning90"), DTSClientDownload._global._champMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            /* // old
            string __userCode = this._screenTop._getDataStr("user_code");
            string __userPassword = this._screenTop._getDataStr("user_password");

            string __query = "select * from " + _g.DataServer.dts_agent._table + " where " + MyLib._myGlobal._addUpper(_g.DataServer.dts_agent._user_code) + "=\'" + __userCode.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(_g.DataServer.dts_agent._user_password) + "=\'" + __userPassword.ToUpper() + "\'";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);

            bool __loginSuccess = false;

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                {
                    if (__result.Tables[0].Rows[__i][_g.DataServer.dts_agent._user_code].ToString().ToUpper().Equals(__userCode.ToUpper()) && __result.Tables[0].Rows[__i][_g.DataServer.dts_agent._user_password].ToString().ToUpper().Equals(__userPassword.ToUpper()))
                    {
                        __loginSuccess = true;
                        MyLib._myGlobal._userCode = __result.Tables[0].Rows[__i][_g.DataServer.dts_agent._agent_code].ToString();
                        MyLib._myGlobal._userName = __result.Tables[0].Rows[__i][_g.DataServer.dts_agent._agent_name].ToString();

                    }
                }
            }

            if (__loginSuccess)
            {
                // check server config
                _global._getServerConnect();
                if (_global._sqlTestConnection())
                {
                    string __message_1 = string.Format("ยินดีต้อนรับ [{0}] เข้าสู่ระบบ\nท่านสามารถทำรายการต่อไปได้", MyLib._myGlobal._userName);
                    string __message_2 = MyLib._myGlobal._resource("warning94");
                    MessageBox.Show(__message_1, __message_2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _saveProfile(MyLib._myGlobal._providerCode, "SML", __userCode, false);
                    MyLib._myGlobal._userLoginSuccess = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("ไม่สามารถเชื่อมต่อกับแม่ข่าย CHAMP ได้", MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("warning90"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }*/
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_dtsLogin));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._perferenceButton = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._userCodeTextbox = new System.Windows.Forms.TextBox();
            this._passwordTextbox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._perferenceButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(280, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _perferenceButton
            // 
            this._perferenceButton.Image = global::DTSClientDownload.Properties.Resources.gear_connection;
            this._perferenceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._perferenceButton.Name = "_perferenceButton";
            this._perferenceButton.Size = new System.Drawing.Size(142, 22);
            this._perferenceButton.Text = "สร้างการเชื่อมโยงข้อมูล";
            this._perferenceButton.Click += new System.EventHandler(this._perferenceButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "รหัสผู้ใช้ :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "รหัสผ่าน :";
            // 
            // _userCodeTextbox
            // 
            this._userCodeTextbox.Location = new System.Drawing.Point(86, 42);
            this._userCodeTextbox.Name = "_userCodeTextbox";
            this._userCodeTextbox.Size = new System.Drawing.Size(182, 22);
            this._userCodeTextbox.TabIndex = 3;
            // 
            // _passwordTextbox
            // 
            this._passwordTextbox.Location = new System.Drawing.Point(86, 70);
            this._passwordTextbox.Name = "_passwordTextbox";
            this._passwordTextbox.PasswordChar = '*';
            this._passwordTextbox.Size = new System.Drawing.Size(182, 22);
            this._passwordTextbox.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Image = global::DTSClientDownload.Properties.Resources._lock;
            this.button2.Location = new System.Drawing.Point(172, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "เข้าสู่ระบบ";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // _dtsLogin
            // 
            this.ClientSize = new System.Drawing.Size(280, 127);
            this.Controls.Add(this.button2);
            this.Controls.Add(this._passwordTextbox);
            this.Controls.Add(this._userCodeTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_dtsLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เลือกข้อมูล";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void _perferenceButton_Click(object sender, EventArgs e)
        {
            _serverConfig __configServer = new _serverConfig();
            __configServer.ShowDialog();
        }

    }
}
