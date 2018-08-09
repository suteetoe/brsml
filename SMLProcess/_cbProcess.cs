using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Xml;
using MyLib;
using System.Windows.Forms;

namespace SMLProcess
{
    public class _myWebservice : _g.SMLJavaWebService.SMLFrameWorkService
    {
        private string __webServiceFunctionName = "/SMLFrameWork";
        public _myWebservice()
        {
            this.Url = string.Concat("http://", MyLib._myGlobal._webServiceServer, "/", MyLib._myGlobal._webServiceName, this.__webServiceFunctionName);
            if (MyLib._myGlobal._proxyUsed == 1)
            {
                WebProxy __proxy = new WebProxy(MyLib._myGlobal._proxyUrl, true);
                __proxy.Credentials = new NetworkCredential(MyLib._myGlobal._proxyUser, MyLib._myGlobal._proxyPassword);
                this.Proxy = __proxy;
            }
            _checkCompress(MyLib._myGlobal._webServiceServer);
        }

        public _myWebservice(string webServiceName)
        {
            this.Url = string.Concat("http://", webServiceName, "/", MyLib._myGlobal._webServiceName, this.__webServiceFunctionName);
            MyLib._myGlobal._webServiceServer = webServiceName;
            if (MyLib._myGlobal._proxyUsed == 1)
            {
                WebProxy __proxy = new WebProxy(MyLib._myGlobal._proxyUrl, true);
                __proxy.Credentials = new NetworkCredential(MyLib._myGlobal._proxyUser, MyLib._myGlobal._proxyPassword);
                this.Proxy = __proxy;
            }
            _checkCompress(MyLib._myGlobal._webServiceServer);
        }

        private void _checkCompress(string webServiceName)
        {
            _myGlobal._compressWebservice = (webServiceName.IndexOf(".com") != -1 || webServiceName.IndexOf(".net") != -1 || webServiceName.IndexOf("203.") != -1 || webServiceName.IndexOf("202.") != -1 || webServiceName.IndexOf(".info") != -1) ? true : false;
            //_myGlobal._compressWebservice = true; // ทดสอบการย่อ จะได้ดูว่ามี Bug หรือเปล่า
        }
    }

    public class _cbProcess : MyLib._myFrameWork
    {
        public DataSet _processCBStatement(string databaseName, string dateBegin, string dateEnd, string where, string where2, string orderBy, int reporttype)
        {
            DataSet __ds = new DataSet();

            return (__ds);
        }

        public event QueryStreamEventHandler __queryStreamEvent;

        public byte[] _queryCBStreamGet(string reportGuid, int blockSize, int blockNumber)
        {
            _myWebservice __ws = new _myWebservice();
            return __ws._queryCBGetStream(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, reportGuid, blockSize, blockNumber);
        }

        public DataSet _queryCBStream(string databaseName, string query, string message)
        {
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            DataSet __resultDataSet = null;
            long __fileSize = 0;
            if (__myFrameWork._databaseSelectType == _myGlobal._databaseType.PostgreSql && query.ToLower().IndexOf("isnull") != -1)
            {
                MessageBox.Show("ISNULL ไม่สามารถใช้ได้กับ PostgreSQL");
            }
            if (MyLib._myGlobal._webServiceServer == null)
            {
                return null;
            }
            bool __loopWebservice = true;
            _tryCount = 0;
            while (__loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice();
                    string __reportGuid = Guid.NewGuid().ToString().ToLower();
                    //
                    if (__queryStreamEvent != null)
                    {
                        try
                        {
                            __queryStreamEvent(message + " : Server process query...", 0);
                        }
                        catch
                        {
                        }
                    }
                    __fileSize = MyLib._myGlobal._intPhase(__ws._queryCBStream(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, __reportGuid, databaseName, query));
                    //
                    int __blockSize = 102400;
                    int __calcBlock = ((int)__fileSize / __blockSize) + 1;
                    byte[] __result = new byte[0];
                    for (int __loop = 0; __loop < __calcBlock; __loop++)
                    {
                        int __countLoop = __loop + 1;
                        int __persent = (int)((__countLoop * 100) / __calcBlock);
                        if (__queryStreamEvent != null)
                        {
                            try
                            {
                                __queryStreamEvent(String.Format(message + " : Load data from Server ({0}/{1}) : Total file size {2}Kb", __countLoop, __calcBlock, __fileSize / 1024), __persent);
                            }
                            catch
                            {
                            }
                        }
                        byte[] __getData = _queryStreamGet(__reportGuid, __blockSize, __loop);
                        if (__getData != null)
                        {
                            __result = _appendArrays(__result, __getData);
                        }
                    }
                    //byte[] __getData = _myFrameWork._queryStreamGet(__reportGuid, 0);
                    System.IO.MemoryStream __fsSource = new System.IO.MemoryStream(__result);
                    System.IO.Compression.GZipStream __gzDecompressed = new System.IO.Compression.GZipStream(__fsSource, System.IO.Compression.CompressionMode.Decompress, true);

                    // Retrieve the size of the file from the compressed archive's footer
                    //FileStream fsSource = new FileStream(@"c:\temp\test.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] __bufferWrite = new byte[4];
                    __fsSource.Position = (int)__fsSource.Length - 4;
                    // Write the first 4 bytes of data from the compressed file into the buffer
                    __fsSource.Read(__bufferWrite, 0, 4);
                    // Set the position back at the start
                    __fsSource.Position = 0;
                    int __bufferLength = BitConverter.ToInt32(__bufferWrite, 0);

                    byte[] __buffer = new byte[__bufferLength + 100];
                    int __readOffset = 0;
                    int __totalBytes = 0;

                    // Loop through the compressed stream and put it into the buffer
                    while (true)
                    {
                        int __bytesRead = __gzDecompressed.Read(__buffer, __readOffset, 100);

                        // If we reached the end of the data
                        if (__bytesRead == 0)
                            break;

                        __readOffset += __bytesRead;
                        __totalBytes += __bytesRead;
                    }

                    // Write the content of the buffer to the destination stream (file)
                    System.Text.Encoding __enc = System.Text.Encoding.UTF8;
                    __resultDataSet = MyLib._myGlobal._convertStringToDataSet(__enc.GetString(__buffer));

                    // Close the streams
                    __fsSource.Close();
                    __gzDecompressed.Close();
                    //
                    __loopWebservice = false;
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                }
                catch (TimeoutException)
                {
                    _tryConnect();
                }
                catch (WebException)
                {
                    _tryConnect();
                }
                catch (XmlException)
                {
                    return null;
                }
            }
            return (__resultDataSet);
        }

        private byte[] _appendArrays(byte[] __result, byte[] __getData)
        {
            throw new NotImplementedException();
        }

        public DataSet _queryCBReportProcess(string databaseName, string dateBegin, string dateEnd, string where, string orderby, int reporttype)
        {
            DataSet __resultDataSet = null;
            //DataSet __ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable __table = new DataTable();
                __resultDataSet.Tables.Add(__table);
                return __resultDataSet;
            }
            bool __loopWebservice = true;
            while (__loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    String _getqurywhere = "";
                    String _getqurysubwhere = "";
                    if (where.Length != 0)
                    {
                        _getqurywhere = " where " + where;
                    }
                    else
                    {
                        _getqurywhere = "";
                    }
                    if ((dateBegin.Length != 0) && (dateEnd.Length != 0))
                    {
                        _getqurysubwhere = " and ( cb_trans_detail.doc_date  between '" + dateBegin + "' and '" + dateEnd + "')";
                    }
                    else
                    {
                        if (dateBegin.Length != 0) _getqurysubwhere = " and ( cb_trans_detail.doc_date  > " + MyLib._myGlobal._sqlDateFunction(dateBegin) + ")";
                        if (dateEnd.Length != 0) _getqurysubwhere = " and ( cb_trans_detail.doc_date  < " + MyLib._myGlobal._sqlDateFunction(dateEnd) + ")";
                    }
                    String __query = _getQuery(_getqurywhere, _getqurysubwhere, orderby, reporttype);
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                    __myQuery.Append("</node>");
                    MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                    __resultDataSet = MyLib._myGlobal._convertStringToDataSet(myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString()));
                    MyLib._myGlobal._serviceConnected = true;
                }
                catch (TimeoutException)
                {
                    _tryConnect();
                }
                catch (WebException)
                {
                    _tryConnect();
                }
                catch (XmlException)
                {
                    DataTable __table = new DataTable();
                    __resultDataSet.Tables.Add(__table);
                    return __resultDataSet;
                }
            }

            return (__resultDataSet);
        }
        public String _getQuery(String qurywhere, String qurysubwhere, String orderby, int reporttype)
        {
            String __result = "";
            String __query = "";
            if (reporttype == 1)
            {
                //รายงานรายได้จากธนาคาร
                __query = "select doc_date,doc_no,pass_book_code,remark,"
                                            + "((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=5  " + qurysubwhere + ")"
                                            + "-"
                                            + "(select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=6  " + qurysubwhere + "))"
                                            + " as sum_received"
                                            + " from cb_trans_detail " + qurywhere + " " + orderby + " ";
                __result = "select doc_date ,doc_no ,pass_book_code,remark,sum_received from (" + __query + ") as temp1 LIMIT 10";
                return __result;
            }
            else if (reporttype == 2)
            {
                //รายงานการฝากเงินสด
                __query = "select doc_date,doc_no,pass_book_code,remark,"
                                           + "((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=1  " + qurysubwhere + ")"
                                           + "-"
                                           + "(select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=2  " + qurysubwhere + ")"
                                           + " as sum_received"
                                           + " from cb_trans_detail " + qurywhere + " " + orderby + " ";
                __result = "select doc_date ,doc_no ,pass_book_code,remark,sum_received from (" + __query + ") as temp1";
                return __result;
            }
            else if (reporttype == 4)
            {
                //รายงานฝากเงินสด
                __query = "select doc_date,doc_no,pass_book_code,remark"
                    + ",((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=1  " + qurysubwhere + ")"
                       + "-"
                       + "(select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=2  " + qurysubwhere + "))"
                       + " as sum_received_out,"
                       + ",((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=1  " + qurysubwhere + ")"
                       + "-"
                       + "(select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=2  " + qurysubwhere + "))"
                       + " as sum_received_in"
                       + " from cb_trans_detail " + qurywhere + " " + orderby + " ";
                __result = "select doc_date ,doc_no ,pass_book_code,pass_book_code,remark,sum_received_out,sum_received_in from (" + __query + ") as temp1";
                return __result;
            }
            return __result;
        }

    }

    public class _cbInfoProcess
    {
        public string _createCashQuery(string dateWhere)
        {
            StringBuilder __query = new StringBuilder();

            __query.Append("select doc_date, doc_no, trans_flag, trans_type, ap_ar_code, pay_type" +
            ", case when (pay_type =1) then cash_amount else 0 end as income_amount" +
            ", case when (pay_type =2) then cash_amount else 0 end as pay_amount" +
            ", case when(pay_type =1)then cash_amount else -1*cash_amount end as amount" +
            ", 0 as balance_amount " +
            " from cb_trans where cash_amount <> 0 and (case when (trans_flag in (19,239) ) then  (select last_status from ap_ar_trans where ap_ar_trans.doc_no = cb_trans.doc_no and ap_ar_trans.trans_flag = cb_trans.trans_flag) else (select last_status from ic_trans where ic_trans.doc_no = cb_trans.doc_no and ic_trans.trans_flag = cb_trans.trans_flag) end)= 0 " + dateWhere);

            return __query.ToString();
        }

        public string _cashMovementQuery(DateTime beginDate, DateTime endDate)
        {
            StringBuilder __query = new StringBuilder();

            string __dateBegin = MyLib._myGlobal._convertDateToQuery(beginDate);
            string __dateEnd = MyLib._myGlobal._convertDateToQuery(endDate);

            // ยกมาจากการคำณวน doc_type = 0
            __query.Append("select 0 as doc_type, '1900-01-01' as doc_date, '' as doc_no, 0 as trans_type, '' as ap_ar_code, 0 as income_amount, 0 as pay_amount, sum(amount) as amount,  sum(amount) as balance_amount from (" + _createCashQuery(" and doc_date < " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " ") + ") as temp1");

            // รายการเคลื่อนไหว doc_type = 1
            __query.Append(" union all ");
            __query.Append(" select 1 as doc_type, doc_date, doc_no, trans_flag as trans_type, ap_ar_code, income_amount, pay_amount, amount, balance_amount from (" + _createCashQuery(" and doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ") as temp2 ");

            StringBuilder __query2 = new StringBuilder();
            __query2.Append(" select doc_type, doc_date, doc_no, trans_type, income_amount, pay_amount, amount, balance_amount from (" + __query.ToString() + ") as temp3 order by doc_type, doc_date, doc_no ");


            return __query.ToString();
        }

        public string _createPettyCashQuery(string dateWhere)
        {
            StringBuilder __query = new StringBuilder();


            string __payFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้);

            string __receiptFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ","
                + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน);
            // 0. คงเหลือ
            __query.Append("select 0 as calc_type,'1900-01-01' as doc_date, '' as doc_no, code as item_code, balance_money as sum_amount from cb_petty_cash as t1 where t1.code = cb_petty_cash.code");

            // รายวัน
            //1. เบิก
            __query.Append(" union all ");
            __query.Append(" select 1 as calc_type, doc_date, doc_no, item_code, -1*sum_amount  as sum_amount from ic_trans_detail where trans_flag = 300 and last_status = 0 and item_code = cb_petty_cash.code " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));

            // 2. คืน
            // 
            __query.Append(" union all ");
            __query.Append(" select 2 as calc_type, doc_date, doc_no, item_code, sum_amount  from ic_trans_detail where trans_flag = 301 and last_status = 0 and item_code = cb_petty_cash.code " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));

            // ตัดชำระ
            __query.Append(" union all ");
            __query.Append("select 3 as calc_type, doc_date, doc_no, trans_number  as item_code, -1*amount as sum_amount from cb_trans_detail where doc_type = 4 and last_status = 0  and trans_flag in (" + __payFlag + ") and trans_number = cb_petty_cash.code " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));

            // รับชำระ
            __query.Append(" union all ");
            __query.Append(" select 4 as calc_type, doc_date, doc_no, trans_number  as item_code, amount as sum_amount from cb_trans_detail where doc_type = 4 and last_status = 0  and trans_flag in (" + __receiptFlag + ") and trans_number = cb_petty_cash.code " + ((dateWhere.Length > 0) ? " and " + dateWhere : ""));

            return __query.ToString();
        }

        public string _cbStatusQuery(DateTime dateBegin, DateTime dateEnd)
        {
            StringBuilder __query = new StringBuilder();

            string __dateBegin = MyLib._myGlobal._convertDateToQuery(dateBegin);
            string __dateEnd = MyLib._myGlobal._convertDateToQuery(dateEnd);

            string __querySum = "select code, name_1" +
                ",(select sum(sum_amount) from (" + _createPettyCashQuery(" doc_date < " + MyLib._myGlobal._sqlDateFunction(__dateBegin)) + " ) as temp1 ) as balance_first" +
                ",(select -1*sum(sum_amount) from (" + _createPettyCashQuery(" doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ")  as temp2  where calc_type = 1) as request_amount" +
                ",(select sum(sum_amount) from (" + _createPettyCashQuery(" doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ")  as temp3  where calc_type = 2) as return_amount" +
                ",(select -1*sum(sum_amount) from (" + _createPettyCashQuery(" doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ")  as temp4  where calc_type = 3) as pay_amount" +
                ",(select sum(sum_amount) from (" + _createPettyCashQuery(" doc_date between " + MyLib._myGlobal._sqlDateFunction(__dateBegin) + " and " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ")  as temp5  where calc_type = 4) as receipt_amount" +
                ",(select sum(sum_amount) from (" + _createPettyCashQuery(" doc_date < " + MyLib._myGlobal._sqlDateFunction(__dateEnd)) + ") as temp6 ) as balance_end" +
                " from cb_petty_cash ";
            __query.Append(__querySum);
            return __query.ToString();
        }
    }
}
