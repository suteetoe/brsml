using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SMLReport._report
{
    public class _util
    {
        public static string _processValue(string source)
        {
            source = source.Replace(_reportValueDefault._ltdName, MyLib._myGlobal._ltdName);
            source = source.Replace(_reportValueDefault._currentDateTime, MyLib._myGlobal._convertDateToString(DateTime.Today, true));
            return source;
        }
    }

    public class _reportValueDefault
    {
        public static string _ltdName = "&LTD_NAME&";
        public static string _currentDateTime = "&CURRENT_DATETIME&";
        public static string _address = "&LTD_ADDRESS&";  //MooAe add 12/03/2551
        public static string _telephone = "&LTD_TEL&";
        public static string _fax = "&LTD_FAX&";  //MooAe add 12/03/2551
        public static string _email = "&LTD_EMAIL";  //MooAe add 12/03/2551
        public static string _website = "&LTD_WEBSITE&"; //MooAe add 12/03/2551
        public static string _pageNumber = "&PAGE_NUMBER&";
        public static string _pageTotal = "&PAGE_TOTAL&";
    }

    public class _pageListType
    {
        public ArrayList _objectList = new ArrayList();
        public Size _pageSize = new Size(0, 0);
        public Size _paperPageSize = new Size(0, 0);
        public Boolean _haveFooterPage = false;
    }

    public class _cellListType
    {
        /// <summary>
        /// ประเภท (1=String,2=Double)
        /// </summary>
        public _cellType _type;
        public Font _font;
        public Point _position = new Point(0, 0);
        public bool _autoPosition = true;
        public string _text;
        public float _width;
        public float _height;
        public _cellAlign _alignCell = _cellAlign.Left;
    }

    public class _objectListType
    {
        public _objectType _type = _objectType.Detail;
        public ArrayList _objectList = new ArrayList();
        public ArrayList _columnList = new ArrayList();
        public bool _autoSize = true;
        public Size _size;
        public bool _autoPosition = true;
        public Point _position = new Point(0, 0);
        public float _leftMargin = 0f;
        public bool _leftMarginByPersent;
        public _columnBorder _border;
        public Boolean _show = true;
        public Boolean _pageBreak = false;
        public Boolean _isDataRow = false;
    }

    public class _columnDataListType
    {
        public _objectListType _objectSource;
        public int _columnAddr;
        public string _text;
        public _cellAlign _alignCell = _cellAlign.Default;
        public float _spaceBeforeText = 0f;
        public Font _font;
        public Color _fontColor;
        public _columnBorder _border;
        //somruk
        public _cellType _dataType = _cellType.String;
        public Boolean _breakLine = false;
        public Boolean _allowNewLineAscii = false;
        public Boolean _isHide = false;
        public float _totalColumnWidth = 0f;
    }

    public class _columnListType
    {
        public _objectType _type = _objectType.Detail;
        public bool _autoPosition = true;
        public Point _position = new Point(0, 0);
        public float _width;
        public float _height;
        public float _columnWidth = 100;
        public Font _fontData;
        public string _columnName;
        public string _fieldName;
        public string _resourceName;
        public string _text;
        public Color _columnHeadColor = Color.Black;
        public Color _columnHeadBackColor = Color.White;
        public Font _columnHeadFont;
        public Color _columnDetailColor = Color.Black;
        public Color _columnDetailBackColor = Color.White;
        public Font _columnDetailFont;
        /// <summary>
        /// 0=Left,1=Right,3=Center
        /// </summary>
        public int _alignHead = 3;
        public _cellAlign _alignCell = _cellAlign.Left;
        public ArrayList _columnList = new ArrayList();
        public ArrayList _cellList = new ArrayList();
        public int _maxLine = 0;
        public _columnBorder _border = _columnBorder.None;
        public _columnBorder _cellBorder = _columnBorder.None;
        public Color _borderColor = Color.Black;
        public Boolean _showColumnName = true;
        public Boolean _breakLine = false;
        public int _columnCount = 1;

    }

    public class _listType
    {
        public Font _font;
        public ArrayList _reportColumn = new ArrayList();
        public bool _columnWidthByPersent = true;
    }

    public class _linePositionType
    {
        public float _lineSpace;
        public float _fromTop;
    }

    public enum _objectType
    {
        Header,
        PageHeader,
        Detail,
        PageFooter,
        Footer,
        Bottom
    }

    public enum _cellType
    {
        String,
        Number,
        DateTime,
        Formula
    }

    public enum _cellAlign
    {
        Default,
        Left,
        Center,
        Right
    }

    public enum _columnBorder
    {
        None,
        All,
        Top,
        Left,
        LeftBottom,
        Right,
        RightBottom,
        Bottom,
        LeftRight,
        LeftRightBottom,
        TopBottom
    }
    public enum _reportType
    {
        Standard,
        Form
    }
}
