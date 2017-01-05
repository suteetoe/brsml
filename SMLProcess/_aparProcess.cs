using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Xml;
using System.Windows.Forms;

namespace SMLProcess
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

    public class _aparProcess : MyLib._myFrameWork
    {
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
        public DataSet _processApStatus(string databaseName, string dateEnd, string where, string where2, string orderBy, int reportType, int startRecord, int maxRecords)
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
                        __getString = __ws._process_ap_status(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName,  dateEnd, where, where2, orderBy, reportType,startRecord,maxRecords);
                    }
                    else
                    {
                        byte[] __getByte = __ws._process_ap_status_compress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName,  dateEnd, where, where2, orderBy, reportType,startRecord,maxRecords);
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        public event QueryStreamEventHandler __queryStreamEvent;
        public delegate void QueryStreamEventHandler(string lastMessage, int persentProcess);

        byte[] _appendArrays(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length]; // just one array allocation
            Buffer.BlockCopy(a, 0, c, 0, a.Length);
            Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        public DataSet _queryStream(string _querywhere,string _orderby , int _reporttype)
        {
            DataSet __resultDataSet = null;
            try
            {
                long __fileSize = 0;
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
                        string __reportGuid = "AR-"+Guid.NewGuid().ToString().ToLower();
                        //
                        if (__queryStreamEvent != null)
                        {
                            try
                            {
                                __queryStreamEvent(" AR: Server process query...", 0);
                            }
                            catch
                            {
                            }
                        }
                        __fileSize = MyLib._myGlobal._intPhase(__ws._process_ar_stream(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, __reportGuid, _querywhere, _orderby, _reporttype));
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
                                    __queryStreamEvent(String.Format(" AR : Load data from Server ({0}/{1}) : Total file size {2}Kb", __countLoop, __calcBlock, __fileSize / 1024), __persent);
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
            }
            catch
            {
            }
            return (__resultDataSet);

        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------

    }
}