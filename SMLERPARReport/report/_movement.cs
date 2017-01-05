using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPARAPReport.condition;

namespace SMLERPARAPReport.report
{
    public partial class _movement : UserControl
    {
        string __formatNumberAmount = _g.g._getFormatNumberStr(3);
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTableRoot;
        private DataTable _dataTable;
        private DataTable _dataTableDoc;
        private _condition_form _form_condition;
        string _levelNameCust = "cust";
        string _levelNameDoc = "doc";
        SMLERPARAPInfo._apArConditionEnum _mode;

        public _movement(SMLERPARAPInfo._apArConditionEnum mode, string screenName)
        {
            this._screenName = screenName;
            this._mode = mode;
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._updateDecimalValue += new SMLReport._generate.UpdateDecimalValueEventHandler(_report__updateDecimalValue);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report.Disposed += new EventHandler(_report_Disposed);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._objectPageBreak += new SMLReport._generate.ObjectPageBreakEventHandler(_report__objectPageBreak);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            int __creditDayColumnNumber = sender._findColumnName(_g.d.ap_ar_resource._credit_day);
            if (__creditDayColumnNumber != -1 && __creditDayColumnNumber == columnNumber && sender._columnList[__creditDayColumnNumber]._dataStr.Length > 0)
            {
                if (MyLib._myGlobal._intPhase(sender._columnList[__creditDayColumnNumber]._dataStr) == 0)
                {
                    // กรณีเครดิตเป็น 0
                    sender._columnList[__creditDayColumnNumber]._dataStr = "";
                }
            }
        }

        bool _report__objectPageBreak(SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Total)
            {
                string __newPage = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._new_page_by_ar_code);
                if (__newPage.Equals("1"))
                {
                    return true;
                }
            }
            return false;
        }

        void _report__updateDecimalValue(SMLReport._generateLevelClass sender, SMLReport._generateColumnStyle isTotal)
        {
            int __columnNumber = sender._findColumnName(_g.d.ap_ar_resource._ar_balance);
            switch (isTotal)
            {
                case SMLReport._generateColumnStyle.GrandTotal:
                case SMLReport._generateColumnStyle.Total:
                    sender._columnList[__columnNumber]._dataNumber = 0;
                    break;
            }
        }

        private string _getColumnName(string fieldName)
        {
            string __resourceFieldName = _g.d.ap_ar_resource._table + "." + fieldName;
            MyLib._myResourceType __getResource = MyLib._myResource._findResource(__resourceFieldName, __resourceFieldName);
            return __getResource._str;
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (level._levelName.Equals(this._levelNameCust))
            {
                return this._dataTable.Select();
            }
            else
                if (level._levelName.Equals(this._levelNameDoc))
                {
                    StringBuilder __where = new StringBuilder();
                    for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
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
                    return this._dataTableDoc.Select(__where.ToString());
                }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        SMLReport._generateLevelClass _reportInitCust(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_code, _g.d.ap_ar_resource._ap_code, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_name, _g.d.ap_ar_resource._ap_name, 70, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass("", "", 20, SMLReport._report._cellType.String, 0));
            return this._report._addLevel(this._levelNameCust, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDocColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            int __columnWidth = 8;
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_date, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._vat_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ref_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._credit_day, null, __columnWidth, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_type, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._debit_amount, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._credit_amount, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_balance, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
        }

        SMLReport._generateLevelClass _reportInitDoc(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDocColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDoc, levelParent, __columnList, sumTotal, autoWidth);
        }


        void _report__init()
        {
            this._report._resourceTable = _g.d.ap_ar_resource._table;
            this._report._level = this._reportInitCust(null, false, false);
            SMLReport._generateLevelClass __level2 = this._reportInitDoc(this._report._level, true, 2, true);
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                DateTime __dateBegin = MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_date));
                DateTime __dateEnd = MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_date));
                string __movementOnly = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._movement_only);
                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                this._dataTableRoot = __process._movement(this._mode, null, this._form_condition._condition_grid1, __dateBegin, __dateEnd, (__movementOnly.Equals("1")) ? true : false);
                this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTableRoot, _g.d.ap_ar_resource._ar_code + "," + _g.d.ap_ar_resource._ar_name);
                this._dataTableDoc = this._dataTableRoot;
            }
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new _condition_form(this._mode, this._mode.ToString(), this._screenName);
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                this._dataTable = null; // จะได้ load data ใหม่
                //
                string __beginDate = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_date);
                string __endDate = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_date);
                this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
                this._report._conditionText = _g.g._conditionGrid(this._form_condition._condition_grid1, this._report._conditionText);
                //
                this._report._build();
            }
        }
    }
}
