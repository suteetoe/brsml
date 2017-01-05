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
    public partial class _icPurchasePermium : UserControl
    {
        private string _oldCode = "";

        public _icPurchasePermium()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myManageMain._displayMode = 0;
            this._myManageMain._dataList._lockRecord = true;
            this._myManageMain._selectDisplayMode(this._myManageMain._displayMode);
            this._myManageMain._dataList._loadViewFormat(_g.g._search_ic_purchase_permium, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageMain._dataList._referFieldAdd(_g.d.ic_purchase_permium._permium_code, 1);
            this._myManageMain._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageMain._manageButton = this._myToolBar;
            //this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageMain._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageMain._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageMain._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageMain._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageMain._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageMain._dataList._loadViewData(0);
            this._myManageMain._calcArea();
            this._myManageMain._dataListOpen = true;
            this._myManageMain._autoSize = true;
            this._myManageMain._autoSizeHeight = 450;
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._myManageMain.Invalidate();
            //
            this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int loop = 0; loop < selectRowOrder.Count; loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[loop];
                int _getColumnCode = _myManageMain._dataList._gridData._findColumnByName(_g.d.ic_purchase_permium._table + "." + _g.d.ic_purchase_permium._permium_code);
                string __getPermiumCode = this._myManageMain._dataList._gridData._cellGet(getData.row, _getColumnCode).ToString();
                __myQuery.Append(string.Format("<query>delete from " + _g.d.ic_purchase_permium._table + " {1}</query>", _g.d.ic_purchase_permium._table, getData.whereString));
                string __myFormat = "";
                __myFormat += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_purchase_permium_condition._table + " where " + _g.d.ic_purchase_permium_condition._permium_code + " = \'" + __getPermiumCode + "\'");
                __myFormat += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_purchase_permium_list._table + " where " + _g.d.ic_purchase_permium_list._permium_code + " = \'" + __getPermiumCode + "\'");
            }
            __myQuery.Append("</node>");

            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(0, null);
                this._myManageMain._dataList._refreshData();
                this._icPurchasePermiumScreen._clear();
                this._condition._clear();
                this._permiumList._clear();
                this._icPurchasePermiumScreen._focusFirst();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _myToolBar_EnabledChanged(object sender, EventArgs e)
        {
            this._condition.Enabled = this._myToolBar.Enabled;
            this._permiumList.Enabled = this._myToolBar.Enabled;
            //
            this._condition.Invalidate();
            this._permiumList.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12 && _myToolBar.Enabled)
            {
                _saveData(true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_purchase_permium._table + " where " + _g.d.ic_purchase_permium._permium_code + "=\'" + this._oldCode + "\'"));
                // เงื่อนไข
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,"
                    + " (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._ic_code + ") as " + _g.d.ic_purchase_permium_condition._ic_name + ","
                    + " (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_purchase_permium_condition._table + "." + _g.d.ic_purchase_permium_condition._unit_code + ") as " + _g.d.ic_purchase_permium_condition._unit_name
                    + " from " + _g.d.ic_purchase_permium_condition._table + " where " + _g.d.ic_purchase_permium_condition._permium_code + "=\'" + this._oldCode + "\'"));
                // ของแถม
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,"
                    + " (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_purchase_permium_list._table + "." + _g.d.ic_purchase_permium_list._ic_code + ") as " + _g.d.ic_purchase_permium_list._ic_name + ","
                    + " (select  " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_purchase_permium_list._table + "." + _g.d.ic_purchase_permium_list._unit_code + ") as " + _g.d.ic_purchase_permium_list._unit_name
                    + " from " + _g.d.ic_purchase_permium_list._table + " where " + _g.d.ic_purchase_permium_list._permium_code + "=\'" + this._oldCode + "\'"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icPurchasePermiumScreen._loadData(((DataSet)__getData[0]).Tables[0]);
                //
                this._condition._clear();
                this._permiumList._clear();
                //
                DataTable __conditionTable = ((DataSet)__getData[1]).Tables[0];
                for (int __row = 0; __row < __conditionTable.Rows.Count; __row++)
                {
                    string __itemCode = __conditionTable.Rows[__row][_g.d.ic_purchase_permium_condition._ic_code].ToString();
                    string __itemName = __conditionTable.Rows[__row][_g.d.ic_purchase_permium_condition._ic_name].ToString();
                    string __unitCode = __conditionTable.Rows[__row][_g.d.ic_purchase_permium_condition._unit_code].ToString();
                    string __unitName = __conditionTable.Rows[__row][_g.d.ic_purchase_permium_condition._unit_name].ToString();
                    decimal __qty = MyLib._myGlobal._decimalPhase(__conditionTable.Rows[__row][_g.d.ic_purchase_permium_condition._qty].ToString());
                    decimal __standValue = MyLib._myGlobal._decimalPhase(__conditionTable.Rows[__row][_g.d.ic_purchase_permium_condition._stand_value].ToString());
                    decimal __divideValue = MyLib._myGlobal._decimalPhase(__conditionTable.Rows[__row][_g.d.ic_purchase_permium_condition._divide_value].ToString());
                    int __addr = this._condition._addRow();
                    this._condition._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                    this._condition._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                    this._condition._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                    this._condition._cellUpdate(__addr, _g.d.ic_trans_detail._unit_name, __unitCode, false);
                    this._condition._cellUpdate(__addr, _g.d.ic_trans_detail._stand_value, __standValue, false);
                    this._condition._cellUpdate(__addr, _g.d.ic_trans_detail._divide_value, __divideValue, false);
                    this._condition._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                }
                this._condition.Invalidate();
                //
                DataTable __preminmListTable = ((DataSet)__getData[2]).Tables[0];
                for (int __row = 0; __row < __preminmListTable.Rows.Count; __row++)
                {
                    string __itemCode = __preminmListTable.Rows[__row][_g.d.ic_purchase_permium_condition._ic_code].ToString();
                    string __itemName = __preminmListTable.Rows[__row][_g.d.ic_purchase_permium_condition._ic_name].ToString();
                    string __unitCode = __preminmListTable.Rows[__row][_g.d.ic_purchase_permium_condition._unit_code].ToString();
                    string __unitName = __preminmListTable.Rows[__row][_g.d.ic_purchase_permium_condition._unit_name].ToString();
                    decimal __qty = MyLib._myGlobal._decimalPhase(__preminmListTable.Rows[__row][_g.d.ic_purchase_permium_condition._qty].ToString());
                    int __addr = this._permiumList._addRow();
                    this._permiumList._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, false);
                    this._permiumList._cellUpdate(__addr, _g.d.ic_trans_detail._item_name, __itemName, false);
                    this._permiumList._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                    this._permiumList._cellUpdate(__addr, _g.d.ic_trans_detail._unit_name, __unitCode, false);
                    this._permiumList._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __qty, false);
                }
                this._condition.Invalidate();
                //this._condition._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                //this._permiumList._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                //
                this._icPurchasePermiumScreen.Invalidate();
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
            this._myManageMain._dataList._refreshData();
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__clearData()
        {
            _clearScreen();
            this._condition._clear();
            this._permiumList._clear();
        }

        void _clearScreen()
        {
            this._icPurchasePermiumScreen._clear();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData(true);
        }

        private void _saveData(Boolean clearData)
        {
            try
            {
                string __getEmtry = this._icPurchasePermiumScreen._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                    string __code = this._icPurchasePermiumScreen._getDataStrQuery(_g.d.ic_purchase_permium._permium_code).ToString();
                    ArrayList __getDataScreenTop = this._icPurchasePermiumScreen._createQueryForDatabase();
                    if (this._myManageMain._mode == 1)
                    {
                        __myQuery.Append("<query>insert into " + _g.d.ic_purchase_permium._table + " (" + __getDataScreenTop[0].ToString() + ") values (" + __getDataScreenTop[1].ToString() + ")</query>");
                    }
                    else
                    {
                        __myQuery.Append("<query>update " + this._myManageMain._dataList._tableName + " set " + __getDataScreenTop[2].ToString() + this._myManageMain._dataList._whereString + "</query>");
                    }
                    // Delete
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_purchase_permium_condition._table + " where " + _g.d.ic_purchase_permium_condition._permium_code + "=" + __code));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_purchase_permium_list._table + " where " + _g.d.ic_purchase_permium_list._permium_code + "=" + __code));
                    // เงื่อนไข
                    for (int __row = 0; __row < this._condition._rowData.Count; __row++)
                    {
                        string __itemCode = this._condition._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                        string __unitCode = this._condition._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                        decimal __qty = MyLib._myGlobal._decimalPhase(this._condition._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                        decimal __standValue = MyLib._myGlobal._decimalPhase(this._condition._cellGet(__row, _g.d.ic_trans_detail._stand_value).ToString());
                        decimal __divideValue = MyLib._myGlobal._decimalPhase(this._condition._cellGet(__row, _g.d.ic_trans_detail._divide_value).ToString());
                        if (__itemCode.Length > 0)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(
                                "insert into " + _g.d.ic_purchase_permium_condition._table +
                                " (" + _g.d.ic_purchase_permium_condition._permium_code + "," + _g.d.ic_purchase_permium_condition._ic_code + "," + _g.d.ic_purchase_permium_condition._unit_code + "," + _g.d.ic_purchase_permium_condition._stand_value + "," + _g.d.ic_purchase_permium_condition._divide_value + "," + _g.d.ic_purchase_permium_condition._qty + ") values " +
                                " (" + __code + ",\'" + __itemCode + "\',\'" + __unitCode + "\'," + __standValue + "," + __divideValue + "," + __qty + ")"));
                        }
                    }
                    // ของแถม
                    for (int __row = 0; __row < this._permiumList._rowData.Count; __row++)
                    {
                        string __itemCode = this._permiumList._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                        string __unitCode = this._permiumList._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                        decimal __qty = MyLib._myGlobal._decimalPhase(this._permiumList._cellGet(__row, _g.d.ic_trans_detail._qty).ToString());
                        if (__itemCode.Length > 0)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(
                                "insert into " + _g.d.ic_purchase_permium_list._table +
                                " (" + _g.d.ic_purchase_permium_list._permium_code + "," + _g.d.ic_purchase_permium_list._ic_code + "," + _g.d.ic_purchase_permium_list._unit_code + "," + _g.d.ic_purchase_permium_list._qty + ") values " +
                                " (" + __code + ",\'" + __itemCode + "\',\'" + __unitCode + "\'," + __qty + ")"));
                        }
                    }
                    /*this._normalPrice._grid._updateRowIsChangeAll(true);
                    this._apPrice._grid._updateRowIsChangeAll(true);
                    __myQuery.Append(this._normalPrice._grid._createQueryForInsert(_g.d.ic_inventory_purchase_price._table, __fieldList, __dataList + "1,", false));
                    __myQuery.Append(this._apPrice._grid._createQueryForInsert(_g.d.ic_inventory_purchase_price._table, __fieldList, __dataList + "3,", false));*/
                    __myQuery.Append("</node>");

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                        if (this._myManageMain._mode == 1)
                        {
                            this._myManageMain._afterInsertData();
                            _clearScreen();
                            this._icPurchasePermiumScreen._focusFirst();
                        }
                        else
                        {
                            this._myManageMain._afterUpdateData();
                        }
                        if (clearData)
                        {
                            this._myManageDetail__clearData();
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

    public class _icPurchasePermiumScreenControl : MyLib._myScreen
    {
        public _icPurchasePermiumScreenControl()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m00");
            this._table_name = _g.d.ic_purchase_permium._table;
            this._maxColumn = 2;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_purchase_permium._permium_code, 1, 1, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_purchase_permium._name_1, 2, 1, 0, true, false, false);
            this._addDateBox(2, 0, 1, 0, _g.d.ic_purchase_permium._date_begin, 1, true, false);
            this._addDateBox(2, 1, 1, 0, _g.d.ic_purchase_permium._date_end, 1, true, false);
            this._addNumberBox(3, 0, 1, 0, _g.d.ic_purchase_permium._important, 1, 2, true, __formatNumber);
        }
    }
}
