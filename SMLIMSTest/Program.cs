using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMLIMSTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MyLib._myGlobal._maxUser = 250;
            MyLib._myGlobal._programName = "IMEX Management System";
            MyLib._myGlobal._OEMVersion = "imex";
            // toe MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLIMS;
            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLAccountProfessional;
            SMLAccount.Program.Main();
        }
    }
}
