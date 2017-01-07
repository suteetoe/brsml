using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace SMLERPAPARControl._depositControl
{
    public partial class _po_so_deposit_control : UserControl
    {
        public delegate void _afterSaveEventHandler();
        public event _afterSaveEventHandler _afterSave;

        public string _screen_code_temp = "";
        public string _docFormatCode = "";
        public _po_so_deposit_screen_more_control _screenMore;
        private MyLib._myPanel _myPanelGl;
        private System.Windows.Forms.TabPage _tab_gl;
        private SMLERPGLControl._journalScreen _glScreenTop;
        private SMLERPGLControl._withHoldingTax _withHoldingTax;
        private SMLERPGLControl._glDetail _glDetail;
        private _g.g._transControlTypeEnum _ictransControlTypeTemp;
        private int _buildCount = 0;
        private string _oldDocNo = "";
        private SMLERPGLControl._vatBuy _vatBuy;
        private SMLERPGLControl._vatSale _vatSale;

        //toe
        public bool _showPrintDialogByCtrl = false;
        public bool _printFormAfterSave = false;

        public void _glScreenCheck()
        {
            if (this._glScreenTop != null)
            {
                Boolean __glManual = (((int)MyLib._myGlobal._decimalPhase(this._glScreenTop._getDataStr(_g.d.gl_journal._trans_direct))) == 1) ? true : false;
                this._glScreenTop._enabedControl(_g.d.gl_journal._doc_format_code, false);
                this._glScreenTop._enabedControl(_g.d.gl_journal._doc_no, false);
                this._glScreenTop._enabedControl(_g.d.gl_journal._doc_date, false);
                this._glScreenTop._enabedControl(_g.d.gl_journal._ref_date, __glManual);
                this._glScreenTop._enabedControl(_g.d.gl_journal._ref_no, __glManual);
                this._glScreenTop._enabedControl(_g.d.gl_journal._book_code, __glManual);
                this._glScreenTop._enabedControl(_g.d.gl_journal._journal_type, __glManual);
                this._glScreenTop._enabedControl(_g.d.gl_journal._description, __glManual);
                this._glScreenTop._enabedControl(_g.d.gl_journal._ap_ar_code, __glManual);
                this._glScreenTop._enabedControl(_g.d.gl_journal._ap_ar_originate_from, __glManual);

                MyLib._myDateBox __myTextbox = (MyLib._myDateBox)this._screenTop._getControl(_g.d.ic_trans._doc_date);

                if (__myTextbox != null)
                {
                    _g.g._accountPeriodClass __accountPeriod = _g.g._accountPeriodClassFind(this._screenTop._getDataDate(_g.d.ic_trans._doc_date));
                    if (__accountPeriod != null)
                    {
                        this._glScreenTop._setDataStr(_g.d.gl_journal._period_number, __accountPeriod._number.ToString());
                        this._glScreenTop._setDataStr(_g.d.gl_journal._account_year, __accountPeriod._year.ToString());
                    }
                }
                //
                this._glScreenTop._setDataDate(_g.d.gl_journal._doc_date, MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ic_trans._doc_date)));
                this._glScreenTop._setDataStr(_g.d.gl_journal._doc_no, this._screenTop._getDataStr(_g.d.ic_trans._doc_no));
                this._glScreenTop.Invalidate();
                //
                this._glDetail._glDetailGrid._isEdit = __glManual;
                this._glDetail._glDetailGrid.Invalidate();
            }
        }

        public void _glCreate()
        {
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                this._myPanelGl = new MyLib._myPanel();
                this._myPanelGl.SuspendLayout();
                //
                // _myPanelGl
                // 
                this._myPanelGl._switchTabAuto = false;
                this._myPanelGl.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this._myPanelGl.CornerPicture = null;
                this._myPanelGl.Dock = System.Windows.Forms.DockStyle.Fill;
                this._myPanelGl.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this._myPanelGl.Location = new System.Drawing.Point(0, 0);
                this._myPanelGl.Name = "_myPanel2";
                this._myPanelGl.Padding = new System.Windows.Forms.Padding(4);
                this._myPanelGl.Size = new System.Drawing.Size(880, 282);
                this._myPanelGl.TabIndex = 1;
                //
                this._myPanelGl.ResumeLayout(false);
                //
                this._tab_gl = new System.Windows.Forms.TabPage();
                this._tab_gl.SuspendLayout();
                this._tab.Controls.Add(this._tab_gl);
                // 
                // tab_gl
                // 
                this._tab_gl.Controls.Add(this._myPanelGl);
                this._tab_gl.ImageKey = "document_add.png";
                this._tab_gl.Location = new System.Drawing.Point(4, 23);
                this._tab_gl.Name = "tab_gl";
                this._tab_gl.Size = new System.Drawing.Size(66, 0);
                this._tab_gl.TabIndex = 1;
                this._tab_gl.Text = "tab_gl";
                this._tab_gl.UseVisualStyleBackColor = true;
                //
                this._tab_gl.ResumeLayout(false);

                this._glScreenTop = new SMLERPGLControl._journalScreen();
                this._glDetail = new SMLERPGLControl._glDetail();
                this._glScreenTop.Dock = DockStyle.Top;
                this._glDetail.Dock = DockStyle.Fill;
                this._myPanelGl.Controls.Add(this._glDetail);
                this._myPanelGl.Controls.Add(this._glScreenTop);
                //
                this._glScreenTop._checkBoxChanged += (s1, name) =>
                {
                    if (name.Equals(_g.d.gl_journal._trans_direct))
                    {
                        this._glScreenCheck();
                    }
                };
                this._glScreenCheck();
            }
        }

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                if (value != _g.g._transControlTypeEnum.ว่าง)
                {
                    this._ictransControlTypeTemp = value;
                    this._build();
                    this._screenTop._icTransControlType = value;
                    this._screenBottom._icTransControlType = value;
                    this._payControl._icTransControlType = value;
                    //_myManageData       

                    this._myManageData1._dataList._lockRecord = true;
                    this._myManageData1._dataList._isLockDoc = true;

                    this._myManageData1._displayMode = 0;
                    this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
                    this._myManageData1._manageButton = this._myToolbar;
                    this._myManageData1._manageBackgroundPanel = this._myPanel1;
                    this._myManageData1._autoSize = true;
                    this._myManageData1._autoSizeHeight = 400;
                    //even
                    this._load();
                    //ManageData
                    // ตัวนี้เอาไว้อ้างเพื่อเป็นตัวเชื่อม (relations) เพื่อใช้ในการแก้ไข จะได้ Update ถูก Record
                    this._myManageData1._dataList._referFieldAdd(_g.d.ic_trans._doc_no, 1);
                    this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
                    this._myManageData1._checkEditData += _myManageData1__checkEditData;
                    this._myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
                    this._myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
                    this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
                    this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
                    this._myManageData1._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                    this._myManageData1._dataList._extraWhereEvent += _dataList__extraWhereEvent;
                    //
                    this._screenTop._textBoxSaved += new MyLib.TextBoxSavedHandler(_screenTop__textBoxSaved);
                    this._screenTop._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_screenTop__comboBoxSelectIndexChanged);
                    this._screenBottom._vatType += new _po_so_deposit_screen_bottom_control.VatTypeEventHandler(_screenBottom__vatType);
                    this._screenBottom._afterCalc += new _po_so_deposit_screen_bottom_control._afterCalcEvent(_screenBottom__afterCalc);
                    //
                    this._payControl._getTotalAmount += new SMLERPAPARControl._payControl._getTotalAmountEvent(_payControl__getTotalAmount);
                    this._payControl._getCustCode += new SMLERPAPARControl._payControl._getCustCodeEvent(_payControl___getCustCode);
                    this._payControl._payDepositGrid._getCustCode += new _payDepositAdvanceGridControl._getCustCodeEvent(_payDepositGrid__getCustCode);
                    this._payControl._payDepositGrid._getProcessDate += new _payDepositAdvanceGridControl._getProcessDateEvent(_payDepositGrid__getProcessDate);

                    if (MyLib._myGlobal._isUserLockDocument == true)
                    {
                        this._myManageData1._dataList._buttonUnlockDoc.Visible = true;
                        this._myManageData1._dataList._buttonLockDoc.Visible = true;
                        this._myManageData1._dataList._separatorLockDoc.Visible = true;
                    }

                }
            }
            get
            {
                return this._ictransControlTypeTemp;
            }
        }

        string _dataList__extraWhereEvent()
        {
            string __result = "";

            __result = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString(); // " (" + _g.d.ap_ar_trans._trans_flag + " = " + _transFlag + ") and (" + _g.d.ap_ar_trans._trans_type + " = " + _transType + ")"; ;
            // toe ใส่สาขาเข้าไป filter
            if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only == true && _g.g._companyProfile._change_branch_code == false)
            {
                __result = __result + " and " + _g.d.ap_ar_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\' ";
            }

            return __result;

        }

        bool _myManageData1__checkEditData(int row, MyLib._myGrid sender)
        {
            int __usedStatus = 0;
            int __usedStatus2 = 0;
            int __docSuccess = 0;
            int __lastStatus = 0;
            int __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
            int __usedStatusColumn2 = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status_2);
            int __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
            int __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
            if (__usedStatusColumn != -1) __usedStatus = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn).ToString());
            if (__usedStatusColumn2 != -1) __usedStatus2 = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn2).ToString());
            if (__docSuccessColumn != -1) __docSuccess = MyLib._myGlobal._intPhase(sender._cellGet(row, __docSuccessColumn).ToString());
            if (__lastStatusColumn != -1) __lastStatus = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
            Boolean __result = (__usedStatus == 1 || __usedStatus2 == 1 || __docSuccess == 1 || __lastStatus == 1) ? false : true;

            return __result;
        }

        public string _screen_code
        {
            set
            {
                this._screen_code_temp = value;
                //somruk ทำให้ Screen_code ไม่มีค่า 
                //this._screenTop._screen_code = value;
                string __tabPay = "tab_pay";
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                        this._tab.TabPages[__tabPay].Tag = "tab_pay_in";
                        break;
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                        this._tab.TabPages[__tabPay].Tag = "tab_pay_out";
                        break;
                }
                this._tab.Invalidate();
            }
            get
            {
                return this._screen_code_temp;
            }
        }

        public _po_so_deposit_control()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                _g.g._checkOpenPeriod();
            }
        }

        void _screenBottom__afterCalc()
        {
            if (this._payControl != null)
            {
                this._payControl._reCalc();
            }
            //
            if (this._vatBuy != null)
            {
                this._vatBuy._updateFirstRow(this._vatBuy__vatRequest());
            }
            else
                if (this._vatSale != null)
            {
                this._vatSale._updateFirstRow(this._vatSale__vatRequest());
            }
        }

        void _screenTop__comboBoxSelectIndexChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_trans._inquiry_type))
            {
                this._checkEnable();
            }
            else if (name.Equals(_g.d.ic_trans._vat_type))
            {
                this._screenBottom._calcVat();
                this._payControl._reCalc();
            }

        }

        void _checkEnable()
        {
            if (this._screenTop._getControl(_g.d.ic_trans._inquiry_type) != null)
            {
                try
                {
                    int __getData = MyLib._myGlobal._intPhase(this._screenTop._getDataStr(_g.d.ic_trans._inquiry_type));
                    if (this._payControl != null)
                    {
                        this._payControl.Enabled = (__getData == 0) ? true : false;
                    }
                    if (this._withHoldingTax != null)
                    {
                        this._withHoldingTax.Enabled = (__getData == 0) ? true : false;
                    }
                }
                catch
                {
                }
            }
        }

        _g.g._vatTypeEnum _screenBottom__vatType(object sender)
        {
            _g.g._vatTypeEnum __result = _g.g._vatTypeEnum.ภาษีรวมใน;
            try
            {
                int __vatType = Int32.Parse(this._screenTop._getDataStr(_g.d.ic_trans._vat_type));
                switch (__vatType)
                {
                    case 0: __result = _g.g._vatTypeEnum.ภาษีแยกนอก; break;
                    case 1: __result = _g.g._vatTypeEnum.ภาษีรวมใน; break;
                    case 2: __result = _g.g._vatTypeEnum.ยกเว้นภาษี; break;
                }
            }
            catch
            {
            }
            return __result;
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
                int __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
                int __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                if (__usedStatusColumn != -1)
                {
                    // มีการนำไปใช้แล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.LightSeaGreen;
                    }
                }
                if (__docSuccessColumn != -1)
                {
                    // มีการอ้างอิงครบแล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __docSuccessColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.SlateBlue;
                    }
                }
                if (__lastStatusColumn != -1)
                {
                    // เอกสารมีการยกเลิก
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.Red;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        string _payControl___getCustCode()
        {
            return this._screenTop._getDataStr(_g.d.ic_trans._cust_code);
        }

        DateTime _payDepositGrid__getProcessDate()
        {
            return MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ic_trans._doc_date));
        }

        string _payDepositGrid__getCustCode()
        {
            return this._screenTop._getDataStr(_g.d.ic_trans._cust_code);
        }

        void _screenTop__textBoxSaved(object sender, string name)
        {
            if (this._withHoldingTax != null)
            {
                this._withHoldingTax._custCode = this._screenTop._getDataStr(_g.d.ic_trans._cust_code);
                this._withHoldingTax._docDate = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ic_trans._doc_date));
            }
            if (name.Equals(_g.d.ic_trans._doc_date))
            {
                string __docDate = this._screenTop._getDataStr(_g.d.ic_trans._doc_date).ToString();
                if (__docDate.Length == 0)
                {
                    this._screenTop._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
                }
                this._setDefaultDate();
            }
            this._glScreenCheck();
        }

        decimal _payControl__getTotalAmount()
        {
            decimal __result = 0M;
            __result = this._screenBottom._getDataNumber(_g.d.ic_trans._total_amount);
            return (decimal)__result;
        }

        void _build()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            if (this._buildCount++ > 0)
            {
                MessageBox.Show("grid สร้างมากกว่า 1 ครั้ง");
            }
            string __formatNumber = _g.g._getFormatNumberStr(3);
            this._detailGrid._table_name = _g.d.ap_ar_trans_detail._table;
            this._detailGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 255, 80, true, false, true, false, "", "", "", _g.d.ap_ar_trans_detail._description);
            this._detailGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 20, true, false, true, false, __formatNumber);
            this._detailGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_detailGrid__afterCalcTotal);
            this._detailGrid._totalCheck += _detailGrid__totalCheck;
            ((MyLib._myGrid._columnType)this._detailGrid._columnList[this._detailGrid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_amount)])._plusOnly = true;
            //
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    this._vatSale = new SMLERPGLControl._vatSale();
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLPOSLite)
                    {
                        this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ขาย);
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    this._vatBuy = new SMLERPGLControl._vatBuy();
                    //if (this._withHoldingTax != null)
                    //{
                    this._withHoldingTax = new SMLERPGLControl._withHoldingTax(SMLERPGLControl._withHoldingTaxType.ซื้อ);
                    //}
                    break;
            }
            // ภาษี
            string __tabVatCode = "tab_vat";
            if (this._vatBuy == null && this._vatSale == null)
            {
                this._tab.TabPages[__tabVatCode].Dispose();
            }
            else
            {
                if (this._vatBuy != null)
                {
                    this._vatBuy.Dock = DockStyle.Fill;
                    this._tab.TabPages[__tabVatCode].Controls.Add(this._vatBuy);
                    this._vatBuy._vatRequest += new SMLERPGLControl._vatBuy.VatRequestDataEventHandler(_vatBuy__vatRequest);
                }
                else
                    if (this._vatSale != null)
                {
                    this._vatSale.Dock = DockStyle.Fill;
                    this._tab.TabPages[__tabVatCode].Controls.Add(this._vatSale);
                    this._vatSale._vatRequest += new SMLERPGLControl._vatSale.VatRequestDataEventHandler(_vatSale__vatRequest);
                }
            }
            // ภาษีหัก ณ. ที่จ่าย
            string __tabWHTCode = "tab_wht";
            if (this._withHoldingTax == null)
            {
                this._tab.TabPages[__tabWHTCode].Dispose();
            }
            else
            {
                this._withHoldingTax.Dock = DockStyle.Fill;
                this._tab.TabPages[__tabWHTCode].Controls.Add(this._withHoldingTax);
                string __tabName = "";
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                        __tabName = _g.d.ic_trans._tab_wht_in;
                        break;
                    case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                        __tabName = _g.d.ic_trans._tab_wht_out;
                        break;
                }
                this._tab.TabPages[__tabWHTCode].Tag = __tabName;
                this._payControl._getTotalTaxAmount += new SMLERPAPARControl._payControl._getTotalTaxAmountEvent(_payControl__getTotalTaxAmount);
                this._withHoldingTax._mainGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_mainGrid__afterCalcTotal);
            }
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    this._glCreate();
                    break;
            }
            //
            this._screenMore = new _po_so_deposit_screen_more_control();
            this._screenMore._icTransControlType = _ictransControlTypeTemp;
            this._screenMore.Dock = DockStyle.Fill;
            this.tab_more.Controls.Add(this._screenMore);
        }

        private bool _detailGrid__totalCheck(object sender, int row, int column)
        {
            return (this._detailGrid._cellGet(row, _g.d.ap_ar_trans_detail._remark).ToString().Trim().Length == 0) ? false : true;
        }

        decimal _payControl__getTotalTaxAmount()
        {
            // ดึง ภาษี หัก ณ. ที่จ่าย
            if (this._withHoldingTax == null)
            {
                return 0M;
            }
            this._withHoldingTax._mainGrid._calcTotal(false);
            int __columnNumber = this._withHoldingTax._mainGrid._findColumnByName(_g.d.gl_wht_list._tax_value);
            decimal __result = ((MyLib._myGrid._columnType)this._withHoldingTax._mainGrid._columnList[__columnNumber])._total;
            //Console.WriteLine(__result.ToString());
            return __result;
        }

        void _mainGrid__afterCalcTotal(object sender)
        {
            if (this._payControl != null)
            {
                if (this._withHoldingTax != null)
                {
                    this._withHoldingTax._mainGrid._calcTotal(false);
                }
                this._payControl._reCalc();
            }
        }

        SMLERPGLControl._vatRequestData _vatSale__vatRequest()
        {
            SMLERPGLControl._vatRequestData __result = new SMLERPGLControl._vatRequestData();
            __result._vatDocNo = this._screenTop._getDataStr(_g.d.ic_trans._tax_doc_no);
            __result._vatDate = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ic_trans._tax_doc_date).ToString());
            __result._vatRate = this._screenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
            __result._vatBaseAmount = this._screenBottom._getDataNumber(_g.d.ic_trans._total_before_vat);
            __result._vatAmount = this._screenBottom._getDataNumber(_g.d.ic_trans._total_vat_value);
            __result._vatTotalAmount = __result._vatBaseAmount + __result._vatAmount;
            __result._vatExceptAmount = this._screenBottom._getDataNumber(_g.d.ic_trans._total_except_vat);
            return __result;
        }

        SMLERPGLControl._vatRequestData _vatBuy__vatRequest()
        {
            SMLERPGLControl._vatRequestData __result = new SMLERPGLControl._vatRequestData();
            __result._vatDocNo = this._screenTop._getDataStr(_g.d.ic_trans._tax_doc_no);
            __result._vatDate = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ic_trans._tax_doc_date).ToString());
            __result._vatRate = this._screenBottom._getDataNumber(_g.d.ic_trans._vat_rate);
            __result._vatBaseAmount = this._screenBottom._getDataNumber(_g.d.ic_trans._total_before_vat);
            __result._vatAmount = this._screenBottom._getDataNumber(_g.d.ic_trans._total_vat_value);
            __result._vatTotalAmount = __result._vatBaseAmount + __result._vatAmount;
            __result._vatExceptAmount = this._screenBottom._getDataNumber(_g.d.ic_trans._total_except_vat);
            return __result;
        }

        void _detailGrid__afterCalcTotal(object sender)
        {
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    {
                        MyLib._myGrid __grid = (MyLib._myGrid)sender;
                        decimal __total = ((MyLib._myGrid._columnType)__grid._columnList[__grid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_amount)])._total;
                        this._screenBottom._setDataNumber(_g.d.ic_trans._total_value, __total);
                        this._screenBottom._calcVat();
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    {
                        MyLib._myGrid __grid = (MyLib._myGrid)sender;
                        decimal __total = ((MyLib._myGrid._columnType)__grid._columnList[__grid._findColumnByName(_g.d.ap_ar_trans_detail._sum_debt_amount)])._total;
                        this._screenBottom._setDataNumber(_g.d.ic_trans._total_value, __total);
                        this._screenBottom._calcVat();
                    }
                    break;
            }
            this._payControl._reCalc();
        }

        void _setDefaultDate()
        {
            DateTime __getDate = MyLib._myGlobal._convertDate(this._screenTop._getDataStr(_g.d.ic_trans._doc_date));
            this._payControl._defaultDate = __getDate;
        }

        void _myManageData1__clearData()
        {
            this._screenTop._clear();
            this._screenBottom._clear();
            this._detailGrid._clear();
            this._payControl._clear();
            if (this._glScreenTop != null)
            {
                this._glScreenTop._clear();
                this._glDetail._glDetailGrid._clear();
            }
            //
            if (this._vatBuy != null)
            {
                this._vatBuy._vatGrid._clear();
            }
            if (this._vatSale != null)
            {
                this._vatSale._vatGrid._clear();
            }
            if (this._withHoldingTax != null)
            {
                _withHoldingTax._clear();
            }
            //
            this._screenTop._isChange = false;
            this._screenBottom._setDataNumber(_g.d.ic_trans._vat_rate, _g.g._companyProfile._vat_rate);

        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (this._screenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._screenTop._isChange = false;
                }
            }
            return (result);
        }

        void _myManageData1__newDataClick()
        {
            this._myManageData1__clearData();
            this._oldDocNo = "";
            Control __codeControl = this._screenTop._getControl(_g.d.ic_trans._doc_date);
            __codeControl.Enabled = true;
            this._screenBottom._setDataNumber(_g.d.ic_trans._vat_rate, _g.g._companyProfile._vat_rate);
            this._screenTop._newData();
            this._screenMore._newData();
            this._screenTop._focusFirst();

        }

        int _getColumnNumberDocNo()
        {
            return this._myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            //Boolean __isdeleteData = true;
            int __docDateColumn = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date);
            //if (__isdeleteData)
            {
                StringBuilder __myQuery = new StringBuilder();

                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                {
                    MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                    string __getDocNo = this._myManageData1._dataList._gridData._cellGet(__getData.row, this._getColumnNumberDocNo()).ToString();

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        if (__docDateColumn != -1)
                        {
                            DateTime __getDocDate = (DateTime)this._myManageData1._dataList._gridData._cellGet(__getData.row, __docDateColumn);
                            if (_g.g._checkOpenPeriod(__getDocDate) == false)
                            {
                                this._myManageData1._dataList._refreshData();
                                break;
                            }


                        }
                        else
                        {
                            if (_g.g._checkOpenPeriod() == false)
                            {
                                this._myManageData1._dataList._refreshData();
                                break;
                            }
                        }
                    }
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format("delete from {0} {1}", this._myManageData1._dataList._tableName, __getData.whereString + " and " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString())));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ap_ar_trans_detail where doc_no = \'" + __getDocNo + "\'"));
                    //
                    if (this._vatBuy != null) __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString()));
                    if (this._vatSale != null) __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString()));
                    //
                    __myQuery.Append(this._payControl._queryDelete(__getDocNo));
                    // ลบ GL
                    __myQuery.Append(this._glDeleteQuery(__getDocNo));
                } // for
                __myQuery.Append("</node>");
                //
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                string __result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    SMLProcess._docFlow __process = new SMLProcess._docFlow();
                    __process._processAll(_g.g._transControlTypeEnum.ว่าง, "", "");
                    //
                    MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                    this._myManageData1._dataList._refreshData();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                this._myManageData1__clearData();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList __rowDataArray = (ArrayList)rowData;
                this._oldDocNo = __rowDataArray[this._myManageData1._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no)].ToString();

                string __queryWhere1 = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
                StringBuilder __query = new StringBuilder();
                // #0
                int __tableCount = 0;
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + whereString + " and " + __queryWhere1));
                __tableCount++;
                // #1
                string __queryWhere2 = _g.d.ap_ar_trans_detail._trans_type + "=" + _g.g._transTypeGlobal._transType(this._icTransControlType).ToString() + " and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + __queryWhere2 + " order by " + _g.d.ap_ar_trans_detail._line_number));
                // #2
                __query.Append(this._payControl._queryLoad(this._oldDocNo));
                __tableCount += this._payControl._tableLoadCount;
                //
                int __vatBuyTableNumber = -1;
                if (this._vatBuy != null)
                {
                    __tableCount++;
                    string __query2 = "select * from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__query2));
                    __vatBuyTableNumber = __tableCount;
                }
                int __vatSaleTableNumber = -1;
                if (this._vatSale != null)
                {
                    __tableCount++;
                    string __query2 = "select * from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__query2));
                    __vatSaleTableNumber = __tableCount;
                }
                // Load GL
                int __glTableNumber = -1;
                int __glDetailTableNumber = -1;
                if (this._glScreenTop != null)
                {
                    __tableCount++;
                    __glTableNumber = __tableCount;
                    //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + this._oldDocNo + "\' "));
                    __tableCount++;
                    __glDetailTableNumber = __tableCount;
                    // __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + this._oldDocNo + "\' order by " + _g.d.gl_journal_detail._debit_or_credit + "," + _g.d.gl_journal_detail._account_code));

                    __query.Append(this._glDetail._loadDataQuery(this._oldDocNo));
                    __tableCount += 5;

                }
                int __whtTableNumber = -1;
                if (this._withHoldingTax != null)
                {
                    __whtTableNumber = __tableCount + 1;
                    __tableCount += 2;
                    __query.Append(this._withHoldingTax._queryLoad(this._oldDocNo, this._icTransControlType));
                }
                //
                __query.Append("</node>");
                //
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                DataTable __dt1 = ((DataSet)__getData[0]).Tables[0];
                this._screenTop._docFormatCode = "";
                try
                {
                    this._screenTop._docFormatCode = __dt1.Rows[0][_g.d.ic_trans._doc_format_code].ToString();
                }
                catch
                {
                }
                this._screenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._screenBottom._loadData(((DataSet)__getData[0]).Tables[0]);
                this._screenMore._loadData(((DataSet)__getData[0]).Tables[0]);
                this._detailGrid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                this._payControl._loadToScreen(__getData, 2);
                //
                if (__vatBuyTableNumber != -1)
                {
                    this._vatBuy._vatGrid._loadFromDataTable(((DataSet)__getData[__vatBuyTableNumber]).Tables[0]);
                }
                if (__vatSaleTableNumber != -1)
                {
                    this._vatSale._vatGrid._loadFromDataTable(((DataSet)__getData[__vatSaleTableNumber]).Tables[0]);
                }
                // GL
                if (__glTableNumber != -1)
                {
                    this._glScreenTop._loadData((((DataSet)__getData[__glTableNumber]).Tables[0]));
                    this._glDetail._glDetailGrid._loadFromDataTable(((DataSet)__getData[__glDetailTableNumber]).Tables[0]);
                }
                if (this._withHoldingTax != null)
                {
                    this._withHoldingTax._loadToScreen(__getData, __whtTableNumber);
                }

                // Search
                this._screenTop._search(false);
                if (forEdit)
                {
                    this._screenTop._focusFirst();
                }
                // ให้คำนวณยอดรวม             
                this._screenTop.Invalidate();
                if (this._payControl != null)
                {
                    if (this._withHoldingTax != null)
                    {
                        this._withHoldingTax._mainGrid._calcTotal(false);
                    }
                    this._payControl._reCalc();
                }
                return (true);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            return (false);
        }


        private void _saveButton_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                _showPrintDialogByCtrl = true;
            }

            _saveData();
        }

        private void _printData()
        {
            try
            {
                string __docNo = this._screenTop._getDataStr(_g.d.ic_trans._doc_no);
                bool __printResult = SMLERPReportTool._global._printForm(this._screenTop._docFormatCode, __docNo, _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString(), _showPrintDialogByCtrl);

                if (__printResult == true)
                {
                    // update print count
                    MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                    __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
                }

            }
            catch
            {
            }
        }

        private string _glDeleteQuery(string docNo)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + docNo + "\'"));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + docNo + "\'"));
            return __myQuery.ToString();
        }

        private void _saveData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __getEmtry = this._screenTop._checkEmtryField();
            Boolean __pass = true;
            if (__getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, __getEmtry);
                __pass = false;
            }
            else
            {
                if (this._payControl != null && this._payControl.Enabled == true)
                {
                    if (this._payControl._checker() == false)
                    {
                        __pass = false;
                    }
                }
            }

            if (__pass == true && (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                   MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                   MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                   MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional))
            {
                __pass = _g.g._checkOpenPeriod(this._screenTop._getDataDate(_g.d.ic_trans._doc_date));
            }

            if (__pass == true)
            {
                if (MyLib._myGlobal._OEMVersion == "tvdirect")
                {
                    // check amount
                    decimal __total_amount = this._screenBottom._getDataNumber(_g.d.ic_trans._total_amount);
                    if (__total_amount == 0M)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่มียอดเงินมัดจำ"), "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __pass = false;
                    }
                }
            }


            if (__pass == true && _g.g._companyProfile._warning_branch_input)
            {
                if (this._screenMore._getControl(_g.d.ic_trans._branch_code) != null)
                {
                    if (this._screenMore._getDataStr(_g.d.ic_trans._branch_code).Trim().Length == 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อน สาขา"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __pass = false;
                    }
                }
            }


            // singha 
            if (__pass == true && MyLib._myGlobal._OEMVersion == ("SINGHA")
                && (
                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ ||
                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน))
            {
                string __cust_code = this._screenTop._getDataStr(_g.d.ic_trans._cust_code);
                DataTable __custStatusDataTable = __myFrameWork._queryShort("select " + _g.d.ar_customer._status + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __cust_code + "\'").Tables[0];
                if (__custStatusDataTable.Rows.Count > 0)
                {
                    int __arStatus = MyLib._myGlobal._intPhase(__custStatusDataTable.Rows[0][_g.d.ar_customer._status].ToString());

                    if (__arStatus == 1)
                    {
                        MessageBox.Show("สถานะ ลูกค้าปิดการใช้งาน");
                        return;
                    }

                }

            }

            Boolean __glManual = false;
            if (__pass == true && this._glScreenTop != null)
            {
                __glManual = (((int)MyLib._myGlobal._decimalPhase(this._glScreenTop._getDataStr(_g.d.gl_journal._trans_direct))) == 1) ? true : false;
                if (__glManual == true)
                {
                    __getEmtry = this._glScreenTop._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                        __pass = false;
                    }
                }
            }

            if (__pass)
            {
                // เช็คยอดคืน
                if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน ||
                    this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน ||
                    this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน ||
                    this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน)
                {
                    _g.g._depositAdvanceEnum __mode = _g.g._depositAdvanceEnum.จ่ายเงินมัดจำ;

                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                            __mode = _g.g._depositAdvanceEnum.รับเงินมัดจำ;
                            break;
                        case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                            __mode = _g.g._depositAdvanceEnum.รับเงินล่วงหน้า;
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                            __mode = _g.g._depositAdvanceEnum.จ่ายเงินมัดจำ;
                            break;
                        case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                            __mode = _g.g._depositAdvanceEnum.จ่ายเงินล่วงหน้า;
                            break;
                    }

                    decimal __balanceDocAmount = 0M;

                    string __docRef = this._screenTop._getDataStr(_g.d.ic_trans._doc_ref);
                    SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                    DataTable __getData = __process._depositBalanceDoc(__mode, _oldDocNo, 0, "", "", __docRef, __docRef, DateTime.Now, "");
                    if (__getData.Rows.Count > 0)
                    {
                        __balanceDocAmount = MyLib._myGlobal._decimalPhase(__getData.Rows[0][_g.d.ap_ar_resource._balance_amount].ToString());
                    }

                    if (this._screenBottom._getDataNumber(_g.d.ic_trans._total_amount) > __balanceDocAmount)
                    {
                        MessageBox.Show("ยอดคืน ไม่สัมพันธ์กับยอดคงเหลือ");
                        __pass = false;
                    }
                }
            }

            if (__pass)
            {
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                if (this._glScreenTop != null)
                {
                    // __glManual = (((int)MyLib._myGlobal._decimalPhase(this._glScreenTop._getDataStr(_g.d.gl_journal._trans_direct))) == 1) ? true : false;
                    // ลบ GL เก่าออก
                    __myQuery.Append(this._glDeleteQuery(this._oldDocNo));
                }
                string __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
                string __transType = _g.g._transTypeGlobal._transType(this._icTransControlType).ToString();
                if (this._myManageData1._mode == 2)
                {
                    // ลบของเก่า
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans._trans_type + "=" + __transType + " and " + _g.d.ic_trans._trans_flag + "=" + __transFlag));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ap_ar_trans_detail._table + " where " + _g.d.ap_ar_trans_detail._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_type + "=" + __transType + " and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + __transFlag));
                    __myQuery.Append(this._payControl._queryDelete(this._oldDocNo));
                    if (this._vatBuy != null) __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_buy._table + " where " + _g.d.gl_journal_vat_buy._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_buy._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString()));
                    if (this._vatSale != null) __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.gl_journal_vat_sale._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.gl_journal_vat_sale._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString()));
                    if (this._glScreenTop != null)
                    {
                        // ลบ GL เก่าออก
                        __myQuery.Append(this._glDeleteQuery(this._oldDocNo));
                    }
                    if (this._withHoldingTax != null)
                    {
                        // ลบภาษีหัก ณ. ที่จ่าย
                        __myQuery.Append(this._withHoldingTax._queryDelete(this._oldDocNo, this._icTransControlType));
                    }
                }
                // เพิ่มของใหม่
                // ประกอบ
                ArrayList __main1 = this._screenTop._createQueryForDatabase();
                ArrayList __main2 = this._screenBottom._createQueryForDatabase();
                ArrayList __getMoreQuery = this._screenMore._createQueryForDatabase();
                string __docNo = this._screenTop._getDataStr(_g.d.ic_trans._doc_no);
                string __docDate = this._screenTop._getDataStrQuery(_g.d.ic_trans._doc_date);
                string __docTime = this._screenTop._getDataStr(_g.d.ic_trans._doc_time);

                string __extraValue = "";
                string __extraField = "";

                if (_g.g._companyProfile._use_sale_shift == true)
                {
                    __extraField = "," + _g.d.ic_trans._sale_shift_id;
                    __extraValue = ",\'" + _g.g._companyProfile._sale_shift_id + "\'";
                }

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + "(" + _g.d.ic_trans._trans_type + "," + _g.d.ic_trans._trans_flag + "," + __main1[0].ToString() + "," + __main2[0].ToString() + "," + __getMoreQuery[0].ToString() + __extraField + ") values (" + __transType + "," + __transFlag + "," + __main1[1].ToString() + "," + __main2[1].ToString() + "," + __getMoreQuery[1].ToString() + __extraValue + ")"));
                //
                string __fieldList = _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._doc_date + "," + _g.d.ap_ar_trans_detail._trans_type + "," + _g.d.ap_ar_trans_detail._trans_flag + "," + _g.d.ap_ar_trans_detail._sale_shift_id + ",";
                string __dataList = "\'" + this._screenTop._getDataStr(_g.d.ic_trans._doc_no) + "\'," + this._screenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + __transType + "," + __transFlag + ",\'" + _g.g._companyProfile._sale_shift_id + "\',";
                //
                this._detailGrid._updateRowIsChangeAll(true);
                __myQuery.Append(this._detailGrid._createQueryForInsert(_g.d.ap_ar_trans_detail._table, __fieldList, __dataList, false, true));
                //
                // ดึง Query จากหน้าจอรับเงิน
                __myQuery.Append(this._payControl._queryInsert(__docNo, __docDate, __docTime, this._screenTop._docFormatCode, this._screenTop._getDataStr(_g.d.ic_trans._remark)));
                //
                if (this._vatBuy != null)
                {
                    // ภาษีซื้อ
                    this._vatBuy._deleteRowIfBlank();
                    this._vatBuy._vatGrid._updateRowIsChangeAll(true);
                    string __vatFieldList = _g.d.gl_journal_vat_buy._book_code + "," + _g.d.gl_journal_vat_buy._vat_calc + "," + _g.d.gl_journal_vat_buy._trans_type + "," + _g.d.gl_journal_vat_buy._trans_flag + "," + _g.d.gl_journal_vat_buy._doc_date + "," + _g.d.gl_journal_vat_buy._doc_no + "," + _g.d.gl_journal_vat_buy._ap_code + ",";
                    string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatBuyType(this._icTransControlType).ToString() + "," + _g.g._transTypeGlobal._transType(this._icTransControlType) + "," + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString() + "," + this._screenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + this._screenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + "," + this._screenTop._getDataStrQuery(_g.d.ic_trans._cust_code) + ",";
                    __myQuery.Append(this._vatBuy._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_buy._table, __vatFieldList, __vatDataList, false, true));
                }
                if (this._vatSale != null)
                {
                    // ภาษีขาย
                    this._vatSale._vatGrid._updateRowIsChangeAll(true);
                    this._vatSale._deleteRowIfBlank();
                    string __vatFieldList = _g.d.gl_journal_vat_sale._book_code + "," + _g.d.gl_journal_vat_sale._vat_calc + "," + _g.d.gl_journal_vat_sale._trans_type + "," + _g.d.gl_journal_vat_sale._trans_flag + "," + _g.d.gl_journal_vat_sale._doc_date + "," + _g.d.gl_journal_vat_sale._doc_no + "," + _g.d.gl_journal_vat_sale._ar_code + ",";
                    string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatSaleType(this._icTransControlType).ToString() + "," + _g.g._transTypeGlobal._transType(this._icTransControlType) + "," + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString() + "," + this._screenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + this._screenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + "," + this._screenTop._getDataStrQuery(_g.d.ic_trans._cust_code) + ",";
                    __myQuery.Append(this._vatSale._vatGrid._createQueryForInsert(_g.d.gl_journal_vat_sale._table, __vatFieldList, __vatDataList, false, true));
                }
                // GL
                if (__glManual)
                {
                    Control __getControl = this._glScreenTop._getControl(_g.d.gl_journal._doc_date);
                    DateTime __getDate = ((MyLib._myDateBox)__getControl)._dateTime;
                    int __periodNumber = _g.g._accountPeriodFind(__getDate);
                    int __getColumnNumberDebit = this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);
                    int __getColumnNumberCredit = this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._credit);
                    ArrayList __getData = this._glScreenTop._createQueryForDatabase();
                    string __extData = ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberDebit])._total.ToString() + "," +
                        ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString() + "," + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();// +"," + __periodNumber.ToString();
                    string __extField = _g.d.gl_journal._debit + "," + _g.d.gl_journal._credit + "," + _g.d.gl_journal._trans_flag;// +"," + _g.d.gl_journal._period_number;
                    // head
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal._table + " (" + __getData[0].ToString() + "," + __extField + ") values (" + __getData[1].ToString() + "," + __extData + ")"));
                    // detail
                    string __fieldListGl = _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._period_number + "," + _g.d.gl_journal._journal_type + "," + _g.d.gl_journal._trans_flag + ",";
                    string __dataListGl = this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," + __periodNumber.ToString() + "," + this._glScreenTop._getDataStr(_g.d.gl_journal._journal_type) + "," + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString() + ",";
                    this._glDetail._glDetailGrid._updateRowIsChangeAll(true);
                    __myQuery.Append(this._glDetail._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldListGl, __dataListGl));
                }
                if (this._withHoldingTax != null && this._withHoldingTax.Enabled == true)
                {
                    // ภาษีหัก ณ. ที่จ่าย
                    __myQuery.Append(this._withHoldingTax._queryInsert(__docNo, __docDate, this._icTransControlType));
                }
                // update ค่าอื่นๆ เผื่อหลุด
                __myQuery.Append(_g.g._queryUpdateTrans());
                //
                __myQuery.Append("</node>");
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    SMLProcess._docFlow __process = new SMLProcess._docFlow();
                    __process._processAll(_g.g._transControlTypeEnum.ว่าง, "", "");
                    //
                    MyLib._myGlobal._displayWarning(1, null);
                    this._screenTop._isChange = false;
                    if (this._myManageData1._mode == 1)
                    {
                        this._myManageData1._afterInsertData();
                    }
                    else
                    {
                        this._myManageData1._afterUpdateData();
                    }

                    if (this._afterSave != null)
                    {
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                        {
                            this._printFormAfterSave = _checkFormPrint();
                        }
                        this._afterSave();
                    }

                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore || this._printFormAfterSave == true)
                    {

                        try
                        {
                            _printData();
                        }
                        catch
                        {
                        }
                    }

                    this._myManageData1__clearData();
                    this._screenTop._focusFirst();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _showPrintDialogByCtrl = false;

        }

        /// <summary>
        /// Even การทำงาน From Load
        /// </summary>
        /// <param name="number">ทำงานครั้งแรกที่เปิดหน้าจอ</param>

        void _load()
        {
            // toe ใส่ใน extrawhere event ไปแล้ว
            //this._myManageData1._dataList._extraWhere = _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
            this._myManageData1._dataList._loadViewFormat(_g.g._transGlobalTemplate._transTemplate(this._icTransControlType), MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData1._dataListOpen = true;
            this._myManageData1._calcArea();
            this._myManageData1__newDataClick();
        }

        private void _buttonPrint_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                _showPrintDialogByCtrl = true;
            }

            try
            {
                this._printFormAfterSave = _checkFormPrint();

                if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore || this._printFormAfterSave == true)
                {
                    _printData();
                }
            }
            catch (Exception ex)
            {
            }

            _showPrintDialogByCtrl = false;

        }

        void _rePrintForm()
        {
            // ใช้ this._oldDocNo อ้างอิง
            if (this._myToolbar.Enabled == false && this._oldDocNo.Length > 0)
            {
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                {
                    this._printFormAfterSave = _checkFormPrint();
                    if (this._afterSave != null)
                    {
                        this._afterSave();
                    }
                }

                if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore || this._printFormAfterSave == true)
                {
                    string __docNo = this._oldDocNo;

                    //string __docNo = this._icTransScreenTop._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();
                    bool __printResult = SMLERPReportTool._global._printForm(this._screenTop._docFormatCode, __docNo, _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString(), false);
                    if (__printResult == true)
                    {
                        // update print count
                        MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                        __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
                    }
                }
            }

            //// reprint 
            //if (MyLib._myGlobal._OEMVersion == "tvdirect" && this._oldDocNo.Length > 0)
            //{
            //    string __docNo = this._oldDocNo;
            //    SMLERPReportTool._global._printForm(this._screenTop._docFormatCode, __docNo, _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString(), false);
            //}
        }

        bool _checkFormPrint()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            bool __result = false;
            try
            {
                string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._screenTop._docFormatCode + "\'";
                DataTable __resultCheck = __myFrameWork._queryShort(__query).Tables[0];
                if (__resultCheck.Rows.Count > 0)
                {
                    if (__resultCheck.Rows[0][_g.d.erp_doc_format._form_code].ToString().Length > 0)
                    {
                        __result = true;
                    }
                }
            }
            catch
            {
            }

            return __result;
        }

        private string _docNoAdd(string oldDocNo, string newDocNo)
        {
            if (newDocNo.Trim().Length == 0)
            {
                return oldDocNo;
            }
            if (oldDocNo.Length == 0)
            {
                return string.Concat("\'", newDocNo, "\'");
            }
            return string.Concat(oldDocNo, ",\'", newDocNo, "\'");
        }

        void _docFlowRecheck()
        {
            string __docNoList = "";
            __docNoList = this._docNoAdd(__docNoList, this._oldDocNo);

            // get ref doc and add to __docNoList
            //if (this._oldDocRef.Length > 0)
            //{
            //    __docNoList = __docNoList + "," + this._oldDocRef;
            //}


            SMLProcess._docFlow __process = new SMLProcess._docFlow();
            __process._processAll(this._icTransControlType, "", __docNoList);

            MessageBox.Show(this._oldDocNo + " : Recheck Doc Flow Success");


            this._myManageData1._dataList._refreshData();
            // this._myManageTrans._dataList._gridData.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control)
                {
                    switch (__keyCode)
                    {
                        case Keys.P:
                            {
                                this._rePrintForm();
                                return true;
                            }
                        case Keys.R:
                            {
                                _docFlowRecheck();
                                return true;
                            }
                            break;
                        case Keys.F12:
                            {
                                if (MyLib._myGlobal._OEMVersion.Equals("SINGHA")

                                    /*&&
                                    (this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                    this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                                    this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)*/

                                    )
                                {

                                    if (_myManageData1__checkEditData(this._myManageData1._dataList._gridData._selectRow, this._myManageData1._dataList._gridData))
                                    {
                                        // ยกเลิก เอกสาร
                                        _g._docCancelForm __docCancelForm = new _g._docCancelForm(this._oldDocNo);

                                        if (__docCancelForm.ShowDialog() == DialogResult.Yes)
                                        {
                                            if (__docCancelForm._comboCancelConfirm.SelectedIndex == 1)
                                            {
                                                int __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType);

                                                // update cancel 
                                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                                StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set is_cancel = 1, " + _g.d.ic_trans._cancel_code + "=\'" + MyLib._myGlobal._userCode + "\', " + _g.d.ic_trans._cancel_datetime + "=\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\' where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + __transFlag));
                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal._trans_flag + " =" + __transFlag));
                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + " = \'" + this._oldDocNo + "\' and " + _g.d.gl_journal_detail._trans_flag + " =" + __transFlag));

                                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_cancel_logs._table +
                                                    " (" + _g.d.erp_cancel_logs._doc_no + "," + _g.d.erp_cancel_logs._trans_flag + "," + _g.d.erp_cancel_logs._user_code + "," + _g.d.erp_cancel_logs._cancel_flag + "," + _g.d.erp_cancel_logs._cancel_datetime + "," + _g.d.erp_cancel_logs._cancel_reason + ") " +
                                                    " values " +
                                                    " (\'" + this._oldDocNo + "\', " + __transFlag + ", \'" + MyLib._myGlobal._userCode + "\', 0, \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + __docCancelForm._cancelReasonTextbox.Text + "\') "));

                                                __queryListUpdate.Append("</node>");

                                                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryListUpdate.ToString());
                                                if (__result.Length > 0)
                                                {
                                                    MessageBox.Show(__result.ToString(), "error");
                                                }
                                                else
                                                {
                                                    string __docNoList = "";

                                                    __docNoList = this._docNoAdd(__docNoList, this._oldDocNo);

                                                    // get ref doc and add to __docNoList
                                                    /*if (this._oldDocRef.Length > 0)
                                                    {
                                                        __docNoList = __docNoList + "," + this._oldDocRef;
                                                    }*/
                                                    SMLProcess._docFlow __process = new SMLProcess._docFlow();
                                                    __process._processAll(this._icTransControlType, "", __docNoList);

                                                    MessageBox.Show("ยกเลิกเอกสารสำเร็จ");
                                                    this._myManageData1._dataList._refreshData();

                                                }
                                                this._myManageData1._dataList._refreshData();
                                            }
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("เอกสารอ้างอิงไปแล้ว");
                                    }
                                }

                                return false;
                            }
                    }
                }

                if (keyData == (Keys.Shift | Keys.F12))
                {
                    {
                        // un cancel  doc
                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA")
                                    /*&& (
                                    this._transControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ||
                                            this._transControlType == _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ||
                                            this._transControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)*/
                                    )
                        {

                            if (MessageBox.Show("ต้องการเรียกเอกสารกลับมาใชังานได้ปรกติ หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                                int __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType);

                                StringBuilder __queryListUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_trans set is_cancel = 0 where doc_no = \'" + this._oldDocNo + "\' and trans_flag =" + __transFlag));
                                __queryListUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_cancel_logs._table +
                                    " (" + _g.d.erp_cancel_logs._doc_no + "," + _g.d.erp_cancel_logs._trans_flag + "," + _g.d.erp_cancel_logs._user_code + "," + _g.d.erp_cancel_logs._cancel_flag + "," + _g.d.erp_cancel_logs._cancel_datetime + ") " +
                                    " values " +
                                    " (\'" + this._oldDocNo + "\', " + __transFlag + ", \'" + MyLib._myGlobal._userCode + "\', 1, \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\') "));

                                //__queryListUpdate.Append("</node>");
                                __queryListUpdate.Append("</node>");

                                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryListUpdate.ToString());
                                if (__result.Length > 0)
                                {
                                    MessageBox.Show(__result.ToString(), "error");
                                }
                                else
                                {

                                    string __docNoList = "";

                                    __docNoList = this._docNoAdd(__docNoList, this._oldDocNo);

                                    // get ref doc and add to __docNoList
                                    /*if (this._oldDocRef.Length > 0)
                                    {
                                        __docNoList = __docNoList + "," + this._oldDocRef;
                                    }*/
                                    SMLProcess._docFlow __process = new SMLProcess._docFlow();
                                    __process._processAll(this._icTransControlType, "", __docNoList);
                                    MessageBox.Show("เรียกคืนเอกสารสำเร็จ");
                                    this._myManageData1._dataList._refreshData();

                                    // ประมวลผล GL ด้วย อย่าลืม
                                }
                                this._myManageData1._dataList._refreshData();
                            }
                            return true;
                        }
                        return false;
                    }
                }


            }
            catch
            {
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
