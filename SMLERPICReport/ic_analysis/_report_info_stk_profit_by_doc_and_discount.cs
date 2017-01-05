using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport.ic_analysis
{
    public partial class _report_info_stk_profit_by_doc_and_discount : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLReport._generateLevelClass _level2;
        SMLERPReportTool._reportEnum _mode;
        _g.g._transControlTypeEnum _transFlag;

        public _report_info_stk_profit_by_doc_and_discount(string screenName, SMLERPReportTool._reportEnum mode)
        {
            this._screenName = screenName;
            this._mode = mode;
            this._report = new SMLReport._generate(screenName, true);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._sumTotal += new SMLReport._generate.SumTotalEventHandler(_report__sumTotal);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report__sumTotal(SMLReport._generateLevelClass level, int columnNumber, decimal value)
        {
            if (level != null && level.__grandTotal.Count > 0)
            {
                level.__grandTotal[columnNumber]._sumTotalColumn += value;
                if (level._levelParent != null)
                {
                    this._report__sumTotal(level._levelParent, columnNumber, value);
                }
            }
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (columnNumber == sender._findColumnName(_g.d.ic_resource._table + "." + _g.d.ic_resource._profit_lost_persent))
            {
                decimal __amountNet = sender._columnList[sender._findColumnName(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount_net)]._dataNumber;
                decimal __costNet = sender._columnList[sender._findColumnName(_g.d.ic_resource._table + "." + _g.d.ic_resource._cost_net)]._dataNumber;
                decimal __profit = __amountNet - __costNet;
                decimal __calc = (__amountNet == 0.0M) ? 0.0M : ((__profit * 100) / __amountNet);
                sender._columnList[columnNumber]._dataNumber = __calc;
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
                    string __whereStr = _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date + "=\'" + source[levelParent._columnList[0]._fieldName].ToString().Replace("\'", "\'\'") + "\'";
                    return (this._dataTableDetail == null || this._dataTableDetail.Rows.Count == 0) ? null : this._dataTableDetail.Select(__whereStr);
                }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = FontStyle.Bold;
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date, null, 15, SMLReport._report._cellType.DateTime, 0, __fontStyle));
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
                case SMLERPReportTool._reportEnum.สินค้า_รายงานกำไรขั้นต้นตามเอกสารแบบมีส่วนลด:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._doc_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ar_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._ar_detail, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    // ขาย
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount_sale, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._trans_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._cost_sale, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    // รับคืน
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount_sale_return, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._cost_sale_return, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    // เพิ่มหนี้
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount_sale_debit, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._cost_sale_debit, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    // ผลการคำนวณ
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._amount_net, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._cost_net, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._profit_lost_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._table + "." + _g.d.ic_resource._profit_lost_persent, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
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
            this._report._level = this._reportInitRoot(null, true, true);
            this._level2 = this._reportInitDetail(this._report._level, true, 2, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(this._report._level.__grandTotal, 2);
            this._reportInitColumnValue(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._level2;
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    string __beginDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._from_date);
                    string __endDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._to_date);
                    string __where = this._form_condition._extra._getWhere("").Replace(" where ", " and ");
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    //
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select doc_date as \"ic_resource.doc_date\",doc_no as \"ic_resource.doc_no\",cust_code as \"ic_resource.ar_code\",");
                    __query.Append("(select name_1 from ar_customer where ar_customer.code=cust_code) as \"ic_resource.ar_detail\",");
                    __query.Append("total_value as \"ic_resource.amount_sale\",");
                    __query.Append("total_discount as \"ic_resource.trans_discount\",cost_sale as \"ic_resource.cost_sale\",");
                    __query.Append("amount_sale_return as \"ic_resource.amount_sale_return\",sum_of_cost_sale_return as \"ic_resource.cost_sale_return\",");
                    __query.Append("amount_sale_debit as \"ic_resource.amount_sale_debit\",sum_of_cost_sale_debit as \"ic_resource.cost_sale_debit\",");
                    __query.Append("amount_net as \"ic_resource.amount_net\",cost_net as \"ic_resource.cost_net\",amount_net-cost_net as \"ic_resource.profit_lost_amount\",");
                    __query.Append("case when amount_net=0 then 0 else round(((amount_net-cost_net)*100)/amount_net," + _g.g._companyProfile._item_amount_decimal.ToString() + ")  end as \"ic_resource.profit_lost_persent\" from ");
                    //
                    __query.Append("(select doc_date,doc_no,cust_code,total_value,total_discount,sum_of_cost as cost_sale,amount_sale_return,sum_of_cost_sale_return,amount_sale_debit,sum_of_cost_sale_debit,(total_value+amount_sale_debit)-(total_discount+amount_sale_return) as amount_net,(sum_of_cost+sum_of_cost_sale_debit)-sum_of_cost_sale_return as cost_net from ");
                    //
                    __query.Append("(select doc_date,doc_no,cust_code,case when vat_type=0 then total_value else round(total_value*100/(100+vat_rate)," + _g.g._companyProfile._item_amount_decimal.ToString() + ") end as total_value,case when vat_type=0 then total_discount else round(total_discount*100/(100+vat_rate)," + _g.g._companyProfile._item_amount_decimal.ToString() + ") end as total_discount,(select coalesce(sum(sum_of_cost),0) from ic_trans_detail where ic_trans_detail.doc_no=ic_trans.doc_no and ic_trans_detail.last_status=0 and ic_trans_detail.trans_flag=ic_trans.trans_flag) as sum_of_cost,");
                    __query.Append("(select coalesce(sum(sum_amount_exclude_vat),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.last_status=0 and ic_trans_detail.trans_flag=48) as amount_sale_return,");
                    __query.Append("(select coalesce(sum(sum_of_cost),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.last_status=0 and ic_trans_detail.trans_flag=48) as sum_of_cost_sale_return,");
                    __query.Append("(select coalesce(sum(sum_amount_exclude_vat),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.last_status=0 and ic_trans_detail.trans_flag=46) as amount_sale_debit,");
                    __query.Append("(select coalesce(sum(sum_of_cost),0) from ic_trans_detail where ic_trans_detail.ref_doc_no=ic_trans.doc_no and ic_trans_detail.last_status=0 and ic_trans_detail.trans_flag=46) as sum_of_cost_sale_debit");
                    __query.Append(" from ic_trans where trans_flag=44 and last_status=0) as temp1) as temp2 where doc_date between " + __beginDate + " and " + __endDate + " order by doc_date,doc_no");
                    string __queryStr = __query.ToString();
                    this._dataTableDetail = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryStr).Tables[0];
                    this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTableDetail, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date);
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
                this._form_condition._extra._tableName = _g.d.ic_resource._table;
                string[] __fieldList = this._report._level.__fieldList(false).Split(',');
                this._form_condition._extra._addFieldComboBox(__fieldList);
                //this._form_condition._title = this._screenName;
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._processClick)
            {
                this._dataTable = null; // จะได้ load data ใหม่
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
