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
    public partial class _icPriceFormula : UserControl
    {
        string _oldCode = "";
        SMLERPControl._ic._icPriceFormulaDetail _pricePanel = new SMLERPControl._ic._icPriceFormulaDetail();

        public _icPriceFormula()
        {
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

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
            this._pricePanel.Dock = DockStyle.Fill;
            this._pricePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myPanel1.Controls.Add(this._pricePanel);
            this._pricePanel.BringToFront();
            this._pricePanel._createGrid();
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
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
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + _oldCode + "\' order by " + _g.d.ic_inventory_price_formula._unit_code + "," + _g.d.ic_inventory_price_formula._sale_type));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._pricePanel._grid._clear();
                this._pricePanel._gridResult._clear();
                this._pricePanel._itemCode = this._icmainScreenTop._getDataStr(_g.d.ic_inventory._code, false);
                this._pricePanel._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._pricePanel._calc();
                //
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
                        string __fieldList = _g.d.ic_inventory_price_formula._ic_code + ",";
                        string __dataList = this._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code) + ",";
                        // Delete
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=" + __itemCode));
                        this._pricePanel._grid._updateRowIsChangeAll(true);
                        __myQuery.Append(this._pricePanel._grid._createQueryForInsert(_g.d.ic_inventory_price_formula._table, __fieldList, __dataList, false));
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
