using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace DTSClientDownload
{
    public class _clientFrameWork
    {
        SqlConnection _objConn;
        SqlCommand _objCmd;
        SqlTransaction _objTrans;

        int _lastAffectedRow = -1;
        Boolean _inStanceByOhterServer = false;
        string _server = "";
        string _user = "";
        string _password = "";
        string _databasename = "";

        public Boolean _saveLogFile = true;
        public String _saveLogFileName = @"C:\\BCS\\dtsqoledb.txt";

        public _clientFrameWork()
        {

        }

        public _clientFrameWork(string server, string user, string password, string databasename)
        {
            this._inStanceByOhterServer = true;
            this._server = server;
            this._user = user;
            this._password = password;
            this._databasename = databasename;

        }

        private SqlConnection _getConnection
        {
            get
            {
                if (this._inStanceByOhterServer)
                {
                    return _global._getConnection(this._server, this._databasename, this._user, this._password);
                }

                return _global._sqlConnection;
            }
        }

        public String _excute(string __queryStr)
        {
            string __result = "";
            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();

            _objConn = _getConnection;
            _objConn.Open();

            int __effectRow = -1;

            if (_saveLogFile == true && this._saveLogFileName.Length > 0)
            {
                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.WriteLine("");
                    sw.WriteLine(DateTime.Now.ToString() + " " + __queryStr + "\n");
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }

            }

            try
            {

                _objCmd = new SqlCommand();
                _objCmd.Connection = _objConn;
                _objCmd.CommandType = CommandType.Text;
                _objCmd.CommandText = __queryStr;

                __effectRow = _objCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.ToString();
                //_appendLog(ex.ToString());
            }

            _lastAffectedRow = __effectRow;

            _objConn.Close();
            _objConn.Dispose();

            return __result;
        }
        
        public DataSet _query(string __queryStr)
        {
            DataSet ds = new DataSet();

            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();

            _objConn = _getConnection;
            _objConn.Open();

            if (_saveLogFile == true && this._saveLogFileName.Length > 0)
            {
                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.WriteLine("");
                    sw.WriteLine(DateTime.Now.ToString() + " " + __queryStr + "\n");
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }

            }

            try
            {

                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                if (_objConn != null)
                    if (_objConn.State == ConnectionState.Open)
                        _objConn.Close();


                _objCmd = new SqlCommand();
                _objCmd.Connection = _objConn;
                _objCmd.CommandText = __queryStr;
                _objCmd.CommandType = CommandType.Text;

                dtAdapter.SelectCommand = (SqlCommand)_objCmd;
                dtAdapter.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
                    
            }
            _objConn.Close();
            _objConn.Dispose();

            return ds;
        }

        public string _queryList(List<string> _queryList)
        {
            string __result = "";

            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();

            _objConn = _getConnection;
            _objConn.Open();

            _objTrans = _objConn.BeginTransaction(IsolationLevel.ReadCommitted);

            /*
            if (_saveLogFile == true && this._saveLogFileName.Length > 0)
            {
                // append log
                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.Write(DateTime.Now.ToString() + "Start Query");
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }


                // create new file
            }
             * */

            foreach (string __query in _queryList)
            {
                string _result = "";

                if (_saveLogFile == true && this._saveLogFileName.Length > 0)
                {
                    try
                    {
                        // append log
                        FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Append, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(aFile);
                        sw.WriteLine(DateTime.Now.ToString() + __query.ToString());
                        sw.Close();
                        aFile.Close();

                    }
                    catch
                    {
                    }

                }

                _result = TransExecute(__query);

                if (_result.Length > 0) // exception 
                {
                    // return error
                    __result = _result;
                    break;
                }
            }

            if (__result.Length == 0)
            {
                _objTrans.Commit();
            }
            else
            {
                // rollback 
                _objTrans.Rollback();
            }

            try
            {
                _objConn.Close();
                _objConn.Dispose();
            }
            catch
            {
            }

            return __result;
        }

        public DataTable _queryTable(String __queryStr)
        {
            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();

            _objConn = _getConnection;
            _objConn.Open();

            if (_saveLogFile == true && this._saveLogFileName.Length > 0)
            {
                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.WriteLine("");
                    sw.WriteLine(DateTime.Now.ToString() + " " + __queryStr + "\n");
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }

            }

            try
            {

                SqlDataAdapter dtAdapter;
                DataTable dt = new DataTable();

                dtAdapter = new SqlDataAdapter(__queryStr, (SqlConnection)_objConn);
                dtAdapter.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                //_appendLog(ex.ToString());
            }

            _objConn.Close();

            return null;
        }

        public void _clearLog()
        {
            if (_saveLogFile == true && this._saveLogFileName.Length > 0)
            {
                // append log
                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.Write(DateTime.Now.ToString() + " Start DTS Download Program \n");
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }


                // create new file
            }
        }

        public ArrayList _getDataList(List<string> queryList)
        {
            ArrayList __result = null;

            /* ย้ายไป clear log ตอนเข้าโปรแกรม
            if (_saveLogFile == true && this._saveLogFileName.Length > 0)
            {
                // append log
                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.Write(DateTime.Now.ToString() + " Start Query\n");
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }


                // create new file
            }
            */

            try
            {
                __result = new ArrayList();
                foreach (string __query in queryList)
                {
                    if (_saveLogFile == true && this._saveLogFileName.Length > 0)
                    {
                        try
                        {
                            // append log
                            FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Append, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(aFile);
                            sw.WriteLine("");
                            sw.WriteLine(DateTime.Now.ToString() + " " + __query.ToString() + "\n");
                            sw.Close();
                            aFile.Close();

                        }
                        catch
                        {
                        }

                    }

                    DataSet __ds = this._query(__query);
                    __result.Add(__ds);
                }
            }
            catch (Exception ex)
            {
                __result = null;
            }

            return __result;
        }

        public void TransStart()
        {
            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();

            _objConn = _getConnection;
            _objConn.Open();

            _objTrans = _objConn.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public string TransExecute(String strSQL)
        {
            string __result = "";

            if (_saveLogFile == true && this._saveLogFileName.Length > 0)
            {
                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.WriteLine("");
                    sw.WriteLine(DateTime.Now.ToString() + " " + strSQL + "\n");
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }

            }

            try
            {

                _objCmd = new SqlCommand();
                _objCmd.Connection = _objConn;
                _objCmd.Transaction = _objTrans;
                _objCmd.CommandType = CommandType.Text;
                _objCmd.CommandText = strSQL;
                _objCmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                __result = ex.Message.ToString();
            }

            try
            {
                _objCmd.Dispose();
            }
            catch
            {
            }

            return __result;


        }

        public void TransRollBack()
        {
            _objTrans.Rollback();
        }

        public void TransCommit()
        {
            _objTrans.Commit();
        }

        public Boolean _isTableExists(string tableName)
        {
            Boolean __result = false;

            string __query = "select name from sys.tables where name='" + tableName + "'";

            if (_getVersionNumber() < 9f)
            {
                __query = "SELECT name FROM sysobjects WHERE xtype='U' and name='" + tableName + "' ";
            }

            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();

            _objConn = _getConnection;
            _objConn.Open();

            DataSet ds = new DataSet();


            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();


            _objCmd = new SqlCommand();
            _objCmd.Connection = _objConn;
            _objCmd.CommandText = __query;
            _objCmd.CommandType = CommandType.Text;

            dtAdapter.SelectCommand = (SqlCommand)_objCmd;
            dtAdapter.Fill(ds);


            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString().ToLower().Equals(tableName.ToLower()))
                {
                    return true;
                }
            }

            return __result;
        }

        public DataSet _getTableSchema(string tableName)
        {
            DataSet __ds = new DataSet();
            if (_objConn != null)
                if (_objConn.State == ConnectionState.Open)
                    _objConn.Close();

            _objConn = _getConnection;
            _objConn.Open();

            string __query = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableName + "'";

            try
            {
                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                if (_objConn != null)
                    if (_objConn.State == ConnectionState.Open)
                        _objConn.Close();


                _objCmd = new SqlCommand();
                _objCmd.Connection = _objConn;
                _objCmd.CommandText = __query;
                _objCmd.CommandType = CommandType.Text;

                dtAdapter.SelectCommand = (SqlCommand)_objCmd;
                dtAdapter.Fill(__ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            _objConn.Close();
            _objConn.Dispose();
            return __ds;
        }

        public float _getVersionNumber()
        {
            float _versionNumber = 0f;

            DataSet __ds = this._query("select left(cast(serverproperty('productversion') as varchar), 4);");

            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
            {
                _versionNumber = (float)MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][0].ToString());
            }

            return _versionNumber;
        }
    }

}
