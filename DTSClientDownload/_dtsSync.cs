using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;

namespace DTSClientDownload
{
    public class _dtsSync
    {
        private static List<SMLDataCenterSync._syncTableListStruct> _tableList = new List<SMLDataCenterSync._syncTableListStruct>();

        public static void _startSync()
        {
            MyLib._myFrameWork __centerFrameWork = new MyLib._myFrameWork();

            // toe fix script guid_delete
            StringBuilder __script = new StringBuilder();
            __script.Append("CREATE TABLE guid_delete \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder int IDENTITY(1,1) NOT NULL, \r\n");
            __script.Append("table_name character varying(100), \r\n");
            __script.Append("guid uniqueidentifier, \r\n");
            __script.Append("CONSTRAINT roworder_idx PRIMARY KEY (roworder) \r\n");
            __script.Append("); \r\n");
            _clientFrameWork __clientFrameWork = new _clientFrameWork();
            string __execute_result = __clientFrameWork._excute(__script.ToString());


            //Thread.Sleep(1000);
            int __waitTime = 0;

            __waitTime = 1000 * 10;

            if (__waitTime < 1000)
            {
                __waitTime = 1000;
            }

            while (true)
            {
                try
                {
                    // fixtable for sync
                    _tableList.Clear();
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcitem._table, _transferType = 1, _targetTableName = "dts_bcitem" });
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcstkpacking._table, _transferType = 1, _targetTableName = "dts_bcstkpacking" });
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.allunitcode._table, _transferType = 1, _targetTableName = "dts_allunitcode" });
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcpurchaseorder._table, _transferType = 1, _targetTableName = "dts_bcpurchaseorder" });
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcpurchaseordersub._table, _transferType = 1, _targetTableName = "dts_bcpurchaseordersub" });
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcsaleorder._table, _transferType = 1, _targetTableName = "dts_bcsaleorder" });
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcsaleordersub._table, _transferType = 1, _targetTableName = "dts_bcsaleordersub" });



                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.dts_download._table, _transferType = 2, _targetTableName = _g.DataServer.dts_download._table });
                    _tableList.Add(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.dts_download_detail._table, _transferType = 2, _targetTableName = _g.DataServer.dts_download_detail._table });


                    // pack table for sync
                    for (int __i = 0; __i < _tableList.Count; __i++)
                    {
                        _syncTable(_tableList[__i]);
                    }

                }
                catch (Exception __ex)
                {
                    MyLib._myGlobal._syncDataActive = false;
                    _syncLog(__centerFrameWork, __ex.Message.ToString() + ":" + __ex.StackTrace.ToString());

                }

                MyLib._myGlobal._syncDataActive = false;
                Thread.Sleep(__waitTime);
                MyLib._myGlobal._syncDataActive = true;

                try
                {
                    // สั่งลบรายการที่ server
                    _syncClientDownload(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcpurchaseordersub._table, _transferType = 1, _targetTableName = "dts_bcpurchaseordersub" });
                    _syncClientDownload(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcsaleordersub._table, _transferType = 1, _targetTableName = "dts_bcsaleordersub" });
                    _syncClientDownload(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcpurchaseorder._table, _transferType = 1, _targetTableName = "dts_bcpurchaseorder" }, true);
                    _syncClientDownload(new SMLDataCenterSync._syncTableListStruct() { _tableName = _g.DataServer.bcsaleorder._table, _transferType = 1, _targetTableName = "dts_bcsaleorder" }, true);


                }
                catch (Exception __ex)
                {
                    MyLib._myGlobal._syncDataActive = false;
                    _syncLog(__centerFrameWork, __ex.Message.ToString() + ":" + __ex.StackTrace.ToString());
                }

                MyLib._myGlobal._syncDataActive = false;
                Thread.Sleep(__waitTime);
                MyLib._myGlobal._syncDataActive = true;


            }


            // mode รับเข้า
        }

        public static void _syncClientDownload(SMLDataCenterSync._syncTableListStruct tableStruct)
        {
            _syncClientDownload(tableStruct, false);
        }

        /// <summary>ลบรายการที่นำเข้าแล้ว เช็คจากตาราง client_sync_data</summary>
        public static void _syncClientDownload(SMLDataCenterSync._syncTableListStruct tableStruct, Boolean _refCheck)
        {
            MyLib._myFrameWork __centerFrameWork = new MyLib._myFrameWork();
            _clientFrameWork __clientFrameWork = new _clientFrameWork();

            string __sql = "select count(*) as xcount from client_sync_data where table_name = \'" + tableStruct._tableName + "\' ";
            DataSet __queryCountresult = __clientFrameWork._query(__sql);

            if (__queryCountresult.Tables.Count > 0)
            {
                // start pack query sync
                DataTable __guidCountForSync = __queryCountresult.Tables[0];
                int __count = 0;
                if (__guidCountForSync.Rows.Count > 0)
                {
                    __count = (int)MyLib._myGlobal._decimalPhase(__guidCountForSync.Rows[0][0].ToString());
                }

                if (__count > 0)
                {
                    while (MyLib._myGlobal._syncDataActive == true)
                    {
                        DataTable __getGuid = null;
                        try
                        {
                            __getGuid = __clientFrameWork._query("select  distinct top " + tableStruct._limitSendRecord.ToString() + " guid from client_sync_data where table_name = \'" + tableStruct._tableName + "\'").Tables[0];
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
                                // pack query to delete data
                                StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                StringBuilder __guidList = new StringBuilder();

                                for (int __loop = 0; __loop < __getGuid.Rows.Count; __loop++)
                                {
                                    if (__guidList.Length > 0)
                                    {
                                        __guidList.Append(",");
                                    }
                                    __guidList.Append("\'" + __getGuid.Rows[__loop]["guid"].ToString() + "\'");

                                    Boolean __passCheck = true;
                                    if (_refCheck == true)
                                    {
                                        __passCheck = false;
                                        try {
                                            DataSet __ds = __centerFrameWork._query(MyLib._myGlobal._databaseName, "select count(*) as xcount from " + ((tableStruct._tableName.Equals("bcpurchaseorder") == true) ? _g.DataServer.bcpurchaseordersub._table : _g.DataServer.bcsaleordersub._table) + " where docno = (select docno from " + ((tableStruct._tableName.Equals("bcpurchaseorder") == true) ? _g.DataServer.bcpurchaseorder._table : _g.DataServer.bcsaleorder._table) + " where guid =\'" + __getGuid.Rows[__loop]["guid"].ToString() + "\' ) ");

                                            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][0].ToString()) == 0M)
                                            {
                                                __passCheck = true;
                                            }
                                        }
                                        catch {
                                        }
                                    }

                                    if (__passCheck == true)
                                    {
                                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + tableStruct._tableName + " where guid =\'" + __getGuid.Rows[__loop]["guid"].ToString() + "\'"));
                                    }
                                }
                                // 
                                __queryList.Append("</node>");

                                string __debugQuer = __queryList.ToString();

                                string __result = __centerFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                                if (__result.Length == 0)
                                {
                                    __result = __clientFrameWork._excute("delete from client_sync_data where table_name = \'" + tableStruct._tableName + "\' and guid in (" + __guidList.ToString() + ")");
                                    if (__result.Length > 0)
                                    {
                                        _syncLog(__centerFrameWork, __result);
                                        MyLib._myGlobal._syncDataActive = false;
                                    }
                                }
                                else
                                {
                                    _syncLog(__centerFrameWork, __result);
                                    MyLib._myGlobal._syncDataActive = false;
                                }
                            }
                        }
                    }

                }
            }

        }

        public static void _syncTable(SMLDataCenterSync._syncTableListStruct centerSyncTable)
        {
            MyLib._myFrameWork __centerFrameWork = new MyLib._myFrameWork();
            _clientFrameWork __clientFrameWork = new _clientFrameWork();
            __clientFrameWork._saveLogFile = false;
            StringBuilder __script;

            string __tableName = centerSyncTable._tableName;
            string __targetTableName = centerSyncTable._targetTableName;

            int __transferType = centerSyncTable._transferType;
            int __limitReceiveRecord = centerSyncTable._limitReceiveRecord;
            int __limitSendRecord = centerSyncTable._limitSendRecord;

            switch (__transferType)
            {
                case 1: // รับข้อมูล
                    {
                        // add guid field

                        __script = new StringBuilder();
                        __script.Append("ALTER TABLE " + __targetTableName + " ADD guid uniqueidentifier");
                        __clientFrameWork._excute(__script.ToString());

                        // guid index
                        __script = new StringBuilder();
                        __script.Append("create nonclustered index idx_synd_send_guid on " + __targetTableName + " (guid asc);");
                        __clientFrameWork._excute(__script.ToString());


                        // start sync

                        string __queryCount = "select count(*) as xcount from sync_send_data where ( agent_code = '" + MyLib._myGlobal._userCode + "' or right(agent_code, 7) = '" + MyLib._myGlobal._userCode + "' ) and table_name = \'" + __tableName + "\'";
                        DataSet __getGuidFromServer = __centerFrameWork._query(MyLib._myGlobal._databaseName, __queryCount);
                        if (__getGuidFromServer.Tables.Count > 0)
                        {
                            DataTable __getGuidCount = __getGuidFromServer.Tables[0];

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
                                try
                                {
                                    __selectField = __centerFrameWork._query(MyLib._myGlobal._databaseName, "select column_name as field_name, '' as default_value from INFORMATION_SCHEMA.COLUMNS where table_name=\'" + __tableName + "\'").Tables[0];
                                }
                                catch
                                {

                                }

                                // มีข้อมูล ให้เริ่ม pack แล้ว sync โลด
                                string __getXmlServre = __centerFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, __tableName);
                                List<SMLDataCenterSync._syncFieldListStruct> __fieldListServer = SMLDataCenterSync._run._getFieldFromXml(__tableName, __getXmlServre, __selectField);

                                List<SMLDataCenterSync._syncFieldListStruct> __fieldListClient = _getFieldFromClient(__targetTableName, __selectField);

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
                                for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                {
                                    if (__fieldForSelect.Length > 0)
                                    {
                                        __fieldForSelect.Append(",");
                                        __fieldForInsert.Append(",");
                                    }
                                    __fieldForSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());
                                    __fieldForInsert.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate.ToString());
                                }

                                // Update or Insert delete
                                while (MyLib._myGlobal._syncDataActive == true)
                                {
                                    // Delete
                                    #region Delete Method

                                    while (MyLib._myGlobal._syncDataActive == true)
                                    {
                                        DataTable __getGuidDelete = null;
                                        try
                                        {
                                            __getGuidDelete = __centerFrameWork._query(MyLib._myGlobal._databaseName, "select  distinct top " + __limitReceiveRecord.ToString() + " guid from sync_send_data where ( agent_code = '" + MyLib._myGlobal._userCode + "' or right(agent_code, 7) = '" + MyLib._myGlobal._userCode + "' ) and table_name=\'" + __tableName + "\' and command_mode=2 ").Tables[0];
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
                                                string __result = __clientFrameWork._excute("delete from " + __targetTableName + " where guid in (" + __guidList.ToString() + ")");
                                                if (__result.Length == 0)
                                                {
                                                    // update สถานะ send_success
                                                    __result = __centerFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from sync_send_data where ( agent_code = '" + MyLib._myGlobal._userCode + "' or right(agent_code, 7) = '" + MyLib._myGlobal._userCode + "' ) and command_mode=2 and guid in  (" + __guidList.ToString() + ")");
                                                    if (__result.Length != 0)
                                                    {
                                                        _syncLog(__centerFrameWork, __result);
                                                        MyLib._myGlobal._syncDataActive = false;
                                                    }
                                                }
                                                else
                                                {
                                                    _syncLog(__centerFrameWork, __result);
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

                                    #region Insert Or Update Method

                                    DataTable __getGuid = null;
                                    try
                                    {
                                        __getGuid = __centerFrameWork._query(MyLib._myGlobal._databaseName, "select  distinct top " + __limitReceiveRecord.ToString() + " guid from sync_send_data where ( agent_code = '" + MyLib._myGlobal._userCode + "' or right(agent_code, 7) = '" + MyLib._myGlobal._userCode + "' ) and table_name=\'" + __tableName + "\' and command_mode=1 ").Tables[0];
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
                                                __getDataFromCenter = __centerFrameWork._query(MyLib._myGlobal._databaseName, "select " + __fieldForSelect.ToString() + ",guid from " + __tableName + " where guid in (" + __guidList.ToString() + ")").Tables[0];
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
                                                    __getData = __clientFrameWork._query("select " + __fieldForSelect.ToString() + ",guid from " + __targetTableName + " where guid in (" + __guidList.ToString() + ")").Tables[0];
                                                }
                                                catch
                                                {
                                                }
                                                if (__getData != null)
                                                {
                                                    // เปรียบเทียบข้อมูล ถ้าพบข้อมูลเก่าก็ update หรือ ถ้าไม่พบก็ insert
                                                    //StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                                                    List<string> __queryList = new List<string>();

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
                                                            __query.Append("update " + __targetTableName + " set ");
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
                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                            __query.Append("\'" + __value + "\'");
                                                                        }
                                                                        break;
                                                                    case "date":
                                                                    case "datetime":
                                                                        {
                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                            __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                        }
                                                                        break;
                                                                    default: // ตัวเลข
                                                                        {
                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
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
                                                            __query.Append("insert into " + __targetTableName + " (" + __fieldForInsert + ",guid) values (");
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
                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                            __query.Append("\'" + __value + "\'");
                                                                        }
                                                                        break;
                                                                    case "date":
                                                                    case "datetime":
                                                                        {
                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                            __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                                                        }
                                                                        break;
                                                                    default: // ตัวเลข
                                                                        {
                                                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                                            __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                                                        }
                                                                        break;
                                                                }
                                                            }
                                                            __query.Append(",\'" + __guidServer + "\')");
                                                        }
                                                        __queryList.Add(__query.ToString());
                                                    }

                                                    //__queryList.Append("</node>");

                                                    string __result = __clientFrameWork._queryList(__queryList);
                                                    if (__result.Length == 0)
                                                    {
                                                        // update สถานะ send_success
                                                        __result = __centerFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from sync_send_data where ( agent_code = '" + MyLib._myGlobal._userCode + "' or right(agent_code, 7) = '" + MyLib._myGlobal._userCode + "' ) and command_mode=1 and guid in  (" + __guidList.ToString() + ")");
                                                        if (__result.Length != 0)
                                                        {
                                                            _syncLog(__centerFrameWork, __result);
                                                            MyLib._myGlobal._syncDataActive = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _syncLog(__centerFrameWork, __result);
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
                                __selectField.Dispose();
                                __selectField = null;
                            }
                        }
                    }
                    break;
                case 2: // ส่งข้อมูล 
                    {
                        DataTable __delete = null;
                        try
                        {
                            __delete = __clientFrameWork._query("select guid from guid_delete where table_name=\'" + __tableName + "\'").Tables[0];
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
                                string __result = __centerFrameWork._queryInsertOrUpdate(_g.g._companyProfile._activeSyncDatabase, "delete from " + __tableName + " where guid in (" + __guidList.ToString() + ")");
                                if (__result.Length == 0)
                                {
                                    __clientFrameWork._excute("delete from guid_delete where guid in (" + __guidList.ToString() + ")");
                                }
                            }

                            // โอนเข้า
                            List<SMLDataCenterSync._syncFieldListStruct> __fieldList = _getFieldFromClient(__tableName, null, "roworder,send_success");
                            // ดึงโครงสร้าง
                            /*
                            List<SMLDataCenterSync._syncFieldListStruct> __fieldListClient = _getFieldFromClient(__tableName, null);


                            for (int __detail = 0; __detail < __fieldListClient.Count; __detail++)
                            {
                                //XmlNode __xFirstNode = __xReader.Item(__detail);
                                //if (__xFirstNode.NodeType == XmlNodeType.Element)
                                //{
                                //XmlElement __xTable = (XmlElement)__xFirstNode;
                                //if (__tableName.ToLower().Equals(__xTable.GetAttribute("table_name").ToLower()))
                                //{
                                string __type = __fieldListClient[__detail]._fieldType.ToLower();
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
                                    __type.Equals("float8"))
                                {
                                    string __fieldName = __fieldListClient[__detail]._fieldNameForSelect.ToLower(); // __xTable.GetAttribute("column_name").ToLower();
                                    if (__fieldName.Equals("send_success") == false)
                                    {
                                        SMLDataCenterSync._syncFieldListStruct __new = new SMLDataCenterSync._syncFieldListStruct();
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
                                //}
                                //}
                            }
                            //
                             * */

                            StringBuilder __fieldForInsert = new StringBuilder();
                            StringBuilder __fieldForSelect = new StringBuilder();
                            for (int __loop = 0; __loop < __fieldList.Count; __loop++)
                            {
                                if (__fieldForSelect.Length > 0)
                                {

                                    __fieldForSelect.Append(",");
                                    __fieldForInsert.Append(",");
                                }

                                if (__fieldList[__loop]._fieldType == "datetime")
                                {
                                    __fieldForSelect.Append("(CONVERT(varchar," + __fieldList[__loop]._fieldNameForSelect + ", 120)) as " + __fieldList[__loop]._fieldNameForSelect);
                                    __fieldForInsert.Append(__fieldList[__loop]._fieldNameForInsertUpdate);
                                }
                                else
                                {
                                    __fieldForSelect.Append(__fieldList[__loop]._fieldNameForSelect);
                                    __fieldForInsert.Append(__fieldList[__loop]._fieldNameForInsertUpdate);
                                }
                            }
                            while (MyLib._myGlobal._syncDataActive == true)
                            {
                                DataTable __clientData = __clientFrameWork._query("select top " + __limitSendRecord.ToString() + " " + __fieldForSelect + " from " + __tableName + " where send_success=0 ").Tables[0];
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
                                                    {
                                                        string __value = __clientData.Rows[__loopRow][__loopColumn].ToString().Replace("\'", "\'\'");

                                                        DateTime __tempDate = MyLib._myGlobal._convertDateFromQuery(__value);
                                                        string __dateValue = MyLib._myGlobal._convertDateTimeToQuery(__tempDate);

                                                        __data.Append((__value.Length == 0) ? "null" : "\'" + __dateValue + "\'");
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
                                        __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __tableName + " (" + __fieldForInsert + ") values (" + __data.ToString() + ")"));
                                    }
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + __tableName + " where guid in (" + __guidSelect.ToString() + ")"));
                                    __query.Append(__queryInsert.ToString());
                                    __query.Append("</node>");
                                    string __result = __centerFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                                    if (__result.Length == 0)
                                    {
                                        // update สถานะ sned_success
                                        __result = __clientFrameWork._excute("update " + __tableName + " set send_success=1 where guid in  (" + __guidSelect.ToString() + ")");
                                        if (__result.Length != 0)
                                        {
                                            _syncLog(__centerFrameWork, __result);
                                            MyLib._myGlobal._syncDataActive = false;
                                        }
                                    }
                                    else
                                    {
                                        _syncLog(__centerFrameWork, __result);
                                        MyLib._myGlobal._syncDataActive = false;
                                    }
                                }
                                __clientData.Dispose();
                                __clientData = null;
                            }
                        }
                    }
                    break;
                case 3:
                    {

                    }
                    break;
            }
        }

        public static List<SMLDataCenterSync._syncFieldListStruct> _getFieldFromClient(string tableName, DataTable selectField)
        {
            return _getFieldFromClient(tableName, selectField, "");
        }

        public static List<SMLDataCenterSync._syncFieldListStruct> _getFieldFromClient(string tableName, DataTable selectField, string exceptColumn)
        {
            string[] __exceptTableName;
            if (exceptColumn.Length == 0)
            {
                __exceptTableName = new string[] { "roworder", "guid" };
            }
            else
            {
                __exceptTableName = exceptColumn.Split(',');
            }

            List<SMLDataCenterSync._syncFieldListStruct> __fieldList = new List<SMLDataCenterSync._syncFieldListStruct>();

            _clientFrameWork __frameWork = new _clientFrameWork();
            DataSet __ds = __frameWork._getTableSchema(tableName);

            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
            {
                for (int __row = 0; __row < __ds.Tables[0].Rows.Count; __row++)
                {
                    if (tableName.ToLower().Equals(__ds.Tables[0].Rows[__row]["table_name"].ToString().ToLower()))
                    {
                        string __type = __ds.Tables[0].Rows[__row]["data_type"].ToString().ToLower();
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

                            // toe for sql server
                            __type.Equals("float") ||
                            __type.Equals("money") ||
                            __type.Equals("datetime") ||
                            __type.Equals("smallint") ||
                            __type.Equals("uniqueidentifier"))
                        {
                            string __fieldName = __ds.Tables[0].Rows[__row]["column_name"].ToString().ToLower();
                            //if ((isGuidGet || __fieldName.Equals("guid") == false) && (_isSendSuccess || __fieldName.Equals("send_success") == false) && __fieldName.Equals("roworder") == false)
                            //{
                            if (__exceptTableName != null && Array.IndexOf(__exceptTableName, __fieldName) == -1)
                            {
                                if (selectField == null || selectField.Select("field_name=\'" + __fieldName + "\'").Length != 0)
                                {
                                    SMLDataCenterSync._syncFieldListStruct __new = new SMLDataCenterSync._syncFieldListStruct();
                                    __new._fieldNameForInsertUpdate = __fieldName;
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
                    }
                }
            }

            return __fieldList;
        }

        private static void _syncLog(MyLib._myFrameWork datacenterFrameWork, string message)
        {
            if (message.Trim().Length > 0)
            {
                datacenterFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.sync_log._table + "(" + _g.d.sync_log._date_time + "," + _g.d.sync_log._branch_code + "," + _g.d.sync_log._message + ") values (GETDATE(),\'" + MyLib._myGlobal._userCode + "\',\'" + message.Replace("\'", " ").Replace("\"", " ").Replace("\r", " ").Replace("\n", " ") + "\')");
                Thread.Sleep(10000);
            }
        }
    }
}
