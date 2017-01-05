using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace AKZOUpdateAgencodeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string __server = args[0].ToString();
                string __userName = args[1].ToString();
                string __password = args[2].ToString();
                string __databaseName = args[3].ToString();

                string __connectionString = "Data Source=" + __server + ";" + "User ID=" + __userName + ";" + "Password=" + __password + ";" + "Initial Catalog=" + __databaseName;
                SqlConnection __sqlConnection = new SqlConnection();
                //
                __sqlConnection.ConnectionString = __connectionString;
                __sqlConnection.Open();

                string __updateTransQuery = "update ic_trans set agencode = branch_sync where agencode is null and branch_sync is not null ";

                SqlCommand __sqlUpdateTransCommand = new SqlCommand(__updateTransQuery, __sqlConnection);
                __sqlUpdateTransCommand.ExecuteNonQuery();
                __sqlUpdateTransCommand.Dispose();

                string __updateTransDetailQuery = "update ic_trans_detail set agencode = branch_sync where agencode is null and branch_sync is not null ";

                SqlCommand __sqlUpdateTransDetailCommand = new SqlCommand(__updateTransDetailQuery, __sqlConnection);
                __sqlUpdateTransDetailCommand.ExecuteNonQuery();
                __sqlUpdateTransDetailCommand.Dispose();



                __sqlConnection.Close();
                __sqlConnection.Dispose();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
