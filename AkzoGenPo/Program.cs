using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace AkzoGenPo
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
            //Application.Run(new _mainScreen());

            System.IO.Directory.CreateDirectory(@"c:\\smlpdftemp");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Application.Run(new _loginForm());
            if (AkzoGlobal._global._loginPass)
            {
                Application.Run(new _mainScreen());
            }
        }
    }
}
