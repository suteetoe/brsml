using MyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    public abstract class _processBase
    {
        protected string _tableName = "";
        protected string _importedTableName = "";
        private string _addDataServiceUrl = "";
        private string _updateDataServiceUrl = "";

        public string tableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
                this._importedTableName = string.Format("{0}_imported", value);
                verifySendSuccessTable();
            }
        }

        protected abstract string _createTableImportCompleteScipt();

        void verifySendSuccessTable()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(Program.serverURL, "SMLConfig" + Program.provider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);

            string __checkTableQuery = string.Format("SELECT count(table_name) from information_schema.tables where table_name = '{0}' ", this._importedTableName);

            DataSet __result = __myFrameWork._query(Program.dbName, __checkTableQuery);

            if (__result.Tables.Count == 0 || (__result.Tables[0].Rows.Count > 0 && __result.Tables[0].Rows[0][0].ToString() == "0"))
            {
                string _script = _createTableImportCompleteScipt();
                string __getData = __myFrameWork._queryInsertOrUpdate(Program.dbName, _script);
                if (__getData.Length > 0)
                {
                    throw new Exception(__getData);
                }
            }
        }

        public string addDataServiceUrl
        {
            get
            {
                return this._addDataServiceUrl;
            }
            set
            {
                this._addDataServiceUrl = value;
            }
        }

        public void _startProcess()
        {
            // read data
            String __query = this.getDataQuery();


            DataSet __dataSet = this._getDataSml(__query);
            // start send

            if (__dataSet.Tables.Count > 0)
            {
                try
                {
                    DataRow[] __row = __dataSet.Tables[0].Select();
                    foreach (DataRow row in __row)
                    {
                        string __dataProcess = _processData(row);

                        string __sendResult = _sendData(__dataProcess);
                        if (__sendResult.Length == 0)
                        {
                            // save to send_success_table
                            afterImportSuccess(row);
                        }
                        else
                        {
                            if (afterSendDataFailed != null)
                            {
                                afterSendDataFailed(__sendResult, row);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected abstract string getDataQuery();

        protected abstract string _processData(DataRow row);

        public void afterImportSuccess(DataRow row)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(Program.serverURL, "SMLConfig" + Program.provider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
            string __queryInsertSendSuccess = afterSendDataSuccess(row);
            string __result = __myFrameWork._queryInsertOrUpdate(Program.dbName, __queryInsertSendSuccess);
           
            if (__result.Length > 0)
            {
                Console.WriteLine(__result);
            }
        }

        protected abstract string afterSendDataSuccess(DataRow row);


        public DataSet _getDataSml(string queryGetData)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(Program.serverURL, "SMLConfig" + Program.provider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
                DataSet __getData = __myFrameWork._queryStream(Program.dbName, queryGetData);

                return __getData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        private string _sendData(string data)
        {
#if (DEBUG)
            return "";
#endif
            // pack json

            MyLib._restClient __rest = new _restClient(this._addDataServiceUrl, HttpVerb.POST, data);
            __rest._setContentType("application/json");

            __rest._addHeaderRequest(string.Format("APIKey: {0}", Program.APIKey));
            __rest._addHeaderRequest(string.Format("APISecret: {0}", Program.APISecret));

            string __result = __rest.MakeRequest("");
            if (__result.Length > 0)
            {
#if (DEBUG)
                Debugger.Break();
#endif
                return __result;
            }

            // send request to restful api

            // if send success  return ""

            // else return error from restful

            return "";
        }


        public event afterSendDataFailedEvent afterSendDataFailed;
        public delegate void afterSendDataFailedEvent(String err, DataRow row);
    }


}
