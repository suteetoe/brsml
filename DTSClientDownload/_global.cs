using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml;
using System.Data;
using System.Collections;

namespace DTSClientDownload
{
    public class _global
    {
        public static string _dts_server_configFileName = "DTSDownloadServerConnect.xml";

        public static string _champMessageCaption = "SCG Download";

        public static string _champServer = "";
        public static string _champDatabaseName = "";
        public static string _champUserName = "";
        public static string _champPassword = "";

        public static string _errSendMailMessage = "";

        public static SqlConnection _sqlConnection
        {
            get
            {
                return new SqlConnection("Server=" + _global._champServer + ";Database=" + _global._champDatabaseName + ";uid=" + _global._champUserName + ";Password=" + _global._champPassword + ";Connect Timeout=3000");
            }
        }

        public static SqlConnection _getConnection(string server, string databaseName, string usercode, string userpassword)
        {
            return new SqlConnection("Server=" + server + ";Database=" + databaseName + ";uid=" + usercode + ";Password=" + userpassword + ";Connect Timeout=3000");
        }

        public static bool _sqlTestConnection()
        {
            if (_global._champServer.Length == 0)
                return false;

            SqlConnection __conn = new SqlConnection("Server=" + _global._champServer + ";Database=" + _global._champDatabaseName + ";uid=" + _global._champUserName + ";Password=" + _global._champPassword + ";Connect Timeout=3000");
            //__conn.ConnectionTimeout = 300;
            try
            {
                __conn.Open();
                __conn.Close();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static bool _sqlTestConnection(string serverName, string databaseName, string databaseUser, string databasePassword)
        {
            SqlConnection __conn = new SqlConnection("Server=" + serverName + ";Database=" + databaseName + ";uid=" + databaseUser + ";Password=" + databasePassword + ";Connect Timeout=3000");
            try
            {
                __conn.Open();
                __conn.Close();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static string _smlConfigFile
        {
            get
            {
                string __smlConfigPath = @"C:\smlconfig";
                bool __isDirCreate = System.IO.Directory.Exists(__smlConfigPath);

                if (__isDirCreate == false)
                {
                    System.IO.Directory.CreateDirectory(__smlConfigPath); // create folders
                }
                return __smlConfigPath;
            }
        }

        public static string _whereLike(string fieldList, string query)
        {
            StringBuilder __result = new StringBuilder();
            String[] __fieldList = fieldList.Trim().Split(',');
            String[] __query = query.Trim().Split(' ');
            for (int __loop1 = 0; __loop1 < __query.Length; __loop1++)
            {
                if (__loop1 > 0)
                {
                    __result.Append(" and ");
                }
                __result.Append("(");
                for (int __loop2 = 0; __loop2 < __fieldList.Length; __loop2++)
                {
                    if (__loop2 > 0)
                    {
                        __result.Append(" or ");
                    }
                    __result.Append("upper(").Append(__fieldList[__loop2].ToString()).Append(")");
                    __result.Append(" like \'%");
                    __result.Append(__query[__loop1].ToString().ToUpper());
                    __result.Append("%\'");
                }
                __result.Append(")");
            }
            return __result.ToString();
        }

        public static Boolean _getServerConnect()
        {
            Boolean __result = false;

            try
            {
                string __xFileName = _global._smlConfigFile + "\\" + _global._dts_server_configFileName;

                XmlDocument xDoc = new XmlDocument();
                try
                {
                    xDoc.Load(__xFileName);
                }
                catch
                {
                    return __result;
                }

                xDoc.DocumentElement.Normalize();
                XmlElement __xRoot = xDoc.DocumentElement;


                for (int __loop = 1; __loop < 6; __loop++)
                {
                    string __getName = "";
                    switch (__loop)
                    {
                        case 1: __getName = "server"; break;
                        case 2: __getName = "user"; break;
                        case 3: __getName = "password"; break;
                        case 4: __getName = "database"; break;
                        case 5: __getName = "dts_server"; break;

                    }
                    XmlNodeList __xReader = __xRoot.GetElementsByTagName(__getName);

                    for (int __table = 0; __table < __xReader.Count; __table++)
                    {
                        XmlNode __xFirstNode = __xReader.Item(__table);
                        if (__xFirstNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement __xTable = (XmlElement)__xFirstNode;
                            switch (__loop)
                            {
                                case 1: _global._champServer = __xTable.InnerText; break;
                                case 2: _global._champUserName = __xTable.InnerText; break;
                                case 3: _global._champPassword = __xTable.InnerText; break;
                                case 4: _global._champDatabaseName = __xTable.InnerText; break;
                                case 5:
                                    {
                                        MyLib._myGlobal._webServiceServerList.Clear();

                                        MyLib._myWebserviceType data = new MyLib._myWebserviceType();
                                        data._webServiceName = __xTable.InnerText;
                                        data._webServiceConnected = false;
                                        MyLib._myGlobal._webServiceServerList.Add(data);

                                    }
                                    break;
                            }
                        }
                    } // for
                } // for
            }
            catch
            {
                return __result;
            }

            return true;
        }

        public static string _getValue(string __string)
        {
            return __string.Split('~')[0];
        }

        /// <summary>
        /// สร้าง โครงสร้าง ฝั่ง Client
        /// </summary>
        public static void _verifyClient()
        {
            _clientFrameWork __frameWork = new _clientFrameWork();

            // สร้างตารางเก็บ temp ของ client
            StringBuilder __script = new StringBuilder();


            // ลบfield ฝั่ง server

            // delete discountword
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseorder._table + " drop column discountword");
            string __err = __frameWork._excute(__script.ToString());

            // delete discountamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseorder._table + " drop column discountamount");
            __err = __frameWork._excute(__script.ToString());

            // delete afterdiscount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseorder._table + " drop column afterdiscount");
            __err = __frameWork._excute(__script.ToString());

            // delete beforetaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseorder._table + " drop column beforetaxamount");
            __err = __frameWork._excute(__script.ToString());

            // delete excepttaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseorder._table + " drop column excepttaxamount");
            __err = __frameWork._excute(__script.ToString());

            // delete zerotaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseorder._table + " drop column zerotaxamount");
            __err = __frameWork._excute(__script.ToString());

            // delete bcpurchaseordersub docdate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseordersub._table + " drop column docdate");
            __err = __frameWork._excute(__script.ToString());

            // delete bcpurchaseordersub apcode
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseordersub._table + " drop column apcode");
            __err = __frameWork._excute(__script.ToString());

            // delete bcpurchaseordersub agent_code
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseordersub._table + " drop column agent_code");
            __err = __frameWork._excute(__script.ToString());

            // delete bcpurchaseordersub itemname
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseordersub._table + " drop column itemname");
            __err = __frameWork._excute(__script.ToString());

            // delete bcpurchaseordersub discountword
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseordersub._table + " drop column discountword");
            __err = __frameWork._excute(__script.ToString());

            // delete bcpurchaseordersub discountamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseordersub._table + " drop column discountamount");
            __err = __frameWork._excute(__script.ToString());

            // delete bcpurchaseordersub taxrate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcpurchaseordersub._table + " drop column taxrate");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleorder afterdiscount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleorder._table + " drop column afterdiscount");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleorder beforetaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleorder._table + " drop column beforetaxamount");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleorder excepttaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleorder._table + " drop column excepttaxamount");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleorder zerotaxamount
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleorder._table + " drop column zerotaxamount");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleordersub docdate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleordersub._table + " drop column docdate");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleordersub arcode
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleordersub._table + " drop column arcode");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleordersub agent_code
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleordersub._table + " drop column agent_code");
            __err = __frameWork._excute(__script.ToString());

            // delete bcsaleordersub itemname
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleordersub._table + " drop column itemname");
            __err = __frameWork._excute(__script.ToString());


            // delete bcsaleordersub taxrate
            __script = new StringBuilder();
            __script.Append("alter table " + _g.DataClient.dts_bcsaleordersub._table + " drop column taxrate");
            __err = __frameWork._excute(__script.ToString());


            // toe sync delete server data
            __script = new StringBuilder();
            __script.Append("create table client_sync_data \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("table_name varchar(100), \r\n");
            __script.Append("sync_mode varchar(100), \r\n");
            __script.Append("guid UNIQUEIDENTIFIER, \r\n");
            __script.Append("constraint pk_client_sync_data primary key clustered(roworder) \r\n");
            __script.Append("); \r\n");
            __err = __frameWork._excute(__script.ToString());

            /* ย้ายไป verify จาก script
            string __dtsPrefixDBName = "dts_";
             * 
             * 
            
            // สินค้าของ DTS (dts_bcitem)
            __script.Append("create table " + __dtsPrefixDBName + "bcitem \r\n");
            __script.Append("( \r\n");
            __script.Append("code varchar(25), \r\n");
            __script.Append("barcode varchar(20), \r\n");
            __script.Append("name1 varchar(255), \r\n");
            __script.Append("defstkunitcode varchar(10), \r\n");
            __script.Append("download_success datetime, \r\n");
            __script.Append("status smallint, \r\n");
            __script.Append("unittype smallint null, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "bcitem primary key clustered(code) \r\n");
            __script.Append("); \r\n");


            string __err = __frameWork._excute(__script.ToString());
            if (__err.Length > 0)
                Console.WriteLine(__err);

            // รายละเอียดหน่วยนับ ของ dts (dts_bcstkpacking)
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "bcstkpacking \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("itemcode varchar(25), \r\n");
            __script.Append("unitcode varchar(10), \r\n");
            __script.Append("rate1 money, \r\n");
            __script.Append("rate2 money, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "bcstkpacking primary key clustered(roworder) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());


            // หน่วยนับที่เกี่ยวข้องกับสินค้า ของ dts (dts_allunitcode)
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "allunitcode \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("itemcode varchar(25), \r\n");
            __script.Append("unitcode varchar(10), \r\n");
            __script.Append("isstkunit smallint, \r\n");
            __script.Append("rate money, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "allunitcode primary key clustered(roworder) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());

            // PO ของ dts (dts_bcpurchaseorder)
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "bcpurchaseorder \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("docno varchar(25), \r\n");
            __script.Append("docdate datetime, \r\n");
            __script.Append("taxtype smallint, \r\n");
            __script.Append("taxrate money, \r\n");
            __script.Append("apcode varchar(25) null, \r\n");
            __script.Append("agent_code varchar(25), \r\n");
            __script.Append("sumofitemamount money, \r\n");
            __script.Append("afterdiscount money, \r\n");
            __script.Append("beforetaxamount money, \r\n");
            __script.Append("taxamount money, \r\n");
            __script.Append("totalamount money, \r\n");
            __script.Append("excepttaxamount money, \r\n");
            __script.Append("zerotaxamount money, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "bcpurchaseorder primary key clustered(docno) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());

            // PO Sub ของ dts (dts_bcpurchaseorder)
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "bcpurchaseordersub \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("docno varchar(25), \r\n");
            __script.Append("docdate datetime, \r\n");
            __script.Append("apcode varchar(25) null, \r\n");
            __script.Append("agent_code varchar(25), \r\n");
            __script.Append("itemcode varchar(25), \r\n");
            __script.Append("itemname varchar(255), \r\n");
            __script.Append("unitcode varchar(10), \r\n");
            __script.Append("qty money, \r\n");
            __script.Append("price money, \r\n");
            __script.Append("amount money, \r\n");
            __script.Append("netamount money, \r\n");
            __script.Append("taxrate smallint, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "bcpurchaseordersub primary key clustered(roworder) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());

            // SO ของ dts (dts_bcsaleorder)
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "bcsaleorder \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("docno varchar(25), \r\n");
            __script.Append("docdate datetime, \r\n");
            __script.Append("taxtype smallint, \r\n");
            __script.Append("taxrate money, \r\n");
            __script.Append("arcode varchar(25) null, \r\n");
            __script.Append("agent_code varchar(25), \r\n");
            __script.Append("sumofitemamount money, \r\n");
            __script.Append("afterdiscount money, \r\n");
            __script.Append("beforetaxamount money, \r\n");
            __script.Append("taxamount money, \r\n");
            __script.Append("totalamount money, \r\n");
            __script.Append("excepttaxamount money, \r\n");
            __script.Append("zerotaxamount money, \r\n");
            __script.Append("ownreceive varchar(100) null, \r\n");
            __script.Append("mydescription varchar(255) null, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "bcsaleorder primary key clustered(docno) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());

            // PO Sub ของ dts (dts_bcpurchaseorder)
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "bcsaleordersub \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("docno varchar(25), \r\n");
            __script.Append("docdate datetime, \r\n");
            __script.Append("arcode varchar(25) null, \r\n");
            __script.Append("agent_code varchar(25), \r\n");
            __script.Append("itemcode varchar(25), \r\n");
            __script.Append("itemname varchar(255), \r\n");
            __script.Append("unitcode varchar(10), \r\n");
            __script.Append("qty money, \r\n");
            __script.Append("price money, \r\n");
            __script.Append("amount money, \r\n");
            __script.Append("netamount money, \r\n");
            __script.Append("taxrate smallint, \r\n");
            __script.Append("originaldate datetime, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "bcsaleordersub primary key clustered(roworder) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());

            // guid_delete 
            __script = new StringBuilder();
            __script.Append("create table guid_delete \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("table_name varchar(100), \r\n");
            __script.Append("guid UNIQUEIDENTIFIER, \r\n");
            __script.Append("constraint pk_guid_delete primary key clustered(roworder) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());

            // download log
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "download \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("guid_download varchar(50), \r\n");
            __script.Append("agent_code varchar(25), \r\n");
            __script.Append("download_flag smallint, \r\n");
            __script.Append("download_date datetime, \r\n");
            __script.Append("download_start datetime, \r\n");
            __script.Append("download_success datetime, \r\n");
            __script.Append("last_status smallint default 0, \r\n");
            __script.Append("download_status smallint default 0, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "download primary key clustered(guid_download) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());

            // download log detail
            __script = new StringBuilder();
            __script.Append("create table " + __dtsPrefixDBName + "download_detail \r\n");
            __script.Append("( \r\n");
            __script.Append("roworder  int IDENTITY(1,1) NOT NULL,\r\n");
            __script.Append("guid_download varchar(50), \r\n");
            __script.Append("agent_code varchar(25), \r\n");
            __script.Append("download_flag smallint, \r\n");
            __script.Append("ref_no varchar(25), \r\n");
            __script.Append("download_date datetime, \r\n");
            __script.Append("download_start datetime, \r\n");
            __script.Append("download_success datetime, \r\n");
            __script.Append("last_status smallint default 0, \r\n");
            __script.Append("download_status smallint default 0, \r\n");
            __script.Append("constraint pk_" + __dtsPrefixDBName + "download_detail primary key clustered(roworder) \r\n");
            __script.Append("); \r\n");

            __frameWork._excute(__script.ToString());
            */

            // trigger on insert update dts_download
            _createClientTrigger("dts_download");
            _createClientTrigger("dts_download_detail");


            // check new branch insert sync_send_data
            StringBuilder __serverQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __serverQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(code) as xcount from " + _g.DataServer.bcitem._table));
            __serverQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(roworder) as xcount from " + _g.DataServer.allunitcode._table));
            __serverQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(roworder) as xcount from " + _g.DataServer.bcstkpacking._table));
            __serverQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(roworder) as xcount from " + _g.DataServer.bcpurchaseorder._table));
            __serverQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(roworder) as xcount from " + _g.DataServer.bcpurchaseordersub._table));
            __serverQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(roworder) as xcount from " + _g.DataServer.bcsaleorder._table));
            __serverQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(roworder) as xcount from " + _g.DataServer.bcsaleordersub._table));
            __serverQuery.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __resultServer = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __serverQuery.ToString());

            string __serverItem = "select count(code) as xcount from " + _g.DataServer.bcitem._table;


            //DataSet __serverItemResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __serverItem);

            //if (__serverItemResult.Tables.Count > 0 && __serverItemResult.Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(__serverItemResult.Tables[0].Rows[0][0].ToString()) > 0)
            if (__resultServer.Count > 0)
            {
                //string __clientItemQuery = "select count(code) as xcount from dts_bcitem ";
                List<string> __queryList = new List<string>();
                __queryList.Add("select count(code) as xCount from dts_bcitem ");
                __queryList.Add("select count(roworder) as xCount from dts_allunitcode ");
                __queryList.Add("select count(roworder) as xcount from dts_bcstkpacking ");

                __queryList.Add("select count(roworder) as xcount from dts_bcpurchaseorder ");
                __queryList.Add("select count(roworder) as xcount from dts_bcpurchaseordersub ");
                __queryList.Add("select count(roworder) as xcount from dts_bcsaleorder ");
                __queryList.Add("select count(roworder) as xcount from dts_bcsaleordersub ");


                ArrayList __rowCheckResult = __frameWork._getDataList(__queryList);

                if (__rowCheckResult.Count > 0)
                {
                    int _bcItemIndex = 0;
                    int _allUnitCodeIndex = 1;
                    int _bcstkpackingIndex = 2;
                    int _bcPurchaseOrder = 3;
                    int __bcPurchaseOrderSub = 4;
                    int __bcsaleorder = 5;
                    int __bcsaleordersub = 6;

                    StringBuilder __syncQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    // เช็คสินค้า
                    if (__resultServer.Count > _bcItemIndex && __resultServer[_bcItemIndex] != null && ((DataSet)__resultServer[_bcItemIndex]).Tables.Count > 0 && ((DataSet)__resultServer[_bcItemIndex]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__resultServer[_bcItemIndex]).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (__rowCheckResult.Count > _bcItemIndex && ((DataSet)__rowCheckResult[_bcItemIndex]).Tables.Count > 0 && ((DataSet)__rowCheckResult[_bcItemIndex]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__rowCheckResult[_bcItemIndex]).Tables[0].Rows[0][0].ToString()) == 0M)
                        {
                            __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcitem\', 1, guid from " + _g.DataServer.bcitem._table));
                        }
                    }

                    // allunitcode
                    if (__resultServer.Count > _allUnitCodeIndex && __resultServer[_allUnitCodeIndex] != null && ((DataSet)__resultServer[_allUnitCodeIndex]).Tables.Count > 0 && ((DataSet)__resultServer[_allUnitCodeIndex]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__resultServer[_allUnitCodeIndex]).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (__rowCheckResult.Count > _allUnitCodeIndex && ((DataSet)__rowCheckResult[_allUnitCodeIndex]).Tables.Count > 0 && ((DataSet)__rowCheckResult[_allUnitCodeIndex]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__rowCheckResult[_allUnitCodeIndex]).Tables[0].Rows[0][0].ToString()) == 0M)
                        {
                            __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'allunitcode\', 1, guid from " + _g.DataServer.allunitcode._table));
                        }
                    }

                    // bcstkpacking
                    if (__resultServer.Count > _bcstkpackingIndex && __resultServer[_bcstkpackingIndex] != null && ((DataSet)__resultServer[_bcstkpackingIndex]).Tables.Count > 0 && ((DataSet)__resultServer[_bcstkpackingIndex]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__resultServer[_bcstkpackingIndex]).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (__rowCheckResult.Count > _bcstkpackingIndex && ((DataSet)__rowCheckResult[_bcstkpackingIndex]).Tables.Count > 0 && ((DataSet)__rowCheckResult[_bcstkpackingIndex]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__rowCheckResult[_bcstkpackingIndex]).Tables[0].Rows[0][0].ToString()) == 0M)
                        {
                            __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcstkpacking\', 1, guid from " + _g.DataServer.bcstkpacking._table));
                        }
                    }

                    // bcPurchaseOrder
                    if (__resultServer.Count > _bcPurchaseOrder && __resultServer[_bcPurchaseOrder] != null && ((DataSet)__resultServer[_bcPurchaseOrder]).Tables.Count > 0 && ((DataSet)__resultServer[_bcPurchaseOrder]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__resultServer[_bcPurchaseOrder]).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (__rowCheckResult.Count > _bcPurchaseOrder && ((DataSet)__rowCheckResult[_bcPurchaseOrder]).Tables.Count > 0 && ((DataSet)__rowCheckResult[_bcPurchaseOrder]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__rowCheckResult[_bcPurchaseOrder]).Tables[0].Rows[0][0].ToString()) == 0M)
                        {
                            __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcpurchaseorder\', 1, guid from " + _g.DataServer.bcpurchaseorder._table + " where " + _g.DataServer.bcpurchaseorder._agentcode + " = '" + MyLib._myGlobal._userCode + "' "));
                        }
                    }

                    // bcPurchaseOrderSub
                    if (__resultServer.Count > __bcPurchaseOrderSub && __resultServer[__bcPurchaseOrderSub] != null && ((DataSet)__resultServer[__bcPurchaseOrderSub]).Tables.Count > 0 && ((DataSet)__resultServer[__bcPurchaseOrderSub]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__resultServer[__bcPurchaseOrderSub]).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (__rowCheckResult.Count > __bcPurchaseOrderSub && ((DataSet)__rowCheckResult[__bcPurchaseOrderSub]).Tables.Count > 0 && ((DataSet)__rowCheckResult[__bcPurchaseOrderSub]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__rowCheckResult[__bcPurchaseOrderSub]).Tables[0].Rows[0][0].ToString()) == 0M)
                        {
                            __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcpurchaseordersub\', 1, guid from " + _g.DataServer.bcpurchaseordersub._table + " where " + _g.DataServer.bcpurchaseordersub._docno + " in (select " + _g.DataServer.bcpurchaseorder._docno + " from " + _g.DataServer.bcpurchaseorder._table + " where " + _g.DataServer.bcpurchaseorder._agentcode + " = '" + MyLib._myGlobal._userCode + "') "));
                        }
                    }

                    // bcsaleorder
                    if (__resultServer.Count > __bcsaleorder && __resultServer[__bcsaleorder] != null && ((DataSet)__resultServer[__bcsaleorder]).Tables.Count > 0 && ((DataSet)__resultServer[__bcsaleorder]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__resultServer[__bcsaleorder]).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (__rowCheckResult.Count > __bcsaleorder && ((DataSet)__rowCheckResult[__bcsaleorder]).Tables.Count > 0 && ((DataSet)__rowCheckResult[__bcsaleorder]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__rowCheckResult[__bcsaleorder]).Tables[0].Rows[0][0].ToString()) == 0M)
                        {
                            __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcsaleorder\', 1, guid from " + _g.DataServer.bcsaleorder._table + " where " + _g.DataServer.bcsaleorder._agentcode + " = '" + MyLib._myGlobal._userCode + "' "));
                        }
                    }

                    // bcsaleordersub
                    if (__resultServer.Count > __bcsaleordersub && __resultServer[__bcsaleordersub] != null && ((DataSet)__resultServer[__bcsaleordersub]).Tables.Count > 0 && ((DataSet)__resultServer[__bcsaleordersub]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__resultServer[__bcsaleordersub]).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        if (__rowCheckResult.Count > __bcsaleordersub && ((DataSet)__rowCheckResult[__bcsaleordersub]).Tables.Count > 0 && ((DataSet)__rowCheckResult[__bcsaleordersub]).Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(((DataSet)__rowCheckResult[__bcsaleordersub]).Tables[0].Rows[0][0].ToString()) == 0M)
                        {
                            __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcsaleordersub\', 1, guid from " + _g.DataServer.bcsaleordersub._table + " where " + _g.DataServer.bcsaleordersub._docno + " in ( select " + _g.DataServer.bcsaleorder._docno + " from  " + _g.DataServer.bcsaleorder._table + " where " + _g.DataServer.bcsaleorder._agentcode + " = '" + MyLib._myGlobal._userCode + "' ) "));
                        }
                    }


                    //if (__clientItemResult.Tables.Count > 0 && __clientItemResult.Tables[0].Rows.Count > 0 && MyLib._myGlobal._decimalPhase(__clientItemResult.Tables[0].Rows[0][0].ToString()) == 0M)
                    //{
                    //    // สั่ง sync ทุกสินค้า
                    //    __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcpurchaseorder\', 1, guid from bcpurchaseorder where agent_code = '" + MyLib._myGlobal._userCode + "' "));
                    //    __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcpurchaseordersub\', 1, guid from bcpurchaseordersub where agent_code = '" + MyLib._myGlobal._userCode + "' "));
                    //    __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcsaleorder\', 1, guid from bcsaleorder where agent_code = '" + MyLib._myGlobal._userCode + "' "));
                    //    __syncQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (agent_code, table_name, command_mode,guid)  select \'" + MyLib._myGlobal._userCode + "\', \'bcsaleordersub\', 1, guid from bcsaleordersub where agent_code = '" + MyLib._myGlobal._userCode + "' "));
                    //    __syncQuery.Append("</node>");
                    __syncQuery.Append("</node>");
                    string __serverResult = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __syncQuery.ToString());
                    Console.WriteLine(__serverResult.ToString());
                    //}
                }
            }

        }

        public static void _createClientTrigger(string tableName)
        {
            _clientFrameWork __frameWork = new _clientFrameWork();


            //guid
            StringBuilder __script = new StringBuilder();
            __script.Append("ALTER TABLE " + tableName + " ADD guid UNIQUEIDENTIFIER default NEWSEQUENTIALID(); ");
            __frameWork._excute(__script.ToString());

            // send success
            __script = new StringBuilder();
            __script.Append("ALTER TABLE " + tableName + " ADD send_success smallint default 0; ");
            __frameWork._excute(__script.ToString());


            // after delete trigger
            string __triggerName = tableName + "_after_delete_trigger";
            __script = new StringBuilder();
            __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
            __script.Append("ON " + tableName + " \r\n");
            __script.Append("AFTER DELETE \r\n");
            __script.Append("AS \r\n");
            __script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
            __script.Append("SELECT @guid =  i.guid FROM deleted i; \r\n");
            __script.Append("begin \r\n");
            __script.Append("insert into guid_delete(table_name, guid) \r\n");
            __script.Append(" values ( '" + tableName + "', @guid ) \r\n");
            __script.Append("end \r\n");
            __frameWork._excute(__script.ToString());

            // before update
            __triggerName = tableName + "_before_update_trigger";
            __script = new StringBuilder();
            __script.Append("CREATE TRIGGER  " + __triggerName + " \r\n");
            __script.Append("ON " + tableName + " \r\n");
            __script.Append("AFTER UPDATE \r\n");
            __script.Append("AS \r\n");
            __script.Append("DECLARE @send_success AS smallint; \r\n");
            __script.Append("DECLARE @guid AS UNIQUEIDENTIFIER; \r\n");
            __script.Append("SELECT @send_success =  i.send_success FROM inserted i; \r\n");
            __script.Append("SELECT @guid =  i.guid FROM inserted i; \r\n");
            __script.Append("BEGIN \r\n");
            __script.Append("IF (UPDATE(send_success)) \r\n");
            __script.Append("BEGIN \r\n");
            __script.Append("RETURN \r\n");
            __script.Append("END \r\n");
            __script.Append("IF @send_success = 1 or @send_success is null \r\n");
            __script.Append("BEGIN \r\n");
            __script.Append("UPDATE dts_download SET send_success = 0 WHERE (send_success = 1 OR @send_success is null) AND guid=@guid \r\n");
            __script.Append("END \r\n");
            __script.Append("END \r\n");
            __frameWork._excute(__script.ToString());


        }

    }

}

// yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy

namespace _g
{
    public class DataClient
    {
        /// <summary>
        /// การส่งข้อมูล
        /// </summary>
        public class dts_download
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_download";
            /// <summary>
            /// รหัส Agent
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// รหัสการ Download
            /// </summary>
            public static String _guid_download = "guid_download";
            /// <summary>
            /// ประเภทการ Download
            /// </summary>
            public static String _download_flag = "download_flag";
            /// <summary>
            /// วันที่ Download
            /// </summary>
            public static String _download_date = "download_date";
            /// <summary>
            /// เริ่มต้น Download
            /// </summary>
            public static String _download_start = "download_start";
            /// <summary>
            /// สิ้นสุด Download
            /// </summary>
            public static String _download_success = "download_success";
            /// <summary>
            /// สถานะการยกเลิก
            /// </summary>
            public static String _last_status = "last_status";
            /// <summary>
            /// สถานะการ Download
            /// </summary>
            public static String _download_status = "download_status";
        }

        /// <summary>
        /// รายละเอียดการส่งข้อมูล
        /// </summary>
        public class dts_download_detail
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_download_detail";
            /// <summary>
            /// รหัสการ Download
            /// </summary>
            public static String _guid_download = "guid_download";
            /// <summary>
            /// รหัสตัวแทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// ประเภทการ Download
            /// </summary>
            public static String _download_flag = "download_flag";
            /// <summary>
            /// รหัสสินค้า/รหัส PO
            /// </summary>
            public static String _ref_no = "ref_no";
            /// <summary>
            /// วันที่ download 
            /// </summary>
            public static String _download_date = "download_date";
            /// <summary>
            /// เริ่มต้น Download
            /// </summary>
            public static String _download_start = "download_start";
            /// <summary>
            /// สินสุด Download
            /// </summary>
            public static String _download_success = "download_success";
            /// <summary>
            /// สถานะ การยกเลิก
            /// </summary>
            public static String _last_status = "last_status";
            /// <summary>
            /// สถานะ การ Download
            /// </summary>
            public static String _download_status = "download_status";
        }

        /// <summary>
        /// ข้อมูลสินค้า
        /// </summary>
        public class dts_bcitem
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_bcitem";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// บาร์โค๊ดสินค้า
            /// </summary>
            public static String _barcode = "barcode";
            /// <summary>
            /// ชื่อสินค้า
            /// </summary>
            public static String _name1 = "name1";
            /// <summary>
            /// หน่วยนับมาตาฐาน
            /// </summary>
            public static String _defstkunitcode = "defstkunitcode";
            /// <summary>
            /// วันที่นำเข้าระบบ
            /// </summary>
            public static String _download_success = "download_success";
            /// <summary>
            /// สถานะใช้งาน
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// สินค้าหลายหน่วยนับหรือไม่
            /// </summary>
            public static String _unittype = "unittype";
            /// <summary>
            /// Category
            /// </summary>
            public static String _groupcode = "groupcode";
            /// <summary>
            /// Sub-Category
            /// </summary>
            public static String _typecode = "typecode";
            /// <summary>
            /// Class
            /// </summary>
            public static String _categorycode = "categorycode";
            /// <summary>
            /// Sub-Class
            /// </summary>
            public static String _formatcode = "formatcode";
        }

        /// <summary>
        /// การสั่งซื้อ
        /// </summary>
        public class dts_bcpurchaseorder
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_bcpurchaseorder";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _docdate = "docdate";
            /// <summary>
            /// ประเภทภาษี
            /// </summary>
            public static String _taxtype = "taxtype";
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public static String _taxrate = "taxrate";
            /// <summary>
            /// รหัสเจ้าหนี้ 
            /// </summary>
            public static String _apcode = "apcode";
            /// <summary>
            /// รหัสตัวแทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// รวมมูลค่าสินค้า
            /// </summary>
            public static String _sumofitemamount = "sumofitemamount";
            /// <summary>
            /// มูลค่าภาษี
            /// </summary>
            public static String _taxamount = "taxamount";
            /// <summary>
            /// มูลค่ารวมภาษี
            /// </summary>
            public static String _totalamount = "totalamount";
            /// <summary>
            /// มูลค่าสุทธิ
            /// </summary>
            public static String _netamount = "netamount";
            /// <summary>
            /// ระยะเวลาขนส่ง
            /// </summary>
            public static String _leadtime = "leadtime";
            /// <summary>
            /// มูลค่าก่อนคิดภาษี
            /// </summary>
            public static String _beforetaxamount = "beforetaxamount";
            /// <summary>
            /// มูลค่ายกเว้นภาษี
            /// </summary>
            public static String _excepttaxamount = "excepttaxamount";
            /// <summary>
            /// มูลค่าอัตราภาษีศูนย์
            /// </summary>
            public static String _zerotaxamount = "zerotaxamount";
            /// <summary>
            /// เครดิต (วัน)
            /// </summary>
            public static String _creditday = "creditday";
            /// <summary>
            /// วันครบกำหนด
            /// </summary>
            public static String _duedate = "duedate";
        }

        /// <summary>
        /// รายละเอียดการสั่งซื้อสินค้า
        /// </summary>
        public class dts_bcpurchaseordersub
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_bcpurchaseordersub";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับ
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// จำนวน
            /// </summary>
            public static String _qty = "qty";
            /// <summary>
            /// ราคา
            /// </summary>
            public static String _price = "price";
            /// <summary>
            /// รวม
            /// </summary>
            public static String _amount = "amount";
            /// <summary>
            /// มูลค่าสทธิ
            /// </summary>
            public static String _netamount = "netamount";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _docdate = "docdate";
            /// <summary>
            /// รหัสเจ้าหนี้
            /// </summary>
            public static String _apcode = "apcode";
            /// <summary>
            /// รหัสผู้แทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// ชื่อสินค้า
            /// </summary>
            public static String _itemname = "itemname";
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public static String _taxrate = "taxrate";
            /// <summary>
            /// Rate 1
            /// </summary>
            public static String _rate1 = "rate1";
            /// <summary>
            /// Rate 2
            /// </summary>
            public static String _rate2 = "rate2";
            /// <summary>
            /// Group Code
            /// </summary>
            public static String _groupcode = "groupcode";
            /// <summary>
            /// Type Code
            /// </summary>
            public static String _typecode = "typecode";
            /// <summary>
            /// Category Code
            /// </summary>
            public static String _categorycode = "categorycode";
            /// <summary>
            /// Format Code
            /// </summary>
            public static String _formatcode = "formatcode";
        }

        /// <summary>
        /// BC Stock Packing
        /// </summary>
        public class dts_bcstkpacking
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_bcstkpacking";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับสินค้า
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// ตัวตั้ง
            /// </summary>
            public static String _rate1 = "rate1";
            /// <summary>
            /// ตัวหาร
            /// </summary>
            public static String _rate2 = "rate2";
        }

        /// <summary>
        /// All Unit Code
        /// </summary>
        public class dts_allunitcode
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_allunitcode";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับ
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// เป็นหน่วยมาตรฐาน
            /// </summary>
            public static String _isstkunit = "isstkunit";
            /// <summary>
            /// อัตราส่วน
            /// </summary>
            public static String _rate = "rate";
        }

        /// <summary>
        /// สังขาย/สั่งจองสินค้า
        /// </summary>
        public class dts_bcsaleorder
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_bcsaleorder";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _docdate = "docdate";
            /// <summary>
            /// ประเภทภาษี
            /// </summary>
            public static String _taxtype = "taxtype";
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public static String _taxrate = "taxrate";
            /// <summary>
            /// รหัสลูกหนี้
            /// </summary>
            public static String _arcode = "arcode";
            /// <summary>
            /// รหัสผู้แทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// รวมมูลค่าสินค้า
            /// </summary>
            public static String _sumofitemamount = "sumofitemamount";
            /// <summary>
            /// มูลค่าหลังลด
            /// </summary>
            public static String _afterdiscount = "afterdiscount";
            /// <summary>
            /// มูลค่าก่อนคิดภาษี
            /// </summary>
            public static String _beforetaxamount = "beforetaxamount";
            /// <summary>
            /// มูลค่าภาษี
            /// </summary>
            public static String _taxamount = "taxamount";
            /// <summary>
            /// มูลค่ารวมภาษี
            /// </summary>
            public static String _totalamount = "totalamount";
            /// <summary>
            /// มูลค่ายกเว้นภาษี
            /// </summary>
            public static String _excepttaxamount = "excepttaxamount";
            /// <summary>
            /// มูลค่าอัตราภาษีศูนย์
            /// </summary>
            public static String _zerotaxamount = "zerotaxamount";
            /// <summary>
            /// ผู้รับสินค้า
            /// </summary>
            public static String _ownreceive = "ownreceive";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _mydescription = "mydescription";
            /// <summary>
            /// มูลค่าสุทธิ
            /// </summary>
            public static String _netamount = "netamount";
            /// <summary>
            /// ส่งมอบภายใน (วัน)
            /// </summary>
            public static String _deliveryday = "deliveryday";
            /// <summary>
            /// เงื่อนไขการส่งมอบ (0=รับเอง, 1=ส่งให้)
            /// </summary>
            public static String _isconditionsend = "isconditionsend";
            /// <summary>
            /// ประเภทเอกสาร
            /// </summary>
            public static String _billtype = "billtype";
            /// <summary>
            /// กำหนดส่งสินค้า
            /// </summary>
            public static String _deliverydate = "deliverydate";
        }

        /// <summary>
        /// รายละเอียดการสั่งซื้อ/สั่งจอง
        /// </summary>
        public class dts_bcsaleordersub
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_bcsaleordersub";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับ
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// จำนวน
            /// </summary>
            public static String _qty = "qty";
            /// <summary>
            /// ราคา
            /// </summary>
            public static String _price = "price";
            /// <summary>
            /// รวม
            /// </summary>
            public static String _amount = "amount";
            /// <summary>
            /// มูลค่าสุทธิ
            /// </summary>
            public static String _netamount = "netamount";
            /// <summary>
            /// กำหนดส่งสินค้า
            /// </summary>
            public static String _originaldate = "originaldate";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _docdate = "docdate";
            /// <summary>
            /// รหัสลูกหนี้
            /// </summary>
            public static String _arcode = "arcode";
            /// <summary>
            /// รหัสผู้แทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// ชื่อสินค้า
            /// </summary>
            public static String _itemname = "itemname";
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public static String _taxrate = "taxrate";
            /// <summary>
            /// Rate 1
            /// </summary>
            public static String _rate1 = "rate1";
            /// <summary>
            /// Rate 2
            /// </summary>
            public static String _rate2 = "rate2";
            /// <summary>
            /// Group Code
            /// </summary>
            public static String _groupcode = "groupcode";
            /// <summary>
            /// Type Code
            /// </summary>
            public static String _typecode = "typecode";
            /// <summary>
            /// Category Code
            /// </summary>
            public static String _categorycode = "categorycode";
            /// <summary>
            /// Format Code
            /// </summary>
            public static String _formatcode = "formatcode";
        }
    }
}

// zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz

namespace _g
{
    public class DataServer
    {
        /// <summary>
        /// การส่งข้อมูล
        /// </summary>
        public class dts_download
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_download";
            /// <summary>
            /// รหัส Agent
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// รหัสการ Download
            /// </summary>
            public static String _guid_download = "guid_download";
            /// <summary>
            /// ประเภทการ Download
            /// </summary>
            public static String _download_flag = "download_flag";
            /// <summary>
            /// วันที่ Download
            /// </summary>
            public static String _download_date = "download_date";
            /// <summary>
            /// เริ่มต้น Download
            /// </summary>
            public static String _download_start = "download_start";
            /// <summary>
            /// สิ้นสุด Download
            /// </summary>
            public static String _download_success = "download_success";
            /// <summary>
            /// สถานะการยกเลิก
            /// </summary>
            public static String _last_status = "last_status";
            /// <summary>
            /// สถานะการ Download
            /// </summary>
            public static String _download_status = "download_status";
        }

        /// <summary>
        /// รายละเอียดการส่งข้อมูล
        /// </summary>
        public class dts_download_detail
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_download_detail";
            /// <summary>
            /// รหัสการ Download
            /// </summary>
            public static String _guid_download = "guid_download";
            /// <summary>
            /// รหัสตัวแทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// ประเภทการ Download
            /// </summary>
            public static String _download_flag = "download_flag";
            /// <summary>
            /// รหัสสินค้า/รหัส PO
            /// </summary>
            public static String _ref_no = "ref_no";
            /// <summary>
            /// วันที่ download 
            /// </summary>
            public static String _download_date = "download_date";
            /// <summary>
            /// เริ่มต้น Download
            /// </summary>
            public static String _download_start = "download_start";
            /// <summary>
            /// สินสุด Download
            /// </summary>
            public static String _download_success = "download_success";
            /// <summary>
            /// สถานะ การยกเลิก
            /// </summary>
            public static String _last_status = "last_status";
            /// <summary>
            /// สถานะ การ Download
            /// </summary>
            public static String _download_status = "download_status";
        }

        /// <summary>
        /// ข้อมูลสินค้า
        /// </summary>
        public class bcitem
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "bcitem";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _code = "code";
            /// <summary>
            /// บาร์โค๊ดสินค้า
            /// </summary>
            public static String _barcode = "barcode";
            /// <summary>
            /// ชื่อสินค้า
            /// </summary>
            public static String _name1 = "name1";
            /// <summary>
            /// หน่วยนับมาตาฐาน
            /// </summary>
            public static String _defstkunitcode = "defstkunitcode";
            /// <summary>
            /// วันที่นำเข้าระบบ
            /// </summary>
            public static String _download_success = "download_success";
            /// <summary>
            /// สถานะใช้งาน
            /// </summary>
            public static String _status = "status";
            /// <summary>
            /// สินค้าหลายหน่วยนับหรือไม่
            /// </summary>
            public static String _unittype = "unittype";
            /// <summary>
            /// Category
            /// </summary>
            public static String _groupcode = "groupcode";
            /// <summary>
            /// Sub-Category
            /// </summary>
            public static String _typecode = "typecode";
            /// <summary>
            /// Class
            /// </summary>
            public static String _categorycode = "categorycode";
            /// <summary>
            /// Sub-Class
            /// </summary>
            public static String _formatcode = "formatcode";
        }

        /// <summary>
        /// การสั่งซื้อ
        /// </summary>
        public class bcpurchaseorder
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "bcpurchaseorder";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _docdate = "docdate";
            /// <summary>
            /// ประเภทภาษี
            /// </summary>
            public static String _taxtype = "taxtype";
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public static String _taxrate = "taxrate";
            /// <summary>
            /// รหัสเจ้าหนี้ 
            /// </summary>
            public static String _apcode = "apcode";
            /// <summary>
            /// รหัสตัวแทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// รวมมูลค่าสินค้า
            /// </summary>
            public static String _sumofitemamount = "sumofitemamount";
            /// <summary>
            /// มูลค่าภาษี
            /// </summary>
            public static String _taxamount = "taxamount";
            /// <summary>
            /// มูลค่ารวมภาษี
            /// </summary>
            public static String _totalamount = "totalamount";
            /// <summary>
            /// มูลค่าสุทธิ
            /// </summary>
            public static String _netamount = "netamount";
            /// <summary>
            /// ระยะเวลาขนส่ง
            /// </summary>
            public static String _leadtime = "leadtime";
        }

        /// <summary>
        /// รายละเอียดการสั่งซื้อสินค้า
        /// </summary>
        public class bcpurchaseordersub
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "bcpurchaseordersub";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับ
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// จำนวน
            /// </summary>
            public static String _qty = "qty";
            /// <summary>
            /// ราคา
            /// </summary>
            public static String _price = "price";
            /// <summary>
            /// รวม
            /// </summary>
            public static String _amount = "amount";
            /// <summary>
            /// มูลค่าสทธิ
            /// </summary>
            public static String _netamount = "netamount";
        }

        /// <summary>
        /// ข้อมูลตัวแทน
        /// </summary>
        public class dts_agent
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "dts_agent";
            /// <summary>
            /// รหัสตัวแทน
            /// </summary>
            public static String _agent_code = "agent_code";
            /// <summary>
            /// ชื่อตัวแทน
            /// </summary>
            public static String _agent_name = "agent_name";
            /// <summary>
            /// ที่อยู่
            /// </summary>
            public static String _agent_addres = "agent_addres";
            /// <summary>
            /// User Code
            /// </summary>
            public static String _user_code = "user_code";
            /// <summary>
            /// Password
            /// </summary>
            public static String _user_password = "user_password";
            /// <summary>
            /// ชื่อผู้ติดต่อ
            /// </summary>
            public static String _contact_name = "contact_name";
            /// <summary>
            /// โทรศัพท์
            /// </summary>
            public static String _tel = "tel";
        }

        /// <summary>
        /// รายละเอียดการแสดงผลแต่ละหน้าจอ
        /// </summary>
        public class erp_view_table
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_view_table";
            /// <summary>
            /// รหัสหน้าจอ
            /// </summary>
            public static String _screen_code = "screen_code";
            /// <summary>
            /// ชื่อหน้าจอ
            /// </summary>
            public static String _name_1 = "name_1";
            /// <summary>
            /// ชื่อหน้าจอ (ภาษาอังกฤษ)
            /// </summary>
            public static String _name_2 = "name_2";
            /// <summary>
            /// ชื่อตารางข้อมูลหลัก
            /// </summary>
            public static String _table_name = "table_name";
            /// <summary>
            /// ชื่อตารางข้อมูลที่เกี่ยวข้อง
            /// </summary>
            public static String _table_list = "table_list";
            /// <summary>
            /// ชื่อข้อมูลที่จะเรียง
            /// </summary>
            public static String _sort = "sort";
            /// <summary>
            /// เงื่อนไขการแสดงผล
            /// </summary>
            public static String _filter = "filter";
            /// <summary>
            /// ความกว้างเป็นเปอร์เซ็นต์
            /// </summary>
            public static String _width_persent = "width_persent";
        }

        /// <summary>
        /// รายละเอียดหน้าจอค้นหา
        /// </summary>
        public class erp_view_column
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "erp_view_column";
            /// <summary>
            /// กลุ่มหน้าจอ
            /// </summary>
            public static String _screen_group = "screen_group";
            /// <summary>
            /// รหัสหน้าจอ
            /// </summary>
            public static String _screen_code = "screen_code";
            /// <summary>
            /// ลำดับที่
            /// </summary>
            public static String _column_number = "column_number";
            /// <summary>
            /// ชื่อ
            /// </summary>
            public static String _column_name = "column_name";
            /// <summary>
            /// ชื่อ (ภาษาอังกฤษ)
            /// </summary>
            public static String _column_name_2 = "column_name_2";
            /// <summary>
            /// ชื่อข้อมูล
            /// </summary>
            public static String _column_field_name = "column_field_name";
            /// <summary>
            /// ชื่อข้อมูลเพื่อเรียง
            /// </summary>
            public static String _column_field_sort = "column_field_sort";
            /// <summary>
            /// ความกว้าง
            /// </summary>
            public static String _column_width = "column_width";
            /// <summary>
            /// ประเภท
            /// </summary>
            public static String _column_type = "column_type";
            /// <summary>
            /// จัดช่อง
            /// </summary>
            public static String _column_align = "column_align";
            /// <summary>
            /// รูปแบบ
            /// </summary>
            public static String _column_format = "column_format";
            /// <summary>
            /// Resource
            /// </summary>
            public static String _column_resource = "column_resource";
        }

        /// <summary>
        /// BC Stock Packing
        /// </summary>
        public class bcstkpacking
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "bcstkpacking";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับสินค้า
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// ตัวตั้ง
            /// </summary>
            public static String _rate1 = "rate1";
            /// <summary>
            /// ตัวหาร
            /// </summary>
            public static String _rate2 = "rate2";
        }

        /// <summary>
        /// All Unit Code
        /// </summary>
        public class allunitcode
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "allunitcode";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับ
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// เป็นหน่วยมาตรฐาน
            /// </summary>
            public static String _isstkunit = "isstkunit";
            /// <summary>
            /// อัตราส่วน
            /// </summary>
            public static String _rate = "rate";
        }

        /// <summary>
        /// สังขาย/สั่งจองสินค้า
        /// </summary>
        public class bcsaleorder
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "bcsaleorder";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// วันที่เอกสาร
            /// </summary>
            public static String _docdate = "docdate";
            /// <summary>
            /// ประเภทภาษี
            /// </summary>
            public static String _taxtype = "taxtype";
            /// <summary>
            /// อัตราภาษี
            /// </summary>
            public static String _taxrate = "taxrate";
            /// <summary>
            /// รหัสลูกหนี้
            /// </summary>
            public static String _arcode = "arcode";
            /// <summary>
            /// รหัสผู้แทนจำหน่าย
            /// </summary>
            public static String _agentcode = "agentcode";
            /// <summary>
            /// รวมมูลค่าสินค้า
            /// </summary>
            public static String _sumofitemamount = "sumofitemamount";
            /// <summary>
            /// มูลค่าหลังลด
            /// </summary>
            public static String _afterdiscount = "afterdiscount";
            /// <summary>
            /// มูลค่าก่อนคิดภาษี
            /// </summary>
            public static String _beforetaxamount = "beforetaxamount";
            /// <summary>
            /// มูลค่าภาษี
            /// </summary>
            public static String _taxamount = "taxamount";
            /// <summary>
            /// มูลค่ารวมภาษี
            /// </summary>
            public static String _totalamount = "totalamount";
            /// <summary>
            /// มูลค่ายกเว้นภาษี
            /// </summary>
            public static String _excepttaxamount = "excepttaxamount";
            /// <summary>
            /// มูลค่าอัตราภาษีศูนย์
            /// </summary>
            public static String _zerotaxamount = "zerotaxamount";
            /// <summary>
            /// ผู้รับสินค้า
            /// </summary>
            public static String _ownreceive = "ownreceive";
            /// <summary>
            /// หมายเหตุ
            /// </summary>
            public static String _mydescription = "mydescription";
            /// <summary>
            /// มูลค่าสุทธิ
            /// </summary>
            public static String _netamount = "netamount";
            /// <summary>
            /// ส่งมอบภายใน (วัน)
            /// </summary>
            public static String _deliveryday = "deliveryday";
            /// <summary>
            /// เงือนไขการส่งมอบ (0=รับเอง, 1=ส่งให้)
            /// </summary>
            public static String _isconditionsend = "isconditionsend";
            /// <summary>
            /// ประเภทเอกสาร
            /// </summary>
            public static String _billtype = "billtype";
        }

        /// <summary>
        /// รายละเอียดการสั่งซื้อ/สั่งจอง
        /// </summary>
        public class bcsaleordersub
        {
            /// <summary>
            /// ชื่อ Table
            /// </summary>
            public static String _table = "bcsaleordersub";
            /// <summary>
            /// เลขที่เอกสาร
            /// </summary>
            public static String _docno = "docno";
            /// <summary>
            /// รหัสสินค้า
            /// </summary>
            public static String _itemcode = "itemcode";
            /// <summary>
            /// หน่วยนับ
            /// </summary>
            public static String _unitcode = "unitcode";
            /// <summary>
            /// จำนวน
            /// </summary>
            public static String _qty = "qty";
            /// <summary>
            /// ราคา
            /// </summary>
            public static String _price = "price";
            /// <summary>
            /// รวม
            /// </summary>
            public static String _amount = "amount";
            /// <summary>
            /// มูลค่าสุทธิ
            /// </summary>
            public static String _netamount = "netamount";
            /// <summary>
            /// กำหนดส่งสินค้า
            /// </summary>
            public static String _originaldate = "originaldate";
        }




    }
}
