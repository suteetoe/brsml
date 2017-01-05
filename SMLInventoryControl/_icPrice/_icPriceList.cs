using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._icPrice
{
    public partial class _icPriceList : UserControl
    {
        string _oldCode = "";
        private int _mode;

        /// <summary>
        /// 0=ราคามาตรฐาน,1=ราคาขายทั่วไป
        /// </summary>
        /// <param name="mode"></param>
        public _icPriceList(int mode)
        {
            this._mode = mode;
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
            {
                if (mode == 0)
                {
                    this._myTabControlDetail.TabPages[1].Dispose();
                    this._myTabControlDetail.TabPages[1].Dispose();
                }
                else
                {
                    this._myManageDetail._isLockRecordFromDatabaseActive = false;
                    this._myTabControlDetail.TabPages[2].Dispose();
                }
            }
            this._myTabControlDetail.TableName = _g.d.ic_inventory_price._table;
            this._myTabControlDetail._getResource();
            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            this._myManageDetail._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._myManageDetail._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageDetail._manageButton = this._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageDetail._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageDetail._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageDetail._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            this._myManageDetail._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            //this._myManageDetail._dataList._loadViewData(0);
            this._myManageDetail._calcArea();
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._autoSizeHeight = 450;
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._myManageDetail.Invalidate();
            //
            this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
            if (this._mode == 0 && MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
            {
                return false;
            }
            int __itemCodeColumnNumber = sender._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
            string __itemCode = sender._cellGet(row, __itemCodeColumnNumber).ToString();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select " + _g.d.ic_inventory._update_price + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'").Tables[0];
            int __updatePrice = MyLib._myGlobal._intPhase(__getData.Rows[0][0].ToString());
            return (__updatePrice == 1) ? true : false;
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            int __itemTypeColumnNumber = sender._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._item_type);
            if (__itemTypeColumnNumber != -1)
            {
                if (MyLib._myGlobal._intPhase(sender._cellGet(row, __itemTypeColumnNumber).ToString()) == 5)
                {
                    senderRow.newColor = Color.Blue;
                }
            }
            return senderRow;
        }

        void _myToolBar_EnabledChanged(object sender, EventArgs e)
        {
            this._normalPrice.Enabled = this._myToolBar.Enabled;
            this._custGroupPrice.Enabled = this._myToolBar.Enabled;
            this._custPrice.Enabled = this._myToolBar.Enabled;
            //
            this._normalPrice.Invalidate();
            this._custGroupPrice.Invalidate();
            this._custPrice.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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
            if (keyData == Keys.F12 && _myToolBar.Enabled)
            {
                _saveData();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string _dataList__columnFieldNameReplace(string source)
        {
            return source.Replace("sml_value_branch_code", MyLib._myGlobal._branchCode);
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageDetail__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._oldCode = __rowDataArray[1].ToString();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + _oldCode + "\'"));
                string __getPriceQuery = "select *,"
                    + " (select  " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._unit_code + ") as " + _g.d.ic_inventory_price._unit_name + ","
                    + " (select  " + _g.d.ar_group._name_1 + " from " + _g.d.ar_group._table + " where " + _g.d.ar_group._table + "." + _g.d.ar_group._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_1 + ") as " + _g.d.ic_inventory_price._group_name_1 + ","
                    + " (select  " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_2
                    + " and " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._main_group + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_1 + ") as " + _g.d.ic_inventory_price._group_name_2 + ","
                    + " (select  " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_code + ") as " + _g.d.ic_inventory_price._cust_name
                    + " from " + _g.d.ic_inventory_price._table
                    + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + _oldCode + "\' and " + _g.d.ic_inventory_price._price_mode + "=" + this._mode.ToString() + " and " + _g.d.ic_inventory_price._price_type;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + "=1 order by " + _g.d.ic_inventory_price._unit_code + ",roworder")); //somruk
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + "=2"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + "=3"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                //
                this._normalPrice._grid._clear();
                this._custPrice._grid._clear();
                this._custGroupPrice._grid._clear();
                //
                this._normalPrice._removeEvent();
                this._custGroupPrice._removeEvent();
                this._custPrice._removeEvent();
                //
                this._normalPrice._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._custGroupPrice._grid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                this._custPrice._grid._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
                //
                this._normalPrice._addEvent();
                this._custGroupPrice._addEvent();
                this._custPrice._addEvent();
                //
                this._icmainScreenTop.Invalidate();
                __result = true;
                this._normalPrice._itemCode = this._custPrice._itemCode = this._icmainScreenTop._getDataStr(_g.d.ic_inventory._code, false);
                this._custGroupPrice._itemCode = this._custPrice._itemCode = this._icmainScreenTop._getDataStr(_g.d.ic_inventory._code, false);
                this._custPrice._itemCode = this._custPrice._itemCode = this._icmainScreenTop._getDataStr(_g.d.ic_inventory._code, false);
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
            this._normalPrice._grid._clear();
            this._custPrice._grid._clear();
            this._custGroupPrice._grid._clear();
        }

        void _clearScreen()
        {
            this._icmainScreenTop._clear();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _saveData()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                try
                {
                    string __getEmtry = this._icmainScreenTop._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                        string __itemCode = this._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code).ToString();
                        string __fieldList = _g.d.ic_inventory_price._ic_code + " , " + _g.d.ic_inventory_price._price_type + "," + _g.d.ic_inventory_price._price_mode + ",";
                        string __dataList = this._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                        // Delete
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._ic_code + "=" + __itemCode + " and " + _g.d.ic_inventory_price._price_mode + "=" + this._mode.ToString()));
                        this._normalPrice._grid._updateRowIsChangeAll(true);
                        this._custGroupPrice._grid._updateRowIsChangeAll(true);
                        this._custPrice._grid._updateRowIsChangeAll(true);
                        __myQuery.Append(this._normalPrice._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "1," + this._mode.ToString() + ",", false, true));
                        __myQuery.Append(this._custGroupPrice._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "2," + this._mode.ToString() + ",", false, true));
                        __myQuery.Append(this._custPrice._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "3," + this._mode.ToString() + ",", false, true));
                        __myQuery.Append("</node>");

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
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
}
