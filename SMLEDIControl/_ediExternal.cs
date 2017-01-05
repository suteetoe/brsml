using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLEDIControl
{
    public partial class _ediExternal : UserControl
    {
        string _oldCode = "";
        public _ediExternal()
        {
            InitializeComponent();

            // screen_edi_external

            this._myManageData1._displayMode = 0;
            this._myManageData1._dataList._lockRecord = true;
            this._myManageData1._isLockRecordFromDatabaseActive = false;
            this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            // this._myManageData1._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._myManageData1._dataList._loadViewFormat("screen_edi_external", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._myManageData1._manageButton = this._ediExternalScreenControl._myToolbar;
            this._myManageData1._manageBackgroundPanel = this._ediExternalScreenControl._myPanel;
            this._myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            // this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._myManageData1._dataList._referFieldAdd(_g.d.edi_external._code, 1);

            //this._myManageData1._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            //this._myManageData1._checkEditData += new MyLib.CheckEditDataEvent(_myManageData1__checkEditData);
            //this._myManageData1._dataList._loadViewData(0);

            /*
            this._myManageData1._dataList._buttonNew.Visible = false;
            this._myManageData1._dataList._buttonNewFromTemp.Visible = false;
            this._myManageData1._dataList._buttonSelectAll.Visible = false;
            this._myManageData1._dataList._buttonDelete.Visible = false;*/

            this._myManageData1._calcArea();
            this._myManageData1._dataListOpen = true;
            this._myManageData1._autoSize = true;
            this._myManageData1._autoSizeHeight = 450;

            //this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._ediExternalScreenControl._saveButton.Click += _saveButton_Click;
            this._ediExternalScreenControl._closeButton.Click += _closeButton_Click;
            this._myManageData1.Invalidate();
        }

        void _saveData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            try
            {

                this._ediExternalScreenControl._gridProduct._removeLastControl();
                this._ediExternalScreenControl._gridBarcode._removeLastControl();
                this._ediExternalScreenControl._gridUnit._removeLastControl();
                this._ediExternalScreenControl._gridCustomer._removeLastControl();

                StringBuilder __myQuery = new StringBuilder(MyLib._myGlobal._xmlHeader);
                __myQuery.Append("<node>");

                ArrayList __screenTop = this._ediExternalScreenControl._screenTop._createQueryForDatabase();
                string __externalCode = this._ediExternalScreenControl._screenTop._getDataStr(_g.d.edi_external._code);

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\' "));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.edi_barcode_list._table + " where " + _g.d.edi_barcode_list._edi_code + "=\'" + __externalCode + "\' "));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.edi_unit_list._table + " where " + _g.d.edi_unit_list._edi_code + "=\'" + __externalCode + "\' "));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.edi_ar_list._table + " where " + _g.d.edi_ar_list._edi_code + "=\'" + __externalCode + "\' "));

                if (this._myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.edi_external._table + "(" + __screenTop[0] + ") values (" + __screenTop[1] + ")"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.edi_external._table + " set " + __screenTop[2] + " where " + _g.d.edi_external._code + "=\'" + __externalCode + "\' "));


                }

                // remove empty row
                for (int __row = this._ediExternalScreenControl._gridProduct._rowData.Count - 1; __row >= 0; __row--)
                {
                    if (this._ediExternalScreenControl._gridProduct._cellGet(__row, 0).ToString().Length == 0)
                    {
                        this._ediExternalScreenControl._gridProduct._rowData.RemoveAt(__row);
                    }
                }

                for (int __row = this._ediExternalScreenControl._gridUnit._rowData.Count - 1; __row >= 0; __row--)
                {
                    if (this._ediExternalScreenControl._gridUnit._cellGet(__row, 0).ToString().Length == 0)
                    {
                        this._ediExternalScreenControl._gridUnit._rowData.RemoveAt(__row);
                    }
                }

                for (int __row = this._ediExternalScreenControl._gridBarcode._rowData.Count - 1; __row >= 0; __row--)
                {
                    if (this._ediExternalScreenControl._gridBarcode._cellGet(__row, 0).ToString().Length == 0)
                    {
                        this._ediExternalScreenControl._gridBarcode._rowData.RemoveAt(__row);
                    }
                }


                for (int __row = this._ediExternalScreenControl._gridCustomer._rowData.Count - 1; __row >= 0; __row--)
                {
                    if (this._ediExternalScreenControl._gridCustomer._cellGet(__row, 0).ToString().Length == 0)
                    {
                        this._ediExternalScreenControl._gridCustomer._rowData.RemoveAt(__row);
                    }
                }

                // update data only

                // อย่าลืม Event _queryForUpdateWhere ไม่งั้นมันไม่ทำงานนะ                   
                // ต่อท้ายด้วย Insert บรรทัดใหม่
                // product
                string __fieldUpdate = _g.d.edi_product_list._edi_code + "=\'" + __externalCode + "\'";
                string __fieldList = _g.d.edi_product_list._edi_code + ",";
                string __dataList = "\'" + __externalCode + "\',";

                /*
                __myQuery.Append(this._ediExternalScreenControl._gridProduct._createQueryRowRemove(_g.d.edi_product_list._table));
                __myQuery.Append(this._ediExternalScreenControl._gridProduct._createQueryForUpdate(_g.d.edi_product_list._table, __fieldUpdate));
                __myQuery.Append(this._ediExternalScreenControl._gridProduct._createQueryForInsert(_g.d.edi_product_list._table, __fieldList, __dataList, true));
                */
                this._ediExternalScreenControl._gridProduct._updateRowIsChangeAll(true);
                __myQuery.Append(this._ediExternalScreenControl._gridProduct._createQueryForInsert(_g.d.edi_product_list._table, __fieldList, __dataList, false, true));

                // barcode
                __fieldUpdate = _g.d.edi_barcode_list._edi_code + "=\'" + __externalCode + "\'";
                __fieldList = _g.d.edi_barcode_list._edi_code + ",";
                __dataList = "\'" + __externalCode + "\',";
                /*
                __myQuery.Append(this._ediExternalScreenControl._gridBarcode._createQueryRowRemove(_g.d.edi_barcode_list._table));
                __myQuery.Append(this._ediExternalScreenControl._gridBarcode._createQueryForUpdate(_g.d.edi_barcode_list._table, __fieldUpdate));
                __myQuery.Append(this._ediExternalScreenControl._gridBarcode._createQueryForInsert(_g.d.edi_barcode_list._table, __fieldList, __dataList, true));
                */
                this._ediExternalScreenControl._gridBarcode._updateRowIsChangeAll(true);
                __myQuery.Append(this._ediExternalScreenControl._gridBarcode._createQueryForInsert(_g.d.edi_barcode_list._table, __fieldList, __dataList, false, true));

                // unit
                __fieldUpdate = _g.d.edi_unit_list._edi_code + "=\'" + __externalCode + "\'";
                __fieldList = _g.d.edi_unit_list._edi_code + ",";
                __dataList = "\'" + __externalCode + "\',";
                /*
                __myQuery.Append(this._ediExternalScreenControl._gridUnit._createQueryRowRemove(_g.d.edi_unit_list._table));
                __myQuery.Append(this._ediExternalScreenControl._gridUnit._createQueryForUpdate(_g.d.edi_unit_list._table, __fieldUpdate));
                __myQuery.Append(this._ediExternalScreenControl._gridUnit._createQueryForInsert(_g.d.edi_unit_list._table, __fieldList, __dataList, true));
                */
                this._ediExternalScreenControl._gridUnit._updateRowIsChangeAll(true);
                __myQuery.Append(this._ediExternalScreenControl._gridUnit._createQueryForInsert(_g.d.edi_unit_list._table, __fieldList, __dataList, false, true));

                // customer
                __fieldUpdate = _g.d.edi_ar_list._edi_code + "=\'" + __externalCode + "\'";
                __fieldList = _g.d.edi_ar_list._edi_code + ",";
                __dataList = "\'" + __externalCode + "\',";
                /*
                __myQuery.Append(this._ediExternalScreenControl._gridUnit._createQueryRowRemove(_g.d.edi_unit_list._table));
                __myQuery.Append(this._ediExternalScreenControl._gridUnit._createQueryForUpdate(_g.d.edi_unit_list._table, __fieldUpdate));
                __myQuery.Append(this._ediExternalScreenControl._gridUnit._createQueryForInsert(_g.d.edi_unit_list._table, __fieldList, __dataList, true));
                */
                this._ediExternalScreenControl._gridCustomer._updateRowIsChangeAll(true);
                __myQuery.Append(this._ediExternalScreenControl._gridCustomer._createQueryForInsert(_g.d.edi_ar_list._table, __fieldList, __dataList, false, true));

                __myQuery.Append("</node>");

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    if (this._myManageData1._mode == 1)
                    {
                        this._myManageData1._afterInsertData();
                        _clear();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _clear()
        {
            this._oldCode = "";
            this._ediExternalScreenControl._screenTop._clear();
            this._ediExternalScreenControl._gridProduct._clear();
            this._ediExternalScreenControl._gridBarcode._clear();
            this._ediExternalScreenControl._gridUnit._clear();
            this._ediExternalScreenControl._gridCustomer._clear();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
        }

        private void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        private void _myManageData1__clearData()
        {
            this._clear();
        }

        private bool _myManageData1__discardData()
        {
            return true;
        }

        private void _myManageData1__newDataClick()
        {
            this._clear();
        }

        private bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork __myFramWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();

                ArrayList __rowDataArray = (ArrayList)rowData;
                this._oldCode = __rowDataArray[1].ToString();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.edi_external._table + " where " + _g.d.edi_external._code + "=\'" + this._oldCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select name_1 from ic_inventory where ic_inventory.code = edi_product_list.ic_code ) as " + _g.d.edi_product_list._ic_name + " from " + _g.d.edi_product_list._table + " where " + _g.d.edi_product_list._edi_code + "=\'" + this._oldCode + "\' order by line_number "));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.edi_barcode_list._table + " where " + _g.d.edi_barcode_list._edi_code + "=\'" + this._oldCode + "\' order by line_number "));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select name_1 from ic_unit where ic_unit.code = edi_unit_list.unit_code ) as " + _g.d.edi_unit_list._unit_name + " from " + _g.d.edi_unit_list._table + " where " + _g.d.edi_unit_list._edi_code + "=\'" + this._oldCode + "\' order by line_number "));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select name_1 from ar_customer where ar_customer.code = edi_ar_list.ar_code ) as " + _g.d.edi_ar_list._ar_name + " from " + _g.d.edi_ar_list._table + " where " + _g.d.edi_ar_list._edi_code + "=\'" + this._oldCode + "\' order by line_number "));
                __myquery.Append("</node>");

                ArrayList __result = __myFramWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                if (__result.Count > 0)
                {
                    this._ediExternalScreenControl._screenTop._loadData(((DataSet)__result[0]).Tables[0]);
                    this._ediExternalScreenControl._gridProduct._loadFromDataTable(((DataSet)__result[1]).Tables[0]);
                    this._ediExternalScreenControl._gridBarcode._loadFromDataTable(((DataSet)__result[2]).Tables[0]);
                    this._ediExternalScreenControl._gridUnit._loadFromDataTable(((DataSet)__result[3]).Tables[0]);
                    this._ediExternalScreenControl._gridCustomer._loadFromDataTable(((DataSet)__result[4]).Tables[0]);
                    this._ediExternalScreenControl._search(false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
