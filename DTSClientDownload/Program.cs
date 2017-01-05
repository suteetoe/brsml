using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;
using System.Globalization;
using System.Reflection;

namespace DTSClientDownload
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
            //Application.Run(new Form1());

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
                MyLib._myGlobal._isDesignMode = false;
                //MyLib._myGlobal._isVersion = "SMLERP";
                MyLib._myGlobal._language = MyLib._languageEnum.Thai;
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.Null)
                {
                    MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLAccount;
                }

                // Moo ย้ายมาจาก SMLERPTemplate._main
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                MyLib._myGlobal._computerName = WindowsIdentity.GetCurrent().Name;
                MyLib._myGlobal._year_type = 1;
                MyLib._myGlobal._year_add = 543;
                MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
                MyLib._myGlobal._workingDate = DateTime.Now;
                MyLib._myGlobal._programName = "SCG Download";

                MyLib._myGlobal._profileFileName = "DTSClientLogin.XML".ToLower();
                MyLib._myGlobal._mainDatabase = "SMLERPMAIN".ToLower();
                MyLib._myGlobal._dataViewXmlFileName = "SMLERPView.xml".ToLower();
                MyLib._myGlobal._tableNameView = "erp_view_table".ToLower();
                MyLib._myGlobal._tableNameViewColumn = "erp_view_column".ToLower();
                MyLib._myGlobal._tableCustomNameView = "erp_view_table_custom".ToLower();
                MyLib._myGlobal._tableCustomNameViewColumn = "erp_view_column_custom".ToLower();
                MyLib._myGlobal._databaseStructFileName = "DTSDatabase.xml".ToLower();
                MyLib._myGlobal._mainDatabaseStruct = "SMLMainDatabase.xml".ToLower();
                MyLib._myGlobal._languageXmlFileName = "smllanguage.xml".ToLower();
                MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
                MyLib._myGlobal._dataViewTemplateXmlFileName = "SMLERPViewTemplate.xml".ToLower();
                MyLib._myGlobal._webServiceName = "SMLJavaWebService";
                MyLib._myGlobal._databaseVerifyXmlFileName = "smldatabaseverify.xml".ToLower();

                CultureInfo __ci = new CultureInfo("th-TH");// CultureInfo.CurrentCulture;
                if (__ci.DateTimeFormat.ShortDatePattern.Equals("d/M/yyyy") == false &&
                    __ci.DateTimeFormat.ShortDatePattern.Equals("dd/MM/yyyy") == false)
                {
                    MessageBox.Show("Date format Error Please select d/M/yyyy or dd/MM/yyyy : Control Panel -> Regional");
                    Environment.Exit(1);
                }
                //
                string __computerName = SystemInformation.ComputerName.ToLower();
                //
                SMLERPTemplate._main._startProgram();
                MyLib._getStream._getStreamEvent -= new MyLib._getStreamEventHandler(_getStream__getStreamEvent);
                MyLib._getStream._getStreamEvent += new MyLib._getStreamEventHandler(_getStream__getStreamEvent);

                Boolean __autoLogin = MyLib._myGlobal._isDemo;
                MyLib._myGlobal._providerCode = "DTS";
                MyLib._myGlobal._databaseName = "DTSOutBound";
                MyLib._myGlobal._addUpperDatebaseName = false;
                MyLib._myGlobal._mainDatabasePOSStarter = MyLib._myGlobal._databaseName;

                _clientFrameWork __clientFrameWork = new _clientFrameWork();
                __clientFrameWork._clearLog();

                if (__autoLogin == false)
                {
                    //if (__computerName.IndexOf("jead") != -1 ||
                    //    __computerName.Equals("sml-pc") ||
                    //    __computerName.Equals("s2") ||
                    //    __computerName.Equals("asus-viroon") ||
                    //    __computerName.Equals("acer_viroon") ||
                    //    __computerName.Equals("toe-pc") ||
                    //    __computerName.Equals("jead88-pc"))
                    if (__computerName.IndexOf("toe-pc") != -1)
                    {
                        DialogResult __select = MessageBox.Show("ต้องการเข้าทดสอบระบบ (ไม่ถามรหัสผ่าน)", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        __autoLogin = (__select == DialogResult.Yes) ? true : false;
                        //__autoLogin = true;

                        //MyLib._myGlobal._webServiceServerList.Clear();
                        //for (int loop = 1; loop <= 1; loop++)
                        //{
                        //    string getStr = "";
                        //    switch (loop)
                        //    {
                        //        case 1: getStr = "sml1.thaiddns.com:8080"; break;
                        //        // case 1: getStr = "www.smlsoft.com:8080"; break;
                        //        // case 2: getStr = "www.gotosme.com:8080"; break;
                        //        //case 3: getStr = "www.smlsoft.com:8080"; break;
                        //        //case 1: getStr = "S1"; break;
                        //    }

                        //    MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                        //    data._webServiceName = getStr;
                        //    data._webServiceConnected = false;
                        //    MyLib._myGlobal._webServiceServerList.Add(data);
                        //}

                        MyLib._myGlobal._nonPermission = true;
                        MyLib._myGlobal._userCode = "001"; //  "superadmin";

                    }
                }

                if (__autoLogin)
                {
                    if (__computerName.IndexOf("toe-pc") != -1)
                    {
                        // local connect
                        DialogResult __dialogLocal = MessageBox.Show("Local Connect", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        if (__dialogLocal == DialogResult.Yes)
                        {
                            //MyLib._myGlobal._webServiceServerList.Clear();
                            //for (int loop = 1; loop <= 1; loop++)
                            //{
                            //    string getStr = "";

                            //    // ตอนเอาไปใช้จริง ต้องใส่ มากกว่า 2 line กันหลุด
                            //    switch (loop)
                            //    {
                            //        case 1: getStr = "localhost:8080"; break;
                            //        // case 1: getStr = "www.smlsoft.com:8080"; break;
                            //        // case 2: getStr = "www.gotosme.com:8080"; break;
                            //        //case 3: getStr = "www.smlsoft.com:8080"; break;
                            //        //case 1: getStr = "S1"; break;
                            //    }

                            //    MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            //    data._webServiceName = getStr;
                            //    data._webServiceConnected = false;
                            //    MyLib._myGlobal._webServiceServerList.Add(data);
                            //}

                            MyLib._myGlobal._nonPermission = true;
                            MyLib._myGlobal._userCode = "001"; //  "superadmin";

                            _global._champServer = "TOE-PC2";
                            _global._champUserName = "sa";
                            _global._champPassword = "sml";
                            _global._champDatabaseName = "kks54";


                        }
                    }

                    // สั่ง login

                    //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    //__myFrameWork._webserviceServerReConnect(false);
                    //MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
                    ////
                    //bool no_guid = true;


                    //string __guid = _g.g._logMenu(0, "", "Login");
                    ////
                    //MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);

                    _global._getServerConnect();

                    MyLib._myGlobal._mainForm = new _dtsMain();
                    Application.Run(MyLib._myGlobal._mainForm);
                    //_g.g._logMenu(2, __guid, "");

                }
                else
                {
                    // Mode login ปรกติ
                    // lock web service ไว้

                    //MyLib._myGlobal._webServiceServerList.Clear();
                    //for (int loop = 1; loop <= 1; loop++)
                    //{
                    //    string getStr = "";
                    //    switch (loop)
                    //    {
                    //        case 1: getStr = "sml1.thaiddns.com:8080"; break;
                    //        // case 1: getStr = "www.smlsoft.com:8080"; break;
                    //        // case 2: getStr = "www.gotosme.com:8080"; break;
                    //        //case 3: getStr = "www.smlsoft.com:8080"; break;
                    //        //case 1: getStr = "S1"; break;
                    //    }

                    //    MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                    //    data._webServiceName = getStr;
                    //    data._webServiceConnected = false;
                    //    MyLib._myGlobal._webServiceServerList.Add(data);
                    //}

                }
                // lock web service url

                if (__autoLogin == false)
                {
                    // change server 
                    //if (__computerName.IndexOf("toe") != -1 && (MessageBox.Show("Connect Localhost Server", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                    //{
                        //MyLib._myGlobal._webServiceServerList.Clear();
                        //for (int loop = 1; loop <= 1; loop++)
                        //{
                        //    string getStr = "";

                        //    // ตอนเอาไปใช้จริง ต้องใส่ มากกว่า 2 line กันหลุด
                        //    switch (loop)
                        //    {
                        //        case 1: getStr = "localhost:8080"; break;
                        //        // case 1: getStr = "www.smlsoft.com:8080"; break;
                        //        // case 2: getStr = "www.gotosme.com:8080"; break;
                        //        //case 3: getStr = "www.smlsoft.com:8080"; break;
                        //        //case 1: getStr = "S1"; break;
                        //    }

                        //    MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                        //    data._webServiceName = getStr;
                        //    data._webServiceConnected = false;
                        //    MyLib._myGlobal._webServiceServerList.Add(data);
                        //}
                    //}

                    // Login Screen
                    // หมวด Login
                    MyLib._myGlobal._guid = "SMLX";
                    //MyLib._myGlobal._mainForm = new _dtsMain(); // _mainForm();

                    _dtsLogin __selectDatabase = new _dtsLogin();

                    Application.Run(__selectDatabase);
                    if (MyLib._myGlobal._databaseName.Length > 0 && MyLib._myGlobal._userLoginSuccess)
                    {
                        //MyLib._myGlobal._checkFirst();
                        string __guid = _g.g._logMenu(0, "", "Login");

                        //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        //MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                        //MyLib._myGlobal._mainForm.Dispose();

                        MyLib._myGlobal._mainForm = new _dtsMain();
                        Application.Run(MyLib._myGlobal._mainForm);

                        //_g.g._logMenu(2, __guid, "");
                    }
                }

            }
        }

        static System.IO.Stream _getStream__getStreamEvent(string xmlFileName)
        {
            Assembly __thisAssembly = Assembly.GetExecutingAssembly();
            return __thisAssembly.GetManifestResourceStream("DTSClientDownload." + xmlFileName);

        }
    }
}
