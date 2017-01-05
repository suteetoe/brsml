using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SMLReport._design;
using System.Drawing.Drawing2D;
using SMLReport._formReport;
using System.IO;
using System.Xml.Serialization;
using System.Drawing.Printing;

namespace SMLTransportLabel
{
    public partial class _printLabelForm : MyLib._myForm
    {
        string _printrecentFileName = "";

        private string _addrId = "";
        int _mode = 0;
        _transportLabelObj __label = new _transportLabelObj();

        public _printLabelForm(int mode, string addr)
        {
            InitializeComponent();
            this._mode = mode;

            this._printrecentFileName = @"c:\smlconfig\translabelprint_" + this._mode.ToString() + ".xml";
            this._addrId = addr;
            this._build();


        }

        void _build()
        {
            _recentPrintLabel __recentPrint = new _recentPrintLabel();

            // อ่าน config
            try
            {
                    TextReader readFile = new StreamReader(this._printrecentFileName);
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(_recentPrintLabel));
                    __recentPrint = (_recentPrintLabel)__xsLoad.Deserialize(readFile);
                    readFile.Close();
            }
            catch
            {
            }


            // set printer 
            int __default = 0;
            int __count = 0;
            //foreach (ManagementObject __getPrinter in __printerList.Get())
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault && (__recentPrint._printerName == null || __recentPrint._printerName.Length == 0))
                {
                    __default = __count;
                }

                if (__printerName.Equals(__recentPrint._printerName))
                {
                    __default = __count;
                }

                _printerCombo.Items.Add(__printerName);
                __count++;
            }
            
            _printerCombo.SelectedIndex = __default;

            // set form
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.formdesign._formname + ", " + _g.d.formdesign._formcode + " from " + _g.d.formdesign._table + ""));

            string __custName = (this._mode.Equals(0)) ? " (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename :
                " (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename;

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, " + __custName + " from " + _g.d.ap_ar_transport_label._table + " where roworder = " + this._addrId + ""));
            __query.Append("</node>");

            ArrayList __queryResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__queryResult.Count > 0)
            {

                DataSet __result = (DataSet)__queryResult[0]; //__myFrameWork._queryShort("select " + _g.d.formdesign._formname + ", " + _g.d.formdesign._formcode +" from " + _g.d.formdesign._table + "");
                //if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                //{
                //    //for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                //    //{
                //    //    _formObject __obj = new _formObject();
                //    //    __obj.Name = __result.Tables[0].Rows[__i][_g.d.formdesign._formcode].ToString();
                //    //    __obj.Value = __result.Tables[0].Rows[__i][_g.d.formdesign._formname].ToString();

                //    //    this._formComboBox.Items.Add(__obj);
                //    //}

                //}

                this._formComboBox.DataSource = __result.Tables[0];
                this._formComboBox.DisplayMember = _g.d.formdesign._formname;
                this._formComboBox.ValueMember = _g.d.formdesign._formcode;

                if (__recentPrint._formCode != null && __recentPrint._formCode.Length > 0)
                {
                    this._formComboBox.SelectedValue = __recentPrint._formCode;
                }

                // label obj
                DataSet __labelResult = (DataSet)__queryResult[1];
                if (__labelResult.Tables.Count > 0 && __labelResult.Tables[0].Rows.Count > 0)
                {
                    __label._custCode = __labelResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._cust_code].ToString();
                    __label._receiveName = __labelResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._name_1].ToString();
                    __label._custName = __labelResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._receivename].ToString();
                    __label._address = __labelResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._address].ToString();
                    __label._telephone = __labelResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._telephone].ToString();
                    __label._fax = __labelResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._fax].ToString();
                    __label._email = __labelResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._email].ToString();
                }
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูล", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            _print(false);
        }

        /// <summary>
        /// Concept 1 : 1
        /// </summary>
        /// <param name="__isPreview"></param>
        private void _print(Boolean __isPreview)
        {
            // print 
            string __formCode = (this._formComboBox.SelectedValue != null) ? this._formComboBox.SelectedValue.ToString() : "";


            if (__formCode.Length > 0)
            {

                SMLReport._formReport._formDesigner __form = new _formDesigner();

                // get form cache 
                string __currentConfigFileName = string.Format("_cache-{0}-{1}-{2}-{3}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, __formCode); // "_cache + formCode + ".xml";
                string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
                string __lastTimeUpdate = "";

                // check cache  update version
                bool _isCache = false;
                try
                {
                    // check xml 
                    StringBuilder __queryCheckCode = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    __queryCheckCode.Append(MyLib._myUtil._convertTextToXmlForQuery("SELECT " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._timeupdate + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + "=\'" + __formCode.ToUpper() + "\'"));
                    __queryCheckCode.Append("</node>");

                    MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                    ArrayList __result = __ws._queryListGetData(MyLib._myGlobal._databaseName, __queryCheckCode.ToString());

                    DataTable __da = ((DataSet)__result[0]).Tables[0];
                    __lastTimeUpdate = __da.Rows[0]["timeupdate"].ToString();

                    SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();

                    TextReader readFile = new StreamReader(__path);
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                    __cacheXML = (SMLReport._formReport.SMLFormDesignXml)__xsLoad.Deserialize(readFile);
                    readFile.Close();

                    if (__lastTimeUpdate == __cacheXML._lastUpdate)
                    {
                        _isCache = true;
                    }
                }
                catch (Exception ex)
                {
                    _isCache = false;
                }

                // not exit get form and write cache
                try
                {
                    if (_isCache)
                    {
                        SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();
                        XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLFormDesignXml));
                        FileStream __readFileStream = new FileStream(__path, FileMode.Open, FileAccess.Read, FileShare.Read);
                        __form._loadFromStream(__readFileStream, null, _openFormMethod.OpenFromServer);
                        __readFileStream.Close();

                    }
                    else
                    {
                        string __query = "SELECT " + _g.d.formdesign._formcode + "," + _g.d.formdesign._guid_code + "," + _g.d.formdesign._formname + "," + _g.d.formdesign._timeupdate + "," + _g.d.formdesign._formdesigntext + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + " =\'" + __formCode.ToUpper() + "\'";

                        MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                        MyLib.SMLJAVAWS.formDesignType __formDesign = __ws._loadForm(MyLib._myGlobal._databaseName, __query);

                        try
                        {
                            // ลองดึงดู ถ้าข้อมูล Compress แล้ว ก็ผ่าน ถ้าไม่ ก็ไปดึงแบบเดิม
                            MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__formDesign._formdesign));
                            __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                            __ms.Close();
                        }
                        catch (Exception _ex_xmlcompress_1)
                        {
                            // กรณีที่ดึงของเก่าที่ไม่ได้ Compress
                            try
                            {
                                MemoryStream __ms = new MemoryStream((byte[])__formDesign._formdesign);
                                __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                                __ms.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }

                        // write cache
                        SMLFormDesignXml __formCacheXML = __form._writeXMLSource(_writeXMLSourceOption.DrawObjectOnly);
                        __formCacheXML._lastUpdate = __lastTimeUpdate;

                        XmlSerializer __colXs = new XmlSerializer(typeof(SMLFormDesignXml));
                        TextWriter __memoryStream = new StreamWriter(__path);
                        __colXs.Serialize(__memoryStream, __formCacheXML);
                        __memoryStream.Close();

                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
                // print 

                if (__form != null)
                {
                    //int __maxPage = __barCodeList.Count;

                    float _startYPos = 0f;
                    float _startXPos = 0f;
                    float __paperWidth = 0f;
                    float __paperHeight = 0f;

                    String __paperSize = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PaperSize.ToString();
                    int __labelHeight = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Height;
                    int __labelWidth = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Width;

                    //int __currentLabel = 0;
                    // get margin bound                

                    PrintPreviewDialog __preview = new PrintPreviewDialog();
                    PrintDocument __printDoc = new PrintDocument();
                    //__printDoc.PrinterSettings.PrinterName = "Canon LBP5050";
                    if (__printDoc.PrinterSettings.IsValid)
                    {
                        __printDoc.PrinterSettings.PrinterName = this._printerCombo.SelectedItem.ToString();

                        __printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                        __printDoc.DefaultPageSettings.PaperSize =
                                                __printDoc.DefaultPageSettings.PaperSize = new PaperSize(__paperSize, __labelWidth, __labelHeight);


                        int __currentPage = 0;
                        __printDoc.BeginPrint += (_beginPrintSender, _beginPrintEvent) =>
                        {
                            __currentPage = 0;
                            //__currentLabel = 0;
                            _startYPos = 0f;
                            _startXPos = 0f;
                        };

                        __printDoc.PrintPage += (s1, e1) =>
                        {
                            e1.PageSettings.Margins.Top = 0;
                            e1.PageSettings.Margins.Left = 0;
                            e1.PageSettings.Margins.Bottom = 0;
                            e1.PageSettings.Margins.Right = 0;

                            _startYPos = 0f;
                            _startXPos = 0f;

                            //e1.Graphics.PageUnit = GraphicsUnit.Millimeter;
                            Graphics __g = e1.Graphics;
                            float __leftMargin = 0f;// (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._leftMarginTextBox.Text));
                            float __topMargin = 0f; // (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._topMarginTextBox.Text));

                            _startXPos += __leftMargin;
                            _startYPos += __topMargin;

                            //__g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                            __paperWidth = e1.PageBounds.Width;
                            __paperHeight = e1.PageBounds.Height;

                            _drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), this.__label, new PointF(_startXPos, _startYPos));

                        };
                        if (__isPreview)
                        {
                            __preview.Document = __printDoc;
                            __preview.ShowDialog();
                        }
                        else
                        {
                            __printDoc.Print();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Printer is invalid.");
                    }
                }

                // save last print form
                _recentPrintLabel __recent = new _recentPrintLabel();
                __recent._printerName = this._printerCombo.SelectedItem.ToString();
                __recent._formCode = __formCode;

                // write xml

                try
                {
                    
                    XmlSerializer __colXs = new XmlSerializer(typeof(_recentPrintLabel));
                    TextWriter __memoryStream = new StreamWriter(this._printrecentFileName);
                    __colXs.Serialize(__memoryStream, __recent);
                    __memoryStream.Close();
                }
                catch
                {
                }

            }
        }

        void _drawBarcodeLabelForm(Graphics g, SMLReport._formReport._drawPaper paperStruct, _transportLabelObj label, PointF drawPoint)
        {
            foreach (Control __control1 in paperStruct.Controls)
            {
                if (__control1.GetType() == typeof(SMLReport._design._drawPanel))
                {
                    SMLReport._design._drawPanel __getControl = (SMLReport._design._drawPanel)__control1;
                    //Graphics __g = __getControl.CreateGraphics();
                    for (int __loop = __getControl._graphicsList._count - 1; __loop >= 0; __loop--)
                    {
                        // call print object
                        SMLReport._design._drawObject __obj = ((SMLReport._design._drawObject)__getControl._graphicsList[__loop]);

                        if (__obj.GetType() == typeof(_drawLabel))
                        {

                            _drawLabel __drawLabel = (_drawLabel)__obj;


                            Pen __pen = new Pen(__drawLabel._lineColor, __drawLabel._penWidth);
                            __pen.DashStyle = __drawLabel._lineStyle;
                            SolidBrush __brush = new SolidBrush(__drawLabel._foreColor);
                            SolidBrush __BgBrush = new SolidBrush(__drawLabel._backColor);
                            string __str = __drawLabel._text.Replace("&cust_code&", label._custCode).Replace("&cust_name&", label._custName).Replace("&receive_name&", label._receiveName).Replace("&address&", label._address).Replace("&telephone&", label._telephone).Replace("&fax&", label._fax).Replace("&email&", label._email);
                            RectangleF __LabelRect = __drawLabel._actualSize;
                            Font __newFont = new Font(__drawLabel._font.FontFamily, __drawLabel._font.Size, __drawLabel._font.Style, __drawLabel._font.Unit, __drawLabel._font.GdiCharSet, __drawLabel._font.GdiVerticalFont);

                            PointF __strDrawPoint = new PointF(drawPoint.X + __LabelRect.X, drawPoint.Y + __LabelRect.Y);

                            // จัดตำแนห่ง แล้วพิมพ์ ตัดบรรทัดด้วยนะ
                            ArrayList __getString = SMLReport._design._drawLabel._cutString(__str, __drawLabel._font, __LabelRect.Width, __drawLabel._charSpace, __drawLabel._charWidth, __drawLabel._padding);
                            SizeF __dataStrSize = SMLReport._formReport._formPrint._getTextSize(__getString, __drawLabel._font, g);

                            PointF __tmpPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __dataStrSize.Width, __dataStrSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                            __strDrawPoint.Y += __tmpPoint.Y;
                            __strDrawPoint.X += __tmpPoint.X;

                            for (int __line = 0; __line < __getString.Count; __line++)
                            {
                                SizeF __strLineSize = SMLReport._formReport._formPrint._getTextSize((string)__getString[__line], __drawLabel._font, g);

                                PointF __getDrawPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __strLineSize.Width, __strLineSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                                g.DrawString((string)__getString[__line], __drawLabel._font, new SolidBrush(__drawLabel._foreColor), (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                                __strDrawPoint.Y += __strLineSize.Height;

                            }

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                        }
                        //else if (__obj.GetType() == typeof(_drawTextField))
                        //{

                        //}
                        else if (__obj.GetType() == typeof(_drawImageField))
                        {
                            _drawImageField __imageField = (_drawImageField)__obj;
                            //if (__imageField.FieldType == _FieldType.Barcode)
                            //{
                            //    if (__imageField._typeBarcode == _barcodeType.BarCode_EAN13)
                            //    {
                            //        try
                            //        {
                            //            SMLBarcodeManage.Ean13 __barcodeEan13 = new Ean13();
                            //            Ean13Settings __setting = new Ean13Settings();
                            //            //__setting.Font = __imageField._font;
                            //            __setting.BarWidth = 4;
                            //            //__setting.BarCodeHeight = __imageField._actualSize.Height;
                            //            //__setting.BottomMargin = 20;

                            //            __barcodeEan13._createEan13(label._barCode, null, __setting);
                            //            //__barcodeEan13._createEan13(barcode._barCode, null);
                            //            Image __imgBarcode = __barcodeEan13.Paint();

                            //            g.DrawImage(__imgBarcode, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                            //            //Image __image = __imageField._getBarcodeImage(barcode._barCode, Rectangle.Round(__imageField.Size).Size, __imageField._barcodeAlignment, __imageField._typeBarcode, __imageField._showBarcodeLabel, __imageField._barcodeLabelPosition, __imageField._font, __imageField.RotateFlip, __imageField._foreColor, __imageField._backColor);
                            //            //g.DrawImage(__image, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                            //        }
                            //        catch
                            //        {

                            //        }

                            //    }
                            //    else if (__imageField._typeBarcode == _barcodeType.BarCode_Code39)
                            //    {
                            //        SMLBarcodeManage._createCode39 __code39 = new SMLBarcodeManage._createCode39();
                            //        try
                            //        {
                            //            g.DrawImage(__code39._createBarCode(label._barCode), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                            //        }
                            //        catch
                            //        {
                            //        }

                            //    }
                            //    else if (__imageField._typeBarcode == _barcodeType.BarCode_QRCode)
                            //    {
                            //        //QRCodeLib.QRCode __qrCode = new QRCodeLib.QRCode();
                            //        //try
                            //        //{
                            //        //    g.DrawImage(__qrCode._createQRCode(barcode._barCode), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                            //        //}
                            //        //catch
                            //        //{
                            //        //}

                            //    }
                            //    else if (__imageField._typeBarcode == _barcodeType.BarCode_UPCA)
                            //    {
                            //        BarcoderLib.BarcodeUPCA __barcode = new BarcoderLib.BarcodeUPCA();

                            //        try
                            //        {
                            //            g.DrawImage(__barcode.Encode(label._barCode.Substring(0, 11)), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                            //        }
                            //        catch
                            //        {
                            //        }

                            //    }
                            //}
                        }
                        else if (__obj.GetType() == typeof(_drawImage))
                        {
                            _drawImage __drawImage = (_drawImage)__obj;

                            Pen __pen = new Pen(__drawImage._lineColor, __drawImage._penWidth);
                            SolidBrush __brush = new SolidBrush(__drawImage._backColor);


                            if (__drawImage.Image != null)
                            {

                                float __x = __drawImage._actualSize.X + drawPoint.X;
                                float __y = __drawImage._actualSize.Y + drawPoint.Y;
                                float __width = __drawImage._actualSize.Width;
                                float __height = __drawImage._actualSize.Height;

                                // get Image from sizeType


                                Image __newImage = SMLReport._design._drawImage._SizeModeImg(__drawImage._actualSize, __drawImage.Image, __drawImage.SizeMode);

                                if (__drawImage.BorderStyle != SMLReport._design._drawImage.ImageBorderStyleType.None)
                                {
                                    if (__drawImage._penWidth < 1)
                                    {
                                        __drawImage._penWidth = 1;
                                    }
                                    if (__drawImage.BorderStyle == SMLReport._design._drawImage.ImageBorderStyleType.Line)
                                    {
                                        int __newLineWidth = __drawImage._penWidth / 2;
                                        if (__newLineWidth == 0)
                                        {
                                            __newLineWidth = 1;
                                        }
                                        __x += __newLineWidth;
                                        __y += __newLineWidth;
                                        __width -= __newLineWidth;
                                        __height -= __newLineWidth;
                                    }
                                    Pen __newPen = new Pen(__drawImage._lineColor, __drawImage._penWidth);

                                    //g.DrawImage(__newImage, __x, __y, __width, __height);
                                    g.DrawRectangle(__newPen, __drawImage._actualSize);

                                }

                                RectangleF __tmpNewRect = new RectangleF(__x, __y, __width, __height);

                                g.DrawImage(__newImage, __tmpNewRect);
                                //g.DrawRectangle(pen, _drawRectangle._getNormalizedRectangle(__tmpNewRect));
                            }
                            else
                            {
                                g.FillRectangle(__brush, __drawImage._actualSize);
                            }

                            __pen.DashStyle = __drawImage._LineStyle;
                            g.DrawRectangle(__pen, __drawImage._actualSize);

                            __pen.Dispose();
                            __brush.Dispose();
                        }
                        else if (__obj.GetType() == typeof(_drawLine))
                        {
                            _drawLine __drawLine = (_drawLine)__obj;

                            Pen __linePen = new Pen(__drawLine._lineColor, __drawLine._penWidth);
                            __linePen.DashStyle = __drawLine._LineStyle;

                            PointF __tmpStartPoint = __drawLine.StartPoint;
                            __tmpStartPoint.X += drawPoint.X;
                            __tmpStartPoint.Y += drawPoint.Y;

                            PointF __tmpEndPoint = __drawLine.EndPoint;
                            __tmpEndPoint.X += drawPoint.X;
                            __tmpEndPoint.Y += drawPoint.Y;

                            //e.DrawLine(__linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
                            //onDrawLine(e, __linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
                            g.DrawLine(__linePen, __tmpStartPoint, __tmpEndPoint);


                            __linePen.Dispose();
                        }
                        else if (__obj.GetType() == typeof(_drawEllipse))
                        {
                            _drawEllipse __drawEllipse = (_drawEllipse)__obj;
                            Pen __lineEllipse = new Pen(__drawEllipse._lineColor, __drawEllipse._penWidth);
                            __lineEllipse.DashStyle = __drawEllipse._LineStyle;
                            SolidBrush __ellipseBG = new SolidBrush(__drawEllipse._backColor);

                            Rectangle __rect = new Rectangle(__drawEllipse._actualSize.X, __drawEllipse._actualSize.Y, __drawEllipse._actualSize.Width, __drawEllipse._actualSize.Height);

                            g.FillEllipse(__ellipseBG, __rect);
                            g.DrawEllipse(__lineEllipse, __rect);

                            __lineEllipse.Dispose();
                            __ellipseBG.Dispose();

                        }
                        else if (__obj.GetType() == typeof(_drawRectangle))
                        {
                            _drawRectangle __drawRect = (_drawRectangle)__obj;
                            Pen __rectPen = new Pen(__drawRect._lineColor, __drawRect._penWidth);
                            __rectPen.DashStyle = __drawRect._LineStyle;

                            SolidBrush __rectBrush = new SolidBrush(__drawRect._backColor);

                            RectangleF __rect = __drawRect._actualSize;
                            __rect.X += drawPoint.X;
                            __rect.Y += drawPoint.Y;

                            g.FillRectangle(__rectBrush, Rectangle.Round(__rect));
                            //e.DrawRectangle(__rectPen, Rectangle.Round(__rect));
                            g.DrawRectangle(__rectPen, Rectangle.Round(__rect));

                            __rectBrush.Dispose();
                            __rectPen.Dispose();

                        }
                        else if (__obj.GetType() == typeof(_drawRoundedRectangle))
                        {
                            _drawRoundedRectangle __drawRoundedRect = (_drawRoundedRectangle)__obj;

                            Pen __pen = new Pen(__drawRoundedRect._lineColor, __drawRoundedRect._penWidth);
                            __pen.DashStyle = __drawRoundedRect._LineStyle;
                            SolidBrush __brush = new SolidBrush(__drawRoundedRect._backColor);
                            GraphicsPath gfxPath = new GraphicsPath();

                            RectangleF __rect = __drawRoundedRect._actualSize;
                            __rect.X += drawPoint.X;
                            __rect.Y += drawPoint.Y;


                            gfxPath = __drawRoundedRect._getRectangleGraphic(Rectangle.Round(__rect));

                            g.FillPath(__brush, gfxPath);
                            g.DrawPath(__pen, gfxPath);

                            __pen.Dispose();
                            __brush.Dispose();
                        }
                    }
                }
            }
        }

        private void _printPreviewButton_Click(object sender, EventArgs e)
        {
            _print(true);
        }


    }

    public class _formObject
    {
        public string Name;
        public string Value;
    }

    public class _transportLabelObj
    {
        public String _custCode = "";
        public String _custName = "";
        public String _receiveName = "";
        public String _address = "";
        public String _telephone = "";
        public String _fax = "";
        public String _email = "";
        public String _invoiceNo = "";
        public int _page = 1;
        public int _total = 1;
    }

    public class _recentPrintLabel
    {
        public string _printerName { get; set; }
        public string _formCode { get; set; }
    }

}
