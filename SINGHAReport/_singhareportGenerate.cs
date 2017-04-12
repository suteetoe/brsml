using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace SINGHAReport
{
    public partial class _singhareportGenerate : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTable2;
        _conditionForm _conditionScreen;
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private _singhaReportEnum _mode;
        private int _recordCount;
        //string _conditionText = "";
        // string _conditionTextDetail = "";\

        #region variable for book statement
        private List<string> _bookCodeList;
        private StringBuilder _bookCode;
        private string _dateBegin;
        private string _dateEnd;

        #endregion

        public _singhareportGenerate(_singhaReportEnum reportType, string screenName)
        {
            InitializeComponent();

            _mode = reportType;

            Boolean __landscape = false;

            switch (this._mode)
            {
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    __landscape = true;
                    break;
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                    // __landscape = true;
                    break;
            }
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, __landscape);

            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._renderHeader += new SMLReport._generate.RenderHeaderEventHandler(_report__renderHeader);

            this._report.Disposed += new EventHandler(_report_Disposed);
            this._report._objectPageBreak += new SMLReport._generate.ObjectPageBreakEventHandler(_report__objectPageBreak);
            this._report._viewControl._beforeReportDrawPaperArgs += _viewControl__beforeReportDrawPaperArgs;

            if (this._mode == _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย)
            {
                this._report._renderFooterPage += _report__renderFooterPage;
            }

            if (this._mode == _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง)
            {
                this._report._viewControl._paper._drawFooterReportEvent += _paper__drawFooterReportEvent;
            }

            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);

            _conditionScreen = new _conditionForm(reportType, screenName);
            _showCondition();

        }

        private void _paper__drawFooterReportEvent(int pageNumber, Graphics g, float startDrawYPoint, float drawScale)
        {
            if (this._mode == _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง)
            {
                int _pageWidth = (int)(((_report._viewControl._pageSetupDialog.PageSettings.Landscape) ? _report._viewControl._pageSetupDialog.PageSettings.PaperSize.Height : _report._viewControl._pageSetupDialog.PageSettings.PaperSize.Width) - (_report._viewControl._pageSetupDialog.PageSettings.Margins.Left + _report._viewControl._pageSetupDialog.PageSettings.Margins.Right));
                int _pageHeight = (int)(((_report._viewControl._pageSetupDialog.PageSettings.Landscape) ? _report._viewControl._pageSetupDialog.PageSettings.PaperSize.Width : _report._viewControl._pageSetupDialog.PageSettings.PaperSize.Height) - (_report._viewControl._pageSetupDialog.PageSettings.Margins.Top + _report._viewControl._pageSetupDialog.PageSettings.Margins.Bottom));

                float __y = (_pageHeight - 110) * drawScale; // startDrawYPoint;
                int _leftMargin = (int)(_report._viewControl._pageSetupDialog.PageSettings.Margins.Left * drawScale);
                //
                Font __newFont = new Font(_report._viewControl._fontStandard.FontFamily, (float)(_report._viewControl._fontStandard.Size * drawScale), _report._viewControl._fontStandard.Style);


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

        private void _report__renderFooterPage(SMLReport._generate source)
        {
            if (this._mode == _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย)
            {
                // footer
                SMLReport._report._objectListType __footerObject = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.PageFooter, true, 0, true, SMLReport._report._columnBorder.None);
                int __col1 = source._viewControl._addColumn(__footerObject, 60);
                int __columnSale = source._viewControl._addColumn(__footerObject, 20);
                int __columnFinance = source._viewControl._addColumn(__footerObject, 20);

                source._viewControl._addCell(__footerObject, __columnSale, true, 0, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);

                source._viewControl._addCell(__footerObject, __columnSale, true, 1, -1, SMLReport._report._cellType.String, "พนักงานขาย.....................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnFinance, true, 1, -1, SMLReport._report._cellType.String, "เจ้าหน้าที่การเงิน.....................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);

            }
            /*else if (this._mode == _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง)
            {
                // footer
                SMLReport._report._objectListType __footerObjectLine1 = source._viewControl._addObject(source._viewControl._objectList, SMLReport._report._objectType.PageFooter, true, 0, true, SMLReport._report._columnBorder.None);

                int __columnDetail = source._viewControl._addColumn(__footerObjectLine1, 20);
                int __columnDetail1 = source._viewControl._addColumn(__footerObjectLine1, 10);
                int __columnDetail2 = source._viewControl._addColumn(__footerObjectLine1, 10);
                int __columnDetail3 = source._viewControl._addColumn(__footerObjectLine1, 10);
                int __columnSale = source._viewControl._addColumn(__footerObjectLine1, 25);
                int __columnFinance = source._viewControl._addColumn(__footerObjectLine1, 25);

                source._viewControl._addCell(__footerObjectLine1, __columnDetail, true, 1, -1, SMLReport._report._cellType.String, "รายการคืน ขวด - ลังเปล่า", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObjectLine1, __columnDetail1, true, 1, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObjectLine1, __columnDetail2, true, 1, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObjectLine1, __columnDetail3, true, 1, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObjectLine1, __columnSale, true, 1, -1, SMLReport._report._cellType.String, "คงคลัง", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObjectLine1, __columnFinance, true, 1, -1, SMLReport._report._cellType.String, "คงเหลือ", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);

                source._viewControl._addCell(__footerObject, __columnDetail, true, 2, -1, SMLReport._report._cellType.String, "รายการ", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail1, true, 2, -1, SMLReport._report._cellType.String, "ขวดโซดา", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail2, true, 2, -1, SMLReport._report._cellType.String, "ขวดน้ำ", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail3, true, 2, -1, SMLReport._report._cellType.String, "รวมลัง", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnSale, true, 2, -1, SMLReport._report._cellType.String, "Checker................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnFinance, true, 2, -1, SMLReport._report._cellType.String, "Checker................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);

                source._viewControl._addCell(__footerObject, __columnDetail, true, 3, -1, SMLReport._report._cellType.String, "รวมจ่าย", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail1, true, 3, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail2, true, 3, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail3, true, 3, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnSale, true, 3, -1, SMLReport._report._cellType.String, "ผู้เบิก.....................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnFinance, true, 3, -1, SMLReport._report._cellType.String, "ผู้เบิก.....................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);

                source._viewControl._addCell(__footerObject, __columnDetail, true, 4, -1, SMLReport._report._cellType.String, "รับเข้า", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail1, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail2, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail3, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnSale, true, 4, -1, SMLReport._report._cellType.String, "ผู้อนุมัติ................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnFinance, true, 4, -1, SMLReport._report._cellType.String, "ผู้อนุมัติ................................", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);

                source._viewControl._addCell(__footerObject, __columnDetail, true, 5, -1, SMLReport._report._cellType.String, "ขาย/คืน", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail1, true, 5, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail2, true, 5, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnDetail3, true, 5, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Center, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnSale, true, 5, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                source._viewControl._addCell(__footerObject, __columnFinance, true, 5, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Left, source._viewControl._fontStandard);
                
            }*/
        }

        private void _viewControl__beforeReportDrawPaperArgs(SMLReport._report._pageListType pageListType)
        {
        }

        bool _report__objectPageBreak(SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Total)
            {
                {
                    this._recordCount = 0;
                    // return true;
                }
            }
            return false;
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (this._dataTable == null)
            {
                return null;
            }

            if (level._levelName.Equals("master"))
            {
                // 
                if (this._mode == _singhaReportEnum.BankStatement)
                {
                    DataTable __data = new DataTable();
                    __data.Columns.Add(_g.d.cb_resource._book_no, typeof(string));
                    __data.Columns.Add(_g.d.cb_resource._book_name, typeof(string));
                    __data.Columns.Add(_g.d.cb_resource._book_number, typeof(string));
                    __data.Columns.Add(_g.d.cb_resource._bank_code, typeof(string));

                    for (int __loop = 0; __loop < this._bookCodeList.Count; __loop++)
                    {
                        int __passBookCodeColumn = this._conditionScreen._gridCondition._findColumnByName(_g.d.erp_pass_book._code);
                        int __findRow = this._conditionScreen._gridCondition._findData(__passBookCodeColumn, this._bookCodeList[__loop]);

                        string __bookName = this._conditionScreen._gridCondition._cellGet(__findRow, _g.d.erp_pass_book._name_1).ToString();
                        string __bookNumber = this._conditionScreen._gridCondition._cellGet(__findRow, _g.d.erp_pass_book._book_number).ToString();
                        string __bookCode = this._conditionScreen._gridCondition._cellGet(__findRow, _g.d.erp_pass_book._bank_code).ToString() + "~" + this._conditionScreen._gridCondition._cellGet(__findRow, "bank_name").ToString();
                        __data.Rows.Add(this._bookCodeList[__loop], __bookName, __bookNumber, __bookCode);
                    }
                    return __data.Select();
                }
                else
                {
                    __sumAmountGrandTotal = 0M;
                    __sumOutDueGrandTotal = 0M;
                    __sum7 = 0M;
                    __sum15 = 0M;
                    __sum30 = 0M;
                    __sumOverDue = 0M;

                    return this._dataTable.Select();
                }
            }
            else if (level._levelName.Equals("detail"))
            {
                StringBuilder __where = new StringBuilder();

                switch (this._mode)
                {
                    case _singhaReportEnum.การ์ดเจ้าหนี้:
                        __where.Append(" ap_ar_resource.ap_code = \'" + source[_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ap_code].ToString() + "\' ");
                        DataRow[] dataRow = this._dataTable2.Select(__where.ToString());

                        // ปรับยอด
                        decimal __balance = 0M;
                        for (int __row = 0; __row < dataRow.Length; __row++)
                        {
                            decimal __amount = MyLib._myGlobal._decimalPhase(dataRow[__row][_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._amount].ToString());

                            __balance += __amount;
                            dataRow[__row][_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._balance_amount] = __balance;

                        }
                        return dataRow;
                    case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                    case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                    case _singhaReportEnum.ลูกหนี้คงค้าง:
                        {
                            //StringBuilder __where = new StringBuilder();
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
                            return this._dataTable2.Select(__where.ToString());
                        }

                    case _singhaReportEnum.รายงานการตัดเช็ค:
                        {
                            __where.Append(" trans_number = \'" + source[_g.d.cb_chq_list._chq_number].ToString() + "\' ");
                            __where.Append(" and doc_ref = \'" + source[_g.d.cb_chq_list._doc_ref].ToString() + "\' ");

                            return this._dataTable2.Select(__where.ToString());

                        }
                    case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                        {
                            __where.Append(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + "=\'" + source[_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no].ToString() + "\'");
                            return (this._dataTable2 == null) ? null : this._dataTable2.Select(__where.ToString());
                        }
                    case _singhaReportEnum.BankStatement:
                        {
                            int __columnBookCode = levelParent._findColumnName(_g.d.cb_resource._book_no);

                            /*
                            for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                            {
                                if (levelParent._columnList[__loop]._fieldName.Length > 0)
                                {
                                    if (__where.Length > 0)
                                    {
                                        __where.Append(" and ");
                                    }
                                }
                            }*/

                            __where.Append(levelParent._columnList[__columnBookCode]._fieldName + "=\'" + source[levelParent._columnList[__columnBookCode]._fieldName].ToString().Replace("\'", "\'\'") + "\'");

                            if (this._dataTable2.Rows.Count == 0)
                                return null;

                            return this._dataTable2.Select(__where.ToString());
                        }
                    case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                        {
                            int __warehouse = levelParent._findColumnName(_g.d.ic_resource._warehouse);
                            int __location = levelParent._findColumnName(_g.d.ic_resource._location);

                            __where.Append(levelParent._columnList[__warehouse]._fieldName + "=\'" + source[levelParent._columnList[__warehouse]._fieldName].ToString().Replace("\'", "\'\'") + "\' and " + levelParent._columnList[__location]._fieldName + "=\'" + source[levelParent._columnList[__location]._fieldName].ToString().Replace("\'", "\'\'") + "\'");

                            if (this._dataTable2.Rows.Count == 0)
                                return null;

                            return this._dataTable2.Select(__where.ToString());

                        }
                }
                return null;
            }

            return null;
        }

        void _report__renderHeader(SMLReport._generate source)
        {
            switch (this._mode)
            {
                default:
                    {
                        SMLReport._report._objectListType __headerObject = this._report._viewControl._addObject(this._report._viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                        int __newColumn = this._report._viewControl._addColumn(__headerObject, 100);
                        this._report._viewControl._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, this._report._viewControl._fontHeader1);
                        int __row = 2;
                        if (this._report._conditionText.Trim().Length > 0)
                        {
                            this._report._viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, this._report._conditionText, SMLReport._report._cellAlign.Center, this._report._viewControl._fontHeader2);
                            __row++;
                        }
                        if (this._report._conditionTextDetail.Trim().Length > 0)
                        {
                            this._report._viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, this._report._conditionTextDetail, SMLReport._report._cellAlign.Center, this._report._viewControl._fontHeader2);
                            __row++;
                        }
                        this._report._viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Title    : " + this._screenName, SMLReport._report._cellAlign.Left, this._report._viewControl._fontHeader2);
                        this._report._viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._report._viewControl._fontHeader2);
                        __row++;
                        this._report._viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._report._viewControl._fontHeader2);
                        this._report._viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._report._viewControl._fontHeader2);
                        if (this._report._reportDescripton.Length > 0)
                        {
                            __row++;
                            this._report._viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Description : " + this._report._reportDescripton, SMLReport._report._cellAlign.Left, this._report._viewControl._fontHeader2);
                            //_viewControl._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, _viewControl._fontHeader2);
                        }

                    }
                    break;
            }
        }

        decimal __sumAmountGrandTotal = 0M;
        decimal __sumOutDueGrandTotal = 0M;
        decimal __sum7 = 0M;
        decimal __sum15 = 0M;
        decimal __sum30 = 0M;
        decimal __sumOverDue = 0M;

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Data)
            {
                if (sender._levelName.Equals("master"))
                {
                    switch (this._mode)
                    {
                        case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                        case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                        case _singhaReportEnum.ลูกหนี้คงค้าง:
                            {
                                if (columnNumber == sender._findColumnName(_g.d.ap_ar_resource._credit_money))
                                {
                                    sender._columnList[columnNumber]._dataStr = "วงเงินเครดิต " + MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr));
                                }
                                if (columnNumber == sender._findColumnName(_g.d.ap_ar_resource._credit_day))
                                {
                                    sender._columnList[columnNumber]._dataStr = (sender._columnList[columnNumber]._dataStr.Length == 0) ? "" : "ระยะเวลาเครดิต " + MyLib._myGlobal._intPhase(sender._columnList[columnNumber]._dataStr).ToString() + " วัน";
                                }
                            }
                            break;
                    }
                }
                else if (sender._levelName.Equals("detail"))
                {
                    switch (this._mode)
                    {

                        case _singhaReportEnum.รายงานการตัดเช็ค:
                            {
                                int __columnDoctype = sender._findColumnName(_g.d.cb_trans_detail._trans_flag);
                                int __columnBalanceAmount = sender._findColumnName(_g.d.cb_trans_detail._balance_amount);
                                int __columnAmount = sender._findColumnName(_g.d.cb_trans_detail._amount);
                                if (__columnBalanceAmount == columnNumber)
                                {
                                    if (sender._columnList[__columnDoctype]._dataStr == "ยอดยกมา")
                                    {
                                        __sumAmountGrandTotal = sender._columnList[__columnBalanceAmount]._dataNumber;
                                    }
                                    else
                                    {
                                        __sumAmountGrandTotal -= sender._columnList[__columnAmount]._dataNumber;
                                        sender._columnList[columnNumber]._dataNumber = __sumAmountGrandTotal;
                                    }

                                }
                            }
                            break;
                        case _singhaReportEnum.BankStatement:
                            {
                                if (columnNumber == sender._findColumnName(_g.d.cb_resource._doc_type))
                                {
                                    sender._columnList[columnNumber]._dataStr = _g.g._transFlagGlobal._transName((int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr));
                                }
                            }
                            break;
                    }

                }
            }
            else if (isTotal == SMLReport._generateColumnStyle.Total)
            {
                if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ || this._mode == _singhaReportEnum.ลูกหนี้คงค้าง || this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
                {
                    if (sender._levelName.Equals("detail"))
                    {
                        int __columnDueDate = sender._findColumnName(_g.d.ap_ar_resource._doc_no);

                        int __columnSumAmount = sender._findColumnName(_g.d.ap_ar_resource._ar_balance);
                        int __columnOutDue = sender._findColumnName(_g.d.ap_ar_resource._out_due);
                        int __columnSum7 = sender._findColumnName(_g.d.ap_ar_resource._term_1);
                        int __columnSum15 = sender._findColumnName(_g.d.ap_ar_resource._term_2);
                        int __columnSum30 = sender._findColumnName(_g.d.ap_ar_resource._term_3);
                        int __columnSumOverDue = sender._findColumnName(_g.d.ap_ar_resource._term_4);

                        if (columnNumber == __columnSumAmount)
                            __sumAmountGrandTotal += sender._columnList[__columnSumAmount]._dataNumber;
                        if (columnNumber == __columnOutDue)
                            __sumOutDueGrandTotal += sender._columnList[__columnOutDue]._dataNumber;
                        if (columnNumber == __columnSum7)
                            __sum7 += sender._columnList[__columnSum7]._dataNumber;
                        if (columnNumber == __columnSum15)
                            __sum15 += sender._columnList[__columnSum15]._dataNumber;
                        if (columnNumber == __columnSum30)
                            __sum30 += sender._columnList[__columnSum30]._dataNumber;
                        if (columnNumber == __columnSumOverDue)
                            __sumOverDue += sender._columnList[__columnSumOverDue]._dataNumber;

                        if (columnNumber == __columnDueDate)
                        {
                            sender._columnList[__columnDueDate]._dataStr = "รวม";
                        }
                    }
                }
            }
            else if (isTotal == SMLReport._generateColumnStyle.GrandTotal)
            {
                if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ || this._mode == _singhaReportEnum.ลูกหนี้คงค้าง || this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
                {
                    int __columnSumAmount = sender._findColumnName(_g.d.ap_ar_resource._ar_balance);
                    int __columnOutDue = sender._findColumnName(_g.d.ap_ar_resource._out_due);
                    int __columnSum7 = sender._findColumnName(_g.d.ap_ar_resource._term_1);
                    int __columnSum15 = sender._findColumnName(_g.d.ap_ar_resource._term_2);
                    int __columnSum30 = sender._findColumnName(_g.d.ap_ar_resource._term_3);
                    int __columnSumOverDue = sender._findColumnName(_g.d.ap_ar_resource._term_4);

                    int __columnDueDate = sender._findColumnName(_g.d.ap_ar_resource._doc_no);

                    if (columnNumber == __columnSumAmount)
                        sender._columnList[__columnSumAmount]._dataNumber = __sumAmountGrandTotal;
                    if (columnNumber == __columnOutDue)
                        sender._columnList[__columnOutDue]._dataNumber = __sumOutDueGrandTotal;
                    if (columnNumber == __columnSum7)
                        sender._columnList[__columnSum7]._dataNumber = __sum7;
                    if (columnNumber == __columnSum15)
                        sender._columnList[__columnSum15]._dataNumber = __sum15;
                    if (columnNumber == __columnSum30)
                        sender._columnList[__columnSum30]._dataNumber = __sum30;
                    if (columnNumber == __columnSumOverDue)
                        sender._columnList[__columnSumOverDue]._dataNumber = __sumOverDue;

                    if (columnNumber == __columnDueDate)
                    {
                        sender._columnList[__columnDueDate]._dataStr = "รวมทั้งสิ้น";
                    }
                }
            }

        }

        void _report__query()
        {
            this._recordCount = 0;

            if (this._dataTable == null)
            {
                StringBuilder __queryMain = new StringBuilder();
                StringBuilder __queryDetail = new StringBuilder();



                switch (this._mode)
                {
                    #region การ์ดเจ้าหนี้
                    case _singhaReportEnum.การ์ดเจ้าหนี้:
                        {
                            string __from_date = this._conditionScreen._screen._getDataStrQuery(_g.d.resource_report._from_date);
                            string __to_date = this._conditionScreen._screen._getDataStrQuery(_g.d.resource_report._to_date);
                            string __from_ap = this._conditionScreen._screen._getDataStr(_g.d.resource_report._from_ap);
                            string __to_ap = this._conditionScreen._screen._getDataStr(_g.d.resource_report._to_ap);

                            __queryMain.Append("select code as \"ap_ar_resource.ap_code\", name_1 as \"ap_ar_resource.ap_name\" from ap_supplier ");
                            if (__from_ap.Length > 0 && __to_ap.Length > 0)
                                __queryMain.Append(" where code between \'" + __from_ap + "\' and \'" + __to_ap + "\'  order by code ");

                            // detail

                            string __purchaseQuery = "select cust_code, 1 as sort, 1 as sort2, 'P' as doc_type,doc_date,doc_no, tax_doc_no,total_amount as amount" +
                                ", 0 as pay_chq, 0 as pay_cash, 0 as pay_wht, 0 as pay_discount, 0 as pay_advance, 0 as pay_over" +
                                ", 0 as balance" +
                                ", array_to_string(array(select '#ใบสำคัญจ่าย=' || doc_no || ' ลงวันที่ ' || to_char(doc_date, 'dd/MM/YYYY') from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type=ic_trans.trans_flag), ',') as pay_detail " +
                                " from ic_trans where last_status=0 and " +
                                " ( " +
                                " ((trans_flag in (12, 315) or trans_flag = 260 ) and(inquiry_type in (0, 2) )) " +
                                " or(trans_flag in (81, 87))  " +
                                " or(trans_flag in (316, 14, 83, 89, 264)) " +
                                " )";
                            string __returnQuery = " select cust_code, 1 as sort, 2 as sort2,'O' as doc_type, doc_date, doc_no, tax_doc_no,-1*total_amount as amount" +
                                ", 0 as pay_chq, 0 as pay_cash, 0 as pay_wht, 0 as pay_discount, 0 as pay_advance, 0 as pay_over" +
                                ", 0 as balance" +
                                ", array_to_string(array(select '#ใบสำคัญจ่าย=' || doc_no || ' ลงวันที่ ' || to_char(doc_date, 'dd/MM/YYYY') from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type=ic_trans.trans_flag), ',') as pay_detail " +
                                " from ic_trans where last_status=0 and  " +
                                " ( " +
                                " (trans_flag in (16, 317) and(inquiry_type in (0, 1) )) " +
                                " or(trans_flag in (85, 91, 262)) " +
                                " )";

                            string __fieldPay = "(select {0} from cb_trans where cb_trans.doc_no = ap_ar_trans.doc_no and cb_trans.trans_flag = ap_ar_trans.trans_flag )";
                            string __payQuery = " select cust_code, 1 as sort, 3 as sort2,'C' as doc_type,doc_date,doc_no, '' as tax_doc_no,-1*total_net_value as amount" +
                                ", " + string.Format(__fieldPay, "chq_amount") + " as pay_chq" +
                                ", " + string.Format(__fieldPay, "cash_amount + tranfer_amount") + " as pay_cash" +
                                ", " + string.Format(__fieldPay, "total_tax_at_pay") + " as pay_wht" +
                                ", " + string.Format(__fieldPay, "discount_amount +  case when (total_income_amount > 0) then total_income_amount else 0 end ") + " as pay_discount" +
                                ", " + string.Format(__fieldPay, "deposit_amount") + " as pay_advance" +
                                ", " + string.Format(__fieldPay, "case when (total_income_amount < 0) then -1*total_income_amount else 0 end") + " as pay_over" +
                                ", 0 as balance, '' as pay_detail " +
                                " from ap_ar_trans where last_status = 0 and trans_flag =19 ";


                            string __balanceQuery = "select cust_code, 0 as sort, 0 as sort2, '0' as doc_type , null as doc_date, 'ยอดยกมา' as doc_no, '' as tax_doc_no,  sum(amount) as amount" +
                                ", 0 as pay_chq, 0 as pay_cash, 0 as pay_wht, 0 as pay_discount, 0 as pay_advance, 0 as pay_over" +
                                " , 0 as balance, '' as pay_detail from ( " +
                                __purchaseQuery + " and doc_date < date(" + __from_date + ")" +
                                " union all " +
                                __returnQuery + " and doc_date < date(" + __from_date + ")" +
                                " union all " +
                                __payQuery + " and doc_date < date(" + __from_date + ")" +
                                " ) as temp1 group by cust_code ";

                            string __docQuery =
                                __purchaseQuery + " and doc_date between  date(" + __from_date + ") and date(" + __to_date + ") " +
                                " union all " +
                                __returnQuery + " and doc_date between  date(" + __from_date + ") and date(" + __to_date + ") " +
                                " union all " +
                                __payQuery + " and doc_date between  date(" + __from_date + ") and date(" + __to_date + ") ";

                            __queryDetail.Append("select ");

                            __queryDetail.Append("cust_code as \"ap_ar_resource." + _g.d.ap_ar_resource._ap_code + "\" ");
                            __queryDetail.Append(",(select name_1 from ap_supplier where ap_supplier.code = temp99.cust_code) as \"ap_ar_resource." + _g.d.ap_ar_resource._ap_name + "\" ");
                            __queryDetail.Append(", doc_type as \"ap_ar_resource." + _g.d.ap_ar_resource._ap_ar_debt_type + "\"");
                            __queryDetail.Append(", doc_date as \"ap_ar_resource." + _g.d.ap_ar_resource._doc_date + "\"");
                            __queryDetail.Append(", doc_no as \"ap_ar_resource." + _g.d.ap_ar_resource._doc_no + "\"");
                            __queryDetail.Append(", tax_doc_no as \"ap_ar_resource." + _g.d.ap_ar_resource._vat_no + "\"");
                            __queryDetail.Append(", amount as \"ap_ar_resource." + _g.d.ap_ar_resource._amount + "\"");

                            __queryDetail.Append(", case when doc_type = 'P' then amount else 0 end as \"ap_ar_resource." + _g.d.ap_ar_resource._po_purchase + "\"");
                            __queryDetail.Append(", case when doc_type = 'O' then -1*amount else 0 end as \"ap_ar_resource." + _g.d.ap_ar_resource._po_cn_balance + "\"");
                            //__queryDetail.Append(", case when doc_type = 'C' then -1*amount else 0 end as \"ap_ar_resource." + _g.d.ap_ar_trans._sum_pay_money_chq + "\"");
                            __queryDetail.Append(", balance as \"ap_ar_resource." + _g.d.ap_ar_resource._balance_amount + "\"");
                            __queryDetail.Append(", pay_detail as \"ap_ar_resource." + _g.d.ap_ar_resource._receive_type + "\"");

                            __queryDetail.Append(", pay_chq as \"ap_ar_resource." + _g.d.ap_ar_trans._sum_pay_money_chq + "\"");
                            __queryDetail.Append(", pay_cash as \"ap_ar_resource." + _g.d.ap_ar_resource._cash_money + "\"");
                            __queryDetail.Append(", pay_wht as \"ap_ar_resource." + _g.d.gl_chart_of_account._wht_in + "\"");
                            __queryDetail.Append(", pay_discount as \"ap_ar_resource." + _g.d.ic_resource._discount + "\"");
                            __queryDetail.Append(", pay_advance as \"ap_ar_resource." + _g.d.ic_trans._sum_advance + "\"");
                            __queryDetail.Append(", pay_over as \"ap_ar_resource." + _g.d.ap_ar_resource._over_due + "\"");

                            __queryDetail.Append(" from (");

                            // balance
                            __queryDetail.Append(__balanceQuery);
                            // doc
                            __queryDetail.Append(" union all ");

                            __queryDetail.Append(__docQuery);

                            __queryDetail.Append(" ) as temp99 ");

                            if (__from_ap.Length > 0 && __to_ap.Length > 0)
                                __queryDetail.Append(" where cust_code between \'" + __from_ap + "\' and \'" + __to_ap + "\'  ");

                            __queryDetail.Append(" order by cust_code, sort, doc_date, sort2 ");


                        }
                        break;
                    #endregion

                    #region ยอดลูกหนี้คงเหลือ ลูกหนี้คงค้าง
                    case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                    case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                    case _singhaReportEnum.ลูกหนี้คงค้าง:
                        {
                            StringBuilder __whereBranch = new StringBuilder();
                            if (this._conditionScreen._useBranchCheckbox.Checked == true)
                            {
                                for (int __row = 0; __row < this._conditionScreen._gridCondition._rowData.Count; __row++)
                                {
                                    if (this._conditionScreen._gridCondition._cellGet(__row, 0).ToString().Equals("1"))
                                    {
                                        if (__whereBranch.Length > 0)
                                            __whereBranch.Append(",");

                                        string __branchCode = this._conditionScreen._gridCondition._cellGet(__row, _g.d.erp_branch_list._code).ToString();
                                        __whereBranch.Append("\'" + __branchCode + "\'");
                                    }
                                }
                            }

                            string __closeCreditStatus = this._conditionScreen._screen._getDataStr(_g.d.ap_ar_resource._status).ToString();

                            string __from_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.ap_ar_resource._date_begin));
                            string __to_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.ap_ar_resource._date_end));

                            string __from_close_date = "";
                            string __to_close_date = "";
                            string __from_close_date_check = "";
                            string __to_close_date_check = "";

                            if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ)
                            {
                                __from_close_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.ap_ar_resource._close_credit_date_from));
                                __to_close_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.ap_ar_resource._close_credit_date_to));
                                __from_close_date_check = this._conditionScreen._screen._getDataStr(_g.d.ap_ar_resource._close_credit_date_from);
                                __to_close_date_check = this._conditionScreen._screen._getDataStr(_g.d.ap_ar_resource._close_credit_date_to);
                            }

                            SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();


                            // int dateMode = 0;
                            string __from_ar = this._conditionScreen._screen._getDataStr(_g.d.ap_ar_resource._ar_code_begin);
                            string __to_ar = this._conditionScreen._screen._getDataStr(_g.d.ap_ar_resource._ar_code_end);

                            string __custWhere = __process.__createWhere(__from_ar, __to_ar, "ic_trans.cust_code"); // between '" + __from_ar + "' and '" + __to_ar + "' ";
                            int dateMode = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.ap_ar_resource._due_date_select));
                            string __dueDate = (dateMode == 0) ? "due_date" : "doc_date";

                            string __orderBy = _g.d.ap_ar_resource._ar_code + "," + _g.d.ap_ar_resource._doc_date + "," + _g.d.ap_ar_resource._doc_no;

                            int __term_1_begin = 1; // (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_1_begin);
                            int __term_1_end = 7; // (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_1_end);
                            int __term_2_begin = 8; // (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_2_begin);
                            int __term_2_end = 15; // (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_2_end);
                            int __term_3_begin = 16; // (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_3_begin);
                            int __term_3_end = 30; // (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_3_end);
                            //int __term_4_begin = 31; // (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_4_begin);
                            // int __term_4_end = (int)this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._term_4_end);

                            __queryMain.Append("select code as \"ap_ar_resource.ap_code\", name_1 as \"ap_ar_resource.ap_name\" from ap_supplier ");
                            if (__from_ar.Length > 0 && __to_ar.Length > 0)
                                __queryMain.Append(" where code between \'" + __from_ar + "\' and \'" + __to_ar + "\'  order by code ");


                            string __querySale = "select cust_code,doc_date, credit_day as doc_credit_day, credit_date as due_date " +
                                ", doc_no, trans_flag as doc_type,used_status " +
                                ",doc_ref as ref_doc_no " +
                                ",doc_ref_date as ref_doc_date " +
                                ",coalesce(total_amount, 0) as amount " +
                                ",coalesce(total_amount, 0) - (select coalesce(sum(coalesce(sum_pay_money, 0)), 0) from ap_ar_trans_detail where coalesce(last_status, 0) = 0 and trans_flag in (239) and ic_trans.doc_no = ap_ar_trans_detail.billing_no and ic_trans.doc_date = ap_ar_trans_detail.billing_date " + ((this._mode == _singhaReportEnum.ลูกหนี้คงค้าง) ? " and ap_ar_trans_detail.doc_date <= date('" + __to_date + "') " : "") + " ) as balance_amount " + // and doc_date <= date('2016-01-31')
                                ",branch_code " +
                                ", (select doc_no from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag and ap_ar_trans_detail.trans_flag = 235 and ap_ar_trans_detail.last_status = 0 limit 1) as bill_doc_no " +
                                "from ic_trans where coalesce(last_status, 0) = 0 and trans_flag = 44 and(inquiry_type = 0  or inquiry_type = 2)  and doc_date between \'" + __from_date + "\' and \'" + __to_date + "\' " +
                                ((__closeCreditStatus == "1") ? " and (select credit_status from ar_customer_detail where ar_customer_detail.ar_code = ic_trans.cust_code) in (1,2) " : "") +
                                ((__from_close_date_check != "" && __to_close_date_check != "") ? " and ((select close_credit_date from ar_customer_detail where ar_customer_detail.ar_code = ic_trans.cust_code) between \'" + __from_close_date + "\' and \'" + __to_close_date + "\' )" : "") +
                                ((__whereBranch.Length > 0) ? " and branch_code in (" + __whereBranch.ToString() + ")" : "") +
                                ((__custWhere.Length > 0) ? " and " + __custWhere : "");


                            string __queryDebtNote = "select cust_code,doc_date, credit_day as doc_credit_day, credit_date as due_date " +
                                ", doc_no, trans_flag as doc_type,used_status " +
                                ",'' as ref_doc_no " +
                                ",null as ref_doc_date " +
                                ", coalesce(total_amount, 0) as amount " +
                                ", coalesce(total_amount, 0) - (select coalesce(sum(coalesce(sum_pay_money, 0)), 0) from ap_ar_trans_detail where coalesce(last_status, 0) = 0 and trans_flag in (239) and ic_trans.doc_no = ap_ar_trans_detail.billing_no and ic_trans.doc_date = ap_ar_trans_detail.billing_date " + ((this._mode == _singhaReportEnum.ลูกหนี้คงค้าง) ? " and ap_ar_trans_detail.doc_date <= date('" + __to_date + "') " : "") + " ) as balance_amount " + // and doc_date <= date('2016-01-31')
                                ", branch_code " +
                                ", (select doc_no from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag and ap_ar_trans_detail.trans_flag = 235 and ap_ar_trans_detail.last_status = 0 limit 1) as bill_doc_no " +
                                " from ic_trans where coalesce(last_status, 0) = 0 and(trans_flag = 46 or trans_flag = 93 or trans_flag = 99 or trans_flag = 95 or trans_flag = 101 or((trans_flag = 250 or trans_flag = 254 or trans_flag = 252) and(inquiry_type in (0, 2)) )) and doc_date between \'" + __from_date + "\' and \'" + __to_date + "\' " +
                                ((__closeCreditStatus == "1") ? " and (select credit_status from ar_customer_detail where ar_customer_detail.ar_code = ic_trans.cust_code) in (1,2) " : "") +
                                ((__from_close_date_check != "" && __to_close_date_check != "") ? " and ((select close_credit_date from ar_customer_detail where ar_customer_detail.ar_code = ic_trans.cust_code) between \'" + __from_close_date + "\' and \'" + __to_close_date + "\' )" : "") +
                                ((__whereBranch.Length > 0) ? " and branch_code in (" + __whereBranch.ToString() + ")" : "") +
                                ((__custWhere.Length > 0) ? " and " + __custWhere : "");

                            string __queryCreditNote = "select cust_code,doc_date, credit_day as doc_credit_day, credit_date as due_date " +
                                ", doc_no, trans_flag as doc_type,used_status " +
                                ",'' as ref_doc_no " +
                                ",null as ref_doc_date " +
                                ",-1 * coalesce(total_amount, 0) as amount " +
                                ",-1 * (coalesce(total_amount, 0) + (select coalesce(sum(coalesce(sum_pay_money, 0)), 0) from ap_ar_trans_detail where coalesce(last_status, 0) = 0 and trans_flag in (239) and ic_trans.doc_no = ap_ar_trans_detail.billing_no and ic_trans.doc_date = ap_ar_trans_detail.billing_date " + ((this._mode == _singhaReportEnum.ลูกหนี้คงค้าง) ? " and ap_ar_trans_detail.doc_date <= date('" + __to_date + "') " : "") + " )) as balance_amount " + // and doc_date <= date('2016-01-31')
                                ", branch_code " +
                                ", (select doc_no from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = ic_trans.doc_no and ap_ar_trans_detail.bill_type = ic_trans.trans_flag and ap_ar_trans_detail.trans_flag = 235 and ap_ar_trans_detail.last_status = 0  limit 1) as bill_doc_no " +
                                " from ic_trans where coalesce(last_status, 0) = 0 and((trans_flag = 48 and inquiry_type in (0, 2, 4) ) or trans_flag = 97 or trans_flag = 103) and doc_date  between \'" + __from_date + "\' and \'" + __to_date + "\' " +
                                ((__closeCreditStatus == "1") ? " and (select credit_status from ar_customer_detail where ar_customer_detail.ar_code = ic_trans.cust_code) in (1,2) " : "") +
                                ((__from_close_date_check != "" && __to_close_date_check != "") ? " and ((select close_credit_date from ar_customer_detail where ar_customer_detail.ar_code = ic_trans.cust_code) between \'" + __from_close_date + "\' and \'" + __to_close_date + "\' )" : "") +
                                ((__whereBranch.Length > 0) ? " and branch_code in (" + __whereBranch.ToString() + ")" : "") +
                                ((__custWhere.Length > 0) ? " and " + __custWhere : "");

                            string __queryDoc = __querySale + " union all " + __queryDebtNote + " union all " + __queryCreditNote; // __process._createQuery(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร, dateMode, __custWhere, "", __to_date);

                            string __queryDetail2 = "select cust_code as " + _g.d.ap_ar_resource._ar_code + "," +
                                "doc_no as " + _g.d.ap_ar_resource._doc_no + "," +
                                "doc_date as " + _g.d.ap_ar_resource._doc_date + "," +
                                "due_date as " + _g.d.ap_ar_resource._due_date + "," +
                                "doc_credit_day  as " + _g.d.ap_ar_resource._doc_credit_day + "," +
                                "bill_doc_no as " + _g.d.ap_ar_resource._debt_bill_no + " ," +
                                "amount as " + _g.d.ap_ar_resource._amount + "," +
                                "doc_type as " + _g.d.ap_ar_resource._doc_type + "," +

                                "case when coalesce(date(now())-" + __dueDate + ", 0) <= 0 then balance_amount else 0 end as " + _g.d.ap_ar_resource._out_due + "," +
                                // "case when " + MyLib._myGlobal._sqlDateFunction(__to_date) + "-" + __dueDate + " = 0 then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_0 + "," +
                                "case when date(now())-" + __dueDate + " between " + __term_1_begin + " and " + __term_1_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_1 + "," +
                                "case when date(now())-" + __dueDate + " between " + __term_2_begin + " and " + __term_2_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_2 + "," +
                                "case when date(now())-" + __dueDate + " between " + __term_3_begin + " and " + __term_3_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_3 + "," +
                                //"case when " + MyLib._myGlobal._sqlDateFunction(__to_date) + "-" + __dueDate + " between " + __term_4_begin + " and " + __term_4_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_4 + "," +
                                "case when date(now())-" + __dueDate + " > " + __term_3_end + " then balance_amount else 0 end as " + _g.d.ap_ar_resource._term_4 + "," +

                                "case when date(now())-" + __dueDate + " > 0  then " + MyLib._myGlobal._sqlDateFunction(__to_date) + "-" + __dueDate + " else 0 end as " + _g.d.ap_ar_resource._due_day + "," +
                                "balance_amount as " + _g.d.ap_ar_resource._ar_balance + " from " +
                                "(" + __queryDoc.ToString() + ") as temp2 ";
                            //


                            string __query = "select " + _g.d.ap_ar_resource._ar_code + "," +
                                "(select name_1 from ar_customer where ar_customer.code=temp3." + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ap_ar_resource._ar_name + "," +
                                "(select " + _g.d.ar_customer_detail._credit_date + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + ".ar_code=temp3." + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ar_customer_detail._credit_date + "," +
                                "(select " + _g.d.ar_customer_detail._credit_money + " from ar_customer_detail where ar_customer_detail.ar_code=temp3." + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ap_ar_resource._credit_money + "," +
                                "(select " + _g.d.ar_customer_detail._credit_day + " from ar_customer_detail where ar_customer_detail.ar_code=temp3." + _g.d.ap_ar_resource._ar_code + ") as " + _g.d.ap_ar_resource._credit_day + "," +
                                _g.d.ap_ar_resource._doc_no + "," +
                                _g.d.ap_ar_resource._doc_date + "," +
                                _g.d.ap_ar_resource._due_date + "," +
                                _g.d.ap_ar_resource._doc_credit_day + "," +
                                _g.d.ap_ar_resource._debt_bill_no + "," +
                                _g.d.ap_ar_resource._doc_type + "," +
                                _g.d.ap_ar_resource._due_day + "," +
                                _g.d.ap_ar_resource._out_due + "," +
                                _g.d.ap_ar_resource._amount + "," +
                                //_g.d.ap_ar_resource._term_0 + "," +
                                _g.d.ap_ar_resource._term_1 + "," +
                                _g.d.ap_ar_resource._term_2 + "," +
                                _g.d.ap_ar_resource._term_3 + "," +
                                _g.d.ap_ar_resource._term_4 + "," +
                                //_g.d.ap_ar_resource._term_5 + "," +
                                _g.d.ap_ar_resource._ar_balance + " from (" + __queryDetail2 + ") as temp3 " +
                                " where (" +
                                _g.d.ap_ar_resource._out_due + "<>0 or " +
                                //_g.d.ap_ar_resource._term_0 + "<>0 or " +
                                _g.d.ap_ar_resource._term_1 + "<>0 or " +
                                _g.d.ap_ar_resource._term_2 + "<>0 or " +
                                _g.d.ap_ar_resource._term_3 + "<>0 or " +
                                _g.d.ap_ar_resource._term_4 + "<>0 or " +
                                //_g.d.ap_ar_resource._term_5 + "<>0 or " +
                                _g.d.ap_ar_resource._ar_balance + "<>0 )" + ((this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต) ? " and (select credit_day from ar_customer_detail where ar_customer_detail.ar_code=temp3.ar_code) between " + this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._from_credit_day).ToString() + " and " + this._conditionScreen._screen._getDataNumber(_g.d.ap_ar_resource._to_credit_day).ToString() : "") + " order by " + __orderBy;


                            __queryDetail.Append(__query);
                        }
                        break;
                    #endregion

                    #region รายงานการตัดเช็ค
                    case _singhaReportEnum.รายงานการตัดเช็ค:
                        {
                            string __from_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date));
                            string __to_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date));

                            string __from_ar = this._conditionScreen._screen._getDataStr(_g.d.resource_report._from_ar);
                            string __to_ap = this._conditionScreen._screen._getDataStr(_g.d.resource_report._to_ar);

                            // {0} table_main, {1} date where
                            string __chqBalanceQuery = "coalesce((select sum(amount) from cb_trans_detail where {1} and  {2} = cb_trans_detail.trans_number and doc_type = 2 and last_status = 0 and {0}.chq_due_date =cb_trans_detail.chq_due_date and {0}.bank_code=cb_trans_detail.bank_code and coalesce({0}.bank_branch, '')=coalesce(cb_trans_detail.bank_branch, '') ), 0)";

                            //  ถ้าไม่ได้ ไปดึง จากทะเบียนเช็ค
                            /*
                            string __chqMasterQuery = "select " +
                                MyLib._myGlobal._fieldAndComma(
                                    _g.d.cb_trans_detail._trans_number,
                                    _g.d.cb_trans_detail._bank_code,
                                    _g.d.cb_trans_detail._bank_branch,
                                    _g.d.cb_trans_detail._chq_due_date,
                                    _g.d.cb_trans_detail._sum_amount,
                                    "(" + _g.d.cb_trans_detail._sum_amount + " - " + string.Format(__chqBalanceQuery, "temp2", " doc_date < \'" + __from_date + "\' and cb_trans_detail.doc_ref = temp2.doc_no ") + ") as " + _g.d.cb_trans_detail._balance_amount,
                                    _g.d.cb_trans_detail._doc_no
                                  )
                                +
                                " from " + _g.d.cb_trans_detail._table + " as temp2" +
                                " where doc_type = 2 and last_status = 0  and chq_on_hand = 0 and doc_date <= \'" + __to_date + "\' " +
                                " and (" + _g.d.cb_trans_detail._sum_amount + "-" + string.Format(__chqBalanceQuery, "temp2", " doc_date < \'" + __from_date + "\' and cb_trans_detail.doc_ref = temp2.doc_no ") + " ) > 0" +
                                " order by trans_number , doc_date ";
                            */

                            string __chqMasterQuery = "select " +
                                MyLib._myGlobal._fieldAndComma(
                                    _g.d.cb_chq_list._chq_number,
                                    _g.d.cb_chq_list._bank_code,
                                    _g.d.cb_chq_list._bank_branch,
                                    _g.d.cb_chq_list._chq_due_date,
                                    _g.d.cb_chq_list._amount,
                                    "(" + _g.d.cb_chq_list._amount + " - " + string.Format(__chqBalanceQuery, _g.d.cb_chq_list._table, " doc_date < \'" + __from_date + "\' ", "cb_chq_list.chq_number") + ") as " + _g.d.cb_trans_detail._balance_amount,
                                    _g.d.cb_chq_list._doc_ref
                                    )
                                    +
                                    " from " + _g.d.cb_chq_list._table + "" +
                                    " where " + _g.d.cb_chq_list._ap_ar_type + " = 1  and " + _g.d.cb_chq_list._chq_get_date + " <= \'" + __to_date + "\' " +
                                    " and (" + _g.d.cb_chq_list._amount + " - " + string.Format(__chqBalanceQuery, _g.d.cb_chq_list._table, " doc_date < \'" + __from_date + "\' ", "cb_chq_list.chq_number") + ") > 0" +
                                    " order by chq_number , doc_ref ";

                            __queryMain.Append(__chqMasterQuery);

                            // ยอดยกมา
                            /*
                            string __chqBalanceFirstQuery = "select " + MyLib._myGlobal._fieldAndComma(
                                "0 as doc_sort", 
                                "'' as doc_no", 
                                _g.d.cb_trans_detail._trans_number, 
                                "null as doc_date", 
                                "'' as trans_flag", 
                                "0 as amount", 
                                "(sum_amount -  " + string.Format(__chqBalanceQuery, "temp2", " doc_date < \'" + __from_date + "\'", "temp2.trans_number")  + ") as balance_amount", 
                                "'' as doc_ref")
                                +
                                " from " + _g.d.cb_trans_detail._table +  " as temp2 " +
                                " where doc_type = 2 and last_status = 0 and doc_date < \'" + __from_date + "\' ";
                            */

                            string __chqBalanceFirstQuery = "select " + MyLib._myGlobal._fieldAndComma(
                                "0 as doc_sort",
                                "'' as doc_no",
                                _g.d.cb_chq_list._chq_number + " as " + _g.d.cb_trans_detail._trans_number,
                                "null as doc_date",
                                "'ยอดยกมา' as trans_flag",
                                "0 as amount",
                                    "(" + _g.d.cb_chq_list._amount + " - " + string.Format(__chqBalanceQuery, _g.d.cb_chq_list._table, " doc_date < \'" + __from_date + "\' ", "cb_chq_list.chq_number") + ") as " + _g.d.cb_trans_detail._balance_amount,
                                "doc_ref")
                                +
                                " from " + _g.d.cb_chq_list._table + "" +
                                " where " + _g.d.cb_chq_list._ap_ar_type + " = 1  and " + _g.d.cb_chq_list._chq_get_date + " <= \'" + __to_date + "\' " +
                                " and (" + _g.d.cb_chq_list._amount + " - " + string.Format(__chqBalanceQuery, _g.d.cb_chq_list._table, " doc_date < \'" + __from_date + "\' ", "cb_chq_list.chq_number") + ") > 0";

                            // เดินรายการ
                            string __chqDetailQuery = "select " +
                                MyLib._myGlobal._fieldAndComma(
                                    "1 as doc_sort",
                                    "doc_no",
                                    "trans_number",
                                    "doc_date",
                                    "trans_flag(trans_flag) as trans_flag",
                                    "amount",
                                    "0 as balance_amount",
                                    "case when chq_on_hand = 0 then doc_no else doc_ref end as doc_ref")
                                    +
                                    " from cb_trans_detail " +
                                    " where ap_ar_type = 1 and doc_type = 2 and last_status = 0 and doc_date between \'" + __from_date + "\' and \'" + __to_date + "\' ";

                            __queryDetail.Append(" select doc_sort, trans_number,  doc_date, doc_no,trans_flag, amount, balance_amount, doc_ref from ( ");
                            __queryDetail.Append(__chqBalanceFirstQuery);
                            __queryDetail.Append(" union all ");
                            __queryDetail.Append(__chqDetailQuery);
                            __queryDetail.Append(" ) as temp99 ");
                            __queryDetail.Append(" order by doc_sort, trans_number, doc_date, doc_no");
                        }
                        break;
                    #endregion

                    #region รายงานสรุปการขายสินค้าตามพนักงานขาย
                    case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                        {
                            string __from_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date));
                            string __to_date = MyLib._myGlobal._convertDateToQuery(this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date));
                            string __fromSaleCode = this._conditionScreen._screen._getDataStr(_g.d.resource_report._from_sale_person);
                            string __toSaleCode = this._conditionScreen._screen._getDataStr(_g.d.resource_report._to_sale_person);

                            __queryMain.Append("select " + _g.d.ic_trans._doc_no + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "\",");
                            __queryMain.Append(_g.d.ic_trans._doc_date + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "\",");
                            __queryMain.Append(_g.d.ic_trans._sale_code + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + "\",");
                            __queryMain.Append("( select " + _g.d.erp_user._table + "." + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + " = " + _g.d.ic_trans._sale_code + ") as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_name + "\",");
                            __queryMain.Append(_g.d.ic_trans._cust_code + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + "\",");
                            __queryMain.Append("( select " + _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name + "\",");
                            __queryMain.Append(_g.d.ic_trans._total_discount + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount + "\",");
                            __queryMain.Append(_g.d.ic_trans._total_before_vat + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat + "\",");
                            __queryMain.Append(_g.d.ic_trans._total_vat_value + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value + "\",");
                            __queryMain.Append(_g.d.ic_trans._total_amount + " as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount + "\",");
                            __queryMain.Append(" coalesce((select " + _g.d.cb_trans._total_tax_at_pay + " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + " = " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "), 0) as \"" + _g.d.cb_trans._table + "." + _g.d.cb_trans._total_tax_at_pay + "\" ");
                            __queryMain.Append(" from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._trans_flag + "=44 ");
                            __queryMain.Append(" and " + _g.d.ic_trans._doc_date + " between '" + __from_date + "' and '" + __to_date + "' ");

                            if (__fromSaleCode != "" && __toSaleCode != "")
                            {
                                __queryMain.Append(" and " + _g.d.ic_trans._sale_code + " between '" + __fromSaleCode + "' and '" + __toSaleCode + "' ");
                            }
                            __queryMain.Append(" order by sale_code, doc_date, doc_no ");



                            //------------------------------------------------------------------------------------------------------------------------
                            __queryDetail.Append("select " + _g.d.ic_trans_detail._item_code + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._doc_no + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._doc_date + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_date + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._item_name + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._unit_code + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._price + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._qty + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._sum_amount + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount + "\",");
                            __queryDetail.Append(_g.d.ic_trans_detail._discount_amount + " as \"" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount + "\" ");
                            __queryDetail.Append(" from " + _g.d.ic_trans_detail._table + "  where " + _g.d.ic_trans._trans_flag + "=44 ");
                            __queryDetail.Append(" and doc_date between '" + __from_date + "' and '" + __to_date + "'");

                            if (__fromSaleCode != "" && __toSaleCode != "")
                            {

                            }

                            __queryDetail.Append(" order by doc_date,doc_time");
                        }

                        break;
                    #endregion

                    #region Bank Statement

                    case _singhaReportEnum.BankStatement:
                        //StringBuilder __myquery = new StringBuilder();
                        StringBuilder __incomeFlagCBTransDetail = new StringBuilder();
                        StringBuilder __outcomeFlagCBTransDetail = new StringBuilder();
                        StringBuilder __incomeFlagICTransDetail = new StringBuilder();
                        StringBuilder __outcomeFlagICTransDetail = new StringBuilder();
                        StringBuilder __arFlag = new StringBuilder();
                        // ช่องเงินโอน (รับ)
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค) + ",");
                        __incomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด));
                        // ช่องเงินโอน (จ่าย)
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค) + ",");
                        __outcomeFlagCBTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ));
                        // เงินสดธนาคาร (รับ)
                        __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน) + ",");
                        __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน) + ",");
                        __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน) + ",");
                        __incomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน));
                        // เงินสดธนาคาร (จ่าย)
                        __outcomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน) + ",");
                        __outcomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน) + ",");
                        __outcomeFlagICTransDetail.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน));
                        // ประเภทลูกหนี้
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน) + ",");
                        __arFlag.Append(_g.g._transFlagGlobal._transFlagStr(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้));

                        //
                        String __allFlagCBTransDetail = __incomeFlagCBTransDetail.ToString() + "," + __outcomeFlagCBTransDetail.ToString();
                        String __alFlagICTransDetail = __incomeFlagICTransDetail.ToString() + "," + __outcomeFlagICTransDetail.ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร);
                        //
                        //__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                        this._dateBegin = MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery((MyLib._myGlobal._convertDate(this._conditionScreen._screen._getDataStr(_g.d.cb_resource._date_from)))));
                        this._dateEnd = MyLib._myGlobal._sqlDateFunction(MyLib._myGlobal._convertDateToQuery((MyLib._myGlobal._convertDate(this._conditionScreen._screen._getDataStr(_g.d.cb_resource._date_to)))));
                        this._bookCode = new StringBuilder();
                        for (int __row = 0; __row < this._conditionScreen._gridCondition._rowData.Count; __row++)
                        {
                            if ((int)MyLib._myGlobal._decimalPhase(this._conditionScreen._gridCondition._cellGet(__row, 0).ToString()) == 1)
                            {
                                string __value = this._conditionScreen._gridCondition._cellGet(__row, _g.d.erp_pass_book._code).ToString().Trim().ToUpper();
                                if (__value.Length > 0)
                                {
                                    if (this._bookCode.Length > 0)
                                    {
                                        this._bookCode.Append(",");
                                    }
                                    this._bookCode.Append("\'" + __value + "\'");
                                }
                            }
                        }
                        //
                        string __queryTrans1 = "select 1 as doc_sort, " + MyLib._myGlobal._fieldAndComma("case when " + _g.d.cb_trans_detail._pass_book_code + " is null or " + _g.d.cb_trans_detail._pass_book_code + "=\'\' then " + _g.d.cb_trans_detail._trans_number + " else " + _g.d.cb_trans_detail._pass_book_code + " end as " + _g.d.cb_resource._book_no, " case when " + _g.d.cb_trans_detail._doc_type + "=1 then " + _g.d.cb_trans_detail._chq_due_date + " else " + _g.d.cb_trans_detail._doc_date + " end as " + _g.d.cb_resource._doc_date, _g.d.cb_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time, _g.d.cb_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no, _g.d.cb_trans_detail._remark + " as " + _g.d.cb_resource._remark, "case when " +
                            _g.d.cb_trans_detail._trans_flag + " in (" + __incomeFlagCBTransDetail.ToString() + ") then " + _g.d.cb_trans_detail._amount + " else 0 end as " + _g.d.cb_resource._amount_in, "case when " +
                            _g.d.cb_trans_detail._trans_flag + " in (" + __outcomeFlagCBTransDetail.ToString() + ") then " + _g.d.cb_trans_detail._amount + " else 0 end as " + _g.d.cb_resource._amount_out, "0 as " + _g.d.cb_resource._amount_balance, _g.d.cb_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type, "{1}") +
                            ", (case when (trans_flag in (19, 239)) then (select branch_code from ap_ar_trans where ap_ar_trans.doc_no = cb_trans_detail.doc_no and ap_ar_trans.trans_flag = cb_trans_detail.trans_flag) else (select branch_code from ic_trans where ic_trans.doc_no = cb_trans_detail.doc_no and ic_trans.trans_flag = cb_trans_detail.trans_flag) end) as branch_code " +
                            " from " + _g.d.cb_trans_detail._table + " where case when " + _g.d.cb_trans_detail._pass_book_code + " is null or " + _g.d.cb_trans_detail._pass_book_code + "=\'\' then " + _g.d.cb_trans_detail._trans_number + " else " + _g.d.cb_trans_detail._pass_book_code + " end in (" + this._bookCode.ToString() + ") and " + _g.d.cb_trans_detail._trans_flag + " in (" + __allFlagCBTransDetail + ") " +
                            " and ( case when  " + _g.d.cb_trans_detail._doc_type + "=1 then " + _g.d.cb_trans_detail._chq_due_date + " {0} else " + _g.d.cb_trans_detail._doc_date + " {0} end) " +
                            " and coalesce(" + _g.d.cb_trans_detail._last_status + ", 0)=0 " +
                            " and " + _g.d.cb_trans_detail._doc_type + "=1 "; // " and " + _g.d.cb_trans_detail._doc_type + "<>2 ";

                        string __queryTrans2 = "select 1 as doc_sort, " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code + " as " + _g.d.cb_resource._book_no, _g.d.ic_trans_detail._doc_date + " as " + _g.d.cb_resource._doc_date, _g.d.ic_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time, _g.d.ic_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no, _g.d.cb_resource._remark + " as " + _g.d.cb_trans_detail._remark, "case when " +
            _g.d.ic_trans_detail._trans_flag + " in (" + __incomeFlagICTransDetail.ToString() + ") then " + _g.d.ic_trans_detail._sum_amount + " else (case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินเข้าธนาคาร) + " then " + _g.d.ic_trans_detail._transfer_amount + " else  0  end) end as " + _g.d.cb_resource._amount_in, "case when " +
            _g.d.ic_trans_detail._trans_flag + " in (" + __outcomeFlagICTransDetail.ToString() + ") then " + _g.d.ic_trans_detail._sum_amount + " else (case when " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร) + " then " + _g.d.ic_trans_detail._transfer_amount + " else  0  end) end as " + _g.d.cb_resource._amount_out, "0 as " + _g.d.cb_resource._amount_balance, _g.d.ic_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type, "\'\' as " + _g.d.cb_resource._remark1) +
            ", (select branch_code from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) as branch_code " +
            " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._item_code + " in (" + this._bookCode.ToString() + ") and " + _g.d.ic_trans_detail._trans_flag + " in (" + __alFlagICTransDetail + ") and (" + _g.d.ic_trans_detail._doc_date + " {0}) and coalesce(" + _g.d.ic_trans_detail._last_status + ", 0)=0";

                        // toe ค่าธรรมเนียม
                        string __queryFeeAmount = "select 2 as doc_sort, " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code + " as " + _g.d.cb_resource._book_no, _g.d.ic_trans_detail._doc_date + " as " + _g.d.cb_resource._doc_date, _g.d.ic_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time, _g.d.ic_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no, _g.d.cb_resource._remark + " as " + _g.d.cb_trans_detail._remark
                            , " 0 as " + _g.d.cb_resource._amount_in
                            , "case when " + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร) + ") then " + _g.d.ic_trans_detail._fee_amount + " else 0 end as " + _g.d.cb_resource._amount_out
                            , "0 as " + _g.d.cb_resource._amount_balance, _g.d.ic_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type, "\'\' as " + _g.d.cb_resource._remark1) +
                            ", (select branch_code from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) as branch_code " +
                            " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._item_code + " in (" + this._bookCode.ToString() + ") and " + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร) + ") and (" + _g.d.ic_trans_detail._fee_amount + " > 0 ) and (" + _g.d.ic_trans_detail._doc_date + " {0}) and coalesce(" + _g.d.ic_trans_detail._last_status + ", 0)=0";

                        // ยอดยกมา
                        string __queryBalance = "select " + _g.d.cb_resource._book_no + ",sum(" + _g.d.cb_resource._amount_in + "-" + _g.d.cb_resource._amount_out + ") as " + _g.d.cb_resource._amount_balance + " from (" + String.Format(__queryTrans1 + " union all " + __queryTrans2 + " union all " + __queryFeeAmount, " < " + this._dateBegin, "\'\' as " + _g.d.cb_resource._remark1) + ") as temp1 group by " + _g.d.cb_resource._book_no;
                        __queryMain.Append((__queryBalance));

                        // รายวัน
                        string __apArCode = "(select ap_ar_code from cb_trans where cb_trans.doc_no=" + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no + " and " + " cb_trans.trans_flag=" + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_flag + ")";
                        string __queryTrans = String.Format(__queryTrans1 + " union all " + __queryTrans2 + " union all " + __queryFeeAmount, " between " + this._dateBegin + " and " + this._dateEnd, "case when " + _g.d.cb_trans_detail._trans_flag + " in (" + __arFlag.ToString() + ") then (select name_1 from ar_customer where ar_customer.code=" + __apArCode + ") else  (select name_1 from ap_supplier where ap_supplier.code=" + __apArCode + ") end as " + _g.d.cb_resource._remark1) + " order by " + MyLib._myGlobal._fieldAndComma(_g.d.cb_resource._book_no, _g.d.cb_resource._doc_date, _g.d.cb_resource._doc_time) + ", doc_sort";
                        __queryDetail.Append((__queryTrans));
                        //__myquery.Append("</node>");

                        //ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        this._bookCodeList = new List<string>();
                        //this._dataTableBalance = null;
                        //this._dataTableData = null;
                        //if (_getData.Count > 0)
                        //{
                        //    this._dataTableBalance = ((DataSet)_getData[0]).Tables[0];
                        //    this._dataTableData = ((DataSet)_getData[1]).Tables[0];

                        //}
                        break;
                    #endregion

                    case _singhaReportEnum.รายงานค่าใช้จ่ายอื่น:
                        {
                            __queryMain.Append("select ");
                            __queryMain.Append("doc_date as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "\",");
                            __queryMain.Append("doc_no as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "\",");
                            __queryMain.Append("doc_time as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time + "\",");
                            __queryMain.Append("doc_ref as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref + "\",");
                            __queryMain.Append("doc_ref_date as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date + "\",");
                            __queryMain.Append("cust_code as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + "\",");
                            __queryMain.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code) as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name + "\",");
                            __queryMain.Append("total_value as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_value + "\",");
                            __queryMain.Append("total_discount as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount + "\",");
                            __queryMain.Append("total_value - total_discount as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount + "\",");
                            __queryMain.Append("total_except_vat as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_except_vat + "\",");
                            __queryMain.Append("vat_rate as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._vat_rate + "\",");
                            __queryMain.Append("total_vat_value as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value + "\",");
                            __queryMain.Append("vat_type as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._vat_type + "\",");
                            __queryMain.Append("total_amount as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount + "\",");
                            //__queryMain.Append("doc_date as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "\",");
                            //__queryMain.Append("doc_date as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "\",");
                            //__queryMain.Append("doc_date as \"" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date + "\",");
                            __queryMain.Append("cast(last_status as varchar)||','||cast(used_status as varchar)||','||cast(doc_success as varchar)||','||cast(not_approve_1 as varchar)||','||cast(on_hold as varchar)||','||cast(approve_status as varchar)||','||cast(expire_status  as varchar) as \"ic_trans.last_status\"");
                            __queryMain.Append(" from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + " in(" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() + ") ");
                            __queryMain.Append(" order by doc_date, doc_no ");

                            __queryDetail.Append("select ");
                            __queryDetail.Append("doc_date as \"ic_trans.doc_date\","); // กำหนดเป็น ic_trans เพื่อใช้สำหรับ datatable select
                            __queryDetail.Append("doc_no as \"ic_trans.doc_no\","); // กำหนดเป็น ic_trans เพื่อใช้สำหรับ datatable select
                            __queryDetail.Append("item_code as \"ic_trans_detail.item_code\",");
                            __queryDetail.Append("(select name_1 from erp_income_list where erp_income_list.code = item_code) as \"ic_trans_detail.item_name\",");
                            __queryDetail.Append("remark as \"ic_trans_detail.remark\",");
                            __queryDetail.Append("sum_amount as \"ic_trans_detail.sum_amount\"");

                            __queryDetail.Append(" from " + _g.d.ic_trans_detail._table);
                            __queryDetail.Append(" where " + _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString() + ") ");
                        }
                        break;
                }

                StringBuilder __queryStr = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryMain.ToString()));
                if (__queryDetail.Length > 0)
                    __queryStr.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail.ToString()));
                __queryStr.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr.ToString());

                if (__result.Count > 0)
                {
                    this._dataTable = ((DataSet)__result[0]).Tables[0];


                    if (__result.Count >= 2)
                        this._dataTable2 = ((DataSet)__result[1]).Tables[0];  //__myFrameWork._queryStream(MyLib._myGlobal._databaseName, __fullQuery).Tables[0];

                    if (this._mode == _singhaReportEnum.การ์ดเจ้าหนี้)
                    {
                        this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTable2, "ap_ar_resource." + _g.d.ap_ar_resource._ap_code + ", " + "ap_ar_resource." + _g.d.ap_ar_resource._ap_name, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ap_ar_debt_type + " <> '0' ");
                    }
                    else if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ || this._mode == _singhaReportEnum.ลูกหนี้คงค้าง || this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
                    {
                        this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTable2, _g.d.ap_ar_resource._ar_code + "," + _g.d.ap_ar_resource._ar_name + "," + _g.d.ap_ar_resource._credit_money + "," + _g.d.ap_ar_resource._credit_day + "," + _g.d.ar_customer_detail._credit_date);
                    }
                    else if (this._mode == _singhaReportEnum.BankStatement)
                    {
                        DataTable __book1 = MyLib._dataTableExtension._selectDistinct(this._dataTable, _g.d.cb_resource._book_no);
                        DataTable __book2 = MyLib._dataTableExtension._selectDistinct(this._dataTable2, _g.d.cb_resource._book_no);
                        if (__book1 != null)
                        {
                            for (int __loop = 0; __loop < __book1.Rows.Count; __loop++)
                            {
                                this._bookCodeList.Add(__book1.Rows[__loop][0].ToString());
                            }
                        }
                        if (__book2 != null)
                        {
                            for (int __loop1 = 0; __loop1 < __book2.Rows.Count; __loop1++)
                            {
                                Boolean __found = false;
                                for (int __loop2 = 0; __loop2 < this._bookCodeList.Count; __loop2++)
                                {
                                    if (this._bookCodeList[__loop2].Equals(__book2.Rows[__loop1][0].ToString()))
                                    {
                                        __found = true;
                                        break;
                                    }
                                }
                                if (__found == false)
                                {
                                    this._bookCodeList.Add(__book2.Rows[__loop1][0].ToString());
                                }
                            }
                        }
                        // คำนวณยอดคงเหลือ
                        for (int __loop1 = 0; __loop1 < this._bookCodeList.Count; __loop1++)
                        {
                            string __bookCode = this._bookCodeList[__loop1];
                            decimal __balance = 0M;
                            if (__book1 != null && this._dataTable.Rows.Count > 0)
                            {
                                DataRow[] __select = this._dataTable.Select(_g.d.cb_resource._book_no + "=\'" + __bookCode + "\'");
                                if (__select.Length > 0)
                                {
                                    __balance = MyLib._myGlobal._decimalPhase(__select[0][_g.d.cb_resource._amount_balance].ToString());
                                }
                            }
                            for (int __loop2 = 0; __loop2 < this._dataTable2.Rows.Count; __loop2++)
                            {
                                if (this._dataTable2.Rows[__loop2][_g.d.cb_resource._book_no].ToString().Equals(__bookCode))
                                {
                                    __balance = __balance + (MyLib._myGlobal._decimalPhase(this._dataTable2.Rows[__loop2][_g.d.cb_resource._amount_in].ToString()) - MyLib._myGlobal._decimalPhase(this._dataTable2.Rows[__loop2][_g.d.cb_resource._amount_out].ToString()));
                                    this._dataTable2.Rows[__loop2][_g.d.cb_resource._amount_balance] = __balance;
                                }
                            }
                        }
                    }
                    //else if (this._mode == _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง)
                    //{
                    //    this._dataTable = MyLib._dataTableExtension._selectDistinct(this._dataTable2, _g.d.ic_resource._warehouse + ", " + _g.d.ic_resource._location);

                    //}

                }


            }
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            if (_conditionScreen.DialogResult == DialogResult.Yes)
            {
                // condition header 
                switch (this._mode)
                {
                    case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                    case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                    case _singhaReportEnum.ลูกหนี้คงค้าง:
                        {
                            DateTime __from_date = (this._conditionScreen._screen._getDataDate(_g.d.ap_ar_resource._date_begin));
                            DateTime __to_date = (this._conditionScreen._screen._getDataDate(_g.d.ap_ar_resource._date_end));

                            // __beginDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " +
                            this._report._conditionText = "จากวันที่ " + __from_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " ถึงวันที่ " + __to_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                            if (this._conditionScreen._useBranchCheckbox.Checked == true)
                            {
                                StringBuilder __whereBranch = new StringBuilder();
                                if (this._conditionScreen._useBranchCheckbox.Checked == true)
                                {
                                    for (int __row = 0; __row < this._conditionScreen._gridCondition._rowData.Count; __row++)
                                    {
                                        if (this._conditionScreen._gridCondition._cellGet(__row, 0).ToString().Equals("1"))
                                        {
                                            if (__whereBranch.Length > 0)
                                                __whereBranch.Append(",");

                                            string __branchCode = this._conditionScreen._gridCondition._cellGet(__row, _g.d.erp_branch_list._name_1).ToString();
                                            __whereBranch.Append(__branchCode);
                                        }
                                    }
                                }
                                this._report._conditionText += " สาขา : " + __whereBranch.ToString();
                            }
                        }
                        break;
                    case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                        {
                            DateTime __from_date = (this._conditionScreen._screen._getDataDate(_g.d.resource_report._from_date));
                            DateTime __to_date = (this._conditionScreen._screen._getDataDate(_g.d.resource_report._to_date));

                            string __fromSaleCode = this._conditionScreen._screen._getDataStr(_g.d.resource_report._from_sale_person);
                            string __toSaleCode = this._conditionScreen._screen._getDataStr(_g.d.resource_report._to_sale_person);

                            // __beginDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " +
                            this._report._conditionText = "จากวันที่ " + __from_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " ถึงวันที่ " + __to_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                            if (__fromSaleCode != "" && __toSaleCode != "")
                            {
                                this._report._conditionText += " จากพนักงานขาย : " + __fromSaleCode + " ถึงพนักงานขาย : " + __toSaleCode;
                            }
                        }
                        break;
                    case _singhaReportEnum.BankStatement:
                        {
                            DateTime __from_date = (this._conditionScreen._screen._getDataDate(_g.d.cb_resource._date_from));
                            DateTime __to_date = (this._conditionScreen._screen._getDataDate(_g.d.cb_resource._date_to));
                            this._report._conditionText = "จากวันที่ " + __from_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " ถึงวันที่ " + __to_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                        }
                        break;
                    case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                        {
                            DateTime __to_date = (this._conditionScreen._screen._getDataDate(_g.d.ic_resource._date_end));
                            string __warehouse = this._conditionScreen._screen._getDataStr(_g.d.ic_resource._warehouse);
                            string __location = this._conditionScreen._screen._getDataStr(_g.d.ic_resource._location);


                            this._report._conditionText = "ถึงวันที่ " + __to_date.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " คลัง : " + __warehouse + " ที่เก็บ : " + __location;

                        }
                        break;

                }

                // start process
                this._report__init();
                // this._report._underZeroType = MyLib._myGlobal._intPhase(this._conditionScreen._screen._getDataStr(_g.d.resource_report_vat._format_number_0).ToString());
                this._dataTable = null; // จะได้ load data ใหม่
                //
                //_showFullVatRefer = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._show_full_invoice_doc_no).Equals("1") ? true : false;
                //_showDocNo = this._form_condition._conditionScreenTop._getDataStr(_g.d.resource_report_vat._show_doc_no).Equals("1") ? true : false;
                this._report._build();
            }
        }

        SMLReport._generateLevelClass _levelDetail;
        void _report__init()
        {
            if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ)
            {
                // this._report._resourceTable = _g.d.ap_ar_resource._table;
            }
            else if (this._mode == _singhaReportEnum.BankStatement)
            {
                this._report._resourceTable = _g.d.cb_resource._table;
            }
            else if (this._mode == _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง)
            {
                this._report._resourceTable = _g.d.ic_resource._table;
            }

            // master 
            switch (this._mode)
            {
                case _singhaReportEnum.รายงานการตัดเช็ค:
                case _singhaReportEnum.BankStatement:
                    this._report._level = this._reportInitMain(null, false, false);
                    break;
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    this._report._level = this._reportInitMain(null, true, true);
                    break;
                default:
                    this._report._level = this._reportInitMain(null, true, false);
                    break;
            }

            // detail
            switch (this._mode)
            {
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                case _singhaReportEnum.ลูกหนี้คงค้าง:

                case _singhaReportEnum.รายงานการตัดเช็ค:
                case _singhaReportEnum.BankStatement:

                case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {
                        _levelDetail = this._reportInitDetail(null, true, true);
                    }
                    break;
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    {
                        if (this._conditionScreen._screen._getDataStr(_g.d.resource_report._display_detail).Equals("1"))
                        {
                            _levelDetail = this._reportInitDetail(null, false, true);
                        }
                    }
                    break;
            }


            if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ || this._mode == _singhaReportEnum.ลูกหนี้คงค้าง || this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
            {
                this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
                this._reportGrandTotalColumn(this._report._level.__grandTotal);
                this._report._level._levelGrandTotal = _levelDetail;
            }

            // this._report._resourceTable = ""; // แบบกำหนดเอง
            this._recordCount = 0;

        }


        private void _reportGrandTotalColumn(List<SMLReport._generateColumnListClass> __columnList)
        {
            switch (this._mode)
            {
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ap_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ap_name, null, 70, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass("", "", 20, SMLReport._report._cellType.String, 0));
                    break;
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:

                case _singhaReportEnum.ลูกหนี้คงค้าง:
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_no, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_no, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_date, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_date, 7, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._due_date, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._due_date, 7, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_credit_day, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_credit_day, 7, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ || this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._debt_bill_no, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._debt_bill_no, 9, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }

                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_balance, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._amount, 11, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._out_due, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._out_due, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_1, "1 - 7 วัน", 9, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_2, "8 - 15 วัน", 9, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_3, "16 - 30 วัน", 9, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_4, "มากกว่า 30 วัน", 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                    break;
            }

        }



        SMLReport._generateLevelClass _reportInitMain(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();

            switch (this._mode)
            {
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ap_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ap_name, null, 70, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass("", "", 20, SMLReport._report._cellType.String, 0));
                    break;
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                case _singhaReportEnum.ลูกหนี้คงค้าง:
                    string __closeCreditStatus = this._conditionScreen._screen._getDataStr(_g.d.ap_ar_resource._status).ToString();


                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_code, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_code, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_name, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ar_name, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._credit_day, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._credit_day, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._credit_money, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._credit_money, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                    if (__closeCreditStatus == "1")
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ar_customer_detail._credit_date, "วันที่ปิดสถานะ", 20, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    }

                    __columnList.Add(new SMLReport._generateColumnListClass("", "", 10, SMLReport._report._cellType.String, 0));
                    break;
                case _singhaReportEnum.รายงานการตัดเช็ค:
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._chq_number, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number, 15, SMLReport._report._cellType.String, 0, FontStyle.Bold));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._bank_code, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._bank_code, 15, SMLReport._report._cellType.String, 0, FontStyle.Bold));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._bank_branch, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._bank_branch, 15, SMLReport._report._cellType.String, 0, FontStyle.Bold));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._chq_due_date, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_due_date, 15, SMLReport._report._cellType.String, 0, FontStyle.Bold));
                    __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._amount, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._amount, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
                    //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_chq_list._doc_ref, _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_ref, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold) { _isHideColumn = true });
                    __columnList.Add(new SMLReport._generateColumnListClass("", "", 10, SMLReport._report._cellType.String, 0));
                    break;
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    {
                        FontStyle __fontStyle = FontStyle.Regular;

                        if (this._conditionScreen._screen._getDataStr(_g.d.resource_report._display_detail).Equals("1"))
                        {
                            __fontStyle = FontStyle.Bold;
                        }
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code, "ap_ar_trans.employee", 6, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sale_name, "erp_user_group_detail.user_name", 17, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, "resource_report.custom_code", 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, "pp_plan.cust_name", 19, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat, null, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 11, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans._table + "." + _g.d.cb_trans._total_tax_at_pay, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 14, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _singhaReportEnum.BankStatement:
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._book_no, _g.d.cb_resource._book_no, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._book_name, _g.d.cb_resource._book_name, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._bank_code, _g.d.cb_resource._bank_code, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._book_number, _g.d.cb_resource._book_number, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;
                /*case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._warehouse, _g.d.ic_resource._warehouse, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.LeftRightBottom });
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._location, _g.d.ic_resource._location, 90, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                        //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._balance_qty, _g.d.ic_resource._balance_qty, 40, SMLReport._report._cellType.String, 0, FontStyle.Regular, false, false, SMLReport._report._cellAlign.Center));
                    }
                    break;*/
                case _singhaReportEnum.รายงานค่าใช้จ่ายอื่น:
                    {

                        FontStyle __fontStyle = (false) ? FontStyle.Bold : FontStyle.Regular;

                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 12, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name, 30, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount, null, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_except_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_rate, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_type, null, 5, SMLReport._report._cellType.String, 0, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_next, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));

                    }
                    break;


            }
            return this._report._addLevel("master", levelParent, __columnList, sumTotal, autoWidth);
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            switch (this._mode)
            {
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    {

                        int __columnWidth = 8;
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._ap_ar_debt_type, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._vat_no, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._po_purchase, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._po_cn_balance, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_trans._sum_pay_money_chq, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sum_pay_money_chq, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._cash_money, "เงินสด/เงินโอน", 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.gl_chart_of_account._wht_in, "ภาษีหัก ณ ที่จ่าย", 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ic_resource._discount, "ส่วนลด/เงินขาด", 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ic_trans._sum_advance, "เงินมัดจำ", __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._over_due, "เงินเกิน", __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                        // __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._credit_amount, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._balance_amount, null, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, false, false));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._receive_type, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    break;
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                case _singhaReportEnum.ลูกหนี้คงค้าง:
                    {
                        int __columnWidth = 10;
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_no, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_no, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_date, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_date, 7, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        // __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_type, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._due_date, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._due_date, 7, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._doc_credit_day, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._doc_credit_day, 7, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._debt_bill_no, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._debt_bill_no, 9, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        }
                        else if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._debt_bill_no, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._debt_bill_no, 9, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        }
                        else
                        {
                            __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._debt_bill_no, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._debt_bill_no, 9, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        }

                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_balance, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._amount, 11, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
                        //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._ar_balance, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Bold));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._out_due, _g.d.ap_ar_resource._table + "." + _g.d.ap_ar_resource._out_due, 12, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_0, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_1, "1 - 7 วัน", 9, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_2, "8 - 15 วัน", 9, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_3, "16 - 30 วัน", 9, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_4, "มากกว่า 30 วัน", 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        // __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._term_5, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.ap_ar_resource._due_day, null, __columnWidth, SMLReport._report._cellType.Number, 0, FontStyle.Regular));

                    }
                    break;
                case _singhaReportEnum.รายงานการตัดเช็ค:
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass("", "", 10, SMLReport._report._cellType.String, 0));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._doc_date, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_date, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._doc_no, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._trans_flag, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_flag, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._amount, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._amount, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._balance_amount, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._balance_amount, 20, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular, false, false));
                        __columnList.Add(new SMLReport._generateColumnListClass("", "", 10, SMLReport._report._cellType.String, 0));

                    }
                    break;
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass("", "", 2, SMLReport._report._cellType.String, 0));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 8, SMLReport._report._cellType.String, 0));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 15, SMLReport._report._cellType.String, 0));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 6, SMLReport._report._cellType.String, 0));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 6, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal));
                    }
                    break;

                case _singhaReportEnum.BankStatement:
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass("", "", 2, SMLReport._report._cellType.String, 0));
                        int __columnWidth = 15;
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_time, null, 6, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._branch_code, null, 9, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._doc_type, null, 12, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._remark1, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        //__columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._remark, null, 9, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._amount_in, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_resource._amount_out, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        SMLReport._generateColumnListClass __columnAmountBalance = new SMLReport._generateColumnListClass(_g.d.cb_resource._amount_balance, null, __columnWidth, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular);
                        __columnAmountBalance._isTotal = false;
                        __columnList.Add(__columnAmountBalance);

                    }
                    break;
                /*case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._ic_code, _g.d.ic_resource._ic_code, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.LeftRightBottom });
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._ic_name, _g.d.ic_resource._ic_name, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });

                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._big_qty, _g.d.ic_resource._big_qty, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._small_qty, _g.d.ic_resource._small_qty, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });

                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._sale, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._premium, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._sale_return, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_resource._balance_qty, null, 10, SMLReport._report._cellType.Number, 0, FontStyle.Regular) { _dataBorderStyle = SMLReport._report._columnBorder.RightBottom });
                    }
                    break;*/
                case _singhaReportEnum.รายงานค่าใช้จ่ายอื่น:
                    {
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_code, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._expense_name, 30, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._remark, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._detail, 45, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        __columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._amount2, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));

                    }
                    break;

            }
            return this._report._addLevel("detail", this._report._level, __columnList, sumTotal, autoWidth);

        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }


        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
