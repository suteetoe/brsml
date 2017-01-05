using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;

namespace SMLIMS
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

            MyLib._myGlobal._maxUser = 250;
            MyLib._myGlobal._programName = "IMEX Management System";
            MyLib._myGlobal._OEMVersion = "imex";
            // toe MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLIMS;
            MyLib._myGlobal._isVersionEnum = MyLib._myGlobal._versionType.SMLAccountProfessional;
            CheckForShortcut();
            SMLAccount.Program.Main();
        }

        static void CheckForShortcut()
        {
            // if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            try
            {
                //ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                //if (ad.IsFirstRun)  //first time user has run the app since installation or update
                {
                    Assembly code = Assembly.GetExecutingAssembly();
                    string company = string.Empty;
                    string description = string.Empty;
                    if (Attribute.IsDefined(code, typeof(AssemblyCompanyAttribute)))
                    {
                        AssemblyCompanyAttribute ascompany =
                            (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(code,
                            typeof(AssemblyCompanyAttribute));
                        company = ascompany.Company;
                    }
                    if (Attribute.IsDefined(code, typeof(AssemblyDescriptionAttribute)))
                    {
                        AssemblyDescriptionAttribute asdescription =
                            (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(code,
                            typeof(AssemblyDescriptionAttribute));
                        description = asdescription.Description;
                    }
                    if (company != string.Empty && description != string.Empty)
                    {
                        string desktopPath = string.Empty;
                        desktopPath = string.Concat(
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                            "\\", description, ".appref-ms");
                        string shortcutName = string.Empty;
                        shortcutName = string.Concat(
                            Environment.GetFolderPath(Environment.SpecialFolder.Programs),
                            "\\", company, "\\", description, ".appref-ms");
                        System.IO.File.Copy(shortcutName, desktopPath, true);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
