using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Data;

namespace AkzoGenPoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(AkzoGlobal._global._poConfigFileName))
            {
                try
                {
                    // read config file
                    XmlSerializer s = new XmlSerializer(typeof(AkzoGlobal._global._poConfig));
                    AkzoGlobal._global._poConfig __config;
                    TextReader r = new StreamReader(AkzoGlobal._global._poConfigFileName);
                    __config = (AkzoGlobal._global._poConfig)s.Deserialize(r);
                    r.Close();

                    AkzoGlobal._global._webServiceServer = __config._serverAddress;
                    AkzoGlobal._global._databaseName = __config._database_name;
                    AkzoGlobal._global._userName = __config._user_code;
                    AkzoGlobal._global._password = __config._user_password;

                    SqlConnection __conn = AkzoGlobal._global._getConnection(__config._serverAddress, __config._database_name, __config._user_code, __config._user_password);

                    __conn.Open();
                    string __myQueryTop = "select doc_no,doc_date_time,amount_before_discount,discount , amount ,memo,agent_code,(select name from sml_agent where sml_agent.code=eorder_order.agent_code) as agent_name, (select email from sml_agent where sml_agent.code=eorder_order.agent_code) as agent_email from eorder_order  where last_status = 2 and (pdf_status is null or pdf_status<>1)";
                    SqlDataAdapter __daWait = new SqlDataAdapter(__myQueryTop, __conn);
                    DataTable __dtWait = new DataTable();
                    __daWait.Fill(__dtWait);
                    for (int __row = 0; __row < __dtWait.Rows.Count; __row++)
                    {
                        string __getDocNo = __dtWait.Rows[__row].ItemArray.GetValue(0).ToString();
                        string __getAgentCode = __dtWait.Rows[__row].ItemArray.GetValue(6).ToString();
                        string __getAgentEmail = __dtWait.Rows[__row].ItemArray.GetValue(8).ToString();

                        AkzoGenPo.poPrint __print = new AkzoGenPo.poPrint();
                        if (__print._print(__getDocNo))
                        {
                            SqlCommand __cmdSQL = new SqlCommand("update eorder_order set pdf_status=1 where doc_no =\'" + __getDocNo + "\' and  last_status = 2", __conn);
                            int rs = __cmdSQL.ExecuteNonQuery();
                            __cmdSQL.Dispose();

                            //Console.WriteLine("Generate PO No :" + __getDocNo + " Success");
                            // ส่งเมลล์
                            if (__config._sendOrder)
                            {
                                //AkzoGlobal._global._sendMail("akzoeorder@gmail.com", "029649744", __getAgentEmail, "E-Ordering-" + __getAgentCode, "Order Detail", @"c:\\smlpdftemp\\" + __getDocNo + ".pdf");
                                AkzoGlobal._global._sendMail(__config._emailOrderTarget, __config._emailSenderPassword, __getAgentEmail, "E-Ordering-" + __getAgentCode, "Order Detail", AkzoGlobal._global._pdfLocation + __getDocNo + ".pdf");
                            }
                        }
                    }
                    __conn.Close();
                }
                catch
                {

                }
            }
            else
            {
                Console.WriteLine("Not Found ConfigFile in :" + AkzoGlobal._global._poConfigFileName);
            }

        }
    }
}
