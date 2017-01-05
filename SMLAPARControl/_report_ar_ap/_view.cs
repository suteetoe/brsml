using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Collections;
using System.Drawing.Drawing2D;

namespace SMLERPAPARControl._report_ar_ap
{
    public partial class _view : Form
    {
        DateTime _start;

        Font __printFontReportName = new Font("Tahoma", 16, FontStyle.Bold);
        Font __printFontTitle = new Font("Tahoma", 12, FontStyle.Bold);
        Font __printFontTitleDetail = new Font("Tahoma", 8, FontStyle.Bold);
        Font __printFontDetail = new Font("Tahoma", 8, FontStyle.Regular);

        private float _drawScaleResult;
        public Point _topLeftPaperResult = new Point(20, 20);
        //       
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

        float _line = 0;
        string[] _filed_name;
        string[] _filed_type;
        string[] _filed_width;

        string _scree_name;
        MyLib._myGrid _grid;

        public MyLib._myGrid Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }

        public string scree_name
        {
            get { return _scree_name; }
            set { _scree_name = value; }
        }

        public _view()
        {
            InitializeComponent();
            
            this._preview._btnCancel.Click += new EventHandler(_btnCancel_Click);
            
        }

        public void __loadDefaul(string __screen)
        {

            if (__screen.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล.ToString()))
            {

                string[] _s = { "ลำดับที่", "เลขที่ใบเสร็จ", "วันที่", "ครบกำหนด", "จำนวนเงิน", "ยอดคงค้าง", "ยอดชำระ" };
                string[] _d = { "50", "100", "100", "100", "100", "100", "100" };
                string[] _f = { "1", "1", "2", "2", "3", "3", "3" };
                this._filed_name = (_s);
                this._filed_width = _d;
                this._filed_type = _f;
                Util._reportname = "ใบรับวางบิล(AP)";
                _scree_name = __screen;
                this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(printDocument1_BeginPrint);
                this.printDocument1.EndPrint += new PrintEventHandler(printDocument1_EndPrint);
                this.printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                this._preview.Document = this.printDocument1;
            }

        }

        void _btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle rc = e.MarginBounds;
            rc.Height = 20;

            Pen __objectPen = new Pen(Color.Black, 1);
            float __getWidth = e.MarginBounds.Width;
            float __getHeight = e.MarginBounds.Height;

            Size __getPaperSizeByPixel = (e.PageSettings.Landscape) ? new Size(e.PageSettings.PaperSize.Height, e.PageSettings.PaperSize.Width) : new Size(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
            int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScaleResult) + 2;
            int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScaleResult) + 2;

            float __getLeft = 30;
            float __getTop = 30;
            int __xxWidth = (int)__getPaperSizeByPixel.Width;
            int __xHeight = (int)__getPaperSizeByPixel.Height;
            __printHeadBox(e.Graphics, __getLeft, __getTop, (float)__xxWidth, (float)__xHeight, "tax_invoice");
            __printTextHeadCompany(e.Graphics, __getLeft, __getTop, (float)__xxWidth, (float)__xHeight);
            __printTextHeadDetail(e.Graphics, __getLeft, __getTop, (float)__xxWidth, (float)__xHeight, string.Empty);

            float lineHeight = __printFontDetail.GetHeight(e.Graphics);
            float height = lineHeight;
            float lineLeft = 0;
            _line += 10;
            Util.noOfItems = Grid._rowData.Count;

            while ((Util.currentItem < Util.noOfItems)
                && (_line < 900))
            {
                _line += __printFontDetail.GetHeight(e.Graphics);
                lineLeft = 0;
                for (int c = 0; c < this._filed_name.Length; c++)
                {
                    lineLeft += Util.translateInt(this._filed_width[c].ToString());
                    bool rightAlign = (this._filed_type[c].Equals("3") ? true : false);
                    float maxWidth = float.Parse(this._filed_width[c].ToString());
                    height = Util.printLine(e.Graphics
                                        , Grid._cellGet(Util.currentItem,c).ToString()
                                        , __printFontDetail
                                        , lineLeft
                                        , _line
                                        , maxWidth - 10
                                        , rightAlign
                                        , Brushes.Black);
                }
                Util.currentItem++;
                e.HasMorePages = (Util.currentItem < Util.noOfItems);
            }

            StringFormat theFormat = new StringFormat();
            theFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            RectangleF rect = new RectangleF(30, 300, _line, lineHeight);
            height = Util.printLine(e.Graphics
                                        , "จำนวนเงินทั้งสิ้น :"
                                        , __printFontTitle
                                        , 530
                                        , 940
                                        , 100
                                        , false
                                        , Brushes.Black);
            height = Util.printLine(e.Graphics
                                         , "123456.789"
                                         , __printFontTitle
                                         , 630
                                         , 940
                                         , 150
                                         , true
                                         , Brushes.Black);

            _line = height + 940;
            lineLeft = 30;
            height = Util.printLine(e.Graphics
                                       , "การชำระเงินด้วยเช็คจะสมบูรณ์เมือบริษัทได้รับเงินตามเช็คเรียบร้อย"
                                       , __printFontDetail
                                       , lineLeft
                                       , _line
                                       , __xxWidth
                                       , false
                                       , Brushes.Black);
            _line += height;
            height = Util.printLine(e.Graphics
                                       , "เงินสด................................บาท"
                                       , __printFontDetail
                                       , lineLeft
                                       , _line
                                       , __xxWidth
                                       , false
                                       , Brushes.Black);
            _line += height;
            height = Util.printLine(e.Graphics
                                       , "เช็คธนาคาร................................เลขที่.........................................ลงวันที่..............................จำนวนเงิน.................."
                                       , __printFontDetail
                                       , lineLeft
                                       , _line
                                       , __xxWidth
                                       , false
                                       , Brushes.Black);
            _line += height;
            height = Util.printLine(e.Graphics
                                       , "เช็คธนาคาร................................เลขที่.........................................ลงวันที่..............................จำนวนเงิน.................."
                                       , __printFontDetail
                                       , lineLeft
                                       , _line
                                       , __xxWidth
                                       , false
                                       , Brushes.Black);

            _line += height + 30;
            height = Util.printLine(e.Graphics
                                        , "ผู้รับเงิน................................ลงวันที่..............................จำนวนเงิน.................."
                                        , __printFontDetail
                                        , lineLeft
                                        , _line
                                        , __xxWidth
                                        , false
                                        , Brushes.Black);
            _line += height;
            height = Util.printLine(e.Graphics
                                        , "ผู้รับมอบอำนาจ................................ลงวันที่.............................."
                                        , __printFontDetail
                                        , lineLeft
                                        , _line
                                        , __xxWidth
                                        , false
                                        , Brushes.Black);

        }

        

        void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            e.Cancel = true;
        }

        void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            PrintDocument __printDocument = (PrintDocument)sender;
            __printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4 (210 x 297 mm)", 827, 1169);
            __printDocument.DefaultPageSettings.Margins = new Margins(20, 20, 20, 20);
            __printDocument.DefaultPageSettings.Landscape = false;
        }

        public void DrawRoundRect(Graphics g, Pen p, float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y); // Line   
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90); // Corner   
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2)); // Line   
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Corner   
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height); // Line  
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90); // Corner  
            gp.AddLine(x, y + height - (radius * 2), x, y + radius); // Line  
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90); // Corner  
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        /// <param name="_stly">1 ไม่มีกรอบ 2,มีกรอบ,3 ทั่วไป</param>

        void __printHeadBox(Graphics e, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight, string _stly)
        {
            try
            {
                Pen __objectPen = new Pen(Color.Black, 1);

                ///
                // _stly = 1 ไม่มีกรอบ  
                if (_stly.Equals("1"))
                {
                    e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__pageWidth - 252, (int)__pageTop, 252, 40);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    float x = (__pageWidth / 4);
                    e.FillRectangle(linearBrushHead, (x * 3) - (x / 2), (int)__pageTop, 280, 40);
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 150;
                    float __detailWidth = __pageWidth - (__pageLeft * 2);
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 30, __detailWidth + __pageLeft, __detailTop + 30);
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft + 2, (int)__detailTop + 1, (int)__detailWidth - 3, 28);
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = __pageLeft + (__pageWidth / 2) + 80;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = (__pageWidth - __buttomBoxWidth) - (__pageLeft * 2);
                    e.DrawRectangle(__objectPen, (x * 3), __buttomBox1Top, 180, 30);

                }
                /// แบบที่ 2 มีกรอบ
                if (_scree_name.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล.ToString()))
                {
                    e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__pageWidth - 250, (int)__pageTop, 250, 40);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    float x = (__pageWidth / 4);
                    e.FillRectangle(linearBrushHead, (x * 3) - (x / 2), (int)__pageTop, 280, 40);

                    ///
                    //กรอบที่ 1 บน
                    DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 65, (__pageWidth / 2) + 60, 85, 5);
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + (__pageWidth / 2) + 60;
                    float __box2Width = (__pageWidth - __box2Left) - __pageLeft;
                    DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 65, __box2Width, 85, 5); //กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 155;
                    float __detailWidth = __pageWidth - (__pageLeft * 2);
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 35, __detailWidth + __pageLeft, __detailTop + 35);
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 5, (int)__detailWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft + 2, (int)__detailTop + 5, (int)__detailWidth - 3, 28);
                    //กรอบล่างซ้าย
                    //float __buttomBox1Top = ((__detailHeight + 3) + __detailTop);
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop + 5);
                    float __buttomBoxWidth = __pageLeft + (__pageWidth / 2) + 40;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 180;
                    DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8);
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = (__pageWidth - __buttomBoxWidth) - (__pageLeft * 2);
                    DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8);
                    //กรอบล่างสุด
                    //float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    //float __footWidth = __pageWidth - (__pageLeft * 2);
                    //float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight) - 30;
                    //DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __footWidth, __footHeight, 8);
                }
                else
                {
                    e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__pageWidth - 250, (int)__pageTop, 250, 40);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    float x = (__pageWidth / 4);
                    e.FillRectangle(linearBrushHead, (x * 3) - (x / 2), (int)__pageTop, 280, 40);

                    ///
                    //กรอบที่ 1 บน
                    DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 100, (__pageWidth / 2) + 50, 150, 8);
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + (__pageWidth / 2) + 60;
                    float __box2Width = (__pageWidth - __box2Left) - __pageLeft;
                    DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 100, __box2Width, 150, 8); //กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailWidth = __pageWidth - (__pageLeft * 2);
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    //กรอบล่างซ้าย
                    //float __buttomBox1Top = ((__detailHeight + 3) + __detailTop);
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = __pageLeft + (__pageWidth / 2) + 80;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8);
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = (__pageWidth - __buttomBoxWidth) - (__pageLeft * 2);
                    DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8);
                    //กรอบล่างสุด
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footWidth = __pageWidth - (__pageLeft * 2);
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight) - 30;
                    DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __footWidth, __footHeight, 8);
                }

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// หัวกระดาษ บริษัท
        /// </summary>
        /// <param name="e"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>

        void __printTextHeadCompany(Graphics e, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            float lineHeightReport = __printFontReportName.GetHeight(e);
            StringFormat theFormat = new StringFormat();
            theFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
            float lineHeight = __printFontTitle.GetHeight(e);
            _line = __pageTop;
            RectangleF rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(MyLib._myGlobal._ltdName, __printFontTitle, Brushes.Black, rect, theFormat);
            float lineHeightf = __printFontTitleDetail.GetHeight(e);
            _line += lineHeightf;
            rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(MyLib._myGlobal._ltdAddress, __printFontTitleDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontTitleDetail.GetHeight(e);
            _line += lineHeightf;
            rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(Util._address_x, __printFontTitleDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontTitleDetail.GetHeight(e);
            _line += lineHeightf;
            rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString("Tel "+MyLib._myGlobal._ltdTel + " Fax " + MyLib._myGlobal._ltdFax, __printFontDetail, Brushes.Black, rect, theFormat);
            float x = (__pageWidth / 4);
            rect = new RectangleF((x * 3) - (x / 2), _line, (__pageWidth / 2), lineHeight);
            e.DrawString("Tax No. " + MyLib._myGlobal._ltdTax, __printFontTitleDetail, Brushes.Black, rect, theFormat);
            rect = new RectangleF((x * 3) - (x / 2), __pageTop + 8, ((x * 3) - (x / 2)), lineHeightReport);
            e.DrawString(Util._reportname, __printFontReportName, Brushes.Black, rect, theFormat);
        }

        /*yLib._myGlobal._ltdName = (_row["company_name_1"].ToString());
                    MyLib._myGlobal._ltdAddress = (str);
                    MyLib._myGlobal._ltdTel = (_row["telephone_number"].ToString());
                    MyLib._myGlobal._ltdFax = (_row["fax_number"].ToString());*/
        /// <summary>
        /// รายละเอียดหัวกระดาษ
        /// </summary>
        /// <param name="e"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        /// <param name="type"></param>
        void __printTextHeadDetail(Graphics e, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight, string type)
        {
            float lineHeightf = __printFontTitle.GetHeight(e);
            _line += 30;
            StringFormat theFormat = new StringFormat();
            theFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
            float lineHeight = __printFontTitle.GetHeight(e);
            RectangleF rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString("ลูกค้า ", __printFontTitleDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontTitleDetail.GetHeight(e);
            rect = new RectangleF(__pageLeft + 50, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(" : " + Util._ap_ar_code, __printFontDetail, Brushes.Black, rect, theFormat);
            //-----------------------------------
            lineHeightf = __printFontDetail.GetHeight(e);
            _line += lineHeightf;
            rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(Util._ap_ar_name, __printFontDetail, Brushes.Black, rect, theFormat);


            float x = (__pageWidth / 4);
            rect = new RectangleF((x * 3) - (x / 2), _line, (__pageWidth / 2), lineHeight);
            e.DrawString("เลขที่ใบเสร็จ", __printFontTitleDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontTitleDetail.GetHeight(e);
            rect = new RectangleF((x * 3) - (x / 2) + 60, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(" : " + Util._ap_ar_doc_no, __printFontDetail, Brushes.Black, rect, theFormat);

            //-------------------------
            lineHeightf = __printFontDetail.GetHeight(e);
            _line += lineHeightf;
            rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(Util._ap_ar_address, __printFontDetail, Brushes.Black, rect, theFormat);
            rect = new RectangleF((x * 3) - (x / 2), _line, (__pageWidth / 2), lineHeight);
            e.DrawString("วันที่ ", __printFontTitleDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontTitleDetail.GetHeight(e);
            rect = new RectangleF((x * 3) - (x / 2) + 60, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(" : " + Util._ap_ar_doc_date, __printFontDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontDetail.GetHeight(e);
            _line += lineHeightf;
            rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(Util._ap_ar_address_x, __printFontDetail, Brushes.Black, rect, theFormat);
            rect = new RectangleF((x * 3) - (x / 2), _line, (__pageWidth / 2), lineHeight);
            e.DrawString("พนักงานขาย", __printFontTitleDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontTitleDetail.GetHeight(e);
            rect = new RectangleF((x * 3) - (x / 2) + 60, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(" : " + Util._ap_ar_sale_code, __printFontDetail, Brushes.Black, rect, theFormat);
            lineHeightf = __printFontDetail.GetHeight(e);
            _line += lineHeightf;
            rect = new RectangleF(__pageLeft, _line, (__pageWidth / 2), lineHeight);
            e.DrawString(Util._ap_ar_address_t, __printFontDetail, Brushes.Black, rect, theFormat);


            float height = lineHeightf;
            float lineLeft = 0;
            _line += 30;
            if (this._filed_name.Length > 0)
            {
                for (int i = 0; i < this._filed_name.Length; i++)
                {
                    float maxWidth = float.Parse(this._filed_width[i].ToString());
                    lineLeft += Util.translateInt(this._filed_width[i].ToString());
                    height = Util.printLine(e
                                        , this._filed_name[i].ToString()
                                        , __printFontDetail
                                        , lineLeft + 20
                                        , _line
                                        , maxWidth + 20
                                        , false
                                        , Brushes.Black);
                }
            }

        }
    }
}
