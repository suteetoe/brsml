using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icWarehouseLocation : UserControl
    {
        public _icWarehouseLocation()
        {
            InitializeComponent();

            this._myManageData1._dataList._buttonNew.Visible =
                this._myManageData1._dataList._buttonNewFromTemp.Visible =
                 this._myManageData1._dataList._buttonDelete.Visible =
                  this._myManageData1._dataList._buttonSelectAll.Visible =
                   this._myManageData1._dataList._printRangeButton.Visible = false;
            this._myManageData1._dataList._lockRecord = false;
            this._myManageData1._autoSize = true;
            this._myManageData1._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData1._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            this._myManageData1._loadDataToScreen += _myManageData1__loadDataToScreen;
            this._myManageData1._closeScreen += _myManageData1__closeScreen;
            this._myManageData1._clearData += _myManageData1__clearData;
            this._icmainScreenTopControl1.Enabled = false;

        }

        private void _myManageData1__clearData()
        {
            this._icmainScreenTopControl1._clear();
            this._icmainGridWarehouseLocationControl1._clear();
        }

        private void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        private bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList __rowDataArray = (ArrayList)rowData;
                int __itemCodeColumnNumber = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                string icCode = __rowDataArray[__itemCodeColumnNumber].ToString();

                string __whereString = whereString;
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                // 0
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * " +
                    ", (select " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._have_point + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as " + _g.d.ic_inventory._have_point +
                    ", (select " + _g.d.ic_unit_use._width_length_height + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + _g.d.ic_inventory._width_length_height +
                    ", (select " + _g.d.ic_unit_use._weight + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_cost + ") as " + _g.d.ic_inventory._weight +
                    //", (select " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._have_point + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._table + "." + _g.d.ic_inventory_detail._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as " + _g.d.ic_inventory._have_point + 
                    " from " + _g.d.ic_inventory._table + __whereString));

                // 3
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._icmainGridWarehouseLocationControl1._createQueryForLoad(icCode)));


                __myquery.Append("</node>");

                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTopControl1._clear();
                this._icmainScreenTopControl1._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainGridWarehouseLocationControl1._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
            }
            catch
            {
                return false;
            }

            return false;
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MyLib._myGlobal._resource("ต้องการล้างข้อมูลหรือไม่"), "เตือน", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this._icmainGridWarehouseLocationControl1._clear();
            }
        }

        private void _getWareHouseAndLocationButton_Click(object sender, EventArgs e)
        {
            Boolean __process = true;
            if (this._icmainGridWarehouseLocationControl1._rowData.Count > 0)
            {
                DialogResult __check = MessageBox.Show(MyLib._myGlobal._resource("ต้องการดึงข้อมูลใหม่มาแทนของเดิมหรือไม่"), "เตือน", MessageBoxButtons.YesNo);
                if (__check == DialogResult.No)
                {
                    __process = false;
                }
            }
            if (__process)
            {
                this._icmainGridWarehouseLocationControl1._clear();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select *,coalesce((select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_shelf._whcode + "),'') as wh_name_1 from " + _g.d.ic_shelf._table + " order by " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code;
                DataSet __result = __myFrameWork._queryShort(__query);
                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                {
                    string __whCode = __result.Tables[0].Rows[__row][_g.d.ic_shelf._whcode].ToString();
                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_shelf._code].ToString();
                    int __addr = this._icmainGridWarehouseLocationControl1._addRow();
                    this._icmainGridWarehouseLocationControl1._cellUpdate(__addr, _g.d.ic_wh_shelf._wh_code, __whCode, false);
                    this._icmainGridWarehouseLocationControl1._cellUpdate(__addr, _g.d.ic_wh_shelf._wh_name, __result.Tables[0].Rows[__row]["wh_name_1"].ToString(), false);
                    this._icmainGridWarehouseLocationControl1._cellUpdate(__addr, _g.d.ic_wh_shelf._shelf_code, __shelfCode, false);
                    this._icmainGridWarehouseLocationControl1._cellUpdate(__addr, _g.d.ic_wh_shelf._shelf_name, __result.Tables[0].Rows[__row][_g.d.ic_shelf._name_1].ToString(), false);
                }
                this._icmainGridWarehouseLocationControl1.Invalidate();
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
        }

        void _saveData()
        {
            try
            {
                string __getEmtry = this._icmainScreenTopControl1._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    string __itemCode = this._icmainScreenTopControl1._getDataStr(_g.d.ic_inventory._code);
                    string __fieldList = _g.d.ic_inventory_detail._ic_code + " , ";
                    string __dataList = this._icmainScreenTopControl1._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                    // do save
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                    // คลังที่เก็บ
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_wh_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " =  \'" + __itemCode.ToUpper() + "\' "));
                    this._icmainGridWarehouseLocationControl1._updateRowIsChangeAll(true);
                    __myQuery.Append(this._icmainGridWarehouseLocationControl1._createQueryForInsert(_g.d.ic_wh_shelf._table, __fieldList, __dataList, false));

                    __myQuery.Append("</node>");

                    string __debugQuery = __myQuery.ToString();

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);

                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                }
            catch (Exception ex)
            {

            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
