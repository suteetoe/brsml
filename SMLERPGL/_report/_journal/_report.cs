using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Threading;

namespace SMLERPGL._report._journal
{
    public partial class _report : UserControl
    {
        DataSet _getJournal;
        DataSet _getJournalDetail;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __journalObject;
        SMLReport._report._objectListType __journalDetailObject;
        SMLReport._report._objectListType __totalDateObject;
        //
        _condition _conditionScreen = new _condition();
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        string _conditionDocBegin;
        string _conditionDocEnd;
        bool _conditionTotalByDate;
        int _conditionSortBy;
        //
        int _totalCountByDate;
        int _totalCount;
        double _totalAmountByDate;
        double _totalAmount;
        DateTime _totalDate = new DateTime(1000, 1, 1);
        //
        string _totalDescriptionColumnName = "totalDesc";
        string _totalAmountColumnName = "totalAmount";
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
            _view1._loadDataStream += new SMLReport._report.LoadDataStreamEventHandle(_view1__loadDataStream);
            _myFrameWork.__queryStreamEvent += new MyLib.QueryStreamEventHandler(_myFrameWork___queryStreamEvent);
            this._conditionScreen._buttonOk.Click += new EventHandler(_buttonOk_Click);
            _showCondition();
        }

        void _buttonOk_Click(object sender, EventArgs e)
        {
            this._build();
        }

        void _myFrameWork___queryStreamEvent(string lastMessage, int persentProcess)
        {
            this._view1._processMessage = lastMessage;
            this._view1._progessBarValue = persentProcess;
        }

        void _view1__loadDataStream()
        {
            if (this._getJournal == null)
            {
                _totalCountByDate = 0;
                _totalCount = 0;
                _totalAmountByDate = 0;
                _totalAmount = 0;
                _totalDate = new DateTime(1000, 1, 1);
                //
                try
                {
                    //
                    string __whereStr = " where ";
                    string __andStr = " and ";
                    StringBuilder __whereJournal = new StringBuilder();
                    StringBuilder __whereJournalDetail = new StringBuilder();
                    if (this._conditionDateBegin.Year != 1000)
                    {
                        if (__whereJournal.Length == 0)
                        {
                            __whereJournal.Append(__whereStr);
                            __whereJournalDetail.Append(__whereStr);
                        }
                        __whereJournal.Append(_g.d.gl_journal._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateBegin) + "\'");
                        __whereJournalDetail.Append(_g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateBegin) + "\'");
                    }
                    if (this._conditionDateEnd.Year != 1000)
                    {
                        if (__whereJournal.Length == 0)
                        {
                            __whereJournal.Append(__whereStr);
                            __whereJournalDetail.Append(__whereStr);
                        }
                        else
                        {
                            __whereJournal.Append(__andStr);
                            __whereJournalDetail.Append(__andStr);
                        }
                        __whereJournal.Append(_g.d.gl_journal._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateEnd) + "\'");
                        __whereJournalDetail.Append(_g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateEnd) + "\'");
                    }
                    if (this._conditionDocBegin.Length != 0)
                    {
                        if (__whereJournal.Length == 0)
                        {
                            __whereJournal.Append(__whereStr);
                            __whereJournalDetail.Append(__whereStr);
                        }
                        else
                        {
                            __whereJournal.Append(__andStr);
                            __whereJournalDetail.Append(__andStr);
                        }
                        __whereJournal.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no) + ">=\'" + this._conditionDocBegin.ToUpper() + "\'");
                        __whereJournalDetail.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._doc_no) + ">=\'" + this._conditionDocBegin.ToUpper() + "\'");
                    }
                    if (this._conditionDocEnd.Length != 0)
                    {
                        if (__whereJournal.Length == 0)
                        {
                            __whereJournal.Append(__whereStr);
                            __whereJournalDetail.Append(__whereStr);
                        }
                        else
                        {
                            __whereJournal.Append(__andStr);
                            __whereJournalDetail.Append(__andStr);
                        }
                        __whereJournal.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no) + "<=\'" + this._conditionDocEnd.ToLower() + "\'");
                        __whereJournalDetail.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._doc_no) + "<=\'" + this._conditionDocEnd.ToUpper() + "\'");
                    }
                    //
                    string __orderByJournal = (_conditionSortBy == 1) ? (_g.d.gl_journal._doc_date + "," + MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no)) : (MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no) + "," + _g.d.gl_journal._doc_date);
                    //
                    StringBuilder __whereJournalBook = new StringBuilder();
                    StringBuilder __whereJournalDetailBook = new StringBuilder();
                    bool __allBook = true;
                    for (int __loop = 0; __loop < this._conditionScreen._selectBook._bookGrid._rowData.Count; __loop++)
                    {
                        if (((int)this._conditionScreen._selectBook._bookGrid._cellGet(__loop, "check")) == 0)
                        {
                            __allBook = false;
                            break;
                        }
                    }
                    if (__allBook == false)
                    {
                        bool __isFirst = false;
                        for (int __loop = 0; __loop < this._conditionScreen._selectBook._bookGrid._rowData.Count; __loop++)
                        {
                            if (((int)this._conditionScreen._selectBook._bookGrid._cellGet(__loop, "check")) == 1)
                            {
                                if (__isFirst == false)
                                {
                                    __isFirst = true;
                                    if (__whereJournal.Length != 0)
                                    {
                                        __whereJournalBook.Append(" and ");
                                        __whereJournalDetailBook.Append(" and ");
                                    }
                                    else
                                    {
                                        __whereJournalBook.Append(" where ");
                                        __whereJournalDetailBook.Append(" where ");
                                    }
                                    __whereJournalBook.Append(" (");
                                    __whereJournalDetailBook.Append(" (");
                                }
                                else
                                {
                                    __whereJournalBook.Append(" or ");
                                    __whereJournalDetailBook.Append(" or ");
                                }
                                string __getBookCode = (string)this._conditionScreen._selectBook._bookGrid._cellGet(__loop, _g.d.gl_journal_book._code);
                                __whereJournalBook.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal._book_code) + "=\'" + __getBookCode.ToUpper() + "\'");
                                __whereJournalDetailBook.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._book_code) + "=\'" + __getBookCode.ToUpper() + "\'");
                            }
                        }
                        if (__isFirst)
                        {
                            __whereJournalBook.Append(") ");
                            __whereJournalDetailBook.Append(") ");
                        }
                    }
                    __whereJournal.Append(__whereJournalBook.ToString());
                    __whereJournalDetail.Append(__whereJournalDetailBook.ToString());
                                     
                    //
                    string __query = "select " + _g.d.gl_journal._doc_date +
                        "," + _g.d.gl_journal._doc_no +
                        "," + _g.d.gl_journal._ref_date +
                        "," + _g.d.gl_journal._ref_no +
                        "," + MyLib._myGlobal._concatQuery(MyLib._myGlobal._concatQuery(_g.d.gl_journal._book_code, "\'/\'"), "(select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_book._code) + "=" + MyLib._myGlobal._addUpper(_g.d.gl_journal._book_code)) + ") as book_full " +
                        "," + _g.d.gl_journal._description + "," + _g.d.gl_journal._debit + "," + _g.d.gl_journal._book_code +
                        " from " + _g.d.gl_journal._table +
                        __whereJournal.ToString() + " order by " + __orderByJournal;

                    _getJournal = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query, "Journal");
                    //
                    __query = "select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._account_code + "," + _g.d.gl_journal_detail._account_name + "," + _g.d.gl_journal_detail._description + "," + _g.d.gl_journal_detail._debit + "," + _g.d.gl_journal_detail._credit + "," + _g.d.gl_journal_detail._book_code + " from " + _g.d.gl_journal_detail._table + __whereJournalDetail.ToString() + " order by " + _g.d.gl_journal_detail._doc_date + "," + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._doc_no) + "," + _g.d.gl_journal_detail._debit_or_credit + "," + _g.d.gl_journal_detail._account_code;
                    _getJournalDetail = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query, "Journal Detail");
                    // สั่งให้ประมวลผลรายงาน
                    this._view1._buildReportActive = true;
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
            // สั่งให้ประมวลผลรายงาน
            this._view1._buildReportActive = true;
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _totalDateObject(SMLReport._report._columnListType getColumn)
        {
            if (_conditionTotalByDate == true && _conditionSortBy == 1 && _totalCountByDate > 0)
            {
                int __columnTotalDesc = _view1._findColumn(__totalDateObject, _totalDescriptionColumnName);
                int __columnTotalAmount = _view1._findColumn(__totalDateObject, _totalAmountColumnName);
                //
                SMLReport._report._objectListType __totalDateNewObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 45, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__totalDateObject, __totalDateNewObject);
                Font __newFont = new Font(getColumn._fontData, FontStyle.Bold);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalDesc, "ยอดรวมวันที่ " + MyLib._myGlobal._convertDateToString(_totalDate, false, false) + " จำนวน " + string.Format(_formatCountNumber, _totalCountByDate) + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalAmount, string.Format(_formatNumber, _totalAmountByDate), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
            }
            _totalCountByDate = 0;
            _totalAmountByDate = 0;
        }

        void _totalObject(SMLReport._report._columnListType getColumn)
        {
            int __columnTotalDesc = _view1._findColumn(__totalDateObject, _totalDescriptionColumnName);
            int __columnTotalAmount = _view1._findColumn(__totalDateObject, _totalAmountColumnName);
            //
            if (_totalCount > 0)
            {
                SMLReport._report._objectListType __totalDateNewObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 45, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__totalDateObject, __totalDateNewObject);
                Font __newFont = new Font(getColumn._fontData, FontStyle.Bold);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalDesc, "ยอดรวมทั้งสิ้น  จำนวน " + string.Format(_formatCountNumber, _totalCount) + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalAmount, string.Format(_formatNumber, _totalAmount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
            }
        }

        void _view1__getDataObject()
        {
            int __docDate = _view1._findColumn(__journalObject, _g.d.gl_journal._doc_date);
            int __docNo = _view1._findColumn(__journalObject, _g.d.gl_journal._doc_no);
            //int __docRefDate = _view1._findColumn(__journalObject, _g.d.gl_journal._ref_date);
            //int __docRefNo = _view1._findColumn(__journalObject, _g.d.gl_journal._ref_no);
            int __docBook = _view1._findColumn(__journalObject, _g.d.gl_journal._book_code);
            int __docDescription = _view1._findColumn(__journalObject, _g.d.gl_journal._description);
            int __docAmount = _view1._findColumn(__journalObject, _g.d.gl_journal._debit);
            //
            int __detailAccountCode = _view1._findColumn(__journalDetailObject, _g.d.gl_journal_detail._account_code);
            int __detailAccountName = _view1._findColumn(__journalDetailObject, _g.d.gl_journal_detail._account_name);
            int __detailAccountDescription = _view1._findColumn(__journalDetailObject, _g.d.gl_journal_detail._description);
            int __detailDebit = _view1._findColumn(__journalDetailObject, _g.d.gl_journal_detail._debit);
            int __detailCredit = _view1._findColumn(__journalDetailObject, _g.d.gl_journal_detail._credit);
            //
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__journalObject._columnList[0];
            for (int __row = 0; __row < _getJournal.Tables[0].Rows.Count; __row++)
            {
                string __getDateStr = _getJournal.Tables[0].Rows[__row].ItemArray[0].ToString();
                DateTime __getDate = MyLib._myGlobal._convertDateFromQuery(__getDateStr);
                if (_totalDate.Year == 1000)
                {
                    _totalDate = __getDate;
                }
                if (_totalDate.Equals(__getDate) == false)
                {
                    _totalDateObject(__getColumn);
                    _totalDate = __getDate;
                }
                DateTime __getRefDate = MyLib._myGlobal._convertDateFromQuery(_getJournal.Tables[0].Rows[__row].ItemArray[2].ToString());
                double __getAmount = Double.Parse(_getJournal.Tables[0].Rows[__row].ItemArray[6].ToString());
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                string __getDocNo = _getJournal.Tables[0].Rows[__row].ItemArray[1].ToString();
                string __getBook = _getJournal.Tables[0].Rows[__row].ItemArray[7].ToString();
                //
                SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__journalObject, __dataObject);
                if (_conditionSortBy == 1)
                {
                    // เรียงตามวันที่
                    _view1._addDataColumn(__journalObject, __dataObject, __docDate, MyLib._myGlobal._convertDateToString(__getDate, false, true), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__journalObject, __dataObject, __docNo, __getDocNo, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                }
                else
                {
                    // เรียงตามเลขที่
                    _view1._addDataColumn(__journalObject, __dataObject, __docNo, __getDocNo, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__journalObject, __dataObject, __docDate, MyLib._myGlobal._convertDateToString(__getDate, false, true), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                }
                //_view1._addDataColumn(__journalObject, __dataObject, __docRefDate, MyLib._myGlobal._convertDateToString(__getRefDate, false, true), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                //_view1._addDataColumn(__journalObject, __dataObject, __docRefNo, _getJournal.Tables[0].Rows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                _view1._addDataColumn(__journalObject, __dataObject, __docBook, _getJournal.Tables[0].Rows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__journalObject, __dataObject, __docDescription, _getJournal.Tables[0].Rows[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__journalObject, __dataObject, __docAmount, string.Format(_formatNumber, __getAmount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                //
                // รวม
                _totalCountByDate++;
                _totalAmountByDate += __getAmount;
                _totalCount++;
                _totalAmount += __getAmount;
                //
                DataRow[] __getJournalDetail = _getJournalDetail.Tables[0].Select(_g.d.gl_journal_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_detail._doc_date + "=\'" + __getDateStr + "\' and " + _g.d.gl_journal_detail._book_code + "=\'" + __getBook + "\'");
                for (int __rowDetail = 0; __rowDetail < __getJournalDetail.Length; __rowDetail++)
                {
                    SMLReport._report._objectListType __dataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(__journalDetailObject, __dataDetailObject);
                    double __getDebit = Double.Parse(__getJournalDetail[__rowDetail].ItemArray[5].ToString());
                    double __getCredit = Double.Parse(__getJournalDetail[__rowDetail].ItemArray[6].ToString());
                    _view1._addDataColumn(__journalDetailObject, __dataDetailObject, __detailAccountCode, __getJournalDetail[__rowDetail].ItemArray[2].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__journalDetailObject, __dataDetailObject, __detailAccountName, __getJournalDetail[__rowDetail].ItemArray[3].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__journalDetailObject, __dataDetailObject, __detailAccountDescription, __getJournalDetail[__rowDetail].ItemArray[4].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__journalDetailObject, __dataDetailObject, __detailDebit, (__getDebit == 0) ? "" : string.Format(_formatNumber, __getDebit), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    _view1._addDataColumn(__journalDetailObject, __dataDetailObject, __detailCredit, (__getCredit == 0) ? "" : string.Format(_formatNumber, __getCredit), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                }
            }
            _totalDateObject(__getColumn);
            _totalObject(__getColumn);
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1.__excelFlieName = "รายงานข้อมูลรายวัน จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานข้อมูลรายวัน จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false), SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
            {
                __journalObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                if (_conditionSortBy == 1)
                {
                    // เรียงตามวันที่
                    _view1._addColumn(__journalObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_date, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date, _g.d.gl_journal._doc_date, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__journalObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_no, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no, _g.d.gl_journal._doc_no, SMLReport._report._cellAlign.Left);
                }
                else
                {
                    // เรียงตามเลขที่เอกสาร
                    _view1._addColumn(__journalObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_no, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no, _g.d.gl_journal._doc_no, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__journalObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_date, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date, _g.d.gl_journal._doc_date, SMLReport._report._cellAlign.Left);
                }
                //_view1._addColumn(__journalObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._ref_date, _g.d.gl_journal._table + "." + _g.d.gl_journal._ref_date, _g.d.gl_journal._ref_date, SMLReport._report._cellAlign.Left);
                //_view1._addColumn(__journalObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._ref_no, _g.d.gl_journal._table + "." + _g.d.gl_journal._ref_no, _g.d.gl_journal._ref_no, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__journalObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._book_code, _g.d.gl_journal._table + "." + _g.d.gl_journal._book_code, _g.d.gl_journal._book_code, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__journalObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._debit, _g.d.gl_journal._table + "." + _g.d.gl_journal._debit, _g.d.gl_journal._debit, SMLReport._report._cellAlign.Right);
                _view1._addColumn(__journalObject, 24 + 15 + 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._description, _g.d.gl_journal._table + "." + _g.d.gl_journal._description, _g.d.gl_journal._description, SMLReport._report._cellAlign.Left);
                //
                __journalDetailObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.Bottom);
                _view1._addColumn(__journalDetailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._account_code, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__journalDetailObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._account_name, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__journalDetailObject, 22, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._description, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._description, _g.d.gl_journal_detail._description, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__journalDetailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._debit, SMLReport._report._cellAlign.Right);
                _view1._addColumn(__journalDetailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._credit, SMLReport._report._cellAlign.Right);
                // สร้าง Column ไม่ให้แสดง มีไว้เพื่อกำหนดตำแหน่งยอดรวมเท่านั้น
                __totalDateObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 45, true, SMLReport._report._columnBorder.None, false);
                _view1._addColumn(__totalDateObject, 40, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.Bottom, this._totalDescriptionColumnName, this._totalDescriptionColumnName, this._totalDescriptionColumnName, SMLReport._report._cellAlign.Left, false);
                _view1._addColumn(__totalDateObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.Bottom, this._totalAmountColumnName, this._totalAmountColumnName, this._totalAmountColumnName, SMLReport._report._cellAlign.Right, false);
                return true;
            }
            return false;
        }

        bool _view1__loadData()
        {
            _totalCountByDate = 0;
            _totalCount = 0;
            _totalAmountByDate = 0;
            _totalAmount = 0;
            _totalDate = new DateTime(1000, 1, 1);
            //
            _view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //
                string __whereStr = " where ";
                string __andStr = " and ";
                StringBuilder __whereJournal = new StringBuilder();
                StringBuilder __whereJournalDetail = new StringBuilder();
                if (this._conditionDateBegin.Year != 1000)
                {
                    if (__whereJournal.Length == 0)
                    {
                        __whereJournal.Append(__whereStr);
                        __whereJournalDetail.Append(__whereStr);
                    }
                    __whereJournal.Append(_g.d.gl_journal._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateBegin) + "\'");
                    __whereJournalDetail.Append(_g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateBegin) + "\'");
                }
                if (this._conditionDateEnd.Year != 1000)
                {
                    if (__whereJournal.Length == 0)
                    {
                        __whereJournal.Append(__whereStr);
                        __whereJournalDetail.Append(__whereStr);
                    }
                    else
                    {
                        __whereJournal.Append(__andStr);
                        __whereJournalDetail.Append(__andStr);
                    }
                    __whereJournal.Append(_g.d.gl_journal._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateEnd) + "\'");
                    __whereJournalDetail.Append(_g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateEnd) + "\'");
                }
                if (this._conditionDocBegin.Length != 0)
                {
                    if (__whereJournal.Length == 0)
                    {
                        __whereJournal.Append(__whereStr);
                        __whereJournalDetail.Append(__whereStr);
                    }
                    else
                    {
                        __whereJournal.Append(__andStr);
                        __whereJournalDetail.Append(__andStr);
                    }
                    __whereJournal.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no) + ">=\'" + this._conditionDocBegin.ToUpper() + "\'");
                    __whereJournalDetail.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._doc_no) + ">=\'" + this._conditionDocBegin.ToUpper() + "\'");
                }
                if (this._conditionDocEnd.Length != 0)
                {
                    if (__whereJournal.Length == 0)
                    {
                        __whereJournal.Append(__whereStr);
                        __whereJournalDetail.Append(__whereStr);
                    }
                    else
                    {
                        __whereJournal.Append(__andStr);
                        __whereJournalDetail.Append(__andStr);
                    }
                    __whereJournal.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no) + "<=\'" + this._conditionDocEnd.ToLower() + "\'");
                    __whereJournalDetail.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._doc_no) + "<=\'" + this._conditionDocEnd.ToUpper() + "\'");
                }
                //
                string __orderByJournal = (_conditionSortBy == 1) ? (_g.d.gl_journal._doc_date + "," + MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no)) : (MyLib._myGlobal._addUpper(_g.d.gl_journal._doc_no) + "," + _g.d.gl_journal._doc_date);
                //
                StringBuilder __whereJournalBook = new StringBuilder();
                StringBuilder __whereJournalDetailBook = new StringBuilder();
                bool __allBook = true;
                for (int __loop = 0; __loop < this._conditionScreen._selectBook._bookGrid._rowData.Count; __loop++)
                {
                    if (((int)this._conditionScreen._selectBook._bookGrid._cellGet(__loop, "check")) == 0)
                    {
                        __allBook = false;
                        break;
                    }
                }
                if (__allBook == false)
                {
                    bool __isFirst = false;
                    for (int __loop = 0; __loop < this._conditionScreen._selectBook._bookGrid._rowData.Count; __loop++)
                    {
                        if (((int)this._conditionScreen._selectBook._bookGrid._cellGet(__loop, "check")) == 1)
                        {
                            if (__isFirst == false)
                            {
                                __isFirst = true;
                                if (__whereJournal.Length != 0)
                                {
                                    __whereJournalBook.Append(" and ");
                                    __whereJournalDetailBook.Append(" and ");
                                }
                                else
                                {
                                    __whereJournalBook.Append(" where ");
                                    __whereJournalDetailBook.Append(" where ");
                                }
                                __whereJournalBook.Append(" (");
                                __whereJournalDetailBook.Append(" (");
                            }
                            else
                            {
                                __whereJournalBook.Append(" or ");
                                __whereJournalDetailBook.Append(" or ");
                            }
                            string __getBookCode = (string)this._conditionScreen._selectBook._bookGrid._cellGet(__loop, _g.d.gl_journal_book._code);
                            __whereJournalBook.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal._book_code) + "=\'" + __getBookCode.ToUpper() + "\'");
                            __whereJournalDetailBook.Append(MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._book_code) + "=\'" + __getBookCode.ToUpper() + "\'");
                        }
                    }
                    if (__isFirst)
                    {
                        __whereJournalBook.Append(") ");
                        __whereJournalDetailBook.Append(") ");
                    }
                }
                __whereJournal.Append(__whereJournalBook.ToString());
                __whereJournalDetail.Append(__whereJournalDetailBook.ToString());
                //
                string __query = "select " + _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._ref_date + "," + _g.d.gl_journal._ref_no + "," +
                    MyLib._myGlobal._concatQuery(MyLib._myGlobal._concatQuery(_g.d.gl_journal._book_code, "\'/\'"),
                    "(select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_book._code) + "=" + MyLib._myGlobal._addUpper(_g.d.gl_journal._book_code)) + ") as book_full ," +
                    _g.d.gl_journal._description + "," + _g.d.gl_journal._debit + "," + _g.d.gl_journal._book_code + " from " + _g.d.gl_journal._table + __whereJournal.ToString() + " order by " + __orderByJournal;
                _getJournal = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                //
                __query = "select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._account_code + "," + _g.d.gl_journal_detail._account_name + "," + _g.d.gl_journal_detail._description + "," + _g.d.gl_journal_detail._debit + "," +
                    _g.d.gl_journal_detail._credit + "," + _g.d.gl_journal_detail._book_code + " from " + _g.d.gl_journal_detail._table + __whereJournalDetail.ToString() +
                    " order by " + _g.d.gl_journal_detail._doc_date + "," + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._doc_no) + "," + _g.d.gl_journal_detail._debit_or_credit + "," + _g.d.gl_journal_detail._account_code;
                _getJournalDetail = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query, "Journal Detail");
                this._view1._buildReportActive = true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
                return false;
            }
            _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            this.Cursor = Cursors.Default;
            return true;
        }

        void _build()
        {
            _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin));
            _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_end));
            _conditionDocBegin = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._doc_begin);
            _conditionDocEnd = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._doc_end);
            _conditionTotalByDate = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._total_end_date).Equals("1") ? true : false;
            _conditionSortBy = MyLib._myGlobal._intPhase(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._sort_by));//1=วันที่,2=เอกสาร
            this._getJournal = null; // จะได้ load data ใหม่
            this._getJournalDetail = null;
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._build();
        }
    }
}
