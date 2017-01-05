using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.Diagnostics;
using MyLib;


namespace SMLERPICReport
{
    public partial class _report_ic : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLProcess._icProcess _myicProcess = new SMLProcess._icProcess();
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;
        SMLReport._report._objectListType _objReport3;
        SMLReport._report._objectListType _objReportNone;
        DataSet _getTransHead;
        DataSet _getTransDetail;
        DataSet _ds;
        DataSet _ds_2;
        DataSet _ds_3;
        DataTable _dt_head = new DataTable();
        DataTable _dt_detail_1 = new DataTable();
        DataTable _dt_detail_2 = new DataTable();

        public string _report_name = "";
        string _report_description = "";
        int _level = 0;
        ArrayList _width = new ArrayList();
        ArrayList _width_2 = new ArrayList();
        ArrayList _width_3 = new ArrayList();
        ArrayList _column = new ArrayList();
        ArrayList _column_2 = new ArrayList();
        ArrayList _column_3 = new ArrayList();
        ArrayList _contionStr = new ArrayList();
        private StringBuilder __query_head = new StringBuilder();
        private StringBuilder __query_detail_1 = new StringBuilder();
        private StringBuilder __query_detail_2 = new StringBuilder();
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string _formatNumber_no_decimal = MyLib._myGlobal._getFormatNumber("m00");

        private SMLERPReportTool._reportEnum __icTypeTemp;
        //
        public SMLERPReportTool._reportEnum _icType
        {
            set
            {
                this.__icTypeTemp = value;
                this.Invalidate();
            }
            get
            {
                return this.__icTypeTemp;
            }
        }

        SMLERPReportTool._conditionScreen _condition;
        string _item_code = "item_code";
        string _item_name = "item_name";
        string _item_name_2 = "item_name_2";
        string _item_remark = "item_remark";
        string _tax_type = "tax_type";
        string _cost_type = "cost_type";
        string _unit_standard = "unit_standard";
        string _unit_detail = "unit_detail";
        string _ap_code = "ap_code";
        string _ap_name = "ap_name";
        string _item_serial_number = "serial_number";
        string _item_description = "description";
        string _item_body_no = "body_no";

        string _item_license = "license";

        public _report_ic()
        {
            InitializeComponent();
            _view1._pageSetupDialog.PageSettings.Landscape = true;
            this._view1._buttonExample.Enabled = false;
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            //this._view1._loadData += new SMLReport._report.LoadDataEventHandler(_view_ic__loadData);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view_ic__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view_ic__getDataObject);
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._myicProcess.__queryStreamEvent += new SMLProcess.QueryStreamEventHandler(_myicProcess___queryStreamEvent);

            //this._showCondition();
        }

        void _myicProcess___queryStreamEvent(string lastMessage, int persentProcess)
        {
            this._view1._processMessage = lastMessage;
            this._view1._progessBarValue = persentProcess;
        }

        string _convert_flag_to_stauts_now(string __trans_flag)
        {
            string __result = "";
            switch (__trans_flag)
            {
                case "56": return "เบิกใช้งาน";

                case "44": return "ขายแล้ว";
                case "46": return "ขายแล้ว";
                case "50": return "ขายแล้ว";

                case "45": return "ยกเลิก";
                case "47": return "ยกเลิก";

                case "18": return "ในสต๊อก";
                case "48": return "ในสต๊อก";
                case "54": return "ในสต๊อก";
                case "58": return "ในสต๊อก";
                case "60": return "ในสต๊อก";
            }
            //return "";
            return __result;
        }

        string _convert_flag_to_description(string trans_flag)
        {
            string __result = "";
            switch (trans_flag)
            {
                case "12":
                    __result = MyLib._myGlobal._resource("บันทึกซื้อสินค้าบริการ");
                    break;
                case "44":
                    __result = MyLib._myGlobal._resource("บันทึกขายสินค้าบริการ");
                    break;
            }
            return __result;
        }


        string _cal_diff_from_count(string __qty_from_data, string __count_qty_from_data)
        {
            string __result = "";
            double __qty = 0;
            double __count_qty = 0;
            try
            {
                if (__qty_from_data != "" || __qty_from_data.Length > 0) __qty = double.Parse(__qty_from_data);
                if (__count_qty_from_data != "" || __count_qty_from_data.Length > 0) __count_qty = double.Parse(__count_qty_from_data);
                __result = (__qty - __count_qty).ToString("##.##");
            }
            catch (Exception __e)
            {
                string __error = __e.Message;
            }
            return __result;
        }

        void _view_ic__getDataObject()
        {
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)_objReport._columnList[0];
            DataRow[] __dr = null;
            DataRow[] __drGroup;
            float __ss = 8.0F;

            Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
            Font __total_font = new Font(__getColumn._fontData, FontStyle.Bold);

            if (_dt_head == null) _dt_head = new DataTable();
            if ((_dt_head.Rows.Count > 0) || (_dt_head != null)) __dr = _dt_head.Select("");


            if ((__dr.Length == 0)) // ในกรณีที่ไม่มีข้อมูล
            {
                SMLReport._report._objectListType __dataObjectHead = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(_objReport, __dataObjectHead);
                this._view1._addDataColumn(_objReport, __dataObjectHead, 0, "รวม  0  รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
            }

            int _no = 1;
            double __amount = 0.00;
            double __interest = 0.00;
            double __fine = 0.00;
            double __sumtotal = 0.00;
            //this._view1._reportProgressBar.Value = 0;
            int _rowprogreesbar = 0;
            if (__dr.Length > 0)
            {
                _rowprogreesbar = (100 / __dr.Length);
            }//
            //int loopprogressbar = 0;

            try
            {

                if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดสินค้า)
                {
                    // รายละเอียดสินค้า do it


                    string __item_code = "";
                    int __no = 0;

                    __dr = _dt_head.Select("");
                    for (int __loopTop = 0; __loopTop < __dr.Length; __loopTop++)
                    {
                        string __item_code2 = __dr[__loopTop]["item_code"].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            //ชั้นแรก
                            __item_code = __item_code2;

                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__loopTop]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __item_name = "";
                            __item_name = __dr[__loopTop]["item_name"].ToString();
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __item_name, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__loopTop]["unit_standard_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, _convert_tax_type(__dr[__loopTop][_g.d.ic_inventory._tax_type].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__loopTop]["item_pattern"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, _convert_item_type(__dr[__loopTop]["item_type"].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, _convert_cost_type(__dr[__loopTop]["cost_type"].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                            //  ชั้นที่ 2

                            /*SMLReport._report._objectListType __dataObject3;
                            DataRow[] __dr_unit; string __unit = ""; DataRow[] __dr_wh_shelf;

                            string __chk_unit_type = __dr[__loopTop]["unit_type"].ToString();
                            __dr_wh_shelf = _dt_detail_2.Select("ic_code = '" + __item_code + "'");
                            for (int __k = 0; __k < __dr_wh_shelf.Length; __k++)
                            {
                                __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject3);
                                this._view1._addDataColumn(_objReport2, __dataObject3, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                //this._view1._addDataColumn(_objReport2, __dataObject3, 1, __unit, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                this._view1._addDataColumn(_objReport2, __dataObject3, 1, __dr_wh_shelf[__k]["wh_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject3, 2, __dr_wh_shelf[__k]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            }
                             * */
                            __no++;
                        }

                        if (__loopTop == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject4 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(this._objReport2, __dataObject4);

                            this._view1._addDataColumn(_objReport2, __dataObject4, 0, "รวม  " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 1, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);

                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดบาร์โค๊ด)
                {
                    string __item_code = "";
                    int __no_main = 0;

                    __dr = _dt_head.Select("");
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (__item_code != __item_code2)
                        {
                            __no_main++;
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["unit_standard"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __tax_type = "";
                            if (__dr[__i]["tax_type"].ToString() != "") __tax_type = _convert_tax_type(__dr[__i]["tax_type"].ToString());
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __tax_type, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __item_pattern = "";
                            if (__dr[__i]["item_pattern"].ToString() != "") __item_pattern = _convert_tax_type(__dr[__i]["item_pattern"].ToString());
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["item_pattern"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __item_type = "";
                            if (__dr[__i]["item_type"].ToString() != "") __item_type = _convert_item_type(__dr[__i]["item_type"].ToString());
                            this._view1._addDataColumn(_objReport, __dataObject, 5, __item_type, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __cost_type = "";
                            if (__dr[__i]["cost_type"].ToString() != "") __cost_type = _convert_cost_type(__dr[__i]["cost_type"].ToString());
                            this._view1._addDataColumn(_objReport, __dataObject, 6, __cost_type, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = _dt_head.Select("item_code = '" + __item_code + "'");
                            string __barcode = "";
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __barcode2 = __dr2[__j]["barcode"].ToString();
                                if (__barcode != __barcode2)
                                {
                                    __barcode = __barcode2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["barcode"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                }

                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);

                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __no_main) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสูตรสีผสม || _icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสูตรสินค้าชุด)
                {
                    //  รายงานสูตรสีผสม  สินค้าชุด
                    __dr = _dt_head.Select("");
                    double __qty_all = 0;
                    double __price_all = 0;
                    double __amount_all = 0;
                    string __item_code = ""; int __no_main = 0;
                    string __item_code_for_number = "";
                    // หาจำนวน item_code
                    int __number = 0;
                    for (int __a = 0; __a < __dr.Length; __a++)
                    {
                        string __item_code_for_number2 = __dr[__a]["item_code"].ToString();
                        if (__item_code_for_number != __item_code_for_number2)
                        {
                            __number++;
                        }
                    }
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (__item_code != __item_code2)
                        {
                            __no_main++;
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["unit_standard"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["item_pattern"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __item_type = "";
                            if (__dr[__i]["item_type"].ToString() != "") __item_type = _convert_item_type(__dr[__i]["item_type"].ToString());
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __item_type, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __tax_type = "";
                            if (__dr[__i]["tax_type"].ToString() != "") __tax_type = _convert_tax_type(__dr[__i]["tax_type"].ToString());

                            this._view1._addDataColumn(_objReport, __dataObject, 5, __tax_type, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __cost_type = "";
                            if (__dr[__i]["cost_type"].ToString() != "") __cost_type = _convert_cost_type(__dr[__i]["cost_type"].ToString());
                            this._view1._addDataColumn(_objReport, __dataObject, 6, __cost_type, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //DataRow[] __dr2 = this._dt_detail_1.Select("ic_set_code = '" + __item_code + "'");
                            DataRow[] __dr2 = this._dt_head.Select("item_code = '" + __item_code + "'"); // query เดียว
                            string __ic_code = "";
                            double __qty_in = 0;
                            double __price_in = 0;
                            double __amount_in = 0;
                            int __no_in = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __ic_code2 = __dr2[__j]["ic_code"].ToString();
                                if (__ic_code != __ic_code2)
                                {
                                    __no_in++;
                                    __ic_code = __ic_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[__j]["ic_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr[__j]["ic_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                    string __qty_from_color_mixing = "";
                                    __qty_from_color_mixing = __dr[__j]["qty"].ToString();

                                    __qty_from_color_mixing = MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __j, _g.d.ic_resource._qty).ToString();


                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __j, _g.d.ic_resource._qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr, __j, "price"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr, __j, "sum_amount"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    if (MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __j, _g.d.ic_resource._qty) != "")
                                    {
                                        __qty_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __j, _g.d.ic_resource._qty));
                                        __qty_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __j, _g.d.ic_resource._qty));
                                    }
                                    if (MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr, __j, "price") != "")
                                    {
                                        __price_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr, __j, "price"));
                                        __price_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr, __j, "price"));
                                    }
                                    if (MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr, __j, "sum_amount") != "")
                                    {
                                        __amount_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr, __j, "sum_amount"));
                                        __amount_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr, __j, "sum_amount"));
                                    }

                                }
                                if (__j == (__dr2.Length - 1))
                                {
                                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject);

                                    this._view1._addDataColumn(_objReport2, __dataObject, 0, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no_in) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject, 4, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject, 5, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    //this._view1._addDataColumn(_objReport2, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                }

                            }
                        }
                        if (__i == (__number - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);

                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __no_main), __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, "รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                        }
                    }

                }
                else if ((_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานราคาขายสินค้า) || (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานราคาซื้อสินค้า))
                {

                    int __no_main = 0;
                    string __item_code = "";
                    __dr = _dt_head.Select("");
                    double __split_vat_all = 0;
                    double __include_vat_all = 0;
                    double __from_qty_all = 0;
                    double __to_qty_all = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (__item_code != __item_code2)
                        {
                            __no_main++;
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name_th"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["item_name_eng"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["unitstandard"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["pattern"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, _convert_item_type(__dr[__i]["item_type"].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = _dt_head.Select("item_code = '" + __item_code + "'");
                            double __total_sale_price1 = 0; double __total_sale_price2 = 0;
                            double __price1 = 0;// double price2 = 0;

                            double __split_vat_in = 0;
                            double __include_vat_in = 0;
                            double __from_qty_in = 0;
                            double __to_qty_in = 0;
                            int __no_detail = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no_detail++;
                                double __price11 = 0; ;
                                if (__dr2[__j]["sale_price1"].ToString() != "") __price11 = double.Parse(__dr2[__j]["sale_price1"].ToString());


                                double __sale_price1 = 0; double __sale_price2 = 0;
                                if (__dr2[__j]["sale_price1"].ToString() != "")
                                {
                                    __sale_price1 = double.Parse(__dr2[__j]["sale_price1"].ToString());
                                    __total_sale_price1 += double.Parse(__dr2[__j]["sale_price1"].ToString());
                                }
                                if (__dr2[__j]["sale_price2"].ToString() != "")
                                {
                                    __sale_price2 = double.Parse(__dr2[__j]["sale_price2"].ToString());
                                    __total_sale_price2 += double.Parse(__dr2[__j]["sale_price2"].ToString());
                                }

                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price1"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price2"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                string __from_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["from_date"].ToString()).ToShortDateString();
                                string __to_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["to_date"].ToString()).ToShortDateString();

                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, __from_date, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, __to_date, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "from_qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "to_qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                if (MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price1") != "")
                                {
                                    __split_vat_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price1"));
                                    __split_vat_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price1"));
                                }
                                if (MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price2") != "")
                                {
                                    __include_vat_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price2"));
                                    __include_vat_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sale_price2"));
                                }
                                if (MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "from_qty") != "")
                                {
                                    __from_qty_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "from_qty"));
                                    __from_qty_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "from_qty"));
                                }
                                if (MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "to_qty") != "")
                                {
                                    __to_qty_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "to_qty"));
                                    __to_qty_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "to_qty"));
                                }


                                if (__j == (__dr2.Length - 1))
                                {
                                    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no_detail) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __split_vat_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __include_vat_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __from_qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __to_qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                }
                            }
                        }
                        //}
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);
                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __no_main) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __split_vat_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __include_vat_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __from_qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __to_qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Item_Giveaway)
                {
                    //รายงานของแถม
                    string __condition_code = "";
                    int __no_main = 0;
                    double __qty_all = 0;
                    __dr = _dt_head.Select("");
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __condition_code2 = __dr[__i]["condition_code"].ToString();
                        if (__condition_code != __condition_code2)
                        {
                            __condition_code = __condition_code2;
                            __no_main++;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __start = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["start_date"].ToString()).ToShortDateString();
                            string __stop = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["stop_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["condition_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["condition_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __start, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __stop, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("condition_code = '" + __condition_code + "'");
                            int __no_detail = 0;
                            double __qty_in = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no_detail++;
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                string __unit = __dr2[__j]["unit"].ToString(); double __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                __qty_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));
                                __qty_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));

                                if (__j == (__dr2.Length - 1))
                                {
                                    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no_detail + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                }
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);
                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __no_main) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);

                        }
                    }
                }
                //else if (_icType == _g.g._icreportEnum.Item_by_supplier)
                //{
                //    // รายละเอียดสินค้าตามเจ้าหนี้  ไม่มีในรายงาน
                //    string __ap_code = "";
                //    string __xunit_use = "";
                //    string __xitemCode = "";
                //    string __xapCode = "";
                //    string _xapCode = "";
                //    bool __test = false;
                //    for (int __i = 0; __i < __dr.Length; __i++)
                //    {

                //        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                //        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                //        this._view1._createEmtryColumn(_objReport, __dataObject);
                //        __ap_code = __dr[__i][_ap_code].ToString();

                //        if (_xapCode.Equals(__ap_code))
                //        {
                //            _xapCode = __ap_code;
                //            __test = false;
                //        }
                //        else
                //        {
                //            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i][_ap_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i][_ap_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //            _xapCode = __ap_code;
                //            __test = true;
                //        }
                //        if (__test == true)
                //        {
                //            string _xitemCode2 = "";
                //            bool __testUnit = false;
                //            for (int __j = 0; __j < __dr.Length; __j++)
                //            {
                //                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                //                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                //                __xitemCode = __dr[__j][_item_code].ToString();
                //                __xapCode = __dr[__j][_ap_code].ToString();
                //                if (_xapCode.Equals(__xapCode))
                //                {
                //                    if (!_xitemCode2.Equals(__xitemCode))
                //                    {
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[__j][_item_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr[__j][_item_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr[__j][_unit_standard].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 6, _check_costType(MyLib._myGlobal._intPhase(__dr[__j][_cost_type].ToString())), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        this._view1._addDataColumn(_objReport2, __dataObject2, 8, _check_taxType(MyLib._myGlobal._intPhase(__dr[__j][_tax_type].ToString())), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                        _xitemCode2 = __xitemCode;
                //                        __testUnit = true;
                //                    }
                //                    else
                //                    {
                //                        __testUnit = false;
                //                    }

                //                }

                //                string _xunitCode2 = "";
                //                string __ap3 = "";
                //                string __item3 = "";
                //                string __unit1 = "";

                //                if (__testUnit == true)
                //                {

                //                    for (int __k = 0; __k < __dr.Length; __k++)
                //                    {
                //                        SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                //                        this._view1._createEmtryColumn(_objReport3, __dataObject3);
                //                        __item3 = __dr[__k][_item_code].ToString();
                //                        __ap3 = __dr[__k][_ap_code].ToString();
                //                        __unit1 = __dr[__k][_unit_detail].ToString();

                //                        if (__ap3.Equals(__xapCode))
                //                        {
                //                            if (__item3.Equals(__xitemCode))
                //                            {
                //                                if (__unit1.Equals(_xunitCode2))
                //                                {

                //                                }
                //                                else
                //                                {
                //                                    this._view1._addDataColumn(_objReport3, __dataObject3, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                                    this._view1._addDataColumn(_objReport3, __dataObject3, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                                    this._view1._addDataColumn(_objReport3, __dataObject3, 2, __dr[__k][_unit_detail].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                //                                    this._view1._addDataColumn(_objReport3, __dataObject3, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                //                                    this._view1._addDataColumn(_objReport3, __dataObject3, 4, __dr[__k][_g.d.ic_unit_use._stand_value].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                //                                    this._view1._addDataColumn(_objReport3, __dataObject3, 5, __dr[__k][_g.d.ic_unit_use._divide_value].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                //                                    this._view1._addDataColumn(_objReport3, __dataObject3, 6, __dr[__k][_g.d.ic_unit_use._ratio].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                //                                    _xunitCode2 = __xunit_use;
                //                                }
                //                            }
                //                        }

                //                    }
                //                }
                //            }
                //        }



                //    }

                //}
                else if (_icType == SMLERPReportTool._reportEnum.Item_by_serial)
                {
                    // รายละเอียดสินค้าแบบมี Serial
                    string __item_code = "";
                    __dr = _dt_head.Select();
                    int __no = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i][_item_code].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i][_g.d.ic_trans_detail._item_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // item code (รหัสสินค้า)
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i][_g.d.ic_trans_detail._item_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // item name (ชื่อสินค้า)

                            DataRow[] __dr2 = _dt_head.Select("item_code = '" + __item_code + "'");
                            string __serial_no = "";
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __serial_no2 = __dr2[__j][_g.d.ic_serial._serial_number].ToString();
                                if (!__serial_no.Equals(__serial_no2))
                                {
                                    __serial_no = __serial_no2;
                                    __no++;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j][_g.d.ic_serial._serial_number].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number); // serial number (รหัสทะเบียนกลุ่ม)
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j][_g.d.ic_serial._description].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // description (รายละเอียดเฉพาะ)
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, _convert_item_status(__dr2[__j][_g.d.ic_inventory._status].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// status (สถานะสินค้า)
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, _convert_flag_to_stauts_now(__dr2[__j][_g.d.ic_trans_detail._trans_flag].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// trans flag (สถานะปัจจุบัน)
                                }
                            }
                        }

                        if (__i == __dr.Length - 1)
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Serial_number)
                {
                    // รายงานเคลื่อนไหว Serial Number  
                    string __serial_no = "";
                    int __no = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __serial_no2 = __dr[__i]["serial_number"].ToString();
                        if (!__serial_no.Equals(__serial_no2))
                        {
                            __serial_no = __serial_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            
                            //string __due_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["due_date"].ToString()).ToShortDateString();

                            //this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["serial_number"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["body_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["item_license"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 6, __due_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 7, __dr[__i]["customer_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i][_g.d.ic_serial._serial_number].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i][_g.d.ic_serial._ic_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i][_g.d.ic_resource._item_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i][_g.d.ic_serial._status].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i][_g.d.resource_report._date_import].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);

                            __no++;

                            DataRow[] __dr2 = _dt_head.Select("serial_number = '" + __serial_no + "'");
                            int __no_per_serial = 0;
                            string __doc_no = "";
                            /*
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __doc_no2 = __dr2[__j]["doc_no"].ToString();
                                if (!__doc_no.Equals(__doc_no2))
                                {
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__j]["doc_date"].ToString()).ToShortDateString();
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["description"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    __no_per_serial++;
                                }
                                //if (__j == __dr2.Length - 1)
                                //{
                                //    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "รวม " +string.Format(_formatNumber_no_decimal, __no_per_serial) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //}

                            }
                             */
                        }
                        if (__i == __dr.Length - 1)
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "รวมรายการ/หน้า " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        }

                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ)
                {  //  รายงานโอนสินค้าและวัตถุดิบ
                    string __doc_date = "";
                    int __total_in_page = 0;
                    double __total_qty_in_page = 0;
                    double __qty_all = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __chk_trans_flag = __dr[__i]["trans_flag"].ToString();
                        if ((__chk_trans_flag == "71") || (__chk_trans_flag == "73"))
                        {
                            __newFont = new Font(__getColumn._fontData, FontStyle.Strikeout);
                        }
                        string __doc_date_2 = __dr[__i]["doc_date"].ToString();
                        if (__doc_date != __doc_date_2)
                        {
                            __doc_date = __doc_date_2;
                            //SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            //this._view1._createEmtryColumn(_objReport, __dataObject);
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_3 = MyLib._myGlobal._convertDateFromQuery(__dr[__i][_g.d.ic_trans_detail._doc_date].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_3, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i][_g.d.ic_trans_detail._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i][_g.d.ic_trans_detail._doc_ref].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i][_g.d.ic_trans_detail._remark].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = this._dt_head.Select(_g.d.ic_trans_detail._doc_date + " = '" + __doc_date_2 + "'");
                            string __item_code = "";
                            int __no = 0;
                            double __total_qty = 0;

                            // เก็บค่าคลังเข้า  คลังออก  ชั้นวางเข้า  ชั้นวางออก
                            string __wh_export = ""; string __wh_import = ""; string __shelf_import = ""; string __shelf_export = "";
                            for (int __k = 0; __k < __dr2.Length; __k++)
                            {
                                if ((__dr2[__k]["wh_code_export"].ToString() != "") && (__dr2[__k]["wh_name_export"].ToString() != ""))
                                {
                                    __wh_export = __dr2[__k]["wh_code_export"].ToString() + "~" + __dr2[__k]["wh_name_export"].ToString();
                                }

                                if ((__dr2[__k]["wh_code_import"].ToString() != "") && (__dr2[__k]["wh_name_import"].ToString() != ""))
                                {
                                    __wh_import = __dr2[__k]["wh_code_import"].ToString() + "~" + __dr2[__k]["wh_name_import"].ToString();
                                }


                                if ((__dr2[__k]["shelf_code_export"].ToString() != "") && (__dr2[__k]["shelf_name_export"].ToString() != ""))
                                {
                                    __shelf_export = __dr2[__k]["shelf_code_export"].ToString() + "~" + __dr2[__k]["shelf_name_export"].ToString();
                                }

                                if ((__dr2[__k]["shelf_code_import"].ToString() != "") && (__dr2[__k]["shelf_name_import"].ToString() != ""))
                                {
                                    __shelf_import = __dr2[__k]["shelf_code_import"].ToString() + "~" + __dr2[__k]["shelf_name_import"].ToString();
                                }
                            }

                            double __qty_in = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __item_code2 = __dr2[__j][_g.d.ic_trans_detail._item_code].ToString();
                                if (__item_code != __item_code2)
                                {
                                    __item_code = __item_code2;
                                    __no++;
                                    __total_in_page++;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j][_g.d.ic_trans_detail._item_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  รหัสสินค้า
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j][_g.d.ic_trans_detail._item_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  ชื่อสินค้า
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __wh_export, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //   จากคลัง
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __wh_import, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //  ถึงคลัง
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __shelf_export, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //   จากที่เก็บ
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __shelf_import, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //  ถึงที่เก็บ

                                    string __unit = __dr2[__j][_g.d.ic_trans_detail._unit_code].ToString() + "~" + __dr2[__j]["unit_name"].ToString();
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  หน่วยนับ
                                    double __qty = 0;
                                    if (__dr2[__j][_g.d.ic_trans_detail._qty].ToString() != "") __qty = double.Parse(__dr2[__j][_g.d.ic_trans_detail._qty].ToString());
                                    __qty_in += __qty;
                                    __qty_all += __qty;
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, _g.d.ic_trans_detail._qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  //  จำนวน


                                }
                                if (__j == (__dr2.Length - 1))
                                {
                                    SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject3);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 0, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 4, "", __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 6, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 7, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject3, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);


                                }
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject4 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject4);

                            this._view1._addDataColumn(_objReport2, __dataObject4, 0, "รวม " + string.Format(_formatNumber_no_decimal, __total_in_page) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 4, "", __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 6, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 7, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);

                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Cancel_Transfer_Item_and_material)
                {  //  รายงานยกเลิกโอนสินค้าและวัตถุดิบ
                    string __doc_date = "";
                    int __total_in_page = 0;
                    double __total_qty_in_page = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_date_2 = __dr[__i]["doc_date"].ToString();
                        if (__doc_date != __doc_date_2)
                        {
                            __doc_date = __doc_date_2;
                            //SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            //this._view1._createEmtryColumn(_objReport, __dataObject);
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_3 = MyLib._myGlobal._convertDateFromQuery(__dr[__i][_g.d.ic_trans_detail._doc_date].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_3, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i][_g.d.ic_trans_detail._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i][_g.d.ic_trans_detail._doc_ref].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = this._dt_head.Select(_g.d.ic_trans_detail._doc_date + " = '" + __doc_date_2 + "'");
                            string __item_code = "";

                            //  เก็บข้อมูลคลังเข้า  คลังออก  ชั้นวางเข้า  ชั้นวางออก
                            string __wh_export = ""; string __wh_import = ""; string __shelf_import = ""; string __shelf_export = "";

                            for (int __k = 0; __k < __dr2.Length; __k++)
                            {
                                if ((__dr2[__k]["wh_code_export"].ToString() != "") && (__dr2[__k]["wh_name_export"].ToString() != ""))
                                {
                                    __wh_export = __dr2[__k]["wh_code_export"].ToString() + "~" + __dr2[__k]["wh_name_export"].ToString();
                                }

                                if ((__dr2[__k]["wh_code_import"].ToString() != "") && (__dr2[__k]["wh_name_import"].ToString() != ""))
                                {
                                    __wh_import = __dr2[__k]["wh_code_import"].ToString() + "~" + __dr2[__k]["wh_name_import"].ToString();
                                }


                                if ((__dr2[__k]["shelf_code_export"].ToString() != "") && (__dr2[__k]["shelf_name_export"].ToString() != ""))
                                {
                                    __shelf_export = __dr2[__k]["shelf_code_export"].ToString() + "~" + __dr2[__k]["shelf_name_export"].ToString();
                                }

                                if ((__dr2[__k]["shelf_code_import"].ToString() != "") && (__dr2[__k]["shelf_name_import"].ToString() != ""))
                                {
                                    __shelf_import = __dr2[__k]["shelf_code_import"].ToString() + "~" + __dr2[__k]["shelf_name_import"].ToString();
                                }
                            }

                            int __no = 0;
                            double __total_qty = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __item_code2 = __dr2[__j][_g.d.ic_trans_detail._item_code].ToString();
                                if (__item_code != __item_code2)
                                {
                                    string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "จากคลัง", "เข้าคลัง", "จากที่เก็บ", "เข้าที่เก็บ", "หน่วยนับ", "จำนวน*" };
                                    __no++;
                                    __total_in_page++;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j][_g.d.ic_trans_detail._item_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  รหัสสินค้า
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j][_g.d.ic_trans_detail._item_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  ชื่อสินค้า
                                    string __unit = __dr2[__j][_g.d.ic_trans_detail._unit_code].ToString() + "~" + __dr2[__j]["unit_name"].ToString();
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __wh_export, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  หน่วยนับ
                                    double __qty = 0;
                                    if (__dr2[__j][_g.d.ic_trans_detail._qty].ToString() != "") __qty = double.Parse(__dr2[__j][_g.d.ic_trans_detail._qty].ToString());
                                    __total_qty += __qty;
                                    __total_qty_in_page += __qty;
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __wh_import, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  //  จำนวน



                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __shelf_export, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //   จากคลัง
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __shelf_import, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //  ถึงคลัง
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, __unit, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //   จากที่เก็บ
                                    //this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text); //  ถึงที่เก็บ
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, _g.d.ic_trans_detail._qty), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); //  ถึงที่เก็บ
                                }
                                //if (__j == (__dr2.Length - 1))
                                //{
                                //    SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject3);
                                //    this._view1._addDataColumn(_objReport2, __dataObject3, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject3, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject3, 2, "รวม " +string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject3, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject3, 4,string.Format(_formatNumber,  __total_qty), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);

                                //}
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject4 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject4);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 1, "รวม " + string.Format(_formatNumber_no_decimal, __total_in_page) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject4, 4, string.Format(_formatNumber, __total_qty_in_page), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }


                else if (_icType == SMLERPReportTool._reportEnum.Item_status)
                {
                    // รายงานสถานะสินค้า doit                                        
                    int __countRow = 0;
                    int __countTotal = 0;

                    __drGroup = _dt_head.Select("");



                    string __group_code = "";

                    for (int __loop = 0; __loop < __drGroup.Length; __loop++)
                    {
                        string __group_code2 = __drGroup[__loop]["group_main"].ToString();
                        if (__group_code != __group_code2)
                        {
                            __countTotal++;
                            __group_code = __group_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __group_code, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __drGroup[__loop]["group_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2;
                            __dr2 = _dt_head.Select(_g.d.ic_inventory._group_main + "=\'" + __group_code + "\'");
                            string __item_code = "";
                            for (int __row = 0; __row < __dr2.Length; __row++)
                            {
                                string __item_code2 = __dr2[__row][_g.d.ic_inventory._code].ToString();
                                string __firstInput = __dr2[__row]["first_input"].ToString();
                                string __lastInput = __dr2[__row]["last_input"].ToString();
                                string __lastOutput = __dr2[__row]["last_output"].ToString();
                                if (!__item_code.Equals(__item_code2))
                                {
                                    //  เรียงวันที่ตามรายงาน
                                    __item_code = __item_code2;
                                    string __first_input = "";
                                    string __last_input = "";
                                    string __last_output = "";
                                    double __getDiffDate = 0;
                                    DateTime __date_first_input = new DateTime();
                                    DateTime __date_last_input = new DateTime();
                                    DateTime __date_last_output = new DateTime();
                                    if (__firstInput.Length > 0)
                                    {
                                        __date_first_input = new DateTime();
                                        __date_first_input = DateTime.Parse(__firstInput, MyLib._myGlobal._cultureInfo());
                                        __first_input = MyLib._myGlobal._convertDateFromQuery(__date_first_input.ToString()).ToShortDateString();
                                    }
                                    if (__lastInput.Length > 0)
                                    {
                                        __date_last_input = DateTime.Parse(__lastInput, MyLib._myGlobal._cultureInfo());
                                        __last_input = MyLib._myGlobal._convertDateFromQuery(__date_last_input.ToString()).ToShortDateString();
                                    }
                                    if (__lastOutput.Length > 0)
                                    {
                                        __date_last_output = DateTime.Parse(__lastOutput, MyLib._myGlobal._cultureInfo());
                                        __last_output = MyLib._myGlobal._convertDateFromQuery(__date_last_output.ToString()).ToShortDateString();
                                    }
                                    __getDiffDate = _calDiffDate(__date_last_input, __date_last_output);
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__row][_g.d.ic_inventory._code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__row][_g.d.ic_inventory._name_1].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __first_input, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __last_input, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __last_output, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __getDiffDate.ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    //if (__getDiffDate > 0)
                                    //{
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __getDiffDate.ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    //}
                                    __countRow++;

                                }

                            }
                            //SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            //this._view1._createEmtryColumn(_objReport2, __dataObject3);
                            //this._view1._addDataColumn(_objReport2, __dataObject3, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport2, __dataObject3, 2, "รวมรายการ  " + string.Format(_formatNumber_no_decimal, __countRow) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                            __countRow = 0;
                        }
                    }
                    SMLReport._report._objectListType __dataObject4 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(_objReport2, __dataObject4);
                    this._view1._addDataColumn(_objReport2, __dataObject4, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(_objReport2, __dataObject4, 0, "รวม " + string.Format(_formatNumber_no_decimal, __countTotal) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);

                    //SMLReport._report._objectListType __dataObject5 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, false, 0, false, SMLReport._report._columnBorder.None);
                    //this._view1._createEmtryColumn(_objReport2, __dataObject5);
                    //this._view1._addDataColumn(_objReport2, __dataObject5, 0, "เงื่อนไขการพิมพ์รายงาน", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);

                    //for (int __loopCon = 0; __loopCon <  _contionStr.Count; __loopCon++)
                    //{
                    //    SMLReport._report._objectListType __dataObject6 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, false, 0, false, SMLReport._report._columnBorder.None);
                    //    this._view1._createEmtryColumn(_objReport2, __dataObject6);
                    //    this._view1._addDataColumn(_objReport2, __dataObject6, 0, _contionStr[__loopCon].ToString(), __newFont, SMLReport._report._cellAlign.Left, 10, SMLReport._report._columnBorder.None);
                    //}
                }
                else if (_icType == SMLERPReportTool._reportEnum.Result_item_export)
                {
                    string __item_code = "";
                    int __total_per_page = 0;
                    int __total_report = 0;
                    double __total_qty_per_page = 0;
                    double __tot_approval_qty_per_page = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (__item_code != __item_code2)
                        {
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["item_group"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["unit_code"].ToString() + "/" + __dr[__i]["unit_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("item_code = \'" + __item_code + "\'");
                            int __row = 0;
                            double __total_qty = 0;
                            double __total_approval_qty = 0;

                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __qty = "";
                                string __approval_qty = "";
                                if (__dr2[__j]["sum_sale_invoice"].ToString() != "" || __dr2[__j]["sum_sale_invoice"].ToString().Length != 0)
                                {
                                    __qty = __dr2[__j]["sum_sale_invoice"].ToString();
                                    __total_qty = __total_qty + double.Parse(__dr2[__j]["sum_sale_invoice"].ToString());
                                    __total_qty_per_page = __total_qty_per_page + double.Parse(__dr2[__j]["sum_sale_invoice"].ToString());
                                }
                                if (__dr2[__j]["sum_sale_remain"].ToString() != "" || __dr2[__j]["sum_sale_remain"].ToString().Length != 0)
                                {
                                    __approval_qty = __dr2[__j]["sum_sale_remain"].ToString();
                                    __total_approval_qty = __total_approval_qty + double.Parse(__dr2[__j]["sum_sale_remain"].ToString());
                                    __tot_approval_qty_per_page = __tot_approval_qty_per_page + double.Parse(__dr2[__j]["sum_sale_remain"].ToString());
                                }

                                if (__approval_qty.Length == 0) __approval_qty = "0.00";
                                else if (!__approval_qty.Contains(".")) __approval_qty = __approval_qty + ".00";
                                if (__qty.Length == 0) __qty = "0.00";
                                else if (!__qty.Contains(".")) __qty = __qty + ".00";


                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["cust_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["cust_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 4, __qty, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 5, __approval_qty, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "sum_sale_invoice"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "sum_sale_remain"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                __row++;
                                __total_per_page++;

                                //if (__j == __dr2.Length - 1)
                                //{
                                //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "รวมรายการ " + __row.ToString() + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, string.Format(_formatNumber, __total_qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(_formatNumber, __total_approval_qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //}
                            }
                        }

                        if (__i == __dr.Length - 1)
                        {
                            SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 3, "รวมรายการ/หน้า " + string.Format(_formatNumber_no_decimal, __total_per_page) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 4, string.Format(_formatNumber, __total_qty_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(_formatNumber, __tot_approval_qty_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }

                    }
                }

                else if (_icType == SMLERPReportTool._reportEnum.Result_item_import)
                {

                    string __item_code = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i][_g.d.ic_inventory._code].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i][_g.d.ic_inventory._code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i][_g.d.ic_inventory._name_1].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["group_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["unit_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            ////this._view1._addDataColumn(_objReport, __dataObject, 4, _check_value_is_zero(double.Parse(__dr[__i]["sum_cost_balance"].ToString()).ToString("##.##")), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                            ////this._view1._addDataColumn(_objReport, __dataObject, 5, _check_value_is_zero(double.Parse(__dr[__i]["sum_qty_balance"].ToString()).ToString("##.##")), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                            ////this._view1._addDataColumn(_objReport, __dataObject, 6, _check_value_is_zero(double.Parse(__dr[__i]["sum_qty_remain"].ToString()).ToString("##.##")), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                            ////this._view1._addDataColumn(_objReport, __dataObject, 7, _check_value_is_zero(double.Parse(__dr[__i]["sum_qty_remain_send"].ToString()).ToString("##.##")), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                            ////this._view1._addDataColumn(_objReport, __dataObject, 8, _cal_available_for_sale(__dr[__i]["sum_qty_balance"].ToString(), __dr[__i]["sum_qty_remain"].ToString(), __dr[__i]["sum_qty_remain_send"].ToString()), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr, __i, "sum_cost_balance"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "sum_qty_balance"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "sum_qty_remain"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "sum_qty_remain_send"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, _cal_available_for_sale(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "sum_qty_balance"), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "sum_qty_remain"), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "sum_qty_remain_send")), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        }

                    }
                }

                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า)
                {
                    //string[] __column = { "ลำดับ", "รหัสสินค้า", "ชื่อสินค้า", "ประเภทสินค้า", "กลุ่มสินค้า", "ต้นทุนแบบ", "สถานะ", "คิดภาษี", "หน่วยนับ", "จำนวน", "ต้นทุนเฉลี่ย", "มูลค่าคงเหลือ", "แสดงยอดคงเหลือตามหน่วยนับ" };
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i][_g.d.ic_resource._ic_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i][_g.d.ic_resource._ic_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i][_g.d.ic_resource._ic_unit_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 3, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, _g.d.ic_resource._balance_qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr, __i, _g.d.ic_resource._average_cost_end), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr, __i, _g.d.ic_resource._balance_amount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Item_balance_hightest)
                {
                    string __group_code = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __group_code2 = __dr[__i]["group_code"].ToString();
                        if (!__group_code.Equals(__group_code2))
                        {
                            __group_code = __group_code2;
                            string __group_name = "";
                            if (__dr[__i]["group_name"].ToString().Length > 0) __group_name = __dr[__i]["group_name"].ToString();
                            else if (__dr[__i]["group_name"].ToString().Length == 0) __group_name = "ไม่มีกลุ่ม";
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["group_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __group_name, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("group_code = \'" + __group_code + "\'");
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["name_1"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["unit_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            }

                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าที่ไม่มีการเคลื่อนไหว)
                {
                    int __no = 0;
                    double __qty = 0;
                    double __cost_avg = 0;
                    double __balance = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        // this._view1._addDataColumn(_objReport, __dataObject, 3, string.Format(_formatNumber, __dr[__i]["qty"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        this._view1._addDataColumn(_objReport, __dataObject, 3, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 10, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        __no++;
                        if (__dr[__i]["qty"].ToString().Length > 0) __qty = __qty + double.Parse(__dr[__i]["qty"].ToString());

                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);

                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "รวมรายการ " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }

                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.Result_transfer_item)
                {
                    int __no = 0;
                    // ยอดยกมา
                    double __qty_import = 0;
                    // ยอดขาย
                    double __qty_sell = 0;
                    // ยอดซื้อ 
                    double __qty_buy = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 10, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 11, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);


                        __no++;
                        //if (__dr[__i]["qty"].ToString().Length > 0) __qty_import = __qty_import + double.Parse(__dr[__i]["qty"].ToString());

                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "รวมรายการ " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __qty_import.ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหว)
                {
                    string __item_code = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("item_code = \'" + __item_code + "\'");

                            string __wh_code = "";
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __wh_code2 = __dr2[__j]["warehouse_code"].ToString();
                                if (!__wh_code.Equals(__wh_code2))
                                {
                                    __wh_code = __wh_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[__j]["warehouse_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    // this._view1._addDataColumn(_objReport2, __dataObject2, 2, string.Format(_formatNumber, __dr[__j]["qty"]), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __j, "qty"), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                }
                            }
                            string __doc_no = "";
                            for (int __k = 0; __k < __dr2.Length; __k++)
                            {
                                string __doc_no2 = __dr[__k]["doc_no"].ToString();
                                if (!__doc_no.Equals(__doc_no2))
                                {
                                    SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport3, __dataObject3);
                                    string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__k]["doc_date"].ToString()).ToShortDateString();
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 1, __dr[__k]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 2, __dr[__k]["tax_no"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                }
                            }

                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Item_transfer_standard)
                {
                    string __item_code = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("item_code = \'" + __item_code + "\'");
                            string __docno = "";
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __docno2 = __dr2[__j]["doc_no"].ToString();
                                if (!__docno.Equals(__docno2))
                                {
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__j]["doc_date"].ToString()).ToShortDateString();
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[__j]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 9, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 10, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 11, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 12, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 13, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 14, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 15, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                }
                            }

                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานบัญชีคุมพิเศษ)
                {
                    //  รายงานบัญชีคุมพิเศษสินค้า
                    string __item_code = "";
                    int __no = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {  //  ชั้นที่ 1
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __no++;
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("item_code = \'" + __item_code + "\'");
                            string __wh_code = "";
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            { //  ชั้นที่ 2
                                string __wh_code2 = __dr2[__j]["warehouse_code"].ToString();
                                if (!__wh_code.Equals(__wh_code2))
                                {
                                    __wh_code = __wh_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                }
                            }

                            string __docno = "";

                            for (int __k = 0; __k < __dr2.Length; __k++)
                            { //  ชั้นที่ 3
                                string __docno2 = __dr2[__k]["doc_no"].ToString();
                                if (!__docno.Equals(__docno2))
                                {
                                    __docno = __docno2;
                                    SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport3, __dataObject3);
                                    string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__k]["doc_date"].ToString()).ToShortDateString();
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 1, __dr[__k]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 2, __dr[__k]["tax_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                                }
                            }

                            if (__i == (__dr.Length - 1))
                            {
                                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport, __dataObject);
                                this._view1._addDataColumn(_objReport, __dataObject, 0, "รวม " + __no.ToString("") + "รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            }
                        }


                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Print_document_for_count_by_item)
                {
                    string __item_code = "";
                    int __no_per_item = 0;
                    int __no_per_page = 0;
                    int __qty_per_page = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        int __qty_per_item = 0;
                        string __item_code2 = __dr[__i]["item_code"].ToString();

                        if (!__item_code.Equals(__item_code2))
                        {
                            __no_per_page++;
                            __no_per_item++;

                            if (__dr[__i]["qty"].ToString().Length > 0)
                            {
                                __qty_per_item += MyLib._myGlobal._intPhase(__dr[__i]["qty"].ToString());
                                __qty_per_page += MyLib._myGlobal._intPhase(__dr[__i]["qty"].ToString());
                            }

                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, __dr[__i]["unit_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            // this._view1._addDataColumn(_objReport, __dataObject, 7, _check_value_is_zero(__dr[__i]["qty"].ToString()) + "   -----", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty") + "   -----", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            if (__i == __dr.Length - 1)
                            {
                                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport, __dataObject);
                                this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 1, "รวม " + __no_per_item.ToString() + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 7, _check_value_is_zero(__qty_per_item.ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            }

                            // complete
                        }
                    }
                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(_objReport, __dataObject2);
                    this._view1._addDataColumn(_objReport, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(_objReport, __dataObject2, 1, "รวม/หน้า " + __no_per_page.ToString() + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(_objReport, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(_objReport, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(_objReport, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(_objReport, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(_objReport, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(_objReport, __dataObject2, 7, _check_value_is_zero(__qty_per_page.ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);

                }
                else if (_icType == SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ)
                {

                    int __no = 0;
                    double __qty = 0;
                    double __count_qty = 0;
                    int __total_qty = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();

                        //if (!__item_code.Equals(__item_code2))
                        //{
                        __no++;
                        __total_qty += MyLib._myGlobal._intPhase(__dr[__i]["qty"].ToString());
                        //if (__dr[__i]["qty"].ToString() != "" || __dr[__i]["qty"].ToString().Length > 0) __qty = double.Parse(__dr[__i]["qty"].ToString());
                        //if (__dr[__i]["count_qty"].ToString() != "" || __dr[__i]["count_qty"].ToString().Length > 0) __count_qty = double.Parse(__dr[__i]["count_qty"].ToString());
                        if (__dr[__i]["qty"].ToString() != "" || __dr[__i]["qty"].ToString().Length > 0) __qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"));
                        if (__dr[__i]["count_qty"].ToString() != "" || __dr[__i]["count_qty"].ToString().Length > 0) __count_qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "count_qty"));
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        string __wh = "";
                        if (__dr[__i]["warehouse_code"].ToString().Length > 0 && __dr[__i]["warehouse_name"].ToString().Length > 0)
                        {
                            __wh = __dr[__i]["warehouse_code"].ToString() + "( " + __dr[__i]["warehouse_name"].ToString() + " )";
                        }
                        string __shelf = "";
                        if (__dr[__i]["shelf_code"].ToString().Length > 0 && __dr[__i]["shelf_name"].ToString().Length > 0)
                        {
                            __shelf = __dr[__i]["shelf_code"].ToString() + "( " + __dr[__i]["shelf_name"].ToString() + " )";
                        }
                        string __unit = "";
                        if (__dr[__i][_g.d.ic_trans_detail._unit_code].ToString().Length > 0 && __dr[__i]["unit_name"].ToString().Length > 0)
                        {
                            __unit = __dr[__i][_g.d.ic_trans_detail._unit_code].ToString() + "( " + __dr[__i]["unit_name"].ToString() + " )";
                        }
                        string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __wh, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 3, __shelf, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 4, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 6, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        ////this._view1._addDataColumn(_objReport, __dataObject, 7, __qty.ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        ////this._view1._addDataColumn(_objReport, __dataObject, 8, __count_qty.ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        ////this._view1._addDataColumn(_objReport, __dataObject, 9, _cal_diff_from_count(__dr[__i]["qty"].ToString(), __dr[__i]["count_qty"].ToString()), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        this._view1._addDataColumn(_objReport, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "count_qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 9, _cal_diff_from_count(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"), MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "count_qty")), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 10, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        //}
                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "รวม " + __no + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, _check_value_is_zero(__total_qty.ToString()), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 10, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.Implement_Item)
                {

                    int __no = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();

                        //if (!__item_code.Equals(__item_code2))
                        //{
                        __no++;
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 6, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 9, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        //}
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Span_import_item)
                {
                    string __ap_code = "";
                    int __no = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __ap_code2 = __dr[__i][""].ToString();
                        if (!__ap_code.Equals(__ap_code2))
                        {
                            __no++;
                            __ap_code = __ap_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["ap_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["ap_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = _dt_head.Select("ap_code = \'" + __ap_code + "\'");
                            string __group_item_code = "";
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __group_item_code2 = __dr2[__j]["group_code"].ToString();
                                if (!__group_item_code.Equals(__group_item_code2))
                                {
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j]["group_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["group_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                }
                            }
                            string __item_code = "";

                            double __total_price = 0.0;
                            for (int __k = 0; __k < __dr2.Length; __k++)
                            {
                                string __item_code2 = __dr2[__k]["item_code"].ToString();
                                if (!__item_code.Equals(__item_code2))
                                {

                                    //__amount += double.Parse(__dr2[__k]["amount"].ToString());
                                    SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport3, __dataObject3);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 0, __dr2[__k]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 1, __dr2[__k]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport3, __dataObject3, 9, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                }

                            }

                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject3 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport3, __dataObject3);
                            this._view1._addDataColumn(_objReport3, __dataObject3, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport3, __dataObject3, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport3, __dataObject3, 2, "รวมรายการ " + __no.ToString() + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport3, __dataObject3, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport3, __dataObject3, 4, __amount.ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Lot_item)
                {
                    string __date_in = "";
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __date_in2 = __dr[__i]["date_in"].ToString();
                        if (!__date_in.Equals(__date_in2))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = _dt_head.Select("date_in = \'" + __date_in + "\'");
                            int __qty = 0;
                            double __total_price = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, double.Parse(__dr2[__j][""].ToString()).ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, double.Parse(__dr2[__j][""].ToString()).ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 9, double.Parse(__dr2[__j][""].ToString()).ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 10, double.Parse(__dr2[__j][""].ToString()).ToString("##.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 11, __dr2[__j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            }

                        }

                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา)
                {  //  รายงานสินค้าและวัตถุดิบ  คงเหลือยกมา
                    string __doc_no = "";
                    int __totals = 0;
                    double __qty_all = 0;
                    double __amount_all = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __chk_cancel_document = __dr[__i]["trans_flag"].ToString();
                        if (__chk_cancel_document == "55")
                        { __newFont = new Font(__getColumn._fontData, FontStyle.Strikeout); }
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {  //  ชั้นแรก
                            __totals++;
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                            string __ref_doc_date = "";
                            if (__dr[__i]["ref_doc_date"].ToString() != "") __ref_doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["ref_doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __ref_doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["doc_ref"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("doc_no = '" + __doc_no + "'");
                            string __item_code = "";
                            int __no = 0;

                            double __total_qty = 0; double __total_price = 0; double __total_all = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {  //  ชั้นที่  2 
                                string __item_code2 = __dr2[__j]["item_code"].ToString();

                                if (!__item_code.Equals(__item_code2))
                                {
                                    __no++;
                                    __item_code = __item_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["unit_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["wh_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    double __qty = 0; double __price = 0; double __total = 0;
                                    ////if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                    ////if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(__dr2[__j]["price"].ToString());
                                    if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));
                                    if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "price"));
                                    __total = __qty * __price;
                                    __total_qty += __qty; __total_price += __price; __total_all += __total;
                                    __qty_all += __qty; __amount_all += __total;
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "price"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    if (__j == (__dr2.Length - 1))
                                    {
                                        __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                        this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no.ToString() + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __total_qty), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __total_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    }
                                }
                            }


                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);
                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __totals) + "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 7, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);

                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.Cancel_Item_and_Staple)
                {  //  รายงานยกเลิกสินค้าและวัตถุดิบ  คงเหลือยกมา
                    string __doc_no = "";
                    int __totals = 0;

                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {  //  ชั้นแรก
                            __totals++;
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // doc_no
                            string __doc_date = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  doc_date
                            string __ref_doc_date = "";
                            if (__dr[__i]["ref_doc_date"].ToString() != "") __ref_doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["ref_doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __ref_doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ref_doc_date
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["ref_doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ref_doc_no
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // remark

                            DataRow[] __dr2 = _dt_head.Select("doc_no = '" + __doc_no + "'");
                            string __item_code = "";
                            int __no = 0;
                            double __total_qty = 0; double __total_price = 0; double __total_all = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {  //  ชั้นที่  2 
                                string __item_code2 = __dr2[__j]["item_code"].ToString();

                                if (!__item_code.Equals(__item_code2))
                                {
                                    __no++;
                                    __item_code = __item_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // item_code
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  item_name
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["unit_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // unit name
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["wh_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  //  wh name
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // shelf name
                                    double __qty = 0; double __price = 0; double __total = 0;
                                    ////if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                    ////if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(__dr2[__j]["price"].ToString());
                                    if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));
                                    if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "price"));
                                    __total = __qty * __price;
                                    __total_qty += __qty; __total_price += __price; __total_all += __total;

                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "price"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal), __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                    //if (__j == (__dr2.Length - 1))
                                    //{
                                    //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no.ToString() + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __total_qty.ToString("#,###.##"), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 7, __total_price.ToString("#,###.##"), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 8, __total_all.ToString("#,###.##"), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //}
                                }
                            }


                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __totals) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //string __doc_date = "";
                            //if (__dr[__i]["doc_date"].ToString() != "") __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //string __ref_doc_date = "";
                            //if (__dr[__i]["ref_doc_date"].ToString() != "") __ref_doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["ref_doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __ref_doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["ref_doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.Print_document_for_count_by_warehouse)
                {
                    string __wh_code = "";
                    int __no = 0;
                    double __total_qty = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __wh_code2 = __dr[__i]["warehouse_code"].ToString();
                        //if (!__wh_code.Equals(__wh_code2))
                        //{
                        __no++;
                        //__total_qty += MyLib._myGlobal._intPhase(__dr[__i]["qty"].ToString());
                        __wh_code = __wh_code2;
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["warehouse_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                        this._view1._addDataColumn(_objReport, __dataObject, 2, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        this._view1._addDataColumn(_objReport, __dataObject, 6, __dr[__i]["unit_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        double __qty = 0; if (__dr[__i]["qty"].ToString() != "") __qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"));
                        __total_qty += __qty;
                        //this._view1._addDataColumn(_objReport, __dataObject, 7, __qty.ToString("#,###.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        this._view1._addDataColumn(_objReport, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(_objReport, __dataObject, 8, "----------", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        //}

                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            // this._view1._addDataColumn(_objReport, __dataObject, 7, string.Format(_formatNumber, __total_qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal), __total_qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน)
                {
                    //  รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)
                    string __doc_no = "";
                    int __totals = 0;
                    double __qty_all = 0;
                    double __price_all = 0;
                    double __amount_all = 0;

                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __chk_tran_flag = __dr[__i]["trans_flag"].ToString();
                        if (__chk_tran_flag == "67")
                        { __newFont = new Font(__getColumn._fontData, FontStyle.Strikeout); }
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {  //  ชั้นแรก
                            __totals++;
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                            string __ref_doc_date = "";
                            //if (__dr[__i]["ref_doc_date"].ToString() != "") __ref_doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["ref_doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __ref_doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["ref_doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["doc_ref"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("doc_no = '" + __doc_no + "'");
                            string __item_code = "";
                            int __no = 0;
                            double __qty_in = 0;
                            double __price_in = 0;
                            double __amount_in = 0;
                            double __total_qty = 0; double __total_price = 0; double __total_all = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {  //  ชั้นที่  2 
                                string __item_code2 = __dr2[__j]["item_code"].ToString();

                                if (!__item_code.Equals(__item_code2))
                                {
                                    __no++;
                                    __item_code = __item_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                    string __unit = ""; string __unit_code = ""; string __unit_name = "";
                                    if (__dr2[__j]["unit_code"].ToString() != "") __unit_code = __dr2[__j]["unit_code"].ToString();
                                    if (__dr2[__j]["unit_name"].ToString() != "") __unit_name = __dr2[__j]["unit_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __unit = __unit_code + "~" + __unit_name;
                                    else
                                    {
                                        if (__unit_code != "") __unit = __unit_code;
                                        if (__unit_name != "") __unit = __unit_name;
                                    }

                                    string __wh = ""; string __wh_code = ""; string __wh_name = "";
                                    if (__dr2[__j]["wh_code"].ToString() != "") __wh_code = __dr2[__j]["wh_code"].ToString();
                                    if (__dr2[__j]["wh_name"].ToString() != "") __wh_name = __dr2[__j]["wh_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __wh = __wh_code + "~" + __wh_name;
                                    else
                                    {
                                        if (__wh_code != "") __wh = __wh_code;
                                        if (__wh_name != "") __wh = __wh_name;
                                    }

                                    string __shelf = ""; string __shelf_code = ""; string __shelf_name = "";
                                    if (__dr2[__j]["shelf_code"].ToString() != "") __shelf_code = __dr2[__j]["shelf_code"].ToString();
                                    if (__dr2[__j]["shelf_name"].ToString() != "") __shelf_name = __dr2[__j]["shelf_name"].ToString();
                                    if ((__shelf_code != "") && (__shelf_name != "")) __shelf = __shelf_code + "~" + __shelf_name;
                                    else
                                    {
                                        if (__shelf_code != "") __shelf = __shelf_code;
                                        if (__shelf_name != "") __shelf = __shelf_name;
                                    }


                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __wh, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __shelf, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    double __qty = 0;
                                    double __price = 0;
                                    double __total = 0;
                                    ////if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                    ////if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(__dr2[__j]["price"].ToString());
                                    ////if (__dr2[__j]["total_amount"].ToString() != "") __total = double.Parse(__dr2[__j]["total_amount"].ToString());
                                    string __qq = MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty");
                                    if (__qq != "" && __qq != "0") __qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));
                                    if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "price"));
                                    if (__dr2[__j]["total_amount"].ToString() != "") __total = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr2, __j, "total_amount"));
                                    __qty_in += __qty; __qty_all += __qty;
                                    __price_in += __price; __price_all += __price;
                                    __amount_in += __total; __amount_all += __total;


                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);


                                }
                                if (__j == (__dr2.Length - 1))
                                {
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_in), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_in), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                }
                            }


                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);

                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __totals) + "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.Cancel_Implement_Item_and_Staple_Over)
                {
                    //  รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)
                    string __doc_no = "";
                    int __totals = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {  //  ชั้นแรก
                            __totals++;
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __ref_doc_date = "";
                            //if (__dr[__i]["ref_doc_date"].ToString() != "") __ref_doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["ref_doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __ref_doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["ref_doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("doc_no = '" + __doc_no + "'");
                            string __item_code = "";
                            int __no = 0;
                            double __total_qty = 0; double __total_price = 0; double __total_all = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {  //  ชั้นที่  2 
                                string __item_code2 = __dr2[__j]["item_code"].ToString();

                                if (!__item_code.Equals(__item_code2))
                                {
                                    __no++;
                                    __item_code = __item_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                    string __unit = ""; string __unit_code = ""; string __unit_name = "";
                                    if (__dr2[__j]["unit_code"].ToString() != "") __unit_code = __dr2[__j]["unit_code"].ToString();
                                    if (__dr2[__j]["unit_name"].ToString() != "") __unit_name = __dr2[__j]["unit_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __unit = __unit_code + "~" + __unit_name;
                                    else
                                    {
                                        if (__unit_code != "") __unit = __unit_code;
                                        if (__unit_name != "") __unit = __unit_name;
                                    }

                                    string __wh = ""; string __wh_code = ""; string __wh_name = "";
                                    if (__dr2[__j]["wh_code"].ToString() != "") __wh_code = __dr2[__j]["wh_code"].ToString();
                                    if (__dr2[__j]["wh_name"].ToString() != "") __wh_name = __dr2[__j]["wh_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __wh = __wh_code + "~" + __wh_name;
                                    else
                                    {
                                        if (__wh_code != "") __wh = __wh_code;
                                        if (__wh_name != "") __wh = __wh_name;
                                    }

                                    string __shelf = ""; string __shelf_code = ""; string __shelf_name = "";
                                    if (__dr2[__j]["shelf_code"].ToString() != "") __shelf_code = __dr2[__j]["shelf_code"].ToString();
                                    if (__dr2[__j]["shelf_name"].ToString() != "") __shelf_name = __dr2[__j]["shelf_name"].ToString();
                                    if ((__shelf_code != "") && (__shelf_name != "")) __shelf = __shelf_code + "~" + __shelf_name;
                                    else
                                    {
                                        if (__shelf_code != "") __shelf = __shelf_code;
                                        if (__shelf_name != "") __shelf = __shelf_name;
                                    }


                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __wh, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __shelf, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    double __qty = 0; double __price = 0; double __total = 0;
                                    ////if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                    ////if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(__dr2[__j]["price"].ToString());
                                    if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));
                                    if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "price"));
                                    __total = __qty * __price;
                                    __total_qty += __qty; __total_price += __price; __total_all += __total;

                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._decimalPhase(__qty.ToString()).ToString("#,###.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._decimalPhase(__price.ToString()).ToString("#,###.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._decimalPhase(__total.ToString()).ToString("#,###.##"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "price"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal), __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                    //if (__j == (__dr2.Length - 1))
                                    //{
                                    //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __total_qty), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __total_price), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //}
                                }
                            }


                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);
                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __totals) + "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 7, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 8, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);

                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด)
                {
                    //  รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด)
                    string __doc_no = "";
                    int __totals = 0;
                    double __qty_all = 0;
                    double __price_all = 0;
                    double __amount_all = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __chk_trans_flag = __dr[__i]["trans_flag"].ToString();
                        if (__chk_trans_flag == "69")
                        { __newFont = new Font(__getColumn._fontData, FontStyle.Strikeout); }
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {  //  ชั้นแรก
                            __totals++;
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                            string __ref_doc_date = "";
                            //if (__dr[__i]["ref_doc_date"].ToString() != "") __ref_doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["ref_doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __ref_doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["ref_doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["doc_ref"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("doc_no = '" + __doc_no + "'");
                            string __item_code = "";
                            int __no = 0;
                            double __qty_in = 0;
                            double __price_in = 0;
                            double __amount_in = 0;
                            double __total_qty = 0; double __total_price = 0; double __total_all = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {  //  ชั้นที่  2 
                                string __item_code2 = __dr2[__j]["item_code"].ToString();

                                if (!__item_code.Equals(__item_code2))
                                {
                                    __no++;
                                    __item_code = __item_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                    string __unit = ""; string __unit_code = ""; string __unit_name = "";
                                    if (__dr2[__j]["unit_code"].ToString() != "") __unit_code = __dr2[__j]["unit_code"].ToString();
                                    if (__dr2[__j]["unit_name"].ToString() != "") __unit_name = __dr2[__j]["unit_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __unit = __unit_code + "~" + __unit_name;
                                    else
                                    {
                                        if (__unit_code != "") __unit = __unit_code;
                                        if (__unit_name != "") __unit = __unit_name;
                                    }

                                    string __wh = ""; string __wh_code = ""; string __wh_name = "";
                                    if (__dr2[__j]["wh_code"].ToString() != "") __wh_code = __dr2[__j]["wh_code"].ToString();
                                    if (__dr2[__j]["wh_name"].ToString() != "") __wh_name = __dr2[__j]["wh_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __wh = __wh_code + "~" + __wh_name;
                                    else
                                    {
                                        if (__wh_code != "") __wh = __wh_code;
                                        if (__wh_name != "") __wh = __wh_name;
                                    }

                                    string __shelf = ""; string __shelf_code = ""; string __shelf_name = "";
                                    if (__dr2[__j]["shelf_code"].ToString() != "") __shelf_code = __dr2[__j]["shelf_code"].ToString();
                                    if (__dr2[__j]["shelf_name"].ToString() != "") __shelf_name = __dr2[__j]["shelf_name"].ToString();
                                    if ((__shelf_code != "") && (__shelf_name != "")) __shelf = __shelf_code + "~" + __shelf_name;
                                    else
                                    {
                                        if (__shelf_code != "") __shelf = __shelf_code;
                                        if (__shelf_name != "") __shelf = __shelf_name;
                                    }


                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __wh, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __shelf, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    double __qty = 0; double __price = 0; double __total = 0;
                                    if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                    if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(__dr2[__j]["price"].ToString());
                                    __total = __qty * __price;
                                    __qty_in += __qty; __qty_all += __qty;
                                    __price_in += __price; __price_all += __price;
                                    __amount_in += __total; __amount_all += __total;

                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                }
                                if (__j == (__dr2.Length - 1))
                                {
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_in), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_in), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                }
                            }


                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);

                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __totals) + "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.Cancel_Implement_Item_and_Staple_Minus)
                {
                    //  รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด)
                    string __doc_no = "";
                    int __totals = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {  //  ชั้นแรก
                            __totals++;
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __ref_doc_date = "";
                            //if (__dr[__i]["ref_doc_date"].ToString() != "") __ref_doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["ref_doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __ref_doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["ref_doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            DataRow[] __dr2 = _dt_head.Select("doc_no = '" + __doc_no + "'");
                            string __item_code = "";
                            int __no = 0;
                            double __total_qty = 0; double __total_price = 0; double __total_all = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {  //  ชั้นที่  2 
                                string __item_code2 = __dr2[__j]["item_code"].ToString();

                                if (!__item_code.Equals(__item_code2))
                                {
                                    __no++;
                                    __item_code = __item_code2;
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                    string __unit = ""; string __unit_code = ""; string __unit_name = "";
                                    if (__dr2[__j]["unit_code"].ToString() != "") __unit_code = __dr2[__j]["unit_code"].ToString();
                                    if (__dr2[__j]["unit_name"].ToString() != "") __unit_name = __dr2[__j]["unit_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __unit = __unit_code + "~" + __unit_name;
                                    else
                                    {
                                        if (__unit_code != "") __unit = __unit_code;
                                        if (__unit_name != "") __unit = __unit_name;
                                    }

                                    string __wh = ""; string __wh_code = ""; string __wh_name = "";
                                    if (__dr2[__j]["wh_code"].ToString() != "") __wh_code = __dr2[__j]["wh_code"].ToString();
                                    if (__dr2[__j]["wh_name"].ToString() != "") __wh_name = __dr2[__j]["wh_name"].ToString();
                                    if ((__unit_code != "") && (__unit_name != "")) __wh = __wh_code + "~" + __wh_name;
                                    else
                                    {
                                        if (__wh_code != "") __wh = __wh_code;
                                        if (__wh_name != "") __wh = __wh_name;
                                    }

                                    string __shelf = ""; string __shelf_code = ""; string __shelf_name = "";
                                    if (__dr2[__j]["shelf_code"].ToString() != "") __shelf_code = __dr2[__j]["shelf_code"].ToString();
                                    if (__dr2[__j]["shelf_name"].ToString() != "") __shelf_name = __dr2[__j]["shelf_name"].ToString();
                                    if ((__shelf_code != "") && (__shelf_name != "")) __shelf = __shelf_code + "~" + __shelf_name;
                                    else
                                    {
                                        if (__shelf_code != "") __shelf = __shelf_code;
                                        if (__shelf_name != "") __shelf = __shelf_name;
                                    }


                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __wh, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __shelf, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    double __qty = 0; double __price = 0; double __total = 0;
                                    if (__dr2[__j]["qty"].ToString() != "") __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                    if (__dr2[__j]["price"].ToString() != "") __price = double.Parse(__dr2[__j]["price"].ToString());
                                    __total = __qty * __price;
                                    __total_qty += __qty; __total_price += __price; __total_all += __total;

                                    // this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    //this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    //this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    //if (__j == (__dr2.Length - 1))
                                    //{
                                    //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __total_qty), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //    //this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __total_price), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //    //this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                    //}
                                }
                            }


                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);
                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __totals) + "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                        }
                    }

                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป)
                {
                    //  รายงานการรับสินค้าสำเร็จรูป

                    string __doc_date = "";
                    string __test_query = this.__query_head.ToString();
                    int __total = 0;
                    double __qty_all = 0;
                    double __price_all = 0;
                    double __amount_all = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __chk_trans_flag = __dr[__i]["trans_flag"].ToString();
                        if (__chk_trans_flag == "61") __newFont = new Font(__getColumn._fontData, FontStyle.Strikeout);
                        string __doc_date2 = __dr[__i]["doc_date"].ToString();
                        if (!__doc_date.Equals(__doc_date2))
                        {

                            __total++;
                            __doc_date = __doc_date2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            string __doc_no = __dr[__i][1].ToString();
                            string __remark = __dr[__i]["remark"].ToString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_screen, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);

                            this._view1._addDataColumn(_objReport, __dataObject, 1, __remark, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = _dt_head.Select("doc_date = \'" + __doc_date + "\'");
                            string __item_code = "";
                            int __no = 0;
                            double __total_qty = 0;
                            double __total_amount = 0;
                            double __total_cost = 0;
                            double __qty = 0;
                            double __price = 0;
                            double __cost = 0;
                            double __qty_in = 0;
                            double __price_in = 0;
                            double __amount_in = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __Item_code2 = __dr2[__j][_g.d.ic_trans_detail._item_code].ToString();
                                if (!__item_code.Equals(__Item_code2))
                                {
                                    __item_code = __Item_code2;
                                    __no++;
                                    if (__dr2[__j]["qty"].ToString().Length != 0 || __dr2[__j]["qty"].ToString() != "")
                                    {
                                        string __test_qty = __dr2[__j]["qty"].ToString();
                                        __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                        //__total_qty += double.Parse(__dr2[__j]["qty"].ToString());
                                        __qty_in += __qty;
                                        __qty_all += __qty;

                                    }
                                    if (__dr2[__j]["price"].ToString().Length != 0 || __dr2[__j]["price"].ToString() != "")
                                    {
                                        __cost = double.Parse(__dr2[__j]["price"].ToString());
                                        __price_in += __cost;
                                        __price_all += __cost;
                                        //__total_cost += double.Parse(__dr2[__j]["price"].ToString());
                                    }
                                    if (__dr2[__j]["sum_of_cost"].ToString().Length != 0 || __dr2[__j]["sum_of_cost"].ToString() != "")
                                    {
                                        //__price = double.Parse(__dr2[__j]["sum_of_cost"].ToString()) ;
                                        __price = __qty * __cost;
                                        __amount_in += __price;
                                        __amount_all += __price;
                                        //__total_amount += __price;
                                    }


                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["doc_ref"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __dr2[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    string __unit = __dr2[__j][_g.d.ic_trans_detail._unit_code].ToString() + "( " + __dr2[__j][_g.d.ic_trans_detail._unit_name].ToString() + " )";
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal), __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal), __cost), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 10, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal), __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                }
                                if (__j == __dr2.Length - 1)
                                {
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber, __no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 10, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                }
                            }

                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);
                            //string __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __total) + "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 9, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __price_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject, 10, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __amount_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Cancel_Import_Item_ready)
                {
                    //  รายงานการยกเลิกรับสินค้าสำเร็จรูป
                    string __doc_date = "";
                    int __total = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_date2 = __dr[__i]["doc_date"].ToString();
                        if (!__doc_date.Equals(__doc_date2))
                        {
                            __total++;
                            __doc_date = __doc_date2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_screen, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = _dt_head.Select("doc_date = \'" + __doc_date + "\'");
                            string __item_code = "";
                            int __no = 0;
                            double __total_qty = 0;
                            double __total_amount = 0;
                            double __total_cost = 0;
                            double __qty = 0;
                            double __price = 0;
                            double __cost = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __Item_code2 = __dr2[__j][_g.d.ic_trans_detail._item_code].ToString();
                                if (!__item_code.Equals(__Item_code2))
                                {
                                    __item_code = __Item_code2;
                                    __no++;
                                    if (__dr2[__j]["qty"].ToString().Length != 0 || __dr2[__j]["qty"].ToString() != "")
                                    {
                                        __qty = double.Parse(__dr2[__j]["qty"].ToString());
                                        __total_qty += double.Parse(__dr2[__j]["qty"].ToString());

                                    }
                                    if (__dr2[__j]["sum_of_cost"].ToString().Length != 0 || __dr2[__j]["sum_of_cost"].ToString() != "")
                                    {
                                        __price = double.Parse(__dr2[__j]["sum_of_cost"].ToString());
                                        __total_amount += double.Parse(__dr2[__j]["sum_of_cost"].ToString());
                                    }
                                    if (__dr2[__j]["price"].ToString().Length != 0 || __dr2[__j]["price"].ToString() != "")
                                    {
                                        __cost = double.Parse(__dr2[__j]["price"].ToString());
                                        __total_cost += double.Parse(__dr2[__j]["price"].ToString());
                                    }

                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    //this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["department"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    string __unit = __dr2[__j][_g.d.ic_trans_detail._unit_code].ToString() + "( " + __dr2[__j][_g.d.ic_trans_detail._unit_name].ToString() + " )";
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __unit, __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __cost), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "price"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                }
                                //if (__j == __dr2.Length - 1)
                                //{
                                //    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "รวม " + string.Format(_formatNumber,__no) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    //this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    //this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __total_qty), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __total_cost), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total_amount), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //}
                            }

                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            //string __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __total) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Receptance_Widen_by_Date)
                {
                    string __doc_date = "";
                    int __total = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_date2 = __dr[__i]["doc_date"].ToString();
                        if (!__doc_date.Equals(__doc_date2))
                        {
                            __total++;
                            __doc_date = __doc_date2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_screen, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            DataRow[] __dr2 = _dt_head.Select("doc_date = \'" + __doc_date + "\'");

                            int __no = 0;
                            int __total_qty = 0;

                            double __total_cost = 0; // ต้นทุน
                            double __total_cost_all = 0; // ต้นทุนทั้งสิ้น

                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);

                                if (_condition._display_serial == true)
                                {
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["serial"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["sale_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["department"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __dr2[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __dr2[__j]["qty"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, __dr2[__j]["cost"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 10, string.Format(_formatNumber, __dr2[__j]["sum_of_cost"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr2, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                }
                                else if (_condition._display_serial == false)
                                {
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                    this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["sale_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["department"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 6, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __dr2[__j]["qty"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __dr2[__j]["cost"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    ////this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, __dr2[__j]["sum_of_cost"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    this._view1._addDataColumn(_objReport2, __dataObject2, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr2, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                }


                                //if (__j == __dr2.Length - 1)
                                //{
                                //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber_no_decimal,__no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total_qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, __total_cost), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 10, string.Format(_formatNumber, __total_cost_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                                //}
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            //string __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "รวม " + string.Format(_formatNumber_no_decimal, __total) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ)
                {
                    // รายงานเบิกสินค้าและวัตถุ
                    string __doc_date = "";
                    int __total = 0;
                    double __qty_all = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __chk_trans_flag = __dr[__i]["doc_ref"].ToString();
                        if (__chk_trans_flag == "57")
                        { __newFont = new Font(__getColumn._fontData, FontStyle.Strikeout); }

                        string __doc_date2 = __dr[__i]["doc_date"].ToString();
                        if (!__doc_date.Equals(__doc_date2))
                        {
                            __total++;
                            __doc_date = __doc_date2;
                            //SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            // this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_screen = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_screen, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            DataRow[] __dr2 = _dt_head.Select("doc_date = \'" + __doc_date + "\'");
                            string __doc_no = "";

                            for (int __a = 0; __a < __dr2.Length; __a++)
                            {
                                string __doc_no2 = __dr2[__a]["doc_no"].ToString();
                                if (__doc_no != __doc_no2)
                                {
                                    __doc_no = __doc_no2;
                                    SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport, __dataObject);
                                    this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date_screen, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                                    this._view1._addDataColumn(_objReport, __dataObject, 2, __dr2[__a]["doc_ref"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 3, __dr2[__a]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                    int __no = 0;
                                    double __total_qty = 0;
                                    double __total_cost_all = 0; // ต้นทุนทั้งสิ้น

                                    DataRow[] __dr3 = _dt_head.Select("doc_no = \'" + __dr2[__a]["doc_no"].ToString() + "\'");
                                    double __qty_in = 0;
                                    for (int __j = 0; __j < __dr3.Length; __j++)
                                    {
                                        __no++;
                                        if (__dr3[__j]["qty"].ToString() != "") __total_qty += double.Parse(__dr3[__j]["qty"].ToString());
                                        if (__dr3[__j]["sum_of_cost"].ToString() != "") __total_cost_all += double.Parse(__dr3[__j]["sum_of_cost"].ToString());

                                        SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr3[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr3[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr3[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr3[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr3[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        double __qty = 0; double __sum_of_cost = 0;
                                        if (__dr3[__j]["qty"].ToString() != "") __qty = double.Parse(__dr3[__j]["qty"].ToString());
                                        if (__dr3[__j]["sum_of_cost"].ToString() != "") __sum_of_cost = double.Parse(__dr3[__j]["sum_of_cost"].ToString());
                                        __qty_in += __qty;
                                        __qty_all += __qty;

                                        this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);


                                        if (__j == __dr3.Length - 1)
                                        {
                                            __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                        }
                                    }
                                }
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject);
                            this._view1._addDataColumn(_objReport2, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __total) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);

                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Cancel_Withdraw_Item_Staple)
                {
                    string __doc_date = "";
                    int __total = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_date2 = __dr[__i]["doc_date"].ToString();
                        if (!__doc_date.Equals(__doc_date2))
                        {
                            __total++;
                            __doc_date = __doc_date2;
                            //SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            // this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_screen = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_screen, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            DataRow[] __dr2 = _dt_head.Select("doc_date = \'" + __doc_date + "\'");
                            string __doc_no = "";
                            for (int __a = 0; __a < __dr2.Length; __a++)
                            {
                                string __doc_no2 = __dr2[__a]["doc_no"].ToString();
                                if (__doc_no != __doc_no2)
                                {
                                    __doc_no = __doc_no2;
                                    SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport, __dataObject);
                                    this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date_screen, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 2, __dr2[__a]["doc_ref"].ToString(), __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 3, __dr2[__a]["remark"].ToString(), __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                                    int __no = 0;
                                    double __total_qty = 0;
                                    double __total_cost_all = 0; // ต้นทุนทั้งสิ้น
                                    DataRow[] __dr3 = _dt_head.Select("doc_no = \'" + __dr2[__a]["doc_no"].ToString() + "\'");
                                    for (int __j = 0; __j < __dr3.Length; __j++)
                                    {
                                        __no++;
                                        if (__dr3[__j]["qty"].ToString() != "") __total_qty += double.Parse(__dr3[__j]["qty"].ToString());
                                        if (__dr3[__j]["sum_of_cost"].ToString() != "") __total_cost_all += double.Parse(__dr3[__j]["sum_of_cost"].ToString());

                                        SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr3[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr3[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        //this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr3[__j]["sale_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                        //this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr3[__j]["department"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr3[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr3[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr3[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        double __qty = 0; double __sum_of_cost = 0;
                                        if (__dr3[__j]["qty"].ToString() != "") __qty = double.Parse(__dr3[__j]["qty"].ToString());
                                        if (__dr3[__j]["sum_of_cost"].ToString() != "") __sum_of_cost = double.Parse(__dr3[__j]["sum_of_cost"].ToString());
                                        __total_qty += __qty; __total_cost_all += __sum_of_cost;
                                        ////this._view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                        ////this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __sum_of_cost), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr3, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                        //if (__j == __dr3.Length - 1)
                                        //{
                                        //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                        //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                        //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                        //    //this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                        //    //this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                        //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                        //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                        //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None);
                                        //    this._view1._addDataColumn(_objReport2, __dataObject2, 5,string.Format(_formatNumber, __total_qty), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                        //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __total_cost_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                        //}
                                    }
                                }
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            //string __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __total) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ)
                {
                    //  รายงานรับคืนสินค้าและวัตถุดิบ  จากการเบิก
                    string __doc_date = "";
                    double __qty_all = 0;
                    double __price_all = 0;
                    double __amount_all = 0;
                    int __totals = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __chk_tran_flag = __dr[__i]["trans_flag"].ToString();
                        if (__chk_tran_flag == "59")
                        { __newFont = new Font(__getColumn._fontData, FontStyle.Strikeout); }
                        string __doc_date2 = __dr[__i]["doc_date"].ToString();
                        if (!__doc_date.Equals(__doc_date2))
                        {

                            __doc_date = __doc_date2;

                            string __doc_date_screen = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_screen, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            DataRow[] __dr2 = _dt_head.Select("doc_date = \'" + __doc_date + "\'");
                            string __doc_no = "";
                            for (int __a = 0; __a < __dr2.Length; __a++)
                            {
                                string __doc_no2 = __dr2[__a]["doc_no"].ToString();
                                if (__doc_no != __doc_no2)
                                {
                                    __totals++;
                                    __doc_no = __doc_no2;
                                    SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport, __dataObject);
                                    this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date_screen, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.DateTime);
                                    this._view1._addDataColumn(_objReport, __dataObject, 2, __dr2[__a]["doc_ref_from_withdraw"].ToString(), __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 3, __dr2[__a]["doc_ref"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 4, __dr2[__a]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                                    int __no = 0;
                                    double __qty_in = 0;
                                    double __price_in = 0;
                                    double __amount_in = 0;

                                    DataRow[] __dr3 = _dt_head.Select("doc_no = \'" + __dr2[__a]["doc_no"].ToString() + "\'");
                                    string __item_code = "";

                                    for (int __j = 0; __j < __dr3.Length; __j++)
                                    {
                                        string __item_code2 = __dr3[__j]["item_code"].ToString();
                                        if (__item_code != __item_code2)
                                        {
                                            __no++;

                                            SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr3[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr3[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            // this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr3[__j]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr3[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr3[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr3[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            //this._view1._addDataColumn(_objReport2, __dataObject2, 7, __dr3[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            double __qty = 0; double __sum_of_cost = 0; double __price = 0;
                                            ////if (__dr3[__j]["qty"].ToString() != "") __qty = double.Parse(__dr3[__j]["qty"].ToString());
                                            ////if (__dr3[__j]["sum_of_cost"].ToString() != "") __sum_of_cost = double.Parse(__dr3[__j]["sum_of_cost"].ToString());
                                            if (__dr3[__j]["qty"].ToString() != "") __qty = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty"));
                                            if (__dr3[__j]["sum_of_cost"].ToString() != "") __sum_of_cost = double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr3, __j, "sum_of_cost"));

                                            __price = __qty * __sum_of_cost;
                                            __qty_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty"));
                                            __qty_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty"));
                                            __price_in += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr3, __j, "sum_of_cost"));
                                            __price_all += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr3, __j, "sum_of_cost"));
                                            __amount_in += __price;
                                            __amount_all += __price;

                                            this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr3, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal), __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);


                                        }
                                        if (__j == __dr3.Length - 1)
                                        {
                                            SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_in), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                        }
                                    }
                                }
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "รวม " + __totals + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal).ToString(), __qty_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal).ToString(), __price_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal).ToString(), __amount_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Cancel_Refunded_Withdraw_Item_Staple)
                {
                    //  รายงานยกเลิกรับคืนสินค้าและวัตถุดิบ  จากการเบิก
                    string __doc_date = "";
                    int __totals = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_date2 = __dr[__i]["doc_date"].ToString();
                        if (!__doc_date.Equals(__doc_date2))
                        {

                            __doc_date = __doc_date2;
                            //SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            // this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date_screen = "";
                            if (__dr[__i]["doc_date"].ToString() != "") __doc_date_screen = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            //this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date_screen, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            DataRow[] __dr2 = _dt_head.Select("doc_date = \'" + __doc_date + "\'");
                            string __doc_no = "";
                            for (int __a = 0; __a < __dr2.Length; __a++)
                            {
                                string __doc_no2 = __dr2[__a]["doc_no"].ToString();
                                if (__doc_no != __doc_no2)
                                {
                                    __totals++;
                                    __doc_no = __doc_no2;
                                    SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport, __dataObject);
                                    this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_no, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date_screen, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                    this._view1._addDataColumn(_objReport, __dataObject, 2, __dr2[__a]["doc_ref"].ToString(), __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                                    int __no = 0;
                                    double __total_qty = 0;
                                    double __total_cost_all = 0; // ต้นทุนทั้งสิ้น
                                    double __total_price = 0;
                                    DataRow[] __dr3 = _dt_head.Select("doc_no = \'" + __dr2[__a]["doc_no"].ToString() + "\'");
                                    string __item_code = "";

                                    for (int __j = 0; __j < __dr3.Length; __j++)
                                    {
                                        string __item_code2 = __dr3[__j]["item_code"].ToString();
                                        if (__item_code != __item_code2)
                                        {
                                            __no++;

                                            //    string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "คลังสินค้า", "พื้นที่เก็บ", "จำนวน*", "ต้นทุน*", "มูลค่า*" };
                                            SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr3[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr3[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            // this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr3[__j]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr3[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr3[__j]["warehouse_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr3[__j]["shelf_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                            //this._view1._addDataColumn(_objReport2, __dataObject2, 7, __dr3[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            double __price = 0;
                                            ////if (__dr3[__j]["qty"].ToString() != "") __qty = double.Parse(__dr3[__j]["qty"].ToString());
                                            ////if (__dr3[__j]["sum_of_cost"].ToString() != "") __sum_of_cost = double.Parse(__dr3[__j]["sum_of_cost"].ToString());                                            
                                            double __qty = (__dr3[__j]["qty"].ToString() != "") ? double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty")) : 0;
                                            double __sum_of_cost = (__dr3[__j]["sum_of_cost"].ToString() != "") ? double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr3, __j, "sum_of_cost")) : 0;
                                            __total_qty += __qty; __total_cost_all += __sum_of_cost;
                                            __price = __qty * __sum_of_cost; __total_price += __price;
                                            ////this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                            ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __sum_of_cost), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                            ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr3, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr3, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                            this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal), __price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                            //if (__j == __dr3.Length - 1)
                                            //{
                                            //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + __no + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __total_qty), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, __total_cost_all), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                            //    this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, __total_price), __total_font, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "รวม " + string.Format(_formatNumber_no_decimal, __totals) + " รายการ", __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 1, __doc_date_screen, __total_font, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Expose_Item_price)
                {
                    string __item_code = "";
                    int __no = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["code"].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __no++;
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["name_1"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["name_eng_1"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, _check_value_is_zero(__dr[__i]["last_price"].ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, string.Format(_formatNumber, __dr[__i]["from_qty"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, string.Format(_formatNumber, __dr[__i]["to_qty"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, string.Format(_formatNumber, _check_value_is_zero(__dr[__i]["sale_price1"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 9, string.Format(_formatNumber, _check_value_is_zero(__dr[__i]["sale_price2"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 10, string.Format(_formatNumber, _check_value_is_zero(__dr[__i]["sale_price3"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 11, string.Format(_formatNumber, _check_value_is_zero(__dr[__i]["sale_price4"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                            DataRow[] __dr2 = _dt_head.Select("code = \'" + __item_code + "\'");  // สำหรับ โปรโมชัน
                            string __promotion_code = "";

                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {

                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["promotion"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["unit_promotion"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["from_qty_promotion"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["to_qty_promotion"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                string __from_date_promotion = MyLib._myGlobal._convertDateFromQuery(__dr[__j]["from_date_promotion"].ToString()).ToShortDateString();
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, __from_date_promotion, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                string __to_date_promotion = MyLib._myGlobal._convertDateFromQuery(__dr[__j]["to_date_promotion"].ToString()).ToShortDateString();
                                this._view1._addDataColumn(_objReport2, __dataObject2, 9, __to_date_promotion, __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 10, __dr2[__j]["ar_group"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);


                            }
                        }
                        if (__i == __dr.Length - 1)
                        {
                            SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                        }
                    }
                }

                else if (_icType == SMLERPReportTool._reportEnum.TransferItem_between_Warehouse_by_output)
                {
                    int __no = 0;
                    //int __total_qty = 0;
                    double __total_qty = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        __no++;
                        //__total_qty += MyLib._myGlobal._intPhase((__dr[__i]["qty"].ToString());
                        __total_qty += Double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"));
                        SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(_objReport, __dataObject);
                        if (_condition._display_serial.Equals("1"))
                        {
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["from_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["from_shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["to_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["to_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 7, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 9, string.Format(_formatNumber, _check_value_is_zero(__dr[__i]["qty"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                            this._view1._addDataColumn(_objReport, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        }
                        else //if (_condition._display_serial.Equals("1"))
                        {
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["from_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["from_shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["to_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["to_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, __dr[__i]["serial"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 8, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 9, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr, __i, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        }

                        if (__i == __dr.Length - 1)
                        {
                            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 9, string.Format(_formatNumber, _check_value_is_zero(__total_qty.ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 9, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal), __total_qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.TransferItem_between_and_Detail)
                {
                    string __doc_no = "";
                    int __totals = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {
                            __totals++;
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["department"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // แผนก
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 6, string.Format(_formatNumber, __dr[__i]["qty_docno"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                            DataRow[] __dr2 = _dt_head.Select("doc_no = \'" + __doc_no + "\'");
                            string __item_code = "";
                            int __no = 0;
                            int __total_qty = 0;
                            double __total_sum_of_cost = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                string __item_code2 = __dr2[__j]["item_code"].ToString();
                                if (__item_code != __item_code2)
                                {
                                    __item_code = __item_code2;
                                    __no++;
                                    __total_qty += MyLib._myGlobal._intPhase(__dr2[__j]["qty_item"].ToString());
                                    __total_sum_of_cost += double.Parse(__dr2[__j]["sum_of_cost"].ToString());
                                    SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                    if (_condition._display_serial == false)
                                    {
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["from_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// โอนจากคลัง
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["from_shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// โอนจากที่เก็บ
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["to_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // โอนเข้าคลัง
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["to_shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // โอนเข้าที่เก็บ
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 6, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // หน่วยนับ
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        //this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["qty_item"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                        //this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["sum_of_cost"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty_item"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                    }
                                    else if (_condition._display_serial == true)
                                    {
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["serial"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["from_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// โอนจากคลัง
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["from_shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// โอนจากที่เก็บ
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["to_warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // โอนเข้าคลัง
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 6, __dr2[__j]["to_shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // โอนเข้าที่เก็บ
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 7, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // หน่วยนับ
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                        //this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["qty_item"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                        //this._view1._addDataColumn(_objReport2, __dataObject2, 10, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["sum_of_cost"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);  // ต้นทุนทั้งสิ้น
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty_item"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                        this._view1._addDataColumn(_objReport2, __dataObject2, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // ต้นทุนทั้งสิ้น
                                    }
                                }
                                //if (__j == __dr2.Length - 1)
                                //{
                                //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber,_check_value_is_zero(__total_qty.ToString("##.##"))), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber,_check_value_is_zero(__total_sum_of_cost.ToString("##.##"))), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //}
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "รายงาน " + string.Format(_formatNumber_no_decimal, __totals) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            //this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["department"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text); // แผนก
                            //this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 6, string.Format(_formatNumber, __dr[__i]["qty_docno"]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Import_Stock_Item)
                {
                    string __item_code = "";
                    int __no = 0;
                    //int __sum_qty = 0;
                    double __sum_qty = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();


                        if (!__item_code.Equals(__item_code2))
                        {
                            __no++;
                            __item_code = __item_code2;

                            //  รวมจำนวน  ในแต่ละสินค้า
                            DataRow[] __dr2 = _dt_head.Select("item_code = '" + __item_code + "'");
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                //__sum_qty += MyLib._myGlobal._intPhase(__dr2[__j]["qty"].ToString());
                                __sum_qty += Double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));
                            }

                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, (__i + 1).ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, __dr[__i]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 4, __dr[__i]["branch"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, __dr[__i]["warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // รับภายในแผนก
                            this._view1._addDataColumn(_objReport, __dataObject, 6, __dr[__i]["shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            // this._view1._addDataColumn(_objReport, __dataObject, 7, _check_value_is_zero(__sum_qty.ToString()), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            this._view1._addDataColumn(_objReport, __dataObject, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal), __sum_qty), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            if (__i == __dr.Length - 1)
                            {
                                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject);
                                this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 1, "รวม " + string.Format(_formatNumber_no_decimal, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport, __dataObject, 7, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                            }

                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Record_Total_Item_First_Year)
                {
                    string __doc_no = "";
                    int __no_per_page = 0;
                    // int __qty_per_page = 0;
                    double __qty_per_page = 0;
                    double __total_cost_per_page = 0;
                    double __total_price_per_page = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __remark = "";
                        if (__dr[__i]["remark"].ToString() != "null") __remark = __dr[__i]["remark"].ToString();
                        string __doc_no2 = __dr[__i]["doc_no"].ToString();
                        if (!__doc_no.Equals(__doc_no2))
                        {
                            __doc_no = __doc_no2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__i]["doc_date"].ToString()).ToShortDateString();
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 2, __dr[__i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);//หมายเหตุ

                            DataRow[] __dr2 = _dt_head.Select("doc_no = \'" + __doc_no + "\'");
                            int __no = 0;
                            //int __qty = 0;
                            double __qty = 0;
                            double __total_cost = 0;
                            double __total_price = 0;


                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                __no_per_page++;
                                ////if (__dr2[__j]["qty"].ToString() != "" || __dr2[__j]["qty"].ToString().Length > 0)
                                ////{
                                ////    __qty += MyLib._myGlobal._intPhase(__dr2[__j]["qty"].ToString());
                                ////    __qty_per_page += MyLib._myGlobal._intPhase(__dr2[__j]["qty"].ToString());
                                ////}
                                ////if (__dr2[__j]["cost"].ToString() != "" || __dr2[__j]["cost"].ToString().Length > 0)
                                ////{
                                ////    __total_cost += double.Parse(__dr2[__j]["cost"].ToString());
                                ////    __total_cost_per_page += double.Parse(__dr2[__j]["cost"].ToString());
                                ////}
                                ////if (__dr2[__j]["sum_of_cost"].ToString() != "" || __dr2[__j]["sum_of_cost"].ToString().Length > 0)
                                ////{
                                ////    __total_price += double.Parse(__dr2[__j]["sum_of_cost"].ToString());
                                ////    __total_price_per_page += double.Parse(__dr2[__j]["sum_of_cost"].ToString());
                                ////}
                                if (__dr2[__j]["qty"].ToString() != "" || __dr2[__j]["qty"].ToString().Length > 0)
                                {
                                    __qty += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "qty"));
                                    __qty_per_page += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "qty"));
                                }
                                if (__dr2[__j]["cost"].ToString() != "" || __dr2[__j]["cost"].ToString().Length > 0)
                                {
                                    __total_cost += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "cost"));
                                    __total_cost_per_page += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "cost"));
                                }
                                if (__dr2[__j]["sum_of_cost"].ToString() != "" || __dr2[__j]["sum_of_cost"].ToString().Length > 0)
                                {
                                    __total_price += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr2, __j, "sum_of_cost"));
                                    __total_price_per_page += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr2, __j, "sum_of_cost"));
                                }

                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, __dr2[__j]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // คลังสินค้า
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // ที่เก็บ
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String); // หน่วยนับ
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["qty"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["cost"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["sum_of_cost"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);

                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                //if (__j == __dr2.Length - 1)
                                //{
                                //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "รวม " + string.Format(_formatNumber, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(_formatNumber, __qty), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __total_cost), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 7,string.Format(_formatNumber, __total_price), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //}
                            }
                            if (__i == __dr.Length - 1)
                            {

                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, "รวมรายการ/หน้า " + string.Format(_formatNumber_no_decimal, __no_per_page) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(_formatNumber, __qty_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(_formatNumber, __total_cost_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, __total_price_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal), __qty_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal), __total_cost_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal), __total_price_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            }
                        }
                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Item_Material_Balance_Bring)
                {
                    string __item_code = "";
                    int __no_per_page = 0;
                    //int __total_qty_per_page = 0;
                    double __total_qty_per_page = 0;
                    double __total_cost_per_page = 0;
                    double __total_sum_of_cost_per_page = 0;

                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, __dr[__i]["item_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสสินค้า
                            this._view1._addDataColumn(_objReport, __dataObject, 1, __dr[__i]["item_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ชื่อสินค้า
                            DataRow[] __dr2 = _dt_head.Select("item_code = \'" + __item_code + "\'");
                            int __no = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                __no_per_page++;

                                ////if (__dr2[__j]["qty"].ToString() != "" || __dr2[__j]["qty"].ToString().Length > 0)
                                ////{
                                ////    __total_qty_per_page += MyLib._myGlobal._intPhase(__dr2[__j]["qty"].ToString());
                                ////}

                                ////if (__dr2[__j]["cost"].ToString() != "" || __dr2[__j]["cost"].ToString().Length > 0)
                                ////{
                                ////    __total_cost_per_page += double.Parse(__dr2[__j]["cost"].ToString());
                                ////}
                                ////if (__dr2[__j]["sum_of_cost"].ToString() != "" || __dr2[__j]["sum_of_cost"].ToString().Length > 0)
                                ////{
                                ////    __total_sum_of_cost_per_page += double.Parse(__dr2[__j]["sum_of_cost"].ToString());
                                ////}
                                if (__dr2[__j]["qty"].ToString() != "" || __dr2[__j]["qty"].ToString().Length > 0)
                                {
                                    __total_qty_per_page += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"));
                                }

                                if (__dr2[__j]["cost"].ToString() != "" || __dr2[__j]["cost"].ToString().Length > 0)
                                {
                                    __total_cost_per_page += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "cost"));
                                }
                                if (__dr2[__j]["sum_of_cost"].ToString() != "" || __dr2[__j]["sum_of_cost"].ToString().Length > 0)
                                {
                                    __total_sum_of_cost_per_page += double.Parse(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr2, __j, "sum_of_cost"));
                                }
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                string __doc_date = MyLib._myGlobal._convertDateFromQuery(__dr[__j]["doc_date"].ToString()).ToShortDateString();
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, __doc_date, __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ;วันที่เอกสาร
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, __dr2[__j]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // เลขที่เอกสาร
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, __dr2[__j]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // คำอธิบาย
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, __dr2[__j]["department"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);//แผนก
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, __dr2[__j]["warehouse"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// คลังสินค้า
                                this._view1._addDataColumn(_objReport2, __dataObject2, 5, __dr2[__j]["shelf"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// ที่เก็บ
                                this._view1._addDataColumn(_objReport2, __dataObject2, 6, __dr2[__j]["unit"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 5, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);// หน่วยนับ
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["qty"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["cost"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                ////this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, _check_value_is_zero(__dr2[__j]["sum_of_cos"].ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __dr2, __j, "qty"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_price_decimal, __dr2, __j, "cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dr2, __j, "sum_of_cost"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                //if (__j == __dr2.Length - 1)
                                //{
                                //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, "รวม " + string.Format(_formatNumber, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 7, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 8, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 9, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                                //}
                            }

                        }
                        if (__i == __dr.Length - 1)
                        {
                            SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport2, __dataObject2);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 2, "รวมรายการ/หน้า " + string.Format(_formatNumber, __no_per_page) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 5, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                            ////this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(_formatNumber, _check_value_is_zero(__total_qty_per_page.ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                            ////this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(_formatNumber, _check_value_is_zero(__total_cost_per_page.ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                            ////this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(_formatNumber, _check_value_is_zero(__total_sum_of_cost_per_page.ToString())), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 7, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_qty_decimal), __total_qty_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 8, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_price_decimal), __total_cost_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            this._view1._addDataColumn(_objReport2, __dataObject2, 9, string.Format(MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal), __total_sum_of_cost_per_page), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);

                        }

                    }
                }
                else if (_icType == SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial)
                {
                    string __item_code = "";
                    int __no = 0;
                    int __totals = 0;
                    for (int __i = 0; __i < __dr.Length; __i++)
                    {
                        string __item_code2 = __dr[__i]["item_code"].ToString();
                        if (!__item_code.Equals(__item_code2))
                        {
                            __totals++;
                            __item_code = __item_code2;
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            this._view1._addDataColumn(_objReport, __dataObject, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสสินค้า
                            this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ชื่อสินค้า
                            this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ชื่อหน่วยนับ
                            this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // จำนวน
                            this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);  // มูลค่าคงเหลือ

                            DataRow[] __dr2 = _dt_head.Select("item_code = \'" + __item_code + "\'");
                            int __total_qty = 0;
                            for (int __j = 0; __j < __dr2.Length; __j++)
                            {
                                __no++;
                                SMLReport._report._objectListType __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // รหัสคบวคุมทะเบียนสินค้า
                                this._view1._addDataColumn(_objReport2, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // หมายเลขทะเบียน
                                this._view1._addDataColumn(_objReport2, __dataObject2, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // คลัง
                                this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);  // ที่เก็บ

                                //if (__j == __dr2.Length - 1)
                                //{
                                //    __dataObject2 = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //    this._view1._createEmtryColumn(_objReport2, __dataObject2);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 1, "รวม " + string.Format(_formatNumber, __no) + " รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 2, string.Format(_formatNumber, __total_qty), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Double);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //    this._view1._addDataColumn(_objReport2, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                //}
                            }
                        }
                        if (__i == (__dr.Length - 1))
                        {
                            SMLReport._report._objectListType __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            this._view1._createEmtryColumn(_objReport, __dataObject);
                            //this._view1._addDataColumn(_objReport, __dataObject, 0, "รวม "+string.Format(_formatNumber_no_decimal,__totals)+" รายการ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);  // รหัสสินค้า
                            //this._view1._addDataColumn(_objReport, __dataObject, 1, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);  // ชื่อสินค้า
                            //this._view1._addDataColumn(_objReport, __dataObject, 2, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 3, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);  // ชื่อหน่วยนับ
                            //this._view1._addDataColumn(_objReport, __dataObject, 4, "", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            //this._view1._addDataColumn(_objReport, __dataObject, 5, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);  // จำนวน
                            //this._view1._addDataColumn(_objReport, __dataObject, 6, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);  // มูลค่าคงเหลือ
                        }
                    }
                }
            }
            catch (Exception __e)
            {
                string __error = __e.Message;
            }
        }


        private string[] _ic_column_order_by()
        {
            string[] __result = null;


            if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดสินค้า)
            {
                __result = new string[]{ _g.d.ic_inventory_detail._table+"."+_g.d.ic_inventory_detail._ic_code,
                                //_g.d.ic_inventory._table+"."+_g.d.ic_inventory._name_1,
                                //_g.d.ic_inventory._table+"."+_g.d.ic_inventory._name_2,
                                //_g.d.ic_inventory._table+"."+_g.d.ic_inventory._description,
                                //_g.d.ic_inventory._table+"."+_g.d.ic_inventory._unit_standard,
                                //_g.d.ic_inventory._table+"."+_g.d.ic_inventory._cost_type,
                                //_g.d.ic_inventory._table+"."+_g.d.ic_inventory._status,
                               //_g.d.ic_unit_use._table+"."+_g.d.ic_unit_use._code,
                               //_g.d.ic_unit_use._table+"."+_g.d.ic_unit_use._name_1
                                };
            }
            if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดบาร์โค๊ด)
            {
                __result = new string[] { _g.d.ic_inventory_detail._ic_code, };
            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_by_serial)
            {
                /*__result = new string[] { _g.d.ic_inventory._table+"."+_g.d.ic_inventory._code,
                                _g.d.ic_inventory._table+"."+_g.d.ic_inventory._name_1
                                };*/
                __result = new string[] { _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_code,
                                _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_name
                                };
            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_status)
            {
                __result = new string[] { _g.d.ic_inventory._table+"."+_g.d.ic_inventory._code,
                                _g.d.ic_inventory._table+"."+_g.d.ic_inventory._name_1
                                };
            }
            else if (_icType == SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ)
            {
                __result = new string[] { _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_code
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_name
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_no
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_date
                                };
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป)
            {
                //__result = new string[] { _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_code
                //                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_name
                //                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_no
                //                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_date
                //                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._price
                //                };
            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Import_Item_ready)
            {
                __result = new string[] { _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_code
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_name
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_no
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_date
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._price
                                };
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา)
            {
                __result = new string[] { _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_code
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_name
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_date
                                ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_no
                                };
            }
            //else if ((_icType == _g.g._icreportEnum.Refunded_Withdraw_Item_Staple) || (_icType == _g.g._icreportEnum.Withdraw_Item_Staple))
            //{
            //    __result = new string[] { _g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_code
            //                    ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._item_name
            //                    ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_date
            //                    ,_g.d.ic_trans_detail._table+"."+_g.d.ic_trans_detail._doc_no
            //                    };
            //}


            return __result;
        }

        private void _config()
        {
            this.__query_head = new StringBuilder();
            this.__query_detail_1 = new StringBuilder();
            this.__query_detail_2 = new StringBuilder();

            if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดสินค้า)
            {
                // รายละเอียดสินค้า
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_detail, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_detail)._str;


                string __myQuery = "";


                int[] __width = { 13, 15, 10, 8, 10, 10, 10 };
                string[] __column = { MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                        , "*" + MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard)._str
                                         , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type)._str + ""
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern)._str + ""
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type)._str + ""
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type)._str + "" };

                int[] __width_2 = { 74, 13, 13 };
                string[] __column_2 = { ""
                                        
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf)._str
                                      };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __k = 0; __k < __column_2.Length; __k++)
                {
                    this._width_2.Add(__width_2[__k]);
                    this._column_2.Add(__column_2[__k]);
                }
                //for (int __j = 0; __j < __column_3.Length; __j++)
                //{
                //    this._width_3.Add(__width_3[__j]);
                //    this._column_3.Add(__column_3[__j]);
                //}  item_pattern

                this.__query_head.Append(" select code as item_code ");
                this.__query_head.Append(" , name_1  as item_name  ");
                this.__query_head.Append(" , item_pattern ||'~'||(select name_1 from ic_pattern where ic_pattern.code = item_pattern ) as item_pattern    ");
                this.__query_head.Append(" , unit_standard ||'~'||( select name_1 from ic_unit where ic_unit.code = unit_standard  ) as unit_standard_name   ");
                this.__query_head.Append(" , item_type  as item_type   ");
                this.__query_head.Append(" , tax_type  ");
                this.__query_head.Append(" , cost_type  ");
                this.__query_head.Append(" , unit_type  ");
                this.__query_head.Append(" from ic_inventory ");
                this.__query_head.Append(" where item_type  not in (3 , 5)  ");
                this.__query_head.Append(" order by item_code  ");



                // unit
                this.__query_detail_1.Append(" select code as unit_code ");
                this.__query_detail_1.Append(" , ( select name_1 from ic_unit where ic_unit.code = ic_unit_use.code ) as unit_name  ");
                this.__query_detail_1.Append(" , ic_code ");
                this.__query_detail_1.Append(" from ic_unit_use ");

                // warehouse and shelf

                this.__query_detail_2.Append(" select ");
                this.__query_detail_2.Append(" (( wh_code ) ||'~'|| ( select name_1 from ic_warehouse where ic_warehouse.code = ic_wh_shelf.wh_code )) as wh_name ");
                this.__query_detail_2.Append(" ,(( shelf_code ) ||'~'|| ( select name_1 from ic_shelf where ic_shelf.code = ic_wh_shelf.shelf_code )) as shelf_name ");
                this.__query_detail_2.Append(" , ic_code ");
                this.__query_detail_2.Append(" from ic_wh_shelf ");
                this.__query_detail_2.Append(" where " + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._ic_code + " in (select code from ic_inventory where item_type not in (3 , 5))");

            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_by_supplier)
            { // ไม่มีในรายงาน
                // รายละเอียดสินค้าตามเจ้าหนี้
                //this._report_name = "รายละเอียดสินค้าตามเจ้าหนี้";
                //int[] __width = { 10, 15 };
                //string[] __column = { "รหัส Supplier", "ชื่อ Supplier" };
                //int[] __width_2 = { 10, 10, 15, 10, 10, 10, 10, 10, 10 };
                //string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "", "", "หน่วยนับมาตรฐาน", "ประเภทต้นทุน", "", "คำนวณภาษี" };
                //int[] __width_3 = { 10, 10, 10, 10, 10, 10, 10 };
                //string[] __column_3 = { "", "", "ชื่อหน่วยนับ", "", "ตัวตั้ง", "ตัวหาร", "อัตราส่วน" };
                //this._level = 3;
                //for (int __i = 0; __i < __column.Length; __i++)
                //{
                //    this._width.Add(__width[__i]);
                //    this._column.Add(__column[__i]);
                //}
                //for (int __j = 0; __j < __column_2.Length; __j++)
                //{
                //    this._width_2.Add(__width_2[__j]);
                //    this._column_2.Add(__column_2[__j]);
                //}
                //for (int __k = 0; __k < __column_3.Length; __k++)
                //{
                //    this._width_3.Add(__width_3[__k]);
                //    this._column_3.Add(__column_3[__k]);
                //}
                //this.__query = new StringBuilder();
                //__query.Append(" select (select code  from ic_inventory where code = ic_unit_use.ic_code)  as " + _item_code + ",");
                //__query.Append(" (select name_1  from ic_inventory where code = ic_unit_use.ic_code) as " + _item_name + ",");
                //__query.Append(" (select tax_type  from ic_inventory where code = ic_unit_use.ic_code) as " + _tax_type + ",");
                //__query.Append(" (select cost_type  from ic_inventory where code = ic_unit_use.ic_code) as " + _cost_type + " ,");
                //__query.Append(" (select name_1 from ic_unit where code = (select unit_standard  from ic_inventory where code = ic_unit_use.ic_code)) as " + _unit_standard + ",");
                //__query.Append(" name_1 as unit_detail , stand_value , divide_value , ratio , ");
                //__query.Append(" (select ap_code from ap_item_by_supplier where ic_code = ic_unit_use.ic_code)as " + _ap_code + " , ");
                //__query.Append(" (select name_1 from ap_supplier where code = (select ap_code from ap_item_by_supplier where  ic_code = ic_unit_use.ic_code)) as " + _ap_name);
                //__query.Append(" from ic_unit_use");
                //__query.Append(" where ic_code <> '' and  ");
                //__query.Append(" (select ap_code from ap_item_by_supplier where ic_code = ic_unit_use.ic_code) <> '' ");
                //__query.Append(" order by ap_code asc");

            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_by_serial)
            {
                // รายละเอียดสินค้าแบบมี Serial
                this._report_name = "รายละเอียดสินค้าแบบมี Serial";
                int[] __width = { 10, 15 };
                string[] __column = { _g.d.resource_report_ic_column._item_code
                                        , _g.d.resource_report_ic_column._item_name 
                                    };
                int[] __width_2 = { 10, 15, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column_2 = { "รหัสทะเบียนกลุ่ม"
                                          , "รายละเอียดเฉพาะ"
                                          , "หมายเลขตัวถัง"
                                          , "หมายเลขเครื่อง"
                                          , "พิมพ์ที่"
                                          , "วันที่รับเข้า"
                                          , "วันหมดประกัน"
                                          , "สถานะปัจจุบัน"
                                          , "สถานะสินค้า" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();
                // string __where = _condition.__where;
                this.__query_head.Append(" select distinct " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name);
                //this.__query.Append(" , (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_inventory._name_1);  // item name
                this.__query_head.Append(" , (select " + _g.d.ic_serial._serial_number + " from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_serial._serial_number); // serial number
                this.__query_head.Append(" , (select " + _g.d.ic_serial._description + " from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_serial._description); // description
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._trans_flag);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_date);
                //, (select item_type from ic_inventory where code = item_code) as item_type
                this.__query_head.Append(" , (select " + _g.d.ic_inventory._status + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_inventory._status);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in  (18,44,45,46,47,50,54,56,60)) and (" + _g.d.ic_trans_detail._trans_flag + " not in (19,55,61))");
                this.__query_head.Append(" and (select " + _g.d.ic_serial._serial_number + " from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") <> ''");
                // __query.Append(__where);
                // this.__query.Append(" order by  "+_g.d.ic_trans_detail._item_code+" , "+_g.d.ic_trans_detail._doc_date+" desc ");
                //__query.Append(" order by " + _g.d.ic_serial._ic_code);

                // ok 80%
            }
            else if (_icType == SMLERPReportTool._reportEnum.Serial_number)
            {
                // รายงานเคลื่อนไหว Serial Number  พี่ขาว
                this._report_name = "รายงานเคลื่อนไหว Serial Number";
                int[] __width = { 10, 10, 10, 15, 10, 10, 10, 10 };
                string[] __column = { "Serial No", "หมายเลขตัวถัง", "รหัสสินค้า", "ชื่อสินค้า", "", "หมายเลขทะเบียน", "วันที่รับสินค้า", "พนักงานรับสินค้า" };
                int[] __width_2 = { 10, 15, 15, 15, 10, 10 };
                string[] __column_2 = { "วันที่เอกสาร", "เลขที่เอกสาร", "หมายเหตุ", "", "คลัง", "พื้นที่เก็บ" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();
                this.__query_head.Append(" select * from (");
                this.__query_head.Append("select " + _g.d.ic_serial._serial_number + " , " + _g.d.ic_serial._ic_code);
                this.__query_head.Append(" , (select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code + ") as " + _g.d.ic_resource._item_name);
                this.__query_head.Append(" , " + _g.d.ic_serial._description);
                this.__query_head.Append(" , case  when " + _g.d.ic_serial._status + "= 0 then '" + MyLib._myGlobal._resource("ในสต๊อค") + "' else '" + MyLib._myGlobal._resource("ในสต๊อค") + "' end as " + _g.d.ic_serial._status);
                this.__query_head.Append(" , (select " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._doc_date + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + " and " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + " = 12 limit 1) as " + _g.d.resource_report._date_import);
                this.__query_head.Append(", (select " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._price + " from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number + " and " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._trans_flag + " = 12 limit 1) as " + _g.d.ic_resource._ic_cost + "");
                this.__query_head.Append(" from " + _g.d.ic_serial._table);
                this.__query_head.Append(" ) as temp1 ");
                // detail
                this.__query_detail_1.Append("select " + _g.d.ic_trans_serial_number._doc_date + "," + _g.d.ic_trans_serial_number._doc_no + "," + _g.d.ic_trans_serial_number._trans_flag + "," + _g.d.ic_trans_serial_number._wh_code + "," + _g.d.ic_trans_serial_number._shelf_code);
                this.__query_detail_1.Append(" , case when " + _g.d.ic_trans_serial_number._trans_flag + " in (44) then ( select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where  " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans_serial_number._cust_code + ") else '' end as " + _g.d.ic_resource._ar_detail + "");
                this.__query_detail_1.Append(" , (select r." + _g.d.ic_trans_serial_number._doc_date + " from " + _g.d.ic_trans_serial_number._table + " as r where r." + _g.d.ic_trans_serial_number._serial_number + " = " + _g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number + " and r." + _g.d.ic_trans_serial_number._trans_flag + " = 12 limit 1) as " + _g.d.resource_report._date_import);
                this.__query_detail_1.Append(" from " + _g.d.ic_trans_serial_number._table);

                //__query.Append(" select (select serial_number from ic_serial where ic_code = ic_trans_detail.item_code) as serial_number  ");
                //__query.Append(" , (select body_no from ic_serial where ic_code = ic_trans_detail.item_code) as body_no  ");
                //__query.Append(" , (select ic_code from ic_serial where ic_code = ic_trans_detail.item_code) as item_code ");
                //__query.Append(" , (select name_1 from ic_inventory where code = ic_trans_detail.item_code) as item_name ");
                //__query.Append(" , (select license from ic_serial where ic_code = ic_trans_detail.item_code) as item_license  ");
                //__query.Append(" , due_date ");
                //__query.Append(" , (select name_1 from ar_customer where code =cust_code) as customer_name ");
                //__query.Append("  , doc_date ");
                //__query.Append(" , doc_no  ");
                //__query.Append(" , (select description from ic_serial where ic_code = item_code) as description ");
                //__query.Append(" , (select name_1 from ic_warehouse where code = (select wh_code from ic_wh_shelf where ic_code = item_code)) as warehouse ");
                //__query.Append(" , (select name_1 from ic_shelf where code = (select shelf_code from ic_wh_shelf where ic_code = item_code)) as shelf ");
                //__query.Append(" from ic_trans_detail");
                //__query.Append(" where (select ic_code from ic_serial where ic_code = ic_trans_detail.item_code) <> ''");
                //__query.Append(" order by item_code , due_date");


            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดบาร์โค๊ด)
            {
                //   รายงานบาร์โค้ดสินค้า
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_barcode, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_barcode)._str; ;
                int[] __width = { 15, 15, 10, 10, 10, 10, 10 };
                string[] __column = { MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                        , "*"+MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type)._str+"*" };
                int[] __width_2 = { 84, 8, 8 };
                string[] __column_2 = { ""
                                          , "บาร์โค้ด"+"*"
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str+"*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select  ( select code from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code ) as item_code ");
                this.__query_head.Append(" , ( select name_1 from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code ) as item_name   ");
                this.__query_head.Append(" , (( select unit_standard from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code )||'~'||( select name_1 from ic_unit where ic_unit.code = ( select unit_standard from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code ) )) as unit_standard   ");
                this.__query_head.Append(" , (( select item_pattern from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code )||'~'||( select name_1 from ic_pattern where ic_pattern.code = ic_inventory_barcode.ic_code )) as item_pattern   ");
                this.__query_head.Append(" , ( select item_type from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code ) as item_type   ");
                this.__query_head.Append(" , ( select tax_type from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code ) as tax_type   ");
                this.__query_head.Append(" , ( select cost_type from ic_inventory where ic_inventory.code = ic_inventory_barcode.ic_code ) as cost_type   ");
                this.__query_head.Append(" , barcode   ");
                this.__query_head.Append(" , unit_code||'~'||( select name_1 from ic_unit where code = ic_inventory_barcode.unit_code ) as unit   ");
                this.__query_head.Append(" from ic_inventory_barcode  ");
                this.__query_head.Append(" order by item_code  ");
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสูตรสีผสม)
            {
                //  รายงานสูตรสีผสม
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_color_mixing, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_color_mixing)._str;
                int[] __width = { 20, 20, 12, 12, 12, 12, 12 };
                string[] __column = {  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._color_mixing_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._color_mixing_code)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._color_mixing_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._color_mixing_name)._str
                                        , "*"+MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type)._str+"*" 
                                    };
                int[] __width_2 = { 5, 20, 25, 10, 10, 10, 12, 8 };
                string[] __column_2 = { ""
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price)._str+"*"
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price)._str+ "*"
             ,""      
             };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                //this.__query_head = new StringBuilder();
                //this.__query_head.Append(" select ");
                //this.__query_head.Append(" code as item_code   ");
                //this.__query_head.Append(" , name_1 as item_name  ");
                //this.__query_head.Append(" , unit_standard ||'~'||( select name_1 from ic_unit where ic_unit.code = unit_standard ) as unit_standard  ");
                //this.__query_head.Append(" , item_pattern ||'~'||( select name_1 from ic_pattern where ic_pattern.code = item_pattern ) as item_pattern  ");
                //this.__query_head.Append(" , item_type as item_type ");
                //this.__query_head.Append(" , tax_type as tax_type ");
                //this.__query_head.Append(" , cost_type as cost_type ");
                //this.__query_head.Append(" from ic_inventory ");
                //this.__query_head.Append(" where item_type in (5) ");
                //this.__query_head.Append(" order by item_code ");

                //this.__query_detail_1 = new StringBuilder();
                //this.__query_detail_1.Append(" select ic_set_code , ic_code ");
                //this.__query_detail_1.Append(" ,  ( select name_1 from ic_inventory where code = ic_inventory_set_detail.ic_code ) as ic_name ");
                //this.__query_detail_1.Append(" , unit_code||'~'||( select name_1 from ic_unit where ic_unit.code = ic_inventory_set_detail.unit_code ) as unit ");
                //this.__query_detail_1.Append(" , qty  ");
                //this.__query_detail_1.Append(" , price ");
                //this.__query_detail_1.Append(" , sum_amount ");
                //this.__query_detail_1.Append(" from ic_inventory_set_detail ");
                //this.__query_detail_1.Append(" where status = 1 ");
                //this.__query_detail_1.Append(" order by ic_set_code , line_number ");


                // old query 
                this.__query_head.Append(" select ");
                this.__query_head.Append(" ( select code from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as item_code ");
                this.__query_head.Append(" , ( select name_1 from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as item_name ");
                this.__query_head.Append(" , (( select unit_standard from ic_inventory where code = ic_inventory_set_detail.ic_set_code )||'~'||( select name_1 from ic_unit where ic_unit.code = ( select unit_standard from ic_inventory where ic_inventory.code = ic_inventory_set_detail.ic_set_code ) )) as unit_standard ");
                this.__query_head.Append(" , (( select item_pattern from ic_inventory where code = ic_inventory_set_detail.ic_set_code )||'~'||( select name_1 from ic_pattern where ic_pattern.code = ( select item_pattern from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) )) as item_pattern ");
                this.__query_head.Append(" , ( select item_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code  ) as item_type ");
                this.__query_head.Append(" , ( select tax_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as tax_type ");
                this.__query_head.Append(" , ( select cost_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as cost_type ");
                this.__query_head.Append(" , ic_code ");
                this.__query_head.Append(" ,  ( select name_1 from ic_inventory where code = ic_inventory_set_detail.ic_code ) as ic_name ");
                this.__query_head.Append(" , unit_code||'~'||( select name_1 from ic_unit where ic_unit.code = ic_inventory_set_detail.unit_code ) as unit ");
                this.__query_head.Append(" , qty , price , sum_amount ");
                this.__query_head.Append(" from ic_inventory_set_detail ");
                this.__query_head.Append(" where ( select item_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) = 5 ");
                this.__query_head.Append("  order by ic_set_code , line_number ");
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสูตรสินค้าชุด)
            {
                //  รายงานสูตรสินค้าชุด
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_item_set, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_item_set)._str; ;
                int[] __width = { 20, 20, 12, 12, 12, 12, 12 };
                string[] __column = {  "รหัสสินค้าชุด"
                                        , "ชื่อสินค้าชุด"
                                        , "*"+MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._tax_type)._str+"*"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._cost_type)._str+"*" 
                                    };
                int[] __width_2 = { 5, 20, 25, 10, 10, 10, 12, 8 };
                string[] __column_2 = { ""
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price)._str+"*"
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price)._str+ "*"
                                     ,"" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }

                this.__query_head = new StringBuilder();
                //this.__query_head.Append(" select ");
                //this.__query_head.Append(" code as item_code   ");
                //this.__query_head.Append(" , name_1 as item_name  ");
                //this.__query_head.Append(" , unit_standard ||'~'||( select name_1 from ic_unit where ic_unit.code = unit_standard ) as unit_standard  ");
                //this.__query_head.Append(" , item_pattern ||'~'||( select name_1 from ic_pattern where ic_pattern.code = item_pattern ) as item_pattern  ");
                //this.__query_head.Append(" , item_type as item_type ");
                //this.__query_head.Append(" , tax_type as tax_type ");
                //this.__query_head.Append(" , cost_type as cost_type ");
                //this.__query_head.Append(" from ic_inventory ");
                //this.__query_head.Append(" where item_type = 3 ");
                //this.__query_head.Append(" order by item_code "); 

                //this.__query_detail_1 = new StringBuilder();
                //this.__query_detail_1.Append(" select ic_set_code , ic_code ");
                //this.__query_detail_1.Append(" ,  ( select name_1 from ic_inventory where code = ic_inventory_set_detail.ic_code ) as ic_name ");
                //this.__query_detail_1.Append(" , unit_code||'~'||( select name_1 from ic_unit where ic_unit.code = ic_inventory_set_detail.unit_code ) as unit ");
                //this.__query_detail_1.Append(" , qty  ");
                //this.__query_detail_1.Append(" , price ");
                //this.__query_detail_1.Append(" , sum_amount ");
                //this.__query_detail_1.Append(" from ic_inventory_set_detail ");
                //this.__query_detail_1.Append(" order by ic_set_code , line_number ");

                // old query
                this.__query_head.Append(" select ");
                this.__query_head.Append(" ( select code from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as item_code ");
                this.__query_head.Append(" , ( select name_1 from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as item_name ");
                this.__query_head.Append(" , (( select unit_standard from ic_inventory where code = ic_inventory_set_detail.ic_set_code )||'~'||( select name_1 from ic_unit where ic_unit.code = ( select unit_standard from ic_inventory where ic_inventory.code = ic_inventory_set_detail.ic_set_code ) )) as unit_standard ");
                this.__query_head.Append(" , (( select item_pattern from ic_inventory where code = ic_inventory_set_detail.ic_set_code )||'~'||( select name_1 from ic_pattern where ic_pattern.code = ( select item_pattern from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) )) as item_pattern ");
                this.__query_head.Append(" , ( select item_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code  ) as item_type ");
                this.__query_head.Append(" , ( select tax_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as tax_type ");
                this.__query_head.Append(" , ( select cost_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) as cost_type ");
                this.__query_head.Append(" , ic_code ");
                this.__query_head.Append(" ,  ( select name_1 from ic_inventory where code = ic_inventory_set_detail.ic_code ) as ic_name ");
                this.__query_head.Append(" , unit_code||'~'||( select name_1 from ic_unit where ic_unit.code = ic_inventory_set_detail.unit_code ) as unit ");
                this.__query_head.Append(" , qty , price , sum_amount ");
                this.__query_head.Append(" from ic_inventory_set_detail ");
                this.__query_head.Append(" where ( select item_type from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) = 3 ");
                this.__query_head.Append("  order by ic_set_code , line_number ");
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานราคาขายสินค้า)
            {
                //  รายงานราคาขายสินค้า
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_sale_price, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_sale_price)._str; ;
                int[] __width = { 15, 26, 20, 15, 13 };
                string[] __column = {    MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                        //,_g.d.resource_report_ic_column._item_name_eng
                                        ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern)._str
                                        ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type)._str

                                    };
                //int[] __width_2 = { 32, 18, 15, 14, 12 };
                //string[] __column_2 = { "", "หน่วยนับ", "ประเภทการขาย", "ราคาแยกภาษี*", "ราคารวมภาษี*" };
                int[] __width_2 = { 30, 10, 14, 14, 7, 7, 9, 9 };
                string[] __column_2 = { ""
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._split_tax_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._split_tax_price)._str+"*"
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._include_tax_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._include_tax_price)._str+"*" 
                                          , "จากวันที่"+"*"
                                          , "ถึงวันที่"+"*"
                                          , "จากจำนวน"+"*"
                                          , "ถึงจำนวน"+"*"
                                      };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select  distinct ");
                this.__query_head.Append(" ic_code as item_code ");
                this.__query_head.Append(" , ( select name_1 from ic_inventory where code = ic_code ) as item_name_th ");
                this.__query_head.Append(" , ( select name_eng_1 from ic_inventory where code = ic_code ) as item_name_eng ");
                this.__query_head.Append(" , ( (select unit_standard from ic_inventory where code = ic_code)||'~'|| ( select name_1 from ic_unit where code = (select unit_standard from ic_inventory where code = ic_code) ) ) as unitstandard ");
                this.__query_head.Append(" , ( (select item_pattern from ic_inventory where code = ic_code)||'~'||( select name_1 from ic_pattern where code = (select item_pattern from ic_inventory where code = ic_code) ) ) as pattern  ");
                this.__query_head.Append(" , ( select item_type from ic_inventory where code = ic_code ) as item_type  ");
                this.__query_head.Append(" , sale_price1 , sale_price2 , from_date , to_date , from_qty , to_qty ");
                this.__query_head.Append(" ,unit_code||'~'||( select name_1 from ic_unit where code = unit_code )  as unit ");
                this.__query_head.Append(" , sale_type ");
                this.__query_head.Append(" from ic_inventory_price ");
                this.__query_head.Append(" order by item_code  ");
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานราคาซื้อสินค้า)
            {
                //  รายงานราคาซื้อสินค้า
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_purcharse_price, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_purcharse_price)._str; ;
                int[] __width = { 15, 26, 20, 15, 13 };
                string[] __column = {  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                        //,_g.d.resource_report_ic_column._item_name_eng
                                        ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit_standard)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_pattern)._str
                                        ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_type)._str


                                    };
                //int[] __width_2 = { 32, 18, 15, 14, 12 };
                //string[] __column_2 = { "", "หน่วยนับ", "ประเภทการขาย", "ราคาแยกภาษี*", "ราคารวมภาษี*" };
                int[] __width_2 = { 30, 10, 14, 14, 7, 7, 9, 9 };
                string[] __column_2 = { ""
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._split_tax_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._split_tax_price)._str+"*"
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._include_tax_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._include_tax_price)._str+"*" 
                                          , "จากวันที่"+"*"
                                          , "ถึงวันที่"+"*"
                                          , "จากจำนวน"+"*"
                                          , "ถึงจำนวน"+"*"
                                      };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select  distinct ");
                this.__query_head.Append(" ic_code as item_code ");
                this.__query_head.Append(" , ( select name_1 from ic_inventory where code = ic_code ) as item_name_th ");
                this.__query_head.Append(" , ( select name_eng_1 from ic_inventory where code = ic_code ) as item_name_eng ");
                this.__query_head.Append(" , ( (select unit_standard from ic_inventory where code = ic_code)||'~'|| ( select name_1 from ic_unit where code = (select unit_standard from ic_inventory where code = ic_code) ) ) as unitstandard ");
                this.__query_head.Append(" , ( (select item_pattern from ic_inventory where code = ic_code)||'~'||( select name_1 from ic_pattern where code = (select item_pattern from ic_inventory where code = ic_code) ) ) as pattern  ");
                this.__query_head.Append(" , ( select item_type from ic_inventory where code = ic_code ) as item_type  ");
                this.__query_head.Append(" , sale_price1 , sale_price2 , from_date , to_date , from_qty , to_qty ");
                this.__query_head.Append(" ,unit_code||'~'||( select name_1 from ic_unit where code = unit_code )  as unit ");
                this.__query_head.Append(" , sale_type ");
                this.__query_head.Append(" from ic_inventory_purchase_price ");
                this.__query_head.Append(" order by item_code ");
            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_Giveaway)
            {
                //  รายางานของแถม
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_giveaway, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_giveaway)._str; ;
                int[] __width = { 15, 20, 14, 14 };
                string[] __column = { "รหัสเงื่อนไข"
                                        ,"ชื่อเงื่อนไข"
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._date_begin, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._date_begin)._str
                                        ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._date_end, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._date_end)._str

                                    };
                int[] __width_2 = { 40, 20, 20, 15, 5 };
                string[] __column_2 = { ""
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                      };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select permium_code as condition_code ");
                this.__query_head.Append(" , ( select distinct name_1 from ic_purchase_permium where (permium_code = ic_purchase_permium_list.permium_code) ) as condition_name ");
                this.__query_head.Append(" , ( select distinct date_begin from ic_purchase_permium where (permium_code = ic_purchase_permium_list.permium_code)) as start_date  ");
                this.__query_head.Append(" , ( select distinct date_end from ic_purchase_permium where (permium_code = ic_purchase_permium_list.permium_code))  as stop_date  ");
                this.__query_head.Append(" ,( select code from ic_inventory where code = ic_code ) as item_code   ");
                this.__query_head.Append(" , ( select name_1 from ic_inventory where code = ic_code ) as item_name   ");
                this.__query_head.Append(" , unit_code ||'~'|| ( select name_1 from ic_unit where code = unit_code ) as unit  ");
                this.__query_head.Append(" , qty  ");
                this.__query_head.Append(" from ic_purchase_permium_list  ");
                this.__query_head.Append(" order by condition_code  ");

            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_status)
            {
                // รายงานสถานะสินค้า
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_status, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_status)._str; ;
                string __myQuery = "";
                int[] __width = { 10, 15 };
                string[] __column = { "กลุ่มสินค้า", "ชื่อกลุ่มสินค้า" };
                int[] __width_2 = { 15, 15, 32, 10, 10, 10, 6 };
                string[] __column_2 = { ""
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._import_first_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._import_first_date)._str
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._import_last_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._import_last_date)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._pay_last_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._pay_last_date)._str
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._different_day, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._different_day)._str+"*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                //string __where = _condition.__where;

                this.__query_head = new StringBuilder();

                __myQuery = "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._group_main + ",(select " + _g.d.ic_group._name_1 + " from " + _g.d.ic_group._table + " where " + _g.d.ic_group._table + "." + _g.d.ic_group._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._group_main + ") as group_name ";
                __myQuery += " , (select " + _g.d.ic_trans_detail._doc_date + " from " + _g.d.ic_trans_detail._table + " where (" + _g.d.ic_trans_detail._trans_flag + " in (12 , 18)) and (" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") order by " + _g.d.ic_trans_detail._doc_date + " asc limit(1) ) as First_input ";
                __myQuery += " , (select " + _g.d.ic_trans_detail._doc_date + " from " + _g.d.ic_trans_detail._table + " where (" + _g.d.ic_trans_detail._trans_flag + " in (12 , 18)) and (" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") order by " + _g.d.ic_trans_detail._doc_date + " desc limit(1) ) as Last_input ";
                __myQuery += " , (select " + _g.d.ic_trans_detail._doc_date + " from " + _g.d.ic_trans_detail._table + " where (" + _g.d.ic_trans_detail._trans_flag + " in (44 , 50)) and (" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") order by " + _g.d.ic_trans_detail._doc_date + " desc limit(1) ) as Last_output";
                __myQuery += " from " + _g.d.ic_inventory._table;
                __myQuery += " where " + "(" + _g.d.ic_inventory._code + " <> ''" + ") ";
                __myQuery += " and ( ";
                __myQuery += "  ((select " + _g.d.ic_trans_detail._doc_date + " from " + _g.d.ic_trans_detail._table + " where (" + _g.d.ic_trans_detail._trans_flag + " in (12 , 18)) and (" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") order by " + _g.d.ic_trans_detail._doc_date + " asc limit(1) ) is not null ) ";
                __myQuery += "  and ((select " + _g.d.ic_trans_detail._doc_date + " from " + _g.d.ic_trans_detail._table + " where (" + _g.d.ic_trans_detail._trans_flag + " in (12 , 18)) and (" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") order by " + _g.d.ic_trans_detail._doc_date + " desc limit(1) ) is not null ) ";
                __myQuery += "  and ((select " + _g.d.ic_trans_detail._doc_date + " from " + _g.d.ic_trans_detail._table + " where (" + _g.d.ic_trans_detail._trans_flag + " in (44 , 50)) and (" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") order by " + _g.d.ic_trans_detail._doc_date + " desc limit(1) ) is not null ) ";
                __myQuery += " ) ";
                __myQuery += " order by group_main ";
                this.__query_head.Append(__myQuery);

                //if (__where.Length == 0)
                //{
                //    _contionStr.Add("พิมพ์จากรายงานทั้งหมด");
                //}
                //else
                //{
                //    _contionStr = _condition._conditionStr;
                //}



                //  ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.Result_item_export)
            {
                // รายงานสรุปยอดสินค้าค้างส่ง
                this._report_name = "รายงานสรุปยอดสินค้าค้างส่ง";
                int[] __width = { 10, 40, 10, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "กลุ่มสินค้า", "หน่วยนับ" };
                int[] __width_2 = { 15, 10, 12, 25, 15, 15 };
                string[] __column_2 = { "เลขที่ใบสั่งของ/สั่งจอง", "วันที่เอกสาร", "รหัสลูกค้า", "ชื่อลูกค้า/บริษัท", "จำนวนที่สั่งขาย/สั่งจอง*", "ยอดสินค้าคงค้างส่ง*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();
                /////////////////////////////////////////   query จากพี่อเนก   //////////////////////////////////////////////////  


            }
            else if (_icType == SMLERPReportTool._reportEnum.Result_item_import)
            {
                // รายงานสรุปยอดสินค้าค้างรับ
                this._report_name = "รายงานสรุปยอดสินค้าค้างรับ";
                int[] __width = { 10, 15, 10, 8, 10, 10, 10, 10, 15 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "กลุ่มสินค้า", "หน่วยนับ", "มูลค่าคงเหลือ*", "ปริมาณคงเหลือ*", "ยอดสินค้าคงค้างรับ*", "ยอดสินค้าคงค้างส่ง*", "Available For Sale" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                /////////////////////////////////////////   query จากพี่อเนก   //////////////////////////////////////////////////  


            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ)
            {
                //  รายงานโอนสินค้าและวัตถุดิบ
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_transfer_item_and_material, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_transfer_item_and_material)._str;
                int[] __width = { 10, 18, 18, 20 };
                string[] __column = {  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date)._str
                                        ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no)._str
                                        ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no)._str
                                        ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark)._str
                                    };
                int[] __width_2 = { 7, 16, 18, 12, 12, 12, 12, 6, 5 };
                string[] __column_2 = { ""
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._from_warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._from_warehouse)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._to_warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._to_warehouse)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._from_shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._from_shelf)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._to_shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._to_shelf)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                      };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {

                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head.Append("");
                this.__query_head.Append(" select doc_date , doc_no,item_code ,item_name,qty,unit_code , remark ");
                this.__query_head.Append(" , (select name_1 from ic_unit where ic_unit.code= ic_trans_detail.unit_code)as unit_name ");
                this.__query_head.Append(" , (select wh_code where trans_flag = 70) as wh_code_import  ");
                this.__query_head.Append(" , (select wh_code where trans_flag = 72) as wh_code_export ");
                this.__query_head.Append(" , doc_ref ");
                //this.__query_head.Append(" , (select name_1 from ic_warehouse where (ic_warehouse.code = ic_trans_detail.wh_code))  as wh_name ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where( ic_warehouse.code = wh_code) and (trans_flag = 72)  )as wh_name_export ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where( ic_warehouse.code = wh_code) and (trans_flag = 70)  )as wh_name_import ");
                this.__query_head.Append(" , (select shelf_code where (trans_flag = 70)) as shelf_code_import ");
                this.__query_head.Append(" , (select shelf_code where (trans_flag = 72)) as shelf_code_export ");
                this.__query_head.Append(" , (select name_1 from ic_shelf where( ic_shelf.code = shelf_code) and (trans_flag = 72)  )as shelf_name_export   ");
                this.__query_head.Append(" , (select name_1 from ic_shelf where( ic_shelf.code = shelf_code) and (trans_flag = 70)  )as shelf_name_import  ");
                this.__query_head.Append(" ,line_number,trans_flag ");
                this.__query_head.Append(" from ic_trans_detail ");
                this.__query_head.Append(" where item_code <> '' and trans_flag in (70 , 72) ");
                this.__query_head.Append(" order by line_number , doc_date  ");

            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Transfer_Item_and_material)
            {
                //  รายงานยกเลิกโอนสินค้าและวัตถุดิบ
                this._report_name = "รายงานยกเลิกโอนสินค้าและวัตถุดิบ";
                int[] __width = { 10, 14, 14 };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "เลขที่เอกสารอ้างอิง" };
                int[] __width_2 = { 7, 16, 18, 12, 12, 12, 12, 6, 5 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "จากคลัง", "เข้าคลัง", "จากที่เก็บ", "เข้าที่เก็บ", "หน่วยนับ", "จำนวน*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {

                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head.Append("");
                this.__query_head.Append(" select doc_date , doc_no,doc_ref,item_code ,item_name,qty,unit_code ");
                this.__query_head.Append(" , (select name_1 from ic_unit where ic_unit.code= ic_trans_detail.unit_code)as unit_name ");
                this.__query_head.Append(" , (select wh_code where trans_flag = 71) as wh_code_import  ");
                this.__query_head.Append(" , (select wh_code where trans_flag = 73) as wh_code_export ");
                //this.__query_head.Append(" , (select name_1 from ic_warehouse where (ic_warehouse.code = ic_trans_detail.wh_code))  as wh_name ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where( ic_warehouse.code = wh_code) and (trans_flag = 73)  )as wh_name_export ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where( ic_warehouse.code = wh_code) and (trans_flag = 71)  )as wh_name_import ");
                this.__query_head.Append(" , (select shelf_code where (trans_flag = 71)) as shelf_code_import ");
                this.__query_head.Append(" , (select shelf_code where (trans_flag = 73)) as shelf_code_export ");
                this.__query_head.Append(" , (select name_1 from ic_shelf where( ic_shelf.code = shelf_code) and (trans_flag = 73)  )as shelf_name_export   ");
                this.__query_head.Append(" , (select name_1 from ic_shelf where( ic_shelf.code = shelf_code) and (trans_flag = 71)  )as shelf_name_import  ");
                this.__query_head.Append(" ,line_number,trans_flag ");
                this.__query_head.Append(" from ic_trans_detail ");
                this.__query_head.Append(" where item_code <> '' and trans_flag in (71 , 73) ");
                this.__query_head.Append(" order by line_number , doc_date ");

            }

            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า)
            {
                // รายงานยอดคงเหลือสินค้า
                this._report_name = _g.d.resource_report_ic_report_name._ic_balance;
                int[] __width = { 20, 40, 10, 10, 10, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "จำนวน*", "ต้นทุน*", "มูลค่า*" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }

                /////////////////////////////////////////   query จากพี่อเนก   //////////////////////////////////////////////////  



            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_balance_hightest)
            {
                // รายงานยอดคงเหลือสินค้าที่จุดสูงสุด
                this._report_name = "รายงานยอดคงเหลือสินค้าที่จุดสูงสุด";
                int[] __width = { 15, 20 };
                string[] __column = { "รหัสกลุ่มสินค้า", "ชื่อกลุ่มสินค้า" };
                int[] __width_2 = { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "จุดต่ำสุด*", "จุดสูงสุด(1)*", "ปริมาณยอดคงเหลือ(2)*", "ปริมาณที่เกิน(2)-(1)*", "ต้นทุนเฉลี่ย*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                /////////////////////////////////////////   query จากพี่อเนก   //////////////////////////////////////////////////  
            }
            if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าที่ไม่มีการเคลื่อนไหว)
            {
                // รายงานสินค้าที่ไม่มีการเคลื่อนไหว
                this._report_name = "รายงานสินค้าที่ไม่มีการเคลื่อนไหว";
                int[] __width = { 15, 15, 10, 10, 10, 10, 10, 10, 15 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "จำนวน*", "ต้นทุนเฉลี่ย*", "มูลค่าเฉลี่ย*", "วันที่รับล่าสุด", "วันที่รับออกสุด", "สินค้าที่ไม่เคลื่อนไหว" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                this.__query_head = new StringBuilder();

                /////////////////////////จากพี่ขาว///////////////////////////////////

            }
            else if (_icType == SMLERPReportTool._reportEnum.Result_transfer_item)
            {
                // รายงานสรุปเคลื่อนไหวปริมาณสินค้า
                this._report_name = "รายงานสรุปเคลื่อนไหวปริมาณสินค้า";
                int[] __width = { 10, 10, 8, 8, 8, 8, 8, 8, 10, 8, 8, 8 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "ยอดยกมา*", "ยอดขาย*", "ยอดส่งคืน*", "ยอดรับคืน*", "ยอดเบิก*", "ยอดรับสำเร็จรูป*", "ยอดรับเข้าอื่น ๆ*", "จ่ายออกอื่น ๆ*", "ยอดยกไป*" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                this.__query_head = new StringBuilder();

                /////////////////////////จากพี่ขาว///////////////////////////////////


            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหว)
            {
                // รายงานเคลื่อนไหวสินค้า
                this._report_name = "รายงานเคลื่อนไหวสินค้า";
                int[] __width = { 10, 20, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ" };
                int[] __width_2 = { 5, 10, 20, 10 };
                string[] __column_2 = { "", "รหัสคลังสินค้า", "ชื่อคลังสินค้า", "จำนวนสินค้า*" };
                int[] __width_3 = { 8, 10, 15, 15, 10, 10, 10, 10 };
                string[] __column_3 = { "", "วันที่", "เลขที่เอกสาร", "เลขที่ใบกำกับภาษี", "รายวัน", "ปริมาณเข้า*", "ปริมาณออก*", "ปริมาณคงเหลือ*" };
                this._level = 3;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                for (int __k = 0; __k < __column_3.Length; __k++)
                {
                    this._width_3.Add(__width_3[__k]);
                    this._column_3.Add(__column_3[__k]);
                }
                this.__query_head = new StringBuilder();

                /////////////////////////จากพี่ขาว///////////////////////////////////


            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_transfer_standard)
            {
                // รายงานเคลื่อนไหวสินค้าต้นทุนมาตรฐาน
                this._report_name = "รายงานเคลื่อนไหวสินค้าต้นทุนมาตรฐาน";
                int[] __width = { 10, 20, 10, 10, 10, 10, 10, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "", "", "ยอดเข้า*", "ยอดออก*", "ยอดคงเหลือ*" };
                int[] __width_2 = { 8, 10, 10, 5, 8, 5, 5, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
                string[] __column_2 = { "วันที่", "เลขที่เอกสาร", "เลขที่ใบกำกับภาษี", "รายวัน", "คำอธิบาย", "พื้นที่เก็บ", "คลัง", "ปริมาณ*", "ต้นทุน*", "มูลค่า*", "ปริมาณ*", "ต้นทุน*", "มูลค่า*", "ปริมาณ*", "ต้นทุน*", "มูลค่า*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }
                /////////////////////////จากพี่ขาว///////////////////////////////////

            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานบัญชีคุมพิเศษ)
            {
                // รายงานบัญชีคุมพิเศษสินค้า
                this._report_name = "รายงานบัญชีคุมพิเศษสินค้า";
                int[] __width = { 10, 15, 10, 10, 10, 10, 10, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "ยอดเข้า*", "", "ยอดออก*", "", "ยอดคงเหลือ*" };
                int[] __width_2 = { 5, 8, 25, 10, 10, 10, 5, 10 };
                string[] __column_2 = { "", "คลังสินค้า", "", "จำนวนสินค้า*", "", "ต้นทุนเฉลี่ย*", "", "ยอดรวมมูลค่าสินค้า*" };
                int[] __width_3 = { 3, 8, 15, 10, 10, 10, 10, 10 };
                string[] __column_3 = { "", "วันที่", "เลขที่เอกสาร", "เลขที่ใบกำกับภาาษี", "Lot", "ปริมาณ*", "ต้นทุน*", "มูลค่า*" };
                this._level = 3;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                for (int __k = 0; __k < __column_3.Length; __k++)
                {
                    this._width_3.Add(__width_3[__k]);
                    this._column_3.Add(__column_3[__k]);
                }
                this.__query_head = new StringBuilder();

                /////////////////////////จากพี่ขาว///////////////////////////////////

            }
            else if (_icType == SMLERPReportTool._reportEnum.Print_document_for_count_by_item)
            {
                // พิมพ์เอกสารเพื่อตรวจนับ-ตามสินค้า
                this._report_name = "พิมพ์เอกสารเพื่อตรวจนับ-ตามสินค้า";
                int[] __width = { 10, 20, 10, 10, 10, 15, 10, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "คลังสินค้า", "พื้นที่เก็บ", "วันที่เอกสาร", "เลขที่เอกสาร", "หน่วยนับ", "ปริมาณ(จากการนับ)*" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                string __where = this._condition._where;

                this.__query_head = new StringBuilder();


                this.__query_head.Append(" select ");
                this.__query_head.Append(_g.d.ic_trans_detail._item_code);
                this.__query_head.Append(" , (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") as item_name ");
                this.__query_head.Append(" , (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where" + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ") as warehouse_name ");
                this.__query_head.Append(" , (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ") as shelf_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_date + " , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = (select " + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ")) as unit_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                // this.__query.Append(" where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " <> ''" + __where);
                //this.__query.Append(" order by " + _g.d.ic_trans_detail._item_code);
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ)
            {
                // รายงานผลต่างจากการตรวจนับ
                this._report_name = "รายงานผลต่างจากการตรวจนับ";
                int[] __width = { 8, 10, 12, 12, 8, 10, 12, 8, 8, 8, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "คลัง", "พื้นที่เก็บ", "วันที่เอกสาร", "เลขที่เอกสาร", "หน่วยนับ", "จำนวนสินค้า*", "ยอดที่ตรวจนับ*", "ผลต่าง*", "หมายเหตุ" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }

                //  string __where = _condition.__where;
                this.__query_head = new StringBuilder();
                this.__query_head.Append(" select distinct ");
                this.__query_head.Append(_g.d.ic_trans_detail._item_code + " , (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") as item_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._unit_code + " ,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + ") as unit_name ");
                this.__query_head.Append(" ,  " + _g.d.ic_trans_detail._wh_code + " as warehouse_code ");
                this.__query_head.Append(" , (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ") as warehouse_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_date + " , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , (select " + _g.d.ic_trans._tax_doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " limit(1) ) as tax_no ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._shelf_code + " ,(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + " limit (1) ) as shelf_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty + " , " + _g.d.ic_trans_detail._price + " , " + _g.d.ic_trans_detail._remark);
                this.__query_head.Append(" , (select " + _g.d.ic_trans_detail._qty + " where " + _g.d.ic_trans_detail._trans_flag + " in (62 , 64)" + " ) as count_qty");
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where " + _g.d.ic_trans_detail._item_code + " <> '' ");
                //  this.__query.Append(" where " + _g.d.ic_trans_detail._item_code + " <> '' and (" + _g.d.ic_trans_detail._trans_flag + " in (64)) and (" + _g.d.ic_trans_detail._trans_flag + " not in (65)) ");
                // this.__query.Append(__where);
                //this.__query.Append(" order by " + _g.d.ic_trans_detail._item_code);
                // ok ขาดยอดตรวจนับ ผลต่าง 
            }
            else if (_icType == SMLERPReportTool._reportEnum.Implement_Item)
            {
                // รายงานการปรับปรุงยอดสินค้า  doit
                this._report_name = "รายงานการปรับปรุงยอดสินค้า";
                int[] __width = { 10, 15, 10, 20, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "รหัสสินค้า", "ชื่อสินค้า", "คลัง", "พื้นที่เก็บ", "หน่วยนับ", "ยอดปรับปรุง*", "ทุนปรับปรุง*", "รวมเงิน+/-ราคา/หน่วย*", "มุลค่า*" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                this.__query_head = new StringBuilder();


                //this.__query.Append(" select ");
                //this.__query.Append(" (select code from ic_inventory where code = item_code) as item_code ");
                //this.__query.Append(" , (select name_1 from ic_inventory where code = item_code) as item_name ");
                //this.__query.Append(" , (select name_1 from ic_unit where ic_unit.code =(select unit_standard from ic_inventory where ic_inventory.code = ic_trans_detail.item_code) ) as unit ");
                //this.__query.Append(" , (select code from ic_warehouse where ic_warehouse.code = (select wh_code from ic_wh_shelf where ic_wh_shelf.ic_code = ic_trans_detail.item_code)) as warehouse_code ");
                //this.__query.Append(" , (select name_1 from ic_warehouse where ic_warehouse.code = (select wh_code from ic_wh_shelf where ic_wh_shelf.ic_code = ic_trans_detail.item_code)) as warehouse_name ");
                //this.__query.Append(" , doc_date as doc_date, doc_no as doc_no ");
                //this.__query.Append(" , (select tax_doc_no from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no) as tax_no ");
                //this.__query.Append(" from ic_trans_detail ");
                //this.__query.Append(" where item_code <> '' ");
                //this.__query.Append(" order by item_code ");

                //  จาก  service
            }
            else if (_icType == SMLERPReportTool._reportEnum.Span_import_item)
            {
                // รายงานการประเมินการรับสินค้า
                this._report_name = "รายงานการประเมินการรับสินค้า";
                int[] __width = { 10, 15 };
                string[] __column = { "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้" };
                int[] __width_2 = { 10, 15 };
                string[] __column_2 = { "รหัสกลุ่มสินค้า", "ชื่อกลุ่มสินค้า" };
                int[] __width_3 = { 10, 20, 10, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column_3 = { "รหัสสินค้า", "ชื่อสินค้า", "เลขที่PO", "วันที่ตรวจสินค้า", "วันที่รับสินค้า", "ยอดรับ*", "สมบูรณ์", "ไม่ดี", "แตก", "เกินกำหนด(วัน)*" };
                this._level = 3;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }
                for (int __k = 0; __k < __column_3.Length; __k++)
                {
                    this._width_3.Add(__width_3[__k]);
                    this._column_3.Add(__column_3[__k]);

                }
                this.__query_head = new StringBuilder();
                this.__query_head.Append("");

                //  จาก  service
            }
            else if (_icType == SMLERPReportTool._reportEnum.Lot_item)
            {
                // รายงาน Lot สินค้า
                this._report_name = "รายงาน Lot สินค้า";
                int[] __width = { 10 };
                string[] __column = { "วันที่รับเข้า" };
                int[] __width_2 = { 15, 10, 15, 10, 10, 10, 10, 10, 15, 10, 10, 10 };
                string[] __column_2 = { "เลขที่ Lot", "คลัง", "เลขที่เอกสาร", "วันที่เอกสาร", "จำนวน*", "ต้นทุน*", "จำนวนเงิน*", "รหัสสินค้า", "ชื่อสินค้า", "ปริมาณคงเหลือ*", "มูลค่าคงเหลือ*", "หน่วยนับ", };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }

                this.__query_head = new StringBuilder();
                this.__query_head.Append("");

                //  จาก  service
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา)
            {
                // รายงานสินค้าและวัตถุดิบ คงเหลือยกมา
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_balance_bring, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_balance_bring)._str; ;
                int[] __width = { 15, 15, 15, 15, 20 };
                string[] __column = {MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no)._str
                                        ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_date)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no)._str
                                        ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark)._str
                                    };
                int[] __width_2 = { 7, 15, 20, 10, 12, 12, 8, 8, 8 };
                string[] __column_2 = { ""
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf)._str
                                           , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price)._str+"*"
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price)._str+ "*"
                                      };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }

                this.__query_head.Append(" select item_code , item_name ");
                this.__query_head.Append(" , qty ,price, doc_date , doc_no , ref_doc_date , doc_ref");
                this.__query_head.Append(" , remark  , wh_code , unit_code , shelf_code , trans_flag  ");
                this.__query_head.Append(" ,unit_code||'~'|| (select name_1 from ic_unit where code = unit_code) as unit_name  ");
                this.__query_head.Append(" , wh_code||'~'||(select name_1 from ic_warehouse where code = wh_code) as wh_name  ");
                this.__query_head.Append(" ,shelf_code||'~'||(select name_1 from ic_shelf where code = shelf_code) as shelf_name ");
                this.__query_head.Append(" from ic_trans_detail  ");
                this.__query_head.Append(" where item_code <> ''  and trans_flag = 54  ");
                this.__query_head.Append(" order by doc_no , line_number , item_code  ");


            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Item_and_Staple)
            {
                // รายงานยกเลิกสินค้าและวัตถุดิบ คงเหลือยกมา
                this._report_name = "รายงานยกเลิกสินค้าและวัตถุดิบ คงเหลือยกมา";

                int[] __width = { 15, 15, 15, 15, 20 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "เอกสารอ้างอิงวันที่", "เอกสารอ้างอิงเลขที่", "หมายเหตุ" };
                int[] __width_2 = { 7, 15, 20, 10, 12, 12, 8, 8, 8 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "คลัง", "พื้นที่เก็บ", "จำนวน*", "ต้นทุน*", "มูลค่า*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }

                this.__query_head.Append(" select item_code , item_name ");
                this.__query_head.Append(" , qty ,price, doc_date , doc_no , ref_doc_date , ref_doc_no ");
                this.__query_head.Append(" , remark  , wh_code , unit_code , shelf_code  ");
                this.__query_head.Append(" ,unit_code||'~'|| (select name_1 from ic_unit where code = unit_code) as unit_name  ");
                this.__query_head.Append(" , wh_code||'~'||(select name_1 from ic_warehouse where code = wh_code) as wh_name  ");
                this.__query_head.Append(" ,shelf_code||'~'||(select name_1 from ic_shelf where code = shelf_code) as shelf_name ");
                this.__query_head.Append(" from ic_trans_detail  ");
                this.__query_head.Append(" where item_code <> ''  and trans_flag = 55  ");
                this.__query_head.Append(" order by   line_number , doc_no , item_code ");


            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน)
            {
                // รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._cancel_implement_item_over, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._cancel_implement_item_over)._str; ;
                int[] __width = { 14, 15, 14, 18 };
                string[] __column = {     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no)._str
                                        ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date)._str
                                        ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no)._str
                                        ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark)._str
                                    };
                int[] __width_2 = { 6, 18, 18, 10, 15, 15, 6, 6, 6 };
                string[] __column_2 = { ""
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse)._str
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price)._str+"*"
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price)._str+ "*"
                                      };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }

                this.__query_head.Append(" select item_code , item_name ");
                this.__query_head.Append(" , qty ," + _g.d.ic_trans_detail._average_cost + " as price," + _g.d.ic_trans_detail._sum_of_cost + " as total_amount");
                this.__query_head.Append(" , doc_date , doc_no , ref_doc_date , doc_ref ");
                this.__query_head.Append(" , remark  , wh_code , unit_code , shelf_code  , trans_flag ");
                this.__query_head.Append(" , (select name_1 from ic_unit where code = unit_code) as unit_name  ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where code = wh_code) as wh_name  ");
                this.__query_head.Append(" ,(select name_1 from ic_shelf where code = shelf_code) as shelf_name ");
                this.__query_head.Append(" from ic_trans_detail  ");
                this.__query_head.Append(" where item_code <> ''  and trans_flag = 66  ");
                this.__query_head.Append(" order by  line_number , doc_date  ");


            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Implement_Item_and_Staple_Over)
            {
                // รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)
                this._report_name = "รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)";
                int[] __width = { 14, 10, 14, 18 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "เลขที่เอกสารอ้างอิง", "หมายเหตุ" };
                int[] __width_2 = { 6, 18, 18, 10, 15, 15, 6, 6, 6 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "คลัง", "พื้นที่เก็บ", "จำนวน*", "ต้นทุน*", "มูลค่า*" };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }

                this.__query_head.Append(" select item_code , item_name ");
                this.__query_head.Append(" , qty ,price, doc_date , doc_no , ref_doc_date , ref_doc_no ");
                this.__query_head.Append(" , remark  , wh_code , unit_code , shelf_code  ");
                this.__query_head.Append(" , (select name_1 from ic_unit where code = unit_code) as unit_name  ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where code = wh_code) as wh_name  ");
                this.__query_head.Append(" ,(select name_1 from ic_shelf where code = shelf_code) as shelf_name ");
                this.__query_head.Append(" from ic_trans_detail  ");
                this.__query_head.Append(" where item_code <> ''  and trans_flag = 67  ");
                this.__query_head.Append(" order by  line_number , doc_date  ");


            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด)
            {
                // รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด)
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._cancel_implement_item_minus, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._cancel_implement_item_minus)._str;
                int[] __width = { 14, 10, 14, 18 };
                string[] __column = {   MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no)._str
                                        ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date)._str
                                        ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no)._str
                                        ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark)._str
                                    };
                int[] __width_2 = { 9, 14, 14, 15, 15, 15, 6, 6, 6 };
                string[] __column_2 = { ""
                                         ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse)._str
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price)._str+"*"
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price)._str+ "*"
                                      };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }

                this.__query_head.Append(" select item_code , item_name ");
                this.__query_head.Append(" , qty ,price, doc_date , doc_no , ref_doc_date , doc_ref ");
                this.__query_head.Append(" , remark  , wh_code , unit_code , shelf_code , trans_flag  ");
                this.__query_head.Append(" , (select name_1 from ic_unit where code = unit_code) as unit_name  ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where code = wh_code) as wh_name  ");
                this.__query_head.Append(" ,(select name_1 from ic_shelf where code = shelf_code) as shelf_name ");
                this.__query_head.Append(" from ic_trans_detail  ");
                this.__query_head.Append(" where item_code <> ''  and trans_flag = 68  ");
                this.__query_head.Append(" order by  line_number , doc_date ");


            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Implement_Item_and_Staple_Minus)
            {
                // รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด)
                this._report_name = "รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด)";
                int[] __width = { 10, 10, 15, 18 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "เลขที่เอกสารอ้างอิง", "หมายเหตุ" };
                //int[] __width_2 = { 5, 17, 20, 10, 15, 15, 6, 6, 6 };
                //string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "คลัง", "พื้นที่เก็บ", "จำนวน", "ต้นทุน", "รวมมูลค่า" };
                int[] __width_2 = { 5, 20, 20, 15, 17, 17, 6 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "คลัง", "พื้นที่เก็บ", "จำนวน*" };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);

                }

                this.__query_head.Append(" select item_code , item_name ");
                this.__query_head.Append(" , qty ,price, doc_date , doc_no , ref_doc_date , ref_doc_no ");
                this.__query_head.Append(" , remark  , wh_code , unit_code , shelf_code  ");
                this.__query_head.Append(" , (select name_1 from ic_unit where code = unit_code) as unit_name  ");
                this.__query_head.Append(" , (select name_1 from ic_warehouse where code = wh_code) as wh_name  ");
                this.__query_head.Append(" ,(select name_1 from ic_shelf where code = shelf_code) as shelf_name ");
                this.__query_head.Append(" from ic_trans_detail  ");
                this.__query_head.Append(" where item_code <> ''  and trans_flag = 69  ");
                this.__query_head.Append(" order by   line_number , doc_date  ");


            }
            else if (_icType == SMLERPReportTool._reportEnum.Print_document_for_count_by_warehouse)
            {
                // พิมพ์เอกสารเพื่อตรวจนับ-ตามคลัง
                this._report_name = "พิมพ์เอกสารเพื่อตรวจนับ-ตามคลัง";
                int[] __width = { 6, 10, 10, 15, 10, 15, 10, 10, 10 };
                string[] __column = { "คลังสินค้า", "พื้นที่เก็บ", "วันที่เอกสาร", "เลขที่เอกสาร", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "ปริมาณ*", "(จากการนับ)" };
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                string __where = _condition._where;
                this.__query_head = new StringBuilder();
                this.__query_head.Append(" select ");
                this.__query_head.Append(_g.d.ic_trans_detail._wh_code + " as warehouse_code ");
                this.__query_head.Append(" , (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + " ) as warehouse_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_date + " , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , (select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") as item_code ");
                this.__query_head.Append(" , (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") as item_name ");
                this.__query_head.Append(" , (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + " ) as unit_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                //this.__query.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in (64)) and (" + _g.d.ic_trans_detail._trans_flag + " not in (65)) " + __where);
                //this.__query.Append(" order by " + _g.d.ic_trans_detail._wh_code);
                // ok

            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป)
            {
                // รายงานการรับสินค้าสำเร็จรูป
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._cancel_ic_import_item_ready, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._cancel_ic_import_item_ready)._str;
                int[] __width = { 10, 15 };
                string[] __column = {  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date)._str
                                        , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark)._str
                                    };
                int[] __width_2 = { 5, 8, 12, 12, 12, 12, 12, 9, 6, 6, 6 };
                string[] __column_2 = { ""
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no)._str
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse)._str
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf)._str
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price)._str+"*"
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price)._str+ "*"
                                      };
                this._level = 2;
                for (int __j = 0; __j < __column.Length; __j++)
                {
                    this._width.Add(__width[__j]);
                    this._column.Add(__column[__j]);

                }
                for (int __i = 0; __i < __column_2.Length; __i++)
                {
                    this._width_2.Add(__width_2[__i]);
                    this._column_2.Add(__column_2[__i]);
                }
                this.__query_head = new StringBuilder();
                //  string __where = _condition.__where;
                //string __order_by = _condition._order_by;
                this.__query_head.Append(" select ");
                this.__query_head.Append(_g.d.ic_trans_detail._doc_date);
                //this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._remark);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_code);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_name);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_ref);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._trans_flag);
                this.__query_head.Append(" , (select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ") as department ");
                this.__query_head.Append(" , (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ") as warehouse_name ");
                this.__query_head.Append(" , (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ") as shelf_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._unit_code);
                //this.__query.Append(" ,  " + _g.d.ic_trans_detail._unit_name);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._unit_code + "||'~'||(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty + " ," + _g.d.ic_trans_detail._sum_of_cost + " , " + _g.d.ic_trans_detail._price);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in (60)) ");
                this.__query_head.Append(" order by  line_number , doc_date ");
                //      this.__query.Append(__where);
                //     this.__query.Append(__order_by);

                //this.__query.Append(" order by item_code ");

            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Import_Item_ready)
            {
                // รายงานการยกเลิกรับสินค้าสำเร็จรูป-วันที่
                this._report_name = "รายงานการยกเลิกรับสินค้าสำเร็จรูป";
                int[] __width = { 10 };
                string[] __column = { "วันที่เอกสาร" };
                int[] __width_2 = { 5, 10, 22, 12, 12, 12, 9, 6, 6, 6 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "เลขที่เอกสาร", "คลังสินค้า", "พื้นที่เก็บ", "หน่วยนับ", "จำนวน*", "ต้นทุน*", "มูลค่า*" };
                this._level = 2;
                for (int __j = 0; __j < __column.Length; __j++)
                {
                    this._width.Add(__width[__j]);
                    this._column.Add(__column[__j]);

                }
                for (int __i = 0; __i < __column_2.Length; __i++)
                {
                    this._width_2.Add(__width_2[__i]);
                    this._column_2.Add(__column_2[__i]);
                }
                this.__query_head = new StringBuilder();
                //  string __where = _condition.__where;
                //string __order_by = _condition._order_by;
                this.__query_head.Append(" select ");
                this.__query_head.Append(_g.d.ic_trans_detail._doc_date);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_code);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_name);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , (select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ") as department ");
                this.__query_head.Append(" , (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ") as warehouse_name ");
                this.__query_head.Append(" , (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ") as shelf_name ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._unit_code);
                //this.__query.Append(" ,  " + _g.d.ic_trans_detail._unit_name);
                this.__query_head.Append(" , (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty + " ," + _g.d.ic_trans_detail._sum_of_cost + " , " + _g.d.ic_trans_detail._price);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in (19)) ");
                //      this.__query.Append(__where);
                //     this.__query.Append(__order_by);

                //this.__query.Append(" order by item_code ");

            }
            else if (_icType == SMLERPReportTool._reportEnum.Receptance_Widen_by_Date)
            {
                // รายงานรับคืนเบิก-วันที่
                string __where = _condition._where;
                string __order_by = _condition._order_by;
                bool __display_serial = _condition._display_serial;

                this._report_name = "รายงานรับคืนเบิก-วันที่";
                int[] __width = { 10, 15, 15 };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "คำอธิบายรายการ" };
                int[] __width_2 = null;
                string[] __column_2 = null;

                if (__display_serial == true)
                {
                    __width_2 = new int[] { 10, 14, 10, 10, 8, 8, 8, 10, 7, 7, 7 };
                    __column_2 = new string[] { "รหัสสินค้า", "ชื่อสินค้า", "Serial", "ผู้เบิก", "แผนก", "คลังสินค้า", "พื้นที่เก็บ", "หน่วยนับ", "จำนวนสินค้า*", "ต้นทุน/หน่วย*", "ต้นทุนทั้งสิ้น*" };
                }
                else if (__display_serial == false)
                {
                    __width_2 = new int[] { 10, 14, 10, 8, 8, 8, 10, 7, 7, 7 };
                    __column_2 = new string[] { "รหัสสินค้า", "ชื่อสินค้า", "ผู้เบิก", "แผนก", "คลังสินค้า", "พื้นที่เก็บ", "หน่วยนับ", "จำนวนสินค้า*", "ต้นทุน/หน่วย*", "ต้นทุนทั้งสิ้น*" };
                }
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                string __serial = "";

                if (__display_serial == false) __serial = "";
                if (__display_serial == true) __serial = " ,(select " + _g.d.ic_serial._serial_number + " from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._ic_code + " = " + _g.d.ic_trans_detail._item_code + ") as serial ";

                this.__query_head.Append(" select ");
                this.__query_head.Append(_g.d.ic_trans_detail._doc_date);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" ,(select " + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + ") as remark  ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_code);
                this.__query_head.Append(" ,  " + _g.d.ic_trans_detail._item_name);
                this.__query_head.Append(__serial);
                this.__query_head.Append(" , (select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + " = (select " + _g.d.ic_trans._sale_code + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + ")) as sale_name");
                this.__query_head.Append(" , (select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ") as department");
                this.__query_head.Append(" , (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ") as warehouse_name ");
                this.__query_head.Append(" , (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ") as shelf_name ");
                this.__query_head.Append(" , (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = (select " + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ")) as unit ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._cost);
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._sum_of_cost);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in (58)) ");
                //  this.__query.Append(__where);
                // this.__query.Append(__order_by);
                //this.__query.Append(" order by item_code ");
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ)
            {
                // รายงานการเบิกสินค้า,วัตถุดิบ
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_withdraw_item, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_withdraw_item)._str;
                int[] __width = { 12, 12, 12, 30 };
                string[] __column = {      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no)._str
                                        ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date)._str
                                        ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._ref_doc_no)._str
                                        ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark)._str
                                    };
                //int[] __width_2 = { 8,15, 20, 15, 15, 10, 8, 8 };
                //string[] __column_2 = {"", "รหัสสินค้า", "ชื่อสินค้า", "คลังสินค้า", "พื้นที่เก็บ", "หน่วยนับ", "จำนวน*", "ต้นทุน*" };
                int[] __width_2 = { 17, 15, 20, 15, 15, 10, 8 };
                string[] __column_2 = { ""
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse)._str
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf)._str
                                          ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                      };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select doc_date , doc_no , doc_ref ");
                this.__query_head.Append(" ,(select remark from ic_trans where( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag)) as remark  ");
                this.__query_head.Append(" , item_code ,  item_name  ");
                this.__query_head.Append(" , (select name_1 from erp_user where code = (select sale_code from ic_trans where ( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag) ) ) as sale_name  ");
                this.__query_head.Append(" ,wh_code ");
                this.__query_head.Append(" , (select name_1 from erp_department_list where code = department_code) as department ,wh_code||'~'|| (select name_1 from ic_warehouse where code = wh_code) as warehouse_name   ");
                this.__query_head.Append(" ,shelf_code ");
                this.__query_head.Append(" , shelf_code||'~'||(select name_1 from ic_shelf where code = shelf_code) as shelf_name  , unit_code||'~'||(select name_1 from ic_unit where code = unit_code) as unit   ");
                this.__query_head.Append(" , qty , sum_of_cost  ");
                this.__query_head.Append(" from ic_trans_detail ");
                this.__query_head.Append(" where trans_flag = 56 ");
                this.__query_head.Append(" order by  doc_date , line_number   ");
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Withdraw_Item_Staple)
            {
                // รายงานการยกเลิกเบิกสินค้า,วัตถุดิบ
                this._report_name = "รายงานการยกเลิกเบิกสินค้า  วัตถุดิบ";
                int[] __width = { 12, 12, 12, 30 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "เลขที่เอกสารอ้างอิง", "คำอธิบายรายการ" };

                int[] __width_2 = { 17, 15, 20, 15, 15, 10, 8 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "คลังสินค้า", "พื้นที่เก็บ", "หน่วยนับ", "จำนวน*" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select doc_date , doc_no , doc_ref ");
                this.__query_head.Append(" ,(select remark from ic_trans where( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag)) as remark  ");
                this.__query_head.Append(" , item_code ,  item_name  ");
                this.__query_head.Append(" , (select name_1 from erp_user where code = (select sale_code from ic_trans where ( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag) ) ) as sale_name  ");
                this.__query_head.Append(" ,wh_code ");
                this.__query_head.Append(" , (select name_1 from erp_department_list where code = department_code) as department , (select name_1 from ic_warehouse where code = wh_code) as warehouse_name   ");
                this.__query_head.Append(" ,shelf_code ");
                this.__query_head.Append(" , (select name_1 from ic_shelf where code = shelf_code) as shelf_name  , (select name_1 from ic_unit where code = unit_code) as unit   ");
                this.__query_head.Append(" , qty , sum_of_cost  ");
                this.__query_head.Append(" from ic_trans_detail ");
                this.__query_head.Append(" where trans_flag = 57 ");
                this.__query_head.Append(" order by  doc_date , line_number   ");
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ)
            {
                // รายงานการรับคืนเบิกสินค้า  วัตถุดิบ  จากการเบิก
                this._report_name = MyLib._myResource._findResource(_g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_item_return, _g.d.resource_report_ic_report_name._table + "." + _g.d.resource_report_ic_report_name._ic_item_return)._str;
                int[] __width = { 12, 10, 14, 14, 20 };
                string[] __column = {      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_no)._str
                                        ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._doc_date)._str
                                        , "เลขที่เอกสารอ้างอิง(ใบเบิก)"
                                        , "เลขที่เอกสารอ้งอิง(ใบรับคืน)"
                                        ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._remark)._str
                                    };
                int[] __width_2 = { 6, 15, 15, 10, 15, 15, 8, 8, 8 };
                string[] __column_2 = { ""
                                          ,    MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_code)._str
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._item_name)._str
                                          ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._unit)._str
                                          ,     MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._warehouse)._str
                                          ,      MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._shelf)._str
                                          , MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._qty)._str+"*"
                                          ,  MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._price)._str+"*"
                                          ,MyLib._myResource._findResource(_g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price, _g.d.resource_report_ic_column._table + "." + _g.d.resource_report_ic_column._total_price)._str+ "*"
                                      };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select doc_date , doc_no , doc_ref ");
                this.__query_head.Append(" ,( select doc_ref from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no ) as doc_ref_from_withdraw ");
                this.__query_head.Append(" ,(select remark from ic_trans where( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag)) as remark  ");
                this.__query_head.Append(" , item_code ,  item_name  ");
                this.__query_head.Append(" , (select name_1 from erp_user where code = (select sale_code from ic_trans where ( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag) ) ) as sale_name  ");
                this.__query_head.Append(" , wh_code ");
                this.__query_head.Append(" , (select name_1 from erp_department_list where code = department_code) as department , (select name_1 from ic_warehouse where code = wh_code) as warehouse_name   ");
                this.__query_head.Append(" , shelf_code ");
                this.__query_head.Append(" , (select name_1 from ic_shelf where code = shelf_code) as shelf_name  , (select name_1 from ic_unit where code = unit_code) as unit   ");
                this.__query_head.Append(" , qty , sum_of_cost , trans_flag ");
                this.__query_head.Append(" from ic_trans_detail ");
                this.__query_head.Append(" where trans_flag = 58 ");
                this.__query_head.Append(" order by  doc_date , line_number   ");
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.Cancel_Refunded_Withdraw_Item_Staple)
            {
                // รายงานการยกเลิกรับคืนเบิกสินค้า  วัตถุดิบ  จากการเบิก
                this._report_name = "รายงานการยกเลิกรับคืนเบิกสินค้า  วัตถุดิบ  จากการเบิก";
                int[] __width = { 12, 10, 12 };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "เลขที่เอกสารอ้างอิง(ใบรันคืน)" };
                int[] __width_2 = { 6, 15, 15, 10, 15, 15, 8, 8, 8 };
                string[] __column_2 = { "", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "คลังสินค้า", "พื้นที่เก็บ", "จำนวน*", "ต้นทุน*", "มูลค่า*" };


                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();

                this.__query_head.Append(" select doc_date , doc_no , doc_ref ");
                this.__query_head.Append(" ,(select remark from ic_trans where( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag)) as remark  ");
                this.__query_head.Append(" , item_code ,  item_name  ");
                this.__query_head.Append(" , (select name_1 from erp_user where code = (select sale_code from ic_trans where ( ic_trans.doc_no = ic_trans_detail.doc_no) and (ic_trans.doc_date= ic_trans_detail.doc_date) and (ic_trans.trans_flag = ic_trans_detail.trans_flag) ) ) as sale_name  ");
                this.__query_head.Append(" ,wh_code ");
                this.__query_head.Append(" , (select name_1 from erp_department_list where code = department_code) as department , (select name_1 from ic_warehouse where code = wh_code) as warehouse_name   ");
                this.__query_head.Append(" ,shelf_code ");
                this.__query_head.Append(" , (select name_1 from ic_shelf where code = shelf_code) as shelf_name  , (select name_1 from ic_unit where code = unit_code) as unit   ");
                this.__query_head.Append(" , qty , sum_of_cost  ");
                this.__query_head.Append(" from ic_trans_detail ");
                this.__query_head.Append(" where trans_flag = 59 ");
                this.__query_head.Append(" order by  doc_date , line_number   ");
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.Expose_Item_price)
            {
                // รายงานกำหนดราคาขายสินค้า
                this._report_name = "รายงานกำหนดราคาขายสินค้า";
                int[] __width = { 5, 12, 8, 10, 10, 5, 8, 8, 8, 8, 8, 8 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "ชื่อภาษาอังกฤษ", "ยอดคงเหลือ", "ราคาซื้อล่าสุด", "หน่วยนับ", "จากจำนวน*", "ถึงจำนวน*", "ราคาขาย1*", "ราคาขาย2*", "ราคาขาย3*", "ราคาขาย4*" };
                int[] __width_2 = { 8, 8, 8, 10, 10, 8, 10, 10, 8, 8, 8 };
                string[] __column_2 = { "", "โปรโมชันสินค้า", "", "", "", "หน่วยนับ", "จากจำนวน*", "ถึงจำนวน*", "จากวันที่", "ถึงวันที่", "จากกลุ่มลูกค้า" };
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();
                this.__query_head.Append(" select " + _g.d.ic_inventory._code + " , " + _g.d.ic_inventory._name_1 + " , " + _g.d.ic_inventory._name_eng_1);
                this.__query_head.Append(" ,(select " + _g.d.ic_trans_detail._price + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_inventory._code + " and " + _g.d.ic_trans_detail._trans_flag + " = 12) as last_price ");
                this.__query_head.Append(" ,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + " =(select " + _g.d.ic_inventory_price._unit_code + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._ic_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ")) as unit ");
                this.__query_head.Append(" ,(select " + _g.d.ic_inventory_price._from_qty + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._ic_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as from_qty ");
                this.__query_head.Append(" ,(select " + _g.d.ic_inventory_price._to_qty + " from " + _g.d.ic_inventory_price._table + " where " + _g.d.ic_inventory_price._table + "." + _g.d.ic_inventory_price._ic_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + ") as to_qty ");
                this.__query_head.Append(" ,(select " + _g.d.ic_inventory_price._sale_price1 + " from " + _g.d.ic_inventory_price._table + " where ic_inventory_price.ic_code = ic_inventory.code) as sale_price1 ");
                this.__query_head.Append(" ,(select " + _g.d.ic_inventory_detail._sale_price_2 + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._ic_code + " = " + _g.d.ic_inventory._code + ") as sale_price2 ");
                this.__query_head.Append(" ,(select " + _g.d.ic_inventory_detail._sale_price_3 + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._ic_code + " = " + _g.d.ic_inventory._code + ") as sale_price3 ");
                this.__query_head.Append(" ,(select " + _g.d.ic_inventory_detail._sale_price_4 + " from " + _g.d.ic_inventory_detail._table + " where " + _g.d.ic_inventory_detail._ic_code + " = " + _g.d.ic_inventory._code + ") as sale_price4 ");
                this.__query_head.Append(" ,(select " + _g.d.ic_promotion_detail._promote_code + " from " + _g.d.ic_promotion_detail._table + " where " + _g.d.ic_promotion_detail._ic_code + "= " + _g.d.ic_inventory._code + ") as promotion ");
                this.__query_head.Append(" ,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = (select " + _g.d.ic_promotion_detail._unit_code + " from " + _g.d.ic_promotion_detail._table + " where " + _g.d.ic_promotion_detail._ic_code + " = " + _g.d.ic_inventory._code + ")) as unit_promotion ");
                this.__query_head.Append(" ,(select " + _g.d.ic_promotion_detail._qty_1 + " from " + _g.d.ic_promotion_detail._table + " where " + _g.d.ic_promotion_detail._ic_code + " = " + _g.d.ic_inventory._code + ") as from_qty_promotion ");
                this.__query_head.Append(" ,(select " + _g.d.ic_promotion_detail._qty_2 + " from " + _g.d.ic_promotion_detail._table + " where " + _g.d.ic_promotion_detail._ic_code + " = " + _g.d.ic_inventory._code + ") as to_qty_promotion ");
                this.__query_head.Append(" ,(select " + _g.d.ic_promotion._promote_start + " from " + _g.d.ic_promotion._table + " where " + _g.d.ic_promotion._promote_code + " =(select " + _g.d.ic_promotion_detail._promote_code + "from " + _g.d.ic_promotion_detail._table + " where " + _g.d.ic_promotion_detail._ic_code + " = " + _g.d.ic_inventory._code + ")) as from_date_promotion ");
                this.__query_head.Append(" ,(select " + _g.d.ic_promotion._promote_stop + " from " + _g.d.ic_promotion._table + " where " + _g.d.ic_promotion._promote_code + " =(select " + _g.d.ic_promotion_detail._promote_code + "from " + _g.d.ic_promotion_detail._table + " where " + _g.d.ic_promotion_detail._ic_code + " = " + _g.d.ic_inventory._code + ")) as to_promotion ");
                this.__query_head.Append(" ,(select " + _g.d.ic_promotion._ar_group + " from " + _g.d.ic_promotion._table + " where " + _g.d.ic_promotion._promote_code + " =(select " + _g.d.ic_promotion_detail._promote_code + " from " + _g.d.ic_promotion_detail._table + " where " + _g.d.ic_promotion_detail._ic_code + " = " + _g.d.ic_inventory._code + ")) as ar_group ");
                this.__query_head.Append(" from " + _g.d.ic_inventory._table);
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.TransferItem_between_Warehouse_by_output)
            {
                // รายงานโอนสินค้าระหว่างคลัง-ตามคลังโอนออก
                this._report_name = "รายงานโอนสินค้าระหว่างคลัง-ตามคลังโอนออก";
                int[] __width = null;
                string[] __column = null;
                if (_condition._display_serial == false)
                {
                    __width = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    __column = new string[] { "โอนจากคลัง", "โอนจากที่เก็บ", "โอนเข้าคลัง", "โอนเข้าที่เก็บ", "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "วันที่เอกสาร", "เลขที่เอกสาร", "จำนวน*" };
                }
                else if (_condition._display_serial == true)
                {
                    __width = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    __column = new string[] { "โอนจากคลัง", "โอนจากที่เก็บ", "โอนเข้าคลัง", "โอนเข้าที่เก็บ", "รหัสสินค้า", "ชื่อสินค้า", "Serial", "หน่วยนับ", "วันที่เอกสาร", "เลขที่เอกสาร", "จำนวน*" };
                }
                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                this.__query_head = new StringBuilder();

                string __serial = "";
                if (_condition._display_serial == true) __serial = " , (select " + _g.d.ic_serial._serial_number + " from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._ic_code + " = " + _g.d.ic_trans_detail._item_code + ") as serial";

                this.__query_head.Append(" select ");
                this.__query_head.Append(" (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = (select " + _g.d.ic_wh_shelf._wh_code + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._ic_code + " = " + _g.d.ic_trans_detail._item_code + ") and " + _g.d.ic_trans_detail._trans_flag + " = 72) as from_warehouse ");
                this.__query_head.Append(" ,(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = (select " + _g.d.ic_wh_shelf._shelf_code + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._ic_code + " = " + _g.d.ic_trans_detail._item_code + ") and " + _g.d.ic_trans_detail._trans_flag + " = 72) as from_shelf ");
                this.__query_head.Append(" (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = (select " + _g.d.ic_wh_shelf._wh_code + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._ic_code + " = " + _g.d.ic_trans_detail._item_code + ") and " + _g.d.ic_trans_detail._trans_flag + " = 70) as to_warehouse ");
                this.__query_head.Append(" ,(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = (select " + _g.d.ic_wh_shelf._shelf_code + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._ic_code + " = " + _g.d.ic_trans_detail._item_code + ") and " + _g.d.ic_trans_detail._trans_flag + " = 70) as to_shelf ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name);
                this.__query_head.Append(" ,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = (select " + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") ) as unit ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._doc_date + " , " + _g.d.ic_trans_detail._doc_no + " , " + _g.d.ic_trans_detail._qty);
                this.__query_head.Append(__serial);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in (72)) and  (" + _g.d.ic_trans_detail._trans_flag + " not in (73) ) ");
                // this.__query.Append(" order by (select code from ic_inventory where code = item_code) ");
                // ok


            }
            else if (_icType == SMLERPReportTool._reportEnum.TransferItem_between_and_Detail)
            {
                // รายงานโอนสินค้าระหว่างคลังและรายการย่อย
                this._report_name = "รายงานโอนสินค้าระหว่างคลังและรายการย่อย";
                int[] __width = { 10, 15, 10, 10, 10, 10, 10 };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "แผนก", "", "", "", "จำนวน*" };
                int[] __width_2 = null;
                string[] __column_2 = null;
                if (_condition._display_serial == false)
                {
                    __width_2 = new int[] { 10, 10, 10, 10, 10, 10, 10, 5, 10, 10 };
                    __column_2 = new string[] { "รหัสสินค้า", "ชื่อสินค้า", "โอนจากคลัง", "โอนจากที่เก็บ", "โอนเข้าคลัง", "โอนเข้าที่เก็บ", "หน่วยนับ", "", "จำนวนสินค้า*", "ต้นทุนทั้งสิ้น*" };
                }
                else if (_condition._display_serial == true)
                {
                    __width_2 = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 5, 10, 10 };
                    __column_2 = new string[] { "รหัสสินค้า", "ชื่อสินค้า", "Serial", "โอนจากคลัง", "โอนจากที่เก็บ", "โอนเข้าคลัง", "โอนเข้าที่เก็บ", "หน่วยนับ", "", "จำนวนสินค้า*", "ต้นทุนทั้งสิ้น*" };
                }
                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }

                string __serial = "";
                if (_condition._display_serial == true) __serial = "";

                this.__query_head = new StringBuilder();
                this.__query_head.Append(" select " + _g.d.ic_trans_detail._doc_date + " , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , (select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + "where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ") as department ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name + " ," + _g.d.ic_trans_detail._qty + " as qty_docno ");
                this.__query_head.Append(" ,(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + " and " + _g.d.ic_trans_detail._trans_flag + " = 72) as from_warehouse ");
                this.__query_head.Append(" ,(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + "where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + " and " + _g.d.ic_trans_detail._trans_flag + " = 72) as from_shelf ");
                this.__query_head.Append(" ,(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + " and " + _g.d.ic_trans_detail._trans_flag + " = 70) as to_warehouse ");
                this.__query_head.Append(" ,(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + "where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + " and " + _g.d.ic_trans_detail._trans_flag + " = 70) as to_shelf ");
                this.__query_head.Append(" ,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + " ) as unit ");
                this.__query_head.Append(" ," + _g.d.ic_trans_detail._qty + " as qty_item , " + _g.d.ic_trans_detail._sum_of_cost);
                this.__query_head.Append(__serial);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where " + _g.d.ic_trans_detail._trans_flag + " = 70 or " + _g.d.ic_trans_detail._trans_flag + " = 71 or " + _g.d.ic_trans_detail._trans_flag + " = 72 or " + _g.d.ic_trans_detail._trans_flag + " = 73 ");
                // ok

            }
            else if (_icType == SMLERPReportTool._reportEnum.Import_Stock_Item)
            {
                // รายงานการรับสต๊อกสินค้า
                this._report_name = "รายงานการรับสต๊อกสินค้า";
                int[] __width = { 5, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column = { "ลำดับ", "รหัส", "ชื่อสินค้า", "หน่วยนับ", "สาขา", "คลัง", "พื้นที่เก็บ", "จำนวน*" };


                this._level = 1;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }

                string __where = _condition._where;

                this.__query_head = new StringBuilder();
                this.__query_head.Append(" select " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name);
                this.__query_head.Append(" ,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + ") as unit ");
                this.__query_head.Append(" ,(select " + _g.d.erp_branch_list._name_1 + " from " + _g.d.erp_branch_list._table + " where " + _g.d.erp_branch_list._code + " = " + _g.d.ic_trans_detail._branch_code + " ) as branch ");
                this.__query_head.Append(" ,(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + " ) as warehouse "); this.__query_head.Append(" ,(select name_1 from ic_shelf where code = shelf_code) as shelf ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty + " , " + _g.d.ic_trans_detail._trans_flag);
                this.__query_head.Append("  , (select " + _g.d.erp_department_list._code + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ") ");  //  ใช้ใน  where condition
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " not in (51,61)) and (" + _g.d.ic_trans_detail._trans_flag + " in (50,60))");
                this.__query_head.Append(__where);

                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_Material_Balance_Bring)
            {
                // รายงานการบันทึกยอดสินค้ายกมาต้นปี
                this._report_name = "รายงานการบันทึกยอดสินค้ายกมาต้นปี";
                int[] __width = { 10, 15, 15 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หมายเหตุ" };
                int[] __width_2 = { 10, 10, 10, 10, 10, 10, 10, 10 };
                string[] __column_2 = { "รหัสสินค้า", "ชื่อสินค้า", "คลัง", "พื้นที่เก็บ", "หน่วยนับ", "จำนวนสินค้า*", "ต้นทุน/หน่วย*", "จำนวนเงิน*" };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();
                this.__query_head.Append(" select " + _g.d.ic_trans_detail._doc_date + " , " + _g.d.ic_trans_detail._doc_no);
                this.__query_head.Append(" , (select " + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + ") as remark ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name);
                this.__query_head.Append(" , (select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ") as warehouse ");
                this.__query_head.Append(" , (select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ") as shelf ");
                this.__query_head.Append(" , (select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + ") as unit ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty + " , " + _g.d.ic_trans_detail._cost + " , " + _g.d.ic_trans_detail._sum_of_cost);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in (54)) and (" + _g.d.ic_trans_detail._trans_flag + " not in (55) ) ");
                this.__query_head.Append(" group by doc_date , doc_no ,remark,item_code , item_name , warehouse , shelf , unit , qty , cost,sum_of_cost ");
                //this.__query.Append(" order by doc_no , item_code ");
                // ok
            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_Material_Balance_Bring)
            {
                // รายงานรายการสินค้ายกมาต้นปี
                this._report_name = "รายงานรายการสินค้ายกมาต้นปี";
                int[] __width = { 10, 15 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า" };
                int[] __width_2 = { 10, 10, 15, 8, 8, 8, 8, 10, 10, 10 };
                string[] __column_2 = { "วันที่เอกสาร", "เลขที่เอกสาร", "คำอธิบายรายการ", "แผนก", "คลังสินค้า", "พื้นที่เก็บ", "หน่วยนับ", "จำนวนสินค้า*", "ต้นทุน/หน่วย*", "จำนวนเงิน*" };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();
                this.__query_head.Append("");
                this.__query_head.Append(" select " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name + " , " + _g.d.ic_trans_detail._doc_date + " , " + _g.d.ic_trans_detail._doc_no + " , " + _g.d.ic_trans_detail._remark);
                this.__query_head.Append(" ,(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ") as department ");
                this.__query_head.Append(" ,(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ") as warehouse ");
                this.__query_head.Append(" ,(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " 1where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ") as shelf ");
                this.__query_head.Append(" ,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_trans_detail._unit_code + ") as unit ");
                this.__query_head.Append(" , " + _g.d.ic_trans_detail._qty + " , " + _g.d.ic_trans_detail._cost + " , " + _g.d.ic_trans_detail._sum_of_cost);
                this.__query_head.Append(" from " + _g.d.ic_trans_detail._table);
                this.__query_head.Append(" where (" + _g.d.ic_trans_detail._trans_flag + " in (54)) and (" + _g.d.ic_trans_detail._trans_flag + " not in (55) ) ");
                this.__query_head.Append(" group by  item_code , item_name, doc_date ,doc_no , remark, department , warehouse,shelf,unit , qty , cost , sum_of_cost ");
                //this.__query.Append(" order by  item_code , doc_no ");
                // ok

            }
            else if (_icType == SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial)
            {
                // รายงานยอดคงเหลือสินค้า ณ ปัจจุบัน(เฉพาะสินค้ามี Serial)
                this._report_name = "รายงานยอดคงเหลือสินค้า ณ ปัจจุบัน(เฉพาะสินค้ามี Serial)";
                int[] __width = { 10, 15, 10, 10, 10, 10, 10 };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "", "หน่วยนับ", "", "จำนวน*", "มูลค่าคงเหลือ*" };
                int[] __width_2 = { 10, 10, 10, 10, 10 };
                string[] __column_2 = { "รหัสควบคุมสินค้า", "หมายเลขทะเบียน", "คลัง", "", "พื้นที่เก็บ" };

                this._level = 2;
                for (int __i = 0; __i < __column.Length; __i++)
                {
                    this._width.Add(__width[__i]);
                    this._column.Add(__column[__i]);
                }
                for (int __j = 0; __j < __column_2.Length; __j++)
                {
                    this._width_2.Add(__width_2[__j]);
                    this._column_2.Add(__column_2[__j]);
                }
                this.__query_head = new StringBuilder();
                //_g.d.ic_trans_serial_number._table;
                //_g.d.ic_serial._table
                //this.__query_head.Append(""); _g.d.ic_inventory._ic_serial_no = 1

                //จาก service
            }
            // _condition.Dispose();
        }

        bool _view_ic__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Left, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title    : " + this._report_name, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                if (this._report_description.Length > 0)
                {
                    _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "Description : " + this._report_description, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                }
                _view1.__excelFlieName = this._report_name;
                //this._view_ic._excelFileName = "รายงานยอดการชำระเงิน";//
                //this._view_ic._maxColumn = 9;
                return true;
            }
            else if (type == SMLReport._report._objectType.Detail)
            {
                _objReport = null;
                _objReport2 = null;
                _objReport3 = null;
                _objReportNone = null;
                if (this._level == 1)
                {

                    _objReport = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                    for (int __i = 0; __i < this._column.Count; __i++)
                    {
                        this._view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._width[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column[__i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                }
                else if (this._level == 2)
                {
                    _objReport = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _objReport2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    for (int __i = 0; __i < this._column.Count; __i++)
                    {
                        this._view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._width[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column[__i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    for (int __j = 0; __j < this._column_2.Count; __j++)
                    {
                        this._view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._width_2[__j].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column_2[__j].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                }
                else if (this._level == 3)
                {
                    _objReport = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _objReport2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _objReport3 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    for (int __i = 0; __i < this._column.Count; __i++)
                    {
                        this._view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._width[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column[__i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    for (int __j = 0; __j < this._column_2.Count; __j++)
                    {
                        this._view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._width_2[__j].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column_2[__j].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    for (int __k = 0; __k < this._column_3.Count; __k++)
                    {
                        if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดสินค้า)
                        {
                            if (__k == 3 || __k == 4 || __k == 5)
                            {
                                this._view1._addColumn(_objReport3, MyLib._myGlobal._intPhase(this._width_3[__k].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column_3[__k].ToString(), "", SMLReport._report._cellAlign.Right);
                            }
                            else
                            {
                                this._view1._addColumn(_objReport3, MyLib._myGlobal._intPhase(this._width_3[__k].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column_3[__k].ToString(), "", SMLReport._report._cellAlign.Left);
                            }
                        }
                        else
                        {
                            this._view1._addColumn(_objReport3, MyLib._myGlobal._intPhase(this._width_3[__k].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column_3[__k].ToString(), "", SMLReport._report._cellAlign.Left);
                        }
                    }
                }
                else if (this._level == 4)
                {
                    _objReport = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _objReport2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _objReport3 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    _objReportNone = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    for (int __i = 0; __i < this._column.Count; __i++)
                    {
                        this._view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._width[__i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column[__i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    for (int __j = 0; __j < this._column_2.Count; __j++)
                    {
                        this._view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._width_2[__j].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._column_2[__j].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    this._view1._addColumn(_objReport3, 100, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                }

                return true;

            }
            return false;
        }


        void _view1__loadDataByThread()
        {
            _ds = null;
            _ds_2 = null;
            _ds_3 = null;
            if (_ds == null)
            {
                try
                {
                    if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า)  // รายงานยอดคงเหลือสินค้า
                    {
                        SMLProcess._icProcess __myProcess = new SMLProcess._icProcess();
                        DateTime __dateEnd = MyLib._myGlobal._convertDate(this._condition._screen._getDataStr(_g.d.resource_report._to_date));
                        SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                        this._report_description = "สิ้นสุด ณ. วันที่ : " + MyLib._myGlobal._convertDateToString(__dateEnd, true);
                        _dt_head = __process._stkStockInfoAndBalance(_g.g._productCostType.ปรกติ, this._condition._grid, null, null, __dateEnd, __dateEnd, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามสินค้า, "", "", true, false, "");
                    }
                    else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานบัญชีคุมพิเศษ) //  รายงานบัญชีคุมพิเศษสินค้า
                    {

                    }
                    else if (_icType == SMLERPReportTool._reportEnum.Result_item_import)//รายงานสรุปยอดสินค้าค้างรับ
                    {
                        SMLProcess._icProcess __myProcess = new SMLProcess._icProcess();
                        _dt_head = __myProcess._process_ic_balance("ic-00002", "ITM00002", "", " order by " + _g.d.ic_inventory._code);
                        string __test_column_list = "";
                        for (int i = 0; i < _dt_head.Columns.Count; i++)
                        {
                            __test_column_list += _dt_head.Columns[i].ToString() + " , ";
                        }

                        string[] __column = __test_column_list.Split(',');
                    }
                    else if (_icType == SMLERPReportTool._reportEnum.Result_item_export)//รายงานสรุปยอดสินค้าค้างส่ง
                    {
                        //_dt = __myProcess._process_ic_remain("", "");                        
                        SMLProcess._icProcess __myProcess = new SMLProcess._icProcess();
                        _ds = __myProcess._queryRemainStream(MyLib._myGlobal._databaseName, _g.d.ic_trans_detail._item_code + " between 'ic-00002' and 'ITM00002'", "", "Remain Qty");
                        _dt_head = _ds.Tables[0];

                    }
                    else if (_icType == SMLERPReportTool._reportEnum.Item_balance_hightest)//รายงานยอดคงเหลือสินค้า ณ จุดสูงสุด
                    {
                        SMLProcess._icProcess __myProcess = new SMLProcess._icProcess();
                        _dt_head = __myProcess._process_ic_balance("ic-00002", "ITM00002", "", " order by " + _g.d.ic_inventory._code);
                    }
                    //else if (_icType == _g.g._icreportEnum.Item_status)
                    //{
                    //    string __topQuery = "select " + _g.d.ic_inventory._group_main + ",(select " + _g.d.ic_group._name_1 + " from " + _g.d.ic_group._table + " where " + _g.d.ic_group._table + "." + _g.d.ic_group._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._group_main + " ) as group_name from " + _g.d.ic_inventory._table + _condition.__where + " group by " + _g.d.ic_inventory._group_main;
                    //    _dtTop = _myFrameWork._query(MyLib._myGlobal._databaseName, __topQuery.ToString()).Tables[0];
                    //    _dt = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
                    //}

                    // เรียกตรง
                    else
                    {
                        SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                        string __where = "";
                        string __orderBy = "";
                        if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดสินค้า) //  รายละเอียดสินค้า
                        {

                            if (this._condition != null)
                            {
                                // from grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                                __getWhere = __getWhere.Replace("where (", "");

                                __where = " and " + __getWhere;

                                string __test = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __test = __test.Replace("from_group=", ""); __test = __test.Replace("to_group=", "");
                                __test = __test.Replace("from_brand=", ""); __test = __test.Replace("to_brand=", "");
                                __test = __test.Replace("from_type=", ""); __test = __test.Replace("to_type=", "");
                                string[] __split = __test.Split(',');
                                string __where_group = ""; string __where_brand = ""; string __where_type = "";

                                //if (__split[0] != "''" && __split[1] != "''") __where_group = " and (" + _g.d.ic_inventory._group_main + " between " + __split[0] + " and " + __split[1] + ")";
                                //else
                                //{
                                //    if (__split[0] != "''") __where_group = " and (" + _g.d.ic_inventory._group_main + " = " + __split[0] + ")";
                                //    if (__split[1] != "''") __where_group = " and (" + _g.d.ic_inventory._group_main + " = " + __split[1] + ")";
                                //}
                                //if (__split[2] != "''" && __split[3] != "''") __where_brand = " and (" + _g.d.ic_inventory._item_brand + " between " + __split[2] + " and " + __split[3] + ")";
                                //else
                                //{
                                //    if (__split[2] != "''") __where_brand = " and (" + _g.d.ic_inventory._item_brand + " = " + __split[2] + ")";
                                //    if (__split[3] != "''") __where_brand = " and (" + _g.d.ic_inventory._item_brand + " = " + __split[3] + ")";
                                //}
                                //if (__split[4] != "''" && __split[5] != "''") __where_type = " and (" + _g.d.ic_inventory._item_type + " between " + __split[4] + " and " + __split[5] + ")";
                                //else
                                //{
                                //    if (__split[4] != "''") __where_type = " and (" + _g.d.ic_inventory._item_type + " = " + __split[4] + ")";
                                //    if (__split[5] != "''") __where_type = " and (" + _g.d.ic_inventory._item_type + " = " + __split[5] + ")";
                                //}

                                //__where += __where_group + __where_brand + __where_type;
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy.Replace("\a","");

                                if (__orderBy.Contains(_g.d.ic_inventory_detail._table))
                                {
                                    //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                    //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                    __orderBy = __orderBy.Replace('"', ' ');
                                    //__orderBy = " order by " + __orderBy;
                                }
                                else
                                {

                                }

                            }


                            int __a = this.__query_head.ToString().IndexOf("order");
                            //this.__query_head.Append(__where);
                            this.__query_head.Insert(__a, __where + " ");


                            //this.__query_head.Append(__orderBy);


                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this.__query_head.ToString()));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this.__query_detail_1.ToString()));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this.__query_detail_2.ToString()));
                            __myquery.Append("</node>");

                            ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                            if (_getData.Count != 0)
                            {
                                _dt_head = ((DataSet)_getData[0]).Tables[0];
                                _dt_detail_1 = ((DataSet)_getData[1]).Tables[0];
                                _dt_detail_2 = ((DataSet)_getData[2]).Tables[0];
                            }

                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรายละเอียดบาร์โค๊ด)
                        {  //  รายงานบาร์โค้ดสินค้า

                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                __getWhere = __getWhere.Replace("item_code", "ic_code");
                                if (__getWhere.Length > 0) __where += " where " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                //if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                //else
                                //{
                                //    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                //    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                //}

                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");

                                this.__query_head.Insert(__a, __where + " ");
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query_head.Append(__orderBy);
                            }

                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());

                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสูตรสีผสม || _icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสูตรสินค้าชุด)
                        {
                            //     รายงานสูตรสีผสม 

                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                __getWhere = __getWhere.Replace('"', ' ');
                                __getWhere = __getWhere.Replace("item_code", " ( select code from ic_inventory where code = ic_inventory_set_detail.ic_set_code ) ");

                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                //if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                //else
                                //{
                                //    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                //    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                //}

                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");

                                this.__query_head.Insert(__a, __where + " ");
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }


                            //string __before = this.__query_head.ToString();
                            //string __after = MyLib._myUtil._convertTextToXml(this.__query_head.ToString());
                            //StringBuilder __myquery = new StringBuilder();
                            //__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this.__query_head.ToString()));
                            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(this.__query_detail_1.ToString()));
                            //__myquery.Append("</node>");

                            //ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());


                            //if (_getData.Count != 0)
                            //{
                            //    _dt_head = ((DataSet)_getData[0]).Tables[0];
                            //    _dt_detail_1 = ((DataSet)_getData[1]).Tables[0];
                            //}
                            //_ds = new DataSet(); _ds_2 = new DataSet();


                            this.__query_head.Append(" limit 50 ");


                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            //_ds = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __after, "Test");
                            _dt_head = _ds.Tables[0];

                            //try
                            //{
                            //    //string ddd = "select * from ic_inventory";
                            //    _ds = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, this.__query_head.ToString(), "Head");
                            //    _dt_head = _ds.Tables[0];

                            //    _ds_2 = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, this.__query_detail_1.ToString(), "Detail");
                            //    //_ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            //    _dt_detail_1 = _ds_2.Tables[0];
                            //}
                            //catch (Exception e)
                            //{
                            //    MessageBox.Show(e.Message);
                            //}




                        }
                        else if ((_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานราคาขายสินค้า) || (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานราคาซื้อสินค้า))
                        {
                            //     รายงานราคาขาย รายงานราคาซื้อ

                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                __getWhere = __getWhere.Replace("item_code", "ic_code");
                                if (__getWhere.Length > 0) __where += " where " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }


                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");

                                this.__query_head.Insert(__a, __where + " ");
                                // this.__query.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());

                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Item_Giveaway)
                        {
                            //  รายงานของแถม
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                __getWhere = __getWhere.Replace("item_code", "ic_code");
                                if (__getWhere.Length > 0) __where += " where " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''")
                                {
                                    __where_doc_date = " ( select distinct date_begin from ic_purchase_permium where (permium_code = ic_purchase_permium_list.permium_code)) " + " between " + __split[0] + " and " + __split[1];
                                    __where_doc_date += " or " + " ( select distinct date_end from ic_purchase_permium where (permium_code = ic_purchase_permium_list.permium_code)) " + " between " + __split[0] + " and " + __split[1];

                                }
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " ( select distinct date_begin from ic_purchase_permium where (permium_code = ic_purchase_permium_list.permium_code)) " + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " ( select distinct date_end from ic_purchase_permium where (permium_code = ic_purchase_permium_list.permium_code)) " + " = " + __split[1];
                                }

                                __where += " where " + __where_doc_date;  //  เพราะ  query  ไม่มี  where

                                int __a = this.__query_head.ToString().IndexOf("order");

                                this.__query_head.Insert(__a, __where + " ");
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());

                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Item_by_serial) //  รายงานสินค้าแบบมี Serial
                        {
                            string __where_in_doc_date = ""; string __where_insure_expire = ""; string __where_trans_flag = "";
                            if (this._condition != null)
                            {
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                                __getWhere = __getWhere.Replace(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code, _g.d.ic_trans_detail._item_code);
                                __where = " and " + __getWhere;
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date_import=", ""); __top_screen = __top_screen.Replace("to_date_import=", "");
                                __top_screen = __top_screen.Replace("from_date_end_insulance=", ""); __top_screen = __top_screen.Replace("to_date_end_insulance=", "");
                                __top_screen = __top_screen.Replace("status_item=", "");
                                string[] __split = __top_screen.Split(',');

                                if (__split[4] != "''")
                                {
                                    // 18,44,45,46,47,50,54,56,60
                                    if (__split[4] == "1") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (18 , 54 , 60))"; // ในสต๊อก
                                    if (__split[4] == "2") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (50 , 56))"; // เบิกแล้ว
                                    if (__split[4] == "3") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (44 , 46))"; // ขายแล้ว
                                    if (__split[4] == "4") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (45 , 47))"; // ยกเลิก
                                }
                                __where += __where_in_doc_date + __where_insure_expire + __where_trans_flag;
                                __orderBy = " " + this._condition._extra._getOrderBy();

                                //if (__orderBy.Contains(_g.d.ic_inventory._table))
                                //{
                                __orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                __orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                if (__orderBy == _g.d.ic_inventory._code) __orderBy = _g.d.ic_trans_detail._item_code;
                                if (__orderBy == _g.d.ic_inventory._name_1) __orderBy = _g.d.ic_trans_detail._item_name;
                                __orderBy = " order by " + __orderBy;
                                //}
                                //else
                                //{

                                //}

                            }

                            this.__query_head.Append(__where);
                            this.__query_head.Append(__orderBy);
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Serial_number)
                        {

                            string __where_in_doc_date = ""; string __where_item_code = "";
                            string __whereDetail = "";

                            if (this._condition != null)
                            {
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number);

                                __getWhere = __getWhere.Replace(_g.d.ic_serial._table + "." + _g.d.ic_serial._serial_number, _g.d.ic_serial._serial_number);

                                if (__getWhere.Length > 0)
                                {
                                    __where = (__where.Length > 0) ? " and " : " where " + __getWhere;

                                }


                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace(_g.d.resource_report._from_date + "=", ""); __top_screen = __top_screen.Replace(_g.d.resource_report._to_date + "=", "");
                                __top_screen = __top_screen.Replace(_g.d.resource_report._from_item_code + "=", ""); __top_screen = __top_screen.Replace(_g.d.resource_report._to_item_code + "=", "");
                                //__top_screen = __top_screen.Replace("status_item=", "");
                                string[] __split = __top_screen.Split(',');

                                if (__split[0] != "''" && __split[1] != "''")
                                {
                                    __where_in_doc_date = " " + _g.d.resource_report._date_import + " between " + __split[0] + " and " + __split[1] + " ";
                                    __where = __where + ((__where.Length > 0) ? " and " : " where ") + __where_in_doc_date;
                                }

                                if (__split[2] != "''" && __split[3] != "''")
                                {
                                    __where_item_code = " " + _g.d.ic_serial._ic_code + "between " + __split[2] + " and " + __split[3] + " ";
                                    __where = __where + ((__where.Length > 0) ? " and " : " where ") + __where_item_code;
                                }

                                //if (__split[4] != "''")
                                //{
                                //    // 18,44,45,46,47,50,54,56,60
                                //    if (__split[4] == "1") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (18 , 54 , 60))"; // ในสต๊อก
                                //    if (__split[4] == "2") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (50 , 56))"; // เบิกแล้ว
                                //    if (__split[4] == "3") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (44 , 46))"; // ขายแล้ว
                                //    if (__split[4] == "4") __where_trans_flag = " and (" + _g.d.ic_trans_detail._trans_flag + " in (45 , 47))"; // ยกเลิก
                                //}

                                //__where = " where " + __where_in_doc_date + __where_item_code;

                                //__orderBy = " " + this._condition._extra._getOrderBy();

                                ////if (__orderBy.Contains(_g.d.ic_inventory._table))
                                ////{
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //if (__orderBy == _g.d.ic_inventory._code) __orderBy = _g.d.ic_trans_detail._item_code;
                                //if (__orderBy == _g.d.ic_inventory._name_1) __orderBy = _g.d.ic_trans_detail._item_name;
                                //__orderBy = " order by " + __orderBy;
                                //}
                                //else
                                //{

                                //}
                                string __repalceValue = "";
                                if (__getWhere.Length > 0)
                                {
                                    __repalceValue = __getWhere.Replace(_g.d.ic_serial._serial_number, _g.d.ic_trans_serial_number._serial_number);
                                }
                                __whereDetail = (__getWhere.Length > 0) ? __where.Replace(__getWhere, __repalceValue) : __getWhere;
                            }

                            this.__query_head.Append(__where);
                            this.__query_head.Append(__orderBy);

                            this.__query_detail_1.Append(__whereDetail);

                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];

                            _ds_2 = _myFrameWork._query(MyLib._myGlobal._databaseName, __query_detail_1.ToString());
                            _dt_detail_1 = _ds_2.Tables[0];


                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Item_status) //  รายงานสินค้าแบบมี Item Status
                        {

                            if (_condition != null)
                            {

                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;

                                // Screen
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                __top_screen = __top_screen.Replace("from_group=", ""); __top_screen = __top_screen.Replace("to_group=", "");
                                string[] __split = __top_screen.Split(',');
                                string __where_doc_date = ""; string __where_group = "";
                                string __field_doc_date = ""; string __field_group = "";
                                __field_doc_date = "(select " + _g.d.ic_trans_detail._doc_date + " from " + _g.d.ic_trans_detail._table +
                                    " where " + "( " + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_inventory._code + " )" + " and " +
                                    "( " + _g.d.ic_trans_detail._trans_flag + " in (12 , 18 , 44 , 50)" + " ) limit(1)" + ")";
                                __field_group = _g.d.ic_inventory._group_main;
                                if ((__split[0] != "''") && (__split[1] != "''"))
                                {
                                    __where_doc_date = " and " + "(" + __field_doc_date + " between " + __split[0] + " and " + __split[1] + ")";
                                }
                                else
                                {
                                    if (__split[0] != "''") { __where_doc_date = " and " + "(" + __field_doc_date + " = " + __split[0] + ")"; }
                                    if (__split[1] != "''") { __where_doc_date = " and " + "(" + __field_doc_date + " = " + __split[1] + ")"; }
                                }

                                if ((__split[2] != "''") && (__split[3] != "''"))
                                {
                                    __where_group = " and " + "(" + __field_group + " beteen " + __split[2] + " and " + __split[3] + ")";
                                }
                                else
                                {
                                    if (__split[2] != "''") { __where_group = "and " + __field_group + " = " + __split[2] + "(" + ")"; }
                                    if (__split[3] != "''") { __where_group = "and " + __field_group + " = " + __split[3] + "(" + ")"; }
                                }

                                __where += __where_doc_date + __where_group;
                                __orderBy = " " + this._condition._extra._getOrderBy();

                                __orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                __orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                __orderBy = " order by " + __orderBy;
                            }
                            this.__query_head.Append(__where);
                            this.__query_head.Append(__orderBy);
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ) //  รายงานผลต่างจากการตรวจนับ
                        {

                            if (_condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_warehouse=", ""); __top_screen = __top_screen.Replace("to_warehouse=", "");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                __top_screen = __top_screen.Replace("from_docno=", ""); __top_screen = __top_screen.Replace("to_docno=", "");
                                string[] __split = __top_screen.Split(',');
                                string __where_warehouse = ""; string __where_doc_date = ""; string __where_doc_no = "";
                                if ((__split[0] != "''") && (__split[1] != "''"))
                                {
                                    __where_warehouse = " and " + _g.d.ic_trans_detail._wh_code + " between " + __split[0] + " and " + __split[1];
                                }
                                else
                                {
                                    if (__split[0] != "''") { __where_warehouse = " and " + _g.d.ic_trans_detail._wh_code + " = " + __split[0]; }
                                    if (__split[1] != "''") { __where_warehouse = " and " + _g.d.ic_trans_detail._wh_code + " = " + __split[1]; }
                                }

                                if ((__split[2] != "''") && (__split[3] != "''"))
                                {
                                    __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[2] + " and " + __split[3];
                                }
                                else
                                {
                                    if (__split[2] != "''") { __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[2]; }
                                    if (__split[3] != "''") { __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[3]; }
                                }

                                if ((__split[4] != "''") && (__split[5] != "''"))
                                {
                                    __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " between " + __split[4] + " and " + __split[5];
                                }
                                else
                                {
                                    if (__split[4] != "''") { __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[4]; }
                                    if (__split[5] != "''") { __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[5]; }
                                }
                                __orderBy = " " + this._condition._extra._getOrderBy();

                                __orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                __orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                __orderBy = " order by " + __orderBy;

                            }

                            this.__query_head.Append(__where);
                            this.__query_head.Append(__orderBy);
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป)  //  รายงานการรับสินค้าสำเร็จรูป
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                                __getWhere = __getWhere.Replace("ic_inventory.code", "item_code");
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;

                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                //__top_screen = __top_screen.Replace("from_docno=", ""); __top_screen = __top_screen.Replace("to_docno=", "");
                                //__top_screen = __top_screen.Replace("from_department=", ""); __top_screen = __top_screen.Replace("to_department=", "");
                                //__top_screen = __top_screen.Replace("from_warehouse=", ""); __top_screen = __top_screen.Replace("to_warehouse=", "");
                                //__top_screen = __top_screen.Replace("from_shelf=", ""); __top_screen = __top_screen.Replace("to_shelf=", "");
                                __top_screen = __top_screen.Replace("show_cancel_document=", "");
                                string[] __split = __top_screen.Split(',');
                                string __where_doc_date = ""; string __where_doc_no = ""; string __where_department = ""; string __where_warhouse = ""; string __where_shelf = "";
                                string __where_cancel_document = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }
                                if (__split[2] == "1")
                                { __where_cancel_document = " or (trans_flag in (61)) "; }

                                //if ((__split[2] != "''") && __split[3] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " between " + __split[2] + " and " + __split[3];
                                //else
                                //{
                                //    if (__split[2] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[2];
                                //    if (__split[3] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[3];
                                //}

                                //string __field_department = "(select " + _g.d.erp_department_list._code + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ")";
                                //if ((__split[4] != "''") && __split[5] != "''") __where_department = " and " + __field_department + " between " + __split[4] + " and " + __split[5];
                                //else
                                //{
                                //    if (__split[4] != "''") __where_department = " and " + __field_department + " = " + __split[4];
                                //    if (__split[5] != "''") __where_department = " and " + __field_department + " = " + __split[5];
                                //}

                                //string __field_warehouse = "(select " + _g.d.ic_warehouse._code + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ")";
                                //if ((__split[6] != "''") && __split[7] != "''") __where_warhouse = " and " + __field_warehouse + " between " + __split[6] + " and " + __split[7];
                                //else
                                //{
                                //    if (__split[6] != "''") __where_warhouse = " and " + __field_warehouse + " = " + __split[6];
                                //    if (__split[7] != "''") __where_warhouse = " and " + __field_warehouse + " = " + __split[7];
                                //}

                                //string __field_shelf = "(select " + _g.d.ic_shelf._code + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ")";
                                //if ((__split[8] != "''") && __split[9] != "''") __where_shelf = " and " + __field_shelf + " between " + __split[8] + " and " + __split[9];
                                //else
                                //{
                                //    if (__split[8] != "''") __where_shelf = " and " + __field_shelf + " = " + __split[8];
                                //    if (__split[9] != "''") __where_shelf = " and " + __field_shelf + " = " + __split[9];
                                //}
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();

                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;

                                __where += __where_doc_date + __where_doc_no + __where_department + __where_warhouse + __where_shelf + __where_cancel_document;
                                int __a = this.__query_head.ToString().IndexOf("order");

                                this.__query_head.Insert(__a, __where);
                                //this.__query_head.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Import_Item_ready)  //  รายงานการยกเลิกรับสินค้าสำเร็จรูป
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;

                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                __top_screen = __top_screen.Replace("from_docno=", ""); __top_screen = __top_screen.Replace("to_docno=", "");
                                __top_screen = __top_screen.Replace("from_department=", ""); __top_screen = __top_screen.Replace("to_department=", "");
                                __top_screen = __top_screen.Replace("from_warehouse=", ""); __top_screen = __top_screen.Replace("to_warehouse=", "");
                                __top_screen = __top_screen.Replace("from_shelf=", ""); __top_screen = __top_screen.Replace("to_shelf=", "");
                                string[] __split = __top_screen.Split(',');
                                string __where_doc_date = ""; string __where_doc_no = ""; string __where_department = ""; string __where_warhouse = ""; string __where_shelf = "";

                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                if ((__split[2] != "''") && __split[3] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " between " + __split[2] + " and " + __split[3];
                                else
                                {
                                    if (__split[2] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[2];
                                    if (__split[3] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[3];
                                }

                                string __field_department = "(select " + _g.d.erp_department_list._code + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + " = " + _g.d.ic_trans_detail._department_code + ")";
                                if ((__split[4] != "''") && __split[5] != "''") __where_department = " and " + __field_department + " between " + __split[4] + " and " + __split[5];
                                else
                                {
                                    if (__split[4] != "''") __where_department = " and " + __field_department + " = " + __split[4];
                                    if (__split[5] != "''") __where_department = " and " + __field_department + " = " + __split[5];
                                }

                                string __field_warehouse = "(select " + _g.d.ic_warehouse._code + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + " = " + _g.d.ic_trans_detail._wh_code + ")";
                                if ((__split[6] != "''") && __split[7] != "''") __where_warhouse = " and " + __field_warehouse + " between " + __split[6] + " and " + __split[7];
                                else
                                {
                                    if (__split[6] != "''") __where_warhouse = " and " + __field_warehouse + " = " + __split[6];
                                    if (__split[7] != "''") __where_warhouse = " and " + __field_warehouse + " = " + __split[7];
                                }

                                string __field_shelf = "(select " + _g.d.ic_shelf._code + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + " = " + _g.d.ic_trans_detail._shelf_code + ")";
                                if ((__split[8] != "''") && __split[9] != "''") __where_shelf = " and " + __field_shelf + " between " + __split[8] + " and " + __split[9];
                                else
                                {
                                    if (__split[8] != "''") __where_shelf = " and " + __field_shelf + " = " + __split[8];
                                    if (__split[9] != "''") __where_shelf = " and " + __field_shelf + " = " + __split[9];
                                }
                                __orderBy = " " + this._condition._extra._getOrderBy();

                                __orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                __orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                __orderBy = " order by " + __orderBy;

                                __where += __where_doc_date + __where_doc_no + __where_department + __where_warhouse + __where_shelf;
                                int __a = this.__query_head.ToString().IndexOf("order");

                                this.__query_head.Insert(__a, __where + " ");
                                this.__query_head.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา)  //  รายงานสินค้าและวัตถุดิบ คงเหลือ  ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                __top_screen = __top_screen.Replace("show_cancel_document=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = ""; string __wher_cancel_document = "";
                                if (__split[0] == "1")
                                { __wher_cancel_document = " or (trans_flag = 55) "; }
                                //if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                //else
                                //{
                                //    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                //    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                //}

                                __where += __where_doc_date + __wher_cancel_document;
                                int __a = this.__query_head.ToString().IndexOf("order");

                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query_head.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Item_and_Staple)  //  รายงานยกเลิกสินค้าและวัตถุดิบ คงเหลือ  ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ)  // รายงานโอนสินค้าและวัตถุดิบ   ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                __top_screen = __top_screen.Replace("show_cancel_document=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                string __where_cancel_document = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }
                                if (__split[2] == "1")
                                { __where_cancel_document = " or (trans_flag in (71,73)) "; }


                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                __where += __where_doc_date + __where_cancel_document;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);

                                //this.__query.Append(__orderBy);

                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Transfer_Item_and_material)  // รายงานยกเลิกโอนสินค้าและวัตถุดิบ   ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);

                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ)  // รายงานเบิกสินค้าและวัตถุดิบ   ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                __top_screen = __top_screen.Replace("from_docno=", ""); __top_screen = __top_screen.Replace("to_docno=", "");
                                __top_screen = __top_screen.Replace("show_cancel_document=", "");
                                string __where_cancel_document = "";
                                string[] __split = __top_screen.Split(','); string __where_doc_date = ""; string __where_doc_no = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }
                                if ((__split[2] != "''") && __split[3] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " between " + __split[2] + " and " + __split[3];
                                else
                                {
                                    if (__split[2] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[2];
                                    if (__split[3] != "''") __where_doc_no = " and " + _g.d.ic_trans_detail._doc_no + " = " + __split[3];
                                }
                                if (__split[4] == "1")
                                { __where_cancel_document = " or trans_flag = 57 "; }

                                __where += __where_doc_date + __where_doc_no + __where_cancel_document + " ";
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;

                                //this.__query.Remove(__a, this.__query.Length);
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Withdraw_Item_Staple)  // รายงานยกเลิกเบิกสินค้าและวัตถุดิบ   ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                __where += __where_doc_date + " ";
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;

                                //this.__query.Remove(__a, this.__query.Length);
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ)  // รายงานรับคืนเบิกสินค้าและวัตถุดิบ   ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                string __where_cancel_document = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }
                                if (__split[2] == "1")
                                { __where_cancel_document = " or (trans_flag = 59) "; }

                                __where += __where_doc_date + __where_cancel_document + " ";
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;

                                //this.__query.Remove(__a, this.__query.Length);
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Refunded_Withdraw_Item_Staple)  // รายงานยกเลิกรับคืนเบิกสินค้าและวัตถุดิบ   ยังไม่ได้ทำ  Condition
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                __where += __where_doc_date + " ";
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;

                                //this.__query.Remove(__a, this.__query.Length);
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน)  //  รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ  (เกิน)  
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = ""; string __where_cancel_document = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }
                                if (__split[2] == "1")
                                { __where_cancel_document = " or (trans_flag = 67) "; }

                                __where += __where_doc_date + __where_cancel_document;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Implement_Item_and_Staple_Over)  //  รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ  (เกิน)  
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด)  //  รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ  (ขาด)
                        {
                            if (this._condition != null)
                            {
                                //Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = ""; string __where_cancel_document = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }
                                if (__split[2] == "69")
                                {
                                    __where_cancel_document = " or (trans_flag = 69) ";
                                }

                                __where += __where_doc_date + __where_cancel_document;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Implement_Item_and_Staple_Over)  //  รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ  (เกิน)
                        {
                            if (this._condition != null)
                            {
                                //Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                        else if (_icType == SMLERPReportTool._reportEnum.Cancel_Implement_Item_and_Staple_Minus)  //  รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ  (ขาด)
                        {
                            if (this._condition != null)
                            {
                                // Grid
                                string __getWhere = this._condition._grid._createWhere(_g.d.ic_trans_detail._item_code);
                                if (__getWhere.Length > 0) __where += " and " + __getWhere;
                                //top screen 
                                string __top_screen = this._condition._screen._createQueryForDatabase()[2].ToString().Replace("null", "''");
                                __top_screen = __top_screen.Replace("from_date=", ""); __top_screen = __top_screen.Replace("to_date=", "");
                                string[] __split = __top_screen.Split(','); string __where_doc_date = "";
                                if ((__split[0] != "''") && __split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " between " + __split[0] + " and " + __split[1];
                                else
                                {
                                    if (__split[0] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[0];
                                    if (__split[1] != "''") __where_doc_date = " and " + _g.d.ic_trans_detail._doc_date + " = " + __split[1];
                                }

                                __where += __where_doc_date;
                                int __a = this.__query_head.ToString().IndexOf("order");
                                this.__query_head.Insert(__a, __where);
                                //__orderBy = " " + this._condition._whereControl._getOrderBy();
                                //__orderBy = "" + __orderBy.Substring(__orderBy.IndexOf(".") + 1);
                                //__orderBy = __orderBy.Substring(0, __orderBy.Length - 1);
                                //__orderBy = " order by " + __orderBy;
                                //this.__query.Append(__where);
                                //this.__query.Append(__orderBy);
                            }
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }

                        else
                        {
                            _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query_head.ToString());
                            _dt_head = _ds.Tables[0];
                        }
                    }


                }
                catch (Exception __e)
                {
                    string __test_error = __e.ToString();
                    this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
                    this._view1._loadDataByThreadSuccess = false;
                }
                this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            }
            this._view1._loadDataByThreadSuccess = true;

            //return true;
        }


        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (_ds != null || _dt_head != null)
            {
                //_ds = null;
                //_dt.Rows.Clear();
                _width.Clear();
                _width_2.Clear();
                _width_3.Clear();
                _column.Clear();
                _column_2.Clear();
                _column_3.Clear();
            }

            this._config();
            this._view1._buildReport(SMLReport._report._reportType.Standard);
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        private Boolean _check_submit = false;
        public void _showCondition()
        {
            //string __page = _icType.ToString();

            //_condition.__page = __page;
            //_condition.._clear();

            _ds = null;
            this._dt_head = null;

            if (this._condition == null)
            {
                //_condition = new SMLERPReportTool._conditionScreen(_icType, "");
                this._condition = new SMLERPReportTool._conditionScreen(_icType, this._report_name);

                this._condition._extra._tableName = _g.d.ic_inventory._table;
                this._condition._extra._searchTextWord.Visible = false;
                this._condition._extra._orderByComboBox.Visible = false;
                this._condition._extra._orderByComboBox.Dispose();
                //switch (this._icType)
                //{
                //    //  มี order by
                //    case _g.g._icreportEnum.Item_master:  // รายละเอียดสินค้า
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    case _g.g._icreportEnum.Item_Barcode:  // บาร์โค้ด
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    case _g.g._icreportEnum.Item_by_serial:  //  รายงานสินค้าแบบมี  Serial
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    case _g.g._icreportEnum.Item_status:  //  รายงานสถานะสินค้า
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    case _g.g._icreportEnum.Diff_from_count:  //  รายยงาสนผลต่างจากการตรวจนับ
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    case _g.g._icreportEnum.Import_Item_ready:  //  รายงานการรับสินค้าสำเร็จรูป
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //          this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        //this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    case _g.g._icreportEnum.Transfer_Item_and_material:  //  รายงานการโอนสินค้าและวัตถุดิบ
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        //this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    case _g.g._icreportEnum.Cancel_Import_Item_ready:  //  รายงานการยกเลิกรับสินค้าสำเร็จรูป
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //        break;
                //    //case _g.g._icreportEnum.Item_Material_Balance_Bring:  //  รายงานสินค้าและวัตถุดิบ  คงเหลือยกมา
                //    //    this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //    //    this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());
                //    //    break;

                //    //  ไม่มี order by
                //    case _g.g._icreportEnum.Withdraw_Item_Staple:  //  รายงานการเบิกสินค้าและวัตถุดิบ
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Cancel_Withdraw_Item_Staple:  //  รายงานการยกเลิกเบิกสินค้าและวัตถุดิบ
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Refunded_Withdraw_Item_Staple:  //  รายงานการรับคืนสินค้าและวัตถุดิบ จากการเบิก
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Cancel_Refunded_Withdraw_Item_Staple:  //  รายงานการยกเลิกรับคืนสินค้าและวัตถุดิบ จากการเบิก
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;

                //    case _g.g._icreportEnum.Cancel_Transfer_Item_and_material:  //  รายงานการยกเลิกโอนสินค้าและวัตถุดิบ
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;


                //    case _g.g._icreportEnum.Item_and_Staple:  //  รายงานสินค้าและวัตถุดิบ  คงเหลือยกมา
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Cancel_Item_and_Staple:  //  รายงานยกเลิกสินค้าและวัตถุดิบ  คงเหลือยกมา
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Implement_Item_and_Staple_Over:  //  รายงานปรับปรุงสินค้าและวัตถุดิบ (เกิน)
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Cancel_Implement_Item_and_Staple_Over:  //  รายงานยกเลิกปรับปรุงสินค้าและวัตถุดิบ (เกิน)
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Implement_Item_and_Staple_Minus:  //  รายงานปรับปรุงสินค้าและวัตถุดิบ (ขาด)
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;
                //    case _g.g._icreportEnum.Cancel_Implement_Item_and_Staple_Minus:  //  รายงานยกเลิกปรับปรุงสินค้าและวัตถุดิบ (ขาด)
                //        this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //        this._condition._whereControl._searchTextWord.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Visible = false;
                //        this._condition._whereControl._orderByComboBox.Dispose();
                //        break;


                //}
                //this._condition._whereControl._tableName = _g.d.ic_inventory._table;
                //this._condition._whereControl._addFieldComboBox(this._ic_column_order_by());

                // new ArrayList ใหม่  เพราะ ป้องกันการ  add width column  เพิ่ม
                _width.Clear(); _width_2.Clear(); _width_3.Clear();
                _column.Clear(); _column_2.Clear(); _column_3.Clear();

                this._condition.Size = new Size(600, 600);
            }

            this._condition.ShowDialog();
            if (this._condition._processClick)
            {

                this._check_submit = this._condition._processClick;
                // new ArrayList ใหม่  เพราะ ป้องกันการ  add width column  เพิ่ม
                _width.Clear(); _width_2.Clear(); _width_3.Clear();
                _column.Clear(); _column_2.Clear(); _column_3.Clear();
                this._config();
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }

        string _cal_available_for_sale(string qty_balance, string qty_remain_recieve, string qty_remain_send)
        {
            string __result = "";
            double __available = 0;
            double __balance = 0;
            double __recieve = 0;
            double __send = 0;

            try
            {
                if (qty_balance != "" || qty_balance.Length > 0)
                {
                    __balance = double.Parse(qty_balance.ToString());

                }
                if (qty_remain_recieve != "" || qty_remain_recieve.Length > 0)
                {
                    __recieve = double.Parse(qty_remain_recieve.ToString());

                }
                if (qty_remain_send != "" || qty_remain_send.Length > 0)
                {
                    __send = double.Parse(qty_remain_send.ToString());

                }
                __available = (__balance + __recieve) - __send;

                __result = __available.ToString("##.##");
                if (__result.Equals("")) __result = "0";

            }
            catch (Exception e)
            {
                string __chk_error = e.Message.ToString();
            }
            return __result;
        }

        public string _convert_item_status(string __status)
        {
            string __result = "";
            try
            {
                if (__status == "0") __result = "ยกเลิก";
                else if (__status == "1") __result = "ใช้งาน";
            }
            catch (Exception e)
            {
                string __error = e.Message;
            }
            return __result;
        }

        public string _convert_tax_type(string __tax_type)
        {
            string __result = "";
            try
            {
                if (__tax_type == "0") __result = "ภาษีมูลค่าเพิ่ม";
                else if (__tax_type == "1") __result = "ภาษีอัตราศูนย์";
                else if (__tax_type == "2") __result = "ยกเว้นภาษี";
                else if (__tax_type == "") __result = "";
                //if (__tax_type == 0) __result = "แยกนอก";
                //if (__tax_type == 1) __result = "รวมใน";
            }
            catch (Exception e)
            { }
            return __result;
        }

        public string _convert_cost_type(string __cost_type)
        {
            string __result = "";
            if (__cost_type == "0") __result = "ต้นทุนเฉลี่ย";
            else if (__cost_type == "1") __result = "ต้นทุน Fifo";
            return __result;
        }

        public string _convert_sale_type(string __sale_type)
        {
            string __result = "";
            if (__sale_type == "0") __result = "ขายสด";
            if (__sale_type == "2") __result = "ขายเชื่อ";
            return __result;
        }

        public string _convert_item_type(string __item_type)
        {
            string __result = "";

            if (__item_type == "0") __result = "สินค้าทั่วไป";
            else if (__item_type == "1") __result = "ค่าบริการ (ไม่ทำสต๊อก)";
            else if (__item_type == "2") __result = "สินค้าให้เช่า";
            else if (__item_type == "3") __result = "สินค้าชุด (ไม่ทำสต๊อก)";
            else if (__item_type == "4") __result = "สินค้าฝากขาย";
            else if (__item_type == "5") __result = "สูตรการผลิต";
            else if (__item_type == "6") __result = "สีผสม";
            else __result = "";
            return __result;
        }
        double _convertNumber(string dataSult)
        {
            double __result = 0.00;
            Double __amount = 0.00;
            try
            {
                __amount = Double.Parse(dataSult);
                if (__amount > 0)
                {
                    __result = __amount;
                }
                else
                {
                    __result = 0;
                }
            }
            catch
            {
            }
            return __result;
        }
        public string _check_value_str(string __valueStr)
        {
            string __result = "";
            if (__valueStr.Length < 10)
            {
                for (int __loop = 0; __loop < 10; __loop++)
                {
                    if (__loop == 0)
                    {
                        __result = " " + __valueStr;
                    }
                    else
                    {
                        if (__result.Length == 0)
                        {
                            return __result;
                        }
                        else
                        {
                            __result = " " + __result;
                        }
                    }
                }
            }
            return __result;
        }
        public string _check_value_detail(string result1, string result2, string result3, int _intFormat)
        {
            string __result = "";
            try
            {
                if (_intFormat == 0)
                {
                    if (result2 != "null" && result3 != "null")
                    {
                        if (result2 != "" && result3 != "")
                        {
                            __result = result3 + "(" + result2 + ")";
                        }
                        else
                        {
                            if (result2 != "" && result3 == "")
                            {
                                __result = result2;
                            }
                        }
                    }
                    else if (result2 != "null" && result3 == "null")
                    {
                        __result = result2;
                    }
                    if (__result.Length > 0)
                    {
                        __result = result1 + ":" + __result;
                    }
                }
                else
                {
                    if (__result.ToString() == "0")
                    {
                        __result = result1 + ":" + "สินค้าทั่วไป";
                    }
                    else if (__result.ToString() == "1")
                    {
                        __result = result1 + ":" + "ค่าบริการ(ไม่ทำสต๊อก)";
                    }
                    else if (__result.ToString() == "2")
                    {
                        __result = result1 + ":" + "สินค้าให้เช่า";
                    }
                    else if (__result.ToString() == "3")
                    {
                        __result = result1 + ":" + "สินค้าฝากขาย";
                    }
                    else if (__result.ToString() == "4")
                    {
                        __result = result1 + ":" + "สินค้าชุด(ไม่ทำสต๊อก)";
                    }
                    else if (__result.ToString() == "5")
                    {
                        __result = result1 + ":" + "สินค้าชนิดพิเศษ";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        string _check_value_is_zero(string __valueNumber)
        {
            string __result = "";
            if (__valueNumber.Length == 0 || __valueNumber == "")
            {
                __result = "0";
            }
            else
            {
                if (!__valueNumber.Contains(".")) __result = __valueNumber + ".00";
                else
                {
                    string __before_dot = __valueNumber.Substring(0, __valueNumber.IndexOf("."));
                    string __after_dot = __valueNumber.Substring(__valueNumber.IndexOf(".") + 1);
                    if (__after_dot.Length == 1)
                    {
                        __result = __before_dot + "." + __after_dot + "0";
                    }
                    else if (__after_dot.Length > 1)
                    {

                        __result = double.Parse(__valueNumber).ToString("##.##");
                    }
                }
            }
            return __result;
        }

        string _checkStatus(int __status)
        {
            string __result = "";
            if (__status == 0) __result = "ไม่ใช้";
            else if (__status > 0) __result = "ใช้";
            return __result;
        }

        public string _check_masterName(string result1, string result2)
        {
            string __result = "";
            try
            {
                if (result1 != "null" && result2 != "null")
                {
                    if (result1 != "" && result2 != "")
                    {
                        __result = result1 + "~" + result2 + "";
                    }
                    else
                    {
                        if (result1 != "" && result2 == "")
                        {
                            __result = result1;
                        }
                    }
                }
                else if (result1 != "null" && result2 == "null")
                {
                    __result = result1;
                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        public string _check_costType(int __type)
        {
            string __cost_name = "";
            if (__type == 0) { __cost_name = "เฉลี่ย"; }
            else if (__type == 1) { __cost_name = "มาตรฐาน"; }
            else if (__type == 2) { __cost_name = "fifo"; }
            else if (__type == 3) { __cost_name = "lifo"; }
            else if (__type == 4) { __cost_name = "lot"; }
            return __cost_name;
        }


        /// <summary>
        /// หาผลต่างของวันที่
        /// </summary>
        /// <returns></returns>
        double _calDiffDate(DateTime __start, DateTime __stop) //  การลบวันที่
        {
            double __result = 0;
            try
            {
                DateTime __chk_date = new DateTime();
                if (__chk_date.CompareTo(__start) == 0 || __chk_date.CompareTo(__stop) == 0)
                {
                    __result = 0;
                }
                else
                {
                    __result = (__stop.Date - __start.Date).TotalDays;
                    if (__result <= 0) __result = __result * -1; // ค่าของผลต่างต้องไม่ติดลบ
                }
            }
            catch (Exception __e)
            {

            }
            return __result;
        }

        double _cal_cost_average(double __cost, double __qty)
        {
            double __result = 0;
            try
            {
                if (!(__cost == 0) || !(__qty == 0))
                {
                    __result = __cost / __qty;
                }
                else { __result = 0; }
            }
            catch (Exception __ex) { string __error = __ex.Message; }
            return __result;
        }

        double _cal_ratio_value(object __stand_value, object __divide_value)
        {
            double __result = 0;
            try
            {
                if (!(double.Parse(__stand_value.ToString()) == 0) || !(double.Parse(__divide_value.ToString()) == 0))
                {
                    __result = double.Parse(__stand_value.ToString()) / double.Parse(__divide_value.ToString());
                }
            }
            catch (Exception __ex) { string __error = __ex.Message; }
            return __result;
        }
    }






}
