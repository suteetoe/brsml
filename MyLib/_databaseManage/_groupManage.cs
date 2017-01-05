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
    public partial class _groupManage : MyLib._myForm
    {
        ArrayList _groupList = new ArrayList();
        ArrayList _userList = new ArrayList();
        ArrayList _userMemberList = new ArrayList();
        ArrayList _groupMember = new ArrayList();
        int _selectGroupID = -1;
        Boolean _isChange = false;

        public _groupManage()
        {
            InitializeComponent();
            // ดึงข้อมูลเก่า
            _myFrameWork myFrameWork = new _myFrameWork();
            string query = "select " + MyLib._d.sml_group_list._group_code + "," + MyLib._d.sml_group_list._group_name + "," + MyLib._d.sml_group_list._active_status + " from " + MyLib._d.sml_group_list._table+ " order by "+ MyLib._d.sml_group_list._group_code;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                _groupList.Clear();
                for (int row = 0; row < getRows.Length; row++)
                {
                    _myGroupType data = new _myGroupType();
                    data._code = getRows[row].ItemArray[0].ToString().ToUpper();
                    data._codeOld = data._code;
                    data._name = getRows[row].ItemArray[1].ToString();
                    data._active = (getRows[row].ItemArray[2].ToString().CompareTo("1") == 0) ? true : false;
                    data._isOldRecord = true;
                    _groupList.Add(data);
                    _myGroupMemberType data2 = new _myGroupMemberType();
                    data2._userCode = new ArrayList();
                    _groupMember.Add(data2);
                } // for
                if (_groupList.Count == 0)
                {
                    // ไม่พบกลุ่มผู้ใช้ กรุณากลับไปทำการสร้างกลุ่มผู้ใช้ก่อน
                    MessageBox.Show(MyLib._myGlobal._resource("warning16"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            //
            query = "select " + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + "," + MyLib._d.sml_user_list._active_status + "," + MyLib._d.sml_user_list._user_password + "," + MyLib._d.sml_user_list._user_level + " from " + MyLib._d.sml_user_list._table + " order by " + MyLib._d.sml_user_list._user_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                _userList.Clear();
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
            query = "select " + MyLib._d.sml_user_and_group._user_code + "," + MyLib._d.sml_user_and_group._group_code + " from " + MyLib._d.sml_user_and_group._table+ " order by " + MyLib._d.sml_user_and_group._user_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    string getGroupCode = getRows[row].ItemArray[1].ToString();
                    bool found = false;
                    int addr = -1;
                    for (int loop = 0; loop < _groupList.Count; loop++)
                    {
                        _myGroupType dataGroup = (_myGroupType)_groupList[loop];
                        if (dataGroup._code.CompareTo(getGroupCode.ToUpper()) == 0)
                        {
                            found = true;
                            addr = loop;
                            break;
                        }
                    } // for
                    if (found)
                    {
                        _myGroupMemberType dataGroup = new _myGroupMemberType();
                        dataGroup = (_myGroupMemberType)_groupMember[addr];
                        dataGroup._userCode.Add(getRows[row].ItemArray[0].ToString());
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
            _listViewGroup.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(_listViewGroup_ItemSelectionChanged);
            this.Invalidate();
            // show group
            _listViewGroup.Items.Clear();
            for (int loop = 0; loop < _groupList.Count; loop++)
            {
                ListViewItem myDataView = new ListViewItem();
                _myGroupType data = (_myGroupType)_groupList[loop];
                myDataView.Text = data._name + " (" + data._code + ")";
                myDataView.ImageIndex = 0;
                myDataView.Group = _listViewGroup.Groups[0];
                myDataView.Tag = data._code;
                myDataView.ToolTipText = myDataView.Text;
                if (data._active == false)
                {
                    myDataView.ForeColor = Color.Red;
                }
                _listViewGroup.Items.Add(myDataView);
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
                _myGroupMemberType data = (_myGroupMemberType)_groupMember[_selectGroupID];
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
            ListView.SelectedListViewItemCollection breakfast = _listViewGroup.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                getCode = item.Tag.ToString();
                break;
            }
            _selectGroupID = -1;
            if (getCode.Length > 0)
            {
                for (int loop = 0; loop < _groupList.Count; loop++)
                {
                    _myGroupType data = (_myGroupType)_groupList[loop];
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
                // กรุณาเลือกกลุ่มที่ต้องการก่อน
                MessageBox.Show(MyLib._myGlobal._resource("warning34"),MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string getCode = "";
                ListView.SelectedListViewItemCollection breakfast = _listViewUser.SelectedItems;
                foreach (ListViewItem item in breakfast)
                {
                    getCode = item.Tag.ToString();
                    ((_myGroupMemberType)_groupMember[_selectGroupID])._userCode.Add(getCode);
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
            _listViewGroup._setExStyles();
            _listViewUser._setExStyles();
            _listViewUserMember._setExStyles();

            if (_selectGroupID != -1)
            {
                _myGroupMemberType dataGroupMember = (_myGroupMemberType)_groupMember[_selectGroupID];
                for (int loop = 0; loop < dataGroupMember._userCode.Count; loop++)
                {
                    int addr = _listViewFindUserCode(dataGroupMember._userCode[loop].ToString());
                    if (addr != -1)
                    {
                        ListViewItem myDataView = new ListViewItem();
                        _myUserType data = (_myUserType)_userList[addr];
                        myDataView.Text = data._name + " (" + data._code + ")";
                        myDataView.ImageIndex = 4;
                        myDataView.Group = _listViewUserMember.Groups[0];
                        myDataView.Tag = data._code;
                        myDataView.ToolTipText = myDataView.Text;
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
                    myDataView.ImageIndex = 3;
                    myDataView.Group = _listViewUser.Groups[0];
                    myDataView.Tag = data._code;
                    myDataView.ToolTipText = myDataView.Text;
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
                    if (data._active == false)
                    {
                        myDataView.ForeColor = Color.Red;
                    }
                    _listViewUser.Items.Add(myDataView);
                }
            } // for
        }

        private void _groupManage_Load(object sender, EventArgs e)
        {
        }

        private void _viewByIcon_CheckedChanged(object sender, EventArgs e)
        {
            _listViewGroup.View = View.LargeIcon;
            _listViewUserMember.View = View.LargeIcon;
            _listViewUser.View = View.LargeIcon;
        }

        private void _viewByList_CheckedChanged(object sender, EventArgs e)
        {
            _listViewGroup.View = View.Tile;
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

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            // save ทั้งหมด
            StringBuilder myQuery = new StringBuilder();
            myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            myQuery.Append("<query>delete from " + MyLib._d.sml_user_and_group._table + "</query>");
            for (int row = 0; row < this._groupMember.Count; row++)
            {
                _myGroupMemberType myGroupMember = (_myGroupMemberType)_groupMember[row];
                _myGroupType myGroup = (_myGroupType)_groupList[row];
                for (int user = 0; user < myGroupMember._userCode.Count; user++)
                {
                    myQuery.Append("<query>");
                    myQuery.Append("insert into " + MyLib._d.sml_user_and_group._table + " (" + MyLib._d.sml_user_and_group._group_code + "," + MyLib._d.sml_user_and_group._user_code + ") values (\'" + myGroup._code + "\',\'" + myGroupMember._userCode[user] + "\')");
                    myQuery.Append("</query>");
                }
            } // for
            myQuery.Append("</node>");
            _myFrameWork myFrameWork = new _myFrameWork();
            string result = myFrameWork._queryList(MyLib._myGlobal._mainDatabase, myQuery.ToString());
            if (result.Length == 0)
            {
                // บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้
                MessageBox.Show(MyLib._myGlobal._resource("warning35"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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