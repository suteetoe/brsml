using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MyLib._databaseManage._linkDatabase
{
    public partial class _linkDatabase : MyLib._myForm
    {
        private DataSet _allDatabase;//รายชื่อข้อมูลทั้งหมด
        private ArrayList _allDatabaseGroup = new ArrayList();//รายชื่อกลุ่มข้อมูลทั้งหมด
        Boolean _isChange = false;

        public _linkDatabase()
        {
            InitializeComponent();
            //
            _gridDatabaseLink._clickSearchButton += new SearchEventHandler(_gridDatabaseLink__clickSearchButton);
            _myFrameWork myFrameWork = new _myFrameWork();
            _allDatabase = myFrameWork._getAllDatabase(MyLib._myGlobal._databaseConfig);
            _listViewDatabase._setExStyles();
            _gridDatabaseLink._addColumn("Database Group", 1, 100, 20, true, false, false, true);
            _gridDatabaseLink._addColumn("Database Code", 1, 100, 20, true, false);
            _gridDatabaseLink._addColumn("Company Name", 1, 100, 40, true, false);
            _gridDatabaseLink._addColumn("Database Name", 1, 100, 20, true, false, false, true);
            _gridDatabaseLink.Invalidate();
            this.Invalidate();
            //
            // ดึงฐานข้อมูลทั้งหมด
          //  string query = "select " + MyLib._d.sml_database_list._data_group + "," + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + " from " + MyLib._d.sml_database_list._table;
            string query = "select " + MyLib._d.sml_database_list._data_group + "," + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + "," + MyLib._d.sml_database_list._data_database_name + " from " + MyLib._d.sml_database_list._table;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            DataRow[] getRows = result.Tables[0].Select();
            for (int row = 0; row < getRows.Length; row++)
            {
                _gridDatabaseLink._addRow();
                int rowDataGrid = _gridDatabaseLink._rowData.Count - 1;
                _gridDatabaseLink._cellUpdate(rowDataGrid, 0, getRows[row].ItemArray[0].ToString(), false);
                _gridDatabaseLink._cellUpdate(rowDataGrid, 1, getRows[row].ItemArray[1].ToString(), false);
                _gridDatabaseLink._cellUpdate(rowDataGrid, 2, getRows[row].ItemArray[2].ToString(), false);
                _gridDatabaseLink._cellUpdate(rowDataGrid, 3, getRows[row].ItemArray[3].ToString(), false);
            } // for
            // ดึงกลุ่มข้อมูล
            query = "select " + MyLib._d.sml_database_group._group_code + "," + MyLib._d.sml_database_group._group_name + " from " + MyLib._d.sml_database_group._table;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            getRows = result.Tables[0].Select();
            for (int row = 0; row < getRows.Length; row++)
            {
                _myDatabaseGroupType data = new _myDatabaseGroupType();
                data._code = getRows[row].ItemArray[0].ToString();
                data._name = getRows[row].ItemArray[1].ToString();
                _allDatabaseGroup.Add(data);
            } // for
            //
            _splitMain.Location = new Point(_splitMain.Location.X, 5);
            _refresh();
            this.SizeChanged += new EventHandler(_linkDatabase_SizeChanged);
            // 
            _displayDatabase();
            //
            _gridDatabaseLink._beforeCellUpdate += new BeforeCellUpdateEventHandler(_gridDatabaseLink__beforeCellUpdate);
            _gridDatabaseLink._alterCellUpdate += new AfterCellUpdateEventHandler(_gridDatabaseLink__alterCellUpdate);
            _gridDatabaseLink._gotoCell(0, 0);
        }

        void _gridDatabaseLink__clickSearchButton(object sender, GridCellEventArgs e)
        {
            if (e._column == 0)
            {
                // เลือกกลุ่มข้อมูล
                _linkDatabaseSelectDatabaseGroup databaseGroupSearch = new _linkDatabaseSelectDatabaseGroup();
                for (int loop = 0; loop < _allDatabaseGroup.Count; loop++)
                {
                    _myDatabaseGroupType data = (_myDatabaseGroupType)_allDatabaseGroup[loop];
                    ListViewItem item = new ListViewItem();
                    item.Tag = data._code;
                    item.Text = data._name + " (" + data._code + ")";
                    item.ImageIndex = 0;
                    databaseGroupSearch._listViewDatabaseGroup.Items.Add(item);
                } // for
                databaseGroupSearch._doubleClick += new searchDatabaseGroupDoubleClickEventHandler(databaseGroupSearch__doubleClick);
                databaseGroupSearch.ShowDialog();
            }
            if (e._column == 3)
            {
                // เลือกฐานข้อมูล
                _linkDatabaseSearchDatabase databaseSearch = new _linkDatabaseSearchDatabase();
                DataRow[] getRows = _allDatabase.Tables[0].Select();
                for (int loop = 0; loop < getRows.Length; loop++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Name = getRows[loop].ItemArray[0].ToString();
                    item.Text = item.Name;
                    item.ImageIndex = 0;
                    if (_findSelectDatabase(item.Name) == false)
                    {
                        databaseSearch._listViewDatabase.Items.Add(item);
                    }
                } // for
                databaseSearch._doubleClick += new searchDatabaseDoubleClickEventHandler(databaseSearch__doubleClick);
                databaseSearch.ShowDialog();
            }
        }

        void databaseGroupSearch__doubleClick(object sender, string e)
        {
            _gridDatabaseLink._cellUpdate(_gridDatabaseLink._selectRow, _gridDatabaseLink._selectColumn, e, true);
        }

        void databaseSearch__doubleClick(object sender, string e)
        {
            _gridDatabaseLink._cellUpdate(_gridDatabaseLink._selectRow, _gridDatabaseLink._selectColumn, e, true);
        }

        private Boolean _findSelectDatabase(string tableCode)
        {
            for (int loop = 0; loop <= _gridDatabaseLink._rowData.Count - 1; loop++)
            {
                if (_gridDatabaseLink._cellGet(loop, 3).ToString().ToLower().CompareTo(tableCode.ToLower()) == 0)
                {
                    return (true);
                }
            } // for
            return (false);
        }

        private void _displayDatabase()
        {
            _listViewDatabase.Clear();
            DataRow[] getRows = _allDatabase.Tables[0].Select();
            for (int loop = 0; loop < getRows.Length; loop++)
            {
                ListViewItem item = new ListViewItem();
                item.Name = getRows[loop].ItemArray[0].ToString();
                item.Text = item.Name;
                bool findFound = _findSelectDatabase(item.Name);
                item.ImageIndex = (findFound) ? 1 : 0;
                item.Group = _listViewDatabase.Groups[(findFound) ? 0 : 1];
                _listViewDatabase.Items.Add(item);
            } // for
        }

        private void _linkDatabase_Load(object sender, EventArgs e)
        {
        }

        void _gridDatabaseLink__alterCellUpdate(object sender, int row, int column)
        {
            _isChange = true;
            _displayDatabase();
        }

        bool _gridDatabaseLink__beforeCellUpdate(object sender, GridCellEventArgs e)
        {
            if (e._column == 0)
            {
                for (int loop = 0; loop < _allDatabaseGroup.Count; loop++)
                {
                    if (((_myDatabaseGroupType)_allDatabaseGroup[loop])._code.ToLower().CompareTo(e._text.ToString().ToLower()) == 0)
                    {
                        return (true);
                    }
                } // for
                //   --  
                if (e._text.ToString().Length > 0)
                {
                    // ไม่พบกลุ่มข้อมูลที่ต้องการ กรุณาเลือกใหม่
                    MessageBox.Show(MyLib._myGlobal._resource("warning17"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                return (false);
            }
            if (e._column == 3)
            {
                DataRow[] getRows = _allDatabase.Tables[0].Select();
                for (int loop = 0; loop < getRows.Length; loop++)
                {
                    if (getRows[loop].ItemArray[0].ToString().ToLower().CompareTo(e._text.ToString().ToLower()) == 0)
                    {
                        return (true);
                    }
                } // for
                //  --  
                if (e._text.ToString().Length > 0)
                {
                    // ไม่พบฐานข้อมูลที่ต้องการ กรุณาเลือกใหม่
                    MessageBox.Show(MyLib._myGlobal._resource("warning18"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                return (false);
            }
            return (true);
        }

        void _linkDatabase_SizeChanged(object sender, EventArgs e)
        {
            _refresh();
        }

        void _refresh()
        {
            _splitMain.Width = this.DisplayRectangle.Width - (_splitMain.Location.X + 5);
            _splitMain.Height = this.DisplayRectangle.Height - (_splitMain.Location.Y + 5);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_isChange)
            {
                DialogResult result = MyLib._myGlobal._displayWarning(5, "");
                if (result == DialogResult.No) e.Cancel = true;
            }
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            // save ทั้งหมด
            string myQuery = MyLib._myGlobal._xmlHeader + "<node>";
            myQuery += "<query>delete from " + MyLib._d.sml_database_list._table + "</query>";
            int countError = 0;
            int countSave = 0;
            for (int row = 0; row <= _gridDatabaseLink._rowData.Count - 1; row++)
            {
                string dataGroup = _gridDatabaseLink._cellGet(row, 0).ToString();
                string dataCode = _gridDatabaseLink._cellGet(row, 1).ToString();
                string dataName = _gridDatabaseLink._cellGet(row, 2).ToString();
                if (dataGroup.Length > 0 && dataCode.Length > 0 && dataName.Length > 0)
                {
                    //myQuery += "<query>insert into " + MyLib._d.sml_database_list._table + " (" + MyLib._d.sml_database_list._data_group + "," + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + "," + MyLib._d.sml_database_list._data_database_name + ") values (";
                    myQuery += "<query>insert into " + MyLib._d.sml_database_list._table + " (" + MyLib._d.sml_database_list._data_group + "," + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + ") values (";
                    myQuery += "\'" + _gridDatabaseLink._cellGet(row, 0).ToString() + "\',";
                    myQuery += "\'" + _gridDatabaseLink._cellGet(row, 1).ToString() + "\',";
                    myQuery += "\'" + _gridDatabaseLink._cellGet(row, 2).ToString() + "\',";
                    myQuery += "\'" + _gridDatabaseLink._cellGet(row, 3).ToString() + "\')";
                    myQuery += "</query>";
                    countSave++;
                }
                else countError++;

            }// for
            myQuery += "</node>";
            _myFrameWork myFrameWork = new _myFrameWork();
            string result = myFrameWork._queryList(MyLib._myGlobal._mainDatabase, myQuery);
            if (result.Length == 0)
            {
                //   --  
                string message = String.Format(MyLib._myGlobal._resource("warning88"),countSave,countError);
                MessageBox.Show(message, MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isChange = false;
                Close();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _byIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.LargeIcon;
        }

        private void byListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.Tile;
        }
    }
}