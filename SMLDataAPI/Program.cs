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
        public static string APIKey = "";
        public static string APISecret = "";

        static void Main()
        {
            _globalConfig();
            //serverURL = "localhost:8080";
            //provider = "DATACENTER";
            //dbName = "DATACENTER1".ToLower();

            serverURL = "ws.brteasy.com:8080";
            provider = "BRDATACENTER";
            dbName = "brcenter".ToLower();

            //_arCustomerProcess __processAR = new _arCustomerProcess();
            //__processAR._startProcess();

            _customerDetailProcess __process = new _customerDetailProcess();
            __process._startProcess();
        }


        public static void _globalConfig()
        {
            MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
            MyLib._myGlobal._webServiceName = "SMLJavaWebService";
            MyLib._myGlobal._databaseName = dbName;
        }
    }
}
