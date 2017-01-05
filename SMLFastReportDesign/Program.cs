using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Security.Principal;

namespace SMLFastReportDesign
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

            MyLib._myGlobal._isDesignMode = false;
            //MyLib._myGlobal._isVersion = "SMLERP";
            MyLib._myGlobal._language = MyLib._languageEnum.Thai;
            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLPOS;

            // Moo ย้ายมาจาก SMLERPTemplate._main
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            MyLib._myGlobal._computerName = WindowsIdentity.GetCurrent().Name;
            MyLib._myGlobal._year_type = 1;
            MyLib._myGlobal._year_add = 543;
            MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
            MyLib._myGlobal._workingDate = DateTime.Now;
            //SMLERPTemplate._main._startProgram();

            MyLib._myGlobal._programName = "SML POS";
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
            MyLib._myGlobal._databaseVerifyXmlFileName = "smldatabaseverify.xml".ToLower();
            _g.g._saveLog = true;


            //
            string __computerName = SystemInformation.ComputerName.ToLower();
            //
            //SMLERPTemplate._main._startProgram();

            // global config
            MyLib._myGlobal._webServiceServerList.Clear();
            for (int loop = 1; loop <= 1; loop++)
            {
                string getStr = "";
                switch (loop)
                {
                    //case 1: getStr = "smlsoft.homeip.net"; break;
                    //case 1: getStr = "www.smlsoft.com:8083"; break;
                    case 1: getStr = "www.smlsoft.com:8080"; break;
                    //case 1: getStr = "localhost:8080"; break;
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
            MyLib._myGlobal._providerCode = "SMLMASTER"; //"BAL";
            MyLib._myGlobal._databaseName = "SMLMASTER"; //"BAL";
            MyLib._myGlobal._nonPermission = true;
            MyLib._myGlobal._password = "superadmin"; //  "superadmin";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            __myFrameWork._webserviceServerReConnect(false);
            MyLib._myGlobal._webServiceServer = __myFrameWork._findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
            //
            bool no_guid = true;
            MyLib._myGlobal._guid = __myFrameWork._loginProcess(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._mainDatabase, MyLib._myGlobal._userCode, MyLib._myGlobal._password, MyLib._myGlobal._computerName, MyLib._myGlobal._databaseName);

            Application.Run(new Form1());
        }
    }
}
