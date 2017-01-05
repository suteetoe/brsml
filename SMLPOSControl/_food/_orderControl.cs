using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Threading;

namespace SMLPOSControl._food
{
    public partial class _orderControl : UserControl
    {
        class _userSaleOrder
        {
            public string _saleCode;
            public string _saleName;
            public Boolean _loginPass = false;
        }

        private string _tableNumberSelect = "";
        private string _tableNameSelect = "";

        private _orderDeviceXMLConfig __xmlConfig = null;
        private string _order_doc_format = "";
        private int _priceLevel = 0;
        private Boolean _disable_user_password = false;
        public _foodOrderControl _foodOrder;

        /// <summary>พิมพ์ใบสั่งอาหารจากส่วนกลาง</summary>
        private Boolean _print_order_center = false;
        private string _device_id = "";
        private int _mode = 0;
        private int _tableStatus = 0;
        public string _user_code = "";
        public string _user_name = "";

        // Thread _speechThread;

        string _beforeCloseTableScriptFileName = @"C:\\smlsoft\\beforeclosetablescriptURL.txt";

        //private _g._checkerDeviceXMLConfig _config;
        //MyLib._myFrameWork _myFrameWork;
        public _orderControl(int mode)
        {
            InitializeComponent();

            this._mode = mode;
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
            {
                this._confirmButton.Visible = true;
            }

            this._workPanelClear(true);

            _loadMaster();
            //
            if (mode == 1 || mode == 2)
            {
                this._topFlowLayoutPanel.Controls.Clear();
                this._tableStatus = 1;
                this._orderScreen("", "", "", "", "");

            }
            /*else if (mode == 0 && _g.g._companyProfile._orders_speech && _g.g._companyProfile._use_order_checker == true)
            {
                // พูดรายการอาหาร
                _myFrameWork = new MyLib._myFrameWork();

                this._loadConfig();
                //_speechThread = new Thread(new ThreadStart(_ordersSpeechWork));
                _orderSpeechTimer.Start();
            }*/
        }

        /*class _speechListStuct
        {
            public string _itemName = "";
            public string _itemUnit = "";
            public float _itemQty = 0f;
        }*/

        /*void _loadConfig()
        {
            try
            {
                string __localpath = MyLib._myGlobal._smlConfigFile + _g.g._checkerXmlFileName;
                TextReader __readFile = new StreamReader(__localpath);
                XmlSerializer __xsLoad = new XmlSerializer(typeof(_g._checkerDeviceXMLConfig));
                this._config = (_g._checkerDeviceXMLConfig)__xsLoad.Deserialize(__readFile);
                __readFile.Close();
            }
            catch
            {
            }
        }*/

        private void _loadMaster()
        {
            string __configFilePath = string.Format(MyLib._myGlobal._smlConfigFile + "{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _orderdeviceConfig._saveOrderDeviceConfigFileName);

            try
            {
                TextReader readFile = new StreamReader(__configFilePath.ToLower());
                XmlSerializer __xsLoad = new XmlSerializer(typeof(_orderDeviceXMLConfig));
                __xmlConfig = (_orderDeviceXMLConfig)__xsLoad.Deserialize(readFile);
                readFile.Close();
            }
            catch
            {
            }

            if (__xmlConfig != null)
            {
                if (__xmlConfig._device_id.Equals("ORDERDEVICEDEMO"))
                {
                    _order_doc_format = "ORDER-YYMMDD####";
                    this._priceLevel = 1;
                    this._device_id = __xmlConfig._device_id;
                }
                else
                {
                    string __query = "select * from " + _g.d.order_device_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.order_device_id._device_id) + "=\'" + __xmlConfig._device_id + "\'";

                    MyLib._myFrameWork __fw = new MyLib._myFrameWork();

                    DataTable __dt = __fw._queryShort(__query).Tables[0];

                    if (__dt.Rows.Count > 0)
                    {
                        _order_doc_format = __dt.Rows[0][_g.d.order_device_id._doc_format_order].ToString();
                        this._priceLevel = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.order_device_id._price_number].ToString());
                        this._device_id = __xmlConfig._device_id;
                        this._disable_user_password = __dt.Rows[0][_g.d.order_device_id._device_disable_password].ToString().Equals("1") ? true : false;
                        this._print_order_center = __dt.Rows[0][_g.d.order_device_id._print_from_center].ToString().Equals("1") ? true : false;
                    }
                }
            }
        }

        private void _workPanelClear(Boolean showLogo)
        {
            if (this._mode == 0)
            {
                this._workPanel.Controls.Clear();
            }
            else
            {
                if (this._foodOrder != null)
                {
                    this._foodOrder._itemGrid._clear();
                }
            }
        }

        /*_userSaleOrder _getSaleOrder()
        {
            _userSaleOrder _sale = new _userSaleOrder();

            return _sale;
        }*/

        /// <summary>
        /// เปิดโต๊ะ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _openTableButton_Click(object sender, EventArgs e)
        {
            _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
            __selectSale._disable_user_password = this._disable_user_password;

            Form __saleSaleForm = new Form();
            __saleSaleForm.WindowState = FormWindowState.Maximized;
            __selectSale.Dock = DockStyle.Fill;
            __selectSale._selectSaleClick += (s2, e2) =>
            {
                _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                __saleSaleForm.Close();
                // เปิดโต๊ะใหม่
                this._workPanelClear(false);
                _tableSearchLevelControl __tableSelect = new _tableSearchLevelControl();
                __tableSelect.Dock = DockStyle.Fill;
                __tableSelect._menuTableClick += (s1, e1) =>
                {
                    _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;
                    switch (__table._status)
                    {
                        case 0:
                            {
                                // ถามจำนวนคน
                                int __customer_count = 0;

                                if (_g.g._companyProfile._count_customer_table)
                                {
                                    _tableOpenControl __openControl = new _tableOpenControl();
                                    __openControl.ShowDialog();

                                    if (__openControl.DialogResult == DialogResult.OK)
                                    {
                                        __customer_count = MyLib._myGlobal._intPhase(__openControl._customerCountTextbox.Text);
                                    }
                                }

                                this._tableNumberSelect = __table._tableNumber;
                                this._tableNameSelect = __table._tableName;
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                StringBuilder __myQuery = new StringBuilder();
                                IFormatProvider __cultureEng = new CultureInfo("en-US");
                                string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                string __transGuid = Guid.NewGuid().ToString();

                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=1," + _g.d.table_master._trans_guid + "=\'" + __transGuid + "\'," + _g.d.table_master._open_date + "=\'" + __date + "\'," + _g.d.table_master._open_time + "=\'" + __time + "\' where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + this._tableNumberSelect.ToUpper() + "\'"));
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_trans._table + " (" + _g.d.table_trans._trans_guid + "," + _g.d.table_trans._table_number + "," + _g.d.table_trans._open_doc_date + "," + _g.d.table_trans._open_doc_time + "," + _g.d.table_trans._open_sale_code + "," + _g.d.table_trans._customer_count + ") values (\'" + __transGuid + "\',\'" + this._tableNumberSelect.ToUpper() + "\',\'" + __date + "\',\'" + __time + "\',\'" + __sale._saleCode + "\',\'" + __customer_count + "\')"));

                                // log
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + "," + _g.d.table_order_log._sale_code + ") values (now(),\'open\',\'Table Number : " + this._tableNumberSelect + ", trans_guid : " + __transGuid + "\', \'" + __sale._saleCode + "\')"));

                                __myQuery.Append("</node>");

                                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                if (__result.Length == 0)
                                {
                                    MessageBox.Show("เปิดโต๊ะหมายเลข" + " : " + this._tableNumberSelect + "/" + this._tableNameSelect + "สำเร็จ");

                                    if (__customer_count > 0)
                                    {
                                        if (this._print_order_center)
                                        {

                                            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_command._table + "(" + _g.d.kitchen_command._doc_no + "," + _g.d.kitchen_command._doc_type + ") values (\'" + this._tableNumberSelect + "\',\'2\')"));
                                            __query.Append("</node>");

                                            __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                                            if (__result.Length > 0)
                                            {
                                                MessageBox.Show(__result);
                                            }
                                        }
                                        else
                                        {
                                            _kitchenPrintClass __printOrder = new _kitchenPrintClass();
                                            __printOrder._printCustomerTable(this._tableNumberSelect.ToUpper());
                                        }
                                    }

                                    this._workPanelClear(true);

                                    __tableSelect.Dispose();


                                    if (_g.g._companyProfile._order_after_open_table == true)
                                    {
                                        __table._transGuidNumber = __transGuid;
                                        __table._status = 1;
                                        this._tableStatus = 1;
                                        this._orderScreen(__sale._saleCode, __sale._saleName, __table._tableNumber, __table._tableName, __table._transGuidNumber);

                                    }
                                }
                                else
                                {
                                    MessageBox.Show(__result.ToString());
                                }

                            };
                            break;
                        case 1:
                            MessageBox.Show("โต๊ะนี้เปิดไปแล้ว");
                            break;
                    }
                };
                this._workPanel.Controls.Add(__tableSelect);
            };
            __saleSaleForm.Controls.Add(__selectSale);

            if (_g.g._companyProfile._save_user_order)
            {
                __selectSale._saleSelect(this._user_code, this._user_name);
            }
            else
            {
                __saleSaleForm.ShowDialog();
                __selectSale.Dispose();
                __saleSaleForm.Dispose();
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void _orderScreen(string saleCode, string saleName, string tableNumber, string tableName, string tableTransGuidNumber)
        {
            // สั่งอาหาร
            switch (this._tableStatus)
            {
                case 0:
                    MessageBox.Show("โต๊ะนี้ยังไม่เปิด");
                    break;
                case 1:
                    {
                        this._workPanelClear(false);
                        this._tableNumberSelect = tableNumber;
                        this._tableNameSelect = tableName;
                        if (_foodOrder == null)
                        {
                            _foodOrder = new _foodOrderControl(this._mode, saleCode, saleName, this._tableNumberSelect, this._tableNameSelect, this._device_id, this._order_doc_format, tableTransGuidNumber, this._priceLevel, this._print_order_center);
                            _foodOrder._closeButton.Click -= _closeButton_Click;
                            _foodOrder._closeButton.Click += _closeButton_Click;
                        }
                        else
                        {
                            _foodOrder._loadControl(saleCode, saleName, this._tableNumberSelect, this._tableNameSelect, this._device_id, this._order_doc_format, tableTransGuidNumber, this._priceLevel);
                        }
                        _foodOrder._processComplete += () =>
                        {
                            this._workPanelClear(true);
                        };
                        _foodOrder.Dock = DockStyle.Fill;
                        this._workPanel.Controls.Add(_foodOrder);
                        if (this._mode == 1)
                        {
                            _foodOrder._afterCloseTableForPay -= _foodOrder__afterCloseTableForPay;
                            _foodOrder._afterCloseTableForPay += _foodOrder__afterCloseTableForPay;
                            //_foodOrder._afterCloseTableForPay += (s1, e1, sale, isCloseTable) =>
                            //{
                            //    if (this._afterCloseTableForPay != null)
                            //    {
                            //        this._afterCloseTableForPay(s1, e1, sale, isCloseTable);
                            //    }
                            //};
                        }
                    };
                    break;
            }
        }

        void _foodOrder__afterCloseTableForPay(object sender, _tableSearchLevelMenuControl table, string _saleCode, bool isCloseTable)
        {
            if (this._afterCloseTableForPay != null)
            {
                this._afterCloseTableForPay(sender, table, _saleCode, isCloseTable);
            }
        }

        private void _orderButton_Click(object sender, EventArgs e)
        {
            _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
            __selectSale._disable_user_password = this._disable_user_password;

            Form __saleSaleForm = new Form();
            __saleSaleForm.WindowState = FormWindowState.Maximized;
            __selectSale.Dock = DockStyle.Fill;
            __selectSale._selectSaleClick += (s2, e2) =>
            {
                this._workPanelClear(false);
                _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                __saleSaleForm.Close();
                _tableSearchLevelControl __tableSelect = new _tableSearchLevelControl();
                __tableSelect.Dock = DockStyle.Fill;
                __tableSelect._menuTableClick += (s1, e1) =>
                {
                    _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;
                    this._tableStatus = __table._status;

                    if (this._tableStatus == 1)
                        __tableSelect.Dispose();

                    this._orderScreen(__sale._saleCode, __sale._saleName, __table._tableNumber, __table._tableName, __table._transGuidNumber);
                };
                this._workPanel.Controls.Add(__tableSelect);
            };
            __saleSaleForm.Controls.Add(__selectSale);
            if (_g.g._companyProfile._save_user_order)
            {
                __selectSale._saleSelect(this._user_code, this._user_name);
            }
            else
            {
                __saleSaleForm.ShowDialog();
                __selectSale.Dispose();
                __saleSaleForm.Dispose();
            }
        }

        /// <summary>
        /// สถานะโต๊ะ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _tableButton_Click(object sender, EventArgs e)
        {
            // สถานะโต๊ะ
            this._workPanelClear(false);
            _tableInfoControl __tableInfo = new _tableInfoControl();
            __tableInfo._print_from_center = this._print_order_center;
            __tableInfo._printBillButton.Click += (s1, e1) =>
            {
                _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
                __selectSale._disable_user_password = this._disable_user_password;

                Form __saleSaleForm = new Form();
                __saleSaleForm.WindowState = FormWindowState.Maximized;
                __selectSale.Dock = DockStyle.Fill;
                __selectSale._selectSaleClick += (s2, e2) =>
                {
                    _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                    __saleSaleForm.Close();
                    __saleSaleForm.Dispose();
                    // start pos screen dialog
                    if (_afterCloseTableForPay != null)
                    {
                        if (__tableInfo._table != null)
                        {
                            _afterCloseTableForPay(this, __tableInfo._table, __sale._saleCode, false);
                        }
                    }
                };
                __saleSaleForm.Controls.Add(__selectSale);
                __saleSaleForm.ShowDialog();
            };
            __tableInfo.Dock = DockStyle.Fill;
            this._workPanel.Controls.Add(__tableInfo);
        }

        public event _afterCloseTable _afterCloseTableForPay;

        /// <summary>
        /// ปิดโต๊ะ  รอคิดเงิน table_order.last_status=2 , ถ้าไม่มีรายการ ถามว่าต้องการ ปิดโต๊ะ นี้ไปหรือไม่
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _tableCloseButton_Click(object sender, EventArgs e)
        {
            _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
            __selectSale._disable_user_password = this._disable_user_password;

            Form __saleSaleForm = new Form();
            __saleSaleForm.WindowState = FormWindowState.Maximized;
            __selectSale.Dock = DockStyle.Fill;
            __selectSale._selectSaleClick += (s2, e2) =>
            {
                _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                __saleSaleForm.Close();
                // ปิดโต๊ะ
                this._workPanelClear(false);
                _tableSearchLevelControl __tableSelect = new _tableSearchLevelControl();
                __tableSelect.Dock = DockStyle.Fill;
                __tableSelect._menuTableClick += (s1, e1) =>
                {
                    _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;
                    switch (__table._status)
                    {
                        case 0:
                            MessageBox.Show("โต๊ะนี้ยังไม่เปิด");
                            break;
                        case 1:
                            {
                                if (MessageBox.Show("ยืนยันการปิดโต๊ะ", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {

                                    // before close table
                                    if (System.IO.File.Exists(this._beforeCloseTableScriptFileName))
                                    {
                                        // call script by web
                                        // 1. get text url in file
                                        // 2. call by web
                                        string __closeTableUrl = System.IO.File.ReadAllLines(this._beforeCloseTableScriptFileName)[0];

                                        DateTime __now = DateTime.Now;
                                        CultureInfo __culture = new CultureInfo("en-US");
                                        __closeTableUrl = __closeTableUrl.Replace("#table#", __table._barcode.Replace(" ", "%20")).Replace("#dayclose#", __now.ToString("dd", __culture)).Replace("#monthclose#", __now.ToString("MM", __culture)).Replace("#yearclose#", __now.ToString("yyyy", __culture)).Replace("#dateclose#", __now.ToString("yyyy-MM-dd", __culture));

                                        WebRequest webRequest = WebRequest.Create(__closeTableUrl);
                                        WebResponse webResp = webRequest.GetResponse();
                                    }

                                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                    // check order ในโต๊ะ 
                                    //string _debug_q = "select count(" + _g.d.table_order._item_code + ") as countItem from " + _g.d.table_order._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + __table._tableNumber.ToUpper(); // +" and " + _g.d.table_order._last_status + "=0";
                                    DataTable _orderTableRow = __myFrameWork._queryShort("select count(" + _g.d.table_order._item_code + ") as countItem, sum(" + _g.d.table_order._qty_balance + ") as sum_balance from " + _g.d.table_order._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + __table._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0 ").Tables[0];
                                    if (_orderTableRow.Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(_orderTableRow.Rows[0]["countItem"].ToString()) > 0) && (MyLib._myGlobal._decimalPhase(_orderTableRow.Rows[0]["sum_balance"].ToString()) > 0))
                                    {
                                        // ปิดโต๊ะ อัพเดทสถานะ รอคิดเงิน
                                        StringBuilder __myQuery = new StringBuilder();
                                        IFormatProvider __cultureEng = new CultureInfo("en-US");
                                        string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                        string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + __table._tableNumber.ToUpper() + "\'"));
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "=\'" + __date + "\'," + _g.d.table_trans._close_doc_time + "=\'" + __time + "\'," + _g.d.table_trans._close_sale_code + "=\'" + __sale._saleCode + "\' where " + _g.d.table_trans._trans_guid + " = \'" + __table._transGuidNumber + "\'"));
                                        // , " + _g.d.table_trans._cust_code + "=\'" + + "\' where 
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._last_status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + __table._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0"));// โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order_doc._table + " set " + _g.d.table_order_doc._last_status + "=2 where " + MyLib._myGlobal._addUpper(_g.d.table_order_doc._table_number) + " = \'" + __table._tableNumber.ToUpper() + "\' and " + _g.d.table_order_doc._last_status + " =0")); // โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                        // toe log
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + "," + _g.d.table_order_log._sale_code + ") values (now(),\'Close For Pay\',\'Table Number : " + __table._tableNumber + ", trans_guid : " + __table._transGuidNumber + "\',\'" + __sale._saleCode + "\')"));

                                        __myQuery.Append("</node>");

                                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                        if (__result.Length == 0)
                                        {
                                            MessageBox.Show("ปิดโต๊ะหมายเลข" + " : " + __table._tableNumber + "/" + __table._tableName + " สำเร็จ");

                                            // start pos screen dialog
                                            if (_afterCloseTableForPay != null)
                                            {
                                                _afterCloseTableForPay(this, __table, __sale._saleCode, true);
                                            }

                                            this._workPanelClear(true);

                                            __tableSelect.Dispose();

                                        }
                                        else
                                        {
                                            MessageBox.Show(__result.ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("ไม่มีรายการอาหารในโต๊ะ " + __table._tableNumber + "/" + __table._tableName + " คุณต้องการที่จะปิดโต๊ะ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                        {
                                            // โต๊ะว่าง อัพเดทสถานะ ว่าง
                                            StringBuilder __myQuery = new StringBuilder();
                                            IFormatProvider __cultureEng = new CultureInfo("en-US");
                                            string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                            string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                            // อัพเดท ผู้ปิดโต๊ะ 
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "=\'" + __date + "\'," + _g.d.table_trans._close_doc_time + "=\'" + __time + "\'," + _g.d.table_trans._close_sale_code + "=\'" + __sale._saleCode + "\' where " + _g.d.table_trans._trans_guid + " = \'" + __table._transGuidNumber + "\'"));
                                            // เปลี่ยนสถานะโต๊ะ เป็น ว่าง, trans_guid = ''
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=0, " + _g.d.table_order._trans_guid + "=\'\' where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + __table._tableNumber.ToUpper() + "\'"));
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._last_status + "=3 where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + __table._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=0 "));

                                            // toe 
                                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + "," + _g.d.table_order_log._sale_code + ") values (now(),\'Close\',\'Table Number : " + __table._tableNumber + ", trans_guid : " + __table._transGuidNumber + "\', \'" + __sale._saleCode + "\')"));

                                            __myQuery.Append("</node>");

                                            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                            if (__result.Length == 0)
                                            {
                                                MessageBox.Show("ปิดโต๊ะหมายเลข" + " : " + __table._tableNumber + "/" + __table._tableName + " สำเร็จ");
                                                this._workPanelClear(true);

                                                __tableSelect.Dispose();

                                            }
                                            else
                                            {
                                                MessageBox.Show(__result.ToString());
                                            }
                                        }
                                    }

                                }
                                this._workPanelClear(true);

                                __tableSelect.Dispose();

                            };
                            break;
                    }
                };
                this._workPanel.Controls.Add(__tableSelect);
            };
            __saleSaleForm.Controls.Add(__selectSale);
            if (_g.g._companyProfile._save_user_order)
            {
                __selectSale._saleSelect(this._user_code, this._user_name);
            }
            else
            {
                __saleSaleForm.ShowDialog();
                __selectSale.Dispose();
                __saleSaleForm.Dispose();
            }
        }

        /// <summary>
        /// ย้ายโต๊ะ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void _moveTableButton_Click(object sender, EventArgs e)
        {
            _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
            __selectSale._disable_user_password = this._disable_user_password;

            Form __saleSaleForm = new Form();
            __saleSaleForm.WindowState = FormWindowState.Maximized;
            __selectSale.Dock = DockStyle.Fill;
            __selectSale._selectSaleClick += (s2, e2) =>
            {
                _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                __saleSaleForm.Close();

                this._workPanelClear(false);
                _tableMoveControl __moveControl = new _tableMoveControl(); // this._device_id, this._order_doc_format
                __moveControl._saleCode = __sale._saleCode;
                __moveControl.Dock = DockStyle.Fill;
                this._workPanel.Controls.Add(__moveControl);
                __moveControl._cancelButton.Click += (s1, e1) =>
                {
                    this._workPanelClear(true);
                };
                __moveControl._saveSuccess += (s1) =>
                {
                    this._workPanelClear(true);
                };
            };
            __saleSaleForm.Controls.Add(__selectSale);
            if (_g.g._companyProfile._save_user_order)
            {
                __selectSale._saleSelect(this._user_code, this._user_name);
            }
            else
            {
                __saleSaleForm.ShowDialog();
            }

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// คืน ยกเลิก รายการสั่งอาหาร
        /// </summary>
        private void _cancelOrderButton_Click(object sender, EventArgs e)
        {
            if (_g.g._companyProfile._disable_edit_order == false)
            {
                _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
                __selectSale._disable_user_password = this._disable_user_password;

                Form __saleSaleForm = new Form();
                __saleSaleForm.WindowState = FormWindowState.Maximized;
                __selectSale.Dock = DockStyle.Fill;
                __selectSale._selectSaleClick += (s2, e2) =>
                {
                    _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                    __saleSaleForm.Close();
                    // สั่งอาหาร
                    this._workPanelClear(false);
                    _tableSearchLevelControl __tableSelect = new _tableSearchLevelControl();
                    __tableSelect.Dock = DockStyle.Fill;
                    __tableSelect._menuTableClick += (s1, e1) =>
                    {
                        _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;
                        switch (__table._status)
                        {
                            case 0:
                                MessageBox.Show("โต๊ะนี้ยังไม่เปิด");
                                break;
                            case 1:
                                {
                                    this._workPanelClear(false);
                                    this._tableNumberSelect = __table._tableNumber;
                                    this._tableNameSelect = __table._tableName;
                                    _orderCancelControl __foodOrderCancel = new _orderCancelControl(0, __sale._saleCode, __sale._saleName, this._tableNumberSelect, this._tableNameSelect, this._device_id, this._order_doc_format, __table, this._priceLevel, this._print_order_center);
                                    __foodOrderCancel._processComplete += () =>
                                    {
                                        this._workPanelClear(true);
                                        __tableSelect.Dispose();

                                    };
                                    __foodOrderCancel.Dock = DockStyle.Fill;
                                    this._workPanel.Controls.Add(__foodOrderCancel);
                                };
                                break;
                        }
                    };
                    this._workPanel.Controls.Add(__tableSelect);
                };
                __saleSaleForm.Controls.Add(__selectSale);
                if (_g.g._companyProfile._save_user_order)
                {
                    __selectSale._saleSelect(this._user_code, this._user_name);
                }
                else
                {
                    __saleSaleForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("ห้ามยกเลิกรายการอาหาร", "ห้าม", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// ยกเลิก ปิดโต๊ะ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _tableReOpenButton_Click(object sender, EventArgs e)
        {
            _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
            __selectSale._disable_user_password = this._disable_user_password;

            Form __saleSaleForm = new Form();
            __saleSaleForm.WindowState = FormWindowState.Maximized;
            __selectSale.Dock = DockStyle.Fill;
            __selectSale._selectSaleClick += (s2, e2) =>
            {
                _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                __saleSaleForm.Close();
                // ปิดโต๊ะ
                this._workPanelClear(false);
                _tableSearchLevelControl __tableSelect = new _tableSearchLevelControl();
                __tableSelect.Dock = DockStyle.Fill;
                __tableSelect._menuTableClick += (s1, e1) =>
                {
                    _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;
                    switch (__table._status)
                    {
                        case 0:
                            MessageBox.Show("โต๊ะนี้ยังไม่เปิด");
                            break;
                        case 1:
                            MessageBox.Show("โต๊ะนี้ยังไม่ปิด");
                            break;
                        case 2:
                            {
                                if (MessageBox.Show("ยืนยันการยกเลิกปิดโต๊ะ", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                    //// check order ในโต๊ะ 
                                    //string _debug_q = "select count(" + _g.d.table_order._item_code + ") as countItem from " + _g.d.table_order._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + __table._tableNumber; // +" and " + _g.d.table_order._last_status + "=0";
                                    //DataTable _orderTableRow = __myFrameWork._queryShort("select count(" + _g.d.table_order._item_code + ") as countItem from " + _g.d.table_order._table + " where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + __table._tableNumber + "\' and " + _g.d.table_order._last_status + "=0 ").Tables[0];
                                    //if (_orderTableRow.Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(_orderTableRow.Rows[0]["countItem"].ToString()) > 0))
                                    //{
                                    // ปิดโต๊ะ อัพเดทสถานะ รอคิดเงิน
                                    StringBuilder __myQuery = new StringBuilder();
                                    IFormatProvider __cultureEng = new CultureInfo("en-US");
                                    string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                    string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=1 where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + __table._tableNumber.ToUpper() + "\'"));
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "= null," + _g.d.table_trans._close_doc_time + "=null," + _g.d.table_trans._close_sale_code + "=null, " + _g.d.table_trans._cust_code + "=null, " + _g.d.table_trans._food_discount + "=null, " + _g.d.table_trans._total_discount + "=null, " + _g.d.table_trans._ispromotion_process + "=null where " + _g.d.table_trans._trans_guid + " = \'" + __table._transGuidNumber + "\'"));
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order._table + " set " + _g.d.table_order._last_status + "=0 where " + MyLib._myGlobal._addUpper(_g.d.table_order._table_number) + " = \'" + __table._tableNumber.ToUpper() + "\' and " + _g.d.table_order._last_status + "=2"));// โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_order_doc._table + " set " + _g.d.table_order_doc._last_status + "=0 where " + MyLib._myGlobal._addUpper(_g.d.table_order_doc._table_number) + " = \'" + __table._tableNumber.ToUpper() + "\' and " + _g.d.table_order_doc._last_status + " =2")); // โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0

                                    // toe log
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.table_order_log._table + " (" + _g.d.table_order_log._date_time + "," + _g.d.table_order_log._command1 + "," + _g.d.table_order_log._message + "," + _g.d.table_order_log._sale_code + ") values (now(),\'Re Open\',\'Table Number : " + __table._tableNumber + ", trans_guid : " + __table._transGuidNumber + "\',\'" + __sale._saleCode + "\')")); // โต๋ แก้ไข จาก where trans_guid เป็น last_status = 0
                                    __myQuery.Append("</node>");

                                    string __debuq_query = __myQuery.ToString();

                                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                    if (__result.Length == 0)
                                    {
                                        MessageBox.Show("ยกเลิกปิดโต๊ะหมายเลข" + " : " + __table._tableNumber + "/" + __table._tableName + " สำเร็จ");
                                        this._workPanelClear(true);

                                        __tableSelect.Dispose();

                                    }
                                    else
                                    {
                                        MessageBox.Show(__result.ToString());
                                    }
                                    //}
                                    //else
                                    //{
                                    //    if (MessageBox.Show("ไม่มีรายการอาหารในโต๊ะ " + __table._tableNumber + "/" + __table._tableName + " คุณต้องการที่จะปิดโต๊ะ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                    //    {
                                    //        // โต๊ะว่าง อัพเดทสถานะ ว่าง
                                    //        StringBuilder __myQuery = new StringBuilder();
                                    //        IFormatProvider __cultureEng = new CultureInfo("en-US");
                                    //        string __date = DateTime.Now.ToString("yyyy-MM-dd", __cultureEng);
                                    //        string __time = DateTime.Now.ToString("HH:mm", __cultureEng);
                                    //        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                    //        // อัพเดท ผู้ปิดโต๊ะ 
                                    //        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_trans._table + " set " + _g.d.table_trans._close_doc_date + "=\'" + __date + "\'," + _g.d.table_trans._close_doc_time + "=\'" + __time + "\'," + _g.d.table_trans._close_sale_code + "=\'" + __sale._saleCode + "\' where " + _g.d.table_trans._trans_guid + " = \'" + __table._transGuidNumber + "\'"));
                                    //        // เปลี่ยนสถานะโต๊ะ เป็น ว่าง, trans_guid = ''
                                    //        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.table_master._table + " set " + _g.d.table_master._status + "=0, " + _g.d.table_order._trans_guid + "=\'\' where " + MyLib._myGlobal._addUpper(_g.d.table_master._number) + " = \'" + __table._tableNumber + "\'"));
                                    //        __myQuery.Append("</node>");

                                    //        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                    //        if (__result.Length == 0)
                                    //        {
                                    //            MessageBox.Show("ปิดโต๊ะหมายเลข" + " : " + __table._tableNumber + "/" + __table._tableName + " สำเร็จ");
                                    //            this._workPanelClear(true);
                                    //        }
                                    //        else
                                    //        {
                                    //            MessageBox.Show(__result.ToString());
                                    //        }
                                    //    }
                                    //}

                                }
                                this._workPanelClear(true);

                                __tableSelect.Dispose();

                            };
                            break;
                    }
                };
                this._workPanel.Controls.Add(__tableSelect);
            };
            __saleSaleForm.Controls.Add(__selectSale);
            if (_g.g._companyProfile._save_user_order)
            {
                __selectSale._saleSelect(this._user_code, this._user_name);
            }
            else
            {
                __saleSaleForm.ShowDialog();
            }
        }


        /// <summary>
        /// แก้ไขรายการ order 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _editOrderButton_Click(object sender, EventArgs e)
        {
            if (_g.g._companyProfile._disable_edit_order == false)
            {
                _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
                __selectSale._disable_user_password = this._disable_user_password;

                Form __saleSaleForm = new Form();
                __saleSaleForm.WindowState = FormWindowState.Maximized;
                __selectSale.Dock = DockStyle.Fill;
                __selectSale._selectSaleClick += (s2, e2) =>
                {
                    _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                    __saleSaleForm.Close();
                    // สั่งอาหาร
                    this._workPanelClear(false);
                    _tableSearchLevelControl __tableSelect = new _tableSearchLevelControl();
                    __tableSelect.Dock = DockStyle.Fill;
                    __tableSelect._menuTableClick += (s1, e1) =>
                    {
                        _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;
                        switch (__table._status)
                        {
                            case 0:
                                MessageBox.Show("โต๊ะนี้ยังไม่เปิด");
                                break;
                            case 1:
                                {
                                    this._workPanelClear(false);
                                    this._tableNumberSelect = __table._tableNumber;
                                    this._tableNameSelect = __table._tableName;

                                    _tableOrderEditControl __foodOrderEditControl = new _tableOrderEditControl(__sale._saleCode, __sale._saleName, this._tableNumberSelect, this._tableNameSelect, this._device_id, this._order_doc_format, __table, this._priceLevel);
                                    __foodOrderEditControl._processComplete += () =>
                                    {
                                        this._workPanelClear(true);
                                        __tableSelect.Dispose();

                                    };
                                    __foodOrderEditControl.Dock = DockStyle.Fill;
                                    this._workPanel.Controls.Add(__foodOrderEditControl);

                                };
                                break;
                        }
                    };
                    this._workPanel.Controls.Add(__tableSelect);
                };
                __saleSaleForm.Controls.Add(__selectSale);
                if (_g.g._companyProfile._save_user_order)
                {
                    __selectSale._saleSelect(this._user_code, this._user_name);
                }
                else
                {
                    __saleSaleForm.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("ห้ามแก้ไขรายการอาหาร", "ห้าม", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void _reservedTableButton_Click(object sender, EventArgs e)
        {

        }

        private void _reservedTableCancelButton_Click(object sender, EventArgs e)
        {

        }

        // confirm
        private void _confirmButton_Click(object sender, EventArgs e)
        {
            _food._selectSaleUserControl __selectSale = new _selectSaleUserControl();
            __selectSale._disable_user_password = this._disable_user_password;

            Form __saleSaleForm = new Form();
            __saleSaleForm.WindowState = FormWindowState.Maximized;
            __selectSale.Dock = DockStyle.Fill;
            __selectSale._selectSaleClick += (s2, e2) =>
            {

                _selectSaleButtonControl __sale = (_selectSaleButtonControl)s2;
                __saleSaleForm.Close();
                // สั่งอาหาร
                this._workPanelClear(false);
                _tableSearchLevelControl __tableSelect = new _tableSearchLevelControl();
                __tableSelect.Dock = DockStyle.Fill;
                __tableSelect._menuTableClick += (s1, e1) =>
                {
                    _tableSearchLevelMenuControl __table = (_tableSearchLevelMenuControl)s1;
                    switch (__table._status)
                    {
                        case 0:
                            MessageBox.Show("โต๊ะนี้ยังไม่เปิด");
                            break;
                        case 1:
                            {
                                this._workPanelClear(false);
                                this._tableNumberSelect = __table._tableNumber;
                                this._tableNameSelect = __table._tableName;
                                /*
                                if (__foodOrder == null)
                                {
                                    __foodOrder = new _foodOrderControl(__sale._saleCode, __sale._saleName, this._tableNumberSelect, this._tableNameSelect, this._device_id, this._order_doc_format, __table, this._priceLevel, this._print_order_center);
                                }
                                else
                                {
                                    __foodOrder._loadControl(__sale._saleCode, __sale._saleName, this._tableNumberSelect, this._tableNameSelect, this._device_id, this._order_doc_format, __table, this._priceLevel);
                                }
                                __foodOrder._processComplete += () =>
                                {
                                    this._workPanelClear(true);
                                };
                                __foodOrder.Dock = DockStyle.Fill;
                                this._workPanel.Controls.Add(__foodOrder);*/
                                _tableConfirmControl __tableConfirmControl = new _tableConfirmControl(__sale._saleCode, __sale._saleName, this._tableNumberSelect, this._tableNameSelect, this._device_id, this._order_doc_format, __table, this._priceLevel);
                                __tableConfirmControl._processComplete += () =>
                                {
                                    this._workPanelClear(true);
                                    __tableSelect.Dispose();

                                };

                                __tableConfirmControl.Dock = DockStyle.Fill;
                                this._workPanel.Controls.Add(__tableConfirmControl);

                            };
                            break;
                    }
                };
                this._workPanel.Controls.Add(__tableSelect);
            };
            __saleSaleForm.Controls.Add(__selectSale);
            __saleSaleForm.ShowDialog();
        }

        public void _loadTable(string saleCode, string saleName, string tableNumber, string tableName, string tableTransGuidNumber)
        {
            this._orderScreen(saleCode, saleName, tableNumber, tableName, tableTransGuidNumber);
        }

        /*private void _orderSpeechTimer_Tick(object sender, EventArgs e)
        {
            // พูดรายการอาหาร
            // play sound
            try
            {
                StringBuilder __kitchenList = new StringBuilder();
                // toe fix bug 
                if (this._config != null)
                {
                    for (int __loop = 0; __loop < this._config._kitchenCode.Count; __loop++)
                    {
                        if (__kitchenList.Length > 0)
                        {
                            __kitchenList.Append(",");
                        }
                        __kitchenList.Append("\'" + this._config._kitchenCode[__loop] + "\'");
                    }
                }

                if (__kitchenList.Length > 0)
                {
                    DateTime __date = DateTime.Now.AddDays(-2);
                    string __dateStr = __date.Year.ToString() + "-" + __date.Month.ToString() + "-" + __date.Day.ToString();
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
                    if (__kitchenList.Length > 0)
                    {
                        __query.Append(" and kitchen_code in (" + __kitchenList.ToString() + ") ");
                    }
                    __query.Append(") as t2");
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
                        if (__loop == 0)
                        {
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
                                        MyLib._googleSound._play(__count.ToString());
                                        break;
                                    case "name":
                                        MyLib._googleSound._play(__speechList[__loop]._itemName);
                                        break;
                                    case "qty":
                                        MyLib._googleSound._play(__speechList[__loop]._itemQty.ToString());
                                        break;
                                    case "unit":
                                        MyLib._googleSound._play(__speechList[__loop]._itemUnit);
                                        break;
                                    case "qtyword":
                                        MyLib._googleSound._play("จำนวน");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            MyLib._googleSound._play(__count.ToString());
                            MyLib._googleSound._play(__speechList[__loop]._itemName);
                            if (__speechList[__loop]._itemQty > 1)
                            {
                                MyLib._googleSound._play("จำนวน");
                                MyLib._googleSound._play(__speechList[__loop]._itemQty.ToString());
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
            catch
            {
            }
        }*/
    }

    public delegate void _afterCloseTable(object sender, _tableSearchLevelMenuControl table, string _saleCode, bool isCloseTable);
}
