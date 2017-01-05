using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace MyLib
{
    public class _myDatabaseFrameWork
    {
        public MyLib._myGlobal._databaseType _type = _myGlobal._databaseType.PostgreSql;
        public string _host = "";
        public string _userCode = "";
        public string _password = "";
        public string _databaseName = "";
        public string _port = "";
        public string _lastErrorMeaages = "";

        private int _timeOutResult = -1;
        public int _connectionTimeOut
        {
            get
            {
                if (_timeOutResult == -1)
                    return 120 * 60;

                return _timeOutResult;
            }
            set
            {
                _timeOutResult = value;
            }
        }


        public _myDatabaseFrameWork()
        {

        }

        public _myDatabaseFrameWork(MyLib._myGlobal._databaseType type, string host, string port, string username, string password, string databaseName)
        {
            this._type = type;
            this._host = host;
            this._port = port;
            this._userCode = username;
            this._password = password;
            this._databaseName = databaseName;
        }

        SqlConnection _getSqlConnection()
        {
            return _getSqlConnection("");
        }

        SqlConnection _getSqlConnection(string timeOut)
        {
            string __connectionString = string.Format("Server={0};uid={1};pwd={2};Database={3}", this._host, this._userCode, this._password, this._databaseName);
            return new SqlConnection(__connectionString + timeOut);
        }

        Npgsql.NpgsqlConnection _getPostgresConnection()
        {
            return _getPostgresConnection("");
        }

        Npgsql.NpgsqlConnection _getPostgresConnection(string setTimeOut)
        {
            string __connectionString = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", this._host, this._port, this._userCode, this._password, this._databaseName);
            return new Npgsql.NpgsqlConnection(__connectionString + setTimeOut);
        }

        public ArrayList _queryList(List<string> queryList)
        {
            ArrayList __result = new ArrayList();
            try
            {
                foreach (string __query in queryList)
                {
                    DataSet __ds = this._query(__query);
                    __result.Add(__ds);
                }
            }
            catch
            {
                __result = null;
            }
            return __result;
        }

        #region Insert Or Update List

        public string _queryInsertOrUpdateList(List<string> queryList)
        {
            string __result = "";
            switch (this._type)
            {
                case _myGlobal._databaseType.PostgreSql:
                    __result = _postgresQueryInsertOrUpdateList(queryList);
                    break;
                case _myGlobal._databaseType.MicrosoftSQL2000:
                case _myGlobal._databaseType.MicrosoftSQL2005:
                    __result = _sqlServerQueryInsertOrUpdateList(queryList);
                    break;
            }

            return __result;
        }

        string _sqlServerQueryInsertOrUpdateList(List<string> queryList)
        {
            string __result = "";
            try
            {
                SqlConnection __conn = _getSqlConnection(";Connect Timeout=30000");
                __conn.Open();

                SqlTransaction __trans = __conn.BeginTransaction(IsolationLevel.ReadCommitted);

                foreach (string __query in queryList)
                {
                    try
                    {
                        SqlCommand __command = new SqlCommand();
                        __command.Connection = __conn;
                        __command.Transaction = __trans;
                        __command.CommandType = CommandType.Text;
                        __command.CommandText = __query;
                        __command.CommandTimeout = _connectionTimeOut;
                        __command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        __result = ex.Message.ToString();
                    }

                    if (__result.Length > 0)
                    {
                        break;
                    }
                }

                if (__result.Length == 0)
                {
                    __trans.Commit();
                }
                else
                {
                    __trans.Rollback();
                }

                __trans.Dispose();

                __conn.Close();
                __conn.Dispose();

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return __result;
        }

        string _postgresQueryInsertOrUpdateList(List<string> queryList)
        {
            string __result = "";
            try
            {
                Npgsql.NpgsqlConnection __conn = _getPostgresConnection("Timeout=1024;");
                __conn.Open();

                Npgsql.NpgsqlTransaction __trans = __conn.BeginTransaction(IsolationLevel.ReadCommitted);

                foreach (string __query in queryList)
                {
                    try
                    {
                        Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand();
                        __command.CommandTimeout = _connectionTimeOut;
                        __command.Connection = __conn;
                        __command.Transaction = __trans;
                        __command.CommandType = CommandType.Text;
                        __command.CommandText = __query;
                        __command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        __result = ex.Message.ToString();
                    }

                    if (__result.Length > 0)
                    {
                        break;
                    }
                }

                if (__result.Length == 0)
                {
                    __trans.Commit();
                }
                else
                {
                    // rollback 
                    __trans.Rollback();
                }

                __trans.Dispose();

                __conn.Close();
                __conn.Dispose();

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return __result;
        }

        #endregion 

        public string _queryInsertOrUpdate(string _queryStr)
        {
            try
            {
                switch (this._type)
                {
                    case _myGlobal._databaseType.PostgreSql:
                        {
                            Npgsql.NpgsqlConnection __conn = _getPostgresConnection();
                            __conn.Open();

                            Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand(_queryStr, __conn);
                            int result = __command.ExecuteNonQuery();

                            __conn.Close();
                            __conn.Dispose();
                        }
                        break;
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                        {
                            SqlConnection __conn = _getSqlConnection();
                            __conn.Open();

                            SqlCommand __command = new SqlCommand(_queryStr, __conn);
                            int result = __command.ExecuteNonQuery();

                            __conn.Close();
                            __conn.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "";
        }

        public DataSet _query(string queryStr)
        {
            this._lastErrorMeaages = "";
            DataSet __ds = new DataSet();
            try
            {
                switch (this._type)
                {
                    case _myGlobal._databaseType.PostgreSql:
                        {
                            Npgsql.NpgsqlConnection __conn = _getPostgresConnection();
                            __conn.Open();

                            Npgsql.NpgsqlDataAdapter __da = new Npgsql.NpgsqlDataAdapter();

                            Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand(queryStr, __conn);
                            __da.SelectCommand = __command;
                            __da.Fill(__ds);

                            __conn.Close();
                            __conn.Dispose();
                        }
                        break;
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                        {
                            SqlConnection __conn = _getSqlConnection();
                            __conn.Open();

                            SqlDataAdapter __da = new SqlDataAdapter();
                            SqlCommand __command = new SqlCommand(queryStr, __conn);
                            __da.SelectCommand = __command;
                            __da.Fill(__ds);

                            __conn.Close();
                            __conn.Dispose();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                _lastErrorMeaages = ex.Message;
            }
            return __ds;
        }

        public bool _testConnect()
        {
            try
            {

                switch (this._type)
                {
                    case _myGlobal._databaseType.PostgreSql:
                        {
                            Npgsql.NpgsqlConnection __conn = _getPostgresConnection();
                            __conn.Open();
                            __conn.Close();
                            __conn.Dispose();
                            return true;
                        }
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                        {
                            SqlConnection __conn = _getSqlConnection();
                            __conn.Open();
                            __conn.Close();
                            __conn.Dispose();
                            return true;
                        }
                }
            }
            catch
            {
            }

            return false;
        }

        #region GetField 

        public List<_databaseColumn> _getField(string tableName)
        {
            switch (this._type)
            {
                case _myGlobal._databaseType.PostgreSql:
                    return _getPostgreSQLField(tableName);
                case _myGlobal._databaseType.MicrosoftSQL2000:
                case _myGlobal._databaseType.MicrosoftSQL2005:
                    return _getSQLServerField(tableName);
            }
            return null;
        }

        List<_databaseColumn> _getSQLServerField(string tableName)
        {
            List<_databaseColumn> __columnList = new List<_databaseColumn>();

            try
            {
                SqlConnection __conn = _getSqlConnection();
                __conn.Open();

                DataSet __ds = new DataSet(); string __query = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableName + "'";

                try
                {
                    SqlDataAdapter dtAdapter = new SqlDataAdapter();

                    SqlCommand __command = new SqlCommand();
                    __command.Connection = __conn;
                    __command.CommandText = __query;
                    __command.CommandType = CommandType.Text;

                    dtAdapter.SelectCommand = (SqlCommand)__command;
                    dtAdapter.Fill(__ds);

                    if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                    {
                        for (int __col = 0; __col < __ds.Tables[0].Rows.Count; __col++)
                        {
                            _databaseColumnType __columnType = _databaseColumnType.String;
                            switch (__ds.Tables[0].Rows[__col]["data_type"].ToString())
                            {
                                case "smalldatetime":
                                    __columnType = _databaseColumnType.Datetime;
                                    break;
                                default:
                                    __columnType = _databaseColumnType.String;
                                    break;
                            }
                            string __columName = __ds.Tables[0].Rows[__col]["column_name"].ToString();
                            Boolean __isNull = __ds.Tables[0].Rows[__col]["is_nullable"].Equals("YES") ? true : false;

                            __columnList.Add(new _databaseColumn(__columnType, __columName, 0, false, __isNull));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                __conn.Close();
                __conn.Dispose();
            }
            catch
            {
            }

            return __columnList;
        }

        List<_databaseColumn> _getPostgreSQLField(string tableName)
        {
            List<_databaseColumn> __columnList = new List<_databaseColumn>();

            try
            {
                Npgsql.NpgsqlConnection __conn = _getPostgresConnection();
                __conn.Open();

                string __query = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableName + "'";

                try
                {
                    Npgsql.NpgsqlDataAdapter __da = new Npgsql.NpgsqlDataAdapter();
                    DataSet __ds = new DataSet();

                    Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand();
                    __command.Connection = __conn ;
                    __command.CommandText = __query;
                    __command.CommandType = CommandType.Text;

                    __da.SelectCommand = __command;
                    __da.Fill(__ds);

                    if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                    {
                        for (int __col = 0; __col < __ds.Tables[0].Rows.Count; __col++)
                        {
                            _databaseColumnType __columnType = _databaseColumnType.String;
                            switch (__ds.Tables[0].Rows[__col]["data_type"].ToString().ToLower()) {
                                case "date" :
                                    __columnType = _databaseColumnType.Datetime;
                                    break;
                                case "integer" :
                                case "smallint" :
                                case "numeric" :
                                    __columnType = _databaseColumnType.Number;
                                    break;
                                default :
                                    __columnType =  _databaseColumnType.String;
                                    break;
                            }
                            string __columName = __ds.Tables[0].Rows[__col]["column_name"].ToString();
                            Boolean __isNull = __ds.Tables[0].Rows[__col]["is_nullable"].Equals("YES") ? true : false;

                            __columnList.Add(new _databaseColumn(__columnType, __columName, 0, false, __isNull));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                __conn.Close();
                __conn.Dispose();
            }
            catch
            {
            }

            return __columnList;
        }

        #endregion
    }

    public class _databaseColumn
    {
        public _databaseColumn(_databaseColumnType type, string fieldName, int length, Boolean isPrimary, Boolean isNull)
        {
            this._columnType = type;
            this._columnName = fieldName;
            this._columnLength = length;
            this._isPrimaryKey = isPrimary;
            this._isNull = isNull;
        }

        public _databaseColumn(_databaseColumnType type, string fieldName, int length, Boolean isPrimary)
        {
            this._columnType = type;
            this._columnName = fieldName;
            this._columnLength = length;
            this._isPrimaryKey = isPrimary;
        }

        public _databaseColumnType _columnType = _databaseColumnType.String;
        public string _columnName = "";
        public int _columnLength = 0;
        public Boolean _isPrimaryKey = false;
        public Boolean _isNull = true;
    }

    public enum _databaseColumnType
    {
        String,
        Datetime,
        Number,
        Image
    }

}
