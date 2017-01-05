using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Net;

namespace SMLICIAdmin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyLib._myGlobal._isDemo = false;
            //MyLib._myGlobal._isVersion = "SMLERP";
           // MyLib._myGlobal._isVersion = MyLib._myGlobal._versionType.SMLColorStore.ToString();  // MooAe
            //MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLColorStore;
            MyLib._myGlobal._language = MyLib._languageEnum.Thai;
            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLColorStore;
            //MyLib._myGlobal._databaseSelectType = MyLib._myGlobal._databaseType.MicrosoftSQL2005;
            // Moo ย้ายมาจาก SMLERPTemplate._main
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            MyLib._myGlobal._computerName = Dns.GetHostName();
            MyLib._myGlobal._year_type = 1;
            MyLib._myGlobal._year_add = 543;
            MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
            MyLib._myGlobal._workingDate = DateTime.Now;
            //SMLERPTemplate._main._startProgram();

            MyLib._myGlobal._profileFileName = "SMLERPLogin.XML".ToLower();
            MyLib._myGlobal._mainDatabase = "SMLERPMAIN".ToLower();
            MyLib._myGlobal._dataViewXmlFileName = "SMLERPView.xml".ToLower();
            MyLib._myGlobal._tableNameView = "erp_view_table".ToLower();
            MyLib._myGlobal._tableNameViewColumn = "erp_view_column".ToLower();
            MyLib._myGlobal._databaseStructFileName = "SMLDatabase.xml".ToLower();
            MyLib._myGlobal._mainDatabaseStruct = "SMLMainDatabase.xml".ToLower();
            MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
            MyLib._myGlobal._dataViewTemplateXmlFileName = "SMLERPViewTemplate.xml".ToLower();
            MyLib._myGlobal._webServiceName = "SMLJavaWebService";
            _g.g._saveLog = true;

            SMLERPTemplate._main._startProgram();
            Boolean __autoLogin = false;
            string __computerName = SystemInformation.ComputerName.ToLower();
            if (__computerName.Equals("jead9") ||
                __computerName.Equals("jead88") ||
                __computerName.Equals("nutt") ||
                __computerName.Equals("pae") ||
                __computerName.Equals("night") ||
                __computerName.Equals("anek") ||
                __computerName.Equals("aew") ||
                __computerName.Equals("yai") ||
                __computerName.Equals("ning") ||
                __computerName.Equals("balls") ||
                __computerName.Equals("moo") ||
                __computerName.Equals("acer_viroon") ||
                __computerName.Equals("sml-pc") ||
                __computerName.Equals("viroon") ||
                __computerName.Equals("mozart"))
            {
                __autoLogin = true;
            }
            if (__autoLogin)
            {
                DialogResult __select = MessageBox.Show("Auto Login", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                if (__select == DialogResult.Yes)
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
                            case 3: getStr = "www.smlsoft.com:8080"; break;
                            //case 1: getStr = "S1"; break;
                        }

                        MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                        data._webServiceName = getStr;
                        data._webServiceConnected = false;
                        MyLib._myGlobal._webServiceServerList.Add(data);
                    }


                    if (__computerName.Equals("nutt") ||
                        __computerName.Equals("pae") ||
                        __computerName.Equals("night"))
                    {

                        MyLib._myGlobal._compressWebservice = true;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 1; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                //case 1: getStr = "www.smlsoft.com:8084"; break;
                                case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "s1"; break;
                                //case 1: getStr = "localhost:8084"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }

                    ///anek
                    if (__computerName.Equals("anek"))
                    {
                        MyLib._myGlobal._compressWebservice = true;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 1; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                //case 1: getStr = "smlsoft.homeip.net"; break;
                                //case 1: getStr = "www.smlsoft.com:8083"; break;
                                case 1: getStr = "localhost:8080"; break;
                                //case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "s1"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }
                    if (__computerName.Equals("jead9"))
                    {
                        MyLib._myGlobal._compressWebservice = false;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 3; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                case 1: getStr = "localhost:8084"; break;
                                //case 1: getStr = "www.smlsoft.com:8084"; break;
                                //case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "www.lannasoft.com:8089"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }
                    if (__computerName.Equals("jead88"))
                    {
                        MyLib._myGlobal._compressWebservice = false;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 3; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                //case 1: getStr = "localhost:8084"; break;
                                //case 1: getStr = "www.smlsoft.com:8084"; break;
                                //case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "www.lannasoft.com:8089"; break;
                                case 1: getStr = "smltest1.homeip.net:8080"; break;
                                case 2: getStr = "smltest2.homeip.net:8080"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }
                    if (__computerName.Equals("acer_viroon") ||
                        __computerName.Equals("sml-pc") ||
                        __computerName.Equals("viroon"))
                    {
                        MyLib._myGlobal._compressWebservice = false;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 3; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                //case 1: getStr = "smlsoft.homeip.net"; break;
                                //case 1: getStr = "www.smlsoft.com:8083"; break;
                                //case 1: getStr = "localhost:8084"; break;
                                //case 1: getStr = "smlsoftws.homeip.net"; break;
                                // case 2: getStr = "www.gotosme.com:8080"; break;
                                case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "S1"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }
                    if (__computerName.Equals("mozart"))
                    {
                        MyLib._myGlobal._compressWebservice = true;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 1; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                //case 1: getStr = "smlsoft.homeip.net"; break;
                                case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "localhost:8084"; break;
                                //case 1: getStr = "jead9:8084"; break;
                                //case 1: getStr = "s1"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }

                    //
                    if (__computerName.CompareTo("somruk") == 0)
                    {
                        MyLib._myGlobal._compressWebservice = true;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 1; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "localhost:8084"; break;
                                // case 1: getStr = "S1"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }

                    if (__computerName.Equals("moo") ||
                        __computerName.Equals("aew") ||
                        __computerName.Equals("yai") ||
                        __computerName.Equals("ning"))
                    {

                        MyLib._myGlobal._compressWebservice = true;
                        MyLib._myGlobal._webServiceServerList.Clear();
                        for (int loop = 1; loop <= 1; loop++)
                        {
                            string getStr = "";
                            switch (loop)
                            {
                                //case 1: getStr = "www.smlsoft.com:8080"; break;
                                //case 1: getStr = "jead9:8084"; break;
                                //case 1: getStr = "192.168.1.20:8180"; break;
                                case 1: getStr = "ANEK:8080"; break;
                            }

                            MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                            data._webServiceName = getStr;
                            data._webServiceConnected = false;
                            MyLib._myGlobal._webServiceServerList.Add(data);
                        }
                    }
                    //
                    // Auto Login
                    if (__computerName.Equals("nutt") ||
                        __computerName.Equals("pae") ||
                        __computerName.Equals("night") ||
                        __computerName.Equals("anek") ||
                        __computerName.Equals("aew") ||
                        __computerName.Equals("yai") ||
                        __computerName.Equals("ning") ||
                        __computerName.Equals("balls") ||
                        __computerName.Equals("moo") ||
                        __computerName.Equals("mozart"))
                    {
                        MyLib._myGlobal._providerCode = "ICI";
                        MyLib._myGlobal._databaseName = "ICIDEMO3";
                        MyLib._myGlobal._nonPermission = true;
                    }
                    else
                    {
                        MyLib._myGlobal._providerCode = "ICI";
                        MyLib._myGlobal._databaseName = "ICIDEMO";
                        MyLib._myGlobal._nonPermission = true;
                    }
                    if (__computerName.Equals("acer_viroon") ||
                        __computerName.Equals("viroon"))
                    {
                        MyLib._myGlobal._providerCode = "ICI";
                        MyLib._myGlobal._databaseName = "ICIDEMO3";
                    }

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    __myFrameWork._webserviceServerReConnect(false);
                    //__myFrameWork._webserviceServerReConnect(true);
                    MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
                    MyLib._myGlobal._userCode = "Superadmin";
                    MyLib._myGlobal._userLoginSuccess = true;
                    MyLib._myGlobal._password = "superadmin";
                    MyLib._myGlobal._userName = "User FOR TEST PROGRAMS";
                    MyLib._myGlobal._branchCode = "000";
                    if (__computerName.Equals("jead9") || __computerName.Equals("jead88"))
                    {
                        MyLib._myGlobal._providerCode = "ICI";
                        MyLib._myGlobal._databaseName = "ICIDEMO";//"icitest"
                        MyLib._myGlobal._nonPermission = true;
                        MyLib._myGlobal._password = "";
                    }
                    bool no_guid = true;
                    MyLib._myGlobal._guid = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._userCode, MyLib._myGlobal._password, MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);
                    if (MyLib._myGlobal._guid.Length == 0)
                    {
                        MessageBox.Show("GUID Error");
                        no_guid = false;
                    }
                    string __guid = _g.g._logMenu(0, "", "Login");
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
                
                //  __selectDatabase._changeLanguage += new MyLib._databaseManage.ChangeLanguageHandler(__selectDatabase__changeLanguage);
                Application.Run(__selectDatabase);
                if (MyLib._myGlobal._databaseName.Length > 0)
                {
                    MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                    myFrameWork._databaseSelectType = MyLib._myGlobal._databaseType.MicrosoftSQL2005;
                    string __guid = _g.g._logMenu(0, "", "Login");
                  //  MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                   // myFrameWork._databaseSelectType = MyLib._myGlobal._databaseType.MicrosoftSQL2005;
                    MyLib._myResource._resource = myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                    MyLib._myGlobal._mainForm.Dispose();
                    MyLib._myGlobal._mainForm = new _mainForm();
                    Application.Run(MyLib._myGlobal._mainForm);
                    _g.g._logMenu(2, __guid, "");
                }
            }
        }
        //static void __selectDatabase__changeLanguage(int languageCode)
        //{
        //    ((SMLERPTemplate._templateMainForm)MyLib._myGlobal._mainForm)._changeLanguage(languageCode);
        //}
    }
}
