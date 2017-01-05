using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport._report_chq
{
    public class _chqCreditCardList : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private SMLERPReportTool._conditionScreen _form_condition;
        private SMLERPReportTool._reportEnum _mode;

        public _chqCreditCardList(string screenName, SMLERPReportTool._reportEnum mode)
        {
            this._screenName = screenName;
            this._mode = mode;
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _report__init()
        {
            this._report._level = this._reportInitData(null, true, true);
            this._report._resourceTable = ""; // แบบกำหนดเอง
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (this._dataTable == null)
            {
                return null;
            }
            return this._dataTable.Select();
        }

        SMLReport._generateLevelClass _reportInitData(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            string __chqDateFieldName = "";
            string __chqNumberFieldName = "";
            switch (this._mode)
            {
                case SMLERPReportTool._reportEnum.เช็ค_รายงานเช็ครับ_ตามวันที่รับเช็ค:
                    __chqDateFieldName = _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._date_receive;
                    __chqNumberFieldName = _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number;
                    break;
                case SMLERPReportTool._reportEnum.เช็ค_รายงานเช็คจ่าย_ตามวันที่จ่ายเช็ค:
                    __chqDateFieldName = _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._date_pay;
                    __chqNumberFieldName = _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number;
                    break;
                case SMLERPReportTool._reportEnum.บัตรเครดิต_รายงานบัตรเครดิต_ตามวันที่รับ:
                    __chqDateFieldName = _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._credit_card_date;
                    __chqNumberFieldName = _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._credit_card_number;
                    break;
            }
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_get_date, __chqDateFieldName, 8, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            if (this._mode == SMLERPReportTool._reportEnum.เช็ค_รายงานเช็คจ่าย_ตามวันที่จ่ายเช็ค ||
                this._mode == SMLERPReportTool._reportEnum.เช็ค_รายงานเช็ครับ_ตามวันที่รับเช็ค)
            {
                __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._bank_code, null, 6, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._bank_branch, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            }
            if (this._mode == SMLERPReportTool._reportEnum.เช็ค_รายงานเช็คจ่าย_ตามวันที่จ่ายเช็ค)
            {
                __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._pass_book_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            }
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number, __chqNumberFieldName, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            if (this._mode == SMLERPReportTool._reportEnum.บัตรเครดิต_รายงานบัตรเครดิต_ตามวันที่รับ)
            {
                __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._credit_card_type, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            }
            else
            {
                __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_due_date, null, 8, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
            }
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._ap_ar_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._ap_ar_name, null, 24, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            return this._report._addLevel("temp", levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    int __chqType = 0;
                    switch (this._mode)
                    {
                        case SMLERPReportTool._reportEnum.เช็ค_รายงานเช็ครับ_ตามวันที่รับเช็ค:
                            __chqType = 1;
                            break;
                        case SMLERPReportTool._reportEnum.เช็ค_รายงานเช็คจ่าย_ตามวันที่จ่ายเช็ค:
                            __chqType = 2;
                            break;
                        case SMLERPReportTool._reportEnum.บัตรเครดิต_รายงานบัตรเครดิต_ตามวันที่รับ:
                            __chqType = 3;
                            break;
                    }
                    string __startDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._from_date).Replace("'", "");
                    string __endDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._to_date).Replace("'", "");
                    string __getWhere = this._form_condition._grid._createWhere(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_get_date);
                    StringBuilder __whereStr = new StringBuilder(this._form_condition._extra._getWhere(__getWhere).Trim());
                    __whereStr.Append((__whereStr.Length == 0) ? " where " : " and ");
                    __whereStr.Append("(");
                    __whereStr.Append(_g.d.cb_chq_list._chq_get_date + " between \'" + __startDate + "\' and \'" + __endDate + "\'");
                    __whereStr.Append(" and " + _g.d.cb_chq_list._chq_type + "=" + __chqType.ToString());
                    __whereStr.Append("  )");
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "select " + this._report._level.__fieldList(true) + " from " + _g.d.cb_chq_list._table + __whereStr.ToString() + this._form_condition._extra._getOrderBy();
                    string __query1 = "case when ap_ar_type=1 then ((select name_1 from ar_customer where code = cb_chq_list.ap_ar_code)) else ((select name_1 from ap_supplier where code = cb_chq_list.ap_ar_code)) end";
                    __query = __query.Replace("," + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._ap_ar_name, "," + __query1);
                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query).Tables[0];
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
