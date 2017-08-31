using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MyLib._databaseManage
{
    public partial class _userAndGroup : MyLib._myForm
    {
        ArrayList _groupList = new ArrayList();
        ArrayList _userList = new ArrayList();
        ArrayList _deleteGroupCodeOld = new ArrayList();
        ArrayList _deleteUserCodeOld = new ArrayList();
        Boolean _insertMode = false; // true=เพิ่ม,false=แก้ไข
        string _currentCode = "";
        Boolean _isChange = false;

        public _userAndGroup()
        {
            InitializeComponent();
            // ดึงข้อมูลเก่า
            try
            {
                //MyLib._database myDatabase = new MyLib._database();
                _myFrameWork __myFrameWork = new _myFrameWork();
                string __query = "select " + MyLib._d.sml_group_list._group_code + "," + MyLib._d.sml_group_list._group_name + "," + MyLib._d.sml_group_list._active_status + " from " + MyLib._d.sml_group_list._table + " order by " + MyLib._d.sml_group_list._group_code;
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
                if (__result.Tables.Count > 0)
                {
                    DataRow[] getRows = __result.Tables[0].Select();
                    for (int row = 0; row < getRows.Length; row++)
                    {
                        _myGroupType data = new _myGroupType();
                        data._code = getRows[row].ItemArray[0].ToString();
                        data._codeOld = data._code;
                        data._name = getRows[row].ItemArray[1].ToString();
                        data._active = (getRows[row].ItemArray[2].ToString().CompareTo("1") == 0) ? true : false;
                        data._isOldRecord = true;
                        _groupList.Add(data);
                    } // for
                }
                //
                __query = "select " + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + "," + MyLib._d.sml_user_list._active_status + "," + MyLib._d.sml_user_list._user_password + "," + MyLib._d.sml_user_list._user_level + "," + MyLib._d.sml_user_list._device_id + " from " + MyLib._d.sml_user_list._table + " order by " + MyLib._d.sml_user_list._user_code;
                __result = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
                if (__result.Tables.Count > 0)
                {
                    DataRow[] getRows = __result.Tables[0].Select();
                    _userList.Clear();
                    for (int row = 0; row < getRows.Length; row++)
                    {
                        _myUserType data = new _myUserType();
                        data._code = getRows[row].ItemArray[0].ToString();
                        data._codeOld = data._code;
                        data._name = getRows[row].ItemArray[1].ToString();
                        data._active = (getRows[row].ItemArray[2].ToString().CompareTo("1") == 0) ? true : false;
                        data._password = MyLib._myUtil._decrypt(getRows[row].ItemArray[3].ToString());
                        data._level = Convert.ToInt32(getRows[row].ItemArray[4].ToString());
                        data._device = getRows[row].ItemArray[5].ToString();
                        data._isOldRecord = true;
                        _userList.Add(data);
                    } // for
                }
            }
            catch
            {
            }

            if (MyLib._myGlobal._userLevel == 2)
            {
                this._levelComboBox.Items.Clear();
                this._levelComboBox.Items.AddRange(new object[] {
            "User",
            "Super User",
            "Admin"});

            }
            _userRadio.Enabled = false;
            _groupRadio.Enabled = false;
            _listView.DoubleClick += new EventHandler(_listView_DoubleClick);
            _listView._setExStyles();
            listViewRefresh();
        }

        private void _userAndGroup_Load(object sender, EventArgs e)
        {
        }

        void _listView_GotFocus(object sender, EventArgs e)
        {
        }


        void getGroupToPanel(string getCode, Boolean isEnable)
        {
            int _addr = _listViewFindGroupCode(getCode);
            if (_addr != -1)
            {
                _myGroupType _data = (_myGroupType)_groupList[_addr];
                _code.Text = _currentCode = _data._code;
                _name.Text = _data._name;
                _active.Checked = _data._active;
                _userRadio.Checked = false;
                _groupRadio.Checked = true;
                _userPassword.Enabled = false;
                _levelComboBox.Enabled = false;
                _deviceTextBox.Enabled = false;
                _code.Focus();
            }
        }

        void getUserToPanel(string getCode, Boolean isEnable)
        {
            int __addr = _listViewFindUserCode(getCode);
            if (__addr != -1)
            {
                _myUserType __data = (_myUserType)_userList[__addr];
                _userPassword.Enabled = true;
                _levelComboBox.Enabled = true;
                _deviceTextBox.Enabled = true;
                _code.Text = _currentCode = __data._code;
                _name.Text = __data._name;
                _active.Checked = __data._active;
                _levelComboBox.SelectedIndex = __data._level;
                _userPassword.Text = __data._password;
                _deviceTextBox.Text = __data._device;
                _userRadio.Checked = true;
                _groupRadio.Checked = false;
                _code.Focus();
            }
        }

        void _listView_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection __breakfast = this._listView.SelectedItems;
            string _getCode = "";
            int _mode = 1; // 1=Group,2=User
            this._insertMode = false; // เมื่อกด save จะได้ไป update
            foreach (ListViewItem __item in __breakfast)
            {
                _getCode = __item.Tag.ToString();
                if (__item.Group == _listView.Groups[1])
                {
                    _mode = 2;
                }
                break;
            }
            if (_mode == 1)
            {
                getGroupToPanel(_getCode, true);
            }
            else
            {
                getUserToPanel(_getCode, true);
            }
        }


        void listViewRefresh()
        {
            _listView.Items.Clear();
            for (int __loop = 0; __loop < _groupList.Count; __loop++)
            {
                ListViewItem _myDataView = new ListViewItem();
                _myGroupType _data = (_myGroupType)_groupList[__loop];
                _myDataView.Text = _data._name + " (" + _data._code + ")";
                _myDataView.ImageIndex = 0;
                _myDataView.Group = _listView.Groups[0];
                _myDataView.Tag = _data._code;
                _myDataView.ToolTipText = _myDataView.Text;
                _myDataView.ImageIndex = 0;
                if (_data._active == false)
                {
                    _myDataView.ForeColor = Color.Red;
                }
                _listView.Items.Add(_myDataView);
            } // for
            for (int __loop = 0; __loop < _userList.Count; __loop++)
            {
                ListViewItem __myDataView = new ListViewItem();
                _myUserType _data = (_myUserType)_userList[__loop];
                __myDataView.Text = _data._name + " (" + _data._code + ")";
                __myDataView.ImageIndex = 0;
                __myDataView.Group = _listView.Groups[1];
                __myDataView.Tag = _data._code;
                __myDataView.ToolTipText = __myDataView.Text;
                __myDataView.ImageIndex = 1;
                if (_data._level == 1)
                {
                    __myDataView.ForeColor = Color.Navy;
                }
                if (_data._level == 2)
                {
                    __myDataView.ForeColor = Color.Green;
                }
                if (_data._level == 3)
                {
                    __myDataView.ForeColor = Color.Blue;
                    __myDataView.ImageIndex = 2;
                }
                if (_data._active == false)
                {
                    __myDataView.ForeColor = Color.Red;
                    __myDataView.ImageIndex = 2;
                }
                _listView.Items.Add(__myDataView);
            } // for
        }

        int _listViewFindGroupCode(string code)
        {
            for (int __loop = 0; __loop < _groupList.Count; __loop++)
            {
                _myGroupType __data = (_myGroupType)_groupList[__loop];
                if (__data._code.ToUpper().CompareTo(code.ToUpper()) == 0)
                {
                    return (__loop);
                }
            } // for
            return (-1);
        }

        int _listViewFindUserCode(string code)
        {
            for (int __loop = 0; __loop < _userList.Count; __loop++)
            {
                _myUserType _data = (_myUserType)_userList[__loop];
                if (_data._code.ToUpper().CompareTo(code.ToUpper()) == 0)
                {
                    return (__loop);
                }
            } // for
            return (-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_code.Text.Length == 0 || _name.Text.Length == 0)
            {
                // รายละเอียดไม่ครบ กรุณาตรวจสอบใหม่
                MessageBox.Show(MyLib._myGlobal._resource("warning29"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _myGroupType __dataGroup = new _myGroupType();
                __dataGroup._code = _code.Text;
                __dataGroup._codeOld = "";
                __dataGroup._name = _name.Text;
                __dataGroup._active = _active.Checked;
                __dataGroup._isChange = true;
                //
                _myUserType _dataUser = new _myUserType();
                _dataUser._code = _code.Text;
                _dataUser._codeOld = "";
                _dataUser._name = _name.Text;
                _dataUser._active = _active.Checked;
                _dataUser._level = _levelComboBox.SelectedIndex;
                _dataUser._password = _userPassword.Text;
                _dataUser._device = _deviceTextBox.Text;
                _dataUser._isChange = true;
                //
                if (_insertMode == true)
                {
                    // เพิ่ม
                    if (_groupRadio.Checked)
                    {
                        if (_listViewFindGroupCode(_code.Text) != -1)
                        {
                            DialogResult result = MyLib._myGlobal._displayWarning(6, "");
                        }
                        else
                        {
                            _groupList.Add(__dataGroup);
                            _code.Text = "";
                            _name.Text = "";
                            _active.Checked = true;
                            _code.Focus();
                            listViewRefresh();
                            _isChange = true;
                        }
                    }
                    if (_userRadio.Checked)
                    {
                        if (_listViewFindUserCode(_code.Text) != -1)
                        {
                            DialogResult __result = MyLib._myGlobal._displayWarning(6, "");
                        }
                        else
                        {
                            _userList.Add(_dataUser);
                            _code.Text = "";
                            _name.Text = "";
                            _active.Checked = true;
                            _userPassword.Text = "";
                            _code.Focus();
                            listViewRefresh();
                            _isChange = true;
                        }
                    }
                }
                else
                {
                    // แก้ไข
                    if (_groupRadio.Checked)
                    {
                        int __addr_check = _listViewFindGroupCode(_code.Text);
                        int __addr = _listViewFindGroupCode(_currentCode);
                        if (__addr_check != -1 && __addr_check != __addr)
                        {
                            DialogResult result = MyLib._myGlobal._displayWarning(6, "");
                        }
                        else
                        {
                            __dataGroup._codeOld = ((_myGroupType)_groupList[__addr])._codeOld;
                            __dataGroup._isOldRecord = ((_myGroupType)_groupList[__addr])._isOldRecord;
                            _groupList[__addr] = (_myGroupType)__dataGroup;
                            listViewRefresh();
                            _isChange = true;
                        }
                    }
                    if (_userRadio.Checked)
                    {
                        int __addr_check = _listViewFindUserCode(_code.Text);
                        int __addr = _listViewFindUserCode(_currentCode);
                        if (__addr_check != -1 && __addr_check != __addr)
                        {
                            DialogResult result = MyLib._myGlobal._displayWarning(6, "");
                        }
                        else
                        {
                            _dataUser._codeOld = ((_myUserType)_userList[__addr])._codeOld;
                            _dataUser._isOldRecord = ((_myUserType)_userList[__addr])._isOldRecord;
                            _userList[__addr] = (_myUserType)_dataUser;
                            listViewRefresh();
                            _isChange = true;
                        }
                    }
                }
            }
        }

        private void _viewByIcon_CheckedChanged(object sender, EventArgs e)
        {
            _listView.View = View.LargeIcon;
        }

        private void _viewByList_CheckedChanged(object sender, EventArgs e)
        {
            _listView.View = View.Tile;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_isChange)
            {
                DialogResult __result = MyLib._myGlobal._displayWarning(5, "");
                if (__result == DialogResult.No) e.Cancel = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void _listView_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Boolean __foundItem = false;
            ListViewItem _getItem = new ListViewItem(); ;
            ListView.SelectedListViewItemCollection _breakfast = this._listView.SelectedItems;
            foreach (ListViewItem __item in _breakfast)
            {
                __foundItem = true;
                _getItem = __item;
                break;
            }
            if (__foundItem)
            {
                if (_getItem.Group == _listView.Groups[0])
                {
                    // กลุ่ม
                    getGroupToPanel(_getItem.Tag.ToString(), false);
                }
                else
                {
                    getUserToPanel(_getItem.Tag.ToString(), false);
                }
            }
        }

        private void _buttonNewGroup_Click(object sender, EventArgs e)
        {
            // สร้างกลุ่มใหม่
            _insertMode = true;
            _userRadio.Checked = false;
            _groupRadio.Checked = true;
            _userPassword.Enabled = false;
            _levelComboBox.Enabled = false;
            _deviceTextBox.Enabled = false;
            _active.Checked = true;
            _code.Text = "";
            _name.Text = "";
            _userPassword.Text = "";
            _active.Checked = true;
            _levelComboBox.SelectedIndex = 0;
            _code.Focus();
        }

        private void _buttonNewUser_Click(object sender, EventArgs e)
        {
            // สร้างผู้ใช้ใหม่
            _insertMode = true;
            _userPassword.Enabled = true;
            _levelComboBox.Enabled = true;
            _deviceTextBox.Enabled = true;
            _levelComboBox.SelectedIndex = 0;
            _userRadio.Checked = true;
            _groupRadio.Checked = false;
            _active.Checked = true;
            _code.Text = "";
            _name.Text = "";
            _userPassword.Text = "";
            _levelComboBox.SelectedIndex = 0;
            _code.Focus();
        }

        private void _buttonDelete_Click(object sender, EventArgs e)
        {
            Boolean __foundItem = false;
            ListView.SelectedListViewItemCollection _breakfast = this._listView.SelectedItems;
            foreach (ListViewItem _item in _breakfast)
            {
                __foundItem = true;
                break;
            }
            if (__foundItem)
            {
                //  --  
                string __message = MyLib._myGlobal._resource("warning41");
                MessageBoxButtons __buttons = MessageBoxButtons.YesNo;
                DialogResult __result;

                // ต้องการลบจริงหรือไม่
                __result = MessageBox.Show(this, __message, "Delete", __buttons, MessageBoxIcon.Asterisk);

                if (__result == DialogResult.Yes)
                {
                    foreach (ListViewItem __item in _breakfast)
                    {
                        string _getCode = __item.Tag.ToString();
                        if (__item.Group == _listView.Groups[0])
                        {
                            // ลบในกลุ่ม
                            int __addr = _listViewFindGroupCode(_getCode);
                            if (__addr != -1)
                            {
                                _deleteGroupCodeOld.Add(((_myGroupType)_groupList[__addr])._codeOld);
                                _groupList.RemoveAt(__addr);
                                _isChange = true;
                            }
                        }
                        if (__item.Group == _listView.Groups[1])
                        {
                            // ลบในผู้ใช้
                            int __addr = _listViewFindUserCode(_getCode);
                            if (__addr != -1)
                            {
                                _deleteUserCodeOld.Add(((_myUserType)_userList[__addr])._codeOld);
                                _userList.RemoveAt(__addr);
                                _isChange = true;
                            }
                        }
                    }
                    listViewRefresh();
                }
            }
            else
            {
                // กรุณาเลือกรายการที่ต้องการลบ
                MessageBox.Show(MyLib._myGlobal._resource("warning42"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {

        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            // save ทั้งหมด
            string __myQuery = MyLib._myGlobal._xmlHeader + "<node>";
            for (int _row = 0; _row < _deleteGroupCodeOld.Count; _row++)
            {
                __myQuery += "<query>delete from " + MyLib._d.sml_group_list._table + " where " + _myGlobal._addUpper(MyLib._d.sml_group_list._group_code) + "=\'" + _deleteGroupCodeOld[_row].ToString().ToUpper() + "\'</query>";
            }// for
            for (int __row = 0; __row < _deleteUserCodeOld.Count; __row++)
            {
                __myQuery += "<query>delete from " + MyLib._d.sml_user_list._table + " where " + _myGlobal._addUpper(MyLib._d.sml_user_list._user_code) + "=\'" + _deleteUserCodeOld[__row].ToString().ToUpper() + "\'</query>";
            }// for
            for (int __row = 0; __row < _groupList.Count; __row++)
            {
                _myGroupType __myGroup = (_myGroupType)_groupList[__row];
                if (__myGroup._isChange)
                {
                    __myQuery += "<query>";
                    if (__myGroup._isOldRecord)
                    {
                        __myQuery += "update " + MyLib._d.sml_group_list._table + " set " + MyLib._d.sml_group_list._group_code + "=\'" + __myGroup._code + "'," +
                            MyLib._d.sml_group_list._group_name + "=\'" + __myGroup._name + "\'," + MyLib._d.sml_group_list._active_status + "=" +
                            ((__myGroup._active) ? 1 : 0) + " where " + _myGlobal._addUpper(MyLib._d.sml_group_list._group_code) + "=\'" + __myGroup._codeOld.ToUpper() + "\'";
                    }
                    else
                    {
                        __myQuery += "insert into " + MyLib._d.sml_group_list._table + " (" + MyLib._d.sml_group_list._group_code + "," +
                            MyLib._d.sml_group_list._group_name + "," + MyLib._d.sml_group_list._active_status + ") values (\'" + __myGroup._code + "\',\'" + __myGroup._name + "\'," + ((__myGroup._active) ? 1 : 0) + ")";
                    }
                    __myQuery += "</query>";
                }
            } // for
            for (int __row = 0; __row < _userList.Count; __row++)
            {
                _myUserType __myUser = (_myUserType)_userList[__row];
                if (__myUser._isChange)
                {
                    __myQuery += "<query>";
                    if (__myUser._isOldRecord)
                    {
                        __myQuery += "update " + MyLib._d.sml_user_list._table + " set " + MyLib._d.sml_user_list._user_code + "=\'" + __myUser._code + "\'," + MyLib._d.sml_user_list._user_name + "=\'" + __myUser._name + "\'," + MyLib._d.sml_user_list._active_status + "=" + ((__myUser._active) ? 1 : 0) + "," + MyLib._d.sml_user_list._user_password + "=\'" + MyLib._myUtil._encrypt(__myUser._password) + "\'," + MyLib._d.sml_user_list._user_level + "=" + __myUser._level + "," + MyLib._d.sml_user_list._device_id + "=\'" + __myUser._device.Trim() + "\' where " + _myGlobal._addUpper(MyLib._d.sml_user_list._user_code) + "=\'" + __myUser._codeOld.ToUpper() + "\'";
                    }
                    else
                    {
                        __myQuery += "insert into " + MyLib._d.sml_user_list._table + " (" + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + "," + MyLib._d.sml_user_list._active_status + "," + MyLib._d.sml_user_list._user_password + "," + MyLib._d.sml_user_list._user_level + "," + MyLib._d.sml_user_list._device_id + ") values (\'" + __myUser._code + "\',\'" + __myUser._name + "\'," + ((__myUser._active) ? 1 : 0) + ",\'" + MyLib._myUtil._encrypt(__myUser._password) + "\'," + __myUser._level + ",\'" + __myUser._device.Trim() + "\')";
                    }
                    __myQuery += "</query>";
                }
            } // for
            __myQuery += "</node>";
            //MyLib._database myDatabase = new MyLib._database();
            _myFrameWork __myFrameWork = new _myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._mainDatabase, __myQuery);
            if (__result.Length == 0)
            {
                // บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้
                MessageBox.Show(MyLib._myGlobal._resource("warning43"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isChange = false;
                Close();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}