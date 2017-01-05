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
    public partial class _absolute_status : UserControl
    {
        SMLReport._generate _report;
        String _screenName = "";
        DataTable _dataTable;
        //
        private _condition_form _form_condition;
        private SMLERPARAPInfo._apArConditionEnum _mode;

        public _absolute_status(SMLERPARAPInfo._apArConditionEnum mode ,string screenName)
        {
            InitializeComponent();

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

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            return this._dataTable.Select();
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        SMLReport._generateLevelClass _reportInit(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_name, null, 38, SMLReport._report._cellType.String, 0, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._balance_first, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._debit_1, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._debit_2, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._credit_1, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._credit_2, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._balance_end, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
            return this._report._addLevel("temp", levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            this._report._level = this._reportInit(null, true, true);
            this._report._resourceTable = _g.d.ap_ar_resource._table;
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                string __arCodeBegin = "";
                string __arCodeEnd = "";
                DateTime __dateBegin = MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_date));
                DateTime __dateEnd = MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_date));

                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                this._dataTable = __process._arStat(this._mode,(MyLib._myGrid) this._form_condition._condition_grid1,__arCodeBegin, __arCodeEnd, __dateBegin, __dateEnd);
            }
        }

        void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new _condition_form(this._mode, this._mode.ToString(), this._screenName);
                //this._form_condition._whereUserControl1._tableName = _g.d.ar_customer._table;
                //this._form_condition._whereUserControl1._addFieldComboBox(this._order_by_column);
            }
            this._form_condition._process = false;
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