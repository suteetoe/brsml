using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icBarcode : UserControl
    {
        private string _oldCode = "";

        public _icBarcode()
        {
            _utility __utilty = new _utility();
            __utilty._barcodeDuplicate();
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._icmainScreenBarCode.Enabled = false;
            //_myManageDataBarCode
            this._myManageDataBarCode._displayMode = 0;
            this._myManageDataBarCode._dataList._lockRecord = true;
            this._myManageDataBarCode._selectDisplayMode(this._myManageDataBarCode._displayMode);
            this._myManageDataBarCode._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._myManageDataBarCode._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDataBarCode._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDataBarCode__loadDataToScreen);
            this._myManageDataBarCode._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageDataBarCode._manageButton = this._myToolBar;
            this._myManageDataBarCode._manageBackgroundPanel = this._myPanel1;
            this._myManageDataBarCode._newDataClick += new MyLib.NewDataEvent(_myManageDataBarCode__newDataClick);
            this._myManageDataBarCode._discardData += new MyLib.DiscardDataEvent(_myManageDataBarCode__discardData);
            this._myManageDataBarCode._clearData += new MyLib.ClearDataEvent(_myManageDataBarCode__clearData);
            this._myManageDataBarCode._closeScreen += new MyLib.CloseScreenEvent(_myManageDataBarCode__closeScreen);
            this._myManageDataBarCode._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            this._myManageDataBarCode._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            this._myManageDataBarCode._checkEditData += new MyLib.CheckEditDataEvent(_myManageDataBarCode__checkEditData);
            this._myManageDataBarCode._calcArea();
            this._myManageDataBarCode._dataListOpen = true;
            this._myManageDataBarCode._autoSize = true;
            this._myManageDataBarCode._autoSizeHeight = 450;
            this._myManageDataBarCode.Invalidate();
            //
            this._icmainGridBarCode.GetItemCode += new SMLInventoryControl.GetItemCodeEventHandler(_icmainGridBarCode_GetItemCode);
            this._icmainGridBarCode.GetItemDesc += new SMLInventoryControl.GetItemDescEventHandler(_icmainGridBarCode_GetItemDesc);
            this._icmainGridBarCode.GetUnitType += new SMLInventoryControl.GetUnitTypeEventHandler(_icmainGridBarCode_GetUnitType);
            this._icmainGridBarCode.GetUnitCode += new SMLInventoryControl.GetUnitCodeEventHandler(_icmainGridBarCode_GetUnitCode);
            this._icmainGridBarCode._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridBarCode__queryForInsertCheck);
            this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
            // this._saveButton.Click += new EventHandler(_saveButton_Click);
            this.Disposed += new EventHandler(_icBarcode_Disposed);
        }

        void _icBarcode_Disposed(object sender, EventArgs e)
        {
            _g._utils __utils = new _g._utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
            __thread.Start();
        }

        bool _myManageDataBarCode__checkEditData(int row, MyLib._myGrid sender)
        {
            int __itemCodeColumnNumber = sender._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
            string __itemCode = sender._cellGet(row, __itemCodeColumnNumber).ToString().ToUpper();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select " + _g.d.ic_inventory._update_detail + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __itemCode + "\'").Tables[0];
            int __updateDetail = MyLib._myGlobal._intPhase(__getData.Rows[0][0].ToString());
            return (__updateDetail == 1) ? true : false;
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
                                    this._myManageDataBarCode._dataList._searchText.textBox.Text = __itemCode;
                                    this._myManageDataBarCode._dataList._refreshData();
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

        void _myToolBar_EnabledChanged(object sender, EventArgs e)
        {
            this._icmainGridBarCode.Enabled = ((ToolStrip)sender).Enabled;
        }

        string _icmainGridBarCode_GetUnitCode(object sender)
        {
            return this._icmainScreenBarCode._getDataStr(_g.d.ic_inventory._unit_standard);
        }

        bool _icmainGridBarCode__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((this._icmainGridBarCode._cellGet(row, _g.d.ic_inventory_barcode._barcode).ToString().Trim().Length == 0) ? false : true);
        }

        int _icmainGridBarCode_GetUnitType(object sender)
        {
            return MyLib._myGlobal._intPhase(this._icmainScreenBarCode._getDataStr(_g.d.ic_inventory._unit_type));
        }

        string _icmainGridBarCode_GetItemDesc(object sender)
        {
            return this._icmainScreenBarCode._getDataStr(_g.d.ic_inventory._name_1);
        }

        string _icmainGridBarCode_GetItemCode(object sender)
        {
            return this._icmainScreenBarCode._getDataStr(_g.d.ic_inventory._code);
        }

        string _dataList__columnFieldNameReplace(string source)
        {
            return source.Replace("sml_value_branch_code", MyLib._myGlobal._branchCode);
        }

        bool _myManageDataBarCode__loadDataToScreen(object rowData, string whereString, bool forEdit)
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
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenBarCode._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainGridBarCode._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._icmainScreenBarCode._search(true);
                this._icmainScreenBarCode.Invalidate();
                this._icmainGridBarCode.Invalidate();
                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
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
                    int _getColumnCode = this._myManageDataBarCode._dataList._gridData._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                    string __getItemCode = this._myManageDataBarCode._dataList._gridData._cellGet(getData.row, _getColumnCode).ToString().ToUpper();
                    string __myFormat = "";
                    __myFormat += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " = \'" + __getItemCode + "\'");
                    __myQuery.Append(string.Format(__myFormat, _g.d.ic_inventory._table));
                }
                __myQuery.Append("</node>");

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(0, null);
                    this._myManageDataBarCode._dataList._refreshData();
                    this._icmainScreenBarCode._clear();
                    this._icmainGridBarCode._clear();
                    this._icmainScreenBarCode._focusFirst();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _myManageDataBarCode__newDataClick()
        {
            this._icmainScreenBarCode._clear();
            this._icmainGridBarCode._clear();
            this._myManageDataBarCode._dataList._refreshData();
        }

        bool _myManageDataBarCode__discardData()
        {
            return true;
        }

        void _myManageDataBarCode__clearData()
        {
            this._icmainScreenBarCode._clear();
            this._icmainGridBarCode._clear();
        }

        void _myManageDataBarCode__closeScreen()
        {
            this.Dispose();
            _utility __utilty = new _utility();
            __utilty._barcodeDuplicate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            if (_myManageDataBarCode._dataList._loadViewDataSuccess == false)
            {
                this._myManageDataBarCode._dataList._loadViewData(0);
            }
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonDelete_Click(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                StringBuilder __myQuery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                string __getItemCode = this._icmainScreenBarCode._getDataStr(_g.d.ic_inventory._code).ToUpper();
                string __myFormat = "";
                __myFormat += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " = \'" + __getItemCode + "\'");
                __myQuery.Append(string.Format(__myFormat, _g.d.ic_inventory._table));

                __myQuery.Append("</node>");

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(0, null);
                    this._myManageDataBarCode._dataList._refreshData();
                    this._icmainScreenBarCode._clear();
                    this._icmainGridBarCode._clear();
                    this._icmainScreenBarCode._focusFirst();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _saveData()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                try
                {
                    string __getEmtry = this._icmainScreenBarCode._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        if (_myManageDataBarCode._mode == 1)  // insert
                        {

                        }
                        else // update
                        {
                            string __itemCode = this._icmainScreenBarCode._getDataStrQuery(_g.d.ic_inventory._code).ToString().ToUpper();
                            string __fieldList = _g.d.ic_inventory_detail._ic_code + " , ";
                            string __dataList = this._icmainScreenBarCode._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                            this._icmainGridBarCode._updateRowIsChangeAll(true);
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " = " + __itemCode));
                            __myQuery.Append(this._icmainGridBarCode._createQueryForInsert(_g.d.ic_inventory_barcode._table, __fieldList, __dataList));
                        }
                        __myQuery.Append("</node>");
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            // Update ชื่อหน่วยนับ
                            _g._utils __utils = new _g._utils();
                            __utils._updateInventoryMaster(this._icmainScreenBarCode._getDataStr(_g.d.ic_inventory._code).ToString().ToUpper());
                            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
                            __thread.Start();
                            //
                            MyLib._myGlobal._displayWarning(1, null);
                            if (this._myManageDataBarCode._mode == 1)
                            {
                                this._myManageDataBarCode._afterInsertData();
                                this._icmainScreenBarCode._focusFirst();
                            }
                            else
                            {
                                this._myManageDataBarCode._afterUpdateData();
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
