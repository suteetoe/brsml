using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing.Imaging;

namespace VR.PrintPreview
{

    public partial class EnhancedPrintPreviewDialog : Form
    {
        private CompositingQuality compositingQuality = CompositingQuality.Default;
        int _rowCount = 0;
        int _no = 1;
        int _numperLine = 0;
        int _rowLine = 1;
        int _pageNum = 0;
        int _pageNo = 0;
        bool _printLine = true;
        bool _getDataRecord = false;
        int _printCount = 0;
        bool _printCheck = false;
        Image __imageLogo;
        MemoryStream ms;
        public Font _fontNormal10 = new Font("Angsana New", 10, FontStyle.Regular);
        public Font _fontNormal12 = new Font("Angsana New", 12, FontStyle.Regular);
        public Font _fontBold12 = new Font("Angsana New", 12, FontStyle.Bold);
        public Font _fontNormal13 = new Font("Angsana New", 13, FontStyle.Regular);
        public Font _fontBold13 = new Font("Angsana New", 13, FontStyle.Bold);
        public Font _fontNormal14 = new Font("Angsana New", 14, FontStyle.Regular);
        public Font _fontBold14 = new Font("Angsana New", 14, FontStyle.Bold);
        public Font _fontNormal16 = new Font("Angsana New", 16, FontStyle.Regular);
        public Font _fontBold16 = new Font("Angsana New", 16, FontStyle.Bold);
        public Font _fontBold18 = new Font("Angsana New", 18, FontStyle.Bold);
        public Font _fontBold20 = new Font("Angsana New", 20, FontStyle.Bold);
        public Font _fontBold22 = new Font("Angsana New", 22, FontStyle.Bold);
        #region PRIVATE FIELDS

        PrintDocument mDocument;
        PageSetupDialog mPageSetupDialog;
        PrintDialog mPrintDialog;
        //PageSettings mPageSetting;
        int mVisibilePages = 1;
        int mTotalPages = 0;
        bool mShowPageSettingsButton = true;
        bool mShowPrinterSettingsButton = true;
        bool mShowPrinterSettingsBeforePrint = true;
        // List<AdditionalText> mAdditionalTextList;
        public bool __headerBox;
        public bool __detailBox;
        public bool __buttomBox;

        public string _paperSourceName = "";
        public int _printQuality = 0;
        public int _box1 = 0;
        public int _box2 = 0;
        public int _box3 = 0;
        public int _box4 = 0;
        public int _box5 = 0;
        public bool _printerSelect = false;
        public string __pageEnum = "";
        public string __docnoResult = "";
        public string __docdateResult = "";
        public string __arcodeResult = "";
        public string __refDocno = "";
        public int __transFlag;
        public int __transType;
        double __sumtaxOldAmount = 0;
        double __sumQty = 0;
        DataSet __dsDetail;
        DataSet __dsTop;
        DataSet __dsCustomer;
        DataSet __dsTopSub;
        // DataSet __dsTop
        string __strHeader1 = "";
        string __strHeader2 = "RECEIPT/TAX INVOICE";
        string __docNostr = "";
        string __docDateStr = "";
        string __compayName = "";
        string __companyAddress = "";
        string __companyAddress2 = "";
        string __companyTel = "";
        string __companyFax = "";
        string __companyTaxStr = "";
        string __companyTax = "";
        int __companyType = 0;
        string __companyBranchCode = "";

        string __customerCodeStr = "";
        string __customerCode = "";
        string __customerTitle = "";
        string __customerName = "";
        string __customerTax = "";
        string __customerAddress = "";
        string __customerAddress2 = "";
        string __customerTel = "";
        string __customerFax = "";
        string __customerAddressStr = "";
        string __customerTelStr = "";
        string __customerFaxStr = "";
        string __customerEmail = "";


        string __creditStr = "";

        string __duedatestr = "";
        string __remark = "";
        string __itemnoStr = "";
        string __itemcodeStr = "";
        string __descriptionStr = "";
        string __unitStr = "";
        string __qtyStr = "";
        string __priceStr = "";
        string __discountStr = "";
        string __totalAmountStr = "";
        string __saledetailStr = "";
        string __saletelStr = "";
        string __sumpriceStr = "";
        string __sumdiscountStr = "";
        string __sumvatStr = "";
        string __sumtotalAmountStr = "";
        string __moneyRemarkStr = "";
        string __quotationSumPriceStr = "";
        string __quotationApprovalStr = "";
        string __quotationSaleCodeStr = "";


        string __docDateRefer = "";
        string __docNoRefer = "";
        string __amountRefer = "";
        //string __sale        
        #endregion

        public EnhancedPrintPreviewDialog(string __page, string __docno, string __docdate, string __arcode, ArrayList __printOption)
        {
            InitializeComponent();
            printPreviewControl1.StartPageChanged += new EventHandler(printPreviewControl1_StartPageChanged);
            ShowPrinterSettingsButton = false;
            __pageEnum = __page;
            __docnoResult = __docno;
            __docdateResult = __docdate;
            __arcodeResult = __arcode;
            try
            {
                for (int __loop = 0; __loop < __printOption.Count; __loop++)
                {
                    SMLInventoryControl._gForm._printoption __getPrintOption = (SMLInventoryControl._gForm._printoption)__printOption[__loop];
                    _paperSourceName = __getPrintOption._paperSource;
                    _printQuality = __getPrintOption._printQuality;
                    _box1 = __getPrintOption._box1; //กรอบบน
                    _box2 = __getPrintOption._box2; //กรอบกลาง
                    _box3 = __getPrintOption._box3; //เส้นคู่กรอบกลาง ล่าง
                    _box4 = __getPrintOption._box4; //เส้นแนวตั้ง
                    _box5 = __getPrintOption._box5; //กรอบล่าง
                    _printerSelect = __getPrintOption._printerSelect;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region PROPERTIES



        public PageSetupDialog PageSetupDialog
        {
            get
            {
                if (mPageSetupDialog == null) mPageSetupDialog = new PageSetupDialog();
                return mPageSetupDialog;
            }
            set { mPageSetupDialog = value; }
        }

        public PrintDialog PrintDialog
        {
            get
            {
                if (mPrintDialog == null) mPrintDialog = new PrintDialog();
                return mPrintDialog;
            }
            set { mPrintDialog = value; }
        }

        public bool ShowPrinterSettingsButton
        {
            get { return mShowPrinterSettingsButton; }
            set
            {
                mShowPrinterSettingsButton = value;
                tsBtnPrinterSettings.Visible = value;
            }
        }

        public bool ShowPageSettingsButton
        {
            get { return mShowPageSettingsButton; }
            set
            {
                mShowPageSettingsButton = value;
                tsBtnPageSettings.Visible = value;
            }
        }

        public bool ShowPrinterSettingsBeforePrint
        {
            get { return mShowPrinterSettingsBeforePrint; }
            set { mShowPrinterSettingsBeforePrint = value; }
        }

        public PrintPreviewControl PrintPreviewControl { get { return printPreviewControl1; } }

        public PrintDocument Document
        {
            get { return mDocument; }
            set
            {
                SwitchPrintDocumentHandlers(mDocument, false);
                mDocument = value;
                SwitchPrintDocumentHandlers(mDocument, true);
                printPreviewControl1.Document = mDocument;
            }
        }

        public bool UseAntiAlias
        {
            get { return printPreviewControl1.UseAntiAlias; }
            set { printPreviewControl1.UseAntiAlias = value; }
        }

        #endregion


        #region DOCUMENT EVENT HANDLERS

        public void __printData()
        {
            try
            {
                string __pageNumddd = __pageEnum;
                switch (__pageEnum)
                {
                    case "ขาย_ขายสินค้าและบริการ_ici":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ); break;
                    case "ใบเสนอราคา":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา); break;
                    case "ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า); break;
                    case "ขาย_ใบสั่งขาย":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย); break;
                    case "ขาย_เพิ่มหนี้":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้); break;
                    case "ขาย_ลดหนี้":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้); break;
                    case "ซื้อ_ซื้อสินค้าและค่าบริการ":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ); break;
                    case "ซื้อ_ใบสั่งซื้อ":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ); break;
                    case "ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด); break;
                    case "ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า); break;
                    /* jead เดี๋ยวให้เอนกแก้ต่อ
                     * case "ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า": 
                        //__transFlag = 20; break;
                        __transFlag = _g.g._PoSoDepositGlobal._PoSoDepositFlag(_g.g._PoSoDepositControlFlagEnum.po_advance_return);break;*/
                    case "ขาย_รับเงินล่วงหน้า":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า); break;
                    case "ขาย_คืนเงินล่วงหน้า":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน); break;
                    case "ลูกหนี้_ใบวางบิล":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล); break;
                    case "ลูกหนี้_รับชำระหนี้":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้); break;
                    case "เจ้าหนี้_ใบรับวางบิล":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล); break;
                    case "เจ้าหนี้_จ่ายชำระหนี้":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้); break;
                    case "ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด); break;
                    case "สินค้า_รับสินค้าสำเร็จรูป":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป); break;
                    case "สินค้า_เบิกสินค้าวัตถุดิบ":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ); break;
                    case "สินค้า_รับคืนสินค้าจากการเบิก":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก); break;
                    case "สินค้า_โอนออก":
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก); break;

                }
                if (_printerSelect)
                {

                    using (var dlg = new PrintDialog())
                    {
                        // configure dialog
                        dlg.AllowSomePages = false;
                        dlg.AllowSelection = false;
                        dlg.UseEXDialog = true;
                        dlg.Document = this.mDocument;

                        // show allowed page range
                        //var ps = dlg.PrinterSettings;
                        //ps.MinimumPage = ps.FromPage = 1;
                        //ps.MaximumPage = ps.ToPage = 

                        // show dialog
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            // print selected page range
                            _printCheck = true;
                            mDocument.Print();
                            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 1);
                        }
                    }
                }
                else
                {
                    try
                    {
                        _printCheck = true;
                        mDocument.Print();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 1);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        void mDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            mTotalPages = 0;
            _rowCount = 0;
            _pageNo = 0;
            _pageNum = 0;
            _printCount = 0;
            _rowLine = 1;
            __dsCustomer = null;
            __dsDetail = null;
            __dsTop = null;
            __dsTopSub = null;
        }

        private string scandirectory(string _guid_formdatabase)
        {
            string x_return = "";
            DirectoryInfo _dir = new DirectoryInfo(strPathname);
            FileInfo[] rgFiles = _dir.GetFiles(_guid_formdatabase + ".jpg");
            for (int x = 0; x < rgFiles.Length; x++)
            {
                string filename = rgFiles[x].Name.Substring(0, rgFiles[x].Name.Length - 4);
                if (_guid_formdatabase == filename)
                {
                    x_return = rgFiles[x].Name;
                }
                else
                {
                }

            }
            return x_return;
        }

        private IDataObject tempObj;

        public class BMPXMLSerialization
        {
            public int Width;
            public int Height;
            private Bitmap bitmapObject;

            //XmlSerializer can only serialize classes that have default constructor
            public BMPXMLSerialization()
            {
                bitmapObject = null;
            }

            public BMPXMLSerialization(Bitmap bmpToSerialize)
            {

                bitmapObject = bmpToSerialize;
                Width = bmpToSerialize.Width;
                Height = bmpToSerialize.Height;
            }

            public byte[] BMPBytes
            {
                get
                {
                    MemoryStream memStream = new MemoryStream();
                    bitmapObject.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    int ImageSize = Convert.ToInt32(memStream.Length);
                    byte[] ImageBytes = new byte[ImageSize];
                    memStream.Position = 0;
                    memStream.Read(ImageBytes, 0, ImageSize);
                    memStream.Close();
                    return ImageBytes;
                }

                set
                {
                    bitmapObject = new Bitmap(new MemoryStream(value));
                }
            }

            [XmlIgnore]
            public Bitmap Bitmap
            {
                get
                {
                    return bitmapObject;
                }
            }
        }
        string _getCode = "";
        void _imgebytefromdatabaseThread()
        {
            try
            {
                byte[] __databyte = new byte[1024];
                string __xquery = "select guid_code from Images where image_id = \'" + this._getCode + "\'";
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, __xquery);
                DataTable __dt = __ds.Tables[0];

                string xcompare = "";
                for (int __xloop = 0; __xloop < __dt.Rows.Count; __xloop++)
                {
                    string __guid_code = __dt.Rows[__xloop][0].ToString();
                    string __scran = scandirectory(__guid_code);
                    if (__scran.Length != 0)
                        xcompare = __scran.Substring(0, __scran.Length - 4);
                    if (xcompare.Equals(__guid_code))
                    {
                        if (xcompare.CompareTo("noimage") != 0)
                        {
                            BMPXMLSerialization bmpx = new BMPXMLSerialization(new Bitmap(Image.FromFile(strPathname + "\\" + __scran)));
                            __databyte = bmpx.BMPBytes;
                        }
                    }
                    else
                    {
                        if (xcompare.Equals(__guid_code) == false)
                        {
                            string __qurey = "select Image_file from images where guid_code ='" + __guid_code + "'";
                            __databyte = __myFrameWork._ImageByte(MyLib._myGlobal._databaseName, __qurey);
                            FileStream oOutput = File.Create(this.strPathname + @"\" + __guid_code + ".jpg", __databyte.Length);
                            oOutput.Write(__databyte, 0, __databyte.Length);
                            oOutput.Close();
                            oOutput.Dispose();
                        }
                    }
                    try
                    {


                        __imageLogo = Image.FromStream(new MemoryStream(__databyte));
                    }
                    catch
                    {
                    }

                }
                __dt.Dispose();
                __ds.Dispose();
            }
            catch
            {
            }
        }
        Thread __getThread;
        private void _imgebytefromdatabase(string _code)
        {
            this._getCode = _code;
            try
            {
                __getThread.Abort();
                __getThread = null;
            }
            catch
            {
            }
            __getThread = new Thread(new ThreadStart(_imgebytefromdatabaseThread));
            __getThread.Start();
        }

        private string strPathname = @"c:\smltemp";
        private void _createFlodertem()
        {
            try
            {
                if (!Directory.Exists(strPathname))
                {
                    Directory.CreateDirectory(strPathname);
                }
            }
            catch
            {
            }
        }

        MemoryStream __getMemoryStream(string namefile)
        {
            FileStream oImg;
            BinaryReader oBinaryReader;
            byte[] oImgByteArray;

            oImg = new FileStream(namefile, FileMode.Open, FileAccess.Read);
            oBinaryReader = new BinaryReader(oImg);
            oImgByteArray = oBinaryReader.ReadBytes((int)oImg.Length);
            ms = new MemoryStream(oImgByteArray);
            oBinaryReader.Close();
            oImg.Close();
            return ms;
        }

        void __getDataSet(string __page)
        {
            string __result = "";
            try
            {
                _createFlodertem();
                _imgebytefromdatabase(MyLib._myGlobal._ltdName);
                // _imgebytefromdatabase("CONTRACTAP00100002");
                #region ขาย_ขายสินค้าและบริการ
                if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._tax_doc_date + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._sale_code;
                        __myQueryTop += ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += ",(select " + _g.d.erp_user._telephone + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as saletel";
                        __myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\'";

                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";


                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบเสร็จรับเงิน/ใบกำกับภาษี";
                        __strHeader2 = "RECEIPT/TAX INVOICE";
                        __docNostr = "เลขที่";
                        __docDateStr = "วันที่";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ลูกค้า";
                        //-------------------------------------- รหัสลูกค้า
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString(); // +" " + __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerAddress2 = "โทรศัพท์ : " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString() + "   Fax : " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();

                    }
                }
                #endregion
                #region ขาย_ขายสินค้าและบริการ_ici
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ_ici.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._tax_doc_date + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._sale_code + "," + _g.d.ic_trans._discount_word;
                        __myQueryTop += ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += ",(select " + _g.d.erp_user._telephone + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as saletel";
                        __myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\'";

                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + "," + _g.d.ar_customer._ar_status;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._tax_id;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._branch_type;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._branch_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._branch_code;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._card_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._card_id;

                        __myQueryCus += " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";


                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and (" + _g.d.ic_trans_detail._set_ref_qty + " != 1 or " + _g.d.ic_trans_detail._item_type + " = 0) and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;
                        //  __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and (" + _g.d.ic_trans_detail._set_ref_qty + " != 0 or " + _g.d.ic_trans_detail._item_type + " = 0) and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบเสร็จรับเงิน/ใบกำกับภาษี";
                        __strHeader2 = "RECEIPT/TAX INVOICE";
                        __docNostr = "เลขที่";
                        __docDateStr = "วันที่";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __companyType = MyLib._myGlobal._branch_type;
                        __companyBranchCode = MyLib._myGlobal._branch_code;
                        __customerTitle = "ลูกค้า";
                        //-------------------------------------- รหัสลูกค้า
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();

                        if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._ar_status].ToString().Equals("1"))
                        {
                            // นิติบุคคล
                            // ตรวจสอบประเภทกิจการ
                            if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._branch_type].ToString().Equals("1"))
                            {
                                // สาขา
                                __customerName += " สาขาที่ " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._branch_code].ToString() + " ";
                                if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                                {
                                    __customerName += "(เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + ")";
                                }
                            }
                            else
                            {
                                // สนง.ใหญ่
                                __customerName += " สำนักงานใหญ่ ";
                                if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                                {
                                    __customerName += "(เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + ")";
                                }
                            }
                        }
                        else
                        {
                            // บุคคลธรรมดา
                            if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                            {
                                __customerName += " (เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + ")";
                            }
                            else if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._card_id].ToString().Length > 0)
                            {
                                __customerName += " (เลขประจำตัวประชาชน " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._card_id].ToString() + ")";
                            }

                        }

                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString(); // +" " + __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerAddress2 = "โทรศัพท์ : " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString() + "   Fax : " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                //   int __rowdata = __checkRowsNumber(__dsDetail.Tables[0], __page, __xpageWidth, __xgetLeft, e);
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 18;
                                    _numperLine = 18;
                                }
                                else
                                {
                                    __countLinePerRow = 17;
                                    _numperLine = 17;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                #endregion
                #region ใบเสนอราคา
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ใบเสนอราคา.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                        //  __transType = _g.g._ictransTypeGlobal._ictransType(_g.g._ictransControlTypeEnum.ขาย_ใบเสนอราคา);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value + "," + _g.d.ic_trans._total_before_vat;
                        __myQueryTop += ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and (" + _g.d.ic_trans_detail._set_ref_qty + " != 1 or " + _g.d.ic_trans_detail._item_type + " = 0) and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsCustomer = (DataSet)_getData[0];
                        __dsTop = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ชื่อลูกค้า :";
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();

                        //Label
                        __customerAddressStr = "ที่อยู่";
                        __customerTelStr = "โทร";
                        __customerFaxStr = "Fax";

                        __docDateStr = "วันที่/Date:";
                        __docNostr = "เลขที่เอกสาร/No:";
                        __duedatestr = "วันครบกำหนด/DUEDATE:";
                        __creditStr = "เครดิต/CREEDIT DAY:";

                        __itemcodeStr = "รหัสสินค้า";
                        __descriptionStr = "รายการ";
                        __unitStr = "หน่วย";
                        __qtyStr = "จำนวน";
                        __priceStr = "ราคาขาย";
                        __discountStr = "ส่วนลดรายตัว";
                        __totalAmountStr = "จำนวนเงิน";

                        __remark = "หมายเหตุ / Remark :";
                        __moneyRemarkStr = "ยอดเงินตัวหนังสือ :";

                        __sumpriceStr = "รวมจำนวนเงิน :";
                        __sumdiscountStr = "ส่วนลดการค้า :";
                        __quotationSumPriceStr = "ราคาสินค้า/บริการ :";
                        __sumvatStr = "ภาษีมูลค่าเพิ่ม :";
                        __sumtotalAmountStr = "จำนวนเงินรวมทั้งสิ้น :";
                        __quotationApprovalStr = "ผู้อนุมัติ / CUSTOMER :";
                        __quotationSaleCodeStr = "พนักงานขาย / SALE :";
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 22;
                                    _numperLine = 22;
                                }
                                else
                                {
                                    __countLinePerRow = 20;
                                    _numperLine = 20;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    // toe สั่ง print line จาก detail แล้ว
                                    //_printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    // toe สั่ง print line จาก detail แล้ว
                                    // _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                #endregion
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
                        __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._sale_code;
                        __myQueryTop += ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += ",(select " + _g.d.erp_user._telephone + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as saletel";
                        //__myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag + " and " + _g.d.ic_trans._trans_type + "=" + __transType;
                        __myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        //__myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " and " + _g.d.ic_trans_detail._trans_type + "=" + __transType + " order by " + _g.d.ic_trans_detail._item_code;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบสั่งซื้อ/ใบสั่งจองสินค้า";
                        if (MyLib._myGlobal._language == 0)
                        {
                            __strHeader2 = "SALE ORDER";
                        }
                        else
                        {
                            __strHeader2 = "";
                        }
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ชื่อลูกค้า :";
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();

                        //Label
                        __customerAddressStr = "ที่อยู่";
                        __customerTelStr = "โทรศัพท์:";
                        __customerFaxStr = "Fax:";

                        __docDateStr = "วันที่/Date:";
                        __docNostr = "เลขที่เอกสาร/No:";
                        __duedatestr = "วันครบกำหนด/DUEDATE:";
                        __creditStr = "เครดิต/CREEDIT DAY:";
                        __saledetailStr = "พนักงานขาย/ผู้ติดต่อ:";
                        __saletelStr = "โทรศัพท์/Tel:";

                        __itemnoStr = "ลำดับที่";
                        __itemcodeStr = "รหัสสินค้า";
                        __descriptionStr = "รายการ";
                        __unitStr = "หน่วย";
                        __qtyStr = "จำนวน";
                        __priceStr = "ราคาขาย";
                        __discountStr = "ส่วนลดรายตัว";
                        __totalAmountStr = "จำนวนเงิน";

                        __remark = "หมายเหตุ / Remark :";
                        __moneyRemarkStr = "ยอดเงินตัวหนังสือ :";

                        __sumpriceStr = "รวมจำนวนเงิน :";
                        __sumdiscountStr = "ส่วนลดการค้า :";
                        __quotationSumPriceStr = "ราคาสินค้า/บริการ :";
                        __sumvatStr = "ภาษีมูลค่าเพิ่ม :";
                        __sumtotalAmountStr = "จำนวนเงินรวมทั้งสิ้น :";
                        __quotationApprovalStr = "ผู้อนุมัติ / CUSTOMER :";
                        __quotationSaleCodeStr = "พนักงานขาย / SALE :";
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 23;
                                    _numperLine = 23;
                                }
                                else
                                {
                                    __countLinePerRow = 20;
                                    _numperLine = 20;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ใบสั่งขาย.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย);
                        __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_สั่งขาย);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._sale_code;
                        __myQueryTop += ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += ",(select " + _g.d.erp_user._telephone + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as saletel";
                        //  __myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag + " and " + _g.d.ic_trans._trans_type + "=" + __transType;
                        __myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        //__myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " and " + _g.d.ic_trans_detail._trans_type + "=" + __transType + " order by " + _g.d.ic_trans_detail._item_code;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบสั่งขาย";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร :" + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;

                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();
                        if (__dsDetail.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                for (int __loop = 0; __loop < __dsDetail.Tables[0].Rows.Count; __loop++)
                                {
                                    __sumQty = __sumQty + __checkInt(__dsDetail.Tables[0].Rows[__loop][_g.d.ic_trans_detail._qty].ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        //Label
                        __customerCodeStr = "รหัส";
                        __customerTitle = "ชื่อ";
                        __customerAddressStr = "ที่อยู่";

                        __docDateStr = "วันที่/Date:";
                        __docNostr = "เลขที่เอกสาร/No:";
                        __saledetailStr = "พนักงานขาย/ผู้ติดต่อ:";
                        __saletelStr = "โทรศัพท์/Tel:";

                        __itemnoStr = "ลำดับที่";
                        __itemcodeStr = "รหัสสินค้า";
                        __descriptionStr = "รายการ";
                        __qtyStr = "จำนวน";

                        __remark = "หมายเหตุ / Remark :";
                        __sumpriceStr = "รวม :";
                        __quotationApprovalStr = "ผู้อนุมัติ / CUSTOMER :";
                        __quotationSaleCodeStr = "พนักงานขาย / SALE :";
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 23;
                                    _numperLine = 23;
                                }
                                else
                                {
                                    __countLinePerRow = 20;
                                    _numperLine = 20;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                #region ขาย_เพิ่มหนี้
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_เพิ่มหนี้.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้);
                        int __transFlagAdd = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        int __transTypeAdd = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + "," + _g.d.ar_customer._ar_status;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._tax_id;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._branch_type;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._branch_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._branch_code;
                        __myQueryCus += ",(select " + _g.d.ar_customer_detail._card_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._card_id;

                        __myQueryCus += " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._sale_code;
                        __myQueryTop += ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += ",(select " + _g.d.erp_user._telephone + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as saletel";
                        __myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " and " + _g.d.ic_trans_detail._set_ref_qty + " != 1 order by " + _g.d.ic_trans_detail._item_code;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบเพิ่มหนี้/เพิ่มสินค้า Debit Note";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyTel = MyLib._myGlobal._ltdTel;
                        __companyFax = MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __companyType = MyLib._myGlobal._branch_type;
                        __companyBranchCode = MyLib._myGlobal._branch_code;

                        __customerTitle = "ลูกค้า";

                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();

                        if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._ar_status].ToString().Equals("1"))
                        {
                            // นิติบุคคล
                            // ตรวจสอบประเภทกิจการ
                            if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._branch_type].ToString().Equals("1"))
                            {
                                // สาขา
                                __customerTax += "สาขาที่ " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._branch_code].ToString() + " ";
                                if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                                {
                                    __customerTax += "(เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + ")";
                                }
                            }
                            else
                            {
                                // สนง.ใหญ่
                                __customerTax += "สำนักงานใหญ่ ";
                                if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                                {
                                    __customerTax += "(เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + ")";
                                }
                            }
                        }
                        else
                        {
                            // บุคคลธรรมดา
                            if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                            {
                                __customerTax += "เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + "";
                            }
                            else if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._card_id].ToString().Length > 0)
                            {
                                __customerTax += "เลขประจำตัวประชาชน " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._card_id].ToString() + "";
                            }

                        }

                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();

                        //Label
                        __customerCodeStr = "รหัส";
                        __customerTitle = "ชื่อ";
                        __customerAddressStr = "ที่อยู่";

                        __quotationApprovalStr = "ผู้รับมอบอำนาจ";
                        __quotationSaleCodeStr = "ผู้รับเอกสาร";
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        string __getTaxDocNo = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_no].ToString();
                        string __querySub = "select " + _g.d.ic_trans._total_amount + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._tax_doc_no + "=\'" + __getTaxDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlagAdd;
                        StringBuilder __myQuerySub = new StringBuilder();
                        __myQuerySub.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySub));
                        __dsTopSub = ___myFrameWork._query(MyLib._myGlobal._databaseName, __myQuerySub.ToString());
                        if (__dsTopSub.Tables[0].Rows.Count > 0)
                        {
                            __sumtaxOldAmount = Double.Parse(__dsTopSub.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString());
                        }
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 13;
                                    _numperLine = 13;
                                }
                                else
                                {
                                    __countLinePerRow = 10;
                                    _numperLine = 10;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                #endregion
                #region ขาย_ลดหนี้
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ลดหนี้.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        try
                        {
                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้);
                            MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                            string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                            __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                            __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                            __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                            __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + "," + _g.d.ar_customer._ar_status;
                            __myQueryCus += ",(select " + _g.d.ar_customer_detail._tax_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._tax_id;
                            __myQueryCus += ",(select " + _g.d.ar_customer_detail._branch_type + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._branch_type;
                            __myQueryCus += ",(select " + _g.d.ar_customer_detail._branch_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._branch_code;
                            __myQueryCus += ",(select " + _g.d.ar_customer_detail._card_id + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " ) as " + _g.d.ar_customer_detail._card_id;

                            __myQueryCus += " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                            string __myQueryTop = "select " + _g.d.ic_trans._ref_amount + "," + _g.d.ic_trans._ref_new_amount + "," + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname,";
                            __myQueryTop += _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                            __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                            __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + "," + _g.d.ic_trans._vat_type + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                            string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + " = " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + ") as " + _g.d.ic_trans_detail._shelf_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._ref_doc_no + "," + _g.d.ic_trans_detail._ref_doc_date;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                            __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and (coalesce(" + _g.d.ic_trans_detail._item_code_main + ", \'\') = \'\') and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;



                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                            __myquery.Append("</node>");
                            ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            __dsCustomer = (DataSet)_getData[0];
                            __dsTop = (DataSet)_getData[1];
                            __dsDetail = (DataSet)_getData[2];
                            __compayName = MyLib._myGlobal._ltdName;
                            __companyAddress = MyLib._myGlobal._ltdAddress;
                            __companyAddress2 = "";
                            __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                            __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                            __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษีอากร:";
                            __companyTax = MyLib._myGlobal._ltdTax;
                            __companyType = MyLib._myGlobal._branch_type;
                            __companyBranchCode = MyLib._myGlobal._branch_code;

                            __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._code].ToString();
                            __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();

                            if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._ar_status].ToString().Equals("1"))
                            {
                                // นิติบุคคล
                                // ตรวจสอบประเภทกิจการ
                                if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._branch_type].ToString().Equals("1"))
                                {
                                    // สาขา
                                    __customerTax += "สาขาที่ " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._branch_code].ToString() + " ";
                                    if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                                    {
                                        __customerTax += "(เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + ")";
                                    }
                                }
                                else
                                {
                                    // สนง.ใหญ่
                                    __customerTax += "สำนักงานใหญ่ ";
                                    if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                                    {
                                        __customerTax += "(เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + ")";
                                    }
                                }
                            }
                            else
                            {
                                // บุคคลธรรมดา
                                if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString().Length > 0)
                                {
                                    __customerTax += "เลขประจำตัวผู้เสียภาษีอากร " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._tax_id].ToString() + "";
                                }
                                else if (__dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._card_id].ToString().Length > 0)
                                {
                                    __customerTax += "เลขประจำตัวประชาชน " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer_detail._card_id].ToString() + "";
                                }

                            }

                            __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                            __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                            __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                            __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();
                            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                            ////if (__dsDetail.Tables[0].Rows.Count > 0)
                            ////{
                            ////    double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                            ////    double __countLinePerRow = 0;
                            ////    if (_paperSourceName.Equals("A4"))
                            ////    {
                            ////        __countLinePerRow = 22;
                            ////        _numperLine = 22;
                            ////    }
                            ////    else
                            ////    {
                            ////        __countLinePerRow = 19;
                            ////        _numperLine = 19;
                            ////    }
                            ////    double __totalPage = __countRow / (__countLinePerRow + 1);
                            ////    int __pageper = Convert.ToInt32(__totalPage);
                            ////    if (__pageper < __totalPage)
                            ////    {
                            ////        __pageper = __pageper + 1;
                            ////        _pageNum = __pageper;
                            ////    }
                            ////    else
                            ////    {
                            ////        _pageNum = __pageper;
                            ////    }
                            ////    if (_pageNum > 1)
                            ////    {
                            ////        _pageNo = 1;
                            ////        _printLine = false;
                            ////    }
                            ////    else
                            ////    {
                            ////        _pageNo = 1;
                            ////        _printLine = true;
                            ////    }
                            ////}                          
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {


                                __docNoRefer = __dsDetail.Tables[0].Rows[0][_g.d.ic_trans_detail._ref_doc_no].ToString(); ;
                                int __transFlagTemp = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                                string __myQueryTopsub = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._total_amount + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNoRefer + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlagTemp;
                                StringBuilder __myquery2 = new StringBuilder();
                                __myquery2.Append(MyLib._myGlobal._xmlHeader + "<node>");
                                __myquery2.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTopsub));
                                __myquery2.Append("</node>");
                                ArrayList _getData2 = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery2.ToString());
                                __dsTopSub = (DataSet)_getData2[0];

                                __amountRefer = __dsTopSub.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString();
                                __docDateRefer = MyLib._myGlobal._convertDateFromQuery(__dsTopSub.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 12;
                                    _numperLine = 12;
                                }
                                else
                                {
                                    __countLinePerRow = 9;
                                    _numperLine = 9;
                                }
                                double __totalPage = __countRow / (__countLinePerRow + 1);
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                #endregion
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าและค่าบริการ.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryCus = "select " + _g.d.ap_supplier._code + "," + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as province";
                        __myQueryCus += "," + _g.d.ap_supplier._zip_code + "," + _g.d.ap_supplier._telephone + "," + _g.d.ap_supplier._fax + "," + _g.d.ap_supplier._email + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._tax_doc_date + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._sale_code;
                        __myQueryTop += ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += ",(select " + _g.d.erp_user._telephone + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as saletel";
                        __myQueryTop += " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\'";

                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบซื้อสินค้า/บริการ";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyTel = "โทร :" + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;

                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._address].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                        //__customerAddress2 =  __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._fax].ToString();
                        __customerEmail = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._email].ToString();
                        //Label
                        __customerCodeStr = "รหัส";
                        __customerTitle = "ชื่อ";
                        __customerAddressStr = "ที่อยู่";
                        __quotationApprovalStr = "ผู้รับมอบอำนาจ";
                        __quotationSaleCodeStr = "ผู้รับเอกสาร";
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 19;
                                    _numperLine = 19;
                                }
                                else
                                {
                                    __countLinePerRow = 16;
                                    _numperLine = 16;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ใบสั่งซื้อ.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ap_supplier._code + "," + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as province";
                        __myQueryCus += "," + _g.d.ap_supplier._zip_code + "," + _g.d.ap_supplier._telephone + "," + _g.d.ap_supplier._fax + "," + _g.d.ap_supplier._email + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=\'" + __arcodeResult + "\'";


                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._set_ref_qty + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsCustomer = (DataSet)_getData[0];
                        __dsTop = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบสั่งซื้อ";
                        if (MyLib._myGlobal._language == 0)
                        {
                            __strHeader2 = "PURCHASE ORDER";
                        }
                        else
                        {
                            __strHeader2 = "";
                        }
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร :" + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ชื่อลูกค้า";
                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._fax].ToString();
                        __customerEmail = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._email].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        if (__dsDetail.Tables[0].Rows.Count > 0)
                        {
                            double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                            double __countLinePerRow = 0;
                            if (_paperSourceName.Equals("A4"))
                            {
                                __countLinePerRow = 19;
                                _numperLine = 19;
                            }
                            else
                            {
                                __countLinePerRow = 17;
                                _numperLine = 17;
                            }
                            double __totalPage = __countRow / __countLinePerRow;
                            int __pageper = Convert.ToInt32(__totalPage);
                            if (__pageper < __totalPage)
                            {
                                __pageper = __pageper + 1;
                                _pageNum = __pageper;
                            }
                            else
                            {
                                _pageNum = __pageper;
                            }
                            if (_pageNum > 1)
                            {
                                _pageNo = 1;
                                _printLine = false;
                            }
                            else
                            {
                                _pageNo = 1;
                                _printLine = true;
                            }
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด);
                        int __transFlagAdd = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname";
                        __myQueryTop += "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._total_after_vat + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + "," + _g.d.ic_trans._ref_amount + "," + _g.d.ic_trans._ref_new_amount + "," + _g.d.ic_trans._ref_diff + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._ref_doc_no + "," + _g.d.ic_trans_detail._set_ref_qty;
                        __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                        __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsDetail = (DataSet)_getData[1];

                        __refDocno = __dsDetail.Tables[0].Rows[0][_g.d.ic_trans_detail._ref_doc_no].ToString();
                        string __querySub = "select " + _g.d.ic_trans._total_amount + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + "=\'" + __refDocno + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlagAdd;
                        StringBuilder __myQuerySub = new StringBuilder();
                        __myQuerySub.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySub));
                        __dsTopSub = ___myFrameWork._query(MyLib._myGlobal._databaseName, __myQuerySub.ToString());
                        if (__dsTopSub.Tables[0].Rows.Count > 0)
                        {
                            __sumtaxOldAmount = Double.Parse(__dsTopSub.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()); //มูลค่าใบกำกับภาษีเดิม
                        }
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        if (__dsDetail.Tables[0].Rows.Count > 0)
                        {
                            double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                            double __countLinePerRow = 0;
                            if (_paperSourceName.Equals("A4"))
                            {
                                __countLinePerRow = 23;
                                _numperLine = 23;
                            }
                            else
                            {
                                __countLinePerRow = 20;
                                _numperLine = 20;
                            }
                            double __totalPage = __countRow / (__countLinePerRow + 1);
                            int __pageper = Convert.ToInt32(__totalPage);
                            if (__pageper < __totalPage)
                            {
                                __pageper = __pageper + 1;
                                _pageNum = __pageper;
                            }
                            else
                            {
                                _pageNum = __pageper;
                            }
                            if (_pageNum > 1)
                            {
                                _pageNo = 1;
                                _printLine = false;
                            }
                            else
                            {
                                _pageNo = 1;
                                _printLine = true;
                            }
                        }

                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryCus = "select " + _g.d.ap_supplier._code + "," + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as province";
                        __myQueryCus += "," + _g.d.ap_supplier._zip_code + "," + _g.d.ap_supplier._telephone + "," + _g.d.ap_supplier._fax + "," + _g.d.ap_supplier._email + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname";
                        __myQueryTop += "," + _g.d.ic_trans._total_after_vat + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._pay_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsCustomer = (DataSet)_getData[0];
                        __dsTop = (DataSet)_getData[1];
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ชื่อลูกค้า";
                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._fax].ToString();
                        __customerEmail = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._email].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        //__transFlag = _g.g._ictransFlagGlobal._ictransFlag(_g.g._ictransControlTypeEnum.ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า);
                        //__transFlag = 20;
                        // เอนกแก้ต่อ __transFlag = _g.g._PoSoDepositGlobal._PoSoDepositFlag(_g.g._PoSoDepositControlFlagEnum.po_advance_return);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryCus = "select " + _g.d.ap_supplier._code + "," + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as province";
                        __myQueryCus += "," + _g.d.ap_supplier._zip_code + "," + _g.d.ap_supplier._telephone + "," + _g.d.ap_supplier._fax + "," + _g.d.ap_supplier._email + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                        __myQueryTop += "," + _g.d.ic_trans._doc_ref + "," + _g.d.ic_trans._money + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._pay_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsCustomer = (DataSet)_getData[0];
                        __dsTop = (DataSet)_getData[1];
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ชื่อลูกค้า";
                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._fax].ToString();
                        __customerEmail = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._email].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_รับเงินล่วงหน้า.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                        __myQueryTop += "," + _g.d.ic_trans._sale_code + ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._pay_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsCustomer = (DataSet)_getData[0];
                        __dsTop = (DataSet)_getData[1];
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ชื่อลูกค้า";
                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();
                        __customerEmail = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._email].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_คืนเงินล่วงหน้า.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname," + _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                        __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                        __myQueryTop += "," + _g.d.ic_trans._sale_code + ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._sale_code + ") as " + _g.d.ic_trans._sale_name;
                        __myQueryTop += "," + _g.d.ic_trans._doc_ref + "," + _g.d.ic_trans._money + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._pay_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsCustomer = (DataSet)_getData[0];
                        __dsTop = (DataSet)_getData[1];
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร :" + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;
                        __customerTitle = "ชื่อลูกค้า";
                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();
                        __customerEmail = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._email].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ลูกหนี้_ใบวางบิล.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ap_ar_trans._doc_no + "," + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._credit_day + "," + _g.d.ap_ar_trans._due_date + "," + _g.d.ap_ar_trans._remark + "," + _g.d.ap_ar_trans._total_net_value;
                        __myQueryTop += "," + _g.d.ap_ar_trans._sale_code + ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sale_code + ") as " + _g.d.ap_ar_trans._sale_name;
                        __myQueryTop += "  from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ap_ar_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._billing_no + "," + _g.d.ap_ar_trans_detail._billing_date + "," + _g.d.ap_ar_trans_detail._due_date + "," + _g.d.ap_ar_trans_detail._sum_pay_money + " as " + _g.d.ap_ar_trans_detail._sum_debt_value + "," + _g.d.ap_ar_trans_detail._balance_ref + " as " + _g.d.ap_ar_trans_detail._sum_value + "," + _g.d.ap_ar_trans_detail._bill_type + " from " + _g.d.ap_ar_trans_detail._table;
                        __myQueryDetail += " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ap_ar_trans_detail._line_number;
                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบวางบิล";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;

                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 23;
                                    _numperLine = 23;
                                }
                                else
                                {
                                    __countLinePerRow = 20;
                                    _numperLine = 20;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ลูกหนี้_รับชำระหนี้.ToString()))
                {
                    if (__dsDetail == null)
                    {

                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ar_customer._code + "," + _g.d.ar_customer._name_1 + "," + _g.d.ar_customer._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ar_customer._table + "." + _g.d.ar_customer._province + ") as province";
                        __myQueryCus += "," + _g.d.ar_customer._zip_code + "," + _g.d.ar_customer._telephone + "," + _g.d.ar_customer._fax + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ap_ar_trans._doc_no + "," + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._remark;
                        __myQueryTop += "," + _g.d.ap_ar_trans._sale_code + ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sale_code + ") as " + _g.d.ap_ar_trans._sale_name;
                        __myQueryTop += "," + _g.d.ap_ar_trans._total_value + "," + _g.d.ap_ar_trans._total_before_vat + "," + _g.d.ap_ar_trans._total_vat_value + "," + _g.d.ap_ar_trans._total_discount + "," + _g.d.ap_ar_trans._total_net_value;
                        __myQueryTop += "," + _g.d.ap_ar_trans._total_pay_money + "," + _g.d.ap_ar_trans._total_debt_balance;
                        __myQueryTop += "  from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ap_ar_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._billing_no + "," + _g.d.ap_ar_trans_detail._billing_date + "," + _g.d.ap_ar_trans_detail._due_date + "," + _g.d.ap_ar_trans_detail._sum_debt_value + "," + _g.d.ap_ar_trans_detail._sum_value;
                        __myQueryDetail += "," + _g.d.ap_ar_trans_detail._sum_pay_money + "," + _g.d.ap_ar_trans_detail._sum_debt_balance + "," + _g.d.ap_ar_trans_detail._bill_type + " from " + _g.d.ap_ar_trans_detail._table;
                        __myQueryDetail += " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ap_ar_trans_detail._line_number;

                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;

                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ar_customer._fax].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 23;
                                    _numperLine = 23;
                                }
                                else
                                {
                                    __countLinePerRow = 20;
                                    _numperLine = 20;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }


                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบรับวางบิล.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ap_supplier._code + "," + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as province";
                        __myQueryCus += "," + _g.d.ap_supplier._zip_code + "," + _g.d.ap_supplier._telephone + "," + _g.d.ap_supplier._fax + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ap_ar_trans._doc_no + "," + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._credit_day + "," + _g.d.ap_ar_trans._due_date + "," + _g.d.ap_ar_trans._remark + "," + _g.d.ap_ar_trans._total_value + "," + _g.d.ap_ar_trans._total_discount + "," + _g.d.ap_ar_trans._total_before_vat + "," + _g.d.ap_ar_trans._total_vat_value + "," + _g.d.ap_ar_trans._total_net_value + "," + _g.d.ap_ar_trans._vat_rate;
                        __myQueryTop += "," + _g.d.ap_ar_trans._sale_code + ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sale_code + ") as " + _g.d.ap_ar_trans._sale_name;
                        __myQueryTop += "  from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ap_ar_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._billing_no + "," + _g.d.ap_ar_trans_detail._billing_date + "," + _g.d.ap_ar_trans_detail._due_date + "," + _g.d.ap_ar_trans_detail._balance_ref + "," + _g.d.ap_ar_trans_detail._sum_debt_value + ", " + _g.d.ap_ar_trans_detail._sum_pay_money + "," + _g.d.ap_ar_trans_detail._bill_type + " from " + _g.d.ap_ar_trans_detail._table;
                        __myQueryDetail += " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ap_ar_trans_detail._line_number;
                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบรับวางบิล";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;

                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._fax].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 23;
                                    _numperLine = 23;
                                }
                                else
                                {
                                    __countLinePerRow = 20;
                                    _numperLine = 20;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบจ่ายชำระหนี้.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้);
                        MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                        string __myQueryCus = "select " + _g.d.ap_supplier._code + "," + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address;
                        __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as tambon";
                        __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as amper";
                        __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as province";
                        __myQueryCus += "," + _g.d.ap_supplier._zip_code + "," + _g.d.ap_supplier._telephone + "," + _g.d.ap_supplier._fax + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=\'" + __arcodeResult + "\'";

                        string __myQueryTop = "select " + _g.d.ap_ar_trans._doc_no + "," + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._credit_day + "," + _g.d.ap_ar_trans._due_date + "," + _g.d.ap_ar_trans._remark + "," + _g.d.ap_ar_trans._total_value + "," + _g.d.ap_ar_trans._total_discount + "," + _g.d.ap_ar_trans._total_before_vat + "," + _g.d.ap_ar_trans._total_vat_value + "," + _g.d.ap_ar_trans._total_debt_balance + "," + _g.d.ap_ar_trans._vat_rate + "," + _g.d.ap_ar_trans._total_net_value + "," + _g.d.ap_ar_trans._total_debt_value + "," + _g.d.ap_ar_trans._total_pay_money;
                        __myQueryTop += "," + _g.d.ap_ar_trans._sale_code + ",(select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._table + "." + _g.d.erp_user._code + "=" + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._sale_code + ") as " + _g.d.ap_ar_trans._sale_name;
                        __myQueryTop += "  from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ap_ar_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + __transFlag;

                        string __myQueryDetail = "select " + _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._billing_no + "," + _g.d.ap_ar_trans_detail._billing_date + "," + _g.d.ap_ar_trans_detail._due_date + "," + _g.d.ap_ar_trans_detail._sum_debt_value + "," + _g.d.ap_ar_trans_detail._balance_ref + "," + _g.d.ap_ar_trans_detail._sum_pay_money + "," + _g.d.ap_ar_trans_detail._sum_debt_balance + "," + _g.d.ap_ar_trans_detail._bill_type + " from " + _g.d.ap_ar_trans_detail._table;
                        __myQueryDetail += " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ap_ar_trans_detail._line_number;
                        StringBuilder __myquery = new StringBuilder();
                        __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                        __myquery.Append("</node>");
                        ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                        __dsTop = (DataSet)_getData[0];
                        __dsCustomer = (DataSet)_getData[1];
                        __dsDetail = (DataSet)_getData[2];
                        __strHeader1 = "ใบจ่ายชำระหนี้";
                        __compayName = MyLib._myGlobal._ltdName;
                        __companyAddress = MyLib._myGlobal._ltdAddress;
                        __companyAddress2 = "";
                        __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                        __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                        __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                        __companyTax = MyLib._myGlobal._ltdTax;

                        __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._code].ToString();
                        __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                        __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._address].ToString();
                        __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                        __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._telephone].ToString();
                        __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._fax].ToString();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        try
                        {
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 23;
                                    _numperLine = 23;
                                }
                                else
                                {
                                    __countLinePerRow = 20;
                                    _numperLine = 20;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        try
                        {
                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด);
                            MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                            string __myQueryCus = "select " + _g.d.ap_supplier._code + "," + _g.d.ap_supplier._name_1 + "," + _g.d.ap_supplier._address;
                            __myQueryCus += ",(select " + _g.d.erp_tambon._name_1 + "  from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._tambon + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._amper + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_tambon._table + "." + _g.d.erp_tambon._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as tambon";
                            __myQueryCus += ",(select " + _g.d.erp_amper._name_1 + " from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._table + "." + _g.d.erp_amper._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._amper + " and " + _g.d.erp_amper._table + "." + _g.d.erp_amper._province + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as amper";
                            __myQueryCus += ",(select " + _g.d.erp_province._name_1 + " from " + _g.d.erp_province._table + " where " + _g.d.erp_province._table + "." + _g.d.erp_province._code + "=" + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._province + ") as province";
                            __myQueryCus += "," + _g.d.ap_supplier._zip_code + "," + _g.d.ap_supplier._telephone + "," + _g.d.ap_supplier._fax + "," + _g.d.ap_supplier._email + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=\'" + __arcodeResult + "\'";

                            string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname,";
                            __myQueryTop += _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                            __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                            __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                            string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._ref_doc_no;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                            __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryCus));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                            __myquery.Append("</node>");
                            ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            __dsCustomer = (DataSet)_getData[0];
                            __dsTop = (DataSet)_getData[1];
                            __dsDetail = (DataSet)_getData[2];
                            __compayName = MyLib._myGlobal._ltdName;
                            __companyAddress = MyLib._myGlobal._ltdAddress;
                            __companyAddress2 = "";
                            __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                            __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                            __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                            __companyTax = MyLib._myGlobal._ltdTax;

                            __customerCode = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._code].ToString();
                            __customerName = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._name_1].ToString();
                            __customerAddress = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._address].ToString();
                            __customerAddress2 = __dsCustomer.Tables[0].Rows[0]["tambon"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["amper"].ToString() + " " + __dsCustomer.Tables[0].Rows[0]["province"].ToString() + " " + __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._zip_code].ToString();
                            __customerTel = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._telephone].ToString();
                            __customerFax = __dsCustomer.Tables[0].Rows[0][_g.d.ap_supplier._fax].ToString();
                            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 22;
                                    _numperLine = 22;
                                }
                                else
                                {
                                    __countLinePerRow = 19;
                                    _numperLine = 19;
                                }
                                double __totalPage = __countRow / (__countLinePerRow + 1);
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับสินค้าสำเร็จรูป.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        try
                        {
                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป);
                            MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                            string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._credit_date + "," + _g.d.ic_trans._credit_day + "," + _g.d.ic_trans._department_code + ",(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._table + "." + _g.d.erp_department_list._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._department_code + ") as departmentname,";
                            __myQueryTop += _g.d.ic_trans._doc_ref + "," + _g.d.ic_trans._doc_ref_date + ",";
                            __myQueryTop += _g.d.ic_trans._total_value + "," + _g.d.ic_trans._total_discount + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._vat_rate + "," + _g.d.ic_trans._total_vat_value;
                            __myQueryTop += "," + _g.d.ic_trans._cust_code + ",(select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code + ") as custName ," + _g.d.ic_trans._contactor;
                            __myQueryTop += "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + "," + _g.d.ic_trans._tax_doc_no + "," + _g.d.ic_trans._tax_doc_date + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                            string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._ref_doc_no;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                            __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                            __myquery.Append("</node>");
                            ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            __dsTop = (DataSet)_getData[0];
                            __dsDetail = (DataSet)_getData[1];
                            __compayName = MyLib._myGlobal._ltdName;
                            __companyAddress = MyLib._myGlobal._ltdAddress;
                            __companyAddress2 = "";
                            __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                            __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                            __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                            __companyTax = MyLib._myGlobal._ltdTax;
                            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 26;
                                    _numperLine = 26;
                                }
                                else
                                {
                                    __countLinePerRow = 24;
                                    _numperLine = 24;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_เบิกสินค้าวัตถุดิบ.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        try
                        {
                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ);
                            MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                            string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._doc_ref_date + "," + _g.d.ic_trans._doc_ref;
                            __myQueryTop += "," + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                            string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + " and  " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + " ) as " + _g.d.ic_trans_detail._shelf_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._ref_doc_no;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                            __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                            __myquery.Append("</node>");
                            ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            __dsTop = (DataSet)_getData[0];
                            __dsDetail = (DataSet)_getData[1];
                            __compayName = MyLib._myGlobal._ltdName;
                            __companyAddress = MyLib._myGlobal._ltdAddress;
                            __companyAddress2 = "";
                            __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                            __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                            __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                            __companyTax = MyLib._myGlobal._ltdTax;
                            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 26;
                                    _numperLine = 26;
                                }
                                else
                                {
                                    __countLinePerRow = 24;
                                    _numperLine = 24;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับคืนสินค้าจากการเบิก.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        try
                        {
                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก);
                            MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                            string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._doc_ref_date + "," + _g.d.ic_trans._doc_ref;
                            __myQueryTop += "," + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                            string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + ") as " + _g.d.ic_trans_detail._shelf_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._ref_doc_no;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount + "," + _g.d.ic_trans_detail._sum_amount + " from " + _g.d.ic_trans_detail._table;
                            __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                            __myquery.Append("</node>");
                            ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            __dsTop = (DataSet)_getData[0];
                            __dsDetail = (DataSet)_getData[1];
                            __compayName = MyLib._myGlobal._ltdName;
                            __companyAddress = MyLib._myGlobal._ltdAddress;
                            __companyAddress2 = "";
                            __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                            __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                            __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                            __companyTax = MyLib._myGlobal._ltdTax;
                            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_โอนออก.ToString()))
                {
                    if (__dsDetail == null)
                    {
                        try
                        {
                            __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก);
                            MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();

                            string __myQueryTop = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._doc_ref_date + "," + _g.d.ic_trans._doc_ref;
                            __myQueryTop += "," + _g.d.ic_trans._remark + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag;

                            string __myQueryDetail = "select " + _g.d.ic_trans_detail._item_code + ",(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._wh_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code + ") as " + _g.d.ic_trans_detail._shelf_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._wh_code_2 + ",(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code_2 + ") as " + _g.d.ic_trans_detail._wh_name_2;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._shelf_code_2 + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code_2 + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code_2 + ") as " + _g.d.ic_trans_detail._shelf_name_2;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._unit_code + ",(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code + ") as " + _g.d.ic_trans_detail._unit_name;
                            __myQueryDetail += "," + _g.d.ic_trans_detail._qty + " from " + _g.d.ic_trans_detail._table;
                            __myQueryDetail += " where  " + _g.d.ic_trans_detail._doc_no + "=\'" + __docnoResult + "\' and " + _g.d.ic_trans_detail._doc_date + "=\'" + __docdateResult + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag + " order by " + _g.d.ic_trans_detail._line_number;

                            StringBuilder __myquery = new StringBuilder();
                            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTop));
                            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryDetail));
                            __myquery.Append("</node>");
                            ArrayList _getData = ___myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                            __dsTop = (DataSet)_getData[0];
                            __dsDetail = (DataSet)_getData[1];
                            __compayName = MyLib._myGlobal._ltdName;
                            __companyAddress = MyLib._myGlobal._ltdAddress;
                            __companyAddress2 = "";
                            __companyTel = "โทร : " + MyLib._myGlobal._ltdTel;
                            __companyFax = "Fax : " + MyLib._myGlobal._ltdFax;
                            __companyTaxStr = "หมายเลขประจำตัวผู้เสียภาษี:";
                            __companyTax = MyLib._myGlobal._ltdTax;
                            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 0);
                            if (__dsDetail.Tables[0].Rows.Count > 0)
                            {
                                double __countRow = Double.Parse(__dsDetail.Tables[0].Rows.Count.ToString());
                                double __countLinePerRow = 0;
                                if (_paperSourceName.Equals("A4"))
                                {
                                    __countLinePerRow = 21;
                                    _numperLine = 21;
                                }
                                else
                                {
                                    __countLinePerRow = 19;
                                    _numperLine = 19;
                                }
                                double __totalPage = __countRow / __countLinePerRow;
                                int __pageper = Convert.ToInt32(__totalPage);
                                if (__pageper < __totalPage)
                                {
                                    __pageper = __pageper + 1;
                                    _pageNum = __pageper;
                                }
                                else
                                {
                                    _pageNum = __pageper;
                                }
                                if (_pageNum > 1)
                                {
                                    _pageNo = 1;
                                    _printLine = false;
                                }
                                else
                                {
                                    _pageNo = 1;
                                    _printLine = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void __chekPrintData(int __pageFlag, string __docno, string __docdate, int __printStatus)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __myQueryTemp = "select * from " + _g.d.form_print_detail_temp._table + " where " + _g.d.form_print_detail_temp._doc_no + "=\'" + __docno + "\' and " + _g.d.form_print_detail_temp._trans_flag + "=" + __pageFlag;
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__myQueryTemp));
                __myquery.Append("</node>");
                ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                DataSet __dsPrintCount = (DataSet)_getData[0];
                if (__dsPrintCount.Tables[0].Rows.Count > 0)
                {
                    if (__printStatus == 0)
                    {
                        //_printCount = MyLib._myGlobal._intPhase(__dsPrintCount.Tables[0].Rows[0][_g.d.form_print_detail_temp._print_no].ToString())+1;
                        _printCount = MyLib._myGlobal._intPhase(__dsPrintCount.Tables[0].Rows[0][_g.d.form_print_detail_temp._print_no].ToString()) + 1;
                    }
                }
                else
                {
                    _printCount = 0;
                }
                if (__printStatus == 1)
                {
                    string __query = "";
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    if (_printCount == 0)
                    {
                        __query = "insert into " + _g.d.form_print_detail_temp._table + " ( " + _g.d.form_print_detail_temp._trans_flag + "," + _g.d.form_print_detail_temp._doc_no + "," + _g.d.form_print_detail_temp._doc_date + "," + _g.d.form_print_detail_temp._print_no + ")";
                        __query += " values (" + __pageFlag + ",'" + __docno + "','" + __docdate + "'," + _printCount + ")";
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));

                    }
                    else
                    {
                        __query = "update " + _g.d.form_print_detail_temp._table + " set " + _g.d.form_print_detail_temp._print_no + "=" + _printCount + "," + _g.d.form_print_detail_temp._doc_date + "='" + __docdate + "' where " + _g.d.form_print_detail_temp._trans_flag + "=" + __pageFlag + " and  " + _g.d.form_print_detail_temp._doc_no + "=\'" + __docno + "\'";
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                    }
                    __myQuery.Append("</node>");
                    string __resultQuery = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__resultQuery.Length == 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        void mDocument_EndPrint(object sender, PrintEventArgs e)
        {
            tsLblTotalPages.Text = " / " + mTotalPages.ToString();
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
                float __xWidth = __pageWidth;
                #region พิมพเส้น ใบเสนอราคา
                if (__pageEnum.Equals(SMLInventoryControl._gForm._formEnum.ใบเสนอราคา.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    e.SmoothingMode = SmoothingMode.None;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 150, (int)__pageTop, 150, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 150, (int)__pageTop, 150, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 100, 130, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 100);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 100);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 130, 8); } //กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight); }
                    if (_box2 == 1 && _box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50); }
                    if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                        float _xdetailtop = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xdetailtop, __xWidth + __pageLeft, _xdetailtop);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    //กรอบล่างสุด
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footWidth = __xWidth;
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __footWidth, __footHeight, 8); }

                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;

                    float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 27) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 9) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 9) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 11) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 11) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    }
                    float __footerylineTop = __buttomBoxHeight + (__detailTop + __detailHeight) + 3;
                    float __footerylineEnd = __footerylineTop + (__pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight));
                    float __footerxline1 = __pageLeft + __pageWidth / 2;
                    if (_box5 == 1 && _printLine == true) { e.DrawLine(__objectPen, __footerxline1, __footTop, __footerxline1, __footerylineEnd); }
                }
                #endregion
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    float __titleBoxLeft = __pageWidth - 250;
                    float __titleBoxWidth = __titleBoxLeft + 220;
                    if (_box1 == 1) { e.DrawRectangle(__objectPen, __titleBoxLeft, __pageTop + 60, 220, 60); }
                    if (_box1 == 1) { e.DrawLine(__objectPen, __titleBoxLeft, __pageTop + 90, __titleBoxWidth, __pageTop + 90); }
                    float __titleLineLeft1 = __pageWidth - 180;
                    if (_box1 == 1 && _box4 == 1) { e.DrawLine(__objectPen, __titleLineLeft1, __pageTop + 60, __titleLineLeft1, __pageTop + 120); }
                    if (_box1 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __pageTop + 200, __xWidth, 80); }
                    ///เส้นแนวตั้งกรอบบนสุด
                    float __xlineLeft1 = __pageLeft + ((__pageWidth * 26) / 100);
                    float __ylineTop1 = __pageTop + 200;
                    float __ylineEnd1 = __pageTop + 200 + 80;
                    if (_box1 == 1 && _box4 == 1) { e.DrawLine(__objectPen, __xlineLeft1, __ylineTop1, __xlineLeft1, __ylineEnd1); }
                    float __xlineLeft2 = __xlineLeft1 + ((__pageWidth * 13) / 100);
                    if (_box1 == 1 && _box4 == 1) { e.DrawLine(__objectPen, __xlineLeft2, __ylineTop1, __xlineLeft2, __ylineEnd1); }
                    float __xlineLeft3 = __xlineLeft2 + ((__pageWidth * 13) / 100);
                    if (_box1 == 1 && _box4 == 1) { e.DrawLine(__objectPen, __xlineLeft3, __ylineTop1, __xlineLeft3, __ylineEnd1); }
                    //-------------------------------------------------------------------------------------
                    float __detailTop = __pageTop + 203 + 80;
                    float __detailheight = __pageHeight - (__detailTop);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailheight); }
                    if (_box2 == 1 && _box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50); }
                    if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                    }
                    //////เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__xWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    float __footterTopLine = __detailTop + (__detailheight / 2) + 50;
                    if (_box2 == 1 || _box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __footterTopLine, __xWidth + __pageLeft, __footterTopLine); }
                    ///เส้นแนวตั้ง                
                    float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 32) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                    float __yline1 = __pageTop + 283;
                    float __yline2 = __detailTop + (__detailheight / 2) + 50;
                    if (_box4 == 1) { e.DrawLine(__objectPen, __xline1, __yline1, __xline1, __yline2); }
                    if (_box4 == 1) { e.DrawLine(__objectPen, __xline2, __yline1, __xline2, __yline2); }
                    float __xline3Buttom = __detailheight + __yline1;
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline3, __yline1, __xline3, __xline3Buttom);
                        e.DrawLine(__objectPen, __xline4, __yline1, __xline4, __yline2);
                        e.DrawLine(__objectPen, __xline5, __yline1, __xline5, __yline2);
                    }
                    float __lineButtomTop = __yline2 + (_fontBold14.GetHeight(e) * 5);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline6, __yline1, __xline6, __lineButtomTop);
                        e.DrawLine(__objectPen, __xline3, __lineButtomTop, __xWidth + __pageLeft, __lineButtomTop);
                    }


                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า.ToString()))
                {
                    //  e.SmoothingMode = SmoothingMode.HighQuality;
                    __xWidth = __pageWidth - __pageLeft;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 180, (int)__pageTop, 210, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 180, (int)__pageTop, 210, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 100, 130, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 100);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 100);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 130, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight); }
                    if (_box2 == 1 && _box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50); }
                    if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + __detailHeight, __xWidth + __pageLeft, __detailTop + __detailHeight);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 28) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ใบสั่งขาย.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;

                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 150, (int)__pageTop, 150, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 150, (int)__pageTop, 150, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 100, 130, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 100);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 100);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 130, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight); }
                    if (_box2 == 1 && _box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50); }
                    if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + __detailHeight, __xWidth + __pageLeft, __detailTop + __detailHeight);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    //กรอบล่างสุด
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footWidth = __xWidth;
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __footWidth, __footHeight, 8); }
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;

                    float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 22) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 32) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 8) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    }
                    float __footerylineTop = __buttomBoxHeight + (__detailTop + __detailHeight) + 3;
                    float __footerylineEnd = __footerylineTop + (__pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight));
                    float __footerxline1 = __pageLeft + __pageWidth / 3;
                    float __footerxline2 = __footerxline1 + (__pageWidth / 3);
                    if (_box5 == 1 && _box4 == 1 && _printLine == true)
                    {
                        e.DrawLine(__objectPen, __footerxline1, __footTop, __footerxline1, __footerylineEnd);
                        e.DrawLine(__objectPen, __footerxline2, __footTop, __footerxline2, __footerylineEnd);
                    }

                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_เพิ่มหนี้.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    float __detailTop = __pageTop + 320;
                    float __detailWidth = __pageWidth - __pageLeft;
                    float __detailHeight = __pageHeight - (__detailTop);
                    if (_box1 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __pageTop, __detailWidth, 320); }
                    if (_box1 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight); }
                    if (_box1 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50); }
                    if (_box1 == 0 && _box2 == 1 && _printLine == true)
                    {

                        float __xbox2 = __pageHeight - (__detailTop) - 400;
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __xbox2);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box1 == 0 && _box2 == 1 && _printLine == false)
                    {
                        float __xbox2 = __pageHeight - (__detailTop) - 400;
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __xbox2);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box1 == 0 && _box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                        float __xbox2 = __pageHeight - 400;
                        e.DrawLine(__objectPen, __pageLeft, __xbox2, __detailWidth + __pageLeft, __xbox2);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 2, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__detailWidth - 2, 48); }
                    float __footerlineTop = __detailTop + (__detailHeight - 150);
                    if (_box1 == 1 && _printLine == true)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __footerlineTop, __detailWidth + __pageLeft, __footerlineTop);
                    }

                    float __buttomTop = __footerlineTop - 250;
                    if (_box1 == 1) { e.DrawLine(__objectPen, __pageLeft, __buttomTop, __detailWidth + __pageLeft, __buttomTop); }

                    float __ylineTop = __detailTop;
                    float __ylineEnd = __buttomTop;
                    float __xline1 = __pageLeft + ((__xWidth * 10) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 55) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 12) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 12) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        if (_box1 == 0 && _box2 == 1)
                        {
                            e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        }
                        else if (_box1 == 0 && _box2 == 0)
                        {
                            e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        }
                        else if (_box1 == 1 && _printLine == true)
                        {
                            e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __footerlineTop);
                        }
                        else
                        {
                            e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        }
                    }
                    float __buttomSplitTop = __buttomTop + 100;
                    if (_box1 == 1 && _box4 == 1 && _printLine == true)
                    {
                        e.DrawLine(__objectPen, __xline4, __buttomSplitTop, __detailWidth + __pageLeft, __buttomSplitTop);
                    }

                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ลดหนี้.ToString()))
                {
                    //__xWidth = __pageWidth - __pageLeft;
                    //float __detailTop = __pageTop + 263;
                    //float __detailHeight = __pageHeight - (__detailTop + 250);
                    //if (_box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop); }
                    //if (_box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop + 40, __xWidth + __pageLeft, __detailTop + 40); }
                    //float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    //float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 200;
                    //if (_box5 == 1) { e.DrawLine(__objectPen, __pageLeft, __buttomBox1Top, __xWidth + __pageLeft, __buttomBox1Top); }
                    //if (_box5 == 1 && _printLine == true) { e.DrawLine(__objectPen, __pageLeft, __buttomBox1Top + 130, __xWidth + __pageLeft, __buttomBox1Top + 130); }


                    __xWidth = __pageWidth - __pageLeft;
                    float __detailTop = __pageTop + 300;
                    float __detailWidth = __xWidth;
                    float __detailHeight = __pageHeight - (__detailTop + 230);
                    //if (_box2 == 1 && _box5 == 1 && _printLine == true)
                    //{
                    e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                    // }
                    //else if (_box2 == 1 && _box5 == 1 && _printLine == false)
                    //{
                    float __xdetailTop = __pageTop + 300;
                    float __xdetailHeight = __pageHeight - (__xdetailTop) - 230;
                    e.DrawRectangle(__objectPen, __pageLeft, __xdetailTop, __detailWidth, __xdetailHeight);
                    e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                    //}
                    //else if (_box2 == 1 && _box5 == 0)
                    //{
                    //    float __xdetailTop = __pageTop + 273;
                    //    float __xdetailHeight = __pageHeight - (__xdetailTop) - 310;
                    //    e.DrawRectangle(__objectPen, __pageLeft, __xdetailTop, __detailWidth, __xdetailHeight);
                    //    e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                    //}
                    //else if (_box2 == 0 && _box5 == 0 && _box3 == 1)
                    //{
                    //    e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                    //    e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                    //    float __xdetailTop = __pageTop + 273;
                    //    float __xdetailHeight = __pageHeight - (__xdetailTop) - 310;
                    //    float __xbuttomRightTop = (__detailTop + __xdetailHeight);
                    //    e.DrawLine(__objectPen, __pageLeft, __xbuttomRightTop, __detailWidth + __pageLeft, __xbuttomRightTop);22222222
                    //}
                    float __ylineTop = __pageTop + 300;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __ylineMid = __detailTop + __detailHeight - 150;
                    float __xline1 = __pageLeft + ((__xWidth * 10) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 40) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 15) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineMid);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineMid);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineMid);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                    }
                    e.DrawLine(__objectPen, __pageLeft, __ylineMid, __detailWidth + __pageLeft, __ylineMid);
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าและค่าบริการ.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    // e.SmoothingMode = SmoothingMode.AntiAlias;
                    float __box1width = (__xWidth / 2) + 60;
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 130, __box1width, 140, 8); }//กรอบที่ 1 บน
                    float __box2Left = __box1width + __pageLeft;
                    float __box2Width = __xWidth - __box1width;
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 130, __box2Width, 140, 8); }//กรอบที่ 2 บน
                    float __xtoplineTop1 = __box2Left;
                    float __ytoplineTop1 = __pageTop + 140;
                    float __ytoplineTop2 = __pageTop + 180;
                    float __ytoplineTop3 = __pageTop + 220;
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 273;
                    float __detailWidth = __xWidth;
                    float __detailHeight = __pageHeight - __detailTop - 30;
                    if (_box2 == 1 && _box5 == 1 && _printLine == true)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                    }
                    else if (_box2 == 1 && _box5 == 1 && _printLine == false)
                    {
                        float __xdetailTop = __pageTop + 273;
                        float __xdetailHeight = __pageHeight - (__xdetailTop) - 310;
                        e.DrawRectangle(__objectPen, __pageLeft, __xdetailTop, __detailWidth, __xdetailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                    }
                    else if (_box2 == 1 && _box5 == 0)
                    {
                        float __xdetailTop = __pageTop + 273;
                        float __xdetailHeight = __pageHeight - (__xdetailTop) - 310;
                        e.DrawRectangle(__objectPen, __pageLeft, __xdetailTop, __detailWidth, __xdetailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                    }
                    else if (_box2 == 0 && _box5 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 47, __detailWidth + __pageLeft, __detailTop + 47);
                        float __xdetailTop = __pageTop + 273;
                        float __xdetailHeight = __pageHeight - (__xdetailTop) - 310;
                        float __xbuttomRightTop = (__detailTop + __xdetailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __xbuttomRightTop, __detailWidth + __pageLeft, __xbuttomRightTop);
                    }
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft, (int)__detailTop + 1, (int)__detailWidth - 1, 45);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__detailWidth - 2, 45); }
                    float __ylineTop = __pageTop + 273;
                    float __ylineEnd = __detailTop + __detailHeight + 30;
                    float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 33) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 8) / 100);
                    if (_box4 == 1 && _box5 == 1 && _printLine == true)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd - 150);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd - 150);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd - 310);
                    }
                    else if (_box4 == 1 && _box5 == 1 && _printLine == false)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd - 310);
                    }
                    else if (_box4 == 1 && _box5 == 0)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd - 310);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd - 310);
                    }
                    float __ybuttomTop1 = (__detailTop + __detailHeight) - 280;
                    if (_box4 == 1 & _box5 == 1) { e.DrawLine(__objectPen, __pageLeft, __ybuttomTop1, __detailWidth + __pageLeft, __ybuttomTop1); }
                    float __ybuttomTop2 = (__detailTop + __detailHeight) - 248;
                    float __ybuttomTop3 = (__detailTop + __detailHeight) - 216;
                    float __ybuttomTop4 = (__detailTop + __detailHeight) - 184;
                    float __ybuttomTop5 = (__detailTop + __detailHeight) - 152;
                    float __ybuttomTop6 = (__detailTop + __detailHeight) - 120;
                    if (_box4 == 1 && _box5 == 1 && _printLine == true)
                    {
                        e.DrawLine(__objectPen, __xline5, __ybuttomTop2, __detailWidth + __pageLeft, __ybuttomTop2);
                        e.DrawLine(__objectPen, __xline5, __ybuttomTop3, __detailWidth + __pageLeft, __ybuttomTop3);
                        e.DrawLine(__objectPen, __xline5, __ybuttomTop4, __detailWidth + __pageLeft, __ybuttomTop4);
                        e.DrawLine(__objectPen, __xline5, __ybuttomTop5, __detailWidth + __pageLeft, __ybuttomTop5);
                        e.DrawLine(__objectPen, __pageLeft, __ybuttomTop6, __detailWidth + __pageLeft, __ybuttomTop6);
                    }

                    float __footerylineTop = (__detailTop + __detailHeight) - 120;
                    float __footerylineEnd = __footerylineTop + 120;
                    float __footerWidth = (__pageWidth * 50) / 100;
                    float __footerxline1 = __footerWidth;
                    float __footerxline2 = __footerWidth + (__footerWidth / 2);
                    if (_box4 == 1 && _box5 == 1 && _printLine == true)
                    {
                        e.DrawLine(__objectPen, __footerxline1, __footerylineTop, __footerxline1, __footerylineEnd);
                        e.DrawLine(__objectPen, __footerxline2, __footerylineTop, __footerxline2, __footerylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ใบสั่งซื้อ.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 210, (int)__pageTop, 210, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 210, (int)__pageTop, 210, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 150, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 150, 8); } //กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 273;
                    float __detailWidth = __xWidth;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                        float __xtopDetailLine = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, __xtopDetailLine, __detailWidth + __pageLeft, __xtopDetailLine);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    float __ylineTop = __pageTop + 273;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 28) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 11) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToString().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 100, __xWidth, 40, 8); }
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 140, __xWidth, 40, 8); }
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 180, __xWidth, 70, 8); }

                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailHeight = __pageHeight - (__detailTop + 230);
                    if (_box2 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                        float __xtopDetail = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, __xtopDetail, __xWidth + __pageLeft, __xtopDetail);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 1, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 110;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    //กรอบล่างสุด
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footWidth = __xWidth;
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __footWidth, __footHeight, 8); }
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;

                    float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 25) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 6) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                    float __xline7 = __xline6 + ((__xWidth * 8) / 100);
                    float __xline8 = __xline7 + ((__xWidth * 6) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                        e.DrawLine(__objectPen, __xline7, __ylineTop, __xline7, __ylineEnd);
                        e.DrawLine(__objectPen, __xline8, __ylineTop, __xline8, __ylineEnd);
                    }

                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    //  e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 170, (int)__pageTop, 190, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 170, (int)__pageTop, 190, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 140, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 140, 8); }//กรอบที่ 2 บน}
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 265;
                    float __detailHeight = __pageHeight - (__detailTop + 200);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 3) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = 80;
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    //กรอบล่างสุด:ซ้าย
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __buttomBoxWidth, __footHeight, 8); }
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __footTop, __buttomBox2Width, __footHeight, 8); }
                    float __checkBoxTop1 = __footTop + 35;
                    float __checkBoxTop2 = __footTop + 60;
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop1, 20, 20);
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop2, 20, 20);
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    //  e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 170, (int)__pageTop, 190, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 170, (int)__pageTop, 190, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 140, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 140, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 265;
                    float __detailHeight = __pageHeight - (__detailTop + 200);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 3) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = 80;
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    //กรอบล่างสุด:ซ้าย
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __buttomBoxWidth, __footHeight, 8); }
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __footTop, __buttomBox2Width, __footHeight, 8); }
                    float __checkBoxTop1 = __footTop + 35;
                    float __checkBoxTop2 = __footTop + 60;
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop1, 20, 20);
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop2, 20, 20);
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_รับเงินล่วงหน้า.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    //  e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 170, (int)__pageTop, 190, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 170, (int)__pageTop, 190, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 140, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 140, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 265;
                    float __detailHeight = __pageHeight - (__detailTop + 200);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 3) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = 80;
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    //กรอบล่างสุด:ซ้าย
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __buttomBoxWidth, __footHeight, 8); }
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __footTop, __buttomBox2Width, __footHeight, 8); }
                    float __checkBoxTop1 = __footTop + 35;
                    float __checkBoxTop2 = __footTop + 60;
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop1, 20, 20);
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop2, 20, 20);
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_คืนเงินล่วงหน้า.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    //  e.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 170, (int)__pageTop, 190, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 170, (int)__pageTop, 190, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 140, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 140, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 265;
                    float __detailHeight = __pageHeight - (__detailTop + 200);
                    if (_box2 == 1) { e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 3) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = 80;
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    //กรอบล่างสุด:ซ้าย
                    float __footTop = (__buttomBox1Top + __buttomBoxHeight) + 3;
                    float __footHeight = __pageHeight - (__detailTop + __detailHeight + __buttomBoxHeight);
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __footTop, __buttomBoxWidth, __footHeight, 8); }
                    if (_box5 == 1) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __footTop, __buttomBox2Width, __footHeight, 8); }
                    float __checkBoxTop1 = __footTop + 35;
                    float __checkBoxTop2 = __footTop + 60;
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop1, 20, 20);
                    e.DrawRectangle(__objectPen, __pageLeft + 10, __checkBoxTop2, 20, 20);
                }

                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ลูกหนี้_ใบวางบิล.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 210, (int)__pageTop, 210, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 210, (int)__pageTop, 210, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 130, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 130, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailWidth = __xWidth;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                        float _xtopDetail = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xtopDetail, __detailWidth + __pageLeft, _xtopDetail);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 12) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 12) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 14) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ลูกหนี้_รับชำระหนี้.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 200, (int)__pageTop, 230, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 200, (int)__pageTop, 230, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 130, (__xWidth / 2) + 100, 130, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 100);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 100);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 130, __box2Width, 130, 8); } //กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 263;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __xWidth + __pageLeft, __detailTop + 50);
                        float _xtopDetail = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xtopDetail, __xWidth + __pageLeft, _xtopDetail);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 200;
                    if (_box5 == 1 && _printLine == true) { e.DrawRectangle(__objectPen, __pageLeft, __buttomBox1Top, __xWidth, __buttomBoxHeight); }
                    float __ylineTop = __pageTop + 263;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 25) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 12) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 12) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 14) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 14) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        if (_printLine == true)
                        {
                            e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd + __buttomBoxHeight);
                        }
                        else
                        {
                            e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        }
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบรับวางบิล.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 210, (int)__pageTop, 210, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 210, (int)__pageTop, 210, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 130, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 130, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailWidth = __xWidth;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                        float _xtopDetail = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xtopDetail, __detailWidth + __pageLeft, _xtopDetail);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 18) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 18) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบจ่ายชำระหนี้.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 210, (int)__pageTop, 210, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 210, (int)__pageTop, 210, 30); }
                    //กรอบที่ 1 บน
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 120, (__xWidth / 2) + 50, 130, 8); }
                    //กรอบที่ 2 บน
                    float __box2Left = __pageLeft + ((__xWidth / 2) + 55);
                    float __box2Width = __xWidth - ((__xWidth / 2) + 55);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __box2Left, __pageTop + 120, __box2Width, 130, 8); }//กรอบที่ 2 บน
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 253;
                    float __detailWidth = __xWidth;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __detailWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __detailWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 50, __detailWidth + __pageLeft, __detailTop + 50);
                        float _xtopDetail = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xtopDetail, __detailWidth + __pageLeft, _xtopDetail);
                    }
                    //เส้นทึบ
                    Rectangle drawArea1 = new Rectangle((int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 1, 48);
                    LinearGradientBrush linearBrush = new LinearGradientBrush(drawArea1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.ForwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrush, (int)__pageLeft + 1, (int)__detailTop + 1, (int)__xWidth - 2, 48); }
                    //กรอบล่างซ้าย                
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __pageLeft, __buttomBox1Top, __buttomBoxWidth, __buttomBoxHeight, 8); }
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    if (_box5 == 1 && _printLine == true) { DrawRoundRect(e, __objectPen, __buttomBox2Left, __buttomBox1Top, __buttomBox2Width, __buttomBoxHeight, 8); }
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                    float __xline7 = __xline6 + ((__xWidth * 10) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                        e.DrawLine(__objectPen, __xline7, __ylineTop, __xline7, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    float __detailTop = __pageTop + 263;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop); }
                    if (_box3 == 1) { e.DrawLine(__objectPen, __pageLeft, __detailTop + 40, __xWidth + __pageLeft, __detailTop + 40); }
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 200;
                    if (_box5 == 1) { e.DrawLine(__objectPen, __pageLeft, __buttomBox1Top, __xWidth + __pageLeft, __buttomBox1Top); }
                    if (_box5 == 1 && _printLine == true) { e.DrawLine(__objectPen, __pageLeft, __buttomBox1Top + 130, __xWidth + __pageLeft, __buttomBox1Top + 130); }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับสินค้าสำเร็จรูป.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    float __detailTop = __pageTop + 253;
                    float __detailHeight = __pageHeight - (__detailTop) - 170;
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 40, __xWidth + __pageLeft, __detailTop + 40);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 40, __xWidth + __pageLeft, __detailTop + 40);
                        float _xdetailTop = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xdetailTop, __xWidth + __pageLeft, _xdetailTop);
                    }
                    float __ylineTop = __pageTop + 253;
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 6) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 22) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 28) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 12) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __ylineTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __ylineTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __ylineTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __ylineTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __ylineTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __ylineTop, __xline6, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_เบิกสินค้าวัตถุดิบ.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 150, __xWidth, 60, 8); }
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 230;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 60, __xWidth + __pageLeft, __detailTop + 60);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 60, __xWidth + __pageLeft, __detailTop + 60);
                        float _xdetailTop = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xdetailTop, __xWidth + __pageLeft, _xdetailTop);
                    }
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 25) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 35) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __detailTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __detailTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __detailTop, __xline3, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับคืนสินค้าจากการเบิก.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 150, __xWidth, 60, 8); }
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 230;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 60, __xWidth + __pageLeft, __detailTop + 60);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 60, __xWidth + __pageLeft, __detailTop + 60);
                        float _xdetailtop = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xdetailtop, __xWidth + __pageLeft, _xdetailtop);
                    }
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 25) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 35) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __detailTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __detailTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __detailTop, __xline3, __ylineEnd);
                    }
                }
                else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_โอนออก.ToString()))
                {
                    __xWidth = __pageWidth - __pageLeft;
                    Rectangle drawAreaHead = new Rectangle((int)__xWidth - 150, (int)__pageTop, 150, 30);
                    LinearGradientBrush linearBrushHead = new LinearGradientBrush(drawAreaHead, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
                    if (_printQuality == 1) { e.FillRectangle(linearBrushHead, (int)__xWidth - 150, (int)__pageTop, 150, 30); }
                    float __boxLeft1 = ((__xWidth * 50) / 100) + 100;
                    float __boxWidth1 = (__xWidth - (((__xWidth * 50) / 100) + 100)) + __pageLeft;
                    // DrawRoundRect(e, __objectPen, __pageLeft, __pageTop + 150, __xWidth, 60, 8);
                    if (_box1 == 1) { DrawRoundRect(e, __objectPen, __boxLeft1, __pageTop + 80, __boxWidth1, 120, 8); }
                    //กรอบกลาง Detail
                    float __detailTop = __pageTop + 210;
                    float __detailHeight = __pageHeight - (__detailTop + 250);
                    if (_box2 == 1 && _box3 == 1)
                    {
                        e.DrawRectangle(__objectPen, __pageLeft, __detailTop, __xWidth, __detailHeight);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 60, __xWidth + __pageLeft, __detailTop + 60);
                    }
                    else if (_box2 == 0 && _box3 == 1)
                    {
                        e.DrawLine(__objectPen, __pageLeft, __detailTop, __xWidth + __pageLeft, __detailTop);
                        e.DrawLine(__objectPen, __pageLeft, __detailTop + 60, __xWidth + __pageLeft, __detailTop + 60);
                        float _xdetailtop = __detailTop + __detailHeight;
                        e.DrawLine(__objectPen, __pageLeft, _xdetailtop, __xWidth + __pageLeft, _xdetailtop);
                    }
                    float __ylineEnd = __detailTop + __detailHeight;
                    float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 23) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 10) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                    float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                    float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                    float __xline7 = __xline6 + ((__xWidth * 8) / 100);
                    if (_box4 == 1)
                    {
                        e.DrawLine(__objectPen, __xline1, __detailTop, __xline1, __ylineEnd);
                        e.DrawLine(__objectPen, __xline2, __detailTop, __xline2, __ylineEnd);
                        e.DrawLine(__objectPen, __xline3, __detailTop, __xline3, __ylineEnd);
                        e.DrawLine(__objectPen, __xline4, __detailTop, __xline4, __ylineEnd);
                        e.DrawLine(__objectPen, __xline5, __detailTop, __xline5, __ylineEnd);
                        e.DrawLine(__objectPen, __xline6, __detailTop, __xline6, __ylineEnd);
                        e.DrawLine(__objectPen, __xline7, __detailTop, __xline7, __ylineEnd);
                    }
                }
            }

            catch (Exception ex)
            {
            }
        }

        public void __printDataReportICStockTransfer(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            ///SMLInventoryControl._gForm._formEnum.สินค้า_โอนออก
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                RectangleF rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                g.DrawString("ใบโอนสินค้า", _fontBold20, Brushes.Black, rect, sfCenter);

                float __boxLeft1 = ((__xWidth * 50) / 100) + 100;
                float __boxWidth1 = (__xWidth - (((__xWidth * 50) / 100) + 100)) - 125;
                float __cusLeft = __boxLeft1 + 10;
                float __custTop = __pageTop + 90;

                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __detailTop = __pageTop + 230;
                float __detailHeight = __pageHeight - (__detailTop + 250);

                float __headerWidth = ((__xWidth * 50) / 100);
                float __headerLeft = __pageLeft + ((__xWidth * 50) / 100);
                rect = new RectangleF(__cusLeft + 10, __custTop, 120, __custTop);
                g.DrawString("วันที่/Date :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 125, __custTop, __boxWidth1, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 10, __custTop, 120, lineHeight);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 125, __custTop, __boxWidth1, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 10, __custTop, 120, __custTop);
                g.DrawString("วันที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 120, __custTop, __boxWidth1, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).Length > 0)
                {
                    string __setrefdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setrefdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 10, __custTop, 120, lineHeight);
                g.DrawString("เลขที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 125, __custTop, __boxWidth1, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                //----------------------------------------------------------------               
                //หัวตาราง                            
                float __titleTop = __pageTop + 220;
                float __titleTop2 = __pageTop + 250;
                float __titleLeft = __pageLeft;
                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 23) / 100);
                float __xline3 = __xline2 + ((__xWidth * 10) / 100);
                float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                float __xline7 = __xline6 + ((__xWidth * 8) / 100);

                float __xwidth1 = ((__xWidth * 20) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__pageLeft, __titleTop2, __xwidth1, __custTop);
                g.DrawString("Code", _fontNormal12, Brushes.Black, rect, sfCenter);//    
                float __xwidth2 = ((__pageWidth * 25) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รายการสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__xline1, __titleTop2, __xwidth2, __custTop);
                g.DrawString("Name", _fontNormal12, Brushes.Black, rect, sfCenter);//   
                float __xwidth3 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("คลัง/ออก", _fontBold13, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__xline2, __titleTop2, __xwidth3, __custTop);
                g.DrawString("FROM W/H", _fontNormal12, Brushes.Black, rect, sfCenter);//  
                float __xwidth4 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("ที่เก็บ/ออก", _fontBold13, Brushes.Black, rect, sfCenter);//                                               
                rect = new RectangleF(__xline3, __titleTop2, __xwidth4, __custTop);
                g.DrawString("FROM SHELF", _fontNormal12, Brushes.Black, rect, sfCenter);//  
                float __xwidth5 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("คลัง/เข้า", _fontBold13, Brushes.Black, rect, sfCenter);//                                               
                rect = new RectangleF(__xline4, __titleTop2, __xwidth5, __custTop);
                g.DrawString("TO W/H", _fontNormal12, Brushes.Black, rect, sfCenter);//  
                float __xwidth6 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("ที่เก็บ/เข้า", _fontBold13, Brushes.Black, rect, sfCenter);//                                               
                rect = new RectangleF(__xline5, __titleTop2, __xwidth6, __custTop);
                g.DrawString("TO SHELF", _fontNormal12, Brushes.Black, rect, sfCenter);//  
                float __xwidth7 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("หน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//                                               
                rect = new RectangleF(__xline6, __titleTop2, __xwidth7, __custTop);
                g.DrawString("Unit", _fontNormal12, Brushes.Black, rect, sfCenter);//  
                float __xwidth8 = ((__pageWidth * 7) / 100);
                rect = new RectangleF(__xline7, __titleTop, __xwidth8, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//                                               
                rect = new RectangleF(__xline7, __titleTop2, __xwidth8, __custTop);
                g.DrawString("Qty", _fontNormal12, Brushes.Black, rect, sfCenter);//
                if (_printLine == true)
                {
                    float __remarkTop = __detailTop + __detailHeight + 10;
                    rect = new RectangleF(__pageLeft, __remarkTop, __xWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ: ", _fontBold14, Brushes.Black, rect, sfLeft);
                    __remarkTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__pageLeft + 20, __remarkTop, __xWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal14, Brushes.Black, rect, sfLeft);

                    float __xbwidth = (__xWidth * 25) / 100;
                    float __xbline1 = __pageLeft + ((__xWidth * 25) / 100);
                    float __xbline2 = __xbline1 + ((__xWidth * 25) / 100);
                    float __xbline3 = __xbline2 + ((__xWidth * 25) / 100);
                    float __xbTop = __pageHeight - 120;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้จัดทำ/ผู้ตรวจสอบสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้เบิกสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้รับสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้อนุมัติ", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __xbTop += _fontNormal14.GetHeight(g) + 30;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    __xbTop += _fontBold13.GetHeight(g) + 10;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void __printDataReportICStockRequestReturn(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.สินค้า_รับคืนสินค้าจากการเบิก
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += _fontBold14.GetHeight(g);
                RectangleF rect = new RectangleF(__pageLeft, __lineY, __xWidth, lineHeight);
                g.DrawString("ใบรับคืนจากการเบิก", _fontBold20, Brushes.Black, rect, sfCenter);

                __y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 160;
                float __custTop2 = (__pageTop + 160) + _fontBold14.GetHeight(g);
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __detailTop = __pageTop + 230;
                float __detailHeight = __pageHeight - (__detailTop + 250);

                float __headerWidth = ((__xWidth * 50) / 100);
                float __headerLeft = __pageLeft + ((__xWidth * 50) / 100);
                rect = new RectangleF(__cusLeft + 10, __custTop, 120, __custTop);
                g.DrawString("วันที่/Date :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 120, __custTop, __headerWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                rect = new RectangleF(__cusLeft + 10, __custTop2, 120, lineHeight);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 120, __custTop2, __headerWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__headerLeft + 10, __custTop, 120, __custTop);
                g.DrawString("วันที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__headerLeft + 125, __custTop, __headerWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).Length > 0)
                {
                    string __setrefdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setrefdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                rect = new RectangleF(__headerLeft + 10, __custTop2, 120, lineHeight);
                g.DrawString("เลขที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__headerLeft + 125, __custTop2, __headerWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                //----------------------------------------------------------------               
                //หัวตาราง                            
                float __titleTop = __pageTop + 235;
                float __titleTop2 = __pageTop + 260;
                float __titleLeft = __pageLeft;
                float __xline1 = __pageLeft + ((__xWidth * 25) / 100);
                float __xline2 = __xline1 + ((__xWidth * 35) / 100);
                float __xline3 = __xline2 + ((__xWidth * 20) / 100);

                float __xwidth1 = ((__xWidth * 25) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold14, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__pageLeft, __titleTop2, __xwidth1, __custTop);
                g.DrawString("Code", _fontBold14, Brushes.Black, rect, sfCenter);//    
                float __xwidth2 = ((__pageWidth * 35) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รายการสินค้า", _fontBold14, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__xline1, __titleTop2, __xwidth2, __custTop);
                g.DrawString("Name", _fontBold14, Brushes.Black, rect, sfCenter);//   
                float __xwidth3 = ((__pageWidth * 20) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("หน่วย", _fontBold14, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__xline2, __titleTop2, __xwidth3, __custTop);
                g.DrawString("Unit", _fontBold14, Brushes.Black, rect, sfCenter);//  
                float __xwidth4 = ((__pageWidth * 20) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("จำนวน", _fontBold14, Brushes.Black, rect, sfCenter);//                                               
                rect = new RectangleF(__xline3, __titleTop2, __xwidth4, __custTop);
                g.DrawString("Qty", _fontBold14, Brushes.Black, rect, sfCenter);//  

                float __remarkTop = __detailTop + __detailHeight + 10;
                rect = new RectangleF(__pageLeft, __remarkTop, __xWidth, __custTop);
                string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                g.DrawString("หมายเหตุ: ", _fontBold14, Brushes.Black, rect, sfLeft);
                __remarkTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__pageLeft + 20, __remarkTop, __xWidth, __custTop);
                g.DrawString(__remarkResult, _fontNormal14, Brushes.Black, rect, sfLeft);

                float __xbwidth = (__xWidth * 25) / 100;
                float __xbline1 = __pageLeft + ((__xWidth * 25) / 100);
                float __xbline2 = __xbline1 + ((__xWidth * 25) / 100);
                float __xbline3 = __xbline2 + ((__xWidth * 25) / 100);
                float __xbTop = __pageHeight - 120;
                rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้จัดทำ/ผู้ตรวจสอบสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้เบิกสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้รับสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้อนุมัติ", _fontNormal14, Brushes.Black, rect, sfCenter);
                __xbTop += _fontNormal14.GetHeight(g) + 30;
                rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                __xbTop += _fontBold13.GetHeight(g) + 10;
                rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
            }
            catch (Exception ex)
            {
            }
        }

        public void __printDataReportICStockRequest(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            ///SMLInventoryControl._gForm._formEnum.สินค้า_เบิกสินค้าวัตถุดิบ
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += _fontBold14.GetHeight(g);
                RectangleF rect = new RectangleF(__pageLeft, __lineY, __xWidth, lineHeight);
                g.DrawString("ใบเบิกสินค้า", _fontBold20, Brushes.Black, rect, sfCenter);

                __y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 160;
                float __custTop2 = (__pageTop + 160) + _fontBold14.GetHeight(g);
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __detailTop = __pageTop + 230;
                float __detailHeight = __pageHeight - (__detailTop + 250);

                float __headerWidth = ((__xWidth * 50) / 100);
                float __headerLeft = __pageLeft + ((__xWidth * 50) / 100);
                rect = new RectangleF(__cusLeft + 10, __custTop, 120, __custTop);
                g.DrawString("วันที่/Date :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 120, __custTop, __headerWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                rect = new RectangleF(__cusLeft + 10, __custTop2, 120, lineHeight);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 120, __custTop2, __headerWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__headerLeft + 10, __custTop, 120, __custTop);
                g.DrawString("วันที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__headerLeft + 125, __custTop, __headerWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).Length > 0)
                {
                    string __setrefdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setrefdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                rect = new RectangleF(__headerLeft + 10, __custTop2, 120, lineHeight);
                g.DrawString("เลขที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__headerLeft + 125, __custTop2, __headerWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                //----------------------------------------------------------------               
                //หัวตาราง                            
                float __titleTop = __pageTop + 235;
                float __titleTop2 = __pageTop + 260;
                float __titleLeft = __pageLeft;
                float __xline1 = __pageLeft + ((__xWidth * 25) / 100);
                float __xline2 = __xline1 + ((__xWidth * 35) / 100);
                float __xline3 = __xline2 + ((__xWidth * 20) / 100);

                float __xwidth1 = ((__xWidth * 25) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold14, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__pageLeft, __titleTop2, __xwidth1, __custTop);
                g.DrawString("Code", _fontBold14, Brushes.Black, rect, sfCenter);//    
                float __xwidth2 = ((__pageWidth * 35) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รายการสินค้า", _fontBold14, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__xline1, __titleTop2, __xwidth2, __custTop);
                g.DrawString("Name", _fontBold14, Brushes.Black, rect, sfCenter);//   
                float __xwidth3 = ((__pageWidth * 20) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("หน่วย", _fontBold14, Brushes.Black, rect, sfCenter);//                
                rect = new RectangleF(__xline2, __titleTop2, __xwidth3, __custTop);
                g.DrawString("Unit", _fontBold14, Brushes.Black, rect, sfCenter);//  
                float __xwidth4 = ((__pageWidth * 20) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("จำนวน", _fontBold14, Brushes.Black, rect, sfCenter);//                                               
                rect = new RectangleF(__xline3, __titleTop2, __xwidth4, __custTop);
                g.DrawString("Qty", _fontBold14, Brushes.Black, rect, sfCenter);//  

                float __remarkTop = __detailTop + __detailHeight + 10;
                rect = new RectangleF(__pageLeft, __remarkTop, __xWidth, __custTop);
                string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                g.DrawString("หมายเหตุ: ", _fontBold14, Brushes.Black, rect, sfLeft);
                __remarkTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__pageLeft + 20, __remarkTop, __xWidth, __custTop);
                g.DrawString(__remarkResult, _fontNormal14, Brushes.Black, rect, sfLeft);

                float __xbwidth = (__xWidth * 25) / 100;
                float __xbline1 = __pageLeft + ((__xWidth * 25) / 100);
                float __xbline2 = __xbline1 + ((__xWidth * 25) / 100);
                float __xbline3 = __xbline2 + ((__xWidth * 25) / 100);
                float __xbTop = __pageHeight - 120;
                rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้จัดทำ/ผู้ตรวจสอบสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้เบิกสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้รับสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                g.DrawString("ผู้อนุมัติ", _fontNormal14, Brushes.Black, rect, sfCenter);
                __xbTop += _fontNormal14.GetHeight(g) + 30;
                rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                __xbTop += _fontBold13.GetHeight(g) + 10;
                rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                g.DrawString("วันที่_____/______/_______", _fontNormal14, Brushes.Black, rect, sfCenter);
            }
            catch (Exception ex)
            {
            }
        }

        public void __printDataReportICFinishgoods(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            ///SMLInventoryControl._gForm._formEnum.สินค้า_รับสินค้าสำเร็จรูป
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += _fontBold14.GetHeight(g);
                RectangleF rect = new RectangleF(__pageLeft, __lineY, __xWidth, lineHeight);
                g.DrawString("ใบรับสินค้าสำเร็จรูป", _fontBold20, Brushes.Black, rect, sfCenter);

                __y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 140;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 140;
                float __custTop2 = __pageTop + 140;
                float __detailWidth = (__xWidth / 2);
                float __titleTop = __pageTop + 265;
                float __titleLeft = __pageLeft;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 10) + __titleTop); ;
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;

                float __headerWidth = ((__xWidth * 50) / 100);
                float __headerLeft = __pageLeft + ((__xWidth * 50) / 100) + 100;
                rect = new RectangleF(__cusLeft, __custTop, 120, lineHeight);
                g.DrawString("เลขที่ใบรับสินค้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 120, __custTop, __detailWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, 120, __custTop);
                g.DrawString("วันที่รับสินค้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 120, __custTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                rect = new RectangleF(__headerLeft + 10, __custTop2, 120, __custTop);
                g.DrawString("วันที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__headerLeft + 125, __custTop2, __headerWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).Length > 0)
                {
                    string __setrefdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setrefdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __custTop2 += _fontBold14.GetHeight(g);
                rect = new RectangleF(__headerLeft + 10, __custTop2, 120, lineHeight);
                g.DrawString("เลขที่เอกสารอ้างอิง :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__headerLeft + 125, __custTop2, __headerWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                if (_pageNum > 1)
                {
                    __custTop2 += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__headerLeft + 10, __custTop2, 120, lineHeight);
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                //----------------------------------------------------------------               
                //หัวตาราง                            
                float __ylineTop = __pageTop + 253;
                float __ylineEnd = __detailTop + __detailHeight;
                float __xline1 = __pageLeft + ((__xWidth * 6) / 100);
                float __xline2 = __xline1 + ((__xWidth * 22) / 100);
                float __xline3 = __xline2 + ((__xWidth * 28) / 100);
                float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 12) / 100);

                float __xwidth1 = ((__xWidth * 6) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("ลำดับ", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth2 = ((__pageWidth * 22) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth3 = ((__pageWidth * 28) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("ชื่อสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth4 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("หน่วยนับ", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth5 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth6 = ((__pageWidth * 12) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("ราคา:หน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth7 = ((__pageWidth * 12) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//                

                float __remarkTop = __pageHeight - 160;
                if (_printLine == true)
                {
                    rect = new RectangleF(__pageLeft, __remarkTop, __xWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ: ", _fontBold13, Brushes.Black, rect, sfLeft);
                    __remarkTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__pageLeft + 20, __remarkTop, __xWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal13, Brushes.Black, rect, sfLeft);

                    float __xbwidth = (__xWidth * 25) / 100;
                    float __xbline1 = __pageLeft + ((__xWidth * 25) / 100);
                    float __xbline2 = __xbline1 + ((__xWidth * 25) / 100);
                    float __xbline3 = __xbline2 + ((__xWidth * 25) / 100);
                    float __xbTop = __pageHeight - 80;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    __xbTop += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้จัดทำ", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้ตรวจรับสินค้า", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้อนุมัติตรวจรับ", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้จัดเก็บสินค้า", _fontNormal13, Brushes.Black, rect, sfCenter);
                    __xbTop += _fontBold13.GetHeight(g) + 10;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline1, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline2, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xbline3, __xbTop, __xbwidth, __custTop);
                    g.DrawString("วันที่_____/______/_______", _fontNormal13, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void __printDataReportPOCreditnote(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            ///SMLInventoryControl._gForm._formEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);


                RectangleF rect = new RectangleF(__pageWidth - 230, __y, 230, lineHeight);
                g.DrawString("ใบส่งคืน/ลดหนี้", _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 230, lineHeight);
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                }


                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 140;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 140;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);
                float __titleTop = __pageTop + 275;
                float __titleLeft = __pageLeft;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 10) + __titleTop); ;
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;

                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ลูกค้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยุ่ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);
                //----------------------------------------------------------------
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    //  string                 
                    g.DrawString(__setdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No.:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่ตามใบกำกับ:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).Length > 0)
                {
                    string __setTaxdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setTaxdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่ตามใบกำกับ:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                //หัวตาราง                            
                float __ylineTop = __pageTop + 253;
                float __ylineEnd = __detailTop + __detailHeight;
                float __xline1 = __pageLeft + ((__xWidth * 5) / 100);
                float __xline2 = __xline1 + ((__xWidth * 26) / 100);
                float __xline3 = __xline2 + ((__xWidth * 25) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 8) / 100);
                float __xline7 = __xline6 + ((__xWidth * 8) / 100);

                float __xwidth1 = ((__xWidth * 5) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("ลำดับ", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth2 = ((__pageWidth * 26) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รหัส", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth3 = ((__pageWidth * 25) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("ชื่อสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth4 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth5 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("หน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth6 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("ราคา", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth7 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfCenter);//                

                float __xwidth8 = ((__pageWidth * 11) / 100);
                float __sumdetailRightLeft = __titleLeft;
                float __sumdetailRightWidth = ((__pageWidth * 11) / 100);
                rect = new RectangleF(__xline7, __titleTop, __xwidth8, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//                                              

                float __buttomWidth = __xwidth1 + __xwidth2 + __xwidth3 + __xwidth4 + __xwidth5;


                float __buttomTopLeft = __sumdetailTop;
                float __buttomTopRight = __sumdetailTop;
                if (_printLine == true)
                {
                    float __remarkWidth = __xwidth1 + __xwidth2 + __xwidth3 + __xwidth4;
                    rect = new RectangleF(__pageLeft, __buttomTopLeft, __remarkWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ: ", _fontBold14, Brushes.Black, rect, sfLeft);
                    __buttomTopLeft += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__pageLeft + 20, __buttomTopLeft, __remarkWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal13, Brushes.Black, rect, sfLeft);
                    float __sumdetailwidth = __xwidth5 + __xwidth6 + __xwidth7;
                    rect = new RectangleF(__xline4, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("มูลค่าสินค้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline4, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ส่วนลด :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline4, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("จำนวนเงินหลังหักส่วนลด :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline4, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ภาษีมูลค่าเพิ่ม :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline4, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ยอดรวมสุทธิ :", _fontBold13, Brushes.Black, rect, sfLeft);

                    rect = new RectangleF(__xline7, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                    float __pageLineTop = __buttomTopRight - _fontBold13.GetHeight(g);
                    if (_printCount > 0)
                    {
                        rect = new RectangleF(__pageLeft, __pageLineTop, __remarkWidth, __custTop);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                    }
                    rect = new RectangleF(__pageLeft, __buttomTopRight, __remarkWidth, __custTop);
                    try
                    {
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfCenter); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    float __xbwidth = (__xWidth * 50) / 100;
                    float __xbTop = __pageHeight - 50;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__pageLeft + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    __xbTop += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้ออกเอกสาร", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__pageLeft + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้อนุมัติ", _fontNormal13, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ลูกหนี้_รับชำระหนี้
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportArDebtBilling(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ลูกหนี้_รับชำระหนี้.
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);


                RectangleF rect = new RectangleF(__pageWidth - 230, __y, 230, lineHeight);
                g.DrawString("ใบเสร็จรับเงิน", _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                    if (_printCount > 0)
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString() + "  พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        __y += _fontBold18.GetHeight(g);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                }
                __y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 20;
                float __custTop = __pageTop + 140;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 140;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);
                float __titleTop = __pageTop + 275;
                float __titleLeft = __pageLeft;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 10) + __titleTop); ;
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;

                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ลูกค้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยุ่ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);
                //----------------------------------------------------------------
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    //  string                 
                    g.DrawString(__setdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No.:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold13.GetHeight(g);
                //หัวตาราง                            
                float __titleTop2 = __titleTop + 20;
                float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                float __xline2 = __xline1 + ((__xWidth * 25) / 100);
                float __xline3 = __xline2 + ((__xWidth * 12) / 100);
                float __xline4 = __xline3 + ((__xWidth * 12) / 100);
                float __xline5 = __xline4 + ((__xWidth * 14) / 100);
                float __xline6 = __xline5 + ((__xWidth * 14) / 100);
                float __xwidth1 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("ลำดับที่", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth2 = ((__pageWidth * 25) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("เลขที่ใบกำกับ", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth3 = ((__pageWidth * 12) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("วันที่", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth4 = ((__pageWidth * 12) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("ครบกำหนด", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth5 = ((__pageWidth * 14) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth6 = ((__pageWidth * 14) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("ยอดคงค้าง", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth7 = ((__pageWidth * 15) / 100);
                float __sumdetailRightLeft = __titleLeft;
                float __sumdetailRightWidth = ((__pageWidth * 15) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("ยอดชำระ", _fontBold13, Brushes.Black, rect, sfCenter);//                                              
                float __buttomWidth = __xwidth1 + __xwidth2 + __xwidth3 + __xwidth4 + __xwidth5;
                rect = new RectangleF(__pageLeft, __sumdetailTop, __buttomWidth, __custTop);
                if (_printLine == true)
                {
                    try
                    {
                        //string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_pay_money].ToString()));
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_net_value].ToString()));
                        if (strangThai.Length > 0)
                            g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfCenter); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    rect = new RectangleF(__xline5, __sumdetailTop, __xwidth6, __custTop);
                    g.DrawString("จำนวนเงินทั้งสิ้น :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 5, __custTop);
                    //g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_pay_money].ToString()), _fontBold13, Brushes.Black, rect, sfRight);//รวมจำนวนเงิน
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_net_value].ToString()), _fontBold13, Brushes.Black, rect, sfRight);//รวมจำนวนเงิน
                    __sumdetailTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__pageLeft, __sumdetailTop, __buttomWidth, __custTop);
                    g.DrawString("การชำระเงินด้วยเช็คจะสมบูรณ์เมื่อบริษัทได้รับเงินตามเช็คเรียบร้อย", _fontBold14, Brushes.Black, rect, sfLeft);

                    float __xbLeft1 = __pageLeft + ((__xWidth * 25) / 100);
                    float __xbLeft2 = __xbLeft1 + ((__xWidth * 25) / 100);
                    float __xbLeft3 = __xbLeft2 + ((__xWidth * 25) / 100);
                    float __xbwidth = (__xWidth * 25) / 100;
                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("เงินสด .............................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("เช็คธนาคาร.....................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xbLeft1, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("เช็คเลขที่.........................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xbLeft2, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("ลงวันที่............................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xbLeft3, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("จำนวนเงิน.......................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("เช็คธนาคาร.....................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xbLeft1, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("เช็คเลขที่.........................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xbLeft2, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("ลงวันที่............................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xbLeft3, __sumdetailTop, __xbwidth, __custTop);
                    g.DrawString("จำนวนเงิน.......................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    float __xfLeft1 = __pageLeft + ((__xWidth * 50) / 100);
                    float __xfWidth = ((__xWidth * 50) / 100);
                    __sumdetailTop += _fontNormal14.GetHeight(g) + 30;
                    rect = new RectangleF(__pageLeft, __sumdetailTop, __xfWidth, __custTop);
                    g.DrawString("ผู้รับเงิน.....................................  ลงวันที่ .........../............/............", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__xfLeft1, __sumdetailTop, __xfWidth, __custTop);
                    g.DrawString("ผู้ออกเอกสาร.....................................  ลงวันที่ .........../............/............", _fontNormal13, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ลูกหนี้_ใบวางบิล
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportArPaybill(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ลูกหนี้_ใบวางบิล
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                //  float __companyWidth = ((__xWidth / 2) + 100);
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strArName = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strArName.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);


                RectangleF rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                g.DrawString("ใบวางบิล", _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                    if (_printCount > 0)
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString() + "  พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        __y += _fontBold18.GetHeight(g);
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 210, lineHeight);
                        g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold14, Brushes.Black, rect, sfCenter);
                    }
                }
                //__y += _fontBold18.GetHeight(g) - 10;

                //__y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 20;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 55) + 10;
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 55) / 2);

                float __titleLeft = __pageLeft;
                float __titleWidth = (__xWidth / 2) + 100;

                //float __
                rect = new RectangleF(__cusLeft, __custTop, 100, lineHeight);
                g.DrawString("ชื่อลูกค้า : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่ : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, 100, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 100, __custTop);
                g.DrawString("Fax :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                __detailTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("พนักงานขาย/Sale: ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._sale_code].ToString() + ":" + __dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._sale_name].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                float __titleTop = __pageTop + 255;
                float __titleTop2 = __pageTop + 280;
                float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                float __xline4 = __xline3 + ((__xWidth * 12) / 100);
                float __xline5 = __xline4 + ((__xWidth * 12) / 100);
                float __xline6 = __xline5 + ((__xWidth * 14) / 100);

                //หัวตาราง                             
                float __xlineWidth1 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xlineWidth1, __custTop);
                g.DrawString("ลำดับ", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__pageLeft, __titleTop2, __xlineWidth1, __custTop);
                g.DrawString("No", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth2 = ((__xWidth * 20) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xlineWidth2, __custTop);
                g.DrawString("เลขที่เอกสาร", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline1, __titleTop2, __xlineWidth2, __custTop);
                g.DrawString("Document No", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth3 = ((__xWidth * 20) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xlineWidth3, __custTop);
                g.DrawString("เลขที่ใบกำกับภาษี", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline2, __titleTop2, __xlineWidth3, __custTop);
                g.DrawString("Invoice No", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth4 = ((__xWidth * 12) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xlineWidth4, __custTop);
                g.DrawString("วันที่ใบกำกับ", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline3, __titleTop2, __xlineWidth4, __custTop);
                g.DrawString("Invoice Date", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth5 = ((__xWidth * 12) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xlineWidth5, __custTop);
                g.DrawString("วันที่ครบ", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline4, __titleTop + 15, __xlineWidth5, __custTop);
                g.DrawString("กำหนด", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline4, __titleTop2, __xlineWidth5, __custTop);
                g.DrawString("Due Date", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth6 = ((__xWidth * 14) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xlineWidth6, __custTop);
                g.DrawString("มูลค่าคงเหลือ", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline5, __titleTop2, __xlineWidth6, __custTop);
                g.DrawString("Ref Amount", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth7 = ((__xWidth * 14) / 100);
                float __sumdetailRightLeft = __xline6 - 10;
                float __sumdetailRightWidth = ((__pageWidth * 14) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xlineWidth7, __custTop);
                g.DrawString("ยอดวางบิล", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline6, __titleTop2, __xlineWidth7, __custTop);
                g.DrawString("Ref Amount", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 5) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 1) + __titleTop); ;
                float __sumdetailLeft = __detailButtomWidth + 10;
                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;
                float __buttomDetailWidth = 0;
                //หมายเหตุ             
                if (_printLine == true)
                {
                    try
                    {
                        rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                        g.DrawString("หากมีข้อสงสัย กรุณาติดต่อสอบถามได้ที่ฝ่ายการเงิน", _fontNormal14, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    __detailButtomTop += _fontNormal12.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ:" + __remarkResult, _fontBold14, Brushes.Black, rect, sfLeft);

                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("รวมจำนวนเงิน", _fontBold14, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xlineWidth7, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_net_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                    __buttomDetailWidth = __pageLeft + __pageWidth / 2;
                    float __rightbuttomTop = __buttomDetailTop;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__quotationApprovalStr, _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้รับวางบิล", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeft = __buttomDetailLeft + (__pageWidth / 2);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__quotationSaleCodeStr, _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้วางบิล", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ขาย_คืนเงินล่วงหน้า
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportSODepositPaymentReturn(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ขาย_คืนเงินล่วงหน้า
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strArName = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strArName.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__xWidth - 200, __y, 250, lineHeight);
                g.DrawString("ใบคืนเงินล่วงหน้า", _fontBold20, Brushes.Black, rect, sfCenter);


                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 60);
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __titleLeft = __pageLeft;
                ///

                float __titleTop = __pageTop + 280;

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;

                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 20;
                float __buttomDetailLeft = __pageLeft;
                float __buttomRightTop = (__detailButtomTop + __detailButtomHeight) + 20;
                ////
                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ชื่อลูกค้า", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                //
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("อ้างอิงใบรับเงินล่วงหน้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("พนักงานขาย/Sale:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 100, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_code].ToString() + "  " + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                float __detailTop2 = __pageTop + 270;
                float __detailHeight2 = __pageHeight - (__detailTop + 200);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หมายเหตุเพิ่มเติม", _fontBold13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("..", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หากไม่มารับของตามกำหนดระยะเวลา", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ทางบริษัทจะทำการยึดเงินส่วนนี้", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ถือเป็นค่าเสียหาย ในการดำเนินงาน", _fontNormal13, Brushes.Black, rect, sfLeft);

                float __detailTop3 = __pageTop + 265;
                float __detailHeight3 = __pageHeight - (__detailTop3 + 200);

                //กรอบล่างซ้าย                
                float __buttomBox1Top = ((__detailHeight3 + 10) + __detailTop3);

                float __sumdetailTop = ((__detailHeight3 + 10) + __detailTop3);
                float __sumdetailLeft = __detailButtomWidth + 10;

                float __buttomBox2Top2 = __pageHeight - (__detailTop + 200);
                float __buttomBoxWidth2 = (__xWidth / 2) + 130;
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                g.DrawString("จ่ายคืนเงินเป็นจำนวน", _fontNormal13, Brushes.Black, rect, sfLeft);
                __buttomBox1Top += _fontBold12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                try
                {
                    string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._money].ToString()));
                    g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //กรอบล่างขวา
                float __buttomBox2Left = __pageLeft + __buttomBoxWidth2;
                float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString("รวมเงินทั้งสิ้น :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._money].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                //กรอบล่างสุด:ซ้าย               
                rect = new RectangleF(__cusLeft, __buttomDetailTop - 10, __buttomBoxWidth2, __custTop);
                g.DrawString("ชำระโดย", _fontBold13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop1 = __buttomDetailTop + 25;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop1 - 3, __buttomBoxWidth2, __custTop);
                g.DrawString("เงินสด", _fontNormal13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop2 = __buttomDetailTop + 45;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เช็ค                      ธนาคาร ........................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                __checkBoxTop2 += _fontBold14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เลขที่เช็ค.......................................    ลงวันที่ _____/________/_________", _fontNormal13, Brushes.Black, rect, sfLeft);

                //__buttomRightTop
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้จ่ายเเงิน.................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้รับเงิน....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ลงวันที่ _____/________/_________", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้อนุมัติ....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ขาย_รับเงินล่วงหน้า
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportSODepositPayment(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ขาย_รับเงินล่วงหน้า
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strArName = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strArName.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__xWidth - 200, __y, 250, lineHeight);
                g.DrawString("ใบรับเงินล่วงหน้า", _fontBold20, Brushes.Black, rect, sfCenter);


                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 60);
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __titleLeft = __pageLeft;
                ///

                float __titleTop = __pageTop + 280;

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;

                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 20;
                float __buttomDetailLeft = __pageLeft;
                float __buttomRightTop = (__detailButtomTop + __detailButtomHeight) + 20;
                ////
                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ชื่อลูกค้า", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                //
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("พนักงานขาย/Sale:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 100, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_code].ToString() + "  " + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                //
                float __detailTop2 = __pageTop + 270;
                float __detailHeight2 = __pageHeight - (__detailTop + 200);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หมายเหตุเพิ่มเติม", _fontBold13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("..", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หากไม่มารับของตามกำหนดระยะเวลา", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ทางบริษัทจะทำการยึดเงินส่วนนี้", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ถือเป็นค่าเสียหาย ในการดำเนินงาน", _fontNormal13, Brushes.Black, rect, sfLeft);

                float __detailTop3 = __pageTop + 265;
                float __detailHeight3 = __pageHeight - (__detailTop3 + 200);

                //กรอบล่างซ้าย                
                float __buttomBox1Top = ((__detailHeight3 + 10) + __detailTop3);

                float __sumdetailTop = ((__detailHeight3 + 10) + __detailTop3);
                float __sumdetailLeft = __detailButtomWidth + 10;

                float __buttomBox2Top2 = __pageHeight - (__detailTop + 200);
                float __buttomBoxWidth2 = (__xWidth / 2) + 130;
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                g.DrawString("ได้รับชำระเป็นจำนวนเงิน", _fontNormal13, Brushes.Black, rect, sfLeft);
                __buttomBox1Top += _fontBold12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                try
                {
                    string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                    g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //กรอบล่างขวา
                float __buttomBox2Left = __pageLeft + __buttomBoxWidth2;
                float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString("รวมเงินทั้งสิ้น :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                //กรอบล่างสุด:ซ้าย               
                rect = new RectangleF(__cusLeft, __buttomDetailTop - 10, __buttomBoxWidth2, __custTop);
                g.DrawString("ชำระโดย", _fontBold13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop1 = __buttomDetailTop + 25;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop1 - 3, __buttomBoxWidth2, __custTop);
                g.DrawString("เงินสด", _fontNormal13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop2 = __buttomDetailTop + 45;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เช็ค                      ธนาคาร ........................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                __checkBoxTop2 += _fontBold14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เลขที่เช็ค.......................................    ลงวันที่ _____/________/_________", _fontNormal13, Brushes.Black, rect, sfLeft);

                //__buttomRightTop
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้จ่ายเเงิน...................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้รับเงิน....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ลงวันที่ _____/________/_________", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้อนุมัติ....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportDepositPaymentReturn(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strArName = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strArName.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__xWidth - 200, __y, 250, lineHeight);
                g.DrawString("ใบคืนเงินล่วงหน้า", _fontBold20, Brushes.Black, rect, sfCenter);


                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 60);
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __titleLeft = __pageLeft;
                ///

                float __titleTop = __pageTop + 280;

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;

                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 20;
                float __buttomDetailLeft = __pageLeft;
                float __buttomRightTop = (__detailButtomTop + __detailButtomHeight) + 20;
                ////
                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ชื่อเจ้าหนี้", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("อ้างอิงใบจ่ายเงินล่วงหน้าเลขที่:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_ref].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                float __detailTop2 = __pageTop + 270;
                float __detailHeight2 = __pageHeight - (__detailTop + 200);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หมายเหตุเพิ่มเติม", _fontBold13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("..", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หากไม่มารับของตามกำหนดระยะเวลา", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ทางบริษัทจะทำการยึดเงินส่วนนี้", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ถือเป็นค่าเสียหาย ในการดำเนินงาน", _fontNormal13, Brushes.Black, rect, sfLeft);

                float __detailTop3 = __pageTop + 265;
                float __detailHeight3 = __pageHeight - (__detailTop3 + 200);

                //กรอบล่างซ้าย                
                float __buttomBox1Top = ((__detailHeight3 + 10) + __detailTop3);

                float __sumdetailTop = ((__detailHeight3 + 10) + __detailTop3);
                float __sumdetailLeft = __detailButtomWidth + 10;

                float __buttomBox2Top2 = __pageHeight - (__detailTop + 200);
                float __buttomBoxWidth2 = (__xWidth / 2) + 130;
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                g.DrawString("จ่ายชำระเป็นจำนวนเงิน", _fontNormal13, Brushes.Black, rect, sfLeft);
                __buttomBox1Top += _fontBold12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                try
                {
                    string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._money].ToString()));
                    g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //กรอบล่างขวา
                float __buttomBox2Left = __pageLeft + __buttomBoxWidth2;
                float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString("รวมเงินทั้งสิ้น :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._money].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                //กรอบล่างสุด:ซ้าย               
                rect = new RectangleF(__cusLeft, __buttomDetailTop - 10, __buttomBoxWidth2, __custTop);
                g.DrawString("ชำระโดย", _fontBold13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop1 = __buttomDetailTop + 25;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop1 - 3, __buttomBoxWidth2, __custTop);
                g.DrawString("เงินสด", _fontNormal13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop2 = __buttomDetailTop + 45;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เช็ค                      ธนาคาร ........................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                __checkBoxTop2 += _fontBold14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เลขที่เช็ค.......................................    ลงวันที่ _____/________/_________", _fontNormal13, Brushes.Black, rect, sfLeft);

                //__buttomRightTop
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้จ่ายเเงิน...................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้รับเงิน....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ลงวันที่ _____/________/_________", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้อนุมัติ....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportDepositPayment(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strArName = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strArName.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__xWidth - 200, __y, 250, lineHeight);
                g.DrawString("ใบจ่ายเงินล่วงหน้า", _fontBold20, Brushes.Black, rect, sfCenter);


                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 60);
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __titleLeft = __pageLeft;
                ///

                float __titleTop = __pageTop + 280;

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;

                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 20;
                float __buttomDetailLeft = __pageLeft;
                float __buttomRightTop = (__detailButtomTop + __detailButtomHeight) + 20;
                ////
                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ชื่อเจ้าหนี้", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/DATE:", _fontBold13, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                    g.DrawString(__setDate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/NO:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันครบกำหนด/DUEDATE:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).Length > 0)
                {
                    string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDueDate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เครดิต/CREDIT DAY:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString() + "   วัน", _fontNormal13, Brushes.Black, rect, sfLeft);

                float __detailTop2 = __pageTop + 270;
                float __detailHeight2 = __pageHeight - (__detailTop + 200);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หมายเหตุเพิ่มเติม", _fontBold13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("..", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("หากไม่มารับของตามกำหนดระยะเวลา", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ทางบริษัทจะทำการยึดเงินส่วนนี้", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop2 += _fontBold13.GetHeight(g);
                rect = new RectangleF(__cusLeft, __detailTop2, __detailWidth, __custTop);
                g.DrawString("ถือเป็นค่าเสียหาย ในการดำเนินงาน", _fontNormal13, Brushes.Black, rect, sfLeft);

                float __detailTop3 = __pageTop + 265;
                float __detailHeight3 = __pageHeight - (__detailTop3 + 200);

                //กรอบล่างซ้าย                
                float __buttomBox1Top = ((__detailHeight3 + 10) + __detailTop3);

                float __sumdetailTop = ((__detailHeight3 + 10) + __detailTop3);
                float __sumdetailLeft = __detailButtomWidth + 10;

                float __buttomBox2Top2 = __pageHeight - (__detailTop + 200);
                float __buttomBoxWidth2 = (__xWidth / 2) + 130;
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                g.DrawString("จ่ายชำระเป็นจำนวนเงิน", _fontNormal13, Brushes.Black, rect, sfLeft);
                __buttomBox1Top += _fontBold12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __buttomBox1Top, __detailWidth, __custTop);
                try
                {
                    string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                    g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //กรอบล่างขวา
                float __buttomBox2Left = __pageLeft + __buttomBoxWidth2;
                float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString("รวมเงินทั้งสิ้น :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__buttomBox2Left, __sumdetailTop, __buttomBox2Width, __custTop);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                //กรอบล่างสุด:ซ้าย               
                rect = new RectangleF(__cusLeft, __buttomDetailTop - 10, __buttomBoxWidth2, __custTop);
                g.DrawString("ชำระโดย", _fontBold13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop1 = __buttomDetailTop + 25;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop1 - 3, __buttomBoxWidth2, __custTop);
                g.DrawString("เงินสด", _fontNormal13, Brushes.Black, rect, sfLeft);
                float __checkBoxTop2 = __buttomDetailTop + 45;
                rect = new RectangleF(__cusLeft + 20, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เช็ค                      ธนาคาร ........................................", _fontNormal13, Brushes.Black, rect, sfLeft);
                __checkBoxTop2 += _fontBold14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __checkBoxTop2, __buttomBoxWidth2, __custTop);
                g.DrawString("เลขที่เช็ค.......................................    ลงวันที่ _____/________/_________", _fontNormal13, Brushes.Black, rect, sfLeft);

                //__buttomRightTop
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้จ่ายเเงิน...................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้รับเงิน....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ลงวันที่ _____/________/_________", _fontBold13, Brushes.Black, rect, sfLeft);
                __buttomRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__buttomBox2Left + 10, __buttomRightTop, __buttomBox2Width, __custTop);
                g.DrawString("ผู้อนุมัติ....................................................", _fontBold13, Brushes.Black, rect, sfLeft);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportPurchaseAdd(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rect = new RectangleF(__pageLeft, __pageTop, __xWidth, __fontHeight14);
                g.DrawString("ใบเพิ่มหนี้/เพิ่มสินค้า", _fontBold20, Brushes.Black, rect, sfCenter);
                __pageTop += _fontBold18.GetHeight(g);
                rect = new RectangleF(__pageLeft, __pageTop, __xWidth, __fontHeight14);
                g.DrawString("Credit Note", _fontBold20, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__pageLeft, __y + 110, 200, __fontHeight14);
                g.DrawString("เลขที่ใบเพิ่มหนี้/เพิ่มสินค้า", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 150, __y + 110, 200, __fontHeight14);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                float __docDateLeft = ((__xWidth * 50) / 100) + 50;
                rect = new RectangleF(__docDateLeft, __y + 110, 200, __fontHeight14);
                g.DrawString("วันที่ออกเอกสาร", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__docDateLeft + 100, __y + 110, 200, __fontHeight14);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDueDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                rect = new RectangleF(__pageLeft, __y + 150, 200, __fontHeight14);
                g.DrawString("รหัสเจ้าหนี้", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 70, __y + 150, 200, __fontHeight14);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._cust_code].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                float __apNameLeft = ((__xWidth * 30) / 100);
                rect = new RectangleF(__apNameLeft, __y + 150, 200, __fontHeight14);
                g.DrawString("ชื่อ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__apNameLeft + 25, __y + 150, 400, __fontHeight14);
                g.DrawString(__dsTop.Tables[0].Rows[0]["custName"].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);


                rect = new RectangleF(__pageLeft, __y + 190, 200, __fontHeight14);
                g.DrawString("เลขที่ใบกำกับภาษี", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 120, __y + 190, 200, __fontHeight14);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                __apNameLeft = ((__xWidth * 50) / 100);
                rect = new RectangleF(__apNameLeft, __y + 190, 200, __fontHeight14);
                g.DrawString("อ้างถึงเลขที่ใบตั้งหนี้/รับสินค้า", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__apNameLeft + 180, __y + 190, 200, __fontHeight14);
                g.DrawString(__refDocno, _fontNormal14, Brushes.Black, rect, sfLeft);

                __y = __y + 190 + _fontBold14.GetHeight(g);
                rect = new RectangleF(__pageLeft, __y, 200, __fontHeight14);
                g.DrawString("วันที่ใบกำกับภาษี", _fontBold14, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).Length > 0)
                {
                    string __setTaxDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__pageLeft + 120, __y, 200, __fontHeight14);
                    g.DrawString(__setTaxDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                //page 
                if (_pageNum > 1)
                {
                    // __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__apNameLeft, __y, 200, __fontHeight14);
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                float __titleTop = __pageTop + 230;
                float __titleTop2 = (__pageTop + 220) + _fontBold14.GetHeight(g);
                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 25) / 100);
                float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 6) / 100);
                float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                float __xline7 = __xline6 + ((__xWidth * 8) / 100);
                float __xline8 = __xline7 + ((__xWidth * 6) / 100);

                //หัวตาราง                             
                float __xlineWidth1 = ((__xWidth * 22) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xlineWidth1, __fontHeight14);
                g.DrawString("รหัสสินค้า", _fontNormal12, Brushes.Black, rect, sfCenter);//               
                float __xlineWidth2 = ((__xWidth * 25) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xlineWidth2, __fontHeight14);
                g.DrawString("รายการสินค้า", _fontNormal12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth3 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xlineWidth3, __fontHeight14);
                g.DrawString("คลัง", _fontNormal12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth4 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xlineWidth4, __fontHeight14);
                g.DrawString("พื้นที่เก็บ", _fontNormal12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth5 = ((__xWidth * 6) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xlineWidth5, __fontHeight14);
                g.DrawString("จำนวน", _fontNormal12, Brushes.Black, rect, sfCenter);//               
                float __xlineWidth6 = ((__xWidth * 9) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xlineWidth6, __fontHeight14);
                g.DrawString("หน่วยนับ", _fontNormal12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth7 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xlineWidth7, __fontHeight14);
                g.DrawString("ราคาซื้อ", _fontNormal12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth8 = ((__xWidth * 6) / 100);
                rect = new RectangleF(__xline7, __titleTop, __xlineWidth8, __fontHeight14);
                g.DrawString("ส่วนลด", _fontNormal12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth9 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline8, __titleTop, __xlineWidth9, __fontHeight14);
                g.DrawString("จำนวนเงิน", _fontNormal12, Brushes.Black, rect, sfCenter);//                
                float __detailTop = __pageTop + 253;
                float __detailHeight = __pageHeight - (__detailTop + 230);
                float __buttomTop = __detailTop + __detailHeight;
                float __buttomRightLeft = __pageLeft + (__xWidth / 2) + 130;
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                //  หมายเหตุ        
                if (_printLine == true)
                {
                    float __xwidthRemark = __xlineWidth1 + __xlineWidth2 + __xlineWidth3 + __xlineWidth4 + __xlineWidth5;
                    rect = new RectangleF(__pageLeft, __buttomTop, 200, __fontHeight14);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("สาเหตุการเพิ่มหนี้/เพิ่มสินค้า : ", _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__pageLeft + 20, __buttomTop + 20, __xwidthRemark, __fontHeight14);
                    g.DrawString(__remarkResult, _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__buttomRightLeft, __buttomTop, 200, __fontHeight14);
                    g.DrawString("มูลค่าใบกำกับภาษีเดิม", _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTop, __xlineWidth9, __fontHeight14);
                    //g.DrawString(__checkNumstr(__sumtaxOldAmount.ToString()), _fontNormal12, Brushes.Black, rect, sfRight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._ref_amount].ToString()), _fontNormal12, Brushes.Black, rect, sfRight);

                    __buttomTop += _fontBold12.GetHeight(g);
                    double __getSumTotal = __sumtaxOldAmount + Double.Parse(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString());
                    rect = new RectangleF(__buttomRightLeft, __buttomTop, 200, __fontHeight14);
                    g.DrawString("มูลค่าที่ถูกต้อง", _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTop, __xlineWidth9, __fontHeight14);
                    //g.DrawString(__checkNumstr(__getSumTotal.ToString()), _fontNormal12, Brushes.Black, rect, sfRight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._ref_new_amount].ToString()), _fontNormal12, Brushes.Black, rect, sfRight);

                    __buttomTop += _fontBold12.GetHeight(g);
                    rect = new RectangleF(__buttomRightLeft, __buttomTop, 200, __fontHeight14);
                    g.DrawString("ผลต่าง", _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTop, __xlineWidth9, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._ref_diff].ToString()), _fontNormal12, Brushes.Black, rect, sfRight);

                    __buttomTop += _fontBold12.GetHeight(g);
                    rect = new RectangleF(__buttomRightLeft, __buttomTop, 200, __fontHeight14);
                    g.DrawString("ภาษีมูลค่าเพิ่ม " + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_rate].ToString() + " %", _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTop, __xlineWidth9, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal12, Brushes.Black, rect, sfRight);

                    __buttomTop += _fontBold12.GetHeight(g);
                    rect = new RectangleF(__buttomRightLeft, __buttomTop, 200, __fontHeight14);
                    g.DrawString("จำนวนเงินรวมทั้งสิ้น", _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTop, __xlineWidth9, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal12, Brushes.Black, rect, sfRight);

                    if (_printCount > 0)
                    {
                        rect = new RectangleF(__pageLeft, __buttomTop, 200, __fontHeight14);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal12, Brushes.Black, rect, sfLeft);
                    }

                    float __buttomDetailTop = __buttomTop + 60;
                    float __buttomDetailLeft = __pageLeft;
                    float __buttomDetailWidth = __pageLeft + __pageWidth / 2;
                    float __rightbuttomTop = __buttomTop + 60;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __fontHeight14);
                    g.DrawString("ลงชื่อ...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __fontHeight14);
                    g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __fontHeight14);
                    g.DrawString("ผู้ออกเอกสาร", _fontBold14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeft = (__pageWidth / 2);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __fontHeight14);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __fontHeight14);
                    g.DrawString("ลงชื่อ..............................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __fontHeight14);
                    g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __fontHeight14);
                    g.DrawString("ผู้อนุมัติ", _fontBold14, Brushes.Black, rect, sfCenter);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ซื้อ_ใบสั่งซื้อ
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportPurchase(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ซื้อ_ใบสั่งซื้อ
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeight14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeight14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeight14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeight14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                g.DrawString(__strHeader1, _fontBold20, Brushes.Black, rect, sfCenter);
                __y += _fontBold18.GetHeight(g) - 10;
                rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                g.DrawString(__strHeader2, _fontBold18, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    __y += _fontBold18.GetHeight(g);
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                __y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 60);
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);

                float __titleLeft = __pageLeft;

                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ชื่อเจ้าหนี้", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold14, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                    g.DrawString(__setDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันครบกำหนด/DUEDATE:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).Length > 0)
                {
                    string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDueDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold16.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เครดิต/CREDIT DAY:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 160, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString() + "  วัน", _fontNormal14, Brushes.Black, rect, sfLeft);

                float __titleTop = __pageTop + 280;
                float __titleTop2 = __pageTop + 300;
                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 28) / 100);
                float __xline3 = __xline2 + ((__xWidth * 11) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 10) / 100);

                //หัวตาราง                             
                float __xlineWidth1 = ((__xWidth * 22) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xlineWidth1, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__pageLeft, __titleTop2, __xlineWidth1, __custTop);
                g.DrawString("Code", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xlineWidth2 = ((__xWidth * 28) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xlineWidth2, __custTop);
                g.DrawString("รายการสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline1, __titleTop2, __xlineWidth2, __custTop);
                g.DrawString("Name", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xlineWidth3 = ((__xWidth * 11) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xlineWidth3, __custTop);
                g.DrawString("หน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline2, __titleTop2, __xlineWidth3, __custTop);
                g.DrawString("Unit", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xlineWidth4 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xlineWidth4, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline3, __titleTop2, __xlineWidth4, __custTop);
                g.DrawString("QTY", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xlineWidth5 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xlineWidth5, __custTop);
                g.DrawString("ราคาซื้อ", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline4, __titleTop2, __xlineWidth5, __custTop);
                g.DrawString("Price", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xlineWidth6 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xlineWidth6, __custTop);
                g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline5, __titleTop2, __xlineWidth6, __custTop);
                g.DrawString("Discount", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xlineWidth7 = ((__xWidth * 11) / 100);
                float __sumdetailRightLeft = __xline6 - 5;
                float __sumdetailRightWidth = ((__pageWidth * 11) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xlineWidth7, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline6, __titleTop2, __xlineWidth7, __custTop);
                g.DrawString("Amount", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 1) + __titleTop); ;
                float __sumdetailLeft = __detailButtomWidth + 10;
                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;
                float __buttomDetailWidth = 0;
                //หมายเหตุ            
                if (_printLine == true)
                {
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth - 50, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ:" + __remarkResult, _fontBold14, Brushes.Black, rect, sfLeft);

                    __detailButtomTop += _fontNormal12.GetHeight(g) + 50;
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    g.DrawString("ยอดเงินเป็นตัวหนังสือ", _fontBold14, Brushes.Black, rect, sfLeft);
                    float __moneyStringTop = __detailButtomTop + _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __moneyStringTop, __detailButtomWidth, __custTop);
                    try
                    {
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        g.DrawString("( " + strangThai + " )", _fontNormal14, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("รวมจำนวนเงิน", _fontBold14, Brushes.Black, rect, sfLeft);
                    //__sumdetailRightLeft
                    //__sumdetailRightWidth
                    rect = new RectangleF(__sumdetailRightLeft, __sumdetailTop, __sumdetailRightWidth, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//รวมจำนวนเงิน

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ส่วนลด", _fontBold14, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__sumdetailRightLeft, __sumdetailTop, __sumdetailRightWidth, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//ส่วนลดการค่า

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ราคาสินค้า/บริการ", _fontBold14, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__sumdetailRightLeft, __sumdetailTop, __sumdetailRightWidth, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//ราคาสินค้า/บริการ

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ภาษีมูลค่าเพิ่ม  " + __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_rate].ToString()) + " %", _fontBold14, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__sumdetailRightLeft, __sumdetailTop, __sumdetailRightWidth, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal14, Brushes.Black, rect, sfRight); //ภาษีมูลค่าเพิ่ม

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("จำนวนเงินรวมทั้งสิ้น", _fontBold14, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__sumdetailRightLeft, __sumdetailTop, __sumdetailRightWidth, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontBold14, Brushes.Black, rect, sfRight);//จำนวนเงินรวมทั้งสิ้น


                    __buttomDetailWidth = __pageLeft + __pageWidth / 2;
                    float __rightbuttomTop = __buttomDetailTop;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__quotationApprovalStr, _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้สั่งซื้อ...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__docDateStr + " _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeft = __buttomDetailLeft + (__pageWidth / 2);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__quotationSaleCodeStr, _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้อนุมัติ..............................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__docDateStr + " _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ซื้อ_ซื้อสินค้าและค่าบริการ
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportPurchaseService(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าและค่าบริการ
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__pageWidth - 230, __y, 250, lineHeight);
                g.DrawString(__strHeader1, _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 250, lineHeight);
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                }

                float __headRight = __pageLeft;
                float __headRightTop = __pageTop + 130;
                float __headLeftY = __pageTop + 130;
                float __detailWidth = ((__pageWidth * 70) / 100);
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 70);
                rect = new RectangleF(__pageLeft, __headLeftY, 150, lineHeight);
                g.DrawString("รหัส", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 50, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(":", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 60, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(__customerCode, _fontNormal12, Brushes.Black, rect, sfLeft);
                __headLeftY += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft, __headLeftY, 150, lineHeight);
                g.DrawString("ชื่อ", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 50, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(":", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 60, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal12, Brushes.Black, rect, sfLeft);
                __headLeftY += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft, __headLeftY, 150, lineHeight);
                g.DrawString("ที่อยู่", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 50, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(":", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 60, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(__customerAddress, _fontNormal12, Brushes.Black, rect, sfLeft);
                __headLeftY += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft, __headLeftY, 150, lineHeight);
                g.DrawString("โทรศัพท์", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 50, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(":", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 60, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(__customerTel, _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 200, __headLeftY, __detailWidth, lineHeight);
                g.DrawString("โทรสาร", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 250, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(__customerFax, _fontNormal12, Brushes.Black, rect, sfLeft);
                __headLeftY += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft, __headLeftY, 150, lineHeight);
                g.DrawString("Email Address :", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 80, __headLeftY, __detailWidth, lineHeight);
                g.DrawString(__customerEmail, _fontNormal12, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, lineHeight);
                g.DrawString("เลขที่ :", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal12, Brushes.Black, rect, sfLeft);
                __headRightTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, lineHeight);
                g.DrawString("วันที่ :", _fontNormal12, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDocDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, lineHeight);
                    g.DrawString(__setDocDate, _fontNormal12, Brushes.Black, rect, sfLeft);
                }
                __headRightTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, lineHeight);
                g.DrawString("เลขที่ใบกำกับภาษี :", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_no].ToString(), _fontNormal12, Brushes.Black, rect, sfLeft);
                __headRightTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, lineHeight);
                g.DrawString("วันที่ใบกำกับภาษี :", _fontNormal12, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).Length > 0)
                {
                    string __setDocDateTax = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, lineHeight);
                    g.DrawString(__setDocDateTax, _fontNormal12, Brushes.Black, rect, sfLeft);
                }
                __headRightTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, lineHeight);
                g.DrawString("วันครบกำหนด :", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, lineHeight);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).Length > 0)
                {
                    string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDueDate, _fontNormal12, Brushes.Black, rect, sfLeft);
                }
                __headRightTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, lineHeight);
                g.DrawString("เครดิต : ", _fontNormal12, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, lineHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString() + "  วัน", _fontNormal12, Brushes.Black, rect, sfLeft);


                float __titleTop2 = __pageTop + 280;
                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 33) / 100);
                float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 8) / 100);

                float __xlineWidth1 = ((__xWidth * 22) / 100);
                rect = new RectangleF(__pageLeft, __titleTop2, __xlineWidth1, lineHeight);
                g.DrawString("รหัสสินค้า", _fontBold12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth2 = ((__xWidth * 33) / 100);
                rect = new RectangleF(__xline1, __titleTop2, __xlineWidth2, lineHeight);
                g.DrawString("รายการสินค้า", _fontBold12, Brushes.Black, rect, sfCenter);//                                     
                float __xlineWidth3 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline2, __titleTop2, __xlineWidth3, lineHeight);
                g.DrawString("หน่วย", _fontBold12, Brushes.Black, rect, sfCenter);//
                float __xlineWidth4 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline3, __titleTop2, __xlineWidth4, lineHeight);
                g.DrawString("จำนวน", _fontBold12, Brushes.Black, rect, sfCenter);//                
                float __xlineWidth5 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline4, __titleTop2, __xlineWidth5, lineHeight);
                g.DrawString("ราคาขาย", _fontBold12, Brushes.Black, rect, sfCenter);//        
                float __xlineWidth6 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline5, __titleTop2, __xlineWidth6, lineHeight);
                g.DrawString("ส่วนลด", _fontBold12, Brushes.Black, rect, sfCenter);//        
                float __xlineWidth7 = ((__xWidth * 11) / 100);
                rect = new RectangleF(__xline6, __titleTop2, __xlineWidth7, lineHeight);
                g.DrawString("จำนวนเงิน", _fontBold12, Brushes.Black, rect, sfCenter);//        

                float __detailTop = __pageTop + 320;
                float __detailHeight = __pageHeight - (__detailTop) - 320;
                float __buttomRightTop = (__detailTop + __detailHeight) + 15;
                float __buttomSumLeft = __xline4;
                float __buttomSumWidth = __xlineWidth5;

                float __footerLeft = __pageLeft;
                if (_printLine == true)
                {
                    rect = new RectangleF(__footerLeft, __buttomRightTop, __xWidth, lineHeight);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ : ", _fontBold12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__footerLeft, __buttomRightTop + 20, __xWidth, lineHeight);
                    g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString(), _fontNormal12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline3 + 10, __buttomRightTop, __xlineWidth4 + __xlineWidth5, lineHeight);
                    g.DrawString("รวมจำนวนเงิน", _fontBold12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __buttomRightTop, __xlineWidth6 + __xlineWidth7, lineHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontBold12, Brushes.Black, rect, sfRight);

                    __buttomRightTop += _fontBold16.GetHeight(g);
                    rect = new RectangleF(__xline3 + 10, __buttomRightTop, __xlineWidth4 + __xlineWidth5, lineHeight);
                    g.DrawString("ส่วนลด", _fontBold12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __buttomRightTop, __xlineWidth6 + __xlineWidth7, lineHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontBold12, Brushes.Black, rect, sfRight);

                    __buttomRightTop += _fontBold16.GetHeight(g);
                    rect = new RectangleF(__xline3 + 10, __buttomRightTop, __xlineWidth4 + __xlineWidth5, lineHeight);
                    g.DrawString("ราคาสินค้า/บริการ", _fontBold12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __buttomRightTop, __xlineWidth6 + __xlineWidth7, lineHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontBold12, Brushes.Black, rect, sfRight);

                    __buttomRightTop += _fontBold16.GetHeight(g);
                    rect = new RectangleF(__xline3 + 10, __buttomRightTop, __xlineWidth4 + __xlineWidth5, lineHeight);
                    g.DrawString("ภาษีมูลค่าเพิ่ม      " + __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_rate].ToString()) + "%", _fontBold12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __buttomRightTop, __xlineWidth6 + __xlineWidth7, lineHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontBold12, Brushes.Black, rect, sfRight);
                    __buttomRightTop += _fontBold16.GetHeight(g);
                    try
                    {
                        if (_printCount > 0)
                        {
                            rect = new RectangleF(__footerLeft, __buttomRightTop - 30, 250, lineHeight);
                            g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal12, Brushes.Black, rect, sfLeft);
                        }
                        rect = new RectangleF(__footerLeft, __buttomRightTop, __xlineWidth1 + __xlineWidth2 + __xlineWidth3, lineHeight);
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString());  //ตัวเลขที่เป็นตัวหนังสือ
                        g.DrawString("( " + strangThai + " )", _fontBold12, Brushes.Black, rect, sfCenter); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    rect = new RectangleF(__xline3 + 10, __buttomRightTop, __xlineWidth4 + __xlineWidth5, lineHeight);
                    g.DrawString("จำนวนเงินรวมทั้งสิ้น", _fontBold12, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __buttomRightTop, __xlineWidth6 + __xlineWidth7, lineHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontBold12, Brushes.Black, rect, sfRight);


                    __buttomRightTop += _fontBold14.GetHeight(g) + 30;
                    float __footerLeftWidth = (__xWidth * 50) / 100;
                    rect = new RectangleF(__footerLeft, __buttomRightTop, __footerLeftWidth, lineHeight);
                    g.DrawString("..............................................", _fontNormal12, Brushes.Black, rect, sfCenter);

                    float __footerCenterLeft = __footerLeftWidth;
                    float __footerCenterWidth = (__xWidth * 25) / 100;
                    rect = new RectangleF(__pageLeft + __footerCenterLeft, __buttomRightTop, __footerCenterWidth, lineHeight);
                    g.DrawString("..............................................", _fontNormal12, Brushes.Black, rect, sfCenter);


                    float __footerRightLeft = __footerLeftWidth + __footerCenterWidth;
                    rect = new RectangleF(__pageLeft + __footerRightLeft, __buttomRightTop, __footerCenterWidth, lineHeight);
                    g.DrawString("...............................................", _fontNormal12, Brushes.Black, rect, sfCenter);


                    __buttomRightTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__footerLeft, __buttomRightTop, __footerLeftWidth, lineHeight);
                    g.DrawString("ผู้ซื้อสินค้า", _fontNormal12, Brushes.Black, rect, sfCenter);

                    rect = new RectangleF(__pageLeft + __footerCenterLeft, __buttomRightTop, __footerCenterWidth, lineHeight);
                    g.DrawString("ผู้ส่งสินค้า", _fontNormal12, Brushes.Black, rect, sfCenter);

                    rect = new RectangleF(__pageLeft + __footerRightLeft, __buttomRightTop, __footerCenterWidth, lineHeight);
                    g.DrawString("ผู้มีอำนาจลงนาม", _fontNormal12, Brushes.Black, rect, sfCenter);
                    __buttomRightTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__footerLeft, __buttomRightTop, __footerLeftWidth, lineHeight);
                    g.DrawString("วันที่______/_______/______", _fontNormal12, Brushes.Black, rect, sfCenter);

                    rect = new RectangleF(__pageLeft + __footerCenterLeft, __buttomRightTop, __footerCenterWidth, lineHeight);
                    g.DrawString("วันที่______/_______/______", _fontNormal12, Brushes.Black, rect, sfCenter);

                    rect = new RectangleF(__pageLeft + __footerRightLeft, __buttomRightTop, __footerCenterWidth, lineHeight);
                    g.DrawString("วันที่______/_______/______", _fontNormal12, Brushes.Black, rect, sfCenter);
                }

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ขาย_ลดหนี้
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportSOCreditnote(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            ///SMLInventoryControl._gForm._formEnum.ขาย_ลดหนี้
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);


                RectangleF rect = new RectangleF(__pageWidth - 230, __y, 230, lineHeight);
                g.DrawString("ใบส่งคืน/ลดหนี้", _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 230, lineHeight);
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                }


                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 140;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 140;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);
                float __titleTop = __pageTop + 275;
                float __titleLeft = __pageLeft;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 10) + __titleTop); ;
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;

                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ลูกค้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยุ่ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);
                //----------------------------------------------------------------
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    //  string                 
                    g.DrawString(__setdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No.:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่ตามใบกำกับ:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).Length > 0)
                {
                    string __setTaxdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setTaxdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่ตามใบกำกับ:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                //หัวตาราง                            
                float __ylineTop = __pageTop + 253;
                float __ylineEnd = __detailTop + __detailHeight;
                float __xline1 = __pageLeft + ((__xWidth * 5) / 100);
                float __xline2 = __xline1 + ((__xWidth * 10) / 100);
                float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                float __xline4 = __xline3 + ((__xWidth * 23) / 100);
                float __xline5 = __xline4 + ((__xWidth * 8) / 100);
                float __xline6 = __xline5 + ((__xWidth * 8) / 100);
                float __xline7 = __xline6 + ((__xWidth * 8) / 100);
                float __xline8 = __xline7 + ((__xWidth * 8) / 100);

                float __xwidth1 = ((__xWidth * 5) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("ลำดับ", _fontBold13, Brushes.Black, rect, sfCenter);//  
                float __xwidth2 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("อ้างอิง", _fontBold13, Brushes.Black, rect, sfCenter);//     
                float __xwidth3 = ((__pageWidth * 20) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("รหัส", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth4 = ((__pageWidth * 23) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("ชื่อสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth5 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth6 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("หน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth7 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("ราคา", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth8 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline7, __titleTop, __xwidth8, __custTop);
                g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfCenter);//                

                float __xwidth9 = ((__pageWidth * 10) / 100);
                float __sumdetailRightLeft = __titleLeft;
                float __sumdetailRightWidth = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline8, __titleTop, __xwidth9, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//                                              

                float __buttomWidth = __xwidth1 + __xwidth2 + __xwidth3 + __xwidth4 + __xwidth5 + __xwidth6;


                float __buttomTopLeft = __sumdetailTop;
                float __buttomTopRight = __sumdetailTop;
                if (_printLine == true)
                {
                    float __remarkWidth = __xwidth1 + __xwidth2 + __xwidth3 + __xwidth4 + __xwidth5;
                    rect = new RectangleF(__pageLeft, __buttomTopLeft, __remarkWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ: ", _fontBold14, Brushes.Black, rect, sfLeft);
                    __buttomTopLeft += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__pageLeft + 20, __buttomTopLeft, __remarkWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal13, Brushes.Black, rect, sfLeft);
                    float __sumdetailwidth = __xwidth5 + __xwidth6 + __xwidth7;
                    rect = new RectangleF(__xline5, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("มูลค่าสินค้า :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline5, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ส่วนลด :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline5, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("จำนวนเงินหลังหักส่วนลด :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline5, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ภาษีมูลค่าเพิ่ม :", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline8, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__xline5, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ยอดรวมสุทธิ :", _fontBold13, Brushes.Black, rect, sfLeft);

                    rect = new RectangleF(__xline8, __buttomTopRight, __xwidth8, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                    float __pageLineTop = __buttomTopRight - _fontBold13.GetHeight(g);
                    if (_printCount > 0)
                    {
                        rect = new RectangleF(__pageLeft, __pageLineTop, __remarkWidth, __custTop);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                    }
                    rect = new RectangleF(__pageLeft, __buttomTopRight, __remarkWidth, __custTop);
                    try
                    {
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfCenter); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    float __xbwidth = (__xWidth * 50) / 100;
                    float __xbTop = __pageHeight - 50;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__pageLeft + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);
                    __xbTop += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้ออกเอกสาร", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__pageLeft + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้อนุมัติ", _fontNormal13, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        //New Form
        /// <summary>
        /// ขาย_ลดหนี้
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportSOCreditnoteII(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            ///SMLInventoryControl._gForm._formEnum.ขาย_ลดหนี้
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }

                string __branchTax = "[ " + ((__companyType == 0) ? "x" : " ") + " ] สำนักงานใหญ่ " + "[ " + ((__companyType == 1) ? "x" : " ") + " ] สาขา ลำดับที่ " + ((__companyType == 1) ? __companyBranchCode : "") + "";

                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษีอากร : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__branchTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);


                RectangleF rect = new RectangleF(__pageWidth - 230, __y, 230, lineHeight);
                g.DrawString("ใบลดหนี้/ใบกำกับภาษี", _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 230, lineHeight);
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                }


                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 140;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 140;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);
                float __titleTop = __pageTop + 305;
                float __titleLeft = __pageLeft;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 10) + __titleTop);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;

                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString("ผู้ซื้อ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);

                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTax, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);

                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยุ่ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 50, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("อ้างอิงใบกำกับภาษีเลขที่  ", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 150, __custTop, __detailWidth, __custTop);
                g.DrawString(__docNoRefer, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 400, __custTop, __detailWidth, __custTop);
                if (__checkString(__docDateRefer).Length > 0)
                {
                    // string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__docDateRefer).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString("ลงวันที่ ", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__cusLeft + 440, __custTop, __detailWidth, __custTop);
                    g.DrawString(__docDateRefer, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                else
                {
                    g.DrawString("ลงวันที่ ", _fontBold13, Brushes.Black, rect, sfLeft);
                }
                //----------------------------------------------------------------
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                int _vatType = (int)MyLib._myGlobal._decimalPhase(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_type].ToString());
                g.DrawString("วันที่/Date:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    //  string                 
                    g.DrawString(__setdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No.:", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                //__detailTop += _fontBold13.GetHeight(g);
                //rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                //g.DrawString("วันที่ตามใบกำกับ:", _fontBold13, Brushes.Black, rect, sfLeft);
                //rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                //if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).Length > 0)
                //{
                //    string __setTaxdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                //    g.DrawString(__setTaxdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                //}
                //__detailTop += _fontBold13.GetHeight(g);
                //rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                //g.DrawString("เลขที่ตามใบกำกับ:", _fontBold13, Brushes.Black, rect, sfLeft);
                //rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                //g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                //หัวตาราง                            
                float __ylineTop = __pageTop + 305;
                float __ylineEnd = __detailTop + __detailHeight;
                float __xline1 = __pageLeft + ((__xWidth * 10) / 100);
                float __xline2 = __xline1 + ((__xWidth * 40) / 100);
                float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                float __xline4 = __xline3 + ((__xWidth * 15) / 100);
                float __xline5 = __xline4 + ((__xWidth * 15) / 100);
                float __ylineMid = __detailTop + __detailHeight - 150;
                /* float __xline1 = __pageLeft + ((__xWidth * 15) / 100);
                    float __xline2 = __xline1 + ((__xWidth * 35) / 100);
                    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    float __xline4 = __xline3 + ((__xWidth * 15) / 100); */
                float __xwidth1 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("ลำดับ", _fontBold13, Brushes.Black, rect, sfCenter);//  
                float __xwidth2 = ((__pageWidth * 40) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รายการ", _fontBold13, Brushes.Black, rect, sfCenter);//     
                float __xwidth3 = ((__pageWidth * 20) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth4 = ((__pageWidth * 15) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("ราคาต่อหน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//                
                float __xwidth5 = ((__pageWidth * 15) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//                


                float __xwidth9 = ((__pageWidth * 10) / 100);
                float __sumdetailRightLeft = __titleLeft;
                float __sumdetailRightWidth = ((__pageWidth * 10) / 100);


                //  float __buttomWidth = __xwidth1 + __xwidth2 + __xwidth3 + __xwidth4 + __xwidth5 + __xwidth6;


                float __buttomTopLeft = __sumdetailTop;
                float __buttomTopRight = __sumdetailTop - 120;
                float __buttomRemarkTop = __sumdetailTop - 120;
                if (_printLine == true)
                {

                    //float __remarkWidth = __xwidth1 + __xwidth2 + __xwidth3 + __xwidth4 + __xwidth5;
                    //rect = new RectangleF(__pageLeft, __buttomTopLeft, __remarkWidth, __custTop);
                    //string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    //g.DrawString("หมายเหตุ: ", _fontBold14, Brushes.Black, rect, sfLeft);
                    //__buttomTopLeft += _fontBold14.GetHeight(g);
                    //rect = new RectangleF(__pageLeft + 20, __buttomTopLeft, __remarkWidth, __custTop);
                    //g.DrawString(__remarkResult, _fontNormal13, Brushes.Black, rect, sfLeft);
                    float __sumdetailwidth = (__xwidth1 + __xwidth2 + __xwidth3 + __xwidth4) - __pageLeft;
                    rect = new RectangleF(__pageLeft, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("รวมมูลค่าสินค้าตามใบกำกับภาษีเดิม", _fontBold13, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomTopRight, __xwidth5 - 5, __fontHeight14);
                    if (_vatType == 0 || _vatType == 1)
                    {
                        // รวมใน
                        __amountRefer = ((MyLib._myGlobal._decimalPhase(__amountRefer) * 100M) / 107M).ToString();
                    }
                    //g.DrawString(__checkNumstr(__amountRefer), _fontNormal13, Brushes.Black, rect, sfRight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._ref_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    string format2 = MyLib._myGlobal._getFormatNumber("m02"); //_global._getFormatNumber("m01");   
                    Decimal __amount3 = 0;
                    Decimal __value = 0;
                    Decimal __value2 = 0;
                    Decimal __value3 = 0;
                    Decimal __value4 = 0;
                    try
                    {
                        if (__amountRefer != null)
                        {
                            __value = (__amountRefer == null) ? 0M : MyLib._myGlobal._decimalPhase(__amountRefer);
                        }
                        string __getTotal_amount = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString();
                        if (__getTotal_amount != null)
                        {
                            __value2 = (__getTotal_amount == null) ? 0M : MyLib._myGlobal._decimalPhase(__getTotal_amount);
                            if (_vatType == 0 || _vatType == 1)
                            {
                                // รวมใน
                                __value2 = (__value2 * 100M) / 107M;
                            }

                        }
                        __amount3 = __value - __value2;
                    }
                    catch (Exception ex)
                    {
                    }
                    /*if (_vatType == 1)
                    {
                        // รวมใน
                        __amount3 = (__amount3 * 100M) / 107M;
                    }*/
                    rect = new RectangleF(__pageLeft, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("มูลค่าที่ถูกต้อง", _fontBold13, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomTopRight, __xwidth5 - 5, __fontHeight14);
                    //g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    //g.DrawString(__checkNumstr(__amount3.ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._ref_new_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);

                    rect = new RectangleF(__pageLeft, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ผลต่าง", _fontBold13, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomTopRight, __xwidth5 - 5, __fontHeight14);
                    //g.DrawString(__checkNumstr(__amount3.ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    decimal __amount4 = MyLib._myGlobal._decimalPhase(__amountRefer) - __amount3;// MyLib._myGlobal._decimalPhase(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString());
                    //g.DrawString(__checkNumstr(__amount4.ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("ภาษีมูลค่าเพิ่ม 7%", _fontBold13, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomTopRight, __xwidth5 - 5, __fontHeight14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);
                    __buttomTopRight += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomTopRight, __sumdetailwidth, __custTop);
                    g.DrawString("รวม", _fontBold13, Brushes.Black, rect, sfRight);
                    try
                    {
                        string __getTaxValue = __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString());
                        __value3 = (__getTaxValue == null) ? 0M : MyLib._myGlobal._decimalPhase(__getTaxValue);
                        __value4 = __amount4 + __value3;
                        rect = new RectangleF(__xline4, __buttomTopRight, __xwidth5 - 5, __custTop);
                        //g.DrawString(__checkNumstr(__value4.ToString()), _fontBold13, Brushes.Black, rect, sfRight); ฝฝ __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString())
                        g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontBold13, Brushes.Black, rect, sfRight);
                    }
                    catch (Exception ex)
                    {
                    }
                    //rect = new RectangleF(__xline4, __buttomTopRight, __xwidth5, __fontHeight14);
                    //g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);

                    //float __pageLineTop = __buttomTopRight - _fontBold13.GetHeight(g);
                    //if (_printCount > 0)
                    //{
                    //    rect = new RectangleF(__pageLeft, __pageLineTop, __remarkWidth, __custTop);
                    //    g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                    ////}                    
                    rect = new RectangleF(__pageLeft, __buttomRemarkTop, __sumdetailwidth, __custTop);
                    try
                    {
                        // string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__value4.ToString()));
                        g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    __buttomRemarkTop += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomRemarkTop, __sumdetailwidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ: ", _fontBold14, Brushes.Black, rect, sfLeft);
                    __buttomRemarkTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__pageLeft + 20, __buttomRemarkTop, __sumdetailwidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal13, Brushes.Black, rect, sfLeft);

                    float __xbwidth = (__xWidth * 33) / 100;
                    float __xbTop = __pageHeight - 50;
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);

                    rect = new RectangleF(__pageLeft + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);

                    rect = new RectangleF(__pageLeft + __xbwidth + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("....................................................", _fontNormal13, Brushes.Black, rect, sfCenter);

                    __xbTop += _fontBold13.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้ออกเอกสาร", _fontNormal13, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__pageLeft + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้รับเอกสาร", _fontNormal13, Brushes.Black, rect, sfCenter);

                    rect = new RectangleF(__pageLeft + __xbwidth + __xbwidth, __xbTop, __xbwidth, __custTop);
                    g.DrawString("ผู้อนุมัติ", _fontNormal13, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ขาย_เพิ่มหนี้
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportInvoiceAdd(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.ขาย_เพิ่มหนี้
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

                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }

                string __branchTax = "[ " + ((__companyType == 0) ? "x" : " ") + " ] สำนักงานใหญ่ " + "[ " + ((__companyType == 1) ? "x" : " ") + " ] สาขา ลำดับที่ " + ((__companyType == 1) ? __companyBranchCode : "") + "";

                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName + " " + __branchTax, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("โทร : " + __companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("Fax : " + __companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษีอากร : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                __lineY += _fontNormal14.GetHeight(g);
                RectangleF rect = new RectangleF(__pageLeft, __lineY, __pageWidth - __pageLeft, __fontHeightNormal14);
                g.DrawString(__strHeader1, _fontBold20, Brushes.Black, rect, sfCenter);
                __lineY += _fontNormal14.GetHeight(g) + 30;
                float __headRight = __pageLeft;
                float __headRightTop = __lineY;
                float __detailWidth = ((__pageWidth * 70) / 100);
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 50);
                rect = new RectangleF(__pageLeft, __lineY, 150, __fontHeightNormal14);
                g.DrawString("ลูกค้า : ", _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__pageLeft + 50, __lineY, __detailWidth, __fontHeightNormal14);
                g.DrawString(__customerCode, _fontNormal14, Brushes.Black, rect, sfLeft);
                __lineY += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft + 50, __lineY, __detailWidth, __lineY);
                g.DrawString(__customerName, _fontNormal14, Brushes.Black, rect, sfLeft);
                __lineY += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft + 50, __lineY, __detailWidth, __lineY);
                g.DrawString(__customerAddress, _fontNormal14, Brushes.Black, rect, sfLeft);
                __lineY += _fontNormal14.GetHeight(g);// +25;

                rect = new RectangleF(__pageLeft + 50, __lineY, __detailWidth, __lineY);
                g.DrawString(__customerTax, _fontNormal14, Brushes.Black, rect, sfLeft);
                __lineY += _fontNormal14.GetHeight(g);// +5;

                rect = new RectangleF(__pageLeft, __lineY, __detailWidth, __lineY);
                g.DrawString("บริษัทได้เพิ่มหนี้และเดบิตบัญชีของท่านตามรายการต่อไปนี้:-", _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, __fontHeightNormal14);
                g.DrawString("เลขที่ใบเพิ่มหนี้", _fontNormal14, Brushes.Black, rect, sfLeft);
                //__headRightTop += _fontHeaderNormal14.GetHeight(g);
                rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, __fontHeightNormal14);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                __headRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, __fontHeightNormal14);
                g.DrawString("วันที่", _fontNormal14, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDocDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, __fontHeightNormal14);
                    g.DrawString(__setDocDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __headRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, __fontHeightNormal14);
                g.DrawString("อ้างถึงใบกำกับภาษี", _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, __fontHeightNormal14);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._tax_doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                __headRightTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, __fontHeightNormal14);
                g.DrawString("พนักงานขาย", _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __headRightTop, __detailWidth, __fontHeightNormal14);
                string __salemanDetail = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_code].ToString() + "    " + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString();
                g.DrawString(__salemanDetail, _fontNormal14, Brushes.Black, rect, sfLeft);
                if (_pageNum > 1)
                {
                    __headRightTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, __fontHeightNormal14);
                    if (_printCount > 0)
                    {
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString() + " Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __headRightTop += _fontNormal14.GetHeight(g);
                        rect = new RectangleF(__detailLeft, __headRightTop, __detailWidth, __fontHeightNormal14);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);
                    }
                }

                float __titleTop2 = __pageTop + 330;
                float __xline1 = __pageLeft + ((__xWidth * 10) / 100);
                float __xline2 = __xline1 + ((__xWidth * 55) / 100);
                float __xline3 = __xline2 + ((__xWidth * 12) / 100);
                float __xline4 = __xline3 + ((__xWidth * 12) / 100);

                float __xlineWidth1 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__pageLeft, __titleTop2, __xlineWidth1, __fontHeightNormal14);
                g.DrawString("No.", _fontNormal14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth2 = ((__xWidth * 55) / 100);
                rect = new RectangleF(__xline1, __titleTop2, __xlineWidth2, __fontHeightNormal14);
                g.DrawString("รหัสสินค้า/รายละเอียด", _fontNormal14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth3 = ((__xWidth * 12) / 100);
                rect = new RectangleF(__xline2, __titleTop2, __xlineWidth3, __fontHeightNormal14);
                g.DrawString("จำนวน", _fontNormal14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth4 = ((__xWidth * 12) / 100);
                rect = new RectangleF(__xline3, __titleTop2, __xlineWidth4, __fontHeightNormal14);
                g.DrawString("หน่วยละ", _fontNormal14, Brushes.Black, rect, sfCenter);//

                //float __xlineWidth5 = __pageLeft + ((__xWidth * 9) / 100);
                float __xlineWidth5 = ((__xWidth * 11) / 100);

                rect = new RectangleF(__xline4, __titleTop2, __xlineWidth5, __fontHeightNormal14);
                g.DrawString("จำนวนเงิน", _fontNormal14, Brushes.Black, rect, sfCenter);//        

                float __detailTop = __pageTop + 320;
                float __detailHeight = __pageHeight - (__detailTop) - 400;
                float __buttomRightTop = (__detailTop + __detailHeight) + 3;
                //  float __buttomSumRightTop = (__detailTop + __detailHeight) + 3;
                float __buttomSumLeft = __xline4;
                float __buttomSumWidth = __xlineWidth5;

                float __footerLeft = __pageLeft;
                rect = new RectangleF(__footerLeft, __buttomRightTop, __xWidth, __fontHeightNormal14);
                string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                g.DrawString("สาเหตุการเพิ่มหนี้ " + __remarkResult, _fontNormal14, Brushes.Black, rect, sfLeft);

                if (_printLine == true)
                {
                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("รวม", _fontNormal14, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//รวม

                    __buttomRightTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("หัก ส่วนลด", _fontNormal14, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//หัก ส่วนลด

                    __buttomRightTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("จำนวนเงินหลังหักส่วนลด", _fontNormal14, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//จำนวนเงินหลังหักส่วนลด

                    __buttomRightTop += _fontBold14.GetHeight(g) + 20;
                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("มูลค่าของสินค้าหรือบริการตามใบกำกับภาษีเดิม", _fontNormal14, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__sumtaxOldAmount.ToString()), _fontNormal14, Brushes.Black, rect, sfRight);

                    __buttomRightTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("มูลค่าของสินค้าหรือบริการที่ถูกต้อง", _fontNormal14, Brushes.Black, rect, sfRight);
                    double __xtotal1 = __sumtaxOldAmount + Double.Parse(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__xtotal1.ToString()), _fontNormal14, Brushes.Black, rect, sfRight);


                    __buttomRightTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("ผลต่าง", _fontNormal14, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);

                    __buttomRightTop += _fontBold14.GetHeight(g);
                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("จำนวนภาษีมูลค่าเพิ่ม", _fontNormal14, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline3, __buttomRightTop, __xlineWidth4, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_rate].ToString()) + " %", _fontNormal14, Brushes.Black, rect, sfRight);//จำนวนภาษีมูลค่าเพิ่ม
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//จำนวนภาษีมูลค่าเพิ่ม

                    __buttomRightTop += _fontBold14.GetHeight(g);
                    try
                    {
                        rect = new RectangleF(__footerLeft, __buttomRightTop, __xWidth, __fontHeightNormal14);
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));  //ตัวเลขที่เป็นตัวหนังสือ
                        g.DrawString("( " + strangThai + " )", _fontNormal14, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    rect = new RectangleF(__xline1, __buttomRightTop, __xlineWidth2 + __xlineWidth3, __fontHeightNormal14);
                    g.DrawString("จำนวนเงินรวมทั้งสิ้น", _fontNormal14, Brushes.Black, rect, sfRight);
                    rect = new RectangleF(__xline4, __buttomRightTop, __xlineWidth5, __fontHeightNormal14);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal14, Brushes.Black, rect, sfRight);//จำนวนเงินหลังหักส่วนลด
                }
                else
                {
                    for (int __loopFont = 1; __loopFont <= 8; __loopFont++)
                    {
                        __buttomRightTop += _fontBold14.GetHeight(g);
                    }
                }
                float __footerxline1 = __pageLeft + __pageWidth / 3;
                float __footerxline2 = __footerxline1 + (__pageWidth / 3);
                float __buttomDetailTop = __buttomRightTop + 50;
                float __footerWidth = __pageWidth / 3;
                rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("ผู้ออกเอกสาร", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline1, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("พนักงานขาย", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline2, __buttomDetailTop, __footerWidth - __pageLeft, __fontHeightNormal14);
                g.DrawString("ผู้อนุมัติ", _fontNormal14, Brushes.Black, rect, sfCenter);
                __buttomDetailTop += _fontNormal14.GetHeight(g) + 10;
                rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("....................................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline1, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("....................................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline2, __buttomDetailTop, __footerWidth - __pageLeft, __fontHeightNormal14);
                g.DrawString("....................................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                __buttomDetailTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline1, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline2, __buttomDetailTop, __footerWidth - __pageLeft, __fontHeightNormal14);
                g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                __buttomDetailTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline1, __buttomDetailTop, __footerWidth, __fontHeightNormal14);
                g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                rect = new RectangleF(__footerxline2, __buttomDetailTop, __footerWidth - __pageLeft, __fontHeightNormal14);
                g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ขาย_ใบสั่งขาย
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportSaleOrder(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            // SMLInventoryControl._gForm._formEnum.ขาย_ใบสั่งขาย
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                g.DrawString("ใบสั่งขาย", _fontBold20, Brushes.Black, rect, sfCenter);
                __y += _fontBold18.GetHeight(g) - 10;
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                    if (_printCount > 0)
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString() + "  พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                __y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 120;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);
                float __titleTop = __pageTop + 260;
                float __titleLeft = __pageLeft;
                float __titleWidth = 0;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 1) + __titleTop); ;
                float __sumdetailLeft = __detailButtomWidth + 10;
                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;
                float __buttomDetailWidth = 0;
                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString(__customerTitle, _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal12, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่ : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 220, __custTop, 150, __custTop);
                g.DrawString("Fax : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 260, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold14, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                    g.DrawString(__setdocdate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No.:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                __detailTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("พนักงานขาย :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 80, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_code].ToString() + "/" + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                //__detailTop += _fontHeaderBold14.GetHeight(g);
                //rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                //g.DrawString("โทรศัพท์/Tel:", _fontHeaderBold12, Brushes.Black, rect, sfLeft);
                //rect = new RectangleF(__detailLeft + 140, __detailTop, __detailWidth, __custTop);
                //   g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString() + "  วัน", _fontHeaderNormal12, Brushes.Black, rect, sfLeft);
                //หัวตาราง                            
                float __titleTop2 = __titleTop + 20;
                float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                float __xline2 = __xline1 + ((__xWidth * 22) / 100);
                float __xline3 = __xline2 + ((__xWidth * 32) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 8) / 100);
                float __xline6 = __xline5 + ((__xWidth * 10) / 100);

                float __xwidth1 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("ลำดับที่", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__pageLeft, __titleTop2, __xwidth1, __custTop);
                g.DrawString("No", _fontBold14, Brushes.Black, rect, sfCenter);//               
                float __xwidth2 = ((__pageWidth * 22) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline1, __titleTop2, __xwidth2, __custTop);
                g.DrawString("Code", _fontBold14, Brushes.Black, rect, sfCenter);//
                float __xwidth3 = ((__pageWidth * 32) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("รายการ", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline2, __titleTop2, __xwidth3, __custTop);
                g.DrawString("Item", _fontBold14, Brushes.Black, rect, sfCenter);//                         
                float __xwidth4 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("คลัง", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline3, __titleTop2, __xwidth4, __custTop);
                g.DrawString("WH", _fontBold14, Brushes.Black, rect, sfCenter);//
                float __xwidth5 = ((__pageWidth * 8) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("พื้นที่เก็บ", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline4, __titleTop2, __xwidth5, __custTop);
                g.DrawString("SH", _fontBold14, Brushes.Black, rect, sfCenter);//
                float __xwidth6 = ((__pageWidth * 10) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("หน่วยนับ", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline5, __titleTop2, __xwidth6, __custTop);
                g.DrawString("Unit", _fontBold14, Brushes.Black, rect, sfCenter);//
                float __xwidth7 = ((__pageWidth * 12) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("จำนวน", _fontBold14, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline6, __titleTop2, __xwidth7, __custTop);
                g.DrawString("QTY", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __sumdetailRightLeft = __titleLeft;
                float __sumdetailRightWidth = ((__pageWidth * 11) / 100);
                if (_printLine == true)
                {

                    //หมายเหตุ             
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ : ", _fontBold14, Brushes.Black, rect, sfLeft);
                    __detailButtomTop += _fontNormal12.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal14, Brushes.Black, rect, sfLeft);
                    __detailButtomTop += _fontNormal12.GetHeight(g) + 30;

                    float __footerxline1 = __pageLeft + __pageWidth / 3;
                    float __footerxline2 = __footerxline1 + (__pageWidth / 3);
                    //  float __footerxline3 = __footerxline2 + (__pageWidth / 3);
                    float __footerWidth = __pageWidth / 3;
                    float __buttomBox1Top = ((__detailHeight + 1) + __detailTop);
                    float __buttomBoxWidth = (__xWidth / 2) + 130;
                    float __buttomBoxHeight = (__pageHeight - __buttomBox1Top) - 120;
                    //กรอบล่างขวา
                    float __buttomBox2Left = __pageLeft + __buttomBoxWidth;
                    float __buttomBox2Width = __xWidth - ((__xWidth / 2) + 130);
                    rect = new RectangleF(__buttomBox2Left + 10, __sumdetailTop, __xwidth5 + __xwidth6, __custTop);
                    g.DrawString("รวมจำนวนสินค้า", _fontBold14, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7, __custTop);
                    g.DrawString(__sumQty.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);//รวมจำนวน

                    float __rightbuttomTop = __buttomDetailTop;
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __custTop);
                    g.DrawString("ผู้อนุมัติ", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline1, __rightbuttomTop, __footerWidth, __custTop);
                    g.DrawString("ฝ่ายคลังสินค้า", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline2, __rightbuttomTop, __footerWidth - __pageLeft, __custTop);
                    g.DrawString("ฝ่ายขาย", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g) + 10;
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __custTop);
                    g.DrawString("....................................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline1, __buttomDetailTop, __footerWidth, __custTop);
                    g.DrawString("....................................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline2, __buttomDetailTop, __footerWidth - __pageLeft, __custTop);
                    g.DrawString("....................................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __custTop);
                    g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline1, __buttomDetailTop, __footerWidth, __custTop);
                    g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline2, __buttomDetailTop, __footerWidth - __pageLeft, __custTop);
                    g.DrawString("(........................................................)", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __footerWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline1, __buttomDetailTop, __footerWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                    rect = new RectangleF(__footerxline2, __buttomDetailTop, __footerWidth - __pageLeft, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                }

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportReserve(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า
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
                float __y = __pageTop;
                float __lineY = __pageTop;
                float __xWidth = __pageWidth - __pageLeft;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);

                RectangleF rect = new RectangleF(__pageWidth - 230, __y, 250, lineHeight);
                g.DrawString(__strHeader1, _fontBold20, Brushes.Black, rect, sfCenter);
                __y += _fontBold18.GetHeight(g) - 10;
                rect = new RectangleF(__pageWidth - 230, __y, 250, lineHeight);
                g.DrawString(__strHeader2, _fontBold18, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 250, lineHeight);
                    if (_printCount > 0)
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString() + "  พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 250, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                float __cusLeft = __pageLeft + 10;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);
                float __titleTop = __pageTop + 255;
                float __titleLeft = __pageLeft;
                float __titleWidth = 0;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 1) + __titleTop); ;
                float __sumdetailLeft = __detailButtomWidth + 10;
                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;
                float __buttomDetailWidth = 0;
                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString(__customerTitle, _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddressStr + ":", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTelStr, _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString(__customerFaxStr, _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString(__docDateStr, _fontBold13, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDocDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                    g.DrawString(__setDocDate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString(__docNostr, _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString(__duedatestr, _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).Length > 0)
                {
                    string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDueDate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString(__creditStr, _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString() + "  วัน", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold12.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("พนักงานขาย : ", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 80, __detailTop, __detailWidth, __custTop);
                string __salemanDetail = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_code].ToString() + "/" + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString();
                g.DrawString(__salemanDetail, _fontNormal13, Brushes.Black, rect, sfLeft);

                //หัวตาราง


                float __titleTop2 = __titleTop + 25;

                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 28) / 100);
                float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                float __xwidth1 = ((__xWidth * 22) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__pageLeft, __titleTop2, __xwidth1, __custTop);
                g.DrawString("Code", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth2 = ((__xWidth * 28) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รายการ", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline1, __titleTop2, __xwidth2, __custTop);
                g.DrawString("Name", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth3 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("หน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline2, __titleTop2, __xwidth3, __custTop);
                g.DrawString("Unit", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth4 = ((__xWidth * 8) / 100);
                //                float __leftButtomDetail = __titleLeft + ((__pageWidth * 11) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline3, __titleTop2, __xwidth4, __custTop);
                g.DrawString("QTY", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth5 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("ราคาขาย", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline4, __titleTop2, __xwidth5, __custTop);
                g.DrawString("Price", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth6 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline5, __titleTop2, __xwidth6, __custTop);
                g.DrawString("Discount", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth7 = ((__xWidth * 14) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline6, __titleTop2, __xwidth7, __custTop);
                g.DrawString("Amount", _fontBold13, Brushes.Black, rect, sfCenter);//
                if (_printLine == true)
                {
                    //หมายเหตุ             
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, 200, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ : " + __remarkResult, _fontNormal13, Brushes.Black, rect, sfLeft);

                    __detailButtomTop += _fontNormal12.GetHeight(g) + 50;
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    g.DrawString("ยอดเงินตัวหนังสือ", _fontBold13, Brushes.Black, rect, sfLeft);
                    float __moneyStringTop = __detailButtomTop + _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __moneyStringTop, __detailButtomWidth, __custTop);
                    try
                    {
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("รวมจำนวนเงิน", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 3, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//รวมจำนวนเงิน

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ส่วนลด:", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 3, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ส่วนลดการค่า

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ราคาสินค้า/บริการ:", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 3, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ราคาสินค้า/บริการ

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ภาษีมูลค่าเพิ่ม " + __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_rate].ToString()) + " %", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 3, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight); //ภาษีมูลค่าเพิ่ม

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("จำนวนเงินรวมทั้งสิ้น", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 3, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//จำนวนเงินรวมทั้งสิ้น


                    __buttomDetailWidth = __pageLeft + __pageWidth / 2;
                    float __rightbuttomTop = __buttomDetailTop;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้อนุมัติ / Approve By", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("............................................................................", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("(........................................................)", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__docDateStr + " _____/_____/______", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeft = __buttomDetailLeft + (__pageWidth / 2);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("พนักงานขาย/SALE", _fontBold13, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("............................................................................", _fontBold13, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("(........................................................)", _fontBold13, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__docDateStr + " _____/_____/______", _fontBold13, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ใบเสนอราคา
        /// </summary>
        /// <param name="g"></param>
        /// <param name="__pageLeft"></param>
        /// <param name="__pageTop"></param>
        /// <param name="__pageWidth"></param>
        /// <param name="__pageHeight"></param>
        public void __printDataReportQuotation(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //ใบเสนอราคา F002
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);

                ////Graphics g;
                ////g = e.Graphics;
                ////g.SmoothingMode = SmoothingMode.HighQuality;
                ////g.FillRectangle(Brushes.Yellow, new Rectangle(new Point(0, 0), this.ClientSize));

                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }

                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                RectangleF rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                g.DrawString("ใบเสนอราคา", _fontBold20, Brushes.Black, rect, sfCenter);
                __y += _fontBold18.GetHeight(g) - 10;
                rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                g.DrawString("QUOTATION", _fontBold18, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                    if (_printCount > 0)
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString() + "  พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 180, __y, 150, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }

                float __cusLeft = __pageLeft + 20;
                float __custTop = __pageTop + 125;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 100) + 10;
                float __detailTop = __pageTop + 125;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 100) / 2);
                float __titleTop = __pageTop + 260;
                float __titleLeft = __pageLeft;
                float __titleWidth = 0;
                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 1) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 1) + __titleTop); ;
                float __sumdetailLeft = __detailButtomWidth + 10;
                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __pageLeft;
                float __buttomDetailWidth = 0;
                //float __
                rect = new RectangleF(__cusLeft, __custTop, 150, lineHeight);
                g.DrawString(__customerTitle, _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("", _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddressStr + ":", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal13, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal12.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 150, __custTop);
                g.DrawString("Fax :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 290, __custTop, __detailWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal13, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date :", _fontBold13, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setdocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                    g.DrawString(__setdocdate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No. :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันครบกำหนด/DUEDATE :", _fontBold13, Brushes.Black, rect, sfLeft);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDuedate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                    g.DrawString(__setDuedate, _fontNormal13, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เครดิต/CREDIT DAY :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 150, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString() + "  วัน", _fontNormal13, Brushes.Black, rect, sfLeft);
                __detailTop += _fontBold13.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("พนักงานขาย :", _fontBold13, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 80, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString(), _fontNormal13, Brushes.Black, rect, sfLeft);

                //หัวตาราง                            
                float __titleTop2 = __titleTop + 20;
                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 27) / 100);
                float __xline3 = __xline2 + ((__xWidth * 9) / 100);
                float __xline4 = __xline3 + ((__xWidth * 9) / 100);
                float __xline5 = __xline4 + ((__xWidth * 11) / 100);
                float __xline6 = __xline5 + ((__xWidth * 11) / 100);
                float __xwidth1 = ((__xWidth * 22) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xwidth1, __custTop);
                g.DrawString("รหัสสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__pageLeft, __titleTop2, __xwidth1, __custTop);
                g.DrawString("Code", _fontBold13, Brushes.Black, rect, sfCenter);//               
                float __xwidth2 = ((__pageWidth * 27) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xwidth2, __custTop);
                g.DrawString("รายการสินค้า", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline1, __titleTop2, __xwidth2, __custTop);
                g.DrawString("Name", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth3 = ((__pageWidth * 9) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xwidth3, __custTop);
                g.DrawString("หน่วย", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline2, __titleTop2, __xwidth3, __custTop);
                g.DrawString("Unit", _fontBold13, Brushes.Black, rect, sfCenter);//                         
                float __xwidth4 = ((__pageWidth * 9) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xwidth4, __custTop);
                g.DrawString("จำนวน", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline3, __titleTop2, __xwidth4, __custTop);
                g.DrawString("QTY", _fontBold13, Brushes.Black, rect, sfCenter);//
                float __xwidth5 = ((__pageWidth * 11) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xwidth5, __custTop);
                g.DrawString("ราคาขาย", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline4, __titleTop2, __xwidth5, __custTop);
                g.DrawString("Price", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xwidth6 = ((__pageWidth * 11) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xwidth6, __custTop);
                g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline5, __titleTop2, __xwidth6, __custTop);
                g.DrawString("Discount", _fontBold13, Brushes.Black, rect, sfCenter);//

                float __xwidth7 = ((__pageWidth * 11) / 100);
                float __sumdetailRightLeft = __titleLeft;
                float __sumdetailRightWidth = ((__pageWidth * 11) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xwidth7, __custTop);
                g.DrawString("จำนวนเงิน", _fontBold13, Brushes.Black, rect, sfCenter);//
                rect = new RectangleF(__xline6, __titleTop2, __xwidth7, __custTop);
                g.DrawString("Amount", _fontBold13, Brushes.Black, rect, sfCenter);//
                if (_printLine == true)
                {
                    //หมายเหตุ             
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ : ", _fontBold13, Brushes.Black, rect, sfLeft);
                    __detailButtomTop += _fontNormal12.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal13, Brushes.Black, rect, sfLeft);
                    __detailButtomTop += _fontNormal12.GetHeight(g) + 30;
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    g.DrawString("ยอดเงินตัวหนังสือ :", _fontBold13, Brushes.Black, rect, sfLeft);
                    float __moneyStringTop = __detailButtomTop + _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __moneyStringTop, __detailButtomWidth, __custTop);
                    try
                    {
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        g.DrawString("( " + strangThai + " )", _fontBold13, Brushes.Black, rect, sfLeft); //ยอดเงินเป็นตัวเลข                     
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("รวมจำนวนเงิน", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//รวมจำนวนเงิน

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ส่วนลดการค่า

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ราคาสินค้า/บริการ", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ราคาสินค้า/บริการ

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("ภาษีมูลค่าเพิ่ม   " + __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_rate].ToString()) + " %", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight); //ภาษีมูลค่าเพิ่ม

                    __sumdetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __sumdetailTop, __sumdetailWidth, __custTop);
                    g.DrawString("จำนวนเงินรวมทั้งสิ้น", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline6, __sumdetailTop, __xwidth7 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//จำนวนเงินรวมทั้งสิ้น


                    __buttomDetailWidth = __pageLeft + __pageWidth / 2;
                    float __rightbuttomTop = __buttomDetailTop;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้อนุมัติ/APPROVE", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g) + 10;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("............................................................................", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("(........................................................)", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontBold13, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeft = __buttomDetailLeft + (__pageWidth / 2);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("พนักงานขาย/SALE", _fontBold13, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g) + 10;
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("............................................................................", _fontBold13, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("(........................................................)", _fontBold13, Brushes.Black, rect, sfCenter);
                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __rightbuttomTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontBold13, Brushes.Black, rect, sfCenter);
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

                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                //float lineHeight = _fontHeaderBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strCompany = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strCompany.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __lineY += __fontHeightNormal14 + 20;
                float __cusY = __pageTop + 60;
                rectTitleDetail = new RectangleF(__pageLeft, __cusY, __xWidth, __fontHeight14);
                g.DrawString("ลูกค้า", _fontBold14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 50, __cusY, __xWidth, __fontHeight14);
                g.DrawString(__customerName, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __cusY += __fontHeightNormal14 - 5;
                rectTitleDetail = new RectangleF(__pageLeft + 50, __cusY, __xWidth, __fontHeight14);
                g.DrawString(__customerAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __cusY += __fontHeightNormal14 - 5;
                rectTitleDetail = new RectangleF(__pageLeft + 50, __cusY, __xWidth, __fontHeight14);
                g.DrawString(__customerAddress2, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                float lineHeight = _fontBold14.GetHeight(g);
                RectangleF rect = new RectangleF(__pageWidth - 250, __y, 220, lineHeight);
                g.DrawString("ใบเสร็จรับเงิน/ใบกำกับภาษี", _fontBold14, Brushes.Black, rect, sfCenter);
                __y += _fontBold14.GetHeight(g) - 10;
                rect = new RectangleF(__pageWidth - 250, __y, 220, lineHeight);
                g.DrawString("RECEIPT/TAX INVOICE", _fontBold14, Brushes.Black, rect, sfCenter);
                float __yDocNo = __pageTop + 60;
                float __titleBoxLeft = __pageWidth - 250;
                float __titleBoxWidth = __pageWidth - 180;
                float __docnoHeight = _fontNormal14.GetHeight(g);
                RectangleF rectDocno = new RectangleF(__titleBoxLeft, __yDocNo, 70, __docnoHeight);
                g.DrawString("เลขที่", _fontNormal14, Brushes.Black, rectDocno, sfCenter);
                rectDocno = new RectangleF(__titleBoxLeft + 80, __yDocNo, __pageWidth, __docnoHeight);
                g.DrawString(__docnoResult, _fontNormal14, Brushes.Black, rectDocno, sfLeft);
                __yDocNo += __docnoHeight + 8;
                rectDocno = new RectangleF(__titleBoxLeft, __yDocNo, 70, __docnoHeight);
                g.DrawString("วันที่", _fontNormal14, Brushes.Black, rectDocno, sfCenter);
                rectDocno = new RectangleF(__titleBoxLeft + 80, __yDocNo, 70, __docnoHeight);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDocdate, _fontNormal14, Brushes.Black, rectDocno, sfLeft);
                }
                //หนักงานขาย - หมายเหตุ
                float __topTitleDetail = __pageTop + 210;
                float __saleWidth = ((__pageWidth * 26) / 100);
                rectTitleDetail = new RectangleF(__pageLeft, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("พนักงานขาย", _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);//พนักงานขาย
                float __titleDetailHeight = __topTitleDetail + _fontNormal14.GetHeight(g) + 10;
                rectTitleDetail = new RectangleF(__pageLeft, __titleDetailHeight, __saleWidth, __docnoHeight);
                string __salemanData = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_code].ToString() + ":" + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString();
                g.DrawString(__salemanData, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter); //พนักงานขาย
                float __titleDetailLeft1 = __saleWidth + ((__pageWidth * 13) / 100);
                float __xlineLeft2 = __pageLeft + ((__pageWidth * 26) / 100);
                __saleWidth = ((__pageWidth * 13) / 100);
                rectTitleDetail = new RectangleF(__xlineLeft2, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("เครดิตวัน", _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);//"เครดิต"
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth, __docnoHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString(), _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter); //วัน
                // g.DrawString("วัน", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);
                __xlineLeft2 = __xlineLeft2 + ((__pageWidth * 13) / 100);
                __saleWidth = ((__pageWidth * 13) / 100);
                rectTitleDetail = new RectangleF(__xlineLeft2, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("วันครบกำหนด", _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);

                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth, __docnoHeight);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).Length > 0)
                {
                    string __setduedate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setduedate, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                }
                __xlineLeft2 = __pageWidth - __xlineLeft2;
                __saleWidth = __pageWidth - __xlineLeft2;
                rectTitleDetail = new RectangleF(__xlineLeft2, __topTitleDetail, __saleWidth, __docnoHeight);
                g.DrawString("หมายเหตุ", _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth, __docnoHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString(), _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 32) / 100);
                float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                float __xline7 = __xline6 + ((__xWidth * 11) / 100);
                float __topDetailtitle = __pageTop + 290;
                float __xlineWidth1 = ((__xWidth * 22) / 100);
                rectTitleDetail = new RectangleF(__pageLeft, __topDetailtitle, __xlineWidth1, __docnoHeight);
                g.DrawString("รหัสสินค้า", _fontBold12, Brushes.Black, rectTitleDetail, sfCenter);
                float __xlineWidth2 = ((__xWidth * 32) / 100);
                rectTitleDetail = new RectangleF(__xline1, __topDetailtitle, __xlineWidth2, __docnoHeight);
                g.DrawString("รายการ", _fontBold12, Brushes.Black, rectTitleDetail, sfCenter);
                float __xlineWidth3 = ((__xWidth * 8) / 100);
                rectTitleDetail = new RectangleF(__xline2, __topDetailtitle, __xlineWidth3, __docnoHeight);
                g.DrawString("หน่วยนับ", _fontBold12, Brushes.Black, rectTitleDetail, sfCenter);
                float __xlineWidth4 = ((__xWidth * 8) / 100);
                rectTitleDetail = new RectangleF(__xline3, __topDetailtitle, __xlineWidth4, __docnoHeight);
                g.DrawString("จำนวน", _fontBold12, Brushes.Black, rectTitleDetail, sfCenter);
                float __xlineWidth5 = ((__xWidth * 10) / 100);
                rectTitleDetail = new RectangleF(__xline4, __topDetailtitle, __xlineWidth5, __docnoHeight);
                g.DrawString("ราคาต่อหน่วย", _fontBold12, Brushes.Black, rectTitleDetail, sfCenter);
                float __xlineWidth6 = ((__xWidth * 9) / 100);
                rectTitleDetail = new RectangleF(__xline5, __topDetailtitle, __xlineWidth6, __docnoHeight);
                g.DrawString("ส่วนลด", _fontBold12, Brushes.Black, rectTitleDetail, sfCenter);
                float __xlineWidth7 = ((__xWidth * 11) / 100);
                // float __widthButtomNumber = ((__pageWidth * 11) / 100);
                rectTitleDetail = new RectangleF(__xline6, __topDetailtitle, __xlineWidth7, __docnoHeight);
                g.DrawString("จำนวนเงิน", _fontBold12, Brushes.Black, rectTitleDetail, sfCenter);
                float __detailTop = __pageTop + 300;
                float __detailheight = __pageHeight - (__detailTop);
                float __buttomTop = __detailTop + (__detailheight / 2) + __lineNum;
                float __buttomLeft = __pageLeft;
                float __buttomWidth = __pageLeft + ((__pageWidth * 47) / 100) + 30;

                rectTitleDetail = new RectangleF(__buttomLeft, __buttomTop, __buttomWidth, __docnoHeight);
                try
                {
                    string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                    g.DrawString("( " + strangThai + " )", _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter); //ยอดเงินเป็นตัวเลข
                    //  g.DrawString("( " + _gForm._convertNumToBaht(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()) + " )", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                float __remarkTop = __buttomTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft, __remarkTop, __buttomWidth + 20, __docnoHeight);
                g.DrawString("ใบเสร็จรับเงินฉบับนี้จะสมบูรณ์ต่อเมื่อบริษัทฯ ได้เรียกเก็บเงินตามเช็คเรียบร้อยแล้วเท่านั้น", _fontNormal12, Brushes.Black, rectTitleDetail, sfCenter);
                __remarkTop = __remarkTop + _fontNormal14.GetHeight(g) + __lineNum;
                rectTitleDetail = new RectangleF(__buttomLeft, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("ชำระเงินโดย", _fontBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 20, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("เงินสด :", _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("จำนวนเงิน ...........................................................", _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontNormal14.GetHeight(g) + 20;
                rectTitleDetail = new RectangleF(__buttomLeft + 20, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("เช็ค :", _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("จำนวนเงิน ...........................................................", _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("ธนาคาร ...........................................................", _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __remarkTop = __remarkTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__buttomLeft + 40, __remarkTop, __buttomWidth, __docnoHeight);
                g.DrawString("เลขที่เช็ค ...........................................................", _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomLeft = __pageLeft + __buttomWidth;
                // __buttomWidth = ((__pageWidth * 12) / 100) + ((__pageWidth * 15) / 100);
                __buttomWidth = __xlineWidth4 + __xlineWidth5 + __xlineWidth6;

                rectTitleDetail = new RectangleF(__xline3, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("รวมราคาทั้งสิ้น", _fontBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline3, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("หักส่วนลด", _fontBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline3, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("ยอดหลังหักส่วนลด", _fontBold14, Brushes.Black, rectTitleDetail, sfLeft);
                //__buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                //rectTitleDetail = new RectangleF(__xline3, __buttomTop, __buttomWidth, __docnoHeight);
                //g.DrawString("หักเงินมัดจำ", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                //__buttomTop = __buttomTop + _fontHeaderNormal14.GetHeight(g);
                //rectTitleDetail = new RectangleF(__xline3, __buttomTop, __buttomWidth, __docnoHeight);
                //g.DrawString("ยอดหลังหักเงินมัดจำ", _fontHeaderBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline3, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("ภาษีมูลค่าเพิ่ม " + " " + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._vat_rate].ToString() + " %", _fontBold14, Brushes.Black, rectTitleDetail, sfLeft);
                __buttomTop = __buttomTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline3, __buttomTop, __buttomWidth, __docnoHeight);
                g.DrawString("รวมเงินทั้งสิ้น", _fontBold14, Brushes.Black, rectTitleDetail, sfLeft);
                float __buttomNumberTop = __detailTop + (__detailheight / 2) + __lineNum;
                rectTitleDetail = new RectangleF(__xline6, __buttomNumberTop, __xlineWidth7, __docnoHeight);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal14, Brushes.Black, rectTitleDetail, sfRight); //รวมราคาทั้งสิ้น
                float __buttomDetailHeight = __buttomNumberTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline6, __buttomDetailHeight, __xlineWidth7, __docnoHeight);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal14, Brushes.Black, rectTitleDetail, sfRight); //หักส่วนลด
                __buttomDetailHeight = __buttomDetailHeight + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline6, __buttomDetailHeight, __xlineWidth7, __docnoHeight);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal14, Brushes.Black, rectTitleDetail, sfRight);//ยอดหลังหักส่วนลด
                //__buttomDetailHeight = __buttomDetailHeight + _fontHeaderNormal14.GetHeight(g);
                //rectTitleDetail = new RectangleF(__xline6, __buttomDetailHeight, __xlineWidth7, __docnoHeight);
                //g.DrawString("", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);//หักเงินมัดจำ
                //__buttomDetailHeight = __buttomDetailHeight + _fontHeaderNormal14.GetHeight(g);
                //rectTitleDetail = new RectangleF(__xline6, __buttomDetailHeight, __xlineWidth7, __docnoHeight);
                //g.DrawString("", _fontHeaderNormal14, Brushes.Black, rectTitleDetail, sfRight);//ยอดหลังหักเงินมัดจำ
                __buttomDetailHeight = __buttomDetailHeight + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline6, __buttomDetailHeight, __xlineWidth7, __docnoHeight);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal14, Brushes.Black, rectTitleDetail, sfRight);//ภาษีมูลค่าเพิ่ม
                __buttomDetailHeight = __buttomDetailHeight + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline6, __buttomDetailHeight, __xlineWidth7, __docnoHeight);
                g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal14, Brushes.Black, rectTitleDetail, sfRight);//รวมเงินทั้งสิ้น
                float __buttomDetailTop = __pageHeight - 80;
                // __topDetailtitleWidth = __widthButtomNumber + __buttomWidth;
                float __buttomDetailwidth = __xlineWidth4 + __xlineWidth5 + __xlineWidth6 + __xlineWidth7;
                rectTitleDetail = new RectangleF(__xline3, __buttomDetailTop, __buttomDetailwidth, __docnoHeight);
                g.DrawString("ผู้รับเงิน ............................................", _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __buttomDetailTop = __buttomDetailTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline3, __buttomDetailTop, __buttomDetailwidth, __docnoHeight);
                g.DrawString("(....................................................)", _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __buttomDetailTop = __buttomDetailTop + _fontNormal14.GetHeight(g);
                rectTitleDetail = new RectangleF(__xline3, __buttomDetailTop, __buttomDetailwidth, __docnoHeight);
                g.DrawString("วันที่ ............./............./...........", _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);


            }
            catch (Exception ex)
            {
            }
        }

        void __printDataReportReceiptICIForm(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //ใบเสร็จรับเงิน-ใบกำกับภาษี  F001 ICI Form
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop + 140;
                float __customerY = __pageTop + 125;
                float __fontHeight14 = _fontBold14.GetHeight(g);



                float __fontHeightNormal12 = _fontNormal12.GetHeight(g);
                RectangleF rectTitleDetail = new RectangleF(__pageLeft, __customerY, __xWidth, __fontHeightNormal12);
                g.DrawString(__customerName, _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft);
                __customerY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft, __customerY, __xWidth, __fontHeight14);
                g.DrawString(__customerAddress, _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft);
                __customerY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft, __customerY, __xWidth, __fontHeight14);
                g.DrawString(__customerAddress2, _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft);

                // toe draw branch_type
                string __branchTax = "[ " + ((__companyType == 0) ? "x" : " ") + " ] สำนักงานใหญ่ " + "[ " + ((__companyType == 1) ? "x" : " ") + " ] สาขา ลำดับที่ " + ((__companyType == 1) ? __companyBranchCode : "") + "";
                RectangleF rectBranchType = new RectangleF(__pageWidth - 220, __pageTop + 35, 200, __fontHeightNormal12);
                g.DrawString(__branchTax, _fontNormal12, Brushes.Black, rectBranchType, sfLeft);

                float __yDocNo = __pageTop + 135;
                float __titlePage = __pageWidth - 45;
                float __titleBoxLeft = __pageWidth - 120;
                float __titleBoxWidth = __pageWidth - 180;
                float __docnoHeight = _fontNormal12.GetHeight(g);
                float __memoHeight = _fontBold18.GetHeight(g);
                RectangleF rectDocno = new RectangleF(__titlePage, __yDocNo - 30, 60, __docnoHeight);
                if (_pageNum > 1)
                {
                    g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal12, Brushes.Black, rectDocno, sfLeft);
                }

                rectDocno = new RectangleF(__titleBoxLeft, __yDocNo + 2, 100, __docnoHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_no].ToString(), _fontNormal12, Brushes.Black, rectDocno, sfLeft);
                __yDocNo += _fontBold14.GetHeight(g);
                rectDocno = new RectangleF(__titleBoxLeft, __yDocNo, 100, __docnoHeight);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDocdate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDocdate, _fontNormal12, Brushes.Black, rectDocno, sfLeft);
                }
                //หนักงานขาย - หมายเหตุ
                float __topTitleDetail = __pageTop + 220;
                float __saleWidth = ((__pageWidth * 27) / 100);
                float __titleDetailHeight = __topTitleDetail;
                rectTitleDetail = new RectangleF(__pageLeft + 10, __titleDetailHeight, __saleWidth, __docnoHeight);
                string __salemanData = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_code].ToString() + ":" + __dsTop.Tables[0].Rows[0][_g.d.ic_trans._sale_name].ToString();
                g.DrawString(__salemanData, _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft); //พนักงานขาย
                float __titleDetailLeft1 = __saleWidth + ((__pageWidth * 13) / 100);
                float __xlineLeft2 = __pageLeft + ((__pageWidth * 27) / 100);
                float __saleWidth1 = ((__pageWidth * 13) / 100);
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth1, __docnoHeight);
                int __checkCreditDay = __checkInt(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString());
                if (__checkCreditDay > 0)
                {
                    g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_day].ToString(), _fontNormal12, Brushes.Black, rectTitleDetail, sfCenter); //วัน
                }
                __xlineLeft2 = __xlineLeft2 + ((__pageWidth * 13) / 100);
                float __saleWidth2 = ((__pageWidth * 13) / 100);
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth2, __docnoHeight);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).Length > 0)
                {
                    string __setduedate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._credit_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setduedate, _fontNormal12, Brushes.Black, rectTitleDetail, sfCenter);
                }
                __xlineLeft2 = __xlineLeft2 + ((__pageWidth * 13) / 100);
                float __saleWidth3 = ((__pageWidth * 47) / 100);
                rectTitleDetail = new RectangleF(__xlineLeft2, __titleDetailHeight, __saleWidth3, __docnoHeight);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString(), _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft);

                float __xline1 = __pageLeft + ((__xWidth * 22) / 100);
                float __xline2 = __xline1 + ((__xWidth * 32) / 100);
                float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                float __xline7 = __xline6 + ((__xWidth * 11) / 100);
                float __topDetailtitle = __pageTop + 290;
                float __xlineWidth1 = ((__xWidth * 22) / 100);
                float __xlineWidth2 = ((__xWidth * 32) / 100);
                float __xlineWidth3 = ((__xWidth * 8) / 100);
                float __xlineWidth4 = ((__xWidth * 8) / 100);
                float __xlineWidth5 = ((__xWidth * 10) / 100);
                float __xlineWidth6 = ((__xWidth * 9) / 100);
                float __xlineWidth7 = ((__xWidth * 11) / 100);
                float __buttomTop = __pageHeight - 285;
                float __buttomLeft = __pageLeft;
                float __buttomWidth = __pageLeft + ((__pageWidth * 47) / 100) + 30;

                rectTitleDetail = new RectangleF(__buttomLeft + 10, __buttomTop - 3, __buttomWidth, __docnoHeight);
                if (_printLine == true)
                {
                    try
                    {
                        string strangThai = SMLInventoryControl._gForm._convertNumToBaht(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()));
                        g.DrawString("( " + strangThai + " )", _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft); //ยอดเงินเป็นตัวเลข
                        float _yPrintData = __buttomTop + 50;
                        if (_pageNum > 1)
                        {
                            if (_printCount == 0)
                            {
                                rectTitleDetail = new RectangleF(__buttomLeft, _yPrintData, __buttomWidth, __memoHeight);
                                g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold12, Brushes.Black, rectTitleDetail, sfLeft); //ยอดเงินเป็นตัวเลข
                            }
                            else
                            {
                                //SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13);
                                rectTitleDetail = new RectangleF(__buttomLeft, _yPrintData, __buttomWidth, __memoHeight);
                                g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft);
                                SizeF __strMemo = g.MeasureString("พิมพ์ซ้ำครั้งที่ ", _fontNormal12);
                                rectTitleDetail = new RectangleF(__buttomLeft + __strMemo.Width + 8, _yPrintData, __buttomWidth - __strMemo.Width, __memoHeight);
                                g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold12, Brushes.Black, rectTitleDetail, sfLeft);
                            }
                        }
                        else
                        {
                            if (_printCount > 0)
                            {
                                rectTitleDetail = new RectangleF(__buttomLeft, _yPrintData, __buttomWidth, __memoHeight);
                                g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal12, Brushes.Black, rectTitleDetail, sfLeft);
                                SizeF __strMemo = g.MeasureString("พิมพ์ซ้ำครั้งที่ ", _fontNormal12);
                                rectTitleDetail = new RectangleF(__buttomLeft + __strMemo.Width + 8, _yPrintData, __buttomWidth - __strMemo.Width, __memoHeight);
                                g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold12, Brushes.Black, rectTitleDetail, sfLeft);
                            }
                            else
                            {
                                rectTitleDetail = new RectangleF(__buttomLeft, _yPrintData, __buttomWidth, __memoHeight);
                                g.DrawString("ไม่รับคืนหรือเปลี่ยนสินค้าทุกกรณี", _fontBold12, Brushes.Black, rectTitleDetail, sfLeft);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    float __buttomNumberTop1 = __pageHeight - 290;
                    rectTitleDetail = new RectangleF(__xline6 + 12, __buttomNumberTop1, __xlineWidth7, __docnoHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_value].ToString()), _fontNormal12, Brushes.Black, rectTitleDetail, sfRight); //รวมราคาทั้งสิ้น
                    __buttomNumberTop1 += _fontBold14.GetHeight(g);

                    string __discountWord = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._discount_word].ToString();
                    if (__discountWord.IndexOf("%") != -1)
                    {
                        rectTitleDetail = new RectangleF(__xline6 - __xlineWidth7, __buttomNumberTop1, __xlineWidth7, __docnoHeight);
                        g.DrawString(__checkNumstrDiscount(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._discount_word].ToString()), _fontNormal12, Brushes.Black, rectTitleDetail, sfRight); // ส่วนลด %                
                    }

                    rectTitleDetail = new RectangleF(__xline6 + 12, __buttomNumberTop1, __xlineWidth7, __docnoHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_discount].ToString()), _fontNormal12, Brushes.Black, rectTitleDetail, sfRight); //หักส่วนลด                
                    __buttomNumberTop1 += _fontBold14.GetHeight(g);
                    rectTitleDetail = new RectangleF(__xline6 + 12, __buttomNumberTop1, __xlineWidth7, __docnoHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString()), _fontNormal12, Brushes.Black, rectTitleDetail, sfRight);//ยอดหลังหักส่วนลด
                    __buttomNumberTop1 += _fontBold14.GetHeight(g);
                    rectTitleDetail = new RectangleF(__xline6 + 12, __buttomNumberTop1, __xlineWidth7, __docnoHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_vat_value].ToString()), _fontNormal12, Brushes.Black, rectTitleDetail, sfRight);//ภาษีมูลค่าเพิ่ม
                    __buttomNumberTop1 += _fontBold14.GetHeight(g);
                    rectTitleDetail = new RectangleF(__xline6 + 12, __buttomNumberTop1, __xlineWidth7, __docnoHeight);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString()), _fontNormal12, Brushes.Black, rectTitleDetail, sfRight);//รวมเงินทั้งสิ้น          
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void __printDataReportAPPaybill(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบรับวางบิล
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                //  float __companyWidth = ((__xWidth / 2) + 100);
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strArName = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strArName.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);


                RectangleF rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                g.DrawString("ใบรับวางบิล", _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                    if (_printCount > 0)
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString() + "  พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                //__y += _fontBold18.GetHeight(g) - 10;

                //__y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 20;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 55) + 10;
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 55) / 2);

                float __titleLeft = __pageLeft;
                float __titleWidth = (__xWidth / 2) + 100;

                //float __
                rect = new RectangleF(__cusLeft, __custTop, 100, lineHeight);
                g.DrawString("ชื่อลูกค้า : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่ : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, 100, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 100, __custTop);
                g.DrawString("Fax :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                //__detailTop += _fontBold14.GetHeight(g);
                //rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                //g.DrawString("พนักงานขาย/Sale: ", _fontBold14, Brushes.Black, rect, sfLeft);
                //rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                //g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._sale_code].ToString() + ":" + __dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._sale_name].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                float __titleTop = __pageTop + 255;
                float __titleTop2 = __pageTop + 280;
                float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                float __xline4 = __xline3 + ((__xWidth * 18) / 100);
                float __xline5 = __xline4 + ((__xWidth * 18) / 100);

                //หัวตาราง                             
                float __xlineWidth1 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xlineWidth1, __custTop);
                g.DrawString("ลำดับ", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth2 = ((__xWidth * 20) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xlineWidth2, __custTop);
                g.DrawString("เลขที่เอกสาร", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth3 = ((__xWidth * 20) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xlineWidth3, __custTop);
                g.DrawString("วันที่เอกสาร", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth4 = ((__xWidth * 18) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xlineWidth4, __custTop);
                g.DrawString("วันครบกำหนด", _fontBold14, Brushes.Black, rect, sfCenter);//


                float __xlineWidth5 = ((__xWidth * 18) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xlineWidth5, __custTop);
                g.DrawString("มูลค่าคงเหลือ", _fontBold14, Brushes.Black, rect, sfCenter);//


                float __xlineWidth6 = ((__xWidth * 16) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xlineWidth6, __custTop);
                g.DrawString("ยอดรับวางบิล", _fontBold14, Brushes.Black, rect, sfCenter);//                

                float __sumdetailRightLeft = __xline5 - 10;
                float __sumdetailRightWidth = ((__pageWidth * 14) / 100);

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 5) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 1) + __titleTop); ;
                float __sumdetailLeft = __detailButtomWidth + 10;
                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __xWidth - ((__xWidth / 2) + 130);
                float __buttomDetailWidth = 0;
                float __buttomDetailLeftTop = (__detailButtomTop + __detailButtomHeight) + 3;
                __buttomDetailWidth = __pageLeft + __pageWidth / 2;
                float __rightbuttomTop = __detailButtomTop;
                //หมายเหตุ             
                if (_printLine == true)
                {
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ:", _fontBold14, Brushes.Black, rect, sfLeft);
                    __detailButtomTop += _fontNormal12.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal14, Brushes.Black, rect, sfLeft);



                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("มูลค่าสินค้า", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __rightbuttomTop, __xlineWidth6 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//รวมจำนวนเงิน

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __rightbuttomTop, __xlineWidth6 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_discount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ส่วนลดการค่า

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("ยอดก่อนภาษี", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __rightbuttomTop, __xlineWidth6 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_before_vat].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ราคาสินค้า/บริการ

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    //  g.DrawString("ภาษีมูลค่าเพิ่ม   " + __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._vat_rate].ToString()) + " %", _fontBold13, Brushes.Black, rect, sfLeft);
                    g.DrawString("ภาษีมูลค่าเพิ่ม   ", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __rightbuttomTop, __xlineWidth6 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_vat_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight); //ภาษีมูลค่าเพิ่ม

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("รวมมูลค่าทั้งสิ้น", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline5, __rightbuttomTop, __xlineWidth6 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_net_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//จำนวนเงินรวมทั้งสิ้น


                    __buttomDetailTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้รับวางบิล", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);

                    __buttomDetailLeft = __buttomDetailWidth;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__quotationSaleCodeStr, _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeftTop += _fontNormal14.GetHeight(g) + 20;

                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString("...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeftTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้วางบิล", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeftTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void __printDataReportAPDebtPaybill(Graphics g, float __pageLeft, float __pageTop, float __pageWidth, float __pageHeight)
        {
            //SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบจ่ายชำระหนี้
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
                float __xWidth = __pageWidth - __pageLeft;
                float __y = __pageTop;
                float __lineY = __pageTop;
                float lineHeight = _fontBold18.GetHeight(g);
                float __fontHeight14 = _fontBold14.GetHeight(g);
                float __fontHeightNormal14 = _fontNormal14.GetHeight(g);
                if (__imageLogo != null)
                {
                    g.DrawImage(__imageLogo, __pageLeft, __pageTop, 110, 100);
                }
                RectangleF rectTitle = new RectangleF(__pageLeft + 120, __y, __xWidth, __fontHeight14);
                g.DrawString(__compayName, _fontBold18, Brushes.Black, rectTitle, sfLeft);
                __lineY += __fontHeight14 - 5;
                RectangleF rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __xWidth, __fontHeightNormal14);
                float __xPos = __pageLeft + 120;
                float __companyWidth = ((__xWidth / 2) + 100) - 120;
                //  float __companyWidth = ((__xWidth / 2) + 100);
                Point __dataPosition = new Point((int)__xPos, (int)__lineY);
                SizeF __strArName = g.MeasureString(__companyAddress, _fontNormal14);
                if (__strArName.Width > __companyWidth)
                {
                    ArrayList __getText = _cutString(g, __companyAddress, _fontNormal14, __companyWidth);
                    int __yPos = __dataPosition.Y;
                    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    {
                        g.DrawString(__getText[__loop].ToString(), _fontNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        SizeF __stringTextx = g.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                        __yPos += (int)(__stringTextx.Height);
                        __lineY = __yPos;
                    }
                }
                else
                {
                    rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                    g.DrawString(__companyAddress, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                    __lineY += _fontNormal12.GetHeight(g);
                }

                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyTel, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);
                rectTitleDetail = new RectangleF(__pageLeft + 200, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString(__companyFax, _fontNormal14, Brushes.Black, rectTitleDetail, sfCenter);
                __lineY += _fontNormal12.GetHeight(g);
                rectTitleDetail = new RectangleF(__pageLeft + 120, __lineY, __companyWidth, __fontHeightNormal14);
                g.DrawString("เลขประจำตัวผู้เสียภาษี : " + __companyTax, _fontNormal14, Brushes.Black, rectTitleDetail, sfLeft);


                RectangleF rect = new RectangleF(__xWidth - 200, __y, 200, lineHeight);
                g.DrawString("ใบจ่ายชำระหนี้", _fontBold20, Brushes.Black, rect, sfCenter);
                if (_pageNum > 1)
                {
                    __y += _fontBold18.GetHeight(g);
                    rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                    if (_printCount > 0)
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString() + "  พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                    else
                    {
                        g.DrawString("Pages : " + _pageNo.ToString() + "/" + _pageNum.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                else
                {
                    if (_printCount > 0)
                    {
                        __y += _fontBold18.GetHeight(g);
                        rect = new RectangleF(__pageWidth - 230, __y, 200, lineHeight);
                        g.DrawString("พิมพ์ซ้ำครั้งที่ " + _printCount.ToString(), _fontNormal14, Brushes.Black, rect, sfCenter);
                    }
                }
                //__y += _fontBold18.GetHeight(g) - 10;

                //__y += _fontBold18.GetHeight(g);
                float __cusLeft = __pageLeft + 20;
                float __custTop = __pageTop + 130;
                float __detailLeft = __pageLeft + ((__xWidth / 2) + 55) + 10;
                float __detailTop = __pageTop + 130;
                float __detailWidth = __xWidth - (((__xWidth / 2) + 55) / 2);

                float __titleLeft = __pageLeft;
                float __titleWidth = (__xWidth / 2) + 100;

                //float __
                rect = new RectangleF(__cusLeft, __custTop, 100, lineHeight);
                g.DrawString("ชื่อลูกค้า : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, lineHeight);
                g.DrawString(__customerName, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, __detailWidth, __custTop);
                g.DrawString("ที่อยู่ : ", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerAddress, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerAddress2, _fontNormal14, Brushes.Black, rect, sfLeft);
                __custTop += _fontNormal14.GetHeight(g);
                rect = new RectangleF(__cusLeft, __custTop, 100, __custTop);
                g.DrawString("โทรศัพท์ :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 60, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerTel, _fontNormal14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 250, __custTop, 100, __custTop);
                g.DrawString("Fax :", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__cusLeft + 300, __custTop, __titleWidth, __custTop);
                g.DrawString(__customerFax, _fontNormal14, Brushes.Black, rect, sfLeft);

                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("วันที่/Date:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                if (__checkString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).Length > 0)
                {
                    string __setDate = MyLib._myGlobal._convertDateFromQuery(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                    g.DrawString(__setDate, _fontNormal14, Brushes.Black, rect, sfLeft);
                }
                __detailTop += _fontBold14.GetHeight(g);
                rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                g.DrawString("เลขที่เอกสาร/No:", _fontBold14, Brushes.Black, rect, sfLeft);
                rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_no].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                //__detailTop += _fontBold14.GetHeight(g);
                //rect = new RectangleF(__detailLeft, __detailTop, __detailWidth, __custTop);
                //g.DrawString("พนักงานขาย/Sale: ", _fontBold14, Brushes.Black, rect, sfLeft);
                //rect = new RectangleF(__detailLeft + 110, __detailTop, __detailWidth, __custTop);
                //g.DrawString(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._sale_code].ToString() + ":" + __dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._sale_name].ToString(), _fontNormal14, Brushes.Black, rect, sfLeft);

                float __titleTop = __pageTop + 255;
                float __titleTop2 = __pageTop + 280;
                float __xline1 = __pageLeft + ((__xWidth * 8) / 100);
                float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                float __xline7 = __xline6 + ((__xWidth * 10) / 100);

                //หัวตาราง                             
                float __xlineWidth1 = ((__xWidth * 8) / 100);
                rect = new RectangleF(__pageLeft, __titleTop, __xlineWidth1, __custTop);
                g.DrawString("ลำดับ", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth2 = ((__xWidth * 20) / 100);
                rect = new RectangleF(__xline1, __titleTop, __xlineWidth2, __custTop);
                g.DrawString("เลขที่เอกสาร", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth3 = ((__xWidth * 20) / 100);
                rect = new RectangleF(__xline2, __titleTop, __xlineWidth3, __custTop);
                g.DrawString("วันที่เอกสาร", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth4 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline3, __titleTop, __xlineWidth4, __custTop);
                g.DrawString("วันครบกำหนด", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth5 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline4, __titleTop, __xlineWidth5, __custTop);
                g.DrawString("มูลค่าสินค้า", _fontBold14, Brushes.Black, rect, sfCenter);//

                float __xlineWidth6 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline5, __titleTop, __xlineWidth6, __custTop);
                g.DrawString("ยอดคงเหลือ", _fontBold14, Brushes.Black, rect, sfCenter);//


                float __xlineWidth7 = ((__xWidth * 10) / 100);
                rect = new RectangleF(__xline6, __titleTop, __xlineWidth7, __custTop);
                g.DrawString("ยอดตัดจ่าย", _fontBold14, Brushes.Black, rect, sfCenter);//                

                float __xlineWidth8 = ((__xWidth * 12) / 100);
                rect = new RectangleF(__xline7, __titleTop, __xlineWidth8, __custTop);
                g.DrawString("ยอดคงค้าง", _fontBold14, Brushes.Black, rect, sfCenter);//  

                float __sumdetailRightLeft = __xline5 - 10;
                float __sumdetailRightWidth = ((__pageWidth * 14) / 100);

                float __detailHeight = __pageHeight - (__titleTop + 250);
                float __detailButtomTop = ((__detailHeight + 5) + __titleTop);
                float __detailButtomLeft = __cusLeft;
                float __detailButtomWidth = (__xWidth / 2) + 150; ;
                float __detailButtomHeight = (__pageHeight - __detailButtomTop) - 120;
                float __sumdetailTop = ((__detailHeight + 1) + __titleTop); ;
                float __sumdetailLeft = __detailButtomWidth + 10;
                float __sumdetailWidth = __xWidth - ((__xWidth / 2) + 150);
                float __buttomDetailTop = (__detailButtomTop + __detailButtomHeight) + 3;
                float __buttomDetailLeft = __xWidth - ((__xWidth / 2) + 130);
                float __buttomDetailWidth = 0;
                float __buttomDetailLeftTop = (__detailButtomTop + __detailButtomHeight) + 3;
                __buttomDetailWidth = __pageLeft + __pageWidth / 2;
                float __rightbuttomTop = __detailButtomTop;
                //หมายเหตุ             
                if (_printLine == true)
                {
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    string __remarkResult = __dsTop.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                    g.DrawString("หมายเหตุ:", _fontBold14, Brushes.Black, rect, sfLeft);
                    __detailButtomTop += _fontNormal12.GetHeight(g);
                    rect = new RectangleF(__detailButtomLeft, __detailButtomTop, __detailButtomWidth, __custTop);
                    g.DrawString(__remarkResult, _fontNormal14, Brushes.Black, rect, sfLeft);



                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("มูลค่าสินค้า", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __rightbuttomTop, __xlineWidth8 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_debt_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//รวมจำนวนเงิน

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("ส่วนลด", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __rightbuttomTop, __xlineWidth8 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_discount].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ส่วนลดการค่า

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("ยอดก่อนภาษี", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __rightbuttomTop, __xlineWidth8 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_before_vat].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//ราคาสินค้า/บริการ

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("ภาษีมูลค่าเพิ่ม   " + __checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._vat_rate].ToString()) + " %", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __rightbuttomTop, __xlineWidth8 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_vat_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight); //ภาษีมูลค่าเพิ่ม

                    __rightbuttomTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__sumdetailLeft, __rightbuttomTop, __sumdetailWidth, __custTop);
                    g.DrawString("รวมมูลค่าทั้งสิ้น", _fontBold13, Brushes.Black, rect, sfLeft);
                    rect = new RectangleF(__xline7, __rightbuttomTop, __xlineWidth8 - 5, __custTop);
                    g.DrawString(__checkNumstr(__dsTop.Tables[0].Rows[0][_g.d.ap_ar_trans._total_debt_value].ToString()), _fontNormal13, Brushes.Black, rect, sfRight);//จำนวนเงินรวมทั้งสิ้น


                    __buttomDetailTop += _fontNormal14.GetHeight(g) + 20;
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้อนุมัติ", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__pageLeft, __buttomDetailTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);

                    __buttomDetailLeft = __buttomDetailWidth;
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString(__quotationSaleCodeStr, _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeftTop += _fontNormal14.GetHeight(g) + 20;

                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString("...........................................................", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeftTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString("ผู้ออกเอกสาร", _fontNormal14, Brushes.Black, rect, sfCenter);
                    __buttomDetailLeftTop += _fontNormal14.GetHeight(g);
                    rect = new RectangleF(__buttomDetailLeft, __buttomDetailLeftTop, __buttomDetailWidth, __custTop);
                    g.DrawString("วันที่ _____/_____/______", _fontNormal14, Brushes.Black, rect, sfCenter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        void mDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int __topMargin = e.MarginBounds.Top + (this.__pageEnum.Equals("ขาย_ขายสินค้าและบริการ_ici") ? _g.g._companyProfile._printerMargin : 0);
            mTotalPages++;
            float __detailY = __topMargin + 325;
            float __detailX = e.MarginBounds.Left;
            float __fontHeight = _fontNormal12.GetHeight(e.Graphics);
            string __paperName = e.PageSettings.PaperSize.PaperName;
            //New Get
            float __getLeft = e.MarginBounds.Left;
            float __getTop = __topMargin;
            float __getWidth = e.MarginBounds.Width;
            float __getHeight = e.MarginBounds.Height + (this.__pageEnum.Equals("ขาย_ขายสินค้าและบริการ_ici") ? _g.g._companyProfile._printerMargin : 0);
            //
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
            e.PageSettings.PrinterResolution.X = 300;
            e.PageSettings.PrinterResolution.Y = 300;
            if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ.ToString()))
            {
                __detailX = __getWidth - __getLeft;
                float __xWidth = __getWidth - __getLeft;
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportReceipt(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 335;
                    float __chkY = 0;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {

                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 32) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                        float __xwidth1 = ((__xWidth * 22) / 100);
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __xwidth1, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);
                        float __xwidth2 = ((__xWidth * 32) / 100);
                        ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                        ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14);
                        ////if (__strItemName.Width > __xwidth2)
                        ////{
                        ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14, __xwidth2);
                        ////    int __yPos = __dataPosition.Y;
                        ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                        ////    {
                        ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal14);
                        ////        __yPos += (int)(__stringTextx.Height);
                        ////        __chkY = __yPos;
                        ////    }
                        ////}
                        ////else
                        ////{
                        rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);
                        ////}

                        float __xwidth3 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline2, __detailY, __xwidth3, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                        float __xwidth4 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __xwidth4, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                        float __xwidth5 = ((__xWidth * 10) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        if (__intRef != 1)
                        {
                            rectDetail = new RectangleF(__xline4, __detailY, __xwidth5, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfRight);
                            float __xwidth6 = ((__xWidth * 9) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __xwidth6, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfRight);
                            float __xwidth7 = ((__xWidth * 11) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __xwidth7, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfRight);
                        }
                        __detailY += _fontNormal12.GetHeight(e.Graphics);
                        ////if (__strItemName.Width > __xwidth2)
                        ////{
                        ////    __detailY += __chkY;

                        ////}
                        ////else
                        ////{
                        ////    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                        ////    __chkY = 0;
                        ////}
                    }
                }
                catch (Exception ex)
                {
                }
            }
            #region รายการสินค้า ขายสินค้าและบริการ
            if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ_ici.ToString()))
            {
                __detailX = __getWidth - __getLeft;
                float __xWidth = __getWidth - __getLeft;
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printDataReportReceiptICIForm(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 300;
                    float __detailHeight = __getHeight - (__detailY + 297);
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 32) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                        float __xwidth1 = ((__xWidth * 22) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        int __intItemType = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_type].ToString());
                        RectangleF rectDetail = new RectangleF(__getLeft + 5, __detailY, __xwidth1, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfLeft);
                        }
                        float __xwidth2 = ((__xWidth * 32) / 100);
                        ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                        ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal12);
                        if (__intRef != 1)
                        {
                            //if (__strItemName.Width > __xwidth2)
                            //{
                            //    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal12, __xwidth2);
                            //    int __yPos = __dataPosition.Y;
                            //    for (int __loop = 0; __loop < __getText.Count; __loop++)
                            //    {
                            //        e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                            //        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal12);
                            //        __yPos += (int)(__stringTextx.Height);
                            //        __chkY = __yPos;
                            //    }
                            //    if (__yPos > __detailHeight)
                            //    {
                            //        _rowLine++;
                            //    }
                            //}
                            //else
                            //{
                            rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfLeft);
                            /// }
                        }
                        else
                        {
                            if (__intItemType == 0)
                            {
                                rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                                e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfLeft);
                            }
                        }
                        float __xwidth3 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline2 - 10, __detailY, __xwidth3, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_code].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                        }
                        float __xwidth4 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __xwidth4, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                        }
                        float __xwidth5 = ((__xWidth * 10) / 100);

                        if (__intRef != 1)
                        {
                            rectDetail = new RectangleF(__xline4 + 10, __detailY, __xwidth5, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);

                            float __xwidth6 = ((__xWidth * 9) / 100);
                            rectDetail = new RectangleF(__xline5 + 20, __detailY, __xwidth6, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrDiscount(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfCenter);

                            float __xwidth7 = ((__xWidth * 11) / 100);
                            rectDetail = new RectangleF(__xline6 + 10, __detailY, __xwidth7, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);
                        }
                        if (__intRef != 1 || __intItemType == 0)
                        {
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    __detailY += __chkY;

                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    // _pageNo++;
                                }

                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            #endregion
            #region รายการสินค้า ใบเสนอราคา
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ใบเสนอราคา.ToString()))
            {
                __detailX = __getWidth - __getLeft;
                float __xWidth = __getWidth - __getLeft;
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportQuotation(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __detailY - 20;
                    float __detailTop = __getTop + 253;
                    float __detailHeight = __getHeight - (__detailTop + 250);
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 27) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 9) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 9) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 11) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 11) / 100);
                        float __xwidth1 = ((__xWidth * 22) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        int __intItemType = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_type].ToString());
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __xwidth1, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                        }
                        float __xwidth2 = ((__getWidth * 27) / 100);
                        ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                        ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14);
                        if (__intRef != 1)
                        {
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14, __xwidth2);
                            ////    int __yPos = __dataPosition.Y;
                            ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                            ////    {
                            ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                            ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal14);
                            ////        __yPos += (int)(__stringTextx.Height);
                            ////        __chkY = __yPos;
                            ////    }
                            ////    if (__yPos > __detailHeight)
                            ////    {
                            ////        _rowLine++;
                            ////    }
                            ////}
                            ////else
                            ////{
                            rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                            ////}
                        }
                        else
                        {
                            if (__intItemType == 0)
                            {
                                rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                                e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                            }
                        }

                        float __xwidth3 = ((__getWidth * 9) / 100);
                        rectDetail = new RectangleF(__xline2, __detailY, __xwidth3, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        }
                        float __xwidth4 = ((__getWidth * 9) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __xwidth4, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        }
                        float __xwidth5 = ((__getWidth * 11) / 100);

                        if (__intRef != 1)
                        {
                            rectDetail = new RectangleF(__xline4, __detailY, __xwidth5 - 3, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __xwidth6 = ((__getWidth * 11) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __xwidth6 - 3, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrDiscount(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __xwidth7 = ((__getWidth * 11) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __xwidth7 - 3, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                        }
                        if (__intRef != 1 || __intItemType == 0)
                        {
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    __detailY += __chkY;
                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }

                        // toe เช็คเป็นรายการสุดท้ายหรือเปล่า ถ้าเป็นรายการสุดท้ายก็ให้พิมพ์ footer ออกมาเลย
                        //if (i == __dsDetail.Tables[0].Rows.Count - 1)
                        //{
                        //    _printLine = true;
                        //    _rowLine = 1;
                        //    e.HasMorePages = false;
                        //    break;

                        //}
                    }
                }
                catch (Exception ex)
                {
                }
            }
            #endregion
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_สั่งจองสินค้าและสั่งซื้อสินค้า.ToString()))
            {
                __detailX = __getWidth - __getLeft;
                float __xWidth = __getWidth - __getLeft;
                __detailY = __detailY - 20;
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportReserve(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 28) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                        float __xwidth1 = ((__xWidth * 22) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        int __intItemType = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_type].ToString());
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __xwidth1, __fontHeight);

                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                        }
                        //float __xwidth2 = ((__xWidth * 28) / 100);
                        ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                        ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14);

                        ////    if (__strItemName.Width > __xwidth2)
                        ////    {
                        ////        ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14, __xwidth2);
                        ////        int __yPos = __dataPosition.Y;
                        ////        for (int __loop = 0; __loop < __getText.Count; __loop++)
                        ////        {
                        ////            e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        ////            SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal14);
                        ////            __yPos += (int)(__stringTextx.Height);
                        ////            __chkY = __yPos;
                        ////        }
                        ////    }
                        ////    else
                        ////    {                       
                        float __xwidth2 = ((__xWidth * 28) / 100);
                        if (__intRef != 1)
                        {
                            rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                            ////}
                        }
                        else
                        {
                            if (__intItemType == 0)
                            {
                                rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                                e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                            }
                        }
                        if (__intRef != 1)
                        {
                            float __xwidth3 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __xwidth3, __fontHeight);

                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                            float __xwidth4 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __xwidth4, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                            float __xwidth5 = ((__xWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline4, __detailY, __xwidth5 - 3, __fontHeight);
                            //__checkNumstrThird
                            //e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                            float __xwidth6 = ((__xWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __xwidth6 - 3, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                            float __xwidth7 = ((__xWidth * 14) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __xwidth7 - 3, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                        }
                        if (__intRef != 1 || __intItemType == 0)
                        {
                            __detailY += _fontNormal14.GetHeight(e.Graphics);
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    __detailY += __chkY;
                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontHeaderNormal14.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ใบสั่งขาย.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __detailY = __topMargin + 305;
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportSaleOrder(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    float __chkY = 0;
                    float __xWidth = __getWidth - __getLeft;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 8) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 22) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 32) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 8) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                        float __xwidth1 = ((__xWidth * 8) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        int __intItemType = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_type].ToString());
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __xwidth1, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString((_no + i).ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                            float __xwidth2 = ((__getWidth * 22) / 100);
                            rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);
                        }
                        float __xwidth3 = ((__getWidth * 32) / 100);
                        ////Point __dataPosition = new Point((int)__xline2, (int)__detailY);
                        ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14);
                        ////if (__strItemName.Width > __xwidth3)
                        ////{
                        ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14, __xwidth3);
                        ////    int __yPos = __dataPosition.Y;
                        ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                        ////    {
                        ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal14);
                        ////        __yPos += (int)(__stringTextx.Height);
                        ////        __chkY = __yPos;
                        ////    }
                        ////}
                        ////else
                        ////{
                        if (__intRef != 1)
                        {
                            rectDetail = new RectangleF(__xline2, __detailY, __xwidth3, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);
                        }
                        else
                        {
                            if (__intItemType == 0)
                            {
                                rectDetail = new RectangleF(__xline2, __detailY, __xwidth3, __fontHeight);
                                e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);
                            }
                        }
                        if (__intRef != 1)
                        {
                            ////}
                            float __xwidth4 = ((__getWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __xwidth4, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._wh_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);

                            float __xwidth5 = ((__getWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline4, __detailY, __xwidth5, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._shelf_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);

                            float __xwidth6 = ((__getWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __xwidth6, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                            float __xwidth7 = ((__getWidth * 12) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __xwidth7, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                        }
                        if (__intRef != 1 || __intItemType == 0)
                        {
                            __detailY += _fontNormal14.GetHeight(e.Graphics);
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    __detailY += __chkY;
                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontHeaderNormal14.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_เพิ่มหนี้.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportInvoiceAdd(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 370;

                    float __xWidth = __getWidth - __getLeft;
                    float __detailtitleWidth = ((__xWidth * 10) / 100);
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __detailtitleLeft = __detailX;
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        if (__intRef != 1)
                        {
                            RectangleF rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                            e.Graphics.DrawString((_no + i).ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                            __detailtitleLeft = __detailtitleLeft + ((__xWidth * 10) / 100);
                            __detailtitleWidth = ((__xWidth * 55) / 100);

                            ////Point __dataPosition = new Point((int)__detailtitleLeft, (int)__detailY);
                            ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString().Trim() + __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString().Trim(), _fontHeaderNormal14);
                            ////if (__strItemName.Width > __detailtitleWidth)
                            ////{
                            ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString().Trim() + __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString().Trim(), _fontHeaderNormal14, __detailtitleWidth);
                            ////    int __yPos = __dataPosition.Y;
                            ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                            ////    {
                            ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                            ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal14);
                            ////        __yPos += (int)(__stringTextx.Height);
                            ////        __chkY = __yPos;
                            ////    }
                            ////}
                            ////else
                            ////{
                            rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString() + "  " + __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);
                            ////}
                            __detailtitleLeft = __detailtitleLeft + ((__xWidth * 55) / 100);
                            __detailtitleWidth = ((__xWidth * 12) / 100);
                            rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                            string __qtyUnit = __checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString());// +" " + __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString();
                            e.Graphics.DrawString(__qtyUnit, _fontNormal14, Brushes.Black, rectDetail, sfCenter);

                            __detailtitleLeft = __detailtitleLeft + ((__xWidth * 12) / 100);
                            __detailtitleWidth = ((__xWidth * 12) / 100);
                            rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfRight);

                            __detailtitleLeft = __detailtitleLeft + ((__getWidth * 12) / 100);
                            __detailtitleWidth = ((__xWidth * 11) / 100);
                            rectDetail = new RectangleF(__detailtitleLeft, __detailY, __detailtitleWidth, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfRight);
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            ////if (__strItemName.Width > __detailtitleWidth)
                            ////{
                            ////    __detailY += __chkY;
                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าและค่าบริการ.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportPurchaseService(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 320;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 33) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 8) / 100);
                        float __xwidth1 = ((__xWidth * 22) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __xwidth1, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfLeft);
                            float __xwidth2 = ((__xWidth * 33) / 100);
                            ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                            ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12);
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12, __xwidth2);
                            ////    int __yPos = __dataPosition.Y;
                            ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                            ////    {
                            ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontNormal12, Brushes.Black, new Point(__dataPosition.X, __yPos));
                            ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                            ////        __yPos += (int)(__stringTextx.Height);
                            ////        __chkY = __yPos;
                            ////    }
                            ////}
                            ////else
                            ////{
                            rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfLeft);
                            ////}


                            float __xwidth3 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __xwidth3, __fontHeight);
                            string __qtyUnit = __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString();
                            e.Graphics.DrawString(__qtyUnit, _fontNormal12, Brushes.Black, rectDetail, sfCenter);


                            float __xwidth4 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __xwidth4, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfCenter);


                            float __xwidth5 = ((__xWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline4, __detailY, __xwidth5, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);


                            float __xwidth6 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __xwidth6, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrDiscount(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);


                            float __xwidth7 = ((__xWidth * 11) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __xwidth7, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    __detailY += __chkY;

                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}                          
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ใบสั่งซื้อ.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportPurchase(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 325;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 28) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 11) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 10) / 100);

                        float __xwidth1 = ((__xWidth * 22) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        if (__intRef != 1)
                        {
                            RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __xwidth1, __fontHeight);
                            //e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                            float __xwidth2 = ((__xWidth * 28) / 100);
                            ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                            ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontDetail13);
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontDetail13, __xwidth2);
                            ////    int __yPos = __dataPosition.Y;
                            ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                            ////    {
                            ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontDetail13, Brushes.Black, new Point(__dataPosition.X, __yPos));
                            ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontDetail13);
                            ////        __yPos += (int)(__stringTextx.Height);
                            ////        __chkY = __yPos;
                            ////    }
                            ////}
                            ////else
                            ////{
                            rectDetail = new RectangleF(__xline1, __detailY, __xwidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                            ////}

                            float __xwidth3 = ((__xWidth * 11) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __xwidth3, __fontHeight);
                            string __qtyName = __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString();
                            e.Graphics.DrawString(__qtyName, _fontNormal12, Brushes.Black, rectDetail, sfCenter);

                            float __xwidth4 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __xwidth4, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                            float __xwidth5 = ((__xWidth * 10) / 100) - 2;
                            rectDetail = new RectangleF(__xline4, __detailY, __xwidth5, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __xwidth6 = ((__xWidth * 10) / 100) - 2;
                            rectDetail = new RectangleF(__xline5, __detailY, __xwidth6, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrDiscount(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __xwidth7 = ((__xWidth * 11) / 100) - 2;
                            rectDetail = new RectangleF(__xline6, __detailY, __xwidth7, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                            __detailY += _fontNormal13.GetHeight(e.Graphics);
                            ////if (__strItemName.Width > __xwidth2)
                            ////{
                            ////    __detailY += __chkY;

                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontDetail13.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}

                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด.ToString()))
            {
                //__printDataReportPurchaseAdd
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportPurchaseAdd(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 305;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 25) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 8) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 6) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 9) / 100);
                        float __xline7 = __xline6 + ((__xWidth * 8) / 100);
                        float __xline8 = __xline7 + ((__xWidth * 6) / 100);
                        //float __detailtitleLeft = __detailX;
                        float __detailWidth1 = ((__xWidth * 22) / 100);
                        int __intRef = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._set_ref_qty].ToString());
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                        if (__intRef != 1)
                        {
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfLeft);
                            float __detailWidth2 = ((__xWidth * 25) / 100);
                            ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                            ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12);
                            ////if (__strItemName.Width > __detailWidth2)
                            ////{
                            ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12, __detailWidth2);
                            ////    int __yPos = __dataPosition.Y;
                            ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                            ////    {
                            ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontNormal12, Brushes.Black, new Point(__dataPosition.X, __yPos));
                            ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontNormal12);
                            ////        __yPos += (int)(__stringTextx.Height);
                            ////        __chkY = __yPos;
                            ////    }
                            ////}
                            ////else
                            ////{
                            rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfLeft);
                            ////}

                            float __detailWidth3 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._wh_code].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            float __detailWidth4 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._shelf_code].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            float __detailWidth5 = ((__xWidth * 6) / 100);
                            rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            float __detailWidth6 = ((__xWidth * 9) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_code].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            float __detailWidth7 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                            e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);
                            float __detailWidth8 = ((__xWidth * 6) / 100);
                            rectDetail = new RectangleF(__xline7, __detailY, __detailWidth8, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);
                            float __detailWidth9 = ((__xWidth * 8) / 100);
                            rectDetail = new RectangleF(__xline8, __detailY, __detailWidth9, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า.ToString()))
            {
                //__printDataReportDepositPayment
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportDepositPayment(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportDepositPaymentReturn(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_รับเงินล่วงหน้า.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportSODepositPayment(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_คืนเงินล่วงหน้า.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportSODepositPaymentReturn(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                }
                catch (Exception ex)
                {
                }
            }
            //ลูกหนี้_ใบวางบิน
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ลูกหนี้_ใบวางบิล.ToString()))
            {

                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportArPaybill(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 305;
                    float __xWidth = __getWidth - __getLeft;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 8) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 12) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 12) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 14) / 100);
                        int __checkBillStatus = __checkInt(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._bill_type].ToString());
                        if (__checkBillStatus != 2 && __checkBillStatus != 5)
                        {
                            float __detailWidth1 = ((__xWidth * 8) / 100);
                            RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                            e.Graphics.DrawString((_no + i).ToString(), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            float __detailWidth2 = ((__xWidth * 20) / 100);
                            rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._doc_no].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            float __detailWidth3 = ((__xWidth * 20) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_no].ToString(), _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            float __detailWidth4 = ((__xWidth * 12) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                            if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).Length > 0)
                            {
                                string __setBillDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                e.Graphics.DrawString(__setBillDate, _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            }
                            float __detailWidth5 = ((__xWidth * 12) / 100);
                            rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                            if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).Length > 0)
                            {
                                string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                e.Graphics.DrawString(__setDueDate, _fontNormal12, Brushes.Black, rectDetail, sfCenter);
                            }
                            float __detailWidth6 = ((__xWidth * 14) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_value].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);
                            float __detailWidth7 = ((__xWidth * 14) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                            /*string __sumValue = __dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_value].ToString();
                            e.Graphics.DrawString(__checkNumstr(__sumValue), _fontNormal12, Brushes.Black, rectDetail, sfRight);                        */
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_debt_value].ToString()), _fontNormal12, Brushes.Black, rectDetail, sfRight);
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ลูกหนี้_รับชำระหนี้.ToString()))
            {

                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportArDebtBilling(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 310;
                    float __xWidth = __getWidth - __getLeft;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        string __checkPayMoney = __checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_pay_money].ToString());
                        if (__checkPayMoney.Length > 0)
                        {
                            float __xline1 = __getLeft + ((__xWidth * 8) / 100);
                            float __xline2 = __xline1 + ((__xWidth * 25) / 100);
                            float __xline3 = __xline2 + ((__xWidth * 12) / 100);
                            float __xline4 = __xline3 + ((__xWidth * 12) / 100);
                            float __xline5 = __xline4 + ((__xWidth * 14) / 100);
                            float __xline6 = __xline5 + ((__xWidth * 14) / 100);
                            float __detailWidth1 = ((__xWidth * 8) / 100);
                            RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                            e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                            float __detailWidth2 = ((__xWidth * 25) / 100);
                            rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_no].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);

                            float __detailWidth3 = ((__xWidth * 12) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                            if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).Length > 0)
                            {
                                try
                                {
                                    string __setBillDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                    e.Graphics.DrawString(__setBillDate, _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            float __detailWidth4 = ((__xWidth * 12) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                            if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).Length > 0)
                            {
                                try
                                {
                                    string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                    e.Graphics.DrawString(__setDueDate, _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                                }
                                catch (Exception ex)
                                {
                                }
                            }

                            float __detailWidth5 = ((__xWidth * 14) / 100);
                            rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_debt_value].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __detailWidth6 = ((__xWidth * 14) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_debt_balance].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __detailWidth7 = ((__xWidth * 15) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                            e.Graphics.DrawString(__checkPayMoney, _fontNormal13, Brushes.Black, rectDetail, sfRight);
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบรับวางบิล.ToString()))
            {

                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportAPPaybill(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 310;
                    float __xWidth = __getWidth - __getLeft;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        string __checkPayMoney = __checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_debt_value].ToString());
                        //if (__checkPayMoney.Length > 0)
                        //{
                        float __xline1 = __getLeft + ((__xWidth * 8) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 18) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 18) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 16) / 100);
                        float __detailWidth1 = ((__xWidth * 8) / 100);
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                        e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth2 = ((__xWidth * 20) / 100);
                        rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_no].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth3 = ((__xWidth * 20) / 100);
                        rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                        if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).Length > 0)
                        {
                            try
                            {
                                string __setBillDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                e.Graphics.DrawString(__setBillDate, _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        float __detailWidth4 = ((__xWidth * 18) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                        if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).Length > 0)
                        {
                            try
                            {
                                string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                e.Graphics.DrawString(__setDueDate, _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        float __detailWidth5 = ((__xWidth * 18) / 100);
                        rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._balance_ref].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                        float __detailWidth6 = ((__xWidth * 16) / 100);
                        rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_pay_money].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                        __detailY += _fontNormal12.GetHeight(e.Graphics);
                        if (_rowLine == _numperLine)
                        {
                            if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                            {
                                //if (_pageNo < _pageNum)
                                //{
                                //    _printLine = false;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                //else
                                //{
                                //    _printLine = true;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                if (_pageNo < _pageNum)
                                {
                                    _printLine = false;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo == _pageNum)
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo > _pageNum)
                                {
                                    e.HasMorePages = false;
                                    break;
                                }
                                else
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            this._rowLine++;
                        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบจ่ายชำระหนี้.ToString()))
            {

                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportAPDebtPaybill(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 310;
                    float __xWidth = __getWidth - __getLeft;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        //string __checkPayMoney = __checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_debt_value].ToString());
                        string __checkPayMoney = __checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_pay_money].ToString());
                        if (__checkPayMoney.Length > 0)
                        {
                            float __xline1 = __getLeft + ((__xWidth * 8) / 100);
                            float __xline2 = __xline1 + ((__xWidth * 20) / 100);
                            float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                            float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                            float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                            float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                            float __xline7 = __xline6 + ((__xWidth * 10) / 100);
                            float __detailWidth1 = ((__xWidth * 8) / 100);
                            RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                            e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                            float __detailWidth2 = ((__xWidth * 20) / 100);
                            rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_no].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                            float __detailWidth3 = ((__xWidth * 20) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                            if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).Length > 0)
                            {
                                try
                                {
                                    string __setBillDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._billing_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                    e.Graphics.DrawString(__setBillDate, _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            float __detailWidth4 = ((__xWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                            if (__checkString(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).Length > 0)
                            {
                                try
                                {
                                    string __setDueDate = MyLib._myGlobal._convertDateFromQuery(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._due_date].ToString()).ToString("dd/MM/yyyy", MyLib._myGlobal._cultureInfo());
                                    e.Graphics.DrawString(__setDueDate, _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                                }
                                catch (Exception ex)
                                {
                                }
                            }

                            float __detailWidth5 = ((__xWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_debt_value].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __detailWidth6 = ((__xWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._balance_ref].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __detailWidth7 = ((__xWidth * 10) / 100);
                            rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_pay_money].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            float __detailWidth8 = ((__xWidth * 12) / 100);
                            rectDetail = new RectangleF(__xline7, __detailY, __detailWidth8, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ap_ar_trans_detail._sum_debt_balance].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            if (_rowLine == _numperLine)
                            {
                                if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                                {
                                    //if (_pageNo < _pageNum)
                                    //{
                                    //    _printLine = false;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    //else
                                    //{
                                    //    _printLine = true;
                                    //    _rowLine = 1;
                                    //    e.HasMorePages = true;
                                    //    break;
                                    //}
                                    if (_pageNo < _pageNum)
                                    {
                                        _printLine = false;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo == _pageNum)
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = true;
                                        break;
                                    }
                                    else if (_pageNo > _pageNum)
                                    {
                                        e.HasMorePages = false;
                                        break;
                                    }
                                    else
                                    {
                                        _printLine = true;
                                        _rowLine = 1;
                                        e.HasMorePages = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                this._rowLine++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------------------------- : ซื้อ : ส่งคืนสินค้า/ลดหนี้
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด.ToString()))
            {

                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportPOCreditnote(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 310;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 5) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 26) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 25) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 8) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 8) / 100);
                        float __xline7 = __xline6 + ((__xWidth * 8) / 100);

                        float __detailWidth1 = ((__xWidth * 5) / 100);
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                        e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth2 = ((__xWidth * 26) / 100);
                        rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth3 = ((__xWidth * 25) / 100);
                        //Point __dataPosition = new Point((int)__xline2, (int)__detailY);
                        //SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13);
                        //if (__strItemName.Width > __detailWidth3)
                        //{
                        //    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, __detailWidth3);
                        //    int __yPos = __dataPosition.Y;
                        //    for (int __loop = 0; __loop < __getText.Count; __loop++)
                        //    {
                        //        e.Graphics.DrawString(__getText[__loop].ToString(), _fontNormal13, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        //        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontNormal13);
                        //        __yPos += (int)(__stringTextx.Height);
                        //        __chkY = __yPos;
                        //    }
                        //}
                        //else
                        //{
                        rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                        // }

                        float __detailWidth4 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfCenter);


                        float __detailWidth5 = ((__xWidth * 10) / 100);
                        rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth6 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                        e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                        float __detailWidth7 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                        e.Graphics.DrawString(__checkNumstrDiscount(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                        float __detailWidth8 = ((__xWidth * 11) / 100);
                        rectDetail = new RectangleF(__xline7, __detailY, __detailWidth8, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                        __detailY += _fontNormal12.GetHeight(e.Graphics);
                        if (_rowLine == _numperLine)
                        {
                            if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                            {
                                //if (_pageNo < _pageNum)
                                //{
                                //    _printLine = false;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                //else
                                //{
                                //    _printLine = true;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                if (_pageNo < _pageNum)
                                {
                                    _printLine = false;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo == _pageNum)
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo > _pageNum)
                                {
                                    e.HasMorePages = false;
                                    break;
                                }
                                else
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            this._rowLine++;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------------------------- : สินค้า : รับสินค้าสำเร็จรูป
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับสินค้าสำเร็จรูป.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportICFinishgoods(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 305;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        this._rowLine++;
                        float __xline1 = __getLeft + ((__xWidth * 6) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 22) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 28) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 12) / 100);
                        float __detailWidth1 = ((__xWidth * 6) / 100);
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                        e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth2 = ((__xWidth * 22) / 100);
                        rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);

                        float __detailWidth3 = ((__xWidth * 28) / 100);
                        ////Point __dataPosition = new Point((int)__xline2, (int)__detailY);
                        ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontDetail13);
                        ////if (__strItemName.Width > __detailWidth3)
                        ////{
                        ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontDetail13, __detailWidth3);
                        ////    int __yPos = __dataPosition.Y;
                        ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                        ////    {
                        ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontDetail13, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontDetail13);
                        ////        __yPos += (int)(__stringTextx.Height);
                        ////        __chkY = __yPos;
                        ////    }
                        ////}
                        ////else
                        ////{
                        rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                        ////}

                        float __detailWidth4 = ((__xWidth * 10) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth5 = ((__xWidth * 10) / 100);
                        rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth6 = ((__xWidth * 12) / 100);
                        rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                        e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                        float __detailWidth7 = ((__xWidth * 12) / 100);
                        rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                        __detailY += _fontNormal12.GetHeight(e.Graphics);
                        ////if (__strItemName.Width > __detailWidth3)
                        ////{
                        ////    __detailY = __chkY;

                        ////}
                        ////else
                        ////{
                        ////    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                        ////    __chkY = 0;
                        ////}
                        if (_rowLine == _numperLine)
                        {
                            if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                            {
                                //if (_pageNo < _pageNum)
                                //{
                                //    _printLine = false;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                //else
                                //{
                                //    _printLine = true;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                if (_pageNo < _pageNum)
                                {
                                    _printLine = false;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo == _pageNum)
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo > _pageNum)
                                {
                                    e.HasMorePages = false;
                                    break;
                                }
                                else
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------------------------- : สินค้า : เบิกสินค้า/วัตถุดิบ
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_เบิกสินค้าวัตถุดิบ.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportICStockRequest(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 290;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        float __xline1 = __getLeft + ((__xWidth * 25) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 35) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                        float __detailWidth1 = ((__xWidth * 25) / 100);
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);

                        float __detailWidth2 = ((__xWidth * 35) / 100);
                        ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                        ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14);
                        ////if (__strItemName.Width > __detailWidth2)
                        ////{
                        ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14, __detailWidth2);
                        ////    int __yPos = __dataPosition.Y;
                        ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                        ////    {
                        ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal14);
                        ////        __yPos += (int)(__stringTextx.Height);
                        ////        __chkY = __yPos;
                        ////    }
                        ////}
                        ////else
                        ////{
                        rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);

                        ////}
                        float __detailWidth3 = ((__xWidth * 20) / 100);
                        rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth4 = ((__xWidth * 20) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                        __detailY += _fontNormal12.GetHeight(e.Graphics);
                        //if (__strItemName.Width > __detailWidth2)
                        //{
                        //    __detailY += __chkY;

                        //}
                        //else
                        //{
                        //    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                        //    __chkY = 0;
                        //}
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------------------------- : สินค้า : รับคืนสินค้า /วัตถุดิบจากการเบิก
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับคืนสินค้าจากการเบิก.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    if (__dsDetail == null)
                    {
                        __getDataSet(__pageEnum);
                    }
                    try
                    {
                        __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                        __printDataReportICStockRequestReturn(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                        __detailY = __topMargin + 290;
                        float __xWidth = __getWidth - __getLeft;
                        float __chkY = 0;
                        for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                        {
                            float __xline1 = __getLeft + ((__xWidth * 25) / 100);
                            float __xline2 = __xline1 + ((__xWidth * 35) / 100);
                            float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                            float __detailWidth1 = ((__xWidth * 25) / 100);
                            RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);

                            float __detailWidth2 = ((__xWidth * 35) / 100);
                            ////Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                            ////SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14);
                            ////if (__strItemName.Width > __detailWidth2)
                            ////{
                            ////    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontHeaderNormal14, __detailWidth2);
                            ////    int __yPos = __dataPosition.Y;
                            ////    for (int __loop = 0; __loop < __getText.Count; __loop++)
                            ////    {
                            ////        e.Graphics.DrawString(__getText[__loop].ToString(), _fontHeaderNormal14, Brushes.Black, new Point(__dataPosition.X, __yPos));
                            ////        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontHeaderNormal14);
                            ////        __yPos += (int)(__stringTextx.Height);
                            ////        __chkY = __yPos;
                            ////    }
                            ////}
                            ////else
                            ////{
                            rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfLeft);

                            //// }

                            float __detailWidth3 = ((__xWidth * 20) / 100);
                            rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                            e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal14, Brushes.Black, rectDetail, sfCenter);

                            float __detailWidth4 = ((__xWidth * 20) / 100);
                            rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                            e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal14, Brushes.Black, rectDetail, sfCenter);
                            __detailY += _fontNormal12.GetHeight(e.Graphics);
                            ////if (__strItemName.Width > __detailWidth2)
                            ////{
                            ////    __detailY += __chkY;

                            ////}
                            ////else
                            ////{
                            ////    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                            ////    __chkY = 0;
                            ////}
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------------------------- : สินค้า : รับโอนสินค้า
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_โอนออก.ToString()))
            {
                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportICStockTransfer(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 280;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        this._rowLine++;
                        float __xline1 = __getLeft + ((__xWidth * 22) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 23) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 10) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 10) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 10) / 100);
                        float __xline6 = __xline5 + ((__xWidth * 10) / 100);
                        float __xline7 = __xline6 + ((__xWidth * 8) / 100);
                        float __detailWidth1 = ((__xWidth * 22) / 100);
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                        float __detailWidth2 = ((__xWidth * 23) / 100);
                        //Point __dataPosition = new Point((int)__xline1, (int)__detailY);
                        //SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontDetail13);
                        //if (__strItemName.Width > __detailWidth2)
                        //{
                        //    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontDetail13, __detailWidth2);
                        //    int __yPos = __dataPosition.Y;
                        //    for (int __loop = 0; __loop < __getText.Count; __loop++)
                        //    {
                        //        e.Graphics.DrawString(__getText[__loop].ToString(), _fontDetail13, Brushes.Black, new Point(__dataPosition.X, __yPos));
                        //        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontDetail13);
                        //        __yPos += (int)(__stringTextx.Height);
                        //        __chkY = __yPos;
                        //    }
                        //    this._rowLine += __getText.Count - 1;
                        //}
                        //else
                        //{
                        rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                        // }
                        float __detailWidth3 = ((__xWidth * 10) / 100);
                        rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._wh_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        float __detailWidth4 = ((__xWidth * 10) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._shelf_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        float __detailWidth5 = ((__xWidth * 10) / 100);
                        rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._wh_code_2].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        float __detailWidth6 = ((__xWidth * 10) / 100);
                        rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._shelf_code_2].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        float __detailWidth7 = ((__xWidth * 8) / 100);
                        rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        float __detailWidth8 = ((__xWidth * 7) / 100);
                        rectDetail = new RectangleF(__xline7, __detailY, __detailWidth8, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfCenter);
                        //if (__strItemName.Width > __detailWidth2)
                        //{
                        //    __detailY = __chkY;

                        //}
                        //else
                        //{
                        //    __detailY += _fontHeaderNormal12.GetHeight(e.Graphics);
                        //    __chkY = 0;
                        //}
                        __detailY += _fontNormal12.GetHeight(e.Graphics);
                        if (_rowLine == _numperLine)
                        {
                            if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                            {
                                //if (_pageNo < _pageNum)
                                //{
                                //    _printLine = false;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                //else
                                //{
                                //    _printLine = true;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                if (_pageNo < _pageNum)
                                {
                                    _printLine = false;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo == _pageNum)
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo > _pageNum)
                                {
                                    e.HasMorePages = false;
                                    break;
                                }
                                else
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------------------------- : ขาย : ส่งคืนสินค้า/ลดหนี้
            else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ลดหนี้.ToString()))
            {

                if (__dsDetail == null)
                {
                    __getDataSet(__pageEnum);
                }
                try
                {
                    ////////__printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    ////////__printDataReportSOCreditnote(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    //////__printDataReportSOCreditnoteII(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    //////__detailY = __topMargin + 310;
                    //////float __xWidth = __getWidth - __getLeft;
                    //////float __chkY = 0;
                    //////_pageNo++;
                    //////for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    //////{
                    //////    this._rowCount++;
                    //////    float __xline1 = __getLeft + ((__xWidth * 5) / 100);
                    //////    float __xline2 = __xline1 + ((__xWidth * 10) / 100);
                    //////    float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                    //////    float __xline4 = __xline3 + ((__xWidth * 23) / 100);
                    //////    float __xline5 = __xline4 + ((__xWidth * 8) / 100);
                    //////    float __xline6 = __xline5 + ((__xWidth * 8) / 100);
                    //////    float __xline7 = __xline6 + ((__xWidth * 8) / 100);
                    //////    float __xline8 = __xline7 + ((__xWidth * 8) / 100);

                    //////    float __detailWidth1 = ((__xWidth * 5) / 100);
                    //////    RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                    //////    e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                    //////    float __detailWidth2 = ((__xWidth * 10) / 100);
                    //////    rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                    //////    e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._ref_doc_no].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);

                    //////    float __detailWidth3 = ((__xWidth * 20) / 100);
                    //////    rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                    //////    e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_code].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);

                    //////    float __detailWidth4 = ((__xWidth * 23) / 100);
                    //////    //Point __dataPosition = new Point((int)__xline2, (int)__detailY);
                    //////    //SizeF __strItemName = e.Graphics.MeasureString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13);
                    //////    //if (__strItemName.Width > __detailWidth3)
                    //////    //{
                    //////    //    ArrayList __getText = _cutString(e.Graphics, __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, __detailWidth3);
                    //////    //    int __yPos = __dataPosition.Y;
                    //////    //    for (int __loop = 0; __loop < __getText.Count; __loop++)
                    //////    //    {
                    //////    //        e.Graphics.DrawString(__getText[__loop].ToString(), _fontNormal13, Brushes.Black, new Point(__dataPosition.X, __yPos));
                    //////    //        SizeF __stringTextx = e.Graphics.MeasureString(__getText[__loop].ToString(), _fontNormal13);
                    //////    //        __yPos += (int)(__stringTextx.Height);
                    //////    //        __chkY = __yPos;
                    //////    //    }
                    //////    //}
                    //////    //else
                    //////    //{
                    //////    rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                    //////    e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);
                    //////    // }

                    //////    float __detailWidth5 = ((__xWidth * 8) / 100);
                    //////    rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                    //////    e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfCenter);


                    //////    float __detailWidth6 = ((__xWidth * 8) / 100);
                    //////    rectDetail = new RectangleF(__xline5, __detailY, __detailWidth6, __fontHeight);
                    //////    e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                    //////    float __detailWidth7 = ((__xWidth * 8) / 100);
                    //////    rectDetail = new RectangleF(__xline6, __detailY, __detailWidth7, __fontHeight);
                    //////    e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                    //////    float __detailWidth8 = ((__xWidth * 8) / 100);
                    //////    rectDetail = new RectangleF(__xline7, __detailY, __detailWidth8, __fontHeight);
                    //////    e.Graphics.DrawString(__checkNumstrDiscount(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._discount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                    //////    float __detailWidth9 = ((__xWidth * 10) / 100);
                    //////    rectDetail = new RectangleF(__xline8, __detailY, __detailWidth9, __fontHeight);
                    //////    e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                    //////    __detailY += _fontNormal12.GetHeight(e.Graphics);
                    //////    if (_rowLine == _numperLine)
                    //////    {
                    //////        if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                    //////        {
                    //////            //if (_pageNo < _pageNum)
                    //////            //{
                    //////            //    _printLine = false;
                    //////            //    _rowLine = 1;
                    //////            //    e.HasMorePages = true;
                    //////            //    break;
                    //////            //}
                    //////            //else
                    //////            //{
                    //////            //    _printLine = true;
                    //////            //    _rowLine = 1;
                    //////            //    e.HasMorePages = true;
                    //////            //    break;
                    //////            //}
                    //////            if (_pageNo < _pageNum)
                    //////            {
                    //////                _printLine = false;
                    //////                _rowLine = 1;
                    //////                e.HasMorePages = true;
                    //////                break;
                    //////            }
                    //////            else if (_pageNo == _pageNum)
                    //////            {
                    //////                _printLine = true;
                    //////                _rowLine = 1;
                    //////                e.HasMorePages = true;
                    //////                break;
                    //////            }
                    //////            else if (_pageNo > _pageNum)
                    //////            {
                    //////                e.HasMorePages = false;
                    //////                break;
                    //////            }
                    //////            else
                    //////            {
                    //////                _printLine = true;
                    //////                _rowLine = 1;
                    //////                e.HasMorePages = false;
                    //////                break;
                    //////            }
                    //////        }
                    //////    }
                    //////    else
                    //////    {
                    //////        this._rowLine++;
                    //////    }
                    //////}
                    __printHeadBox(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    //__printDataReportSOCreditnote(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __printDataReportSOCreditnoteII(e.Graphics, __getLeft, __getTop, __getWidth, __getHeight);
                    __detailY = __topMargin + 345;
                    float __xWidth = __getWidth - __getLeft;
                    float __chkY = 0;
                    _pageNo++;
                    for (int i = _rowCount; i < __dsDetail.Tables[0].Rows.Count; i++)
                    {
                        this._rowCount++;
                        float __xline1 = __getLeft + ((__xWidth * 10) / 100);
                        float __xline2 = __xline1 + ((__xWidth * 40) / 100);
                        float __xline3 = __xline2 + ((__xWidth * 20) / 100);
                        float __xline4 = __xline3 + ((__xWidth * 15) / 100);
                        float __xline5 = __xline4 + ((__xWidth * 15) / 100);

                        float __detailWidth1 = ((__xWidth * 10) / 100);
                        RectangleF rectDetail = new RectangleF(__getLeft, __detailY, __detailWidth1, __fontHeight);
                        e.Graphics.DrawString((_no + i).ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth2 = ((__xWidth * 40) / 100);
                        rectDetail = new RectangleF(__xline1, __detailY, __detailWidth2, __fontHeight);
                        e.Graphics.DrawString(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._item_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfLeft);

                        float __detailWidth3 = ((__xWidth * 20) / 100);
                        rectDetail = new RectangleF(__xline2, __detailY, __detailWidth3, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._qty].ToString()) + " " + __dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._unit_name].ToString(), _fontNormal13, Brushes.Black, rectDetail, sfCenter);

                        float __detailWidth4 = ((__xWidth * 15) / 100);
                        rectDetail = new RectangleF(__xline3, __detailY, __detailWidth4, __fontHeight);
                        e.Graphics.DrawString(__checkNumstrThird(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._price].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);
                        // }

                        float __detailWidth5 = ((__xWidth * 15) / 100);
                        rectDetail = new RectangleF(__xline4, __detailY, __detailWidth5, __fontHeight);
                        e.Graphics.DrawString(__checkNumstr(__dsDetail.Tables[0].Rows[i][_g.d.ic_trans_detail._sum_amount].ToString()), _fontNormal13, Brushes.Black, rectDetail, sfRight);

                        __detailY += _fontNormal12.GetHeight(e.Graphics);
                        if (_rowLine == _numperLine)
                        {
                            if (_rowCount < __dsDetail.Tables[0].Rows.Count)
                            {
                                //if (_pageNo < _pageNum)
                                //{
                                //    _printLine = false;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                //else
                                //{
                                //    _printLine = true;
                                //    _rowLine = 1;
                                //    e.HasMorePages = true;
                                //    break;
                                //}
                                if (_pageNo < _pageNum)
                                {
                                    _printLine = false;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo == _pageNum)
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = true;
                                    break;
                                }
                                else if (_pageNo > _pageNum)
                                {
                                    e.HasMorePages = false;
                                    break;
                                }
                                else
                                {
                                    _printLine = true;
                                    _rowLine = 1;
                                    e.HasMorePages = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            this._rowLine++;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion


        #region TOOLBAR EVENT HANDLERS


        private void tsTxtCurrentPage_Leave(object sender, EventArgs e)
        {
            int startPage;
            if (Int32.TryParse(tsTxtCurrentPage.Text, out startPage))
            {
                try
                {
                    startPage--;
                    if (startPage < 0) startPage = 0;
                    if (startPage > mTotalPages - 1) startPage = mTotalPages - mVisibilePages;
                    printPreviewControl1.StartPage = startPage;
                }
                catch { }
            }
        }

        private void NumOfPages_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mnuitem = (ToolStripMenuItem)sender;
            tsDDownPages.Image = mnuitem.Image;
            //mVisibilePages = MyLib._myGlobal._intPhase((string)mnuitem.Tag);
            mVisibilePages = MyLib._myGlobal._intPhase((string)mnuitem.Tag);
            switch (mVisibilePages)
            {
                case 1:
                    printPreviewControl1.Rows = 1;
                    printPreviewControl1.Columns = 1;
                    break;
                case 2:
                    printPreviewControl1.Rows = 1;
                    printPreviewControl1.Columns = 2;
                    break;
                case 4:
                    printPreviewControl1.Rows = 2;
                    printPreviewControl1.Columns = 2;
                    break;
                case 6:
                    printPreviewControl1.Rows = 2;
                    printPreviewControl1.Columns = 3;
                    break;
                case 8:
                    printPreviewControl1.Rows = 2;
                    printPreviewControl1.Columns = 4;
                    break;
            }
            tsBtnZoom_Click(null, null);
        }

        // Manages the next and previous button 
        private void Navigate_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            int startPage = printPreviewControl1.StartPage;
            try
            {
                if (btn.Name == "tsBtnNext")
                {
                    startPage += mVisibilePages;
                }
                else
                {
                    startPage -= mVisibilePages;
                }
                if (startPage < 0) startPage = 0;
                if (startPage > mTotalPages - 1) startPage = mTotalPages - mVisibilePages;
                printPreviewControl1.StartPage = startPage;

            }
            catch { }
        }

        void printPreviewControl1_StartPageChanged(object sender, EventArgs e)
        {
            int tmp = printPreviewControl1.StartPage + 1;
            tsTxtCurrentPage.Text = tmp.ToString();
        }

        private void tsComboZoom_Leave(object sender, EventArgs e)
        {
            if (tsComboZoom.SelectedIndex == 0)
            {
                printPreviewControl1.AutoZoom = true;
                return;
            }
            string sZoomVal = tsComboZoom.Text.Replace("%", "");
            double zoomval;
            if (double.TryParse(sZoomVal, out zoomval))
            {
                try
                {
                    printPreviewControl1.Zoom = zoomval / 100;
                }
                catch { }
                zoomval = (printPreviewControl1.Zoom * 100);
                tsComboZoom.Text = zoomval.ToString() + "%";
            }
        }

        private void tsBtnZoom_Click(object sender, EventArgs e)
        {
            tsComboZoom.SelectedIndex = 0;
            tsComboZoom_Leave(null, null);
        }

        private void tsComboZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tsComboZoom_Leave(null, null);
            }
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            if (_printerSelect)
            {

                using (var dlg = new PrintDialog())
                {
                    // configure dialog
                    dlg.AllowSomePages = false;
                    dlg.AllowSelection = false;
                    dlg.UseEXDialog = true;
                    dlg.Document = this.mDocument;

                    // show allowed page range
                    //var ps = dlg.PrinterSettings;
                    //ps.MinimumPage = ps.FromPage = 1;
                    //ps.MaximumPage = ps.ToPage = 

                    // show dialog
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        // print selected page range
                        _printCheck = true;
                        mDocument.Print();
                        __chekPrintData(__transFlag, __docnoResult, __docdateResult, 1);
                    }
                }
            }
            else
            {
                try
                {
                    _printCheck = true;
                    mDocument.Print();
                    __chekPrintData(__transFlag, __docnoResult, __docdateResult, 1);
                }
                catch (Exception ex)
                {
                }
            }
            //////PrintDialog.Document = this.mDocument;
            //////if (mShowPrinterSettingsBeforePrint)
            //////{
            //////    if (PrintDialog.ShowDialog() == DialogResult.OK)
            //////    {
            //////        try
            //////        {
            //////            _printCheck = true;
            //////            mDocument.Print();

            //////            __chekPrintData(__transFlag, __docnoResult, __docdateResult, 1);
            //////        }
            //////        catch { }
            //////    }
            //////}
            //else
            //{
            //    try
            //    {
            //        mDocument.Print();
            //    }
            //    catch { }
            //}
            //try
            //{                
            //    _printCheck = true;
            //    mDocument.Print();
            //    __chekPrintData(__transFlag, __docnoResult, __docdateResult, 1);
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void tsBtnPageSettings_Click(object sender, EventArgs e)
        {
            PageSetupDialog.Document = this.mDocument;
            if (PageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printPreviewControl1.InvalidatePreview();
            }
        }

        private void tsBtnPrinterSettings_Click(object sender, EventArgs e)
        {
            PrintDialog.Document = this.mDocument;
            PrintDialog.ShowDialog();
        }

        #endregion


        private void RadPrintPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            SwitchPrintDocumentHandlers(mDocument, false);
        }

        private void SwitchPrintDocumentHandlers(PrintDocument Document, bool Attach)
        {
            if (Document == null) return;
            if (Attach)
            {
                mDocument.BeginPrint += new PrintEventHandler(mDocument_BeginPrint);
                mDocument.PrintPage += new PrintPageEventHandler(mDocument_PrintPage);
                mDocument.EndPrint += new PrintEventHandler(mDocument_EndPrint);
            }
            else
            {
                mDocument.BeginPrint -= new PrintEventHandler(mDocument_BeginPrint);
                mDocument.PrintPage -= new PrintPageEventHandler(mDocument_PrintPage);
                mDocument.EndPrint -= new PrintEventHandler(mDocument_EndPrint);
            }
        }

        public static ArrayList _cutString(Graphics g, string text, Font font, float width)
        {
            width -= 8;
            ArrayList __result = new ArrayList();
            SizeF __stringSize = g.MeasureString(text, font);
            if (__stringSize.Width > width)
            {
                int _tailFirst = 0;
                int _tail = 0;
                int _lastCutPoint = -1;
                int _lastThaiPoint = -1;
                char _lastChar = ' ';

                while (_tail < text.Length)
                {
                    char __getChar = text[_tail];
                    if (__getChar <= ' ' || (__getChar >= ';' && __getChar <= '@') ||
                        (__getChar >= 'ก' && __getChar <= 'ฮ' && _tail - _lastThaiPoint > 2 && _lastChar != 'า' && !(_lastChar >= 'เ' && _lastChar <= 'โ')) ||
                        (__getChar >= 'เ' && __getChar <= 'โ') || (__getChar >= '0' && __getChar <= 'z' && _lastChar >= 'ก' && _lastChar <= 'ฮ'))
                    {
                        _lastCutPoint = _tail;
                        _lastThaiPoint = _lastCutPoint;
                    }
                    _lastChar = text[_tail];
                    __stringSize = g.MeasureString(text.Substring(_tailFirst, _tail - _tailFirst), font);
                    if (__stringSize.Width > width)
                    {
                        if (_lastCutPoint == -1)
                        {
                            _lastCutPoint = _tail;
                            _lastThaiPoint = _lastCutPoint;
                        }
                        __result.Add(text.Substring(_tailFirst, (_lastCutPoint - _tailFirst)));
                        _tailFirst = _lastCutPoint;
                        _lastCutPoint = -1;
                        _lastThaiPoint = -1;
                    }
                    _tail++;
                }// while
                if (_tailFirst != _lastCutPoint)
                {
                    __result.Add(text.Substring(_tailFirst, (text.Length - _tailFirst)));
                }
            }
            else
            {
                __result.Add(text);
            }
            return __result;
        }

        private string __checkNumstr(string result)
        {
            string __result = "";

            string format2 = MyLib._myGlobal._getFormatNumber("m02"); //_global._getFormatNumber("m01");   
            try
            {
                if (result != null)
                {
                    //double __setNumber = Double.Parse(result);
                    //if (__setNumber > 0)
                    //{
                    //    __result = string.Format(format2, __setNumber);
                    //}
                    //else
                    //{
                    //    __result = "";
                    //}
                    if (result != null)
                    {
                        Decimal __value = (result == null) ? 0M : MyLib._myGlobal._decimalPhase(result);
                        if (__value != 0)
                        {
                            __result = string.Format(format2, double.Parse(result));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        private string __checkNumstrThird(string result)
        {
            string __result = "";
            string format2 = MyLib._myGlobal._getFormatNumber("m03"); //_global._getFormatNumber("m01");   
            try
            {
                if (result != null)
                {
                    Decimal __value = (result == null) ? 0M : MyLib._myGlobal._decimalPhase(result);
                    if (__value != 0)
                    {
                        __result = string.Format(format2, double.Parse(result));
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        /*public static Decimal _decimalPhase(string value)
                {
                    decimal __getValue = 0M;
                    if (value.Length != 0)
                    {
                        decimal __value = 0M;
                        if (Decimal.TryParse(value, out __value) == true)
                        {
                            __getValue = __value;
                        }
                    }
                    //Debug.Print(value.ToString() + "," + __getValue.ToString());
                    return __getValue;
                }*/
        private string __checkNumstrDiscount(string result)
        {
            string __result = "";
            string format2 = MyLib._myGlobal._getFormatNumber("m02"); //_global._getFormatNumber("m01");   
            try
            {
                if (result != null)
                {
                    string[] __splitText = result.Split('%');
                    if (__splitText.Length > 1)
                    {
                        __result = result;
                    }
                    else
                    {
                        double __setNumber = Double.Parse(result);
                        if (__setNumber > 0)
                        {
                            __result = string.Format(format2, __setNumber);
                        }
                        else
                        {
                            __result = "";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        private string __checkNumstrNone(string result)
        {
            string __result = "";
            string format2 = MyLib._myGlobal._getFormatNumber("m00"); //_global._getFormatNumber("m01");   
            try
            {
                if (result != null)
                {
                    double __setNumber = Double.Parse(result);
                    if (__setNumber > 0)
                    {
                        __result = string.Format(format2, __setNumber);
                    }
                    else
                    {
                        __result = "";
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return __result;
        }

        private int __checkInt(string result)
        {
            int __result = 0;
            try
            {
                if (result != null)
                {
                    if (result != "")
                    {
                        // __result = MyLib._myGlobal._intPhase(result);
                        __result = MyLib._myGlobal._intPhase(result);
                    }
                }
            }
            catch (Exception ex)
            {
                __result = 0;
            }
            return __result;
        }

        private string __checkString(string result)
        {
            string __result = "";
            try
            {
                if (result != null)
                {
                    if (result != "")
                    {
                        __result = result;
                    }
                }
            }
            catch (Exception ex)
            {
                __result = "";
            }
            return __result;
        }
    }


}