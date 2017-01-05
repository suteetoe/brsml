using System;
using System.Data;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Threading;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MyLib
{
    public class _myFrameWork
    {
        public int _tryCount = 0;
        private string _webServiceServer = "";
        private string _databaseConfig = "";
        private ArrayList _webServiceServerList;
        public _myGlobal._databaseType _databaseSelectType;
        private ArrayList _tableForAutoUnlock;
        private Boolean _compressWebservice;
        public String _guid;
        private String _databaseName;
        private String _userCode;
        private String _mainDatabase;
        private String _computerName;

        public _myFrameWork()
        {
            this._initGlobal();
        }

        public _myFrameWork(string webServiceServerName, string databaseConfig, _myGlobal._databaseType databaseSelectType)
        {
            // สำหรับเชื่อมกับ web service กลาง
            this._webServiceServer = webServiceServerName;
            this._databaseConfig = databaseConfig;
            this._webServiceServerList = new ArrayList();
            _myWebserviceType __new = new _myWebserviceType();
            __new._webServiceName = this._webServiceServer;
            this._webServiceServerList.Add(__new);
            this._databaseSelectType = databaseSelectType;
            this._tableForAutoUnlock = new ArrayList();
            this._compressWebservice = true;
            this._guid = "SMLX";
            this._databaseName = _myGlobal._masterDatabaseName;
            this._mainDatabase = _myGlobal._masterDatabaseName;
            this._userCode = "SML";
            this._computerName = "SML";
        }

        public void _initGlobal()
        {
            this._webServiceServer = MyLib._myGlobal._webServiceServer;
            if (this._webServiceServer == null || this._webServiceServer.Length == 0)
            {
                this._webServiceServer = _findWebserviceServer(MyLib._myGlobal._webServiceServerList, "");
            }
            this._databaseConfig = MyLib._myGlobal._databaseConfig;
            this._webServiceServerList = _myGlobal._webServiceServerList;
            // this._databaseSelectType = _myGlobal._databaseSelectType;
            this._tableForAutoUnlock = _myGlobal._tableForAutoUnlock;
            this._compressWebservice = _myGlobal._compressWebservice;
            this._databaseName = _myGlobal._databaseName;
            this._userCode = _myGlobal._userCode;
            this._mainDatabase = _myGlobal._mainDatabase;
            this._computerName = _myGlobal._computerName;
            this._guid = _myGlobal._guid;
            this._findDatabaseType();
        }

        public string _getPortURL()
        {
            Uri __uri = new Uri("http://" + this._webServiceServer);
            return __uri.Port.ToString();
        }

        public void _findDatabaseType()
        {
            // ค้นหา Provider ว่า เป็น database ประเภทไหน
            Boolean __found = false;
            for (int __loop = 0; __loop < MyLib._myGlobal._providerList.Count; __loop++)
            {
                if (MyLib._myGlobal._providerList[__loop]._name.Equals(this._databaseConfig))
                {
                    __found = true;
                    this._databaseSelectType = MyLib._myGlobal._providerList[__loop]._databaseType;
                    break;
                }
            }
            if (__found == false)
            {
                this._databaseSelectType = this._setDataBaseCode();
                MyLib._providerListClass __new = new _providerListClass();
                __new._name = this._databaseConfig;
                __new._databaseType = this._databaseSelectType;
                MyLib._myGlobal._providerList.Add(__new);
            }
        }

        public void _tryConnect()
        {
            this._tryCount++;
            try
            {
                MyLib._myGlobal._serviceConnected = false;
                Thread.Sleep(2000);
                Application.DoEvents();
            }
            catch
            {
            }
            Console.WriteLine("_tryConnect : " + this._tryCount.ToString());
            _disableWebserviceServer(this._webServiceServer);
            bool __result = false;
            for (int __loop = 0; __loop < this._webServiceServerList.Count && __result == false; __loop++)
            {
                _myWebserviceType __getData = (_myWebserviceType)this._webServiceServerList[__loop];
                if (__getData._webServiceConnected)
                {
                    __result = true;
                }
            }
            if (__result == false)
            {
                _webserviceServerReConnect(false);
            }
            this._webServiceServer = _findWebserviceServer(this._webServiceServerList, this._webServiceServer);
        }

        public Boolean _testConnect()
        {
            Boolean __result = false;
            for (int __loop = 0; __loop < this._webServiceServerList.Count; __loop++)
            {
                _myWebserviceType __data = (_myWebserviceType)this._webServiceServerList[__loop];
                try
                {
                    if (__data._webServiceName.Length > 0)
                    {
                        if (MyLib._myGlobal._checkConnectionAvailable(__data._webServiceName))
                        {
                            _myWebservice __ws = new _myWebservice(__data._webServiceName);
                            __ws.Timeout = 10000;
                            string getCOnnectStr = __ws._checkConnect();
                            if (getCOnnectStr == "Connected")
                            {
                                return true;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return __result;
        }

        public string _packTableFromUnlock()
        {
            StringBuilder __result = new StringBuilder();
            for (int __loop = 0; __loop < this._tableForAutoUnlock.Count; __loop++)
            {
                if (__loop != 0)
                {
                    __result.Append(",");
                }
                __result.Append(this._tableForAutoUnlock[__loop]);
            }
            return (__result.ToString());
        }
        /// <summary>
        /// ตรวจสอบ query isnull,coalesce,ifnull,now(),getDete(),||,+
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        string __checkQueryforProvider(string query)
        {
            if (this._databaseSelectType == _myGlobal._databaseType.PostgreSql)
            {
                if (query.ToLower().IndexOf("isnull") != -1)
                {
                    query = query.Replace("isnull", "coalesce");
                }
                if (query.ToLower().IndexOf("ifnull") != -1)
                {
                    query = query.Replace("ifnull", "coalesce");
                }
                if (query.ToLower().IndexOf("smlupper") != -1)
                {
                    query = query.Replace("smlupper", "upper");
                }
                //if (query.ToLower().IndexOf("+") != -1)
                //{
                //    query = query.Replace("+", "||");
                //}

            }
            if (this._databaseSelectType == _myGlobal._databaseType.MicrosoftSQL2005 || this._databaseSelectType == _myGlobal._databaseType.MicrosoftSQL2000)
            {
                if (query.ToLower().IndexOf("coalesce") != -1)
                {
                    query = query.Replace("coalesce", "isnull");
                }
                if (query.ToLower().IndexOf("ifnull") != -1)
                {
                    query = query.Replace("ifnull", "isnull");
                }
                if (query.ToLower().IndexOf("||") != -1)
                {
                    query = query.Replace("||", "+");
                }
                if (query.ToLower().IndexOf("now()") != -1)
                {
                    query = query.Replace("now()", "getDate()");
                }
                if (query.ToLower().IndexOf("smlupper") != -1)
                {
                    query = query.Replace("smlupper", "");
                }
            }
            return query;
        }

        public DateTime _getFileLastUpdate(string fileName)
        {
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                string __getLastUpdate = __ws._getFileLastUpdate(fileName);
                CultureInfo __dateZone = new CultureInfo("en-US");
                DateTime __result = Convert.ToDateTime(__getLastUpdate, __dateZone);
                __ws.Dispose();
                return __result;
            }
            catch
            {
                return new DateTime();
            }
        }

        public void _saveXmlFile(string xmlFileName, string value)
        {
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                __ws._saveXmlFile(xmlFileName, value);
                __ws.Dispose();
            }
            catch (Exception __ex)
            {
                MessageBox.Show("Send File Error : " + xmlFileName + " : " + __ex.Message.ToString());
            }
        }

        public void _sendXmlFile(string xmlFileName)
        {
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                StreamReader __source = MyLib._getStream._getDataStream(xmlFileName);
                string __getXml = __source.ReadToEnd();
                __source.Close();
                __ws._saveXmlFile(xmlFileName, __getXml);
                __ws.Dispose();
            }
            catch (Exception __ex)
            {
                MessageBox.Show("Send File Error : " + xmlFileName + " : " + __ex.Message.ToString());
            }
            /*_myWebservice __ws = new _myWebservice();
            FileStream __file = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader __source = new StreamReader(__file, Encoding.GetEncoding("windows-874"));
            string __getXml = __source.ReadToEnd();
            __ws._saveXmlFile(xmlFileName, __getXml);
            __file.Close();
            __ws.Dispose();*/
        }

        public void _webserviceServerReConnect(bool fastMode)
        {
            int __countWebservice = 0;
            if (this._webServiceServerList.Count == 1)
            {
                _myWebserviceType __data = (_myWebserviceType)this._webServiceServerList[0];
                __data._webServiceConnected = true;
                __data._connectBytesPerSecond = 1000;
                this._webServiceServerList[0] = (_myWebserviceType)__data;
                this._webServiceServer = __data._webServiceName;
                return;
            }

            for (int __loop = 0; __loop < this._webServiceServerList.Count; __loop++)
            {
                _myWebserviceType __data = (_myWebserviceType)this._webServiceServerList[__loop];
                try
                {
                    if (__data._webServiceName.Length > 0)
                    {
                        _myWebservice __ws = new _myWebservice(__data._webServiceName);
                        this._compressWebservice = __ws._compress;
                        __ws.Timeout = 10000;
                        // string result = __ws._checkConnect();
                        __ws.Dispose();
                        __data._webServiceConnected = true;
                        __countWebservice++;
                        if (fastMode)
                        {
                            this._webServiceServer = __data._webServiceName;
                            break;
                        }
                    }
                }
                catch
                {
                    // Debugger.Break();
                    __data._webServiceConnected = false;
                    __data._connectBytesPerSecond = 0;
                }
                this._webServiceServerList[__loop] = (_myWebserviceType)__data;
            }
            if (__countWebservice > 1)
            {
                for (int __loop = 0; __loop < this._webServiceServerList.Count; __loop++)
                {
                    _myWebserviceType __data = (_myWebserviceType)this._webServiceServerList[__loop];
                    try
                    {
                        _myWebservice __ws = new _myWebservice(__data._webServiceName);
                        this._compressWebservice = __ws._compress;
                        if (__ws.Url.Length > 0)
                        {
                            __ws.Timeout = 10000;
                            DateTime __start = DateTime.Now;
                            string __result = __ws._checkConnectSpeed();
                            DateTime __stop = DateTime.Now;
                            __data._webServiceConnected = true;
                            __data._connectBytesPerSecond = _calcSpeedWebservice(__start, __stop, __result.Length);
                            __countWebservice++;
                        }
                        __ws.Dispose();
                    }
                    catch
                    {
                        // Debugger.Break();
                        __data._webServiceConnected = false;
                        __data._connectBytesPerSecond = 0;
                    }
                    this._webServiceServerList[__loop] = (_myWebserviceType)__data;
                }
            }
        }

        public void _disableWebserviceServer(string webserviceName)
        {
            for (int __loop = 0; __loop < this._webServiceServerList.Count; __loop++)
            {
                _myWebserviceType __getData = (_myWebserviceType)this._webServiceServerList[__loop];
                if (__getData._webServiceName.CompareTo(webserviceName) == 0)
                {
                    __getData._webServiceConnected = false;
                    this._webServiceServerList[__loop] = (_myWebserviceType)__getData;
                }
            }
        }

        public bool _statusWebserviceServer(string webserviceName)
        {
            for (int __loop = 0; __loop < this._webServiceServerList.Count; __loop++)
            {
                _myWebserviceType __getData = (_myWebserviceType)this._webServiceServerList[__loop];
                if (__getData._webServiceName.CompareTo(webserviceName) == 0)
                {
                    return (__getData._webServiceConnected);
                }
            }
            return (false);
        }

        public int _getWebserviceServerNumber(string webserviceName)
        {
            for (int __loop = 0; __loop < this._webServiceServerList.Count; __loop++)
            {
                _myWebserviceType __getData = (_myWebserviceType)this._webServiceServerList[__loop];
                if (__getData._webServiceName.CompareTo(webserviceName) == 0)
                {
                    return (__loop);
                }
            }
            return (0);
        }

        public string _findWebserviceServer(ArrayList webServiceList, string oldName)
        {
            int __foundCount = 0;
            if (oldName != null)
            {
                if (oldName.Length > 0)
                {
                    if (webServiceList.Count <= 1)
                    {
                        return (oldName);
                    }
                }
            }
            try
            {
                // ตรวจสอบว่า ปิดหมดหรือเปล่า ถ้าปิด ให้เปิดใหม่
                Boolean __webserviceHaveConnect = false;
                for (int __loop = 0; __loop < webServiceList.Count; __loop++)
                {
                    if (((_myWebserviceType)webServiceList[__loop])._webServiceConnected == true)
                    {
                        __webserviceHaveConnect = true;
                        break;
                    }
                }
                if (__webserviceHaveConnect == false)
                {
                    for (int __loop = 0; __loop < webServiceList.Count; __loop++)
                    {
                        if (((_myWebserviceType)webServiceList[__loop])._webServiceName.Length > 0)
                        {
                            ((_myWebserviceType)webServiceList[__loop])._webServiceConnected = true;
                            ((_myWebserviceType)webServiceList[__loop])._connectBytesPerSecond = 1000;
                        }
                    }
                }
                //
                bool __found = false;
                for (int __loop = 0; __loop < webServiceList.Count; __loop++)
                {
                    if (((_myWebserviceType)webServiceList[__loop])._webServiceConnected == true)
                    {
                        __found = true;
                        __foundCount++;
                    }
                }
                if (__found == false)
                {
                    return (oldName);
                }
                string __result = "";
                int __goodServer = -1;
                double __calcMax = -1;
                double __firstSpeed = -1;
                bool __isCompareAll = true;
                for (int __loop = 0; __loop < webServiceList.Count; __loop++)
                {
                    _myWebserviceType __getData = (_myWebserviceType)webServiceList[__loop];
                    if (__getData._webServiceConnected)
                    {
                        if (__getData._connectBytesPerSecond > __calcMax)
                        {
                            if (__calcMax == -1)
                            {
                                __firstSpeed = __getData._connectBytesPerSecond;
                            }
                            __calcMax = __getData._connectBytesPerSecond;
                            __goodServer = __loop;
                        }
                        if (__getData._connectBytesPerSecond != __firstSpeed)
                        {
                            __isCompareAll = false;
                        }
                    }
                }
                if (__isCompareAll == true)
                {
                    _myWebserviceType __getDataServer = (_myWebserviceType)webServiceList[__goodServer];
                    if (__getDataServer._webServiceName.CompareTo(oldName) == 0)
                    {
                        bool __findWebservice = false;
                        while (__findWebservice == false)
                        {
                            Random __getNumber = new Random();
                            int __randomNumber = __getNumber.Next(webServiceList.Count);
                            _myWebserviceType __getData = (_myWebserviceType)webServiceList[__randomNumber];
                            if (__getData._webServiceConnected)
                            {
                                __goodServer = __randomNumber;
                                break;
                            }
                        }
                    }
                }
                _myWebserviceType __getNewDataServer = (_myWebserviceType)webServiceList[__goodServer];
                __result = __getNewDataServer._webServiceName;
                return __result;
            }
            catch
            {
                return oldName;
            }
        }

        //somruk
        public _myGlobal._databaseType _setDataBaseCode()
        {
            string __result = "1";
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                __result = __ws._getDatabaseCode(this._databaseConfig);
                __ws.Dispose();
                switch (MyLib._myGlobal._intPhase(__result))
                {
                    case 1: return _myGlobal._databaseType.PostgreSql;
                    case 2: return _myGlobal._databaseType.MySql;
                    case 3: return _myGlobal._databaseType.MicrosoftSQL2000;
                    case 4: return _myGlobal._databaseType.MicrosoftSQL2005;
                    case 5: return _myGlobal._databaseType.Oracle;
                    case 6: return _myGlobal._databaseType.Firebird;
                }
            }
            catch
            {
            }
            return _myGlobal._databaseType.PostgreSql;
        }

        public string _changePassword(string configFileName, string databaseName, string userCode, string oldPassword, string newPassword)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            return __ws._changePassword(configFileName, databaseName, userCode, oldPassword, newPassword);
        }

        public string _getConnection(_myGlobal._databaseType databaseID, string configFileName, string databaseName, string mainDatabaseFileName, string databaseServer, string databaseUser, string databasePassword)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            int __databaseNumber = 1;
            switch (databaseID)
            {
                case _myGlobal._databaseType.PostgreSql: __databaseNumber = 1; break;
                case _myGlobal._databaseType.MySql: __databaseNumber = 2; break;
                case _myGlobal._databaseType.MicrosoftSQL2000: __databaseNumber = 3; break;
                case _myGlobal._databaseType.MicrosoftSQL2005: __databaseNumber = 4; break;
                case _myGlobal._databaseType.Oracle: __databaseNumber = 5; break;
                case _myGlobal._databaseType.Firebird: __databaseNumber = 6; break;
            }
            string __result = __ws._getConnection(__databaseNumber, configFileName, databaseName, mainDatabaseFileName, databaseServer, databaseUser, databasePassword);
            __ws.Dispose();
            return (__result);
        }

        /// <summary>
        /// ดึง Table ทั้งหมดจาก XML
        /// </summary>
        /// <param name="mode">0=เฉพาะชื่อ Table,1=ชื่อ Table,ชื่อไทย,ชื่ออังกฤษ</param>
        /// <param name="configFileName"></param>
        /// <param name="structFileName"></param>
        /// <returns></returns>
        public ArrayList _getAllTable(int mode, string configFileName, string structFileName)
        {
            ArrayList __result = new ArrayList();
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    string __getTableString = __ws._getAllTable(configFileName, structFileName);
                    System.Xml.XmlTextReader __xmlrt1 = new XmlTextReader(new StringReader(__getTableString));
                    while (__xmlrt1.Read())
                    {
                        //Trace.Write("Node Type", xmlrt1.NodeType.ToString());
                        if (__xmlrt1.NodeType.ToString().Equals("Element"))
                        {
                            switch (__xmlrt1.Name.ToLower())
                            {
                                case "tablename":
                                    __xmlrt1.Read();
                                    string __getName = __xmlrt1.Value;
                                    if (mode == 0)
                                    {
                                        string[] __getNameSplit = __getName.Split(',');
                                        string __getVersion = __getNameSplit[3].ToLower();
                                        __getName = __getNameSplit[0];
                                    }
                                    if (__getName.Length != 0)
                                    {
                                        __result.Add(__getName);
                                    }
                                    break;
                            } // end switch
                        } // end if       
                    } // end while
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
            }
            return (__result);
        }

        public ArrayList _getTableFromDatabase(string configFileName, string databaseName)
        {
            ArrayList __result = new ArrayList();
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    string getTableString = __ws._getTableFromDatabase(configFileName, databaseName);
                    System.Xml.XmlTextReader __xmlrt1 = new XmlTextReader(new StringReader(getTableString));
                    while (__xmlrt1.Read())
                    {
                        //Trace.Write("Node Type", xmlrt1.NodeType.ToString());
                        if (__xmlrt1.NodeType.ToString().Equals("Element"))
                        {
                            switch (__xmlrt1.Name)
                            {
                                case "tablename":
                                    __xmlrt1.Read();
                                    __result.Add(__xmlrt1.Value);
                                    break;
                            } // end switch
                        } // end if       
                    } // end while
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
            }
            return (__result);
        }

        public ArrayList _getFieldFromDatabase(string configFileName, string databaseName, string tableName)
        {
            ArrayList __result = new ArrayList();
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    string __getTableString = __ws._getFieldFromDatabase(configFileName, databaseName, tableName);
                    System.Xml.XmlTextReader __xmlrt1 = new XmlTextReader(new StringReader(__getTableString));
                    while (__xmlrt1.Read())
                    {
                        //Trace.Write("Node Type", xmlrt1.NodeType.ToString());
                        if (__xmlrt1.NodeType.ToString().Equals("Element"))
                        {
                            switch (__xmlrt1.Name)
                            {
                                case "fieldname":
                                    __xmlrt1.Read();
                                    __result.Add(__xmlrt1.Value);
                                    break;
                            } // end switch
                        } // end if       
                    } // end while
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
            }
            return (__result);
        }

        public string _insertLanguageTable(string configFileName, string databaseName, string xmlFileName)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            return __ws._insertLanguageToTable(configFileName, databaseName, xmlFileName);
        }

        public String _verifyDatabase(string configFileName, string databaseGroup, string databaseName, string tableName, string structFileName)
        {
            this._tryCount = 0;
            while (this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    string __result = __ws._verifyDatabase(configFileName, databaseGroup, databaseName, tableName, structFileName);
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                    return (__result);
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
            return "";
        }

        public void _verifyDatabaseScript(string scriptName, string configFileName, string databaseGroup, string databaseName, string structFileName)
        {
            this._tryCount = 0;
            while (this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    __ws._verifyDatabaseScript(scriptName, configFileName, databaseGroup, databaseName, structFileName);
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                    return;
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
        }

        public DataSet _getDatabaseList()
        {
            DataSet __ds = new DataSet();
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    string __getString = "";
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    if (this._compressWebservice == false)
                    {
                        __getString = __ws._getDatabaseList(this._guid, this._databaseConfig);
                    }
                    XmlTextReader __readXml = new XmlTextReader(new StringReader(__getString));
                    // Convert MDataSet into a standard ADO.NET DataSet
                    __ds.ReadXml(__readXml, XmlReadMode.InferSchema);
                    if (__ds.Tables.Count == 0)
                    {
                        DataTable __table = new DataTable();
                        __ds.Tables.Add(__table);
                    }
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
            }
            return (__ds);
        }

        public byte[] _queryStreamGet(string reportGuid, int blockSize, int blockNumber)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            return __ws._queryGetStream(this._guid, this._databaseConfig, reportGuid, blockSize, blockNumber);
        }

        byte[] _appendArrays(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length]; // just one array allocation
            Buffer.BlockCopy(a, 0, c, 0, a.Length);
            Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        public string _loadStream(string __reportGuid, long __fileSize, string message)
        {
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
            __fsSource.Close();
            __gzDecompressed.Close();
            //
            System.Text.Encoding __enc = System.Text.Encoding.UTF8;
            return __enc.GetString(__buffer);
        }

        public event QueryStreamEventHandler __queryStreamEvent;

        public DataSet _queryStream(string databaseName, string query)
        {
            return this._queryStream(databaseName, query, "Query");
        }

        public DataSet _queryStream(string databaseName, string query, string message)
        {
            DateTime __start = DateTime.Now;
            string __query = __checkQueryforProvider(query);
            DataSet __resultDataSet = null;
            long __fileSize = 0;

            if (this._webServiceServer == null)
            {
                return null;
            }
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
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
                    __fileSize = MyLib._myGlobal._intPhase(__ws._queryStream(this._guid, this._databaseConfig, __reportGuid, databaseName, __query));
                    //
                    /*int __blockSize = 102400;
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
                    __resultDataSet = _convertStringToDataSet(__enc.GetString(__buffer));*/

                    __resultDataSet = MyLib._myGlobal._convertStringToDataSet(_loadStream(__reportGuid, __fileSize, message));
                    // Close the streams
                    /*__fsSource.Close();
                    __gzDecompressed.Close();*/
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
            _queryTrack(__start, databaseName, query);
            return (__resultDataSet);
        }

        public DataSet _queryShort(string query)
        {
            return _query(this._databaseName, query);
        }

        public String _queryColumnName(string databaseName, string query)
        {
            // replace special charector provider
            string __query = __checkQueryforProvider(query);

            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    __result = __ws._queryColumnName(this._guid, this._databaseConfig, databaseName, __query);
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
                    return "";
                }
            }
            return (__result);
        }

        public byte[] _queryByte(string databaseName, string query)
        {
            byte[] __databyte = new byte[1024];
            try
            {
                //string __getString = "";
                //_myWebservice __ws = new _myWebservice(MyLib._myGlobal._masterWebservice);

                // toe แก้ไข ให้สามารถ เปลี่่ยนไปใช้่ webservice ตัวอื่นได้
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                // __databyte = __ws._querybyte(databaseName, query);
                __databyte = __ws._query_Image(this._guid, this._databaseConfig, databaseName, query);
                __ws.Dispose();
            }
            catch (Exception)
            {
            }

            return __databyte;
        }

        public string _queryInsertOrUpdate2(string databaseName, string query)
        {
            _myWebservice __ws = new _myWebservice(MyLib._myGlobal._masterWebservice);
            this._compressWebservice = __ws._compress;
            return __ws._queryInsertOrUpdateNoGuid2(Convert.ToBase64String(Encoding.UTF8.GetBytes(this._databaseConfig)), Convert.ToBase64String(Encoding.UTF8.GetBytes(databaseName)), Convert.ToBase64String(Encoding.UTF8.GetBytes(query)));
        }

        public DataSet _query2(string databaseName, string query)
        {
            DataSet __ds = new DataSet();
            try
            {
                _myWebservice __ws = new _myWebservice(MyLib._myGlobal._masterWebservice);
                this._compressWebservice = __ws._compress;
                string __databaseName = Convert.ToBase64String(Encoding.UTF8.GetBytes(databaseName));
                string __query = Convert.ToBase64String(Encoding.UTF8.GetBytes(query));
                string __result = __ws._query2(Convert.ToBase64String(Encoding.UTF8.GetBytes(this._databaseConfig)), __databaseName, __query);
                string __getString = Encoding.UTF8.GetString(Convert.FromBase64String(__result));
                __ds = MyLib._myGlobal._convertStringToDataSet(__getString);
                __ws.Dispose();
            }
            catch (XmlException)
            {
                DataTable __table = new DataTable();
                __ds.Tables.Add(__table);
                return __ds;
            }
            return (__ds);
        }
        /// <summary>
        /// Query Database
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        public DataSet _query(string databaseName, string query)
        {
            DateTime __start = DateTime.Now;
            string __query = __checkQueryforProvider(query);
            DataSet __ds = new DataSet();
            if (this._webServiceServer == null)
            {
                DataTable __table = new DataTable();
                __ds.Tables.Add(__table);
                return __ds;
            }
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    string __getString = "";
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    if (this._compressWebservice == false)
                    {
                        byte[] __bcomp = _compress._compressString(__query.ToString());
                        byte[] __getByte = __ws._queryCompress(this._guid, this._databaseConfig, databaseName, __bcomp);
                        __getString = _compress._deCompressString(__getByte);
                        //__getString = __ws._query(this._guid, this._databaseConfig, databaseName, __query);
                    }
                    else
                    {
                        byte[] __bcomp = _compress._compressString(__query.ToString());
                        byte[] __getByte = __ws._queryCompress(this._guid, this._databaseConfig, databaseName, __bcomp);
                        __getString = _compress._deCompressString(__getByte);
                    }
                    if (__getString.IndexOf("?xml") != -1)
                    {
                        __ds = MyLib._myGlobal._convertStringToDataSet(__getString);
                    }
                    /*XmlTextReader __readXml = new XmlTextReader(new StringReader(__getString));
                    // Convert MDataSet into a standard ADO.NET DataSet
                    __ds.ReadXml(__readXml, XmlReadMode.InferSchema);
                    if (__ds.Tables.Count == 0)
                    {
                        DataTable __table = new DataTable();
                        __ds.Tables.Add(__table);
                    }*/
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
            _queryTrack(__start, databaseName, query);
            return (__ds);
        }

        //toe
        public string _queryByteData(string databaseName, string __query, object[] __byteValue)
        {
            string __result = "";

            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                __result = __ws._queryByteData(this._guid, this._databaseConfig, databaseName, __query, __byteValue);
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

            return __result;
        }

        public string _saveFormDesign(string databaseName, string InsertOrUpdate, string formCode, string formGuidCode, string formName, byte[] __formText, byte[] __formBg)
        {
            string __result = "";
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                __result = __ws._SaveFormDesign(this._guid, this._databaseConfig, databaseName, InsertOrUpdate, formCode, formGuidCode, formName, __formText, __formBg);
                /*if (MyLib._myGlobal._compressWebservice == false)
                {
                    __result = __ws._SaveFormDesign(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, InsertOrUpdate, formCode, formGuidCode, formName, __formText, __formBg);
                }
                else
                {
                    __result = __ws._SaveFormDesign(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, InsertOrUpdate, formCode, formGuidCode, formName, __formText, __formBg);
                }*/
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
            return __result;
        }

        public MyLib.SMLJAVAWS.formDesignType _loadForm(string databaseName, string _query)
        {
            MyLib.SMLJAVAWS.formDesignType __result = new SMLJAVAWS.formDesignType();
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                __result = __ws._loadForm(this._guid, this._databaseConfig, databaseName, _query);
                /*if (MyLib._myGlobal._compressWebservice == false)
                {
                    __result = __ws._loadForm(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, _query);
                }
                else
                {
                    __result = __ws._loadForm(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, _query);
                }*/
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

            return __result;
        }

        //somruk
        public byte[] _ImageByte(string databaseName, string query)
        {
            // query = __checkQueryforProvider(query);
            byte[] __databyte = new byte[1024];
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;

            if (this._compressWebservice == false)
            {
                __databyte = __ws._query_Image(this._guid, this._databaseConfig, databaseName, query);
            }
            else
            {
                __databyte = __ws._query_Image(this._guid, this._databaseConfig, databaseName, query);
            }
            __ws.Dispose();
            return __databyte;
        }

        // toe 
        public byte[] _qrCodeByte(string content)
        {
            byte[] __databyte = new byte[1024];
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;

            if (this._compressWebservice == false)
            {
                __databyte = __ws._getQrCodeByte(this._guid, this._databaseConfig, content);
            }
            else
            {
                __databyte = __ws._getQrCodeByte(this._guid, this._databaseConfig, content);
            }
            __ws.Dispose();
            return __databyte;
        }

        MyLib.SMLJAVAWS.imageType[] imageTypeResult;
        public MyLib.SMLJAVAWS.imageType[] _LoadImageList(string guid_codefomscreen, string databaseName, string query)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            if (this._compressWebservice == false)
            {
                imageTypeResult = __ws._LoadImageList(guid_codefomscreen, this._databaseConfig, databaseName, query);
            }
            else
            {
                imageTypeResult = __ws._LoadImageList(guid_codefomscreen, this._databaseConfig, databaseName, query);
            }
            __ws.Dispose();
            return (imageTypeResult);
        }

        public string _SaveImageList(string guid_codefomscreen, string databaseName, SMLJAVAWS.imageType[] dataType, string InsertorUpdateType, string[] feilds, string tableName, string swherefeilds, string swheredata)
        {
            string __result = "";
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;

            if (this._compressWebservice == false)
            {
                __result = __ws._SaveImageList(this._guid, this._databaseConfig, databaseName, dataType, InsertorUpdateType, feilds, tableName, swherefeilds, swheredata);
            }
            else
            {
                __result = __ws._SaveImageList(this._guid, this._databaseConfig, databaseName, dataType, InsertorUpdateType, feilds, tableName, swherefeilds, swheredata);
            }
            __ws.Dispose();
            return __result;
        }

        public string _DeleteImageList(string databaseName, string tableName, string swherefeilds, string[] swheredata)
        {
            string __result = "";
            bool __loopWebservice = true;
            while (__loopWebservice == true)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    if (this._compressWebservice == false)
                    {
                        __result = __ws._DeleteImageList(this._guid, this._databaseConfig, databaseName, tableName, swherefeilds, swheredata);
                        __loopWebservice = false;
                    }
                    else
                    {
                        //byte[] bcomp = _compress._compressString(query.ToString());
                        //   aaa = ws._query_ImageList(MyLib._myGlobal._guid, MyLib._myGlobal._databaseConfig, databaseName, query);
                        // getString = _compress._decompressString(getByte);
                    }
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
            }
            return __result;
        }

        private int _calcSpeedWebservice(DateTime begin, DateTime end, double dataSize)
        {
            int __calcBytePerMilliSecond = (int)_myUtil._dateDiff("second", begin, end) + 1;
            return (int)(dataSize / __calcBytePerMilliSecond);
        }

        public String _queryInsertOrUpdate(string databaseName, string query)
        {
            return _queryInsertOrUpdate(databaseName, query, false);
        }

        public String _queryInsertOrUpdate(string databaseName, string query, Boolean checkGuid)
        {
            DateTime __start = DateTime.Now;
            string __query = __checkQueryforProvider(query);
            string __guid = (checkGuid) ? this._guid : "SMLX";
            bool __loopWebservice = true;
            string __result = "";
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    if (this._compressWebservice == false)
                    {
                        __result = __ws._queryInsertOrUpdate(__guid, this._databaseConfig, databaseName, __query);
                    }
                    else
                    {
                        try
                        {
                            byte[] __bcomp = _compress._compressString(__query.ToString());
                            byte[] __getByte = __ws._queryInsertOrUpdateCompress(this._guid, this._databaseConfig, databaseName, __bcomp);
                            if (__getByte.Length == 1024)
                            {
                                Boolean __emtry = true;
                                for (int __count = 0; __count < __getByte.Length && __emtry == true; __count++)
                                {
                                    if (__getByte[__count] != 0)
                                    {
                                        __emtry = false;
                                    }
                                }
                                if (__emtry)
                                {
                                    __result = "";
                                }
                                else
                                {
                                    __result = _compress._deCompressString(__getByte);
                                }
                            }
                            else
                            {
                                __result = _compress._deCompressString(__getByte);
                            }
                        }
                        catch
                        {
                            __result = __ws._queryInsertOrUpdate(__guid, this._databaseConfig, databaseName, __query);
                        }
                    }
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
            }
            _queryTrack(__start, databaseName, query);
            return __result;
        }

        public void _queryTrack(DateTime start, string databaseName, string query)
        {
            TimeSpan __diff = DateTime.Now.Subtract(start);
            Console.WriteLine(__diff.Seconds.ToString() + ":" + __diff.Milliseconds.ToString() + ":" + databaseName + ":" + ((query.Length > 400) ? query.Substring(0, 300) : query));
        }

        public String _queryList(string databaseName, string query)
        {
            DateTime __start = DateTime.Now;
            string __query = __checkQueryforProvider(query);
            bool __loopWebservice = true;
            string __result = "";
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    if (this._compressWebservice == false)
                    {
                        __result = __ws._queryList(this._guid, this._databaseConfig, databaseName, __query);
                    }
                    else
                    {
                        try
                        {
                            byte[] __bcomp = _compress._compressString(__query.ToString());
                            byte[] __getByte = __ws._queryListCompress(this._guid, this._databaseConfig, databaseName, __bcomp);
                            __result = _compress._deCompressString(__getByte);
                        }
                        catch
                        {
                            __result = __ws._queryList(this._guid, this._databaseConfig, databaseName, __query);
                        }
                    }
                    __loopWebservice = false;
                    __ws.Dispose();
                    __ws = null;
                    MyLib._myGlobal._serviceConnected = true;
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("_queryList Time Out");
                    _tryConnect();
                }
                catch (WebException)
                {
                    Console.WriteLine("_queryList Time Out");
                    _tryConnect();
                }
            }
            _queryTrack(__start, databaseName, query);
            return (__result);
        }

        public ArrayList _queryListGetData(string databaseName, string query)
        {
            DateTime __start = DateTime.Now;
            string __query = __checkQueryforProvider(query);
            ArrayList __result = new ArrayList();
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    string __getString = "";
                    __result.Clear();
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    if (this._compressWebservice == false)
                    {
                        __getString = __ws._queryListGetData(this._guid, this._databaseConfig, databaseName, __query);
                    }
                    else
                    {
                        byte[] __bcomp = _compress._compressString(__query.ToString());
                        byte[] __getByte = __ws._queryListGetDataCompress(this._guid, this._databaseConfig, databaseName, __bcomp);
                        __getString = _compress._deCompressString(__getByte);
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
                            Console.WriteLine("_queryListGetData : " + __getString);
                        }
                        __loopWebservice = false;
                    }
                    __ws.Dispose();
                    MyLib._myGlobal._serviceConnected = true;
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("_queryListGetData Time Out");
                    _tryConnect();
                }
                catch (WebException)
                {
                    Console.WriteLine("_queryListGetData Time Out");
                    _tryConnect();
                }
            }
            _queryTrack(__start, databaseName, query);
            return (__result);
        }

        public bool _getGroup(string groupCode)
        {
            // ดูว่ามีกลุ่มนี้ใน SMLMAIN หรือไม่ โดยดูจากกลุ่มข้อมูลใน table sml_database_list
            bool __result = false;
            try
            {
                string __query = string.Concat("select data_group from sml_database_list where " + _myGlobal._addUpper("data_group") + "=\'", groupCode.ToUpper(), "\'");
                DataSet __xDataSet = _query(this._mainDatabase, __query);
                if (__xDataSet.Tables.Count > 0)
                {
                    DataRow[] getRows = __xDataSet.Tables[0].Select();
                    if (getRows.Length > 0)
                    {
                        __result = true;
                    }
                }
            }
            catch
            {
                // Debugger.Break();
                __result = false;
            }
            return (__result);
        }

        public DataSet _getUser(string userCode)
        {
            // ดูว่ามีกลุ่มนี้ใน SMLMAIN หรือไม่ โดยดูจากกลุ่มข้อมูลใน table sml_database_list
            string __query = string.Concat("select user_name,user_level from sml_user_list where " + _myGlobal._addUpper("user_code") + "=\'", userCode.ToUpper(), "\'");
            DataSet __xDataSet = _query(this._mainDatabase, __query);
            return (__xDataSet);
        }

        public bool _checkUserAndPassword(string userCode, string userPassword)
        {
            // ดูว่ามีกลุ่มนี้ใน SMLMAIN หรือไม่ โดยดูจากกลุ่มข้อมูลใน table sml_database_list
            bool __result = false;
            string __query = string.Concat("select user_password from sml_user_list where " + _myGlobal._addUpper("user_code") + "=\'", userCode.ToUpper(), "\'");
            try
            {
                this._guid = "SMLX";
                DataSet __xDataSet = _query(this._mainDatabase, __query);
                if (__xDataSet.Tables.Count > 0)
                {
                    DataRow[] __getRows = __xDataSet.Tables[0].Select();
                    if (__getRows.Length > 0)
                    {
                        string __getPassword = MyLib._myUtil._decrypt(__getRows[0].ItemArray.GetValue(0).ToString());
                        if (__getPassword.Equals(userPassword))
                        {
                            __result = true;
                        }
                    }
                }
                __xDataSet.Dispose();
            }
            catch
            {
                // Debugger.Break();
                __result = false;
            }
            return (__result);
        }

        public string _createDatabaseAndTable(string configFileName, string databaseGroup, string databaseName, string tableName, string structFileName)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            string __result = __ws._createDatabaseAndTable(configFileName, databaseGroup, databaseName, tableName, structFileName);
            __ws.Dispose();
            __ws = null;
            return (__result);
        }

        public bool _findDatabase(string configFileName, string databaseName)
        {
            bool __result = false;
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            string __getStr = __ws._connectDatabase(this._databaseConfig, databaseName);
            if (__getStr.Length == 0)
            {
                __result = true;
            }
            __ws.Dispose();
            __ws = null;
            return (__result);
        }

        public string _getServerIPAddr(string serverRemoteAddr)
        {
            string __result = "";
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                //this._compressWebservice = __ws._compress;
                __result = __ws._getServerIpAddr(serverRemoteAddr);
                __ws.Dispose();
                __ws = null;
            }
            catch
            {
            }

            return (__result);

        }

        public DataSet _getAllDatabase(string configFileName)
        {
            //somruk
            DataSet __result = new DataSet();
            if (this._databaseSelectType == _myGlobal._databaseType.MicrosoftSQL2000 || this._databaseSelectType == _myGlobal._databaseType.MicrosoftSQL2005)
            {
                __result = _query("master", "select name from sysdatabases");
            }
            if (this._databaseSelectType == _myGlobal._databaseType.PostgreSql)
            {
                __result = _query("", "select data_name from sml_database_list");
            }
            return (__result);
        }

        private int _resourceCount(string dataGroup)
        {
            try
            {
                string __query = string.Concat("select count(*) as xcount from sml_resource where ", _myGlobal._addUpper("data_group") + "=\'", dataGroup.ToUpper(), "\'");
                DataSet __result = _query(this._mainDatabase, __query);
                if (__result.Tables[0].Rows.Count == 0)
                {
                    return 0;
                }
                return MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
        }

        public DataTable _resourceLoadAll(string dataGroup)
        {
            // ตรวจดูว่ามี Resource หรือยัง ถ้ายังให้เพิ่มอัตโนัมัต
            if (this._resourceCount(dataGroup) < 500)
            {
                try
                {
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    __ws._resourceInsertAll(this._databaseConfig, MyLib._myGlobal._databaseStructFileName, dataGroup);
                    __ws.Dispose();
                    __ws = null;
                }
                catch
                {
                }
            }
            //
            string __query = string.Concat("select code,name_1,name_2,name_3,name_4,name_5,name_6,length from sml_resource where ", _myGlobal._addUpper("data_group"), "=\'", dataGroup.ToUpper(), "\' order by code");
            DataSet __xDataSet = _query(this._mainDatabase, __query);
            if (__xDataSet.Tables.Count > 0)
            {
                DataTable __ret = __xDataSet.Tables[0];
                if (__ret.Columns.Count == 0)
                {
                    __xDataSet = new DataSet();
                    DataTable __newTable = new DataTable();
                    __newTable.Columns.Add("code");
                    __newTable.Columns.Add("name_1");
                    __newTable.Columns.Add("name_2");
                    __newTable.Columns.Add("name_3");
                    __newTable.Columns.Add("name_4");
                    __newTable.Columns.Add("name_5");
                    __newTable.Columns.Add("name_6");
                    __newTable.Columns.Add("length");
                    __xDataSet.Tables.Add(__newTable);
                    __ret = __xDataSet.Tables[0];
                }
                try
                {
                    __ret.PrimaryKey = new DataColumn[] { __ret.Columns[0] };
                }
                catch
                {
                    //MessageBox.Show("Duplicate Key Resource");
                }
                return (__ret);
            }
            else
            {
                MessageBox.Show("Resource load fail.");
                return null;
            }
        }

        public string _systemStartup(string configFileName, string databaseName, string screenGroup, string viewTableName, string viewDetailTableName, string dataViewFileName, string dataViewTemplateFileName)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            string __result = __ws._systemStartup(this._guid, configFileName, databaseName, screenGroup, viewTableName, viewDetailTableName, dataViewFileName, dataViewTemplateFileName);
            __ws.Dispose();
            return (__result);
        }

        /// <summary>
        /// เพิ่ม Resouce ลง SMLERPMAIN
        /// </summary>
        /// <param name="dataGroup"></param>
        /// <param name="code"></param>
        /// <param name="defaultName"></param>
        /// <param name="structFileName"></param>
        /// <returns></returns>
        public MyLib.SMLJAVAWS.resourceType _resourceInsert(String dataGroup, String code, String defaultName, String structFileName)
        {
            if (defaultName.Length == 0)
            {
                MyLib.SMLJAVAWS.resourceType __result = new MyLib.SMLJAVAWS.resourceType();
                __result._code = code;
                __result._name_1 = code;
                __result._name_2 = code;
                __result._name_3 = code;
                __result._name_4 = code;
                __result._name_5 = code;
                __result._name_6 = code;
                __result._length = 100;
                __result._status = 0;
                return (__result);
            }
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            MyLib.SMLJAVAWS.resourceType __result2 = (MyLib.SMLJAVAWS.resourceType)__ws._resourceInsert(this._mainDatabase, this._databaseConfig, dataGroup, code, defaultName, structFileName);
            __ws.Dispose();
            return (__result2);
        }

        public string _loadXmlFile(string fileName)
        {
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                string __getXml = __ws._loadXmlFile(fileName);
                return __getXml;
            }
            catch
            {
            }
            return "";
        }

        /// <summary>
        /// ดึงโครงสร้างฐานข้อมูล
        /// </summary>
        /// <param name="dataStructFileName"></param>
        /// <returns></returns>
        public DataSet _dataStruct(string dataStructFileName)
        {
            DataSet __ds = new DataSet();
            try
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                string __getXml = __ws._readXmlFile(this._databaseConfig, dataStructFileName);
                XmlTextReader __readXml = new XmlTextReader(new StringReader(__getXml));
                __ds.ReadXml(__readXml, XmlReadMode.Auto);
                __ws.Dispose();
            }
            catch
            {
                // Debugger.Break();
                __ds = null;
            }
            return (__ds);
        }

        public string _getSchemaTable(string databaseName, string tableName)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            string __result = __ws._getSchemaTable(this._databaseConfig, databaseName, tableName);
            __ws.Dispose();
            return (__result);
        }

        public string _getViewTemplate(string xmlFileName, string screenCode)
        {
            string __result = "";
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            string __getXml = __ws._readXmlFile(this._databaseConfig, MyLib._myGlobal._dataViewTemplateXmlFileName);
            XmlDocument __xDoc = new XmlDocument();
            __xDoc.LoadXml(__getXml);
            __xDoc.DocumentElement.Normalize();
            XmlElement __xRoot = __xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("screen");
            for (int __table = 0; __table < __xReader.Count; __table++)
            {
                XmlNode __xFirstNode = __xReader.Item(__table);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;
                    string __getScreenCode = __xTable.GetAttribute("screen_code");
                    if (__getScreenCode.ToLower().CompareTo(screenCode.ToLower()) == 0)
                    {
                        __result = __xFirstNode.OuterXml.ToString();
                        break;
                    }
                }
            }
            __ws.Dispose();
            __ws = null;
            __xDoc = null;
            return (__result);
        }

        public _queryReturn _queryLimit(string databaseName, string queryForCount, string query, int startRecord, int maxRecords, int countTotalRecord)
        {
            DateTime __start = DateTime.Now;
            string __query = __checkQueryforProvider(query);
            queryForCount = __checkQueryforProvider(queryForCount);
            XmlTextReader __readXml = null;
            _queryReturn __dsRet = new _queryReturn();
            string __result = "";
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                _myWebservice __ws = new _myWebservice(this._webServiceServer);
                this._compressWebservice = __ws._compress;
                try
                {
                    if (this._compressWebservice == false)
                    {
                        __result = __ws._queryLimit(this._guid, this._databaseConfig, databaseName, queryForCount, __query, startRecord, maxRecords, countTotalRecord);
                        if (__result.Length > 0)
                        {
                            int __findComma = __result.IndexOf(',');
                            string __getLength = __result.Substring(0, __findComma);
                            string __getString = __result.Substring(__findComma + 1);
                            __dsRet.totalRecord = MyLib._myGlobal._intPhase(__getLength);
                            __readXml = new XmlTextReader(new StringReader(__getString));
                        }
                    }
                    else
                    {
                        byte[] __resultByte = __ws._queryLimitCompress(this._guid, this._databaseConfig, databaseName, queryForCount, __query, startRecord, maxRecords, countTotalRecord);
                        __result = _compress._deCompressString(__resultByte);
                        if (__result.Length > 0)
                        {
                            int __findComma = __result.IndexOf(',');
                            string __getLength = __result.Substring(0, __findComma);
                            string __getString = __result.Substring(__findComma + 1);
                            __dsRet.totalRecord = MyLib._myGlobal._intPhase(__getLength);
                            __readXml = new XmlTextReader(new StringReader(__getString));
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
                __ws.Dispose();
                __ws = null;
            }
            _queryTrack(__start, databaseName, query);
            return (__dsRet);
        }

        public String _loginProcess(String configFileName, String databaseName, String userCode, String userPassword, String computerName, String databaseCode)
        {
            String __newGuid = "";
            int __loop = 0;
            bool __loginSuccess = false;
            while (__loginSuccess == false && __loop < this._webServiceServerList.Count)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    __newGuid = __ws._loginProcess(configFileName, databaseName, userCode, MyLib._myUtil._encrypt(userPassword), computerName, databaseCode);
                    __loginSuccess = true;
                    __ws.Dispose();
                }
                catch
                {
                    _disableWebserviceServer(this._webServiceServer);
                    this._webServiceServer = _findWebserviceServer(this._webServiceServerList, this._webServiceServer);
                    __loop++;
                }
            }
            return (__newGuid);
        }

        public void _logoutProcess(String configFileName, String guid)
        {
            bool __loopWebservice = true;
            this._tryCount = 0;
            while (__loopWebservice == true && this._tryCount < 10)
            {
                try
                {
                    _myWebservice __ws = new _myWebservice(this._webServiceServer);
                    this._compressWebservice = __ws._compress;
                    __ws._logoutProcess(configFileName, guid, this._databaseName, this._userCode, this._computerName, this._databaseName, _packTableFromUnlock());
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
            }
        }

        /// <summary>
        /// password server (ใหญ่สุด)
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string _systemLogin(string password, string url)
        {
            _myWebservice __ws = new _myWebservice(url);
            this._compressWebservice = __ws._compress;
            return __ws._systemLogin(password);
        }

        public string _systemChangePassword(string oldPassword, string newPassword, string url)
        {
            _myWebservice __ws = new _myWebservice(url);
            this._compressWebservice = __ws._compress;
            return __ws._systemChangePassword(oldPassword, newPassword);
        }

        public ArrayList _providerLoad(String password)
        {
            ArrayList __resultProvider = new ArrayList();
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            string __resultStr = __ws._providerLoad(password).Replace("<br>", "@");
            String[] __result = __resultStr.Split('@');
            for (int __loop = 0; __loop < __result.Length; __loop++)
            {
                String[] __data = __result[__loop].Split(',');
                if (__data.Length > 1)
                {
                    _providerType __newData = new _providerType();
                    __newData._code = __data[0].ToString();
                    __newData._name = __data[1].ToString();
                    __resultProvider.Add(__newData);
                }
            }
            return __resultProvider;
        }

        public string _providerSave(String password, string source)
        {
            _myWebservice __ws = new _myWebservice(this._webServiceServer);
            this._compressWebservice = __ws._compress;
            return __ws._providerSave(password, source);
        }
    }

    public delegate void QueryStreamEventHandler(string lastMessage, int persentProcess);

    public class _providerType
    {
        public string _name;
        public string _code;
    }

    public class _myResourceType
    {
        public string _str = "";
        public int _length = 0;
    }

    public class _queryReturn
    {
        public int totalRecord;
        public DataSet detail;
    }
}
