using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SMLPOSChecker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new _mainForm());

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
                    //decimal __testRound=659M;
                    //for (int __loop = 0;__loop < 100000;__loop++){
                    //    Console.WriteLine(__testRound.ToString()+" "+MyLib._myGlobal._round(__testRound, 2).ToString());
                    //    __testRound += 0.00001M;
                    //}

                    MyLib._myGlobal._isDesignMode = false;
                    //MyLib._myGlobal._isVersion = "SMLERP";
                    MyLib._myGlobal._language = MyLib._languageEnum.Thai;

                    // toe 

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    MyLib._myGlobal._computerName = WindowsIdentity.GetCurrent().Name;
                    MyLib._myGlobal._year_type = 1;
                    MyLib._myGlobal._year_add = 543;
                    MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
                    MyLib._myGlobal._workingDate = DateTime.Now;
                    //SMLERPTemplate._main._startProgram();
                    MyLib._myGlobal._programName = "SML POS Checker";

                    MyLib._myGlobal._profileFileName = "SMLERPActiveSyncLogin.XML".ToLower();
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
                    //
                    string __computerName = SystemInformation.ComputerName.ToLower();
                    Boolean __autoLogin = MyLib._myGlobal._isDemo;

                    //
                    if (__computerName.IndexOf("toe-pc") != -1)
                    {
                        DialogResult __dialogLocal = MessageBox.Show("Auto Connect", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        if (__dialogLocal == DialogResult.Yes)
                        {
                            __autoLogin = true;
                            MyLib._myGlobal._webServiceServerList.Clear();
                            for (int loop = 1; loop <= 1; loop++)
                            {
                                string getStr = "";
                                switch (loop)
                                {
                                    //case 1: getStr = "smlsoft.homeip.net"; break;
                                    //case 1: getStr = "www.smlsoft.com:8083"; break;
                                    case 1: getStr = "www.smldemo.com:8080"; break;
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

                            //if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                            //{
                            //    MyLib._myGlobal._providerCode = "UAT";
                            //    MyLib._myGlobal._databaseName = "tomyumtoe";
                            //}
                            //else
                            //{
                            //    MyLib._myGlobal._providerCode = "UAT";
                            //    MyLib._myGlobal._databaseName = "TOEUAT";
                            //}

                            MyLib._myGlobal._providerCode = "DEBUG";
                            MyLib._myGlobal._databaseName = "POSDEBUG";

                            MyLib._myGlobal._nonPermission = true;
                            MyLib._myGlobal._userCode = "Superadmin";
                            MyLib._myGlobal._userLoginSuccess = true;
                            MyLib._myGlobal._password = "superadmin";
                            MyLib._myGlobal._userName = "User FOR TEST PROGRAMS";
                            MyLib._myGlobal._branchCode = "000";

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
                                //
                                MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                                MyLib._myGlobal._mainForm = new _mainForm();

                                Application.Run(MyLib._myGlobal._mainForm);
                                _g.g._logMenu(2, __guid, "");
                            }
                           
                            
                        }
                    }

                    SMLERPTemplate._main._startProgram();

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
                            MyLib._myGlobal._checkFirst();
                            string __guid = _g.g._logMenu(0, "", "Login");
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            MyLib._myResource._resource = __myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                            MyLib._myGlobal._mainForm.Dispose();
                            MyLib._myGlobal._mainForm = new _mainForm();
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
        }
    }
}
