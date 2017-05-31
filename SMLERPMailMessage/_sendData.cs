using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace SMLERPMailMessage
{
    public class _sendData
    {
        public void _sendMessageData()
        {
            Thread.Sleep(60 * 1000);

            while (true)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // update เพื่อจอง รายการที่จะส่งทีละ 10
                string __updateForProcessQuery = "update " + _g.d.erp_send_message._table + " set " + _g.d.erp_send_message._send_process_guid + "=\'" + MyLib._myGlobal._guid + "\', " + _g.d.erp_send_message._send_process_time + "=now() where send_status = 0 and coalesce(send_process_guid, '') = '' and roworder  in (select  roworder from erp_send_message as ref where ref.send_status = 0 and coalesce(ref.send_process_guid, '') = '' limit 10 ) ";
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __updateForProcessQuery);

                // select from erp_send_message เฉพาะ รายการที่จองไว้
                string __query = "select roworder, send_type,send_to," + _g.d.erp_send_message._send_message + ",send_title from erp_send_message where send_status = 0 and " + _g.d.erp_send_message._send_process_guid + "=\'" + MyLib._myGlobal._guid + "\' ";

                // send 
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__result.Tables.Count > 0)
                {
                    DataTable __table = __result.Tables[0];
                    if (__table.Rows.Count > 0)
                    {
                        // start send
                        for (int __row = 0; __row < __table.Rows.Count; __row++)
                        {
                            int __roworder = MyLib._myGlobal._intPhase(__table.Rows[__row]["roworder"].ToString());
                            int __sendType = MyLib._myGlobal._intPhase(__table.Rows[__row][_g.d.erp_send_message._send_type].ToString());
                            string __to = __table.Rows[__row][_g.d.erp_send_message._send_to].ToString();
                            string __message = __table.Rows[__row][_g.d.erp_send_message._send_message].ToString();
                            string __title = __table.Rows[__row][_g.d.erp_send_message._send_title].ToString();

                            string __sendResult = "";
                            switch (__sendType)
                            {
                                case 1009:
                                    {
                                        __sendResult = MyLib.SendSMS._sendSMS._sendSaleHubSINGHA(__to, System.Uri.EscapeUriString(__message));
                                    }
                                    break;
                                case 2:
                                    // sms
                                    {
                                        MyLib.SendSMS._sendSMS._send(__to, __message);
                                    }
                                    break;
                                case 3:
                                    // line
                                    break;
                                default:
                                    // send email

                                    break;
                            }

                            if (__sendResult.Length > 0)
                            {
                                // insert logs
                                //string __insertLogsErrorQuery = "insert into " + _g.d.erp_send;
                                string __logsError = "Failed to Send type " + __sendType.ToString() + "\r\nTo : " + __to + "\r\nMessage : " + __message + " Error Detail : " + __sendResult;
                                MyLib._myGlobal._writeEventLog(__logsError);
                            }
                            else
                            {
                                // update erp_send_message success & failed status

                                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.erp_send_message._table + " set " + _g.d.erp_send_message._send_status + "=1, " + _g.d.erp_send_message._send_to_date + "=now()  where roworder =" + __roworder);
                            }
                        }
                    }
                }

                // update expire session send_message
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.erp_send_message._table + " set " + _g.d.erp_send_message._send_process_guid + "=\'\' where send_status = 0 and send_process_time < (now() - interval '10 minute') ");

                Thread.Sleep(60 * 1000);

            }

        }
    }
}
