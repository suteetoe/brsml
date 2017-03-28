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
    public partial class _icAllPrice : UserControl
    {
        string _oldCode = "";

        public _icAllPrice()
        {
            InitializeComponent();

            //this._myTabControlDetail.TableName = _g.d.ic_inventory_price._table;
            //this._myTabControlDetail._getResource();
            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._isLockRecordFromDatabaseActive = true;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            // this._myManageDetail._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._myManageDetail._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageDetail._manageButton = this._icAllPriceControl._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._icAllPriceControl._myPanel1;
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

            this._icAllPriceControl._closeButton.Click += _closeButton_Click;
            this._icAllPriceControl._saveButton.Click += _saveButton_Click;
            //this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._myManageDetail.Invalidate();
            //
            // this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
            this._myManageDetail._dataList._isLockDoc = true;
            if (MyLib._myGlobal._isUserLockDocument == true)
            {
                this._myManageDetail._dataList._buttonUnlockDoc.Visible = true;
                this._myManageDetail._dataList._buttonLockDoc.Visible = true;
                this._myManageDetail._dataList._separatorLockDoc.Visible = true;
            }

        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myQuery = new StringBuilder();

                string __itemCode = this._icAllPriceControl._icmainScreenTopControl1._getDataStrQuery(_g.d.ic_inventory._code).ToString();
                string __fieldList = _g.d.ic_inventory_price._ic_code + " , " + _g.d.ic_inventory_price._price_type + "," + _g.d.ic_inventory_price._price_mode + ",";
                string __dataList = this._icAllPriceControl._icmainScreenTopControl1._getDataStrQuery(_g.d.ic_inventory._code) + " , ";

                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                // Delete
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._ic_code + "=" + __itemCode + " and " + _g.d.ic_inventory_price._price_mode + " in (0, 1)"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=" + __itemCode));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " in (" + __itemCode.ToUpper() + ")"));


                this._icAllPriceControl._gridStandardPrice0._grid._updateRowIsChangeAll(true);
                this._icAllPriceControl._gridStandardPrice1._grid._updateRowIsChangeAll(true);
                this._icAllPriceControl._gridStandardPrice2._grid._updateRowIsChangeAll(true);

                this._icAllPriceControl._gridNormalPrice0._grid._updateRowIsChangeAll(true);
                this._icAllPriceControl._gridNormalPrice1._grid._updateRowIsChangeAll(true);
                this._icAllPriceControl._gridNormalPrice2._grid._updateRowIsChangeAll(true);

                __myQuery.Append(this._icAllPriceControl._gridStandardPrice0._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "1,0,", false, true));
                __myQuery.Append(this._icAllPriceControl._gridStandardPrice1._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "2,0,", false, true));
                __myQuery.Append(this._icAllPriceControl._gridStandardPrice2._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "3,0,", false, true));

                __myQuery.Append(this._icAllPriceControl._gridNormalPrice0._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "1,1,", false, true));
                __myQuery.Append(this._icAllPriceControl._gridNormalPrice1._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "2,1,", false, true));
                __myQuery.Append(this._icAllPriceControl._gridNormalPrice2._grid._createQueryForInsert(_g.d.ic_inventory_price._table, __fieldList, __dataList + "3,1,", false, true));

                // price formula
                __fieldList = _g.d.ic_inventory_price_formula._ic_code + ",";
                __dataList = this._icAllPriceControl._icmainScreenTopControl1._getDataStrQuery(_g.d.ic_inventory._code) + ",";
                this._icAllPriceControl._gridPriceFormula._updateRowIsChangeAll(true);
                __myQuery.Append(this._icAllPriceControl._gridPriceFormula._createQueryForInsert(_g.d.ic_inventory_price_formula._table, __fieldList, __dataList, false));

                // barcode
                 __fieldList = _g.d.ic_inventory_barcode._ic_code + " , ";
                __dataList = this._icAllPriceControl._icmainScreenTopControl1._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                this._icAllPriceControl._gridBarcode._updateRowIsChangeAll(true);
                __myQuery.Append(this._icAllPriceControl._gridBarcode._createQueryForInsert(_g.d.ic_inventory_barcode._table, __fieldList, __dataList, false));


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

        void _clearScreen()
        {
            this._icAllPriceControl._clear();
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
                    + " (select  " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._unit_code + ") as " + _g.d.ic_inventory_price._unit_name + ","
                    + " (select  " + _g.d.ar_group._name_1 + " from " + _g.d.ar_group._table + " where " + _g.d.ar_group._table + "." + _g.d.ar_group._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_1 + ") as " + _g.d.ic_inventory_price._group_name_1 + ","
                    + " (select  " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_2
                    + " and " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._main_group + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_1 + ") as " + _g.d.ic_inventory_price._group_name_2 + ","
                    + " (select  " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_code + ") as " + _g.d.ic_inventory_price._cust_name
                    + " from " + _g.d.ic_inventory_price._table
                    + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + _oldCode + "\' ";

                // มาตรฐาน
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_price._price_mode + "=0 and " + _g.d.ic_inventory_price._price_type + "= 1 order by " + _g.d.ic_inventory_price._unit_code + ",roworder")); //somruk
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_price._price_mode + "=0 and " + _g.d.ic_inventory_price._price_type + "=2"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_price._price_mode + "=0 and " + _g.d.ic_inventory_price._price_type + "=3"));

                // ทั่วไป
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_price._price_mode + "=1 and " + _g.d.ic_inventory_price._price_type + "= 1 order by " + _g.d.ic_inventory_price._unit_code + ",roworder")); //somruk
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_price._price_mode + "=1 and " + _g.d.ic_inventory_price._price_type + "=2"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__getPriceQuery + " and " + _g.d.ic_inventory_price._price_mode + "=1 and " + _g.d.ic_inventory_price._price_type + "=3"));

                //สูตร
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + _oldCode + "\' order by " + _g.d.ic_inventory_price_formula._unit_code + "," + _g.d.ic_inventory_price_formula._sale_type));

                // บารโค๊ด
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + "=\'" + _oldCode.ToUpper() + "\' order by (select " + _g.d.ic_unit_use._ratio + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + ")"));

                __myquery.Append("</node>");

                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icAllPriceControl._icmainScreenTopControl1._loadData(((DataSet)__getData[0]).Tables[0]);

                this._icAllPriceControl._gridNormalPrice0._grid._clear();
                this._icAllPriceControl._gridNormalPrice1._grid._clear();
                this._icAllPriceControl._gridNormalPrice2._grid._clear();
                this._icAllPriceControl._gridStandardPrice0._grid._clear();
                this._icAllPriceControl._gridStandardPrice1._grid._clear();
                this._icAllPriceControl._gridStandardPrice2._grid._clear();

                //
                this._icAllPriceControl._gridNormalPrice0._removeEvent();
                this._icAllPriceControl._gridNormalPrice1._removeEvent();
                this._icAllPriceControl._gridNormalPrice2._removeEvent();
                this._icAllPriceControl._gridStandardPrice0._removeEvent();
                this._icAllPriceControl._gridStandardPrice1._removeEvent();
                this._icAllPriceControl._gridStandardPrice2._removeEvent();
                //
                this._icAllPriceControl._gridStandardPrice0._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._icAllPriceControl._gridStandardPrice1._grid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                this._icAllPriceControl._gridStandardPrice2._grid._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
                this._icAllPriceControl._gridNormalPrice0._grid._loadFromDataTable(((DataSet)__getData[4]).Tables[0]);
                this._icAllPriceControl._gridNormalPrice1._grid._loadFromDataTable(((DataSet)__getData[5]).Tables[0]);
                this._icAllPriceControl._gridNormalPrice2._grid._loadFromDataTable(((DataSet)__getData[6]).Tables[0]);

                this._icAllPriceControl._gridPriceFormula._loadFromDataTable(((DataSet)__getData[7]).Tables[0]);
                this._icAllPriceControl._gridBarcode._loadFromDataTable(((DataSet)__getData[8]).Tables[0]);

                //
                this._icAllPriceControl._gridNormalPrice0._addEvent();
                this._icAllPriceControl._gridNormalPrice1._addEvent();
                this._icAllPriceControl._gridNormalPrice2._addEvent();
                this._icAllPriceControl._gridStandardPrice0._addEvent();
                this._icAllPriceControl._gridStandardPrice1._addEvent();
                this._icAllPriceControl._gridStandardPrice2._addEvent();
                //
                this._icAllPriceControl._icmainScreenTopControl1.Invalidate();
                __result = true;

                this._icAllPriceControl._gridNormalPrice0._itemCode = this._icAllPriceControl._gridNormalPrice2._itemCode = this._icAllPriceControl._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code, false);
                this._icAllPriceControl._gridNormalPrice1._itemCode = this._icAllPriceControl._gridNormalPrice2._itemCode = this._icAllPriceControl._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code, false);
                this._icAllPriceControl._gridNormalPrice2._itemCode = this._icAllPriceControl._gridNormalPrice2._itemCode = this._icAllPriceControl._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code, false);

                this._icAllPriceControl._gridStandardPrice0._itemCode = this._icAllPriceControl._gridStandardPrice2._itemCode = this._icAllPriceControl._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code, false);
                this._icAllPriceControl._gridStandardPrice1._itemCode = this._icAllPriceControl._gridStandardPrice2._itemCode = this._icAllPriceControl._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code, false);
                this._icAllPriceControl._gridStandardPrice2._itemCode = this._icAllPriceControl._gridStandardPrice2._itemCode = this._icAllPriceControl._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code, false);

            }
            catch
            {

            }
            return __result;

        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__clearData()
        {
            this._clearScreen();
        }
    }
}
