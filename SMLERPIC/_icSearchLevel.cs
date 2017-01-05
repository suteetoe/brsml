using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icSearchLevel : UserControl
    {
        private _icSearchLevelScreen _screen = new _icSearchLevelScreen();
        private string _oldCode = "";

        public _icSearchLevel()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._screen._toolStrip.EnabledChanged += new EventHandler(_toolStrip_EnabledChanged);
            //
            this._screen.Dock = DockStyle.Fill;
            this._screen.Enabled = false;
            this._myManageData1._form2.Controls.Add(this._screen);
            //
            this._myManageData1._displayMode = 0;
            this._myManageData1._dataList._lockRecord = true;
            if (MyLib._myGlobal._OEMVersion == "SINGHA")
            {
                this._myManageData1._isLockRecordFromDatabaseActive = false;
            }
            this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            this._myManageData1._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageData1._manageButton = this._screen._toolStrip;
            this._myManageData1._manageBackgroundPanel = this._screen._panel;
            this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._myManageData1._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            this._myManageData1._checkEditData += new MyLib.CheckEditDataEvent(_myManageData1__checkEditData);
            this._myManageData1._calcArea();
            this._myManageData1._dataListOpen = true;
            this._myManageData1._autoSize = true;
            this._myManageData1._autoSizeHeight = 450;
            this._myManageData1.Invalidate();
            //
            this._screen._saveButton.Click += (s1, e1) =>
            {
                this._saveData();
            };
            this._screen._closeButton.Click += (s1, e1) =>
            {
                this.Dispose();
            };
            this._screen._loadItem.Click += (s1, e1) =>
            {
                this._loadItem();
            };
            this._screen._loadItemCondition.Click += (s1, e1) =>
            {
                this._loadItem();
            };
            this._screen._grid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_grid__queryForInsertCheck);
            this._screen._conditionGrid._queryForInsertCheck += _conditionGrid__queryForInsertCheck;
        }

        bool _conditionGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((this._screen._conditionGrid._cellGet(row, _g.d.ic_inventory_barcode_condition._unit_code).ToString().Trim().Length == 0) ? false : true);
        }

        void _loadItem()
        {
            // pack where
            string __fieldBarcode = "barcode";
            string __barcodeQuery = "select " + _g.d.ic_inventory_barcode._ic_code + " as " + _g.d.ic_inventory_level._ic_code + ", " + _g.d.ic_inventory_barcode._barcode + " as " + __fieldBarcode + ", " + _g.d.ic_inventory_barcode._unit_code + " as " + _g.d.ic_inventory_level._unit_code + " from " + _g.d.ic_inventory_barcode._table + " where  " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " =\'" + this._oldCode + "\' ";
            string __unitQurey = " union all (select " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "  as " + _g.d.ic_inventory_level._ic_code + ", \'\' as " + __fieldBarcode + " , " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + " as " + _g.d.ic_inventory_level._unit_code + " from " + _g.d.ic_unit_use._table + " where upper(" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + ")  = \'" + this._oldCode + "\' and not exists (select " + _g.d.ic_inventory_barcode._ic_code + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " = " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " = " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + " ))";

            StringBuilder __myquery2 = new StringBuilder();
            __myquery2.Append(MyLib._myGlobal._xmlHeader + "<node>");
            string __query = __barcodeQuery + __unitQurey;

            __myquery2.Append(MyLib._myUtil._convertTextToXmlForQuery(__barcodeQuery + __unitQurey));
            __myquery2.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __getData2 = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());

            if (__getData2.Tables.Count > 0 && __getData2.Tables[0].Rows.Count > 0)
            {
                DataTable __getTable2 = __getData2.Tables[0];
                for (int __loop = 0; __loop < __getTable2.Rows.Count; __loop++)
                {
                    {
                        // ลำดับการค้นหา
                        bool __found = false;
                        for (int __i = 0; __i < this._screen._grid._rowData.Count; __i++)
                        {
                            string __unit_code = this._screen._grid._cellGet(__i, _g.d.ic_inventory_level._unit_code).ToString();
                            string __barcode = this._screen._grid._cellGet(__i, _g.d.ic_inventory_level._barcode).ToString();

                            if (__unit_code.Equals(__getTable2.Rows[__loop][_g.d.ic_inventory_level._unit_code].ToString()) && __barcode.Equals(__getTable2.Rows[__loop][_g.d.ic_inventory_level._barcode].ToString()))
                            {
                                __found = true;
                            }
                        }

                        if (__found == false)
                        {
                            int __addr = this._screen._grid._addRow();
                            this._screen._grid._cellUpdate(__addr, _g.d.ic_inventory_level._unit_code, __getTable2.Rows[__loop][_g.d.ic_inventory_level._unit_code].ToString(), false);
                            this._screen._grid._cellUpdate(__addr, _g.d.ic_inventory_level._barcode, __getTable2.Rows[__loop][__fieldBarcode].ToString(), false);
                        }
                    }
                    {
                        // เงื่อนไขพิเศษ
                        bool __found = false;
                        for (int __i = 0; __i < this._screen._conditionGrid._rowData.Count; __i++)
                        {
                            string __unit_code = this._screen._conditionGrid._cellGet(__i, _g.d.ic_inventory_level._unit_code).ToString();
                            string __barcode = this._screen._conditionGrid._cellGet(__i, _g.d.ic_inventory_level._barcode).ToString();

                            if (__unit_code.Equals(__getTable2.Rows[__loop][_g.d.ic_inventory_level._unit_code].ToString()) && __barcode.Equals(__getTable2.Rows[__loop][_g.d.ic_inventory_level._barcode].ToString()))
                            {
                                __found = true;
                            }
                        }

                        if (__found == false)
                        {
                            int __addr = this._screen._conditionGrid._addRow();
                            this._screen._conditionGrid._cellUpdate(__addr, _g.d.ic_inventory_barcode_condition._unit_code, __getTable2.Rows[__loop][_g.d.ic_inventory_level._unit_code].ToString(), false);
                            this._screen._conditionGrid._cellUpdate(__addr, _g.d.ic_inventory_barcode_condition._barcode, __getTable2.Rows[__loop][__fieldBarcode].ToString(), false);
                        }
                    }
                }
            }
        }

        bool _grid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((this._screen._grid._cellGet(row, _g.d.ic_inventory_level._unit_code).ToString().Trim().Length == 0) ? false : true);
        }

        void _toolStrip_EnabledChanged(object sender, EventArgs e)
        {
            this._screen.Enabled = true;
            this._screen._grid.Enabled = ((ToolStrip)sender).Enabled;
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
                                    this._myManageData1._dataList._searchText.textBox.Text = __itemCode;
                                    this._myManageData1._dataList._refreshData();
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

        bool _myManageData1__checkEditData(int row, MyLib._myGrid sender)
        {
            return true;
        }


        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _clear()
        {
            this._screen._icmainScreen._clear();
            this._screen._grid._clear();
            this._screen._conditionGrid._clear();
        }

        void _myManageData1__clearData()
        {
            this._clear();
        }

        bool _myManageData1__discardData()
        {
            return true;
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                StringBuilder __myQuery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int loop = 0; loop < selectRowOrder.Count; loop++)
                {
                    MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[loop];
                    int _getColumnCode = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                    string __getItemCode = this._myManageData1._dataList._gridData._cellGet(getData.row, _getColumnCode).ToString().ToUpper();
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_level._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_level._ic_code) + " = \'" + __getItemCode + "\'"));
                }
                __myQuery.Append("</node>");

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(0, null);
                    this._clear();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._oldCode = __rowDataArray[1].ToString().ToUpper();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_level._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_level._ic_code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode_condition._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode_condition._ic_code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screen._icmainScreen._loadData(((DataSet)__getData[0]).Tables[0]);
                this._screen._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._screen._conditionGrid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                //
                if (this._screen._grid._rowData.Count == 0)
                {
                    string __fieldBarcode = "barcode";
                    StringBuilder __myquery2 = new StringBuilder();
                    __myquery2.Append(MyLib._myGlobal._xmlHeader + "<node>");

                    // toe แก้
                    //__myquery2.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._code + ",coalesce((select " + _g.d.ic_inventory_barcode._barcode + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + "=" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + "=" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "),\'\') as " + __fieldBarcode + " from " + _g.d.ic_unit_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit_use._ic_code) + "=\'" + this._oldCode + "\'"));

                    string __barcodeQuery = "select " + _g.d.ic_inventory_barcode._ic_code + " as " + _g.d.ic_inventory_level._ic_code + ", " + _g.d.ic_inventory_barcode._barcode + " as " + __fieldBarcode + ", " + _g.d.ic_inventory_barcode._unit_code + " as " + _g.d.ic_inventory_level._unit_code + " from " + _g.d.ic_inventory_barcode._table + " where  " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " =\'" + this._oldCode + "\' ";
                    string __unitQurey = " union all (select " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "  as " + _g.d.ic_inventory_level._ic_code + ",(select barcode from ic_inventory_barcode where ic_inventory_barcode.ic_code=ic_unit_use.ic_code and ic_inventory_barcode.unit_code=ic_unit_use.code) as " + __fieldBarcode + " , " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + " as " + _g.d.ic_inventory_level._unit_code + " from " + _g.d.ic_unit_use._table + " where upper(" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + ")  = \'" + this._oldCode + "\' and not exists (select " + _g.d.ic_inventory_barcode._ic_code + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " = " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + " and " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " = " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + " ))";

                    __myquery2.Append(MyLib._myUtil._convertTextToXmlForQuery(__barcodeQuery + __unitQurey));

                    __myquery2.Append("</node>");
                    ArrayList __getData2 = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery2.ToString());
                    if (__getData2.Count > 0)
                    {
                        DataTable __getTable2 = ((DataSet)__getData2[0]).Tables[0];
                        for (int __loop = 0; __loop < __getTable2.Rows.Count; __loop++)
                        {
                            int __addr = this._screen._grid._addRow();
                            this._screen._grid._cellUpdate(__addr, _g.d.ic_inventory_level._unit_code, __getTable2.Rows[__loop][_g.d.ic_inventory_level._unit_code].ToString(), false);
                            this._screen._grid._cellUpdate(__addr, _g.d.ic_inventory_level._barcode, __getTable2.Rows[__loop][__fieldBarcode].ToString(), false);
                        }
                    }
                }
                //
                if (this._screen._conditionGrid._rowData.Count == 0)
                {
                    string __fieldBarcode = "barcode";
                    StringBuilder __myquery2 = new StringBuilder();
                    __myquery2.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    string __barcodeQuery = "select " + _g.d.ic_inventory_barcode_condition._ic_code + " as " + _g.d.ic_inventory_level._ic_code + ", " + _g.d.ic_inventory_barcode_condition._barcode + " as " + __fieldBarcode + ", " + _g.d.ic_inventory_barcode_condition._unit_code + " as " + _g.d.ic_inventory_level._unit_code + " from " + _g.d.ic_inventory_barcode_condition._table + " where  " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode_condition._ic_code) + " =\'" + this._oldCode + "\' ";
                    string __unitQurey = " union all (select " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "  as " + _g.d.ic_inventory_level._ic_code + ", (select barcode from ic_inventory_barcode where ic_inventory_barcode.ic_code=ic_unit_use.ic_code and ic_inventory_barcode.unit_code=ic_unit_use.code) as " + __fieldBarcode + " , " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + " as " + _g.d.ic_inventory_level._unit_code + " from " + _g.d.ic_unit_use._table + " where upper(" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + ")  = \'" + this._oldCode + "\' and not exists (select " + _g.d.ic_inventory_barcode_condition._ic_code + " from " + _g.d.ic_inventory_barcode_condition._table + " where " + _g.d.ic_inventory_barcode_condition._table + "." + _g.d.ic_inventory_barcode_condition._ic_code + " = " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + " and " + _g.d.ic_inventory_barcode_condition._table + "." + _g.d.ic_inventory_barcode_condition._unit_code + " = " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + " ))";

                    __myquery2.Append(MyLib._myUtil._convertTextToXmlForQuery(__barcodeQuery + __unitQurey));
                    __myquery2.Append("</node>");
                    ArrayList __getData2 = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery2.ToString());
                    if (__getData2.Count > 0)
                    {
                        DataTable __getTable2 = ((DataSet)__getData2[0]).Tables[0];
                        for (int __loop = 0; __loop < __getTable2.Rows.Count; __loop++)
                        {
                            int __addr = this._screen._conditionGrid._addRow();
                            this._screen._conditionGrid._cellUpdate(__addr, _g.d.ic_inventory_barcode_condition._unit_code, __getTable2.Rows[__loop][_g.d.ic_inventory_barcode_condition._unit_code].ToString(), false);
                            this._screen._conditionGrid._cellUpdate(__addr, _g.d.ic_inventory_barcode_condition._barcode, __getTable2.Rows[__loop][__fieldBarcode].ToString(), false);
                        }
                    }
                }
                //
                this._screen._icmainScreen._search(true);
                this._screen._icmainScreen.Invalidate();
                this._screen._grid.Invalidate();
                this._screen._conditionGrid.Invalidate();
                this._screen.toolStrip1.Enabled = this._screen._toolStrip.Enabled;
                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        private void _saveData()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                try
                {
                    string __getEmtry = this._screen._icmainScreen._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        if (_myManageData1._mode == 1)  // insert
                        {

                        }
                        else // update
                        {
                            string __itemCode = this._screen._icmainScreen._getDataStrQuery(_g.d.ic_inventory._code).ToString().ToUpper();
                            string __fieldList = _g.d.ic_inventory_detail._ic_code + " , ";
                            string __dataList = this._screen._icmainScreen._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_inventory_level._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_level._ic_code) + "=" + __itemCode));
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_inventory_barcode_condition._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode_condition._ic_code) + "=" + __itemCode));
                            //
                            this._screen._grid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._screen._grid._createQueryForInsert(_g.d.ic_inventory_level._table, __fieldList, __dataList));
                            //
                            this._screen._conditionGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._screen._conditionGrid._createQueryForInsert(_g.d.ic_inventory_barcode_condition._table, __fieldList, __dataList));
                        }
                        __myQuery.Append("</node>");
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            //
                            MyLib._myGlobal._displayWarning(1, null);
                            if (this._myManageData1._mode == 1)
                            {
                                this._myManageData1._afterInsertData();
                            }
                            else
                            {
                                this._myManageData1._afterUpdateData();
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

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _testButton_Click(object sender, EventArgs e)
        {
            Form __testForm = new Form();
            __testForm.WindowState = FormWindowState.Maximized;
            SMLInventoryControl._itemSearchLevelControl __control = new SMLInventoryControl._itemSearchLevelControl();
            __control._productBasket = true;
            __control.Dock = DockStyle.Fill;
            __testForm.Controls.Add(__control);
            __testForm.ShowDialog();
        }
    }
}
