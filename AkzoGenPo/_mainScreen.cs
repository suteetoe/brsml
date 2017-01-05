using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp;
using System.Xml.Serialization;
using System.IO;

namespace AkzoGenPo
{
    public partial class _mainScreen : Form
    {
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m01");

        public _mainScreen()
        {
            InitializeComponent();

            this._myGrid1._addColumn("PO No", 1, 1, 15, false, false, false, false);
            this._myGrid1._addColumn("PO Date", 4, 1, 15, false, false, false, false);
            this._myGrid1._addColumn("Agent Code", 1, 1, 15, false, false, false, false);
            this._myGrid1._addColumn("Agent Name", 1, 1, 25, false, false, false, false);
            this._myGrid1._addColumn("จำนวนเงิน", 3, 1, 15, false, false, false, false, _formatNumber);
            this._myGrid1._addColumn("สร้าง PDF", 1, 1, 10, false, false, false, false);
            this._myGrid1._addColumn("S", 12, 1, 5, true, false, false);
            this._myGrid1._addColumn("email", 1, 1, 5, false, true, false);

            this._myGrid1._mouseClickClip += new MyLib.ClipMouseClickHandler(_myGrid1__mouseClickClip);
            this.Load += new EventHandler(_mainScreen_Load);
        }

        void _myGrid1__mouseClickClip(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals("S"))
            {
                try
                {
                    MyLib._myGrid __grid = (MyLib._myGrid)sender;
                    if (__grid._cellGet(__grid._selectRow, 0).ToString().Length > 0)
                    {
                        string __docNo = this._myGrid1._cellGet(__grid._selectRow, 0).ToString();

                        if (MessageBox.Show("คุณต้องการ พิมพ์รายการ " + __docNo + "หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            poPrint __print = new poPrint();
                            if (__print._print(__docNo))
                            {

                                MessageBox.Show("การพิมพ์ " + __docNo + " เสร็จสมบุรณ์", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // if email config
                                try
                                {
                                    XmlSerializer s = new XmlSerializer(typeof(AkzoGlobal._global._poConfig));
                                    AkzoGlobal._global._poConfig __config;
                                    TextReader r = new StreamReader(AkzoGlobal._global._poConfigFileName);
                                    __config = (AkzoGlobal._global._poConfig)s.Deserialize(r);
                                    r.Close();

                                    string __getAgentCode = __grid._cellGet(__grid._selectRow, "Agent Code").ToString();
                                    string __getAgentEmail = __grid._cellGet(__grid._selectRow, "email").ToString();

                                    if (__config._sendOrder && __config._emailOrderTarget.Length > 0 && (MessageBox.Show("คุณต้องการที่จะส่ง Order Email ไปยัง Agent หรือไม่ \n " + __getAgentEmail + "", "ยืนยันการส่ง Email Order", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes))
                                    {

                                        bool __result = AkzoGlobal._global._sendMail(__config._emailOrderTarget, __config._emailSenderPassword, __getAgentEmail, "E-Ordering-" + __getAgentCode, "Order Detail", AkzoGlobal._global._pdfLocation + __docNo + ".pdf");

                                        if (__result == false)
                                        {
                                            MessageBox.Show(AkzoGlobal._global._errSendMailMessage, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                    }
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                MessageBox.Show("Print Fail");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void _mainScreen_Load(object sender, EventArgs e)
        {
            _loadDataToScreen();
        }

        private void _exitButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _loadDataToScreen()
        {
            try
            {
                SqlConnection __conn = AkzoGlobal._global._sqlConnection;
                __conn.Open();
                string __myQueryTop = "select doc_no ,doc_date_time, amount_before_discount,discount , amount ,memo,agent_code,(select name from sml_agent where sml_agent.code=eorder_order.agent_code) as agent_name, (select email from sml_agent where sml_agent.code=eorder_order.agent_code) as agent_email from eorder_order  where last_status = 2";
                SqlDataAdapter __daWait = new SqlDataAdapter(__myQueryTop, __conn);
                DataTable __dtWait = new DataTable();
                __daWait.Fill(__dtWait);
                for (int __row = 0; __row < __dtWait.Rows.Count; __row++)
                {
                    string __getDocNo = __dtWait.Rows[__row].ItemArray.GetValue(0).ToString();
                    string __getDocDate = __dtWait.Rows[__row].ItemArray.GetValue(1).ToString();
                    DateTime __getDate = MyLib._myGlobal._convertDate(__getDocDate);
                    string __getAgentCode = __dtWait.Rows[__row].ItemArray.GetValue(6).ToString();
                    string __getAgentName = __dtWait.Rows[__row].ItemArray.GetValue(7).ToString();
                    string ssss = __dtWait.Rows[__row].ItemArray.GetValue(4).ToString();
                    decimal __getAmount = MyLib._myGlobal._decimalPhase(__dtWait.Rows[__row].ItemArray.GetValue(4).ToString());
                    string __getEmail = __dtWait.Rows[__row].ItemArray.GetValue(8).ToString();

                    this._myGrid1._addRow();
                    this._myGrid1._cellUpdate(__row, 0, __getDocNo, false);
                    this._myGrid1._cellUpdate(__row, 1, __getDate, false);
                    this._myGrid1._cellUpdate(__row, 2, __getAgentCode, false);
                    this._myGrid1._cellUpdate(__row, 3, __getAgentName, false);
                    this._myGrid1._cellUpdate(__row, 4, __getAmount, false);
                    this._myGrid1._cellUpdate(__row, 5, "Waiting..", false);
                    this._myGrid1._cellUpdate(__row, 7, __getEmail, false);
                    this._myGrid1.Invalidate();
                }
                //__buildForm();
            }
            catch (Exception ex)
            {
            }
        }

        private void _formdesignButton_Click(object sender, EventArgs e)
        {
            _POFormDesign __design = new _POFormDesign();
            __design.ShowDialog();
        }

        static XFont _fontBold14 = new XFont("Angsana New", 14, XFontStyle.Bold, new XPdfFontOptions(PdfFontEncoding.Unicode));
        static XFont _fontBold18 = new XFont("Angsana New", 18, XFontStyle.Bold, new XPdfFontOptions(PdfFontEncoding.Unicode));
        static XFont _fontNormal14 = new XFont("Angsana New", 14, XFontStyle.Regular, new XPdfFontOptions(PdfFontEncoding.Unicode));

        private void _testPrintPDF_Click(object sender, EventArgs e)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a font
            XFont font = new XFont("Tahoma", 16, XFontStyle.Regular, new XPdfFontOptions(PdfFontEncoding.Unicode));
            XFont font14 = new XFont("Tahoma", 14, XFontStyle.Regular, new XPdfFontOptions(PdfFontEncoding.Unicode));

            //Left
            XStringFormat sfLeft = new XStringFormat();
            sfLeft.Alignment = XStringAlignment.Near;
            //Center
            XStringFormat sfCenter = new XStringFormat();
            sfCenter.Alignment = XStringAlignment.Center;
            //Right
            XStringFormat sfRight = new XStringFormat();
            sfRight.Alignment = XStringAlignment.Far;

            // Create first page
            PdfPage page = document.AddPage();
            page.Size = PageSize.A4;
            page.Width = page.Width - 20;
            double __xWidth = page.Width;
            XGraphics g = XGraphics.FromPdfPage(page);

            double __detailY = 325;
            double __getLeft = 20;

            double __titleTop = 255;
            double __titleTop2 = 325;

            XRect rect = new XRect();

            double __cusLeft = 20 + 10;
            double __custTop = 20 + 130;
            double __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

            double lineHeight = _fontBold18.GetHeight(g);

            rect = new RectangleF((float)__cusLeft, (float)__custTop, 150, 500);
            g.DrawString("ชื่อเจ้าหนี้", _fontBold14, Brushes.Black, rect, sfLeft);

            __custTop += _fontNormal14.GetHeight(g);
            rect = new RectangleF((float)__cusLeft, (float)__custTop, (float)__detailWidth, 200);
            g.DrawString("ที่อยู่", _fontBold14, Brushes.Black, rect, sfLeft);

            string filename = @"c:\\smlpdftemp\\testprint.pdf";
            document.Save(filename);

        }

        private void _poConfigToolstrip_Click(object sender, EventArgs e)
        {
            _poConfigForm __form = new _poConfigForm();
            __form.ShowDialog();
        }
    }
}
