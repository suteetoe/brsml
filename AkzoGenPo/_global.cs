using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace AkzoGenPo
{
    public class _global
    {
        // ย้ายไป AkzoGlobal._global เพื่อทำให้ console มีขนาดเล็กลง
        //public static bool _loginPass = false;
        //public static string _poFormFileName = "po_form.xml";
        //public static string _poConfigFileName = @"c:\\smlconfig\\poconfig.xml";

        //public static string _webServiceServer = "";
        //public static string _databaseName = "";
        //public static string _userName = "";
        //public static string _password = "";

        //public static SqlConnection _sqlConnection
        //{
        //    get
        //    {
        //        return new SqlConnection("Server=" + MyLib._myGlobal._webServiceServer + ";Database=" + MyLib._myGlobal._databaseName + ";uid=" + MyLib._myGlobal._userName + ";Password=" + MyLib._myGlobal._password + ";Connect Timeout=3000");
        //    }
        //}

        //public static SqlConnection _getConnection(string server, string databaseName,string usercode, string userpassword)
        //{
        //    return new SqlConnection("Server=" + server + ";Database=" + databaseName + ";uid=" + usercode + ";Password=" + userpassword + ";Connect Timeout=3000");
        //}

        //public static bool _sqlTestConnection(string serverName, string databaseName, string databaseUser, string databasePassword)
        //{
        //    SqlConnection __conn = new SqlConnection("Server=" + serverName + ";Database=" + databaseName + ";uid=" + databaseUser + ";Password=" + databasePassword + ";Connect Timeout=3000");
        //    try
        //    {
        //        __conn.Open();
        //        __conn.Close();
        //        return true;
        //    }
        //    catch
        //    {
        //    }
        //    return false;
        //}


    }

    //public class _poConfig
    //{
    //    public string _serverAddress = "";
    //    public string _user_code = "";
    //    public string _user_password = "";
    //    public string _database_name = "";
    //    public Boolean _sendOrder = true;
    //    public string _emailOrderTarget = "";

    //}
}
