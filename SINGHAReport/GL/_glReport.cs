using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace SINGHAReport.GL
{
    public partial class _glReport : UserControl
    {
        _conditionForm _conditionScreen;
        DataSet _getAccount;
        _singhaReportEnum _mode;

        SMLReport._report._objectListType __accountObject;
        SMLReport._report._objectListType __jounalListObject;
        SMLReport._report._objectListType __ojtReport;
        SMLReport._report._objectListType __totalDateObject;

        private DataTable _dataTable;
        private DataTable _dataDetailTable;

        DataSet _getJournal;
        DataSet _getJournalDetail;


        MyLib._myFrameWork _myFrameWork;


        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        string _conditionDocBegin;
        string _conditionDocEnd;
        bool _conditionTotalByDate;
        int _conditionSortBy;
        int _totalCountByDate;
        int _totalCount;
        double _totalAmountByDate;
        double _totalAmount;

        double _totalDebitByDate;
        double _totalCreditByDate;

        double _totalDebitAmount;
        double _totalCreditAmount;


        DateTime _totalDate = new DateTime(1000, 1, 1);
        string _totalDescriptionColumnName = "totalDesc";
        string _totalAmountColumnName = "totalAmount";

        string _columnTotalAmounDebit = "totalDebit";
        string _columnTotalAmounCredit = "totalCredit";

        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");


        string _reportDescripton = "";
        string _conditionTextDetail = "";
        string _conditionText = "";
        string _screenName = "";

        public _glReport(_singhaReportEnum reportType, string screenName)
        {
            _mode = reportType;
            _screenName = screenName;

            InitializeComponent();

            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);

            switch (this._mode)
            {
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
                    break;

                case _singhaReportEnum.GL_ข้อมูลรายวัน:
                    _view1._buttonExample.Enabled = false;
                    _view1._loadDataStream += new SMLReport._report.LoadDataStreamEventHandle(_view1__loadDataStream);
                    this._myFrameWork = new MyLib._myFrameWork();
                    _myFrameWork.__queryStreamEvent += new MyLib.QueryStreamEventHandler(_myFrameWork___queryStreamEvent);
                    break;

                default:
                    _view1._loadDataByThread += _view1__loadDataByThread;
                    //_view1._getTotalCurrentRow += _view1__getTotalCurrentRow;
                    _view1._beforeReportDrawPaperArgs += _view1__beforeReportDrawPaperArgs;
                    break;
            }

            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            if (this._mode == _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง)
            {
                this._view1._paper._lineSpaceing = 140;
                this._view1._paper._drawFooterReportEvent += _paper__drawFooterReportEvent;
            }


            if (this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3 ||
                this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53)
            {
                // this._view1._pageSetupDialog.PageSettings.Landscape = true;
            }

            // condition
            _conditionScreen = new _conditionForm(reportType, screenName);
            _showCondition();
        }

        private void _paper__drawFooterReportEvent(int pageNumber, Graphics g, float startDrawYPoint, float drawScale)
        {
            if (this._mode == _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง)
            {
                int _pageWidth = (int)(((_view1._pageSetupDialog.PageSettings.Landscape) ? _view1._pageSetupDialog.PageSettings.PaperSize.Height : _view1._pageSetupDialog.PageSettings.PaperSize.Width) - (_view1._pageSetupDialog.PageSettings.Margins.Left + _view1._pageSetupDialog.PageSettings.Margins.Right));
                int _pageHeight = (int)(((_view1._pageSetupDialog.PageSettings.Landscape) ? _view1._pageSetupDialog.PageSettings.PaperSize.Width : _view1._pageSetupDialog.PageSettings.PaperSize.Height) - (_view1._pageSetupDialog.PageSettings.Margins.Top + _view1._pageSetupDialog.PageSettings.Margins.Bottom));

                float __y = (_pageHeight - 110) * drawScale; // startDrawYPoint;
                int _leftMargin = (int)(_view1._pageSetupDialog.PageSettings.Margins.Left * drawScale);
                //
                Font __newFont = new Font(_view1._fontStandard.FontFamily, (float)(_view1._fontStandard.Size * drawScale), _view1._fontStandard.Style);


                // rect
                g.DrawString("รายการคืน ขวด - ลังเปล่า", __newFont, Brushes.Black, new PointF(_leftMargin, __y));
                __y += (25 * drawScale);

                Pen __drawPen = new Pen(Brushes.Black, 1);
                RectangleF __rect1 = new RectangleF(_leftMargin, __y, ((40 * (_pageWidth / 100)) * drawScale), 100 * drawScale);
                g.DrawRectangle(__drawPen, Rectangle.Round(__rect1));

                RectangleF __rect2 = new RectangleF(_leftMargin + ((55 * (_pageWidth / 100)) * drawScale), __y, ((24 * (_pageWidth / 100)) * drawScale), 100 * drawScale);
                g.DrawRectangle(__drawPen, Rectangle.Round(__rect2));

                RectangleF __rect3 = new RectangleF(_leftMargin + ((80 * (_pageWidth / 100)) * drawScale), __y, ((24 * (_pageWidth / 100)) * drawScale), 100 * drawScale);
                g.DrawRectangle(__drawPen, Rectangle.Round(__rect3));


                StringFormat __centerFormat = StringFormat.GenericDefault;
                __centerFormat.Alignment = StringAlignment.Center;


                g.DrawString("รายการ", __newFont, Brushes.Black, new PointF(_leftMargin, __y));
                g.DrawString("ขวดโซดา", __newFont, Brushes.Black, new PointF(_leftMargin + ((10 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawString("ขวดน้ำ", __newFont, Brushes.Black, new PointF(_leftMargin + ((20 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawString("รวมลัง", __newFont, Brushes.Black, new PointF(_leftMargin + ((30 * (_pageWidth / 100)) * drawScale), __y));

                g.DrawString("คงคลัง", __newFont, Brushes.Black, new PointF(_leftMargin + ((55 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawString("คงเหลือ", __newFont, Brushes.Black, new PointF(_leftMargin + ((80 * (_pageWidth / 100)) * drawScale), __y));
                __y += (25 * drawScale);

                g.DrawLine(__drawPen, new PointF(__rect1.X, __y), new PointF(__rect1.X + __rect1.Width, __y));

                g.DrawString("รวมจ่าย", __newFont, Brushes.Black, new PointF(_leftMargin, __y));
                g.DrawString("Checker.........................................", __newFont, Brushes.Black, new PointF(_leftMargin + ((55 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawString("Checker.........................................", __newFont, Brushes.Black, new PointF(_leftMargin + ((80 * (_pageWidth / 100)) * drawScale), __y));
                __y += (25 * drawScale);

                g.DrawLine(__drawPen, new PointF(__rect1.X, __y), new PointF(__rect1.X + __rect1.Width, __y));

                g.DrawString("รับเข้า", __newFont, Brushes.Black, new PointF(_leftMargin, __y));
                g.DrawString("ผู้เบิก...............................................", __newFont, Brushes.Black, new PointF(_leftMargin + ((55 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawString("ผู้เบิก...............................................", __newFont, Brushes.Black, new PointF(_leftMargin + ((80 * (_pageWidth / 100)) * drawScale), __y));
                __y += (25 * drawScale);

                g.DrawLine(__drawPen, new PointF(__rect1.X, __y), new PointF(__rect1.X + __rect1.Width, __y));

                g.DrawString("ขาย/คืน", __newFont, Brushes.Black, new PointF(_leftMargin, __y));
                g.DrawString("ผู้อนุมัติ............................................", __newFont, Brushes.Black, new PointF(_leftMargin + ((55 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawString("ผู้อนุมัติ............................................", __newFont, Brushes.Black, new PointF(_leftMargin + ((80 * (_pageWidth / 100)) * drawScale), __y));

                __y += (25 * drawScale);
                g.DrawLine(__drawPen, new PointF(__rect1.X + ((10 * (_pageWidth / 100)) * drawScale), __rect1.Y), new PointF(__rect1.X + ((10 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawLine(__drawPen, new PointF(__rect1.X + ((20 * (_pageWidth / 100)) * drawScale), __rect1.Y), new PointF(__rect1.X + ((20 * (_pageWidth / 100)) * drawScale), __y));
                g.DrawLine(__drawPen, new PointF(__rect1.X + ((30 * (_pageWidth / 100)) * drawScale), __rect1.Y), new PointF(__rect1.X + ((30 * (_pageWidth / 100)) * drawScale), __y));


                __newFont.Dispose();
            }
        }

        void _myFrameWork___queryStreamEvent(string lastMessage, int persentProcess)
        {
            this._view1._processMessage = lastMessage;
            this._view1._progessBarValue = persentProcess;
        }

        void _view1__loadDataStream()
        {
            if (this._dataTable == null)
            {
                _totalCountByDate = 0;
                _totalCount = 0;
                _totalAmountByDate = 0;
                _totalDebitAmount = 0;
                _totalAmount = 0;

                _totalDebitByDate = 0;
                _totalCreditByDate = 0;

                _totalCreditAmount = 0;
                _totalDebitAmount = 0;

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
                    /*
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
                    }*/
                    __whereJournal.Append(__whereJournalBook.ToString());
                    __whereJournalDetail.Append(__whereJournalDetailBook.ToString());

                    // branch
                    StringBuilder __branchCodeFilter = new StringBuilder();

                    if (this._conditionScreen._useBranchCheckbox.Checked == true)
                    {
                        for (int __branchRow = 0; __branchRow < this._conditionScreen._gridCondition._rowData.Count; __branchRow++)
                        {
                            if (this._conditionScreen._gridCondition._cellGet(__branchRow, 0).ToString().Equals("1"))
                            {
                                if (__branchCodeFilter.Length > 0)
                                {
                                    __branchCodeFilter.Append(",");
                                }

                                string __branchCode = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._code).ToString();
                                // string __branchName = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._name_1).ToString();

                                __branchCodeFilter.Append("\'" + __branchCode + "\'");
                            }
                        }
                    }

                    if (__branchCodeFilter.Length > 0)
                    {
                        if (__whereJournal.Length > 0)
                        {
                            __whereJournal.Append(" and " + _g.d.gl_journal._branch_code + " in (" + __branchCodeFilter.ToString() + ")");
                            __whereJournalDetail.Append(" and " + _g.d.gl_journal._branch_code + " in (" + __branchCodeFilter.ToString() + ")");
                        }
                        else
                        {
                            __whereJournal.Append(" where " + _g.d.gl_journal._branch_code + " in (" + __branchCodeFilter.ToString() + ")");
                            __whereJournalDetail.Append(" where " + _g.d.gl_journal._branch_code + " in (" + __branchCodeFilter.ToString() + ")");
                        }
                    }

                    //
                    string __query = "select " + _g.d.gl_journal._doc_date +
                    "," + _g.d.gl_journal._doc_no +
                    "," + _g.d.gl_journal._ref_date +
                    "," + _g.d.gl_journal._ref_no +
                    "," + MyLib._myGlobal._concatQuery(MyLib._myGlobal._concatQuery(_g.d.gl_journal._book_code, "\'/\'"), "(select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_book._code) + "=" + MyLib._myGlobal._addUpper(_g.d.gl_journal._book_code)) + ") as book_full " +
                    "," + _g.d.gl_journal._description + "," + _g.d.gl_journal._debit + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._credit + "," + _g.d.gl_journal._branch_code +
                    " from " + _g.d.gl_journal._table +
                    __whereJournal.ToString() + " order by " + ((this._conditionScreen._useBranchCheckbox.Checked == true) ? _g.d.gl_journal._branch_code + "," : "") + __orderByJournal;

                    this._getJournal = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query, "Journal");
                    //
                    __query = "select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._account_code + "," + _g.d.gl_journal_detail._account_name + "," + _g.d.gl_journal_detail._description + "," + _g.d.gl_journal_detail._debit + "," + _g.d.gl_journal_detail._credit + "," + _g.d.gl_journal_detail._book_code + "," + _g.d.gl_journal_detail._branch_code + " from " + _g.d.gl_journal_detail._table + __whereJournalDetail.ToString() + " order by " + ((this._conditionScreen._useBranchCheckbox.Checked == true) ? _g.d.gl_journal._branch_code + "," : "") + _g.d.gl_journal_detail._doc_date + "," + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._doc_no) + "," + _g.d.gl_journal_detail._debit_or_credit + "," + _g.d.gl_journal_detail._account_code;
                    this._getJournalDetail = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query, "Journal Detail");
                    // สั่งให้ประมวลผลรายงาน
                    this._view1._buildReportActive = true;
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }

            this._view1._buildReportActive = true;

        }

        private void _view1__beforeReportDrawPaperArgs(SMLReport._report._pageListType pageListType)
        {
            if (this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3 || this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53)
            {
                decimal __sumPay = 0M;
                decimal __sumTax = 0M;
                string __currentBranch = "";

                for (int loop = 0; loop < pageListType._objectList.Count; loop++)
                {
                    SMLReport._report._objectListType __getObject = (SMLReport._report._objectListType)pageListType._objectList[loop];

                    if (__getObject._isDataRow == true)
                    {
                        __currentBranch = ((SMLReport._report._columnDataListType)__getObject._columnList[10])._text;
                        __sumPay += MyLib._myGlobal._decimalPhase(((SMLReport._report._columnDataListType)__getObject._columnList[7])._text);
                        __sumTax += MyLib._myGlobal._decimalPhase(((SMLReport._report._columnDataListType)__getObject._columnList[8])._text);
                        //((SMLReport._report._columnDataListType)__getObject._columnList[9])._text = "1";
                    }
                }

                // if show by branch
                if (this._conditionScreen._useBranchCheckbox.Checked == true)
                {
                    if (this._getAccount.Tables.Count > 0)
                    {
                        DataRow[] __getBranchRow = this._getAccount.Tables[0].Select(_g.d.erp_branch_list._code + "=\'" + __currentBranch + "\'");

                        if (__getBranchRow.Length > 0)
                        {
                            string __branchNumber = (MyLib._myGlobal._intPhase(__getBranchRow[0][_g.d.erp_branch_list._number].ToString()) == 0) ? " สำนักงานใหญ่" : " สาขาที่ " + MyLib._myGlobal._intPhase(__getBranchRow[0][_g.d.erp_branch_list._number].ToString()).ToString("00000");
                            string __branchAddress = __getBranchRow[0][_g.d.erp_branch_list._address_1].ToString();


                            for (int loop = 0; loop < pageListType._objectList.Count; loop++)
                            {
                                SMLReport._report._objectListType __getObject = (SMLReport._report._objectListType)pageListType._objectList[loop];

                                if (__getObject._type == SMLReport._report._objectType.Header)
                                {
                                    ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[0])._cellList[4])._text = ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno))._str + "\t: " + MyLib._myGlobal._ltdTax + __branchNumber;
                                    ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[0])._cellList[5])._text = "ที่อยู่\t: " + __branchAddress.Replace("\n", "");
                                }
                            }
                        }
                    }
                }

                // set footer value
                for (int loop = 0; loop < pageListType._objectList.Count; loop++)
                {
                    SMLReport._report._objectListType __getObject = (SMLReport._report._objectListType)pageListType._objectList[loop];

                    if (__getObject._type == SMLReport._report._objectType.PageFooter)
                    {
                        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
                        ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[2])._cellList[0])._text = string.Format(__formatNumber, __sumPay);//  __sumPay.ToString();
                        ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[3])._cellList[0])._text = string.Format(__formatNumber, __sumTax); //  __sumTax.ToString();
                    }
                }

            }
        }

        private void _view1__getTotalCurrentRow(ArrayList source, ArrayList objectList)
        {
            if (this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3 || this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53)
            {
                decimal __sumPay = 0M;
                decimal __sumTax = 0M;

                // sum data 
                //SMLReport._report._pageListType __pageList = (SMLReport._report._pageListType)source;
                for (int loop = 0; loop < source.Count; loop++)
                {
                    SMLReport._report._objectListType __getObject = (SMLReport._report._objectListType)source[loop];

                    if (__getObject._isDataRow == true)
                    {
                        __sumPay += MyLib._myGlobal._decimalPhase(((SMLReport._report._columnDataListType)__getObject._columnList[7])._text);
                        __sumTax += MyLib._myGlobal._decimalPhase(((SMLReport._report._columnDataListType)__getObject._columnList[8])._text);
                    }
                }


                // find total column
                /* for (int __loop = 0; __loop < objectList.Count; __loop++)
                 {
                     SMLReport._report._objectListType __getObject = (SMLReport._report._objectListType)objectList[__loop];
                     if (__getObject._type == SMLReport._report._objectType.PageFooter)
                     {
                         string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());

                         ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[2])._cellList[0])._text = string.Format(__formatNumber, __sumPay);//  __sumPay.ToString();
                         ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[3])._cellList[0])._text = string.Format(__formatNumber, __sumTax); //  __sumTax.ToString();

                     }
                 }*/

                for (int loop = 0; loop < source.Count; loop++)
                {
                    SMLReport._report._objectListType __getObject = (SMLReport._report._objectListType)source[loop];

                    if (__getObject._type == SMLReport._report._objectType.PageFooter)
                    {
                        /*__sumPay += MyLib._myGlobal._decimalPhase(((SMLReport._report._columnDataListType)__getObject._columnList[7])._text);
                        __sumTax += MyLib._myGlobal._decimalPhase(((SMLReport._report._columnDataListType)__getObject._columnList[8])._text);*/
                        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
                        ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[2])._cellList[0])._text = string.Format(__formatNumber, __sumPay);//  __sumPay.ToString();
                        ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[3])._cellList[0])._text = string.Format(__formatNumber, __sumTax); //  __sumTax.ToString();

                    }
                }
            }
        }

        private void _view1__loadDataByThread()
        {
            //throw new NotImplementedException();
            try
            {
                switch (this._mode)
                {
                    #region หัก ณ ที่จ่าย
                    case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                    case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:
                        {


                            string __getCustName = _g.d.gl_wht_list_detail._cust_code;
                            string __getCustAddress = "";
                            string __getCustTaxNo = "";
                            string __getCustStatus = "";
                            string __transFlag = "";
                            string __subWhere = " and gl_wht_list_detail.cust_code is not null";
                            string __transFlagSale = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "," +
                                       _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + "," +
                                       _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString() + "," +
                                       _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString() + "," +
                                       _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString() + "," +
                                       _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString() + "," +
                                       _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString() + "," +
                                       _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีถูกหักณที่จ่าย).ToString();
                            string __transFlagBuy = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString() + "," +
                                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString() + "," +
                                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString() + "," +
                                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString() + "," +
                                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() + "," +
                                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.GL_ภาษีหักณที่จ่าย).ToString();

                            __getCustName = "coalesce(" +
                                "(" + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._cust_name + " )" +
                                "," +
                                " (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ")) ";
                            __transFlag = __transFlagBuy;
                            __getCustAddress = " case when coalesce(" + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._cust_address + ", '') = '' then (select " + _g.d.ap_supplier._address + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ") else " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._cust_address + " end ";
                            __getCustTaxNo = "coalesce((" + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_number + "), (select " + _g.d.ap_supplier_detail._tax_id + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + "))";
                            __getCustStatus = "coalesce(( " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._cust_tax_type + "), (select " + _g.d.ap_supplier._ap_status + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + "))";

                            if (this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3)
                            {
                                __subWhere += " and " + __getCustStatus + "=0 ";
                            }
                            else
                            {
                                __subWhere += " and " + __getCustStatus + "=1 ";
                            }
                            /*     break;
                             case _whtConditionType.ถูกหักณที่จ่าย:
                                 __getCustName = "coalesce((select " + _g.d.gl_wht_list._cust_name + " || case when cust_code = '' then '' else '(' || cust_code || ')' end from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ") ||'('||" + _g.d.gl_wht_list_detail._cust_code + "||')' ) ";
                                 __transFlag = __transFlagSale;
                                 __getCustAddress = "coalesce((select " + _g.d.gl_wht_list._cust_address + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer._address + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ")) ";
                                 __getCustTaxNo = "coalesce((select " + _g.d.gl_wht_list._tax_number + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ")) ";
                                 __getCustStatus = "coalesce((select " + _g.d.gl_wht_list._cust_tax_type + " from " + _g.d.gl_wht_list._table + " where " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._tax_doc_no + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no + " and " + _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._trans_flag + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag + "), (select " + _g.d.ar_customer._ar_status + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._cust_code + ")) ";
                                 break;*/
                            // Qurey Where
                            string __vatmonth = ((MyLib._myComboBox)this._conditionScreen._screen._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex.ToString();
                            string __vatyear = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_year).ToString();
                            string __begindate = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._date_begin);
                            string __enddate = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._date_end);
                            StringBuilder __Qureywhere = new StringBuilder();

                            if (__begindate.Length == 0 && __enddate.Length == 0)
                            {
                                //string __day = GetDaysInMonth(MyLib._myGlobal._intPhase(__vatmonth) + 1, MyLib._myGlobal._intPhase(__vatyear)).ToString();

                                int __month = MyLib._myGlobal._intPhase(__vatmonth) + 1;
                                int __year = MyLib._myGlobal._intPhase(__vatyear);
                                if (__year > 2400)
                                    __year -= 543;


                                DateTime __dateBegin = new DateTime(__year, __month, 1);
                                DateTime __dateEnd = new DateTime(__year, __month, 1).AddMonths(1).AddDays(-1);

                                __Qureywhere.Append(String.Format(" ({0} in (" + __transFlag + ")) and ({1} between \'{2}\' and \'{3}\')",
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._due_date,
                                __dateBegin.ToString("yyyy-MM-dd", new CultureInfo("en-US")),
                                __dateEnd.ToString("yyyy-MM-dd", new CultureInfo("en-US"))));
                            }
                            else
                            {
                                DateTime __dateBegin = this._conditionScreen._screen._getDataDate(_g.d.resource_report_vat._date_begin);
                                DateTime __dateEnd = this._conditionScreen._screen._getDataDate(_g.d.resource_report_vat._date_end);

                                if (__dateBegin.Year > 2500)
                                {
                                    __dateBegin = __dateBegin.AddYears(-543);
                                    __dateEnd = __dateEnd.AddYears(-543);
                                }
                                string __datebeginQuery = MyLib._myGlobal._convertDateToQuery(__dateBegin);
                                string __dateEndQuery = MyLib._myGlobal._convertDateToQuery(__dateEnd);

                                __Qureywhere.Append(String.Format(" ({0} in (" + __transFlag + ")) and ({1} between \'{2}\' and \'{3}\')",
                                    _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag,
                                    _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._due_date,
                                    __datebeginQuery,
                                    __dateEndQuery));
                            }

                            string __whereDocCancel = " and coalesce(case when gl_wht_list_detail.trans_flag not in (239,19) then(select last_status from ic_trans where ic_trans.doc_no = gl_wht_list_detail.doc_no and trans_flag = gl_wht_list_detail.trans_flag) else (select last_status from ap_ar_trans where ap_ar_trans.doc_no = gl_wht_list_detail.doc_no and ap_ar_trans.trans_flag = gl_wht_list_detail.trans_flag ) end, 0) = 0";

                            // int __sortByIndex = (int)((MyLib._myComboBox)this._conditionScreen._screen._getControl(_g.d.resource_report_vat._sort_by)).SelectedIndex;
                            string __orderBy = _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._doc_date;

                            string __query = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}," +
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_doc_no +
                                ", (select branch_code from ap_supplier_detail where ap_supplier_detail.ap_code=gl_wht_list_detail.cust_code) as " + _g.d.ap_supplier_detail._branch_code +
                                ", case when gl_wht_list_detail.trans_flag not in (239,19) then (select branch_code from ic_trans where ic_trans.doc_no = gl_wht_list_detail.doc_no and trans_flag = gl_wht_list_detail.trans_flag ) else (select branch_code from ap_ar_trans where ap_ar_trans.doc_no = gl_wht_list_detail.doc_no and ap_ar_trans.trans_flag = gl_wht_list_detail.trans_flag ) end as branch_code_filter " +
                                " from " + _g.d.gl_wht_list_detail._table + ", " + _g.d.gl_wht_list._table +
                                " where gl_wht_list_detail.tax_doc_no = gl_wht_list.tax_doc_no and gl_wht_list.trans_flag=gl_wht_list_detail.trans_flag  " + ((__Qureywhere.Length > 0) ? " and " + __Qureywhere : "") + __subWhere + __whereDocCancel +
                                " order by " + __orderBy,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._due_date,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._income_type,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_rate,
                                __getCustName + " as " + _g.d.gl_wht_list_detail._cust_code,
                                __getCustAddress + " as " + _g.d.resource_report_vat._cust_address,
                                __getCustTaxNo + " as " + _g.d.resource_report_vat._cust_taxno,
                                __getCustStatus + " as " + _g.d.resource_report_vat._cust_status,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._amount,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._tax_value,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._doc_no,
                                _g.d.gl_wht_list._table + "." + _g.d.gl_wht_list._doc_date,
                                _g.d.gl_wht_list_detail._table + "." + _g.d.gl_wht_list_detail._trans_flag);
                            //
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            this._dataTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];

                            // branch
                            if (this._conditionScreen._useBranchCheckbox.Checked == true)
                            {
                                __query = "select * from " + _g.d.erp_branch_list._table + " order by " + _g.d.erp_branch_list._code;
                                _getAccount = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                            }


                        }
                        break;
                    #endregion

                    case _singhaReportEnum.การ์ดสินค้า:
                        {
                            StringBuilder __queryDetail = new StringBuilder();
                            string __from_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date));
                            string __to_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date));
                            string __itemCodeWhere = this._conditionScreen._screen._getDataStr(_g.d.resource_report._item_code);

                            string __itemCodeWhereList = MyLib._myUtil._genCodeList("ic_code", __itemCodeWhere);


                            string __whShelfCodeWhere = this._conditionScreen._selectWarehouseAndLocation._wareHouseLocationSelected("wh_code", "shelf_code");

                            string __balanceStockQuery = "select ic_code, doc_type, null as doc_date, \'\' as  doc_no, trans_flag, wh_code, shelf_code, qty_in, qty_out, balance_qty from ( " +
                                "select item_code as ic_code, 0 as doc_type, 0 as trans_flag, wh_code, shelf_code" +
                                ", 0 as qty_in, 0 as qty_out" +
                                ", coalesce(sum(calc_flag * (case when((trans_flag in (66, 68, 70, 54, 60, 58, 310, 12) or(trans_flag = 14 and inquiry_type = 0) or(trans_flag = 48 and inquiry_type < 2)) or(trans_flag in (56, 72, 44) or(trans_flag = 46 and inquiry_type = 1) or(trans_flag = 16 and inquiry_type in (0, 2)) or(trans_flag = 311 and inquiry_type = 0))) then qty*(stand_value / divide_value) else 0 end)),0) as balance_qty" +
                                " from ic_trans_detail " +
                                " where last_status = 0 and item_type<> 3 and item_type<> 5 and doc_date_calc < \'" + __from_date + "\' " +
                                ((__whShelfCodeWhere.Length > 0) ? " and (" + __whShelfCodeWhere + ")" : "") +
                                " group by item_code, wh_code, shelf_code " +
                                " ) as b where balance_qty <> 0 ";

                            string __movementStockQuery = "select ic_code, doc_type, doc_date, doc_no, trans_flag, wh_code, shelf_code " +
                                ", case when(calc_flag = 1 and qty > 0) then qty*(stand_value / divide_value) else 0 end as qty_in " +
                                ", case when(calc_flag = -1 or trans_flag = 66 and qty < 0) then qty*(stand_value / divide_value) else 0 end as qty_out " +
                                ", 0 as balance_qty " +
                                " from ( " +
                                " select 1 as doc_type, item_code as ic_code, doc_date, doc_no, trans_flag, calc_flag, qty, stand_value, divide_value, wh_code, shelf_code " +
                                " from ic_trans_detail " +
                                " where last_status = 0 " +
                                " and((trans_flag in (66, 68, 70, 54, 60, 58, 310, 12) or(trans_flag = 14 and inquiry_type = 0) or(trans_flag = 48 and inquiry_type < 2)) or(trans_flag in (56, 72, 44) or(trans_flag = 46 and inquiry_type = 1) or(trans_flag = 16 and inquiry_type in (0, 2)) or(trans_flag = 311 and inquiry_type = 0)) ) " +
                                " and doc_date_calc>= \'" + __from_date + "\'  and doc_date_calc<= \'" + __to_date + "\' " +
                                ((__whShelfCodeWhere.Length > 0) ? " and (" + __whShelfCodeWhere + ")" : "") +
                                " ) as temp1 ";


                            __queryDetail.Append("select ic_code, doc_type, doc_date, doc_no, trans_flag, trans_flag(trans_flag) as trans_name, wh_code, shelf_code, qty_in, qty_out, balance_qty, ( select name_1 from ic_warehouse where ic_warehouse.code = net.wh_code) wh_name," +
                                " (select name_1 from ic_shelf where ic_shelf.code = net.shelf_code and ic_shelf.whcode = net.wh_code) as shelf_name, (select name_1 from ic_inventory where ic_inventory.code = net.ic_code) as item_name" +
                                ", (select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code = net.ic_code order by row_order desc limit 1) as ratio_max " +
                                ", (select stand_value / divide_value from ic_unit_use where ic_unit_use.ic_code = net.ic_code order by row_order limit 1) as ratio_min " +
                                ", (select name_1 from ic_unit where ic_unit.code = (select code from ic_unit_use where ic_unit_use.ic_code = net.ic_code order by row_order desc limit 1)) as unit_max " +
                                ", (select name_1 from ic_unit where ic_unit.code = (select code from ic_unit_use where ic_unit_use.ic_code = net.ic_code order by row_order limit 1)) as unit_min " +
                                " from ( ");
                            __queryDetail.Append(__balanceStockQuery + " union all " + __movementStockQuery);
                            __queryDetail.Append(" ) as net " + ((__itemCodeWhereList.Length > 0) ? " where " + __itemCodeWhereList : "") + " order by wh_code, shelf_code, ic_code, doc_type, doc_date ");

                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            this._dataDetailTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __queryDetail.ToString()).Tables[0];

                            this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataDetailTable, "wh_code,shelf_code,ic_code,wh_name,shelf_name,item_name,ratio_max,ratio_min,unit_max,unit_min");
                        }
                        break;
                    case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                        {
                            string __toDate = this._conditionScreen._screen._getDataStrQuery(_g.d.ic_resource._date_end);
                            string __whCode = this._conditionScreen._screen._getDataStrQuery(_g.d.ic_resource._warehouse);
                            string __shelfCode = this._conditionScreen._screen._getDataStrQuery(_g.d.ic_resource._location);
                            StringBuilder __queryMain = new StringBuilder();

                            __queryMain.Append("select '' as " + _g.d.ic_resource._balance_qty + "," + _g.d.ic_shelf._code + " as " + _g.d.ic_resource._location + "," + _g.d.ic_shelf._whcode + " as " + _g.d.ic_resource._warehouse +
                                "," + _g.d.ic_shelf._name_1 +
                                //",(select name_1 from ic_warehouse where ic_warehouse.code = ic_shelf.whcode ) as whname " + 
                                " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._code + "=" + __shelfCode + " and " + _g.d.ic_shelf._whcode + "=" + __whCode + "");


                            SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                            string __query = "select ic_code,warehouse,location, ic_name " +
                                ", (case when count_unit = 1 then balance_qty  else trunc(balance_qty/ratio_max) end) as " + _g.d.ic_resource._big_qty +
                                ", (case when count_unit = 1 then 0 else mod(balance_qty,ratio_max) end) as " + _g.d.ic_resource._small_qty +
                                ", 0 as " + _g.d.ic_resource._sale +
                                ", 0 as " + _g.d.ic_resource._premium +
                                ", 0 as " + _g.d.ic_resource._sale_return +
                                ", 0 as " + _g.d.ic_resource._balance_qty +
                                " from( " +
                                " select ic_code, warehouse, location, ic_name, balance_qty " +
                                " ,(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code = temp1.ic_code order by row_order desc limit 1) as ratio_max " +
                                " , (select count(code) from ic_unit_use where ic_unit_use.ic_code = temp1.ic_code) as count_unit " +
                                "  from( " +
                                " select item_code as ic_code, wh_code as warehouse, shelf_code as location " +
                                " ,(select name_1 from ic_inventory where ic_inventory.code = item_code) as ic_name " +
                                " ,coalesce(sum(calc_flag * (case when((trans_flag in (70, 54, 60, 58, 310, 12) or(trans_flag = 66 and qty > 0) or(trans_flag = 14 and inquiry_type = 0) or(trans_flag = 48 and inquiry_type < 2)) or(trans_flag in (56, 68, 72, 44) or(trans_flag = 66 and qty < 0) or(trans_flag = 46 and inquiry_type in (0, 2))  or(trans_flag = 16 and inquiry_type in (0, 2)) or(trans_flag = 311 and inquiry_type = 0)) and not (((select coalesce(ic_trans.doc_ref, '') from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag and ic_trans.is_pos = 1 ) <> '' ) and((select coalesce(ic_trans.is_pos, 0) from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) = 1))) then qty*(stand_value / divide_value) else 0 end)),0) as balance_qty " +
                                " from  ic_trans_detail " +
                                " where ic_trans_detail.last_status = 0 and ic_trans_detail.item_type <> 5 " +
                                " and(select item_type from ic_inventory where ic_inventory.code = ic_trans_detail.item_code) not in (1,3)  " +
                                " and doc_date_calc<= " + __toDate + "" +
                                " and ic_trans_detail.wh_code in (" + __whCode + ") " +
                                " and ic_trans_detail.shelf_code in (" + __shelfCode + ") group by item_code, wh_code, shelf_code " +
                                " ) as temp1 " +
                                " ) as temp2 " +
                                "  where(balance_qty <> 0) order by coalesce((select line_number from ic_report_sort_item where ic_report_sort_item.ic_code = temp2.ic_code), 9999), ic_code";

                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            StringBuilder __queryStr = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryMain.ToString()));
                            __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                            __queryStr.Append("</node>");

                            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr.ToString());
                            if (__result.Count > 0)
                            {
                                this._dataTable = ((DataSet)__result[0]).Tables[0];
                                this._dataDetailTable = ((DataSet)__result[1]).Tables[0];
                            }

                            // MyLib._dataTableExtension._selectDistinct(this._dataDetailTable, "wh_code,shelf_code,ic_code,wh_name,shelf_name,item_name,ratio_max,ratio_min,unit_max,unit_min");

                        }
                        break;

                    case _singhaReportEnum.รายงานยอดขายรายเดือนตามกลุ่มสินค้า_เทียบปี:
                        {
                            int __year = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report._year)) - MyLib._myGlobal._year_add;
                            decimal __monthname = this._conditionScreen._screen._getDataNumber(_g.d.resource_report._monthly) + 1;

                            string __querySaleExtract = "(select sum(sum_amount)from ic_trans_detail where trans_flag = 44 and last_status = 0 and extract(year from doc_date) = '{0}' and extract(month from doc_date) = '{1}' {2} )";
                            string __productGroupFilter = " and exists (select code from ic_inventory where ic_inventory.code = ic_trans_detail.item_code and ic_inventory.item_brand = ic_brand.code ) ";


                            StringBuilder __query1 = new StringBuilder(" select code, name_1" +
                                ", coalesce(" + string.Format(__querySaleExtract, __year, __monthname.ToString(), __productGroupFilter) + ", 0) as sale_1" +
                                ", coalesce(" + string.Format(__querySaleExtract, __year, __monthname.ToString(), "") + ", 0) as total_sale_1" +
                                ", coalesce(" + string.Format(__querySaleExtract, (__year - 1).ToString(), __monthname.ToString(), __productGroupFilter) + ", 0) as sale_0" +
                                ", coalesce(" + string.Format(__querySaleExtract, (__year - 1).ToString(), __monthname.ToString(), "") + ", 0) as total_sale_0" +
                                " from ic_brand order by code ");

                            StringBuilder __query = new StringBuilder("select code, name_1, sale_0, case when (total_sale_0 = 0) then 0 else ((sale_0 / total_sale_0)*100) end as sale_0_percent, sale_1, case when (total_sale_1 = 0) then 0 else ((sale_1 / total_sale_1)*100) end as sale_1_percent from (" + __query1.ToString() + ") as net");

                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            this._dataTable = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];

                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this._view1._loadDataByThreadSuccess = false;
                return;

            }
            this._view1._loadDataByThreadSuccess = true;
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            if (_conditionScreen.DialogResult == DialogResult.Yes)
            {
                // start process
                this._build();
            }
        }

        void _build()
        {
            if (this._mode == _singhaReportEnum.GL_ข้อมูลรายวัน)
            {
                _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screen._getDataStr(_g.d.gl_resource._date_begin));
                _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screen._getDataStr(_g.d.gl_resource._date_end));
                _conditionDocBegin = this._conditionScreen._screen._getDataStr(_g.d.gl_resource._doc_begin);
                _conditionDocEnd = this._conditionScreen._screen._getDataStr(_g.d.gl_resource._doc_end);
                _conditionTotalByDate = this._conditionScreen._screen._getDataStr(_g.d.gl_resource._total_end_date).Equals("1") ? true : false;
                _conditionSortBy = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.gl_resource._sort_by));//1=วันที่,2=เอกสาร
                this._getJournal = null; // จะได้ load data ใหม่
                this._getJournalDetail = null;
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
            else if (this._mode == _singhaReportEnum.การ์ดสินค้า)
            {
                _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screen._getDataStr(_g.d.resource_report._from_date));
                _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screen._getDataStr(_g.d.resource_report._to_date));
                this._getAccount = null;
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
            else
            {
                this._getAccount = null;
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }

        }

        void _view1__getDataObject()
        {
            switch (this._mode)
            {
                #region สมุดบัญชีแยกประเภทเงินสด

                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    {
                        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
                        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");

                        int __accountCode = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._code);
                        int __accountName1 = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._name_1);
                        int __accountName2 = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._name_2);
                        //
                        int __jounalDocDate = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_date);
                        int __jounalDocNo = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_doc_no);
                        int __jounalDescription = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_description);
                        int __jounalBook = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._book_code);
                        int __journalPeriod = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._period_number);
                        int __jounalDebit = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._debit);
                        int __jounalCredit = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._credit);
                        int __jounalBalance = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._balance);

                        DateTime _conditionDateBegin = this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date);
                        DateTime _conditionDateEnd = this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date);

                        Boolean _conditionTotalByDate = true;
                        Boolean _conditionAllAccount = false;
                        //
                        SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__accountObject._columnList[0];
                        //this._view1._reportProgressBar.Value = 0;
                        int __totalRecords = _getAccount.Tables[0].Rows.Count;
                        for (int __accountRow = 0; __accountRow < __totalRecords; __accountRow++)
                        {
                            string __getAccountCode = _getAccount.Tables[0].Rows[__accountRow].ItemArray[0].ToString();
                            string __getAccountName1 = _getAccount.Tables[0].Rows[__accountRow].ItemArray[1].ToString();
                            string __getAccountName2 = _getAccount.Tables[0].Rows[__accountRow].ItemArray[2].ToString();

                            decimal __sumDebetAccount = 0;
                            decimal __sumCreditAccount = 0;

                            if (this._conditionScreen._useBranchCheckbox.Checked == true)
                            {
                                for (int __branchRow = 0; __branchRow < this._conditionScreen._gridCondition._rowData.Count; __branchRow++)
                                {
                                    if (this._conditionScreen._gridCondition._cellGet(__branchRow, 0).ToString().Equals("1"))
                                    {
                                        string __branchCode = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._code).ToString();
                                        string __branchName = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._name_1).ToString();

                                        this._view1._progessBarValue = (__accountRow + 1) * 100 / __totalRecords;
                                        this._view1._processMessage = __getAccountCode + "/" + __getAccountName1 + "/" + __getAccountName2;

                                        string __branchWhere = _g.d.gl_journal_detail._branch_code + "= \'" + __branchCode + "\'";

                                        string __docTypeFilter = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._doc_type).ToString();
                                        if (__docTypeFilter.Length > 0)
                                        {
                                            __branchWhere += " and " + "(case when trans_flag in (239,19) then (select doc_format_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_detail.doc_no and ap_ar_trans.trans_flag=gl_journal_detail.trans_flag) else (select doc_format_code from ic_trans where ic_trans.doc_no = gl_journal_detail.doc_no and ic_trans.trans_flag=gl_journal_detail.trans_flag) end )=\'" + __docTypeFilter + "\'";
                                        }

                                        //
                                        // คำนวณ และพิมพ์รายละเอียด
                                        SMLProcess._glProcess __process = new SMLProcess._glProcess();
                                        ArrayList __result = __process._glViewJounalDetail(__getAccountCode, _conditionDateBegin, _conditionDateEnd, _conditionTotalByDate, 0, __branchWhere, true);
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
                                            _view1._addDataColumn(__accountObject, __accountDataObject, __accountCode, "รหัสบัญชี " + __getAccountCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__accountObject, __accountDataObject, __accountName1, "ชื่อบัญชี " + __getAccountName1, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
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
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                    /*                        int __newRow = this._listGrid._addRow();
                                                                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                                                                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance, __getRow._balance, false);*/
                                                }
                                                else if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal)
                                                {
                                                    __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

                                                }
                                                else
                                                {
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, MyLib._myGlobal._convertDateToString(__getRow._date, false, true), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, __getRow._number, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, __getRow._bookCode, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, __getRow._period_number.ToString(), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

                                                    __sumDebetAccount += __getRow._debit;
                                                    __sumCreditAccount += __getRow._credit;
                                                }
                                            }
                                        }
                                    }
                                }

                                // print total all branch in account

                                if (__sumDebetAccount > 0 || __sumCreditAccount > 0)
                                {
                                    SMLReport._report._objectListType __journalTotalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__jounalListObject, __journalTotalObject);

                                    Font __totalFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __jounalDocDate, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __jounalDocNo, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __jounalBook, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __journalPeriod, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __jounalDescription, "รวม " + __getAccountCode, __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __jounalDebit, MyLib._myUtil._moneyFormat(__sumDebetAccount, _formatNumber), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __jounalCredit, MyLib._myUtil._moneyFormat(__sumCreditAccount, _formatNumber), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__jounalListObject, __journalTotalObject, __jounalBalance, "", __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

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

                                string __docTypeWhere = "";
                                string __docTypeFilter = this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._doc_type).ToString();
                                if (__docTypeFilter.Length > 0)
                                {
                                    __docTypeWhere += "(case when trans_flag in (239,19) then (select doc_format_code from ap_ar_trans where ap_ar_trans.doc_no = gl_journal_detail.doc_no and ap_ar_trans.trans_flag=gl_journal_detail.trans_flag) else (select doc_format_code from ic_trans where ic_trans.doc_no = gl_journal_detail.doc_no and ic_trans.trans_flag=gl_journal_detail.trans_flag) end)=\'" + __docTypeFilter + "\'";
                                }
                                //
                                // คำนวณ และพิมพ์รายละเอียด
                                SMLProcess._glProcess __process = new SMLProcess._glProcess();
                                ArrayList __result = __process._glViewJounalDetail(__getAccountCode, _conditionDateBegin, _conditionDateEnd, _conditionTotalByDate, 0, __docTypeWhere, true);
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
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
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
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        }
                                        else
                                        {
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, MyLib._myGlobal._convertDateToString(__getRow._date, false, true), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, __getRow._number, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, __getRow._bookCode, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, __getRow._period_number.ToString(), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    break;

                #endregion

                #region  GL_สมุดบัญชีแยกประเงินโอน

                case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                    {
                        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
                        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");

                        int __accountCode = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._code);
                        int __accountName1 = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._name_1);
                        int __accountName2 = _view1._findColumn(__accountObject, _g.d.gl_chart_of_account._name_2);
                        //
                        int __jounalDocDate = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_date);
                        int __jounalDocNo = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_doc_no);
                        int __jounalDescription = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._journal_description);
                        int __jounalBook = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._book_code);
                        int __journalPeriod = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._period_number);
                        int __jounalDebit = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._debit);
                        int __jounalCredit = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._credit);
                        int __jounalBalance = _view1._findColumn(__jounalListObject, _g.d.gl_list_view._balance);

                        DateTime _conditionDateBegin = this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date);
                        DateTime _conditionDateEnd = this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date);

                        Boolean _conditionTotalByDate = false;
                        Boolean _conditionAllAccount = false;
                        //
                        SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__accountObject._columnList[0];
                        //this._view1._reportProgressBar.Value = 0;
                        int __totalRecords = _getAccount.Tables[0].Rows.Count;
                        for (int __accountRow = 0; __accountRow < __totalRecords; __accountRow++)
                        {
                            string __getAccountCode = _getAccount.Tables[0].Rows[__accountRow].ItemArray[0].ToString();
                            string __getAccountName1 = _getAccount.Tables[0].Rows[__accountRow].ItemArray[1].ToString();
                            string __getAccountName2 = _getAccount.Tables[0].Rows[__accountRow].ItemArray[2].ToString();

                            if (this._conditionScreen._useBranchCheckbox.Checked == true)
                            {
                                for (int __branchRow = 0; __branchRow < this._conditionScreen._gridCondition._rowData.Count; __branchRow++)
                                {
                                    if (this._conditionScreen._gridCondition._cellGet(__branchRow, 0).ToString().Equals("1"))
                                    {
                                        string __branchCode = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._code).ToString();
                                        string __branchName = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._name_1).ToString();

                                        this._view1._progessBarValue = (__accountRow + 1) * 100 / __totalRecords;
                                        this._view1._processMessage = __getAccountCode + "/" + __getAccountName1 + "/" + __getAccountName2;

                                        string __branchWhere = _g.d.gl_journal_detail._branch_code + "= \'" + __branchCode + "\'";
                                        //
                                        // คำนวณ และพิมพ์รายละเอียด
                                        SMLProcess._glProcess __process = new SMLProcess._glProcess();
                                        ArrayList __result = __process._glViewJounalDetail(__getAccountCode, _conditionDateBegin, _conditionDateEnd, _conditionTotalByDate, 0, __branchWhere, false);
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
                                            _view1._addDataColumn(__accountObject, __accountDataObject, __accountCode, "รหัสบัญชี " + __getAccountCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__accountObject, __accountDataObject, __accountName1, "ชื่อบัญชี " + __getAccountName1, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
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
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                    /*                        int __newRow = this._listGrid._addRow();
                                                                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                                                                            this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance, __getRow._balance, false);*/
                                                }
                                                else if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal)
                                                {

                                                    // ยอดยกไป
                                                    __newFont = new Font(__getColumn._fontData, FontStyle.Bold);


                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, "ยอดสะสมยกไป", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, "", __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, "", __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

                                                    __journalDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.None);
                                                    _view1._createEmtryColumn(__jounalListObject, __journalDataObject);

                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);


                                                }
                                                else
                                                {
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, MyLib._myGlobal._convertDateToString(__getRow._date, false, true), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, __getRow._number, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, __getRow._bookCode, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, __getRow._period_number.ToString(), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                    _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
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
                                ArrayList __result = __process._glViewJounalDetail(__getAccountCode, _conditionDateBegin, _conditionDateEnd, _conditionTotalByDate, 0, "", false);
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
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            /*                        int __newRow = this._listGrid._addRow();
                                                                    this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._journal_description, __getRow._desc, false);
                                                                    this._listGrid._cellUpdate(__newRow, _g.d.gl_list_view._balance, __getRow._balance, false);*/
                                        }
                                        else if (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal)
                                        {

                                            //ยกไป
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, "ยอดสะสมยกไป", __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, "", __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, "", __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

                                            __journalDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.None);
                                            _view1._createEmtryColumn(__jounalListObject, __journalDataObject);

                                            __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, "", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, "", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        }
                                        else
                                        {
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocDate, MyLib._myGlobal._convertDateToString(__getRow._date, false, true), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDocNo, __getRow._number, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBook, __getRow._bookCode, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __journalPeriod, __getRow._period_number.ToString(), __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDescription, __getRow._desc, __getColumn._fontData, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalDebit, MyLib._myUtil._moneyFormat(__getRow._debit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalCredit, MyLib._myUtil._moneyFormat(__getRow._credit, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__jounalListObject, __journalDataObject, __jounalBalance, MyLib._myUtil._moneyFormat(__getRow._balance, _formatNumber), __getColumn._fontData, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    break;

                #endregion

                #region หัก ณ ที่จ่าย 3 53

                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:
                    {
                        if (_dataTable.Rows.Count > 0)
                        {


                            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
                            SMLReport._report._objectListType __dataObject = null;
                            string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
                            Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                            Font __totalFont = new Font(__getColumn._fontData, FontStyle.Bold);

                            if (this._conditionScreen._useBranchCheckbox.Checked == true)
                            {
                                // สาขา
                                for (int __branchRow = 0; __branchRow < this._conditionScreen._gridCondition._rowData.Count; __branchRow++)
                                {
                                    if (this._conditionScreen._gridCondition._cellGet(__branchRow, 0).ToString().Equals("1"))
                                    {
                                        string __branchCode = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._code).ToString();
                                        string __branchName = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._name_1).ToString();

                                        string __branchWhere = " branch_code_filter = \'" + __branchCode + "\'";

                                        {
                                            DataRow[] _dr = _dataTable.Select(__branchWhere);
                                            string __custCode = "";
                                            string __chekcustCode = "";
                                            //ใส่รวมล่างสุด
                                            decimal __sumamount = 0M;
                                            decimal __sumtax = 0M;
                                            //ใส่ค่าตามข้อมูล
                                            decimal __amount = 0M;
                                            decimal __tax = 0M;
                                            decimal __taxrate = 0M;
                                            //ใส่ผลรวมตามเจ้าหนี้ลูกหนี้
                                            decimal __totalamount = 0M;
                                            decimal __totaltax = 0M;

                                            int __custLength = 0;
                                            int __row = 1;
                                            for (int _row = 0; _row < _dr.Length; _row++)
                                            {
                                                {
                                                    //if (__custStatus == 0)
                                                    {
                                                        if (__chekcustCode.Equals(""))
                                                        {
                                                            __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                                        }
                                                        //ใส่จำนวนเงินเตรียม format
                                                        __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                                        __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                                        __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                                        //ใส่จำนวนเงินล่างเจ้าหนี้
                                                        __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();

                                                        //ชื่อเจ้าหนี้ // .Replace("\n", "")
                                                        __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);

                                                        // page break
                                                        if (_row == _dr.Length - 1)
                                                            __dataObject._pageBreak = true;

                                                        _view1._createEmtryColumn(__ojtReport, __dataObject);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 0, (__row).ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString() + "\n" + _dr[_row][_g.d.resource_report_vat._cust_address].ToString().Replace("\n", "") + "\n", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.ap_supplier_detail._branch_code].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 4, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format("{0:#%}", (__taxrate / 100)), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 9, "1", null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._cellType.String);
                                                        _view1._addDataColumn(__ojtReport, __dataObject, 10, __branchCode, null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, false, true);

                                                        //ที่อยู่ และรายละเอียด
                                                        __row++;

                                                        __totalamount = __amount + __totalamount;
                                                        __totaltax = __tax + __totaltax;

                                                        __custLength++;

                                                    }
                                                }


                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DataRow[] _dr = _dataTable.Select();
                                string __custCode = "";
                                string __chekcustCode = "";
                                //ใส่รวมล่างสุด
                                decimal __sumamount = 0M;
                                decimal __sumtax = 0M;
                                //ใส่ค่าตามข้อมูล
                                decimal __amount = 0M;
                                decimal __tax = 0M;
                                decimal __taxrate = 0M;
                                //ใส่ผลรวมตามเจ้าหนี้ลูกหนี้
                                decimal __totalamount = 0M;
                                decimal __totaltax = 0M;

                                int __custLength = 0;
                                int __row = 1;
                                for (int _row = 0; _row < _dr.Length; _row++)
                                {
                                    {
                                        //if (__custStatus == 0)
                                        {
                                            if (__chekcustCode.Equals(""))
                                            {
                                                __chekcustCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();
                                            }
                                            //ใส่จำนวนเงินเตรียม format
                                            __taxrate = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_rate].ToString());
                                            __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._amount].ToString());
                                            __tax = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString().Equals("") ? "0" : _dr[_row][_g.d.gl_wht_list_detail._tax_value].ToString());
                                            //ใส่จำนวนเงินล่างเจ้าหนี้
                                            __custCode = _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString();

                                            //ชื่อเจ้าหนี้ // .Replace("\n", "")
                                            __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            _view1._createEmtryColumn(__ojtReport, __dataObject);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 0, (__row).ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString() + "\n" + _dr[_row][_g.d.resource_report_vat._cust_address].ToString().Replace("\n", ""), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.ap_supplier_detail._branch_code].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 4, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 9, "1", null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__ojtReport, __dataObject, 10, "", null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._cellType.String);

                                            //ที่อยู่ และรายละเอียด
                                            __row++;

                                            __totalamount = __amount + __totalamount;
                                            __totaltax = __tax + __totaltax;

                                            __custLength++;

                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                #endregion

                #region ข้อมูลรายวัน

                case _singhaReportEnum.GL_ข้อมูลรายวัน:
                    {
                        int __docDate = _view1._findColumn(__ojtReport, _g.d.gl_journal._doc_date);
                        int __docNo = _view1._findColumn(__ojtReport, _g.d.gl_journal._doc_no);
                        //int __docRefDate = _view1._findColumn(__journalObject, _g.d.gl_journal._ref_date);
                        //int __docRefNo = _view1._findColumn(__journalObject, _g.d.gl_journal._ref_no);
                        int __docBook = _view1._findColumn(__ojtReport, _g.d.gl_journal._book_code);
                        int __docDescription = _view1._findColumn(__ojtReport, _g.d.gl_journal._description);
                        //int __docAmount = _view1._findColumn(__ojtReport, _g.d.gl_journal._debit);
                        //
                        int __detailAccountCode = _view1._findColumn(__jounalListObject, _g.d.gl_journal_detail._account_code);
                        int __detailAccountName = _view1._findColumn(__jounalListObject, _g.d.gl_journal_detail._account_name);
                        int __detailAccountDescription = _view1._findColumn(__jounalListObject, _g.d.gl_journal_detail._description);
                        int __detailDebit = _view1._findColumn(__jounalListObject, _g.d.gl_journal_detail._debit);
                        int __detailCredit = _view1._findColumn(__jounalListObject, _g.d.gl_journal_detail._credit);
                        //

                        // filter by branch
                        if (this._conditionScreen._useBranchCheckbox.Checked == true)
                        {
                            int __branchCountAll = 0;
                            for (int __branchRow = 0; __branchRow < this._conditionScreen._gridCondition._rowData.Count; __branchRow++)
                            {
                                if (this._conditionScreen._gridCondition._cellGet(__branchRow, 0).ToString().Equals("1"))
                                {
                                    __branchCountAll++;
                                }
                            }

                            #region Branch Filter

                            int __branchCountList = 0;
                            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
                            for (int __branchRow = 0; __branchRow < this._conditionScreen._gridCondition._rowData.Count; __branchRow++)
                            {
                                if (this._conditionScreen._gridCondition._cellGet(__branchRow, 0).ToString().Equals("1"))
                                {
                                    __branchCountList++;

                                    string __branchCode = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._code).ToString();
                                    string __branchName = this._conditionScreen._gridCondition._cellGet(__branchRow, _g.d.erp_branch_list._name_1).ToString();


                                    DataRow[] __journalBranchSelect = _getJournal.Tables[0].Select(_g.d.gl_journal._branch_code + "=\'" + __branchCode + "\'");

                                    if (__journalBranchSelect.Length > 0)
                                    {
                                        SMLReport._report._objectListType __dataBranchObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        _view1._createEmtryColumn(__ojtReport, __dataBranchObject);
                                        // เรียงตามเลขที่
                                        _view1._addDataColumn(__ojtReport, __dataBranchObject, __docDate, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataBranchObject, __docNo, __branchCode, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataBranchObject, __docBook, __branchName, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__ojtReport, __dataBranchObject, __docDescription, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);


                                        for (int __row = 0; __row < __journalBranchSelect.Length; __row++)
                                        {
                                            string __getDateStr = __journalBranchSelect[__row].ItemArray[0].ToString();
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
                                            DateTime __getRefDate = MyLib._myGlobal._convertDateFromQuery(__journalBranchSelect[__row].ItemArray[2].ToString());
                                            double __getAmount = Double.Parse(__journalBranchSelect[__row].ItemArray[6].ToString());

                                            double __getCreditDoc = Double.Parse(__journalBranchSelect[__row][_g.d.gl_journal._credit].ToString());
                                            double __getDebitDoc = Double.Parse(__journalBranchSelect[__row][_g.d.gl_journal._debit].ToString());

                                            Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                            string __getDocNo = __journalBranchSelect[__row].ItemArray[1].ToString();
                                            string __getBook = __journalBranchSelect[__row].ItemArray[7].ToString();
                                            //
                                            SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            _view1._createEmtryColumn(__ojtReport, __dataObject);
                                            if (_conditionSortBy == 1)
                                            {
                                                // เรียงตามวันที่
                                                _view1._addDataColumn(__ojtReport, __dataObject, __docDate, MyLib._myGlobal._convertDateToString(__getDate, false, true), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                _view1._addDataColumn(__ojtReport, __dataObject, __docNo, __getDocNo, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            }
                                            else
                                            {
                                                // เรียงตามเลขที่
                                                _view1._addDataColumn(__ojtReport, __dataObject, __docNo, __getDocNo, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                _view1._addDataColumn(__ojtReport, __dataObject, __docDate, MyLib._myGlobal._convertDateToString(__getDate, false, true), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            }
                                            _view1._addDataColumn(__ojtReport, __dataObject, __docBook, __journalBranchSelect[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__ojtReport, __dataObject, __docDescription, __journalBranchSelect[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            //
                                            // รวม
                                            _totalCountByDate++;
                                            // _totalAmountByDate += 

                                            _totalCount++;
                                            _totalAmount += __getAmount;


                                            this._totalDebitByDate += __getDebitDoc;
                                            this._totalCreditByDate += __getCreditDoc;

                                            this._totalDebitAmount += __getDebitDoc;
                                            this._totalCreditAmount += __getCreditDoc;

                                            //
                                            DataRow[] __getJournalDetail = _getJournalDetail.Tables[0].Select(_g.d.gl_journal_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_detail._doc_date + "=\'" + __getDateStr + "\' and " + _g.d.gl_journal_detail._book_code + "=\'" + __getBook + "\'");
                                            for (int __rowDetail = 0; __rowDetail < __getJournalDetail.Length; __rowDetail++)
                                            {
                                                SMLReport._report._objectListType __dataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.None);
                                                _view1._createEmtryColumn(__jounalListObject, __dataDetailObject);
                                                double __getDebit = Double.Parse(__getJournalDetail[__rowDetail].ItemArray[5].ToString());
                                                double __getCredit = Double.Parse(__getJournalDetail[__rowDetail].ItemArray[6].ToString());
                                                _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailAccountCode, __getJournalDetail[__rowDetail].ItemArray[2].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailAccountName, __getJournalDetail[__rowDetail].ItemArray[3].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailAccountDescription, __getJournalDetail[__rowDetail].ItemArray[4].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                                _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailDebit, (__getDebit == 0) ? "" : string.Format(_formatNumber, __getDebit), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                                _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailCredit, (__getCredit == 0) ? "" : string.Format(_formatNumber, __getCredit), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            }
                                        }

                                        _totalDateObject(__getColumn, (__branchCountList != __branchCountAll) ? true : false, __branchCode, __branchName);
                                    }
                                }
                            }
                            _totalObject(__getColumn);


                            #endregion

                        }
                        else
                        {
                            // normal

                            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
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

                                double __getCreditDoc = Double.Parse(_getJournal.Tables[0].Rows[__row][_g.d.gl_journal._credit].ToString());
                                double __getDebitDoc = Double.Parse(_getJournal.Tables[0].Rows[__row][_g.d.gl_journal._debit].ToString());

                                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                                string __getDocNo = _getJournal.Tables[0].Rows[__row].ItemArray[1].ToString();
                                string __getBook = _getJournal.Tables[0].Rows[__row].ItemArray[7].ToString();
                                //
                                SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__ojtReport, __dataObject);
                                if (_conditionSortBy == 1)
                                {
                                    // เรียงตามวันที่
                                    _view1._addDataColumn(__ojtReport, __dataObject, __docDate, MyLib._myGlobal._convertDateToString(__getDate, false, true), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, __docNo, __getDocNo, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                }
                                else
                                {
                                    // เรียงตามเลขที่
                                    _view1._addDataColumn(__ojtReport, __dataObject, __docNo, __getDocNo, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dataObject, __docDate, MyLib._myGlobal._convertDateToString(__getDate, false, true), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                }
                                _view1._addDataColumn(__ojtReport, __dataObject, __docBook, _getJournal.Tables[0].Rows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__ojtReport, __dataObject, __docDescription, _getJournal.Tables[0].Rows[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                //
                                // รวม
                                _totalCountByDate++;
                                // _totalAmountByDate += 

                                _totalCount++;
                                _totalAmount += __getAmount;


                                this._totalDebitByDate += __getDebitDoc;
                                this._totalCreditByDate += __getCreditDoc;

                                this._totalDebitAmount += __getDebitDoc;
                                this._totalCreditAmount += __getCreditDoc;

                                //
                                DataRow[] __getJournalDetail = _getJournalDetail.Tables[0].Select(_g.d.gl_journal_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_detail._doc_date + "=\'" + __getDateStr + "\' and " + _g.d.gl_journal_detail._book_code + "=\'" + __getBook + "\'");
                                for (int __rowDetail = 0; __rowDetail < __getJournalDetail.Length; __rowDetail++)
                                {
                                    SMLReport._report._objectListType __dataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__jounalListObject, __dataDetailObject);
                                    double __getDebit = Double.Parse(__getJournalDetail[__rowDetail].ItemArray[5].ToString());
                                    double __getCredit = Double.Parse(__getJournalDetail[__rowDetail].ItemArray[6].ToString());
                                    _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailAccountCode, __getJournalDetail[__rowDetail].ItemArray[2].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailAccountName, __getJournalDetail[__rowDetail].ItemArray[3].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailAccountDescription, __getJournalDetail[__rowDetail].ItemArray[4].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailDebit, (__getDebit == 0) ? "" : string.Format(_formatNumber, __getDebit), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                    _view1._addDataColumn(__jounalListObject, __dataDetailObject, __detailCredit, (__getCredit == 0) ? "" : string.Format(_formatNumber, __getCredit), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                }
                            }
                            _totalDateObject(__getColumn);
                            _totalObject(__getColumn);
                        }
                    }
                    break;
                #endregion


                #region การ์ดสินค้า

                case _singhaReportEnum.การ์ดสินค้า:
                    {
                        SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
                        DataTable __whCodeTable = MyLib._dataTableExtension._selectDistinct(this._dataTable, "wh_code,shelf_code,wh_name,shelf_name");

                        int __itemCodeColumn = _view1._findColumn(__ojtReport, _g.d.ic_resource._item_code);
                        int __itemNameColumn = _view1._findColumn(__ojtReport, _g.d.ic_resource._item_name);
                        int __inHeadColumn = _view1._findColumn(__ojtReport, _g.d.ic_resource._in);
                        int __outHeadColumn = _view1._findColumn(__ojtReport, _g.d.ic_resource._out);
                        int __balanceQtyHeadColumn = _view1._findColumn(__ojtReport, _g.d.ic_resource._balance_qty);

                        int __maxUnitCode = _view1._findColumn(__ojtReport, _g.d.ic_resource._many_unit);
                        int __minUnitCode = _view1._findColumn(__ojtReport, _g.d.ic_resource._single_unit);


                        // detail
                        int __docDateColumn = _view1._findColumn(__totalDateObject, _g.d.ic_resource._doc_date);
                        int __transFlagColumn = _view1._findColumn(__totalDateObject, _g.d.ic_resource._trans_flag);
                        int __docNoColumn = _view1._findColumn(__totalDateObject, _g.d.ic_resource._doc_no);
                        int __detailColumn = _view1._findColumn(__totalDateObject, _g.d.ic_resource._detail);

                        int __inBigColumn = _view1._findColumn(__totalDateObject, "in_max");
                        int __inSmallColumn = _view1._findColumn(__totalDateObject, "in_min");
                        int __outBigColumn = _view1._findColumn(__totalDateObject, "out_max");
                        int __outSmallColumn = _view1._findColumn(__totalDateObject, "out_min");
                        int __balanceBigColumn = _view1._findColumn(__totalDateObject, "balance_max");
                        int __balanceSmallColumn = _view1._findColumn(__totalDateObject, "balance_min");

                        for (int __row = 0; __row < __whCodeTable.Rows.Count; __row++)
                        {
                            string __whCode = __whCodeTable.Rows[__row]["wh_code"].ToString();
                            string __shelfCode = __whCodeTable.Rows[__row]["shelf_code"].ToString();


                            string __whName = __whCode + "~" + __whCodeTable.Rows[__row]["wh_name"].ToString();
                            string __shelfName = __shelfCode + "~" + __whCodeTable.Rows[__row]["shelf_name"].ToString();
                            // add wh_code row
                            SMLReport._report._objectListType __dataWarehouseObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__ojtReport, __dataWarehouseObject);
                            _view1._addDataColumn(__ojtReport, __dataWarehouseObject, __itemCodeColumn, __whName, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataWarehouseObject, __itemNameColumn, __shelfName, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                            _view1._addDataColumn(__ojtReport, __dataWarehouseObject, __maxUnitCode, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataWarehouseObject, __minUnitCode, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                            _view1._addDataColumn(__ojtReport, __dataWarehouseObject, __inHeadColumn, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataWarehouseObject, __outHeadColumn, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __dataWarehouseObject, __balanceQtyHeadColumn, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                            if (this._dataTable.Rows.Count > 0)
                            {
                                DataRow[] __itemInWHRow = this._dataTable.Select("wh_code =\'" + __whCode + "\' and shelf_code= \'" + __shelfCode + "\'");
                                for (int __itemRow = 0; __itemRow < __itemInWHRow.Length; __itemRow++)
                                {
                                    string __itemCode = __itemInWHRow[__itemRow]["ic_code"].ToString();
                                    string __itemName = __itemInWHRow[__itemRow]["item_name"].ToString();

                                    string __maxUnit = __itemInWHRow[__itemRow]["unit_max"].ToString();
                                    string __minUnit = __itemInWHRow[__itemRow]["unit_min"].ToString();

                                    int __ratioMax = MyLib._myGlobal._intPhase(__itemInWHRow[__itemRow]["ratio_max"].ToString());
                                    int __ratioSmall = MyLib._myGlobal._intPhase(__itemInWHRow[__itemRow]["ratio_min"].ToString());

                                    decimal __balanceBigQty = 0M;
                                    decimal __balanceSmallQty = 0M;

                                    // add item_code , item_name row
                                    SMLReport._report._objectListType __dateProductObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                    _view1._createEmtryColumn(__ojtReport, __dateProductObject);
                                    _view1._addDataColumn(__ojtReport, __dateProductObject, __itemCodeColumn, __itemCode, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dateProductObject, __itemNameColumn, __itemName, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                                    _view1._addDataColumn(__ojtReport, __dateProductObject, __maxUnitCode, __maxUnit, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dateProductObject, __minUnitCode, __minUnit, new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                                    _view1._addDataColumn(__ojtReport, __dateProductObject, __inHeadColumn, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dateProductObject, __outHeadColumn, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                    _view1._addDataColumn(__ojtReport, __dateProductObject, __balanceQtyHeadColumn, "", new Font(__getColumn._fontData, FontStyle.Bold), SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                                    if (this._dataDetailTable.Rows.Count > 0)
                                    {

                                        DataRow[] __dataRow = this._dataDetailTable.Select("wh_code =\'" + __whCode + "\' and shelf_code= \'" + __shelfCode + "\' and ic_code =\'" + __itemCode + "\' ");

                                        decimal __balanceQty = 0M;
                                        for (int __detailRow = 0; __detailRow < __dataRow.Length; __detailRow++)
                                        {
                                            int __docType = MyLib._myGlobal._intPhase(__dataRow[__detailRow]["doc_type"].ToString());
                                            decimal __qtyIn = MyLib._myGlobal._decimalPhase(__dataRow[__detailRow]["qty_in"].ToString());
                                            decimal __qtyOut = MyLib._myGlobal._decimalPhase(__dataRow[__detailRow]["qty_out"].ToString());
                                            string __docNo = __dataRow[__detailRow]["doc_no"].ToString();

                                            DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__dataRow[__detailRow]["doc_date"].ToString());


                                            string __docFlag = __dataRow[__detailRow]["trans_name"].ToString();
                                            string __detail = "";

                                            if (__docType == 0)
                                            {
                                                __balanceQty = MyLib._myGlobal._decimalPhase(__dataRow[__detailRow]["balance_qty"].ToString());
                                                __docFlag = "ยอดยกมา";
                                            }
                                            else
                                            {
                                                __balanceQty = (__balanceQty + __qtyIn) - __qtyOut;
                                            }


                                            __balanceBigQty = Math.Floor(__balanceQty / __ratioMax);
                                            __balanceSmallQty = __balanceQty % __ratioMax;

                                            decimal __inBigQty = Math.Floor(__qtyIn / __ratioMax);
                                            decimal __inSmallQty = __qtyIn % __ratioMax;

                                            decimal __outBigQty = Math.Floor(__qtyOut / __ratioMax);
                                            decimal __outSmallQty = __qtyOut % __ratioMax;

                                            SMLReport._report._objectListType __dataObect = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            _view1._createEmtryColumn(__totalDateObject, __dataObect);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __docDateColumn, MyLib._myGlobal._convertDateToString(__docDate, false, true), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __transFlagColumn, __docFlag, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __docNoColumn, __docNo, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __detailColumn, __detail, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                                            _view1._addDataColumn(__totalDateObject, __dataObect, __inBigColumn, __inBigQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __inSmallColumn, __inSmallQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __outBigColumn, __outBigQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __outSmallColumn, __outSmallQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __balanceBigColumn, __balanceBigQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                                            _view1._addDataColumn(__totalDateObject, __dataObect, __balanceSmallColumn, __balanceSmallQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

                                            // add dataRow

                                        }

                                        // total row

                                        __balanceBigQty = Math.Floor(__balanceQty / __ratioMax);
                                        __balanceSmallQty = __balanceQty % __ratioMax;

                                        SMLReport._report._objectListType __totalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                        _view1._createEmtryColumn(__totalDateObject, __totalObject);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __docDateColumn, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __transFlagColumn, "ยอดยกไป", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __docNoColumn, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __detailColumn, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                                        _view1._addDataColumn(__totalDateObject, __totalObject, __inBigColumn, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __inSmallColumn, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __outBigColumn, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __outSmallColumn, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __balanceBigColumn, __balanceBigQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                                        _view1._addDataColumn(__totalDateObject, __totalObject, __balanceSmallColumn, __balanceSmallQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);

                                        if (__itemRow == __itemInWHRow.Length - 1 && __row < __whCodeTable.Rows.Count - 1)
                                            __totalObject._pageBreak = true;
                                    }
                                }
                                // new page
                            }
                        }
                    }
                    break;
                #endregion

                case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {
                        SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];

                        if (this._dataTable.Rows.Count > 0)
                        {
                            int __whCodeColumnNumber = _view1._findColumn(__ojtReport, _g.d.ic_resource._warehouse);
                            int __locationColumnNumber = _view1._findColumn(__ojtReport, _g.d.ic_resource._location);
                            string __whCode = this._dataTable.Rows[0][_g.d.ic_resource._warehouse].ToString();
                            string __shelfCode = this._dataTable.Rows[0][_g.d.ic_resource._location].ToString();
                            string __shelfName = this._dataTable.Rows[0][_g.d.ic_shelf._name_1].ToString();

                            SMLReport._report._objectListType __warehouseObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__ojtReport, __warehouseObject);

                            _view1._addDataColumn(__ojtReport, __warehouseObject, __whCodeColumnNumber, __whCode, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.LeftRightBottom, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__ojtReport, __warehouseObject, __locationColumnNumber, __shelfCode + "~" + __shelfName, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.String);

                            DataRow[] __balanceItem = this._dataDetailTable.Select(_g.d.ic_resource._warehouse + "=\'" + __whCode + "\' and " + _g.d.ic_resource._location + "=\'" + __shelfCode + "\' ");

                            int __lineNumberColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._sort_order);
                            int __icCodeColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._ic_code);
                            int __icNameColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._ic_name);
                            int __bigQtyColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._big_qty);
                            int __smallQtyColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._small_qty);
                            int __saleColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._sale);
                            int __premiumColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._premium);
                            int __salereturnColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._sale_return);
                            int __balanceColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._balance_qty);
                            for (int __row = 0; __row < __balanceItem.Length; __row++)
                            {
                                int __no = __row + 1;
                                string __itemCode = __balanceItem[__row][_g.d.ic_resource._ic_code].ToString();
                                string __itemName = __balanceItem[__row][_g.d.ic_resource._ic_name].ToString();
                                decimal __bigQty = MyLib._myGlobal._decimalPhase(__balanceItem[__row][_g.d.ic_resource._big_qty].ToString());
                                decimal __smallQty = MyLib._myGlobal._decimalPhase(__balanceItem[__row][_g.d.ic_resource._small_qty].ToString());

                                SMLReport._report._objectListType __dataList = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);

                                if (__no % 20 == 0)
                                {
                                    __dataList._pageBreak = true;
                                }
                                _view1._createEmtryColumn(__totalDateObject, __dataList);


                                _view1._addDataColumn(__totalDateObject, __dataList, __lineNumberColumnNumber, __no.ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.LeftRightBottom, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__totalDateObject, __dataList, __icCodeColumnNumber, __itemCode, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.LeftRightBottom, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__totalDateObject, __dataList, __icNameColumnNumber, __itemName, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.String);

                                _view1._addDataColumn(__totalDateObject, __dataList, __bigQtyColumnNumber, __bigQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__totalDateObject, __dataList, __smallQtyColumnNumber, __smallQty.ToString("#,###,###"), null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.Number);

                                _view1._addDataColumn(__totalDateObject, __dataList, __saleColumnNumber, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__totalDateObject, __dataList, __premiumColumnNumber, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__totalDateObject, __dataList, __salereturnColumnNumber, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__totalDateObject, __dataList, __balanceColumnNumber, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.RightBottom, SMLReport._report._cellType.String);

                            }
                        }
                    }
                    break;

                case _singhaReportEnum.รายงานยอดขายรายเดือนตามกลุ่มสินค้า_เทียบปี:
                    {
                        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
                        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");

                        if (this._dataTable.Rows.Count > 0)
                        {

                            int __lineNumberColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._sort_order);
                            int __icCodeColumnNumber = _view1._findColumn(__totalDateObject, _g.d.ic_resource._ic_group);

                            int __yearColumnNumber = _view1._findColumn(__totalDateObject, "year_0_net");
                            int __yearPercentColumnNumber = _view1._findColumn(__totalDateObject, "year_0_percent");
                            int __yearOldColumnNumber = _view1._findColumn(__totalDateObject, "year_1_net");
                            int __yearOldPercentColumnNumber = _view1._findColumn(__totalDateObject, "year_1_percent");

                            decimal __totalYear = 0;
                            decimal __totalYearPercent = 0;
                            decimal __totalOldYear = 0;
                            decimal __totalOldYearPercent = 0;

                            for (int __row = 0; __row < _dataTable.Rows.Count; __row++)
                            {
                                int __no = __row + 1;
                                string __itemName = _dataTable.Rows[__row][_g.d.ic_group._name_1].ToString();

                                decimal __yearAmount = MyLib._myGlobal._decimalPhase(_dataTable.Rows[__row]["sale_0"].ToString());
                                decimal __yearPercent = MyLib._myGlobal._decimalPhase(_dataTable.Rows[__row]["sale_0_percent"].ToString());
                                decimal __yearOldAmount = MyLib._myGlobal._decimalPhase(_dataTable.Rows[__row]["sale_1"].ToString());
                                decimal __yearOldPercent = MyLib._myGlobal._decimalPhase(_dataTable.Rows[__row]["sale_1_percent"].ToString());

                                SMLReport._report._objectListType __dataList = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(__totalDateObject, __dataList);

                                _view1._addDataColumn(__totalDateObject, __dataList, __lineNumberColumnNumber, __no.ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(__totalDateObject, __dataList, __icCodeColumnNumber, __itemName, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                                _view1._addDataColumn(__totalDateObject, __dataList, __yearColumnNumber, __yearAmount.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__totalDateObject, __dataList, __yearPercentColumnNumber, __yearPercent.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                _view1._addDataColumn(__totalDateObject, __dataList, __yearOldColumnNumber, __yearOldAmount.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                                _view1._addDataColumn(__totalDateObject, __dataList, __yearOldPercentColumnNumber, __yearOldPercent.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);

                                __totalYear += __yearAmount;
                                __totalYearPercent += __yearPercent;
                                __totalOldYear += __yearOldAmount;
                                __totalOldYearPercent += __yearOldPercent;

                            }

                            // total

                            SMLReport._report._objectListType __totalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            _view1._createEmtryColumn(__totalDateObject, __totalObject);
                            _view1._addDataColumn(__totalDateObject, __totalObject, __lineNumberColumnNumber, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                            _view1._addDataColumn(__totalDateObject, __totalObject, __icCodeColumnNumber, "รวม", null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._cellType.String);

                            _view1._addDataColumn(__totalDateObject, __totalObject, __yearColumnNumber, __totalYear.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__totalDateObject, __totalObject, __yearPercentColumnNumber, __totalYearPercent.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__totalDateObject, __totalObject, __yearOldColumnNumber, __totalOldYear.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                            _view1._addDataColumn(__totalDateObject, __totalObject, __yearOldPercentColumnNumber, __totalOldYearPercent.ToString("#,###,##0.00"), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);

                        }
                    }
                    break;
            }
        }

        void _totalDateObject(SMLReport._report._columnListType getColumn)
        {
            _totalDateObject(getColumn, false, "", "");
        }

        void _totalDateObject(SMLReport._report._columnListType getColumn, bool pageBraek, string branchCode, string branchName)
        {
            if (_conditionTotalByDate == true && _conditionSortBy == 1 && _totalCountByDate > 0)
            {
                int __columnTotalDesc = _view1._findColumn(__totalDateObject, _totalDescriptionColumnName);
                int __columnTotalDebitAmount = _view1._findColumn(__totalDateObject, _columnTotalAmounDebit);
                int __columnTotalCreditAmount = _view1._findColumn(__totalDateObject, _columnTotalAmounCredit);
                //
                SMLReport._report._objectListType __totalDateNewObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 45, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__totalDateObject, __totalDateNewObject);
                Font __newFont = new Font(getColumn._fontData, FontStyle.Bold);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalDesc, "ยอดรวม" + ((branchCode.Length > 0) ? " " + branchName + " " : "") + "วันที่ " + MyLib._myGlobal._convertDateToString(_totalDate, false, false) + " จำนวน " + string.Format(_formatCountNumber, _totalCountByDate) + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalDebitAmount, string.Format(_formatNumber, _totalDebitAmount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalCreditAmount, string.Format(_formatNumber, _totalCreditAmount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);

                if (pageBraek)
                    __totalDateNewObject._pageBreak = true;
            }
            _totalCountByDate = 0;
            _totalAmountByDate = 0;

            //_totalDebitAmount = 0;
            //_totalCreditAmount = 0;
            this._totalDebitByDate = 0;
            this._totalCreditByDate = 0;
        }

        void _totalObject(SMLReport._report._columnListType getColumn)
        {
            int __columnTotalDesc = _view1._findColumn(__totalDateObject, _totalDescriptionColumnName);
            int __columnTotalDebitAmount = _view1._findColumn(__totalDateObject, _columnTotalAmounDebit);
            int __columnTotalCreditAmount = _view1._findColumn(__totalDateObject, _columnTotalAmounCredit);
            //
            if (_totalCount > 0)
            {
                SMLReport._report._objectListType __totalDateNewObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 45, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__totalDateObject, __totalDateNewObject);
                Font __newFont = new Font(getColumn._fontData, FontStyle.Bold);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalDesc, "ยอดรวมทั้งสิ้น  จำนวน " + string.Format(_formatCountNumber, _totalCount) + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalDebitAmount, string.Format(_formatNumber, _totalDebitAmount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                _view1._addDataColumn(__totalDateObject, __totalDateNewObject, __columnTotalCreditAmount, string.Format(_formatNumber, _totalCreditAmount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
            }
        }


        bool _view1__loadData()
        {
            switch (this._mode)
            {
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    {


                        if (_getAccount == null)
                        {
                            this.Cursor = Cursors.WaitCursor;

                            string __conditionAccountBegin = this._conditionScreen._screen._getDataStr(_g.d.resource_report._from_project_account_code);
                            string __conditionAccountEnd = this._conditionScreen._screen._getDataStr(_g.d.resource_report._to_project_account_code);

                            try
                            {
                                //
                                string __whereStr = " where ";
                                string __andStr = " and ";
                                StringBuilder __whereAccount = new StringBuilder();
                                //
                                __whereAccount.Append(__whereStr);
                                __whereAccount.Append(_g.d.gl_chart_of_account._status + "=0");

                                if (__conditionAccountBegin.Length != 0)
                                {
                                    if (__whereAccount.Length == 0)
                                    {
                                        __whereAccount.Append(__whereStr);
                                    }
                                    else
                                    {
                                        __whereAccount.Append(__andStr);
                                    }
                                    __whereAccount.Append(_g.d.gl_chart_of_account._code + ">=\'" + __conditionAccountBegin + "\'");
                                }
                                if (__conditionAccountEnd.Length != 0)
                                {
                                    if (__whereAccount.Length == 0)
                                    {
                                        __whereAccount.Append(__whereStr);
                                    }
                                    else
                                    {
                                        __whereAccount.Append(__andStr);
                                    }
                                    __whereAccount.Append(_g.d.gl_chart_of_account._code + "<=\'" + __conditionAccountEnd + "\'");
                                }
                                //
                                string __accountOrderBy = _g.d.gl_chart_of_account._code;
                                string __query = "select " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._name_2 +
                                    " from " + _g.d.gl_chart_of_account._table + __whereAccount.ToString() + " order by " + __accountOrderBy;
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                _getAccount = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                            }
                            catch
                            {
                                this.Cursor = Cursors.Default;
                                return false;
                            }
                        }

                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                    {

                    }
                    break;

                case _singhaReportEnum.GL_ข้อมูลรายวัน:
                    {
                        _totalCountByDate = 0;
                        _totalCount = 0;
                        _totalAmountByDate = 0;
                        _totalAmount = 0;

                        _totalCreditAmount = 0;
                        _totalDebitAmount = 0;

                        this._totalDebitByDate = 0;
                        this._totalCreditByDate = 0;

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
                            /*
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
                            }*/
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
                    break;

            }
            this.Cursor = Cursors.Default;
            return true;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {

            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);

                //_view1.__excelFlieName = "บัญชีแยกประเภท จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                switch (this._mode)
                {
                    case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                        {
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "สมุดบัญชีแยกประเภท (เงินสด)", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                            _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่ " + this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date).ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " + this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date).ToString("dd/MM/yyyy", new CultureInfo("th-TH")), SMLReport._report._cellAlign.Left, _view1._fontStandard);
                            //_view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                        }
                        return true;
                    case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                        {
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "สมุดบัญชีแยกประเภท (เงินโอน)", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                            _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่ " + this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date).ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " + this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date).ToString("dd/MM/yyyy", new CultureInfo("th-TH")), SMLReport._report._cellAlign.Left, _view1._fontStandard);
                            //_view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                        }
                        return true;
                    case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                        {
                            string __month = ((MyLib._myComboBox)this._conditionScreen._screen._getControl(_g.d.resource_report_vat._vat_month)).SelectedItem.ToString();
                            _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, "รายงานภาษีหัก ณ ที่จ่าย (ภ.ง.ด.3)", SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                            //this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._report_name), SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                            this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_month, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_month))._str + "  " + __month + "  " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_year, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_year))._str + "  " + this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_year), SMLReport._report._cellAlign.Center, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "แผ่นที่" + " : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);

                            this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._compamy_name, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._compamy_name))._str + "\t: " + SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno))._str + "\t: " + MyLib._myGlobal._ltdTax + ((MyLib._myGlobal._branchCode.Length > 0 && MyLib._myGlobal._branchCode != "000") ? " สาขาที่ " + MyLib._myGlobal._branchCode : " สำนักงานใหญ่ "), SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._address, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._address))._str + "\t: " + MyLib._myGlobal._ltdAddress.Replace("\n", ""), SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                        }
                        return true;
                    case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:
                        {
                            string __month = ((MyLib._myComboBox)this._conditionScreen._screen._getControl(_g.d.resource_report_vat._vat_month)).SelectedItem.ToString();
                            _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, "รายงานภาษีหัก ณ ที่จ่าย (ภ.ง.ด.53)", SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                            //this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._report_name), SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                            this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_month, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_month))._str + "  " + __month + "  " + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_year, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._vat_year))._str + "  " + this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._vat_year), SMLReport._report._cellAlign.Center, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "แผ่นที่" + " : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);

                            this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._compamy_name, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._compamy_name))._str + "\t: " + SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno))._str + "\t: " + MyLib._myGlobal._ltdTax + ((MyLib._myGlobal._branchCode.Length > 0 && MyLib._myGlobal._branchCode != "000") ? " สาขาที่ " + MyLib._myGlobal._branchCode : " สำนักงานใหญ่ "), SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._address, _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._address))._str + "\t: " + MyLib._myGlobal._ltdAddress.Replace("\n", ""), SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                        }
                        return true;
                    case _singhaReportEnum.GL_ข้อมูลรายวัน:
                        {
                            _view1.__excelFlieName = "รายงานข้อมูลรายวัน จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                            _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานข้อมูลรายวัน จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false), SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                        }
                        return true;
                    case _singhaReportEnum.การ์ดสินค้า:
                        {
                            string __itemCodeWhere = this._conditionScreen._screen._getDataStr(_g.d.resource_report._item_code);

                            _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, "สรุปการ์ดสินค้า", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                            _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "จากวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false) + ((__itemCodeWhere.Length > 0) ? " สินค้า : " + __itemCodeWhere : ""), SMLReport._report._cellAlign.Center, _view1._fontHeader2);


                            _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                            _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                        }
                        break;
                    default:
                        {
                            this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                            int __row = 2;
                            if (this._conditionText.Trim().Length > 0)
                            {
                                _view1._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, this._conditionText, SMLReport._report._cellAlign.Center, this._view1._fontHeader2);
                                __row++;
                            }
                            if (this._conditionTextDetail.Trim().Length > 0)
                            {
                                this._view1._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, this._conditionTextDetail, SMLReport._report._cellAlign.Center, this._view1._fontHeader2);
                                __row++;
                            }
                            this._view1._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Title    : " + this._screenName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                            __row++;
                            this._view1._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                            this._view1._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                            if (this._reportDescripton.Length > 0)
                            {
                                __row++;
                                this._view1._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Description : " + this._reportDescripton, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                                //_viewControl._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, _viewControl._fontHeader2);
                            }

                        }
                        break;
                }
            }
            else if (type == SMLReport._report._objectType.Detail)
            {
                switch (this._mode)
                {
                    case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                        {
                            __accountObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top, false);
                            _view1._addColumn(__accountObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._code, "", _g.d.gl_chart_of_account._code, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__accountObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._name_1, "", _g.d.gl_chart_of_account._name_1, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__accountObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._name_2, "", _g.d.gl_chart_of_account._name_2, SMLReport._report._cellAlign.Left);

                            //
                            __jounalListObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.Bottom);
                            _view1._addColumn(__jounalListObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_date, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_date, _g.d.gl_list_view._journal_date, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_doc_no, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_doc_no, _g.d.gl_list_view._journal_doc_no, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._book_code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._book_code, _g.d.gl_list_view._book_code, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._period_number, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._period_number, _g.d.gl_list_view._period_number, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 27, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_description, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_description, _g.d.gl_list_view._journal_description, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._debit, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._credit, _g.d.gl_list_view._credit, SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._balance, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance, _g.d.gl_list_view._balance, SMLReport._report._cellAlign.Right);
                        }
                        return true;
                    case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                        {
                            __accountObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                            _view1._addColumn(__accountObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._code, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code, _g.d.gl_chart_of_account._code, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__accountObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._name_1, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._name_1, _g.d.gl_chart_of_account._name_1, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__accountObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_chart_of_account._name_2, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._name_2, _g.d.gl_chart_of_account._name_2, SMLReport._report._cellAlign.Left);

                            //
                            __jounalListObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 5, true, SMLReport._report._columnBorder.Bottom);
                            _view1._addColumn(__jounalListObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_date, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_date, _g.d.gl_list_view._journal_date, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_doc_no, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_doc_no, _g.d.gl_list_view._journal_doc_no, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._book_code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._book_code, _g.d.gl_list_view._book_code, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._period_number, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._period_number, _g.d.gl_list_view._period_number, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 27, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._journal_description, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._journal_description, _g.d.gl_list_view._journal_description, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._debit, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._credit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._credit, _g.d.gl_list_view._credit, SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__jounalListObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_list_view._balance, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance, _g.d.gl_list_view._balance, SMLReport._report._cellAlign.Right);
                        }
                        return true;
                    case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                    case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:
                        {
                            //พิมพ์ชื่อฟิลด์บน
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                            _view1._addColumn(__ojtReport, 4, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._number, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 28, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ar_name, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 14, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._cust_taxno, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._branch_code, "", SMLReport._report._cellAlign.Default);
                            //_view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._wht_doc_no, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._d_m_y, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._amount, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._conditions, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 0, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);

                            //พิมพ์ชื่อฟิลด์ล่าง

                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                            _view1._addColumn(__ojtReport, 4, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 28, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._ar_address, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 14, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pay, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._status_pay, "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax_rate, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_pay, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_sent, "", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 0, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);


                        }
                        return true;
                    case _singhaReportEnum.GL_ข้อมูลรายวัน:
                        {
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                            if (_conditionSortBy == 1)
                            {
                                // เรียงตามวันที่
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_date, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date, _g.d.gl_journal._doc_date, SMLReport._report._cellAlign.Left);
                                _view1._addColumn(__ojtReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_no, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no, _g.d.gl_journal._doc_no, SMLReport._report._cellAlign.Left);
                            }
                            else
                            {
                                // เรียงตามเลขที่เอกสาร
                                _view1._addColumn(__ojtReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_no, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no, _g.d.gl_journal._doc_no, SMLReport._report._cellAlign.Left);
                                _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._doc_date, _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date, _g.d.gl_journal._doc_date, SMLReport._report._cellAlign.Left);
                            }
                            //_view1._addColumn(__journalObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._ref_date, _g.d.gl_journal._table + "." + _g.d.gl_journal._ref_date, _g.d.gl_journal._ref_date, SMLReport._report._cellAlign.Left);
                            //_view1._addColumn(__journalObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._ref_no, _g.d.gl_journal._table + "." + _g.d.gl_journal._ref_no, _g.d.gl_journal._ref_no, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__ojtReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._book_code, _g.d.gl_journal._table + "." + _g.d.gl_journal._book_code, _g.d.gl_journal._book_code, SMLReport._report._cellAlign.Left);
                            //_view1._addColumn(__ojtReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._debit, _g.d.gl_journal._table + "." + _g.d.gl_journal._debit, _g.d.gl_journal._debit, SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__ojtReport, 24 + 15 + 15 + 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal._description, _g.d.gl_journal._table + "." + _g.d.gl_journal._description, _g.d.gl_journal._description, SMLReport._report._cellAlign.Left);
                            //
                            __jounalListObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.Bottom);
                            _view1._addColumn(__jounalListObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code, _g.d.gl_journal_detail._account_code, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_name, _g.d.gl_journal_detail._account_name, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 22, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._description, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._description, _g.d.gl_journal_detail._description, SMLReport._report._cellAlign.Left);
                            _view1._addColumn(__jounalListObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._debit, _g.d.gl_journal_detail._debit, SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__jounalListObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._credit, _g.d.gl_journal_detail._credit, SMLReport._report._cellAlign.Right);
                            // สร้าง Column ไม่ให้แสดง มีไว้เพื่อกำหนดตำแหน่งยอดรวมเท่านั้น
                            __totalDateObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 45, true, SMLReport._report._columnBorder.None, false);
                            _view1._addColumn(__totalDateObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.Bottom, this._totalDescriptionColumnName, this._totalDescriptionColumnName, this._totalDescriptionColumnName, SMLReport._report._cellAlign.Left, false);
                            _view1._addColumn(__totalDateObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.Bottom, this._columnTotalAmounDebit, this._columnTotalAmounDebit, this._columnTotalAmounDebit, SMLReport._report._cellAlign.Right, false);
                            _view1._addColumn(__totalDateObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.Bottom, this._columnTotalAmounCredit, this._columnTotalAmounCredit, this._columnTotalAmounCredit, SMLReport._report._cellAlign.Right, false);
                        }
                        return true;
                    case _singhaReportEnum.การ์ดสินค้า:
                        {

                            // row 1
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._item_code, _g.d.ic_resource._table + "." + _g.d.ic_resource._item_code, _g.d.ic_resource._item_code, SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 26, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._item_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._item_name, _g.d.ic_resource._item_name, SMLReport._report._cellAlign.Default);

                            //_view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._many_unit, _g.d.ic_resource._table + "." + _g.d.ic_resource._many_unit, _g.d.ic_resource._many_unit, SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._many_unit, _g.d.ic_resource._table + "." + _g.d.ic_resource._big_qty, _g.d.ic_resource._many_unit, SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._single_unit, _g.d.ic_resource._table + "." + _g.d.ic_resource._small_qty, _g.d.ic_resource._single_unit, SMLReport._report._cellAlign.Default);
                            // _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._single_unit, _g.d.ic_resource._table + "." + _g.d.ic_resource._single_unit, _g.d.ic_resource._single_unit, SMLReport._report._cellAlign.Default);

                            _view1._addColumn(__ojtReport, 16, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._in, _g.d.ic_resource._table + "." + _g.d.ic_resource._in, _g.d.ic_resource._in, SMLReport._report._cellAlign.Center);
                            _view1._addColumn(__ojtReport, 16, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._out, _g.d.ic_resource._table + "." + _g.d.ic_resource._out, _g.d.ic_resource._out, SMLReport._report._cellAlign.Center);
                            _view1._addColumn(__ojtReport, 16, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._balance_qty, _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, _g.d.ic_resource._balance_qty, SMLReport._report._cellAlign.Center);

                            // row 2
                            __totalDateObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._doc_date, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_date, _g.d.ic_resource._doc_date, SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__totalDateObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._trans_flag, _g.d.ic_resource._table + "." + _g.d.ic_resource._trans_flag, _g.d.ic_resource._trans_flag, SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__totalDateObject, 14, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._doc_no, _g.d.ic_resource._table + "." + _g.d.ic_resource._doc_no, _g.d.ic_resource._doc_no, SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__totalDateObject, 18, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._detail, _g.d.ic_resource._table + "." + _g.d.ic_resource._detail, _g.d.ic_resource._detail, SMLReport._report._cellAlign.Default);

                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "in_max", _g.d.ic_resource._table + "." + _g.d.ic_resource._big_qty, "in_max", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "in_min", _g.d.ic_resource._table + "." + _g.d.ic_resource._small_qty, "in_min", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "out_max", _g.d.ic_resource._table + "." + _g.d.ic_resource._big_qty, "out_max", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "out_min", _g.d.ic_resource._table + "." + _g.d.ic_resource._small_qty, "out_min", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "balance_max", _g.d.ic_resource._table + "." + _g.d.ic_resource._big_qty, "balance_max", SMLReport._report._cellAlign.Right);
                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "balance_min", _g.d.ic_resource._table + "." + _g.d.ic_resource._small_qty, "balance_min", SMLReport._report._cellAlign.Right);

                        }
                        return true;

                    case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                        {
                            // row 1
                            __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);

                            _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._warehouse, _g.d.ic_resource._table + "." + _g.d.ic_resource._warehouse, _g.d.ic_resource._warehouse, SMLReport._report._cellAlign.Default);
                            _view1._addColumn(__ojtReport, 90, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._location, _g.d.ic_resource._table + "." + _g.d.ic_resource._location, _g.d.ic_resource._location, SMLReport._report._cellAlign.Default);

                            // row 2
                            __totalDateObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);

                            _view1._addColumn(__totalDateObject, 3, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._sort_order, _g.d.ic_resource._table + "." + _g.d.ic_resource._sort_order, _g.d.ic_resource._sort_order, SMLReport._report._cellAlign.Center);

                            _view1._addColumn(__totalDateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._ic_code, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_code, _g.d.ic_resource._ic_code, SMLReport._report._cellAlign.Center);
                            _view1._addColumn(__totalDateObject, 29, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._ic_name, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_name, _g.d.ic_resource._ic_name, SMLReport._report._cellAlign.Center);

                            _view1._addColumn(__totalDateObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._big_qty, _g.d.ic_resource._table + "." + _g.d.ic_resource._big_qty, _g.d.ic_resource._big_qty, SMLReport._report._cellAlign.Center);
                            _view1._addColumn(__totalDateObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._small_qty, _g.d.ic_resource._table + "." + _g.d.ic_resource._small_qty, _g.d.ic_resource._small_qty, SMLReport._report._cellAlign.Center);

                            _view1._addColumn(__totalDateObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._sale, _g.d.ic_resource._table + "." + _g.d.ic_resource._sale, _g.d.ic_resource._sale, SMLReport._report._cellAlign.Center);
                            _view1._addColumn(__totalDateObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._premium, _g.d.ic_resource._table + "." + _g.d.ic_resource._premium, _g.d.ic_resource._premium, SMLReport._report._cellAlign.Center);
                            _view1._addColumn(__totalDateObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._sale_return, _g.d.ic_resource._table + "." + _g.d.ic_resource._sale_return, _g.d.ic_resource._sale_return, SMLReport._report._cellAlign.Center);
                            _view1._addColumn(__totalDateObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._balance_qty, _g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty, _g.d.ic_resource._balance_qty, SMLReport._report._cellAlign.Center);

                            /*
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._ic_code, _g.d.ic_resource._ic_code, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.LeftRightBottom });
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._ic_name, _g.d.ic_resource._ic_name, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });

                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._big_qty, _g.d.ic_resource._big_qty, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._small_qty, _g.d.ic_resource._small_qty, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });

                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._sale, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._premium, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._sale_return, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._balance_qty, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                            */
                        }
                        return true;
                    case _singhaReportEnum.รายงานยอดขายรายเดือนตามกลุ่มสินค้า_เทียบปี:

                        int __year = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report._year));
                        string __monthname = this._conditionScreen._screen._getDataComboStr(_g.d.resource_report._monthly, false);

                        __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                        _view1._addColumn(__ojtReport, 60, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", _g.d.ic_resource._warehouse, SMLReport._report._cellAlign.Default);
                        _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "year_0", __monthname + " " + (__year - 1), _g.d.ic_resource._location, SMLReport._report._cellAlign.Center);
                        _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "year_1", __monthname + " " + (__year), _g.d.ic_resource._location, SMLReport._report._cellAlign.Center);

                        __totalDateObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                        _view1._addColumn(__totalDateObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._sort_order, _g.d.ic_resource._table + "." + _g.d.ic_resource._sort_order, _g.d.ic_resource._sort_order, SMLReport._report._cellAlign.Center);
                        _view1._addColumn(__totalDateObject, 55, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.ic_resource._ic_group, _g.d.ic_resource._table + "." + _g.d.ic_resource._ic_group, _g.d.ic_resource._ic_group, SMLReport._report._cellAlign.Center);

                        _view1._addColumn(__totalDateObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "year_0_net", "ยอดขายสุทธิ", "year_0_net", SMLReport._report._cellAlign.Center);
                        _view1._addColumn(__totalDateObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "year_0_percent", "สัดส่วน", "year_0_percent", SMLReport._report._cellAlign.Center);

                        _view1._addColumn(__totalDateObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "year_1_net", "ยอดขายสุทธิ", "year_1_net", SMLReport._report._cellAlign.Center);
                        _view1._addColumn(__totalDateObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "year_1_percent", "สัดส่วน", "year_1_percent", SMLReport._report._cellAlign.Center);
                        break;

                }
            }
            else if (type == SMLReport._report._objectType.PageFooter)
            {
                if (this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3 || this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53)
                {
                    SMLReport._report._objectListType __footerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.PageFooter, true, 0, true, SMLReport._report._columnBorder.None);
                    int __col1 = _view1._addColumn(__footerObject, 4);
                    int __columnDetail = _view1._addColumn(__footerObject, 72);
                    int __columnPayAmount = _view1._addColumn(__footerObject, 10);
                    int __columnTaxAmount = _view1._addColumn(__footerObject, 10);
                    int __columnCondition = _view1._addColumn(__footerObject, 6);

                    this._view1._addCell(__footerObject, __columnDetail, true, 0, -1, SMLReport._report._cellType.String, "รวมยอดเงินได้และภาษีที่นำส่ง(ยกไปรวมกับแผ่นแรก)", SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                    this._view1._addCell(__footerObject, __columnPayAmount, true, 0, -1, SMLReport._report._cellType.Number, "", SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                    this._view1._addCell(__footerObject, __columnTaxAmount, true, 0, -1, SMLReport._report._cellType.Number, "", SMLReport._report._cellAlign.Right, this._view1._fontHeader2);

                    this._view1._addCell(__footerObject, __columnDetail, true, 2, -1, SMLReport._report._cellType.String, "ลงชื่อ....................................................ผู้จ่ายเงิน", SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                    this._view1._addCell(__footerObject, __columnDetail, true, 3, -1, SMLReport._report._cellType.String, "ตำแหน่ง....................................................", SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                    this._view1._addCell(__footerObject, __columnDetail, true, 4, -1, SMLReport._report._cellType.String, "ยื่นวันที่....................................................", SMLReport._report._cellAlign.Left, this._view1._fontStandard);

                    return true;
                }
            }

            return false;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._build();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
