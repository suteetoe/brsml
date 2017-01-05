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
    public partial class _other_debt : UserControl
    {
        private int _report_type;
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private _condition_form _form_condition;
        private DataTable _conditionFromTo;
        private string _process_date;
        private string _from_date;
        private string _to_date;
        private string _from_docno;
        private string _to_docno;
        private string _from_project;
        private string _to_project;
        private string _from_job;
        private string _to_job;
        private string _from_department;
        private string _to_department;
        private string _from_group;
        private string _to_group;
        private string _from_amount;
        private string _to_amount;
        private string[] _order_by_column = { _g.d.ic_trans._table+"."+_g.d.ic_trans._doc_date,
                                             _g.d.ic_trans._table+"."+_g.d.ic_trans._doc_no,
                                             _g.d.ic_trans._table+"."+_g.d.ic_trans._cust_code,
                                             _g.d.ic_trans._table+"."+_g.d.ic_trans._remark,
                                             _g.d.ic_trans._table+"."+_g.d.ic_trans._doc_group,
                                             _g.d.ic_trans._table+"."+_g.d.ic_trans._total_amount,
                                             _g.d.ic_trans._table+"."+_g.d.ic_trans._balance_amount};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="__report_type">1=ตั้งหนี้ 2=ยกเลิกตั้งหนี้ 3=เพิ่มหนี้ 4=ยกเลิกเพิ่มหนี้ 5=ลดหนี้ 6=ยกเลิกลดหนี้</param>
        public _other_debt(int __report_type)
        {
            InitializeComponent();

            this._report_type = __report_type;
            this._view1._pageSetupDialog.PageSettings.Landscape = true;
            this._view1._buttonExample.Enabled = false;
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            this._view1._fontHeader1 = new Font("Angsana New", 18, FontStyle.Bold);
            this._view1._fontHeader2 = new Font("Angsana New", 14, FontStyle.Bold);
            this._view1._fontStandard = new Font("Angsana New", 12, FontStyle.Regular);

            this._showCondition();
        }

        void _view1__loadDataByThread()
        {
            if (this._dataTable == null)
            {
                try
                {
                    StringBuilder __getWhereScreen = new StringBuilder();
                    StringBuilder __getWhereGrid = new StringBuilder();
                    string __getUserWhere1 = "";
                    string __getUserWhere2 = "";
                    string __orderBy = "";
                    string __custNameQuery = "";
                    string __docGroupQuery = "";
                    string __query = "";
                    if (this._to_docno.Trim().Length == 0) this._to_docno = this._from_docno;
                    if (this._to_project.Trim().Length == 0) this._to_project = this._from_project;
                    if (this._to_job.Trim().Length == 0) this._to_job = this._from_job;
                    if (this._to_department.Trim().Length == 0) this._to_department = this._from_department;
                    if (this._to_group.Trim().Length == 0) this._to_group = this._from_group;
                    if (this._to_amount.Trim().Length == 0) this._to_amount = this._from_amount;

                    //where screen===========================================================================================
                    __getWhereScreen.Append(String.Format("({0}={1}) and ({2} between \'{3}\' and \'{4}\')",
                        _g.d.ic_trans._trans_flag,
                        this._getTransFlag(this._report_type),
                        _g.d.ic_trans._doc_date,
                        this._from_date,
                        this._to_date));
                    if (this._from_docno.Trim().Length > 0) __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", _g.d.ic_trans._doc_no, this._from_docno, this._to_docno));
                    //if (this._from_project.Trim().Length > 0) __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", _g.d.ap_ar_trans._project_code, this._from_project, this._to_project));
                    //if (this._from_job.Trim().Length > 0) __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", _g.d.ap_ar_trans._job_code, this._from_job, this._to_job));
                    //if (this._from_department.Trim().Length > 0) __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", _g.d.ap_ar_trans._department_code, this._from_department, this._to_department));
                    //if (this._from_group.Trim().Length > 0) __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", _g.d.ap_ar_trans._doc_group, this._from_group, this._to_group));
                    //if (this._from_amount.Trim().Length > 0) __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", _g.d.ap_ar_trans._total_debt_balance, this._from_amount, this._to_amount));

                    //where grid=============================================================================================
                    if (this._conditionFromTo != null && this._conditionFromTo.Rows.Count > 0)
                    {
                        for (int __row = 0; __row < this._conditionFromTo.Rows.Count; __row++)
                        {
                            __getWhereGrid.Append(string.Format("({0} between \'{1}\' and \'{2}\')",
                                _g.d.ic_trans._cust_code,
                                this._conditionFromTo.Rows[__row][0].ToString(),
                                this._conditionFromTo.Rows[__row][1].ToString()));
                            if (__row != this._conditionFromTo.Rows.Count - 1)
                            {
                                __getWhereGrid.Append(" or ");
                            }
                        }
                        if (__getWhereGrid.Length > 0) __getWhereGrid.Insert(0, " and ");
                    }

                    //where user control=====================================================================================
                    __getUserWhere1 = this._form_condition._whereUserControl1._getWhere1("");
                    __getUserWhere1 = __getUserWhere1.Replace("\"", "");
                    __getUserWhere1 = __getUserWhere1.Replace("where", "and");
                    __getUserWhere2 = this._form_condition._whereUserControl1._getWhere2();
                    __getUserWhere2 = __getUserWhere2.Replace("\"", "");
                    __getUserWhere2 = __getUserWhere2.Replace("where", "and");
                    //order by===============================================================================================
                    __orderBy = this._form_condition._whereUserControl1._getOrderBy();
                    __orderBy = __orderBy.Replace("\"", "");
                    //SubQuery
                    __custNameQuery = String.Format("(select {2} from {0} where {1}={3}.{4}) as {5}",
                        _g.d.ap_supplier._table,        //{0}
                        _g.d.ap_supplier._code,         //{1}
                        _g.d.ap_supplier._name_1,       //{2}
                        _g.d.ic_trans._table,           //{3}
                        _g.d.ic_trans._cust_code,       //{4}
                        "cust_name");                   //{5}
                    __docGroupQuery = String.Format("(select {2} from {0} where {1}={3}.{4}) as {4}",
                        _g.d.erp_doc_group._table,      //{0}
                        _g.d.erp_doc_group._code,       //{1}
                        _g.d.erp_doc_group._name_1,     //{2}
                        _g.d.ic_trans._table,           //{3}
                        _g.d.ic_trans._doc_group);      //{4}
                    //query==================================================================================================
                    __query = String.Format("select {0},{1},{2},{3},{4},{5},coalesce({6},0) as {6},coalesce({7},0) as {7} from {8} where {9}{10} {11} {12} {13}",
                        _g.d.ic_trans._doc_date,                                                            //{0}
                        _g.d.ic_trans._doc_no,                                                              //{1}
                        _g.d.ic_trans._cust_code,                                                           //{2}
                        __custNameQuery,                                                                    //{3}
                        _g.d.ic_trans._remark,                                                              //{4}
                        __docGroupQuery,                                                                    //{5}
                        _g.d.ic_trans._total_amount,                                                        //{6}
                        _g.d.ic_trans._balance_amount,                                                      //{7}
                        _g.d.ic_trans._table,                                                               //{8}
                        __getWhereScreen,                                                                   //{9}
                        __getWhereGrid,                                                                     //{10}
                        __getUserWhere1,                                                                    //{11}
                        __getUserWhere2,                                                                    //{12}
                        __orderBy);                                                                         //{13}
                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            this._view1._loadDataByThreadSuccess = true;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "Title\t: " + this._getReportName(this._report_type), SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Printed By\t: " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Printed Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Description\t: ", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_trans._doc_date, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, _g.d.ic_trans._doc_date, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_trans._doc_no, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, _g.d.ic_trans._doc_no, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_trans._ap_code, _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code, _g.d.ic_trans._ap_code, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_trans._remark, _g.d.ic_trans._table + "." + _g.d.ic_trans._remark, _g.d.ic_trans._remark, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_trans._doc_group, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_group, _g.d.ic_trans._doc_group, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_trans._total_amount, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, _g.d.ic_trans._total_amount, SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_trans._balance_amount, _g.d.ic_trans._table + "." + _g.d.ic_trans._balance_amount, _g.d.ic_trans._balance_amount, SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = null;
            Font __totalFont = new Font("Angsana New", 12, FontStyle.Bold);
            decimal __value = 0;
            string __dateTimeString = "";
            SMLReport._report._objectListType __dataObject = null;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");

            if (this._dataTable == null) return;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRows[__row].ItemArray[0].ToString()).ToShortDateString();
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dateTimeString, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRows[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[6].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[6].ToString());
                this._view1._addDataColumn(__detailObject, __dataObject, 6, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[7].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[7].ToString());
                this._view1._addDataColumn(__detailObject, __dataObject, 7, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
            }
            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            this._view1._createEmtryColumn(__detailObject, __dataObject);
            this._view1._addDataColumn(__detailObject, __dataObject, 0, "รวม", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
            this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRows.Length.ToString() + " รายการ", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (this._form_condition._condition_screen1._checkEmtryField().Length != 0)
            {
                MessageBox.Show(MyLib._myResource._findResource(_g.d.resource_report._table + "." + _g.d.resource_report._must_set_condition, _g.d.resource_report._table + "." + _g.d.resource_report._must_set_condition)._str, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this._dataTable = null; // จะได้ load data ใหม่
            this._view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this._view1._buildReport(SMLReport._report._reportType.Standard);
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new _condition_form(SMLERPARAPInfo._apArConditionEnum.debtor, this._getPage(this._report_type), this._getReportName(this._report_type));
                this._form_condition._whereUserControl1._tableName = _g.d.ar_customer._table;
                this._form_condition._whereUserControl1._addFieldComboBox(this._order_by_column);
            }
            this._form_condition._process = false;
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                this._dataTable = null; // จะได้ load data ใหม่

                this._process_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._process_date)));

                this._from_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_docdate)));

                this._to_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_docdate)));

                this._from_docno = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_docno);

                this._to_docno = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_docno);

                this._from_project = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_project);

                this._to_project = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_project);

                this._from_job = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_job);

                this._to_job = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_job);

                this._from_department = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_department);

                this._to_department = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_department);

                this._from_group = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_group);

                this._to_group = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_group);

                this._from_amount = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_amount);

                this._to_amount = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_amount);

                this._conditionFromTo = this._form_condition._condition_grid1._getCondition();

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }

        private string _getPage(int __report_type)
        {
            switch (__report_type)
            {
                case 1: return SMLERPARAPInfo._apArConditionEnum.other_debt_setup.ToString();
                case 2: return SMLERPARAPInfo._apArConditionEnum.other_debt_setup_cancel.ToString();
                case 3: return SMLERPARAPInfo._apArConditionEnum.other_debt_increase.ToString();
                case 4: return SMLERPARAPInfo._apArConditionEnum.other_debt_increase_cancel.ToString();
                case 5: return SMLERPARAPInfo._apArConditionEnum.other_debt_decrease.ToString();
                case 6: return SMLERPARAPInfo._apArConditionEnum.other_debt_decrease_cancel.ToString();
            }
            return "0";
        }

        private string _getTransFlag(int __report_type)
        {
            switch (__report_type)
            {
                case 1: return "99";
                case 2: return "100";
                case 3: return "101";
                case 4: return "102";
                case 5: return "103";
                case 6: return "104";
            }
            return "0";
        }

        private string _getReportName(int __report_type)
        {
            switch (__report_type)
            {
                case 1: return "รายงานเอกสารตั้งหนี้อื่นๆ";
                case 2: return "รายงานยกเลิกเอกสารตั้งหนี้อื่นๆ";
                case 3: return "รายงานเอกสารเพิ่มหนี้อื่นๆ";
                case 4: return "รายงานยกเลิกเอกสารเพิ่มหนี้อื่นๆ";
                case 5: return "รายงานเอกสารลดหนี้อื่นๆ";
                case 6: return "รายงานยกเลิกเอกสารลดหนี้อื่นๆ";
            }
            return "";
        }
    }
}
