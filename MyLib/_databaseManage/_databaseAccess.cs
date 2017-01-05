using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _databaseAccess : Form
    {
        ArrayList _databaseList = new ArrayList();
        ArrayList _userList = new ArrayList();
        ArrayList _userMemberList = new ArrayList();
        ArrayList _databaseMember = new ArrayList();
        int _selectGroupID = -1;
        Boolean _isChange = false;

        public _databaseAccess()
        {
            InitializeComponent();
            // ดึงข้อมูลเก่า
            _myFrameWork myFrameWork = new _myFrameWork();
            string query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_group + " from " + MyLib._d.sml_database_list._table + " order by "+ MyLib._d.sml_database_list._data_code;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                _databaseList.Clear();
                for (int row = 0; row < getRows.Length; row++)
                {
                    _myDatabaseType data = new _myDatabaseType();
                    data._code = getRows[row].ItemArray[0].ToString().ToUpper();
                    data._codeOld = data._code;
                    data._name = getRows[row].ItemArray[1].ToString();
                    data._databaseGroup = getRows[row].ItemArray[2].ToString();
                    _databaseList.Add(data);
                    _myGroupMemberType data2 = new _myGroupMemberType();
                    data2._userCode = new ArrayList();
                    _databaseMember.Add(data2);
                } // for
                if (_databaseList.Count == 0)
                {
                    // ไม่พบฐานข้อมูล กรุณากลับไปทำการสร้างก่อน
                    MessageBox.Show(MyLib._myGlobal._resource("warning10"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            //  ดึงรายชื่อกลุ่มผู้ใช้
            _userList.Clear();
            query = "select " + MyLib._d.sml_group_list._group_code + "," + MyLib._d.sml_group_list._group_name + "," + MyLib._d.sml_group_list._active_status + " from " + MyLib._d.sml_group_list._table + " order by "+ MyLib._d.sml_group_list._group_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    _myUserType data = new _myUserType();
                    data._code = getRows[row].ItemArray[0].ToString().ToUpper();
                    data._codeOld = data._code;
                    data._name = getRows[row].ItemArray[1].ToString();
                    data._active = (getRows[row].ItemArray[2].ToString().CompareTo("1") == 0) ? true : false;
                    data._isOldRecord = true;
                    data._swapMode = 0;
                    data._isUser = false;
                    _userList.Add(data);
                } // for
            }
            //  ดึงรายชื่อผู้ใช้
            query = "select " + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + "," + MyLib._d.sml_user_list._active_status + "," + MyLib._d.sml_user_list._user_password + "," +MyLib._d.sml_user_list._user_level + " from " + MyLib._d.sml_user_list._table+" order by "+ MyLib._d.sml_user_list._user_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    _myUserType data = new _myUserType();
                    data._code = getRows[row].ItemArray[0].ToString().ToUpper();
                    data._codeOld = data._code;
                    data._name = getRows[row].ItemArray[1].ToString();
                    data._active = (getRows[row].ItemArray[2].ToString().CompareTo("1") == 0) ? true : false;
                    data._password = getRows[row].ItemArray[3].ToString();
                    data._level = Convert.ToInt32(getRows[row].ItemArray[4].ToString());
                    data._isOldRecord = true;
                    data._swapMode = 0;
                    _userList.Add(data);
                } // for
            }
            //
            // ดึงข้อมูลเก่า
            query = "select " + MyLib._d.sml_database_list_user_and_group._data_group + "," + MyLib._d.sml_database_list_user_and_group._data_code + "," + 
                MyLib._d.sml_database_list_user_and_group._user_or_group_code + "," + MyLib._d.sml_database_list_user_and_group._user_or_group_status + " from " + MyLib._d.sml_database_list_user_and_group._table + " order by " + MyLib._d.sml_database_list_user_and_group._data_group;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    string __getDatabaseCode = getRows[row].ItemArray[1].ToString().ToLower();
                    bool found = false;
                    int addr = -1;
                    for (int loop = 0; loop < _databaseList.Count; loop++)
                    {
                        _myDatabaseType __getDatabase = (_myDatabaseType)_databaseList[loop];
                        if (__getDatabase._code.ToLower().Equals(__getDatabaseCode.ToLower()))
                        {
                            found = true;
                            addr = loop;
                            break;
                        }
                    } // for
                    if (found)
                    {
                        _myGroupMemberType dataGroup = (_myGroupMemberType)_databaseMember[addr];
                        dataGroup._userCode.Add(getRows[row].ItemArray[2].ToString());
                    }
                } // for
            }
            //
            _listViewUser.ItemDrag += new ItemDragEventHandler(_listViewUser_ItemDrag);
            _listViewUserMember.DragOver += new DragEventHandler(_listViewUserMember_DragOver);
            _listViewUser.DragDrop += new DragEventHandler(_listViewUser_DragDrop);
            //
            _listViewUserMember.ItemDrag += new ItemDragEventHandler(_listViewUserMember_ItemDrag);
            _listViewUser.DragOver += new DragEventHandler(_listViewUser_DragOver);
            _listViewUserMember.DragDrop += new DragEventHandler(_listViewUserMember_DragDrop);
            //
            _listViewDatabase.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(_listViewGroup_ItemSelectionChanged);
            this.Invalidate();
            // show group
            _listViewDatabase.Items.Clear();
            for (int loop = 0; loop < _databaseList.Count; loop++)
            {
                ListViewItem myDataView = new ListViewItem();
                _myDatabaseType data = (_myDatabaseType)_databaseList[loop];
                myDataView.Text = data._name + " (" + data._code + ")";
                myDataView.ImageIndex = 5;
                myDataView.Group = _listViewDatabase.Groups[0];
                myDataView.Tag = data._code;
                myDataView.ToolTipText = myDataView.Text;
                _listViewDatabase.Items.Add(myDataView);
            } // for
            //
            listViewRefresh();
        }

        void _listViewUser_DragDrop(object sender, DragEventArgs e)
        {
            // 
            string getCode = "";
            ListView.SelectedListViewItemCollection breakfast = _listViewUserMember.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                getCode = item.Tag.ToString();
                _myGroupMemberType data = (_myGroupMemberType)_databaseMember[_selectGroupID];
                for (int loop = 0; loop < data._userCode.Count; loop++)
                {
                    if (data._userCode[loop].ToString().CompareTo(getCode) == 0)
                    {
                        data._userCode.RemoveAt(loop);
                        break;
                    }
                } // for
            }
            listViewRefresh();
            _isChange = true;
        }

        void _listViewGroup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string getCode = "";
            ListView.SelectedListViewItemCollection breakfast = _listViewDatabase.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                getCode = item.Tag.ToString();
                break;
            }
            _selectGroupID = -1;
            if (getCode.Length > 0)
            {
                for (int loop = 0; loop < _databaseList.Count; loop++)
                {
                    _myDatabaseType data = (_myDatabaseType)_databaseList[loop];
                    if (data._code.CompareTo(getCode) == 0)
                    {
                        _selectGroupID = loop;
                        _listViewUserMember.Groups[0].Header = data._name;
                        break;
                    }
                } // for
            }
            listViewRefresh();
        }

        void _listViewUserMember_DragDrop(object sender, DragEventArgs e)
        {
            // วาง
            if (_selectGroupID == -1)
            {
                MessageBox.Show(MyLib._myGlobal._resource("warning40"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string getCode = "";
                ListView.SelectedListViewItemCollection breakfast = _listViewUser.SelectedItems;
                foreach (ListViewItem item in breakfast)
                {
                    getCode = item.Tag.ToString();
                    ((_myGroupMemberType)_databaseMember[_selectGroupID])._userCode.Add(getCode);
                }
                listViewRefresh();
                _isChange = true;
            }
        }

        void _listViewUser_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void _listViewUserMember_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _listViewUserMember.DoDragDrop(_listViewUserMember, DragDropEffects.All | DragDropEffects.Link);
        }

        void _listViewUserMember_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void _listViewUser_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _listViewUser.DoDragDrop(_listViewUser, DragDropEffects.All | DragDropEffects.Link);
        }


        int _listViewFindUserCode(string code)
        {
            for (int loop = 0; loop < _userList.Count; loop++)
            {
                _myUserType data = (_myUserType)_userList[loop];
                if (data._code.ToUpper().CompareTo(code.ToUpper()) == 0)
                {
                    return (loop);
                }
            } // for
            return (-1);
        }

        int _findUserMember(string code)
        {
            for (int loop = 0; loop < _userMemberList.Count; loop++)
            {
                if (_userMemberList[loop].ToString().CompareTo(code.ToUpper()) == 0)
                {
                    return (loop);
                }
            } // for
            return (-1);
        }

        void listViewRefresh()
        {
            _userMemberList.Clear();
            _listViewUser.Items.Clear();
            _listViewUserMember.Items.Clear();
            //
            _listViewDatabase._setExStyles();
            _listViewUser._setExStyles();
            _listViewUserMember._setExStyles();

            if (_selectGroupID != -1)
            {
                _myGroupMemberType dataGroupMember = (_myGroupMemberType)_databaseMember[_selectGroupID];
                for (int loop = 0; loop < dataGroupMember._userCode.Count; loop++)
                {
                    int addr = _listViewFindUserCode(dataGroupMember._userCode[loop].ToString());
                    if (addr != -1)
                    {
                        ListViewItem myDataView = new ListViewItem();
                        _myUserType data = (_myUserType)_userList[addr];
                        myDataView.Text = data._name + " (" + data._code + ")";
                        myDataView.ImageIndex = (data._isUser) ? 4 : 0;
                        myDataView.Group = _listViewUserMember.Groups[0];
                        myDataView.Tag = data._code;
                        myDataView.ToolTipText = myDataView.Text;
                        if (data._isUser)
                        {
                            if (data._level == 1)
                            {
                                myDataView.ForeColor = Color.Navy;
                            }
                            if (data._level == 2)
                            {
                                myDataView.ForeColor = Color.Green;
                                myDataView.ImageIndex = 2;
                            }
                            if (data._level == 3)
                            {
                                myDataView.ForeColor = Color.Blue;
                                myDataView.ImageIndex = 2;
                            }
                        }
                        if (data._active == false)
                        {
                            myDataView.ForeColor = Color.Red;
                        }
                        _listViewUserMember.Items.Add(myDataView);
                        _userMemberList.Add(data._code);
                    }
                } // for
            }
            // User
            for (int loop = 0; loop < _userList.Count; loop++)
            {
                ListViewItem myDataView = new ListViewItem();
                _myUserType data = (_myUserType)_userList[loop];
                if (_findUserMember(data._code) == -1)
                {
                    myDataView.Text = data._name + " (" + data._code + ")";
                    myDataView.ImageIndex = (data._isUser) ? 3 : 0;
                    myDataView.Group = _listViewUser.Groups[0];
                    myDataView.Tag = data._code;
                    myDataView.ToolTipText = myDataView.Text;
                    if (data._isUser)
                    {
                        if (data._level == 1)
                        {
                            myDataView.ForeColor = Color.Navy;
                        }
                        if (data._level == 2)
                        {
                            myDataView.ForeColor = Color.Green;
                            myDataView.ImageIndex = 1;
                        }
                        if (data._level == 3)
                        {
                            myDataView.ForeColor = Color.Blue;
                            myDataView.ImageIndex = 1;
                        }
                    }
                    if (data._active == false)
                    {
                        myDataView.ForeColor = Color.Red;
                    }
                    _listViewUser.Items.Add(myDataView);
                }
            } // for
        }

        private void _viewByIcon_CheckedChanged(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.LargeIcon;
            _listViewUserMember.View = View.LargeIcon;
            _listViewUser.View = View.LargeIcon;
        }

        private void _viewByList_CheckedChanged(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.Tile;
            _listViewUserMember.View = View.Tile;
            _listViewUser.View = View.Tile;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_isChange)
            {
                DialogResult result = MyLib._myGlobal._displayWarning(5, "");
                if (result == DialogResult.No) e.Cancel = true;
            }
        }

        private void _databaseAccess_Load(object sender, EventArgs e)
        {

        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            // save ทั้งหมด
            StringBuilder myQuery = new StringBuilder();
            myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            myQuery.Append("<query>delete from " + MyLib._d.sml_database_list_user_and_group._table + "</query>");
            for (int row = 0; row < this._databaseMember.Count; row++)
            {
                _myGroupMemberType myGroupMember = (_myGroupMemberType)_databaseMember[row];
                _myDatabaseType _getDatabase = (_myDatabaseType)_databaseList[row];
                for (int user = 0; user < myGroupMember._userCode.Count; user++)
                {
                    int __userOrGroup = 0;
                    for (int __find = 0; __find < this._userList.Count; __find++)
                    {
                        _myUserType __getUser = (_myUserType)this._userList[__find];
                        if (__getUser._code.ToLower().Equals(myGroupMember._userCode[user].ToString().ToLower()))
                        {
                            __userOrGroup = (__getUser._isUser) ? 0 : 1;
                            break;
                        }
                    }
                    myQuery.Append("<query>");
                    myQuery.Append("insert into " + MyLib._d.sml_database_list_user_and_group._table + " (" +
                        MyLib._d.sml_database_list_user_and_group._data_group + "," + MyLib._d.sml_database_list_user_and_group._data_code + "," +
                        MyLib._d.sml_database_list_user_and_group._user_or_group_code + "," + MyLib._d.sml_database_list_user_and_group._user_or_group_status +
                        ") values (\'" + _getDatabase._databaseGroup + "\',\'" + _getDatabase._code + "\',\'" + myGroupMember._userCode[user] + "\'," + __userOrGroup.ToString() + ")");
                    myQuery.Append("</query>");
                }
            } // for
            myQuery.Append("</node>");
            _myFrameWork myFrameWork = new _myFrameWork();
            string result = myFrameWork._queryList(MyLib._myGlobal._mainDatabase, myQuery.ToString());
            if (result.Length == 0)
            {
                // บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้
                MessageBox.Show(MyLib._myGlobal._resource("warning43"), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}