using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SMLTransfer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            MyLib._myGlobal._computerName = WindowsIdentity.GetCurrent().Name;
            MyLib._myGlobal._year_type = 1;
            MyLib._myGlobal._year_add = 543;
            MyLib._myGlobal._year_current = DateTime.Now.Year + MyLib._myGlobal._year_add;
            MyLib._myGlobal._workingDate = DateTime.Now;

            // for connect srevice
            MyLib._myGlobal._mainDatabase = "SMLERPMAIN".ToLower();
            MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
            MyLib._myGlobal._webServiceName = "SMLJavaWebService";

            CultureInfo __ci = new CultureInfo("th-TH");// CultureInfo.CurrentCulture;
            if (__ci.DateTimeFormat.ShortDatePattern.Equals("d/M/yyyy") == false &&
                __ci.DateTimeFormat.ShortDatePattern.Equals("dd/MM/yyyy") == false)
            {
                MessageBox.Show("Date format Error Please select d/M/yyyy or dd/MM/yyyy : Control Panel -> Regional");
                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new _mainForm1());
        }
    }
}
