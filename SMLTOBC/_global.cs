using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Data.SqlClient;

namespace SMLTOBC
{
    public class _global
    {
        // sml
        public String _smlConnectProvider;
        public String _smlConnectPort = "5432";
        public String _smlConnectUser;
        public String _smlConnectPassword;
        public String _smlConnectDatabaseName;
        // bc
        public String _bcConnectProvider;
        public String _bcConnectPort = "";
        public String _bcConnectUser;
        public String _bcConnectPassword;
        public String _bcConnectDatabaseName;
    }

}
