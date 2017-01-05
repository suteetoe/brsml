using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;
/*using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;*/

namespace SMLReport._report
{
    public partial class _view : UserControl
    {
        public Font _fontStandard = new Font("Angsana New", 9, FontStyle.Regular);
        public Font _fontHeader1 = new Font("Angsana New", 12, FontStyle.Bold);
        public Font _fontHeader2 = new Font("Angsana New", 9, FontStyle.Bold);
        private float _drawScale = 1f;
        private float _drawScaleOld = 1f;
        public ArrayList _objectList = new ArrayList();
        /// <summary>
        /// Split Row Singha
        /// </summary>
        public Boolean _splitRowOnOverFlowPage = false;

        /// <summary>
        /// เก็บจำนวนแถว ของข้อมูล
        /// </summary>
        public ArrayList _objectDataList = new ArrayList();
        ArrayList _pageList = new ArrayList();
        private _excelClass _reportexcel;
        int _maxcol = 0;
        /// <summary>
        /// ข้อความจะแสดงด้านล่าง ใช้สำหรับสื่อสารระหว่าง Thread
        /// </summary>
        public String _processMessage = "";
        /// <summary>
        /// % ของ ProgressBar ด้านล่าง ใช้สำหรับสื่อสารระหว่าง Thread
        /// </summary>
        public int _progessBarValue = 0;
        //
        _reportType reportConstType;
        //
        /// <summary>
        /// ชื่อ รายงานออก excel
        /// </summary>
        public string __excelFlieName = "SMLExportExcel";
        Thread _loadDataStreamThread = null;
        Thread _buildReportThread = null;
        Thread _loadDataThread = null;
        /// <summary>
        /// เมื่อเป็น True Timer จะสั่งให้สร้างรายงานเลย
        /// </summary>
        public Boolean _buildReportActive = false;
        /// <summary>
        /// สร้างรายงานสำเร็จจะเป็น True
        /// </summary>
        Boolean _buildReportSuccess = false;
        /// <summary>
        /// Load ข้อมูลสำเร็จหรือไม่ ถ้าสำเร็จ ระบบจะสร้างรายงานให้ต่อเลย
        /// </summary>
        public Boolean _loadDataByThreadSuccess = false;
        /// <summary>
        /// เริ่มนับเวลา
        /// </summary>
        private Boolean _startCount = false;
        private int _countTime = 0;

        public float _footerHeight = 0;

        public _view()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStripPreview.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                //this._toolStripPreview.Font = new Font(_g.g._companyProfile._reportFontName, _g.g._companyProfile._reportFontSize);

                if (_g.g._companyProfile._reportFontName.Length > 0)
                {
                    this._fontStandard = new Font(_g.g._companyProfile._reportFontName, _g.g._companyProfile._reportFontSize);
                }

                if (_g.g._companyProfile._reportHeaderFontName_1.Length > 0)
                {
                    this._fontHeader1 = new Font(_g.g._companyProfile._reportHeaderFontName_1, _g.g._companyProfile._reportHeaderFontSize_1);
                }

                if (_g.g._companyProfile._reportHeaderFontName_2.Length > 0)
                {
                    this._fontHeader2 = new Font(_g.g._companyProfile._reportHeaderFontName_2, _g.g._companyProfile._reportHeaderFontSize_2);
                }
            }
            //
            if (MyLib._myGlobal._isDesignMode == false)
            {
                //
                try
                {
                    this._pageSetupDialog.PageSettings.PaperSize = this._printDocument.PrinterSettings.DefaultPageSettings.PaperSize;
                }
                catch
                {
                    MessageBox.Show("Please set Default Printer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                this._pageSetupDialog.PageSettings.Landscape = false;
                this._pageSetupDialog.PageSettings.Margins.Left = 50;
                this._pageSetupDialog.PageSettings.Margins.Right = 50;
                this._pageSetupDialog.PageSettings.Margins.Top = 50;
                this._pageSetupDialog.PageSettings.Margins.Bottom = 50;
                //
                this._printDocument.OriginAtMargins = false;
                //
                this._scaleComboBox.SelectedIndex = 7;
                this._scaleComboBox.SelectedIndexChanged += new EventHandler(_scaleComboBox_SelectedIndexChanged);
                //
                this._paper._pageSetupDialog = this._pageSetupDialog;
                this._paper._objectList = _objectList;
                this._paper._pageList = _pageList;
                this._paper._beforePaperDrawReportArgs += _paper__beforePaperDrawReportArgs;
                //
                this._rulerTopControl.BringToFront();
                this._rulerLeftControl.BringToFront();
                this._vScrollBar.BringToFront();
                this._hScrollBar.BringToFront();
                this._vScrollBar.Visible = false;
                this._hScrollBar.Visible = false;
                this._paper.Location = new Point(this._rulerLeftControl.Width, this._rulerTopControl.Height);
                this._panelView.SizeChanged += new EventHandler(_panelView_SizeChanged);
                _vScrollBar.ValueChanged += new EventHandler(_vScrollBar_ValueChanged);
                _hScrollBar.ValueChanged += new EventHandler(_hScrollBar_ValueChanged);
                //
                _textBoxPageNumber.Leave += new EventHandler(_textBoxPageNumber_Leave);
                //
                this._printDocument.BeginPrint += new PrintEventHandler(_printDocument_BeginPrint);
                this._printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                this._printDocument.EndPrint += new PrintEventHandler(_printDocument_EndPrint);
                //
                this.Disposed += new EventHandler(_view_Disposed);
            }
        }

        private void _paper__beforePaperDrawReportArgs(_pageListType pageListType)
        {
            if (_beforeReportDrawPaperArgs != null)
            {
                _beforeReportDrawPaperArgs(pageListType);
            }
        }

        void _view_Disposed(object sender, EventArgs e)
        {
            try
            {
                if (_loadDataStreamThread != null && _loadDataStreamThread.IsAlive)
                {
                    _loadDataStreamThread.Abort();
                }
                if (_buildReportThread != null && _buildReportThread.IsAlive)
                {
                    _buildReportThread.Abort();
                }
                if (_loadDataThread != null && _loadDataThread.IsAlive)
                {
                    _loadDataThread.Abort();
                }
            }
            catch
            {
            }
        }

        void _panelView_SizeChanged(object sender, EventArgs e)
        {
            _calcPageSize();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                if (_vScrollBar.Value - e.Delta > 0)
                {
                    _vScrollBar.Value -= e.Delta;
                }
                else
                {
                    _vScrollBar.Value = 0;
                }
            }
            catch
            {
                _vScrollBar.Value = _vScrollBar.Maximum;
            }
            this.Invalidate();
            base.OnMouseWheel(e);
        }

        void _printDocument_EndPrint(object sender, PrintEventArgs e)
        {
            this._drawScale = this._drawScaleOld;
            this._paper._drawScale = this._drawScale;
            this._calcMargin();
            this._paper.Invalidate();
            this.Invalidate();
        }

        void _printDocument_BeginPrint(object sender, PrintEventArgs e)
        {

            PrintDocument __printDocument = (PrintDocument)sender;
            if (__printDocument.PrinterSettings.PrintRange == PrintRange.SomePages)
            {
                this._paper._pageCurrent = __printDocument.PrinterSettings.FromPage - 1;
            }
            //else if (__printDocument.PrinterSettings.PrintRange == PrintRange.CurrentPage)
            //{
            //    this._paper._pageCurrent = this._paper._pageCurrent;
            //}
            else if (__printDocument.PrinterSettings.PrintRange == PrintRange.AllPages)
            {
                this._paper._pageCurrent = 0;
            }
            this._drawScaleOld = this._drawScale;
            this._drawScale = 1.0f;
            this._calcMargin();
        }

        void _printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics __getGraphics = e.Graphics;
            __getGraphics.SmoothingMode = SmoothingMode.HighQuality;
            __getGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            PrintDocument __printDocument = (PrintDocument)sender;
            float __y = this._paper._drawReport(this._paper._pageCurrent, __getGraphics);
            this._paper._drawFooter(this._paper._pageCurrent, __getGraphics, __y);
            

            if (__printDocument.PrinterSettings.PrintRange == PrintRange.CurrentPage)
            {
                e.HasMorePages = false;
                return;
            }
            if (++this._paper._pageCurrent < this._paper._pageMax)
            {
                if (__printDocument.PrinterSettings.PrintRange == PrintRange.SomePages && this._paper._pageCurrent > __printDocument.PrinterSettings.ToPage - 1)
                {
                    e.HasMorePages = false;
                }
                else
                    e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                _gotoPage();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _gotoPage()
        {
            try
            {
                int __getPageNumber = MyLib._myGlobal._intPhase(_textBoxPageNumber.Text);
                if (__getPageNumber >= 0 && __getPageNumber <= _paper._pageMax)
                {
                    _paper.PageCurrent = __getPageNumber;
                    _calcPageSize();
                }
                else
                {
                    MessageBox.Show("Page not found.");
                }
            }
            catch
            {
            }
        }

        void _textBoxPageNumber_Leave(object sender, EventArgs e)
        {
            _gotoPage();
        }

        void _calcMargin()
        {
            this._paper._drawScale = this._drawScale;
            this._paper._leftMargin = (this._pageSetupDialog.PageSettings.Margins.Left * this._drawScale);
            this._paper._topMargin = (this._pageSetupDialog.PageSettings.Margins.Top * this._drawScale);
        }

        void _calcPageSize()
        {
            try
            {
                switch (_scaleComboBox.SelectedIndex)
                {
                    case 0: this._drawScale = 5f; break;
                    case 1: this._drawScale = 2f; break;
                    case 2: this._drawScale = 1.5f; break;
                    case 3: this._drawScale = 1f; break;
                    case 4: this._drawScale = 0.75f; break;
                    case 5: this._drawScale = 0.5f; break;
                    case 6: this._drawScale = 0.25f; break;
                    case 7:
                        this._drawScale = (float)Math.Round(((decimal)(this._panelView.Width) - (decimal)80.0) / ((this._pageSetupDialog.PageSettings.Landscape) ? this._pageSetupDialog.PageSettings.PaperSize.Height : this._pageSetupDialog.PageSettings.PaperSize.Width), 2);
                        break;
                    case 8:
                        float _drawScale1 = (float)Math.Round(((decimal)(this._panelView.Height) - (decimal)80.0) / ((this._pageSetupDialog.PageSettings.Landscape) ? this._pageSetupDialog.PageSettings.PaperSize.Width : this._pageSetupDialog.PageSettings.PaperSize.Height), 2);
                        float _drawScale2 = (float)Math.Round(((decimal)(this._panelView.Width) - (decimal)80.0) / ((this._pageSetupDialog.PageSettings.Landscape) ? this._pageSetupDialog.PageSettings.PaperSize.Height : this._pageSetupDialog.PageSettings.PaperSize.Width), 2);
                        this._drawScale = (_drawScale1 < _drawScale2) ? _drawScale1 : _drawScale2;
                        break;
                }
            }
            catch
            {
                // toe fix bug error windows xp on initial resize
                this._drawScale = 1f;
            }

            this._calcMargin();
            this._paper._topLeftPaper = new Point(20, 20);
            this._paper.Location = new Point(this._rulerLeftControl.Width, this._rulerTopControl.Height);
            this._paper._area.Invalidate();
            this._calcDrawAreaSize();
            this._ruleRefresh();
            this.Invalidate();
        }

        void _scaleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _calcPageSize();
            this._textBoxPageNumber.Focus();
        }

        public event GetObjectEventHandler _getObject;
        public event LoadDataEventHandler _loadData;
        public event GetDataObjectEventHandler _getDataObject;
        public event LoadDataStreamEventHandle _loadDataStream;
        public event LoadDataByThreadEventHandle _loadDataByThread;
        public event ColumnResourceEventHandler _columnResource;
        public event BuildSuccessEventHandle _buildSuccess;
        public event GetTotalCurrentRow _getTotalCurrentRow;
        public event beforeReportDrawPaper _beforeReportDrawPaperArgs;

        public void _buildObject(object objectSource, float objectWidth)
        {
            _objectListType __object = (_objectListType)objectSource;
            float __height = 0;
            try
            {
                // คำนวณตำแหน่งของ Column และ Cell
                float __columnX = 0;
                for (int __loop = 0; __loop < __object._columnList.Count; __loop++)
                {
                    //somruk
                    if (this._maxcol < __object._columnList.Count)
                    {
                        this._maxcol = __object._columnList.Count;
                    }
                    if (__object._columnList[__loop].GetType() == typeof(_columnListType))
                    {
                        // กรณีเป็น Column และ Cell ทั่วไป
                        _columnListType __getColumn = (_columnListType)__object._columnList[__loop];
                        __getColumn._width = objectWidth * (__getColumn._columnWidth / 100.0f);
                        // คำนวณหาความสูงของแต่ละบรรทัด
                        ArrayList __lineSpace = new ArrayList();
                        for (int __add = 0; __add <= __getColumn._maxLine; __add++)
                        {
                            _linePositionType __newLineSpace = new _linePositionType();
                            __newLineSpace._lineSpace = 0;
                            __newLineSpace._fromTop = 0;
                            __lineSpace.Add(__newLineSpace);
                        }
                        // คำนวณความสูง Column
                        SizeF __stringColumnNameSize = this._paper._graphicSystem.MeasureString(__getColumn._text, __getColumn._fontData);
                        //SizeF __stringColumnNameSize = this._paper._graphicSystem.MeasureString(__getColumn._text, __getColumn._columnHeadFont);
                        float __stringHeight = __stringColumnNameSize.Height * (this._paper._lineSpaceing / 100);
                        if (__stringHeight == 0 && __getColumn._fontData != null)
                        {
                            __stringHeight = __getColumn._fontData.Height;
                        }
                        if (__stringHeight > __height)
                        {
                            __height = (int)__stringHeight;
                        }
                        __getColumn._height = __height;
                        __getColumn._position = new Point((int)__columnX, 0);
                        __columnX += __getColumn._width;
                        __object._columnList[__loop] = (_columnListType)__getColumn;
                        // คำนวณตำแหน่งของ Cell
                        for (int __cell = 0; __cell < __getColumn._cellList.Count; __cell++)
                        {
                            _cellListType __getCell = (_cellListType)__getColumn._cellList[__cell];
                            string __getString = _paper._processValue(__getCell._text);
                            SizeF __stringSize = this._paper._graphicSystem.MeasureString(__getString, __getCell._font);
                            __stringSize.Height = __stringSize.Height * (this._paper._lineSpaceing / 100);
                            __getCell._height = __stringSize.Height;
                            __getCell._width = __stringSize.Width;
                            if (__getCell._autoPosition)
                            {
                                if ((int)__stringSize.Height > ((_linePositionType)__lineSpace[__getCell._position.X])._lineSpace)
                                {
                                    ((_linePositionType)__lineSpace[__getCell._position.X])._lineSpace = (int)__stringSize.Height;
                                }
                            }
                            else
                            {
                                if ((int)__stringSize.Height + __getCell._position.Y > __height)
                                {
                                    __height = (int)__stringSize.Height + __getCell._position.Y;
                                }
                            }
                        }
                        // คำนวณ Line Space
                        float __calcHeight = 0;
                        for (int __line = 0; __line < __lineSpace.Count; __line++)
                        {
                            _linePositionType __getLinePosition = (_linePositionType)__lineSpace[__line];
                            __getLinePosition._fromTop = __calcHeight;
                            __calcHeight += __getLinePosition._lineSpace;
                        }
                        if (__calcHeight > __height)
                        {
                            __height = __calcHeight;
                        }
                        // กำหนดตำแหน่งของ Cell
                        for (int __cell = 0; __cell < __getColumn._cellList.Count; __cell++)
                        {
                            _cellListType __getCell = (_cellListType)__getColumn._cellList[__cell];
                            if (__getCell._autoPosition)
                            {
                                _linePositionType __getLinePosition = (_linePositionType)__lineSpace[__getCell._position.X];
                                switch (__getCell._alignCell)
                                {
                                    case _cellAlign.Left:
                                        {
                                            // left
                                            __getCell._position = new Point(0, (int)__getLinePosition._fromTop);
                                        }
                                        break;
                                    case _cellAlign.Right:
                                        {
                                            // Right
                                            __getCell._position = new Point((int)(objectWidth - __getCell._width), (int)__getLinePosition._fromTop);
                                        }
                                        break;
                                    case _cellAlign.Center:
                                        {
                                            // Center
                                            __getCell._position = new Point((int)((objectWidth / 2) - (__getCell._width / 2)), (int)__getLinePosition._fromTop);
                                        }
                                        break;
                                }
                                __getColumn._cellList[__cell] = (_cellListType)__getCell;
                            }
                        }
                        __object._columnList[__loop] = (_columnListType)__getColumn;
                    }
                    else
                    {
                        if (__object._columnList[__loop].GetType() == typeof(_columnDataListType))
                        {
                            // กรณีที่เป็น object data
                            _columnDataListType __getDataColumn = (_columnDataListType)__object._columnList[__loop];
                            if (__getDataColumn._objectSource != null && __getDataColumn._isHide == false)
                            {
                                _columnListType __getColumn = (_columnListType)__getDataColumn._objectSource._columnList[__getDataColumn._columnAddr];

                                float __colWidthCalc = (__getDataColumn._totalColumnWidth != 0) ? (__getDataColumn._totalColumnWidth * objectWidth) * this._drawScale : (__getColumn._width - __getDataColumn._spaceBeforeText);

                                // toe fix searchwidth
                                //ArrayList __getString = MyLib._myUtil._cutString(this._paper._graphicSystem, __getDataColumn._text, __getColumn._fontData, (float)(__getColumn._width - __getDataColumn._spaceBeforeText), false);
                                ArrayList __getString = MyLib._myUtil._cutString(this._paper._graphicSystem, __getDataColumn._text, __getColumn._fontData, __colWidthCalc, false);

                                if (__getDataColumn._breakLine == true)
                                {
                                    string __getFirstString = __getString[0].ToString();
                                    __getString = new ArrayList();
                                    __getString.Add(__getFirstString);
                                }

                                int __sumHeight = 0;
                                for (int __line = 0; __line < __getString.Count; __line++)
                                {
                                    SizeF __stringSize = this._paper._graphicSystem.MeasureString(__getString[__line].ToString(), __getColumn._columnDetailFont);
                                    __sumHeight += (int)(__stringSize.Height * (this._paper._lineSpaceing / 100));
                                    if (__sumHeight > __height)
                                    {
                                        __height = __sumHeight;
                                    }
                                }

                            }
                        }
                    }
                }
                if (__object._autoSize)
                {
                    // กำหนดขนาด Object อัตโนมัติ
                    __object._size = new Size((int)objectWidth, (__object._show == false) ? 0 : (int)__height);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// ทำหลังจาก Load Data จาก Thread มาแล้ว
        /// </summary>
        private void _buildReportNow()
        {
            this._progessBarValue = 0;
            this._processMessage = "Build Report";
            if (_getObject != null)
            {
                // ดึงข้อมูลทั้งหมด
                _getObject(this._objectList, _objectType.Header);
                _getObject(this._objectList, _objectType.PageHeader);
                _getObject(this._objectList, _objectType.Detail);
                _getObject(this._objectList, _objectType.PageFooter);
                _getObject(this._objectList, _objectType.Footer);
                _getObject(this._objectList, _objectType.Bottom);
            }
            // คำนวณขนาดของ Object แต่ละรายการ
            int _pageWidth = (int)(((this._pageSetupDialog.PageSettings.Landscape) ? this._pageSetupDialog.PageSettings.PaperSize.Height : this._pageSetupDialog.PageSettings.PaperSize.Width) - (this._pageSetupDialog.PageSettings.Margins.Left + this._pageSetupDialog.PageSettings.Margins.Right));
            int _pageHeight = (int)(((this._pageSetupDialog.PageSettings.Landscape) ? this._pageSetupDialog.PageSettings.PaperSize.Width : this._pageSetupDialog.PageSettings.PaperSize.Height) - (this._pageSetupDialog.PageSettings.Margins.Top + this._pageSetupDialog.PageSettings.Margins.Bottom));
            for (int __loop = 0; __loop < _objectList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)_objectList[__loop];
                _buildObject(__getObject, _pageWidth);
            }
            if (_getDataObject != null)
            {
                this._processMessage = "Build Report Get Data (Please Wait)";
                _getDataObject();
            }
            // คำนวณขนาดของ Object แต่ละรายการ
            for (int __loop = 0; __loop < _objectDataList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)_objectDataList[__loop];
                _buildObject(__getObject, _pageWidth);
            }
            // ประกอบหน้า
            _paper._leftMargin = (int)(this._pageSetupDialog.PageSettings.Margins.Left * this._drawScale);
            _paper._topMargin = (int)(this._pageSetupDialog.PageSettings.Margins.Top * this._drawScale);
            _pageList.Clear();
            int __pageNumber = 0;
            int __pageAddr = 0;
            bool __newPage = true;
            float __pageHeight = 0;

            float __footerHeight = 0;

            for (int __loop = 0; __loop < _objectList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)_objectList[__loop];
                if (__getObject._type == _objectType.PageFooter)
                {
                    __footerHeight = __getObject._size.Height;

                    break;
                }
            }


            if (_footerHeight != 0)
            {
                __footerHeight = _footerHeight;
            }

            if (__footerHeight > 0)
            {
                _pageHeight -= (int)__footerHeight;
            }



            if (this.reportConstType == _reportType.Standard)
            {
                for (int __loop = 0; __loop < _objectDataList.Count; __loop++)
                {
                    /*
                    // toe เพิ่มแสดง footer report
                    if (_getTotalCurrentRow != null)
                    {
                        _objectListType totalCurrentRow = _getTotalCurrentRow((_objectListType)_objectDataList[__loop]);
                        if (totalCurrentRow != null)
                        {
                            _buildObject(totalCurrentRow, _pageWidth);
                        }

                        // เช็คแล้ว ไม่พอพิมพ์ 
                    }*/



                    _objectListType __getObject = (_objectListType)_objectDataList[__loop];
                    if (__pageHeight + (int)__getObject._size.Height > _pageHeight)
                    {
                        // add footer page

                        // call event sum
                        __findAndAddObject(((_pageListType)_pageList[__pageAddr])._objectList, _objectType.PageFooter, _pageHeight);
                        if (_getTotalCurrentRow != null)
                        {
                            _getTotalCurrentRow(((_pageListType)_pageList[__pageAddr])._objectList, _objectList);
                        }

                        // สั่งขึ้นหน้าใหม่
                        __newPage = true;
                    }

                    // toe split row
                    if (__newPage && __pageHeight + (int)__getObject._size.Height > _pageHeight && this._splitRowOnOverFlowPage)
                    {

                        // split and next page
                        {
                            // หา max row 
                            int __maxLineInRow = 0;
                            for (int __colunListIndex = 0; __colunListIndex < __getObject._columnList.Count; __colunListIndex++)
                            {
                                if (__getObject._columnList[__colunListIndex].GetType() == typeof(_columnDataListType))
                                {
                                    _columnDataListType __getDataColumn = (_columnDataListType)__getObject._columnList[__colunListIndex];
                                    if (__getDataColumn._objectSource != null && __getDataColumn._isHide == false)
                                    {
                                        _columnListType __getColumn = (_columnListType)__getDataColumn._objectSource._columnList[__getDataColumn._columnAddr];
                                        float __colWidthCalc = (__getDataColumn._totalColumnWidth != 0) ? (__getDataColumn._totalColumnWidth * _pageWidth) * this._drawScale : (__getColumn._width - __getDataColumn._spaceBeforeText);
                                        ArrayList __getString = MyLib._myUtil._cutString(this._paper._graphicSystem, __getDataColumn._text, __getColumn._fontData, __colWidthCalc, false);


                                        if (__maxLineInRow < __getString.Count)
                                            __maxLineInRow = __getString.Count;
                                    }
                                }
                            }

                            Boolean __nextPageProcess = false;

                            for (int __lineDataIndex = 0; __lineDataIndex < __maxLineInRow; __lineDataIndex++)
                            {
                                bool __isDrawLine = false;
                                float __currentLineHeight = 0f;

                                // หาความสูงแต่ละ row
                                for (int __colunListIndex = 0; __colunListIndex < __getObject._columnList.Count; __colunListIndex++)
                                {
                                    if (__getObject._columnList[__colunListIndex].GetType() == typeof(_columnDataListType))
                                    {
                                        _columnDataListType __getDataColumn = (_columnDataListType)__getObject._columnList[__colunListIndex];
                                        if (__getDataColumn._objectSource != null && __getDataColumn._isHide == false)
                                        {

                                            _columnListType __getColumn = (_columnListType)__getDataColumn._objectSource._columnList[__getDataColumn._columnAddr];
                                            float __colWidthCalc = (__getDataColumn._totalColumnWidth != 0) ? (__getDataColumn._totalColumnWidth * _pageWidth) * this._drawScale : (__getColumn._width - __getDataColumn._spaceBeforeText);
                                            ArrayList __getString = MyLib._myUtil._cutString(this._paper._graphicSystem, __getDataColumn._text, __getColumn._fontData, __colWidthCalc, false);

                                            if (__lineDataIndex < __getString.Count)
                                            {
                                                // มีบรรทัดที่พิมพ์ ใน line นี้
                                                SizeF __stringSize = this._paper._graphicSystem.MeasureString(__getString[__lineDataIndex].ToString(), __getColumn._columnDetailFont);

                                                if (__currentLineHeight < __stringSize.Height)
                                                {
                                                    __currentLineHeight = __stringSize.Height;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (__pageHeight + (int)__currentLineHeight > _pageHeight)
                                {
                                    // ชึ้นหน้าใหม่
                                    __pageHeight = 0;
                                    __pageNumber++;
                                    this._processMessage = "Build Report Page : " + __pageNumber.ToString();
                                    _paper._pageMax = __pageNumber;
                                    _pageListType __newDataPage = new _pageListType();
                                    float __headerHeight = __findAndAddObject(__newDataPage._objectList, _objectType.Header, __pageHeight);
                                    __pageHeight += __headerHeight;
                                    float __detailHeight = __findAndAddObject(__newDataPage._objectList, _objectType.Detail, __pageHeight);
                                    __pageHeight += __detailHeight;

                                    //__findAndAddObject(__newDataPage._objectList, _objectType.PageFooter, _pageHeight);
                                    __newDataPage._paperPageSize = new Size(_pageWidth, _pageHeight);
                                    if (__footerHeight > 0)
                                    {
                                        __newDataPage._haveFooterPage = true;
                                    }

                                    __pageAddr = _pageList.Add(__newDataPage);
                                }

                                // วาด ลงไปในหน้านี้
                                //SMLReport._report._objectListType __dataObject = this._addObject(_objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None); // เส้นบรรทัด
                                _objectListType __dataObject = new _objectListType();
                                __dataObject._type = SMLReport._report._objectType.Detail;
                                __dataObject._autoSize = true;
                                __dataObject._position = new Point(0, 0);
                                __dataObject._leftMargin = 0;
                                __dataObject._leftMarginByPersent = true;
                                __dataObject._border = SMLReport._report._columnBorder.None;
                                __dataObject._show = true;

                                this._createEmtryColumn(((_columnDataListType)__getObject._columnList[0])._objectSource, __dataObject);


                                for (int __colunListIndex = 0; __colunListIndex < __getObject._columnList.Count; __colunListIndex++)
                                {
                                    if (__getObject._columnList[__colunListIndex].GetType() == typeof(_columnDataListType))
                                    {
                                        _columnDataListType __getDataColumn = (_columnDataListType)__getObject._columnList[__colunListIndex];
                                        if (__getDataColumn._objectSource != null && __getDataColumn._isHide == false)
                                        {

                                            _columnListType __getColumn = (_columnListType)__getDataColumn._objectSource._columnList[__getDataColumn._columnAddr];
                                            float __colWidthCalc = (__getDataColumn._totalColumnWidth != 0) ? (__getDataColumn._totalColumnWidth * _pageWidth) * this._drawScale : (__getColumn._width - __getDataColumn._spaceBeforeText);
                                            ArrayList __getString = MyLib._myUtil._cutString(this._paper._graphicSystem, __getDataColumn._text, __getColumn._fontData, __colWidthCalc, false);

                                            if (__lineDataIndex < __getString.Count)
                                            {
                                                if (__lineDataIndex > 0 && ((_columnDataListType)__getObject._columnList[__colunListIndex])._breakLine == true)
                                                {
                                                    this._addDataColumn(((_columnDataListType)__getObject._columnList[0])._objectSource, __dataObject, __colunListIndex, "", __getColumn._fontData, __getColumn._alignCell, 0, __getColumn._border, _cellType.String, Color.Black, false, false);
                                                }
                                                else
                                                {
                                                    this._addDataColumn(((_columnDataListType)__getObject._columnList[0])._objectSource, __dataObject, __colunListIndex, __getString[__lineDataIndex].ToString(), __getColumn._fontData, __getColumn._alignCell, 0, __getColumn._border, __getDataColumn._dataType, Color.Black, false, false);
                                                }
                                            }
                                            else
                                            {
                                                this._addDataColumn(((_columnDataListType)__getObject._columnList[0])._objectSource, __dataObject, __colunListIndex, "", __getColumn._fontData, __getColumn._alignCell, 0, __getColumn._border, _cellType.String, Color.Black, false, false);
                                            }
                                        }
                                    }
                                }

                                __dataObject._isDataRow = true;
                                __dataObject._position = new Point(__getObject._position.X, (int)__pageHeight);
                                __dataObject._size = new Size((int)_pageWidth, (__dataObject._show == false) ? 0 : (int)__currentLineHeight);
                                //_objectDataList[__loop] = (_objectListType)__getObject;
                                __pageHeight += (int)__currentLineHeight;
                                ((_pageListType)_pageList[__pageAddr])._pageSize = new Size(_pageWidth, (int)__pageHeight);
                                ((_pageListType)_pageList[__pageAddr])._objectList.Add(__dataObject);

                                /*
                                if (__getObject._pageBreak)
                                {
                                    if (__loop != _objectDataList.Count - 1)
                                    {
                                        __findAndAddObject(((_pageListType)_pageList[__pageAddr])._objectList, _objectType.PageFooter, _pageHeight);
                                        if (_getTotalCurrentRow != null)
                                        {
                                            _getTotalCurrentRow(((_pageListType)_pageList[__pageAddr])._objectList, _objectList);
                                        }
                                    }
                                    __newPage = true;
                                }*/
                            }

                            // กรณีที่เป็น object data
                            /*
                            _columnDataListType __getDataColumn = (_columnDataListType)__object._columnList[__loop];
                            if (__getDataColumn._objectSource != null && __getDataColumn._isHide == false)
                            {
                                _columnListType __getColumn = (_columnListType)__getDataColumn._objectSource._columnList[__getDataColumn._columnAddr];

                                float __colWidthCalc = (__getDataColumn._totalColumnWidth != 0) ? (__getDataColumn._totalColumnWidth * objectWidth) * this._drawScale : (__getColumn._width - __getDataColumn._spaceBeforeText);

                                // toe fix searchwidth
                                //ArrayList __getString = MyLib._myUtil._cutString(this._paper._graphicSystem, __getDataColumn._text, __getColumn._fontData, (float)(__getColumn._width - __getDataColumn._spaceBeforeText), false);
                                ArrayList __getString = MyLib._myUtil._cutString(this._paper._graphicSystem, __getDataColumn._text, __getColumn._fontData, __colWidthCalc, false);

                                if (__getDataColumn._breakLine == true)
                                {
                                    string __getFirstString = __getString[0].ToString();
                                    __getString = new ArrayList();
                                    __getString.Add(__getFirstString);
                                }

                                int __sumHeight = 0;
                                for (int __line = 0; __line < __getString.Count; __line++)
                                {
                                    SizeF __stringSize = this._paper._graphicSystem.MeasureString(__getString[__line].ToString(), __getColumn._columnDetailFont);
                                    __sumHeight += (int)(__stringSize.Height * (this._paper._lineSpaceing / 100));
                                    if (__sumHeight > __height)
                                    {
                                        __height = __sumHeight;
                                    }
                                }

                            }*/

                        }
                        __newPage = false;
                        continue;
                    }

                    // ขั้นหน้าใหม่
                    if (__newPage == true)
                    {
                        __newPage = false;
                        __pageHeight = 0;
                        __pageNumber++;
                        this._processMessage = "Build Report Page : " + __pageNumber.ToString();
                        _paper._pageMax = __pageNumber;
                        _pageListType __newDataPage = new _pageListType();
                        float __headerHeight = __findAndAddObject(__newDataPage._objectList, _objectType.Header, __pageHeight);
                        __pageHeight += __headerHeight;
                        float __detailHeight = __findAndAddObject(__newDataPage._objectList, _objectType.Detail, __pageHeight);
                        __pageHeight += __detailHeight;

                        //__findAndAddObject(__newDataPage._objectList, _objectType.PageFooter, _pageHeight);
                        __newDataPage._paperPageSize = new Size(_pageWidth, _pageHeight);
                        if (__footerHeight > 0)
                        {
                            __newDataPage._haveFooterPage = true;
                        }

                        __pageAddr = _pageList.Add(__newDataPage);
                    }


                    // check page footer 
                    // toe footer page
                    /*
                    float __footerPageHeight = __findAndAddObject(__newDataPage._objectList, _objectType.PageFooter, __pageHeight);
                    __pageHeight += __footerPageHeight;

                    */

                    __getObject._isDataRow = true;
                    __getObject._position = new Point(__getObject._position.X, (int)__pageHeight);
                    _objectDataList[__loop] = (_objectListType)__getObject;
                    __pageHeight += (int)__getObject._size.Height;
                    ((_pageListType)_pageList[__pageAddr])._pageSize = new Size(_pageWidth, (int)__pageHeight);
                    ((_pageListType)_pageList[__pageAddr])._objectList.Add(__getObject);
                    if (__getObject._pageBreak)
                    {
                        if (__loop != _objectDataList.Count - 1)
                        {
                            __findAndAddObject(((_pageListType)_pageList[__pageAddr])._objectList, _objectType.PageFooter, _pageHeight);
                            if (_getTotalCurrentRow != null)
                            {
                                _getTotalCurrentRow(((_pageListType)_pageList[__pageAddr])._objectList, _objectList);
                            }
                        }
                        __newPage = true;
                    }
                }

                // footer page
                if (_objectDataList.Count > 0)
                {
                    __findAndAddObject(((_pageListType)_pageList[__pageAddr])._objectList, _objectType.PageFooter, _pageHeight);
                    if (_getTotalCurrentRow != null)
                    {
                        _getTotalCurrentRow(((_pageListType)_pageList[__pageAddr])._objectList, _objectList);
                    }
                }

            }
            this._buildReportSuccess = true;
            this._processMessage = "Success";
        }

        void _setToolStripPreview(bool isEanable)
        {
            for (int __row = 0; __row < _toolStripPreview.Items.Count; __row++)
            {
                ToolStripItem __item = _toolStripPreview.Items[__row];
                if (__item.GetType() == typeof(MyLib.ToolStripMyButton))
                {
                    if (__item.Name.Equals("_buttonExample"))
                    {

                    }

                    else if (__item.Name.Equals("_buttonClose"))
                    {

                    }
                    else
                    {
                        __item.Enabled = isEanable;
                    }
                }
                else
                {

                    __item.Enabled = isEanable;
                }
            }
        }
        /// <summary>
        /// สร้างรายงานทั้งหมด
        /// </summary>
        public void _buildReport(_reportType reportType)
        {
            this.reportConstType = reportType;
            this._paper._pageCurrent = 0;
            //this._paper._graphicSystem = this.CreateGraphics();
            this._paper._graphicSystem = this._printDocument.PrinterSettings.CreateMeasurementGraphics();
            this._paper._graphicSystem.SmoothingMode = SmoothingMode.HighQuality;
            this._paper._graphicSystem.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Boolean __loadDataSuccess = false;
            this._objectList.Clear();
            this._objectDataList.Clear();
            if (this._loadDataStream != null)
            {
                this._reportProgressBar.Style = ProgressBarStyle.Blocks;
                //this._buttonBuildReport.Enabled = false;
                //this._buttonCondition.Enabled = false;
                this._setToolStripPreview(false);
                this._toolStripPreview.Invalidate();
                this._loadDataStreamThread = new Thread(new ThreadStart(_loadDataStream));
                this._loadDataStreamThread.IsBackground = true;
                this._loadDataStreamThread.Start();
            }
            else
                if (_loadDataByThread != null)
            {
                this._loadDataByThreadSuccess = false;
                // this._buttonBuildReport.Enabled = false;
                // this._buttonCondition.Enabled = false;
                _setToolStripPreview(false);
                this._toolStripPreview.Invalidate();
                this._loadDataThread = new Thread(new ThreadStart(_loadDataByThread));
                this._loadDataThread.Start();
                this._countTime = 0;
                this._startCount = true;
            }
            else
            {
                if (_loadData != null)
                {
                    __loadDataSuccess = _loadData();
                }
                if (__loadDataSuccess == false)
                {
                    MessageBox.Show("Data Read fail.");
                }
                else
                {
                    // สั่งให้สร้างรายงาน
                    this._buildReportActive = true;
                }
            }
        }

        float __findAndAddObject(ArrayList source, _objectType type, float height)
        {
            float __heightResult = 0;
            for (int __loop = 0; __loop < _objectList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)this._objectList[__loop];
                if (__getObject._type == type)
                {
                    __getObject._position = new Point(__getObject._position.X, (int)(height + __heightResult));
                    _objectList[__loop] = (_objectListType)__getObject;
                    source.Add(__getObject);
                    __heightResult += __getObject._size.Height;
                }
            }
            return __heightResult;
        }

        public _cellListType _addCell(object objectSource, int columnAddr, bool autoPosition, float x, float y, _cellType type, string text, _cellAlign align, Font font)
        {
            _columnListType __column = (_columnListType)((_objectListType)objectSource)._columnList[columnAddr];
            _cellListType __result = new _cellListType();
            __result._type = type;
            __result._autoPosition = autoPosition;
            __result._position = new Point((int)x, (int)y);
            __result._text = text;
            __result._font = font;
            __result._alignCell = align;
            if (autoPosition)
            {
                // พิมพ์แบบเรียงบรรทัด
                if (x > __column._maxLine)
                {
                    __column._maxLine = (int)x;
                }
            }
            __column._cellList.Add(__result);
            ((_objectListType)objectSource)._columnList[columnAddr] = (_columnListType)__column;
            return __result;
        }

        public void _createEmtryColumn(_objectListType objectSource, _objectListType objectTarget)
        {
            for (int __loop = 0; __loop < objectSource._columnList.Count; __loop++)
            {
                _columnDataListType __newColumnData = new _columnDataListType();
                objectTarget._columnList.Add(__newColumnData);
            }
        }

        public void _addDataColumn(_objectListType _objectMaster, _objectListType objectSource, int columnAddr, string text, Font font, _cellAlign align, float spaceBeforeText, _cellType datatype)
        {
            _addDataColumn(_objectMaster, objectSource, columnAddr, text, font, align, spaceBeforeText, _columnBorder.None, datatype);
        }

        public void _addDataColumn(_objectListType _objectMaster, _objectListType objectSource, int columnAddr, string text, Font font, _cellAlign align, float spaceBeforeText, _columnBorder border, _cellType datatype)
        {
            _addDataColumn(_objectMaster, objectSource, columnAddr, text, font, align, spaceBeforeText, border, datatype, Color.Black);
        }

        public void _addDataColumn(_objectListType _objectMaster, _objectListType objectSource, int columnAddr, string text, Font font, _cellAlign align, float spaceBeforeText, _columnBorder border, _cellType datatype, Color fontColor)
        {
            _addDataColumn(_objectMaster, objectSource, columnAddr, text, font, align, spaceBeforeText, border, datatype, fontColor, false);
        }

        public void _addDataColumn(_objectListType _objectMaster, _objectListType objectSource, int columnAddr, string text, Font font, _cellAlign align, float spaceBeforeText, _columnBorder border, _cellType datatype, Color fontColor, bool breakLine)
        {
            _addDataColumn(_objectMaster, objectSource, columnAddr, text, font, align, spaceBeforeText, border, datatype, fontColor, breakLine, false);
        }

        public void _addDataColumn(_objectListType _objectMaster, _objectListType objectSource, int columnAddr, string text, Font font, _cellAlign align, float spaceBeforeText, _columnBorder border, _cellType datatype, Color fontColor, bool breakLine, bool allowAsciiNewLine)
        {
            _addDataColumn(_objectMaster, objectSource, columnAddr, text, font, align, spaceBeforeText, border, datatype, fontColor, breakLine, false, false);
        }
        public void _addDataColumn(_objectListType _objectMaster, _objectListType objectSource, int columnAddr, string text, Font font, _cellAlign align, float spaceBeforeText, _columnBorder border, _cellType datatype, Color fontColor, bool breakLine, bool allowAsciiNewLine, Boolean isHide)
        {
            if (datatype == _cellType.DateTime)
            {
                try
                {
                    text = (text.Trim().Length == 0) ? "" : MyLib._myGlobal._convertDateToString(MyLib._myGlobal._convertDateFromQuery(text), false);
                }
                catch
                {
                }
            }
            _columnDataListType __newColumnData = new _columnDataListType();
            __newColumnData._objectSource = _objectMaster;
            __newColumnData._text = text;
            __newColumnData._columnAddr = columnAddr;
            __newColumnData._alignCell = align;
            __newColumnData._spaceBeforeText = spaceBeforeText;
            __newColumnData._font = font;
            __newColumnData._fontColor = fontColor;
            __newColumnData._border = border;
            __newColumnData._dataType = datatype;
            __newColumnData._breakLine = breakLine;
            __newColumnData._isHide = isHide;
            objectSource._columnList[columnAddr] = (_columnDataListType)__newColumnData;
        }

        public int _findColumn(_objectListType objectSource, string columnName)
        {
            int __result = -1;
            for (int __loop = 0; __loop < objectSource._columnList.Count; __loop++)
            {
                if (((_columnListType)objectSource._columnList[__loop])._columnName.Equals(columnName))
                {
                    return __loop;
                }
            }
            return __result;
        }

        public int _addColumn(object objectSource, float columnWidth)
        {
            _columnListType __result = new _columnListType();
            __result._columnWidth = columnWidth;
            if (objectSource.GetType() == typeof(_objectListType))
            {
                return (((_objectListType)objectSource)._columnList.Add(__result));
            }
            else
                if (objectSource.GetType() == typeof(_columnListType))
            {
                return (((_columnListType)objectSource)._columnList.Add(__result));
            }
            return -1;
        }

        public int _addColumn(_objectListType objectSource, float columnWidth, bool autoPosition, _columnBorder border, _columnBorder cellBorder, string columnName, string resourceName, string fieldName, _cellAlign align)
        {
            return _addColumn(objectSource, columnWidth, autoPosition, border, cellBorder, columnName, resourceName, fieldName, align, true);
        }

        public int _addColumn(_objectListType objectSource, float columnWidth, bool autoPosition, _columnBorder border, _columnBorder cellBorder, string columnName, string resourceName, string fieldName, _cellAlign align, Boolean showColumnName)
        {
            return _addColumn(objectSource, columnWidth, autoPosition, border, cellBorder, columnName, resourceName, fieldName, align, showColumnName, _fontStandard);
        }

        public int _addColumn(_objectListType objectSource, float columnWidth, bool autoPosition, _columnBorder border, _columnBorder cellBorder, string columnName, string resourceName, string fieldName, _cellAlign align, Boolean showColumnName, Font cellFont)
        {
            return _addColumn(objectSource, columnWidth, autoPosition, border, cellBorder, columnName, resourceName, fieldName, align, showColumnName, cellFont, Color.Black);
        }

        public int _addColumn(_objectListType objectSource, float columnWidth, bool autoPosition, _columnBorder border, _columnBorder cellBorder, string columnName, string resourceName, string fieldName, _cellAlign align, Boolean showColumnName, Font cellFont, Color cellFontColor)
        {
            return _addColumn(objectSource, columnWidth, autoPosition, border, cellBorder, columnName, resourceName, fieldName, align, showColumnName, cellFont, Color.Black, 1);
        }

        public int _addColumn(_objectListType objectSource, float columnWidth, bool autoPosition, _columnBorder border, _columnBorder cellBorder, string columnName, string resourceName, string fieldName, _cellAlign align, Boolean showColumnName, Font cellFont, Color cellFontColor, int columnCount)
        {
            if (resourceName != null && resourceName.IndexOf('*') == (resourceName.Length - 1))
            {
                resourceName = resourceName.Replace("*", "");
                align = _cellAlign.Right;
            }
            else if (resourceName != null && resourceName.IndexOf('*') == 0)
            {
                resourceName = resourceName.Replace("*", "");
                align = _cellAlign.Center;
            }
            int __addr = _addColumn(objectSource, columnWidth); ;
            if (__addr != -1)
            {
                _columnListType __result = (_columnListType)objectSource._columnList[__addr];
                __result._autoPosition = autoPosition;
                __result._columnName = columnName;
                __result._resourceName = resourceName;
                __result._fieldName = fieldName;
                __result._border = border;
                __result._cellBorder = cellBorder;
                if (this._columnResource != null)
                {
                    ColumnResourceStruct __get = this._columnResource(resourceName);
                    resourceName = __get._resourceName;
                    __result._text = resourceName;
                    if (__get._findResource)
                    {
                        __result._text = (resourceName == null || resourceName.Length == 0) ? "" : ((MyLib._myResourceType)MyLib._myResource._findResource(resourceName, resourceName, false))._str;
                    }
                }
                else
                {
                    __result._text = (resourceName == null || resourceName.Length == 0) ? "" : ((MyLib._myResourceType)MyLib._myResource._findResource(resourceName, resourceName))._str;
                }
                __result._columnWidth = columnWidth;
                __result._fontData = cellFont;
                __result._columnDetailFont = cellFont;
                __result._columnHeadFont = cellFont;
                __result._columnDetailColor = cellFontColor;
                __result._alignCell = align;
                __result._showColumnName = showColumnName;
                __result._columnCount = columnCount;

                objectSource._columnList[__addr] = (_columnListType)__result;
            }
            return __addr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectList">คือ _objectDataList ใน _view</param>
        /// <param name="_type"></param>
        /// <param name="autoSize"></param>
        /// <param name="leftMargin"></param>
        /// <param name="leftMarginByPersent"></param>
        /// <param name="border"></param>
        /// <returns></returns>
        public _objectListType _addObject(ArrayList objectList, _objectType _type, bool autoSize, int leftMargin, bool leftMarginByPersent, SMLReport._report._columnBorder border)
        {
            return _addObject(objectList, _type, autoSize, leftMargin, leftMarginByPersent, border, true);
        }

        public _objectListType _addObject(ArrayList objectList, _objectType _type, bool autoSize, int leftMargin, bool leftMarginByPersent, SMLReport._report._columnBorder border, Boolean show)
        {
            _objectListType __result = new _objectListType();
            __result._type = _type;
            __result._autoSize = true;
            __result._position = new Point(0, 0);
            __result._leftMargin = leftMargin;
            __result._leftMarginByPersent = leftMarginByPersent;
            __result._border = border;
            __result._show = show;
            objectList.Add(__result);
            return __result;
        }

        void _hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this._paper.Location = new Point(this._rulerTopControl.Height + (-this._hScrollBar.Value), this._paper.Location.Y);
            this._paper.Invalidate();
            this._ruleRefresh();
        }

        void _vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this._paper.Location = new Point(this._paper.Location.X, this._rulerLeftControl.Width + (-this._vScrollBar.Value));
            this._paper.Invalidate();
            this._ruleRefresh();
        }

        void _calcDrawAreaSize()
        {
            this._paper.Size = new Size(this._panelView.Width - this._rulerLeftControl.Width, this._panelView.Height - this._rulerTopControl.Height);
            Size __getPaperSizeByPixel = (this._pageSetupDialog.PageSettings.Landscape) ? new Size(this._pageSetupDialog.PageSettings.PaperSize.Height, this._pageSetupDialog.PageSettings.PaperSize.Width) : new Size(this._pageSetupDialog.PageSettings.PaperSize.Width, this._pageSetupDialog.PageSettings.PaperSize.Height);
            int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScale);
            int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScale);
            int _newWidth = __calcWidth + (this._paper._topLeftPaper.X * 2);
            int _newHeigth = __calcHeight + (this._paper._topLeftPaper.Y * 2);
            //
            if (_newWidth < this._panelView.Width - this._rulerLeftControl.Width)
            {
                _newWidth = this._panelView.Width - this._rulerLeftControl.Width;
            }
            if (_newHeigth < this._panelView.Height - this._rulerTopControl.Height)
            {
                _newHeigth = this._panelView.Height - this._rulerTopControl.Height;
            }
            this._paper.Size = new Size(_newWidth, _newHeigth);
            //
            _vScrollBar.SuspendLayout();
            _hScrollBar.SuspendLayout();
            if (this._paper.Height > this._panelView.Height - this._paper._topLeftPaper.X)
            {
                _vScrollBar.Visible = true;
                _vScrollBar.Location = new Point(this._panelView.Width - _vScrollBar.Width, 0);
                _vScrollBar.Height = this._panelView.Height;
                _vScrollBar.Maximum = (this._paper.Height - this._panelView.Height) + (this._paper._topLeftPaper.X * 2);
            }
            else
            {
                _vScrollBar.Visible = false;
            }
            if (this._paper.Width > this._panelView.Width - this._paper._topLeftPaper.Y)
            {
                _hScrollBar.Visible = true;
                _hScrollBar.Location = new Point(0, this._panelView.Height - _hScrollBar.Height);
                _hScrollBar.Width = this._panelView.Width - ((_vScrollBar.Visible) ? _vScrollBar.Width : 0);
                _vScrollBar.Height = this._panelView.Height - _hScrollBar.Height;
                _hScrollBar.Maximum = (this._paper.Width - this._panelView.Width) + (this._paper._topLeftPaper.Y * 2);
            }
            else
            {
                _hScrollBar.Visible = false;
            }
            _vScrollBar.ResumeLayout(false);
            _hScrollBar.ResumeLayout(false);
            //
            this._panelView.Invalidate();
            this._paper.Invalidate();
            this.Invalidate();
            _ruleRefresh();
        }

        void _ruleRefresh()
        {
            _rulerTopControl._unit = SMLReport._design._measurementUnitType.Centimeters;
            _rulerTopControl._ruleScale = _drawScale;
            _rulerTopControl.Location = new Point(this._rulerLeftControl.Width, 0);
            _rulerTopControl.Width = this._panelView.Width - this._rulerLeftControl.Width;
            _rulerTopControl._beginValue = _design._pageSetup._convertPixelToUnit(_rulerTopControl._unit, _paper._topLeftPaper.X - ((_hScrollBar.Visible) ? _hScrollBar.Value : 0), _drawScale);
            _rulerTopControl.Invalidate();
            //
            _rulerLeftControl._unit = SMLReport._design._measurementUnitType.Centimeters;
            _rulerLeftControl._ruleScale = _drawScale;
            _rulerLeftControl.Location = new Point(0, this._rulerTopControl.Height);
            _rulerLeftControl.Height = this._panelView.Height - this._rulerTopControl.Height;
            _rulerLeftControl._beginValue = _design._pageSetup._convertPixelToUnit(_rulerLeftControl._unit, _paper._topLeftPaper.Y - ((_vScrollBar.Visible) ? _vScrollBar.Value : 0), _drawScale);
            _rulerLeftControl.Invalidate();
        }

        private void _myReportView_Load(object sender, EventArgs e)
        {

        }

        private void _buttonPageSetup_Click(object sender, EventArgs e)
        {
        }

        private void _buttonBuildReport_Click(object sender, EventArgs e)
        {
        }

        private void _buttonPageFirst_Click(object sender, EventArgs e)
        {
            _paper._pageCurrent = 0;
            _pageToolBarProcess();
            _paper.Invalidate();
        }

        private void _buttonPageLast_Click(object sender, EventArgs e)
        {
            _paper._pageCurrent = _paper._pageMax - 1;
            _pageToolBarProcess();
            _paper.Invalidate();
        }

        private void _buttonPagePrev_Click(object sender, EventArgs e)
        {
            if (_paper._pageCurrent > 0)
            {
                _paper._pageCurrent--;
                _pageToolBarProcess();
                _paper.Invalidate();
            }
        }

        private void _buttonPageNext_Click(object sender, EventArgs e)
        {
            if (_paper._pageCurrent < _paper._pageMax - 1)
            {
                _paper._pageCurrent++;
                _pageToolBarProcess();
                _paper.Invalidate();
            }
        }

        void _pageToolBarProcess()
        {
            _textBoxPageNumber.Text = _paper.PageCurrent.ToString();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this._printDialog.PrinterSettings = this._printDocument.PrinterSettings;
                this._printDialog.PrintToFile = false;
                ((Form)this._printPreviewDialog).WindowState = FormWindowState.Maximized;
                this._printPreviewDialog.PrintPreviewControl.Columns = (this._paper._pageMax > 1 && this._pageSetupDialog.PageSettings.Landscape == false) ? 2 : 1;
                this._printPreviewDialog.ShowDialog();
            }
            catch
            {
            }
        }

        private void _buttonPrintSetup_Click(object sender, EventArgs e)
        {
            if (this._pageSetupDialog.ShowDialog() == DialogResult.Cancel)
            {
                this._pageSetupDialog.PageSettings.PaperSize = this._printDocument.PrinterSettings.DefaultPageSettings.PaperSize;
                this._pageSetupDialog.PageSettings.Landscape = this._pageSetupDialog.PageSettings.Landscape;
            }
            this._buildReport(_reportType.Standard);
            this._calcDrawAreaSize();
            this._ruleRefresh();
            this._paper.Location = new Point(this._rulerLeftControl.Width, this._rulerTopControl.Height);
            _calcPageSize();
            this._paper.Invalidate();
        }

        private void _buttonPrint_Click(object sender, EventArgs e)
        {
            this._printDialog.PrinterSettings = this._printDocument.PrinterSettings;
            this._printDialog.PrintToFile = false;
            this._printDialog.AllowSelection = false;
            this._printDialog.AllowCurrentPage = true;
            if (this._printDialog.ShowDialog(MyLib._myGlobal._mainForm) == DialogResult.OK)
            {
                this._printDocument.PrinterSettings = this._printDialog.PrinterSettings;
                this._printDocument.Print();
            }
        }

        //somruk
        private string _getStyleExcel(_horizontalType _horizontalTypeexcel, string font, float fontsize, string numberformat, bool isbold)
        {
            return _reportexcel._styleAdd(_horizontalTypeexcel, _verticalType.Center, true, 1, 1, 1, 1, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, "", true, font, fontsize, isbold);
        }

        private _horizontalType __ObjectStyle(_cellAlign __getColumn)
        {
            if (__getColumn == _cellAlign.Center) return _horizontalType.Center;
            if (__getColumn == _cellAlign.Default) return _horizontalType.Left;
            if (__getColumn == _cellAlign.Left) return _horizontalType.Left;
            if (__getColumn == _cellAlign.Right) return _horizontalType.Right;
            return _horizontalType.Center;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=Open,1=Save</param>
        void _excel(int mode)
        {

            string __numberFormat = @"0;-0;;@";
            String __excelFileNameTitle = __excelFlieName;
            if (__excelFlieName.IndexOf(' ') != -1)
            {
                __excelFileNameTitle = __excelFlieName.Substring(0, __excelFlieName.IndexOf(' '));
            }
            __excelFileNameTitle = __excelFileNameTitle;
            _reportexcel = new _excelClass(_maxcol, _fontStandard.Name, _fontStandard.Size);
            _reportexcel._pageSetup._orientation = OrientationType.Landscape;
            _reportexcel._pageSetup._pageMarginBottom = 0.35f;
            _reportexcel._pageSetup._pageMarginLeft = 0.35f;
            _reportexcel._pageSetup._pageMarginRight = 0.35f;
            _reportexcel._pageSetup._pageMarginTop = 0.35f;
            _reportexcel._pageSetup._headerMargin = 0.5f;
            _reportexcel._pageSetup._sheetName = __excelFileNameTitle;
            //  _reportexcel._pageSetup._printTitle = "=Report!R1:R4";
            ArrayList __yList = new ArrayList();
            string __styleheaderGrid = _getStyleExcel(_horizontalType.Center, _fontStandard.Name, _fontStandard.Size, "", _fontStandard.Bold);// _reportexcel._styleAdd(_horizontalType.Center, _verticalType.Center, true, 1, 1, 1, 1, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, "", true, _font, 8.0f, true);                       
            for (int __loop = 0; __loop < this._objectList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)this._objectList[__loop];
                if (__getObject._type == _objectType.Header)
                {
                    bool __newline = true;
                    for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                    {
                        _columnListType __getColumn = (_columnListType)__getObject._columnList[__column];
                        for (int __cell = 0; __cell < __getColumn._cellList.Count; __cell++)
                        {

                            _cellListType __getCell = (_cellListType)__getColumn._cellList[__cell];
                            _horizontalType _horizontalTypExcel = _horizontalType.Center;
                            if (__yList.Count == 0)
                            {
                                __yList.Add(__getCell._position.Y.ToString() + __getCell._position.X.ToString());
                                __newline = true;
                            }
                            else
                            {
                                if (!__yList.Contains(__getCell._position.Y.ToString() + __getCell._position.X.ToString()))
                                {
                                    __yList.Add(__getCell._position.Y.ToString() + __getCell._position.X.ToString());
                                    __newline = true;
                                }
                                else
                                {
                                    __newline = false;
                                }
                            }
                            _horizontalTypExcel = __ObjectStyle(__getCell._alignCell);
                            Font __newfont;
                            if (__getCell._font == null)
                            {
                                __newfont = _fontStandard;
                            }
                            else
                            {
                                __newfont = new Font(((Font)__getCell._font).Name, ((Font)__getCell._font).Size);
                            }
                            string __xstyle = _getStyleExcel(_horizontalTypExcel, __newfont.Name, __newfont.Size, "", __newfont.Bold);
                            if (__newline && __getCell._text.IndexOf("Page") < 0 && __getCell._text.IndexOf("หน้า") < 0)
                            {
                                _reportexcel._addRow();
                                try
                                {
                                    _reportexcel._cellValue(_reportexcel._currentRow, __cell, _paper._processValue(__getCell._text), __xstyle, _maxcol, SMLReport._report._cellType.String);
                                    _columnClass __excelcol = (_columnClass)((ArrayList)_reportexcel._columnData)[__cell];
                                    __excelcol._hide = false;
                                    __excelcol._width = __getCell._width;
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                if (__getObject._type == _objectType.Detail)
                {

                    bool _isfound = false;
                    for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                    {
                        _columnListType __getColumn = (_columnListType)__getObject._columnList[__column];

                        if (_isfound == false && __getColumn._showColumnName)
                        {
                            _isfound = true;
                            _reportexcel._addRow();
                        }
                        _horizontalType _horizontalTypExcel = _horizontalType.Center;
                        _horizontalTypExcel = __ObjectStyle(__getColumn._alignCell);
                        Font __newfont;
                        if (__getColumn._fontData == null)
                        {
                            __newfont = _fontStandard;
                        }
                        else
                        {
                            __newfont = new Font(((Font)__getColumn._fontData).Name, ((Font)__getColumn._fontData).Size);
                        }

                        string __xstyle = _getStyleExcel(_horizontalTypExcel, __newfont.Name, __newfont.Size, "", __newfont.Bold);
                        if (__getColumn._showColumnName)
                        {
                            // toe merge
                            if (__getColumn._columnCount > 1)
                            {
                                _reportexcel._cellValue(_reportexcel._currentRow, __column, __getColumn._text, __xstyle, __getColumn._columnCount, _cellType.String);
                                // _reportexcel._cellColumnCount(_reportexcel._currentRow, __column, __getColumn._columnCount); 
                            }
                            else
                            {
                                _reportexcel._cellValue(_reportexcel._currentRow, __column, __getColumn._text, __xstyle);
                            }
                            //   _columnClass __excelcol = (_columnClass)((ArrayList)_reportexcel._columnData)[__column];
                            //  __excelcol._hide = false;
                            // __excelcol._width = __getColumn._columnWidth;
                        }
                    }
                }
            }
            for (int __loop = 0; __loop < this._objectDataList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)this._objectDataList[__loop];
                _reportexcel._addRow();
                for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                {
                    _columnDataListType __getColumn = (_columnDataListType)__getObject._columnList[__column];
                    string __format = __getColumn._text;
                    _horizontalType _horizontalTypExcel = _horizontalType.Center;
                    SMLReport._report._cellType __numberformat = SMLReport._report._cellType.String;
                    _cellAlign __celAlign = __getColumn._alignCell;
                    if (__getColumn._dataType == _cellType.Number)
                    {
                        // toe check mix data type
                        decimal n;
                        bool isNumeric = decimal.TryParse(__getColumn._text, out n);

                        if (isNumeric)
                        {
                            __numberformat = SMLReport._report._cellType.Number;
                            _horizontalTypExcel = _horizontalType.Right;
                        }
                        else
                        {
                            _horizontalTypExcel = __ObjectStyle(__getColumn._alignCell);
                        }
                    }
                    else
                    {
                        _horizontalTypExcel = __ObjectStyle(__getColumn._alignCell);
                    }
                    Font __newfont;
                    if (__getColumn._font == null)
                    {
                        __newfont = _fontStandard;
                    }
                    else
                    {
                        __newfont = new Font(((Font)__getColumn._font).Name, ((Font)__getColumn._font).Size);
                    }
                    string __xstlye = _getStyleExcel(_horizontalTypExcel, __newfont.Name, __newfont.Size, __numberFormat, false);
                    if (__getColumn._objectSource != null)
                    {

                        _reportexcel._cellValue(_reportexcel._currentRow, __column, __getColumn._text, __xstlye, __numberformat);
                        if (((_columnListType)__getColumn._objectSource._columnList[__getColumn._columnAddr])._showColumnName)
                        {
                            _columnClass __excelcol = (_columnClass)((ArrayList)_reportexcel._columnData)[__column];
                            __excelcol._hide = false;
                            __excelcol._width = ((_columnListType)__getColumn._objectSource._columnList[__getColumn._columnAddr])._width;
                        }
                    }
                }
            }

            /*
            for (int __loop = 0; __loop < this._objectList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)this._objectList[__loop];
                if (__getObject._type == _objectType.PageFooter)
                {

                    _reportexcel._addRow();


                    bool __newline = true;
                    for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                    {
                        _columnListType __getColumn = (_columnListType)__getObject._columnList[__column];
                        for (int __cell = 0; __cell < __getColumn._cellList.Count; __cell++)
                        {
                            //if (__cell == __footerRow)
                            {
                                _cellListType __getCell = (_cellListType)__getColumn._cellList[__cell];
                                _horizontalType _horizontalTypExcel = _horizontalType.Center;
                                if (__yList.Count == 0)
                                {
                                    __yList.Add(__getCell._position.Y.ToString() + __getCell._position.X.ToString());
                                    __newline = true;
                                }
                                else
                                {
                                    if (!__yList.Contains(__getCell._position.Y.ToString() + __getCell._position.X.ToString()))
                                    {
                                        __yList.Add(__getCell._position.Y.ToString() + __getCell._position.X.ToString());
                                        __newline = true;
                                    }
                                    else
                                    {
                                        __newline = false;
                                    }
                                }
                                _horizontalTypExcel = __ObjectStyle(__getCell._alignCell);
                                Font __newfont;
                                if (__getCell._font == null)
                                {
                                    __newfont = _fontStandard;
                                }
                                else
                                {
                                    __newfont = new Font(((Font)__getCell._font).Name, ((Font)__getCell._font).Size);
                                }
                                string __xstyle = _getStyleExcel(_horizontalTypExcel, __newfont.Name, __newfont.Size, "", __newfont.Bold);
                                // if (__newline && __getCell._text.IndexOf("Page") < 0 && __getCell._text.IndexOf("หน้า") < 0)
                                {
                                    //_reportexcel._addRow();
                                    try
                                    {
                                        _reportexcel._cellValue(_reportexcel._currentRow, __column, _paper._processValue(__getCell._text), __xstyle, 0, SMLReport._report._cellType.String);
                                        _columnClass __excelcol = (_columnClass)((ArrayList)_reportexcel._columnData)[__cell];
                                        __excelcol._hide = false;
                                        __excelcol._width = __getCell._width;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }*/

            try
            {
                if (mode == 0)
                {
                    string __FileNameTemp = _reportexcel._createExcelFile(__excelFileNameTitle, true);
                    try
                    {
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.EnableRaisingEvents = false;
                        proc.StartInfo.FileName = "excel";
                        proc.StartInfo.Arguments = __FileNameTemp;
                        proc.Start();
                        //proc.WaitForExit();
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show("Fail Open Excel : " + __ex.Message);
                    }
                }
                else
                {
                    SaveFileDialog __fileDialog = new SaveFileDialog();
                    __fileDialog.Filter = "xml|*.xml";
                    __fileDialog.Title = "Save XML File";
                    __fileDialog.ShowDialog();
                    if (__fileDialog.FileName.Length > 0)
                    {
                        string __FileNameTemp = _reportexcel._createExcelFile(__fileDialog.FileName, false);
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message);
            }
            /*            Excel.Application _excelApp = null;
                        //
                        _excelApp = new Excel.ApplicationClass();
                        _excelApp.Application.Workbooks.Add(true);
                        // 
                        for (int __loop = 0; __loop < this._objectList.Count; __loop++)
                        {
                            _objectListType __getObject = (_objectListType)this._objectList[__loop];
                            if (__getObject._type == _objectType.Detail)
                            {
                                for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                                {
                                    _columnListType __getColumn = (_columnListType)__getObject._columnList[__column];
                                    ((Excel.Range)_excelApp.Cells[1, __column + 1]).EntireColumn.ColumnWidth = __getColumn._width / 5;
                                    ((Excel.Range)_excelApp.Cells[1, __column + 1]).EntireColumn.NumberFormat = "@";
                                    _excelApp.Cells[1, __column + 1] = __getColumn._resourceName;
                                }
                            }
                        }
                        //
                        for (int __loop = 0; __loop < this._objectDataList.Count; __loop++)
                        {
                            _objectListType __getObject = (_objectListType)this._objectDataList[__loop];
                            for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                            {
                                _columnDataListType __getColumn = (_columnDataListType)__getObject._columnList[__column];
                                _excelApp.Cells[2 + __loop, __column + 1] = __getColumn._text;
                            }
                        }
                        _excelApp.Visible = true;*/
        }

        private void _buttonOpenExcel_Click(object sender, EventArgs e)
        {
            this._excel(0);
        }

        private void _timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this._reportProgressBar.Style == ProgressBarStyle.Blocks)
                {
                    this._reportProgressBar.Maximum = 100;
                    this._reportProgressBar.Value = this._progessBarValue;
                    this._reportProgressBar.Invalidate();
                }
                if (this._buildReportActive || this._loadDataByThreadSuccess)
                {
                    // สั่งให้ Thread สร้างรายงานทำงาน
                    this._startCount = false;
                    this._buildReportActive = false;
                    this._loadDataByThreadSuccess = false;
                    this._reportProgressBar.Style = ProgressBarStyle.Marquee;
                    this._buildReportThread = new Thread(new ThreadStart(_buildReportNow));
                    this._buildReportThread.Start();
                }
                if (this._buildReportSuccess)
                {
                    this._reportProgressBar.Style = ProgressBarStyle.Blocks;
                    this._progessBarValue = 100;
                    this._buildReportSuccess = false;
                    this._textBoxPageNumber.Text = _paper.PageCurrent.ToString();
                    this._paper.Invalidate();
                    //this._buttonBuildReport.Enabled = true;
                    // this._buttonCondition.Enabled = true;
                    _setToolStripPreview(true);
                    if (this._buildSuccess != null)
                    {
                        this._buildSuccess();
                    }
                }
                if (this._startCount)
                {
                    this._countTime += 10;
                    int __count = (int)this._countTime / 100;
                    this._processMessage = "Query and Retrieve Data (" + __count.ToString() + ")";
                }
                this._reportStatus.Text = this._processMessage;
                this._reportStatus.Invalidate();
            }
            catch
            {
            }
        }

        private void _saveToExcelButton_Click(object sender, EventArgs e)
        {
            this._excel(1);
        }

        private void _csvExportButton_Click(object sender, EventArgs e)
        {
            _csvClass __exportCSV = new _csvClass();

            for (int __loop = 0; __loop < this._objectList.Count; __loop++)
            {
                _objectListType __getObject = (_objectListType)this._objectList[__loop];
                if (__getObject._type == _objectType.Header) // หัว่รายงาน
                {
                    bool __newline = true;
                    for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                    {
                        _columnListType __getColumn = (_columnListType)__getObject._columnList[__column];
                        for (int __cell = 0; __cell < __getColumn._cellList.Count; __cell++)
                        {
                            _cellListType __getCell = (_cellListType)__getColumn._cellList[__cell];


                            if (__newline && __getCell._text.IndexOf("Page") < 0 && __getCell._text.IndexOf("หน้า") < 0)
                            {
                                __exportCSV._addNewLine();
                                __exportCSV._addCell(_paper._processValue(__getCell._text), __getCell._type);
                            }
                            // __line.Add(__getCell._text);
                        }
                    }
                }
                if (__getObject._type == _objectType.Detail) // หัวคอลัมน์
                {
                    bool _isfound = false;
                    for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                    {
                        _columnListType __getColumn = (_columnListType)__getObject._columnList[__column];
                        if (_isfound == false && __getColumn._showColumnName)
                        {
                            _isfound = true;
                            __exportCSV._addNewLine();
                        }
                        __exportCSV._addCell(__getColumn._text);

                        //__line.Add(__getColumn._text);
                    }
                }

                //__csvDocs.AppendLine(string.Join(",", __line.ToArray()));
            }
            __exportCSV._addNewLine();
            // field ข้อมูล
            for (int __loop = 0; __loop < this._objectDataList.Count; __loop++)
            {
                //List<string> __line = new List<string>();

                _objectListType __getObject = (_objectListType)this._objectDataList[__loop];
                for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                {
                    _columnDataListType __getColumn = (_columnDataListType)__getObject._columnList[__column];
                    //__line.Add(__getColumn._text);
                    __exportCSV._addCell(__getColumn._text, __getColumn._dataType);
                }
                __exportCSV._addNewLine();
                //__csvDocs.AppendLine(string.Join(",", __line.ToArray()));

            }

            //string __csvStr = __csvDocs.ToString();

            SaveFileDialog __saveCSV = new SaveFileDialog();
            __saveCSV.Filter = "CSV Files (*.csv)|*.csv";
            __saveCSV.Title = "Save SCV File";
            if (__saveCSV.ShowDialog() == DialogResult.OK)
            {
                if (__saveCSV.FileName.Length > 0)
                {
                    __exportCSV._exportCSVFile(__saveCSV.FileName);
                }
            }
        }
    }
    //
    public delegate bool GetObjectEventHandler(ArrayList objectList, _objectType type);
    public delegate bool LoadDataEventHandler();
    public delegate void GetDataObjectEventHandler();
    public delegate void LoadDataStreamEventHandle();
    public delegate void LoadDataByThreadEventHandle();
    public delegate ColumnResourceStruct ColumnResourceEventHandler(string resourceName);
    public delegate void BuildSuccessEventHandle();

    public delegate void GetTotalCurrentRow(ArrayList source, ArrayList objectList);
    public delegate void beforeReportDrawPaper(_pageListType pageListType);
    //
    public class ColumnResourceStruct
    {
        public Boolean _findResource = true;
        public string _resourceName;
    }
}
