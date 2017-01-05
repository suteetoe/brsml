using System;
using System.Net;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Reflection;

namespace SMLERPTemplate
{
    public static class _main
    {
        public static void _startProgram()
        {
            CultureInfo __ci = new CultureInfo("th-TH");// CultureInfo.CurrentCulture;
            if (__ci.DateTimeFormat.ShortDatePattern.Equals("d/M/yyyy") == false &&
                __ci.DateTimeFormat.ShortDatePattern.Equals("dd/MM/yyyy") == false)
            {
                MessageBox.Show("Date format Error Please select d/M/yyyy or dd/MM/yyyy : Control Panel -> Regional");
                Environment.Exit(1);
            }

            // MOO ย้ายไปอยู่ที่ SMLColorStore.Program 
            //เพราะ เกิดไม่ได้ ล๊อกอินออโตจะมีปัญหาเรื่องวันที่
            // ---------------------------------------------------------------------
            /*Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            MyLib._myGlobal._computerName = Dns.GetHostName();
            MyLib._myGlobal._year_type = 1;
            MyLib._myGlobal._year_add = 543;
            MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
            MyLib._myGlobal._workingDate = DateTime.Now;*/
            //----------------------------------------------------------------------


            //MyLib._myGlobal._webServiceName = "SMLWS/services";
            //MyLib._myGlobal._webServiceName = "SMLJavaWebService";
            //MyLib._myGlobal._userCode = "superadmin";
            //MyLib._myGlobal._password = "";
            //MyLib._myGlobal._userName = "Superadmin";
            //MyLib._myGlobal._userCode = "DEMO";
            //MyLib._myGlobal._password = "";
            //MyLib._myGlobal._userName = "DEMO FOR TEST PROGRAMS";
            //
            MyLib._getStream._getStreamEvent += new MyLib._getStreamEventHandler(_getStream__getStreamEvent);
            /////
            ///// <summary>
            ///// SMLHP(HirePurchase)  เช้าซื้อ
            ///// </summary>
            ///// 
            //MyLib._myGlobal._profileFileName = "SMLLogin.XML";
            //MyLib._myGlobal._mainDatabase = "SMLHPMAIN";
            //MyLib._myGlobal._dataViewTemplateXmlFileName = "SMLHPVIEWTEMPLATE.XML";
            //MyLib._myGlobal._dataViewXmlFileName = "SMLHPView.xml";
            //MyLib._myGlobal._tableNameView = "erp_view_table";
            //MyLib._myGlobal._tableNameViewColumn = "erp_view_column";
            //MyLib._myGlobal._databaseStructFileName = "SMLHPDatabase.xml";
            //MyLib._myGlobal._mainDatabaseStruct = "SMLHPMainDatabase.xml";
            //MyLib._myGlobal._databaseConfig = "SMLHPConfig.xml";

            /* MyLib._myGlobal._profileFileName = "SMLERPLogin.XML";
             MyLib._myGlobal._mainDatabase = "SMLERPMAIN";
             MyLib._myGlobal._dataViewXmlFileName = "SMLERPView.xml";
             MyLib._myGlobal._tableNameView = "erp_view_table";
             MyLib._myGlobal._tableNameViewColumn = "erp_view_column";
             MyLib._myGlobal._databaseStructFileName = "SMLDatabase.xml";
             MyLib._myGlobal._mainDatabaseStruct = "SMLMainDatabase.xml";
             MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
             MyLib._myGlobal._dataViewTemplateXmlFileName = "SMLERPViewTemplate.XML";*/

            MyLib._myGlobal._compressWebservice = true;
            //
            MyLib._myGlobal._holiday.Add(true);
            MyLib._myGlobal._holiday.Add(false);
            MyLib._myGlobal._holiday.Add(false);
            MyLib._myGlobal._holiday.Add(false);
            MyLib._myGlobal._holiday.Add(false);
            MyLib._myGlobal._holiday.Add(false);
            MyLib._myGlobal._holiday.Add(true);
            //
            MyLib._myGlobal._officialHolidayType __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 5;
            __holiday1._month = 12;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = true;
            __holiday1._governmentHoliday = true;
            __holiday1._name1 = "วันพ่อ";
            __holiday1._name2 = "Dad day";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 12;
            __holiday1._month = 8;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = true;
            __holiday1._governmentHoliday = true;
            __holiday1._name1 = "วันแม่";
            __holiday1._name2 = "Mama Day";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 7;
            __holiday1._year = 2006;
            __holiday1._month = 10;
            __holiday1._bankHoliday = false;
            __holiday1._governmentHoliday = false;
            __holiday1._name1 = "วันออกพรรษา";
            __holiday1._name2 = "";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 23;
            __holiday1._month = 10;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = true;
            __holiday1._governmentHoliday = true;
            __holiday1._name1 = "วันปิยมหาราช";
            __holiday1._name2 = "";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 5;
            __holiday1._month = 11;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = false;
            __holiday1._governmentHoliday = false;
            __holiday1._name1 = "วันลอยกระทง";
            __holiday1._name2 = "";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 10;
            __holiday1._month = 12;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = true;
            __holiday1._governmentHoliday = true;
            __holiday1._name1 = "วันพระราชทานรัฐธรรมนูญ";
            __holiday1._name2 = "";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 11;
            __holiday1._month = 12;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = true;
            __holiday1._governmentHoliday = true;
            __holiday1._name1 = "ชดเชยวันพระราชทานรัฐธรรมนูญ";
            __holiday1._name2 = "";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 25;
            __holiday1._month = 12;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = true;
            __holiday1._governmentHoliday = true;
            __holiday1._name1 = "วันคริสมาส";
            __holiday1._name2 = "";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);
            __holiday1 = new MyLib._myGlobal._officialHolidayType();
            __holiday1._day = 31;
            __holiday1._month = 12;
            __holiday1._year = 2006;
            __holiday1._bankHoliday = true;
            __holiday1._governmentHoliday = true;
            __holiday1._name1 = "วันสิ้นปี";
            __holiday1._name2 = "";
            MyLib._myGlobal._officialHoliday.Add(__holiday1);

            // ย้าย add web service ไปไว้ที่ Program.CS มันตีกัน
            //
            System.Drawing.Font font = new System.Drawing.Font("Tahoma", 10);
            Application.EnableVisualStyles();
            //
        }

        static System.IO.Stream _getStream__getStreamEvent(string xmlFileName)
        {
            Assembly __thisAssembly = Assembly.GetExecutingAssembly();
            return __thisAssembly.GetManifestResourceStream("SMLERPTemplate." + xmlFileName);
        }
    }
}
