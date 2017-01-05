using System;
using System.Collections.Generic;
using System.Text;

namespace SMLTransferDataPOS
{
    public class _global
    {
        public static string _datacenter_server = "";
        public static string _datacenter_provider = "";
        public static string _datacenter_database_name = "";
        public static MyLib._myGlobal._databaseType _datacenter_database_type = MyLib._myGlobal._databaseType.PostgreSql;

        public static String _datacenter_configFileName
        {
            get
            {
                return "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml";
            }
        }
    }
}
