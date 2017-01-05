using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace SMLProcess
{
    public class _smlICFrameWork : _g.SMLJavaWebService.SMLFrameWorkService
    {
        private string __webServiceFunctionName = "/SMLFrameWork";
        public _smlICFrameWork()
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

        public _smlICFrameWork(string webServiceName)
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
            MyLib._myGlobal._compressWebservice = (webServiceName.IndexOf(".com") != -1 || webServiceName.IndexOf(".net") != -1 || webServiceName.IndexOf("203.") != -1 || webServiceName.IndexOf("202.") != -1 || webServiceName.IndexOf(".info") != -1) ? true : false;
            // MyLib._myGlobal._compressWebservice = true; // ทดสอบการย่อ จะได้ดูว่ามี Bug หรือเปล่า
        }
    }

    public class _icProcess
    {
        // Flag คำนวณยอดคงเหลือ
        public String _stockBalanceFlag = "[12],[14],[16],[44],[46],[48],[54],[55],[56],[57],[58],[59],[60],[61],[62],[63],[64],[65],[66],[67],[68],[69],[70],[71],[72],[73],[310],[311]";
        // Flag เอกสารยกเลิก
        public String _stockBalanceCancelFlag = "[13],[15],[17],[45],[47],[49]";
        // Flag รวม
        public String _stockBalanceAllFlag = "";

        public _icProcess()
        {
            this._stockBalanceAllFlag = this._stockBalanceFlag + "," + this._stockBalanceCancelFlag;
        }

        /// <summary>
        /// Query ยอดคงเหลือสินค้า (ทีละตัว)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="fieldGroup"></param>
        /// <param name="fieldQty"></param>
        /// <param name="groupBy"></param>
        /// <param name="extraWhere"></param>
        /// <returns></returns>
        public string _queryItemBalance(string itemCode, string fieldGroup, string fieldQty, string groupBy, string extraWhere)
        {
            string __result = "select ";
            if (fieldGroup.Trim().Length > 0)
            {
                __result = __result + fieldGroup + ",";
            }
            __result = __result + "coalesce(sum((qty * calc_flag) * (stand_value/divide_value)),0) as " + fieldQty + " from ic_trans_detail where trans_flag in (" + this._stockBalanceFlag.Replace("[", "").Replace("]", "") + ") and last_status=0 and item_type in (0,4,6) and not (trans_flag in (14,311) and inquiry_type=1) and not (trans_flag in (16) and inquiry_type in (1,3)) and not (trans_flag in (46) and inquiry_type<>0) and not (trans_flag in (48) and inquiry_type>1) " + _wherefull_inv + " and item_code=\'" + itemCode + "\' " + extraWhere;
            if (groupBy.Trim().Length > 0)
            {
                __result = __result + " group by " + groupBy + " order by " + groupBy;
            }
            return __result;
        }

        /// <summary>ตัดใบกำกับภาษีอย่างเต็มออกแทน</summary>
        private string _wherefull_inv = " and not (trans_flag in (44) and ((select coalesce(is_pos, 0) from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) = 1) and ((select coalesce(doc_ref, '') from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag ) <> '') )";


        // toe limit 1 barcode ด้วย กันซ้ำ
        /// <summary>
        /// Query ยอดคงเหลือสินค้าจาก barcode (ทีละตัว)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="fieldGroup"></param>
        /// <param name="fieldQty"></param>
        /// <param name="groupBy"></param>
        /// <param name="extraWhere"></param>
        /// <returns></returns>
        public string _queryItemBarcodeBalance(string barcode, string fieldGroup, string fieldQty, string groupBy, string extraWhere)
        {
            string __result = "select ";

            if (fieldGroup.Trim().Length > 0)
            {
                __result = __result + fieldGroup + ",";
            }
            __result = __result + "coalesce(sum((qty * calc_flag) * (stand_value/divide_value)),0) as " + fieldQty + " from ic_trans_detail where trans_flag in (" + this._stockBalanceFlag.Replace("[", "").Replace("]", "") + ") and last_status=0 and item_type in (0,4,6) and not (trans_flag in (14,311) and inquiry_type=1) and not (trans_flag in (16) and inquiry_type in (1,3)) and not (trans_flag in (46) and inquiry_type<>0) and not (trans_flag in (48) and inquiry_type>1) " + _wherefull_inv + " and item_code=( select ic_inventory_barcode.ic_code from ic_inventory_barcode where ic_inventory_barcode.barcode = \'" + barcode + "\' limit 1) " + extraWhere;
            if (groupBy.Trim().Length > 0)
            {
                __result = __result + " group by " + groupBy + " order by " + groupBy;
            }
            return __result;
        }

        /// <summary>
        /// Query ยอดคงเหลือสินค้า (หลายตัวพร้อมกัน)
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="fieldGroup"></param>
        /// <param name="fieldQty"></param>
        /// <param name="groupBy"></param>
        /// <param name="extraWhere"></param>
        /// <returns></returns>
        public string _queryItemListBalance(string itemList, string fieldGroup, string fieldQty, string groupBy, string extraWhere)
        {
            string __result = "select ";
            if (fieldGroup.Trim().Length > 0)
            {
                __result = __result + fieldGroup + ",";
            }
            __result = __result + "coalesce(sum((qty * calc_flag) * (stand_value/divide_value)),0) as " + fieldQty + " from ic_trans_detail where trans_flag in (" + this._stockBalanceFlag.Replace("[", "").Replace("]", "") + ") and last_status=0 and item_type in (0,4,6) and not (trans_flag in (14,16,311) and inquiry_type=1) and not (trans_flag in (46) and inquiry_type<>0) and not (trans_flag in (48) and inquiry_type>1) " + _wherefull_inv + " and item_code in (" + itemList + ") " + extraWhere;
            if (groupBy.Trim().Length > 0)
            {
                __result = __result + " group by " + groupBy + " order by " + groupBy;
            }
            return __result;
        }

        public DataTable _process_ic_balance(string startItem, string endItem, string __whereString, string __orderByString)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            while (__myFrameWork._tryCount < 10)
            {
                __myFrameWork._tryCount++;
                try
                {
                    string __getTableString = "";
                    _smlICFrameWork __ws = new _smlICFrameWork();
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getTableString = __ws._process_ic_balance(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, startItem, endItem, __whereString, __orderByString);
                    }
                    else
                    {
                        byte[] __getByte = __ws._process_ic_balance_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, startItem, endItem, __whereString, __orderByString);
                        __getTableString = MyLib._compress._deCompressString(__getByte);
                    }
                    DataSet __ds = MyLib._myGlobal._convertStringToDataSet(__getTableString);
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                    return __ds.Tables[0];
                }
                catch (TimeoutException)
                {
                    __myFrameWork._tryConnect();
                }
                catch (WebException)
                {
                    __myFrameWork._tryConnect();
                }
            }
            return null;
        }

        public DataTable _process_ic_remain(string __whereString, string __orderByString)
        {

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            while (__myFrameWork._tryCount < 10)
            {
                __myFrameWork._tryCount++;
                try
                {
                    string __getTableString = "";
                    _smlICFrameWork __ws = new _smlICFrameWork();
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        //__getTableString = __ws._process_ic_remain(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, __whereString, __orderByString);
                    }
                    else
                    {
                        //byte[] __getByte = __ws._process_ic_remaimn_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, __whereString, __orderByString);
                        //__getTableString = MyLib._compress._deCompressString(__getByte);
                    }
                    DataSet __ds = MyLib._myGlobal._convertStringToDataSet(__getTableString);
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                    return __ds.Tables[0];
                }
                catch (TimeoutException)
                {
                    __myFrameWork._tryConnect();
                }
                catch (WebException)
                {
                    __myFrameWork._tryConnect();
                }
            }
            return null;
        }

        public ArrayList _process_ic_request(string query)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __ds = new DataSet();
            ArrayList __result = new ArrayList();
            bool __loopWebservice = true;
            while (__myFrameWork._tryCount < 10)
            {
                __myFrameWork._tryCount++;
                try
                {
                    string __getString = "";
                    string __getTableString = "";
                    __result.Clear();
                    _smlICFrameWork __ws = new _smlICFrameWork();
                    ////if (MyLib._myGlobal._compressWebservice == false)
                    ////{
                    ////    __getTableString = __ws._process_request(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, query);
                    ////}
                    ////else
                    ////{
                    ////    //byte[] __getByte = __ws._process_ic_balance_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, startItem, endItem, __whereString, __orderByString);
                    ////    //__getTableString = MyLib._compress._deCompressString(__getByte);
                    ////}
                    ////DataSet __ds = __myFrameWork._convertStringToDataSet(__getTableString);
                    ////__ws.Dispose();
                    ////int __xxx = __ds.Tables.Count;
                    ////return __ds.Tables[0];                    
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getString = __ws._process_request(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, query);
                    }
                    else
                    {
                        //byte[] __bcomp = MyLib._compress._compressString(query.ToString());
                        //byte[] __getByte = __ws._queryListGetDataCompress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, __bcomp);
                        //__getString = MyLib._compress._deCompressString(__getByte);
                    }
                    if (__getString.Length > 0)
                    {
                        try
                        {
                            XmlDocument __readXmlDoc = new XmlDocument();
                            __readXmlDoc.LoadXml(__getString);
                            XmlNodeList __tableNodes = __readXmlDoc.SelectNodes("//ResultSet");
                            foreach (XmlNode __getTableNode in __tableNodes)
                            {
                                DataSet __getData = new DataSet();
                                XmlTextReader __readTableXml = new XmlTextReader(new StringReader(string.Concat("<ResultSet>", __getTableNode.InnerXml, "</ResultSet>")));
                                try
                                {
                                    __getData.ReadXml(__readTableXml, XmlReadMode.InferSchema);
                                    if (__getData.Tables.Count == 0)
                                    {
                                        __getData.Tables.Add(new DataTable());
                                    }
                                }
                                catch
                                {
                                    // Debugger.Break();
                                    __getData.Tables.Add(new DataTable());
                                }
                                __result.Add(__getData);
                            }
                        }
                        catch
                        {
                            MessageBox.Show(__getString);
                        }
                        __loopWebservice = false;
                    }
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                }
                catch (TimeoutException)
                {
                    __myFrameWork._tryConnect();
                }
                catch (WebException)
                {
                    __myFrameWork._tryConnect();
                }
            }
            return (__result);
        }

        public ArrayList _process_ic_request_cancel()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __ds = new DataSet();
            ArrayList __result = new ArrayList();
            bool __loopWebservice = true;
            while (__myFrameWork._tryCount < 10)
            {
                __myFrameWork._tryCount++;
                try
                {
                    string __getString = "";
                    string __getTableString = "";
                    __result.Clear();
                    _smlICFrameWork __ws = new _smlICFrameWork();
                    ////if (MyLib._myGlobal._compressWebservice == false)
                    ////{
                    ////    __getTableString = __ws._process_request(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, query);
                    ////}
                    ////else
                    ////{
                    ////    //byte[] __getByte = __ws._process_ic_balance_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, startItem, endItem, __whereString, __orderByString);
                    ////    //__getTableString = MyLib._compress._deCompressString(__getByte);
                    ////}
                    ////DataSet __ds = __myFrameWork._convertStringToDataSet(__getTableString);
                    ////__ws.Dispose();
                    ////int __xxx = __ds.Tables.Count;
                    ////return __ds.Tables[0];                    
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        __getString = __ws._process_request_cancel(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName);
                    }
                    else
                    {
                        //byte[] __bcomp = MyLib._compress._compressString(query.ToString());
                        //byte[] __getByte = __ws._queryListGetDataCompress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, __bcomp);
                        //__getString = MyLib._compress._deCompressString(__getByte);
                    }
                    if (__getString.Length > 0)
                    {
                        try
                        {
                            XmlDocument __readXmlDoc = new XmlDocument();
                            __readXmlDoc.LoadXml(__getString);
                            XmlNodeList __tableNodes = __readXmlDoc.SelectNodes("//ResultSet");
                            foreach (XmlNode __getTableNode in __tableNodes)
                            {
                                DataSet __getData = new DataSet();
                                XmlTextReader __readTableXml = new XmlTextReader(new StringReader(string.Concat("<ResultSet>", __getTableNode.InnerXml, "</ResultSet>")));
                                try
                                {
                                    __getData.ReadXml(__readTableXml, XmlReadMode.InferSchema);
                                    if (__getData.Tables.Count == 0)
                                    {
                                        __getData.Tables.Add(new DataTable());
                                    }
                                }
                                catch
                                {
                                    // Debugger.Break();
                                    __getData.Tables.Add(new DataTable());
                                }
                                __result.Add(__getData);
                            }
                        }
                        catch
                        {
                            MessageBox.Show(__getString);
                        }
                        __loopWebservice = false;
                    }
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                }
                catch (TimeoutException)
                {
                    __myFrameWork._tryConnect();
                }
                catch (WebException)
                {
                    __myFrameWork._tryConnect();
                }
            }
            return (__result);
        }

        public event QueryStreamEventHandler __queryStreamEvent;

        public byte[] _queryRemainStreamGet(string reportGuid, int blockSize, int blockNumber)
        {
            _smlICFrameWork __ws = new _smlICFrameWork();
            return __ws._queryRemainGetStream(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, reportGuid, blockSize, blockNumber);
        }

        byte[] _appendArrays(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length]; // just one array allocation
            Buffer.BlockCopy(a, 0, c, 0, a.Length);
            Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        public DataSet _queryRemainStream(string databaseName, string where, string orderby, string message)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __resultDataSet = null;
            long __fileSize = 0;
            if (__myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.PostgreSql && where.ToLower().IndexOf("isnull") != -1)
            {
                MessageBox.Show("ISNULL ไม่สามารถใช้ได้กับ PostgreSQL");
            }
            if (MyLib._myGlobal._webServiceServer == null)
            {
                return null;
            }
            bool __loopWebservice = true;
            __myFrameWork._tryCount = 0;
            while (__loopWebservice == true && __myFrameWork._tryCount < 10)
            {
                try
                {
                    _smlICFrameWork __ws = new _smlICFrameWork();
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
                    __fileSize = MyLib._myGlobal._intPhase(__ws._queryRemainStream(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, __reportGuid, databaseName, where));
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
                        byte[] __getData = _queryRemainStreamGet(__reportGuid, __blockSize, __loop);
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
                    __myFrameWork._tryConnect();
                }
                catch (WebException)
                {
                    __myFrameWork._tryConnect();
                }
                catch (XmlException)
                {
                    return null;
                }
            }
            return (__resultDataSet);
        }

        public string _createQueryStatus()
        {
            StringBuilder __query = new StringBuilder();

            // calc_type 1 ซื้อ

            // calc_type 2 เพิ่มหนี้

            // calc_type 3 ส่งคืน

            // calc_type 4 ขาย

            // calc_type 5 เพิ่มหนี้/เพิ่มสินค้า

            // calc_type 6 ลดหนี้

            // calc_type 7 ยกมา

            // calc_type 8 เบิก

            // calc_type 9 รับคืนจากเบิก

            // calc_type 10 รับสำเร็จรูป

            // calc_type 11 ปรับปรุงเกิน

            // calc_type 12 ปรับปรุงขาด
            
            return __query.ToString();
        }
    }

    public delegate void QueryStreamEventHandler(string lastMessage, int persentProcess);
}
