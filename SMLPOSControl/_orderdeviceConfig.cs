using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace SMLPOSControl
{
    public partial class _orderdeviceConfig : MyLib._myForm
    {
        public static string _saveOrderDeviceConfigFileName = "order_device_id";
        public _orderdeviceConfig()
        {
            InitializeComponent();

            this.Load += new EventHandler(_orderdeviceConfig_Load);
        }

        void _orderdeviceConfig_Load(object sender, EventArgs e)
        {
            _loadConfig();
        }

        void _loadConfig()
        {

            string __localpath = string.Format(MyLib._myGlobal._smlConfigFile + "{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _saveOrderDeviceConfigFileName);
            _orderDeviceXMLConfig __config = null;

            try
            {
                TextReader readFile = new StreamReader(__localpath.ToLower());
                XmlSerializer __xsLoad = new XmlSerializer(typeof(_orderDeviceXMLConfig));
                __config = (_orderDeviceXMLConfig)__xsLoad.Deserialize(readFile);

                readFile.Close();

            }
            catch
            {

            }

            if (__config != null)
            {
                _orderDeviceConfigScreen1._setDataStr(_g.d.order_device_id._device_id, __config._device_id);
                _orderDeviceConfigScreen1._setCheckBox(_g.d.order_device_id._print_orderdetail_onclose, __config._print_close_table.ToString());
                _orderDeviceConfigScreen1._setComboBox(_g.d.order_device_id._printer_mode, __config._print_mode);
                _orderDeviceConfigScreen1._setDataStr(_g.d.order_device_id._printer_name, __config._printer_name);

                _orderDeviceConfigScreen1._setCheckBox(_g.d.order_device_id._printer_manual_feed, ((__config._printer_manual_feed) ? "1" : "0"));

                this._orderFontSlipScreen1._setDataStr(_g.d.POSConfig._pos_slip_header_font, string.Format("{0},{1}", __config._pos_slip_header_fontname, __config._pos_slip_header_fontsize));
                this._orderFontSlipScreen1._setDataStr(_g.d.POSConfig._pos_slip_detail_font, string.Format("{0},{1}", __config._pos_slip_detail_fontname, __config._pos_slip_detail_fontsize));
                this._orderFontSlipScreen1._setDataStr(_g.d.POSConfig._pos_slip_footer_font, string.Format("{0},{1}", __config._pos_slip_footer_fontname, __config._pos_slip_footer_fontsize));

                this._orderFontSlipScreen1._setDataNumber(_g.d.order_device_id._pos_slip_width, MyLib._myGlobal._decimalPhase(__config._pos_slip_width.ToString()));

                _orderDeviceConfigScreen1._setCheckBox(_g.d.order_device_id._remark_print, ((__config._remark_print) ? "1" : "0"));
                _orderDeviceConfigScreen1._setDataStr(_g.d.order_device_id._summary_detail_font_name, string.Format("{0},{1}", __config._summary_detail_font_name, __config._summary_detail_font_size));

                _orderDeviceConfigScreen1._setCheckBox(_g.d.order_device_id._is_dot_printer, ((__config._is_dot_printer) ? "1" : "0"));
            }
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool _save_data()
        {
            string __checkEmpty = _orderDeviceConfigScreen1._checkEmtryField();

            if (__checkEmpty.Length > 0)
            {
                MessageBox.Show(__checkEmpty, "warning");
                return false;
            }
            else
            {
                _orderDeviceXMLConfig __config = new _orderDeviceXMLConfig();
                __config._device_id = _orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._device_id);
                __config._print_close_table = (int)MyLib._myGlobal._decimalPhase(_orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._print_orderdetail_onclose));
                __config._print_mode = (int)MyLib._myGlobal._decimalPhase(_orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._printer_mode));
                __config._printer_name = _orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._printer_name);

                string __headerFontSizeStr = this._orderFontSlipScreen1._getDataStr(_g.d.POSConfig._pos_slip_header_font);
                string __detailFontSizeStr = this._orderFontSlipScreen1._getDataStr(_g.d.POSConfig._pos_slip_detail_font);
                string __footerFontSizeStr = this._orderFontSlipScreen1._getDataStr(_g.d.POSConfig._pos_slip_footer_font);

                string __fontSummaryDetailStr = this._orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._summary_detail_font_name);

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

                __config._printer_manual_feed = this._orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._printer_manual_feed).Equals("1") ? true : false;
                __config._pos_slip_width = (float)this._orderFontSlipScreen1._getDataNumber(_g.d.order_device_id._pos_slip_width);
                __config._is_dot_printer = this._orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._is_dot_printer).Equals("1") ? true : false;

                __config._remark_print = this._orderDeviceConfigScreen1._getDataStr(_g.d.order_device_id._remark_print).Equals("1") ? true : false;
                if (__fontSummaryDetailStr.Length > 0)
                {
                    __config._summary_detail_font_name = __fontSummaryDetailStr.Split(',')[0].ToString();
                    __config._summary_detail_font_size = (float)MyLib._myGlobal._decimalPhase(__fontSummaryDetailStr.Split(',')[1].ToString());
                }


                // write config to file 
                string __localpath = string.Format(MyLib._myGlobal._smlConfigFile + "{3}-{0}-{1}-{2}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, _saveOrderDeviceConfigFileName);

                XmlSerializer __colXs = new XmlSerializer(typeof(_orderDeviceXMLConfig));
                TextWriter __memoryStream = new StreamWriter(__localpath.ToLower(), false, Encoding.UTF8);
                __colXs.Serialize(__memoryStream, __config);
                __memoryStream.Close();


                return true;
            }

            return false;
        }

        private void _buttonSaveConfig_Click(object sender, EventArgs e)
        {
            if (_save_data())
            {
                MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }

    [Serializable]
    public class _orderDeviceXMLConfig
    {
        /// <summary>รหัสเครื่องสั่งอาหาร</summary>
        public string _device_id;
        public int _print_close_table = 0;
        public int _print_mode = 0;
        public string _printer_name = "";
        /// <summary>ลำดับแบบฟอร์มเรียกชำระเงิน</summary>
        public int _billNumber = 0;
        /// <summary>Font ส่วนหัว Slip</summary>
        public string _pos_slip_header_fontname = "Tahoma";
        /// <summary>ขนาด Font ส่วนหัว Slip</summary>
        public float _pos_slip_header_fontsize = 9f;
        /// <summary>Font ส่วนรายละเอียด Slip</summary>
        public string _pos_slip_detail_fontname = "Tahoma";
        /// <summary>ขนาด Font ส่วนรายละเอียด Slip</summary>
        public float _pos_slip_detail_fontsize = 9f;
        /// <summary>Font ส่วนท้าย Slip</summary>
        public string _pos_slip_footer_fontname = "Tahoma";
        /// <summary>ขนาด Font ท้าย Slip</summary>
        public float _pos_slip_footer_fontsize = 9f;
        /// <summary>ใช้ Printer แบบฉีกมือ</summary>
        public bool _printer_manual_feed = false;
        /// <summary>ความยาว Slip</summary>
        public float _pos_slip_width = 575f;
        /// <summary>
        /// แสดงหมายเหตุในใบสรุป
        /// </summary>
        public Boolean _remark_print = false;
        /// <summary>
        /// รูปแบบตัวอักษรรายละเอียดสินค้า
        /// </summary>
        public string _summary_detail_font_name = "";
        public float _summary_detail_font_size = 0f;

        /// <summary>เป็นเครื่องพิมพ์ Dotmatrix</summary>
        public bool _is_dot_printer = false;

    }

    public class _orderDeviceConfigScreen : MyLib._myScreen
    {
        string _searchName = "";
        MyLib._searchDataFull _searchItem;
        TextBox _searchTextBox;
        public _orderDeviceConfigScreen()
        {
            this._autoUpperString = false;

            this._maxColumn = 1;
            this._table_name = _g.d.order_device_id._table;

            this._addTextBox(0, 0, 1, 0, _g.d.order_device_id._device_id, 1, 0, 1, true, false);
            this._addCheckBox(1, 0, _g.d.order_device_id._print_orderdetail_onclose, false, true);
            this._addComboBox(2, 0, _g.d.order_device_id._printer_mode, true, new string[] { _g.d.order_device_id._printer_mode_1, _g.d.order_device_id._printer_mode_2, _g.d.order_device_id._printer_mode_3 }, true);
            this._addTextBox(3, 0, 1, 0, _g.d.order_device_id._printer_name, 1, 0, 1, true, false);
            //this._addNumberBox(4, 0, 1, 0, "billNumber", 1, 0, true);
            this._addCheckBox(4, 0, _g.d.order_device_id._printer_manual_feed, false, true);

            this._addCheckBox(5, 0, _g.d.order_device_id._remark_print, false, true);
            this._addTextBox(6, 0, 1, 0, _g.d.order_device_id._summary_detail_font_name, 1, 0, 1, true, false);
            this._addCheckBox(7, 0, _g.d.order_device_id._is_dot_printer, false, true);

            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_orderDeviceConfigScreen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_orderDeviceConfigScreen__textBoxChanged);
        }


        void _orderDeviceConfigScreen__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.order_device_id._device_id))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }

        }

        public void _search(Boolean warning)
        {
            string __custCode = this._getDataStr(_searchName);

            string __query = "";
            string __searchName = "";

            if (_searchName.Equals(_g.d.order_device_id._device_id))
            {
                __searchName = _searchName;
                __query = "select " + _g.d.order_device_id._device_name + " from " + _g.d.order_device_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.order_device_id._device_id) + " = '" + __custCode.ToUpper() + "'";
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
                string getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, getDataStr, getData, true);
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        void _orderDeviceConfigScreen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = __getControl._name;
            string label_name = __getControl._labelName;

            if (_searchName.Equals(_g.d.order_device_id._device_id))
            {
                this._searchItem = new MyLib._searchDataFull();
                this._searchItem.Text = MyLib._myGlobal._resource("ค้นหารหัสเครื่องสั่งอาหาร");
                this._searchItem._dataList._loadViewFormat("screen_order_device_id", MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchItem.WindowState = FormWindowState.Maximized;
                this._searchItem._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string device_id = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.order_device_id._table + "." + _g.d.order_device_id._device_id).ToString();
                    this._searchItem.Close();
                    this._searchItem.Dispose();
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input");
                    this._setDataStr(_searchName, device_id);
                };
                this._searchItem._searchEnterKeyPress += (s1, e1) =>
                {
                    string device_id = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.order_device_id._table + "." + _g.d.order_device_id._device_id).ToString();
                    this._searchItem.Close();
                    this._searchItem.Dispose();
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input");
                    this._setDataStr(_searchName, device_id);
                };
                this._searchItem.ShowDialog();
            }
            else if (_searchName.Equals(_g.d.order_device_id._printer_name))
            {
                // start searh printer
                _food._myPrinterSearchDialog __dialog = new _food._myPrinterSearchDialog();
                __dialog.Text = "ค้นหาเครื่องพิมพ์";
                __dialog._beforeClose += (s1, e1) =>
                {
                    _food._myPrinterSearchDialog _search = (_food._myPrinterSearchDialog)s1;
                    if (_search.DialogResult == DialogResult.Yes)
                    {
                        MyLib._myComboBox __printerSelectCombo = (MyLib._myComboBox)_search._dialogScreen._getControl(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_name);
                        string __printerSelect = __printerSelectCombo.Text;
                        this._setDataStr(_g.d.order_device_id._printer_name, __printerSelect);
                    }

                };
                __dialog.ShowDialog();
            }
            else if (_searchName.Equals(_g.d.order_device_id._summary_detail_font_name))
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

    public class _orderFontSlipScreen : MyLib._myScreen
    {
        MyLib._myTextBox __textboxSearch;
        string _searchName = "";
        public _orderFontSlipScreen()
        {
            int __row = 0;
            this._table_name = _g.d.order_device_id._table;
            this._maxColumn = 1;
            this._autoUpperString = false;
            this._addTextBox(__row++, 0, 1, 0, _g.d.order_device_id._pos_slip_header_font, 1, 0, 1, true, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.order_device_id._pos_slip_detail_font, 1, 0, 1, true, false);
            this._addTextBox(__row++, 0, 1, 0, _g.d.order_device_id._pos_slip_footer_font, 1, 0, 1, true, false);

            this._addNumberBox(__row++, 0, 1, 0, _g.d.order_device_id._pos_slip_width, 1, 0, true);

            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screen_pos_configslipFont__textBoxSearch);
        }

        void _screen_pos_configslipFont__textBoxSearch(object sender)
        {
            //throw new NotImplementedException();
            __textboxSearch = (MyLib._myTextBox)sender;
            _searchName = __textboxSearch._name.ToLower();

            if (_searchName.Equals(_g.d.order_device_id._pos_slip_header_font) ||
                _searchName.Equals(_g.d.order_device_id._pos_slip_detail_font) ||
                _searchName.Equals(_g.d.order_device_id._pos_slip_footer_font))
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
}
