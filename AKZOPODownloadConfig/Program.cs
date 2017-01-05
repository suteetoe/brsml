using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AKZOPODownloadConfig
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

            if (System.IO.Directory.Exists(@"C:\smlconfig\") == false)
            {
                System.IO.Directory.CreateDirectory(@"C:\smlconfig\");
            }


            Application.Run(new Form1());
        }
    }
}
