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
    public partial class _ageing : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private _condition_aging _form_condition;
        private SMLERPARAPInfo._apArConditionEnum _mode;

        int __term_1_begin;
        int __term_1_end;
        int __term_2_begin;
        int __term_2_end;
        int __term_3_begin;
        int __term_3_end;
        int __term_4_begin;
        int __term_4_end;

        public _ageing(SMLERPARAPInfo._apArConditionEnum mode,string screenName)
        {
            this._screenName = screenName;
            this._mode = mode;
            this._form_condition = new _condition_aging(mode);
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report.Disposed += new EventHandler(_report_Disposed);
            this._report._viewControl._columnResource += new SMLReport._report.ColumnResourceEventHandler(_viewControl__columnResource);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        SMLReport._report.ColumnResourceStruct _viewControl__columnResource(string resourceName)
        {
            SMLReport._report.ColumnResourceStruct __result = new SMLReport._report.ColumnResourceStruct();
            __result._resourceName = resourceName;
            __result._findResource = false;
            string __word2 = this._getColumnName(_g.d.ap_ar_resource._day_due);
            string __format = "{0}-{1} {2}";
            if (resourceName.Equals(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._term_1)) __result._resourceName = string.Format(__format, this.__term_1_begin, this.__term_1_end, __word2);
            else
                if (resourceName.Equals(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._term_2)) __result._resourceName = string.Format(__format, this.__term_2_begin, this.__term_2_end, __word2);
                else
                    if (resourceName.Equals(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._term_3)) __result._resourceName = string.Format(__format, this.__term_3_begin, this.__term_3_end, __word2);
                    else
                        if (resourceName.Equals(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._term_4)) __result._resourceName = string.Format(__format, this.__term_4_begin, this.__term_4_end, __word2);
                        else
                            if (resourceName.Equals(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._term_5)) __result._resourceName = string.Format("{0} {1}", _getColumnName(_g.d.ap_ar_resource._term_6), __term_4_end);
                            else
                                __result._findResource = true;
            return __result;
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
            return this._dataTable.Select();
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        SMLReport._generateLevelClass _reportInitProduct(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_name, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            int __columnWidth = 8;
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_balance, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._out_due, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_0, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_1, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_2, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_3, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_4, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_5, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            return this._report._addLevel("temp", levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._report._level = this._reportInitProduct(null, true, true);
            this._report._resourceTable = _g.d.ap_ar_resource._table;
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                int __dueDateSelect = MyLib._myGlobal._intPhase(this._form_condition._screenTop._getDataStr(_g.d.ap_ar_resource._due_date_select));
                string __custCodeBegin = this._form_condition._screenTop._getDataStr((SMLERPARAPInfo._apAr._apArCheck(this._mode) == SMLERPARAPInfo._apArEnum.ลูกหนี้) ? _g.d.ap_ar_resource._ar_code_begin : _g.d.ap_ar_resource._ap_code_begin);
                string __custCodeEnd = this._form_condition._screenTop._getDataStr((SMLERPARAPInfo._apAr._apArCheck(this._mode) == SMLERPARAPInfo._apArEnum.ลูกหนี้) ? _g.d.ap_ar_resource._ar_code_end : _g.d.ap_ar_resource._ap_code_end);
                DateTime __dateEnd = MyLib._myGlobal._convertDate(this._form_condition._screenTop._getDataStr(_g.d.ap_ar_resource._date_end));
                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                this._dataTable = __process._arAgeing(this._mode, __dueDateSelect, __custCodeBegin, __custCodeEnd, this.__term_1_begin, this.__term_1_end, this.__term_2_begin, this.__term_2_end, this.__term_3_begin, this.__term_3_end, this.__term_4_begin, this.__term_4_end, __dateEnd, "");
            }
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new _condition_aging(this._mode);
                this._form_condition._title = this._screenName;
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                this._dataTable = null; // จะได้ load data ใหม่
                this.__term_1_begin = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_1_begin);
                this.__term_1_end = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_1_end);
                this.__term_2_begin = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_2_begin);
                this.__term_2_end = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_2_end);
                this.__term_3_begin = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_3_begin);
                this.__term_3_end = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_3_end);
                this.__term_4_begin = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_4_begin);
                this.__term_4_end = (int)this._form_condition._screenTop._getDataNumber(_g.d.ap_ar_resource._term_4_end);
                //
                string __endDate = this._form_condition._screenTop._getDataStr(_g.d.ap_ar_resource._date_end);
                this._report._conditionText = MyLib._myGlobal._resource("ยอดถึงวันที่") + " : " + __endDate + " ";
                //
                this._report._build();
            }
        }
   }
}