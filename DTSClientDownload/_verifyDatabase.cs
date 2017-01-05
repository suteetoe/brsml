using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.Common;

namespace DTSClientDownload
{

    public class _verifyDatabase
    {
        private int _databaseID = 0;
        private string _host = "";
        private string _dbUser = "";
        private string _dbPass = "";
        private string _dbName = "";
        private bool _isNewTable = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="dbUser"></param>
        /// <param name="dbPass"></param>
        /// <param name="dbName"></param>
        /// <param name="databaseType">1=Postgresql,2=mysql,3=sql server 2000,4=sql server 2500</param>
        public _verifyDatabase(String host, String dbUser, String dbPass, String dbName, int databaseType)
        {
            this._host = host;
            this._dbUser = dbUser;
            this._dbPass = dbPass;
            this._dbName = dbName;
            this._databaseID = databaseType;
        }

        private Boolean _testConnect()
        {
            if (_global._champServer.Length == 0)
                return false;

            SqlConnection __conn = new SqlConnection("Server=" + this._host + ";Database=" + this._dbName + ";uid=" + this._dbUser + ";Password=" + this._dbPass + ";Connect Timeout=3000");
            //__conn.ConnectionTimeout = 300;
            try
            {
                __conn.Open();
                __conn.Close();
                return true;
            }
            catch
            {
            }
            return false;
        }

        private DbConnection _connect()
        {
            if (this._host != "")
            {
                switch (this._databaseID)
                {
                    case 3:
                    case 4:

                        if (_testConnect())
                        {
                            return new SqlConnection("Server=" + this._host + ";Database=" + this._dbName + ";uid=" + this._dbUser + ";Password=" + this._dbPass + ";Connect Timeout=3000");
                        }
                        break;
                }
            }

            return null;
        }

        public String _verifyTable(String databaseName, String tableName, XmlNodeList readerField)
        {
            String __result = "";
            Boolean __createTable = true;
            this._isNewTable = false;

            if (_isTableExists(tableName) == true)
            {
                __createTable = false;
            }
            else
            {
                __createTable = true;
            }

            // ตรวจสอบ table พร้อม field และทำตามขบวนการสร้าง, แก้ไข, เพิ่ม
            if (__createTable)
            {
                // ไม่พบ และทำการสร้าง table
                __result = _createTable(databaseName, tableName, readerField);
                _isNewTable = true;
            }
            else
            {
                // ถ้าพบ ให้ตรวจสอบ Field ต่อไป
                __result = _verifyField(databaseName, tableName, readerField);
            }
            return __result;
        }

        private String _verifyField(String databaseName, String tableName, XmlNodeList readerField)
        {
            StringBuilder __createQuery = new StringBuilder();
            String __result = "";
            try
            {

                for (int __field = 0; __field < readerField.Count; __field++)
                {
                    __createQuery = new StringBuilder();
                    /*Element __fieldElement = (__field ==-1) ? null : (Element) readerField.item(__field);
                    String __getRealFieldName = __fieldElement.getAttribute("name").toLowerCase();
                    String __getType = _fieldTypeName(__fieldElement.getAttribute("type"));
                    String __getLength = __fieldElement.getAttribute("length").toLowerCase();
                    String __getAllowNulls = __fieldElement.getAttribute("allow_null").toLowerCase();
                    String __getIndentity = __fieldElement.getAttribute("indentity").toLowerCase();
                    boolean __getResourceOnly = (__fieldElement.getAttribute("resource_only").toLowerCase().compareTo("true") == 0) ? true : false;*/
                    XmlElement __fieldElement = (__field == -1 || __field == -2) ? null : (XmlElement)readerField.Item(__field);
                    String __getRealFieldName = (__field == -1) ? "roworder" : ((__field == -2) ? "is_lock_record" : __fieldElement.GetAttribute("name").ToLower().Trim());
                    String __getTypeReal = (__field == -1 || __field == -2) ? "int" : __fieldElement.GetAttribute("type").ToLower().Trim();
                    String __getType = (__field == -1 || __field == -2) ? "int" : _fieldTypeName(__getTypeReal);
                    String __getLength = (__field == -1 || __field == -2) ? "0" : __fieldElement.GetAttribute("length").ToLower().Trim();
                    String __getIndentity = (__field == -1) ? "yes" : ((__field == -2) ? "no" : __fieldElement.GetAttribute("indentity").ToLower().Trim());
                    String __getAllowNulls = (__field == -1) ? "false" : ((__field == -2) ? "true" : __fieldElement.GetAttribute("allow_null").ToLower().Trim());
                    Boolean __getResourceOnly = (__field == -1 || __field == -2) ? false : ((__fieldElement.GetAttribute("resource_only").ToLower().CompareTo("true") == 0) ? true : false);
                    if (__getResourceOnly == false)
                    {
                        if (__getTypeReal.CompareTo("int") == 0)
                        {
                            __getLength = "10";
                            __getType = "int";
                            // _getType = "int identity";
                        }
                        if (__getAllowNulls == null)
                        {
                            __getAllowNulls = "true";
                        }
                        if (__getAllowNulls.CompareTo("false") == 0)
                        {
                            __getAllowNulls = " not null default ''";
                        }
                        if (__getAllowNulls.CompareTo("true") == 0)
                        {
                            __getAllowNulls = " null ";
                        }
                        if (__getRealFieldName.Length > 0)
                        {
                            // 0=มี Field,1=ไม่มี Field,2=ให้แก้ไขความกว้าง,3=type ผิด
                            int __fieldStatus = _findField(tableName, __getRealFieldName, __getType, int.Parse(__getLength));
                            // สร้าง type
                            String __createType = __getType;
                            if (__getTypeReal.CompareTo("varchar") == 0)
                            {
                                __createType = __getType + " (" + __getLength + ")";
                            }
                            switch (__fieldStatus)
                            {
                                case 1:// ไม่มี field ให้สร้างใหม่เลย
                                    {
                                        // UNIQUE";
                                        if (__getIndentity != null)
                                        {
                                            // กรณีพิเศษ เป็นแบบ Autorun
                                            if (__getIndentity.ToLower().CompareTo("yes") == 0)
                                            {
                                                switch (this._databaseID)
                                                {
                                                    case 1:
                                                        // PostgreSql
                                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName); // + " CONSTRAINT exb_unique
                                                        __createQuery.Append(" serial ");
                                                        break;
                                                    case 2:
                                                        //MySQL
                                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName).Append(" ").Append(__createType).Append(" ").Append(__getAllowNulls); // + " CONSTRAINT exb_unique
                                                        __createQuery.Append(" AUTO_INCREMENT PRIMARY key ");//somruk
                                                        __getAllowNulls = "false";
                                                        break;
                                                    case 3:
                                                    case 4:
                                                        // Microsoft SQL
                                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName).Append(" ").Append(__createType).Append(" ").Append(__getAllowNulls); // + " CONSTRAINT exb_unique
                                                        __createQuery.Append(" IDENTITY (1,1) ");
                                                        break;
                                                }
                                            }
                                        }
                                        if (__createQuery.ToString().Trim().Length == 0)
                                        {
                                            __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName).Append(" ").Append(__createType).Append(" ").Append(__getAllowNulls); // + " CONSTRAINT exb_unique
                                        }
                                    }
                                    break;
                                case 2: //ความยาวไม่ตรง
                                    {
                                        switch (this._databaseID)
                                        {
                                            case 1:
                                                // PostgreSql
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER ").Append(__getRealFieldName).Append(" type ").Append(__createType);
                                                break;
                                            case 2:
                                                //MySQL
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" modify ").Append(__getRealFieldName).Append(__createType);
                                                break;
                                            case 3:
                                            case 4:
                                                // Microsoft SQL
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append("  ").Append(__createType);
                                                break;
                                        }
                                    }
                                    break;
                                case 3: //Type
                                    {
                                        switch (this._databaseID)
                                        {
                                            case 1:
                                                // PostgreSql
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER ").Append(__getRealFieldName).Append(" type ").Append(__createType);
                                                break;
                                            case 2:
                                                //MySQL
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" modify ").Append(__getRealFieldName).Append(" ").Append(__createType);
                                                break;
                                            case 3:
                                            case 4:
                                                // Microsoft SQL
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append(" ").Append(__createType);
                                                break;
                                        }
                                    }
                                    break;
                            }

                            //try
                            //{
                            //    // Get a Statement object
                            //    String __queryStr = __createQuery.ToString();
                            //    if (__queryStr.Length > 0)
                            //    {
                            //        Statement __stmt = __con.createStatement();
                            //        __stmt.executeUpdate(__queryStr);
                            //        __stmt.close();
                            //    }
                            //}
                            //catch (Exception __ex)
                            //{
                            //    __result = __ex.Message + ":" + __createQuery;
                            //}
                            try
                            {
                                String __queryStr = __createQuery.ToString();
                                if (__queryStr.Length > 0)
                                    __result = _excute(__queryStr);
                            }
                            catch
                            {
                            }


                            //if (this._databaseID != 3 && this._databaseID != 4)
                            //{
                            __createQuery = new StringBuilder();
                            if (__getIndentity.ToLower().CompareTo("yes") != 0)
                            {
                                // Default
                                if (__getTypeReal.Equals("varchar"))
                                {
                                    switch (this._databaseID)
                                    {
                                        case 3:
                                        case 4:
                                            __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD DEFAULT '' FOR ").Append(__getRealFieldName);
                                            break;
                                        default:
                                            __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append(" set default ''");
                                            break;
                                    }
                                }
                                if (__getTypeReal.Equals("int") || __getTypeReal.Equals("smallint"))
                                {
                                    switch (this._databaseID)
                                    {
                                        case 3:
                                            __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append(" set default 0");
                                            break;
                                        case 4:
                                            __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD DEFAULT 0 FOR ").Append(__getRealFieldName);
                                            break;
                                        default:
                                            __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append(" set default 0");
                                            break;
                                    }
                                }
                                if (__getTypeReal.Equals("float") || __getTypeReal.Equals("numberic"))
                                {
                                    __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append(" set default 0.0");
                                }
                            }
                            //try
                            //{
                            //    // Get a Statement object
                            //    String __queryStr = __createQuery.ToString();
                            //    if (__queryStr.Length > 0)
                            //    {
                            //        Statement __stmt = __con.createStatement();
                            //        __stmt.executeUpdate(__queryStr);
                            //        __stmt.close();
                            //    }
                            //}
                            //catch (Exception __ex)
                            //{
                            //}
                            try
                            {
                                String __queryStr = __createQuery.ToString();
                                string __tmpResult = _excute(__queryStr);

                                Console.WriteLine(__tmpResult);
                            }
                            catch
                            {
                            }
                            //}

                        }
                    }
                    // ประเภทผิด, ความยาวไม่ตรง จะต้องทำการสร้าง Database ใหม่ แล้ว Copy
                    // ข้อมูลไป วันหลังจะทำ
                }

            }
            catch (Exception __ex)
            {
            }
            return __result;
        }

        public String _createTable()
        {
            StringBuilder __result = new StringBuilder();

            return __result.ToString();
        }

        private String _fieldTypeName(String fieldTypeName)
        {
            String __getType = fieldTypeName.ToLower();
            if (fieldTypeName.Equals("float"))
            {
                switch (_databaseID)
                {
                    case 1:
                        // PostgreSQL
                        __getType = "numeric";
                        break;
                    case 2:
                        // MySQL
                        __getType = "float";
                        break;
                }
            }
            else if (fieldTypeName.Equals("smalldatetime"))
            {
                switch (_databaseID)
                {
                    case 1:
                        // PostgreSQL
                        __getType = "date";
                        break;
                    case 2:
                        // MySQL
                        __getType = "datetime";
                        break;
                }
            }
            else if (fieldTypeName.Equals("varchar"))
            {
                switch (_databaseID)
                {
                    case 1:
                        // PostgreSQL
                        __getType = "character varying";
                        break;
                }
            }
            else if (fieldTypeName.Equals("tinyint"))
            {
                switch (_databaseID)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        // PostgreSQL
                        __getType = "smallint";
                        break;
                }
            }
            else if (fieldTypeName.Equals("datetime"))
            {
                switch (_databaseID)
                {
                    case 1:
                        // PostgreSQL
                        __getType = "timestamp without time zone";
                        break;
                }
            }
            else if (fieldTypeName.Equals("image"))
            {
                switch (_databaseID)
                {
                    case 1:
                        // PostgreSQL
                        // __getType = "oid";
                        __getType = "bytea";
                        break;
                    case 2:
                        // MySQL
                        __getType = "longblob";
                        break;
                }
                //MOO
            }
            else if (fieldTypeName.Equals("text"))
            {
                switch (_databaseID)
                {
                    case 1:
                        // PostgreSQL
                        // __getType = "oid";
                        __getType = "text";
                        break;
                    case 2:
                        // MySQL
                        __getType = "text";
                        break;
                }

            }
            else if (fieldTypeName.Equals("currency"))
            {
                if (_databaseID == 4)
                {
                    __getType = "money";
                }
            }
            return __getType;
        }

        /**
         * สร้าง Table ใหม่
         *
         * @param databaseName
         * @param tableName
         * @param readerField
         * @return
         */
        private String _createTable(String databaseName, String tableName, XmlNodeList readerField)
        {
            // สร้าง Table
            StringBuilder __createQuery = new StringBuilder("");
            __createQuery.Append("create table ").Append(tableName).Append(" (");
            Boolean __create = false;
            for (int __field = -2; __field < readerField.Count; __field++)
            {
                XmlElement __fieldElement = (__field == -1 || __field == -2) ? null : (XmlElement)readerField.Item(__field);

                String __getFieldName = (__field == -1) ? "roworder" : ((__field == -2) ? "is_lock_record" : __fieldElement.GetAttribute("name").ToLower());
                String __getType = (__field == -1 || __field == -2) ? "int" : _fieldTypeName(__fieldElement.GetAttribute("type").ToLower());
                String __getLength = (__field == -1 || __field == -2) ? "0" : __fieldElement.GetAttribute("length").ToLower();
                String __getIndentity = (__field == -1) ? "yes" : ((__field == -2) ? "no" : __fieldElement.GetAttribute("indentity").ToLower());
                String __getAllowNulls = (__field == -1) ? "false" : ((__field == -2) ? "true" : __fieldElement.GetAttribute("allow_null").ToLower());
                Boolean __getResourceOnly = (__field == -1 || __field == -2) ? false : ((__fieldElement.GetAttribute("resource_only").ToLower().CompareTo("true") == 0) ? true : false);

                if (__getResourceOnly == false)
                {
                    __create = true;
                    if (__field != -2)
                    {
                        // กรณีเป็น loop แรก ไม่ต้องใส่คอมม่า
                        __createQuery.Append(",");
                    }
                    __createQuery.Append(" ").Append(__getFieldName).Append(" ");
                    String __oldQuery = __createQuery.ToString();
                    __createQuery.Append(__getType);
                    // if (__getType.compareTo("int") == 0 || __getType.compareTo("image") == 0 || __getType.compareTo("oid") == 0 || __getType.compareTo("blob") == 0) {
                    if (__getType.CompareTo("int") == 0 || __getType.CompareTo("image") == 0 || __getType.CompareTo("bytea") == 0 || __getType.CompareTo("longblob") == 0)
                    {
                        __getLength = "0";
                    }
                    if (__getLength != null)
                    {
                        if (__getLength.CompareTo("0") != 0)
                        {
                            __createQuery.Append("(").Append(__getLength).Append(")");
                        }
                    }
                    if (__getIndentity != null)
                    {
                        if (__getIndentity.ToLower().CompareTo("yes") == 0)
                        {
                            switch (this._databaseID)
                            {
                                case 1:
                                    // PostgreSql
                                    __createQuery = new StringBuilder("");
                                    __createQuery.Append(__oldQuery);
                                    __createQuery.Append(" serial ");
                                    break;
                                case 2:
                                    __createQuery.Append(" AUTO_INCREMENT PRIMARY key ");
                                    __getAllowNulls = "false";
                                    //  __createQuery.Append(" default '1'");//somruk
                                    break;
                                case 3:
                                case 4:
                                    // Microsoft SQL
                                    __createQuery.Append(" IDENTITY (1,1) ");
                                    break;
                            }
                        }
                    }
                    if (__getAllowNulls != null)
                    {
                        if (__getAllowNulls.ToLower().CompareTo("false") == 0)
                        {
                            __createQuery.Append(" NOT NULL ");
                        }
                        if (__getAllowNulls.ToLower().CompareTo("true") == 0)
                        {
                            __createQuery.Append(" NULL ");
                        }
                    }
                    else
                    {
                        __createQuery.Append(" NULL ");
                    }
                }
            }
            switch (this._databaseID)
            {
                case 1:
                    // PostgreSQL
                    __createQuery.Append(") ");
                    break;
                case 2:
                    // MySQL
                    __createQuery.Append(") ENGINE = InnoDB DEFAULT CHARACTER SET = tis620 COLLATE = tis620_thai_ci;");
                    break;
                case 3:
                case 4:
                    // Microsoft SQL
                    __createQuery.Append(") ON [PRIMARY]");
                    break;
            }

            String __result = "";
            if (__create)
            {
                String __getString = __createQuery.ToString();
                __result = _excute(__getString);
            }
            return __result;
        }

        public string _excute(string script)
        {
            String __result = "";
            try
            {
                switch (this._databaseID)
                {
                    case 3:
                    case 4:

                        SqlConnection __con = (SqlConnection)_connect();
                        if (__con.State == ConnectionState.Open)
                            __con.Close();

                        __con.Open();

                        SqlCommand _objCmd = new SqlCommand();
                        _objCmd.Connection = __con;
                        _objCmd.CommandType = CommandType.Text;
                        _objCmd.CommandText = script;

                        _objCmd.ExecuteNonQuery();

                        __con.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                __result = ex.Message + ":" + script;
            }

            return __result;
        }

        public Boolean _isTableExists(string tableName)
        {
            Boolean __result = false;

            try
            {
                switch (this._databaseID)
                {
                    case 3:
                    case 4:

                        string __query = "select table_name from information_schema.tables where table_name='" + tableName + "'";

                        SqlConnection __objConn = (SqlConnection)_connect();
                        if (__objConn.State == ConnectionState.Open)
                            __objConn.Close();
                        __objConn.Open();

                        DataSet ds = new DataSet();


                        SqlDataAdapter dtAdapter = new SqlDataAdapter();

                        SqlCommand __objCmd = new SqlCommand();
                        __objCmd.Connection = __objConn;
                        __objCmd.CommandText = __query;
                        __objCmd.CommandType = CommandType.Text;

                        dtAdapter.SelectCommand = (SqlCommand)__objCmd;
                        dtAdapter.Fill(ds);


                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString().ToLower().Equals(tableName.ToLower()))
                            {
                                return true;
                            }
                        }
                        break;
                }
            }
            catch
            {
            }

            return __result;
        }

        /**
     * ในกรณี index ที่มีมากกว่า 1 field โปรแกรมจะ pack ให้โดยอัตโนมัติ
     * ใช้ในการเปรียบเทียบว่าเป็น index แบบเดิมหรือแบบใหม่
     *
     * @param databaseName
     * @param tableName
     * @param fieldName
     * @param fieldType
     * @param fieldLength
     * @return
     */
        //private
        public int _findField(String tableName, String fieldName, String fieldType, int fieldLength)
        {
            int __result = 1; // ไม่มี Field ไว้ก่อน

            try
            {
                switch (this._databaseID)
                {
                    case 3:
                    case 4:

                        string __query = "SELECT COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName.ToUpper() + "' ";

                        SqlConnection __objConn = (SqlConnection)_connect();
                        if (__objConn.State == ConnectionState.Open)
                            __objConn.Close();
                        __objConn.Open();

                        DataSet ds = new DataSet();


                        SqlDataAdapter dtAdapter = new SqlDataAdapter();

                        SqlCommand __objCmd = new SqlCommand();
                        __objCmd.Connection = __objConn;
                        __objCmd.CommandText = __query;
                        __objCmd.CommandType = CommandType.Text;

                        dtAdapter.SelectCommand = (SqlCommand)__objCmd;
                        dtAdapter.Fill(ds);


                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int __i = 0; __i < ds.Tables[0].Rows.Count; __i++)
                            {
                                String __getFieldName = ds.Tables[0].Rows[__i]["COLUMN_NAME"].ToString();
                                String __getFieldType = _reconvertFieldType(ds.Tables[0].Rows[__i]["DATA_TYPE"].ToString().Trim()).ToLower();
                                int __getLength = (int)MyLib._myGlobal._decimalPhase(ds.Tables[0].Rows[__i]["CHARACTER_MAXIMUM_LENGTH"].ToString());
                                if (__getFieldName.CompareTo(fieldName.ToLower()) == 0)
                                {
                                    __result = 0; // มี Field
                                    if (__getFieldName.Equals("roworder") == false)
                                    {
                                        switch (this._databaseID)
                                        {
                                            case 1:
                                                // PostgreSql
                                                if (__getFieldType.Equals(fieldType) == false)
                                                {
                                                    __result = 3; // ประเภทผิด
                                                }
                                                else if (__getFieldType.Equals("varchar") && __getLength != fieldLength)
                                                {
                                                    __result = 2; // ความยาวไม่ได้
                                                }

                                                break;
                                            case 2:
                                                //MySQL
                                                if (__getFieldType.CompareTo(fieldType.ToLower()) != 0)
                                                {
                                                    __result = 3; // ประเภทผิด
                                                }
                                                else if (__getFieldType.Equals("varchar") && __getLength != fieldLength)
                                                {
                                                    __result = 2; // ความยาวไม่ได้
                                                }

                                                break;
                                            case 3:
                                            case 4:
                                                // Microsoft SQL
                                                if (__getFieldType.CompareTo(fieldType.ToLower()) != 0)
                                                {
                                                    __result = 3; // ประเภทผิด
                                                }
                                                else if (__getFieldType.Equals("varchar") && __getLength != fieldLength)
                                                {
                                                    __result = 2; // ความยาวไม่ได้
                                                }

                                                break;
                                        }

                                    }
                                    break;
                                }
                            }

                            //if (ds.Tables[0].Rows[0][0].ToString().ToLower().Equals(tableName.ToLower()))
                            //{
                            //    return true;
                            //}
                        }
                        break;
                }
            }
            catch
            {
            }

            /*
            try
            {
                DatabaseMetaData __dbmd = __con.getMetaData();
                ResultSet __columnRS = __dbmd.getColumns(null, null, tableName, null);
                while (__columnRS.next())
                {
                    String __getFieldName = __columnRS.getString("COLUMN_NAME");
                    String __getFieldType = _reconvertFieldType(__columnRS.getString("TYPE_NAME").trim()).toLowerCase();
                    int __getLength = __columnRS.getInt("COLUMN_SIZE");
                    if (__getFieldName.compareTo(fieldName.toLowerCase()) == 0)
                    {
                        __result = 0; // มี Field
                        if (__getFieldName.equalsIgnoreCase("roworder") == false)
                        {
                            switch (this._databaseID)
                            {
                                case 1:
                                    // PostgreSql
                                    if (__getFieldType.equalsIgnoreCase(fieldType) == false)
                                    {
                                        __result = 3; // ประเภทผิด
                                    }
                                    else if (__getFieldType.equals("varchar") && __getLength != fieldLength)
                                    {
                                        __result = 2; // ความยาวไม่ได้
                                    }

                                    break;
                                case 2:
                                    //MySQL
                                    if (__getFieldType.compareTo(fieldType.toLowerCase()) != 0)
                                    {
                                        __result = 3; // ประเภทผิด
                                    }
                                    else if (__getFieldType.equals("varchar") && __getLength != fieldLength)
                                    {
                                        __result = 2; // ความยาวไม่ได้
                                    }

                                    break;
                                case 3:
                                case 4:
                                    // Microsoft SQL
                                    if (__getFieldType.compareTo(fieldType.toLowerCase()) != 0)
                                    {
                                        __result = 3; // ประเภทผิด
                                    }
                                    else if (__getFieldType.equals("varchar") && __getLength != fieldLength)
                                    {
                                        __result = 2; // ความยาวไม่ได้
                                    }

                                    break;
                            }

                        }
                        break;
                    }

                } // while
                __columnRS.close();
            }
            catch (Exception __ex)
            {
            }
             * */
            return __result;
        }

        public String _reconvertFieldType(String fieldType)
        {
            switch (this._databaseID)
            {
                case 1:
                    if (fieldType.ToLower().Equals("int4".ToLower()))
                    {
                        return "int";
                    }
                    if (fieldType.ToLower().Equals("int2".ToLower()))
                    {
                        return "smallint";
                    }
                    if (fieldType.ToLower().Equals("varchar".ToLower()))
                    {
                        return "character varying";
                    }
                    if (fieldType.ToLower().Equals("timestamp".ToLower()))
                    {
                        return "timestamp without time zone";
                    }
                    break;
                case 2:
                    //MySQL
                    break;
                case 3:
                case 4:
                    // Microsoft SQL
                    break;

            }
            return fieldType;
        }
    }
}
