using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using System.Xml;

namespace MyLib
{
    public static class _myGlobal
    {
        public static Boolean _isDesignMode = true;
        public static List<_providerListClass> _providerList = new List<_providerListClass>();
        public static List<_printerListClass> _printerList = new List<_printerListClass>();
        public static DateTime _xmlUpdate = new DateTime(2012, 5, 1, 2, 9, 40);
        public static Boolean _autoLogin = false;
        public static Boolean _useNoVat = false;
        public static string _programName = "";
        public static Form _mainForm;
        public static ToolStrip _statusStrip;
        public static ToolStripStatusLabel _statusLabel;
        public static Boolean _isUserTest = false;
        public static Boolean _isUserSupport = false;

        public static Boolean _isDemo = false;
        public static Label _statusLabeltemp;
        public static _versionType _isVersionEnum = _versionType.Null;
        public static Boolean _syncDataActive = false;
        public static Boolean _isAutoVerify = true;
        public static Boolean _isSendSMS = true;
        /// <summary>
        /// true=ติดต่อสำเร็จ,false=ติดต่อไม่สำเร็จ
        /// </summary>
        public static Boolean _serviceConnected = false;
        /// <summary>
        /// ให้แสดง Menu ทุกรายการ ไม่สน TAG
        /// </summary>
        public static Boolean _menuAll = false;
        public static int _subVersion = 1; // 0=รุ่นปัจจุบัน,1=เก่ากว่า  (สุริวงศ์ =0 เท่านั้น)
        /*public static int _pointForQty = 2;
        public static int _pointForPrice = 2;
        public static int _pointForAmount = 2;
        public static double _vatRate = 7.0;*/

        public static Boolean _save_logs = false;
        // toe
        /// <summary>
        /// อนุญาติให้แก้ไขข้อมูล Master
        /// </summary>
        public static Boolean _allowChangeMaster = true;

        /// <summary>อนุญาติให้ ดึง report</summary>
        public static Boolean _allowProcessReportServer = true;

        //โหมดฉุกเฉิน
        public static Boolean _emergencyMode = false;
        public static String _emergencyURL = "";
        public static String _emergencyPort = "";

        public static String _OEMVersion = "";

        /// <summary>Provider สำหรับ POS Starter</summary>
        public static String _mainProviderPOSStarter = "POS";
        /// <summary>Database สำหรับ POS Starter</summary>
        public static String _mainDatabasePOSStarter = "posstarter";

        //toe
        public static String _posConfigOpenWelcomeScreen = @"C:\smlsoft\welcomescreenconfig.xml";
        public static String _programClientConfigSFileName = @"c:\smlsoft\programClientConfig.xml";

        public static String _fixBranchCode = "";
        public static String _fixBranchName = "";
        /// <summary>
        /// สาขา
        /// </summary>
        public static String _branchCode = "000";

        public static String _branchCodeFormSerialDrive = "";
        /// <summary>
        /// เป็น User ที่สามารถทำการ Lock เอกสารได้หรือไม่
        /// </summary>
        public static Boolean _isUserLockDocument = false;
        /// <summary>
        /// เป็น User ที่สามารถ reset Print Log ได้
        /// </summary>
        public static Boolean _isUserResetPrintLog = false;
        /// <summary>
        /// Font มาตรฐาน
        /// </summary>        
        public static Font _myFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
        //public static Font _myFont = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
        /// <summary>
        /// Font สำหรับใช้กับ FormDesigner
        /// </summary>
        public static Font _myFontFormDesigner = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        /// รหัสที่ใช้ในการติดต่อกับ Web service เพิ่มความปลอดภัยไม่ให้คนอื่นเรียกใช้ web service</summary>
        public static string _internalCode = MyLib._myGlobal._internalCode = "111222";
        //-------------------------------------------------------
        //MOO
        /// <summary>ชื่อบริษัท</summary>
        public static string _ltdId;
        /// <summary>ชื่อบริษัท</summary>
        public static string _ltdName;
        /// <summary>ที่อยู่บริษัท</summary>
        public static string _ltdAddress;
        /// <summary>เบอร์โทรศัพท์บริษัท</summary>
        public static string _ltdTel;
        /// <summary>โทรสารบริษัท</summary>
        public static string _ltdFax;
        /// <summary>เลขที่ผู้เสียภาษีบริษัท</summary>
        public static string _ltdTax;
        /// <summary>ชื่อผู้ประกอบการ</summary>
        public static string _ltdBusinessName;
        /// <summary>ชื่อสถานที่ประกอบการ</summary>
        public static string _ltdWorkplace;
        /// <summary>ภาษีมูลค่าเพิ่ม</summary>
        public static string _vat_rate;

        /// <summary>ประเภทสถานประกอบการ 0=สำนักงานใหญ่,1=สาขา</summary>
        public static int _branch_type = 0;
        /// <summary>
        /// การปัดเศษ 0 roundbank, 1 digit
        /// </summary>
        public static int _round_Type = 0;

        /// <summary>ลำดับที่สาขา</summary>
        public static string _branch_code = "";

        public static string _approve_po_group = "";
        public static string _approve_sr_group = "";
        public static string _approve_ss_group = "";

        public static float _searchItemZoomLevel = 1f;
        public static float _searchTableZoomLevel = 1f;

        public static string _getRemoteAddrURL = "http://www.smlsoft.com/ipaddr.php";

        //-------------------------------------------------------
        private static string _userCodeTemp = "";
        /// <summary>รหัสผู้ใช้งาน</summary>
        public static string _userCode
        {
            set
            {
                _userCodeTemp = value;
            }
            get
            {
                return _userCodeTemp.ToUpper();
            }
        }
        public static string _masterWebservice = "www.smlsoft.com:8080";
        public static string _masterWebService_2 = "www.smldatacenter.com";
        //public static string _masterWebservice = "localhost:8084";
        public static string _masterDatabaseName = "SMLMASTER";
        public static string _masterRegisterDatabaseName = "register";
        public static string _masterConfigXmlName = "SMLConfigSMLMASTER.xml";
        public static string _masterPointCenterSyncName = "SMLPOINTCENTER";
        public static string _internetSyncName = "thai7";
        public static _databaseType _masterDatabaseType = _databaseType.PostgreSql;
        /// <summary>
        /// ใช้ระบบ Proxy (0=Disable,1=Enable)
        /// </summary>
        public static int _proxyUsed = 0;
        public static string _proxyUrl = "";
        public static string _proxyUser = "";
        public static string _proxyPassword = "";
        /// <summary>ชื่อผู้ใช้งาน</summary>
        public static string _userName = "";
        /// <summary>รหัสผ่าน</summary>
        public static string _password = "";
        /// <summary>ระดับของผู้ใช้งาน</summary>
        public static int _userLevel = 0;
        /// <summary>ภาษาหน้าจอที่กำลังใช้งาน 0=Thai,1=Eng,2,3,4,5</summary>
        public static _languageEnum _language = _languageEnum.Thai;
        /// <summary>รหัสกลุ่มข้อมูลที่ใช้งาน</summary>
        public static string _dataGroup = "SML";
        /// <summary>ชื่อ Server ที่เก็บ Webservice</summary>
        public static string _webServiceServer;
        public static ArrayList _webServiceServerList = new ArrayList();
        /// <summary>
        /// รายชื่อ Table ที่จะ Unlock เมื่อจบโปรแกรม หรือโปรแกรมหลุด หรือปิดเครื่อง
        /// </summary>
        public static ArrayList _tableForAutoUnlock = new ArrayList();

        public static string _getFirstWebServiceServer
        {
            get
            {
                if (_webServiceServerList.Count == 0)
                    return "";

                return ((MyLib._myWebserviceType)_webServiceServerList[0])._webServiceName;
            }
        }
        /// <summary>
        /// ชื่อเครื่องคอมพิวเตอร์
        /// </summary>
        public static string _computerNameTemp = "";
        public static string _computerName
        {
            set
            {
                StringBuilder __buffer = new StringBuilder();
                for (int __loop = 0; __loop < value.Length; __loop++)
                {
                    if (value[__loop] > ' ')
                    {
                        __buffer.Append(value[__loop]);
                    }
                }
                _computerNameTemp = (__buffer.ToString() + Guid.NewGuid().ToString()).Replace("\\", "").Replace("-", "");
            }
            get
            {
                return _computerNameTemp;
            }
        }
        /// <summary>
        /// ย่อข้อมูลระหว่างรับส่ง
        /// </summary>
        public static bool _compressWebservice = false;
        /// <summary>
        ///  GUID สำหรับใช้ในระบบหลังจาก Login
        /// </summary>
        public static string _guidTemp = "";
        /// <summary>ชื่อ webService</summary>
        public static string _webServiceName;//"SMLWebServiceOld";
        /// <summary>ชื่อแฟ้ม XML ที่ใช้เก็บการใช้งาน (เก็บไว้ที่เครื่อง Client)</summary>
        public static string _profileFileName;
        public static _databaseManage._loginHistoryClass _loginHistory = new _databaseManage._loginHistoryClass();
        public static bool _serverSetupCreateDatabaseSuccess = false;
        /// <summary>การ Login สมบูรณ์</summary>
        public static bool _userLoginSuccess = false;
        /// <summary>กลุ่มหน้าจอค้นหาของ User ที่ Login เข้ามาใช้งาน</summary>
        public static int _userSearchScreenGroup = 1;
        public static string _providerCode = "";
        /// <summary>ชื่อ Database หลัก (สำหรับเก็บค่าทั้งระบบ)</summary>
        static string _mainDatabasePrivate;
        public static string _registerXmlFileName = "sml.war";
        public static string _productCode = "FREE";
        public static string _productDesc = "FREE";
        public static string _productCallCenterID = "";
        public static int _maxUser = 1;
        public static int _maxUserCurrent = 0;
        public static string _productVersion = "0";
        public static int _nextProcessTime = 10;
        //somruk
        /// <summary>
        ///Menu รายการเมนูทั้งหมด
        /// </summary>
        public static _mainMenuClass _listMenuAll = new _mainMenuClass();
        public static int _serverCount = 100;

        public static string _smlCloudServicePath = "/SMLCloudService/service/rest";

        public static void _help(string name)
        {
            System.Diagnostics.Process.Start("http://www.smlsoft.com/smlaccountmanual/" + name);
        }

        public static bool _isVersionAccount
        {
            get
            {
                return (_isVersionEnum == _versionType.SMLAccount ||
                    _isVersionEnum == _versionType.SMLAccountPOS ||
                    _isVersionEnum == _versionType.SMLAccountPOSProfessional ||
                    _isVersionEnum == _versionType.SMLAccountProfessional ||
                    _isVersionEnum == _versionType.SMLIMS ||
                    _isVersionEnum == _versionType.IMSAccountPro) ? true : false;
            }
        }

        public static void _loadProgramClientConfig()
        {
            if (File.Exists(_programClientConfigSFileName.ToLower()))
            {
                _programClientConfig __config = null;

                try
                {
                    // ลองอ่านจาก location ใหม่ (c:\smlsoft\)

                    TextReader readFile = new StreamReader(MyLib._myGlobal._programClientConfigSFileName.ToLower());
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(_programClientConfig));
                    __config = (_programClientConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch
                {
                }

                if (__config != null)
                {
                    _myFont = new Font(__config._clientFontName, __config._clientFontSize);
                }
            }
        }

        public static DataSet _convertStringToDataSet(string source)
        {

            // ดัก error text ด้วยนะ

            DataSet __ds = new DataSet();
            try
            {
                source = _sanitizeXmlString(source);

                XmlTextReader __readXml = new XmlTextReader(new StringReader(source));
                // Convert MDataSet into a standard ADO.NET DataSet
                __ds.ReadXml(__readXml, XmlReadMode.InferSchema);
                if (__ds.Tables.Count == 0)
                {
                    DataTable __table = new DataTable();
                    __ds.Tables.Add(__table);
                }
            }
            catch (Exception e)
            {
                string __error = e.Message;
            }
            return __ds;
        }

        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        public static string _sanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            StringBuilder buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (_isLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public static bool _isLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

        public static string _smlConfigFile
        {
            get
            {
                string __smlConfigPath = @"C:\\smlconfig\\";
                bool __isDirCreate = MyLib._myUtil._dirExists(__smlConfigPath);

                if (__isDirCreate == false)
                {
                    System.IO.Directory.CreateDirectory(__smlConfigPath); // create folders
                }
                return __smlConfigPath;
            }
        }

        private static string _getOSName()
        {
            System.OperatingSystem __os = System.Environment.OSVersion;
            string __osName = "Unknown";
            switch (__os.Platform)
            {
                case System.PlatformID.Win32Windows:
                    switch (__os.Version.Minor)
                    {
                        case 0:
                            __osName = "Windows 95";
                            break;
                        case 10:
                            __osName = "Windows 98";
                            break;
                        case 90:
                            __osName = "Windows ME";
                            break;
                    }
                    break;
                case System.PlatformID.Win32NT:
                    switch (__os.Version.Major)
                    {
                        case 3:
                            __osName = "Windws NT 3.51";
                            break;
                        case 4:
                            __osName = "Windows NT 4";
                            break;
                        case 5:
                            if (__os.Version.Minor == 0)
                                __osName = "Windows 2000";
                            else if (__os.Version.Minor == 1)
                                __osName = "Windows XP";
                            else if (__os.Version.Minor == 2)
                                __osName = "Windows Server 2003";
                            break;
                        case 6:
                            if (__os.Version.Minor == 0)
                                __osName = "Windows Vista";
                            else if (__os.Version.Minor == 1)
                                __osName = "Windows 7";
                            break;
                    }
                    break;
            }

            return __osName + ", " + __os.Version.ToString();
        }

        public static string _compileWebserviceName(string name)
        {
            name = name.Replace(" ", "").Replace(" ", "").Trim();
            if (name.Length > 0 && name[0] == '@')
            {
                name = name.Replace("@", "");
                MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                String[] _split = name.Split(',');
                if (_split.Length == 3)
                {
                    DataSet __result2 = __ws._query(MyLib._myGlobal._masterRegisterDatabaseName, "select last_ip from smlaccount where user_code = \'" + _split[0].ToString() + "\' and user_password = \'" + _split[1].ToString() + "\' ");
                    if (__result2.Tables.Count > 0 && __result2.Tables[0].Rows.Count > 0)
                    {
                        name = __result2.Tables[0].Rows[0][0].ToString().Trim() + ":" + _split[2].ToString();
                    }
                }
            }
            return name;
        }

        /// <summary>
        /// เป็นการใช้บริการเช่า server ของ sml หรือไม่
        /// </summary>
        public static Boolean _isCloudService = false;

        public static void _registerProcess()
        {
            if (MyLib._myGlobal._guid.Length > 0 && MyLib._myGlobal._guid.Equals("SMLX") == false)
            {
                _myFrameWork __myFrameWork = new _myFrameWork();
                Boolean __pass = false;

                // toe for cloud check
                string __cloudServiceUrl = ((MyLib._myGlobal._webServiceServer.ToLower().IndexOf("http") != 0) ? "http://" : "") + MyLib._myGlobal._webServiceServer + _smlCloudServicePath + "/checkConnect";
                if (_checkConnectionAvailable(__cloudServiceUrl))
                {

                    try
                    {
                        string __loadXml = __myFrameWork._loadXmlFile(_registerXmlFileName);
                        string __xml = Encoding.Unicode.GetString(Convert.FromBase64String(__loadXml));
                        string[] __getData = __xml.Split('|');
                        string __productCode = __getData[0].ToString();
                        string __productDesc = __getData[1].ToString();
                        string __productCallCenterID = __getData[2].ToString();


                        MyLib._myGlobal._productCode = __productCode;
                        MyLib._myGlobal._productCallCenterID = __productCallCenterID;

                        if (++_serverCount > 100)
                        {

                            string __getCompanyRestUrl = ((MyLib._myGlobal._webServiceServer.ToLower().IndexOf("http") != 0) ? "http://" : "") + MyLib._myGlobal._webServiceServer + _smlCloudServicePath + "/getCompany?providerCode=" + MyLib._myGlobal._providerCode.ToUpper();

                            _restClient __rest = new _restClient(__getCompanyRestUrl);
                            string __response = __rest.MakeRequest();
                            if (__response.Length > 0)
                            {
                                DataSet __dataSet = MyLib._myGlobal._convertStringToDataSet(__response);
                                if (__dataSet.Tables.Count > 0)
                                {
                                    // have provider config in cloud
                                    if (__dataSet.Tables[0].Rows.Count > 0)
                                    {
                                        MyLib._myGlobal._maxUser = (int)MyLib._myGlobal._decimalPhase(__dataSet.Tables[0].Rows[0]["max_user"].ToString());
                                        MyLib._myGlobal._productDesc = __dataSet.Tables[0].Rows[0]["company_name"].ToString();


                                        __pass = true;
                                        _isCloudService = true;

                                    }
                                    _serverCount = 0;
                                }
                            }
                        }

                        // toe get server ip sent to sml
                        if (_myGlobal._getRemoteAddrURL.Length > 0)
                        {
                            if (_checkConnectionAvailable(_myGlobal._getRemoteAddrURL))
                            {
                                string __serverIp = __myFrameWork._getServerIPAddr(_myGlobal._getRemoteAddrURL);
                                if (__serverIp.Length > 0)
                                {
                                    // send ip server to sml server
                                    MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                                    string __result = __ws._queryInsertOrUpdate(MyLib._myGlobal._masterRegisterDatabaseName, "update smlaccount set last_ip=\'" + __serverIp + "\', last_ip_update = LOCALTIMESTAMP where product_code=\'" + MyLib._myGlobal._productCode + "\'");
                                }
                            }
                        }


                    }
                    catch
                    {

                    }
                }

                // check ว่า connect ไปที่ www.smlsoft.com หรือเปล่า
                if (__pass == false && ((MyLib._myGlobal._webServiceServer.ToLower().IndexOf("www.smlsoft.com") != -1) || MyLib._myGlobal._webServiceServer.ToLower().IndexOf("www.smldatacenter.com") != -1))
                {
                    try
                    {
                        try
                        {

                            string __loadXml = __myFrameWork._loadXmlFile(_registerXmlFileName);
                            string __xml = Encoding.Unicode.GetString(Convert.FromBase64String(__loadXml));
                            string[] __getData = __xml.Split('|');
                            string __productCode = __getData[0].ToString();
                            string __productDesc = __getData[1].ToString();
                            string __productCallCenterID = __getData[2].ToString();
                            MyLib._myGlobal._productCode = __productCode;
                            MyLib._myGlobal._productCallCenterID = __productCallCenterID;
                        }
                        catch
                        {
                            /*MyLib._myGlobal._productCode = "";
                            MyLib._myGlobal._productCallCenterID = __productCallCenterID;*/

                        }



                        if (++_serverCount > 100)
                        {
                            // ไปเช็ค register code และ limit user จาก sml ได้เลย
                            MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                            if (__ws._testConnect())
                            {
                                DataTable __getRegister = __ws._query2(MyLib._myGlobal._masterRegisterDatabaseName, "select * from smlcloud where provider_code =\'" + MyLib._myGlobal._providerCode.ToUpper() + "\'").Tables[0];

                                if (__getRegister.Rows.Count > 0)
                                {
                                    MyLib._myGlobal._maxUser = (int)MyLib._myGlobal._decimalPhase(__getRegister.Rows[0]["max_user"].ToString());
                                    MyLib._myGlobal._productDesc = __getRegister.Rows[0]["company"].ToString();


                                    __pass = true;
                                    _isCloudService = true;

                                }
                                _serverCount = 0;
                            }
                        }
                    }
                    catch
                    {
                    }

                    if (_isCloudService == true)
                    {
                        __pass = true;
                    }
                }




                if (__pass == false && _isCloudService == false)
                {
                    try
                    {
                        {
                            try
                            {
                                string __loadXml = __myFrameWork._loadXmlFile(_registerXmlFileName);
                                string __xml = Encoding.Unicode.GetString(Convert.FromBase64String(__loadXml));
                                string[] __getData = __xml.Split('|');
                                string __productCode = __getData[0].ToString();
                                string __productDesc = __getData[1].ToString();
                                string __productCallCenterID = __getData[2].ToString();
                                int __maxUser = (int)MyLib._myGlobal._decimalPhase(__getData[3].ToString());
                                //
                                // get register version
                                if (__getData.Length > 4)
                                {
                                    string __productVersion = __getData[4].ToString();
                                    MyLib._myGlobal._productVersion = __productVersion;
                                }

                                MyLib._myGlobal._productCode = __productCode;
                                MyLib._myGlobal._productDesc = __productDesc;
                                MyLib._myGlobal._productCallCenterID = __productCallCenterID;
                                MyLib._myGlobal._maxUser = __maxUser;

                                // toe get server ip sent to sml
                                if (_myGlobal._getRemoteAddrURL.Length > 0)
                                {

                                    if (_checkConnectionAvailable(_myGlobal._getRemoteAddrURL))
                                    {
                                        string __serverIp = __myFrameWork._getServerIPAddr(_myGlobal._getRemoteAddrURL);
                                        if (__serverIp.Length > 0)
                                        {
                                            // send ip server to sml server
                                            MyLib._myFrameWork __ws = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                                            string __result = __ws._queryInsertOrUpdate(MyLib._myGlobal._masterRegisterDatabaseName, "update smlaccount set last_ip=\'" + __serverIp + "\', last_ip_update = LOCALTIMESTAMP where product_code=\'" + MyLib._myGlobal._productCode + "\'");
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        if (++_serverCount > 100)
                        {
                            string __macAddress = __myFrameWork._queryInsertOrUpdate("", "MACADDRESS").Replace("\"", "");
                            if (__macAddress.Length > 0)
                            {
                                MyLib._myFrameWork __smlWs = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                                DataTable __getRegister = __smlWs._query2(MyLib._myGlobal._masterRegisterDatabaseName, "select * from smlaccount where id=\'" + __macAddress + "\'").Tables[0];
                                if (__getRegister.Rows.Count > 0)
                                {
                                    string __productCode = __getRegister.Rows[0]["product_code"].ToString(); ;
                                    string __productDesc = __getRegister.Rows[0]["company"].ToString();
                                    string __productCallCenterID = __getRegister.Rows[0]["call_center_id"].ToString();
                                    int __maxUser = (int)MyLib._myGlobal._decimalPhase(__getRegister.Rows[0]["max_user"].ToString());
                                    string __versionCode = __getRegister.Rows[0]["version_code"].ToString();

                                    if (MyLib._myGlobal._productCode.Equals(__productCode) == false ||
                                        MyLib._myGlobal._productDesc.Equals(__productDesc) == false ||
                                        MyLib._myGlobal._productCallCenterID.Equals(__productCallCenterID) == false ||
                                        MyLib._myGlobal._maxUser != __maxUser ||
                                        MyLib._myGlobal._productVersion.Equals(__versionCode) == false)
                                    {
                                        MyLib._myGlobal._productCode = __productCode;
                                        MyLib._myGlobal._productDesc = __productDesc;
                                        MyLib._myGlobal._productCallCenterID = __productCallCenterID;
                                        MyLib._myGlobal._maxUser = __maxUser;
                                        MyLib._myGlobal._productVersion = __versionCode;

                                        string __source = Convert.ToBase64String(Encoding.Unicode.GetBytes(String.Format("{0}|{1}|{2}|{3}|{4}", MyLib._myGlobal._productCode, MyLib._myGlobal._productDesc, MyLib._myGlobal._productCallCenterID, MyLib._myGlobal._maxUser.ToString(), MyLib._myGlobal._productVersion)));
                                        __myFrameWork._saveXmlFile(_registerXmlFileName, __source);
                                    }
                                }
                                /* toe รอ support พร้อมค่อยปล่อยก็แล้วกัน
                            else
                            {
                                //toe หากไม่เจอ mac ให้ลบ war ทิ้ง
                                //__myFrameWork._saveXmlFile(_registerXmlFileName, "");
                                //MyLib._myGlobal._maxUser = 0;
                                MessageBox.Show("ไม่พบข้อมูลลงทะเบียน กรุณาติดต่อเจ้าหน้าที่");
                                Application.Exit();
                            }*/
                            }
                            {
                                // ส่งรายละเอียด
                                MyLib._myFrameWork __smlWs = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                                /*string __compareTime = DateTime.Now.AddMinutes(-5).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                                string __query1 = "delete from user_list where last_update<\'"+__compareTime +"\' or (computer_name=\'" + MyLib._myGlobal._computerName + "\' and product_code=\'" + MyLib._myGlobal._productCode + "\')";
                                string __lastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                                string __query2 = "insert into user_list (computer_name,product_code,last_update) values (\'" + MyLib._myGlobal._computerName + "\',\'" + MyLib._myGlobal._productCode + "\',\'" + __lastUpdate + "\')";*/
                                string __query1 = "delete from user_list where last_update + interval '24 hour'<now() or (computer_name=\'" + MyLib._myGlobal._computerName + "\' and product_code=\'" + MyLib._myGlobal._productCode + "\')";
                                string __query2 = "insert into user_list (computer_name,product_code,os_name,database_name,last_update) values (\'" + MyLib._myGlobal._computerName + "\',\'" + MyLib._myGlobal._productCode + "\',\'" + _getOSName() + "\',\'" + MyLib._myGlobal._databaseName + "\',now())";
                                __smlWs._queryInsertOrUpdate2(MyLib._myGlobal._masterRegisterDatabaseName, __query1);
                                __smlWs._queryInsertOrUpdate2(MyLib._myGlobal._masterRegisterDatabaseName, __query2);
                                _serverCount = 0;
                            }
                        }


                    }
                    catch
                    {
                    }
                }
            }
        }

        public static bool _checkConnectionAvailable(string strServer)
        {
            try
            {
                if (strServer.IndexOf("http") != 0)
                {
                    strServer = "http://" + strServer;
                }

                HttpWebRequest reqFP = (HttpWebRequest)HttpWebRequest.Create(strServer);
                reqFP.Timeout = 1000;
                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    // HTTP = 200 - Internet connection available, server online
                    rspFP.Close();
                    return true;
                }
                else
                {
                    // Other status - Server or connection not available
                    rspFP.Close();
                    return false;
                }
            }
            catch (WebException)
            {
                // Exception - connection not available
                return false;
            }
            catch (Exception ex)
            {
                MyLib._myGlobal._writeEventLog(ex.Message.ToString());
                return false;
            }
        }

        public static string _guid
        {
            set
            {
                _guidTemp = value;
            }
            get
            {
                return _guidTemp;
            }
        }

        //public static string _mainMenuId = "";
        //public static string _mainMenuCode = "";

        // เก็บค่า menu ไว้ต้องสร้าง object (โกง)
        public static string _mainMenuIdPassTrue = "";
        public static string _mainMenuCodePassTrue = "";

        /// <summary>
        /// true = ผ่านตลอด ไม่เช็คสิทธิ์ เมนู
        /// </summary>
        public static bool _nonPermission = false;
        public static string _mainDatabase
        {
            set
            {
                _mainDatabasePrivate = value;
            }
            get
            {
                return _mainDatabasePrivate + _providerCode.ToLower();
            }
        }

        public static Boolean _addUpperDatebaseName = true;

        /// <summary>ชื่อ Database ที่เข้ามาใช้งาน</summary>
        public static string _databaseNameTemp = "";
        public static string _databaseName
        {
            set
            {
                _databaseNameTemp = value;
            }
            get
            {
                return (_databaseNameTemp == null) ? null : (_addUpperDatebaseName == true) ? _databaseNameTemp.ToUpper() : _databaseNameTemp;
            }
        }

        /// <summary>โครงสร้างข้อมูล สำหรับการ verify</summary>
        public static string _databaseVerifyXmlFileName = "";
        /// <summary>โครงสร้างข้อมูล</summary>
        public static string _databaseStructFileName;
        /// <summary>XML หน้าจอค้นหา</summary>
        public static string _dataViewXmlFileName;
        /// <summary>XML หน้าจอค้นหา (Template)</summary>
        public static string _dataViewTemplateXmlFileName;
        /// <summary>
        /// xml ภาษาต่างๆ เอาไว้แปล
        /// </summary>
        public static string _languageXmlFileName;
        /// <summary>วันที่กำลังใช้งาน (สำหรับบันทึกข้อมูล)</summary>
        public static DateTime _workingDate;
        /// <summary>0=ค.ศ,1=พ.ศ.</summary>
        public static int _year_type_temp = 1;
        public static int _year_type
        {
            set
            {
                _year_type_temp = value;
                _year_add = (value == 1) ? 543 : 0;
            }
            get
            {
                return _year_type_temp;
            }
        }
        public static int _year_add;
        public static int __calturedefult;
        /// <summary>ปีที่บวกจาก ค.ศ. (ถ้าเป็นอังกฤษให้มีค่าเป็น 0)</summary>
        public static int _year_current;
        public static string _xmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        public static Size _mainSize;
        public static object[] _databaseColumnTypeList = new object[] { "varchar", "currency", "int", "tinyint", "smallint", "float", "date", "text" };
        public static string _tableNameView;
        public static string _tableNameViewColumn;

        public static string _tableCustomNameView;
        public static string _tableCustomNameViewColumn;

        public static string[] _monthList = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
        public static string[] _monthListShort = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
        // วันหยุดประจำสัปดาห์ (เริ่มนับวันอาทิตย์)
        public static ArrayList _holiday = new ArrayList();
        // วันหยุดราชการ
        public static ArrayList _officialHoliday = new ArrayList();
        //
        public static bool _isCheckRuningBeforSave = false;
        //
        /// <summary>
        /// ชื่อโครงสร้างฐานข้อมูลหลักของโปรแกรม
        /// </summary>
        public static string _mainDatabaseStruct = "";
        /// <summary>
        /// ชื่อแฟ้มที่เก็บรายละเอียดการเชื่อมข้อมูล
        /// </summary>
        static string _databaseConfigPrivate = "";
        /// <summary>
        /// ข้อความที่ป้อนล่าสุด
        /// </summary>
        public static string _lastTextBox = "";

        static List<_languageClass> _resourceList = new List<_languageClass>();
        public static List<_languageClass> _resourceFromXml = new List<_languageClass>();
        public static Boolean _connectMySqlForResource = false;

        /// <summary>Fast Report XML Filename</summary>
        public static String _fastReportXmlFileName = "";
        /// <summary>POS Design XML Filename</summary>
        public static String _posDesignXmlFileName = "";

        public static int _languageNumber
        {
            get
            {
                return _getLanguageNumber(_language);
            }
        }

        public static int _getLanguageNumber(_languageEnum language)
        {
            switch (language)
            {
                case _languageEnum.Thai: return 0;
                case _languageEnum.English: return 1;
                case _languageEnum.Malayu: return 2;
                case _languageEnum.Chainese: return 3;
                case _languageEnum.India: return 4;
                case _languageEnum.Lao: return 5;
            }
            return 0;
        }

        public static void _autoVerifyDatabase(DateTime lastUpdate)
        {
            try
            {
                _databaseManage._verifyDatabase __verify = new _databaseManage._verifyDatabase(MyLib._myGlobal._databaseName);
                __verify.ControlBox = false;
                __verify.ShowDialog();
                if (__verify._progressBarValue == 100)
                {
                    MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                    string __dateTimeStr = lastUpdate.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                    string __query = "update " + MyLib._d.sml_database_list._table + " set " + MyLib._d.sml_database_list._last_database_xml_update + "=\'" + __dateTimeStr + "\' where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_database_name) + "=\'" + MyLib._myGlobal._databaseName.ToUpper() + "\'";
                    string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result);
                    }
                }
                else
                {
                    MessageBox.Show("ระบบตรวจสอบข้อมูลไม่สมบูรณ์\nอาจทำให้โปรแกรมมีปัญหา\nกรุณาจบโปรแกรม และเรียกโปรแกรมใหม่");
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        public static void _checkFirst()
        {
            _isDesignMode = false;
            bool __isVerify = false;

            if (_isAutoVerify == true)
            {
                // ตรวจสอบโครงสร้างว่าใหม่หรือเปล่า ถ้าโครงสร้างเก่า ให้ copy โครงสร้างใหม่ไปทับอัตโนมัติ
                string __xmlStructFileName = ("sml_struct_" + MyLib._myGlobal._providerCode + ".xml").ToUpper();
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();

                // update new structer
                try
                {
                    CultureInfo __dateZone = new CultureInfo("en-US");
                    DateTime __lastUpdate = new DateTime(1968, 1, 1);
                    string __loadXml = __myFrameWork._loadXmlFile(__xmlStructFileName);

                    if (__loadXml.Length > 0)
                    {
                        try
                        {
                            __lastUpdate = DateTime.Parse(__loadXml, __dateZone);
                        }
                        catch
                        {
                        }
                        if (_xmlUpdate.CompareTo(__lastUpdate) > 0)
                        {
                            __isVerify = (MessageBox.Show("ตรวจพบการเปลี่ยนแปลงโครงสร้างข้อมูล ต้องการดำเนินการปรับปรุงหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) ? true : false;

                            if (__isVerify)
                            {
                                if (MyLib._myGlobal._databaseVerifyXmlFileName.Length > 0)
                                {
                                    __myFrameWork._sendXmlFile(MyLib._myGlobal._databaseVerifyXmlFileName);
                                }
                                __myFrameWork._sendXmlFile(MyLib._myGlobal._databaseStructFileName);
                                __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewXmlFileName);
                                __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);
                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_column");
                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_table");
                                //
                                string __dateTimeStr = _xmlUpdate.ToString("yyyy-MM-dd HH:mm:ss", __dateZone);
                                __myFrameWork._saveXmlFile(__xmlStructFileName, __dateTimeStr);
                            }
                        }
                    }
                }
                catch
                {
                }


                // ตรวจสอบว่า database ที่เลือกแล้ว verify หรือไม่ ถ้ายังก็ทำ auto เลย
                if (__isVerify)
                {


                    try
                    {
                        DateTime __lastUpdate = new DateTime();
                        string __query = "select " + MyLib._d.sml_database_list._last_database_xml_update + " from " + MyLib._d.sml_database_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_database_name) + "=\'" + MyLib._myGlobal._databaseName + "\'";
                        DataTable __data = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query).Tables[0];
                        if (__data.Rows.Count > 0)
                        {
                            try
                            {
                                string __getDateTime = __data.Rows[0][MyLib._d.sml_database_list._last_database_xml_update].ToString();
                                __lastUpdate = MyLib._myGlobal._convertDateFromQuery(__getDateTime);
                            }
                            catch
                            {
                            }
                            string __fileName = MyLib._myGlobal._databaseStructFileName;
                            DateTime __lastDateTimeUpdate = __myFrameWork._getFileLastUpdate(__fileName);
                            if (__lastDateTimeUpdate.CompareTo(__lastUpdate) > 0)
                            {
                                // กรณี xml มีการ update ต้อง verify auto
                                //if (MessageBox.Show("ตรวจพบการเปลี่ยนแปลงโคารงสร้างข้อมูล ต้องการดำเนินการปรับปรุงหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                _autoVerifyDatabase(__lastDateTimeUpdate);
                            }
                        }
                    }
                    catch
                    {
                        //if (MessageBox.Show("ตรวจพบการเปลี่ยนแปลงโคารงสร้างข้อมูล ต้องการดำเนินการปรับปรุงหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        _autoVerifyDatabase(DateTime.Now);
                    }

                    // toe ดึงค่า save log 
                    try
                    {
                        string __query = "select  *  from erp_option ";
                        DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                        if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
                        {
                            MyLib._myGlobal._save_logs = (MyLib._myGlobal._intPhase(__dataResult.Tables[0].Rows[0]["save_logs"].ToString()) == 1) ? true : false;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                // ตรวจสอบโครงสร้างว่าใหม่หรือเปล่า ถ้าโครงสร้างเก่า ให้ copy โครงสร้างใหม่ไปทับอัตโนมัติ
                string __xmlStructFileName = ("sml_struct_" + MyLib._myGlobal._providerCode + ".xml").ToUpper();
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();

                // update new structer
                try
                {
                    CultureInfo __dateZone = new CultureInfo("en-US");
                    DateTime __lastUpdate = new DateTime(1968, 1, 1);
                    string __loadXml = __myFrameWork._loadXmlFile(__xmlStructFileName);

                    if (__loadXml.Length > 0)
                    {
                        try
                        {
                            __lastUpdate = DateTime.Parse(__loadXml, __dateZone);
                        }
                        catch
                        {
                        }
                        if (_xmlUpdate.CompareTo(__lastUpdate) > 0)
                        {
                            __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);
                            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_column");
                            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_table");

                            MessageBox.Show("ตรวจพบการเปลี่ยนแปลงโครงสร้างข้อมูล กรุณาทำการตรวจสอบโครงสร้างฐานข้อมูลใหม่", "Database Verify", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public static void _showHtml(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception exc1)
            {
                // System.ComponentModel.Win32Exception is a known exception that occurs when Firefox is default browser.  
                // It actually opens the browser but STILL throws this exception so we can just ignore it.  If not this exception,
                // then attempt to open the URL in IE instead.
                if (exc1.GetType().ToString() != "System.ComponentModel.Win32Exception")
                {
                    // sometimes throws exception so we have to just ignore
                    // this is a common .NET bug that no one online really has a great reason for so now we just need to try to open
                    // the URL using IE if we can.
                    try
                    {
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("IExplore.exe", url);
                        System.Diagnostics.Process.Start(startInfo);
                        startInfo = null;
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message);
                    }
                }
            }
        }

        public static string _deleteAscError(string source)
        {
            StringBuilder __result = new StringBuilder();
            for (int __loop = 0; __loop < source.Length; __loop++)
            {
                if (source[__loop] >= 32 || source[__loop] == 10 || source[__loop] == 13)
                {
                    __result.Append(source[__loop]);
                }
            }
            return __result.ToString();
        }

        public static string _fieldAndComma(params string[] fieldName)
        {
            StringBuilder __result = new StringBuilder();
            foreach (string __fieldName in fieldName)
            {
                if (__result.Length > 0)
                {
                    __result.Append(", ");
                }
                __result.Append(__fieldName);
            }
            return __result.ToString();
        }

        public static WebProxy _webProxy()
        {
            WebProxy __proxy = new WebProxy("www.smlsoft.com:3128", true);
            __proxy.Credentials = new NetworkCredential("pim", "pim");
            return __proxy;
        }

        public static string _monthName(DateTime source, Boolean isLong)
        {
            try
            {
                return _resource((isLong) ? _monthList[source.Month - 1] : _monthListShort[source.Month - 1]);
            }
            catch
            {
                return "";
            }
        }

        public static string _monthName(int monthNumber, Boolean isLong)
        {
            try
            {
                return _resource((isLong) ? _monthList[monthNumber - 1] : _monthListShort[monthNumber - 1]);
            }
            catch
            {
                return "";
            }
        }

        public static string _resource(string code)
        {
            return _resource(code, _language);
        }

        public static string _errorText(string result)
        {
            string __result = "";
            if (result.IndexOf("duplicate key value violates unique constraint") != -1 && result.IndexOf("ic_trans_ic_trans_pk_primary") != -1)
            {
                __result = "เลขที่เอกสารซ้ำ \n";
            }
            else if (result.IndexOf("duplicate key value violates unique constraint") != -1 && result.IndexOf("gl_wht_list_gl_wht_list_doc_no") != -1)
            {
                __result = "เลขที่ เอกสารหัก ณ ที่จ่าย ซ้ำ \n";
            }
            else if (result.IndexOf("duplicate key value violates unique constraint") != -1)
            {
                __result = "รหัสซ้ำ \n";
            }
            __result += result;
            return __result;
        }

        public static string _resource(string code, _languageEnum language)
        {
            try
            {
                if (_isDesignMode == false)
                {
                    if (_resourceList.Count == 0)
                    {
                        //_resourceList.Add(new _languageClass("account_period", "งวดบัญชี", "account period", ""));
                        //_resourceList.Add(new _languageClass("add_line", "เพิ่ม", "Insert", ""));
                        //_resourceList.Add(new _languageClass("ampher", "อำเภอ", "District", ""));
                        //_resourceList.Add(new _languageClass("append", "เพิ่ม", "Append", "Append"));
                        //_resourceList.Add(new _languageClass("approval", "อนุมัติรายการ", "Approved", ""));
                        //_resourceList.Add(new _languageClass("auto_period_auto", "กำหนดงวดอัตโนมัติ", "period automatically", ""));
                        //_resourceList.Add(new _languageClass("auto_running", "เลขที่เอกสารอัตโนมัติ", "Auto running number", ""));
                        //_resourceList.Add(new _languageClass("bank", "ธนาคาร", "Bank", ""));
                        //_resourceList.Add(new _languageClass("branch_close", "ระบบสาขาไม่ได้เปิดใช้", "Branch is not open", ""));
                        //_resourceList.Add(new _languageClass("cancel", "ยกเลิก", "Cancel", "Cancel"));
                        //_resourceList.Add(new _languageClass("cancel_close", "ยกเลิกและปิดหน้าจอ", "Cancel and close the screen.", ""));
                        _resourceList.Add(new _languageClass("change_password", "เปลี่ยนรหัส", "Change Password", "Change Password", "", "", ""));
                        //_resourceList.Add(new _languageClass("clear_data_afer_save", "ล้างหน้าจอทุกครั้งหลังจากบันทึก", "Clear screen after Save data", ""));
                        _resourceList.Add(new _languageClass("code", "รหัส", "Code", "Code", "", "", ""));
                        //_resourceList.Add(new _languageClass("confirm", "ยืนยัน", "Confirm", ""));
                        //_resourceList.Add(new _languageClass("confirm_close", "ยืนยันและปิดหน้าจอ", "Confirm and close the screen.", ""));
                        _resourceList.Add(new _languageClass("connect", "เชื่อมต่อ", "Connect", "Connect", "", "", ""));
                        _resourceList.Add(new _languageClass("connect_server", "เชื่อมต่อกับเครื่องแม่ข่าย", "Connect to Server", "", "", "", ""));
                        _resourceList.Add(new _languageClass("create_database_1", "เพิ่มรายละเอียดที่เกี่ยวข้องเรียบร้อยแล้ว", "Process insert Information complete", "Process insert Information complete", "", "", ""));
                        _resourceList.Add(new _languageClass("create_database_2", "เลือกกลุ่มผู้ใช้ และผู้ใช้ ที่มีสิทธิ์ในการใช้ข้อมูล", "Select user group and user for access database", "Select user group and user for access database", "", "", ""));
                        _resourceList.Add(new _languageClass("create_database_3", "มีชื่อข้อมูลอยู่แล้วไม่สามารถสร้างใหม่ได้", "Duplicate database can't create new database", "Duplicate database can't create new database", "", "", ""));
                        _resourceList.Add(new _languageClass("create_database_4", "เลือกกลุ่มข้อมูล ก่อนสร้างฐานข้อมูล", "Select database group before create database", "Select database group before create database", "", "", ""));
                        _resourceList.Add(new _languageClass("create_database_5", "สามารถสร้างข้อมูลใหม่ได้\nDatabase Group : [{0}]\nDatabase Code : [{1}]\nDatabase Name :[{2}]\n\nกรุณายืนยันการสร้างด้วย", "", "", "", "", ""));
                        _resourceList.Add(new _languageClass("create_database_6", "มีรหัสข้อมูลอยู่แล้วไม่สามารถสร้างใหม่ได้", "Duplicate database code can't create new database", "Duplicate database code can't create new database", "", "", ""));//somruk;
                        _resourceList.Add(new _languageClass("create_database_process", "เริ่มสร้างฐานข้อมูลใหม่", "Start create new database", "Start create new database", "", "", ""));
                        //_resourceList.Add(new _languageClass("data_not_complete", "ข้อมูลไม่สมบูรณ์", "Data Not Complete", "Data Not Complete"));
                        _resourceList.Add(new _languageClass("database_group", "กลุ่มข้อมูล", "Database Group", "Database Group", "", "", ""));
                        //_resourceList.Add(new _languageClass("database_name", "ชื่อฐานข้อมูล", "Database Name", "Database Name"));
                        //_resourceList.Add(new _languageClass("default_load", "ดึงรูปแบบต้นฉบับ", "load Default", "load Default"));
                        _resourceList.Add(new _languageClass("delete_database_group", "ลบกลุ่มฐานข้อมูล", "Delete Database Group", "Delete Database Group", "", "", ""));
                        //_resourceList.Add(new _languageClass("delete_line", "ลบ", "Delete", ""));
                        _resourceList.Add(new _languageClass("delete_list", "ลบรายการที่เลือก", "Delete from List", "Delete from List", "", "", ""));
                        _resourceList.Add(new _languageClass("delete_success", "ลบข้อมูลสำเสร็จ", "Delete Record Complete", "Delete Record Complete", "", "", ""));
                        _resourceList.Add(new _languageClass("deselect_all", "ไม่เลือกทั้งหมด", "DeSelect All", "DeSelect All", "", "", ""));
                        _resourceList.Add(new _languageClass("detail", "รายละเอียด", "Detail", "Detail", "", "", ""));
                        _resourceList.Add(new _languageClass("doc_type_not_found", "ไม่พบประเภทเอกสาร", "Doc Type not found", "", "", "", ""));
                        _resourceList.Add(new _languageClass("data_not_complete", "ข้อมูลไม่สมบูรณ์", "Data Not Complete", "", "", "", ""));
                        //_resourceList.Add(new _languageClass("duplicate", "มีรายการซ้ำ", "Duplicate", "Duplicate"));
                        _resourceList.Add(new _languageClass("error", "ผิดพลาด", "Error", "Error", "", "", ""));
                        //_resourceList.Add(new _languageClass("fail", "การทำงานล้มเหลว", "Process Fail", "Records"));
                        //_resourceList.Add(new _languageClass("fail1", "การทำงานมีปัญหา", "Process Fail", "Records"));
                        //_resourceList.Add(new _languageClass("find", "ค้นหา", "Find", "Find"));
                        //_resourceList.Add(new _languageClass("first_date_period", "วันที่เริ่มต้นงวดแรก", "Day start of period", ""));
                        _resourceList.Add(new _languageClass("format_date_error", "ป้อนวันที่ผิดพลาด", "Date format error", "Date format error", "", ""));
                        //_resourceList.Add(new _languageClass("get_picture_file", "ดึงจากรูป", "Get Picture", ""));
                        //_resourceList.Add(new _languageClass("get_picture_full_screen", "แสดงภาพเต็มหน้า", "Full Screen", ""));
                        //_resourceList.Add(new _languageClass("get_picture_normal_size", "แสดงภาพเท่าขนาดจริง", "Actual size.", ""));
                        //_resourceList.Add(new _languageClass("get_picture_paste", "วาง", "Paste", ""));
                        //_resourceList.Add(new _languageClass("get_picture_resize", "เปลี่ยนขนาดรูป", "Picture Resize", ""));
                        //_resourceList.Add(new _languageClass("get_product", "ดึงรายการสินค้า", "Select Product", ""));
                        //_resourceList.Add(new _languageClass("information", "ข้อมูลเพิ่มเติม", "More information.", ""));
                        _resourceList.Add(new _languageClass("input_first", "ต้องบันทึกรหัส", "Input First", "Input First", "", ""));
                        //_resourceList.Add(new _languageClass("insert", "แทรก", "Insert", "Insert"));
                        _resourceList.Add(new _languageClass("is_group", "ให้เป็นกลุ่ม", "Group", "Group", "", ""));
                        _resourceList.Add(new _languageClass("is_user", "ให้เป็นผู้ใช้", "User", "User", "", ""));
                        _resourceList.Add(new _languageClass("level", "ระดับ", "Level", "Level", "", ""));
                        _resourceList.Add(new _languageClass("load_data", "ดึงข้อมูล", "Load", "Load", "", ""));
                        _resourceList.Add(new _languageClass("login", "เข้าสู่ระบบ", "Login", "Login", "", ""));
                        _resourceList.Add(new _languageClass("login_active", "เข้าใช้ระบบได้", "Active", "Active", "", ""));
                        //_resourceList.Add(new _languageClass("main_group", "กลุ่มหลัก", "Main Group", ""));
                        //_resourceList.Add(new _languageClass("main_group_select", "กรุณาเลือกกลุ่มหลัก", "Select Main Group", ""));
                        //_resourceList.Add(new _languageClass("main_product_group", "กลุ่มสินค้าหลัก", "Product Group", ""));
                        //_resourceList.Add(new _languageClass("main_product_group_select", "กรุณาเลือกกลุ่มสินค้าหลัก", "Select Product Main Group", ""));
                        //_resourceList.Add(new _languageClass("money_1", "เงินโอน", "Transfer Money", ""));
                        //_resourceList.Add(new _languageClass("money_2", "เงินสด", "Cash", ""));
                        _resourceList.Add(new _languageClass("name", "ชื่อ", "Name", "Name", "", ""));
                        //_resourceList.Add(new _languageClass("new_chq_in", "สร้างเช็ครับใบใหม่", "New", ""));
                        _resourceList.Add(new _languageClass("new_database_group", "สร้างกลุ่มฐานข้อมูลใหม่", "New Database Group", "New Database Group", "", ""));
                        _resourceList.Add(new _languageClass("new_group", "เพิ่มกลุ่มใหม่", "New Group", "New Group", "", ""));
                        _resourceList.Add(new _languageClass("new_user", "เพิ่มรหัสผู้ใช้", "New User", "New User", "", ""));
                        _resourceList.Add(new _languageClass("open_file", "เปิดแฟ้มข้อมูล", "Open File", "Open File", "", ""));
                        //_resourceList.Add(new _languageClass("order_by", "เรียงลำดับ", "Order By", ""));
                        _resourceList.Add(new _languageClass("password", "รหัสผ่าน", "Password", "Password", "", ""));
                        //_resourceList.Add(new _languageClass("pay_money", "การจ่ายเงิน", "Paying", ""));
                        //_resourceList.Add(new _languageClass("print", "พิมพ์", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_1", "พิมพ์เอกสาร", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_10", "พิมพ์เอกสารขายสินค้า(ลดหนี้)", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_11", "พิมพ์เอกสารซื้อสินค้าและค่าบริการ", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_12", "พิมพ์เอกสารส่งคืนสินค้าลดหนี้", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_13", "พิมพ์เอกสารรับสินค้าสำเร็จรูป", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_14", "พิมพ์เอกสารโอนสินค้าออก", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_15", "พิมพ์เอกสารโอนสินค้าออก", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_2", "พิมพ์เอกสารสั่งขาย", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_3", "พิมพ์เอกสารสั่งจองสั่งซื้อสินค้า", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_4", "พิมพ์เอกสารซื้อสินค้าเพิ่มหนี้", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_5", "พิมพ์เอกสารจ่ายเงินล่วงหน้า", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_6", "พิมพ์เอกสารเบิกสินค้าและวัตถุดิบ", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_7", "พิมพ์เอกสารรับคืนสินค้า", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_8", "พิมพ์เอกสารสั่งซื้อ", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_9", "พิมพ์เอกสารขายสินค้า(เพิ่มหนี้)", "Print", ""));
                        //_resourceList.Add(new _languageClass("print_preview", "พิมพ์ตัวอย่าง", "Print sample", ""));
                        _resourceList.Add(new _languageClass("process", "ประมวลผล", "process", "process", "", ""));
                        //_resourceList.Add(new _languageClass("process_condition", "ประมวลผลเงื่อนไข", "Process", ""));
                        //_resourceList.Add(new _languageClass("province", "จังหวัด", "Province", ""));
                        //_resourceList.Add(new _languageClass("records", "รายการ", "Records", "Records"));
                        _resourceList.Add(new _languageClass("refresh", "แสดงผลใหม่", "Refresh", "Refresh", "", ""));
                        //_resourceList.Add(new _languageClass("refresh_data", "เรียกข้อมูลใหม่", "Refresh", "Refresh"));
                        //_resourceList.Add(new _languageClass("report_print_1", "เริ่มพิมพ์", "Print", ""));
                        //_resourceList.Add(new _languageClass("report_print_2", "กำหนดหน้ากระดาษ", "Page Setup", ""));
                        //_resourceList.Add(new _languageClass("report_print_3", "ข้อเลือกพิเศษ", "Option", ""));
                        //_resourceList.Add(new _languageClass("report_print_4", "ตัวอย่างรายงาน", "Report Sample", ""));
                        //_resourceList.Add(new _languageClass("report_print_5", "เงื่อนไขการพิมพ์", "Condition", ""));
                        //_resourceList.Add(new _languageClass("result", "ผลการทำงาน", "Result", "Result"));
                        //_resourceList.Add(new _languageClass("retrive_now", "retrive_now", "retrive_now", ""));
                        //_resourceList.Add(new _languageClass("s1", "กรุณาบันทึกวันที่เริ่มต้นของงวดแรก และกำหนดจำนวนงวดว่าเป็น 12 หรือ 13 งวดด้วย", "Please note the start date of the first period. And the number of periods that are 12 or 13 periods with", ""));
                        //_resourceList.Add(new _languageClass("s2", "ประจำปี:", "Year", ""));
                        //_resourceList.Add(new _languageClass("s3", "วันที่เริ่มต้นงวดแรก", "Day for first period", ""));
                        //_resourceList.Add(new _languageClass("s4", "ประมวลผล", "Process", ""));
                        //_resourceList.Add(new _languageClass("s5", "กำหนดงวดบัญชี", "Specified period.", ""));
                        _resourceList.Add(new _languageClass("save", "บันทึก", "Save", "Save", "", ""));
                        _resourceList.Add(new _languageClass("save_and_close", "บันทึกพร้อมปิดหน้าจอ", "Save and Close", "Save and Close", "", ""));
                        _resourceList.Add(new _languageClass("save_f12", "บันทึก (F12)", "Save (F12)", "Save (F12)", "", ""));
                        _resourceList.Add(new _languageClass("save_success", "บันทึกข้อมูลสำเร็จ", "Save Record Complete", "Save Record Complete", "", ""));
                        //_resourceList.Add(new _languageClass("save_success", "บันทึกข้อมูลเสร็จเรียบร้อย", "Success", "Success"));
                        _resourceList.Add(new _languageClass("screen_close", "ปิดหน้าจอ", "Close", "Close", "", ""));
                        _resourceList.Add(new _languageClass("search", "ค้นหาข้อมูล", "Search", "Search", "", ""));
                        _resourceList.Add(new _languageClass("select_add", "ให้เพิ่มข้อมูลได้", "To add data.", "", "", ""));
                        _resourceList.Add(new _languageClass("select_all", "เลือกทั้งหมด", "Select All", "Select All", "", ""));
                        _resourceList.Add(new _languageClass("delete1", "หน้าจอนี้ไม่อนุญาติให้ลบข้อมูล", "Can't delete record", "Select All", "", ""));
                        _resourceList.Add(new _languageClass("delete2", "ห้ามแก้ไข หรือลบข้อมูล", "Can't delete record", "Select All", "", ""));
                        _resourceList.Add(new _languageClass("delete3", "Lock เด็ดขาด : ห้ามแก้ไข หรือลบข้อมูล", "Can't delete record", "Select All", "", ""));
                        //_resourceList.Add(new _languageClass("select_ampher", "กรุณาเลือกอำเภอ", "Please select District", ""));               
                        //_resourceList.Add(new _languageClass("select_bank", "กรุณาเลือกธนาคาร", "Select Bank", ""));
                        _resourceList.Add(new _languageClass("select_change", "ให้แก้ไขข้อมูลได้", "To update data.", "", "", ""));
                        _resourceList.Add(new _languageClass("select_data_group_1", "ยังไม่ได้เลือกกลุ่มข้อมูล", "Database's Group not Select", "Database's Group not Select", "", ""));
                        _resourceList.Add(new _languageClass("select_data_group_2", "ยังไม่ได้เลือกผู้มีสิทธิ์ใช้งาน", "User's Authorize not Select", "User's Authorize not Select", "", ""));
                        _resourceList.Add(new _languageClass("select_data_group_3", "กรุณากรอกรายละเอียดให้ครบถ้วนก่อน", "Please complete entry Information", "Please complete entry Information", "", ""));
                        _resourceList.Add(new _languageClass("select_date", "เลือกวันที่", "Select Date", "", "", ""));
                        _resourceList.Add(new _languageClass("select_delete", "ให้ลบข้อมูลได้", "To delete data.", "", "", ""));
                        //_resourceList.Add(new _languageClass("select_doc", "เลือกรายการ", "Select", ""));
                        //_resourceList.Add(new _languageClass("select_doc_1", "เลือกเอกสารรับวางบิล", "Select receipt invoice.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_10", "เลือกเอกสารเสนอราคา", "Select Document.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_11", "เลือกเอกสารซื้อสินค้า/ค่าบริการ", "Select Document.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_12", "รายการขออนุมัติซื้อ", "Select Document.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_13", "เลือกเอกสารอนุมัติเสนอซื้อ", "Select Document.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_14", "เลือกเอกสารสั่งซื้อสินค้า", "Select Document.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_2","บัญชีแยกประเภท","General Ledger",""));
                        //_resourceList.Add(new _languageClass("select_doc_3", "ภาษีหัก ณ. ที่จ่าย", "Withholding Tax", ""));
                        //_resourceList.Add(new _languageClass("select_doc_4", "ภาษีซื้อ", "Vat Purchase", ""));
                        //_resourceList.Add(new _languageClass("select_doc_5", "เลือกเอกสารขายสินค้า/ค่าบริการ", "Select Invoice", ""));
                        //__resourceList.Add(new _languageClass("select_doc_6", "รายการที่ขออนุมัติ", "Document Details.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_8", "เลือกเอกสารเสนอราคา/ใบสั่งซื้อ/ใบสั่งจอง/ใบสั่งขาย", "Select Document.", ""));
                        //_resourceList.Add(new _languageClass("select_doc_9", "เลือกเอกสารเสนอราคา/ใบสั่งซื้อ/ใบสั่งจอง", "Select Document.", ""));
                        //_resourceList.Add(new _languageClass("select_province", "กรุณาเลือกจังหวัด", "Select Province", ""));
                        _resourceList.Add(new _languageClass("select_read", "ให้เข้าใช้งานได้", "Access", "Access", "", ""));
                        //_resourceList.Add(new _languageClass("select_warehouse", "กรุณาเลือกคลังสินค้า", "Select Warehouse", ""));
                        _resourceList.Add(new _languageClass("server_password", "รหัสผ่าน Server", "Server Password", "", "", ""));
                        _resourceList.Add(new _languageClass("server_setup_1", "ฐานข้อมูลที่ใช้", "Database Provider", "", "", ""));
                        _resourceList.Add(new _languageClass("server_setup_2", "ชื่อ Server Webservice", "Webservice Server Name", "", "", ""));
                        _resourceList.Add(new _languageClass("server_setup_3", "ชื่อ Server ที่เก็บข้อมูล", "Database Server Name", "", "", ""));
                        _resourceList.Add(new _languageClass("server_setup_4", "รหัสผู้ใช้ ของ Database Server", "Database User Code", "", "", ""));
                        _resourceList.Add(new _languageClass("server_setup_5", "รหัสผ่าน ของ Database Server", "Database Password", "", "", ""));
                        _resourceList.Add(new _languageClass("set_condition", "กำหนดเงือนไข", "Condition", "", "", ""));
                        _resourceList.Add(new _languageClass("show_me_again", "แสดงข้อความนี้เสมอ", "Always show this message.", "", "", ""));
                        //_resourceList.Add(new _languageClass("start_shink", "เริ่มกระชับฐานข้อมูล", "Start shink Database", "Start shink Database"));
                        _resourceList.Add(new _languageClass("start_verify", "เริ่มตรวจสอบฐานข้อมูล", "Start Verify Database", "Start Verify Database", "", ""));
                        _resourceList.Add(new _languageClass("success", "ทำงานสำเร็จ", "Success", "Success", "", ""));
                        //_resourceList.Add(new _languageClass("success1", "การทำงานสำเร็จ", "Process Complete", "Records"));
                        //_resourceList.Add(new _languageClass("test", "ทดสอบ", "Test", "Test"));
                        _resourceList.Add(new _languageClass("text_for_search", "ข้อความค้นหา", "Search", "Search", "", ""));
                        _resourceList.Add(new _languageClass("update", "ปรับปรุง", "Update", "Update", "", ""));
                        //_resourceList.Add(new _languageClass("vat_type", "ประเภทภาษีมูลค่าเพิ่ม (ขาย)", "Vat Sale", ""));
                        //_resourceList.Add(new _languageClass("vat_type_1", "ประเภทภาษีมูลค่าเพิ่ม (ซื้อ)", "Vat Purchase", ""));
                        //_resourceList.Add(new _languageClass("view_1", "เงื่อนไข", "Condition", ""));
                        //_resourceList.Add(new _languageClass("view_2", "ประมวลผล", "Process", ""));
                        //_resourceList.Add(new _languageClass("view_3", "ข้อเลือกพิเศษ", "Option", ""));
                        //_resourceList.Add(new _languageClass("view_4", "เปิดโดย Excel", "Open by Excel", ""));
                        //_resourceList.Add(new _languageClass("view_5", "ขนาดกระดาษ", "Page Setup", ""));
                        //_resourceList.Add(new _languageClass("view_6", "แสดงก่อนพิมพ์", "Print Preview", ""));
                        //_resourceList.Add(new _languageClass("view_7", "ตัวอย่าง", "Example", ""));
                        _resourceList.Add(new _languageClass("view_by", "รูปแบบการแสดง", "View by", "View by", "", ""));
                        _resourceList.Add(new _languageClass("view_data", "แสดงข้อมูล", "Active", "Active", "", ""));
                        _resourceList.Add(new _languageClass("w1", "ต้องการ Lock รายการนี้ก่อนแก้ไขหรือไม่", "lock record before edit ?", "", "", ""));
                        _resourceList.Add(new _languageClass("w2", "ต้องการแสดงหน้าจอนี้อีกในครั้งต่อไป", "", "Show this warning again.", "", ""));
                        //_resourceList.Add(new _languageClass("w3", "ไม่มีสิทธิอนุมัติ", "Permission fail.", ""));
                        //_resourceList.Add(new _languageClass("w4", "แก้ไขราคา", "Edit price", ""));
                        //_resourceList.Add(new _languageClass("w5", "ราคาตามกลุ่มลูกค้า", "Price customers.", ""));
                        //_resourceList.Add(new _languageClass("w6", "", "", ""));
                        //_resourceList.Add(new _languageClass("w7", "", "", ""));
                        //_resourceList.Add(new _languageClass("warehouse", "คลังสินค้า", "Ware House", ""));
                        _resourceList.Add(new _languageClass("warning", "เตือน", "Warning", "Warning", "", ""));
                        _resourceList.Add(new _languageClass("warning1", "มีการแก้ไขข้อมูลบางส่วน\nต้องการยกเลิกการแก้ไขหรือไม่", "Informations is Change\n Cancel Change or not", "Informations is Change\n Cancel Change or not", "", ""));
                        _resourceList.Add(new _languageClass("warning10", "ไม่พบฐานข้อมูล กรุณากลับไปทำการสร้างก่อน", "Not found database list Plase return to create database", "Not found database list Plase return to create database", "", ""));
                        _resourceList.Add(new _languageClass("warning100", "กรุณากำหนด Webservice Server ก่อน (กำหนด Tab แรก)", "", "", "", ""));
                        _resourceList.Add(new _languageClass("warning101", "ทำงานไม่สำเร็จ", "Not work successfully.", "", "", ""));
                        //_resourceList.Add(new _languageClass("warning102", "ไม่พบ กลุ่มภาษี", "Not Found", ""));
                        //_resourceList.Add(new _languageClass("warning103", "ยังไม่ได้กำหนดงวดบัญชี\nแนะนำให้กำหนดก่อน", "We have defined period \n suggest that the first", ""));
                        //_resourceList.Add(new _languageClass("warning104", "บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้", "Save is complete. The program will close this screen.", ""));
                        //_resourceList.Add(new _languageClass("warning105", "ไม่พบ", "not found", ""));
                        //_resourceList.Add(new _languageClass("warning106", "ยอด Debit, Credit ไม่เท่ากัน\nไม่สามารถบันทึกรายการได้", "Top Debit, Credit does not equal \n Unable to save the item.", ""));
                        //_resourceList.Add(new _languageClass("warning107", "กรุณากรอกข้อมูล สมุดธนาคาร", "Fields book bank", ""));
                        //_resourceList.Add(new _languageClass("warning108", "กรุณาตรวจสอบ จำนวนเงินชำระเงินไม่ครบตามจำนวน \n\r ต้องการทำขั้นตอนต่อไป กด YES \n\r ต้องการกลับไปตรวจสอบใหม่ กด NO", "", ""));
                        //_resourceList.Add(new _languageClass("warning109", "กรุณาเลือกเงือนไขก่อน", "Select Criteria fever before", ""));
                        _resourceList.Add(new _languageClass("warning11", "กำหนดรหัสผ่านใหม่สำเร็จ", "Successfully reset your password.", "", "", ""));
                        //_resourceList.Add(new _languageClass("warning110", "ชำระเงินเกินจำนวน กรุณาตรวจสอบใหม่", "Payments exceeded. Please check back.", ""));
                        //_resourceList.Add(new _languageClass("warning111", "ลบ", "Delete", ""));
                        //_resourceList.Add(new _languageClass("warning112", "เลือกรูป", "Open", ""));
                        //_resourceList.Add(new _languageClass("warning113", "กล้อง", "Web Camera", ""));
                        //_resourceList.Add(new _languageClass("warning114", "สแกร์นภาพ", "Scanner", ""));
                        //_resourceList.Add(new _languageClass("warning115", "บัญชีนี้ไม่สามารถบันทึกรายการได้", "This account can not save the item.", ""));
                        //_resourceList.Add(new _languageClass("warning116", "ไม่พบ ผังบัญชี", "Account not found", ""));
                        //_resourceList.Add(new _languageClass("warning117", "ต้องการลบ [{0}] จริงหรือไม่", "Delete [{0}] real or not.", ""));
                        //_resourceList.Add(new _languageClass("warning118", "ยังไม่ได้เลือกรายการ", "Not selected.", ""));
                        //_resourceList.Add(new _languageClass("warning119", "รูปแแบบไม่ถูกต้อง\n  -รูปแบบต้องเป็น (9x9x99x99)  \n *** 9 แทนตัวเลขของ ความกว้าง ความยาว ความสูง หน่วย \n ต้องอยู่ในรูปแบบ  กว้างxยาวxสูงxหน่วย", "Formate  failed to appear \n  -Formate an auxiliary verb to (99x99x99xx99)  = width x lenght x height x unit", ""));
                        _resourceList.Add(new _languageClass("warning12", "ไม่สามารถกำหนดรหัสใหม่ได้ Server ไม่ยอมรับ", "Fail.", "", "", ""));
                        //_resourceList.Add(new _languageClass("warning120", "เอกสารเลขที่ หรือ ยอดตัดจ่าย ห้ามว่าง", "Document number or total pay cut not available.", ""));
                        //_resourceList.Add(new _languageClass("warning121", "ยังไม่ได้กำหนดเงื่อนไข", "We have defined conditions.", ""));
                        //_resourceList.Add(new _languageClass("warning122", "ต้องการเพิ่ม Menu นี้เข้าไปไว้ใน Menu ส่วนตัวหรือไม่", "To add this into Menu Menu or private.", ""));
                        //_resourceList.Add(new _languageClass("warning123", "วันที่เอกสาร ห้ามว่าง", "Do not place the document on.", ""));
                        _resourceList.Add(new _languageClass("warning124", "ไม่พบ : รหัส หรือ รหัสคลังไม่สัมพันธ์กับที่เก็บสินค้า", "Not found: code or warehouse code not related to inventory storage.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning125", "รหัสที่เลือกมีอยู่แล้ว", "Select an existing code.", "", "", ""));
                        //_resourceList.Add(new _languageClass("warning126", "ไม่สามารถเข้า เมนู", "Can not Acess Menu", ""));
                        //_resourceList.Add(new _languageClass("warning127", "เอกสารวันที่ เอกสารเลขที่ หรือ ยอดตัดจ่าย ห้ามว่าง", "", ""));
                        //_resourceList.Add(new _languageClass("warning128", "ยอดการจ่ายเงินไม่สมบูรณ์", "Balance of payment is not complete.", ""));
                        _resourceList.Add(new _languageClass("warning129", "โอนข้อมูล", "Transfer Data", "", "", ""));
                        _resourceList.Add(new _languageClass("warning13", "ไม่สามารถกำหนดรหัสใหม่ได้", "Can't change password", "", "", ""));
                        //_resourceList.Add(new _languageClass("warning130", "สินค้านี้ไม่สามารถแก้ไขราคาได้", "This product does not fix the price.", ""));
                        //_resourceList.Add(new _languageClass("warning131", "ไม่มีสิทธิแก้ไขราคา", "No right to modify prices.", ""));
                        _resourceList.Add(new _languageClass("warning14", "รหัสไม่ผ่าน", "Password fail.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning15", "กรุณาเลือกกลุ่มที่ต้องการก่อน", "Please Select Database Group", "", "", ""));
                        _resourceList.Add(new _languageClass("warning16", "ไม่พบกลุ่มผู้ใช้ กรุณากลับไปทำการสร้างกลุ่มผู้ใช้ก่อน", "Not found user group Plase return to create user group", "Not found user group Plase return to create user group", "", ""));
                        _resourceList.Add(new _languageClass("warning17", "ไม่พบกลุ่มข้อมูลที่ต้องการ กรุณาเลือกใหม่", "Not found Database Group Please Select", "", "", ""));
                        _resourceList.Add(new _languageClass("warning18", "ไม่พบฐานข้อมูลที่ต้องการ กรุณาเลือกใหม่", "Not found Database selection! Please select database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning19", "ท่านไม่มีสิทธิในการเพิ่มข้อมูลใหม่", "You have no right to add new content.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning2", "ท่านอยู่ในหัวข้อตรวจสอบข้อมูล\nถ้าต้องการแก้ไขให้กด Double Click ที่ข้อมูล", "Verify Record Status\n If you want to Edit Double Click at Record", "Verify Record Status\n If you want to Edit Double Click at Record", "", ""));
                        _resourceList.Add(new _languageClass("warning20", "Query ใช้ได้เฉพาะคำสั่ง select เท่านั้น", "Query only command select only", "", "", ""));
                        //_resourceList.Add(new _languageClass("warning21", "Query มีข้อผิดพลาด กรุณาตรวจสอบใหม่", "Query Error Check new", ""));
                        _resourceList.Add(new _languageClass("warning22", "มีการแก้ไขรายการ\nต้องการทำรายการต่อไปหรือไม่", "Some data is change \n Do you want to process", "", "", ""));
                        _resourceList.Add(new _languageClass("warning23", "ทำการเชื่อมต่อ", "Connect", "", "", ""));
                        _resourceList.Add(new _languageClass("warning24", "ท่านเลือกวันที่", "Select Date", "", "", ""));
                        _resourceList.Add(new _languageClass("warning25", "ยังไม่ได้เลือกข้อมูลที่ต้องการ Shrink กรุณาเลือกก่อน", "Please select Shrink database name", "", "", ""));
                        //_resourceList.Add(new _languageClass("warning26", "ท่านได้เลือกจำนวน : {0} ฐานข้อมูล\n\nต้องการทำการ Shrink หรือไม่", "Number of select database : {0} database\n\n confirm  Shrink database", ""));
                        _resourceList.Add(new _languageClass("warning27", "มีข้อผิดพลาดในการทำงาน\n\n{0}\n", "Error List\n\n{0}\n", "", "", ""));
                        _resourceList.Add(new _languageClass("warning28", "ทำการ Shrink database จำนวน : {0} ฐานข้อมูล\nมีรายการผิดพลาดจำนวน : {1} รายการ", "Total number of Shrink database : {0} database\n Number of shrink database error : {1} database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning29", "รายละเอียดไม่ครบ กรุณาตรวจสอบใหม่", "fill Information not complete Please check", "", "", ""));
                        _resourceList.Add(new _languageClass("warning3", "มีการแก้ไขปรับปรุง\n ต้องการออกจากหน้าจอนี้หรือไม่", "Informations is changed\n You want to quit?", "Informations is changed\n You want to quit?", "", ""));
                        _resourceList.Add(new _languageClass("warning30", "ยังไม่ได้เลือกข้อมูลที่ต้องการ Verify กรุณาเลือกก่อน", "Please select the verify database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning31", "ท่านได้เลือกจำนวน : {0} ฐานข้อมูล\n\nต้องการทำการ Verify หรือไม่", "Total number of select database : {0} database\n\n Confirm to verify database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning32", "ทำการ Verify database จำนวน : {0} ฐานข้อมูล\nมีรายการผิดพลาดจำนวน : {1} รายการ", "Total number of verify database : {0} database\n Number of error verify database : {0} database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning33", "มีการแก้ไขรายการ\nต้องการทำรายการต่อไปหรือไม่", "To amend the list \n want to continue or not.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning34", "กรุณาเลือกกลุ่มที่ต้องการก่อน", "Plase select group", "", "", ""));
                        _resourceList.Add(new _languageClass("warning35", "บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้", "Save complete program will close auto", "", "", ""));
                        _resourceList.Add(new _languageClass("warning36", "รายละเอียดไม่ครบ กรุณาตรวจสอบใหม่", "Some Information not complete Please Check", "", "", ""));
                        _resourceList.Add(new _languageClass("warning37", "ต้องการลบจริงหรือไม่", "Confirm to Delete ?", "", "", ""));
                        _resourceList.Add(new _languageClass("warning38", "กรุณาเลือกรายการที่ต้องการลบ", "Please Select Record to Delete", "", "", ""));
                        _resourceList.Add(new _languageClass("warning39", "บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้", "Save complete! Program will close auto", "", "", ""));
                        _resourceList.Add(new _languageClass("warning4", "รหัสกลุ่มซ้ำ กรุณาตรวจสอบใหม่", "Dulplicate Groups Please Check!", "Dulplicate Groups Please Check!", "", ""));
                        _resourceList.Add(new _languageClass("warning40", "กรุณาเลือกกลุ่มที่ต้องการก่อน", "Plase select group", "", "", ""));
                        _resourceList.Add(new _languageClass("warning41", "ต้องการลบจริงหรือไม่", "confirm to delete", "", "", ""));
                        _resourceList.Add(new _languageClass("warning42", "กรุณาเลือกรายการที่ต้องการลบ", "Please select record to delete", "", "", ""));
                        _resourceList.Add(new _languageClass("warning43", "บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้", "Save record complete application will close automatic", "", "", ""));
                        _resourceList.Add(new _languageClass("warning44", "ท่านไม่มีสิทธิในการเพิ่มข้อมูลใหม่", "You have no right to add new content.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning45", "การเชื่อมต่อสมบูรณ์", "Connect database success", "", "", ""));
                        _resourceList.Add(new _languageClass("warning46", "Web service ติดต่อโปรแกรมฐานข้อมูล สำเร็จ\nDatabase Server ชื่อ : {0} \nได้ทำการลงทะเบียนแล้ว\nโปรแกรมจะดำเนินการขั้นตอนต่อไปโดยอัตโนมัติ", "Web service connect success.\nDatabase Server name : {0}\nis register.\nand will next stop auto.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning47", "การเชื่อมต่อไม่สำเร็จ", "Connect database fail.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning48", "ไม่สามารถติดต่อโปรแกรมฐานข้อมูล\nกรุณาตรวจสอบใหม่\nกรุณาตรวจสอบชื่อของ Database Server, รหัสผู้ใช้ และรหัสผ่านใหม่", "Connect database fail.\nPlease check again.\nCheck Database Server name, User code or Password again.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning49", "การทำงานสำเร็จ", "Success", "", "", ""));
                        _resourceList.Add(new _languageClass("warning5", "ไม่พบรายการ กรุณาตรวจสอบใหม่", "Data not found. Please Check!", "Data not found. Please Check!", "", ""));
                        _resourceList.Add(new _languageClass("warning50", "การทำงานมีปัญหาจำนวน {0} รายการ", "Error {0} Records", "", "", ""));
                        _resourceList.Add(new _languageClass("warning51", "เนื่องจากท่านเป็นผู้ใช้คนแรก ระบบจะเพิ่มชื่อ และรหัสผ่านให้โดยอัตโนมัติ\nโดยท่านจะมีสิทธิ์ในการทำงานสูงสุด กรุณาจดรหัสผู้ใช้ และรหัสผ่านไว้ด้วย\n\nรหัสผู้ใช้คือ : {0}\nรหัสผ่านคือ : {1}", "This user is first login application will add this user in to system automatic \n This user is Administrator plase keep your user and password\n\n user : {0}\nPassword : {1}", "", "", ""));
                        _resourceList.Add(new _languageClass("warning52", "รายการนี้มีผู้ใช้อยู่", "This record locked.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning53", "ห้ามแก้ไข หรือลบข้อมูล", "Do not modify or delete data.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning54", "หน้าจอนี้ไม่อนุญาติให้แก้ไขข้อมูล", "This screen is not allowed to edit.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning55", "ท่านไม่มีสิทธิ์ แก้ไขข้อมูล", "You are not allowed to edit.", "", "", ""));
                        _resourceList.Add(new _languageClass("warning56", "ยังไม่บันทึก : Webservice Name กรุณากลับไปบันทึกก่อน", "Please input : Webservice Name", "", "", ""));
                        _resourceList.Add(new _languageClass("warning57", "ยังไม่บันทึก : {0}", "Please input : {0}", "", "", ""));
                        _resourceList.Add(new _languageClass("warning58", "เลือกข้อมูล", "Select Database", "", "", "", "ເລືອກ"));
                        _resourceList.Add(new _languageClass("warning59", "รหัสผู้ให้บริการ", "Provider Code", "", "", ""));
                        _resourceList.Add(new _languageClass("warning6", "ต้องการจบโปรแกรมจริงหรือไม่", "Close Program ?", "Close Program ?", "", ""));
                        _resourceList.Add(new _languageClass("warning60", "กลุ่มข้อมูล", "Database Group", "", "", ""));
                        _resourceList.Add(new _languageClass("warning61", "รหัสผู้ใช้", "User Code", "", "", ""));
                        _resourceList.Add(new _languageClass("warning62", "รหัสผ่าน", "Password", "", "", ""));
                        _resourceList.Add(new _languageClass("warning63", "เปลี่ยนรหัสผ่าน", "Change Password", "", "", ""));
                        _resourceList.Add(new _languageClass("warning64", "กำหนดการเชื่อมฐานข้อมูล", "Database Connection", "", "", ""));
                        _resourceList.Add(new _languageClass("warning65", "ในกรณียังไม่กำหนด หรือมีการเปลี่ยนรหัสผ่านของฐานข้อมูล", "Database Connection Setup", "", "", ""));
                        _resourceList.Add(new _languageClass("warning66", "กำหนดผู้ใช้ และกลุ่มผู้ใช้", "User ID, User Group Setup", "", "", ""));
                        _resourceList.Add(new _languageClass("warning67", "กำหนดผู้ใช้ และกลุ่มผู้ใช้ เพื่อจะกำหนดสิทธิ์ในการใช้งานต่อไป", "User id and User group Setup", "", "", ""));
                        _resourceList.Add(new _languageClass("warning68", "จัดกลุ่มผู้ใช้", "User Group Management", "", "", ""));
                        _resourceList.Add(new _languageClass("warning69", "กำหนดว่ากลุ่มแต่ละกลุ่ม มีผู้ใช้รหัสอะไรบ้าง เพื่อกำหนดสิทธิ์ในการใช้งานต่อไป", "User Group Management", "", "", ""));
                        _resourceList.Add(new _languageClass("warning7", "ท่านได้เลือกกลุ่ม [{0}] และโปรแกรมจะสร้างข้อมูลในกลุ่มนี้ให้", "Your's select database group is [{0}] and new database will create in this group ?", "Your's select database group is [{0}] and new database will create in this group ?", "", ""));
                        _resourceList.Add(new _languageClass("warning70", "กำหนดกลุ่มข้อมูล", "Database Group Management", "", "", ""));
                        _resourceList.Add(new _languageClass("warning71", "กำหนดว่ามีกลุ่มข้อมูลอะไรบ้าง เพื่อจัดกลุ่มข้อมูลให้เหมาะสมกับผู้ใช้", "Database Group Management", "", "", ""));
                        _resourceList.Add(new _languageClass("warning72", "สร้างฐานข้อมูลใหม่", "Create New Database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning73", "เชื่อมฐานข้อมูล", "Database Connection", "", "", ""));
                        _resourceList.Add(new _languageClass("warning74", "เชื่อมฐานข้อมูล เพื่อให้ผู้ใช้มองเห็นข้อมูล", "Database Connection", "", "", ""));
                        _resourceList.Add(new _languageClass("warning75", "ตรวจสอบฐานข้อมูล", "Verify Database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning76", "ตรวจสอบฐานข้อมูล เพิ่มปรับปรุงโครงสร้างข้อมูล", "", "", "", ""));
                        _resourceList.Add(new _languageClass("warning77", "ลดขนาดฐานข้อมูล", "Shrink Database", "", "", ""));
                        _resourceList.Add(new _languageClass("warning78", "ลดขนาดฐานข้อมูล เพิ่มประหยัดพื้นที่ และเพิ่มความเร็ว", "", "", "", ""));
                        _resourceList.Add(new _languageClass("warning79", "เปลี่ยนข้อความในระบบ", "Resource Edit", "", "", ""));
                        _resourceList.Add(new _languageClass("warning8", "เปลี่ยนรหัสผ่านใหม่สำเร็จ", "Change password sucess", "Change password sucess", "", ""));
                        _resourceList.Add(new _languageClass("warning80", "เปลี่ยนข้อความในระบบสำหรับเมนู, หน้าจอ, รายงาน", "Menu,Screen and Report resource edit", "", "", ""));
                        _resourceList.Add(new _languageClass("warning81", "เปลี่ยนรหัสผ่าน", "Change Password", "", "", ""));
                        _resourceList.Add(new _languageClass("warning82", "กำหนดการเข้าถึงข้อมูล", "Database Access", "", "", ""));
                        _resourceList.Add(new _languageClass("warning83", "กำหนดสิทธิกลุ่มใช้งาน", "Group Permissions", "", "", ""));
                        _resourceList.Add(new _languageClass("warning84", "กำหนดผู้มีสิทธิเข้าใช้ข้อมูล", "User Permissions", "", "", ""));
                        _resourceList.Add(new _languageClass("warning85", "ท่านกำลังทำการเพิ่มหรือแก้ไขข้อมูลไม่อนุญาติให้ลบ", "In add and edit mode can not delete", "", "", ""));
                        _resourceList.Add(new _languageClass("warning86", "กรุณาเลือกรายการที่ต้องการลบก่อน", "Please select record to delete", "", "", ""));
                        _resourceList.Add(new _languageClass("warning87", "มีข้อมูลที่ต้องลบจำนวน: {0} รายการ\nต้องการลบจริงหรือไม่", "Total number of delete records : {0} records\n Confirm to delete", "", "", ""));
                        _resourceList.Add(new _languageClass("warning88", "บันทึกข้อมูลเสร็จเรียบร้อย \nจำนวนรายการที่บันทึก : {0}\nจำนวนรายการที่รายละเอียดไม่ครบถ้วน : {1}\n\nโปรแกรมจะปิดหน้าจอนี้ให้", "", "", "", ""));
                        _resourceList.Add(new _languageClass("warning89", "ไม่พบกลุ่มข้อมูลที่ต้องการ กรุณาเลือกกลุ่มใหม่\nโปรแกรมไม่สามารถแสดงกลุ่มได้ ด้วยเหตุผลด้านความปลอดภัย\nโปรแกรมจะไม่ทำงานในขั้นตอนต่อไปจนกว่าจะระบุกลุ่มข้อมูลถูกต้อง", "Please select database group\n in case of security database group is hidden \n can not proceed until database group is correct", "", "", ""));
                        _resourceList.Add(new _languageClass("warning9", "เปลี่ยนรหัสผ่านใหม่ไม่สำเร็จ", "Change password fail", "Change password fail", "", ""));
                        _resourceList.Add(new _languageClass("warning90", "รหัสผู้ใช้ไม่ถูกต้อง หรือรหัสผ่านไม่ถูกต้อง กรุณาลองอีกครั้ง", "user password is not correct please try again", "", "", ""));
                        _resourceList.Add(new _languageClass("warning91", "ต้องการเลือกหรือไม่", "Confirm to select", "", "", ""));
                        _resourceList.Add(new _languageClass("warning92", "ท่านไม่ใช่กลุ่ม Admin ไม่อนุญาติให้ Login ในหน้าจอ Admin", "You are not Administrator group, can not login in Administrator mode", "", "", ""));
                        _resourceList.Add(new _languageClass("warning93", "ยินดีต้อนรับ [{0}] เข้าสู่ระบบ\nระดับของท่านคือ : {1}\nท่านสามารถทำรายการต่อไปได้", "Welcome [{0}] login \n Your authority level is : {1}\n You can Proceed", "", "", ""));
                        _resourceList.Add(new _languageClass("warning94", "ยินดีต้อนรับ", "Welcome", "", "", ""));
                        _resourceList.Add(new _languageClass("warning95", "ไม่สามารถติดต่อกับ Webservice ได้\nกรุณาบันทึกชื่อ Server อีกครั้ง แล้วลองใหม่\n", "Webservice connection fail.\nPlease enter new server name and try again\n", "", "", ""));
                        _resourceList.Add(new _languageClass("warning96", "ไม่สามารถติดต่อ Webservice ได้ : \n", "Webservice connection fail : \n", "", "", ""));
                        _resourceList.Add(new _languageClass("warning97", "กรุณาเลือกบริษัทที่ต้องการใช้งาน", "Please select company code", "", "", ""));
                        _resourceList.Add(new _languageClass("warning98", "ท่านยังไม่ได้ทำการ Login ไม่สามารถทำรายการได้", "Please login first! can not proceed", "", "", ""));
                        _resourceList.Add(new _languageClass("warning99", "ท่านไม่ใช่กลุ่ม Admin ไม่อนุญาติให้ กระทำการได ๆ ๆ ในหน้าจอ Admin", "You are not Administrator group, can not Works in Administrator mode", "", "", ""));
                        //_resourceList.Add(new _languageClass("web_cam", "กล้อง", "Camera", ""));
                        _resourceList.Add(new _languageClass("webservice_url", "Web Service URL", "Web Service URL", "", "", ""));
                        //_resourceList.Add(new _languageClass("year_type", "รูปแบบปี", "Year Format", ""));
                        //_resourceList.Add(new _languageClass("year1", "ประจำปี", "Year", ""));
                        _resourceList.Add(new _languageClass("yes", "ตกลง", "Yes", "Yes", "", ""));
                        //
                        _resourceList.Sort(delegate (_languageClass __resource1, _languageClass __resource2) { return __resource1._code.CompareTo(__resource2._code); });
                    }
                    _languageClass __find = _resourceList.Find(delegate (_languageClass __resource) { return __resource._code == code.ToLower(); });
                    if (__find != null)
                    {
                        string __result = "";
                        switch (language)
                        {
                            case _languageEnum.Thai: __result = __find._thai; break;
                            case _languageEnum.English: __result = __find._english; break;
                            case _languageEnum.Malayu: __result = __find._malayu; break;
                            case _languageEnum.Lao: __result = (__find._lao.Trim().Length == 0) ? __find._english : __find._lao; break;
                        }
                        __find = null;
                        return __result;
                    }
                    else
                    {
                        // ถ้าไม่เจอ ค้นหาใน Resource ระดับต่อไป โดยใช้คำ
                        _languageClass __findFromXml = _resourceFromXml.Find(delegate (_languageClass __resource) { return __resource._thai == code; });
                        if (__findFromXml != null)
                        {
                            string __result = "";
                            switch (language)
                            {
                                case _languageEnum.Thai: __result = __findFromXml._thai; break;
                                case _languageEnum.English: __result = __findFromXml._english; break;
                                case _languageEnum.Malayu: __result = __findFromXml._malayu; break;
                                case _languageEnum.Lao: __result = (__findFromXml._lao.Trim().Length == 0) ? __findFromXml._english : __findFromXml._lao; break;
                            }
                            __findFromXml = null;
                            return __result;
                        }
                        else
                        {

                            // toe
                            // ถ้าหาไม่เจอ ไป find resource ต่อ
                            if (_connectMySqlForResource)
                            {
                                // เพิ่มใน SQL ที่ server เพื่อแปลต่อไป
                                try
                                {
                                    MyLib._myFrameWork __myFrameWork = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, _databaseType.PostgreSql);

                                    if (__myFrameWork._testConnect() == true)
                                    {
                                        __myFrameWork._queryInsertOrUpdate("sml_language", "insert into sml_language(thai_lang) values (\'" + code + "\')");

                                    }
                                    else
                                    {
                                        _connectMySqlForResource = false;
                                    }
                                }
                                catch
                                {
                                    _connectMySqlForResource = false;
                                }
                            }
                        }

                        /*else
                        {
                            if (_connectMySqlForResource)
                            {
                             // เพิ่มใน SQL ที่ server เพื่อแปลต่อไป
                                try
                                {
                                    string __myConString = "Server=www.smlsoft.com; UserId=root; Password=19682511; Database=sml_language;charset=utf8;";
                                    MySqlConnection __conn = new MySqlConnection(__myConString);
                                    __conn.Open();
                                    try
                                    {
                                        MySqlCommand __commandReader = new MySqlCommand("select * from language_list where thai_lang=\'" + code + "\'", __conn);
                                        MySqlDataReader __reader = __commandReader.ExecuteReader();
                                        if (!__reader.Read())
                                        {
                                            __reader.Close();
                                            MySqlCommand __command = new MySqlCommand("insert into language_list (thai_lang) values (\'" + code + "\')", __conn);
                                            __command.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            __reader.Close();
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    __conn.Close();
                                }
                                catch
                                {
                                    _connectMySqlForResource = false;
                                }
                            }
                        }*/
                    }
                }
            }
            catch
            {
            }
            return code;
        }

        public static string _databaseConfig
        {
            set
            {
                _databaseConfigPrivate = value;
            }
            get
            {
                try
                {
                    string[] __split = _databaseConfigPrivate.Split('.');
                    return __split[0].ToString() + _providerCode + "." + __split[1].ToString();
                }
                catch
                {
                    return "";
                }
            }
        }

        // public static _databaseType _databaseSelectType = _databaseType.PostgreSql;

        public enum _databaseType
        {
            PostgreSql,
            MySql,
            MicrosoftSQL2000,
            MicrosoftSQL2005,
            Oracle,
            Firebird
        }
        /// <summary>
        /// วัตถุประสร้างสีพื้นให้แตกต่างระหว่างโปรแกรม ต่าง ๆ ๆ
        /// CREATE By MOO 
        /// </summary>
        public enum _versionType
        {
            Null,
            SMLPOS,
            SMLPOSLite,
            SMLPOSStarter,
            SMLAccountProfessional,
            SMLAccountPOSProfessional,
            SMLGL,
            SMLHP,
            SMLPP,
            SMLAccount,
            SMLColorStore,
            SMLThai7,
            SMLPayroll,
            SMLServiceCenter,
            SMLHealthy,
            SMLPOSMED,
            SMLTomYumGoong,
            SMLTomYumGoongPro,
            ICCManage,
            SMLPickAndPack,
            SMLIMS,
            IMSPOS,
            IMSAccountPro,
            SMLActiveSync,
            SMLAccountPOS,
            SMLBIllFree,
            SMLGeneralLedger

        }

        public static String _formatNumberForReport(int point, object value)
        {
            string __format = _getFormatNumber("m0" + point.ToString());
            string __result = "";
            try
            {
                Decimal __value = 0M;
                try
                {
                    if (value.GetType() == typeof(decimal))
                        __value = (decimal)value;
                    else
                        __value = _decimalPhase(value.ToString());
                }
                catch
                {
                }
                if (__value != 0)
                {
                    __result = string.Format(__format, __value);
                }
            }
            catch
            {
            }
            return __result;
        }

        public static String _formatNumberForReport(int point, DataRow[] data, int rowNumber, string columnName)
        {
            string __format = _getFormatNumber("m0" + point.ToString());
            string __result = "";
            try
            {
                Decimal __value = (data[rowNumber][columnName] == null) ? 0M : _decimalPhase(data[rowNumber][columnName].ToString());
                if (__value != 0)
                {
                    __result = string.Format(__format, double.Parse(data[rowNumber][columnName].ToString()));
                }
            }
            catch
            {
            }
            return __result;
        }



        public static decimal _round(decimal value, int digits)
        {

            /*double __y = value - Math.Floor(value);
            double __pow = Math.Pow(10, digits);
            double __result = Math.Floor(value) + (Math.Round(__y * __pow) / __pow);
            return __result;*/
            //return Math.Round(value + 0.000001M, digits, MidpointRounding.AwayFromZero);
            //return Math.Round(value+0.000001M, digits); 

            // toe
            if (_round_Type == 1)
            {
                decimal __testValue = Math.Round(value + 0.000001M, digits, MidpointRounding.AwayFromZero);
                return __testValue;
            }
            else
            {
                string __result = value.ToString();
                string[] __strSplit = __result.Split('.');

                if (__strSplit.Length > 1)
                {
                    int _numDigit = __strSplit[1].Length;
                    if (_numDigit > digits)
                    {
                        for (int __i = _numDigit - 1; __i >= digits; __i--) //ปัดลงมาทีละหลัก
                        {
                            value = Math.Round(value, __i, MidpointRounding.AwayFromZero);
                        }
                    }
                }
            }

            return value;
        }

        public static decimal _ceiling(decimal value, int digits)
        {
            decimal __pow = (decimal)Math.Pow(10, digits);
            decimal __result = (__pow == 0) ? value : Math.Ceiling(value * __pow) / __pow;
            return __result;
        }

        public static _PermissionsType _loadmenuPermissions_group(string mainMenuId, string menuid)
        {
            MemoryStream __stream = new MemoryStream();
            _PermissionsType __result = new _PermissionsType();
            __result._isAdd = false;
            __result._isDelete = false;
            __result._isEdit = false;
            __result._isRead = false;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __qurey = "select roworder as xcount from " + MyLib._d.sml_permissions_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_group._usercode) + " in (select " + MyLib._d.sml_user_and_group._group_code + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')";
            DataTable __dt = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __qurey).Tables[0];
            try
            {
                for (int __row = 0; __row < __dt.Rows.Count; __row++)
                {
                    try
                    {
                        // "select " + MyLib._d.sml_permissions_group._image_file + " from " + MyLib._d.sml_permissions_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_group._usercode) + " =" + MyLib._myGlobal._addUpper("\'" + MyLib._myGlobal._dataGroup + "\'");
                        string __roworder = __dt.Rows[__row][0].ToString();
                        byte[] __databyte = new byte[1024];
                        __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._mainDatabase, "select " + MyLib._d.sml_permissions_group._image_file + " from " + MyLib._d.sml_permissions_group._table + " where roworder=" + __roworder);
                        __stream = new MemoryStream(__databyte);
                        XmlSerializer __serializer = new XmlSerializer(typeof(_mainMenuClass));
                        _mainMenuClass __loadMenufromdatabase = (_mainMenuClass)__serializer.Deserialize(__stream);
                        for (int __loop = 0; __loop < __loadMenufromdatabase._MainMenuList.Count; __loop++)
                        {
                            MyLib._menuListClass __sub = (MyLib._menuListClass)__loadMenufromdatabase._MainMenuList[__loop];
                            string __mainmenu = __sub._menuMainname;
                            for (int __groupLoop = 0; __groupLoop < __sub._munsubList.Count; __groupLoop++)
                            {
                                string __menuCode = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._submeid.ToString();
                                if (menuid.ToLower().Equals(__menuCode.ToLower()))
                                {
                                    bool __isRead = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isRead;
                                    bool __isAdd = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isAdd;
                                    bool __isDelete = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isDelete;
                                    bool __isEdit = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isEdit;

                                    __result._isAdd = __result._isAdd || __isAdd;
                                    __result._isDelete = __result._isDelete || __isDelete;
                                    __result._isEdit = __result._isEdit || __isEdit;
                                    __result._isRead = __result._isRead || __isRead;
                                    break;
                                }
                            }

                        }
                    }
                    catch
                    {

                    }
                    __stream.Close();
                    __stream = null;
                }
            }
            catch
            {
            }
            __dt.Dispose();
            __dt = null;
            return __result;
        }

        public static _PermissionsType _loadmenuPermissions_user(string mainMenuId, string menuid)
        {
            MemoryStream __stream = new MemoryStream();
            _PermissionsType __result = new _PermissionsType();
            __result._isAdd = false;
            __result._isDelete = false;
            __result._isEdit = false;
            __result._isRead = false;
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            string __qurey = "select  count(*) as xcount from " + MyLib._d.sml_permissions_user._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_user._usercode) + " =" + MyLib._myGlobal._addUpper("\'" + MyLib._myGlobal._userCode + "\'");
            DataSet __ds = _myFrameWork._query(MyLib._myGlobal._mainDatabase, __qurey);
            byte[] __databyte = new byte[1024];
            try
            {
                if ((int)MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][0].ToString()) > 0)
                {
                    try
                    {
                        //"select " + MyLib._d.sml_permissions_user._image_file + " from " + MyLib._d.sml_permissions_user._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_user._usercode) + " =" + MyLib._myGlobal._addUpper("\'" + MyLib._myGlobal._userCode + "\'");
                        __databyte = _myFrameWork._ImageByte(MyLib._myGlobal._mainDatabase, "select " + MyLib._d.sml_permissions_user._image_file + " from " + MyLib._d.sml_permissions_user._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_user._usercode) + " =" + MyLib._myGlobal._addUpper("\'" + MyLib._myGlobal._userCode + "\'"));
                        __stream = new MemoryStream(__databyte);
                        XmlSerializer __serializer = new XmlSerializer(typeof(_mainMenuClass));
                        _mainMenuClass __loadMenufromdatabase = (_mainMenuClass)__serializer.Deserialize(__stream);
                        for (int __loop = 0; __loop < __loadMenufromdatabase._MainMenuList.Count; __loop++)
                        {
                            MyLib._menuListClass __sub = (MyLib._menuListClass)__loadMenufromdatabase._MainMenuList[__loop];
                            string __mainmenu = __sub._menuMainname;
                            for (int __groupLoop = 0; __groupLoop < __sub._munsubList.Count; __groupLoop++)
                            {
                                string __menuCode = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._submeid.ToString();
                                //if (MyLib._myGlobal._mainMenuCode.ToLower().Equals(__menuCode.ToLower()))
                                if (menuid.ToLower().Equals(__menuCode.ToLower()))
                                {
                                    bool __isRead = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isRead;
                                    bool __isAdd = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isAdd;
                                    bool __isDelete = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isDelete;
                                    bool __isEdit = ((MyLib._submenuListClass)__sub._munsubList[__groupLoop])._isEdit;

                                    __result._isAdd = __isAdd;
                                    __result._isDelete = __isDelete;
                                    __result._isEdit = __isEdit;
                                    __result._isRead = __isRead;
                                    break;
                                }
                            }

                        }
                    }
                    catch
                    {

                    }
                    __stream.Close();
                    __stream = null;
                }
            }
            catch
            {

            }
            __ds.Dispose();
            __ds = null;
            return __result;
        }

        public static _mainMenuClass _getUserAccessMenuPermissionAll()
        {
            try
            {
                MemoryStream __stream = new MemoryStream();
                byte[] __databyte = new byte[1024];

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._mainDatabase, "select " + MyLib._d.sml_permissions_user._image_file + " from " + MyLib._d.sml_permissions_user._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_user._usercode) + " =" + MyLib._myGlobal._addUpper("\'" + MyLib._myGlobal._userCode + "\'"));
                __stream = new MemoryStream(__databyte);
                XmlSerializer __serializer = new XmlSerializer(typeof(_mainMenuClass));
                _mainMenuClass __loadMenufromdatabase = (_mainMenuClass)__serializer.Deserialize(__stream);

                return __loadMenufromdatabase;
            }
            catch
            {

            }


            return null;
        }

        public static _mainMenuClass _getGroupAccessMenuPermissionAll()
        {
            _mainMenuClass __loadMenufromdatabase = null;
            try
            {
                MemoryStream __stream = new MemoryStream();
                byte[] __databyte = new byte[1024];

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                string __qurey = "select roworder as xcount from " + MyLib._d.sml_permissions_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_permissions_group._usercode) + " in (select " + MyLib._d.sml_user_and_group._group_code + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')";
                DataTable __dt = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __qurey).Tables[0];

                for (int __row = 0; __row < __dt.Rows.Count; __row++)
                {
                    string __roworder = __dt.Rows[__row][0].ToString();
                    __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._mainDatabase, "select " + MyLib._d.sml_permissions_group._image_file + " from " + MyLib._d.sml_permissions_group._table + " where roworder=" + __roworder);
                    __stream = new MemoryStream(__databyte);
                    XmlSerializer __serializer = new XmlSerializer(typeof(_mainMenuClass));
                    _mainMenuClass __loadMenufromdatabase2 = (_mainMenuClass)__serializer.Deserialize(__stream);


                    if (__row != 0)
                    {
                        // compare or


                    }
                    else
                    {
                        __loadMenufromdatabase = __loadMenufromdatabase2;
                    }
                }

                return __loadMenufromdatabase;
            }
            catch
            {

            }


            return null;
        }

        public static _PermissionsType _isAccessMenuPermision(string mainMenuId, string mainMenuCode)
        {
            //if (MyLib._myGlobal._userLevel > 2 || MyLib._myGlobal._nonPermission == true) return true;
            //if (MyLib._myGlobal._userLevel > 2 ) return true;
            // MyLib._myGlobal._mainMenuCode = MyLib._myGlobal._mainMenuCode;
            _PermissionsType __permisstion = new _PermissionsType();
            if (MyLib._myGlobal._userCode.ToLower().Equals("superadmin") || MyLib._myGlobal._nonPermission || mainMenuId.Equals("sml_soft_home_screen_menu_fast_report"))
            {
                __permisstion._isAdd = true;
                __permisstion._isDelete = true;
                __permisstion._isEdit = true;
                __permisstion._isRead = true;
                return __permisstion;
            }
            //
            __permisstion._isAdd = false;
            __permisstion._isDelete = false;
            __permisstion._isEdit = false;
            __permisstion._isRead = false;
            _PermissionsType __permisstion_group = new _PermissionsType();
            _PermissionsType __permisstion_user = new _PermissionsType();
            if (mainMenuCode.Length > 0)
            {
                __permisstion_group = _loadmenuPermissions_group(mainMenuId, mainMenuCode);

                __permisstion._isAdd = __permisstion_group._isAdd;
                __permisstion._isDelete = __permisstion_group._isDelete;
                __permisstion._isEdit = __permisstion_group._isEdit;
                __permisstion._isRead = __permisstion_group._isRead;

                __permisstion_user = _loadmenuPermissions_user(mainMenuId, mainMenuCode);

                __permisstion._isAdd = (__permisstion._isAdd) ? true : __permisstion_user._isAdd;
                __permisstion._isDelete = (__permisstion._isDelete) ? true : __permisstion_user._isDelete;
                __permisstion._isEdit = (__permisstion._isEdit) ? true : __permisstion_user._isEdit;
                __permisstion._isRead = (__permisstion._isRead) ? true : __permisstion_user._isRead;
            }
            return __permisstion;
        }

        public static _calcDiscountResultStruct _calcDiscountOnly(decimal price, string discountWord, decimal amount, int point)
        {
            _calcDiscountResultStruct __result = new _calcDiscountResultStruct();
            decimal __oldAmount = amount;
            if (discountWord.Trim().Length == 0)
            {
                __result._newPrice = price;
                __result._discountAmount = 0M;
                return __result;
            }
            string[] __words = discountWord.Replace(" ", "").Replace(" ", "").Replace(" ", "").Replace(" ", "").Split(',');
            for (int __loop = 0; __loop < __words.Length; __loop++)
            {
                string __getValue = __words[__loop].ToString();
                if (__getValue.Length > 0)
                {
                    __getValue = __getValue.Replace("-", "");
                    if (__getValue.IndexOf('%') == -1)
                    {
                        amount -= MyLib._myGlobal._decimalPhase(__getValue);
                    }
                    else
                    {
                        __getValue = __getValue.Replace("%", "");
                        amount -= ((MyLib._myGlobal._decimalPhase(__getValue) / 100M) * amount);
                    }
                }
            }
            __result._newPrice = price;
            __result._discountAmount = _round(__oldAmount - amount, point);
            return __result;
            //return Math.Round(__oldAmount - amount, point);
        }

        public static decimal _calcAfterDiscount(string discountWord, decimal amount, int point)
        {
            return _calcAfterDiscount(discountWord, amount, point, 1);
        }

        public static decimal _calcAfterDiscount(string discountWord, decimal amount, int point, decimal qty)
        {
            if (discountWord.Trim().Length == 0)
            {
                return amount;
            }
            string[] __words = discountWord.Replace(" ", "").Replace(" ", "").Replace(" ", "").Replace(" ", "").Split(',');
            for (int __loop = 0; __loop < __words.Length; __loop++)
            {
                string __getValue = __words[__loop].ToString();
                if (__getValue.IndexOf("@") == 0)
                {
                    // toe สำหรับของคุณปัญญาชัย ลดต่อชิ้น
                    amount -= _round((MyLib._myGlobal._decimalPhase(__getValue.Replace("@", string.Empty)) * qty), point);

                }
                else if (__getValue.IndexOf('%') == -1)
                {
                    amount -= _round(MyLib._myGlobal._decimalPhase(__getValue), point);
                }

                else
                {
                    __getValue = __getValue.Replace("%", "");
                    //amount -= ((MyLib._myGlobal._decimalPhase(__getValue) / 100M) * amount);
                    amount -= _round((MyLib._myGlobal._decimalPhase(__getValue) / 100M) * amount, point);
                }
            }
            return _round(amount, point);
            //return Math.Round(amount, point);
        }



        public static string _concatQuery(string value1, string value2)
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            switch (__myFrameWork._databaseSelectType)
            {
                case _databaseType.PostgreSql: return "textcat(" + value1 + "," + value2 + ")";
            }
            return "concat(" + value1 + "," + value2 + ")";
        }

        /// <summary>
        /// ดึงข้อมูลจาก web www.smlsoft.com เพื่อใช้งานต่างๆ เช่น Auto Template
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable _serverTemplateLoad(string query)
        {
            _myFrameWork __select = new _myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
            return __select._query(MyLib._myGlobal._masterDatabaseName, query).Tables[0];
        }

        /// <summary>
        /// สำหรับบางฐานข้อมูล ในการ Search จะต้องทำให้เป็นตัวใหญ่เหมือนกัน ไม่งั้นไม่เจอ (Firebird, PostgreSql) แต่ถ้าฐานข้อมูลอื่นไม่เป็นไร
        /// </summary>
        /// <param name="value">ค่าที่ต้องการเพิ่ม upper</param>
        /// <returns></returns>
        public static string _addUpper(string value)
        {
            return _addUpper(value, false);
        }


        /// <summary>
        /// สำหรับบางฐานข้อมูล ในการ Search จะต้องทำให้เป็นตัวใหญ่เหมือนกัน ไม่งั้นไม่เจอ (Firebird, PostgreSql) แต่ถ้าฐานข้อมูลอื่นไม่เป็นไร
        /// </summary>
        /// <param name="value">ค่าที่ต้องการเพิ่ม upper</param>
        /// <param name="haveAs">ต้องการให้มี as ต่อหรือไม่ ไม่งั้นชื่อ column จะไม่ตรง</param>
        /// <returns></returns>
        public static string _addUpper(string value, bool haveAs)
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            if (__myFrameWork._databaseSelectType == _databaseType.PostgreSql)
            {
                return "upper(" + value + ")" + ((haveAs) ? " as " + value : "");
            }
            return value;
        }

        /// <summary>
        /// isnull=MSSQL,ifnull=mysql,postgre ต้องใช้ coalesce
        /// </summary>
        /// <returns></returns>
        public static string _isnullStr()
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            switch (__myFrameWork._databaseSelectType)
            {
                case _databaseType.MySql: return "ifnull";
                case _databaseType.PostgreSql: return "coalesce";
            }
            __myFrameWork = null;
            return "isnull";
        }

        /// <summary>
        /// ประกอบ isnull query
        /// </summary>
        /// <param name="isNotNull">ตัวแปร กรณี is not null</param>
        /// <param name="isNull">ตัวแปร กรณี is null</param>
        /// <returns></returns>
        public static string _isnull(string isNotNull, string isNull)
        {
            return " " + _isnullStr() + "(" + isNotNull + "," + isNull + ") ";
        }

        /// <summary>
        /// โครงสร้างวันหยุดราชการ
        /// </summary>
        public class _officialHolidayType
        {
            /// <summary>
            /// วันที่
            /// </summary>
            public int _day;
            /// <summary>
            /// เดือน
            /// </summary>
            public int _month;
            /// <summary>
            /// ปี
            /// </summary>
            public int _year;
            /// <summary>
            /// ชื่อภาษาไทย
            /// </summary>
            public string _name1;
            /// <summary>
            /// ชื่อภาษาอังกฤษ
            /// </summary>
            public string _name2;
            /// <summary>
            /// เป็นวันหยุดหรือไม่
            /// </summary>
            public bool _bankHoliday;
            /// <summary>
            /// วันหยุดราชการหรือไม่
            /// </summary>
            public bool _governmentHoliday;
        }

        /// <summary>
        /// อ้างอิงหน้าจอ Manage Data + Datalist
        /// </summary>
        public class _referFieldType
        {
            /// <summary>
            /// ชื่อ Field
            /// </summary>
            public string _fieldName;
            /// <summary>
            /// ประเภท Field
            /// 1=String,2=Date,3=Number
            /// </summary>
            public int _fieldDataType;
            /// <summary>
            /// ข้อมูลปัจจุบัน
            /// </summary>
            public object _fieldData;
        }

        /// <summary>
        /// ตรวจดูว่าเป็นวันหยุดหรือไม่ (ธนาคาร)
        /// </summary>
        /// <param name="myDateTime"></param>
        /// <returns></returns>
        public static bool _bankHoliday(DateTime myDateTime)
        {
            try
            {
                bool __result = (bool)_holiday[(int)myDateTime.DayOfWeek];
                if (__result == false)
                {
                    for (int __loop = 0; __loop < _officialHoliday.Count; __loop++)
                    {
                        _officialHolidayType __getHoliday = (_officialHolidayType)_officialHoliday[__loop];
                        if (__getHoliday._day == myDateTime.Day && __getHoliday._month == myDateTime.Month && __getHoliday._year == myDateTime.Year)
                        {
                            if (__getHoliday._bankHoliday)
                            {
                                __result = true;
                            }
                            break;
                        }
                    }
                }
                return __result;
            }
            catch
            {
                // Debugger.Break();
                return false;
            }
        }

        /// <summary>
        /// ตรวจดูว่าเป็นวันหยุดหรือไม่ (ราชการ)
        /// </summary>
        /// <param name="myDateTime"></param>
        /// <returns></returns>
        public static bool _governmentHoliday(DateTime myDateTime)
        {
            try
            {
                bool __result = (bool)_holiday[(int)myDateTime.DayOfWeek];
                if (__result == false)
                {
                    for (int __loop = 0; __loop < _officialHoliday.Count; __loop++)
                    {
                        _officialHolidayType __getHoliday = (_officialHolidayType)_officialHoliday[__loop];
                        if (__getHoliday._day == myDateTime.Day && __getHoliday._month == myDateTime.Month && __getHoliday._year == myDateTime.Year)
                        {
                            if (__getHoliday._governmentHoliday)
                            {
                                __result = true;
                            }
                            break;
                        }
                    }
                }
                return __result;
            }
            catch
            {
                // Debugger.Break();
                return false;
            }
        }

        public static string _firstString(string source)
        {
            StringBuilder __firstString = new StringBuilder();
            int __trial = 0;
            while (__trial < source.Length)
            {
                if (source[__trial] >= 'A' && source[__trial] <= 'Z')
                {
                    __firstString.Append(source[__trial]);
                }
                else
                {
                    break;
                }
                __trial++;
            }
            return __firstString.ToString();
        }

        /// <summary>
        /// เปลี่ยนวันที่ ให้เป็นข้อความ
        /// </summary>
        /// <param name="myDateTime"></param>
        /// <returns></returns>
        public static string _convertDateToString(DateTime myDateTime, bool fullMode)
        {
            return _convertDateToString(myDateTime, fullMode, false);
        }

        public static CultureInfo _cultureInfo()
        {
            return new CultureInfo((_year_type == 1) ? "th-TH" : "en-US");
        }

        public static string _convertDateToString(DateTime myDateTime, bool fullMode, bool shortFormat)
        {
            return _convertDateToString(myDateTime, fullMode, shortFormat, "d/M/yyyy");
        }


        public static string _convertDateToString(DateTime myDateTime, bool fullMode, bool shortFormat, string formatStr)
        {
            StringBuilder __result = new StringBuilder();
            //IFormatProvider __culture = new CultureInfo("th-TH");
            /*if (fullMode)
            {
                __result.Append((myDateTime.Year <= 1000) ? "" : myDateTime.ToString("d MMMM yyyy (dddd)", __culture));
                if (__result.Length > 0)
                {
                    for (int __loop = 0; __loop < MyLib._myGlobal._officialHoliday.Count; __loop++)
                    {
                        MyLib._myGlobal._officialHolidayType __getHoliday = (MyLib._myGlobal._officialHolidayType)MyLib._myGlobal._officialHoliday[__loop];
                        if (__getHoliday._day == myDateTime.Day && __getHoliday._month == myDateTime.Month)
                        {
                            __result.Append(" ").Append(((MyLib._myGlobal._language == 0) ? __getHoliday._name1 : __getHoliday._name2));
                            if (__getHoliday._bankHoliday)
                            {
                                __result.Append((MyLib._myGlobal._language == 0) ? " ธนาคาร" : " Bank");
                            }
                            if (__getHoliday._governmentHoliday)
                            {
                                __result.Append((MyLib._myGlobal._language == 0) ? " ราชการ" : " Government");
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                __result.Append((myDateTime.Year <= 1000) ? "" : myDateTime.ToString((shortFormat) ? "dd/MM/yyyy" : "d MMMM yyyy", __culture));
            }
            __result.Append((myDateTime.Year <= 1000) ? "" : myDateTime.ToString((shortFormat) ? "dd/MM/yyyy" : "d MMMM yyyy", __culture));*/
            __result.Append((myDateTime.Year <= 1000) ? "" : myDateTime.ToString(formatStr, _cultureInfo()));
            return __result.ToString();
        }

        /// <summary>
        /// ตรวจสอบ เลขที่เอกสารก่อนว่ามีเอกสารใหม่หรือไม่ 
        /// 
        /// </summary>
        /// <param name="oldCode"></param>
        /// <param name="_tableName"></param>
        /// <param name="_fildName"></param>
        /// <returns></returns>
        /// Create By : Moo Ae
        /// Create Date 15/05/2551
        public static bool _chkAutoRunBeforSave(int mode, string oldCode, string _tableName, string _fildName)
        {
            bool _return = false;
            if (mode == 1)
            {
                bool __chkNewCode = _getAutoRunBeforSave(_tableName, _fildName, oldCode);
                //1 = true , 0 = false
                if (__chkNewCode)
                {
                    _isCheckRuningBeforSave = false;
                    _return = true;
                }
                else
                {
                    string message = "  รหัส/เลขที่เอกสาร : " + oldCode + " มีอยู่ในระบบแล้ว ???  \n  ต้องการเปลื่ยนเป็นรหัสใหม่แล้วดำเนินการต่อไป  \n  ใช่ หรือ ไม่ ถ้าใช่ กด Yes";
                    if (MessageBox.Show(message, "Really Confirm?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _isCheckRuningBeforSave = true;
                        _return = true;
                    }
                }
            }
            else
            {
                _return = true;
            }
            return _return;
        }

        /// Create By : Moo Ae
        /// Create Date 15/05/2551
        /// 
        public static bool _getAutoRunBeforSave(string _tableName, string _fildName, string _oldCode)
        {
            bool __result = true;
            try
            {
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                string _query = "select " + _fildName + " from  " + _tableName + " where " + _fildName + " = '" + _oldCode + "'";
                DataSet __getLastCode = _myFrameWork._query(MyLib._myGlobal._databaseName, _query);
                if (__getLastCode.Tables[0].Rows.Count > 0)
                {
                    __result = false;// __result = _autoRunningNumberStyleRightToLeft(__getLastCode.Tables[0].Rows[0].ItemArray[0].ToString().Trim());
                }
            }
            catch
            {
            }
            return __result;
        }
        /// Create By : Moo Ae
        /// Create Date 15/05/2551
        public static string _runningRightToLeftBeforSave(string number)
        {
            string result = "";
            string getNumberStr = "";
            StringBuilder newFormat = new StringBuilder();
            int tail = number.Length - 1;
            Boolean first = false;
            while (tail >= 0)
            {
                char getChr = number[tail];
                if (getChr >= '0' && getChr <= '9')
                {
                    if (first == true)
                    {
                        newFormat.Append("0");
                    }
                    first = true;
                    getNumberStr = getChr.ToString() + getNumberStr;
                }
                else
                {
                    break;
                }
                tail--;
            } // while
            if (newFormat.Length > 0)
            {
                tail++;
                StringBuilder __newFormat = new StringBuilder();
                __newFormat.Append(string.Concat("{0:", newFormat, "#}"));
                result = number.Substring(0, tail);
            }
            return (result);
        }

        /// <summary>
        /// running เลขที่เอกสาร โดยอัตโนมัติ
        /// </summary>
        /// <param name="_tableName"></param>
        /// <param name="_fildName"></param>
        /// <returns></returns>
        /// <param name="number">เอกสารต้นฉบับ</param>
        /// Create By : Moo Ae
        /// Create Date 24/04/2551
        public static string _getAutoRun(string tableName, string fildName)
        {
            string __result = "";
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ตรวจสอบ Data Bases Type
                string __query = "";
                switch (__myFrameWork._databaseSelectType)
                {
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                        {
                            __query = "select top 1 " + fildName + " from  " + tableName + " order by " + fildName + " desc ";
                        }
                        break;
                    default:
                        __query = "select " + fildName + " from  " + tableName + " order by " + fildName + " desc  limit(1)";
                        break;
                }
                DataSet __getLastCode = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__getLastCode.Tables[0].Rows.Count > 0)
                {
                    __result = _autoRunningNumberStyleRightToLeft(__getLastCode.Tables[0].Rows[0].ItemArray[0].ToString().Trim());
                }
                else
                {
                    // ไม่พบประเภทเอกสาร
                    MessageBox.Show(MyLib._myGlobal._resource("doc_type_not_found"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK);
                }
            }
            catch
            {
            }
            return __result;
        }

        /// <summary>
        /// running เลขที่เอกสาร โดยไล่จากขวามาซ้าย แบบมีเงื่อนไข
        /// </summary>
        /// <param name="number">tableName=ชื่อเทเบิล,fildName=ชื่อฟิว,fildWhereName=ชื่อฟิวที่เป็นเงื่อนไข,fildValue=ข้อมูลที่ใช้ในเงื่อนไข</param>
        public static string _getAutoRun(string tableName, string fildName, string fildWhereName, string fildValue)
        {
            string __result = "";
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ตรวจสอบ Data Bases Type
                string __query = "";
                switch (__myFrameWork._databaseSelectType)
                {
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                        __query = "select top 1 " + fildName + " from  " + tableName + " where " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc ";
                        break;
                    default:
                        __query = "select " + fildName + " from  " + tableName + " where " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc  limit(1)";
                        break;
                }
                DataSet __getLastCode = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__getLastCode.Tables[0].Rows.Count > 0)
                {
                    __result = _autoRunningNumberStyleRightToLeft(__getLastCode.Tables[0].Rows[0].ItemArray[0].ToString().Trim());
                }
                else
                {
                    // ไม่พบเอกสารในแฟ้มงาน... 
                    MessageBox.Show(MyLib._myGlobal._resource("doc_type_not_found"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK);
                }
            }
            catch
            {
            }
            return __result;
        }

        /// <summary>
        /// running เลขที่เอกสาร โดยไล่จากขวามาซ้าย แบบมีเงื่อนไข
        /// </summary>
        /// <param name="number">tableName=ชื่อเทเบิล,fildName=ชื่อฟิว,fildWhereName=ชื่อฟิวที่เป็นเงื่อนไข,fildValue=ข้อมูลที่ใช้ในเงื่อนไข,fildLike=ค้นหาแบบLike</param>
        public static string _getAutoRun(string tableName, string fildName, string fildWhereName, string fildValue, string fildLike)
        {
            string __result = "";
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                // ตรวจสอบ Data Bases Type
                string __query = "";
                switch (__myFrameWork._databaseSelectType)
                {
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                        __query = "select top 1 " + fildName + " from  " + tableName + " where " + fildName + " like '%" + fildLike + "%' and " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc ";
                        break;
                    default:
                        __query = "select " + fildName + " from  " + tableName + " where " + fildName + " like '%" + fildLike + "%' and " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc  limit(1)";
                        break;
                }
                DataSet __getLastCode = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__getLastCode.Tables[0].Rows.Count > 0)
                {
                    //ถ้า like เจอ เข้าทำ
                    String[] __chkFild = fildLike.Split('-');
                    // ตรวจสอบ Data Bases Type
                    string __chkQuery = "";
                    switch (__myFrameWork._databaseSelectType)
                    {
                        case _myGlobal._databaseType.MicrosoftSQL2005:
                        case _myGlobal._databaseType.MicrosoftSQL2000:
                            __chkQuery = "select top 1 " + fildName + " from  " + tableName + " where " + fildName + " = " + fildLike + " and " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc";
                            break;
                        default:
                            __chkQuery = "select " + fildName + " from  " + tableName + " where " + fildName + " = " + fildLike + " and " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc  limit(1)";
                            break;
                    }
                    DataSet __getCode = __myFrameWork._query(MyLib._myGlobal._databaseName, __chkQuery);
                    if (__getCode.Tables[0].Rows.Count > 0)
                    {
                        //มี รหัสนี้  อยู่แล้ว ให้ รัน หมายเลข สุดท้ายแทน
                        __result = _autoRunningNumberStyleRightToLeft(__getLastCode.Tables[0].Rows[0].ItemArray[0].ToString().Trim());
                    }
                    else
                    {
                        //ไม่มี รหัสนี้ อยู่
                        int __chklengthSplit = __chkFild.Length;
                        if (__chklengthSplit > 1)
                        {
                            if ((int)MyLib._myGlobal._decimalPhase(__chkFild[1].ToString()) == 0)
                            {
                                //ใส่ค่ามา 2 ตัว แต่ รหัส เป็น 0 ให้ รัน หมายเลข ถัดไป
                                __result = _autoRunningNumberStyleRightToLeft(fildLike);
                            }
                            else
                            {
                                //ใส่ค่ามา 2 ตัว รหัสไม่เป็น 0 ให้ ใส่ค่า ตรงตัวเลย
                                __result = fildLike;
                            }
                        }
                        else
                        {
                            //ใส่ค่า มา 1 ตัว ให้ เพิ่ม หมายเลข ที่ 1 เพิ่มเข้าไป
                            __result = __chkFild[0].ToString() + "-00001";
                        }
                    }
                }
                else
                {
                    //ไม่เจอ like เข้าทำ
                    String[] __chkFild = fildLike.Split('-');
                    string __chkQuery = "";
                    switch (__myFrameWork._databaseSelectType)
                    {
                        case _myGlobal._databaseType.MicrosoftSQL2005:
                        case _myGlobal._databaseType.MicrosoftSQL2000:
                            __chkQuery = "select top 1 " + fildName + " from  " + tableName + " where " + fildName + " like  %" + __chkFild[0].ToString() + "% and " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc";
                            break;
                        default:
                            __chkQuery = "select " + fildName + " from  " + tableName + " where " + fildName + " like  %" + __chkFild[0].ToString() + "% and " + fildWhereName + " = " + fildValue + " order by " + fildName + " desc  limit(1)";
                            break;
                    }
                    DataSet __getCode = __myFrameWork._query(MyLib._myGlobal._databaseName, __chkQuery);
                    if (__getCode.Tables[0].Rows.Count > 0)
                    {
                        //มีหัวแบบเดียวกับ รหัส เดิม ให้ใส่ค่า ตรงตัว
                        __result = fildLike;
                    }
                    else
                    {
                        //มีหัวแบบ อื่น
                        int __chklengthSplit = __chkFild.Length;
                        if (__chklengthSplit > 1)
                        {
                            //มีค่า 2 ตัว
                            if ((int)MyLib._myGlobal._decimalPhase(__chkFild[1].ToString()) == 0)
                            {
                                //รหัสเป็น 0 ใส่ค่าถัดไป
                                __result = _autoRunningNumberStyleRightToLeft(fildLike);
                            }
                            else
                            {
                                //รหัสไม่เป็น 0 ใส่ค่าตรงตัว
                                __result = fildLike;
                            }
                        }
                        else
                        {
                            //มีค่า 1 ตัวให้ ใส่ หมายเลข 1 เข้าไป
                            __result = __chkFild[0].ToString() + "-00001";
                        }
                    }
                }
            }
            catch
            {
            }
            return __result;
        }

        /// <summary>
        /// running เลขที่เอกสาร โดยไล่จากขวามาซ้าย แบบมีเงื่อนไข
        /// </summary>
        /// <param name="number">tableName=ชื่อเทเบิล,fildName=ชื่อฟิว,fildWhereName=ชื่อฟิวที่เป็นเงื่อนไข,fildValue=ข้อมูลที่ใช้ในเงื่อนไข ,fildTranslagName=ชื่อฟิวที่เป็นเงื่อนไข,fildTransValue=ข้อมูลที่ใช้ในเงื่อนไข</param>
        public static string _getAutoRun(string tableName, string fildName, string fildWhereName, string fildValue, string fildTranslagName, string fildTransValue)
        {
            string __result = "";
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ตรวจสอบ Data Bases Type
                string __query = "";
                switch (__myFrameWork._databaseSelectType)
                {
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                        __query = "select top 1 " + fildName + " from  " + tableName + " where " + fildWhereName + " = " + fildValue + " and " + fildTranslagName + " = " + fildTransValue + " order by " + fildName + " desc";
                        break;
                    default:
                        __query = "select " + fildName + " from  " + tableName + " where " + fildWhereName + " = " + fildValue + " and " + fildTranslagName + " = " + fildTransValue + " order by " + fildName + " desc  limit(1)";
                        break;
                }
                DataSet __getLastCode = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__getLastCode.Tables[0].Rows.Count > 0)
                {
                    __result = _autoRunningNumberStyleRightToLeft(__getLastCode.Tables[0].Rows[0].ItemArray[0].ToString().Trim());
                }
                else
                {
                    // ไม่พบเอกสารในแฟ้มงาน... 
                    MessageBox.Show(MyLib._myGlobal._resource("doc_type_not_found"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK);
                }
            }
            catch
            {
            }
            return __result;
        }
        ///MoOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOo
        ///นับเพื่อ กำหนดพนักงาน ของ payroll
        public static int _countEmployee_Limited;
        /// <summary>
        /// running เลขที่เอกสาร โดยไล่จากขวามาซ้าย แบบมีเงื่อนไข
        /// </summary>
        /// <param name="number">tableName=ชื่อเทเบิล,fildName=ชื่อฟิว,fildWhereName=ชื่อฟิวที่เป็นเงื่อนไข,fildValue=ข้อมูลที่ใช้ในเงื่อนไข ,fildTranslagName=ชื่อฟิวที่เป็นเงื่อนไข,fildTransValue=ข้อมูลที่ใช้ในเงื่อนไข</param>
        public static string _getAutoRun(string tableName, string fildName, string quereyWhere, string fildValue, bool _bool)
        {
            return _getAutoRun(tableName, fildName, quereyWhere, fildValue, _bool, "#####");
        }

        public static string _getAutoRun(string tableName, string fieldName, string quereyWhere, string docNo, bool _bool, string format)
        {
            return _getAutoRun(tableName, fieldName, quereyWhere, docNo, _bool, format, "");
        }

        public static string _getAutoRun(string tableName, string fieldName, string quereyWhere, string docNo, bool _bool, string format, string docRunningStart)
        {
            string __result = "";
            try
            {
                Boolean __passRunning = false;
                double __runningNumber = 1;
                string __newFormat = format.Replace('#', '0');
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // ตรวจสอบ Data Bases Type
                string __query = "";
                switch (__myFrameWork._databaseSelectType)
                {
                    case _myGlobal._databaseType.MicrosoftSQL2005:
                    case _myGlobal._databaseType.MicrosoftSQL2000:
                        __query = "select top 1 " + fieldName + " from  " + tableName + " where " + quereyWhere + " order by " + fieldName + " desc ";
                        break;
                    default:
                        __query = "select " + fieldName + " from  " + tableName + " where " + quereyWhere + " order by " + fieldName + " desc  limit(1)";
                        break;
                }
                DataSet __getLastCode = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__getLastCode.Tables[0].Rows.Count > 0)
                {
                    if (__getLastCode.Tables[0].Rows[0][0].ToString().IndexOf(docNo) != -1)
                    {
                        string __docRun = __getLastCode.Tables[0].Rows[0][0].ToString();
                        __docRun = __docRun.Substring(docNo.Length);
                        __runningNumber = double.Parse(__docRun) + 1;
                        __passRunning = true;
                    }
                }

                if (__passRunning == false)
                {
                    // toe default running
                    if (docRunningStart.Length > 0)
                    {
                        if (docRunningStart.IndexOf(docNo) != -1)
                        {
                            string __docRun = docRunningStart;
                            __docRun = __docRun.Substring(docNo.Length);
                            __runningNumber = double.Parse(__docRun) + 1;
                        }
                    }
                }

                __result = docNo + String.Format("{0:" + __newFormat.Remove(0, 1) + "#}", __runningNumber);
            }
            catch
            {
            }
            return __result;
        }


        /// <summary>
        /// running เลขที่เอกสาร โดยไล่จากขวามาซ้าย
        /// </summary>
        /// <param name="number">เอกสารต้นฉบับ</param>
        public static string _autoRunningNumberStyleRightToLeft(string number)
        {
            string __result = "";
            string __getNumberStr = "";
            StringBuilder __newFormat = new StringBuilder();
            int __tail = number.Length - 1;
            Boolean __first = false;
            while (__tail >= 0)
            {
                char __getChr = number[__tail];
                if (__getChr >= '0' && __getChr <= '9')
                {
                    if (__first == true)
                    {
                        __newFormat.Append("0");
                    }
                    __first = true;
                    __getNumberStr = __getChr.ToString() + __getNumberStr;
                }
                else
                {
                    break;
                }
                __tail--;
            } // while
            if (__newFormat.Length > 0)
            {
                __tail++;
                StringBuilder __newFormat2 = new StringBuilder(); // anek edit 2-4-2550
                __newFormat2.Append(string.Concat("{0:", __newFormat, "#}"));
                string __beginStr = number.Substring(0, __tail);
                decimal __getNumber = MyLib._myGlobal._decimalPhase(__getNumberStr) + 1;
                string __endStr = string.Format(__newFormat2.ToString(), __getNumber);
                __result = string.Format("{0}{1}", __beginStr, __endStr);
            }
            return (__result);
        }



        public static void _startSearchBox(_myTextBox getControl, string label, _searchDataFull screen)
        {
            _startSearchBox(getControl, label, screen, true, false, "");
        }

        public static void _startSearchBox(_myTextBox getControl, string label, _searchDataFull screen, bool showDialog)
        {
            _startSearchBox(getControl, label, screen, showDialog, false, "");
        }

        public static void _startSearchBox(IWin32Window sender, _myTextBox getControl, string label, _searchDataFull screen, bool showDialog)
        {
            _startSearchBox(sender, getControl, label, screen, showDialog, false, "");
        }

        public static void _startSearchBox(_myTextBox getControl, string label, _searchDataFull screen, bool showDialog, bool reloadData, string whereQuery)
        {
            _startSearchBox(_myGlobal._mainForm, getControl, label, screen, showDialog, reloadData, whereQuery);
        }

        public static void _startSearchBox(IWin32Window sender, _myTextBox getControl, string label, _searchDataFull screen, bool showDialog, bool reloadData, string whereQuery)
        {
            screen._show = true;
            screen.Text = label;
            screen.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            screen.Size = new Size(450, 320);
            int __newLocationX = (0 - screen.Width) + getControl._iconSearch.Width;
            int __newLocationY = getControl._iconSearch.Height;
            Point __newPoint = getControl._iconSearch.PointToScreen(new Point(__newLocationX, __newLocationY));
            screen.DesktopLocation = __newPoint;
            if (screen.DesktopLocation.Y + screen.Height + 50 > Screen.PrimaryScreen.Bounds.Height)
            {
                __newLocationY -= (screen.Height + getControl.Height);
                __newPoint = getControl._iconSearch.PointToScreen(new Point(__newLocationX, __newLocationY));
                screen.DesktopLocation = __newPoint;
            }
            if (__newPoint.X < 5)
            {
                __newPoint.X = 5;
                screen.DesktopLocation = __newPoint;
            }
            screen.StartPosition = FormStartPosition.Manual;
            /*if (screen._dataList._loadViewDataSuccess == false || reloadData)
            {
                screen._dataList._recalcPosition();
                screen._dataList._loadViewData(0, whereQuery);
            }*/
            if (showDialog)
            {
                screen._showMode = 0;
                screen.Opacity = 100;
                screen.ShowDialog();
            }
            else
            {
                screen._showMode = 1;
                screen.Opacity = 80;
                if (screen.Visible == false)
                {
                    screen.Show(sender);
                }
            }
            if (screen._dataList._loadViewDataSuccess == false || reloadData)
            {
                screen._dataList._recalcPosition();
                screen._dataList._loadViewData(0, whereQuery);
            }
        }

        public static string _getFormatNumber(string mode)
        {
            string __result = "{0:n2}";
            mode = mode.ToLower();
            if (mode.Length > 0 && mode[0] == 'm')
            {
                mode = mode.Remove(0, 1);
                int __point = MyLib._myGlobal._intPhase(mode);
                __result = string.Concat("{0:n", __point.ToString(), "}");
            }
            return (__result);
        }

        /// <summary>
        /// ค้นหาว่า ชื่อ column อยู่ในหมายเลขอะไรของระบบ เช่น 0=varchar,1=currency
        /// </summary>
        /// <param name="name">ชื่อ column type</param>
        /// <returns></returns>
        public static int _databaseColumnFind(string name)
        {
            for (int __loop = 0; __loop < _myGlobal._databaseColumnTypeList.Length; __loop++)
            {
                if (name.ToLower().CompareTo(_myGlobal._databaseColumnTypeList[__loop].ToString().ToLower()) == 0)
                {
                    return (__loop);
                }
            } // for
            return (0);
        }

        public static DateTime _convertDate(string date)
        {
            return _convertDate(date, MyLib._myGlobal._year_type);
        }

        public static int _checkYearType(DateTime date)
        {
            if (date.Year > 2500)
                return 1;

            return 0;
        }

        /// <summary>
        /// สำหรับตรวจสอบประวันที่ว่าถูกต้องหรือเปล่า
        /// </summary>
        /// <param name="date"></param>
        /// <param name="yearType">0 us, 1 th</param>
        /// <returns></returns>
        public static DateTime _checkDate(DateTime date, int yearType)
        {
            if (yearType == 1)
            {
                if (date.Year < 2500)
                {
                    date = date.AddYears(543);
                }
            }
            else
            {
                if (date.Year > 2500)
                {
                    date = date.AddYears(-543);
                }

            }

            return date;
        }

        public static DateTime _convertDate(string date, int yearType)
        {
            DateTime __result = new DateTime(1800, 1, 1);
            try
            {
                if (date.Length > 0)
                {
                    string __dateConvert = date;
                    if (__dateConvert.IndexOf('/') != -1)
                    {
                        switch (yearType)
                        {
                            case 0:
                                {
                                    // ปี us สลับ จาก dd/mm/yyyy เป็น yyyy-mm-dd
                                    string[] __split = __dateConvert.Split('/');
                                    __dateConvert = string.Format("{0}-{1}-{2}", __split[2].ToString(), __split[1].ToString(), __split[0].ToString());
                                    __result = DateTime.Parse(__dateConvert, new CultureInfo("en-US"));
                                    return (__result);
                                }
                            case 1:
                                {
                                    // ปี thai  dd/mm/yyyy 
                                    __result = DateTime.Parse(__dateConvert, new CultureInfo("th-TH"));
                                    return (__result);
                                }
                        }
                    }
                    __result = DateTime.Parse(__dateConvert, _myGlobal._cultureInfo());
                }
            }
            catch
            {
                // Debugger.Break();
            }
            return (__result);
        }

        public static string[] _getTopAndLimitOneRecord()
        {
            return _getTopAndLimitOneRecord(1);
        }

        /// <summary>
        /// เชื่อมข้อความใน Query, PostgreSQL=||, SQLServer = +
        /// </summary>
        /// <returns></returns>
        public static string _getSignPlusStringQuery()
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            string __result = "";
            switch (__myFrameWork._databaseSelectType)
            {
                case _myGlobal._databaseType.MicrosoftSQL2005:
                case _myGlobal._databaseType.MicrosoftSQL2000:
                    __result = " + ";
                    break;
                default:
                    __result = " || ";
                    break;
            }
            return __result;

        }

        /// <summary>
        /// somruk แก้ top และ Limit
        /// </summary>
        /// <returns>string [0] top string [1] limit</returns>
        public static string[] _getTopAndLimitOneRecord(int number)
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            string[] __result = { "", "" };
            switch (__myFrameWork._databaseSelectType)
            {
                case _myGlobal._databaseType.MicrosoftSQL2005:
                case _myGlobal._databaseType.MicrosoftSQL2000:
                    __result[0] = " top " + number.ToString() + " ";
                    __result[1] = "";
                    break;
                default:
                    __result[0] = "";
                    __result[1] = " offset 0 limit " + number.ToString() + " ";
                    break;
            }
            return __result;
        }

        public static string _sqlDateFunction(string value)
        {
            string __result = "";
            _myFrameWork __myFrameWork = new _myFrameWork();
            switch (__myFrameWork._databaseSelectType)
            {
                case _myGlobal._databaseType.MicrosoftSQL2005:
                case _myGlobal._databaseType.MicrosoftSQL2000:
                    __result = "cast(\'" + value + "\' as datetime)";
                    break;
                default:
                    __result = "date(\'" + value + "\')";
                    break;
            }
            return __result;
        }

        public static DateTime _convertDateFromQuery(string date)
        {
            if (date == "")
            {
                return new DateTime();
            }

            DateTime __result = new DateTime(1979, 3, 25);
            try
            {
                if (date.Length > 0)
                {
                    __result = DateTime.Parse(date, new CultureInfo("en-US"));
                }
            }
            catch
            {
                // Debugger.Break();
            }
            return (__result);
        }

        public static string _convertDateToQuery(DateTime date)
        {
            string __result = "";
            try
            {
                if (date.CompareTo(new DateTime(1900, 1, 1)) < 0)
                {
                    __result = "1900-1-1";
                }
                else
                {
                    __result = date.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                }
            }
            catch
            {
                __result = "1900-1-1";
            }
            return (__result);
        }

        public static string _convertDateTimeToQuery(DateTime date)
        {
            string __result = "";
            try
            {
                if (date.CompareTo(new DateTime(1900, 1, 1)) < 0)
                {
                    __result = "1900-1-1";
                }
                else
                {
                    __result = date.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
                }
            }
            catch
            {
                __result = "1900-1-1";
            }
            return (__result);
        }

        public static string _convertDateTimeToQuery(string dateStr)
        {
            string __result = "";
            try
            {
                DateTime __date = _convertDate(dateStr);
                IFormatProvider __culture = new CultureInfo("en-US");
                __result = __date.ToString("yyyy-MM-dd  HH:mm:ss", __culture);
            }
            catch
            {
                // Debugger.Break();
            }
            return (__result);
        }

        public static string _convertDateToQuery(string dateStr)
        {
            string __result = "";
            try
            {
                DateTime __date = _convertDate(dateStr);
                IFormatProvider __culture = new CultureInfo("en-US");
                __result = __date.ToString("yyyy-MM-dd", __culture);
            }
            catch
            {
                // Debugger.Break();
            }
            return (__result);
        }

        public static string _currentPath()
        {
            return (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));
        }

        public static string _getUserLevelName(int level)
        {
            string __result = "";
            switch (level)
            {
                case 0:
                    __result = "User";
                    break;
                case 1:
                    __result = "Super User";
                    break;
                case 2:
                    __result = "Admin";
                    break;
                case 3:
                    __result = "Supper Admin";
                    break;
            }
            return (__result);
        }

        public static string _helpPath()
        {
            return (string.Concat(_currentPath(), "\\..\\..\\htmlhelp\\", ((_language == 0) ? "thai" : "eng"), "\\"));
        }

        /// <summary>
        /// แปลงรูปภาพ (Image) เป็น String
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string _imageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// แปลง String เป็น Image
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image _base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public static void _demoVersion()
        {
            MessageBox.Show("Demo Only");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=ลบแล้ว,1=save ok</param>
        /// <returns></returns>
        public static DialogResult _displayWarning(int mode, string message)
        {
            if (message == null)
                message = "";
            DialogResult __result = new DialogResult();
            switch (mode)
            {
                case 0:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("delete_success");
                    }
                    // ลบข้อมูลเสร็จเรียบร้อย
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("save_success");
                    }
                    // บันทึกข้อมูลเสร็จเรียบร้อย
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    message = MyLib._myGlobal._resource("data_not_complete") + " " + message;
                    // ข้อมูลไม่สมบูรณ์
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 3:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("warning1");
                    }
                    // มีการแก้ไขข้อมูลบางส่วน\nต้องการยกเลิกการแก้ไขหรือไม่
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                    break;
                case 4:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("warning2");
                    }
                    // ท่านอยู่ในหัวข้อตรวจสอบข้อมูล\nถ้าต้องการแก้ไขให้กด Double Click ที่ข้อมูล
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 5:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("warning3");
                    }
                    // มีการแก้ไขปรับปรุง\n ต้องการออกจากหน้าจอนี้หรือไม่
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                    break;
                case 6:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("warning4");
                    }
                    // รหัสกลุ่มซ้ำ กรุณาตรวจสอบใหม่
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                case 7:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("warning5");
                    }
                    // ไม่พบรายการ กรุณาตรวจสอบใหม่
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                case 8:
                    if (message.Length == 0)
                    {
                        message = MyLib._myGlobal._resource("warning6");
                    }
                    // ต้องการจบโปรแกรมจริงหรือไม่
                    __result = MessageBox.Show(message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.YesNo);
                    break;
            }
            return (__result);
        }

        public static String _codeRemove(string source)
        {
            StringBuilder __result = new StringBuilder();
            source = source.ToUpper();
            for (int __loop = 0; __loop < source.Length; __loop++)
            {
                if (source[__loop] != '<' && source[__loop] != '>')
                {
                    __result.Append(source[__loop]);
                }
            }
            return __result.ToString();
        }

        public static Image _loadImageFromUrl(string url)
        {
            byte[] __downloadedData = new byte[0];

            WebRequest __req = WebRequest.Create(url);
            WebResponse __response = __req.GetResponse();
            Stream __stream = __response.GetResponseStream();
            //Download in chuncks
            byte[] __buffer = new byte[1024];
            //Get Total Size
            int __dataLength = (int)__response.ContentLength;

            MemoryStream __memStream = new MemoryStream();
            while (true)
            {
                //Try to read the data
                int __bytesRead = __stream.Read(__buffer, 0, __buffer.Length);
                if (__bytesRead == 0)
                {
                    //Finished downloading
                    break;
                }
                else
                {
                    //Write the downloaded data
                    __memStream.Write(__buffer, 0, __bytesRead);
                }
            }
            //Convert the downloaded stream to a byte array
            __downloadedData = __memStream.ToArray();
            //Clean up
            __stream.Close();
            __memStream.Close();
            //
            MemoryStream __stream2 = new MemoryStream(__downloadedData);
            Image __img = Image.FromStream(__stream2);
            __stream2.Close();
            return __img;
        }

        public static Decimal _decimalPhase(string value)
        {
            decimal __getValue = 0M;
            if (value != null && value.Length != 0)
            {
                decimal __value = 0M;
                if (Decimal.TryParse(value, out __value) == true)
                {
                    __getValue = __value;
                }
            }
            return __getValue;
        }

        public static int _intPhase(string value)
        {
            int __getValue = 0;
            if (value.Length != 0)
            {
                try
                {
                    int __value = 0;
                    if (Int32.TryParse(value, out __value) == true)
                    {
                        __getValue = __value;
                    }
                    else
                    {
                        __getValue = (int)_decimalPhase(value);
                    }
                }
                catch
                {
                    __getValue = (int)_decimalPhase(value);
                }
            }
            return __getValue;
        }

        public static string _convertMemoryStreamToString(MemoryStream ms)
        {
            ms.Seek(0, SeekOrigin.Begin);
            byte[] jsonBytes = new byte[ms.Length];
            ms.Read(jsonBytes, 0, (int)ms.Length);
            return Encoding.UTF8.GetString(jsonBytes);
        }

        // Toe ย้ายมาจาก Formdesign
        public static Color _convertStringToColor(string source)
        {
            if (source == null)
                return Color.Transparent;

            Color __result = new Color();
            if (source.IndexOf('=') != -1)
            {
                source = source.Replace(']', ',');
                string[] __split = source.Split('=');
                __result = Color.FromArgb(MyLib._myGlobal._intPhase(__split[1].Substring(0, __split[1].IndexOf(','))), MyLib._myGlobal._intPhase(__split[2].Substring(0, __split[2].IndexOf(','))), MyLib._myGlobal._intPhase(__split[3].Substring(0, __split[3].IndexOf(','))), MyLib._myGlobal._intPhase(__split[4].Substring(0, __split[4].IndexOf(','))));
            }
            else
            {
                int __index1 = source.IndexOf('[') + 1;
                int __index2 = source.IndexOf(']') - 1;
                string __getName = source.Substring(__index1, (__index2 - __index1) + 1);
                __result = Color.FromName(__getName);
            }
            return __result;
        }

        public static StringAlignment _getStringFormatTextAlignment(ContentAlignment __contentAlignment)
        {
            switch (__contentAlignment)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return StringAlignment.Center;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return StringAlignment.Far;
            }
            return StringAlignment.Near;
        }

        public static StringAlignment _getStringFormatTextLineAlignment(ContentAlignment __contentAlignment)
        {
            switch (__contentAlignment)
            {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    return StringAlignment.Center;
                case ContentAlignment.TopRight:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                    return StringAlignment.Near;
            }
            return StringAlignment.Far;
        }

        // toe 
        public static object _objectFromXml(string Xml, System.Type ObjType)
        {
            XmlSerializer ser = new XmlSerializer(ObjType);
            StringReader stringReader;
            stringReader = new StringReader(Xml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            object obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();
            return obj;
        }

        public static string _objectToXml(object Obj, System.Type ObjType)
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;

            XmlSerializer ser = new XmlSerializer(ObjType);
            MemoryStream memStream = new MemoryStream();
            StringBuilder __str = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(__str, setting);

            ser.Serialize(xmlWriter, Obj);
            xmlWriter.Close();
            memStream.Close();
            return __str.ToString();
        }

        public static void _writeLogFile(string filePath, string text, bool _startNewLog)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    if (_startNewLog)
                    {
                        FileStream aFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(aFile);
                        sw.Write(text);
                        sw.Close();
                        aFile.Close();
                    }
                    else
                    {
                        FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(aFile);
                        sw.WriteLine(text);
                        sw.Close();
                        aFile.Close();
                    }
                }
                else
                {
                    FileStream aFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.Write(text);
                    sw.Close();
                    aFile.Close();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// ตัดเครื่องหมาย แปลก ๆ ที่ทำให้ query error
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string _convertStrToQuery(string str)
        {
            return str.Replace("\'", "\'\'");
        }

        public static Boolean _checkChangeMaster()
        {
            if (_myGlobal._allowChangeMaster == false)
            {
                MessageBox.Show(_myGlobal._resource("ไม่อนุญาติให้แก้ไขข้อมูล"), _myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return _myGlobal._allowChangeMaster;
        }

        public static string _encapeStringForFilePath(string code)
        {
            return code.Replace("\\", "_").Replace(":", "__").Replace("/", string.Empty).Replace("*", string.Empty).Replace("\"", string.Empty).Replace("|", string.Empty).Replace("?", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty).Replace(".", "_");
        }

        public static object FromXml(string Xml, System.Type ObjType)
        {
            XmlSerializer __ser = new XmlSerializer(ObjType);
            StringReader __stringReader;
            __stringReader = new StringReader(Xml);
            XmlReader __xmlReader = XmlReader.Create(__stringReader);
            object obj = __ser.Deserialize(__xmlReader);
            __xmlReader.Close();
            __stringReader.Close();
            return obj;
        }

        public static void _writeEventLog(string message)
        {
            _writeEventLog(message, "Application Hang");
        }

        public static void _writeEventLog(string message, string source)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = source;
                eventLog.WriteEntry(message, EventLogEntryType.Error, 101, 1);
            }
        }
    }

    public class _PermissionsType
    {

        /// <summary>
        /// เข้าใช้งานได้
        /// </summary>
        public bool _isRead;
        /// <summary>
        /// เพิ่มได้
        /// </summary>
        public bool _isAdd;
        /// <summary>
        /// แก้ไขได้
        /// </summary>
        public bool _isEdit;
        /// <summary>
        /// ลบได้
        /// </summary>
        public bool _isDelete;
    }

    class _myGroupType
    {
        public string _codeOld;
        public string _code;
        public string _name;
        public Boolean _active;
        public Boolean _isChange;
        public Boolean _isOldRecord;
    }

    class _myDatabaseType
    {
        public string _codeOld;
        public string _code;
        public string _name;
        public string _databaseGroup;
    }

    class _myUserType
    {
        public string _codeOld;
        public string _code;
        public string _name;
        public Boolean _active;
        public int _level = 0;
        public string _password;
        public Boolean _isChange;
        public Boolean _isOldRecord;
        public int _swapMode;
        public bool _isUser = true;
        public string _device;
    }

    public class _myWebserviceType
    {
        public string _webServiceName = "";
        public bool _webServiceConnected = false;
        public int _connectBytesPerSecond = 100000;
    }

    class _myGroupMemberType
    {
        public ArrayList _userCode;
    }

    class _myDatabaseGroupType
    {
        public string _code;
        public string _name;
        public ArrayList _adminList;
    }

    // Main data
    public class _d
    {
        /// <summary>
        /// กลุ่มของฐานข้อมูล
        /// </summary>
        public class sml_database_group
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_database_group";
            /// <summary>
            /// รหัสกลุ่มผู้ข้อมูล
            /// </summary>
            public static String _group_code = "group_code";
            /// <summary>
            /// ชื่อกลุ่มผู้ใช้
            /// </summary>
            public static String _group_name = "group_name";
        }

        /// <summary>
        /// รหัส Admin ของแต่ละกลุ่มข้อมูล
        /// </summary>
        public class sml_database_group_admin
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_database_group_admin";
            /// <summary>
            /// รหัสกลุ่มข้อมูล
            /// </summary>
            public static String _group_code = "group_code";
            /// <summary>
            /// รหัสผู้ใช้
            /// </summary>
            public static String _admin_code = "admin_code";
        }

        /// <summary>
        /// รายชื่อฐานข้อมูลทั้งหมด
        /// </summary>
        public class sml_database_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_database_list";
            /// <summary>
            /// รหัสกลุ่มข้อมูล
            /// </summary>
            public static String _data_group = "data_group";
            /// <summary>
            /// รหัสข้อมูล
            /// </summary>
            public static String _data_code = "data_code";
            /// <summary>
            /// ชื่อบริษัท
            /// </summary>
            public static String _data_name = "data_name";
            /// <summary>
            /// ชื่อฐานข้อมูล
            /// </summary>
            public static String _data_database_name = "data_database_name";
            /// <summary>
            /// update xml ล่าสุด
            /// </summary>
            public static String _last_database_xml_update = "last_database_xml_update";
        }

        /// <summary>
        /// รายชื่อกลุ่มผู้ใช้ และรหัสผู้ใช้ที่มีสิทธิใช้ข้อมูล
        /// </summary>
        public class sml_database_list_user_and_group
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_database_list_user_and_group";
            /// <summary>
            /// รหัสกลุ่มข้อมูล
            /// </summary>
            public static String _data_group = "data_group";
            /// <summary>
            /// รหัสข้อมูล
            /// </summary>
            public static String _data_code = "data_code";
            /// <summary>
            /// รหัสผู้ใช้ หรือกลุ่มผู้ใช้
            /// </summary>
            public static String _user_or_group_code = "user_or_group_code";
            /// <summary>
            /// สถานะ (1=กลุ่ม,2=ผู้ใช้)
            /// </summary>
            public static String _user_or_group_status = "user_or_group_status";
        }

        /// <summary>
        /// รายชื่อกลุ่ม
        /// </summary>
        public class sml_group_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_group_list";
            /// <summary>
            /// รหัสกลุ่ม
            /// </summary>
            public static String _group_code = "group_code";
            /// <summary>
            /// ชื่อกลุ่ม
            /// </summary>
            public static String _group_name = "group_name";
            /// <summary>
            /// ใช้งาน
            /// </summary>
            public static String _active_status = "active_status";
            /// <summary>
            /// ค้นหา
            /// </summary>
            public static String _search = "search";
        }

        /// <summary>
        /// กำหนดสิทธิ์กลุ่มผู้ใช้งาน
        /// </summary>
        public class sml_permissions_group
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_permissions_group";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _usercode = "usercode";
            /// <summary>
            /// แฟ้มกำหนดสิทธิ์
            /// </summary>
            public static String _image_file = "image_file";
            /// <summary>
            /// ระบบ
            /// </summary>
            public static String _system_id = "system_id";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// รหัสเมนู
            /// </summary>
            public static String _menucode = "menucode";
            /// <summary>
            /// ชื่อเมนู
            /// </summary>
            public static String _menuname = "menuname";
            /// <summary>
            /// ใช้งานได้
            /// </summary>
            public static String _isread = "isread";
            /// <summary>
            /// เพิ่ม
            /// </summary>
            public static String _isadd = "isadd";
            /// <summary>
            /// ลบ
            /// </summary>
            public static String _isdelete = "isdelete";
            /// <summary>
            /// แก้ไข
            /// </summary>
            public static String _isedit = "isedit";
        }

        /// <summary>
        /// กำหนดสิทธิ์ผู้ใช้งาน
        /// </summary>
        public class sml_permissions_user
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_permissions_user";
            /// <summary>
            /// รหัส
            /// </summary>
            public static String _usercode = "usercode";
            /// <summary>
            /// แฟ้มกำหนดสิทธิ์
            /// </summary>
            public static String _image_file = "image_file";
            /// <summary>
            /// ระบบ
            /// </summary>
            public static String _system_id = "system_id";
            /// <summary>
            /// guid_code
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// รหัสเมนู
            /// </summary>
            public static String _menucode = "menucode";
            /// <summary>
            /// ชื่อเมนู
            /// </summary>
            public static String _menuname = "menuname";
            /// <summary>
            /// ใช้งานได้
            /// </summary>
            public static String _isread = "isread";
            /// <summary>
            /// เพิ่ม
            /// </summary>
            public static String _isadd = "isadd";
            /// <summary>
            /// ลบ
            /// </summary>
            public static String _isdelete = "isdelete";
            /// <summary>
            /// แก้ไข
            /// </summary>
            public static String _isedit = "isedit";
        }

        /// <summary>
        /// รายละเอียดข้อความหน้าจอ และรายงาน
        /// </summary>
        public class sml_resource
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_resource";
            /// <summary>
            /// รหัสกลุ่มข้อมูล
            /// </summary>
            public static String _data_group = "data_group";
            /// <summary>
            /// รหัสข้อความ
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// ข้อความภาษาไทย
            /// </summary>
            public static String _name_1 = "name_1";
            /// <summary>
            /// ข้อความภาษาอังกฤษ
            /// </summary>
            public static String _name_2 = "name_2";
            /// <summary>
            /// ข้อความภาษาอังกฤษ
            /// </summary>
            public static String _name_3 = "name_3";
            /// <summary>
            /// ข้อความภาษาอังกฤษ
            /// </summary>
            public static String _name_4 = "name_4";
            /// <summary>
            /// ข้อความภาษาอังกฤษ
            /// </summary>
            public static String _name_5 = "name_5";
            /// <summary>
            /// ข้อความภาษาอังกฤษ
            /// </summary>
            public static String _name_6 = "name_6";
            /// <summary>
            /// สถานะ
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// ความยาว
            /// </summary>
            public static String _length = "length";
        }

        /// <summary>
        /// รายละเอียดพนักงาน ที่อยู่ในกลุ่ม
        /// </summary>
        public class sml_user_and_group
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_user_and_group";
            /// <summary>
            /// รหัสพนักงาน
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// รหัสกลุ่ม
            /// </summary>
            public static String _group_code = "group_code";
        }

        /// <summary>
        /// รายละเอียดผู้ใช้
        /// </summary>
        public class sml_user_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_user_list";
            /// <summary>
            /// รหัสผู้ใช้
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// ชื่อผู้ใช้
            /// </summary>
            public static String _user_name = "user_name";
            /// <summary>
            /// รหัสผ่าน
            /// </summary>
            public static String _user_password = "user_password";
            /// <summary>
            /// ระดับความสามารถ
            /// </summary>
            public static String _user_level = "user_level";
            /// <summary>
            /// ใช้งาน
            /// </summary>
            public static String _active_status = "active_status";
            /// <summary>
            /// Search
            /// </summary>
            public static String _search = "search";
            /// <summary>
            /// Device
            /// </summary>
            public static String _device_id = "device_id";
        }

        /// <summary>
        /// GUID
        /// </summary>
        public class sml_guid
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_guid";
            /// <summary>
            /// รหัส GUID
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// วันเวลา Login
            /// </summary>
            public static String _login_time = "login_time";
            /// <summary>
            /// วันเวลา Connect ล่าสุด
            /// </summary>
            public static String _last_access_time = "last_access_time";
            /// <summary>
            /// รหัสผู้ใช้งาน
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// ชื่อคอมพิวเตอร์
            /// </summary>
            public static String _computer_name = "computer_name";
            /// <summary>
            /// ชื่อฐานข้อมูลที่ใช้
            /// </summary>
            public static String _database_code = "database_code";
        }

        /// <summary>
        /// รายการผู้เข้าใช้งาน
        /// </summary>
        public class sml_access_list
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_access_list";
            /// <summary>
            /// เวลาที่เข้าใช้
            /// </summary>
            public static String _access_time = "access_time";
            /// <summary>
            /// ประเภทการเข้าใช้
            /// </summary>
            public static String _access_type = "access_type";
            /// <summary>
            /// รหัสผู้ใช้งาน
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// ชื่อเครื่อง
            /// </summary>
            public static String _computer_name = "computer_name";
            /// <summary>
            /// รหัสฐานข้อมูล
            /// </summary>
            public static String _database_code = "database_code";
        }

        /// <summary>
        /// Language
        /// </summary>
        public class sml_language
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_language";
            /// <summary>
            /// 
            /// </summary>
            public static String _thai_lang = "thai_lang";
            /// <summary>
            /// 
            /// </summary>
            public static String _english_lang = "english_lang";
            /// <summary>
            /// 
            /// </summary>
            public static String _chinese_lang = "chinese_lang";
            /// <summary>
            /// 
            /// </summary>
            public static String _malay_lang = "malay_lang";
            /// <summary>
            /// 
            /// </summary>
            public static String _india_lang = "india_lang";
        }

        /// <summary>
        /// จับหน้าจอ
        /// </summary>
        public class sml_screen_capture
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_screen_capture";
            /// <summary>
            /// GUID
            /// </summary>
            public static String _guid_code = "guid_code";
            /// <summary>
            /// Request
            /// </summary>
            public static String _request = "request";
            /// <summary>
            /// Time
            /// </summary>
            public static String _capture_time = "capture_time";
            /// <summary>
            /// Screen
            /// </summary>
            public static String _capture_screen = "capture_screen";
            /// <summary>
            /// Computer Name
            /// </summary>
            public static String _computer_name = "computer_name";
            /// <summary>
            /// Database
            /// </summary>
            public static String _database_code = "database_code";
            /// <summary>
            /// User Code
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// thumbnail
            /// </summary>
            public static String _capture_screen_thumbnail = "capture_screen_thumbnail";
        }

        /// <summary>
        /// Table 15
        /// </summary>
        public class sml_screen_realtime
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "sml_screen_realtime";
            /// <summary>
            /// computername
            /// </summary>
            public static String _computer_name = "computer_name";
            /// <summary>
            /// screen
            /// </summary>
            public static String _screen_thumbnail = "screen_thumbnail";
            /// <summary>
            /// time
            /// </summary>
            public static String _request_time = "request_time";
            /// <summary>
            /// time
            /// </summary>
            public static String _update_time = "update_time";
        }
    }

    public class _languageClass
    {
        public string _code;
        public string _thai;
        public string _english;
        public string _malayu;
        public string _chinese;
        public string _india;
        public string _lao;

        public _languageClass(string code, string thai, string eng, string malayu, string chinese, string india)
        {
            this._code = code;
            this._thai = thai;
            this._english = eng;
            this._malayu = malayu;
            this._chinese = chinese;
            this._india = india;
            this._lao = eng;
        }

        public _languageClass(string code, string thai, string eng, string malayu, string chinese, string india, string lao)
        {
            this._code = code;
            this._thai = thai;
            this._english = eng;
            this._malayu = malayu;
            this._chinese = chinese;
            this._india = india;
            this._lao = (lao.Trim().Length == 0) ? eng : lao;
        }
    }

    public enum _languageEnum
    {
        English,
        Thai,
        Malayu,
        Chainese,
        India,
        Lao,
        Null
    }

    public class _providerListClass
    {
        public string _name;
        public _myGlobal._databaseType _databaseType;
    }

    public class _printerListClass
    {
        public string _printerName;
        public bool _isDefault;
    }

    public class _calcDiscountResultStruct
    {
        /// <summary>
        /// ราคาใหม่
        /// </summary>
        public decimal _newPrice = 0M;
        /// <summary>
        /// ราคาหลังหักส่วนลด
        /// </summary>
        public decimal _realPrice = 0M;
        /// <summary>
        /// ส่วนลดที่คำนวณได้
        /// </summary>
        public decimal _discountAmount = 0M;
        /// <summary>
        /// ส่วนลดสำหรับสูตร
        /// </summary>
        public string _discountWord = "";
    }

    [Serializable]
    public class _programClientConfig
    {
        public string _clientFontName = "";
        public float _clientFontSize = 9f;
    }
}
