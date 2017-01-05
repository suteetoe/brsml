using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPOSControl
{
    public partial class _posConfig : MyLib._myForm
    {
        private int _maxTab = 10;
        private List<_mainPOSConfigScreen> _posScreen = new List<_mainPOSConfigScreen>();

        public _posConfig()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this.Load += new EventHandler(_posConfig_Load);
        }

        void _posConfig_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            for (int __loop = 0; __loop < this._maxTab; __loop++)
            {
                int __number = __loop + 1;
                this._tabControl.TabPages.Add(__number.ToString());
                _mainPOSConfigScreen __screen = new _mainPOSConfigScreen();
                this._posScreen.Add(__screen);
                __screen.Dock = DockStyle.Fill;
                this._tabControl.TabPages[__loop].Controls.Add(__screen);
                this._tabControl.TabPages[__loop].BackColor = Color.White;
                try
                {
                    string __query = "select * from " + _g.d.POSConfig._table + " where " + _g.d.POSConfig._pos_config_number + "=" + __number.ToString() + " or " + _g.d.POSConfig._pos_config_number + " is null";
                    __screen._loadData(__myFrameWork._queryShort(__query).Tables[0]);
                }
                catch
                {
                }
                __screen._setDataNumber(_g.d.POSConfig._pos_config_number, (decimal)__number);
            }

            /*ArrayList getData = _mainPOSConfigScreen1._createQueryForDatabase();

            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string query = "select " + getData[0].ToString() + ", ( select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where UPPER(" + _g.d.ar_customer._code + " ) = UPPER(" + _g.d.POSConfig._table + "." + _g.d.POSConfig._cust_code_default + ")) AS " + _g.d.ar_customer._name_1 + " from " + _mainPOSConfigScreen1._table_name;
            DataSet result = myFrameWork._query(MyLib._myGlobal._databaseName, query);
            _mainPOSConfigScreen1._loadData(result.Tables[0]);
            //_companyProfileDetailScreen1._loadData(result.Tables[0]);
            if (result.Tables[0].Rows.Count > 0)
            {
                _mainPOSConfigScreen1._setDataStr(_g.d.POSConfig._cust_code_default, result.Tables[0].Rows[0][_g.d.POSConfig._cust_code_default].ToString(), result.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString(), true);
            }*/
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _saveData()
        {
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.POSConfig._table));
            for (int __loop = 0; __loop < this._maxTab; __loop++)
            {
                ArrayList __getData = this._posScreen[__loop]._createQueryForDatabase();
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.POSConfig._table + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                this._posScreen[__loop]._isChange = false;
            }
            __query.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show(__result, MyLib._myGlobal._resource("ล้มเหลว"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*string getEmtry = _mainPOSConfigScreen1._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                string query = "";
                ArrayList getData = _mainPOSConfigScreen1._createQueryForDatabase();
                //ArrayList getDetail = _companyProfileDetailScreen1._createQueryForDatabase();

                query = "insert into " + _g.d.POSConfig._table + " (" + getData[0].ToString() + ") values (" + getData[1].ToString() + ")";
                //
                string myQuery = MyLib._myGlobal._xmlHeader + "<node>";
                myQuery += "<query>delete from " + _g.d.POSConfig._table + "</query>";
                myQuery += "<query>" + query + "</query>";
                myQuery += "</node>";
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                string result = myFrameWork._queryList(MyLib._myGlobal._databaseName, myQuery);
                if (result.Length == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _mainPOSConfigScreen1._isChange = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result, MyLib._myGlobal._resource("ล้มเหลว"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
        }
    }

    public class _mainPOSConfigScreen : MyLib._myScreen
    {
        MyLib._searchDataFull _searchItem;
        string _searchName = "";
        TextBox _searchTextBox;

        public _mainPOSConfigScreen()
        {
            this._maxColumn = 6;
            this._table_name = _g.d.POSConfig._table;
            int __row = 0;

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSConfig._pos_config_number, 2, 0, true, "", true);
            this._addTextBox(__row, 2, 1, 0, _g.d.POSConfig._cust_code_default, 2, 1, 1, true, false, false);
            this._addTextBox(__row++, 4, 1, 0, _g.d.POSConfig._weight_scale_prefix, 2, 1, 0, true, false, true);

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSConfig._MaxPosMoney, 2, 1, true);
            this._addComboBox(__row, 2, _g.d.POSConfig._Is_Member, 2, true, new string[] { "use_member_system", "not_use_member_system" }, true, _g.d.POSConfig._Is_Member, true);
            this._addNumberBox(__row++, 4, 1, 0, _g.d.POSConfig._weight_prefix_start, 2, 1, true);

            this._addTextBox(__row, 0, 1, 0, _g.d.POSConfig._pos_bill_type, 2, 1, 0, true, false, true);
            this._addComboBox(__row, 2, _g.d.POSConfig._RoundChangeDecimal, 2, true, new string[] { "actual_round_decimal", "table_round_decimal" }, true, _g.d.POSConfig._RoundChangeDecimal, true);
            this._addNumberBox(__row++, 4, 1, 0, _g.d.POSConfig._weight_prefix_stop, 2, 1, true);

            this._addTextBox(__row, 0, 1, 0, _g.d.POSConfig._pos_bill_credit_type, 2, 1, 0, true, false, true);
            this._addTextBox(__row, 2, 1, 0, _g.d.POSConfig._FormatRound, 2, 1, 0, true, false, true);
            this._addNumberBox(__row++, 4, 1, 0, _g.d.POSConfig._weight_ic_code_start, 2, 1, true);

            this._addComboBox(__row, 0, _g.d.POSConfig._vat_type, 2, true, new string[] { "vat_type_include", "vat_type_exclude" }, true, _g.d.POSConfig._vat_type, true);
            this._addNumberBox(__row, 2, 1, 0, _g.d.POSConfig._service_charge_rate, 2, 1, true);
            this._addNumberBox(__row++, 4, 1, 0, _g.d.POSConfig._weight_ic_code_stop, 2, 1, true);

            this._addTextBox(__row, 0, 2, 0, _g.d.POSConfig._pos_header, 2, 1, 0, true, false, true);
            this._addTextBox(__row, 2, 2, 0, _g.d.POSConfig._pos_footer, 2, 1, 0, true, false, true);

            this._addNumberBox(__row++, 4, 1, 0, _g.d.POSConfig._weight_price_start, 2, 1, true);
            this._addNumberBox(__row++, 4, 1, 0, _g.d.POSConfig._weight_price_stop, 2, 1, true);

            //__row += 2;
            //__row += 2;
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.POSConfig._change_money_currency, 2, 1, 1, true, false, true);
            }

            this._addCheckBox(__row, 0, _g.d.POSConfig._pos_change_price_config, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._pos_check_send_money, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._use_item_set, false, true);


            this._addCheckBox(__row, 0, _g.d.POSConfig._pos_change_discount_config, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._pos_multilanguage_slip, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._pos_close_discount_bill, false, true);

            this._addCheckBox(__row, 0, _g.d.POSConfig._pos_cancel_bill, false, true, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._pos_cancel_bill_other_date, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._pos_send_deposit_money, false, true);

            this._addCheckBox(__row, 0, _g.d.POSConfig._show_tax_detail, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._barcode_picture, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._disable_cancel_order_outshift, false, true);

            this._addCheckBox(__row, 0, _g.d.POSConfig._show_short_description, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._pos_show_total_balance, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._balance_control, false, true);

            this._addCheckBox(__row, 0, _g.d.POSConfig._pos_cashdrawer_open_password, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._print_item_set_detail, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._warning_not_open_period, false, true);

            this._addCheckBox(__row, 0, _g.d.POSConfig._check_status_drawer, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._warning_select_cust, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._check_remove_item, false, true);

            this._addCheckBox(__row, 0, _g.d.POSConfig._lock_edit_order, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._point_member_only, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._check_member_expire, false, true);

            this._addCheckBox(__row, 0, _g.d.POSConfig._check_float_money, false, true);
            this._addCheckBox(__row, 2, _g.d.POSConfig._print_sale_daily_item, false, true);
            this._addCheckBox(__row++, 4, _g.d.POSConfig._show_item_location_on_only, false, true);

            //
            this._enabedControl(_g.d.POSConfig._pos_config_number, false);
            //this.Load += new EventHandler(_mainPOSConfigScreen_Load);
            _searchItem = new MyLib._searchDataFull();
            _searchItem.Text = MyLib._myGlobal._resource("ค้นหาลูกค้า");
            _searchItem._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchItem.WindowState = FormWindowState.Maximized;
            _searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_mainPOSConfigScreen1__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_mainPOSConfigScreen1__textBoxSearch);
            this._focusFirst();

            //this._maxLabelWidth = new int[] { 180, 20, 20};
        }

        void _mainPOSConfigScreen_Load(object sender, EventArgs e)
        {
        }
        void _mainPOSConfigScreen1__textBoxChanged(object sender, string name)
        {
            // check ว่า change จาก กำหนดรหัสลูกค้าเริ่มต้นหรือเปล่า
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.POSConfig._cust_code_default))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _mainPOSConfigScreen1__textBoxSearch(object sender)
        {
            // show search customer
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            //string _search_text_new = _search_screen_name(this._searchName);

            if (_searchName.Equals(_g.d.POSConfig._cust_code_default))
            {
                _searchItem._name = _g.d.POSConfig._cust_code_default;
                _searchItem.ShowDialog();
            }
            else if (_searchName.Equals(_g.d.POSConfig._change_money_currency))
            {
                MyLib._searchDataFull __searchCurrency = new MyLib._searchDataFull();
                __searchCurrency._dataList._loadViewFormat(_g.g._search_screen_erp_currency, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchCurrency._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        string __getIcCode = __searchCurrency._dataList._gridData._cellGet(__searchCurrency._dataList._gridData._selectRow, _g.d.erp_currency._table + "." + _g.d.erp_currency._code).ToString();
                        //string __getICName = __searchCurrency._dataList._gridData._cellGet(__searchCurrency._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();

                        //this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_code, __getIcCode, true);
                        //this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_name, __getICName, true);
                        this._setDataStr(_g.d.POSConfig._change_money_currency, __getIcCode);
                        SendKeys.Send("{TAB}");

                        __searchCurrency.Close();

                    }

                };

                __searchCurrency._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        string __getIcCode = __searchCurrency._dataList._gridData._cellGet(__searchCurrency._dataList._gridData._selectRow, _g.d.erp_currency._table + "." + _g.d.erp_currency._code).ToString();
                        //string __getICName = __searchCurrency._dataList._gridData._cellGet(__searchCurrency._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();

                        //this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_code, __getIcCode, true);
                        //this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.edi_product_list._ic_name, __getICName, true);
                        this._setDataStr(_g.d.POSConfig._change_money_currency, __getIcCode);
                        SendKeys.Send("{TAB}");

                        __searchCurrency.Close();

                    }
                };

                MyLib._myGlobal._startSearchBox(__getControl, "ค้นหาสกุลเงิน", __searchCurrency);
            }
        }

        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            //throw new NotImplementedException();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            //this._searchAll(__getParent2._name, e._row);
            string _search_text_new = __getParent2._name;
            string __result = "";
            if (__getParent2._name.Length > 0)
            {
                __result = (string)this._searchItem._dataList._gridData._cellGet(e._row, 0);
                if (__result.Length != 0)
                {
                    this._searchItem.Visible = false;
                    this._setDataStr(_searchName, __result);
                }
            }

        }

        public void _search(Boolean warning)
        {
            string __custCode = this._getDataStr(_g.d.POSConfig._cust_code_default);

            try
            {
                // Top Screen Search
                string __querySearchArName = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + __custCode.ToUpper() + "\'";
                _searchAndWarning(_g.d.POSConfig._cust_code_default, __querySearchArName, warning);

            }
            catch
            {
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
    }
}
