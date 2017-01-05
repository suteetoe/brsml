using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BRAGENTPOSTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLAccountPOS;
            MyLib._myGlobal._OEMVersion = "SINGHA";
            MyLib._myGlobal._programName = "SINGHA SML";
            SMLAccount.Program.Main();
        }
    }
}
