using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace AkzoGlobal
{
    public class _global
    {
        public static bool _loginPass = false;
        public static string _poFormFileName = "po_form.xml";
        public static string _poConfigFileName = @"c:\\smlconfig\\poconfig.xml";
        public static string _pdfLocation = @"c:\\smlpdftemp\\";

        public static string _webServiceServer = "";
        public static string _databaseName = "";
        public static string _userName = "";
        public static string _password = "";

        public static string _errSendMailMessage = "";

        public static SqlConnection _sqlConnection
        {
            get
            {
                return new SqlConnection("Server=" + AkzoGlobal._global._webServiceServer + ";Database=" + AkzoGlobal._global._databaseName + ";uid=" + AkzoGlobal._global._userName + ";Password=" + AkzoGlobal._global._password + ";Connect Timeout=3000");
            }
        }

        public static SqlConnection _getConnection(string server, string databaseName, string usercode, string userpassword)
        {
            return new SqlConnection("Server=" + server + ";Database=" + databaseName + ";uid=" + usercode + ";Password=" + userpassword + ";Connect Timeout=3000");
        }

        public static bool _sqlTestConnection(string serverName, string databaseName, string databaseUser, string databasePassword)
        {
            SqlConnection __conn = new SqlConnection("Server=" + serverName + ";Database=" + databaseName + ";uid=" + databaseUser + ";Password=" + databasePassword + ";Connect Timeout=3000");
            try
            {
                __conn.Open();
                __conn.Close();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public class _poConfig
        {
            public string _serverAddress = "";
            public string _user_code = "";
            public string _user_password = "";
            public string _database_name = "";
            public Boolean _sendOrder = true;
            public string _emailOrderTarget = "";
            public string _emailSenderPassword = "";

        }

        public static bool _sendMail(string gMailAccount, string password, string to, string subject, string message, string pdf)
        {
            string[] __toSplit = to.Split(',');
            Console.Write("Send Mail to : " + to + " : ");
            try
            {
                NetworkCredential loginInfo = new NetworkCredential(gMailAccount, password);
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(gMailAccount);
                msg.To.Add(new MailAddress(__toSplit[0]));
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;
                for (int __loop = 1; __loop < __toSplit.Length; __loop++)
                {
                    MailAddress __cc = new MailAddress(__toSplit[__loop]);
                    msg.CC.Add(__cc);
                }
                Attachment pdfFile = new Attachment(pdf);
                msg.Attachments.Add(pdfFile);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
                client.Send(msg);
                Console.WriteLine("Success");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail : " + ex.Message.ToString());
                _errSendMailMessage = "Error Send Mail From :" + gMailAccount + "(" + password + ") \nTo :" + to + "\nError : " + ex.Message.ToString();
                return false;

                //try
                //{
                //    NetworkCredential loginInfo = new NetworkCredential(gMailAccount, password);
                //    MailMessage msg = new MailMessage();
                //    msg.From = new MailAddress(gMailAccount);
                //    msg.To.Add(new MailAddress(__toSplit[0]));
                //    msg.Subject = subject;
                //    msg.Body = message;
                //    msg.IsBodyHtml = true;
                //    for (int __loop = 1; __loop < __toSplit.Length; __loop++)
                //    {
                //        MailAddress __cc = new MailAddress(__toSplit[__loop]);
                //        msg.CC.Add(__cc);
                //    }
                //    Attachment pdfFile = new Attachment(pdf);
                //    msg.Attachments.Add(pdfFile);
                //    SmtpClient client = new SmtpClient("smtp.gmail.com");
                //    client.Port = 465;
                //    client.EnableSsl = true;
                //    client.UseDefaultCredentials = false;
                //    client.Credentials = loginInfo;
                //    client.Send(msg);
                //    Console.WriteLine("Success");
                //    return true;
                //}
                //catch
                //{
                //Console.WriteLine("Fail : " + ex.Message.ToString());
                //return false;

                //}
            }

        }

        public static string _smlConfigFile
        {
            get
            {
                string __smlConfigPath = @"C:\smlconfig\";
                bool __isDirCreate = System.IO.Directory.Exists(__smlConfigPath);

                if (__isDirCreate == false)
                {
                    System.IO.Directory.CreateDirectory(__smlConfigPath); // create folders
                }
                return __smlConfigPath;
            }
        }
    }
}
