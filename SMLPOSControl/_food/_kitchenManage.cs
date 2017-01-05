using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _kitchenManage : UserControl
    {
        private _kitchenControl _screen = new _kitchenControl();
        private string _oldCode = "";

        public _kitchenManage()
        {
            InitializeComponent();

            this._screen._toolStrip.EnabledChanged += new EventHandler(_toolStrip_EnabledChanged);
            //
            this._screen.Dock = DockStyle.Fill;
            this._screen.Enabled = false;
            this._myManageData1._form2.Controls.Add(this._screen);
            //
            this._myManageData1._displayMode = 0;
            this._myManageData1._dataList._lockRecord = true;
            this._myManageData1._dataList._loadViewFormat(_g.g._screen_kitchen_master, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData1._dataList._referFieldAdd(_g.d.kitchen_master._code, 1);
            this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageData1._manageButton = this._screen._toolStrip;
            this._myManageData1._manageBackgroundPanel = this._screen._panel;
            this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._myManageData1._checkEditData += new MyLib.CheckEditDataEvent(_myManageData1__checkEditData);
            this._myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
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
            this._screen._reLoad();
        }

        void _myManageData1__newDataClick()
        {
            this._screen._reLoad();
        }

        void _toolStrip_EnabledChanged(object sender, EventArgs e)
        {
            this._screen.Enabled = true;
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
            this._oldCode = "";
            this._screen._myScreen1._clear();
            this._screen._itemGrid._clear();
            this._screen._orderComputerGrid._clear();
            this._screen._gridPrintCopy._clear();
            this._screen._gridCancelCopy._clear();
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
            StringBuilder __myQuery = new StringBuilder();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int loop = 0; loop < selectRowOrder.Count; loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[loop];
                int _getColumnNumber = this._myManageData1._dataList._gridData._findColumnByName(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._code);
                string __code = this._myManageData1._dataList._gridData._cellGet(getData.row, _getColumnNumber).ToString().ToUpper();
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master._code) + " = \'" + __code + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master_order_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_order_id._kitchen_code) + " = \'" + __code + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master_item._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_item._kitchen_code) + " = \'" + __code + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master_pos_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_pos_id._kitchen_code) + " = \'" + __code + "\'"));

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_print_copy._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_print_copy._kitchen_code) + " = \'" + __code + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_print_cancel_copy._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_print_cancel_copy._kitchen_code) + " = \'" + __code + "\'"));
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

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._oldCode = __rowDataArray[1].ToString().ToUpper();
                StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master._code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_master_order_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_order_id._kitchen_code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.kitchen_master_item._food_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.kitchen_master_item._food_code + ") as " + _g.d.kitchen_master_item._food_name + " from " + _g.d.kitchen_master_item._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_item._kitchen_code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_master_pos_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_pos_id._kitchen_code) + "=\'" + this._oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_print_copy._table + " where  " + MyLib._myGlobal._addUpper(_g.d.kitchen_print_copy._kitchen_code) + "= \'" + this._oldCode + "\' " + " order by " + _g.d.kitchen_print_copy._line_number));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_print_cancel_copy._table + " where  " + MyLib._myGlobal._addUpper(_g.d.kitchen_print_cancel_copy._kitchen_code) + "= \'" + this._oldCode + "\' " + " order by " + _g.d.kitchen_print_cancel_copy._line_number));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screen._myScreen1._loadData(((DataSet)__getData[0]).Tables[0]);
                this._screen._myPrintOptionScreen._loadData(((DataSet)__getData[0]).Tables[0]);

                this._screen._reLoad();
                DataTable __selectKitchen = ((DataSet)__getData[1]).Tables[0];
                if (__selectKitchen.Rows.Count > 0)
                {
                    for (int __row = 0; __row < this._screen._orderComputerGrid._rowData.Count; __row++)
                    {
                        string __code = this._screen._orderComputerGrid._cellGet(__row, _g.d.kitchen_master_order_id._order_device).ToString();
                        if (__selectKitchen.Select(_g.d.kitchen_master_order_id._order_device + "=\'" + __code + "\'").Length > 0)
                        {
                            this._screen._orderComputerGrid._cellUpdate(__row, _g.d.kitchen_master_order_id._selected, 1, false);
                        }
                    }
                }

                // pos
                DataTable __posSelectItem = ((DataSet)__getData[3]).Tables[0];
                if (__posSelectItem.Rows.Count > 0)
                {
                    for (int __row = 0; __row < this._screen._pos_id_grid._rowData.Count; __row++)
                    {
                        string __code = this._screen._pos_id_grid._cellGet(__row, _g.d.kitchen_master_pos_id._pos_id).ToString();
                        if (__posSelectItem.Select(_g.d.kitchen_master_pos_id._pos_id + "=\'" + __code + "\'").Length > 0)
                        {
                            this._screen._pos_id_grid._cellUpdate(__row, _g.d.kitchen_master_order_id._selected, 1, false);
                        }
                    }
                }

                this._screen._itemGrid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                this._screen._gridPrintCopy._loadFromDataTable(((DataSet)__getData[4]).Tables[0]);
                this._screen._gridCancelCopy._loadFromDataTable(((DataSet)__getData[5]).Tables[0]);

                this._screen._itemGrid.Invalidate();
                this._screen._orderComputerGrid.Invalidate();
                this._screen._myScreen1.Invalidate();
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
            try
            {
                string __getEmtry = this._screen._myScreen1._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master._code) + "=\'" + this._oldCode + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master_order_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_order_id._kitchen_code) + "=\'" + this._oldCode + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master_item._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_item._kitchen_code) + "=\'" + this._oldCode + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_master_pos_id._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_master_pos_id._kitchen_code) + "=\'" + this._oldCode + "\'"));

                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_print_copy._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_print_copy._kitchen_code) + "=\'" + this._oldCode + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.kitchen_print_cancel_copy._table + " where " + MyLib._myGlobal._addUpper(_g.d.kitchen_print_cancel_copy._kitchen_code) + "=\'" + this._oldCode + "\'"));
                    ArrayList __getData = this._screen._myScreen1._createQueryForDatabase();
                    ArrayList __getPrintDatae = this._screen._myPrintOptionScreen._createQueryForDatabase();

                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_master._table + " (" + __getData[0].ToString() + ", " + __getPrintDatae[0] + ") values (" + __getData[1].ToString() + ","+ __getPrintDatae[1].ToString() + ")"));
                    string __code = this._screen._myScreen1._getDataStr(_g.d.kitchen_master._code);
                    //
                    for (int __row = 0; __row < this._screen._orderComputerGrid._rowData.Count; __row++)
                    {
                        int __select = (int)MyLib._myGlobal._decimalPhase(this._screen._orderComputerGrid._cellGet(__row, _g.d.kitchen_master_order_id._selected).ToString());
                        if (__select == 1)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_master_order_id._table + " (" + _g.d.kitchen_master_order_id._kitchen_code + "," + _g.d.kitchen_master_order_id._order_device + ") values (\'" + __code + "\',\'" + this._screen._orderComputerGrid._cellGet(__row, _g.d.kitchen_master_order_id._order_device).ToString() + "\'" + ")"));
                        }
                    }
                    //
                    for (int __row = 0; __row < this._screen._itemGrid._rowData.Count; __row++)
                    {
                        string __itemCode = this._screen._itemGrid._cellGet(__row, _g.d.kitchen_master_item._food_code).ToString();
                        if (__itemCode.Length > 0)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_master_item._table + " (" + _g.d.kitchen_master_item._kitchen_code + "," + _g.d.kitchen_master_item._food_code + ") values (\'" + __code + "\',\'" + this._screen._itemGrid._cellGet(__row, _g.d.kitchen_master_item._food_code).ToString() + "\'" + ")"));
                        }
                    }
                    //
                    //
                    for (int __row = 0; __row < this._screen._pos_id_grid._rowData.Count; __row++)
                    {
                        int __select = (int)MyLib._myGlobal._decimalPhase(this._screen._pos_id_grid._cellGet(__row, _g.d.kitchen_master_pos_id._selected).ToString());
                        if (__select == 1)
                        {
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.kitchen_master_pos_id._table + " (" + _g.d.kitchen_master_pos_id._kitchen_code + "," + _g.d.kitchen_master_pos_id._pos_id + ") values (\'" + __code + "\',\'" + this._screen._pos_id_grid._cellGet(__row, _g.d.kitchen_master_pos_id._pos_id).ToString() + "\'" + ")"));
                        }
                    }

                    // print copy
                    this._screen._gridPrintCopy._updateRowIsChangeAll(true);
                    __myQuery.Append(this._screen._gridPrintCopy._createQueryForInsert(_g.d.kitchen_print_copy._table, _g.d.kitchen_print_copy._kitchen_code + ",", "\'" + __code + "\',", false, true));

                    // print cancel
                    this._screen._gridCancelCopy._updateRowIsChangeAll(true);
                    __myQuery.Append(this._screen._gridCancelCopy._createQueryForInsert(_g.d.kitchen_print_cancel_copy._table, _g.d.kitchen_print_cancel_copy._kitchen_code + ",", "\'" + __code + "\',", false, true));

                    __myQuery.Append("</node>");
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length == 0)
                    {
                        this._clear();
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

        private void _closeButton_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
