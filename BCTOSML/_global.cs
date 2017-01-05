using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Npgsql;

namespace BCTOSML
{
    public static class _global
    {
        public static string _sourceServerName = "";
        public static string _sourceUserCode = "";
        public static string _sourceUserPassword = "";
        public static string _sourceDatabase = "";
        //
        /// <summary>
        /// 0=Postgres,1=MSSQL
        /// </summary>
        public static int _tagetDatabaseType = 0;
        public static string _targetServerName = "";
        public static string _targetUserCode = "";
        public static string _targetUserPassword = "";
        public static string _targetDatabase = "";

        public static SqlConnection _sourceConnect()
        {
            string __sqlConnectString = "Data Source=" + _sourceServerName + ";Initial Catalog=" + _sourceDatabase + ";User ID=" + _sourceUserCode + ";Password=" + _sourceUserPassword + ";Connect Timeout=600000;";
            return new SqlConnection(__sqlConnectString);
        }

        public static NpgsqlConnection _targetConnect()
        {
            string __sqlConnectString = "Server=" + _targetServerName + ";Port=5432;User Id=" + _targetUserCode + ";Password=" + _targetUserPassword + ";Database=" + _targetDatabase + ";";
            return new NpgsqlConnection(__sqlConnectString);
        }
    }
}
