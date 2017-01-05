using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;
using System.Diagnostics;
using MyLib;

namespace SMLERPGlobal
{
    public class _myWebservice_PO : _g.SMLJAVAWS_PO1.SMLFrameWorkService
    {
        public _myWebservice_PO()
        {
            this.Url = "http://" + MyLib._myGlobal._webServiceServer + "/" + MyLib._myGlobal._webServiceName + "/SMLFrameWork";
            _checkCompress(MyLib._myGlobal._webServiceServer);
        }

        public _myWebservice_PO(string webServiceName)
        {
            this.Url = "http://" + webServiceName + "/" + MyLib._myGlobal._webServiceName + "/SMLFrameWork";
            _checkCompress(webServiceName);
            MyLib._myGlobal._webServiceServer = webServiceName;
        }

        private void _checkCompress(string webServiceName)
        {
            _myGlobal._compressWebservice = (webServiceName.IndexOf(".com") != -1 || webServiceName.IndexOf(".net") != -1 || webServiceName.IndexOf("203.") != -1 || webServiceName.IndexOf("202.") != -1 || webServiceName.IndexOf(".info") != -1) ? true : false;
        }
    }

    //public class _myResourceTypeSML
    //{
    //    public string _str;
    //    public int _length;
    //}

    //public class _queryReturnSML
    //{
    //    public int totalRecord;
    //    public DataSet detail;
    //}



    public class _mySMLFrameWork : _myWebservice
    {
        public int _tryCount = 0;
        public void _tryConnect()
        {
            _tryCount++;
            Debug.WriteLine("Try Connect");
            _disableWebserviceServer(MyLib._myGlobal._webServiceServer);
            bool result = false;
            for (int loop = 0; loop < _myGlobal._webServiceServerList.Count && result == false; loop++)
            {
                _myWebserviceType getData = (_myWebserviceType)_myGlobal._webServiceServerList[loop];
                if (getData._webServiceConnected)
                {
                    result = true;
                }
            }
            if (result == false)
            {
                _webserviceServerReConnect(false);
            }
            MyLib._myGlobal._webServiceServer = _findWebserviceServer(MyLib._myGlobal._webServiceServerList, MyLib._myGlobal._webServiceServer);
        }
        public string _packTableFromUnlock()
        {
            StringBuilder result = new StringBuilder();
            for (int loop = 0; loop < _myGlobal._tableForAutoUnlock.Count; loop++)
            {
                if (loop != 0)
                {
                    result.Append(",");
                }
                result.Append(_myGlobal._tableForAutoUnlock[loop]);
            }
            return (result.ToString());
        }
        public void _webserviceServerReConnect(bool fastMode)
        {
            int __countWebservice = 0;
            if (_myGlobal._webServiceServerList.Count == 1)
            {
                _myWebserviceType __data = (_myWebserviceType)_myGlobal._webServiceServerList[0];
                __data._webServiceConnected = true;
                __data._connectBytesPerSecond = 1000;
                _myGlobal._webServiceServerList[0] = (_myWebserviceType)__data;
                return;
            }

            for (int __loop = 0; __loop < _myGlobal._webServiceServerList.Count; __loop++)
            {
                _myWebserviceType __data = (_myWebserviceType)_myGlobal._webServiceServerList[__loop];
                try
                {
                    _myWebservice ws = new _myWebservice(__data._webServiceName);
                    ws.Timeout = 10000;
                    string result = ws._checkConnect();
                    ws.Dispose();
                    __data._webServiceConnected = true;
                    __countWebservice++;
                    Debug.WriteLine("_webserviceServerReConnect : Pass");
                    if (fastMode)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("_webserviceServerReConnect : Fail - " + ex.Message);
                    __data._webServiceConnected = false;
                    __data._connectBytesPerSecond = 0;
                }
                _myGlobal._webServiceServerList[__loop] = (_myWebserviceType)__data;
            }
            if (__countWebservice > 1)
            {
                for (int loop = 0; loop < _myGlobal._webServiceServerList.Count; loop++)
                {
                    _myWebserviceType data = (_myWebserviceType)_myGlobal._webServiceServerList[loop];
                    try
                    {
                        _myWebservice ws = new _myWebservice(data._webServiceName);
                        ws.Timeout = 10000;
                        DateTime start = DateTime.Now;
                        string result = ws._checkConnectSpeed();
                        DateTime stop = DateTime.Now;
                        data._webServiceConnected = true;
                        data._connectBytesPerSecond = _calcSpeedWebservice(start, stop, result.Length);
                        __countWebservice++;
                        ws.Dispose();
                    }
                    catch
                    {
                        data._webServiceConnected = false;
                        data._connectBytesPerSecond = 0;
                    }
                    _myGlobal._webServiceServerList[loop] = (_myWebserviceType)data;
                }
            }
        }
        public void _disableWebserviceServer(string webserviceName)
        {
            for (int loop = 0; loop < _myGlobal._webServiceServerList.Count; loop++)
            {
                _myWebserviceType getData = (_myWebserviceType)_myGlobal._webServiceServerList[loop];
                if (getData._webServiceName.CompareTo(webserviceName) == 0)
                {
                    getData._webServiceConnected = false;
                    _myGlobal._webServiceServerList[loop] = (_myWebserviceType)getData;
                }
            }
        }
        public string _findWebserviceServer(ArrayList webServiceList, string oldName)
        {
            int foundCount = 0;
            if (oldName.Length > 0)
            {
                if (webServiceList.Count <= 1)
                {
                    return (oldName);
                }
            }
            bool found = false;
            for (int loop = 0; loop < webServiceList.Count; loop++)
            {
                if (((_myWebserviceType)webServiceList[loop])._webServiceConnected == true)
                {
                    found = true;
                    foundCount++;
                }
            }
            if (found == false)
            {
                return (oldName);
            }
            string result = "";
            int goodServer = -1;
            double calcMax = -1;
            double firstSpeed = -1;
            bool isCompareAll = true;
            for (int loop = 0; loop < webServiceList.Count; loop++)
            {
                _myWebserviceType getData = (_myWebserviceType)webServiceList[loop];
                if (getData._webServiceConnected)
                {
                    if (getData._connectBytesPerSecond > calcMax)
                    {
                        if (calcMax == -1)
                        {
                            firstSpeed = getData._connectBytesPerSecond;
                        }
                        calcMax = getData._connectBytesPerSecond;
                        goodServer = loop;
                    }
                    if (getData._connectBytesPerSecond != firstSpeed)
                    {
                        isCompareAll = false;
                    }
                }
            }
            if (isCompareAll == true)
            {
                _myWebserviceType getDataServer = (_myWebserviceType)webServiceList[goodServer];
                if (getDataServer._webServiceName.CompareTo(oldName) == 0)
                {
                    bool findWebservice = false;
                    while (findWebservice == false)
                    {
                        Random getNumber = new Random();
                        int randomNumber = getNumber.Next(webServiceList.Count);
                        _myWebserviceType getData = (_myWebserviceType)webServiceList[randomNumber];
                        if (getData._webServiceConnected)
                        {
                            goodServer = randomNumber;
                            break;
                        }
                    }
                }
            }
            _myWebserviceType getNewDataServer = (_myWebserviceType)webServiceList[goodServer];
            result = getNewDataServer._webServiceName;
            return result;
        }
        private int _calcSpeedWebservice(DateTime begin, DateTime end, double dataSize)
        {
            int calcBytePerMilliSecond = (int)_myUtil._dateDiff("second", begin, end) + 1;
            return (int)(dataSize / calcBytePerMilliSecond);
        }

        public DataSet _dePositGetquery(string databaseName, string transection_flag)
        {
            DataSet ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable table = new DataTable();
                ds.Tables.Add(table);
                return ds;
            }
            bool loopWebservice = true;
            _tryCount = 0;
            while (loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    string getString = "";
                    _myWebservice_PO ws = new _myWebservice_PO();
                    Debug.Write("_query:" + MyLib._myGlobal._webServiceServer + ":" + databaseName + ":" + transection_flag);
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        getString = ws._dePositGetquery(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, transection_flag);
                    }
                    else
                    {
                        byte[] bcomp = MyLib._compress._compressString(transection_flag.ToString());
                        byte[] getByte = ws._dePositGetqueryCompress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, bcomp);
                        getString = MyLib._compress._deCompressString(getByte);
                    }
                    XmlTextReader readXml = new XmlTextReader(new StringReader(getString));
                    // Convert MDataSet into a standard ADO.NET DataSet
                    ds.ReadXml(readXml, XmlReadMode.InferSchema);
                    if (ds.Tables.Count == 0)
                    {
                        DataTable table = new DataTable();
                        ds.Tables.Add(table);
                    }
                    loopWebservice = false;
                    ws.Dispose();
                    Debug.WriteLine(" --> Query Success.");
                }
                catch (TimeoutException)
                {
                    _tryConnect();
                }
                catch (WebException)
                {
                    _tryConnect();
                }
            }
            return (ds);
        }

        public DataSet _dePositGetquerySearch(string databaseName, string queryForsearch)
        {
            DataSet ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable table = new DataTable();
                ds.Tables.Add(table);
                return ds;
            }
            bool loopWebservice = true;
            _tryCount = 0;
            while (loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    string getString = "";
                    _myWebservice_PO ws = new _myWebservice_PO();
                    Debug.Write("_query:" + MyLib._myGlobal._webServiceServer + ":" + databaseName + ":" + queryForsearch);
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        getString = ws._dePositGetquerySearch(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, queryForsearch);
                    }
                    else
                    {
                        byte[] bcomp = MyLib._compress._compressString(queryForsearch.ToString());
                        byte[] getByte = ws._dePositGetquerySearchCompress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, bcomp);
                        getString = MyLib._compress._deCompressString(getByte);
                    }
                    XmlTextReader readXml = new XmlTextReader(new StringReader(getString));
                    // Convert MDataSet into a standard ADO.NET DataSet
                    ds.ReadXml(readXml, XmlReadMode.InferSchema);
                    if (ds.Tables.Count == 0)
                    {
                        DataTable table = new DataTable();
                        ds.Tables.Add(table);
                    }
                    loopWebservice = false;
                    ws.Dispose();
                    Debug.WriteLine(" --> Query Success.");
                }
                catch (TimeoutException)
                {
                    _tryConnect();
                }
                catch (WebException)
                {
                    _tryConnect();
                }
            }
            return (ds);
        }

        public DataSet _invoiceGetquery(string databaseName, string transection_flag)
        {
            DataSet ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable table = new DataTable();
                ds.Tables.Add(table);
                return ds;
            }
            bool loopWebservice = true;
            _tryCount = 0;
            while (loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    string getString = "";
                    _myWebservice_PO ws = new _myWebservice_PO();
                    Debug.Write("_query:" + MyLib._myGlobal._webServiceServer + ":" + databaseName + ":" + transection_flag);
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        getString = ws._invoiceGetquery(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, transection_flag);
                    }
                    else
                    {
                        byte[] bcomp = MyLib._compress._compressString(transection_flag.ToString());
                        byte[] getByte = ws._invoiceGetqueryCompress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, bcomp);
                        getString = MyLib._compress._deCompressString(getByte);
                    }
                    XmlTextReader readXml = new XmlTextReader(new StringReader(getString));
                    // Convert MDataSet into a standard ADO.NET DataSet
                    ds.ReadXml(readXml, XmlReadMode.InferSchema);
                    if (ds.Tables.Count == 0)
                    {
                        DataTable table = new DataTable();
                        ds.Tables.Add(table);
                    }
                    loopWebservice = false;
                    ws.Dispose();
                    Debug.WriteLine(" --> Query Success.");
                }
                catch (TimeoutException)
                {
                    _tryConnect();
                }
                catch (WebException)
                {
                    _tryConnect();
                }
            }
            return (ds);
        }

        public DataSet _invoiceGetquerySearch(string databaseName, string queryForsearch)
        {
            DataSet ds = new DataSet();
            if (MyLib._myGlobal._webServiceServer == null)
            {
                DataTable table = new DataTable();
                ds.Tables.Add(table);
                return ds;
            }
            bool loopWebservice = true;
            _tryCount = 0;
            while (loopWebservice == true && _tryCount < 10)
            {
                try
                {
                    string getString = "";
                    _myWebservice_PO ws = new _myWebservice_PO();
                    Debug.Write("_query:" + MyLib._myGlobal._webServiceServer + ":" + databaseName + ":" + queryForsearch);
                    if (MyLib._myGlobal._compressWebservice == false)
                    {
                        getString = ws._invoiceGetquerySearch(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, queryForsearch);
                    }
                    else
                    {
                        byte[] bcomp = MyLib._compress._compressString(queryForsearch.ToString());
                        byte[] getByte = ws._invoiceGetquerySearchCompress(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, bcomp);
                        getString = MyLib._compress._deCompressString(getByte);
                    }
                    XmlTextReader readXml = new XmlTextReader(new StringReader(getString));
                    // Convert MDataSet into a standard ADO.NET DataSet
                    ds.ReadXml(readXml, XmlReadMode.InferSchema);
                    if (ds.Tables.Count == 0)
                    {
                        DataTable table = new DataTable();
                        ds.Tables.Add(table);
                    }
                    loopWebservice = false;
                    ws.Dispose();
                    Debug.WriteLine(" --> Query Success.");
                }
                catch (TimeoutException)
                {
                    _tryConnect();
                }
                catch (WebException)
                {
                    _tryConnect();
                }
            }
            return (ds);
        }
    }
}
