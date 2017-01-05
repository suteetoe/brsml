using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._singhaReportProfitAndLost
{
    public partial class _report : UserControl
    {
        // ArrayList __getFromProcess;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __accountObject;
        //
        _condition _conditionScreen = new _condition();
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        Boolean _conditionAllAccount = false;
        Boolean _conditionAccountBalance = false;
        Boolean _conditionByPeriod = false;
        //
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        DataTable _dataGroup = null;
        DataTable _dataGroupSum = null;
        DataTable _data = null;
        DataTable _dataProfit = null;
        string _fieldAccountGroupName = "account_group_name";
        string _totalWord = "";
        int _mode = 0;
        int _loopBegin = 0;
        int _loopEnd = 0;
        //
        /// <summary>
        /// mode
        /// 0=งบกำไรขาดทุน
        /// 1=งบดุล
        /// </summary>
        /// <param name="mode"></param>
        public _report(int mode)
        {
            InitializeComponent();
            this._mode = mode;
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
            _conditionScreen._screenJournalCondition1._screenCreated = false;
            switch (this._mode)
            {
                case 0:
                    _conditionScreen._screenJournalCondition1._screenType = _screenJournalConditionType.SinghaReportProfitAndLost;
                    _conditionScreen._headLabel.Text = "รายงานงบกำไรขาดทุน";
                    break;
                case 1:
                    _conditionScreen._screenJournalCondition1._screenType = _screenJournalConditionType.SinghaReportBalance;
                    _conditionScreen._headLabel.Text = "รายงานงบดุล";
                    break;
            }
            string __dateBegin = _conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin);
            if (__dateBegin == "")
            {
                this._conditionScreen._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_begin, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                this._conditionScreen._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_end, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1));

            }
            this._conditionScreen.ShowDialog();
            this._conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin));
            this._conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_end));
            this._conditionAllAccount = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._select_all_account).Equals("1") ? true : false;
            this._conditionAccountBalance = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._select_account_balance).Equals("1") ? false : true;
            this._conditionByPeriod = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._period).Equals("1") ? true : false;
            this._dataGroup = null;
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
            int __columnAccountCode = _view1._findColumn(__accountObject, _g.d.gl_list_view._account_code);
            int __columnDebit = _view1._findColumn(__accountObject, _g.d.gl_list_view._debit);
            int __columnCredit = _view1._findColumn(__accountObject, _g.d.gl_list_view._credit);
            int __columnBalanceDebit = _view1._findColumn(__accountObject, _g.d.gl_list_view._balance_debit);
            decimal __sum1 = 0;
            decimal __sum2 = 0;
            int __accountGroupTypeLast = -1;
            //
            try
            {
                decimal __profitAndLost = 0;
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__accountObject._columnList[0];
                for (int __type = _loopBegin; __type <= _loopEnd; __type++)
                {
                    for (int __accountRow = 0; __accountRow < this._dataGroup.Rows.Count; __accountRow++)
                    {
                        DataRow __accountGroupGetRow = this._dataGroup.Rows[__accountRow];
                        int __accountGroupType = MyLib._myGlobal._intPhase(__accountGroupGetRow[_g.d.gl_chart_of_account._account_type].ToString());
                        if (__accountGroupTypeLast == -1)
                        {
                            __accountGroupTypeLast = __accountGroupType;
                        }
                        Boolean __check = false;
                        if (__accountGroupType == __type)
                        {
                            __check = true;
                        }
                        if (__check)
                        {
                            if (this._mode != 0 && __accountGroupTypeLast != __accountGroupType)
                            {
                                if (__accountGroupTypeLast == 0)
                                {
                                    Font __newFont2 = new Font(__getColumn._fontData, FontStyle.Bold);
                                    SMLReport._report._objectListType __accountDataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                                    _view1._createEmtryColumn(__accountObject, __accountDataDetailObject);
                                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnAccountCode, "* รวมสินทรัพย์ *", __newFont2, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnDebit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont2, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    decimal __value = (__sum1 - __sum2) * -1;
                                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnCredit, MyLib._myUtil._moneyFormat(__value, _formatNumber), __newFont2, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    __sum1 = 0;
                                    __sum2 = 0;
                                }
                                __accountGroupTypeLast = __accountGroupType;
                            }
                            string __accountGroupCode = __accountGroupGetRow[_g.d.gl_chart_of_account._account_group].ToString();
                            string __accountGroupName = __accountGroupGetRow[this._fieldAccountGroupName].ToString();
                            SMLReport._report._objectListType __accountDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__accountObject, __accountDataObject);
                            Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                            _view1._addDataColumn(__accountObject, __accountDataObject, __columnAccountCode, __accountGroupName, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            decimal __groupDebit = 0;
                            decimal __groupCredit = 0;
                            decimal __groupBalance = 0;
                            DataRow[] __groupSum = this._dataGroupSum.Select(_g.d.gl_chart_of_account._account_group + "='" + __accountGroupCode + "'");
                            if (__groupSum.Length > 0)
                            {
                                __groupDebit = MyLib._myGlobal._decimalPhase(__groupSum[0][_g.d.gl_journal_detail._debit].ToString());
                                __groupCredit = MyLib._myGlobal._decimalPhase(__groupSum[0][_g.d.gl_journal_detail._credit].ToString());
                                __groupBalance = __groupDebit - __groupCredit;
                            }
                            _view1._addDataColumn(__accountObject, __accountDataObject, __columnDebit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__accountObject, __accountDataObject, __columnCredit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__accountObject, __accountDataObject, __columnBalanceDebit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            //
                            Font __newFontDetail = new Font(__getColumn._fontData, FontStyle.Regular);
                            DataRow[] __details = this._data.Select(_g.d.gl_chart_of_account._account_group + "='" + __accountGroupCode + "'");
                            for (int __detailLoop = 0; __detailLoop < __details.Length; __detailLoop++)
                            {
                                string __accountCode = __details[__detailLoop][_g.d.gl_chart_of_account._code].ToString();
                                string __accountName = __details[__detailLoop][_g.d.gl_chart_of_account._name_1].ToString();
                                int __accountType = MyLib._myGlobal._intPhase(__details[__detailLoop][_g.d.gl_chart_of_account._account_type].ToString());
                                decimal __debit = MyLib._myGlobal._decimalPhase(__details[__detailLoop][_g.d.gl_journal_detail._debit].ToString());
                                decimal __credit = MyLib._myGlobal._decimalPhase(__details[__detailLoop][_g.d.gl_journal_detail._credit].ToString());
                                decimal __balance = __debit - __credit;
                                decimal __groupBalanceCalc = __groupBalance;
                                if (__type == 3 || __type == 1 || __type == 2)
                                {
                                    __balance = __balance * -1;
                                    __groupBalanceCalc = __groupBalanceCalc * -1;
                                    __sum1 += __balance;
                                }
                                else
                                {
                                    __sum2 += __balance;
                                }
                                if (__balance != 0)
                                {
                                    decimal __persent = (__groupBalanceCalc == 0) ? 0 : ((__balance * 100) / __groupBalanceCalc);
                                    //
                                    SMLReport._report._objectListType __accountDataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__accountObject, __accountDataDetailObject);
                                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnAccountCode, "    " + __accountName, __newFontDetail, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnDebit, MyLib._myUtil._moneyFormat(__balance, _formatNumber), __newFontDetail, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnCredit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFontDetail, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnBalanceDebit, MyLib._myUtil._moneyFormat(__persent, _formatNumber), __newFontDetail, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                }
                            }
                            if (__type == 2)
                            {
                                // กำไรขาดทุน
                                __profitAndLost = MyLib._myGlobal._decimalPhase(this._dataProfit.Rows[0][0].ToString()) * -1;
                                Font __newFont2 = new Font(__getColumn._fontData, FontStyle.Regular);
                                SMLReport._report._objectListType __accountDataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__accountObject, __accountDataDetailObject);
                                _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnAccountCode, "    กำไร (ขาดทุน) สะสม", __newFont2, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnDebit, MyLib._myUtil._moneyFormat(__profitAndLost, _formatNumber), __newFont2, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnCredit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont2, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                __groupBalance += (__profitAndLost * -1);
                                __sum1 += __profitAndLost;
                            }
                            // sum
                            if (__groupBalance != 0)
                            {
                                decimal __groupBalanceCalc = __groupBalance;
                                switch (this._mode)
                                {
                                    case 0:
                                        if (__type == 3)
                                        {
                                            __groupBalanceCalc = __groupBalanceCalc * -1;
                                        };
                                        break;
                                    default:
                                        {
                                            if (__accountGroupType == 1 || __accountGroupType == 2)
                                            {
                                                __groupBalanceCalc = __groupBalanceCalc * -1;
                                            }
                                        };
                                        break;
                                        //case 1: __groupBalanceCalc = __groupBalanceCalc * 1; break;
                                        //case 2: __groupBalanceCalc = __groupBalanceCalc * -1; break;
                                }
                                SMLReport._report._objectListType __accountDataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                                _view1._createEmtryColumn(__accountObject, __accountDataDetailObject);
                                _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnAccountCode, "** รวม" + __accountGroupName, __newFontDetail, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnDebit, MyLib._myUtil._moneyFormat(__groupBalanceCalc, _formatNumber), __newFontDetail, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnCredit, MyLib._myUtil._moneyFormat(__groupBalanceCalc, _formatNumber), __newFontDetail, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                            }
                        }
                    }
                    if (__type == 3 && __sum1 != 0)
                    {
                        Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                        SMLReport._report._objectListType __accountDataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(__accountObject, __accountDataDetailObject);
                        _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnAccountCode, "** รวมรายได้ ***", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnDebit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnCredit, MyLib._myUtil._moneyFormat(__sum1, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    }
                    if (__type == 4 && __sum2 != 0)
                    {
                        Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                        SMLReport._report._objectListType __accountDataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(__accountObject, __accountDataDetailObject);
                        _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnAccountCode, "** รวมค่าใช้จ่าย ***", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnDebit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnCredit, MyLib._myUtil._moneyFormat(__sum2, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    }
                }
                {
                    Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                    SMLReport._report._objectListType __accountDataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                    _view1._createEmtryColumn(__accountObject, __accountDataDetailObject);
                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnAccountCode, this._totalWord, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnDebit, MyLib._myUtil._moneyFormat(0, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    decimal __value = 0;
                    switch (this._mode)
                    {
                        case 0: __value = __sum1 - __sum2; break;
                        case 1: __value = __sum1 - __sum2; break;
                    }
                    _view1._addDataColumn(__accountObject, __accountDataDetailObject, __columnCredit, MyLib._myUtil._moneyFormat(__value, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1.__excelFlieName = _conditionScreen._headLabel.Text + " : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                string __str = _conditionScreen._headLabel.Text + " : ";
                if (this._conditionByPeriod)
                {
                    __str = __str + "ช่วงวันที่ " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " - " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                }
                else
                {
                    __str = __str + "สิ้นสุด ณ.วันที่ " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);

                }
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, __str, SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
            {
                // Column ข้อมูล
                __accountObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._addColumn(__accountObject, 50, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._account_code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_code, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Left, false);
                _view1._addColumn(__accountObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._debit, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Center, false);
                _view1._addColumn(__accountObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._credit, _g.d.gl_list_view._credit, SMLReport._report._cellAlign.Center, false);
                _view1._addColumn(__accountObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._balance_debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance_debit, _g.d.gl_list_view._balance_debit, SMLReport._report._cellAlign.Center, false);
                return true;
            }
            return false;
        }

        bool _view1__loadData()
        {
            if (this._dataGroup == null)
            {
                string __whereDate = "";
                string __whereFlag = "";
                if (this._mode == 0)
                {
                    if (this._conditionByPeriod)
                    {
                        __whereDate = _g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(_conditionDateBegin) + "\' and " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(_conditionDateEnd) + "\'";
                    }
                    else
                    {
                        __whereDate = _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(_conditionDateEnd) + "\'";
                    }
                }
                else
                {
                    __whereDate = _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(_conditionDateEnd) + "\'";
                }
                switch (this._mode)
                {
                    case 0:
                        __whereFlag = _g.d.gl_chart_of_account._account_type + " in (3,4) ";
                        this._loopBegin = 3;
                        this._loopEnd = 4;
                        this._totalWord = "* กำไร (ขาดทุน) *";
                        break;
                    case 1:
                        __whereFlag = _g.d.gl_chart_of_account._account_type + " in (0,1,2) ";
                        this._loopBegin = 0;
                        this._loopEnd = 2;
                        //this._loopEnd = 0;
                        this._totalWord = "* รวมหนี้สินและส่วนผู้ถือหุ้น *";
                        //this._totalWord = "* รวมสินทรัพย์ *";
                        break;
                        /*case 2:
                            __whereFlag = _g.d.gl_chart_of_account._account_type + " in (1,2) ";
                            this._loopBegin = 1;
                            this._loopEnd = 2;
                            this._totalWord = "* รวมหนี้สินและส่วนผู้ถือหุ้น *";
                            break;*/
                }
                this.Cursor = Cursors.WaitCursor;
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                string __query = "select distinct " + _g.d.gl_chart_of_account._account_group + ",(select " + _g.d.gl_account_group._name_1 + " from " + _g.d.gl_account_group._table + " where " + _g.d.gl_account_group._table + "." + _g.d.gl_account_group._code + "=" + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._account_group + ") as " + this._fieldAccountGroupName + "," + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + __whereFlag + " and " + _g.d.gl_chart_of_account._account_group + " <> '' and " + _g.d.gl_chart_of_account._account_group + " is not null order by " + _g.d.gl_chart_of_account._account_group + "," + _g.d.gl_chart_of_account._account_type;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                //
                __query = "select account_group,sum(debit) as debit,sum(credit) as credit from (select " + _g.d.gl_chart_of_account._account_group + "," + _g.d.gl_chart_of_account._code + ",coalesce((select sum(" + _g.d.gl_journal_detail._debit + ") from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code + "=" + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code + " and " + __whereDate + "),0) as " + _g.d.gl_journal_detail._debit + ",coalesce((select sum(" + _g.d.gl_journal_detail._credit + ") from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code + "=" + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code + " and " + __whereDate + "),0) as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_chart_of_account._table + " where " + __whereFlag + " group by " + _g.d.gl_chart_of_account._account_group + "," + _g.d.gl_chart_of_account._code + " order by " + _g.d.gl_chart_of_account._account_group + "," + _g.d.gl_chart_of_account._code + ") as q1 group by account_group order by account_group";
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                //
                __query = "select " + _g.d.gl_chart_of_account._account_group + "," + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._account_type + ",coalesce((select sum(" + _g.d.gl_journal_detail._debit + ") from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code + "=" + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code + " and " + __whereDate + "),0) as " + _g.d.gl_journal_detail._debit + ",coalesce((select sum(" + _g.d.gl_journal_detail._credit + ") from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code + "=" + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code + " and " + __whereDate + "),0) as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_chart_of_account._table + " where " + __whereFlag + " order by " + _g.d.gl_chart_of_account._code;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                //
                __query = "select sum(" + _g.d.gl_journal_detail._debit + "-" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._debit + " from " + _g.d.gl_journal_detail._table + " where " + __whereDate + " and (select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=" + _g.d.gl_journal_detail._account_code + ") in (3,4)";
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                //
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._dataGroup = ((DataSet)_getData[0]).Tables[0];
                this._dataGroupSum = ((DataSet)_getData[1]).Tables[0];
                this._data = ((DataSet)_getData[2]).Tables[0];
                this._dataProfit = ((DataSet)_getData[3]).Tables[0];
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
