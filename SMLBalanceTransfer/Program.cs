using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMLBalanceTransfer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MyLib._myGlobal._year_type = 0;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string __computerName = SystemInformation.ComputerName.ToLower();

            if (__computerName.IndexOf("toe-pc") != -1)
            {
                if (MessageBox.Show("Local Login", "Login", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _global._sourceProvider = 0;
                    _global._soruceHost = "localhost";
                    _global._sourcePort = "5432";
                    _global._sourceUser = "postgres";
                    _global._sourcePass = "sml";
                    _global._sourceDatabase = "akzo001";

                    _global._targetProvider = 0;
                    _global._targetHost = "localhost";
                    _global._targetPort = "5432";
                    _global._targetUser = "postgres";
                    _global._targetPass = "sml";
                    _global._targetDatabase = "akzo001_new";

                    _global._autoLogin = true;
                }
            }
            Application.Run(new _mainForm());
        }
    }
}
