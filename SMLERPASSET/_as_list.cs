using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPASSET
{
    public partial class _as_list : UserControl
    {
        MyLib._searchDataFull _searchUnitCode = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchAsType = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchSideCode = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchDepartmentCode = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchAsLocation = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchUser = new MyLib._searchDataFull();
        MyLib._searchDataFull _searchProvince = new MyLib._searchDataFull();

        string _searchName = "";
        TextBox _searchTextBox;
        string _searchTxtName = "";
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        // Old Code For Update
        int _getColumnAsCode = 0;
        string _oldAsCode = "";

        TabPage tab_other;
        private MyLib._myScreen _other_screen;

        public _as_list()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            // Clear And Auto Run
            this._autoRunningButton._iconNumber = 1;
            this._autoRunningButton.Image = imageList1.Images[this._autoRunningButton._iconNumber];
            this._clearDataAfterSaveButton._iconNumber = 0;
            this._clearDataAfterSaveButton.Image = imageList1.Images[this._clearDataAfterSaveButton._iconNumber];
            // Screen Top
            this._screenTop.AutoSize = true;
            this._screenTop._maxColumn = 2;
            this._screenTop._table_name = _g.d.as_asset._table;

            string __getNormalStatusResource = MyLib._myGlobal._resource(_g.d.as_asset._table + "." + _g.d.as_asset._status_normal);
            string __getDefectiveStatusResource = MyLib._myGlobal._resource(_g.d.as_asset._table + "." + _g.d.as_asset._status_defective);
            string __getLostStatusResource = MyLib._myGlobal._resource(_g.d.as_asset._table + "." + _g.d.as_asset._status_lost);

            this._screenTop._addTextBox(0, 0, 1, 0, _g.d.as_asset._code, 1, 25, 1, true, false, false);
            this._screenTop._addTextBox(1, 0, 1, 0, _g.d.as_asset._name_1, 2, 100, 0, true, false, false);
            this._screenTop._addTextBox(2, 0, 1, 0, _g.d.as_asset._name_2, 2, 100, 0, true, false, true);
            this._screenTop._addTextBox(3, 0, 1, 0, _g.d.as_asset._name_eng_1, 2, 100, 0, true, false, false);
            this._screenTop._addTextBox(4, 0, 1, 0, _g.d.as_asset._name_eng_2, 2, 100, 0, true, false, true);
            this._screenTop._addTextBox(5, 0, 1, 0, _g.d.as_asset._code_old, 1, 25, 0, true, false, true);
            MyLib._myGroupBox __statusGroupBox = _screenTop._addGroupBox(6, 0, 1, 3, 2, _g.d.as_asset._status, true);
            _screenDepreciation._addRadioButtonOnGroupBox(0, 0, __statusGroupBox, __getNormalStatusResource, 0, true);
            _screenDepreciation._addRadioButtonOnGroupBox(0, 1, __statusGroupBox, __getDefectiveStatusResource, 1, false);
            _screenDepreciation._addRadioButtonOnGroupBox(0, 2, __statusGroupBox, __getLostStatusResource, 2, false);

            this._screenTop._addTextBox(8, 0, 2, _g.d.as_asset._remark, 2, 255);
            this._screenTop._refresh();
            // Event Screeen Top
            this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
            this._screenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenTop__textBoxSearch);
            this._screenTop._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenTop__checkKeyDown);
            this._screenTop._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenTop__checkKeyDownReturn);
            this._screenTop._textBoxChanged += _screenTop__textBoxChanged;
            // Screen General
            this._screenGeneral.AutoSize = true;
            this._screenGeneral._maxColumn = 2;
            this._screenGeneral._table_name = _g.d.as_asset._table;
            this._screenGeneral._addTextBox(0, 0, 1, 0, _g.d.as_asset._unit_code, 1, 10, 1, true, false, true);
            this._screenGeneral._addTextBox(0, 1, 1, 0, _g.d.as_asset._as_type, 1, 10, 1, true, false, true);
            this._screenGeneral._addTextBox(1, 0, 1, 0, _g.d.as_asset._side_code, 1, 10, 1, true, false, true);
            this._screenGeneral._addTextBox(1, 1, 1, 0, _g.d.as_asset._department_code, 1, 10, 1, true, false, true);
            this._screenGeneral._addTextBox(2, 0, 1, 0, _g.d.as_asset._as_location, 1, 10, 1, true, false, true);
            this._screenGeneral._addTextBox(2, 1, 1, 0, _g.d.as_asset._user_code, 1, 10, 1, true, false, true);
            this._screenGeneral._refresh();
            // Event Screen General
            this._screenGeneral._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenGeneral__saveKeyDown);
            this._screenGeneral._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenGeneral__checkKeyDown);
            this._screenGeneral._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenGeneral__checkKeyDownReturn);
            this._screenGeneral._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenGeneral__textBoxChanged);
            this._screenGeneral._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenGeneral__textBoxSearch);
            // Start Unit Code
            _searchUnitCode._name = _g.g._search_master_ic_unit;
            _searchUnitCode._dataList._loadViewFormat(_searchUnitCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchUnitCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchUnitCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchUnitCode__searchEnter);
            // Start Asset Type
            _searchAsType._name = _g.g._search_master_as_asset_type;
            _searchAsType._dataList._loadViewFormat(_searchAsType._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchAsType._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchAsType._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchAsType__searchEnter);
            // Start Sid Code
            _searchSideCode._name = _g.g._search_screen_erp_side_list;
            _searchSideCode._dataList._loadViewFormat(_searchSideCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchSideCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchSideCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchSideCode__searchEnter);
            // Start Section Code
            _searchDepartmentCode._name = _g.g._search_screen_erp_department_list;
            _searchDepartmentCode._dataList._loadViewFormat(_searchDepartmentCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchDepartmentCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchDepartmentCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDepartmentCode__searchEnter);
            // Start Asset Location
            _searchAsLocation._name = _g.g._search_master_as_asset_location;
            _searchAsLocation._dataList._loadViewFormat(_searchAsLocation._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchAsLocation._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchAsLocation._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchAsLocation__searchEnter);

            // search user
            _searchUser._name = _g.g._search_screen_erp_user;
            _searchUser._dataList._loadViewFormat(_searchUser._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchUser._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchUser._searchEnterKeyPress += _searchUser__searchEnterKeyPress;

            _searchProvince._name = _g.g._screen_erp_province;
            _searchProvince._dataList._loadViewFormat(_searchProvince._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchProvince._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchProvince._searchEnterKeyPress += _searchProvince__searchEnterKeyPress;

            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getUnitCodeControl = (MyLib._myTextBox)_screenGeneral._getControl(_g.d.as_asset._unit_code);
            __getUnitCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getUnitCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getUnitCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getAsTypeControl = (MyLib._myTextBox)_screenGeneral._getControl(_g.d.as_asset._as_type);
            __getAsTypeControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getAsTypeControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getAsTypeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getAsLocationControl = (MyLib._myTextBox)_screenGeneral._getControl(_g.d.as_asset._as_location);
            __getAsLocationControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getAsLocationControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getAsLocationControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getSideCodeControl = (MyLib._myTextBox)_screenGeneral._getControl(_g.d.as_asset._side_code);
            __getSideCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getSideCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getSideCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            // ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
            MyLib._myTextBox __getSectionCodeControl = (MyLib._myTextBox)_screenGeneral._getControl(_g.d.as_asset._department_code);
            __getSectionCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getSectionCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getSectionCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);

            MyLib._myTextBox __getUserCodeControl = (MyLib._myTextBox)_screenGeneral._getControl(_g.d.as_asset._user_code);
            __getUserCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
            __getUserCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
            __getUserCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);


            // Screen Original
            this._screenOriginal.AutoSize = true;
            this._screenOriginal._maxColumn = 2;
            this._screenOriginal._table_name = _g.d.as_asset_detail._table;
            this._screenOriginal._addDateBox(0, 0, 1, 0, _g.d.as_asset_detail._as_buy_date, 1, true);
            this._screenOriginal._addNumberBox(0, 1, 0, 0, _g.d.as_asset_detail._as_buy_price, 1, 2, true);
            this._screenOriginal._addTextBox(1, 0, 1, _g.d.as_asset_detail._as_buy_from, 2, 255);
            this._screenOriginal._addTextBox(2, 0, 1, _g.d.as_asset_detail._derived_from, 2, 255);
            this._screenOriginal._addNumberBox(3, 0, 0, 0, _g.d.as_asset_detail._as_buy_year, 1, 2, true);

            this._screenOriginal._addTextBox(4, 0, 1, _g.d.as_asset_detail._doc_buy_ref, 1, 25);
            this._screenOriginal._addDateBox(4, 1, 1, 0, _g.d.as_asset_detail._doc_buy_date, 1, true);
            this._screenOriginal._addTextBox(5, 0, 2, _g.d.as_asset_detail._as_buy_description, 2, 255);
            this._screenOriginal._refresh();
            // Event Screen Original
            this._screenOriginal._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenOriginal__saveKeyDown);
            this._screenOriginal._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenOriginal__checkKeyDown);
            this._screenOriginal._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenOriginal__checkKeyDownReturn);
            // Screen Insurance
            this._screenInsurance.AutoSize = true;
            this._screenInsurance._maxColumn = 2;
            this._screenInsurance._table_name = _g.d.as_asset_detail._table;
            this._screenInsurance._addTextBox(0, 0, 1, _g.d.as_asset_detail._insure_no, 1, 25);
            this._screenInsurance._addTextBox(0, 1, 1, _g.d.as_asset_detail._safety_code, 1, 25);
            this._screenInsurance._addTextBox(1, 0, 1, _g.d.as_asset_detail._insure_company, 2, 100);
            this._screenInsurance._addNumberBox(2, 0, 0, 0, _g.d.as_asset_detail._insure_money, 1, 2, true);
            this._screenInsurance._addNumberBox(2, 1, 0, 0, _g.d.as_asset_detail._insure_age, 1, 2, true);
            this._screenInsurance._addDateBox(3, 0, 1, 0, _g.d.as_asset_detail._insure_start_date, 1, true);
            this._screenInsurance._addDateBox(3, 1, 1, 0, _g.d.as_asset_detail._insure_stop_date, 1, true);
            this._screenInsurance._addTextBox(4, 0, 2, _g.d.as_asset_detail._remark, 2, 255);
            this._screenInsurance._refresh();
            // Event Screen Insurance
            this._screenInsurance._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenInsurance__saveKeyDown);
            this._screenInsurance._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenInsurance__checkKeyDown);
            this._screenInsurance._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenInsurance__checkKeyDownReturn);
            // Screen Sale  
            this._screenSale.AutoSize = true;
            this._screenSale._maxColumn = 2;
            this._screenSale._table_name = _g.d.as_asset_sale_detail._table;
            this._screenSale._addDateBox(0, 0, 1, 0, _g.d.as_asset_sale_detail._doc_date, 1, true, false);
            this._screenSale._addTextBox(0, 1, 1, 0, _g.d.as_asset_sale_detail._doc_no, 1, 25, 0, true, false, false);
            this._screenSale._addNumberBox(1, 0, 0, 0, _g.d.as_asset_sale_detail._as_value, 1, 2, true);
            this._screenSale._addNumberBox(1, 1, 0, 0, _g.d.as_asset_sale_detail._depreciate_value, 1, 2, true);
            this._screenSale._addNumberBox(2, 0, 0, 0, _g.d.as_asset_sale_detail._after_depreciate, 1, 2, true);
            this._screenSale._addNumberBox(2, 1, 0, 0, _g.d.as_asset_sale_detail._sale_price, 1, 2, true);
            this._screenSale._addNumberBox(3, 0, 0, 0, _g.d.as_asset_sale_detail._vat_value, 1, 2, true);
            this._screenSale._addNumberBox(3, 1, 0, 0, _g.d.as_asset_sale_detail._net_value, 1, 2, true);
            this._screenSale._addTextBox(4, 0, 2, _g.d.as_asset_sale_detail._remark, 2, 255);
            this._screenSale._refresh();
            //this._screenSale.Enabled = false;
            MyLib._myDateBox _setDocDate = (MyLib._myDateBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._doc_date);
            _setDocDate.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setDocDate.TextBox.Enabled = false;
            MyLib._myTextBox _setDocNo = (MyLib._myTextBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._doc_no);
            _setDocNo.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setDocNo.TextBox.Enabled = false;
            MyLib._myNumberBox _setAsValue = (MyLib._myNumberBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._as_value);
            _setAsValue.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setAsValue.TextBox.Enabled = false;
            MyLib._myNumberBox _setDepreciateValue = (MyLib._myNumberBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._depreciate_value);
            _setDepreciateValue.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setDepreciateValue.TextBox.Enabled = false;
            MyLib._myNumberBox _setAfterDepreciate = (MyLib._myNumberBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._after_depreciate);
            _setAfterDepreciate.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setAfterDepreciate.TextBox.Enabled = false;
            MyLib._myNumberBox _setSalePrice = (MyLib._myNumberBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._sale_price);
            _setSalePrice.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setSalePrice.TextBox.Enabled = false;
            MyLib._myNumberBox _setVatValue = (MyLib._myNumberBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._vat_value);
            _setVatValue.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setVatValue.TextBox.Enabled = false;
            MyLib._myNumberBox _setNetValue = (MyLib._myNumberBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._net_value);
            _setNetValue.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setNetValue.TextBox.Enabled = false;
            MyLib._myTextBox _setRemark = (MyLib._myTextBox)this._screenSale._getControl(_g.d.as_asset_sale_detail._remark);
            _setRemark.TextBox.BackColor = Color.FromArgb(214, 217, 227);
            _setRemark.TextBox.Enabled = false;
            // Screen Depreciation
            this._screenDepreciation.AutoSize = true;
            this._screenDepreciation._maxColumn = 2;
            this._screenDepreciation._table_name = _g.d.as_asset_detail._table;
            MyLib._myGroupBox __calcGroupBox = _screenDepreciation._addGroupBox(0, 0, 2, 2, 2, _g.d.as_asset_detail._calc_type, true);
            _screenDepreciation._addRadioButtonOnGroupBox(0, 0, __calcGroupBox, "straight_line", 0, true);
            _screenDepreciation._addRadioButtonOnGroupBox(1, 0, __calcGroupBox, "decline_balance", 1, false);
            this._screenDepreciation._addNumberBox(3, 0, 0, 0, _g.d.as_asset_detail._as_start_cut, 1, 2, true);
            this._screenDepreciation._addNumberBox(3, 1, 0, 0, _g.d.as_asset_detail._as_calc_value, 1, 2, true);
            this._screenDepreciation._addCheckBox(4, 0, _g.d.as_asset_detail._as_dead_calc, false, true);
            this._screenDepreciation._addNumberBox(4, 1, 0, 0, _g.d.as_asset_detail._as_dead_value, 1, 2, true);
            this._screenDepreciation._addNumberBox(5, 0, 0, 0, _g.d.as_asset_detail._as_age, 1, 2, true);
            this._screenDepreciation._addNumberBox(5, 1, 0, 0, _g.d.as_asset_detail._as_rate, 1, 2, true);
            this._screenDepreciation._addDateBox(6, 0, 1, 0, _g.d.as_asset_detail._start_calc_date, 1, true);
            this._screenDepreciation._addDateBox(6, 1, 1, 0, _g.d.as_asset_detail._stop_calc_date, 1, true);
            this._screenDepreciation._addNumberBox(7, 0, 0, 0, _g.d.as_asset_detail._as_value_balance, 1, 2, true);
            this._screenDepreciation._addNumberBox(7, 1, 0, 0, _g.d.as_asset_detail._as_sum_value_balance, 1, 2, true);
            this._screenDepreciation._refresh();
            // Event Screen Depreciation
            MyLib._myNumberBox _numAgeChange = (MyLib._myNumberBox)this._screenDepreciation._getControl(_g.d.as_asset_detail._as_age);
            _numAgeChange.Leave += new EventHandler(_numAgeChange_Leave);
            this._screenDepreciation._textBoxChanged += _screenDepreciation__textBoxChanged;
            this._screenDepreciation._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenDepreciation__saveKeyDown);
            this._screenDepreciation._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenDepreciation__checkKeyDown);
            this._screenDepreciation._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenDepreciation__checkKeyDownReturn);

            //if (MyLib._myGlobal._programName.Equals("Sea And Hill Account"))
            {
                tab_other = new TabPage();
                this._other_screen = new MyLib._myScreen();

                tab_other.SuspendLayout();

                this._other_screen.Dock = DockStyle.Fill;
                this._other_screen.AutoSize = true;
                this._other_screen._maxColumn = 2;
                this._other_screen._table_name = _g.d.as_asset_detail._table;
                this._other_screen._addTextBox(0, 0, 1, _g.d.as_asset_detail._asset_status, 2, 100);
                this._other_screen._addTextBox(1, 0, 1, 0, _g.d.as_asset_detail._asset_province, 1, 10, 1, true, false, true);
                this._other_screen._addTextBox(2, 0, 1, _g.d.as_asset_detail._as_model, 2, 100);
                this._other_screen._addTextBox(3, 0, 1, _g.d.as_asset_detail._as_description, 2, 100);
                this._other_screen._addTextBox(4, 0, 1, _g.d.as_asset_detail._as_price_group, 2, 100);
                this._other_screen._addTextBox(5, 0, 1, _g.d.as_asset_detail._as_budget, 2, 100);
                this._other_screen._refresh();

                this.tab_other.Controls.Add(_other_screen);
                this.tab_other.Location = new System.Drawing.Point(4, 40);
                this.tab_other.Name = "tab_other";
                this.tab_other.Size = new System.Drawing.Size(494, 335);
                this.tab_other.TabIndex = 5;
                this.tab_other.Text = "7.as_asset_detail.tab_other";
                this.tab_other.UseVisualStyleBackColor = true;

                MyLib._myTextBox __getProvinceCodeControl = (MyLib._myTextBox)_other_screen._getControl(_g.d.as_asset_detail._asset_province);
                __getProvinceCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
                __getProvinceCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
                __getProvinceCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);



                this._myTabControl1.TabPages.Add(tab_other);

                tab_other.ResumeLayout(false);
            }

            // Manage Data
            _myManageData1._displayMode = 0;
            _myManageData1._selectDisplayMode(_myManageData1._displayMode);
            _myManageData1._dataList._lockRecord = true;
            _myManageData1._dataList._loadViewFormat("screen_as_asset", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;
            _myManageData1._dataList._referFieldAdd(_g.d.as_asset._code, 1);
            // Tab Event
            this._myTabControl1.SelectedIndexChanged += new EventHandler(_myTabControl1_SelectedIndexChanged);
            // Resize
            this.Resize += new EventHandler(_as_list_Resize);
        }

        private void _screenDepreciation__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.as_asset_detail._start_calc_date))
            {
                DateTime __start_calc_date = this._screenDepreciation._getDataDate(_g.d.as_asset_detail._start_calc_date);
                // calc end date
                int __year = MyLib._myGlobal._intPhase(this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_age).ToString());
                if (__year != 0)
                {
                    DateTime __end_calc_date = __start_calc_date.AddYears(__year).AddDays(-1);
                    this._screenDepreciation._setDataDate(_g.d.as_asset_detail._stop_calc_date, __end_calc_date);
                }
            }
        }

        void _searchProvince__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);

        }

        void _searchUser__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);

        }

        void _numAgeChange_Leave(object sender, EventArgs e)
        {
            int _calcType = MyLib._myGlobal._intPhase(this._screenDepreciation._getDataStr(_g.d.as_asset_detail._calc_type));
            decimal _rate = 0;
            if (_calcType == 0)
            {
                if (this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_age) > 0)
                {
                    _rate = (decimal)100M / this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_age);
                    this._screenDepreciation._setDataNumber(_g.d.as_asset_detail._as_rate, _rate);
                }
                else
                {
                    MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึก อายุการใช้งาน ก่อน แล้วโปรแกรมจะทำการคำนวณอัตราค่าเสื่อมให้โดยอัตโนมัติ" : "Please enter age, Program will automatic calculate ratevalue");
                    this._screenDepreciation._setDataNumber(_g.d.as_asset_detail._as_rate, 0);
                }
            }
            else
            {
                if (this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_dead_value) == 0 || this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_calc_value) == 0 || this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_age) == 0)
                {
                    MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึก มูลค่าที่คิดค่าเสื่อม ราคาซาก และ อายุการใช้งาน ก่อน แล้วโปรแกรมจะทำการคำนวณอัตราค่าเสื่อมให้โดยอัตโนมัติ" : "Please enter calcvalue deadvalue and age, Program will automatic calculate ratevalue");
                    this._screenDepreciation._setDataNumber(_g.d.as_asset_detail._as_rate, 0);
                }
                else
                {
                    decimal _dDeadValue = this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_dead_value);
                    decimal _dCalcValue = this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_calc_value);
                    decimal _dAge = this._screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_age);
                    decimal _beforeStartSquareRoot = _dDeadValue / _dCalcValue;
                    decimal _yearStartForPower = (decimal)1M / _dAge;
                    decimal _depreciatePercent = (decimal)1M - (decimal)Math.Pow((double)_beforeStartSquareRoot, (double)_yearStartForPower);
                    _rate = (decimal)100M * _depreciatePercent;
                    this._screenDepreciation._setDataNumber(_g.d.as_asset_detail._as_rate, _rate);
                }
            }
        }

        void _screenDepreciation__saveKeyDown(object sender)
        {
            save_data();
        }

        void _screenInsurance__saveKeyDown(object sender)
        {
            save_data();
        }

        void _screenOriginal__saveKeyDown(object sender)
        {
            save_data();
        }

        void _screenGeneral__saveKeyDown(object sender)
        {
            save_data();
        }

        void _screenTop__saveKeyDown(object sender)
        {
            save_data();
        }

        string _autoRunning()
        {
            string __result = "";
            DataSet __getLastCode = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[0] + _g.d.as_asset._code + " from  " + _g.d.as_asset._table + " order by " + _g.d.as_asset._code + " desc " + ((string[])MyLib._myGlobal._getTopAndLimitOneRecord())[1]);
            if (__getLastCode.Tables[0].Rows.Count > 0)
            {
                __result = MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__getLastCode.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            return __result;
        }

        private void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.as_asset._code))
            {
                MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
                string __code = __textBox._textFirst;

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __code + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newArCode = _g.g._getAutoRun(_g.g._autoRunType.ว่าง, __code, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง, _g.d.as_asset._table, __getFormat.Rows[0][2].ToString(), _g.d.as_asset._code, "");
                    this._screenTop._setDataStr(_g.d.ar_customer._code, __newArCode, "", true);
                }
                else
                {
                    _autoRunning();
                }
            }
        }

        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();

        void _screenTop__textBoxSearch(object sender)
        {
            string __name = ((MyLib._myTextBox)sender)._name;
            _searchTxtName = __name;

            //string _search_text_new = _search_screen_neme(this._searchName);
            if (!this._search_data_full._name.Equals(_g.g._search_screen_erp_doc_format))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _g.g._search_screen_erp_doc_format;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                this._search_data_full._dataList._refreshData();
            }

            if (__name.Equals(_g.d.as_asset._code))
            {
                /* ย้ายไปทำตอน textchange
                string __getNewCode = _autoRunning();
                if (__getNewCode.Length > 0)
                {
                    this._screenTop._setDataStr(_g.d.as_asset._code, __getNewCode);
                }
                */
                _searchName = _g.d.as_asset._code;
                string _where = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'AS\'";
                if (_g.g._companyProfile._branchStatus == 1 || _g.g._companyProfile._change_branch_code == false)
                {
                    _where += " AND ((coalesce(" + _g.d.erp_doc_format._use_branch_select + ", 0) = 0 ) or (" + _g.d.erp_doc_format._branch_list + " like '%" + MyLib._myGlobal._branchCode + "%'))";
                }
                MyLib._myGlobal._startSearchBox(this, ((MyLib._myTextBox)sender), ((MyLib._myTextBox)sender)._labelName, this._search_data_full, false, true, _where);

            }
        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            //this._searchByParent(sender, row);

        }

        void _as_list_Resize(object sender, EventArgs e)
        {
            if (_myManageData1._dataList._loadViewDataSuccess == false)
            {
                _myManageData1._dataListOpen = true;
                _myManageData1._calcArea();
                _myManageData1._dataList._loadViewData(0);
            }
        }

        bool _screenDepreciation__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myNumberBox))
                    {
                        MyLib._myNumberBox __getTextBox = (MyLib._myNumberBox)sender;
                        if (__getTextBox._name.Equals(_g.d.as_asset_detail._as_sum_value_balance))
                        {
                            this._myTabControl1.SelectedIndex = 4;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool _screenInsurance__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.as_asset_detail._remark))
                        {
                            this._myTabControl1.SelectedIndex = 3;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool _screenOriginal__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.as_asset_detail._as_buy_description))
                        {
                            this._myTabControl1.SelectedIndex = 2;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool _screenGeneral__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.as_asset._as_location))
                        {
                            this._myTabControl1.SelectedIndex = 1;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        void _myTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._myTabControl1.SelectedIndex == 0)
            {
                _TabGeneralFocus();
            }
            else
                if (this._myTabControl1.SelectedIndex == 1)
            {
                _TabOriginalFocus();
            }
            else
                    if (this._myTabControl1.SelectedIndex == 2)
            {
                _TabInsuranceFocus();
            }
            else
                        if (this._myTabControl1.SelectedIndex == 3)
            {
                _TabDepreciationFocus();
            }
        }

        bool _screenTop__checkKeyDownReturn(object sender, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Tab)
            {
                if (sender != null)
                {
                    if (sender.GetType() == typeof(MyLib._myTextBox))
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
                        if (__getTextBox._name.Equals(_g.d.as_asset._remark))
                        {
                            this._myTabControl1.SelectedIndex = 0;
                            _TabGeneralFocus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if ((keyData & Keys.Control) == Keys.Control && (keyCode == Keys.Home))
            {
                this._screenTop._focusFirst();
                return true;
            }
            else
            {
                if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D1))
                {
                    this._myTabControl1.SelectedIndex = 0;
                    return true;
                }
                else
                {
                    if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D2))
                    {
                        this._myTabControl1.SelectedIndex = 1;
                        return true;
                    }
                    else
                    {
                        if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D3))
                        {
                            this._myTabControl1.SelectedIndex = 2;
                            return true;
                        }
                        else
                        {
                            if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D4))
                            {
                                this._myTabControl1.SelectedIndex = 3;
                                return true;
                            }
                            else
                            {
                                if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D5))
                                {
                                    this._myTabControl1.SelectedIndex = 4;
                                    return true;
                                }
                                else
                                {
                                    if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D6))
                                    {
                                        this._myTabControl1.SelectedIndex = 5;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (keyData == Keys.F12)
            {
                save_data();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _TabGeneralFocus()
        {
            this._screenGeneral._focusFirst();
        }

        void _TabOriginalFocus()
        {
            this._screenOriginal._focusFirst();
        }

        void _TabInsuranceFocus()
        {
            this._screenInsurance._focusFirst();
        }

        void _TabDepreciationFocus()
        {
            this._screenDepreciation._focusFirst();
        }

        void _searchDepartmentCode__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchSideCode__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        Boolean _screenDepreciation__checkKeyDown(object sender, Keys keyData)
        {
            if (_buttonSave.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
            }
            return true;
        }

        Boolean _screenInsurance__checkKeyDown(object sender, Keys keyData)
        {
            if (_buttonSave.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
            }
            return true;
        }

        Boolean _screenOriginal__checkKeyDown(object sender, Keys keyData)
        {
            if (_buttonSave.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
            }
            return true;
        }

        Boolean _screenGeneral__checkKeyDown(object sender, Keys keyData)
        {
            if (_buttonSave.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
            }
            return true;
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (_searchUnitCode.Visible)
                {
                    _searchUnitCode.Focus();
                    _searchUnitCode._firstFocus();
                }
                if (_searchAsType.Visible)
                {
                    _searchAsType.Focus();
                    _searchAsType._firstFocus();
                }
                if (_searchSideCode.Visible)
                {
                    _searchSideCode.Focus();
                    _searchSideCode._firstFocus();
                }
                if (_searchDepartmentCode.Visible)
                {
                    _searchDepartmentCode.Focus();
                    _searchDepartmentCode._firstFocus();
                }
                if (_searchAsLocation.Visible)
                {
                    _searchAsLocation.Focus();
                    _searchAsLocation._firstFocus();
                }

                if (_searchUser.Visible)
                {
                    _searchUser.Focus();
                    _searchUser._firstFocus();
                }

                if (_searchProvince.Visible)
                {
                    _searchProvince.Focus();
                    _searchProvince._firstFocus();

                }
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            _searchUnitCode.Visible = false;
            _searchAsType.Visible = false;
            _searchSideCode.Visible = false;
            _searchDepartmentCode.Visible = false;
            _searchAsLocation.Visible = false;
            _searchUser.Visible = false;
            _searchProvince.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _screenGeneral__textBoxSearch(__getControl);
            __getControl.textBox.Focus();
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                // Delete Master
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
                string getAssetCode = this._myManageData1._dataList._gridData._cellGet(getData.row, 1).ToString();
                // Delete Detail
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + _g.d.as_asset_detail._as_code + "=\'" + getAssetCode + "\'"), _g.d.as_asset_detail._table));
                // Delete Result
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + _g.d.as_asset_result._as_code + "=\'" + getAssetCode + "\'"), _g.d.as_asset_result._table));

            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, MyLib._myGlobal._resource("ล้มเหลว"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Boolean _screenTop__checkKeyDown(object sender, Keys keyData)
        {
            if (_buttonSave.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
            }
            else
            {
                if (keyData == Keys.PageDown)
                {
                    if (this._myTabControl1.SelectedIndex == 0)
                    {
                        _TabGeneralFocus();
                    }
                    else
                        if (this._myTabControl1.SelectedIndex == 1)
                    {
                        _TabOriginalFocus();
                    }
                    else
                            if (this._myTabControl1.SelectedIndex == 2)
                    {
                        _TabInsuranceFocus();
                    }
                    else
                                if (this._myTabControl1.SelectedIndex == 3)
                    {
                        _TabDepreciationFocus();
                    }
                }
            }
            return true;
        }

        void _searchAsLocation__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchUnitCode__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchAsType__searchEnter(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _get_column_number()
        {
            _getColumnAsCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.as_asset._table + "." + _g.d.as_asset._code);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                _get_column_number();
                _oldAsCode = __rowDataArray[_getColumnAsCode].ToString();                //
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.as_asset._table + whereString));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=\'" + _oldAsCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.as_asset_sale_detail._table + " where " + _g.d.as_asset_sale_detail._as_code + "=\'" + _oldAsCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.as_asset_maintenance_detail._table + " where " + _g.d.as_asset_maintenance_detail._as_code + "=\'" + _oldAsCode + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                // Master
                this._screenTop._loadData(((DataSet)_getData[0]).Tables[0]);
                this._screenGeneral._loadData(((DataSet)_getData[0]).Tables[0]);
                Control _codeControl = _screenTop._getControl(_g.d.as_asset._code);
                _codeControl.Enabled = false;
                // Detail
                this._screenOriginal._loadData(((DataSet)_getData[1]).Tables[0]);
                this._screenInsurance._loadData(((DataSet)_getData[1]).Tables[0]);
                this._other_screen._loadData(((DataSet)_getData[1]).Tables[0]);
                this._search(false);
                if (((DataSet)_getData[2]).Tables[0].Rows.Count > 0)
                {
                    this._screenSale._loadData(((DataSet)_getData[2]).Tables[0]);
                }
                this._screenDepreciation._loadData(((DataSet)_getData[1]).Tables[0]);
                // Result
                this._resultGrid1._myGrid1._clear();
                this._resultGrid1._myGrid1._loadFromDataTable(((DataSet)_getData[3]).Tables[0]);
                if (forEdit)
                {
                    _screenTop._focusFirst();
                }
                this._resultGrid1._myGrid1._calcTotal(false);
                this._resultGrid1._myGrid1.Invalidate();
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        void _screenGeneral__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.Equals(_g.d.as_asset._unit_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchUnitCode, false);
            }
            if (name.Equals(_g.d.as_asset._as_type))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchAsType, false);
            }
            if (name.Equals(_g.d.as_asset._side_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchSideCode, false);
            }
            if (name.Equals(_g.d.as_asset._department_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDepartmentCode, false);
            }
            if (name.Equals(_g.d.as_asset._as_location))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchAsLocation, false);
            }
            if (name.Equals(_g.d.as_asset._user_code))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchUser, false);
            }

            if (name.Equals(_g.d.as_asset_detail._asset_province))
            {
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = getControl.textBox;
                MyLib._myGlobal._startSearchBox(getControl, label_name, _searchProvince, false);

            }
        }

        void _screenGeneral__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.as_asset._unit_code) || name.Equals(_g.d.as_asset._as_type) || name.Equals(_g.d.as_asset._side_code) || name.Equals(_g.d.as_asset._department_code) || name.Equals(_g.d.as_asset._as_location))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                _search(true);
            }
        }
        /// <summary>
        /// กด Mouse ตอนค้นหา หรือ Enter ตอนค้นหา
        /// </summary>
        /// <param name="name"></param>
        /// <param name="row"></param>
        void _searchAll(string name, int row)
        {
            if (name.Equals(_g.g._search_master_ic_unit))
            {
                string result = (string)_searchUnitCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchUnitCode.Close();
                    _screenGeneral._setDataStr(_searchName, result, "", true);
                    _search(true);
                }
            }
            if (name.Equals(_g.g._search_master_as_asset_type))
            {
                string result = (string)_searchAsType._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchAsType.Close();
                    _screenGeneral._setDataStr(_searchName, result, "", true);
                    _search(true);
                }
            }
            if (name.Equals(_g.g._search_screen_erp_side_list))
            {
                string result = (string)_searchSideCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchSideCode.Close();
                    _screenGeneral._setDataStr(_searchName, result, "", true);
                    _search(true);
                }
            }
            if (name.Equals(_g.g._search_screen_erp_department_list))
            {
                string result = (string)_searchDepartmentCode._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchDepartmentCode.Close();
                    _screenGeneral._setDataStr(_searchName, result, "", true);
                    _search(true);
                }
            }
            if (name.Equals(_g.g._search_master_as_asset_location))
            {
                string result = (string)_searchAsLocation._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchAsLocation.Close();
                    _screenGeneral._setDataStr(_searchName, result, "", true);
                    _search(true);
                }
            }
            if (name.Equals(_g.g._search_screen_erp_user))
            {
                string result = (string)this._searchUser._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchUser.Close();
                    _screenGeneral._setDataStr(_searchName, result, "", true);
                    _search(true);
                }

            }

            if (name.Equals(_g.g._screen_erp_province))
            {
                string result = (string)this._searchProvince._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _searchProvince.Close();
                    _other_screen._setDataStr(_searchName, result, "", true);
                    _search(true);
                }

            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (_searchName.Equals(_g.d.as_asset._code))
            {
                MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
                MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;

                string result = (string)__getParent2._dataList._gridData._cellGet(e._row, 0);
                this._screenTop._setDataStr(_g.d.as_asset._code, result);
                __getParent2.Close();
            }
            else
            {
                MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
                MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
                _searchAll(__getParent2._name, e._row);
            }
        }

        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                if (fieldName.Equals(_g.d.as_asset_detail._asset_province))
                {
                    string getDataStr = this._other_screen._getDataStr(fieldName);
                    _other_screen._setDataStr(fieldName, getDataStr, getData, true);

                }
                else
                {
                    string getDataStr = _screenGeneral._getDataStr(fieldName);
                    _screenGeneral._setDataStr(fieldName, getDataStr, getData, true);
                }
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && _screenGeneral._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + _screenGeneral._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

        public void _search(Boolean warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + "=\'" + _screenGeneral._getDataStr(_g.d.as_asset._unit_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_type._name_1 + " from " + _g.d.as_asset_type._table + " where " + _g.d.as_asset_type._code + "=\'" + _screenGeneral._getDataStr(_g.d.as_asset._as_type) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where " + _g.d.erp_side_list._code + "=\'" + _screenGeneral._getDataStr(_g.d.as_asset._side_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + "=\'" + _screenGeneral._getDataStr(_g.d.as_asset._department_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_location._name_1 + " from " + _g.d.as_asset_location._table + " where " + _g.d.as_asset_location._code + "=\'" + _screenGeneral._getDataStr(_g.d.as_asset._as_location) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + _screenGeneral._getDataStr(_g.d.as_asset._user_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._code + "=\'" + this._other_screen._getDataStr(_g.d.as_asset_detail._asset_province) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.as_asset._unit_code, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.as_asset._as_type, (DataSet)_getData[1], warning);
                _searchAndWarning(_g.d.as_asset._side_code, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.as_asset._department_code, (DataSet)_getData[3], warning);
                _searchAndWarning(_g.d.as_asset._as_location, (DataSet)_getData[4], warning);
                _searchAndWarning(_g.d.as_asset._user_code, (DataSet)_getData[5], warning);
                _searchAndWarning(_g.d.as_asset_detail._asset_province, (DataSet)_getData[6], warning);
            }
            catch
            {
            }
        }
        void save_data()
        {
            // กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
            this._myTabControl1.Focus();
            if (_myManageData1._manageButton.Enabled)
            {
                string getEmtry = _screenTop._checkEmtryField();
                if (getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, getEmtry);
                }
                else
                {
                    // Check Calc Date
                    DateTime __dateStart = MyLib._myGlobal._convertDate(this._screenDepreciation._getDataStr(_g.d.as_asset_detail._start_calc_date));
                    DateTime __dateStop = MyLib._myGlobal._convertDate(this._screenDepreciation._getDataStr(_g.d.as_asset_detail._stop_calc_date));
                    int __dateCompare = DateTime.Compare(__dateStart, __dateStop);
                    if (__dateCompare > 0)
                    {
                        MessageBox.Show((MyLib._myGlobal._language == 0) ? "วันที่สิ้นสุดคิดค่าเสื่อมต้องมากกว่าหรือเท่ากับวันที่เริ่มคิดค่าเสื่อมเท่านั้น" : "StopCalcDate much more than or equal to StartCaclDate");
                    }
                    else
                    {
                        // Asset Master
                        ArrayList getDataTop = _screenTop._createQueryForDatabase();
                        ArrayList getDataGeneral = _screenGeneral._createQueryForDatabase();
                        // Asset Detail
                        ArrayList getDataOriginal = _screenOriginal._createQueryForDatabase();
                        ArrayList getDataInsurance = _screenInsurance._createQueryForDatabase();
                        ArrayList getDataOther = this._other_screen._createQueryForDatabase();
                        //*************
                        if (_screenDepreciation._getDataNumber(_g.d.as_asset_detail._as_dead_value) <= 0)
                        {
                            _screenDepreciation._setDataNumber(_g.d.as_asset_detail._as_dead_value, 1.0M);
                        }
                        //*************
                        ArrayList getDataDepreciation = _screenDepreciation._createQueryForDatabase();
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        if (_myManageData1._mode == 1)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + getDataTop[0].ToString() + ", " + getDataGeneral[0].ToString() + ") values (" + getDataTop[1].ToString() + ", " + getDataGeneral[1].ToString() + ")"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.as_asset_detail._table + " (" + _g.d.as_asset_detail._as_code + ", " + getDataOriginal[0].ToString() + ", " + getDataInsurance[0].ToString() + ", " + getDataDepreciation[0].ToString() + "," + getDataOther[0].ToString() + ") values (" + "\'" + _screenTop._getDataStr(_g.d.as_asset._code) + "\'" + "," + getDataOriginal[1].ToString() + ", " + getDataInsurance[1].ToString() + ", " + getDataDepreciation[1].ToString() + "," + getDataOther[1].ToString() + ")"));
                        }
                        else
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + getDataTop[2].ToString() + ", " + getDataGeneral[2].ToString() + _myManageData1._dataList._whereString));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.as_asset_detail._table + " set " + getDataOriginal[2].ToString() + ", " + getDataInsurance[2].ToString() + ", " + getDataDepreciation[2].ToString() + "," + getDataOther[2].ToString() + " where " + _g.d.as_asset_detail._as_code + "=\'" + _screenTop._getDataStr(_g.d.as_asset._code) + "\'"));
                        }
                        __myQuery.Append("</node>");
                        string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        // Show Status
                        if (result.Length == 0)
                        {
                            MyLib._myGlobal._displayWarning(1, null);
                            if (_myManageData1._mode == 1)
                            {
                                _myManageData1._afterInsertData();
                                string __getOldBuyRefer = this._screenOriginal._getDataStr(_g.d.as_asset_detail._doc_buy_ref);
                                if (this._clearDataAfterSaveButton._iconNumber == 0)
                                {
                                    _screenTop._clear();
                                    _screenGeneral._clear();
                                    _screenOriginal._clear();
                                    _screenInsurance._clear();
                                    _screenDepreciation._clear();
                                }
                                if (this._autoRunningButton._iconNumber == 0)
                                {
                                    this._screenOriginal._setDataStr(_g.d.as_asset_detail._doc_buy_ref, MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__getOldBuyRefer));
                                }
                                _screenTop._focusFirst();
                            }
                            else
                            {
                                _myManageData1._afterUpdateData();
                            }
                            _screenTop._isChange = false;
                            _screenGeneral._isChange = false;
                            _screenOriginal._isChange = false;
                            _screenInsurance._isChange = false;
                            _screenDepreciation._isChange = false;
                        }
                        else
                        {
                            MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (_screenTop._isChange || _screenGeneral._isChange || _screenOriginal._isChange || _screenInsurance._isChange || _screenDepreciation._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    _screenTop._isChange = false;
                    _screenGeneral._isChange = false;
                    _screenOriginal._isChange = false;
                    _screenInsurance._isChange = false;
                    _screenDepreciation._isChange = false;
                }
            }
            return (result);
        }

        void _myManageData1__newDataClick()
        {
            Control codeControl = _screenTop._getControl(_g.d.as_asset._code);
            codeControl.Enabled = true;
            _screenTop._focusFirst();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void _clearDataAfterSaveButton_Click(object sender, EventArgs e)
        {
            this._clearDataAfterSaveButton._iconNumber = (this._clearDataAfterSaveButton._iconNumber == 0) ? 1 : 0;
            this._clearDataAfterSaveButton.Image = imageList1.Images[this._clearDataAfterSaveButton._iconNumber];
            this._clearDataAfterSaveButton.Invalidate();
        }

        private void _autoRunningButton_Click(object sender, EventArgs e)
        {
            this._autoRunningButton._iconNumber = (this._autoRunningButton._iconNumber == 0) ? 1 : 0;
            this._autoRunningButton.Image = imageList1.Images[this._autoRunningButton._iconNumber];
            this._autoRunningButton.Invalidate();
        }
    }
}
