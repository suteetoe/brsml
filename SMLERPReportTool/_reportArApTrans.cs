using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReportTool
{
    public partial class _reportArApTrans : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLReport._generateLevelClass _level2;
        Boolean _displayDetail = false;
        _reportEnum _mode;
        _g.g._transControlTypeEnum _transFlag;

        public _reportArApTrans(_reportEnum mode, _g.g._transControlTypeEnum transFlag, string screenName)
        {
            Boolean __landscape = false;
            switch (mode)
            {
            }

            this._screenName = screenName;
            this._mode = mode;
            this._transFlag = transFlag;
            this._report = new SMLReport._generate(screenName, __landscape);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (columnNumber == sender._findColumnName(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._bill_type))
            {
                sender._columnList[columnNumber]._dataStr = (sender._columnList[columnNumber]._dataStr.Length == 0) ? "" : _g.g._transFlagGlobal._transName((MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr)));
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
            if (level._levelName.Equals(this._levelNameRoot))
            {
                return this._dataTable.Select();
            }
            else
                if (level._levelName.Equals(this._levelNameDetail))
                {
                    StringBuilder __where = new StringBuilder();
                    for (int __loop = 0; __loop < 2; __loop++)
                    {
                        if (levelParent._columnList[__loop]._fieldName.Length > 0)
                        {
                            if (__where.Length > 0)
                            {
                                __where.Append(" and ");
                            }
                            __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                        }
                    }
                    return (this._dataTableDetail == null || this._dataTableDetail.Rows.Count == 0) ? null : this._dataTableDetail.Select(__where.ToString());
                }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;
            switch (this._mode)
            {
                case _reportEnum.เจ้าหนี้_จ่ายชำระหนี้:
                case _reportEnum.ลูกหนี้_รับชำระหนี้:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case SMLERPReportTool._reportTypeEnum.เจ้าหนี้:
                                __custCodeField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ap_code;
                                __custNameField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ap_name;
                                break;
                            case SMLERPReportTool._reportTypeEnum.ลูกหนี้:
                                __custCodeField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ar_code;
                                __custNameField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code, __custCodeField, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_name, __custNameField, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_net_value, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sum_pay_money_1, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ลูกหนี้_รับวางบิล:
                case _reportEnum.เจ้าหนี้_รับวางบิล:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case SMLERPReportTool._reportTypeEnum.เจ้าหนี้:
                                __custCodeField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ap_code;
                                __custNameField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ap_name;
                                break;
                            case SMLERPReportTool._reportTypeEnum.ลูกหนี้:
                                __custCodeField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ar_code;
                                __custNameField = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._due_date, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));                        
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code, __custCodeField, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_name, __custNameField, 40, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_net_value, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sum_pay_money_1, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ลูกหนี้_รายงานการรับชำระหนี้ประจำวัน:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_date, _g.d.resource_report._table + "." + _g.d.resource_report._bill_date, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_no, _g.d.resource_report._table + "." + _g.d.resource_report._bill_no, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans._cust_code, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ar_code, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans._cust_name, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ar_name, 45, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_debt_amount, null, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._balance_ref, null, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_pay_money, null, 15, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    //columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._balance_ref, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._doc_date, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ic_trans._sale_code, _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code, 15, SMLReport._report._cellType.String, 0, __fontStyle));

                    break;
                case _reportEnum.เจ้าหนี้_รับวางบิล_ยกเลิก:
                case _reportEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_ref_date, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans._cust_name, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ap_name, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;

                case _reportEnum.ลูกหนี้_รับวางบิล_ยกเลิก:
                case _reportEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_ref_date, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans._cust_name, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._ar_name, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;


            }
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            switch (this._mode)
            {
                case _reportEnum.ลูกหนี้_รับชำระหนี้:
                case _reportEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_date, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_no, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._bill_type, _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._bill_type_name, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._due_date, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_debt_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._balance_ref, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_pay_money, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.ลูกหนี้_รับวางบิล:
                case _reportEnum.เจ้าหนี้_รับวางบิล:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_date, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_no, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._bill_type, _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._bill_type_name, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._due_date, null, 15, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_debt_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._balance_ref, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_pay_money, _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_pay_money_1, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._displayDetail = this._form_condition._screen._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;
            this._report._level = this._reportInitRoot(null, true, true);
            if (this._displayDetail == true)
            {
                this._level2 = this._reportInitDetail(this._report._level, false, 2, true);
            }
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                try
                {

                    // toe
                    if (this._mode == _reportEnum.ลูกหนี้_รายงานการรับชำระหนี้ประจำวัน)
                    {
                        string __beginDate = this._form_condition._screen._getDataStr(_g.d.resource_report._from_date, true);
                        string __endDate = this._form_condition._screen._getDataStr(_g.d.resource_report._to_date, true);

                        string __fromDocNo = this._form_condition._screen._getDataStr(_g.d.resource_report._from_receive_no);
                        string __toDocNo = this._form_condition._screen._getDataStr(_g.d.resource_report._to_receive_no);

                        string __fromBillDate = this._form_condition._screen._getDataStr(_g.d.resource_report._from_bill, true);
                        string __toBillDate = this._form_condition._screen._getDataStr(_g.d.resource_report._to_bill, true);

                        string __fromBillNo = this._form_condition._screen._getDataStr(_g.d.resource_report._from_bill_no);
                        string __toBillNo = this._form_condition._screen._getDataStr(_g.d.resource_report._to_bill_no);

                        string __getWhereMain = this._form_condition._grid._createWhere(_g.d.ap_ar_trans._cust_code);

                        string __branchSelect = this._form_condition._screen._getDataStr(_g.d.resource_report._branch);
                        string __whereBranch = "";
                        if (__branchSelect.Length > 0)
                        {
                            __whereBranch = MyLib._myUtil._genCodeList(_g.d.ap_ar_trans._branch_code, __branchSelect);
                        }

                        StringBuilder __query1 = new StringBuilder();
                        __query1.Append("select ");
                        __query1.Append(MyLib._myGlobal._fieldAndComma(
                            "(select " + _g.d.ap_ar_trans._cust_code + " from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no + "=" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._doc_no + " and " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._trans_flag + " = " + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._trans_flag + " ) as cust_code",
                            _g.d.ap_ar_trans_detail._doc_no,
                            _g.d.ap_ar_trans_detail._doc_date,
                            _g.d.ap_ar_trans_detail._trans_flag,
                            _g.d.ap_ar_trans_detail._billing_no,
                            _g.d.ap_ar_trans_detail._billing_date,
                            _g.d.ap_ar_trans_detail._bill_type,
                            _g.d.ap_ar_trans_detail._sum_debt_amount,
                            _g.d.ap_ar_trans_detail._balance_ref,
                            _g.d.ap_ar_trans_detail._sum_pay_money,
                            "(select " + _g.d.ap_ar_trans._branch_code + " from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no + "=" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._doc_no + " and " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._trans_flag + " = " + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._trans_flag + " ) as " + _g.d.ap_ar_trans._branch_code
                            ));
                        __query1.Append(" from " + _g.d.ap_ar_trans_detail._table);
                        __query1.Append(" where  trans_flag = 239 and last_status = 0 ");

                        StringBuilder __where = new StringBuilder();
                        StringBuilder __query = new StringBuilder();
                        //__query.Append(MyLib._myGlobal._fieldAndComma(""));
                        __query.Append(" select ");
                        __query.Append(" cust_code as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans._cust_code + "\", ");
                        __query.Append(" ( select name_1 from ar_customer where ar_customer.code = temp1.cust_code ) as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans._cust_name + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._doc_date + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._doc_date + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._doc_no + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._doc_no + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._billing_no + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_no + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._billing_date + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._billing_date + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._bill_type + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._bill_type + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._sum_debt_amount + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_debt_amount + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._balance_ref + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._balance_ref + "\", ");
                        __query.Append(_g.d.ap_ar_trans_detail._sum_pay_money + " as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ap_ar_trans_detail._sum_pay_money + "\", ");

                        __query.Append(" (select " + _g.d.ic_trans._sale_code + " from ic_trans where ic_trans.doc_no = temp1.billing_no and ic_trans.trans_flag=temp1.bill_type ) as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ic_trans._sale_code + "\" , ");
                        __query.Append(" (select total_except_vat from ic_trans where ic_trans.doc_no = temp1.billing_no and ic_trans.trans_flag=temp1.bill_type ) as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ic_trans._total_except_vat + "\" , ");
                        __query.Append(" (select total_before_vat from ic_trans where ic_trans.doc_no = temp1.billing_no and ic_trans.trans_flag=temp1.bill_type ) as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ic_trans._total_before_vat + "\" , ");
                        __query.Append(" (select total_vat_value from ic_trans where ic_trans.doc_no = temp1.billing_no and ic_trans.trans_flag=temp1.bill_type ) as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ic_trans._total_vat_value + "\"  , ");
                        __query.Append(" (select total_amount from ic_trans where ic_trans.doc_no = temp1.billing_no and ic_trans.trans_flag=temp1.bill_type ) as \"" + _g.d.ap_ar_trans_detail._table + "." + _g.d.ic_trans._total_amount + "\" ");
                        __query.Append(" from ( " + __query1.ToString() + " ) as temp1 ");



                        //__query.Append(" from " + _g.d.ap_ar_trans_detail._table);
                        //__query.Append(" where " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + " and " + _g.d.ap_ar_trans._last_status + "=0" + " and " + _g.d.ap_ar_trans._trans_type + "=2 ");
                        //__query.Append(" where ");
                        __where.Append(" where " + _g.d.ap_ar_trans_detail._doc_date + " between " + __beginDate + " and " + __endDate);

                        if (__getWhereMain.Length > 0)
                        {
                            __where.Append(" and " + __getWhereMain);
                        }

                        if (__fromDocNo.Length > 0 && __toDocNo.Length > 0)
                        {
                            __where.Append(" and " + _g.d.ap_ar_trans_detail._doc_no + " between \'" + __fromDocNo + "\' and \'" + __toDocNo + "\'");
                        }

                        if (__fromBillDate.Length > 0 && __toBillDate.Length > 0 && __fromBillDate.IndexOf("null") == -1 && __toBillDate.IndexOf("null") == -1)
                        {
                            __where.Append(" and " + _g.d.ap_ar_trans_detail._billing_date + " between " + __fromBillDate + " and " + __toBillDate + " ");
                        }

                        if (__fromBillNo.Length > 0 && __toBillNo.Length > 0)
                        {
                            __where.Append(" and " + _g.d.ap_ar_trans_detail._billing_no + " between \'" + __fromBillNo + "\' and \'" + __toBillNo + "\'");
                        }

                        if (__whereBranch.Length > 0)
                        {
                            __where.Append(" and (" + __whereBranch.ToString() + ") ");
                        }

                        __query.Append(__where.ToString());
                        __query.Append(" order by doc_date, doc_no");
                        this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];

                    }
                    else
                    {
                        //
                        string __beginDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._from_date);
                        string __endDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._to_date);
                        Boolean __useLastStatus = this._form_condition._screen._getDataStr(_g.d.resource_report._show_cancel_document).ToString().Equals("1") ? true : false;
                        string __getWhereMain = "";
                        string __getWhere = "";
                        string __where = this._form_condition._extra._getWhere("").Replace(" where ", " and ");
                        //

                        string __lastStatusWhere = (__useLastStatus) ? "" : " and last_status=0";
                        //
                        StringBuilder __extraField = new StringBuilder();
                        StringBuilder __extraAs = new StringBuilder();
                        switch (this._mode)
                        {
                            case _reportEnum.ลูกหนี้_รับวางบิล:
                            case _reportEnum.ลูกหนี้_รับชำระหนี้:
                            case _reportEnum.เจ้าหนี้_จ่ายชำระหนี้:
                            case _reportEnum.เจ้าหนี้_รับวางบิล:
                            case _reportEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                            case _reportEnum.เจ้าหนี้_รับวางบิล_ยกเลิก:
                            case _reportEnum.ลูกหนี้_รับวางบิล_ยกเลิก:
                            case _reportEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                                __extraAs.Append("cust_code as \"ap_ar_trans.cust_code\",");
                                __extraAs.Append("cust_name as \"ap_ar_trans.cust_name\",");
                                if (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.เจ้าหนี้)
                                {
                                    __extraField.Append("cust_code,(select name_1 from ap_supplier where ap_ar_trans.cust_code=ap_supplier.code) as cust_name,");
                                }
                                else
                                {
                                    __extraField.Append("cust_code,(select name_1 from ar_customer where ap_ar_trans.cust_code=ar_customer.code) as cust_name,");
                                }
                                __getWhereMain = this._form_condition._grid._createWhere(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code);
                                break;
                        }

                        if (this._mode == _reportEnum.เจ้าหนี้_รับวางบิล_ยกเลิก || this._mode == _reportEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก || this._mode == _reportEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก || this._mode == _reportEnum.ลูกหนี้_รับวางบิล_ยกเลิก)
                        {
                            __extraAs.Append(_g.d.ap_ar_trans._doc_ref + " as \"ap_ar_trans.doc_ref\",");
                            __extraAs.Append(_g.d.ap_ar_trans._doc_ref_date + " as \"ap_ar_trans.doc_ref_date\",");
                            __extraField.Append(_g.d.ap_ar_trans._doc_ref + "," + _g.d.ap_ar_trans._doc_ref_date + ",");
                        }

                        if (__getWhereMain.Length > 0) __getWhereMain = " and (" + __getWhereMain + ")";
                        if (__getWhere.Length > 0) __getWhere = " and (" + __getWhere + ")";
                        //
                        StringBuilder __query = new StringBuilder();
                        __query.Append("select " + __extraAs.ToString());
                        __query.Append("doc_date as \"ap_ar_trans.doc_date\",");
                        __query.Append("doc_no as \"ap_ar_trans.doc_no\",");
                        __query.Append("due_date as \"ap_ar_trans." + _g.d.ap_ar_trans._due_date + "\",");
                        __query.Append("total_value as \"ap_ar_trans.total_value\",");
                        __query.Append("total_before_vat as \"ap_ar_trans.total_before_vat\",");
                        __query.Append("total_discount as \"ap_ar_trans.total_discount\",");
                        __query.Append("total_vat_value as \"ap_ar_trans.total_vat_value\",");
                        __query.Append("total_net_value as \"ap_ar_trans.total_net_value\",");
                        __query.Append("last_status as \"ap_ar_trans.last_status\"");
                        __query.Append(" from (select ");
                        __query.Append(__extraField.ToString() + "doc_date,doc_no,due_date,last_status,total_value,total_before_vat,total_discount,total_vat_value,total_net_value");
                        __query.Append(" from ap_ar_trans where trans_flag=" + _g.g._transFlagGlobal._transFlag(this._transFlag).ToString() + " and ap_ar_trans.doc_date between " + __beginDate + " and " + __endDate + __lastStatusWhere + __getWhereMain + __where + ") as temp1");
                        __query.Append(this._form_condition._extra._getOrderBy() + ",doc_no");
                        //
                        StringBuilder __queryDetail = new StringBuilder();
                        __queryDetail.Append("select ");
                        __queryDetail.Append("doc_date as \"ap_ar_trans.doc_date\","); // กำหนดเป็น ap_ar_trans เพื่อใช้สำหรับ datatable select
                        __queryDetail.Append("doc_no as \"ap_ar_trans.doc_no\","); // กำหนดเป็น ap_ar_trans เพื่อใช้สำหรับ datatable select
                        __queryDetail.Append("billing_no as \"ap_ar_trans_detail.billing_no\",");
                        __queryDetail.Append("billing_date as \"ap_ar_trans_detail.billing_date\",");
                        __queryDetail.Append("bill_type as \"ap_ar_trans_detail.bill_type\",");
                        __queryDetail.Append("due_date as \"ap_ar_trans_detail.due_date\",");
                        __queryDetail.Append("sum_debt_amount as \"ap_ar_trans_detail.sum_debt_amount\",");
                        __queryDetail.Append("balance_ref as \"ap_ar_trans_detail.balance_ref\",");
                        __queryDetail.Append("sum_pay_money as \"ap_ar_trans_detail.sum_pay_money\"");
                        __queryDetail.Append(" from (select ");
                        __queryDetail.Append("doc_date,doc_no,billing_no,billing_date,bill_type,due_date,sum_debt_amount,balance_ref,sum_pay_money");
                        __queryDetail.Append(" from ap_ar_trans_detail where trans_flag=" + _g.g._transFlagGlobal._transFlag(this._transFlag).ToString() + " and ap_ar_trans_detail.doc_date between " + __beginDate + " and " + __endDate + __getWhere + " order by doc_date,doc_no,line_number) as temp1");
                        //
                        this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                        this._dataTableDetail = (this._displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetail.ToString()).Tables[0] : null;
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);
                this._report__init();
                this._form_condition._extra._tableName = _g.d.ap_ar_trans._table;
                string[] __fieldList = this._report._level.__fieldList(false).Split(',');
                this._form_condition._extra._addFieldComboBox(__fieldList);
                //this._form_condition._title = this._screenName;
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._processClick)
            {
                this._dataTable = null; // จะได้ load data ใหม่
                //
                string __beginDate = this._form_condition._screen._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._form_condition._screen._getDataStr(_g.d.resource_report._to_date);
                this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                this._report._conditionText = _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
                //
                this._report._build();
            }
        }
    }
}
