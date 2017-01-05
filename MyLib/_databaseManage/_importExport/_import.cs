using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage._importExport
{
    public partial class _import : UserControl
    {
        _myFrameWork __myFrameWork = new _myFrameWork();
        string __columnName = "Table Name";
        string __mappingColumnSource = "Source";
        string __mappingColumnDestination = "Destination";
        string __mappingColumnType = "Type";
        ArrayList __destinationField;

        public _import()
        {
            InitializeComponent();
            //
            ArrayList __getTableName = __myFrameWork._getTableFromDatabase(_myGlobal._databaseConfig, _myGlobal._databaseName);
            this._destinationTableGrid._isEdit = false;
            this._destinationTableGrid._addColumn(__columnName, 1, 255, 100);
            for (int __loop = 0; __loop < __getTableName.Count; __loop++)
            {
                int __addr = this._destinationTableGrid._addRow();
                this._destinationTableGrid._cellUpdate(__addr, 0, __getTableName[__loop].ToString(), false);
            }
            this._mappingGrid._cellComboBoxItem += new CellComboBoxItemEventHandler(_mappingGrid__cellComboBoxItem);
            this._mappingGrid._cellComboBoxGet += new CellComboBoxItemGetDisplay(_mappingGrid__cellComboBoxGet);
            this._destinationTableGrid._mouseClick += new MouseClickHandler(_destinationTableGrid__mouseClick);
            // Mappings
            this._mappingGrid._addColumn(__mappingColumnSource, 1, 150, 40);
            this._mappingGrid._addColumn(__mappingColumnDestination, 10, 150, 40);
            this._mappingGrid._addColumn(__mappingColumnType, 1, 20, 20);
        }

        string _mappingGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            return this.__destinationField[select].ToString();
        }

        object[] _mappingGrid__cellComboBoxItem(object sender, int row, int column)
        {
            return (object[])this.__destinationField.ToArray();
        }


        void _getField(int destinationRow)
        {
            this.__destinationField = __myFrameWork._getFieldFromDatabase(_myGlobal._databaseConfig, _myGlobal._databaseName, this._destinationTableGrid._cellGet(destinationRow, 0).ToString());
            this.__destinationField.Insert(0, "*ignore*");
            for (int __row = 0; __row < this._mappingGrid._rowData.Count; __row++)
            {
                int __addr = 0;
                string __getSourceName = this._mappingGrid._cellGet(__row, __mappingColumnSource).ToString().ToLower();
                for (int __find = 1; __find < this.__destinationField.Count; __find++)
                {
                    if (this.__destinationField[__find].ToString().ToLower().Equals(__getSourceName))
                    {
                        __addr = __find;
                        break;
                    }
                }
                this._mappingGrid._cellUpdate(__row, __mappingColumnDestination, __addr, false);
            }
        }

        void _destinationTableGrid__mouseClick(object sender, GridCellEventArgs e)
        {
            _getField(e._row);
        }

        private void _providerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._connectButton.Enabled =
            this._fileExploreButton.Enabled =
            this._fileNameTextBox.Enabled =
            this._serverNameComboBox.Enabled =
            this._tableNameComboBox.Enabled =
            this._userCodeTextBox.Enabled =
            this._userPasswordTextBox.Enabled =
            this._serverNameComboBox.Enabled =
            this._databaseNameComboBox.Enabled =
            this._viewDataButton.Enabled = false;
            if (this._providerComboBox.SelectedIndex == 0)
            {
                this.Cursor = Cursors.WaitCursor;
                // Microsoft SQL
                this._connectButton.Enabled =
                this._serverNameComboBox.Enabled =
                this._userCodeTextBox.Enabled =
                this._userPasswordTextBox.Enabled =
                this._serverNameComboBox.Enabled = true;
                this._serverNameComboBox.Items.Clear();
                //
                SqlDataSourceEnumerator __servers = SqlDataSourceEnumerator.Instance;
                DataTable __serversTable = __servers.GetDataSources();
                foreach (DataRow __row in __serversTable.Rows)
                {
                    string __serverName = __row[0].ToString();
                    if (__row[1].ToString().Length > 0)
                    {
                        __serverName = __serverName + "\\\\" + __row[1].ToString();
                    }
                    this._serverNameComboBox.Items.Add(__serverName);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void _serverExploreButton_Click(object sender, EventArgs e)
        {

        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                SqlConnection __conn = new SqlConnection(String.Format("Data Source={0};User ID={1};Password={2}", this._serverNameComboBox.Text, this._userCodeTextBox.Text, this._userPasswordTextBox.Text));
                __conn.Open();
                SqlDataAdapter __da = new SqlDataAdapter("sp_databases", __conn);
                DataTable __dt = new DataTable();
                __da.Fill(__dt);
                this._databaseNameComboBox.Enabled = true;
                this._databaseNameComboBox.Items.Clear();
                for (int __row = 0; __row < __dt.Rows.Count; __row++)
                {
                    this._databaseNameComboBox.Items.Add(__dt.Rows[__row].ItemArray.GetValue(0).ToString());
                }
                __da.Dispose();
                __conn.Close();
                this.Cursor = Cursors.Default;
                MessageBox.Show("Connect Success");
            }
            catch (Exception __ex)
            {
                this.Cursor = Cursors.Default;
                this._databaseNameComboBox.Enabled = false;
                MessageBox.Show("Connect Fail : " + __ex.Message.ToString());
            }
        }

        private void _databaseNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._viewDataButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                SqlConnection __conn = new SqlConnection(String.Format("Data Source={0};User ID={1};Password={2};Initial Catalog={3}", this._serverNameComboBox.Text, this._userCodeTextBox.Text, this._userPasswordTextBox.Text, this._databaseNameComboBox.SelectedItem.ToString()));
                __conn.Open();
                SqlDataAdapter __da = new SqlDataAdapter("select name from SYSOBJECTS where TYPE = 'U' order by name", __conn);
                DataTable __dt = new DataTable();
                __da.Fill(__dt);
                this._tableNameComboBox.Enabled = true;
                this._tableNameComboBox.Items.Clear();
                for (int __row = 0; __row < __dt.Rows.Count; __row++)
                {
                    this._tableNameComboBox.Items.Add(__dt.Rows[__row].ItemArray.GetValue(0).ToString());
                }
                __da.Dispose();
                __conn.Close();
            }
            catch 
            {
                this._tableNameComboBox.Enabled = false;
            }
            this.Cursor = Cursors.Default;
        }

        private void _tableNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this._viewDataButton.Enabled = true;
                this._mappingGrid._clear();
                SqlConnection __conn = new SqlConnection(String.Format("Data Source={0};User ID={1};Password={2};Initial Catalog={3}", this._serverNameComboBox.Text, this._userCodeTextBox.Text, this._userPasswordTextBox.Text, this._databaseNameComboBox.SelectedItem.ToString()));
                __conn.Open();
                SqlDataAdapter __da = new SqlDataAdapter(String.Format("select column_name,data_type from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = \'{0}\' order by column_name", this._tableNameComboBox.SelectedItem.ToString()), __conn);
                DataTable __dt = new DataTable();
                __da.Fill(__dt);
                for (int __row = 0; __row < __dt.Rows.Count; __row++)
                {
                    int __addr = this._mappingGrid._addRow();
                    this._mappingGrid._cellUpdate(__addr, __mappingColumnSource, __dt.Rows[__row].ItemArray.GetValue(0).ToString(), false);
                    this._mappingGrid._cellUpdate(__addr, __mappingColumnType, __dt.Rows[__row].ItemArray.GetValue(1).ToString(), false);
                }
                __da.Dispose();
                __conn.Close();
                _getField(this._destinationTableGrid._selectRow);
            }
            catch 
            {
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void __insertData(string query)
        {
            string __myQuery = MyLib._myGlobal._xmlHeader + "<node>" + query + "</node>";
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery);
            if (__result.Length != 0)
            {
                MessageBox.Show("Insert fail : " + __result.ToString());
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                StringBuilder __fieldSourceList = new StringBuilder();
                StringBuilder __fieldDestinationList = new StringBuilder();
                for (int __row = 0; __row < this._mappingGrid._rowData.Count; __row++)
                {
                    int __getValue = (int)this._mappingGrid._cellGet(__row, __mappingColumnDestination);
                    if (__getValue != 0)
                    {
                        if (__fieldSourceList.Length > 0)
                        {
                            __fieldSourceList.Append(",");
                            __fieldDestinationList.Append(",");
                        }
                        __fieldSourceList.Append(this._mappingGrid._cellGet(__row, __mappingColumnSource).ToString());
                        __fieldDestinationList.Append(this.__destinationField[(int)this._mappingGrid._cellGet(__row, __mappingColumnDestination)].ToString());
                    }
                }
                SqlConnection __conn = new SqlConnection(String.Format("Data Source={0};User ID={1};Password={2};Initial Catalog={3}", this._serverNameComboBox.Text, this._userCodeTextBox.Text, this._userPasswordTextBox.Text, this._databaseNameComboBox.SelectedItem.ToString()));
                __conn.Open();
                //
                int __count = 0;
                int __countRecord = 0;
                StringBuilder __cmdInsert = new StringBuilder();
                //__cmdInsert.Append(MyLib._myGlobal._xmlHeader + "<node>");
                SqlCommand __cmd = new SqlCommand("select " + __fieldSourceList.ToString() + " from " + this._tableNameComboBox.SelectedItem.ToString(), __conn);
                SqlDataReader __reader = __cmd.ExecuteReader();
                while (__reader.Read())
                {
                    StringBuilder __insertValue = new StringBuilder();
                    int __countField = 0;
                    for (int __row = 0; __row < this._mappingGrid._rowData.Count; __row++)
                    {
                        int __getValue = (int)this._mappingGrid._cellGet(__row, __mappingColumnDestination);
                        if (__getValue != 0)
                        {
                            if (__insertValue.Length > 0)
                            {
                                __insertValue.Append(",");
                            }
                            string __getType = this._mappingGrid._cellGet(__row, __mappingColumnType).ToString().ToLower();
                            string __getDataValue = __reader[__countField].ToString();
                            if (__getType.Equals("varchar"))
                            {
                                __getDataValue = "\'" + __getDataValue + "\'";
                            }
                            __insertValue.Append(__getDataValue);
                            __countField++;
                        }
                    }
                    __cmdInsert.Append(String.Format("<query>insert into {0} ({1}) values ({2})</query>", this._destinationTableGrid._cellGet(this._destinationTableGrid._selectRow, 0).ToString(), __fieldDestinationList.ToString(), _myUtil._convertTextToXml(__insertValue.ToString())));
                    __count++;
                    __countRecord++;
                    this._resultTextBox.Text = String.Format("Total {0} Records", __countRecord.ToString());
                    this._resultTextBox.Invalidate();
                    this._resultTextBox.Refresh();
                    if (__count == 100)
                    {
                        __insertData(__cmdInsert.ToString());
                        __cmdInsert = new StringBuilder();
                        __count = 0;
                    }
                }
                if (__cmdInsert.Length > 0)
                {
                    __insertData(__cmdInsert.ToString());
                }
                //
                __conn.Close();
                __conn.Dispose();
                MessageBox.Show("Success Total : " + __countRecord.ToString());
            }
            catch (Exception __ex)
            {
                MessageBox.Show("Error : " + __ex.Message.ToString());
            }
            this.Cursor = Cursors.Default;
        }
    }
}
