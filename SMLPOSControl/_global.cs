using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SMLPOSControl
{
    public class _global
    {
        /// <summary>auto insert จอขาย</summary>
        public static void _checkInitPosDesign()
        {
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            string _query = "select count(" + _g.d.sml_posdesign._screen_code + ") as xcount from " + _g.d.sml_posdesign._table + " where screen_code in ('POS1', 'POS2', 'POS3', 'TOMYUMGOONG1', 'TOMYUMGOONG2')";
            DataTable __table = __fw._query(MyLib._myGlobal._databaseName, _query).Tables[0];
            XmlDocument _xmlDocs;

            if (MyLib._myGlobal._decimalPhase(__table.Rows[0][0].ToString()) == 0)
            {
                try
                {
                    StreamReader __source = MyLib._getStream._getDataStream(MyLib._myGlobal._posDesignXmlFileName);
                    string __getXml = __source.ReadToEnd();
                    __source.Close();

                    if (__getXml.Length > 0)
                    {
                        _xmlDocs = new XmlDocument();
                        _xmlDocs.LoadXml(__getXml);
                        XmlNodeList __xmlList = _xmlDocs.SelectNodes("/node/posscreen");
                        foreach (XmlNode __node in __xmlList)
                        {
                            string __screenid = __node.Attributes["screenid"].Value.ToString();
                            string __screenname = __node.Attributes["screenname"].Value.ToString();
                            string __reportContent = __node["content"].InnerText;

                            byte[] __reportByte = Convert.FromBase64String(__reportContent);
                            string __reportStr = MyLib._compress._deCompressString(__reportByte);

                            SMLPOSControl._designer._posDesignXML __xmlClass = (SMLPOSControl._designer._posDesignXML)MyLib._myGlobal.FromXml(__reportStr, typeof(SMLPOSControl._designer._posDesignXML));

                            XmlSerializer __xs = new XmlSerializer(typeof(SMLPOSControl._designer._posDesignXML));
                            MemoryStream __memoryStream = new MemoryStream();
                            __xs.Serialize(__memoryStream, __xmlClass);

                            string _queryIns = string.Format("insert into " + _g.d.sml_posdesign._table + "(" + _g.d.sml_posdesign._screen_code + "," + _g.d.sml_posdesign._screen_name + "," + _g.d.sml_posdesign._screen_data + ") VALUES('{0}','{1}',?)", __screenid, __screenname);

                            byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                            if (MyLib._myGlobal._allowProcessReportServer == true)
                            {
                                string __result = __fw._queryByteData(MyLib._myGlobal._databaseName, _queryIns, new object[] { __memoryStreamCompress });
                            }

                        }
                    }
                    else
                    {
                        // not found file init screen
                        MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_POSDesignScreen.txt", DateTime.Now.ToString() + " : Not Found File Init POS Design", true);
                    }
                }
                catch (Exception ex)
                {
                    string __error = ex.ToString();
                }
            }

            if (MyLib._myGlobal._OEMVersion.Equals("ais"))
            {
                // initial mpay screen
                _query = "select count(" + _g.d.sml_posdesign._screen_code + ") as xcount from " + _g.d.sml_posdesign._table + " where screen_code in ('MPAYPOS')";
                __table = __fw._query(MyLib._myGlobal._databaseName, _query).Tables[0];
                if (MyLib._myGlobal._decimalPhase(__table.Rows[0][0].ToString()) == 0)
                {
                    // get screen and insert to server

                    MyLib._myFrameWork __myFrameWorkMaster = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

                    // start sycn
                    // select from master
                    string __query = string.Format("select " + _g.d.sml_posdesign._screen_data + " from " + _g.d.sml_posdesign._table + " where UPPER(" + _g.d.sml_posdesign._screen_code + ") = '{0}'", "MPAYPOS");
                    byte[] __getByte = __myFrameWorkMaster._queryByte(MyLib._myGlobal._masterDatabaseName, __query);


                    try
                    {
                        // insert to local

                        string __query2 = string.Format("insert into " + _g.d.sml_posdesign._table + "(" + _g.d.sml_posdesign._screen_code + "," + _g.d.sml_posdesign._screen_name + ", " + _g.d.sml_posdesign._screen_data + ") VALUES('{0}','{1}', ?)", "MPAYPOS", "MPAYPOS");
                        string __resultStr = __fw._queryByteData(MyLib._myGlobal._databaseName, __query2, new object[] { __getByte });

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                    }

                }

                // insert warehouse and shelf
                _query = "select count(" + _g.d.ic_warehouse._code + ") as xcount from " + _g.d.ic_warehouse._table + "";
                __table = __fw._query(MyLib._myGlobal._databaseName, _query).Tables[0];

                if (MyLib._myGlobal._decimalPhase(__table.Rows[0][0].ToString()) == 0)
                {
                    string __resultStr = __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into ic_warehouse (code, name_1) values ('001', 'คลังหน้าร้าน')");
                    __resultStr = __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into ic_shelf (code, name_1, whcode) values ('01', 'ที่เก็บหน้าร้าน', '001')");
                }
            }

            /*
        else if (__table.Rows.Count != 5)
        {
            try
            {
                StreamReader __source = MyLib._getStream._getDataStream(MyLib._myGlobal._posDesignXmlFileName);
                string __getXml = __source.ReadToEnd();
                __source.Close();

                if (__getXml.Length > 0)
                {
                    _xmlDocs = new XmlDocument();
                    _xmlDocs.LoadXml(__getXml);
                    XmlNodeList __xmlList = _xmlDocs.SelectNodes("/node/posscreen");
                    foreach (XmlNode __node in __xmlList)
                    {
                        string __screenid = __node.Attributes["screenid"].Value.ToString();
                        string __screenname = __node.Attributes["screenname"].Value.ToString();
                        string __reportContent = __node["content"].InnerText;

                        byte[] __reportByte = Convert.FromBase64String(__reportContent);
                        string __reportStr = MyLib._compress._deCompressString(__reportByte);

                        SMLPOSControl._designer._posDesignXML __xmlClass = (SMLPOSControl._designer._posDesignXML)FromXml(__reportStr, typeof(SMLPOSControl._designer._posDesignXML));

                        XmlSerializer __xs = new XmlSerializer(typeof(SMLPOSControl._designer._posDesignXML));
                        MemoryStream __memoryStream = new MemoryStream();
                        __xs.Serialize(__memoryStream, __xmlClass);

                        string _queryIns = string.Format("insert into " + _g.d.sml_posdesign._table + "(" + _g.d.sml_posdesign._screen_code + "," + _g.d.sml_posdesign._screen_name + "," + _g.d.sml_posdesign._screen_data + ") VALUES('{0}','{1}',?)", __screenid, __screenname);

                        byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                        string __result = __fw._queryByteData(MyLib._myGlobal._databaseName, _queryIns, new object[] { __memoryStreamCompress });

                    }
                }
                else
                {
                    // not found file init screen
                    MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_POSDesignScreen.txt", DateTime.Now.ToString() + " : Not Found File Init POS Design", true);
                }
            }
            catch (Exception ex)
            {
                string __error = ex.ToString();
            }
        }*/
        }


        /// <summary>
        /// กำหนด config เริ่มต้น
        /// </summary>
        public static void _checkInitConfig()
        {
            if (MyLib._myGlobal._isDemo == false)
            {
                // path in smlsoft
                string __localPath = string.Format(@"c:\\smlsoft\\smlPOSScreenConfig-{0}-{1}-{2}.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName);
                bool __fileInLocal = MyLib._myUtil._fileExists(__localPath);
                if (__fileInLocal == false)
                {
                    SMLPOSControl._posScreenConfig __config = new SMLPOSControl._posScreenConfig();

                    Screen __primary = Screen.PrimaryScreen;
                    string _screenDeviceName = Regex.Split(__primary.DeviceName, "\0")[0].ToString();

                    // add pos screen
                    SMLPOSControl._screenConfig __device = new SMLPOSControl._screenConfig() { _moniter = "0", _deviceName = MyLib._myUtil._convertTextToXml(_screenDeviceName) };
                    __device._isMasterScreen = true;

                    if (MyLib._myGlobal._OEMVersion.Equals("ais"))
                    {
                        __device._screen_code = "MPAYPOS";
                    }
                    else
                        __device._screen_code = "POS1";
                    __config._screenConfig.Add(__device);

                    // other config 
                    __config._pos_slip_header_fontname = "Tahoma";
                    __config._pos_slip_header_fontsize = 9;
                    __config._pos_slip_detail_fontname = "Tahoma";
                    __config._pos_slip_detail_fontsize = 9;
                    __config._pos_slip_footer_fontname = "Tahoma";
                    __config._pos_slip_footer_fontsize = 9;
                    __config._pos_slip_width = 579;

                    __config._invoiceSlip = "0";
                    __config._printerType = "0";
                    __config._useCashDrawer = "0";
                    __config._drawerCodes = "";

                    // sound
                    __config._use_sound_scan_barcode = 1;
                    __config._sound_on_scan_barcode = @"C:\smlsoft\beep-24.wav";
                    __config._use_sound_on_already_barcode = 1;
                    __config._sound_on_already_barcode = @"C:\smlsoft\beep-30.wav";
                    __config._use_sound_on_not_found_barcode = 1;
                    __config._sound_on_not_found_barcode = @"C:\smlsoft\beep-29.wav";
                    __config._use_sound_amount = 1;

                    XmlSerializer __colXs = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                    TextWriter __memoryStream = new StreamWriter(__localPath.ToLower(), false, Encoding.UTF8);
                    __colXs.Serialize(__memoryStream, __config);
                    __memoryStream.Close();
                }
            }

        }



    }

    public enum _posControls
    {
        Panel,
        Button,
        Label,
        ItemsTable,
        ItemButtonPanel,
        Image,
        HTML,
        TextBox,
        SearchLevel
    }

    public enum _printSlipTextStyle
    {
        Left,
        Right,
        Center
    }

    [Serializable]
    public class _screenConfig
    {
        public string _moniter;
        public string _deviceName;
        public string _screen_code;
        public bool _isMasterScreen;

    }

    [Serializable]
    public class _posScreenConfig
    {
        //public string FormCode;
        [XmlArrayItem("screens", typeof(_screenConfig))]
        public ArrayList _screenConfig = new ArrayList();
        /// <summary>จอจ่ายเงิน</summary>
        public string _payform;
        /// <summary>รหัสเครื่อง POS</summary>
        public string _posid;
        /// <summary>ชนิดของ Slip</summary>
        public string _invoiceSlip;
        /// <summary>ชนิดเครื่องพิมพ์</summary>
        public string _printerType;
        /// <summary>วิธีการพิมพ์ 0 พิมพ์ผ่าน Windows Driver, 1 พิมพ์ตรง ,2 พิมพ์ผ่าน TCP/IP</summary>
        public int _print_method = 0;
        /// <summary>Port Printer</summary>
        public string _printerPort;
        /// <summary>ใช้ลิ้นชักเก็บเงิน</summary>
        public string _useCashDrawer;
        /// <summary>Port ลิ้นชักเก็บเงิน</summary>
        public string _cashDrawerPort;
        /// <summary>คำสั่งเปิดลิ้นชัก</summary>
        public string _drawerCodes;
        /// <summary>Printer Name</summary>
        public string _pos_printer_name;
        /// <summary>Font ส่วนหัว Slip</summary>
        public string _pos_slip_header_fontname;
        /// <summary>ขนาด Font ส่วนหัว Slip</summary>
        public float _pos_slip_header_fontsize;
        /// <summary>Font ส่วนรายละเอียด Slip</summary>
        public string _pos_slip_detail_fontname;
        /// <summary>ขนาด Font ส่วนรายละเอียด Slip</summary>
        public float _pos_slip_detail_fontsize;
        /// <summary>Font ส่วนท้าย Slip</summary>
        public string _pos_slip_footer_fontname;
        /// <summary>ขนาด Font ท้าย Slip</summary>
        public float _pos_slip_footer_fontsize;
        /// <summary>ความกว้างของกระดาษ</summary>
        public float _pos_slip_width;

        /// <summary>ใช้เสียงเมื่อมีการป้อน Barcode</summary>
        public int _use_sound_scan_barcode;
        /// <summary>เสียงเมื่อมีการป้อน Barcode</summary>
        public string _sound_on_scan_barcode;
        public int _use_sound_on_already_item;
        public string _sound_on_already_item;
        public int _use_sound_on_open_cash_drawer;
        public string _sound_on_open_cash_drawer;
        public int _use_sound_on_already_barcode;
        public string _sound_on_already_barcode;
        public int _use_sound_on_not_found_barcode;
        public string _sound_on_not_found_barcode;
        public int _use_sound_on_selected_member;
        public string _sound_on_selected_member;
        public int _use_sound_on_change_seller;
        public string _sound_on_change_seller;
        public int _use_sound_on_close_selling;
        public string _sound_on_close_selling;
        public int _use_sound_amount;

        public int full_invoice_print_type = 0;
        public String full_invoice_form_code = "";
        public String full_invoice_printer_name = "";
        public String _slip_form = "";

        public bool _android_customer_display= false;

        public bool _pos_slip_copy_print = false;
        public bool _printer_manual_feed = false;
        public int _pos_slip_copy_printNumber = 1;

        public Boolean _search_level_basket = false;

        public Boolean _pos_ask_before_print = false;
        public int _pos_delay_finish_screen = 5;
        public Boolean _manual_close_finish_screen = false;

        public Boolean _pos_no_print_barcode_slip = false;
        public Boolean _pos_search_minibox = false;
        public int _pos_search_result_items = 20;

        /// <summary>รูปแบบการปัดเศษ</summary>
        public int _round_type = -1;
        /// <summary>ตารางการปัดเศษ</summary>
        public string _round_table = "";

        public List<_roundTable> _round_table_list = new List<_roundTable>();

        public Boolean _use_customer_display = false;
        public string _customer_display_port = "";
        public string _customer_display_baudrate = "";
        public System.IO.Ports.Parity _customer_display_parity = System.IO.Ports.Parity.None;
        public string _customer_display_databits= "";
        public System.IO.Ports.StopBits _customer_display_stopbits = System.IO.Ports.StopBits.None;
        public int _customer_display_linecount = 20;

        public string _weight_scale_prefix = "";
        public int _weight_prefix_start;
        public int _weight_prefix_stop;
        public int _weight_ic_code_start;
        public int _weight_ic_code_stop;
        public int _weight_price_start;
        public int _weight_price_stop;

        public Boolean _no_print_slip = false;
        /// <summary>
        /// 0 รวมใน, 1 แยกนอก
        /// </summary>
        public int _pos_vat_type = 0;
        public decimal _service_charge_rate = 0M;
    }

    public class _roundTable
    {
        public decimal _fromValue = 0M;
        public decimal _toValue = 0M;
        public decimal _roundValue = 0M;

    }
}
