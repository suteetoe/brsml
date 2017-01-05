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
    public partial class _groupDatabase : MyLib._myForm
    {
        ArrayList _dataGroupBuffer = new ArrayList();
        ArrayList _userList = new ArrayList();
        Boolean _insertMode = false;
        Boolean _isChange = false;
        string _currentCode = "";
        int _selectGroupID = -1;

        public _groupDatabase()
        {
            InitializeComponent();
            // ดึงรหัส admin
            _myFrameWork myFrameWork = new _myFrameWork();
            string query = "select " + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + " from " + MyLib._d.sml_user_list._table + " where " + MyLib._d.sml_user_list._user_level + "=2 or +" + MyLib._d.sml_user_list._user_level + "=3 order by "+ MyLib._d.sml_user_list._user_code;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    _myUserType data = new _myUserType();
                    data._code = getRows[row].ItemArray[0].ToString().ToUpper();
                    data._name = getRows[row].ItemArray[1].ToString();
                    _userList.Add(data);
                } // for
            }
            // ดึงรหัส กลุ่มข้อมูล
            query = "select " + MyLib._d.sml_database_group._group_code + "," + MyLib._d.sml_database_group._group_name + " from " + MyLib._d.sml_database_group._table + " order by "+ MyLib._d.sml_database_group._group_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    _myDatabaseGroupType data = new _myDatabaseGroupType();
                    data._code = getRows[row].ItemArray[0].ToString().ToUpper();
                    data._name = getRows[row].ItemArray[1].ToString();
                    data._adminList = new ArrayList();
                    _dataGroupBuffer.Add(data);
                } // for
            }
            if (_dataGroupBuffer.Count == 0)
            {
                _myDatabaseGroupType data = new _myDatabaseGroupType();
                data._code = "sml";
                data._name = "Default";
                data._adminList = new ArrayList();
                _dataGroupBuffer.Add(data);
            }
            //
            // ดึงรหัส admin ย่อย
            query = "select " + MyLib._d.sml_database_group_admin._group_code + "," + MyLib._d.sml_database_group_admin._admin_code + " from " + MyLib._d.sml_database_group_admin._table + " order by "+ MyLib._d.sml_database_group_admin._group_code;
            result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            if (result.Tables.Count > 0)
            {
                DataRow[] getRows = result.Tables[0].Select();
                for (int row = 0; row < getRows.Length; row++)
                {
                    string getGroupCode = getRows[row].ItemArray[0].ToString().ToUpper();
                    for (int loop = 0; loop < _dataGroupBuffer.Count; loop++)
                    {
                        _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[loop];
                        if (data._code.CompareTo(getGroupCode) == 0)
                        {
                            data._adminList.Add(getRows[row].ItemArray[1].ToString().ToUpper());
                            _dataGroupBuffer[loop] = (_myDatabaseGroupType)data;
                            break;
                        }
                    } // for
                } // for
            }
            // ลากแล้ววาง
            _listViewAdmin.AllowDrop = true;
            _listViewAdminOwnerGroup.AllowDrop = true;
            //
            _listViewAdmin.ItemDrag += new ItemDragEventHandler(_listViewAdmin_ItemDrag);
            _listViewAdminOwnerGroup.DragOver += new DragEventHandler(_listViewAdminOwnerGroup_DragOver);
            _listViewAdmin.DragDrop += new DragEventHandler(_listViewAdmin_DragDrop);
            //
            _listViewAdminOwnerGroup.ItemDrag += new ItemDragEventHandler(_listViewAdminOwnerGroup_ItemDrag);
            _listViewAdmin.DragOver += new DragEventHandler(_listViewAdmin_DragOver);
            _listViewAdminOwnerGroup.DragDrop += new DragEventHandler(_listViewAdminOwnerGroup_DragDrop);
            //
            _listViewGroup.MouseDoubleClick += new MouseEventHandler(_listViewGroup_MouseDoubleClick);
            listViewRefresh(0);
        }

        void _listViewAdmin_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _listViewAdmin.DoDragDrop(_listViewAdmin, DragDropEffects.All | DragDropEffects.Link);
        }

        void _listViewAdminOwnerGroup_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void _listViewAdmin_DragDrop(object sender, DragEventArgs e)
        {
            string getCode = "";
            ListView.SelectedListViewItemCollection breakfast = _listViewAdminOwnerGroup.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                getCode = item.Tag.ToString();
                _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[_selectGroupID];
                for (int loop = 0; loop < data._adminList.Count; loop++)
                {
                    if (getCode.CompareTo(data._adminList[loop].ToString()) == 0)
                    {
                        data._adminList.RemoveAt(loop);
                        break;
                    }
                }
            }
            listViewRefresh(1);
            _isChange = true;
        }

        void _listViewAdminOwnerGroup_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _listViewAdminOwnerGroup.DoDragDrop(_listViewAdminOwnerGroup, DragDropEffects.All | DragDropEffects.Link);
        }

        void _listViewAdmin_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void _listViewAdminOwnerGroup_DragDrop(object sender, DragEventArgs e)
        {
            // วาง
            if (_selectGroupID == -1)
            {
                // กรุณาเลือกกลุ่มที่ต้องการก่อน
                MessageBox.Show(MyLib._myGlobal._resource("warning15"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string getCode = "";
                ListView.SelectedListViewItemCollection breakfast = _listViewAdmin.SelectedItems;
                foreach (ListViewItem item in breakfast)
                {
                    getCode = item.Tag.ToString();
                    ((_myDatabaseGroupType)_dataGroupBuffer[_selectGroupID])._adminList.Add(getCode);
                }
                listViewRefresh(1);
                _isChange = true;
            }
        }

        private void _groupDatabase_Load(object sender, EventArgs e)
        {
        }

        int _listViewFindGroupCode(string code)
        {
            for (int loop = 0; loop < _dataGroupBuffer.Count; loop++)
            {
                _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[loop];
                if (data._code.ToUpper().CompareTo(code.ToUpper()) == 0)
                {
                    return (loop);
                }
            } // for
            return (-1);
        }

        int _listViewFindUser(string code)
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

        void listViewRefresh(int mode)
        {
            _listViewAdmin._setExStyles();
            _listViewAdminOwnerGroup._setExStyles();
            _listViewGroup._setExStyles();
            if (mode == 0)
            {
                _listViewGroup.Clear();
                for (int loop = 0; loop < _dataGroupBuffer.Count; loop++)
                {
                    _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[loop];
                    ListViewItem myDataView = new ListViewItem();
                    myDataView.Text = data._name + " (" + data._code + ")";
                    myDataView.ImageIndex = 0;
                    myDataView.Group = _listViewGroup.Groups[0];
                    myDataView.Tag = data._code;
                    myDataView.ToolTipText = myDataView.Text;
                    _listViewGroup.Items.Add(myDataView);
                } // for
                for (int loop = 0; loop < _userList.Count; loop++)
                {
                    _myUserType data = (_myUserType)_userList[loop];
                    ListViewItem myDataView = new ListViewItem();
                    myDataView.Text = data._name + " (" + data._code + ")";
                    myDataView.ImageIndex = 1;
                    myDataView.Group = _listViewAdmin.Groups[0];
                    myDataView.Tag = data._code;
                    myDataView.ToolTipText = myDataView.Text;
                    _listViewAdmin.Items.Add(myDataView);
                } // for
            }
            //
            _listViewAdmin.Clear();
            for (int loop = 0; loop < _userList.Count; loop++)
            {
                _myUserType data = (_myUserType)_userList[loop];
                ListViewItem myDataView = new ListViewItem();
                Boolean findCodeInOwnerGroup = false;
                if (_selectGroupID != -1)
                {
                    _myDatabaseGroupType dataGrop = (_myDatabaseGroupType)_dataGroupBuffer[_selectGroupID];
                    for (int sub = 0; sub < dataGrop._adminList.Count; sub++)
                    {
                        if (dataGrop._adminList[sub].ToString().CompareTo(data._code) == 0)
                        {
                            findCodeInOwnerGroup = true;
                            break;
                        }
                    } // for
                }
                if (findCodeInOwnerGroup == false)
                {
                    myDataView.Text = data._name + " (" + data._code + ")";
                    myDataView.ImageIndex = 1;
                    myDataView.Group = _listViewAdmin.Groups[0];
                    myDataView.Tag = data._code;
                    myDataView.ToolTipText = myDataView.Text;
                    _listViewAdmin.Items.Add(myDataView);
                }
            } // for
            if (_selectGroupID != -1)
            {
                _listViewAdminOwnerGroup.Clear();
                _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[_selectGroupID];
                for (int sub = 0; sub < data._adminList.Count; sub++)
                {
                    ListViewItem myDataView = new ListViewItem();
                    _myUserType getUser = new _myUserType();
                    int addr = _listViewFindUser(data._adminList[sub].ToString());
                    if (addr != -1)
                    {
                        getUser = (_myUserType)_userList[addr];
                        myDataView.Text = getUser._name + " (" + getUser._code + ")";
                        myDataView.ImageIndex = 2;
                        myDataView.Group = _listViewAdminOwnerGroup.Groups[0];
                        myDataView.Tag = getUser._code;
                        myDataView.ToolTipText = myDataView.Text;
                        _listViewAdminOwnerGroup.Items.Add(myDataView);
                    }
                } // for
            }
        }

        private void _detailUpdateButton_Click(object sender, EventArgs e)
        {
            if (_detailCode.Text.Length == 0 || _detailName.Text.Length == 0)
            {
                // รายละเอียดไม่ครบ กรุณาตรวจสอบใหม่
                MessageBox.Show(MyLib._myGlobal._resource("warning36"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if (_insertMode)
                {
                    int addr = _listViewFindGroupCode(_detailCode.Text);
                    if (addr != -1)
                    {
                        MyLib._myGlobal._displayWarning(6, "");
                    }
                    else
                    {
                        _myDatabaseGroupType data = new _myDatabaseGroupType();
                        data._code = _detailCode.Text;
                        data._name = _detailName.Text;
                        data._adminList = new ArrayList();
                        _dataGroupBuffer.Add(data);
                        _detailCode.Text = "";
                        _detailName.Text = "";
                        _detailCode.Focus();
                        _isChange = true;
                    }
                }
                else
                {
                    int addr_check = _listViewFindGroupCode(_detailCode.Text);
                    int addr = _listViewFindGroupCode(_currentCode);
                    if (addr_check != -1 && addr_check != addr)
                    {
                        DialogResult result = MyLib._myGlobal._displayWarning(6, "");
                    }
                    else
                    {
                        _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[addr];
                        data._code = _detailCode.Text;
                        data._name = _detailName.Text;
                        _dataGroupBuffer[addr] = (_myDatabaseGroupType)data;
                        _isChange = true;
                    }
                }
                listViewRefresh(0);
            }
        }

        void _listViewGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection breakfast = this._listViewGroup.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                int addr = _listViewFindGroupCode(item.Tag.ToString());
                if (addr != -1)
                {
                    _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[addr];
                    _detailCode.Text = data._code;
                    _detailName.Text = data._name;
                    _currentCode = data._code;
                    _insertMode = false;
                    _detailCode.Focus();
                }
            }
        }

        private void _listViewGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection breakfast = this._listViewGroup.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                int addr = _listViewFindGroupCode(item.Tag.ToString());
                if (addr != -1)
                {
                    _myDatabaseGroupType data = (_myDatabaseGroupType)_dataGroupBuffer[addr];
                    _detailCode.Text = data._code;
                    _detailName.Text = data._name;
                    _selectGroupID = addr;
                    _listViewAdminOwnerGroup.Groups[0].Header = data._name;
                }
            }
            listViewRefresh(1);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_isChange)
            {
                DialogResult result = MyLib._myGlobal._displayWarning(5, "");
                if (result == DialogResult.No) e.Cancel = true;
            }
        }

        private void _viewByIcon_CheckedChanged(object sender, EventArgs e)
        {
            _listViewAdmin.View = View.LargeIcon;
            _listViewAdminOwnerGroup.View = View.LargeIcon;
        }

        private void _viewByList_CheckedChanged(object sender, EventArgs e)
        {
            _listViewAdmin.View = View.Tile;
            _listViewAdminOwnerGroup.View = View.Tile;
        }

        private void _buttonNewGroup_Click(object sender, EventArgs e)
        {
            _insertMode = true;
            _detailCode.Text = "";
            _detailName.Text = "";
            _detailCode.Focus();
        }

        private void _buttonGroup_Click(object sender, EventArgs e)
        {
            Boolean foundItem = false;
            ListView.SelectedListViewItemCollection breakfast = _listViewGroup.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                foundItem = true;
                break;
            }
            if (foundItem)
            {
                //  --  
                string message = MyLib._myGlobal._resource("warning37");
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // ต้องการลบจริงหรือไม่
                result = MessageBox.Show(this, message, "Delete", buttons, MessageBoxIcon.Asterisk);

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in breakfast)
                    {
                        string getCode = item.Tag.ToString();
                        int addr = _listViewFindGroupCode(getCode);
                        if (addr != -1)
                        {
                            _dataGroupBuffer.RemoveAt(addr);
                            _isChange = true;
                            _selectGroupID = -1;
                        }
                    }
                    listViewRefresh(0);
                }
            }
            else
            {
                // กรุณาเลือกรายการที่ต้องการลบ
                MessageBox.Show(MyLib._myGlobal._resource("warning38"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            // save ทั้งหมด
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myQuery.Append("<query>delete from " + MyLib._d.sml_database_group._table + "</query>");
            __myQuery.Append("<query>delete from " + MyLib._d.sml_database_group_admin._table + "</query>");
            for (int row = 0; row < _dataGroupBuffer.Count; row++)
            {
                _myDatabaseGroupType myGroup = (_myDatabaseGroupType)_dataGroupBuffer[row];
                __myQuery.Append("<query>insert into " + MyLib._d.sml_database_group._table + " (" + MyLib._d.sml_database_group._group_code + "," + MyLib._d.sml_database_group._group_name + ") values (\'" + myGroup._code + "\',\'" + myGroup._name + "\')</query>");
                for (int sub = 0; sub < myGroup._adminList.Count; sub++)
                {
                    __myQuery.Append("<query>insert into " + MyLib._d.sml_database_group_admin._table + " (" + MyLib._d.sml_database_group_admin._group_code + "," + MyLib._d.sml_database_group_admin._admin_code + ") values (\'" + myGroup._code + "\',\'" + myGroup._adminList[sub].ToString() + "\')</query>");
                } // for
            } // for
            __myQuery.Append("</node>");
            _myFrameWork myFrameWork = new _myFrameWork();
            string result = myFrameWork._queryList(MyLib._myGlobal._mainDatabase, __myQuery.ToString());
            if (result.Length == 0)
            {
                // บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้
                MessageBox.Show(MyLib._myGlobal._resource("warning39"), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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