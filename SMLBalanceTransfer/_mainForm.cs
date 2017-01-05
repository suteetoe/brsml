using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace SMLBalanceTransfer
{
    public partial class _mainForm : Form
    {
        public _mainForm()
        {
            InitializeComponent();

            //this._build();

            ((Control)this._icTransferTab).Enabled = false;
            ((Control)this._apTransferTab).Enabled = false;
            ((Control)this._arTransferTab).Enabled = false;

            this._sourceProvider.SelectedIndex = 0;
            this._targetProvider.SelectedIndex = 0;

            // set default date
            //this._setDataDate(_g.d.ic_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
            DateTime __today = DateTime.Now;

            this._icBalanceDateBox._dateTime = this._icBalanceDateBox._dateTimeOld = new DateTime(__today.Year, 12, 31);
            this._icBalanceDocDateDateBox._dateTime = this._icBalanceDateBox._dateTimeOld = new DateTime(__today.Year, 12, 31);

            this._apBalanceDateBox._dateTime = this._apBalanceDateBox._dateTimeOld = new DateTime(__today.Year, 12, 31);
            this._apBalanceDocDate._dateTime = this._apBalanceDateBox._dateTimeOld = new DateTime(__today.Year, 12, 31);

            this._arBalanceDateBox._dateTime = this._arBalanceDateBox._dateTimeOld = new DateTime(__today.Year, 12, 31);
            this._arDocDate._dateTime = this._arBalanceDateBox._dateTimeOld = new DateTime(__today.Year, 12, 31);

            this._icBalanceDateBox._refresh();
            this._icBalanceDocDateDateBox._refresh();
            this._apBalanceDateBox._refresh();
            this._apBalanceDocDate._refresh();
            this._arBalanceDateBox._refresh();
            this._arDocDate._refresh();
            this._gridNewItemCode._removeAllEnable(true);

            if (_global._autoLogin)
            {
                ((Control)this._icTransferTab).Enabled = true;
                ((Control)this._apTransferTab).Enabled = true;
                ((Control)this._arTransferTab).Enabled = true;

                _load_erp_option();
                this._build();
                this.tabControl1.SelectedTab = this._icTransferTab;

            }
        }

        void _build()
        {
            string __formatQty = "m0" + _global._item_qty_decimal.ToString();
            string __formatAmountNumber = "m0" + _global._item_amount_decimal.ToString();
            string __format_ic_price = "m0" + _global._item_price_decimal.ToString();

            // icbalancegrid
            this._icBalancGrid._clear();
            this._icBalancGrid._isEdit = false;
            this._icBalancGrid._width_by_persent = true;
            this._icBalancGrid._total_show = true;
            this._icBalancGrid._addColumn("ic_code", 1, 0, 15);
            this._icBalancGrid._addColumn("new_ic_code", 1, 0, 50);
            this._icBalancGrid._addColumn("ic_name", 1, 0, 20);
            this._icBalancGrid._addColumn("unit_code", 1, 0, 20);
            this._icBalancGrid._addColumn("warehouse", 1, 0, 10);
            this._icBalancGrid._addColumn("location", 1, 0, 10);
            this._icBalancGrid._addColumn("balance_qty", 3, 0, 10, false, false, true, false, __formatQty);
            this._icBalancGrid._addColumn("cost", 3, 0, 10, false, false, true, false, __format_ic_price);
            this._icBalancGrid._addColumn("cost_amount", 3, 0, 10, false, false, true, false, __formatAmountNumber);
            this._icBalancGrid._addColumn("stand_value", 3, 0, 10, false, true, true, false, __formatQty);
            this._icBalancGrid._addColumn("divide_value", 3, 0, 10, false, true, true, false, __formatQty);
            this._icBalancGrid._calcPersentWidthToScatter();
            this._icBalancGrid.Invalidate();

            // ap grid
            this._apBalanceGrid1._clear();
            this._apBalanceGrid1._isEdit = false;
            this._apBalanceGrid1._width_by_persent = true;
            this._apBalanceGrid1._addColumn("ap_code", 1, 0, 20);
            this._apBalanceGrid1._addColumn("ap_name", 1, 0, 60);
            this._apBalanceGrid1._addColumn("ap_balance", 3, 0, 20, false, false, true, false, __formatAmountNumber);
            this._apBalanceGrid1._calcPersentWidthToScatter();
            this._apBalanceGrid1.Invalidate();

            // ar grid
            this._arBalanceGrid1._clear();
            this._arBalanceGrid1._isEdit = false;
            this._arBalanceGrid1._width_by_persent = true;
            this._arBalanceGrid1._addColumn("ar_code", 1, 0, 20);
            this._arBalanceGrid1._addColumn("ar_name", 1, 0, 60);
            this._arBalanceGrid1._addColumn("ar_balance", 1, 0, 20, false, false, true, false, __formatAmountNumber);
            this._arBalanceGrid1._calcPersentWidthToScatter();
            this._arBalanceGrid1.Invalidate();

            // ic new code grid
            this._gridNewItemCode._clear();
            this._gridNewItemCode._isEdit = true;
            this._gridNewItemCode._width_by_persent = true;
            this._gridNewItemCode._addColumn("ic_code", 1, 0, 35);
            this._gridNewItemCode._addColumn("new_ic_code", 1, 0, 35);
            //this._gridNewItemCode._addColumn("process_status", 1, 0, 20, false, false, false);
            //this._gridNewItemCode._addColumn("check", 11, 0, 10, false);

            this._gridNewItemCode._calcPersentWidthToScatter();
            this._gridNewItemCode.Invalidate();

            // grid IC Export
            this._gridICExport._clear();
            this._gridICExport._isEdit = false;
            this._gridICExport._width_by_persent = true;
            this._gridICExport._addColumn("ic_code", 1, 0, 30);
            //this._gridICExport._addColumn("new_ic_code", 1, 0, 20);
            this._gridICExport._addColumn("ic_name", 1, 0, 40);
            this._gridICExport._addColumn("ic_unit_standard", 1, 0, 30);
            this._gridICExport._calcPersentWidthToScatter();
            this._gridICExport.Invalidate();

            this._gridTableProcess._clear();
            this._gridTableProcess._isEdit = false;
            this._gridTableProcess._width_by_persent = true;
            this._gridTableProcess._addColumn("tablename", 1, 0, 70);
            this._gridTableProcess._addColumn("fieldname", 1, 0, 0, false, true);
            this._gridTableProcess._addColumn("extrawhere", 1, 0, 0, false, true);
            this._gridTableProcess._addColumn("process_status", 1, 0, 20, false, false, false);
            this._gridTableProcess._addColumn("check", 11, 0, 10, false);

            this._gridTableProcess._calcPersentWidthToScatter();
            this._gridTableProcess.Invalidate();

            this._gridCheckItem._clear();
            this._gridCheckItem._isEdit = true;
            this._gridCheckItem._width_by_persent = true;
            this._gridCheckItem._addColumn("ic_code", 1, 0, 100);

            // set table for change 
            string __tableList = global::SMLBalanceTransfer.Properties.Resources.tablelist;
            DataSet __ds = MyLib._myGlobal._convertStringToDataSet(__tableList);
            this._gridTableProcess._loadFromDataTable(__ds.Tables[0]);
        }

        string _postgresSqlConnString(string host, string port, string user, string pass, string databaseName)
        {
            return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", host, port, user, pass, databaseName);
        }

        string __microsoftSqlConnString(string host, string port, string user, string pass, string databaseName)
        {
            return String.Format("Server={0};uid={1};pwd={2};{3}", host, user, pass, ((databaseName.Length > 0) ? "Database=" + databaseName + ";" : ""));
        }

        void _load_erp_option()
        {
            try
            {
                string __query = "select item_price_decimal,item_qty_decimal,item_amount_decimal from erp_option";

                MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
                MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);

                DataSet __result = __source._query(__query);

                if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                {
                    _global._item_price_decimal = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0]["item_price_decimal"].ToString());
                    _global._item_qty_decimal = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0]["item_qty_decimal"].ToString());
                    _global._item_amount_decimal = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0]["item_amount_decimal"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("กำหนดค่าเริ่มต้นไม่สมบูรณ์");
            }
        }

        int _sourceProviderSelect
        {
            get
            {
                try
                {
                    return _sourceProvider.SelectedIndex;
                }
                catch
                {
                }
                return 0;
            }
        }

        int _targetProviderSelect
        {
            get
            {
                try
                {

                    return _targetProvider.SelectedIndex;
                }
                catch
                {
                }
                return 0;
            }
        }

        private void _sourceDatabaseNameCombobox_DropDown(object sender, EventArgs e)
        {
            try
            {
                this._sourceDatabaseNameCombobox.Items.Clear();
                switch (this._sourceProviderSelect)
                {
                    case 0: // PostgresSql
                        {
                            string __connstring = this._postgresSqlConnString(this._sourceHostTextbox.Text, this._sourcePortTextbox.Text, this._sourceUserTextbox.Text, this._sourcePasswordTextbox.Text, "template1");
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
                                this._sourceDatabaseNameCombobox.Items.Add(__dtsource.Rows[__row]["datname"].ToString());
                            }
                        }
                        break;
                    case 2: // Microsoft SQL
                        {
                            SqlConnection __conn = new SqlConnection(this.__microsoftSqlConnString(this._sourceHostTextbox.Text, this._sourcePortTextbox.Text, this._sourceUserTextbox.Text, this._sourcePasswordTextbox.Text, ""));
                            __conn.Open();
                            SqlCommand __sqlCommand = new SqlCommand();
                            __sqlCommand.Connection = __conn;
                            __sqlCommand.CommandType = CommandType.StoredProcedure;
                            __sqlCommand.CommandText = "sp_databases";
                            SqlDataReader __sqlDR;
                            __sqlDR = __sqlCommand.ExecuteReader();
                            while (__sqlDR.Read())
                            {
                                this._sourceDatabaseNameCombobox.Items.Add(__sqlDR.GetString(0));
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

        private void _targetDatabaseNameCombobox_DropDown(object sender, EventArgs e)
        {
            try
            {
                string __host = (this.checkBox1.Checked == true) ? this._sourceHostTextbox.Text : this._targetHostTextbox.Text;
                string __port = (this.checkBox1.Checked == true) ? this._sourcePortTextbox.Text : this._targetPortTextbox.Text;
                string __user = (this.checkBox1.Checked == true) ? this._sourceUserTextbox.Text : this._targetUserTextbox.Text;
                string __pass = (this.checkBox1.Checked == true) ? this._sourcePasswordTextbox.Text : this._targetPasswordTextbox.Text;
                int __providerSelected = (this.checkBox1.Checked == true) ? this._sourceProviderSelect : this._targetProviderSelect;

                this._targetDatabaseNameCombobox.Items.Clear();
                switch (__providerSelected)
                {
                    case 0: // PostgresSql
                        {
                            string __connstring = this._postgresSqlConnString(__host, __port, __user, __pass, "template1");
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
                                this._targetDatabaseNameCombobox.Items.Add(__dtsource.Rows[__row]["datname"].ToString());
                            }
                        }
                        break;
                    case 2: // Microsoft SQL
                        {
                            SqlConnection __conn = new SqlConnection(this.__microsoftSqlConnString(__host, __port, __user, __pass, ""));
                            __conn.Open();
                            SqlCommand __sqlCommand = new SqlCommand();
                            __sqlCommand.Connection = __conn;
                            __sqlCommand.CommandType = CommandType.StoredProcedure;
                            __sqlCommand.CommandText = "sp_databases";
                            SqlDataReader __sqlDR;
                            __sqlDR = __sqlCommand.ExecuteReader();
                            while (__sqlDR.Read())
                            {
                                this._targetDatabaseNameCombobox.Items.Add(__sqlDR.GetString(0));
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

        private void _sourceProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this._sourceProvider.SelectedIndex)
            {
                case 0:
                    this._sourcePortTextbox.Text = "5432";
                    break;
                case 1:
                    this._sourcePortTextbox.Text = "3306";
                    break;
                case 2:
                    this._sourcePortTextbox.Text = "1433";
                    break;
            }
        }

        private void _targetProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this._sourceProvider.SelectedIndex)
            {
                case 0:
                    this._targetPortTextbox.Text = "5432";
                    break;
                case 1:
                    this._targetPortTextbox.Text = "3306";
                    break;
                case 2:
                    this._targetPortTextbox.Text = "1433";
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool __enable = (this.checkBox1.Checked == true) ? false : true;

            this._targetProvider.Enabled = __enable;
            this._targetHostTextbox.Enabled = __enable;
            this._targetPortTextbox.Enabled = __enable;
            this._targetUserTextbox.Enabled = __enable;
            this._targetPasswordTextbox.Enabled = __enable;
        }

        bool _testConnectSource()
        {
            string __host = this._sourceHostTextbox.Text;
            string __port = this._sourcePortTextbox.Text;
            string __user = this._sourceUserTextbox.Text;
            string __pass = this._sourcePasswordTextbox.Text;

            try
            {
                string __dbName = this._sourceDatabaseNameCombobox.SelectedItem.ToString();

                switch (this._sourceProviderSelect)
                {
                    case 0: // PostgresSql
                        {
                            string __connstring = this._postgresSqlConnString(__host, __port, __user, __pass, __dbName);
                            Npgsql.NpgsqlConnection __conn = new Npgsql.NpgsqlConnection(__connstring);
                            __conn.Open();
                            __conn.Close();
                            return true;
                        }
                    case 2: // Microsoft SQL
                        {
                            SqlConnection __conn = new SqlConnection(this.__microsoftSqlConnString(__host, __port, __user, __pass, __dbName));
                            __conn.Open();
                            __conn.Close();
                            return true;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }

        bool _testConnectTarget()
        {
            string __host = (this.checkBox1.Checked == true) ? this._sourceHostTextbox.Text : this._targetHostTextbox.Text;
            string __port = (this.checkBox1.Checked == true) ? this._sourcePortTextbox.Text : this._targetPortTextbox.Text;
            string __user = (this.checkBox1.Checked == true) ? this._sourceUserTextbox.Text : this._targetUserTextbox.Text;
            string __pass = (this.checkBox1.Checked == true) ? this._sourcePasswordTextbox.Text : this._targetPasswordTextbox.Text;
            int __providerSelected = (this.checkBox1.Checked == true) ? this._sourceProviderSelect : this._targetProviderSelect;

            try
            {
                string __dbName = this._targetDatabaseNameCombobox.SelectedItem.ToString();
                switch (__providerSelected)
                {
                    case 0: // PostgresSql
                        {
                            string __connstring = this._postgresSqlConnString(__host, __port, __user, __pass, __dbName);
                            Npgsql.NpgsqlConnection __conn = new Npgsql.NpgsqlConnection(__connstring);
                            __conn.Open();
                            __conn.Close();
                            return true;
                        }
                    case 2: // Microsoft SQL
                        {
                            SqlConnection __conn = new SqlConnection(this.__microsoftSqlConnString(__host, __port, __user, __pass, __dbName));
                            __conn.Open();
                            __conn.Close();
                            return true;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return false;
        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            if (_testConnectSource() == true && _testConnectTarget() == true)
            {
                MessageBox.Show("Connect Success !!");
                // write global config
                _global._sourceProvider = this._sourceProviderSelect;
                _global._soruceHost = this._sourceHostTextbox.Text;
                _global._sourcePort = this._sourcePortTextbox.Text;
                _global._sourceUser = this._sourceUserTextbox.Text;
                _global._sourcePass = this._sourcePasswordTextbox.Text;
                _global._sourceDatabase = this._sourceDatabaseNameCombobox.SelectedItem.ToString();

                _global._targetProvider = (this.checkBox1.Checked == true) ? this._sourceProviderSelect : this._targetProviderSelect;
                _global._targetHost = (this.checkBox1.Checked == true) ? this._sourceHostTextbox.Text : this._targetHostTextbox.Text;
                _global._targetPort = (this.checkBox1.Checked == true) ? this._sourcePortTextbox.Text : this._targetPortTextbox.Text;
                _global._targetUser = (this.checkBox1.Checked == true) ? this._sourceUserTextbox.Text : this._targetUserTextbox.Text;
                _global._targetPass = (this.checkBox1.Checked == true) ? this._sourcePasswordTextbox.Text : this._targetPasswordTextbox.Text;
                _global._targetDatabase = this._targetDatabaseNameCombobox.SelectedItem.ToString();

                ((Control)this._icTransferTab).Enabled = true;
                ((Control)this._apTransferTab).Enabled = true;
                ((Control)this._arTransferTab).Enabled = true;

                _load_erp_option();
                this._build();
                this.tabControl1.SelectedTab = this._icTransferTab;

            }
        }

        public DataSet _sourceQuery(string query)
        {
            DataSet __ds = new DataSet();
            try
            {
                string __dbName = this._sourceDatabaseNameCombobox.SelectedItem.ToString();

                switch (this._sourceProviderSelect)
                {
                    case 0: // PostgresSql
                        {
                            string __connstring = this._postgresSqlConnString(_global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
                            Npgsql.NpgsqlConnection __conn = new Npgsql.NpgsqlConnection(__connstring);
                            __conn.Open();
                            Npgsql.NpgsqlDataAdapter __npAdapter = new Npgsql.NpgsqlDataAdapter();
                            __npAdapter.SelectCommand = new Npgsql.NpgsqlCommand(query, __conn);
                            DataSet __dset = new DataSet();
                            __npAdapter.Fill(__ds);

                            __npAdapter.Dispose();
                            __conn.Close();
                        }
                        break;
                    case 2: // Microsoft SQL
                        {
                            SqlConnection __conn = new SqlConnection(this.__microsoftSqlConnString(_global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase));
                            __conn.Open();

                            SqlDataAdapter __sqlAdapter = new SqlDataAdapter();
                            __sqlAdapter.SelectCommand = new SqlCommand(query, __conn);
                            __sqlAdapter.Fill(__ds);


                            __sqlAdapter.Dispose();
                            __conn.Close();

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return __ds;
        }

        private void _icBalanceLoadButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
            MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);

            MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
            MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

            __source._connectionTimeOut = (60 * 240);

            // update before
            __source._queryInsertOrUpdate("update ic_trans_detail set inquiry_type = 0 where inquiry_type is null");
            __source._queryInsertOrUpdate("update ic_trans_detail set last_status = 0 where last_status is null");

            // load balance qty
            string __balanceDate = this._icBalanceDateBox._textQuery();
            // select ic_code as "ic_resource.ic_code",ic_name as "ic_resource.ic_name",ic_unit_code as "ic_resource.ic_unit_code",balance_qty as "ic_resource.balance_qty",average_cost_end as "ic_resource.average_cost_end",balance_amount as "ic_resource.balance_amount" from (select ic_code, ic_name, balance_qty, ic_unit_code, case when balance_qty=0 then 0 else balance_amount/balance_qty end as average_cost, coalesce(((select average_cost from ic_trans_detail where ic_trans_detail.last_status=0 and ic_trans_detail.item_code=temp2.ic_code and doc_date_calc<='2012-05-07' and ((trans_flag in (66,70,54,60,58,310,12) or (trans_flag=14 and inquiry_type=0) or (trans_flag=48 and inquiry_type < 2)) or (trans_flag in (56,72,16,44) or (trans_flag=46 and inquiry_type=1) or (trans_flag=48 and inquiry_type in (0,1)) or (trans_flag=16 and inquiry_type in (0,2)) or (trans_flag=311 and inquiry_type=0))) order by doc_date_calc desc,doc_time desc ,line_number desc  offset 0 limit 1 )*unit_ratio),0) as average_cost_end, balance_amount, qty_in, amount_in, case when qty_in=0 then 0 else amount_in/qty_in end as average_cost_in, qty_out, amount_out, case when qty_out=0 then 0 else amount_out/qty_out end as average_cost_out from (select ic_code, ic_name, balance_qty, ic_unit_code, (select unit_standard_stand_value/unit_standard_divide_value from ic_inventory where ic_inventory.code=temp1.ic_code) as unit_ratio, balance_amount, qty_in, amount_in, qty_out, amount_out from (select item_code as ic_code, (select name_1||'('||coalesce((select barcode from ic_inventory_barcode where ic_inventory_barcode.ic_code=ic_inventory.code and ic_inventory_barcode.unit_code=ic_inventory.unit_standard limit 1),'')||')' from ic_inventory where ic_inventory.code=item_code) as ic_name, (select unit_standard from ic_inventory where ic_inventory.code=item_code) as ic_unit_code, coalesce(sum(calc_flag*(case when ((trans_flag in (66,70,54,60,58,310,12) or (trans_flag=14 and inquiry_type=0) or (trans_flag=48 and inquiry_type < 2)) or (trans_flag in (56,72,16,44) or (trans_flag=46 and inquiry_type=1) or (trans_flag=48 and inquiry_type in (0,1)) or (trans_flag=16 and inquiry_type in (0,2)) or (trans_flag=311 and inquiry_type=0))) then qty*(stand_value / divide_value) else 0 end)),0) as balance_qty, coalesce(sum(calc_flag*(case when ((trans_flag in (66,70,54,60,58,310,12,1448)) or (trans_flag in (56,72,16,311,44,46))) then sum_of_cost else 0 end)),0) as balance_amount, 0 as qty_in,0 as amount_in, 0 as qty_out,0 as amount_out from ic_trans_detail where ic_trans_detail.last_status=0 and ic_trans_detail.item_type<>5 and doc_date_calc<='2012-05-07' group by item_code) as temp1) as temp2  where (balance_qty<>0 or balance_amount<>0)) as temp9 where ic_code<>'' order by "ic_resource.ic_code"

            StringBuilder __query = new StringBuilder();
            __query.Append("select item_code as ic_code ");
            __query.Append(",(select name_1 from ic_inventory where ic_inventory.code=item_code) as ic_name");
            __query.Append(", (select unit_standard from ic_inventory where ic_inventory.code=item_code) as unit_code");
            //__query.Append(", (select " + _global._getLimitQueryString(__sourceType, 1)[1].ToString() + " x1.average_cost from ic_trans_detail as x1 where x1.item_code = ic_trans_detail.item_code and x1.trans_flag in (12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73) and doc_date <= " + __balanceDate + " order by x1.doc_date desc, x1.doc_time desc " + _global._getLimitQueryString(__sourceType, 1)[0].ToString() + " ) as cost");
            __query.Append(", {0} "); // replace เป็น {0} __query.Append(", wh_code as warehouse, shelf_code as location"); 
            //__query.Append(", coalesce((select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=(select unit_standard from ic_inventory where ic_inventory.code=item_code)),1) as unit_standard_ratio");
            __query.Append(", coalesce(sum(calc_flag*(case when (" + _g._icInfoFlag._allFlagQty + ") then qty*(stand_value / divide_value) else 0 end)),0) as balance_qty");
            __query.Append(", coalesce(sum(" + _g.d.ic_trans_detail._calc_flag + "*(case when " + _g._icInfoFlag._allFlagAmount + " then case when trans_flag = 66 and qty < 0 then -1*" + _g.d.ic_trans_detail._sum_of_cost + " else " + _g.d.ic_trans_detail._sum_of_cost + " end else 0 end)),0) as " + _g.d.ic_resource._balance_amount);
            __query.Append(" from ic_trans_detail ");
            __query.Append(" where ");
            __query.Append(" ic_trans_detail.last_status=0 ");
            __query.Append(" and ic_trans_detail.item_type<>5 ");
            __query.Append(" and doc_date_calc<=" + __balanceDate + " ");
            __query.Append(" group by {1} "); // replace {1} __query.Append(" group by item_code, wh_code, shelf_code "); 
            //__query.Append(" order by item_code, wh_code, shelf_code ");

            StringBuilder __query2 = new StringBuilder();
            __query2.Append("select ic_code, ic_name, unit_code");
            __query2.Append(",{2}"); //__query2.Append(",warehouse, location");
            __query2.Append(",balance_qty");
            __query2.Append(", case when balance_qty=0 then 0 else balance_amount end as balance_amount");
            __query2.Append(",case when balance_qty=0 then 0 else balance_amount/balance_qty end as average_cost");
            __query2.Append(",coalesce((select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=temp1.ic_code and ic_unit_use.code=temp1.unit_code),1) as unit_standard_ratio");
            __query2.Append(" from (" + __query.ToString() + ") as temp1 ");


            StringBuilder __query3 = new StringBuilder();
            __query3.Append("select ");
            __query3.Append(" temp3.ic_code, temp3.ic_name, temp3.unit_code, temp3.warehouse, temp3.location");
            __query3.Append(", (temp3.balance_qty/temp3.unit_standard_ratio) as balance_qty");
            __query3.Append(", case when (temp3.unit_standard_ratio= 0 ) then 0 else item_cost.average_cost*temp3.unit_standard_ratio end as cost");
            //__query3.Append(", coalesce(((temp3.balance_qty/temp3.unit_standard_ratio) * (temp3.average_cost*temp3.unit_standard_ratio)), 0) as cost_amount");
            __query3.Append(", temp3.balance_amount as cost_amount_2");
            __query3.Append(", case when temp3.balance_qty=0 then 0 else temp3.balance_amount/temp3.balance_qty end as average_cost");
            __query3.Append(", (select coalesce(stand_value , 1) from ic_unit_use where ic_unit_use.code = temp3.unit_code and ic_unit_use.ic_code = temp3.ic_code ) as stand_value ");
            __query3.Append(", (select coalesce(divide_value , 1) from ic_unit_use where ic_unit_use.code = temp3.unit_code and ic_unit_use.ic_code = temp3.ic_code ) as divide_value ");
            __query3.Append(" from (");
            __query3.Append(string.Format(__query2.ToString(), "wh_code as warehouse, shelf_code as location ", " item_code, wh_code, shelf_code", "warehouse, location"));
            __query3.Append(") as temp3 ");


            // ดึงต้นทุนเข้ามา
            __query3.Append(" LEFT JOIN ");
            __query3.Append(" ( " + string.Format(__query2.ToString(), "wh_code as warehouse ", " item_code, wh_code ", "warehouse ") + " ) as item_cost ");
            __query3.Append(" ON ");
            __query3.Append(" temp3.ic_code = item_cost.ic_code AND temp3.warehouse = item_cost.warehouse ");


            __query3.Append(" where temp3.balance_qty <> 0 order by temp3.ic_code, temp3.warehouse, temp3.location");

            // wh_code as warehouse, shelf_code as location from ic_trans_detail where ");

            //__query = "select * from sml_ic_function_stock_balance_warehouse_location(" + __balanceDate + ", '', '', '')";
            StringBuilder __query4 = new StringBuilder();
            __query4.Append("select ");
            __query4.Append("ic_code, ic_name, unit_code, warehouse, location");
            __query4.Append(", balance_qty, case when( cost = 0 ) then 0 else cost end as cost ");
            __query4.Append(", case when cost = 0 then 0 else (balance_qty * cost) end as cost_amount ");
            __query4.Append(", cost_amount_2, average_cost, stand_value, divide_value");
            __query4.Append(" from (" + __query3.ToString() + ") as temp4 where ic_code not in ('99') ");

            string __queryFinalStr = __query4.ToString();

            DataSet __result = __source._query(__queryFinalStr);
            if(__source._lastErrorMeaages.Length > 0)
            {
                MessageBox.Show(__source._lastErrorMeaages);
            }
            if (__result.Tables.Count > 0)
            {
                this._icBalancGrid._loadFromDataTable(__result.Tables[0]);
            }

            // load เอกสาร format
            /* old
            string __queryDocFormat = "select code, format from erp_doc_format where screen_code = 'IB' ";
            DataSet __docformatResult = __target._query(__queryDocFormat);
            if (__docformatResult.Tables.Count > 0 && __docformatResult.Tables[0].Rows.Count > 0)
            {
                string __getDocFormat = __docformatResult.Tables[0].Rows[0]["format"].ToString();
                string __getDocFormatCode = __docformatResult.Tables[0].Rows[0]["code"].ToString();
                DateTime __getDocDate = _icBalanceDocDateDateBox._dateTime;
                string __where = " trans_flag = 54 ";

                string __getDocNo = _getAutoRun(__target, "ic_trans", "doc_no", __where, __getDocFormatCode, __getDocDate, __getDocFormat);
                this._icdocNoTextbox.Text = __getDocNo;
                this._icDocFormat.Text = __getDocFormatCode;
            }*/
            string __getDocFormat = "@-YYMM-####";
            string __getDocFormatCode = "IB";
            DateTime __getDocDate = _icBalanceDocDateDateBox._dateTime;
            string __where = " trans_flag = 54 ";


            string __getDocNo = _getAutoRun(__target, "ic_trans", "doc_no", __where, __getDocFormatCode, __getDocDate, __getDocFormat);
            this._icdocNoTextbox.Text = __getDocNo;
            this._icDocFormat.Text = __getDocFormatCode;

        }

        private void _apBalanceLoadButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
            MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);

            MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
            MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

            string __balanceDate = this._apBalanceDateBox._textQuery();
            StringBuilder __query = new StringBuilder();

            StringBuilder __queryPurchase = new StringBuilder();
            __queryPurchase.Append(" select ");
            __queryPurchase.Append(" cust_code,doc_date,credit_date as due_date,doc_no,trans_flag as doc_type,used_status,doc_ref as ref_doc_no,doc_ref_date as ref_doc_date,coalesce(total_amount,0) as amount ");
            __queryPurchase.Append(", coalesce(total_amount,0)-(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (19) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date and doc_date <= date(" + __balanceDate + ")) as balance_amount ");
            __queryPurchase.Append(" from ic_trans ");
            __queryPurchase.Append(" where ");
            __queryPurchase.Append(" coalesce(last_status, 0)=0  ");
            __queryPurchase.Append(" and trans_flag in (12) and (inquiry_type=0 or inquiry_type=2)   ");
            __queryPurchase.Append(" and doc_date <= date(" + __balanceDate + ")  ");

            StringBuilder __queryCN = new StringBuilder();
            __queryCN.Append("select ");
            __queryCN.Append("cust_code,doc_date,credit_date as due_date,doc_no,trans_flag as doc_type,used_status,'' as ref_doc_no,null as ref_doc_date,coalesce(total_amount,0) as amount");
            __queryCN.Append(", coalesce(total_amount,0)-(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (19) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date and doc_date <= date(" + __balanceDate + ")) as balance_amount ");
            __queryCN.Append(" from ic_trans ");
            __queryCN.Append(" where ");
            __queryCN.Append(" coalesce(last_status, 0)=0 ");
            __queryCN.Append(" and (trans_flag=14 or trans_flag=315 or trans_flag=316 or trans_flag=81 or trans_flag=87 or trans_flag=83 or trans_flag=89) ");
            __queryCN.Append(" and doc_date <= date(" + __balanceDate + ") ");

            StringBuilder __queryReturn = new StringBuilder();
            __queryReturn.Append("select ");
            __queryReturn.Append("cust_code,doc_date,credit_date as due_date,doc_no,trans_flag as doc_type,used_status,'' as ref_doc_no,null as ref_doc_date,-1*coalesce(total_amount,0) as amount ");
            __queryReturn.Append(", -1*(coalesce(total_amount,0)+(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (19) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date and doc_date <= date(" + __balanceDate + "))) as balance_amount ");
            __queryReturn.Append(" from ic_trans ");
            __queryReturn.Append(" where ");
            __queryReturn.Append(" coalesce(last_status, 0)=0  ");
            __queryReturn.Append(" and (trans_flag=16 or trans_flag=317 or trans_flag=85 or trans_flag=91)  ");
            __queryReturn.Append(" and doc_date <= date(" + __balanceDate + ")  ");


            __query.Append("select ");
            __query.Append(" cust_code as ap_code , (select name_1 from ap_supplier where ap_supplier.code=cust_code) as ap_name , sum(balance_amount) as ap_balance");
            __query.Append(" from (");
            __query.Append(__queryPurchase.ToString());
            __query.Append(" union all  ");
            __query.Append(__queryCN.ToString());
            __query.Append(" union all ");
            __query.Append(__queryReturn.ToString());
            __query.Append(") as temp2 ");
            __query.Append(" group by cust_code ");

            StringBuilder __query2 = new StringBuilder();
            __query2.Append(" select ap_code, ap_name, ap_balance");
            __query2.Append(" from (");
            __query2.Append(__query.ToString());
            __query2.Append(" ) as temp4 ");
            __query2.Append(" where ap_balance <> 0 order by ap_code ");

            DataSet __result = __source._query(__query2.ToString());
            if (__result.Tables.Count > 0)
            {
                this._apBalanceGrid1._loadFromDataTable(__result.Tables[0]);
            }

            // load เอกสาร format
            /*
            string __queryDocFormat = "select code, format from erp_doc_format where screen_code = 'DA' ";
            DataSet __docformatResult = __target._query(__queryDocFormat);
            if (__docformatResult.Tables.Count > 0 && __docformatResult.Tables[0].Rows.Count > 0)
            {
                string __getDocFormat = __docformatResult.Tables[0].Rows[0]["format"].ToString();
                string __getDocFormatCode = __docformatResult.Tables[0].Rows[0]["code"].ToString();
                DateTime __getDocDate = _apBalanceDocDate._dateTime;
                string __where = " trans_flag = 81 ";

                string __getDocNo = _getAutoRun(__target, "ic_trans", "doc_no", __where, __getDocFormatCode, __getDocDate, __getDocFormat);
                this._apBalanceDocFormat.Text = __getDocNo;
                this._apBalanceDocFormatCode.Text = __getDocFormatCode;
            }*/
            string __getDocFormat = "@-YYMM-####";
            string __getDocFormatCode = "APB";
            DateTime __getDocDate = _apBalanceDocDate._dateTime;
            string __where = " trans_flag = 81 ";

            string __getDocNo = _getAutoRun(__target, "ic_trans", "doc_no", __where, __getDocFormatCode, __getDocDate, __getDocFormat);
            this._apBalanceDocFormat.Text = __getDocNo;
            this._apBalanceDocFormatCode.Text = __getDocFormatCode;
        }

        private void _arBalanceLoadButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
            MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);

            MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
            MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

            string __balanceDate = this._arBalanceDateBox._textQuery();
            StringBuilder __query = new StringBuilder();

            StringBuilder __querySale = new StringBuilder();
            __querySale.Append("select ");
            __querySale.Append("cust_code");
            __querySale.Append(", doc_date ");
            __querySale.Append(", credit_date as due_date ");
            __querySale.Append(", doc_no ");
            __querySale.Append(", trans_flag as doc_type ");
            __querySale.Append(", used_status ");
            __querySale.Append(", doc_ref as ref_doc_no ");
            __querySale.Append(", doc_ref_date as ref_doc_date ");
            __querySale.Append(", coalesce(total_amount,0) as amount ");
            __querySale.Append(", coalesce(total_amount,0)-(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (239) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date and doc_date <= date(" + __balanceDate + ")) as balance_amount ");
            __querySale.Append(" from ic_trans ");
            __querySale.Append(" where ");
            __querySale.Append(" coalesce(last_status, 0)=0 ");
            __querySale.Append(" and trans_flag=44 and (inquiry_type=0  or inquiry_type=2) ");
            __querySale.Append(" and doc_date <= date(" + __balanceDate + ")  ");

            StringBuilder __queryDebitNote = new StringBuilder();
            __queryDebitNote.Append("select ");
            __queryDebitNote.Append("cust_code ");
            __queryDebitNote.Append(", doc_date ");
            __queryDebitNote.Append(", credit_date as due_date ");
            __queryDebitNote.Append(", doc_no ");
            __queryDebitNote.Append(", trans_flag as doc_type ");
            __queryDebitNote.Append(", used_status ");
            __queryDebitNote.Append(", '' as ref_doc_no ");
            __queryDebitNote.Append(", null as ref_doc_date ");
            __queryDebitNote.Append(", coalesce(total_amount,0) as amount ");
            __queryDebitNote.Append(", coalesce(total_amount,0)-(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (239) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date and doc_date <= date(" + __balanceDate + ")) as balance_amount  ");
            __queryDebitNote.Append("from ic_trans ");
            __queryDebitNote.Append(" where ");
            __queryDebitNote.Append(" coalesce(last_status, 0)=0  ");
            __queryDebitNote.Append(" and (trans_flag=46 or trans_flag=93 or trans_flag=99 or trans_flag=95 or trans_flag=101) ");
            __queryDebitNote.Append(" and doc_date <= date(" + __balanceDate + ")  ");

            StringBuilder __queryReturn = new StringBuilder();
            __queryReturn.Append("select ");
            __queryReturn.Append("cust_code ");
            __queryReturn.Append(", doc_date ");
            __queryReturn.Append(", credit_date as due_date ");
            __queryReturn.Append(", doc_no ");
            __queryReturn.Append(", trans_flag as doc_type ");
            __queryReturn.Append(", used_status ");
            __queryReturn.Append(", '' as ref_doc_no ");
            __queryReturn.Append(", null as ref_doc_date ");
            __queryReturn.Append(", -1*coalesce(total_amount,0) as amount ");
            __queryReturn.Append(", -1*(coalesce(total_amount,0)+(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (239) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date and doc_date <= date(" + __balanceDate + "))) as balance_amount ");
            __queryReturn.Append(" from ic_trans ");
            __queryReturn.Append(" where ");
            __queryReturn.Append(" coalesce(last_status, 0)=0  ");
            __queryReturn.Append(" and ((trans_flag=48 and inquiry_type in (0,2,4) ) or trans_flag=97 or trans_flag=103) ");
            __queryReturn.Append(" and doc_date <= date(" + __balanceDate + ") ");

            __query.Append(" select ");
            __query.Append(" cust_code as ar_code ");
            __query.Append(", (select name_1 from ar_customer where ar_customer.code=cust_code) as ar_name ");
            __query.Append(", sum(balance_amount) as ar_balance ");
            __query.Append(" from ( ");
            __query.Append(__querySale.ToString());
            __query.Append(" union all ");
            __query.Append(__queryDebitNote.ToString());
            __query.Append(" union all ");
            __query.Append(__queryReturn.ToString());
            __query.Append(" ) as temp2 ");
            __query.Append(" group by cust_code ");

            StringBuilder __query2 = new StringBuilder();
            __query2.Append(" select ar_code, ar_name, ar_balance");
            __query2.Append(" from (");
            __query2.Append(__query.ToString());
            __query2.Append(" ) as temp4 ");
            __query2.Append(" where temp4.ar_balance <> 0 order by ar_code ");

            DataSet __result = __source._query(__query2.ToString());
            if (__result.Tables.Count > 0)
            {
                this._arBalanceGrid1._loadFromDataTable(__result.Tables[0]);
            }

            // load เอกสาร format
            /*
            string __queryDocFormat = "select code, format from erp_doc_format where screen_code = 'EA' ";
            DataSet __docformatResult = __target._query(__queryDocFormat);
            if (__docformatResult.Tables.Count > 0 && __docformatResult.Tables[0].Rows.Count > 0)
            {
                string __getDocFormat = __docformatResult.Tables[0].Rows[0]["format"].ToString();
                string __getDocFormatCode = __docformatResult.Tables[0].Rows[0]["code"].ToString();
                DateTime __getDocDate = _arDocDate._dateTime;
                string __where = " trans_flag = 93 ";
                string __getDocNo = _getAutoRun(__target, "ic_trans", "doc_no", __where, __getDocFormatCode, __getDocDate, __getDocFormat);
                this._arDocFormat.Text = __getDocNo;
                this._arDocFormatCode.Text = __getDocFormatCode;
            }*/
            string __getDocFormat = "@-YYMM-####";
            string __getDocFormatCode = "ARB";
            DateTime __getDocDate = _arDocDate._dateTime;
            string __where = " trans_flag = 93 ";
            string __getDocNo = _getAutoRun(__target, "ic_trans", "doc_no", __where, __getDocFormatCode, __getDocDate, __getDocFormat);
            this._arDocFormat.Text = __getDocNo;
            this._arDocFormatCode.Text = __getDocFormatCode;

        }

        private void _exportICButton_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("Export Inventory Data ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                /*
                // step สร้าง ตาราง map สินค้า
                // แล้ว insert รหัสใหม่
                MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);
                MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

                // create table for map new ic_code
                StringBuilder __script = new StringBuilder();
                __script.Append("CREATE TABLE map_new_iccode_temp\n ");
                __script.Append("(\n ");
                __script.Append("  old_code character varying(30) NOT NULL,\n ");
                __script.Append("  new_code character varying(30),\n ");
                __script.Append(")\n ");
                __script.Append("WITH (\n ");
                __script.Append("  OIDS=FALSE\n ");
                __script.Append(");\n ");
                
                string __result = __target._queryInsertOrUpdate(__script.ToString());

                __target._queryInsertOrUpdate("truncate map_new_iccode_temp");

                // insert into 
                List<string> __queryInsertList = new List<string>();
                for (int __i = 0; __i < this._gridNewItemCode._rowData.Count; __i++)
                {
                    string __oldCode = this._gridNewItemCode._cellGet(__i, "ic_code").ToString();
                    string __newCode = this._gridNewItemCode._cellGet(__i, "new_ic_code").ToString();
                    if (__oldCode != "" && __newCode != "")
                    {
                        string __query = "insert into map_new_iccode_temp (old_code,new_code) values (\'" + __oldCode + "\', \'" + __newCode + "\')";
                        __queryInsertList.Add(__query);
                    }

                }

                if (__queryInsertList.Count > 0)
                {
                    __result = __target._queryInsertOrUpdateList(__queryInsertList);
                }*/

                // โอน MASTER สินค้า 

                // ic_inventory
                _transferTable("ic_inventory"); // , true, "code"

                // ic_inventory_detail
                _transferTable("ic_inventory_detail");

                // ic_inventory_barcode
                _transferTable("ic_inventory_barcode");

                // ic_unit
                _transferTable("ic_unit");

                // ic_unit
                _transferTable("ic_unit_use");

                // ic_inventory_set
                _transferTable("ic_inventory_set");

                // ic_inventory_set_detail
                _transferTable("ic_inventory_set_detail");

                _transferTable("ic_warehouse");

                _transferTable("ic_shelf");

                _transferTable("ic_wh_shelf");

                _transferTable("ic_brand");

                _transferTable(_g.d.ic_inventory_price._table);


                MessageBox.Show("Export Inventory Data Success", "success");
            }
        }

        string _transferTable(string tableName)
        {
            return _transferTable(tableName, false, "");
        }

        string _transferTable(string tableName, Boolean checkNewCode, string oldCodeFieldName)
        {
            return _transferTable(tableName, checkNewCode, oldCodeFieldName, "");
        }

        string _transferTable(string tableName, Boolean checkNewCode, string oldCodeFieldName, string extraWhere)
        {
            string __result = "";

            MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
            MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);

            MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
            MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

            // get server field 
            List<MyLib._databaseColumn> __sourceColumnList = __source._getField(tableName);

            // get client field
            List<MyLib._databaseColumn> __targetColumnList = __target._getField(tableName);

            // compare
            int __loop1 = 0;
            while (__loop1 < __sourceColumnList.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __targetColumnList.Count && __found == false; __loop2++)
                {
                    if (__sourceColumnList[__loop1]._columnName.Equals(__targetColumnList[__loop2]._columnName))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __sourceColumnList.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }
            // หาใน field client ถ้าไม่มีใน field server ให้ลบ field client ออก
            __loop1 = 0;
            while (__loop1 < __targetColumnList.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __sourceColumnList.Count && __found == false; __loop2++)
                {
                    if (__targetColumnList[__loop1]._columnName.Equals(__sourceColumnList[__loop2]._columnName))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __targetColumnList.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }

            StringBuilder __fieldForInsert = new StringBuilder();
            StringBuilder __fieldForSelect = new StringBuilder();

            StringBuilder __fieldForServerSelect = new StringBuilder(); // ใส่ default value มาแล้ว

            // toe เพิ่ม default value ต้อง แยก select ระหว่าง client และ center
            for (int __loop = 0; __loop < __sourceColumnList.Count; __loop++)
            {
                if (__fieldForSelect.Length > 0)
                {
                    __fieldForSelect.Append(",");
                    __fieldForInsert.Append(",");
                }
                __fieldForSelect.Append(__sourceColumnList[__loop]._columnName.ToString());
                __fieldForInsert.Append(__sourceColumnList[__loop]._columnName.ToString());
            }

            // select ทีละ 5000 รายการได้มั้ย

            // start copy
            //DataSet __rsCount = __source._query("select count(*) as xcount from " + tableName);
            //if (__rsCount != null)
            //{
            //    int __count =  (int) MyLib._myGlobal._decimalPhase(__rsCount.Tables[0].Rows[0][0].ToString());
            //}

            // concept select มา แต่เอาเข้าทีละ 5000 รายการ

            if (__fieldForSelect.Length > 0)
            {
                string __selectSourceString = "select " + __fieldForSelect.ToString() + " from " + tableName + ((extraWhere.Length > 0) ? " where " + extraWhere : "");

                int __count = 0;
                Npgsql.NpgsqlConnection __postgresqlConn = null;
                Npgsql.NpgsqlCommand __postgresqlCommand = null;
                Npgsql.NpgsqlDataReader __postgresqlReader = null;
                SqlConnection __sqlConnection = null;
                SqlCommand __sqlCommand = null;
                SqlDataReader __sqlReader = null;
                switch (__source._type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        {
                            string __connstring = this._postgresSqlConnString(_global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
                            __postgresqlConn = new Npgsql.NpgsqlConnection(__connstring);
                            __postgresqlConn.Open();

                            __postgresqlCommand = new Npgsql.NpgsqlCommand(__selectSourceString, __postgresqlConn);
                            __postgresqlReader = __postgresqlCommand.ExecuteReader();
                        }
                        break;
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                        {
                            string __connString = __microsoftSqlConnString(_global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
                            __sqlConnection = new SqlConnection(__connString);
                            __sqlCommand = new SqlCommand(__selectSourceString, __sqlConnection);
                            __sqlReader = __sqlCommand.ExecuteReader();
                        }
                        break;
                }

                // get 
                List<string> __queryInsert = new List<string>();
                while (true)
                {
                    Boolean __read = false;
                    switch (__source._type)
                    {
                        case MyLib._myGlobal._databaseType.PostgreSql:
                            __read = __postgresqlReader.Read();
                            break;
                        case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                        case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                            __read = __sqlReader.Read();
                            break;
                    }
                    if (__read == false)
                    {
                        break;
                    }

                    // compare and packquery
                    StringBuilder __data = new StringBuilder();
                    for (int __fieldScan = 0; __fieldScan < __sourceColumnList.Count; __fieldScan++)
                    {
                        if (__data.Length != 0)
                        {
                            __data.Append(",");
                        }
                        int __findTypeAddr = __fieldScan;
                        for (int __loop = 0; __loop < __targetColumnList.Count; __loop++)
                        {
                            if (__targetColumnList[__loop]._columnName.ToString().Trim().ToUpper().Equals(__targetColumnList[__fieldScan]._columnName.ToString().ToUpper().Trim()))
                            {
                                __findTypeAddr = __loop;
                                break;
                            }
                        }

                        // check new ic code column

                        string __getData = "";
                        byte[] __getByte = null;

                        switch (__source._type)
                        {
                            case MyLib._myGlobal._databaseType.PostgreSql:
                                if (__sourceColumnList[__fieldScan]._columnType == MyLib._databaseColumnType.Image)
                                {
                                    try
                                    {
                                        __getByte = (byte[])__postgresqlReader.GetValue(__fieldScan);
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    __getData = __postgresqlReader[__fieldScan].ToString().Replace("\'", "\'\'");
                                }
                                break;
                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                if (__sourceColumnList[__fieldScan]._columnType == MyLib._databaseColumnType.Image)
                                {
                                    __getByte = (byte[])__sqlReader.GetValue(__fieldScan);
                                }
                                else
                                {
                                    __getData = __sqlReader[__fieldScan].ToString().Replace("\'", "\'\'");
                                }
                                break;
                        }
                        if (__sourceColumnList[__findTypeAddr]._isNull && (__sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.Datetime || __sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.String) && __getData.ToString().Trim().Length == 0)
                        {
                            // กรณีวันที่
                            if (__sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.Datetime)
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
                            if (__sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.Datetime || __sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.String)
                            {
                                __data.Append("\'");
                            }
                            // กรณีวันที่
                            if (__sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.Datetime)
                            {
                                __getData = MyLib._myGlobal._convertDateToQuery(this._convertDateFromQuery(__getData));
                            }
                            else
                            {
                                // กรณีเป็นตัวเลข ให้เป็น 0
                                if (__sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.Number && __getData.Length == 0)
                                {
                                    __getData = "0";
                                }
                            }
                            if (__sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.Image)
                            {
                                if (__getByte != null)
                                {
                                    __getData = "decode('" + Convert.ToBase64String(__getByte) + "','base64')";
                                }
                            }
                            __data.Append(__getData);
                            if (__sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.Datetime || __sourceColumnList[__findTypeAddr]._columnType == MyLib._databaseColumnType.String)
                            {
                                __data.Append("\'");
                            }
                        }
                    }

                    __queryInsert.Add("insert into " + tableName + " (" + __fieldForInsert.ToString() + ") values (" + __data.ToString() + ")");
                    if (++__count == 6000 || __queryInsert.Count > 100000)
                    {
                        string __resultInsert = "";
                        try
                        {
                            __resultInsert = __target._queryInsertOrUpdateList(__queryInsert);
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString());
                        }
                        if (__resultInsert.Length > 0)
                        {
                            MessageBox.Show(__resultInsert);
                            break;
                        }
                        __queryInsert = new List<string>();
                        __count = 0;
                    }
                }

                if (__queryInsert.Count > 0)
                {
                    string __resultInsert = "";
                    try
                    {
                        __resultInsert = __target._queryInsertOrUpdateList(__queryInsert);
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                    if (__resultInsert.Length > 0)
                    {
                        MessageBox.Show(__resultInsert);
                    }
                }
                switch (__source._type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        __postgresqlReader.Close();
                        __postgresqlCommand.Dispose();
                        break;
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                        __sqlReader.Close();
                        __sqlCommand.Dispose();
                        break;
                }
            }

            return __result;
        }

        public DateTime _convertDateFromQuery(string date)
        {
            DateTime __result = new DateTime(1979, 3, 25);
            try
            {
                if (date.Length > 0)
                {
                    IFormatProvider __culture = new CultureInfo("th-TH");
                    __result = DateTime.Parse(date, __culture);
                }
            }
            catch
            {
                // Debugger.Break();
            }
            return (__result);
        }

        private MyLib._myGlobal._databaseType _getProviderFromIndex(int index)
        {
            switch (index)
            {
                case 1:
                    return MyLib._myGlobal._databaseType.MySql;
                case 2:
                    return MyLib._myGlobal._databaseType.MicrosoftSQL2005;
            }
            return MyLib._myGlobal._databaseType.PostgreSql;
        }

        private void _toolStripMapIcCode_Click(object sender, EventArgs e)
        {
            MyLib._myGridImportFromTextFileForm __form = new MyLib._myGridImportFromTextFileForm(this._gridNewItemCode._columnList);
            __form._importButton.Click += (s1, e1) =>
            {
                __form.Close();
                for (int __row1 = 0; __row1 < __form._dataGridView.Rows.Count; __row1++)
                {

                    try
                    {
                        int __addrRow = -1;
                        for (int __row2 = 0; __row2 < __form._mapFieldView.Rows.Count; __row2++)
                        {
                            string __name = __form._mapFieldView.Rows[__row2].Cells[0].Value.ToString();
                            string __field = (__form._mapFieldView.Rows[__row2].Cells[1].Value == null) ? "" : __form._mapFieldView.Rows[__row2].Cells[1].Value.ToString();
                            if (__field.Trim().Length > 0)
                            {
                                int __columnNumber = -1;
                                for (int __loop = 0; __loop < __form._dataGridView.Columns.Count; __loop++)
                                {
                                    if (__form._dataGridView.Columns[__loop].Name.Equals(__field))
                                    {
                                        __columnNumber = __loop;
                                        break;
                                    }
                                }
                                if (__columnNumber != -1)
                                {
                                    string __value = __form._dataGridView.Rows[__row1].Cells[__columnNumber].Value.ToString();
                                    if (__addrRow == -1)
                                    {
                                        __addrRow = this._gridNewItemCode._addRow();
                                    }
                                    int __gridColumnNumber = -1;
                                    MyLib._myGrid._columnType __myColumn = null;
                                    for (int __column = 0; __column < this._gridNewItemCode._columnList.Count; __column++)
                                    {
                                        __myColumn = (MyLib._myGrid._columnType)this._gridNewItemCode._columnList[__column];
                                        if (__myColumn._name.Equals(__name))
                                        {
                                            __gridColumnNumber = __column;
                                            break;
                                        }
                                    }
                                    if (__myColumn != null && __gridColumnNumber != -1)
                                    {
                                        switch (__myColumn._type)
                                        {
                                            case 1: this._gridNewItemCode._cellUpdate(__addrRow, __gridColumnNumber, __value, false); break;
                                            case 2:
                                            case 3: this._gridNewItemCode._cellUpdate(__addrRow, __gridColumnNumber, MyLib._myGlobal._decimalPhase(__value), false); break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                }

                // compare _gridNewItemCode , gridbalance
                for (int __row = 0; __row < this._icBalancGrid._rowData.Count; __row++)
                {
                    string __getItemCode = this._icBalancGrid._cellGet(__row, "ic_code").ToString();

                    for (int __loop = 0; __loop < this._gridNewItemCode._rowData.Count; __loop++)
                    {
                        if (this._gridNewItemCode._cellGet(__loop, "ic_code").ToString().Equals(__getItemCode))
                        {
                            string __newIcCode = this._gridNewItemCode._cellGet(__loop, "new_ic_code").ToString();
                            this._icBalancGrid._cellUpdate(__row, "new_ic_code", __newIcCode, false);
                        }
                    }
                }

            };
            __form.ShowDialog();

        }

        public static string _getAutoRun(MyLib._myDatabaseFrameWork __myFrameWork, string tableName, string fieldName, string quereyWhere, string docNo, DateTime docDate, string format)
        {
            return _getAutoRun(__myFrameWork, tableName, fieldName, quereyWhere, docNo, docDate, format, true, 0);
        }

        public static string _getAutoRun(MyLib._myDatabaseFrameWork __myFrameWork, string tableName, string fieldName, string quereyWhere, string docNo, DateTime docDate, string format, bool _bool, int __running)
        {
            string __getFormat = format;
            string __result = __getFormat;
            if (__getFormat.Length > 0)
            {

                DateTime __date = docDate;
                CultureInfo __dateUSZone = new CultureInfo("en-US");
                CultureInfo __dateTHZone = new CultureInfo("th-TH");
                __getFormat = __getFormat.Replace("DD", __date.ToString("dd", __dateUSZone));
                __getFormat = __getFormat.Replace("MM", __date.ToString("MM", __dateUSZone));
                __getFormat = __getFormat.Replace("YYYY", __date.ToString("yyyy", __dateUSZone));
                __getFormat = __getFormat.Replace("YY", __date.ToString("yy", __dateUSZone));
                __getFormat = __getFormat.Replace("วว", __date.ToString("dd", __dateTHZone));
                __getFormat = __getFormat.Replace("ดด", __date.ToString("MM", __dateTHZone));
                __getFormat = __getFormat.Replace("ปปปป", __date.ToString("yyyy", __dateTHZone));
                __getFormat = __getFormat.Replace("ปป", __date.ToString("yy", __dateTHZone));
                __getFormat = __getFormat.Replace("@", docNo);
                int __numberBegin = __getFormat.IndexOf("#");
                StringBuilder __getFormatNumber = new StringBuilder();
                int __numberEnd = __numberBegin;
                while (__numberEnd < __getFormat.Length && __getFormat[__numberEnd] == '#')
                {
                    __getFormatNumber.Append("#");
                    __numberEnd++;
                }
                __getFormat = __getFormat.Remove(__numberBegin, __numberEnd - __numberBegin);

                try
                {
                    Boolean __passRunning = false;
                    double __runningNumber = 1;
                    string __newFormat = __getFormatNumber.ToString().Replace('#', '0');
                    // ตรวจสอบ Data Bases Type
                    string __query = "";

                    if (_bool)
                    {
                        switch (__myFrameWork._type)
                        {
                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                __query = "select top 1 " + fieldName + " from  " + tableName + " where " + quereyWhere + " order by " + fieldName + " desc ";
                                break;
                            default:
                                __query = "select " + fieldName + " from  " + tableName + " where " + quereyWhere + " order by " + fieldName + " desc  limit(1)";
                                break;
                        }
                        DataSet __getLastCode = __myFrameWork._query(__query);
                        if (__getLastCode.Tables[0].Rows.Count > 0)
                        {
                            if (__getLastCode.Tables[0].Rows[0][0].ToString().IndexOf(__getFormat) != -1)
                            {
                                string __docRun = __getLastCode.Tables[0].Rows[0][0].ToString();
                                __docRun = __docRun.Substring(__getFormat.Length);
                                __runningNumber = double.Parse(__docRun) + 1;
                                __passRunning = true;
                            }
                        }
                        __result = __getFormat + String.Format("{0:" + __newFormat.Remove(0, 1) + "#}", __runningNumber);

                    }
                    else
                    {
                        __result = __getFormat + String.Format("{0:" + __newFormat.Remove(0, 1) + "#}", __running);
                    }

                }
                catch
                {
                }
            }

            return __result;
        }

        private void _saveIcBalance_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Export Inventory Balance ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                // pack query and insert to new database
                MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
                MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);

                MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
                MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

                decimal __totalCost = ((MyLib._myGrid._columnType)this._icBalancGrid._columnList[this._icBalancGrid._findColumnByName("cost_amount")])._total;
                string __docNo = this._icdocNoTextbox.Text;
                string __docDate = this._icBalanceDocDateDateBox._textQuery();
                string __docFormatCode = this._icDocFormat.Text;

                string __masterField = "doc_no, doc_date, trans_flag, trans_type, doc_time";
                string __masterValue = "\'" + __docNo + "\', " + __docDate + ", 54, 3, \'" + this._icDocTimeTextbox.Text + "\'";

                List<string> __queryList = new List<string>();

                string __queryInsertIcTrans = "insert into ic_trans (" + __masterField + ", doc_format_code, total_amount) values (" + __masterValue + ",\'" + __docFormatCode + "\', " + __totalCost + ")";
                __queryList.Add(__queryInsertIcTrans);

                for (int __row = 0; __row < this._icBalancGrid._rowData.Count; __row++)
                {
                    object __obj = this._icBalancGrid._cellGet(__row, "new_ic_code").ToString();
                    string __itemCode = (this._icBalancGrid._cellGet(__row, "new_ic_code") != null && this._icBalancGrid._cellGet(__row, "new_ic_code").ToString().Length > 0) ? this._icBalancGrid._cellGet(__row, "new_ic_code").ToString() : this._icBalancGrid._cellGet(__row, "ic_code").ToString();

                    string __detailField = "line_number, item_code, item_name, unit_code, wh_code, shelf_code, qty, price, sum_amount, sum_of_cost, stand_value, divide_value, calc_flag, doc_date_calc, doc_time_calc";
                    string __detailValue = __row.ToString() +
                        ", \'" + __itemCode + "\'" +
                        ",\'" + this._icBalancGrid._cellGet(__row, "ic_name").ToString().Replace("'", "''") + "\'" +
                        ",\'" + this._icBalancGrid._cellGet(__row, "unit_code").ToString() + "\'" +
                        ",\'" + this._icBalancGrid._cellGet(__row, "warehouse").ToString() + "\'" +
                        ",\'" + this._icBalancGrid._cellGet(__row, "location").ToString() + "\'" +
                        "," + this._icBalancGrid._cellGet(__row, "balance_qty").ToString() + "" +
                        "," + this._icBalancGrid._cellGet(__row, "cost").ToString() + "" +
                        "," + this._icBalancGrid._cellGet(__row, "cost_amount").ToString() + "" +
                        "," + this._icBalancGrid._cellGet(__row, "cost_amount").ToString() + "" +
                        "," + this._icBalancGrid._cellGet(__row, "stand_value").ToString() + "" +
                        "," + this._icBalancGrid._cellGet(__row, "divide_value").ToString() + "" +
                        ",1" + // calc_flag
                        "," + __docDate +
                        ",\'" + this._icDocTimeTextbox.Text + "\'";

                    string __queryInsertDetail = "insert into ic_trans_detail (" + __masterField + ", " + __detailField + " ) values (" + __masterValue + ", " + __detailValue + " ) ";
                    __queryList.Add(__queryInsertDetail);
                }

                string __result = __target._queryInsertOrUpdateList(__queryList);

                if (__result.Length == 0)
                {
                    // process ยอดคงเหลือ
                    MessageBox.Show("Export Inventory Balance Success");
                }
                else
                {
                    MessageBox.Show(__result.ToString(), "Error");
                }
            }
        }

        private void _apBalanceExportButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Export Supplier Balance ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {

                // pack query and insert to new database
                MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
                MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);

                MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
                MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

                DateTime __docDate = this._apBalanceDocDate._dateTime;

                string __docDateStr = this._apBalanceDocDate._textQuery();
                string __docFormatCode = this._apBalanceDocFormatCode.Text;

                List<string> __queryList = new List<string>();
                for (int __loop = 0; __loop < this._apBalanceGrid1._rowData.Count; __loop++)
                {
                    string __docNo = this._apBalanceDocFormat.Text + "-" + (__loop + 1).ToString();
                    string __fieldList = "doc_no, doc_date,trans_flag,trans_type,doc_format_code,cust_code, total_amount";
                    string __valueList = "\'" + __docNo + "\', " + __docDateStr + ", 81, 4, \'" + __docFormatCode + "\',  \'" + this._apBalanceGrid1._cellGet(__loop, "ap_code").ToString() + "\'," + this._apBalanceGrid1._cellGet(__loop, "ap_balance").ToString();

                    string __query = "insert into ic_trans(" + __fieldList + ") values (" + __valueList + ")";
                    __queryList.Add(__query);
                }

                if (__queryList.Count > 0)
                {
                    string __result = __target._queryInsertOrUpdateList(__queryList);

                    if (__result.Length == 0)
                    {
                        MessageBox.Show("Export Supplier Balance Success");
                    }
                    else
                    {
                        MessageBox.Show(__result.ToString(), "Error");
                    }
                }
            }
        }

        private void _arBalanceExportButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Export Customer Balance ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {

                MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
                MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);

                MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);
                MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

                DateTime __docDate = this._arDocDate._dateTime;

                string __docDateStr = this._arDocDate._textQuery();
                string __docFormatCode = this._arDocFormatCode.Text;

                List<string> __queryList = new List<string>();
                for (int __loop = 0; __loop < this._arBalanceGrid1._rowData.Count; __loop++)
                {
                    string __docNo = this._arDocFormat.Text + "-" + (__loop + 1).ToString();
                    string __fieldList = "doc_no, doc_date,trans_flag,trans_type,doc_format_code,cust_code, total_amount";
                    string __valueList = "\'" + __docNo + "\', " + __docDateStr + ", 93, 5, \'" + __docFormatCode + "\',  \'" + this._arBalanceGrid1._cellGet(__loop, "ar_code").ToString() + "\'," + this._arBalanceGrid1._cellGet(__loop, "ar_balance").ToString();

                    string __query = "insert into ic_trans(" + __fieldList + ") values (" + __valueList + ")";
                    __queryList.Add(__query);
                }

                if (__queryList.Count > 0)
                {
                    string __result = __target._queryInsertOrUpdateList(__queryList);

                    if (__result.Length == 0)
                    {
                        MessageBox.Show("Export Customer Balance Success");
                    }
                    else
                    {
                        MessageBox.Show(__result.ToString(), "Error");
                    }
                }

            }
        }

        private void _arMasterExportButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Export Customer Data ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {

                _transferTable("ar_customer");
                _transferTable("ar_customer_detail");
                _transferTable("ar_dealer");
                _transferTable("ar_contactor");
                _transferTable("ar_item_by_customer");

                MessageBox.Show("Export Customer Data Success", "success");
            }
        }

        private void _apMasterExportButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Export Supplier Data ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {

                _transferTable("ap_supplier");
                _transferTable("ap_supplier_detail");
                _transferTable("ap_contactor");
                _transferTable("ap_item_by_supplier");

                MessageBox.Show("Export Supplier Data Success", "success");
            }

        }

        private void _changeItemCodeProcessButton_Click(object sender, EventArgs e)
        {
            // new method

            // step สร้าง ตาราง map สินค้า
            // แล้ว insert รหัสใหม่
            MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);
            MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

            // create table for map new ic_code
            StringBuilder __script = new StringBuilder();
            __script.Append("CREATE TABLE map_new_iccode_temp\n ");
            __script.Append("(\n ");
            __script.Append("  old_code character varying(30) NOT NULL,\n ");
            __script.Append("  new_code character varying(30),\n ");
            __script.Append("  CONSTRAINT map_new_iccode_temp_pk_old_code PRIMARY KEY (old_code) \n");
            __script.Append(")\n ");
            __script.Append("WITH (\n ");
            __script.Append("  OIDS=FALSE\n ");
            __script.Append(");\n ");

            string __result = __target._queryInsertOrUpdate(__script.ToString());

            __script = new StringBuilder();
            __script.Append("CREATE INDEX map_new_iccode_temp_old_code_idx \n");
            __script.Append(" ON map_new_iccode_temp \n");
            __script.Append(" USING btree \n");
            __script.Append(" (old_code); \n");

            __result = __target._queryInsertOrUpdate(__script.ToString());

            __target._queryInsertOrUpdate("truncate map_new_iccode_temp");

            // insert into 
            List<string> __queryInsertList = new List<string>();
            for (int __i = 0; __i < this._gridNewItemCode._rowData.Count; __i++)
            {
                string __oldCode = this._gridNewItemCode._cellGet(__i, "ic_code").ToString();
                string __newCode = this._gridNewItemCode._cellGet(__i, "new_ic_code").ToString();
                if (__oldCode != "" && __newCode != "")
                {
                    string __query = "insert into map_new_iccode_temp (old_code,new_code) values (\'" + __oldCode + "\', \'" + __newCode + "\')";
                    __queryInsertList.Add(__query);
                }

            }

            if (__queryInsertList.Count > 0)
            {
                __result = __target._queryInsertOrUpdateList(__queryInsertList);

                if (__result.Length > 0)
                {
                    MessageBox.Show(__result.ToString());
                }
            }



            this._changeItemCodeProcessButton.Enabled = false;
            this._buttonSelectAll.Enabled = false;
            this._buttonRemoveAll.Enabled = false;
            this._gridNewItemCode._isEdit = false;

            __isProcessSuccess = false;
            _threadImport = new System.Threading.Thread(this._process);
            _threadImport.IsBackground = true;
            _threadImport.Start();

            timer1.Start();

        }

        System.Threading.Thread _threadImport;

        void _process()
        {
            for (int __row = 0; __row < this._gridTableProcess._rowData.Count; __row++)
            {
                if (this._gridTableProcess._cellGet(__row, "check").ToString().Equals("1"))
                {
                    string __tableName = this._gridTableProcess._cellGet(__row, "tablename").ToString();
                    string __fieldName = this._gridTableProcess._cellGet(__row, "fieldname").ToString();
                    string __extraWhere = this._gridTableProcess._cellGet(__row, "extrawhere").ToString();

                    string __query = "update " + __tableName + " set " + __fieldName + "=(select new_code from map_new_iccode_temp where map_new_iccode_temp.old_code = " + __tableName + "." + __fieldName + ") where " + __fieldName + "<>(select new_code from map_new_iccode_temp where map_new_iccode_temp.old_code = " + __tableName + "." + __fieldName + ") and exists (select old_code from map_new_iccode_temp where map_new_iccode_temp.old_code = " + __tableName + "." + __fieldName + ") ";

                    MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);
                    MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);
                    __target._connectionTimeOut = (60 * 240);
                    List<string> __queryList = new List<string>();
                    __queryList.Add(__query);

                    string __resultInsert = __target._queryInsertOrUpdateList(__queryList);
                    if (__resultInsert.Length > 0)
                    {
                        MessageBox.Show(__resultInsert.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        this._gridTableProcess._cellUpdate(__row, "process_status", "Success", false);
                        this._gridTableProcess._cellUpdate(__row, "check", 0, false);

                    }
                }
                else
                {
                    this._gridTableProcess._cellUpdate(__row, "process_status", "skip", false);

                }
            }

            /*
            // process เปลี่ยนรหัส
            List<string> __queryList = new List<string>();

            // update list add
            for (int __row = 0; __row < this._gridNewItemCode._rowData.Count; __row++)
            {
                if (this._gridNewItemCode._cellGet(__row, "check").ToString().Equals("1"))
                {
                    // pack query
                    // ic_inventory  
                    string __oldCode = this._gridNewItemCode._cellGet(__row, "ic_code").ToString();
                    string __newCode = this._gridNewItemCode._cellGet(__row, "new_ic_code").ToString();

                    __queryList.Add("update ic_inventory set code = \'" + __newCode + "\' where ic_inventory.code = \'" + __oldCode + "\' ");

                    // ic_inventory_detail 
                    __queryList.Add("update ic_inventory_detail set ic_code = \'" + __newCode + "\' where ic_inventory_detail.ic_code = \'" + __oldCode + "\' ");

                    // ic_inventory_append
                    __queryList.Add("update ic_inventory_append set ic_append_code = \'" + __newCode + "\' where ic_inventory_append.ic_append_code = \'" + __oldCode + "\' ");

                    // ic_inventory_append_detail
                    __queryList.Add("update ic_inventory_append_detail set ic_code = \'" + __newCode + "\' where ic_inventory_append_detail.ic_code = \'" + __oldCode + "\' ");

                    // ic_inventory_barcode 
                    __queryList.Add("update ic_inventory_barcode set ic_code = \'" + __newCode + "\' where ic_inventory_barcode.ic_code = \'" + __oldCode + "\' ");

                    // ic_extra_detail 
                    __queryList.Add("update ic_extra_detail set ic_code = \'" + __newCode + "\' where ic_extra_detail.ic_code = \'" + __oldCode + "\' ");

                    // ic_inventory_price                        
                    __queryList.Add("update ic_inventory_price set ic_code = \'" + __newCode + "\' where ic_inventory_price.ic_code = \'" + __oldCode + "\' ");

                    // ic_inventory_purchase_price
                    __queryList.Add("update ic_inventory_purchase_price set ic_code = \'" + __newCode + "\' where ic_inventory_purchase_price.ic_code = \'" + __oldCode + "\' ");

                    // ic_inventory_replace
                    __queryList.Add("update ic_inventory_replace set ic_replace_code = \'" + __newCode + "\' where ic_inventory_replace.ic_replace_code = \'" + __oldCode + "\' ");

                    // ic_inventory_replace_detail
                    __queryList.Add("update ic_inventory_replace_detail set ic_code = \'" + __newCode + "\' where ic_inventory_replace_detail.ic_code = \'" + __oldCode + "\' ");

                    // ic_inventory_set_detail
                    __queryList.Add("update ic_inventory_set_detail set ic_code = \'" + __newCode + "\' where ic_inventory_set_detail.ic_code = \'" + __oldCode + "\' ");

                    // ic_color_use
                    __queryList.Add("update ic_color_use set ic_code = \'" + __newCode + "\' where ic_color_use.ic_code = \'" + __oldCode + "\' ");

                    // ic_unit_use
                    __queryList.Add("update ic_unit_use set ic_code = \'" + __newCode + "\' where ic_unit_use.ic_code = \'" + __oldCode + "\' ");

                    // ic_wh_shelf
                    __queryList.Add("update ic_wh_shelf set ic_code = \'" + __newCode + "\' where ic_wh_shelf.ic_code = \'" + __oldCode + "\' ");

                    // ic_name_billing
                    __queryList.Add("update ic_name_billing set ic_code = \'" + __newCode + "\' where ic_name_billing.ic_code = \'" + __oldCode + "\' ");

                    // ic_name_merket
                    __queryList.Add("update ic_name_merket set ic_code = \'" + __newCode + "\' where ic_name_merket.ic_code = \'" + __oldCode + "\' ");

                    // ic_name_pos
                    __queryList.Add("update ic_name_pos set ic_code = \'" + __newCode + "\' where ic_name_pos.ic_code = \'" + __oldCode + "\' ");

                    // ic_name_short
                    __queryList.Add("update ic_name_short set ic_code = \'" + __newCode + "\' where ic_name_short.ic_code = \'" + __oldCode + "\' ");

                    // ic_opposite_unit
                    __queryList.Add("update ic_opposite_unit set ic_code = \'" + __newCode + "\' where ic_opposite_unit.ic_code = \'" + __oldCode + "\' ");

                    // ic_pattern_use
                    __queryList.Add("update ic_pattern_use set ic_code = \'" + __newCode + "\' where ic_pattern_use.ic_code = \'" + __oldCode + "\' ");

                    // ic_promotion_detail
                    __queryList.Add("update ic_promotion_detail set ic_code = \'" + __newCode + "\' where ic_promotion_detail.ic_code = \'" + __oldCode + "\' ");

                    // ic_purchase_permium_condition
                    __queryList.Add("update ic_purchase_permium_condition set ic_code = \'" + __newCode + "\' where ic_purchase_permium_condition.ic_code = \'" + __oldCode + "\' ");

                    // ic_purchase_permium_list
                    __queryList.Add("update ic_purchase_permium_list set ic_code = \'" + __newCode + "\' where ic_purchase_permium_list.ic_code = \'" + __oldCode + "\' ");

                    // ic_purchase_price_detail
                    __queryList.Add("update ic_purchase_price_detail set ic_code = \'" + __newCode + "\' where ic_purchase_price_detail.ic_code = \'" + __oldCode + "\' ");

                    // ic_relation_detail
                    __queryList.Add("update ic_relation_detail set ic_code_1 = \'" + __newCode + "\' where ic_relation_detail.ic_code_1 = \'" + __oldCode + "\' ");

                    // ic_relation_detail
                    __queryList.Add("update ic_relation_detail set ic_code_2 = \'" + __newCode + "\' where ic_relation_detail.ic_code_2 = \'" + __oldCode + "\' ");

                    // ic_serial
                    __queryList.Add("update ic_serial set ic_code = \'" + __newCode + "\' where ic_serial.ic_code =\'" + __oldCode + "\' ");

                    // ic_size_use
                    __queryList.Add("update ic_size_use set ic_code = \'" + __newCode + "\' where ic_size_use.ic_code =\'" + __oldCode + "\' ");

                    // ic_stk_build
                    __queryList.Add("update ic_stk_build set ic_code = \'" + __newCode + "\' where ic_stk_build.ic_code = \'" + __oldCode + "\' ");

                    // ic_stk_build_detail
                    __queryList.Add("update ic_stk_build_detail set ic_code = \'" + __newCode + "\' where ic_stk_build_detail.ic_code = \'" + __oldCode + "\' ");

                    // images
                    __queryList.Add("update images set image_id = \'" + __newCode + "\' where images.image_id = \'" + __oldCode + "\' ");

                    // ic_trans_detail
                    __queryList.Add("update ic_trans_detail set item_code = \'" + __newCode + "\' where ic_trans_detail.item_code = \'" + __oldCode + "\' ");

                    // ic_trans_detail_lot
                    __queryList.Add("update ic_trans_detail_lot set item_code = \'" + __newCode + "\' where ic_trans_detail_lot.item_code =\'" + __oldCode + "\' ");

                    // ic_trans_serial_number
                    __queryList.Add("update ic_trans_serial_number set ic_code = \'" + __newCode + "\' where ic_trans_serial_number.ic_code = \'" + __oldCode + "\' ");

                    MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);
                    MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

                    string __resultInsert = "";
                    __resultInsert = __target._queryInsertOrUpdateList(__queryList);
                    if (__resultInsert.Length > 0)
                    {
                        MessageBox.Show(__resultInsert.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // query หนัก
                        //List<string> __hardQueryList = new List<string>();


                        //__resultInsert = __target._queryInsertOrUpdateList(__hardQueryList);

                        this._gridNewItemCode._cellUpdate(__row, "process_status", "Success", false);
                        this._gridNewItemCode._cellUpdate(__row, "check", 0, false);

                    }
                }
                else
                {
                    this._gridNewItemCode._cellUpdate(__row, "process_status", "skip", false);

                }
            }*/

            __isProcessSuccess = true;
            MessageBox.Show("Success");

            //                for (int __row = 0; __row < this._icBalancGrid._rowData.Count; __row++)
            //{
            //    string __getItemCode = this._icBalancGrid._cellGet(__row, "ic_code").ToString();

            //    for (int __loop = 0; __loop < this._gridNewItemCode._rowData.Count; __loop++)
            //    {
            //        if (this._gridNewItemCode._cellGet(__loop, "ic_code").ToString().Equals(__getItemCode))
            //        {
            //            string __newIcCode = this._gridNewItemCode._cellGet(__loop, "new_ic_code").ToString();
            //            this._icBalancGrid._cellUpdate(__row, "new_ic_code", __newIcCode, false);
            //        }
            //    }
            //}
        }

        private void _buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._gridTableProcess._rowData.Count; __i++)
            {
                this._gridTableProcess._cellUpdate(__i, "check", 1, false);
            }
            this._gridTableProcess.Refresh();
            this._gridTableProcess.Invalidate();
        }

        private void _buttonRemoveAll_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._gridTableProcess._rowData.Count; __i++)
            {
                this._gridTableProcess._cellUpdate(__i, "check", 0, false);
            }
            this._gridTableProcess.Refresh();
            this._gridTableProcess.Invalidate();
        }

        private void _buttonStopProcess_Click(object sender, EventArgs e)
        {
            try
            {
                this._threadImport.Abort();
                __isProcessSuccess = true;

            }
            catch
            {
            }
        }

        Boolean __isProcessSuccess = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (__isProcessSuccess == true)
            {
                this._changeItemCodeProcessButton.Enabled = true;
                this._buttonSelectAll.Enabled = true;
                this._buttonRemoveAll.Enabled = true;
                this._gridNewItemCode._isEdit = true;
                timer1.Stop();
            }
        }

        private void _loadICToolStripButton_Click(object sender, EventArgs e)
        {
            // load from source

            string __query = "select code as ic_code, name_1 as ic_name, unit_standard as ic_unit_standard from ic_inventory order by code";

            MyLib._myGlobal._databaseType __sourceType = _getProviderFromIndex(_global._sourceProvider);
            MyLib._myDatabaseFrameWork __source = new MyLib._myDatabaseFrameWork(__sourceType, _global._soruceHost, _global._sourcePort, _global._sourceUser, _global._sourcePass, _global._sourceDatabase);

            DataSet __result = __source._query(__query);

            if (__result.Tables.Count > 0)
            {
                this._gridICExport._loadFromDataTable(__result.Tables[0]);
            }

        }

        private void _chnageICToolStripButton_Click(object sender, EventArgs e)
        {
            MyLib._myGridImportFromTextFileForm __form = new MyLib._myGridImportFromTextFileForm(this._gridNewItemCode._columnList);
            __form._importButton.Click += (s1, e1) =>
            {
                __form.Close();
                for (int __row1 = 0; __row1 < __form._dataGridView.Rows.Count; __row1++)
                {

                    try
                    {
                        int __addrRow = -1;
                        for (int __row2 = 0; __row2 < __form._mapFieldView.Rows.Count; __row2++)
                        {
                            string __name = __form._mapFieldView.Rows[__row2].Cells[0].Value.ToString();
                            string __field = (__form._mapFieldView.Rows[__row2].Cells[1].Value == null) ? "" : __form._mapFieldView.Rows[__row2].Cells[1].Value.ToString();
                            if (__field.Trim().Length > 0)
                            {
                                int __columnNumber = -1;
                                for (int __loop = 0; __loop < __form._dataGridView.Columns.Count; __loop++)
                                {
                                    if (__form._dataGridView.Columns[__loop].Name.Equals(__field))
                                    {
                                        __columnNumber = __loop;
                                        break;
                                    }
                                }
                                if (__columnNumber != -1)
                                {
                                    string __value = __form._dataGridView.Rows[__row1].Cells[__columnNumber].Value.ToString();
                                    if (__addrRow == -1)
                                    {
                                        __addrRow = this._gridNewItemCode._addRow();
                                    }
                                    int __gridColumnNumber = -1;
                                    MyLib._myGrid._columnType __myColumn = null;
                                    for (int __column = 0; __column < this._gridNewItemCode._columnList.Count; __column++)
                                    {
                                        __myColumn = (MyLib._myGrid._columnType)this._gridNewItemCode._columnList[__column];
                                        if (__myColumn._name.Equals(__name))
                                        {
                                            __gridColumnNumber = __column;
                                            break;
                                        }
                                    }
                                    if (__myColumn != null && __gridColumnNumber != -1)
                                    {
                                        switch (__myColumn._type)
                                        {
                                            case 1: this._gridNewItemCode._cellUpdate(__addrRow, __gridColumnNumber, __value, false); break;
                                            case 2:
                                            case 3: this._gridNewItemCode._cellUpdate(__addrRow, __gridColumnNumber, MyLib._myGlobal._decimalPhase(__value), false); break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                }

                // compare _gridNewItemCode , gridbalance
                for (int __row = 0; __row < this._gridICExport._rowData.Count; __row++)
                {
                    string __getItemCode = this._gridICExport._cellGet(__row, "ic_code").ToString();

                    //for (int __loop = 0; __loop < this._gridNewItemCode._rowData.Count; __loop++)
                    //{
                    //    if (this._gridNewItemCode._cellGet(__loop, "ic_code").ToString().Equals(__getItemCode))
                    //    {
                    //        string __newIcCode = this._gridNewItemCode._cellGet(__loop, "new_ic_code").ToString();
                    //        this._gridICExport._cellUpdate(__row, "new_ic_code", __newIcCode, false);
                    //    }
                    //}

                    int __indexOf = this._gridNewItemCode._cellSearch("ic_code", __getItemCode);
                    if (__indexOf != -1)
                    {
                        string __newIcCode = this._gridNewItemCode._cellGet(__indexOf, "new_ic_code").ToString();
                        this._gridICExport._cellUpdate(__row, "new_ic_code", __newIcCode, false);
                    }
                }
            };
            __form.ShowDialog();
        }

        private void _checkAkzoItemButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);
            MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);

            // create table for map new ic_code
            StringBuilder __script = new StringBuilder();
            __script.Append("CREATE TABLE ic_checkakzocode_temp\n ");
            __script.Append("(\n ");
            __script.Append("  ic_code character varying(30) NOT NULL\n ");
            __script.Append(")\n ");
            __script.Append("WITH (\n ");
            __script.Append("  OIDS=FALSE\n ");
            __script.Append(");\n ");
            string __result = __target._queryInsertOrUpdate(__script.ToString());


            __script = new StringBuilder();
            __script.Append("CREATE INDEX ic_checkakzocode_temp_temp_ic_code_idx \n");
            __script.Append(" ON ic_checkakzocode_temp \n");
            __script.Append(" USING btree \n");
            __script.Append(" (ic_code); \n");

            __result = __target._queryInsertOrUpdate(__script.ToString());

            __target._queryInsertOrUpdate("truncate ic_checkakzocode_temp");

            // insert into 
            List<string> __queryInsertList = new List<string>();
            for (int __i = 0; __i < this._gridCheckItem._rowData.Count; __i++)
            {
                string __oldCode = this._gridCheckItem._cellGet(__i, "ic_code").ToString();
                if (__oldCode != "")
                {
                    string __query = "insert into ic_checkakzocode_temp (ic_code) values (\'" + __oldCode + "\')";
                    __queryInsertList.Add(__query);
                }


            }

            if (__queryInsertList.Count > 0)
            {
                __result = __target._queryInsertOrUpdateList(__queryInsertList);

                if (__result.Length > 0)
                {
                    MessageBox.Show(__result.ToString());
                }
            }




            //
            // check now
            _updateIsLockRecord(_g.d.ic_inventory._table, _g.d.ic_inventory._code);
            _updateIsLockRecord(_g.d.ic_inventory_detail._table, _g.d.ic_inventory_detail._ic_code);
            _updateIsLockRecord(_g.d.ic_inventory_barcode._table, _g.d.ic_inventory_barcode._ic_code);
            _updateIsLockRecord(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code);
            _updateIsLockRecord(_g.d.ic_inventory_set._table, _g.d.ic_inventory_set._ic_set_code);
            _updateIsLockRecord(_g.d.ic_inventory_set_detail._table, _g.d.ic_inventory_set_detail._ic_set_code);
            _updateIsLockRecord(_g.d.ic_wh_shelf._table, _g.d.ic_wh_shelf._ic_code);
            _updateIsLockRecord(_g.d.ic_inventory_price._table, _g.d.ic_inventory_price._ic_code);


            MessageBox.Show("success");
        }

        public void _updateIsLockRecord(string tableName, string fieldName)
        {

            string __query = "update " + tableName + " set is_lock_record = 1 where is_lock_record <> 1 and exists (select ic_code from ic_checkakzocode_temp where ic_checkakzocode_temp.ic_code = " + tableName + "." + fieldName + " ) ";

            MyLib._myGlobal._databaseType __targetType = _getProviderFromIndex(_global._targetProvider);
            MyLib._myDatabaseFrameWork __target = new MyLib._myDatabaseFrameWork(__targetType, _global._targetHost, _global._targetPort, _global._targetUser, _global._targetPass, _global._targetDatabase);
            string __resultInsert = __target._queryInsertOrUpdate(__query);

        }

        private void _priceExport_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Export Inventory Price ?? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                _transferTable("ic_inventory", false, "", _g.d.ic_inventory._item_type + " in (0,1,2,3,4) "); // , true, "code"

                // ic_inventory_detail
                _transferTable("ic_inventory_detail", false, "", " (select item_type from ic_inventory where ic_inventory.code = ic_inventory_detail.ic_code) in (0,1,2,3,4)  ");

                // ic_inventory_barcode
                _transferTable("ic_inventory_barcode", false, "", " (select item_type from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code) in (0,1,2,3,4)  ");

                // ic_unit
                _transferTable("ic_unit");

                // ic_unit
                _transferTable("ic_unit_use", false, "", " (select item_type from ic_inventory where ic_inventory.code = ic_unit_use.ic_code) in (0,1,2,3,4)  ");

                // ic_inventory_set
                //_transferTable("ic_inventory_set");

                // ic_inventory_set_detail
                //_transferTable("ic_inventory_set_detail");

                _transferTable("ic_warehouse");

                _transferTable("ic_shelf");

                _transferTable("ic_wh_shelf", false, "", " (select item_type from ic_inventory where ic_inventory.code = ic_wh_shelf.ic_code) in (0,1,2,3,4)  ");

                _transferTable("ic_brand");

                _transferTable(_g.d.ic_inventory_price._table, false, "", _g.d.ic_inventory_price._price_mode + "=1 and (select item_type from ic_inventory where ic_inventory.code = ic_inventory_price.ic_code) in (0,1,2,3,4) ");
                MessageBox.Show("success");
            }

        }


    }

}
