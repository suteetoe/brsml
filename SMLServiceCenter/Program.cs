using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Net;
using System.Threading;

namespace SMLServiceCenter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Environment.Version.Major < 2)
            {
                MessageBox.Show("This program need FrameWork 2.0 up, if your computer have frame work 1.1 please remove first.");
            }
            else
            {
                //MyLib._myGlobal._isVersion = "SMLERP";
                MyLib._myGlobal._language = MyLib._languageEnum.Thai;
                MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLServiceCenter;

                // Moo ย้ายมาจาก SMLERPTemplate._main
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                MyLib._myGlobal._computerName = Dns.GetHostName();
                MyLib._myGlobal._year_type = 1;
                MyLib._myGlobal._year_add = 543;
                MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
                MyLib._myGlobal._workingDate = DateTime.Now;
                //SMLERPTemplate._main._startProgram();

                MyLib._myGlobal._programName = "SML Service Center";
                MyLib._myGlobal._profileFileName = "SMLServiceCenterLogin.XML".ToLower();
                // MyLib._myGlobal._profileFileName = "SMLERPLogin.XML".ToLower();                
                MyLib._myGlobal._mainDatabase = "SMLERPMAIN".ToLower();
                MyLib._myGlobal._dataViewXmlFileName = "SMLERPView.xml".ToLower();
                MyLib._myGlobal._tableNameView = "erp_view_table".ToLower();
                MyLib._myGlobal._tableNameViewColumn = "erp_view_column".ToLower();
                MyLib._myGlobal._databaseStructFileName = "SMLDatabase.xml".ToLower();
                MyLib._myGlobal._mainDatabaseStruct = "SMLMainDatabase.xml".ToLower();
                MyLib._myGlobal._languageXmlFileName = "smllanguage.xml".ToLower();
                MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
                MyLib._myGlobal._dataViewTemplateXmlFileName = "SMLERPViewTemplate.xml".ToLower();
                MyLib._myGlobal._webServiceName = "SMLJavaWebService";
                _g.g._saveLog = true;
                //
                string __computerName = SystemInformation.ComputerName.ToLower();
                //
                SMLERPTemplate._main._startProgram();
                Boolean __autoLogin = MyLib._myGlobal._isDemo;
                if (__autoLogin == false)
                {
                    if (__computerName.Equals("jead9") ||
                        __computerName.Equals("sml-pc") ||
                        __computerName.Equals("kob-pc") ||
                        __computerName.Equals("kob2") ||
                        __computerName.Equals("asus-viroon") ||
                        __computerName.Equals("acer_viroon") ||
                        __computerName.Equals("nutt") ||
                        __computerName.Equals("toe-pc") ||
                            __computerName.Equals("somruk") ||
                        __computerName.Equals("mozart"))
                    {
                        DialogResult __select = MessageBox.Show("ต้องการเข้าทดสอบระบบ (ไม่ถามรหัสผ่าน)", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        __autoLogin = (__select == DialogResult.Yes) ? true : false;
                    }
                }
                if (__autoLogin)
                {
                    MyLib._myGlobal._autoLogin = true;
                    Boolean __loginSuccess = true;
                    if (__loginSuccess)
                    {
                        //MyLib._myGlobal._webServiceServerList.Clear();
                        //for (int loop = 1; loop <= 3; loop++)
                        //{
                        //    string getStr = "";
                        //    switch (loop)
                        //    {
                        //        //case 1: getStr = "smlsoft.homeip.net"; break;
                        //        //case 1: getStr = "www.smlsoft.com:8083"; break;
                        //        //case 1: getStr = "localhost:80848"; break;
                        //        //case 1: getStr = "smlsoftws.homeip.net"; break;
                        //        // case 2: getStr = "www.gotosme.com:8080"; break;
                        //        case 3: getStr = "www.smlsoft.com:8080"; break;
                        //        //case 1: getStr = "S1"; break;
                        //    }

                        //    MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                        //    data._webServiceName = getStr;
                        //    data._webServiceConnected = false;
                        //    MyLib._myGlobal._webServiceServerList.Add(data);
                        //}

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
                                    //case 1: getStr = "www.smlsoft.com:8084"; break;
                                    case 1: getStr = "localhost:8080"; break;
                                    //case 1: getStr = "www.lannasoft.com:8089"; break;
                                }

                                MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                data._webServiceName = getStr;
                                data._webServiceConnected = false;
                                MyLib._myGlobal._webServiceServerList.Add(data);
                            }
                        }
                        //
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        __myFrameWork._webserviceServerReConnect(false);
                        //__myFrameWork._webserviceServerReConnect(true);
                        MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
                        MyLib._myGlobal._userCode = "Superadmin";
                        MyLib._myGlobal._userLoginSuccess = true;
                        MyLib._myGlobal._password = "superadmin";
                        MyLib._myGlobal._userName = "User FOR TEST PROGRAMS";
                        MyLib._myGlobal._branchCode = "000";
                        if (__autoLogin)
                        {
                            MyLib._myGlobal._providerCode = "ICI"; // "TB"; // "ICI";
                            MyLib._myGlobal._databaseName = "ICIDEMO"; // "tb1";// "test7"; // "ICIDEMO";//"icitest"
                            MyLib._myGlobal._nonPermission = true;
                            MyLib._myGlobal._password = "superadmin";
                        }
                        bool no_guid = true;
                        MyLib._myGlobal._guid = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._userCode, MyLib._myGlobal._password, MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);
                        if (MyLib._myGlobal._guid.Length == 0)
                        {
                            MessageBox.Show("GUID Error");
                            no_guid = false;
                        }
                        string __guid = _g.g._logMenu(0, "", "Login");
                        //
                        if (__computerName.Equals("jead9"))
                        {
                            DialogResult __select = MessageBox.Show("Renew Resource", "Renew", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                            if (__select == DialogResult.Yes)
                            {
                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_column");
                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "truncate table erp_view_table");
                                __myFrameWork._sendXmlFile(MyLib._myGlobal._dataViewTemplateXmlFileName);
                            }
                        }
                        //
                        MyLib._myGlobal._mainForm = new _mainForm();
                        Application.Run(MyLib._myGlobal._mainForm);
                        _g.g._logMenu(2, __guid, "");
                    }
                    else
                    {
                        __autoLogin = false;
                    }
                }
                if (__autoLogin == false)
                {
                    // Login Screen
                    //หมวด Login
                    MyLib._myGlobal._guid = "SMLX";
                    MyLib._myGlobal._mainForm = new _mainForm();
                    MyLib._databaseManage._selectDatabase __selectDatabase = new MyLib._databaseManage._selectDatabase();
                    Application.Run(__selectDatabase);
                    if (MyLib._myGlobal._databaseName.Length > 0)
                    {
                        string __guid = _g.g._logMenu(0, "", "Login");
                        MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                        MyLib._myResource._resource = myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                        MyLib._myGlobal._mainForm.Dispose();
                        MyLib._myGlobal._mainForm = new _mainForm();
                        Application.Run(MyLib._myGlobal._mainForm);
                        _g.g._logMenu(2, __guid, "");
                    }
                }
            }
        }
    }
}
