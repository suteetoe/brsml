using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MyLib._databaseManage
{
    public partial class _resourceEdit : MyLib._myForm
    {
        public _resourceEdit()
        {
            InitializeComponent();
            _myFrameWork myFrameWork = new _myFrameWork();
            string query = "select " + MyLib._d.sml_database_group._group_code + "," + MyLib._d.sml_database_group._group_name + " from " + MyLib._d.sml_database_group._table + " order by " + MyLib._d.sml_database_group._group_code;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    _searchGroupComboBox.Items.Add(getRows[row].ItemArray[0].ToString().ToLower());
                } // for
                if (_searchGroupComboBox.Items.Count == 0)
                {
                    _searchGroupComboBox.Items.Add("sml");
                }
                _searchGroupComboBox.SelectedIndex = 0;
            }
            //
            _gridResource._addColumn("Code", 1, 100, 30, false, false);
            _gridResource._addColumn("Thai", 1, 100, 35, true, false);
            _gridResource._addColumn("English", 1, 100, 35, true, false);
            _gridResource._addColumn("CodeOld", 1, 100, 0, true, true);
            //
        }

        private void _resourceEdit_Load(object sender, EventArgs e)
        {
        }

        private void _buttonLoad_Click(object sender, EventArgs e)
        {
            bool loadOk = true;
            if (_gridResource._isChange)
            {
                DialogResult result = MessageBox.Show(MyLib._myGlobal._resource("warning22"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    loadOk = false;
                }
            }
            if (loadOk)
            {
                _myFrameWork myFrameWork = new _myFrameWork();
                StringBuilder query = new StringBuilder();
                query.Append("select " + MyLib._d.sml_resource._code + "," + MyLib._d.sml_resource._name_1 + "," + MyLib._d.sml_resource._name_2 + " from " + MyLib._d.sml_resource._table + " where " + _myGlobal._addUpper(MyLib._d.sml_resource._data_group) + "=\'" + _searchGroupComboBox.SelectedItem.ToString().ToUpper() + "\' order by " + MyLib._d.sml_resource._code);
                if (_searchTextBox.Text.Length > 0)
                {
                    query.Append(" and (" + _myGlobal._addUpper(MyLib._d.sml_resource._code) + " like \'%" + _searchTextBox.Text.ToUpper() + "%\' or " + _myGlobal._addUpper(MyLib._d.sml_resource._name_1) + " like \'%" + _searchTextBox.Text.ToUpper() + "%\' or " + _myGlobal._addUpper("name_2") + " like \'%" + _searchTextBox.Text.ToUpper() + "%\') order by "+ MyLib._d.sml_resource._code);
                }
                DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query.ToString());
                if (result.Tables.Count > 0)
                {
                    DataRow[] getRows = result.Tables[0].Select();
                    _gridResource._clear();
                    for (int row = 0; row < getRows.Length; row++)
                    {
                        _gridResource._addRow();
                        int rowDataGrid = _gridResource._rowData.Count - 1;
                        _gridResource._cellUpdate(rowDataGrid, 0, getRows[row].ItemArray[0].ToString(), false);
                        _gridResource._cellUpdate(rowDataGrid, 1, getRows[row].ItemArray[1].ToString(), false);
                        _gridResource._cellUpdate(rowDataGrid, 2, getRows[row].ItemArray[2].ToString(), false);
                        // code old
                        _gridResource._cellUpdate(rowDataGrid, 3, getRows[row].ItemArray[0].ToString(), false);
                    } // for
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_gridResource._isChange)
            {
                DialogResult result = MyLib._myGlobal._displayWarning(3, "");
                if (result == DialogResult.No) e.Cancel = true;
            }
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            StringBuilder myQuery = new StringBuilder();
            myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int row = 0; row < _gridResource._rowData.Count; row++)
            {
                if ((bool)_gridResource._cellGet(row, _gridResource._columnList.Count))
                {
                    string code = _gridResource._cellGet(row, 0).ToString();
                    string name_1 = _gridResource._cellGet(row, 1).ToString();
                    string name_2 = _gridResource._cellGet(row, 2).ToString();
                    string codeOld = _gridResource._cellGet(row, 3).ToString();
                    // ข้อมูลตำแหน่งเดิม
                    if (codeOld.Length > 0)
                    {
                        myQuery.Append("<query>update " + MyLib._d.sml_resource._table + " set " + MyLib._d.sml_resource._code + "=\'" + code + "\'," + MyLib._d.sml_resource._name_1 + "=\'" + name_1 + "\'," + MyLib._d.sml_resource._name_2 + "=\'" + name_2 + "\' where " + MyLib._d.sml_resource._code + "=\'" + codeOld + "\'</query>");
                    }
                }
            } // for
            myQuery.Append("</node>");
            _myFrameWork myFrameWork = new _myFrameWork();
            string result = myFrameWork._queryList(MyLib._myGlobal._mainDatabase, myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, "");
                _gridResource._clear();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}