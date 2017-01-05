using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport
{
    public partial class _report_ic_color_movement : UserControl
    {
        SMLERPReportTool._conditionScreen _condition;
        DataSet _ds;
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;

        public _report_ic_color_movement()
        {
            InitializeComponent();
            this._condition = new SMLERPReportTool._conditionScreen(SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหวสีผสม, "");
            this._condition._processButton.Click += new EventHandler(_bt_process_Click);
            this._viewReport._buttonClose.Click += new EventHandler(_buttonClose_Click);
            this._viewReport._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._viewReport._pageSetupDialog.PageSettings.Landscape = false;
            this._viewReport._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._viewReport._getObject += new SMLReport._report.GetObjectEventHandler(_viewReport__getObject);
            this._viewReport._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_viewReport__getDataObject);
            this._viewReport._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_viewReport__loadDataByThread);
            this._conditionShow();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this._condition.Close();
            this._buildReport();
        }

        string _findResource(string tableName, string fieldName)
        {
            string __fieldName = string.Concat(tableName, ".", fieldName);
            return MyLib._myResource._findResource(__fieldName, __fieldName)._str;
        }

        void _viewReport__loadDataByThread()
        {
            if (this._ds == null)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append("select " + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._calc_flag + "," + _g.d.ic_trans_detail._trans_flag + ",");
                __query.Append("(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name);
                __query.Append(" from " + _g.d.ic_trans_detail._table);
                __query.Append(" where " + _g.d.ic_trans_detail._item_type + "=5 and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + ")");
                __query.Append(" and " + _g.d.ic_trans_detail._doc_date + " between \'" + MyLib._myGlobal._convertDateToQuery(this._condition._screen._getDataStr(_g.d.resource_report._from_date)) + "\' and \'" + MyLib._myGlobal._convertDateToQuery(this._condition._screen._getDataStr(_g.d.resource_report._to_date)) + "\'");
                string __getItemCode = _g.g._getItemCode(_g.d.ic_trans_detail._item_code, this._condition._grid);
                if (__getItemCode.Length > 0)
                {
                    __query.Append(" and " + __getItemCode);
                }
                __query.Append(" order by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number);
                this._ds = __myFrameWork._queryShort(__query.ToString());
                this._viewReport._loadDataByThreadSuccess = true;
            }
        }

        ArrayList _valueTotal;

        void _viewReport__getDataObject()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)this._objReport._columnList[0];
            Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
            Font __newFont_bold = new Font(__getColumn._fontData, FontStyle.Bold);
            DataRow[] __dr = this._ds.Tables[0].Select();
            string __lastItemCode = "";
            decimal __balanceQty = 0M;
            this._valueTotal = new ArrayList();
            for (int __loop = 0; __loop < 2; __loop++)
            {
                this._valueTotal.Add((decimal)0.0M);
            }
            for (int __row = 0; __row < __dr.Length; __row++)
            {
                string __itemCode = __dr[__row][_g.d.ic_trans_detail._item_code].ToString();
                string __itemName = __dr[__row][_g.d.ic_trans_detail._item_name].ToString();
                decimal __itemQty = MyLib._myGlobal._decimalPhase(__dr[__row][_g.d.ic_trans_detail._qty].ToString());
                string __docDate = MyLib._myGlobal._convertDateToString(MyLib._myGlobal._convertDate(__dr[__row][_g.d.ic_trans_detail._doc_date].ToString()), false);
                if (__itemCode.Equals(__lastItemCode) == false)
                {
                    this._printTotal(this._valueTotal);
                    SMLReport._report._objectListType __dataObjectItem = this._viewReport._addObject(this._viewReport._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._viewReport._createEmtryColumn(this._objReport, __dataObjectItem);
                    this._viewReport._addDataColumn(this._objReport, __dataObjectItem, 0, __dr[__row][_g.d.ic_trans_detail._item_code].ToString(), __newFont_bold, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport, __dataObjectItem, 1, __dr[__row][_g.d.ic_trans_detail._item_name].ToString(), __newFont_bold, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    __balanceQty = 0M;
                }
                string __strDate = __dr[__row][_g.d.ic_resource._doc_date].ToString();
                int __transFlag = MyLib._myGlobal._intPhase(__dr[__row][_g.d.ic_trans_detail._trans_flag].ToString());
                decimal __qty = MyLib._myGlobal._decimalPhase(__dr[__row][_g.d.ic_trans_detail._qty].ToString());
                decimal __qtyIn = 0M;
                decimal __qtyOut = 0M;
                {
                    __qtyIn = __qty;
                    __qtyOut = 0M;
                    __balanceQty = __balanceQty + (__qtyIn - __qtyOut);
                    SMLReport._report._objectListType __dataObject = this._viewReport._addObject(this._viewReport._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._viewReport._createEmtryColumn(this._objReport2, __dataObject);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 1, MyLib._myGlobal._convertDateFromQuery(__dr[__row][_g.d.ic_trans_detail._doc_date].ToString()).ToShortDateString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 2, _g.g._transFlagGlobal._transName(60), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 3, __dr[__row][_g.d.ic_trans_detail._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 4, __dr[__row][_g.d.ic_trans_detail._unit_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __qtyIn), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __qtyOut), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __balanceQty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    this._valueTotal[0] = (decimal)((decimal)this._valueTotal[0]) + __qtyIn;
                    this._valueTotal[1] = (decimal)((decimal)this._valueTotal[1]) + __qtyOut;
                }
                {
                    __qtyIn = 0M;
                    __qtyOut = __qty;
                    __balanceQty = __balanceQty + (__qtyIn - __qtyOut);
                    SMLReport._report._objectListType __dataObject = this._viewReport._addObject(this._viewReport._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._viewReport._createEmtryColumn(this._objReport2, __dataObject);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 1, MyLib._myGlobal._convertDateFromQuery(__dr[__row][_g.d.ic_trans_detail._doc_date].ToString()).ToShortDateString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 2, _g.g._transFlagGlobal._transName(__transFlag), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 3, __dr[__row][_g.d.ic_trans_detail._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 4, __dr[__row][_g.d.ic_trans_detail._unit_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __qtyIn), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __qtyOut), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    this._viewReport._addDataColumn(this._objReport2, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __balanceQty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    this._valueTotal[0] = (decimal)((decimal)this._valueTotal[0]) + __qtyIn;
                    this._valueTotal[1] = (decimal)((decimal)this._valueTotal[1]) + __qtyOut;
                }
                //
                __lastItemCode = __docDate;
            }
            this._printTotal(this._valueTotal);
        }

        private void _printTotal(ArrayList value)
        {
            Boolean __foundTotal = false;
            for (int __loop = 0; __loop < 2; __loop++)
            {
                if ((decimal)value[__loop] != 0)
                {
                    __foundTotal = true;
                    break;
                }
            }
            if (__foundTotal)
            {
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)this._objReport._columnList[0];
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
                //
                SMLReport._report._objectListType __dataObject = this._viewReport._addObject(this._viewReport._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._viewReport._createEmtryColumn(this._objReport2, __dataObject);
                this._viewReport._addDataColumn(this._objReport2, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport2, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport2, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport2, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport2, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport2, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, (decimal)value[0]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                this._viewReport._addDataColumn(this._objReport2, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, (decimal)value[1]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                value[0] = 0M;
                value[1] = 0M;
            }
        }

        bool _viewReport__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._viewReport._addObject(this._viewReport._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._viewReport._addColumn(__headerObject, 100);
                this._viewReport._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, this._viewReport._fontHeader1);
                //
                string __beginDate = this._condition._screen._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._condition._screen._getDataStr(_g.d.resource_report._to_date);
                string __conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                __conditionText = _g.g._conditionGrid(this._condition._grid, __conditionText);
                this._viewReport._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, __conditionText, SMLReport._report._cellAlign.Center, this._viewReport._fontHeader2);
                //
                this._viewReport._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title    : " + MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_info_stk_movement, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_info_stk_movement)._str, SMLReport._report._cellAlign.Left, this._viewReport._fontHeader2);
                this._viewReport._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._viewReport._fontHeader2);
                this._viewReport._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._viewReport._fontHeader2);
                this._viewReport._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._viewReport._fontHeader2);
                this._viewReport._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "Description : ", SMLReport._report._cellAlign.Left, this._viewReport._fontHeader2);
                this._viewReport._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, this._viewReport._fontHeader2);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    // Column บน
                    this._objReport = this._viewReport._addObject(this._viewReport._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    this._viewReport._addColumn(this._objReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._ic_code, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, _g.d.ic_resource._ic_code, SMLReport._report._cellAlign.Left);
                    this._viewReport._addColumn(this._objReport, 35, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._ic_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, _g.d.ic_resource._ic_name, SMLReport._report._cellAlign.Left);
                    this._viewReport._addColumn(this._objReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._in, _g.d.ic_resource._table + "." + _g.d.ic_resource._in, _g.d.ic_resource._in, SMLReport._report._cellAlign.Center);
                    this._viewReport._addColumn(this._objReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._out, _g.d.ic_resource._table + "." + _g.d.ic_resource._out, _g.d.ic_resource._out, SMLReport._report._cellAlign.Center);
                    this._viewReport._addColumn(this._objReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._amount, _g.d.ic_resource._table + "." + _g.d.ic_resource._amount, _g.d.ic_resource._amount, SMLReport._report._cellAlign.Center);
                    // Column ข้อมูล
                    this._objReport2 = this._viewReport._addObject(this._viewReport._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    this._viewReport._addColumn(this._objReport2, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, null, null, null, SMLReport._report._cellAlign.Left);
                    this._viewReport._addColumn(this._objReport2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._doc_date, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date, _g.d.ic_resource._doc_date, SMLReport._report._cellAlign.Left);
                    this._viewReport._addColumn(this._objReport2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._trans_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_name, _g.d.ic_resource._trans_name, SMLReport._report._cellAlign.Left);
                    this._viewReport._addColumn(this._objReport2, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._doc_no, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_no, _g.d.ic_resource._doc_no, SMLReport._report._cellAlign.Left);
                    this._viewReport._addColumn(this._objReport2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._ic_unit_code, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_unit_code, _g.d.ic_resource._ic_unit_code, SMLReport._report._cellAlign.Left);
                    this._viewReport._addColumn(this._objReport2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._qty_in, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_in, _g.d.ic_resource._qty_in, SMLReport._report._cellAlign.Right);
                    this._viewReport._addColumn(this._objReport2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._qty_out, _g.d.ic_resource._table + "." + _g.d.ic_resource._qty_out, _g.d.ic_resource._qty_out, SMLReport._report._cellAlign.Right);
                    this._viewReport._addColumn(this._objReport2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._balance_qty, _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, _g.d.ic_resource._balance_qty, SMLReport._report._cellAlign.Right);
                    return true;
                }
            return false;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._buildReport();
        }

        void _buildReport()
        {
            this._ds = null;
            this._viewReport._buildReport(SMLReport._report._reportType.Standard);
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._conditionShow();
        }

        private void _conditionShow()
        {
            this._condition.ShowDialog();
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
