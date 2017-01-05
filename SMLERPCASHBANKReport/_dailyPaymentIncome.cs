using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport
{
    public class _dailyPaymentIncome : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private SMLERPReportTool._conditionScreen _form_condition;
        private SMLERPReportTool._reportEnum _mode;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLReport._generateLevelClass _level2;

        public _dailyPaymentIncome(string screenName, SMLERPReportTool._reportEnum mode)
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
            if (sender._levelName == this._levelNameRoot)
            {

            }
            else  if (columnNumber == sender._findColumnName(_g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag))
            {
                sender._columnList[columnNumber]._dataStr = (sender._columnList[columnNumber]._dataStr.Length == 0) ? "" : _g.g._transFlagGlobal._transName((MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr)));
            }
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._ap_ar_name, null, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_net_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._cash_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_income_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._petty_cash_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._deposit_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._advance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_tax_at_pay, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._chq_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._tranfer_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._card_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._doc_date, null, 75, SMLReport._report._cellType.DateTime, 0, FontStyle.Bold));

            // toe
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_net_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._cash_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_income_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._petty_cash_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._deposit_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._advance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_tax_at_pay, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._chq_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._tranfer_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            //columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._card_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
        }

        void _report__init()
        {
            this._report._level = this._reportInitRoot(null, true, true);
            this._level2 = this._reportInitDetail(this._report._level, true, 2, true);
            SMLReport._generateLevelClass __grandTotallevel = this._reportInitDetail(this._report._level, true, 2, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            // กำหนดว่ามี Column อะไรบ้าง
            this._reportInitDetailColumn(this._report._level.__grandTotal, 0);
            this._reportInitColumnValue(this._report._level.__grandTotal);
            //
            this._report._level._levelGrandTotal = __grandTotallevel;
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
                    string __fieldName = levelParent._columnList[0]._fieldName;
                    if (__fieldName.Length > 0)
                    {
                        string __where = __fieldName + "=\'" + source[__fieldName].ToString().Replace("\'", "\'\'") + "\'";
                        return (this._dataTableDetail == null || this._dataTableDetail.Rows.Count == 0) ? null : this._dataTableDetail.Select(__where);
                    }
                    else
                    {
                        return null;
                    }
                }
            return null;
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    int __payType = 0;
                    switch (this._mode)
                    {
                        case SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายงานการรับเงินประจำวัน:
                            __payType = 1;
                            break;
                        case SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายงานการจ่ายเงินประจำวัน:
                            __payType = 2;
                            break;
                    }
                    string __startDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._from_date).Replace("'", "");
                    string __endDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._to_date).Replace("'", "");
                    StringBuilder __whereStr = new StringBuilder(this._form_condition._extra._getWhere("").Trim());
                    __whereStr.Append((__whereStr.Length == 0) ? " where " : " and ");
                    __whereStr.Append("(");
                    __whereStr.Append(_g.d.cb_trans._doc_date + " between \'" + __startDate + "\' and \'" + __endDate + "\'");
                    __whereStr.Append(" and " + _g.d.cb_trans._pay_type + "=" + __payType.ToString());
                    __whereStr.Append(")");
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "select " + this._level2.__fieldList(true) + " from " + _g.d.cb_trans._table + __whereStr.ToString() + this._form_condition._extra._getOrderBy();
                    string __query1 = "case when trans_type=2 then ((select name_1 from ar_customer where code = cb_trans.ap_ar_code)) else ((select name_1 from ap_supplier where code = cb_trans.ap_ar_code)) end";
                    __query = __query.Replace("," + _g.d.cb_trans._table + "." + _g.d.cb_trans._ap_ar_name, "," + __query1);
                    this._dataTableDetail = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query).Tables[0];
                    this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTableDetail, _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_date);
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        private void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._report__init();
                this._form_condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);
                this._form_condition._extra._tableName = _g.d.ic_inventory._table;
                string[] __fieldList = this._report._level.__fieldList(false).Split(',');
                this._form_condition._extra._addFieldComboBox(__fieldList);
                //this._form_condition._title = this._screenName;
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._processClick)
            {
                this._dataTable = null; // จะได้ load data ใหม่
                //
                this._report._build();
            }
        }
    }
}
