using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Data;

namespace MyLib._databaseManage
{
    public partial class _serverSetup : MyLib._myForm
    {
        string _database_provider = "database_provider";
        string[] _database_provider_list = { "PostgreSQL", "MySQL", "Microsoft SQL 2000,MSDE", "Microsoft SQL 2005,SQL Express", "Oracle", "Firebird" };
        string _webservice_server_name = "webservice_server_name";
        string _database_server_name = "database_server_name";
        string _user_code = "user_code";
        string _user_password = "user_password";
        string _admin_user = "superadmin";
        // Resource
        //string[] _resource_database_provider = { "ฐานข้อมูลที่ใช้", "Database Provider" };
        // string[] _resource_server_webservice = { "ชื่อ Server Webservice", "Webservice Server Name" };
        // string[] _resource_server_name = { "ชื่อ Server ที่เก็บข้อมูล", "Database Server Name" };
        // string[] _resource_user_code = { "รหัสผู้ใช้ ของ Database Server", "User Code" };
        //string[] _resource_user_password = { "รหัสผ่าน ของ Database Server", "Password" };
        _providerConfig _provider;
        public Boolean _connectSuccess = false;

        public _serverSetup(_providerConfig provider)
        {
            this._provider = provider;
            //
            InitializeComponent();
            this.Font = MyLib._myGlobal._myFont;
            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            //
            // ทำการเชื่อมต่อ
            _buttonConnect.Text = MyLib._myGlobal._resource("warning23");
            //
            _selectDatabase getDatabase = new _selectDatabase();
            getDatabase._loadLastProfile();
            this.databaseScreen._maxColumn = 1;
            this.databaseScreen._getResource = false;
            this.databaseScreen._table_name = "";
            this.databaseScreen._addTextBox(0, 0, 1, 0, _webservice_server_name, 1, 25, 0, true, false);
            this.databaseScreen._addComboBox(1, 0, _database_provider, true, _database_provider_list, true);
            this.databaseScreen._addTextBox(2, 0, 1, 0, _database_server_name, 1, 25, 0, true, false);
            this.databaseScreen._addTextBox(3, 0, 1, 0, _user_code, 1, 25, 0, true, false);
            this.databaseScreen._addTextBox(4, 0, 1, 0, _user_password, 1, 25, 0, true, true);
            //
            this.databaseScreen._changeResource(_webservice_server_name, _myGlobal._resource("server_setup_2"));
            this.databaseScreen._changeResource(_database_provider, _myGlobal._resource("server_setup_1"));
            this.databaseScreen._changeResource(_database_server_name, _myGlobal._resource("server_setup_3"));
            this.databaseScreen._changeResource(_user_code, _myGlobal._resource("server_setup_4"));
            this.databaseScreen._changeResource(_user_password, _myGlobal._resource("server_setup_5"));
            //
            this.databaseScreen._setDataStr(_database_server_name, "localhost");
            //
            this.databaseScreen._refresh();
            //
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

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
        }

        private void _serverSetup_Load(object sender, EventArgs e)
        {
        }

        private void _sendXmlFile(string xmlFileName)
        {
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            __myFrameWork._sendXmlFile(xmlFileName);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            MyLib._myGlobal._webServiceServer = databaseScreen._getDataStr(_webservice_server_name);
            MyLib._myComboBox __getControl = (MyLib._myComboBox)databaseScreen._getControl(_database_provider);
            //
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            switch (__getControl._selectedIndex)
            {
                case 1: __myFrameWork._databaseSelectType = _myGlobal._databaseType.PostgreSql; break;
                case 2: __myFrameWork._databaseSelectType = _myGlobal._databaseType.MySql; break;
                case 3: __myFrameWork._databaseSelectType = _myGlobal._databaseType.MicrosoftSQL2000; break;
                case 4: __myFrameWork._databaseSelectType = _myGlobal._databaseType.MicrosoftSQL2005; break;
                case 5: __myFrameWork._databaseSelectType = _myGlobal._databaseType.Oracle; break;
                case 6: __myFrameWork._databaseSelectType = _myGlobal._databaseType.Firebird; break;
            }
            //
            //myFrameWork._query("SMLERPMAIN", "asasd");
            string __providerTemp = MyLib._myGlobal._providerCode;
            try
            {
                string __databaseServer = databaseScreen._getDataStr(_database_server_name);
                string __serverName = MyLib._myUtil._encrypt(__databaseServer);
                string __userCode = MyLib._myUtil._encrypt(databaseScreen._getDataStr(_user_code).ToString());
                string __userPassword = MyLib._myUtil._encrypt(databaseScreen._getDataStr(_user_password).ToString());
                MyLib._myGlobal._providerCode = _provider._selectCode;
                string __result = __myFrameWork._getConnection(__myFrameWork._databaseSelectType, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._mainDatabaseStruct, __serverName, __userCode, __userPassword);
                Boolean __connectSuccess = (__result.CompareTo("1") == 0);
                if (__connectSuccess)
                {
                    // การเชื่อมต่อสมบูรณ์
                    string __message_header = MyLib._myGlobal._resource("warning45");
                    string __message_1 = String.Format(MyLib._myGlobal._resource("warning46"), __databaseServer);
                    MessageBox.Show(__message_1, __message_header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    __myFrameWork._initGlobal();
                }
                else
                {
                    // การเชื่อมต่อไม่สำเร็จ
                    string __message_header = MyLib._myGlobal._resource("warning47"); ;
                    string __message = MyLib._myGlobal._resource("warning48") + " : " + __result;
                    MessageBox.Show(__message, __message_header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this._connectSuccess = __connectSuccess;
                if (__connectSuccess)
                {
                    // ทำการตรวจสอบฐานข้อมูลว่ามีหรือไม่ในฐานข้อมูล SMLMAIN ถ้าไม่มีก็เพิ่มเข้าไป หรือถ้ามีก็ตรวจสอบว่า ฐานข้อมูลมีการแก้ไขหรือไม่ ถ้ามีก็แก้ไขฐานใหม่
                    this.Refresh();
                    // Copy XML ที่จำเป็นไปเก็บไว้ใน Webservice Server
                    if (MyLib._myGlobal._databaseVerifyXmlFileName.Length > 0)
                    {
                        this._sendXmlFile(MyLib._myGlobal._databaseVerifyXmlFileName);
                        this.progressText.Text = "Create [" + MyLib._myGlobal._databaseVerifyXmlFileName + "]";
                        this.progressText.Refresh();
                        this.Focus();
                    }
                    this._sendXmlFile(MyLib._myGlobal._databaseStructFileName);
                    this.progressText.Text = "Create [" + MyLib._myGlobal._databaseStructFileName + "]";
                    this.progressText.Refresh();
                    this.Focus();
                    this._sendXmlFile(MyLib._myGlobal._dataViewXmlFileName);
                    this.progressText.Text = "Create [" + MyLib._myGlobal._dataViewXmlFileName + "]";
                    this.progressText.Refresh();
                    this.Focus();
                    this._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);
                    this.progressText.Text = "Create [" + MyLib._myGlobal._dataViewTemplateXmlFileName + "]";
                    this.progressText.Refresh();
                    this.Focus();
                    this._sendXmlFile(MyLib._myGlobal._mainDatabaseStruct);
                    this.progressText.Text = "Create [" + MyLib._myGlobal._mainDatabaseStruct + "]";
                    this.progressText.Refresh();
                    this.Focus();
                    //ArrayList getTableList = myFrameWork.SMLGetTableList(0, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabaseStruct);
                    ArrayList __getTableList = __myFrameWork._getAllTable(0, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabaseStruct);
                    int __maxTable = __getTableList.Count;
                    int __errorCount = 0;
                    for (int __loop = 0; __loop < __maxTable; __loop++)
                    {
                        this.progressText.Text = "Verify table [" + _myGlobal._mainDatabase + "." + __getTableList[__loop].ToString() + "]";
                        this.progressText.Refresh();
                        string __verifyResult = __myFrameWork._verifyDatabase(MyLib._myGlobal._databaseConfig, "SML", MyLib._myGlobal._mainDatabase, __getTableList[__loop].ToString(), MyLib._myGlobal._mainDatabaseStruct);
                        if (__verifyResult.Length > 0)
                        {
                            MessageBox.Show("[" + this.progressText.Text + "]\n" + __verifyResult, "Verify database fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            __errorCount++;
                        }
                        this.progressBarShow.Value = (((__loop + 1) * 100) / (__maxTable));
                        this.progressBarShow.Refresh();
                    }
                    // for
                    //
                    {
                        DialogResult __truncateResult = MessageBox.Show("Reset All Resource", "Truncate", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        if (__truncateResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, "truncate table sml_resource");
                            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, "truncate table sml_language");
                        }
                    }
                    //
                    if (MyLib._myGlobal._languageXmlFileName != null)
                    {
                        this._sendXmlFile(MyLib._myGlobal._languageXmlFileName);
                        string __resultLanguage = __myFrameWork._insertLanguageTable(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._languageXmlFileName);
                        if (__resultLanguage.Length > 0)
                        {
                            MessageBox.Show(__resultLanguage, "insert language fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            __errorCount++;
                        }
                    }

                    // ค้นหาผู้ใช้ถ้าไม่มีก็ทำการเพิ่มให้เลย (superadmin)
                    MyLib._myGlobal._guid = "SMLX";
                    string __query = "select * from " + MyLib._d.sml_user_list._table + " where " + _myGlobal._addUpper(_d.sml_user_list._user_code) + "=\'" + _admin_user.ToUpper() + "\'";
                    DataSet _datasetResult = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
                    if (_datasetResult.Tables.Count == 0)
                    {
                        string _insertResult = _insertFirstUser();
                        if (_insertResult.Length != 0)
                        {
                            MessageBox.Show("Fail : [" + _insertResult + "]", "Insert fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        DataRow[] __getRows = _datasetResult.Tables[0].Select();
                        if (__getRows.Length == 0)
                        {
                            string _insertResult = _insertFirstUser();
                            if (_insertResult.Length != 0)
                            {
                                MessageBox.Show("Fail : [" + _insertResult + "]", "Insert fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    //
                    {
                        DialogResult __truncateResult = MessageBox.Show("Reset superadmin password", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        if (__truncateResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, "update sml_user_list set user_password=\'superadmin\' where upper(user_code)=\'SUPERADMIN\'");
                        }
                    }

                    //
                    string __messageSuccess = (__errorCount == 0) ? MyLib._myGlobal._resource("warning49") : string.Format(MyLib._myGlobal._resource("warning50"), __errorCount);
                    MessageBox.Show(__messageSuccess, MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Refresh();

                    if (__errorCount == 0)
                    {
                        MyLib._myGlobal._serverSetupCreateDatabaseSuccess = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Webservice can't connect : [" + ex.Message.ToString() + "]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
            MyLib._myGlobal._providerCode = __providerTemp;
        }

        private string _insertFirstUser()
        {
            string __result = "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __myQuery = MyLib._myGlobal._xmlHeader + "<node>";
            __myQuery += "<query>insert into " + MyLib._d.sml_user_list._table + " (" + MyLib._d.sml_user_list._user_code + "," + MyLib._d.sml_user_list._user_name + "," + MyLib._d.sml_user_list._active_status + ",";
            __myQuery += MyLib._d.sml_user_list._user_password + "," + MyLib._d.sml_user_list._user_level + ") values (\'" + _admin_user + "\',\'" + _admin_user + "\',1,\'" + MyLib._myUtil._encrypt(_admin_user) + "\',2)</query>";
            __myQuery += "</node>";
            string __resultQuery = __myFrameWork._queryList(MyLib._myGlobal._mainDatabase, __myQuery);
            if (__resultQuery.Length == 0)
            {
                MessageBox.Show(string.Format(MyLib._myGlobal._resource("warning51"), _admin_user, _admin_user), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return (__result);
        }
    }
}
