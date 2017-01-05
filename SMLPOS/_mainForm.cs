using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using MyLib;
using System.IO;
using SMLPosClient;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;

namespace SMLPOS
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public ArrayList _threadList = new ArrayList();
        Thread _orderTabletWork = null;
        Thread _tableCloseTableWork = null;
        Thread _valcummwork = null;
        Thread __dlSoundThread = null;
        DataTable _promotionFormula;
        DataTable _promotionFormulaCondition;
        DataTable _promotionFormulaPriceAndDiscount;
        DataTable _promotionFormulaAction;
        DataTable _promotionFormulaGroup;
        int _posConfigNumber = 0;
        String _posDefaultCust = "";
        /// <summary>
        /// เป็นเครื่อง process หรือเปล่า
        /// </summary>
        /// 
        AlertBox __alert;


        public _mainForm()
        {
            InitializeComponent();
            // toe switch icon
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite)
            {
                this.Icon = new Icon(GetType(), "shoppingbasket_full.ico");
            }

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOSMED)
            {
                this.Icon = new Icon(GetType(), "Drug-basket.ico");
            }

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro)
            {
                this.Icon = new Icon(GetType(), "Pork-Chop.ico");
            }

            if (MyLib._myGlobal._OEMVersion == "ais")
            {
                this.Icon = new Icon(GetType(), "mpayicon.ico");
            }

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLPOS && MyLib._myGlobal._programName.Equals("Sea And Hill POS"))
            {
                this.Icon = new Icon(GetType(), "SeaAndHillPOS_2.ico");
            }

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong && MyLib._myGlobal._programName.Equals("Sea And Hill Restaurant"))
            {
                this.Icon = new Icon(GetType(), "SeaAndHillRESTAURANT_2.ico");
            }

            this.Load += new EventHandler(_mainForm_Load);
            this.Resize += new EventHandler(_mainForm_Resize);

            this._mainMenu = this._mainMenuSMLPOS;

            this._programName = (MyLib._myGlobal._programName.Length == 0) ? "SML POS" : MyLib._myGlobal._programName;
            System.Version __myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime __myTime = new DateTime(2000, 1, 1).AddDays(__myVersion.Build).AddSeconds(__myVersion.Revision * 2);
            this._versionInfo = string.Format("Version : {0}  Build : {1:s}", __myVersion, __myTime);
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();

            this.Disposed += new EventHandler(_mainForm_Disposed);

            // โต๋ ย้ายไปใน _templateMainForm.cs
            this._manageTableForAutoUnlock();

            try
            {
                string __smlSoftPath = @"C:\smlsoft\";
                bool __isDirCreate = MyLib._myUtil._dirExists(__smlSoftPath);

                if (__isDirCreate == false)
                {
                    System.IO.Directory.CreateDirectory(__smlSoftPath); // create folders
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(@"Failed Create Folder C:\smlsoft" + ex.ToString(), "error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Failed Create C:\smlsoft" + ex.ToString(), "error");
            }

            this.FormClosed += _mainForm_FormClosed;

            __alert = new AlertBox(_alertMessage);
        }

        void _alertMessage(string Message, string Caption)
        {
            AutoClosingMessageBox.Show(Message, Caption, 5000);
        }

        void _mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _androidCustomerScreen._stop();
        }

        protected override void _checkTransectionProcess()
        {
            // start process
            SMLERPICInfo._icTransectionCheck._startProcess();

            // toe update doc_no_guid = '' เมื่อผ่านไปแล้ว 10 วัน
            MyLib._myFrameWork __fw = new _myFrameWork();
            string _updateDocGuid = "update ic_trans set doc_no_guid = '' where doc_date < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-7)) + "\' and doc_no_guid <> '' and doc_no_guid is not null";
            __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, _updateDocGuid);

            __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from order_checker where doc_date < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-7)) + "\' ");

            __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.barcode_import_list._table + " where " + _g.d.barcode_import_list._doc_date + " < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-60)) + "\' ");
            __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.barcode_import_list_detail._table + " where " + _g.d.barcode_import_list_detail._doc_date + " < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-60)) + "\' ");

        }

        void _killThreadProcess()
        {
            try
            {
                for (int __loop = 0; __loop < this._threadList.Count; __loop++)
                {
                    try
                    {
                        Thread __getThread = (Thread)this._threadList[__loop];
                        if (__getThread != null && __getThread.IsAlive)
                        {
                            __getThread.Abort();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            try
            {
                this._valcummwork.Abort();
            }
            catch
            {
            }

            try
            {
                __dlSoundThread.Abort();
            }
            catch
            {
            }
            try
            {
                _orderTabletWork.Abort();
            }
            catch
            {
            }

            try
            {
                _tableCloseTableWork.Abort();
            }
            catch
            {
            }
        }

        void _mainForm_Disposed(object sender, EventArgs e)
        {
            this._killThreadProcess();
        }

        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;

            // check is process
            bool _isProcessDevice = false;
            if (_g.g._companyProfile._process_serial_device.Length > 0)
            {
                MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                string[] _dataDive = Environment.GetLogicalDrives();


                for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                {
                    string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                    if (__getDeviceCode.Length > 0 && _g.g._companyProfile._process_serial_device.ToLower().IndexOf(__getDeviceCode) != -1)
                    {
                        _isProcessDevice = true;
                        break;
                    }
                }
            }
            else
            {
                _isProcessDevice = true;
            }

            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            // Process
            if (_isProcessDevice == true)
            {
                for (int __loop = 0; __loop < 3; __loop++)
                {
                    Thread __process = new Thread(new ThreadStart(_process));
                    __process.Name = "SML Thread " + __loop.ToString();
                    __process.IsBackground = true;
                    __process.Start();
                    this._threadList.Add(__process);
                }
            }

            if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro)
            {
                if (_isProcessDevice)
                {
                    this._orderTabletWork = new Thread(new ThreadStart(_processOrder));
                    this._orderTabletWork.Name = "SML Tablet ";
                    this._orderTabletWork.IsBackground = true;
                    this._orderTabletWork.Start();

                    this._tableCloseTableWork = new Thread(new ThreadStart(_processCloseTableWork));
                    this._tableCloseTableWork.Name = "SML Tablet Close Table ";
                    this._tableCloseTableWork.IsBackground = true;
                    this._tableCloseTableWork.Start();
                }
            }
            //
            //this._valcummwork = new Thread(new ThreadStart(_vacuum));
            //this._valcummwork.Start();

            // toe check pos sound
            this._checkPOSSound();

            // set default config for demo
            //this._demoDefaultConfig(); toe ไม่ต้องเขียน config แล้ว ดักที่ หน้าขายเอา

            this._loadMyMenu();

            // ย้ายไป SMLPOSControl._global
            SMLPOSControl._global._checkInitPosDesign();
            SMLPOSControl._global._checkInitConfig();

            _openWelcomeScreen();

            // ย้ายไปทำใน thread ตรวจสอบ สินค้า auto
            //// toe update doc_no_guid = '' เมื่อผ่านไปแล้ว 10 วัน
            //string _updateDocGuid = "update ic_trans set doc_no_guid = '' where doc_date < \'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now.AddDays(-10)) + "\' and doc_no_guid <> '' and doc_no_guid is not null";
            //__fw._query(MyLib._myGlobal._databaseName, _updateDocGuid);

            // toe order speech

            // check by serial hdd 

            if (_g.g._companyProfile._orders_speech)
            {
                this._myFrameWork = new MyLib._myFrameWork();

                // check hdd
                MyLib._getInfoStatus _getinfo = new MyLib._getInfoStatus();
                string[] _dataDive = Environment.GetLogicalDrives();

                StringBuilder __serialDriveList = new StringBuilder();
                for (int __loop = 0; __loop < _dataDive.Length; __loop++)
                {
                    string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                    if (__getDeviceCode.Length > 0)
                    {
                        if (__serialDriveList.Length > 0)
                        {
                            __serialDriveList.Append(",");
                        }

                        __serialDriveList.Append("\'" + __getDeviceCode.ToLower() + "\'");
                    }
                }

                // start check
                string __checkQuery = "select code from kitchen_master where lower(speech_serial) in (" + __serialDriveList.ToString() + ")";
                DataTable __kitchenSpeech = this._myFrameWork._queryShort(__checkQuery).Tables[0];
                for (int __loop = 0; __loop < __kitchenSpeech.Rows.Count; __loop++)
                {
                    if (_kitchenList.Length > 0)
                    {
                        _kitchenList.Append(",");
                    }
                    _kitchenList.Append("\'" + __kitchenSpeech.Rows[__loop][0].ToString() + "\'");
                }

                if (_kitchenList.Length > 0)
                {
                    _orderSpeechTimer.Start();
                }
            }

            if (_g.g._companyProfile._warning_reorder_point)
            {
                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                __process._checkICPurchasePointAlert();
            }

            if (_g.g._companyProfile._warning_product_expire)
            {
                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                __process._checkICExpireAlert();
            }
        }

        // toe
        public void _openWelcomeScreen()
        {
            //string __computerName = SystemInformation.ComputerName.ToLower();
            //if (__computerName.Equals("toe-pc"))
            //{


            if (MyLib._myGlobal._subVersion == 1 && MyLib._myGlobal._isVersionEnum != _myGlobal._versionType.SMLPOSMED) // version สุริวงศ์ ไม่ต้องมี
            {
                // check config open welcome screen
                Boolean __openWelcomeScreen = true;

                try
                {
                    if (File.Exists(MyLib._myGlobal._posConfigOpenWelcomeScreen))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(MyLib._myGlobal._posConfigOpenWelcomeScreen);
                        xDoc.DocumentElement.Normalize();

                        XmlElement __xRoot = xDoc.DocumentElement;

                        XmlNodeList __xReader = __xRoot.GetElementsByTagName("openDisplay");
                        if (__xReader.Count > 0)
                        {
                            XmlNode __xFirstNode = __xReader.Item(0);
                            if (__xFirstNode.NodeType == XmlNodeType.Element)
                            {
                                XmlElement __xTable = (XmlElement)__xFirstNode;
                                if (((int)MyLib._myGlobal._decimalPhase(__xTable.InnerText)) == 1)
                                {
                                    __openWelcomeScreen = false;
                                }
                            }
                        }
                    }

                }
                catch
                {
                }

                if (__openWelcomeScreen)
                {
                    // main shortcut
                    _mainShortcut __main = new _mainShortcut();
                    __main._menuButtonClick += (menuId, menuText, menuTag) =>
                    {
                        _activeMenu("menu_shortcut", menuText, menuTag);
                    };
                    _createAndSelectTab("Main", "Main", "menu_shortcut", __main);
                }
            }
            //}
        }

        /// <summary>เขียน demo config file</summary>
        /*
        public void _demoDefaultConfig()
        {
            if (MyLib._myGlobal._isDemo)
            {
                // check config
                // path in temp
                string __configFileName = "smlPOSScreenConfig" + MyLib._myGlobal._databaseName + ".xml";
                string __pathTemp = Path.GetTempPath() + "\\" + __configFileName.ToLower();

                // path in smlsoft
                string __localPath = string.Format(@"c:\smlsoft\smlPOSScreenConfig-{0}-{1}-{2}.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName);
                bool __fileInTemp = MyLib._myUtil._fileExists(__pathTemp);
                bool __fileInLocal = MyLib._myUtil._fileExists(__localPath);
                if (__fileInTemp == false && __fileInLocal == false)
                {
                    // gen new Config for demo
                    SMLPOSControl._posScreenConfig __config = new SMLPOSControl._posScreenConfig();

                    __config._posid = "DEMOCONFIG";

                    Screen __primary = Screen.PrimaryScreen;
                    string _screenDeviceName = Regex.Split(__primary.DeviceName, "\0")[0].ToString();

                    // add pos screen
                    SMLPOSControl._screenConfig __device = new SMLPOSControl._screenConfig() { _moniter = "0", _deviceName = MyLib._myUtil._convertTextToXml(_screenDeviceName) };
                    __device._isMasterScreen = true;
                    __device._screen_code = "S1";
                    __config._screenConfig.Add(__device);

                    // other config 
                    __config._pos_slip_header_fontname = "Tahoma";
                    __config._pos_slip_header_fontsize = 9;
                    __config._pos_slip_detail_fontname = "Tahoma";
                    __config._pos_slip_detail_fontsize = 9;
                    __config._pos_slip_footer_fontname = "Tahoma";
                    __config._pos_slip_footer_fontsize = 9;
                    __config._pos_slip_width = 579f;

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


                    string __smlSoftPath = @"C:\smlsoft\";
                    bool __isDirCreate = MyLib._myUtil._dirExists(__smlSoftPath);

                    if (__isDirCreate == false)
                    {
                        System.IO.Directory.CreateDirectory(__smlSoftPath); // create folders
                    }

                    XmlSerializer __colXs = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                    TextWriter __memoryStream = new StreamWriter(__localPath.ToLower(), false, Encoding.UTF8);
                    __colXs.Serialize(__memoryStream, __config);
                    __memoryStream.Close();

                }

                // config order machine for demo
                if (MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == _myGlobal._versionType.SMLTomYumGoongPro)
                {
                    string __localpath = string.Format(MyLib._myGlobal._smlConfigFile + "{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, SMLPOSControl._orderdeviceConfig._saveOrderDeviceConfigFileName);

                    if (File.Exists(__localpath) == false)
                    {
                        SMLPOSControl._orderDeviceXMLConfig __config = new SMLPOSControl._orderDeviceXMLConfig();
                        __config._device_id = "ORDERDEVICEDEMO";

                        // write config to file 

                        XmlSerializer __colXs = new XmlSerializer(typeof(SMLPOSControl._orderDeviceXMLConfig));
                        TextWriter __memoryStream = new StreamWriter(__localpath.ToLower(), false, Encoding.UTF8);
                        __colXs.Serialize(__memoryStream, __config);
                        __memoryStream.Close();
                    }
                }

            }
        }
        */

        public void _checkPOSSound()
        {
            string __smlSourndPath = @"C:\\smlsoft\\";
            if (MyLib._myUtil._dirExists(__smlSourndPath) == false || System.IO.File.Exists(__smlSourndPath + "beep-24.wav") == false)
            {
                //System.IO.Directory.CreateDirectory(__smlSourndPath);
                SMLPOSControl._posSoundDownload __dw = new SMLPOSControl._posSoundDownload();

                __dlSoundThread = new Thread(__dw._downloadSound);
                __dlSoundThread.Start();
            }

            if (MyLib._myUtil._dirExists(@"c:\smltemp\") == false)
            {
                System.IO.Directory.CreateDirectory(@"c:\smltemp\");
            }
        }

        /// <summary>
        /// ประมวลผลทุกหนึ่งวินาที
        /// </summary>
        private void _process()
        {
            while (true)
            {
                try
                {
                    SMLProcess._sendProcess._procesNow();
                }
                catch
                {
                }
                Thread.Sleep(MyLib._myGlobal._nextProcessTime * 1000);
            }
        }

        private void _processCloseTableWork()
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            while (true)
            {
                try
                {
                    DataTable __data = __myFrameWork._queryShort("select * from " + _g.d.pos_sync_device._table + " where " + _g.d.pos_sync_device._last_status + "=0").Tables[0];
                    if (__data.Rows.Count > 0)
                    {
                        string __dateQuery = MyLib._myGlobal._convertDateToQuery(DateTime.Now);

                        // get pos global config 
                        StringBuilder __queryList = new StringBuilder();
                        __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        string __query = "select * from " + _g.d.POSConfig._table;
                        if (this._posConfigNumber != 0)
                        {
                            __query = __query + " where " + _g.d.POSConfig._pos_config_number + "=" + this._posConfigNumber.ToString();
                        }
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula._table + " where " + _g.d.ic_promotion_formula._status + "=1 order by " + _g.d.ic_promotion_formula._order_number));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_condition._table + " order by " + _g.d.ic_promotion_formula_condition._code + "," + _g.d.ic_promotion_formula_condition._line_number));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_price_discount._table + " order by " + _g.d.ic_promotion_formula_price_discount._code + "," + _g.d.ic_promotion_formula_price_discount._line_number));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_action._table + " where " + _g.d.ic_promotion_formula_action._action_command + "<>'' order by " + _g.d.ic_promotion_formula_action._code + "," + _g.d.ic_promotion_formula_action._qty_from));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_group_qty._table + " where " + _g.d.ic_promotion_formula_group_qty._group_number + ">0 order by " + _g.d.ic_promotion_formula_group_qty._group_number));
                        // config แต้มสะสม
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.pos_point_period._amount + " from " + _g.d.pos_point_period._table + " where \'" + __dateQuery + "\' between " + _g.d.pos_point_period._from_date + " and " + _g.d.pos_point_period._to_date));
                        __queryList.Append("</node>");
                        ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());
                        DataTable __posConfig = ((DataSet)_getData[0]).Tables[0];
                        if (__posConfig.Rows.Count > 0)
                        {
                            this._posDefaultCust = __posConfig.Rows[0][_g.d.POSConfig._cust_code_default].ToString();
                        }
                        this._promotionFormula = ((DataSet)_getData[1]).Tables[0];
                        this._promotionFormulaCondition = ((DataSet)_getData[2]).Tables[0];
                        this._promotionFormulaPriceAndDiscount = ((DataSet)_getData[3]).Tables[0];
                        this._promotionFormulaAction = ((DataSet)_getData[4]).Tables[0];
                        this._promotionFormulaGroup = ((DataSet)_getData[5]).Tables[0];
                        //
                        string __guid = __data.Rows[0][_g.d.pos_sync_device._command_guid].ToString();
                        string __saleCode = __data.Rows[0][_g.d.pos_sync_device._sale_code].ToString();
                        string __tableNumber = __data.Rows[0][_g.d.pos_sync_device._table_number].ToString();
                        string __custCode = __data.Rows[0][_g.d.pos_sync_device._cust_code].ToString();
                        string __memberCode = __custCode;
                        string __foodDiscount = __data.Rows[0][_g.d.pos_sync_device._food_discount].ToString();
                        bool __isPromotion = (int.Parse(__data.Rows[0][_g.d.pos_sync_device._is_promotion].ToString()) == 0) ? false : true;
                        string __serviceCharge = __data.Rows[0][_g.d.pos_sync_device._service_charge].ToString();


                        //
                        List<_promotionResultClass> __promotionDiscountResult = new List<_promotionResultClass>();
                        List<_posClientItemClass> __itemProcess = new List<_posClientItemClass>();
                        List<_posClientItemClass> __itemList = new List<_posClientItemClass>();
                        decimal __totalQty = 0M;
                        decimal __totalFoodQty = 0M;
                        decimal __totalDrinkQty = 0M;

                        // load from Tablet
                        String __unitRatioWhere = " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._unit_code;
                        String __drinkType = "coalesce((select " + _g.d.ic_inventory._drink_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + "),0) ";
                        String __itemName = "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + "),\'\') as " + _g.d.table_order._item_name;
                        String __queryLoad = "select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order._barcode, __itemName, _g.d.table_order._item_code, _g.d.table_order._unit_code, _g.d.table_order._qty_balance, _g.d.table_order._price, "coalesce((select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + __unitRatioWhere + "),1) as stand_value,coalesce((select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + __unitRatioWhere + "),1) as divide_value," + __drinkType + " as " + " drink_type ") + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + __tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,2) and " + _g.d.table_order._qty_balance + ">0 order by " + __drinkType + ", " + _g.d.table_order._item_code;
                        DataTable __itemTable = __myFrameWork._queryShort(__queryLoad).Tables[0];
                        for (int __row = 0; __row < __itemTable.Rows.Count; __row++)
                        {
                            bool __found = false;
                            int __foundAddr = -1;
                            int __drink_type = (int)MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row]["drink_type"].ToString());
                            string __barCode = __itemTable.Rows[__row][_g.d.table_order._barcode].ToString();
                            decimal __qty = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row][_g.d.table_order._qty_balance].ToString());
                            string __itemCode = __itemTable.Rows[__row][_g.d.table_order._item_code].ToString();
                            string __unitCode = __itemTable.Rows[__row][_g.d.table_order._unit_code].ToString();
                            __totalQty += __qty;

                            if (__drink_type == 0)
                            {
                                __totalFoodQty += __qty;
                            }
                            else
                            {
                                __totalDrinkQty += __qty;
                            }

                            for (int __rowProcess = 0; __rowProcess < __itemList.Count; __rowProcess++)
                            {
                                if (__itemCode.Equals(__itemList[__rowProcess]._itemCode) && __unitCode.Equals(__itemList[__rowProcess]._unitCode))
                                {
                                    __found = true;
                                    __foundAddr = __rowProcess;
                                    __itemList[__rowProcess]._qty += __qty;
                                }
                            }

                            if (__found == false)
                            {
                                _posClientItemClass __itemData = new _posClientItemClass();
                                __itemData._barCode = __barCode;
                                __itemData._itemCode = __itemCode;
                                __itemData._itemName = __itemTable.Rows[__row][_g.d.table_order._item_name].ToString();
                                __itemData._qty = __qty;
                                __itemData._price = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row][_g.d.table_order._price].ToString());
                                __itemData._unitCode = __unitCode;
                                __itemData._standValue = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row]["stand_value"].ToString());
                                __itemData._divideValue = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row]["divide_value"].ToString());
                                __itemData._drink_type = __drink_type;
                                __itemList.Add(__itemData);
                            }
                        }

                        // toe
                        decimal __pointDivAmount = 0M;
                        decimal __pointBalance = 0M;

                        DataTable __pointTable = ((DataSet)_getData[6]).Tables[0];
                        if (__pointTable.Rows.Count > 0)
                        {
                            __pointDivAmount = MyLib._myGlobal._decimalPhase(__pointTable.Rows[0][_g.d.pos_point_period._amount].ToString());
                        }

                        if (__custCode.Length > 0 && __pointDivAmount != 0)
                        {
                            // หาแต้มมาใส่
                            SMLProcess._posProcess __process = new SMLProcess._posProcess();
                            __pointBalance = __process._pointBalance(__custCode);

                        }

                        try
                        {
                            // point โต๋ ใส่ 0 ไปก่อน
                            _processItem __process = new _processItem(__itemProcess, true, __itemList, __custCode, __memberCode, this._posDefaultCust, this._promotionFormula, this._promotionFormulaCondition, this._promotionFormulaAction, this._promotionFormulaGroup, __promotionDiscountResult, "#,0.00", false, 0, "price_type", __pointBalance, __pointDivAmount);

                            // service charge
                            __process._serviceChargeWord = __serviceCharge;

                            String __result1 = __process._start();
                            // ส่วนลดค่าอาหาร
                            if (__foodDiscount.Trim().Length > 0)
                            {
                                decimal __discountAmount = MyLib._myGlobal._calcAfterDiscount(__foodDiscount, __process._totalFoodAmount, 2);
                                __process._foodDiscountAmount = __process._totalFoodAmount - __discountAmount;
                                __process._totalAmount -= __process._foodDiscountAmount;
                            }

                            if (__serviceCharge.Trim().Length > 0)
                            {
                                __process._totalAmount += __process._serviceChargeAmount;
                            }
                            //
                            StringBuilder __result2 = new StringBuilder();
                            __result2.Append(MyLib._myGlobal._xmlHeader);
                            __result2.Append("<ResultSet>");
                            __result2.Append("<item>");
                            __result2.Append("<foodDiscountAmount>" + MyLib._myUtil._convertTextToXml(__process._foodDiscountAmount.ToString()) + "</foodDiscountAmount>");
                            __result2.Append("<discountAmount>" + MyLib._myUtil._convertTextToXml(__process._discountAmount.ToString()) + "</discountAmount>");
                            __result2.Append("<totalAmount>" + MyLib._myUtil._convertTextToXml(__process._totalAmount.ToString()) + "</totalAmount>");
                            __result2.Append("<finalBalance>" + MyLib._myUtil._convertTextToXml(__process._finalBalance.ToString()) + "</finalBalance>");
                            __result2.Append("<diffAmount>" + MyLib._myUtil._convertTextToXml(__process._diffAmount.ToString()) + "</diffAmount>");
                            __result2.Append("<totalAmountVat>" + MyLib._myUtil._convertTextToXml(__process._totalAmountVat.ToString()) + "</totalAmountVat>");
                            __result2.Append("<totalAmountExceptVat>" + MyLib._myUtil._convertTextToXml(__process._totalAmountExceptVat.ToString()) + "</totalAmountExceptVat>");
                            __result2.Append("<totalExtraDiscountAmount>" + MyLib._myUtil._convertTextToXml(__process._totalExtraDiscountAmount.ToString()) + "</totalExtraDiscountAmount>");
                            __result2.Append("<totalFoodAmount>" + MyLib._myUtil._convertTextToXml(__process._totalFoodAmount.ToString()) + "</totalFoodAmount>");
                            __result2.Append("<totalDrinkAmount>" + MyLib._myUtil._convertTextToXml(__process._totalDrinkAmount.ToString()) + "</totalDrinkAmount>");
                            __result2.Append("<totalQty>" + MyLib._myUtil._convertTextToXml(__totalQty.ToString()) + "</totalQty>");
                            __result2.Append("<totalDrinkQty>" + MyLib._myUtil._convertTextToXml(__totalDrinkQty.ToString()) + "</totalDrinkQty>");
                            __result2.Append("<totalFoodQty>" + MyLib._myUtil._convertTextToXml(__totalFoodQty.ToString()) + "</totalFoodQty>");
                            __result2.Append("<totalServiceCharge>" + MyLib._myUtil._convertTextToXml(__process._serviceChargeAmount.ToString()) + "</totalServiceCharge>");
                            __result2.Append("</item>");
                            __result2.Append("</ResultSet>");
                            // Save
                            string __base64Result1 = System.Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(__result1));
                            string __base64Result2 = System.Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(__result2.ToString()));
                            String __queryUpdate = "update " + _g.d.pos_sync_device._table + " set " + _g.d.pos_sync_device._last_status + "=1," + _g.d.pos_sync_device._result1 + "=\'" + __base64Result1 + "\'," + _g.d.pos_sync_device._result2 + "=\'" + __base64Result2 + "\' where " + _g.d.pos_sync_device._command_guid + "=\'" + __guid + "\'";
                            String __resultUpdate = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryUpdate);
                            if (__resultUpdate.Length > 0)
                            {
                                MessageBox.Show(__resultUpdate);
                            }
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString());
                        }
                    }
                    __data.Dispose();
                }
                catch
                {
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// ประมวลผลร้านอาหารทุก 1 วินาที เชื่อมกับ android
        /// </summary>
        private void _processOrder()
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            while (true)
            {
                /* toe แยก thread
                try
                {
                    DataTable __data = __myFrameWork._queryShort("select * from " + _g.d.pos_sync_device._table + " where " + _g.d.pos_sync_device._last_status + "=0").Tables[0];
                    if (__data.Rows.Count > 0)
                    {
                        string __dateQuery = MyLib._myGlobal._convertDateToQuery(DateTime.Now);

                        // get pos global config 
                        StringBuilder __queryList = new StringBuilder();
                        __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        string __query = "select * from " + _g.d.POSConfig._table;
                        if (this._posConfigNumber != 0)
                        {
                            __query = __query + " where " + _g.d.POSConfig._pos_config_number + "=" + this._posConfigNumber.ToString();
                        }
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula._table + " where " + _g.d.ic_promotion_formula._status + "=1 order by " + _g.d.ic_promotion_formula._order_number));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_condition._table + " order by " + _g.d.ic_promotion_formula_condition._code + "," + _g.d.ic_promotion_formula_condition._line_number));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_price_discount._table + " order by " + _g.d.ic_promotion_formula_price_discount._code + "," + _g.d.ic_promotion_formula_price_discount._line_number));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_action._table + " where " + _g.d.ic_promotion_formula_action._action_command + "<>'' order by " + _g.d.ic_promotion_formula_action._code + "," + _g.d.ic_promotion_formula_action._qty_from));
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_group_qty._table + " where " + _g.d.ic_promotion_formula_group_qty._group_number + ">0 order by " + _g.d.ic_promotion_formula_group_qty._group_number));
                        // config แต้มสะสม
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.pos_point_period._amount + " from " + _g.d.pos_point_period._table + " where \'" + __dateQuery + "\' between " + _g.d.pos_point_period._from_date + " and " + _g.d.pos_point_period._to_date));
                        __queryList.Append("</node>");
                        ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());
                        DataTable __posConfig = ((DataSet)_getData[0]).Tables[0];
                        if (__posConfig.Rows.Count > 0)
                        {
                            this._posDefaultCust = __posConfig.Rows[0][_g.d.POSConfig._cust_code_default].ToString();
                        }
                        this._promotionFormula = ((DataSet)_getData[1]).Tables[0];
                        this._promotionFormulaCondition = ((DataSet)_getData[2]).Tables[0];
                        this._promotionFormulaPriceAndDiscount = ((DataSet)_getData[3]).Tables[0];
                        this._promotionFormulaAction = ((DataSet)_getData[4]).Tables[0];
                        this._promotionFormulaGroup = ((DataSet)_getData[5]).Tables[0];
                        //
                        string __guid = __data.Rows[0][_g.d.pos_sync_device._command_guid].ToString();
                        string __saleCode = __data.Rows[0][_g.d.pos_sync_device._sale_code].ToString();
                        string __tableNumber = __data.Rows[0][_g.d.pos_sync_device._table_number].ToString();
                        string __custCode = __data.Rows[0][_g.d.pos_sync_device._cust_code].ToString();
                        string __memberCode = __custCode;
                        string __foodDiscount = __data.Rows[0][_g.d.pos_sync_device._food_discount].ToString();
                        bool __isPromotion = (int.Parse(__data.Rows[0][_g.d.pos_sync_device._is_promotion].ToString()) == 0) ? false : true;
                        //
                        List<_promotionResultClass> __promotionDiscountResult = new List<_promotionResultClass>();
                        List<_posClientItemClass> __itemProcess = new List<_posClientItemClass>();
                        List<_posClientItemClass> __itemList = new List<_posClientItemClass>();
                        // load from Tablet
                        String __unitRatioWhere = " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._unit_code;
                        String __drinkType = "coalesce((select " + _g.d.ic_inventory._drink_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + "),0) as " + "drink_type";
                        String __itemName = "coalesce((select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.table_order._table + "." + _g.d.table_order._item_code + "),\'\') as " + _g.d.table_order._item_name;
                        String __queryLoad = "select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order._barcode, __itemName, _g.d.table_order._item_code, _g.d.table_order._unit_code, _g.d.table_order._qty_balance, _g.d.table_order._price, "coalesce((select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + __unitRatioWhere + "),1) as stand_value,coalesce((select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + __unitRatioWhere + "),1) as divide_value," + __drinkType) + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + __tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,2) and " + _g.d.table_order._qty_balance + ">0 order by " + _g.d.table_order._line_number;
                        DataTable __itemTable = __myFrameWork._queryShort(__queryLoad).Tables[0];
                        for (int __row = 0; __row < __itemTable.Rows.Count; __row++)
                        {
                            _posClientItemClass __itemData = new _posClientItemClass();
                            __itemData._barCode = __itemTable.Rows[__row][_g.d.table_order._barcode].ToString();
                            __itemData._itemCode = __itemTable.Rows[__row][_g.d.table_order._item_code].ToString();
                            __itemData._itemName = __itemTable.Rows[__row][_g.d.table_order._item_name].ToString();
                            __itemData._qty = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row][_g.d.table_order._qty_balance].ToString());
                            __itemData._price = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row][_g.d.table_order._price].ToString());
                            __itemData._unitCode = __itemTable.Rows[__row][_g.d.table_order._unit_code].ToString();
                            __itemData._standValue = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row]["stand_value"].ToString());
                            __itemData._divideValue = MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row]["divide_value"].ToString());
                            __itemData._drink_type = (int)MyLib._myGlobal._decimalPhase(__itemTable.Rows[__row]["drink_type"].ToString());
                            __itemList.Add(__itemData);
                        }

                        // toe
                        decimal __pointDivAmount = 0M;
                        decimal __pointBalance = 0M;

                        DataTable __pointTable = ((DataSet)_getData[6]).Tables[0];
                        if (__pointTable.Rows.Count > 0)
                        {
                            __pointDivAmount = MyLib._myGlobal._decimalPhase(__pointTable.Rows[0][_g.d.pos_point_period._amount].ToString());
                        }

                        if (__custCode.Length > 0 && __pointDivAmount != 0)
                        {
                            // หาแต้มมาใส่
                            SMLProcess._posProcess __process = new SMLProcess._posProcess();
                            __pointBalance = __process._pointBalance(__custCode);

                        }

                        try
                        {
                            // point โต๋ ใส่ 0 ไปก่อน
                            _processItem __process = new _processItem(__itemProcess, true, __itemList, __custCode, __memberCode, this._posDefaultCust, this._promotionFormula, this._promotionFormulaCondition, this._promotionFormulaAction, this._promotionFormulaGroup, __promotionDiscountResult, "#,0.00", false, 0, "price_type", __pointBalance, __pointDivAmount);
                            String __result1 = __process._start();
                            // ส่วนลดค่าอาหาร
                            if (__foodDiscount.Trim().Length > 0)
                            {
                                decimal __discountAmount = MyLib._myGlobal._calcAfterDiscount(__foodDiscount, __process._totalFoodAmount, 2);
                                __process._foodDiscountAmount = __process._totalFoodAmount - __discountAmount;
                                __process._totalAmount -= __process._foodDiscountAmount;
                            }
                            //
                            StringBuilder __result2 = new StringBuilder();
                            __result2.Append(MyLib._myGlobal._xmlHeader);
                            __result2.Append("<ResultSet>");
                            __result2.Append("<item>");
                            __result2.Append("<foodDiscountAmount>" + MyLib._myUtil._convertTextToXml(__process._foodDiscountAmount.ToString()) + "</foodDiscountAmount>");
                            __result2.Append("<discountAmount>" + MyLib._myUtil._convertTextToXml(__process._discountAmount.ToString()) + "</discountAmount>");
                            __result2.Append("<totalAmount>" + MyLib._myUtil._convertTextToXml(__process._totalAmount.ToString()) + "</totalAmount>");
                            __result2.Append("<finalBalance>" + MyLib._myUtil._convertTextToXml(__process._finalBalance.ToString()) + "</finalBalance>");
                            __result2.Append("<diffAmount>" + MyLib._myUtil._convertTextToXml(__process._diffAmount.ToString()) + "</diffAmount>");
                            __result2.Append("<totalAmountVat>" + MyLib._myUtil._convertTextToXml(__process._totalAmountVat.ToString()) + "</totalAmountVat>");
                            __result2.Append("<totalAmountExceptVat>" + MyLib._myUtil._convertTextToXml(__process._totalAmountExceptVat.ToString()) + "</totalAmountExceptVat>");
                            __result2.Append("<totalExtraDiscountAmount>" + MyLib._myUtil._convertTextToXml(__process._totalExtraDiscountAmount.ToString()) + "</totalExtraDiscountAmount>");
                            __result2.Append("<totalFoodAmount>" + MyLib._myUtil._convertTextToXml(__process._totalFoodAmount.ToString()) + "</totalFoodAmount>");
                            __result2.Append("<totalDrinkAmount>" + MyLib._myUtil._convertTextToXml(__process._totalDrinkAmount.ToString()) + "</totalDrinkAmount>");
                            __result2.Append("</item>");
                            __result2.Append("</ResultSet>");
                            // Save
                            string __base64Result1 = System.Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(__result1));
                            string __base64Result2 = System.Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(__result2.ToString()));
                            String __queryUpdate = "update " + _g.d.pos_sync_device._table + " set " + _g.d.pos_sync_device._last_status + "=1," + _g.d.pos_sync_device._result1 + "=\'" + __base64Result1 + "\'," + _g.d.pos_sync_device._result2 + "=\'" + __base64Result2 + "\' where " + _g.d.pos_sync_device._command_guid + "=\'" + __guid + "\'";
                            String __resultUpdate = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryUpdate);
                            if (__resultUpdate.Length > 0)
                            {
                                MessageBox.Show(__resultUpdate);
                            }
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString());
                        }
                    }
                    __data.Dispose();
                }
                catch
                {
                } 
                 string __getDeviceCode = _getinfo.GetVolumeSerial((_dataDive[__loop].Replace(":\\", ""))).Trim().ToLower();
                                if (__getDeviceCode.Length > 0 && __device.IndexOf(__getDeviceCode) != -1)
                 */

                try
                {
                    // ตรวจสอบการพิมพ์ไปครัว
                    DataSet __dataSetResult = __myFrameWork._queryShort("select * from " + _g.d.kitchen_command._table);
                    if (__dataSetResult != null && __dataSetResult.Tables.Count > 0)
                    {
                        DataTable __data = __dataSetResult.Tables[0];

                        if (__data.Rows.Count > 0)
                        {
                            for (int __row = 0; __row < __data.Rows.Count; __row++)
                            {
                                string __rowOrder = __data.Rows[__row]["roworder"].ToString();
                                string __docNo = __data.Rows[__row][_g.d.kitchen_command._doc_no].ToString();
                                string __docDate = __data.Rows[__row][_g.d.kitchen_command._doc_date].ToString();
                                string __docTime = __data.Rows[__row][_g.d.kitchen_command._doc_time].ToString();
                                int __doc_type = (int)MyLib._myGlobal._decimalPhase(__data.Rows[__row][_g.d.kitchen_command._doc_type].ToString());

                                SMLPOSControl._kitchenPrintClass __kitchenPrint = new SMLPOSControl._kitchenPrintClass();
                                int __count = 0;
                                try
                                {
                                    __count++;
                                    if (__doc_type == 1)
                                    {
                                        // พิมพ์ใบยกเลิก
                                        __kitchenPrint._printCancelOrder(__docNo, "", "");
                                    }
                                    else if (__doc_type == 2)
                                    {
                                        __kitchenPrint._printCustomerTable(__docNo);
                                    }
                                    else if (__doc_type == 3)
                                    {
                                        // พิมพ์ order ซ้ำ
                                        __kitchenPrint._printOrder(__docNo, __docDate, __docTime, "พิมพ์ใหม่", __count);
                                    }
                                    else
                                    {
                                        // พิมพ์ order ปรกติ
                                        __kitchenPrint._printOrder(__docNo, __docDate, __docTime, __count);
                                    }

                                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.kitchen_command._table + " where roworder=" + __rowOrder);
                                }
                                catch (Exception __ex)
                                {
                                    //MessageBox.Show("ส่งพิมพ์ห้องครัวมีปัญหา" + " " + "เอกสารสั่งอาหารเลขที่" + " : " + __docNo);
                                    // mask kitchen is offline
                                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.kitchen_master._table + " set " + _g.d.kitchen_master._kitchen_ready + "=1 where  " + _g.d.kitchen_master._code + "=\'" + __kitchenPrint._lastPrintKitchenCode + "\' and " + _g.d.kitchen_master._kitchen_ready + "<>1 ");
                                    this.Invoke(__alert, new object[] { "ส่งพิมพ์ห้องครัวมีปัญหา" + " " + "เอกสารสั่งอาหารเลขที่" + " : " + __docNo + " : " + __ex.Message.ToString(), "ผิดพลาด" });
                                    Thread.Sleep(3000);
                                }
                            }
                        }
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
                Thread.Sleep(1000);

            }
        }

        private void _vacuum()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                if (__myFrameWork._databaseSelectType == _myGlobal._databaseType.PostgreSql)
                {
                    Thread.Sleep((1000 * 60) * 60);
                    string __query = "VACUUM FULL ANALYZE";
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
                }
            }
            catch
            {
            }
        }

        void _selectMenuFlow(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPControl._flow._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuAudit(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAudit._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuConfig(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPConfig._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuGl(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPGL._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuSo(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPSO._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPo(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPPO._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuAR(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAR._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuAsSet(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPASSET._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuICInfo(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPICInfo._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        private void _selectMenuAPReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAPReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        private void _selectMenuARReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPARReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        private void _selectMenuSOReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPSOReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuAP(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPAP._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuCashBank(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPCASHBANK._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuIC(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPIC._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        private void _selectMenuPOReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPPOReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuReportIC(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPICReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPOS(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLPOSControl._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuHealthyConfig(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLHealthyConfig._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuARHealthy(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLHealthyControl._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuPOSClient(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLPosClient._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuTransportLabel(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLTransportLabel._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuFood(string menunamem, string screenName)
        {

        }

        void _selectMenuCBReport(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPCASHBANKReport._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }



        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            if (MyLib._myGlobal._maxUserCurrent > MyLib._myGlobal._maxUser)
            {

                MessageBox.Show("Limit user please wait.");
                MyLib._myGlobal._registerProcess();

                // โต๋ เพิ่่ม กรณี เข้า server ลูกค้าแล้ว user เต็ม
                if (SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") == -1)
                {
                    return;
                }
            }
            // ตรวจสอบสิทธิ์
            MyLib._mainMenuClass __listmenu = new MyLib._mainMenuClass();
            __listmenu = MyLib._myGlobal._listMenuAll;
            bool __ischeckMainmenu = (mainMenuId.Equals(menuName)) ? true : false;
            string _mainMenuCode = "";
            // start
            if (__ischeckMainmenu == false)
            {
                string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
                _mainMenuCode = menuName;
                _PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(mainMenuId, _mainMenuCode);
                MyLib._myGlobal._mainMenuIdPassTrue = mainMenuId;
                MyLib._myGlobal._mainMenuCodePassTrue = _mainMenuCode;
                _g.g._companyProfileLoad();
                //
                if (__permission._isRead || MyLib._myGlobal._userCode.ToLower().Equals("superadmin"))
                {
                    if (tag.IndexOf("&fastreportother&") != -1)
                    {
                        //SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                        //__fastReport._loadReportOhter(menuName);
                        Control __getControl = SMLFastReport._selectMenu._getObject(menuName, menuName);
                        if (__getControl != null)
                        {
                            _createAndSelectTab(menuName, menuName, menuName, __getControl);
                        }
                    }
                    else
                        if (tag.IndexOf("&fastreport&") != -1)
                        {
                            SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                            __fastReport._load(menuName);
                            _createAndSelectTab(menuName, menuName, menuName, __fastReport);
                        }
                        else
                            if (tag.IndexOf("&transportlabel&") != -1)
                            {
                                //_createAndSelectTab(menuName, menuName, menuName, new SMLTransportLabel._transport_label(0));
                                _selectMenuTransportLabel(menuName, __screenName);

                            }
                            else
                                if (menuName.Equals("menu_welcome_page"))
                                {
                                    _mainShortcut __main = new _mainShortcut();
                                    __main._menuButtonClick += (menuId, menuText, menuTag) =>
                                    {
                                        _activeMenu("menu_shortcut", menuText, menuTag);
                                    };
                                    _createAndSelectTab("Main", "Main", "menu_shortcut", __main);

                                }
                                else
                                    if (menuName.Equals("data_backup"))
                                    {
                                        MyLib._databaseManage._dataBackupForm __form = new MyLib._databaseManage._dataBackupForm();
                                        __form.ShowDialog();
                                    }
                                    else if (menuName.Equals("data_restore"))
                                    {
                                        MyLib._databaseManage._dataRestoreForm __form = new MyLib._databaseManage._dataRestoreForm();
                                        __form.ShowDialog();

                                    }
                                    else
                                        if (menuName.Equals("menu_food_kitchen_fast"))
                                        {
                                            _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._food._kitchenFast());
                                        }
                                        else
                                            if (menuName.Equals("menu_food_kitchen"))
                                            {
                                                _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._food._kitchenManage());
                                            }
                                            else
                                                if (menuName.Equals("menu_food_master_table"))
                                                {
                                                    _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._table());
                                                }
                                                else
                                                    if (menuName.Equals("menu_fast_order"))
                                                    {
                                                        SMLPOSControl._food._orderControl __orderControl = new SMLPOSControl._food._orderControl(2);
                                                        __orderControl._afterCloseTableForPay += (s1, e1, sale, isCloseTable) =>
                                                        {
                                                            // call show pay bill
                                                            SMLPosClient._closeTableSummaryForm _summaryForm = new _closeTableSummaryForm(e1, sale, isCloseTable);
                                                            _summaryForm.ShowDialog();
                                                            _summaryForm.Dispose();
                                                            // ถาม บัตรสมาชิก 
                                                        };
                                                        _createAndSelectTab(menuName, menuName, menuName, __orderControl);
                                                    }
                                                    else
                                                        if (menuName.Equals("menu_food_master_remark"))
                                                        {
                                                            _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._masterRemark());
                                                        }
                                                        else
                                                            if (menuName.Equals("menu_chefmaster_list"))
                                                            {
                                                                _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._food._chefListControl());
                                                            }
                                                            else
                                                                if (menuName.Equals("menu_food_order"))
                                                                {
                                                                    //SMLPosClient._closeTableSummaryForm _summaryForm = new _closeTableSummaryForm(null, "toe");
                                                                    //_summaryForm.ShowDialog();
                                                                    Boolean __loginPass = true;
                                                                    string __user_code_login = "";
                                                                    string __user_name_login = "";
                                                                    if (_g.g._companyProfile._save_user_order == true)
                                                                    {
                                                                        // ถามพนักงาน แล้วจำ
                                                                        MyLib._erpUserLoginForm __loginForm = new MyLib._erpUserLoginForm();
                                                                        __loginForm.ShowDialog(MyLib._myGlobal._mainForm);
                                                                        __loginPass = __loginForm._isPassed;
                                                                        __user_code_login = __loginForm._userCode;
                                                                        __user_name_login = __loginForm._userName;
                                                                    }

                                                                    if (__loginPass)
                                                                    {

                                                                        SMLPOSControl._food._orderControl __orderControl = new SMLPOSControl._food._orderControl(0);
                                                                        __orderControl._afterCloseTableForPay += (s1, e1, sale, isCloseTable) =>
                                                                        {
                                                                            // call show pay bill
                                                                            SMLPosClient._closeTableSummaryForm _summaryForm = new _closeTableSummaryForm(e1, sale, isCloseTable);
                                                                            _summaryForm.ShowDialog();
                                                                            _summaryForm.Dispose();
                                                                            // ถาม บัตรสมาชิก 
                                                                        };

                                                                        if (_g.g._companyProfile._save_user_order)
                                                                        {
                                                                            __orderControl._user_code = __user_code_login;
                                                                            __orderControl._user_name = __user_name_login;
                                                                        }
                                                                        _createAndSelectTab(menuName, menuName, menuName, __orderControl);
                                                                    }

                                                                }
                                                                else
                                                                    if (menuName.Equals("menu_food_order_fast"))
                                                                    {
                                                                        SMLPOSControl._food._orderControl __orderControl = new SMLPOSControl._food._orderControl(1);
                                                                        __orderControl._afterCloseTableForPay += (s1, e1, sale, isCloseTable) =>
                                                                        {
                                                                            // call show pay bill
                                                                            SMLPosClient._closeTableSummaryForm _summaryForm = new _closeTableSummaryForm(e1, sale, isCloseTable);
                                                                            _summaryForm.ShowDialog();
                                                                            // ถาม บัตรสมาชิก 
                                                                        };
                                                                        _createAndSelectTab(menuName, menuName, menuName, __orderControl);
                                                                    }
                                                                    else
                                                                        if (menuName.Equals("menu_pos_point_rate"))
                                                                        {
                                                                            SMLPOSControl._posPointPeriodForm __form = new SMLPOSControl._posPointPeriodForm();
                                                                            __form.ShowDialog();
                                                                        }
                                                                        else
                                                                            if (menuName.Equals("menu_order_device_id"))
                                                                            {
                                                                                _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._orderDevice());
                                                                            }
                                                                            else
                                                                                if (menuName.Equals("menu_food_config_order_device"))
                                                                                {
                                                                                    MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, __screenName, new SMLPOSControl._orderdeviceConfig());
                                                                                }
                                                                                else
                                                                                    if (menuName.Equals("menu_food_config_checker_device"))
                                                                                    {
                                                                                        MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, __screenName, new SMLPOSControl._checkerDeviceConfig());
                                                                                    }
                                                                                    else
                                                                                        if (menuName.Equals("menu_food_checker"))
                                                                                        {
                                                                                            _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._checkerKitchen());
                                                                                        }
                                                                                        else
                                                                                            if (menuName.Equals("menu_mis_calendar_trans"))
                                                                                            {
                                                                                                _createAndSelectTab(menuName, menuName, menuName, new SMLMIS.calendar_trans());
                                                                                            }
                                                                                            else
                                                                                                if (menuName.Equals("fast_report_designer"))
                                                                                                {
                                                                                                    _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._designer());
                                                                                                }
                                                                                                else
                                                                                                    if (menuName.Equals("menu_form_edit"))
                                                                                                    {
                                                                                                        _createAndSelectTab(menuName, menuName, menuName, new SMLReport._formReport._formDesigner());
                                                                                                    }
                                                                                                    else
                                                                                                        if (menuName.Equals("menu_import_formdesign"))
                                                                                                        {
                                                                                                            _createAndSelectTab(menuName, menuName, menuName, new SMLReport._ImportFormDesign());
                                                                                                        }
                                                                                                        else if (menuName.Equals("menu_import_report"))
                                                                                                        {
                                                                                                            _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._importFastReport());
                                                                                                        }
                                                                                                        else if (menuName.Equals("menu_import_report_branch"))
                                                                                                        {
                                                                                                            _createAndSelectTab(menuName, menuName, menuName, new SMLFastReport._importFastReport() { mode = 1 });
                                                                                                        }
                                                                                                        else
                                                                                                            if (menuName.Equals("client_design"))
                                                                                                            {
                                                                                                                _createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._designer._clientDesign());
                                                                                                            }
                                                                                                            else
                                                                                                                if (menuName.Equals("menu_setup_staff_pic"))
                                                                                                                {
                                                                                                                    _createAndSelectTab(menuName, menuName, menuName, new SMLERPControl.erp_user._erp_userDetailpicture());
                                                                                                                }
                                                                                                                else
                                                                                                                    if (tag.IndexOf("&posclient&") != -1)
                                                                                                                    {
                                                                                                                        //_createAndSelectTab(menuName, menuName, menuName, new SMLPOSControl._designer._clientDesign());
                                                                                                                        //MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, __screenName, new SMLPosClient._configPOSScreen());
                                                                                                                        _selectMenuPOSClient(menuName, __screenName);
                                                                                                                    }
                                                                                                                    else
                                                                                                                        if (tag.IndexOf("&audit&") != -1)
                                                                                                                        {
                                                                                                                            _selectMenuAudit(menuName, __screenName);
                                                                                                                        }
                                                                                                                        else
                                                                                                                            if (tag.IndexOf("&flow&") != -1)
                                                                                                                            {
                                                                                                                                _selectMenuFlow(menuName, __screenName);
                                                                                                                            }
                                                                                                                            else
                                                                                                                                if (tag.IndexOf("&config&") != -1)
                                                                                                                                {
                                                                                                                                    _selectMenuConfig(menuName, __screenName);
                                                                                                                                }
                                                                                                                                else
                                                                                                                                    if (tag.IndexOf("&gl&") != -1)
                                                                                                                                    {
                                                                                                                                        _selectMenuGl(menuName, __screenName);
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                        if (tag.IndexOf("&so&") != -1)
                                                                                                                                        {
                                                                                                                                            _selectMenuSo(menuName, __screenName);
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                            if (tag.IndexOf("&po&") != -1)
                                                                                                                                            {
                                                                                                                                                _selectMenuPo(menuName, __screenName);
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                                if (tag.IndexOf("&ar&") != -1)
                                                                                                                                                {
                                                                                                                                                    _selectMenuAR(menuName, __screenName);
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                    if (tag.IndexOf("&poreport&") != -1)
                                                                                                                                                    {
                                                                                                                                                        _selectMenuPOReport(menuName, __screenName);
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                        if (tag.IndexOf("&ap&") != -1)
                                                                                                                                                        {
                                                                                                                                                            _selectMenuAP(menuName, __screenName);
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                            if (tag.IndexOf("&cashbank&") != -1)
                                                                                                                                                            {
                                                                                                                                                                _selectMenuCashBank(menuName, __screenName);
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                                if (tag.IndexOf("&ic&") != -1)
                                                                                                                                                                {
                                                                                                                                                                    _selectMenuIC(menuName, __screenName);
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                    if (tag.IndexOf("&apreport&") != -1)
                                                                                                                                                                    {
                                                                                                                                                                        _selectMenuAPReport(menuName, __screenName);
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                        if (tag.IndexOf("&arreport&") != -1)
                                                                                                                                                                        {
                                                                                                                                                                            _selectMenuARReport(menuName, __screenName);
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                            if (tag.IndexOf("&report&") != -1)
                                                                                                                                                                            {
                                                                                                                                                                                _selectMenuReport(menuName, __screenName);
                                                                                                                                                                            }
                                                                                                                                                                            else if (tag.IndexOf("&reportic&") != -1)
                                                                                                                                                                            {
                                                                                                                                                                                _selectMenuReportIC(menuName, __screenName);
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                                if (tag.IndexOf("&icinfo&") != -1)
                                                                                                                                                                                {
                                                                                                                                                                                    _selectMenuICInfo(menuName, __screenName);
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                    if (tag.IndexOf("&soreport&") != -1)
                                                                                                                                                                                    {
                                                                                                                                                                                        _selectMenuSOReport(menuName, __screenName);
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        if (tag.IndexOf("&as&") != -1)
                                                                                                                                                                                        {
                                                                                                                                                                                            _selectMenuAsSet(menuName, __screenName);
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                            if (tag.IndexOf("&pos&") != -1)
                                                                                                                                                                                            {
                                                                                                                                                                                                _selectMenuPOS(menuName, __screenName);
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                                if (tag.IndexOf("&healthyconfig&") != -1)
                                                                                                                                                                                                {
                                                                                                                                                                                                    _selectMenuHealthyConfig(menuName, __screenName);

                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                    if (tag.IndexOf("&ar_healthy&") != -1)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        _selectMenuARHealthy(menuName, __screenName);
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (tag.IndexOf("&cbreport&") != -1)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            _selectMenuCBReport(menuName, __screenName);
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            bool __selectTabFound = _selectTab(menuName);
                                                                                                                                                                                                            if (__selectTabFound == false)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Control __getControl = (new _g._selectMenu())._getObject(menuName, __screenName, _programName);
                                                                                                                                                                                                                if (__getControl != null)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    _createAndSelectTab(menuName, menuName, __screenName, __getControl);
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                    }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถเข้าเมนูนี้ได้") + __screenName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        void _mainMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private delegate void AlertBox(string Message, string Caption);

        class _speechListStuct
        {
            public string _itemName = "";
            public string _itemUnit = "";
            public float _itemQty = 0f;
        }

        MyLib._myFrameWork _myFrameWork;
        StringBuilder _kitchenList = new StringBuilder();

        private void _orderSpeechTimer_Tick(object sender, EventArgs e)
        {
            _orderSpeechTimer.Stop();
            this._processSpeech();
        }

        private void _processSpeech()
        {
            // พูดรายการอาหาร
            // play sound
            string _lastItemName = "";
            string _lastPlaySoune = "";
            try
            {

                // toe fix bug 
                /*if (this._config != null)
                {
                    for (int __loop = 0; __loop < this._config._kitchenCode.Count; __loop++)
                    {
                        if (__kitchenList.Length > 0)
                        {
                            __kitchenList.Append(",");
                        }
                        __kitchenList.Append("\'" + this._config._kitchenCode[__loop] + "\'");
                    }
                }*/

                if (_kitchenList.Length > 0)
                {
                    DateTime __date = DateTime.Now.AddDays(-2);
                    string __dateStr = __date.Year.ToString() + "-" + __date.Month.ToString() + "-" + __date.Day.ToString();

                    /*
                    List<String> __fieldList = new List<string>();
                    __fieldList.Add("item_code");
                    __fieldList.Add("table_number");
                    __fieldList.Add("unit_code");
                    __fieldList.Add("remark");
                    __fieldList.Add("price");
                    __fieldList.Add("qty_balance");
                    __fieldList.Add("doc_time");
                    __fieldList.Add("last_status");
                    __fieldList.Add("isspeech");
                    __fieldList.Add("kitchen_code");
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select roworderx,item_name||' '||remark as item_name,order_qty,unit_code from (");
                    __query.Append("select *,");
                    __query.Append("(select name_1 from ic_inventory where ic_inventory.code=item_code limit 1) as item_name ");
                    __query.Append(" from ");
                    __query.Append("(select *");
                    for (int __loop = 0; __loop < __fieldList.Count; __loop++)
                    {
                        __query.Append(",(select " + __fieldList[__loop] + " from table_order where table_order.guid_line=order_checker.guid_line) as " + __fieldList[__loop]);
                    }
                    __query.Append(",(select roworder from table_order where table_order.guid_line=order_checker.guid_line) as roworderx");
                    __query.Append(" from order_checker where doc_date > \'" + __dateStr + "\'");
                    __query.Append(" ) as t1");
                    // toe fix error ก่อน
                    __query.Append(" where last_status in (0,2) and (isspeech is null or isspeech=0) ");
                    if (_kitchenList.Length > 0)
                    {
                        __query.Append(" and kitchen_code in (" + _kitchenList.ToString() + ") ");
                    }                   
                    
                    __query.Append(") as t2");*/

                    StringBuilder __query = new StringBuilder();

                    __query.Append("select roworderx,item_name||' '||remark as item_name,order_qty,unit_code from ( ");

                    __query.Append("select roworder as roworderx, qty as order_qty, unit_code, isspeech, last_status, kitchen_code, remark ");
                    __query.Append(",(select name_1 from ic_inventory where ic_inventory.code=item_code limit 1) as item_name  ");
                    __query.Append(",(select is_speech from ic_inventory where ic_inventory.code=item_code limit 1) as item_is_speech  ");
                    __query.Append(" from table_order  where doc_date > \'" + __dateStr + "\' ");
                    if (_kitchenList.Length > 0)
                    {
                        __query.Append(" and kitchen_code in (" + _kitchenList.ToString() + ") ");
                    }
                    __query.Append(") as t2 ");
                    __query.Append(" where last_status in (0,2) and (isspeech is null or isspeech=0) ");
                    __query.Append(" and (item_is_speech = 1) ");

                    DataTable __data = this._myFrameWork._queryShort(__query.ToString()).Tables[0];
                    List<_speechListStuct> __speechList = new List<_speechListStuct>();
                    StringBuilder __rowOrder = new StringBuilder();
                    for (int __row = 0; __row < __data.Rows.Count; __row++)
                    {
                        if (__rowOrder.Length > 0)
                        {
                            __rowOrder.Append(",");
                        }
                        __rowOrder.Append(__data.Rows[__row]["roworderx"].ToString());
                        Boolean __found = false;
                        string __getItemName = __data.Rows[__row]["item_name"].ToString();
                        string __getUnitName = __data.Rows[__row]["unit_code"].ToString();
                        float __getQty = float.Parse(__data.Rows[__row]["order_qty"].ToString());
                        for (int __find = 0; __find < __speechList.Count; __find++)
                        {
                            if (__speechList[__find]._itemName.Equals(__getItemName) && __speechList[__find]._itemUnit.Equals(__getUnitName))
                            {
                                __speechList[__find]._itemQty = __speechList[__find]._itemQty + __getQty;
                                __found = true;
                                break;
                            }
                        }
                        if (__found == false)
                        {
                            _speechListStuct __newData = new _speechListStuct();
                            __newData._itemName = __getItemName;
                            __newData._itemUnit = __getUnitName;
                            __newData._itemQty = __getQty;
                            __speechList.Add(__newData);
                        }
                    }
                    for (int __loop = 0; __loop < __speechList.Count; __loop++)
                    {
                        _lastItemName = __speechList[__loop]._itemName;

                        if (__loop == 0)
                        {
                            _lastPlaySoune = "ออเด้อมาใหม่";
                            MyLib._googleSound._play("ออเด้อมาใหม่");
                        }

                        int __count = __loop + 1;
                        if (_g.g._companyProfile._orders_speech_format.Length > 0)
                        {
                            string[] __speechFormat = _g.g._companyProfile._orders_speech_format.Split(',');
                            foreach (string format in __speechFormat)
                            {
                                switch (format)
                                {
                                    case "count":
                                        _lastPlaySoune = __count.ToString();
                                        MyLib._googleSound._play(__count.ToString());
                                        break;
                                    case "name":
                                        _lastPlaySoune = __speechList[__loop]._itemName;
                                        MyLib._googleSound._play(__speechList[__loop]._itemName);
                                        break;
                                    case "qty":
                                        _lastPlaySoune = __speechList[__loop]._itemQty.ToString();
                                        MyLib._googleSound._play(__speechList[__loop]._itemQty.ToString());
                                        break;
                                    case "unit":
                                        _lastPlaySoune = __speechList[__loop]._itemUnit;
                                        MyLib._googleSound._play(__speechList[__loop]._itemUnit);
                                        break;
                                    case "qtyword":
                                        _lastPlaySoune = "จำนวน";
                                        MyLib._googleSound._play("จำนวน");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            _lastPlaySoune = __count.ToString();
                            MyLib._googleSound._play(__count.ToString());

                            _lastPlaySoune = __speechList[__loop]._itemName;
                            MyLib._googleSound._play(__speechList[__loop]._itemName);

                            if (__speechList[__loop]._itemQty > 1)
                            {
                                _lastPlaySoune = "จำนวน";
                                MyLib._googleSound._play("จำนวน");

                                _lastPlaySoune = __speechList[__loop]._itemQty.ToString();
                                MyLib._googleSound._play(__speechList[__loop]._itemQty.ToString());

                                _lastPlaySoune = __speechList[__loop]._itemUnit;
                                MyLib._googleSound._play(__speechList[__loop]._itemUnit);
                            }
                        }

                    }
                    if (__rowOrder.ToString().Trim().Length > 0)
                    {
                        // update
                        _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.table_order._table + " set " + _g.d.table_order._isspeech + "=1 where roworder in (" + __rowOrder.ToString() + ")");
                    }
                }
            }
            catch (Exception ex)
            {
                MyLib._myGlobal._writeEventLog("Unhandled exception Order Speech [" + _lastItemName + " -" + _lastPlaySoune + "] : " + ex.Message.ToString());
                // _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.table_order._table + " set " + _g.d.table_order._isspeech + "=1 where roworder in (" + __rowOrder.ToString() + ")");
            }

            _orderSpeechTimer.Start();
        }

    }


}
