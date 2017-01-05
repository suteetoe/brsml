using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMLColorStoreSyncTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLColorStore;
            SMLActivesync.Program.Main();
        }
    }
}
