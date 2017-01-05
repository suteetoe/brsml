using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Npgsql;
using System.IO;
using System.Data;

namespace Java
{
    public class _routine
    {

    // ข้อมูลหลักสำหรับเก็บทุกอย่างในระบบ
    /**
     * สำหรับเก็บค่าของระบบฐานข้อมูล (MSSQL,mySQL) ตัวแปรที่ดึงจาก
     * C:\smlconfig\Database Config.xml (ถ้ายังไม่แก้เป็นระบบอื่นๆ)
     */
    // 1=PostgreSQL
    // 2=MySQL
    // 3=Microsoft SQL 2000
    // 4=Microsoft SQL 2005
    // 5=Oracle
    public int _databaseID = 1;
    // public Boolean _silverLightMode = false;
    /**
     * ชื่อ Database Server ที่ได้จาก C:\smlconfig\Database Config.xml
     * (ถ้ายังไม่แก้เป็นระบบอื่นๆ)
     */
    private String _databaseServer = "";
    /**
     * ชื่อฐานข้อมูลหลัก ที่ใช้เก็บข้อมูลของ Enterprise เช่น รายชื่อฐานข้อมูล
     * รายชื่อผู้ใช้ข้อมูล
     */
    public String _mainDatabaseName = "";
    /**
     * ชื่อ User สำหรับ Login เข้าสู่ฐานข้อมูล ได้จาก C:\smlconfig\Database
     * Config.xml (ถ้ายังไม่แก้เป็นระบบอื่นๆ)
     */
    private String _userCode = "";
    /**
     * รหัสการเข้าถึงฐานข้อมูล ดึงมาจาก C:\smlconfig\Database Config.xml
     * (ถ้ายังไม่แก้เป็นระบบอื่นๆ)
     */
    private String _userPassword = "";
    /**
     * ชื่อโครงสร้างฐานข้อมูลหลัก ที่เป็น XML สำหรับใช้งานทั่วไป (Create,Verify)
     */
    private String _mainDatabaseStruct = "";
    /**
     * ชื่อของ Default Config รับค่ามาจาก javascript
     */
    public String _databaseConfig = "";
    /**
     * ได้มาจาก _databaseConfig แต่เพิ่ม Path เข้าไป เพื่อให้สะดวก
     */
    private String _databaseConfigFile = "";
    /**
     * ได้มาจาก _mailDatabaseStruct แล้วเพิ่ม Path เข้าไป
     */
    public String _mainDatabaseStructFile = "";
    /**
     * มี Config หรือไม่ เพื่อจะได้ดึงข้อมูลได้ถูกต้อง
     * (กรณีนี้กำหนดไว้เพื่อจะได้ไม่ต้องดึงซ้ำซ้อน ในกรณีเรียก Function
     * ซ้ำกันไปมา)
     */
    Boolean _haveConfigFile;
    /**
     * ประเภทของฐานข้อมูล 0=Microsoft SQL
     */
    Boolean _isNewTable = false;
    // int _startDropIndex = 0;
    //
    public String _xmlTagHead = "<?xml version=\'1.0\' encoding=\'utf-8\'?>";
    public String _xmlTagBegin = "<xmlresource>";
    public String _xmlTagEnd = "</xmlresource>";
    // MOO text
    Object[] _databaseColumnTypeList = new Object[]{"varchar", "currency", "int", "tinyint", "smallint", "float", "date", "image", "text"};
        /// <summary>
        /// Postgre Connection (ต่อค้างไว้ตลอดเวลา)
        /// </summary>
        Npgsql.NpgsqlConnection _postgreConnect=null;

    public String _encrypt(String text) {
        return text;
    }

    public String _decrypt(String text) {
        return text;
    }

    // ดึงตัวแปรจาก Tag XML
    /**
     * ตัวช่วยสำหรับดึงค่า Value ออกจาก XML
     *
     * @param firstElement
     *            Element ที่ต้องการ
     * @param tagName
     *            ชื่อ Tag ที่ต้องการ
     * @return ข้อมูลที่อยู่ระหว่าง Tag (Value)
     */
    //private
    public String _xmlGetNodeValue(XmlElement  firstElement, String tagName) {
        try {
            XmlNodeList __firstNameList = firstElement.GetElementsByTagName(tagName);
            if (__firstNameList.Count > 0) {
                XmlElement __firstNameElement = (XmlElement) __firstNameList.Item(0);
                XmlNodeList __textFNList = __firstNameElement.ChildNodes;
                if (__textFNList.Count > 0) {
                    XmlNode __getData = __textFNList.Item(0);
                    return __getData.Value.ToString().Trim();
                }
            }
        } catch (Exception __ex) {
            //Console.Out.WriteLine("_xmlGetNodeValue:" + __ex.Message.ToString());
        }
        return "";
    }

    public int _executeNonQuery(string query) {
            Npgsql.NpgsqlCommand __cmd = new NpgsqlCommand(query, this._postgreConnect);
        return __cmd .ExecuteNonQuery();
    }

        public String _insertLanguageToTable(String databaseName, String xmlName) {
        String __result = "";
        String __lastQuery = "";
        try {
            //Connection __conn = _connect(databaseName);
            //Statement __statment = __conn.createStatement();
            string __q1="truncate table sml_language";
            this._executeNonQuery(__q1);
            //__statment.execute("truncate table sml_language");
            //
            String __bufferStructFileName = _readXmlFile(xmlName);
            XmlDocument __doc = new XmlDocument();
            __doc.LoadXml(__bufferStructFileName);
            XmlNodeList _listOfTable = __doc.GetElementsByTagName("row");
            for (int __table = 0; __table < _listOfTable.Count; __table++) {
                XmlElement __tableElement = (XmlElement) _listOfTable.Item(__table);
                XmlNodeList __readerField = __tableElement.GetElementsByTagName("field");
                StringBuilder __query = new StringBuilder();
                __query.Append("insert into sml_language (thai_lang,english_lang,chinese_lang,malay_lang,india_lang) values (");
                StringBuilder __queryValue = new StringBuilder();
                for (int __field = 0; __field < __readerField.Count; __field++) {
                    XmlElement __fieldElement = (XmlElement) __readerField.Item(__field);
                    String __langName = __fieldElement.GetAttribute("name");
                    XmlNodeList __xx = __fieldElement.ChildNodes;
                    String __langDetail = __fieldElement.InnerText.Replace("\'", "\'\'");
                    if (__langName.ToLower().Equals("roworder") == false) {
                        if (__queryValue.Length > 0) {
                            __queryValue.Append(",");
                        }
                        __queryValue.Append("\'").Append(__langDetail).Append("\'");
                    }
                }
                __query.Append(__queryValue.ToString()).Append(")");
                __lastQuery = __query.ToString();
                this._executeNonQuery(__query.ToString());
            }
            //
            /*DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
            Document __doc = __docBuilder.parse(new InputSource(new StringReader(__bufferStructFileName)));
            __doc.getDocumentElement().normalize();
            NodeList _listOfTable = __doc.getElementsByTagName("row");
            for (int __table = 0; __table < _listOfTable.Count; __table++) {
                Element __tableElement = (Element) _listOfTable.item(__table);
                NodeList __readerField = __tableElement.getElementsByTagName("field");
                StringBuilder __query = new StringBuilder();
                __query.Append("insert into sml_language (thai_lang,english_lang,chinese_lang,malay_lang,india_lang) values (");
                StringBuilder __queryValue = new StringBuilder();
                for (int __field = 0; __field < __readerField.Count; __field++) {
                    Element __fieldElement = (Element) __readerField.item(__field);
                    String __langName = __fieldElement.getAttribute("name");
                    NodeList __xx = __fieldElement.getChildNodes();
                    String __langDetail = __fieldElement.getTextContent().Replace("\'", "\'\'");
                    if (__langName.ToLower().Equals("roworder") == false) {
                        if (__queryValue.Length > 0) {
                            __queryValue.Append(",");
                        }
                        __queryValue.Append("\'").Append(__langDetail).Append("\'");
                    }
                }
                __query.Append(__queryValue.ToString()).Append(")");
                __lastQuery = __query.ToString();
                __statment.execute(__query.ToString());
            }*/
        } catch (Exception __ex) {
            __result = __ex.Message.ToString();
        }
        return __result;
    }

    /**
     * อ่านไฟล์ xml
     *
     * @param xmlName
     *            ไฟล์ xml ที่ต้องการอ่าน
     * @return
     */
    public String _readXmlFile(String xmlName) {
        String __readLine = "";
        try {
            // Reader __input = new InputStreamReader(new FileInputStream(xmlName));
            //     BufferedReader __in = new BufferedReader(__input);
            String __tempDir = System.IO.Path.GetTempPath();
            StreamReader streamReader = new StreamReader(__tempDir + "/" + xmlName);
            __readLine = streamReader.ReadToEnd();
            streamReader.Close();

            /*BufferedReader __in = new BufferedReader(new InputStreamReader(new FileInputStream(__tempDir + "/" + xmlName), "UTF8"));
            char[] __cBuf = new char[65536];
            StringBuilder __stringBuf = new StringBuilder();
            int __readThisTime = 0;
            while (__readThisTime != -1) {
                try {
                    __readThisTime = __in.read(__cBuf, 0, 65536);
                    __stringBuf.Append(__cBuf, 0, __readThisTime);
                } catch (Exception __ex) {
                }
            } // end while
            __readLine = __stringBuf.ToString();
            __in.close();*/
        } catch (Exception __ex) {
            Console.Out.WriteLine("_readXmlFile:" + __ex.Message.ToString());
            __readLine = __ex.Message.ToString();
        }
        return __readLine;
    }

    /*public String _convertXmlToText(String source) {
        StringBuilder __xmlResult = new StringBuilder();
        String __result = source.ToString().Replace("&amp;", "&");
        __result = __result.Replace("&lt;", "<");
        __result = __result.Replace("&gt;", ">");
        //---------------------------------------------------------------------------------------
        // MOO
        __result = __result.Replace("&quot;", "\"");
        __result = __result.Replace("&apos;", "\'");
        //---------------------------------------------------------------------------------------
        return __result;
    }*/

    /**
     * แปลงข้อความให้เป็น HTML เพื่อไม่ให้ XML เกิด Error
     *
     * @param source
     *            ต้นฉบับ
     */
    public String _convertTextToXml(String source) {
        StringBuilder __xmlResult = new StringBuilder();
        String __result = source.ToString().Replace("&", "&amp;");
        __result = __result.Replace("<", "&lt;");
        __result = __result.Replace(">", "&gt;");
        //---------------------------------------------------------------------------------------
        // MOO
        __result = __result.Replace("\"", "&quot;");
        __result = __result.Replace("\'", "&apos;");
        //---------------------------------------------------------------------------------------
        return __result;
    }

    public String _convertTextToXml(String tagName, String source) {
        StringBuilder __xmlResult = new StringBuilder();
        String __result = source.ToString().Replace("&", "&amp;");
        __result = __result.Replace("<", "&lt;");
        __result = __result.Replace(">", "&gt;");
        //---------------------------------------------------------------------------------------
        // MOO
        __result = __result.Replace("\"", "&quot;");
        __result = __result.Replace("\'", "&apos;");
        //---------------------------------------------------------------------------------------
        __xmlResult.Append("<").Append(tagName).Append(">").Append(__result).Append("</").Append(tagName).Append(">");
        return __xmlResult.ToString();
    }

    public String _convertDoubleToXml(String tagName, Double source) {
        return "<" + tagName + ">" + source.ToString() + "</" + tagName + ">";
    }

    public String _addUpper(String value) {
        if (this._databaseID == 1) {
            // PostgreSql
            return "upper(" + value + ")";
        }
        return value;
    }

    /**
     * สำหรับเปลี่ยนชื่อ Config ที่ใช้ติดต่อกับฐานข้อมูล
     *
     * @param xmlFileName
     *            ชื่อของ XML File ที่ต้องการเปลี่ยนใหม่ (default : Database
     *            Config.xml)
     */
    public void _changeDatabaseConfig(String xmlFileName) {
        this._databaseConfig = xmlFileName;
    }

    public String _systemLogin(String password) {
        String __fileName = "systempassword.txt";
        String __password = "1234567890";
        try {
            String __tempDir = System.IO.Path.GetTempPath();
            using (StreamReader sr = File.OpenText(__tempDir + "/" + __fileName))
            {
                __password = sr.ReadLine();
                sr.Close();
            }
        } catch (Exception __ex) {
            Console.Out.WriteLine("_systemLogin:" + __ex.Message.ToString());
        }
        if (__password.Equals(password)) {
            return "1";
        }
        return "0";
    }

    public String _systemChangePassword(String oldPassword, String newPassword) {
        if (_systemLogin(oldPassword).Equals("1")) {
            String __fileName = "systempassword.txt";
            try {
            String __tempDir = System.IO.Path.GetTempPath();
                return "1";
            } catch (Exception __ex) {
                Console.Out.WriteLine("_systemChangePassword:" + __ex.Message.ToString());
            }
        }
        return "0";
    }

    public String _providerLoad(String password) {
        String __fileName = "providerlist.txt";
        StringBuilder __result = new StringBuilder();
        if (_systemLogin(password).Equals("1")) {
            try {
            String __tempDir = System.IO.Path.GetTempPath();
                if (File.Exists(__tempDir + "/" + __fileName) == true) {
                    String __text = null;
                    using (StreamReader sr = File.OpenText(__tempDir + "/" + __fileName))
                    {
                            while ((__text = sr.ReadLine()) != null) {
                                __result.Append(__text).Append("<br>");
                                }
                            sr.Close();
                    }
                }
            } catch (Exception __ex) {
                Console.Out.WriteLine("_providerLoad:" + __ex.Message.ToString());
            }
        }
        return __result.ToString();
    }

    public String _providerSave(String password, String source) {
        String __fileName = "providerlist.txt";
        if (_systemLogin(password).Equals("1")) {
            try {
            String __tempDir = System.IO.Path.GetTempPath();
           using (StreamWriter outfile =         	new StreamWriter(__tempDir + "/" + __fileName))
            {
                outfile.Write(source.ToString());
               outfile.Close();
            }
                return "1";
            } catch (Exception __ex) {
                Console.Out.WriteLine("_providerSave:" + __ex.Message.ToString());
            }
        }
        return "0";
    }

    /**
     * ทดสอบ connect กับ Database Service
     *
     * @param configFileName
     *            ไฟล์ xml เริ่มต้นที่ต้องการสร้าง
     * @param mainDatabase
     *            ชื่อ database
     * @param mainDatabaseStruct
     *            โครงสร้างของ main database
     * @param databaseConfig
     *            ชื่อ ไฟล์ config
     * @param sqlServerName
     *            ชื่อ Database Server
     * @param sqlUserCode
     *            รหัส Database Server
     * @param sqlUserPassword
     *            รหัสผ่าน Datbase Server
     * @return String "1" ถ้าสำเร็จ : ถ้าไม่สำเร็จ Return Error Message
     */
    public String _testConnect(String configFileName, String mainDatabase, String mainDatabaseStruct, String databaseConfig, String sqlServerName, String sqlUserCode, String sqlUserPassword) {
        String __result = "";
        this._databaseServer = this._decrypt(sqlServerName);
        this._userCode = this._decrypt(sqlUserCode);
        this._userPassword = this._decrypt(sqlUserPassword);
        try {
            // ทดสอบว่าระบบสามารถติดต่อกับ Database Server ได้
            String __tempDir = System.IO.Path.GetTempPath();
            //Connection __con = _connect("");
            //__con.close();
            __result = "1";
            // ในกรณีไม่มี Error โปรแกรมจะสร้าง Database Config.xml
            // ให้โดยอัตโนมัติ เพื่อใช้กับระบบต่อไป
                string __fileName = __tempDir + "/" + configFileName;
            try {
                File.Delete(__fileName );
            } catch (Exception __ex) {
                Console.Out.WriteLine("_testConnect:" + __ex.Message.ToString());
            }
            String __xmlStr = "<?xml version=\'1.0\' encoding=\'utf-8\' ?><node>";
            __xmlStr += "<server>" + sqlServerName + "</server>";
            __xmlStr += "<user>" + sqlUserCode + "</user>";
            __xmlStr += "<password>" + sqlUserPassword + "</password>";
            __xmlStr += "<main_database>" + mainDatabase + "</main_database>";
            __xmlStr += "<main_database_struct>" + mainDatabaseStruct + "</main_database_struct>";
            __xmlStr += "<database_config>" + databaseConfig + "</database_config>";
            __xmlStr += "<database_id>" + this._databaseID + "</database_id>";
            __xmlStr += "</node>";
            using (StreamWriter outfile = new StreamWriter(__fileName))
        {
            outfile.Write(__xmlStr.ToString());
                outfile.Close();
        }
        } catch (Exception e) {
            Console.Out.WriteLine("_testConnect:" + e.Message);
            __result = e.Message;
        }
        return __result;
    }

    /**
     * ดึงชื่อ Table ที่อยู่ใน XML File
     *
     * @param mode
     * @param configFileName
     * @param structFileName
     * @return
     */
    public ArrayList _getTableList(String configFileName, String structFileName) {
        String __databaseStructFile = structFileName;
        ArrayList __result = new ArrayList();
        String __fileName = __databaseStructFile;
        Boolean __found = false;
        String __bufferFileName = _readXmlFile(__fileName);
        try {
            XmlDocument __doc = new XmlDocument();
            __doc .LoadXml(__bufferFileName);
            XmlNodeList _listOfTable = __doc.GetElementsByTagName("table");
            for (int __table = 0; __table < _listOfTable.Count && __found == false; __table++) {
                XmlElement __tableElement = (XmlElement) _listOfTable.Item(__table);
                StringBuilder __tableName = new StringBuilder();
                __tableName.Append(__tableElement.GetAttribute("name")).Append(",");
                __tableName.Append(__tableElement.GetAttribute("thai")).Append(",");
                __tableName.Append(__tableElement.GetAttribute("eng")).Append(",");
                __tableName.Append(__tableElement.GetAttribute("xversion"));
                __result.Add(__tableName);
            } // for
        } catch (Exception __ex) {
            Console.Out.WriteLine("_getTableList:" + __ex.Message.ToString());
        }
        return __result;
    }

    /**
     * Verify Database เป็นตัวเสริมของ Function Verify Database
     *
     * @param configFileName
     * @param databaseGroup
     * @param databaseName
     * @param selectTableName
     * @param structFileName
     * @return
     */
    public String _verifyDatabase(String configFileName, String databaseGroup, String databaseName, String selectTableName, String structFileName) {
        String __databaseStructFile = structFileName;
        return _verifyDatabase(configFileName, databaseGroup, databaseName, selectTableName, __databaseStructFile, true);
    }

    /**
     * สร้างฐานข้อมูลใหม่
     *
     * @param configFileName
     * @param databaseGroup
     * @param databaseName
     * @param selectTableName
     * @param structFileName
     * @return
     */
    public String _createDatabaseAndTable(String configFileName, String databaseGroup, String databaseName, String selectTableName, String structFileName) {
        String __databaseStructFile = structFileName;
        return _verifyDatabase(configFileName, databaseGroup, databaseName, selectTableName, __databaseStructFile, true);
    }

    /**
     * ค้นหา Database ว่ามีอยู่หรือไม่
     *
     * @param configFileName
     * @param databaseName
     * @return
     */
    public String _findDatabase(String configFileName, String databaseName) {
        String __result = "database found";
        this._mainDatabaseName = databaseName;
        // ตรวจสอบ SML Main
        // ดูว่ามี database หรือไม่
        switch (this._databaseID) {
            case 1:
                // PostgreSQL
                try {
                    //Connection __con = _connect(this._mainDatabaseName);
                    //__con.close();
                } catch (Exception __ex) {
                    Console.Out.WriteLine(__ex.Message.ToString());
                    __result = "";
                }
                break;
            case 2:
                // MySQL
                try {
                    //Connection __con = _connect(this._mainDatabaseName);
                    //__con.close();
                } catch (Exception __ex) {
                    Console.Out.WriteLine("_findDatabase:" + __ex.Message.ToString());
                    __result = "";
                }
                break;
            case 3:
            case 4:
                // Microsoft SQL
                try {
                    //Connection __con = _connect(this._mainDatabaseName);
                    //__con.close();
                } catch (Exception __ex) {
                    Console.Out.WriteLine("_findDatabase:" + __ex.Message.ToString());
                    __result = "";
                }
                break;
        }
        return __result;
    }

    public void _verifyDatabaseScript(String scriptName, String configFileName, String databaseGroup, String databaseName, String structFileName) {
        String __bufferStructFileName = _readXmlFile(structFileName);
        try {
            /*Connection __con = _connect(databaseName);
            String __databaseType = "postgres";
            DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
            Document __doc = __docBuilder.parse(new InputSource(new StringReader(__bufferStructFileName)));
            __doc.getDocumentElement().normalize();
            NodeList _listOfTable = __doc.getElementsByTagName(scriptName);
            for (int __table = 0; __table < _listOfTable.Count; __table++) {
                Element __tableElement = (Element) _listOfTable.item(__table);
                NodeList __database = __tableElement.getElementsByTagName(__databaseType);
                for (int __loop2 = 0; __loop2 < __database.Count; __loop2++) {
                    Element __scriptElement = (Element) __database.item(__loop2);
                    NodeList __scriptList = __scriptElement.getElementsByTagName("script");
                    for (int __loop3 = 0; __loop3 < __scriptList.Count; __loop3++) {
                        Element __scriptElement2 = (Element) __scriptList.item(__loop3);
                        NodeList __script2 = __scriptElement2.getChildNodes();
                        String __queryStr = __script2.item(0).getNodeValue();
                        try {
                            Statement __stmt = __con.createStatement();
                            __stmt.executeUpdate(__queryStr);
                            __stmt.close();
                        } catch (Exception __e) {
                        }
                    }
                }
            }       
            __con.close();*/
        } catch (Exception __e) {
        }
    }

    /**
     * ตรวจสอบ Database ถ้าไม่มีก็สร้างให้ใหม่ด้วย
     *
     * @param configFileName
     * @param databaseGroup
     * @param databaseName
     * @param selectTableName
     * @param structFileName
     * @param createAuto
     * @return
     */
    private String _verifyDatabase(String configFileName, String databaseGroup, String databaseName, String selectTableName, String structFileName, Boolean createAuto) {
        if (createAuto) {
            // Connect ถ้าผ่านแสดงว่ามี database แล้ว
            /*switch (this._databaseID) {
                case 1:
                    // PostgreSQL
                    try {
                        Connection __con = _connect(databaseName);
                        __con.close();
                    } catch (Exception __ex) {
                        try {
                            // ถ้าไม่มีก็สร้างซะ และต้องใช้ Connect ที่สร้างใหม่
                            // เพราะยังไม่มีชื่อ database
                            Connection __con = _connect("");
                            // Get a Statement object
                            Statement __stmt = __con.createStatement();
                            // Create the new database
                            __stmt.executeUpdate("CREATE DATABASE " + databaseName + ";");
                            
                            // toe verifyScript Bytea For Postgresql 9.0 better
                            Statement __stmtVersion = __con.createStatement(DataTable.TYPE_SCROLL_INSENSITIVE, DataTable.CONCUR_READ_ONLY);
                
                            // check version ก่อน
                            DataTable _result = __stmtVersion.executeQuery("SELECT version(); ");
                            _result.last();
                            _result.first();
                            String __versionStr = _result.getString(1) ;
                            
                            if (__versionStr.indexOf("PostgreSQL 9") != -1) { // ถ้าเป็น version 9 อัด bytea เข้าไปเลย
                                __stmt.executeUpdate("ALTER DATABASE " + databaseName + " SET bytea_output TO 'escape'; ");
                            }

                            
                            __stmt.close();
                            __con.close();
                        } catch (Exception exx) {
                            return exx.Message;
                        }
                    }
                    break;
                case 2:
                    // MySQL
                    try {
                        Connection __con = _connect(databaseName);
                        __con.close();
                    } catch (Exception __ex) {
                        try {
                            // ถ้าไม่มีก็สร้างซะ และต้องใช้ Connect ที่สร้างใหม่
                            // เพราะยังไม่มีชื่อ database
                            Connection __con = _connect("");
                            // Get a Statement object
                            Statement __stmt = __con.createStatement();
                            // Create the new database
                            __stmt.executeUpdate("CREATE DATABASE " + databaseName + ";");
                            __stmt.close();
                            __con.close();
                        } catch (Exception __exx) {
                            return __exx.Message;
                        }
                    }
                    break;
                case 3:
                case 4:
                    // Microsoft SQL
                    try {
                        Connection __con = _connect(databaseName);
                        __con.close();
                    } catch (Exception __ex) {
                        // ถ้าไม่มีก็สร้างซะ และต้องใช้ Connect ที่สร้างใหม่
                        // เพราะยังไม่มีชื่อ database
                        try {
                            Connection __con = _connect("");
                            // Get a Statement object
                            Statement __stmt = __con.createStatement();
                            // Create the new database
                            __stmt.executeUpdate("CREATE DATABASE " + databaseName);
                            __stmt.close();
                            __con.close();
                        } catch (Exception __exx) {
                            return __exx.Message;
                        }
                    }
                    break;
            }*/
        }
        // Create Table
        String __bufferStructFileName = _readXmlFile(structFileName);
        Boolean _found = false;
        try {
            /*DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
            Document __doc = __docBuilder.parse(new InputSource(new StringReader(__bufferStructFileName)));
            __doc.getDocumentElement().normalize();
            NodeList _listOfTable = __doc.getElementsByTagName("table");
            for (int __table = 0; __table < _listOfTable.Count && _found == false; __table++) {
                Element __tableElement = (Element) _listOfTable.item(__table);
                String __tableName = __tableElement.getAttribute("name").ToLower();
                if (selectTableName.ToLower().compareTo(__tableName.ToLower()) == 0) {
                    // Field
                    NodeList __readerField = __tableElement.getElementsByTagName("field");
                    // ถ้าไม่มี table จะเปลี่ยน _isNewTable=true
                    String __result = _verifyTable(databaseName, __tableName, __readerField);
                    if (__result.Length > 0) {
                        return __result;
                    }
                    // Index
                    NodeList __readerIndex = __tableElement.getElementsByTagName("index");
                    __result = _verifyIndex(databaseName, __tableName, __readerIndex);
                    if (__result.Length > 0) {
                        return __result;
                    }
                    // เพิ่มข้อมูล (aftercreate)
                    if (_isNewTable) {
                        __readerField = __tableElement.getElementsByTagName("aftercreate");
                        __result = _afterCreate(databaseName, __tableName, __readerField);
                        if (__result.Length > 0) {
                            return __result;
                        }
                    }
                    // after verify
                    __readerField = __tableElement.getElementsByTagName("afterverify");
                    __result = _afterVerify(databaseGroup, databaseName, __tableName, __readerField);
                    if (__result.Length > 0) {
                        return __result;
                    }
                }
            }*/
        } catch (Exception __ex) {
        }
        return "";
    }

    public List<String>_getTableFromDatabase(String databaseName) {
        List<String> __result = new List<String>();
        String[] _tableTypes = {"TABLE", "VIEW"};
        try {
            /*Connection __con = _connect(databaseName);
            DatabaseMetaData __dbmd = __con.getMetaData();
            DataTable _tableNamesRS = __dbmd.getTables(null, null, null, _tableTypes);
            while (_tableNamesRS.next()) {
                String tableName = _tableNamesRS.getString("TABLE_NAME").ToLower();
                __result.add(tableName);
            }
            _tableNamesRS.close();
            __con.close();*/
        } catch (Exception __ex) {
        }
        return __result;
    }

    /**
     * ตรวจสอบ Table ถ้าไม่มีก็สร้างให้ใหม่ด้วย โครงสร้างจาก XML File
     *
     * @param databaseName
     * @param tableName
     * @param readerField
     * @return
     */
    private String _verifyTable(String databaseName, String tableName, XmlNodeList readerField) {
        String __result = "";
        Boolean __createTable = true;
        this._isNewTable = false;
        String[] __tableTypes = {"TABLE"};
        try {
            /*Connection __con = _connect(databaseName);
            DatabaseMetaData __dbmd = __con.getMetaData();
            DataTable __tableNamesRS = __dbmd.getTables(null, null, tableName, __tableTypes);
            String __getTableName = "";
            while (__tableNamesRS.next()) {
                __getTableName = __tableNamesRS.getString("TABLE_NAME").ToLower();
            }
            if (__getTableName.ToLower().compareTo(tableName) == 0) {
                __createTable = false;
            }
            __tableNamesRS.close();
            __con.close();*/
        } catch (Exception __ex) {
        }
        // ตรวจสอบ table พร้อม field และทำตามขบวนการสร้าง, แก้ไข, เพิ่ม
        if (__createTable) {
            // ไม่พบ และทำการสร้าง table
            __result = _createTable(databaseName, tableName, readerField);
            _isNewTable = true;
        } else {
            // ถ้าพบ ให้ตรวจสอบ Field ต่อไป
            __result = _verifyField(databaseName, tableName, readerField);
        }
        return __result;
    }

    private String _fieldTypeName(String fieldTypeName) {
        String __getType = fieldTypeName.ToLower();
        if (fieldTypeName.Equals("float")) {
            switch (_databaseID) {
                case 1:
                    // PostgreSQL
                    __getType = "numeric";
                    break;
                case 2:
                    // MySQL
                    __getType = "float";
                    break;
            }
        } else if (fieldTypeName.Equals("smalldatetime")) {
            switch (_databaseID) {
                case 1:
                    // PostgreSQL
                    __getType = "date";
                    break;
                case 2:
                    // MySQL
                    __getType = "datetime";
                    break;
            }
        } else if (fieldTypeName.Equals("varchar")) {
            switch (_databaseID) {
                case 1:
                    // PostgreSQL
                    __getType = "character varying";
                    break;
            }
        } else if (fieldTypeName.Equals("tinyint")) {
            switch (_databaseID) {
                case 1:
                case 2:
                case 3:
                case 4:
                    // PostgreSQL
                    __getType = "smallint";
                    break;
            }
        } else if (fieldTypeName.Equals("datetime")) {
            switch (_databaseID) {
                case 1:
                    // PostgreSQL
                    __getType = "timestamp without time zone";
                    break;
            }
        } else if (fieldTypeName.Equals("image")) {
            switch (_databaseID) {
                case 1:
                    // PostgreSQL
                    // __getType = "oid";
                    __getType = "bytea";
                    break;
                case 2:
                    // MySQL
                    __getType = "longblob";
                    break;
            }
            //MOO
        } else if (fieldTypeName.Equals("text")) {
            switch (_databaseID) {
                case 1:
                    // PostgreSQL
                    // __getType = "oid";
                    __getType = "text";
                    break;
                case 2:
                    // MySQL
                    __getType = "text";
                    break;
            }

        }
        return __getType;
    }

    /**
     * สร้าง Table ใหม่
     *
     * @param databaseName
     * @param tableName
     * @param readerField
     * @return
     */
    private String _createTable(String databaseName, String tableName, XmlNodeList readerField) {
        // สร้าง Table
        StringBuilder __createQuery = new StringBuilder("");
        __createQuery.Append("create table ").Append(tableName).Append(" (");
        Boolean __create = false;
        for (int __field = -2; __field < readerField.Count; __field++) {
            /*Element __fieldElement = (__field == -1 || __field == -2) ? null : (Element) readerField.item(__field);
            String __getFieldName = (__field == -1) ? "roworder" : ((__field == -2) ? "is_lock_record" : __fieldElement.getAttribute("name").ToLower());
            String __getType = (__field == -1 || __field == -2) ? "int" : _fieldTypeName(__fieldElement.getAttribute("type").ToLower());
            String __getLength = (__field == -1 || __field == -2) ? "0" : __fieldElement.getAttribute("length").ToLower();
            String __getIndentity = (__field == -1) ? "yes" : ((__field == -2) ? "no" : __fieldElement.getAttribute("indentity").ToLower());
            String __getAllowNulls = (__field == -1) ? "false" : ((__field == -2) ? "true" : __fieldElement.getAttribute("allow_null").ToLower());
            Boolean __getResourceOnly = (__field == -1 || __field == -2) ? false : ((__fieldElement.getAttribute("resource_only").ToLower().compareTo("true") == 0) ? true : false);

            if (__getResourceOnly == false) {
                __create = true;
                if (__field != -2) {
                    // กรณีเป็น loop แรก ไม่ต้องใส่คอมม่า
                    __createQuery.Append(",");
                }
                __createQuery.Append(" ").Append(__getFieldName).Append(" ");
                String __oldQuery = __createQuery.ToString();
                __createQuery.Append(__getType);
                // if (__getType.compareTo("int") == 0 || __getType.compareTo("image") == 0 || __getType.compareTo("oid") == 0 || __getType.compareTo("blob") == 0) {
                if (__getType.compareTo("int") == 0 || __getType.compareTo("image") == 0 || __getType.compareTo("bytea") == 0 || __getType.compareTo("longblob") == 0) {
                    __getLength = "0";
                }
                if (__getLength != null) {
                    if (__getLength.compareTo("0") != 0) {
                        __createQuery.Append("(").Append(__getLength).Append(")");
                    }
                }
                if (__getIndentity != null) {
                    if (__getIndentity.ToLower().compareTo("yes") == 0) {
                        switch (this._databaseID) {
                            case 1:
                                // PostgreSql
                                __createQuery = new StringBuilder("");
                                __createQuery.Append(__oldQuery);
                                __createQuery.Append(" serial ");
                                break;
                            case 2:
                                __createQuery.Append(" AUTO_INCREMENT PRIMARY key ");
                                __getAllowNulls = "false";
                                //  __createQuery.Append(" default '1'");//somruk
                                break;
                            case 3:
                            case 4:
                                // Microsoft SQL
                                __createQuery.Append(" IDENTITY (1,1) ");
                                break;
                        }
                    }
                }
                if (__getAllowNulls != null) {
                    if (__getAllowNulls.ToLower().compareTo("false") == 0) {
                        __createQuery.Append(" NOT NULL ");
                    }
                    if (__getAllowNulls.ToLower().compareTo("true") == 0) {
                        __createQuery.Append(" NULL ");
                    }
                } else {
                    __createQuery.Append(" NULL ");
                }
            }*/
        }
        switch (this._databaseID) {
            case 1:
                // PostgreSQL
                __createQuery.Append(") ");
                break;
            case 2:
                // MySQL
                __createQuery.Append(") TYPE=InnoDB CHARACTER SET \'utf8\' COLLATE \'utf8_unicode_ci\';");
                break;
            case 3:
            case 4:
                // Microsoft SQL
                __createQuery.Append(") ON [PRIMARY]");
                break;
        }
        String __result = "";
        if (__create) {
            try {
                /*Connection __con = _connect(databaseName);
                // Get a Statement object
                Statement __stmt = __con.createStatement();
                // Create the new database
                String __getString = __createQuery.ToString();
                __stmt.executeUpdate(__getString);
                __stmt.close();
                __con.close();*/
            } catch (Exception ex) {
                __result = ex.Message + ":" + __createQuery.ToString();
            }
        }
        return __result;
    }

    /**
     * ตรวจสอบ Index ถ้าไม่พบ ก็สร้างให้ใหม่
     *
     * @param databaseName
     * @param tableName
     * @param readerField
     * @return
     */
    private String _verifyIndex(String databaseName, String tableName, XmlNodeList readerField) {
        // ลบ index เดิมทิ้ง แล้วสร้าง index ใหม่ แต่จะทำให้ฉลาดขึ้น
        // โดยดึงขึ้นมาดูว่า index เดิม กับ index ใหม่ เหมือนกันหรือไม่
        // ถ้าไม่เหมือนก็ลบ แล้วสร้างใหม่ ถ้าเหมือนก็ผ่าน จะได้เร็ว
        // สร้าง Table

        String __result = "";
        try {
            /*Connection __con = _connect(databaseName);
            for (int __index = -1; __index < readerField.Count; __index++) {
                StringBuilder __createQuery = new StringBuilder("");
                Element __indexElement = (__index == -1) ? null : (Element) readerField.item(__index);
                String __realIndexName = (__index == -1) ? tableName + "_roworder" : __indexElement.getAttribute("index_name").ToLower();
                String __realFieldName = (__index == -1) ? "roworder" : __indexElement.getAttribute("field").ToLower();
                String __getIndexName = __realIndexName;
                String __getFieldName = __realFieldName;
                String __getCuster = (__index == -1) ? "false" : __indexElement.getAttribute("custer").ToLower();
                String __getUnique = (__index == -1) ? "false" : __indexElement.getAttribute("unique").ToLower();
                String __myIndex = __realIndexName + "_idx";
                String __dropIndex = "";
                __getCuster = (__getCuster == null) ? "" : __getCuster;
                __getUnique = (__getUnique == null) ? "" : __getUnique;
                if (__getUnique.ToLower().compareTo("true") == 0) {
                    __getUnique = "unique";
                }
                Boolean __isCuster = (__getCuster.ToLower().compareTo("true") == 0) ? true : false; // Primaykey ห้ามลบทิ้ง และห้ามสร้างทับ
                // if (_oldIndex == true && _startDropIndex > 1)
                Boolean __oldIndex = _findIndex(databaseName, tableName, __realIndexName, __realFieldName);
                Boolean __oldIndexCluster = _findIndex(databaseName, tableName, __myIndex, __realFieldName);
                if (__oldIndex == false) {
                    //somruk
                    try {
                        Statement __stmt = __con.createStatement();
                        try {
                            if (__isCuster == true) {
                                switch (this._databaseID) {

                                    case 1:
                                        __dropIndex = "drop index " + __realIndexName;
                                        break;
                                    case 2:
                                        // MySQL
                                        __dropIndex = "alter table  " + tableName + "  drop index " + __realIndexName;
                                        break;
                                    case 3:
                                    case 4:
                                        // Microsoft SQL
                                        //  __dropIndex = "ALTER TABLE " + tableName + " DROP CONSTRAINT PK_" + tableName;
                                        __dropIndex = " ALTER TABLE " + tableName + " DROP CONSTRAINT " + __realIndexName;
                                        break;
                                }
                            }
                            if (__isCuster == false && __getUnique.Equals("unique")) {
                                switch (this._databaseID) {
                                    case 1:
                                        // PostgreSQL
                                        __dropIndex = "drop index " + __realIndexName;
                                        break;
                                    case 2:
                                        // MySQL
                                        __dropIndex = "alter table  " + tableName + "  drop index " + __realIndexName;
                                        break;
                                    case 3:
                                    case 4:
                                        // Microsoft SQL
                                        __dropIndex = "drop index " + tableName + "." + __realIndexName;
                                        break;
                                }
                            }
                            // _startDropIndex++;
                        } catch (Exception __ex) {
                            //   __stmt.executeUpdate("ALTER TABLE "+tableName+" DROP CONSTRAINT "+__realIndexName);
                            Logger.getLogger(Routine.class.getName()).log(Level.SEVERE, __ex.Message.ToString(), __ex);
                        }
                        __stmt.executeUpdate(__dropIndex);
                        __stmt.close();
                    } catch (Exception __ex) {
                        Logger.getLogger(Routine.class.getName()).log(Level.SEVERE, __ex.Message.ToString(), __ex);
                    }
                }
                if (__oldIndexCluster == false) {
                    try {
                        Statement __stmt = __con.createStatement();
                        try {
                            if (__isCuster == true) {
                                switch (this._databaseID) {

                                    case 1:
                                        __dropIndex = "drop index " + __myIndex;
                                        break;
                                    case 2:
                                        // MySQL
                                        __dropIndex = "alter table  " + tableName + "  drop index " + __myIndex;
                                        break;
                                    case 3:
                                    case 4:
                                        // Microsoft SQL
                                        __dropIndex = "drop index " + tableName + "." + __realIndexName;
                                        break;
                                }
                            }
                            if (__isCuster == false && __getUnique.Equals("unique")) {
                                switch (this._databaseID) {
                                    case 1:
                                        // PostgreSQL
                                        __dropIndex = "drop index " + __myIndex;
                                        break;
                                    case 2:
                                        // MySQL
                                        __dropIndex = "alter table  " + tableName + "  drop index " + __myIndex;
                                        break;
                                    case 3:
                                    case 4:
                                        // Microsoft SQL
                                        // __stmt.executeUpdate("drop index " + tableName + "." + __realIndexName);
                                        __dropIndex = "drop index " + tableName + "." + __myIndex;
                                        break;
                                }
                            }
                            // _startDropIndex++;
                        } catch (Exception __ex) {
                            __stmt.executeUpdate("ALTER TABLE " + tableName + " DROP CONSTRAINT " + __realIndexName);
                            Logger.getLogger(Routine.class.getName()).log(Level.SEVERE, __ex.Message.ToString(), __ex);
                        }
                        __stmt.executeUpdate(__dropIndex);
                        __stmt.close();
                    } catch (Exception __ex) {
                        Logger.getLogger(Routine.class.getName()).log(Level.SEVERE, __ex.Message.ToString(), __ex);
                    }
                }
                if (__isCuster == true && __oldIndex == false) {
                    switch (this._databaseID) {
                        case 1:
                            // PostgreSQL
                            //__createQuery.Append("alter table " + tableName + " add CONSTRAINT " + __getIndexName + " primary key (" + __getFieldName + "); CREATE INDEX "+__myIndex+" ON "+tableName+" ("+__getFieldName+");");
                            __createQuery.Append("alter table ").Append(tableName).Append(" add CONSTRAINT ").Append(__getIndexName).Append(" primary key (").Append(__getFieldName).Append(");");
                            break;
                        case 2:
                            // MySQL
                            __createQuery.Append("alter table ").Append(tableName).Append(" add ").Append(__getUnique).Append(" ").Append(__getIndexName).Append(" (").Append(__getFieldName).Append(")");
                            break;
                        case 3:
                        case 4:
                            // Microsoft SQL
                            __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" WITH NOCHECK ADD CONSTRAINT ").Append(__getIndexName).Append(" PRIMARY KEY  CLUSTERED (").Append(__getFieldName).Append(")  ON [PRIMARY]");
                            break;
                    }
                }
                if (__isCuster == false && __oldIndex == false && __getUnique.Equals("unique")) {

                    switch (this._databaseID) {
                        case 1:
                            // PostgreSQL
                            //__createQuery.Append(" create " + __getUnique + " index " + __getIndexName + " on " + tableName + " (" + __getFieldName + "),ENGINE = InnoDB;");
                            __createQuery.Append(" create ").Append(__getUnique).Append(" index ").Append(__getIndexName).Append(" on ").Append(tableName).Append(" (").Append(__getFieldName).Append(");");
                            break;
                        case 2:
                            // MySQL
                            __createQuery.Append("alter table ").Append(tableName).Append(" add ").Append(__getUnique).Append(" ").Append(__getIndexName).Append(" (").Append(__getFieldName).Append(")");
                            break;
                        case 3:
                        case 4:
                            // Microsoft SQL
                            __createQuery.Append(" create ").Append(__getUnique).Append(" CLUSTERED index ").Append(__getIndexName).Append(" ON ").Append(tableName).Append(" (").Append(__getFieldName).Append(") ON [PRIMARY]");
                            break;
                    }
                }
                if (__isCuster == true && __oldIndexCluster == false) {
                    switch (this._databaseID) {
                        case 1:
                            // PostgreSQL
                            //__createQuery.Append("alter table " + tableName + " add CONSTRAINT " + __getIndexName + " primary key (" + __getFieldName + "); CREATE INDEX "+__myIndex+" ON "+tableName+" ("+__getFieldName+");");
                            __createQuery.Append(" CREATE INDEX ").Append(__myIndex).Append(" ON ").Append(tableName).Append(" (").Append(__getFieldName).Append(");ALTER TABLE ").Append(tableName).Append(" CLUSTER ON ").Append(__myIndex).Append(";");
                            break;
                        case 2:
                            // MySQL
                            if (__createQuery.ToString().Length > 0) {
                                __createQuery.Append(" , add  index ").Append(__myIndex).Append(" (").Append(__getFieldName).Append(")");
                            } else {
                                __createQuery.Append(" alter table ").Append(tableName).Append(" add  index ").Append(__myIndex).Append(" (").Append(__getFieldName).Append(")");
                            }
                            break;
                        case 3:
                        case 4:
                            // Microsoft SQL
                            // __createQuery.Append("ALTER TABLE " + tableName + " WITH NOCHECK ADD CONSTRAINT " + __getIndexName + " PRIMARY KEY  CLUSTERED (" + __getFieldName + ")  ON [PRIMARY]");
                            break;
                    }
                }
                if (__isCuster == false && __oldIndexCluster == false && __getUnique.Equals("unique")) {
                    switch (this._databaseID) {
                        case 1:
                            // PostgreSQL
                            //__createQuery.Append(" create " + __getUnique + " index " + __getIndexName + " on " + tableName + " (" + __getFieldName + "),ENGINE = InnoDB;");
                            __createQuery.Append(" CREATE INDEX ").Append(__myIndex).Append(" ON ").Append(tableName).Append(" (").Append(__getFieldName).Append(");ALTER TABLE ").Append(tableName).Append(" CLUSTER ON ").Append(__myIndex).Append(";");
                            break;
                        case 2:
                            // MySQL
                            if (__createQuery.ToString().Length > 0) {
                                __createQuery.Append(" , add  index ").Append(__myIndex).Append(" (").Append(__getFieldName).Append(")");
                            } else {
                                __createQuery.Append(" alter table ").Append(tableName).Append(" add  index ").Append(__myIndex).Append(" (").Append(__getFieldName).Append(")");
                            }

                            break;
                        case 3:
                        case 4:
                            // Microsoft SQL

                            // __createQuery.Append(" create " + __getUnique + " CLUSTERED index " + __getIndexName + " ON " + tableName + " (" + __getFieldName + ") ON [PRIMARY]");
                            break;
                    }
                }
                if (__isCuster == false && __oldIndexCluster == false && __getUnique.Equals("unique") == false) {
                    switch (this._databaseID) {
                        case 1:
                            // PostgreSQL
                            //__createQuery.Append(" create " + __getUnique + " index " + __getIndexName + " on " + tableName + " (" + __getFieldName + "),ENGINE = InnoDB;");
                            __createQuery.Append(" CREATE INDEX ").Append(__myIndex).Append(" ON ").Append(tableName).Append(" (").Append(__getFieldName).Append(");");
                            break;
                        case 2:
                            // MySQL
                            if (__createQuery.ToString().Length > 0) {
                                __createQuery.Append(" , add  index ").Append(__myIndex).Append(" (").Append(__getFieldName).Append(")");
                            } else {
                                __createQuery.Append(" alter table ").Append(tableName).Append(" add  index ").Append(__myIndex).Append(" (").Append(__getFieldName).Append(")");
                            }

                            break;
                        case 3:
                        case 4:
                            // Microsoft SQL

                            // __createQuery.Append(" create " + __getUnique + " CLUSTERED index " + __getIndexName + " ON " + tableName + " (" + __getFieldName + ") ON [PRIMARY]");
                            break;
                    }
                }
                if (__createQuery.ToString().Length > 0) {
                    try {
                        // Get a Statement object
                        Statement __stmt = __con.createStatement();
                        __stmt.execute(__createQuery.ToString());
                        __stmt.close();
                        // _newIndex++;
                    } catch (Exception __ex) {
                        __result = __ex.Message.ToString() + ":" + __createQuery.ToString();
                    }
                }

            }
            __con.close();
             * */
        } catch (Exception __ex) {
            //Logger.getLogger(Routine.class.getName()).log(Level.SEVERE, __ex.Message.ToString(), __ex);
        }
        return __result;
    }

    /**
     * ตรวจสอบ Index
     *
     * @param databaseName
     * @param tableName
     * @param indexName
     * @param realFieldName
     * @return
     */
    private Boolean _findIndex(String databaseName, String tableName, String indexName, String realFieldName) {
        Boolean __result = false;
        // ในกรณี index ที่มีมากกว่า 1 field โปรแกรมจะ pack ให้โดยอัตโนมัติ
        // ใช้ในการเปรียบเทียบว่าเป็น index แบบเดิมหรือแบบใหม่
        Boolean __foundFirst = false; // พบ index ครั้งแรก
        StringBuilder __packFieldName = new StringBuilder(); // เอาไว้ต่อ index หลาย field เป็น string
        // เดียว
        try {
            /*Connection __con = _connect(databaseName);
            DatabaseMetaData __dbmd = __con.getMetaData();
            String[] __tableTypes = {"TABLE"};
            DataTable __tableNamesRS = __dbmd.getTables(null, null, tableName, __tableTypes);
            String __getTableName = "";
            while (__tableNamesRS.next()) {
                __getTableName = __tableNamesRS.getString("TABLE_NAME").ToLower();
            }
            __tableNamesRS.close();
            DataTable __indexNameRS = __dbmd.getIndexInfo(__con.getCatalog(), null, tableName, false, true);
            if (this._databaseID == 3 || this._databaseID == 4) {
                // Microsoft SQL
                __indexNameRS.next();
            }
            while (__indexNameRS.next()) {
                String __getIndexName = __indexNameRS.getString("INDEX_NAME");
                String __getFieldName = __indexNameRS.getString("COLUMN_NAME");
                if (__getTableName.compareTo(tableName.ToLower()) == 0 && __getIndexName.ToLower().compareTo(indexName.ToLower()) == 0) {
                    if (__foundFirst) {
                        __packFieldName.Append(",");
                    }

                    __packFieldName.Append(__getFieldName);
                    __foundFirst = true;
                    // _startDropIndex++;
                } else {
                    if (__foundFirst) {
                        // ในกรณีที่เจอแล้ว ไม่ต้องการหาต่อไป ให้หยุด
                        break;
                    }
                }
            }
            __indexNameRS.close();
            __result = (realFieldName.ToLower().compareTo(__packFieldName.ToString().ToLower()) == 0) ? true : false;
            __con.close();*/
        } catch (Exception __ex) {
        }

        return __result;
    }

    /**
     * กรณีสร้าง Table ใหม่ ต้องการให้ทำอะไรสามารถกำหนดได้ใน XML File
     *
     * @param databaseName
     * @param tableName
     * @param readerField
     * @return
     */
    private String _afterCreate(String databaseName, String tableName, XmlNodeList readerField) {
        // ในกรณีสร้าง table ใหม่ คำสั่ง SQL
        String __result = "";
        for (int __field = 0; __field < readerField.Count; __field++) {
            /*Element __fieldElement = (Element) readerField.item(__field);
            String __query = __fieldElement.getTextContent();
            try {
                Connection __con = _connect(databaseName);
                // Get a Statement object
                Statement __stmt = __con.createStatement();
                __stmt.executeUpdate(__query);
                __stmt.close();
                __con.close();
            } catch (Exception __ex) {
                __result = __query;
            }*/
        }
        return __result;
    }

    private String _processQueryString(String databaseGroup, String query) {
        String __result = query.Replace("_data_group_", "\'" + databaseGroup + "\'");
        return __result;
    }

    /**
     * หลังจาก Vertify Table ต้องการให้ทำอะไรบ้าง กำหนดได้ใน XML File
     *
     * @param databaseName
     * @param tableName
     * @param readerField
     * @return
     */
    private String _afterVerify(String databaseGroup, String databaseName, String tableName, XmlNodeList readerField) {
        // กรณีสร้าง Table ใหม่ หรือ Verify Table
        String __result = "";
        for (int __field = 0; __field < readerField.Count; __field++) {
            /*Element __fieldElement = (Element) readerField.item(__field);
            String __getXml = __fieldElement.getTextContent();
            String __query = _processQueryString(databaseGroup, __getXml);
            try {
                Connection __con = _connect(databaseName);
                // Get a Statement object
                Statement __stmt = __con.createStatement();
                __stmt.executeUpdate(__query);
                __stmt.close();
                __con.close();
            } catch (Exception __ex) {
                __result = "";
            }*/
        }

        return __result;
    }

    /**
     * ตรวจสอบว่ามี Field เพิ่มหรือไม่ ฉลาดขึ้นในกรณีที่ความยาว Field ของ
     * Database น้อยกว่าของใหม่ โปรแกรมจะปรับให้มากขึ้นโดยอัตโนมัติ
     *
     * @param databaseName
     * @param tableName
     * @param readerField
     * @return
     */
    private String _verifyField(String databaseName, String tableName, XmlNodeList readerField) {
        StringBuilder __createQuery = new StringBuilder();
        String __result = "";
        try {
            /*Connection __con = _connect(databaseName);
            for (int __field = -2; __field < readerField.Count; __field++) {
                __createQuery = new StringBuilder();
                Element __fieldElement = (__field == -1 || __field == -2) ? null : (Element) readerField.item(__field);
                String __getRealFieldName = (__field == -1) ? "roworder" : ((__field == -2) ? "is_lock_record" : __fieldElement.getAttribute("name").ToLower().trim());
                String __getTypeReal = (__field == -1 || __field == -2) ? "int" : __fieldElement.getAttribute("type").ToLower().trim();
                String __getType = (__field == -1 || __field == -2) ? "int" : _fieldTypeName(__getTypeReal);
                String __getLength = (__field == -1 || __field == -2) ? "0" : __fieldElement.getAttribute("length").ToLower().trim();
                String __getIndentity = (__field == -1) ? "yes" : ((__field == -2) ? "no" : __fieldElement.getAttribute("indentity").ToLower().trim());
                String __getAllowNulls = (__field == -1) ? "false" : ((__field == -2) ? "true" : __fieldElement.getAttribute("allow_null").ToLower().trim());
                Boolean __getResourceOnly = (__field == -1 || __field == -2) ? false : ((__fieldElement.getAttribute("resource_only").ToLower().compareTo("true") == 0) ? true : false);
                if (__getResourceOnly == false) {
                    if (__getTypeReal.compareTo("int") == 0) {
                        __getLength = "10";
                        __getType = "int";
                        // _getType = "int identity";
                    }
                    if (__getAllowNulls == null) {
                        __getAllowNulls = "true";
                    }
                    if (__getAllowNulls.compareTo("false") == 0) {
                        __getAllowNulls = " not null default ''";
                    }
                    if (__getAllowNulls.compareTo("true") == 0) {
                        __getAllowNulls = " null ";
                    }
                    if (__getRealFieldName.Length > 0) {
                        // 0=มี Field,1=ไม่มี Field,2=ให้แก้ไขความกว้าง,3=type ผิด
                        int __fieldStatus = _findField(__con, tableName, __getRealFieldName, __getType, Integer.parseInt(__getLength));
                        // สร้าง type
                        String __createType = __getType;
                        if (__getTypeReal.compareTo("varchar") == 0) {
                            __createType = __getType + " (" + __getLength + ")";
                        }
                        switch (__fieldStatus) {
                            case 1:// ไม่มี field ให้สร้างใหม่เลย
                            {
                                // UNIQUE";
                                if (__getIndentity != null) {
                                    // กรณีพิเศษ เป็นแบบ Autorun
                                    if (__getIndentity.ToLower().compareTo("yes") == 0) {
                                        switch (this._databaseID) {
                                            case 1:
                                                // PostgreSql
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName); // + " CONSTRAINT exb_unique
                                                __createQuery.Append(" serial ");
                                                break;
                                            case 2:
                                                //MySQL
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName).Append(" ").Append(__createType).Append(" ").Append(__getAllowNulls); // + " CONSTRAINT exb_unique
                                                __createQuery.Append(" AUTO_INCREMENT PRIMARY key ");//somruk
                                                __getAllowNulls = "false";
                                                break;
                                            case 3:
                                            case 4:
                                                // Microsoft SQL
                                                __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName).Append(" ").Append(__createType).Append(" ").Append(__getAllowNulls); // + " CONSTRAINT exb_unique
                                                __createQuery.Append(" IDENTITY (1,1) ");
                                                break;
                                        }
                                    }
                                }
                                if (__createQuery.ToString().trim().Length == 0) {
                                    __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ADD ").Append(__getRealFieldName).Append(" ").Append(__createType).Append(" ").Append(__getAllowNulls); // + " CONSTRAINT exb_unique
                                }
                            }
                            break;
                            case 2: //ความยาวไม่ตรง
                            {
                                switch (this._databaseID) {
                                    case 1:
                                        // PostgreSql
                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER ").Append(__getRealFieldName).Append(" type ").Append(__createType);
                                        break;
                                    case 2:
                                        //MySQL
                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER ").Append(__getRealFieldName).Append(" type ").Append(__createType);
                                        break;
                                    case 3:
                                    case 4:
                                        // Microsoft SQL
                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append("  ").Append(__createType);
                                        break;
                                }
                            }
                            break;
                            case 3: //Type
                            {
                                switch (this._databaseID) {
                                    case 1:
                                        // PostgreSql
                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER ").Append(__getRealFieldName).Append(" type ").Append(__createType);
                                        break;
                                    case 2:
                                        //MySQL
                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER ").Append(__getRealFieldName).Append(" type ").Append(__createType);
                                        break;
                                    case 3:
                                    case 4:
                                        // Microsoft SQL
                                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" ALTER column ").Append(__getRealFieldName).Append(" ").Append(__createType);
                                        break;
                                }
                            }
                            break;
                        }
                        try {
                            // Get a Statement object
                            String __queryStr = __createQuery.ToString();
                            if (__queryStr.Length > 0) {
                                Statement __stmt = __con.createStatement();
                                __stmt.executeUpdate(__queryStr);
                                __stmt.close();
                            }
                        } catch (Exception __ex) {
                            __result = __ex.Message.ToString() + ":" + __createQuery;
                        }
                    }
                }
                // ประเภทผิด, ความยาวไม่ตรง จะต้องทำการสร้าง Database ใหม่ แล้ว Copy
                // ข้อมูลไป วันหลังจะทำ
            }
            __con.close();*/
        } catch (Exception __ex) {
        }
        return __result;
    }

    public ArrayList _getFieldFromDatabase(String databaseName, String tableName) {
        ArrayList __result = new ArrayList();
        try {
            /*Connection __con = _connect(databaseName);
            DatabaseMetaData __dbmd = __con.getMetaData();
            DataTable __columnRS = __dbmd.getColumns(null, null, tableName, null);
            while (__columnRS.next()) {
                String __getFieldName = __columnRS.getString("COLUMN_NAME");
                String __getFieldType = __columnRS.getString("TYPE_NAME");
                __result.add(__getFieldName + "," + __getFieldType);
            } // while
            __columnRS.close();
            __con.close();*/
        } catch (Exception __ex) {
        }
        return __result;
    }

    public String _reconvertFieldType(String fieldType) {
        switch (this._databaseID) {
            case 1:
                if (fieldType.ToLower().Equals("int4")) {
                    return "int";
                }
                if (fieldType.ToLower().Equals("int2")) {
                    return "smallint";
                }
                if (fieldType.ToLower().Equals("varchar")) {
                    return "character varying";
                }
                if (fieldType.ToLower().Equals("timestamp")) {
                    return "timestamp without time zone";
                }
                break;
            case 2:
                //MySQL
                break;
            case 3:
            case 4:
                // Microsoft SQL
                break;

        }
        return fieldType;
    }

    /**
     * ในกรณี index ที่มีมากกว่า 1 field โปรแกรมจะ pack ให้โดยอัตโนมัติ
     * ใช้ในการเปรียบเทียบว่าเป็น index แบบเดิมหรือแบบใหม่
     *
     * @param databaseName
     * @param tableName
     * @param fieldName
     * @param fieldType
     * @param fieldLength
     * @return
     */
//private
    public int _findField(object __con, String tableName, String fieldName, String fieldType, int fieldLength) {
        int __result = 1; // ไม่มี Field ไว้ก่อน
        try {
            /*DatabaseMetaData __dbmd = __con.getMetaData();
            DataTable __columnRS = __dbmd.getColumns(null, null, tableName, null);
            while (__columnRS.next()) {
                String __getFieldName = __columnRS.getString("COLUMN_NAME");
                String __getFieldType = _reconvertFieldType(__columnRS.getString("TYPE_NAME").trim());
                int __getLength = __columnRS.getInt("COLUMN_SIZE");
                if (__getFieldName.compareTo(fieldName.ToLower()) == 0) {
                    __result = 0; // มี Field
                    if (__getFieldName.equalsIgnoreCase("roworder") == false) {
                        switch (this._databaseID) {
                            case 1:
                                // PostgreSql
                                if (__getFieldType.equalsIgnoreCase(fieldType) == false) {
                                    __result = 3; // ประเภทผิด
                                } else if (__getFieldType.Equals("varchar") && __getLength != fieldLength) {
                                    __result = 2; // ความยาวไม่ได้
                                }

                                break;
                            case 2:
                                //MySQL
                                if (__getFieldType.compareTo(fieldType.ToLower()) != 0) {
                                    __result = 3; // ประเภทผิด
                                } else if (__getFieldType.Equals("varchar") && __getLength != fieldLength) {
                                    __result = 2; // ความยาวไม่ได้
                                }

                                break;
                            case 3:
                            case 4:
                                // Microsoft SQL
                                if (__getFieldType.compareTo(fieldType.ToLower()) != 0) {
                                    __result = 3; // ประเภทผิด
                                } else if (__getFieldType.Equals("varchar") && __getLength != fieldLength) {
                                    __result = 2; // ความยาวไม่ได้
                                }

                                break;
                        }

                    }
                    break;
                }

            } // while
            __columnRS.close();*/
        } catch (Exception __ex) {
        }
        return __result;
    }
//test Edit

    public byte[] _queryGetStream(String GUID, String reportGuid, int blockSize, int skip, int mode) {
        if (_guidReady(GUID)) {
            try {
                /*String __fileName = System.getProperty("java.io.tmpdir") + "/" + reportGuid + ((mode == 0) ? ".gzip" : ".zip");
                File __file = new File(__fileName);
                FileInputStream __fileInput = new FileInputStream(__file);
                DataInputStream __data = new DataInputStream(__fileInput);
                long __fileLength = __file.Length;
                //byte[] __buffer = new byte[(int)__fileLength];
                byte[] __buffer = new byte[blockSize];
                long __offset = (long) (skip * blockSize);
                //__pos = 1;
                __data.skip(__offset);
                int __length = blockSize;
                if ((int) __offset + __length > __fileLength) {
                    __length = (int) (__fileLength - __offset);
                    __buffer =
                            new byte[__length];
                }

                __data.read(__buffer, 0, __length);
                __data.close();
                return __buffer;*/
            } catch (Exception __ex) {
                Console.Out.WriteLine("_queryGetStream:" + __ex.Message.ToString());
                return null;
            }

        }
        return null;
    }

    public String _queryStream(String GUID, String reportGuid, String databaseName, String query, int mode)  {
        // ถ้า userCode, userPassword == '*' ผ่านตลอด
        String __size = "0";
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        /*Connection __conn = _connect(databaseName);
        if (_guidReady(GUID)) {
            try {
                // ไล่ลบ temp ที่เก่าเกิน 2 วัน
                File __dir = new File(System.getProperty("java.io.tmpdir"));
                File[] __list = __dir.listFiles();
                for (int i = 0; i < __list.length; i++) {
                    String __fileName = __list[i].getName().ToLower();
                    if (__fileName.endsWith(".gzip") || __fileName.endsWith(".zip")) {
                        Date __lastModified = new Date(__list[i].lastModified());
                        Date __now = new Date();
                        int __calcDate = (int) ((__now.getTime() - __lastModified.getTime()) / (24 * 60 * 60 * 1000));
                        if (__calcDate >= 2) {
                            __list[i].delete();
                        }

                    }
                }
            } catch (Exception __ex) {
                Console.Out.WriteLine("_queryStream:" + __ex.Message.ToString());
            }

            try {
                Statement __stmt = __conn.createStatement(DataTable.TYPE_FORWARD_ONLY, DataTable.CONCUR_READ_ONLY);
                DataTable __rs = __stmt.executeQuery(query);
                String __fileName = System.getProperty("java.io.tmpdir") + "/" + reportGuid;
                String __fileNameXml = __fileName + ".xml";
                String __fileNameZip = __fileName + ((mode == 0) ? ".gzip" : ".zip");
                String __result = _DataTableToXml(__rs, -1, -1, true, __fileNameXml);
                __rs.close();
                __stmt.close();
                //
                _makeGZip(__fileNameXml, __fileNameZip, mode);
                if (__result.Equals("1")) {
                    File __fileSize = new File(__fileNameZip);
                    __size =
                            Long.ToString(__fileSize.Length);
                }

                File __fileDelete = new File(__fileNameXml);
                __fileDelete.delete();
            } catch (Exception __ex) {
                Console.Out.WriteLine("_queryStream:" + __ex.Message.ToString());
                return __ex.Message.ToString() + ":" + query;
            }

        }
        __conn.close();*/
        return __size;
    }

    public String _createStream(String GUID, String reportGuid, String databaseName, String source)  {
        // ถ้า userCode, userPassword == '*' ผ่านตลอด
        String __size = "0";
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        try {
            // ไล่ลบ temp ที่เก่าเกิน 2 วัน
            /*File __dir = new File(System.getProperty("java.io.tmpdir"));
            File[] __list = __dir.listFiles();
            for (int i = 0; i < __list.length; i++) {
                String __fileName = __list[i].getName().ToLower();
                if (__fileName.endsWith(".gzip") || __fileName.endsWith(".zip")) {
                    Date __lastModified = new Date(__list[i].lastModified());
                    Date __now = new Date();
                    int __calcDate = (int) ((__now.getTime() - __lastModified.getTime()) / (24 * 60 * 60 * 1000));
                    if (__calcDate >= 2) {
                        __list[i].delete();
                    }

                }
            }*/
        } catch (Exception __ex) {
            Console.Out.WriteLine("_createStream:" + __ex.Message.ToString());
        }

        try {
            /*String __fileName = System.getProperty("java.io.tmpdir") + "/" + reportGuid;
            String __fileNameXml = __fileName + ".xml";
            String __fileNameZip = __fileName + ".gzip";
            String __result = _createStremToXml(source, __fileNameXml);
            //
            _makeGZip(__fileNameXml, __fileNameZip, 0);
            if (__result.Equals("1")) {
                File __fileSize = new File(__fileNameZip);
                __size =
                        Long.ToString(__fileSize.Length);
            }

            File __fileDelete = new File(__fileNameXml);
            __fileDelete.delete();*/
        } catch (Exception __ex) {
            Console.Out.WriteLine("_createStream:" + __ex.Message.ToString());
            return __ex.Message.ToString();
        }

        return __size;
    }

    public String _createStremToXml(String source, String fileName)  {
        StringBuilder __myXml = new StringBuilder();
        try {
            /*Writer __dataOut = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(fileName), "UTF8"));
            __dataOut.write(source);
            __dataOut.flush();
            __dataOut.close();*/
            return "1";
        } catch (Exception __ex) {
            Console.Out.WriteLine("_createStremToXml:" + __ex.Message.ToString());
        }

        return __myXml.ToString();
    }

    public String _queryColumnName(            String GUID, String databaseName, String query)  {
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        StringBuilder __result = new StringBuilder();
        /*Connection __conn = _connect(databaseName);
        if (_guidReady(GUID)) {
            try {
                Statement __stmt = __conn.createStatement(DataTable.TYPE_FORWARD_ONLY, DataTable.CONCUR_READ_ONLY);
                DataTable __rs = __stmt.executeQuery(query);
                DataTableMetaData __rsmd = __rs.getMetaData();
                int __colCount = __rsmd.getColumnCount();
                for (int __i = 1; __i <= __colCount; __i++) {
                    String __columnName = __rsmd.getColumnName(__i);
                    if (__result.Length > 0) {
                        __result.Append(",");
                    }

                    __result.Append(__columnName);
                }

            } catch (Exception __ex) {
                Console.Out.WriteLine("_query:" + __ex.Message.ToString() + ":" + query);
                return __ex.Message.ToString() + ":" + query;
            }

        }
        __conn.close();*/
        return __result.ToString();
    }

    public String _query(            String GUID, String databaseName, String query) {
        // ถ้า userCode, userPassword == '*' ผ่านตลอด
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        StringBuilder __result = new StringBuilder();
        /*Connection __conn = _connect(databaseName);
        if (_guidReady(GUID)) {
            __result.Append(_xmlTagHead);
            try {
                Statement __stmt = __conn.createStatement(DataTable.TYPE_FORWARD_ONLY, DataTable.CONCUR_READ_ONLY);
                DataTable __rs = __stmt.executeQuery(query);
                __result.Append("<DataTable>");
                __result.Append(_DataTableToXml(__rs, -1, -1, false, ""));
                __result.Append("</DataTable>");
                __rs.close();
                __stmt.close();
            } catch (Exception __ex) {
                Console.Out.WriteLine("_query:" + __ex.Message.ToString() + ":" + query);
                return __ex.Message.ToString() + ":" + query;
            }

        }
        __conn.close();
         * */
        return __result.ToString();
    }

//private
    public String _DataTableToXml(DataTable rs, int startRecord, int maxRecords, Boolean toFile, String fileName)  {
        StringBuilder __myXml = new StringBuilder();
        Boolean __getLimit = (startRecord != -1 && maxRecords != -1) ? true : false;
        /*if (__getLimit) {
            switch (this._databaseID) {
                case 3:
                    // SQL 2000 only
                    for (int __loop = 0; __loop < startRecord; __loop++) {
                        rs.next();
                    }

                    break;
                case 4:
                    // Sql 2005
                    rs.absolute(startRecord);
                    break;
            }
        }
        DataTableMetaData __rsmd = rs.getMetaData();
        int __colCount = __rsmd.getColumnCount();
        int __count = 0;
        Writer __dataOut = null;
        try {
            if (toFile) {
                __dataOut = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(fileName), "UTF8"));
                __dataOut.write(_xmlTagHead);
                __dataOut.write("<DataTable>");
            }

            while (rs.next()) {
                if (toFile) {
                    __dataOut.write("<Row>");
                } else {
                    __myXml.Append("<Row>");
                }

                for (int __i = 1; __i <= __colCount; __i++) {
                    String __columnName = __rsmd.getColumnName(__i);
                    int __getColumnType = __rsmd.getColumnType(__i);
                    if (__getColumnType == java.sql.Types.DOUBLE) {
                        Object __value = rs.getBigDecimal(__i);
                        String __data = _convertTextToXml(__columnName, (__value == null) ? "" : __value.ToString());
                        if (toFile) {
                            __dataOut.write(__data);
                        } else {
                            __myXml.Append(__data);
                        }

                    } else {
                        Object __value = rs.getObject(__i);
                        String __data = _convertTextToXml(__columnName, (__value == null) ? "" : __value.ToString());
                        if (toFile) {
                            __dataOut.write(__data);
                        } else {
                            __myXml.Append(__data);
                        }

                    }
                }
                if (toFile) {
                    __dataOut.write("</Row>");
                } else {
                    __myXml.Append("</Row>");
                }

                if (__getLimit) {
                    __count++;
                    if (__count == maxRecords) {
                        break;
                    }

                }
            }
            if (toFile) {
                __dataOut.write("</DataTable>");
                __dataOut.flush();
                __dataOut.close();
                return "1";
            }

        } catch (Exception __ex) {
            Console.Out.WriteLine("_DataTableToXml:" + __ex.Message.ToString());
        }
        */
        return __myXml.ToString();
    }

    public String _queryLimit(String GUID, String databaseName, String queryForCount, String query, int startRecord, int maxRecords, int countTotalRecord) {
        // ถ้า userCode, userPassword == '*' ผ่านตลอด
        String __query = query;
        // somruk edit 11:28 9/6/2552    เพิ่ม countTotalRecord
        StringBuilder __result = new StringBuilder();
        String __counterrecord = "rowcount";
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        if (_guidReady(GUID)) {
            /*Connection __conn = _connect(databaseName);
            int __totalRecord = 0;
            if (countTotalRecord == 1) {

                switch (this._databaseID) {
                    case 4:
                        // PostgreSql
                        __counterrecord = "rowcounter";
                        queryForCount =
                                queryForCount.Replace("rowcount", "rowcounter");
                        break;
                }
                Statement __stmtCount = __conn.createStatement(DataTable.TYPE_SCROLL_INSENSITIVE, DataTable.CONCUR_READ_ONLY);
                DataTable __rsCount = __stmtCount.executeQuery(queryForCount);
                __rsCount.next();
                __totalRecord =
                        __rsCount.getInt(__counterrecord);
                __rsCount.close();
                __stmtCount.close();
                //MOO เอาออก
                //----------------------------------
                //}
                //----------------------------------

            }

            if (startRecord != -1 && maxRecords != -1) {
                switch (this._databaseID) {
                    case 1:
                        // PostgreSql
                        __query = __query + " offset " + startRecord + " limit " + maxRecords;
                        break;

                    case 2:
                        // MySql
                        __query = __query + " limit " + startRecord + "," + maxRecords;
                        break;

                }




            }
            Statement __stmt = __conn.createStatement(DataTable.TYPE_SCROLL_INSENSITIVE, DataTable.CONCUR_READ_ONLY);
            DataTable __rs = __stmt.executeQuery(__query);
            __result.Append(String.valueOf(__totalRecord)).Append(",");
            __result.Append("<DataTable>");
            __result.Append(_DataTableToXml(__rs, startRecord, maxRecords, false, ""));
            __result.Append("</DataTable>");

            __rs.close();
            __stmt.close();
            __conn.close();*/
        }

        return __result.ToString();
    }

    public int _countTotalRecord(DataTable rs)  {
        int __count = 0;
        /*rs.last();
        __count =
                rs.getRow();
        rs.beforeFirst();*/
        return __count;
    }

    public String _queryUpdateTracking(String databaseName, String query) {
        /*try {
            String __dbClassName = null;
            StringBuilder __dbConnectUrl = new StringBuilder();

            __dbClassName =
                    "org.postgresql.Driver";
            __dbConnectUrl.Append("jdbc:postgresql");
            __dbConnectUrl.Append("://").Append("localhost").Append(":").Append("5432");
            if (databaseName.Length != 0) {
                __dbConnectUrl.Append("/").Append(databaseName.ToLower());
            }

            Class.forName(__dbClassName).newInstance();
            Connection __conn = DriverManager.getConnection(__dbConnectUrl.ToString(), "postgres", "1");
            Statement __stmt = __conn.createStatement();
            __stmt.execute(query);
            __stmt.close();
            __conn.close();
        } catch (Exception __ex) {
        }*/
        return "";
    }

    public String _queryInsertOrUpdateNoGuid(String databaseName, String query)  {
        /*try {
            Connection __conn = _connect(databaseName);
            Statement __stmt = __conn.createStatement();
            __stmt.execute(query);
            __stmt.close();
            __conn.close();
        } catch (Exception __ex) {
            Console.Out.WriteLine("_queryInsertOrUpdateNoGuid:" + __ex.Message.ToString());
            return __ex.Message.ToString() + ":" + query;
        }
        */
        return "";
    }

    public String _queryByteData(String GUID, String configFileName, String databaseName, String query, Object[] byteValue) {

        StringBuilder __result = new StringBuilder();

        if (_guidReady(GUID)) {
            /*try {
                Connection __conn = _connect(databaseName);
                java.sql.PreparedStatement __pstmt = __conn.prepareStatement(query);                

                for (int __i = 0; __i < byteValue.length; __i++){
                    if (byteValue[__i].getClass().isArray()){
                        __pstmt.setBytes(__i+1,(byte[]) byteValue[__i]);
                    }
                    else {
                        __pstmt.setString(__i+1, (String) byteValue[__i]);
                    }
                }                
                __pstmt.executeUpdate();
                __pstmt.close();
            }
            catch (Exception __ex) {
                Console.Out.WriteLine("_query:" + __ex.Message.ToString() + ":" + query);
                return __ex.Message.ToString() + ":" + query;
            }*/
        }        
        return __result.ToString();
    }


    public FormDesignType _loadForm(String GUID, String configFileName, String databaseName, String query) {
        FormDesignType __form = new FormDesignType();
        /*if (_guidReady(GUID)) {

            try {
                Connection __conn = _connect(databaseName);
                Statement __stmt = __conn.createStatement(DataTable.TYPE_SCROLL_INSENSITIVE, DataTable.CONCUR_READ_ONLY);
                DataTable __rs = __stmt.executeQuery(query);

                __rs.last();
                __rs.first();

                __form._code = __rs.getString("formcode");
                __form._name = __rs.getString("formname");
                __form._formdesign = __rs.getBytes("formdesigntext");
                __form._formbg = __rs.getBytes("formbackground");

                __conn.close();
            } catch (Exception e) {
            }
        }*/
        return __form;
    }

    public byte[] _loadFormDesignText(String GUID, String databaseName, String query) {
        byte[] __value = new byte[1024];
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        /*if (_guidReady(GUID)) {

            try {
                Connection __conn = _connect(databaseName);
                Statement __stmt = __conn.createStatement();
                DataTable __rs = __stmt.executeQuery(query);
                DataTableMetaData __rsmd = __rs.getMetaData();
                int __colCount = __rsmd.getColumnCount();
                // int count = 0;
                while (__rs.next()) {

                    for (int __i = 1; __i <= __colCount; __i++) {
                        // String columnName = rsmd.getColumnName(i);
                        __value = __rs.getBytes(__i);
                    }

                }
                __conn.close();
            } catch (Exception e) {
            }
        }
        */
        return __value;
    }

    public String _SaveFormDesign(String GUID, String configFileName, String databaseName, String InsertorUpdateType, String formCode, String formGuidCode, String formName, byte[] fileCode, byte[] backgroundCode) {
        String __Result = "";
        String __query = "";

        switch (this._databaseID) {
            case 1:
                // PostgreSQL
                __query = "insert into formdesign(formcode, formguid_code, formname, formdesigntext, formbackground, timeupdate) values(?, ?, ?, ?, ?, now())";
                //__query = "select * from sml_guid where now()-last_access_time > \'00:05:00\'";
                break;

            case 2:
                // MySQL
                __query = "insert into formdesign(formcode, formguid_code, formname, formdesigntext, formbackground, timeupdate) values(?, ?, ?, ?, ?, now())";
                //__query = "select * from sml_guid where \'" + __getTime + "\'-last_access_time>\'1900-01-01 00:05:00.00\'";
                break;

            case 3:
            case 4:
                // Microsoft SQL
                __query = "insert into formdesign(formcode, formguid_code, formname, formdesigntext, formbackground, timeupdate) values(?, ?, ?, ?, ?, getdate())";
                //__query = "select * from sml_guid where \'" + __getTime + "\'-last_access_time>\'1900-01-01 00:05:00.00\'";
                break;

        }

        //String __query = "insert into formdesign(formcode, formguid_code, formname, formdesigntext, formbackground) values(?, ?, ?, ?, ?)";

        String __queryDelete = "DELETE FROM formdesign WHERE formcode = '" + formCode + "'";

        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        if (_guidReady(GUID)) {
            /*try {
                Connection __conn = _connect(databaseName);
                if (InsertorUpdateType.compareTo("0") == 0) {
                    Statement __stmt = __conn.createStatement();
                    __stmt.execute(__queryDelete);
                }

                java.sql.PreparedStatement __pstmt = __conn.prepareStatement(__query);

                __pstmt.setString(1, formCode);
                __pstmt.setString(2, formGuidCode);
                __pstmt.setString(3, formName);
                //__pstmt.setString(4, formName);
                __pstmt.setBytes(4, fileCode);
                __pstmt.setBytes(5, backgroundCode);
                __pstmt.executeUpdate();

                __pstmt.close();
                __conn.close();
            } catch (Exception e) {
                __Result = e.Message;
            }
            */
        }

        return __Result;
    }

    public byte[] _imagebyte(String GUID, String databaseName, String query) {
        byte[] __value = new byte[1024];
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        if (_guidReady(GUID)) {
/*
            try {
                Connection __conn = _connect(databaseName);
                Statement __stmt = __conn.createStatement();
                DataTable __rs = __stmt.executeQuery(query);
                DataTableMetaData __rsmd = __rs.getMetaData();
                int __colCount = __rsmd.getColumnCount();
                // int count = 0;
                while (__rs.next()) {

                    for (int __i = 1; __i <= __colCount; __i++) {
                        // String columnName = rsmd.getColumnName(i);
                        __value = __rs.getBytes(__i);
                    }

                }
                __conn.close();
            } catch (Exception e) {
            }*/
        }

        return __value;
    }
    public ImageType imageType = new ImageType();

    public ImageType[] _LoadImageList(String GUID, String configFileName, String databaseName, String query) {
        byte[] __xbyte = new byte[1024];
        List<ImageType> __x_result = new List<ImageType>();
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        /*if (_guidReady(GUID)) {
            try {
                Connection __conn = this._connect(databaseName);
                Statement __stmt = __conn.createStatement();
                DataTable __rs = __stmt.executeQuery(query);
                while (__rs.next()) {
                    imageType = new ImageType();
                    __xbyte =
                            __rs.getBytes("Image_File");
                    imageType._code = __rs.getString("guid_code");
                    imageType._databyteImage = __xbyte;
                    __x_result.add(imageType);
                }

                __conn.close();
            } catch (Exception e) {
                ImageType __Imagetype = new ImageType();
                __Imagetype._code = "0";
                __Imagetype._databyteImage = __xbyte;
                __x_result.add(__Imagetype);
            }

        }*/
        //return __x_result.ToArray(new ImageType[__x_result.Count]);
        return null;
        //return (ImageType[]) __x_result.toArray(new ImageType[__x_result.size()]);
    }

// somruk
// 0 = update,1=insert;
    public String _SaveImageList(String GUID, String configFileName, String databaseName, ImageType[] query, String InsertorUpdateType, String[] fields, String TableName, String swherefield, String swhereData)  {
        /*String __xquery = "";
        String __Result = "";
        String __querydelete = "";
        int __i;
        StringBuilder __s = new StringBuilder();
        StringBuilder __v = new StringBuilder();
        __v.Append(" values (");
        // String xquery ="select ImageID,ImageFile from images";
        byte[] __value = new byte[1024];
        String __guid_file = "";
        if (InsertorUpdateType.compareTo("0") == 1) {
            // xquery ="INSERT INTO Images (ImageID,ImageFile) VALUES(?,?)";
            __s.Append("insert into ");
            __s.Append(TableName);
            __s.Append(" ( ");
            for (__i = 0; __i < fields.length; __i++) {
                __s.Append(fields[__i]);
                __v.Append("?");
                if (__i < fields.length - 1) {
                    __s.Append(",");
                    __v.Append(",");
                }

            }
            // String vx = v.Append(")").ToString();
            __xquery = __s.Append(" ) ").ToString();
            __xquery +=
                    __v.Append(")").ToString();
            // s.Append(swhere);
        } else {
            // xquery ="UPDATE Images SET ImageFile = ? WHERE ImageID = ?";
      
            __s.Append("insert into ");
            __s.Append(TableName);
            __s.Append(" ( ");
            for (__i = 0; __i < fields.length; __i++) {
                __s.Append(fields[__i]);
                __v.Append("?");
                if (__i < fields.length - 1) {
                    __s.Append(",");
                    __v.Append(",");
                }

            }
            // String vx = v.Append(")").ToString();
            __xquery = __s.Append(" ) ").ToString();
            __xquery +=
                    __v.Append(")").ToString();
            __querydelete =
                    "delete from " + TableName + " where " + swherefield + " ='" + swhereData + "'";
        }

        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        if (_guidReady(GUID)) {

            try {
                Connection __conn = _connect(databaseName);
                if (InsertorUpdateType.compareTo("0") == 0) {
                    Statement __stmt = __conn.createStatement();
                    __stmt.execute(__querydelete);
                }

                java.sql.PreparedStatement __pstmt = __conn.prepareStatement(__xquery);
                if (InsertorUpdateType.compareTo("0") == 1) {
                    __pstmt.setString(1, swhereData);
                } else {
                    __pstmt.setString(1, swhereData);
                }

                for (int __x = 0; __x < query.length; __x++) {
                    __value = ((byte[]) query[__x]._databyteImage);
                    __guid_file =
                            query[__x]._code;
                    if (__value == null) {
                        __value = null;
                    }

                    __pstmt.setBytes(2, __value);
                    __pstmt.setString(3, __guid_file);
                    __pstmt.executeUpdate();
                }

                __pstmt.close();
                __conn.close();
            } catch (Exception e) {
                __Result = e.Message;
            }

        }
        return __Result;*/
        return "";
    }

    public String _DeleteImageList(            String GUID, String configFileName, String databaseName, String TableName, String swherefield, String[] swhereData)  {
        String __Result = "";
        String __querydelete = "";

        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        /*if (_guidReady(GUID)) {

            try {
                Connection __conn = _connect(databaseName);
                Statement __stmt = __conn.createStatement();
                for (int __d = 0; __d < swhereData.length; __d++) {
                    __querydelete = "delete from " + TableName + " where " + swherefield + " ='" + swhereData[__d] + "'";
                    __stmt.execute(__querydelete);
                }
// querydelete="delete from "+TableName+" where "+swherefield+"
// ='"+swhereData+"'";
// stmt.execute(querydelete);

                __stmt.close();
                __conn.close();
            } catch (Exception e) {
                __Result = e.Message;
            }

        }*/
        return __Result;
    }

    public String _queryInsertOrUpdate(            String GUID, String databaseName, String query) {
        if (databaseName.Length == 0) {
            databaseName = this._mainDatabaseName;
        }

        if (_guidReady(GUID)) {
            return _queryInsertOrUpdateNoGuid(databaseName, query);
        }

        return "guid expire";
    }

    /**
     * Query เป็นชุด เพื่อใช้ในการ Query แบบ Commit
     *
     * @param GUID
     * @param configFileName
     * @param databaseName
     * @param query
     * @return
     */
    public String _queryList(String GUID, String configFileName, String databaseName, String query) {
        String __result = "";
        String __query = "";
        /*if (_guidReady(GUID)) {
            try {
                Connection __con = null;
                if (databaseName.Length == 0) {
                    databaseName = this._mainDatabaseName;
                }

                __con = _connect(databaseName);
                // enable transactions
                __con.setAutoCommit(false);
                try {
                    DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
                    DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
                    InputSource __in = new InputSource(new StringReader(query));
                    //__in.setEncoding("UTF-8");
                    //ByteArrayInputStream __in = new ByteArrayInputStream(query.getBytes("UTF-8"));
                    Document __doc = __docBuilder.parse(__in);
                    __doc.getDocumentElement().normalize();
                    NodeList listOfQuery = __doc.getElementsByTagName("query");
                    for (int __table = 0; __table < listOfQuery.Count; __table++) {
                        Element __tableElement = (Element) listOfQuery.item(__table);
                        NodeList __get1 = __tableElement.getChildNodes();
                        //__query = ((Node) __get1.item(0)).getNodeValue();
                        __query = __tableElement.getTextContent();
                        // Get a Statement object
                        Statement __stmt = __con.createStatement();
                        __stmt.execute(__query);
                        __stmt.close();
                    } // for

                    __con.commit();
                } catch (SAXParseException __err) {
                    Console.Out.WriteLine("_queryList:" + __err.Message);
                    __con.rollback();
                    // _con.close();
                    __result =
                            __err.Message + __query;
                    Console.Out.WriteLine("** Parsing error" + ", line " + __err.getLineNumber() + ", uri " + __err.getSystemId());
                    Console.Out.WriteLine(" " + __err.Message);
                } catch (SAXException __ex) {
                    __con.rollback();
                    // _con.close();
                    __result =
                            __ex.Message.ToString() + __query;
                    Exception __x = __ex.getException();
                } catch (Throwable __t) {
                    __con.rollback();
                    // _con.close();
                    __result =
                            __t.Message + ":" + __query;
                }

                __con.close();
            } catch (Exception __ex) {
                Console.Out.WriteLine("_queryList:" + __ex.Message.ToString());
                __result =
                        __ex.Message.ToString();
            }

        }*/
        return __result;
    }

    /**
     * เพื่อใช้ดึง Config จาก SMLCONFIG.XML เพื่อจะได้รู้ว่าต้องติดต่อกับ
     * Database Server อะไร
     */
    void _reLoad() {
        // อ่านค่าสำหรับเริ่มต้นการเชื่อมต่อระบบทั้งหมด
        // String xPathName = System.getProperty("user.dir").ToString();
        /*this._databaseConfigFile = this._databaseConfig;
        try {
            String __xReloadFile = _readXmlFile(this._databaseConfigFile);
            DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
            Document __doc = __docBuilder.parse(new InputSource(new StringReader(__xReloadFile)));
            __doc.getDocumentElement().normalize();
            NodeList __listOfData = __doc.getElementsByTagName("node");
            Node __firstNode = __listOfData.item(0);
            if (__firstNode.getNodeType() == Node.ELEMENT_NODE) {
                Element __firstElement = (Element) __firstNode;
                // ---
                String __databaseServer = _xmlGetNodeValue(__firstElement, "server");
                String __userCode = _xmlGetNodeValue(__firstElement, "user");
                String __userPassword = _xmlGetNodeValue(__firstElement, "password");
                this._databaseID = Integer.parseInt(_xmlGetNodeValue(__firstElement, "database_id"));
                this._databaseServer = _decrypt(__databaseServer);
                this._userCode = _decrypt(__userCode);
                this._userPassword = _decrypt(__userPassword);

                this._mainDatabaseName = _xmlGetNodeValue(__firstElement, "main_database");
                this._mainDatabaseStruct = _xmlGetNodeValue(__firstElement, "main_database_struct");
                this._databaseConfig = _xmlGetNodeValue(__firstElement, "database_config");
            }

            _mainDatabaseStructFile = _mainDatabaseStruct;
        } catch (SAXParseException __err) {
            Console.Out.WriteLine("** Parsing error" + ", line " + __err.getLineNumber() + ", uri " + __err.getSystemId());
            Console.Out.WriteLine(" " + __err.Message);
        } catch (SAXException __ex) {
            Exception __x = __ex.getException();
        } catch (Throwable __t) {
        }*/
        // Console.Out.WriteLine("reload");
    }

    public String _systemStartup(            String GUID, String databaseName, String screenGroup, String viewTableName, String viewDetailTableName, String dataViewFileName, String dataViewTemplateFileName) {
        String __result = "";
        // ตรวจสอบว่ามีข้อมูล DataView ใน Table หรือไม่ ถ้าไม่มี ก็ดึงจาก
        // Template เข้ามา
        /*try {
            String __xReloadFile = _readXmlFile(dataViewTemplateFileName);
            Connection __conn = _connect(databaseName);
            DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
            Document __doc = __docBuilder.parse(new InputSource(new StringReader(__xReloadFile)));
            __doc.getDocumentElement().normalize();
            NodeList __xReader = __doc.getElementsByTagName("screen");
            for (int __table = 0; __table < __xReader.Count; __table++) {
                Node __xFirstNode = __xReader.item(__table);
                if (__xFirstNode.getNodeType() == Node.ELEMENT_NODE) {
                    Boolean __foundScreen = false;
                    Element __xTable = (Element) __xFirstNode;
                    String __screenCode = __xTable.getAttribute("screen_code");
                    String __query = "select screen_code as xcount from " + viewTableName + " where " + _addUpper("screen_code") + "=\'" + __screenCode.toUpperCase() + "\'";
                    try {
                        Statement __stmt = __conn.createStatement();
                        DataTable __rs = __stmt.executeQuery(__query);
                        while (__rs.next()) {
                            __foundScreen = true;
                            break;
                        }
                        __rs.close();
                        __stmt.close();
                    } catch (Exception __ex) {
                    }

                    if (__foundScreen == false) {
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append("<?xml version=\'1.0\' encoding=\'utf-8\' ?><node>");
                        // เพิ่มในกลุ่มข้อมูล
                        __myQuery.Append("<query>");
                        __myQuery.Append("delete from ").Append(viewDetailTableName).Append(" where screen_group=1 and screen_code=\'").Append(__screenCode).Append("\'");
                        __myQuery.Append("</query>");
                        __myQuery.Append("<query>");
                        __myQuery.Append("insert into ").Append(viewTableName).Append(" (screen_code,name_1,name_2,table_name,table_list,sort,filter,width_persent) values ");
                        __myQuery.Append("(\'").Append(__screenCode).Append("\',\'").Append(__xTable.getAttribute("name_1")).Append("\',");
                        __myQuery.Append("\'").Append(__xTable.getAttribute("name_2")).Append("\',");
                        __myQuery.Append("\'").Append(__xTable.getAttribute("table_name")).Append("\',");
                        __myQuery.Append("\'").Append(__xTable.getAttribute("table_list")).Append("\',");
                        __myQuery.Append("\'").Append(__xTable.getAttribute("sort")).Append("\',");
                        __myQuery.Append("\'").Append(__xTable.getAttribute("filter")).Append("\',");
                        String __getdata = __xTable.getAttribute("width_by_persent").ToLower();
                        __myQuery.Append(((__getdata.ToLower().compareTo("true") == 0) ? "1" : "0")).Append(")");
                        __myQuery.Append("</query>");
                        NodeList __xDetail = __xTable.getElementsByTagName("detail");
                        for (int __detail = 0; __detail < __xDetail.Count; __detail++) {
                            Node __xDetailFirstNode = __xDetail.item(__detail);
                            if (__xDetailFirstNode.getNodeType() == Node.ELEMENT_NODE) {
                                Element __xDetailData = (Element) __xDetailFirstNode;
                                __myQuery.Append("<query>");
                                __myQuery.Append("insert into ").Append(viewDetailTableName).Append(" (screen_group,screen_code,column_number,column_name,column_name_2,column_field_name,column_field_sort,column_resource,column_format,column_width,column_type,column_align) values (");
                                __myQuery.Append("1,");
                                __myQuery.Append("\'").Append(__screenCode).Append("\',");
                                __myQuery.Append((__detail + 1)).Append(",");
                                __myQuery.Append("\'").Append(__xDetailData.getAttribute("column_name").Replace("\'", "\'\'")).Append("\',");
                                __myQuery.Append("\'").Append(__xDetailData.getAttribute("column_name_2").Replace("\'", "\'\'")).Append("\',");
                                __myQuery.Append("\'").Append(__xDetailData.getAttribute("column_field_name").Replace("\'", "\'\'")).Append("\',");
                                __myQuery.Append("\'").Append(__xDetailData.getAttribute("column_field_sort").Replace("\'", "\'\'")).Append("\',");
                                __myQuery.Append("\'").Append(__xDetailData.getAttribute("column_resource").Replace("\'", "\'\'")).Append("\',");
                                __myQuery.Append("\'").Append(__xDetailData.getAttribute("format").Replace("\'", "\'\'")).Append("\',");
                                __myQuery.Append(__xDetailData.getAttribute("column_width")).Append(",");
                                __myQuery.Append(_databaseColumnFind(__xDetailData.getAttribute("column_type"))).Append(",");
                                int __alignInt = 0;
                                String __getColumnAlign = __xDetailData.getAttribute("column_align").ToLower();
                                if (__getColumnAlign.compareTo("left") == 0) {
                                    __alignInt = 0;
                                }

                                if (__getColumnAlign.compareTo("center") == 0) {
                                    __alignInt = 1;
                                }

                                if (__getColumnAlign.compareTo("right") == 0) {
                                    __alignInt = 2;
                                }

                                __myQuery.Append(__alignInt).Append(")");
                                __myQuery.Append("</query>");
                            }

                        } // for Table Sub
                        __myQuery.Append("</node>");
                        __result =
                                _queryList(GUID, this._databaseConfig, databaseName, __myQuery.ToString());
                    }

                }
            } // for table
            __conn.close();
        } catch (Exception __ex) {
            __result = __ex.Message.ToString();
        }
        */
        return __result;
    }

//private
    public int _databaseColumnFind(String name) {
        /*for (int __loop = 0; __loop < _databaseColumnTypeList.length; __loop++) {
            if (name.ToLower().compareTo(_databaseColumnTypeList[__loop].ToString().ToLower()) == 0) {
                return __loop;
            }

        } // for
         * */
        return 0;
    }

    public void _resourceInsertAll(String databaseStructFile, String dataGroup) {
        /*try {
            Connection __conn = _connect(this._mainDatabaseName);
            //
            Boolean __xFound = false;
            String __xReloadFile = _readXmlFile(databaseStructFile);
            DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
            Document __doc = __docBuilder.parse(new InputSource(new StringReader(__xReloadFile)));
            __doc.getDocumentElement().normalize();
            NodeList __xReader = __doc.getElementsByTagName("table");
            for (int __table = 0; __table < __xReader.Count && __xFound == false; __table++) {
                Node __xFirstNode = __xReader.item(__table);
                if (__xFirstNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element __xTable = (Element) __xFirstNode;
                    String __tableName = __xTable.getAttribute("name").ToLower();
                    NodeList __xReaderField = __xTable.getElementsByTagName("field");
                    for (int __field = 0; __field < __xReaderField.Count; __field++) {
                        Element __xFieldData = (Element) __xReaderField.item(__field);
                        String __fieldName = __xFieldData.getAttribute("name").ToLower();
                        String __langThai = __xFieldData.getAttribute("thai");
                        int __length = 0;
                        String __langEng = "";
                        String __langChina = "";
                        String __langMalay = "";
                        String __langIndia = "";
                        int __status = 1;
                        try {
                            __length = Integer.valueOf(__xFieldData.getAttribute("length")).intValue();
                        } catch (Exception __ex) {
                            __length = 0;
                        }
//

                        Statement __statment = __conn.createStatement();
                        DataTable __rs = __statment.executeQuery("select english_lang,chinese_lang,malay_lang,india_lang from sml_language where thai_lang=\'" + __langThai + "\'");
                        if (__rs.next()) {
                            __langEng = __rs.getString("english_lang");
                            __langChina =
                                    __rs.getString("chinese_lang");
                            __langMalay =
                                    __rs.getString("malay_lang");
                            __langIndia =
                                    __rs.getString("india_lang");
                        }

                        __statment.close();
                        //
                        try {
                            String __query = "insert into sml_resource (data_group,code,name_1,name_2,name_3,name_4,name_5,name_6,status,length) values (\'" + dataGroup + "\',\'" + __tableName + "." + __fieldName + "\',\'" + __langThai + "\',\'" + __langEng + "\',\'" + __langChina + "\',\'" + __langMalay + "\',\'" + __langIndia + "\',\'\'," + __status + "," + __length + ")";
                            Statement __stmt = __conn.createStatement();
                            __stmt.execute(__query);
                            __stmt.close();
                        } catch (Exception __ex) {
                        }
                        //
                    }
                }
            }
            //
            __conn.close();
        } catch (Exception __ex) {
        }*/
    }

    public ResourceType _findResourceFromDatabaseType(            String databaseName, String databaseStructFile, String code, String defaultName) {
        ResourceType __result = new ResourceType();
        __result._code = code;
        __result._name_1 = (defaultName.Length == 0) ? code : defaultName;
        __result._name_2 = code;
        __result._name_3 = "";
        __result._name_4 = "";
        __result._name_5 = "";
        __result._name_6 = "";
        __result._status = 0;
        __result._length = 0;
        //
        String[] __xCodeSplit = null;
        /*String __code = code.ToLower();
        __xCodeSplit =
                __code.split("\\.");
        if (__xCodeSplit.length == 2) {
            try {
                Boolean __xFound = false;
                String __xReloadFile = _readXmlFile(databaseStructFile);
                DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
                DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
                Document __doc = __docBuilder.parse(new InputSource(new StringReader(__xReloadFile)));
                __doc.getDocumentElement().normalize();
                NodeList __xReader = __doc.getElementsByTagName("table");
                for (int __table = 0; __table < __xReader.Count && __xFound == false; __table++) {
                    Node __xFirstNode = __xReader.item(__table);
                    if (__xFirstNode.getNodeType() == Node.ELEMENT_NODE) {
                        Element __xTable = (Element) __xFirstNode;
                        if (__xTable.getAttribute("name").ToLower().compareTo(__xCodeSplit[0].ToLower()) == 0) {
                            NodeList __xReaderField = __xTable.getElementsByTagName("field");
                            for (int __field = 0; __field < __xReaderField.Count; __field++) {
                                Element __xFieldData = (Element) __xReaderField.item(__field);
                                if (__xFieldData.getAttribute("name").ToLower().compareTo(__xCodeSplit[1].ToLower()) == 0) {
                                    __result._name_1 = __xFieldData.getAttribute("thai");
                                    __result._status = 1;
                                    try {
                                        __result._length = Integer.valueOf(__xFieldData.getAttribute("length")).intValue();
                                    } catch (Exception __ex) {
                                        __result._length = 0;
                                    }

                                    __xFound = true;
                                    break;

                                }




                            }
                        }
                    }
                }
            } catch (Exception __ex) {
            }
        }
        try {
            // find translate
            __result._name_2 = __result._name_3 = __result._name_4 = __result._name_5 = "";
            Connection __conn = _connect(databaseName);
            Statement __statment = __conn.createStatement();
            DataTable __rs = __statment.executeQuery("select english_lang,chinese_lang,malay_lang,india_lang from sml_language where thai_lang=\'" + __result._name_1 + "\'");
            if (__rs.next()) {
                __result._name_2 = __rs.getString("english_lang").Replace("\'", "\'\'");
                __result._name_3 = __rs.getString("chinese_lang").Replace("\'", "\'\'");
                __result._name_4 = __rs.getString("malay_lang").Replace("\'", "\'\'");
                __result._name_5 = __rs.getString("india_lang").Replace("\'", "\'\'");
            }

            __statment.close();
            __conn.close();
        } catch (Exception __ex) {
        }*/
        return __result;
    }

    public ResourceType _resourceInsert(            String databaseName, String dataGroup, String code, String defaultName, String structFileName) {
        ResourceType __xGetResource = _findResourceFromDatabaseType(databaseName, structFileName, code, defaultName);
        /*try {
            if (code.compareTo(__xGetResource._name_1) != 0) {
                String __query = "insert into sml_resource (data_group,code,name_1,name_2,name_3,name_4,name_5,name_6,status,length) values (\'" + dataGroup + "\',\'" + code + "\',\'" + __xGetResource._name_1 + "\',\'" + __xGetResource._name_2 + "\',\'" + __xGetResource._name_3 + "\',\'" + __xGetResource._name_4 + "\',\'" + __xGetResource._name_5 + "\',\'" + __xGetResource._name_6 + "\'," + __xGetResource._status + "," + __xGetResource._length + ")";
                _queryInsertOrUpdateNoGuid(this._mainDatabaseName, __query);
            }

        } catch (Exception __ex) {
        }*/
        return __xGetResource;
    }

    public String _getSchemaTable(            String databaseName, String tableName) {
        String __result = "<node>";
        try {
            /*Connection __con = _connect(databaseName);
            DatabaseMetaData __dbmd = __con.getMetaData();
            DataTable __columnRS = __dbmd.getColumns(null, null, tableName, null);
            while (__columnRS.next()) {
                String __getFieldName = __columnRS.getString("column_name");
                String __getLength = __columnRS.getString("COLUMN_SIZE");
                String __getFieldType = __columnRS.getString("TYPE_NAME");
                __result +=
                        ("<detail table_name=\'" + tableName + "\' column_name=\'" + __getFieldName + "\' length=\'" + __getLength + "\' type=\'" + __getFieldType + "\'></detail>");
            }

            __columnRS.close();
            __con.close();*/

            /*
             * Connection conn = _connect(databaseName); String query = "SELECT
             * column_name=syscolumns.name,datatype=systypes.name,length=syscolumns.length
             * FROM sysobjects " + "JOIN syscolumns ON sysobjects.id =
             * syscolumns.id " + "JOIN systypes ON
             * syscolumns.xtype=systypes.xtype " + "WHERE sysobjects.xtype='U'
             * and sysobjects.name='" + tableName + "' " + "ORDER BY
             * sysobjects.name,syscolumns.colid"; Statement stmt =
             * conn.createStatement(); DataTable rs = stmt.executeQuery(query);
             * while (rs.next()) { String getFieldName =
             * rs.getString("column_name"); String getLength =
             * rs.getString("length"); String getFieldType =
             * rs.getString("datatype"); result += ("<detail table_name=\'" +
             * tableName + "\' column_name=\'" + getFieldName + "\' length=\'" +
             * getLength + "\' type=\'" + getFieldType + "\'></detail>"); }
             * rs.close(); stmt.close(); conn.close();
             */
        } catch (Exception __ex) {
        }
        return __result.ToLower() + "</node>";
    }
// Compression

    public String _changePassword(            String databaseName, String userCode, String oldPassword, String newPassword) {
        Boolean __foundUser = false;
        Boolean __success = false;
        /*try {
            Connection __conn = this._connect(databaseName);
            Statement __stmt = __conn.createStatement();
            String __query = "select * from sml_user_list where " + _addUpper("user_code") + "=\'" + userCode.toUpperCase() + "\' and user_password=\'" + oldPassword + "\'";
            DataTable __rs = __stmt.executeQuery(__query);
            if (__rs.next()) {
                __foundUser = true;
            }

            __rs.close();
            __stmt.close();
            __conn.close();
        } catch (Exception __ex) {
        }
        if (__foundUser) {
            try {
                Connection __conn = this._connect(databaseName);
                Statement __stmt = __conn.createStatement();
                String __query = "update sml_user_list set user_password=\'" + newPassword + "\' where " + _addUpper("user_code") + "=\'" + userCode.toUpperCase() + "\' and user_password=\'" + oldPassword + "\'";
                if (__stmt.executeUpdate(__query) == 1) {
                    __success = true;
                }

                __stmt.close();
                __conn.close();
            } catch (Exception __ex) {
            }
        }*/
        return (__success) ? "1" : "0";
    }

    public String _loginProcess(            String databaseName, String userCode, String userPassword, String computerName, String databaseCode) {
        Boolean __foundUser = false;
        String __newGuid = "";
        /*try {
            Connection __conn = this._connect(databaseName);
            Statement __stmt = __conn.createStatement();
            String __query = "select * from sml_user_list where " + _addUpper("user_code") + "=\'" + userCode.toUpperCase() + "\' and user_password=\'" + userPassword + "\'";
            DataTable __rs = __stmt.executeQuery(__query);
            if (__rs.next()) {
                __foundUser = true;
            }

            __rs.close();
            __stmt.close();
            __conn.close();
        } catch (Exception __ex) {
            return "";
        }

        if (__foundUser) {
            RandomGUID __getGUID = new RandomGUID();
            __newGuid =
                    __getGUID.ToString();
            try {
                _queryInsertOrUpdateNoGuid(databaseName, "delete from sml_guid where guid_code=\'" + __newGuid + "\'");
            } catch (Exception __ex) {
            }
            try {
                Date __now = new Date();
                SimpleDateFormat __df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.ENGLISH);
                String __getTime = __df.format(__now.getTime());
                String __query = "insert into sml_guid (guid_code,login_time,last_access_time,user_code,computer_name,database_code) values (\'" + __newGuid + "\',\'" + __getTime + "\',\'" + __getTime + "\',\'" + userCode + "\'" + ",\'" + computerName + "\',\'" + databaseCode + "\')";
                _queryInsertOrUpdateNoGuid(databaseName, __query);
                //
                __query =
                        "insert into sml_access_list (access_time,access_type,user_code,computer_name,database_code) values (\'" + __getTime + "\',1,\'" + userCode + "\',\'" + computerName + "\',\'" + databaseCode + "\')";
                _queryInsertOrUpdateNoGuid(databaseName, __query);
            } catch (Exception __ex) {
            }
        }*/
        return __newGuid;
    }

//private
    public Boolean _guidReady(String guid) {
        return true;
        /*if (guid.Equals("SMLX")) {
        return true;
        }
        Boolean __result = false;
        try {
        Connection __conn = this._connect(this._mainDatabaseName);
        Statement __stmt = __conn.createStatement(DataTable.TYPE_FORWARD_ONLY, DataTable.CONCUR_READ_ONLY);
        String __query = "select guid_code from sml_guid where " + _addUpper("guid_code") + "=\'" + guid.toUpperCase() + "\'";
        DataTable __rs = __stmt.executeQuery(__query);
        if (__rs.next()) {
        __result = true;
        }
        __rs.close();
        __stmt.close();
        __conn.close();
        } catch (Exception __ex) {
        }
        return __result;*/
    }

//private
    public void _unlockRecord(String databaseName, String tableListString) {
        // ArrayList<String> tableList = new ArrayList<String>();
        /*Connection __connMain = this._connect(this._mainDatabaseName);
        Statement __stmtMain = __connMain.createStatement(DataTable.TYPE_FORWARD_ONLY, DataTable.CONCUR_READ_ONLY);
        DataTable __rsMain = __stmtMain.executeQuery("select guid_code from sml_guid");
        StringBuilder __notIn = new StringBuilder();
        while (__rsMain.next()) {
            if (__notIn.Length != 0) {
                __notIn.Append(",");
            }

            __notIn.Append("\'").Append(__rsMain.getString(1).toUpperCase()).Append("\'");
        }
//

        if (__notIn.Length > 0) {
            Object[] __tableList = tableListString.split(",");
            for (int __table = 0; __table < __tableList.length; __table++) {
                Boolean __foundData = false;
                try {
                    Connection __conn = this._connect(databaseName);
                    Statement __stmt = __conn.createStatement();
                    String __query = "select * from " + __tableList[__table] + " where guid_code is not null and " + _addUpper("guid_code") + " not in (" + __notIn.ToString() + ")";
                    DataTable __rs = __stmt.executeQuery(__query);
                    if (__rs.next()) {
                        __foundData = true;
                    }

                    __rs.close();
                    __stmt.close();
                    __conn.close();
                } catch (Exception __ex) {
                }
                if (__foundData) {
                    String query = "update " + __tableList[__table] + " set guid_code=null where guid_code is not null and " + _addUpper("guid_code") + " not in (" + __notIn.ToString() + ")";
                    _queryInsertOrUpdateNoGuid(databaseName, query);
                }

            }
        }
        __rsMain.close();
        __stmtMain.close();
        __connMain.close();
         * */
    }

    public Boolean _sendGuid(String guid, String databaseName, String tableList) {
        /*Date __now = new Date();
        SimpleDateFormat __df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.ENGLISH);
        String __getTime = __df.format(__now.getTime());
        try {
            String __query = "update sml_guid set last_access_time=\'" + __getTime + "\' where " + _addUpper("guid_code") + "=\'" + guid.toUpperCase() + "\'";
            _queryInsertOrUpdateNoGuid(this._mainDatabaseName, __query);
            Boolean __foundUser = false;
            try {
                Connection __conn = this._connect(this._mainDatabaseName);
                Statement __stmt = __conn.createStatement();
                switch (this._databaseID) {
                    case 1:
                        // PostgreSQL
                        __query = "select * from sml_guid where now()-last_access_time > \'00:05:00\'";
                        break;

                    case 2:
                        // MySQL
                        __query = "select * from sml_guid where \'" + __getTime + "\'-last_access_time>\'1900-01-01 00:05:00.00\'";
                        break;

                    case 3:
                    case 4:
                        // Microsoft SQL
                        __query = "select * from sml_guid where \'" + __getTime + "\'-last_access_time>\'1900-01-01 00:05:00.00\'";
                        break;

                }




                DataTable __rs = __stmt.executeQuery(__query);
                if (__rs.next()) {
                    __foundUser = true;
                }

                __rs.close();
                __stmt.close();
                __conn.close();
            } catch (Exception __ex) {
            }
            if (__foundUser) {
                switch (this._databaseID) {
                    case 1:
                        // PostgreSQL
                        __query = "delete from sml_guid where now()-last_access_time > \'00:05:00\'";
                        break;

                    case 2:
                        // MySQL
                        __query = "delete from sml_guid where \'" + __getTime + "\'-last_access_time>\'1900-01-01 00:05:00.00\'";
                        break;

                    case 3:
                    case 4:
                        // Microsoft SQL
                        __query = "delete from sml_guid where \'" + __getTime + "\'-last_access_time>\'1900-01-01 00:05:00.00\'";
                        break;

                }




                _queryInsertOrUpdateNoGuid(this._mainDatabaseName, __query);
            }

            _unlockRecord(databaseName, tableList);
        } catch (Exception __ex) {
        }*/
        return _guidReady(guid);
    }

    public void _logoutProcess(String guid, String databaseName, String userCode, String computerName, String databaseCode, String tableList) {
        /*try {
            String __query = "delete from sml_guid where guid_code=\'" + guid + "\'";
            _queryInsertOrUpdateNoGuid(this._mainDatabaseName, __query);
            _unlockRecord(databaseName, tableList);
            //
            Date __now = new Date();
            SimpleDateFormat __df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.ENGLISH);
            String __getTime = __df.format(__now.getTime());

            __query =
                    "insert into sml_access_list (access_time,access_type,user_code,computer_name,database_code) values (\'" + __getTime + "\',2,\'" + userCode + "\',\'" + computerName + "\',\'" + databaseCode + "\')";
            _queryInsertOrUpdateNoGuid(this._mainDatabaseName, __query);
        } catch (Exception __ex) {
        }*/
    }

    /**
     * Query ดึงข้อมูลเป็นชุด เพื่อลดการติดต่อระหว่างโปรแกรม ทำให้เร็วขึ้นมาก
     *
     * @param GUID
     * @param configFileName
     * @param databaseName
     * @param query
     * @return
     */
    public String _queryListGetData(
            String GUID, String configFileName, String databaseName, String query) {
        StringBuilder __result = new StringBuilder();
        String __query = "";
        /*if (_guidReady(GUID)) {
            try {
                Connection __con = null;
                if (databaseName.Length == 0) {
                    databaseName = this._mainDatabaseName;
                }

                __con = _connect(databaseName);
                // enable transactions
                try {
                    DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
                    DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
                    Document __doc = __docBuilder.parse(new InputSource(new StringReader(query)));
                    __doc.getDocumentElement().normalize();
                    __result.Append(_xmlTagHead);
                    __result.Append("<node>");
                    NodeList __listOfQuery = __doc.getElementsByTagName("query");
                    for (int __table = 0; __table < __listOfQuery.Count; __table++) {
                        Element __tableElement = (Element) __listOfQuery.item(__table);
                        __query =
                                __tableElement.getTextContent();
                        // Get a Statement object
                        __result.Append("<DataTable>");
                        try {
                            Statement __stmt = __con.createStatement();
                            DataTable __rs = __stmt.executeQuery(__query);
                            __result.Append(_DataTableToXml(__rs, -1, -1, false, ""));
                            __rs.close();
                            __stmt.close();
                        } catch (Exception __ex) {
                            return __ex.Message.ToString() + ":" + __query;
                        }

                        __result.Append("</DataTable>");
                    } // for

                    __result.Append("</node>");
                } catch (SAXParseException __err) {
                    // _con.close();
                    Console.Out.WriteLine("_queryListGetData:" + __err.Message);
                    __result.Append(__err.Message).Append(__query);
                    Console.Out.WriteLine("** Parsing error" + ", line " + __err.getLineNumber() + ", uri " + __err.getSystemId());
                    Console.Out.WriteLine(" " + __err.Message);
                } catch (SAXException __ex) {
                    // _con.close();
                    __result.Append(__ex.Message.ToString()).Append(__query);
                    Exception __x = __ex.getException();
                } catch (Throwable __t) {
                    // _con.close();
                    __result.Append(__t.Message).Append(":").Append(__query);
                }

                __con.close();
            } catch (Exception __ex) {
                Console.Out.WriteLine("_queryListGetData:" + __ex.Message.ToString());
                __result.Append(__ex.Message.ToString());
            }

        }*/
        return __result.ToString();
    }

    public double _convertTextTodouble(String resultData) {
        double __result = 0;
        /*try {
            if (resultData == null) {
            } else if (resultData.Length == 0) {
                __result = 0;
            } else {
                __result = Double.parseDouble(resultData);
            }

        } catch (Exception ex) {
            return __result;
        }
        */
        return __result;
    }

    public String _checkStringNull(
            String resultData) {
        String __result = "";
        try {
            if (resultData == null) {
            } else if (resultData.Length == 0) {
                return __result.ToString();
            } else {
                __result = resultData.ToString();
            }

        } catch (Exception ex) {
            return __result.ToString();
        }

        return __result.ToString();
    }

    public _routine(String configFileName) {
        _changeDatabaseConfig(configFileName);
        this._reLoad();
    }

    public String _getDatabaseCode(            String configFileName) {
        //somruk
        // อ่านค่าสำหรับเริ่มต้นการเชื่อมต่อระบบทั้งหมด
        // String xPathName = System.getProperty("user.dir").ToString();
        String __result = "0";
        int __databaseID = 1;
        /*try {
            String __xReloadFile = _readXmlFile(configFileName);
            DocumentBuilderFactory __docBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder __docBuilder = __docBuilderFactory.newDocumentBuilder();
            Document __doc = __docBuilder.parse(new InputSource(new StringReader(__xReloadFile)));
            __doc.getDocumentElement().normalize();
            NodeList __listOfData = __doc.getElementsByTagName("node");
            Node __firstNode = __listOfData.item(0);
            if (__firstNode.getNodeType() == Node.ELEMENT_NODE) {
                Element __firstElement = (Element) __firstNode;
                // ---
                __databaseID =
                        Integer.parseInt(_xmlGetNodeValue(__firstElement, "database_id"));

            }

            __result = String.valueOf(__databaseID).ToString();
        } catch (SAXParseException __err) {
            Console.Out.WriteLine("** Parsing error" + ", line " + __err.getLineNumber() + ", uri " + __err.getSystemId());
            Console.Out.WriteLine(" " + __err.Message);
        } catch (SAXException __ex) {
            Exception __x = __ex.getException();
        } catch (Throwable __t) {
        }*/
        return __result;
    }

    }

public class FormDesignType {
       public String _code;
    public String _name;
    public Object _formdesign;
    public Object _formbg;
}

public class ImageType {

  public String _code;
  public Object _databyteImage;
}

public class ResourceType {

  public String _code;
  public String _name_1;
  public String _name_2;
  public String _name_3;
  public String _name_4;
  public String _name_5;
  public String _name_6;
  public int _status;
  public int _length;
}
}
