using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._sheet
{
    public partial class _report : UserControl
    {
        DataSet _getAccount;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __accountObject;
        SMLReport._report._objectListType __jounalListObject;
        //
        _condition _conditionScreen = new _condition();
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        string _conditionDocBegin;
        string _conditionDocEnd;
        bool _conditionTotalByDate;
        bool _conditionAllAccount;
        string _conditionAccountBegin;
        string _conditionAccountEnd;
        //
        DateTime _totalDate = new DateTime(1000, 1, 1);
        //
        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        //
        public _report()
        {
            InitializeComponent();
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            _showCondition();
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            _conditionAccountBegin = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._account_code_begin);
            _conditionAccountEnd = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._account_code_end);
            _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin));
            _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_end));
            _conditionDocBegin = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._doc_begin);
            _conditionDocEnd = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._doc_end);
            _conditionTotalByDate = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._total_end_date).Equals("1") ? true : false;
            _conditionAllAccount = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._select_all_account).Equals("1") ? true : false;
            _getAccount = null;

            this._build();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _view1__getDataObject()
        {
            int __accountCode = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._code);
            int __accountName1 = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._name_1);
            int __accountName2 = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._name_2);
            //
            int __jounalDocDate = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_date);
            int __jounalDocNo = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_doc_no);
            int __jounalDescription = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_description);
            int __jounalBook = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._book_code);
            int __jounalDebit = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._debit);
            int __jounalCredit = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._credit);
            int __jounalBalance = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._balance);
            //
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__accountObject._columnList[0];
            //this._view1._reportProgressBar.Value = 0;
            int __totalRecords = _getAccount.Tables[0].Rows.Count;
            for (int __accountRow = 0; __accountRow < __totalRecords; __accountRow++)
            {
                string __getAccountCode = _getAccount.Tables[0].Rows[__accountRow].ItemArray[0].ToString();
                string __getAccountName1 = _getAccount.Tables[0].Rows[__accountRow].ItemArray[1].ToString();
                string __getAccountName2 = _getAccount.Tables[0].Rows[__accountRow].ItemArray[2].ToString();

                if (this._conditionScreen._branchShowCheckbox.Checked == true)
                {
                    for (int __branchRow = 0; __branchRow < this._conditionScreen._selectBranchControl._gridBranch._rowData.Count; __branchRow++)
                    {
                        if (this._conditionScreen._selectBranchControl._gridBranch._cellGet(__branchRow, 0).ToString().Equals("1"))
                        {
                            string __branchCode = this._conditionScreen._selectBranchControl._gridBranch._cellGet(__branchRow, _g.d.erp_branch_list._code).ToString();
                            string __branchName = this._conditionScreen._selectBranchControl._gridBranch._cellGet(__branchRow, _g.d.erp_branch_list._name_1).ToString();

                            this._view1._progessBarValue = (__accountRow + 1) * 100 / __totalRecords;
                            this._view1._processMessage = __getAccountCode + "/" + __getAccountName1 + "/" + __getAccountName2;

                            string __branchWhere = _g.d.gl_journal_detail._branch_code + "= \'" + __branchCode + "\'";
                            //
                            // คำนวณ และพิมพ์รายละเอียด
                            SMLProcess._glProcess __process = new SMLProcess._glProcess();
                            ArrayList __result = __process._glViewJounalDetail(__getAccountCode, _conditionDateBegin, _conditionDateEnd, _conditionTotalByDate, 0, __branchWhere);
                            bool __foundDetail = false;
                            if (_conditionAllAccount == false)
                            {
                                // ในกรณีต้องการเฉพาะบัญชีที่มีรายการ
                                for (int __row = 0; __row < __result.Count; __row++)
                                {
                                    SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)__result[__row];
                                    if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.Balance && __getRow._balance != 0)
                                    {
                                        __foundDetail = true;
                                        break;
                                    }
                                    if (__getRow._lineType != SMLProcess._glViewJounalDetailListLineType.Balance && __getRow._lineType != SMLProcess._glViewJounalDetailListLineType.SubTotal)
                                    {
                                        __foundDetail = true;
                                        break;
                                    }
                                }
                            }
                            if (_conditionAllAccount == true || __foundDetail == true)
                            {
                                // ชื่อบัญชี
                                SMLReport._report._objectListType __accountDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__accountObject, __accountDataObject);
                                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                _view1._addDataColumn(__accountObject, __accountDataObject, __accountCode, __getAccountCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__accountObject, __accountDataObject, __accountName1, __getAccountName1, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__accountObject, __accountDataObject, __accountName2, __getAccountName2, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                                // สาขา
                                SMLReport._report._objectListType __accountDataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__accountObject, __accountDataObject2);
                                _view1._addDataColumn(__accountObject, __accountDataObject2, __accountCode, __branchCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__accountObject, __accountDataObject2, __accountName1, __branchName, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);


                                // รายละเอียด
                                for (int __row = 0; __row < __result.Count; __row++)
                                {
                                    SMLReport._report._objectListType __journalDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__jounalListObject, __journalDataObject);
                                    //
                                    SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)__result[__row];
                                    if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.Balance)
                                    {
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        /*                        int __newRow = this._listGrid._addRow();
                                                                this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                                                                this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance, __getRow._balance, false);*/
                                    }
                                    else
                                        if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal)
                                    {
                                        __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    }
                                    else
                                    {
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, MyLib._myGlobal._convertDateToString(__getRow._date, false, true), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, __getRow._number, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, __getRow._book, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //this._view1._reportProgressBar.Value = (__accountRow + 1) * 100 / __totalRecords;
                    //this._view1._reportStatus.Text = __getAccountCode + "/" + __getAccountName1 + "/" + __getAccountName2;
                    //this._view1._reportProgressBar.Invalidate();
                    //this._view1._reportStatus.Invalidate();
                    //this._view1._reportStatusStrip.Refresh();
                    this._view1._progessBarValue = (__accountRow + 1) * 100 / __totalRecords;
                    this._view1._processMessage = __getAccountCode + "/" + __getAccountName1 + "/" + __getAccountName2;

                    //
                    // คำนวณ และพิมพ์รายละเอียด
                    SMLProcess._glProcess __process = new SMLProcess._glProcess();
                    ArrayList __result = __process._glViewJounalDetail(__getAccountCode, _conditionDateBegin, _conditionDateEnd, _conditionTotalByDate, 0);
                    bool __foundDetail = false;
                    if (_conditionAllAccount == false)
                    {
                        // ในกรณีต้องการเฉพาะบัญชีที่มีรายการ
                        for (int __row = 0; __row < __result.Count; __row++)
                        {
                            SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)__result[__row];
                            if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.Balance && __getRow._balance != 0)
                            {
                                __foundDetail = true;
                                break;
                            }
                            if (__getRow._lineType != SMLProcess._glViewJounalDetailListLineType.Balance && __getRow._lineType != SMLProcess._glViewJounalDetailListLineType.SubTotal)
                            {
                                __foundDetail = true;
                                break;
                            }
                        }
                    }
                    if (_conditionAllAccount == true || __foundDetail == true)
                    {
                        // ชื่อบัญชี
                        SMLReport._report._objectListType __accountDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(__accountObject, __accountDataObject);
                        Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __accountCode, __getAccountCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __accountName1, __getAccountName1, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __accountName2, __getAccountName2, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        // รายละเอียด
                        for (int __row = 0; __row < __result.Count; __row++)
                        {
                            SMLReport._report._objectListType __journalDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__jounalListObject, __journalDataObject);
                            //
                            SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)__result[__row];
                            if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.Balance)
                            {
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                /*                        int __newRow = this._listGrid._addRow();
                                                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                                                        this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance, __getRow._balance, false);*/
                            }
                            else
                                if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal)
                            {
                                __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            }
                            else
                            {
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, MyLib._myGlobal._convertDateToString(__getRow._date, false, true), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, __getRow._number, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, __getRow._book, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            }
                        }
                    }

                }
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1.__excelFlieName = "บัญชีแยกประเภท จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "บัญชีแยกประเภท จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false), SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
            {
                __accountObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                _view1._addColumn(__accountObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._code, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code, _g.d.gl_chart_of_account._code, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._name_1, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._name_1, _g.d.gl_chart_of_account._name_1, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._name_2, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._name_2, _g.d.gl_chart_of_account._name_2, SMLReport._report._cellAlign.Left);

                //
                __jounalListObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.Bottom);
                _view1._addColumn(__jounalListObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_date, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_date, _g.d.gl_list_view._journal_date, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__jounalListObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_doc_no, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_doc_no, _g.d.gl_list_view._journal_doc_no, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__jounalListObject, 27, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_description, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_description, _g.d.gl_list_view._journal_description, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__jounalListObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._book_code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._book_code, _g.d.gl_list_view._book_code, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._debit, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Right);
                _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._credit, _g.d.gl_list_view._credit, SMLReport._report._cellAlign.Right);
                _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._balance, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance, _g.d.gl_list_view._balance, SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }

        bool _view1__loadData()
        {
            _totalDate = new DateTime(1000, 1, 1);
            //
            if (_getAccount == null)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    //
                    string __whereStr = " where ";
                    string __andStr = " and ";
                    StringBuilder __whereAccount = new StringBuilder();
                    //
                    __whereAccount.Append(__whereStr);
                    __whereAccount.Append(_g.d.gl_chart_of_account._status + "=0");

                    if (this._conditionAccountBegin.Length != 0)
                    {
                        if (__whereAccount.Length == 0)
                        {
                            __whereAccount.Append(__whereStr);
                        }
                        else
                        {
                            __whereAccount.Append(__andStr);
                        }
                        __whereAccount.Append(_g.d.gl_chart_of_account._code + ">=\'" + this._conditionAccountBegin + "\'");
                    }
                    if (this._conditionAccountEnd.Length != 0)
                    {
                        if (__whereAccount.Length == 0)
                        {
                            __whereAccount.Append(__whereStr);
                        }
                        else
                        {
                            __whereAccount.Append(__andStr);
                        }
                        __whereAccount.Append(_g.d.gl_chart_of_account._code + "<=\'" + this._conditionAccountEnd + "\'");
                    }
                    //
                    string __accountOrderBy = _g.d.gl_chart_of_account._code;
                    string __query = "select " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._name_2 +
                        " from " + _g.d.gl_chart_of_account._table + __whereAccount.ToString() + " order by " + __accountOrderBy;
                    _getAccount = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                }
                catch
                {
                    this.Cursor = Cursors.Default;
                    return false;
                }
            }
            this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._build();
        }

        void _build()
        {
            if (this._conditionDateBegin.Year == 1000 || this._conditionDateEnd.Year == 1000)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ต้องกำหนดวันที่ก่อน"));
            }
            else
            {
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
