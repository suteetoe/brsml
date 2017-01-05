using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMLColorStoreSync
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
            //Application.Run(new Form1());

            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLColorStore;
            SMLActivesync.Program.Main();
            
        }
    }
}
