using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using SMLReport._formReport;
using System.IO;
using System.Xml.Serialization;
using SMLReport._design;
using SMLBarcodeManage;
using System.Drawing.Drawing2D;

namespace SMLERPIC
{
    public partial class _icQualityLabelPrint : Form
    {
        string _docNo;
        string _transFlag;
        public _icQualityLabelPrint(string docNo, string transflag)
        {
            InitializeComponent();
            this._docNo = docNo;
            this._transFlag = transflag;

            _build();
            _loadData();
        }

        void _build()
        {
            int __default = 0;
            int __count = 0;
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault)
                {
                    __default = __count;
                }

                _printerCombobox.Items.Add(__printerName);
                __count++;
            }

            _printerCombobox.SelectedIndex = __default;

            // set form
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.formdesign._formname + ", " + _g.d.formdesign._formcode + " from " + _g.d.formdesign._table + " where coalesce(" + _g.d.formdesign._form_type + ", 0) in (2) "));

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

                this._formCombobox.DataSource = __result.Tables[0];
                this._formCombobox.DisplayMember = _g.d.formdesign._formname;
                this._formCombobox.ValueMember = _g.d.formdesign._formcode;
                // 

            }

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            //string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            //string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._printGrid._table_name = _g.d.ic_quality_control._table;
            this._printGrid._addRowEnabled = false;
            this._printGrid.WidthByPersent = true;
            this._printGrid._addColumn("check", 11, 10, 10);
            this._printGrid._addColumn(_g.d.ic_quality_control._ic_code, 1, 10, 10);
            this._printGrid._addColumn(_g.d.ic_quality_control._ic_name, 1, 20, 20);

            this._printGrid._addColumn(_g.d.ic_quality_control._lot_number, 1, 15, 15, false, true);
            this._printGrid._addColumn(_g.d.ic_quality_control._mfn, 1, 15, 15, false, false, true);
            this._printGrid._addColumn(_g.d.ic_quality_control._mfd_date, 4, 15, 15, false, false, true);
            this._printGrid._addColumn(_g.d.ic_quality_control._exp_date, 4, 15, 15, false, true);


            this._printGrid._addColumn(_g.d.ic_quality_control._lab_no, 1, 20, 20);
            this._printGrid._addColumn(_g.d.ic_quality_control._lab_retest_date, 4, 20, 20);
            //this._printGrid._addColumn(_g.d.ic_quality_control._ic_name, 1, 15, 15);
            //this._printGrid._addColumn(_g.d.ic_quality_control., 1, 15, 15);
            //this._addColumn(_g.d.ic_trans_detail._qty, 3, 15, 15);
            this._printGrid._addColumn(_g.d.ic_quality_control._print_qty, 2, 15, 15, true, false, true, false, "#,###");
            this._printGrid._addColumn(_g.d.ic_quality_control._start_qty, 2, 15, 15, true, false, true, false, "#,###");

            this._printGrid._addColumn(_g.d.ic_quality_control._ic_qc_status, 2, 15, 15, true, true, true, false, "#,###");

            this._printGrid._calcPersentWidthToScatter();

        }

        void _loadData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            try
            {

                // int __getTransFlagColumnNumber = this._myManageData._dataList._gridData._findColumnByName(_g.d.ic_trans._trans_flag);
                //int __getDocNoColumnNumber = this._myManageData._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                // int __transFlag = MyLib._myGlobal._intPhase(((ArrayList)rowData)[__getTransFlagColumnNumber].ToString());
                //  string __getDocNo = ((ArrayList)rowData)[__getDocNoColumnNumber].ToString();
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                // __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._doc_date + " from " + _g.d.ic_trans._table + " " + whereString + " and " + _g.d.ic_trans._trans_flag + " = " + __transFlag.ToString()));

                string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code + " as " + _g.d.ic_quality_control._ic_code
                    , _g.d.ic_trans_detail._item_name + " as " + _g.d.ic_quality_control._ic_name
                    , _g.d.ic_trans_detail._lot_number_1 + " as " + _g.d.ic_quality_control._lot_number
                    , _g.d.ic_trans_detail._date_expire + " as " + _g.d.ic_quality_control._exp_date
                    , _g.d.ic_trans_detail._mfn_name + " as " + _g.d.ic_quality_control._mfn
                    , " ( select " + _g.d.ic_quality_control._ic_qc_status + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "   and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "  and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._line_number + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " ) as " + _g.d.ic_quality_control._ic_qc_status
                    , _g.d.ic_trans_detail._mfd_date + " as " + _g.d.ic_quality_control._mfd_date
                    //, " ( select " + _g.d.ic_quality_control._mfd_date + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + " ) as " + _g.d.ic_quality_control._mfd_date
                    , " ( select " + _g.d.ic_quality_control._lab_no + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "   and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "  and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._line_number + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + ") as " + _g.d.ic_quality_control._lab_no
                    , " ( select " + _g.d.ic_quality_control._lab_retest_date + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "  and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "  and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._line_number + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " ) as " + _g.d.ic_quality_control._lab_retest_date);
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __field + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + this._docNo + "\' and " + _g.d.ic_trans_detail._trans_flag + " = " + this._transFlag.ToString()));
                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Count > 0)
                {
                    DataSet __detail = (DataSet)__result[0];
                    if (__detail.Tables.Count > 0)
                    {
                        this._printGrid._loadFromDataTable(__detail.Tables[0]);

                        for (int __row = 0; __row < this._printGrid._rowData.Count; __row++)
                        {
                            this._printGrid._cellUpdate(__row, 0, 1, true);
                            this._printGrid._cellUpdate(__row, _g.d.ic_quality_control._print_qty, 1, true);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            _print(true);
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            _print(false);
        }

        void _print(bool isPreview)
        {
            string __formCode = (this._formCombobox.SelectedValue != null) ? this._formCombobox.SelectedValue.ToString() : "";


            if (__formCode.Length > 0)
            {
                string __printerName = this._printerCombobox.SelectedItem.ToString();

                // pack รายการที่จะพิมพ์
                List<_labelQcPrint> __objList = new List<_labelQcPrint>();
                for (int __i = 0; __i < this._printGrid._rowData.Count; __i++)
                {
                    if (this._printGrid._cellGet(__i, 0).ToString().Equals("1"))
                    {


                        int __countLabel = (int)this._printGrid._cellGet(__i, _g.d.ic_quality_control._print_qty);
                        int __startLabel = (int)this._printGrid._cellGet(__i, _g.d.ic_quality_control._start_qty);

                        for (int __count = 0; __count < __countLabel; __count++)
                        {

                            _labelQcPrint __label = new _labelQcPrint();
                            __label._ic_qc_status = MyLib._myGlobal._intPhase(this._printGrid._cellGet(__i, _g.d.ic_quality_control._ic_qc_status).ToString());
                            __label._ic_code = this._printGrid._cellGet(__i, _g.d.ic_quality_control._ic_code).ToString();
                            __label._ic_name = this._printGrid._cellGet(__i, _g.d.ic_quality_control._ic_name).ToString();
                            __label._lot_number = this._printGrid._cellGet(__i, _g.d.ic_quality_control._lot_number).ToString();
                            __label._mfn = this._printGrid._cellGet(__i, _g.d.ic_quality_control._mfn).ToString();
                            __label._mfd_date = MyLib._myGlobal._convertDateFromQuery(this._printGrid._cellGet(__i, _g.d.ic_quality_control._mfd_date).ToString());
                            __label._exp_date = MyLib._myGlobal._convertDateFromQuery(this._printGrid._cellGet(__i, _g.d.ic_quality_control._exp_date).ToString());
                            __label._lab_no = this._printGrid._cellGet(__i, _g.d.ic_quality_control._lab_no).ToString();
                            __label._lab_retest_date = MyLib._myGlobal._convertDateFromQuery(this._printGrid._cellGet(__i, _g.d.ic_quality_control._lab_retest_date).ToString());

                            __label._page = __count + (__startLabel) + 1;
                            __label._total = __countLabel + (__startLabel);

                            __objList.Add(__label);
                        }
                    }

                }


                // ส่งพิมพ์
                if (__objList.Count > 0)
                {
                    //_labelPrintClass __printClass = new _labelPrintClass();
                    _sendPrint(isPreview, __formCode, __printerName, __objList);

                }

            }
        }

        public class _labelQcPrint
        {
            public int _ic_qc_status = 0;
            public string _ic_code = "";
            public string _ic_name = "";
            public string _lot_number = "";
            public string _mfn = "";
            public DateTime _mfd_date;
            public DateTime _exp_date;
            public string _lab_no = "";
            public DateTime _lab_retest_date;

            public int _page = 0;
            public int _total = 0;
        }

        void _sendPrint(bool isPreview, string formCode, string printerName, List<_labelQcPrint> objList)
        {
            string __formCode = formCode; // (this._formCombobox.SelectedValue != null) ? this._formCombobox.SelectedValue.ToString() : "";


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

                List<_labelQcPrint> __objList = objList;
                if (__form != null)
                {

                    float _startYPos = 0f;
                    float _startXPos = 0f;
                    float __paperWidth = 0f;
                    float __paperHeight = 0f;

                    String __paperSize = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PaperSize.ToString();
                    int __labelHeight = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Height;
                    int __labelWidth = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Width;

                    int __currentLabel = 0;

                    PrintPreviewDialog __preview = new PrintPreviewDialog();
                    PrintDocument __printDoc = new PrintDocument();

                    if (__printDoc.PrinterSettings.IsValid)
                    {

                        __printDoc.PrinterSettings.PrinterName = printerName;

                        __printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                        __printDoc.DefaultPageSettings.PaperSize = __printDoc.DefaultPageSettings.PaperSize = new PaperSize(__paperSize, __labelWidth, __labelHeight);


                        int __currentPage = 0;
                        __printDoc.BeginPrint += (_beginPrintSender, _beginPrintEvent) =>
                        {
                            __currentPage = 0;
                            __currentLabel = 0;
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

                            //__g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                            __paperWidth = e1.PageBounds.Width;
                            __paperHeight = e1.PageBounds.Height;


                            // ยังไม่เสร็จ
                            _drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), __objList[__currentPage], new PointF(_startXPos, _startYPos));
                            if (__objList.Count - 1 > __currentPage)
                            {
                                __currentPage++;
                                e1.HasMorePages = true;
                            }
                            else
                            {
                                __currentPage++;
                                e1.HasMorePages = false;
                            }

                        };
                        if (isPreview)
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
            }
        }

        void _drawBarcodeLabelForm(Graphics g, SMLReport._formReport._drawPaper paperStruct, _labelQcPrint label, PointF drawPoint)
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
                            string __str = _replaceString(label, __drawLabel._text); // __drawLabel._text.Replace("&ic_code&", label._ic_code).Replace("&ic_name&", label._ic_name).Replace("&lab_no&", label._lab_no).Replace("&lot_number&", label._lot_number).Replace("&mfn&", label._mfn);

                            // __str = __str.Replace("&page&", label._page.ToString()).Replace("&totalpage&", label._total.ToString());

                            RectangleF __LabelRect = __drawLabel._actualSize;
                            Font __newFont = new Font(__drawLabel._font.FontFamily, __drawLabel._font.Size, __drawLabel._font.Style, __drawLabel._font.Unit, __drawLabel._font.GdiCharSet, __drawLabel._font.GdiVerticalFont);

                            PointF __strDrawPoint = new PointF(drawPoint.X + __LabelRect.X, drawPoint.Y + __LabelRect.Y);

                            // จัดตำแนห่ง แล้วพิมพ์ ตัดบรรทัดด้วยนะ
                            ArrayList __getString = SMLReport._design._drawLabel._cutString(__str, __drawLabel._font, __LabelRect.Width, __drawLabel._charSpace, __drawLabel._charWidth, __drawLabel._padding);
                            SizeF __dataStrSize = SMLReport._formReport._formPrint._getTextSize(__getString, __drawLabel._font, g);
                            PointF __tmpPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __dataStrSize.Width, __dataStrSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                            //__strDrawPoint.Y += __tmpPoint.Y;
                            //__strDrawPoint.X += __tmpPoint.X;


                            for (int __line = 0; __line < __getString.Count; __line++)
                            {
                                SizeF __strLineSize = SMLReport._formReport._formPrint._getTextSize((string)__getString[__line], __drawLabel._font, g);

                                PointF __getDrawPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __strLineSize.Width, __strLineSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                                g.DrawString((string)__getString[__line], __drawLabel._font, new SolidBrush(__drawLabel._foreColor), (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                                __strDrawPoint.Y += __strLineSize.Height;

                            }

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                        }
                        else if (__obj.GetType() == typeof(_drawTextField))
                        {
                            _drawTextField __textField = (_drawTextField)__obj;

                            Pen __pen = new Pen(__textField._lineColor, __textField._penWidth);
                            __pen.DashStyle = __textField._lineStyle;
                            SolidBrush __brush = new SolidBrush(__textField._foreColor);
                            SolidBrush __BgBrush = new SolidBrush(__textField._backColor);

                            RectangleF __LabelRect = __textField._actualSize;
                            Font __newFont = new Font(__textField._font.FontFamily, __textField._font.Size, __textField._font.Style, __textField._font.Unit, __textField._font.GdiCharSet, __textField._font.GdiVerticalFont);

                            PointF __strDrawPoint = new PointF(drawPoint.X + __LabelRect.X, drawPoint.Y + __LabelRect.Y);


                            string __str = _getData(label, __textField.Field, __textField.FieldType.ToString(), __textField.FieldFormat);

                            // จัดตำแนห่ง แล้วพิมพ์ ตัดบรรทัดด้วยนะ
                            ArrayList __getString = SMLReport._design._drawLabel._cutString(__str, __textField._font, __LabelRect.Width, __textField._charSpace, __textField._charWidth, __textField._padding);
                            SizeF __dataStrSize = SMLReport._formReport._formPrint._getTextSize(__getString, __textField._font, g);
                            PointF __tmpPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __dataStrSize.Width, __dataStrSize.Height, __textField._textAlign, __textField._padding);

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                            //__strDrawPoint.Y += __tmpPoint.Y;
                            //__strDrawPoint.X += __tmpPoint.X;


                            for (int __line = 0; __line < __getString.Count; __line++)
                            {
                                SizeF __strLineSize = SMLReport._formReport._formPrint._getTextSize((string)__getString[__line], __textField._font, g);

                                PointF __getDrawPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __strLineSize.Width, __strLineSize.Height, __textField._textAlign, __textField._padding);

                                g.DrawString((string)__getString[__line], __textField._font, new SolidBrush(__textField._foreColor), (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                                __strDrawPoint.Y += __strLineSize.Height;

                            }
                        }
                        else if (__obj.GetType() == typeof(_drawImageField))
                        {
                            _drawImageField __imageField = (_drawImageField)__obj;
                            if (__imageField.FieldType == _FieldType.Barcode)
                            {
                                string __barcode = _getData(label, __imageField.Field, __imageField.FieldType.ToString(), "");
                                __barcode = _replaceString(label, __barcode);

                                // __imageField._showText.Replace("&cust_code&", label._custCode).Replace("&cust_name&", label._custName).Replace("&receive_name&", label._receiveName).Replace("&address&", label._address).Replace("&telephone&", label._telephone).Replace("&fax&", label._fax).Replace("&email&", label._email);
                                if (__imageField._typeBarcode == _barcodeType.BarCode_EAN13)
                                {
                                    try
                                    {
                                        SMLBarcodeManage.Ean13 __barcodeEan13 = new Ean13();
                                        Ean13Settings __setting = new Ean13Settings();
                                        //__setting.Font = __imageField._font;
                                        __setting.BarWidth = 4;
                                        //__setting.BarCodeHeight = __imageField._actualSize.Height;
                                        //__setting.BottomMargin = 20;

                                        __barcodeEan13._createEan13(__barcode, null, __setting);
                                        //__barcodeEan13._createEan13(barcode._barCode, null);
                                        Image __imgBarcode = __barcodeEan13.Paint();

                                        g.DrawImage(__imgBarcode, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                                        //Image __image = __imageField._getBarcodeImage(barcode._barCode, Rectangle.Round(__imageField.Size).Size, __imageField._barcodeAlignment, __imageField._typeBarcode, __imageField._showBarcodeLabel, __imageField._barcodeLabelPosition, __imageField._font, __imageField.RotateFlip, __imageField._foreColor, __imageField._backColor);
                                        //g.DrawImage(__image, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                                    }
                                    catch
                                    {

                                    }

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_Code39)
                                {
                                    SMLBarcodeManage._createCode39 __code39 = new SMLBarcodeManage._createCode39( );
                                    try
                                    {
                                        RectangleF __imageRect = __imageField._actualSize;
                                        __imageRect.X += drawPoint.X;
                                        __imageRect.Y += drawPoint.Y;

                                       /* Image __image = __code39._createBarCode(__barcode, new Code39Settings() { InterCharacterGap = 1 });
                                        g.DrawImage(__image, __imageRect);
                                        */
                                        // __image = SMLReport._design._drawImage._SizeModeImg(Rectangle.Round(new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height)), __image, __imageField.SizeMode);

                                        // g.DrawImage(__image, new PointF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y)); // new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height) );

                                        Image __image = __imageField._getBarcodeImage(__barcode, Rectangle.Round(__imageRect).Size, __imageField._barcodeAlignment, __imageField._typeBarcode, __imageField._showBarcodeLabel, __imageField._barcodeLabelPosition, __imageField._font, __imageField.RotateFlip, __imageField._foreColor, __imageField._backColor);
                                        g.DrawImage(__image, Rectangle.Round(__imageRect));

                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_QRCode)
                                {
                                    //QRCodeLib.QRCode __qrCode = new QRCodeLib.QRCode();
                                    //try
                                    //{
                                    //    g.DrawImage(__qrCode._createQRCode(barcode._barCode), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    //}
                                    //catch
                                    //{
                                    //}

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_UPCA)
                                {/*
                                    BarcoderLib.BarcodeUPCA __barcode = new BarcoderLib.BarcodeUPCA();

                                    try
                                    {
                                        g.DrawImage(__barcode.Encode(__barcode.Substring(0, 11)), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    }
                                    catch
                                    {
                                    }*/

                                }
                            }
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

        private string _cutField(string source)
        {
            // @"\[(\w*)\]+,\s+(\w*)" //// regex field 

            if (source == null)
                return "";
            string[] __split = source.Split(',');
            if (source.Trim().Length == 0 || __split.Length == 0)
            {
                return source;
            }
            string __result = __split[0].Replace("[", "").Replace("]", "");
            return __result;
        }

        private string _replaceString(_labelQcPrint label, string text)
        {
            string __result = text;
            __result = __result.Replace("&ic_code&", label._ic_code).Replace("&ic_name&", label._ic_name).Replace("&lab_no&", label._lab_no).Replace("&lot_number&", label._lot_number).Replace("&mfn&", label._mfn).Replace("&page&", label._page.ToString()).Replace("&totalpage&", label._total.ToString());

            return __result;
        }

        private string _getData(_labelQcPrint label, string field, string type, string format)
        {
            field = _cutField(field);
            if (field == _g.d.ic_quality_control._exp_date)
            {
                return label._exp_date.ToString(format, MyLib._myGlobal._cultureInfo());

            }
            else if (field == _g.d.ic_quality_control._mfd_date)
            {
                return label._mfd_date.ToString(format, MyLib._myGlobal._cultureInfo());

            }
            else if (field == _g.d.ic_quality_control._lab_retest)
            {
                return label._lab_retest_date.ToString(format, MyLib._myGlobal._cultureInfo());
            }
            else if (field == _g.d.ic_quality_control._lab_retest_date)
            {
                return label._lab_retest_date.ToString(format, MyLib._myGlobal._cultureInfo());

            }
            else if (field == _g.d.ic_quality_control._ic_code)
            {
                return label._ic_code;
            }
            else if (field == _g.d.ic_quality_control._ic_name)
            {
                return label._ic_name;
            }
            else if (field == _g.d.ic_quality_control._ic_qc_status)
            {
                return label._ic_qc_status.ToString();

            }
            else if (field == _g.d.ic_quality_control._lab_no)
            {
                return label._lab_no;
            }
            return "";
        }

    }


}
