using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.Data;
using System.Globalization;
using System.IO;

namespace AKZOPODownloadConfig
{
    public class _process
    {
        string _configFileName = @"C:\smlconfig\akzopodownloadconfig.xml";
        string _termXMLFileName = @"C:\smlconfig\akzoterm.xml";
        string _unitcodeXMLFileName = @"C:\smlconfig\akzounit.xml";
        string _logFilePath = @"c:\smllog\";

        string _center_address = "";
        string _partn = "";
        string _host = "";
        string _port = "";
        string _db_user = "";
        string _db_pass = "";
        string _db_name = "";
        string _ap_code = "";

        static Dictionary<string, int> _term = null;
        static Dictionary<string, string> _unitCode = null;

        public string _getLogFileName
        {
            get
            {
                return string.Format("{0}-{1}-{2}-logs.txt", DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US")), _partn, _db_name);
            }
        }

        public void _run()
        {
            // read config

            if (System.IO.File.Exists(_configFileName) == true)
            {
                try
                {
                    XmlDocument xDoc = new XmlDocument();
                    try
                    {
                        xDoc.Load(_configFileName);
                    }
                    catch
                    {
                    }

                    xDoc.Normalize();
                    XmlElement __xRoot = xDoc.DocumentElement;

                    _center_address = __xRoot.GetElementsByTagName("center_address").Item(0).InnerText;
                    _partn = __xRoot.GetElementsByTagName("partn").Item(0).InnerText;
                    _host = __xRoot.GetElementsByTagName("host").Item(0).InnerText;
                    _port = __xRoot.GetElementsByTagName("port").Item(0).InnerText;
                    _db_user = __xRoot.GetElementsByTagName("db_user").Item(0).InnerText;
                    _db_pass = __xRoot.GetElementsByTagName("db_pass").Item(0).InnerText;
                    _db_name = __xRoot.GetElementsByTagName("db_name").Item(0).InnerText;
                    _ap_code = __xRoot.GetElementsByTagName("ap_code").Item(0).InnerText;
                }
                catch
                {

                }

                // load term
                try
                {
                    XmlDocument __termXML = new XmlDocument();
                    __termXML.Load(_termXMLFileName);
                    __termXML.Normalize();

                    XmlElement __root = __termXML.DocumentElement;
                    XmlNodeList __list = __root.GetElementsByTagName("ZTERM");

                    if (__list.Count > 0)
                    {
                        _term = new Dictionary<string, int>();
                        for (int __i = 0; __i < __list.Count; __i++)
                        {
                            _term.Add(((XmlNode)__list[__i]).Attributes["key"].Value, MyLib._myGlobal._intPhase(((XmlNode)__list[__i]).Attributes["value"].Value));
                        }
                    }
                }
                catch
                {
                }

                // load unitcode
                try
                {
                    XmlDocument __unitXML = new XmlDocument();
                    __unitXML.Load(_unitcodeXMLFileName);
                    __unitXML.Normalize();

                    XmlElement __root = __unitXML.DocumentElement;
                    XmlNodeList __list = __root.GetElementsByTagName("PEINH");

                    if (__list.Count > 0)
                    {
                        _unitCode = new Dictionary<string, string>();
                        for (int __i = 0; __i < __list.Count; __i++)
                        {
                            _unitCode.Add(((XmlNode)__list[__i]).Attributes["key"].Value, ((XmlNode)__list[__i]).Attributes["value"].Value);
                        }
                    }
                }
                catch
                {
                }

                if (_center_address.Length > 0)
                {
                    try
                    {
                        // get po xml list
                        string __fileListUrl = string.Format("http://{0}/SMLJavaWebService/getXMLList.jsp?q=po", _center_address);

                        XmlDocument __fileListXML = new XmlDocument();
                        __fileListXML.Load(__fileListUrl);
                        __fileListXML.Normalize();

                        XmlElement __fileListRootElement = __fileListXML.DocumentElement;
                        XmlNodeList __fileList = __fileListRootElement.GetElementsByTagName("file");
                        for (int __fileIndex = 0; __fileIndex < __fileList.Count; __fileIndex++)
                        {
                            // start readxml from list file

                            try
                            {
                                string __poFileName = string.Format("http://{0}/SMLJavaWebService/po/{1}", _center_address, __fileList[__fileIndex].InnerText);
                                XmlDocument __poXML = new XmlDocument();
                                __poXML.Load(__poFileName);

                                __poXML.Normalize();
                                // check partn ID

                                //XmlNodeList __partnNodeList = __poDoc.GetElementsByTagName(_nodePartnName);
                                //if (__partnNodeList.Count > 0)
                                //{                            

                                //}

                                _poDocument __po = new _poDocument();
                                __po._writeLogStr += (mess) =>
                                {
                                    _writeErrorLog(string.Format("{0}-{1}", DateTime.Now.ToString(), "Failed Load File " + __fileList[__fileIndex].InnerText + "(" + __po._docNo + ") : " + mess));
                                };
                                __po._loadFromXML(__poXML);

                                // รหัสตรงกันเอาเข้าโลด
                                if (__po._partner == _partn || __po._partner.Substring(__po._partner.Length - 6, 6) == _partn)
                                {
                                    int __trans_flag = 6;
                                    Boolean __pass = true;

                                    // check dup
                                    string __checkDupQuerySstr = "select count(doc_no) as xcount from ic_trans where doc_no = \'" + __po._docNo + "\' and trans_flag = " + __trans_flag;
                                    DataSet __ds = _queryGetData(__checkDupQuerySstr);

                                    if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                                    {
                                        if (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][0].ToString()) > 0)
                                        {
                                            _writeErrorLog(string.Format("{0}-{1}", DateTime.Now.ToString(), "Duplicate DocNo : " + __po._docNo));
                                            __pass = false;
                                        }
                                    }

                                    if (__pass == true)
                                    {
                                        // เอาเข้าโลด
                                        string __fieldList = "trans_type,inquiry_type,vat_type, " + MyLib._myGlobal._fieldAndComma(
                                            "trans_flag",
                                            "doc_no",
                                            "doc_date",
                                            "cust_code",
                                            "credit_day",
                                            "vat_rate",
                                            "total_value",
                                            "total_vat_value",
                                            "total_after_vat",
                                            "total_amount",
                                            "credit_date",
                                            "total_before_vat"
                                            );
                                        string __valueList = "1,0,0," + MyLib._myGlobal._fieldAndComma(
                                            "\'" + __trans_flag + "\'",
                                            "\'" + __po._docNo + "\'",
                                            "\'" + __po._docDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) + "\'",
                                            "\'" + _ap_code + "\'",
                                            __po._creditDay.ToString(),
                                            __po._vatRate.ToString(),
                                            __po._totalAmount_beforevat.ToString(),
                                            __po._totalVatValue.ToString(),
                                            __po._totalNetValue.ToString(),
                                            __po._totalNetValue.ToString(),
                                            "\'" + __po._creditDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) + "\'",
                                            __po._totalAmount_beforevat.ToString()
                                            );

                                        string __insertStr = "insert into ic_trans(" + __fieldList + ") values (" + __valueList + ")";

                                        string __result = _insertCmd(__insertStr);
                                        if (__result.Length > 0)
                                        {
                                            _writeErrorLog(string.Format("{0}-{1}", DateTime.Now.ToString(), __result));
                                        }

                                        for (int __i = 0; __i < __po._detail.Count; __i++)
                                        {
                                            string __fieldDetaiList = "trans_type,inquiry_type,vat_type," + MyLib._myGlobal._fieldAndComma(
                                                "trans_flag",
                                                "doc_no",
                                                "doc_date",
                                                "cust_code",
                                                "item_code",
                                                "item_name",
                                                "unit_code",
                                                "qty",
                                                "price",
                                                "sum_amount",
                                                "sum_amount_exclude_vat",
                                                "price_exclude_vat",
                                                "line_number"
                                                );
                                            string __valueDetailList = "1,0,0," + MyLib._myGlobal._fieldAndComma(
                                                "\'" + __trans_flag + "\'",
                                                "\'" + __po._docNo + "\'",
                                                "\'" + __po._docDate.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) + "\'",
                                                "\'" + _ap_code + "\'",
                                                "\'" + __po._detail[__i]._itemCode + "\'",
                                                "\'" + __po._detail[__i]._itemName + "\'",
                                                "\'" + __po._detail[__i]._unitCode + "\'",
                                                "\'" + __po._detail[__i]._qty.ToString() + "\'",
                                                "\'" + __po._detail[__i]._price.ToString() + "\'",
                                                "\'" + __po._detail[__i]._amount.ToString() + "\'",
                                                "\'" + __po._detail[__i]._amount.ToString() + "\'",
                                                "\'" + __po._detail[__i]._price.ToString() + "\'",
                                                __i.ToString()
                                                );

                                            string __insertDetailStr = "insert into ic_trans_detail(" + __fieldDetaiList + ") values (" + __valueDetailList + ")";

                                            __result = _insertCmd(__insertDetailStr);
                                            if (__result.Length > 0)
                                            {
                                                _writeErrorLog(string.Format("{0}-{1}", DateTime.Now.ToString(), __result));
                                            }

                                            // เสร็จแล้วตามไป update หน่วยนับ ตัวตั้งตัวหาร
                                            StringBuilder __queryUpdateUnit = new StringBuilder();
                                            __queryUpdateUnit.Append("update ic_trans_detail set ");
                                            __queryUpdateUnit.Append(" stand_value = (select ic_unit_use.stand_value from ic_unit_use where ic_unit_use.ic_code = ic_trans_detail.item_code and ic_unit_use.code = ic_trans_detail.unit_code ), ");
                                            __queryUpdateUnit.Append(" divide_value =  (select ic_unit_use.divide_value from ic_unit_use where ic_unit_use.ic_code = ic_trans_detail.item_code and ic_unit_use.code = ic_trans_detail.unit_code ) ");
                                            __queryUpdateUnit.Append(" where doc_no = \'" + __po._docNo + "\' and trans_flag = " + __trans_flag);
                                            __queryUpdateUnit.Append(" and stand_value <> (select ic_unit_use.stand_value from ic_unit_use where ic_unit_use.ic_code = ic_trans_detail.item_code and ic_unit_use.code = ic_trans_detail.unit_code ) ");
                                            __queryUpdateUnit.Append(" and divide_value <> (select ic_unit_use.divide_value from ic_unit_use where ic_unit_use.ic_code = ic_trans_detail.item_code and ic_unit_use.code = ic_trans_detail.unit_code ) ");
                                            __result = _insertCmd(__queryUpdateUnit.ToString());
                                            if (__result.Length > 0)
                                            {
                                                _writeErrorLog(string.Format("{0}-{1}", DateTime.Now.ToString(), __result));
                                            }


                                        }

                                        _writeErrorLog(string.Format("{0}-{1}", DateTime.Now.ToString(), "Insert PO : " + __po._docNo + " success"));


                                    }

                                    // move to po success
                                    string __movePoFileName = string.Format("http://{0}/SMLJavaWebService/moveXML.jsp?q={1}", _center_address, __fileList[__fileIndex].InnerText);

                                    WebClient client = new WebClient();
                                    String htmlCode = client.DownloadString(__movePoFileName).Replace("\n", "").Trim();
                                    if (htmlCode.Length > 0)
                                    {
                                        _writeErrorLog(string.Format("{0}-{1}", DateTime.Now.ToString(), htmlCode));
                                    }
                                }

                            }
                            catch
                            {
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                }
            }
        }

        string _insertCmd(string __query)
        {
            string __result = "";

            try
            {
                Npgsql.NpgsqlConnection __conn = new Npgsql.NpgsqlConnection(_getConnectionString());
                __conn.Open();

                Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand(__query, __conn);
                __command.ExecuteNonQuery();
                __command.Dispose();


                __conn.Close();
                __conn.Dispose();
            }
            catch (Exception ex)
            {
                __result = ex.Message.ToString();
            }

            return __result;
        }

        DataSet _queryGetData(string query)
        {
            DataSet ds = new DataSet();

            try
            {
                Npgsql.NpgsqlConnection __conn = new Npgsql.NpgsqlConnection(_getConnectionString());
                __conn.Open();

                Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand(query, __conn);
                Npgsql.NpgsqlDataAdapter __dtAdapter = new Npgsql.NpgsqlDataAdapter();

                __dtAdapter.SelectCommand = __command;
                __dtAdapter.Fill(ds);
                return ds;

            }
            catch
            {
            }

            return ds;
        }

        string _getConnectionString()
        {
            string __result = "";
            __result = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", this._host, this._port, this._db_user, this._db_pass, this._db_name);

            return __result;
        }

        void _writeErrorLog(string message)
        {
            try
            {
                string __fileName = _logFilePath + _getLogFileName;

                // check folders before
                if (System.IO.Directory.Exists(_logFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(_logFilePath);
                }

                FileMode __accessMode = FileMode.Append;
                // if file exists create new file
                if (System.IO.File.Exists(__fileName) == false)
                {
                    __accessMode = FileMode.Create;
                }

                // append log
                FileStream aFile = new FileStream(__fileName, __accessMode, FileAccess.Write);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine(message);
                sw.Close();
                aFile.Close();


            }
            catch
            {
            }
        }

        public class _poDocument
        {
            public string _docNo = "";
            public string _docDateStr = "";
            public DateTime _docDate
            {
                get
                {
                    try
                    {
                        DateTime __date;
                        if (DateTime.TryParseExact(this._docDateStr, "yyyyMMdd", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out __date))
                        {
                            return __date;
                        }
                    }
                    catch
                    {
                    }

                    return new DateTime();

                }

            }

            public string _partner = "";
            public decimal _vatRate = 0M;
            public int _creditDay = 0;

            public decimal _totalAmount_beforevat = 0M;
            public decimal _totalVatValue = 0M;
            /// <summary>
            /// มูลค่าสุทธิ
            /// </summary>
            public decimal _totalNetValue = 0M;

            public DateTime _creditDate
            {
                get
                {
                    return _docDate.AddDays(this._creditDay);
                }
            }

            public List<_poDocDetail> _detail = new List<_poDocDetail>();

            private string _tagPartnName = "PARTN";
            private string _tagVatRate = "MSATZ";
            private string _tagDocNo = "BELNR";
            private string _tagDocDate = "DATUM";
            private string _tagTermDay = "ZTERM";


            private string _tagItemList = "E1EDP01";
            private string _tagItemCode = "IDTNR";
            private string _tagItemName = "KTEXT";
            private string _tagItemQty = "MENGE";
            private string _tagUnitCode = "PEINH";
            private string _tagItemAmount = "NETWR";

            private string _tagFooterDoc = "E1EDS01";
            private string _tagSumID = "SUMID";
            private string _tagSumValue = "SUMME";

            public void _loadFromXML(XmlDocument _doc)
            {
                // get partn
                XmlNodeList __NodeList = _doc.GetElementsByTagName(_tagPartnName);
                if (__NodeList.Count > 0)
                {
                    this._partner = __NodeList[0].InnerText;
                }

                // get docNo
                __NodeList = _doc.GetElementsByTagName(_tagDocNo);
                if (__NodeList.Count > 0)
                {
                    this._docNo = __NodeList[0].InnerText;
                }

                // get docDate
                __NodeList = _doc.GetElementsByTagName(_tagDocDate);
                if (__NodeList.Count > 0)
                {
                    this._docDateStr = __NodeList[0].InnerText;
                }

                // get vat rate
                __NodeList = _doc.GetElementsByTagName(_tagVatRate);
                if (__NodeList.Count > 0)
                {
                    this._vatRate = MyLib._myGlobal._decimalPhase(__NodeList[0].InnerText);
                }

                // get term
                __NodeList = _doc.GetElementsByTagName(_tagTermDay);
                if (__NodeList.Count > 0)
                {
                    if (_term != null)
                    {
                        try
                        {
                            this._creditDay = _term[__NodeList[0].InnerText];
                        }
                        catch
                        {
                            Console.WriteLine("Not Found Term : " + __NodeList[0].InnerText);
                            if (_writeLogStr != null)
                                _writeLogStr("Not Found Term : " + __NodeList[0].InnerText);
                        }
                    }
                }

                // get item detail
                __NodeList = _doc.GetElementsByTagName(_tagItemList);
                if (__NodeList.Count > 0)
                {
                    for (int __count = 0; __count < __NodeList.Count; __count++)
                    {
                        XmlNode __node = __NodeList[__count];

                        _poDocDetail __detail = new _poDocDetail();

                        XmlDocument __detailDoc = new XmlDocument();
                        __detailDoc.LoadXml(__node.OuterXml);

                        __detail._itemCode = MyLib._myGlobal._decimalPhase(__detailDoc.GetElementsByTagName(_tagItemCode)[0].InnerText).ToString("00");
                        __detail._itemName = __detailDoc.GetElementsByTagName(_tagItemName)[0].InnerText;
                        __detail._qty = MyLib._myGlobal._decimalPhase(__detailDoc.GetElementsByTagName(_tagItemQty)[0].InnerText);
                        __detail._amount = MyLib._myGlobal._decimalPhase(__detailDoc.GetElementsByTagName(_tagItemAmount)[0].InnerText);
                        // item code
                        if (_unitCode != null)
                        {
                            try
                            {
                                __detail._unitCode = _unitCode[__detailDoc.GetElementsByTagName(_tagUnitCode)[0].InnerText];
                            }
                            catch
                            {
                                Console.WriteLine("Not Found Unit Code :" + __detailDoc.GetElementsByTagName(_tagUnitCode)[0].InnerText);
                                if (_writeLogStr != null)
                                    _writeLogStr("Not Found Unit Code :" + __detailDoc.GetElementsByTagName(_tagUnitCode)[0].InnerText);

                            }
                        }

                        this._detail.Add(__detail);
                    }
                }

                // get footer 
                __NodeList = _doc.GetElementsByTagName(_tagFooterDoc);
                if (__NodeList.Count > 0)
                {
                    for (int __count = 0; __count < __NodeList.Count; __count++)
                    {
                        XmlNode __node = __NodeList[__count];

                        XmlDocument __detailDoc = new XmlDocument();
                        __detailDoc.LoadXml(__node.OuterXml);

                        if (__detailDoc.GetElementsByTagName(_tagSumID).Count > 0)
                        {
                            switch (__detailDoc.GetElementsByTagName(_tagSumID)[0].InnerText)
                            {
                                case "001":
                                    this._totalNetValue = MyLib._myGlobal._decimalPhase(__detailDoc.GetElementsByTagName(_tagSumValue)[0].InnerText);
                                    break;
                                case "002":
                                    this._totalAmount_beforevat = MyLib._myGlobal._decimalPhase(__detailDoc.GetElementsByTagName(_tagSumValue)[0].InnerText);
                                    break;
                                case "005":
                                    this._totalVatValue = MyLib._myGlobal._decimalPhase(__detailDoc.GetElementsByTagName(_tagSumValue)[0].InnerText);
                                    break;
                            }
                        }


                    }
                }
            }

            public event _writeLog _writeLogStr;
        }

        public delegate void _writeLog(string message);

        public class _poDocDetail
        {
            public string _itemCode = "";
            public string _itemName = "";
            public decimal _qty = 0M;
            public decimal _amount = 0M;
            public string _unitCode = "";

            public decimal _price
            {
                get
                {
                    return (_amount / _qty);
                }
            }
        }

    }


}
