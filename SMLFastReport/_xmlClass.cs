using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SMLFastReport
{
    public class _totalClass
    {
        public int _lineNumber;
        public int _columnNumber;
        public int _columnCount;
        public decimal _value;
    }

    public class _xmlClass
    {
        public string _header;
        public string _refNo = "";
        public Boolean _isLandscape;
        public List<_queryClass> _query = new List<_queryClass>();
        public List<_conditionDetailClass> _conditionList = new List<_conditionDetailClass>();
        public _glHeaderClass _glHeader = new _glHeaderClass();
        public List<_glColumnClass> _glColumnList = new List<_glColumnClass>();
        public List<_glColumnClass> _glCommandList = new List<_glColumnClass>();
        public Boolean _splitRow = false;
        public Boolean _showLineLastPage = false;
    }

    /// <summary>
    /// งบการเงิน
    /// </summary>
    public class _glHeaderClass
    {
        /// <summary>
        /// 0=งบทั่วไป
        /// </summary>
        public int _type;
        /// <summary>
        /// หัวรายงาน (ได้หลายบรรทัด)
        /// </summary>
        public string _header;
        /// <summary>
        /// Font หัวรายงาน
        /// </summary>
        public string _headerFont;
        /// <summary>
        /// คำอธิบาย
        /// </summary>
        public string _headerDetail;
        /// <summary>
        /// font คำอธิบาย
        /// </summary>
        public string _headerDetailFont;
        /// <summary>
        /// จำนวน Column ของตัวเลข
        /// </summary>
        public int _columnMax;
    }

    public class _glColumnClass
    {
        public int _columnNumber;
        public float _columnWidth;
        public string _columnName;
        public string _columnFont;
    }

    public class _glCommandClass
    {
        public int _lineNumber;
        public int _columnNumber;
        public string _command;
        public string _fontName;
        /// <summary>
        /// จาก
        /// </summary>
        public int _periodBegin;
        public int _periodEnd;
        public string _dateBegin;
        public string _dateEnd;
        public List<_glCommandAccountListClass> _glCommandAccountList = new List<_glCommandAccountListClass>();

    }

    public class _glCommandAccountListClass
    {
        public string _accountCodeBegin;
        public string _accountCodeEnd;
    }

    public class _conditionDetailClass
    {
        /// <summary>
        /// บรรทัด
        /// </summary>
        public int _row;
        /// <summary>
        /// Column Number
        /// </summary>
        public int _column;
        /// <summary>
        /// Column Span
        /// </summary>
        public int _span;
        /// <summary>
        /// ชื่อ field
        /// </summary>
        public string _code;
        /// <summary>
        /// ข้อความ
        /// </summary>
        public string _text;
        /// <summary>
        /// Text,Number,Date
        /// </summary>
        public string _type;
        /// <summary>
        /// ค่า Default
        /// </summary>
        public string _defaultValue;
        /// <summary>
        /// คำสั่งค้นหา
        /// </summary>
        public string _command;
        /// <summary>
        /// ชื่อตัวแปรที่เอาไปใช้
        /// </summary>
        public string _name;
        /// <summary>
        /// ชื่อ Column อ้างอิง
        /// </summary>
        public string _columnName;
    }

    public class _queryClass
    {
        /// <summary>
        /// เอาไว้ดึงชื่อ field
        /// </summary>
        public string _queryForGetField;
        /// <summary>
        /// เอาไว้ทำงานตอนประมวลผล
        /// </summary>
        public string _query;
        /// <summary>
        /// ตัวแปรสำหรับเชื่อมโยง
        /// </summary>
        public string _relation;
        /// <summary>
        /// ชิดซ้าย (ตัวเลข)
        /// </summary>
        public float _leftMargin;
        /// <summary>ชื่อ Resource</summary>
        public string _resourceTableName;
        /// <summary>
        /// รายชื่อ Field
        /// </summary>
        public List<_fieldClass> _field = new List<_fieldClass>();
        public string _defaultFont;
        public string _defaultFontColor;
        /// <summary>ตำแหน่ง Field สำหรับแสดงผล (TOE)</summary>
        public List<int> _dataColumnPosition;
        public List<_totalClass> _total;
    }

    public class _fieldClass
    {
        /// <summary>บรรทัด</summary>
        public int _line;
        /// <summary>ชิดซ้าย</summary>
        public float _margin;
        /// <summary>False=ไม่แสดง,True=แสดง</summary>
        public bool _hide;
        /// <summary>
        /// ความกว้าง
        /// </summary>
        public float _width;
        /// <summary>
        /// ความกว้างเป็น % (จากการคำนวณ)
        /// </summary>
        public float _widthPersent;
        /// <summary>
        /// ชื่อ Field
        /// </summary>
        public string _fieldName;
        /// <summary>
        /// รหัส Resource
        /// </summary>
        public string _resourceCode;
        /// <summary>
        /// Resource (ลูกค้าแก้ได้)
        /// </summary>
        public string _resourceName;
        /// <summary>
        /// Text
        /// Date
        /// Number
        /// AutoNumber=แสดงหมายเลขบรรทัด
        /// </summary>
        public string _type;
        public string _format;
        /// <summary>
        /// True=พิมพ์,False=ไม่พิมพ์
        /// </summary>
        public bool _printTotal;
        public string _fontName;
        public string _fontColor;
        public string _align;
        /// <summary>
        /// ไม่ขึ้นบรรทัดใหม่ True=Cut First Line, False=New Line
        /// </summary>
        public bool _breakLine;
    }
}
