using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

namespace SMLTOBC
{
    /*
     champ_guid 
     table_code (1=ap,2=ar,3=ic_trans)
     action_code (1=Insert,2=Delete,3=Update)
    */

    public partial class Form1 : Form
    {
        Thread _autoThread = null;
        Thread _countThreadRun = null;
        List<String> _autoStatus = new List<string>();
        CultureInfo _culture = new CultureInfo("en-US");
        public Form1()
        {
            InitializeComponent();

            _routine __routine = new _routine();
            _global __global = __routine._loadConfig();
            this._smlServerTextBox.Text = __global._smlConnectProvider;
            this._smlUserTextBox.Text = __global._smlConnectUser;
            this._smlPasswordTextBox.Text = __global._smlConnectPassword;
            this._smlDatabaseNameTextBox.Text = __global._smlConnectDatabaseName;
            //
            this._bcServerTextBox.Text = __global._bcConnectProvider;
            this._bcUserTextBox.Text = __global._bcConnectUser;
            this._bcPasswordTextBox.Text = __global._bcConnectPassword;
            this._bcDatabaseNameTextBox.Text = __global._bcConnectDatabaseName;
            //
            this._testConnectDatabase();
            //
            this._countThreadRun = new Thread(new ThreadStart(_countThread));
            this._countThreadRun.IsBackground = true;
            this._countThreadRun.Start();
            this.Disposed += Form1_Disposed;
        }

        private void Form1_Disposed(object sender, EventArgs e)
        {
            try
            {
                this._autoThread.Abort();
            }
            catch
            {

            }
            try
            {
                this._countThreadRun.Abort();
            }
            catch
            {

            }
        }

        public class _mapField
        {
            public string _bc;
            public string _sml;
            /// <summary>
            /// 0=String,1=Integer,2=Date,3=Double
            /// </summary>
            public int _type;
            public int _no;
            public Boolean _check;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="bc"></param>
            /// <param name="sml"></param>
            /// <param name="type">0=String,1=Integer,2=Date,3=Double</param>
            /// <param name="no"></param>
            /// <param name="check"></param>
            public _mapField(string bc, string sml, int type, int no, Boolean check)
            {
                this._bc = bc;
                this._sml = sml;
                this._type = type;
                this._no = no;
                this._check = check;
            }
        }

        public class _mapFieldCommand
        {
            public string _fieldSql;
            public string _insertCommand;
            public string _updateCommand;
            public Boolean _isUpdate = false;
        }

        public _mapFieldCommand _mapFieldGen(List<_mapField> mapField, SqlDataReader sqlReader, List<NpgsqlDataReader> reader)
        {
            string __lastFieldName = "";
            if (reader != null)
            {
                for (int __loop = 0; __loop < reader.Count; __loop++)
                {
                    reader[__loop].Read();
                }
            }
            _mapFieldCommand __result = new _mapFieldCommand();
            StringBuilder __fieldSql = new StringBuilder();
            StringBuilder __insertField = new StringBuilder();
            StringBuilder __insertValue = new StringBuilder();
            StringBuilder __updateCommand = new StringBuilder();
            try
            {
                for (int __loop = 0; __loop < mapField.Count; __loop++)
                {
                    _mapField __mapField = mapField[__loop];
                    if (__insertField.Length > 0)
                    {
                        __fieldSql.Append(",");
                        __insertField.Append(",");
                        __insertValue.Append(",");
                    }
                    __fieldSql.Append(__mapField._bc);
                    __insertField.Append(__mapField._bc);
                    if (reader != null)
                    {
                        switch (__mapField._type)
                        {
                            case 0:
                                {
                                    __lastFieldName = __mapField._sml;
                                    int __getOrdinal = reader[__mapField._no].GetOrdinal(__mapField._sml);
                                    string __value = reader[__mapField._no].IsDBNull(__getOrdinal) ? "" : reader[__mapField._no][__getOrdinal].ToString();
                                    __insertValue.Append("'" + reader[__mapField._no][__mapField._sml].ToString() + "'");
                                }
                                break;
                            case 1:
                                if (__mapField._sml[0] == '#')
                                {
                                    string __cut = __mapField._sml.Remove(0, 1);
                                    __insertValue.Append(__cut);
                                }
                                else
                                {
                                    __lastFieldName = __mapField._sml;
                                    int __getOrdinal = reader[__mapField._no].GetOrdinal(__mapField._sml);
                                    string __value = reader[__mapField._no].IsDBNull(__getOrdinal) ? "" : reader[__mapField._no][__getOrdinal].ToString();
                                    __insertValue.Append(__value);
                                }
                                break;
                            case 2:
                                break;
                        }
                    }
                }
                if (sqlReader != null)
                {
                    for (int __loop = 0; __loop < mapField.Count; __loop++)
                    {
                        _mapField __mapField = mapField[__loop];
                        if (__mapField._check)
                        {
                            if (reader != null)
                            {
                                switch (__mapField._type)
                                {
                                    case 0:
                                        {
                                            string __sourceValue = reader[__mapField._no][__mapField._sml].ToString();
                                            string __descValue = sqlReader[__mapField._bc].ToString();
                                            if (__sourceValue.Equals(__descValue) == false)
                                            {
                                                if (__updateCommand.Length > 0)
                                                {
                                                    __updateCommand.Append(",");
                                                }
                                                __updateCommand.Append(__mapField._bc + "=\'" + __sourceValue + "\'");
                                                __result._isUpdate = true;
                                            }
                                        }
                                        break;
                                    case 1:
                                        break;
                                    case 2:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception __ex)
            {
                int a = 0;
                int b = a + 1;
            }
            __result._fieldSql = __fieldSql.ToString();
            __result._insertCommand = "(" + __insertField.ToString() + ") values (" + __insertValue.ToString() + ")";
            __result._updateCommand = __updateCommand.ToString();
            return __result;
        }

        public void _countThread()
        {
            _routine __routine = new _routine();
            _global __global = __routine._loadConfig();
            NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
            while (true)
            {
                try
                {
                    __connPostgre.Open();
                    NpgsqlCommand __command = new NpgsqlCommand("SELECT COUNT(*) FROM champ_guid", __connPostgre);
                    object __value = __command.ExecuteScalar();
                    if (__value != null)
                    {
                        _static._guidCount = Int32.Parse(__value.ToString());
                    }
                }
                catch (Exception __ex)
                {
                    _autoStatus.Add(__ex.Message.ToString() + Environment.NewLine);
                }
                finally
                {
                    __connPostgre.Close();
                }
                Thread.Sleep(1000);
            }
        }
        public void _autoThreadFunction()
        {
            // Item 
            List<_mapField> __mapFieldItem = new List<_mapField>();
            __mapFieldItem.Add(new _mapField("code", "code", 0, 0, false));
            __mapFieldItem.Add(new _mapField("name1", "name_1", 0, 0, true));
            __mapFieldItem.Add(new _mapField("name2", "name_2", 0, 0, true));
            __mapFieldItem.Add(new _mapField("defstkunitcode", "unit_standard", 0, 0, true));
            __mapFieldItem.Add(new _mapField("unittype", "unit_type", 0, 0, true));

            // เจ้าหนี้
            List<_mapField> __mapFieldAp = new List<_mapField>();
            __mapFieldAp.Add(new _mapField("code", "code", 0, 0, false));
            __mapFieldAp.Add(new _mapField("name1", "name_1", 0, 0, true));
            __mapFieldAp.Add(new _mapField("name2", "name_2", 0, 0, true));
            __mapFieldAp.Add(new _mapField("Address", "address", 0, 0, true));
            __mapFieldAp.Add(new _mapField("GroupCode", "group_main", 0, 1, true));
            __mapFieldAp.Add(new _mapField("IDCardNo", "card_id", 0, 1, true));
            __mapFieldAp.Add(new _mapField("Telephone", "telephone", 0, 0, true));
            __mapFieldAp.Add(new _mapField("ActiveStatus", "#1", 1, 0, true));

            // ลูกหนี้
            List<_mapField> __mapFieldAr = new List<_mapField>();
            __mapFieldAr.Add(new _mapField("code", "code", 0, 0, false));
            __mapFieldAr.Add(new _mapField("name1", "name_1", 0, 0, true));
            __mapFieldAr.Add(new _mapField("name2", "name_2", 0, 0, true));
            __mapFieldAr.Add(new _mapField("billaddress", "address", 0, 0, true));
            __mapFieldAr.Add(new _mapField("workaddress", "address", 0, 0, true));
            __mapFieldAr.Add(new _mapField("telephone", "telephone", 0, 0, true));
            __mapFieldAr.Add(new _mapField("fax", "fax", 0, 0, true));
            __mapFieldAr.Add(new _mapField("idcardno", "card_id", 0, 1, true));
            __mapFieldAr.Add(new _mapField("groupcode", "group_main", 0, 1, true));
            __mapFieldAr.Add(new _mapField("memberid", "code", 0, 0, true));
            __mapFieldAr.Add(new _mapField("ActiveStatus", "#1", 1, 0, true));
            // ขาย
            List<_mapField> __mapFieldIcTrans = new List<_mapField>();
            __mapFieldIcTrans.Add(new _mapField("doc_date", "docdate", 2, 0, false));
            __mapFieldIcTrans.Add(new _mapField("doc_no", "doc_no", 0, 0, true));
            __mapFieldIcTrans.Add(new _mapField("cust_code", "arcode", 0, 0, true));
            __mapFieldIcTrans.Add(new _mapField("total_amount", "card_id", 3, 0, true));
            //
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                //
                {
                    List<_champGuid> __champGuid = new List<_champGuid>();
                    //
                    /*
                    NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                    __connPostgre.Open();
                    //
                    SqlConnection __connSqlUpdate = __routine._bcConnection(__global);
                    __connSqlUpdate.Open();
                    //
                    SqlConnection __connSql = __routine._bcConnection(__global);
                    __connSql.Open();
                    //
                    NpgsqlConnection __connPostgreDetail1 = __routine._smlConnection(__global);
                    __connPostgreDetail1.Open();
                    NpgsqlConnection __connPostgreDetail2 = __routine._smlConnection(__global);
                    __connPostgreDetail2.Open();
                    SqlConnection __connSqlExec = __routine._bcConnection(__global);
                    __connSqlExec.Open();
                    */
                    //
                    {
                        NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                        __connPostgre.Open();
                        NpgsqlCommand __command = new NpgsqlCommand("select ref_no_1,action_code,roworder,table_code from champ_guid order by create_date_time_now", __connPostgre);
                        NpgsqlDataReader __dr = __command.ExecuteReader();
                        while (__dr.Read())
                        {
                            int __tableCodeInt = Int32.Parse(__dr[3].ToString());
                            _tableCodeEnum __tableCode =  _tableCodeEnum.Ap;
                            switch (__tableCodeInt)
                            {
                                case 1: __tableCode = _tableCodeEnum.Ar; break;
                                case 2: __tableCode = _tableCodeEnum.Ap; break;
                                case 7: __tableCode = _tableCodeEnum.Item; break;
                            }
                            __champGuid.Add(new _champGuid(__dr[0].ToString(), Int32.Parse(__dr[1].ToString()), Int32.Parse(__dr[2].ToString()), __tableCode));
                        }
                        __dr.Close();
                        __command.Dispose();
                        __connPostgre.Close();
                    }
                    for (int __row = 0; __row < __champGuid.Count; __row++)
                    {
                        _champGuid __champ = __champGuid[__row];
                        try
                        {
                            switch (__champ._actionCode)
                            {
                                case 1: // Insert 
                                    {
                                        switch (__champ._tableCode)
                                        {
                                            case _tableCodeEnum.Ap: // ap 
                                                {
                                                    NpgsqlConnection __connPostgreDetail1 = __routine._smlConnection(__global);
                                                    __connPostgreDetail1.Open();
                                                    NpgsqlConnection __connPostgreDetail2 = __routine._smlConnection(__global);
                                                    __connPostgreDetail2.Open();
                                                    NpgsqlCommand __commandDetail1 = new NpgsqlCommand("select * from ap_supplier where code='" + __champ._refNo + "'", __connPostgreDetail1);
                                                    NpgsqlCommand __commandDetail2 = new NpgsqlCommand("select * from ap_supplier_detail where ap_code='" + __champ._refNo + "'", __connPostgreDetail2);
                                                    NpgsqlDataReader __drDetail1 = __commandDetail1.ExecuteReader();
                                                    NpgsqlDataReader __drDetail2 = __commandDetail2.ExecuteReader();
                                                    // กรณีไม่เจอ ให้เพิ่มได้เลย
                                                    List<NpgsqlDataReader> __commandDetail = new List<NpgsqlDataReader>();
                                                    __commandDetail.Add(__drDetail1);
                                                    __commandDetail.Add(__drDetail2);
                                                    _mapFieldCommand __command = this._mapFieldGen(__mapFieldAp, null, __commandDetail);
                                                    string __query = "INSERT INTO BCAP " + __command._insertCommand;
                                                    //
                                                    SqlConnection __connSqlExec = __routine._bcConnection(__global);
                                                    __connSqlExec.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand(__query, __connSqlExec);
                                                    try
                                                    {
                                                        __sqlCommand.ExecuteNonQuery();
                                                        _autoStatus.Add(__query);
                                                    }
                                                    catch (Exception __ex)
                                                    {
                                                        _autoStatus.Add(__ex.Message.ToString());
                                                    }
                                                    __sqlCommand.Dispose();
                                                    __connSqlExec.Close();
                                                    //
                                                    __drDetail2.Close();
                                                    __drDetail1.Close();
                                                    __commandDetail1.Dispose();
                                                    __commandDetail2.Dispose();
                                                    //
                                                    __connPostgreDetail1.Close();
                                                    __connPostgreDetail2.Close();
                                                }
                                                break;
                                            case _tableCodeEnum.Ar: // ar 
                                                {
                                                    NpgsqlConnection __connPostgreDetail1 = __routine._smlConnection(__global);
                                                    __connPostgreDetail1.Open();
                                                    NpgsqlConnection __connPostgreDetail2 = __routine._smlConnection(__global);
                                                    __connPostgreDetail2.Open();
                                                    NpgsqlCommand __commandDetail1 = new NpgsqlCommand("select * from ar_customer where code='" + __champ._refNo + "'", __connPostgreDetail1);
                                                    NpgsqlCommand __commandDetail2 = new NpgsqlCommand("select * from ar_customer_detail where ar_code='" + __champ._refNo + "'", __connPostgreDetail2);
                                                    NpgsqlDataReader __drDetail1 = __commandDetail1.ExecuteReader();
                                                    NpgsqlDataReader __drDetail2 = __commandDetail2.ExecuteReader();
                                                    // กรณีไม่เจอ ให้เพิ่มได้เลย
                                                    List<NpgsqlDataReader> __commandDetail = new List<NpgsqlDataReader>();
                                                    __commandDetail.Add(__drDetail1);
                                                    __commandDetail.Add(__drDetail2);
                                                    _mapFieldCommand __command = this._mapFieldGen(__mapFieldAr, null, __commandDetail);
                                                    string __query = "INSERT INTO BCAR " + __command._insertCommand;
                                                    //
                                                    SqlConnection __connSqlExec = __routine._bcConnection(__global);
                                                    __connSqlExec.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand(__query, __connSqlExec);
                                                    try
                                                    {
                                                        __sqlCommand.ExecuteNonQuery();
                                                        _autoStatus.Add(__query);
                                                    }
                                                    catch (Exception __ex)
                                                    {
                                                        _autoStatus.Add(__ex.Message.ToString());
                                                    }
                                                    __sqlCommand.Dispose();
                                                    __connSqlExec.Close();
                                                    //
                                                    __drDetail2.Close();
                                                    __drDetail1.Close();
                                                    __commandDetail1.Dispose();
                                                    __commandDetail2.Dispose();
                                                    //
                                                    __connPostgreDetail1.Close();
                                                    __connPostgreDetail2.Close();
                                                }
                                                break;
                                            case _tableCodeEnum.Item: // item 
                                                {
                                                    NpgsqlConnection __connPostgreDetail1 = __routine._smlConnection(__global);
                                                    __connPostgreDetail1.Open();
                                                    NpgsqlConnection __connPostgreDetail2 = __routine._smlConnection(__global);
                                                    __connPostgreDetail2.Open();
                                                    NpgsqlCommand __commandDetail1 = new NpgsqlCommand("select * from ic_inventory where code='" + __champ._refNo + "'", __connPostgreDetail1);
                                                    NpgsqlDataReader __drDetail1 = __commandDetail1.ExecuteReader();
                                                    // กรณีไม่เจอ ให้เพิ่มได้เลย
                                                    List<NpgsqlDataReader> __commandDetail = new List<NpgsqlDataReader>();
                                                    __commandDetail.Add(__drDetail1);
                                                    _mapFieldCommand __command = this._mapFieldGen(__mapFieldItem, null, __commandDetail);
                                                    string __query = "INSERT INTO BCitem " + __command._insertCommand;
                                                    //
                                                    SqlConnection __connSqlExec = __routine._bcConnection(__global);
                                                    __connSqlExec.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand(__query, __connSqlExec);
                                                    try
                                                    {
                                                        __sqlCommand.ExecuteNonQuery();
                                                        _autoStatus.Add(__query);
                                                    }
                                                    catch (Exception __ex)
                                                    {
                                                        _autoStatus.Add(__ex.Message.ToString());
                                                    }
                                                    __sqlCommand.Dispose();
                                                    __connSqlExec.Close();
                                                    //
                                                    __drDetail1.Close();
                                                    __commandDetail1.Dispose();
                                                    //
                                                    __connPostgreDetail1.Close();
                                                    __connPostgreDetail2.Close();
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                case 2: // Delete
                                    {
                                        switch (__champ._tableCode)
                                        {
                                            case _tableCodeEnum.Ap: // ap
                                                {
                                                    string __query = "DELETE from BCAP where code='" + __champ._refNo + "'";
                                                    //
                                                    SqlConnection __connSqlExec = __routine._bcConnection(__global);
                                                    __connSqlExec.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand(__query, __connSqlExec);
                                                    __sqlCommand.ExecuteNonQuery();
                                                    __sqlCommand.Dispose();
                                                    __connSqlExec.Close();
                                                    _autoStatus.Add(__query);
                                                }
                                                break;
                                            case _tableCodeEnum.Ar: // ar 
                                                {
                                                    string __query = "DELETE from BCAR where code='" + __champ._refNo + "'";
                                                    //
                                                    SqlConnection __connSqlExec = __routine._bcConnection(__global);
                                                    __connSqlExec.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand(__query, __connSqlExec);
                                                    __sqlCommand.ExecuteNonQuery();
                                                    __sqlCommand.Dispose();
                                                    __connSqlExec.Close();
                                                    _autoStatus.Add(__query);
                                                }
                                                break;
                                            case _tableCodeEnum.Item: // item 
                                                {
                                                    string __query = "DELETE from bcitem where code='" + __champ._refNo + "'";
                                                    //
                                                    SqlConnection __connSqlExec = __routine._bcConnection(__global);
                                                    __connSqlExec.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand(__query, __connSqlExec);
                                                    __sqlCommand.ExecuteNonQuery();
                                                    __sqlCommand.Dispose();
                                                    __connSqlExec.Close();
                                                    _autoStatus.Add(__query);
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                case 3: // Update 
                                    {
                                        switch (__champ._tableCode)
                                        {
                                            case _tableCodeEnum.Ap: // ap 
                                                {
                                                    SqlConnection __connSql = __routine._bcConnection(__global);
                                                    __connSql.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand("select " + this._mapFieldGen(__mapFieldAp, null, null)._fieldSql + " from BCAp where code=\'" + __champ._refNo + "\'", __connSql);
                                                    SqlDataReader __sqlReader = __sqlCommand.ExecuteReader();
                                                    if (__sqlReader.Read())
                                                    {
                                                        NpgsqlConnection __connPostgreDetail1 = __routine._smlConnection(__global);
                                                        __connPostgreDetail1.Open();
                                                        NpgsqlCommand __commandDetail1 = new NpgsqlCommand("select * from ap_supplier where code='" + __champ._refNo + "'", __connPostgreDetail1);
                                                        NpgsqlCommand __commandDetail2 = new NpgsqlCommand("select * from ap_supplier_detail where ap_code='" + __champ._refNo + "'", __connPostgreDetail1);
                                                        NpgsqlDataReader __drDetail1 = __commandDetail1.ExecuteReader(CommandBehavior.CloseConnection);
                                                        NpgsqlDataReader __drDetail2 = __commandDetail2.ExecuteReader();
                                                        // กรณีไม่เจอ ให้เพิ่มได้เลย
                                                        List<NpgsqlDataReader> __commandDetail = new List<NpgsqlDataReader>();
                                                        __commandDetail.Add(__drDetail1);
                                                        __commandDetail.Add(__drDetail2);
                                                        _mapFieldCommand __command = this._mapFieldGen(__mapFieldAp, __sqlReader, __commandDetail);
                                                        if (__command._isUpdate)
                                                        {
                                                            string __query = "update bcap set " + __command._updateCommand + " where code=\'" + __champ._refNo + "\'";
                                                            SqlCommand __sqlUpdateCommand = new SqlCommand(__query, __connSql);
                                                            __sqlUpdateCommand.ExecuteNonQuery();
                                                            __sqlUpdateCommand.Dispose();
                                                            //
                                                            _autoStatus.Add(__query);
                                                        }
                                                        __drDetail2.Close();
                                                        __commandDetail2.Dispose();
                                                        //
                                                        __drDetail1.Close();
                                                        __commandDetail1.Dispose();
                                                        //
                                                        __connPostgreDetail1.Close();
                                                    }
                                                    __sqlReader.Close();
                                                    __sqlCommand.Dispose();
                                                    __connSql.Close();
                                                }
                                                break;
                                            case _tableCodeEnum.Ar:
                                                {
                                                    SqlConnection __connSql = __routine._bcConnection(__global);
                                                    __connSql.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand("select " + this._mapFieldGen(__mapFieldAr, null, null)._fieldSql + " from BCAR where code=\'" + __champ._refNo + "\'", __connSql);
                                                    SqlDataReader __sqlReader = __sqlCommand.ExecuteReader();
                                                    if (__sqlReader.Read())
                                                    {
                                                        NpgsqlConnection __connPostgreDetail1 = __routine._smlConnection(__global);
                                                        __connPostgreDetail1.Open();
                                                        NpgsqlConnection __connPostgreDetail2 = __routine._smlConnection(__global);
                                                        __connPostgreDetail2.Open();
                                                        NpgsqlCommand __commandDetail1 = new NpgsqlCommand("select * from ar_customer where code='" + __champ._refNo + "'", __connPostgreDetail1);
                                                        NpgsqlCommand __commandDetail2 = new NpgsqlCommand("select * from ar_customer_detail where ar_code='" + __champ._refNo + "'", __connPostgreDetail2);
                                                        NpgsqlDataReader __drDetail1 = __commandDetail1.ExecuteReader(CommandBehavior.CloseConnection);
                                                        NpgsqlDataReader __drDetail2 = __commandDetail2.ExecuteReader();
                                                        // กรณีไม่เจอ ให้เพิ่มได้เลย
                                                        List<NpgsqlDataReader> __commandDetail = new List<NpgsqlDataReader>();
                                                        __commandDetail.Add(__drDetail1);
                                                        __commandDetail.Add(__drDetail2);
                                                        _mapFieldCommand __command = this._mapFieldGen(__mapFieldAr, __sqlReader, __commandDetail);
                                                        if (__command._isUpdate)
                                                        {
                                                            string __query = "update bcar set " + __command._updateCommand + " where code=\'" + __champ._refNo + "\'";
                                                            SqlCommand __sqlUpdateCommand = new SqlCommand(__query, __connSql);
                                                            __sqlUpdateCommand.ExecuteNonQuery();
                                                            __sqlUpdateCommand.Dispose();
                                                            //
                                                            _autoStatus.Add(__query);
                                                        }
                                                        __drDetail2.Close();
                                                        __commandDetail2.Dispose();
                                                        //
                                                        __drDetail1.Close();
                                                        __commandDetail1.Dispose();
                                                        //
                                                        __connPostgreDetail1.Close();
                                                        __connPostgreDetail2.Close();
                                                    }
                                                    __sqlReader.Close();
                                                    __sqlCommand.Dispose();
                                                }
                                                break;
                                            case _tableCodeEnum.Item:
                                                {
                                                    SqlConnection __connSql = __routine._bcConnection(__global);
                                                    __connSql.Open();
                                                    SqlCommand __sqlCommand = new SqlCommand("select " + this._mapFieldGen(__mapFieldAr, null, null)._fieldSql + " from BCAR where code=\'" + __champ._refNo + "\'", __connSql);
                                                    SqlDataReader __sqlReader = __sqlCommand.ExecuteReader();
                                                    if (__sqlReader.Read())
                                                    {
                                                        NpgsqlConnection __connPostgreDetail1 = __routine._smlConnection(__global);
                                                        __connPostgreDetail1.Open();
                                                        NpgsqlCommand __commandDetail1 = new NpgsqlCommand("select * from ar_customer where code='" + __champ._refNo + "'", __connPostgreDetail1);
                                                        NpgsqlDataReader __drDetail1 = __commandDetail1.ExecuteReader(CommandBehavior.CloseConnection);
                                                        // กรณีไม่เจอ ให้เพิ่มได้เลย
                                                        List<NpgsqlDataReader> __commandDetail = new List<NpgsqlDataReader>();
                                                        __commandDetail.Add(__drDetail1);
                                                        _mapFieldCommand __command = this._mapFieldGen(__mapFieldItem, __sqlReader, __commandDetail);
                                                        if (__command._isUpdate)
                                                        {
                                                            string __query = "update bcitem set " + __command._updateCommand + " where code=\'" + __champ._refNo + "\'";
                                                            SqlCommand __sqlUpdateCommand = new SqlCommand(__query, __connSql);
                                                            __sqlUpdateCommand.ExecuteNonQuery();
                                                            __sqlUpdateCommand.Dispose();
                                                            //
                                                            _autoStatus.Add(__query);
                                                        }
                                                        //
                                                        __drDetail1.Close();
                                                        __commandDetail1.Dispose();
                                                        //
                                                        __connPostgreDetail1.Close();
                                                    }
                                                    __sqlReader.Close();
                                                    __sqlCommand.Dispose();
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        catch (Exception __ex)
                        {
                            _autoStatus.Add(__ex.Message.ToString());
                        }
                        try
                        {
                            NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                            __connPostgre.Open();
                            NpgsqlCommand __deleteCommand = new NpgsqlCommand("delete from champ_guid where roworder=" + __champ._rowOrder.ToString(), __connPostgre);
                            __deleteCommand.ExecuteNonQuery();
                            __deleteCommand.Dispose();
                            __connPostgre.Close();
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch (Exception __ex)
            {
                _autoStatus.Add(__ex.Message.ToString());
            }
        }


        void _testConnectDatabase()
        {
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                NpgsqlConnection __conn = __routine._smlConnection(__global);
                __conn.Open();
                __conn.Close();
                this._postgreSqlConnectStatus.Text = "เชื่อม SML (" + __global._smlConnectProvider + "->" + __global._smlConnectDatabaseName + ") สำเร็จ";
                this._postgreSqlConnectStatus.ForeColor = Color.Blue;
            }
            catch
            {
                this._postgreSqlConnectStatus.Text = "เชื่อม SML ไม่สำเร็จ";
                this._postgreSqlConnectStatus.ForeColor = Color.Red;
            }
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                SqlConnection __conn = __routine._bcConnection(__global);
                __conn.Open();
                __conn.Close();
                this._microsoftSqlConnectStatus.Text = "เชื่อม CHAMP (" + __global._bcConnectProvider + "->" + __global._bcConnectDatabaseName + ") สำเร็จ";
                this._microsoftSqlConnectStatus.ForeColor = Color.Blue;
            }
            catch
            {
                this._microsoftSqlConnectStatus.Text = "เชื่อม CHAMP ไม่สำเร็จ";
                this._microsoftSqlConnectStatus.ForeColor = Color.Red;
            }
        }

        private void _smlConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                __global._smlConnectProvider = this._smlServerTextBox.Text.Trim();
                __global._smlConnectUser = this._smlUserTextBox.Text.Trim();
                __global._smlConnectPassword = this._smlPasswordTextBox.Text.Trim();
                __global._smlConnectDatabaseName = this._smlDatabaseNameTextBox.Text.Trim();
                NpgsqlConnection __conn = __routine._smlConnection(__global);
                __conn.Open();
                __conn.Close();
                this._smlConnectStatusTextBox.Text = "เชื่อมต่อสำเร็จ";
                __routine._saveConfig(__global);
            }
            catch (Exception __ex)
            {
                this._smlConnectStatusTextBox.Text = "เชื่อมต่อไม่สำเร็จ : " + __ex.Message.ToString();
            }
            this._testConnectDatabase();
        }

        private void _bcConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                __global._bcConnectProvider = this._bcServerTextBox.Text.Trim();
                __global._bcConnectUser = this._bcUserTextBox.Text.Trim();
                __global._bcConnectPassword = this._bcPasswordTextBox.Text.Trim();
                __global._bcConnectDatabaseName = this._bcDatabaseNameTextBox.Text.Trim();
                SqlConnection __conn = __routine._bcConnection(__global);
                __conn.Open();
                __conn.Close();
                this._bcConnectStatusTextBox.Text = "เชื่อมต่อสำเร็จ";
                __routine._saveConfig(__global);
            }
            catch (Exception __ex)
            {
                this._bcConnectStatusTextBox.Text = "เชื่อมต่อไม่สำเร็จ : " + __ex.Message.ToString();
            }
            this._testConnectDatabase();
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            this._timer.Stop();
            Boolean __scrollToCaret = false;
            try
            {
                while (this._autoStatus.Count > 0)
                {
                    if (this._autoTextBox.Lines.Length > 1000)
                    {
                        this._autoTextBox.Clear();
                    }
                    this._autoTextBox.AppendText(this._autoStatus[0].ToString() + Environment.NewLine);
                    this._autoStatus.RemoveAt(0);
                    __scrollToCaret = true;
                }
                if (__scrollToCaret)
                {
                    this._autoTextBox.ScrollToCaret();
                }
            }
            catch (Exception __ex)
            {
                this._autoTextBox.AppendText(__ex.Message.ToString() + Environment.NewLine);
            }
            try
            {
                this._countChapGuidLabel.Text = (_static._guidCount == 0) ? "ไม่มีรายการรอโอน" : "รายการโอนคงเหลือ : " + _static._guidCount.ToString() + " รายการ";
                if (_static._guidCount == 0)
                {
                    this._autoButton.Visible = false;
                    this._autoButton.Text = "";
                }
                else
                {
                    this._autoButton.Visible = true;
                    this._autoButton.Text = "เริ่มโอนข้อมูล";
                }
            }
            catch (Exception __ex)
            {
                this._autoTextBox.AppendText(__ex.Message.ToString() + Environment.NewLine);
            }
            this._timer.Start();
        }

        private void _createTriggerButton_Click(object sender, EventArgs e)
        {
            _validForm __valid = new _validForm();
            __valid.ShowDialog();
            if (__valid._pass)
            {
                try
                {
                    this._autoThread.Abort();
                }
                catch
                {

                }
                try
                {
                    _routine __routine = new _routine();
                    _global __global = __routine._loadConfig();
                    NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                    __connPostgre.Open();
                    try
                    {
                        NpgsqlCommand __createChampGuid = new NpgsqlCommand(@"
                        CREATE TABLE public.champ_guid
                        (
                          roworder  serial primary key,
                          table_code smallint,
                          ref_no_1 character varying(50),
                          ref_no_2 character varying(50),
                          ref_no_3 character varying(50),
                          line_number int,
                          action_code smallint,
                          trans_flag smallint,
                          create_date_time_now timestamp without time zone DEFAULT now()
                        )
                        WITH(
                          OIDS= FALSE
                        );
                        ALTER TABLE public.champ_guid OWNER TO postgres;", __connPostgre);
                        try
                        {

                            __createChampGuid.ExecuteNonQuery();
                        }
                        catch
                        {

                        }
                        __createChampGuid.Dispose();
                        //
                        List<_tableDetail> __tableName = new List<_tableDetail>();
                        __tableName.Add(new _tableDetail(1, "ar_customer", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(2, "ap_supplier", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(3, "ic_trans", "doc_no,trans_flag", "ref_no_1,trans_flag"));
                        __tableName.Add(new _tableDetail(4, "ic_trans_detail", "doc_no,trans_flag,line_number", "ref_no_1,trans_flag,line_number"));
                        __tableName.Add(new _tableDetail(5, "ar_group", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(6, "ic_inventory_barcode", "barcode", "ref_no_1"));
                        __tableName.Add(new _tableDetail(7, "ic_inventory", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(8, "ic_group", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(9, "ic_type", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(10, "ic_category", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(11, "ic_pattern", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(12, "ic_unit", "code", "ref_no_1"));
                        __tableName.Add(new _tableDetail(13, "ic_unit_use", "ic_code,code", "ref_no_1,ref_no_2"));
                        __tableName.Add(new _tableDetail(14, "ic_warehouse", "code", "ref_no_1"));
                        for (int __loop = 0; __loop < __tableName.Count; __loop++)
                        {
                            _tableDetail __tableDetails = __tableName[__loop];
                            StringBuilder __query = new StringBuilder();
                            StringBuilder __fieldChampGuid = new StringBuilder();
                            StringBuilder __refGuidOld = new StringBuilder();
                            StringBuilder __refGuidNew = new StringBuilder();
                            StringBuilder __refGuidCompare = new StringBuilder();
                            for (int __add = 0; __add < __tableDetails._fieldChampGuid.Count; __add++)
                            {
                                // drop trigger
                                {
                                    for (int __del = 0; __del < 3; __del++)
                                    {
                                        string __commandName = "";
                                        switch (__del)
                                        {
                                            case 0: __commandName = "delete"; break;
                                            case 1: __commandName = "insert"; break;
                                            case 2: __commandName = "update"; break;
                                        }
                                        NpgsqlCommand __dropTrigger = new NpgsqlCommand("DROP TRIGGER champ_" + __tableDetails._tableName + "_after_" + __commandName + "_trigger ON public." + __tableDetails._tableName + ";", __connPostgre);
                                        try
                                        {
                                            __dropTrigger.ExecuteNonQuery();
                                        }
                                        catch
                                        {
                                        }
                                        __dropTrigger.Dispose();
                                    }
                                }
                                // drop function
                                {
                                    for (int __del = 0; __del < 3; __del++)
                                    {
                                        string __commandName = "";
                                        switch (__del)
                                        {
                                            case 0: __commandName = "delete"; break;
                                            case 1: __commandName = "insert"; break;
                                            case 2: __commandName = "update"; break;
                                        }
                                        NpgsqlCommand __dropFunction = new NpgsqlCommand("DROP FUNCTION public.champ_" + __tableDetails._tableName + "_after_" + __commandName + "();", __connPostgre);
                                        try
                                        {
                                            __dropFunction.ExecuteNonQuery();
                                        }
                                        catch
                                        {
                                        }
                                        __dropFunction.Dispose();
                                    }
                                }
                                {
                                    if (__fieldChampGuid.Length > 0)
                                    {
                                        __fieldChampGuid.Append(",");
                                        __refGuidOld.Append(",");
                                        __refGuidNew.Append(",");
                                        __refGuidCompare.Append(" and ");
                                    }
                                    __fieldChampGuid.Append(__tableDetails._fieldChampGuid[__add].ToString());
                                    string __fieldName = __tableDetails._fieldCompare[__add].ToString();
                                    __refGuidOld.Append("OLD." + __fieldName);
                                    __refGuidNew.Append("NEW." + __fieldName);
                                    __refGuidCompare.Append("OLD." + __fieldName + "=" + "NEW." + __fieldName);

                                    // delete
                                    __query.Append("CREATE OR REPLACE FUNCTION public.champ_" + __tableDetails._tableName + "_after_delete() ");
                                    __query.Append("RETURNS trigger AS ");
                                    __query.Append("$BODY$ ");
                                    __query.Append("BEGIN ");
                                    __query.Append("insert into champ_guid (table_code," + __fieldChampGuid.ToString() + ", action_code) values (" + __tableDetails._tableCode.ToString() + "," + __refGuidOld.ToString() + ", 2); ");
                                    __query.Append("RETURN NEW; ");
                                    __query.Append("END; ");
                                    __query.Append("$BODY$ ");
                                    __query.Append("LANGUAGE plpgsql VOLATILE COST 100; ");
                                    __query.Append("ALTER FUNCTION public.champ_" + __tableDetails._tableName + "_after_delete() OWNER TO postgres; ");
                                    //  insert
                                    __query.Append("CREATE OR REPLACE FUNCTION public.champ_" + __tableDetails._tableName + "_after_insert() ");
                                    __query.Append("RETURNS trigger AS ");
                                    __query.Append("$BODY$  ");
                                    __query.Append("BEGIN ");
                                    __query.Append("insert into champ_guid(table_code," + __fieldChampGuid.ToString() + ", action_code) values (" + __tableDetails._tableCode.ToString() + "," + __refGuidNew.ToString() + ",1); ");
                                    __query.Append("RETURN NEW; ");
                                    __query.Append("END;  ");
                                    __query.Append("$BODY$ ");
                                    __query.Append("LANGUAGE plpgsql VOLATILE COST 100; ");
                                    __query.Append("ALTER FUNCTION public.champ_" + __tableDetails._tableName + "_after_insert() ");
                                    __query.Append("OWNER TO postgres; ");
                                    // update
                                    __query.Append("CREATE OR REPLACE FUNCTION public.champ_" + __tableDetails._tableName + "_after_update() ");
                                    __query.Append("RETURNS trigger AS ");
                                    __query.Append("$BODY$  ");
                                    __query.Append("BEGIN ");
                                    __query.Append("IF " + __refGuidCompare.ToString() + " then ");
                                    __query.Append("insert into champ_guid(table_code," + __fieldChampGuid.ToString() + ", action_code) values (" + __tableDetails._tableCode.ToString() + "," + __refGuidNew.ToString() + ",3);  ");
                                    __query.Append("else ");
                                    __query.Append("insert into champ_guid(table_code," + __fieldChampGuid.ToString() + ", action_code) values (" + __tableDetails._tableCode.ToString() + "," + __refGuidOld.ToString() + ",2); ");
                                    __query.Append("insert into champ_guid(table_code," + __fieldChampGuid.ToString() + ", action_code) values (" + __tableDetails._tableCode.ToString() + "," + __refGuidNew.ToString() + ",1); ");
                                    __query.Append("end if; ");
                                    __query.Append("RETURN NEW; ");
                                    __query.Append("END;  ");
                                    __query.Append("$BODY$ ");
                                    __query.Append("LANGUAGE plpgsql VOLATILE COST 100; ");
                                    __query.Append("ALTER FUNCTION public.champ_" + __tableDetails._tableName + "_after_update() OWNER TO postgres; ");
                                    NpgsqlCommand __createTrigger = new NpgsqlCommand(__query.ToString(), __connPostgre);
                                    __createTrigger.ExecuteNonQuery();
                                    __createTrigger.Dispose();
                                }
                                // create trigger
                                {
                                    for (int __create = 0; __create < 3; __create++)
                                    {
                                        string __commandName = "";
                                        switch (__create)
                                        {
                                            case 0: __commandName = "delete"; break;
                                            case 1: __commandName = "insert"; break;
                                            case 2: __commandName = "update"; break;
                                        }
                                        StringBuilder __queryCreateTrigger = new StringBuilder();
                                        __queryCreateTrigger.Append("CREATE TRIGGER champ_" + __tableDetails._tableName + "_after_" + __commandName + "_trigger ");
                                        __queryCreateTrigger.Append("AFTER " + __commandName + " ON public." + __tableDetails._tableName + " ");
                                        __queryCreateTrigger.Append("FOR EACH ROW ");
                                        __queryCreateTrigger.Append("EXECUTE PROCEDURE public.champ_" + __tableDetails._tableName + "_after_" + __commandName + "(); ");
                                        NpgsqlCommand __createTrigger = new NpgsqlCommand(__queryCreateTrigger.ToString(), __connPostgre);
                                        try
                                        {
                                            __createTrigger.ExecuteNonQuery();
                                        }
                                        catch
                                        {
                                        }
                                        __createTrigger.Dispose();
                                    }
                                }
                            }
                        }
                        MessageBox.Show("สำเร็จ");
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                    __connPostgre.Close();
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        private void _autoButton_Click(object sender, EventArgs e)
        {
            try
            {
                this._autoThread.Abort();
            }
            catch
            {

            }
            {
                this._autoThread = new Thread(new ThreadStart(_autoThreadFunction));
                this._autoThread.IsBackground = true;
                this._autoThread.Start();
                this._autoTextBox.Clear();
            }
        }

        void _restartSync(int tableNumber, string tableName)
        {
            _validForm __valid = new _validForm();
            __valid.ShowDialog();
            if (__valid._pass)
            {
                try
                {
                    _routine __routine = new _routine();
                    _global __global = __routine._loadConfig();
                    NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                    __connPostgre.Open();
                    NpgsqlCommand __command = new NpgsqlCommand("insert into champ_guid (table_code,ref_no_1, action_code) (select " + tableNumber.ToString() + ", code,1 from " + tableName + ")", __connPostgre);
                    try
                    {
                        __command.ExecuteNonQuery();
                        MessageBox.Show("สำเร็จ (จะเริ่มทำงานเมื่อกดปุ่มเริ่มโอนข้อมูล)");
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                    __command.Dispose();
                    __connPostgre.Close();
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }
        private void _arCustomerButton_Click(object sender, EventArgs e)
        {
            this._restartSync(2, "ar_customer");
        }

        private void _apSupplierButton_Click(object sender, EventArgs e)
        {
            this._restartSync(1, "ap_supplier");
        }

        private void _itemButton_Click(object sender, EventArgs e)
        {
            this._restartSync(7, "ic_inventory");
        }
    }
}
