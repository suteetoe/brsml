using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _colorInfo : UserControl
    {
        public _colorInfo()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myManageDetail._dataList._fullMode = false;
            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            this._myManageDetail._dataList._loadViewFormat("screen_ic_color", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            this._myManageDetail._dataList._extraWhere = _g.d.ic_inventory._item_type + "=5";
            this._myManageDetail._calcArea();
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._autoSizeHeight = 450;
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail.Invalidate();
            this._qtyTextBox.Text = "1";
            this._icTransItemGrid._vatRate += new _icTransItemGridControl.VatRateEventHandler(_icTransItemGrid__vatRate);
        }

        decimal _icTransItemGrid__vatRate()
        {
            return _g.g._companyProfile._vat_rate;
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageDetail__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                this._icTransItemGrid._clear();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + whereString));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTop._clear();
                this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainScreenTop._search(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            //
            this._icTransItemGrid._vatType -= new _icTransItemGridControl.VatTypeEventHandler(_icTransItemGrid__vatType);
            this._icTransItemGrid._vatType += new _icTransItemGridControl.VatTypeEventHandler(_icTransItemGrid__vatType);
            //
            this._icTransItemGrid._fixedItemSetRow = false;
            this._icTransItemGrid._colorQtyShow = false;
            this._icTransItemGrid._clear();
            string __itemCode = this._icmainScreenTop._getDataStr(_g.d.ic_inventory._code);
            decimal __itemQty = MyLib._myGlobal._decimalPhase(this._qtyTextBox.Text);
            int __addr = this._icTransItemGrid._addRow();
            this._icTransItemGrid._selectRow = __addr;
            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __itemCode, true);
            this._icTransItemGrid._cellUpdate(__addr, _g.d.ic_trans_detail._qty, __itemQty, true);
            //
            StringBuilder __colorList = new StringBuilder();
            for (int __row = 3; __row < this._icTransItemGrid._rowData.Count; __row++)
            {
                string __getItemCode = this._icTransItemGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                if (__getItemCode.Length > 0)
                {
                    if (__colorList.Length > 0)
                    {
                        __colorList.Append(",");
                    }
                    __colorList.Append("\'" + __getItemCode + "\'");
                }
            }
            // ดึงต้นทุนสี base
            int __baseRow = 1;
            string __baseCode = this._icTransItemGrid._cellGet(__baseRow, _g.d.ic_trans_detail._item_code).ToString();
            decimal __baseQty = (decimal)this._icTransItemGrid._cellGet(__baseRow, _g.d.ic_trans_detail._qty);
            decimal __baseAmount = (decimal)this._icTransItemGrid._cellGet(__baseRow, _g.d.ic_trans_detail._sum_amount);
            //
            StringBuilder __getDataQuery = new StringBuilder();
            __getDataQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __getDataQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._average_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __baseCode + "\'"));
            __getDataQuery.Append("</node>");
            //
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __getDataQuery.ToString());
            //
            DataTable __baseTable = ((DataSet)__getData[0]).Tables[0];
            decimal __baseCost = MyLib._myGlobal._decimalPhase(__baseTable.Rows[0][_g.d.ic_inventory._average_cost].ToString()) * __baseQty;
            this._icTransItemGrid._cellUpdate(__baseRow, _g.d.ic_trans_detail._sum_of_cost, __baseCost, false);
            this._icTransItemGrid._cellUpdate(__baseRow, _g.d.ic_trans_detail._profit, __baseAmount - __baseCost, false);
            // คำนวณหาต้นทุน สีผสม
            decimal __colorCost = (decimal)this._icTransItemGrid._findCostByItemSet(__itemCode, __itemQty);
            decimal __colorAmount = (decimal)this._icTransItemGrid._cellGet(2, _g.d.ic_trans_detail._sum_amount);
            this._icTransItemGrid._cellUpdate(2, _g.d.ic_trans_detail._sum_of_cost, __colorCost, false);
            this._icTransItemGrid._cellUpdate(2, _g.d.ic_trans_detail._profit, __colorAmount - __colorCost, false);
            // รวมต้นทุนไว้บรรทัดแรก
            decimal __sumAmount = (decimal)this._icTransItemGrid._cellGet(0, _g.d.ic_trans_detail._sum_amount);
            decimal __sumCost = __baseCost + __colorCost;
            this._icTransItemGrid._cellUpdate(0, _g.d.ic_trans_detail._sum_of_cost, __sumCost, false);
            this._icTransItemGrid._cellUpdate(0, _g.d.ic_trans_detail._profit, __sumAmount - __sumCost, false);

        }

        _g.g._vatTypeEnum _icTransItemGrid__vatType(object sender)
        {
            return _g.g._vatTypeEnum.ภาษีรวมใน;
        }
    }
}
