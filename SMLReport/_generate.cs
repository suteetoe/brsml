using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport
{
    public partial class _generate : UserControl
    {
        public delegate void ShowConditionEventHandler(string screenName);
        public delegate void InitEventHandler();
        public delegate void QueryEventHandler();
        public delegate void SumTotalEventHandler(_generateLevelClass level, int columnNumber, decimal value);
        public delegate DataRow[] DataRowSelectEventHandler(_generateLevelClass levelParent, _generateLevelClass level, DataRow source);
        public delegate void UpdateDecimalValueEventHandler(_generateLevelClass sender, _generateColumnStyle isTotal);
        public delegate void RenderValueEventHandler(_generateLevelClass sender, int columnNumber, _generateColumnStyle isTotal);
        public delegate Boolean ObjectPageBreakEventHandler(_generateColumnStyle isTotal);
        public delegate Font RenderFontEventHandler(DataRow data, _generateColumnListClass source, _generateLevelClass sender);
        public delegate void RenderHeaderEventHandler(_generate source);
        public delegate void RenderFooterEventHandler(_generate source);
        public delegate void RenderFooterPageEventHandler(_generate source);
        //
        public _generateLevelClass _level = new _generateLevelClass("", false);
        public List<_generateLevelClass> __grandTotalList = new List<_generateLevelClass>();
        public string _resourceTable = "";
        public string _conditionText = "";
        public string _conditionTextDetail = "";
        /// <summary>
        /// 0=ปรกติ,1=กรณีติดลบให้ใส่วงเล็บ
        /// </summary>
        public int _underZeroType = 0;
        //
        private string _reportName = "";
        public string _reportDescripton = "";
        //private DataTable _dataTable;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public event ObjectPageBreakEventHandler _objectPageBreak;
        public event UpdateDecimalValueEventHandler _updateDecimalValue;
        public event RenderValueEventHandler _renderValue;
        public event RenderFontEventHandler _renderFont;
        public event InitEventHandler _init;
        public event ShowConditionEventHandler _showCondition;
        public event QueryEventHandler _query;
        public event DataRowSelectEventHandler _dataRowSelect;
        public event SumTotalEventHandler _sumTotal;
        public event RenderHeaderEventHandler _renderHeader;

        public event RenderFooterEventHandler _renderFooter;
        public event RenderFooterEventHandler _renderFooterPage;

        public _generate(string reportName, Boolean landscape)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._reportName = reportName;
                this._viewControl._pageSetupDialog.PageSettings.Landscape = landscape;
                this._viewControl._buttonExample.Enabled = false;
                this._viewControl._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
                this._viewControl._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
                this._viewControl._buttonClose.Click += new EventHandler(_buttonClose_Click);
                this._viewControl._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_viewControl__loadDataByThread);
                this._viewControl._getObject += new SMLReport._report.GetObjectEventHandler(_viewControl__getObject);
                this._viewControl._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_viewControl__getDataObject);
                this._myFrameWork.__queryStreamEvent += new MyLib.QueryStreamEventHandler(_myFrameWork___queryStreamEvent);
            }
        }



        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._build();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            if (this._showCondition != null)
            {
                this._showCondition(this._reportName);
            }
        }

        void _printData(_generateLevelClass level, Font newFont, Boolean sumTotal, SMLReport._report._columnBorder columnBorder, _generateColumnStyle columnStyle)
        {
            SMLReport._report._objectListType __dataObject = _viewControl._addObject(_viewControl._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None); // เส้นบรรทัด
            // toe
            //__dataObject._show = level._isHide == true ? false : true;

            if (this._objectPageBreak != null)
            {
                if (this._objectPageBreak(columnStyle))
                {
                    __dataObject._pageBreak = true;
                }
            }

            // toe check hide level
            //if (level._isHide == false)
            {
                _viewControl._createEmtryColumn(level._objectList, __dataObject);
                for (int __column = 0; __column < level._columnList.Count; __column++)
                {
                    if (level._columnList[__column]._widthPercent > 0)
                    {
                        SMLReport._report._columnBorder __columnBorderNew = columnBorder;
                        // toe if border
                        if (columnStyle == _generateColumnStyle.Data)
                        {
                            __columnBorderNew = level._columnList[__column]._dataBorderStyle;
                        }

                        _generateColumnListClass __data = level._columnList[__column];
                        Font __newFont = (newFont == null) ? __data._font : newFont;
                        string __dataStr;
                        SMLReport._report._cellAlign __cellAlign;
                        switch (__data._dataType)
                        {
                            case SMLReport._report._cellType.Number:
                            case SMLReport._report._cellType.Formula:
                                __cellAlign = SMLReport._report._cellAlign.Right;
                                if (this._updateDecimalValue != null)
                                {
                                    this._updateDecimalValue(level, columnStyle);
                                }
                                decimal __value = level._columnList[__column]._dataNumber;
                                __dataStr = MyLib._myGlobal._formatNumberForReport(__data._point, __value);
                                if (this._renderValue != null)
                                {
                                    this._renderValue(level, __column, columnStyle);
                                    __value = level._columnList[__column]._dataNumber;

                                    if (this._underZeroType == 0)
                                    {
                                        __dataStr = MyLib._myGlobal._formatNumberForReport(__data._point, __value);
                                    }
                                    else
                                    {
                                        if (__value < 0M)
                                        {
                                            __dataStr = "(" + MyLib._myGlobal._formatNumberForReport(__data._point, __value * -1) + ")";
                                        }
                                    }

                                    if (level._columnList[__column]._dataStr.Length > 0)
                                    {
                                        __dataStr = level._columnList[__column]._dataStr;
                                    }

                                }
                                if (sumTotal)
                                {
                                    level._columnList[__column]._sumTotalColumn += __value;
                                    if (this._sumTotal != null)
                                    {
                                        this._sumTotal(level._levelParent, __column, __value);
                                    }
                                }
                                break;
                            default:
                                __cellAlign = (__data._align == _report._cellAlign.Default) ? SMLReport._report._cellAlign.Left : __data._align;
                                __dataStr = level._columnList[__column]._dataStr;
                                if (this._renderValue != null)
                                {
                                    this._renderValue(level, __column, columnStyle);
                                    __dataStr = level._columnList[__column]._dataStr;
                                }
                                if (__dataStr.Length == 0 && level._columnList[__column]._dataBorderStyle == _report._columnBorder.None) // toe for cell border
                                {
                                    __columnBorderNew = SMLReport._report._columnBorder.None;
                                }
                                break;
                        }
                        _viewControl._addDataColumn(level._objectList, __dataObject, __column, __dataStr, __newFont, __cellAlign, 0, __columnBorderNew, __data._dataType, Color.Black, false, false, (level._columnList[__column]._isHideColumn || level._isHide));
                    }
                }
            }
        }

        void _printGrandTotal(_generateLevelClass levelFormat, _generateLevelClass level, Font newFont)
        {
            for (int __column = 0; __column < levelFormat._columnList.Count; __column++)
            {
                _generateColumnListClass __data = levelFormat._columnList[__column];
                switch (__data._dataType)
                {
                    case SMLReport._report._cellType.Number:
                    case SMLReport._report._cellType.Formula:
                        levelFormat._columnList[__column]._dataNumber = level.__grandTotal[__column]._dataNumber;
                        break;
                    default:
                        levelFormat._columnList[__column]._dataStr = "";
                        break;
                }
            }
            this._printData(levelFormat, newFont, false, SMLReport._report._columnBorder.Bottom, _generateColumnStyle.GrandTotal);
        }
        /// <summary>
        /// ประมวลผล ข้อมุล จาก database
        /// </summary>
        /// <param name="levelParent"></param>
        /// <param name="level"></param>
        /// <param name="source"></param>
        private void _getDataObjectLevel(_generateLevelClass levelParent, _generateLevelClass level, DataRow source)
        {
            Font __newFont_bold = new Font(_viewControl._fontStandard, FontStyle.Bold);
            DataRow[] __selectData = (this._dataRowSelect == null) ? level._dataTable.Select() : this._dataRowSelect(levelParent, level, source);
            if (__selectData != null)
            {
                for (int __row = 0; __row < __selectData.Length; __row++)
                {
                    // ดึงข้อมูล
                    for (int __column = 0; __column < level._columnList.Count; __column++)
                    {
                        level._columnList[__column]._dataStr = "";
                        level._columnList[__column]._dataNumber = 0M;
                        switch (level._columnList[__column]._dataType)
                        {
                            case SMLReport._report._cellType.Number:
                            case SMLReport._report._cellType.Formula:
                                if (__selectData[__row].IsNull(level._columnList[__column]._fieldName))
                                {
                                    level._columnList[__column]._dataNumber = 0;
                                }
                                else
                                {
                                    level._columnList[__column]._dataNumber = MyLib._myGlobal._decimalPhase(__selectData[__row][level._columnList[__column]._fieldName].ToString());
                                }
                                break;
                            default:
                                if (level._columnList[__column]._fieldName.Length == 0)
                                {
                                    level._columnList[__column]._dataStr = "";
                                }
                                else
                                {
                                    try
                                    {
                                        level._columnList[__column]._dataStr = __selectData[__row][level._columnList[__column]._fieldName].ToString();
                                    }
                                    catch
                                    {
                                        level._columnList[__column]._dataStr = "";
                                    }
                                }
                                break;
                        }
                        if (this._renderFont != null)
                        {
                            Font __newFont = level._columnList[__column]._font;
                            level._columnList[__column]._font = this._renderFont(__selectData[__row], level._columnList[__column], level);
                        }
                    }
                    // พิมพ์
                    this._printData(level, null, true, SMLReport._report._columnBorder.None, _generateColumnStyle.Data); // toe al;
                    // ดึงรายการ level ต่อไป
                    if (level._levelNext != null)
                    {
                        this._getDataObjectLevel(level, level._levelNext, __selectData[__row]);
                    }                   
                }
                // รวม
                if (level._sumTotal)
                {
                    // ข้อมูล
                    for (int __column = 0; __column < level._columnList.Count; __column++)
                    {
                        level._columnList[__column]._dataStr = "";
                        level._columnList[__column]._dataNumber = (level._columnList[__column]._isTotal) ? level._columnList[__column]._sumTotalColumn : 0M;
                    }
                    this._printData(level, __newFont_bold, false, SMLReport._report._columnBorder.TopBottom, _generateColumnStyle.Total);
                    for (int __column = 0; __column < level._columnList.Count; __column++)
                    {
                        level._columnList[__column]._sumTotalColumn = 0M;
                    }
                }
                // รวมกรณีมี grand total
                if (level._levelGrandTotal != null && level.__grandTotal != null && level.__grandTotal.Count > 0)
                {
                    // ข้อมูล
                    for (int __column = 0; __column < level.__grandTotal.Count; __column++)
                    {
                        level.__grandTotal[__column]._dataStr = "";
                        // toe check null && count level._columnList
                        level.__grandTotal[__column]._dataNumber = ((level._levelGrandTotal._sumTotal == true) || (level._columnList.Count > __column && level._columnList[__column] != null && level._columnList[__column]._isTotal)) ? level.__grandTotal[__column]._sumTotalColumn : 0M;
                    }
                    this._printGrandTotal(level._levelGrandTotal, level, __newFont_bold);
                    for (int __column = 0; __column < level.__grandTotal.Count; __column++)
                    {
                        level.__grandTotal[__column]._sumTotalColumn = 0M;
                    }
                }
            }
        }

        void _viewControl__getDataObject()
        {
            this._getDataObjectLevel(null, this._level, null);
        }

        /// <summary>
        /// จัดการหัวตาราง
        /// </summary>
        /// <param name="level"></param>
        private void _getObjectLevel(_generateLevelClass level)
        {
            SMLReport._report._columnBorder __border = SMLReport._report._columnBorder.None;
            if (level._levelParent == null) __border = SMLReport._report._columnBorder.Top;
            if (level._levelNext == null) __border = SMLReport._report._columnBorder.Bottom;
            if ((level._levelParent == null || level._levelParent._isHide) && level._levelNext == null) __border = SMLReport._report._columnBorder.TopBottom;

            // toe check hide
            //if (level._isHide == false)
            {
                level._objectList = _viewControl._addObject(_viewControl._objectList, SMLReport._report._objectType.Detail, true, 0, true, __border, level._isHide == true ? false : true);
                //level._objectList = _viewControl._addObject(_viewControl._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                for (int __column = 0; __column < level._columnList.Count; __column++)
                {
                    //if (level._columnList[__column]._isHideColumn == false)
                    {


                        _generateColumnListClass __data = level._columnList[__column];
                        SMLReport._report._cellAlign __cellAlign;
                        switch (__data._dataType)
                        {
                            case SMLReport._report._cellType.Number:
                            case SMLReport._report._cellType.Formula:
                                __cellAlign = SMLReport._report._cellAlign.Right;
                                break;
                            default:
                                __cellAlign = SMLReport._report._cellAlign.Left;
                                break;
                        }
                        /*SMLReport._report._columnBorder __border = SMLReport._report._columnBorder.None;
                        if (level._levelParent == null) __border = SMLReport._report._columnBorder.Top;
                        if (level._levelNext == null) __border = SMLReport._report._columnBorder.Bottom;
                        if (level._levelParent == null && level._levelNext == null) __border = SMLReport._report._columnBorder.TopBottom;*/
                        // toe headeralign
                        if (__data._headeralign != _report._cellAlign.Default)
                        {
                            __cellAlign = __data._headeralign;
                        }

                        // toe border 
                        __border = SMLReport._report._columnBorder.None;
                        if (__data._headerBorderStyle != _report._columnBorder.None)
                            __border = __data._headerBorderStyle;

                        string __resourceName = (this._resourceTable.Length == 0) ? __data._resourceName : this._resourceTable + "." + __data._resourceName;
                        if (__data._resourceName.Length == 0) __resourceName = "";
                        _viewControl._addColumn(level._objectList, __data._widthPercent, true, __border, SMLReport._report._columnBorder.None, __data._fieldName, __resourceName, __data._fieldName, __cellAlign); // หัวตาราง

                        //_viewControl._addColumn(level._objectList, __data._widthPercent, true, __border, SMLReport._report._columnBorder.None, __data._fieldName, __resourceName, __data._fieldName, __cellAlign);
                    }
                }
            }

            if (level._levelNext != null)
            {
                this._getObjectLevel(level._levelNext);
            }
        }

        bool _viewControl__getObject(ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                if (_renderHeader == null)
                {
                    SMLReport._report._objectListType __headerObject = _viewControl._addObject(_viewControl._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                    int __newColumn = _viewControl._addColumn(__headerObject, 100);
                    _viewControl._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, _viewControl._fontHeader1);
                    int __row = 2;
                    if (this._conditionText.Trim().Length > 0)
                    {
                        _viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, this._conditionText, SMLReport._report._cellAlign.Center, _viewControl._fontHeader2);
                        __row++;
                    }
                    if (this._conditionTextDetail.Trim().Length > 0)
                    {
                        _viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, this._conditionTextDetail, SMLReport._report._cellAlign.Center, _viewControl._fontHeader2);
                        __row++;
                    }
                    _viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Title    : " + this._reportName, SMLReport._report._cellAlign.Left, _viewControl._fontHeader2);
                    _viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _viewControl._fontHeader2);
                    __row++;
                    _viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, _viewControl._fontHeader2);
                    _viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, _viewControl._fontHeader2);
                    if (this._reportDescripton.Length > 0)
                    {
                        __row++;
                        _viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, "Description : " + this._reportDescripton, SMLReport._report._cellAlign.Left, _viewControl._fontHeader2);
                        //_viewControl._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, _viewControl._fontHeader2);
                    }
                }
                else
                {
                    this._renderHeader(this);
                }
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
            {
                this._getObjectLevel(this._level);
                return true;
            }
            else if (type == _report._objectType.PageFooter)
            {
                if (this._renderFooterPage != null)
                {
                    this._renderFooterPage(this);
                    return true;
                }
            }
            else if (type == _report._objectType.Footer)
            {
                if (this._renderFooter != null)
                {
                    this._renderFooter(this);
                    return true;
                }
            }
            return false;
        }

        void _viewControl__loadDataByThread()
        {
            if (this._query != null)
            {
                this._query();
            }
            this._viewControl._loadDataByThreadSuccess = true;
        }

        void _myFrameWork___queryStreamEvent(string lastMessage, int persentProcess)
        {
            this._viewControl._processMessage = lastMessage;
            this._viewControl._progessBarValue = persentProcess;
        }

        public _generateLevelClass _addLevel(string levelName, _generateLevelClass levelParent, List<_generateColumnListClass> columnList, Boolean sumTotal, Boolean autoWidth)
        {
            _generateLevelClass __levelNew = new _generateLevelClass(levelName, autoWidth);
            if (levelParent != null)
            {
                __levelNew._levelParent = levelParent;
                levelParent._levelNext = __levelNew;
            }
            __levelNew._sumTotal = sumTotal;
            for (int __loop = 0; __loop < columnList.Count; __loop++)
            {
                __levelNew._columnList.Add((_generateColumnListClass)columnList[__loop]);
            }
            return __levelNew;
        }

        private void _calcWidth(_generateLevelClass level)
        {
            level._calcWidth();
            if (level._levelNext != null)
            {
                this._calcWidth(level._levelNext);
            }
        }

        public void _build()
        {
            this._level = new _generateLevelClass("", false);
            if (this._init != null)
            {
                this._init();
            }
            // คำนวณความกว้างของ Field อัตโนมัติ
            this._calcWidth(this._level);
            this._viewControl._buildReport(SMLReport._report._reportType.Standard);
        }
    }

    public class _generateLevelClass
    {
        public List<_generateColumnListClass> __grandTotal;
        public string _levelName = "";
        public _generateLevelClass _levelParent = null;
        public _generateLevelClass _levelNext = null;
        public _generateLevelClass _levelGrandTotal = null;
        public List<_generateColumnListClass> _columnList = new List<_generateColumnListClass>();
        public SMLReport._report._objectListType _objectList;
        public Boolean _sumTotal;
        public DataTable _dataTable = null;
        public Boolean _autoWidth = true;
        public Boolean _isHide = false;

        public _generateLevelClass(string levelName, Boolean autoWidth)
        {
            this._levelName = levelName;
            this._autoWidth = autoWidth;
        }

        public string __fieldList(Boolean query)
        {
            StringBuilder __result = new StringBuilder();
            for (int __loop = 0; __loop < this._columnList.Count; __loop++)
            {
                if (this._columnList[__loop]._fieldName.Length > 0)
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    if (query)
                    {
                        __result.Append(this._columnList[__loop]._fieldName);
                        __result.Append(" as \"");
                        __result.Append(this._columnList[__loop]._fieldName);
                        __result.Append("\"");
                    }
                    else
                    {
                        __result.Append(this._columnList[__loop]._fieldName);
                    }
                }
            }
            return __result.ToString();
        }

        public int _findColumnName(string fieldName)
        {
            for (int __loop = 0; __loop < this._columnList.Count; __loop++)
            {
                if (this._columnList[__loop]._fieldName.Equals(fieldName))
                {
                    return __loop;
                }
            }
            return -1;
        }

        public void _calcWidth()
        {
            if (this._autoWidth)
            {
                float __sum = 0;
                for (int __loop = 0; __loop < this._columnList.Count; __loop++)
                {
                    __sum += this._columnList[__loop]._widthPercent;
                }
                for (int __loop = 0; __loop < _columnList.Count; __loop++)
                {
                    this._columnList[__loop]._widthPercent = ((this._columnList[__loop]._widthPercent * 100.0f) / __sum);
                }
            }
        }
    }

    public enum _generateColumnStyle
    {
        Data,
        Total,
        GrandTotal
    }

    public class _generateColumnListClass
    {
        private string _fontName = "Angsana New";
        public string _fieldName;
        public string _resourceName;
        public float _widthPercent;
        public SMLReport._report._cellType _dataType;
        public Font _font;
        public int _point;
        public decimal _sumTotalColumn = 0M;
        public string _dataStr = "";
        public decimal _dataNumber = 0M;
        public Boolean _isWhere = false;
        public Boolean _isTotal = true;
        public _report._cellAlign _align = _report._cellAlign.Default;
        public _report._cellAlign _headeralign = _report._cellAlign.Default;
        public SMLReport._report._columnBorder _dataBorderStyle = _report._columnBorder.None;
        public SMLReport._report._columnBorder _headerBorderStyle = _report._columnBorder.None;
        public float _fontSize = 9;
        public Boolean _isHideColumn = false;

        public _generateColumnListClass(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point)
        {
            if (_g.g._companyProfile._reportFontName.Length > 0)
            {
                this._fontName = _g.g._companyProfile._reportFontName;
                this._fontSize = _g.g._companyProfile._reportFontSize;
            }
            this._createColumn(fieldName, resourceName, width, dataType, point, this._fontName, this._fontSize, FontStyle.Regular, false, true);
        }

        public _generateColumnListClass(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, FontStyle fontStyle)
        {
            if (_g.g._companyProfile._reportFontName.Length > 0)
            {
                this._fontName = _g.g._companyProfile._reportFontName;
                this._fontSize = _g.g._companyProfile._reportFontSize;
            }
            this._createColumn(fieldName, resourceName, width, dataType, point, this._fontName, this._fontSize, fontStyle, false, true);
        }

        public _generateColumnListClass(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, FontStyle fontStyle, Boolean isWhereForDetail)
        {
            if (_g.g._companyProfile._reportFontName.Length > 0)
            {
                this._fontName = _g.g._companyProfile._reportFontName;
                this._fontSize = _g.g._companyProfile._reportFontSize;
            }
            this._createColumn(fieldName, resourceName, width, dataType, point, this._fontName, this._fontSize, fontStyle, isWhereForDetail, true);
        }

        public _generateColumnListClass(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, FontStyle fontStyle, Boolean isWhereForDetail, bool isTotal)
        {
            if (_g.g._companyProfile._reportFontName.Length > 0)
            {
                this._fontName = _g.g._companyProfile._reportFontName;
                this._fontSize = _g.g._companyProfile._reportFontSize;
            }
            this._createColumn(fieldName, resourceName, width, dataType, point, this._fontName, this._fontSize, fontStyle, isWhereForDetail, isTotal);
        }

        public _generateColumnListClass(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, FontStyle fontStyle, Boolean isWhereForDetail, bool isTotal, _report._cellAlign align)
        {
            if (_g.g._companyProfile._reportFontName.Length > 0)
            {
                this._fontName = _g.g._companyProfile._reportFontName;
                this._fontSize = _g.g._companyProfile._reportFontSize;
            }
            this._createColumn(fieldName, resourceName, width, dataType, point, this._fontName, this._fontSize, fontStyle, isWhereForDetail, isTotal, align);
        }

        public _generateColumnListClass(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, string fontName, float fontSize, FontStyle fontStyle)
        {
            string __fontName = (fontName == null || fontName.Length == 0) ? this._fontName : fontName;
            float __fontSize = (fontSize == 0) ? 9 : fontSize;
            this._createColumn(fieldName, resourceName, width, dataType, point, __fontName, __fontSize, fontStyle, false, true);
        }

        public _generateColumnListClass(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, string fontName, float fontSize, FontStyle fontStyle, Boolean isWhereForDetail)
        {
            this._createColumn(fieldName, resourceName, width, dataType, point, fontName, fontSize, fontStyle, isWhereForDetail, true);
        }

        private void _createColumn(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, string fontName, float fontSize, FontStyle fontStyle, Boolean isWhereForDetail, Boolean isTotal)
        {
            this._createColumn(fieldName, resourceName, width, dataType, point, fontName, fontSize, fontStyle, isWhereForDetail, isTotal, _report._cellAlign.Default);
        }

        private void _createColumn(string fieldName, string resourceName, float width, SMLReport._report._cellType dataType, int point, string fontName, float fontSize, FontStyle fontStyle, Boolean isWhereForDetail, Boolean isTotal, _report._cellAlign align)
        {
            if (fontName == null)
            {
                fontName = this._fontName;
            }
            this._fieldName = fieldName;
            this._resourceName = (resourceName == null || resourceName.Length == 0) ? fieldName : resourceName;
            this._widthPercent = width;
            this._dataType = dataType;
            this._font = new Font(fontName, fontSize, fontStyle);
            this._point = point;
            this._isWhere = isWhereForDetail;
            this._isTotal = isTotal;
            this._align = align;
        }
    }
}
