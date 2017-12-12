using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace SMLERPIC
{
    public partial class _icMain : UserControl
    {
        _icMainControl _icMainPanel;
        string _oldCode = "";
        int _getColumnCode = 0;
        int _displayMode = 0;
        string _refField = "";
        string _menuName = "";

        System.Json.JsonObject _oldData;
        public bool _syncModeResult = false;
        public bool _syncMode
        {
            get
            {
                return this._syncModeResult;
            }
            set
            {
                this._syncModeResult = value;
                this._activeSyncMode();
            }
        }

        public void _activeSyncMode()
        {
            this._icMainPanel.splitContainer1.Panel2Collapsed = true;
        }

        public _icMain(int mode)
        {
            _utility __utilty = new _utility();
            __utilty._barcodeDuplicate();
            //
            this._displayMode = mode;
            this._refField = (this._displayMode == 0) ? _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code : _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code;
            this._icMainPanel = new _icMainControl(mode);
            this._icMainPanel.Dock = DockStyle.Fill;
            //
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.SuspendLayout();
            this._myManageMain._dataList._lockRecord = true;
            this._myManageMain._autoSize = true;
            this._myManageMain._displayMode = 0;
            this._myManageMain._selectDisplayMode(this._myManageMain._displayMode);
            this._myManageMain._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this._myManageMain._dataList._loadViewFormat("screen_mini_ic_inventory", MyLib._myGlobal._userSearchScreenGroup, true);
                this._myManageMain._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            }
            else
            {
                this._myManageMain._dataList._loadViewFormat((this._displayMode == 0) ? _g.g._search_screen_ic_inventory : _g.g._search_screen_ic_inventory_barcode, MyLib._myGlobal._userSearchScreenGroup, true);
                this._myManageMain._dataList._referFieldAdd((this._displayMode == 0) ? _g.d.ic_inventory._code : _g.d.ic_inventory_barcode._ic_code, 1);
            }
            this._myManageMain._manageButton = this._myToolBar;
            this._myManageMain._manageBackgroundPanel = this._myPanel1;
            this._myManageMain._autoSizeHeight = 450;
            this._myManageMain._dataListOpen = true;
            this._myManageMain._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageMain__loadDataToScreen);
            this._myManageMain._closeScreen += new MyLib.CloseScreenEvent(_myManageMain__closeScreen);
            this._myManageMain._newDataClick += new MyLib.NewDataEvent(_myManageMain__newDataClick);
            this._myManageMain._newDataFromTempClick += new MyLib.NewDataFromTempEvent(_myManageMain__newDataFromTempClick);
            this._myManageMain._clearData += new MyLib.ClearDataEvent(_myManageMain__clearData);
            this._myManageMain._discardData += new MyLib.DiscardDataEvent(_myManageMain__discardData);
            this._myManageMain._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageMain._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            this._myPanel1.Controls.Add(this._icMainPanel);
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._icMainPanel._saveData += new _icMainControl.SaveEventHandler(_icMainPanel__saveData);
            this._icMainPanel._icmainGridBarCode._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridBarCode__queryForInsertCheck);
            this.ResumeLayout(false);
            //
            _g._utils __utils = new _g._utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
            __thread.Start();

            this._myManageMain._dataList._isLockDoc = true;
            if (MyLib._myGlobal._isUserLockDocument == true)
            {
                this._myManageMain._dataList._buttonUnlockDoc.Visible = true;
                this._myManageMain._dataList._buttonLockDoc.Visible = true;
                this._myManageMain._dataList._separatorLockDoc.Visible = true;
            }

            this.Disposed += new EventHandler(_icMain_Disposed);
        }

        public void _createLog(int mode)
        {
            System.Json.JsonObject __icMainJson = this._icMainPanel._icmainScreenTop._getJson();
            System.Json.JsonObject __icMainGL = this._icMainPanel._icmainScreenAccount._getJson();
            System.Json.JsonObject __icMainOther = this._icMainPanel._icmainScreenMoreControl._getJson();
            System.Json.JsonArray __icUnitUse = this._icMainPanel._icmainGridUnit._getJson("unit_use");
            System.Json.JsonArray __icBarcode = this._icMainPanel._icmainGridBarCode._getJson("barcode");
            System.Json.JsonArray __icWhShelf = this._icMainPanel._icmainGridBranch._getJson("wh_shelf");

            System.Json.JsonObject __icJson = new System.Json.JsonObject();
            __icJson.AddRange(__icMainJson);
            __icJson.AddRange(__icMainGL);
            __icJson.AddRange(__icMainOther);

            __icJson.Add("unit_use", __icUnitUse);
            __icJson.Add("barcode", __icBarcode);
            __icJson.Add("wh_shelf", __icWhShelf);

            var encoding = new UTF8Encoding();
            string __oldDataString = (this._oldData == null) ? "" : this._oldData.ToString();
            string __newDataString = __icJson.ToString();

            byte[] __oldData = encoding.GetBytes(__oldDataString); //.Substring(1, __oldDataString.Length - 2));
            byte[] __newData = encoding.GetBytes(__newDataString); //.Substring(1, __newDataString.Length - 2));

            String __saveLogQuery = ("insert into " + _g.d.master_logs._table + "(" + _g.d.master_logs._function_code + "," + _g.d.master_logs._computer_name + "," + _g.d.master_logs._guid +
                "," + _g.d.master_logs._menu_name + "," + _g.d.master_logs._screen_code + "," + _g.d.master_logs._function_type + "," + _g.d.master_logs._user_code +
                "," + _g.d.logs._date_time + "," + _g.d.master_logs._code + "," + _g.d.master_logs._name_1 +
                "," + _g.d.master_logs._data1 + "," + _g.d.master_logs._data2 +
                ") values (" + mode.ToString() + ", \'" + SystemInformation.ComputerName + "\', \'" + Guid.NewGuid().ToString("N") + "\'" +
                ",\'" + this._menuName + "\', \'" + this._displayMode.ToString() + "\', 1, \'" + MyLib._myGlobal._userCode + "\' " +
                ",\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")) + "\', \'" + this._icMainPanel._icmainScreenTop._getDataStr(_g.d.ic_inventory._code) + "\', \'" + this._icMainPanel._icmainScreenTop._getDataStr(_g.d.ic_inventory._name_1) + "\'" +
                ", ?, ?) ");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __resultLog = __myFrameWork._queryByteData(MyLib._myGlobal._databaseName, __saveLogQuery, new object[] { __newData, __oldData });
            if (__resultLog.Length != 0)
            {
                MessageBox.Show(__resultLog, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _myManageMain__newDataFromTempClick()
        {
            Control __codeControl = this._icMainPanel._icmainScreenTop._getControl(_g.d.ic_inventory._code);
            __codeControl.Enabled = true;
            this._icMainPanel._newDataFromTemp();
            this._icMainPanel._icmainScreenTop._focusFirst();
        }

        void _icMain_Disposed(object sender, EventArgs e)
        {
            _g._utils __utils = new _g._utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
            __thread.Start();
            _utility __utilty = new _utility();
            __utilty._barcodeDuplicate();
        }

        bool _icmainGridBarCode__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((this._icMainPanel._icmainGridBarCode._cellGet(row, _g.d.ic_inventory_barcode._barcode).ToString().Trim().Length == 0) ? false : true);
        }

        void _icMainPanel__saveData(bool clear)
        {
            this._saveData(clear);
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
                                    this._myManageMain._dataList._searchText.textBox.Text = __itemCode;
                                    this._myManageMain._dataList._refreshData();
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

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                if (this._displayMode == 0)
                {
                    StringBuilder __myQuery = new StringBuilder();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                    {
                        MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                        int __getColumnCode = _myManageMain._dataList._gridData._findColumnByName(this._refField);
                        string __getItemCode = this._myManageMain._dataList._gridData._cellGet(__getData.row, __getColumnCode).ToString().ToUpper();

                        //
                        Boolean __pass = true;
                        {
                            string __quey = "select count(*) as countx from " + _g.d.ic_trans_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_detail._item_code) + "=\'" + MyLib._myUtil._convertTextToXml(__getItemCode).ToUpper() + "\'";
                            DataTable __getCount = __myFrameWork._queryShort(__quey).Tables[0];
                            int __count = (int)MyLib._myGlobal._decimalPhase(__getCount.Rows[0][0].ToString());
                            if (__count > 0)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("สินค้ามีการใช้ไปแล้ว") + " " + MyLib._myGlobal._resource("ห้ามลบ") + " : " + __getItemCode);
                                __pass = false;
                            }
                        }
                        //
                        if (__pass)
                        {
                            // ลบรายละเอียดที่เกี่ยวข้อง
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory._table + " " + __getData.whereString));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_unit_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit_use._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_opposite_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_opposite_unit._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_wh_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_wh_shelf._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_name_billing._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_name_billing._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_name_merket._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_name_merket._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_color_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_color_use._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_size_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_size_use._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_name_short._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_name_short._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_name_pos._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_name_pos._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_pattern_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_pattern_use._ic_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_set_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._ic_set_code) + " = \'" + __getItemCode + "\'"));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " = \'" + __getItemCode + "\'"));

                            string __itemName = (this._displayMode == 0) ? _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1 : _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._description;

                            String __saveLogQuery = ("insert into " + _g.d.master_logs._table + "(" + _g.d.master_logs._function_code + "," + _g.d.master_logs._computer_name + "," + _g.d.master_logs._guid +
                                "," + _g.d.master_logs._menu_name + "," + _g.d.master_logs._screen_code + "," + _g.d.master_logs._function_type + "," + _g.d.master_logs._user_code +
                                "," + _g.d.logs._date_time + "," + _g.d.master_logs._code + "," + _g.d.master_logs._name_1 +
                                ") values (3, \'" + SystemInformation.ComputerName + "\', \'" + Guid.NewGuid().ToString("N") + "\'" +
                                ",\'" + this._menuName + "\', \'" + this._displayMode.ToString() + "\', 1, \'" + MyLib._myGlobal._userCode + "\' " +
                                ",\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")) + "\', \'" + __getItemCode + "\', \'" + this._myManageMain._dataList._gridData._cellGet(__getData.row, __itemName).ToString() + "\'" +
                                ") ");

                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__saveLogQuery));
                        }
                    }



                    __myQuery.Append("</node>");

                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(0, null);
                        this._myManageMain._dataList._refreshData();
                        this._icMainPanel._icmainScreenTop._clear();
                        this._icMainPanel._icmainGridUnit._clear();
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            // หน่วยนับขนาน
                            this._icMainPanel._icmainGridUnitOpposi._clear();
                        }
                        this._icMainPanel._icmainGridBranch._clear();
                        this._icMainPanel._icmainScreenAccount._clear();
                        this._icMainPanel._icmainScreenTop._focusFirst();
                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("หน้าจอนี้ห้ามลบข้อมูล");
                }
            }
        }

        void _myManageMain__newDataClick()
        {
            Control __codeControl = this._icMainPanel._icmainScreenTop._getControl(_g.d.ic_inventory._code);
            __codeControl.Enabled = true;
            this._icMainPanel._newData();
            this._icMainPanel._icmainScreenTop._focusFirst();
        }

        bool _myManageMain__discardData()
        {
            bool result = true;
            if (this._myToolBar.Enabled && (
                this._icMainPanel._icmainScreenTop._isChange ||
                this._icMainPanel._icmainGridUnit._isChange ||
                this._icMainPanel._icmainGridBranch._isChange ||
                ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional) && this._icMainPanel._icmainGridUnitOpposi._isChange) ||
                this._icMainPanel._icmainScreenAccount._isChange))
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._icMainPanel._icmainScreenTop._isChange = false;
                }
            }
            return (result);
        }

        void _myManageMain__clearData()
        {
            this._icMainPanel._clear();
        }

        bool _myManageMain__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                _get_column_number();
                this._icMainPanel._clear();
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._oldCode = __rowDataArray[_getColumnCode].ToString();
                string __whereString = (this._displayMode == 0) ? whereString : whereString.Replace(this._refField, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                // 0
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * " +
                    ", (select " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._have_point + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as " + _g.d.ic_inventory._have_point +
                    ", (select " + _g.d.ic_unit_use._width_length_height + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + _g.d.ic_inventory._width_length_height +
                    ", (select " + _g.d.ic_unit_use._weight + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + _g.d.ic_inventory._weight +
                    //", (select " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._have_point + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as " + _g.d.ic_inventory._have_point + 
                    " from " + _g.d.ic_inventory._table + __whereString));
                // 1
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._icMainPanel._icmainGridUnit._createQueryForLoad(_oldCode)));
                // 2
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table
                    + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_opposite_unit._table + "." + _g.d.ic_opposite_unit._unit_code) + ") as " + _g.d.ic_opposite_unit._unit_name
                    + " from " + _g.d.ic_opposite_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode.ToUpper() + "\'"));
                // 3
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._icMainPanel._icmainGridBranch._createQueryForLoad(_oldCode)));
                // 4
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + _oldCode.ToUpper() + "\'"));
                // 5
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + "=\'" + _oldCode.ToUpper() + "\' order by (select " + _g.d.ic_unit_use._ratio + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + ")"));
                // 6
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_standard_cost._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_standard_cost._ic_code) + "=\'" + _oldCode.ToUpper() + "\'"));
                __myquery.Append("</node>");

                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icMainPanel._icmainScreenTop._clear();
                this._icMainPanel._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icMainPanel._icmainScreenMoreControl._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icMainPanel._icmainGridUnit._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                    MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                {
                    // หน่วยนับขนาน
                    this._icMainPanel._icmainGridUnitOpposi._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                }
                this._icMainPanel._icmainGridBranch._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
                this._icMainPanel._icmainScreenAccount._loadData(((DataSet)__getData[4]).Tables[0]);
                this._icMainPanel._icmainGridBarCode._loadFromDataTable(((DataSet)__getData[5]).Tables[0]);
                this._icMainPanel._icStandardCost._loadFromDataTable(((DataSet)__getData[6]).Tables[0]);
                //
                this._icMainPanel._icmainScreenTop._search(true);
                this._icMainPanel._icmainScreenAccount._search(true);
                ((Control)this._icMainPanel._icmainScreenTop._getControl(_g.d.ic_inventory._code)).Enabled = false;
                this._icMainPanel._icmainScreenTop.Invalidate();
                __result = true;
                this._icMainPanel._processPack();

                // pack json log
                System.Json.JsonObject __icMainJson = this._icMainPanel._icmainScreenTop._getJson();
                System.Json.JsonObject __icMainGL = this._icMainPanel._icmainScreenAccount._getJson();
                System.Json.JsonObject __icMainOther = this._icMainPanel._icmainScreenMoreControl._getJson();
                System.Json.JsonArray __icUnitUse = this._icMainPanel._icmainGridUnit._getJson("unit_use");
                System.Json.JsonArray __icBarcode = this._icMainPanel._icmainGridBarCode._getJson("barcode");
                System.Json.JsonArray __icWhShelf = this._icMainPanel._icmainGridBranch._getJson("wh_shelf");

                System.Json.JsonObject __icJson = new System.Json.JsonObject();
                __icJson.AddRange(__icMainJson);
                __icJson.AddRange(__icMainGL);
                __icJson.AddRange(__icMainOther);

                __icJson.Add("unit_use", __icUnitUse);
                __icJson.Add("barcode", __icBarcode);
                __icJson.Add("wh_shelf", __icWhShelf);

                this._oldData = __icJson;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        void _get_column_number()
        {
            _getColumnCode = this._myManageMain._dataList._gridData._findColumnByName(this._refField);
        }

        void _myManageMain__closeScreen()
        {
            // Update ชื่อหน่วยนับ
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            String __unitNameQuery = "coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._table + "." + _g.d.ic_unit._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard) + "),'')";
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._unit_standard_name +
                "=" + __unitNameQuery + " where " + _g.d.ic_inventory._unit_standard_name + " is null or " + _g.d.ic_inventory._unit_standard_name + "<>" + __unitNameQuery);
            //
            Thread __thread = new Thread(new ThreadStart(_changeUnitCodeRatio));
            __thread.Start();

            this.Dispose();
        }

        public void _changeUnitCodeRatio()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_unit_use set stand_value=1 where stand_value=0 or stand_value is null");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_unit_use set divide_value=1 where divide_value=0 or divide_value is null");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set stand_value=1 where stand_value <> 1 and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=0)");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set divide_value=1 where divide_value <> 1 and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=0)");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set stand_value=(select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                    " where stand_value<>(select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                    " and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=1)");
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set divide_value=(select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                    " where divide_value<>(select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_trans_detail.item_code and ic_unit_use.code=ic_trans_detail.unit_code) " +
                    " and ic_trans_detail.item_code in (select code from ic_inventory where ic_inventory.unit_type=1)");
            //
            __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans_detail set average_cost_1 = 0 where average_cost_1 is null ");
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData(true);
        }

        private void _saveData(Boolean clearData)
        {
            if (this._myToolBar.Enabled == true)
            {
                if (MyLib._myGlobal._checkChangeMaster())
                {
                    try
                    {
                        string __getEmtry = this._icMainPanel._icmainScreenTop._checkEmtryField() + this._icMainPanel._icmainScreenAccount._checkEmtryField();
                        if (__getEmtry.Length > 0)
                        {
                            MyLib._myGlobal._displayWarning(2, __getEmtry);
                        }
                        else
                        {
                            Boolean __pass = true;
                            // ตรวจสอบหน่วยต้นทุน
                            Boolean __findUnit1 = false;
                            string __getUnit1 = this._icMainPanel._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_cost);
                            if (__getUnit1.Length > 0)
                            {
                                for (int __row = 0; __row < this._icMainPanel._icmainGridUnit._rowData.Count; __row++)
                                {
                                    string __getUnitCompare = this._icMainPanel._icmainGridUnit._cellGet(__row, _g.d.ic_unit._code).ToString();
                                    if (__getUnit1.Equals(__getUnitCompare))
                                    {
                                        __findUnit1 = true;
                                        break;
                                    }
                                }
                            }
                            if (__findUnit1 == false)
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("ไม่พบหน่วยต้นทุนในตารางหน่วยนับ"));
                                __pass = false;
                            }
                            else
                            {
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                                {
                                    __pass = true;
                                }
                                else
                                {
                                    // ตรวจสอบหน่วยมาตรฐาน
                                    Boolean __findUnit2 = false;
                                    string __getUnit2 = this._icMainPanel._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_standard);
                                    if (__getUnit2.Length > 0)
                                    {
                                        for (int __row = 0; __row < this._icMainPanel._icmainGridUnit._rowData.Count; __row++)
                                        {
                                            string __getUnitCompare = this._icMainPanel._icmainGridUnit._cellGet(__row, _g.d.ic_unit._code).ToString();
                                            if (__getUnit2.Equals(__getUnitCompare))
                                            {
                                                __findUnit2 = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (__findUnit2 == false)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบหน่วยมาตรฐานในตารางหน่วยนับ"));
                                        __pass = false;
                                    }
                                    else
                                    {
                                        // ตรวจสอบ wharehouse+shelf
                                        if (this._syncMode == false)
                                        {
                                            Boolean __findWhShef = false;
                                            for (int __row = 0; __row < this._icMainPanel._icmainGridBranch._rowData.Count; __row++)
                                            {
                                                string __getWhCode = this._icMainPanel._icmainGridBranch._cellGet(__row, _g.d.ic_wh_shelf._wh_code).ToString().Trim();
                                                string __getShelfCode = this._icMainPanel._icmainGridBranch._cellGet(__row, _g.d.ic_wh_shelf._shelf_code).ToString().Trim();
                                                if (__getWhCode.Length > 0 && __getWhCode.Length > 0)
                                                {
                                                    __findWhShef = true;
                                                    break;
                                                }
                                            }
                                            if (__findWhShef == false)
                                            {
                                                Form __ANBridgeCenterAdmin = ((System.Windows.Forms.ContainerControl)(this._icMainPanel._icmainGridBranch)).ParentForm; //somruk
                                                if (__ANBridgeCenterAdmin.GetType().ToString() == "ANBridgeCenterAdmin._mainScreen")
                                                {
                                                    __pass = true;
                                                }
                                                else
                                                {
                                                    MessageBox.Show(MyLib._myGlobal._resource("ยังไม่บันทึกคลังที่เก็บ"));
                                                    __pass = false;
                                                }

                                            }
                                        }
                                    }
                                }
                            }

                            // get iitem color
                            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                            {
                                string __getItemType = this._icMainPanel._icmainScreenTop._getDataStr(_g.d.ic_inventory._item_type);
                                if (__getItemType == "5" && _g.g._companyProfile._add_item_color == false && this._myManageMain._mode == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("ไม่อนุญาติให้เพิ่มสูตรสี"));
                                    __pass = false;
                                }
                            }
                            //
                            if (__pass)
                            {
                                // toe fix 
                                this._icMainPanel._icmainGridUnit._removeLastControl();
                                this._icMainPanel._icmainGridBarCode._removeLastControl();

                                // Save
                                StringBuilder __myQuery = new StringBuilder();
                                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_inventory_detail where not exists (select code from ic_inventory where ic_inventory.code=ic_inventory_detail.ic_code)"));
                                ArrayList __getDataScreenTop = this._icMainPanel._icmainScreenTop._createQueryForDatabase(); //ic_inventory    
                                ArrayList __getDataScreenMore = this._icMainPanel._icmainScreenMoreControl._createQueryForDatabase();

                                // ArrayList __getDataScreenAccount = this._icmainScreenAccount._createQueryForDatabase(); //ic Account                anek
                                string __fieldList = _g.d.ic_inventory_detail._ic_code + " , ";
                                string __dataList = this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                                string __itemCode = this._icMainPanel._icmainScreenTop._getDataStr(_g.d.ic_inventory._code);

                                string __extraField = "";
                                string __extraValue = "";
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                                {
                                    __extraField = "," + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._unit_standard, _g.d.ic_inventory._update_detail, _g.d.ic_inventory._update_price);
                                    __extraValue = "," + MyLib._myGlobal._fieldAndComma("\'" + this._icMainPanel._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_cost) + "\'", "1", "1");
                                }

                                if (this._myManageMain._mode == 1)
                                {
                                    this._icMainPanel._icmainGridUnit._updateRowIsChangeAll(true);
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory._table + " (" + __getDataScreenTop[0].ToString() + "," + __getDataScreenMore[0].ToString() + __extraField + ") values (" + __getDataScreenTop[1].ToString() + "," + __getDataScreenMore[1].ToString() + __extraValue + ")"));
                                    __myQuery.Append(this._icMainPanel._icmainGridUnit._createQueryForInsert(_g.d.ic_unit_use._table, __fieldList, __dataList));
                                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                                    {
                                        // หน่วยนับขนาน
                                        this._icMainPanel._icmainGridUnitOpposi._updateRowIsChangeAll(true);
                                        __myQuery.Append(this._icMainPanel._icmainGridUnitOpposi._createQueryForInsert(_g.d.ic_opposite_unit._table, __fieldList, __dataList));
                                    }
                                    // คลังที่เก็บ
                                    if (this._syncMode == false)
                                    {
                                        this._icMainPanel._icmainGridBranch._updateRowIsChangeAll(true);
                                        __myQuery.Append(this._icMainPanel._icmainGridBranch._createQueryForInsert(_g.d.ic_wh_shelf._table, __fieldList, __dataList));
                                    }
                                    // __myQuery.Append("<query>insert into " + _g.d.ic_inventory_detail._table + " (" + __fieldList.ToString() + __getDataScreenAccount[0].ToString() + ") values (" + __dataList.ToString() + __getDataScreenAccount[1].ToString() + ")</query>");
                                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                                    {
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_detail._table + " (" + _g.d.ic_inventory_detail._ic_code + "," + _g.d.ic_inventory_detail._have_point + ") values (" + this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code) + ",0)"));
                                    }
                                    else
                                    {
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_detail._table + " (" + _g.d.ic_inventory_detail._ic_code + "," + _g.d.ic_inventory_detail._have_point + ") values (" + this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code) + "," + this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._have_point) + ")"));
                                    }
                                    // BarCode
                                    if (this._syncMode == false)
                                    {
                                        this._icMainPanel._icmainGridBarCode._updateRowIsChangeAll(true);
                                        __myQuery.Append(this._icMainPanel._icmainGridBarCode._createQueryForInsert(_g.d.ic_inventory_barcode._table, __fieldList, __dataList, false));
                                    }
                                    // StandardCost
                                    this._icMainPanel._icStandardCost._updateRowIsChangeAll(true);
                                    __myQuery.Append(this._icMainPanel._icStandardCost._createQueryForInsert(_g.d.ic_standard_cost._table, __fieldList, __dataList, false));
                                }
                                else
                                {
                                    string __whereString = (this._displayMode == 0) ? this._myManageMain._dataList._whereString : this._myManageMain._dataList._whereString.Replace(this._refField, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + __getDataScreenTop[2].ToString() + "," + __getDataScreenMore[2].ToString() + " " + __whereString));
                                    //__myQuery.Append("<query>update " + _g.d.ic_inventory_detail._table + " set " + __getDataScreenAccount[2].ToString() + " where " + _g.d.ic_inventory_detail._ic_code + "=\'" + _oldCode + "\'</query>");
                                    //__myQuery.Append("<query>update " + _g.d.ic_inventory_detail._table + " set " + __getDataScreenAccount[2].ToString() + " where " + _g.d.ic_inventory_detail._ic_code + "=\'" + _oldCode + "\'</query>");
                                    this._icMainPanel._icmainGridUnit._updateRowIsChangeAll(true);
                                    //string __fieldUpdate = _g.d.ic_inventory_detail._ic_code + "=" + this._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code);
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_unit_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " in ( \'" + _oldCode.ToUpper() + "\',\'" + __itemCode.ToUpper() + "\')"));
                                    __myQuery.Append(this._icMainPanel._icmainGridUnit._createQueryForInsert(_g.d.ic_unit_use._table, __fieldList, __dataList, false));
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_opposite_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " in ( \'" + _oldCode.ToUpper() + "\',\'" + __itemCode.ToUpper() + "\')"));
                                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                                    {
                                        // หน่วยนับขนาน
                                        this._icMainPanel._icmainGridUnitOpposi._updateRowIsChangeAll(true);
                                        __myQuery.Append(this._icMainPanel._icmainGridUnitOpposi._createQueryForInsert(_g.d.ic_opposite_unit._table, __fieldList, __dataList, false));
                                    }
                                    // คลังที่เก็บ
                                    if (this._syncMode == false)
                                    {
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_wh_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " in ( \'" + _oldCode.ToUpper() + "\',\'" + __itemCode.ToUpper() + "\')"));
                                        this._icMainPanel._icmainGridBranch._updateRowIsChangeAll(true);
                                        __myQuery.Append(this._icMainPanel._icmainGridBranch._createQueryForInsert(_g.d.ic_wh_shelf._table, __fieldList, __dataList, false));

                                        // BarCode
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " in ( \'" + _oldCode.ToUpper() + "\',\'" + __itemCode.ToUpper() + "\')"));
                                        this._icMainPanel._icmainGridBarCode._updateRowIsChangeAll(true);
                                        __myQuery.Append(this._icMainPanel._icmainGridBarCode._createQueryForInsert(_g.d.ic_inventory_barcode._table, __fieldList, __dataList, false));
                                    }
                                        // StandardCost
                                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_standard_cost._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_standard_cost._ic_code) + " in ( \'" + _oldCode.ToUpper() + "\',\'" + __itemCode.ToUpper() + "\')"));
                                    this._icMainPanel._icStandardCost._updateRowIsChangeAll(true);
                                    __myQuery.Append(this._icMainPanel._icStandardCost._createQueryForInsert(_g.d.ic_standard_cost._table, __fieldList, __dataList, false));

                                    // toe update have_point
                                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                                    {
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory_detail._table + " set " + _g.d.ic_inventory_detail._have_point + "=0 where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + __itemCode + "\'"));
                                    }
                                    else
                                    {
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory_detail._table + " set " + _g.d.ic_inventory_detail._have_point + "=" + this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._have_point) + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + __itemCode + "\'"));
                                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory_detail._table + " set " + _g.d.ic_inventory_detail._is_premium + "=" + this._icMainPanel._icmainScreenMoreControl._getDataStrQuery(_g.d.ic_inventory_detail._is_premium) + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + __itemCode + "\'"));

                                    }

                                }

                                __myQuery.Append("</node>");

                                string __debugQuery = __myQuery.ToString();

                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                                if (__result.Length == 0)
                                {
                                    // save log
                                    this._createLog(this._myManageMain._mode);

                                    // Update ชื่อหน่วยนับ
                                    /* _g._utils __utils = new _g._utils();
                                    __utils._updateInventoryMaster(__itemCode);
                                    Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
                                    __thread.Start(); */
                                    // แก้เพื่อให้ Update ก่อน ค่อย filter
                                    _g._utils __utils = new _g._utils();
                                    __utils._updateInventoryMaster(__itemCode);
                                    __utils._updateInventoryMasterFunction();
                                    //
                                    SMLProcess._docFlow __docFlow = new SMLProcess._docFlow();
                                    __docFlow._processAll(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, "\'" + __itemCode + "\'", "");
                                    MyLib._myGlobal._displayWarning(1, null);
                                    this._icMainPanel._icmainScreenTop._isChange = false;
                                    if (this._myManageMain._mode == 1)
                                    {
                                        this._myManageMain._afterInsertData();
                                        /*this._icmainScreenTop._clear();
                                        this._icmainGridUnit._clear();
                                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLERP || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount)
                                        {
                                            // หน่วยนับขนาน
                                            this._icmainGridUnitOpposi._clear();
                                        }
                                        this._icmainGridBranch._clear();
                                        this._icmainScreenAccount._clear();
                                        this._icmainScreenTop._focusFirst();
                                         * */
                                        if (clearData)
                                        {
                                            this._icMainPanel._clear();
                                            this._icMainPanel._loadWarehouseAndLocationAuto();
                                        }
                                    }
                                    else
                                    {
                                        this._myManageMain._afterUpdateData();
                                    }
                                    //
                                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                                    // _g.g._updateDateTimeForCalc();
                                    string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, "\'" + __itemCode + "\'", "*");
                                }
                                else
                                {
                                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception __e)
                    {
                        MessageBox.Show(__e.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถบันทึกข้อมูลได้"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
