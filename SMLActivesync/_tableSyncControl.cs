using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLActivesync
{
    public partial class _tableSyncControl : UserControl
    {
        private string _gridSelect = "Select";
        private List<_fieldListClass> _fieldList = new List<_fieldListClass>();

        public _tableSyncControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip3.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._tableGrid._table_name = _g.d.sync_table_list._table;
            this._tableGrid._addColumn(this._gridSelect, 11, 10, 10);
            this._tableGrid._addColumn(_g.d.sync_table_list._table_name, 1, 30, 30, false, false, true);
            this._tableGrid._addColumn(_g.d.sync_table_list._trans_type, 1, 30, 30, false, false, true);
            this._tableGrid._addColumn(_g.d.sync_table_list._trans_command, 1, 30, 30, true, false, true);
            //this._tableGrid._isEdit = false;
            this._tableGrid.Invalidate();
            //
            this._fieldGrid._table_name = _g.d.sync_field_list._table;
            this._fieldGrid._addColumn(this._gridSelect, 11, 20, 20);
            this._fieldGrid._addColumn(_g.d.sync_field_list._field_name, 1, 80, 40, false, false);
            this._fieldGrid._addColumn(_g.d.sync_field_list._default_value, 1, 80, 40);
            this._fieldGrid._isEdit = true;
            this._fieldGrid.Invalidate();
            this._fieldGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_fieldGrid__alterCellUpdate);
            //
            this.Load += new EventHandler(_branchControl_Load);
            this._tableGrid._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_tableGrid__afterSelectRow);
            //
            this._tableGrid._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_tableGrid__beforeDisplayRendering);
            //
            this._saveButton.Click += (s1, e1) =>
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate table " + _g.d.sync_table_list._table));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate table " + _g.d.sync_field_list._table));
                // Save Table
                for (int __row = 0; __row < this._tableGrid._rowData.Count; __row++)
                {
                    int __select = (int)MyLib._myGlobal._decimalPhase(this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(this._gridSelect)).ToString());
                    if (__select == 1)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.sync_table_list._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.sync_table_list._table_name, _g.d.sync_table_list._trans_type, _g.d.sync_table_list._trans_command) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + this._tableGrid._cellGet(__row, _g.d.sync_table_list._table_name).ToString() + "\'", this._tableGrid._cellGet(__row, _g.d.sync_table_list._trans_type).ToString(), "\'" + MyLib._myGlobal._convertStrToQuery(this._tableGrid._cellGet(__row, _g.d.sync_table_list._trans_command).ToString()) + "\'") + ")"));
                    }
                }
                // Save Field
                for (int __row = 0; __row < this._fieldList.Count; __row++)
                {
                    if (this._fieldList[__row]._selected)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.sync_field_list._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.sync_field_list._table_name, _g.d.sync_field_list._field_name, _g.d.sync_field_list._default_value) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + this._fieldList[__row]._tableName + "\'", "\'" + this._fieldList[__row]._fieldName + "\',\'" + this._fieldList[__row]._default_value + "\')")));
                    }
                }
                __query.Append("</node>");
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                    // สร้าง Table sync_send_data_truncate_table

                    StringBuilder __script = new StringBuilder();

                    switch (__myFrameWork._databaseSelectType)
                    {
                        case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                        case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                            {
                                __script = new StringBuilder();
                                __script.Append("CREATE TABLE sync_send_data_truncate_table \r\n");
                                __script.Append("( \r\n");
                                __script.Append("roworder int IDENTITY(1,1) NOT NULL, \r\n");
                                __script.Append("branch_code character varying(100), \r\n");
                                __script.Append("table_name character varying(100), \r\n");
                                __script.Append("CONSTRAINT sync_send_data_truncate_table_idx PRIMARY KEY (roworder) \r\n");
                                __script.Append("); \r\n");

                                //__script.Append("WITH ( \r\n");
                                //__script.Append("OIDS=FALSE \r\n");
                                //__script.Append("); \r\n");
                                //__script.Append("ALTER TABLE sync_send_data_truncate_table OWNER TO postgres; \r\n");
                                //__script.Append("CREATE INDEX sync_send_data_truncate_table_idx1 \r\n");
                                //__script.Append("ON sync_send_data_truncate_table \r\n");
                                //__script.Append("USING btree \r\n");
                                //__script.Append("(branch_code); \r\n");
                                //__script.Append("CREATE INDEX sync_send_data_truncate_table_idx2 \r\n");
                                //__script.Append("ON sync_send_data_truncate_table \r\n");
                                //__script.Append("USING btree \r\n");
                                //__script.Append("(branch_code, table_name); \r\n");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());


                                // sync_send_data
                                __script = new StringBuilder();
                                __script.Append("create table sync_send_data \r\n");
                                __script.Append(" ( \r\n");
                                __script.Append("roworder int IDENTITY(1,1) NOT NULL, \r\n");
                                __script.Append("branch_code varchar(100) NOT NULL, \r\n");
                                __script.Append("table_name varchar(100) NOT NULL, \r\n");
                                __script.Append("command_mode int default 0, \r\n");
                                __script.Append("guid uniqueidentifier , \r\n");
                                __script.Append("CONSTRAINT pk_synd_send_data primary key nonclustered (roworder) \r\n");
                                __script.Append(" ); \r\n");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                __script = new StringBuilder();
                                __script.Append("create nonclustered index idx_synd_send_data_agentcode on sync_send_data (branch_code asc);");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                __script = new StringBuilder();
                                __script.Append("create nonclustered index idx_synd_send_data_syncguid on sync_send_data (guid asc);");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                __script = new StringBuilder();
                                __script.Append("create nonclustered index idx_synd_send_data_tablename_branch_code on sync_send_data (table_name asc,branch_code asc);");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                            }
                            break;
                        default:
                            {
                                __script = new StringBuilder();
                                __script.Append("CREATE TABLE sync_send_data_truncate_table \r\n");
                                __script.Append("( \r\n");
                                __script.Append("roworder serial NOT NULL, \r\n");
                                __script.Append("branch_code character varying(100), \r\n");
                                __script.Append("table_name character varying(100), \r\n");
                                __script.Append("CONSTRAINT sync_send_data_truncate_table_idx PRIMARY KEY (roworder) \r\n");
                                __script.Append(") \r\n");
                                __script.Append("WITH ( \r\n");
                                __script.Append("OIDS=FALSE \r\n");
                                __script.Append("); \r\n");
                                __script.Append("ALTER TABLE sync_send_data_truncate_table OWNER TO postgres; \r\n");
                                __script.Append("CREATE INDEX sync_send_data_truncate_table_idx1 \r\n");
                                __script.Append("ON sync_send_data_truncate_table \r\n");
                                __script.Append("USING btree \r\n");
                                __script.Append("(branch_code); \r\n");
                                __script.Append("CREATE INDEX sync_send_data_truncate_table_idx2 \r\n");
                                __script.Append("ON sync_send_data_truncate_table \r\n");
                                __script.Append("USING btree \r\n");
                                __script.Append("(branch_code, table_name); \r\n");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                // sync_reset_receive_data
                                __script = new StringBuilder();
                                __script.Append("CREATE TABLE sync_reset_receive_data \r\n");
                                __script.Append("( \r\n");
                                __script.Append("roworder serial NOT NULL, \r\n");
                                __script.Append("branch_code character varying(100), \r\n");
                                __script.Append("table_name character varying(100), \r\n");
                                __script.Append("CONSTRAINT sync_reset_receive_data_idx PRIMARY KEY (roworder) \r\n");
                                __script.Append(") \r\n");
                                __script.Append("WITH ( \r\n");
                                __script.Append("OIDS=FALSE \r\n");
                                __script.Append("); \r\n");
                                __script.Append("ALTER TABLE sync_reset_receive_data OWNER TO postgres; \r\n");
                                __script.Append("CREATE INDEX sync_reset_receive_data_idx1 \r\n");
                                __script.Append("ON sync_reset_receive_data \r\n");
                                __script.Append("USING btree \r\n");
                                __script.Append("(branch_code); \r\n");
                                __script.Append("CREATE INDEX sync_reset_receive_data_idx2 \r\n");
                                __script.Append("ON sync_reset_receive_data \r\n");
                                __script.Append("USING btree \r\n");
                                __script.Append("(branch_code, table_name); \r\n");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                // สร้าง Table sync_send_data
                                __script = new StringBuilder();
                                __script.Append("CREATE TABLE sync_send_data \r\n");
                                __script.Append("( \r\n");
                                __script.Append("roworder serial NOT NULL, \r\n");
                                __script.Append("branch_code character varying(100), \r\n");
                                __script.Append("table_name character varying(100), \r\n");
                                __script.Append("command_mode integer DEFAULT 0, \r\n");
                                __script.Append("guid uuid, \r\n");
                                __script.Append("CONSTRAINT sync_send_data_idx PRIMARY KEY (roworder) \r\n");
                                __script.Append(") \r\n");
                                __script.Append("WITH ( \r\n");
                                __script.Append("OIDS=FALSE \r\n");
                                __script.Append("); \r\n");
                                __script.Append("ALTER TABLE sync_send_data OWNER TO postgres; \r\n");
                                __script.Append("CREATE INDEX syn_send_data_idx_guid \r\n");
                                __script.Append("ON sync_send_data \r\n");
                                __script.Append("USING btree \r\n");
                                __script.Append("(guid); \r\n");
                                __script.Append("CREATE INDEX sync_send_data_idx1 \r\n");
                                __script.Append("ON sync_send_data \r\n");
                                __script.Append("USING btree \r\n");
                                __script.Append("(branch_code); \r\n");
                                __script.Append("CREATE INDEX sync_send_data_idx2 \r\n");
                                __script.Append("ON sync_send_data \r\n");
                                __script.Append("USING btree \r\n");
                                __script.Append("(branch_code, table_name); \r\n");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                // Script new()
                                __script = new StringBuilder();
                                __script.Append("CREATE OR REPLACE FUNCTION newid() \r\n");
                                __script.Append("RETURNS uuid AS \r\n");
                                __script.Append("$BODY$\r\n");
                                __script.Append("SELECT CAST(md5(current_database()|| user ||current_timestamp ||random()) as uuid) \r\n");
                                __script.Append("$BODY$ \r\n");
                                __script.Append("LANGUAGE sql VOLATILE \r\n");
                                __script.Append("COST 100; \r\n");
                                __script.Append("ALTER FUNCTION newid() OWNER TO postgres; \r\n");
                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                //// script sync_trans
                                //__script = new StringBuilder();
                                //__script.Append("");
                                //__script.Append("");
                                //__script.Append("");
                                //__script.Append("");
                                //__script.Append("");
                                //__script.Append("");
                            }
                            break;
                    }

                    // สร้าง Script ฝั่ง Data Center (ส่งออก)
                    for (int __row = 0; __row < this._tableGrid._rowData.Count; __row++)
                    {
                        int __select = (int)MyLib._myGlobal._decimalPhase(this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(this._gridSelect)).ToString());
                        if (__select == 1)
                        {
                            int __mode = (int)MyLib._myGlobal._decimalPhase(this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(_g.d.sync_table_list._trans_type)).ToString());
                            string __tableName = this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(_g.d.sync_table_list._table_name)).ToString().ToLower();
                            string __tableCommand = this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(_g.d.sync_table_list._trans_command)).ToString().ToLower();

                            switch (__mode)
                            {
                                #region Mode 1 ส่งข้อมูล
                                case 1:
                                case 3:
                                    {
                                        // toe for sql server
                                        switch (__myFrameWork._databaseSelectType)
                                        {
                                            #region MSSQL
                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                {
                                                    __script = new StringBuilder();
                                                    if (__myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.MicrosoftSQL2000)
                                                    {
                                                        // sql server 2000
                                                        __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWID();");
                                                    }
                                                    else
                                                    {
                                                        // sql server 2005
                                                        __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWSEQUENTIALID();");
                                                    }
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                    // check empty guid
                                                    string __checkGuidNullSQL = "select count(*) as xcount from  " + __tableName + " where guid is null ";
                                                    DataTable __getCheckGuid = __myFrameWork._queryShort(__checkGuidNullSQL).Tables[0];

                                                    if (__getCheckGuid.Rows.Count > 0)
                                                    {
                                                        if (MyLib._myGlobal._decimalPhase(__getCheckGuid.Rows[0][0].ToString()) > 0)
                                                        {
                                                            __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set guid = NEWID() where guid is null ");
                                                        }
                                                    }

                                                    // guid index
                                                    __script = new StringBuilder();
                                                    __script.Append("create nonclustered index " + __tableName + "_guid_idx on " + __tableName + " (guid asc);");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                    string __triggerName = "send_" + __tableName + "_after_insert_or_update";

                                                    __script = new StringBuilder();
                                                    __script.Append("DROP TRIGGER " + __triggerName + "; ");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                    // check trans_command 
                                                    string __extraCommand = "";

                                                    if (__tableCommand.Length > 0)
                                                    {
                                                        __tableCommand = __tableCommand.Replace("&branch_code&", "a." + _g.d.sync_branch_list._branch_code);
                                                        __tableCommand = __tableCommand.Replace("&table_sync&", "i");

                                                        __extraCommand = " where " + __tableCommand;
                                                    }

                                                    //creat trigger
                                                    __script = new StringBuilder();
                                                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                                                    __script.Append("ON " + __tableName + " \r\n");
                                                    __script.Append("AFTER INSERT,UPDATE \r\n");
                                                    __script.Append("AS \r\n");
                                                    __script.Append("begin \r\n");
                                                    __script.Append("insert into sync_send_data(branch_code,table_name,command_mode,guid) \r\n");
                                                    __script.Append(" select a.branch_code, '" + __tableName + "', 1, i.guid from  inserted i, sync_branch_list a " + __extraCommand + " \r\n");
                                                    __script.Append("end \r\n");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                    __triggerName = "send_sync_after_delete_" + __tableName;

                                                    __script = new StringBuilder();
                                                    __script.Append("DROP TRIGGER " + __triggerName + "; ");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                    __script = new StringBuilder();
                                                    __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
                                                    __script.Append("ON " + __tableName + " \r\n");
                                                    __script.Append("AFTER DELETE \r\n");
                                                    __script.Append("AS \r\n");
                                                    __script.Append("begin \r\n");
                                                    __script.Append("insert into sync_send_data(branch_code, table_name, command_mode, guid) \r\n");
                                                    __script.Append(" select a.branch_code, '" + __tableName + "', 2, i.guid from deleted i, sync_branch_list a " + __extraCommand + " \r\n");
                                                    __script.Append("end \r\n");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                }
                                                break;
                                            #endregion
                                            #region PostgreSQL
                                            default:
                                                {

                                                    __script = new StringBuilder();
                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid DEFAULT newid()");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                    __script = new StringBuilder();
                                                    __script.Append("CREATE INDEX " + __tableName + "_guid_idx  \r\n");
                                                    __script.Append("ON " + __tableName + " \r\n");
                                                    __script.Append("USING btree \r\n");
                                                    __script.Append("(guid); \r\n");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                    // check empty guid
                                                    string __checkGuidNullSQL = "select count(*) as xcount from  " + __tableName + " where guid is null ";
                                                    DataTable __getCheckGuid = __myFrameWork._queryShort(__checkGuidNullSQL).Tables[0];

                                                    if (__getCheckGuid.Rows.Count > 0)
                                                    {
                                                        if (MyLib._myGlobal._decimalPhase(__getCheckGuid.Rows[0][0].ToString()) > 0)
                                                        {
                                                            __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set guid = newid() where guid is null ");
                                                        }
                                                    }

                                                    string __extraCommand = "";

                                                    if (__tableCommand.Length > 0)
                                                    {
                                                        string __extraInsertUpdateCommand = __tableCommand;
                                                        __extraInsertUpdateCommand = __extraInsertUpdateCommand.Replace("&branch_code&", _g.d.sync_branch_list._table + "." + _g.d.sync_branch_list._branch_code);
                                                        __extraInsertUpdateCommand = __extraInsertUpdateCommand.Replace("&table_sync&", "NEW");

                                                        __extraCommand = " where " + __extraInsertUpdateCommand;
                                                    }

                                                    if (__mode == 3)
                                                    {
                                                        if (__extraCommand.Length == 0)
                                                        {
                                                            __extraCommand += " where ";
                                                        }
                                                        else
                                                        {
                                                            __extraCommand += " and ";
                                                        }
                                                        __extraCommand += " sync_branch_list.branch_code <> NEW.branch_sync ";
                                                    }

                                                    string __triggerInsertUpdateName = "send_" + __tableName + "_after_insert_or_update()";
                                                    string __triggerDeleteName = "send_" + __tableName + "_after_delete()";
                                                    // drop trigger ก่อน
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "DROP TRIGGER send_" + __tableName + "_after_insert_or_update_trigger ON " + __tableName + ";");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "DROP FUNCTION " + __triggerInsertUpdateName + ";");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "DROP TRIGGER send_" + __tableName + "_after_delete_trigger ON " + __tableName + ";");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "DROP FUNCTION " + __triggerDeleteName + ";");


                                                    // insert or update (command_mode=1)

                                                    __script = new StringBuilder();
                                                    __script.Append("CREATE OR REPLACE FUNCTION " + __triggerInsertUpdateName + " \r\n");
                                                    __script.Append("RETURNS trigger AS \r\n");
                                                    __script.Append("$BODY$ \r\n");
                                                    __script.Append("BEGIN \r\n");
                                                    __script.Append("insert into sync_send_data (branch_code,table_name,command_mode,guid) (select branch_code,'" + __tableName + "',1,NEW.guid  from sync_branch_list " + __extraCommand + " ); \r\n");
                                                    __script.Append("RETURN NEW; \r\n");
                                                    __script.Append("END; \r\n");
                                                    __script.Append("$BODY$ \r\n");
                                                    __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                    __script.Append("COST 100; \r\n");
                                                    __script.Append("ALTER FUNCTION " + __triggerInsertUpdateName + " OWNER TO postgres; \r\n");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                    // insert or update trigger
                                                    __script = new StringBuilder();
                                                    __script.Append("CREATE TRIGGER send_" + __tableName + "_after_insert_or_update_trigger \r\n");
                                                    __script.Append("AFTER INSERT or UPDATE \r\n");
                                                    __script.Append("ON " + __tableName + " \r\n");
                                                    __script.Append("FOR EACH ROW \r\n");
                                                    __script.Append("EXECUTE PROCEDURE " + __triggerInsertUpdateName + "; \r\n");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                    // delete (command_mode=2)
                                                    //__triggerName = "send_" + __tableName + "_after_delete()";

                                                    __extraCommand = "";
                                                    if (__tableCommand.Length > 0)
                                                    {
                                                        string __extraDeleteCommand = __tableCommand;
                                                        __extraDeleteCommand = __extraDeleteCommand.Replace("&branch_code&", _g.d.sync_branch_list._table + "." + _g.d.sync_branch_list._branch_code);
                                                        __extraDeleteCommand = __extraDeleteCommand.Replace("&table_sync&", "OLD");

                                                        __extraCommand = " where " + __extraDeleteCommand;
                                                    }

                                                    if (__mode == 3)
                                                    {
                                                        if (__extraCommand.Length == 0)
                                                        {
                                                            __extraCommand += " where ";
                                                        }
                                                        else
                                                        {
                                                            __extraCommand += " and ";
                                                        }
                                                        __extraCommand += " sync_branch_list.branch_code <> OLD.branch_sync ";
                                                    }


                                                    __script = new StringBuilder();
                                                    __script.Append("CREATE OR REPLACE FUNCTION " + __triggerDeleteName + " \r\n");
                                                    __script.Append("RETURNS trigger AS \r\n");
                                                    __script.Append("$BODY$ \r\n");
                                                    __script.Append("BEGIN \r\n");
                                                    __script.Append("insert into sync_send_data (branch_code,table_name,command_mode,guid) (select branch_code,'" + __tableName + "',2, OLD.guid  from sync_branch_list " + __extraCommand + " ); \r\n");
                                                    __script.Append("RETURN NEW; \r\n");
                                                    __script.Append("END; \r\n");
                                                    __script.Append("$BODY$ \r\n");
                                                    __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                    __script.Append("COST 100; \r\n");
                                                    __script.Append("ALTER FUNCTION " + __triggerDeleteName + " OWNER TO postgres; \r\n");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                    // insert or update trigger
                                                    __script = new StringBuilder();
                                                    __script.Append("CREATE TRIGGER send_" + __tableName + "_after_delete_trigger \r\n");
                                                    __script.Append("AFTER delete \r\n");
                                                    __script.Append("ON " + __tableName + " \r\n");
                                                    __script.Append("FOR EACH ROW \r\n");
                                                    __script.Append("EXECUTE PROCEDURE " + __triggerDeleteName + "; \r\n");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                }
                                                break;
                                            #endregion
                                        }
                                    }
                                    break;
                                #endregion
                                #region Mode 2 รับข้อมูล
                                case 2:
                                    {
                                        switch (__myFrameWork._databaseSelectType)
                                        {
                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                {
                                                    __script = new StringBuilder();
                                                    __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier");
                                                    __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                }
                                                break;
                                            default:
                                                __script = new StringBuilder();
                                                __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid");
                                                __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                break;
                                        }
                                    }
                                    break;
                                #endregion

                            }

                        }
                    }
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("error : " + __result);
                }
            };
            //
            this._selectAllButton.Click += (s1, e1) =>
            {
                for (int __row = 0; __row < this._fieldGrid._rowData.Count; __row++)
                {
                    this._fieldGrid._cellUpdate(__row, this._gridSelect, 1, true);
                }
                this._fieldGrid.Invalidate();
            };
            //
            this._deSelectAllButton.Click += (s1, e1) =>
            {
                for (int __row = 0; __row < this._fieldGrid._rowData.Count; __row++)
                {
                    this._fieldGrid._cellUpdate(__row, this._gridSelect, 0, true);
                }
                this._fieldGrid.Invalidate();
            };
            //
            this._closeButton.Click += (s1, e1) =>
            {
                this.Dispose();
            };
            //
            this._sendModeButton.Click += (s1, e1) =>
            {
                if (this._tableGrid._selectRow < this._tableGrid._rowData.Count)
                {
                    this._tableGrid._cellUpdate(this._tableGrid._selectRow, _g.d.sync_table_list._trans_type, 1, true);
                    this._tableGrid.Invalidate();
                }
            };
            //
            this._receiveModeButton.Click += (s1, e1) =>
            {
                if (this._tableGrid._selectRow < this._tableGrid._rowData.Count)
                {
                    this._tableGrid._cellUpdate(this._tableGrid._selectRow, _g.d.sync_table_list._trans_type, 2, true);
                    this._tableGrid.Invalidate();
                }
            };
            //
            this._exchangeModeButton.Click += (s1, e1) =>
            {
                if (this._tableGrid._selectRow < this._tableGrid._rowData.Count)
                {
                    this._tableGrid._cellUpdate(this._tableGrid._selectRow, _g.d.sync_table_list._trans_type, 3, true);
                    this._tableGrid.Invalidate();
                }
            };
        }

        MyLib.BeforeDisplayRowReturn _tableGrid__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            if (row < sender._rowData.Count && columnName.Equals(_g.d.sync_table_list._table + "." + _g.d.sync_table_list._trans_type))
            {
                int __mode = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(_g.d.sync_table_list._trans_type)).ToString());
                int __select = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(this._gridSelect)).ToString());
                ((ArrayList)senderRow.newData)[columnNumber] = "";
                if (__select == 1)
                {
                    switch (__mode)
                    {
                        case 1: __result.newColor = Color.Red;
                            ((ArrayList)senderRow.newData)[columnNumber] = "ส่งออกจาก Data Center";
                            break;
                        case 2: __result.newColor = Color.Blue;
                            ((ArrayList)senderRow.newData)[columnNumber] = "รับข้อมูลเข้า Data Center";
                            break;
                        case 3: __result.newColor = Color.Green;
                            ((ArrayList)senderRow.newData)[columnNumber] = "แลกเปลี่ยนข้อมูลกับ Data Center";
                            break;
                    }
                }
            }
            return __result;
        }

        void _fieldGrid__alterCellUpdate(object sender, int row, int column)
        {
            try
            {
                int __check = (int)this._fieldGrid._cellGet(row, this._gridSelect);
                string __tableName = this._tableGrid._cellGet(this._tableGrid._selectRow, _g.d.sync_table_list._table_name).ToString();
                string __fieldName = this._fieldGrid._cellGet(row, _g.d.sync_field_list._field_name).ToString();
                string __default_value = this._fieldGrid._cellGet(row, _g.d.sync_field_list._default_value).ToString();

                for (int __loop = 0; __loop < this._fieldList.Count; __loop++)
                {
                    if (__tableName.Equals(this._fieldList[__loop]._tableName) && __fieldName.Equals(this._fieldList[__loop]._fieldName))
                    {
                        this._fieldList[__loop]._selected = (__check == 0) ? false : true;
                        this._fieldList[__loop]._default_value = __default_value;
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        void _tableGrid__afterSelectRow(object sender, int row)
        {
            string __tableName = this._tableGrid._cellGet(row, _g.d.sync_table_list._table_name).ToString().Trim();
            this._fieldGrid._clear();
            if (__tableName.Length > 0)
            {
                for (int __loop = 0; __loop < this._fieldList.Count; __loop++)
                {
                    if (__tableName.Equals(this._fieldList[__loop]._tableName))
                    {
                        int __addr = this._fieldGrid._addRow();
                        this._fieldGrid._cellUpdate(__addr, _g.d.sync_field_list._field_name, this._fieldList[__loop]._fieldName, false);
                        this._fieldGrid._cellUpdate(__addr, this._gridSelect, (this._fieldList[__loop]._selected) ? 1 : 0, false);
                        this._fieldGrid._cellUpdate(__addr, _g.d.sync_field_list._default_value, this._fieldList[__loop]._default_value, false);
                    }
                }
            }
            this._fieldGrid.Invalidate();
        }

        void _branchControl_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            // ดึงชื่อ Table ทั้งหมด
            ArrayList __getTableName = __myFrameWork._getTableFromDatabase(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName);
            for (int __loop = 0; __loop < __getTableName.Count; __loop++)
            {
                string __tableNameStr = __getTableName[__loop].ToString().ToLower();
                int __addr = this._tableGrid._addRow();
                this._tableGrid._cellUpdate(__addr, _g.d.sync_table_list._table_name, __tableNameStr, false);
                this._tableGrid._cellUpdate(__addr, _g.d.sync_table_list._trans_type, 2, false);
                this._tableGrid.Invalidate();
            }
            // ดึงชื่อ Field ทั้งหมด
            string __queryGetColumnName = "SELECT table_name,column_name FROM information_schema.columns order by table_name,column_name";
            DataTable __field = __myFrameWork._query(MyLib._myGlobal._databaseName, __queryGetColumnName).Tables[0];
            for (int __row = 0; __row < __field.Rows.Count; __row++)
            {
                _fieldListClass __new = new _fieldListClass();
                __new._tableName = __field.Rows[__row]["table_name"].ToString().ToLower();
                __new._fieldName = __field.Rows[__row]["column_name"].ToString().ToLower();
                //__new._default_value = __field.Rows[__row]["column_name"].ToString().ToLower();
                __new._selected = false;
                this._fieldList.Add(__new);
            }
            // ดึงข้อมูล
            DataTable __getTable = __myFrameWork._queryShort("select * from " + _g.d.sync_table_list._table).Tables[0];
            for (int __row = 0; __row < __getTable.Rows.Count; __row++)
            {
                string __tableName = __getTable.Rows[__row][_g.d.sync_table_list._table_name].ToString();
                int __transType = (int)MyLib._myGlobal._decimalPhase(__getTable.Rows[__row][_g.d.sync_table_list._trans_type].ToString());
                string __trans_command = __getTable.Rows[__row][_g.d.sync_table_list._trans_command].ToString();

                for (int __findRow = 0; __findRow < this._tableGrid._rowData.Count; __findRow++)
                {
                    if (this._tableGrid._cellGet(__findRow, _g.d.sync_table_list._table_name).ToString().Equals(__tableName))
                    {
                        this._tableGrid._cellUpdate(__findRow, this._gridSelect, 1, false);
                        this._tableGrid._cellUpdate(__findRow, _g.d.sync_table_list._trans_type, __transType, false);
                        this._tableGrid._cellUpdate(__findRow, _g.d.sync_table_list._trans_command, __trans_command, false);
                        break;
                    }
                }
            }
            this._tableGrid.Invalidate();
            //
            DataTable __getField = __myFrameWork._queryShort("select * from " + _g.d.sync_field_list._table).Tables[0];
            for (int __row = 0; __row < __getField.Rows.Count; __row++)
            {
                string __tableName = __getField.Rows[__row][_g.d.sync_field_list._table_name].ToString();
                string __fieldName = __getField.Rows[__row][_g.d.sync_field_list._field_name].ToString();
                string __default_value = __getField.Rows[__row][_g.d.sync_field_list._default_value].ToString();
                for (int __findRow = 0; __findRow < this._fieldList.Count; __findRow++)
                {
                    if (__tableName.Equals(this._fieldList[__findRow]._tableName) && __fieldName.Equals(this._fieldList[__findRow]._fieldName))
                    {
                        this._fieldList[__findRow]._selected = true;
                        this._fieldList[__findRow]._default_value = __default_value;
                        break;
                    }
                }
            }
        }

        class _fieldListClass
        {
            public string _tableName;
            public string _fieldName;
            public Boolean _selected;
            public string _default_value;
        }
    }
}
