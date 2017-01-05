using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._trialBalance
{
    public partial class _report : UserControl
    {
        ArrayList __getFromProcess;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __accountObject;
        //
        _condition _conditionScreen = new _condition();
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        Boolean _conditionAllAccount = false;
        Boolean _conditionAccountBalance = false;
        Boolean _conditionShowSum = false;

        //
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
            //
            _showCondition();
        }

        void _showCondition()
        {
            string __dateBegin = _conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin);
            if (__dateBegin == "")
            {
                this._conditionScreen._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_begin, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                this._conditionScreen._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_end, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1));

            }

            _conditionScreen.ShowDialog();
            _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin));
            _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_end));
            _conditionAllAccount = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._select_all_account).Equals("1") ? true : false;
            _conditionAccountBalance = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._select_account_balance).Equals("1") ? false : true;
            _conditionShowSum = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._total_end_date).Equals("1") ? true : false;
            __getFromProcess = null;

            this._view1._buildReport(SMLReport._report._reportType.Standard);

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
            int __accountCode = _view1._findColumn(__accountObject, _g.d.gl_list_view._account_code);
            int __accountName1 = _view1._findColumn(__accountObject, _g.d.gl_list_view._account_name);
            int __prevDebit = _view1._findColumn(__accountObject, _g.d.gl_list_view._prev_debit);
            int __prevCredit = _view1._findColumn(__accountObject, _g.d.gl_list_view._prev_credit);
            int __debit = _view1._findColumn(__accountObject, _g.d.gl_list_view._debit);
            int __credit = _view1._findColumn(__accountObject, _g.d.gl_list_view._credit);
            int __balanceDebit = _view1._findColumn(__accountObject, _g.d.gl_list_view._balance_debit);
            int __balanceCredit = _view1._findColumn(__accountObject, _g.d.gl_list_view._balance_credit);
            //
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__accountObject._columnList[0];
            //this._view1._reportProgressBar.Value = 0;
            int __totalRecords = __getFromProcess.Count;
            for (int __accountRow = 0; __accountRow < __totalRecords; __accountRow++)
            {
                SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)__getFromProcess[__accountRow];
                if (__getRow._show)
                {
                    if (this._conditionShowSum || __getRow._accountStatus == 0 || __getRow._accountLevel == 0)
                    {
                        SMLReport._report._objectListType __accountDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(__accountObject, __accountDataObject);
                        Font __newFont = new Font(__getColumn._fontData, (__getRow._accountStatus == 0) ? FontStyle.Regular : FontStyle.Bold);
                        int __accountLevel = (__getRow._accountLevel == 0) ? 0 : ((__getRow._accountLevel - 1) * 2);
                        if (__accountLevel < 0 || this._conditionShowSum == false)
                        {
                            __accountLevel = 0;
                        }
                        SMLReport._report._columnBorder __newBorder = (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal) ? SMLReport._report._columnBorder.TopBottom : SMLReport._report._columnBorder.None;
                        _view1._addDataColumn(__accountObject, __accountDataObject, __accountCode, __getRow._accountCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __accountName1, __getRow._accountName, __newFont, SMLReport._report._cellAlign.Default, __accountLevel, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __prevDebit, MyLib._myUtil._moneyFormat(__getRow._prevDebit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __prevCredit, MyLib._myUtil._moneyFormat(__getRow._prevCredit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __debit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __credit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __balanceDebit, MyLib._myUtil._moneyFormat(__getRow._balanceDebit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __balanceCredit, MyLib._myUtil._moneyFormat(__getRow._balanceCredit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
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
                _view1.__excelFlieName = "งบทดลอง ประจำวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่  : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "งบทดลอง ประจำวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่  : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false), SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
            {
                // Column บน
                SMLReport._report._objectListType __columnHeaderObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                _view1._addColumn(__columnHeaderObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, null, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__columnHeaderObject, 30, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, null, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__columnHeaderObject, 20, true, SMLReport._report._columnBorder.LeftBottom, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, "ยอดยกมา", _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__columnHeaderObject, 20, true, SMLReport._report._columnBorder.LeftBottom, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, "ยอดประจำงวด", _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__columnHeaderObject, 20, true, SMLReport._report._columnBorder.LeftRightBottom, SMLReport._report._columnBorder.LeftRight, _g.d.gl_list_view._account_code, "ยอดสะสม", _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                // Column ข้อมูล
                __accountObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_code, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, 30, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_name, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_name, _g.d.gl_list_view._account_name, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._prev_debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._prev_debit, _g.d.gl_list_view._prev_debit, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._prev_credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._prev_credit, _g.d.gl_list_view._prev_credit, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._debit, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._credit, _g.d.gl_list_view._credit, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._balance_debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance_debit, _g.d.gl_list_view._balance_debit, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.LeftRight, SMLReport._report._columnBorder.LeftRight, _g.d.gl_list_view._balance_credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance_credit, _g.d.gl_list_view._balance_credit, SMLReport._report._cellAlign.Center);
                return true;
            }
            return false;
        }

        bool _view1__loadData()
        {
            //
            if (__getFromProcess == null)
            {
                this.Cursor = Cursors.WaitCursor;
                SMLProcess._glProcess __process = new SMLProcess._glProcess();
                MyLib._myTreeViewDragDrop _chartOfAccountTreeView = new MyLib._myTreeViewDragDrop();
                _chartOfAccountTreeView = __process._getChartOfAccountTreeView(_chartOfAccountTreeView);
                __getFromProcess = __process._glViewTrialBalance(_conditionDateBegin, _conditionDateEnd, _chartOfAccountTreeView, this._conditionAllAccount, this._conditionAccountBalance, 0);
            }
            this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            try
            {
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
            catch
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถทำรายการได้"));
            }
            this.Cursor = Cursors.Default;
        }
    }
}
