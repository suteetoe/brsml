using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;
using System.Drawing.Printing;

namespace SMLPosClient
{
    public partial class _configPOSScreen : Form
    {
        private const string _resourcePayForm = "payform";

        string _searchName = "";
        TextBox _searchTextBox;

        Screen[] _displayDevice = null;
        //DataTable _posData = null;
        string _savePosScreenConfigFileName = "smlPOSScreenConfig";
        string _moniterStr = "moniter";
        private MyLib._searchDataFull _searchItem;

        public _configPOSScreen()
        {
            InitializeComponent();
            //this._screenMoniter._maxColumn = 1;
            this.Load += new EventHandler(_configPOSScreen_Load);

            //int __row = 0;
            _displayDevice = Screen.AllScreens;
            //int __i;
            //for (__i = 0; __i < _displayDevice.Length; __i++)
            //{
            //    Screen __src = _displayDevice[__i];
            //    string __devieName = string.Format("จอแสดงผล {0} ({1}x{2})", __i, __src.Bounds.Width, __src.Bounds.Height);
            //    this._screenMoniter._addComboBox(__row++, 1, _moniterStr + __i.ToString(), true, new string[] { "" }, false, __devieName);
            //}

            //// pay form
            ////this._screenMoniter._addComboBox(__row++, 0, _resourcePayForm, true, new string[] { "" }, false, "จอรับเงิน");
            //this._screenMoniter.Invalidate();
            ////this._screenMoniter._maxLabelWidth = new int[] { 160 };


            this._posClientGeneralConfig._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenMoniter__textBoxChanged);
            this._posClientGeneralConfig._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenMoniter__textBoxSearch);
            this._posClientGeneralConfig._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_screenMoniter__comboBoxSelectIndexChanged);
        }

        void _screenMoniter__comboBoxSelectIndexChanged(object sender, string name)
        {
            try
            {
                //throw new NotImplementedException();
                MyLib._myComboBox __comboBox = (MyLib._myComboBox)sender;
                if (__comboBox._name.Equals(_g.d.POSConfig._pos_type_slip))
                {
                    if (__comboBox._selectedIndex == 2)
                    {
                        _enablePrinterSetting(true);
                        _enabledSlipFormCode(false);
                    }
                    else if (__comboBox._selectedIndex == 3)
                    {
                        _enablePrinterSetting(true);
                        _enabledSlipFormCode(true);
                    }
                    else
                    {
                        _enablePrinterSetting(false);
                        _enabledSlipFormCode(false);
                    }
                }
                else if (__comboBox._name.Equals(_g.d.POSConfig._pos_cash_drawer))
                {
                    if (__comboBox._selectedIndex == 2)
                    {
                        _enableDrawCash(true);
                    }
                    else
                        _enableDrawCash(false);
                }
            }
            catch
            {
            }
        }

        private bool _checkIsPrimaryScreen(string __deviceName)
        {
            foreach (Screen __scr in _displayDevice)
            {
                if (__scr.DeviceName.Equals(__deviceName))
                {
                    return __scr.Primary;
                }
            }
            return false;
        }

        void _screenMoniter__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = __getControl._name;
            string label_name = __getControl._labelName;
            //string _search_text_new = _search_screen_name(this._searchName);

            if (_searchName.Equals(_g.d.POSConfig._pos_terminal_id))
            {
                // start search pos id dialog
                this._searchItem = new MyLib._searchDataFull();
                this._searchItem.Text = MyLib._myGlobal._resource("ค้นหาลูกค้า");
                this._searchItem._dataList._loadViewFormat("screen_pos_select_pos_id", MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchItem.WindowState = FormWindowState.Maximized;
                this._searchItem._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string __arCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.POS_ID._table + "." + _g.d.POS_ID._MACHINECODE).ToString();
                    this._searchItem.Close();
                    this._searchItem.Dispose();
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input");
                    _posClientGeneralConfig._setDataStr(_searchName, __arCode);
                };
                this._searchItem._searchEnterKeyPress += (s1, e1) =>
                {
                    string __arCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.POS_ID._table + "." + _g.d.POS_ID._MACHINECODE).ToString();
                    this._searchItem.Close();
                    this._searchItem.Dispose();
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input");
                    _posClientGeneralConfig._setDataStr(_searchName, __arCode);
                };
                this._searchItem.ShowDialog();

                //_searchItem.ShowDialog();
            }
        }

        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            string __setdata = "";

            if (__setdata.Length > 0)
            {
                _posClientGeneralConfig._setDataStr(_searchName, __setdata);
            }
            this._searchItem.Close();
            this._searchItem.Dispose();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __setdata = "";

            if (__setdata.Length > 0)
            {
                _posClientGeneralConfig._setDataStr(_searchName, __setdata);
            }
            this._searchItem.Close();
            this._searchItem.Dispose();
        }

        void _screenMoniter__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.POSConfig._pos_terminal_id))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        public void _search(Boolean warning)
        {
            string __custCode = _posClientGeneralConfig._getDataStr(_searchName);

            string __query = "";
            string __searchName = "";

            if (_searchName.Equals(_g.d.POSConfig._pos_terminal_id))
            {
                __searchName = _searchName;
                __query = "select " + _g.d.POS_ID._POS_ID + " from " + _g.d.POS_ID._table + " where " + MyLib._myGlobal._addUpper(_g.d.POS_ID._MACHINECODE) + " = '" + __custCode.ToUpper() + "'";
            }

            if (!__query.Equals("") && !__searchName.Equals(""))
            {
                try
                {
                    _searchAndWarning(__searchName, __query, warning);
                }
                catch
                {
                }
            }

        }

        bool _searchAndWarning(string fieldName, string query, Boolean warning)
        {
            bool __result = false;
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = _posClientGeneralConfig._getDataStr(fieldName);
                _posClientGeneralConfig._setDataStr(fieldName, getDataStr, getData, true);
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && _screenMoniter._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + _posClientGeneralConfig._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        /// <summary>
        /// อ่าน config ที่ save ไว้ 
        /// </summary>
        public void _loadConfig()
        {
            string __configFileName = _savePosScreenConfigFileName + MyLib._myGlobal._databaseName + ".xml";
            string __path = Path.GetTempPath() + "\\" + __configFileName.ToLower();

            //string __localpath = @"c:\\smlsoft" + "\\" + __configFileName.ToLower();
            string __localpath = string.Format(@"c:\\smlsoft\\{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _savePosScreenConfigFileName);

            SMLPOSControl._posScreenConfig __config = null;

            try
            {
                // ลองอ่านจาก location ใหม่ (c:\smlsoft\)

                TextReader readFile = new StreamReader(__localpath.ToLower());
                XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                readFile.Close();
            }
            catch
            {
                // ถ้าไม่ได้ให้ไปอ่านที่เก่า
                try
                {
                    TextReader readFile = new StreamReader(__path.ToLower());
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                    __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch (Exception ex)
                {
                }
            }
            if (__config != null)
            {

                // โหลด config tag general

                this._posClientGeneralConfig._setDataStr(_g.d.POSConfig._pos_terminal_id, __config._posid);

                this._posClientGeneralConfig._setComboBox(_g.d.POSConfig._pos_type_slip, (int)MyLib._myGlobal._decimalPhase(__config._invoiceSlip));
                this._posClientGeneralConfig._setDataStr(_g.d.POSConfig._slip_code, __config._slip_form);
                this._posClientGeneralConfig._setComboBox(_g.d.POSConfig._pos_type_printer, (int)MyLib._myGlobal._decimalPhase(__config._printerType));
                this._posClientGeneralConfig._setComboBox(_g.d.POSConfig._pos_print_method, __config._print_method);
                if (__config._printerPort != null) this._posClientGeneralConfig._setDataStr(_g.d.POSConfig._pos_port_printer, __config._printerPort);
                if (__config._pos_printer_name != null)
                {
                    MyLib._myComboBox __printerCombo = (MyLib._myComboBox)this._posClientGeneralConfig._getControl(_g.d.POSConfig._pos_printer_name);
                    __printerCombo.SelectedItem = __config._pos_printer_name;
                }
                this._posClientGeneralConfig._setCheckBox(_g.d.POSConfig._pos_slip_print_copy, ((__config._pos_slip_copy_print) ? "1" : "0")); // set copy print
                this._posClientGeneralConfig._setCheckBox(_g.d.POSConfig._printer_manual_feed, ((__config._printer_manual_feed) ? "1" : "0")); // ใช้กระดาษแบบฉีก
                this._posClientGeneralConfig._setDataNumber(_g.d.POSConfig._pos_slip_print_copy_number, __config._pos_slip_copy_printNumber); // num of copy

                this._posClientGeneralConfig._setComboBox(_g.d.POSConfig._pos_cash_drawer, (int)MyLib._myGlobal._decimalPhase(__config._useCashDrawer));
                if (__config._cashDrawerPort != null) this._posClientGeneralConfig._setDataStr(_g.d.POSConfig._pos_cash_drawer_port, __config._cashDrawerPort);
                this._posClientGeneralConfig._setDataStr(_g.d.POSConfig._drawer_codes, __config._drawerCodes);
                this._posClientGeneralConfig._setComboBox(_g.d.POSConfig._full_invoice_print_type, __config.full_invoice_print_type);// = (int)MyLib._myGlobal._decimalPhase(this._posClientGeneralConfig._getDataStr("full_invoice_print_type"));
                this._posClientGeneralConfig._setDataStr(_g.d.POSConfig._form_code, __config.full_invoice_form_code);
                if (__config.full_invoice_printer_name != null)
                {
                    MyLib._myComboBox __comboFullInvPrinterName = (MyLib._myComboBox)this._posClientGeneralConfig._getControl(_g.d.POSConfig._full_invoice_printer_name);
                    __comboFullInvPrinterName.SelectedItem = __config.full_invoice_printer_name; // = __comboFullInvPrinterName.Text;
                }

                // tab display
                for (int __i = 0; __i < __config._screenConfig.Count; __i++)
                {
                    Control __obj = this._screenMoniter._getControl(_moniterStr + __i);
                    if (__obj != null && __obj.GetType() == typeof(MyLib._myComboBox))
                    {
                        if (!((SMLPOSControl._screenConfig)__config._screenConfig[__i])._screen_code.Equals("None"))
                        {
                            // toe
                            string __screenCode = ((SMLPOSControl._screenConfig)__config._screenConfig[__i])._screen_code;
                            string __moniterCode = ((SMLPOSControl._screenConfig)__config._screenConfig[__i])._moniter;
                            int __index = this._screenMoniter._screenCodeList.IndexOf(__screenCode);

                            this._screenMoniter._setComboBox(_moniterStr + __moniterCode, __index);
                        }
                    }
                }
                this._screenMoniter._setCheckBox(_g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_basket, (__config._search_level_basket) ? "1" : "0");
                this._screenMoniter._setCheckBox(_g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_minibox, (__config._pos_search_minibox) ? "1" : "0");
                this._screenMoniter._setDataNumber(_g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_result_items, __config._pos_search_result_items);

                // tab print and slip
                _screen_pos_configslipFont._setDataStr(_g.d.POSConfig._pos_slip_header_font, string.Format("{0},{1}", __config._pos_slip_header_fontname, __config._pos_slip_header_fontsize));
                _screen_pos_configslipFont._setDataStr(_g.d.POSConfig._pos_slip_detail_font, string.Format("{0},{1}", __config._pos_slip_detail_fontname, __config._pos_slip_detail_fontsize));
                _screen_pos_configslipFont._setDataStr(_g.d.POSConfig._pos_slip_footer_font, string.Format("{0},{1}", __config._pos_slip_footer_fontname, __config._pos_slip_footer_fontsize));
                _screen_pos_configslipFont._setDataNumber(_g.d.POSConfig._pos_slip_width, (decimal)__config._pos_slip_width);
                this._screen_pos_configslipFont._setCheckBox(_g.d.POSConfig._pos_ask_before_print, ((__config._pos_ask_before_print) ? "1" : "0"));
                this._screen_pos_configslipFont._setDataNumber(_g.d.POSConfig._pos_delay_change_money_screen, __config._pos_delay_finish_screen);
                this._screen_pos_configslipFont._setCheckBox(_g.d.POSConfig._pos_manualClose_change_money_screen, ((__config._manual_close_finish_screen) ? "1" : "0"));
                this._screen_pos_configslipFont._setCheckBox(_g.d.POSConfig._pos_no_print_barcode_slip, ((__config._pos_no_print_barcode_slip) ? "1" : "0"));
                this._screen_pos_configslipFont._setCheckBox(_g.d.POSConfig._no_print_slip, ((__config._no_print_slip) ? "1" : "0"));

                // tab sound
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_scan_barcode, __config._use_sound_scan_barcode.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_scan_barcode, __config._sound_on_scan_barcode);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_on_already_item, __config._use_sound_on_already_item.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_already_item, __config._sound_on_already_item);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_on_open_cash_drawer, __config._use_sound_on_open_cash_drawer.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_open_cash_drawer, __config._sound_on_open_cash_drawer);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_on_already_barcode, __config._use_sound_on_already_barcode.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_already_barcode, __config._sound_on_already_barcode);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_on_not_found_barcode, __config._use_sound_on_not_found_barcode.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_not_found_barcode, __config._sound_on_not_found_barcode);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_on_selected_member, __config._use_sound_on_selected_member.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_selected_member, __config._sound_on_selected_member);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_on_change_seller, __config._use_sound_on_change_seller.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_change_seller, __config._sound_on_change_seller);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_on_close_selling, __config._use_sound_on_close_selling.ToString());
                _screen_pos_sound_config._setDataStr(_g.d.POSConfig._sound_on_close_selling, __config._sound_on_close_selling);
                _screen_pos_sound_config._setCheckBox(_g.d.POSConfig._use_sound_amount, __config._use_sound_amount.ToString());

                // customer display
                this._configCustomerDisplayControl1._use_customer_checkbox.Checked = __config._use_customer_display;
                this._configCustomerDisplayControl1._customerDisplayPort.Text = __config._customer_display_port;
                this._configCustomerDisplayControl1.cmbBaudRate.Text = __config._customer_display_baudrate;
                this._configCustomerDisplayControl1.cmbParity.Text = __config._customer_display_parity.ToString();
                this._configCustomerDisplayControl1.cmbStopBits.Text = __config._customer_display_stopbits.ToString();
                this._configCustomerDisplayControl1.cmbDataBits.Text = __config._customer_display_databits;
                this._configCustomerDisplayControl1._customerDisplayLineCount._setDataNumber = __config._customer_display_linecount;
                this._configCustomerDisplayControl1._androidcheckbox.Checked = __config._android_customer_display;
            }
        }



        private void _enabledSlipFormCode(Boolean __disable)
        {
            MyLib._myTextBox __textbox = (MyLib._myTextBox)this._posClientGeneralConfig._getControl(_g.d.POSConfig._slip_code);
            __textbox.Enabled = __disable;
        }

        private void _enablePrinterSetting(bool __disable)
        {

            Control __comboPrinterType = this._posClientGeneralConfig._getControl(_g.d.POSConfig._pos_type_printer);
            Control __comboPrinterPort = this._posClientGeneralConfig._getControl(_g.d.POSConfig._pos_port_printer);
            Control __comboPrinterName = this._posClientGeneralConfig._getControl(_g.d.POSConfig._pos_printer_name);


            if (__comboPrinterPort != null && __comboPrinterType != null)
            {
                if (__disable == true)
                {
                    __comboPrinterType.Enabled = true;
                    __comboPrinterPort.Enabled = true;
                    __comboPrinterName.Enabled = true;
                }
                else
                {
                    __comboPrinterType.Enabled = false;
                    __comboPrinterPort.Enabled = false;
                    __comboPrinterName.Enabled = false;
                }
            }
        }

        private void _enableDrawCash(bool __disable)
        {
            Control __comboCashDrawer = this._posClientGeneralConfig._getControl(_g.d.POSConfig._pos_cash_drawer_port);

            if (__comboCashDrawer != null)
            {
                if (__disable == true)
                {
                    __comboCashDrawer.Enabled = true;
                }
                else
                {
                    __comboCashDrawer.Enabled = false;
                }
            }
        }

        /*
        void _databaseCombobox_DropDown(object sender, EventArgs e)
        {
            // get database list
            this._databaseCombobox.Items.Clear();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_database_name + "," + MyLib._d.sml_database_list._data_name + "  from " + MyLib._d.sml_database_list._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_group) + "=\'" + MyLib._myGlobal._dataGroup.ToUpper() + "\' and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=0 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\') or " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list._data_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._data_code) + " from " + MyLib._d.sml_database_list_user_and_group._table + " where " + MyLib._d.sml_database_list_user_and_group._user_or_group_status + "=1 and " + MyLib._myGlobal._addUpper(MyLib._d.sml_database_list_user_and_group._user_or_group_code) + " in (select " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._group_code) + " from " + MyLib._d.sml_user_and_group._table + " where " + MyLib._myGlobal._addUpper(MyLib._d.sml_user_and_group._user_code) + "=\'" + MyLib._myGlobal._userCode.ToUpper() + "\')) order by " + MyLib._d.sml_database_list._data_name;

            try
            {
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._mainDatabase, __query);
                string[] _dbList = null;
                if (__result.Tables.Count > 0)
                {
                    DataRow[] __getRows = __result.Tables[0].Select();
                    _dbList = new string[__getRows.Length];

                    Dictionary<string, string> __dbList = new Dictionary<string, string>();
                    for (int __row = 0; __row < __getRows.Length; __row++)
                    {
                        this._databaseCombobox.Items.Add(__getRows[__row].ItemArray[0].ToString());
                        //__dbList[__getRows[__row].ItemArray[0].ToString()] = __getRows[__row].ItemArray[1].ToString();
                        //_dbList[__row] = __getRows[__row].ItemArray[0].ToString();

                        //_gridDatabaseList._addRow();
                        //int rowDataGrid = _gridDatabaseList._rowData.Count - 1;
                        //_gridDatabaseList._cellUpdate(rowDataGrid, 0, __getRows[__row].ItemArray[0].ToString(), false);
                        //_gridDatabaseList._cellUpdate(rowDataGrid, 1, __getRows[__row].ItemArray[1].ToString(), false);

                    } // for

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
         * */

        void _configPOSScreen_Load(object sender, EventArgs e)
        {
            string __smlSoftPath = @"C:\smlsoft\";
            bool __isDirCreate = MyLib._myUtil._dirExists(__smlSoftPath);

            if (__isDirCreate == false)
            {
                System.IO.Directory.CreateDirectory(__smlSoftPath); // create folders
            }


            this._tabPosConfig.TableName = _g.d.POSConfig._table;
            this._tabPosConfig._getResource();


            _enablePrinterSetting(false);
            _enableDrawCash(false);


            //_loadPosScreenList();
            _loadConfig();

        }


        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _saveData()
        {
            SMLPOSControl._posScreenConfig __config = new SMLPOSControl._posScreenConfig();

            // save tab general
            __config._posid = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_terminal_id);
            __config._invoiceSlip = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_type_slip);
            __config._slip_form = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._slip_code);
            __config._printerType = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_type_printer);
            __config._print_method = (int)MyLib._myGlobal._decimalPhase(this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_print_method));
            __config._printerPort = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_port_printer);
            __config._pos_printer_name = this._posClientGeneralConfig._getDataComboStr(_g.d.POSConfig._pos_printer_name, false);
            __config._pos_slip_copy_print = (this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_slip_print_copy).Equals("1")) ? true : false; // save copy print
            __config._printer_manual_feed = (this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._printer_manual_feed).Equals("1")) ? true : false; // เครื่องพิมพ์เป็นแบบฉีก
            __config._pos_slip_copy_printNumber = (int)this._posClientGeneralConfig._getDataNumber(_g.d.POSConfig._pos_slip_print_copy_number);
            __config._useCashDrawer = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_cash_drawer);
            __config._cashDrawerPort = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._pos_cash_drawer_port);
            __config._drawerCodes = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._drawer_codes);
            __config.full_invoice_print_type = (int)MyLib._myGlobal._decimalPhase(this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._full_invoice_print_type)); // full invoice 
            __config.full_invoice_form_code = this._posClientGeneralConfig._getDataStr(_g.d.POSConfig._form_code);
            __config.full_invoice_printer_name = this._posClientGeneralConfig._getDataComboStr(_g.d.POSConfig._full_invoice_printer_name, false);

            // tab display
            for (int __i = 0; __i < this._displayDevice.Length; __i++)
            {
                //toe
                //Control __obj = this._screenMoniter._getControl(_moniterStr + __i);
                //if (__obj != null && __obj.GetType() == typeof(MyLib._myComboBox))
                //{
                //    MyLib._myComboBox __comboBox = (MyLib._myComboBox)__obj;
                //    Screen __src = _displayDevice[__i];

                //    string _screenDeviceName = Regex.Split(__src.DeviceName, "\0")[0].ToString();

                //    SMLPOSControl._screenConfig __device = new SMLPOSControl._screenConfig() { _moniter = __i.ToString(), _deviceName = MyLib._myUtil._convertTextToXml(_screenDeviceName) };
                //    __device._isMasterScreen = _checkIsPrimaryScreen(__src.DeviceName);

                //    // toe
                //    //__device._screen_code = (__comboBox.SelectedIndex <= 0) ? "None" : _posData.Rows[__comboBox.SelectedIndex - 1][_g.d.sml_posdesign._screen_code].ToString();
                //    __config._screenConfig.Add(__device);
                //}

                Screen __src = _displayDevice[__i];
                string _screenDeviceName = Regex.Split(__src.DeviceName, "\0")[0].ToString();
                SMLPOSControl._screenConfig __device = new SMLPOSControl._screenConfig() { _moniter = __i.ToString(), _deviceName = MyLib._myUtil._convertTextToXml(_screenDeviceName) };
                __device._isMasterScreen = _checkIsPrimaryScreen(__src.DeviceName);

                int _deviceSelectIndex = (int)this._screenMoniter._getDataNumber(_moniterStr + __i);

                __device._screen_code = (this._screenMoniter._screenCodeList != null && this._screenMoniter._screenCodeList.Count > _deviceSelectIndex) ? this._screenMoniter._screenCodeList[_deviceSelectIndex] : "None";
                __config._screenConfig.Add(__device);
            }

            __config._search_level_basket = this._screenMoniter._getDataStr(_g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_basket).Equals("1") ? true : false;
            __config._pos_search_minibox = this._screenMoniter._getDataStr(_g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_minibox).Equals("1") ? true : false;
            __config._pos_search_result_items = (int)this._screenMoniter._getDataNumber(_g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_result_items);

            // tab print and slip
            string __headerFontSizeStr = _screen_pos_configslipFont._getDataStr(_g.d.POSConfig._pos_slip_header_font);
            string __detailFontSizeStr = _screen_pos_configslipFont._getDataStr(_g.d.POSConfig._pos_slip_detail_font);
            string __footerFontSizeStr = _screen_pos_configslipFont._getDataStr(_g.d.POSConfig._pos_slip_footer_font);

            if (__headerFontSizeStr.Length > 0)
            {
                __config._pos_slip_header_fontname = __headerFontSizeStr.Split(',')[0].ToString();
                __config._pos_slip_header_fontsize = (float)MyLib._myGlobal._decimalPhase(__headerFontSizeStr.Split(',')[1].ToString());
            }
            if (__detailFontSizeStr.Length > 0)
            {
                __config._pos_slip_detail_fontname = __detailFontSizeStr.Split(',')[0].ToString();
                __config._pos_slip_detail_fontsize = (float)MyLib._myGlobal._decimalPhase(__detailFontSizeStr.Split(',')[1].ToString());
            }
            if (__footerFontSizeStr.Length > 0)
            {
                __config._pos_slip_footer_fontname = __footerFontSizeStr.Split(',')[0].ToString();
                __config._pos_slip_footer_fontsize = (float)MyLib._myGlobal._decimalPhase(__footerFontSizeStr.Split(',')[1].ToString());
            }
            __config._pos_slip_width = (float)_screen_pos_configslipFont._getDataNumber(_g.d.POSConfig._pos_slip_width);
            __config._pos_ask_before_print = this._screen_pos_configslipFont._getDataStr(_g.d.POSConfig._pos_ask_before_print).Equals("1") ? true : false;
            __config._pos_delay_finish_screen = (int)this._screen_pos_configslipFont._getDataNumber(_g.d.POSConfig._pos_delay_change_money_screen);
            __config._manual_close_finish_screen = this._screen_pos_configslipFont._getDataStr(_g.d.POSConfig._pos_manualClose_change_money_screen).Equals("1") ? true : false;
            __config._pos_no_print_barcode_slip = this._screen_pos_configslipFont._getDataStr(_g.d.POSConfig._pos_no_print_barcode_slip).Equals("1") ? true : false;
            __config._no_print_slip = this._screen_pos_configslipFont._getDataStr(_g.d.POSConfig._no_print_slip).Equals("1") ? true : false;


            // tab sound
            __config._use_sound_scan_barcode = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_scan_barcode));
            __config._sound_on_scan_barcode = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_scan_barcode);
            __config._use_sound_on_already_item = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_on_already_item));
            __config._sound_on_already_item = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_already_item);
            __config._use_sound_on_open_cash_drawer = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_on_open_cash_drawer));
            __config._sound_on_open_cash_drawer = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_open_cash_drawer);
            __config._use_sound_on_already_barcode = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_on_already_barcode));
            __config._sound_on_already_barcode = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_already_barcode);
            __config._use_sound_on_not_found_barcode = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_on_not_found_barcode));
            __config._sound_on_not_found_barcode = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_not_found_barcode);
            __config._use_sound_on_selected_member = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_on_selected_member));
            __config._sound_on_selected_member = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_selected_member);
            __config._use_sound_on_change_seller = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_on_change_seller));
            __config._sound_on_change_seller = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_change_seller);
            __config._use_sound_on_close_selling = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_on_close_selling));
            __config._sound_on_close_selling = _screen_pos_sound_config._getDataStr(_g.d.POSConfig._sound_on_close_selling);
            __config._use_sound_amount = (int)MyLib._myGlobal._decimalPhase(_screen_pos_sound_config._getDataStr(_g.d.POSConfig._use_sound_amount));

            // customer display config
            __config._use_customer_display = this._configCustomerDisplayControl1._use_customer_checkbox.Checked;
            __config._android_customer_display = this._configCustomerDisplayControl1._androidcheckbox.Checked;
            __config._customer_display_port = this._configCustomerDisplayControl1._customerDisplayPort.Text;
            __config._customer_display_baudrate = this._configCustomerDisplayControl1.cmbBaudRate.Text;
            __config._customer_display_stopbits = (this._configCustomerDisplayControl1.cmbStopBits.Text.Length == 0) ? System.IO.Ports.StopBits.None : (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), this._configCustomerDisplayControl1.cmbStopBits.Text);
            __config._customer_display_parity = (this._configCustomerDisplayControl1.cmbParity.Text.Length == 0) ? System.IO.Ports.Parity.None : (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), this._configCustomerDisplayControl1.cmbParity.Text);
            __config._customer_display_databits = this._configCustomerDisplayControl1.cmbDataBits.Text;

            __config._customer_display_linecount = (int)MyLib._myGlobal._decimalPhase(this._configCustomerDisplayControl1._customerDisplayLineCount._textFirst);


            // write config to filename
            //string __configFileName = _savePosScreenConfigFileName + MyLib._myGlobal._databaseName + ".xml";
            //string __path = @"c:\\smlsoft\\" + __configFileName.ToLower();  // Path.GetTempPath() + "\\" + __configFileName.ToLower();
            string __localpath = string.Format(@"c:\\smlsoft\\{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _savePosScreenConfigFileName);

            try
            {
                XmlSerializer __colXs = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                TextWriter __memoryStream = new StreamWriter(__localpath.ToLower(), false, Encoding.UTF8);
                __colXs.Serialize(__memoryStream, __config);
                __memoryStream.Close();

                MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _buttonSaveConfig_Click(object sender, EventArgs e)
        {
            this._saveData();
        }
    }

    public class _posClientGeneralConfig : MyLib._myScreen
    {
        public _posClientGeneralConfig()
        {
            this._maxColumn = 1;
            string[] __serialPortList = new string[] { "COM1", "COM2", "COM3", "LPT1", "LPT2", "LPT2", "USB1", "USB2", "USB3" }; //System.IO.Ports.SerialPort.GetPortNames();

            List<string> __printerNameList = new List<string>();

            //
            try
            {
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    // PrinterSettings settings = new PrinterSettings();
                    //string __printerName = printer;
                    __printerNameList.Add(printer);
                }
            }
            catch
            {
                try
                {

                    ManagementObjectSearcher __printerList = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                    foreach (ManagementObject __getPrinter in __printerList.Get())
                    {
                        __printerNameList.Add(__getPrinter["Name"].ToString());
                    }
                }
                catch
                {

                }
            }

            this._table_name = _g.d.POSConfig._table;
            int __row = 0;
            this._addTextBox(__row++, 0, 1, 0, _g.d.POSConfig._pos_terminal_id, 1, 0, 1, true, false, false);
            //
            __row++;
            this._addComboBox(__row++, 0, _g.d.POSConfig._pos_type_slip, true, new string[] { _g.d.POSConfig._pos_slip_no_print, _g.d.POSConfig._pos_slip_roll, _g.d.POSConfig._pos_slip_fix }, true);
            this._addTextBox(__row++, 0, _g.d.POSConfig._slip_code, 0);
            this._addComboBox(__row++, 0, _g.d.POSConfig._pos_type_printer, true, new string[] { _g.d.POSConfig._pos_printer_dotmatrix, _g.d.POSConfig._pos_printer_thermal }, true);
            this._addComboBox(__row++, 0, _g.d.POSConfig._pos_print_method, true, new string[] { _g.d.POSConfig._pos_print_driver_method, _g.d.POSConfig._pos_print_direct_method, _g.d.POSConfig._pos_print_tcp_ip_method }, true);
            //this._addComboBox(__row++, 0, _g.d.POSConfig._pos_port_printer, true, __serialPortList, false, _g.d.POSConfig._pos_port_printer, false);
            this._addTextBox(__row++, 0, _g.d.POSConfig._pos_port_printer, 0);
            this._addComboBox(__row++, 0, _g.d.POSConfig._pos_printer_name, true, __printerNameList.ToArray(), false, _g.d.POSConfig._pos_printer_name, false);
            this._addCheckBox(__row++, 0, _g.d.POSConfig._pos_slip_print_copy, false, true);
            this._addCheckBox(__row++, 0, _g.d.POSConfig._printer_manual_feed, false, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSConfig._pos_slip_print_copy_number, 1, 0, true);

            __row++;
            this._addComboBox(__row++, 0, _g.d.POSConfig._pos_cash_drawer, true, new string[] { _g.d.POSConfig._pos_nouse_cash_drawer, _g.d.POSConfig._pos_use_cash_drawer }, true);
            //this._addComboBox(__row++, 0, _g.d.POSConfig._pos_cash_drawer_port, true, __serialPortList, false, _g.d.POSConfig._pos_cash_drawer_port, false);
            this._addTextBox(__row++, 0, _g.d.POSConfig._pos_cash_drawer_port, 0);
            this._addTextBox(__row++, 0, _g.d.POSConfig._drawer_codes, 0);
            __row++;
            this._addComboBox(__row++, 0, _g.d.POSConfig._full_invoice_print_type, true, new string[] { _g.d.POSConfig._full_invoice_print_type_standrad, _g.d.POSConfig._full_invoice_print_type_formdesign }, true);
            this._addTextBox(__row++, 0, _g.d.POSConfig._form_code, 0);
            this._addComboBox(__row++, 0, _g.d.POSConfig._full_invoice_printer_name, true, __printerNameList.ToArray(), false, _g.d.POSConfig._full_invoice_printer_name, false);

            //this._addCheckBox(__row++, 0, _g.d.POSConfig._pos_search_basket, false, true);

            this._enabedControl(_g.d.POSConfig._pos_slip_print_copy_number, false);

            this._checkBoxChanged += new MyLib.CheckBoxChangedHandler(_posClientGeneralConfig__checkBoxChanged);
        }

        void _posClientGeneralConfig__checkBoxChanged(object sender, string name)
        {
            MyLib._myCheckBox __checkBox = (MyLib._myCheckBox)this._getControl(_g.d.POSConfig._pos_slip_print_copy);
            if (__checkBox.Checked == true)
            {
                this._enabedControl(_g.d.POSConfig._pos_slip_print_copy_number, true);
            }
            else
            {
                this._enabedControl(_g.d.POSConfig._pos_slip_print_copy_number, false);
            }
        }
    }

    public class _screen_pos_configslipFont : MyLib._myScreen
    {
        MyLib._myTextBox __textboxSearch;
        string _searchName = "";
        public _screen_pos_configslipFont()
        {
            int __row = 0;
            this._table_name = _g.d.POSConfig._table;
            this._maxColumn = 1;
            this._autoUpperString = false;
            this._addTextBox(__row++, 0, 1, 0, _g.d.POSConfig._pos_slip_header_font, 1, 0, 1, true, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.POSConfig._pos_slip_detail_font, 1, 0, 1, true, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.POSConfig._pos_slip_footer_font, 1, 0, 1, true, false);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSConfig._pos_slip_width, 1, 0, true);
            this._addCheckBox(__row++, 0, _g.d.POSConfig._pos_ask_before_print, false, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSConfig._pos_delay_change_money_screen, 1, 0, true);
            this._addCheckBox(__row++, 0, _g.d.POSConfig._pos_manualClose_change_money_screen, false, true);
            this._addCheckBox(__row++, 0, _g.d.POSConfig._pos_no_print_barcode_slip, false, true);
            this._addCheckBox(__row++, 0, _g.d.POSConfig._no_print_slip, false, true);

            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screen_pos_configslipFont__textBoxSearch);
        }

        void _screen_pos_configslipFont__textBoxSearch(object sender)
        {
            //throw new NotImplementedException();
            __textboxSearch = (MyLib._myTextBox)sender;
            _searchName = __textboxSearch._name.ToLower();

            if (_searchName.Equals(_g.d.POSConfig._pos_slip_header_font) ||
                _searchName.Equals(_g.d.POSConfig._pos_slip_detail_font) ||
                _searchName.Equals(_g.d.POSConfig._pos_slip_footer_font))
            {
                FontDialog __dialog = new FontDialog();
                try
                {
                    string __fontinitStr = this._getDataStr(_searchName);
                    if (__fontinitStr.Length > 0)
                    {
                        __dialog.Font = new Font(__fontinitStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__fontinitStr.Split(',')[1].ToString()));
                    }
                }
                catch
                {
                }
                DialogResult __result = __dialog.ShowDialog(MyLib._myGlobal._mainForm);

                if (__result == DialogResult.OK)
                {
                    string __fontStr = string.Format("{0},{1}", __dialog.Font.Name, __dialog.Font.Size.ToString());
                    this._setDataStr(_searchName, __fontStr);
                }
            }
        }


    }

    public class _screen_pos_sound_config : MyLib._myScreen
    {
        MyLib._myTextBox __textboxSearch;
        string _searchName = "";

        public _screen_pos_sound_config()
        {
            int __row = 0;
            this._maxLabelWidth = new int[] { 0, 30 };
            this._table_name = _g.d.POSConfig._table;
            this._maxColumn = 2;
            this._autoUpperString = false;
            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_scan_barcode, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_scan_barcode, 1, 0, 1, false, false);

            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_on_already_item, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_already_item, 1, 0, 1, false, false);

            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_on_open_cash_drawer, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_open_cash_drawer, 1, 0, 1, false, false);

            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_on_already_barcode, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_already_barcode, 1, 0, 1, false, false);

            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_on_not_found_barcode, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_not_found_barcode, 1, 0, 1, false, false);

            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_on_selected_member, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_selected_member, 1, 0, 1, false, false);

            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_on_change_seller, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_change_seller, 1, 0, 1, false, false);

            this._addCheckBox(__row, 0, _g.d.POSConfig._use_sound_on_close_selling, false, true);
            this._addTextBox(__row++, 1, 1, 0, _g.d.POSConfig._sound_on_close_selling, 1, 0, 1, false, false);

            this._addCheckBox(__row++, 0, _g.d.POSConfig._use_sound_amount, false, true);

            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screen_pos_sound_config__textBoxSearch);
            this._checkBoxChanged += new MyLib.CheckBoxChangedHandler(_screen_pos_sound_config__checkBoxChanged);

            this.Invalidate();
        }

        void _screen_pos_sound_config__checkBoxChanged(object sender, string name)
        {
            //throw new NotImplementedException();
            MyLib._myCheckBox __checkBox = (MyLib._myCheckBox)sender;
            MyLib._myTextBox __textbox = null;

            if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_scan_barcode))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_scan_barcode);
            }
            else if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_on_already_item))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_already_item);
            }
            else if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_on_open_cash_drawer))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_open_cash_drawer);
            }
            else if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_on_already_barcode))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_already_barcode);
            }
            else if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_on_not_found_barcode))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_not_found_barcode);
            }
            else if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_on_selected_member))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_selected_member);
            }
            else if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_on_change_seller))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_change_seller);
            }
            else if (__checkBox._name.Equals(_g.d.POSConfig._use_sound_on_close_selling))
            {
                __textbox = (MyLib._myTextBox)this._getControl(_g.d.POSConfig._sound_on_close_selling);
            }

            if (__textbox != null)
            {
                __textbox.Enabled = (__checkBox.Checked) ? true : false;
            }
        }

        void _screen_pos_sound_config__textBoxSearch(object sender)
        {
            __textboxSearch = (MyLib._myTextBox)sender;
            _searchName = __textboxSearch._name.ToLower();

            if (_searchName.Equals(_g.d.POSConfig._sound_on_scan_barcode) ||
                _searchName.Equals(_g.d.POSConfig._sound_on_already_item) ||
                _searchName.Equals(_g.d.POSConfig._sound_on_open_cash_drawer) ||
                _searchName.Equals(_g.d.POSConfig._sound_on_already_barcode) ||
                _searchName.Equals(_g.d.POSConfig._sound_on_not_found_barcode) ||
                _searchName.Equals(_g.d.POSConfig._sound_on_selected_member) ||
                _searchName.Equals(_g.d.POSConfig._sound_on_change_seller) ||
                _searchName.Equals(_g.d.POSConfig._sound_on_close_selling))
            {
                OpenFileDialog __dialog = new OpenFileDialog();
                __dialog.Filter = "Wave Files(*.wav)|*.wav";
                try
                {
                    string __fontinitStr = this._getDataStr(_searchName);
                    if (__fontinitStr.Length > 0)
                    {
                        //__dialog.FileName = __fontinitStr;
                    }
                }
                catch
                {
                }
                DialogResult __result = __dialog.ShowDialog(MyLib._myGlobal._mainForm);

                if (__result == DialogResult.OK)
                {
                    string __fontStr = __dialog.FileName; //string.Format("{0},{1}", __dialog.Font.Name, __dialog.Font.Size.ToString());
                    this._setDataStr(_searchName, __fontStr);
                }
            }
        }
    }

    public class _screen_pos_display : MyLib._myScreen
    {
        Screen[] _displayDevice = null;
        string _moniterStr = "moniter";
        DataTable _posData = null;

        List<string> _screenNameList = null;
        public List<string> _screenCodeList = null;

        public _screen_pos_display()
        {
            int __row = 0;
            this._maxColumn = 1;

            //this._maxLabelWidth = new int[] { 120, 120 };
            _loadPosScreenList();

            _displayDevice = Screen.AllScreens;
            int __i;
            for (__i = 0; __i < _displayDevice.Length; __i++)
            {
                Screen __src = _displayDevice[__i];
                string __devieName = string.Format("จอแสดงผล {0} ({1}x{2})", __i, __src.Bounds.Width, __src.Bounds.Height);
                this._addComboBox(__row++, 0, _moniterStr + __i.ToString(), true, _screenNameList.ToArray(), false, __devieName);
            }

            this._addCheckBox(__row++, 0, _g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_basket, false, true);
            this._addCheckBox(__row++, 0, _g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_minibox, false, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSConfig._table + "." + _g.d.POSConfig._pos_search_result_items, 1, 0, true);
        }

        private void _loadPosScreenList()
        {
            string __query = "select " + _g.d.sml_posdesign._screen_code + " , " + _g.d.sml_posdesign._screen_name + " from " + _g.d.sml_posdesign._table;
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            DataSet __ds = __fw._query(MyLib._myGlobal._databaseName, __query);
            List<string> __scrName = new List<string>();
            List<string> __scrCode = new List<string>();

            __scrName.Add("None");
            __scrCode.Add("None");

            //Dictionary<string, string> __posList = new Dictionary<string, string>();
            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
            {
                _posData = __ds.Tables[0];
                for (int __row = 0; __row < __ds.Tables[0].Rows.Count; __row++)
                {
                    __scrName.Add(__ds.Tables[0].Rows[__row][_g.d.sml_posdesign._screen_name].ToString());
                    __scrCode.Add(__ds.Tables[0].Rows[__row][_g.d.sml_posdesign._screen_code].ToString());
                }
            }

            _screenNameList = __scrName;
            _screenCodeList = __scrCode;


        }

    }
}
