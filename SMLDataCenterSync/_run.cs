using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Threading;
using System.Windows.Forms;

namespace SMLDataCenterSync
{
    public static class _run
    {
        private static List<_syncTableListStruct> _tableList = new List<_syncTableListStruct>();

        public static List<_syncFieldListStruct> _getFieldFromXml(string tableName, string getXml, DataTable selectField)
        {
            List<_syncFieldListStruct> __fieldList = new List<_syncFieldListStruct>();
            XmlDocument __xDoc = new XmlDocument();
            __xDoc.LoadXml(getXml);
            __xDoc.DocumentElement.Normalize();
            XmlElement __xRoot = __xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("detail");
            for (int __detail = 0; __detail < __xReader.Count; __detail++)
            {
                XmlNode __xFirstNode = __xReader.Item(__detail);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;
                    if (tableName.ToLower().Equals(__xTable.GetAttribute("table_name").ToLower()))
                    {
                        string __type = __xTable.GetAttribute("type").ToLower();
                        if (__type.Equals("varchar") ||
                            __type.Equals("date") ||
                            __type.Equals("int2") ||
                            __type.Equals("int4") ||
                            __type.Equals("int8") ||
                            __type.Equals("char") ||
                            __type.Equals("uuid") ||
                            __type.Equals("bool") ||
                            __type.Equals("numeric") ||
                            __type.Equals("decimal") ||
                            __type.Equals("bytea") ||
                            __type.Equals("image") ||
                            __type.Equals("float8") ||
                            __type.Equals("timestamp") ||

                            // toe for sql server
                            __type.Equals("int") ||
                            __type.Equals("float") ||
                            __type.Equals("money") ||
                            __type.Equals("smalldatetime") ||
                            __type.Equals("datetime") ||
                            __type.Equals("smallint") ||
                            __type.Equals("uniqueidentifier")
                            )
                        {
                            string __fieldName = __xTable.GetAttribute("column_name").ToLower();
                            if (__fieldName.Equals("guid") == false && __fieldName.Equals("roworder") == false)
                            {
                                DataRow[] __selectFieldRow = selectField.Select("field_name=\'" + __fieldName + "\'");
                                if (__selectFieldRow.Length != 0)
                                {
                                    _syncFieldListStruct __new = new _syncFieldListStruct();
                                    __new._fieldNameForInsertUpdate = __fieldName;
                                    __new._fieldDefaultValue = __selectFieldRow[0]["default_value"].ToString();
                                    if (__type.Equals("bytea") || __type.Equals("image"))
                                    {
                                        __new._fieldNameForSelect = "encode(" + __fieldName + ",\'base64\') as " + __fieldName;
                                    }
                                    else
                                    {
                                        __new._fieldNameForSelect = __fieldName;
                                    }
                                    __new._fieldType = __type;
                                    __fieldList.Add(__new);
                                }
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }
            return __fieldList;
        }

        private static void _syncLog(MyLib._myFrameWork datacenterFrameWork, string message)
        {
            _syncLog(datacenterFrameWork, message, MyLib._myGlobal._databaseType.PostgreSql);
        }

        private static void _syncLog(MyLib._myFrameWork datacenterFrameWork, string message, MyLib._myGlobal._databaseType databaseType)
        {
            if (message.Trim().Length > 0)
            {
                string __currentDateStr = "now()";

                if (databaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2000 || databaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2005)
                {
                    __currentDateStr = "getdate()";
                }

                datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "insert into " + _g.d.sync_log._table + "(" + _g.d.sync_log._date_time + "," + _g.d.sync_log._branch_code + "," + _g.d.sync_log._message + ") values (" + __currentDateStr + ",\'" + _g.g._companyProfile._activeSyncBranchCode + "\',\'" + message.Replace("\'", " ").Replace("\"", " ").Replace("\r", " ").Replace("\n", " ") + "\')");
                Thread.Sleep(10000);
            }
        }

        public static string _command(string tableName, string fieldName, string oldCode, string newCode, string extraWhere)
        {
            string __itemCodeOld = oldCode;
            string __itemCodeNew = newCode;

            StringBuilder __result = new StringBuilder();
            if (__itemCodeOld.Equals(__itemCodeNew) == false)
            {
                //string __deleteFormat = "delete from {0} where {1}=\'" + __itemCodeNew + "\'";
                string __updateFormat = "update {0} set {1}=\'" + __itemCodeNew + "\' where {1}=\'" + __itemCodeOld + "\'" + ((extraWhere.Length > 0) ? " and " + extraWhere : "");
                //__result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__deleteFormat), tableName, fieldName));
                __result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__updateFormat), tableName, fieldName));
            }
            return __result.ToString();
        }

        public static void _start()
        {
            // ดึง xml จาก server เพื่อดูว่าจะเอา Table ไหนบ้าง
            Thread.Sleep(10000);
            _g.g._companyProfileLoad();
            if (_g.g._companyProfile._activeSyncServer.Trim().Length > 0)
            {
                MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();
                MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(_g.g._companyProfile._activeSyncServer, "SMLConfig" + _g.g._companyProfile._activeSyncProvider + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                MyLib._myGlobal._databaseType __centerDatabaseType = __datacenterFrameWork._setDataBaseCode();
                MyLib._myGlobal._databaseType __clientDatabaseType = __clientFrameWork._setDataBaseCode();
                // ตรวจสอบเครื่อง ว่าจะให้ทำงานหรือไม่

                Boolean __active = false;

                //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\synclog.txt", DateTime.Now.ToString() + " GET CENTER DATATYPE " + __datacenterFrameWork._databaseSelectType.ToString() , true);

                int __waitTime = 0;
                try
                {
                    if (_g.g._companyProfile._activeSyncDatabase.Trim().Length > 0)
                    {
                        string __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.sync_branch_list._active, _g.d.sync_branch_list._access_code, _g.d.sync_branch_list._branch_computer_name, _g.d.sync_branch_list._wait_time) + " from " + _g.d.sync_branch_list._table + " where " + MyLib._myGlobal._addUpper(_g.d.sync_branch_list._branch_code) + "=\'" + _g.g._companyProfile._activeSyncBranchCode.ToUpper() + "\'";
                        DataTable __getConfig = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, __query).Tables[0];
                        __waitTime = ((int)MyLib._myGlobal._decimalPhase(__getConfig.Rows[0][_g.d.sync_branch_list._wait_time].ToString())) * 1000;
                        if ((int)MyLib._myGlobal._decimalPhase(__getConfig.Rows[0][_g.d.sync_branch_list._active].ToString()) == 1 && __getConfig.Rows[0][_g.d.sync_branch_list._access_code].ToString().ToUpper().Equals(_g.g._companyProfile._activeSyncAccessCode.ToUpper()))
                        {
                            string __device = __getConfig.Rows[0][_g.d.sync_branch_list._branch_computer_name].ToString().ToLower();
                            MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                            string[] _dataDive = Environment.GetLogicalDrives();
                            for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                            {
                                string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                                if (__getDeviceCode.Length > 0 && __device.IndexOf(__getDeviceCode) != -1)
                                {
                                    __active = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                if (__active == false)
                {
                    return;
                }
                // Script new() 
                

                StringBuilder __script = new StringBuilder();
                string __execute_result = "";
                switch (__clientDatabaseType)
                {
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                        {
                            __script = new StringBuilder();
                            __script.Append("CREATE TABLE guid_delete \r\n");
                            __script.Append("( \r\n");
                            __script.Append("roworder int IDENTITY(1,1) NOT NULL, \r\n");
                            __script.Append("table_name character varying(100), \r\n");
                            __script.Append("guid uniqueidentifier, \r\n");
                            __script.Append("CONSTRAINT roworder_idx PRIMARY KEY (roworder) \r\n");
                            __script.Append("); \r\n");
                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                        }
                        break;
                    default:
                        {

                            __script = new StringBuilder();
                            __script.Append("CREATE OR REPLACE FUNCTION newid() \r\n");
                            __script.Append("RETURNS uuid AS \r\n");
                            __script.Append("$BODY$\r\n");
                            __script.Append("SELECT CAST(md5(current_database()|| user ||current_timestamp ||random()) as uuid) \r\n");
                            __script.Append("$BODY$ \r\n");
                            __script.Append("LANGUAGE sql VOLATILE \r\n");
                            __script.Append("COST 100; \r\n");
                            __script.Append("ALTER FUNCTION newid() OWNER TO postgres; \r\n");
                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                            //
                            __script = new StringBuilder();
                            __script.Append("CREATE TABLE guid_delete \r\n");
                            __script.Append("( \r\n");
                            __script.Append("roworder serial NOT NULL, \r\n");
                            __script.Append("table_name character varying(100), \r\n");
                            __script.Append("guid uuid, \r\n");
                            __script.Append("CONSTRAINT roworder_idx PRIMARY KEY (roworder) \r\n");
                            __script.Append(") \r\n");
                            __script.Append("WITH ( \r\n");
                            __script.Append("OIDS=FALSE \r\n");
                            __script.Append("); \r\n");
                            __script.Append("ALTER TABLE guid_delete OWNER TO postgres; \r\n");
                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                        }
                        break;
                }
                //
                if (__waitTime < 1000)
                {
                    __waitTime = 1000;
                }
                Boolean __createdScript = false;
                Boolean __createdScript_2 = false;
                Boolean __createdScript_3 = false;
                while (true)
                {

                    //create script
                    if (__createdScript == false)
                    {
                        DataSet __tableCheckScript = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select table_name,trans_type from sync_table_list ");
                        if (__tableCheckScript.Tables.Count > 0)
                        {
                            DataTable __scriptList = __tableCheckScript.Tables[0];
                            for (int __row = 0; __row < __scriptList.Rows.Count; __row++)
                            {
                                string __tableName = __scriptList.Rows[__row]["table_name"].ToString().ToLower();
                                int __tableType = (int)MyLib._myGlobal._decimalPhase(__scriptList.Rows[__row]["trans_type"].ToString());
                                _syncTableListStruct __new = new _syncTableListStruct();
                                __new._tableName = __tableName;
                                __new._transferType = __tableType;
                                _tableList.Add(__new);
                                if (__createdScript == false)
                                {
                                    switch (__tableType)
                                    {
                                        #region Mode รับข้อมูล
                                        case 1:
                                            {
                                                switch (__clientDatabaseType)
                                                {
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                        {
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD guid UNIQUEIDENTIFIER default NEWSEQUENTIALID(); ");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());


                                                            // index
                                                            // send success
                                                            __script = new StringBuilder();
                                                            __script.Append("create nonclustered index " + __tableName + "_guid_idx on " + __tableName + " (guid asc);");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                        }
                                                        break;
                                                    default:
                                                        {
                                                            // add guid field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // guid index
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE INDEX " + __tableName + "_guid_idx  \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("USING btree \r\n");
                                                            __script.Append("(guid); \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                        #endregion
                                        #region Mode ส่งข้อมูล
                                        case 2:
                                            {
                                                // จัดการ server ก่อน
                                                switch (__centerDatabaseType)
                                                {
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                        {
                                                            // add server branch_sync
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD branch_sync character varying(100) default ''; ");
                                                            __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                            // create index
                                                            // index
                                                            // send success
                                                            __script = new StringBuilder();
                                                            __script.Append("create nonclustered index " + __tableName + "_branch_sync_idx on " + __tableName + " (branch_sync asc);");
                                                            __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                        }
                                                        break;
                                                    default:
                                                        {
                                                            // add server branch_sync
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN branch_sync varchar(100) DEFAULT ''::character varying");
                                                            __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());
                                                            // branch_sync index
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE INDEX " + __tableName + "_branch_sync_idx  \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("USING btree \r\n");
                                                            __script.Append("(branch_sync); \r\n");
                                                            __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                        }
                                                        break;
                                                }

                                                // จัดการ field client
                                                switch (__clientDatabaseType)
                                                {
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                        {
                                                            // add guid field

                                                            __script = new StringBuilder();
                                                            if (__clientDatabaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2000)
                                                            {
                                                                // sql server 2000
                                                                __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWID();");
                                                            }
                                                            else
                                                            {
                                                                // sql server 2005
                                                                __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWSEQUENTIALID();");
                                                            }
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // create index
                                                            __script = new StringBuilder();
                                                            __script.Append("create nonclustered index " + __tableName + "_guid_idx on " + __tableName + " (guid asc);");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // add send success field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD send_success smallint default (0);");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            //after delete
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER  " + __tableName + "_after_delete \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("AFTER DELETE \r\n");
                                                            __script.Append("AS \r\n");
                                                            __script.Append("begin \r\n");
                                                            __script.Append("insert into guid_delete(table_name,guid) \r\n");
                                                            __script.Append("select '" + __tableName + "', i.guid from deleted i \r\n");
                                                            __script.Append("end \r\n");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // before update
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER  " + __tableName + "_before_update \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("AFTER UPDATE \r\n");
                                                            __script.Append("AS \r\n");
                                                            __script.Append("begin \r\n");
                                                            __script.Append("IF (UPDATE(send_success)) \r\n");
                                                            __script.Append("BEGIN \r\n");
                                                            __script.Append("RETURN \r\n");
                                                            __script.Append("END \r\n");
                                                            __script.Append("UPDATE " + __tableName + " SET send_success = 0 WHERE EXISTS (select i.guid from inserted i WHERE i.guid = " + __tableName + ".guid ) and " + __tableName + ".send_success != 0 \r\n");
                                                            __script.Append("END \r\n");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                        }
                                                        break;
                                                    default:
                                                        {

                                                            // add guid field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid DEFAULT newid()");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // guid index
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE INDEX " + __tableName + "_guid_idx  \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("USING btree \r\n");
                                                            __script.Append("(guid); \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // add send_success field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN send_success boolean DEFAULT false");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // after delete function
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_after_delete() \r\n");
                                                            __script.Append("RETURNS trigger AS \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("BEGIN \r\n");
                                                            __script.Append("insert into guid_delete (table_name,guid) values ('" + __tableName + "',OLD.guid); \r\n");
                                                            __script.Append("RETURN NEW; \r\n");
                                                            __script.Append("END; \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                            __script.Append("COST 100; \r\n");
                                                            __script.Append("ALTER FUNCTION " + __tableName + "_after_delete() OWNER TO postgres; \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // after delete trigger
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER " + __tableName + "_after_delete_trigger \r\n");
                                                            __script.Append("AFTER DELETE \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("FOR EACH ROW \r\n");
                                                            __script.Append("EXECUTE PROCEDURE " + __tableName + "_after_delete(); \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // before update function
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_before_update() \r\n");
                                                            __script.Append("RETURNS trigger AS \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("BEGIN \r\n");
                                                            __script.Append("IF OLD.send_success=true or OLD.send_success is null then \r\n");
                                                            __script.Append("NEW.send_success= false;  \r\n");
                                                            __script.Append("END IF; \r\n");
                                                            __script.Append("RETURN NEW; \r\n");
                                                            __script.Append("END; \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                            __script.Append("COST 100; \r\n");
                                                            __script.Append("ALTER FUNCTION " + __tableName + "_before_update() OWNER TO postgres; \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // before update trigger
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER " + __tableName + "_before_update_trigger \r\n");
                                                            __script.Append("BEFORE UPDATE \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("FOR EACH ROW \r\n");
                                                            __script.Append("EXECUTE PROCEDURE " + __tableName + "_before_update(); \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                        #endregion
                                        #region Mode แลกเปลี่ยนข้อมูล
                                        case 3:
                                            {
                                                // จัดการ server ก่อน
                                                switch (__centerDatabaseType)
                                                {
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                        {
                                                            // add server branch_sync
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD branch_sync character varying(100) default ''; ");
                                                            __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                            // create index
                                                            // index
                                                            // send success
                                                            __script = new StringBuilder();
                                                            __script.Append("create nonclustered index " + __tableName + "_branch_sync_idx on " + __tableName + " (branch_sync asc);");
                                                            __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                        }
                                                        break;
                                                    default:
                                                        {
                                                            // add server branch_sync
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN branch_sync varchar(100) DEFAULT ''::character varying");
                                                            __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());
                                                            // branch_sync index
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE INDEX " + __tableName + "_branch_sync_idx  \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("USING btree \r\n");
                                                            __script.Append("(branch_sync); \r\n");
                                                            __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                        }
                                                        break;
                                                }

                                                // จัดการ field client
                                                switch (__clientDatabaseType)
                                                {
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                        {
                                                            // add guid field

                                                            __script = new StringBuilder();
                                                            if (__clientDatabaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2000)
                                                            {
                                                                // sql server 2000
                                                                __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWID();");
                                                            }
                                                            else
                                                            {
                                                                // sql server 2005
                                                                __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWSEQUENTIALID();");
                                                            }
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // create index
                                                            __script = new StringBuilder();
                                                            __script.Append("create nonclustered index " + __tableName + "_guid_idx on " + __tableName + " (guid asc);");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // add send success field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD send_success smallint default (0);");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // add is sync data field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD is_sync_in smallint default (0);");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            //after delete
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER  " + __tableName + "_after_delete \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("AFTER DELETE \r\n");
                                                            __script.Append("AS \r\n");
                                                            __script.Append("begin \r\n");
                                                            __script.Append("insert into guid_delete(table_name,guid) \r\n");
                                                            __script.Append("select '" + __tableName + "', i.guid from deleted i \r\n");
                                                            __script.Append("end \r\n");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // before update
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER  " + __tableName + "_before_update \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("AFTER UPDATE \r\n");
                                                            __script.Append("AS \r\n");
                                                            __script.Append("begin \r\n");
                                                            __script.Append("IF (UPDATE(send_success)) \r\n");
                                                            __script.Append("BEGIN \r\n");
                                                            __script.Append("RETURN \r\n");
                                                            __script.Append("END \r\n");
                                                            __script.Append("UPDATE " + __tableName + " SET send_success = 0 WHERE EXISTS (select i.guid from inserted i WHERE i.guid = " + __tableName + ".guid ) and " + __tableName + ".send_success != 0 \r\n");
                                                            __script.Append("END \r\n");
                                                            __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                        }
                                                        break;
                                                    default:
                                                        {

                                                            // add guid field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid DEFAULT newid()");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                            // guid index
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE INDEX " + __tableName + "_guid_idx  \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("USING btree \r\n");
                                                            __script.Append("(guid); \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // add send_success field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN send_success boolean DEFAULT false");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // add send_success field
                                                            __script = new StringBuilder();
                                                            __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN is_sync_in boolean DEFAULT false");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // after delete function
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_after_delete() \r\n");
                                                            __script.Append("RETURNS trigger AS \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("BEGIN \r\n");
                                                            __script.Append("insert into guid_delete (table_name,guid) values ('" + __tableName + "',OLD.guid); \r\n");
                                                            __script.Append("RETURN NEW; \r\n");
                                                            __script.Append("END; \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                            __script.Append("COST 100; \r\n");
                                                            __script.Append("ALTER FUNCTION " + __tableName + "_after_delete() OWNER TO postgres; \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // after delete trigger
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER " + __tableName + "_after_delete_trigger \r\n");
                                                            __script.Append("AFTER DELETE \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("FOR EACH ROW \r\n");
                                                            __script.Append("EXECUTE PROCEDURE " + __tableName + "_after_delete(); \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // before update function
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_before_update() \r\n");
                                                            __script.Append("RETURNS trigger AS \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("BEGIN \r\n");

                                                            // check mode 
                                                            __script.Append("IF ( TG_OP = 'INSERT') THEN \r\n");
                                                            // insert mode
                                                            __script.Append("IF (NEW.is_sync_in = false ) THEN \r\n");
                                                            __script.Append("NEW.send_success = false; \r\n");
                                                            __script.Append("ELSE \r\n");
                                                            __script.Append("NEW.send_success = true; \r\n");
                                                            __script.Append("END IF; \r\n");

                                                            __script.Append("ELSE \r\n");
                                                            // mode update
                                                            // check guid_code 
                                                            //__script.Append("IF OLD.guid_code = NEW.guid_code THEN \r\n");

                                                            __script.Append("IF (NEW.is_sync_in = true ) THEN \r\n");
                                                            __script.Append("NEW.send_success= true;  \r\n");
                                                            __script.Append("ELSE \r\n");
                                                            // -- update จาก client
                                                            __script.Append("IF (OLD.send_success=true or OLD.send_success is null) THEN \r\n");
                                                            __script.Append("NEW.send_success= false;  \r\n");
                                                            __script.Append("END IF; \r\n");

                                                            __script.Append("END IF; \r\n");
                                                            // end check sync server

                                                            //__script.Append("END IF; \r\n");
                                                            // end check update guid

                                                            __script.Append("END IF; \r\n");

                                                            __script.Append("NEW.is_sync_in= false;  \r\n");
                                                            __script.Append("RETURN NEW; \r\n");
                                                            __script.Append("END; \r\n");
                                                            __script.Append("$BODY$ \r\n");
                                                            __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                            __script.Append("COST 100; \r\n");
                                                            __script.Append("ALTER FUNCTION " + __tableName + "_before_update() OWNER TO postgres; \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                            // before update trigger
                                                            __script = new StringBuilder();
                                                            __script.Append("CREATE TRIGGER " + __tableName + "_before_update_trigger \r\n");
                                                            __script.Append("BEFORE INSERT OR UPDATE \r\n");
                                                            __script.Append("ON " + __tableName + " \r\n");
                                                            __script.Append("FOR EACH ROW \r\n");
                                                            __script.Append("EXECUTE PROCEDURE " + __tableName + "_before_update(); \r\n");
                                                            __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                        }
                                                        break;
                                                }

                                            }
                                            break;
                                        #endregion
                                    }
                                }
                            }

                        }
                    }
                    __createdScript = true;

                    // toe change master code
                    if (_g.g._companyProfile._activeSync)
                    {
                        MyLib._myGlobal._syncDataActive = false;
                        Thread.Sleep(__waitTime);

                        try
                        {
                            string __getChangeCodeQuery = "select * from sync_change_code where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' order by roworder";
                            DataSet __getChangeCodeTable = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, __getChangeCodeQuery);

                            if (__getChangeCodeTable.Tables.Count > 0 && __getChangeCodeTable.Tables[0].Rows.Count > 0)
                            {
                                for (int __row = 0; __row < __getChangeCodeTable.Tables[0].Rows.Count; __row++)
                                {
                                    string __changeCodeType = __getChangeCodeTable.Tables[0].Rows[__row][_g.d.sync_change_code._change_mode].ToString();
                                    string __oldCode = __getChangeCodeTable.Tables[0].Rows[__row][_g.d.sync_change_code._old_code].ToString();
                                    string __newCode = __getChangeCodeTable.Tables[0].Rows[__row][_g.d.sync_change_code._new_code].ToString();
                                    string __rowOrder = __getChangeCodeTable.Tables[0].Rows[__row]["roworder"].ToString();
                                    switch (__changeCodeType)
                                    {
                                        case "AR":
                                            {
                                                // เปลี่ยนรหัสลูกหนี้    
                                                string __flag_ar_update = MyLib._myGlobal._fieldAndComma(
                                                    // เสนอราคา
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ).ToString(),

                                                    // สั่งซื้อ
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ).ToString(),

                                                    // สั่งขาย
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ).ToString(),

                                                    // เงินล่วงหน้า
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก).ToString(),

                                                    // เงินมัดจำ
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก).ToString(),

                                                    // ขาย
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก).ToString(),

                                                    // หนี้ยกมา
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา).ToString(),

                                                    // หนี้อื่น ๆ 
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก).ToString(),

                                                    // วางบิล
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก).ToString(),

                                                    // จ่ายชำระ
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString(),
                                                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก).ToString(),

                                                      // ค่าใช้จ่าย
                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString(),
                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก).ToString(),
                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้).ToString(),
                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก).ToString(),
                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้).ToString(),
                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก).ToString(),

                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString(),
                                                      _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดแต้ม).ToString()

                                                      );

                                                StringBuilder __query = new StringBuilder();
                                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                                // trans 
                                                __query.Append(_command(_g.d.ic_trans._table, _g.d.ic_trans._cust_code, __oldCode, __newCode, _g.d.ic_trans._trans_flag + " in (" + __flag_ar_update + ")"));
                                                __query.Append(_command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._cust_code, __oldCode, __newCode, _g.d.ic_trans_detail._trans_flag + " in (" + __flag_ar_update + ")"));

                                                __query.Append(_command(_g.d.ap_ar_trans._table, _g.d.ap_ar_trans._cust_code, __oldCode, __newCode, _g.d.ap_ar_trans._trans_flag + " in (" + __flag_ar_update + ")"));
                                                __query.Append(_command(_g.d.ap_ar_trans_detail._table, _g.d.ap_ar_trans_detail._cust_code, __oldCode, __newCode, _g.d.ap_ar_trans_detail._trans_flag + " in (" + __flag_ar_update + ")"));

                                                __query.Append(_command(_g.d.cb_trans._table, _g.d.cb_trans._ap_ar_code, __oldCode, __newCode, _g.d.cb_trans._trans_flag + " in (" + __flag_ar_update + ")"));
                                                __query.Append(_command(_g.d.cb_trans_detail._table, _g.d.cb_trans_detail._ap_ar_code, __oldCode, __newCode, _g.d.cb_trans_detail._trans_flag + " in (" + __flag_ar_update + ")"));

                                                __query.Append(_command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._cust_code, __oldCode, __newCode, _g.d.ic_trans_serial_number._trans_flag + " in (" + __flag_ar_update + ")"));
                                                __query.Append(_command(_g.d.ic_trans_shipment._table, _g.d.ic_trans_shipment._cust_code, __oldCode, __newCode, _g.d.ic_trans_shipment._trans_flag + " in (" + __flag_ar_update + ")"));

                                                __query.Append(_command(_g.d.ap_ar_transport_label._table, _g.d.ap_ar_transport_label._cust_code, __oldCode, __newCode, _g.d.ap_ar_transport_label._ar_ap_type + "=1"));

                                                __query.Append("</node>");

                                                string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                                                if (__result.Length == 0)
                                                {
                                                    // กลับไปลบรายการฝั่ง server
                                                    string __truncateResult = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_change_code where change_mode = 'AR' and  branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and " + _g.d.sync_change_code._old_code + "=\'" + __oldCode + "\' ");
                                                    if (__truncateResult.Length != 0)
                                                    {
                                                        _syncLog(__datacenterFrameWork, __truncateResult, __centerDatabaseType);
                                                        MyLib._myGlobal._syncDataActive = false;
                                                    }
                                                }
                                                else
                                                {
                                                    _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                    MyLib._myGlobal._syncDataActive = false;
                                                }

                                            }
                                            break;
                                        case "AP":
                                            {
                                                string _flag_ap_update = MyLib._myGlobal._fieldAndComma(
                                                    // เสนอซื้อ 
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ).ToString(),

                                                   // สั่งซื้อ
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ).ToString(),

                                                   // เงินล่วงหน้า
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก).ToString(),

                                                   // เงินมัดจำ
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก).ToString(),

                                                   // ซื้อ
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก).ToString(),

                                                   // ซื้อพาเชียล
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้).ToString(),

                                                   // หนี้ยกมา
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา).ToString(),

                                                   // หนี้อื่น ๆ 
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก).ToString(),

                                                   // วางบิล
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก).ToString(),

                                                  // จ่ายชำระ
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก).ToString(),

                                                   // ค่าใช้จ่าย
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้).ToString(),
                                                   _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก).ToString()
                                                   );

                                                StringBuilder __query = new StringBuilder();
                                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

                                                __query.Append(_command(_g.d.ic_trans._table, _g.d.ic_trans._cust_code, __oldCode, __newCode, _g.d.ic_trans._trans_flag + " in (" + _flag_ap_update + ")"));
                                                __query.Append(_command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._cust_code, __oldCode, __newCode, _g.d.ic_trans_detail._trans_flag + " in (" + _flag_ap_update + ")"));

                                                __query.Append(_command(_g.d.ap_ar_trans._table, _g.d.ap_ar_trans._cust_code, __oldCode, __newCode, _g.d.ap_ar_trans._trans_flag + " in (" + _flag_ap_update + ")"));
                                                __query.Append(_command(_g.d.ap_ar_trans_detail._table, _g.d.ap_ar_trans_detail._cust_code, __oldCode, __newCode, _g.d.ap_ar_trans_detail._trans_flag + " in (" + _flag_ap_update + ")"));

                                                __query.Append(_command(_g.d.cb_trans._table, _g.d.cb_trans._ap_ar_code, __oldCode, __newCode, _g.d.cb_trans._trans_flag + " in (" + _flag_ap_update + ")"));
                                                __query.Append(_command(_g.d.cb_trans_detail._table, _g.d.cb_trans_detail._ap_ar_code, __oldCode, __newCode, _g.d.cb_trans_detail._trans_flag + " in (" + _flag_ap_update + ")"));

                                                __query.Append(_command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._cust_code, __oldCode, __newCode, _g.d.ic_trans_serial_number._trans_flag + " in (" + _flag_ap_update + ")"));
                                                __query.Append(_command(_g.d.ic_trans_shipment._table, _g.d.ic_trans_shipment._cust_code, __oldCode, __newCode, _g.d.ic_trans_shipment._trans_flag + " in (" + _flag_ap_update + ")"));

                                                __query.Append(_command(_g.d.ap_ar_transport_label._table, _g.d.ap_ar_transport_label._cust_code, __oldCode, __newCode, _g.d.ap_ar_transport_label._ar_ap_type + "=0"));

                                                __query.Append("</node>");

                                                string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                                                if (__result.Length == 0)
                                                {
                                                    // กลับไปลบรายการฝั่ง server
                                                    string __truncateResult = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_change_code where change_mode = 'AP' and  branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and " + _g.d.sync_change_code._old_code + "=\'" + __oldCode + "\' ");
                                                    if (__truncateResult.Length != 0)
                                                    {
                                                        _syncLog(__datacenterFrameWork, __truncateResult, __centerDatabaseType);
                                                        MyLib._myGlobal._syncDataActive = false;
                                                    }
                                                }
                                                else
                                                {
                                                    _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                    MyLib._myGlobal._syncDataActive = false;
                                                }
                                            }
                                            break;
                                        case "IC":
                                            {
                                                StringBuilder __query = new StringBuilder();
                                                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");

                                                __query.Append(_command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._item_code, __oldCode, __newCode, ""));
                                                __query.Append(_command(_g.d.ic_trans_detail_lot._table, _g.d.ic_trans_detail_lot._item_code, __oldCode, __newCode, ""));
                                                __query.Append(_command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._ic_code, __oldCode, __newCode, ""));
                                                __query.Append("</node>");

                                                string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                                                if (__result.Length == 0)
                                                {
                                                    // กลับไปลบรายการฝั่ง server
                                                    string __truncateResult = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_change_code where change_mode = 'IC' and  branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and " + _g.d.sync_change_code._old_code + "=\'" + __oldCode + "\' ");
                                                    if (__truncateResult.Length != 0)
                                                    {
                                                        _syncLog(__datacenterFrameWork, __truncateResult, __centerDatabaseType);
                                                        MyLib._myGlobal._syncDataActive = false;
                                                    }
                                                }
                                                else
                                                {
                                                    _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                    MyLib._myGlobal._syncDataActive = false;
                                                }
                                            }
                                            break;
                                    }
                                }


                            }

                        }
                        catch (Exception ex)
                        {

                        }

                        MyLib._myGlobal._syncDataActive = true;

                    }


                    // toe ทำทีละ mode
                    for (int __mode = 1; __mode <= 3; __mode++)
                    {
                        MyLib._myGlobal._syncDataActive = false;
                        Thread.Sleep(__waitTime);
                        MyLib._myGlobal._syncDataActive = true;
                        try
                        {
                            if (_g.g._companyProfile._activeSync)
                            {
                                //
                                // access log
                                __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "insert into " + _g.d.sync_access_log._table + "(" + _g.d.sync_access_log._branch_code + "," + _g.d.sync_access_log._date_time + ") values(\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\', " + ((__centerDatabaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2000 || __centerDatabaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2005) ? "getdate()" : "now()") + ")");

                                DataSet __tableListDs = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select table_name,trans_type from sync_table_list" + ((__mode == 3) ? "" : " where " + _g.d.sync_table_list._trans_type + "=" + __mode.ToString()));
                                if (__tableListDs.Tables.Count > 0)
                                {
                                    DataTable __tableList = __tableListDs.Tables[0];
                                    _tableList.Clear();
                                    for (int __row = 0; __row < __tableList.Rows.Count; __row++)
                                    {
                                        string __tableName = __tableList.Rows[__row]["table_name"].ToString().ToLower();
                                        int __tableType = (int)MyLib._myGlobal._decimalPhase(__tableList.Rows[__row]["trans_type"].ToString());
                                        _syncTableListStruct __new = new _syncTableListStruct();
                                        __new._tableName = __tableName;
                                        __new._transferType = __tableType;
                                        _tableList.Add(__new);

                                        /* toe ย้ายไปข้างบน
                                        if (__createdScript == false)
                                        {
                                            switch (__tableType)
                                            {
                                                #region Mode รับข้อมูล
                                                case 1:
                                                    {
                                                        switch (__clientDatabaseType)
                                                        {
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                {
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD guid UNIQUEIDENTIFIER default NEWSEQUENTIALID(); ");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());


                                                                    // index
                                                                    // send success
                                                                    __script = new StringBuilder();
                                                                    __script.Append("create nonclustered index " + __tableName + "_guid_idx on " + __tableName + " (guid asc);");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                }
                                                                break;
                                                            default:
                                                                {
                                                                    // add guid field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // guid index
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE INDEX " + __tableName + "_guid_idx  \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("USING btree \r\n");
                                                                    __script.Append("(guid); \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                #endregion
                                                #region Mode ส่งข้อมูล
                                                case 2:
                                                    {
                                                        // จัดการ server ก่อน
                                                        switch (__centerDatabaseType)
                                                        {
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                {
                                                                    // add server branch_sync
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD branch_sync character varying(100) default ''; ");
                                                                    __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                                    // create index
                                                                    // index
                                                                    // send success
                                                                    __script = new StringBuilder();
                                                                    __script.Append("create nonclustered index " + __tableName + "_branch_sync_idx on " + __tableName + " (branch_sync asc);");
                                                                    __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                                }
                                                                break;
                                                            default:
                                                                {
                                                                    // add server branch_sync
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN branch_sync varchar(100) DEFAULT ''::character varying");
                                                                    __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());
                                                                    // branch_sync index
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE INDEX " + __tableName + "_branch_sync_idx  \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("USING btree \r\n");
                                                                    __script.Append("(branch_sync); \r\n");
                                                                    __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                                }
                                                                break;
                                                        }

                                                        // จัดการ field client
                                                        switch (__clientDatabaseType)
                                                        {
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                {
                                                                    // add guid field

                                                                    __script = new StringBuilder();
                                                                    if (__clientDatabaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2000)
                                                                    {
                                                                        // sql server 2000
                                                                        __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWID();");
                                                                    }
                                                                    else
                                                                    {
                                                                        // sql server 2005
                                                                        __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWSEQUENTIALID();");
                                                                    }
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // create index
                                                                    __script = new StringBuilder();
                                                                    __script.Append("create nonclustered index " + __tableName + "_guid_idx on " + __tableName + " (guid asc);");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // add send success field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD send_success smallint default (0);");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    //after delete
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER  " + __tableName + "_after_delete \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("AFTER DELETE \r\n");
                                                                    __script.Append("AS \r\n");
                                                                    __script.Append("begin \r\n");
                                                                    __script.Append("insert into guid_delete(table_name,guid) \r\n");
                                                                    __script.Append("select '" + __tableName + "', i.guid from deleted i \r\n");
                                                                    __script.Append("end \r\n");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // before update
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER  " + __tableName + "_before_update \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("AFTER UPDATE \r\n");
                                                                    __script.Append("AS \r\n");
                                                                    __script.Append("begin \r\n");
                                                                    __script.Append("IF (UPDATE(send_success)) \r\n");
                                                                    __script.Append("BEGIN \r\n");
                                                                    __script.Append("RETURN \r\n");
                                                                    __script.Append("END \r\n");
                                                                    __script.Append("UPDATE " + __tableName + " SET send_success = 0 WHERE EXISTS (select i.guid from inserted i WHERE i.guid = " + __tableName + ".guid ) and " + __tableName + ".send_success != 0 \r\n");
                                                                    __script.Append("END \r\n");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                }
                                                                break;
                                                            default:
                                                                {

                                                                    // add guid field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid DEFAULT newid()");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // guid index
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE INDEX " + __tableName + "_guid_idx  \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("USING btree \r\n");
                                                                    __script.Append("(guid); \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // add send_success field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN send_success boolean DEFAULT false");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // after delete function
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_after_delete() \r\n");
                                                                    __script.Append("RETURNS trigger AS \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("BEGIN \r\n");
                                                                    __script.Append("insert into guid_delete (table_name,guid) values ('" + __tableName + "',OLD.guid); \r\n");
                                                                    __script.Append("RETURN NEW; \r\n");
                                                                    __script.Append("END; \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                                    __script.Append("COST 100; \r\n");
                                                                    __script.Append("ALTER FUNCTION " + __tableName + "_after_delete() OWNER TO postgres; \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // after delete trigger
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER " + __tableName + "_after_delete_trigger \r\n");
                                                                    __script.Append("AFTER DELETE \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("FOR EACH ROW \r\n");
                                                                    __script.Append("EXECUTE PROCEDURE " + __tableName + "_after_delete(); \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // before update function
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_before_update() \r\n");
                                                                    __script.Append("RETURNS trigger AS \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("BEGIN \r\n");
                                                                    __script.Append("IF OLD.send_success=true or OLD.send_success is null then \r\n");
                                                                    __script.Append("NEW.send_success= false;  \r\n");
                                                                    __script.Append("END IF; \r\n");
                                                                    __script.Append("RETURN NEW; \r\n");
                                                                    __script.Append("END; \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                                    __script.Append("COST 100; \r\n");
                                                                    __script.Append("ALTER FUNCTION " + __tableName + "_before_update() OWNER TO postgres; \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // before update trigger
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER " + __tableName + "_before_update_trigger \r\n");
                                                                    __script.Append("BEFORE UPDATE \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("FOR EACH ROW \r\n");
                                                                    __script.Append("EXECUTE PROCEDURE " + __tableName + "_before_update(); \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                #endregion
                                                #region Mode แลกเปลี่ยนข้อมูล
                                                case 3:
                                                    {
                                                        // จัดการ server ก่อน
                                                        switch (__centerDatabaseType)
                                                        {
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                {
                                                                    // add server branch_sync
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD branch_sync character varying(100) default ''; ");
                                                                    __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                                    // create index
                                                                    // index
                                                                    // send success
                                                                    __script = new StringBuilder();
                                                                    __script.Append("create nonclustered index " + __tableName + "_branch_sync_idx on " + __tableName + " (branch_sync asc);");
                                                                    __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                                }
                                                                break;
                                                            default:
                                                                {
                                                                    // add server branch_sync
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN branch_sync varchar(100) DEFAULT ''::character varying");
                                                                    __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());
                                                                    // branch_sync index
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE INDEX " + __tableName + "_branch_sync_idx  \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("USING btree \r\n");
                                                                    __script.Append("(branch_sync); \r\n");
                                                                    __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __script.ToString());

                                                                }
                                                                break;
                                                        }

                                                        // จัดการ field client
                                                        switch (__clientDatabaseType)
                                                        {
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                {
                                                                    // add guid field

                                                                    __script = new StringBuilder();
                                                                    if (__clientDatabaseType == MyLib._myGlobal._databaseType.MicrosoftSQL2000)
                                                                    {
                                                                        // sql server 2000
                                                                        __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWID();");
                                                                    }
                                                                    else
                                                                    {
                                                                        // sql server 2005
                                                                        __script.Append("ALTER TABLE " + __tableName + " ADD guid uniqueidentifier default NEWSEQUENTIALID();");
                                                                    }
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // create index
                                                                    __script = new StringBuilder();
                                                                    __script.Append("create nonclustered index " + __tableName + "_guid_idx on " + __tableName + " (guid asc);");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // add send success field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD send_success smallint default (0);");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // add is sync data field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD is_sync_in smallint default (0);");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    //after delete
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER  " + __tableName + "_after_delete \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("AFTER DELETE \r\n");
                                                                    __script.Append("AS \r\n");
                                                                    __script.Append("begin \r\n");
                                                                    __script.Append("insert into guid_delete(table_name,guid) \r\n");
                                                                    __script.Append("select '" + __tableName + "', i.guid from deleted i \r\n");
                                                                    __script.Append("end \r\n");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // before update
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER  " + __tableName + "_before_update \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("AFTER UPDATE \r\n");
                                                                    __script.Append("AS \r\n");
                                                                    __script.Append("begin \r\n");
                                                                    __script.Append("IF (UPDATE(send_success)) \r\n");
                                                                    __script.Append("BEGIN \r\n");
                                                                    __script.Append("RETURN \r\n");
                                                                    __script.Append("END \r\n");
                                                                    __script.Append("UPDATE " + __tableName + " SET send_success = 0 WHERE EXISTS (select i.guid from inserted i WHERE i.guid = " + __tableName + ".guid ) and " + __tableName + ".send_success != 0 \r\n");
                                                                    __script.Append("END \r\n");
                                                                    __execute_result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                }
                                                                break;
                                                            default:
                                                                {

                                                                    // add guid field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN guid uuid DEFAULT newid()");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                    // guid index
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE INDEX " + __tableName + "_guid_idx  \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("USING btree \r\n");
                                                                    __script.Append("(guid); \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // add send_success field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN send_success boolean DEFAULT false");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // add send_success field
                                                                    __script = new StringBuilder();
                                                                    __script.Append("ALTER TABLE " + __tableName + " ADD COLUMN is_sync_in boolean DEFAULT false");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // after delete function
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_after_delete() \r\n");
                                                                    __script.Append("RETURNS trigger AS \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("BEGIN \r\n");
                                                                    __script.Append("insert into guid_delete (table_name,guid) values ('" + __tableName + "',OLD.guid); \r\n");
                                                                    __script.Append("RETURN NEW; \r\n");
                                                                    __script.Append("END; \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                                    __script.Append("COST 100; \r\n");
                                                                    __script.Append("ALTER FUNCTION " + __tableName + "_after_delete() OWNER TO postgres; \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // after delete trigger
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER " + __tableName + "_after_delete_trigger \r\n");
                                                                    __script.Append("AFTER DELETE \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("FOR EACH ROW \r\n");
                                                                    __script.Append("EXECUTE PROCEDURE " + __tableName + "_after_delete(); \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // before update function
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE OR REPLACE FUNCTION " + __tableName + "_before_update() \r\n");
                                                                    __script.Append("RETURNS trigger AS \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("BEGIN \r\n");

                                                                    // check mode 
                                                                    __script.Append("IF ( TG_OP = 'INSERT') THEN \r\n");
                                                                    // insert mode
                                                                    __script.Append("IF (NEW.is_sync_in = false ) THEN \r\n");
                                                                    __script.Append("NEW.send_success = false; \r\n");
                                                                    __script.Append("ELSE \r\n");
                                                                    __script.Append("NEW.send_success = true; \r\n");
                                                                    __script.Append("END IF; \r\n");

                                                                    __script.Append("ELSE \r\n");
                                                                    // mode update
                                                                    // check guid_code 
                                                                    //__script.Append("IF OLD.guid_code = NEW.guid_code THEN \r\n");

                                                                    __script.Append("IF (NEW.is_sync_in = true ) THEN \r\n");
                                                                    __script.Append("NEW.send_success= true;  \r\n");
                                                                    __script.Append("ELSE \r\n");
                                                                    // -- update จาก client
                                                                    __script.Append("IF (OLD.send_success=true or OLD.send_success is null) THEN \r\n");
                                                                    __script.Append("NEW.send_success= false;  \r\n");
                                                                    __script.Append("END IF; \r\n");

                                                                    __script.Append("END IF; \r\n");
                                                                    // end check sync server

                                                                    //__script.Append("END IF; \r\n");
                                                                    // end check update guid

                                                                    __script.Append("END IF; \r\n");

                                                                    __script.Append("NEW.is_sync_in= false;  \r\n");
                                                                    __script.Append("RETURN NEW; \r\n");
                                                                    __script.Append("END; \r\n");
                                                                    __script.Append("$BODY$ \r\n");
                                                                    __script.Append("LANGUAGE plpgsql VOLATILE \r\n");
                                                                    __script.Append("COST 100; \r\n");
                                                                    __script.Append("ALTER FUNCTION " + __tableName + "_before_update() OWNER TO postgres; \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());

                                                                    // before update trigger
                                                                    __script = new StringBuilder();
                                                                    __script.Append("CREATE TRIGGER " + __tableName + "_before_update_trigger \r\n");
                                                                    __script.Append("BEFORE INSERT OR UPDATE \r\n");
                                                                    __script.Append("ON " + __tableName + " \r\n");
                                                                    __script.Append("FOR EACH ROW \r\n");
                                                                    __script.Append("EXECUTE PROCEDURE " + __tableName + "_before_update(); \r\n");
                                                                    __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
                                                                }
                                                                break;
                                                        }

                                                    }
                                                    break;
                                                #endregion
                                            }
                                        }*/
                                    }
                                    __createdScript = true;
                                    // เริ่มทำงาน
                                    for (int __tableLoop = 0; __tableLoop < _tableList.Count && MyLib._myGlobal._syncDataActive == true; __tableLoop++)
                                    {
                                        string __tableName = _tableList[__tableLoop]._tableName;
                                        int __limitSendRecord = _tableList[__tableLoop]._limitSendRecord;
                                        int __limitReceiveRecord = _tableList[__tableLoop]._limitReceiveRecord;

                                        // update receive count
                                        switch (_tableList[__tableLoop]._transferType)
                                        {
                                            case 2:
                                            case 3:
                                                // select count and update to server
                                                DataTable __countResult = null;
                                                switch (__clientDatabaseType)
                                                {
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                        {
                                                            __countResult = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select count(*)  from " + __tableName + " where coalesce(send_success, 0)=0 ").Tables[0];
                                                        }
                                                        break;
                                                    default:
                                                        {
                                                            __countResult = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select count(*) from " + __tableName + " where send_success=false ").Tables[0];
                                                        }
                                                        break;
                                                }

                                                if (__countResult != null)
                                                {
                                                    int __count = (int)MyLib._myGlobal._decimalPhase(__countResult.Rows[0][0].ToString());

                                                    StringBuilder __queryUpdateReceive = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                    __queryUpdateReceive.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_receive_data (branch_code ,table_name) select branch_code, table_name from (select sync_branch_list.branch_code, sync_table_list.table_name from sync_branch_list, sync_table_list ) as temp where not exists (select * from sync_receive_data where sync_receive_data.branch_code = temp.branch_code and sync_receive_data.table_name = temp.table_name) "));


                                                    __queryUpdateReceive.Append(MyLib._myUtil._convertTextToXmlForQuery("update sync_receive_data set " + _g.d.sync_receive_data._sync_count + "=" + __count + " where " + _g.d.sync_receive_data._branch_code + "=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and " + _g.d.sync_receive_data._table_name + "=\'" + __tableName + "\'"));
                                                    __queryUpdateReceive.Append("</node>");

                                                    __execute_result = __datacenterFrameWork._queryList(_g.g._companyProfile._activeSyncDatabase, __queryUpdateReceive.ToString());

                                                }
                                                break;
                                        }

                                        switch (_tableList[__tableLoop]._transferType)
                                        {
                                            #region Mode 1 : เริ่ม รับข้อมูล
                                            case 1: // รับข้อมูล
                                                {
                                                    try
                                                    {
                                                        // delete from sync_send_data_truncate_table
                                                        DataSet __getTruncateTable = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select * from sync_send_data_truncate_table where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                        if (__getTruncateTable.Tables.Count > 0 && __getTruncateTable.Tables[0].Rows.Count > 0)
                                                        {
                                                            if (__getTruncateTable.Tables[0].Rows[0]["table_name"].ToString().Equals(__tableName) == true)
                                                            {
                                                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                                                                {
                                                                    // ให้ลบ รหัสสินค้าทุกตัวที่จะส่งมาใหม่


                                                                    // color store ไมต้อง truncate 
                                                                    string __truncateResult = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + __tableName + " where is_lock_record=1");
                                                                    if (__truncateResult.Length == 0)
                                                                    {
                                                                        // กลับไปลบรายการฝั่ง server
                                                                        // update สถานะ send_success
                                                                        string __truncateResult2 = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_send_data_truncate_table where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                                        if (__truncateResult2.Length != 0)
                                                                        {
                                                                            _syncLog(__datacenterFrameWork, __truncateResult, __centerDatabaseType);
                                                                            MyLib._myGlobal._syncDataActive = false;
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        _syncLog(__datacenterFrameWork, __truncateResult);
                                                                        MyLib._myGlobal._syncDataActive = false;
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    // truncate โลด
                                                                    string __truncateResult = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table " + __tableName);

                                                                    if (__truncateResult.Length == 0)
                                                                    {
                                                                        // กลับไปลบรายการฝั่ง server
                                                                        // update สถานะ send_success
                                                                        __truncateResult = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_send_data_truncate_table where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                                        if (__truncateResult.Length != 0)
                                                                        {
                                                                            _syncLog(__datacenterFrameWork, __truncateResult, __centerDatabaseType);
                                                                            MyLib._myGlobal._syncDataActive = false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        _syncLog(__datacenterFrameWork, __truncateResult, __centerDatabaseType);
                                                                        MyLib._myGlobal._syncDataActive = false;

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }

                                                    DataSet __getGuidCountDs = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select  count(*) as xcount from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\'");
                                                    if (__getGuidCountDs.Tables.Count > 0)
                                                    {
                                                        DataTable __getGuidCount = __getGuidCountDs.Tables[0];
                                                        int __count = 0;
                                                        if (__getGuidCount.Rows.Count > 0)
                                                        {
                                                            __count = (int)MyLib._myGlobal._decimalPhase(__getGuidCount.Rows[0][0].ToString());
                                                        }
                                                        __getGuidCount.Dispose();
                                                        __getGuidCount = null;
                                                        if (__count > 0)
                                                        {
                                                            DataTable __selectField = null;
                                                            DataTable __compareField = null;
                                                            try
                                                            {
                                                                __selectField = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select  field_name,default_value from sync_field_list where table_name=\'" + __tableName + "\'").Tables[0];
                                                                __compareField = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select  field_name,default_value from sync_field_list where table_name=\'" + __tableName + "\'").Tables[0];
                                                            }
                                                            catch
                                                            {
                                                            }
                                                            if (__selectField != null && __compareField != null)
                                                            {
                                                                List<_syncFieldListStruct> __fieldServerList = new List<_syncFieldListStruct>();
                                                                // ดึงโครงสร้างฝั่ง Server
                                                                string __getXmlServre = __datacenterFrameWork._getSchemaTable(_g.g._companyProfile._activeSyncDatabase, __tableName);
                                                                List<_syncFieldListStruct> __fieldListServer = _getFieldFromXml(__tableName, __getXmlServre, __selectField);
                                                                // ดึงโครงสร้างฝั่ง Client
                                                                string __getXmlClient = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, __tableName);
                                                                List<_syncFieldListStruct> __fieldListClient = _getFieldFromXml(__tableName, __getXmlClient, __selectField);
                                                                // หาดูว่ามี Field รูปหรือเปล่า ถ้ามี ก็ให้ลดขนาดการรับส่ง
                                                                for (int __loop3 = 0; __loop3 < __fieldListServer.Count; __loop3++)
                                                                {
                                                                    if (__fieldListServer[__loop3]._fieldType.Equals("bytea") || __fieldListServer[__loop3]._fieldType.Equals("image"))
                                                                    {
                                                                        __limitReceiveRecord = 10;
                                                                        __limitSendRecord = 10;
                                                                        break;
                                                                    }
                                                                }
                                                                // ลบ Field ที่ไม่ตรงกัน ระหว่าง server,client
                                                                // หาจาก field server ถ้าไม่มีใน field client ลบ field server ออก
                                                                int __loop1 = 0;
                                                                while (__loop1 < __fieldListServer.Count)
                                                                {
                                                                    Boolean __found = false;
                                                                    for (int __loop2 = 0; __loop2 < __fieldListClient.Count && __found == false; __loop2++)
                                                                    {
                                                                        if (__fieldListServer[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListClient[__loop2]._fieldNameForInsertUpdate))
                                                                        {
                                                                            __found = true;
                                                                            break;
                                                                        }
                                                                    }
                                                                    if (__found == false)
                                                                    {
                                                                        __fieldListServer.RemoveAt(__loop1);
                                                                    }
                                                                    else
                                                                    {
                                                                        __loop1++;
                                                                    }
                                                                }
                                                                // หาใน field client ถ้าไม่มีใน field server ให้ลบ field client ออก
                                                                __loop1 = 0;
                                                                while (__loop1 < __fieldListClient.Count)
                                                                {
                                                                    Boolean __found = false;
                                                                    for (int __loop2 = 0; __loop2 < __fieldListServer.Count && __found == false; __loop2++)
                                                                    {
                                                                        if (__fieldListClient[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListServer[__loop2]._fieldNameForInsertUpdate))
                                                                        {
                                                                            __found = true;
                                                                            break;
                                                                        }
                                                                    }
                                                                    if (__found == false)
                                                                    {
                                                                        __fieldListClient.RemoveAt(__loop1);
                                                                    }
                                                                    else
                                                                    {
                                                                        __loop1++;
                                                                    }
                                                                }
                                                                //
                                                                /*List<_syncFieldListStruct> __compareFieldList = new List<_syncFieldListStruct>();
                                                                for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                                                {
                                                                    if (__compareField.Select("field_name=\'" + __fieldListClient[__loop]._fieldName + "\'").Length != 0)
                                                                    {
                                                                        _syncFieldListStruct __new2 = new _syncFieldListStruct();
                                                                        __new2._fieldName = __fieldListClient[__loop]._fieldName;
                                                                        __new2._fieldType = __fieldListClient[__loop]._fieldType;
                                                                        __compareFieldList.Add(__new2);
                                                                    }
                                                                }*/
                                                                // ประกอบ field
                                                                StringBuilder __fieldForInsert = new StringBuilder();
                                                                StringBuilder __fieldForSelect = new StringBuilder();

                                                                StringBuilder __fieldForServerSelect = new StringBuilder(); // ใส่ default value มาแล้ว

                                                                // toe เพิ่ม default value ต้อง แยก select ระหว่าง client และ center
                                                                for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                                                {
                                                                    if (__fieldForSelect.Length > 0)
                                                                    {
                                                                        __fieldForSelect.Append(",");
                                                                        __fieldForInsert.Append(",");
                                                                        __fieldForServerSelect.Append(",");
                                                                    }
                                                                    __fieldForSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());
                                                                    __fieldForInsert.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate.ToString());
                                                                    // toe
                                                                    __fieldForServerSelect.Append(((__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? ((__fieldListClient[__loop]._fieldType.Equals("varchar") || __fieldListClient[__loop]._fieldType.Equals("date") || __fieldListClient[__loop]._fieldType.Equals("char") || __fieldListClient[__loop]._fieldType.Equals("uuid")) ? "\'" : "") + __fieldListClient[__loop]._fieldDefaultValue + ((__fieldListClient[__loop]._fieldType.Equals("varchar") || __fieldListClient[__loop]._fieldType.Equals("date") || __fieldListClient[__loop]._fieldType.Equals("char") || __fieldListClient[__loop]._fieldType.Equals("uuid")) ? "\'" : "") + " as " + __fieldListClient[__loop]._fieldNameForSelect : __fieldListClient[__loop]._fieldNameForSelect.ToString()));

                                                                }
                                                                // Update or Insert delete
                                                                while (MyLib._myGlobal._syncDataActive == true)
                                                                {
                                                                    // Delete
                                                                    while (MyLib._myGlobal._syncDataActive == true)
                                                                    {
                                                                        DataTable __getGuidDelete = null;
                                                                        try
                                                                        {
                                                                            string __getGuidQuery = "";
                                                                            __datacenterFrameWork._findDatabaseType();

                                                                            switch (__centerDatabaseType)
                                                                            {
                                                                                case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                                case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                                    {
                                                                                        __getGuidQuery = "select distinct top " + __limitReceiveRecord.ToString() + " guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=2 ";
                                                                                    }
                                                                                    break;
                                                                                default:
                                                                                    {
                                                                                        __getGuidQuery = "select  distinct guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=2 limit " + __limitReceiveRecord.ToString();
                                                                                    }
                                                                                    break;

                                                                            }

                                                                            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\synclog.txt", DateTime.Now.ToString() + " GET GUID DELETE : " + __getGuidQuery, true);

                                                                            __getGuidDelete = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, __getGuidQuery).Tables[0];
                                                                        }
                                                                        catch
                                                                        {
                                                                        }
                                                                        if (__getGuidDelete != null)
                                                                        {
                                                                            if (__getGuidDelete.Rows.Count == 0)
                                                                            {
                                                                                break;
                                                                            }
                                                                            else
                                                                            {
                                                                                StringBuilder __guidList = new StringBuilder();
                                                                                for (int __loop = 0; __loop < __getGuidDelete.Rows.Count; __loop++)
                                                                                {
                                                                                    if (__loop != 0)
                                                                                    {
                                                                                        __guidList.Append(",");
                                                                                    }
                                                                                    __guidList.Append("\'" + __getGuidDelete.Rows[__loop][0].ToString() + "\'");
                                                                                }
                                                                                // ลบฝั่ง สาขา
                                                                                string __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + __tableName + " where guid in (" + __guidList.ToString() + ")");
                                                                                if (__result.Length == 0)
                                                                                {
                                                                                    // update สถานะ send_success
                                                                                    __result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and command_mode=2 and guid in  (" + __guidList.ToString() + ")");
                                                                                    if (__result.Length != 0)
                                                                                    {
                                                                                        _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                        MyLib._myGlobal._syncDataActive = false;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                    MyLib._myGlobal._syncDataActive = false;
                                                                                }

                                                                                //
                                                                                __getGuidDelete.Dispose();
                                                                                __getGuidDelete = null;
                                                                            }
                                                                        }
                                                                    }
                                                                    // Update or Insert 
                                                                    DataTable __getGuid = null;
                                                                    try
                                                                    {
                                                                        string __getGuidQuery = "";
                                                                        __datacenterFrameWork._findDatabaseType();
                                                                        switch (__centerDatabaseType)
                                                                        {
                                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                                {
                                                                                    __getGuidQuery = "select  distinct top " + __limitReceiveRecord.ToString() + " guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=1 ";
                                                                                }
                                                                                break;
                                                                            default:
                                                                                {
                                                                                    __getGuidQuery = "select  distinct guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=1 limit " + __limitReceiveRecord.ToString();
                                                                                }
                                                                                break;

                                                                        }

                                                                        //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\synclog.txt", DateTime.Now.ToString() + "GET GUID QuERY : " + __getGuidQuery, true);
                                                                        __getGuid = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, __getGuidQuery).Tables[0];
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                    if (__getGuid != null)
                                                                    {
                                                                        if (__getGuid.Rows.Count == 0)
                                                                        {
                                                                            break;
                                                                        }
                                                                        else
                                                                        {
                                                                            StringBuilder __guidList = new StringBuilder();
                                                                            for (int __loop = 0; __loop < __getGuid.Rows.Count; __loop++)
                                                                            {
                                                                                if (__loop != 0)
                                                                                {
                                                                                    __guidList.Append(",");
                                                                                }
                                                                                __guidList.Append("\'" + __getGuid.Rows[__loop][0].ToString() + "\'");
                                                                            }
                                                                            // 
                                                                            DataTable __getDataFromCenter = null;
                                                                            try
                                                                            {
                                                                                __getDataFromCenter = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select " + __fieldForServerSelect.ToString() + ",guid from " + __tableName + " where guid in (" + __guidList.ToString() + ")").Tables[0];
                                                                            }
                                                                            catch
                                                                            {
                                                                            }
                                                                            if (__getDataFromCenter != null)
                                                                            {
                                                                                /*StringBuilder __where = new StringBuilder();
                                                                                for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                                                                                {
                                                                                    if (__rowData != 0)
                                                                                    {
                                                                                        __where.Append(" or ");
                                                                                    }
                                                                                    __where.Append("(");
                                                                                    for (int __loop = 0; __loop < __compareFieldList.Count; __loop++)
                                                                                    {
                                                                                        if (__loop != 0)
                                                                                        {
                                                                                            __where.Append(" and ");
                                                                                        }
                                                                                        __where.Append(__compareFieldList[__loop]._fieldName);
                                                                                        string __value = __getDataFromCenter.Rows[__rowData][__compareFieldList[__loop]._fieldName].ToString().Replace("\'", "\'\'");
                                                                                        switch (__compareFieldList[__loop]._fieldType.ToLower())
                                                                                        {
                                                                                            case "varchar": __where.Append("=\'" + __value + "\'");
                                                                                                break;
                                                                                            case "date": __where.Append((__value.Length == 0) ? " is null" : "=\'" + __value + "\'");
                                                                                                break;
                                                                                            default: // ตัวเลข
                                                                                                __where.Append((__value.Length == 0) ? "=0" : "=" + __value.ToString());
                                                                                                break;
                                                                                        }
                                                                                    }
                                                                                    __where.Append(")");
                                                                                }*/
                                                                                // ดึงข้อมูลจาก server ฝั่ง สาขา
                                                                                DataTable __getData = null;
                                                                                try
                                                                                {
                                                                                    __getData = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select " + __fieldForSelect.ToString() + ",guid from " + __tableName + " where guid in (" + __guidList.ToString() + ")").Tables[0];

                                                                                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\synclog.txt", DateTime.Now.ToString() + "GET DATA CLIENT QuERY : select " + __fieldForSelect.ToString() + ",guid from " + __tableName + " where guid in (" + __guidList.ToString() + ")", true);
                                                                                }
                                                                                catch
                                                                                {
                                                                                }
                                                                                if (__getData != null)
                                                                                {
                                                                                    // เปรียบเทียบข้อมูล ถ้าพบข้อมูลเก่าก็ update หรือ ถ้าไม่พบก็ insert
                                                                                    StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                                                    for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                                                                                    {
                                                                                        StringBuilder __query = new StringBuilder();
                                                                                        /*StringBuilder __whereInTable = new StringBuilder();
                                                                                        for (int __loop = 0; __loop < __compareFieldList.Count; __loop++)
                                                                                        {
                                                                                            if (__loop != 0)
                                                                                            {
                                                                                                __whereInTable.Append(" and ");
                                                                                            }
                                                                                            __whereInTable.Append(__compareFieldList[__loop]._fieldName);
                                                                                            string __value = __getDataFromCenter.Rows[__rowData][__compareFieldList[__loop]._fieldName].ToString().Replace("\'", "\'\'");
                                                                                            switch (__compareFieldList[__loop]._fieldType.ToLower())
                                                                                            {
                                                                                                case "varchar": __whereInTable.Append("=\'" + __value + "\'");
                                                                                                    break;
                                                                                                case "date": __whereInTable.Append((__value.Length == 0) ? " is null" : "=\'" + __value + "\'");
                                                                                                    break;
                                                                                                default: // ตัวเลข
                                                                                                    __whereInTable.Append((__value.Length == 0) ? "=0" : "=" + __value.ToString());
                                                                                                    break;
                                                                                            }
                                                                                        }*/
                                                                                        //Boolean __have = (__getData.Rows.Count == 0) ? false : ((__getData.Select(__whereInTable.ToString()).Length == 0) ? false : true);
                                                                                        string __guidServer = __getDataFromCenter.Rows[__rowData]["guid"].ToString();
                                                                                        Boolean __have = (__getData.Rows.Count == 0) ? false : ((__getData.Select("guid=\'" + __guidServer + "\'").Length == 0) ? false : true);
                                                                                        if (__have)
                                                                                        {
                                                                                            // update
                                                                                            __query.Append("update " + __tableName + " set ");
                                                                                            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                                                                            {
                                                                                                if (__loop != 0)
                                                                                                {
                                                                                                    __query.Append(",");
                                                                                                }
                                                                                                __query.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate + "=");
                                                                                                switch (__fieldListClient[__loop]._fieldType.ToLower())
                                                                                                {
                                                                                                    case "bytea":
                                                                                                    case "image":
                                                                                                        {
                                                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                                                                            __query.Append("decode(\'" + __value + "\',\'base64\')");
                                                                                                        }
                                                                                                        break;
                                                                                                    case "uniqueidentifier":
                                                                                                    case "uuid":
                                                                                                    case "varchar":
                                                                                                        {
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append("\'" + __value + "\'");
                                                                                                        }
                                                                                                        break;
                                                                                                    case "date":
                                                                                                    case "datetime":
                                                                                                    case "smalldatetime":
                                                                                                    case "timestamp":
                                                                                                        {
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                                                        }
                                                                                                        break;
                                                                                                    default: // ตัวเลข
                                                                                                        {
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                                                        }
                                                                                                        break;
                                                                                                }
                                                                                            }
                                                                                            //__query.Append(" where " + __whereInTable.ToString());
                                                                                            __query.Append(" where guid=\'" + __guidServer + "\'");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            // insert
                                                                                            __query.Append("insert into " + __tableName + " (" + __fieldForInsert + ",guid) values (");
                                                                                            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                                                                            {
                                                                                                if (__loop != 0)
                                                                                                {
                                                                                                    __query.Append(",");
                                                                                                }
                                                                                                switch (__fieldListClient[__loop]._fieldType.ToLower())
                                                                                                {
                                                                                                    case "bytea":
                                                                                                    case "image":
                                                                                                        {
                                                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                                                                            __query.Append("decode(\'" + __value + "\',\'base64\')");
                                                                                                        }
                                                                                                        break;
                                                                                                    case "uniqueidentifier":
                                                                                                    case "uuid":
                                                                                                    case "varchar":
                                                                                                        {
                                                                                                            //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append("\'" + __value + "\'");
                                                                                                        }
                                                                                                        break;
                                                                                                    case "date":
                                                                                                    case "datetime":
                                                                                                    case "smalldatetime":
                                                                                                    case "timestamp":
                                                                                                        {
                                                                                                            //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                                                        }
                                                                                                        break;
                                                                                                    default: // ตัวเลข
                                                                                                        {
                                                                                                            //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                                                        }
                                                                                                        break;
                                                                                                }
                                                                                            }
                                                                                            __query.Append(",\'" + __guidServer + "\')");
                                                                                        }
                                                                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                                                                                    }
                                                                                    __queryList.Append("</node>");

                                                                                    string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                                                                                    if (__result.Length == 0)
                                                                                    {
                                                                                        // update สถานะ send_success
                                                                                        __result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and command_mode=1 and guid in  (" + __guidList.ToString() + ")");
                                                                                        if (__result.Length != 0)
                                                                                        {
                                                                                            _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                            MyLib._myGlobal._syncDataActive = false;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                        MyLib._myGlobal._syncDataActive = false;
                                                                                    }
                                                                                    __getData.Dispose();
                                                                                    __getData = null;
                                                                                }
                                                                            }
                                                                            __getGuid.Dispose();
                                                                            __getGuid = null;
                                                                            __getDataFromCenter.Dispose();
                                                                            __getDataFromCenter = null;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            __selectField.Dispose();
                                                            __selectField = null;
                                                            __compareField.Dispose();
                                                            __compareField = null;
                                                        }
                                                    }
                                                    __getGuidCountDs.Dispose();
                                                    __getGuidCountDs = null;
                                                }
                                                break;
                                            #endregion
                                            #region Mode 2 : เริ่มส่งข้อมูล

                                            case 2: // ส่งเข้า
                                                {
                                                    DataTable __delete = null;
                                                    try
                                                    {
                                                        __delete = __clientFrameWork._queryShort("select guid from guid_delete where table_name=\'" + __tableName + "\'").Tables[0];
                                                    }
                                                    catch (Exception delEx)
                                                    {
                                                        _syncLog(__datacenterFrameWork, delEx.ToString() + ":" + "select guid from guid_delete where table_name=\'" + __tableName + "\'", __centerDatabaseType);
                                                    }
                                                    if (__delete != null)
                                                    {
                                                        #region ลบ// ไล่ลบก่อน
                                                        StringBuilder __guidList = new StringBuilder();
                                                        for (int __loop = 0; __loop < __delete.Rows.Count; __loop++)
                                                        {
                                                            if (__guidList.Length > 0)
                                                            {
                                                                __guidList.Append(",");
                                                            }
                                                            __guidList.Append("\'" + __delete.Rows[__loop][0].ToString() + "\'");
                                                        }
                                                        __delete.Dispose();
                                                        __delete = null;
                                                        if (__guidList.Length > 0)
                                                        {
                                                            string __result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from " + __tableName + " where guid in (" + __guidList.ToString() + ")");
                                                            if (__result.Length == 0)
                                                            {
                                                                __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from guid_delete where guid in (" + __guidList.ToString() + ")");
                                                            }
                                                        }
                                                        #endregion

                                                        // check resync send data
                                                        DataSet __getDataResend = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select * from sync_reset_receive_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                        if (__getDataResend.Tables.Count > 0 && __getDataResend.Tables[0].Rows.Count > 0)
                                                        {
                                                            if (__getDataResend.Tables[0].Rows[0]["table_name"].ToString().Equals(__tableName) == true)
                                                            {
                                                                string __updateSendFlat = "false";
                                                                switch (__clientDatabaseType)
                                                                {
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                        {
                                                                            __updateSendFlat = "0";
                                                                        }
                                                                        break;
                                                                    default:
                                                                        {
                                                                            __updateSendFlat = "false";
                                                                        }
                                                                        break;
                                                                }

                                                                // toe fix 
                                                                string __getRowOrderMinMaxQuery = "select min(roworder) as row_min, max(roworder) as row_max  from " + __tableName;
                                                                DataSet __getRowMaxResult = __clientFrameWork._queryShort(__getRowOrderMinMaxQuery);
                                                                if (__getRowMaxResult.Tables.Count > 0)
                                                                {
                                                                    string __execute_result_updateresend = "";

                                                                    int __rowIndex = 0;
                                                                    int __rowmax = MyLib._myGlobal._intPhase(__getRowMaxResult.Tables[0].Rows[0][1].ToString());
                                                                    int __rowMin = MyLib._myGlobal._intPhase(__getRowMaxResult.Tables[0].Rows[0][1].ToString());
                                                                    __rowIndex = __rowMin;
                                                                    while (__rowmax >= __rowIndex)
                                                                    {
                                                                        __execute_result_updateresend = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set send_success=" + __updateSendFlat + " where roworder between " + __rowIndex + " and " + (__rowIndex + 5000).ToString());

                                                                        if (__execute_result_updateresend.Length > 0)
                                                                        {
                                                                            break;
                                                                        }
                                                                        __rowIndex += 5000;
                                                                    }

                                                                    if (__execute_result_updateresend.Length == 0)
                                                                    {
                                                                        __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_reset_receive_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                                    }
                                                                }


                                                                //string __execute_result_updateresend = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set send_success=" + __updateSendFlat);
                                                                //if (__execute_result_updateresend.Length == 0)
                                                                //{
                                                                //    __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_reset_receive_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                                //}

                                                            }
                                                        }


                                                        // โอนเข้า
                                                        List<_syncFieldListStruct> __fieldList = new List<_syncFieldListStruct>();
                                                        // ดึงโครงสร้าง
                                                        string __getXml = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, __tableName);
                                                        XmlDocument __xDoc = new XmlDocument();
                                                        __xDoc.LoadXml(__getXml);
                                                        __xDoc.DocumentElement.Normalize();
                                                        XmlElement __xRoot = __xDoc.DocumentElement;
                                                        XmlNodeList __xReader = __xRoot.GetElementsByTagName("detail");
                                                        for (int __detail = 0; __detail < __xReader.Count; __detail++)
                                                        {
                                                            XmlNode __xFirstNode = __xReader.Item(__detail);
                                                            if (__xFirstNode.NodeType == XmlNodeType.Element)
                                                            {
                                                                XmlElement __xTable = (XmlElement)__xFirstNode;
                                                                if (__tableName.ToLower().Equals(__xTable.GetAttribute("table_name").ToLower()))
                                                                {
                                                                    string __type = __xTable.GetAttribute("type").ToLower();
                                                                    if (__type.Equals("varchar") ||
                                                                        __type.Equals("date") ||
                                                                        __type.Equals("datetime") ||
                                                                        __type.Equals("timestamp") ||
                                                                        __type.Equals("smalldatetime") ||
                                                                        __type.Equals("int") ||
                                                                        __type.Equals("int2") ||
                                                                        __type.Equals("int4") ||
                                                                        __type.Equals("int8") ||
                                                                        __type.Equals("char") ||
                                                                        __type.Equals("uniqueidentifier") ||
                                                                        __type.Equals("uuid") ||
                                                                        __type.Equals("bool") ||
                                                                        __type.Equals("numeric") ||
                                                                        __type.Equals("decimal") ||
                                                                        __type.Equals("bytea") ||
                                                                        __type.Equals("image") ||
                                                                        __type.Equals("float8"))
                                                                    {
                                                                        string __fieldName = __xTable.GetAttribute("column_name").ToLower();
                                                                        if (__fieldName.Equals("send_success") == false && __fieldName.Equals("is_sync_in") == false)
                                                                        {
                                                                            _syncFieldListStruct __new = new _syncFieldListStruct();
                                                                            __new._fieldNameForInsertUpdate = __fieldName;
                                                                            if (__type.Equals("bytea") || __type.Equals("image"))
                                                                            {
                                                                                __new._fieldNameForSelect = "encode(" + __fieldName + ",\'base64\') as " + __fieldName;
                                                                                // Field รูปหรือเปล่า ถ้ามี ก็ให้ลดขนาดการรับส่ง
                                                                                __limitReceiveRecord = 10;
                                                                                __limitSendRecord = 10;
                                                                            }
                                                                            else
                                                                            {
                                                                                __new._fieldNameForSelect = __fieldName;
                                                                            }
                                                                            __new._fieldType = __type;
                                                                            __fieldList.Add(__new);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //
                                                        StringBuilder __fieldForInsert = new StringBuilder();
                                                        StringBuilder __fieldForSelect = new StringBuilder();
                                                        for (int __loop = 0; __loop < __fieldList.Count; __loop++)
                                                        {
                                                            if (__fieldForSelect.Length > 0)
                                                            {
                                                                __fieldForSelect.Append(",");
                                                                __fieldForInsert.Append(",");
                                                            }
                                                            __fieldForSelect.Append(__fieldList[__loop]._fieldNameForSelect);
                                                            __fieldForInsert.Append(__fieldList[__loop]._fieldNameForInsertUpdate);
                                                        }
                                                        while (MyLib._myGlobal._syncDataActive == true)
                                                        {
                                                            DataTable __clientData = null;
                                                            string __updateSuccessValue = "true";
                                                            try
                                                            {
                                                                switch (__clientDatabaseType)
                                                                {
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                        {
                                                                            __clientData = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select top " + __limitSendRecord.ToString() + " " + __fieldForSelect + " from " + __tableName + " where coalesce(send_success, 0)=0 ").Tables[0];
                                                                            __updateSuccessValue = "1";
                                                                        }
                                                                        break;
                                                                    default:
                                                                        {
                                                                            __clientData = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select " + __fieldForSelect + " from " + __tableName + " where send_success=false limit " + __limitSendRecord.ToString()).Tables[0];
                                                                            __updateSuccessValue = "true";
                                                                        }
                                                                        break;
                                                                }
                                                            }
                                                            catch
                                                            {
                                                            }

                                                            if (__clientData != null)
                                                            {
                                                                if (__clientData.Rows.Count == 0)
                                                                {
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    // ส่งข้อมูลเข้า
                                                                    StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                                    StringBuilder __guidSelect = new StringBuilder();
                                                                    StringBuilder __queryInsert = new StringBuilder();
                                                                    for (int __loopRow = 0; __loopRow < __clientData.Rows.Count; __loopRow++)
                                                                    {
                                                                        if (__loopRow != 0)
                                                                        {
                                                                            __guidSelect.Append(",");
                                                                        }
                                                                        __guidSelect.Append("\'" + __clientData.Rows[__loopRow]["guid"].ToString() + "\'");
                                                                        StringBuilder __data = new StringBuilder();
                                                                        for (int __loopColumn = 0; __loopColumn < __fieldList.Count; __loopColumn++)
                                                                        {
                                                                            if (__loopColumn != 0)
                                                                            {
                                                                                __data.Append(",");
                                                                            }
                                                                            switch (__fieldList[__loopColumn]._fieldType.ToLower())
                                                                            {
                                                                                case "image":
                                                                                case "bytea":
                                                                                    {
                                                                                        string __value = __clientData.Rows[__loopRow][__loopColumn].ToString();
                                                                                        __data.Append("decode(\'" + __value + "\',\'base64\')");
                                                                                    }
                                                                                    break;
                                                                                case "uniqueidentifier":
                                                                                case "uuid":
                                                                                case "varchar":
                                                                                    {
                                                                                        string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                        __data.Append("\'" + __value + "\'");
                                                                                    }
                                                                                    break;
                                                                                case "date":
                                                                                case "datetime":
                                                                                case "smalldatetime":
                                                                                case "timestamp":
                                                                                    {
                                                                                        string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                        __data.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                                    }
                                                                                    break;
                                                                                default: // ตัวเลข
                                                                                    {
                                                                                        string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                        __data.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                                    }
                                                                                    break;
                                                                            }
                                                                        }
                                                                        __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __tableName + " (branch_sync," + __fieldForInsert + ") values (\'" + _g.g._companyProfile._activeSyncBranchCode + "\'," + __data.ToString() + ")"));
                                                                    }
                                                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + __tableName + " where guid in (" + __guidSelect.ToString() + ")"));
                                                                    __query.Append(__queryInsert.ToString());
                                                                    __query.Append("</node>");
                                                                    string __result = __datacenterFrameWork._queryList(_g.g._companyProfile._activeSyncDatabase, __query.ToString());
                                                                    if (__result.Length == 0)
                                                                    {
                                                                        // toe
                                                                        if (_g.g._companyProfile._use_point_center)
                                                                        {
                                                                            // สั่ง process ที่ center
                                                                            if (__tableName == _g.d.ic_trans._table)
                                                                            {
                                                                                string __queryPointBalance = SMLProcess._posProcess._processPointBalanceQuery("", "", "", true);
                                                                                string __resultPointBalance = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __queryPointBalance);
                                                                            }
                                                                        }

                                                                        // update สถานะ sned_success
                                                                        __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set send_success=" + __updateSuccessValue + " where guid in  (" + __guidSelect.ToString() + ")");
                                                                        if (__result.Length != 0)
                                                                        {
                                                                            _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                            MyLib._myGlobal._syncDataActive = false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                        MyLib._myGlobal._syncDataActive = false;
                                                                    }
                                                                }
                                                                __clientData.Dispose();
                                                                __clientData = null;
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            #endregion
                                            #region โหมดแลกเปลี่ยน
                                            case 3:
                                                {

                                                    // รับเข้า
                                                    #region ขารับเข้า
                                                    DataSet __getGuidCountDs = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select  count(*) as xcount from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\'");
                                                    if (__getGuidCountDs.Tables.Count > 0)
                                                    {
                                                        DataTable __getGuidCount = __getGuidCountDs.Tables[0];
                                                        int __count = 0;
                                                        if (__getGuidCount.Rows.Count > 0)
                                                        {
                                                            __count = (int)MyLib._myGlobal._decimalPhase(__getGuidCount.Rows[0][0].ToString());
                                                        }
                                                        __getGuidCount.Dispose();
                                                        __getGuidCount = null;
                                                        if (__count > 0)
                                                        {
                                                            DataTable __selectField = null;
                                                            DataTable __compareField = null;
                                                            try
                                                            {
                                                                __selectField = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select  field_name,default_value from sync_field_list where table_name=\'" + __tableName + "\'").Tables[0];
                                                                __compareField = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select  field_name,default_value from sync_field_list where table_name=\'" + __tableName + "\'").Tables[0];
                                                            }
                                                            catch
                                                            {
                                                            }
                                                            if (__selectField != null && __compareField != null)
                                                            {
                                                                List<_syncFieldListStruct> __fieldServerList = new List<_syncFieldListStruct>();
                                                                // ดึงโครงสร้างฝั่ง Server
                                                                string __getXmlServre = __datacenterFrameWork._getSchemaTable(_g.g._companyProfile._activeSyncDatabase, __tableName);
                                                                List<_syncFieldListStruct> __fieldListServer = _getFieldFromXml(__tableName, __getXmlServre, __selectField);
                                                                // ดึงโครงสร้างฝั่ง Client
                                                                string __getXmlClient = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, __tableName);
                                                                List<_syncFieldListStruct> __fieldListClient = _getFieldFromXml(__tableName, __getXmlClient, __selectField);
                                                                // หาดูว่ามี Field รูปหรือเปล่า ถ้ามี ก็ให้ลดขนาดการรับส่ง
                                                                for (int __loop3 = 0; __loop3 < __fieldListServer.Count; __loop3++)
                                                                {
                                                                    if (__fieldListServer[__loop3]._fieldType.Equals("bytea") || __fieldListServer[__loop3]._fieldType.Equals("image"))
                                                                    {
                                                                        __limitReceiveRecord = 10;
                                                                        __limitSendRecord = 10;
                                                                        break;
                                                                    }
                                                                }
                                                                // ลบ Field ที่ไม่ตรงกัน ระหว่าง server,client
                                                                // หาจาก field server ถ้าไม่มีใน field client ลบ field server ออก
                                                                int __loop1 = 0;
                                                                while (__loop1 < __fieldListServer.Count)
                                                                {
                                                                    Boolean __found = false;
                                                                    for (int __loop2 = 0; __loop2 < __fieldListClient.Count && __found == false; __loop2++)
                                                                    {
                                                                        if (__fieldListServer[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListClient[__loop2]._fieldNameForInsertUpdate))
                                                                        {
                                                                            __found = true;
                                                                            break;
                                                                        }
                                                                    }
                                                                    if (__found == false)
                                                                    {
                                                                        __fieldListServer.RemoveAt(__loop1);
                                                                    }
                                                                    else
                                                                    {
                                                                        __loop1++;
                                                                    }
                                                                }
                                                                // หาใน field client ถ้าไม่มีใน field server ให้ลบ field client ออก
                                                                __loop1 = 0;
                                                                while (__loop1 < __fieldListClient.Count)
                                                                {
                                                                    Boolean __found = false;
                                                                    for (int __loop2 = 0; __loop2 < __fieldListServer.Count && __found == false; __loop2++)
                                                                    {
                                                                        if (__fieldListClient[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListServer[__loop2]._fieldNameForInsertUpdate))
                                                                        {
                                                                            __found = true;
                                                                            break;
                                                                        }
                                                                    }
                                                                    if (__found == false)
                                                                    {
                                                                        __fieldListClient.RemoveAt(__loop1);
                                                                    }
                                                                    else
                                                                    {
                                                                        __loop1++;
                                                                    }
                                                                }

                                                                // ประกอบ field
                                                                StringBuilder __fieldForInsert = new StringBuilder();
                                                                StringBuilder __fieldForSelect = new StringBuilder();

                                                                StringBuilder __fieldForServerSelect = new StringBuilder(); // ใส่ default value มาแล้ว

                                                                // toe เพิ่ม default value ต้อง แยก select ระหว่าง client และ center
                                                                for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                                                {
                                                                    if (__fieldForSelect.Length > 0)
                                                                    {
                                                                        __fieldForSelect.Append(",");
                                                                        __fieldForInsert.Append(",");
                                                                        __fieldForServerSelect.Append(",");
                                                                    }
                                                                    __fieldForSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());
                                                                    __fieldForInsert.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate.ToString());
                                                                    // toe
                                                                    __fieldForServerSelect.Append(((__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? ((__fieldListClient[__loop]._fieldType.Equals("varchar") || __fieldListClient[__loop]._fieldType.Equals("date") || __fieldListClient[__loop]._fieldType.Equals("char") || __fieldListClient[__loop]._fieldType.Equals("uuid")) ? "\'" : "") + __fieldListClient[__loop]._fieldDefaultValue + ((__fieldListClient[__loop]._fieldType.Equals("varchar") || __fieldListClient[__loop]._fieldType.Equals("date") || __fieldListClient[__loop]._fieldType.Equals("char") || __fieldListClient[__loop]._fieldType.Equals("uuid")) ? "\'" : "") + " as " + __fieldListClient[__loop]._fieldNameForSelect : __fieldListClient[__loop]._fieldNameForSelect.ToString()));

                                                                }

                                                                // Update or Insert delete
                                                                while (MyLib._myGlobal._syncDataActive == true)
                                                                {
                                                                    // Delete
                                                                    #region Delete ก่อน (command_mode=2)

                                                                    while (MyLib._myGlobal._syncDataActive == true)
                                                                    {
                                                                        DataTable __getGuidDelete = null;
                                                                        try
                                                                        {
                                                                            string __getGuidQuery = "";
                                                                            __datacenterFrameWork._findDatabaseType();

                                                                            switch (__centerDatabaseType)
                                                                            {
                                                                                case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                                case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                                    {
                                                                                        __getGuidQuery = "select distinct top " + __limitReceiveRecord.ToString() + " guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=2 ";
                                                                                    }
                                                                                    break;
                                                                                default:
                                                                                    {
                                                                                        __getGuidQuery = "select  distinct guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=2 limit " + __limitReceiveRecord.ToString();
                                                                                    }
                                                                                    break;

                                                                            }

                                                                            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\synclog.txt", DateTime.Now.ToString() + " GET GUID DELETE : " + __getGuidQuery, true);

                                                                            __getGuidDelete = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, __getGuidQuery).Tables[0];
                                                                        }
                                                                        catch
                                                                        {
                                                                        }
                                                                        if (__getGuidDelete != null)
                                                                        {
                                                                            if (__getGuidDelete.Rows.Count == 0)
                                                                            {
                                                                                break;
                                                                            }
                                                                            else
                                                                            {
                                                                                StringBuilder __guidList = new StringBuilder();
                                                                                for (int __loop = 0; __loop < __getGuidDelete.Rows.Count; __loop++)
                                                                                {
                                                                                    if (__loop != 0)
                                                                                    {
                                                                                        __guidList.Append(",");
                                                                                    }
                                                                                    __guidList.Append("\'" + __getGuidDelete.Rows[__loop][0].ToString() + "\'");
                                                                                }
                                                                                // ลบฝั่ง สาขา
                                                                                string __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + __tableName + " where guid in (" + __guidList.ToString() + ")");
                                                                                if (__result.Length == 0)
                                                                                {
                                                                                    // update สถานะ send_success
                                                                                    __result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and command_mode=2 and guid in  (" + __guidList.ToString() + ")");
                                                                                    if (__result.Length == 0)
                                                                                    {
                                                                                        // ลบ query ฝั่ง client ป้องกันการส่งกลับ
                                                                                        __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from guid_delete where table_name = \'" + __tableName + "\' and guid in (" + __guidList.ToString() + ")");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                        MyLib._myGlobal._syncDataActive = false;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                    MyLib._myGlobal._syncDataActive = false;
                                                                                }

                                                                                //
                                                                                __getGuidDelete.Dispose();
                                                                                __getGuidDelete = null;
                                                                            }
                                                                        }
                                                                    }
                                                                    #endregion

                                                                    // Update or Insert 
                                                                    #region Insert Or Update (Command_mode=1)

                                                                    DataTable __getGuid = null;
                                                                    try
                                                                    {
                                                                        string __getGuidQuery = "";
                                                                        __datacenterFrameWork._findDatabaseType();
                                                                        switch (__centerDatabaseType)
                                                                        {
                                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                                {
                                                                                    __getGuidQuery = "select  distinct top " + __limitReceiveRecord.ToString() + " guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=1 ";
                                                                                }
                                                                                break;
                                                                            default:
                                                                                {
                                                                                    __getGuidQuery = "select  distinct guid from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and table_name=\'" + __tableName + "\' and command_mode=1 limit " + __limitReceiveRecord.ToString();
                                                                                }
                                                                                break;

                                                                        }

                                                                        //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\synclog.txt", DateTime.Now.ToString() + "GET GUID QuERY : " + __getGuidQuery, true);
                                                                        __getGuid = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, __getGuidQuery).Tables[0];
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                    if (__getGuid != null)
                                                                    {
                                                                        if (__getGuid.Rows.Count == 0)
                                                                        {
                                                                            break;
                                                                        }
                                                                        else
                                                                        {
                                                                            StringBuilder __guidList = new StringBuilder();
                                                                            for (int __loop = 0; __loop < __getGuid.Rows.Count; __loop++)
                                                                            {
                                                                                if (__loop != 0)
                                                                                {
                                                                                    __guidList.Append(",");
                                                                                }
                                                                                __guidList.Append("\'" + __getGuid.Rows[__loop][0].ToString() + "\'");
                                                                            }
                                                                            // 
                                                                            DataTable __getDataFromCenter = null;
                                                                            try
                                                                            {
                                                                                __getDataFromCenter = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select " + __fieldForServerSelect.ToString() + ",guid from " + __tableName + " where guid in (" + __guidList.ToString() + ")").Tables[0];
                                                                            }
                                                                            catch
                                                                            {
                                                                            }
                                                                            if (__getDataFromCenter != null)
                                                                            {
                                                                                // ดึงข้อมูลจาก server ฝั่ง สาขา
                                                                                DataTable __getData = null;
                                                                                try
                                                                                {
                                                                                    __getData = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select " + __fieldForSelect.ToString() + ",guid from " + __tableName + " where guid in (" + __guidList.ToString() + ")").Tables[0];

                                                                                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\synclog.txt", DateTime.Now.ToString() + "GET DATA CLIENT QuERY : select " + __fieldForSelect.ToString() + ",guid from " + __tableName + " where guid in (" + __guidList.ToString() + ")", true);
                                                                                }
                                                                                catch
                                                                                {
                                                                                }
                                                                                if (__getData != null)
                                                                                {
                                                                                    // เปรียบเทียบข้อมูล ถ้าพบข้อมูลเก่าก็ update หรือ ถ้าไม่พบก็ insert
                                                                                    StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                                                    for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                                                                                    {
                                                                                        StringBuilder __query = new StringBuilder();


                                                                                        //Boolean __have = (__getData.Rows.Count == 0) ? false : ((__getData.Select(__whereInTable.ToString()).Length == 0) ? false : true);
                                                                                        string __guidServer = __getDataFromCenter.Rows[__rowData]["guid"].ToString();
                                                                                        Boolean __have = (__getData.Rows.Count == 0) ? false : ((__getData.Select("guid=\'" + __guidServer + "\'").Length == 0) ? false : true);
                                                                                        if (__have)
                                                                                        {
                                                                                            // update
                                                                                            __query.Append("update " + __tableName + " set ");
                                                                                            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                                                                            {
                                                                                                if (__loop != 0)
                                                                                                {
                                                                                                    __query.Append(",");
                                                                                                }
                                                                                                __query.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate + "=");
                                                                                                switch (__fieldListClient[__loop]._fieldType.ToLower())
                                                                                                {
                                                                                                    case "bytea":
                                                                                                    case "image":
                                                                                                        {
                                                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                                                                            __query.Append("decode(\'" + __value + "\',\'base64\')");
                                                                                                        }
                                                                                                        break;
                                                                                                    case "uniqueidentifier":
                                                                                                    case "uuid":
                                                                                                    case "varchar":
                                                                                                        {
                                                                                                            // 
                                                                                                            //if (__fieldlistcl
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");

                                                                                                            if (__fieldListClient[__loop]._fieldNameForInsertUpdate.Equals("guid_code") == true && __value.Length == 0)
                                                                                                            {
                                                                                                                __query.Append("null");
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                __query.Append("\'" + __value + "\'");
                                                                                                            }
                                                                                                        }
                                                                                                        break;
                                                                                                    case "date":
                                                                                                    case "datetime":
                                                                                                    case "smalldatetime":
                                                                                                    case "timestamp":
                                                                                                        {
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                                                        }
                                                                                                        break;
                                                                                                    default: // ตัวเลข
                                                                                                        {
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                                                        }
                                                                                                        break;
                                                                                                }
                                                                                            }
                                                                                            //__query.Append(" where " + __whereInTable.ToString());
                                                                                            __query.Append(", is_sync_in=true ");
                                                                                            __query.Append(" where guid=\'" + __guidServer + "\'");
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            // insert
                                                                                            __query.Append("insert into " + __tableName + " (" + __fieldForInsert + ",guid,is_sync_in) values (");
                                                                                            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                                                                            {
                                                                                                if (__loop != 0)
                                                                                                {
                                                                                                    __query.Append(",");
                                                                                                }
                                                                                                switch (__fieldListClient[__loop]._fieldType.ToLower())
                                                                                                {
                                                                                                    case "bytea":
                                                                                                    case "image":
                                                                                                        {
                                                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                                                                            __query.Append("decode(\'" + __value + "\',\'base64\')");
                                                                                                        }
                                                                                                        break;
                                                                                                    case "uniqueidentifier":
                                                                                                    case "uuid":
                                                                                                    case "varchar":
                                                                                                        {
                                                                                                            //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");

                                                                                                            if (__fieldListClient[__loop]._fieldNameForInsertUpdate.Equals("guid_code") == true && __value.Length == 0)
                                                                                                            {
                                                                                                                __query.Append("null");
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                __query.Append("\'" + __value + "\'");
                                                                                                            }
                                                                                                        }
                                                                                                        break;
                                                                                                    case "date":
                                                                                                    case "datetime":
                                                                                                    case "smalldatetime":
                                                                                                    case "timestamp":
                                                                                                        {
                                                                                                            //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                                                        }
                                                                                                        break;
                                                                                                    default: // ตัวเลข
                                                                                                        {
                                                                                                            //string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            string __value = (__fieldListClient[__loop]._fieldDefaultValue.Length > 0) ? __fieldListClient[__loop]._fieldDefaultValue : __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                                                            __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                                                        }
                                                                                                        break;
                                                                                                }
                                                                                            }
                                                                                            __query.Append(",\'" + __guidServer + "\', true)");
                                                                                        }
                                                                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                                                                                    }
                                                                                    __queryList.Append("</node>");

                                                                                    string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                                                                                    if (__result.Length == 0)
                                                                                    {
                                                                                        // update สถานะ send_success
                                                                                        __result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_send_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode + "\' and command_mode=1 and guid in  (" + __guidList.ToString() + ")");
                                                                                        if (__result.Length != 0)
                                                                                        {
                                                                                            _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                            MyLib._myGlobal._syncDataActive = false;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                        MyLib._myGlobal._syncDataActive = false;
                                                                                    }
                                                                                    __getData.Dispose();
                                                                                    __getData = null;
                                                                                }
                                                                            }
                                                                            __getGuid.Dispose();
                                                                            __getGuid = null;
                                                                            __getDataFromCenter.Dispose();
                                                                            __getDataFromCenter = null;
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                            }
                                                            __selectField.Dispose();
                                                            __selectField = null;
                                                            __compareField.Dispose();
                                                            __compareField = null;
                                                        }
                                                    }
                                                    __getGuidCountDs.Dispose();
                                                    __getGuidCountDs = null;

                                                    #endregion


                                                    // ส่งออก
                                                    #region ขาส่งออก
                                                    // ไล่ลบก่อน
                                                    DataTable __delete = null;
                                                    try
                                                    {
                                                        __delete = __clientFrameWork._queryShort("select guid from guid_delete where table_name=\'" + __tableName + "\'").Tables[0];
                                                    }
                                                    catch
                                                    {
                                                    }

                                                    if (__delete != null)
                                                    {
                                                        StringBuilder __guidList = new StringBuilder();
                                                        for (int __loop = 0; __loop < __delete.Rows.Count; __loop++)
                                                        {
                                                            if (__guidList.Length > 0)
                                                            {
                                                                __guidList.Append(",");
                                                            }
                                                            __guidList.Append("\'" + __delete.Rows[__loop][0].ToString() + "\'");
                                                        }
                                                        __delete.Dispose();
                                                        __delete = null;
                                                        if (__guidList.Length > 0)
                                                        {
                                                            string __result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from " + __tableName + " where guid in (" + __guidList.ToString() + ")");
                                                            if (__result.Length == 0)
                                                            {
                                                                __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from guid_delete where guid in (" + __guidList.ToString() + ")");
                                                            }
                                                        }

                                                        // check resync send data
                                                        DataSet __getDataResend = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select * from sync_reset_receive_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                        if (__getDataResend.Tables.Count > 0 && __getDataResend.Tables[0].Rows.Count > 0)
                                                        {
                                                            if (__getDataResend.Tables[0].Rows[0]["table_name"].ToString().Equals(__tableName) == true)
                                                            {
                                                                string __updateSendFlat = "false";
                                                                switch (__clientDatabaseType)
                                                                {
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                        {
                                                                            __updateSendFlat = "0";
                                                                        }
                                                                        break;
                                                                    default:
                                                                        {
                                                                            __updateSendFlat = "false";
                                                                        }
                                                                        break;
                                                                }

                                                                string __execute_result_updateresend = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set send_success=" + __updateSendFlat);
                                                                if (__execute_result_updateresend.Length == 0)
                                                                {
                                                                    __execute_result = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from sync_reset_receive_data where branch_code=\'" + _g.g._companyProfile._activeSyncBranchCode.ToLower() + "\' and table_name=\'" + __tableName + "\' ");
                                                                }

                                                            }
                                                        }

                                                        // โอนเข้า
                                                        List<_syncFieldListStruct> __fieldList = new List<_syncFieldListStruct>();
                                                        // ดึงโครงสร้าง
                                                        string __getXml = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, __tableName);
                                                        XmlDocument __xDoc = new XmlDocument();
                                                        __xDoc.LoadXml(__getXml);
                                                        __xDoc.DocumentElement.Normalize();
                                                        XmlElement __xRoot = __xDoc.DocumentElement;
                                                        XmlNodeList __xReader = __xRoot.GetElementsByTagName("detail");
                                                        for (int __detail = 0; __detail < __xReader.Count; __detail++)
                                                        {
                                                            XmlNode __xFirstNode = __xReader.Item(__detail);
                                                            if (__xFirstNode.NodeType == XmlNodeType.Element)
                                                            {
                                                                XmlElement __xTable = (XmlElement)__xFirstNode;
                                                                if (__tableName.ToLower().Equals(__xTable.GetAttribute("table_name").ToLower()))
                                                                {
                                                                    string __type = __xTable.GetAttribute("type").ToLower();
                                                                    if (__type.Equals("varchar") ||
                                                                        __type.Equals("date") ||
                                                                        __type.Equals("datetime") ||
                                                                        __type.Equals("timestamp") ||
                                                                        __type.Equals("smalldatetime") ||
                                                                        __type.Equals("int") ||
                                                                        __type.Equals("int2") ||
                                                                        __type.Equals("int4") ||
                                                                        __type.Equals("int8") ||
                                                                        __type.Equals("char") ||
                                                                        __type.Equals("uniqueidentifier") ||
                                                                        __type.Equals("uuid") ||
                                                                        __type.Equals("bool") ||
                                                                        __type.Equals("numeric") ||
                                                                        __type.Equals("decimal") ||
                                                                        __type.Equals("bytea") ||
                                                                        __type.Equals("image") ||
                                                                        __type.Equals("float8"))
                                                                    {
                                                                        string __fieldName = __xTable.GetAttribute("column_name").ToLower();
                                                                        if (__fieldName.Equals("send_success") == false && __fieldName.Equals("is_sync_in") == false)
                                                                        {
                                                                            _syncFieldListStruct __new = new _syncFieldListStruct();
                                                                            __new._fieldNameForInsertUpdate = __fieldName;
                                                                            if (__type.Equals("bytea") || __type.Equals("image"))
                                                                            {
                                                                                __new._fieldNameForSelect = "encode(" + __fieldName + ",\'base64\') as " + __fieldName;
                                                                                // Field รูปหรือเปล่า ถ้ามี ก็ให้ลดขนาดการรับส่ง
                                                                                __limitReceiveRecord = 10;
                                                                                __limitSendRecord = 10;
                                                                            }
                                                                            else
                                                                            {
                                                                                __new._fieldNameForSelect = __fieldName;
                                                                            }
                                                                            __new._fieldType = __type;
                                                                            __fieldList.Add(__new);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //
                                                        StringBuilder __fieldForInsert = new StringBuilder();
                                                        StringBuilder __fieldForSelect = new StringBuilder();
                                                        for (int __loop = 0; __loop < __fieldList.Count; __loop++)
                                                        {
                                                            if (__fieldForSelect.Length > 0)
                                                            {
                                                                __fieldForSelect.Append(",");
                                                                __fieldForInsert.Append(",");
                                                            }
                                                            __fieldForSelect.Append(__fieldList[__loop]._fieldNameForSelect);
                                                            __fieldForInsert.Append(__fieldList[__loop]._fieldNameForInsertUpdate);
                                                        }
                                                        while (MyLib._myGlobal._syncDataActive == true)
                                                        {
                                                            DataTable __clientData = null;
                                                            string __updateSuccessValue = "true";
                                                            string __updateUnSuccessValue = "false";

                                                            try
                                                            {
                                                                switch (__clientDatabaseType)
                                                                {
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                                                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                                                        {
                                                                            __clientData = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select top " + __limitSendRecord.ToString() + " " + __fieldForSelect + " from " + __tableName + " where coalesce(send_success, 0)=0 ").Tables[0];
                                                                            __updateSuccessValue = "1";
                                                                            __updateUnSuccessValue = "0";
                                                                        }
                                                                        break;
                                                                    default:
                                                                        {
                                                                            __clientData = __clientFrameWork._query(MyLib._myGlobal._databaseName, "select " + __fieldForSelect + " from " + __tableName + " where send_success=false limit " + __limitSendRecord.ToString()).Tables[0];
                                                                            __updateSuccessValue = "true";
                                                                            __updateUnSuccessValue = "false";
                                                                        }
                                                                        break;
                                                                }
                                                            }
                                                            catch
                                                            {
                                                            }

                                                            if (__clientData != null)
                                                            {
                                                                if (__clientData.Rows.Count == 0)
                                                                {
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    // toe check ว่าที่ center มีข้อมูลอยู่หรือเปล่า 
                                                                    // toe pack guid 
                                                                    StringBuilder __guidServerCheck = new StringBuilder();
                                                                    for (int __loop = 0; __loop < __clientData.Rows.Count; __loop++)
                                                                    {
                                                                        if (__guidServerCheck.Length > 0)
                                                                        {
                                                                            __guidServerCheck.Append(",");
                                                                        }
                                                                        __guidServerCheck.Append("\'" + __clientData.Rows[__loop]["guid"].ToString() + "\'");
                                                                    }

                                                                    DataTable __getServerData = null;
                                                                    try
                                                                    {
                                                                        __getServerData = __datacenterFrameWork._query(_g.g._companyProfile._activeSyncDatabase, "select guid from " + __tableName + " where guid in (" + __guidServerCheck.ToString() + ") ").Tables[0];
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Console.WriteLine(ex.Message.ToString());
                                                                    }

                                                                    if (__getServerData != null)
                                                                    {

                                                                        // ส่งข้อมูลเข้า
                                                                        StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                                        StringBuilder __guidSelect = new StringBuilder();
                                                                        StringBuilder __queryInsert = new StringBuilder();
                                                                        for (int __loopRow = 0; __loopRow < __clientData.Rows.Count; __loopRow++)
                                                                        {
                                                                            if (__loopRow != 0)
                                                                            {
                                                                                __guidSelect.Append(",");
                                                                            }
                                                                            __guidSelect.Append("\'" + __clientData.Rows[__loopRow]["guid"].ToString() + "\'");

                                                                            string __guidClient = __clientData.Rows[__loopRow]["guid"].ToString();

                                                                            // update send_success ก่อน
                                                                            string __result2 = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set send_success=" + __updateSuccessValue + ",is_sync_in=true where guid = \'" + __guidClient + "\' ");


                                                                            Boolean __have = (__getServerData.Rows.Count == 0) ? false : ((__getServerData.Select("guid=\'" + __guidClient + "\'").Length == 0) ? false : true);

                                                                            if (__have == true)
                                                                            {
                                                                                // update
                                                                                StringBuilder __data = new StringBuilder();
                                                                                __data.Append("update " + __tableName + " set ");

                                                                                for (int __loopColumn = 0; __loopColumn < __fieldList.Count; __loopColumn++)
                                                                                {
                                                                                    if (__loopColumn != 0)
                                                                                    {
                                                                                        __data.Append(",");
                                                                                    }

                                                                                    __data.Append(__fieldList[__loopColumn]._fieldNameForInsertUpdate + "=");
                                                                                    switch (__fieldList[__loopColumn]._fieldType.ToLower())
                                                                                    {
                                                                                        case "image":
                                                                                        case "bytea":
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString();
                                                                                                __data.Append("decode(\'" + __value + "\',\'base64\')");
                                                                                            }
                                                                                            break;
                                                                                        case "uniqueidentifier":
                                                                                        case "uuid":
                                                                                        case "varchar":
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");

                                                                                                if (__fieldList[__loopColumn]._fieldNameForInsertUpdate.Equals("guid_code") == true && __value.Length == 0)
                                                                                                {
                                                                                                    __data.Append("null");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    __data.Append("\'" + __value + "\'");
                                                                                                }
                                                                                            }
                                                                                            break;
                                                                                        case "date":
                                                                                        case "datetime":
                                                                                        case "smalldatetime":
                                                                                        case "timestamp":
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                                __data.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                                            }
                                                                                            break;
                                                                                        default: // ตัวเลข
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                                __data.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                                            }
                                                                                            break;
                                                                                    }
                                                                                }
                                                                                __data.Append(",branch_sync=\'" + _g.g._companyProfile._activeSyncBranchCode + "\'");
                                                                                __data.Append(" where guid=\'" + __guidClient + "\'");
                                                                                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(__data.ToString()));
                                                                            }
                                                                            else
                                                                            {

                                                                                StringBuilder __data = new StringBuilder();
                                                                                for (int __loopColumn = 0; __loopColumn < __fieldList.Count; __loopColumn++)
                                                                                {
                                                                                    if (__loopColumn != 0)
                                                                                    {
                                                                                        __data.Append(",");
                                                                                    }
                                                                                    switch (__fieldList[__loopColumn]._fieldType.ToLower())
                                                                                    {
                                                                                        case "image":
                                                                                        case "bytea":
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString();
                                                                                                __data.Append("decode(\'" + __value + "\',\'base64\')");
                                                                                            }
                                                                                            break;
                                                                                        case "uniqueidentifier":
                                                                                        case "uuid":
                                                                                        case "varchar":
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                                if (__fieldList[__loopColumn]._fieldNameForInsertUpdate.Equals("guid_code") == true && __value.Length == 0)
                                                                                                {
                                                                                                    __data.Append("null");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    __data.Append("\'" + __value + "\'");
                                                                                                }
                                                                                            }
                                                                                            break;
                                                                                        case "date":
                                                                                        case "datetime":
                                                                                        case "smalldatetime":
                                                                                        case "timestamp":
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                                __data.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                                            }
                                                                                            break;
                                                                                        default: // ตัวเลข
                                                                                            {
                                                                                                string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");
                                                                                                __data.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                                            }
                                                                                            break;
                                                                                    }
                                                                                }
                                                                                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __tableName + " (branch_sync," + __fieldForInsert + ") values (\'" + _g.g._companyProfile._activeSyncBranchCode + "\'," + __data.ToString() + ")"));
                                                                            }
                                                                        }
                                                                        // toe ย้ายไป compare __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + __tableName + " where guid in (" + __guidSelect.ToString() + ")"));

                                                                        __query.Append(__queryInsert.ToString());
                                                                        __query.Append("</node>");
                                                                        string __result = __datacenterFrameWork._queryList(_g.g._companyProfile._activeSyncDatabase, __query.ToString());
                                                                        if (__result.Length == 0)
                                                                        {
                                                                            // toe
                                                                            if (_g.g._companyProfile._use_point_center)
                                                                            {
                                                                                // สั่ง process ที่ center
                                                                                if (__tableName == _g.d.ic_trans._table)
                                                                                {
                                                                                    string __queryPointBalance = SMLProcess._posProcess._processPointBalanceQuery("", "", "", true);
                                                                                    string __resultPointBalance = __datacenterFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, __queryPointBalance);
                                                                                }
                                                                            }

                                                                            // update สถานะ sned_success
                                                                            //__result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set send_success=" + __updateSuccessValue + ",is_sync_in=true where guid in  (" + __guidSelect.ToString() + ")");
                                                                            //if (__result.Length != 0)
                                                                            //{
                                                                            //    _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                            //    MyLib._myGlobal._syncDataActive = false;
                                                                            //}
                                                                        }
                                                                        else
                                                                        {
                                                                            // update send_success = false;
                                                                            _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                            __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + __tableName + " set send_success=" + __updateUnSuccessValue + " where guid in  (" + __guidSelect.ToString() + ")");
                                                                            if (__result.Length != 0)
                                                                            {
                                                                                _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                                MyLib._myGlobal._syncDataActive = false;
                                                                            }

                                                                            _syncLog(__datacenterFrameWork, __result, __centerDatabaseType);
                                                                            MyLib._myGlobal._syncDataActive = false;
                                                                        }
                                                                    }
                                                                }
                                                                __clientData.Dispose();
                                                                __clientData = null;
                                                            }
                                                        }
                                                    }
                                                    #endregion

                                                }
                                                break;
                                            #endregion
                                        }
                                    }
                                    __tableList.Dispose();
                                    __tableList = null;
                                }
                            }
                        }
                        catch (Exception __ex)
                        {
                            MyLib._myGlobal._syncDataActive = false;
                            _syncLog(__datacenterFrameWork, __ex.Message.ToString() + ":" + __ex.StackTrace.ToString(), __centerDatabaseType);
                        }
                    }
                }
            }
        }
    }

}
