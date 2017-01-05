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

namespace SMLReport._report
{
    public partial class _reportDrawPaper : UserControl
    {
        public Font _fontStandard = new Font("Angsana New", 12, FontStyle.Regular);
        private float _drawScaleResult;
        public Point _topLeftPaperResult = new Point(20, 20);
        //
        public PageSetupDialog _pageSetupDialog;
        //
        public int _pageMax = 0;
        public int _pageCurrent = 0;
        public float _lineSpaceing = 80;
        //
        public ArrayList _objectList;
        public ArrayList _pageList;

        //
        public float _topMargin = 0;
        public float _leftMargin = 0;
        //
        public Graphics _graphicSystem;
        public Boolean _showLineLastPage = false;

        public delegate void drawFooterReport(int pageNumber, Graphics g, float startDrawYPoint, float drawScale);
        public event drawFooterReport _drawFooterReportEvent;
        //
        public _reportDrawPaper()
        {
            InitializeComponent();
            this._drawScale = 1f;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);
            this.Paint += new PaintEventHandler(_drawPaper_Paint);
            this._area.MouseMove += new MouseEventHandler(_area_MouseMove);
            this._area.Paint += new PaintEventHandler(_area_Paint);
            this.Invalidated += new InvalidateEventHandler(_reportDrawPaper_Invalidated);
        }

        public string _processValue(string source)
        {
            source = _util._processValue(source);
            source = source.Replace(_reportValueDefault._pageNumber, string.Format("{0:#,0}", this._pageCurrent + 1));
            source = source.Replace(_reportValueDefault._pageTotal, string.Format("{0:#,0}", this._pageMax));
            return source;
        }

        public int PageCurrent
        {
            get
            {
                return _pageCurrent + 1;
            }
            set
            {
                _pageCurrent = value - 1;
            }
        }

        void _reportDrawPaper_Invalidated(object sender, InvalidateEventArgs e)
        {
            this._area.Invalidate();
        }

        void _areaColumnDraw(Graphics g, _columnListType columnSource, Point position)
        {
            if (columnSource._border != _columnBorder.None)
            {
                Pen __myPen = new Pen(columnSource._borderColor, 1);
                int __columnWidth = (int)(columnSource._width * this._drawScale);
                int __columnHeight = (int)(columnSource._height * this._drawScale);
                if (columnSource._border == _columnBorder.All)
                {
                    Point[] __borderPoints = {
                        new Point(position.X,position.Y),
                        new Point(position.X+__columnWidth,position.Y),
                        new Point(position.X+__columnWidth,position.Y+__columnHeight),
                        new Point(position.X,position.Y+__columnHeight)
                    };
                    g.DrawPolygon(__myPen, __borderPoints);
                }
                else
                {
                    // บน
                    if (columnSource._border == _columnBorder.Top || columnSource._border == _columnBorder.TopBottom)
                    {
                        g.DrawLine(__myPen, position.X, position.Y, position.X + __columnWidth, position.Y);
                    }
                    // ล่าง
                    if (columnSource._border == _columnBorder.Bottom || columnSource._border == _columnBorder.LeftBottom || columnSource._border == _columnBorder.RightBottom || columnSource._border == _columnBorder.LeftRightBottom || columnSource._border == _columnBorder.TopBottom)
                    {
                        g.DrawLine(__myPen, position.X, position.Y + __columnHeight, position.X + __columnWidth, position.Y + __columnHeight);
                    }
                    // ซ้าย
                    if (columnSource._border == _columnBorder.Left || columnSource._border == _columnBorder.LeftRight || columnSource._border == _columnBorder.LeftBottom || columnSource._border == _columnBorder.LeftRightBottom)
                    {
                        g.DrawLine(__myPen, position.X, position.Y, position.X, position.Y + __columnHeight);
                    }
                    // ขวา
                    if (columnSource._border == _columnBorder.Right || columnSource._border == _columnBorder.LeftRight || columnSource._border == _columnBorder.RightBottom || columnSource._border == _columnBorder.LeftRightBottom)
                    {
                        g.DrawLine(__myPen, position.X + __columnWidth, position.Y, position.X + __columnWidth, position.Y + __columnHeight);
                    }
                }


                /*
                else
                    if (columnSource._border == _columnBorder.Left)
                {
                    g.DrawLine(__myPen, position.X, position.Y, position.X, position.Y + __columnHeight);
                }
                else
                        if (columnSource._border == _columnBorder.Right)
                {
                    g.DrawLine(__myPen, position.X + __columnWidth, position.Y, position.X + __columnWidth, position.Y + __columnHeight);
                }
                else
                            if (columnSource._border == _columnBorder.LeftRight)
                {
                    g.DrawLine(__myPen, position.X, position.Y, position.X, position.Y + __columnHeight);
                    g.DrawLine(__myPen, position.X + __columnWidth, position.Y, position.X + __columnWidth, position.Y + __columnHeight);
                }
                else
                                if (columnSource._border == _columnBorder.LeftBottom)
                {
                    g.DrawLine(__myPen, position.X, position.Y, position.X, position.Y + __columnHeight);
                    g.DrawLine(__myPen, position.X, position.Y + __columnHeight, position.X + __columnWidth, position.Y + __columnHeight);
                }
                else
                                    if (columnSource._border == _columnBorder.LeftRightBottom)
                {
                    g.DrawLine(__myPen, position.X, position.Y, position.X, position.Y + __columnHeight);
                    g.DrawLine(__myPen, position.X + __columnWidth, position.Y, position.X + __columnWidth, position.Y + __columnHeight);
                    g.DrawLine(__myPen, position.X, position.Y + __columnHeight, position.X + __columnWidth, position.Y + __columnHeight);
                }
                else
                                        if (columnSource._border == _columnBorder.Top)
                {
                    g.DrawLine(__myPen, position.X, position.Y, position.X + __columnWidth, position.Y);
                }
                else
                                            if (columnSource._border == _columnBorder.Bottom)
                {
                    g.DrawLine(__myPen, position.X, position.Y + __columnHeight, position.X + __columnWidth, position.Y + __columnHeight);
                }
                else
                                                if (columnSource._border == _columnBorder.TopBottom)
                {
                    g.DrawLine(__myPen, position.X, position.Y, position.X + __columnWidth, position.Y);
                    g.DrawLine(__myPen, position.X, position.Y + __columnHeight, position.X + __columnWidth, position.Y + __columnHeight);
                }*/

                __myPen.Dispose();
            }
            SolidBrush __cellBrush = new SolidBrush(columnSource._columnDetailColor);
            Font __newFont = new Font(columnSource._columnHeadFont.FontFamily, (float)(columnSource._columnHeadFont.Size * this._drawScale), columnSource._columnHeadFont.Style);
            switch (columnSource._alignCell)
            {
                case _cellAlign.Right:
                    {
                        SizeF __stringText = g.MeasureString(columnSource._text, __newFont);
                        position = new Point((int)(position.X + (float)((columnSource._width * this._drawScale) - __stringText.Width)), position.Y);
                        g.DrawString(columnSource._text, __newFont, __cellBrush, position);
                    }
                    break;
                case _cellAlign.Center:
                    {
                        SizeF __stringText = g.MeasureString(columnSource._text, __newFont);
                        position = new Point((int)(position.X + (float)(((columnSource._width * this._drawScale) / 2) - (__stringText.Width / 2))), position.Y);
                        g.DrawString(columnSource._text, __newFont, __cellBrush, position);
                    }
                    break;
                default:
                    {
                        g.DrawString(columnSource._text, __newFont, __cellBrush, position);
                    }
                    break;
            }
            __cellBrush.Dispose();
        }

        float _areaColumnDataDraw(Graphics g, _columnDataListType columnSource, Point position, float cellHeight, float spaceBeforeText)
        {
            if (columnSource._objectSource == null)
            {
                return 0f;
            }

            Point __dataPosition = new Point((int)(position.X + (spaceBeforeText * this._drawScale)), position.Y);
            _columnListType __getColumn = ((_columnListType)((_objectListType)columnSource._objectSource)._columnList[columnSource._columnAddr]);
            SolidBrush __cellBrush = new SolidBrush((columnSource._fontColor == null) ? Color.Black : columnSource._fontColor); // มั่ว
            //
            Font __getFont = (columnSource._font == null) ? __getColumn._columnDetailFont : columnSource._font;


            float __columnWidthCalc = ((__getColumn._width - spaceBeforeText));

            if (columnSource._totalColumnWidth != 0)
            {
                int _pageWidth = (int)(((this._pageSetupDialog.PageSettings.Landscape) ? this._pageSetupDialog.PageSettings.PaperSize.Height : this._pageSetupDialog.PageSettings.PaperSize.Width) - (this._pageSetupDialog.PageSettings.Margins.Left + this._pageSetupDialog.PageSettings.Margins.Right));
                // float __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScaleResult) + 2;

                __columnWidthCalc = _pageWidth * (columnSource._totalColumnWidth / 100);
            }


            // toe fix searchwidth
            //ArrayList __getText = MyLib._myUtil._cutString(_graphicSystem, columnSource._text, __getFont, (float)((__getColumn._width - spaceBeforeText)));
            ArrayList __getText = MyLib._myUtil._cutString(_graphicSystem, columnSource._text, __getFont, __columnWidthCalc);

            // toe แก้ เพิ่ม option ปัดบรรทัดใหม่
            if (columnSource._breakLine == true)
            {
                string __getFirstTextTmp = __getText[0].ToString();
                __getText = new ArrayList();
                __getText.Add(__getFirstTextTmp);
                columnSource._text = __getFirstTextTmp;
            }
            //
            Font __newFont = new Font(__getFont.FontFamily, (float)(__getFont.Size * this._drawScale), __getFont.Style);
            // ข้อมูล
            if (__getText.Count <= 1)
            {
                switch (columnSource._alignCell)
                {
                    case _cellAlign.Center:
                        {
                            SizeF __stringText = g.MeasureString(columnSource._text, __newFont);
                            __dataPosition = new Point((int)(__dataPosition.X + ((__getColumn._width / 2) * this._drawScale) - (__stringText.Width / 2)), position.Y);
                            g.DrawString(columnSource._text, __newFont, __cellBrush, __dataPosition);
                        }
                        break;
                    case _cellAlign.Right:
                        {
                            SizeF __stringText = g.MeasureString(columnSource._text, __newFont);
                            // __dataPosition = new Point((int)(__dataPosition.X + (__getColumn._width * this._drawScale) - __stringText.Width), position.Y);
                            __dataPosition = new Point((int)(__dataPosition.X + (__columnWidthCalc * this._drawScale) - __stringText.Width), position.Y);
                            g.DrawString(columnSource._text, __newFont, __cellBrush, __dataPosition);
                        }
                        break;
                    default:
                        {
                            g.DrawString(columnSource._text, __newFont, __cellBrush, __dataPosition);
                        }
                        break;
                }
            }
            else
            {
                // ตัดคำพิมพ์หลายบรรทัด
                int __y = __dataPosition.Y;
                for (int __loop = 0; __loop < __getText.Count; __loop++)
                {
                    g.DrawString(__getText[__loop].ToString(), __newFont, __cellBrush, new Point(__dataPosition.X, __y));
                    SizeF __stringText = g.MeasureString(__getText[__loop].ToString(), __newFont);
                    __y += (int)(__stringText.Height * (_lineSpaceing / 100.0f));
                }
            }
            // ตีกรอบ
            if (__getColumn._cellBorder != _columnBorder.None || columnSource._border != _columnBorder.None)
            {
                Pen __myPen = new Pen(__getColumn._borderColor, 1);
                int __columnWidth = (int)(__getColumn._width * this._drawScale);
                int __columnHeight = (int)(cellHeight * this._drawScale);
                if (__getColumn._cellBorder == _columnBorder.Left || __getColumn._cellBorder == _columnBorder.LeftRight || __getColumn._cellBorder == _columnBorder.LeftBottom || __getColumn._cellBorder == _columnBorder.LeftRightBottom ||
                    columnSource._border == _columnBorder.Left || columnSource._border == _columnBorder.LeftRight || columnSource._border == _columnBorder.LeftBottom || columnSource._border == _columnBorder.LeftRightBottom || columnSource._border == _columnBorder.All)
                {
                    // ตีเส้นซ้าย
                    g.DrawLine(__myPen, position.X, position.Y, position.X, position.Y + __columnHeight);
                }
                if (__getColumn._cellBorder == _columnBorder.Right || __getColumn._cellBorder == _columnBorder.LeftRight || __getColumn._cellBorder == _columnBorder.RightBottom || __getColumn._cellBorder == _columnBorder.LeftRightBottom ||
                    columnSource._border == _columnBorder.Right || columnSource._border == _columnBorder.LeftRight || columnSource._border == _columnBorder.RightBottom || columnSource._border == _columnBorder.LeftRightBottom || columnSource._border == _columnBorder.All)
                {
                    // ตีเส้นขวา
                    g.DrawLine(__myPen, position.X + __columnWidth, position.Y, position.X + __columnWidth, position.Y + __columnHeight);
                }
                if (__getColumn._cellBorder == _columnBorder.Top || __getColumn._cellBorder == _columnBorder.TopBottom ||
                    columnSource._border == _columnBorder.Top || columnSource._border == _columnBorder.TopBottom || columnSource._border == _columnBorder.All)
                {
                    // ตีเส้นบน
                    g.DrawLine(__myPen, position.X, position.Y, position.X + __columnWidth, position.Y);
                }
                if (__getColumn._cellBorder == _columnBorder.Bottom || __getColumn._cellBorder == _columnBorder.LeftBottom || __getColumn._cellBorder == _columnBorder.RightBottom || __getColumn._cellBorder == _columnBorder.LeftRightBottom || __getColumn._cellBorder == _columnBorder.TopBottom ||
                    columnSource._border == _columnBorder.Bottom || columnSource._border == _columnBorder.LeftBottom || columnSource._border == _columnBorder.RightBottom || columnSource._border == _columnBorder.LeftRightBottom || columnSource._border == _columnBorder.TopBottom || columnSource._border == _columnBorder.All)
                {
                    // ตีเส้นล่าง
                    g.DrawLine(__myPen, position.X, position.Y + __columnHeight, position.X + __columnWidth, position.Y + __columnHeight);
                }
                __myPen.Dispose();
            }
            __cellBrush.Dispose();
            return (float)(__getColumn._width * this._drawScale); // toe floor ให้เส้นตรงกัน
            //return (float)Math.Floor((__getColumn._width * this._drawScale));
        }

        public delegate void beforePaperDrawReport(_pageListType pageListType);
        public event beforePaperDrawReport _beforePaperDrawReportArgs;


        public float _drawReport(int pageNumber, Graphics g)
        {
            float __y = 0;
            if (_pageList.Count > 0)
            {
                _pageListType __getPage = (_pageListType)_pageList[pageNumber];

                if (_beforePaperDrawReportArgs != null)
                {
                    _beforePaperDrawReportArgs(__getPage);
                }

                // before draw report event
                for (int __loop = 0; __loop < __getPage._objectList.Count; __loop++)
                {

                    _objectListType __getObject = (_objectListType)__getPage._objectList[__loop];

                    // before draw row 
                    float __leftMarginMain = this._drawScale * (((__getObject._leftMarginByPersent == false) ? __getObject._leftMargin : ((__getObject._leftMargin == 0) ? 0 : ((__getObject._leftMargin * __getObject._size.Width) / 100))));
                    float __x = ((float)__getObject._position.X) + __leftMarginMain;
                    if (__getObject._columnList.Count > 0)
                    {
                        for (int __column = 0; __column < __getObject._columnList.Count; __column++)
                        {
                            if (__getObject._columnList[__column].GetType() == typeof(_columnListType))
                            {
                                _columnListType __getColumn = (_columnListType)__getObject._columnList[__column];
                                SolidBrush __cellBrush = new SolidBrush(__getColumn._columnDetailColor);
                                if (__getColumn._text != null && __getColumn._showColumnName == true)
                                {
                                    // แสดงชื่อ Column
                                    Point __columnPosition = new Point((int)(__x + this._leftMargin), (int)(__y + this._topMargin));
                                    _areaColumnDraw(g, __getColumn, __columnPosition);
                                    __x += (__getColumn._width * this._drawScale); //toe ใส่ floor เพื่อให้ปัดให้ตรงกัน
                                    //__x += (float)Math.Floor((__getColumn._width * this._drawScale));
                                }
                                for (int __cell = 0; __cell < __getColumn._cellList.Count; __cell++)
                                {
                                    _cellListType __getCell = (_cellListType)__getColumn._cellList[__cell];
                                    string __getString = _processValue(__getCell._text);
                                    SizeF __stringSize = g.MeasureString(__getString, __getCell._font);
                                    __getCell._height = (__stringSize.Height == 0) ? ((__getColumn._columnHeadFont == null) ? 0 : __getColumn._columnHeadFont.Height) : __stringSize.Height;
                                    __getCell._width = __stringSize.Width;
                                    // Left                                   
                                    Point __cellPosition = new Point((int)this._leftMargin + (int)(__getColumn._position.X * this._drawScale), (int)(((__getObject._position.Y + __getCell._position.Y) * this._drawScale) + this._topMargin));
                                    switch (__getCell._alignCell)
                                    {
                                        case _cellAlign.Right:
                                            {
                                                // Right
                                                __cellPosition = new Point((int)(((__getColumn._width - __getCell._width) * this._drawScale) + this._leftMargin + (int)(__getColumn._position.X * this._drawScale)), (int)(((__getObject._position.Y + __getCell._position.Y) * this._drawScale) + this._topMargin));
                                            }
                                            break;
                                        case _cellAlign.Center:
                                            {
                                                // Center
                                                __cellPosition = new Point((int)((((__getColumn._width / 2) - (__getCell._width / 2)) * this._drawScale) + this._leftMargin + (int)(__getColumn._position.X * this._drawScale)), (int)(((__getObject._position.Y + __getCell._position.Y) * this._drawScale) + this._topMargin));
                                            }
                                            break;
                                    }
                                    Font __newFont = new Font(__getCell._font.FontFamily, (float)(__getCell._font.Size * this._drawScale), __getCell._font.Style);
                                    g.DrawString(__getString, __newFont, __cellBrush, __cellPosition);
                                }
                            }
                            else
                                if (__getObject._columnList[__column].GetType() == typeof(_columnDataListType))
                            {
                                _columnDataListType __getData = (_columnDataListType)__getObject._columnList[__column];
                                //if (__getData._text != null)
                                {
                                    // แสดง Data
                                    try
                                    {
                                        if (__getData._isHide == false)
                                        {
                                            Point __columnPosition = new Point((int)(__x + this._leftMargin), (int)(__y + this._topMargin));
                                            __x += _areaColumnDataDraw(g, __getData, __columnPosition, __getObject._size.Height, __getData._spaceBeforeText);
                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }

                                /*if (__getData._border != _columnBorder.None)
                                {
                                    // บน
                                    if (__getData._border == _columnBorder.Top || __getData._border == _columnBorder.TopBottom || __getData._border == _columnBorder.All)
                                    {
                                        Pen __objectPen = new Pen(Color.Black, 1);
                                        g.DrawLine(__objectPen, (int)(__x + this._leftMargin), (int)(__y + this._topMargin), (int)(__x + this._leftMargin ), (int)(__y + this._topMargin));
                                        __objectPen.Dispose();

                                    }

                                    // ขวา
                                    if (__getData._border == _columnBorder.LeftRight || __getData._border == _columnBorder.LeftRightBottom || __getData._border == _columnBorder.Right || __getData._border == _columnBorder.RightBottom || __getData._border == _columnBorder.All)
                                    {

                                    }

                                    // ล่าง
                                    if (__getData._border == _columnBorder.Bottom || __getData._border == _columnBorder.LeftBottom || __getData._border == _columnBorder.LeftRightBottom || __getData._border == _columnBorder.RightBottom || __getData._border == _columnBorder.TopBottom || __getData._border == _columnBorder.All)
                                    {

                                    }

                                    // ซ้าย
                                    if (__getData._border == _columnBorder.Left || __getData._border == _columnBorder.LeftBottom || __getData._border == _columnBorder.LeftRight || __getData._border == _columnBorder.LeftRightBottom || __getData._border == _columnBorder.All)
                                    {

                                    }

                                }*/
                            }
                        }
                    }
                    if (__getObject._border != _columnBorder.None)
                    {
                        // ตีกรอบ
                        if (__getObject._border == _columnBorder.Top || __getObject._border == _columnBorder.TopBottom || __getObject._border == _columnBorder.All)
                        {
                            Pen __objectPen = new Pen(Color.Black, 1);
                            g.DrawLine(__objectPen, this._leftMargin + ((float)__getObject._position.X * this._drawScale), this._topMargin + ((float)__getObject._position.Y * this._drawScale), this._leftMargin + ((float)__getObject._size.Width * this._drawScale), this._topMargin + ((float)__getObject._position.Y * this._drawScale));
                            __objectPen.Dispose();
                        }
                        if (__getObject._border == _columnBorder.Bottom || __getObject._border == _columnBorder.TopBottom || __getObject._border == _columnBorder.All)
                        {
                            Pen __objectPen = new Pen(Color.Black, 1);
                            g.DrawLine(__objectPen, this._leftMargin + ((float)__getObject._position.X * this._drawScale), this._topMargin + ((float)__getObject._position.Y * this._drawScale) + ((float)__getObject._size.Height * this._drawScale), this._leftMargin + (((float)__getObject._size.Width * this._drawScale)), this._topMargin + ((float)__getObject._position.Y * this._drawScale) + ((float)__getObject._size.Height * this._drawScale));
                            __objectPen.Dispose();
                        }
                    }
                    __y += ((float)__getObject._size.Height * this._drawScale);

                    // after draw row 
                }
                // ตีเส้นล่าง (หน้ากระดาษ)

                Pen __myPen = new Pen(Color.Black, 1);

                // toe check have footer draw before footer page

                int __calcHeight = (int)((float)__getPage._pageSize.Height * this._drawScale);
                if (__getPage._haveFooterPage)
                {
                    __calcHeight = (int)((float)__getPage._paperPageSize.Height * this._drawScale);
                }

                if (_showLineLastPage)
                {
                    if (pageNumber == this._pageMax - 1)
                        g.DrawLine(__myPen, this._leftMargin, this._topMargin + __calcHeight, this._leftMargin + ((float)__getPage._pageSize.Width * this._drawScale), this._topMargin + __calcHeight);
                }
                else
                {
                    g.DrawLine(__myPen, this._leftMargin, this._topMargin + __calcHeight, this._leftMargin + ((float)__getPage._pageSize.Width * this._drawScale), this._topMargin + __calcHeight);
                }
                __myPen.Dispose();
            }

            return __y;
        }

        void _area_Paint(object sender, PaintEventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (this._pageCurrent >= this._pageMax)
                {
                    this._pageCurrent = 0;
                }
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                // วาดรายงาน
                float __y = _drawReport(this._pageCurrent, g);

                _drawFooter(this._pageCurrent, g, __y);
            }
        }

        public void _drawFooter(int page, Graphics g, float y)
        {
            // draw footr report custome
            if (_drawFooterReportEvent != null)
            {
                _drawFooterReportEvent(this._pageCurrent, g, y, this._drawScale);
            }

        }

        void _area_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseMove != null)
            {
                _mouseMove(this.PointToClient(Control.MousePosition));
            }
        }

        public event MouseMoveEventHandler _mouseMove;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_mouseMove != null)
            {
                _mouseMove(this.PointToClient(Control.MousePosition));
            }
            base.OnMouseMove(e);
        }

        public Point _topLeftPaper
        {
            get
            {
                if (MyLib._myGlobal._isDesignMode || this._pageSetupDialog == null)
                {
                    return new Point(0, 0);
                }
                Size __getPaperSizeByPixel = (this._pageSetupDialog.PageSettings.Landscape) ? new Size(this._pageSetupDialog.PageSettings.PaperSize.Height, this._pageSetupDialog.PageSettings.PaperSize.Width) : new Size(this._pageSetupDialog.PageSettings.PaperSize.Width, this._pageSetupDialog.PageSettings.PaperSize.Height);
                int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScaleResult) + 2;
                int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScaleResult) + 2;
                Point __tempPoint = new Point((this.Width / 2) - (__calcWidth / 2), (this.Height / 2) - (__calcHeight / 2));
                if (__tempPoint.Y < 20)
                {
                    __tempPoint = new Point(__tempPoint.X, 20);
                }
                if (__tempPoint.X < 20)
                {
                    __tempPoint = new Point(20, __tempPoint.Y);
                }
                _topLeftPaperResult = __tempPoint;
                return _topLeftPaperResult;
            }
            set
            {
                _topLeftPaperResult = value;
            }
        }

        public float _drawScale
        {
            get
            {
                return _drawScaleResult;
            }
            set
            {
                _drawScaleResult = value;
                //_area._drawScale = _drawScaleResult;
            }
        }

        void _drawPaper_Paint(object sender, PaintEventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                try
                {
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    Size __getPaperSizeByPixel = (this._pageSetupDialog.PageSettings.Landscape) ? new Size(this._pageSetupDialog.PageSettings.PaperSize.Height, this._pageSetupDialog.PageSettings.PaperSize.Width) : new Size(this._pageSetupDialog.PageSettings.PaperSize.Width, this._pageSetupDialog.PageSettings.PaperSize.Height);
                    int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScaleResult) + 2;
                    int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScaleResult) + 2;
                    _area.Location = new Point(_topLeftPaper.X + 1, _topLeftPaper.Y + 1);
                    _area.Size = new Size(__calcWidth - 2, __calcHeight - 2);
                    Pen __pen = new Pen(Color.Black, 1);
                    SolidBrush __brush = new SolidBrush(Color.White);
                    Color __colorBegin = Color.Silver;
                    Color __colorEnd = Color.DarkGray;
                    int __calcGrap = (int)(5f * _drawScaleResult);
                    Rectangle __box = new Rectangle(_topLeftPaper.X, _topLeftPaper.Y, __calcWidth, __calcHeight);
                    LinearGradientBrush __brushRightShadow = new LinearGradientBrush(new Point(_topLeftPaper.X + __calcWidth + (__calcGrap * 2), _topLeftPaper.X + __calcGrap), new Point(_topLeftPaper.X + __calcWidth, _topLeftPaper.X + __calcGrap), __colorBegin, __colorEnd);
                    LinearGradientBrush __brushBottomShadow = new LinearGradientBrush(new Point(_topLeftPaper.Y + __calcGrap, _topLeftPaper.Y + __calcHeight + (__calcGrap * 2)), new Point(_topLeftPaper.Y + __calcGrap, _topLeftPaper.Y + __calcHeight), __colorBegin, __colorEnd);
                    Point[] __rightShadow = {
                    new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcGrap),
                    new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcHeight),
                    new Point(_topLeftPaper.X+__calcWidth+(__calcGrap*2),_topLeftPaper.Y+__calcHeight + (__calcGrap * 2)),
                    new Point(_topLeftPaper.X+__calcWidth+(__calcGrap*2),_topLeftPaper.Y+__calcGrap),
                    new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcGrap)
                };
                    Point[] __bottomShadow = {
                    new Point(_topLeftPaper.X+__calcGrap,_topLeftPaper.Y+__calcHeight),
                    new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcHeight),
                    new Point(_topLeftPaper.X+__calcWidth+(__calcGrap*2),_topLeftPaper.Y+__calcHeight+(__calcGrap*2)),
                    new Point(_topLeftPaper.X+__calcGrap,_topLeftPaper.Y+__calcHeight+(__calcGrap*2)),
                    new Point(_topLeftPaper.X+__calcGrap,_topLeftPaper.Y+__calcHeight)
                };
                    g.FillPolygon(__brushRightShadow, __rightShadow);
                    g.FillPolygon(__brushBottomShadow, __bottomShadow);
                    g.FillRectangle(__brush, __box);
                    g.DrawRectangle(__pen, __box);
                }
                catch
                {
                }
            }
        }
    }
    public delegate void MouseMoveEventHandler(Point _mousePosition);
}
