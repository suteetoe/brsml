using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;
using System.Net.Sockets;
using System.Text;
using System.Drawing;

namespace SMLAccount
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

        private static void Print(string data)
        {
            NetworkStream ns = null;
            Socket socket = null;

            IPEndPoint adresIP = new IPEndPoint(IPAddress.Parse("192.168.2.76"), 9100);


            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(adresIP);

            ns = new NetworkStream(socket);
            Encoding encode = Encoding.GetEncoding(874);
            byte[] toSend = encode.GetBytes(data);
            ns.Write(toSend, 0, toSend.Length);
            ns.Flush();
            if (ns != null)
                ns.Close();
            if (socket != null && socket.Connected)
                socket.Close();
        }
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
            AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs args)
            {
                //MyLib._myGlobal._writeEventLog(__ex.Message.ToString());
                Exception e = (Exception)args.ExceptionObject;
                MyLib._myGlobal._writeEventLog("Unhandled exception: " + e.ToString());
                Environment.Exit(1);
            };

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
                        MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLAccount;
                    }

                    if (MyLib._myGlobal._programName.Equals("SML Account DEMO EN") || MyLib._myGlobal._programName.Equals("SML Account DEMO Lao") || MyLib._myGlobal._programName.Equals("SML Account Pro DEMO Lao"))
                    {
                        //MyLib._myGlobal._language = MyLib._languageEnum.English;
                    }
                    else
                    {
                        MyLib._myGlobal._language = MyLib._languageEnum.Thai;
                    }

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

                    //singha 
                    if (MyLib._myGlobal._programName.Equals("SML BR Account POS"))
                    {
                        MyLib._myGlobal._profileFileName = "SMLBRACCOUNTLogin.XML".ToLower();
                    }

                    SMLERPTemplate._main._startProgram();

                    if (MyLib._myGlobal._programName.Equals("SML CM") || MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        MyLib._myGlobal._isAutoVerify = false;
                    }

                    Boolean __autoLogin = MyLib._myGlobal._isDemo;
                    if (__autoLogin == false)
                    {
                        if (__computerName.IndexOf("jead") != -1 ||
                            __computerName.IndexOf("f-o-n") != -1 ||
                            __computerName.Equals("sml-pc") ||
                            __computerName.IndexOf("sml-dev-pc") != -1 ||
                            // __computerName.Equals("sutee-pc") ||
                            //__computerName.Equals("kob2") ||
                            __computerName.Equals("asus-viroon") ||
                            __computerName.Equals("acer_viroon") ||
                            __computerName.Equals("somkidsml-pc") ||
                            // __computerName.Equals("suphot") ||
                            __computerName.IndexOf("toe-pc") != -1 ||
                            __computerName.Equals("desktop-3j5lcil")
                            // __computerName.Equals("mozart"))
                            )
                        {
                            MyLib._myGlobal._isUserTest = true;
                            if (__computerName.IndexOf("xjead") != -1 || __computerName.IndexOf("f-o-n") != -1)
                            {
                                __autoLogin = true;
                            }
                            else
                            {
                                DialogResult __select = MessageBox.Show("ต้องการเข้าทดสอบระบบ (ไม่ถามรหัสผ่าน)", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                                __autoLogin = (__select == DialogResult.Yes) ? true : false;
                            }
                            MyLib._myGlobal._connectMySqlForResource = false;

                
                            
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
                                        //case 1: getStr = "smlsoft.homeip.net"; break;
                                        //case 1: getStr = "www.smlsoft.com:8083"; break;
                                        //case 1: getStr = "localhost:80848"; break;
                                        //case 1: getStr = "smlsoftws.homeip.net"; break;
                                        // case 2: getStr = "www.gotosme.com:8080"; break;
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
                                        //case 1: getStr = "www.smldatacenter.com"; break;
                                        case 1: getStr = "www.smlsoft.com:8080"; break;
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
                                MyLib._myGlobal._providerCode = (MyLib._myGlobal._isUserTest) ? "UAT" : "DEMO1"; // "TB"; // "ICI";
                                MyLib._myGlobal._databaseName = (MyLib._myGlobal._isUserTest) ? "UAT1" : "DEMO001";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin";
                            }

                            if (MyLib._myGlobal._programName.Equals("Account POS Singha"))
                            {
                                MyLib._myGlobal._webServiceServerList.Clear();

                                MyLib._myWebserviceType __data = new MyLib._myWebserviceType();
                                __data._webServiceName = MyLib._myGlobal._compileWebserviceName("www.smldatacenter.com:8080");
                                __data._webServiceConnected = false;
                                MyLib._myGlobal._webServiceServerList.Add(__data);

                                MyLib._myGlobal._providerCode = "SINGHA";
                                MyLib._myGlobal._databaseName = "SINGHA";

                            }

                            if (__computerName.IndexOf("jead") != -1 || __computerName.IndexOf("f-o-n") != -1 || __computerName.IndexOf("s2") != -1)
                            {
                                MyLib._myGlobal._webServiceServerList.Clear();
                                for (int loop = 1; loop <= 5; loop++)
                                {
                                    string __getStr = "";
                                    switch (loop)
                                    {
                                        //case 1: __getStr = "110.164.209.169:8008"; break;
                                        // case 1: __getStr = "www.brteasy.com:8080"; break;
                                        //case 1: __getStr = "www.smldatacenter.com:8080"; break;
                                        //case 1: __getStr = "www.smldatacenter.com"; break;
                                        //case 1: __getStr = "taveepanit.thaiddns.com:8080"; break;
                                        //case 1: __getStr = "@taveeserver,taveeemrmode,8080"; break;
                                        //case 2: __getStr = "www.smlsoft.com:8080"; break;
                                        case 1: __getStr = "localhost:8084"; break;
                                            //case 1: __getStr = "192.168.2.47:8080"; break;
                                            //case 1: getStr = "www.smlsoft.com:8083"; break;
                                            //case 1: __getStr = "www.smlsoft.com:8080"; break;
                                            // case 2: getStr = "www.gotosme.com:8080"; break;
                                            //case 1: getStr = "s2:8084"; break;
                                            /*case 1: __getStr = "suriwongonline3.dyndns.org:8080"; break;
                                            case 2: __getStr = "suriwongonline4.dyndns.org:8080"; break;
                                            case 3: __getStr = "suriwongonline.dyndns.org:8080"; break;
                                            case 4: __getStr = "suriwongonline2.dyndns.org:8080"; break;*/
                                    }
                                    if (__getStr.Length > 0)
                                    {
                                        MyLib._myWebserviceType __data = new MyLib._myWebserviceType();
                                        __data._webServiceName = MyLib._myGlobal._compileWebserviceName(__getStr);
                                        __data._webServiceConnected = false;
                                        MyLib._myGlobal._webServiceServerList.Add(__data);
                                    }
                                }

                                /*MyLib._myGlobal._providerCode = "DATA";// "UAT"; // "sql";
                                MyLib._myGlobal._dataGroup = "SML";
                                MyLib._myGlobal._databaseName = "DATA1"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._userCode = "superadmin"; // "029649744"; //"1"; //  
                                MyLib._myGlobal._password = "superadmin"; // "029649744"; //"1"; //  
                                */
                                /*MyLib._myGlobal._providerCode = "7777";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "A88"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._userCode = "superadmin"; // "029649744"; //"1"; //  
                                MyLib._myGlobal._password = "superadmin"; // "029649744"; //"1"; //  */

                                /*MyLib._myGlobal._providerCode = "DEBUG";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "BEER"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin"; // "029649744"; //"1"; //  */

                                MyLib._myGlobal._providerCode = "MITRBR"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._dataGroup = "SML";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._userCode = "adminsml";
                                MyLib._myGlobal._databaseName = "RBR2015"; // "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._password = "sml123"; // "1111"; // "029649744"; //8 "029649744"; //"1"; //  

                                MyLib._myGlobal._providerCode = "MITRSM"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._dataGroup = "SML";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._userCode = "adminsml";
                                MyLib._myGlobal._databaseName = "rsm2016"; // "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._password = "sml123"; // "1111"; // "029649744"; //8 "029649744"; //"1"; //  

                                MyLib._myGlobal._providerCode = "JEAD"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._dataGroup = "SML";
                                MyLib._myGlobal._databaseName = "TEST1"; // "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin"; // "1111"; // "029649744"; //8 "029649744"; //"1"; //  

                                MyLib._myGlobal._providerCode = "SML"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "ubon1"; // "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._userCode = "superadmin";
                                MyLib._myGlobal._password = "superadmin"; // "1111"; // "029649744"; //8 "029649744"; //"1"; //  

                                /*MyLib._myGlobal._providerCode = "SINGHA"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "SINGHA"; // "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._dataGroup = "SML";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._userCode = "superadmin";
                                MyLib._myGlobal._password = "superadmin"; // "1111"; // "029649744"; //8 "029649744"; //"1"; //  */

                                /*MyLib._myGlobal._providerCode = "UAT"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "PRO001TEST"; // "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "029649744"; // "1111"; // "029649744"; // "029649744"; //"1"; //  */


                                /*MyLib._myGlobal._providerCode = "UAT"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "PRO001TEST"; //"PCAIR4"; // "UAT1"; //  "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "029649744"; // "1111"; // "029649744"; // "029649744"; //"1"; //  
                                */
                                //

                                // TEST 
                                /*MyLib._myGlobal._providerCode = "UAT"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "TVP1"; //  "TVP"; // "TESTPROMOTION"; // "UAT1"; //  "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "029649744"; // "1111"; // "029649744"; // "029649744"; //"1"; //  */

                                // ทวี 1 (server smlsoft)
                                /*MyLib._myGlobal._providerCode = "DATA"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "TVP"; // "TESTPROMOTION"; // "UAT1"; //  "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "superadmin"; // "1111"; // "029649744"; // "029649744"; //"1"; //  */
                                // ทวี 2
                                /*MyLib._myGlobal._providerCode = "DATA"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "DATA1"; // "TESTPROMOTION"; // "UAT1"; //  "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "supermario"; // "1111"; // "029649744"; // "029649744"; //"1"; //  */

                                /*MyLib._myGlobal._providerCode = "TEST"; // "BOOKCENTER"; // "UAT";// "UAT"; // "sql";
                                MyLib._myGlobal._databaseName = "T1"; // "UAT1"; //  "SBC002"; //"KK";// "SBC003"; // "UAT1"; // "sqltest1";
                                MyLib._myGlobal._nonPermission = true;
                                MyLib._myGlobal._password = "123"; // "1111"; // "029649744"; // "029649744"; //"1"; //                                  
                                 * */
                            }

                            if ((__computerName.IndexOf("toe-pc") != -1 || __computerName.ToLower().Equals("sutee-pc")) && MyLib._myGlobal._isDemo == false)
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

                                    if (MyLib._myGlobal._OEMVersion == "imex")
                                    {
                                        MyLib._myGlobal._providerCode = "DEBUG";
                                        MyLib._myGlobal._databaseName = "TOC";
                                    }
                                    else if (MyLib._myGlobal._OEMVersion == "SINGHA")
                                    {
                                        MyLib._myGlobal._providerCode = "DEBUG";
                                        MyLib._myGlobal._databaseName = "BS";

                                    }
                                    else if (MyLib._myGlobal._programName == "SML CM")
                                    {
                                        MyLib._myGlobal._providerCode = "DEBUG";
                                        MyLib._myGlobal._databaseName = "JJ";

                                    }
                                    else
                                    {
                                        MyLib._myGlobal._providerCode = "DEBUG";
                                        MyLib._myGlobal._databaseName = "GREENLATEX";
                                    }
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
                                                //case 1: getStr = "smlsoft.homeip.net"; break;
                                                //case 1: getStr = "www.smlsoft.com:8083"; break;
                                                case 1: getStr = "sml1.thaiddns.com:8081"; break;
                                                    //case 1: getStr = "www.smlsoft.com:8080"; break;
                                                    // case 2: getStr = "www.gotosme.com:8080"; break;
                                                    //case 3: getStr = "www.smlsoft.com:8080"; break;
                                                    //case 1: getStr = "S1"; break;
                                            }

                                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                            data._webServiceName = getStr;
                                            data._webServiceConnected = false;
                                            MyLib._myGlobal._webServiceServerList.Add(data);
                                        }
                                        MyLib._myGlobal._providerCode = "SMLMANUAL";
                                        MyLib._myGlobal._databaseName = "SMLMANUAL1";
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
                                if (__computerName.IndexOf("jead") != -1 || __computerName.IndexOf("f-o-n") != -1 || __computerName.IndexOf("s2") != -1)
                                {
                                    MyLib._myGlobal._mainForm.WindowState = FormWindowState.Normal;
                                    MyLib._myGlobal._mainForm.Location = new System.Drawing.Point(0, 0);
                                }
                                if (__autoLogin == false)
                                {
                                    _g.g._checkBackup();
                                }
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
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                        {
                            // MyLib._myGlobal._isAutoVerify = false;
                            MyLib._myGlobal._getRemoteAddrURL = "";
                        }

                        // Login Screen
                        // หมวด Login
                        MyLib._myGlobal._guid = "SMLX";
                        MyLib._myGlobal._mainForm = new _mainForm();
                        MyLib._databaseManage._selectDatabase __selectDatabase = new MyLib._databaseManage._selectDatabase();
                        Application.Run(__selectDatabase);
                        if (MyLib._myGlobal._databaseName.Length > 0)
                        {
                            // check row count first
                            
                            _g.g._companyProfileLoad(false);

                            // load font
                            MyLib._myGlobal._loadProgramClientConfig();

                            MyLib._myGlobal._checkFirst();
                            string __guid = _g.g._logMenu(0, "", "Login");
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                            MyLib._myGlobal._mainForm.Dispose();
                            MyLib._myGlobal._mainForm = new _mainForm();
                            if (MyLib._myGlobal._OEMVersion.Length == 0 && _g.g._companyProfile._close_warning_backup == false)
                            {
                                _g.g._checkBackup();
                            }

                            //

                            if (MyLib._myGlobal._programName.Equals("SML CM"))
                            {
                                // select branch
                                if (_g.g._companyProfile._change_branch_code)
                                {
                                    // popup select branch
                                    MyLib._selectBranchForm __selectBranchForm = new MyLib._selectBranchForm();
                                    if (__selectBranchForm.ShowDialog() == DialogResult.Yes)
                                    {
                                        MyLib._myGlobal._fixBranchCode = __selectBranchForm._selectBranch;
                                        MyLib._myGlobal._fixBranchName = __selectBranchForm._selectBranchName;
                                        MessageBox.Show("Select Branch : " + MyLib._myGlobal._fixBranchName + "(" + MyLib._myGlobal._fixBranchCode + ")");
                                    }

                                }
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
