using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SMLGeneralLedger
{
    static class Program
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
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new _mainForm());

            CoInternetSetFeatureEnabled(
                FEATURE_DISABLE_NAVIGATION_SOUNDS,
                SET_FEATURE_ON_PROCESS,
                true);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //_hookID = SetHook(_proc);
            if (Environment.Version.Major < 2)
            {
                MessageBox.Show("This program need FrameWork 2.0 up, if your computer have frame work 1.1 please remove first.");
            }
            else
            {
                try
                {
                    MyLib._myGlobal._isDesignMode = false;
                    //MyLib._myGlobal._isVersion = "SMLERP";
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.Null)
                    {
                        MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLGeneralLedger;
                    }

                        MyLib._myGlobal._language = MyLib._languageEnum.Thai;

                    // Moo ย้ายมาจาก SMLERPTemplate._main
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    MyLib._myGlobal._computerName = WindowsIdentity.GetCurrent().Name;
                    MyLib._myGlobal._year_type = 1;
                    MyLib._myGlobal._year_add = 543;
                    MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
                    MyLib._myGlobal._workingDate = DateTime.Now;
                    //SMLERPTemplate._main._startProgram();
                    switch (MyLib._myGlobal._isVersionEnum)
                    {
                        case MyLib._myGlobal._versionType.SMLAccount:
                            {
                                if (MyLib._myGlobal._programName.Length == 0)
                                    MyLib._myGlobal._programName = "SML Account";
                            }
                            break;
                        case MyLib._myGlobal._versionType.SMLAccountProfessional:
                            if (MyLib._myGlobal._programName.Length == 0)
                                MyLib._myGlobal._programName = "SML Account Professional";
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
                        if (__computerName.IndexOf("jead8") != -1 ||
                            __computerName.IndexOf("jead7") != -1 ||
                            __computerName.Equals("sml-pc") ||
                            __computerName.Equals("s2") ||
                            __computerName.Equals("kob-pc") ||
                            __computerName.Equals("kob2") ||
                            __computerName.Equals("asus-viroon") ||
                            __computerName.Equals("acer_viroon") ||
                            __computerName.Equals("somkidsml-pc") ||
                            __computerName.Equals("dragon") ||
                            __computerName.Equals("suphot") ||
                            __computerName.IndexOf("toe-pc") != -1 ||
                            __computerName.Equals("mozart") ||
                            __computerName.Equals("jead88-pc"))
                        {
                            MyLib._myGlobal._isUserTest = true;
                            if (__computerName.IndexOf("jead") != -1)
                            {
                                __autoLogin = true;
                            }
                            else
                            {
                                DialogResult __select = MessageBox.Show("ต้องการเข้าทดสอบระบบ (ไม่ถามรหัสผ่าน)", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                                __autoLogin = (__select == DialogResult.Yes) ? true : false;
                            }
                            MyLib._myGlobal._connectMySqlForResource = true;

                            //__autoLogin = true;
                        }
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
                                    case 1: getStr = "www.smldatacenter.com"; break;
                                    case 2: getStr = "www.smlsoft.com:8080"; break;
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
                                        case 1: getStr = "www.smldatacenter.com"; break;
                                        case 2: getStr = "www.smlsoft.com:8080"; break;
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
                                MyLib._myGlobal._providerCode = (MyLib._myGlobal._isUserTest) ? "UAT" : "DEMO1"; // "TB"; // "ICI";
                                MyLib._myGlobal._databaseName = (MyLib._myGlobal._isUserTest) ? "UAT1" : "DEMO001";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin";
                            }
                            if (__computerName.IndexOf("jead8") != -1 || __computerName.IndexOf("jead7") != -1 || __computerName.IndexOf("s2") != -1)
                            {
                                MyLib._myGlobal._webServiceServerList.Clear();
                                for (int loop = 1; loop <= 5; loop++)
                                {
                                    string __getStr = "";
                                    switch (loop)
                                    {
                                        case 2: __getStr = "www.smlsoft.com:8080"; break;
                                    }
                                    if (__getStr.Length > 0)
                                    {
                                        MyLib._myWebserviceType __data = new MyLib._myWebserviceType();
                                        __data._webServiceName = MyLib._myGlobal._compileWebserviceName(__getStr);
                                        __data._webServiceConnected = false;
                                        MyLib._myGlobal._webServiceServerList.Add(__data);
                                    }
                                }


                                MyLib._myGlobal._providerCode = "JEAD"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "JEAD2"; // "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin"; // "1111"; // "029649744"; // "029649744"; //"1"; //  */

                            }

                            if (__computerName.IndexOf("toe-pc") != -1)
                            {
                                DialogResult __dialogLocal = MessageBox.Show("Local Connect", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                                if (__dialogLocal == DialogResult.Yes)
                                {
                                    MyLib._myGlobal._webServiceServerList.Clear();
                                    for (int loop = 1; loop <= 1; loop++)
                                    {
                                        string getStr = "";
                                        switch (loop)
                                        {
                                            case 1: getStr = "localhost:8080"; break;
                                        }

                                        MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                        data._webServiceName = getStr;
                                        data._webServiceConnected = false;
                                        MyLib._myGlobal._webServiceServerList.Add(data);
                                    }
                                    MyLib._myGlobal._providerCode = "DEBUG";
                                    MyLib._myGlobal._databaseName = "GREENLATEX";
                                    MyLib._myGlobal._nonPermission = true;
                                    MyLib._myGlobal._password = "superadmin"; //  "superadmin";
                                }
                                else
                                {
                                    if (MyLib._myGlobal._isDemo == false)
                                    {
                                        MyLib._myGlobal._webServiceServerList.Clear();
                                        for (int loop = 1; loop <= 1; loop++)
                                        {
                                            string getStr = "";
                                            switch (loop)
                                            {
                                                case 1: getStr = "www.smlsoft.com:8080"; break;
                                            }

                                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                            data._webServiceName = getStr;
                                            data._webServiceConnected = false;
                                            MyLib._myGlobal._webServiceServerList.Add(data);
                                        }
                                        MyLib._myGlobal._providerCode = "JEAD";
                                        MyLib._myGlobal._databaseName = "JEAD1";
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
                        MyLib._databaseManage._selectDatabase __selectDatabase = new MyLib._databaseManage._selectDatabase();
                        Application.Run(__selectDatabase);
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
                            if (MyLib._myGlobal._OEMVersion.Length == 0)
                            {
                                _g.g._checkBackup();
                            }
                            Application.Run(MyLib._myGlobal._mainForm);
                            _g.g._logMenu(2, __guid, "");
                        }
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
            UnhookWindowsHookEx(_hookID);
        }
    }
}
