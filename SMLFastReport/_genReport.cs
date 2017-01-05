using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Diagnostics;
using MyLib;

namespace SMLFastReport
{
    public class _genReport
    {
        List<DataTable> _data = new List<DataTable>();
        _xmlClass _xmlSource;
        SMLReport._report._view _viewSource;
        private List<SMLReport._report._objectListType> _detailObject;
        int _numberOfDataTable = 0;
        public _conditionControl _condition;
        // โต๋ ยกเลิก ไปใช้ใน _xmlSource เอา
        //private List<int> _dataColumnPosition;
        //private List<_totalClass> _total;

        int _selectYear = 0;
        int _selectMonth = 0;
        Boolean _showQuery;

        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _genReport(SMLReport._report._view viewSource, _conditionControl condition)
        {
            this._viewSource = viewSource;
            this._condition = condition;
            //
            viewSource._getObject += new SMLReport._report.GetObjectEventHandler(viewSource__getObject);
            viewSource._getDataObject += new SMLReport._report.GetDataObjectEventHandler(viewSource__getDataObject);
            viewSource._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(viewSource__loadDataByThread);
            viewSource._columnResource += new SMLReport._report.ColumnResourceEventHandler(viewSource__columnResource);
            //
        }

        SMLReport._report.ColumnResourceStruct viewSource__columnResource(string resourceName)
        {
            SMLReport._report.ColumnResourceStruct __result = new SMLReport._report.ColumnResourceStruct();
            __result._findResource = true;
            __result._resourceName = MyLib._myResource._findResource(resourceName, false)._str;
            // Year,Month
            string __cutStr = "#month_name_";
            if (resourceName.IndexOf(__cutStr) != -1)
            {
                try
                {
                    int __monthNumber = (int)MyLib._myGlobal._decimalPhase(resourceName.Remove(0, __cutStr.Length));
                    int __month = this._selectMonth;
                    int __year = this._selectYear;
                    for (int __loop = 12; __loop > __monthNumber; __loop--)
                    {
                        __month--;
                        if (__month == 0)
                        {
                            __month = 12;
                            __year--;
                        }
                    }
                    __result._resourceName = _myGlobal._monthName(__month, false) + " " + __year.ToString();
                }
                catch
                {
                };
            }

            /*if (resourceName.IndexOf("#year_") != -1)
            {
                try
                {
                    int __yearNumber = (int)MyLib._myGlobal._decimalPhase(resourceName.Remove(0, __cutStr.Length));
                    int __year = this._selectYear;
                    for (int __loop = 12; __loop > __yearNumber; __loop--)
                    {
                        __year--;
                    }
                    __result._resourceName = __year.ToString();
                }
                catch
                {
                };
            }*/
            return __result;
        }

        public void _init(_xmlClass xmlSource, Boolean showQuery)
        {
            this._showQuery = showQuery;
            this._condition._buildFieldValue();

            if (this._condition._field != null)
            {
                for (int __loopField = 0; __loopField < this._condition._field.Count; __loopField++)
                {
                    switch (this._condition._field[__loopField]._command.ToLower())
                    {
                        case "#year#":
                            this._selectYear = (int)MyLib._myGlobal._decimalPhase(this._condition._field[__loopField]._value.ToString()) + MyLib._myGlobal._year_add;
                            break;
                        case "#month#":
                            this._selectMonth = (int)MyLib._myGlobal._decimalPhase(this._condition._field[__loopField]._value.ToString());
                            break;
                    }
                }
            }
            this._xmlSource = xmlSource;
            //
            //this._viewSource._pageSetupDialog.PageSettings.Landscape = true;
            this._viewSource._buttonExample.Enabled = false;
            //
            this._viewSource._fontHeader1 = new Font("Angsana New", 18, FontStyle.Bold);
            this._viewSource._fontHeader2 = new Font("Angsana New", 14, FontStyle.Bold);
            this._viewSource._fontStandard = new Font("Angsana New", 12, FontStyle.Regular);
            this._detailObject = new List<SMLReport._report._objectListType>();
            //this._total = new List<_totalClass>();
            //this._dataColumnPosition = new List<int>();
            this._viewSource._buildReport(SMLReport._report._reportType.Standard);
        }

        void viewSource__loadDataByThread()
        {
            Boolean __haveData = true;
            try
            {
                // ดึงข้อมูลจาก server
                this._data = new List<DataTable>();
                _myFrameWork __myFrameWork = new _myFrameWork();
                for (int __loop = 0; __loop < 3; __loop++)
                {
                    string __query = this._xmlSource._query[__loop]._query.Trim();
                    // add condition
                    this._condition._buildFieldValue();

                    string __conditionStr = "";

                    if (this._condition._field != null)
                    {
                        for (int __loopField = 0; __loopField < this._condition._field.Count; __loopField++)
                        {
                            switch (this._condition._field[__loopField]._fieldType)
                            {
                                case "Text":
                                    {
                                        __query = __query.Replace("@or_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueOr.ToString());
                                        __query = __query.Replace("@and_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueAnd.ToString());
                                        __query = __query.Replace("@where_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueWhere.ToString());
                                        __query = __query.Replace("@" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._value.ToString());

                                        __query = __query.Replace("@andcheck_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueAndcheck.ToString());
                                        __query = __query.Replace("@orcheck_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueOrCheck.ToString());
                                    }
                                    break;
                                case "Number":
                                    {
                                        __query = __query.Replace("@" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._value.ToString());
                                    }
                                    break;
                                case "Date":
                                    {
                                        string __value = MyLib._myGlobal._convertDateToQuery((DateTime)this._condition._field[__loopField]._value);
                                        __query = __query.Replace("@" + this._condition._field[__loopField]._queryName + "@", __value);
                                    }
                                    break;
                                case "DropDown":
                                    {
                                        __query = __query.Replace("@or_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._value.ToString());
                                        __query = __query.Replace("@and_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueAnd.ToString());
                                        __query = __query.Replace("@where_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueWhere.ToString());

                                        __query = __query.Replace("@" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._value.ToString());

                                        __query = __query.Replace("@andcheck_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueAndcheck.ToString());
                                        __query = __query.Replace("@orcheck_" + this._condition._field[__loopField]._queryName + "@", this._condition._field[__loopField]._valueOrCheck.ToString());

                                    }
                                    break;
                            }
                        }
                    }
                    //
                    if (__query.Trim().Length != 0)
                    {
                        try
                        {
                            if (this._showQuery)
                            {
                                _showQueryForm __form = new _showQueryForm();
                                __form._textBox.Text = __query;
                                __form.ShowDialog();
                            }
                            this._data.Add(__myFrameWork._queryStream(MyLib._myGlobal._databaseName, __query).Tables[0]);
                        }
                        catch (Exception __ex)
                        {
                            __haveData = false;
                            MessageBox.Show("ไม่พบข้อมูล หรือคำสั่งผิดพลาด" + " : " + __ex.Message.ToString());
                        }
                    }
                    else
                    {
                        this._data.Add(null);
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            if (__haveData)
            {
                this._viewSource._loadDataByThreadSuccess = true;
                // สั่งให้ประมวลผลรายงาน
                this._viewSource._buildReportActive = true;
            }
        }

        void _addDataTotal(int tableNumber, int objectNumber, int line, Font newFont, DataRow data, int endLine)
        {
            Boolean __lineTotal = false;
            for (int __column = 0; __column < this._xmlSource._query[tableNumber]._field.Count && __lineTotal == false; __column++)
            {
                _fieldClass __field = this._xmlSource._query[tableNumber]._field[__column];
                // ดูว่ามียอดรวมหรือไม่
                for (int __loop = 0; __loop < this._xmlSource._query[tableNumber]._total.Count; __loop++)
                {
                    if (this._xmlSource._query[tableNumber]._total[__loop]._lineNumber == line + 1/* && this._total[__loop]._columnNumber == __column*/)
                    {
                        __lineTotal = true;
                        break;
                    }
                }
            }
            if (__lineTotal)
            {
                // หา Column ที่กว้างที่สุด สำหรับพิมพ์ข้อมความยอดรวม

                // toe เปลี่ยน เป็นหา column ก่อนที่จะแสดง total เพื่อพิมพ์ยอดรวม

                int __columnForTotalWord = -1;
                float __columnForTotalWordWidth = 0.0f;
                for (int __column1 = 0; __column1 < this._xmlSource._query[tableNumber]._field.Count; __column1++)
                {
                    if (this._xmlSource._query[tableNumber]._field[__column1]._line == endLine)
                    {
                        if (__columnForTotalWord == -1)
                        {
                            __columnForTotalWord = __column1; // __column1 - 1;
                        }

                        if (this._xmlSource._query[tableNumber]._field[__column1]._printTotal == true)
                        {
                            //__columnForTotalWordWidth += this._xmlSource._query[tableNumber]._field[__column1]._widthPersent;
                            //__columnForTotalWord = __column1 - 1;

                            break;
                        }
                        __columnForTotalWordWidth += this._xmlSource._query[tableNumber]._field[__column1]._widthPersent;

                        /* if (__columnForTotalWordWidth < this._xmlSource._query[tableNumber]._field[__column1]._widthPersent)
                         {
                             __columnForTotalWordWidth = this._xmlSource._query[tableNumber]._field[__column1]._widthPersent;
                             __columnForTotalWord = __column1;
                         }*/
                    }
                }
                // หา Column ตั้งต้นของ Line
                StringBuilder __wordTotal = new StringBuilder();
                int __columnFirst = -1;
                for (int __column1 = 0; __column1 < this._xmlSource._query[tableNumber]._field.Count; __column1++)
                {
                    if (this._xmlSource._query[tableNumber]._field[__column1]._line == line + 1)
                    {
                        __columnFirst = __column1 - 1;
                        break;
                    }
                }
                for (int __column1 = __columnFirst; __column1 >= 0; __column1--)
                {
                    if (this._xmlSource._query[tableNumber]._field[__column1]._line == line)
                    {
                        string __str = (data == null) ? "*" : data[this._xmlSource._query[tableNumber]._dataColumnPosition[__column1]].ToString();
                        __wordTotal.Insert(0, __str);
                    }
                }
                if (__wordTotal.Length > 0)
                {
                    __wordTotal.Insert(0, "Total" + " : ");
                }
                if (line == -1)
                {
                    __wordTotal = new StringBuilder("Grand Total" + " : ");
                }

                int __columnCount = 0;
                Boolean __first = false;
                SMLReport._report._objectListType __newDataObject = null;
                for (int __column = 0; __column < this._xmlSource._query[tableNumber]._field.Count; __column++)
                {
                    decimal __value = 0M;
                    _fieldClass __field = this._xmlSource._query[tableNumber]._field[__column];
                    if (__field._line == endLine && __field._hide == false)
                    {
                        if (__first == false)
                        {
                            // __newDataObject = this._viewSource._addObject(this._viewSource._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            __newDataObject = this._viewSource._addObject(this._viewSource._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            __newDataObject._leftMargin = __field._margin;
                            __newDataObject._leftMarginByPersent = true;
                            this._viewSource._createEmtryColumn(this._detailObject[objectNumber], __newDataObject);
                            __first = true;
                        }
                        // ดูว่ามียอดรวมหรือไม่
                        Boolean __foundTotal = false;
                        for (int __loop = 0; __loop < this._xmlSource._query[tableNumber]._total.Count; __loop++)
                        {
                            if (this._xmlSource._query[tableNumber]._total[__loop]._lineNumber == line + 1 && this._xmlSource._query[tableNumber]._total[__loop]._columnNumber == __column)
                            {
                                __foundTotal = true;
                                __value = this._xmlSource._query[tableNumber]._total[__loop]._value;
                                this._xmlSource._query[tableNumber]._total[__loop]._value = 0;
                            }
                        }
                        //
                        Font __columnFont = newFont;
                        if (this._xmlSource._query[tableNumber]._field[__column]._fontName.Length > 0)
                        {
                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                            __columnFont = (Font)__tc1.ConvertFromString(this._xmlSource._query[tableNumber]._field[__column]._fontName);
                        }
                        __columnFont = new Font(__columnFont, FontStyle.Bold);
                        TypeConverter __tc = TypeDescriptor.GetConverter(typeof(Color));
                        Color __columnFontColor = (Color)__tc.ConvertFromString(this._xmlSource._query[tableNumber]._defaultFontColor);
                        if (this._xmlSource._query[tableNumber]._field[__column]._fontColor.Length > 0)
                        {
                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Color));
                            __columnFontColor = (Color)__tc1.ConvertFromString(this._xmlSource._query[tableNumber]._field[__column]._fontColor);
                        }
                        SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Right;
                        switch (this._xmlSource._query[tableNumber]._field[__column]._align.ToLower())
                        {
                            case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                            case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                            case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                        }
                        //
                        if (__foundTotal)
                        {
                            string __format = __field._format.Trim();
                            if (__format.ToLower().Equals("config_qty")) __format = "{0:#,#." + new String('0', _g.g._companyProfile._item_qty_decimal) + "}";
                            if (__format.ToLower().Equals("config_price")) __format = "{0:#,#." + new String('0', _g.g._companyProfile._item_price_decimal) + "}";
                            if (__format.ToLower().Equals("config_amount")) __format = "{0:#,#." + new String('0', _g.g._companyProfile._item_amount_decimal) + "}";
                            if (__format.Length == 0)
                            {
                                __format = "{0:#,#.00}";
                            }
                            string __dataStr = "";
                            if (__value != 0)
                            {
                                try
                                {
                                    __dataStr = String.Format(__format, __value);
                                }
                                catch
                                {
                                }
                            }
                            this._viewSource._addDataColumn(this._detailObject[objectNumber], __newDataObject, __columnCount, __dataStr, __columnFont, __cellAlign, this._xmlSource._query[tableNumber]._leftMargin, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number, __columnFontColor);
                            __columnCount++;
                        }
                        else
                        {
                            string __word = "";
                            if (__column == __columnForTotalWord)
                            {
                                __word = __wordTotal.ToString();
                                __wordTotal = new StringBuilder();
                            }
                            this._viewSource._addDataColumn(this._detailObject[objectNumber], __newDataObject, __columnCount, __word, __columnFont, SMLReport._report._cellAlign.Right, this._xmlSource._query[tableNumber]._leftMargin, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, __columnFontColor);

                            ((SMLReport._report._columnDataListType)__newDataObject._columnList[__columnCount])._totalColumnWidth = __columnForTotalWordWidth;
                            __columnCount++;
                        }
                    }
                }
            }
        }

        void _addDataColumn(int tableNumber, int objectNumberStart, DataTable parentTable, DataRow parentRow)
        {
            try
            {
                if (this._data.Count == 0 || this._data.Count <= tableNumber || this._data[tableNumber] == null)
                {
                    return;
                }
                DataRow[] __dataRows = null;
                if (this._xmlSource._query[tableNumber]._relation.Trim().Length > 0 && parentTable != null && parentRow != null)
                {
                    string __where = this._xmlSource._query[tableNumber]._relation;
                    // เอา ค่าจาก table ก่อนหน้า มา replace
                    for (int __column = 0; __column < parentRow.ItemArray.Length; __column++)
                    {
                        string __columnName = parentTable.Columns[__column].ColumnName;
                        string __columnValue = parentRow.ItemArray[__column].ToString().Replace("\'", "\'\'");
                        __where = __where.Replace("#" + __columnName + "#", __columnValue);
                    }

                    if (this._data[tableNumber].Rows.Count > 0)
                        __dataRows = this._data[tableNumber].Select(__where);
                }
                else
                {
                    __dataRows = this._data[tableNumber].Select();
                }
                SMLReport._report._objectListType __newDataObject = null;
                Font __newFont = new Font("Angsana New", 12, FontStyle.Regular);
                if (this._xmlSource._query[tableNumber]._defaultFont.ToString().Length > 0)
                {
                    TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                    __newFont = (Font)__tc1.ConvertFromString(this._xmlSource._query[tableNumber]._defaultFont.ToString());
                }

                ArrayList __oldDataLine = new ArrayList();
                for (int __line = 0; __line < 10; __line++)
                {
                    List<object> __data = new List<object>();
                    for (int __column = 0; __column < 1000; __column++)
                    {
                        __data.Add("@#$%^");
                    }
                    __oldDataLine.Add(__data);
                }
                int __maxLine = 0;
                this._xmlSource._query[tableNumber]._dataColumnPosition = new List<int>();
                this._xmlSource._query[tableNumber]._total = new List<_totalClass>();

                for (int __column1 = 0; __column1 < this._xmlSource._query[tableNumber]._field.Count; __column1++)
                {
                    if (this._xmlSource._query[tableNumber]._field[__column1]._line > __maxLine)
                    {
                        __maxLine = this._xmlSource._query[tableNumber]._field[__column1]._line;
                    }
                    // หาตำแหน่งข้อมูลให้สัมพันธ์กับชื่อ column
                    if (__dataRows != null && __dataRows.Length > 0)
                    {
                        for (int __loop = 0; __loop < this._data[tableNumber].Columns.Count; __loop++)
                        {
                            if (this._data[tableNumber].Columns[__loop].ColumnName.ToLower().Equals(this._xmlSource._query[tableNumber]._field[__column1]._fieldName.ToLower()))
                            {
                                this._xmlSource._query[tableNumber]._dataColumnPosition.Add(__loop);
                                break;
                            }
                        }
                    }
                    else
                    {
                        // toe แทรก ให้แสดง หัว columns กรณี ไม่มีข้อมูล
                        _fieldClass __field = this._xmlSource._query[tableNumber]._field[__column1];
                        __newDataObject = this._viewSource._addObject(this._viewSource._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);

                    }
                }
                int __rowNumber = 0;

                int __objectNumber = 0;
                if (__dataRows != null)
                {

                    for (int __row = 0; __row < __dataRows.Length; __row++)
                    {
                        //__rowNumber++;
                        __objectNumber = objectNumberStart;
                        for (int __line = 0; __line <= __maxLine; __line++)
                        {
                            // เปรียบเทียบ
                            Boolean __isEqual = true;
                            for (int __column = 0; __column < this._xmlSource._query[tableNumber]._field.Count; __column++)
                            {
                                _fieldClass __field = this._xmlSource._query[tableNumber]._field[__column];
                                // toe ทดสอบ concept คือ ถ้าเป็น maxline ไม่ต้องเช็ค equal แล้ว ปล่อยไปเลย
                                if (__field._line == __maxLine && __line == __maxLine && __field._hide == false)
                                {
                                    __isEqual = false;
                                    //__rowNumber = 1;
                                    break;
                                }
                                else
                                    // toe ทดสอบ
                                    if (__field._line == __line && __field._hide == false)
                                {
                                    string __data = __dataRows[__row].ItemArray[this._xmlSource._query[tableNumber]._dataColumnPosition[__column]].ToString();
                                    //string __data = __dataRows[__row].ItemArray[__column].ToString();
                                    List<object> __object = (List<object>)__oldDataLine[__line];
                                    if (__data.Equals(__object[__column].ToString()) == false)
                                    {
                                        __isEqual = false;
                                        //__rowNumber = 1;
                                        break;
                                    }
                                }
                            }
                            //__rowNumber++;
                            if (__isEqual == false)
                            {

                                Boolean __firstProcessLine = false;
                                //SMLReport._report._objectListType __newDataObject = this._viewSource._addObject(this._viewSource._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                //this._viewSource._createEmtryColumn(this._detailObject[objectNumber], __newDataObject);
                                int __columnCount = 0;
                                for (int __column = 0; __column < this._xmlSource._query[tableNumber]._field.Count; __column++)
                                {
                                    string __data = __dataRows[__row].ItemArray[this._xmlSource._query[tableNumber]._dataColumnPosition[__column]].ToString();
                                    //string __data = __dataRows[__row].ItemArray[__column].ToString();
                                    List<object> __object = (List<object>)__oldDataLine[__line];
                                    __object[__column] = __data;
                                    _fieldClass __field = this._xmlSource._query[tableNumber]._field[__column];
                                    if (__field._line == __line && __field._hide == false)
                                    {
                                        if (__firstProcessLine == false)
                                        {
                                            __firstProcessLine = true;
                                            __newDataObject = this._viewSource._addObject(this._viewSource._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                            __newDataObject._leftMargin = __field._margin;
                                            __newDataObject._leftMarginByPersent = true;

                                            // toe
                                            /*
                                            int _pageWidth = (int)(((this._viewSource._pageSetupDialog.PageSettings.Landscape) ? this._viewSource._pageSetupDialog.PageSettings.PaperSize.Height : this._viewSource._pageSetupDialog.PageSettings.PaperSize.Width) - (this._viewSource._pageSetupDialog.PageSettings.Margins.Left + this._viewSource._pageSetupDialog.PageSettings.Margins.Right));
                                            int _pageHeight = (int)(((this._viewSource._pageSetupDialog.PageSettings.Landscape) ? this._viewSource._pageSetupDialog.PageSettings.PaperSize.Width : this._viewSource._pageSetupDialog.PageSettings.PaperSize.Height) - (this._viewSource._pageSetupDialog.PageSettings.Margins.Top + this._viewSource._pageSetupDialog.PageSettings.Margins.Bottom));

                                            this._detailObject[__objectNumber]._size.Width = _pageWidth;
                                            this._detailObject[__objectNumber]._size.Height = _pageHeight;
                                            */

                                            this._viewSource._createEmtryColumn(this._detailObject[__objectNumber], __newDataObject);
                                        }
                                        Font __columnFont = __newFont;
                                        if (this._xmlSource._query[tableNumber]._field[__column]._fontName.Length > 0)
                                        {
                                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                                            __columnFont = (Font)__tc1.ConvertFromString(this._xmlSource._query[tableNumber]._field[__column]._fontName);
                                        }
                                        TypeConverter __tc = TypeDescriptor.GetConverter(typeof(Color));
                                        Color __columnFontColor = (Color)__tc.ConvertFromString(this._xmlSource._query[tableNumber]._defaultFontColor);
                                        if (this._xmlSource._query[tableNumber]._field[__column]._fontColor.Length > 0)
                                        {
                                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Color));
                                            __columnFontColor = (Color)__tc1.ConvertFromString(this._xmlSource._query[tableNumber]._field[__column]._fontColor);
                                        }
                                        switch (__field._type)
                                        {
                                            case "Text":
                                                {
                                                    SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Default;
                                                    switch (this._xmlSource._query[tableNumber]._field[__column]._align.ToLower())
                                                    {
                                                        case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                                                        case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                                                        case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                                                    }
                                                    this._viewSource._addDataColumn(this._detailObject[__objectNumber], __newDataObject, __columnCount, __data, __columnFont, __cellAlign, this._xmlSource._query[tableNumber]._leftMargin, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, __columnFontColor, __field._breakLine);

                                                    if (this._xmlSource._query[tableNumber]._field[__column]._printTotal)
                                                    {
                                                        Boolean __foundTotal = false;
                                                        for (int __find = 0; __find < this._xmlSource._query[tableNumber]._total.Count; __find++)
                                                        {
                                                            if (this._xmlSource._query[tableNumber]._total[__find]._lineNumber <= __line && this._xmlSource._query[tableNumber]._total[__find]._columnCount == __columnCount)
                                                            {
                                                                this._xmlSource._query[tableNumber]._total[__find]._value += 1;
                                                                __foundTotal = true;
                                                            }
                                                        }
                                                        if (__foundTotal == false)
                                                        {
                                                            for (int __lineLoop = __line; __lineLoop >= 0; __lineLoop--)
                                                            {
                                                                _totalClass __newData = new _totalClass();
                                                                __newData._lineNumber = __lineLoop;
                                                                __newData._columnNumber = __column;
                                                                __newData._columnCount = __columnCount;
                                                                __newData._value = 1;
                                                                this._xmlSource._query[tableNumber]._total.Add(__newData);
                                                            }
                                                        }
                                                    }
                                                    __columnCount++;
                                                }
                                                break;
                                            case "Number":
                                                {
                                                    string __format = __field._format.Trim();
                                                    if (__format.ToLower().Equals("config_qty")) __format = "{0:#,#." + new String('0', _g.g._companyProfile._item_qty_decimal) + "}";
                                                    if (__format.ToLower().Equals("config_price")) __format = "{0:#,#." + new String('0', _g.g._companyProfile._item_price_decimal) + "}";
                                                    if (__format.ToLower().Equals("config_amount")) __format = "{0:#,#." + new String('0', _g.g._companyProfile._item_amount_decimal) + "}";
                                                    if (__format.Length == 0)
                                                    {
                                                        __format = "{0:#,#.00}";
                                                    }
                                                    string __dataStr = "";
                                                    decimal __value = MyLib._myGlobal._decimalPhase(__data);
                                                    if (__value != 0)
                                                    {
                                                        __dataStr = __dataRows[__row].ItemArray[__column].ToString();
                                                        try
                                                        {
                                                            __dataStr = String.Format(__format, __value);
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Right;
                                                    switch (this._xmlSource._query[tableNumber]._field[__column]._align.ToLower())
                                                    {
                                                        case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                                                        case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                                                        case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                                                    }
                                                    this._viewSource._addDataColumn(this._detailObject[__objectNumber], __newDataObject, __columnCount, __dataStr, __columnFont, __cellAlign, this._xmlSource._query[tableNumber]._leftMargin, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number, __columnFontColor, __field._breakLine);
                                                    // สะสมยอดรวม
                                                    if (this._xmlSource._query[tableNumber]._field[__column]._printTotal)
                                                    {
                                                        Boolean __foundTotal = false;
                                                        for (int __find = 0; __find < this._xmlSource._query[tableNumber]._total.Count; __find++)
                                                        {
                                                            if (this._xmlSource._query[tableNumber]._total[__find]._lineNumber <= __line && this._xmlSource._query[tableNumber]._total[__find]._columnCount == __columnCount)
                                                            {
                                                                this._xmlSource._query[tableNumber]._total[__find]._value += __value;
                                                                __foundTotal = true;
                                                            }
                                                        }
                                                        if (__foundTotal == false)
                                                        {
                                                            for (int __lineLoop = __line; __lineLoop >= 0; __lineLoop--)
                                                            {
                                                                _totalClass __newData = new _totalClass();
                                                                __newData._lineNumber = __lineLoop;
                                                                __newData._columnNumber = __column;
                                                                __newData._columnCount = __columnCount;
                                                                __newData._value = __value;
                                                                this._xmlSource._query[tableNumber]._total.Add(__newData);
                                                            }
                                                        }
                                                    }
                                                    //
                                                    __columnCount++;
                                                }
                                                break;
                                            case "Date":
                                                {
                                                    string __dataStr = __data;
                                                    string __format = __field._format.Trim();
                                                    if (__format.Length > 0)
                                                    {
                                                        try
                                                        {
                                                            DateTime __date = DateTime.Parse(__dataStr, new CultureInfo("en-US"));
                                                            string[] __formatSplit = __format.Split(':');
                                                            if (__formatSplit.Length == 1)
                                                            {
                                                                __dataStr = __date.ToString(__formatSplit[0], MyLib._myGlobal._cultureInfo());
                                                            }
                                                            else
                                                                if (__formatSplit.Length == 2)
                                                            {
                                                                __dataStr = __date.ToString(__formatSplit[0], new CultureInfo(__formatSplit[1]));
                                                            }
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            DateTime __date = DateTime.Parse(__dataStr, new CultureInfo("en-US"));
                                                            __dataStr = MyLib._myGlobal._convertDateToString(__date, false);
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Default;
                                                    switch (this._xmlSource._query[tableNumber]._field[__column]._align.ToLower())
                                                    {
                                                        case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                                                        case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                                                        case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                                                    }
                                                    this._viewSource._addDataColumn(this._detailObject[__objectNumber], __newDataObject, __columnCount, __dataStr, __columnFont, __cellAlign, this._xmlSource._query[tableNumber]._leftMargin, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, __columnFontColor, __field._breakLine);
                                                    __columnCount++;
                                                }
                                                break;
                                            // toe
                                            case "AutoNumber":
                                                {
                                                    SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Default;
                                                    switch (this._xmlSource._query[tableNumber]._field[__column]._align.ToLower())
                                                    {
                                                        case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                                                        case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                                                        case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                                                    }
                                                    int __lineNumber = __row + 1;
                                                    this._viewSource._addDataColumn(this._detailObject[__objectNumber], __newDataObject, __columnCount, __lineNumber.ToString(), __columnFont, __cellAlign, this._xmlSource._query[tableNumber]._leftMargin, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, __columnFontColor, __field._breakLine);
                                                    __columnCount++;
                                                }
                                                break;
                                        }
                                    }
                                }
                                if (__firstProcessLine)
                                {
                                    __objectNumber++;
                                }
                            }
                            else
                            {
                                __objectNumber++;
                            }
                        }
                        // ตรวจดูว่า รายการต่อไป ซ้ำหรือเปล่า ถ้าไม่ซ้ำก็ใหม่พิมพ์ sub total
                        // เปรียบเทียบ
                        for (int __line2 = __maxLine - 1; __line2 >= 0; __line2--)
                        {
                            Boolean __checkEqual = true;
                            if (__row < __dataRows.Length - 1)
                            {

                                for (int __column = 0; __column < this._xmlSource._query[tableNumber]._field.Count; __column++)
                                {
                                    _fieldClass __field = this._xmlSource._query[tableNumber]._field[__column];
                                    if (__field._line == __line2 && __field._hide == false)
                                    {
                                        string __data = __dataRows[__row + 1].ItemArray[this._xmlSource._query[tableNumber]._dataColumnPosition[__column]].ToString();
                                        //string __data = __dataRows[__row].ItemArray[__column].ToString();
                                        List<object> __object = (List<object>)__oldDataLine[__line2];
                                        if (__data.Equals(__object[__column].ToString()) == false)
                                        {
                                            __checkEqual = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                __checkEqual = false;
                            }
                            if (__checkEqual == false)
                            {
                                this._addDataTotal(tableNumber, __objectNumber - 1, __line2, __newFont, __dataRows[__row], __maxLine);
                            }
                        }
                        if (this._numberOfDataTable > tableNumber - 1)
                        {
                            // toe เอาใส่เอง ให้ดึงรายการ บรรทัดต่อไป
                            this._addDataColumn(tableNumber + 1, __objectNumber, this._data[tableNumber], __dataRows[__row]);
                        }
                    }
                }
                this._addDataTotal(tableNumber, __objectNumber - 1, -1, __newFont, null, __maxLine);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void viewSource__getDataObject()
        {
            // this._detailObject = new List<SMLReport._report._objectListType>();
            this._addDataColumn(0, 0, null, null);
        }

        bool viewSource__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            try
            {
                if (type == SMLReport._report._objectType.Header)
                {
                    this._detailObject = new List<SMLReport._report._objectListType>();

                    // Write Header Report
                    SMLReport._report._objectListType __headerObject = this._viewSource._addObject(this._viewSource._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                    int __newColumn = this._viewSource._addColumn(__headerObject, 100);
                    this._viewSource._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._viewSource._fontHeader1);

                    // โต๋ เพิ่ม condition ในรายงาน
                    StringBuilder __conditionStr = new StringBuilder();
                    for (int __i = 0; __i < this._condition._field.Count; __i++)
                    {
                        _myResourceType __getResource = _myResource._findResource(this._condition._field[__i]._name, this._condition._field[__i]._name);

                        string __fieldName = (__getResource._str.Length > 0) ? __getResource._str : this._condition._field[__i]._name;
                        string __fieldValue = "";// = this._condition._field[__i]._value.ToString();

                        switch (this._condition._field[__i]._fieldType)
                        {
                            case "Number":
                                __fieldValue = this._condition._field[__i]._value.ToString();
                                break;
                            case "Date":
                                //__fieldValue = MyLib._myGlobal._convertDateToString(MyLib._myGlobal._convertDateFromQuery(this._condition._field[__i]._value.ToString()), false);
                                __fieldValue = MyLib._myGlobal._convertDateToString((DateTime)this._condition._field[__i]._value, false);
                                break;
                            default:
                                __fieldValue = this._condition._field[__i]._value.ToString();
                                break;
                        }

                        if (__fieldValue.Length > 0)
                        {
                            __conditionStr.Append(string.Format("{0} : {1} ", __fieldName, __fieldValue));
                        }
                    }

                    //_viewControl._addCell(__headerObject, __newColumn, true, __row, -1, SMLReport._report._cellType.String, this._conditionTextDetail, SMLReport._report._cellAlign.Center, _viewControl._fontHeader2);
                    if (__conditionStr.Length > 0)
                    {
                        this._viewSource._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, __conditionStr.ToString(), SMLReport._report._cellAlign.Center, this._viewSource._fontHeader2);
                    }

                    this._viewSource._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title\t: " + MyLib._myResource._findResource(this._xmlSource._header, false)._str, SMLReport._report._cellAlign.Left, this._viewSource._fontHeader2);
                    this._viewSource._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._viewSource._fontHeader2);
                    this._viewSource._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Printed By\t: " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._viewSource._fontHeader2);
                    this._viewSource._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Printed Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._viewSource._fontHeader2);
                    this._viewSource._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "Description\t: ", SMLReport._report._cellAlign.Left, this._viewSource._fontHeader2);
                    this._viewSource._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, this._xmlSource._refNo, SMLReport._report._cellAlign.Right, this._viewSource._fontHeader2);

                    return true;
                }
                else
                    if (type == SMLReport._report._objectType.Detail)
                {
                    // พิมพ์ชื่อฟิลด์ query
                    this._numberOfDataTable = 0;
                    for (int __queryLoop = 0; __queryLoop < 3; __queryLoop++)
                    {
                        if (this._data[__queryLoop] != null)
                        {
                            this._numberOfDataTable++;
                        }
                    }
                    for (int __queryLoop = 0; __queryLoop < _numberOfDataTable; __queryLoop++)
                    {
                        int __maxLine = 0;
                        for (int __column1 = 0; __column1 < this._xmlSource._query[__queryLoop]._field.Count; __column1++)
                        {
                            if (this._xmlSource._query[__queryLoop]._field[__column1]._line > __maxLine)
                            {
                                __maxLine = this._xmlSource._query[__queryLoop]._field[__column1]._line;
                            }
                        }
                        for (int __line = 0; __line <= __maxLine; __line++)
                        {
                            SMLReport._report._columnBorder __columnBorder = SMLReport._report._columnBorder.None;
                            if (__queryLoop == 0 && __queryLoop == _numberOfDataTable - 1 && __maxLine == 0)
                            {
                                // กรณีมีบรรทัดเดียว
                                __columnBorder = SMLReport._report._columnBorder.TopBottom;
                            }
                            else
                                if (__queryLoop == 0 && __line == 0)
                            {
                                __columnBorder = SMLReport._report._columnBorder.Top;
                            }
                            else
                                    if (__queryLoop == _numberOfDataTable - 1 && __line == __maxLine)
                            {
                                __columnBorder = SMLReport._report._columnBorder.Bottom;
                            }
                            Boolean __firstProcessLine = false;
                            for (int __column = 0; __column < this._xmlSource._query[__queryLoop]._field.Count; __column++)
                            {
                                if (this._xmlSource._query[__queryLoop]._field[__column]._line == __line)
                                {
                                    if (this._xmlSource._query[__queryLoop]._field[__column]._hide == false)
                                    {
                                        _fieldClass __field = this._xmlSource._query[__queryLoop]._field[__column];
                                        if (__firstProcessLine == false)
                                        {
                                            __firstProcessLine = true;
                                            SMLReport._report._objectListType __dataObject = this._viewSource._addObject(this._viewSource._objectList, SMLReport._report._objectType.Detail, true, 0, true, __columnBorder);
                                            __dataObject._leftMargin = __field._margin;
                                            __dataObject._leftMarginByPersent = true;

                                            // toe
                                            /*
                                            int _pageWidth = (int)(((this._viewSource._pageSetupDialog.PageSettings.Landscape) ? this._viewSource._pageSetupDialog.PageSettings.PaperSize.Height : this._viewSource._pageSetupDialog.PageSettings.PaperSize.Width) - (this._viewSource._pageSetupDialog.PageSettings.Margins.Left + this._viewSource._pageSetupDialog.PageSettings.Margins.Right));
                                            int _pageHeight = (int)(((this._viewSource._pageSetupDialog.PageSettings.Landscape) ? this._viewSource._pageSetupDialog.PageSettings.PaperSize.Width : this._viewSource._pageSetupDialog.PageSettings.PaperSize.Height) - (this._viewSource._pageSetupDialog.PageSettings.Margins.Top + this._viewSource._pageSetupDialog.PageSettings.Margins.Bottom));

                                            __dataObject._size.Width = _pageWidth;
                                            __dataObject._size.Height = _pageHeight;*/

                                            this._detailObject.Add(__dataObject);
                                        }
                                        //
                                        Font __newFont = _viewSource._fontStandard;
                                        if (this._xmlSource._query[__queryLoop]._defaultFont.Length > 0)
                                        {
                                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                                            __newFont = (Font)__tc1.ConvertFromString(this._xmlSource._query[__queryLoop]._defaultFont);
                                        }
                                        if (__field._fontName.Length > 0)
                                        {
                                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                                            __newFont = (Font)__tc1.ConvertFromString(__field._fontName);
                                        }
                                        //
                                        Color __newColor = Color.Black;
                                        if (this._xmlSource._query[__queryLoop]._defaultFontColor.Length > 0)
                                        {
                                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Color));
                                            __newColor = (Color)__tc1.ConvertFromString(this._xmlSource._query[__queryLoop]._defaultFontColor);
                                        }
                                        if (__field._fontName.Length > 0)
                                        {
                                            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Color));
                                            __newColor = (Color)__tc1.ConvertFromString(__field._fontColor);
                                        }
                                        //
                                        switch (__field._type)
                                        {
                                            case "Text":
                                            case "AutoNumber":
                                                {
                                                    SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Default;
                                                    switch (__field._align.ToLower())
                                                    {
                                                        case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                                                        case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                                                        case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                                                    }
                                                    this._viewSource._addColumn(this._detailObject[this._detailObject.Count - 1], __field._widthPersent, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, __field._fieldName, __field._resourceCode, __field._fieldName, __cellAlign, true, __newFont, __newColor);
                                                }
                                                break;
                                            case "Number":
                                                {
                                                    SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Right;
                                                    switch (__field._align.ToLower())
                                                    {
                                                        case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                                                        case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                                                        case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                                                    }
                                                    this._viewSource._addColumn(this._detailObject[this._detailObject.Count - 1], __field._widthPersent, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, __field._fieldName, __field._resourceCode, __field._fieldName, __cellAlign, true, __newFont, __newColor);
                                                }
                                                break;
                                            case "Date":
                                                {
                                                    SMLReport._report._cellAlign __cellAlign = SMLReport._report._cellAlign.Default;
                                                    switch (__field._align.ToLower())
                                                    {
                                                        case "left": __cellAlign = SMLReport._report._cellAlign.Left; break;
                                                        case "center": __cellAlign = SMLReport._report._cellAlign.Center; break;
                                                        case "right": __cellAlign = SMLReport._report._cellAlign.Right; break;
                                                    }
                                                    this._viewSource._addColumn(this._detailObject[this._detailObject.Count - 1], __field._widthPersent, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, __field._fieldName, __field._resourceCode, __field._fieldName, __cellAlign, true, __newFont, __newColor);
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}

