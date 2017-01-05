using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;


namespace MyLib._databaseManage
{
    public partial class _selectDatabase : System.Windows.Forms.Form
    {
        string _provider_code = "provider_code";
        string _database_group = "database_group";
        string _user_code = "user_code";
        string _user_password = "user_password";
        string _column_name_1 = "Code";
        string _column_name_2 = "Name";
        //
        string _dataGroup = "";
        string _userCode = "";
        string _providerCode = "";
        // Resource
        //string[] _resource_screen_name = { "mySML (เลือกข้อมูล)", "mySML (Select Database)" };
        // string[] _resource_provider_code = { "รหัสผู้ให้บริการ", "Provider Code" };
        // string[] _resource_database_group = { "กลุ่มข้อมูล", "Database Group" };
        // string[] _resource_user_code = { "รหัสผู้ใช้", "User Code" };
        // string[] _resource_user_password = { "รหัสผ่าน", "Password" };
        string[] _resource_column_name_1 = { "รหัสบริษัท", "Company Code" };
        string[] _resource_column_name_2 = { "ชื่อบริษัท", "Company Name" };
        // Admin Menu
        string _system_provider_config = "provider_config";
        string _admin_menu_server_setup = "webservice_setup";
        string _admin_menu_grou_and_user = "user_and_group";
        string _admin_menu_group_manage = "group_manage";
        string _admin_menu_database_group = "database_group";
        string _admin_menu_create_new_database = "create_new_database";
        string _admin_menu_link_database = "link_database";
        string _admin_menu_verify_database = "verify_database";
        string _admin_menu_shrink_database = "shrink_database";
        string _admin_menu_resource_edit = "resource_edit";
        string _admin_menu_change_password = "admin_change_password";
        string _admin_menu_database_access = "database_access";
        string _server_menu_change_password = "server_change_password";
        string _admin_menu_group_permissions = "group_permissions";
        string _admin_menu_user_permissions = "user_permissions";
        string _admin_menu_trasnfer_data = "transfer_database";
        string _register = "register";

        public _selectDatabase()
        {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._history.DropDownItemClicked += _history_DropDownItemClicked;
                this._listViewAdminMenu.Click += new EventHandler(_listViewAdminMenu_Click);
                this._listViewServerMenu.Click += new EventHandler(_listViewServerSetup_Click);
                //
                string __tempTable = "Data";
                this._screenTop.Width = (_gridDatabaseList.Width - _buttonLogin.Width) - 15;
                _screenTop.Location = new Point(10, 10);
                _screenTop._maxColumn = 1;
                _screenTop._getResource = false;
                _screenTop._table_name = __tempTable;
                _screenTop._addTextBox(0, 0, 1, 0, _provider_code, 1, 25, 0, true, false);
                _screenTop._addTextBox(1, 0, 1, 0, _database_group, 1, 25, 0, true, false);
                _screenTop._addTextBox(2, 0, 1, 0, _user_code, 1, 25, 0, true, false);
                _screenTop._addTextBox(3, 0, 1, 0, _user_password, 1, 25, 0, true, true);
                _buttonLogin.Location = new Point(_buttonLogin.Location.X, (_screenTop.Height + 10) - _buttonLogin.Height);
                _gridDatabaseList.Location = new Point(_gridDatabaseList.Location.X, (_screenTop.Height + 15));
                _gridDatabaseList.Height = (this.Height - _gridDatabaseList.Location.Y) - 120;
                _gridDatabaseList._isEdit = false;
                _gridDatabaseList._mouseClick += new MouseClickHandler(_gridDatabaseList__mouseClick);
                this._gridDatabaseList._table_name = __tempTable;
                this._gridDatabaseList._addColumn(_column_name_1, 1, 10, 30);
                this._gridDatabaseList._addColumn(_column_name_2, 1, 255, 70);
                // web service
                this._gridWebservice._table_name = __tempTable;
                this._gridWebservice._addColumn("WebserviceURL", 1, 250, 100);

                // for Admin
                this._screenTopAdmin.Width = this._screenTop.Width;
                _screenTopAdmin.Location = new Point(10, 10);
                _screenTopAdmin._maxColumn = 1;
                _screenTopAdmin._getResource = false;
                _screenTopAdmin._table_name = __tempTable;
                _screenTopAdmin._addTextBox(0, 0, 1, 0, _provider_code, 1, 25, 0, true, false);
                _screenTopAdmin._addTextBox(1, 0, 1, 0, _user_code, 1, 25, 0, true, false);
                _screenTopAdmin._addTextBox(2, 0, 1, 0, _user_password, 1, 25, 0, true, true);
                _buttonLoginAdmin.Location = new Point(_buttonLoginAdmin.Location.X, (_screenTopAdmin.Height + 10) - _buttonLoginAdmin.Height);
                _selectDatabaseChangeResource(0);
                // ดึง Profile ล่าสุด
                _loadLastProfile();
                // ค่าเริ่มต้น (Profle)
                _screenTop._setDataStr(_provider_code, _providerCode);
                _screenTop._setDataStr(_database_group, _dataGroup);
                _screenTop._setDataStr(_user_code, _userCode);
                _screenTop._setDataStr(_user_password, "");
                _screenTopAdmin._setDataStr(_user_code, _userCode);
                //
                _languageSelect(MyLib._myGlobal._language);

                // oem
                // imex logo
                if ((MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountProfessional)
                    && MyLib._myGlobal._OEMVersion.Equals("imex"))
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.logoIMS;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccount)
                {
                    //this._logoPicture.Image = global::MyLib.Properties.Resources.sml_account_pro_logo;
                    if (MyLib._myGlobal._programName == "Sea And Hill Account")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.seaandhillacc;
                    }
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountProfessional)
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.sml_account_pro_logo;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLAccountPOS)
                {
                    if (MyLib._myGlobal._programName == "Sea And Hill Account POS")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.seaandhillacc;
                    }
                    else
                    {
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                        {
                            this._logoPanel.Visible = false;
                        }
                        else
                            this._logoPicture.Image = global::MyLib.Properties.Resources.sml_accountpos_logo;
                    }
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOS && MyLib._myGlobal._subVersion == 0)
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.sml_pos_suriwong;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.IMSPOS)
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.imspos;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOS)
                {
                    if (MyLib._myGlobal._programName == "Sea And Hill POS")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.seaandhillpos;
                    }
                    else if (MyLib._myGlobal._programName == "SML POS Cashier")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.sml_pos_cashier;
                    }
                    else if (MyLib._myGlobal._programName == "SML POS Manage")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.sml_pos_manager;
                    }
                    else if (MyLib._myGlobal._programName == "mPay POS")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.mpayposlogo;
                    }
                    else
                        this._logoPicture.Image = global::MyLib.Properties.Resources.sml_pos_logo;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOSLite)
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.sml_pos_lite_logo;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLIMS)
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.logoIMS;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOSMED)
                {
                    if (MyLib._myGlobal._programName == "IMS POS Medical")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.imsposmedical;
                    }
                    else
                        this._logoPicture.Image = global::MyLib.Properties.Resources.sml_posmed_logo;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOSStarter)
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.sml_posstarter_logo;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong)
                {
                    if (MyLib._myGlobal._programName == "Sea And Hill Restaurant")
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.seaandhillrestaurant;
                    }
                    else
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.sml_tomyumgoong_logo;
                    }
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro)
                {
                    if (MyLib._myGlobal._programName.Equals("Saby Pay Restaurant"))
                    {
                        this._logoPicture.Image = global::MyLib.Properties.Resources.sabypayrestaurant;
                    }
                    else
                        this._logoPicture.Image = global::MyLib.Properties.Resources.sml_tomyumgoong_logo;
                }
                else if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.IMSAccountPro)
                {
                    this._logoPicture.Image = global::MyLib.Properties.Resources.sml_tomyumgoong_logo;
                }
                else
                {
                    this._logoPanel.Visible = false;
                }
            }
        }

        private void _history_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string __name = e.ClickedItem.Text;
            int __foundAddr = -1;
            for (int __loop = 0; __loop < MyLib._myGlobal._loginHistory.__details.Count; __loop++)
            {
                if (MyLib._myGlobal._loginHistory.__details[__loop].__name.Equals(__name))
                {
                    __foundAddr = __loop;
                    break;
                }
            }
            if (__foundAddr != -1)
            {
                _loginHistoryDetailClass __detail = MyLib._myGlobal._loginHistory.__details[__foundAddr];
                //
                _gridWebservice._clear();
                for (int __row = 0; __row < __detail.__url.Count; __row++)
                {
                    _gridWebservice._addRow();
                    _gridWebservice._cellUpdate(__row, 0, __detail.__url[__row], false);
                }
                //
                this._profileNameTextBox.Text = __detail.__name;
                _providerCode = __detail.__provider;
                _dataGroup = __detail.__group;
                _userCode = __detail.__user;
                MyLib._myGlobal._proxyUrl = __detail.__proxyUrl;
                MyLib._myGlobal._proxyUsed = __detail.__proxyUsed;
                MyLib._myGlobal._proxyUser = __detail.__proxyUser;
                MyLib._myGlobal._proxyPassword = __detail.__proxyPassword;
                switch (__detail.__languageNumber)
                {
                    case 0: MyLib._myGlobal._language = _languageEnum.Thai; break;
                    case 1: MyLib._myGlobal._language = _languageEnum.English; break;
                    case 2: MyLib._myGlobal._language = _languageEnum.Malayu; break;
                    case 3: MyLib._myGlobal._language = _languageEnum.Chainese; break;
                    case 4: MyLib._myGlobal._language = _languageEnum.India; break;
                    case 5: MyLib._myGlobal._language = _languageEnum.Lao; break;
                }
                //
                _screenTop._setDataStr(_provider_code, _providerCode);
                _screenTop._setDataStr(_database_group, _dataGroup);
                _screenTop._setDataStr(_user_code, _userCode);
                _screenTop._setDataStr(_user_password, "");
                _screenTopAdmin._setDataStr(_user_code, _userCode);
                //
                _languageSelect(MyLib._myGlobal._language);
            }
        }

        void _listViewServerSetup_Click(object sender, EventArgs e)
        {
            saveGridToWebserviceList();
            ListView.SelectedListViewItemCollection __breakfast = this._listViewServerMenu.SelectedItems;
            string __menuName = "";
            string __screenText = "";
            foreach (ListViewItem item in __breakfast)
            {
                __menuName = item.Tag.ToString();
                __screenText = item.Text;
                break;
            }
            if (__menuName.CompareTo(this._system_provider_config) == 0)
            {
                MyLib._myUtil._startDialog(this, __screenText, new _providerConfig(0, "Config Server"));
            }
            else
                if (__menuName.CompareTo(_admin_menu_server_setup) == 0)
            {
                // ไม่ login ก็สามารถทำรายการได้ เนื่องจากอาจจะมีการเปลี่ยน password
                _providerConfig __provider = new _providerConfig(1, "Select Provider");
                __provider.ShowDialog();
                if (__provider._exitMode == 1)
                {
                    return;
                }
                MyLib._myUtil._startDialog(this, __screenText, new _serverSetup(__provider));
            }
            else
            {
                if (__menuName.CompareTo(_server_menu_change_password) == 0)
                {
                    MyLib._myUtil._startDialog(this, __screenText, new _serverChangePassword());
                }
                else
                    if (__menuName.CompareTo(_register) == 0)
                {
                    MyLib._myUtil._startDialog(this, __screenText, new _register());
                }
            }
        }

        #region Assembly Attibute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
        }

        private void _selectDatabaseChangeResource(MyLib._languageEnum number)
        {
            this.Text = MyLib._myGlobal._resource("warning58", number);
            // แก้ resource หน้าจอ Login
            _screenTop._changeResource(_provider_code, MyLib._myGlobal._resource("warning59", number));
            _screenTop._changeResource(_database_group, MyLib._myGlobal._resource("warning60", number));
            _screenTop._changeResource(_user_code, MyLib._myGlobal._resource("warning61", number));
            _screenTop._changeResource(_user_password, MyLib._myGlobal._resource("warning62", number));
            _screenTop._refresh();
            // screen admin
            _screenTopAdmin._changeResource(_provider_code, MyLib._myGlobal._resource("warning59", number));
            _screenTopAdmin._changeResource(_database_group, MyLib._myGlobal._resource("warning60", number));
            _screenTopAdmin._changeResource(_user_code, MyLib._myGlobal._resource("warning61", number));
            _screenTopAdmin._changeResource(_user_password, MyLib._myGlobal._resource("warning62", number));
            _screenTopAdmin._refresh();
            // Menu Server
            foreach (ListViewItem __item in this._listViewServerMenu.Items)
            {
                if (__item.Tag != null)
                {
                    // viroon Text = ข้อความ ToolTipText=help
                    string __getStr = __item.Tag.ToString().ToLower();
                    if (__getStr.CompareTo(_server_menu_change_password) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning63", number);
                        __item.ToolTipText = __item.Text;
                    }
                }
            }
            // Menu list ของ admin
            foreach (ListViewItem __item in this._listViewAdminMenu.Items)
            {
                if (__item.Tag != null)
                {
                    // viroon Text = ข้อความ ToolTipText=help
                    string __getStr = __item.Tag.ToString().ToLower();
                    if (__getStr.CompareTo(_admin_menu_server_setup) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning64", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning65", number);
                    }
                    else
                        if (__getStr.CompareTo(_admin_menu_grou_and_user) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning66", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning67", number);
                    }
                    else
                            if (__getStr.CompareTo(_admin_menu_group_manage) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning68", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning69", number);
                    }
                    else
                                if (__getStr.CompareTo(_admin_menu_database_group) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning70", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning71", number);
                    }
                    else
                                    if (__getStr.CompareTo(_admin_menu_create_new_database) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning72", number);
                        __item.ToolTipText = __item.Text;
                    }
                    else
                                        if (__getStr.CompareTo(_admin_menu_link_database) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning73", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning74", number);
                    }
                    else
                                            if (__getStr.CompareTo(_admin_menu_verify_database) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning75", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning76", number);
                    }
                    else
                                                if (__getStr.CompareTo(_admin_menu_shrink_database) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning77", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning78", number);
                    }
                    else
                                                    if (__getStr.CompareTo(_admin_menu_resource_edit) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning79", number);
                        __item.ToolTipText = MyLib._myGlobal._resource("warning80", number);
                    }
                    else
                                                        if (__getStr.CompareTo(_admin_menu_change_password) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning81", number);
                        __item.ToolTipText = __item.Text;
                    }
                    else
                                                            if (__getStr.CompareTo(_admin_menu_database_access) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning82", number);
                        __item.ToolTipText = __item.Text;
                    }
                    else
                                                                if (__getStr.CompareTo(_admin_menu_group_permissions) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning83", number);
                        __item.ToolTipText = __item.Text;
                    }
                    else
                                                                    if (__getStr.CompareTo(_admin_menu_user_permissions) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning84", number);
                        __item.ToolTipText = __item.Text;
                    }
                    else
                                                                        if (__getStr.CompareTo(_admin_menu_trasnfer_data) == 0)
                    {
                        __item.Text = MyLib._myGlobal._resource("warning129", number);
                        __item.ToolTipText = __item.Text;
                    }

                }
            }
        }

        private ArrayList _packWebserviceName()
        {
            ArrayList __result = new ArrayList();
            for (int __loop = 0; __loop < _gridWebservice._rowData.Count; __loop++)
            {
                _myWebserviceType __data = new _myWebserviceType();
                if (_gridWebservice._cellGet(__loop, 0).ToString().Length > 0)
                {
                    __data._webServiceName = _gridWebservice._cellGet(__loop, 0).ToString().Replace(" ", "").Replace(" ", "").Trim();
                    if (__data._webServiceName.Length > 0)
                    {
                        __data._webServiceName = MyLib._myGlobal._compileWebserviceName(__data._webServiceName);
                        __data._webServiceConnected = true;
                        __result.Add(__data);
                    }
                }
            }
            return __result;
        }

        private void _checkInput(MyLib._myScreen myScreen, int mode)
        {
            string __data_provider_code = myScreen._getDataStr(_provider_code).ToUpper();
            string __data_database_group = myScreen._getDataStr(_database_group);
            string __data_user_code = myScreen._getDataStr(_user_code);
            string __data_user_password = myScreen._getDataStr(_user_password);
            //
            // ตรวจสอบว่าป้อนครบหรือเปล่า
            MyLib._myGlobal._providerCode = __data_provider_code;
            MyLib._myGlobal._userLoginSuccess = false;
            this.Cursor = Cursors.WaitCursor;
            StringBuilder __result = new StringBuilder();
            bool __found = false;
            for (int __loop = 0; __loop < _gridWebservice._rowData.Count; __loop++)
            {
                if (_gridWebservice._cellGet(__loop, 0).ToString().Length > 0)
                {
                    __found = true;
                }
            }
            if (__found == false)
            {
                // ยังไม่ป้อน web service name
                MessageBox.Show(MyLib._myGlobal._resource("warning56"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //MooAe หากไม่มีการบันทึก Webservice  ให้กลับไปหน้า แรกเพื่อใส่ Webservice 
                this._loginTab.SelectTab(0);
            }
            else
            {
                string __getStr = MyLib._myGlobal._resource("warning61");
                string __getData = myScreen._getDataStr(_user_code);
                if (__getData.Length == 0)
                {
                    //  
                    __result.Append(String.Concat(string.Format(MyLib._myGlobal._resource("warning57"), __getStr), "\n"));
                }
                if (__result.Length > 0)
                {
                    MessageBox.Show(__result.ToString(), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    // ป้อนครบ ตรวจสอบต่อไป
                    try
                    {
                        _myFrameWork __myFrameWork = new _myFrameWork();
                        ArrayList __webServerList = _packWebserviceName();
                        MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(__webServerList, "");
                        __myFrameWork._initGlobal();
                        /*HttpWebRequest getServer = (HttpWebRequest)WebRequest.Create("http://" + webServiceServer + "/" + MyLib._myGlobal._webServiceName + "/DotNetFrameWork");
                        HttpWebResponse getResponse = (HttpWebResponse)getServer.GetResponse();
                        getResponse.Close();*/
                        // หลังจากตรวจสอบว่าติดต่อกับ Web service ได้แล้ว ก็ทำการตรวจสอบรหัสผ่านต่อไป
                        //MyLib._database getDatabase = new MyLib._database();
                        bool __getGroupCode = true;
                        bool __getUserAndPassword = false;
                        if (mode == 0)
                        {
                            // จากหน้าจอ Login , admin ไม่ต้องตรวจ
                            __getGroupCode = __myFrameWork._getGroup(__data_database_group);
                            if (__getGroupCode == false)
                            {
                                // --  \n คือบรรทัดใหม่นะครับ
                                MessageBox.Show(MyLib._myGlobal._resource("warning89"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                        }
                        // ตรวจสอบรหัสผ่านผู้ใช้งานต่อเลย
                        if (__getGroupCode == true)
                        {
                            __getUserAndPassword = __myFrameWork._checkUserAndPassword(__data_user_code, __data_user_password);
                            if (__getUserAndPassword == false)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("warning90"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                // รหัสผ่านแล้ว ดึง level user
                                DataSet __getUser = __myFrameWork._getUser(__data_user_code);
                                if (__getUser.Tables.Count > 0)
                                {
                                    DataRow[] getRows = __getUser.Tables[0].Select();
                                    MyLib._myGlobal._userName = getRows[0].ItemArray[0].ToString();
                                    MyLib._myGlobal._userLevel = Convert.ToInt32(getRows[0].ItemArray[1].ToString());
                                    // viroon
                                    if (mode == 1 && (MyLib._myGlobal._userLevel == 0 || MyLib._myGlobal._userLevel == 1))
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("warning92"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                        //MooAe ให้กลับมาหน้า Login 
                                        this._loginTab.SelectTab(1);
                                    }
                                    else
                                    {
                                        if (mode == 1)
                                        {
                                            // Admin
                                            __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid);
                                            MyLib._myGlobal._guid = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, __data_user_code, __data_user_password, MyLib._myGlobal._computerName, MyLib._myGlobal._mainDatabase);
                                        }
                                        // Device
                                        Boolean __pass = true;
                                        try
                                        {
                                            DataTable __getDevice = __myFrameWork._query(MyLib._myGlobal._mainDatabase, "select " + MyLib._d.sml_user_list._device_id + " from " + MyLib._d.sml_user_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_list._user_code) + "=\'" + __data_user_code.ToUpper() + "\' order by "+ MyLib._d.sml_user_list._device_id).Tables[0];
                                            if (__getDevice.Rows.Count > 0)
                                            {
                                                string __device = __getDevice.Rows[0][0].ToString().Trim();
                                                if (__device.Trim().Length > 0)
                                                {
                                                    __pass = false;
                                                    MyLib._getInfoStatus _getinfo = new _getInfoStatus();
                                                    string[] _dataDive = Environment.GetLogicalDrives();
                                                    for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                                                    {
                                                        string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim();
                                                        if (__getDeviceCode.Length > 0 && __device.IndexOf(__getDeviceCode) != -1)
                                                        {
                                                            __pass = true;
                                                            break;
                                                        }
                                                    }
                                                    if (__pass == false)
                                                    {
                                                        MessageBox.Show("Device not found.", "Error");
                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                        //
                                        if (__pass)
                                        {
                                            string __message_1 = String.Format(MyLib._myGlobal._resource("warning93"), MyLib._myGlobal._userName, MyLib._myGlobal._getUserLevelName(MyLib._myGlobal._userLevel));
                                            string __message_2 = MyLib._myGlobal._resource("warning94");
                                            MessageBox.Show(__message_1, __message_2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            MyLib._myGlobal._userLoginSuccess = true;
                                            _saveProfile(this._profileNameTextBox.Text, __data_provider_code, __data_database_group, __data_user_code, false);
                                            __myFrameWork._databaseSelectType = __myFrameWork._setDataBaseCode();
                                            __myFrameWork._webserviceServerReConnect(false);
                                            MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //  
                        string __message_1 = MyLib._myGlobal._resource("warning95") + ex.Message.ToString();
                        string __message_2 = MyLib._myGlobal._resource("warning96") + ex.Message.ToString();
                        MessageBox.Show(__message_1, __message_2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        public void _saveProfile(string profileName, string providerCode, string groupName, string userCode, Boolean firstLogin)
        {
            if (MyLib._myGlobal._emergencyMode == false)
            {
                {
                    StringBuilder __xmlStr = new StringBuilder(String.Concat(MyLib._myGlobal._xmlHeader, "<node>"));
                    __xmlStr.Append("<server>");
                    if (firstLogin)
                    {
                        if (MyLib._myGlobal._OEMVersion == "imex" || MyLib._myGlobal._OEMVersion == "ims")
                        {
                            __xmlStr.Append(String.Concat("<name>", "203.150.230.42:8080", "</name>"));
                        }
                        else
                            __xmlStr.Append(String.Concat("<name>", "www.smlsoft.com:8080", "</name>"));
                    }
                    else
                    {
                        for (int __loop = 0; __loop < _gridWebservice._rowData.Count; __loop++)
                        {
                            if (_gridWebservice._cellGet(__loop, 0).ToString().Length > 0)
                            {
                                __xmlStr.Append(String.Concat("<name>", _gridWebservice._cellGet(__loop, 0).ToString(), "</name>"));
                            }
                        }
                    }
                    __xmlStr.Append("</server>");
                    __xmlStr.Append(String.Concat("<provider>", providerCode, "</provider>"));
                    __xmlStr.Append(String.Concat("<group>", groupName, "</group>"));
                    __xmlStr.Append(String.Concat("<user>", userCode, "</user>"));
                    __xmlStr.Append(String.Concat("<proxy_url>", MyLib._myGlobal._proxyUrl, "</proxy_url>"));
                    __xmlStr.Append(String.Concat("<proxy_use>", MyLib._myGlobal._proxyUsed, "</proxy_use>"));
                    __xmlStr.Append(String.Concat("<proxy_user>", MyLib._myGlobal._proxyUser, "</proxy_user>"));
                    __xmlStr.Append(String.Concat("<proxy_password>", MyLib._myGlobal._proxyPassword, "</proxy_password>"));
                    __xmlStr.Append(String.Concat("<language>", MyLib._myGlobal._languageNumber, "</language>"));
                    __xmlStr.Append(String.Concat("</node>"));

                    string __newPathFile = MyLib._myGlobal._smlConfigFile + MyLib._myGlobal._profileFileName;

                    //string __xPathName = Path.GetTempPath();
                    //string __xFileName = Path.GetTempPath() + "\\" + MyLib._myGlobal._profileFileName;

                    StreamWriter __sr = File.CreateText(__newPathFile);
                    __sr.WriteLine(__xmlStr.ToString());
                    __sr.Close();
                }
                {
                    // History
                    if (profileName.Trim().Length > 0)
                    {
                        string __historyPathFile = MyLib._myGlobal._smlConfigFile + "H" + MyLib._myGlobal._profileFileName;
                        int __historyAddr = -1;
                        for (int __loop = 0; __loop < MyLib._myGlobal._loginHistory.__details.Count; __loop++)
                        {
                            if (MyLib._myGlobal._loginHistory.__details[__loop].__name.Equals(profileName))
                            {
                                __historyAddr = __loop;
                                break;
                            }
                        }
                        if (__historyAddr != -1)
                        {
                            MyLib._myGlobal._loginHistory.__details.RemoveAt(__historyAddr);
                        }
                        //
                        _loginHistoryDetailClass __history = new _loginHistoryDetailClass();
                        for (int __loop = 0; __loop < _gridWebservice._rowData.Count; __loop++)
                        {
                            if (_gridWebservice._cellGet(__loop, 0).ToString().Length > 0)
                            {
                                __history.__url.Add(_gridWebservice._cellGet(__loop, 0).ToString());
                            }
                        }
                        __history.__name = profileName;
                        __history.__group = groupName;
                        __history.__provider = providerCode;
                        __history.__user = userCode;
                        __history.__proxyUrl = MyLib._myGlobal._proxyUrl;
                        __history.__proxyUsed = MyLib._myGlobal._proxyUsed;
                        __history.__proxyUser = MyLib._myGlobal._proxyUser;
                        __history.__proxyPassword = MyLib._myGlobal._proxyPassword;
                        __history.__languageNumber = MyLib._myGlobal._languageNumber;
                        MyLib._myGlobal._loginHistory.__details.Insert(0, __history);
                        //
                        XmlSerializer __writer = new XmlSerializer(typeof(MyLib._databaseManage._loginHistoryClass));
                        FileStream __file = File.Create(__historyPathFile);
                        __writer.Serialize(__file, MyLib._myGlobal._loginHistory);
                        __file.Close();
                    }
                }
            }
        }

        public void _loadLastProfile()
        {
            try
            {
                // load history
                string __historyPathFile = MyLib._myGlobal._smlConfigFile + "H" + MyLib._myGlobal._profileFileName;
                XmlSerializer __x = new XmlSerializer(typeof(MyLib._databaseManage._loginHistoryClass));
                FileStream __fs = new FileStream(__historyPathFile, FileMode.Open);
                MyLib._myGlobal._loginHistory = (MyLib._databaseManage._loginHistoryClass)__x.Deserialize(__fs);
                __fs.Close();
                for (int __loop = 0; __loop < MyLib._myGlobal._loginHistory.__details.Count; __loop++)
                {
                    this._history.DropDownItems.Add(MyLib._myGlobal._loginHistory.__details[__loop].__name);
                }
            }
            catch
            {

            }
            try
            {
                // toe change new path file
                string __newPathFile = MyLib._myGlobal._smlConfigFile + MyLib._myGlobal._profileFileName;


                string __xPathName = Path.GetTempPath();
                string __xFileName = Path.GetTempPath() + "\\" + MyLib._myGlobal._profileFileName;

                XmlDocument xDoc = new XmlDocument();

                try
                {
                    // โหลด path ใหม่
                    xDoc.Load(__newPathFile);

                }
                catch
                {

                    try
                    {
                        xDoc.Load(__xFileName);
                    }
                    catch
                    {
                        if (MyLib._myGlobal._OEMVersion == "imex")
                        {
                            _saveProfile("DEMO", "DEMO", "DEMO", "DEMO", true);
                        }
                        else
                            _saveProfile("demo", "demo", "SML", "demo", true);
                        //xDoc.Load(__xFileName);
                        xDoc.Load(__newPathFile);
                    }
                }

                xDoc.DocumentElement.Normalize();
                XmlElement __xRoot = xDoc.DocumentElement;
                XmlNodeList __xReader = __xRoot.GetElementsByTagName("name");
                _gridWebservice._clear();
                for (int __table = 0; __table < __xReader.Count; __table++)
                {
                    XmlNode __xFirstNode = __xReader.Item(__table);
                    if (__xFirstNode.NodeType == XmlNodeType.Element)
                    {
                        XmlElement __xTable = (XmlElement)__xFirstNode;
                        _gridWebservice._addRow();
                        _gridWebservice._cellUpdate(__table, 0, __xTable.InnerText, false);
                    }
                }

                for (int __loop = 1; __loop < 9; __loop++)
                {
                    string __getName = "";
                    switch (__loop)
                    {
                        case 1: __getName = "provider"; break;
                        case 2: __getName = "group"; break;
                        case 3: __getName = "user"; break;
                        case 4: __getName = "proxy_url"; break;
                        case 5: __getName = "proxy_use"; break;
                        case 6: __getName = "proxy_user"; break;
                        case 7: __getName = "proxy_password"; break;
                        case 8: __getName = "language"; break;
                    }
                    __xReader = __xRoot.GetElementsByTagName(__getName);
                    for (int __table = 0; __table < __xReader.Count; __table++)
                    {
                        XmlNode __xFirstNode = __xReader.Item(__table);
                        if (__xFirstNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement __xTable = (XmlElement)__xFirstNode;
                            switch (__loop)
                            {
                                case 1: _providerCode = __xTable.InnerText; break;
                                case 2: _dataGroup = __xTable.InnerText; break;
                                case 3: _userCode = __xTable.InnerText; break;
                                case 4: MyLib._myGlobal._proxyUrl = __xTable.InnerText; break;
                                case 5: MyLib._myGlobal._proxyUsed = (int)MyLib._myGlobal._decimalPhase(__xTable.InnerText); break;
                                case 6: MyLib._myGlobal._proxyUser = __xTable.InnerText; break;
                                case 7: MyLib._myGlobal._proxyPassword = __xTable.InnerText; break;
                                case 8:
                                    {
                                        if (__xTable.InnerText.Equals("0"))
                                        {
                                            MyLib._myGlobal._language = _languageEnum.Thai;
                                        }
                                        if (__xTable.InnerText.Equals("1"))
                                        {
                                            MyLib._myGlobal._language = _languageEnum.English;
                                        }
                                        if (__xTable.InnerText.Equals("2"))
                                        {
                                            MyLib._myGlobal._language = _languageEnum.Malayu;
                                        }
                                        if (__xTable.InnerText.Equals("3"))
                                        {
                                            MyLib._myGlobal._language = _languageEnum.Chainese;
                                        }
                                        if (__xTable.InnerText.Equals("4"))
                                        {
                                            MyLib._myGlobal._language = _languageEnum.India;
                                        }
                                        if (__xTable.InnerText.Equals("5"))
                                        {
                                            MyLib._myGlobal._language = _languageEnum.Lao;
                                        }
                                    }
                                    break;
                            }
                        }
                    } // for
                } // for
            }
            catch
            {
            }
        }

        private void _selectDatabase_Load(object sender, EventArgs e)
        {

        }

        void __login(string databaseName)
        {
            string __errorMessage = MyLib._myGlobal._resource("warning97");
            try
            {
                MyLib._myGlobal._databaseName = databaseName;
                _myFrameWork __myFrameWork = new _myFrameWork();
                __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid);
                MyLib._myGlobal._guid = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, _screenTop._getDataStr(_user_code), _screenTop._getDataStr(_user_password), MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);
                Close();
                if (MyLib._myGlobal._guid.Length == 0)
                {
                    MessageBox.Show(__errorMessage, "GUID Login fail.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(__errorMessage, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show(__errorMessage, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _gridDatabaseList__mouseClick(object sender, GridCellEventArgs e)
        {
            if (e._text.Length > 0)
            {
                __login(((MyLib._myGrid)sender)._cellGet(e._row, _column_name_2).ToString());
            }
        }

        void _listViewAdminMenu_Click(object sender, EventArgs e)
        {
            saveGridToWebserviceList();
            ListView.SelectedListViewItemCollection __breakfast = this._listViewAdminMenu.SelectedItems;
            string __menuName = "";
            string __screenText = "";
            foreach (ListViewItem item in __breakfast)
            {
                __menuName = item.Tag.ToString();
                __screenText = item.Text;
                break;
            }
            if (__menuName.CompareTo(_admin_menu_server_setup) == 0)
            {
                // ไม่ login ก็สามารถทำรายการได้ เนื่องจากอาจจะมีการเปลี่ยน password
                _providerConfig __provider = new _providerConfig(1, "Select Provider");
                __provider.ShowDialog();
                if (__provider._exitMode == 1)
                {
                    return;
                }
                MyLib._myUtil._startDialog(this, __screenText, new _serverSetup(__provider));
            }
            else
            {
                if (MyLib._myGlobal._userLoginSuccess == false)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning98"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                // MooAE ให้เข้าใช้งานเฉพาะ กลุ่ม admin เท่านั้น
                if (MyLib._myGlobal._userLevel == 2 || MyLib._myGlobal._userLevel == 3)
                {
                    if (__menuName.CompareTo(_admin_menu_grou_and_user) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _userAndGroup());
                    }
                    else
                        if (__menuName.CompareTo(_admin_menu_group_manage) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _groupManage());
                    }
                    else
                            if (__menuName.CompareTo(_admin_menu_database_group) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _groupDatabase());
                    }
                    else
                                if (__menuName.CompareTo(_admin_menu_create_new_database) == 0)
                    {
                        // check is clound

                        MyLib._myUtil._startDialog(this, __screenText, new _createDatabase(0));
                    }
                    else
                                    if (__menuName.CompareTo(_admin_menu_link_database) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._linkDatabase._linkDatabase());
                    }
                    else
                                        if (__menuName.CompareTo(_admin_menu_verify_database) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._verifyDatabase());
                    }
                    else
                                            if (__menuName.CompareTo(_admin_menu_shrink_database) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._shrinkDatabase());
                    }
                    else
                                                if (__menuName.CompareTo(_admin_menu_resource_edit) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._resourceEdit());
                    }
                    else
                                                    if (__menuName.CompareTo(_admin_menu_change_password) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._smlChangePassword(this._screenTopAdmin._getDataStr(_user_code)));
                    }
                    else
                                                        if (__menuName.CompareTo(_admin_menu_database_access) == 0)
                    {
                        MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._databaseAccess());
                    }
                    else
                                                            if (__menuName.CompareTo(_admin_menu_group_permissions) == 0)
                    {
                        try
                        {
                            MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._groupPermissions(), true);
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
                        }

                    }
                    else
                                                                if (__menuName.CompareTo(_admin_menu_user_permissions) == 0)
                    {
                        try
                        {
                            MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._userPermissions(), true);
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
                        }
                    }
                    else
                                                                    if (__menuName.CompareTo(_admin_menu_trasnfer_data) == 0)
                    {
                        try
                        {
                            _passwordForm __password = new _passwordForm();
                            __password.StartPosition = FormStartPosition.CenterScreen;
                            __password._okButton.Click += (s1, e1) =>
                            {
                                if (__password._passwordTextBox.Text.Equals("smladmin"))
                                {
                                    __password.Dispose();
                                    MyLib._myUtil._startDialog(this, __screenText, new _databaseManage._transferDatabase());
                                }
                            };
                            __password.ShowDialog();
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning99"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    //this.loginTab.SelectTab(0);
                }
            }
        }

        private void languageSwitch_Click(object sender, EventArgs e)
        {
        }

        private void buttonLoginAdmin_Click(object sender, EventArgs e)
        {
        }

        private void loginBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void buttonLoginAdmin_Click_1(object sender, EventArgs e)
        {
            if (_myGlobal._isDemo)
            {
                _myGlobal._demoVersion();
            }
            else
            {
                saveGridToWebserviceList();
                if (MyLib._myGlobal._webServiceServerList.Count == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning100"));
                    //MooAe หากไม่มีการบันทึก Webservice  ให้กลับไปหน้า แรกเพื่อใส่ Webservice  
                    this._loginTab.SelectTab(0);
                }
                else
                {
                    _checkInput(_screenTopAdmin, 1);
                }
            }
        }

        private void saveGridToWebserviceList()
        {
            MyLib._myGlobal._webServiceServerList.Clear();

            //if (MyLib._myGlobal._emergencyMode == false)
            //{
            for (int __loop = 0; __loop < _gridWebservice._rowData.Count; __loop++)
            {
                string __getStr = MyLib._myGlobal._compileWebserviceName(_gridWebservice._cellGet(__loop, 0).ToString());
                if (__getStr.Length > 0)
                {
                    _myWebserviceType __data = new _myWebserviceType();
                    __data._webServiceName = __getStr;
                    __data._webServiceConnected = false;
                    MyLib._myGlobal._webServiceServerList.Add(__data);
                }
            }
            _myFrameWork __myFrameWork = new _myFrameWork();
            __myFrameWork._webserviceServerReConnect(true);
            //}
            //else
            //{
            //    _myWebserviceType __data = new _myWebserviceType();
            //    __data._webServiceName = MyLib._myGlobal._emergencyURL + ":" + MyLib._myGlobal._emergencyPort;
            //    __data._webServiceConnected = false;
            //    MyLib._myGlobal._webServiceServerList.Add(__data);

            //}
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            bool __isLoginFirstDatabaseAuto = false;
            if (_myGlobal._isDemo)
            {
                _myGlobal._demoVersion();
            }
            else
            {
                saveGridToWebserviceList();
                string __guidOld = MyLib._myGlobal._guid;
                MyLib._myGlobal._guid = "SMLX";
                MyLib._myGlobal._providerCode = _screenTop._getDataStr(this._provider_code).ToUpper();
                _checkInput(_screenTop, 0);
                if (MyLib._myGlobal._userLoginSuccess)
                {
                    MyLib._myGlobal._dataGroup = _screenTop._getDataStr(_database_group);
                    MyLib._myGlobal._userCode = _screenTop._getDataStr(_user_code);
                    MyLib._myGlobal._providerCode = _screenTop._getDataStr(_provider_code).ToUpper();
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    string __query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + "  from " + MyLib._d.sml_database_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_group) + "=\'" + MyLib._myGlobal._dataGroup.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=0 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\') or " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=1 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._group_code) + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')) order by " + MyLib._d.sml_database_list._data_name;
                    //query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + " from " + MyLib._d.sml_database_list._table;
                    DataSet __result = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
                    if (__result.Tables.Count > 0)
                    {
                        DataRow[] __getRows = __result.Tables[0].Select();
                        _gridDatabaseList._clear();
                        for (int __row = 0; __row < __getRows.Length; __row++)
                        {
                            _gridDatabaseList._addRow();
                            int rowDataGrid = _gridDatabaseList._rowData.Count - 1;
                            _gridDatabaseList._cellUpdate(rowDataGrid, 0, __getRows[__row].ItemArray[0].ToString(), false);
                            _gridDatabaseList._cellUpdate(rowDataGrid, 1, __getRows[__row].ItemArray[1].ToString(), false);
                        } // for
                        // auto กรณีมีฐานข้อมูลเดียว
                        if (__getRows.Length == 1)
                        {
                            __isLoginFirstDatabaseAuto = true;
                            MyLib._myGlobal._guid = __guidOld;
                            __login(_gridDatabaseList._cellGet(0, _column_name_2).ToString());
                        }
                    }
                }

                if (__isLoginFirstDatabaseAuto == false)
                    MyLib._myGlobal._guid = __guidOld;
            }
        }

        public event ChangeLanguageHandler _changeLanguage;

        private void _languageSelect(_languageEnum language)
        {
            MyLib._myGlobal._language = language;
            _selectDatabaseChangeResource(MyLib._myGlobal._language);
            _screenTop._refresh();
            _screenTopAdmin._refresh();
            if (_changeLanguage != null)
            {
                _changeLanguage(MyLib._myGlobal._language);
            }
            this._selectLanguage.Text = MyLib._myGlobal._language.ToString();
            //
            _scanScreenChangeResource(this);
        }

        private void _scanScreenChangeResource(Control first)
        {
            foreach (Control __getControl in first.Controls)
            {
                __getControl.Invalidate();
                _scanScreenChangeResource(__getControl);
            }
        }

        private void buttonFirstTime_Click(object sender, EventArgs e)
        {
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
        }

        private void listViewAdminMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {

        }

        private void _gridDatabaseList_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void _myShadowLabel2_Click(object sender, EventArgs e)
        {

        }

        private void _listViewXP1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void _selectLanguage_Click(object sender, EventArgs e)
        {

        }

        private void _selectEnglishLanguage_Click(object sender, EventArgs e)
        {
            _languageSelect(_languageEnum.English);
        }

        private void _selectThaiLanguage_Click(object sender, EventArgs e)
        {
            _languageSelect(_languageEnum.Thai);
        }

        private void _selectMalayLanguage_Click(object sender, EventArgs e)
        {
            _languageSelect(_languageEnum.Malayu);
        }

        private void _selectChineseLanguage_Click(object sender, EventArgs e)
        {
            _languageSelect(_languageEnum.English);
        }

        private void _selectTaiwanLanguage_Click(object sender, EventArgs e)
        {
            _languageSelect(_languageEnum.English);
        }

        private void _selectIndonesianLanguage_Click(object sender, EventArgs e)
        {
            _languageSelect(_languageEnum.English);
        }

        private void _selectLaoLanguage_Click(object sender, EventArgs e)
        {
            _languageSelect(_languageEnum.Lao);
        }

        private void _proxyButton_Click(object sender, EventArgs e)
        {
            _proxyForm __proxy = new _proxyForm();
            __proxy.ShowDialog();
        }

        private void _keysToolStripButton_Click(object sender, EventArgs e)
        {
            _computerStatus __status = new _computerStatus();
            __status.ControlBox = true;
            __status.StartPosition = FormStartPosition.CenterScreen;
            __status.ShowDialog();
        }

        private void _helpButton_Click(object sender, EventArgs e)
        {
            _myGlobal._help("admin.html");
        }

        private void _emergencyButton_Click(object sender, EventArgs e)
        {
            _emergencyForm __emergencyForm = new _emergencyForm();
            __emergencyForm.ShowDialog();

            if (MyLib._myGlobal._emergencyMode)
            {
                _gridWebservice._cellUpdate(0, 0, MyLib._myGlobal._emergencyURL + ":" + MyLib._myGlobal._emergencyPort, false);
            }
        }

        private void _settingToolStripButton_Click(object sender, EventArgs e)
        {
            _preferenceForm __settingForm = new _preferenceForm();
            __settingForm.StartPosition = FormStartPosition.CenterScreen;
            __settingForm.ShowDialog();
        }
    }
    public delegate void ChangeLanguageHandler(_languageEnum languageCode);
}
