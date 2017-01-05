using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Data.SqlClient;

namespace SMLTOBC
{
    public class _routine
    {
        public string _xmlConfigFile = "config.xml";

        public NpgsqlConnection _smlConnection(_global value)
        {
            string __connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};timeout =10;", value._smlConnectProvider, value._smlConnectPort, value._smlConnectUser, value._smlConnectPassword, value._smlConnectDatabaseName);
            return new NpgsqlConnection(__connstring);
        }

        public SqlConnection _bcConnection(_global value)
        {
            string __connetionString = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", value._bcConnectProvider, value._bcConnectDatabaseName, value._bcConnectUser, value._bcConnectPassword);
            return new SqlConnection(__connetionString);
        }

        public void _saveConfig(_global value)
        {
            System.Xml.Serialization.XmlSerializer __writer = new System.Xml.Serialization.XmlSerializer(value.GetType());
            System.IO.StreamWriter __file = new System.IO.StreamWriter(this._xmlConfigFile);
            __writer.Serialize(__file, value);
            __file.Close();
        }

        public _global _loadConfig()
        {
            _global __value = new _global();
            try
            {
                System.Xml.Serialization.XmlSerializer __reader = new System.Xml.Serialization.XmlSerializer(__value.GetType());

                // Read the XML file.
                System.IO.StreamReader __file = new System.IO.StreamReader(this._xmlConfigFile);

                // Deserialize the content of the file into a Book object.
                __value = (_global)__reader.Deserialize(__file);
                __file.Close();
            }
            catch
            {

            }
            return __value;
        }
    }
}
