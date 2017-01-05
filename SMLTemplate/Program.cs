using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Security.Principal;

namespace SMLTemplate
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
            //SMLERPTemplate._main._startProgram();

            MyLib._myGlobal._programName = "SML Account";
            MyLib._myGlobal._profileFileName = "SMLERPLogin.XML".ToLower();
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

            MyLib._myGlobal._compressWebservice = false;
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
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            __myFrameWork._webserviceServerReConnect(false);
            //__myFrameWork._webserviceServerReConnect(true);
            MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
            MyLib._myGlobal._userCode = "Superadmin";
            MyLib._myGlobal._userLoginSuccess = true;
            MyLib._myGlobal._password = "superadmin";
            MyLib._myGlobal._userName = "User FOR TEST PROGRAMS";
            MyLib._myGlobal._branchCode = "000";

            MyLib._myGlobal._providerCode = "SMLMASTER";
            MyLib._myGlobal._databaseName = "SMLMASTER";

            MyLib._myGlobal._guid = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._userCode, MyLib._myGlobal._password, MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);
            if (MyLib._myGlobal._guid.Length == 0)
            {
                MessageBox.Show("GUID Error");
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
