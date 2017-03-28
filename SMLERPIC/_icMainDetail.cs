using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icMainDetail : UserControl
    {
        string _oldCode = "";
        int _displayMode = 0;
        string _refField = "";
        SMLInventoryControl._icmainGridInteraction _icmainGridInteraction;

        public _icMainDetail(int mode)
        {
            _utility __utilty = new _utility();
            __utilty._barcodeDuplicate();
            //
            this._displayMode = mode;
            this._refField = (this._displayMode == 0) ? _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code : _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code;
            //
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myTabControlDetail.TableName = _g.d.ic_resource._table;
            this._myTabControlDetail._getResource();
            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            this._myManageDetail._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._myManageDetail._dataList._loadViewFormat((this._displayMode == 0) ? _g.g._search_screen_ic_inventory : _g.g._search_screen_ic_inventory_barcode, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageDetail._manageButton = this._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageDetail._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageDetail._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageDetail._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail._dataList._referFieldAdd((this._displayMode == 0) ? _g.d.ic_inventory._code : _g.d.ic_inventory_barcode._ic_code, 1);
            this._myManageDetail._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            //this._myManageDetail._dataList._loadViewData(0);
            this._myManageDetail._calcArea();
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._autoSizeHeight = 450;
            this._myManageDetail.Invalidate();
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            //
            // ถ้าไม่มี Item Detail ให้เพิ่มใหม่
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __queryUpdate = "insert into " + _g.d.ic_inventory_detail._table + " (" + _g.d.ic_inventory_detail._ic_code + ") " +
                "(select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._item_type + "<>5 and " +
                "not exists (select distinct " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " from " + _g.d.ic_inventory_detail._table +
                " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "))";
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __queryUpdate);
            //
            this._icmainScreenPurchaseWh._itemCode += new SMLInventoryControl._icmainScreenPurchaseWh.ItemCodeEventHandler(_icmainScreenPurchaseWh__itemCode);
            this._icmainScreenSaleWh._itemCode += new SMLInventoryControl._icmainScreenSaleWh.ItemCodeEventHandler(_icmainScreenSaleWh__itemCode);
            this._icmainScreenOutWh._itemCode += new SMLInventoryControl._icmainScreenOutWh.ItemCodeEventHandler(_icmainScreenSaleWh__itemCode);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
            {
                // remove tab drug interaction

                //this._myTabControlDetail.TabPages[this.tab_interaction.Name].remove
                _icmainGridInteraction = new SMLInventoryControl._icmainGridInteraction();
                _icmainGridInteraction.Dock = DockStyle.Fill;
                this._myTabControlDetail.TabPages[tab_interaction.Name].Controls.Add(this._icmainGridInteraction);
            }

            if (this._icmainGridInteraction == null) this._myTabControlDetail.TabPages[tab_interaction.Name].Dispose();

            this._myManageDetail._dataList._isLockDoc = true;
            if (MyLib._myGlobal._isUserLockDocument == true)
            {
                this._myManageDetail._dataList._buttonUnlockDoc.Visible = true;
                this._myManageDetail._dataList._buttonLockDoc.Visible = true;
                this._myManageDetail._dataList._separatorLockDoc.Visible = true;
            }

        }

        string _icmainScreenSaleWh__itemCode()
        {
            return this._oldCode;
        }

        string _icmainScreenPurchaseWh__itemCode()
        {
            return this._oldCode;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control && (__keyCode == Keys.B))
                {
                    SMLERPControl._barcodeSearchForm __barcodeSearch = new SMLERPControl._barcodeSearchForm();
                    __barcodeSearch.TopMost = true;
                    __barcodeSearch._textBoxBarcode.KeyDown += (s, e) =>
                    {
                        if (e.KeyData == Keys.Enter)
                        {
                            try
                            {
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                string __query = "select " + _g.d.ic_inventory_barcode._ic_code + " from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + "=\'" + __barcodeSearch._textBoxBarcode.Text.Trim().ToUpper() + "\'";
                                DataTable __findItem = __myFrameWork._queryShort(__query).Tables[0];
                                if (__findItem.Rows.Count > 0)
                                {
                                    string __itemCode = __findItem.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString();
                                    this._myManageDetail._dataList._searchText.textBox.Text = __itemCode;
                                    this._myManageDetail._dataList._refreshData();
                                }
                                __barcodeSearch._labelBarcode.Text = __barcodeSearch._textBoxBarcode.Text;
                                __barcodeSearch._textBoxBarcode.Text = "";
                                e.Handled = true;
                            }
                            catch
                            {
                            }
                        }
                    };
                    __barcodeSearch.Show();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
            int __itemCodeColumnNumber = sender._findColumnByName(this._refField);
            string __itemCode = sender._cellGet(row, __itemCodeColumnNumber).ToString();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select " + _g.d.ic_inventory._update_detail + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __itemCode.ToUpper() + "\'").Tables[0];
            int __updateDetail = MyLib._myGlobal._intPhase(__getData.Rows[0][0].ToString());
            Form __ANBridgeCenterAdmin = ((System.Windows.Forms.ContainerControl)(this)).ParentForm; //somruk
            if (__ANBridgeCenterAdmin.GetType().ToString() == "ANBridgeCenterAdmin._mainScreen")
            {
                __updateDetail = 1;
            }
            return (__updateDetail == 1) ? true : false;
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            if (this._displayMode == 0)
            {
                int __itemTypeColumnNumber = sender._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._item_type);
                if (__itemTypeColumnNumber != -1)
                {
                    if (MyLib._myGlobal._intPhase(sender._cellGet(row, __itemTypeColumnNumber).ToString()) == 5)
                    {
                        senderRow.newColor = Color.Blue;
                    }
                }
            }
            return senderRow;
        }

        string _dataList__columnFieldNameReplace(string source)
        {
            return source.Replace("sml_value_branch_code", MyLib._myGlobal._branchCode);
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
            _utility __utilty = new _utility();
            __utilty._barcodeDuplicate();
        }

        bool _myManageDetail__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._oldCode = __rowDataArray[1].ToString().ToUpper();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *" +
                    ", (select " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._have_point + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as " + _g.d.ic_inventory._have_point +
                    " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_name_merket._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_name_billing._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * ,(select " + _g.d.ic_color._name_1 + " from " + _g.d.ic_color._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_color._table + "." + _g.d.ic_color._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_color_use._table + "." + _g.d.ic_color_use._code) + " ) as " + _g.d.ic_color_use._name_1 + " from " + _g.d.ic_color_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * ,(select " + _g.d.ic_size._name_1 + " from " + _g.d.ic_size._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_size._table + "." + _g.d.ic_size._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_size_use._table + "." + _g.d.ic_size_use._code) + ") as " + _g.d.ic_size_use._name_1 + ",(select " + _g.d.ic_size._width_length_height + " from " + _g.d.ic_size._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_size._table + "." + _g.d.ic_size._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_size_use._table + "." + _g.d.ic_size_use._code) + ") as " + _g.d.ic_size_use._width_length_height + ",(select " + _g.d.ic_size._weight + " from " + _g.d.ic_size._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_size._table + "." + _g.d.ic_size._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_size_use._table + "." + _g.d.ic_size_use._code) + ") as " + _g.d.ic_size_use._weight + " from " + _g.d.ic_size_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_name_short._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_name_pos._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * ,(select " + _g.d.ic_pattern._name_1 + " from " + _g.d.ic_pattern._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_pattern._table + "." + _g.d.ic_pattern._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_pattern_use._table + "." + _g.d.ic_pattern_use._code) + ") as " + _g.d.ic_pattern_use._name_1 + ",(select " + _g.d.ic_pattern._name_2 + " from " + _g.d.ic_pattern._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_pattern._table + "." + _g.d.ic_pattern._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_pattern_use._table + "." + _g.d.ic_pattern_use._code) + ") as " + _g.d.ic_pattern_use._name_2 + "  from " + _g.d.ic_pattern_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._table + "." + _g.d.ic_inventory_replacement._ic_replace_code) + ") as " + _g.d.ic_inventory_replacement._ic_name + " from " + _g.d.ic_inventory_replacement._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._ic_replace_code) + "=\'" + _oldCode + "\'"));
                // 10
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_bundle._table + "." + _g.d.ic_inventory_bundle._ic_code + ") as " + _g.d.ic_inventory_bundle._ic_name + " from " + _g.d.ic_inventory_bundle._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_bundle._ic_code_bundle) + "=\'" + _oldCode + "\'"));
                // 11
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_suggest._table + "." + _g.d.ic_inventory_suggest._ic_code + ") as " + _g.d.ic_inventory_bundle._ic_name + " from " + _g.d.ic_inventory_suggest._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_suggest_code) + "=\'" + _oldCode + "\' or  " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select ic_code, status ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_suggest._table + "." + _g.d.ic_inventory_suggest._ic_code + ") as " + _g.d.ic_inventory_bundle._ic_name + ",line_number from " + _g.d.ic_inventory_suggest._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_suggest_code) + "=\'" + _oldCode + "\' union all  " + " select ic_suggest_code as  ic_code, status ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_suggest._table + "." + _g.d.ic_inventory_suggest._ic_suggest_code + ") as " + _g.d.ic_inventory_bundle._ic_name + ",line_number from " + _g.d.ic_inventory_suggest._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_code) + "=\'" + _oldCode + "\' order by line_number "));

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
                {
                    //12
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_druginteraction._item_code_2 + " as " + _g.d.m_druginteraction._item_code_2 + ", (select name_1 from ic_inventory where ic_inventory.code = " + _g.d.m_druginteraction._item_code_2 + ") as " + _g.d.m_druginteraction._item_name + "  from " + _g.d.m_druginteraction._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_druginteraction._item_code_1) + "=\'" + _oldCode + "\' union all select " + _g.d.m_druginteraction._item_code_1 + " as " + _g.d.m_druginteraction._item_code_2 + ", (select name_1 from ic_inventory where ic_inventory.code = " + _g.d.m_druginteraction._item_code_1 + ") as " + _g.d.m_druginteraction._item_name + "  from " + _g.d.m_druginteraction._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_druginteraction._item_code_2) + "=\'" + _oldCode + "\'"));
                }

                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainScreenDescript._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainScreenGroup._loadData(((DataSet)__getData[1]).Tables[0]);
                this._icmainScreenDimesion._loadData(((DataSet)__getData[1]).Tables[0]);
                this._icmainGridMarket._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                this._icmainGridBilling._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
                this._icmainGridColor._loadFromDataTable(((DataSet)__getData[4]).Tables[0]);
                this._icmainGridSize._loadFromDataTable(((DataSet)__getData[5]).Tables[0]);
                this._icmainScreenPurchaseWh._loadData(((DataSet)__getData[1]).Tables[0]);
                this._icmainScreenSaleWh._loadData(((DataSet)__getData[1]).Tables[0]);
                this._icmainScreenOutWh._loadData(((DataSet)__getData[1]).Tables[0]);
                this._icmainScreenGroupStatus._loadData(((DataSet)__getData[1]).Tables[0]);
                this._icmainScreenStatus._loadData(((DataSet)__getData[1]).Tables[0]);
                this._icmainScreenRemark._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainGridShortName._loadFromDataTable(((DataSet)__getData[6]).Tables[0]);
                this._icmainGridPosName._loadFromDataTable(((DataSet)__getData[7]).Tables[0]);
                this._icmainGridPattern._loadFromDataTable(((DataSet)__getData[8]).Tables[0]);
                this._icmainGridReplacement._loadFromDataTable(((DataSet)__getData[9]).Tables[0]);
                this._icmainGridBundle._loadFromDataTable(((DataSet)__getData[10]).Tables[0]);
                this._icmainGridSuggestControl._loadFromDataTable(((DataSet)__getData[11]).Tables[0]);

                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
                {
                    this._icmainGridInteraction._loadFromDataTable(((DataSet)__getData[12]).Tables[0]);
                }

                this._icmainScreenTop._search(true);
                this._icmainScreenDescript._search(true);
                this._icmainScreenGroup._search(true);
                this._icmainScreenDimesion._search(true);
                this._icmainScreenPurchaseWh._search(true);
                this._icmainScreenSaleWh._search(true);
                this._icmainScreenOutWh._search(true);
                this._icmainScreenGroupStatus._search(true);
                this._icmainScreenTop.Invalidate();
                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        void _myManageDetail__newDataClick()
        {
            _clearScreen();
            this._myManageDetail._dataList._refreshData();
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__clearData()
        {
            _clearScreen();
        }

        void _clearScreen()
        {
            this._icmainScreenTop._clear();
            this._icmainScreenDescript._clear();
            this._icmainScreenGroup._clear();
            this._icmainScreenDimesion._clear();
            this._icmainGridMarket._clear();
            this._icmainGridBilling._clear();
            this._icmainGridColor._clear();
            this._icmainGridSize._clear();
            this._icmainScreenPurchaseWh._clear();
            this._icmainScreenSaleWh._clear();
            this._icmainScreenOutWh._clear();
            this._icmainScreenGroupStatus._clear();
            this._icmainScreenStatus._clear();
            this._icmainScreenRemark._clear();
            this._icmainGridShortName._clear();
            this._icmainGridPosName._clear();
            this._icmainGridPattern._clear();
            this._icmainGridReplacement._clear();
            this._icmainGridBundle._clear();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _saveData()
        {
            this._icmainGridReplacement._removeLastControl();
            this._icmainGridBundle._removeLastControl();
            try
            {
                string __getEmtry = this._icmainScreenTop._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    if (_myManageDetail._mode == 1)  // insert
                    {

                    }
                    else // update
                    {
                        string __itemCode = this._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code).ToString().ToUpper();

                        // Update
                        string __fieldList = _g.d.ic_inventory_detail._ic_code + " , ";
                        string __dataList = this._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                        ArrayList __getDescript = this._icmainScreenDescript._createQueryForDatabase();
                        ArrayList __getRemark = this._icmainScreenRemark._createQueryForDatabase();
                        ArrayList __getGroup = this._icmainScreenGroup._createQueryForDatabase();
                        ArrayList __getDimension = this._icmainScreenDimesion._createQueryForDatabase();
                        ArrayList __getPuechaseWh = this._icmainScreenPurchaseWh._createQueryForDatabase();
                        ArrayList __getSaleWh = this._icmainScreenSaleWh._createQueryForDatabase();
                        ArrayList __getOutWh = this._icmainScreenOutWh._createQueryForDatabase();
                        ArrayList __getGroupStatus = this._icmainScreenGroupStatus._createQueryForDatabase();
                        ArrayList __getStatus = this._icmainScreenStatus._createQueryForDatabase();
                        this._icmainGridBilling._updateRowIsChangeAll(true);
                        this._icmainGridMarket._updateRowIsChangeAll(true);
                        this._icmainGridColor._updateRowIsChangeAll(true);
                        this._icmainGridSize._updateRowIsChangeAll(true);
                        this._icmainGridShortName._updateRowIsChangeAll(true);
                        this._icmainGridPosName._updateRowIsChangeAll(true);
                        this._icmainGridPattern._updateRowIsChangeAll(true);
                        this._icmainGridReplacement._updateRowIsChangeAll(true);
                        this._icmainGridBundle._updateRowIsChangeAll(true);
                        this._icmainGridSuggestControl._updateRowIsChangeAll(true);
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
                        {
                            this._icmainGridInteraction._updateRowIsChangeAll(true);
                        }

                        string __whereString = (this._displayMode == 0) ? this._myManageDetail._dataList._whereString : this._myManageDetail._dataList._whereString.Replace(this._refField, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory._table + " set " + __getDescript[2].ToString() + __whereString));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory._table + " set " + __getRemark[2].ToString() + __whereString));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory_detail._table + " set " + __getGroup[2].ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory_detail._table + " set " + __getDimension[2].ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory_detail._table + " set " + __getPuechaseWh[2].ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory_detail._table + " set " + __getSaleWh[2].ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory_detail._table + " set " + __getOutWh[2].ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory_detail._table + " set " + __getGroupStatus[2].ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory_detail._table + " set " + __getStatus[2].ToString() + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_name_billing._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridBilling._createQueryForInsert(_g.d.ic_name_billing._table, __fieldList, __dataList));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_name_merket._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridMarket._createQueryForInsert(_g.d.ic_name_merket._table, __fieldList, __dataList));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_color_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridColor._createQueryForInsert(_g.d.ic_color_use._table, __fieldList, __dataList));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_size_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridSize._createQueryForInsert(_g.d.ic_size_use._table, __fieldList, __dataList));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_name_short._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridShortName._createQueryForInsert(_g.d.ic_name_short._table, __fieldList, __dataList));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_name_pos._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridPosName._createQueryForInsert(_g.d.ic_name_pos._table, __fieldList, __dataList));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_pattern_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridPattern._createQueryForInsert(_g.d.ic_pattern_use._table, __fieldList, __dataList));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_inventory_replacement._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._ic_replace_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridReplacement._createQueryForInsert(_g.d.ic_inventory_replacement._table, _g.d.ic_inventory_replacement._ic_replace_code + ",", __dataList, false, true));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_inventory_bundle._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_bundle._ic_code_bundle) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridBundle._createQueryForInsert(_g.d.ic_inventory_bundle._table, _g.d.ic_inventory_bundle._ic_code_bundle + ",", __dataList, false, true));

                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_inventory_suggest._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_suggest_code) + " = " + __itemCode + " or " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_code) + " = " + __itemCode));
                        __myQuery.Append(this._icmainGridSuggestControl._createQueryForInsert(_g.d.ic_inventory_suggest._table, _g.d.ic_inventory_suggest._ic_suggest_code + ",", __dataList, false, true));

                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.m_druginteraction._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_druginteraction._item_code_1) + " = " + __itemCode + " or " + MyLib._myGlobal._addUpper(_g.d.m_druginteraction._item_code_2) + " = " + __itemCode));
                            __myQuery.Append(this._icmainGridInteraction._createQueryForInsert(_g.d.m_druginteraction._table, _g.d.m_druginteraction._item_code_1 + ",", __dataList, false, false));
                        }

                    }
                    __myQuery.Append("</node>");

                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length == 0)
                    {
                        SMLProcess._docFlow __docFlow = new SMLProcess._docFlow();
                        __docFlow._processAll(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, "", "");
                        MyLib._myGlobal._displayWarning(1, null);
                        if (this._myManageDetail._mode == 1)
                        {
                            this._myManageDetail._afterInsertData();
                            _clearScreen();
                            this._icmainScreenTop._focusFirst();
                        }
                        else
                        {
                            this._myManageDetail._afterUpdateData();
                        }
                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
        }
    }
}
