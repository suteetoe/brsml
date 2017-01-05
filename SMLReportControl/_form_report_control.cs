using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _form_report_control : Form
    {
        PrintDocument __printdocReport;
        public string __pageEnum = "";
        public string __pageEnumString = "";
        public ArrayList __pointArray = new ArrayList();
        private float _drawScaleResult;
        int _rowCount = 0;
        public Font _fontStandard = new Font("Angsana New", 12, FontStyle.Regular);
        public Font _fontHeaderNormal12 = new Font("Angsana New", 12, FontStyle.Regular);
        public Font _fontHeaderBold16 = new Font("Angsana New", 18, FontStyle.Bold);
        public Font _fontHeaderNormal14 = new Font("Angsana New", 14, FontStyle.Regular);
        public Font _fontHeaderBold14 = new Font("Angsana New", 14, FontStyle.Bold);
        public _form_report_control()
        {
            InitializeComponent();
            this.Load += new EventHandler(_form_report_control_Load);
        }

        void _form_report_control_Load(object sender, EventArgs e)
        {
            __build(__pageEnumString);
        }

        public void __build(string __page)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                __printdocReport = new PrintDocument();
                __printdocReport.BeginPrint += new PrintEventHandler(__printdocReport_BeginPrint);
                __printdocReport.EndPrint += new PrintEventHandler(__printdocReport_EndPrint);
                __printdocReport.PrintPage += new PrintPageEventHandler(__printdocReport_PrintPage);
                //__printdocReport.Print();             
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
            }
        }

        void __printdocReport_BeginPrint(object sender, PrintEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PrintDocument __printDocument = (PrintDocument)sender;
            __printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 840, 1180);
            __printDocument.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);
            __printDocument.DefaultPageSettings.Landscape = false;
            //printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Letter", 850, 1100);
           // printDocument1.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);
           // printDocument1.DefaultPageSettings.Landscape = false;
        }

        void __printdocReport_EndPrint(object sender, PrintEventArgs e)
        {
            // this._rowCount = 0;
            // this._pageNumber = 0;
            PrintDocument __sender = (PrintDocument)sender;
        }

        void __printdocReport_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                PrintDocument __printDocument = (PrintDocument)sender;
                Pen __objectPen = new Pen(Color.Black, 1);
                //Left
                StringFormat sfLeft = new StringFormat();
                sfLeft.Alignment = StringAlignment.Near;
                //Center
                StringFormat sfCenter = new StringFormat();
                sfCenter.Alignment = StringAlignment.Center;
                //Right
                StringFormat sfRight = new StringFormat();
                sfRight.Alignment = StringAlignment.Far;
                // fill page with text                   
                float __getWidth = e.MarginBounds.Width;
                float __getHeight = e.MarginBounds.Height;

                Size __getPaperSizeByPixel = (e.PageSettings.Landscape) ? new Size(e.PageSettings.PaperSize.Height, e.PageSettings.PaperSize.Width) : new Size(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
                int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScaleResult) + 2;
                int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScaleResult) + 2;
                if (__pageEnum.Equals(_gForm._formEnum.ใบเสร็จรับเงิน))
                {
                    __printHeadBox(e.Graphics, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
                    __printDataReportReceipt(e.Graphics, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
                    float __detailY = e.MarginBounds.Top + 310;
                    float __detailX = e.MarginBounds.Left;
                    int __no = 0;
                    float __detailWidth = 0;
                    float __fontHeight = _fontHeaderNormal12.GetHeight(e.Graphics);
                    for (int i = _rowCount; i < 100; i++)
                    {
                        __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                        float __detailtitleWidth = ((__getWidth * 15) / 100);
                        float __detailtitleLeft = __detailX;
                        RectangleF rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                        e.Graphics.DrawString("ICI-" + i.ToString(), _fontHeaderNormal14, Brushes.Black, rectDetail, sfLeft);
                        __detailtitleLeft = __detailtitleLeft + ((__getWidth * 15) / 100);
                        __detailtitleWidth = ((__getWidth * 32) / 100);
                        rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                        e.Graphics.DrawString("ICI-" + i.ToString(), _fontHeaderNormal14, Brushes.Black, rectDetail, sfLeft);
                        __detailtitleLeft = __detailtitleLeft + ((__getWidth * 32) / 100);
                        __detailtitleWidth = ((__getWidth * 10) / 100);
                        rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                        e.Graphics.DrawString("10.00", _fontHeaderNormal14, Brushes.Black, rectDetail, sfRight);
                        __detailtitleLeft = __detailtitleLeft + ((__getWidth * 10) / 100);
                        __detailtitleWidth = ((__getWidth * 15) / 100);
                        rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                        e.Graphics.DrawString("2,600.00", _fontHeaderNormal14, Brushes.Black, rectDetail, sfRight);
                        __detailtitleLeft = __detailtitleLeft + ((__getWidth * 15) / 100);
                        __detailtitleWidth = ((__getWidth * 12) / 100);
                        rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                        e.Graphics.DrawString("5.00", _fontHeaderNormal14, Brushes.Black, rectDetail, sfRight);
                        __detailtitleLeft = __detailtitleLeft + ((__getWidth * 12) / 100);
                        __detailtitleWidth = ((__getWidth * 16) / 100);
                        rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                        e.Graphics.DrawString("26,000.00", _fontHeaderNormal14, Brushes.Black, rectDetail, sfRight);

                        if (__no > 14)
                        {
                            __no = 0;
                            e.HasMorePages = true;
                            break;
                        }
                        __no++;
                        _rowCount++;
                    }

                }                              
            }
            catch (Exception ex)
            {
            }
        }

        void __printDataReport(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            try
            {
            }
            catch (Exception ex)
            {
            }
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

        void __printHeadBox(Graphics e, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            try
            {
                Pen __objectPen = new Pen(Color.Black, 1);
                if (__pageEnum.Equals(_gForm._formEnum.ใบเสนอราคา))
                {
                    e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__pageWidth - 180, (int)__pageTop, 150, 40);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    e.FillRectangle(linearBrushHead, (int)__pageWidth - 180, (int)__pageTop, 150, 40);
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
                    float __buttomBoxWidth = __pageLeft + (__pageWidth / 2) + 50;
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
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__pageWidth * 12) / 100);
                    float __xline2 = __xline1 + __pageLeft + ((__pageWidth * 30) / 100);
                    float __xline3 = __xline2 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline4 = __xline3 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline5 = __xline4 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline6 = __xline5 + __pageLeft + ((__pageWidth * 6) / 100);
                    e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                    e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                    e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                    e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                    e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                    e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    float __footerylineTop = __buttomBoxHeight + (__detailTop + __detailHeight) + 3;
                    float __footerylineEnd = __footerylineTop + (__pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight) - 30);
                    float __footerxline1 = __pageLeft + ((__pageWidth * 50) / 100);
                    e.DrawLine(__objectPen, __footerxline1, __footerylineTop, __footerxline1, __footerylineEnd);
                }
                if (__pageEnum.Equals(_gForm._formEnum.ใบเสร็จรับเงิน))
                {
                    float __titleBoxLeft = __pageWidth - 250;
                    float __titleBoxWidth = __titleBoxLeft + 220;
                    e.DrawRectangle(__objectPen, __titleBoxLeft, __pageTop + 60, 220, 60);
                    e.DrawLine(__objectPen, __titleBoxLeft, __pageTop + 90, __titleBoxWidth, __pageTop + 90);
                    float __titleLineLeft1 = __pageWidth - 180;
                    e.DrawLine(__objectPen, __titleLineLeft1, __pageTop + 60, __titleLineLeft1, __pageTop + 120);
                    float __xWidth = __pageWidth;
                    e.DrawRectangle(__objectPen, __pageLeft, __pageTop + 200, __xWidth, 80);
                    ///เส้นแนวตั้งกรอบบนสุด
                    float __xlineLeft1 = __pageLeft + ((__pageWidth * 26) / 100);
                    float __ylineTop1 = __pageTop + 200;
                    float __ylineEnd1 = __pageTop + 200 + 80;
                    e.DrawLine(__objectPen, __xlineLeft1, __ylineTop1, __xlineLeft1, __ylineEnd1);
                    float __xlineLeft2 = __xlineLeft1 + ((__pageWidth * 13) / 100);
                    e.DrawLine(__objectPen, __xlineLeft2, __ylineTop1, __xlineLeft2, __ylineEnd1);
                    float __xlineLeft3 = __xlineLeft2 + ((__pageWidth * 13) / 100);
                    e.DrawLine(__objectPen, __xlineLeft3, __ylineTop1, __xlineLeft3, __ylineEnd1);
                    //-------------------------------------------------------------------------------------
                    float __detailTop = __pageTop + 203 + 80;
                    float __detailheight = __pageHeight - (__detailTop);
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailheight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                    //////เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__xWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48);
                    float __footterTopLine = __detailTop + (__detailheight / 2) + 50;
                    e.DrawLine(__objectPen, __pageLeft, __footterTopLine, __xWidth + __pageLeft, __footterTopLine);
                    ///เส้นแนวตั้ง
                    float __xline1 = __pageLeft + ((__pageWidth * 15) / 100);
                    float __yline1 = __pageTop + 283;
                    float __yline2 = __detailTop + (__detailheight / 2) + 50;
                    e.DrawLine(__objectPen, __xline1, __yline1, __xline1, __yline2);
                    float __xline2 = __xline1 + ((__pageWidth * 32) / 100);
                    e.DrawLine(__objectPen, __xline2, __yline1, __xline2, __yline2);
                    float __xline3 = __xline2 + ((__pageWidth * 10) / 100);
                    float __xline3Buttom = __detailheight + __yline1;
                    e.DrawLine(__objectPen, __xline3, __yline1, __xline3, __xline3Buttom);
                    float __xline4 = __xline3 + ((__pageWidth * 15) / 100);
                    e.DrawLine(__objectPen, __xline4, __yline1, __xline4, __yline2);
                    float __xline5 = __xline4 + ((__pageWidth * 12) / 100);
                    float __lineButtomTop = __yline2 + (_fontHeaderBold14.GetHeight(e) * 7);
                    e.DrawLine(__objectPen, __xline5, __yline1, __xline5, __lineButtomTop);
                    e.DrawLine(__objectPen, __xline3, __lineButtomTop, __xWidth + __pageLeft, __lineButtomTop);
                }
                if (__pageEnum.Equals(_gForm._formEnum.ใบสั่งซื้อ))
                {
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
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__pageWidth * 12) / 100);
                    float __xline2 = __xline1 + __pageLeft + ((__pageWidth * 30) / 100);
                    float __xline3 = __xline2 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline4 = __xline3 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline5 = __xline4 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline6 = __xline5 + __pageLeft + ((__pageWidth * 6) / 100);
                    e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                    e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                    e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                    e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                    e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                    e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    //////กรอบล่างสุด
                    ////float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    ////float __footWidth = __pageWidth - (__pageLeft * 2);
                    ////float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight) - 30;
                    ////DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __footWidth, __footHeight, 8);      
                }
                if (__pageEnum.Equals(_gForm._formEnum.ใบสั่งขาย))
                {
                    float __xWidth = __pageWidth - (__pageLeft * 2);
                    float __detailTop = __pageTop + 250;
                    float __detailheight = __pageHeight - (__detailTop) - 300;
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailheight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 60, __xWidth + __pageLeft, __detailTop + 60);
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__xWidth - 1, 58);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft, (int)__detailTop + 1, (int)__xWidth - 1, 58);
                    float __footterTop = (__detailTop + __detailheight) + 3;
                    float __foottetHeight = __pageHeight - (__detailTop + __detailheight) - 150;
                    e.DrawRectangle(__objectPen, __pageLeft, __footterTop, __xWidth, __foottetHeight);
                    float __xline1 = __pageLeft + ((__pageWidth * 8) / 100);
                    float __ylineTop = __pageTop + 250;
                    float __ylineEnd = __detailTop + __detailheight;
                    e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                    float __xline2 = __xline1 + __pageLeft + ((__pageWidth * 15) / 100);
                    e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                    float __xline3 = __xline2 + __pageLeft + ((__pageWidth * 45) / 100);
                    e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                } if (__pageEnum.Equals(_gForm._formEnum.ใบกำกับภาษี))
                {
                    e.SmoothingMode = SmoothingMode.HighQuality;
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailWidth = __pageWidth - (__pageLeft * 2);
                    float __detailHeight = __pageHeight - (__detailTop + 180);
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    float __xylineTop = __detailTop + __detailHeight - 180;
                    e.DrawLine(__objectPen, __pageLeft, __xylineTop, __detailWidth + __pageLeft, __xylineTop);
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __xylineTop;
                    float __xline1 = __pageLeft + ((__pageWidth * 5) / 100);
                    float __xline2 = __xline1 + __pageLeft + ((__pageWidth * 8) / 100);
                    float __xline3 = __xline2 + __pageLeft + ((__pageWidth * 28) / 100);
                    float __xline4 = __xline3 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline5 = __xline4 + __pageLeft + ((__pageWidth * 7) / 100);
                    float __xline6 = __xline5 + __pageLeft + ((__pageWidth * 9) / 100);
                    e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                    e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                    e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd + 180);
                    e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                    e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                    e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd + 180);
                    float __ylineButtomTop1 = __xylineTop + 60;
                    float __ylineButtomTop2 = __ylineButtomTop1 + 60;
                    float __xWidth = __pageWidth - (__pageLeft * 2);
                    e.DrawLine(__objectPen, __xline3, __ylineButtomTop1, __xWidth + __pageLeft, __ylineButtomTop1);
                    e.DrawLine(__objectPen, __xline3, __ylineButtomTop2, __xWidth + __pageLeft, __ylineButtomTop2);
                }
                if (__pageEnum.Equals(_gForm._formEnum.ใบแจ้งหนี้))
                {
                    e.SmoothingMode = SmoothingMode.HighQuality;
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailWidth = __pageWidth - (__pageLeft * 2);
                    float __detailHeight = __pageHeight - (__detailTop + 180);
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    float __xylineTop = __detailTop + __detailHeight - 180;
                    e.DrawLine(__objectPen, __pageLeft, __xylineTop, __detailWidth + __pageLeft, __xylineTop);
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __xylineTop;
                    float __xline1 = __pageLeft + ((__pageWidth * 5) / 100);
                    float __xline2 = __xline1 + __pageLeft + ((__pageWidth * 8) / 100);
                    float __xline3 = __xline2 + __pageLeft + ((__pageWidth * 28) / 100);
                    float __xline4 = __xline3 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline5 = __xline4 + __pageLeft + ((__pageWidth * 7) / 100);
                    float __xline6 = __xline5 + __pageLeft + ((__pageWidth * 9) / 100);
                    e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                    e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                    e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd + 180);
                    e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                    e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                    e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd + 180);
                    float __ylineButtomTop1 = __xylineTop + 60;
                    float __ylineButtomTop2 = __ylineButtomTop1 + 60;
                    float __xWidth = __pageWidth - (__pageLeft * 2);
                    e.DrawLine(__objectPen, __xline3, __ylineButtomTop1, __xWidth + __pageLeft, __ylineButtomTop1);
                    e.DrawLine(__objectPen, __xline3, __ylineButtomTop2, __xWidth + __pageLeft, __ylineButtomTop2);
                }
                if (__pageEnum.Equals(_gForm._formEnum.ใบซื้อสินค้าและบริการ))
                {
                    DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 100, (__pageWidth / 2) + 50, 160, 8);//กรอบที่ 1 บน
                    float __box2Left = __pageLeft + (__pageWidth / 2) + 60;
                    float __box2Width = (__pageWidth - __box2Left) - __pageLeft;
                    float __xWidth = __pageWidth - (__pageLeft * 2);
                    DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 100, __box2Width, 160, 8); //กรอบที่ 2 บน
                    float __xtoplineTop1 = __box2Left;
                    float __ytoplineTop1 = __pageTop + 140;
                    float __ytoplineTop2 = __pageTop + 180;
                    float __ytoplineTop3 = __pageTop + 220;
                    e.DrawLine(__objectPen, __xtoplineTop1, __ytoplineTop1, __xWidth + __pageLeft, __ytoplineTop1);
                    e.DrawLine(__objectPen, __xtoplineTop1, __ytoplineTop2, __xWidth + __pageLeft, __ytoplineTop2);
                    e.DrawLine(__objectPen, __xtoplineTop1, __ytoplineTop3, __xWidth + __pageLeft, __ytoplineTop3);
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 263;
                    float __detailWidth = __pageWidth - (__pageLeft * 2);
                    float __detailHeight = __pageHeight - __detailTop - 30;
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    e.FillRectangle(linearBrush, (int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 48);
                    float __ylineTop = __pageTop + 263;
                    float __ylineEnd = __detailTop + __detailHeight + 30;
                    float __xline1 = __pageLeft + ((__pageWidth * 12) / 100);
                    float __xline2 = __xline1 + __pageLeft + ((__pageWidth * 30) / 100);
                    float __xline3 = __xline2 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline4 = __xline3 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline5 = __xline4 + __pageLeft + ((__pageWidth * 6) / 100);
                    float __xline6 = __xline5 + __pageLeft + ((__pageWidth * 6) / 100);
                    e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd - 350);
                    e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd - 350);
                    e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd - 150);
                    e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd - 350);
                    e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd - 150);
                    e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd - 350);
                    float __ybuttomTop1 = (__detailTop + __detailHeight) - 320;
                    e.DrawLine(__objectPen, __pageLeft, __ybuttomTop1, __detailWidth + __pageLeft, __ybuttomTop1);
                    float __ybuttomTop2 = (__detailTop + __detailHeight) - 280;
                    float __ybuttomTop3 = (__detailTop + __detailHeight) - 240;
                    float __ybuttomTop4 = (__detailTop + __detailHeight) - 200;
                    float __ybuttomTop5 = (__detailTop + __detailHeight) - 160;
                    float __ybuttomTop6 = (__detailTop + __detailHeight) - 120;
                    e.DrawLine(__objectPen, __xline5, __ybuttomTop2, __detailWidth + __pageLeft, __ybuttomTop2);
                    e.DrawLine(__objectPen, __xline5, __ybuttomTop3, __detailWidth + __pageLeft, __ybuttomTop3);
                    e.DrawLine(__objectPen, __xline5, __ybuttomTop4, __detailWidth + __pageLeft, __ybuttomTop4);
                    e.DrawLine(__objectPen, __xline5, __ybuttomTop5, __detailWidth + __pageLeft, __ybuttomTop5);
                    e.DrawLine(__objectPen, __pageLeft, __ybuttomTop6, __detailWidth + __pageLeft, __ybuttomTop6);
                    float __footerylineTop = (__detailTop + __detailHeight) - 120;
                    float __footerylineEnd = __footerylineTop + 120;
                    float __footerWidth = (__pageWidth * 50) / 100;
                    float __footerxline1 = __footerWidth;
                    float __footerxline2 = __footerWidth + (__footerWidth / 2);
                    e.DrawLine(__objectPen, __footerxline1, __footerylineTop, __footerxline1, __footerylineEnd);
                    e.DrawLine(__objectPen, __footerxline2, __footerylineTop, __footerxline2, __footerylineEnd);
                }
            }
            catch (Exception ex)
            {
            }
        }

        void __printDataReportReceipt(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //ใบเสร็จรับเงิน-ใบกำกับภาษี  F001
            try
            {
                //Left
                StringFormat sfLeft = new StringFormat();
                sfLeft.Alignment = StringAlignment.Near;
                //Center
                StringFormat sfCenter = new StringFormat();
                sfCenter.Alignment = StringAlignment.Center;
                //Right
                StringFormat sfRight = new StringFormat();
                sfRight.Alignment = StringAlignment.Far;
                float __lineNum = 0;
                if (__pageHeight >= 1180)
                {
                    __lineNum = 60;
                }
                else
                {
                    __lineNum = 50;
                }
                string __strHeader1 = "ใบเสร็จรับเงิน/ใบกำกับภาษี";
                string __strHeader2 = "RECEIPT/TAX INVOICE";
                string __docNostr = "เลขที่";
                string __docDateStr = "วันที่";
                string __compayName = "บริษัทตัวอย่าง";
                string __companyAddress = "99/99";
                string __companyAddress2 = "อำเภอปากเกร็ด";
                string __companyTel = "โทร";
                string __companyTax = "เลขประจำตัวผู้เสียภาษีอากร";
                string __customerTitle = "ลูกค้า";
                string __customerName = "บริษัท ตัวอย่าง";
                string __customerAddress = "sssssssssss";
                string __customerAddress2 = "121222";
                string __docnoResult = "295209-00001";
                string __docdateResult = DateTime.Now.ToShortDateString();
                float __y = __pageTop;
                float __lineY = __pageTop;
                float fHeight = _fontHeaderBold14.GetHeight(g);
                float fNorHeight = _fontHeaderNormal14.GetHeight(g);
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __pageWidth - __pageLeft, fHeight);
                g.DrawString(__compayName, _fontHeaderBold14, Brushes.Black, rectTitle, sfLeft);
                __lineY += fHeight - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __pageWidth - __pageLeft, fNorHeight);
                g.DrawString(__companyAddress, _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += fNorHeight - 5;
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __pageWidth - __pageLeft, fNorHeight);
                g.DrawString(__companyAddress2, _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += fNorHeight - 5;
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __pageWidth - __pageLeft, fNorHeight);
                g.DrawString(__companyTel, _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += fNorHeight - 5;
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __pageWidth - __pageLeft, fNorHeight);
                g.DrawString(__companyTax, _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += fNorHeight + 18;
                rectTitleDetail = new RectangleF(__pageLeft, __lineY, __pageWidth - __pageLeft, fHeight);
                g.DrawString(__customerTitle, _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 50, __lineY, __pageWidth - __pageLeft, fHeight);
                g.DrawString(__customerName, _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += fNorHeight - 5;
                rectTitleDetail = new RectangleF(__pageLeft + 50, __lineY, __pageWidth - __pageLeft, fHeight);
                g.DrawString(__customerAddress, _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += fNorHeight - 5;
                rectTitleDetail = new RectangleF(__pageLeft + 50, __lineY, __pageWidth - __pageLeft, fHeight);
                g.DrawString(__customerAddress2, _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                float lineHeight = _fontHeaderBold16.GetHeight(g);
                RectangleF rect = new RectangleF(__pageWidth - 250, __y, 220, lineHeight);
                g.DrawString(__strHeader1, _fontHeaderBold16, Brushes.Black, rect, sfCenter);
                __y += _fontHeaderBold16.GetHeight(g) - 10;
                rect = new RectangleF(__pageWidth - 250, __y, 220, lineHeight);
                g.DrawString(__strHeader2, _fontHeaderBold16, Brushes.Black, rect, sfCenter);
                float __yDocNo = __pageTop + 60;
                float __titleBoxLeft = __pageWidth - 250;
                float __titleBoxWidth = __pageWidth - 180;
                float __docnoHeight = _fontHeaderNormal14.GetHeight(g);
                RectangleF rectDocno = new RectangleF(__titleBoxLeft, __yDocNo, 70, __docnoHeight);
                g.DrawString(__docNostr, _fontHeaderNormal14, Brushes.Black, rectDocno, sfCenter);
                rectDocno = new RectangleF(__titleBoxLeft + 80, __yDocNo, __pageWidth, __docnoHeight);
                g.DrawString(__docnoResult, _fontHeaderNormal14, Brushes.Black, rectDocno, sfLeft);
                __yDocNo += __docnoHeight + 8;
                rectDocno = new RectangleF(__titleBoxLeft, __yDocNo, 70, __docnoHeight);
                g.DrawString(__docDateStr, _fontHeaderNormal14, Brushes.Black, rectDocno, sfCenter);
                rectDocno = new RectangleF(__titleBoxLeft + 80, __yDocNo, 70, __docnoHeight);
                g.DrawString(__docdateResult, _fontHeaderNormal14, Brushes.Black, rectDocno, sfLeft);
                //หนักงานขาย - หมายเหตุ
                float __topTitleDetail = __pageTop + 210;
                float __saleWidth = ((__pageWidth * 26) / 100);
                rectTitleDetail = new RectangleF(__pageLeft, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("พนักงานขาย", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                float __titleDetailHeight = __topTitleDetail + _fontHeaderNormal14.GetHeight(g) + 10;
                rectTitleDetail = new RectangleF(__pageLeft, __titleDetailHeight, __saleWidth, __docnoHeight);
                g.DrawString("K002:นายขายดี", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                float __titleDetailLeft1 = __saleWidth + ((__pageWidth * 13) / 100);
                float __xlineLeft2 = __pageLeft + ((__pageWidth * 26) / 100);
                __saleWidth = ((__pageWidth * 13) / 100);
                rectTitleDetail = new RectangleF(__xlineLeft2, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("เครดิต", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth, __docnoHeight);
                g.DrawString("20", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                g.DrawString("วัน", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                __xlineLeft2 = __xlineLeft2 + ((__pageWidth * 13) / 100);
                __saleWidth = ((__pageWidth * 13) / 100);
                rectTitleDetail = new RectangleF(__xlineLeft2, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("วันครบกำหนด", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth, __docnoHeight);
                g.DrawString("20", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __xlineLeft2 = __pageWidth - __xlineLeft2;
                __saleWidth = __pageWidth - __xlineLeft2;
                rectTitleDetail = new RectangleF(__xlineLeft2 - __pageLeft, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("หมายเหตุ", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth, __docnoHeight);
                g.DrawString("ไม่มีการเปิดใบสั่งขาย", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                float __topDetailtitle = __pageTop + 290;
                float __topDetailtitleWidth = ((__pageWidth * 15) / 100);
                float __topDetailtitleLeft = __pageLeft;
                rectTitleDetail = new RectangleF(__topDetailtitleLeft, __topDetailtitle, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("รหัสสินค้า", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __topDetailtitleLeft = __pageLeft + ((__pageWidth * 15) / 100);
                __topDetailtitleWidth = ((__pageWidth * 32) / 100);
                rectTitleDetail = new RectangleF(__topDetailtitleLeft, __topDetailtitle, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("Description", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __topDetailtitleLeft = __topDetailtitleLeft + ((__pageWidth * 32) / 100);
                __topDetailtitleWidth = ((__pageWidth * 10) / 100);
                rectTitleDetail = new RectangleF(__topDetailtitleLeft, __topDetailtitle, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("จำนวน", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                float __leftButtomDetail = __topDetailtitleLeft + ((__pageWidth * 10) / 100);
                __topDetailtitleLeft = __topDetailtitleLeft + ((__pageWidth * 10) / 100);
                __topDetailtitleWidth = ((__pageWidth * 15) / 100);
                rectTitleDetail = new RectangleF(__topDetailtitleLeft, __topDetailtitle, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("ราคาต่อหน่วย", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __topDetailtitleLeft = __topDetailtitleLeft + ((__pageWidth * 15) / 100);
                __topDetailtitleWidth = ((__pageWidth * 12) / 100);
                rectTitleDetail = new RectangleF(__topDetailtitleLeft, __topDetailtitle, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("ส่วนลด", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                float __leftButtomNumber = __topDetailtitleLeft + ((__pageWidth * 12) / 100);
                __topDetailtitleLeft = __topDetailtitleLeft + ((__pageWidth * 12) / 100);
                __topDetailtitleWidth = ((__pageWidth * 16) / 100);
                float __widthButtomNumber = ((__pageWidth * 16) / 100);
                rectTitleDetail = new RectangleF(__topDetailtitleLeft, __topDetailtitle, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("จำนวนเงิน", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                float __detailTop = __pageTop + 300;
                float __detailheight = __pageHeight - (__detailTop);
                float __buttomTop = __detailTop + (__detailheight / 2) + __lineNum;
                float __buttomLeft = __pageLeft;
                float __buttomWidth = __pageLeft + ((__pageWidth * 47) / 100) + 30;

                rectTitleDetail = new RectangleF(__buttomLeft, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("(สี่หมื่นหกพันสิบบาท)", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                float __remarkTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("ใบเสร็จรับเงินฉบับนี้จะสมบูรณ์ต่อเมื่อบริษัทฯ ได้เรียกเก็บเงินตามเช็คเรียบร้อยแล้วเท่านั้น", _fontHeaderNormal12, Brushes.Black, rectTitleDetail, sfCenter);
                __remarkTop = __remarkTop + _fontHeaderNormal14.GetHeight(g) + __lineNum;
                rectTitleDetail = new RectangleF(__buttomLeft, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("ชำระโดย", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 20, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("เงินสด :", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("จำนวนเงิน ...........................................................", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                __remarkTop = __remarkTop + _fontHeaderNormal14.GetHeight(g) + 20;
                rectTitleDetail = new RectangleF(__buttomLeft + 20, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("เช็ค :", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("จำนวนเงิน ...........................................................", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("ธนาคาร ...........................................................", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("เลขที่เช็ค ...........................................................", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomLeft = __pageLeft + __buttomWidth;
                __buttomWidth = ((__pageWidth * 12) / 100) + ((__pageWidth * 15) / 100);

                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("รวมราคาทั้งสิ้น", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("หักส่วนลด", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("ยอดหลังหักส่วนลด", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("หักเงินมัดจำ", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("ยอดหลังหักเงินมัดจำ", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("ภาษีมลูค่าเพิ่ม 7.00 %", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("รวมเงินทั้งสิ้น", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                float __buttomNumberTop = __detailTop + (__detailheight / 2) + __lineNum;
                rectTitleDetail = new RectangleF(__leftButtomNumber, __buttomNumberTop, __widthButtomNumber, __docnoHeight);
                g.DrawString("43,000.00", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                float __buttomDetailHeight = __buttomNumberTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomNumber, __buttomDetailHeight, __widthButtomNumber, __docnoHeight);
                g.DrawString("43,000.00", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                __buttomDetailHeight = __buttomDetailHeight + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomNumber, __buttomDetailHeight, __widthButtomNumber, __docnoHeight);
                g.DrawString("43,000.00", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                __buttomDetailHeight = __buttomDetailHeight + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomNumber, __buttomDetailHeight, __widthButtomNumber, __docnoHeight);
                g.DrawString("43,000.00", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                __buttomDetailHeight = __buttomDetailHeight + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomNumber, __buttomDetailHeight, __widthButtomNumber, __docnoHeight);
                g.DrawString("43,000.00", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                __buttomDetailHeight = __buttomDetailHeight + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomNumber, __buttomDetailHeight, __widthButtomNumber, __docnoHeight);
                g.DrawString("43,000.00", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                __buttomDetailHeight = __buttomDetailHeight + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomNumber, __buttomDetailHeight, __widthButtomNumber, __docnoHeight);
                g.DrawString("43,000.00", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                float __buttomDetailTop = __pageHeight - 80;
                __topDetailtitleWidth = __widthButtomNumber + __buttomWidth;
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomDetailTop, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("ผู้รับเงิน ............................................", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __buttomDetailTop = __buttomDetailTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomDetailTop, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("(....................................................)", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __buttomDetailTop = __buttomDetailTop + _fontHeaderNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__leftButtomDetail, __buttomDetailTop, __topDetailtitleWidth, __docnoHeight);
                g.DrawString("วันที่............./............./...........", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);


            }
            catch (Exception ex)
            {
            }
        }

        public class pointBoxColumn
        {
            public int __pointNo;
            public int __point1;
            public int __point2;
            public int __point3;
            public int __point4;
        }
    }
}
