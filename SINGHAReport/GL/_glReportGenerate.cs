using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace SINGHAReport.GL
{
    public partial class _glReportGenerate : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTable2;
        _conditionForm _conditionScreen;
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private _singhaReportEnum _mode;
        private int _recordCount;

        public _glReportGenerate(_singhaReportEnum reportType, string screenName)
        {
            InitializeComponent();

            _mode = reportType;
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._renderHeader += new SMLReport._generate.RenderHeaderEventHandler(_report__renderHeader);
            this._report.Disposed += new EventHandler(_report_Disposed);
            this._report._objectPageBreak += new SMLReport._generate.ObjectPageBreakEventHandler(_report__objectPageBreak);
            this._report._viewControl._beforeReportDrawPaperArgs += _viewControl__beforeReportDrawPaperArgs;
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);

            _conditionScreen = new _conditionForm(reportType, screenName);
            _showCondition();
        }

        private void _viewControl__beforeReportDrawPaperArgs(SMLReport._report._pageListType pageListType)
        {
            if ((this._mode == _singhaReportEnum.GL_รายงานภาษีซื้อ | this._mode == _singhaReportEnum.GL_รายงานภาษีขาย) && this._conditionScreen._useBranchCheckbox.Checked == true)
            {
                string __getBranchCode = ((SMLReport._report._columnDataListType)((SMLReport._report._objectListType)pageListType._objectList[4])._columnList[0])._text;

                DataRow[] __getBranch = this._dataTable.Select(_g.d.erp_branch_list._code + "=\'" + __getBranchCode + "\'");

                if (__getBranch.Length > 0)
                {
                    ((SMLReport._report._cellListType)((SMLReport._report._columnListType)((SMLReport._report._objectListType)pageListType._objectList[1])._columnList[0])._cellList[2])._text = "ที่อยู่ : " + __getBranch[0][_g.d.erp_branch_list._address_1].ToString().Replace("\n", "");
                    ((SMLReport._report._cellListType)((SMLReport._report._columnListType)((SMLReport._report._objectListType)pageListType._objectList[1])._columnList[1])._cellList[1])._text = "[ " + " " + " ] " + MyLib._myGlobal._resource("สำนักงานใหญ่") + "       [ " + "X" + " ] " + MyLib._myGlobal._resource("สาขาลำดับที่") + " " + MyLib._myGlobal._decimalPhase(__getBranch[0][_g.d.erp_branch_list._number].ToString()).ToString("00000");
                }
            }
        }

        bool _report__objectPageBreak(SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Total)
            {
                {
                    this._recordCount = 0;
                    return true;
                }
            }
            return false;
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (this._dataTable == null)
            {
                return null;
            }

            if (level._levelName.Equals("master"))
            {
                // 
                return this._dataTable.Select();
            }
            else if (level._levelName.Equals("detail"))
            {
                StringBuilder __where = new StringBuilder();

                /*for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                {
                    if (levelParent._columnList[__loop]._fieldName.Length > 0)
                    {
                        if (__where.Length > 0)
                        {
                            __where.Append(" and ");
                        }
                        __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                    }
                }*/

                switch (this._mode)
                {
                    case _singhaReportEnum.GL_รายงานภาษีซื้อ:

                        if (this._conditionScreen._useBranchCheckbox.Checked == true)
                        {
                            __where.Append(" gl_journal_vat_buy.branch_code_filter = \'" + source[_g.d.erp_branch_list._code].ToString() + "\' ");
                        }
                        break;
                    case _singhaReportEnum.GL_รายงานภาษีขาย:
                        if (this._conditionScreen._useBranchCheckbox.Checked == true)
                        {
                            __where.Append(" gl_journal_vat_sale.branch_code_filter = \'" + source[_g.d.erp_branch_list._code].ToString() + "\' ");
                        }
                        break;
                }

                return this._dataTable2.Select(__where.ToString());
            }

            return null;
        }

        void _report__renderHeader(SMLReport._generate source)
        {
            switch (this._mode)
            {
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                    {
                        int __reportType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._report_vat_type).ToString());

                        SMLReport._report._objectListType __headerObject = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);

                        DateTime __beginDate = this._conditionScreen._screen._getDataDate(_g.d.resource_report_vat._date_begin);
                        DateTime __endDate = this._conditionScreen._screen._getDataDate(_g.d.resource_report_vat._date_end);


                        int __column0 = source._viewControl._addColumn(__headerObject, 100);
                        //
                        string __reportName = "รายงานภาษีซื้อ";
                        if (__reportType == 1)
                        {
                            __reportName = "รายงานภาษีซื้อ";
                        }

                        source._viewControl._addCell(__headerObject, __column0, true, 0, -1, SMLReport._report._cellType.String, __reportName, SMLReport._report._cellAlign.Center, source._viewControl._fontHeader1);
                        //
                        int __monthNumber = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_month));

                        // หมวดภาษี
                        //string __vatGroup = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_group);

                        string __headStr1 = MyLib._myGlobal._resource("เดือนภาษี") + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.g._monthName()[__monthNumber].ToString()))._str + " " + MyLib._myGlobal._resource("ปี") + " " + this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_year); // + ((_vatGroupName.Length > 0) ? " กลุ่มภาษี " + _vatGroupName : "");
                        source._viewControl._addCell(__headerObject, __column0, true, 1, -1, SMLReport._report._cellType.String, __headStr1, SMLReport._report._cellAlign.Center, source._viewControl._fontHeader2);
                        //
                        SMLReport._report._objectListType __headerObject2 = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                        int __column1 = source._viewControl._addColumn(__headerObject2, 60);
                        int __column2 = source._viewControl._addColumn(__headerObject2, 20);
                        int __column3 = source._viewControl._addColumn(__headerObject2, 20);
                        //
                        source._viewControl._addCell(__headerObject2, __column1, true, 0, -1, SMLReport._report._cellType.String, "วันที่ " + __beginDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " + __endDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        source._viewControl._addCell(__headerObject2, __column2, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("เลขประจำตัวผู้เสียภาษี") + " : " + MyLib._myGlobal._ltdTax, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        source._viewControl._addCell(__headerObject2, __column3, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("หน้า") + " " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, source._viewControl._fontHeader2);
                        //
                        source._viewControl._addCell(__headerObject2, __column1, true, 1, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("ชื่อสถานประกอบการ") + " : " + MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        source._viewControl._addCell(__headerObject2, __column2, true, 1, -1, SMLReport._report._cellType.String, "[ " + ((MyLib._myGlobal._branch_type == 0) ? "X" : " ") + " ] " + MyLib._myGlobal._resource("สำนักงานใหญ่") + "       [ " + ((MyLib._myGlobal._branch_type == 1) ? "X" : " ") + " ] " + MyLib._myGlobal._resource("สาขาลำดับที่") + ((MyLib._myGlobal._branch_type == 1) ? "  " + MyLib._myGlobal._branch_code : ""), SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        //
                        source._viewControl._addCell(__headerObject2, __column1, true, 2, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("ที่อยู่") + " : " + MyLib._myGlobal._ltdAddress.Replace("\n", string.Empty), SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        //
                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    {
                        int __reportType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._report_vat_type).ToString());

                        DateTime __beginDate = this._conditionScreen._screen._getDataDate(_g.d.resource_report_vat._date_begin);
                        DateTime __endDate = this._conditionScreen._screen._getDataDate(_g.d.resource_report_vat._date_end);


                        SMLReport._report._objectListType __headerObject = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                        int __column0 = source._viewControl._addColumn(__headerObject, 100);
                        //
                        string __reportName = "รายงานภาษีขาย";
                        if (__reportType == 1)
                        {
                            __reportName = "รายงานภาษีขาย";
                        }

                        source._viewControl._addCell(__headerObject, __column0, true, 0, -1, SMLReport._report._cellType.String, __reportName, SMLReport._report._cellAlign.Center, source._viewControl._fontHeader1);
                        //
                        int __monthNumber = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_month));

                        // หมวดภาษี
                        //string __vatGroup = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_group);

                        string __headStr1 = MyLib._myGlobal._resource("เดือนภาษี") + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.g._monthName()[__monthNumber].ToString()))._str + " " + MyLib._myGlobal._resource("ปี") + " " + this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_year); // + ((_vatGroupName.Length > 0) ? " กลุ่มภาษี " + _vatGroupName : "");
                        source._viewControl._addCell(__headerObject, __column0, true, 1, -1, SMLReport._report._cellType.String, __headStr1, SMLReport._report._cellAlign.Center, source._viewControl._fontHeader2);
                        //
                        SMLReport._report._objectListType __headerObject2 = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                        int __column1 = source._viewControl._addColumn(__headerObject2, 60);
                        int __column2 = source._viewControl._addColumn(__headerObject2, 20);
                        int __column3 = source._viewControl._addColumn(__headerObject2, 20);
                        //
                        source._viewControl._addCell(__headerObject2, __column1, true, 0, -1, SMLReport._report._cellType.String, "วันที่ " + __beginDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " + __endDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        source._viewControl._addCell(__headerObject2, __column2, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("เลขประจำตัวผู้เสียภาษี") + " : " + MyLib._myGlobal._ltdTax, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        source._viewControl._addCell(__headerObject2, __column3, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("หน้า") + " " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, source._viewControl._fontHeader2);
                        //
                        source._viewControl._addCell(__headerObject2, __column1, true, 1, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("ชื่อสถานประกอบการ") + " : " + MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        source._viewControl._addCell(__headerObject2, __column2, true, 1, -1, SMLReport._report._cellType.String, "[ " + ((MyLib._myGlobal._branch_type == 0) ? "X" : " ") + " ] " + MyLib._myGlobal._resource("สำนักงานใหญ่") + "       [ " + ((MyLib._myGlobal._branch_type == 1) ? "X" : " ") + " ] " + MyLib._myGlobal._resource("สาขาลำดับที่") + ((MyLib._myGlobal._branch_type == 1) ? "  " + MyLib._myGlobal._branch_code : ""), SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                        //
                        source._viewControl._addCell(__headerObject2, __column1, true, 2, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("ที่อยู่") + " : " + MyLib._myGlobal._ltdAddress.Replace("\n", string.Empty), SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);

                    }
                    break;
            }

        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Data)
            {
                switch (this._mode)
                {
                    case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                        if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count))
                        {
                            this._recordCount++;
                            sender._columnList[columnNumber]._dataStr = this._recordCount.ToString();
                        }
                        else
                            if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._trans_flag))
                        {
                            sender._columnList[columnNumber]._dataStr = (sender._columnList[columnNumber]._dataStr.Length == 0) ? "" : _g.g._transFlagGlobal._transName((MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr)));
                        }
                        else
                                if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._is_add))
                        {
                            sender._columnList[columnNumber]._dataStr = (Int32.Parse(sender._columnList[columnNumber]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยื่นเพิ่มเติม");
                        }
                        else
                                    if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_status))
                        {
                            //int __index = sender._findColumnName(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_status);

                            //string __data = sender._columnList[__index]._dataStr;
                            //string __dataString = (Int32.Parse(sender._columnList[__index]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยกเลิก"); ;
                            sender._columnList[columnNumber]._dataStr = (Int32.Parse(sender._columnList[sender._findColumnName(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_status)]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยกเลิก");
                        }

                        break;
                    case _singhaReportEnum.GL_รายงานภาษีขาย:
                        if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count))
                        {
                            this._recordCount++;
                            sender._columnList[columnNumber]._dataStr = this._recordCount.ToString();
                        }
                        else
                            if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._trans_flag))
                        {
                            sender._columnList[columnNumber]._dataStr = (sender._columnList[columnNumber]._dataStr.Length == 0) ? "" : _g.g._transFlagGlobal._transName((MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr)));
                        }
                        else
                                if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._is_add))
                        {
                            sender._columnList[columnNumber]._dataStr = (Int32.Parse(sender._columnList[columnNumber]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยื่นเพิ่มเติม");
                        }
                        else
                                    if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount))
                        {

                            int __columnAmount = sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount);
                            int __columnDocNo = sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number);

                            //string __data = sender._columnList[__index]._dataStr;
                            //string __dataString = (Int32.Parse(sender._columnList[__index]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยกเลิก"); ;
                            if (sender._columnList[columnNumber]._dataNumber == 0 && sender._columnList[__columnAmount]._dataNumber == 0)
                            {
                                string __whereDocNoFilter = sender._columnList[__columnDocNo]._dataStr;

                                if (this._conditionScreen._useBranchCheckbox.Checked == true)
                                {
                                    DataRow[] __docRow = _dataTable2.Select(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number + "=\'" + __whereDocNoFilter + "\'");

                                    if (__docRow.Length > 0 && __docRow[0][_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status].ToString().Equals("1"))
                                    {
                                        sender._columnList[columnNumber]._dataStr = MyLib._myGlobal._resource("ยกเลิก");
                                    }
                                }
                                else
                                {
                                    DataRow[] __docRow = _dataTable.Select(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number + "=\'" + __whereDocNoFilter + "\'");

                                    if (__docRow.Length > 0 && __docRow[0][_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status].ToString().Equals("1"))
                                    {
                                        sender._columnList[columnNumber]._dataStr = MyLib._myGlobal._resource("ยกเลิก");
                                    }

                                }
                            }
                            //sender._columnList[columnNumber]._dataStr = (Int32.Parse(sender._columnList[sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status)]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยกเลิก");
                        }

                        break;
                        /*
                    case _vatConditionType.ภาษีขาย:
                    case _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี:
                    case _vatConditionType.ภาษีขาย_สรุป:
                    case _vatConditionType.ภาษีขาย_เลขประจำตัวผู้เสียภาษี:
                        if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count))
                        {
                            this._recordCount++;
                            sender._columnList[columnNumber]._dataStr = this._recordCount.ToString();
                        }
                        else
                            if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._trans_flag))
                        {
                            sender._columnList[columnNumber]._dataStr = (sender._columnList[columnNumber]._dataStr.Length == 0) ? "" : _g.g._transFlagGlobal._transName((MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr)));
                        }
                        else
                                if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._is_add))
                        {
                            sender._columnList[columnNumber]._dataStr = (Int32.Parse(sender._columnList[columnNumber]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยื่นเพิ่มเติม");
                        }
                        else
                                    if (columnNumber == sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status))
                        {
                            sender._columnList[columnNumber]._dataStr = (MyLib._myGlobal._intPhase(sender._columnList[sender._findColumnName(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status)]._dataStr) == 0) ? "" : MyLib._myGlobal._resource("ยกเลิก");
                        }
                        break;
                        */
                }
            }
        }



        void _report__query()
        {
            this._recordCount = 0;

            if (this._dataTable == null)
            {
                try
                {
                    string __beginDate = this._conditionScreen._screen._getDataStrQuery(_g.d.resource_report_vat._date_begin);
                    string __endDate = this._conditionScreen._screen._getDataStrQuery(_g.d.resource_report_vat._date_end);

                    int __month = (int)MyLib._myGlobal._decimalPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_month)) + 1;
                    int __year = (int)MyLib._myGlobal._decimalPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_year));
                    //

                    bool __showOnlyAmountVat = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._show_vat_amount_only).ToString().Equals("1") ? true : false;
                    //_vatGroupName = new StringBuilder();

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "";
                    StringBuilder __queryMaster = new StringBuilder();

                    // string __orderBy = __orderBy = this._form_condition._whereControl._getOrderBy();
                    switch (this._mode)
                    {
                        /*
                        case _vatConditionType.ภาษีขาย:
                        case _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี:
                        case _vatConditionType.ภาษีขาย_สรุป:
                        case _vatConditionType.ภาษีขาย_เลขประจำตัวผู้เสียภาษี:
                            {
                                // toe ดึงชื่อกลุ่มภาษี
                                string __vatGroupSelect = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_group).ToString();

                                if (__vatGroupSelect.Length > 0)
                                {
                                    string[] __vatGroupSplit = __vatGroupSelect.Split(',');
                                    StringBuilder __vatGroupCode = new StringBuilder();
                                    foreach (string _group in __vatGroupSplit)
                                    {
                                        if (__vatGroupCode.Length > 0)
                                        {
                                            __vatGroupCode.Append(",");
                                        }

                                        if (_group.IndexOf(':') != -1)
                                        {
                                            __vatGroupCode.Append("\'" + _group.Split(':')[0] + "\',\'" + _group.Split(':')[1] + "\'");
                                        }
                                        else
                                        {
                                            __vatGroupCode.Append("\'" + _group + "\'");
                                        }
                                    }

                                    DataSet __result = __myFrameWork._queryShort("select " + _g.d.gl_tax_group._code + "," + _g.d.gl_tax_group._name_1 + " from " + _g.d.gl_tax_group._table + " where " + _g.d.gl_tax_group._code + " in (" + __vatGroupCode.ToString() + ") ");
                                    if (__result.Tables.Count > 0)
                                    {
                                        // ประกอบชื่อ vatgroup

                                        foreach (string _group in __vatGroupSplit)
                                        {
                                            if (_vatGroupName.Length > 0)
                                            {
                                                _vatGroupName.Append(",");
                                            }

                                            if (_group.IndexOf(':') != -1)
                                            {
                                                //__vatGroupCode.Append("\'" + _group.Split(':')[0] + "\',\'" + _group.Split(':')[1] + "\'");
                                                string[] __groupbetween = _group.Split(':');

                                                DataRow[] __select1 = __result.Tables[0].Select(_g.d.gl_tax_group._code + "=\'" + __groupbetween[0] + "\'");
                                                DataRow[] __select2 = __result.Tables[0].Select(_g.d.gl_tax_group._code + "=\'" + __groupbetween[1] + "\'");

                                                _vatGroupName.Append("" + __select1[0][_g.d.gl_tax_group._name_1].ToString() + " ถึง " + __select2[0][_g.d.gl_tax_group._name_1].ToString());
                                            }
                                            else
                                            {
                                                //__vatGroupCode.Append("\'" + _group + "\'");
                                                DataRow[] __select = __result.Tables[0].Select(_g.d.gl_tax_group._code + "=\'" + _group + "\'");
                                                _vatGroupName.Append(__select[0][_g.d.gl_tax_group._name_1].ToString());

                                            }
                                        }
                                    }
                                }

                                string __vatGroup = MyLib._myUtil._genCodeList(_g.d.gl_journal_vat_sale._tax_group, __vatGroupSelect);
                                string __fullInvoiceQuery = "(select doc_no from ic_trans where trans_flag = 144 and doc_ref = gl_journal_vat_sale.doc_no and gl_journal_vat_sale.trans_flag = 44 limit 1)";

                                string __extraField = "";
                                if (this._mode == _vatConditionType.ภาษีขาย_สรุป)
                                {
                                    __extraField = "," + __fullInvoiceQuery + " as \"" + _g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no + "\" " +
                                        ", (select is_pos from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and ic_trans.trans_flag = gl_journal_vat_sale.trans_flag) as is_pos ";
                                }

                                __query = "select " + this._report._level.__fieldList(true) + "," + _g.d.gl_journal_vat_sale._is_add + __extraField + " from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._vat_effective_year + "=" + __year.ToString() + " and " + _g.d.gl_journal_vat_sale._vat_effective_period + "=" + __month.ToString() + ((__showOnlyAmountVat) ? " and " + _g.d.gl_journal_vat_sale._amount + "<>0 " : "") + ((__vatGroup.Length > 0) ? " and " + __vatGroup : "");

                                string __query1 = "case when coalesce(ar_name, '')='' then (select name_1 from ar_customer where code = gl_journal_vat_sale.ar_code) else ar_name end ";
                                __query = __query.Replace("," + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_name, "," + __query1);

                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count + " as ", "\'\'" + " as ");
                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + " as ", _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + "*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + " as ", _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + "*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                if (this._mode == _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total + " as ", "(" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + ")*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + " as ", "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + "*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " end as ");
                                }
                                else
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total + " as ", "(" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + ")*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                }
                                //__query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total + " as ", "(" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + ")*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc, "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " end ");
                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status + " as ", "(select coalesce(last_status, 0) from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag) as");

                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no + " as ", __fullInvoiceQuery + " as ");
                                // ,(select coalesce(last_status, 0) from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag) as \"" + _g.d.gl_journal_vat_sale._doc_status + "\"

                                // tax_no
                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._cust_taxno + " as ", "  case when coalesce(tax_no, '') ='' then ( select " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + " ) else tax_no end  as ");

                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._headquarters + " as ", "case when (case when branch_type is null then (coalesce(( select " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + "  ), 0)) else branch_type end) = 0 and (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._ar_status + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + ") = 1 then 'X' else '' end as ");
                                }
                                else
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._headquarters + " as ", "case when (case when branch_type is null then ( coalesce(( select " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + " ), 0) ) else branch_type end) = 0 then 'X' else '' end as ");
                                }


                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._branch + " as ", " case when branch_type is null then ( select case when coalesce(" + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_type + ", 0) = 1 then " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_code + " else '' end from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + " ) else (case when branch_type=1 then branch_code else '' end) end as ");

                                switch (this._form_condition._whereControl._getOrderSelectIndexNumber())
                                {
                                    case 1: // เรียงตามวันที่
                                        __orderBy = __orderBy + "," + _g.d.gl_journal_vat_sale._vat_number;
                                        break;
                                }
                            }
                            break;
                            */
                        case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                            {
                                __queryMaster.Append("select * from erp_branch_list ");
                                StringBuilder __whereBranch = new StringBuilder();

                                if (this._conditionScreen._useBranchCheckbox.Checked == true)
                                {
                                    for (int __row = 0; __row < this._conditionScreen._gridCondition._rowData.Count; __row++)
                                    {
                                        if (this._conditionScreen._gridCondition._cellGet(__row, 0).ToString().Equals("1"))
                                        {
                                            if (__whereBranch.Length > 0)
                                                __whereBranch.Append(",");

                                            string __branchCode = this._conditionScreen._gridCondition._cellGet(__row, _g.d.erp_branch_list._code).ToString();
                                            __whereBranch.Append("\'" + __branchCode + "\'");
                                        }
                                    }

                                    __queryMaster.Append(" where " + _g.d.erp_branch_list._code + " in (" + __whereBranch.ToString() + ")");
                                    __queryMaster.Append(" order by " + _g.d.erp_branch_list._code);
                                }
                                else
                                {
                                    __queryMaster.Append(" order by " + _g.d.erp_branch_list._code);
                                    __queryMaster.Append(" limit 1");
                                }

                                int __reportType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._report_vat_type).ToString());

                                string __book_code = this._conditionScreen._screen._getDataStr("book_code");
                                string __whereBookCode = "";
                                if (__book_code.Length > 0)
                                {
                                    __whereBookCode = " coalesce((select book_code from gl_journal where gl_journal.doc_no = gl_journal_vat_buy.doc_no and gl_journal_vat_buy.trans_flag = gl_journal_vat_buy.trans_flag), '') = \'" + __book_code + "\' ";
                                }
                                string __getOrderBy = "";

                                decimal __getSort = this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._sort_by);
                                switch (__getSort.ToString())
                                {
                                    case "1":
                                        __getOrderBy = _g.d.gl_journal_vat_buy._vat_date;
                                        break;
                                    case "2":
                                        __getOrderBy = _g.d.gl_journal_vat_buy._doc_no;
                                        break;
                                    case "3":
                                        __getOrderBy = _g.d.gl_journal_vat_buy._vat_doc_no;
                                        break;
                                    default:
                                        __getOrderBy = _g.d.gl_journal_vat_buy._doc_date;
                                        break;

                                }

                                string __vatGroup = MyLib._myUtil._genCodeList(_g.d.gl_journal_vat_buy._vat_group, this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_group).ToString());

                                string __fromDocFormat = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._from_doc_type).ToString();
                                string __toDocFormat = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._to_doc_type).ToString();

                                string __whereDocFormatCode = "";
                                if (__fromDocFormat.Length > 0 && __toDocFormat.Length > 0)
                                {
                                    __whereDocFormatCode = " (case when gl_journal_vat_buy.trans_flag not in (239,19) then (select doc_format_code from ic_trans where ic_trans.doc_no = gl_journal_vat_buy.doc_no and trans_flag = gl_journal_vat_buy.trans_flag ) else (select doc_format_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_buy.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_buy.trans_flag ) end) between \'" + __fromDocFormat + "\' and \'" + __toDocFormat + "\' ";
                                }
                                else if (__fromDocFormat.Length > 0)
                                {
                                    __whereDocFormatCode = " (case when gl_journal_vat_buy.trans_flag not in (239,19) then (select doc_format_code from ic_trans where ic_trans.doc_no = gl_journal_vat_buy.doc_no and trans_flag = gl_journal_vat_buy.trans_flag ) else (select doc_format_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_buy.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_buy.trans_flag ) end) = \'" + __fromDocFormat + "\' ";
                                }
                                else if (__toDocFormat.Length > 0)
                                {
                                    __whereDocFormatCode = " (case when gl_journal_vat_buy.trans_flag not in (239,19) then (select doc_format_code from ic_trans where ic_trans.doc_no = gl_journal_vat_buy.doc_no and trans_flag = gl_journal_vat_buy.trans_flag ) else (select doc_format_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_buy.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_buy.trans_flag ) end) = \'" + __toDocFormat + "\' ";
                                }

                                __query = "select " +
                                    " gl_journal_vat_buy.vat_count as \"gl_journal_vat_buy.vat_count\"" +
                                    ",gl_journal_vat_buy.vat_date as \"gl_journal_vat_buy.vat_date\"" +
                                    ",gl_journal_vat_buy.vat_doc_no as \"gl_journal_vat_buy.vat_doc_no\"" +
                                    ",gl_journal_vat_buy.ap_name as \"gl_journal_vat_buy.ap_name\"" +
                                    ",gl_journal_vat_buy.cust_taxno as \"gl_journal_vat_buy.cust_taxno\"" +
                                    ",gl_journal_vat_buy.headquarters as \"gl_journal_vat_buy.headquarters\"" +
                                    ",gl_journal_vat_buy.branch as \"gl_journal_vat_buy.branch\"" +
                                    ",gl_journal_vat_buy.vat_base_amount as \"gl_journal_vat_buy.vat_base_amount\"" +
                                    ",gl_journal_vat_buy.vat_amount as \"gl_journal_vat_buy.vat_amount\"" +
                                    ",gl_journal_vat_buy.vat_except_amount_1 as \"gl_journal_vat_buy.vat_except_amount_1\"" +
                                    "," + _g.d.gl_journal_vat_sale._is_add +
                                    ", case when gl_journal_vat_buy.trans_flag not in (239,19) then (select branch_code from ic_trans where ic_trans.doc_no = gl_journal_vat_buy.doc_no and trans_flag = gl_journal_vat_buy.trans_flag ) else (select branch_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_buy.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_buy.trans_flag ) end as \"" + _g.d.gl_journal_vat_buy._table + "." + "branch_code_filter\" " +
                                    " from " + _g.d.gl_journal_vat_buy._table +
                                    " where " +
                                    //_g.d.gl_journal_vat_buy._vat_effective_year + "=" + __year.ToString() + " and " + _g.d.gl_journal_vat_buy._vat_effective_period + "=" + __month.ToString() + 
                                    _g.d.gl_journal_vat_buy._vat_date + " between " + __beginDate + " and " + __endDate +

                                    ((__reportType == 3) ? " and (vat_type = 1 ) " : ((__reportType == 2) ? " and (vat_except_amount_1 > 0 ) " : ((__reportType == 1) ? " and (vat_base_amount > 0) " : " and coalesce((select vat_type from ic_trans where ic_trans.doc_no = gl_journal_vat_buy.doc_no and ic_trans.trans_flag = gl_journal_vat_buy.trans_flag), 0) in (0,1) ")) )+
                                    // โต๋ เอา vat type 1 ออก ภาษีซื้อขอคืนไมได้
                                    ((__reportType == 3) ? ""  : " and " + _g.d.gl_journal_vat_buy._vat_type + " in (0) ") + ((__showOnlyAmountVat) ? " and " + _g.d.gl_journal_vat_buy._vat_amount + "<>0 " : "") + ((__vatGroup.Length > 0) ? " and " + __vatGroup : "") + ((__whereDocFormatCode.Length > 0) ? " and (" + __whereDocFormatCode + ") " : "") + ((__whereBookCode.Length > 0) ? " and " + __whereBookCode : "") +
                                    " order by " + __getOrderBy + "," + _g.d.gl_journal_vat_buy._vat_doc_no;



                                string __query1 = "case when coalesce(ap_name, '')='' then (select name_1 from ap_supplier where code = gl_journal_vat_buy.ap_code) else ap_name end ";
                                __query = __query.Replace("," + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_name, "," + __query1);
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count + " as ", "\'\'" + " as ");

                                if (__reportType == 2)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + " as ", _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1 + "*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + " as ", "0 as ");

                                }
                                else
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + " as ", _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + "*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + " as ", _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + "*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                }

                                //if (__reportType == 0)
                                //{
                                // ภาษีศูนย์
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total + " as ", "(" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1 + "+" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + "+" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + ")*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1 + " as ", "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_buy.doc_no and ic_trans.trans_flag=gl_journal_vat_buy.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1 + "*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " end as ");
                                //}
                                //else
                                //{
                                //    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total + " as ", "(" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + "+" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + ")*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                //}

                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc, "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_buy.doc_no and ic_trans.trans_flag=gl_journal_vat_buy.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " end ");
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_status + " as ", "(select coalesce(last_status, 0) from ic_trans where ic_trans.doc_no=gl_journal_vat_buy.doc_no and ic_trans.trans_flag=gl_journal_vat_buy.trans_flag) as");

                                // tax_no
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.resource_report_vat._cust_taxno + " as ", "  case when coalesce(tax_no, '') ='' then  ( select " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._tax_id + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_code + " )  else tax_no end as ");

                                // headquater
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.ar_customer_detail._headquarters + " as ", "case when (case when branch_type is null then (coalesce(( select " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._branch_type + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_code + "  ), 0)) else branch_type end) = 0 and (select " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._ap_status + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_code + ")=1 then 'X' else '' end as ");
                                }
                                else
                                {

                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.ar_customer_detail._headquarters + " as ", "case when (case when branch_type is null then (coalesce(( select " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._branch_type + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_code + " ), 0)) else branch_type end) = 0 then 'X' else '' end as ");
                                }

                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.ar_customer_detail._branch + " as ", " case when branch_type is null then ( select case when coalesce(" + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._branch_type + ", 0) = 1 then " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._branch_code + " else '' end from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_code + " ) else (case when branch_type=1 then branch_code else '' end) end as ");


                                /*switch (this._form_condition._whereControl._getOrderSelectIndexNumber())
                                {
                                    case 1: // เรียงตามวันที่
                                        __orderBy = __orderBy + "," + _g.d.gl_journal_vat_buy._vat_doc_no;
                                        break;
                                }*/
                            }
                            break;

                        case _singhaReportEnum.GL_รายงานภาษีขาย:
                            {
                                int __reportType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._report_vat_type).ToString());

                                __queryMaster.Append("select * from erp_branch_list ");
                                StringBuilder __whereBranch = new StringBuilder();

                                // header

                                if (this._conditionScreen._useBranchCheckbox.Checked == true)
                                {
                                    for (int __row = 0; __row < this._conditionScreen._gridCondition._rowData.Count; __row++)
                                    {
                                        if (this._conditionScreen._gridCondition._cellGet(__row, 0).ToString().Equals("1"))
                                        {
                                            if (__whereBranch.Length > 0)
                                                __whereBranch.Append(",");

                                            string __branchCode = this._conditionScreen._gridCondition._cellGet(__row, _g.d.erp_branch_list._code).ToString();
                                            __whereBranch.Append("\'" + __branchCode + "\'");
                                        }
                                    }

                                    __queryMaster.Append(" where " + _g.d.erp_branch_list._code + " in (" + __whereBranch.ToString() + ")");
                                    __queryMaster.Append(" order by " + _g.d.erp_branch_list._code);
                                }
                                else
                                {
                                    __queryMaster.Append(" order by " + _g.d.erp_branch_list._code);
                                    __queryMaster.Append(" limit 1");
                                }


                                string __getOrderBy = "";
                                decimal __getSort = this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._sort_by);
                                switch (__getSort.ToString())
                                {
                                    case "1":
                                        __getOrderBy = _g.d.gl_journal_vat_sale._vat_date;
                                        break;
                                    case "2":
                                        __getOrderBy = _g.d.gl_journal_vat_sale._doc_no;
                                        break;
                                    case "3":
                                        __getOrderBy = _g.d.gl_journal_vat_sale._vat_number;
                                        break;
                                    default:
                                        __getOrderBy = _g.d.gl_journal_vat_sale._doc_date;
                                        break;

                                }

                                // detail
                                __query = "select " +
                                    " gl_journal_vat_sale.vat_count as \"gl_journal_vat_sale.vat_count\"" +
                                    ",gl_journal_vat_sale.vat_date as \"gl_journal_vat_sale.vat_date\"" +
                                    ",gl_journal_vat_sale.vat_number as \"gl_journal_vat_sale.vat_number\"" +
                                    ",gl_journal_vat_sale.ar_name as \"gl_journal_vat_sale.ar_name\"" +
                                    ",gl_journal_vat_sale.cust_taxno as \"gl_journal_vat_sale.cust_taxno\"" +
                                    ",gl_journal_vat_sale.headquarters as \"gl_journal_vat_sale.headquarters\"" +
                                    ",gl_journal_vat_sale.branch as \"gl_journal_vat_sale.branch\"" +
                                    // ",gl_journal_vat_sale.except_tax_amount*case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else gl_journal_vat_sale.vat_calc end as \"gl_journal_vat_sale.except_tax_amount\"" +
                                    //  ",gl_journal_vat_sale.base_caltax_amount*case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else gl_journal_vat_sale.vat_calc end as \"gl_journal_vat_sale.base_caltax_amount\"" +
                                    // ",gl_journal_vat_sale.amount*case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else gl_journal_vat_sale.vat_calc end as \"gl_journal_vat_sale.amount\"" +
                                    ",gl_journal_vat_sale.except_tax_amount as \"gl_journal_vat_sale.except_tax_amount\"" +
                                    ",gl_journal_vat_sale.base_caltax_amount as \"gl_journal_vat_sale.base_caltax_amount\"" +
                                    ",gl_journal_vat_sale.amount as \"gl_journal_vat_sale.amount\"" +


                                    "," + _g.d.gl_journal_vat_sale._is_add +
                                    ", case when gl_journal_vat_sale.trans_flag not in (239,19) then (select branch_code from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and trans_flag = gl_journal_vat_sale.trans_flag ) else (select branch_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_sale.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_sale.trans_flag ) end as \"" + _g.d.gl_journal_vat_sale._table + "." + "branch_code_filter\" " +
                                    ",(select coalesce(last_status, 0) from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and ic_trans.trans_flag = gl_journal_vat_sale.trans_flag) as\"gl_journal_vat_sale.doc_status\" " +
                                    " from " + _g.d.gl_journal_vat_sale._table +
                                    " where " + //_g.d.gl_journal_vat_sale._vat_effective_year + "=" + __year.ToString() + " and " + _g.d.gl_journal_vat_sale._vat_effective_period + "=" + __month.ToString(); 
                                    _g.d.gl_journal_vat_sale._vat_date + " between " + __beginDate + " and " + __endDate +

                                    ((__reportType == 2) ? " and (except_tax_amount > 0 or (select vat_type from ic_trans where gl_journal_vat_sale.doc_no = ic_trans.doc_no and gl_journal_vat_sale.trans_flag = ic_trans.trans_flag)=2) " : ((__reportType == 1) ? " and (base_caltax_amount > 0 or (select vat_type from ic_trans where gl_journal_vat_sale.doc_no = ic_trans.doc_no and gl_journal_vat_sale.trans_flag = ic_trans.trans_flag) in (1,2)) " : ((__reportType==3) ? " and (select vat_type from ic_trans where gl_journal_vat_sale.doc_no = ic_trans.doc_no and gl_journal_vat_sale.trans_flag = ic_trans.trans_flag)=2 " : ""))) +

                                    " order by " + __getOrderBy + ", vat_number";

                                // + " and " + _g.d.gl_journal_vat_buy._vat_type + " in (0,1) " + ((__showOnlyAmountVat) ? " and " + _g.d.gl_journal_vat_buy._vat_amount + "<>0 " : "");

                                string __query1 = "case when coalesce(ar_name, '')='' then (select name_1 from ar_customer where code = gl_journal_vat_sale.ar_code) else ar_name end ";
                                __query = __query.Replace("," + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_name, "," + __query1);

                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count + " as ", "\'\'" + " as ");


                                // except vat
                                if (__reportType == 0)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + " as ",
                                  "case when (select vat_type from ic_trans where gl_journal_vat_sale.doc_no = ic_trans.doc_no and gl_journal_vat_sale.trans_flag = ic_trans.trans_flag) = 2 then (select total_amount from ic_trans where gl_journal_vat_sale.doc_no = ic_trans.doc_no and gl_journal_vat_sale.trans_flag = ic_trans.trans_flag) else gl_journal_vat_sale.except_tax_amount end*case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else gl_journal_vat_sale.vat_calc end " + " as ");

                                }
                                else
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + " as ",
                                  "gl_journal_vat_sale.except_tax_amount*case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else gl_journal_vat_sale.vat_calc end " + " as ");

                                }

                                if (__reportType == 2 || __reportType == 3)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + " as ", " case when coalesce((select vat_type from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and ic_trans.trans_flag = gl_journal_vat_sale.trans_flag), 0)=2 then (select total_amount from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and ic_trans.trans_flag = gl_journal_vat_sale.trans_flag) else " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + " end*case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else gl_journal_vat_sale.vat_calc end  as ");
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + " as ", "0 as ");
                                }
                                else
                                {

                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + " as ", _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + "*case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else gl_journal_vat_sale.vat_calc end as ");
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + " as ", "gl_journal_vat_sale.amount*case when(select last_status from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and ic_trans.trans_flag = gl_journal_vat_sale.trans_flag) = 1 then 0 else gl_journal_vat_sale.vat_calc end as ");
                                }


                                //if (this._mode == _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี)
                                //{
                                //    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total + " as ", "(" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + ")*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                //    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + " as ", "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + "*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " end as ");
                                //}
                                //else
                                if (__reportType == 1)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total + " as ", "(" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + ")*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                    //__query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + " as ", "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount + "*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " end as ");
                                }
                                else
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total + " as ", "(" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount + "+" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount + ")*" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " as ");
                                }

                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc, "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_calc + " end ");
                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status + " as ", "(select coalesce(last_status, 0) from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag) as");

                                // __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no + " as ", __fullInvoiceQuery + " as ");
                                // ,(select coalesce(last_status, 0) from ic_trans where ic_trans.doc_no=gl_journal_vat_sale.doc_no and ic_trans.trans_flag=gl_journal_vat_sale.trans_flag) as \"" + _g.d.gl_journal_vat_sale._doc_status + "\"

                                // tax_no
                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._cust_taxno + " as ", "  case when coalesce(tax_no, '') ='' then ( select " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + " ) else tax_no end  as ");

                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._headquarters + " as ", "case when (case when branch_type is null then (coalesce(( select " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + "  ), 0)) else branch_type end) = 0 and (select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._ar_status + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + ") = 1 then 'X' else '' end as ");
                                }
                                else
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._headquarters + " as ", "case when (case when branch_type is null then ( coalesce(( select " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + " ), 0) ) else branch_type end) = 0 then 'X' else '' end as ");
                                }


                                __query = __query.Replace(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._branch + " as ", " case when branch_type is null then ( select case when coalesce(" + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_type + ", 0) = 1 then " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._branch_code + " else '' end from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_code + " ) else (case when branch_type=1 then branch_code else '' end) end as ");

                            }
                            break;
                    }
                    //

                    string __queryDetail = __query; // + __orderBy;

                    StringBuilder __queryStr = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryMaster.ToString()));
                    __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail));
                    __queryStr.Append("</node>");


                    ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr.ToString());

                    if (__result.Count > 0)
                    {

                        switch (this._mode)
                        {
                            case _singhaReportEnum.GL_รายงานภาษีขาย:
                            case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                                {

                                    if (this._conditionScreen._useBranchCheckbox.Checked == true)
                                    {
                                        this._dataTable = ((DataSet)__result[0]).Tables[0];
                                        this._dataTable2 = ((DataSet)__result[1]).Tables[0];  //__myFrameWork._queryStream(MyLib._myGlobal._databaseName, __fullQuery).Tables[0];
                                    }
                                    else
                                    {
                                        this._dataTable = ((DataSet)__result[1]).Tables[0];  //__myFrameWork._queryStream(MyLib._myGlobal._databaseName, __fullQuery).Tables[0];
                                    }
                                }
                                break;
                            default:
                                this._dataTable = ((DataSet)__result[0]).Tables[0];
                                this._dataTable2 = ((DataSet)__result[1]).Tables[0];  //__myFrameWork._queryStream(MyLib._myGlobal._databaseName, __fullQuery).Tables[0];
                                break;
                        }

                    }

                    this._recordCount = 0;
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }



        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            return _reportInitDetail(levelParent, sumTotal, autoWidth, "detail");
        }
        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth, string levelName)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            switch (this._mode)
            {
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                    {
                        int __reportType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._report_vat_type).ToString());

                        if (__reportType == 0)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count, null, 4, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_date, null, 9, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_doc_no, null, 14, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.ap_supplier_detail._headquarters, _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._headquarters, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular, false, false, SMLReport._report._cellAlign.Center));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.ap_supplier_detail._branch, _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._branch, 6, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1, null, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_amount, 14, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_vat, 11, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                        }
                        else
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_date, null, 8, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_doc_no, null, 14, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.ap_supplier_detail._headquarters, _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._headquarters, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular, false, false, SMLReport._report._cellAlign.Center));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.ap_supplier_detail._branch, _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._branch, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_amount, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_vat, 11, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));

                        }

                        // __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + ".branch_code_filter", "*", 0, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _isHideColumn = true });

                        // __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + ".branch_code_filter" , _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_vat, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                    }
                    break;

                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    {
                        int __reportType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataNumber(_g.d.resource_report_vat._report_vat_type).ToString());
                        if (__reportType == 0)
                        {

                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count, null, 6, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date, null, 11, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number, null, 13, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_name, null, 21, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, 16, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._headquarters, "สนญ.", 5, SMLReport._report._cellType.String, 0, FontStyle.Regular, false, false, SMLReport._report._cellAlign.Center));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._branch, _g.d.ar_customer_detail._table + "." + _g.d.ap_supplier_detail._branch, 6, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount, null, 13, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_amount, 13, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_vat, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                        }
                        else
                        {

                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count, null, 6, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date, null, 11, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number, null, 13, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._headquarters, _g.d.ar_customer_detail._table + "." + _g.d.ap_supplier_detail._headquarters, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular, false, false, SMLReport._report._cellAlign.Center));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._branch, _g.d.ar_customer_detail._table + "." + _g.d.ap_supplier_detail._branch, 6, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_amount, 13, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_vat, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._gl_no_decimal, FontStyle.Regular));

                        }

                        // __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _isHideColumn = true });

                    }
                    break;

                    /*
                    case _vatConditionType.ภาษีขาย:
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        if (this._showFullVatRefer)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._full_invoice_doc_no, 18, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._trans_flag, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_name, null, 35, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_vat, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        if (this._form_condition != null && MyLib._myGlobal._intPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_vat_type)) == 1)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._is_add, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                        break;
                    case _vatConditionType.ภาษีซื้อ:
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._trans_flag, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_name, null, 35, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_vat, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        if (this._form_condition != null && MyLib._myGlobal._intPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_vat_type)) == 1)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._is_add, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        break;
                    case _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี:
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        if (this._showFullVatRefer)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._full_invoice_doc_no, 18, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_name, null, 35, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));

                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        break;
                    case _vatConditionType.ภาษีซื้อ_สินค้ายกเว้นภาษี:
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._trans_flag, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_name, null, 35, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount, null, 13, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_status, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._is_add, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        break;
                    case _vatConditionType.ภาษีขาย_สรุป:
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status, null, 12, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                        break;
                    case _vatConditionType.ภาษีซื้อ_เลขประจำตัวผู้เสียภาษี:
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        if (this._showDocNo)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLGeneralLedger)
                            {
                                __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._trans_flag, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            }
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.ap_supplier_detail._headquarters, _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._headquarters, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular, false, false, SMLReport._report._cellAlign.Center));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.ap_supplier_detail._branch, _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._branch, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total_vat, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        if (this._form_condition != null && MyLib._myGlobal._intPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_vat_type)) == 1)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total, _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._is_add, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                        break;
                    case _vatConditionType.ภาษีขาย_เลขประจำตัวผู้เสียภาษี:
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        if (this._showDocNo)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLGeneralLedger)
                            {
                                __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._trans_flag, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            }
                        }
                        if (this._showFullVatRefer)
                        {
                            //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._full_invoice_doc_no, 18, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._ar_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._headquarters, _g.d.ar_customer_detail._table + "." + _g.d.ap_supplier_detail._headquarters, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular, false, false, SMLReport._report._cellAlign.Center));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.ar_customer_detail._branch, _g.d.ar_customer_detail._table + "." + _g.d.ap_supplier_detail._branch, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_amount, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total_vat, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        if (this._form_condition != null && MyLib._myGlobal._intPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_vat_type)) == 1)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total, _g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._is_add, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                        break;
                        */
            }
            return this._report._addLevel(levelName, this._report._level, __columnList, sumTotal, autoWidth);
        }



        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            if (_conditionScreen.DialogResult == DialogResult.Yes)
            {
                // start process
                this._report__init();
                // this._report._underZeroType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._format_number_0).ToString());
                this._dataTable = null; // จะได้ load data ใหม่
                //
                //_showFullVatRefer = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._show_full_invoice_doc_no).Equals("1") ? true : false;
                //_showDocNo = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._show_doc_no).Equals("1") ? true : false;
                this._report._build();
            }
        }

        SMLReport._generateLevelClass _reportInitMaster(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();

            switch (this._mode)
            {
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.erp_branch_list._code, "*", 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    break;
            }

            return this._report._addLevel("master", levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _levelDetail;
        void _report__init()
        {

            switch (this._mode)
            {
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                    {
                        if (this._conditionScreen._useBranchCheckbox.Checked == true)
                        {
                            this._report._level = this._reportInitMaster(null, false, false);
                            this._report._level._isHide = true;

                            _levelDetail = this._reportInitDetail(null, true, true);

                            this._report._resourceTable = ""; // แบบกำหนดเอง
                            this._recordCount = 0;

                        }
                        else
                        {
                            this._report._level = this._reportInitDetail(null, true, true, "master");
                            this._report._resourceTable = ""; // แบบกำหนดเอง
                            this._recordCount = 0;
                        }
                    }
                    break;
                default:
                    {
                        this._report._level = this._reportInitMaster(null, false, false);
                        this._report._level._isHide = true;

                        _levelDetail = this._reportInitDetail(null, true, true);

                        this._report._resourceTable = ""; // แบบกำหนดเอง
                        this._recordCount = 0;
                    }
                    break;
            }

        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }


        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
