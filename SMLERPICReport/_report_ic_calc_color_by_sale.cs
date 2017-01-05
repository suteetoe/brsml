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
    public partial class _report_ic_calc_color_by_sale : UserControl
    {
        SMLReport._report._objectListType _objReport;
        DataSet _ds;
        //string[] _itemList = { "BLK", "FFR", "GRN", "HER", "HEY", "LFY", "MAG", "OXR", "TBL", "WHT", "YOX" };
        ArrayList _itemList;
        SMLERPReportTool._conditionScreen _condition;
        ArrayList _width = new ArrayList();
        ArrayList _column = new ArrayList();
        private SMLReport._report._view _viewReport = new SMLReport._report._view();
        private string _screenName;
        private SMLERPReportTool._reportEnum _mode;

        public _report_ic_calc_color_by_sale(string screenName, SMLERPReportTool._reportEnum mode)
        {
            this._screenName = screenName;
            this._mode = mode;
            this._viewReport.Dock = DockStyle.Fill;
            this.Controls.Add(this._viewReport);
            //
            this._condition = new SMLERPReportTool._conditionScreen(this._mode, screenName);
            this._condition._processButton.Click += new EventHandler(_bt_process_Click);
            //
            this._viewReport._pageSetupDialog.PageSettings.Landscape = true;
            this._viewReport._buttonClose.Click += new EventHandler(_buttonClose_Click);
            this._viewReport._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._viewReport._getObject += new SMLReport._report.GetObjectEventHandler(_viewReport__getObject);
            this._viewReport._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_viewReport__getDataObject);
            this._viewReport._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_viewReport__loadDataByThread);
            this._viewReport._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            //this._myicProcess.__queryStreamEvent += new SMLProcess.QueryStreamEventHandler(_myicProcess___queryStreamEvent);
            this._condition.ShowDialog();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._condition.ShowDialog();
        }

        string _findResource(string tableName, string fieldName)
        {
            string __fieldName = string.Concat(tableName, ".", fieldName);
            return MyLib._myResource._findResource(__fieldName, __fieldName)._str;
        }

        void _config()
        {
            int[] __width = { 11, 11, 12, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
            string[] __column = { 
                                        _findResource(_g.d.resource_report_ic_column._table , _g.d.resource_report_ic_column._doc_date),
                                        _findResource(_g.d.resource_report_ic_column._table , _g.d.resource_report_ic_column._doc_no),
                                        _findResource(_g.d.resource_report_ic_column._table , _g.d.resource_report_ic_column._item_code)
                                    };
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select distinct " + _g.d.ic_inventory_set_detail._ic_code + " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._line_number + " > 2 order by " + _g.d.ic_inventory_set_detail._ic_code;
            DataTable __getItemCode = __myFrameWork._queryShort(__query).Tables[0];
            // ความกว้าง ได้จากการคำนวณบางส่วน
            this._width.Add(10); // DocDate
            this._width.Add(10); // DocNo
            this._width.Add(80 - (__getItemCode.Rows.Count * 6)); // ItemCode
            this._itemList = new ArrayList();
            for (int __row = 0; __row < __getItemCode.Rows.Count; __row++)
            {
                this._width.Add(6);
                this._itemList.Add(__getItemCode.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString().ToUpper());
            }
            for (int __loop = 0; __loop < __width.Length; __loop++)
            {
                this._column.Add((__loop < __column.Length) ? __column[__loop] : this._itemList[__loop - __column.Length]);
            }
        }

        void _viewReport__loadDataByThread()
        {
            if (this._ds == null)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append("select " + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._qty + " from " + _g.d.ic_trans_detail._table);
                __query.Append(" where " + _g.d.ic_trans_detail._item_type + "=5 and " + _g.d.ic_trans_detail._last_status + "=0");
                __query.Append(" and " + _g.d.ic_trans_detail._doc_date + " between \'" + MyLib._myGlobal._convertDateToQuery(this._condition._screen._getDataStr(_g.d.resource_report._from_date)) + "\' and \'" + MyLib._myGlobal._convertDateToQuery(this._condition._screen._getDataStr(_g.d.resource_report._to_date)) + "\'");
                __query.Append(" order by " + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._line_number);
                this._ds = __myFrameWork._queryShort(__query.ToString());
                this._viewReport._loadDataByThreadSuccess = true;
            }
        }

        ArrayList _valueTotal;
        ArrayList _valueTotalByDate;

        void _viewReport__getDataObject()
        {
            this._valueTotal = new ArrayList();
            this._valueTotalByDate = new ArrayList();
            for (int __loop = 0; __loop < this._itemList.Count; __loop++)
            {
                this._valueTotal.Add((decimal)0.0M);
                this._valueTotalByDate.Add((decimal)0.0M);
            }
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)this._objReport._columnList[0];
            Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
            DataRow[] __dr = this._ds.Tables[0].Select();
            string __lastDocDate = "";
            string __lastDocNo = "";
            for (int __row = 0; __row < __dr.Length; __row++)
            {
                string __itemCode = __dr[__row][_g.d.ic_trans_detail._item_code].ToString();
                decimal __itemQty = MyLib._myGlobal._decimalPhase(__dr[__row][_g.d.ic_trans_detail._qty].ToString());
                string __docDate = MyLib._myGlobal._convertDateToString(MyLib._myGlobal._convertDate(__dr[__row][_g.d.ic_trans_detail._doc_date].ToString()), false);
                string __docDateDisplay = __docDate;
                if (__docDate.Equals(__lastDocDate))
                {
                    __docDateDisplay = "";
                }
                else
                {
                    _printTotal(MyLib._myGlobal._resource("รวมวันที่") + " " + __lastDocDate, this._valueTotalByDate);
                }
                SMLReport._report._objectListType __dataObject = this._viewReport._addObject(this._viewReport._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._viewReport._createEmtryColumn(this._objReport, __dataObject);
                string __docNo = __dr[__row][_g.d.ic_trans_detail._doc_no].ToString();
                string __docNoDisplay = __docNo;
                if (__docNo.Equals(__lastDocNo))
                {
                    __docNoDisplay = "";
                }
                this._viewReport._addDataColumn(this._objReport, __dataObject, 0, __docDateDisplay, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport, __dataObject, 1, __docNoDisplay, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport, __dataObject, 2, __itemCode, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                __lastDocDate = __docDate;
                __lastDocNo = __docNo;
                // ดึงสูตร
                ArrayList __value = new ArrayList();
                for (int __loop = 0; __loop < this._itemList.Count; __loop++)
                {
                    __value.Add((decimal)0.0M);
                }
                string __queryFormula = "select " + _g.d.ic_inventory_set_detail._ic_code + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code +
                    " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + __itemCode + "\' order by " + _g.d.ic_inventory_set_detail._line_number;
                DataTable __getFormula = __myFrameWork._queryShort(__queryFormula).Tables[0];
                for (int __rowFormula = 2; __rowFormula < __getFormula.Rows.Count; __rowFormula++)
                {
                    string __formulaItemCode = __getFormula.Rows[__rowFormula][_g.d.ic_inventory_set_detail._ic_code].ToString();
                    string __itemUnitCode = __getFormula.Rows[__rowFormula][_g.d.ic_inventory_set_detail._unit_code].ToString();
                    // ปัดจำนวนขึ้นก่อน
                    decimal __qtyFormula = MyLib._myGlobal._round(__itemQty * MyLib._myGlobal._decimalPhase(__getFormula.Rows[__rowFormula][_g.d.ic_inventory_set_detail._qty].ToString()), _g.g._companyProfile._item_qty_decimal);
                    bool __found = false;
                    for (int __loop = 0; __found == false && __loop < this._itemList.Count; __loop++)
                    {
                        if (this._itemList[__loop].ToString().ToLower().Equals(__formulaItemCode.ToLower()))
                        {
                            __value[__loop] = (decimal)((decimal)__value[__loop]) + __qtyFormula;
                            this._valueTotal[__loop] = (decimal)((decimal)this._valueTotal[__loop]) + __qtyFormula;
                            this._valueTotalByDate[__loop] = (decimal)((decimal)this._valueTotalByDate[__loop]) + __qtyFormula;
                            __found = true;
                        }
                    }
                }
                for (int __loop = 0; __loop < this._itemList.Count; __loop++)
                {
                    this._viewReport._addDataColumn(this._objReport, __dataObject, 3 + __loop, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, (decimal)__value[__loop]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                }
            }
            // Total
            _printTotal(MyLib._myGlobal._resource("รวมวันที่") + " " + __lastDocDate, this._valueTotalByDate);
            _printTotal(MyLib._myGlobal._resource("รวมทั้งสิ้น"), this._valueTotal);
        }

        private void _printTotal(string title, ArrayList value)
        {
            Boolean __foundTotal = false;
            for (int __loop = 0; __loop < this._itemList.Count; __loop++)
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
                this._viewReport._createEmtryColumn(this._objReport, __dataObject);
                this._viewReport._addDataColumn(this._objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._viewReport._addDataColumn(this._objReport, __dataObject, 2, title, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                for (int __loop = 0; __loop < this._itemList.Count; __loop++)
                {
                    this._viewReport._addDataColumn(this._objReport, __dataObject, 3 + __loop, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, (decimal)value[__loop]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                    value[__loop] = (decimal)0.0M;
                }
            }
        }

        bool _viewReport__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            switch (type)
            {
                case SMLReport._report._objectType.Header:
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
                    this._viewReport._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title    : " + this._screenName, SMLReport._report._cellAlign.Left, this._viewReport._fontHeader2);
                    this._viewReport._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._viewReport._fontHeader2);
                    this._viewReport._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._viewReport._fontHeader2);
                    this._viewReport._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._viewReport._fontHeader2);
                    this._viewReport.__excelFlieName = this._screenName;
                    //this._view_ic._excelFileName = "รายงานยอดการชำระเงิน";//
                    //this._view_ic._maxColumn = 9;
                    return true;
                case SMLReport._report._objectType.Detail:
                    {
                        this._objReport = this._viewReport._addObject(this._viewReport._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        SMLReport._report._objectListType __dataObject = this._viewReport._addObject(this._viewReport._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._viewReport._createEmtryColumn(this._objReport, __dataObject);
                        for (int __i = 0; __i < this._column.Count; __i++)
                        {
                            this._viewReport._addColumn(this._objReport, MyLib._myGlobal._intPhase(this._width[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column[__i].ToString(), "", (__i < 3) ? SMLReport._report._cellAlign.Left : SMLReport._report._cellAlign.Right);
                        }
                    }
                    return true;
            }
            return false;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._process();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this._ds = null;
            this._condition.Close();
            this._process();
        }

        void _process()
        {
            if (this._ds != null)
            {
                this._width.Clear();
                this._column.Clear();
            }

            this._config();
            //
            this._viewReport._buildReport(SMLReport._report._reportType.Standard);
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
