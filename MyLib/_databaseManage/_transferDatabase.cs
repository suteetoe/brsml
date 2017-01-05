using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace MyLib._databaseManage
{
    public partial class _transferDatabase : Form
    {
        List<String> _scriptArray = new List<String>();
        List<String> _scriptSourceBegin = new List<String>();
        List<String> _scriptSourceEnd = new List<String>();
        List<String> _scriptTargetBegin = new List<String>();
        List<String> _scriptTargetEnd = new List<String>();

        string _targetDatabaseName = "";
        string _fieldTableName = "Table Name";
        string _fieldTotalRecords = "Total Records";
        string _fieldTransferSuccess = "Transfer Success";
        string _fieldCheck = "Check";
        int _providerSourceSelect = 0;
        int _transferSelect = 0;
        string _scriptFileName = "";

        public _transferDatabase()
        {
            InitializeComponent();

            this._providerComboBox.SelectedIndexChanged += new EventHandler(_providerComboBox_SelectedIndexChanged);
            this._providerComboBox.SelectedIndex = 0;
            this._databaseComboBox.DropDown += new EventHandler(_databaseComboBox_DropDown);
            this._databaseComboBox.TextChanged += new EventHandler(_databaseComboBox_TextChanged);
            //
            this._webServiceTextBox.Text = ((MyLib._myWebserviceType)MyLib._myGlobal._webServiceServerList[0])._webServiceName;
            //
            this._resultGrid._table_name = "";
            this._resultGrid._getResource = false;
            this._resultGrid._addColumn(_fieldTableName, 1, 20, 45);
            this._resultGrid._addColumn(_fieldTotalRecords, 1, 20, 25);
            this._resultGrid._addColumn(_fieldTransferSuccess, 1, 20, 25);
            this._resultGrid._addColumn(_fieldCheck, 11, 20, 5);
            //
            this._transferModeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=Table,1=View,9=All</param>
        /// <returns></returns>
        private DataTable _getTableNameFromDatabase(int mode, Boolean updateGrid)
        {
            DataTable __result = null;
            try
            {
                switch (this._transferSelect)
                {
                    case 1: // BC Account -> SML
                        this._processSourceScript(this._scriptSourceBegin);
                        break;
                }
                Object __connObject = null;
                switch (this._providerSourceSelect)
                {
                    case 0: // PostgresSQL
                        {
                            string __query = "select tablename from pg_tables where schemaname=\'public\' order by tablename";
                            DataSet __dset = new DataSet();
                            string __connstring = this._postgresSqlConnString(this._databaseComboBox.Text);
                            __connObject = new Npgsql.NpgsqlConnection(__connstring);
                            Npgsql.NpgsqlConnection __conn = (Npgsql.NpgsqlConnection)__connObject;
                            __conn.Open();
                            Npgsql.NpgsqlDataAdapter __npAdapter = new Npgsql.NpgsqlDataAdapter();
                            __npAdapter.SelectCommand = new Npgsql.NpgsqlCommand(__query, __conn);
                            __npAdapter.Fill(__dset);
                            __result = __dset.Tables[0];
                            __conn.Close();
                        }
                        break;
                    case 2: // Microsoft SQL
                        {
                            string __tableType = "";
                            switch (mode)
                            {
                                case 0: __tableType = "\'BASE TABLE\'"; break;
                                case 1: __tableType = "\'VIEW\'"; break;
                                case 9: __tableType = "\'BASE TABLE\',\'VIEW\'"; break;
                            }
                            string __query = "select table_name as tablename from INFORMATION_SCHEMA.Tables where TABLE_TYPE in (" + __tableType + ") order by tablename";
                            DataSet __dset = new DataSet();
                            string __connstring = this.__microsoftSqlConnString(this._databaseComboBox.Text);
                            __connObject = new SqlConnection(__connstring);
                            SqlConnection __conn = (SqlConnection)__connObject;
                            __conn.Open();
                            SqlDataAdapter __npAdapter = new SqlDataAdapter();
                            __npAdapter.SelectCommand = new SqlCommand(__query, __conn);
                            __npAdapter.Fill(__dset);
                            __result = __dset.Tables[0];
                            __conn.Close();
                        }
                        break;
                }
                if (updateGrid)
                {
                    this._resultGrid._clear();
                    if (__connObject.GetType() == typeof(Npgsql.NpgsqlConnection))
                    {
                        Npgsql.NpgsqlConnection __conn = (Npgsql.NpgsqlConnection)__connObject;
                        __conn.Open();
                    }
                    if (__connObject.GetType() == typeof(SqlConnection))
                    {
                        SqlConnection __conn = (SqlConnection)__connObject;
                        __conn.Open();
                    }
                    for (int __row = 0; __row < __result.Rows.Count; __row++)
                    {
                        string __tableName = __result.Rows[__row]["tablename"].ToString();
                        Boolean __found = false;
                        switch (this._transferSelect)
                        {
                            case 0: // SML -> SML
                                __found = true;
                                break;
                            case 1: // BC Account -> SML
                                for (int __loop = 0; __loop < this._scriptArray.Count; __loop++)
                                {
                                    if (this._scriptArray[__loop][0] == '*')
                                    {
                                        string[] __spilt = this._scriptArray[__loop].Remove(0, 1).Trim().Split(',');
                                        if (__spilt[0].Trim().ToUpper().Equals(__tableName.Trim().ToUpper()))
                                        {
                                            __found = true;
                                            break;
                                        }
                                    }
                                }
                                break;
                        }
                        if (__found)
                        {
                            int __addr = this._resultGrid._addRow();
                            this._resultGrid._cellUpdate(__addr, _fieldTableName, __tableName, false);
                            this._resultGrid._cellUpdate(__addr, _fieldTotalRecords, this._countRecord(__connObject, __tableName), false);
                            this._resultGrid._cellUpdate(__addr, this._fieldCheck, 1, false);
                        }
                    }
                    if (__connObject.GetType() == typeof(Npgsql.NpgsqlConnection))
                    {
                        Npgsql.NpgsqlConnection __conn = (Npgsql.NpgsqlConnection)__connObject;
                        __conn.Close();
                    }
                    if (__connObject.GetType() == typeof(SqlConnection))
                    {
                        SqlConnection __conn = (SqlConnection)__connObject;
                        __conn.Close();
                    }
                    this._resultGrid.Invalidate();
                }
                switch (this._transferSelect)
                {
                    case 1: // BC Account -> SML
                        this._processSourceScript(this._scriptSourceEnd);
                        break;
                }
            }
            catch
            {
            }
            return __result;
        }

        void _databaseComboBox_TextChanged(object sender, EventArgs e)
        {
            this._selectDatabaseName = this._databaseComboBox.Text;
            this._getTableNameFromDatabase(9, true);
        }

        int _countRecord(Object conn, string tableName)
        {
            int __result = 0;
            /*if (conn.GetType() == typeof(Npgsql.NpgsqlConnection))
            {
                Npgsql.NpgsqlConnection __conn = (Npgsql.NpgsqlConnection)conn;
                Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand("SELECT count(*) as xcount FROM " + tableName, __conn);
                Npgsql.NpgsqlDataReader __reader = __command.ExecuteReader();
                while (__reader.Read())
                {
                    __result = MyLib._myGlobal._intPhase(__reader[0].ToString());
                }
                __reader.Dispose();
                __command.Dispose();
            }
            if (conn.GetType() == typeof(SqlConnection))
            {
                SqlConnection __conn = (SqlConnection)conn;
                SqlCommand __command = new SqlCommand("SELECT count(*) as xcount FROM " + tableName, __conn);
                SqlDataReader __reader = __command.ExecuteReader();
                while (__reader.Read())
                {
                    __result = MyLib._myGlobal._intPhase(__reader[0].ToString());
                }
                __reader.Dispose();
                __command.Dispose();
            }*/
            return __result;
        }

        string _postgresSqlConnString(string databaseName)
        {
            return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", this._hostNameTextBox.Text, this._hostPortTextBox.Text, this._userNameTextBox.Text, this._userPasswordTextBox.Text, databaseName);
        }

        string __microsoftSqlConnString()
        {
            return String.Format("Server={0};uid={1};pwd={2};", this._hostNameTextBox.Text, this._userNameTextBox.Text, this._userPasswordTextBox.Text);
        }

        string __microsoftSqlConnString(string databaseName)
        {
            return String.Format("Server={0};uid={1};pwd={2};Database={3};", this._hostNameTextBox.Text, this._userNameTextBox.Text, this._userPasswordTextBox.Text, databaseName);
        }

        void _databaseComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                this._databaseComboBox.Items.Clear();
                switch (this._providerSourceSelect)
                {
                    case 0: // PostgresSql
                        {
                            string __connstring = this._postgresSqlConnString("template1");
                            Npgsql.NpgsqlConnection __conn = new Npgsql.NpgsqlConnection(__connstring);
                            __conn.Open();
                            Npgsql.NpgsqlDataAdapter __npAdapter = new Npgsql.NpgsqlDataAdapter();
                            __npAdapter.SelectCommand = new Npgsql.NpgsqlCommand("select datname from pg_database order by datname", __conn);
                            DataSet __dset = new DataSet();
                            __npAdapter.Fill(__dset);
                            DataTable __dtsource = __dset.Tables[0];
                            __conn.Close();
                            //
                            for (int __row = 0; __row < __dtsource.Rows.Count; __row++)
                            {
                                this._databaseComboBox.Items.Add(__dtsource.Rows[__row]["datname"].ToString());
                            }
                        }
                        break;
                    case 2: // Microsoft SQL
                        {
                            SqlConnection __conn = new SqlConnection(this.__microsoftSqlConnString());
                            __conn.Open();
                            SqlCommand __sqlCommand = new SqlCommand();
                            __sqlCommand.Connection = __conn;
                            __sqlCommand.CommandType = CommandType.StoredProcedure;
                            __sqlCommand.CommandText = "sp_databases";
                            SqlDataReader __sqlDR;
                            __sqlDR = __sqlCommand.ExecuteReader();
                            while (__sqlDR.Read())
                            {
                                this._databaseComboBox.Items.Add(__sqlDR.GetString(0));
                            }
                            __sqlDR.Close();
                            __sqlCommand.Dispose();
                            __conn.Close();
                        }
                        break;
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _providerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._providerSourceSelect = this._providerComboBox.SelectedIndex;
            switch (this._providerSourceSelect)
            {
                case 0: // PostgresSql
                    this._hostPortTextBox.Enabled = true;
                    this._hostNameTextBox.Text = "localhost";
                    this._hostPortTextBox.Text = "5432";
                    this._userNameTextBox.Text = "postgres";
                    break;
                case 2: // Microsoft SQL
                    this._hostPortTextBox.Enabled = false;
                    this._hostNameTextBox.Text = "localhost";
                    this._hostPortTextBox.Text = "";
                    this._userNameTextBox.Text = "sa";
                    break;
            }
        }

        Thread _threadWorking;

        DataTable _queryToDataTable(object conn, string query)
        {
            DataTable __dTable = new DataTable();
            if (conn.GetType() == typeof(Npgsql.NpgsqlConnection))
            {
                Npgsql.NpgsqlConnection __conn = (Npgsql.NpgsqlConnection)conn;
                Npgsql.NpgsqlDataAdapter __npAdapter = new Npgsql.NpgsqlDataAdapter();
                __npAdapter.SelectCommand = new Npgsql.NpgsqlCommand(query, __conn);
                __npAdapter.Fill(__dTable);
                __npAdapter.Dispose();
            }
            if (conn.GetType() == typeof(SqlConnection))
            {
                SqlConnection __conn = (SqlConnection)conn;
                SqlDataAdapter __sqlAapter = new SqlDataAdapter();
                __sqlAapter.SelectCommand = new SqlCommand(query, __conn);
                __sqlAapter.Fill(__dTable);
                __sqlAapter.Dispose();
            }
            return __dTable;
        }

        public DateTime _convertDateFromQuery(string date)
        {
            DateTime __result = new DateTime(1979, 3, 25);
            try
            {
                if (date.Length > 0)
                {
                    IFormatProvider __culture = new CultureInfo("en-US");
                    __result = DateTime.Parse(date, __culture);
                }
            }
            catch
            {
                // Debugger.Break();
            }
            return (__result);
        }

        string _selectDatabaseName = "";
        StringBuilder _resultText = new StringBuilder();

        void _processSourceScript(List<string> source)
        {
            switch (this._providerSourceSelect)
            {
                case 0: // PostgresSQL
                    {
                        Npgsql.NpgsqlConnection __postgresConn = new Npgsql.NpgsqlConnection(this._postgresSqlConnString(this._selectDatabaseName));
                        __postgresConn.Open();
                        __postgresConn.Close();
                    }
                    break;
                case 2: // Microsoft SQL
                    {
                        SqlConnection __microsoftSqlConn = new SqlConnection(this.__microsoftSqlConnString(this._selectDatabaseName));
                        __microsoftSqlConn.Open();
                        for (int __loop = 0; __loop < source.Count; __loop++)
                        {
                            try
                            {
                                SqlCommand __command = new SqlCommand(source[__loop].ToString(), __microsoftSqlConn);
                                __command.ExecuteNonQuery();
                            }
                            catch
                            {
                            }
                        }
                        __microsoftSqlConn.Close();
                    }
                    break;
            }
        }

        void _processTargetScript(List<string> source)
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            for (int __loop = 0; __loop < source.Count; __loop++)
            {
                try
                {
                    string __result = __myFrameWork._queryInsertOrUpdate(this._targetDatabaseName, source[__loop].ToString());
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result);
                    }
                }
                catch
                {
                }
            }
        }

        void _process()
        {
            try
            {
                this._targetDatabaseName = this._targetDatabaseNamtTextBox.Text.Trim();
                this._processSourceScript(this._scriptSourceBegin);
                this._processTargetScript(this._scriptTargetBegin);
                for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
                {
                    this._resultGrid._cellUpdate(__row, _fieldTransferSuccess, "", false);
                }
                //
                this._resultText = new StringBuilder();
                MyLib._myGlobal._guid = "SMLX";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                Npgsql.NpgsqlConnection __postgresConn = null;
                SqlConnection __microsoftSqlConn = null;
                switch (this._providerSourceSelect)
                {
                    case 0: // PostgresSQL
                        __postgresConn = new Npgsql.NpgsqlConnection(this._postgresSqlConnString(this._selectDatabaseName));
                        __postgresConn.Open();
                        break;
                    case 2: // Microsoft SQL
                        __microsoftSqlConn = new SqlConnection(this.__microsoftSqlConnString(this._selectDatabaseName));
                        __microsoftSqlConn.Open();
                        break;
                }
                for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
                {
                    if (this._resultGrid._cellGet(__row, this._fieldCheck).ToString().Equals("1"))
                    {
                        Boolean __transferData = true;
                        StringBuilder __query = new StringBuilder();
                        ArrayList __fieldColumnName = new ArrayList();
                        ArrayList __fieldDataType = new ArrayList();
                        ArrayList __fieldDataNull = new ArrayList();
                        ArrayList __fieldDataStringType = new ArrayList();
                        ArrayList __fieldDataByteAType = new ArrayList();
                        string __sourceTableName = this._resultGrid._cellGet(__row, this._fieldTableName).ToString().ToLower();
                        string __targetTableName = "";
                        switch (this._transferSelect)
                        {
                            case 0: // SML -> SML
                                __targetTableName = __sourceTableName;
                                break;
                            case 1: // BC Account -> SML
                                __targetTableName = this._scriptFindTargetTableName(__sourceTableName);
                                break;
                        }
                        //
                        if (this._scriptTargetFieldNameDuplicate(__sourceTableName, __targetTableName) == false)
                        {
                            string __queryGetTargetColumnName = "";
                            string __queryGetColumnName = "";
                            string __queryGetIndex = "";
                            switch (this._providerSourceSelect)
                            {
                                case 0: // PostgresSQL
                                case 2: // Microsoft SQL
                                    __queryGetTargetColumnName = "SELECT * FROM information_schema.columns where upper(table_name)=\'" + __targetTableName.ToUpper() + "\'";
                                    __queryGetColumnName = "SELECT * FROM information_schema.columns where upper(table_name)=\'" + __sourceTableName.ToUpper() + "\'";
                                    __queryGetIndex = "SELECT * FROM information_schema.constraint_column_usage where upper(table_name)=\'" + __sourceTableName.ToUpper() + "\'";
                                    break;
                            }
                            // ดึง field name เพื่อเปรียบเทียบ
                            DataTable __targetField = __myFrameWork._query(this._targetDatabaseName, __queryGetTargetColumnName).Tables[0];
                            //
                            DataTable __tableList = null;
                            DataTable __keyList = null;
                            //
                            switch (this._providerSourceSelect)
                            {
                                case 0: // PostgresSQL
                                    __tableList = this._queryToDataTable(__postgresConn, __queryGetColumnName);
                                    __keyList = this._queryToDataTable(__postgresConn, __queryGetIndex);
                                    break;
                                case 2: // Microsoft SQL
                                    __tableList = this._queryToDataTable(__microsoftSqlConn, __queryGetColumnName);
                                    __keyList = this._queryToDataTable(__microsoftSqlConn, __queryGetIndex);
                                    break;
                            }
                            //
                            for (int __fieldRow = 0; __fieldRow < __tableList.Rows.Count; __fieldRow++)
                            {
                                if (__query.Length > 0)
                                {
                                    __query.Append(",\n");
                                }
                                DataRow __dataRow = __tableList.Rows[__fieldRow];
                                string __columnName = __dataRow["column_name"].ToString();
                                // ค้นหาว่า field ตรงกันหรือเปล่า
                                Boolean __fieldTargetFound = false;
                                for (int __findField = 0; __findField < __targetField.Rows.Count; __findField++)
                                {
                                    string __fieldName = "";
                                    switch (this._transferSelect)
                                    {
                                        case 0: // SML -> SML
                                            __fieldName = __targetField.Rows[__findField]["column_name"].ToString().ToUpper().Trim();
                                            break;
                                        case 1: // BC Account -> SML
                                            __fieldName = this._scriptFindSourceField(__sourceTableName, __columnName);
                                            if (__fieldName.Length > 0)
                                            {
                                                try
                                                {
                                                    __fieldName = __targetField.Rows[__findField][__fieldName].ToString().ToUpper().Trim();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            break;
                                    }
                                    if (__columnName.Trim().ToUpper().Equals(__fieldName))
                                    {
                                        __fieldTargetFound = true;
                                        break;
                                    }
                                }
                                if (__fieldTargetFound)
                                {
                                    if (this._ignoreFieldTextBox.Text.ToLower().IndexOf("[" + __columnName.ToLower() + "]") == -1)
                                    {
                                        string __columnDefault = __dataRow["column_default"].ToString();
                                        Boolean __isNull = __dataRow["is_nullable"].ToString().ToUpper().Equals("YES");
                                        string __dataType = __dataRow["data_type"].ToString();
                                        Boolean __stringCase = false;
                                        Boolean __byteACase = false;
                                        int __charMaxLength = _convertToInt(__dataRow["character_maximum_length"].ToString());
                                        int __numericPrecision = _convertToInt(__dataRow["numeric_precision"].ToString());
                                        int __numericPrecisionRadix = _convertToInt(__dataRow["numeric_precision_radix"].ToString());
                                        //
                                        __query.Append(__columnName + " ");
                                        if (__columnDefault.IndexOf("nextval") != -1)
                                        {
                                            __query.Append(" serial ");
                                        }
                                        else
                                        {
                                            if (__dataType.Equals("integer"))
                                            {
                                                __query.Append(__dataType);
                                            }
                                            else
                                            {
                                                if (__dataType.Equals("bytea") || __dataType.Equals("image"))
                                                {
                                                    __query.Append(__dataType);
                                                    if (__charMaxLength > 0)
                                                    {
                                                        __query.Append("(" + __charMaxLength.ToString() + ")");
                                                    }
                                                    __byteACase = true;
                                                }
                                                else
                                                    if (__dataType.Equals("character varying") || __dataType.Equals("varchar") || __dataType.Equals("character") || __dataType.Equals("char"))
                                                    {
                                                        __query.Append(__dataType);
                                                        if (__charMaxLength > 0)
                                                        {
                                                            __query.Append("(" + __charMaxLength.ToString() + ")");
                                                        }
                                                        __stringCase = true;
                                                    }
                                                    else
                                                        if (__dataType.Equals("date") || __dataType.Equals("datetime") || __dataType.Equals("smalldatetime"))
                                                        {
                                                            __query.Append(__dataType);
                                                            __stringCase = true;
                                                        }
                                                        else
                                                            if (__dataType.Equals("timestamp without time zone"))
                                                            {
                                                                __query.Append(__dataType);
                                                                __stringCase = true;
                                                            }
                                                            else
                                                                if (__dataType.Equals("double precision"))
                                                                {
                                                                    __query.Append(__dataType);
                                                                }
                                                                else
                                                                    if (__dataType.Equals("smallint"))
                                                                    {
                                                                        __query.Append(__dataType);
                                                                    }
                                                                    else
                                                                    {
                                                                        __query.Append(__dataType);
                                                                    }
                                            }
                                            __fieldColumnName.Add(__columnName);
                                            __fieldDataStringType.Add(__stringCase);
                                            __fieldDataByteAType.Add(__byteACase);
                                            __fieldDataType.Add(__dataType);
                                            __fieldDataNull.Add(__isNull);
                                        }
                                        //
                                        if (__isNull == false)
                                        {
                                            __query.Append(" NOT NULL ");
                                        }
                                    }
                                }
                            }
                            if (__keyList.Rows.Count > 0)
                            {
                                __query.Append(",\n");
                                StringBuilder __indexField = new StringBuilder();
                                for (int __loop = 0; __loop < __keyList.Rows.Count; __loop++)
                                {
                                    if (__indexField.Length > 0)
                                    {
                                        __indexField.Append(",");
                                    }
                                    __indexField.Append(__keyList.Rows[__loop]["column_name"].ToString());
                                }
                                __query.Append("CONSTRAINT " + __keyList.Rows[0]["constraint_name"].ToString() + " PRIMARY KEY (" + __indexField.ToString() + ")\n");
                            }
                            __query.Append(")\n");
                            if (this._createRadioButton.Checked)
                            {
                                // Create
                                __query.Append("CREATE TABLE " + __targetTableName + " (\n");
                                string __result = __myFrameWork._queryInsertOrUpdate(this._targetDatabaseName, __query.ToString().Replace("\n", " "));
                                if (__result.Length > 0)
                                {
                                    this._resultText.Append("Create : " + __targetTableName + " fail.\r\n");
                                    __transferData = false;
                                }
                            }
                            else
                            {
                                if (this._truncateRadioButton.Checked)
                                {
                                    // Truncate
                                    string __result = __myFrameWork._queryInsertOrUpdate(this._targetDatabaseName, "truncate table " + __targetTableName);
                                    if (__result.Length > 0)
                                    {
                                        this._resultText.Append("Truncate : " + __targetTableName + " fail.\r\n");
                                        __transferData = false;
                                    }
                                }
                            }
                            this._resultGrid._cellUpdate(__row, _fieldTransferSuccess, (__transferData) ? "Transfer" : "Skip", false);
                            this._resultGrid._cellUpdate(__row, this._fieldCheck, 0, false);
                            this._resultGrid.Invalidate();
                            if (__transferData)
                            {
                                // Insert
                                ArrayList __fieldSourceName = new ArrayList();
                                ArrayList __fieldTargetName = new ArrayList();
                                StringBuilder __dataSourceField = new StringBuilder();
                                StringBuilder __dataTargetField = new StringBuilder();
                                switch (this._transferSelect)
                                {
                                    case 0: // SML -> SML
                                        {
                                            for (int __fieldScan = 0; __fieldScan < __fieldColumnName.Count; __fieldScan++)
                                            {
                                                if (__dataSourceField.Length != 0)
                                                {
                                                    __dataSourceField.Append(",");
                                                }
                                                __dataSourceField.Append(__fieldColumnName[__fieldScan].ToString());
                                                //
                                                if (__dataTargetField.Length != 0)
                                                {
                                                    __dataTargetField.Append(",");
                                                }
                                                __dataTargetField.Append(__fieldColumnName[__fieldScan].ToString());
                                                //
                                                __fieldSourceName.Add(__fieldColumnName[__fieldScan].ToString());
                                                __fieldTargetName.Add(__fieldColumnName[__fieldScan].ToString());
                                            }
                                        }
                                        break;
                                    case 1: // BC Account -> SML
                                        {
                                            int __addr = this._scriptFindSourceTableNameAddr(__sourceTableName) + 1;
                                            while (__addr < this._scriptArray.Count)
                                            {
                                                if (this._scriptArray[__addr][0] == '*' || this._scriptArray[__addr][0] == '!' || this._scriptArray[__addr][0] == '-')
                                                {
                                                    break;
                                                }
                                                string[] __split = this._scriptArray[__addr].Split(',');
                                                if (__dataSourceField.Length != 0)
                                                {
                                                    __dataSourceField.Append(",");
                                                }
                                                __dataSourceField.Append(__split[0].Trim().ToString());
                                                //
                                                if (__dataTargetField.Length != 0)
                                                {
                                                    __dataTargetField.Append(",");
                                                }
                                                __dataTargetField.Append(__split[1].Trim().ToString());
                                                //
                                                __fieldSourceName.Add(__split[0].Trim().ToString());
                                                __fieldTargetName.Add(__split[1].Trim().ToString());
                                                __addr++;
                                            }
                                        }
                                        break;
                                }
                                if (__dataSourceField.Length > 0)
                                {
                                    string __sqlCommand = "select " + __dataSourceField.ToString() + " from " + __sourceTableName + ((this._extraWhereTextbox.Text.Trim().Length > 0) ? " where " + this._extraWhereTextbox.Text : "");
                                    StringBuilder __queryInsert = new StringBuilder();
                                    int __count = 0;
                                    int __countRecord = 0;
                                    Npgsql.NpgsqlCommand __postgresCommandRead = null;
                                    Npgsql.NpgsqlDataReader __postgresDataReader = null;
                                    SqlCommand __microsoftSqlCommandRead = null;
                                    SqlDataReader __microsoftSqlDataReader = null;
                                    switch (this._providerSourceSelect)
                                    {
                                        case 0: // PostgresSQL
                                            __postgresCommandRead = new Npgsql.NpgsqlCommand(__sqlCommand, __postgresConn);
                                            __postgresDataReader = __postgresCommandRead.ExecuteReader();
                                            break;
                                        case 2: // Microsoft SQL
                                            __microsoftSqlCommandRead = new SqlCommand(__sqlCommand, __microsoftSqlConn);
                                            __microsoftSqlDataReader = __microsoftSqlCommandRead.ExecuteReader();
                                            break;
                                    }
                                    while (true)
                                    {
                                        Boolean __read = false;
                                        switch (this._providerSourceSelect)
                                        {
                                            case 0: // PostgresSQL
                                                __read = __postgresDataReader.Read();
                                                break;
                                            case 2: // Microsoft SQL
                                                __read = __microsoftSqlDataReader.Read();
                                                break;
                                        }
                                        if (__read == false)
                                        {
                                            break;
                                        }
                                        __countRecord++;
                                        this._resultGrid._cellUpdate(__row, _fieldTransferSuccess, (__countRecord == 0) ? "0" : __countRecord.ToString(), false);
                                        if (__queryInsert.Length == 0)
                                        {
                                            __queryInsert.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                        }
                                        StringBuilder __data = new StringBuilder();
                                        for (int __fieldScan = 0; __fieldScan < __fieldSourceName.Count; __fieldScan++)
                                        {
                                            if (__data.Length != 0)
                                            {
                                                __data.Append(",");
                                            }
                                            int __findTypeAddr = __fieldScan;
                                            for (int __loop = 0; __loop < __fieldColumnName.Count; __loop++)
                                            {
                                                if (__fieldColumnName[__loop].ToString().Trim().ToUpper().Equals(__fieldSourceName[__fieldScan].ToString().ToUpper().Trim()))
                                                {
                                                    __findTypeAddr = __loop;
                                                    break;
                                                }
                                            }
                                            string __getData = "";
                                            byte[] __getByte = null;
                                            switch (this._providerSourceSelect)
                                            {
                                                case 0: // PostgresSQL
                                                    if ((Boolean)__fieldDataByteAType[__findTypeAddr])
                                                    {
                                                        try
                                                        {
                                                            __getByte = (byte[])__postgresDataReader.GetValue(__fieldScan);
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                        __getData = __postgresDataReader[__fieldScan].ToString().Replace("\'", "\'\'");
                                                    }
                                                    break;
                                                case 2: // Microsoft SQL
                                                    if ((Boolean)__fieldDataByteAType[__findTypeAddr])
                                                    {
                                                        __getByte = (byte[])__microsoftSqlDataReader.GetValue(__fieldScan);
                                                    }
                                                    else
                                                    {
                                                        __getData = __microsoftSqlDataReader[__fieldScan].ToString().Replace("\'", "\'\'");
                                                    }
                                                    break;
                                            }
                                            if ((Boolean)__fieldDataNull[__findTypeAddr] && (Boolean)__fieldDataStringType[__findTypeAddr] && __getData.ToString().Trim().Length == 0)
                                            {
                                                // กรณีวันที่
                                                if (__fieldDataType[__findTypeAddr].ToString().Equals("date") || __fieldDataType[__findTypeAddr].ToString().Equals("datetime") || __fieldDataType[__findTypeAddr].ToString().Equals("timestamp without time zone") || __fieldDataType[__findTypeAddr].ToString().Equals("smalldatetime"))
                                                {
                                                    __data.Append("null");
                                                }
                                                else
                                                {
                                                    __data.Append("\'\'");
                                                }
                                            }
                                            else
                                            {
                                                if ((Boolean)__fieldDataStringType[__findTypeAddr])
                                                {
                                                    __data.Append("\'");
                                                }
                                                // กรณีวันที่
                                                if (__fieldDataType[__findTypeAddr].ToString().Equals("date") || __fieldDataType[__findTypeAddr].ToString().Equals("datetime") || __fieldDataType[__findTypeAddr].ToString().Equals("timestamp without time zone") || __fieldDataType[__findTypeAddr].ToString().Equals("smalldatetime"))
                                                {
                                                    switch (this._providerSourceSelect)
                                                    {
                                                        case 0: // PostgresSQL

                                                            __getData = ((DateTime)__postgresDataReader[__fieldScan]).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                                                            //.ToString().Replace("\'", "\'\'");

                                                            break;
                                                        case 2: // Microsoft SQL

                                                            __getData = ((DateTime)__microsoftSqlDataReader[__fieldScan]).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));

                                                            break;
                                                    }

                                                    __getData = MyLib._myGlobal._convertDateToQuery(this._convertDateFromQuery(__getData));
                                                }
                                                else
                                                {
                                                    // กรณีเป็นตัวเลข ให้เป็น 0
                                                    if ((Boolean)__fieldDataStringType[__findTypeAddr] == false && __getData.Length == 0)
                                                    {
                                                        __getData = "0";
                                                    }
                                                }
                                                if ((Boolean)__fieldDataByteAType[__findTypeAddr])
                                                {
                                                    if (__getByte != null)
                                                    {
                                                        __getData = "decode('" + Convert.ToBase64String(__getByte) + "','base64')";
                                                    }
                                                }
                                                __data.Append(__getData);
                                                if ((Boolean)__fieldDataStringType[__findTypeAddr])
                                                {
                                                    __data.Append("\'");
                                                }
                                            }
                                        }
                                        __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + __targetTableName + " (" + __dataTargetField.ToString() + ") values (" + __data.ToString() + ")"));
                                        if (++__count == 6000 || __queryInsert.Length > 100000)
                                        {
                                            __queryInsert.Append("</node>");
                                            string __resultInsert = "";
                                            try
                                            {
                                                __resultInsert = __myFrameWork._queryList(this._targetDatabaseName, MyLib._myGlobal._deleteAscError(__queryInsert.ToString()));
                                            }
                                            catch (Exception __ex)
                                            {
                                                MessageBox.Show(__ex.Message.ToString() + __queryInsert.ToString());
                                            }
                                            if (__resultInsert.Length > 0)
                                            {
                                                this._resultText.Append(__resultInsert + "\r\n");
                                                break;
                                            }
                                            __queryInsert = new StringBuilder();
                                            __count = 0;
                                        }
                                    }
                                    if (__queryInsert.Length > 0)
                                    {
                                        __queryInsert.Append("</node>");
                                        string __resultInsert = "";
                                        try
                                        {
                                            __resultInsert = __myFrameWork._queryList(this._targetDatabaseName, MyLib._myGlobal._deleteAscError(__queryInsert.ToString()));
                                        }
                                        catch (Exception __ex)
                                        {
                                            MessageBox.Show(__ex.Message.ToString() + __queryInsert.ToString());
                                        }
                                        if (__resultInsert.Length > 0)
                                        {
                                            this._resultText.Append(__resultInsert + "\r\n");
                                            break;
                                        }
                                    }
                                    switch (this._providerSourceSelect)
                                    {
                                        case 0: // PostgresSQL
                                            __postgresDataReader.Close();
                                            __postgresCommandRead.Dispose();
                                            break;
                                        case 2: // Microsoft SQL
                                            __microsoftSqlDataReader.Close();
                                            __microsoftSqlCommandRead.Dispose();
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                switch (this._providerSourceSelect)
                {
                    case 0: // PostgresSQL
                        __postgresConn.Close();
                        break;
                    case 2: // Microsoft SQL
                        __microsoftSqlConn.Close();
                        break;
                }
                //
                this._processSourceScript(this._scriptSourceEnd);
                this._processTargetScript(this._scriptTargetEnd);
                //
                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        int _convertToInt(string source)
        {
            return (source.Length == 0) ? 0 : MyLib._myGlobal._intPhase(source);
        }

        private List<string> _cutCommandScript(string source)
        {
            List<string> __result = new List<string>();
            StringBuilder __buffer = new StringBuilder();
            for (int __loop = 0; __loop < source.Length; __loop++)
            {
                if (source[__loop] == '[')
                {
                    __buffer = new StringBuilder();
                }
                else
                    if (source[__loop] == ']')
                    {
                        __result.Add(__buffer.ToString());
                    }
                    else
                    {
                        __buffer.Append(source[__loop]);
                    }
            }
            return __result;
        }

        private void _loadScript(string fileName)
        {
            this._scriptArray.Clear();
            StreamReader __re = File.OpenText(fileName);
            Boolean __beginSourceWork = false;
            Boolean __endSourceWork = false;
            Boolean __beginTargetWork = false;
            Boolean __endTargetWork = false;
            string __input = null;
            StringBuilder __beginSourceString = new StringBuilder();
            StringBuilder __endSourceString = new StringBuilder();
            StringBuilder __beginTargetString = new StringBuilder();
            StringBuilder __endTargetString = new StringBuilder();
            while ((__input = __re.ReadLine()) != null)
            {
                string __command = __input.Trim().ToLower();
                switch (__command)
                {
                    case "*begin":
                        __beginSourceWork = (__beginSourceWork) ? false : true;
                        break;
                    case "*end":
                        __endSourceWork = (__endSourceWork) ? false : true;
                        break;
                    case "*begin_target":
                        __beginTargetWork = (__beginTargetWork) ? false : true;
                        break;
                    case "*end_target":
                        __endTargetWork = (__endTargetWork) ? false : true;
                        break;
                    default:
                        if (__command.Length > 0)
                        {
                            if (__beginSourceWork)
                            {
                                __beginSourceString.Append(__command + " ");
                            }
                            else
                                if (__endSourceWork)
                                {
                                    __endSourceString.Append(__command + " ");
                                }
                                else
                                    if (__beginTargetWork)
                                    {
                                        __beginTargetString.Append(__command + " ");
                                    }
                                    else
                                        if (__endTargetWork)
                                        {
                                            __endTargetString.Append(__command + " ");
                                        }
                                        else
                                        {
                                            this._scriptArray.Add(__input.Trim());
                                        }
                            {
                            }
                        }
                        break;
                }
            }
            // ตัดคำสั่ง *bgein,*end
            this._scriptSourceBegin = this._cutCommandScript(__beginSourceString.ToString());
            this._scriptSourceEnd = this._cutCommandScript(__endSourceString.ToString());
            this._scriptTargetBegin = this._cutCommandScript(__beginTargetString.ToString());
            this._scriptTargetEnd = this._cutCommandScript(__endTargetString.ToString());
            __re.Close();
        }

        private void _truncateAllTable()
        {
            this._startButton.Enabled = false;
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            DataTable __targetTableList = this._getTableNameFromDatabase(0, false);
            for (int __row = 0; __row < __targetTableList.Rows.Count; __row++)
            {
                string __targetTableName = __targetTableList.Rows[__row][0].ToString();
                __myFrameWork._queryInsertOrUpdate(this._targetDatabaseName, "truncate table " + __targetTableName);
                _resultTextBox.Text = "Truncate Table : " + __targetTableName;
                _resultTextBox.Refresh();
                Application.DoEvents();
            }
            _resultTextBox.Text = "";
            MessageBox.Show("Truncate all table success.");
            this._startButton.Enabled = true;
        }

        private void _startButton_Click(object sender, EventArgs e)
        {
            if (this._databaseComboBox.SelectedItem.ToString().ToUpper().Trim().Equals(this._targetDatabaseNamtTextBox.Text.ToUpper().Trim()))
            {
                MessageBox.Show("Can't use same database name. : " + this._databaseComboBox.SelectedItem.ToString());
            }
            else
            {
                this._targetDatabaseName = this._targetDatabaseNamtTextBox.Text.Trim();
                if (this._transferSelect == 1)
                {
                    this._loadScript(this._scriptFileName);
                }
                if (this._targetDatabaseNamtTextBox.Text.Trim().Length > 0)
                {
                    //
                    _resultTextBox.Text = "Start Transfer";
                    _resultTextBox.Refresh();
                    Application.DoEvents();
                    //
                    this._selectDatabaseName = this._databaseComboBox.Text;
                    this._timer.Enabled = true;
                    this._timer.Start();
                    this._threadWorking = new Thread(this._process);
                    this._threadWorking.Start();
                    //
                    this._startButton.Enabled = false;
                    this._stopButton.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Input Database Name");
                }
            }
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            if (this._threadWorking != null)
            {
                this._threadWorking.Abort();
            }
            this._timer.Stop();
            this._timer.Enabled = false;
            this._startButton.Enabled = true;
            this._stopButton.Enabled = false;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (this._resultText.ToString().Equals(this._resultTextBox.Text) == false)
            {
                this._resultTextBox.Text = this._resultText.ToString();
            }
            this._resultGrid.Invalidate();
            if (this._threadWorking.IsAlive == false)
            {
                this._startButton.Enabled = true;
                this._stopButton.Enabled = false;
            }
        }

        private void _deSelectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
            {
                this._resultGrid._cellUpdate(__row, this._fieldCheck, 0, false);
            }
            this._resultGrid.Invalidate();
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._resultGrid._rowData.Count; __row++)
            {
                this._resultGrid._cellUpdate(__row, this._fieldCheck, 1, false);
            }
            this._resultGrid.Invalidate();
        }

        private void _transferModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._transferSelect = this._transferModeComboBox.SelectedIndex;
            switch (this._transferSelect)
            {
                case 0: // SML -> SML
                    this._createRadioButton.Enabled = true;
                    this._truncateRadioButton.Enabled = true;
                    this._ignoreFieldTextBox.Enabled = true;
                    this._loadScriptButton.Enabled = false;
                    break;
                case 1: // BC Account -> SML
                    this._createRadioButton.Enabled = false;
                    this._truncateRadioButton.Enabled = false;
                    this._ignoreFieldTextBox.Enabled = false;
                    this._loadScriptButton.Enabled = true;
                    this._createRadioButton.Checked = false;
                    this._truncateRadioButton.Checked = true;
                    break;
            }
            this._getTableNameFromDatabase(9, true);
        }

        private string _scriptFindSourceField(string sourceTableName, string sourceFieldName)
        {
            int __tableAddr = this._scriptFindSourceTableNameAddr(sourceTableName);
            if (__tableAddr != -1)
            {
                for (int __loop = __tableAddr + 1; __loop < this._scriptArray.Count; __loop++)
                {
                    if (this._scriptArray[__loop][0] == '*')
                    {
                        break;
                    }
                    else
                    {
                        string[] __spilt = this._scriptArray[__loop].Trim().Split(',');
                        if (__spilt[0].Trim().ToUpper().Equals(sourceFieldName.Trim().ToUpper()))
                        {
                            return __spilt[0].ToUpper();
                        }
                    }
                }
            }
            return "";
        }

        private int _scriptFindSourceTableNameAddr(string tableName)
        {
            for (int __loop = 0; __loop < this._scriptArray.Count; __loop++)
            {
                if (this._scriptArray[__loop].Length > 0)
                {
                    if (this._scriptArray[__loop][0] == '*')
                    {
                        string[] __spilt = this._scriptArray[__loop].Remove(0, 1).Split(',');
                        string __tableName = __spilt[0].ToString().Trim();
                        if (tableName.ToUpper().Equals(__tableName.ToUpper()))
                        {
                            return __loop;
                        }
                    }
                }
            }
            return -1;
        }

        private int _scriptFindTargetTableNameAddr(string tableName)
        {
            for (int __loop = 0; __loop < this._scriptArray.Count; __loop++)
            {
                if (this._scriptArray[__loop].Length > 0)
                {
                    if (this._scriptArray[__loop][0] == '*')
                    {
                        string[] __spilt = this._scriptArray[__loop].Remove(0, 1).Split(',');
                        string __tableName = __spilt[1].ToString().Trim();
                        if (tableName.ToUpper().Equals(__tableName.ToUpper()))
                        {
                            return __loop;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// ค้นหาชื่อ Table
        /// </summary>
        /// <returns></returns>
        private string _scriptFindTargetTableName(string tableName)
        {
            int __addr = this._scriptFindSourceTableNameAddr(tableName);
            if (__addr != -1)
            {
                string[] __spilt = this._scriptArray[__addr].Remove(0, 1).Split(',');
                return __spilt[1].ToString().Trim().ToUpper();
            }
            return "";
        }

        private Boolean _scriptTargetFieldNameDuplicate(string sourceTableName, string targetTableName)
        {
            {
                List<string> __field = this._scriptGetFieldName(sourceTableName, 0);
                for (int __loop1 = 0; __loop1 < __field.Count; __loop1++)
                {
                    for (int __loop2 = 0; __loop2 < __field.Count; __loop2++)
                    {
                        if (__loop1 != __loop2)
                        {
                            if (__field[__loop1].Equals(__field[__loop2]))
                            {
                                MessageBox.Show("Source Field Duplicate : " + sourceTableName + "." + __field[__loop1].ToString());
                                return true;
                            }
                        }
                    }
                }
            }
            {
                List<string> __field = this._scriptGetFieldName(targetTableName, 1);
                for (int __loop1 = 0; __loop1 < __field.Count; __loop1++)
                {
                    for (int __loop2 = 0; __loop2 < __field.Count; __loop2++)
                    {
                        if (__loop1 != __loop2)
                        {
                            if (__field[__loop1].Equals(__field[__loop2]))
                            {
                                MessageBox.Show("Target Field Duplicate : " + targetTableName + "." + __field[__loop1].ToString());
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private List<string> _scriptGetFieldName(string tableName, int columnNumber)
        {
            List<string> __result = new List<string>();
            int __addr = (columnNumber == 0) ? this._scriptFindSourceTableNameAddr(tableName) : this._scriptFindTargetTableNameAddr(tableName);
            if (__addr != -1)
            {
                for (int __loop = __addr + 1; __loop < this._scriptArray.Count; __loop++)
                {
                    if (this._scriptArray[__loop][0] == '*' || this._scriptArray[__loop][0] == '!' || this._scriptArray[__loop][0] == '-')
                    {
                        break;
                    }
                    else
                    {
                        string[] __split = this._scriptArray[__loop].Split(',');
                        __result.Add(__split[columnNumber].ToString().Trim());
                    }
                }
            }
            return __result;
        }

        private void _loadScriptButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog __ofn = new OpenFileDialog();
                __ofn.Filter = "Text File|*.txt;*.sql";
                __ofn.Title = "Text File";
                if (__ofn.ShowDialog() == DialogResult.OK)
                {
                    this._scriptTextBox.Text = __ofn.FileName;
                    this._scriptTextBox.Invalidate();
                    this._scriptFileName = __ofn.FileName;
                    this._loadScript(this._scriptFileName);
                    this._getTableNameFromDatabase(9, true);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _truncateAllTableButton_Click(object sender, EventArgs e)
        {
            DialogResult __truncate1 = MessageBox.Show(this._targetDatabaseNamtTextBox.Text + " : Truncate All Table ?", "Yes/No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (__truncate1 == System.Windows.Forms.DialogResult.Yes)
            {
                DialogResult __truncate2 = MessageBox.Show(this._targetDatabaseNamtTextBox.Text + " : Again. Truncate All Table ?", "Yes/No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (__truncate2 == System.Windows.Forms.DialogResult.Yes)
                {
                    this._truncateAllTable();
                }
            }
        }
    }
}
