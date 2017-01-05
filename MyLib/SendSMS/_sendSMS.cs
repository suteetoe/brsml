using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MyLib.SendSMS
{
    public class _sendSMS
    {
        public static string _send(string phoneNumber, string message)
        {
            string __result = "";
            Console.WriteLine("Send SMS to {0} : {1}", phoneNumber, message);

            // test
            //MyLib.SendSMS.WebPostRequest smsPost = new MyLib.SendSMS.WebPostRequest("http://www.thaibulksms.com/sms_api_test.php");           
            //smsPost.Add("username", "thaibulksms");
            //smsPost.Add("password", "thisispassword");

            if (MyLib._myGlobal._isSendSMS)
            {
                MyLib.SendSMS.WebPostRequest smsPost = new MyLib.SendSMS.WebPostRequest("http://www.thaibulksms.com/sms_api.php");
                smsPost.Add("username", "0810306268");
                smsPost.Add("password", "697192");

                smsPost.Add("msisdn", phoneNumber);
                smsPost.Add("message", message);
                smsPost.Add("force", "standard");
                smsPost.Add("sender", "THAIBULKSMS"); //sender
                smsPost.Add("ScheduledDelivery", "");

                try
                {
                    string returnData = smsPost.GetResponse();
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(returnData);
                    XmlNodeList xnList = xml.SelectNodes("/SMS");
                    int count_node = xnList.Count;
                    if (count_node > 0)
                    {
                        foreach (XmlNode xn in xnList)
                        {
                            XmlNodeList xnSubList = xml.SelectNodes("/SMS/QUEUE");
                            int countSubNode = xnSubList.Count;
                            if (countSubNode > 0)
                            {
                                foreach (XmlNode xnSub in xnSubList)
                                {
                                    if (xnSub["Status"].InnerText.ToString() == "1")
                                    {
                                        // success

                                        string msisdn = xnSub["Msisdn"].InnerText;
                                        string useCredit = xnSub["UsedCredit"].InnerText;
                                        string creditRemain = xnSub["RemainCredit"].InnerText;
                                        Console.WriteLine("Send SMS to {0} Success.Use credit {1} credit, Credit Remain {2} Credit", msisdn, useCredit, creditRemain);
                                    }
                                    else
                                    {
                                        // failed

                                        string sub_status_detail = xnSub["Detail"].InnerText;
                                        Console.WriteLine("Error: {0}", sub_status_detail);
                                        __result = sub_status_detail;
                                    }
                                }
                            }
                            else
                            {
                                if (xn["Status"].InnerText == "0")
                                {
                                    // failed

                                    string status_detail = xn["Detail"].InnerText;
                                    Console.WriteLine("Error: {0}", status_detail);
                                    __result = status_detail;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    __result = ex.Message.ToString();
                }
            }
            return __result;
        }

        public static string _sendSaleHubSINGHA(string phoneNumber, string message)
        {
            string __result = "";
            Console.WriteLine("Send SMS to {0} : {1}", phoneNumber, message);

            // เปลี่ยนไป path ใหม่

            //MyLib.SendSMS.WebPostRequest smsPost = new MyLib.SendSMS.WebPostRequest("https://apisaleshub.boonrawd.co.th/api/inbox/send");
            //smsPost.Add("apiKey", "fa1303aa97260c6e4bfa69fcbdd7934e");

            MyLib.SendSMS.WebPostRequest smsPost = new MyLib.SendSMS.WebPostRequest("http://bsnews.brteasy.com:8080/api/inbox/send");
            smsPost.Add("apiKey", "5514494e92efe05a6a852a9b328f1b10");
            smsPost.Add("channel", "inbox");
            smsPost.Add("sender", "BS-SINGHA SML");

            string[] _emails = phoneNumber.Split(',');
            foreach (string email in _emails)
            {
                smsPost.Add("emails", email);
            }

            smsPost.Add("message", message);
            //smsPost.Add("sender", "THAIBULKSMS"); //sender
            //smsPost.Add("ScheduledDelivery", "");

            try
            {
                string returnData = smsPost.GetResponse();
                __result = returnData;
                /*
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(returnData);
                XmlNodeList xnList = xml.SelectNodes("/SMS");
                int count_node = xnList.Count;
                if (count_node > 0)
                {
                    foreach (XmlNode xn in xnList)
                    {
                        XmlNodeList xnSubList = xml.SelectNodes("/SMS/QUEUE");
                        int countSubNode = xnSubList.Count;
                        if (countSubNode > 0)
                        {
                            foreach (XmlNode xnSub in xnSubList)
                            {
                                if (xnSub["Status"].InnerText.ToString() == "1")
                                {
                                    // success

                                    string msisdn = xnSub["Msisdn"].InnerText;
                                    string useCredit = xnSub["UsedCredit"].InnerText;
                                    string creditRemain = xnSub["RemainCredit"].InnerText;
                                    Console.WriteLine("Send SMS to {0} Success.Use credit {1} credit, Credit Remain {2} Credit", msisdn, useCredit, creditRemain);
                                }
                                else
                                {
                                    // failed

                                    string sub_status_detail = xnSub["Detail"].InnerText;
                                    Console.WriteLine("Error: {0}", sub_status_detail);
                                    __result = sub_status_detail;
                                }
                            }
                        }
                        else
                        {
                            if (xn["Status"].InnerText == "0")
                            {
                                // failed

                                string status_detail = xn["Detail"].InnerText;
                                Console.WriteLine("Error: {0}", status_detail);
                                __result = status_detail;
                            }
                        }
                    }
                }*/
            }
            catch (Exception ex)
            {
                __result = ex.Message.ToString();
            }

            return __result;

        }
    }
}
