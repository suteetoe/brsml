using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Net;

namespace SMLOffTakeSalesAdmin
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
            MyLib._myGlobal._language = MyLib._languageEnum.Thai;
            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLColorStore;
            MyLib._myGlobal._isDesignMode = false;

            // Moo ย้ายมาจาก SMLERPTemplate._main
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            MyLib._myGlobal._computerName = Dns.GetHostName();
            MyLib._myGlobal._year_type = 1;
            MyLib._myGlobal._year_add = 543;
            MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
            MyLib._myGlobal._workingDate = DateTime.Now;
            MyLib._myGlobal._profileFileName = "OfftakeSalesAdminLogin.XML".ToLower();
            MyLib._myGlobal._mainDatabase = "SMLERPMAIN".ToLower();
            MyLib._myGlobal._dataViewXmlFileName = "SMLERPView.xml".ToLower();
            MyLib._myGlobal._tableNameView = "erp_view_table".ToLower();
            MyLib._myGlobal._tableNameViewColumn = "erp_view_column".ToLower();
            //MyLib._myGlobal._databaseStructFileName = "SMLDatabase.xml".ToLower();
            MyLib._myGlobal._databaseStructFileName = "offtakesalesadmindatabase.xml".ToLower();
            MyLib._myGlobal._languageXmlFileName = "smllanguage.xml".ToLower();
            MyLib._myGlobal._mainDatabaseStruct = "SMLMainDatabase.xml".ToLower();
            MyLib._myGlobal._databaseConfig = "OfftakeSalesAdminConfig.xml";
            MyLib._myGlobal._dataViewTemplateXmlFileName = "SMLERPViewTemplate.xml".ToLower();
            MyLib._myGlobal._webServiceName = "SMLJavaWebService";
            _g.g._saveLog = true;
            MyLib._myGlobal._nonPermission = true;
            SMLERPTemplate._main._startProgram();
            Boolean __autoLogin = false;
            string __computerName = SystemInformation.ComputerName.ToLower();
                      
            if (__autoLogin == false)
            {
                // Login Screen
                //หมวด Login
                MyLib._myGlobal._guid = "SMLX";
                MyLib._myGlobal._mainForm = new _mainScreen();
                MyLib._databaseManage._selectDatabase __selectDatabase = new MyLib._databaseManage._selectDatabase();
                Application.Run(__selectDatabase);
                if (MyLib._myGlobal._databaseName.Length > 0)
                {
                    //MyLib._myGlobal._checkFirst();
                    MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                    myFrameWork._databaseSelectType = MyLib._myGlobal._databaseType.MicrosoftSQL2005;
                    string __guid = _g.g._logMenu(0, "", "Login");
                   // MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                   
                    MyLib._myResource._resource = myFrameWork._resourceLoadAll(MyLib._myGlobal._dataGroup);
                    MyLib._myGlobal._mainForm.Dispose();
                    MyLib._myGlobal._mainForm = new _mainScreen();
                    Application.Run(MyLib._myGlobal._mainForm);
                    _g.g._logMenu(2, __guid, "");
                }
            }
        }       
    }
}
