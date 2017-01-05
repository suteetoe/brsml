using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public class _icmainScreenTopControl : MyLib._myScreen
    {
        private _g._searchChartOfAccountDialog _chartOfAccountScreen = null;
        public string _screenCode = "";
        public bool _displayScreen = true;
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private string _searchName = "";
        private TextBox _searchTextBox;
        private ArrayList _search_data_full_buffer = new ArrayList();
        private ArrayList _search_data_full_buffer_name = new ArrayList();
        private int _search_data_full_buffer_addr = -1;
        private MyLib._searchDataFull _search_data_full_pointer;
        private string _old_filed_name = "";

        /// <summary>
        /// แสดงหน้าจอ
        /// </summary>
        [Category("_SML")]
        [Description("Enable Screen")]
        [DefaultValue(true)]
        public bool DisplayScreen
        {
            get
            {
                return _displayScreen;
            }
            set
            {
                _displayScreen = value;
                _build();
                this.Invalidate();
            }
        }

        public _icmainScreenTopControl()
        {
            _build();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
                this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
                this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_chartOfAccount_gridData__mouseClick);
            }
        }

        public void _newData()
        {
            if (DisplayScreen == true)
            {
                ((Control)this._getControl(_g.d.ic_inventory._code)).Enabled = true;
            }
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this.AutoSize = true;
                this._maxColumn = 2;
                this._table_name = _g.d.ic_inventory._table;
                int __row = 0;

                this._addTextBox(__row++, 0, 1, 0, _g.d.ic_inventory._code, 1, 1, 1, true, false, false);
                this._addTextBox(__row++, 0, 1, 0, _g.d.ic_inventory._name_1, 2, 0, 0, true, false, false);

                this._addComboBox(__row, 0, _g.d.ic_inventory._tax_type, true, new string[] { "normal_vat", "exc_vat" }, true);
                this._addComboBox(__row++, 1, _g.d.ic_inventory._item_type, true, new string[] { "ic_normal", "ic_service", "ic_rent", "ic_set", "ic_consignment", "ic_color", "ic_color_mixed" }, true);

                this._addComboBox(__row, 0, _g.d.ic_inventory._unit_type, true, new string[] { "single_unit", "many_unit" }, true);
                this._addTextBox(__row++, 1, 0, 0, _g.d.ic_inventory._unit_cost, 1, 0, 1, true, false, false);

                this._addTextBox(__row, 0, 0, 0, _g.d.ic_inventory._sign_code, 1, 0, 0, true, false, true);

                this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenTopControl__textBoxSearch);
                this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenTopControl__textBoxChanged);

            }
            else
            {
                this.AutoSize = true;
                this._maxColumn = 4;
                this._table_name = _g.d.ic_inventory._table;
                int __row = 0;
                //
                this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._code, 1, 1, 1, true, false, false);
                this._addCheckBox(__row, 1, _g.d.ic_inventory._ic_serial_no, false, true);
                this._addComboBox(__row, 2, _g.d.ic_inventory._tax_type, true, new string[] { "normal_vat", "exc_vat" }, true);
                this._addComboBox(__row, 3, _g.d.ic_inventory._item_type, true, new string[] { "ic_normal", "ic_service", "ic_rent", "ic_set", "ic_consignment", "ic_color", "ic_color_mixed" }, true);
                __row++;

                this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._name_1, 2, 0, 0, true, false, false);
                this._addTextBox(__row++, 2, 1, 0, _g.d.ic_inventory._name_2, 2, 0, 0, true, false, true);

                this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._name_eng_1, 2, 0, 0, true, false, true);
                this._addTextBox(__row, 2, 1, 0, _g.d.ic_inventory._name_eng_2, 2, 0, 0, true, false, true);
                __row++;

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || 
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || 
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS || 
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    this._addComboBox(__row, 0, _g.d.ic_inventory._unit_type, true, new string[] { "single_unit", "many_unit", "double_unit", "many_unit_and_double_unit" }, true);
                else
                    this._addComboBox(__row, 0, _g.d.ic_inventory._unit_type, true, new string[] { "single_unit", "many_unit" }, true);
                this._addTextBox(__row, 1, 0, 0, _g.d.ic_inventory._unit_cost, 1, 0, 1, true, false, false);
                this._addTextBox(__row, 2, 0, 0, _g.d.ic_inventory._unit_standard, 1, 0, 1, true, false, false);
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || 
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || 
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLIMS || 
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || 
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    this._addComboBox(__row, 3, _g.d.ic_inventory._cost_type, true, new string[] { _g.d.ic_inventory._average_cost, _g.d.ic_inventory._fifo_cost, _g.d.ic_inventory._assign_cost, _g.d.ic_inventory._expire_cost, _g.d.ic_inventory._standard_cost, _g.d.ic_inventory._other_cost_1 }, true);
                else
                    this._addComboBox(__row, 3, _g.d.ic_inventory._cost_type, true, new string[] { "average_cost", "fifo_cost" }, true);
                __row++;

                this._addTextBox(__row, 0, 0, 0, _g.d.ic_inventory._income_type, 1, 0, 1, true, false, true);
                this._addTextBox(__row, 1, 0, 0, _g.d.ic_inventory._item_pattern, 1, 0, 1, true, false, true);
                this._addTextBox(__row, 2, 1, 0, _g.d.ic_inventory._supplier_code, 1, 0, 1, true, false, true);
                this._addTextBox(__row, 3, 0, 0, _g.d.ic_inventory._sign_code, 1, 0, 0, true, false, true);
                __row++;

                this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._account_code_1, 1, 0, 4, true, false, true);
                this._addTextBox(__row, 1, 1, 0, _g.d.ic_inventory._account_code_2, 1, 0, 4, true, false, true);
                this._addTextBox(__row, 2, 1, 0, _g.d.ic_inventory._account_code_3, 1, 0, 4, true, false, true);
                this._addTextBox(__row, 3, 1, 0, _g.d.ic_inventory._account_code_4, 1, 0, 4, true, false, true);
                __row++;
                //
                this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._width_length_height, 1, 1, 0, true, false, true, false, true, true, _g.d.ic_inventory._width_length_height, "");
                this._addTextBox(__row, 1, 1, 0, _g.d.ic_inventory._weight, 1, 1, 0, true, false, true, false, true, true, _g.d.ic_inventory._weight, "");
                this._addCheckBox(__row, 2, _g.d.ic_inventory._update_detail, false, true);
                this._addCheckBox(__row, 3, _g.d.ic_inventory._drink_type, false, true);
                __row++;
                //
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                {
                    this._addCheckBox(__row, 0, _g.d.ic_inventory._is_new_item, false, true, false, true, _g.d.ic_inventory._is_new_item);
                }
                else if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                {
                    this._addCheckBox(__row, 0, _g.d.ic_inventory._no_discount, false, true, false, true, _g.d.ic_inventory._no_discount);
                }
                this._addCheckBox(__row, 1, _g.d.ic_inventory._use_expire, false, true, false, true, _g.d.ic_inventory._use_expire);
                this._addCheckBox(__row, 2, _g.d.ic_inventory._have_point, false, true, false, false, _g.d.ic_inventory._have_point);
                this._addCheckBox(__row, 3, _g.d.ic_inventory._update_price, false, true);

                this._textBoxSearch += new MyLib.TextBoxSearchHandler(_icmainScreenTopControl__textBoxSearch);
                this._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenTopControl__textBoxChanged);
                //
                this._getControl(_g.d.ic_inventory._code).Enabled =
                this._getControl(_g.d.ic_inventory._name_1).Enabled =
                this._getControl(_g.d.ic_inventory._name_2).Enabled =
                this._getControl(_g.d.ic_inventory._name_eng_1).Enabled =
                this._getControl(_g.d.ic_inventory._name_eng_2).Enabled =
                this._getControl(_g.d.ic_inventory._unit_cost).Enabled =
                this._getControl(_g.d.ic_inventory._unit_standard).Enabled =
                this._getControl(_g.d.ic_inventory._item_type).Enabled =
                this._getControl(_g.d.ic_inventory._cost_type).Enabled =
                this._getControl(_g.d.ic_inventory._unit_type).Enabled =
                this._getControl(_g.d.ic_inventory._tax_type).Enabled =
                this._getControl(_g.d.ic_inventory._income_type).Enabled =
                this._getControl(_g.d.ic_inventory._supplier_code).Enabled =
                this._getControl(_g.d.ic_inventory._item_pattern).Enabled =
                this._getControl(_g.d.ic_inventory._account_code_1).Enabled =
                this._getControl(_g.d.ic_inventory._account_code_2).Enabled =
                this._getControl(_g.d.ic_inventory._account_code_3).Enabled =
                this._getControl(_g.d.ic_inventory._account_code_4).Enabled =
                this._getControl(_g.d.ic_inventory._cost_type).Enabled =
                this._getControl(_g.d.ic_inventory._width_length_height).Enabled =
                this._getControl(_g.d.ic_inventory._weight).Enabled =
                this._getControl(_g.d.ic_inventory._sign_code).Enabled =
                this._getControl(_g.d.ic_inventory._update_price).Enabled =
                this._getControl(_g.d.ic_inventory._have_point).Enabled =
                this._getControl(_g.d.ic_inventory._update_detail).Enabled = DisplayScreen;

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                {
                    this._getControl(_g.d.ic_inventory._no_discount).Enabled = DisplayScreen;
                }
                //
            }

            this.ResumeLayout();
            foreach (Control __getControlAll in this.Controls)
            {
                if (__getControlAll.GetType() == typeof(MyLib._myTextBox))
                {
                    MyLib._myTextBox __getControlTextBox = (MyLib._myTextBox)__getControlAll;
                    if (__getControlTextBox._iconNumber == 1)
                    {
                        // กำหนดให้การค้นหาแบบจอเล็กแสดง
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.Enter += new EventHandler(textBox_Enter);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                }
            }
        }


        void _chartOfAccount_search(MyLib._myGrid sender, int row)
        {
            this._chartOfAccountScreen.Close();
            string __accountCode = sender._cellGet(row, 0).ToString();
            if (this._searchName.Equals(_g.d.ic_inventory._account_code_1)) this._setDataStr(_g.d.ic_inventory._account_code_1, __accountCode);
            if (this._searchName.Equals(_g.d.ic_inventory._account_code_2)) this._setDataStr(_g.d.ic_inventory._account_code_2, __accountCode);
            if (this._searchName.Equals(_g.d.ic_inventory._account_code_3)) this._setDataStr(_g.d.ic_inventory._account_code_3, __accountCode);
            if (this._searchName.Equals(_g.d.ic_inventory._account_code_4)) this._setDataStr(_g.d.ic_inventory._account_code_4, __accountCode);
            this._search(true);
        }

        void _chartOfAccount_gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._chartOfAccount_search((MyLib._myGrid)sender, e._row);
        }

        void _chartOfAccountScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._chartOfAccount_search(sender, row);
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full_pointer != null)
                {
                    this._search_data_full_pointer.Visible = true;
                    this._search_data_full_pointer.Focus();
                    this._search_data_full_pointer._firstFocus();
                }
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _icmainScreenTopControl__textBoxSearch(__getControl);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
        }

        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.ic_inventory._code)) return _g.g._search_screen_erp_doc_format;
            if (_name.Equals(_g.d.ic_inventory._unit_cost)) return _g.g._search_master_ic_unit;
            if (_name.Equals(_g.d.ic_inventory._unit_standard)) return _g.g._search_master_ic_unit;
            if (_name.Equals(_g.d.ic_inventory._income_type)) return _g.g._search_screen_income_list;
            if (_name.Equals(_g.d.ic_inventory._item_pattern)) return _g.g._search_master_ic_pattern;
            if (_name.Equals(_g.d.ic_inventory._supplier_code)) return _g.g._search_screen_ap;
            return "";
        }

        string _search_screen_name_extra_where(string _name)
        {
            if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }

        void _icmainScreenTopControl__textBoxSearch(object sender)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._saveLastControl();
                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                this._searchName = __getControl._name.ToLower();
                string label_name = __getControl._labelName;
                string _search_text_new = _search_screen_name(this._searchName);
                string __where_query = _search_screen_name_extra_where(this._searchName);
                if (this._searchName.Equals(_g.d.ic_inventory._account_code_1) || this._searchName.Equals(_g.d.ic_inventory._account_code_2) || this._searchName.Equals(_g.d.ic_inventory._account_code_3) || this._searchName.Equals(_g.d.ic_inventory._account_code_4))
                {
                    this._chartOfAccountScreen.ShowDialog();
                }
                else
                {
                    if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
                    {
                        Boolean __found = false;
                        this._old_filed_name = __getControl._name.ToLower();
                        // jead
                        int __addr = 0;
                        for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                        {
                            if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                            {
                                __addr = __loop;
                                __found = true;
                                break;
                            }
                        }
                        if (__found == false)
                        {
                            __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                            MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                            this._search_data_full_buffer.Add(__searchObject);
                        }
                        if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible == true)
                        {
                            this._search_data_full_pointer.Visible = false;
                        }
                        this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                        this._search_data_full_buffer_addr = __addr;
                        //
                    }

                    if (!this._search_data_full_pointer._name.Equals(_search_text_new.ToLower()))
                    {
                        if (this._search_data_full_pointer._name.Length == 0)
                        {
                            this._search_data_full_pointer._showMode = 0;
                            this._search_data_full_pointer._name = _search_text_new;
                            this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                            this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                            this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                        }
                    }
                    // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
                    if (this._search_data_full_pointer._show == false)
                    {
                        MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
                    }
                    else
                    {
                        MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
                    }
                    // jead : ทำให้ข้อมูลเรียกใหม่ทุกครั้ง ไม่ต้องมี เพราะ _startSearchBox มันดึงให้แล้ว this._search_data_full_pointer._dataList._refreshData(); เอาออกสองรอบแล้วเด้อ
                    if (__getControl._iconNumber == 1)
                    {
                        __getControl.Focus();
                        __getControl.textBox.Focus();
                    }
                    else
                    {
                        this._search_data_full_pointer.Focus();
                        this._search_data_full_pointer._firstFocus();
                    }
                }
            }
        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchMasterScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }


        void _icmainScreenTopControl__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.ic_inventory._code))
            {
                string __newCode = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                if (__newCode.Equals(__textBox._textFirst) == false)
                {
                    __textBox._textFirst = __newCode;
                    __textBox.textBox.Invalidate();
                }
                // autorun
                string __icCode = __textBox._textFirst;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + __icCode.ToUpper() + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newICCode = _g.g._getAutoRun(_g.g._autoRunType.สินค้า, __icCode, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
                    this._setDataStr(_g.d.ic_inventory._code, __newICCode, "", true);
                    this._search(true);
                }
                else
                {
                    if (__icCode.Length > 0)
                    {
                        try
                        {
                            string __newICCode = __icCode;
                            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "<\'" + __icCode + "z\' order by " + _g.d.ic_inventory._code + " desc limit 1").Tables[0];
                            if (__dt.Rows.Count > 0)
                            {
                                string __getItemCode = __dt.Rows[0][_g.d.ic_inventory._code].ToString();
                                if (__getItemCode.Length > __icCode.Length)
                                {
                                    string __s1 = __getItemCode.Substring(0, __icCode.Length);
                                    if (__s1.Equals(__icCode))
                                    {
                                        string __s2 = __getItemCode.Remove(0, __icCode.Length);
                                        int __runningNumber = (int)MyLib._myGlobal._decimalPhase(__s2);
                                        if (__runningNumber > 0)
                                        {
                                            StringBuilder __format = new StringBuilder();
                                            for (int __loop = 0; __loop < __s2.Length; __loop++)
                                            {
                                                __format.Append("0");
                                            }
                                            __newICCode = __s1 + String.Format("{0:" + __format.ToString() + "}", ((decimal)(__runningNumber + 1)));
                                            this._setDataStr(_g.d.ic_inventory._code, __newICCode, "", true);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                __textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            string __result = "";
            if (name.Length > 0)
            {
                __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    this._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }

        public void _search(Boolean warning)
        {
            try
            {
                // Top Screen Search
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_income_list._name_1 + " from " + _g.d.erp_income_list._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_income_list._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._income_type).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._unit_cost).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._unit_standard).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_pattern._name_1 + " from " + _g.d.ic_pattern._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_pattern._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._item_pattern).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._account_code_1).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._account_code_2).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._account_code_3).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._account_code_4).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + MyLib._myGlobal._addUpper(_g.d.ap_supplier._code) + "=\'" + this._getDataStr(_g.d.ic_inventory._supplier_code).ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.ic_inventory._income_type, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.ic_inventory._unit_cost, (DataSet)_getData[1], warning);
                _searchAndWarning(_g.d.ic_inventory._unit_standard, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.ic_inventory._item_pattern, (DataSet)_getData[3], warning);
                _searchAndWarning(_g.d.ic_inventory._account_code_1, (DataSet)_getData[4], warning);
                _searchAndWarning(_g.d.ic_inventory._account_code_2, (DataSet)_getData[5], warning);
                _searchAndWarning(_g.d.ic_inventory._account_code_3, (DataSet)_getData[6], warning);
                _searchAndWarning(_g.d.ic_inventory._account_code_4, (DataSet)_getData[7], warning);
                _searchAndWarning(_g.d.ic_inventory._supplier_code, (DataSet)_getData[8], warning);
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString().Trim();
                string __getDataStr = this._getDataStr(fieldName).Trim();
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName).Trim() != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)(MyLib._myTextBox)this._searchTextBox.Parent;
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __getTextBox._textFirst = "";
                        __getTextBox._textSecond = "";
                        __getTextBox._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        __getTextBox.Focus();
                        __getTextBox.textBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }
    }
}
