using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPMailMessage
{
    public class _sendMessage
    {
        public static void _sendMessageEmail(string sendTo, string Message, string title)
        {
            _send(1, sendTo, Message, title);
        }

        public static void _sendMessageSMS(string sendTo, string Message)
        {
            _send(2, sendTo, Message, "");
        }

        public static void _sendMessageSaleHub(string sendTo, string Message, string title)
        {
            _send(1009, sendTo, Message, title);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1=Email,2=SMS,3=Line,1009=SaleHub</param>
        /// <param name="sendTo"></param>
        /// <param name="Message"></param>
        /// <param name="title"></param>
        public static void _send(int type, string sendTo, string Message, string title)
        {
            // insert into erp_send_message

            string __query = "insert into " + _g.d.erp_send_message._table + "(" + MyLib._myGlobal._fieldAndComma(_g.d.erp_send_message._send_type, _g.d.erp_send_message._send_to, _g.d.erp_send_message._send_message, _g.d.erp_send_message._send_title) + ") values (" + type + ",\'" + MyLib._myGlobal._convertStrToQuery(sendTo) + "\',\'" + MyLib._myGlobal._convertStrToQuery(Message) + "\',\'" + MyLib._myGlobal._convertStrToQuery(title) + "\') ";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
            if (__result.Length > 0)
            {
                try
                {
                    MyLib._myGlobal._writeEventLog(__result.ToString(), "Send Message Error");
                }
                catch
                {

                }
            }

        }

        public static void _sendMessageLine(string sendTo, string Message)
        {
            _send(3, sendTo, Message, "");
        }
    }
}
