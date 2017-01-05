using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._trialBalanceBranch
{
    public partial class _report : UserControl
    {
        ArrayList _getFromProcess;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __accountObject;
        //
        _condition _conditionScreen = new _condition();
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        Boolean _conditionAllAccount = false;
        Boolean _conditionAccountBalance = false;
        Boolean _conditionByPeriod = false;
        List<_g.g._branchListStruct> _branchList;
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
            _view1._fontStandard = new Font("Angsana New", 4, FontStyle.Regular);
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
            _conditionByPeriod= this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._period).Equals("1") ? true : false;
            this._getFromProcess = null;

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
            int __debit = _view1._findColumn(__accountObject, _g.d.gl_list_view._debit);
            int __credit = _view1._findColumn(__accountObject, _g.d.gl_list_view._credit);
            //
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__accountObject._columnList[0];
            //this._view1._reportProgressBar.Value = 0;
            int __totalRecords = this._getFromProcess.Count;
            for (int __accountRow = 0; __accountRow < __totalRecords; __accountRow++)
            {
                SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)this._getFromProcess[__accountRow];
                if (__getRow._show)
                {
                    SMLReport._report._objectListType __accountDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(__accountObject, __accountDataObject);
                    Font __newFont = new Font(__getColumn._fontData, (__getRow._accountStatus == 0) ? FontStyle.Regular : FontStyle.Bold);
                    int __accountLevel = (__getRow._accountLevel == 0) ? 0 : ((__getRow._accountLevel - 1) * 2);
                    if (__accountLevel < 0)
                    {
                        __accountLevel = 0;
                    }
                    SMLReport._report._columnBorder __newBorder = (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal) ? SMLReport._report._columnBorder.TopBottom : SMLReport._report._columnBorder.None;
                    _view1._addDataColumn(__accountObject, __accountDataObject, __accountCode, __getRow._accountCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__accountObject, __accountDataObject, __accountName1, __getRow._accountName, __newFont, SMLReport._report._cellAlign.Default, __accountLevel, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__accountObject, __accountDataObject, __debit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                    _view1._addDataColumn(__accountObject, __accountDataObject, __credit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                    for (int __loop = 0; __loop < this._branchList.Count; __loop++)
                    {
                        _view1._addDataColumn(__accountObject, __accountDataObject, _view1._findColumn(__accountObject, _g.d.gl_list_view._debit + this._branchList[__loop]._code), MyLib._myUtil._moneyFormat(__getRow._branch[__loop]._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__accountObject, __accountDataObject, _view1._findColumn(__accountObject, _g.d.gl_list_view._credit + this._branchList[__loop]._code), MyLib._myUtil._moneyFormat(__getRow._branch[__loop]._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
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
                _view1.__excelFlieName = "งบทดลองแยกสาขา ประจำวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่  : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "งบทดลองแยกสาขา ประจำวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่  : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false), SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
            {
                // Column บน
                SMLReport._report._objectListType __columnHeaderObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                _view1._addColumn(__columnHeaderObject, 5, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, null, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__columnHeaderObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, null, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                float __widthPersent = (float)85 / (float)(this._branchList.Count + 1);
                _view1._addColumn(__columnHeaderObject, __widthPersent, true, SMLReport._report._columnBorder.LeftBottom, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, "ยอดงวด", _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center, true, _view1._fontStandard, Color.Black, 2);
                for (int __loop = 0; __loop < this._branchList.Count; __loop++)
                {
                    _view1._addColumn(__columnHeaderObject, __widthPersent, true, SMLReport._report._columnBorder.LeftRightBottom, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, this._branchList[__loop]._name, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center, true, _view1._fontStandard, Color.Black, 2);
                }
                // Column ข้อมูล
                __accountObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                _view1._addColumn(__accountObject, 5, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_code, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_name, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_name, _g.d.gl_list_view._account_name, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, __widthPersent / (float)2, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._debit, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__accountObject, __widthPersent / (float)2, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._credit, _g.d.gl_list_view._credit, SMLReport._report._cellAlign.Center);
                for (int __loop = 0; __loop < this._branchList.Count; __loop++)
                {
                    _view1._addColumn(__accountObject, __widthPersent / (float)2, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._debit + this._branchList[__loop]._code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._debit, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Center);
                    _view1._addColumn(__accountObject, __widthPersent / (float)2, true, SMLReport._report._columnBorder.LeftRight, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._credit + this._branchList[__loop]._code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._credit, _g.d.gl_list_view._credit, SMLReport._report._cellAlign.Center);
                }
                return true;
            }
            return false;
        }

        bool _view1__loadData()
        {
            //
            if (this._getFromProcess == null)
            {
                this.Cursor = Cursors.WaitCursor;
                string __query = "select distinct branch_code,name_1 from (select case when branch_code is null or branch_code='' then 'XERR.' else branch_code end as branch_code,case when branch_code is null or branch_code='' then 'XERR.' else COALESCE((select name_1 from erp_branch_list where erp_branch_list.code=branch_code),'ERR.'||branch_code) end as name_1 from gl_journal_detail) as q1 order by branch_code";
                DataTable __branchCodeTable = this._myFrameWork._queryShort(__query).Tables[0];
                this._branchList = new List<_g.g._branchListStruct>();
                for (int __row = 0; __row < __branchCodeTable.Rows.Count; __row++)
                {
                    _g.g._branchListStruct __data = new _g.g._branchListStruct();
                    __data._code = __branchCodeTable.Rows[__row][0].ToString();
                    __data._name = __branchCodeTable.Rows[__row][1].ToString();
                    this._branchList.Add(__data);
                }
                SMLProcess._glProcess __process = new SMLProcess._glProcess();
                this._getFromProcess = __process._glViewTrialBalanceBranch(_conditionDateBegin, _conditionDateEnd, __process._getChartOfAccountTreeView(new MyLib._myTreeViewDragDrop()), this._conditionAllAccount, this._conditionAccountBalance, 0, this._branchList,(this._conditionByPeriod) ? 0 : 1);
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
