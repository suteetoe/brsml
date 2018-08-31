using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPConfig
{
    public partial class _option : MyLib._myForm
    {
        private System.Windows.Forms.TabPage tab_sale_shift;
        private System.Windows.Forms.TabPage tab_order;

        private _optionSaleShiftScreen _saleShiftScreen;
        private _optionBillFreeScreen _optionBillFreeScreen;
        private _optionOrderScreeen _optionOrderScreen;
        private _dashboardConfigScreen _dashboardConfig;

        bool _isEdit = true;

        public _option()
        {
            InitializeComponent();


            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_g.g._companyProfile._use_sale_shift == true)
                {
                    _saleShiftScreen = new _optionSaleShiftScreen();
                    _saleShiftScreen.Dock = DockStyle.Fill;

                    this.tab_sale_shift = new TabPage();

                    this._myTabControl1.Controls.Add(this.tab_sale_shift);

                    this.tab_sale_shift.Controls.Add(this._saleShiftScreen);
                    this.tab_sale_shift.Location = new System.Drawing.Point(4, 23);
                    this.tab_sale_shift.Name = "tab_sale_shift";
                    this.tab_sale_shift.Size = new System.Drawing.Size(724, 523);
                    this.tab_sale_shift.TabIndex = 1;
                    this.tab_sale_shift.Text = "2.tab_sale_shift";
                    this.tab_sale_shift.UseVisualStyleBackColor = true;

                }

                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
            {
                //this._myTabControl1.Dispose();
                //this.Controls.Clear();

                _optionBillFreeScreen = new _optionBillFreeScreen();
                this.tab_item.Controls.Clear();
                this.tab_item.Controls.Add(_optionBillFreeScreen);
                this._myTabControl1.TabPages.RemoveAt(0);
                this._myTabControl1.TabPages.RemoveAt(1);
                this._myTabControl1.TabPages.RemoveAt(1);
                _optionBillFreeScreen.Dock = DockStyle.Fill;
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
            {
                _optionOrderScreen = new _optionOrderScreeen();
                _optionOrderScreen.Dock = DockStyle.Fill;

                this.tab_order = new TabPage();

                this._myTabControl1.Controls.Add(this.tab_order);

                this.tab_order.Controls.Add(this._optionOrderScreen);
                this.tab_order.Location = new System.Drawing.Point(4, 23);
                this.tab_order.Name = "tab_order";
                this.tab_order.Size = new System.Drawing.Size(724, 523);
                this.tab_order.TabIndex = 1;
                this.tab_order.Text = this._myTabControl1.TabPages.Count + ".tab_order";
                this.tab_order.UseVisualStyleBackColor = true;

            }

            string __mainMenuId = MyLib._myGlobal._mainMenuIdPassTrue;
            string __mainMenuCode = MyLib._myGlobal._mainMenuCodePassTrue;
            MyLib._PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(__mainMenuId, __mainMenuCode);
            _isEdit = __permission._isEdit;

            //this._glOption.Height += 20;            

        }

        private void _option_Load(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _glOption._saveKeyDown += new MyLib.SaveKeyDownHandler(_glOption__saveKeyDown);
                _glOption._exitKeyDown += new MyLib.ExitKeyDownHandler(_glOption__exitKeyDown);
                _glOption._refresh();
                //
                _itemOption._saveKeyDown += new MyLib.SaveKeyDownHandler(_itemOption__saveKeyDown);
                _itemOption._exitKeyDown += new MyLib.ExitKeyDownHandler(_itemOption__exitKeyDown);
                _itemOption._refresh();
                //
                _saleOption._saveKeyDown += new MyLib.SaveKeyDownHandler(_saleOption__saveKeyDown);
                _saleOption._exitKeyDown += new MyLib.ExitKeyDownHandler(_saleOption__exitKeyDown);
                _saleOption._refresh();
                //
                ArrayList __getDataGlOption = _glOption._createQueryForDatabase();
                ArrayList __getDataItemOption = _itemOption._createQueryForDatabase();
                ArrayList __getDataSyncOption = _optionSync._createQueryForDatabase();
                ArrayList __getDataSaleOption = _saleOption._createQueryForDatabase();
                ArrayList __getARMOption = _optionSINGHAARMScreen1._createQueryForDatabase();

                string __sale_shift_query = "";
                if (_g.g._companyProfile._use_sale_shift == true)
                {
                    ArrayList __getSaleShiftOption = _saleShiftScreen._saleShiftScreen._createQueryForDatabase();
                    __sale_shift_query = "," + __getSaleShiftOption[0].ToString();
                }

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                {
                    ArrayList __getOrderOption = _optionOrderScreen._createQueryForDatabase();
                    __sale_shift_query = "," + __getOrderOption[0].ToString();
                }

                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "select " + __getDataGlOption[0].ToString() + "," + __getDataItemOption[0].ToString() + "," + __getDataSyncOption[0].ToString() + "," + __getDataSaleOption[0].ToString() + "," + __getARMOption[0].ToString() + __sale_shift_query + " from " + _glOption._table_name;
                    DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    _glOption._loadData(__result.Tables[0]);
                    _glOption._focusFirst();
                    //
                    _itemOption._loadData(__result.Tables[0]);
                    _itemOption._focusFirst();
                    //
                    _optionSync._loadData(__result.Tables[0]);
                    _saleOption._loadData(__result.Tables[0]);
                    _optionSINGHAARMScreen1._loadData(__result.Tables[0]);

                    if (_g.g._companyProfile._use_sale_shift == true)
                    {
                        this._saleShiftScreen._saleShiftScreen._loadData(__result.Tables[0]);
                    }

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                    {
                        _optionBillFreeScreen._loadData(__result.Tables[0]);
                    }

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                    {
                        _optionOrderScreen._loadData(__result.Tables[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }

        void _saleOption__exitKeyDown(object sender)
        {
            this.Close();
        }

        void _saleOption__saveKeyDown(object sender)
        {
            save_data();
        }

        void _itemOption__exitKeyDown(object sender)
        {
            this.Close();
        }

        void _itemOption__saveKeyDown(object sender)
        {
            save_data();
        }

        void _glOption__exitKeyDown(object sender)
        {
            this.Close();
        }

        void _glOption__saveKeyDown(object sender)
        {
            save_data();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_glOption._isChange || _itemOption._isChange)
            {
                DialogResult result = MyLib._myGlobal._displayWarning(5, "");
                if (result == DialogResult.No) e.Cancel = true;
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void save_data()
        {
            if (this._isEdit == true)
            {

                if (MyLib._myGlobal._programName.Equals("SML CM"))
                {
                    string __pricenterURL = this._optionSync._getDataStr(_g.d.erp_option._price_list_server);
                    this._optionSync._setDataStr(_g.d.erp_option._price_list_server, MyLib._myUtil._convertTextToXml(__pricenterURL));
                }
                string __query = "";
                ArrayList __getDataGlOption = _glOption._createQueryForDatabase();
                ArrayList __getDataItemOption = _itemOption._createQueryForDatabase();
                ArrayList __getDataSyncOption = _optionSync._createQueryForDatabase();
                ArrayList __getDataSaleOption = _saleOption._createQueryForDatabase();
                ArrayList __getARMOption = _optionSINGHAARMScreen1._createQueryForDatabase();

                string __getSaleShiftField = "";
                string __getSaleShiftValue = "";
                if (_g.g._companyProfile._use_sale_shift)
                {
                    ArrayList __getDataSaleShift = _saleShiftScreen._saleShiftScreen._createQueryForDatabase();
                    __getSaleShiftField = "," + __getDataSaleShift[0].ToString();
                    __getSaleShiftValue = "," + __getDataSaleShift[1].ToString();
                }

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                {
                    ArrayList __getDataOrder = this._optionOrderScreen._createQueryForDatabase();
                    __getSaleShiftField = "," + __getDataOrder[0].ToString();
                    __getSaleShiftValue = "," + __getDataOrder[1].ToString();
                }

                __query = "insert into " + _g.d.erp_option._table + " (" + __getDataGlOption[0].ToString() + "," + __getDataItemOption[0].ToString() + "," + __getDataSyncOption[0].ToString() + "," + __getDataSaleOption[0].ToString() + "," + __getARMOption[0].ToString() + __getSaleShiftField + ") values (" + __getDataGlOption[1].ToString() + "," + __getDataItemOption[1].ToString() + "," + __getDataSyncOption[1].ToString() + "," + __getDataSaleOption[1].ToString() + "," + __getARMOption[1].ToString() + __getSaleShiftValue + ")";
                //

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                {
                    ArrayList __getDataBillFree = _optionBillFreeScreen._createQueryForDatabase();

                    __query = "insert into " + _g.d.erp_option._table + " (" + __getDataBillFree[0].ToString() + "," + _g.d.erp_option._discout_type + ") values (" + __getDataBillFree[1].ToString() + ",1)";
                }

                string __myQuery = MyLib._myGlobal._xmlHeader + "<node>";
                __myQuery += "<query>delete from " + _g.d.erp_option._table + "</query>";
                __myQuery += "<query>" + __query + "</query>";
                __myQuery += "</node>";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery);
                if (__result.Length == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _glOption._isChange = false;
                    _itemOption._isChange = false;
                    Close();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ไม่อนุญาติให้แก้ไขข้อมูล");
            }

        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class _optionGlScreen : MyLib._myScreen
    {
        public _optionGlScreen()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.erp_option._table;

            int __row = 0;
            //
            MyLib._myGroupBox __group1 = this._addGroupBox(__row, 0, 1, 2, 2, _g.d.erp_option._year_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __group1, _g.d.erp_option._christian, 0, false);
            this._addRadioButtonOnGroupBox(0, 1, __group1, _g.d.erp_option._buddhist, 1, true);
            //
            __row += 2;

            MyLib._myGroupBox __group2 = this._addGroupBox(__row, 0, 1, 3, 2, _g.d.erp_option._vat_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __group2, _g.d.erp_option._vat_exclude, 0, true);
            this._addRadioButtonOnGroupBox(0, 1, __group2, _g.d.erp_option._vat_include, 1, false);
            this._addRadioButtonOnGroupBox(0, 2, __group2, _g.d.erp_option._vat_zero, 2, false);
            __row += 2;
            //
            MyLib._myGroupBox __group3 = this._addGroupBox(__row, 0, 1, 3, 2, _g.d.erp_option._vat_type_1, true);
            this._addRadioButtonOnGroupBox(0, 0, __group3, _g.d.erp_option._vat_exclude, 0, true);
            this._addRadioButtonOnGroupBox(0, 1, __group3, _g.d.erp_option._vat_include, 1, false);
            this._addRadioButtonOnGroupBox(0, 2, __group3, _g.d.erp_option._vat_zero, 2, false);
            __row += 2;
            //
            MyLib._myGroupBox __group4 = this._addGroupBox(__row, 0, 1, 2, 2, _g.d.erp_option._gl_trans_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __group4, _g.d.erp_option._separate, 0, true);
            this._addRadioButtonOnGroupBox(0, 1, __group4, _g.d.erp_option._compound, 1, false);
            __row += 2;

            MyLib._myGroupBox __gorup5 = this._addGroupBox(__row, 0, 1, 2, 2, _g.d.erp_option._discout_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __gorup5, _g.d.erp_option._discount_a, 0, false);
            this._addRadioButtonOnGroupBox(0, 1, __gorup5, _g.d.erp_option._discount_b, 1, true);
            __row += 2;
            //
            MyLib._myGroupBox __gorup6 = this._addGroupBox(__row, 0, 1, 2, 2, _g.d.erp_option._round_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __gorup6, _g.d.erp_option._round_bank, 0, false);
            this._addRadioButtonOnGroupBox(0, 1, __gorup6, _g.d.erp_option._round_digit, 1, true);
            __row += 2;
            //
            this._addNumberBox(__row, 0, 1, 0, _g.d.erp_option._vat_rate, 1, 2, true);
            this._addTextBox(__row++, 1, 1, 1, _g.d.erp_option._report_font, 1, 1, 1, true, false);

            this._addNumberBox(__row, 0, 1, 0, _g.d.erp_option._wht_rate, 1, 2, true);
            this._addTextBox(__row++, 1, 1, 1, _g.d.erp_option._report_font_header_1, 1, 1, 1, true, false);

            this._addNumberBox(__row, 0, 1, 0, _g.d.erp_option._gl_no_decimal, 1, 0, true);
            this._addTextBox(__row++, 1, 1, 1, _g.d.erp_option._report_font_header_2, 1, 1, 1, true, false);

            this._addCheckBox(__row, 0, _g.d.erp_option._use_doc_group, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._use_unit, false, true);
            this._addCheckBox(__row, 0, _g.d.erp_option._use_log_book, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._use_work_in_process, false, true);
            this._addCheckBox(__row, 0, _g.d.erp_option._use_print_slip, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._save_logs, false, true);
            this._addCheckBox(__row, 0, _g.d.erp_option._use_department, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._use_reference_pr, false, true);
            this._addCheckBox(__row, 0, _g.d.erp_option._use_project, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._get_purchase_price, false, true);
            this._addCheckBox(__row, 0, _g.d.erp_option._use_allocate, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._fix_item_set_price, false, true);

            this._addCheckBox(__row, 0, _g.d.erp_option._use_job, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._show_branch_doc_only, false, true);

            this._addCheckBox(__row, 0, _g.d.erp_option._use_expire, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._filter_pay_bill, false, true);

            this._addCheckBox(__row, 0, _g.d.erp_option._disable_edit_doc_no_doc_date, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._check_input_vat, false, true);
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                this._addCheckBox(__row, 0, _g.d.erp_option._multi_currency, false, true);
                this._addTextBox(__row++, 1, 1, 1, _g.d.erp_option._home_currency, 1, 1, 1, true, false);
            }

            this._addCheckBox(__row, 0, _g.d.erp_option._real_doc_date_doc_time, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._warning_bill_overdue, false, true);

            this._addCheckBox(__row, 0, _g.d.erp_option._gl_process_realtime, false, true);

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._addCheckBox(__row, 1, _g.d.erp_option._search_cheque_other_cust_code, false, true);
            }
            __row++;
            this._addCheckBox(__row, 0, _g.d.erp_option._running_doc_no_only, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._warning_branch_input, false, true);

            this._addCheckBox(__row, 0, _g.d.erp_option._warning_department, false, true);
            this._addCheckBox(__row++, 1, _g.d.erp_option._close_warning_backup, false, true);

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._addCheckBox(__row, 0, _g.d.erp_option._show_menu_by_permission, false, true);
                __row++;
            }
            this._addNumberBox(__row, 0, 1, 0, _g.d.erp_option._printer_top_margin, 1, 2, true);


            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLGeneralLedger ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                __row++;
                this._addTextBox(__row, 0, _g.d.erp_option._voucher_form_code, 10);
            }

            //
            this._setDataNumber(_g.d.erp_option._vat_rate, 7);
            this._setDataNumber(_g.d.erp_option._gl_no_decimal, 2);
            this._setDataNumber(_g.d.erp_option._wht_rate, 3);

            this._textBoxSearch += _optionGlScreen__textBoxSearch;
        }

        void _optionGlScreen__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;

            if (name == _g.d.erp_option._report_font ||
                name == _g.d.erp_option._report_font_header_1 ||
                name == _g.d.erp_option._report_font_header_2)
            {
                FontDialog __font = new FontDialog();

                /*string __getOldFont = this._getDataStr(_g.d.erp_option._report_font);
                if (__getOldFont.Length > 0)
                {
                    string[] __getFontSplit = __getOldFont.Split(',');
                    if (__getFontSplit.Length > 1)
                    {
                        __font.
                    }
                }*/

                DialogResult __result = __font.ShowDialog();

                if (__result == System.Windows.Forms.DialogResult.OK)
                {
                    string __fontSelect = string.Format("{0},{1}", __font.Font.Name, __font.Font.Size.ToString());
                    this._setDataStr(name, __fontSelect);
                }

            }
            else if (name == _g.d.erp_option._home_currency)
            {
                MyLib._searchDataFull __search = new MyLib._searchDataFull();
                __search.StartPosition = FormStartPosition.CenterScreen;
                __search.Text = MyLib._myGlobal._resource("ค้นหาสกุลเงิน");
                __search._dataList._loadViewFormat(_g.g._search_screen_erp_currency, MyLib._myGlobal._userSearchScreenGroup, false);
                __search._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string __code = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.erp_currency._table + "." + _g.d.erp_currency._code).ToString();
                    __search.Close();
                    this._setDataStr(_g.d.erp_option._home_currency, __code);
                };
                __search._searchEnterKeyPress += (s1, e1) =>
                {
                    string __code = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.erp_currency._table + "." + _g.d.erp_currency._code).ToString();
                    __search.Close();
                    this._setDataStr(_g.d.erp_option._home_currency, __code);
                };
                MyLib._myGlobal._startSearchBox(((MyLib._myTextBox)sender), label_name, __search, false, true, "");

            }
        }
    }

    public class _optionSyncScreen : MyLib._myScreen
    {
        public _optionSyncScreen()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._maxColumn = 1;
                this._table_name = _g.d.erp_option._table;
                int __row = 1;
                this._addTextBox(__row++, 0, _g.d.erp_option._sync_webservice_url, 0);
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                {
                    this._addTextBox(__row++, 0, _g.d.erp_option._sync_provider_code, 0);
                    this._addTextBox(__row++, 0, _g.d.erp_option._sync_database_name, 0);
                }

                this._addCheckBox(__row++, 0, _g.d.erp_option._sync_product, false, true);
                this._addTextBox(__row++, 0, _g.d.erp_option._mis_center_pw, 0);
                __row++;

                this._addCheckBox(__row++, 0, _g.d.erp_option._active_sync_active, false, true);
                this._addTextBox(__row++, 0, _g.d.erp_option._active_sync_branch_code, 0);
                this._addTextBox(__row++, 0, _g.d.erp_option._active_sync_server, 0);
                this._addTextBox(__row++, 0, _g.d.erp_option._active_sync_provider_name, 0);
                this._addTextBox(__row++, 0, _g.d.erp_option._active_sync_database_name, 0);
                this._addTextBox(__row++, 0, _g.d.erp_option._active_sync_access_code, 0);
                //__row++;
                this._addCheckBox(__row++, 0, _g.d.erp_option._use_point_center, false, true);
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                {
                    __row++;
                    this._addCheckBox(__row++, 0, _g.d.erp_option._add_item_color, false, true);
                }
                __row++;
                this._addCheckBox(__row++, 0, _g.d.erp_option._join_money_credit, false, true);
                this._addTextBox(__row++, 0, _g.d.erp_option._join_money_credit_list, 0);

                __row++;
                this._addCheckBox(__row++, 0, _g.d.erp_option._disable_sync_time, false, true);

                if (MyLib._myGlobal._programName.Equals("SML CM"))
                {
                    __row++;
                    this._addCheckBox(__row++, 0, _g.d.erp_option._use_price_center, false, true);
                    this._addTextBox(__row++, 0, _g.d.erp_option._price_list_server, 0);
                    this._addTextBox(__row++, 0, _g.d.erp_option._price_list_branch, 0);
                    this._addTextBox(__row++, 0, _g.d.erp_option._price_list_key, 0);
                    this._addTextBox(__row++, 0, _g.d.erp_option._price_list_serial_process, 0);

                }
                this._addTextBox(__row++, 0, _g.d.erp_option._sync_master_url, 0);
            }
        }
    }

    public class _optionSaleOrderScreen : MyLib._myScreen
    {
        public _optionSaleOrderScreen()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                int __row = 1;
                this._maxColumn = 2;
                this._table_name = _g.d.erp_option._table;
                this._addCheckBox(__row, 0, _g.d.erp_option._warning_price_1, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._warning_price_2, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._warning_low_cost, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._doc_sale_order_edit, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._stock_accrued_control, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._purchase_credit_note_type, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._ic_stock_control, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._sale_auto_packing, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._auto_run_docno_sale, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._doc_sale_tax_number_fixed, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._ic_price_formula_control, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._deposit_format_from_pos, false, true);


                this._addCheckBox(__row, 0, _g.d.erp_option._sale_real_price, false, true);

                if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                {
                    this._addCheckBox(__row, 1, _g.d.erp_option._use_sale_shift, false, true);
                }

                __row++;
                this._addCheckBox(__row, 0, _g.d.erp_option._lock_low_cost, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._warning_credit_money, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._lock_credit_money, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._password_ar_credit, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._find_lot_auto, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._warning_discount_1, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._stock_reserved_control, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._stock_reserved_control_location, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._lock_change_customer, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._cn_actual_cost, false, true);

                if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                {

                    this._addCheckBox(__row++, 0, _g.d.erp_option._sale_order_banalce_control, false, true);
                    this._addCheckBox(__row++, 0, _g.d.erp_option._check_lot_auto, false, true);
                }

                if (MyLib._myGlobal._OEMVersion.Equals("tvdirect"))
                {
                    this._addCheckBox(__row++, 0, _g.d.erp_option._manual_customer_code, false, true);
                }


                MyLib._myGroupBox __group4 = this._addGroupBox(__row, 0, 1, 3, 2, _g.d.erp_option._get_last_price_type, true);
                this._addRadioButtonOnGroupBox(0, 0, __group4, _g.d.erp_option._get_last_price_0, 0, true);
                this._addRadioButtonOnGroupBox(0, 1, __group4, _g.d.erp_option._get_last_price_1, 1, false);
                this._addRadioButtonOnGroupBox(0, 2, __group4, _g.d.erp_option._get_last_price_2, 2, false);

                __row += 2;
                MyLib._myGroupBox __gorup5 = this._addGroupBox(__row, 0, 1, 3, 2, _g.d.erp_option._default_sale_type, true);
                this._addRadioButtonOnGroupBox(0, 0, __gorup5, _g.d.erp_option._sale_type_default, 0, true);
                this._addRadioButtonOnGroupBox(0, 1, __gorup5, _g.d.erp_option._sale_type_credit, 1, false);
                this._addRadioButtonOnGroupBox(0, 2, __gorup5, _g.d.erp_option._sale_type_cash, 2, false);

                __row += 2;

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                {
                    this._addCheckBox(__row++, 0, _g.d.erp_option._print_pos_settle_bill_detail, false, true);
                }

                // Option ซ้ำ เอาออก
                //this._addCheckBox(__row++, 0, _g.d.erp_option._check_ar_status_credit, false, true);

                if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                {
                    this._addCheckBox(__row, 0, _g.d.erp_option._print_invoice_one_time, false, true);
                    this._addCheckBox(__row++, 1, _g.d.erp_option._check_ar_duplicate_name, false, true);
                    this._addCheckBox(__row, 0, _g.d.erp_option._tax_from_invoice, false, true);
                    this._addCheckBox(__row++, 1, _g.d.erp_option._customer_by_branch, false, true);
                }

                if (MyLib._myGlobal._isVersionAccount)
                {
                    this._addCheckBox(__row, 0, _g.d.erp_option._request_ar_credit, false, true);
                    this._addCheckBox(__row++, 1, _g.d.erp_option._ar_credit_chq_outstanding, false, true);

                    MyLib._myGroupBox __groupRequestArCredit = this._addGroupBox(__row, 0, 1, 4, 2, _g.d.erp_option._request_credit_type, true);
                    this._addRadioButtonOnGroupBox(0, 0, __groupRequestArCredit, _g.d.erp_option._system_user_approve, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, __groupRequestArCredit, _g.d.erp_option._sms_approve, 1, false);
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {

                        this._addRadioButtonOnGroupBox(0, 2, __groupRequestArCredit, _g.d.erp_option._salehub_approve, 2, false);
                        this._addRadioButtonOnGroupBox(0, 3, __groupRequestArCredit, _g.d.erp_option._sms_and_salehub_approve, 3, false);
                    }
                    __row += 2;

                    if (MyLib._myGlobal._isVersionAccount)
                    {
                        this._addTextBox(__row, 0, _g.d.erp_option._phone_number_approve, 0);
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            this._addTextBox(__row, 1, _g.d.erp_option._mobile_sale_url, 0);
                        __row++;
                        this._addTextBox(__row, 0, _g.d.erp_option._sale_hub_approve, 0);
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            this._addTextBox(__row, 1, _g.d.erp_option._mobile_bypasskey, 0);
                        __row++;
                    }


                }
                if (MyLib._myGlobal._programName == "SML CM")
                {
                    this._addCheckBox(__row, 0, _g.d.erp_option._hidden_price_formula, false, true);
                    this._addCheckBox(__row++, 1, _g.d.erp_option._quotation_expire, false, true);
                    this._addCheckBox(__row, 0, _g.d.erp_option._sr_ss_credit_check, false, true);
                    this._addCheckBox(__row++, 1, _g.d.erp_option._ss_ref_po_only, false, true);
                    this._addCheckBox(__row, 0, _g.d.erp_option._warning_input_customer, false, true);
                }

                this._addCheckBox(__row, 0, _g.d.erp_option._lock_sale_day_interval, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._lock_bill_auto, false, true);
                this._addNumberBox(__row, 0, 1, 0, _g.d.erp_option._sale_day_interval, 1, 2, true);
                this._addNumberBox(__row++, 1, 1, 0, _g.d.erp_option._lock_bill_auto_interval, 1, 2, true);
                this._addCheckBox(__row, 0, _g.d.erp_option._check_edit_project, false, true);

                //this._maxLabelWidth = new int[] { 129, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
        }
    }

    public class _optionItemScreen : MyLib._myScreen
    {
        public _optionItemScreen()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                int __row = 1;
                this._maxColumn = 2;
                this._table_name = _g.d.erp_option._table;

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                {
                    //
                    MyLib._myGroupBox __group1 = this._addGroupBox(__row, 0, 1, 2, 2, _g.d.erp_option._inventory_gl_post, true);
                    this._addRadioButtonOnGroupBox(0, 0, __group1, _g.d.erp_option._periodic_post, 0, false);
                    this._addRadioButtonOnGroupBox(0, 1, __group1, _g.d.erp_option._perpetual_post, 1, true);
                    //
                    __row += 2;

                }


                this._addNumberBox(__row, 0, 1, 0, _g.d.erp_option._item_qty_decimal, 1, 0, true);
                this._addNumberBox(__row++, 1, 1, 0, _g.d.erp_option._item_price_decimal, 1, 0, true);
                this._addNumberBox(__row++, 0, 1, 0, _g.d.erp_option._item_amount_decimal, 1, 0, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._column_price_enable, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._manual_total, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._stock_balance_control, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._warning_price_3, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._pr_approve_lock, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._ar_bill_inform, false, true);
                this._addCheckBox(__row, 0, _g.d.erp_option._cost_warehouse, false, true);

                this._addCheckBox(__row++, 1, _g.d.erp_option._count_stock_sum, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._use_barcode, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._perm_wh_shelf, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._auto_insert_time, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._item_hidden_cost_1, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._count_stock_display_saleprice, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._item_hidden_income_1, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._transfer_stock_control, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._barcode_picture, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._promotion_fixed_unitcode, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._issue_stock_control, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._disable_ic_cost_process, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._use_serial_no_duplicate, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._ignore_all_case, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._ap_bill_inform, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._disable_auto_stock_process, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._disable_item_cost, false, true);

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                {
                    this._addCheckBox(__row, 0, _g.d.erp_option._sum_point_all, false, true);
                    this._addCheckBox(__row++, 1, _g.d.erp_option._order_on_internet, false, true);
                    this._addCheckBox(__row++, 0, _g.d.erp_option._calc_point_from_pay, false, true);

                }

                if (
                   MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                   MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                   MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                {
                    this._addCheckBox(__row, 0, _g.d.erp_option._digital_barcode_scale, false, true);
                    __row++;
                }
                this._addCheckBox(__row++, 0, _g.d.erp_option._calc_item_price_discount, false, true);

                if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                {
                    this._addCheckBox(__row, 0, _g.d.erp_option._finishgoods_cost_auto, false, true);
                    this._addCheckBox(__row++, 1, _g.d.erp_option._issue_return_real_cost, false, true);
                }

                this._addCheckBox(__row, 0, _g.d.erp_option._warning_reorder_point, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._warning_product_expire, false, true);
                //__row++;
                this._addCheckBox(__row++, 0, _g.d.erp_option._ref_po_approve, false, true);

                //__row++;
                MyLib._myGroupBox __group5 = this._addGroupBox(__row, 0, 1, 3, 2, _g.d.erp_option._balance_control_type, true);
                this._addRadioButtonOnGroupBox(0, 0, __group5, _g.d.erp_option._balance_control_all, 0, true);
                this._addRadioButtonOnGroupBox(0, 1, __group5, _g.d.erp_option._balance_control_wh, 1, false);
                this._addRadioButtonOnGroupBox(0, 2, __group5, _g.d.erp_option._balance_control_shelf, 2, false);

                __row++;
                __row++;
                this._addTextBox(__row, 0, _g.d.erp_option._shop_time_open, 0);
                this._addTextBox(__row++, 1, _g.d.erp_option._shop_time_close, 0);

                this._addNumberBox(__row++, 0, 1, 0, _g.d.erp_option._next_process_time, 1, 0, true);
                this._addTextBox(__row++, 0, _g.d.erp_option._process_serial_device, 10);
                this._addTextBox(__row++, 0, _g.d.erp_option._warehouse_on_the_way, 255);
                this._addTextBox(__row++, 0, _g.d.erp_option._shelf_on_the_way, 255);
                this._addDateBox(__row++, 0, 1, 0, _g.d.erp_option._begin_date_import_inv, 1, true, false);
                this._addDateBox(__row, 0, 1, 0, _g.d.erp_option._end_date_import_inv, 1, true, false);

          
                //
                this._setDataNumber(_g.d.erp_option._item_qty_decimal, 2);
                this._setDataNumber(_g.d.erp_option._item_price_decimal, 2);
                this._setDataNumber(_g.d.erp_option._item_amount_decimal, 2);
            }
        }
    }

    public class _optionBillFreeScreen : MyLib._myScreen
    {
        public _optionBillFreeScreen()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.erp_option._table;
            int __row = 0;

            //
            MyLib._myGroupBox __group1 = this._addGroupBox(__row++, 0, 1, 2, 2, _g.d.erp_option._year_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __group1, _g.d.erp_option._christian, 0, false);
            this._addRadioButtonOnGroupBox(0, 1, __group1, _g.d.erp_option._buddhist, 1, true);
            //
            __row++;
            MyLib._myGroupBox __group2 = this._addGroupBox(__row++, 0, 1, 3, 2, _g.d.erp_option._vat_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __group2, _g.d.erp_option._vat_exclude, 0, true);
            this._addRadioButtonOnGroupBox(0, 1, __group2, _g.d.erp_option._vat_include, 1, false);
            this._addRadioButtonOnGroupBox(0, 2, __group2, _g.d.erp_option._vat_zero, 2, false);
            //
            __row++;
            this._addNumberBox(__row++, 0, 1, 0, _g.d.erp_option._vat_rate, 1, 2, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.erp_option._wht_rate, 1, 2, true);

            this._addNumberBox(__row++, 0, 1, 0, _g.d.erp_option._item_qty_decimal, 1, 0, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.erp_option._item_price_decimal, 1, 0, true);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.erp_option._item_amount_decimal, 1, 0, true);

            MyLib._myGroupBox __gorup5 = this._addGroupBox(__row, 0, 1, 3, 2, _g.d.erp_option._default_sale_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __gorup5, _g.d.erp_option._sale_type_default, 0, true);
            this._addRadioButtonOnGroupBox(0, 1, __gorup5, _g.d.erp_option._sale_type_credit, 1, false);
            this._addRadioButtonOnGroupBox(0, 2, __gorup5, _g.d.erp_option._sale_type_cash, 2, false);

        }
    }

    public class _optionOrderScreeen : MyLib._myScreen
    {
        public _optionOrderScreeen()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                int __row = 1;
                this._maxColumn = 2;
                this._table_name = _g.d.erp_option._table;


                this._addCheckBox(__row, 0, _g.d.erp_option._count_customer_table, false, true);
                this._addTextBox(__row, 0, _g.d.erp_option._process_serial_device, 10);

                this._addCheckBox(__row, 0, _g.d.erp_option._use_order_checker, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._disable_edit_order, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._auto_open_table, false, true);
                this._addCheckBox(__row++, 1, _g.d.erp_option._order_after_open_table, false, true);

                this._addCheckBox(__row, 0, _g.d.erp_option._orders_speech, false, true);
                this._addTextBox(__row++, 1, _g.d.erp_option._orders_speech_format, 0);

                this._addCheckBox(__row, 0, _g.d.erp_option._save_user_order, false, true);

                this._maxLabelWidth = new int[] { 100, 100 };
            }
        }
    }

    public class _optionSINGHAARMScreen : MyLib._myScreen
    {
        public _optionSINGHAARMScreen()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                int __row = 1;
                this._maxColumn = 2;
                this._table_name = _g.d.erp_option._table;


                this._addCheckBox(__row, 0, _g.d.erp_option._arm_send_cancel_doc, false, true);
                this._addTextBox(__row++, 1, _g.d.erp_option._arm_send_cancel_doc_to, 1000);

                this._addCheckBox(__row, 0, _g.d.erp_option._arm_send_cn, false, true);
                this._addTextBox(__row++, 1, _g.d.erp_option._arm_send_cn_to, 1000);

                this._addCheckBox(__row, 0, _g.d.erp_option._arm_send_ar_change, false, true);
                this._addTextBox(__row++, 1, _g.d.erp_option._arm_send_ar_change_to, 1000);

                this._maxLabelWidth = new int[] { 100, 100 };
            }
        }
    }

}