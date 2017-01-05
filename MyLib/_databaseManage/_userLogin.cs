using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;

namespace MyLib._databaseManage
{
    public partial class _userLogin : Form
    {
        string _webservice_url = "webservice_url";
        string _provider_code = "provider_code";
        string _database_group = "database_group";
        string _user_code = "user_code";
        string _user_password = "user_password";
        // Resource

        string[] _resource_column_name_1 = { "รหัสบริษัท", "Company Code" };
        string[] _resource_column_name_2 = { "ชื่อบริษัท", "Company Name" };
        // Admin Menu
        public _userLogin()
        {
            InitializeComponent();
            this._build();
            _loadLastProfile();
            _languageSelect(_languageEnum.Thai);
        }

        protected virtual void _build()
        {
            this._screenTop._addTextBox(0, 0, 1, 0, "webservice_url", 1, 25, 0, true, false);
            this._screenTop._addTextBox(1, 0, 1, 0, "user_password", 1, 25, 0, true, true);
        }

        public event ChangeLanguageHandler _changeLanguage;

        private void _languageSelect(_languageEnum language)
        {
            MyLib._myGlobal._language = language;
            _selectDatabaseChangeResource(MyLib._myGlobal._language);
            _screenTop._refresh();
            //_screenTopAdmin._refresh();
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

        private void _selectDatabaseChangeResource(MyLib._languageEnum number)
        {
            this.Text = MyLib._myGlobal._resource("warning58", number);
            // แก้ resource หน้าจอ Login
            _screenTop._changeResource(_webservice_url, MyLib._myGlobal._resource("webservice_url", number));
            _screenTop._changeResource(_provider_code, MyLib._myGlobal._resource("warning59", number));
            _screenTop._changeResource(_database_group, MyLib._myGlobal._resource("warning60", number));
            _screenTop._changeResource(_user_code, MyLib._myGlobal._resource("warning61", number));
            _screenTop._changeResource(_user_password, MyLib._myGlobal._resource("warning62", number));
            _screenTop._refresh();
            _screenTop._beforeChangeResource();
            _screenTop.Invalidate();
        }

        private void _loginButton_Click(object sender, EventArgs e)
        {
            _loginProcess();
        }

        protected virtual void _loginProcess()
        {
            //MessageBox.Show("login");
            MyLib._myGlobal._webServiceServerList.Clear();

            // set web service
            string __getStr = this._screenTop._getDataStr(_webservice_url);
            if (__getStr.Length > 0)
            {
                _myWebserviceType __data = new _myWebserviceType();
                __data._webServiceName = __getStr;
                __data._webServiceConnected = false;
                MyLib._myGlobal._webServiceServerList.Add(__data);
            }

            _myFrameWork __myFrameWork = new _myFrameWork();
            __myFrameWork._webserviceServerReConnect(true);

            // start login

            string __guidOld = MyLib._myGlobal._guid;
            MyLib._myGlobal._guid = "SMLX";
            MyLib._myGlobal._providerCode = _screenTop._getDataStr(this._provider_code).ToUpper();
            _checkInput(_screenTop, 0);
            if (MyLib._myGlobal._userLoginSuccess)
            {
                //MyLib._myGlobal._userCode = _screenTop._getDataStr(_user_code);
                //MyLib._myGlobal._dataGroup = _screenTop._getDataStr(_database_group);
                //MyLib._myGlobal._providerCode = _screenTop._getDataStr(_provider_code).ToUpper();
                //_myFrameWork __myFrameWork2 = new _myFrameWork();
                //string __query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + "  from " + MyLib._d.sml_database_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_group) + "=\'" + MyLib._myGlobal._dataGroup.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=0 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\') or " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=1 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._group_code) + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')) order by " + MyLib._d.sml_database_list._data_name;
                //query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + " from " + MyLib._d.sml_database_list._table;
                //DataSet __result = __myFrameWork2._query(MyLib._myGlobal._mainDatabase, __query);
                //if (__result.Tables.Count > 0)
                //{
                //    DataRow[] __getRows = __result.Tables[0].Select();
                //    _gridDatabaseList._clear();
                //    for (int __row = 0; __row < __getRows.Length; __row++)
                //    {
                //        _gridDatabaseList._addRow();
                //        int rowDataGrid = _gridDatabaseList._rowData.Count - 1;
                //        _gridDatabaseList._cellUpdate(rowDataGrid, 0, __getRows[__row].ItemArray[0].ToString(), false);
                //        _gridDatabaseList._cellUpdate(rowDataGrid, 1, __getRows[__row].ItemArray[1].ToString(), false);
                //    } // for
                //}

                //MyLib._myGlobal._databaseName = e._text;//somruk
                MyLib._myGlobal._databaseName = MyLib._myGlobal._mainDatabasePOSStarter; // __databaseName;
                _myFrameWork __myFrameWork2 = new _myFrameWork();
                __myFrameWork._logoutProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._guid);
                MyLib._myGlobal._guid = __myFrameWork2._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, _screenTop._getDataStr(_user_code), _screenTop._getDataStr(_user_password), MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);
                Close();
                if (MyLib._myGlobal._guid.Length == 0)
                {
                    string __errorMessage = MyLib._myGlobal._resource("warning97");
                    MessageBox.Show(__errorMessage, "GUID Login fail.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            MyLib._myGlobal._guid = __guidOld;
        }

        private ArrayList _packWebserviceName()
        {
            ArrayList __result = new ArrayList();
            //for (int __loop = 0; __loop < _gridWebservice._rowData.Count; __loop++)
            //{
            _myWebserviceType __data = new _myWebserviceType();
            //if (_gridWebservice._cellGet(__loop, 0).ToString().Length > 0)
            //{
            __data._webServiceName = this._screenTop._getDataStr(_webservice_url).ToString(); // _gridWebservice._cellGet(__loop, 0).ToString();
            __data._webServiceConnected = true;
            __result.Add(__data);
            //}
            //}
            return __result;
        }

        bool _createPOSStarterDatabase()
        {
            if (MessageBox.Show("ไม่พบฐานข้อมูลที่ใช้งาน คุณต้องการสร้างฐานข้อมูลใหม่หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                // สร้าง ใหม่
                _createDatabase _createDatabase = new _createDatabase(1);
                _createDatabase.ShowDialog();

                if (_createDatabase._processResult)
                {
                    return true;
                }

            }

            return false;
        }

        private void _checkInput(MyLib._myScreen myScreen, int mode)
        {
            string __data_provider_code = MyLib._myGlobal._mainProviderPOSStarter;
            string __data_database_group = "SML"; // myScreen._getDataStr(_database_group);
            string __data_user_code = "superadmin"; // myScreen._getDataStr(_user_code);
            string __data_user_password = myScreen._getDataStr(_user_password);
            //
            // ตรวจสอบว่าป้อนครบหรือเปล่า
            MyLib._myGlobal._providerCode = __data_provider_code;
            MyLib._myGlobal._userLoginSuccess = false;
            MyLib._myGlobal._dataGroup = __data_database_group;
            this.Cursor = Cursors.WaitCursor;
            StringBuilder __result = new StringBuilder();


            if (this._screenTop._getDataStr(_webservice_url).ToString().Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("warning56"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                    // โต๋ข้ามการถามกลุ่ม
                    bool __getUserAndPassword = false;
                    //if (mode == 0)
                    //{
                    //    // จากหน้าจอ Login , admin ไม่ต้องตรวจ
                    //    __getGroupCode = __myFrameWork._getGroup(__data_database_group);
                    //    if (__getGroupCode == false)
                    //    {
                    //        _createPOSStarterDatabase();
                    //        // ยังไม่มีฐานข้อมูล
                    //        // --  \n คือบรรทัดใหม่นะครับ
                    //        //MessageBox.Show(MyLib._myGlobal._resource("warning89"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    //    }

                    //}
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
                                MyLib._myGlobal._userCode = __data_user_code;

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
                                    // เช็ค database name
                                    //_myFrameWork __myFrameWork2 = new _myFrameWork();
                                    string __query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + "  from " + MyLib._d.sml_database_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_group) + "=\'" + MyLib._myGlobal._dataGroup.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=0 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\') or " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=1 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._group_code) + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')) order by " + MyLib._d.sml_database_list._data_name;
                                    //query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + " from " + MyLib._d.sml_database_list._table;
                                    DataSet __dbResult = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
                                    bool __foundPOSStarterDB = false;

                                    if (__dbResult.Tables.Count > 0)
                                    {
                                        DataRow[] __getRows = __dbResult.Tables[0].Select();

                                        for (int __row = 0; __row < __getRows.Length; __row++)
                                        {
                                            //_gridDatabaseList._addRow();
                                            //int rowDataGrid = _gridDatabaseList._rowData.Count - 1;
                                            //_gridDatabaseList._cellUpdate(rowDataGrid, 0, __getRows[__row].ItemArray[0].ToString(), false);
                                            //_gridDatabaseList._cellUpdate(rowDataGrid, 1, __getRows[__row].ItemArray[1].ToString(), false);
                                            if (__getRows[__row].ItemArray[0].ToString().Equals(MyLib._myGlobal._mainDatabasePOSStarter))
                                            {
                                                __foundPOSStarterDB = true;
                                                break;
                                            }
                                        } // for
                                    }

                                    if (__foundPOSStarterDB == false)
                                    {
                                        if (_createPOSStarterDatabase())
                                        {
                                            __foundPOSStarterDB = true;
                                        }
                                    }

                                    if (__foundPOSStarterDB == true)
                                    {

                                        string __message_1 = String.Format(MyLib._myGlobal._resource("warning93"), MyLib._myGlobal._userName, MyLib._myGlobal._getUserLevelName(MyLib._myGlobal._userLevel));
                                        string __message_2 = MyLib._myGlobal._resource("warning94");
                                        MessageBox.Show(__message_1, __message_2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        MyLib._myGlobal._userLoginSuccess = true;
                                        _saveProfile(__data_provider_code, __data_database_group, __data_user_code, false);
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

            this.Cursor = Cursors.Default;
        }

        public virtual void _saveProfile(string providerCode, string groupName, string userCode, Boolean firstLogin)
        {
            if (MyLib._myGlobal._emergencyMode == false)
            {
                StringBuilder __xmlStr = new StringBuilder(String.Concat(MyLib._myGlobal._xmlHeader, "<node>"));
                __xmlStr.Append("<server>");
                if (firstLogin)
                {
                    __xmlStr.Append(String.Concat("<name>", "www.smlsoft.com:8080", "</name>"));
                }
                else
                {
                    __xmlStr.Append(String.Concat("<name>", this._screenTop._getDataStr(_webservice_url).ToString(), "</name>"));
                }
                __xmlStr.Append("</server>");
                __xmlStr.Append(String.Concat("<provider>", providerCode, "</provider>"));
                __xmlStr.Append(String.Concat("<group>", groupName, "</group>"));
                __xmlStr.Append(String.Concat("<user>", userCode, "</user>"));
                __xmlStr.Append(String.Concat("<proxy_url>", MyLib._myGlobal._proxyUrl, "</proxy_url>"));
                __xmlStr.Append(String.Concat("<proxy_use>", MyLib._myGlobal._proxyUsed, "</proxy_use>"));
                __xmlStr.Append(String.Concat("<proxy_user>", MyLib._myGlobal._proxyUser, "</proxy_user>"));
                __xmlStr.Append(String.Concat("<proxy_password>", MyLib._myGlobal._proxyPassword, "</proxy_password>"));
                __xmlStr.Append(String.Concat("</node>"));
                string __xPathName = Path.GetTempPath();
                string __xFileName = Path.GetTempPath() + "\\" + MyLib._myGlobal._profileFileName;
                StreamWriter __sr = File.CreateText(__xFileName);
                __sr.WriteLine(__xmlStr.ToString());
                __sr.Close();
            }
        }

        public virtual void _loadLastProfile()
        {
            try
            {
                string __xPathName = Path.GetTempPath();
                string __xFileName = Path.GetTempPath() + "\\" + MyLib._myGlobal._profileFileName;
                XmlDocument xDoc = new XmlDocument();
                try
                {
                    xDoc.Load(__xFileName);
                }
                catch
                {
                    _saveProfile("demo", "SML", "demo", true);
                    xDoc.Load(__xFileName);
                }
                xDoc.DocumentElement.Normalize();
                XmlElement __xRoot = xDoc.DocumentElement;
                XmlNodeList __xReader = __xRoot.GetElementsByTagName("name");

                for (int __table = 0; __table < __xReader.Count; __table++)
                {
                    XmlNode __xFirstNode = __xReader.Item(__table);
                    if (__xFirstNode.NodeType == XmlNodeType.Element)
                    {
                        XmlElement __xTable = (XmlElement)__xFirstNode;
                        //_gridWebservice._addRow();
                        //_gridWebservice._cellUpdate(__table, 0, __xTable.InnerText, false);
                        this._screenTop._setDataStr(_webservice_url, __xTable.InnerText);
                    }
                }

                for (int __loop = 1; __loop < 8; __loop++)
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
                                case 1: break;
                                case 2: break;
                                case 3: break;
                                case 4: MyLib._myGlobal._proxyUrl = __xTable.InnerText; break;
                                case 5: MyLib._myGlobal._proxyUsed = (int)MyLib._myGlobal._decimalPhase(__xTable.InnerText); break;
                                case 6: MyLib._myGlobal._proxyUser = __xTable.InnerText; break;
                                case 7: MyLib._myGlobal._proxyPassword = __xTable.InnerText; break;

                            }
                        }
                    } // for
                } // for
            }
            catch
            {
            }
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

        private void _webServiceConfigButton_Click(object sender, EventArgs e)
        {
            this._onConfigWebService();
        }

        protected virtual void _onConfigWebService()
        {
            _providerConfig __provider = new _providerConfig(2, "Select Provider");
            __provider._selectCode = MyLib._myGlobal._mainProviderPOSStarter;
            __provider.ShowDialog();
            if (__provider._exitMode == 1)
            {
                return;
            }
            MyLib._myUtil._startDialog(this, "", new _serverSetup(__provider));
        }
            

    }

    public class _userLoginScreen : MyLib._myScreen
    {
        public _userLoginScreen()
        {
            this._maxColumn = 1;
            this._getResource = false;
            this._table_name = "Data";

            this.Invalidate();

        }
    }
}
