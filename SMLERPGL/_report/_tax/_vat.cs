using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SMLERPGL._tax
{
    public partial class _vat : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private _vatConditionForm _form_condition;
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private _vatConditionType _mode;
        private int _recordCount;

        public _vat(_vatConditionType conditionType, string screenName)
        {
            this._mode = conditionType;
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._renderHeader += new SMLReport._generate.RenderHeaderEventHandler(_report__renderHeader);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report__renderHeader(SMLReport._generate source)
        {
            switch (this._mode)
            {
                case _vatConditionType.ภาษีซื้อ:
                case _vatConditionType.ภาษีซื้อ_สินค้ายกเว้นภาษี:
                case _vatConditionType.ภาษีขาย:
                case _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี:
                case _vatConditionType.ภาษีขาย_สรุป:
                case _vatConditionType.ภาษีขาย_เลขประจำตัวผู้เสียภาษี:
                case _vatConditionType.ภาษีซื้อ_เลขประจำตัวผู้เสียภาษี:
                    string __reportName = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._report_name);

                    SMLReport._report._objectListType __headerObject = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                    int __column0 = source._viewControl._addColumn(__headerObject, 100);
                    //
                    source._viewControl._addCell(__headerObject, __column0, true, 0, -1, SMLReport._report._cellType.String, __reportName, SMLReport._report._cellAlign.Center, source._viewControl._fontHeader1);
                    //
                    int __monthNumber = MyLib._myGlobal._intPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_month));

                    // หมวดภาษี
                    //string __vatGroup = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_group);

                    string __headStr1 = MyLib._myGlobal._resource("เดือนภาษี") + " " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.g._monthName()[__monthNumber].ToString()))._str + " " + MyLib._myGlobal._resource("ปี") + " " + this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year) + ((_vatGroupName.Length > 0) ? " กลุ่มภาษี " + _vatGroupName : "");
                    source._viewControl._addCell(__headerObject, __column0, true, 1, -1, SMLReport._report._cellType.String, __headStr1, SMLReport._report._cellAlign.Center, source._viewControl._fontHeader2);
                    //
                    SMLReport._report._objectListType __headerObject2 = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                    int __column1 = source._viewControl._addColumn(__headerObject2, 60);
                    int __column2 = source._viewControl._addColumn(__headerObject2, 20);
                    int __column3 = source._viewControl._addColumn(__headerObject2, 20);
                    //
                    source._viewControl._addCell(__headerObject2, __column1, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("ชื่อผู้ประกอบการ") + " : " + MyLib._myGlobal._ltdBusinessName, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                    source._viewControl._addCell(__headerObject2, __column2, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("เลขประจำตัวผู้เสียภาษี") + " : " + MyLib._myGlobal._ltdTax, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                    source._viewControl._addCell(__headerObject2, __column3, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("หน้า") + " " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, source._viewControl._fontHeader2);
                    //
                    source._viewControl._addCell(__headerObject2, __column1, true, 1, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("ชื่อสถานประกอบการ") + " : " + MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                    source._viewControl._addCell(__headerObject2, __column2, true, 1, -1, SMLReport._report._cellType.String, "[ " + ((MyLib._myGlobal._branch_type == 0) ? "X" : " ") + " ] " + MyLib._myGlobal._resource("สำนักงานใหญ่") + "       [ " + ((MyLib._myGlobal._branch_type == 1) ? "X" : " ") + " ] " + MyLib._myGlobal._resource("สาขาลำดับที่") + ((MyLib._myGlobal._branch_type == 1) ? "  " + MyLib._myGlobal._branch_code : ""), SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                    //
                    source._viewControl._addCell(__headerObject2, __column1, true, 2, -1, SMLReport._report._cellType.String, MyLib._myGlobal._resource("ที่อยู่") + " : " + MyLib._myGlobal._ltdAddress, SMLReport._report._cellAlign.Left, source._viewControl._fontHeader2);
                    //
                    break;
            }
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Data)
            {
                switch (this._mode)
                {
                    case _vatConditionType.ภาษีซื้อ:
                    case _vatConditionType.ภาษีซื้อ_สินค้ายกเว้นภาษี:
                    case _vatConditionType.ภาษีซื้อ_เลขประจำตัวผู้เสียภาษี:
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
                }
            }
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (this._dataTable == null)
            {
                return null;
            }
            return this._dataTable.Select();
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            switch (this._mode)
            {
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

            }
            return this._report._addLevel("temp", levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._report._level = this._reportInitDetail(null, true, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
            this._recordCount = 0;
        }

        bool _showFullVatRefer = false;
        bool _showDocNo = false;
        StringBuilder _vatGroupName = new StringBuilder();

        string _whereInPack(string value)
        {
            string[] __vatGroupSplit = value.Split(',');
            StringBuilder __result = new StringBuilder();
            foreach (string _group in __vatGroupSplit)
            {
                if (__result.Length > 0)
                {
                    __result.Append(",");
                }

                if (_group.IndexOf(':') != -1)
                {
                    __result.Append("\'" + _group.Split(':')[0] + "\',\'" + _group.Split(':')[1] + "\'");
                }
                else
                {
                    __result.Append("\'" + _group + "\'");
                }
            }
            return __result.ToString();
        }

        void _report__query()
        {
            this._recordCount = 0;

            if (this._dataTable == null)
            {
                try
                {
                    //string __beginDate = this._form_condition._conditionScreenTop._getDataStrQuery(_g.d.resource_report_vat._date_begin);
                    //string __endDate = this._form_condition._conditionScreenTop._getDataStrQuery(_g.d.resource_report_vat._date_end);
                    int __month = (int)MyLib._myGlobal._decimalPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_month)) + 1;
                    int __year = (int)MyLib._myGlobal._decimalPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year));
                    //

                    bool __showOnlyAmountVat = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._show_vat_amount_only).ToString().Equals("1") ? true : false;
                    _vatGroupName = new StringBuilder();

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "";
                    string __orderBy = __orderBy = this._form_condition._whereControl._getOrderBy();
                    switch (this._mode)
                    {
                        case _vatConditionType.ภาษีขาย:
                        case _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี:
                        case _vatConditionType.ภาษีขาย_สรุป:
                        case _vatConditionType.ภาษีขาย_เลขประจำตัวผู้เสียภาษี:
                            {
                                // toe ดึงชื่อกลุ่มภาษี
                                string __vatGroupSelect = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_group).ToString();

                                string __docType = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._doc_type).ToString();
                                string __branchCode = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._branch_code).ToString();


                                string __extraWhere2 = "";

                                if (__docType.Length > 0)
                                {
                                    __extraWhere2 += " (case when gl_journal_vat_sale.trans_flag not in (239,19) then (select doc_format_code from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and trans_flag = gl_journal_vat_sale.trans_flag ) else (select doc_format_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_sale.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_sale.trans_flag ) end) in (" + _whereInPack(__docType) + ") ";
                                }

                                if (__branchCode.Length > 0)
                                {
                                    if (__docType.Length > 0)
                                        __extraWhere2 += " and ";

                                    __extraWhere2 += " (case when gl_journal_vat_sale.trans_flag not in (239,19) then (select branch_code from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and trans_flag = gl_journal_vat_sale.trans_flag ) else (select branch_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_sale.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_sale.trans_flag ) end) in (" + _whereInPack(__branchCode) + ") ";
                                }

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

                                __query = "select " + this._report._level.__fieldList(true) + "," + _g.d.gl_journal_vat_sale._is_add + __extraField + " from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._vat_effective_year + "=" + __year.ToString() + " and " + _g.d.gl_journal_vat_sale._vat_effective_period + "=" + __month.ToString() + ((__showOnlyAmountVat) ? " and " + _g.d.gl_journal_vat_sale._amount + "<>0 " : "") + ((__vatGroup.Length > 0) ? " and " + __vatGroup : "") + ((__extraWhere2.Length > 0) ? " and " + __extraWhere2 : "");

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
                        case _vatConditionType.ภาษีซื้อ:
                        case _vatConditionType.ภาษีซื้อ_สินค้ายกเว้นภาษี:
                        case _vatConditionType.ภาษีซื้อ_เลขประจำตัวผู้เสียภาษี:
                            {

                                string __docType = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._doc_type).ToString();
                                string __branchCode = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._branch_code).ToString();


                                string __extraWhere2 = "";

                                if (__docType.Length > 0)
                                {
                                    __extraWhere2 += " (case when gl_journal_vat_buy.trans_flag not in (239,19) then (select doc_format_code from ic_trans where ic_trans.doc_no = gl_journal_vat_buy.doc_no and trans_flag = gl_journal_vat_buy.trans_flag ) else (select doc_format_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_buy.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_buy.trans_flag ) end) in (" + _whereInPack(__docType) + ") ";
                                }

                                if (__branchCode.Length > 0)
                                {
                                    if (__docType.Length > 0)
                                        __extraWhere2 += " and ";

                                    __extraWhere2 += " (case when gl_journal_vat_buy.trans_flag not in (239,19) then (case when gl_journal_vat_buy.trans_flag in (1999) then (select branch_code from gl_journal where gl_journal.doc_no = gl_journal_vat_buy.doc_no ) else (select branch_code from ic_trans where ic_trans.doc_no = gl_journal_vat_buy.doc_no and trans_flag = gl_journal_vat_buy.trans_flag )end ) else (select branch_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_vat_buy.doc_no and ap_ar_trans.trans_flag = gl_journal_vat_buy.trans_flag) end) in(" + _whereInPack(__branchCode) + ") ";
                                }

                                string __vatGroup = MyLib._myUtil._genCodeList(_g.d.gl_journal_vat_buy._vat_group, this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_group).ToString());

                                // โต๋เอา gl_journal_vat_buy.vat_type = 1 ออก ขอคืนไม่ได้
                                __query = "select " + this._report._level.__fieldList(true) + "," + _g.d.gl_journal_vat_sale._is_add + " from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._vat_effective_year + "=" + __year.ToString() + " and " + _g.d.gl_journal_vat_buy._vat_effective_period + "=" + __month.ToString() + " and " + _g.d.gl_journal_vat_buy._vat_type + " in (0) " + ((__showOnlyAmountVat) ? " and " + _g.d.gl_journal_vat_buy._vat_amount + "<>0 " : "") + ((__vatGroup.Length > 0) ? " and " + __vatGroup : "") + ((__extraWhere2.Length > 0) ? " and " + __extraWhere2 : "");
                                string __query1 = "case when coalesce(ap_name, '')='' then (select name_1 from ap_supplier where code = gl_journal_vat_buy.ap_code) else ap_name end ";
                                __query = __query.Replace("," + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._ap_name, "," + __query1);
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_count + " as ", "\'\'" + " as ");
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + " as ", _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + "*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + " as ", _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + "*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");

                                if (this._mode == _vatConditionType.ภาษีซื้อ_สินค้ายกเว้นภาษี)
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total + " as ", "(" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1 + "+" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + "+" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + ")*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1 + " as ", "case when (select last_status from ic_trans where ic_trans.doc_no=gl_journal_vat_buy.doc_no and ic_trans.trans_flag=gl_journal_vat_buy.trans_flag)=1 then 0 else " + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_except_amount_1 + "*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " end as ");
                                }
                                else
                                {
                                    __query = __query.Replace(_g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._total + " as ", "(" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_amount + "+" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_base_amount + ")*" + _g.d.gl_journal_vat_buy._table + "." + _g.d.gl_journal_vat_buy._vat_calc + " as ");
                                }

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


                                switch (this._form_condition._whereControl._getOrderSelectIndexNumber())
                                {
                                    case 1: // เรียงตามวันที่
                                        __orderBy = __orderBy + "," + _g.d.gl_journal_vat_buy._vat_doc_no;
                                        break;
                                }
                            }
                            break;
                    }
                    //

                    string __fullQuery = __query + __orderBy;

                    if (this._mode == _vatConditionType.ภาษีขาย_สรุป)
                    {
                        // toe จัดการภาษีแบบสรุป
                        DataTable __getData = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __fullQuery).Tables[0];

                        DataTable __result = new DataTable("Result");
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count, typeof(String));
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date, typeof(String));
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number, typeof(String));
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount, typeof(Decimal));
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount, typeof(Decimal));
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount, typeof(Decimal));
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total, typeof(Decimal));
                        __result.Columns.Add(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status, typeof(String));

                        if (__getData.Rows.Count > 0)
                        {
                            int __vatCountColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count);
                            int __vatDateColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date);
                            int __vatNumberColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number);
                            int __exceptTaxColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount);
                            int __baseCalcColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount);
                            int __amountColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount);
                            int __totalColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total);
                            int __docStatusColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status);
                            int __fullInvoiceColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no);
                            int __isPosColumnNumber = __getData.Columns.IndexOf("is_pos");

                            string __firstDocNo = "";
                            string __firstDocDate = "";
                            string __lastDocNo = "";
                            decimal __exceptVatAmountSum = 0M;
                            decimal __baseTaxAmountSum = 0M;
                            decimal __amountSum = 0M;

                            // foreach (DataRow __dataRow in __getData.Rows)
                            for (int __row = 0; __row < __getData.Rows.Count; __row++)
                            {
                                DataRow __dataRow = __getData.Rows[__row];
                                string __getDocNo = __dataRow[__vatNumberColumnNumber].ToString();
                                string __getDocDate = __dataRow[__vatDateColumnNumber].ToString();
                                string __getFullInvoiceDocNo = __dataRow[__fullInvoiceColumnNumber].ToString();
                                int __isPos = MyLib._myGlobal._intPhase(__dataRow[__isPosColumnNumber].ToString());

                                decimal __exceptVatAmount = MyLib._myGlobal._decimalPhase(__dataRow[__exceptTaxColumnNumber].ToString());
                                decimal __baseTaxAmount = MyLib._myGlobal._decimalPhase(__dataRow[__baseCalcColumnNumber].ToString());
                                decimal __amount = MyLib._myGlobal._decimalPhase(__dataRow[__amountColumnNumber].ToString());
                                int __docStatus = MyLib._myGlobal._intPhase(__dataRow[__docStatusColumnNumber].ToString());
                                decimal __total = MyLib._myGlobal._decimalPhase(__dataRow[__totalColumnNumber].ToString());

                                bool __isLastRow = false;
                                if (__row == __getData.Rows.Count - 1)
                                {
                                    __isLastRow = true;
                                }

                                // หากเป็นใบกำกับภาษีอย่างย่อ
                                if (__isPos == 1)
                                {
                                    // sum

                                    // หากเป็นใบแรก
                                    if (__firstDocNo.Length == 0 && __getFullInvoiceDocNo.Length == 0)
                                    {
                                        __firstDocNo = __getDocNo;
                                        __firstDocDate = __getDocDate;
                                        __lastDocNo = __getDocNo;

                                        __exceptVatAmountSum += __exceptVatAmount;
                                        __baseTaxAmountSum += __baseTaxAmount;
                                        __amountSum += __amount;

                                        if (__isLastRow == false)
                                            continue;
                                    }

                                    // หากต่อจากใบเดิม
                                    if (__firstDocDate.Equals(__getDocDate) && _isNextDocNo(__lastDocNo, __getDocNo) && __getFullInvoiceDocNo.Length == 0 && __docStatus == 0)
                                    {
                                        __exceptVatAmountSum += __exceptVatAmount;
                                        __baseTaxAmountSum += __baseTaxAmount;
                                        __amountSum += __amount;

                                        __lastDocNo = __getDocNo;

                                        if (__isLastRow == false)
                                            continue;
                                    }
                                }

                                string __vatNumber = __getDocNo;
                                string __docDate = __getDocDate;

                                // add to datatable

                                // มีรายการค้างเก่าเหรือไม่
                                if (__firstDocNo.Length > 0)
                                {
                                    string __shortInvNo = __firstDocNo + " ถึง " + __lastDocNo;
                                    Decimal __sumtotal = __baseTaxAmountSum + __amountSum;
                                    // add sum short invoice
                                    __result.Rows.Add(
                                        "",
                                        __firstDocDate,
                                        __shortInvNo,
                                        __exceptVatAmountSum,
                                        __baseTaxAmountSum,
                                        __amountSum,
                                        __sumtotal,
                                        "0"
                                        );

                                }

                                // หากเป็นใบกำกับภาษีอย่างย่อ (อาจจะวันที่ใหม่ หรือ format ต่อกันออกไป) และไม่มีการออกอ้างอิงอย่างเต็ม และ ไม่ถูกยกเลิก ให้เก็บเข้าระบบหาใบต่อไป
                                if (__isPos == 1 && __getFullInvoiceDocNo.Length == 0 && __docStatus == 0)
                                {
                                    __firstDocNo = __getDocNo;
                                    __firstDocDate = __getDocDate;
                                    __lastDocNo = __getDocNo;

                                    __exceptVatAmountSum = __exceptVatAmount;
                                    __baseTaxAmountSum = __baseTaxAmount;
                                    __amountSum = __amount;

                                    if (__isLastRow == false)
                                        continue;
                                }
                                else
                                {
                                    // หากเป็นใบกำกับภาษีอย่างเต็มออกแทน
                                    if (__getFullInvoiceDocNo.Length > 0)
                                    {
                                        __vatNumber = __getFullInvoiceDocNo + " (ออกแทน " + __getDocNo + ")";
                                    }

                                    // add
                                    __result.Rows.Add(
                                        "",
                                        __docDate,
                                        __vatNumber,
                                        __exceptVatAmount,
                                        __baseTaxAmount,
                                        __amount,
                                        __total,
                                        __docStatus.ToString()
                                        );

                                }

                                __firstDocNo = "";
                                __lastDocNo = "";
                                __firstDocDate = "";
                                __exceptVatAmountSum = 0M;
                                __baseTaxAmountSum = 0M;
                                __amountSum = 0M;

                            }
                        }

                        // old                        
                        /*
                        if (__getData.Rows.Count > 0)
                        {
                            int __vatCountColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_count);
                            int __vatDateColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_date);
                            int __vatNumberColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._vat_number);
                            int __exceptTaxColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._except_tax_amount);
                            int __baseCalcColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._base_caltax_amount);
                            int __amountColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._amount);
                            int __totalColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._total);
                            int __docStatusColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.gl_journal_vat_sale._doc_status);
                            int __fullInvoiceColumnNumber = __getData.Columns.IndexOf(_g.d.gl_journal_vat_sale._table + "." + _g.d.resource_report_vat._full_invoice_doc_no);
                            int __isPosColumnNumber = __getData.Columns.IndexOf("is_pos");
                            // เริ่ม จัดแถว
                            string __firstDocNo = "";
                            string __lastDocNo = "";
                            string __lastDocDate = "";
                            decimal __exceptVatAmount = 0M;
                            decimal __baseTaxAmount = 0M;
                            decimal __amount = 0M;
                            int __lastdoc_status = 0;

                            bool __isFirst = true;
                            bool __isLast = false;
                            for (int __row = 0; __row < __getData.Rows.Count; __row++)
                            {
                                bool __isAddRecord = false;
                                DataRow __dataRow = __getData.Rows[__row];

                                string __getDocNo = __dataRow[__vatNumberColumnNumber].ToString();
                                string __getDocDate = __dataRow[__vatDateColumnNumber].ToString();
                                string __getFullInvoiceDocNo = __dataRow[__fullInvoiceColumnNumber].ToString();

                                int __isPos = MyLib._myGlobal._intPhase(__dataRow[__isPosColumnNumber].ToString());
                                if (__row == __getData.Rows.Count - 1)
                                {
                                    __isLast = true;
                                }

                                if (__isFirst)
                                {
                                    __firstDocNo = __getDocNo;
                                    __lastDocNo = __getDocNo;
                                    __lastDocDate = __getDocDate;

                                    // sum value
                                    __isFirst = false;
                                }

                                // last row

                                else
                                {

                                    if (__lastDocDate.Equals(__getDocDate) && _isNextDocNo(__lastDocNo, __getDocNo) && __getFullInvoiceDocNo.Length == 0)
                                    {
                                        __isAddRecord = false;
                                    }
                                    else
                                    {
                                        __isAddRecord = true;
                                    }

                                    if (__isAddRecord)
                                    {
                                        string __vatNumber = __firstDocNo + " ถึง " + __lastDocNo;

                                        decimal __total = 0M;
                                        __result.Rows.Add(
                                            "",
                                            __lastDocDate,
                                            __vatNumber,
                                            __exceptVatAmount,
                                            __baseTaxAmount,
                                            __amount,
                                            __total,
                                            __lastdoc_status.ToString()
                                            );

                                        if (__getFullInvoiceDocNo.Length > 0)
                                        {
                                            __vatNumber = __getFullInvoiceDocNo + " ออกแทน " + __getDocNo;

                                            __isFirst = true;

                                            __result.Rows.Add(
                                                "",
                                                __getDocDate,
                                                __vatNumber,
                                                __exceptVatAmount,
                                                __baseTaxAmount,
                                                __amount,
                                                __total,
                                                __lastdoc_status.ToString());

                                        }
                                        else
                                        {
                                            // เป็นเอกสาร คนละวันที่ หรือเอกสารไม่ เรียงกัน
                                            __lastDocDate = __getDocDate;
                                            __firstDocNo = __getDocNo;
                                            __lastDocNo = __getDocNo;
                                        }

                                        // clear sum

                                    }
                                    else
                                    {
                                        __lastDocNo = __getDocNo;
                                        //sum
                                    }

                                    if (__isLast)
                                    {
                                        // หากเป็นรายการสุดท้าย
                                        string __vatNumber = "";
                                        decimal __total = 0M;

                                        if (__isAddRecord)
                                        {
                                            // และมีการเพิ่ม record ก่อนหน้า
                                            __vatNumber = __firstDocNo + " ถึง " + __lastDocNo;
                                        }
                                        else
                                        {
                                            __vatNumber = __firstDocNo + " ถึง " + __lastDocNo;

                                        }
                                        __result.Rows.Add(
                                            "",
                                            __lastDocDate,
                                            __vatNumber,
                                            __exceptVatAmount,
                                            __baseTaxAmount,
                                            __amount,
                                            __total,
                                            __lastdoc_status.ToString()
                                            );


                                    }
                                }

                                
                            }
                        }
                        */

                        this._dataTable = __result;
                    }
                    else
                    {
                        this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __fullQuery).Tables[0];
                    }
                    this._recordCount = 0;
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        bool _isNextDocNo(string docNo, string docNo2)
        {
            // regex (.*)+([a-z])+([0-9]+)
            Regex __numberRegex = new Regex(@"^([0-9]+)$");
            if (__numberRegex.IsMatch(docNo))
            {
                int __docNoLength = docNo.Length;

                decimal __number = MyLib._myGlobal._decimalPhase(docNo) + 1;
                string __nextDocNo = __number.ToString().PadLeft(__docNoLength, '0'); //string.Format("{0:D" + __docNoLength + "}", __number); //__number.tostr(4);
                if (__nextDocNo == docNo2)
                {
                    return true;
                }
            }
            else
            {
                Regex __replaceNumberRegex = new Regex(@"(.*)+([a-zA-Z\-]+)+([0-9]+)");
                if (__replaceNumberRegex.IsMatch(docNo))
                {
                    Match __matches = __replaceNumberRegex.Match(docNo);
                    string __numberMatch = __matches.Groups[3].Value.ToString();

                    int __length = __numberMatch.Length;

                    int __startIndex = docNo.LastIndexOf(__numberMatch);
                    int __nextNumber = MyLib._myGlobal._intPhase(__numberMatch) + 1;
                    string __prefix = docNo.Substring(0, __startIndex);

                    string __nextDocNo = __prefix + __nextNumber.ToString().PadLeft(__length, '0');

                    if (docNo2 == __nextDocNo)
                    {
                        return true;
                    }

                }
            }


            return false;
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._report__init();
                this._form_condition = new _vatConditionForm(this._mode, this._screenName);
                string[] __fieldList = this._report._level.__fieldList(false).Split(',');
                //
                this._form_condition._whereControl._tableName = _g.d.gl_journal_vat_sale._table;
                this._form_condition._whereControl._addFieldComboBox(__fieldList, 1);
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                this._report__init();
                this._report._underZeroType = MyLib._myGlobal._intPhase(this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._format_number_0).ToString());
                this._dataTable = null; // จะได้ load data ใหม่
                //
                _showFullVatRefer = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._show_full_invoice_doc_no).Equals("1") ? true : false;
                _showDocNo = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._show_doc_no).Equals("1") ? true : false;
                this._report._build();
            }
        }
    }
}
