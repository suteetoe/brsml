using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icMainColorSet : UserControl
    {
        private string _oldCode = "";
        private MyLib._myGrid __gridTemp;

        public _icMainColorSet()
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
            this._myManageDetail._dataList._extraWhere = _g.d.ic_inventory._item_type + "=5"; // 5=สูตรสี
            this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            //this._myManageDetail._dataList._loadViewData(0);
            this._myManageDetail._calcArea();
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._autoSizeHeight = 450;
            this._myManageDetail.Invalidate();
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            //
            this.__gridTemp = new MyLib._myGrid();
            this.__gridTemp._width_by_persent = true;
            this.__gridTemp._table_name = _g.d.ic_inventory_set_detail._table;
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._ic_code, 1, 1, 15, true, false, true, true);
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._ic_name, 1, 1, 30, false, false, false);
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._barcode, 1, 1, 20, true, false, true);
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._unit_code, 1, 1, 10, false, false, true);
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._unit_name, 1, 1, 20, false, false, false);
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._qty, 3, 1, 10, true, false, true, false, _g.g._getFormatNumberStr(14));
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._price, 3, 1, 10, true, false, true, false, _g.g._getFormatNumberStr(2));
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._sum_amount, 3, 1, 10, true, false, true, false, _g.g._getFormatNumberStr(3));
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._line_number, 2, 1, 10, true, false, true, false);
            this.__gridTemp._addColumn(_g.d.ic_inventory_set_detail._unit_type, 2, 1, 10, true, false, false, false);
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
            int __itemCodeColumnNumber = sender._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
            string __itemCode = sender._cellGet(row, __itemCodeColumnNumber).ToString().ToUpper();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select " + _g.d.ic_inventory._update_detail + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __itemCode + "\'").Tables[0];
            int __updateDetail = MyLib._myGlobal._intPhase(__getData.Rows[0][0].ToString());
            return (__updateDetail == 1) ? true : false;
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
                if ((keyData & Keys.Control) == Keys.Control && (__keyCode == Keys.Home))
                {
                    this._icmainScreenTop.Focus();
                    this._icmainScreenTop._focusFirst();
                    return true;
                }
                if (__keyCode == Keys.F12 && _myToolBar.Enabled)
                {
                    this._saveData();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        void _myManageDetail__clearData()
        {
            _clearScreen();
            this._myManageDetail._dataList._refreshData();
        }

        void _clearScreen()
        {
            this._icMainSetGrid._clear();
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__newDataClick()
        {
            _clearScreen();
            this._myManageDetail._dataList._refreshData();
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
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + _oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,"
                    + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code) + ") as " + _g.d.ic_inventory_set_detail._ic_name + ","
                    + "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._table + "." + _g.d.ic_unit._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._unit_code) + ") as " + _g.d.ic_inventory_set_detail._unit_name + ","
                    + "(select " + _g.d.ic_inventory._unit_type + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._table + "." + _g.d.ic_inventory_set_detail._ic_code) + ") as " + _g.d.ic_inventory_set_detail._unit_type
                    + " from " + _g.d.ic_inventory_set_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._ic_set_code) + "=\'" + _oldCode + "\' order by " + _g.d.ic_inventory_set_detail._line_number));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this.__gridTemp._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._icMainSetGrid._clear();
                for (int __row = 0; __row < this.__gridTemp._rowData.Count; __row++)
                {
                    int __addr = this._icMainSetGrid._addRow();
                    int __unitType = MyLib._myGlobal._intPhase(this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._unit_type).ToString());
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._ic_code).ToString(), false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._ic_name).ToString(), false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._barcode, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._barcode).ToString(), false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._unit_code).ToString(), false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_name, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._unit_name).ToString(), false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._unit_type, __unitType, false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._qty), false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._price, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._price), false);
                    this._icMainSetGrid._cellUpdate(__addr, _g.d.ic_trans_detail._sum_amount, this.__gridTemp._cellGet(__row, _g.d.ic_inventory_set_detail._sum_amount), false);
                    //this._icMainSetGrid._searchUnitNameWareHouseNameShelfName(__addr);
                }
                this._icmainScreenTop._search(true);
                this._icmainScreenTop.Invalidate();
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
            // (_g.g._companyProfile._use_barcode)
            try
            {
                string __getEmtry = this._icmainScreenTop._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    // get iitem color
                    bool __pass = true;
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        if (_g.g._companyProfile._add_item_color == false)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่อนุญาติให้เพิ่มสูตรสี"));
                            __pass = false;
                        }
                    }

                    if (__pass)
                    {
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        // ลบของเก่า
                        string __where = MyLib._myGlobal._addUpper(_g.d.ic_inventory_set_detail._ic_set_code) + "=\'" + this._oldCode.ToUpper() + "\'";
                        __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + __where), _g.d.ic_inventory_set_detail._table));
                        this.__gridTemp._clear();
                        int __lineNumber = 0;
                        for (int __row = 0; __row < this._icMainSetGrid._rowData.Count; __row++)
                        {
                            string __getItemCode = this._icMainSetGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString().Trim().ToUpper();
                            if (__getItemCode.Length > 0)
                            {
                                int __addr = this.__gridTemp._addRow();
                                this.__gridTemp._cellUpdate(__addr, _g.d.ic_inventory_set_detail._line_number, ++__lineNumber, true);
                                this.__gridTemp._cellUpdate(__addr, _g.d.ic_inventory_set_detail._ic_code, __getItemCode, true);
                                this.__gridTemp._cellUpdate(__addr, _g.d.ic_inventory_set_detail._barcode, this._icMainSetGrid._cellGet(__row, _g.d.ic_trans_detail._barcode).ToString(), true);
                                this.__gridTemp._cellUpdate(__addr, _g.d.ic_inventory_set_detail._unit_code, this._icMainSetGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString(), true);
                                this.__gridTemp._cellUpdate(__addr, _g.d.ic_inventory_set_detail._qty, this._icMainSetGrid._cellGet(__row, _g.d.ic_trans_detail._qty), true);
                                // สูตรสี
                                this.__gridTemp._cellUpdate(__addr, _g.d.ic_inventory_set_detail._price, (decimal)this._icMainSetGrid._cellGet(__row, _g.d.ic_trans_detail._price), true);
                                this.__gridTemp._cellUpdate(__addr, _g.d.ic_inventory_set_detail._sum_amount, this._icMainSetGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount), true);
                            }
                        }
                        string __fieldList = _g.d.ic_inventory_set_detail._ic_set_code + ",";
                        string __dataList = "\'" + this._oldCode + "\',";
                        __myQuery.Append(this.__gridTemp._createQueryForInsert(_g.d.ic_inventory_set_detail._table, __fieldList, __dataList));
                        __myQuery.Append("</node>");

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            MyLib._myGlobal._displayWarning(1, null);
                            this._myManageDetail._afterInsertData();
                            _clearScreen();
                            this._icmainScreenTop._focusFirst();
                            this._myManageDetail._afterUpdateData();
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
                MessageBox.Show(__e.Message + " " + __e.StackTrace.ToString());
            }
        }
    }
}
