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
    public partial class _menupermissions_group : UserControl
    {
        private DataTable _datatablegroup;
        _mainMenuClass _mylistmenuold = null;
        _mainMenuClass _mylistmenuGolbal = null;

        public _menupermissions_group()
        {
            InitializeComponent();
            _myFrameWork __myFrameWork = new _myFrameWork();
            MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);

            string __query = "select " + MyLib._d.sml_group_list._group_code + "," + MyLib._d.sml_group_list._group_name + "," + MyLib._d.sml_group_list._active_status + " from " + MyLib._d.sml_group_list._table+ " order by " + MyLib._d.sml_group_list._group_code;
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
            this._myGrid_group_list._getResource = true;
            _myGrid_group_list._table_name = MyLib._d.sml_group_list._table;
            _myGrid_group_list._addColumn(MyLib._d.sml_group_list._group_code, 1, 50, 50);
            _myGrid_group_list._addColumn(MyLib._d.sml_group_list._group_name, 1, 50, 50);
            _myGrid_group_list._loadFromDataTable(__result.Tables[0]);
            _myGrid_group_list._mouseClick += new MouseClickHandler(_myGrid_group_list__mouseClick);
            _myGrid_group_list.Invalidate();

            this._myGridpermissions._rowNumberWork = true;
            this._myGridpermissions._getResource = true;
            this._myGridpermissions._isEdit = false;
            this._myGridpermissions._table_name = MyLib._d.sml_permissions_group._table;

            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_group._menuname, 1, 15, 60, false, false, false, false);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_group._isread, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_group._isadd, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_group._isdelete, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_group._isedit, 11, 5, 10);
            this._myGridpermissions._addColumn(MyLib._d.sml_permissions_group._menucode, 1, 15, 0, false, true, false, false);
            this._myGridpermissions._addColumn("MenumainName", 1, 15, 40, false, true, false, false);
            this._myGridpermissions.TabStop = false;
            this._myScreen1._maxColumn = 2;
            _datatablegroup = __result.Tables[0];
            string __resourcegroup_code = MyLib._myResource._findResource(MyLib._d.sml_group_list._group_code, "รหัสกลุ่ม")._str;
            string __resourcegroup_Name = MyLib._myResource._findResource(MyLib._d.sml_group_list._group_name, "ชื่อกลุ่ม")._str;
            string __resourcesearch_Name = MyLib._myResource._findResource(MyLib._d.sml_group_list._search, "ค้นหา")._str;
            this._myScreen1._addTextBox(0, 0, 1, 1, MyLib._d.sml_group_list._search, 2, 1, 1, true, false, true);
            this._myScreen1._addTextBox(1, 0, 1, 0, MyLib._d.sml_group_list._group_code, 1, 1, 0, true, false, false);
            this._myScreen1._addTextBox(1, 1, 1, 1, MyLib._d.sml_group_list._group_name, 1, 1, 0, true, false, true);
            
            this._myScreen1._textBoxSearch += new TextBoxSearchHandler(_myScreen1__textBoxSearch);
            this._myScreen1._textBoxChanged += new TextBoxChangedHandler(_myScreen1__textBoxChanged);
            this._myScreen1.Invalidate();
            MyLib._myTextBox _getControlName = (MyLib._myTextBox)this._myScreen1._getControl(MyLib._d.sml_group_list._group_code);
            _getControlName.textBox.ReadOnly = true;
            this.Disposed += new EventHandler(_menupermissions_group_Disposed);
            // string __resourceGroup_code = MyLib._myResource._findResource(MyLib._d.sml_group_list._group_code, "รหัสกลุ่ม")._str;
            //  string __resourceMenu = MyLib._myResource._findResource("Menu", "เมนู")._str;
            // this._myScreen1._addComboBox(0, 0, "Menu", true, _getDatacombo(), true);
            //this._myScreen1._addComboBox(0, 1, MyLib._d.sml_group_list._group_code, true, _getDatagroup_combo(), true);
            //  MyLib._myComboBox __control = (MyLib._myComboBox)_myScreen1._getControl("Menu");
            //  __control.SelectedIndexChanged += new EventHandler(__control_SelectedIndexChanged);
            //  MyLib._myComboBox __control_group = (MyLib._myComboBox)_myScreen1._getControl(MyLib._d.sml_group_list._group_code);
            // __control_group.SelectedIndexChanged += new EventHandler(__control_group_SelectedIndexChanged);         
            this._mylistmenuGolbal = MyLib._myGlobal._listMenuAll;
            this.__gridInit();
        }
       
        void _myScreen1__textBoxChanged(object sender, string name)
        {
             if (name.Equals(MyLib._d.sml_group_list._search)){
                 _searchText();
             }
        }

        void _menupermissions_group_Disposed(object sender, EventArgs e)
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
        DataTable __SearchMenu(_mainMenuClass menu)
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
            }
            return __DataTableTemp;
        }
        void _searchText() {
            try
            {
                string __search = _myScreen1._getDataStr(MyLib._d.sml_group_list._search).ToString();
                MyLib._myTextBox __searchTextbox = ((MyLib._myTextBox)(_myScreen1._getControl(MyLib._d.sml_user_list._search)));
                DataTable __tempmenu;
                if (_mylistmenuold != null)
                {
                    __tempmenu = new DataTable();
                    this._mylistmenuold = __newmenulist(_mylistmenuold);
                    this._myGridpermissions._clear();
                    __tempmenu = __SearchMenu(this._mylistmenuold);
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
                    //DataRow[] __userRow = __tempmenu.Select(MyLib._d.sml_permissions_group._menuname + " like '" + __search + "%'");
                    int _menuWrite = 0;
                    int _menuDelete = 0;
                    int _menuEdit = 0;
                    int _menuRead = 0;
                    string _menucode = "";
                    string _menuname = "";

                    if (__userRow.Length > 0)
                    {
                        for (int __loop = 0; __loop < this._mylistmenuold._MainMenuList.Count; __loop++)
                        {
                            MyLib._menuListClass __subsearch = (MyLib._menuListClass)this._mylistmenuold._MainMenuList[__loop];
                            string __mainmenu = __subsearch._menuMainname;
                            for (int __subloop = 0; __subloop < __subsearch._munsubList.Count; __subloop++)
                            {

                                for (int __xrow = 0; __xrow < __userRow.Length; __xrow++)
                                {

                                    _menuRead = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_group._isread)];
                                    _menuWrite = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_group._isadd)];
                                    _menuDelete = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_group._isdelete)];
                                    _menuEdit = (int)__userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_group._isedit)];
                                    _menucode = __userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_group._menucode)].ToString();
                                    _menuname = __userRow[__xrow].ItemArray[__tempmenu.Columns.IndexOf(MyLib._d.sml_permissions_group._menuname)].ToString();
                                    if (((MyLib._submenuListClass)__subsearch._munsubList[__subloop])._submeid.ToString().Equals(_menucode))
                                    {
                                        int addr = this._myGridpermissions._addRow();
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._menucode, _menucode, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._menuname, _menuname, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isread, _menuRead, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isadd, _menuWrite, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isdelete, _menuDelete, false);
                                        this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isedit, _menuEdit, false);
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
            _searchText();
        }

        void __gridInit()
        {
            this._myGridpermissions._clear();
            MyLib._mainMenuClass __myMenuStart = new _mainMenuClass();
            __myMenuStart = _mylistmenuGolbal;
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
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._menucode, _menucode, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._menuname, _menuname, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isread, _menuRead, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isadd, _menuWrite, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isdelete, _menuDelete, false);
                    this._myGridpermissions._cellUpdate(addr, MyLib._d.sml_permissions_group._isedit, _menuCancel, false);
                    this._myGridpermissions._cellUpdate(addr, 6, __mainmenu, false);

                }

            }
            this._myGridpermissions.Invalidate();

        }
        _mainMenuClass __newmenulist(_mainMenuClass __mylistmenu)
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
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submeid = this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._menucode).ToString();
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submenuname1 = this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._menuname).ToString();
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isRead = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isread).ToString().Equals("0")) ? false : true;
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isAdd = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isadd).ToString().Equals("0")) ? false : true;
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isDelete = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isdelete).ToString().Equals("0")) ? false : true;
                            ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._isEdit = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isedit).ToString().Equals("0")) ? false : true;
                        }

                        // }
                    }

                }
            }
            return __mylist;
            //__MainMenu = new _MainMenuClass();
            //MyLib._MainMenuClass __mylistmenulocal = __mylistmenu;
            //   for (int __main = 0; __main < __mylistmenulocal._MainMenuList.Count; __main++)
            //   {                  
            //           __MenuList = new _MunuListClass();
            //           __MenuList._menuMainname = ((MyLib._MunuListClass)__mylistmenulocal._MainMenuList[__main])._menuMainname.ToString(); ;
            //           string __testmenu = __MenuList._menuMainname;
            //           string __mainmenu = __MenuList._menuMainname;
            //           for (int __grid = 0; __grid < this._myGridpermissions._rowData.Count; __grid++)
            //           {
            //                string __test = this._myGridpermissions._cellGet(__grid, 6).ToString();
            //                if (__test.ToLower().Equals(__testmenu.ToLower()))
            //                 {
            //               __MenuSubList = new _SubmenuListClass();
            //               __MenuSubList._submeid = this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._menucode).ToString();
            //               __MenuSubList._submenuname1 = this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._menuname).ToString();
            //               __MenuSubList._isRead = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isread).ToString().Equals("0")) ? false : true;
            //               __MenuSubList._isAdd = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isadd).ToString().Equals("0")) ? false : true;
            //               __MenuSubList._isDelete = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isdelete).ToString().Equals("0")) ? false : true;
            //               __MenuSubList._isEdit = (this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._isedit).ToString().Equals("0")) ? false : true;
            //               __MenuList._munsubList.Add(__MenuSubList);
            //                }
            //           }
            //           __MainMenu._MainMenuList.Add(__MenuList);
            //       }
            //   return __MainMenu;
        }

        void _myGrid_group_list__mouseClick(object sender, GridCellEventArgs e)
        {
                    
            string __groupcode = this._myGrid_group_list._cellGet(e._row, MyLib._d.sml_group_list._group_code).ToString();
            string __groupname = this._myGrid_group_list._cellGet(e._row, MyLib._d.sml_group_list._group_name).ToString(); ;
            this._myScreen1._setDataStr(MyLib._d.sml_group_list._group_code, __groupcode);
            this._myScreen1._setDataStr(MyLib._d.sml_group_list._group_name, __groupname);
            //this.__gridInit();
            _loadMenu(__groupcode);
            this._myScreen1._focusFirst();
        }
        private MyLib.SMLJAVAWS.imageType[] ListType;
        private MyLib.SMLJAVAWS.imageType TypeImage;
        _mainMenuClass __MainMenu_Group;
        MemoryStream __stream;
        DataTable _createTable()
        {
            DataTable MenuTable = new DataTable("Menu");
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_group._menucode, Type.GetType("System.String")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_group._menuname, Type.GetType("System.String")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_group._isread, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_group._isadd, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_group._isdelete, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn(MyLib._d.sml_permissions_group._isedit, Type.GetType("System.Int32")));
            MenuTable.Columns.Add(new DataColumn("MenumainName", Type.GetType("System.String")));
            return MenuTable;
        }

        void _loadMenu(string _codegroup)
        {


            this._mylistmenuold = null;
            try
            {

                DataTable __DataTableTemp = new DataTable();
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                __DataTableTemp = _createTable();
                byte[] __databyte = new byte[1024];
                string __qurey = "select " + MyLib._d.sml_permissions_group._image_file + " from " + MyLib._d.sml_permissions_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_group._usercode) + " = " + MyLib._myGlobal._addUpper("\'" + _codegroup + "\'");
                __databyte = _myFrameWork._ImageByte(MyLib._myGlobal._mainDatabase, __qurey);
                // this.bmspoUserGrid1.__gridInin();
                if (1024 == __databyte.Length || __databyte.Length == 0)
                {

                    try
                    {

                        this.__gridInit();
                        this._mylistmenuold = this._mylistmenuGolbal;
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
                    __mylistmenuLoad = this._mylistmenuGolbal;

                    for (int __loop = 0; __loop < __mylistmenuLoad._MainMenuList.Count; __loop++)
                    {
                        MyLib._menuListClass __submenu = (MyLib._menuListClass)__mylistmenuLoad._MainMenuList[__loop];
                        string __mainmenu = __submenu._menuMainname;
                        for (int __subloop = 0; __subloop < __submenu._munsubList.Count; __subloop++)
                        {

                            string _menucode = ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submeid.ToString();
                            string _menuname = ((MyLib._submenuListClass)__submenu._munsubList[__subloop])._submenuname1.ToString();
                            DataRow[] __userRow = __DataTableTemp.Select(MyLib._d.sml_permissions_group._menucode + " = '" + _menucode + "'");
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
                                if (_menucode.Equals(this._myGridpermissions._cellGet(__grid, MyLib._d.sml_permissions_group._menucode).ToString()))
                                {
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_group._menuname, _menuname, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_group._isread, _menuRead, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_group._isadd, _menuWrite, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_group._isdelete, _menuDelete, false);
                                    this._myGridpermissions._cellUpdate(__grid, MyLib._d.sml_permissions_group._isedit, _menuEdit, false);
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
                    this._mylistmenuold = __mylistmenuLoad;
                }
            }
            catch (Exception ex)
            {
                this._mylistmenuold = this._mylistmenuGolbal;
                MessageBox.Show(ex.Message, MyLib._myGlobal._resource("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this._myGridpermissions.Invalidate();
        }
       

        string _saveMenu()
        {
            string __savecode = _myScreen1._getDataStr(MyLib._d.sml_group_list._group_code).ToString();
            if (__savecode.Length > 0)
            {
                try
                {

                    StringBuilder __myqueryWhere = new StringBuilder();
                    __MainMenu_Group = new _mainMenuClass();
                    __MainMenu_Group = this.__newmenulist(_mylistmenuold);                   
                    string[] xfeild = { MyLib._d.sml_permissions_group._usercode, MyLib._d.sml_permissions_group._image_file, MyLib._d.sml_permissions_group._guid_code };//insert
                    string xwhere = MyLib._d.sml_permissions_group._usercode;
                    string xTable = MyLib._d.sml_permissions_group._table;//update
                    string insertorupdate = "0";
                    string xswheredata = __savecode;//id code
                    __stream = new MemoryStream();
                    XmlSerializer s = new XmlSerializer(typeof(MyLib._mainMenuClass));
                    s.Serialize(__stream, __MainMenu_Group);
                    //TextWriter w = new StreamWriter(@"c:\test\Menugroupsave.xml");
                    //s.Serialize(w, __MainMenu_Group);
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
                    MessageBox.Show(MyLib._myGlobal._resource("save_success"), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                  //  MessageBox.Show(MyLib._myGlobal._successMessage);
                    this.__gridInit();
                    this._mylistmenuold = null;
                    this._myScreen1._clear();
                    
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString(), MyLib._myGlobal._resource("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string message = MyLib._myGlobal._resource("input_first") + _myScreen1._getLabelName(MyLib._d.sml_user_list._user_code).Replace(":", "");
                MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return "";
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this._saveMenu();

        }
        public void _ischeckbaxAll(string __id)
        {
            int check = 0;
            for (int __grid = 0; __grid < _myGridpermissions._rowData.Count; __grid++)
            {

                if (__grid == 0)
                {
                    if ((int)_myGridpermissions._cellGet(__grid, __id) == 1)
                    {

                    }
                    else
                    {
                        check = 1;
                    }
                }
                _myGridpermissions._cellUpdate(__grid, __id, check, false);

            }
            _myGridpermissions.Invalidate();
        }
        private void _selectread_Click(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_group._isread);
        }

        private void _selectAdd_Click(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_group._isadd);
        }

        private void _selectEdit_Click(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_group._isedit);
        }

        private void _selectdelete_Click(object sender, EventArgs e)
        {
            _ischeckbaxAll(MyLib._d.sml_permissions_group._isdelete);
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
            __htmlStr.Append("<p>Group Code :" + this._myScreen1._getDataStr(MyLib._d.sml_group_list._group_code) +  "</p>");
            __htmlStr.Append("<p>Group Name :" + this._myScreen1._getDataStr(MyLib._d.sml_group_list._group_name) + "</p>");

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
