using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing.Printing;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Data;
using System.Text;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace SMLReport._formReport
{
    public class _formPrint : IDisposable
    {
        #region form Print Field
        /// <summary>
        /// จำนวนหน้าทั้งหมดที่คำนวณได้ (ตามข้อมูลที่ดึงมา)
        /// </summary>
        private int _pageTotal = 0;
        //
        private int _pageCurrent = 0;
        private int _pageRunning = 0;

        private int _pageIndex = 0;
        private int _subPageIndex = 0;

        public bool _includeDocSeries = false;
        public List<int> _printRange = new List<int>();
        public PrintRange _printRangeType = PrintRange.AllPages;
        public List<int> _printSeriesRange = new List<int>();
        public PrintRange _printSeriesOption = PrintRange.AllPages;

        /// <summary>กำหนด Range ของหน้าที่จะพิมพ์ ex. doc(0) = 1,2 | doc(1) = 3</summary>
        List<_pageForPrintRange> _pagePrintRange;

        private bool _isPreviewDialogResult = true;
        private string _printerNameResult = "";

        //
        //private PageSetupDialog _pageSetupDialog;
        private PrintPreviewDialog _printPreviewDialog; // = new PrintPreviewDialog();
        /// <summary>เอกสารที่จะพิมพ์ กรณีมี size เดียวจะใช้ PrintDocument กรณี MultiSize จะใช้ _myPrintDocument</summary>
        private PrintDocument _printDocument;
        /// <summary>กำหนด หน้าที่พิมพ์จะใช้เอกสารอะไร</summary>
        private List<int> _pageAssign = new List<int>();
        /// <summary>ระบุจำนวนหน้าสูงสุดในเอกสาร</summary>
        private ArrayList _pageTotalAssign;
        private ArrayList _pagePrintRow = new ArrayList();
        private const string _textForMeasure = "SampleText";

        //
        public _formDesigner _form = new _formDesigner();
        private DataTable _a_table;
        private DataTable _b_table;
        private DataTable _c_table;
        private DataTable _d_table;
        private DataTable _e_table;
        private DataTable _f_table;
        private DataTable _g_table;
        private DataTable _h_table;
        private DataTable _i_table;

        // image store 
        private DataTable _image_table;

        /// <summary>เก็บค่าของ record ล่าสุดที่พิมพ์ไป</summary>
        private int _printCurrentRowIndex = -1;

        /// <summary>เก็บค่าของ SUB ROW ในบรรทัด ใช้กรณี ขึ้นหน้าใหม่ไม่ทั้งบรรทัด</summary>
        private int _printSubRowIndex = -1;

        #endregion

        #region Print Public Field

        public List<_conditionClass> _conditon = new List<_conditionClass>();

        public ArrayList _pageTotalList
        {
            get { return _pageTotalAssign; }
        }

        private bool _findDataForPage = false;

        #endregion

        #region formPrint Property

        /// <summary>
        /// หน้าที่ (หน้าปัจจุบัน)
        /// </summary>
        private int _pageNumber
        {
            get
            {
                return this._pageCurrent + 1;
            }
        }

        public void _nextPage()
        {
            this._pageCurrent++;
            this._subPageIndex++;
        }

        /// <summary>
        /// หน้าที่/จำนวนหน้าทั้งหมด
        /// </summary>
        private string _pageNumberStr
        {
            get
            {
                return String.Format("{0}/{1}", this._pageCurrent + 1, this._pageTotal);
            }
        }

        public bool PreviewPrintDialog
        {
            get
            {
                return _isPreviewDialogResult;
            }
            set
            {
                _isPreviewDialogResult = value;
            }
        }

        public string PrinterName
        {
            get
            {
                return _printerNameResult;
            }
            set
            {
                _printerNameResult = value;
            }
        }

        public _formDesigner formDesign
        {
            set
            {
                _form = value;
            }
        }

        public int _currentPageIndex
        {
            get { return _pageIndex; }

        }

        #endregion

        private string _lastCurrencySymbol = "";
        const string _signFieldReplace = "&sign&";

        public _formPrint()
        {
            for (int __loop = 0; __loop < 500; __loop++)
            {
                this._pageAssign.Add(0);
            }
        }

        public _formPrint(string formCode)
        {
            _loadPrintPage(formCode, 0);
        }

        public _formPrint(string formCode, int __screenCode)
        {
            _loadPrintPage(formCode, __screenCode);
        }

        #region Print Private Method

        void _printDocumentMaster_BeginPrint(object sender, PrintEventArgs e)
        {
            this._pageCurrent = 0;
            this._subPageIndex = 0;
        }

        /// <summary>
        /// เก็บ ตำแหน่งของช่วงเอกสารที่พิมพ์ปัจจุบัน
        /// </summary>
        int _multiDocIndex = -1;

        int _rangeDocSeriesIndex = 0;
        /// <summary>
        /// ตอนขึ้น page ใหม่
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            // หากเป็น mode ที่หา DataSet ของ page ตนเอง
            if (this._findDataForPage)
            {
                int __currentDocIndex = ((_myPrintDocument)this._printDocument)._currentDocIndex;
                int __docGroupIndex = (int)_docIndexAssign[__currentDocIndex];

                if (_multiDocIndex != __docGroupIndex)
                {
                    _multiDocIndex = __docGroupIndex;

                    _setDataTableRangeIndex((DataSet)_dataRangeSet[__docGroupIndex]);
                    this._pageTotalAssign = (ArrayList)_listPageTotalAssign[__docGroupIndex];
                    this._pageAssign = (List<int>)_listPageAssign[__docGroupIndex];
                }
            }

            PrintDocument __currentPrint = (PrintDocument)sender;
            _design._pageSetup __myPageSetup = null;
            this._subPageIndex = 0;
            _printCurrentRowIndex = -1;

            if (_printRangeType == PrintRange.SomePages)
            {
                // พิมพ์แบบเลือกหน้า
                if (_includeDocSeries == false)
                {
                    _printRange.Clear();
                    _printRange.AddRange(_pagePrintRange[_rangeDocSeriesIndex]._pageRange);
                }

                if (_includeDocSeries == false || _printSeriesOption == PrintRange.SomePages)
                {
                    if (_rangeDocSeriesIndex < _pagePrintRange.Count)
                    {
                        // เลือกหน้า (pageIndex)
                        this._pageIndex = _pagePrintRange[_rangeDocSeriesIndex]._pageIndex;

                        // กำหนด subPageIndex, _pageCurrent
                        //_pageCurrent = 
                        for (int __i = 0; __i < _pageAssign.Count; __i++)
                        {
                            int __pageIndexCount = _pageAssign[__i];
                            if (__pageIndexCount == _pageIndex)
                            {
                                if (__i == 0)
                                    _pageCurrent = 0;
                                else
                                    _pageCurrent = __i;
                                break;
                            }
                        }

                    }
                }
                _rangeDocSeriesIndex++;
                _drawPaper __tmpPaper = (_drawPaper)this._form._paperList[_pageIndex];
                __myPageSetup = __tmpPaper._myPageSetup;

            }
            else
            {
                // พิมพแบบปรกติ ไม่เลือกหน้า


                int pageNumber = this._pageAssign[this._pageCurrent];

                _drawPaper __tmpPaper = (_drawPaper)this._form._paperList[pageNumber];
                __myPageSetup = __tmpPaper._myPageSetup;
            }

            if (__myPageSetup != null)
            {
                /* ERROR_LAN_PRINT setting page on lan print */
                if (__myPageSetup.Orientation == _design._orientationType.Portrait)
                {
                    if (__myPageSetup._autoPrinterPaperSize == false)
                    {
                        __currentPrint.DefaultPageSettings.PaperSize = new PaperSize(__myPageSetup.PaperSize.ToString(), (int)__myPageSetup._getPageWidthHOI, (int)__myPageSetup._getPageHeightHOI);
                    }
                    __currentPrint.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                }
                else
                {
                    if (__myPageSetup._autoPrinterPaperSize == false)
                    {
                        __currentPrint.DefaultPageSettings.PaperSize = new PaperSize(__myPageSetup.PaperSize.ToString(), (int)__myPageSetup._getPageHeightHOI, (int)__myPageSetup._getPageWidthHOI);
                    }
                    else
                    {

                    }
                    __currentPrint.DefaultPageSettings.Landscape = true;
                    __currentPrint.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                }
            }

        }

        void _printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            Graphics __getGraphics = e.Graphics;
            //__getGraphics.PageUnit = GraphicsUnit.Pixel;
            __getGraphics.SmoothingMode = SmoothingMode.HighQuality;
            __getGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (this._pageAssign[this._pageCurrent] != this._pageIndex)
            {
                _subPageIndex = 0;
            }

            // set pagesize print

            // check print range
            if (_printRangeType == System.Drawing.Printing.PrintRange.SomePages)
            {
                if (_includeDocSeries && _printSeriesOption == PrintRange.AllPages)
                {
                    // ไม่เลือกชุดเอกสาร เอา หน้าที่ n ของ ทุกชุด
                    if (!_printRange.Contains(_subPageIndex + 1))
                    {
                        // ไม่ใช่ พิมพ์ blank โลด

                        // หาหน้าที่จะพิมพ์ต่อไป
                        int __nextPageIndexPrint = _nextPrintPageNumber() - 1;

                        for (; _subPageIndex < __nextPageIndexPrint; _subPageIndex++)
                        {
                            this._drawPage(this._pageCurrent, __getGraphics, false);
                            this._pageCurrent++;
                        }

                        // print page เปล่า ทิ้งใน buffer
                    }
                }
                else
                {
                    //// พิมพ์โดยนับแผ่นเอกสารที่พิมพ์ได้จากชุดนี้ // ยังเป็น bug
                    if (!_printRange.Contains(_subPageIndex + 1)) // หากไม่ใช่หน้าปัจจุบัน
                    {
                        // หาหน้าที่จะพิมพ์ต่อไป
                        int __nextPageIndexPrint = _nextPrintPageNumber() - 1;// _nextPrintePageIndex() - 1;

                        int _pageForPrint = _pageAssign[__nextPageIndexPrint];
                        if (_pageIndex < _pageForPrint) // เช็คหน้าต่อไปที่จะพิมพ์อยู่ในเอกสารชุดเดียวกันหรือไม่
                        {
                            // เอกสารคนละชุด
                            for (; _pageIndex < _pageForPrint; _pageIndex++) // next ไปจนถึงเอกสารชุดทีต้องการ
                            {

                            }

                        }
                        else
                        {
                            // อยู่ในเอกสารชุดเดียวกัน
                            for (; _subPageIndex < __nextPageIndexPrint; _subPageIndex++)
                            {
                                this._drawPage(this._pageCurrent, __getGraphics, false);
                                this._pageCurrent++;
                            }
                        }

                    }
                }
            }

            // Print โลด
            this._drawPage(this._pageCurrent, __getGraphics, true);

            // check more page
            if (_subPageIndex + 1 < (int)_pageTotalAssign[_pageIndex]) // check total page ก่อน
            {
                // หากยังมีหน้าให้พิมพ์อีก 
                if (_printRangeType == System.Drawing.Printing.PrintRange.SomePages)
                {
                    e.HasMorePages = _isPrintNextPage();
                }
                else
                {
                    e.HasMorePages = true;
                }

            }
            else
            {
                e.HasMorePages = false;
            }

            this._pageCurrent++;
            _subPageIndex++;

            /* old check more page
            this._pageCurrent++;
            if (_subPageIndex < (int)_pageTotalAssign[_pageIndex] - 1)
            {
                e.HasMorePages = true;
                _subPageIndex++; // define for drawTable
            }
            else
            {
                e.HasMorePages = false;
            }
             * */

        }

        /// <summary>
        /// ตรวจสอบว่่า มีหน้าต่อไปที่จะให้พิมพ์ หรือไม่
        /// </summary>
        /// <returns></returns>
        private bool _isPrintNextPage()
        {
            // get current index printpage
            int __currentIndex = _printRange.IndexOf(_subPageIndex + 1);
            int _nextPageNumber = -1;

            if (__currentIndex != -1 && __currentIndex != _printRange.Count - 1) //ไม่ใช่หน้าสุดท้าย และไม่ใช่หน้าแรก
            {
                _nextPageNumber = _printRange[__currentIndex + 1];
                // check is last flag (999999999) 
                if (_nextPageNumber == 999999999)
                    return true;

                // เช็คเกินหน้าทั้งหมดหรือไม่
                //if (_includeDocSeries)
                //{
                if ((int)_pageTotalAssign[_pageIndex] >= _nextPageNumber)
                {
                    return true;
                }
                //}
                //else
                //{
                //    if (this._pageTotal >= _nextPageNumber)
                //    {
                //        return true;
                //    }

                //}
            }

            return false;
        }

        private int _nextPrintePageIndex()
        {
            // เช็ค page ด้วยนะ

            foreach (int _pagePrint in _printRange)
            {
                if (_pagePrint == 999999999)
                    return this._pageTotal;
                if (_pagePrint > _pageCurrent)
                    return _pagePrint;
            }
            return -1;
        }

        private int _nextPrintPageNumber()
        {
            foreach (int _pagePrint in _printRange)
            {
                // last page
                if (_pagePrint == 999999999)
                    return (int)_pageTotalAssign[_pageIndex];

                if (_pagePrint > _subPageIndex)
                    return _pagePrint;
            }

            return -1;
        }

        public void _drawPage(int pageNumberForPrint, Graphics e, Boolean _isPrint)
        {
            int pageNumber = this._pageAssign[pageNumberForPrint];
            foreach (Control __control1 in ((_drawPaper)this._form._paperList[pageNumber]).Controls)
            {
                if (__control1.GetType() == typeof(SMLReport._design._drawPanel))
                {
                    SMLReport._design._drawPanel __getControl = (SMLReport._design._drawPanel)__control1;
                    //Graphics __g = __getControl.CreateGraphics();
                    for (int __loop = __getControl._graphicsList._count - 1; __loop >= 0; __loop--)
                    {
                        // call print object
                        _printObject((SMLReport._design._drawObject)__getControl._graphicsList[__loop], pageNumberForPrint, e, _isPrint);
                    }
                }
            }

            // set Page index
            this._pageIndex = pageNumber;

        }

        private void _checkAssignCondition()
        {
            List<string> __conditionList = this._form.__queryEdit._getConditionList();

            if (__conditionList.Count > this._conditon.Count)
            {
                // show screen condition
                _queryParameterForm __paramsform = new _queryParameterForm(__conditionList, this._conditon);
                __paramsform.ShowDialog();

                List<_conditionClass> __newCondition = new List<_conditionClass>();
                for (int __i = 0; __i < __conditionList.Count; __i++)
                {
                    string __fieldName = __conditionList[__i];
                    string __fieldValue = __paramsform._conditionScreen._getDataStr(__conditionList[__i]);
                    _conditionClass __field = new _conditionClass(__fieldName, __fieldValue);

                    __newCondition.Add(__field);
                }

                this._conditon = __newCondition;

                __paramsform.Dispose();
            }
        }

        private bool _buildPage(int pageNumber)
        {
            int __maxPage = 1;
            List<SMLReport._formReport._queryRule> __imageQueryRule = new List<_queryRule>();
            List<string> __imageFieldList = new List<string>();

            PrintDocument __doc = new PrintDocument();
            Graphics __g = __doc.PrinterSettings.CreateMeasurementGraphics();

            // หา Table (Grid) เพือคำนวณว่า มีหน้าเกินหรือเปล่า กรณี ข้อมูลมีมากกว่า 1 หน้า
            foreach (Control __control1 in ((_drawPaper)this._form._paperList[pageNumber]).Controls)
            {
                if (__control1.GetType() == typeof(SMLReport._design._drawPanel))
                {
                    SMLReport._design._drawPanel __getControl = (SMLReport._design._drawPanel)__control1;

                    for (int __loop = 0; __loop < __getControl._graphicsList._count; __loop++)
                    {
                        // เช็คว่ามีการใช้รูปภาพหรือไม่
                        if (__getControl._graphicsList[__loop].GetType() == typeof(SMLReport._design._drawImageField))
                        {
                            SMLReport._design._drawImageField __imageField = (SMLReport._design._drawImageField)__getControl._graphicsList[__loop];

                            if (__imageField.FieldType == _design._FieldType.Image)
                            {
                                __imageQueryRule.Add(__imageField.query);
                                __imageFieldList.Add(__imageField.Field);
                            }

                        }

                        if (__getControl._graphicsList[__loop].GetType() == typeof(SMLReport._design._drawTable))
                        {
                            SMLReport._design._drawTable __drawTable = (SMLReport._design._drawTable)__getControl._graphicsList[__loop];

                            // เช็คว่าใน multifield มีการใช้รูปหรือไม่ จะได้ get รูปมาให้ก่อน

                            if (__drawTable._columnsMultiField)
                            {
                                foreach (SMLReport._design._tableColumns __col in __drawTable.Columns)
                                {
                                    //SMLReport._design._columnMultiFieldCollection __multiFieldCollection = (SMLReport._design._columnMultiFieldCollection) __col._multiFieldCollection
                                    foreach (SMLReport._design._drawObject __drawObject in __col._multiFieldCollection)
                                    {
                                        if (__drawObject.GetType() == typeof(SMLReport._design._drawImageField))
                                        {
                                            SMLReport._design._drawImageField __imageField = (SMLReport._design._drawImageField)__drawObject;

                                            if (__imageField.FieldType == _design._FieldType.Image)
                                            {
                                                __imageQueryRule.Add(__imageField.query);
                                                __imageFieldList.Add(__imageField.Field);
                                                break;
                                            }

                                        }
                                    }
                                }
                            }


                            float __tableWidthScale = (float)__drawTable._actualSize.Width / 100;
                            float __headerHeight = _getHeaderHeight(__drawTable);
                            float __footerHeight = _getFooterHeight(__drawTable);
                            float __rowDetailHeight = __drawTable._actualSize.Height - (__headerHeight + __footerHeight);

                            int __printRowPerPage = __drawTable._getNumAllRows(__rowDetailHeight); // 1 หน้า ปริ้นได้กี่บรรทัด

                            DataTable __data = this._selectDataTable(__drawTable.__queryRuleProperty);// (__drawTable._groupRowDetail) ? this._selectDataTable(__drawTable.__queryRuleProperty, true, __drawTable.Columns, __drawTable._groupDetailFileName) : this._selectDataTable(__drawTable.__queryRuleProperty);

                            //float __rowHeightResult = __drawTable._getRowsHeight(__rowDetailHeight);

                            // คำนวณหาว่าได้กี่หน้า
                            //float __tableHeight = 0;

                            int __rowInPage = 0; // จำนวนบรรทัดใน หน้า
                            int __calcPage = 0; // หน้าที่คำณวนได้
                            if (__data != null)
                            {
                                for (int __row = 0; __row < __data.Rows.Count; __row++)
                                {
                                    //float __rowHeight = 0; // ++
                                    int __numRowInRecord = 0;
                                    for (int __columnLoop = 0; __columnLoop < __drawTable.Columns.Count; __columnLoop++)
                                    {
                                        SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns)__drawTable.Columns[__columnLoop];
                                        string __columnName = this._cutField(__column.Text);


                                        string __dataStr = "";

                                        try
                                        {
                                            __dataStr = _getStringFormat(__data.Rows[__row][__columnName].ToString(), __column.FieldType, __column.FieldFormat, __column._showIsNumberZero);
                                        }
                                        catch
                                        {
                                        }

                                        __dataStr = _getReplaceText(__column._replaceText, __dataStr, __row + 1);

                                        // serial number
                                        if (__column._printSerialNumber && __column._showSerialNewLine == false)
                                        {
                                            //__dataStr = string.Format("{0} {1}", __dataStr, );
                                            string __serial = _getSerialNumber(__drawTable, _design._serialNumberDisplayEnum.SingleLine, 0, __row);
                                            __dataStr = string.Format("{0} {1}", __dataStr, __serial);
                                        }

                                        float __colW = __drawTable.getColumnsWidth(__columnLoop);
                                        // ดูว่าตัดได้กี่บรรทัด

                                        ArrayList __getString = _cutString(__g, __dataStr, __drawTable._font, (__tableWidthScale * __colW), 0, 0, __column._padding);

                                        if (__column._autoLineBreak == false)
                                        {
                                            ArrayList __getStringTmp = new ArrayList();
                                            __getStringTmp.Add(__getString[0]);
                                            __getString = __getStringTmp;
                                        }

                                        if (__getString.Count > __numRowInRecord)
                                        {
                                            __numRowInRecord = __getString.Count;
                                        }

                                        if (__column._printSerialNumber && __column._showSerialNewLine)
                                        {
                                            string __serial = _getSerialNumber(__drawTable, __column._serialNumberDisplay, __column._serialNumberColumn, __row);
                                            __serial = __serial.Replace("||", "\n");

                                            // ตัดว่าได้กี่บรรทัด
                                            if (!__serial.Equals(""))
                                            {
                                                ArrayList __SerialString = _cutString(__g, __serial, __drawTable._font, (int)__tableWidthScale * __colW, 0, 0, __column._padding);
                                                __numRowInRecord += __SerialString.Count;
                                            }
                                        }

                                        if (__column.showLotNumber)
                                        {
                                            string __lotNumberData = __data.Rows[__row][this._cutField(__column.lotFieldName)].ToString();
                                            ArrayList __getDataLotNumber = _getStrLine(__lotNumberData);

                                            __numRowInRecord += __getDataLotNumber.Count;
                                        }

                                    }

                                    // เช็ค จำนวนบรรทัด ว่ามี มากกว่าจำนวนสูงสุดของ ตารางหรือไม่
                                    if (__drawTable._pageOverflowNewLine == true && __numRowInRecord > __printRowPerPage)
                                    {
                                        return false;
                                    }

                                    // คำณวนหน้า
                                    if ((__rowInPage + __numRowInRecord) <= __printRowPerPage)
                                    {
                                        if (__rowInPage == 0 && __numRowInRecord > 0)
                                        {
                                            __calcPage++;
                                        }
                                        // กรณี ไม่ล้นหน้า
                                        __rowInPage += __numRowInRecord;
                                    }
                                    else
                                    {
                                        // เช็ค option ปัดหน้าก่อน ให้ปัดแบบไหน
                                        if (__drawTable._pageOverflowNewLine)
                                        {
                                            // กรณี ขึ้นบรรทัดใหม่ ทั้งบรรทัด ให้ แยกคำณวน
                                            __calcPage++; // ขึ้นหน้าใหม่ 
                                            __rowInPage = __numRowInRecord;
                                        }
                                        else
                                        {
                                            // กรณี ขึ้นหน้าใหม่ เฉพาะบรรทัดที่ล้นไป
                                            int __lineover = (__rowInPage + __numRowInRecord) % __printRowPerPage;
                                            int __calpageRecord = ((__rowInPage + __numRowInRecord) / __printRowPerPage) +  ((__rowInPage == 0 && __lineover > 0 && __numRowInRecord > __printRowPerPage) ? 1 : 0);

                                            __calcPage += __calpageRecord;
                                            __rowInPage = __lineover;
                                        }
                                    }

                                }

                            }

                            if (__calcPage > __maxPage)
                            {
                                __maxPage = __calcPage;
                            }

                        }
                    }
                }

            }

            // set subPage Value 

            this._pageTotal += __maxPage;

            // หน้า Design (ไม่ใช้หน้าพิมพ์) 
            for (int __loop = this._pageRunning; __loop < this._pageTotal; __loop++)
            {
                // กำหนด หน้าที่จะใช้พิมพ์ตามเอกสารจริง ( [0] = paper (0) , [1] = pager (0), [2] = paper (1) )
                this._pageAssign[__loop] = pageNumber;
            }

            // page total assign
            this._pageTotalAssign[pageNumber] = __maxPage;

            this._pageRunning = this._pageTotal;

            // query to Image Store
            if (__imageQueryRule.Count > 0)
            {
                List<string> __imageId = new List<string>();
                for (int __i = 0; __i < __imageQueryRule.Count; __i++)
                {
                    DataTable __tmpTable = _selectDataTable(__imageQueryRule[__i]);
                    for (int __row = 0; __row < __tmpTable.Rows.Count; __row++)
                    {
                        try
                        {
                            string __dataStr = __tmpTable.Rows[__row][this._cutField(__imageFieldList[__i])].ToString();
                            __imageId.Add("'" + __dataStr + "'");
                        }
                        catch
                        {
                        }
                    }
                }

                // query image
                string __where = String.Join(",", __imageId.ToArray());

                // select distinct  images.image_id, (select b.image_file from images as b where b.image_id = images.image_id limit 1) as imagefile from images limit 1

                string __query = "select Image_file from " + "images" + " where image_id in (" + __where + ")";

                MyLib._myFrameWork __framework = new MyLib._myFrameWork();
                //_image_table = __framework._ImageByte(MyLib._myGlobal._databaseName, __query);
                _image_table = new DataTable();

            }

            __g.Dispose();
            __doc.Dispose();

            return true;
        }

        private void _printObject(_design._drawObject __drawObject, int pageNumberForPrint, Graphics e, bool _isPrintPage)
        {
            _printObject(__drawObject, pageNumberForPrint, e, new PointF(0, 0), _isPrintPage);
        }

        private void _printObject(_design._drawObject __drawObject, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrintPage)
        {
            _printObject(__drawObject, pageNumberForPrint, e, __startDrawPoint, 0, _isPrintPage);
        }

        private void _printObject(_design._drawObject __drawObject, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, int __Row, bool _isPrintPage)
        {
            if (__drawObject.GetType() == typeof(SMLReport._design._drawTextField))
            {
                SMLReport._design._drawTextField __drawTextField = (SMLReport._design._drawTextField)__drawObject;
                //try
                //{
                if ((bool)_checkPrintPage(__drawTextField.PringPage))
                {
                    _printTextField(__drawTextField, pageNumberForPrint, e, __startDrawPoint, __Row, _isPrintPage);
                }
                //}
                //catch (Exception ex)
                //{
                //    _printTextField(__drawTextField, pageNumberForPrint, e, __startDrawPoint, __Row, _isPrintPage);

                //}
            }
            else if (__drawObject.GetType() == typeof(SMLReport._design._drawTable))
            {
                SMLReport._design._drawTable __drawTable = (SMLReport._design._drawTable)__drawObject;
                if ((bool)_checkPrintPage(__drawTable.PringPage))
                {
                    if (__drawTable._columnsMultiField)
                    {
                        _printTableMultiField(__drawTable, pageNumberForPrint, e, __startDrawPoint, _isPrintPage);
                    }
                    else
                    {
                        _printTable(__drawTable, pageNumberForPrint, e, __startDrawPoint, _isPrintPage);
                    }
                }
            }

            else if (__drawObject.GetType() == typeof(SMLReport._design._drawLabel))
            {

                SMLReport._design._drawLabel __drawLabel = (SMLReport._design._drawLabel)__drawObject;
                if ((bool)_checkPrintPage(__drawLabel.PringPage))
                {
                    _printLabel(__drawLabel, pageNumberForPrint, e, __startDrawPoint, __Row, _isPrintPage);
                }
            }
            else if (__drawObject.GetType() == typeof(SMLReport._design._drawRectangle))
            {
                SMLReport._design._drawRectangle __drawRect = (SMLReport._design._drawRectangle)__drawObject;
                if ((bool)_checkPrintPage(__drawRect.PringPage))
                {
                    _printRectangle(__drawRect, pageNumberForPrint, e, __startDrawPoint, _isPrintPage);
                }
            }

            else if (__drawObject.GetType() == typeof(SMLReport._design._drawLine))
            {
                SMLReport._design._drawLine __drawLine = (SMLReport._design._drawLine)__drawObject;
                if ((bool)_checkPrintPage(__drawLine.PringPage))
                {
                    _printLine(__drawLine, pageNumberForPrint, e, __startDrawPoint, _isPrintPage);
                }
            }
            else if (__drawObject.GetType() == typeof(SMLReport._design._drawRoundedRectangle))
            {
                SMLReport._design._drawRoundedRectangle __drawRoundRect = (SMLReport._design._drawRoundedRectangle)__drawObject;
                if ((bool)_checkPrintPage(__drawRoundRect.PringPage))
                {
                    _printRoundedRectangle(__drawRoundRect, pageNumberForPrint, e, __startDrawPoint, _isPrintPage);
                }
            }
            else if (__drawObject.GetType() == typeof(SMLReport._design._drawEllipse))
            {
                SMLReport._design._drawEllipse __drawEllipse = (SMLReport._design._drawEllipse)__drawObject;
                if ((bool)_checkPrintPage(__drawEllipse.PringPage))
                {
                    _printEllipse(__drawEllipse, pageNumberForPrint, e, __startDrawPoint, _isPrintPage);
                }
            }
            else if (__drawObject.GetType() == typeof(SMLReport._design._drawImage))
            {
                SMLReport._design._drawImage __drawImage = (SMLReport._design._drawImage)__drawObject;
                if ((bool)_checkPrintPage(__drawImage.PringPage))
                {
                    _printImage(__drawImage, pageNumberForPrint, e, __startDrawPoint, _isPrintPage);
                }
            }
            else if (__drawObject.GetType() == typeof(SMLReport._design._drawImageField))
            {
                SMLReport._design._drawImageField __drawImageField = (SMLReport._design._drawImageField)__drawObject;
                if ((bool)_checkPrintPage(__drawImageField.PringPage))
                {
                    _printImageField(__drawImageField, pageNumberForPrint, e, __startDrawPoint, __Row, _isPrintPage);
                }
            }

        }

        private void _printRectangle(_design._drawRectangle __drawRect, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrint)
        {
            Pen __rectPen = new Pen(__drawRect._lineColor, __drawRect._penWidth);
            __rectPen.DashStyle = __drawRect._LineStyle;

            SolidBrush __rectBrush = new SolidBrush(__drawRect._backColor);

            RectangleF __rect = __drawRect._actualSize;
            __rect.X += __startDrawPoint.X;
            __rect.Y += __startDrawPoint.Y;

            if (_isPrint)
            {
                e.FillRectangle(__rectBrush, Rectangle.Round(__rect));
                //e.DrawRectangle(__rectPen, Rectangle.Round(__rect));
                onDrawRectangle(e, __rectPen, Rectangle.Round(__rect));
            }

            __rectBrush.Dispose();
            __rectPen.Dispose();
        }

        private void _printRoundedRectangle(_design._drawRoundedRectangle __drawRoundedRect, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrint)
        {
            Pen __pen = new Pen(__drawRoundedRect._lineColor, __drawRoundedRect._penWidth);
            __pen.DashStyle = __drawRoundedRect._LineStyle;
            SolidBrush __brush = new SolidBrush(__drawRoundedRect._backColor);

            if (_isPrint)
            {
                this.onDrawRoundRectangle(e, __pen, __brush, __drawRoundedRect);
            }

            __pen.Dispose();
            __brush.Dispose();
        }

        protected virtual void onDrawRoundRectangle(Graphics g, Pen pen, Brush brush, _design._drawRoundedRectangle roundrectangle)
        {

            //Rectangle __RectangleNormalized = _drawRectangle._getNormalizedRectangle(Size);

            GraphicsPath gfxPath = new GraphicsPath();

            gfxPath = roundrectangle._getRectangleGraphic(roundrectangle._actualSize);

            g.FillPath(brush, gfxPath);
            g.DrawPath(pen, gfxPath);

        }

        private void _printEllipse(_design._drawEllipse __drawEllipse, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrint)
        {
            Pen __lineEllipse = new Pen(__drawEllipse._lineColor, __drawEllipse._penWidth);
            __lineEllipse.DashStyle = __drawEllipse._LineStyle;
            SolidBrush __ellipseBG = new SolidBrush(__drawEllipse._backColor);

            if (_isPrint)
            {
                e.FillEllipse(__ellipseBG, __drawEllipse._actualSize);
                e.DrawEllipse(__lineEllipse, __drawEllipse._actualSize);
            }

            __lineEllipse.Dispose();
            __ellipseBG.Dispose();
        }

        private void _printLine(_design._drawLine __drawLine, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrint)
        {
            Pen __linePen = new Pen(__drawLine._lineColor, __drawLine._penWidth);
            __linePen.DashStyle = __drawLine._LineStyle;

            PointF __tmpStartPoint = __drawLine.StartPoint;
            __tmpStartPoint.X += __startDrawPoint.X;
            __tmpStartPoint.Y += __startDrawPoint.Y;

            PointF __tmpEndPoint = __drawLine.EndPoint;
            __tmpEndPoint.X += __startDrawPoint.X;
            __tmpEndPoint.Y += __startDrawPoint.Y;

            if (_isPrint)
            {
                //e.DrawLine(__linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
                onDrawLine(e, __linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
            }

            __linePen.Dispose();
        }

        private void _printImage(_design._drawImage __drawImage, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrint)
        {
            Pen pen = new Pen(__drawImage._lineColor, __drawImage._penWidth);
            SolidBrush brush = new SolidBrush(__drawImage._backColor);


            if (__drawImage.Image != null)
            {
                //if (__drawImage.SizeMode == PictureBoxSizeMode.AutoSize)
                //{
                //    _rectangleResult.Width = _image.Width;
                //    _rectangleResult.Height = _image.Height;
                //}

                int __x = __drawImage._actualSize.X;
                int __y = __drawImage._actualSize.Y;
                int __width = __drawImage._actualSize.Width;
                int __height = __drawImage._actualSize.Height;

                // get Image from sizeType


                Image __newImage = SMLReport._design._drawImage._SizeModeImg(__drawImage._actualSize, __drawImage.Image, __drawImage.SizeMode);

                if (__drawImage.BorderStyle != _design._drawImage.ImageBorderStyleType.None)
                {
                    if (__drawImage._penWidth < 1)
                    {
                        __drawImage._penWidth = 1;
                    }
                    if (__drawImage.BorderStyle == _design._drawImage.ImageBorderStyleType.Line)
                    {
                        int __newLineWidth = __drawImage._penWidth / 2;
                        if (__newLineWidth == 0)
                        {
                            __newLineWidth = 1;
                        }
                        __x += __newLineWidth;
                        __y += __newLineWidth;
                        __width -= __newLineWidth;
                        __height -= __newLineWidth;
                    }
                    Pen __newPen = new Pen(__drawImage._lineColor, __drawImage._penWidth);

                    //g.DrawImage(__newImage, __x, __y, __width, __height);
                    if (_isPrint)
                    {
                        //e.DrawRectangle(__newPen, __drawImage._actualSize);
                        onDrawRectangle(e, __newPen, __drawImage._actualSize);
                    }

                }

                Rectangle __tmpNewRect = new Rectangle(__x, __y, __width, __height);

                if (_isPrint)
                {
                    e.DrawImage(__newImage, __tmpNewRect);
                }
                //g.DrawRectangle(pen, _drawRectangle._getNormalizedRectangle(__tmpNewRect));
            }
            else
            {
                if (_isPrint)
                {
                    e.FillRectangle(brush, __drawImage._actualSize);
                }
            }

            pen.DashStyle = __drawImage._LineStyle;
            if (_isPrint)
            {
                //e.DrawRectangle(pen, __drawImage._actualSize);
                onDrawRectangle(e, pen, __drawImage._actualSize);
            }

            pen.Dispose();
            brush.Dispose();
        }

        private void _printLabel(_design._drawLabel __drawLabel, int pageNumberForPrint, Graphics e, PointF __startPoint, int __Row, bool _isPrint)
        {
            Pen pen = new Pen(__drawLabel._lineColor, __drawLabel._penWidth);
            pen.DashStyle = __drawLabel._lineStyle;
            SolidBrush brush = new SolidBrush(__drawLabel._foreColor);
            SolidBrush __BgBrush = new SolidBrush(__drawLabel._backColor);

            Font __newFont = new Font(__drawLabel._font.FontFamily, __drawLabel._font.Size, __drawLabel._font.Style, __drawLabel._font.Unit, __drawLabel._font.GdiCharSet, __drawLabel._font.GdiVerticalFont);

            RectangleF __LabelRect = __drawLabel._actualSize;
            __LabelRect.X += __startPoint.X;
            __LabelRect.Y += __startPoint.Y;

            if (_isPrint)
            {
                e.FillRectangle(__BgBrush, Rectangle.Round(__LabelRect));
                //e.DrawRectangle(pen, Rectangle.Round(__LabelRect));
                onDrawRectangle(e, pen, Rectangle.Round(__LabelRect));
            }

            //__LabelRect = _getRectangleFPadding(__LabelRect, __drawLabel._padding);

            PointF __drawStrPoint = __LabelRect.Location;


            if (__drawLabel._text != "")
            {
                string __LabelText = _cutGlobalVar(SMLReport._design._drawLabel._replaceLineBreak(__drawLabel._text, __drawLabel._allowLineBreak), __Row + 1);

                SizeF __stringSize = _getTextSize(__LabelText, __newFont, e);
                //SizeF __size = e.MeasureString(__LabelText, __newFont, new PointF(0, 0), _getStringFormat(__drawLabel._textAlign));
                PointF __strDrawPoint = __drawStrPoint;

                if (__drawLabel._charSpace > 0)
                {
                    char[] __char = __LabelText.ToCharArray();

                    __stringSize.Width += (__drawLabel._charSpace * (__char.Length - 1));
                    PointF __getDrawPoint = _getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __stringSize.Width, __stringSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                    PointF __currentDrawCharPoint = new PointF();
                    for (int __i = 0; __i < __char.Length; __i++)
                    {
                        if (_isPrint)
                        {
                            //e.DrawString(__char[__i].ToString(), __newFont, brush, (__drawStrPoint.X + __currentDrawCharPoint.X + __getDrawPoint.X), (__drawStrPoint.Y + __getDrawPoint.Y), StringFormat.GenericTypographic);
                            onDrawString(e, __char[__i].ToString(), __newFont, brush, (__drawStrPoint.X + __currentDrawCharPoint.X + __getDrawPoint.X), (__drawStrPoint.Y + __getDrawPoint.Y), StringFormat.GenericTypographic);
                        }
                        SizeF __currentCharSize = _getTextSize(__char[__i].ToString(), __newFont, e);
                        __currentDrawCharPoint.X += (__currentCharSize.Width + __drawLabel._charSpace);
                    }
                }
                else
                {
                    PointF __getDrawPoint = _getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __stringSize.Width, __stringSize.Height, __drawLabel._textAlign, __drawLabel._padding);
                    if (_isPrint)
                    {
                        //e.DrawString(__LabelText, __newFont, brush, (__strDrawPoint.X + __getDrawPoint.X), (__drawStrPoint.Y + __getDrawPoint.Y), StringFormat.GenericTypographic);
                        onDrawString(e, __LabelText, __newFont, brush, (__strDrawPoint.X + __getDrawPoint.X), (__drawStrPoint.Y + __getDrawPoint.Y), StringFormat.GenericTypographic);
                    }

                    //e.DrawString(__LabelText, __newFont, brush, _getRectangleFPadding(__LabelRect, __drawLabel._padding), _getStringFormat(__drawLabel._textAlign));
                }

            }

            __newFont.Dispose();
            __BgBrush.Dispose();
            pen.Dispose();
            brush.Dispose();

        }

        private void onDrawString(Graphics g, string s, Font font, Brush brush, Point point, StringFormat format)
        {
            this.onDrawString(g, s, font, brush, point.X, point.Y, format);
        }

        /// <summary>
        /// วาดข้อความ
        /// </summary>
        protected virtual void onDrawString(Graphics g, string s, Font font, Brush brush, float x, float y, StringFormat format)
        {
            g.DrawString(s, font, brush, x, y, format);
        }

        private void onDrawRectangle(Graphics g, Pen pen, Rectangle rectangle)
        {
            this.onDrawRectangle(g, pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        protected virtual void onDrawRectangle(Graphics g, Pen pen, float x, float y, float width, float height)
        {
            g.DrawRectangle(pen, x, y, width, height);
        }

        private void onDrawLine(Graphics g, Pen pen, Point pt1, Point pt2)
        {
            this.onDrawLine(g, pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        protected virtual void onDrawLine(Graphics g, Pen pen, float x1, float y1, float x2, float y2)
        {
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        private void _printTextField(_design._drawTextField __drawTextField, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, int __Row, bool _isPrint)
        {
            DataTable __data = this._selectDataTable(__drawTextField.query);
            string __columnName = this._cutField(__drawTextField.Field);
            string __dataStr = "";

            //Control __tmpControl = new Control();
            //Graphics __g = __tmpControl.CreateGraphics();
            //string __textPrintLogs = __drawTextField.Field;
            //Console.WriteLine("Draw Text : " + __textPrintLogs);


            try
            {
                if (__drawTextField._operation == _design._fieldOperation.CurrentRow && __columnName != null)
                {
                    __dataStr = _getStringFormat(_getDataOperation(__data, __columnName, __drawTextField._operation).ToString(), __drawTextField.FieldType, __drawTextField.FieldFormat, __drawTextField._showIsNumberZero);
                }
                else if (__drawTextField._operation != _design._fieldOperation.None && __drawTextField.FieldType == _design._FieldType.Number && __columnName != null)
                {
                    __dataStr = _getStringFormat(_getDataOperation(__data, __columnName, __drawTextField._operation).ToString(), __drawTextField.FieldType, __drawTextField.FieldFormat, __drawTextField._showIsNumberZero);
                }
                else
                {
                    if (__drawTextField._specialField != null && !__drawTextField._specialField.Equals(""))
                    {
                        __dataStr = _mathParser(__drawTextField._specialField, __Row);
                    }
                    else
                    {
                        __dataStr = _getStringFormat(__data.Rows[__Row][__columnName].ToString(), __drawTextField.FieldType, __drawTextField.FieldFormat, __drawTextField._showIsNumberZero);
                    }
                }
            }
            catch
            {
            }

            // for format string # ##### ##### ## #
            if (__drawTextField.FieldType == SMLReport._design._FieldType.String && __drawTextField.FieldFormat != "")
            {
                char[] __showTextChar = __dataStr.ToCharArray();
                Regex __stringRegex = new Regex("#");
                int __strMatch = __stringRegex.Matches(__drawTextField.FieldFormat).Count;


                char[] __charFormat = __drawTextField.FieldFormat.ToCharArray();

                StringBuilder __tmpFormat = new StringBuilder();
                if (__strMatch > 0)
                {
                    int __index = 0;

                    int __minLength = (__charFormat.Length > __showTextChar.Length) ? __showTextChar.Length : __charFormat.Length; // จำนวนตัวอักษรในข้อความไม่เท่ากัน

                    for (int __i = 0; __i < __charFormat.Length; __i++)
                    {
                        string __tmpCharFormat = "";
                        if (__index < __showTextChar.Length)
                        {
                            if (__charFormat[__i].Equals('#'))
                            {
                                __tmpCharFormat = __showTextChar[__index].ToString();
                                __index++;
                            }
                            else
                            {
                                __tmpCharFormat = __charFormat[__i].ToString();
                            }
                            __tmpFormat.Append(__tmpCharFormat);
                        }
                    }
                }

                string __newFormatString = __tmpFormat.ToString();
                __dataStr = __tmpFormat.ToString(); //string.Format(__newFormatString, __showTextChar);
            }

            RectangleF __textRect = __drawTextField._actualSize;
            __textRect.X += __startDrawPoint.X;
            __textRect.Y += __startDrawPoint.Y;

            Pen __pen = new Pen(__drawTextField._lineColor, __drawTextField._penWidth);
            __pen.DashStyle = __drawTextField._lineStyle;
            SolidBrush __brushBG = new SolidBrush(__drawTextField._backColor);

            if (_isPrint)
            {
                e.FillRectangle(__brushBG, Rectangle.Round(__textRect));
                //e.DrawRectangle(__pen, Rectangle.Round(__textRect));
                onDrawRectangle(e, __pen, Rectangle.Round(__textRect));
            }

            if (__dataStr != "")
            {
                if (__drawTextField._replaceText != null && __drawTextField._replaceText.IndexOf(_signFieldReplace) != -1)
                {
                    try
                    {
                        _lastCurrencySymbol = __data.Rows[__Row][this._cutField(__drawTextField.FieldCurrency)].ToString();
                    }
                    catch
                    {
                    }
                }

                __dataStr = SMLReport._design._drawLabel._replaceLineBreak(_getReplaceText(__drawTextField._replaceText, __dataStr), __drawTextField._allowLineBreak);

                ArrayList __getString = SMLReport._design._drawLabel._cutString(__dataStr, __drawTextField._font, __textRect.Width, __drawTextField._charSpace, __drawTextField._charWidth, __drawTextField._padding);
                // ดูว่าตัดได้กี่บรรทัด

                SizeF __dataStrSize = new SizeF();

                if (__drawTextField._multiLine)
                {
                    __dataStrSize = _getTextSize(__getString, __drawTextField._font, e);
                }
                else
                {
                    __dataStrSize = _getTextSize(((__getString.Count == 0) ? "" : (string)__getString[0]), __drawTextField._font, e);
                }


                PointF __tmpPoint = _getPointTextAlingDraw(__textRect.Width, __textRect.Height, __dataStrSize.Width, __dataStrSize.Height, __drawTextField._textAlign, __drawTextField._padding);

                PointF __strDrawPoint = __textRect.Location;
                __strDrawPoint.Y += __tmpPoint.Y;

                for (int __line = 0; __line < __getString.Count; __line++)
                {
                    SizeF __strLineSize = _getTextSize((string)__getString[__line], __drawTextField._font, e);

                    if (__drawTextField._charSpace > 0)
                    {
                        char[] __char = ((string)__getString[__line]).ToCharArray();
                        __strLineSize.Width += (__drawTextField._charSpace * (__char.Length - 1));

                        PointF __getDrawPoint = _getPointTextAlingDraw(__textRect.Width, __textRect.Height, __strLineSize.Width, __strLineSize.Height, __drawTextField._textAlign, __drawTextField._padding);

                        PointF __currentDrawCharPoint = new PointF();
                        for (int __i = 0; __i < __char.Length; __i++)
                        {
                            if (_isPrint)
                            {
                                SolidBrush __drawBrush = new SolidBrush(__drawTextField._foreColor);

                                //e.DrawString(__char[__i].ToString(), __drawTextField._font, new SolidBrush(__drawTextField._foreColor), (__strDrawPoint.X + __getDrawPoint.X + __currentDrawCharPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);
                                onDrawString(e, __char[__i].ToString(), __drawTextField._font, __drawBrush, (__strDrawPoint.X + __getDrawPoint.X + __currentDrawCharPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                                __drawBrush.Dispose();

                            }
                            float __currentCharSize = (__drawTextField._charWidth == -1 || __drawTextField._charWidth == 0) ? _getTextSize(__char[__i].ToString(), __drawTextField._font, e).Width : __drawTextField._charWidth;

                            __currentDrawCharPoint.X += (__currentCharSize + __drawTextField._charSpace);

                        }
                    }
                    else
                    {
                        PointF __getDrawPoint = _getPointTextAlingDraw(__textRect.Width, __textRect.Height, __strLineSize.Width, __strLineSize.Height, __drawTextField._textAlign, __drawTextField._padding);
                        if (_isPrint)
                        {
                            SolidBrush __drawBrush = new SolidBrush(__drawTextField._foreColor);

                            //e.DrawString((string)__getString[__line], __drawTextField._font, new SolidBrush(__drawTextField._foreColor), (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);
                            onDrawString(e, (string)__getString[__line], __drawTextField._font, __drawBrush, (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                            __drawBrush.Dispose();
                        }
                    }

                    __strDrawPoint.Y += (__strLineSize.Height + __drawTextField._lineSpace);

                    if (__drawTextField._multiLine == false)
                        break;
                }
            }

            if (__data != null)
                __data.Dispose();

            __pen.Dispose();
            __brushBG.Dispose();
            //__tmpControl.Dispose();

        }

        private bool _checkIsGroupRow(int currentRow, DataTable table, _design._drawTable drawTable)
        {


            return false;
        }

        private void _printTable(_design._drawTable __drawObject, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrint)
        {
            StringBuilder __log = new StringBuilder();
            __log.Append("Row,Column,Line,String,draw_point, current line size, start draw row point, current column size, current Line Size\n");
            //string __debug = string.Format("row : {0}, Col :{1}, Line : {2}, str : {3}, draw_point : {4}, current line size : {5}, start draw row point : {6}, current column size : {7}, current Line Size : {8}", __row, __columnLoop, __line, __dataList[__line].ToString().Replace(@",", string.Empty), __drawPoint.ToString().Replace(@",", string.Empty), __currentLineSize.ToString().Replace(@",", string.Empty), __startDrawColumnPoint.ToString().Replace(@",", string.Empty), __currentDataColumnSize.ToString().Replace(@",", string.Empty), __currentLineSize.ToString().Replace(@",", string.Empty));
            //__log.Append(__debug + "\n");

            float __tableWidthScale = (float)__drawObject._actualSize.Width / 100;
            PointF __startRowDetailPoint = new PointF();
            DataTable __data = this._selectDataTable(__drawObject.__queryRuleProperty); //(__drawObject._groupRowDetail) ? this._selectDataTable(__drawObject.__queryRuleProperty, true, __drawObject.Columns, __drawObject._groupDetailFileName) : this._selectDataTable(__drawObject.__queryRuleProperty); //this._selectDataTable(__drawObject.__queryRuleProperty);
            ArrayList __colWidth = new ArrayList();
            float __footerHeight = _getFooterHeight(__drawObject);
            int __startPrintRow = -1;
            int __endPrintRow = 0;

            Pen __tablePen = new Pen(__drawObject._lineColor, __drawObject._penWidth);
            __tablePen.DashStyle = __drawObject._LineStyle;
            SolidBrush __tableForeColor = new SolidBrush(__drawObject._foreColor);


            float __y = __drawObject._actualSize.Y;

            // draw border
            SolidBrush __tableBG = new SolidBrush(__drawObject._backColor);

            if (_isPrint)
            {
                e.FillRectangle(__tableBG, __drawObject._actualSize);
                //e.DrawRectangle(__tablePen, __drawObject._actualSize);
                onDrawRectangle(e, __tablePen, __drawObject._actualSize);
            }

            __tableBG.Dispose();

            float __headerHeight = _getHeaderHeight(__drawObject);

            // drawHeader
            if (_isPrint)
            {
                _printTableHeader(__drawObject, pageNumberForPrint, e);
            }

            Pen __tableRowPen = new Pen(__drawObject.RowLineColor, __drawObject._penWidth);
            __tableRowPen.DashStyle = __drawObject.RowLineStyle;

            #region draw tableColumn Line

            Pen __tableColumnPen = new Pen(__drawObject.ColumnsSeparatorLineColor, __drawObject._penWidth);
            __tableColumnPen.DashStyle = __drawObject.ColumnsSeparatorLine;
            __startRowDetailPoint.Y = (__drawObject.ShowHeaderColumns) ? __drawObject._actualSize.Y + __headerHeight : __drawObject._actualSize.Y;

            PointF __currentPoint = new PointF(__drawObject._actualSize.X, __startRowDetailPoint.Y);
            for (int __col = 0; __col < __drawObject.Columns.Count; __col++)
            {
                float __colw = __drawObject.getColumnsWidth(__col) * __tableWidthScale;

                __colWidth.Add(__colw);
                if (__col != __drawObject.Columns.Count - 1)
                {
                    __currentPoint.X += __colw;

                    PointF __startLineCol = new PointF(__currentPoint.X, __startRowDetailPoint.Y);
                    PointF __endLineCol = new PointF(__currentPoint.X, __currentPoint.Y + __drawObject._actualSize.Height - (__footerHeight + __headerHeight));
                    if (_isPrint)
                    {
                        //e.DrawLine(__tableColumnPen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                        onDrawLine(e, __tableColumnPen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                    }
                }
            }

            __tableColumnPen.Dispose();

            #endregion

            // draw table Row

            float __currentYPos = __startRowDetailPoint.Y;
            float __tableRowDetailHeight = 0;
            int __rowIndex = 0;
            int __currentRow = 0;

            float __current = 0;
            float __pageTop = 0;
            float __pageBottom = 0;

            float __rowDetailHeight = __drawObject._actualSize.Height - (__headerHeight + __footerHeight);
            int __printRowPerPage = __drawObject._getNumAllRows(__rowDetailHeight); // 1 หน้า ปริ้นได้กี่บรรทัด


            float __rowHeight = 0;
            float __rowHeightResult = __drawObject._getRowsHeight(__drawObject._actualSize.Height - (__footerHeight + __headerHeight));
            float __rowAverageHeight = __rowHeightResult;

            if (__drawObject._averageRowHeight == false)
            {
                __rowHeightResult = __drawObject._currentRowHeightResult;
            }

            //__pageTop = (_subPageIndex * __drawObject._height) - (_subPageIndex * __headerHeight);
            //__pageBottom = __pageTop + (__drawObject._height - __headerHeight);

            if (__drawObject.RowPerPage > 0 && __drawObject._averageRowHeight == false)
            {
                //__pageBottom = __pageTop + (__rowHeightResult * __drawObject.RowPerPage);
            }

            int __rowInPage = 0;
            for (int __row = 0; __row < __data.Rows.Count; __row++)
            {
                float __x = __drawObject._actualSize.X;
                bool __printRow = false;
                if (__row > _printCurrentRowIndex) // พิมพ์ต่อจากหน้าที่แล้ว
                {
                    if (_checkIsGroupRow(__row, __data, __drawObject) == false)
                    {
                        #region หา จำนวนบรรทัดที่ใช้พิมพ์ใน record นี้

                        int __numRowInRecord = 0;

                        for (int __columnLoop = 0; __columnLoop < __drawObject.Columns.Count; __columnLoop++)
                        {
                            SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns)__drawObject.Columns[__columnLoop];
                            string __columnName = this._cutField(__column.Text);
                            string __dataStr = "";

                            try
                            {
                                __dataStr = (string)_getStringFormat(__data.Rows[__row][__columnName].ToString(), __column.FieldType, __column.FieldFormat, __column._showIsNumberZero);
                            }
                            catch
                            {
                            }

                            __dataStr = _getReplaceText(__column._replaceText, __dataStr, __row + 1);

                            if (__column._printSerialNumber && __column._showSerialNewLine == false)
                            {
                                string __dataSerial = _getSerialNumber(__drawObject, _design._serialNumberDisplayEnum.SingleLine, 0, __row);
                                __dataStr = string.Format("{0} {1}", __dataStr, __dataSerial);
                            }

                            ArrayList __dataList = _cutString(e, __dataStr, __drawObject._font, (float)__colWidth[__columnLoop], 0f, 0f, __column._padding);
                            if (__column._autoLineBreak == false)
                            {
                                ArrayList __getStringTmp = new ArrayList();
                                __getStringTmp.Add(__dataList[0]);
                                __dataList = __getStringTmp;
                            }

                            if (__dataList.Count > __numRowInRecord)
                            {
                                __numRowInRecord = __dataList.Count;
                            }

                            if (__column._printSerialNumber && __column._showSerialNewLine)
                            {
                                string __serial = _getSerialNumber(__drawObject, __column._serialNumberDisplay, __column._serialNumberColumn, __row);
                                __serial = __serial.Replace("||", "\n");

                                if (!__serial.Equals(""))
                                {
                                    ArrayList __serialList = _cutString(e, __serial, __drawObject._font, (float)__colWidth[__columnLoop], 0f, 0f, __column._padding); //SMLReport._design._drawLabel._cutString(__serial, __drawObject._font, (float)__colWidth[__columnLoop], 0f, 0f, __column._padding);
                                    __numRowInRecord += __serialList.Count;
                                }
                            }

                            if (__column.showLotNumber)
                            {
                                string __lotNumberData = __data.Rows[__row][this._cutField(__column.lotFieldName)].ToString();
                                ArrayList __getDataLotNumber = _getStrLine(__lotNumberData);

                                __numRowInRecord += __getDataLotNumber.Count;
                            }
                        }

                        #endregion

                        #region พิมพ์
                        int __printSubRowTotal = __numRowInRecord - (_printSubRowIndex + 1);

                        // ตรวจสอบดูว่า สามารถพิมพ์ลงหน้านี้ได้หรือเปล่า
                        if ((__rowInPage + __printSubRowTotal) <= __printRowPerPage)
                        {
                            #region พิมพ์ตามปรกติ

                            _printCurrentRowIndex = __row;
                            // draw โลด
                            for (int __columnLoop = 0; __columnLoop < __drawObject.Columns.Count; __columnLoop++)
                            {
                                SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns)__drawObject.Columns[__columnLoop];
                                string __columnName = this._cutField(__column.Text);

                                #region Column Image
                                if (__column.FieldType == _design._FieldType.Image)
                                {
                                    string __getItemCode = __data.Rows[__row][__columnName].ToString();
                                    SMLERPControl._getImageData __getImageData = new SMLERPControl._getImageData(__getItemCode);
                                    Image __getPicture = __getImageData._getImageNow();
                                    if (__getPicture != null)
                                    {
                                        // draw image
                                        float __getYPoint = (_printSubRowIndex == -1) ? (__rowInPage) * __rowHeightResult : ((_printSubRowIndex + 1)) * __rowHeightResult;

                                        PointF __drawPoint = new PointF(__x, __currentYPos + __getYPoint); // start Draw Point
                                        SizeF __imageSize = new SizeF((float)__colWidth[__columnLoop], __rowHeightResult);

                                        e.DrawImage(__getPicture, new RectangleF(__drawPoint, __imageSize));

                                    }

                                }
                                #endregion
                                #region Barcode Column
                                else if (__column.FieldType == _design._FieldType.Barcode)
                                {
                                    string __dataStr = "";

                                    try
                                    {
                                        __dataStr = (string)_getStringFormat(__data.Rows[__row][__columnName].ToString(), __column.FieldType, __column.FieldFormat, __column._showIsNumberZero);
                                    }
                                    catch
                                    {
                                    }

                                    if (__column._replaceText != null && __column._replaceText.IndexOf(_signFieldReplace) != -1)
                                    {
                                        try
                                        {
                                            _lastCurrencySymbol = __data.Rows[__row][__drawObject._dataCurrencyCode].ToString();
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    __dataStr = _getReplaceText(__column._replaceText, __dataStr, __row + 1);
                                    try
                                    {
                                        float __getYPoint = (_printSubRowIndex == -1) ? (__rowInPage) * __rowHeightResult : ((_printSubRowIndex + 1)) * __rowHeightResult;
                                        PointF __drawPoint = new PointF(__x, __currentYPos + __getYPoint);
                                        SizeF __imageSize = new SizeF((float)__colWidth[__columnLoop], __rowHeightResult);
                                        RectangleF __draeRect = new RectangleF(__drawPoint, __imageSize);
                                        __draeRect.Y += 1;
                                        __draeRect.Height -= 2;

                                        _design._drawImageField __drawImageField = new _design._drawImageField();
                                        Image __image = __drawImageField._getBarcodeImage(__dataStr, Size.Round(__imageSize), __drawImageField._barcodeAlignment, __column._typeBarcode, __drawImageField._showBarcodeLabel, __drawImageField._barcodeLabelPosition, __drawImageField._font, __drawImageField.RotateFlip, __drawImageField._foreColor, __drawImageField._backColor);
                                        e.DrawImage(__image, Rectangle.Round(__draeRect));
                                    }
                                    catch
                                    {
                                    }
                                    // draw barcode
                                }
                                #endregion
                                #region Text Column
                                else
                                {


                                    string __dataStr = "";

                                    try
                                    {
                                        __dataStr = (string)_getStringFormat(__data.Rows[__row][__columnName].ToString(), __column.FieldType, __column.FieldFormat, __column._showIsNumberZero);

                                        if (__column.lotGroupOperation == _design._fieldOperation.Sum || __column.lotGroupOperation == _design._fieldOperation.Average)
                                        {

                                        }
                                    }
                                    catch
                                    {
                                    }

                                    if (__column._replaceText != null && __column._replaceText.IndexOf(_signFieldReplace) != -1)
                                    {
                                        try
                                        {
                                            _lastCurrencySymbol = __data.Rows[__row][__drawObject._dataCurrencyCode].ToString();
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    __dataStr = _getReplaceText(__column._replaceText, __dataStr, __row + 1);

                                    if (__column._printSerialNumber && __column._showSerialNewLine == false)
                                    {
                                        string __dataSerial = _getSerialNumber(__drawObject, _design._serialNumberDisplayEnum.SingleLine, 0, __row);
                                        __dataStr = string.Format("{0} {1}", __dataStr, __dataSerial);
                                    }

                                    ArrayList __dataList = _cutString(e, __dataStr, __drawObject._font, (float)__colWidth[__columnLoop], 0f, 0f, __column._padding);

                                    if (__column._autoLineBreak == false)
                                    {
                                        ArrayList __getStringTmp = new ArrayList();
                                        __getStringTmp.Add(__dataList[0]);
                                        __dataList = __getStringTmp;
                                    }
                                    #region print serial Line

                                    if (__column._printSerialNumber && __column._showSerialNewLine)
                                    {
                                        string __serial = _getSerialNumber(__drawObject, __column._serialNumberDisplay, __column._serialNumberColumn, __row);
                                        __serial = __serial.Replace("||", "\n");

                                        if (!__serial.Equals(""))
                                        {
                                            ArrayList __serialList = _cutString(e, __serial, __drawObject._font, (float)__colWidth[__columnLoop], 0f, 0f, __column._padding);
                                            for (int __line = 0; __line < __serialList.Count; __line++)
                                            {
                                                __dataList.Add(__serialList[__line].ToString());
                                            }
                                            //for (int __line = 0; __line < __serialList.Count; __line++)
                                            //{
                                            //    SizeF __currentLineSize = _getTextSize(__serialList[__line].ToString(), __drawObject._font, e);
                                            //    PointF __currentLineDrawPoint = _getPointTextAlingDraw(__currentColumnSize.Width, __currentColumnSize.Height, __currentLineSize.Width, __currentLineSize.Height, ContentAlignment.MiddleLeft, __column._padding); // ตำแหน่ง x,y ที่จะวาดในคอลัมน์
                                            //    float __getYPoint = (__rowInPage + __line + __dataList.Count) * __rowHeightResult;

                                            //    PointF __drawPoint = new PointF(__x + __currentLineDrawPoint.X, __currentYPos + __getYPoint + __startDrawColumnPoint.Y); // start Draw Point
                                            //    e.DrawString(__serialList[__line].ToString(), __drawObject._font, __tableForeColor, __drawPoint, StringFormat.GenericTypographic);

                                            //    __calcHeight += __rowHeightResult;
                                            //}
                                        }
                                    }

                                    #endregion

                                    int __lotIndex = 0;
                                    if (__column.showLotNumber)
                                    {
                                        __dataList.Add("LOT");
                                        __lotIndex = __dataList.Count - 1;
                                    }



                                    SizeF __currentColumnSize = new SizeF((float)__colWidth[__columnLoop], __dataList.Count * __rowHeightResult);
                                    SizeF __currentDataColumnSize = _getTextSize(__dataList, __drawObject._font, __rowHeightResult, e);
                                    PointF __startDrawColumnPoint = _getPointTextAlingDraw(__currentColumnSize.Width, __currentColumnSize.Height, __currentDataColumnSize.Width, __currentDataColumnSize.Height, __column.TextAlignment, __column._padding);
                                    float __calcHeight = 0f;

                                    if (__startPrintRow == -1)
                                        __startPrintRow = __row;
                                    __endPrintRow = __row;

                                    __printRow = true;

                                    if (__column.showLotNumber)
                                    {
                                        string __lotNumberData = __data.Rows[__row][this._cutField(__column.lotFieldName)].ToString();

                                        ArrayList __getDataLotNumber = _getStrLine(__lotNumberData);

                                        if (__getDataLotNumber.Count > 1)
                                        {
                                            __dataList[__lotIndex] = __getDataLotNumber[0].ToString();
                                            for (int __rowlot = 1; __rowlot < __getDataLotNumber.Count; __rowlot++)
                                            {
                                                __dataList.Add(__getDataLotNumber[__rowlot].ToString());
                                            }

                                        }
                                        else
                                        {
                                            __dataList[__lotIndex] = __lotNumberData;

                                        }
                                    }

                                    for (int __line = 0; __line < __dataList.Count; __line++)
                                    {

                                        if (_printSubRowIndex == -1 || __line > _printSubRowIndex)
                                        {
                                            SizeF __currentLineSize = _getTextSize(__dataList[__line].ToString(), __drawObject._font, e);
                                            PointF __currentLineDrawPoint = _getPointTextAlingDraw(__currentColumnSize.Width, __currentColumnSize.Height, __currentLineSize.Width, __currentLineSize.Height, __column.TextAlignment, __column._padding); // ตำแหน่ง x,y ที่จะวาดในคอลัมน์
                                            float __getYPoint = (_printSubRowIndex == -1) ? (__rowInPage + __line) * __rowHeightResult : (__line - (_printSubRowIndex + 1)) * __rowHeightResult;
                                            // draw โลด
                                            PointF __drawPoint = new PointF(__x + __currentLineDrawPoint.X, __currentYPos + __getYPoint + __startDrawColumnPoint.Y); // start Draw Point
                                            if (_isPrint)
                                            {
                                                //e.DrawString(__dataList[__line].ToString(), __drawObject._font, __tableForeColor, __drawPoint, StringFormat.GenericTypographic);
                                                onDrawString(e, __dataList[__line].ToString(), __drawObject._font, __tableForeColor, __drawPoint.X, __drawPoint.Y, StringFormat.GenericTypographic);
                                            }
                                            string __debug = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", __row, __columnLoop, __line, __dataList[__line].ToString().Replace(@",", string.Empty), __drawPoint.ToString().Replace(@",", string.Empty), __currentLineSize.ToString().Replace(@",", string.Empty), __startDrawColumnPoint.ToString().Replace(@",", string.Empty), __currentDataColumnSize.ToString().Replace(@",", string.Empty), __currentLineSize.ToString().Replace(@",", string.Empty));
                                            __log.Append(__debug + "\n");
                                            __calcHeight += __rowHeightResult;
                                        }

                                    }
                                }
                                #endregion
                                __x += (float)__colWidth[__columnLoop];
                            }

                            if (_printSubRowIndex != -1)
                            {
                                __numRowInRecord -= (_printSubRowIndex + 1);
                                _printSubRowIndex = -1;
                            }

                            __rowInPage += __numRowInRecord;

                            #endregion
                        }
                        else
                        {
                            // กรณีล้นหน้า

                            if (__drawObject._pageOverflowNewLine == false)
                            {
                                #region วาดโดย Option ไม่ตัดบรรทัด
                                // วาดจนถึงบรรทัดสุดท้าย แล้วขึ้นหน้าใหม่

                                int __numrowsprint = __numRowInRecord - ((__rowInPage + __numRowInRecord) - __printRowPerPage) + (_printSubRowIndex + 1);
                                int __startDrawLineNumber = _printSubRowIndex;
                                int __endDrawLineNumber = __numrowsprint;
                                int __linePrintedBefore = (_printSubRowIndex + 1);

                                // draw โลด
                                for (int __columnLoop = 0; __columnLoop < __drawObject.Columns.Count; __columnLoop++)
                                {
                                    SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns)__drawObject.Columns[__columnLoop];
                                    string __columnName = this._cutField(__column.Text);
                                    string __dataStr = "";

                                    try
                                    {
                                        __dataStr = (string)_getStringFormat(__data.Rows[__row][__columnName].ToString(), __column.FieldType, __column.FieldFormat, __column._showIsNumberZero);
                                    }
                                    catch
                                    {
                                    }

                                    __dataStr = _getReplaceText(__column._replaceText, __dataStr, __row + 1);

                                    if (__column._printSerialNumber && __column._showSerialNewLine == false)
                                    {
                                        string __dataSerial = _getSerialNumber(__drawObject, _design._serialNumberDisplayEnum.SingleLine, 0, __row);
                                        __dataStr = string.Format("{0} {1}", __dataStr, __dataSerial);
                                    }

                                    ArrayList __dataList = _cutString(e, __dataStr, __drawObject._font, (float)__colWidth[__columnLoop], 0f, 0f, __column._padding);
                                    if (__column._autoLineBreak == false)
                                    {
                                        ArrayList __getStringTmp = new ArrayList();
                                        __getStringTmp.Add(__dataList[0]);
                                        __dataList = __getStringTmp;
                                    }
                                    #region print serial Line

                                    if (__column._printSerialNumber && __column._showSerialNewLine)
                                    {
                                        string __serial = _getSerialNumber(__drawObject, __column._serialNumberDisplay, __column._serialNumberColumn, __row);
                                        __serial = __serial.Replace("||", "\n");

                                        if (!__serial.Equals(""))
                                        {
                                            ArrayList __serialList = _cutString(e, __serial, __drawObject._font, (float)__colWidth[__columnLoop], 0f, 0f, __column._padding);

                                            for (int __line = 0; __line < __serialList.Count; __line++)
                                            {
                                                __dataList.Add(__serialList[__line].ToString());
                                            }
                                        }
                                    }

                                    #endregion

                                    int __lotIndex = 0;
                                    if (__column.showLotNumber)
                                    {
                                        __dataList.Add("LOT");
                                        __lotIndex = __dataList.Count - 1;
                                    }

                                    SizeF __currentColumnSize = new SizeF((float)__colWidth[__columnLoop], __dataList.Count * __rowHeightResult);
                                    SizeF __currentDataColumnSize = _getTextSize(__dataList, __drawObject._font, __rowHeightResult, e);
                                    PointF __startDrawColumnPoint = _getPointTextAlingDraw(__currentColumnSize.Width, __currentColumnSize.Height, __currentDataColumnSize.Width, __currentDataColumnSize.Height, __column.TextAlignment, __column._padding);
                                    float __calcHeight = 0f;

                                    if (__startPrintRow == -1)
                                        __startPrintRow = __row;
                                    __endPrintRow = __row;

                                    __printRow = true;

                                    if (__column.showLotNumber)
                                    {
                                        string __lotNumberData = __data.Rows[__row][this._cutField(__column.lotFieldName)].ToString();

                                        ArrayList __getDataLotNumber = _getStrLine(__lotNumberData);

                                        if (__getDataLotNumber.Count > 1)
                                        {
                                            __dataList[__lotIndex] = __getDataLotNumber[0].ToString();
                                            for (int __rowlot = 1; __rowlot < __getDataLotNumber.Count; __rowlot++)
                                            {
                                                __dataList.Add(__getDataLotNumber[__rowlot].ToString());
                                            }

                                        }
                                        else
                                        {
                                            __dataList[__lotIndex] = __lotNumberData;

                                        }
                                    }

                                    for (int __line = 0; __line < __dataList.Count; __line++)
                                    {
                                        // หา line ที่จะพิมพ์

                                        if (__line > __startDrawLineNumber && __line < __endDrawLineNumber)
                                        {
                                            SizeF __currentLineSize = _getTextSize(__dataList[__line].ToString(), __drawObject._font, e);
                                            PointF __currentLineDrawPoint = _getPointTextAlingDraw(__currentColumnSize.Width, __currentColumnSize.Height, __currentLineSize.Width, __currentLineSize.Height, __column.TextAlignment, __column._padding); // ตำแหน่ง x,y ที่จะวาดในคอลัมน์
                                            float __getYPoint = (__rowInPage + (__line - __linePrintedBefore)) * __rowHeightResult;
                                            // draw โลด
                                            PointF __drawPoint = new PointF(__x + __currentLineDrawPoint.X, __currentYPos + __getYPoint + __startDrawColumnPoint.Y); // start Draw Point

                                            if (_isPrint)
                                            {
                                                //e.DrawString(__dataList[__line].ToString(), __drawObject._font, __tableForeColor, __drawPoint, StringFormat.GenericTypographic);
                                                onDrawString(e, __dataList[__line].ToString(), __drawObject._font, __tableForeColor, __drawPoint.X, __drawPoint.Y, StringFormat.GenericTypographic);
                                            }
                                            if (__line > _printSubRowIndex)
                                                _printSubRowIndex = __line;

                                            string __debug = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", __row, __columnLoop, __line, __dataList[__line].ToString().Replace(@",", string.Empty), __drawPoint.ToString().Replace(@",", string.Empty), __currentLineSize.ToString().Replace(@",", string.Empty), __startDrawColumnPoint.ToString().Replace(@",", string.Empty), __currentDataColumnSize.ToString().Replace(@",", string.Empty), __currentLineSize.ToString().Replace(@",", string.Empty));
                                            __log.Append(__debug + "\n");

                                            __calcHeight += __rowHeightResult;
                                        }
                                    }


                                    __x += (float)__colWidth[__columnLoop];
                                }
                                __rowInPage += __numRowInRecord;

                                #endregion
                            }

                            // ขึ้นหน้าใหม่
                            break;
                        }

                        #endregion

                        #region พิมพ์เส้นใต้บรรทัด
                        // draw ine bottom
                        if (__rowIndex > 0 && __printRow == true)
                        {
                            float _printLineYPoint = (__rowInPage - __numRowInRecord) * __rowHeightResult;
                            if (_isPrint)
                            {
                                //e.DrawLine(__tableRowPen, __drawObject._actualSize.X, (__currentYPos + _printLineYPoint), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos + _printLineYPoint));
                                onDrawLine(e, __tableRowPen, __drawObject._actualSize.X, (__currentYPos + _printLineYPoint), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos + _printLineYPoint));
                            }
                        }
                        #endregion
                    }
                    //else
                    //{
                    //    // กรณีเป็นบรรทัดที่ group มา

                    //}
                }

                // สะสมความสูง table
                __tableRowDetailHeight += __rowHeight;
                __y += __rowHeight;
                __currentRow = __row + 1;
                if (__printRow)
                {
                    __rowIndex++;
                }

                // ถ้าข้อมูลที่พิมพ์อยู่ในหน้านี้ ก็ให้ สะสมความสูงของแถวในหน้านี้ 
                if ((__current > __pageTop) && (__current <= __pageBottom))
                {
                    __currentYPos += __rowHeight;
                }
            }

            #region กลบเส้นให้เต็มตาราง
            //int __tableRowsCount = __drawObject._getNumAllRows(__drawObject._actualSize.Height - (__footerHeight + __headerHeight));
            if (_printCurrentRowIndex == __data.Rows.Count - 1 && __rowInPage < __printRowPerPage)
            {
                int __rowEmpty = __printRowPerPage - __rowInPage;
                for (int __i = 0; __i < __rowEmpty; __i++)
                {
                    float _printLineYPoint = (__rowInPage + __i) * __rowHeightResult;
                    if (_isPrint)
                    {
                        //e.DrawLine(__tableRowPen, __drawObject._actualSize.X, (__currentYPos + _printLineYPoint), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos + _printLineYPoint));
                        onDrawLine(e, __tableRowPen, __drawObject._actualSize.X, (__currentYPos + _printLineYPoint), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos + _printLineYPoint));
                    }
                    __currentYPos += __rowHeight;
                }
            }
            #endregion

            __tablePen.Dispose();
            __tableForeColor.Dispose();
            __tableRowPen.Dispose();

            #region Draw Footer

            if (__drawObject.ShowFooter == true)
            {
                Pen __footerPen = new Pen(__drawObject._lineColor, __drawObject._penWidth);
                SolidBrush __footerBrush = new SolidBrush(__drawObject._foreColor);

                // draw footerLine
                PointF __startLineFooter = new PointF(__drawObject._actualSize.X, ((__drawObject._actualSize.Y + __drawObject._actualSize.Height) - __footerHeight));
                PointF __endLineFooter = new PointF(__drawObject._actualSize.X + __drawObject._actualSize.Width, ((__drawObject._actualSize.Y + __drawObject._actualSize.Height) - __footerHeight));

                if (_isPrint)
                {
                    //e.DrawLine(__footerPen, Point.Round(__startLineFooter), Point.Round(__endLineFooter));
                    onDrawLine(e, __footerPen, Point.Round(__startLineFooter), Point.Round(__endLineFooter));
                }

                for (int __i = 0; __i < __drawObject.Footers.Count; __i++)
                {
                    float __footerWidth = __drawObject._getFootersWidth(__i) * __tableWidthScale;

                    // draw Footer Text
                    string __dataStr = "";
                    if (__drawObject.Footers[__i]._operation != _design._fieldOperation.None && __drawObject.Footers[__i].FieldType == _design._FieldType.Number && __drawObject.Footers[__i].Text != null)
                    {
                        __dataStr = _getStringFormat(_getDataOperation(__data, _cutField(__drawObject.Footers[__i].Text), __drawObject.Footers[__i]._operation, __startPrintRow, __endPrintRow).ToString(), __drawObject.Footers[__i].FieldType, __drawObject.Footers[__i].FieldFormat, __drawObject.Footers[__i]._showIsNumberZero);
                        if (__drawObject.Footers[__i]._replaceText != null && !__drawObject.Footers[__i]._replaceText.Equals(""))
                        {
                            __dataStr = _getReplaceFooterText(__drawObject.Footers[__i]._replaceText, __dataStr);
                        }
                    }
                    else
                    {
                        __dataStr = _getReplaceFooterText(__drawObject.Footers[__i]._replaceText, "");
                    }

                    SizeF __tmpSize = _getTextSize(__dataStr, __drawObject._font, e);
                    PointF __tmpPoint = _getPointTextAlingDraw(__footerWidth, __footerHeight, __tmpSize.Width, __tmpSize.Height, __drawObject.Footers[__i].TextAlignment, __drawObject.Footers[__i]._padding);

                    if (_isPrint)
                    {
                        //e.DrawString(__dataStr, __drawObject._font, __footerBrush, new PointF(__startLineFooter.X + __tmpPoint.X, __startLineFooter.Y + __tmpPoint.Y), StringFormat.GenericTypographic);
                        onDrawString(e, __dataStr, __drawObject._font, __footerBrush, (__startLineFooter.X + __tmpPoint.X), (__startLineFooter.Y + __tmpPoint.Y), StringFormat.GenericTypographic);
                    }

                    // draw ColumnsLine
                    if (__i != __drawObject.Footers.Count - 1)
                    {
                        __startLineFooter.X += __footerWidth;

                        PointF __footerColStartPoint = new PointF(__startLineFooter.X, __startLineFooter.Y);
                        PointF __footerColEndPoint = new PointF(__startLineFooter.X, __startLineFooter.Y + __footerHeight);

                        if (_isPrint)
                        {
                            //e.DrawLine(__footerPen, Point.Round(__footerColStartPoint), Point.Round(__footerColEndPoint));
                            onDrawLine(e, __footerPen, Point.Round(__footerColStartPoint), Point.Round(__footerColEndPoint));
                        }
                    }

                }

                __footerPen.Dispose();
                __footerBrush.Dispose();

            }

            #endregion

            // write log
            string __writelogstr = __log.ToString();

        }

        private void _printTableMultiField(_design._drawTable __drawObject, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, bool _isPrint)
        {
            float __tableWidthScale = (float)__drawObject._actualSize.Width / 100;
            PointF __startRowDetailPoint = new PointF();
            float __headerHeight = _getHeaderHeight(__drawObject);
            float __footerHeight = _getFooterHeight(__drawObject);
            ArrayList __colWidth = new ArrayList();
            int __startPrintRow = -1;
            int __endPrintRow = 0;
            DataTable __data = this._selectDataTable(__drawObject.__queryRuleProperty);

            Pen __tablePen = new Pen(__drawObject._lineColor);
            __tablePen.DashStyle = __drawObject._LineStyle;
            SolidBrush __tableForeColor = new SolidBrush(__drawObject._foreColor);

            float __y = __drawObject._actualSize.Y;

            // draw border
            SolidBrush __tableBG = new SolidBrush(__drawObject._backColor);

            e.FillRectangle(__tableBG, __drawObject._actualSize);
            //e.DrawRectangle(__tablePen, __drawObject._actualSize);
            onDrawRectangle(e, __tablePen, __drawObject._actualSize);

            __tableBG.Dispose();

            Pen __tableRowPen = new Pen(__drawObject.RowLineColor, __drawObject._penWidth);
            __tableRowPen.DashStyle = __drawObject.RowLineStyle;

            __startRowDetailPoint.Y = (__drawObject.ShowHeaderColumns) ? __drawObject._actualSize.Y + __headerHeight : __drawObject._actualSize.Y;

            // draw Header Table 
            if (_isPrint)
            {
                _printTableHeader(__drawObject, pageNumberForPrint, e);
            }

            // draw Column line
            Pen __tableColumnPen = new Pen(__drawObject.ColumnsSeparatorLineColor, __drawObject._penWidth);
            __tableColumnPen.DashStyle = __drawObject.ColumnsSeparatorLine;

            PointF __currentPoint = new PointF(__drawObject._actualSize.X, __startRowDetailPoint.Y);

            for (int __col = 0; __col < __drawObject.Columns.Count; __col++)
            {
                float __colw = __drawObject.getColumnsWidth(__col) * __tableWidthScale;

                __colWidth.Add(__colw);

                if (__col != __drawObject.Columns.Count - 1)
                {
                    __currentPoint.X += __colw;

                    PointF __startLineCol = new PointF(__currentPoint.X, __startRowDetailPoint.Y);
                    PointF __endLineCol = new PointF(__currentPoint.X, __currentPoint.Y + (__drawObject._actualSize.Height - (__footerHeight + __headerHeight)));
                    if (_isPrint)
                    {
                        //e.DrawLine(__tableColumnPen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                        onDrawLine(e, __tableColumnPen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                    }
                }
            }

            __tableColumnPen.Dispose();

            // draw table Row
            float __currentYPos = __startRowDetailPoint.Y;
            float __tableRowDetailHeight = 0;
            int __rowIndex = 0;
            int __currentRow = 0;

            //float __pageTop = 0;
            //float __pageBottom = 0;

            // use for fix rowperpage 
            int __startRowPage = 0;
            int __endRowPage = 0;

            int __rowPerPageResult = __drawObject._getNumAllRows(__drawObject._actualSize.Height - __headerHeight - __footerHeight);
            float __rowHeightResult = __drawObject._getRowsHeight(__drawObject._actualSize.Height - __headerHeight - __footerHeight);

            for (int __row = 0; __row < __data.Rows.Count; __row++)
            {
                float __x = __drawObject._actualSize.X;

                //__pageTop = (_subPageIndex * __drawObject._height) - (_subPageIndex * __headerHeight);
                //__pageBottom = __pageTop + (__drawObject._height - __headerHeight);
                bool __printRow = false;

                __startRowPage = (_subPageIndex * __rowPerPageResult);
                __endRowPage = __startRowPage + (__rowPerPageResult - 1);

                for (int __columnLoop = 0; __columnLoop < __drawObject.Columns.Count; __columnLoop++)
                {
                    SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns)__drawObject.Columns[__columnLoop];
                    //float __colW = __drawObject.getColumnsWidth(__columnLoop);

                    //__current = __tableRowDetailHeight + __rowHeightResult;
                    if (__row >= __startRowPage && __row <= __endRowPage)
                    {
                        if (__startPrintRow == -1)
                            __startPrintRow = __row;
                        __endPrintRow = __row;

                        __printRow = true;

                        PointF __drawObjectPoint = new PointF();
                        __drawObjectPoint.X = (__x);
                        __drawObjectPoint.Y = (__currentYPos);

                        // draw multifield
                        for (int __multiField = 0; __multiField < __column._multiFieldCollection.Count; __multiField++)
                        {
                            _printObject(__column._multiFieldCollection[__multiField], pageNumberForPrint, e, __drawObjectPoint, __row, _isPrint);
                        }
                    }

                    //float __colw = __drawObject.getColumnsWidth(__columnLoop);
                    __x += (float)__colWidth[__columnLoop]; // (__colw * __tableWidthScale);
                }
                // end draw Table Columns

                // drawRowsLine
                if (__rowIndex > 0 && __printRow == true)
                {
                    if (_isPrint)
                    {
                        //e.DrawLine(__tableRowPen, __drawObject._actualSize.X, (__currentYPos), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos));
                        onDrawLine(e, __tableRowPen, __drawObject._actualSize.X, (__currentYPos), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos));
                    }
                }

                // สะสมความสูง table
                __tableRowDetailHeight += __rowHeightResult;
                __y += __rowHeightResult;
                __currentRow = __row + 1;
                if (__printRow)
                {
                    __rowIndex++;
                }

                // ถ้าข้อมูลที่พิมพ์อยู่ในหน้านี้ ก็ให้ สะสมความสูงของแถวในหน้านี้ 
                if (__row >= __startRowPage && __row <= __endRowPage)
                {
                    __currentYPos += __rowHeightResult;
                }
            }

            // กลบเส้นให้เต็มตาราง 
            int __tableRowsCount = __drawObject._getNumAllRows(__drawObject._actualSize.Height - (__footerHeight + __headerHeight));
            if (__rowIndex >= 0 && __rowIndex <= __tableRowsCount)
            {
                int __rowEmpty = __tableRowsCount - __rowIndex;
                for (int __i = 0; __i < __rowEmpty; __i++)
                {
                    if (_isPrint)
                    {
                        //e.DrawLine(__tableRowPen, __drawObject._actualSize.X, (__currentYPos), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos));
                        onDrawLine(e, __tableRowPen, __drawObject._actualSize.X, (__currentYPos), __drawObject._actualSize.X + __drawObject._actualSize.Width, (__currentYPos));
                    }
                    __currentYPos += __rowHeightResult;
                }
            }

            __tablePen.Dispose();
            __tableForeColor.Dispose();
            __tableRowPen.Dispose();

            // draw Footer
            if (__drawObject.ShowFooter == true)
            {
                Pen __footerPen = new Pen(__drawObject._lineColor, __drawObject._penWidth);
                SolidBrush __footerBrush = new SolidBrush(__drawObject._foreColor);
                // draw footerLine
                PointF __startLineFooter = new PointF(__drawObject._actualSize.X, __currentYPos);
                PointF __endLineFooter = new PointF(__drawObject._actualSize.X + __drawObject._actualSize.Width, __currentYPos);

                if (_isPrint)
                {
                    //e.DrawLine(__footerPen, Point.Round(__startLineFooter), Point.Round(__endLineFooter));
                    onDrawLine(e, __footerPen, Point.Round(__startLineFooter), Point.Round(__endLineFooter));
                }

                for (int __i = 0; __i < __drawObject.Footers.Count; __i++)
                {
                    float __footerWidth = __drawObject._getFootersWidth(__i) * __tableWidthScale;

                    // draw Footer Text
                    string __dataStr = "";
                    if (__drawObject.Footers[__i]._operation != _design._fieldOperation.None && __drawObject.Footers[__i].FieldType == _design._FieldType.Number && __drawObject.Footers[__i].Text != null)
                    {
                        __dataStr = _getStringFormat(_getDataOperation(__data, _cutField(__drawObject.Footers[__i].Text), __drawObject.Footers[__i]._operation, __startPrintRow, __endPrintRow).ToString(), __drawObject.Footers[__i].FieldType, __drawObject.Footers[__i].FieldFormat, __drawObject.Footers[__i]._showIsNumberZero);
                    }
                    else
                    {
                        __dataStr = _getReplaceFooterText(__drawObject.Footers[__i]._replaceText, "");
                    }

                    SizeF __tmpSize = _getTextSize(__dataStr, __drawObject._font, e);
                    PointF __tmpPoint = _getPointTextAlingDraw(__footerWidth, __footerHeight, __tmpSize.Width, __tmpSize.Height, __drawObject.Footers[__i].TextAlignment, __drawObject.Footers[__i]._padding);

                    if (_isPrint)
                    {
                        //e.DrawString(__dataStr, __drawObject._font, __footerBrush, new PointF(__startLineFooter.X + __tmpPoint.X, __startLineFooter.Y + __tmpPoint.Y), StringFormat.GenericTypographic);
                        onDrawString(e, __dataStr, __drawObject._font, __footerBrush, (__startLineFooter.X + __tmpPoint.X), (__startLineFooter.Y + __tmpPoint.Y), StringFormat.GenericTypographic);
                    }

                    // draw ColumnsLine
                    if (__i != __drawObject.Footers.Count - 1)
                    {
                        __startLineFooter.X += __footerWidth;

                        PointF __footerColStartPoint = new PointF(__startLineFooter.X, __startLineFooter.Y);
                        PointF __footerColEndPoint = new PointF(__startLineFooter.X, __startLineFooter.Y + __footerHeight);
                        if (_isPrint)
                        {
                            //e.DrawLine(__footerPen, Point.Round(__footerColStartPoint), Point.Round(__footerColEndPoint));
                            onDrawLine(e, __footerPen, Point.Round(__footerColStartPoint), Point.Round(__footerColEndPoint));
                        }
                    }

                }

                __footerPen.Dispose();
                __footerBrush.Dispose();
            }
        }

        private void _printTableHeader(_design._drawTable __drawObject, int pageNumberForPrint, Graphics e)
        {
            float __headerHeight = _getHeaderHeight(__drawObject);
            float __tableWidthScale = (float)__drawObject._actualSize.Width / 100;

            // drawHeader
            Pen __headerTablePen = new Pen(__drawObject.HeaderRowLineColor, __drawObject._penWidth);
            __headerTablePen.DashStyle = __drawObject.HeaderRowLineStyle;

            Pen __tablePen = new Pen(__drawObject._lineColor);
            __tablePen.DashStyle = __drawObject._LineStyle;

            SolidBrush __headerTableBg = new SolidBrush(__drawObject.HeaderBackground);
            SolidBrush __headerForeColor = new SolidBrush(__drawObject.HeaderForeColor);

            if (__drawObject.ShowHeaderColumns)
            {
                e.FillRectangle(__headerTableBg, __drawObject._actualSize.X, __drawObject._actualSize.Y, __drawObject._actualSize.Width, __headerHeight);
                //e.DrawRectangle(__headerTablePen, __drawObject._actualSize.X, __drawObject._actualSize.Y, __drawObject._actualSize.Width, __headerHeight);
                onDrawRectangle(e, __headerTablePen, __drawObject._actualSize.X, __drawObject._actualSize.Y, __drawObject._actualSize.Width, __headerHeight);

                // draw Line Header
                PointF __currentHeaderPoint = new PointF();

                for (int __col = 0; __col < __drawObject.Columns.Count; __col++)
                {
                    float __colw = __drawObject.getColumnsWidth(__col) * __tableWidthScale;
                    SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns)__drawObject.Columns[__col];

                    // draw HeaderLine
                    if (__col != __drawObject.Columns.Count - 1)
                    {
                        PointF __startLineCol = new PointF(__drawObject._actualSize.X + __currentHeaderPoint.X + __colw, __drawObject._actualSize.Y);
                        PointF __endLineCol = new PointF(__drawObject._actualSize.X + __currentHeaderPoint.X + __colw, __drawObject._actualSize.Y + __headerHeight);
                        //e.DrawLine(__headerTablePen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                        onDrawLine(e, __headerTablePen, Point.Round(__startLineCol), Point.Round(__endLineCol));
                    }

                    // draw HeaderString
                    ArrayList __getString = SMLReport._design._drawLabel._cutString(e, SMLReport._design._drawLabel._replaceLineBreak(__column.HeaderText, true), __drawObject._headerFont, __drawObject.getColumnsWidth(__col) * ((float)__drawObject._actualSize.Width / 100), 0, 0, new Padding(0));
                    SizeF __headerTextSize = _getTextSize(__getString, __drawObject._headerFont, e);

                    PointF __headerDrawPoint = _getPointTextAlingDraw(__colw, __headerHeight, __headerTextSize.Width, __headerTextSize.Height, __column.HeaderAlignment, new Padding(0));

                    PointF __drawStringPoint = __drawObject._actualSize.Location;
                    __drawStringPoint.Y += __headerDrawPoint.Y;

                    for (int __line = 0; __line < __getString.Count; __line++)
                    {
                        SizeF __strSize = _getTextSize(__getString[__line].ToString(), __drawObject._headerFont, e);
                        PointF __strPoint = _getPointTextAlingDraw(__colw, __headerHeight, __strSize.Width, __strSize.Height, __column.HeaderAlignment, new Padding(0));
                        //e.DrawString(__getString[__line].ToString(), __drawObject._headerFont, __headerForeColor, Point.Round(new PointF((__drawStringPoint.X + __strPoint.X + __currentHeaderPoint.X), __drawStringPoint.Y)), StringFormat.GenericTypographic);
                        onDrawString(e, __getString[__line].ToString(), __drawObject._headerFont, __headerForeColor, Point.Round(new PointF((__drawStringPoint.X + __strPoint.X + __currentHeaderPoint.X), __drawStringPoint.Y)), StringFormat.GenericTypographic);
                        __drawStringPoint.Y += __strSize.Height;
                    }
                    __currentHeaderPoint.X += __colw;
                }

                // draw bottomHeaderLine
                PointF __bottomHeaderStartPoint = new PointF(__drawObject._actualSize.X, __drawObject._actualSize.Y + __headerHeight);
                PointF __bottomHeaderEndPoint = new PointF(__drawObject._actualSize.X + __drawObject._actualSize.Width, __drawObject._actualSize.Y + __headerHeight);

                //e.DrawLine(__tablePen, __bottomHeaderStartPoint, __bottomHeaderEndPoint);
                onDrawLine(e, __tablePen, __bottomHeaderStartPoint.X, __bottomHeaderStartPoint.Y, __bottomHeaderEndPoint.X, __bottomHeaderEndPoint.Y);
            }

            __headerTablePen.Dispose();
            __tablePen.Dispose();
            __headerForeColor.Dispose();
            __headerTableBg.Dispose();


        }

        private void _printImageField(_design._drawImageField __drawImageField, int pageNumberForPrint, Graphics e, PointF __startDrawPoint, int __Row, bool _isPrint)
        {
            bool __compareCondition = false;
            string __dataStr = "";
            try
            {
                DataTable __data = this._selectDataTable(__drawImageField.query);
                string __columnName = this._cutField(__drawImageField.Field);
                __dataStr = __data.Rows[__Row][__columnName].ToString();
                __compareCondition = __dataStr.ToString().Equals(__drawImageField._fieldCompareValue.ToString());
            }
            catch
            {
            }

            RectangleF __imageRect = __drawImageField._actualSize;
            __imageRect.X += __startDrawPoint.X;
            __imageRect.Y += __startDrawPoint.Y;

            Pen __pen = new Pen(__drawImageField._lineColor, __drawImageField._penWidth);
            __pen.DashStyle = __drawImageField._LineStyle;
            SolidBrush __bg = new SolidBrush(__drawImageField._backColor);
            SolidBrush __brush = new SolidBrush(__drawImageField._foreColor);

            if (_isPrint)
            {
                e.FillRectangle(__bg, Rectangle.Round(__imageRect));
                //e.DrawRectangle(__pen, Rectangle.Round(__imageRect));
                onDrawRectangle(e, __pen, Rectangle.Round(__imageRect));
            }

            if (__drawImageField.FieldType == _design._FieldType.Image)
            {
                try
                {
                    if (!__dataStr.Equals(""))
                    {
                        // query image
                        //string __tabble = "images"; //this._Tablename.Length == 0 ? "images" : this._Tablename;  // swith table from imageField
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        byte[] __databyte = new byte[1024];

                        string __qurey = __drawImageField._fieldCompareValue.Replace("@value@", __dataStr); // _processQueryAndCondition(this._form.__queryEdit._getQuery(__drawImageField.query.ToString()), this._conditon);   // "select Image_file from " + __tabble + " where image_id ='" + __dataStr + "' LIMIT 1";

                        // ดึงรูป จาก quer 
                        __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._databaseName, __qurey);

                        // write temp to local
                        //FileStream oOutput = File.Create(this.strPathname + @"\" + __guid_code + ".jpg", __databyte.Length);
                        //oOutput.Write(__databyte, 0, __databyte.Length);
                        //oOutput.Close();
                        //oOutput.Dispose();

                        Image __image = Image.FromStream(new MemoryStream(__databyte));
                        // draw Image 

                        __image = SMLReport._design._drawImage._SizeModeImg(Rectangle.Round(__imageRect), __image, __drawImageField.SizeMode);

                        if (_isPrint)
                        {
                            e.DrawImage(__image, Rectangle.Round(__imageRect));
                        }
                    }
                }
                catch
                {
                }
            }
            else if (__drawImageField.FieldType == _design._FieldType.Barcode)
            {
                try
                {
                    if (_isPrint)
                    {
                        Image __image = __drawImageField._getBarcodeImage(__dataStr, Rectangle.Round(__imageRect).Size, __drawImageField._barcodeAlignment, __drawImageField._typeBarcode, __drawImageField._showBarcodeLabel, __drawImageField._barcodeLabelPosition, __drawImageField._font, __drawImageField.RotateFlip, __drawImageField._foreColor, __drawImageField._backColor);
                        e.DrawImage(__image, Rectangle.Round(__imageRect));
                    }
                }
                catch
                {
                }
            }
            else
            {

                if (__compareCondition || (__drawImageField._alwaysShow))
                {
                    if (__drawImageField._showText != null && __drawImageField._showText != "")
                    {
                        SizeF __tmpSize = _getTextSize(__drawImageField._showText, __drawImageField._font, e);
                        PointF __tmpPoint = _getPointTextAlingDraw(__imageRect.Width, __imageRect.Height, __tmpSize.Width, __tmpSize.Height, ContentAlignment.MiddleCenter);

                        if (_isPrint)
                        {
                            //e.DrawString(__drawImageField._showText, __drawImageField._font, __brush, __imageRect.X + __tmpPoint.X, __imageRect.Y + __tmpPoint.Y, StringFormat.GenericTypographic);
                            onDrawString(e, __drawImageField._showText, __drawImageField._font, __brush, __imageRect.X + __tmpPoint.X, __imageRect.Y + __tmpPoint.Y, StringFormat.GenericTypographic);
                        }
                    }
                    else if (__drawImageField._imageObjectName != null && __drawImageField.__imageList != null)
                    {
                        Image __image = __drawImageField.__imageList._getImageFromName(__drawImageField._imageObjectName);

                        if (__image != null)
                        {
                            __image = SMLReport._design._drawImage._SizeModeImg(Rectangle.Round(__imageRect), __image, __drawImageField.SizeMode);
                            if (_isPrint)
                            {
                                e.DrawImage(__image, Rectangle.Round(__imageRect));
                            }
                        }
                    }

                }
            }

            __pen.Dispose();
            __bg.Dispose();
            __brush.Dispose();
        }

        private string _processQueryAndCondition(string source, List<_conditionClass> condition)
        {
            if (source.Trim().Length == 0)
            {
                return null;
            }
            string __result = source;
            for (int __loop = 0; __loop < condition.Count; __loop++)
            {

                __result = __result.Replace("#" + condition[__loop]._fieldName + "#", condition[__loop]._value);
            }

            return __result;
        }

        private DataTable _processQuery(string source, List<_conditionClass> condition)
        {

            string __result = _processQueryAndCondition(source, condition);
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                return __myFrameWork._queryShort(__result).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        private DataTable _selectDataTable(string queryRuleProperty)
        {
            switch (queryRuleProperty.ToUpper())
            {
                case "A": return this._a_table;
                case "B": return this._b_table;
                case "C": return this._c_table;
                case "D": return this._d_table;
                case "E": return this._e_table;
                case "F": return this._f_table;
                case "G": return this._g_table;
                case "H": return this._h_table;
                case "I": return this._i_table;
                default: return null;
            }
        }

        /// <summary>
        /// เพื่อ group ตารางโดยเฉพาะ
        /// </summary>
        /// <param name="queryRuleProperty"></param>
        /// <param name="rowGroup"></param>
        /// <returns></returns>
        public DataTable _selectDataTable(SMLReport._formReport._queryRule queryRuleProperty, bool rowGroup, _design._tableColumnsCollection columnList, string fieldDetailGroup)
        {
            switch (queryRuleProperty)
            {
                case _queryRule.A: return _groupRow(this._a_table, columnList, fieldDetailGroup);
                case _queryRule.B: return _groupRow(this._b_table, columnList, fieldDetailGroup);
                case _queryRule.C: return _groupRow(this._c_table, columnList, fieldDetailGroup);
                case _queryRule.D: return _groupRow(this._d_table, columnList, fieldDetailGroup);
                case _queryRule.E: return _groupRow(this._e_table, columnList, fieldDetailGroup);
                case _queryRule.F: return _groupRow(this._f_table, columnList, fieldDetailGroup);
                case _queryRule.G: return _groupRow(this._g_table, columnList, fieldDetailGroup);
                case _queryRule.H: return _groupRow(this._h_table, columnList, fieldDetailGroup);
                case _queryRule.I: return _groupRow(this._i_table, columnList, fieldDetailGroup);
                default: return null;
            }
        }

        public DataTable _groupRow(DataTable table, _design._tableColumnsCollection columnList, string fieldDetailGroup)
        {
            // หา group column ก่อน
            List<string> __groupColumnList = new List<string>();
            foreach (_design._tableColumns __column in columnList)
            {
                if (__column._columnGroupType == _design._columnDisplayTypeEnum.GroupColumn)
                {
                    __groupColumnList.Add(this._cutField(__column.Text));
                }
            }


            //
            DataTable __result = table.Clone();

            for (int __row = 0; __row < table.Rows.Count; __row++)
            {

                StringBuilder __getSelectStr = new StringBuilder();
                foreach (string field in __groupColumnList)
                {
                    if (__getSelectStr.Length > 0)
                    {
                        __getSelectStr.Append(" and ");

                    }

                    string __getValue = table.Rows[__row][field].ToString();
                    __getSelectStr.Append(field + "=\'" + __getValue + "\'");
                }

                DataRow[] __selectRow = table.Select(__getSelectStr.ToString());

                // check row นี้ group ไปหรือยัง
                bool __pass = true;
                DataRow[] __selectGroupedRow = __result.Select(__getSelectStr.ToString());

                if (__selectGroupedRow.Length > 0)
                {
                    __pass = false;
                }

                if (__pass)
                {
                    if (__selectRow.Length >= 1)
                    {
                        string __groupDetailName = "";
                        // group ได้
                        ArrayList __dataGroupRow = new ArrayList();
                        // ทำบรรทัด grop ก่อน
                        //List<int> __columnIndex = new List<int>();

                        for (int __dataColumnIndex = 0; __dataColumnIndex < table.Columns.Count; __dataColumnIndex++)
                        {
                            DataColumn __dataTableColumn = table.Columns[__dataColumnIndex];

                            string __GetColName = __dataTableColumn.ColumnName;
                            bool __found = false;
                            if (__GetColName == fieldDetailGroup)
                            {
                                __groupDetailName = table.Rows[__row][fieldDetailGroup].ToString();
                            }

                            foreach (_design._tableColumns __column in columnList)
                            {
                                if (__GetColName == this._cutField(__column.Text))
                                {
                                    // match column 

                                    if (__column._columnGroupType == _design._columnDisplayTypeEnum.SumColumn)
                                    {
                                        decimal __sumAmount = 0M;
                                        foreach (DataRow __getDataRow in __selectRow)
                                        {
                                            __sumAmount += MyLib._myGlobal._decimalPhase(__getDataRow[__dataColumnIndex].ToString());
                                        }
                                        __dataGroupRow.Add(__sumAmount);
                                        __found = true;

                                    }
                                    else if (__column._columnGroupType == _design._columnDisplayTypeEnum.GroupColumn)
                                    {
                                        __dataGroupRow.Add(table.Rows[__row][__GetColName].ToString());
                                        __found = true;
                                    }
                                    else if (__column._columnGroupType == _design._columnDisplayTypeEnum.GroupDetailColumn)
                                    {
                                        __dataGroupRow.Add(__groupDetailName);
                                        __found = true;
                                    }
                                    //__columnIndex.Add(
                                }
                            }

                            if (__found == false)
                            {
                                __dataGroupRow.Add(null);
                            }
                        }

                        //}
                        __result.Rows.Add(__dataGroupRow.ToArray());

                        // ใส่ตัวย่อยเข้าไป
                        foreach (DataRow __dataRow in __selectRow)
                        {
                            ArrayList __detailRow = new ArrayList();
                            for (int __dataColumnIndex = 0; __dataColumnIndex < table.Columns.Count; __dataColumnIndex++)
                            {
                                DataColumn __dataTableColumn = table.Columns[__dataColumnIndex];

                                string __GetColName = __dataTableColumn.ColumnName;
                                bool __found = false;
                                foreach (_design._tableColumns __column in columnList)
                                {
                                    if (__GetColName == this._cutField(__column.Text))
                                    {
                                        // match column 
                                        //if (__GetColName == fieldDetailGroup)
                                        //{
                                        //    __dataGroupRow.Add(table.Rows[__row][fieldDetailGroup].ToString());
                                        //    __found = true;

                                        //}
                                        //else
                                        if (__column._columnGroupType == _design._columnDisplayTypeEnum.GroupDetailColumn)
                                        {
                                            __detailRow.Add(__dataRow[__dataColumnIndex].ToString());
                                            __found = true;
                                        }
                                        //else if (__column._columnGroupType == _design._columnDisplayTypeEnum.GroupColumn)
                                        //{
                                        //    __dataGroupRow.Add(table.Rows[__row][__GetColName].ToString());
                                        //    __found = true;
                                        //}
                                        //__columnIndex.Add(
                                    }
                                }

                                if (__found == false)
                                {
                                    __detailRow.Add(null);
                                }
                            }
                            __result.Rows.Add(__detailRow.ToArray());
                        }
                    }
                    else
                    {
                        ArrayList __data = new ArrayList();
                        string __groupDetailName = table.Rows[__row][fieldDetailGroup].ToString();
                        for (int __dataColumnIndex = 0; __dataColumnIndex < table.Columns.Count; __dataColumnIndex++)
                        {
                            DataColumn __dataTableColumn = table.Columns[__dataColumnIndex];

                            string __GetColName = __dataTableColumn.ColumnName;
                            bool __found = false;

                            foreach (_design._tableColumns __column in columnList)
                            {
                                if (__GetColName == this._cutField(__column.Text))
                                {
                                    // match column 
                                    if (__column._columnGroupType == _design._columnDisplayTypeEnum.GroupDetailColumn)
                                    {
                                        __data.Add(__groupDetailName);
                                        __found = true;
                                    }
                                    //__columnIndex.Add(
                                }
                            }

                            if (__found == false)
                            {
                                __data.Add(__selectRow[0].ItemArray[__dataColumnIndex]);
                            }
                        }
                        // group ไม่ได้
                        __result.Rows.Add(__data.ToArray());

                    }
                }
            }


            return __result;
        }


        private DataTable _selectDataTable(SMLReport._formReport._queryRule queryRuleProperty)
        {
            switch (queryRuleProperty)
            {
                case _queryRule.A: return this._a_table;
                case _queryRule.B: return this._b_table;
                case _queryRule.C: return this._c_table;
                case _queryRule.D: return this._d_table;
                case _queryRule.E: return this._e_table;
                case _queryRule.F: return this._f_table;
                case _queryRule.G: return this._g_table;
                case _queryRule.H: return this._h_table;
                case _queryRule.I: return this._i_table;
                default: return null;
            }
        }

        private string _replaceSpecialLabel(string __labelText)
        {
            Regex __fieldSpecialRegexFormat = new Regex(@"&field:([a-i])+\.(.*)\[(.*)\|(.*)\]&");

            Regex __fieldSpecialRegex = new Regex(@"&field:([a-i])+\.(.*)&");

            if (__fieldSpecialRegexFormat.IsMatch(__labelText))
            {
                Match __match = __fieldSpecialRegex.Match(__labelText);
                string __query = __match.Groups[1].ToString();
                string __field = __match.Groups[2].ToString();
                string __type = __match.Groups[3].ToString();
                string __format = __match.Groups[4].ToString();

                DataTable __data = this._selectDataTable(__query);
                string __dataStr = "";
                try
                {
                    __dataStr = __data.Rows[0][__field].ToString();
                }
                catch
                {
                }

                __dataStr = _getStringFormat(__dataStr, __type, __format);
                __labelText = __fieldSpecialRegex.Replace(__labelText, __dataStr);

            }
            else if (__fieldSpecialRegex.IsMatch(__labelText))
            {
                Match __match = __fieldSpecialRegex.Match(__labelText);
                string __query = __match.Groups[1].ToString();
                string __field = __match.Groups[2].ToString();

                DataTable __data = this._selectDataTable(__query);
                string __dataStr = "";
                try
                {
                    __dataStr = __data.Rows[0][__field].ToString();
                }
                catch
                {
                }

                __labelText = __fieldSpecialRegex.Replace(__labelText, __dataStr);

            }

            return __labelText;
        }

        private string _getReplaceText(string __replaceText, string __data)
        {
            if (__replaceText == null)
                return __data;

            if (__replaceText.ToLower().IndexOf(_signFieldReplace) != -1)
            {
                __replaceText = __replaceText.Replace(_signFieldReplace, _lastCurrencySymbol);
            }

            if (__replaceText.IndexOf("@") != -1)
            {
                __data = __replaceText.Replace("@", __data);
            }

            return __data;
        }

        private string _getReplaceText(string __replaceText, string __data, int __row)
        {
            if (__replaceText == null)
                return __data;

            if (__replaceText.ToLower().IndexOf(_signFieldReplace) != -1)
            {
                __replaceText = __replaceText.Replace(_signFieldReplace, _lastCurrencySymbol);
            }

            if (__replaceText.IndexOf("@") != -1)
            {
                __data = __replaceText.Replace("@", __data);
            }

            if (__replaceText.Equals("&rownumber&"))
            {
                __data = __row.ToString();
            }

            return __data;
        }

        private string _getReplaceFooterText(string _replaceText, string __data)
        {
            if (_replaceText.IndexOf("@") != -1)
            {
                _replaceText = _replaceText.Replace("@", __data);
            }

            return _replaceText;
        }

        private string _cutField(string source)
        {
            // @"\[(\w*)\]+,\s+(\w*)" //// regex field 

            if (source == null)
                return "";
            string[] __split = source.Split(',');
            if (source.Trim().Length == 0 || __split.Length == 0)
            {
                return source;
            }
            string __result = __split[0].Replace("[", "").Replace("]", "");
            return __result;
        }

        private string _getField(string source)
        {
            //string _fieldResRegex = @"\[(\w*)\]+,\s+(\w*)";
            Regex _fieldRegex = new Regex(@"\[(\w*)\]");
            string __result = "";

            if (_fieldRegex.IsMatch(source))
            {
                Match __match = _fieldRegex.Match(source);
                __result = __match.Groups[1].ToString();
            }

            return __result;
        }

        private string _cutGlobalVar(string __str)
        {
            return this._cutGlobalVar(__str, 0);
        }

        private string _cutGlobalVar(string __str, int __row)
        {
            // date value
            __str = _replaceSpecialLabel(__str);

            Regex __dateRegex = new Regex(@"&date\[(.*)\]&");
            if (__dateRegex.IsMatch(__str))
            {
                Match __match = __dateRegex.Match(__str);
                if (__match.Success)
                {
                    string __dateFormatStr = __match.Groups[1].Value;
                    //DateTime __date = new DateTime((MyLib._myGlobal._year_type == 1) ? DateTime.Now.Year + 543 : DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    DateTime __date = DateTime.Now;
                    __str = __dateRegex.Replace(__str, __date.ToString(__dateFormatStr, MyLib._myGlobal._cultureInfo()));
                }
            }

            // total page
            Regex __pageTotalOfRegex = new Regex(@"&totalpageof\[([0-9]+)\]&");
            if (__pageTotalOfRegex.IsMatch(__str))
            {
                Match __match = __pageTotalOfRegex.Match(__str);

                if (__match.Success)
                {
                    int __page = (int)MyLib._myGlobal._decimalPhase(__match.Groups[1].Value);
                    int __pagetotalof = (int)_pageTotalAssign[__page - 1];

                    __str = __pageTotalOfRegex.Replace(__str, __pagetotalof.ToString());
                }
            }

            Regex __pageCurrentRegex = new Regex(@"&pageCurrent&");
            if (__pageCurrentRegex.IsMatch(__str))
            {
                Match __match = __pageCurrentRegex.Match(__str);

                if (__match.Success)
                {
                    int __page = _subPageIndex + 1;
                    __str = __pageCurrentRegex.Replace(__str, __page.ToString());
                }
            }

            Regex __totalPageCurrent = new Regex(@"&totalpageCurrent&");
            if (__totalPageCurrent.IsMatch(__str))
            {
                Match __match = __totalPageCurrent.Match(__str);
                if (__match.Success)
                {
                    //int __pagetotalof = (int)_pageTotalAssign[_pageNumber - 1];
                    int __pagetotalof = (int)_pageTotalAssign[_pageIndex];
                    __str = __totalPageCurrent.Replace(__str, __pagetotalof.ToString());
                }
            }

            // page value
            string __newString = __str.Replace("&totalpage&", _pageTotal.ToString()).Replace("&page&", _pageNumber.ToString()).Replace("&rownumber&", __row.ToString()).Replace("&printby&", MyLib._myGlobal._userCode).Replace("&printbyname&", MyLib._myGlobal._userName);
            return __newString;
        }

        private string _getStringFormat(string __strValue, _design._FieldType __fieldType, string __formatValue)
        {
            return _getStringFormat(__strValue, __fieldType, __formatValue, true);
        }

        private string _getStringFormat(string __strValue, _design._FieldType __fieldType, string __formatValue, bool __showZeroValue)
        {
            string __newStrValue = __strValue;

            try
            {
                switch (__fieldType)
                {
                    case _design._FieldType.Auto:
                        {
                            decimal __value = MyLib._myGlobal._decimalPhase(__strValue.ToString());

                            if (__value != 0)
                            {
                                // is number
                                try
                                {
                                    if (__formatValue.Trim().ToUpper().IndexOf("TEXT") != -1)
                                    {
                                        _moneyToText __convert = new _moneyToText();
                                        string[] __format = __formatValue.Trim().Split(':');

                                        if (__formatValue.Trim().ToUpper().IndexOf("ENTEXT") != -1)
                                        {
                                            __newStrValue = __convert._convertNumToDolla(__value.ToString());
                                        }
                                        else if (__formatValue.Trim().ToUpper().IndexOf("LAOTEXT") != -1)
                                        {
                                            __newStrValue = __convert._convertNumToLaoKip(__value);
                                        }
                                        else
                                        {
                                            __newStrValue = __convert._toText(__value);
                                        }

                                    }
                                    else
                                    {
                                        __value = MyLib._myGlobal._decimalPhase(__strValue.ToString());

                                        __newStrValue = __value.ToString(__formatValue);
                                        if ((__value == 0) && (__showZeroValue == false))
                                            __newStrValue = "";
                                    }
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                return __newStrValue;
                            }
                        }
                        break;
                    case _design._FieldType.Number:
                        if (__formatValue.Trim().Length > 0)
                        {
                            try
                            {
                                if (__formatValue.Trim().ToUpper().IndexOf("TEXT") != -1)
                                {
                                    decimal __value = MyLib._myGlobal._decimalPhase(__strValue.ToString());
                                    _moneyToText __convert = new _moneyToText();
                                    string[] __format = __formatValue.Trim().Split(':');

                                    if (__formatValue.Trim().ToUpper().IndexOf("ENTEXT") != -1)
                                    {
                                        string __firstSign = (__format.Length >= 3) ? __format[2] : "";
                                        string __secondSign = (__format.Length >= 4) ? __format[3] : "";
                                        __newStrValue = __convert._convertNumToDolla(__value.ToString(), __firstSign, __secondSign);
                                    }
                                    else if (__formatValue.Trim().ToUpper().IndexOf("LAOTEXT") != -1)
                                    {
                                        __newStrValue = __convert._convertNumToLaoKip(__value);
                                    }
                                    else
                                    {
                                        __newStrValue = __convert._toText(__value);
                                    }

                                }
                                else
                                {
                                    decimal __value = MyLib._myGlobal._decimalPhase(__strValue.ToString());

                                    __newStrValue = __value.ToString(__formatValue);
                                    if ((__value == 0) && (__showZeroValue == false))
                                        __newStrValue = "";
                                }
                            }
                            catch
                            {
                            }
                        }
                        break;

                    case _design._FieldType.DateTime:
                        if (__formatValue.Trim().Length > 0)
                        {
                            try
                            {
                                DateTime __date = DateTime.Parse(__strValue.ToString(), new CultureInfo("en-US"));
                                if (__formatValue.Trim().ToUpper().Equals("FULL"))
                                {
                                    int __year = (MyLib._myGlobal._year_type == 1) ? __date.Year + 543 : __date.Year;
                                    __newStrValue = String.Format("{0} {1} {2}", __date.Day.ToString(), MyLib._myGlobal._monthName(__date, true), __year.ToString());
                                }
                                else
                                {
                                    __newStrValue = __date.ToString(__formatValue, MyLib._myGlobal._cultureInfo());
                                }
                            }
                            catch
                            {
                            }
                        }

                        break;
                }
            }
            catch
            {
            }

            return __newStrValue;
        }

        private string _getStringFormat(string __strValue, string __fieldType, string __formatValue)
        {
            _design._FieldType __type = new _design._FieldType();

            switch (__fieldType.ToLower())
            {
                case "number":
                    __type = _design._FieldType.Number;
                    break;
                case "datetime":
                    __type = _design._FieldType.DateTime;
                    break;
            }

            return _getStringFormat(__strValue, __type, __formatValue, true);
        }

        private string _getStringFormat(string __strValue, string __fieldType, string __formatValue, bool __showZeroValue)
        {
            _design._FieldType __type = new _design._FieldType();

            switch (__fieldType.ToLower())
            {
                case "number":
                    __type = _design._FieldType.Number;
                    break;
                case "datetime":
                    __type = _design._FieldType.DateTime;
                    break;
            }

            return _getStringFormat(__strValue, __type, __formatValue, __showZeroValue);
        }

        /// <summary>
        /// Raplace Field Pathern  use After Format value
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="_data"></param>
        /// <returns></returns>
        private string _replaceField(string Source, string _data)
        {
            string __result = "";
            Regex _fieldResRegex = new Regex(@"\[(\w*)\]+,\s+(\w*)");
            Regex _fieldRegex = new Regex(@"\[(\w*)\]");
            if (_fieldResRegex.IsMatch(Source))
            {
                __result = _fieldResRegex.Replace(Source, _data);
                return __result;
            }
            else if (_fieldRegex.IsMatch(Source))
            {
                __result = _fieldRegex.Replace(Source, _data);
                return __result;
            }

            return __result;
        }

        private string _mathParser(string __field, int __row)
        {
            __field = _getFieldValue(__field, __row);

            MyLib._mathParser __math = new MyLib._mathParser();
            Regex __substate = new Regex(@"\(([0-9a-z\.\+\/\*\-\[\]_]+)\)");
            if (__substate.IsMatch(__field))
            {
                MatchCollection __matchCollection = __substate.Matches(__field);
                foreach (Match __match in __matchCollection)
                {
                    string __subCalc = __match.Groups[1].ToString();
                    string __result = _mathParser(__subCalc, __row);
                    __field = __field.Replace(__match.Groups[0].ToString(), __result);
                }

                __field = __math.Calculate(__field).ToString();
            }
            __field = __math.Calculate(__field).ToString();

            return __field;
        }

        private string _getFieldValue(string __field, int __row)
        {
            try
            {
                // get value from specialField ( Format : [a.field]);
                Regex _fieldRegex = new Regex(@"\[([\w\.]+)\]");
                if (_fieldRegex.IsMatch(__field))
                {
                    MatchCollection __match = _fieldRegex.Matches(__field);
                    foreach (Match __case in __match)
                    {
                        string __fieldData = __case.Groups[1].ToString();
                        string[] __split = __fieldData.Split('.');
                        DataTable __data = this._selectDataTable(__split[0]);
                        string __dataStr = "";
                        try
                        {
                            __dataStr = __data.Rows[__row][__split[1]].ToString();
                        }
                        catch
                        {
                        }

                        __field = __field.Replace(__case.Groups[0].ToString(), __dataStr);
                    }
                }

                // get value from Variable format : &val(asField)&
                Regex __valRegex = new Regex(@"\&val\(([\w-]+)\)\&");
                if (__valRegex.IsMatch(__field))
                {
                    MatchCollection __match = __valRegex.Matches(__field);
                    foreach (Match __case in __match)
                    {
                        string __newVal = _getVariable(__case.Groups[1].ToString());
                        __field = __field.Replace(__case.Groups[0].ToString(), __newVal);
                    }
                }

                //string __columnName = this._cutField(__drawTextField.Field);
            }
            catch
            {
            }

            return __field;
        }

        private string _getTextFieldValue(SMLReport._design._drawTextField __drawTextField)
        {
            DataTable __data = this._selectDataTable(__drawTextField.query);
            string __columnName = this._cutField(__drawTextField.Field);
            string __dataStr = "";

            try
            {
                if (__drawTextField._operation != _design._fieldOperation.None && __drawTextField.FieldType == _design._FieldType.Number && __columnName != null)
                {
                    __dataStr = _getStringFormat(_getDataOperation(__data, __columnName, __drawTextField._operation).ToString(), __drawTextField.FieldType, __drawTextField.FieldFormat, __drawTextField._showIsNumberZero);
                }
                else
                {
                    if (__drawTextField._specialField != null && !__drawTextField._specialField.Equals(""))
                    {
                        __dataStr = _mathParser(__drawTextField._specialField, 0);

                    }
                    else
                    {
                        __dataStr = _getStringFormat(__data.Rows[0][__columnName].ToString(), __drawTextField.FieldType, __drawTextField.FieldFormat, __drawTextField._showIsNumberZero);
                    }
                }
            }
            catch
            {
            }
            return __dataStr;

        }

        private string _getVariable(string __valStr)
        {
            string __val = "";

            for (int __i = 0; __i < this._form.PageCount; __i++)
            {
                foreach (Control __control1 in ((_drawPaper)this._form._paperList[__i]).Controls)
                {
                    if (__control1.GetType() == typeof(SMLReport._design._drawPanel))
                    {
                        SMLReport._design._drawPanel __getControl = (SMLReport._design._drawPanel)__control1;

                        for (int __loop = 0; __loop < __getControl._graphicsList._count; __loop++)
                        {
                            if (__getControl._graphicsList[__loop].GetType() == typeof(SMLReport._design._drawTextField))
                            {
                                SMLReport._design._drawTextField __textField = (SMLReport._design._drawTextField)__getControl._graphicsList[__loop];

                                if (__textField._asField.Equals(__valStr))
                                {
                                    string __dataStr = _getTextFieldValue(__textField);
                                    __val = __dataStr;

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return __val;
        }

        //private string _getDataStr(string __field, string 

        private string _getDataOperation(DataTable __dt, string __field, SMLReport._design._fieldOperation __operation)
        {
            return _getDataOperation(__dt, __field, __operation, -1, -1);
        }

        private string _getDataOperation(DataTable __dt, string __field, SMLReport._design._fieldOperation __operation, int __rowStart, int __rowEnd)
        {
            float __result = 0f;

            switch (__operation)
            {
                case _design._fieldOperation.Sum:
                    try
                    {
                        for (int __i = 0; __i < __dt.Rows.Count; __i++)
                        {
                            if ((__i >= __rowStart && __i <= __rowEnd) || (__rowStart == -1 && __rowEnd == -1))
                            {
                                __result += (float)MyLib._myGlobal._decimalPhase(__dt.Rows[__i][__field].ToString());
                            }
                        }
                    }
                    catch
                    {
                    }
                    break;

                case _design._fieldOperation.Count:
                    __result = __dt.Rows.Count;
                    break;
                case _design._fieldOperation.CurrentRow:
                    string __resultStr = __dt.Rows[(this._printCurrentRowIndex != -1) ? this._printCurrentRowIndex : 0][__field].ToString();
                    return __resultStr;
                    break;
            }

            return __result.ToString();
        }

        private float _getHeaderHeight(SMLReport._design._drawTable __drawTable)
        {
            float __headerTableHeight = 0f;
            Control __tmpControl = new Control();
            Graphics g = __tmpControl.CreateGraphics();

            if (__drawTable.ShowHeaderColumns == false)
                return __headerTableHeight;

            for (int __col = 0; __col < __drawTable.Columns.Count; __col++)
            {
                SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns)__drawTable.Columns[__col];

                string __headerText = "Test";
                if (__column.HeaderText != null && __column.HeaderText != "")
                {
                    __headerText = __column.HeaderText;
                }

                ArrayList __getString = SMLReport._design._drawLabel._cutString(SMLReport._design._drawLabel._replaceLineBreak(__headerText, true), __drawTable._headerFont, __drawTable.getColumnsWidth(__col) * ((float)__drawTable._actualSize.Width / 100), 0, 0, new Padding(0));

                SizeF __tmpSize = _getTextSize(__getString, __drawTable._headerFont, g);

                if (__tmpSize.Height > __headerTableHeight)
                {
                    __headerTableHeight = __tmpSize.Height;
                }
            }

            g.Dispose();
            __tmpControl.Dispose();

            return __headerTableHeight;
        }

        private float _getFooterHeight(SMLReport._design._drawTable __drawTable)
        {
            float __footerHeight = 0f;

            if (__drawTable.ShowFooter == false || __drawTable.Footers.Count == 0)
                return 0;

            if (__drawTable._footerHeight > 0)
                return __drawTable._footerHeight;

            string __sampleText = "SampleText";
            Control __tmpControl = new Control();
            Graphics g = __tmpControl.CreateGraphics();
            SizeF __tmpSize = g.MeasureString(__sampleText, __drawTable._font);

            __footerHeight = __tmpSize.Height;

            g.Dispose();
            __tmpControl.Dispose();

            return __footerHeight;
        }

        public static SizeF _getTextSize(string __strMeasure, Font __Font, Graphics g)
        {
            SizeF _textSize = new SizeF();
            SizeF _defaultTextSize = g.MeasureString(_textForMeasure, __Font, 0, StringFormat.GenericTypographic);

            _textSize = g.MeasureString(__strMeasure, __Font, 0, StringFormat.GenericTypographic);
            _textSize.Height = _defaultTextSize.Height;

            return _textSize;
        }

        public static SizeF _getTextSize(ArrayList __strMeasureList, Font __Font, Graphics g)
        {
            SizeF _textSize = new SizeF();
            SizeF _defaultTextSize = g.MeasureString(_textForMeasure, __Font, 0, StringFormat.GenericTypographic);
            foreach (string __strMeasure in __strMeasureList)
            {
                SizeF __strSize = g.MeasureString(__strMeasure, __Font, 0, StringFormat.GenericTypographic);
                _textSize.Height += _defaultTextSize.Height;

                if (_textSize.Width < __strSize.Width)
                {
                    _textSize.Width = __strSize.Width;
                }
            }

            return _textSize;
        }

        private SizeF _getTextSize(ArrayList __strMeasureList, Font __Font, float __textRowHeight, Graphics g)
        {
            SizeF _textSize = new SizeF();
            SizeF _defaultTextSize = g.MeasureString(_textForMeasure, __Font, 0, StringFormat.GenericTypographic);
            foreach (string __strMeasure in __strMeasureList)
            {
                SizeF __strSize = g.MeasureString(__strMeasure, __Font, 0, StringFormat.GenericTypographic);
                //_textSize.Height += _defaultTextSize.Height;

                if (_textSize.Width < __strSize.Width)
                {
                    _textSize.Width = __strSize.Width;
                }
            }

            _textSize.Height = _defaultTextSize.Height + (__textRowHeight * (__strMeasureList.Count - 1));

            return _textSize;
        }


        private PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, _design._textAlign __strAlignment)
        {
            return _getPointTextAlingDraw(__width, __height, __strWidth, __strHeight, __strAlignment, new Padding(0));
        }

        private PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, _design._textAlign __strAlignment, Padding __padding)
        {
            PointF __newDrawPoint = new PointF(0, 0);

            switch (__strAlignment)
            {
                case _design._textAlign.Left:
                    __newDrawPoint.X = 0 + __padding.Left;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case _design._textAlign.Center:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case _design._textAlign.Right:
                    __newDrawPoint.X = (__width - __strWidth) - __padding.Right;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;
            }

            return __newDrawPoint;
        }

        private PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, ContentAlignment __strAlignment)
        {
            return _getPointTextAlingDraw(__width, __height, __strWidth, __strHeight, __strAlignment, new Padding(0));
        }

        public static PointF _getPointTextAlingDraw(float __width, float __height, float __strWidth, float __strHeight, ContentAlignment __strAlignment, Padding __padding)
        {
            PointF __newDrawPoint = new PointF(0, 0);

            switch (__strAlignment)
            {
                case ContentAlignment.TopLeft:
                    __newDrawPoint.X = 0 + __padding.Left;
                    break;

                case ContentAlignment.TopCenter:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    break;

                case ContentAlignment.TopRight:
                    __newDrawPoint.X = (__width - __strWidth) - __padding.Right;
                    break;

                case ContentAlignment.MiddleLeft:
                    __newDrawPoint.X = 0 + __padding.Left;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case ContentAlignment.MiddleCenter:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case ContentAlignment.MiddleRight:
                    __newDrawPoint.X = (__width - __strWidth) - __padding.Right;
                    __newDrawPoint.Y = (__height - __strHeight) / 2;
                    break;

                case ContentAlignment.BottomLeft:
                    __newDrawPoint.X = 0 + __padding.Left;
                    __newDrawPoint.Y = (__height - __strHeight);
                    break;

                case ContentAlignment.BottomCenter:
                    __newDrawPoint.X = (__width - __strWidth) / 2;
                    __newDrawPoint.Y = (__height - __strHeight);
                    break;

                case ContentAlignment.BottomRight:
                    __newDrawPoint.X = (__width - __strWidth) - __padding.Right;
                    __newDrawPoint.Y = (__height - __strHeight);
                    break;
            }

            return __newDrawPoint;
        }

        private bool _checkPrintPage(string __printOption)
        {
            string[] __pageOption = __printOption.Split(',');

            for (int __i = 0; __i < __pageOption.Length; __i++)
            {
                if ((bool)_checkPage(__pageOption[__i].Trim()))
                {
                    return true;
                }
            }

            return false;

        }

        private bool _checkPage(string __printOption)
        {

            if (__printOption.ToLower().Equals("All".ToLower()))
            {
                return true;
            }

            if (__printOption.ToLower().Equals("FirstTotal".ToLower()))
            {
                if (_pageNumber == 1)
                {
                    return true;
                }
            }

            if (__printOption.ToLower().Equals("First".ToLower()))
            {
                if (_subPageIndex == 0)
                {
                    return true;
                }
            }

            if (__printOption.ToLower().Equals("Last".ToLower()))
            {
                int pageNumber = this._pageAssign[_pageNumber - 1];
                int __pagetotalof = (int)_pageTotalAssign[pageNumber];

                if ((_subPageIndex + 1) == __pagetotalof)
                {
                    return true;
                }
            }

            if (__printOption.ToLower().Equals("LastTotal".ToLower()))
            {
                if (_pageNumber == _pageTotal)
                {
                    return true;
                }
            }

            if (__printOption.ToLower().Equals("OddTotal".ToLower()))
            {
                if (_pageNumber % 2 == 1)
                {
                    return true;
                }
            }

            if (__printOption.ToLower().Equals("Odd".ToLower()))
            {
                if ((_subPageIndex + 1) % 2 == 1)
                {
                    return true;
                }
            }

            if (__printOption.ToLower().Equals("EvenTotal".ToLower()))
            {
                if (_pageNumber % 2 == 0)
                {
                    return true;
                }

            }

            if (__printOption.ToLower().Equals("Even".ToLower()))
            {
                if ((_subPageIndex + 1) % 2 == 0)
                {
                    return true;
                }

            }

            int _printPage = (int)MyLib._myGlobal._decimalPhase(__printOption);
            if (_printPage == _pageNumber)
            {
                return true;
            }

            return false;
        }

        private StringFormat _getStringFormat(ContentAlignment __alignMent)
        {
            StringFormat __sf = StringFormat.GenericTypographic;

            switch (__alignMent)
            {
                case ContentAlignment.TopLeft:
                    __sf.Alignment = StringAlignment.Near;
                    __sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopCenter:
                    __sf.Alignment = StringAlignment.Center;
                    __sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopRight:
                    __sf.Alignment = StringAlignment.Far;
                    __sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                    __sf.Alignment = StringAlignment.Near;
                    __sf.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleCenter:
                    __sf.Alignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleRight:
                    __sf.Alignment = StringAlignment.Far;
                    __sf.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomLeft:
                    __sf.Alignment = StringAlignment.Near;
                    __sf.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomCenter:
                    __sf.Alignment = StringAlignment.Center;
                    __sf.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomRight:
                    __sf.Alignment = StringAlignment.Far;
                    __sf.LineAlignment = StringAlignment.Far;
                    break;
            }

            return __sf;
        }

        private RectangleF _getRectangleFPadding(RectangleF __rect, Padding __padding)
        {
            __rect.X += __padding.Left;
            __rect.Y += __padding.Top;

            __rect.Width -= (__padding.Left + __padding.Right);
            __rect.Height -= (__padding.Top + __padding.Bottom);

            return __rect;
        }

        public ArrayList _getStrLine(string __data)
        {
            ArrayList __strList = new ArrayList();
            __data = __data.Replace("\\n", "\n");
            string[] __tmpData = __data.Split('\n');

            foreach (string __str in __tmpData)
            {
                __strList.Add(__str);
            }

            return __strList;
        }

        public ArrayList _cutString(Graphics g, string text, Font font, float width, float __charSpace, float __charWidth, Padding __Padding)
        {
            ArrayList __result = new ArrayList();

            ArrayList __strList = _getStrLine(text);

            width -= (__Padding.Left + __Padding.Right);

            foreach (string __str in __strList)
            {
                try
                {
                    SizeF __stringSize = g.MeasureString(__str, font, 0, StringFormat.GenericTypographic);
                    char[] __charCount = __str.ToCharArray();

                    if (__charWidth != -1 && __charWidth != 0)
                    {
                        __stringSize.Width = (__charCount.Length * __charWidth);
                    }

                    __stringSize.Width += (__charSpace * (__charCount.Length - 1));

                    if (__stringSize.Width > width)
                    {
                        int __tailFirst = 0;
                        int __tail = 0;
                        int __lastCutPoint = -1;
                        int __lastThaiPoint = -1;
                        char __lastChar = ' ';

                        while (__tail < __str.Length)
                        {
                            char __getChar = __str[__tail];
                            if (__getChar <= ' ' || (__getChar >= ';' && __getChar <= '@') ||
                                    (__getChar >= 'ก' && __getChar <= 'ฮ' && __tail - __lastThaiPoint > 2 && __lastChar != 'า' && !(__lastChar >= 'เ' && __lastChar <= 'โ')) ||
                                    (__getChar >= 'เ' && __getChar <= 'โ') || (__getChar >= '0' && __getChar <= 'z' && __lastChar >= 'ก' && __lastChar <= 'ฮ'))
                            {
                                __lastCutPoint = __tail;
                                __lastThaiPoint = __lastCutPoint;
                            }
                            __lastChar = __str[__tail];
                            string __lastString = __str.Substring(__tailFirst, __tail - __tailFirst);
                            char[] __lastCharArray = __lastString.ToCharArray();

                            __stringSize = g.MeasureString(__lastString, font, 0, StringFormat.GenericTypographic);

                            if (__charWidth != -1 && __charWidth != 0)
                            {
                                __stringSize.Width = (__lastCharArray.Length * __charWidth);
                            }

                            if (__lastString.Length > 0)
                            {
                                __stringSize.Width += (__charSpace * (__lastCharArray.Length - 1));
                            }

                            if (__stringSize.Width > width)
                            {
                                if (__lastCutPoint == -1)
                                {
                                    __lastCutPoint = __tail;
                                    __lastThaiPoint = __lastCutPoint;
                                }
                                __result.Add(__str.Substring(__tailFirst, (__lastCutPoint - __tailFirst)));
                                __tailFirst = __lastCutPoint;
                                __lastCutPoint = -1;
                                __lastThaiPoint = -1;
                            }
                            __tail++;
                        }// while
                        if (__tailFirst != __lastCutPoint)
                        {
                            __result.Add(__str.Substring(__tailFirst, (__str.Length - __tailFirst)));
                        }
                    }
                    else
                    {
                        __result.Add(__str);
                    }

                }
                catch
                {
                    __result.Add(__str);
                }
            }
            return __result;
        }

        public string _getSerialNumber(_design._drawTable __drawTable, _design._serialNumberDisplayEnum _showAs, int __column, int __row)
        {
            DataTable __serialData = this._selectDataTable(__drawTable.SerialQuery);
            DataTable __tableData = this._selectDataTable(__drawTable.DataQuery);

            string __result = "";
            try
            {
                string __filterSerial = __tableData.Rows[__row][__drawTable._dataPrimaryLink].ToString();
                string __exp = string.Format("{0} = '{1}' and doc_line_number = '{2}' ", __drawTable._dataSecondLink, __filterSerial, __row);
                DataRow[] __rows = __serialData.Select(__exp);

                if (__rows.Length > 0)
                {
                    //string[] __serialList = new string[__rows.Length];
                    // format display
                    StringBuilder __str = new StringBuilder();
                    for (int __i = 0; __i < __rows.Length; __i++)
                    {
                        __str.Append(string.Format("{1}{0}{2}", ((__i != __rows.Length - 1) ? ", " : string.Empty), __rows[__i][__drawTable._serialNumberField].ToString(), ((_showAs == _design._serialNumberDisplayEnum.Columns && ((__i + 1) % __column == 0) && (__i + 1) != __rows.Length) ? "||" : string.Empty)));
                    }

                    __result = __str.ToString();
                }
            }
            catch
            {
            }

            return __result;
        }

        #endregion

        #region Print Public Method

        public void _loadPrintPage(string formCode, int __screenCode)
        {
            string __currentConfigFileName = string.Format("_cache-{0}-{1}-{2}-{3}.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, MyLib._myGlobal._encapeStringForFilePath(formCode)); // "_cache" + formCode + ".xml";
            string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
            string __lastTimeUpdate = "";
            // check cache 
            bool _isCache = false;
            try
            {
                // check xml 
                StringBuilder __queryCheckCode = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __queryCheckCode.Append(MyLib._myUtil._convertTextToXmlForQuery("SELECT formcode, timeupdate FROM formdesign WHERE formcode=\'" + formCode + "\'"));
                __queryCheckCode.Append("</node>");

                MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                ArrayList __result = __ws._queryListGetData(MyLib._myGlobal._databaseName, __queryCheckCode.ToString());

                DataTable __da = ((DataSet)__result[0]).Tables[0];
                __lastTimeUpdate = __da.Rows[0]["timeupdate"].ToString();

                SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();

                TextReader readFile = new StreamReader(__path);
                XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                __cacheXML = (SMLReport._formReport.SMLFormDesignXml)__xsLoad.Deserialize(readFile);
                readFile.Close();

                if (__lastTimeUpdate == __cacheXML._lastUpdate)
                {
                    _isCache = true;
                }
            }
            catch (Exception ex)
            {
                _isCache = false;
            }

            // load cache

            try
            {
                for (int __loop = 0; __loop < 500; __loop++)
                {
                    this._pageAssign.Add(0);
                }

                if (_isCache)
                {
                    SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLFormDesignXml));
                    FileStream __readFileStream = new FileStream(__path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    this._form._loadFromStream(__readFileStream, null, _openFormMethod.OpenFromServer);
                    __readFileStream.Close();

                }
                else
                {
                    string __query = "SELECT formcode,guid_code,formname,timeupdate,formdesigntext FROM formdesign WHERE upper(formcode)=\'" + formCode.ToUpper() + "\'";

                    MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                    MyLib.SMLJAVAWS.formDesignType __formDesign = __ws._loadForm(MyLib._myGlobal._databaseName, __query);

                    try
                    {
                        // ลองดึงดู ถ้าข้อมูล Compress แล้ว ก็ผ่าน ถ้าไม่ ก็ไปดึงแบบเดิม
                        MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__formDesign._formdesign));
                        this._form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                        __ms.Close();
                    }
                    catch
                    {
                        // กรณีที่ดึงของเก่าที่ไม่ได้ Compress
                        try
                        {
                            MemoryStream __ms = new MemoryStream((byte[])__formDesign._formdesign);
                            this._form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                            __ms.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }


                    /*MemoryStream __ms = new MemoryStream((byte[])__formDesign._formdesign);
                    this._form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                    __ms.Close();
                    */

                    // write cache
                    SMLFormDesignXml __formCacheXML = this._form._writeXMLSource(_writeXMLSourceOption.DrawObjectOnly);
                    __formCacheXML._lastUpdate = __lastTimeUpdate;

                    XmlSerializer __colXs = new XmlSerializer(typeof(SMLFormDesignXml));
                    TextWriter __memoryStream = new StreamWriter(__path);
                    __colXs.Serialize(__memoryStream, __formCacheXML);
                    __memoryStream.Close();

                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        /// <summary>
        /// สำหรับ Map กับ Dataset ที่ทำ Query มาเอง
        /// </summary>
        /// <param name="__ds"></param>
        /// <param name="isPrint"></param>
        public void _query(DataSet __ds, Boolean isPrint)
        {
            // map datatable

            this._a_table = (__ds.Tables.Count > 0 && __ds.Tables[0] != null) ? __ds.Tables[0] : null;
            this._b_table = (__ds.Tables.Count > 1 && __ds.Tables[1] != null) ? __ds.Tables[1] : null;
            this._c_table = (__ds.Tables.Count > 2 && __ds.Tables[2] != null) ? __ds.Tables[2] : null;
            this._d_table = (__ds.Tables.Count > 3 && __ds.Tables[3] != null) ? __ds.Tables[3] : null;
            this._e_table = (__ds.Tables.Count > 4 && __ds.Tables[4] != null) ? __ds.Tables[4] : null;
            this._f_table = (__ds.Tables.Count > 5 && __ds.Tables[5] != null) ? __ds.Tables[5] : null;
            this._g_table = (__ds.Tables.Count > 6 && __ds.Tables[6] != null) ? __ds.Tables[6] : null;
            this._h_table = (__ds.Tables.Count > 7 && __ds.Tables[7] != null) ? __ds.Tables[7] : null;
            this._i_table = (__ds.Tables.Count > 8 && __ds.Tables[8] != null) ? __ds.Tables[8] : null;

            this._build(isPrint);
        }

        private void _setDataTableRangeIndex(DataSet __ds)
        {
            this._a_table = (__ds.Tables.Count > 0 && __ds.Tables[0] != null) ? __ds.Tables[0] : null;
            this._b_table = (__ds.Tables.Count > 1 && __ds.Tables[1] != null) ? __ds.Tables[1] : null;
            this._c_table = (__ds.Tables.Count > 2 && __ds.Tables[2] != null) ? __ds.Tables[2] : null;
            this._d_table = (__ds.Tables.Count > 3 && __ds.Tables[3] != null) ? __ds.Tables[3] : null;
            this._e_table = (__ds.Tables.Count > 4 && __ds.Tables[4] != null) ? __ds.Tables[4] : null;
            this._f_table = (__ds.Tables.Count > 5 && __ds.Tables[5] != null) ? __ds.Tables[5] : null;
            this._g_table = (__ds.Tables.Count > 6 && __ds.Tables[6] != null) ? __ds.Tables[6] : null;
            this._h_table = (__ds.Tables.Count > 7 && __ds.Tables[7] != null) ? __ds.Tables[7] : null;
            this._i_table = (__ds.Tables.Count > 8 && __ds.Tables[8] != null) ? __ds.Tables[8] : null;

        }

        public void _query()
        {
            _checkAssignCondition();

            this._a_table = this._processQuery(this._form.__queryEdit._a._queryTextBox.Text, this._conditon);
            this._b_table = this._processQuery(this._form.__queryEdit._b._queryTextBox.Text, this._conditon);
            this._c_table = this._processQuery(this._form.__queryEdit._c._queryTextBox.Text, this._conditon);
            this._d_table = this._processQuery(this._form.__queryEdit._d._queryTextBox.Text, this._conditon);
            this._e_table = this._processQuery(this._form.__queryEdit._e._queryTextBox.Text, this._conditon);
            this._f_table = this._processQuery(this._form.__queryEdit._f._queryTextBox.Text, this._conditon);
            this._g_table = this._processQuery(this._form.__queryEdit._g._queryTextBox.Text, this._conditon);
            this._h_table = this._processQuery(this._form.__queryEdit._h._queryTextBox.Text, this._conditon);
            this._i_table = this._processQuery(this._form.__queryEdit._i._queryTextBox.Text, this._conditon);

            this._build(true);
        }

        public class _queryRangeClass
        {
            public _queryRangeClass(string query, bool isCondition)
            {
                this._query = query;
                this._isConditionQuery = isCondition;
            }

            public string _query = "";
            public bool _isConditionQuery = false;
        }

        /// <summary>
        /// สำหรับเก็บ Query จากการพิมพ์เป็นช่วง
        /// </summary>
        ArrayList _dataRangeSet = new ArrayList();
        /// <summary>
        /// จำนวนเอกสาร
        /// </summary>
        int _docGroupLength = 0;
        /// <summary>
        /// เก็บเอกสารที่จะส่งเข้ามาเป็นชุด
        /// </summary>
        ArrayList _packList;
        /// <summary>
        /// เก็บชุดข้อมูลของ PageTotalAssign ค่อยไป get ตอน begin page เอา
        /// </summary>
        ArrayList _listPageTotalAssign = new ArrayList();
        /// <summary>
        /// เก็บ _pageAssign
        /// </summary>
        ArrayList _listPageAssign = new ArrayList();
        /// <summary>
        /// เก็บ doc map index in myPrintDocument ว่าใช้ index dataset,pageTotalAssign,pageAssign ที่เท่าไหร่
        /// </summary>
        ArrayList _docIndexAssign = new ArrayList();

        /// <summary>
        /// for print range
        /// </summary>
        /// <param name="arrConditionClass">pack of condition</param>
        public void _query(ArrayList arrConditionClass)
        {
            // step
            // 1. packquery เป็นชุดแล้วส่งไป process ครั้งเดียว
            // 2. วน gen เอกสารเข้าใส่ myPrintDocument
            // 3. ตอน begin print ให้หา datatable ของ ชุดตัวเอง
            this._findDataForPage = true;
            _docGroupLength = arrConditionClass.Count;
            _packList = arrConditionClass;
            // เก็บ query เข้า list เพือทำต่อ
            List<_queryRangeClass> __queryEditList = new List<_queryRangeClass>();
            if (this._form.__queryEdit._a._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._a._queryTextBox.Text, (this._form.__queryEdit._a._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._b._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._b._queryTextBox.Text, (this._form.__queryEdit._b._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._c._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._c._queryTextBox.Text, (this._form.__queryEdit._c._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._d._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._d._queryTextBox.Text, (this._form.__queryEdit._d._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._e._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._e._queryTextBox.Text, (this._form.__queryEdit._e._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._f._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._f._queryTextBox.Text, (this._form.__queryEdit._f._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._g._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._g._queryTextBox.Text, (this._form.__queryEdit._g._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._h._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._h._queryTextBox.Text, (this._form.__queryEdit._h._getConditionCount > 0) ? true : false));

            if (this._form.__queryEdit._i._queryTextBox.Text.Length > 0)
                __queryEditList.Add(new _queryRangeClass(this._form.__queryEdit._i._queryTextBox.Text, (this._form.__queryEdit._i._getConditionCount > 0) ? true : false));


            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            ArrayList __queryIndexStr = new ArrayList();
            ArrayList __pageAssignQuery = new ArrayList();

            // reset ค่า
            _dataRangeSet = new ArrayList();

            for (int __queryIndex = 0; __queryIndex < __queryEditList.Count; __queryIndex++)
            {
                _queryRangeClass __queryClass = __queryEditList[__queryIndex];

                if (__queryClass._isConditionQuery == true)
                {
                    for (int __i = 0; __i < _packList.Count; __i++)
                    {
                        List<_conditionClass> __condition = (List<_conditionClass>)_packList[__i];

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery(_processQueryAndCondition(__queryClass._query, __condition)));
                        __queryIndexStr.Add(__queryIndex);
                        __pageAssignQuery.Add(__i);
                    }
                }
                else
                {
                    List<_conditionClass> __condition = (List<_conditionClass>)_packList[0];
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(_processQueryAndCondition(__queryClass._query, __condition)));
                    __queryIndexStr.Add(__queryIndex);
                    __pageAssignQuery.Add(-1);
                }
            }
            __query.Append("</node>");

            // get result
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __getDataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            try
            {
                // pack dataset ต่อ 1 page
                for (int __i = 0; __i < _packList.Count; __i++)
                {
                    DataSet __data = new DataSet();
                    for (int __loop = 0; __loop < __pageAssignQuery.Count; __loop++)
                    {
                        // หาว่าเป็น query ของ page อะไร
                        int __getQueryIndex = (int)__pageAssignQuery[__loop];
                        if (__getQueryIndex == __i || __getQueryIndex == -1)
                        {
                            DataTable __getDataTable = ((DataSet)__getDataResult[__loop]).Tables[0];
                            DataTable __dataForIns = __getDataTable.Copy();
                            //__getDataTable.Copy()
                            __dataForIns.TableName = __loop.ToString();
                            __data.Tables.Add(__dataForIns);
                            //__data.Tables[__data.Tables.Count -1] = __getDataTable;

                        }
                    }
                    _dataRangeSet.Add(__data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            // จับ document print เสียบเข้าไป เป็นชุด
            _buildDocGroup(true);
        }

        public void _buildDocGroup(Boolean isPrint)
        {
            //int __pageTotalAssignLength = this._docGroupLength * this._form._paperList.Count;


            ArrayList __docs = new ArrayList();

            // build เอกสารเป็นชุด
            bool _buildSuccess = true;
            for (int __loop = 0; __loop < _packList.Count; __loop++)
            {
                _pageAssign = new List<int>();
                _pageTotalAssign = new ArrayList(this._form._paperList.Count);
                for (int __loopPageAssign = 0; __loopPageAssign < 500; __loopPageAssign++)
                {
                    this._pageAssign.Add(0);
                }

                for (int __page = 0; __page < this._form._paperList.Count; __page++)
                {
                    _pageTotalAssign.Add(0);

                    // call merge dataset 
                    _setDataTableRangeIndex((DataSet)_dataRangeSet[__loop]);

                    bool __success = this._buildPage(__page);

                    if (__success == false)
                    {
                        _buildSuccess = false;
                        MessageBox.Show("จำนวน บรรทัดในตาราง ไม่พอสำหรับการพิพม์ กรุณาแก้ไขแบบฟอร์มใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    if (isPrint)
                    {
                        // add multiprint docs
                        PrintDocument __print = new PrintDocument();
                        if (this._printerNameResult != "")
                        {
                            __print.PrinterSettings.PrinterName = this._printerNameResult;
                        }
                        __print.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                        __print.BeginPrint += new PrintEventHandler(_printDocument_BeginPrint);

                        __docs.Add(__print);
                        _docIndexAssign.Add(__loop);
                    }

                }

                _listPageTotalAssign.Add(_pageTotalAssign);
                _listPageAssign.Add(this._pageAssign);
            }

            // assign page
            /*
            _pageAssign = new List<int>();
            for (int __loopPageAssign = 0; __loopPageAssign < 500; __loopPageAssign++)
            {
                this._pageAssign.Add(0);
            }

            for (int __totalPageAssign = 0; __totalPageAssign < _listPageAssign.Count; __totalPageAssign++)
            {
                ArrayList __totalPageAssignListInDocPage = (ArrayList)_listPageAssign[__totalPageAssign];
                for (int __pageCount = 0; __pageCount < __totalPageAssignListInDocPage.Count; __pageCount++)
                {
                    int __maxPage =
                }
            }*/

            // start print
            if (isPrint)
            {
                if (_buildSuccess)
                {
                    this._printDocument = new _myPrintDocument();

                    if (this._printerNameResult != "")
                    {
                        this._printDocument.PrinterSettings.PrinterName = this._printerNameResult;
                    }

                    //this._printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                    this._printDocument.BeginPrint += new PrintEventHandler(_printDocumentMaster_BeginPrint);

                    ((_myPrintDocument)this._printDocument)._Documents = (PrintDocument[])__docs.ToArray(typeof(PrintDocument));

                    if (this.PreviewPrintDialog)
                    {
                        // show select page dialog
                        //PrintDialog __dialog = new PrintDialog();
                        //__dialog.AllowSelection = true;
                        //__dialog.AllowSomePages = true;
                        //__dialog.Document = this._printDocument;
                        //__dialog.UseEXDialog = true;
                        //__dialog.PrintToFile = false;

                        //if (__dialog.ShowDialog(MyLib._myGlobal._mainForm) == DialogResult.OK)
                        //{
                        //    this._printDocument.PrinterSettings = __dialog.PrinterSettings;
                        //}
                        this._printPreviewDialog = new PrintPreviewDialog();

                        this._printPreviewDialog.Document = this._printDocument;
                        this._printPreviewDialog.ShowDialog();
                        this._printPreviewDialog.Dispose();
                    }
                    else
                    {
                        // print it
                        try
                        {
                            this._printDocument.Print();
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public void _build(Boolean isPrint)
        {
            _pageTotalAssign = new ArrayList(this._form._paperList.Count);
            ArrayList __docs = new ArrayList();

            bool _buildSuccess = true;

            for (int __page = 0; __page < this._form._paperList.Count; __page++)
            {
                _pageTotalAssign.Add(0);
                bool __success = this._buildPage(__page);

                if (__success == false)
                {
                    _buildSuccess = false;
                    MessageBox.Show("จำนวน บรรทัดในตาราง ไม่พอสำหรับการพิพม์ กรุณาแก้ไขแบบฟอร์มใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                if (isPrint)
                {
                    // add multiprint docs
                    PrintDocument __print = new PrintDocument();
                    if (this._printerNameResult != "")
                    {
                        __print.PrinterSettings.PrinterName = this._printerNameResult;
                    }
                    __print.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                    __print.BeginPrint += new PrintEventHandler(_printDocument_BeginPrint);

                    __docs.Add(__print);
                }

            }


            // ถ้าหากมีการพิมพ์แบบ ไม่รวมเอกสารชุด ให้ process เป็นชุดและหน้า ส่งไปให้ myPrintDocument ได้เลย
            // ex page 5 = paper(1) page(3) กรณีนี้ paper(1) = 2 , paper(2) = 5

            if (_printRangeType == PrintRange.SomePages)
            {
                if (_includeDocSeries == false || (_printSeriesOption == PrintRange.SomePages))
                {
                    if (_includeDocSeries == false)
                    {
                        // ตีความจากหน้าที่เลือก ว่าใช้เอกสารอะไรบ้าง
                        List<int> __docListNumber = new List<int>();
                        foreach (int __pageNumber in _printRange)
                        {
                            int __pageIndexToPrint = (__pageNumber.Equals(999999999)) ? _pageTotal : __pageNumber;

                            if (__pageIndexToPrint <= _pageTotal)
                            {
                                //int __pageIndexToPrint = (__pageNumber.Equals(999999999)) ? _pageTotal : __pageNumber;
                                int __pageForPrint = _pageAssign[__pageIndexToPrint - 1];

                                if (!__docListNumber.Contains(__pageForPrint))
                                    __docListNumber.Add(__pageForPrint);
                            }
                        }

                        // remove page
                        ArrayList __docsFilter = new ArrayList();
                        foreach (int _filterNumber in __docListNumber)
                        {
                            __docsFilter.Add(__docs[_filterNumber]);
                        }

                        __docs = null;
                        __docs = __docsFilter;
                        _pagePrintRange = new List<_pageForPrintRange>();
                        // get sub page for print range ex. doc(0) = 1,2 | doc(1) = 3
                        foreach (int __docNumber in __docListNumber)
                        {
                            List<int> __subPageForPrint = new List<int>();
                            foreach (int __pageNumber in _printRange)
                            {
                                int __pageIndexToPrint = (__pageNumber.Equals(999999999)) ? _pageTotal : __pageNumber;
                                int __pageForPrint = _pageAssign[__pageIndexToPrint - 1];

                                if (__pageForPrint.Equals(__docNumber))
                                {
                                    // ลบ จำนวนหน้าที่ผ่านมา
                                    int __pageRangeNumber = __pageIndexToPrint;
                                    if (__pageForPrint != 0)
                                    {
                                        int __passpageForPrint = 0;
                                        int __lastToPage = __pageForPrint - 1;
                                        for (int __i = 0; __i <= __lastToPage; __i++)
                                        {
                                            if (__i < _pageTotalAssign.Count)
                                            {
                                                __passpageForPrint += (int)_pageTotalAssign[__i];
                                            }
                                        }

                                        __pageRangeNumber -= __passpageForPrint;
                                    }

                                    __subPageForPrint.Add(__pageRangeNumber);
                                }
                            }

                            _pagePrintRange.Add(new _pageForPrintRange(__docNumber, __subPageForPrint.ToArray()));
                        }
                    }
                    else if (_printSeriesOption == PrintRange.SomePages && _printRange.Count > 0)
                    {
                        // remove page
                        ArrayList __docsFilter = new ArrayList();
                        foreach (int _filterNumber in _printSeriesRange)
                        {
                            __docsFilter.Add(__docs[_filterNumber - 1]);
                        }
                        __docs = null;
                        __docs = __docsFilter;

                        // ระบุ range
                        _pagePrintRange = new List<_pageForPrintRange>();

                        foreach (int __seriesRangeNumber in _printSeriesRange)
                        {
                            if (__seriesRangeNumber - 1 < this._form._paperList.Count)
                            {
                                List<int> __subPageForPrint = new List<int>();
                                foreach (int __pageNumber in _printRange)
                                {
                                    int __pageIndexToPrint = (__pageNumber.Equals(999999999)) ? (int)_pageTotalAssign[__seriesRangeNumber - 1] : __pageNumber;

                                    __subPageForPrint.Add(__pageIndexToPrint);
                                }

                                _pagePrintRange.Add(new _pageForPrintRange(__seriesRangeNumber - 1, __subPageForPrint.ToArray()));
                            }
                        }
                    }
                }
            }

            // เริ่มพิมพ์
            if (isPrint)
            {
                if (_buildSuccess)
                {
                    this._printDocument = new _myPrintDocument();

                    if (this._printerNameResult != "")
                    {
                        this._printDocument.PrinterSettings.PrinterName = this._printerNameResult;
                    }

                    //this._printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                    this._printDocument.BeginPrint += new PrintEventHandler(_printDocumentMaster_BeginPrint);

                    ((_myPrintDocument)this._printDocument)._Documents = (PrintDocument[])__docs.ToArray(typeof(PrintDocument));

                    if (this.PreviewPrintDialog)
                    {
                        // show select page dialog
                        //PrintDialog __dialog = new PrintDialog();
                        //__dialog.AllowSelection = true;
                        //__dialog.AllowSomePages = true;
                        //__dialog.Document = this._printDocument;
                        //__dialog.UseEXDialog = true;
                        //__dialog.PrintToFile = false;

                        //if (__dialog.ShowDialog(MyLib._myGlobal._mainForm) == DialogResult.OK)
                        //{
                        //    this._printDocument.PrinterSettings = __dialog.PrinterSettings;
                        //}
                        this._printPreviewDialog = new PrintPreviewDialog();

                        this._printPreviewDialog.Document = this._printDocument;
                        this._printPreviewDialog.ShowDialog();
                        this._printPreviewDialog.Dispose();
                    }
                    else
                    {
                        // print it
                        try
                        {
                            this._printDocument.Print();
                        }
                        catch
                        {
                        }
                    }
                }
            }

            // clear docs
            for (int __docCount = __docs.Count - 1; __docCount >= 0; __docCount--)
            {
                PrintDocument __dockPage = (PrintDocument)__docs[__docCount];
                __dockPage.Dispose();
            }
        }


        #endregion

        public class _conditionClass
        {
            public string _fieldName;
            public string _value;

            public _conditionClass(string fieldName, string value)
            {
                this._fieldName = fieldName;
                this._value = value;
            }
        }

        public class _pageForPrintRange
        {
            public int _pageIndex;
            public int[] _pageRange;

            public _pageForPrintRange()
            {
            }

            public _pageForPrintRange(int __pageIndex, int[] __pageRange)
            {
                _pageIndex = __pageIndex;
                _pageRange = __pageRange;
            }
        }

        public void Dispose()
        {
            this._printDocument.Dispose();
            this._pageAssign.Clear();

            if (this._a_table != null)
                this._a_table.Dispose();
            if (this._b_table != null)
                this._b_table.Dispose();
            if (this._c_table != null)
                this._c_table.Dispose();
            if (this._d_table != null)
                this._d_table.Dispose();
            if (this._e_table != null)
                this._e_table.Dispose();
            if (this._f_table != null)
                this._f_table.Dispose();
            if (this._g_table != null)
                this._g_table.Dispose();
            if (this._h_table != null)
                this._h_table.Dispose();
            if (this._i_table != null)
                this._i_table.Dispose();


            this._form.Dispose();
            GC.SuppressFinalize(this);


        }
    }
}
