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

            //_customerDetailProcess __processCustomerDetail = new _customerDetailProcess();
            //__processCustomerDetail._startProcess();

            //_apSupplierProcess __processApSupplier = new _apSupplierProcess();
            //__processApSupplier._startProcess();

            //_arSupplierDetailProcess __processArSupplierDetailProcess = new _arSupplierDetailProcess();
            //__processArSupplierDetailProcess._startProcess();

            //_icTransProcess __processIcTransProcess = new _icTransProcess();
            //__processIcTransProcess._startProcess();

            //_icTransDetailProcess __processIcTransDetailProcess = new _icTransDetailProcess();
            //__processIcTransDetailProcess._startProcess();

            //_icInventoryProcess __processIcInventoryProcess = new _icInventoryProcess();
            //__processIcInventoryProcess._startProcess();

            _icInventoryDetailProcess __processIcInventoryDetailProcess = new _icInventoryDetailProcess();
            __processIcInventoryDetailProcess._startProcess();

            //_icUnitUseProcess __processIcUnitUseProcessProcess = new _icUnitUseProcess();
            //__processIcUnitUseProcessProcess._startProcess();

        }


        public static void _globalConfig()
        {
            MyLib._myGlobal._databaseConfig = "SMLConfig.xml";
            MyLib._myGlobal._webServiceName = "SMLJavaWebService";
            MyLib._myGlobal._databaseName = dbName;
        }
    }
}
