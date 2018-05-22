using MyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

            DataTable __result = __myFrameWork._queryShort(__checkTableQuery).Tables[0];

            if (__result.Rows.Count > 0 && __result.Rows[0][0].ToString() == "0" )
            {
                string _script = _createTableImportCompleteScipt();
                string __getData = __myFrameWork._queryInsertOrUpdate(Program.dbName, _script);
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
            __myFrameWork._queryInsertOrUpdate(Program.dbName, __queryInsertSendSuccess);
        }

        protected abstract string afterSendDataSuccess(DataRow row);


        public DataSet _getDataSml(string queryGetData)
        {

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(Program.serverURL, "SMLConfig" + Program.provider.ToUpper() + ".xml", MyLib._myGlobal._databaseType.PostgreSql);
            DataSet __getData = __myFrameWork._queryStream(Program.dbName, queryGetData);
            return __getData;
        }


        private string _sendData(string data)
        {
            // start call service api

            // if send success 
            return "";
        }


        public event afterSendDataFailedEvent afterSendDataFailed;
        public delegate void afterSendDataFailedEvent(String err, DataRow row);
    }


}
