using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Globalization;

namespace SMLICIAdmin.excelObject
{
    public class _excelClass
    {
        ArrayList _cellData = new ArrayList();
        public ArrayList _columnData = new ArrayList();
        ArrayList _rowData = new ArrayList();
        ArrayList _styleData = new ArrayList();
        public int _maxColumn = 0;
        public int _currentRow = -1;
        public int _currentColumn = 0;
        int _styleRunningNumber = 0;
        public string _lastFontName = "Arial";
        public float _lastFontSize = 10;
        public _excelClass _reportexcel;
        public _pageSetupType _pageSetup = new _pageSetupType();
        //
        /// <summary>
        /// สร้าง File Excel 
        /// </summary>
        /// <param name="maxColumn">จำนวน Column สูงสุด เพื่อจอง Array ไม่งั้นจะ Error</param>
        /// <param name="defaultFontName">ชื่อ Font มาตรฐาน ใน Sheet ทั้งหมด (เปลี่ยนได้ทีหลัง)</param>
        /// <param name="defaultFontSize">ขนาด Font มาตรฐาน ใน Sheet ทั้งหมด (เปลี่ยนได้ทีหลัง)</param>
        public _excelClass(int maxColumn, string defaultFontName, float defaultFontSize)
        {
            //
            // TODO: Add constructor logic here
            //
            this._maxColumn = maxColumn;
            for (int __loop = 0; __loop < maxColumn; __loop++)
            {
                _columnClass _newColumn = new _columnClass();
                _columnData.Add(_newColumn);
            }
            this._lastFontName = defaultFontName;
            this._lastFontSize = defaultFontSize;
        }


        public string _convertDateToStr(DateTime value)
        {
            string result = "";
            try
            {
                CultureInfo ci1 = new CultureInfo("en-US");
                result = value.ToString("yyyy-MM-dd", ci1);
            }
            catch
            {
            }
            return (result);
        }

        /// <summary>
        /// กำหนดความกว้างของ Column
        /// </summary>
        /// <param name="begin">เริ่มจาก Column (นับจาก 1)</param>
        /// <param name="end">ถึง Column</param>
        /// <param name="width">ความกว้างที่ต้องการกำหนด</param>
        public void _setColumnStyle(int begin, int end, float width)
        {
            for (int __loop = begin - 1; __loop <= end - 1; __loop++)
            {
                ((_columnClass)_columnData[__loop])._width = width;
            }
        }

        /// <summary>
        /// ซ่อน Column
        /// </summary>
        /// <param name="columnNumber">เลขท่ Column</param>
        /// <param name="value">true or false</param>
        public void _setColumnHide(int columnNumber, bool value)
        {
            ((_columnClass)_columnData[columnNumber])._hide = value;
        }


        /// <summary>
        /// หลังจากกำหนดทุกอย่างแล้ว เรียก Function นี้เพิ่มสร้าง xml file พร้อม zip file
        /// </summary>
        /// <param name="fileName">ชื่อแฟ้มข้อมูล (ไม่ต้องมีนามสกุล และชื่อ Folder ขอชื่อล้วนๆ</param>
        /// <returns>ชื่อ zip file สำหรับ Download</returns>

        public string _createExcelFile(string fileName)
        {
            string __xmlFileName = fileName;
            // string __xmlFileName = fileName + Guid.NewGuid().ToString("N") + ".xml";
            // string __xmlPathName = "\\temp\\" + __xmlFileName;
            // string __zipPathName = "\\temp\\" + fileName + ".zip";
            // string __zipFileName = fileName + ".zip";
            // ลบแฟ้มเดิมทิ้ง
            try
            {
                File.Delete(__xmlFileName);
            }
            catch
            {
            }
            //
            StringBuilder __xmlString = new StringBuilder();
            __xmlString.Append("<?xml version=\"1.0\"?>\n");
            __xmlString.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
            __xmlString.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
            __xmlString.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\n");
            __xmlString.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\n");
            __xmlString.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">\n");

            __xmlString.Append("<Styles>\n");
            /*__xmlString.Append("<Style ss:ID=\"Default\" ss:Name=\"Normal\">\n");
            __xmlString.Append("<Alignment ss:Vertical=\"Bottom\"/>\n");
            __xmlString.Append("</Style>\n");
            __xmlString.Append("<Style ss:ID=\"scenter\">\n");
            __xmlString.Append("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>\n");
            __xmlString.Append("</Style>\n");
            __xmlString.Append("<Style ss:ID=\"scenterboxall\">");
            __xmlString.Append("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>");
            __xmlString.Append("<Borders>");
            __xmlString.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            __xmlString.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            __xmlString.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            __xmlString.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>");
            __xmlString.Append("</Borders>");
            __xmlString.Append("</Style>");*/
            for (int __loop = 0; __loop < this._styleData.Count; __loop++)
            {
                _cellStyleType __getStyle = (_cellStyleType)this._styleData[__loop];
                __xmlString.Append("<Style ss:ID=\"" + __getStyle._name + "\">");
                string __horizontal = "";
                string __vertical = "";
                switch (__getStyle._horizontal)
                {
                    case _horizontalType.Center: __horizontal = "Center"; break;
                    case _horizontalType.Left: __horizontal = "Left"; break;
                    case _horizontalType.Right: __horizontal = "Right"; break;
                }
                switch (__getStyle._vertical)
                {
                    case _verticalType.Center: __vertical = "Center"; break;
                    case _verticalType.Top: __vertical = "Top"; break;
                    case _verticalType.Bottom: __vertical = "Bottom"; break;
                }
                __xmlString.Append("<Alignment ss:Horizontal=\"" + __horizontal + "\" ss:Vertical=\"" + __vertical + "\" ");
                if (__getStyle._shrinkToFit)
                {
                    __xmlString.Append(" ss:ShrinkToFit=\"1\" ");
                }
                __xmlString.Append("/>\n");
                if (__getStyle._border)
                {
                    __xmlString.Append("<Borders>\n");
                    if (__getStyle._borderBottomWidth > 0) __xmlString.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"" + _borderLineStyle(__getStyle._borderBottomStyle) + "\" ss:Weight=\"1\" ss:Color=\"" + ((__getStyle._borderBottomWidth == 1) ? "#C0C0C0" : "#000000") + "\"/>\n");
                    if (__getStyle._borderLeftWidth > 0) __xmlString.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"" + _borderLineStyle(__getStyle._borderLeftStyle) + "\" ss:Weight=\"1\" ss:Color=\"" + ((__getStyle._borderLeftWidth == 1) ? "#C0C0C0" : "#000000") + "\"/>\n");
                    if (__getStyle._borderRightWidth > 0) __xmlString.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"" + _borderLineStyle(__getStyle._borderRightStyle) + "\" ss:Weight=\"1\" ss:Color=\"" + ((__getStyle._borderRightWidth == 1) ? "#C0C0C0" : "#000000") + "\"/>\n");
                    if (__getStyle._borderTopWidth > 0) __xmlString.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"" + _borderLineStyle(__getStyle._borderTopStyle) + "\" ss:Weight=\"1\" ss:Color=\"" + ((__getStyle._borderTopWidth == 1) ? "#C0C0C0" : "#000000") + "\"/>\n");
                    __xmlString.Append("</Borders>\n");
                }
                if (__getStyle._numberFormat.Length > 0)
                {
                    __xmlString.Append("<NumberFormat ss:Format=\"" + __getStyle._numberFormat + "\"/>\n");
                } if (__getStyle._isBold)
                {

                    __xmlString.Append("<Font ss:FontName=\"" + __getStyle._fontName + "\" ss:Bold=\"1\" ss:Size=\"" + __getStyle._fontSize + "\"/>\n");
                }
                else
                {
                    __xmlString.Append("<Font ss:FontName=\"" + __getStyle._fontName + "\" ss:Size=\"" + __getStyle._fontSize + "\"/>\n");
                }

                __xmlString.Append("</Style>\n");
            }
            __xmlString.Append("</Styles>\n");
            if (this._pageSetup._sheetName.Length > 0)
            {
                __xmlString.Append("<Worksheet ss:Name=\"" + this._pageSetup._sheetName + "\">\n");
            }
            else
            {
                __xmlString.Append("<Worksheet ss:Name=\"Report\">\n");
            }

            if (this._pageSetup._printTitle.Length > 0)
            {
                __xmlString.Append("<Names>");
                __xmlString.Append("<NamedRange ss:Name=\"Print_Titles\" ss:RefersTo=\"" + this._pageSetup._printTitle + "\"/>\n");
                __xmlString.Append("</Names>\n");
            }
            __xmlString.Append("<Table  ss:ExpandedColumnCount=\"" + this._columnData.Count + "\" ss:ExpandedRowCount=\"" + this._rowData.Count + "\">\n");
            for (int __loop = 0; __loop < this._columnData.Count; __loop++)
            {
                _columnClass __getClumn = (_columnClass)_columnData[__loop];
                if (__getClumn._hide)
                {
                    __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Hidden=\"1\" ss:Width=\"" + __getClumn._width.ToString() + "\"/>\n");
                }
                else
                {
                    __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"" + __getClumn._width.ToString() + "\"/>\n");
                }
            }
            //
            for (int __row = 0; __row < this._rowData.Count; __row++)
            {
                _rowClass __getRow = (_rowClass)_rowData[__row];
                __xmlString.Append("<Row>\n");
                int __pass = 0;
                for (int __column = 0; __column < __getRow._cell.Count; __column++)
                {
                    if (__pass > 0)
                    {
                        __pass--;
                    }
                    else
                    {
                        _cellClass __getCell = (_cellClass)__getRow._cell[__column];
                        if (__getCell._value != null)
                        {
                            string __getData = __getCell._value.ToString();
                            __xmlString.Append("<Cell ");
                            __xmlString.Append("ss:Index=\"" + __getCell._index.ToString() + "\" ");
                            string __style = (__getCell._styleName.Length == 0) ? " " : (" ss:StyleID=\"" + __getCell._styleName + "\" ");
                            if (__getCell._columnCount > 1)
                            {
                                int __getColumnCount = __getCell._columnCount - 1;
                                __xmlString.Append(" ss:MergeAcross=\"" + __getColumnCount.ToString() + "\"" + __style);
                                __pass = __getCell._columnCount - 1;
                            }
                            else
                            {
                                __xmlString.Append(__style);
                            }
                            string __dataType = "";
                            switch (__getCell._dataType)
                            {
                                case _cellType.String: __dataType = "String"; break;
                                case _cellType.Number: __dataType = "Number"; break;
                                case _cellType.DateTime: __dataType = "DateTime"; break;
                                case _cellType.Formula: __dataType = "Number";
                                    __xmlString.Append(" ss:Formula=\"" + __getData + "\"");
                                    __getData = "0";
                                    break;
                            }
                            __xmlString.Append(">");
                            __xmlString.Append("<Data ss:Type=\"" + __dataType + "\">");
                            __xmlString.Append(__getData);
                            __xmlString.Append("</Data>");
                            __xmlString.Append("</Cell>\n");
                        }
                    }
                }
                __xmlString.Append("</Row>\n");
            }
            //
            __xmlString.Append("</Table>\n");
            __xmlString.Append("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
            __xmlString.Append("<DoNotDisplayGridlines/>\n");
            __xmlString.Append("<PageSetup>\n");
            if (this._pageSetup._orientation == OrientationType.Landscape)
            {
                __xmlString.Append("<Layout x:Orientation=\"Landscape\"/>\n");
            }
            __xmlString.Append("<PageMargins x:Bottom=\"" + this._pageSetup._pageMarginBottom + "\" x:Left=\"" + this._pageSetup._pageMarginLeft + "\" x:Right=\"" + this._pageSetup._pageMarginRight + "\" x:Top=\"" + this._pageSetup._pageMarginTop + "\"/>\n");
            if (this._pageSetup._header.Length > 0)
            {
                __xmlString.Append("<Header x:Margin=\"" + this._pageSetup._headerMargin.ToString() + "\"  x:Data=\"" + this._pageSetup._header + "\"/>\n");
            }
            __xmlString.Append("</PageSetup>\n");
            __xmlString.Append("<Print>\n");
            __xmlString.Append("<ValidPrinterInfo/>\n");
            __xmlString.Append("<PaperSizeIndex>9</PaperSizeIndex>\n");
            __xmlString.Append("<VerticalResolution>0</VerticalResolution>\n");
            __xmlString.Append("</Print>\n");
            __xmlString.Append("</WorksheetOptions>\n");
            __xmlString.Append("</Worksheet>\n");
            __xmlString.Append("</Workbook>\n");
          
            File.WriteAllText(__xmlFileName, __xmlString.ToString());
            FileStream fs = File.OpenRead(__xmlFileName);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);

            fs.Close();

            // ลบแฟ้มเดิมทิ้ง
            try
            {
                // File.Delete(__xmlFileName);
            }
            catch
            {
            }
            return __xmlFileName;
        }

        /// <summary>
        /// เพิ่มบรรทัดใหม่
        /// </summary>
        public void _addRow()
        {
            _rowClass __newRow = new _rowClass(this._maxColumn);
            _rowData.Add(__newRow);
            this._currentRow++;
            this._currentColumn = 0;
        }

        /// <summary>
        /// กำหนดตำแหน่ง column ปัจจุบัน เก็บไว้ใน Array ของบรรทัด
        /// </summary>
        /// <param name="row">เลขที่ Row</param>
        /// <param name="column">เลขที่ Column</param>
        /// <param name="columnCount">ตำแหน่ง Column ปัจจุบัน</param>
        public void _cellColumnCount(int row, int column, int columnCount)
        {
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._columnCount = columnCount;
        }

        /// <summary>
        /// เปลี่ยนค่าใน Cell พร้อมกำหนด Style
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="value">Value (Object)</param>
        /// <param name="styleName">Style Name (ได้จากการกำหนด Style ล่วงหน้า)</param>
        public void _cellValue(int row, int column, object value, string styleName)
        {
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._index = this._currentColumn + 1;
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._value = value;
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._styleName = styleName;
            this._currentColumn = column + 1;
        }

        /// <summary>
        /// เปลี่ยนค่าใน Cell พร้อมกำหนด Style ในกรณี Cell นี้อยู่มากกว่า 1 Column
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="value">Value (Object)</param>
        /// <param name="styleName">Style Name</param>
        /// <param name="columnCount">เลื่อนไปอีกกี่ Column (ในกรณี Cell นี้ควบ 2 หรือ 3 Column)</param>
        public void _cellValue(int row, int column, object value, string styleName, int columnCount)
        {
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._index = this._currentColumn + 1;
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._value = value;
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._styleName = styleName;
            this._currentColumn = column + columnCount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="styleName"></param>
        /// <param name="columnCount"></param>
        public void _cellValue(int row, int column, object value, string styleName, _cellType dataType)
        {
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._index = this._currentColumn + 1;
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._value = value;
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._styleName = styleName;
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._dataType = dataType;
            this._currentColumn = column + 1;
        }
        /// <summary>
        /// เปลี่ยนค่าใน Cell พร้อมกำหนด Style ในกรณี Cell นี้อยู่มากกว่า 1 Column และ Row มากกว่า 1
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="value">Value</param>
        /// <param name="styleName">Style Name</param>
        /// <param name="columnCount">เลื่อนไปอีกกี่ Column</param>
        /// <param name="rowCount">เลื่อนลงกี่ Row</param>
        /// <param name="dataType">ประเภทข้อมูล</param>
        public void _cellValue(int row, int column, object value, string styleName, int columnCount, _cellType dataType)
        {
            _cellValue(row, column, value, styleName, columnCount);
            _cellColumnCount(row, column, columnCount);
            ((_cellClass)((_rowClass)_rowData[row])._cell[column])._dataType = dataType;
        }

        /// <summary>
        /// เปลี่ยนค่าใน Cell พร้อมกำหนด Style ในกรณี Cell นี้อยู่มากกว่า 1 Column และ Row มากกว่า 1 และกำหนด Index (ในกรณีที่มีการกระโดดข้าม Cell)
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="value">Value</param>
        /// <param name="styleName">Style Name</param>
        /// <param name="columnCount">เลื่อนไปอีกกี่ Column</param>
        /// <param name="rowCount">เลื่อนลงกี่ Row</param>
        /// <param name="dataType">ประเภทข้อมูล</param>
        /// <param name="index">ตำแหน่งของ Cell เริ่มต้น</param>
        public void _cellValue(int row, int column, object value, string styleName, int columnCount, _cellType dataType, int index)
        {
            _cellValue(row, column, value, styleName, columnCount, dataType);
            if (index != 0)
            {
                ((_cellClass)((_rowClass)_rowData[row])._cell[column])._index = index;
                this._currentColumn = (columnCount + index) - 1;
            }
        }

        /// <summary>
        /// เพิ่ม Style Name ก่อนที่จะทำการเพิ่ม Cell เพราะ Excel จะใช้ Style เป็นตัวกำหนดรูปแบบของ Cell
        /// </summary>
        /// <param name="_horizontal">จัดข้อมูลแนวนอน</param>
        /// <param name="_vertical">จัดข้อมูลแนวตั้ง</param>
        /// <param name="border">มีกรอบหรือไม่</param>
        /// <param name="borderLeftWidth">ความกว้างของเส้นด้านซ้าย</param>
        /// <param name="borderRightWidth">ความกว้างของเส้นด้านขวา</param>
        /// <param name="borderTopWidth">ความกว้างของเส้นด้านบน</param>
        /// <param name="borderBottomWidth">ความกว้างของเส้นด้านล่าง</param>
        /// <returns>Style Name เพื่อนำไปใช้ในการกำหนด Cell ต่อไป</returns>
        public string _styleAdd(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth)
        {
            return (_styleAdd(_horizontal, _vertical, border, borderLeftWidth, borderRightWidth, borderTopWidth, borderBottomWidth, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, "", false, this._lastFontName, this._lastFontSize, false));
        }

        /// <summary>
        /// เพิ่ม Style Name ก่อนที่จะทำการเพิ่ม Cell เพราะ Excel จะใช้ Style เป็นตัวกำหนดรูปแบบของ Cell
        /// </summary>
        /// <param name="_horizontal">จัดข้อมูลแนวนอน</param>
        /// <param name="_vertical">จัดข้อมูลแนวตั้ง</param>
        /// <param name="border">มีกรอบหรือไม่</param>
        /// <param name="borderLeftWidth">ความกว้างของเส้นด้านซ้าย</param>
        /// <param name="borderRightWidth">ความกว้างของเส้นด้านขวา</param>
        /// <param name="borderTopWidth">ความกว้างของเส้นด้านบน</param>
        /// <param name="borderBottomWidth">ความกว้างของเส้นด้านล่าง</param>
        /// <param name="numberFormat">Format ตัวเลข</param>
        /// <param name="shrinkToFit">ปรับขนาดตัวอักษรอัตโนมัติ (พอดีกับช่อง)</param>
        /// <returns>Style Name เพื่อนำไปใช้ในการกำหนด Cell ต่อไป</returns>
        public string _styleAdd(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth, string numberFormat, bool shrinkToFit)
        {
            return (_styleAdd(_horizontal, _vertical, border, borderLeftWidth, borderRightWidth, borderTopWidth, borderBottomWidth, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, numberFormat, shrinkToFit, this._lastFontName, this._lastFontSize, false));
        }

        /// <summary>
        /// เพิ่ม Style Name ก่อนที่จะทำการเพิ่ม Cell เพราะ Excel จะใช้ Style เป็นตัวกำหนดรูปแบบของ Cell
        /// </summary>
        /// <param name="_horizontal">จัดข้อมูลแนวนอน</param>
        /// <param name="_vertical">จัดข้อมูลแนวตั้ง</param>
        /// <param name="border">มีกรอบหรือไม่</param>
        /// <param name="borderLeftWidth">ความกว้างของเส้นด้านซ้าย</param>
        /// <param name="borderRightWidth">ความกว้างของเส้นด้านขวา</param>
        /// <param name="borderTopWidth">ความกว้างของเส้นด้านบน</param>
        /// <param name="borderBottomWidth">ความกว้างของเส้นด้านล่าง</param>
        /// <param name="numberFormat">Format ตัวเลข</param>
        /// <param name="shrinkToFit">ปรับขนาดตัวอักษรอัตโนมัติ (พอดีกับช่อง)</param>
        /// <param name="fontName">ชื่อ Font</param>
        /// <param name="fontSize">ขนาด Font</param>
        /// <returns>Style Name เพื่อนำไปใช้ในการกำหนด Cell ต่อไป</returns>
        public string _styleAdd(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth, string numberFormat, bool shrinkToFit, string fontName, float fontSize)
        {
            return (_styleAdd(_horizontal, _vertical, border, borderLeftWidth, borderRightWidth, borderTopWidth, borderBottomWidth, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, numberFormat, shrinkToFit, fontName, fontSize, false));
        }
        /// <summary>
        /// เพิ่ม Style Name ก่อนที่จะทำการเพิ่ม Cell เพราะ Excel จะใช้ Style เป็นตัวกำหนดรูปแบบของ Cell
        /// </summary>
        /// <param name="_horizontal">จัดข้อมูลแนวนอน</param>
        /// <param name="_vertical">จัดข้อมูลแนวตั้ง</param>
        /// <param name="border">มีกรอบหรือไม่</param>
        /// <param name="borderLeftWidth">ความกว้างของเส้นด้านซ้าย</param>
        /// <param name="borderRightWidth">ความกว้างของเส้นด้านขวา</param>
        /// <param name="borderTopWidth">ความกว้างของเส้นด้านบน</param>
        /// <param name="borderBottomWidth">ความกว้างของเส้นด้านล่าง</param>
        /// <param name="borderLeftStyle">line left style</param>
        /// <param name="borderRightStyle">line right style</param>
        /// <param name="borderTopStyle">line top style</param>
        /// <param name="borderBottomStyle">line bottom style</param>
        /// <param name="numberFormat">Format ตัวเลข</param>
        /// <param name="shrinkToFit">ปรับขนาดตัวอักษรอัตโนมัติ (พอดีกับช่อง)</param>
        /// <param name="fontName">ชื่อ Font</param>
        /// <param name="fontSize">ขนาด Font</param>
        /// <returns>Style Name เพื่อนำไปใช้ในการกำหนด Cell ต่อไป</returns>
        public String _styleAdd(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth,
                _borderStyleType borderLeftStyle, _borderStyleType borderRightStyle, _borderStyleType borderTopStyle, _borderStyleType borderBottomStyle, String numberFormat, bool shrinkToFit,
                String fontName, float fontSize, bool isBold)
        {
            String __result = "";
            int __findAdd = _styleFind(_horizontal, _vertical, border, borderLeftWidth, borderRightWidth, borderTopWidth, borderBottomWidth, borderLeftStyle, borderRightStyle, borderTopStyle, borderBottomStyle, numberFormat, shrinkToFit, fontName, fontSize, isBold);
            if (__findAdd == -1)
            {
                _styleRunningNumber++;
                _cellStyleType __newStyle = new _cellStyleType();
                __newStyle._name = "n" + _styleRunningNumber.ToString();
                __newStyle._horizontal = _horizontal;
                __newStyle._vertical = _vertical;
                __newStyle._border = border;
                __newStyle._borderLeftWidth = borderLeftWidth;
                __newStyle._borderRightWidth = borderRightWidth;
                __newStyle._borderTopWidth = borderTopWidth;
                __newStyle._borderBottomWidth = borderBottomWidth;
                __newStyle._borderLeftStyle = borderLeftStyle;
                __newStyle._borderRightStyle = borderRightStyle;
                __newStyle._borderTopStyle = borderTopStyle;
                __newStyle._borderBottomStyle = borderBottomStyle;
                __newStyle._numberFormat = numberFormat;
                __newStyle._shrinkToFit = shrinkToFit;
                __newStyle._fontName = fontName;
                __newStyle._fontSize = fontSize;
                __newStyle._isBold = isBold;
                this._lastFontName = fontName;
                this._lastFontSize = fontSize;
                this._styleData.Add(__newStyle);
                __result = __newStyle._name;
            }
            else
            {
                _cellStyleType __getStyle = (_cellStyleType)this._styleData[__findAdd];
                __result = __getStyle._name;
            }
            return (__result);
        }

        /// <summary>
        /// ค้นหา Style เดิม จะได้ไม่มี Style มากเกินไป เพราะ Cell จะใช้ Style ซ้ำๆกัน
        /// </summary>
        /// <param name="_horizontal"></param>
        /// <param name="_vertical"></param>
        /// <param name="border"></param>
        /// <param name="borderLeftWidth"></param>
        /// <param name="borderRightWidth"></param>
        /// <param name="borderTopWidth"></param>
        /// <param name="borderBottomWidth"></param>
        /// <param name="numberFormat"></param>
        /// <param name="shrinkToFit"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public int _styleFind(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth, string numberFormat, bool shrinkToFit, string fontName, float fontSize)
        {
            return (_styleFind(_horizontal, _vertical, border, borderLeftWidth, borderRightWidth, borderTopWidth, borderBottomWidth, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, numberFormat, shrinkToFit, fontName, fontSize));
        }

        /// <summary>
        /// ค้นหา Style เดิม จะได้ไม่มี Style มากเกินไป เพราะ Cell จะใช้ Style ซ้ำๆกัน
        /// </summary>
        /// <param name="_horizontal"></param>
        /// <param name="_vertical"></param>
        /// <param name="border"></param>
        /// <param name="borderLeftWidth"></param>
        /// <param name="borderRightWidth"></param>
        /// <param name="borderTopWidth"></param>
        /// <param name="borderBottomWidth"></param>
        /// <param name="borderLeftStyle"></param>
        /// <param name="borderRightStyle"></param>
        /// <param name="borderTopStyle"></param>
        /// <param name="borderBottomStyle"></param>
        /// <param name="numberFormat"></param>
        /// <param name="shrinkToFit"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public int _styleFind(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth,
            _borderStyleType borderLeftStyle, _borderStyleType borderRightStyle, _borderStyleType borderTopStyle, _borderStyleType borderBottomStyle, string numberFormat, bool shrinkToFit,
            string fontName, float fontSize)
        {
            int __result = -1;

            for (int __loop = 0; __loop < this._styleData.Count; __loop++)
            {
                _cellStyleType __getStyle = (_cellStyleType)this._styleData[__loop];
                if (__getStyle._horizontal == _horizontal &&
                    __getStyle._vertical == _vertical &&
                    __getStyle._border == border &&
                    __getStyle._borderLeftWidth == borderLeftWidth &&
                    __getStyle._borderRightWidth == borderRightWidth &&
                    __getStyle._borderTopWidth == borderTopWidth &&
                    __getStyle._borderBottomWidth == borderBottomWidth &&
                    __getStyle._borderLeftStyle == borderLeftStyle &&
                    __getStyle._borderRightStyle == borderRightStyle &&
                    __getStyle._borderTopStyle == borderTopStyle &&
                    __getStyle._borderBottomStyle == borderBottomStyle &&
                    __getStyle._shrinkToFit == shrinkToFit &&
                    __getStyle._fontSize == fontSize &&
                    __getStyle._fontName.Equals(fontName) &&
                    __getStyle._numberFormat.Equals(numberFormat))
                {
                    __result = __loop;
                    break;
                }
            }
            return (__result);
        }
        /// <summary>
        /// ค้นหา Style เดิม จะได้ไม่มี Style มากเกินไป เพราะ Cell จะใช้ Style ซ้ำๆกัน
        /// </summary>
        /// <param name="_horizontal"></param>
        /// <param name="_vertical"></param>
        /// <param name="border"></param>
        /// <param name="borderLeftWidth"></param>
        /// <param name="borderRightWidth"></param>
        /// <param name="borderTopWidth"></param>
        /// <param name="borderBottomWidth"></param>
        /// <param name="numberFormat"></param>
        /// <param name="shrinkToFit"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public int _styleFind(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth, String numberFormat, bool shrinkToFit, String fontName, float fontSize, bool isBold)
        {
            return (_styleFind(_horizontal, _vertical, border, borderLeftWidth, borderRightWidth, borderTopWidth, borderBottomWidth, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, numberFormat, shrinkToFit, fontName, fontSize, isBold));
        }

        /// <summary>
        /// ค้นหา Style เดิม จะได้ไม่มี Style มากเกินไป เพราะ Cell จะใช้ Style ซ้ำๆกัน
        /// </summary>
        /// <param name="_horizontal"></param>
        /// <param name="_vertical"></param>
        /// <param name="border"></param>
        /// <param name="borderLeftWidth"></param>
        /// <param name="borderRightWidth"></param>
        /// <param name="borderTopWidth"></param>
        /// <param name="borderBottomWidth"></param>
        /// <param name="borderLeftStyle"></param>
        /// <param name="borderRightStyle"></param>
        /// <param name="borderTopStyle"></param>
        /// <param name="borderBottomStyle"></param>
        /// <param name="numberFormat"></param>
        /// <param name="shrinkToFit"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public int _styleFind(_horizontalType _horizontal, _verticalType _vertical, bool border, int borderLeftWidth, int borderRightWidth, int borderTopWidth, int borderBottomWidth,
                _borderStyleType borderLeftStyle, _borderStyleType borderRightStyle, _borderStyleType borderTopStyle, _borderStyleType borderBottomStyle, String numberFormat, bool shrinkToFit,
                String fontName, float fontSize, bool isBold)
        {
            int __result = -1;
            for (int __loop = 0; __loop < this._styleData.Count; __loop++)
            {
                _cellStyleType __getStyle = (_cellStyleType)this._styleData[__loop];
                if (__getStyle._horizontal == _horizontal &&
                    __getStyle._vertical == _vertical &&
                    __getStyle._border == border &&
                    __getStyle._borderLeftWidth == borderLeftWidth &&
                    __getStyle._borderRightWidth == borderRightWidth &&
                    __getStyle._borderTopWidth == borderTopWidth &&
                    __getStyle._borderBottomWidth == borderBottomWidth &&
                    __getStyle._borderLeftStyle == borderLeftStyle &&
                    __getStyle._borderRightStyle == borderRightStyle &&
                    __getStyle._borderTopStyle == borderTopStyle &&
                    __getStyle._borderBottomStyle == borderBottomStyle &&
                    __getStyle._shrinkToFit == shrinkToFit &&
                    __getStyle._fontSize == fontSize &&
                    __getStyle._fontName.Equals(fontName) &&
                    __getStyle._numberFormat.Equals(numberFormat) && __getStyle._isBold == isBold)
                {
                    __result = __loop;
                    break;
                }
            }
            return (__result);
        }

        /// <summary>
        /// รูปแบบของ Border ดึงไปสร้าง XML
        /// </summary>
        /// <param name="borderLineStyle">ประเภท</param>
        /// <returns></returns>
        public string _borderLineStyle(_borderStyleType borderLineStyle)
        {
            switch (borderLineStyle)
            {
                case _borderStyleType.Continuous: return ("Continuous");
            }
            return "";
        }
    }

    public class _cellClass
    {
        /// <summary>
        /// Style Name
        /// </summary>
        public string _styleName = "";
        /// <summary>
        /// Index ปัจจุบัน (เปลี่ยนไปเรื่อยๆ เมื่อ Add Cell)
        /// </summary>
        public int _index = 0;
        /// <summary>
        /// บรรทัดปัจจุบัน
        /// </summary>
        public int _row;
        /// <summary>
        /// Column ปัจจุบัน
        /// </summary>
        public int _column;
        /// <summary>
        /// Default Align เปลี่ยนได้เมื่อต้องการค่า Default ใหม่
        /// </summary>
        public _cellAlignType _dataAlign = _cellAlignType.Left;
        /// <summary>
        /// Default Data เปลี่ยนได้
        /// </summary>
        public _cellType _dataType = _cellType.String;
        /// <summary>
        /// Border มาตฐาน
        /// </summary>
        public _cellStyleType _border = new _cellStyleType();
        public int _columnCount = 1;
        public object _value;

        public _cellClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class _columnClass
    {
        public float _width = 50;
        public bool _hide = false;
    }

    public enum _cellAlignType
    {
        Left,
        Right,
        Center
    }
    public enum _cellType
    {
        String,
        Number,
        DateTime,
        Formula
    }

    public class _cellStyleType
    {
        public string _name;
        public _horizontalType _horizontal = _horizontalType.Center;
        public _verticalType _vertical = _verticalType.Bottom;
        public bool _border = false;
        public int _borderLeftWidth = 1;
        public _borderStyleType _borderLeftStyle = _borderStyleType.Continuous;
        public int _borderRightWidth = 1;
        public _borderStyleType _borderRightStyle = _borderStyleType.Continuous;
        public int _borderTopWidth = 1;
        public _borderStyleType _borderTopStyle = _borderStyleType.Continuous;
        public int _borderBottomWidth = 1;
        public _borderStyleType _borderBottomStyle = _borderStyleType.Continuous;
        public string _numberFormat = "";
        public bool _shrinkToFit = false;
        public string _fontName = "Arial";
        public float _fontSize = 10f;
        public bool _isBold = false;
    }

    public enum _borderStyleType
    {
        Continuous
    }

    public enum _horizontalType
    {
        Center,
        Left,
        Right
    }

    public enum _verticalType
    {
        Center,
        Top,
        Bottom
    }

    public class _pageSetupType
    {
        public OrientationType _orientation = OrientationType.Landscape;
        public float _pageMarginTop = 1;
        public float _pageMarginBottom = 1;
        public float _pageMarginLeft = 1;
        public float _pageMarginRight = 1;
        public string _header = "";
        public float _headerMargin = 1;
        public string _printTitle = "";
        public string _sheetName = "";
    }

    public enum OrientationType
    {
        Landscape,
        Portait
    }

    public class _rowClass
    {
        public ArrayList _cell = new ArrayList();
        public _rowClass(int maxColumn)
        {
            for (int __loop = 0; __loop < maxColumn; __loop++)
            {
                _cellClass __newCell = new _cellClass();
                this._cell.Add(__newCell);
            }
        }
    }
}
