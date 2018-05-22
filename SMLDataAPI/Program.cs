using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMLDataAPI
{
    static class Program
    {
        public static string dbFileName = "";
        public static string serverURL = "";
        public static string provider = "";
        public static string dbName = "";

        static void Main()
        {
            _globalConfig();
            //serverURL = "localhost:8080";
            //provider = "DATACENTER";
            //dbName = "DATACENTER1".ToLower();

            serverURL = "dev.smlsoft.com:8081";
            provider = "KOWYOOHAH";
            dbName = "KOWYOOHAH_TEST".ToLower();

            _arCustomer __processAR = new _arCustomer();
            __processAR._startProcess();
        }


        public static void _globalConfig()
        {
            MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
            MyLib._myGlobal._webServiceName = "SMLJavaWebService";
            MyLib._myGlobal._databaseName = "KOWYOOHAH_TEST".ToLower();
        }
    }
}
