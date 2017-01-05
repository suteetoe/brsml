using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPGlobal
{
    public class _smlFrameWorkService : _g.SMLJavaWebService.SMLFrameWorkService
    {
        public _smlFrameWorkService()
        {
            this.Url = "http://" + MyLib._myGlobal._webServiceServer + "/" + MyLib._myGlobal._webServiceName + "/SMLFrameWork";
            _checkCompress(MyLib._myGlobal._webServiceServer);
        }

        public _smlFrameWorkService(string webServiceName)
        {
            this.Url = "http://" + webServiceName + "/" + MyLib._myGlobal._webServiceName + "/SMLFrameWork";
            _checkCompress(webServiceName);
            MyLib._myGlobal._webServiceServer = webServiceName;
        }

        private void _checkCompress(string webServiceName)
        {
            MyLib._myGlobal._compressWebservice = (webServiceName.IndexOf(".com") != -1 || webServiceName.IndexOf(".net") != -1 || webServiceName.IndexOf("203.") != -1 || webServiceName.IndexOf("202.") != -1 || webServiceName.IndexOf(".info") != -1) ? true : false;
        }
    }

    public class _smlFrameWork : MyLib._myFrameWork
    {
        public string _lastError = "";

        /* ย้ายไปไว้ใน global
         * public String _getItemRepack(ArrayList source)
        {
            ArrayList __buildList = new ArrayList();
            StringBuilder __result = new StringBuilder();
            for (int __loop1 = 0; __loop1 < source.Count; __loop1++)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __buildList.Count; __loop2++)
                {
                    if (__buildList[__loop2].ToString().Equals(source[__loop1].ToString()))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    if (source[__loop1].ToString().Trim().Length > 0)
                    {
                        if (__result.Length > 0)
                        {
                            __result.Append(",");
                        }
                        __result.Append("\'" + source[__loop1].ToString() + "\'");
                        __buildList.Add(source[__loop1].ToString());
                    }
                }
            }
            return __result.ToString();
        }*/

        public void _processChqCreditCardCurrentDatabase()
        {
            _processChqCreditCard(MyLib._myGlobal._databaseName);
        }

        public string _processChqCreditCard(string databaseName)
        {
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            _smlFrameWorkService __ws = new _smlFrameWorkService();
            while (__loopWebservice == true && this._tryCount < 10)
            {
                this._lastError = "";
                try
                {
                    __result = __ws._processChqCreditCard(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName);
                    __loopWebservice = false;
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
                catch
                {
                    this._lastError = __result;
                }
                __ws.Dispose();
            }
            return __result;
        }

        public string _process_stock_cost_finish(string databaseName)
        {
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            _smlFrameWorkService __ws = new _smlFrameWorkService();
            while (__loopWebservice == true && this._tryCount < 10)
            {
                this._lastError = "";
                try
                {
                    __result = __ws._process_stock_cost_finish(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName);
                    __loopWebservice = false;
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
                catch
                {
                    this._lastError = __result;
                }
                __ws.Dispose();
            }
            return __result;
        }

        /// <summary>
        /// คำนวณ,รายงานเคลื่อนไหวสินค้า
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="itemCode"></param>
        /// <param name="processMode">0=ประมวล,1=รายงานตัวเดียว,2=รายงานตัวเดียวแล้วเอาไปประกอบ XML เอง,10=ประมวลมีต้นทุนแฝง,11=รายงานตัวเดียวมีต้นทุนแฝง,12=รายงานตัวเดียวมีต้นทุนแฝงแล้วเอาไปประกอบ XML เอง</param>
        /// <returns></returns>
        public string _process_stock_cost(string databaseName, string itemCode, int processMode, string dateBegin, string dateEnd)
        {
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            _smlFrameWorkService __ws = new _smlFrameWorkService();
            while (__loopWebservice == true && this._tryCount < 10)
            {
                this._lastError = "";
                try
                {
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __result = __ws._process_stock_cost(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, itemCode, processMode, dateBegin, dateEnd);
                    }
                    else
                    {
                        byte[] __bcomp = MyLib._compress._compressString(itemCode);
                        byte[] __resultByte = __ws._process_stock_cost_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, __bcomp, processMode, dateBegin, dateEnd);
                        __result = MyLib._compress._deCompressString(__resultByte);
                    }
                    __loopWebservice = false;
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
                catch
                {
                    this._lastError = __result;
                }
                __ws.Dispose();
            }
            return __result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=ตามสินค้า,1=ตามเอกสาร,2=ตามลูกค้า</param>
        /// <param name="databaseName"></param>
        /// <param name="itemCodeBegin"></param>
        /// <param name="itemCodeEnd"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="__reportGuid"></param>
        /// <returns></returns>
        public string _process_profit_and_lost_by_product_stream(int mode, string databaseName, string itemCodeBegin, string itemCodeEnd, string custCodeBegin, string custCodeEnd, string dateBegin, string dateEnd, string itemCodeList, string custCodeList, string __reportGuid)
        {
            _smlFrameWorkService __ws = new _smlFrameWorkService();
            return __ws._process_profit_and_lost_by_product_stream(mode, MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, itemCodeBegin, itemCodeEnd, custCodeBegin, custCodeEnd, MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(dateBegin)), MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(dateEnd)), itemCodeList, custCodeList, __reportGuid);
        }

        public string _process_profit_and_lost_by_color_stream(int mode, string databaseName, string itemCodeBegin, string itemCodeEnd, string custCodeBegin, string custCodeEnd, string dateBegin, string dateEnd, string itemCodeList, string custCodeList, string __reportGuid)
        {
            _smlFrameWorkService __ws = new _smlFrameWorkService();
            return __ws._process_profit_and_lost_by_color_stream(mode, MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, itemCodeBegin, itemCodeEnd, custCodeBegin, custCodeEnd, MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(dateBegin)), MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(dateEnd)), itemCodeList, custCodeList, __reportGuid);
        }

        /// <summary>
        /// คำนวณยอดคงเหลือสินค้า
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="itemList">รหัสสินค้า</param>
        /// <param name="calcFlag"></param>
        /// <returns></returns>
        public string _process_stock_balance(string databaseName, string itemList, string calcFlag)
        {
            ArrayList __itemList = new ArrayList();
            string[] __itemListSplit = itemList.Split(',');
            for (int __loop = 0; __loop < __itemListSplit.Length; __loop++)
            {
                __itemList.Add(__itemListSplit[__loop].ToString());
            }
            while (__itemList.Count > 0)
            {
                StringBuilder __itemListStr = new StringBuilder();
                for (int __loop = 0; __loop < 10 && __itemList.Count > 0; __loop++)
                {
                    if (__itemListStr.Length > 0)
                    {
                        __itemListStr.Append(",");
                    }
                    __itemListStr.Append(__itemList[0].ToString());
                    __itemList.RemoveAt(0);
                }
                this._process_stock_balance_run(databaseName, __itemListStr.ToString(), calcFlag);
            }
            return "";
        }

        public string _process_stock_balance_run(string databaseName, string itemList, string calcFlag)
        {
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            _smlFrameWorkService __ws = new _smlFrameWorkService();
            while (__loopWebservice == true && this._tryCount < 10)
            {
                this._lastError = "";
                try
                {
                    /*string __itemCode = (itemList.Length == 0) ? "" : _g.d.ic_trans_detail._item_code + " in (" + itemList + ") and ";
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "update ic_trans_detail set item_type=(select item_type from ic_inventory where ic_inventory.code=ic_trans_detail.item_code) where " + __itemCode + "(item_type<>(select item_type from ic_inventory where ic_inventory.code=ic_trans_detail.item_code))";
                    __myFrameWork._queryInsertOrUpdate(databaseName, __query);*/
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __result = __ws._process_stock_balance(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, itemList, calcFlag);
                    }
                    else
                    {
                        byte[] __bcomp = MyLib._compress._compressString(itemList);
                        byte[] __resultByte = __ws._process_stock_balance_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, __bcomp, calcFlag);
                        __result = MyLib._compress._deCompressString(__resultByte);
                    }
                    __loopWebservice = false;
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
                catch
                {
                    this._lastError = __result;
                }
                __ws.Dispose();
            }
            return __result;
        }

        public string _purchase_permium_process(string databaseName, string dateProcess, string source)
        {
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            _smlFrameWorkService __ws = new _smlFrameWorkService();
            while (__loopWebservice == true && this._tryCount < 10)
            {
                this._lastError = "";
                try
                {
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __result = __ws._purchase_permium_process(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateProcess, source);
                    }
                    else
                    {
                        byte[] __bcomp = MyLib._compress._compressString(source);
                        byte[] __resultByte = __ws._purchase_permium_process_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateProcess, __bcomp);
                        __result = MyLib._compress._deCompressString(__resultByte);
                    }
                    __loopWebservice = false;
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
                catch
                {
                    this._lastError = __result;
                }
                __ws.Dispose();
            }
            return __result;
        }

        public MyLib._queryReturn _process_ap_trans(string databaseName, string dateBegin, string dateEnd, string queryWhere, string queryWhere2, string orderby, int reportType, int trans_flag, int startRecord, int maxRecords)
        {
            XmlTextReader __readXml = null;
            MyLib._queryReturn __dsRet = new MyLib._queryReturn();
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                this._lastError = "";
                _smlFrameWorkService __ws = new _smlFrameWorkService();
                try
                {
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __result = __ws._processApTrans(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateBegin, dateEnd, queryWhere, queryWhere2, orderby, reportType, trans_flag, startRecord, maxRecords);
                        if (__result.Length > 0)
                        {
                            int __findComma = __result.IndexOf(',');
                            string __getLength = __result.Substring(0, __findComma);
                            string __getString = __result.Substring(__findComma + 1);
                            __readXml = new XmlTextReader(new StringReader(__getString));
                            __dsRet.totalRecord = 0;
                            __dsRet.totalRecord = MyLib._myGlobal._intPhase(__getLength);
                        }
                    }
                    else
                    {
                        byte[] __resultByte = __ws._processApTransCompress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateBegin, dateEnd, queryWhere, queryWhere2, orderby, reportType, trans_flag, startRecord, maxRecords);
                        __result = MyLib._compress._deCompressString(__resultByte);
                        if (__result.Length > 0)
                        {
                            int __findComma = __result.IndexOf(',');
                            string __getLength = __result.Substring(0, __findComma);
                            string __getString = __result.Substring(__findComma + 1);
                            __readXml = new XmlTextReader(new StringReader(__getString));
                            __dsRet.totalRecord = 0;
                            __dsRet.totalRecord = MyLib._myGlobal._intPhase(__getLength);
                        }
                    }
                    // Convert MDataSet into a standard ADO.NET DataSet
                    __dsRet.detail = new DataSet();
                    try
                    {
                        __dsRet.detail.ReadXml(__readXml, XmlReadMode.InferSchema);
                        if (__dsRet.detail.Tables.Count == 0)
                        {
                            DataTable __table = new DataTable();
                            __dsRet.detail.Tables.Add(__table);
                        }
                    }
                    catch
                    {
                        // Debugger.Break();
                        DataTable __table = new DataTable();
                        __dsRet.detail.Tables.Add(__table);
                    }
                    __loopWebservice = false;
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
                catch
                {
                    this._lastError = __result;
                    DataTable __table = new DataTable();
                    __dsRet.detail = new DataSet();
                    __dsRet.detail.Tables.Add(__table);
                    return __dsRet;
                }
                __ws.Dispose();
            }
            return (__dsRet);
        }

        public DataSet _processArStatus(string databaseName, string dateBegin, string dateEnd, string where, string orderBy, int reportType)
        {
            DataSet __ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable __table = new DataTable();
                __ds.Tables.Add(__table);
                return __ds;
            }
            bool __loopWebservice = true;
            while (__loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    string __getString = "";
                    _smlFrameWorkService __ws = new _smlFrameWorkService();
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getString = __ws._process_ar_status(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateBegin, dateEnd, where, orderBy, reportType);
                    }
                    else
                    {
                        byte[] __getByte = __ws._process_ar_status_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateBegin, dateEnd, where, orderBy, reportType);
                        __getString = MyLib._compress._deCompressString(__getByte);
                    }
                    __ds = MyLib._myGlobal._convertStringToDataSet(__getString);
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
                    DataTable __table = new DataTable();
                    __ds.Tables.Add(__table);
                    return __ds;
                }
            }
            return (__ds);
        }

        public DataSet _processArAgeing(string databaseName, string dateEnd, int day1, int day2, int day3, int day4, string where, string orderBy)
        {
            DataSet __ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable __table = new DataTable();
                __ds.Tables.Add(__table);
                return __ds;
            }
            bool __loopWebservice = true;
            while (__loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    string __getString = "";
                    _smlFrameWorkService __ws = new _smlFrameWorkService();
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getString = __ws._process_ar_ageing(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateEnd, day1.ToString(), day2.ToString(), day3.ToString(), day4.ToString(), where, orderBy);
                    }
                    else
                    {
                        byte[] __getByte = __ws._process_ar_ageing_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateEnd, day1.ToString(), day2.ToString(), day3.ToString(), day4.ToString(), where, orderBy);
                        __getString = MyLib._compress._deCompressString(__getByte);
                    }
                    __ds = MyLib._myGlobal._convertStringToDataSet(__getString);
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
                    DataTable __table = new DataTable();
                    __ds.Tables.Add(__table);
                    return __ds;
                }
            }
            return (__ds);
        }

        // เจ้าหนี้
        public MyLib._queryReturn _processApStatus(string databaseName, string dateEnd, string where, string where2, string orderBy, int reportType, int startRecord, int maxRecords)
        {
            XmlTextReader __readXml = null;
            MyLib._queryReturn __dsRet = new MyLib._queryReturn();
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                this._lastError = "";
                _smlFrameWorkService __ws = new _smlFrameWorkService();
                try
                {
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __result = __ws._process_ap_status(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateEnd, where, where2, orderBy, reportType, startRecord, maxRecords);
                        if (__result.Length > 0)
                        {
                            int __findComma = __result.IndexOf(',');
                            string __getLength = __result.Substring(0, __findComma);
                            string __getString = __result.Substring(__findComma + 1);
                            __readXml = new XmlTextReader(new StringReader(__getString));
                            __dsRet.totalRecord = 0;
                            __dsRet.totalRecord = MyLib._myGlobal._intPhase(__getLength);
                        }
                    }
                    else
                    {
                        byte[] __resultByte = __ws._process_ap_status_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateEnd, where, where2, orderBy, reportType, startRecord, maxRecords);
                        __result = MyLib._compress._deCompressString(__resultByte);
                        if (__result.Length > 0)
                        {
                            int __findComma = __result.IndexOf(',');
                            string __getLength = __result.Substring(0, __findComma);
                            string __getString = __result.Substring(__findComma + 1);
                            __readXml = new XmlTextReader(new StringReader(__getString));
                            __dsRet.totalRecord = 0;
                            __dsRet.totalRecord = MyLib._myGlobal._intPhase(__getLength);
                        }
                    }
                    // Convert MDataSet into a standard ADO.NET DataSet
                    __dsRet.detail = new DataSet();
                    try
                    {
                        __dsRet.detail.ReadXml(__readXml, XmlReadMode.InferSchema);
                        if (__dsRet.detail.Tables.Count == 0)
                        {
                            DataTable __table = new DataTable();
                            __dsRet.detail.Tables.Add(__table);
                        }
                    }
                    catch
                    {
                        // Debugger.Break();
                        DataTable __table = new DataTable();
                        __dsRet.detail.Tables.Add(__table);
                    }
                    __loopWebservice = false;
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
                catch
                {
                    this._lastError = __result;
                    DataTable __table = new DataTable();
                    __dsRet.detail = new DataSet();
                    __dsRet.detail.Tables.Add(__table);
                    return __dsRet;
                }
                __ws.Dispose();
            }
            return (__dsRet);
            /*MyLib._queryReturn __ds = new MyLib._queryReturn();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable __table = new DataTable();
                __ds.Tables.Add(__table);
                return __ds;
            }
            bool __loopWebservice = true;
            string __getString = "";
            while (__loopWebservice == true && _tryCount < 10)
            {
                this._lastError = "";
                try
                {
                    _smlFrameWorkService __ws = new _smlFrameWorkService();
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getString = __ws._process_ap_status(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateBegin, dateEnd, where, where2, orderBy, reportType,startRecord,maxRecords);
                    }
                    else
                    {
                        byte[] __getByte = __ws._process_ap_status_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateBegin, dateEnd, where, where2, orderBy, reportType,startRecord,maxRecords);
                        __getString = MyLib._compress._deCompressString(__getByte);
                    }
                    __ds = _convertStringToDataSet(__getString);
                    __loopWebservice = false;
                    __ws.Dispose();
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
                    this._lastError = __getString;
                    DataTable __table = new DataTable();
                    __ds.Tables.Add(__table);
                    return __ds;
                }
            }
            return (__ds);*/
        }

        public DataSet _processApAgeing(string databaseName, string dateEnd, int day1, int day2, int day3, int day4, string where, string orderBy)
        {
            DataSet __ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable __table = new DataTable();
                __ds.Tables.Add(__table);
                return __ds;
            }
            bool __loopWebservice = true;
            while (__loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    string __getString = "";
                    _smlFrameWorkService __ws = new _smlFrameWorkService();
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getString = __ws._process_ap_ageing(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateEnd, day1.ToString(), day2.ToString(), day3.ToString(), day4.ToString(), where, orderBy);
                    }
                    else
                    {
                        byte[] __getByte = __ws._process_ap_ageing_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, dateEnd, day1.ToString(), day2.ToString(), day3.ToString(), day4.ToString(), where, orderBy);
                        __getString = MyLib._compress._deCompressString(__getByte);
                    }
                    __ds = MyLib._myGlobal._convertStringToDataSet(__getString);
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
                    DataTable __table = new DataTable();
                    __ds.Tables.Add(__table);
                    return __ds;
                }
            }
            return (__ds);
        }

        public void _process(string guid)
        {
            try
            {
                _smlFrameWorkService __ws = new _smlFrameWorkService();
                __ws._process(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, guid);
                __ws.Dispose();
            }
            catch
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.process._table + " where " + _g.d.process._last_guid + "=\'" + guid + "\'");
            }
        }

        public void _processKiller()
        {
            try
            {
                _smlFrameWorkService __ws = new _smlFrameWorkService();
                __ws._processKiller(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName);
                __ws.Dispose();
            }
            catch
            {
            }
        }
    }
}
