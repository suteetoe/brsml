using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Collections;

namespace MyLib._databaseManage
{
    public partial class _menupermissions_user : UserControl
    {
        private DataTable _datatablegroup;
        _mainMenuClass __mylistmenuold_user = null;
        _mainMenuClass __mylistmenuGolbal_user = null;
        public _menupermissions_user()
        {
            InitializeComponent();


            _myFrameWork myFrameWork = new _myFrameWork();

            MyLib._myResource._resource = myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
            string query = "select " + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + "," + MyLib._d.sml_user_list._active_status + "," + MyLib._d.sml_user_list._user_password + "," + MyLib._d.sml_user_list._user_level + " from " + MyLib._d.sml_user_list._table+" order by " + MyLib._d.sml_user_list._user_code;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            _myGrid_User_list._getResource = true;
            _myGrid_User_list._table_name = MyLib._d.sml_user_list._table;
            _myGrid_User_list._addColumn(MyLib._d.sml_user_list._user_code, 1, 50, 50);
            _myGrid_User_list._addColumn(MyLib._d.sml_user_list._user_name, 1, 50, 50);            
            _myGrid_User_list._loadFromDataTable(result.Tables[0]);
            _myGrid_User_list._mouseClick += new MouseClickHandler(_myGrid_User_list__mouseClick);
            _myGrid_User_list.Invalidate();

            this._myGridpermissions._rowNumberWork = true;
            this._myGridpermissions._getResource = true;
            this._myGridpermissions._isEdit = false;
            this._myGridpermissions._table_name = MyLib._d.sml_permissions_user._table;            
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_user._menuname, 1, 15, 60, false, false, false, false);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_user._isread, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_user._isadd, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_user._isdelete, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_user._isedit, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_user._menucode, 1, 15, 0, false, true, false, false);
            this._myGridpermissions._addColumn("MenumainName", 1, 15, 40, false, true, false, false);
            this._myGridpermissions.TabStop = false;
            this.Dock = DockStyle.Fill;
            
            this._myGridpermissions.Invalidate();
            this._myGridpermissions._total_show = false;
            this._myGridpermissions.Refresh();
            this._myScreen1._maxColumn = 2;
            _datatablegroup = result.Tables[0];
            string __resourceuser_code = MyLib._myResource._findResource(MyLib._d.sml_user_list._user_code, "รหัสผู้ใช้")._str;
            string __resourceuser_Name = MyLib._myResource._findResource(MyLib._d.sml_user_list._user_name, "ชื่อผู้ใช้")._str;
            string __resourceSearch_Name = MyLib._myResource._findResource(MyLib._d.sml_user_list._user_name, "ค้นหา")._str;
            string __resourceMenu = MyLib._myResource._findResource("Menu", "เมนู")._str;
            this._myScreen1._addTextBox(0, 0, 1, 1, MyLib._d.sml_user_list._search, 2, 1, 1, true, false, true);
            this._myScreen1._addTextBox(1, 0, 1, 0, MyLib._d.sml_user_list._user_code, 1, 1, 0, true, false, false);
            this._myScreen1._addTextBox(1, 1, 1, 1, MyLib._d.sml_user_list._user_name, 1, 1, 0, true, false, true);
            
            this._myScreen1._textBoxSearch += new TextBoxSearchHandler(_myScreen1__textBoxSearch);
            this._myScreen1._textBoxChanged += new TextBoxChangedHandler(_myScreen1__textBoxChanged);
            this._myScreen1.Invalidate();
            MyLib._myTextBox _getControlName = (MyLib._myTextBox)this._myScreen1._getControl(MyLib._d.sml_user_list._user_code);
            _getControlName.textBox.ReadOnly = true;
            this.Disposed += new EventHandler(_menupermissions_user_Disposed);
            this.__mylistmenuGolbal_user = MyLib._myGlobal._listMenuAll;
            this.__gridInit_user();
        }

        void _myScreen1__textBoxChanged(object sender, string name)
        {
            if (name.Equals(MyLib._d.sml_user_list._search))
            {
                _searchText_user();
            }
        }

        void _menupermissions_user_Disposed(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < MyLib._myGlobal._listMenuAll._MainMenuList.Count; __loop++)
            {
                MyLib._menuListClass __submenu = (MyLib._menuListClass)MyLib._myGlobal._listMenuAll._MainMenuList[__loop];
                string __mainmenu = __submenu._menuMainname;
                for (int __subloop = 0; __subloop < __submenu._munsubList.Count; __subloop++)
                {
                    ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isRead = false;
                    ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isAdd = false;
                    ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isDelete = false;
                    ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isEdit = false;

                }
            }
        }

        void _myGrid_User_list__mouseClick(object sender, GridCellEventArgs e)
        {
            string __userid = this._myGrid_User_list._cellGet(e._row, MyLib._d.sml_user_list._user_code).ToString();
            string __username = this._myGrid_User_list._cellGet(e._row, MyLib._d.sml_user_list._user_name).ToString(); ;
            _myScreen1._setDataStr(MyLib._d.sml_user_list._user_code, __userid);
            _myScreen1._setDataStr(MyLib._d.sml_user_list._user_name, __username);
            _loadMenu_user(__userid);
            _myScreen1._focusFirst();
        }
        _mainMenuClass __newmenulist_user(_mainMenuClass __mylistmenu)
        {
            _mainMenuClass __mylist = new _mainMenuClass();
            __mylist = __mylistmenu;
            for (int __loop = 0; __loop < __mylist._MainMenuList.Count; __loop++)
            {
                MyLib._menuListClass __submenu = (MyLib._menuListClass)__mylist._MainMenuList[__loop];
                string __mainmenu = __submenu._menuMainname;
                for (int __subloop = 0; __subloop < __submenu._munsubList.Count; __subloop++)
                {
                    string _menucode = ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submeid.ToString();
                    //string _menuname = ((MyLib._SubmenuListClass)__sub._munsubList[__subloop])._submenuname1.ToString();
                    //int _menuRead = (((MyLib._SubmenuListClass)__sub._munsubList[__subloop])._isRead == true) ? 1 : 0;
                    //int _menuWrite = (((MyLib._SubmenuListClass)__sub._munsubList[__subloop])._isAdd == true) ? 1 : 0;
                    //int _menuDelete = (((MyLib._SubmenuListClass)__sub._munsubList[__subloop])._isDelete == true) ? 1 : 0;
                    //int _menuEdit = (((MyLib._SubmenuListClass)__sub._munsubList[__subloop])._isEdit == true) ? 1 : 0;
                    for (int __grid = 0; __grid < this._myGridpermissions._rowData.Count; __grid++)
                    {
                        if (_menucode.Equals(this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._menucode).ToString()))
                        {
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submeid = this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_user._menucode).ToString();
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submenuname1 = this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_user._menuname).ToString();
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isRead = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_user._isread).ToString().Equals("0")) ? false : true;
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isAdd = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_user._isadd).ToString().Equals("0")) ? false : true;
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isDelete = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_user._isdelete).ToString().Equals("0")) ? false : true;
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isEdit = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_user._isedit).ToString().Equals("0")) ? false : true;
                        }

                        // }
                    }

                }
            }
            return __mylist;
            
        }
        DataTable __SearchMenu_user(_mainMenuClass menu)
        {
            DataTable __DataTableTemp = new DataTable();
            _mainMenuClass __menu = new _mainMenuClass();
            __menu = menu;
            __DataTableTemp = _createTable();
            if (menu != null)
            {
                for (int __main = 0; __main < __menu._MainMenuList.Count; __main++)
                {
                    // __MenuList = new _MunuListClass();
                    //  __MenuList._menuMainname = ((MyLib._MunuListClass)menu._MainMenuList[__main])._menuMainname.ToString(); ;
                    string __mainmenu = ((MyLib._menuListClass)__menu._MainMenuList[__main])._menuMainname.ToString();
                    MyLib._menuListClass __sub = (MyLib._menuListClass)__menu._MainMenuList[__main];
                    for (int __subloop = 0; __subloop < __sub._munsubList.Count; __subloop++)
                    {

                        string _menucode = ((MyLib._submenuListClass)__sub._munsubList[__subloop])._submeid.ToString();
                        string _menuname = ((MyLib._submenuListClass)__sub._munsubList[__subloop])._submenuname1.ToString();
                        int _menuRead = (((MyLib._submenuListClass)__sub._munsubList[__subloop])._isRead == true) ? 1 : 0;
                        int _menuAdd = (((MyLib._submenuListClass)__sub._munsubList[__subloop])._isAdd == true) ? 1 : 0;
                        int _menuDelete = (((MyLib._submenuListClass)__sub._munsubList[__subloop])._isDelete == true) ? 1 : 0;
                        int _menuEdit = (((MyLib._submenuListClass)__sub._munsubList[__subloop])._isEdit == true) ? 1 : 0;

                        DataRow newRow = __DataTableTemp.NewRow();
                        newRow[MyLib._d.sml_permissions_user._menucode] = _menucode;
                        newRow[MyLib._d.sml_permissions_user._menuname] = _menuname;
                        newRow[MyLib._d.sml_permissions_user._isadd] = _menuAdd;
                        newRow[MyLib._d.sml_permissions_user._isread] = _menuRead;
                        newRow[MyLib._d.sml_permissions_user._isdelete] = _menuDelete;
                        newRow[MyLib._d.sml_permissions_user._isedit] = _menuEdit;
                        newRow["MenumainName"] = __mainmenu;
                        __DataTableTemp.Rows.Add(newRow);
                    }

                }
            }
            return __DataTableTemp;
        }
        void _searchText_user() {
            try
            {
                string __search = _myScreen1._getDataStr(MyLib._d.sml_user_list._search).ToString();
                MyLib._myTextBox __searchTextbox = ((MyLib._myTextBox)(_myScreen1._getControl(MyLib._d.sml_user_list._search)));
                
                DataTable __tempmenu;
                if (this.__mylistmenuold_user != null)
                {
                    __tempmenu = new DataTable();
                    //    __mylistmenuold = new _MainMenuClass();
                    this.__mylistmenuold_user = __newmenulist_user(this.__mylistmenuold_user);
                    this._myGridpermissions._clear();
                    __tempmenu = __SearchMenu_user(this.__mylistmenuold_user);
                    if (__searchTextbox.TextBox.Text.Length > 0)
                    {
                        while (__searchTextbox.TextBox.Text[__searchTextbox.TextBox.Text.Length - 1] == ' ')
                        {
                            __searchTextbox.TextBox.Text = __searchTextbox.TextBox.Text.Remove(__searchTextbox.TextBox.Text.Length - 1, 1);
                            if (__searchTextbox.TextBox.Text.Length == 0)
                            {
                                break;
                            }
                        }
                    }
                    StringBuilder __where = new StringBuilder();

                    if (__searchTextbox.TextBox.Text[0] == '+')
                    {
                        __where.Append(string.Concat(" like \'", __searchTextbox.TextBox.Text.Remove(0, 1), "%\'"));
                    }
                    else
                    {
                        __where.Append(string.Concat(" like \'%", __searchTextbox.TextBox.Text, "%\'"));
                    }
                    //label1.Text = __where.ToString();
                 //   DataRow[] __userRow = __tempmenu.Select(MyLib._d.sml_permissions_user._menuname + " like '" + __where.ToString() + "%'");
                    DataRow[] __userRow = __tempmenu.Select(MyLib._d.sml_permissions_user._menuname + __where.ToString());
                    int _menuWrite = 0;
                    int _menuDelete = 0;
                    int _menuEdit = 0;
                    int _menuRead = 0;
                    string _menucode = "";
                    string _menuname = "";

                    if (__userRow.Length > 0)
                    {
                        for (int __loop = 0; __loop < this.__mylistmenuold_user._MainMenuList.Count; __loop++)
                        {
                            MyLib._menuListClass __subsearch = (MyLib._menuListClass)this.__mylistmenuold_user._MainMenuList[__loop];
                            string __mainmenu = __subsearch._menuMainname;
                            for (int __subloop = 0; __subloop < __subsearch._munsubList.Count; __subloop++)
                            {

                                for (int __xrow = 0; __xrow < __userRow.Length; __xrow++)
                                {

                                    _menuRead = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_user._isread)];
                                    _menuWrite = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_user._isadd)];
                                    _menuDelete = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_user._isdelete)];
                                    _menuEdit = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_user._isedit)];
                                    _menucode = __userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_user._menucode)].ToString();
                                    _menuname = __userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_user._menuname)].ToString();
                                    if (((MyLib._submenuListClass)__subsearch._munsubList[__subloop])._submeid.ToString().Equals(_menucode))
                                    {
                                        int addr = this._myGridpermissions._addRow();
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._menucode, _menucode, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._menuname, _menuname, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isread, _menuRead, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isadd, _menuWrite, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isdelete, _menuDelete, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isedit, _menuEdit, false);
                                    }
                                }
                            }
                        }
                        this._myGridpermissions.Invalidate();
                    }


                }
                else
                {
                    //   __tempmenu = new DataTable();
                    //    __mylistmenuDefualt = MyLib._myGlobal._listMenuAll;
                    //    __tempmenu = __SearchMenu(__mylistmenuDefualt);
                    //    __mylistmenuDefualt = __newmenulist(__mylistmenuDefualt);
                }


            }
            catch
            {

            }
        }
        void _myScreen1__textBoxSearch(object sender)
        {
            _searchText_user();
        }
        public DataTable _createTable()
        {
            DataTable MenuTable = new DataTable( "Menu");

            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_user._menucode, Type.GetType("System.String")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_user._menuname, Type.GetType("System.String")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_user._isread, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_user._isadd, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_user._isdelete, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_user._isedit, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn("MenumainName", Type.GetType("System.String")));

            return MenuTable;
        }
        public void _ischeckbaxAll(string __id)
        {
            int check = 0;
            for (int __grid = 0; __grid < this._myGridpermissions._rowData.Count; __grid++)
            {

                if (__grid == 0)
                {
                    if ((int)this._myGridpermissions._cellGet(__grid, __id) == 1)
                    {

                    }
                    else
                    {
                        check = 1;
                    }
                }
                this._myGridpermissions._cellUpdate(__grid, __id, check, false);

            }
            this._myGridpermissions.Invalidate();
        }
        public void __gridInit_user()
        {
            this._myGridpermissions._clear();
            MyLib._mainMenuClass __myMenuStart = new _mainMenuClass();
            __myMenuStart = __mylistmenuGolbal_user;
            for (int __loop = 0; __loop < __myMenuStart._MainMenuList.Count; __loop++)
            {
                MyLib._menuListClass __subMenu = (MyLib._menuListClass)__myMenuStart._MainMenuList[__loop];
                string __mainmenu = __subMenu._menuMainname;
                for (int __subloop = 0; __subloop < __subMenu._munsubList.Count; __subloop++)
                {
                    string _menucode = ((MyLib._submenuListClass)__subMenu._munsubList[__subloop])._submeid.ToString();
                    string _menuname = ((MyLib._submenuListClass)__subMenu._munsubList[__subloop])._submenuname1.ToString();
                    int _menuRead = 0;
                    int _menuWrite = 0;
                    int _menuDelete = 0;
                    int _menuCancel = 0;

                    int addr = this._myGridpermissions._addRow();
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._menucode, _menucode, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._menuname, _menuname, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isread, _menuRead, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isadd, _menuWrite, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isdelete, _menuDelete, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_user._isedit, _menuCancel, false);
                    this._myGridpermissions._cellUpdate(addr, 6, __mainmenu, false);

                }

            }
            this._myGridpermissions.Invalidate();
        }
    
     
        private MyLib.SMLJAVAWS.imageType[] ListType;
        private MyLib.SMLJAVAWS.imageType TypeImage;
        _mainMenuClass __MainMenu_user;
        MemoryStream __stream;
        string _saveMenu_user()
        {
            string __savecode = _myScreen1._getDataStr(MyLib._d.sml_user_list._user_code).ToString();//MyLib._d.sml_user_list._user_code
            if (__savecode.Length > 0)
            {
                try
                {

                    StringBuilder __myqueryWhere = new StringBuilder();
                    __MainMenu_user = new _mainMenuClass();
                    __MainMenu_user = this.__newmenulist_user(__mylistmenuold_user);                   
                    string[] xfeild = { MyLib._d.sml_permissions_user._usercode, MyLib._d.sml_permissions_user._image_file, MyLib._d.sml_permissions_user._guid_code };//insert
                    string xwhere = MyLib._d.sml_permissions_user._usercode;
                    string xTable = MyLib._d.sml_permissions_user._table;//update
                    string insertorupdate = "0";
                    string xswheredata = __savecode;//id code
                    __stream = new MemoryStream();
                    XmlSerializer s = new XmlSerializer(typeof(MyLib._mainMenuClass));
                    s.Serialize(__stream, __MainMenu_user);
                    //TextWriter w = new StreamWriter(@"c:\test\Menuusersave.xml");
                    //s.Serialize(w, __MainMenu_user);
                    //w.Close();
                    byte[] buffer = __stream.GetBuffer();
                    byte[] getData;
                    ArrayList xlist = new ArrayList();
                    getData = buffer;
                    TypeImage = new MyLib.SMLJAVAWS.imageType();
                    TypeImage._databyteImage = getData;
                    TypeImage._code = __savecode;
                    xlist.Add(TypeImage);
                    ListType = ((MyLib.SMLJAVAWS.imageType[])xlist.ToArray(typeof(MyLib.SMLJAVAWS.imageType)));
                    MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                    string x_result = myFrameWork._SaveImageList(xswheredata, MyLib._myGlobal._mainDatabase, ListType, insertorupdate, xfeild, xTable, xwhere, xswheredata);
                    // บันทึกข้อมูลเสร็จเรียบร้อย
                    MessageBox.Show(MyLib._myGlobal._resource("save_success"), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.__gridInit_user();
                    this.__mylistmenuold_user = null;
                    this._myScreen1._clear();
                   
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("input_first") + _myScreen1._getLabelName(MyLib._d.sml_user_list._user_code).Replace(":", ""), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return "";
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
        //    if ((keyData & Keys.Control) == Keys.Control)
        //    {
        //        switch (keyCode)
        //        {
        //            case Keys.Home:
        //                {
        //                    this._myScreen1._focusFirst();
        //                    return true;
        //                }
        //        }
        //    }
        //    if (keyData == Keys.Escape)
        //    {
        //        this.Dispose();
        //        return true;
        //    }
        //    if (keyData == Keys.F12)
        //    {
        //        this._saveMenu();
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
     
        void _loadMenu_user( string _user_code)
        {
            try
            {
                DataTable __DataTableTemp = new DataTable();
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                __DataTableTemp = _createTable();
                byte[] __databyte = new byte[1024];
                string __qurey = "select " + MyLib._d.sml_permissions_user._image_file + " from " + MyLib._d.sml_permissions_user._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_user._usercode) + " =" + MyLib._myGlobal._addUpper("\'" + _user_code + "\'");
                __databyte = _myFrameWork._ImageByte(MyLib._myGlobal._mainDatabase, __qurey);
                // this.bmspoUserGrid1.__gridInin();
                if (1024 == __databyte.Length || __databyte.Length == 0)
                {
                    this._myGridpermissions._clear();
                    try
                    {
                        this.__gridInit_user();
                        this.__mylistmenuold_user = this.__mylistmenuGolbal_user;
                    }
                    catch (Exception ex)
                    {
                        string __ex = ex.Message;
                    }
                }
                else
                {
                    __stream = new MemoryStream(__databyte);
                    XmlSerializer __serializer = new XmlSerializer(typeof(MyLib._mainMenuClass));
                    _mainMenuClass __loadMenufromdatabase = (_mainMenuClass)__serializer.Deserialize(__stream);
                    _mainMenuClass __mylistmenuLoad = __loadMenufromdatabase;
                    __stream = new MemoryStream(__databyte);                                      
                   // XmlSerializer s = new XmlSerializer(typeof(MyLib._mainMenuClass));
                   // TextReader r = new StreamReader(@"c:\test\" + _user_code + ".xml");
                   // __loadMenufromdatabase = (MyLib._mainMenuClass)s.Deserialize(r);
                   // _mainMenuClass __mylistmenuLoad = __loadMenufromdatabase;
                   // r.Close();
                    for (int __loop = 0; __loop < __mylistmenuLoad._MainMenuList.Count; __loop++)
                    {
                        MyLib._menuListClass __subLoad = (MyLib._menuListClass)__mylistmenuLoad._MainMenuList[__loop];
                        string __mainmenu = __subLoad._menuMainname;
                        for (int __subloop = 0; __subloop < __subLoad._munsubList.Count; __subloop++)
                        {

                            string _menucode = ((MyLib._submenuListClass)__subLoad._munsubList[__subloop])._submeid.ToString();
                            string _menuname = ((MyLib._submenuListClass)__subLoad._munsubList[__subloop])._submenuname1.ToString();
                            int _menuRead = (((MyLib._submenuListClass)__subLoad._munsubList[__subloop])._isRead == true) ? 1 : 0;
                            int _menuAdd = (((MyLib._submenuListClass)__subLoad._munsubList[__subloop])._isAdd == true) ? 1 : 0;
                            int _menuDelete = (((MyLib._submenuListClass)__subLoad._munsubList[__subloop])._isDelete == true) ? 1 : 0;
                            int _menuEdit = (((MyLib._submenuListClass)__subLoad._munsubList[__subloop])._isEdit == true) ? 1 : 0;

                            DataRow newRow = __DataTableTemp.NewRow();
                            newRow[MyLib._d.sml_permissions_group._menucode] = _menucode;
                            newRow[MyLib._d.sml_permissions_group._menuname] = _menuname;
                            newRow[MyLib._d.sml_permissions_group._isadd] = _menuAdd;
                            newRow[MyLib._d.sml_permissions_group._isread] = _menuRead;
                            newRow[MyLib._d.sml_permissions_group._isdelete] = _menuDelete;
                            newRow[MyLib._d.sml_permissions_group._isedit] = _menuEdit;
                            newRow["MenumainName"] = __mainmenu;
                            __DataTableTemp.Rows.Add(newRow);
                        }
                    }
                    __stream.Close();
                    __mylistmenuLoad = new _mainMenuClass();
                    __mylistmenuLoad = this.__mylistmenuGolbal_user;

                    for (int __loop = 0; __loop < __mylistmenuLoad._MainMenuList.Count; __loop++)
                    {
                        MyLib._menuListClass __submenu = (MyLib._menuListClass)__mylistmenuLoad._MainMenuList[__loop];
                        string __mainmenu = __submenu._menuMainname;
                        for (int __subloop = 0; __subloop < __submenu._munsubList.Count; __subloop++)
                        {

                            string _menucode = ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submeid.ToString();
                            string _menuname = ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submenuname1.ToString();
                            DataRow[] __userRow = __DataTableTemp.Select(MyLib._d.sml_permissions_user._menucode + " = '" + _menucode + "'");
                            int _menuWrite = 0;
                            int _menuDelete = 0;
                            int _menuEdit = 0;
                            int _menuRead = 0;
                            if (__userRow.Length > 0)
                            {
                                _menuRead = (int)__userRow[0].ItemArray[2];
                                _menuWrite = (int)__userRow[0].ItemArray[3];
                                _menuDelete = (int)__userRow[0].ItemArray[4];
                                _menuEdit = (int)__userRow[0].ItemArray[5];
                            }
                            for (int __grid = 0; __grid < this._myGridpermissions._rowData.Count; __grid++)
                            {
                                if (_menucode.Equals(this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_user._menucode).ToString()))
                                {
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_user._menuname, _menuname, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_user._isread, _menuRead, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_user._isadd, _menuWrite, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_user._isdelete, _menuDelete, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_user._isedit, _menuEdit, false);
                                    this._myGridpermissions._cellUpdate(__grid, 6, __mainmenu, false);
                                }

                            }
                            //int addr = this._myGridpermissions._addRow();
                            //this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._menucode, _menucode, false);
                            //this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._menuname, _menuname, false);
                            //this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isread, _menuRead, false);
                            //this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isadd, _menuWrite, false);
                            //this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isdelete, _menuDelete, false);
                            //this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isedit, _menuEdit, false);
                            //this._myGridpermissions._cellUpdate(addr, 6, __mainmenu, false);
                        }
                    }
                    this.__mylistmenuold_user = __mylistmenuLoad;
                }
            }
            catch (Exception ex)
            {
                this.__mylistmenuold_user = this.__mylistmenuGolbal_user;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this._myGridpermissions.Invalidate();
        }
        


        private void _selectdelete_Click(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_user._isdelete);
        }
        private void _selectread_Click_1(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_user._isread);
        }

        private void _selectAdd_Click_1(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_user._isadd);
        }

        private void _selectEdit_Click_1(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_user._isedit);
        }

        private void ButtonSave_Click_1(object sender, EventArgs e)
        {
            this._saveMenu_user();
        }

        private void ButtonExit_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _selectPrint_Click(object sender, EventArgs e)
        {
            _reportHTMLForm __html = new _reportHTMLForm();
            // gen report 

            StringBuilder __htmlStr = new StringBuilder();
            __htmlStr.Append(@"
<head>
<style type='text/css'>
body {
	font-family: Tahoma,Arial, Helvetica, sans-serif;
    font-size:12px;
}

table {
	font-family: Tahoma,Arial, Helvetica, sans-serif;
    font-size:12px;
}

</style>
</head>
<body>            
            ");

            // group name
            __htmlStr.Append("<p>USER Code :" + this._myScreen1._getDataStr(MyLib._d.sml_user_list._user_code) + "</p>");
            __htmlStr.Append("<p>User Name :" + this._myScreen1._getDataStr(MyLib._d.sml_user_list._user_name) + "</p>");

            // fetch loop
            __htmlStr.Append("<table border=\"1\" cellspacing=\'0\' width=\'800\'> ");
            __htmlStr.Append(@"
<tr>
    <th >Menu Name</th>
    <th  width=70 >Read</th>
    <th  width=70 >Add</th>
    <th  width=70 >Delete</th>
    <th  width=70 >Edit</th>
</tr>");
            // fetch menu name
            for (int __row = 0; __row < this._myGridpermissions._rowData.Count; __row++)
            {
                string __menuName = this._myGridpermissions._cellGet(__row, MyLib._d.sml_permissions_group._menuname).ToString();
                string __isRead = this._myGridpermissions._cellGet(__row, MyLib._d.sml_permissions_group._isread).ToString();
                string __isAdd = this._myGridpermissions._cellGet(__row, MyLib._d.sml_permissions_group._isadd).ToString();
                string __isDelete = this._myGridpermissions._cellGet(__row, MyLib._d.sml_permissions_group._isdelete).ToString();
                string __isEdit = this._myGridpermissions._cellGet(__row, MyLib._d.sml_permissions_group._isedit).ToString();


                __htmlStr.Append(@"
<tr>
    <td>" + __menuName + @"</td>
    <td align=center >" + __isRead + @"</td>
    <td align=center >" + __isAdd + @"</td>
    <td align=center >" + __isDelete + @"</td>
    <td align=center >" + __isEdit + @"</td>
</tr>");
            }

            __htmlStr.Append("</table>");

            __htmlStr.Append("</body>");

            __html._htmlWebBrowser.DocumentText = __htmlStr.ToString();

            __html.ShowDialog(this);
        }

    }
 
}

