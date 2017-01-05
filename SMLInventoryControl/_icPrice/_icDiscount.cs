using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLInventoryControl._icPrice
{
    public partial class _icDiscount : UserControl
    {
        string _oldCode = "";

        public _icDiscount()
        {
            InitializeComponent();

            //this._myTabControlDetail.TableName = _g.d.ic_inventory_price._table;
            //this._myTabControlDetail._getResource();
            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._isLockRecordFromDatabaseActive = false;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            // this._myManageDetail._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._myManageDetail._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageDetail._manageButton = this._icDiscountControl1._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._icDiscountControl1._myPanel1;
            //this._myManageDetail._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageDetail._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageDetail._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            //this._myManageDetail._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            //this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            //this._myManageDetail._dataList._loadViewData(0);

            this._myManageDetail._dataList._buttonNew.Visible = false;
            this._myManageDetail._dataList._buttonNewFromTemp.Visible = false;
            this._myManageDetail._dataList._buttonSelectAll.Visible = false;
            this._myManageDetail._dataList._buttonDelete.Visible = false;

            this._myManageDetail._calcArea();
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._autoSizeHeight = 450;

            this._icDiscountControl1._closeButton.Click += _closeButton_Click;
            this._icDiscountControl1._saveButton.Click += _saveButton_Click;
            //this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._myManageDetail.Invalidate();
            //
            // this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
        }

        void _saveData()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myQuery = new StringBuilder();

                string __itemCode = this._icDiscountControl1._icmainScreenTopControl1._getDataStrQuery(_g.d.ic_inventory._code).ToString();
                string __fieldList = _g.d.ic_inventory_price._ic_code + " , " + _g.d.ic_inventory_discount._discount_type + "," + _g.d.ic_inventory_discount._discount_mode + ",";
                string __dataList = this._icDiscountControl1._icmainScreenTopControl1._getDataStrQuery(_g.d.ic_inventory._code) + " , ";

                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");


                // Delete
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_discount._table + " where " + _g.d.ic_inventory_discount._ic_code + "=" + __itemCode));

                this._icDiscountControl1._gridNormalPrice0._grid._updateRowIsChangeAll(true);
                this._icDiscountControl1._gridNormalPrice1._grid._updateRowIsChangeAll(true);
                this._icDiscountControl1._gridNormalPrice2._grid._updateRowIsChangeAll(true);

                __myQuery.Append(this._icDiscountControl1._gridNormalPrice0._grid._createQueryForInsert(_g.d.ic_inventory_discount._table, __fieldList, __dataList + "0,0,", false, true));
                __myQuery.Append(this._icDiscountControl1._gridNormalPrice1._grid._createQueryForInsert(_g.d.ic_inventory_discount._table, __fieldList, __dataList + "1,0,", false, true));
                __myQuery.Append(this._icDiscountControl1._gridNormalPrice2._grid._createQueryForInsert(_g.d.ic_inventory_discount._table, __fieldList, __dataList + "2,0,", false, true));


                __myQuery.Append("</node>");

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    if (this._myManageDetail._mode == 1)
                    {
                        this._myManageDetail._afterInsertData();
                        _clearScreen();
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
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _closeButton_Click(object sender, EventArgs e)
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
                    + " (select  " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory_discount._table + "." + _g.d.ic_inventory_discount._unit_code + ") as " + _g.d.ic_inventory_discount._unit_name + ","
                    + " (select  " + _g.d.ar_group._name_1 + " from " + _g.d.ar_group._table + " where " + _g.d.ar_group._table + "." + _g.d.ar_group._code + "=" + _g.d.ic_inventory_discount._table + "." + _g.d.ic_inventory_discount._cust_group_1 + ") as " + _g.d.ic_inventory_discount._group_name_1 + ","
                    + " (select  " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._code + "=" + _g.d.ic_inventory_discount._table + "." + _g.d.ic_inventory_discount._cust_group_2
                    + " and " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._main_group + "=" + _g.d.ic_inventory_discount._table + "." + _g.d.ic_inventory_discount._cust_group_1 + ") as " + _g.d.ic_inventory_discount._group_name_2 + ","
                    + " (select  " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_inventory_discount._table + "." + _g.d.ic_inventory_discount._cust_code + ") as " + _g.d.ic_inventory_discount._cust_name

                    + " from " + _g.d.ic_inventory_discount._table
                    + " where " + _g.d.ic_inventory_discount._ic_code + "=\'" + _oldCode + "\' ";

                // ทั่วไป
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_discount._discount_type + "=0 order by " + _g.d.ic_inventory_discount._unit_code + ",roworder")); //somruk
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_discount._discount_type + "=1"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_discount._discount_type + "=2"));

                __myquery.Append("</node>");

                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icDiscountControl1._icmainScreenTopControl1._loadData(((DataSet)__getData[0]).Tables[0]);

                this._icDiscountControl1._gridNormalPrice0._grid._clear();
                this._icDiscountControl1._gridNormalPrice1._grid._clear();
                this._icDiscountControl1._gridNormalPrice2._grid._clear();

                this._icDiscountControl1._gridNormalPrice0._removeEvent();
                this._icDiscountControl1._gridNormalPrice1._removeEvent();
                this._icDiscountControl1._gridNormalPrice2._removeEvent();

                this._icDiscountControl1._gridNormalPrice0._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._icDiscountControl1._gridNormalPrice1._grid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                this._icDiscountControl1._gridNormalPrice2._grid._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);

                this._icDiscountControl1._gridNormalPrice0._addEvent();
                this._icDiscountControl1._gridNormalPrice1._addEvent();
                this._icDiscountControl1._gridNormalPrice2._addEvent();

                this._icDiscountControl1._icmainScreenTopControl1.Invalidate();
                __result = true;

                this._icDiscountControl1._gridNormalPrice0._itemCode =
                this._icDiscountControl1._gridNormalPrice1._itemCode =
                this._icDiscountControl1._gridNormalPrice2._itemCode = this._icDiscountControl1._gridNormalPrice2._itemCode = this._icDiscountControl1._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code, false);


            }
            catch
            {

            }

            return __result;
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _clearScreen()
        {
            this._icDiscountControl1._clear();
        }

        void _myManageDetail__clearData()
        {
            this._clearScreen();
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }
    }
}
