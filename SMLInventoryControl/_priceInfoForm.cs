using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _priceInfoForm : Form
    {
        int _roworder;

        public _priceInfoForm(string itemCode, int roworder, string custCode)
        {
            InitializeComponent();
            try
            {
                this._myLabel1.Text = MyLib._myResource._findResource(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._tab_cust_price)._str;
                this._myLabel2.Text = MyLib._myResource._findResource(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._tab_cust_group_price)._str;
                this._myLabel3.Text = MyLib._myResource._findResource(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._tab_normal_price)._str;
                this._myLabel4.Text = MyLib._myResource._findResource(_g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._tab_standard_price)._str;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._roworder = roworder;
                this.Text = "Code : " + itemCode;
                //
                this._priceByCust._toolStrip1.Visible = false;
                this._priceByCustGroup._toolStrip1.Visible = false;
                this._priceNormal._toolStrip1.Visible = false;
                this._priceStandard._toolStrip1.Visible = false;
                //
                this._priceByCust._grid._isEdit = false;
                this._priceByCustGroup._grid._isEdit = false;
                this._priceNormal._grid._isEdit = false;
                this._priceStandard._grid._isEdit = false;
                //
                this._priceByCust._grid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_grid__beforeDisplayRow);
                this._priceByCustGroup._grid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_grid__beforeDisplayRow);
                this._priceNormal._grid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_grid__beforeDisplayRow);
                this._priceStandard._grid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_grid__beforeDisplayRow);
                //
                string __custGroupCode = "";
                string __queryCustGropup = "select " + _g.d.ar_customer_detail._group_main + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._ar_code + "=\'" + custCode + "\'";
                DataTable __getCustGroup = __myFrameWork._queryShort(__queryCustGropup).Tables[0];
                if (__getCustGroup.Rows.Count > 0)
                {
                    __custGroupCode = __getCustGroup.Rows[0][_g.d.ar_customer_detail._group_main].ToString();
                }
                //
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                // ราคาขายทั่วไป
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._getPrice(itemCode, 1, 1, false, "")));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._getPrice(itemCode, 2, 1, (__custGroupCode.Length == 0) ? false : true, __custGroupCode)));
                // ราคาขายมาตรฐาน
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._getPrice(itemCode, 1, 0, false, "")));
                //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this._getPrice(itemCode, 2, 1, (__custGroupCode.Length == 0) ? false : true, __custGroupCode)));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                // ดึงราคา ราคาขายทั่วไป
                this._priceNormal._grid._loadFromDataTable(((DataSet)__getData[0]).Tables[0]);
                // ดึงราคา ตามกลุ่มลูกค้า
                this._priceByCustGroup._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                // ดึงราคา มาตรฐาน
                this._priceStandard._grid._loadFromDataTable(((DataSet)__getData[2]).Tables[0]);
                //
                if (this._priceByCust._grid._rowData.Count == 0) this._splitContainer1.SplitterDistance = 0;
                if (this._priceByCustGroup._grid._rowData.Count == 0) this._splitContainer2.SplitterDistance = 0;
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        MyLib.BeforeDisplayRowReturn _grid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            int __columnRoworder = sender._findColumnByName("roworder");
            if (__columnRoworder != -1)
            {
                int __getRoworder = MyLib._myGlobal._intPhase(sender._cellGet(row, __columnRoworder).ToString());
                if (__getRoworder == this._roworder)
                {
                    senderRow.newColor = Color.Red;
                    senderRow.newFont = new Font(senderRow.newFont.FontFamily, senderRow.newFont.Size, FontStyle.Bold);
                }
            }
            return senderRow;
        }

        private string _getPrice(string itemCode, int type, int mode, Boolean byCustGroup, string custGroup)
        {
            // Query Copy มาจาก __icPriceList
            return "select *,"
                + " (select  " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._unit_code + ") as " + _g.d.ic_inventory_price._unit_name + ","
                + " (select  " + _g.d.ar_group._name_1 + " from " + _g.d.ar_group._table + " where " + _g.d.ar_group._table + "." + _g.d.ar_group._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_1 + ") as " + _g.d.ic_inventory_price._group_name_1 + ","
                + " (select  " + _g.d.ar_group_sub._name_1 + " from " + _g.d.ar_group_sub._table + " where " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_2
                + " and " + _g.d.ar_group_sub._table + "." + _g.d.ar_group_sub._main_group + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_group_1 + ") as " + _g.d.ic_inventory_price._group_name_2 + ","
                + " (select  " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._cust_code + ") as " + _g.d.ic_inventory_price._cust_name
                + " from " + _g.d.ic_inventory_price._table
                + " where " + _g.d.ic_inventory_price._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_inventory_price._price_mode + "=" + mode.ToString() + " and " + _g.d.ic_inventory_price._price_type + "=" + type.ToString()
                + ((byCustGroup) ? (" and " + _g.d.ic_inventory_price._cust_group_1 + "=\'" + custGroup + "\'") : "")
                + " order by " + _g.d.ic_inventory_price._unit_code + ",roworder";
        }
    }
}
