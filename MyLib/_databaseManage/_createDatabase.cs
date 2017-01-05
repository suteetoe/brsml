using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace MyLib._databaseManage
{
    public partial class _createDatabase : MyLib._myForm
    {
        string _databaseCode = "Database Code";
        string _databaseName = "Database Name";
        string _selectGroupCode = "";
        string _selectGroupName = "";
        int _mode = 0;
        bool _isAutoCreate = false;
        public Boolean _processResult = false;
        Timer __time;

        public _createDatabase(int mode)
        {
            this._mode = mode;

            InitializeComponent();
            // ดึงรหัส User
            _userAndGroupLlistView.View = View.List;
            _userAndGroupLlistView.ShowGroups = true;
            _userAndGroupLlistView.CheckBoxes = true;
            _userAndGroupLlistView.MultiSelect = true;
            _userAndGroupLlistView.Scrollable = true;
            _userAndGroupLlistView._setExStyles();
            //
            _databaseGroupView.View = View.Tile;
            _databaseGroupView.ShowGroups = false;
            _databaseGroupView.CheckBoxes = false;
            _databaseGroupView.MultiSelect = false;
            _databaseGroupView._setExStyles();
            // ดึงผู้ใช้ เพื่อให้เลือกตอนจะสร้างข้อมูลว่าให้ผู้ใช้คนใดสามารถใช้ได้บ้าง
            _myFrameWork myFrameWork = new _myFrameWork();
            string query = "select " + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + " from " + MyLib._d.sml_user_list._table + " order by " + MyLib._d.sml_user_list._user_code;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    ListViewItem data = new ListViewItem();
                    data.Tag = getRows[row].ItemArray[0].ToString().ToUpper();
                    data.Text = "User : " + getRows[row].ItemArray[1].ToString() + " (" + data.Tag.ToString() + ")";
                    data.Group = _userAndGroupLlistView.Groups[1];
                    _userAndGroupLlistView.Items.Add(data);
                } // for
            }
            // ดึงรหัส กลุ่มข้อมูล เพื่อให้เลือกตอนจะสร้างข้อมูลว่าให้กลุ่มใดสามารถใช้ได้บ้าง
            query = "select " + MyLib._d.sml_database_group._group_code + "," + MyLib._d.sml_database_group._group_name + " from " + MyLib._d.sml_database_group._table + " order by " + MyLib._d.sml_database_group._group_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    ListViewItem data = new ListViewItem();
                    data.Tag = getRows[row].ItemArray[0].ToString().ToUpper();
                    data.Text = "Group : " + getRows[row].ItemArray[1].ToString() + " (" + data.Tag.ToString() + ")";
                    data.Group = _userAndGroupLlistView.Groups[0];
                    _userAndGroupLlistView.Items.Add(data);
                } // for
            }
            // ดึงรหัส กลุ่มข้อมูล
            query = "select " + MyLib._d.sml_database_group._group_code + "," + MyLib._d.sml_database_group._group_name + " from " + MyLib._d.sml_database_group._table+ " order by " + MyLib._d.sml_database_group._group_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    ListViewItem data = new ListViewItem();
                    data.Tag = getRows[row].ItemArray[0].ToString().ToUpper();
                    data.Text = getRows[row].ItemArray[1].ToString() + " (" + data.Tag.ToString() + ")";
                    data.ImageIndex = 0;
                    _databaseGroupView.Items.Add(data);
                } // for
            }
            //
            _detailScreen._maxColumn = 1;
            _detailScreen._getResource = false;
            _detailScreen._table_name = "temp";
            _detailScreen._addTextBox(0, 0, 1, 0, _databaseCode, 1, 50, 0, true, false);
            _detailScreen._addTextBox(1, 0, 1, 0, _databaseName, 1, 50, 0, true, false);
            //
            if (this._mode == 0)
            {
                _databaseGroupView.Click += new EventHandler(_databaseGroupView_Click);
            }

            // start create
            if (this._mode == 1)
            {
                // visiable textbox and button
                _userAndGroupLlistView.Enabled = false;
                _databaseGroupView.Enabled = false;
                _detailScreen.Enabled = false;
                _removeSelectButton.Enabled = false;
                _selectAllButton.Enabled = false;
                _createButton.Enabled = false;

                _detailScreen._setDataStr(_databaseCode, MyLib._myGlobal._mainDatabasePOSStarter);
                _detailScreen._setDataStr(_databaseName, MyLib._myGlobal._mainDatabasePOSStarter);

                // auto start 
                this._selectGroupCode = "SML";
                this._selectGroupName = "SML";

                // select all
                for (int loop = 0; loop < _userAndGroupLlistView.Items.Count; loop++)
                {
                    _userAndGroupLlistView.Items[loop].Checked = true;
                } // for

                _isAutoCreate = true;
                this.Load += new EventHandler(_createDatabase_Load);
                __time = new Timer();
                __time.Interval = 1000 * 3;
                __time.Tick += new EventHandler(__time_Tick);
            }
        }

        void __time_Tick(object sender, EventArgs e)
        {
            __time.Stop();
            if (this._mode == 1)
            {
                _createButton_Click(this, null);
            }
        }

        void _databaseGroupView_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection breakfast = this._databaseGroupView.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                _selectGroupCode = item.Tag.ToString();
                _selectGroupName = item.Text;
                //   
                string message = String.Format(MyLib._myGlobal._resource("warning7"), _selectGroupName);
                MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _createDatabase_Load(object sender, EventArgs e)
        {
            if (this._mode == 1)
            {
                __time.Start();
            }

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _splitMain_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _databaseGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            // select all
            for (int loop = 0; loop < _userAndGroupLlistView.Items.Count; loop++)
            {
                _userAndGroupLlistView.Items[loop].Checked = true;
            } // for
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // remove all
            for (int loop = 0; loop < _userAndGroupLlistView.Items.Count; loop++)
            {
                _userAndGroupLlistView.Items[loop].Checked = false;
            } // for
        }

        private void _startCreate(string myDatabaseName)
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            _resultListView.Clear();
            _resultListView.Refresh();
            ArrayList getTableList = __myFrameWork._getAllTable(0, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseStructFileName);
            int maxTable = getTableList.Count;
            int errorCount = 0;
            _progressBar.Value = 0;
            _progressBar.Refresh();
            _resultListView.View = View.List;
            _resultListView.Clear();
            _resultListView.Refresh();
            __myFrameWork._verifyDatabaseScript("before", MyLib._myGlobal._databaseConfig, _selectGroupCode, myDatabaseName, MyLib._myGlobal._databaseVerifyXmlFileName);
            for (int loop = 0; loop < maxTable; loop++)
            {
                _progressText.Text = "Verify table: [" + myDatabaseName + "." + getTableList[loop].ToString() + "]";
                _progressText.Refresh();
                string verifyResult = __myFrameWork._createDatabaseAndTable(MyLib._myGlobal._databaseConfig, _selectGroupCode, myDatabaseName, getTableList[loop].ToString(), MyLib._myGlobal._databaseStructFileName);
                if (verifyResult.Length > 0)
                {
                    MessageBox.Show("[" + _progressText.Text + "]\n" + verifyResult, "Verify database fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    errorCount++;
                    _resultListView.Items.Insert(0, _progressText.Text + " is failed (" + verifyResult + ")");
                }
                else
                {
                    _resultListView.Items.Insert(0, _progressText.Text + " " + getTableList[loop].ToString() + " is Completed");
                }
                _resultListView.Refresh();
                _progressBar.Value = (((loop + 1) * 100) / (maxTable));
                _progressBar.Invalidate();
            }// for
            __myFrameWork._verifyDatabaseScript("after", MyLib._myGlobal._databaseConfig, _selectGroupCode, myDatabaseName, MyLib._myGlobal._databaseVerifyXmlFileName);
            string message = (errorCount == 0) ? MyLib._myGlobal._resource("success") : String.Format(MyLib._myGlobal._resource("warning50"), errorCount);
            MessageBox.Show(message, MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

           

            this.Refresh();
        }

        //somruk
        bool _checkDatabaseCode(string data_code)
        {
            bool __result = false;
            _myFrameWork myFrameWork = new _myFrameWork();
            DataSet __ds = new DataSet();
            __ds = myFrameWork._query(MyLib._myGlobal._mainDatabase, "select data_code from sml_database_list where upper(data_code) = \'" + data_code.ToUpper() + "\'");
            if (__ds.Tables[0].Rows.Count > 0)
            {
                __result = true;
            }
            return __result;
        }

        private void _createButton_Click(object sender, EventArgs e)
        {
            if (_selectGroupCode.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("select_data_group_1"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            //
            int countUser = 0;
            for (int loop = 0; loop < _userAndGroupLlistView.Items.Count; loop++)
            {
                if (_userAndGroupLlistView.Items[loop].Checked == true)
                {
                    countUser++;
                }
            } // for
            if (countUser == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("select_data_group_2"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            string myDatabaseCode = _detailScreen._getDataStr(_databaseCode);
            string myDatabaseName = _detailScreen._getDataStr(_databaseName);
            if (myDatabaseCode.Length == 0 || myDatabaseName.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("select_data_group_3"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            _myFrameWork myFrameWork = new _myFrameWork();
            bool checkDatabase = myFrameWork._findDatabase(MyLib._myGlobal._databaseConfig, myDatabaseName);
            bool checkDatabasecode = _checkDatabaseCode(myDatabaseCode);
            if (checkDatabasecode == true)
            {
                MessageBox.Show(MyLib._myGlobal._resource("create_database_6") + " [" + myDatabaseCode + "]", MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (checkDatabase == true)
            {
                MessageBox.Show(MyLib._myGlobal._resource("create_database_3") + " [" + myDatabaseName + "]", MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (checkDatabase == false && checkDatabasecode == false)
            {
                DialogResult getResult = System.Windows.Forms.DialogResult.Cancel;
                //   
                if (_isAutoCreate == false)
                {
                    string message = String.Format(MyLib._myGlobal._resource("create_database_5"), _selectGroupCode, myDatabaseCode, myDatabaseName);
                    getResult = MessageBox.Show(message, MyLib._myGlobal._resource("success"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                }
                if (getResult == DialogResult.Yes || _isAutoCreate)
                {
                    this.Refresh();
                    _startCreate(myDatabaseName);
                    // เพิ่ม database เข้าไปใน datagroup,usergroup
                    StringBuilder myQuery = new StringBuilder();
                    myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // เพิ่มในกลุ่มข้อมูล
                    myQuery.Append("<query>");
                    myQuery.Append("insert into " + MyLib._d.sml_database_list._table + " (" + MyLib._d.sml_database_list._data_group + "," + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + "," + MyLib._d.sml_database_list._data_database_name + ") values ");
                    //myQuery.Append("(\'" + _selectGroupCode + "\',\'" + myDatabaseCode + "\',\'" + myDatabaseName + "\',\'" + myDatabaseCode + "\')");
                    myQuery.Append("(\'" + _selectGroupCode + "\',\'" + myDatabaseCode + "\',\'" + myDatabaseCode + "\',\'" + myDatabaseName + "\')"); //somruk
                    myQuery.Append("</query>");
                    // เพิ่มผู้ใช้หรือกลุ่มที่มีสิทธิใช้ข้อมูล
                    for (int loop = 0; loop < _userAndGroupLlistView.Items.Count; loop++)
                    {
                        ListViewItem data = _userAndGroupLlistView.Items[loop];
                        if (data.Checked == true)
                        {
                            string getStatus = (data.Text[0] == 'G') ? "1" : "0"; // G=ตัวหน้ากลุ่ม
                            myQuery.Append("<query>");
                            myQuery.Append("insert into " + MyLib._d.sml_database_list_user_and_group._table + " (" + MyLib._d.sml_database_list_user_and_group._data_group + "," + MyLib._d.sml_database_list_user_and_group._data_code + "," + MyLib._d.sml_database_list_user_and_group._user_or_group_code + "," + MyLib._d.sml_database_list_user_and_group._user_or_group_status + ") values ");
                            myQuery.Append("(\'" + _selectGroupCode + "\',\'" + myDatabaseCode + "\',\'" + data.Tag.ToString() + "\'," + getStatus + ")");
                            myQuery.Append("</query>");
                        }
                    } // for
                    myQuery.Append("</node>");
                    string result = myFrameWork._queryList(MyLib._myGlobal._mainDatabase, myQuery.ToString());
                    if (result.Length == 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("create_database_1"), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // update time update
                        //MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                        string __fileName = MyLib._myGlobal._databaseStructFileName;
                        DateTime __lastDateTimeUpdate = myFrameWork._getFileLastUpdate(__fileName);
                        string __dateTimeStr = __lastDateTimeUpdate.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                        string __query = "update " + MyLib._d.sml_database_list._table + " set " + MyLib._d.sml_database_list._last_database_xml_update + "=\'" + __dateTimeStr + "\' where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_database_name) + "=\'" + myDatabaseName + "\'";
                        string __result = myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
                        if (__result.Length > 0)
                        {
                            MessageBox.Show(__result);
                        }

                        this._processResult = true;
                    }
                    else
                    {
                        MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this._processResult = false;
                    }

                    this.Dispose();
                }
            }
            //else
            //{
            //    MessageBox.Show(MyLib._myGlobal._resource("create_database_3") + " [" + myDatabaseName + "]", MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    return;
            //}
        }

        private void _splitSub_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}