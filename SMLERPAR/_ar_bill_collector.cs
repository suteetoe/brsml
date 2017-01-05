using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using SMLERPReportTool;
using System.Xml.Serialization;
using System.Globalization;

namespace SMLERPAR
{
    public partial class _ar_bill_collector : UserControl
    {
        SMLERPAPARControl._ar_ap_trans _ar_ap_trans1;
        SplitContainer _selectBillContainer;

        MyLib._myGrid _arGrid;
        MyLib._myGrid _invoiceDocGrid;
        DataTable _getDataAR;

        private System.Windows.Forms.Panel _arSearchPanel;
        private System.Windows.Forms.TextBox _searchTextbox;
        private System.Windows.Forms.Label _searchLabel;
        private MyLib.VistaButton _searchARButton;


        public _ar_bill_collector()
        {
            InitializeComponent();

            this.SuspendLayout();

            this._ar_ap_trans1 = new SMLERPAPARControl._ar_ap_trans();
            this._ar_ap_trans1.AutoSize = true;
            this._ar_ap_trans1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ar_ap_trans1.Location = new System.Drawing.Point(0, 0);
            this._ar_ap_trans1.Name = "_ar_ap_trans1";
            this._ar_ap_trans1.Size = new System.Drawing.Size(1000, 600);
            this._ar_ap_trans1.TabIndex = 0;
            this._ar_ap_trans1._transControlType = _g.g._transControlTypeEnum.IMEX_Bill_Collector;
            this._ar_ap_trans1._searchBillPanel.Visible = true;
            this._ar_ap_trans1._closeButton.Click += _closeButton_Click;

            this._arSearchPanel = new Panel();
            this._searchLabel = new Label();
            this._searchTextbox = new TextBox();
            this._searchARButton = new MyLib.VistaButton();

            this._selectBillContainer = new SplitContainer();
            this._selectBillContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectBillContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._selectBillContainer.Location = new System.Drawing.Point(0, 0);
            this._selectBillContainer.Name = "_selectBillContainer";
            this._selectBillContainer.Size = new System.Drawing.Size(598, 447);
            this._selectBillContainer.SplitterDistance = 283;
            this._selectBillContainer.TabIndex = 3;


            this._ar_ap_trans1._searchBillPanel.Controls.Add(this._selectBillContainer);
            this._ar_ap_trans1._searchBillPanel.Controls.Add(this._arSearchPanel);

            this.Controls.Add(this._ar_ap_trans1);
            this._ar_ap_trans1._myManageData1._closeScreen += _myManageData1__closeScreen;

            this._arGrid = new MyLib._myGrid();
            this._arGrid.Dock = DockStyle.Fill;
            this._arGrid._table_name = _g.d.ar_customer._table;
            this._arGrid._isEdit = false;
            this._arGrid._addColumn(_g.d.ar_customer._code, 1, 30, 30);
            this._arGrid._addColumn(_g.d.ar_customer._name_1, 1, 70, 70);
            this._arGrid._calcPersentWidthToScatter();
            this._arGrid._mouseClick += _arGrid__mouseClick;

            this._invoiceDocGrid = new MyLib._myGrid();
            this._invoiceDocGrid.Dock = DockStyle.Fill;
            this._invoiceDocGrid._table_name = _g.d.ap_ar_resource._table;
            this._invoiceDocGrid._isEdit = false;
            this._invoiceDocGrid._addColumn(_g.d.ap_ar_resource._doc_no, 1, 10, 15);
            this._invoiceDocGrid._addColumn(_g.d.ap_ar_resource._doc_date, 4, 10, 15);
            //this._invoiceDocGrid._addColumn(_g.d.ap_ar_trans._due_date, 4, 70, 70);
            this._invoiceDocGrid._addColumn(_g.d.ap_ar_resource._doc_type, 1, 10, 15, false);
            this._invoiceDocGrid._addColumn(_g.d.ap_ar_resource._due_date, 4, 10, 15);
            //
            this._invoiceDocGrid._addColumn(_g.d.ap_ar_resource._amount, 3, 10, 15, false, false, false, false, _g.g._getFormatNumberStr(3));

            this._invoiceDocGrid._addColumn(_g.d.ap_ar_resource._ar_code, 1, 70, 70, false, true);
            this._invoiceDocGrid._addColumn(_g.d.ap_ar_resource._ar_name, 1, 70, 70, false, true);
            this._invoiceDocGrid._mouseClick += _invoiceDocGrid__mouseClick;
            this._invoiceDocGrid._calcPersentWidthToScatter();

            this._selectBillContainer.Panel1.Controls.Add(this._arGrid);
            this._selectBillContainer.Panel2.Controls.Add(this._invoiceDocGrid);

            // 
            // vistaButton1
            // 
            this._searchARButton._drawNewMethod = false;
            this._searchARButton.BackColor = System.Drawing.Color.Transparent;
            this._searchARButton.ButtonText = "ค้นหาลูกค้า";
            this._searchARButton.Location = new System.Drawing.Point(400, 4);
            this._searchARButton.Name = "_searchARButton";
            this._searchARButton.Size = new System.Drawing.Size(83, 22);
            this._searchARButton.TabIndex = 2;
            this._searchARButton.Text = "ค้นหาลูกค้า";
            this._searchARButton.UseVisualStyleBackColor = false;
            this._searchARButton.Click += _searchARButton_Click;
            // 
            // _searchLabel
            // 
            this._searchLabel.AutoSize = true;
            this._searchLabel.Location = new System.Drawing.Point(3, 7);
            this._searchLabel.Name = "_searchLabel";
            this._searchLabel.Size = new System.Drawing.Size(87, 14);
            this._searchLabel.TabIndex = 0;
            this._searchLabel.Text = "ข้อความค้นหา : ";
            // 
            // _searchTextbox
            // 
            this._searchTextbox.Location = new System.Drawing.Point(90, 4);
            // this._searchTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchTextbox.Name = "_searchTextbox";
            this._searchTextbox.Size = new System.Drawing.Size(306, 22);
            this._searchTextbox.TabIndex = 1;
            this._searchTextbox.KeyDown += _searchTextbox_KeyDown;
            //
            // _arSearchPanel
            //
            this._arSearchPanel.Controls.Add(this._searchTextbox);
            this._arSearchPanel.Controls.Add(this._searchLabel);
            this._arSearchPanel.Controls.Add(this._searchARButton);
            this._arSearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._arSearchPanel.Location = new System.Drawing.Point(0, 0);
            this._arSearchPanel.Name = "_arSearchPanel";
            this._arSearchPanel.Size = new System.Drawing.Size(490, 30);
            this._arSearchPanel.TabIndex = 0;


            this.ResumeLayout(false);
            this.PerformLayout();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._loadAR();
            }

            this._ar_ap_trans1._customPrint += _ar_ap_trans1__customPrint;
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool _ar_ap_trans1__customPrint(string docType, string docNo, string transFlag, bool showOption)
        {
            string __getTempFileByServer = MyLib._myGlobal._getFirstWebServiceServer.Replace(".", "_").Replace(":", "__") + "-" + MyLib._myGlobal._databaseName + "-";

            // get firstline
            // check xml 
            string __currentConfigFileName = __getTempFileByServer + "configPrinterScreen" + transFlag + ".xml";
            string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
            _PrintConfig __config = new _PrintConfig();

            System.Drawing.Printing.PrintRange __printRangeOption = System.Drawing.Printing.PrintRange.AllPages;
            int[] __printPageRange = null;
            bool __includeDocSeries = false;
            System.Drawing.Printing.PrintRange __seriesRangeOption = System.Drawing.Printing.PrintRange.AllPages;
            int[] __seriesRange = null;

            //toe ทดสอบดึงจาก temp ตัวใหม่
            try
            {
                TextReader readFile = new StreamReader(__path);
                XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                readFile.Close();
            }
            catch
            {
                // ไม่ได้ไปดึงของเก่า
                try
                {
                    __currentConfigFileName = "configPrinterScreen" + transFlag + ".xml";
                    __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();

                    TextReader readFile = new StreamReader(__path);
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                    __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch (Exception ex)
                {
                }

            }

            SMLERPReportTool._formPrintOption __printOption = new SMLERPReportTool._formPrintOption(transFlag, "");

            __printOption._showagainCheck.Checked = __config.ShowAgain;
            __printOption._previewPrintCheck.Checked = __config.isPreview;
            __printOption._printCheck.Checked = __config.isPrint;
            __printOption._setPrintNameSelectIndex(__config.PrinterName);

            System.Windows.Forms.DialogResult __formResult = __formResult = __printOption.ShowDialog(MyLib._myGlobal._mainForm);
            if (__formResult == DialogResult.OK && (__printOption._previewPrintCheck.Checked == true || __printOption._printCheck.Checked == true)) //  || __config.printAttactForm
            {
                // query และสั่งพิมพ์
                _printData(docNo, __printOption._previewPrintCheck.Checked, __printOption._printerCombo.Text.ToString());
            }
            return false;

        }

        #region Print Form

        System.Drawing.Printing.PrintDocument _doc;
        public Font _fontNormal16 = new Font("Angsana New", 16, FontStyle.Regular);
        public Font _fontBold16 = new Font("Angsana New", 16, FontStyle.Bold);
        public Font _fontNormal14 = new Font("Angsana New", 14, FontStyle.Regular);
        public Font _fontBold14 = new Font("Angsana New", 14, FontStyle.Bold);
        public Font _fontBoldUnderLine14 = new Font("Angsana New", 14, FontStyle.Bold | FontStyle.Underline);

        DataTable _tableDoc = null;
        DataTable _tableDetail = null;
        List<float> _columnWidthPercent;
        List<string> _columnHeader;
        List<string> _columnField;

        string _lastCutsCode = "";
        decimal _sumCust = 0M;

        void _printData(string docNo, bool isPreview, string printerName)
        {

            _tableDoc = null;
            _tableDetail = null;
            _columnWidthPercent = new List<float>();

            _columnWidthPercent.Add(6); // 0 
            _columnWidthPercent.Add(10); // 1
            _columnWidthPercent.Add(35); // 2
            _columnWidthPercent.Add(14); // 3
            _columnWidthPercent.Add(10); // 4
            _columnWidthPercent.Add(12); // 5
            _columnWidthPercent.Add(15); // 6

            _columnHeader = new List<string>();
            _columnHeader.Add("ลำดับ");
            _columnHeader.Add("รหัสลูกค้า");
            _columnHeader.Add("ชื่อ ที่อยู่ ลูกค้า");
            _columnHeader.Add("เลขที่บิล");
            _columnHeader.Add("ชื่อผู้แทนขาย");
            _columnHeader.Add("จำนวนเงิน");
            _columnHeader.Add("หมายเหตุ");

            _columnField = new List<string>();
            _columnField.Add("");
            _columnField.Add("cust_code");
            _columnField.Add("cust_name");
            _columnField.Add("billing_no");
            _columnField.Add("sale_code");
            _columnField.Add("sum_debt_amount");
            _columnField.Add("remark");

            // query data
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            // header 
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select doc_no, doc_date, sale_code, (select name_1 from erp_user where erp_user.code = ap_ar_trans.sale_code) as sale_name, trans_flag , total_net_value , (select count(distinct cust_code) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans_detail.trans_flag = ap_ar_trans.trans_flag  ) as count_customer, (select count(cust_code) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no = ap_ar_trans.doc_no and ap_ar_trans_detail.trans_flag = ap_ar_trans.trans_flag  ) as count_bill from ap_ar_trans where doc_no = '" + docNo + "' and trans_flag = 810  "));

            // detaill
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select cust_code, (select name_1 from ar_customer where ar_customer.code = ap_ar_trans_detail.cust_code ) as cust_name, billing_no, (select sale_code from ic_trans where ic_trans.doc_no = ap_ar_trans_detail.billing_no) as sale_code, sum_debt_amount, remark  from ap_ar_trans_detail where doc_no = '" + docNo + "' and trans_flag = 810 order by cust_code, line_number "));

            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Count > 0)
            {
                _tableDoc = ((DataSet)__result[0]).Tables[0];
                _tableDetail = ((DataSet)__result[1]).Tables[0];

                _doc = new System.Drawing.Printing.PrintDocument();
                _doc.PrinterSettings.PrinterName = printerName;
                _doc.PrintPage += _doc_PrintPage;
                _doc.BeginPrint += _doc_BeginPrint;

                if (isPreview)
                {
                    PrintPreviewDialog __preview = new PrintPreviewDialog();
                    __preview.Document = _doc;
                    ((Form)__preview).WindowState = FormWindowState.Maximized;
                    __preview.ShowDialog();
                }
                else
                    _doc.Print();
            }
        }

        int _rowDetailIndex = 0;
        Boolean _drawTotalCompanyOnly = false;

        float _paddingLeft = 40;
        float _paddingRight = 40;
        float _paddingBottom = 60;
        float _paddingTop = 40;

        float _paperWidth = 0f;
        float _paperHeight = 0f;
        float _headerHeight = 110;

        int pageCount = 1;
        int pageIndex = 1;

        void _doc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _rowDetailIndex = 0;
            _drawTotalCompanyOnly = false;
            _lastCutsCode = "";
            _sumCust = 0M;
            pageCount = 1;
            pageIndex = 1;

            // คำณวนหน้า หา จำนวนหน้าทั้งหมด
            System.Drawing.Printing.PrintDocument doc = (System.Drawing.Printing.PrintDocument)sender;

            _paperWidth = doc.DefaultPageSettings.Bounds.Width;
            _paperHeight = doc.DefaultPageSettings.Bounds.Height;

            float __bodyHeight = _paperHeight - (_headerHeight + _paddingBottom);
            Graphics g = doc.PrinterSettings.CreateMeasurementGraphics();
            float __rowHeight = (float)Math.Round(g.MeasureString("XXXX", _fontNormal14).Height);

            float __y = 0f;
            __y += (_headerHeight + __rowHeight);

            for (int row = 0; row < _tableDetail.Rows.Count; row++)
            {
                // total company
                if (row != 0 && _lastCutsCode != _tableDetail.Rows[row]["cust_code"].ToString())
                {
                    // print sum total cust
                    if ((__y + __rowHeight) < _paperHeight - _paddingBottom)
                    {
                        __y += __rowHeight;
                    }
                    else
                    {
                        __y = (_headerHeight + __rowHeight);
                        pageCount++;
                    }
                }

                _lastCutsCode = _tableDetail.Rows[row]["cust_code"].ToString();


                // detail
                if ((__y + __rowHeight) < _paperHeight - _paddingBottom)
                {
                    __y += __rowHeight;
                }
                else
                {

                    __y = (_headerHeight + __rowHeight);
                    __y += __rowHeight;
                    pageCount++;
                }

                //
            }

            // last
            if ((__y + __rowHeight) < _paperHeight - _paddingBottom)
            {
                __y += __rowHeight;

            }
            else
            {
                __y = (_headerHeight + __rowHeight);
                pageCount++;
            }

            // footer
            if ((__y + (__rowHeight * 4)) < _paperHeight - _paddingBottom) // total 4 line
            {
                __y += (__rowHeight * 4);
            }
            else
            {
                __y = (_headerHeight + __rowHeight);
                pageCount++;

            }


            _lastCutsCode = "";
            _sumCust = 0M;


        }

        void _doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float __drawWidth = _paperWidth - (_paddingLeft + _paddingRight); // padding left 40, padding right 40

            // calc width per column
            List<float> _columWidth = new List<float>();
            float __widthTotalPercent = 0f;
            foreach (float widthPercent in _columnWidthPercent)
            {
                __widthTotalPercent += widthPercent;
            }

            float __sumWidth = 0f;
            for (int i = 0; i < _columnWidthPercent.Count; i++)
            {
                float __widthPercent = _columnWidthPercent[i];
                float __colWidth = 0f;
                if (i == _columnWidthPercent.Count - 1)
                {
                    // last column
                    __colWidth = __drawWidth - __sumWidth;
                }
                else
                {
                    __colWidth = (float)Math.Round(__drawWidth * __widthPercent / __widthTotalPercent);
                    __sumWidth += __colWidth;
                }

                _columWidth.Add(__colWidth);
            }

            float __leftPadding = 36;
            float __rowHeight = (float)Math.Round(e.Graphics.MeasureString("XXXX", _fontNormal14).Height);

            /*float __headerHeight = 110;*/
            float __bodyHeight = _paperHeight - (_headerHeight + _paddingBottom);

            // draw HeaderPage
            e.Graphics.DrawString(MyLib._myGlobal._ltdName, _fontBold16, Brushes.Black, new RectangleF(__leftPadding, 20, (_paperWidth - 200) - __leftPadding, 30), new StringFormat() { Alignment = StringAlignment.Center });
            e.Graphics.DrawString(MyLib._myGlobal._ltdAddress.Replace("\n", string.Empty), _fontNormal14, Brushes.Black, new RectangleF(__leftPadding, 52, (_paperWidth - 200) - __leftPadding, 30), new StringFormat() { Alignment = StringAlignment.Center });

            e.Graphics.DrawString("เลขที่", _fontNormal14, Brushes.Black, new RectangleF(_paperWidth - 200, 52, 50, 30));
            e.Graphics.DrawString(_tableDoc.Rows[0][_g.d.ap_ar_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, new RectangleF(_paperWidth - 160, 52, 120, 30));

            e.Graphics.DrawString("วันที่", _fontNormal14, Brushes.Black, new RectangleF(_paperWidth - 200, 84, 50, 30));
            e.Graphics.DrawString(MyLib._myGlobal._convertDateFromQuery(_tableDoc.Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).ToString("d MMMM yyyy", new CultureInfo("en-US")), _fontNormal14, Brushes.Black, new RectangleF(_paperWidth - 160, 84, 120, 30));

            e.Graphics.DrawString("ใบรับบิลเพื่อทำการเรียกเก็บเงินจากลูกค้าโดย", _fontNormal14, Brushes.Black, new PointF(__leftPadding + 20, 84));
            e.Graphics.DrawString(_tableDoc.Rows[0][_g.d.ap_ar_trans._sale_code].ToString() + ":" + _tableDoc.Rows[0][_g.d.ap_ar_trans._sale_name].ToString(), _fontBold16, Brushes.Black, new PointF(__leftPadding + 270, 84));

            float __x = 0;
            float __y = 0;
            __x += _paddingLeft;
            __y += _headerHeight;
            StringFormat __headerFormat = StringFormat.GenericTypographic;
            __headerFormat.Alignment = StringAlignment.Center;
            __headerFormat.LineAlignment = StringAlignment.Center;

            Boolean __hasMorePage = false;

            StringFormat __rowFormat1 = StringFormat.GenericDefault;
            __rowFormat1.LineAlignment = StringAlignment.Far;

            if (_rowDetailIndex < _tableDetail.Rows.Count - 1 || _drawTotalCompanyOnly)
            {
                // header
                for (int columnIndex = 0; columnIndex < _columWidth.Count; columnIndex++)
                {
                    float __columnWidth = _columWidth[columnIndex];
                    string __headerStr = _columnHeader[columnIndex];
                    // ตีเส้นหัวตาราง
                    RectangleF __header = new RectangleF(__x, __y, __columnWidth, __rowHeight);
                    e.Graphics.DrawString(__headerStr, _fontNormal14, Brushes.Black, __header, __headerFormat);
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1f), Rectangle.Round(__header));
                    __x += __columnWidth;
                }
                __y += __rowHeight;

                // drawDetail
                __x = _paddingLeft;

                // drawFooter
                for (int row = _rowDetailIndex; row < _tableDetail.Rows.Count; row++)
                {
                    if (row != 0 && _lastCutsCode != _tableDetail.Rows[row]["cust_code"].ToString())
                    {
                        // print sum total cust
                        if ((__y + __rowHeight) < _paperHeight - _paddingBottom)
                        {
                            RectangleF __columnRect = new RectangleF(450, __y, 150, __rowHeight);
                            e.Graphics.DrawString("รวมเฉพาะบริษัท", _fontBold14, Brushes.Black, __columnRect, __rowFormat1);
                            __columnRect = new RectangleF(_paddingLeft + _columWidth[0] + _columWidth[1] + _columWidth[2] + _columWidth[3] + _columWidth[4], __y, _columWidth[5], __rowHeight);
                            e.Graphics.DrawString(_sumCust.ToString("#,###,###.00"), _fontBoldUnderLine14, Brushes.Black, __columnRect, new StringFormat() { Alignment = StringAlignment.Far });

                            __y += __rowHeight;
                            _sumCust = 0M;
                        }
                        else
                        {
                            __hasMorePage = true;
                            break;
                        }
                    }

                    __x = _paddingLeft;
                    if ((__y + __rowHeight) < _paperHeight - _paddingBottom)
                    {
                        bool __isNewCust = true;
                        if (_lastCutsCode == _tableDetail.Rows[row]["cust_code"].ToString())
                        {
                            __isNewCust = false;
                        }
                        _lastCutsCode = _tableDetail.Rows[row]["cust_code"].ToString();

                        // ตรวจแล้วยังสามารถพิมพ์ได้อีก 1 บรรทัด
                        for (int columnIndex = 0; columnIndex < _columWidth.Count; columnIndex++)
                        {
                            // no
                            RectangleF __columnRect = new RectangleF(__x, __y, _columWidth[columnIndex], __rowHeight);
                            string __dataStr = (columnIndex == 0) ? (row + 1).ToString() : _tableDetail.Rows[row][_columnField[columnIndex]].ToString();

                            if ((columnIndex == 2 || columnIndex == 1) && __isNewCust == false)
                            {
                                __dataStr = "";
                            }
                            if (columnIndex == 5)
                            {
                                decimal __billAmount = MyLib._myGlobal._decimalPhase(__dataStr);
                                _sumCust += __billAmount;
                                __dataStr = __billAmount.ToString("#,###,###.00");
                            }

                            StringFormat __sf = new StringFormat();
                            if (columnIndex == 0 || columnIndex == 1 || columnIndex == 3)
                            {
                                __sf.Alignment = StringAlignment.Center;
                            }
                            else if (columnIndex == 5)
                            {
                                __sf.Alignment = StringAlignment.Far;
                            }

                            if (__dataStr != "")
                            {
                                e.Graphics.DrawString(__dataStr, _fontNormal14, Brushes.Black, __columnRect, __sf);
                            }
                            __x += _columWidth[columnIndex];
                        }


                    }
                    else
                    {
                        __hasMorePage = true;
                        break;
                    }
                    __y += __rowHeight;

                    Pen __linePen = new Pen(Brushes.Black, 1f);
                    __linePen.DashPattern = new float[] { 4, 4 };
                    // draw line under row
                    e.Graphics.DrawLine(__linePen, _paddingLeft, __y - 6, _paperWidth - _paddingRight, __y - 6);

                    _rowDetailIndex = row + 1;
                }

                // draw table line left right bottom
                __x = _paddingLeft;

                // สุดท้าย
                if (_rowDetailIndex == _tableDetail.Rows.Count)
                {
                    if ((__y + __rowHeight) < _paperHeight - _paddingBottom)
                    {
                        RectangleF __columnRect = new RectangleF(450, __y, 150, __rowHeight);
                        e.Graphics.DrawString("รวมเฉพาะบริษัท", _fontBold14, Brushes.Black, __columnRect, __rowFormat1);
                        __columnRect = new RectangleF(_paddingLeft + _columWidth[0] + _columWidth[1] + _columWidth[2] + _columWidth[3] + _columWidth[4], __y, _columWidth[5], __rowHeight);
                        e.Graphics.DrawString(_sumCust.ToString("#,###,###.00"), _fontBoldUnderLine14, Brushes.Black, __columnRect, new StringFormat() { Alignment = StringAlignment.Far });

                        __y += __rowHeight;

                    }
                    else
                    {
                        _drawTotalCompanyOnly = true;
                        __hasMorePage = true;
                    }
                }

                e.Graphics.DrawLine(new Pen(Brushes.Black, 1f), __x, (_headerHeight + __rowHeight), _paddingLeft, __y);
                for (int columnIndex = 0; columnIndex < _columWidth.Count; columnIndex++)
                {
                    __x += _columWidth[columnIndex];
                    e.Graphics.DrawLine(new Pen(Brushes.Black, 1f), __x, (_headerHeight + __rowHeight), __x, __y);
                }
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1f), _paddingLeft, __y, _paperWidth - _paddingRight, __y);
            }

            if (__hasMorePage == false)
            {
                if ((__y + (__rowHeight * 4)) < _paperHeight - _paddingBottom) // total 4 line
                {
                    // draw total
                    RectangleF __columnRect = new RectangleF(80, __y, 200, __rowHeight);
                    e.Graphics.DrawString("รวมลูกค้า", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(300, __y, 120, __rowHeight);
                    e.Graphics.DrawString(MyLib._myGlobal._decimalPhase(_tableDoc.Rows[0]["count_customer"].ToString()).ToString("#,###,###"), _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(400, __y, 50, __rowHeight);
                    e.Graphics.DrawString("ราย", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(450, __y, 200, __rowHeight);
                    e.Graphics.DrawString("จำนวนบิลที่เก็บได้   ..........................................", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(680, __y, 50, __rowHeight);
                    e.Graphics.DrawString("ราย", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __y += __rowHeight;

                    __columnRect = new RectangleF(80, __y, 200, __rowHeight);
                    e.Graphics.DrawString("รวมจำนวนบิลทั้งหมด", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(300, __y, 120, __rowHeight);
                    e.Graphics.DrawString(MyLib._myGlobal._decimalPhase(_tableDoc.Rows[0]["count_bill"].ToString()).ToString("#,###,###"), _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(400, __y, 50, __rowHeight);
                    e.Graphics.DrawString("ใบ", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(450, __y, 200, __rowHeight);
                    e.Graphics.DrawString("จำนวนใบวางบิล   ................................", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(680, __y, 50, __rowHeight);
                    e.Graphics.DrawString("ใบ", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __y += __rowHeight;

                    __columnRect = new RectangleF(80, __y, 200, __rowHeight);
                    e.Graphics.DrawString("รวมจำนวนเงินทั้งหมด", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(300, __y, 120, __rowHeight);
                    e.Graphics.DrawString(MyLib._myGlobal._decimalPhase(_tableDoc.Rows[0]["total_net_value"].ToString()).ToString("#,###,###.00"), _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(400, __y, 50, __rowHeight);
                    e.Graphics.DrawString("บาท", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(450, __y, 200, __rowHeight);
                    e.Graphics.DrawString("จำนวนเงินที่เก็บได้   ................................", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(680, __y, 50, __rowHeight);
                    e.Graphics.DrawString("บาท", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __y += __rowHeight;

                    __columnRect = new RectangleF(80, __y, 200, __rowHeight);
                    e.Graphics.DrawString("ผู้เก็บเงิน   ................................", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                    __columnRect = new RectangleF(450, __y, 600, __rowHeight);
                    e.Graphics.DrawString("ผู้รับเงิน   ................................", _fontNormal16, Brushes.Black, __columnRect, __rowFormat1);

                }
                else
                {
                    __hasMorePage = true;
                }
            }

            // draw total page
            if (pageIndex != pageCount)
            {
                RectangleF __rect = new RectangleF(_paperWidth - (_paddingRight + 300), __y, 300, __rowHeight);
                e.Graphics.DrawString("วันที่พิมพ์ " + DateTime.Now.ToString("d/M/yyyy", new CultureInfo("en-US")) + " หน้าที่ " + pageIndex + "/" + pageCount, _fontNormal14, Brushes.Black, __rect, new StringFormat() { Alignment = StringAlignment.Far });
                __y += __rowHeight;
            }

            pageIndex++;

            if (__hasMorePage)
                e.HasMorePages = __hasMorePage;

        }


        #endregion

        void _searchTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _searchARButton_Click(this, null);
            }
        }

        void _invoiceDocGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            /*
             string __docNo = this._cellGet(row, _g.d.ap_ar_trans_detail._billing_no).ToString();
            if (__docNo.Length > 0)
            {
                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                DataTable __getData = __process._arBalanceDoc(this._transControlType, 0, this._getCustCode(), this._getCustCode(), __docNo, __docNo, this._getProcessDate(), "");
                if (__getData != null && __getData.Rows.Count > 0)
                {
                    DataRow __dataRow = __getData.Rows[0];
                    int __docType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ap_ar_resource._doc_type_number].ToString());
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._billing_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._doc_date].ToString()), false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._bill_type, __docType, false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._due_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._due_date].ToString()), false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_debt_amount, __dataRow[_g.d.ap_ar_resource._amount], false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._balance_ref, __dataRow[_g.d.ap_ar_resource._ar_balance], false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_pay_money, __dataRow[_g.d.ap_ar_resource._ar_balance], false);
                    this._cellUpdate(row, _g.d.ap_ar_resource._ref_doc_no, __dataRow[_g.d.ap_ar_resource._ref_doc_no], false);
                    this._cellUpdate(row, _g.d.ap_ar_resource._ref_doc_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._ref_doc_date].ToString()), false);
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบรายการ กรุณาตรวจสอบใหม่") + " : " + __docNo);
                    this._clearRow(row);
                }
            }
            else
            {
                this._clearRow(row);
            }
            // คำนวณยอดใหม่
            this._screen._reCalc();
             
             */
            string __getDocNo = this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._doc_no).ToString();
            string __getARCode = this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._ar_code).ToString();

            if (this._ar_ap_trans1._detailGrid._findData(this._ar_ap_trans1._detailGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no), __getDocNo) == -1)
            {

                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                DataTable __getData = __process._arBalanceDoc(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล, 0, __getARCode, __getARCode, __getDocNo, __getDocNo, this._ar_ap_trans1._screenTop._getDataDate(_g.d.ap_ar_trans._doc_date), "");
                if (__getData != null && __getData.Rows.Count > 0)
                {
                    int addr = this._ar_ap_trans1._detailGrid._addRow();
                    DataRow __dataRow = __getData.Rows[0];
                    int __docType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ap_ar_resource._doc_type_number].ToString());

                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._billing_no, __getDocNo, true);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._billing_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._doc_date].ToString()), false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._bill_type, __docType, false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._due_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._due_date].ToString()), false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._sum_debt_amount, __dataRow[_g.d.ap_ar_resource._amount], false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._balance_ref, __dataRow[_g.d.ap_ar_resource._ar_balance], false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._sum_pay_money, __dataRow[_g.d.ap_ar_resource._ar_balance], false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_resource._ref_doc_no, __dataRow[_g.d.ap_ar_resource._ref_doc_no], false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_resource._ref_doc_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._ref_doc_date].ToString()), false);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._cust_name, this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._ar_name), true);
                    this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._cust_code, __getARCode, true);

                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบรายการ กรุณาตรวจสอบใหม่") + " : " + __getDocNo);
                }





                //this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._billing_no, __getDocNo, true);

                //this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._cust_name, __getARCode, true);
                //this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._cust_code, this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._ar_name), true);
                //this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._billing_no, this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._doc_no), true);
                //this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._billing_date, this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._doc_date), true);
                //this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._due_date, this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._due_date), true);
                //this._ar_ap_trans1._detailGrid._cellUpdate(addr, _g.d.ap_ar_trans_detail._sum_debt_amount, this._invoiceDocGrid._cellGet(e._row, _g.d.ap_ar_trans._total_net_value), true);
            }

        }

        void _searchARButton_Click(object sender, EventArgs e)
        {
            // ประกอบ where
            string __searchTextTrim = this._searchTextbox.Text.Trim();
            string[] __searchTextSplit = __searchTextTrim.Split(' ');

            StringBuilder __where = new StringBuilder();
            if (__searchTextSplit.Length > 1)
            {
                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                {
                    bool __whereFirst = false;
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                            bool __first2 = false;
                            for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                            {
                                if (__searchTextSplit[__searchIndex].Length > 0)
                                {
                                    string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                    string __newDateValue = __getValue;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            //
                                            decimal __newValue = 0M;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == false)
                                                    {
                                                        if (__where.Length > 0)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __where.Append("(");
                                                        __whereFirst = true;
                                                    }
                                                    if (__first2)
                                                    {
                                                        __where.Append(" and ");
                                                    }
                                                    __first2 = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            if (__whereFirst == false)
                                            {
                                                if (__where.Length > 0)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __where.Append("(");
                                                __whereFirst = true;
                                            }
                                            if (__first2)
                                            {
                                                __where.Append(" and ");
                                            }
                                            __first2 = true;
                                            //
                                            //if (this._addQuotWhere)
                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                            //else
                                            __where.Append(string.Concat("(" + __getColumnType._query + ")"));
                                            if (this._searchTextbox.Text[0] == '+')
                                            {
                                                __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                            }
                                            else
                                            {
                                                __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                            }
                                            break;
                                    }
                                }
                            }
                            if (__whereFirst)
                            {
                                __where.Append(")");
                            }
                        }
                    }
                } // for
            }
            else
            {
                bool __whereFirst = false;
                for (int __loop = 0; __loop < this._arGrid._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._arGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณีการค้นหาตัวเดียว
                            if (this._searchTextbox.Text.Length > 0)
                            {
                                try
                                {
                                    string __getValue = this._searchTextbox.Text;
                                    string __newDateValue = __getValue;
                                    Boolean __valueExtra = false;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                        __valueExtra = true;
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            double __newValue = 0;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = Double.Parse(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            //
                                            if (__valueExtra == false)
                                            {
                                                if (__whereFirst == true)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __whereFirst = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                __where.Append(string.Concat("(" + __getColumnType._query + ")"));
                                                if (this._searchTextbox.Text[0] == '+')
                                                {
                                                    __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                }
                                                else
                                                {
                                                    __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                }
                                            }
                                            break;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                } // for
            }

            try
            {
                DataRow[] __getSearchResult = this._getDataAR.Select(__where.ToString());
                this._arGrid._loadFromDataTable(this._getDataAR, __getSearchResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void _loadAR()
        {
            string __where = "";

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1 from ar_customer " + ((__where.Length > 0) ? " where " + __where : "") + " order by code"));


            __query.Append("</node>");
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            this._getDataAR = ((DataSet)__result[0]).Tables[0];

            this._arGrid._loadFromDataTable(this._getDataAR);
        }

        void _arGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __getARCode = this._arGrid._cellGet(e._row, _g.d.ar_customer._code).ToString();

            if (__getARCode.Length > 0)
            {
                // load doc balance
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();


                /*
                StringBuilder __query = new StringBuilder();
                __query.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.ap_ar_trans._doc_no, _g.d.ap_ar_trans._doc_date, _g.d.ap_ar_trans._due_date, _g.d.ap_ar_trans._total_net_value,
                  _g.d.ap_ar_trans._cust_code, "( select name_1 from ar_customer where ar_customer.code= " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + ") as " + _g.d.ap_ar_trans._cust_name) + " from " + _g.d.ap_ar_trans._table + " where " + "(" + _g.d.ap_ar_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล).ToString() + ")) and  " + _g.d.ap_ar_trans._cust_code + "=\'" + __getARCode + "\' and coalesce(doc_success, 0)=0 ");

                DataTable __result = __myFrameWork._queryShort(__query.ToString()).Tables[0];

                this._invoiceDocGrid._loadFromDataTable(__result);
                */

                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                DataTable __data = __process._arBalanceDoc(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล, 0, __getARCode, __getARCode, "", "", DateTime.Now, _g.d.ap_ar_resource._due_date, "");
                if (__data != null)
                {
                    this._invoiceDocGrid._loadFromDataTable(__data);
                }

            }

        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }
    }
}
