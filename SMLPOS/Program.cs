using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Security.Principal;

namespace SMLPOS
{
    public static class Program
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Keys)vkCode == Keys.PrintScreen)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "insert into " + MyLib._d.sml_screen_capture._table + " (" + MyLib._myGlobal._fieldAndComma(MyLib._d.sml_screen_capture._guid_code, MyLib._d.sml_screen_capture._request) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._guid + "\'", "1") + ")";
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        const int SET_FEATURE_ON_THREAD = 0x00000001;
        const int SET_FEATURE_ON_PROCESS = 0x00000002;
        const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            CoInternetSetFeatureEnabled(
                FEATURE_DISABLE_NAVIGATION_SOUNDS,
                SET_FEATURE_ON_PROCESS,
                true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // toe on stop work write log
            AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs args)
            {
                //MyLib._myGlobal._writeEventLog(__ex.Message.ToString());
                Exception e = (Exception)args.ExceptionObject;
                MyLib._myGlobal._writeEventLog("Unhandled exception: " + e.ToString());
                Environment.Exit(1);
            };

            if (Environment.Version.Major < 2)
            {
                MessageBox.Show("This program need FrameWork 2.0 up, if your computer have frame work 1.1 please remove first.");
            }
            else
            {
                MyLib._myGlobal._isDesignMode = false;
                //MyLib._myGlobal._isVersion = "SMLERP";
                MyLib._myGlobal._language = MyLib._languageEnum.Thai;

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.Null)
                {
                    MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLPOS;
                }

                // Moo ย้ายมาจาก SMLERPTemplate._main
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                MyLib._myGlobal._computerName = WindowsIdentity.GetCurrent().Name;
                MyLib._myGlobal._year_type = 1;
                MyLib._myGlobal._year_add = 543;
                MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
                MyLib._myGlobal._workingDate = DateTime.Now;
                //SMLERPTemplate._main._startProgram();

                switch (MyLib._myGlobal._isVersionEnum)
                {
                    case MyLib._myGlobal._versionType.SMLPOS:
                        if (MyLib._myGlobal._subVersion == 0)
                            MyLib._myGlobal._programName = "SML POS Suriwong";
                        else
                            if (MyLib._myGlobal._programName.Length == 0)
                                MyLib._myGlobal._programName = "SML POS";
                        break;
                    case MyLib._myGlobal._versionType.SMLPOSLite:
                        if (MyLib._myGlobal._programName.Length == 0)
                            MyLib._myGlobal._programName = "SML POS Lite";
                        break;
                    case MyLib._myGlobal._versionType.SMLTomYumGoong:
                        if (MyLib._myGlobal._programName.Length == 0)
                            MyLib._myGlobal._programName = "SML Tom Yum Goong";
                        break;
                    case MyLib._myGlobal._versionType.SMLTomYumGoongPro:
                        if (MyLib._myGlobal._programName.Length == 0)
                            MyLib._myGlobal._programName = "SML Tom Yum Goong Pro";
                        break;
                    case MyLib._myGlobal._versionType.SMLPOSMED:
                        if (MyLib._myGlobal._programName.Length == 0)
                            MyLib._myGlobal._programName = "SML POS Medical";
                        break;
                }


                MyLib._myGlobal._profileFileName = "SMLERPLogin.XML".ToLower();
                MyLib._myGlobal._mainDatabase = "SMLERPMAIN".ToLower();
                MyLib._myGlobal._dataViewXmlFileName = "SMLERPView.xml".ToLower();
                MyLib._myGlobal._tableNameView = "erp_view_table".ToLower();
                MyLib._myGlobal._tableNameViewColumn = "erp_view_column".ToLower();
                MyLib._myGlobal._tableCustomNameView = "erp_view_table_custom".ToLower();
                MyLib._myGlobal._tableCustomNameViewColumn = "erp_view_column_custom".ToLower();
                MyLib._myGlobal._databaseStructFileName = "SMLDatabase.xml".ToLower();
                MyLib._myGlobal._mainDatabaseStruct = "SMLMainDatabase.xml".ToLower();
                MyLib._myGlobal._languageXmlFileName = "smllanguage.xml".ToLower();
                MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
                MyLib._myGlobal._dataViewTemplateXmlFileName = "SMLERPViewTemplate.xml".ToLower();
                MyLib._myGlobal._webServiceName = "SMLJavaWebService";
                MyLib._myGlobal._databaseVerifyXmlFileName = "smldatabaseverify.xml".ToLower();
                MyLib._myGlobal._posDesignXmlFileName = "smlposclientdesign.xml";
                _g.g._saveLog = true;


                //
                string __computerName = SystemInformation.ComputerName.ToLower();
                //
                SMLERPTemplate._main._startProgram();
                Boolean __autoLogin = MyLib._myGlobal._isDemo;

                if (__autoLogin == false)
                {
                    if (__computerName.IndexOf("jead") != -1 ||
                        __computerName.Equals("sml-pc") ||
                        __computerName.Equals("sutee-pc") ||
                        __computerName.Equals("kob2") ||
                        __computerName.Equals("asus-viroon") ||
                        __computerName.Equals("acer_viroon") ||
                        __computerName.Equals("nutt") ||
                        __computerName.Equals("fon") ||
                        __computerName.Equals("dragon") ||
                        __computerName.Equals("suphot") ||
                        __computerName.IndexOf("toe-pc") != -1 ||
                        __computerName.Equals("mozart") ||
                        __computerName.Equals("jead88-pc"))
                    {
                        MyLib._myGlobal._isUserTest = true;

                        DialogResult __select = MessageBox.Show("ต้องการเข้าทดสอบระบบ (ไม่ถามรหัสผ่าน)", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        __autoLogin = (__select == DialogResult.Yes) ? true : false;

                    }
                }

                if (__autoLogin == false && (
                        __computerName.Equals("smlmonsup-pc") ||
                        __computerName.Equals("smlmildsup-pc") ||
                        __computerName.Equals("smlkungsup-pc") ||
                        __computerName.Equals("mildkubb-pc") ||
                        __computerName.Equals("kung-pc") ||
                        __computerName.Equals("smlkungsupport-pc")
                    ))
                {
                    MyLib._myGlobal._isUserSupport = true;
                }

                if (__autoLogin)
                {
                    MyLib._myGlobal._autoLogin = true;
                    Boolean __loginSuccess = true;
                    if (__loginSuccess)
                    {
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 3; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                //case 1: getStr = "smlsoft.homeip.net"; break;
                                //case 1: getStr = "www.smlsoft.com:8083"; break;
                                //case 1: getStr = "localhost:80848"; break;
                                //case 1: getStr = "smlsoftws.homeip.net"; break;
                                // case 2: getStr = "www.gotosme.com:8080"; break;
                                case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 2: getStr = "203.170.193.84:8080"; break;
                                //case 1: getStr = "S1"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }

                        if (__autoLogin)
                        {
                            MyLib._myGlobal._compressWebservice = false;
                            MyLib._myGlobal._webServiceServerList.Clear();
                            for (int loop = 1; loop <= 3; loop++)
                            {
                                string getStr = "";
                                switch (loop)
                                {
                                    //case 1: getStr = "192.168.1.40:8080"; break;
                                    //case 1: getStr = "localhost:80"; break;
                                    case 1: getStr = "www.smlsoft.com:8080"; break;
                                    //case 2: getStr = "203.170.193.84:8080"; break;
                                    //case 1: getStr = "www.smlsoft.com:8080"; break;
                                    //case 1: getStr = "www.lannasoft.com:8089"; break;
                                }

                                MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                data._webServiceName = getStr;
                                data._webServiceConnected = false;
                                MyLib._myGlobal._webServiceServerList.Add(data);
                            }
                        }
                        //
                        MyLib._myGlobal._userCode = "Superadmin";
                        MyLib._myGlobal._userLoginSuccess = true;
                        MyLib._myGlobal._password = "superadmin";
                        MyLib._myGlobal._userName = "User FOR TEST PROGRAMS";
                        MyLib._myGlobal._branchCode = "000";
                        if (__autoLogin)
                        {
                            if (MyLib._myGlobal._OEMVersion.Length == 0)
                            {
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                                {
                                    if (MyLib._myGlobal._programName.Equals("SML TomYumGoong DEMO EN") == true)
                                    {
                                        MyLib._myGlobal._language = MyLib._languageEnum.English;
                                    }

                                    MyLib._myGlobal._providerCode = (MyLib._myGlobal._isUserTest) ? "UAT" : "DEMO1"; // "TB"; // "ICI";
                                    MyLib._myGlobal._databaseName = (MyLib._myGlobal._isUserTest) ? "UAT1" : "TOMYUMGOONGSUSHI";

                                }
                                else if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
                                {
                                    MyLib._myGlobal._providerCode = (MyLib._myGlobal._isUserTest) ? "UAT" : "DEMO1"; // "TB"; // "ICI";
                                    MyLib._myGlobal._databaseName = (MyLib._myGlobal._isUserTest) ? "UAT1" : "POSMEDDEMO";
                                }
                                else
                                {
                                    if (MyLib._myGlobal._programName.Equals("SML POS DEMO EN") == true)
                                    {
                                        MyLib._myGlobal._providerCode = "DEMO1"; // "TB"; // "ICI";
                                        MyLib._myGlobal._databaseName = "POSDEMOEN";
                                        MyLib._myGlobal._language = MyLib._languageEnum.English;
                                    }
                                    else if (MyLib._myGlobal._programName.Equals("SML POS DEMO Lao"))
                                    {
                                        MyLib._myGlobal._providerCode = "DEMO1"; // "TB"; // "ICI";
                                        MyLib._myGlobal._databaseName = "POSDEMO";
                                        MyLib._myGlobal._language = MyLib._languageEnum.Lao;
                                    }
                                    else
                                    {
                                        MyLib._myGlobal._providerCode = (MyLib._myGlobal._isUserTest) ? "UAT" : "DEMO1"; // "TB"; // "ICI";
                                        MyLib._myGlobal._databaseName = (MyLib._myGlobal._isUserTest) ? "UAT1" : "POSDEMO";
                                    }
                                }

                                if (__computerName.Equals("balls"))
                                {
                                    if (MyLib._myGlobal._isUserTest)
                                    {
                                        DialogResult __select = MessageBox.Show("UAT1=Yes,UAT7=No", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                                        if (__select == DialogResult.Yes) MyLib._myGlobal._databaseName = "UAT1";
                                        if (__select == DialogResult.No) MyLib._myGlobal._databaseName = "UAT7";
                                    }
                                }
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin";
                            }
                            else if (MyLib._myGlobal._OEMVersion.ToLower().Equals("ais"))
                            {
                                MyLib._myGlobal._webServiceServerList.Clear();
                                for (int loop = 1; loop <= 1; loop++)
                                {
                                    string getStr = "";
                                    switch (loop)
                                    {
                                        //case 1: getStr = "192.168.2.78:8084"; break;
                                        //case 1: getStr = "smlsoft.homeip.net"; break;
                                        //case 1: getStr = "www.smlsoft.com:8083"; break;
                                        //case 1: getStr = "www.smlsoft.com:8084"; break;
                                        //case 1: getStr = "www.smldemo.com:8080"; break;
                                        //case 2: getStr = "www.gotosme.com:8080"; break;
                                        //case 3: getStr = "www.smlsoft.com:8080"; break;
                                        //case 1: getStr = "www.smldatacenter.com"; break;
                                        case 1: getStr = "www.saby-paypos.com:8080"; break;
                                    }

                                    MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                    data._webServiceName = getStr;
                                    data._webServiceConnected = false;
                                    MyLib._myGlobal._webServiceServerList.Add(data);
                                }

                                // saby pay pos demo
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                                {
                                    MyLib._myGlobal._providerCode = "SABYPAYREST"; // "TB"; // "ICI";
                                    MyLib._myGlobal._databaseName = "SABYPAYRESTDEMO";
                                    MyLib._myGlobal._nonPermission = true;
                                    MyLib._myGlobal._password = "superadmin";
                                    //"SABYPAYREST"
                                }
                                else
                                {
                                    // default mpay
                                    MyLib._myGlobal._providerCode = "MPAY"; // "TB"; // "ICI";
                                    MyLib._myGlobal._databaseName = "mPayCenter";
                                    MyLib._myGlobal._nonPermission = true;
                                    MyLib._myGlobal._password = "superadmin";
                                }

                            }
                        }
                        if (__computerName.IndexOf("jead") != -1 || __computerName.Equals("fon"))
                        {
                            MyLib._myGlobal._webServiceServerList.Clear();
                            for (int loop = 1; loop <= 1; loop++)
                            {
                                string getStr = "";
                                switch (loop)
                                {
                                    //case 1: getStr = "192.168.2.78:8084"; break;
                                    //case 1: getStr = "smlsoft.homeip.net"; break;
                                    //case 1: getStr = "www.smlsoft.com:8083"; break;
                                    //case 1: getStr = "www.smlsoft.com:8084"; break;
                                    //case 1: getStr = "www.smldemo.com:8080"; break;
                                    //case 2: getStr = "www.gotosme.com:8080"; break;
                                    case 1: getStr = "www.smlsoft.com:8080"; break;
                                    //case 1: getStr = "www.smldatacenter.com"; break;
                                    //case 1: getStr = "192.168.0.250:8080"; break;
                                    //case 1: getStr = "localhost:8080"; break;
                                }

                                MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                data._webServiceName = getStr;
                                data._webServiceConnected = false;
                                MyLib._myGlobal._webServiceServerList.Add(data);
                            }
                            MyLib._myGlobal._providerCode = "DEMO1"; // "JEAD"; // "SML";// "JEAD"; // "DEMO1";
                            //MyLib._myGlobal._databaseName = "DATA1"; //  "JEAD2"; //  "POSTOMYUMKUNG";
                            MyLib._myGlobal._databaseName = "TOMYUMGOONGSUSHI";
                            MyLib._myGlobal._nonPermission = true;
                            MyLib._myGlobal._password = "superadmin";
                            /*MyLib._myGlobal._providerCode = "DATA";
                            MyLib._myGlobal._databaseName = "FOOD1";
                            MyLib._myGlobal._nonPermission = true;
                            MyLib._myGlobal._password = "superadmin";*/
                        }
                        if (__computerName.IndexOf("toe-pc") != -1 || __computerName.Equals("sutee-pc"))
                        {
                            MyLib._myGlobal._connectMySqlForResource = false;

                            DialogResult __dialogLocal = MessageBox.Show("Local Connect", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                            if (__dialogLocal == DialogResult.Yes)
                            {
                                MyLib._myGlobal._webServiceServerList.Clear();
                                for (int loop = 1; loop <= 1; loop++)
                                {
                                    string getStr = "";
                                    switch (loop)
                                    {
                                        //case 1: getStr = "smlsoft.homeip.net"; break;
                                        //case 1: getStr = "www.smlsoft.com:8083"; break;
                                        case 1: getStr = "localhost:8080"; break;
                                        // case 1: getStr = "www.smlsoft.com:8080"; break;
                                        // case 2: getStr = "www.gotosme.com:8080"; break;
                                        //case 3: getStr = "www.smlsoft.com:8080"; break;
                                        //case 1: getStr = "S1"; break;
                                    }

                                    MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                    data._webServiceName = getStr;
                                    data._webServiceConnected = false;
                                    MyLib._myGlobal._webServiceServerList.Add(data);
                                }

                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                                {
                                    MyLib._myGlobal._providerCode = "DEBUG";
                                    MyLib._myGlobal._databaseName = "TOMYUMGOONGDEBUG";
                                }
                                else
                                {
                                    MyLib._myGlobal._providerCode = "DEBUG";
                                    MyLib._myGlobal._databaseName = "DEBUG";
                                }
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin"; //  "superadmin";
                            }
                            else
                            {
                                if (MyLib._myGlobal._isDemo == false || (MessageBox.Show("debug ? ", "debug", MessageBoxButtons.YesNo) == DialogResult.Yes))
                                {

                                    MyLib._myGlobal._webServiceServerList.Clear();
                                    for (int loop = 1; loop <= 1; loop++)
                                    {
                                        string getStr = "";
                                        switch (loop)
                                        {
                                            //case 1: getStr = "smlsoft.homeip.net"; break;
                                            //case 1: getStr = "www.smlsoft.com:8083"; break;
                                            case 1: getStr = "sml1.thaiddns.com:8080"; break;
                                            // case 1: getStr = "www.smlsoft.com:8080"; break;
                                            // case 2: getStr = "www.gotosme.com:8080"; break;
                                            //case 3: getStr = "www.smlsoft.com:8080"; break;
                                            //case 1: getStr = "S1"; break;
                                        }

                                        MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                        data._webServiceName = getStr;
                                        data._webServiceConnected = false;
                                        MyLib._myGlobal._webServiceServerList.Add(data);
                                    }

                                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                                    {
                                        MyLib._myGlobal._providerCode = "DEBUG";
                                        MyLib._myGlobal._databaseName = "tomyumgoongdebug";
                                    }
                                    else
                                    {
                                        MyLib._myGlobal._providerCode = "TOEUAT";
                                        MyLib._myGlobal._databaseName = "TOEUAT";
                                    }
                                    MyLib._myGlobal._nonPermission = true;
                                    MyLib._myGlobal._password = "superadmin"; //  "superadmin";
                                }
                            }
                        }
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        __myFrameWork._webserviceServerReConnect(false);
                        MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
                        //
                        bool no_guid = true;
                        MyLib._myGlobal._guid = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._userCode, MyLib._myGlobal._password, MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);
                        if (MyLib._myGlobal._guid.Length == 0)
                        {
                            MessageBox.Show("GUID Error");
                            no_guid = false;
                        }
                        else
                        {
                            MyLib._myGlobal._checkFirst();
                            string __guid = _g.g._logMenu(0, "", "Login");
                            //
                            if (__computerName.IndexOf("jead8") != -1)
                            {
                                /*DialogResult __select = MessageBox.Show("Renew Resource", "Renew", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                                if (__select == DialogResult.Yes)
                                {
                                    __myFrameWork._queryShort("truncate table erp_view_column");
                                    __myFrameWork._queryShort("truncate table erp_view_table");
                                    __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);
                                }*/
                            }
                            //
                            MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                            MyLib._myGlobal._mainForm = new _mainForm();
                            _g.g._checkBackup();

                            Application.Run(MyLib._myGlobal._mainForm);
                            _g.g._logMenu(2, __guid, "");
                        }
                    }
                    else
                    {
                        __autoLogin = false;
                    }
                }


                if (__autoLogin == false)
                {
                    // Login Screen
                    // หมวด Login
                    MyLib._myGlobal._guid = "SMLX";
                    MyLib._myGlobal._mainForm = new _mainForm();

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSStarter)
                    {
                        MyLib._databaseManage._userLogin __selectDatabase = new MyLib._databaseManage._userLogin();
                        Application.Run(__selectDatabase);
                    }
                    else
                    {
                        MyLib._databaseManage._selectDatabase __selectDatabase = new MyLib._databaseManage._selectDatabase();
                        Application.Run(__selectDatabase);
                    }
                    if (MyLib._myGlobal._databaseName.Length > 0)
                    {
                        // load font
                        MyLib._myGlobal._loadProgramClientConfig();


                        MyLib._myGlobal._checkFirst();
                        string __guid = _g.g._logMenu(0, "", "Login");
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                        MyLib._myGlobal._mainForm.Dispose();
                        MyLib._myGlobal._mainForm = new _mainForm();
                        _g.g._checkBackup();
                        Application.Run(MyLib._myGlobal._mainForm);
                        _g.g._logMenu(2, __guid, "");
                    }
                }
                //Application.Run(new _mainForm());
            }
        }
    }
}
